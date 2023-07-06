using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace DX
{
	public class 키워드
	{
		public static void 견적저장(string 파일번호) => 견적저장(상수.회사코드, 파일번호);

		public static void 견적저장(string 회사코드, string 파일번호)
		{
			string query = "SELECT * FROM CZ_SA_QTNL WITH(NOLOCK) WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_FILE = '" + 파일번호 + "'";
			DataTable dt = 디비.결과(query);

			if (dt.Rows.Count > 0)
				견적저장(dt);
		}

		public static void 견적저장(DataTable 아이템)
		{
			if (아이템 == null)
				return;

			DataTable dt = 아이템.Copy();

			// 파일번호 말고 수주번호로 들어오면 컬림 이름 변경
			if (!dt.Columns.Contains("NO_FILE") && dt.Columns.Contains("NO_SO"))
			{
				dt.Columns["NO_SO"].ColumnName = "NO_FILE";
				dt.Columns["SEQ_SO"].ColumnName = "NO_LINE";
			}

			// 컬럼 및 데이터 생성
			dt.Columns.Add("SUBJ_CODE", typeof(string));
			dt.Columns.Add("ITEM_CODE", typeof(string));

			foreach (DataRow row in dt.Rows)
			{
				if (row.RowState != DataRowState.Deleted)
				{
					row["ITEM_CODE"] = 핵심코드(row["CD_ITEM_PARTNER"] + " ‡ " + row["NM_ITEM_PARTNER"]);
					row["SUBJ_CODE"] = 핵심코드(row["NM_SUBJECT"].문자());
				}
			}

			// 저장
			디비.실행("PX_CZ_DX_QTNL_3", dt.Json("CD_COMPANY", "NO_FILE", "NO_LINE", "ITEM_CODE", "SUBJ_CODE"));
		}

		private static string 핵심코드(string 문장)
		{
			// 확실히 단어 사이에 구분져야 할 것.. 슬래쉬 요곤 좀 애매함.. 일단 고민해보자.. (구분져야 하는 CASE : DB18017984/12번)
			foreach (string f in new string[] { "\"", "|", "§", "'", ",", ".", ":", ";", "(", ")", "[", "]", "<", ">", "&", "/", "\r", "\n" })
				문장 = 문장.Replace(f, " ‡ ");

			// 사이즈 형태는 탈락
			문장 = Regex.Replace(문장, @"[WHD]\d+[-X][WHD]\d+[-X][WHD]\d+MM", "", RegexOptions.IgnoreCase);
			문장 = Regex.Replace(문장, @"\d+[-X]\d+[-X]\d+MM", "", RegexOptions.IgnoreCase);
			문장 = Regex.Replace(문장, @"\d+[-X]\d+MM", "", RegexOptions.IgnoreCase);
			
			
			
			// 주의단어
			문장 = 문장.Replace("NO-", "NO ");

			// 한 단어로 붙일 것
			문장 = 문장.Replace("-", "");
			문장 = 문장.Replace("_", "");

			// 여러 공백을 단일 공백으로 변경 및 루프 돌리기 위해 앞뒤 ‡ 추가
			문장 = Regex.Replace(문장, @"\s+", " ");
			문장 = "‡ " + 문장 + " ‡";

			// ********** 각 단어를 분리시켜 분석 시작			
			Regex numeric = new Regex("[0-9]");
			Regex alphabet = new Regex("[A-Z]");

			//independent
			List<string> independentWord = new List<string>();
			string[] word = 문장.Split(' ');

			for (int i = 0; i < word.Length; i++)
			{
				// 전단어, 이후단어 세팅
				string prevStr = "";
				string nextStr = "";

				if (i > 0)
					prevStr = word[i - 1];

				if (i < word.Length - 1)
					nextStr = word[i + 1];

				// 숫자가 하나도 없는 단어가 5글자 이상이면 무조건 탈락
				if (!numeric.IsMatch(word[i]) && word[i].Length >= 5)
				{
					word[i] = "‡";
				}
				

				// 영숫자 결합에 5글자 이상이면 PICK도 하고 단독 단어로 인정도 하고
				else if (numeric.IsMatch(word[i]) && alphabet.IsMatch(word[i]) && word[i].Length >= 5)
				{
					independentWord.Add(word[i]);
				}
				// 자신 또는 앞뒤단어에 숫자 포함되어 있으면 Pick
				else if (numeric.IsMatch(prevStr) || numeric.IsMatch(word[i]) || numeric.IsMatch(nextStr))
				{
					// CODE 060-3117 이런 케이스는 CODE0603117 되버리므로 끝에다가 별도 단독단어를 덛붙임
					if (numeric.IsMatch(word[i]) && word[i].Length >= 5)
						independentWord.Add(word[i]);
				}
				// 나머지는 탈락
				else
				{
					word[i] = "‡";
				}
			}

			// 줄어든 문장 완성
			string workedString = string.Join("", word);
			workedString = workedString.Replace("‡", " ");
			workedString = Regex.Replace(workedString, @"\s+", " ");

			// 마지막 과정 : 5글자 이상 되는 단어만 PICK
			string outString = "";

			foreach (string s in workedString.Split(' '))
			{
				if (s.Length >= 5)
				{
					outString += " " + s;
					independentWord.Remove(s);  // 독립단어 중 중복단어 제거
				}
			}

			outString += " " + string.Join(" ", independentWord);
			outString = outString.Trim().ToUpper();

			return outString;
		}

		public static bool 강조(string[] 키워드, ref string 주제, ref string 품목코드, ref string 품목명, ref string 재고코드)
		{
			bool 찾음 = false;
			string 구분자1 = "●●●";   // 주제 품목코드 등을 구별
			string 구분자2 = "■■■";   // 키워드의 @기능을 쓰기위해 구별
			string 태그열기 = "<▶▶▶>";
			string 태그닫기 = "<◀◀◀>";
			string 문자열 = " " + 주제 + " " + 구분자2 + 구분자1 + 구분자2 + " " + 품목코드 + " " + 구분자1 + " " + 품목명 + " " + 구분자1 + " " + 재고코드 + " ";

			// 합자 처리 (ff, fl, fi 등)
			문자열 = 문자열.합자일반화();

			// 강조 시작
			for (int i = 0; i < 키워드.Length; i++)
			{
				// SQL 패턴을 C# 패턴으로 변경
				string 패턴 = 키워드[i];

				// @ 특수처리
				if (패턴.StartsWith("@"))	패턴 = 패턴.Replace("@", 구분자2 + "%");
				if (패턴.EndsWith("@"))		패턴 = 패턴.Replace("@", "%" + 구분자2);

				// 와일드카드
				패턴 = 패턴.Replace("_", ".");
				패턴 = 패턴.Replace("%", ".*");

				// 패턴 검색
				string 일치 = Regex.Match(문자열, 패턴, RegexOptions.IgnoreCase | RegexOptions.Singleline).Value;
				string 새값 = 일치;

				// %(.*)가 있는 경우는 다시 쪼개서 replace 해줌
				foreach (string s in 패턴.Split(new[] { ".*" }, StringSplitOptions.None).Select(x => x.Trim()).Where(x => x != "" && x != 구분자2).ToArray())
				{
					bool 중복 = false;

					// 패턴이 키워드 뒤쪽에 반복될수 있어서 검사함
					for (int j = i + 1; j < 키워드.Length; j++)
					{
						if (Regex.Match(키워드[j], s, RegexOptions.IgnoreCase | RegexOptions.Singleline).Value != "")
						{
							중복 = true;
							break;
						}
					}

					if (!중복)
						새값 = Regex.Replace(새값, s, 태그열기 + "$0" + 태그닫기, RegexOptions.IgnoreCase | RegexOptions.Singleline);
				}

				if (일치 != "")
				{
					찾음 = true;
					문자열 = 문자열.Replace(일치, 새값);
				}
			}

			// 주제 구분자 삭제 후 주제, 품목코드 품목명 나누기
			문자열	= 문자열.Replace(구분자2, "").Replace(태그열기, "<font style='color:red; font-weight:bold;'>").Replace(태그닫기, "</font>");
			주제		= 문자열.Split(new[] { 구분자1 }, StringSplitOptions.None)[0].Trim();
			품목코드	= 문자열.Split(new[] { 구분자1 }, StringSplitOptions.None)[1].Trim();
			품목명	= 문자열.Split(new[] { 구분자1 }, StringSplitOptions.None)[2].Trim();
			재고코드	= 문자열.Split(new[] { 구분자1 }, StringSplitOptions.None)[3].Trim();

			return 찾음;
		}
	}
}


using Duzon.Common.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Dintec;
using Duzon.ERPU;

using System.Data;
using System.IO;
using System.Xml;

namespace DX
{
	public class 유틸
	{
		// ********** 메세지
		public static bool 메세지(string msg)
		{
			MsgControl.CloseMsg();
			DialogResult 결과 = Global.MainFrame.ShowMessage(msg);
			return 결과 == DialogResult.OK || 결과 == DialogResult.Yes;
		}

		public static bool 메세지(string msg, 메세지구분 구분)
		{
			MsgControl.CloseMsg();
			string buttonType = "QY1";
			if		(구분 == 메세지구분.일반알람) buttonType = "QY1";
			else if (구분 == 메세지구분.일반선택) buttonType = "QY2";
			else if (구분 == 메세지구분.경고알람) buttonType = "WK1";
			else if (구분 == 메세지구분.경고선택) buttonType = "WK2";

			DialogResult 결과 = Global.MainFrame.ShowMessage(msg, buttonType);
			return 결과.포함(DialogResult.OK, DialogResult.Yes);
		}

		public static bool 메세지(공통메세지 msg)
		{
			MsgControl.CloseMsg();
			DialogResult 결과 = Global.MainFrame.ShowMessage(msg);
			return 결과 == DialogResult.OK || 결과 == DialogResult.Yes;
		}

		public static bool 메세지(공통메세지 msg, params string[] values)
		{
			MsgControl.CloseMsg();
			DialogResult 결과 = Global.MainFrame.ShowMessage(msg, values);
			return 결과 == DialogResult.OK || 결과 == DialogResult.Yes;
		}

		public static void 메세지(Exception ex)
		{
			string msg = ex.Message;
			MsgControl.CloseMsg();

			if (msg.시작("**경고발생**"))
			{
				메세지(msg.Replace("**경고발생**", ""), 메세지구분.경고알람);
			}
			else
			{
				if (msg.Contains("SQL ERROR") && msg.NthIndexOf("\r\n", 2) > 0)
					msg = msg.Substring(0, msg.NthIndexOf("\r\n", 2));

				Global.MainFrame.MsgEnd(new Exception(msg));
			}
		}

		public static void 경고발생(string 메세지) => throw new Exception("**경고발생**" + 메세지);

		public static void 예외발생(string 메세지) => throw new Exception(메세지);

		public static void 경고(string 메세지) => throw new Exception("**경고발생**" + 메세지);

		public static void 예외(string 메세지) => throw new Exception(메세지);

		// ********** 프로그레스바
		public static void 작업중(string msg)
		{
			MsgControl.ShowMsg(msg);
			DBHelper.ExecuteNonQuery("", new object[] { }); // 여기에 즉시 띄우는 뭐가 있나봄
			Cursor.Current = Cursors.WaitCursor;
		}

		public static void 작업중()
		{
			MsgControl.CloseMsg();
		}



		public static string HighlightKeyword(string input, string keyword)
		{
			// SQL 패턴을 C# 패턴으로 변경
			string pattern = keyword;
			pattern = pattern.Replace("_", ".");
			pattern = pattern.Replace("%", ".*");

			// 패턴 검색
			string finded = Regex.Match(" " + input + " ", pattern, RegexOptions.IgnoreCase).Value.Trim();

			if (finded != "")
			{
				// % 들어가는건 쪽개서 강조 해줌
				if (keyword.Contains("%"))
				{
					string newVal = finded;

					foreach (string s in keyword.Split('%'))
						newVal = newVal.Replace(s, "<span style='color:red; font-weight:bold;'>" + s + "</span>");

					input = input.Replace(finded, newVal);
				}
				else
				{
					input = input.Replace(finded, "<span style='color:red; font-weight:bold;'>" + finded + "</span>");
				}
			}

			return input;
		}



		/// <summary>
		/// 키워드 강조
		/// </summary>
		/// <param name="키워드">최대 4개까지의 매칭된 키워드</param>
		public static void 키워드강조(string[] 키워드, ref string 주제, ref string 품목코드, ref string 품목명, ref string 재고코드)
		{
			string splitter1 = "●●●●●";   // 주제 품목코드 등을 구별
			string splitter2 = "■■■■■";   // 키워드의 @기능을 쓰기위해 구별
			string tagOpen  = "<▶▶▶▶▶>";
			string tagClose = "<◀◀◀◀◀>";
			string 문자열 = " " + 주제 + " " + splitter2 + splitter1 + splitter2 + " " + 품목코드 + " " + splitter1 + " " + 품목명 + " " + splitter1 + " " + 재고코드 + " ";

			// 합저 처리 (ff, fl, fi 등)
			문자열 = 문자열.합자일반화();

			// 강조 시작
			for (int i = 0; i < 키워드.Length; i++)
			{
				// SQL 패턴을 C# 패턴으로 변경
				string 패턴 = 키워드[i];

				// @ 특수처리
				if (패턴.StartsWith("@")) 패턴 = 패턴.Replace("@", splitter2 + "%");
				if (패턴.EndsWith("@"))   패턴 = 패턴.Replace("@", "%" + splitter2);

				// 와일드카드
				패턴 = 패턴.Replace("_", ".");
				패턴 = 패턴.Replace("%", ".*");

				// 패턴 검색
				string 일치 = Regex.Match(문자열, 패턴, RegexOptions.IgnoreCase | RegexOptions.Singleline).Value;
				string 새값 = 일치;

				// %(.*)가 있는 경우는 다시 쪼개서 replace 해줌
				foreach (string s in 패턴.Split(new[] { ".*" }, StringSplitOptions.None).Select(x => x.Trim()).Where(x => x != "" && x != splitter2).ToArray())
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
						새값 = Regex.Replace(새값, s, tagOpen + "$0" + tagClose, RegexOptions.IgnoreCase | RegexOptions.Singleline);
				}

				문자열 = 문자열.Replace(일치, 새값);
			}

			// 주제 구분자 삭제 후 주제, 품목코드 품목명 나누기
			문자열	= 문자열.Replace(splitter2, "").Replace(tagOpen, "<font style='color:red; font-weight:bold;'>").Replace(tagClose, "</font>");
			주제		= 문자열.Split(new[] { splitter1 }, StringSplitOptions.None)[0].Trim();
			품목코드	= 문자열.Split(new[] { splitter1 }, StringSplitOptions.None)[1].Trim();
			품목명	= 문자열.Split(new[] { splitter1 }, StringSplitOptions.None)[2].Trim();
			재고코드	= 문자열.Split(new[] { splitter1 }, StringSplitOptions.None)[3].Trim();
		}


		public static string 오늘()
		{
			return 오늘(0);
		}

		public static string 오늘(int 날짜더하기)
		{
			return string.Format("{0:yyyyMMdd}", Global.MainFrame.GetDateTimeToday().AddDays(날짜더하기));
		}



		public static string 핵심코드(string inString)
		{
			// 확실히 단어 사이에 구분져야 할 것.. 슬래쉬 요곤 좀 애매함.. 일단 고민해보자.. (구분져야 하는 CASE : DB18017984/12번)
			foreach (string f in new string[] { "\"", "|", "§", "'", ",", ".", ":", ";", "(", ")", "[", "]", "<", ">", "&", "/", "\r", "\n" })
				inString = inString.Replace(f, " ‡ ");

			// 주의단어
			inString = inString.Replace("NO-", "NO ");

			// 한 단어로 붙일 것
			inString = inString.Replace("-", "");
			inString = inString.Replace("_", "");

			// 여러 공백을 단일 공백으로 변경 및 루프 돌리기 위해 앞뒤 ‡ 추가
			inString = Regex.Replace(inString, @"\s+", " ");
			inString = "‡ " + inString + " ‡";

			// ********** 각 단어를 분리시켜 분석 시작			
			Regex numeric = new Regex("[0-9]");
			Regex alphabet = new Regex("[A-Z]");

			//independent
			List<string> independentWord = new List<string>();
			string[] word = inString.Split(' ');

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

		public static string[,] 클립보드()
		{
			string[,] result;

			if (Clipboard.ContainsData("XML Spreadsheet"))
			{
				object clipData = Clipboard.GetData("XML Spreadsheet");
				MemoryStream ms = clipData as MemoryStream;
				XmlDocument xml = new XmlDocument();
				xml.Load(ms);

				// Row 태그만 가져옴 (쓸데없는 것도 들어옴)
				XmlNode table = xml.GetElementsByTagName("Table")[0];
				int rowCount = table.Attributes["ss:ExpandedRowCount"].Value.정수();
				int colCount = table.Attributes["ss:ExpandedColumnCount"].Value.정수();
				result = new string[rowCount, colCount];

				// 부어넣기 시작
				int i = 0;

				foreach (XmlNode row in xml.GetElementsByTagName("Row"))
				{
					//속성 인덱스 체크
					if (row.Attributes["ss:Index"] != null)
						i = row.Attributes["ss:Index"].Value.정수() - 1;

					int j = 0;

					foreach (XmlNode cell in row.ChildNodes)
					{
						// 속성에 인덱스가 있는지 체크하여 있으면 j값을 치환함 (셀이 빈칸인 경우는 그 다음칸에 index가 있음)
						if (cell.Attributes["ss:Index"] != null)
							j = cell.Attributes["ss:Index"].Value.정수() - 1;

						//result[i, j] = cell.InnerText.Replace("\n", "\r\n");

						// cell안에 또 루프 돌아야함, Data 태그 값만 가져와야함, cell에 메모가 있을 경우 같이 가져오는 불상사가 생겨버림
						foreach (XmlNode node in cell.ChildNodes)
						{
							if (node.Name == "Data")
							{
								result[i, j] = node.InnerText.Replace("\n", "\r\n");
								break;
							}
						}

						j++;
					}

					i++;
				}
			}
			else
			{
				string[] rows = Regex.Split(Clipboard.GetText().TrimEnd(), "\r\n");
				string[] columns = Regex.Split(rows[0], "\t");
				result = new string[rows.Length, columns.Length];

				// 부어넣기 시작
				for (int i = 0; i < rows.Length; i++)
				{
					columns = Regex.Split(rows[i], "\t");

					for (int j = 0; j < columns.Length; j++)
						result[i, j] = columns[j];
				}
			}

			return result;
		}
	}
}

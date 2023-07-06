using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System.Data;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Net;

namespace Dintec
{
	public class UT
	{		
		// ********** 메세지
		public static DialogResult ShowMsg(string msg)
		{
			MsgControl.CloseMsg();
			if (msg != "")
				return Global.MainFrame.ShowMessage(msg);
			return DialogResult.OK;
		}

		public static DialogResult ShowMsg(string msg, string buttonType)
		{
			MsgControl.CloseMsg();
			return Global.MainFrame.ShowMessage(msg, buttonType);
		}

		public static DialogResult ShowMsg(공통메세지 msg)
		{
			MsgControl.CloseMsg();
			return Global.MainFrame.ShowMessage(msg);
		}

		public static DialogResult ShowMsg(공통메세지 msg, params string[] values)
		{
			MsgControl.CloseMsg();
			return Global.MainFrame.ShowMessage(msg, values);
		}

		public static void ShowMsg(Exception ex)
		{
			MsgControl.CloseMsg();
			Global.MainFrame.MsgEnd(ex);
		}		

		// ********** 프로그레스바
		public static void ShowPgb(string msg)
		{
			MsgControl.ShowMsg(msg);			
			DBHelper.ExecuteNonQuery("", new object[] { }); // 여기에 즉시 띄우는 뭐가 있나봄
			Cursor.Current = Cursors.WaitCursor;
		}

		public static void ClosePgb()
		{
			MsgControl.CloseMsg();
		}

		// ********** 날짜, 시간
		public static string Today()
		{
			return Today(0);
		}

		public static string Today(int addedDay)
		{
			return string.Format("{0:yyyyMMdd}", Global.MainFrame.GetDateTimeToday().AddDays(addedDay));
		}





		public static string GetDxDesc(string str)
		{
			return " " + Regex.Replace(str, @"\s+", " ").Trim().ToUpper() + " ";
		}




		public static void ExtractCode(DataTable dtDx)
		{
			dtDx.Columns.Add("ITEM_CODE");
			dtDx.Columns.Add("SUBJ_CODE");

			foreach (DataRow row in dtDx.Rows)
			{
				row["ITEM_CODE"] = UT.GetDxCoreCode(row["CD_ITEM_PARTNER"] + " ‡ " + row["NM_ITEM_PARTNER"]);
				row["SUBJ_CODE"] = UT.GetDxCoreCode(row["NM_SUBJECT"].ToString());
			}
		}


		public static string GetDxCoreCode(string inString)
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


		public static string GetDxDictByTrain(string inString, string filter)
        {
            // 선두 단어 가져오기
            string query = @"
SELECT
	NM_SYSDEF				AS [DICT]
,	ISNULL(CD_FLAG2, '')	AS [DICT2]
,	ISNULL(CD_FLAG3, '')	AS [DROP]
FROM V_CZ_MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = 'K100'
	AND CD_FIELD = 'CZ_DX00008'
	AND ISNULL(CD_FLAG1, '') LIKE '%" + filter + "%'";

            DataTable dtDict = DBMgr.GetDataTable(query);

            string outString = "";
            string replaceStr = Regex.Replace(inString, "[^a-zA-Z0-9-/.‡]+", " ");   // 삭제하지 말아야 할 단어들
            string[] words = replaceStr.Split(' ');

            for (int i = 0; i < words.Length - 1; i++)
            {				
				// 사전에 있는 단어 찾기 시작
				if (dtDict.Select("DICT = '" + words[i] + "'").GetFirstRow() is DataRow rowDict && rowDict != null)
				{										
					// 필수단어가 존재하게끔 설정되어 있다면 진짜 있는지 체크
					if ((string)rowDict["DICT2"] is string dict2 && dict2 != "")
					{
						if (dict2.IndexOf(words[i + 1]) >= 0)
							i++;		// 합격
						else
							continue;   // 불합격
					}						
					
					int nowIdx = i + 1; // 찾았으면 인덱스를 한칸 옮겨서 두번째 단어부터 가져오던 거르던 함
					int endIdx = i + 3; // 총 3개 단어 검색
					
					// 다음 인덱스로 옮긴다음 지정된 수만큼(endIdx) 단어 모으기 시작
					for (; nowIdx <= endIdx && nowIdx < words.Length; nowIdx++)
					{
						// 사전에 있는 단어를 만난다면 다시 상위 루프부터 시작
						if (dtDict.Select("DICT = '" + words[nowIdx] + "'").GetFirstRow() != null)
							break;
						else if (words[nowIdx] == "‡")										// ‡ 는 서브젝트 아이템 구분자 이므로 탐색 중단함
							break;
						else if (rowDict["DROP"].ToString().IndexOf(words[nowIdx]) >= 0)	// 제외 키워드에 있는 단어인지 검색하여 있으면 현재꺼는 제외 하고 한칸 더 +함
							endIdx++;
						else
							outString += " " + words[nowIdx];
					}

					// 바로 윗 루프에서 이미 i++ 되어 있으므로 이중증가를 막기위해 한번 줄여줌
					i = nowIdx - 1;
				}
			}

            return outString.Trim().ToUpper();
        }


		public static void SaveInqDX(string companyCode, string fileNumber)
		{
			string query = "SELECT * FROM CZ_SA_QTNL WHERE CD_COMPANY = '" + companyCode + "' AND NO_FILE = '" + fileNumber + "'";
			DataTable dt = SQL.GetDataTable(query);
			SaveInqDX(dt);
		}

		public static void SaveInqDX(DataTable itemTable)
		{
			if (itemTable == null) return;

			// 컬럼 및 데이터 생성
			if (!itemTable.Columns.Contains("ITEM_CODE")) itemTable.Columns.Add("ITEM_CODE");
			if (!itemTable.Columns.Contains("SUBJ_CODE")) itemTable.Columns.Add("SUBJ_CODE");

			foreach (DataRow row in itemTable.Rows)
			{
				if (row.RowState != DataRowState.Deleted)
				{
					row["ITEM_CODE"] = GetDxCoreCode(row["CD_ITEM_PARTNER"] + " ‡ " + row["NM_ITEM_PARTNER"]);
					row["SUBJ_CODE"] = GetDxCoreCode(row["NM_SUBJECT"].ToString());
				}
			}

			string lineXml = itemTable.ToXml("CD_COMPANY", "NO_FILE", "NO_LINE", "ITEM_CODE", "SUBJ_CODE");
			SQL.GetDataTable("PX_CZ_DX_INQ", SQLDebug.Print, lineXml);
		}
	}
}

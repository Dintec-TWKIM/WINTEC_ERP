
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


namespace DX
{
	public class UTIL
	{
		// ********** 메세지
		public static DialogResult 메세지(string msg)
		{
			MsgControl.CloseMsg();
			if (msg != "")
				return Global.MainFrame.ShowMessage(msg);
			return DialogResult.OK;
		}

		public static DialogResult 메세지(string msg, string buttonType)
		{
			MsgControl.CloseMsg();
			return Global.MainFrame.ShowMessage(msg, buttonType);
		}

		public static DialogResult 메세지(string msg, 메세지구분 구분)
		{
			MsgControl.CloseMsg();
			string buttonType = "QY1";
			if		(구분 == 메세지구분.일반알람) buttonType = "QY1";
			else if (구분 == 메세지구분.일반선택) buttonType = "QY2";
			else if (구분 == 메세지구분.경고알람) buttonType = "WK1";
			else if (구분 == 메세지구분.경고선택) buttonType = "WK2";			
			return Global.MainFrame.ShowMessage(msg, buttonType);
		}

		public static DialogResult 메세지(공통메세지 msg)
		{
			MsgControl.CloseMsg();
			return Global.MainFrame.ShowMessage(msg);
		}

		public static DialogResult 메세지(공통메세지 msg, params string[] values)
		{
			MsgControl.CloseMsg();
			return Global.MainFrame.ShowMessage(msg, values);
		}

		public static void 메세지(Exception ex)
		{
			string msg = ex.Message;

			if (msg.Contains("SQL ERROR") && msg.NthIndexOf("\r\n", 2) > 0)
				msg = msg.Substring(0, msg.NthIndexOf("\r\n", 2));

			Exception exNew = new Exception(msg);

			MsgControl.CloseMsg();
			Global.MainFrame.MsgEnd(exNew);
		}

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

		public static string 키워드강조(string 문자열, string 키워드)
		{
			// 앞뒤 빈칸을 넣어서 제외키워드 가능토록 함
			문자열 = " " + 문자열 + " ";

			// SQL 패턴을 C# 패턴으로 변경
			string pattern = 키워드;
			pattern = pattern.Replace("@", "%ㆍㆍㆍ%");	// @ 특수처리
			pattern = pattern.Replace("_", ".");
			pattern = pattern.Replace("%", ".*");

			// 패턴 검색
			string finded = Regex.Match(문자열, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline).Value;

			if (finded != "")
			{
				// % 들어가는건 쪽개서 강조 해줌
				if (키워드.Contains("%"))
				{
					string newVal = finded;

					foreach (string s in 키워드.Split('%').Select(x => x.Trim()).Where(x => x != "").ToArray())
					{
						newVal = Regex.Replace(newVal, s, "<font style='color:red; font-weight:bold;'>$0</font>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
					}

					문자열 = 문자열.Replace(finded, newVal);

					//newVal = Regex.Replace(newVal, 키워드, "<font style='color:red; font-weight:bold;'>$0</font>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
					//문자열 = 문자열.Replace(finded, newVal);

					//var keywords = 키워드.Split('%').Select(k => k.Trim()).Where(k => k != "").Select(k => Regex.Escape(k));
					//string a = @"\b(" + string.Join("|", keywords) + @")\b";
					//Regex exp = new Regex(a, RegexOptions.IgnoreCase | RegexOptions.Singleline);
					//문자열 = exp.Replace(문자열, "<font style='color:red; font-weight:bold;'>$0</font>");

				}
				else
				{
					문자열 = 문자열.Replace(finded, "<font style='color:red; font-weight:bold;'>" + finded + "</font>");
				}
			}

			return 문자열;
		}




		

		/// <summary>
		/// 키워드 강조
		/// </summary>
		/// <param name="키워드배열">최대 4개까지의 매칭된 키워드 배열</param>
		public static void 키워드강조(string[] 키워드배열, ref string 주제, ref string 품목코드, ref string 품목명, ref string 재고코드)
		{
			string splitter1 = "ｏｏｏｏｏ";   // 주제 품목코드 등을 구별
			string splitter2 = "ㆍㆍㆍㆍㆍ";   // 키워드의 @기능을 쓰기위해 구별
			string tagOpen  = "<▶▶▶▶▶>";
			string tagClose = "<◀◀◀◀◀>";
			string 문자열 = " " + 주제 + " " + splitter2 + splitter1 + splitter2 + " " + 품목코드 + " " + splitter1 + " " + 품목명 + " " + splitter1 + " " + 재고코드 + " ";

			// 합저 처리 (ff, fl, fi 등)
			문자열 = 문자열.합자일반화();

			//foreach (string 키워드 in 키워드배열)
			for (int i = 0; i < 키워드배열.Length; i++)
			{
				// SQL 패턴을 C# 패턴으로 변경
				//string 패턴 = 키워드;
				string 패턴 = 키워드배열[i];

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
					for (int j = i + 1; j < 키워드배열.Length; j++)
					{
						if (Regex.Match(키워드배열[j], s, RegexOptions.IgnoreCase | RegexOptions.Singleline).Value != "")
						{
							중복 = true;
							break;
						}
					}

					if (!중복)
					{
						// 뒷부분이 [^문자]로 끝나는 경우 새값에서 한글자 없애줌
						//if (s.종료("]"))
						//{
						//	if (s.Substring(s.LastIndexOf("[") + 1, 1) == "^")
						//		새값 = 새값.Substring(0, 새값.Length - 1);
						//}


						새값 = Regex.Replace(새값, s, tagOpen + "$0" + tagClose, RegexOptions.IgnoreCase | RegexOptions.Singleline);
					}
				}

				if (새값 != "")
					문자열 = 문자열.Replace(일치, 새값);
			}

			// 주제 구분자 삭제 후 주제, 품목코드 품목명 나누기
			문자열 = 문자열.Replace(splitter2, "").Replace(tagOpen, "<font style='color:red; font-weight:bold;'>").Replace(tagClose, "</font>");

			주제		= 문자열.Split(new[] { splitter1 }, StringSplitOptions.None)[0].Trim();
			품목코드 = 문자열.Split(new[] { splitter1 }, StringSplitOptions.None)[1].Trim();
			품목명	= 문자열.Split(new[] { splitter1 }, StringSplitOptions.None)[2].Trim();
			재고코드 = 문자열.Split(new[] { splitter1 }, StringSplitOptions.None)[3].Trim();
		}


		public static string 오늘()
		{
			return 오늘(0);
		}

		public static string 오늘(int 날짜더하기)
		{
			return string.Format("{0:yyyyMMdd}", Global.MainFrame.GetDateTimeToday().AddDays(날짜더하기));
		}


	}

	public enum 메세지구분
	{
		일반알람
	,	일반선택
	,	경고알람
	,	경고선택
	,	오류알람
	,	오류선택
	}
}

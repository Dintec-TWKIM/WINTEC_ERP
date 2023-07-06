using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DX
{
	public class 메시지
	{
		static bool 메시지코드_사용;
		static 메시지코드 메시지코드_코드;
		static string[] 메시지코드_값;

		static bool 공통메세지_사용;
		static 공통메세지 공통메세지_코드;
		static string[] 공통메세지_값;

		// ********** 프로그레스바
		public static bool 프로그레스바팝업;

		public static void 작업중(string msg)
		{
			프로그레스바팝업 = true;
			MsgControl.ShowMsg(msg);
			DBHelper.ExecuteNonQuery("", new object[] { }); // 여기에 즉시 띄우는 뭐가 있나봄
			Cursor.Current = Cursors.WaitCursor;
		}

		public static void 작업중()
		{
			프로그레스바팝업 = false;
			MsgControl.CloseMsg();
		}


		public static void 경고발생(string 메시지) => throw new Exception("**경고발생**" + 메시지);

		public static void 경고발생(메시지코드 메시지, params string[] values)
		{
			메시지코드_사용 = true;
			메시지코드_코드 = 메시지;
			메시지코드_값 = values;
			throw new Exception();
		}

		public static void 경고발생(공통메세지 메시지, params string[] values)
		{
			공통메세지_사용 = true;
			공통메세지_코드 = 메시지;
			공통메세지_값 = values;
			throw new Exception();
		}

		public static void 오류발생(string 메시지) => throw new Exception(메시지);

		public static void 오류알람(Exception ex)
		{
			if (프로그레스바팝업) 작업중();
			string msg = ex.Message;

			if (msg.시작("**경고발생**"))
			{
				경고알람(msg.Replace("**경고발생**", ""));
			}
			else if (메시지코드_사용)
			{
				경고알람(메시지코드_코드, 메시지코드_값);
				메시지코드_사용 = false;
			}
			else if (공통메세지_사용)
			{
				Global.MainFrame.ShowMessage(공통메세지_코드, 공통메세지_값);
				공통메세지_사용 = false;
			}
			else
			{
				if (msg.Contains("SQL ERROR") && msg.NthIndexOf("\r\n", 2) > 0)
					msg = msg.Substring(0, msg.NthIndexOf("\r\n", 2)).Replace("SQL ERROR : ", "");	// 자르는 순서 중요!

				Global.MainFrame.MsgEnd(new Exception(msg));
			}
		}

		public static void 일반알람(string 메시지)
		{
			if (프로그레스바팝업) 작업중();
			Global.MainFrame.ShowMessage(메시지, "QY1");
		}

		public static void 일반알람(공통메세지 메시지)
		{
			if (프로그레스바팝업) 작업중();
			Global.MainFrame.ShowMessage(메시지);
		}

		public static bool 일반선택(string 메시지)
		{
			if (프로그레스바팝업) 작업중();
			return Global.MainFrame.ShowMessage(메시지, "QY2").포함(DialogResult.OK, DialogResult.Yes);
		}

		public static bool 일반선택(공통메세지 메시지)
		{
			if (프로그레스바팝업) 작업중();
			return Global.MainFrame.ShowMessage(메시지).포함(DialogResult.OK, DialogResult.Yes);
		}


		public static void 저장완료() => 일반알람(공통메세지.자료가정상적으로저장되었습니다);

		public static void 삭제완료() => 일반알람(공통메세지.자료가정상적으로삭제되었습니다);








		public static void 경고알람(string 메시지)
		{
			if (프로그레스바팝업) 작업중();
			Global.MainFrame.ShowMessage(메시지, "WK1");
		}

		public static void 경고알람(메시지코드 메시지, params string[] values)
		{
			if (프로그레스바팝업) 작업중();
			string 시스템메세지 = "";

			if		(메시지 == 메시지코드.선택된자료가없습니다)			시스템메세지 = "선택된 자료가 없습니다.";
			else if (메시지 == 메시지코드._은는필수입력항목입니다)			시스템메세지 = "@ 은(는) 필수 입력 항목입니다.";
			else if (메시지 == 메시지코드._은는_보다크거나같아야합니다)	시스템메세지 = "@ 은(는) @ 보다 크거나 같아야 합니다.";
			else if (메시지 == 메시지코드._은는_보다작거나같아야합니다)	시스템메세지 = "@ 은(는) @ 보다 작거나 같아야 합니다.";

			// DD처리
			for (int i = 0; i < values.Length; i++)
			{
				values[i] = Global.MainFrame.DD(values[i]);
			}

			// 출력
			Global.MainFrame.ShowMessage(시스템메세지, values, "WK1");
		}

		public static bool 경고선택(string 메시지)
		{
			if (프로그레스바팝업) 작업중();
			return Global.MainFrame.ShowMessage(메시지, "WK2").포함(DialogResult.OK, DialogResult.Yes);
		}
	}

	public enum 메시지코드
	{
		선택된자료가없습니다	
	,	_은는필수입력항목입니다
	,	_은는_보다크거나같아야합니다
	,	_은는_보다작거나같아야합니다
	}
}

//선택된 자료가 없습니다.
//@ 은(는) 필수 입력 항목입니다.


//Global.MainFrame.ShowMessage("CZ_@ 은(는) @ 보다 크거나 같아야 합니다.", new object[] { Global.MainFrame.DD(a), Global.MainFrame.DD(b) });
			
//				Global.MainFrame.ShowMessage("CZ_@ 은(는) @ 보다 작거나 같아야 합니다.", new object[] { Global.MainFrame.DD(a), Global.MainFrame.DD(b) });
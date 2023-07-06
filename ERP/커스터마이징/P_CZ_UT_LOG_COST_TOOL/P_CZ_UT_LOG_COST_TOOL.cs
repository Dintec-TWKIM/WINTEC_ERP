using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;
using Duzon.Windows.Print;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;

namespace cz
{
	public partial class P_CZ_UT_LOG_COST_TOOL : PageBase
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public string CD_PC { get; set; }

		public string CD_DEPT { get; set; }

		public string NO_EMP { get; set; }

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_UT_LOG_COST_TOOL()
		{
			StartUp.Certify(this);
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			CD_PC = Global.MainFrame.LoginInfo.CdPc;
			CD_DEPT = Global.MainFrame.LoginInfo.DeptCode;
			NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{

		}

		private void InitGrid()
		{
			flexL.BeginSetting(2, 1, false);

			flexL.SetCol("NO_IO"		, "출하번호"		, 120, true);
			
			flexL.SetCol("ROW_ID"		, "전표번호"		, 100, false);
			flexL.SetCol("ROW_NO"		, "라인번호"		, 70, false);
			flexL.SetCol("NO_TAX"		, "세금계산서번호", 100, false);
			flexL.SetCol("CD_PC"		, "회계단위"		, 70);
			flexL.SetCol("CD_WDEPT"		, "작성부서"		, 70);
			flexL.SetCol("NO_DOCU"		, "전표번호"		, 100);
			flexL.SetCol("NO_DOLINE"	, "라인번호"		, 70);
			flexL.SetCol("CD_COMPANY"	, "회사코드"		, 70);
			flexL.SetCol("ID_WRITE"		, "작성사원"		, 70);
			flexL.SetCol("CD_DOCU"		, "전표유형"		, 70);
			flexL.SetCol("DT_ACCT"		, "회계일자"		, 70);
			flexL.SetCol("ST_DOCU"		, "승인여부"		, 70);
			flexL.SetCol("TP_DRCR"		, "차대구분"		, 70);
			flexL.SetCol("CD_ACCT"		, "계정코드"		, 70);
			flexL.SetCol("AMT"			, "금액"			, 110);
			flexL.SetCol("CD_PARTNER"	, "거래처코드"	, 70);
			flexL.SetCol("DT_START"		, "발행일"		, 70);
			flexL.SetCol("CD_DEPT"		, "부서"			, 70);
			flexL.SetCol("CD_CC"		, "코스트센터"	, 70);
			flexL.SetCol("CD_BUDGET"	, "예산단위"		, 70);
			flexL.SetCol("TP_DOCU"		, "전표처리결과"	, 90);
			flexL.SetCol("NO_ACCT"		, "회계승인번호"	, 90);
			flexL.SetCol("TP_GUBUN"		, "전표구분"		, 70);
			flexL.SetCol("CD_MNGD1"		, "내역코드"		, 70);
			flexL.SetCol("NM_MNGD1"		, "내역명"		, 70);
			flexL.SetCol("CD_MNGD2"		, "내역코드"		, 70);
			flexL.SetCol("NM_MNGD2"		, "내역명"		, 70);
			flexL.SetCol("CD_MNGD3"		, "내역코드"		, 70);
			flexL.SetCol("NM_MNGD3"		, "내역명"		, 70);

	
			// 관리항목 헤더
			flexL[0, flexL.Cols["CD_MNGD1"].Index] = "관리항목1";
			flexL[0, flexL.Cols["NM_MNGD1"].Index] = "관리항목1";
			flexL[0, flexL.Cols["CD_MNGD2"].Index] = "관리항목2";
			flexL[0, flexL.Cols["NM_MNGD2"].Index] = "관리항목2";
			flexL[0, flexL.Cols["CD_MNGD3"].Index] = "관리항목3";
			flexL[0, flexL.Cols["NM_MNGD3"].Index] = "관리항목3";


			flexL.KeyActionEnter = KeyActionEnum.MoveDown;
			flexL.SettingVersion = "16.02.12.06";
			flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			flexL.KeyDown += new KeyEventHandler(flexL_KeyDown);
			flexL.ValidateEdit += new ValidateEditEventHandler(flexL_ValidateEdit);
		}

		protected override void InitPaint()
		{
			flexL.Rows.Add();
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void flexL_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == (Keys.Control | Keys.V))
			{
				string[,] clipboard = Util.GetClipboardValues();

				for (int i = 0; i < clipboard.GetLength(0); i++)
				{
					int row = flexL.Row + i;
					string val = clipboard[i, 0];

					if (i == clipboard.GetLength(0) - 1 && val == "") break;
					if (row == flexL.Rows.Count) flexL.Rows.Add();

					flexL[row, flexL.Col] = val;


					DataSet ds = DBMgr.GetDataSet("SP_CZ_UT_LOG_COST_TOOL_SELECT", new object[] { CD_COMPANY, val });

					//DataTable dt = DBMgr.GetDataTable(query);

					string NO_DOCU = ds.Tables[0].Rows[0]["NO_DOCU"].ToString();
					string NO_SO = "";
					string CD_CC = "";
					

					if (ds.Tables[1].Rows.Count == 1)
					{
						NO_SO = ds.Tables[1].Rows[0]["NO_SO"].ToString();
						CD_CC = ds.Tables[1].Rows[0]["CD_CC"].ToString();
					}


					//flexL[row, "NO_SO"] = dt.Rows[0]["NO_SO"];
					flexL[row, "ROW_ID"]	 = NO_DOCU;		//"전표번호"		
					flexL[row, "ROW_NO"]	 = i + 1;		// 라인번호
					flexL[row, "NO_TAX"]	 = "*";			// 세금계산서번호
					flexL[row, "CD_PC"]		 = CD_PC;		// 회계단위
					flexL[row, "CD_WDEPT"]	 = CD_DEPT;		// 작성부서
					flexL[row, "NO_DOCU"]	 = NO_DOCU;		// 전표번호
					flexL[row, "NO_DOLINE"]	 =  i + 1;		// 라인번호
					flexL[row, "CD_COMPANY"] = CD_COMPANY;	// 회사코드
					flexL[row, "ID_WRITE"]	 = NO_EMP;		// 작성사원
					flexL[row, "CD_DOCU"]	 = "11";		// 전표유형
					flexL[row, "DT_ACCT"]	 = "";			// 회계일자
					flexL[row, "ST_DOCU"]	 = "1";			// 승인여부
					flexL[row, "TP_DRCR"]	 = "1";			// 차대구분
					flexL[row, "CD_ACCT"]	 = "58700";		// 계정코드
					flexL[row, "AMT"]		 = "";			// 금액
					flexL[row, "CD_PARTNER"] = "";			// 거래처코드
					flexL[row, "DT_START"]	 = "";			// 발행일	
					flexL[row, "CD_DEPT"]	 =  "";			// 부서		
					flexL[row, "CD_CC"]		 = CD_CC; 		// 코스트센터
					flexL[row, "CD_BUDGET"]	 = CD_CC;		// 예산단위
					flexL[row, "TP_DOCU"]	 = "";			// 전표처리결과
					flexL[row, "NO_ACCT"]	 = "";			// 회계승인번호
					flexL[row, "TP_GUBUN"]	 = "";			// 전표구분
					flexL[row, "CD_MNGD1"]	 = "";			// 내역코드	(코스트센타)
					flexL[row, "NM_MNGD1"]	 = "";			// 내역명	
					flexL[row, "CD_MNGD2"]	 = "";			// 내역코드	(사원)
					flexL[row, "NM_MNGD2"]	 = "";			// 내역명
					flexL[row, "CD_MNGD3"]	 = NO_SO;		// 내역코드	(프로젝트)
					flexL[row, "NM_MNGD3"]	 = NO_SO;		// 내역명		
				}
			}
		}

		private void flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			string COLNAME = flexL.Cols[e.Col].Name;

			if (COLNAME == "CD_SPEC")
			{

			}
		}

		#endregion
	}
}

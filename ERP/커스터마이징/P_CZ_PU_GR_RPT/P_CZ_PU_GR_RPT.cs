using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using DzHelpFormLib;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Duzon.Common.Util;
using Dintec;

namespace cz
{
	public partial class P_CZ_PU_GR_RPT : PageBase
	{
		string companyCode;

		#region ==================================================================================================== Constructor

		public P_CZ_PU_GR_RPT()
		{
			InitializeComponent();
			companyCode = Global.MainFrame.LoginInfo.CompanyCode;
			StartUp.Certify(this);
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
			//ctx회사코드.CodeValue = Global.MainFrame.LoginInfo.CompanyCode;
			//ctx회사코드.CodeName = Global.MainFrame.LoginInfo.CompanyName;

			//dtp일자.StartDateToString = Util.GetToday(-30);
			//dtp일자.EndDateToString = Util.GetToday();

			//Util.SetDB_CODE(cbo파일, "TP_FILE", true);
			//Util.SetDB_CODE(cbo거래처그룹, "MA_B000065", true);

			//// TEST
			//((DataTable)cbo파일.DataSource).Rows.Add("DY", "DY");


			//// 검색콤보
			//DataTable dt = new DataTable();
			//dt.Columns.Add("TYPE");
			//dt.Columns.Add("CODE");
			//dt.Columns.Add("NAME");			

			//dt.Rows.Add("차수", "ASC", "최초");
			//dt.Rows.Add("차수", "DESC", "최종");

			//dt.Rows.Add("DATE", "DT_INQ", "접수일자");
			//dt.Rows.Add("DATE", "DT_QTN", "견적일자");
			//dt.Rows.Add("DATE", "DT_SO" , "수주일자");
			//dt.Rows.Add("DATE", "DT_IV" , "매출일자");

			//dt.Rows.Add("금액", "", "금액검색");
			//dt.Rows.Add("금액", "", "--------");
			//dt.Rows.Add("금액", "P", "매입금액");
			//dt.Rows.Add("금액", "S", "매출금액");
			//dt.Rows.Add("금액", "M", "이윤");

			//dt.Rows.Add("통화", "EX", "외화");
			//dt.Rows.Add("통화", "KR", "원화");

			//dt.Rows.Add("KEYWORD", "NM_SUBJECT"		  , "주제");
			//dt.Rows.Add("KEYWORD", "CD_ITEM_PARTNER"  , "품목코드");
			//dt.Rows.Add("KEYWORD", "NM_ITEM_PARTNER"  , "품목명");
			//dt.Rows.Add("KEYWORD", "CD_UNIQUE_PARTNER", "선사코드");
			//dt.Rows.Add("KEYWORD", "CD_ITEM"		  , "재고코드");

			//dt.Rows.Add("SMART", "1", "일치");	// EQUAL
			//dt.Rows.Add("SMART", "2", "포함");	// LIKE
			//dt.Rows.Add("SMART", "3", "스마트");	// SMART

			//cbo차수.DataSource = dt.Select("TYPE = '차수'").CopyToDataTable();
			//cbo차수.SelectedIndex = 1;

			//cbo일자.DataSource = dt.Select("TYPE = 'DATE'").CopyToDataTable();
			//cbo일자.SelectedIndex = 0;

			//cbo금액.DataSource = dt.Select("TYPE = '금액'").CopyToDataTable();
			//cbo금액.SelectedIndex = 0;

			//cbo통화.DataSource = dt.Select("TYPE = '통화'").CopyToDataTable();
			//cbo통화.SelectedIndex = 0;

			//cbo키워드1.DataSource = dt.Select("TYPE = 'KEYWORD'").CopyToDataTable();
			//cbo키워드1.SetValue("NM_SUBJECT");

			//cbo키워드2.DataSource = dt.Select("TYPE = 'KEYWORD'").CopyToDataTable();
			//cbo키워드2.SetValue("CD_ITEM_PARTNER");

			//cbo키워드3.DataSource = dt.Select("TYPE = 'KEYWORD'").CopyToDataTable();
			//cbo키워드3.SetValue("NM_ITEM_PARTNER");

			//cbo스마트1.DataSource = dt.Select("TYPE = 'SMART'").CopyToDataTable();
			//cbo스마트1.SetValue("2");

			//cbo스마트2.DataSource = dt.Select("TYPE = 'SMART'").CopyToDataTable();
			//cbo스마트2.SetValue("2");

			//cbo스마트3.DataSource = dt.Select("TYPE = 'SMART'").CopyToDataTable();
			//cbo스마트3.SetValue("2");

			//flex견적파일H.DetailGrids = new FlexGrid[] { flex견적파일L };
			//flex수주파일H.DetailGrids = new FlexGrid[] { flex수주파일L };
			//flex매출파일H.DetailGrids = new FlexGrid[] { flex매출파일L };

			//// 버튼별 정책
			//Duzon.ERPU.Grant.UGrant ugrant = new Duzon.ERPU.Grant.UGrant();
			//ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "POLICY", btn최소마진예외);
			//ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "POLICY", btn최소마진적용);
			//ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "ZEROMARGIN", btnApplyZeroMargin);
			//ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "ZEROMARGIN", btnExceptZeroMargin);
		}

		private void InitGrid()
		{
			// ==================================================================================================== 발주번호별
			grdFileHead.BeginSetting(1, 1, false);

			grdFileHead.SetCol("NO_PO"		, "발주번호"		, 90);
			grdFileHead.SetCol("LN_PARTNER"	, "매입처코드"	, false);
			grdFileHead.SetCol("LN_PARTNER"	, "매입처"		, 200);
			grdFileHead.SetCol("NO_ORDER"	, "공사번호"		, 110);
			grdFileHead.SetCol("NO_EMP"		, "담당자사번"	, false);
			grdFileHead.SetCol("NM_EMP"		, "담당자"		, 70);
			grdFileHead.SetCol("CD_PURGRP"	, "구매그룹코드"	, false);
			grdFileHead.SetCol("NM_PURGRP"	, "구매그룹"		, 90);
			grdFileHead.SetCol("CD_CC"		, "C/C코드"		, false);
			grdFileHead.SetCol("NM_CC"		, "C/C명"		, 90);
			grdFileHead.SetCol("DT_PO"		, "발주일자"		, 80	, false	, typeof(string)	, FormatTpType.YEAR_MONTH_DAY);
			grdFileHead.SetCol("DT_GR"		, "입고일자"		, 80	, false	, typeof(string)	, FormatTpType.YEAR_MONTH_DAY);
			grdFileHead.SetCol("DT_IV"		, "정산일자"		, 80	, false	, typeof(string)	, FormatTpType.YEAR_MONTH_DAY);
			grdFileHead.SetCol("QT_PO"		, "발주"			, 60	, false	, typeof(decimal)	, FormatTpType.FOREIGN_MONEY);
			grdFileHead.SetCol("QT_GR"		, "입고"			, 60	, false	, typeof(decimal)	, FormatTpType.FOREIGN_MONEY);
			grdFileHead.SetCol("QT_IV"		, "정산"			, 60	, false	, typeof(decimal)	, FormatTpType.FOREIGN_MONEY);
			grdFileHead.SetCol("QT_RT"		, "반품"			, 60	, false	, typeof(decimal)	, FormatTpType.FOREIGN_MONEY);
			grdFileHead.SetCol("CD_EXCH"	, "통화코드"		, false);
			grdFileHead.SetCol("NM_EXCH"	, "통화명"		, 45);
			grdFileHead.SetCol("RT_EXCH"	, "환율"			, false);
			grdFileHead.SetCol("AM_EX"		, "입고금액"		, 110	, false	, typeof(decimal)	, FormatTpType.FOREIGN_MONEY);
			grdFileHead.SetCol("AM"			, "입고금액(￦)"	, 110	, false	, typeof(decimal)	, FormatTpType.MONEY);
			grdFileHead.SetCol("AM_EX"		, "정산금액"		, 110	, false	, typeof(decimal)	, FormatTpType.FOREIGN_MONEY);
			grdFileHead.SetCol("AM"			, "정산금액(￦)"	, 110	, false	, typeof(decimal)	, FormatTpType.MONEY);
			//grdFileHead.SetCol("LT", "납기", 50, false, typeof(decimal), FormatTpType.MONEY);
			grdFileHead.SetCol("CD_BUYER"	, "매출처코드"	, false);
			grdFileHead.SetCol("LN_BUYER"	, "매출처"		, 200);

			grdFileHead.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			grdFileHead.Cols["NM_PURGRP"].TextAlign = TextAlignEnum.CenterCenter;
			grdFileHead.Cols["NM_CC"].TextAlign = TextAlignEnum.CenterCenter;
			grdFileHead.Cols["NM_EXCH"].TextAlign = TextAlignEnum.CenterCenter;
			grdFileHead.Cols["NM_BUYER_GRP"].TextAlign = TextAlignEnum.CenterCenter;


			//InitGridHead(flex견적파일H);
			//InitGridHead(flex수주파일H);
			//InitGridHead(flex매출파일H);

			//// ================================================== 파일 L
			

			//// ================================================== 품목 L

			//InitGridLine(flex견적파일L);
			//InitGridLine(flex견적품목L);
			//InitGridLine(flex수주파일L);
			//InitGridLine(flex수주품목L);
			//InitGridLine(flex매출파일L);
			//InitGridLine(flex매출품목L);

			//flex견적파일L.Cols["NO_FILE"].Visible = false;
			//flex견적파일L.Cols["NO_HST"].Visible = false;
			//flex견적파일L.Cols["LN_PARTNER"].Visible = false;
			//flex견적파일L.Cols["NM_VESSEL"].Visible = false;
			//flex견적파일L.Cols["NM_EXCH"].Visible = false;
			//flex견적파일L.Cols["RT_EXCH"].Visible = false;

			//flex수주파일L.Cols["NO_FILE"].Visible = false;
			//flex수주파일L.Cols["NO_HST"].Visible = false;
			//flex수주파일L.Cols["LN_PARTNER"].Visible = false;
			//flex수주파일L.Cols["NM_VESSEL"].Visible = false;
			//flex수주파일L.Cols["NM_EXCH"].Visible = false;
			//flex수주파일L.Cols["RT_EXCH"].Visible = false;

			//flex매출파일L.Cols["NO_FILE"].Visible = false;
			//flex매출파일L.Cols["NO_HST"].Visible = false;
			//flex매출파일L.Cols["LN_PARTNER"].Visible = false;
			//flex매출파일L.Cols["NM_VESSEL"].Visible = false;
			//flex매출파일L.Cols["NM_EXCH"].Visible = false;
			//flex매출파일L.Cols["RT_EXCH"].Visible = false;
		}

		private void InitGridHead(FlexGrid grid)
		{
			grid.BeginSetting(1, 1, false);
			
			grid.SetCol("NO_FILE"	, "파일번호"		, 80);
			grid.SetCol("NO_HST"	, "*"			, 20);
			grid.SetCol("CD_PARTNER", "매출처코드"	, false);
			grid.SetCol("LN_PARTNER", "매출처"		, 180);
			grid.SetCol("NO_IMO"	, "IMO"			, false);
			grid.SetCol("NM_VESSEL"	, "선명"			, 150);
			grid.SetCol("NO_REF"	, "문의번호"		, 110);		// 탭에 따라 이름변경
			grid.SetCol("DT_INQ"	, "접수일자"		, 80	, false	, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grid.SetCol("DT_QTN"	, "견적일자"		, 80	, false	, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grid.SetCol("DT_SO"		, "수주일자"		, 80	, false	, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grid.SetCol("DT_IV"		, "매출일자"		, 80	, false	, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grid.SetCol("NM_EMP"	, "담당자"		, 70);
			grid.SetCol("CD_EXCH"	, "통화"			, 45);
			grid.SetCol("RT_EXCH"	, "환율"			, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("AM_EX_P"	, "매입금액"		, 105	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("AM_KR_P"	, "매입금액(￦)"	, 105	, false	, typeof(decimal), FormatTpType.MONEY);
			grid.SetCol("AM_EX_S"	, "매출금액"		, 105	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("AM_KR_S"	, "매출금액(￦)"	, 105	, false	, typeof(decimal), FormatTpType.MONEY);
			grid.SetCol("RT_MARGIN"	, "이윤율"		, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("AM_MARGIN"	, "이윤(￦)"		, 105	, false	, typeof(decimal), FormatTpType.MONEY);
			
			grid.Cols["NO_HST"].TextAlign = TextAlignEnum.CenterCenter;
			grid.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			grid.Cols["CD_EXCH"].TextAlign = TextAlignEnum.CenterCenter;
			grid.SetDataMap("CD_EXCH", GetDb.Code("MA_B000005"), "CODE", "NAME");
			
		    grid.SettingVersion = "17.04.19.02";
		    grid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			//grid.SetExceptSumCol("RT_EXCH", "AM_EX_P", "AM_EX_S", "RT_MARGIN");
			grid.SetExceptSumCol("RT_EXCH", "RT_MARGIN");
			grid.Rows[0].Height = 28;
		}

		private void InitGridLine(FlexGrid grid)
		{
		    grid.BeginSetting(1, 1, false);
			
			grid.SetCol("NO_FILE"			, "파일번호"			, 80);
			grid.SetCol("NO_HST"			, "*"				, 20);
			grid.SetCol("CD_PARTNER"		, "매출처코드"		, false);
			grid.SetCol("LN_PARTNER"		, "매출처"			, 150);
			grid.SetCol("NO_IMO"			, "IMO"				, false);
			grid.SetCol("NO_HULL"			, "호선번호"			, false);
			grid.SetCol("NM_VESSEL"			, "선명"				, 100);
			grid.SetCol("NM_EXCH"			, "통화"				, 45);
			grid.SetCol("RT_EXCH"			, "환율"				, 60	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
		    grid.SetCol("NO_LINE"			, "항번"				, false);
		    grid.SetCol("NO_DSP"			, "순번"				, 40);
		    grid.SetCol("NM_SUBJECT"		, "주제"				, 200);
		    grid.SetCol("CD_ITEM_PARTNER"	, "품목코드"			, 120);
		    grid.SetCol("NM_ITEM_PARTNER"	, "품목명"			, 230);
			grid.SetCol("CD_UNIQUE_PARTNER", "선사코드"			, 80);
			grid.SetCol("CD_ITEM"			, "재고코드"			, 100);
			grid.SetCol("GRP_ITEM"			, "유형"				, 100);
			grid.SetCol("DC_RMK"			, "비고"				, false);
			grid.SetCol("CD_SUPPLIER"		, "매입처코드"		, false);
		    grid.SetCol("LN_SUPPLIER"		, "매입처"			, 150);
		    grid.SetCol("UNIT"				, "단위"				, 45);
		    grid.SetCol("QT"				, "수량"				, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);

			grid.SetCol("UM_EX_STD_P"		, "매입기준단가"		, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("AM_EX_STD_P"		, "매입기준금액"		, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			grid.SetCol("RT_DC_P"			, "매입DC(%)"		, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
		    grid.SetCol("UM_EX_P"			, "매입단가"			, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
		    grid.SetCol("AM_EX_P"			, "매입금액"			, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
		    grid.SetCol("UM_KR_P"			, "매입단가(￦)"		, 110	, false	, typeof(decimal), FormatTpType.MONEY);
		    grid.SetCol("AM_KR_P"			, "매입금액(￦)"		, 110	, false	, typeof(decimal), FormatTpType.MONEY);

		    grid.SetCol("RT_PROFIT"			, "이윤(%)"			, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("UM_EX_Q"			, "매출견적단가"		, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
		    grid.SetCol("AM_EX_Q"			, "매출견적금액"		, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
		    grid.SetCol("UM_KR_Q"			, "매출견적단가(￦)"	, 110	, false	, typeof(decimal), FormatTpType.MONEY);
		    grid.SetCol("AM_KR_Q"			, "매출견적금액(￦)"	, 110	, false	, typeof(decimal), FormatTpType.MONEY);

		    grid.SetCol("RT_DC"				, "DC(%)"			, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
		    grid.SetCol("UM_EX_S"			, "매출단가"			, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
		    grid.SetCol("AM_EX_S"			, "매출금액"			, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
		    grid.SetCol("UM_KR_S"			, "매출단가(￦)"		, 110	, false	, typeof(decimal), FormatTpType.MONEY);
		    grid.SetCol("AM_KR_S"			, "매출금액(￦)"		, 110	, false	, typeof(decimal), FormatTpType.MONEY);
	
		    grid.SetCol("RT_MARGIN"			, "최종(%)"			, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("AM_MARGIN"			, "최종(￦)"			, 110	, false	, typeof(decimal), FormatTpType.MONEY);
		    grid.SetCol("LT"				, "납기"				, 50	, false	, typeof(decimal), FormatTpType.MONEY);

			grid.Cols["NO_HST"].TextAlign = TextAlignEnum.CenterCenter;
			grid.Cols["NO_DSP"].Format = "####.##";
		    grid.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter; 
			grid.Cols["NM_EXCH"].TextAlign = TextAlignEnum.CenterCenter;
			grid.Cols["UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			grid.SetDataMap("UNIT", GetDb.Code("MA_B000004"), "CODE", "NAME");

		    grid.SettingVersion = "17.04.19.02";
		    grid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
			grid.SetExceptSumCol("RT_EXCH", "UM_EX_STD_P", "RT_DC_P", "UM_EX_P", "UM_KR_P", "RT_PROFIT", "UM_EX_Q", "UM_KR_Q", "RT_DC", "UM_EX_S", "UM_KR_S", "RT_MARGIN", "LT");
			//grid.SetExceptSumCol("RT_EXCH", "UM_EX_STD_P", "RT_DC_P", "RT_PROFIT", "RT_DC", "RT_MARGIN", "LT");
			grid.Rows[0].Height = 28;

			// 매입견적단가는 기본값으로 보이지 않도록 하자 (너무 많이 나오니 헛갈림)
			//grid.Cols["UM_EX_STD_P"].Visible = false;
			//grid.Cols["AM_EX_STD_P"].Visible = false;
			//grid.Cols["RT_DC_P"].Visible = false;

			grid.Styles.Add("CHILD_ROW").ForeColor = Color.Gray;

			// 테스트용
			//grid.Cols["NM_SUBJECT"].Visible = false;
			//grid.Cols["NM_ITEM_PARTNER"].Visible = false;
			//grid.Cols["CD_ITEM"].Visible = false;
			//grid.Cols["CD_UNIQUE_PARTNER"].Visible = false;
			//grid.Cols["GRP_ITEM"].Visible = false;

			//// 테스트용2
			//grid.Cols["UM_EX_P"].Visible = false;
			//grid.Cols["UM_KR_P"].Visible = false;
			//grid.Cols["UM_EX_Q"].Visible = false;
			//grid.Cols["AM_EX_Q"].Visible = false;
			//grid.Cols["UM_KR_Q"].Visible = false;
			//grid.Cols["AM_KR_Q"].Visible = false;
			//grid.Cols["UM_EX_S"].Visible = false;
			//grid.Cols["UM_KR_S"].Visible = false;
		}

		private void InitEvent()
		{
			//btn최소마진예외.Click += new EventHandler(btn최소마진예외_Click);
			//btn최소마진적용.Click += new EventHandler(btn최소마진적용_Click);

			//btnExceptZeroMargin.Click += new EventHandler(btnExceptZeroMargin_Click);
			//btnApplyZeroMargin.Click += new EventHandler(btnApplyZeroMargin_Click);

			//tab.SelectedIndexChanged += new EventHandler(tab_SelectedIndexChanged);
			//flex견적파일H.AfterRowChange += new RangeEventHandler(flex견적파일H_AfterRowChange);
			//flex수주파일H.AfterRowChange += new RangeEventHandler(flex수주파일H_AfterRowChange);
			//flex매출파일H.AfterRowChange += new RangeEventHandler(flex매출파일H_AfterRowChange);
		}

		

		protected override void InitPaint()
		{
			cbo차수.Enabled = false;
			//spl거래처별.SplitterDistance = 292;
			//spl매입처별.SplitterDistance = 292;
			//spl담당자별.SplitterDistance = 243;
			//spl견적상태별.SplitterDistance = 243;
		}

		#endregion
	}
}

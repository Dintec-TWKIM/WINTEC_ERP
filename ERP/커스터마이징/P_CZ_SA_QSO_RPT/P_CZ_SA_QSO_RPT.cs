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
using DX;

namespace cz
{
	public partial class P_CZ_SA_QSO_RPT : PageBase
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_SA_QSO_RPT()
		{
			StartUp.Certify(this);
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
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
			ctx회사코드.CodeValue = Global.MainFrame.LoginInfo.CompanyCode;
			ctx회사코드.CodeName = Global.MainFrame.LoginInfo.CompanyName;

			dtp일자.StartDateToString = Util.GetToday(-30);
			dtp일자.EndDateToString = Util.GetToday();

			Util.SetDB_CODE(cbo파일, "TP_FILE", true);
			Util.SetDB_CODE(cbo거래처그룹, "MA_B000065", true);

			// TEST
			((DataTable)cbo파일.DataSource).Rows.Add("DY", "DY");
			((DataTable)cbo파일.DataSource).Rows.Add("HN", "HN");
			((DataTable)cbo파일.DataSource).Rows.Add("엑셀", "엑셀");


			// 검색콤보
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");			

			dt.Rows.Add("차수", "ASC", "최초");
			dt.Rows.Add("차수", "DESC", "최종");

			dt.Rows.Add("DATE", "DT_INQ"	, "접수일자");
			dt.Rows.Add("DATE", "DT_QTN"	, "견적일자");
			dt.Rows.Add("DATE", "DT_SO"		, "수주일자");
			dt.Rows.Add("DATE", "DT_IV"		, "매출일자");
			dt.Rows.Add("DATE", "DT_PU_WF"	, "매입접수일자");

			dt.Rows.Add("담당자", "NO_EMP"		, "영업담당");
			dt.Rows.Add("담당자", "NO_EMP_QTN"	, "견적담당");
			dt.Rows.Add("담당자", "NO_EMP_STK"	, "재고담당");

			dt.Rows.Add("금액", "", "금액검색");
			dt.Rows.Add("금액", "", "--------");
			dt.Rows.Add("금액", "P", "매입금액");
			dt.Rows.Add("금액", "S", "매출금액");
			dt.Rows.Add("금액", "M", "이윤");

			dt.Rows.Add("통화", "EX", "외화");
			dt.Rows.Add("통화", "KR", "원화");

			dt.Rows.Add("KEYWORD", "NM_SUBJECT"		  , "주제");
			dt.Rows.Add("KEYWORD", "CD_ITEM_PARTNER"  , "품목코드");
			dt.Rows.Add("KEYWORD", "NM_ITEM_PARTNER"  , "품목명");
			dt.Rows.Add("KEYWORD", "CD_UNIQUE_PARTNER", "선사코드");
			dt.Rows.Add("KEYWORD", "CD_ITEM"		  , "재고코드");

			dt.Rows.Add("SMART", "1", "일치");	// EQUAL
			dt.Rows.Add("SMART", "2", "포함");	// LIKE
			dt.Rows.Add("SMART", "3", "스마트");	// SMART

			cbo차수.DataSource = dt.Select("TYPE = '차수'").CopyToDataTable();
			cbo차수.SelectedIndex = 1;

			cbo일자.DataSource = dt.Select("TYPE = 'DATE'").CopyToDataTable();
			cbo일자.SelectedIndex = 0;

			cbo담당자.DataSource = dt.Select("TYPE = '담당자'").CopyToDataTable();
			cbo담당자.SelectedIndex = 0;

			cbo금액.DataSource = dt.Select("TYPE = '금액'").CopyToDataTable();
			cbo금액.SelectedIndex = 0;

			cbo통화.DataSource = dt.Select("TYPE = '통화'").CopyToDataTable();
			cbo통화.SelectedIndex = 0;

			cbo키워드1.DataSource = dt.Select("TYPE = 'KEYWORD'").CopyToDataTable();
			cbo키워드1.SetValue("NM_SUBJECT");

			cbo키워드2.DataSource = dt.Select("TYPE = 'KEYWORD'").CopyToDataTable();
			cbo키워드2.SetValue("CD_ITEM_PARTNER");

			cbo키워드3.DataSource = dt.Select("TYPE = 'KEYWORD'").CopyToDataTable();
			cbo키워드3.SetValue("NM_ITEM_PARTNER");

			cbo스마트1.DataSource = dt.Select("TYPE = 'SMART'").CopyToDataTable();
			cbo스마트1.SetValue("2");

			cbo스마트2.DataSource = dt.Select("TYPE = 'SMART'").CopyToDataTable();
			cbo스마트2.SetValue("2");

			cbo스마트3.DataSource = dt.Select("TYPE = 'SMART'").CopyToDataTable();
			cbo스마트3.SetValue("2");

			grd견적파일H.DetailGrids = new FlexGrid[] { grd견적파일L };
			flex수주파일H.DetailGrids = new FlexGrid[] { flex수주파일L };
			flex매출파일H.DetailGrids = new FlexGrid[] { flex매출파일L };

			// 버튼별 정책
			Duzon.ERPU.Grant.UGrant ugrant = new Duzon.ERPU.Grant.UGrant();
			ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "POLICY", btn최소마진예외);
			ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "POLICY", btn최소마진적용);
			ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "ZEROMARGIN", btnApplyZeroMargin);
			ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "ZEROMARGIN", btnExceptZeroMargin);
		}

		private void InitGrid()
		{
			InitGridHead(grd견적파일H);
			InitGridHead(flex수주파일H);
			InitGridHead(flex매출파일H);

			// ================================================== 파일 L
			

			// ================================================== 품목 L

			InitGridLine(grd견적파일L);
			InitGridLine(grd견적품목L);
			InitGridLine(flex수주파일L);
			InitGridLine(flex수주품목L);
			InitGridLine(flex매출파일L);
			InitGridLine(flex매출품목L);

			grd견적파일L.Cols["NO_FILE"].Visible = false;
			grd견적파일L.Cols["NO_HST"].Visible = false;
			grd견적파일L.Cols["LN_PARTNER"].Visible = false;
			grd견적파일L.Cols["NM_VESSEL"].Visible = false;
			grd견적파일L.Cols["NM_EXCH"].Visible = false;
			grd견적파일L.Cols["RT_EXCH"].Visible = false;

			flex수주파일L.Cols["NO_FILE"].Visible = false;
			flex수주파일L.Cols["NO_HST"].Visible = false;
			flex수주파일L.Cols["LN_PARTNER"].Visible = false;
			flex수주파일L.Cols["NM_VESSEL"].Visible = false;
			flex수주파일L.Cols["NM_EXCH"].Visible = false;
			flex수주파일L.Cols["RT_EXCH"].Visible = false;

			flex매출파일L.Cols["NO_FILE"].Visible = false;
			flex매출파일L.Cols["NO_HST"].Visible = false;
			flex매출파일L.Cols["LN_PARTNER"].Visible = false;
			flex매출파일L.Cols["NM_VESSEL"].Visible = false;
			flex매출파일L.Cols["NM_EXCH"].Visible = false;
			flex매출파일L.Cols["RT_EXCH"].Visible = false;
		}

		private void InitGridHead(FlexGrid grid)
		{
			grid.BeginSetting(1, 1, false);
			
			grid.SetCol("NO_FILE"		, "파일번호"		, 80);
			grid.SetCol("NO_HST"		, "*"			, 20);
			grid.SetCol("CD_PARTNER"	, "매출처코드"	, false);
			grid.SetCol("LN_PARTNER"	, "매출처"		, 180);
			grid.SetCol("CD_PARTNER_GRP", "매출처그룹"	, 100);
			grid.SetCol("NO_IMO"		, "IMO"			, false);
			grid.SetCol("NM_VESSEL"		, "선명"			, 150);			
			grid.SetCol("MODEL_ME"		, "대형엔진"		, 100);		// 엔진
			grid.SetCol("MODEL_GE"		, "중형엔진"		, 100);		// 엔진
			grid.SetCol("NO_REF"		, "문의번호"		, 110);		// 탭에 따라 이름변경
			grid.SetCol("DT_INQ"		, "접수일자"		, 80	, false	, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grid.SetCol("DT_QTN"		, "견적일자"		, 80	, false	, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grid.SetCol("DT_SO"			, "수주일자"		, 80	, false	, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grid.SetCol("DT_IV"			, "매출일자"		, 80	, false	, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grid.SetCol("CD_SALEGRP"	, "영업그룹"		, 100);
			grid.SetCol("NM_EMP"		, "영업담당"		, 70);
			grid.SetCol("NM_EMP_QTN"	, "견적담당"		, 70);
			grid.SetCol("NM_EMP_STK"	, "재고담당"		, 70);
			grid.SetCol("CD_EXCH"		, "통화"			, 45);
			grid.SetCol("RT_EXCH"		, "환율"			, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("AM_EX_P"		, "매입금액"		, 105	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("AM_KR_P"		, "매입금액(￦)"	, 105	, false	, typeof(decimal), FormatTpType.MONEY);
			grid.SetCol("AM_EX_S"		, "매출금액"		, 105	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("AM_KR_S"		, "매출금액(￦)"	, 105	, false	, typeof(decimal), FormatTpType.MONEY);
			grid.SetCol("RT_MARGIN"		, "이윤율"		, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("AM_MARGIN"		, "이윤(￦)"		, 105	, false	, typeof(decimal), FormatTpType.MONEY);
			grid.SetCol("NO_REF_P"		, "매입처견적번호", 110);
			grid.SetCol("MAKER_ME"		, "대형제조사"	, false);
			
			grid.Cols["NO_HST"].TextAlign = TextAlignEnum.CenterCenter;
			grid.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			grid.Cols["CD_EXCH"].TextAlign = TextAlignEnum.CenterCenter;
			//grid.SetDataMap("CLS_L", GetDb.Code("MA_B000030"), "CODE", "NAME");
			//grid.SetDataMap("CLS_M", GetDb.Code("MA_B000031"), "CODE", "NAME");
			grid.SetDataMap("CD_PARTNER_GRP", GetDb.Code("MA_B000065"), "CODE", "NAME");
			grid.SetDataMap("CD_EXCH", GetDb.Code("MA_B000005"), "CODE", "NAME");
			grid.SetDataMap("CD_SALEGRP", GetDb.SalesGroup(), "CODE", "NAME");

			grid.SetDefault("22.01.06.02", SumPositionEnum.Top);
			grid.SetExceptSumCol("RT_EXCH", "RT_MARGIN");

			//grid.Cols["CLS_L"].Visible = false;
			//grid.Cols["CLS_M"].Visible = false;
			//grid.Cols["NM_MODEL"].Visible = false;
		}

		private void InitGridLine(FlexGrid grid)
		{
		    grid.BeginSetting(1, 1, false);
			
			grid.SetCol("NO_FILE"			, "파일번호"			, 80);
			grid.SetCol("NO_HST"			, "*"				, 20);
			grid.SetCol("CD_PARTNER"		, "매출처코드"		, false);
			grid.SetCol("LN_PARTNER"		, "매출처"			, 150);
			grid.SetCol("NO_REF"			, "문의번호"			, 80);
			grid.SetCol("NO_PO_PARTNER"		, "주문번호"			, 80);
			grid.SetCol("NO_IMO"			, "IMO"				, false);
			grid.SetCol("NO_HULL"			, "호선번호"			, false);
			grid.SetCol("NM_VESSEL"			, "선명"				, 100);
			grid.SetCol("MODEL_ME"			, "대형엔진"			, 100);		// 엔진
			grid.SetCol("MODEL_GE"			, "중형엔진"			, 100);     // 엔진
			
			grid.SetCol("NM_EXCH"			, "통화"				, 45);
			grid.SetCol("RT_EXCH"			, "환율"				, 60	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
		    grid.SetCol("NO_LINE"			, "항번"				, false);
		    grid.SetCol("NO_DSP"			, "순번"				, 40);
		    grid.SetCol("NM_SUBJECT"		, "주제"				, 200);
		    grid.SetCol("CD_ITEM_PARTNER"	, "품목코드"			, 120);
		    grid.SetCol("NM_ITEM_PARTNER"	, "품목명"			, 230);
			grid.SetCol("CD_UNIQUE_PARTNER", "선사코드"			, 80);
			grid.SetCol("CD_ITEM"			, "재고코드"			, 100);
			grid.SetCol("UCODE"				, "U코드"			, 100);
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
			grid.SetCol("MAPS"				, "맵스"				, 50);
			grid.SetCol("DT_MAPS"			, "맵스발행일"		, 80	, false	, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grid.SetCol("YN_GULL"			, "H"				, false);

			grid.SetCol("DT_IV"		, "매출일자"	, false);
			grid.SetCol("CD_EXCH"	, "통화"		, false);

			grid.Cols["NO_HST"].TextAlign = TextAlignEnum.CenterCenter;
			grid.Cols["NO_DSP"].Format = "####.##";
		    grid.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter; 
			grid.Cols["NM_EXCH"].TextAlign = TextAlignEnum.CenterCenter;
			grid.Cols["UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			//grid.SetDataMap("CLS_L", GetDb.Code("MA_B000030"), "CODE", "NAME");
			//grid.SetDataMap("CLS_M", GetDb.Code("MA_B000031"), "CODE", "NAME");
			grid.SetDataMap("UNIT", GetDb.Code("MA_B000004"), "CODE", "NAME");

		    grid.SettingVersion = "20.07.16.01";
		    grid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
			grid.SetExceptSumCol("RT_EXCH", "UM_EX_STD_P", "RT_DC_P", "UM_EX_P", "UM_KR_P", "RT_PROFIT", "UM_EX_Q", "UM_KR_Q", "RT_DC", "UM_EX_S", "UM_KR_S", "RT_MARGIN", "LT");
			//grid.SetExceptSumCol("RT_EXCH", "UM_EX_STD_P", "RT_DC_P", "RT_PROFIT", "RT_DC", "RT_MARGIN", "LT");
			grid.Rows[0].Height = 28;

			// 매입견적단가는 기본값으로 보이지 않도록 하자 (너무 많이 나오니 헛갈림)
			//grid.Cols["UM_EX_STD_P"].Visible = false;
			//grid.Cols["AM_EX_STD_P"].Visible = false;
			//grid.Cols["RT_DC_P"].Visible = false;

			grid.Styles.Add("CHILD_ROW").ForeColor = Color.Gray;

			//grid.Cols["CLS_L"].Visible = false;
			//grid.Cols["CLS_M"].Visible = false;
			//grid.Cols["NM_MODEL"].Visible = false;

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
			btn최소마진예외.Click += new EventHandler(btn최소마진예외_Click);
			btn최소마진적용.Click += new EventHandler(btn최소마진적용_Click);

			btnExceptZeroMargin.Click += new EventHandler(btnExceptZeroMargin_Click);
			btnApplyZeroMargin.Click += new EventHandler(btnApplyZeroMargin_Click);

			tab.SelectedIndexChanged += new EventHandler(tab_SelectedIndexChanged);
			grd견적파일H.AfterRowChange += new RangeEventHandler(flex견적파일H_AfterRowChange);
			flex수주파일H.AfterRowChange += new RangeEventHandler(flex수주파일H_AfterRowChange);
			flex매출파일H.AfterRowChange += new RangeEventHandler(flex매출파일H_AfterRowChange);

			cbo파일.SelectionChangeCommitted += Cbo파일_SelectionChangeCommitted;
		}

		private void Cbo파일_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (cbo파일.값() == "엑셀")
			{
				OpenFileDialog f = new OpenFileDialog();
				f.Filter = DD("엑셀 파일") + "|*.xls;*.xlsx";

				if (f.ShowDialog() != DialogResult.OK)
				{
					cbo파일.SelectedIndex = 0;
					return;
				}

				ExcelReader excel = new ExcelReader();
				DataTable dtE = excel.Read(f.FileName);

				if (dtE.Rows.Count == 0)
				{
					ShowMessage("엑셀파일을 읽을 수 없습니다.");
					cbo파일.SelectedIndex = 0;
					return;
				}

				string 파일번호s = string.Join(",", dtE.AsEnumerable().Select(x => "'" + x[0] + "'"));
				
				// 조회
				if (tab.SelectedTab == tab견적파일)
				{
					TSQL sql = new TSQL("PS_CZ_SA_QSO_RPT_H_Q");
					sql.변수.추가("@CD_COMPANY"	, ctx회사코드.값());
					sql.변수.추가("@CD_COMPANY_O"	, 상수.사원번호.포함("S-267", "D-038") ? "" : 상수.회사코드);
					sql.변수.추가("@NO_FILE_X"	, 파일번호s);

					grd견적파일H.Binding = sql.결과();
				}
				else if (tab.SelectedTab == tab견적품목)
				{
					TSQL sql = new TSQL("PS_CZ_SA_QSO_RPT_L_Q");
					sql.변수.추가("@CD_COMPANY"	, ctx회사코드.값());
					sql.변수.추가("@CD_COMPANY_O"	, 상수.사원번호.포함("S-267", "D-038") ? "" : 상수.회사코드);
					sql.변수.추가("@NO_FILE_X"	, 파일번호s);

					grd견적품목L.Binding = sql.결과();
				}
				
				cbo파일.SelectedIndex = 0;
			}
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

		#region ==================================================================================================== Event

		private void btn최소마진예외_Click(object sender, EventArgs e)
		{
			if (tab.SelectedTab != tab견적파일)
			{
				ShowMessage("견적현황(파일) 탭에서만 실행 가능합니다.");
				return;
			}

			if (GetTo.String(grd견적파일H["NO_FILE"]) == "")
			{
				ShowMessage("선택된 파일이 없습니다.");
				return;
			}

			string query = @"
UPDATE CZ_SA_QTNH SET
	YN_POLICY = 'N'
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND NO_FILE = '" + grd견적파일H["NO_FILE"] + "'";

			DBMgr.ExecuteNonQuery(query);
			ShowMessage("저장되었습니다.");
		}

		private void btn최소마진적용_Click(object sender, EventArgs e)
		{
			if (tab.SelectedTab != tab견적파일)
			{
				ShowMessage("견적현황(파일) 탭에서만 실행 가능합니다.");
				return;
			}

			if (GetTo.String(grd견적파일H["NO_FILE"]) == "")
			{
				ShowMessage("선택된 파일이 없습니다.");
				return;
			}

			string query = @"
UPDATE CZ_SA_QTNH SET
	YN_POLICY = 'Y'
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND NO_FILE = '" + grd견적파일H["NO_FILE"] + "'";

			DBMgr.ExecuteNonQuery(query);
			ShowMessage("저장되었습니다.");
		}

		private void btnApplyZeroMargin_Click(object sender, EventArgs e)
		{
			if (tab.SelectedTab != tab견적파일)
			{
				ShowMessage("견적현황(파일) 탭에서만 실행 가능합니다.");
				return;
			}

			if (GetTo.String(grd견적파일H["NO_FILE"]) == "")
			{
				ShowMessage("선택된 파일이 없습니다.");
				return;
			}

			string query = @"
UPDATE CZ_SA_QTNH SET
	YN_ZEROMARGIN = 'N'
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND NO_FILE = '" + grd견적파일H["NO_FILE"] + "'";

			DBMgr.ExecuteNonQuery(query);
			ShowMessage("저장되었습니다.");
		}

		private void btnExceptZeroMargin_Click(object sender, EventArgs e)
		{
			if (tab.SelectedTab != tab견적파일)
			{
				ShowMessage("견적현황(파일) 탭에서만 실행 가능합니다.");
				return;
			}

			if (GetTo.String(grd견적파일H["NO_FILE"]) == "")
			{
				ShowMessage("선택된 파일이 없습니다.");
				return;
			}

			string query = @"
UPDATE CZ_SA_QTNH SET
	YN_ZEROMARGIN = 'Y'
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND NO_FILE = '" + grd견적파일H["NO_FILE"] + "'";

			DBMgr.ExecuteNonQuery(query);
			ShowMessage("저장되었습니다.");
		}

		private void tab_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (rdo조회구분1.Checked)
			//{
			//    lbl문의번호.Text = "문의번호";				
			//}
			//else if (rdo조회구분2.Checked)
			//{
			//    lbl문의번호.Text = "주문번호";				
			//}
			//else if (rdo조회구분3.Checked)
			//{
			//    lbl문의번호.Text = "주문번호";				
			//}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			if (tab.SelectedTab == tab견적파일)
			{
				grd견적파일H.Binding = SearchHead("Q");
			}
			else if (tab.SelectedTab == tab견적품목)
			{
				grd견적품목L.Binding = SearchLine("Q", "", "");
			}
			else if (tab.SelectedTab == tab수주파일)
			{
				flex수주파일H.Binding = SearchHead("S");
			}
			else if (tab.SelectedTab == tab수주품목)
			{
				MakeTree(flex수주품목L, SearchLine("S", "", ""));
			}
			else if (tab.SelectedTab == tab매출파일)
			{
				DataTable dt = SearchHead("V");
				flex매출파일H.Binding = dt;

				// 이윤율 계산
				decimal puAmount = GetTo.Decimal(dt.Compute("SUM(AM_KR_P)", ""));
				decimal saAmount = GetTo.Decimal(dt.Compute("SUM(AM_KR_S)", ""));
				decimal marginRate = (saAmount > 0) ? 100 * (1 - puAmount / saAmount) : 0;
				decimal marginRate2 = (saAmount > 0) ? 100 * ((saAmount - puAmount) / saAmount) : 0;

				string format = GetFormatDescription(DataDictionaryTypes.CZ, FormatTpType.FOREIGN_MONEY, FormatFgType.SELECT);
				flex매출파일H[flex매출파일H.Rows.Fixed - 1, "RT_MARGIN"] = marginRate.ToString(format);
			}
			else if (tab.SelectedTab == tab매출품목)
			{
				MakeTree(flex매출품목L, SearchLine("V", "", ""));
				//flex매출파일L.Binding = SearchLine("V", "", "");
			}
		}

		private void flex견적파일H_AfterRowChange(object sender, RangeEventArgs e)
		{
			string filter = "NO_FILE = '" + grd견적파일H["NO_FILE"] + "'";

			if (grd견적파일H.DetailQueryNeed)
			{
				DataTable dt = SearchLine("Q", grd견적파일H["NO_FILE"].ToString(), grd견적파일H["NO_HST"].ToString());
				grd견적파일L.BindingAdd(dt, filter);
			}
			else
			{
				grd견적파일L.BindingAdd(null, filter);
			}			
		}	

		private void flex수주파일H_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt = SearchLine("S", flex수주파일H["NO_FILE"].ToString(), "");
			MakeTree(flex수주파일L, dt);	// 트리구조 만들기			
		}

		private void flex매출파일H_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt = SearchLine("V", flex매출파일H["NO_FILE"].ToString(), "");
			MakeTree(flex매출파일L, dt);	// 트리구조 만들기
		}

		private void MakeTree(FlexGrid grid, DataTable dt)
		{
			// 바인딩
			grid.UnBinding = dt;

			// 트리 만들기 준비
			grid.Tree.Column = grid.Cols["LN_SUPPLIER"].Index;
			grid.Tree.LineColor = Color.Transparent;

			for (int i = grid.Rows.Fixed, colNo = 1; i < grid.Rows.Count; i++)
			{
				// 트리 노드 세팅
				grid.Rows[i].IsNode = true;
				grid.Rows[i].Node.Level = (int)grid[i, "CD_LEVEL"];

				if (grid.Rows[i].Node.Level == 1)
				{
					grid[i, 0] = colNo++;
				}
				else
				{
					grid[i, 0] = "";
					grid.Rows[i].Style = grid.Styles["CHILD_ROW"];	// 레벨2 스타일 적용
				}
			}

			grid.Tree.Show(1);

			// 트리로 인해 합계가 뻥튀기 된 부분 보정
			string format = GetFormatDescription(DataDictionaryTypes.CZ, FormatTpType.FOREIGN_MONEY, FormatFgType.SELECT);

			grid[grid.Rows.Fixed - 1, "AM_EX_STD_P"] = GetTo.Decimal(dt.Compute("SUM(AM_EX_STD_P)", "CD_LEVEL = 1")).ToString(format);
			grid[grid.Rows.Fixed - 1, "AM_EX_P"] = GetTo.Decimal(dt.Compute("SUM(AM_EX_P)", "CD_LEVEL = 1")).ToString(format);
			grid[grid.Rows.Fixed - 1, "AM_KR_P"] = GetTo.Decimal(dt.Compute("SUM(AM_KR_P)", "CD_LEVEL = 1")).ToString(format);

			// 최종이윤율 계산
			decimal puAmount = GetTo.Decimal(dt.Compute("SUM(AM_KR_P)", "CD_LEVEL = 1"));
			decimal saAmount = GetTo.Decimal(dt.Compute("SUM(AM_KR_S)", "CD_LEVEL = 1"));
			decimal marginRate = (saAmount > 0) ? 100 * (1 - puAmount / saAmount) : 0;

			grid[grid.Rows.Fixed - 1, "RT_MARGIN"] = marginRate.ToString(format);
		}
		
		private DataTable SearchHead(string mode)
		{			
			DBMgr dbm = new DBMgr();
			dbm.Procedure = "PS_CZ_SA_QSO_RPT_H_" + mode;
			dbm.DebugMode = DebugMode.Popup;
			dbm.AddParameter("@CD_COMPANY"		, ctx회사코드.CodeValue);

			if (!LoginInfo.EmployeeNo.In("S-267", "D-038"))
				dbm.AddParameter("@CD_COMPANY_O"	, LoginInfo.CompanyCode);

			if (txt파일번호.GetValue() != "")
			{
				dbm.AddParameter("@NO_FILE"			, txt파일번호.GetValue());
			}			
			else
			{
				dbm.AddParameter("@CD_FILE"			, cbo파일.GetValue());
				dbm.AddParameter("@DT"				, cbo일자.GetValue());
				dbm.AddParameter("@DT_F"			, dtp일자.StartDateToString);
				dbm.AddParameter("@DT_T"			, dtp일자.EndDateToString);
			}

			dbm.AddParameter("@NO_EMP_COL"		, cbo담당자.GetValue());
			dbm.AddParameter("@NO_EMP_VAL"		, ctx담당자.CodeValue);
			dbm.AddParameter("@CD_SALEGRP"		, ctx영업그룹.CodeValue);
			dbm.AddParameter("@CD_PARTNER"		, ctx매출처.CodeValue);
			dbm.AddParameter("@CD_PARTNER_GRP"	, cbo거래처그룹.GetValue());
			dbm.AddParameter("@NO_REF"			, txt문의번호.GetValue());			
			dbm.AddParameter("@CD_SUPPLIER"		, ctx매입처.CodeValue);
			dbm.AddParameter("@NO_IMO"			, ctx호선번호.CodeValue);	
			dbm.AddParameter("@AM"				, cbo금액.GetValue());
			dbm.AddParameter("@AM_F"			, cur금액F.GetValue());
			dbm.AddParameter("@AM_T"			, cur금액T.GetValue());
			dbm.AddParameter("@CD_CURRENCY"		, cbo통화.GetValue());
			dbm.AddParameter("@KW_COLUMN1"		, cbo키워드1.GetValue());
			dbm.AddParameter("@KW_TEXT1"		, txt키워드1.GetValue());
			dbm.AddParameter("@KW_SMART1"		, cbo스마트1.GetValue());
			dbm.AddParameter("@KW_COLUMN2"		, cbo키워드2.GetValue());
			dbm.AddParameter("@KW_TEXT2"		, txt키워드2.GetValue());
			dbm.AddParameter("@KW_SMART2"		, cbo스마트2.GetValue());
			dbm.AddParameter("@KW_COLUMN3"		, cbo키워드3.GetValue());
			dbm.AddParameter("@KW_TEXT3"		, txt키워드3.GetValue());
			dbm.AddParameter("@KW_SMART3"		, cbo스마트3.GetValue());
			dbm.AddParameter("@GRP_ITEM"		, ctx품목군.CodeValue);
			dbm.AddParameter("@YN_EXTRA"		, chkExtraCharge.Checked ? "Y" : "N");
			dbm.AddParameter("@YN_PURCHASE"		, chkPurchasQty.Checked ? "Y" : "N");
			dbm.AddParameter("@YN_POLICY"		, chk최소마진예외.Checked ? "N" : "");
			dbm.AddParameter("@YN_CLAIM_EX"		, chk클레임제외.Checked ? "Y" : "N");

			return dbm.GetDataTable();
		}

		private DataTable SearchLine(string mode, string fileNumber, string historyNumber)
		{
			DBMgr dbm = new DBMgr();
			dbm.DebugMode = DebugMode.Print;
			dbm.Procedure = "PS_CZ_SA_QSO_RPT_L_" + mode;
			dbm.AddParameter("@CD_COMPANY"		, ctx회사코드.CodeValue);

			if (!LoginInfo.EmployeeNo.In("S-267", "D-038"))
				dbm.AddParameter("@CD_COMPANY_O", LoginInfo.CompanyCode);

			if (fileNumber != "")
			{
				dbm.AddParameter("@NO_FILE"			, fileNumber);
			}			
			else
			{
				if (txt파일번호.GetValue() != "")
				{
					dbm.AddParameter("@NO_FILE"			, txt파일번호.GetValue());
				}
				else
				{
					dbm.AddParameter("@CD_FILE"			, cbo파일.GetValue());
					dbm.AddParameter("@DT"				, cbo일자.GetValue());
					dbm.AddParameter("@DT_F"			, dtp일자.StartDateToString);
					dbm.AddParameter("@DT_T"			, dtp일자.EndDateToString);
				}

				dbm.AddParameter("@NO_EMP"			, ctx담당자.CodeValue);
				dbm.AddParameter("@CD_SALEGRP"		, ctx영업그룹.CodeValue);
				dbm.AddParameter("@CD_PARTNER"		, ctx매출처.CodeValue);
				dbm.AddParameter("@CD_PARTNER_GRP"	, cbo거래처그룹.GetValue());
				dbm.AddParameter("@NO_REF"			, txt문의번호.GetValue());
				dbm.AddParameter("@CD_SUPPLIER"		, ctx매입처.CodeValue);
				dbm.AddParameter("@NO_IMO"			, ctx호선번호.CodeValue);	
				dbm.AddParameter("@AM"				, cbo금액.GetValue());
				dbm.AddParameter("@AM_F"			, cur금액F.GetValue());
				dbm.AddParameter("@AM_T"			, cur금액T.GetValue());
				dbm.AddParameter("@CD_CURRENCY"		, cbo통화.GetValue());
			}
			
			dbm.AddParameter("@KW_COLUMN1"		, cbo키워드1.GetValue());
			dbm.AddParameter("@KW_TEXT1"		, txt키워드1.GetValue());
			dbm.AddParameter("@KW_SMART1"		, cbo스마트1.GetValue());
			dbm.AddParameter("@KW_COLUMN2"		, cbo키워드2.GetValue());
			dbm.AddParameter("@KW_TEXT2"		, txt키워드2.GetValue());
			dbm.AddParameter("@KW_SMART2"		, cbo스마트2.GetValue());
			dbm.AddParameter("@KW_COLUMN3"		, cbo키워드3.GetValue());
			dbm.AddParameter("@KW_TEXT3"		, txt키워드3.GetValue());
			dbm.AddParameter("@KW_SMART3"		, cbo스마트3.GetValue());
			dbm.AddParameter("@GRP_ITEM"		, ctx품목군.CodeValue);
			dbm.AddParameter("@YN_EXTRA"		, chkExtraCharge.Checked ? "Y" : "N");
			dbm.AddParameter("@YN_PURCHASE"		, chkPurchasQty.Checked ? "Y" : "N");
			dbm.AddParameter("@YN_CLAIM_EX"		, chk클레임제외.Checked ? "Y" : "N");
			DataTable dt = dbm.GetDataTable();

			foreach (DataRow row in dt.Rows)
			{
				if (GetTo.Int(row["CNT_SUPPLIER"]) > 1)
				{
					row["LN_SUPPLIER"] = row["LN_SUPPLIER"] + " 외 " + (GetTo.Int(row["CNT_SUPPLIER"]) - 1) + "건";
				}
			}

			return dt;
		}		

		#endregion
	}
}



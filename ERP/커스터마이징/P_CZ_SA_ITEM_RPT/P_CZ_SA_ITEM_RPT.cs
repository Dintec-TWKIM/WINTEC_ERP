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
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using System.Linq;
using DX;
using System.Data.SqlClient;

namespace cz
{
	public partial class P_CZ_SA_ITEM_RPT : PageBase
	{
		string Codes = "";

		#region ==================================================================================================== Property

		public string Target
		{
			set
			{
				if (value == "견적")
					rdo적용대상1.Checked = true;
			}
		}

		public string Keyword
		{
			get
			{
				return tbx키워드.Text;
			}
			set
			{
				tbx키워드.Text = value;
			}
		}

		public decimal Frequency
		{
			get
			{
				return cur건수.DecimalValue;
			}
			set
			{
				cur건수.DecimalValue = value;
			}
		}

		public DataTable Result
		{
			get
			{
				return grd헤드.DataTable;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_SA_ITEM_RPT()
		{
			StartUp.Certify(this);
			InitializeComponent();
			this.SetConDefault();
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			ctb회사코드.CodeValue = LoginInfo.CompanyCode;
			ctb회사코드.CodeName = LoginInfo.CompanyName;

			dtp일자.StartDateToString = Util.GetToday(-365);
			dtp일자.EndDateToString = Util.GetToday();					

			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			dt.Rows.Add("일자", "B.DT_INQ", "접수일자");
			dt.Rows.Add("일자", "B.DT_QTN", "견적일자");
			dt.Rows.Add("일자", "D.DT_SO", "수주일자");

			dt.Rows.Add("기간", "18"	, "18개월");
			dt.Rows.Add("기간", "12"	, "12개월");
			dt.Rows.Add("기간", "9"	, "9개월");
			dt.Rows.Add("기간", "6"	, "6개월");
			dt.Rows.Add("기간", "3"	, "3개월");

			dt.Rows.Add("키워드", "CD_ITEM_PARTNER"	, "품목코드");
			dt.Rows.Add("키워드", "CD_ITEM"			, "재고코드");
			dt.Rows.Add("키워드", "UCODE"			, "U코드");
			dt.Rows.Add("키워드", "KCODE"			, "K코드");

			cbo일자.DataBind(dt.Select("TYPE = '일자'").CopyToDataTable(), false);
			cbo기간.DataBind(dt.Select("TYPE = '기간'").CopyToDataTable(), false);
			cbo키워드.DataBind(dt.Select("TYPE = '키워드'").CopyToDataTable(), true);

			cbo기간.SetValue("12");


			grd헤드.DetailGrids = new FlexGrid[] { grd라인 };

			InitGrid();
			InitEvent();
		}
		
		protected override void InitPaint()
		{
			lbl건수.TextAlign = ContentAlignment.MiddleLeft;      // 자동 우측정렬이 되버려서 다시 재설정

			if (!GetDb.IsAdmin(LoginInfo.EmployeeNo))
			{
				cbo키워드.RemoveItem("KCODE");
				chkK코드.Visible = false;
				grd헤드.Cols.Remove("KCODE");
			}

			cbo키워드.SetValue("UCODE");
			Cbo키워드_SelectionChangeCommitted(null, null);
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{
			DataTable dtPo = new DataTable();
			dtPo.Columns.Add("CODE");
			dtPo.Columns.Add("NAME");
			dtPo.Rows.Add("", "");
			dtPo.Rows.Add("C", DD("완료"));
			dtPo.Rows.Add("R", DD("예정"));
			dtPo.Rows.Add("X", DD("불가"));

			// ********** 헤드
			grd헤드.BeginSetting(2, 1, false);
										
			grd헤드.SetCol("CD_ITEM_PARTNER"	, "품목코드"	, 120);
			grd헤드.SetCol("CD_ITEM"			, "재고코드"	, 90);
			grd헤드.SetCol("UCODE"			, "U코드"	, 120);
			grd헤드.SetCol("KCODE"			, "K코드"	, 120);

			grd헤드.SetCol("NM_ITEM"			, "재고명"	, 300);
			grd헤드.SetCol("CLS_L"			, "대분류"	, false);
			grd헤드.SetCol("CLS_M"			, "중분류"	, false);
			grd헤드.SetCol("CNT_CODE"		, "중복"		, 45	, typeof(decimal), FormatTpType.QUANTITY);

			grd헤드.SetCol("CNT_QTN"			, "건수"		, "견적"		, 60	, typeof(decimal), FormatTpType.QUANTITY);
			grd헤드.SetCol("CNT_SO"			, "건수"		, "수주"		, 60	, typeof(decimal), FormatTpType.QUANTITY);
			grd헤드.SetCol("CNT_RT_SO"		, "건수"		, "수주율"	, 60	, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			grd헤드.SetCol("SUM_QTN"			, "수량"		, "견적"		, 60	, typeof(decimal), FormatTpType.QUANTITY);
			grd헤드.SetCol("SUM_QTN_COR"		, "수량"		, "견적*"	, 60	, typeof(decimal), FormatTpType.QUANTITY);
			grd헤드.SetCol("SUM_SO"			, "수량"		, "수주"		, 60	, typeof(decimal), FormatTpType.QUANTITY);
			grd헤드.SetCol("SUM_SO_COR"		, "수량"		, "수주*"	, 60	, typeof(decimal), FormatTpType.QUANTITY);
			grd헤드.SetCol("SUM_RT_SO"		, "수량"		, "수주율"	, 60	, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			grd헤드.SetCol("AVG_UM_KR_P"		, "평균"		, "매입단가"	, 90	, typeof(decimal), FormatTpType.MONEY);
			grd헤드.SetCol("AVG_UM_KR_S"		, "평균"		, "매출단가"	, 90	, typeof(decimal), FormatTpType.MONEY);
			grd헤드.SetCol("AVG_LT"			, "평균"		, "납기"		, 55	, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			grd헤드.SetCol("MIN_UM_KR_P"		, "최저"		, "매입단가"	, 90	, typeof(decimal), FormatTpType.MONEY);
			grd헤드.SetCol("MIN_NM_VENDOR"	, "최저"		, "매입처"	, 130);
			grd헤드.SetCol("MIN_UM_KR_S"		, "최저"		, "매출단가"	, 90	, typeof(decimal), FormatTpType.MONEY);

			grd헤드.SetCol("MAX_UM_KR_P"		, "최고"		, "매입단가"	, 90	, typeof(decimal), FormatTpType.MONEY);
			grd헤드.SetCol("MAX_UM_KR_S"		, "최고"		, "매출단가"	, 90	, typeof(decimal), FormatTpType.MONEY);

			grd헤드.SetCol("LAST_UM_KR_P"	, "최근"		, "매입단가"	, 90	, typeof(decimal), FormatTpType.MONEY);
			grd헤드.SetCol("LAST_VENDOR"		, "최근"		, "매입처"	, 200);

			grd헤드.SetCol("LT_PO_MAX"		, "발주 납기"	, "최대"		, 50	, typeof(decimal), FormatTpType.MONEY);
			grd헤드.SetCol("LT_PO_LAST"		, "발주 납기"	, "최근"		, 50	, typeof(decimal), FormatTpType.MONEY);

			grd헤드.SetDefault("22.07.21.01", SumPositionEnum.Top);
			grd헤드.SetExceptSumCol("CNT_CODE", "CNT_RT_SO", "SUM_RT_SO", "AVG_UM_KR_P", "AVG_UM_KR_S", "AVG_LT", "MIN_UM_KR_P", "MIN_UM_KR_S", "MAX_UM_KR_P", "MAX_UM_KR_S", "LAST_UM_KR_P", "LT_PO_MAX", "LT_PO_LAST");
			grd헤드.SetExceptSumCol("NO_DSP", "UM_EX_P", "UM_KR_P", "RT_PROFIT", "UM_EX_Q", "UM_KR_Q", "RT_DC", "UM_EX_S", "UM_KR_S", "RT_MARGIN", "LT");
			grd헤드.SetAlternateRow();
			grd헤드.SetMalgunGothic();

			// ********** 라인
			grd라인.세팅시작(2);

			grd라인.컬럼세팅("NO_FILE"		, "파일번호"		, 90	, 정렬.가운데);
			grd라인.컬럼세팅("NO_LINE"		, "항번"			, false);
			grd라인.컬럼세팅("DT_INQ"			, "접수일자"		, 80	, 포맷.날짜);
			grd라인.컬럼세팅("DT_QTN"			, "견적일자"		, 80	, 포맷.날짜);
			grd라인.컬럼세팅("NM_EMP"			, "담당자"		, 90	, 정렬.가운데);
			grd라인.컬럼세팅("NM_BUYER"		, "매출처"		, false);
			grd라인.컬럼세팅("NO_DSP"			, "순번"			, 45	, "####.##", 정렬.가운데);
			grd라인.컬럼세팅("CNT_ITEM"		, "종수"			, 45	, 포맷.수량);
			grd라인.컬럼세팅("NM_SUBJECT"		, "주제"			, false);
			grd라인.컬럼세팅("CD_ITEM_PARTNER", "품목코드"		, 120);
			grd라인.컬럼세팅("NM_ITEM_PARTNER", "품목명"		, 200);
			grd라인.컬럼세팅("CD_ITEM"		, "재고코드"		, 80);
			grd라인.컬럼세팅("NM_ITEM"		, "재고명"		, false);
			grd라인.컬럼세팅("CD_UNIT"		, "단위"			, "접수"		, 45	, 정렬.가운데);
			grd라인.컬럼세팅("CD_UNIT_QTN"	, "단위"			, "견적"		, 45	, 정렬.가운데);
			grd라인.컬럼세팅("CD_UNIT_STK"	, "단위"			, "표준"		, 45	, 정렬.가운데);
			grd라인.컬럼세팅("QT"				, "수량"			, "접수"		, 50	, 포맷.수량);
			grd라인.컬럼세팅("QT_QTN"			, "수량"			, "견적"		, 50	, 포맷.수량);
			grd라인.컬럼세팅("QT_QTN_COR"		, "수량"			, "견적*"	, 50	, 포맷.수량);
			grd라인.컬럼세팅("QT_SO"			, "수량"			, "수주"		, 50	, 포맷.수량);
			grd라인.컬럼세팅("QT_SO_COR"		, "수량"			, "수주*"	, 50	, 포맷.수량);
			grd라인.컬럼세팅("DC_RMK"			, "비고"			, false);
			grd라인.컬럼세팅("CD_VENDOR"		, "매입처코드"	, false);
			grd라인.컬럼세팅("LN_VENDOR"		, "매입처"		, 180);
			grd라인.컬럼세팅("UM_EX_STD_P"	, "매입견적단가"	, false);
			grd라인.컬럼세팅("RT_DC_P"		, "매입DC"		, false);
			grd라인.컬럼세팅("UM_EX_P"		, "매입단가"		, "외화"		, 90	, 포맷.외화단가);
			grd라인.컬럼세팅("UM_KR_P"		, "매입단가"		, "원화"		, 90	, 포맷.외화단가);
			grd라인.컬럼세팅("RT_PROFIT"		, "매출견적단가"	, "이윤(%)"	, 55	, 포맷.외화단가);
			grd라인.컬럼세팅("UM_EX_Q"		, "매출견적단가"	, "외화"		, 90	, 포맷.외화단가);
			grd라인.컬럼세팅("UM_KR_Q"		, "매출견적단가"	, "원화"		, 90	, 포맷.외화단가);
			grd라인.컬럼세팅("RT_DC"			, "매출단가"		, "DC(%)"	, 55	, 포맷.외화단가);
			grd라인.컬럼세팅("UM_EX_S"		, "매출단가"		, "외화"		, 90	, 포맷.외화단가);
			grd라인.컬럼세팅("UM_KR_S"		, "매출단가"		, "원화"		, 90	, 포맷.외화단가);
			grd라인.컬럼세팅("RT_MARGIN"		, "최종(%)"					, 55	, 포맷.외화단가);
			
			grd라인.컬럼세팅("LT"				, "납기"			, 50	, 포맷.원화단가);
			grd라인.컬럼세팅("LT_N"			, "납기(일반)"	, 50	, 포맷.원화단가);
			grd라인.컬럼세팅("LT_S"			, "납기(재고)"	, 50	, 포맷.원화단가);
			
			grd라인.컬럼세팅("YN_SO"			, "수주"			, 45	, 정렬.가운데);
			grd라인.컬럼세팅("TP_BOM"			, "BOM구분"		, 45	, false);

			grd라인.컬럼세팅("YN_SERIES"		, "시리즈"		, 45	, 정렬.가운데);
			grd라인.컬럼세팅("YN_DUP"			, "중복"			, 45	, 정렬.가운데);
			grd라인.컬럼세팅("YN_OUTLIER"		, "이상치"		, 45	, 포맷.체크);
			grd라인.컬럼세팅("YN_PO_HGS"		, "발주"			, 45	, 정렬.가운데);
			grd라인.컬럼세팅("CD_PO_HGS"		, "진행상황"		, 80);

			grd라인.데이터맵("CD_UNIT", 코드.단위());
			grd라인.데이터맵("CD_UNIT_QTN", 코드.단위());
			grd라인.데이터맵("CD_UNIT_STK", 코드.단위());
			grd라인.데이터맵("CD_PO_HGS", dtPo);

			grd라인.세팅종료("22.07.21.02", true);
			grd라인.SetExceptSumCol("NO_DSP", "UM_EX_P", "UM_KR_P", "RT_PROFIT", "UM_EX_Q", "UM_KR_Q", "RT_DC", "UM_EX_S", "UM_KR_S", "RT_MARGIN", "LT");			
			grd라인.에디트컬럼("QT_QTN_COR", "QT_SO_COR", "YN_OUTLIER", "CD_PO_HGS");

			grd라인.Cols["DT_QTN"].Visible = false;
			grd라인.Cols["LT_N"].Visible = false;
			grd라인.Cols["LT_S"].Visible = false;
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			cbo기간.SelectionChangeCommitted += Cbo기간_SelectionChangeCommitted;

			ctb대분류.QueryAfter += Ctb대분류_QueryAfter;
			ctb대분류.QueryBefore += Ctb대분류_QueryBefore;
			ctb중분류.QueryBefore += Ctb중분류_QueryBefore;

			cbo키워드.SelectionChangeCommitted += Cbo키워드_SelectionChangeCommitted;
			btn엑셀.Click += Btn엑셀_Click;
			
			chk품목코드.CheckedChanged += Chk코드_CheckedChanged;
			chk재고코드.CheckedChanged += Chk코드_CheckedChanged;
			chkU코드.CheckedChanged += Chk코드_CheckedChanged;
			chkK코드.CheckedChanged += Chk코드_CheckedChanged;

			grd헤드.AfterRowChange += Grd헤드_AfterRowChange;

			btn전체조회.Click += Btn전체조회_Click;


			grd라인.AfterEdit += Grd라인_AfterEdit;
			btn테스트.Click += Btn테스트_Click;
			grd라인.ValidateEdit += Grd라인_ValidateEdit;
		}

		private void Cbo기간_SelectionChangeCommitted(object sender, EventArgs e)
		{
			dtp일자.EndDateToString = Util.GetToday();
			dtp일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-cbo기간.GetValue().ToInt()).ToString("yyyyMMdd");
		}

		private void Grd라인_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			//e.edi
		}

		private void Btn테스트_Click(object sender, EventArgs e)
		{
			//throw new NotImplementedException();
			PDF.Merge(@"d:\test.pdf", @"d:\ISCI200859 딘텍(1).pdf", @"d:\BE20000082_00732_PINQ_20200618.pdf", @"d:\SCSCORD2020080001_딘텍.pdf");

			int a = PDF.GetPageCount(@"d:\ISCI200859 딘텍(1).pdf");
			int b = PDF.GetPageCount(@"d:\BE20000082_00732_PINQ_20200618.pdf");

		}

		private void Grd라인_AfterEdit(object sender, RowColEventArgs e)
		{
			string colName = grd라인.Cols[e.Col].Name;

			if (colName == "YN_OUTLIER")
			{
				string query = @"
UPDATE CZ_SA_QTNL SET
	YN_OUTLIER = '" + grd라인[e.Row, e.Col] + @"'
WHERE 1 = 1
	AND CD_COMPANY = '" + grd라인[e.Row, "CD_COMPANY"] + @"'
	AND NO_FILE = '" + grd라인[e.Row, "NO_FILE"] + @"'
	AND NO_LINE = " + grd라인[e.Row, "NO_LINE"];
				SQL.ExecuteNonQuery(query);
			}
			else if (colName == "CD_PO_HGS")
			{
				string query = "";
				string cd_po_hgs = grd라인[e.Row, e.Col].문자();

				if (cd_po_hgs == "R")
				{
					query = @"
UPDATE CZ_SA_QTNH SET
	DC_RMK = NEOE.TRIM(REPLACE(DC_RMK, '기획실 재고발주예정 / 수주시 STOCK메일 연락요망', '')) + CHAR(13) + CHAR(10) + '기획실 재고발주예정 / 수주시 STOCK메일 연락요망'
,	CD_PO_HGS = '" + grd라인[e.Row, e.Col] + @"'
WHERE 1 = 1
	AND CD_COMPANY = '" + grd라인[e.Row, "CD_COMPANY"] + @"'
	AND NO_FILE = '" + grd라인[e.Row, "NO_FILE"] + @"'";
				}
				else
				{
					query = @"
UPDATE CZ_SA_QTNH SET
	DC_RMK = NEOE.TRIM(REPLACE(DC_RMK, '기획실 재고발주예정 / 수주시 STOCK메일 연락요망', ''))
,	CD_PO_HGS = '" + grd라인[e.Row, e.Col] + @"'
WHERE 1 = 1
	AND CD_COMPANY = '" + grd라인[e.Row, "CD_COMPANY"] + @"'
	AND NO_FILE = '" + grd라인[e.Row, "NO_FILE"] + @"'";
				}


				SQL.ExecuteNonQuery(query);
			}
			else if (colName == "QT_QTN_COR")
			{
				string query = @"
UPDATE CZ_SA_QTNL SET
	QT_QTN_COR = '" + grd라인[e.Row, e.Col] + @"'
WHERE 1 = 1
	AND CD_COMPANY = '" + grd라인[e.Row, "CD_COMPANY"] + @"'
	AND NO_FILE = '" + grd라인[e.Row, "NO_FILE"] + @"'
	AND NO_LINE = " + grd라인[e.Row, "NO_LINE"];
				SQL.ExecuteNonQuery(query);
			}
			else if (colName == "QT_SO_COR")
			{
				string query = @"
UPDATE SA_SOL SET
	QT_SO_COR = '" + grd라인[e.Row, e.Col] + @"'
WHERE 1 = 1
	AND CD_COMPANY = '" + grd라인[e.Row, "CD_COMPANY"] + @"'
	AND NO_SO = '" + grd라인[e.Row, "NO_FILE"] + @"'
	AND SEQ_SO = " + grd라인[e.Row, "NO_LINE"];
				SQL.ExecuteNonQuery(query);
			}
		}

		private void Btn전체조회_Click(object sender, EventArgs e)
		{
			if (Codes == "")
				return;

			DataTable dt = null;
			//string filter = "";

			// 일치조건 필터링
			

			
			DBParameters dbp = GetSearchCond("L");
			
			DBMgr dbm = new DBMgr
			{
				DebugMode = DebugMode.Popup
			,	Procedure = "PS_CZ_SA_ITEM_RPT_L_R2"
			};
			dbm.AddParameterRange(dbp.Parameters);
			dbm.AddParameter("@" + cbo키워드.GetValue() + "_X", Codes);
			dt = dbm.GetDataTable();

			grd라인.Binding = dt;
			//grd라인.BindingAdd(dt, filter);
		}

		private void Ctb대분류_QueryAfter(object sender, BpQueryArgs e)
		{
			ctb중분류.Clear();
		}

		private void Ctb대분류_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "MA_B000030";
		}

		private void Ctb중분류_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
			e.HelpParam.P42_CD_FIELD2 = ctb대분류.CodeValue;
		}

		private void Cbo키워드_SelectionChangeCommitted(object sender, EventArgs e)
		{
			foreach (Control c in pnl검색조건.Controls)
			{
				if (c is CheckBoxExt o)
				{
					o.Checked = false;

					if (o.GetTag() == cbo키워드.GetValue())
						o.Checked = true;
				}
			}
		}

		private void Chk코드_CheckedChanged(object sender, EventArgs e)
		{
			CheckBoxExt chk = (CheckBoxExt)sender;

			if (!chk.Checked)
				return;

			foreach (Control c in pnl검색조건.Controls)
			{
				if (chk != c && c is CheckBoxExt o) 
					o.Checked = false;
			}
		}

		#endregion
	
		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;
			
			UT.ShowPgb(DD("빈도 데이터 조회 중입니다."));
			ToggleColumn();

			// 조회
			SQL sql = new SQL("PS_CZ_SA_ITEM_RPT_H_3", SQLType.Procedure, sqlDebug);
			sql.Parameter.Add2(GetSearchCond2());
			sql.Parameter.Add2("@CHK_" + chkU코드.GetCheckedControl().GetTag(), "Y");
			sql.Parameter.Add2("@CNT_REPEAT", cur건수.DecimalValue);
			sql.Parameter.Add2("@" + cbo키워드.GetValue(), tbx키워드.Text);
			sql.Parameter.Add2("@YN_MIN_VENDOR", chk최저매입처.Checked ? "Y" : "N");
			DataTable dt = sql.GetDataTable();

			UT.ShowPgb(DD("최근 매입가 조회 중입니다."));
			DataTable dtLast = JoinLastData(dt);

			UT.ShowPgb(DD("화면에 표시 중입니다."));
			grd헤드.Binding = dtLast;
			
			UT.ClosePgb();
		}

		private void Grd헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt = null;
			string filter = "";

			// 일치조건 필터링
			if (chk품목코드.Checked)	filter = " AND CD_ITEM_PARTNER = '" + grd헤드.GetValue("CD_ITEM_PARTNER") + "'";
			if (chk재고코드.Checked)	filter = " AND CD_ITEM = '" + grd헤드.GetValue("CD_ITEM") + "'";
			if (chkU코드.Checked)	filter = " AND UCODE = '" + grd헤드.GetValue("UCODE") + "'";
			if (chkK코드.Checked)	filter = " AND KCODE = '" + grd헤드.GetValue("KCODE") + "'";
			
			if (filter == "")
				return;
			else
				filter = filter.Substring(5);

			if (grd헤드.DetailQueryNeed)
			{
				SQL sql = new SQL("PS_CZ_SA_ITEM_RPT_L_4", SQLType.Procedure, SQLDebug.Print);
				sql.Parameter.Add2(GetSearchCond2());
				sql.Parameter.Add2("@CD_ITEM_PARTNER", grd헤드.GetValue("CD_ITEM_PARTNER"));
				sql.Parameter.Add2("@CD_ITEM"		 , grd헤드.GetValue("CD_ITEM"));
				sql.Parameter.Add2("@UCODE"			 , grd헤드.GetValue("UCODE"));
				sql.Parameter.Add2("@KCODE"			 , grd헤드.GetValue("KCODE"));
				dt = sql.GetDataTable();
			}

			if (chk발주불가.Checked)
				filter += " AND CD_PO_HGS <> 'C' AND CD_PO_HGS <> 'X'";


			grd라인.BindingAdd(dt, filter);
		}
		
		private void Btn엑셀_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.None;

			DataTable dtExcel = EXCEL.ReadFile();

			if (dtExcel == null)
			{
				ShowMessage("엑셀파일을 읽을 수 없습니다.");
				return;
			}

			string codes = "";
			codes = string.Join(",", dtExcel.AsEnumerable().Select(x => "'" + x[0] + "'").ToArray());

			Util.ShowProgress(DD("빈도 데이터 조회 중입니다."));
			ToggleColumn();

			// 조회
			SQL sql = new SQL("PS_CZ_SA_ITEM_RPT_H_3", SQLType.Procedure, sqlDebug);
			sql.Parameter.Add2(GetSearchCond2());
			sql.Parameter.Add2("@CHK_" + chkU코드.GetCheckedControl().GetTag(), "Y");
			sql.Parameter.Add2("@CNT_REPEAT", cur건수.DecimalValue);
			sql.Parameter.Add2("@" + cbo키워드.GetValue() + "_X", codes);
			//dbm.AddParameter("@" + cbo키워드.GetValue() + "_X", codes);
			DataTable dt = sql.GetDataTable();

			Util.ShowProgress(DD("최근 매입가 조회 중입니다."));
			DataTable dtLast = JoinLastData(dt);

			Util.ShowProgress(DD("화면에 표시 중입니다."));
			grd헤드.Binding = dtLast;
			Util.CloseProgress();

			Codes = codes;
		}

		public DataTable JoinLastData(DataTable dtItem)
		{
			if (!chk최근매입가.Checked)
				return dtItem;			

			string codeName = chkU코드.GetCheckedControl().GetTag();
			
			// Distinct된 데이터 가져오기
			DataTable dtCode = new DataTable();
			dtCode.Columns.Add("CD_COMPANY"	, typeof(string));
			dtCode.Columns.Add(codeName		, typeof(string));

			// group by 쿼리			
			var q1 = from f in dtItem.AsEnumerable()
					 where !f.IsNull(codeName) && f.Field<string>(codeName) != ""
					 group f by new { key1 = f.Field<string>("CD_COMPANY"), key2 = f.Field<string>(codeName) }
					 into g
					 select dtCode.LoadDataRow(new object[] { g.Key.key1, g.Key.key2 }, false);

			// 실행
			q1.Count();

			// 사용자정의테이블형식 파라미터 선언
			SqlParameter param = new SqlParameter("@EZCODE", SqlDbType.Structured)
			{
				TypeName = "EZCODE"
			,	Value = dtCode
			};

			// 최근데이터 가져오기
			DBMgr dbm = new DBMgr
			{
				Procedure = "PS_CZ_MA_FREQUENCY_ANALYSIS_LAST_DATA"
			};
			dbm.AddParameter("@CODENAME", codeName);
			dbm.AddParameter(param);			
			DataTable dtLast = dbm.GetDataTable();

			// ********** 최종 조인된 테이블 생성
			// 구조만 복사해서 관련 컬럼 추가
			DataTable dtResult = dtItem.Clone();
			dtResult.Columns.Add("LAST_UM_KR_P"	, typeof(decimal));
			dtResult.Columns.Add("LAST_VENDOR"	, typeof(string));

			// 조인 쿼리
			var q2 = from f in dtItem.AsEnumerable()
					 join j in dtLast.AsEnumerable() on new { key1 = f.Field<string>(codeName) } equals new { key1 = j.Field<string>(codeName) }
					 into o
					 from r in o.DefaultIfEmpty()
					 select dtResult.LoadDataRow(f.ItemArray.Concat(new object[] {
						r?["LAST_UM_KR_P"]
					 ,	r?["LAST_VENDOR"] }).ToArray(), false);

			// 쿼리 실행 및 커밋
			q2.Count();
			dtResult.AcceptChanges();

			return dtResult;
		}

		

		private DBParameters GetSearchCond(string mode)
		{
			int targetCode;

			if (rdo적용대상1.Checked)
				targetCode = 1;
			else if (rdo적용대상2.Checked)
				targetCode = 2;
			else
				targetCode = 3;

			DBParameters dbp = new DBParameters();
			dbp.Add("@CD_COMPANY"	, ctb회사코드.CodeValue);	
			dbp.Add("@DT"			, cbo일자.SelectedValue);
			dbp.Add("@DT_F"			, dtp일자.StartDateToString);
			dbp.Add("@DT_T"			, dtp일자.EndDateToString);

			dbp.Add("@CD_PARTNER"	, ctb매출처.CodeValue);
			dbp.Add("@CD_VENDOR"	, cbm매입처.QueryWhereIn_WithValueMember);
			dbp.Add("@NO_IMO"		, ctx호선번호.CodeValue);

			dbp.Add("@CLS_L"		, ctb대분류.CodeValue);
			dbp.Add("@CLS_M"		, ctb중분류.CodeValue);
			
			dbp.Add("@CD_TARGET"	, targetCode);
			dbp.Add("@CD_REPEAT"	, rdo조회대상1.Checked ? 1 : 2);

			//dbp.Add("@EXCEPT_NB"	, chk신조.Checked ? "Y" : "N");
			dbp.Add("@EXCEPT_NB"	, chk신조.Checked ? "Y" : "N");

			if (mode == "H")
			{
				dbp.Add("@CHK_" + chkU코드.GetCheckedControl().GetTag(), "Y");
				dbp.Add("@CNT_REPEAT", cur건수.DecimalValue);
			}

			return dbp;
		}

		private SqlParameter[] GetSearchCond2()
		{		
			List<SqlParameter> sp = new List<SqlParameter>()
			{
				new SqlParameter("@CD_COMPANY"      , ctb회사코드.CodeValue)
			,	new SqlParameter("@DT"              , cbo일자.SelectedValue)
			,	new SqlParameter("@DT_F"            , dtp일자.StartDateToString)
			,	new SqlParameter("@DT_T"            , dtp일자.EndDateToString)
			
			,	new SqlParameter("@CD_PARTNER"      , ctb매출처.CodeValue)
			,	new SqlParameter("@CD_VENDOR"       , cbm매입처.QueryWhereIn_WithValueMember)
			,	new SqlParameter("@NO_IMO"          , ctx호선번호.CodeValue)
			
			,	new SqlParameter("@CLS_L"           , ctb대분류.CodeValue)
			,	new SqlParameter("@CLS_M"           , ctb중분류.CodeValue)
			
			,	new SqlParameter("@CD_TARGET"       , rdo적용대상1.GetCheckedControl().GetTag())
			,	new SqlParameter("@CD_REPEAT"       , rdo조회대상1.GetCheckedControl().GetTag())
			
			,	new SqlParameter("@EXCEPT_NB"		, chk신조.Checked ? "Y" : "N")
			,	new SqlParameter("@EXCEPT_SERIES"	, chk시리즈.Checked ? "Y" : "N")
			,	new SqlParameter("@EXCEPT_DUP"		, chk중복.Checked ? "Y" : "N")
			,	new SqlParameter("@EXCEPT_OUTLIER"	, chk이상치.Checked ? "Y" : "N")
			};

			return sp.ToArray();
		}



		private void ToggleColumn()
		{
			grd헤드.Redraw = false;

			grd헤드.Cols["CD_ITEM_PARTNER"].Visible = false;
			grd헤드.Cols["CD_ITEM"].Visible = false;
			grd헤드.Cols["UCODE"].Visible = false;

			if (chk품목코드.Checked)	grd헤드.Cols["CD_ITEM_PARTNER"].Visible = true;
			if (chk재고코드.Checked)	grd헤드.Cols["CD_ITEM"].Visible = true;
			if (chkU코드.Checked)	grd헤드.Cols["UCODE"].Visible = true;
			
			if (grd헤드.Cols.Contains("KCODE"))
			{
				grd헤드.Cols["KCODE"].Visible = false;

				if (chkK코드.Checked)
					grd헤드.Cols["KCODE"].Visible = true;
			}

			grd헤드.Redraw = true;
		}

		#endregion
	}
}

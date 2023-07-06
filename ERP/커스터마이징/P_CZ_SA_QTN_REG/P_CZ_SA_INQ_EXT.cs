using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using DzHelpFormLib;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using System.Net;
using System.Reflection;
using Duzon.Common.Controls;
using DX;

namespace cz
{
	public partial class P_CZ_SA_INQ_EXT : PageBase
	{
		string CompanyCode;

		#region ===================================================================================================== Property

		private string FileNumber
		{
			get
			{
				return ((P_CZ_SA_QTN_REG)this.Parent.GetContainerControl()).파일번호;
			}
		}

		private int HistoryNumber
		{
			get
			{
				return ((P_CZ_SA_QTN_REG)this.Parent.GetContainerControl()).차수;
			}
		}

		public string PartnerCode
		{
			get
			{
				return ((P_CZ_SA_QTN_REG)this.Parent.GetContainerControl()).매출처코드;
			}

		}

		public string ImoNumber
		{
			get
			{
				return ((P_CZ_SA_QTN_REG)this.Parent.GetContainerControl()).Imo번호;
			}
		}
	
		public FlexGrid grdItem
		{
			get
			{
				return grd재고;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_SA_INQ_EXT()
		{
			//StartUp.Certify(this);
			CompanyCode = LoginInfo.CompanyCode;
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
			// 콤보박스 관련
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			// 단가기준
			dt.Rows.Add("CR_EXT", "CONTRACT", "단가계약");
			dt.Rows.Add("CR_EXT", "RECENT"	, "최근견적");
			dt.Rows.Add("CR_EXT", "PROFIT"	, "최고이윤율");
			dt.Rows.Add("CR_EXT", "PRICE"	, "최고단가");

			cbo단가기준1.DataBind(dt.Select("TYPE = 'CR_EXT'").CopyToDataTable(), false);
			cbo단가기준1.SelectedIndex = 0;

			dt.Rows.Add("CR_PEROID", 0 , "전체");
			dt.Rows.Add("CR_PEROID", 3 , "3개월");
			dt.Rows.Add("CR_PEROID", 6 , "6개월");
			dt.Rows.Add("CR_PEROID", 12, "12개월");
			dt.Rows.Add("CR_PEROID", 24, "24개월");

			cbo단가기준2.DataBind(dt.Select("TYPE = 'CR_PEROID'").CopyToDataTable(), false);
            cbo단가기준2.SelectedValue = 12;

			// 검색
			dt.Rows.Add("SEARCH", "C", "단가계약");
			dt.Rows.Add("SEARCH", "R", "기실적");

			cbo검색.DataSource = dt.Select("TYPE = 'SEARCH'").CopyToDataTable();
			cbo검색.SelectedIndex = 0;
			
			// 파일구분 동적 추가
			string query = @"
SELECT
	  CD_SYSDEF
	, CASE WHEN ISNULL(CD_FLAG2, '') != 'GS' THEN '' ELSE 'GS' END	AS IS_GS
FROM MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + CompanyCode + @"'
	AND CD_FIELD = 'CZ_SA00023'
	AND CD_FLAG1 = 'WF'
	AND ISNULL(CD_FLAG2, '') != 'CLAIM'
	AND CD_SYSDEF != 'PT'
	AND CD_SYSDEF != 'TE'

UNION ALL

SELECT 
	  'ZB'
	, 'ALL'

ORDER BY CD_SYSDEF";

			DataTable dtPrefix = DBMgr.GetDataTable(query);

			for (int i = 0; i < dtPrefix.Rows.Count; i++)
			{
				string flag = "GS";	// 선용의 경우는 "GS";

				CheckBoxExt chk = new CheckBoxExt();
				chk.Checked = (dtPrefix.Rows[i]["IS_GS"].ToString() == flag) ? true : false;
				chk.Text = dtPrefix.Rows[i]["CD_SYSDEF"].ToString();
				chk.Width = 41;
				chk.Left = 94 + (i * 51);
				chk.Top = 0;

				pnlFilePrefix.Controls.Add(chk);
			}

			// 부모-자식 그리드
			grd라인.DetailGrids = new FlexGrid[] { grd재고, grd실적 };

			// 포커스용
			tbxFocus.Left = -500;
		}

		private void InitGrid()
		{
			DataTable dtYn = new DataTable();
			dtYn.Columns.Add("CODE");
			dtYn.Columns.Add("NAME");
			dtYn.Rows.Add("Y", DD("확정"));
			dtYn.Rows.Add("N", "");

			// ---------------------------------------------------------------------------------------------------- Head
			// ---------- 컬럼 추가
			grd라인.BeginSetting(2, 1, false);

			grd라인.SetCol("CHK"			    , "S"			, 30	, true	, CheckTypeEnum.Y_N);
			grd라인.SetCol("YN_EXT"			, "확정"			, 40);

			// 견적서
			grd라인.SetCol("NO_FILE"		    , "파일번호"		, false);
			grd라인.SetCol("NO_LINE"		    , "항번"			, false);
			grd라인.SetCol("NO_DSP"			, "순번"			, 40);
			grd라인.SetCol("NM_SUBJECT"		, "주제"			, false);
			grd라인.SetCol("CD_ITEM_PARTNER", "품목코드"		, 70);		// TEST너비:100, LIVE너비:70
			grd라인.SetCol("NM_ITEM_PARTNER", "품목명"		, 250);

			// 단가계약 필드
            grd라인.SetCol("CD_ITEM_OLD"		, "재고코드INQ"	, false);
			grd라인.SetCol("CD_ITEM"		    , "재고코드"		, 80);
			grd라인.SetCol("CD_ITEM_1"		, "재고코드1"	, false);
			grd라인.SetCol("CD_ITEM_2"		, "재고코드2"	, false);
			grd라인.SetCol("CNT_MEMBER"		, "그룹"			, 45);
			grd라인.SetCol("GRP_MFG"		    , "제품군"		, 80);
			grd라인.SetCol("NM_ITEM"		    , "재고명"		, 200);
			grd라인.SetCol("DC_OFFER"		, "오퍼(비고)"	, 100);
			grd라인.SetCol("FG_UM"			, "유형"			, 45);

			// 기실적 필드
			grd라인.SetCol("CD_ITEM_PARTNER_EXT", "실적코드"	, 70);
			grd라인.SetCol("NM_ITEM_PARTNER_EXT", "실적명"	, 250);

			// 공통
			grd라인.SetCol("UNIT"			, "단위(INQ)"	, false);
			grd라인.SetCol("UNIT_EXT"		, "단위"			, 45	, true);
			grd라인.SetCol("QT"				, "수량"			, 50	, false	, typeof(decimal), FormatTpType.QUANTITY);		
	
			// 매입처
			grd라인.SetCol("CD_SUPPLIER"	    , "매입처코드"	, false);
			grd라인.SetCol("LN_SUPPLIER"	    , "매입처"		, 180);

			// 재고수량
			grd라인.SetCol("QT_AVL"			, "가용"			, 45	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd라인.SetCol("QT_NONGR_AVL"	, "미입고"		, 45	, false	, typeof(decimal), FormatTpType.QUANTITY);

			// 매입단가
			grd라인.SetCol("UM_EX_E"		    , "외화"			, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_KR_E"		    , "원화(￦)"		, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("RT_DC_P"		    , "매입DC\n(%)"	, 50	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_EX_P"		    , "외화"			, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_KR_P"		    , "원화(￦)"		, 80	, false	, typeof(decimal), FormatTpType.MONEY);

			// 매출단가
			grd라인.SetCol("RT_PROFIT"	    , "이윤\n(%)"	, 50	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_EX_Q"		    , "외화"			, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_KR_Q"		    , "원화"			, 80	, false	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("RT_DC"		    , "DC\n(%)"		, 50	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_EX_S"		    , "외화"			, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_KR_S"		    , "원화"			, 80	, false	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("RT_MARGIN"	    , "최종\n(%)"	, 50	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			// 기타
			grd라인.SetCol("LT"			    , "납기"			, 40	, false	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("NO_FILE_EXT"	    , "실적파일번호"	, 80);
			grd라인.SetCol("NO_LINE_EXT"	    , "실적항번"		, false);
			grd라인.SetCol("YN_SO"		    , "수주"			, 40);

			// ---------- 헤더 병합
			grd라인[0, grd라인.Cols["QT_AVL"].Index] = "재고수량";
			grd라인[0, grd라인.Cols["QT_NONGR_AVL"].Index] = "재고수량";

			grd라인[0, grd라인.Cols["UM_EX_E"].Index] = "매입견적단가";
			grd라인[0, grd라인.Cols["UM_KR_E"].Index] = "매입견적단가";

			grd라인[0, grd라인.Cols["UM_EX_P"].Index] = "매입단가";
			grd라인[0, grd라인.Cols["UM_KR_P"].Index] = "매입단가";

			grd라인[0, grd라인.Cols["UM_EX_Q"].Index] = "매출견적단가";
			grd라인[0, grd라인.Cols["UM_KR_Q"].Index] = "매출견적단가";

			grd라인[0, grd라인.Cols["UM_EX_S"].Index] = "매출단가";
			grd라인[0, grd라인.Cols["UM_KR_S"].Index] = "매출단가";

			// ---------- 컬럼 속성
			grd라인.Cols["YN_EXT"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["NO_DSP"].Format = "####.##";
			grd라인.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["CD_ITEM"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["CNT_MEMBER"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["FG_UM"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["YN_SO"].TextAlign = TextAlignEnum.CenterCenter;
			grd라인.Cols["NO_FILE_EXT"].TextAlign = TextAlignEnum.CenterCenter;

			grd라인.SetDataMap("YN_EXT", dtYn, "CODE", "NAME");
			grd라인.SetDataMap("GRP_MFG", GetDb.Code("MA_B000066"), "CODE", "NAME");
			grd라인.SetDataMap("UNIT_EXT", GetDb.Code("MA_B000004"), "CODE", "NAME");
			grd라인.SetDataMap("FG_UM", GetDb.Code("SA_B000021"), "CODE", "NAME");

			// ---------- 마무리
			grd라인.SetDefault("19.07.19.01", SumPositionEnum.None);
			grd라인.SetEditColumn("CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "UNIT_EXT", "DC_OFFER", "LT");
			grd라인.Styles.Add("ACTIVE").ForeColor = Color.Blue;
			grd라인.LoadUserCache("P_CZ_SA_QTN_REG_SINQ_EXT");

			// 다양한 검색방법을 위해 바인딩 한번 기본으로 해준다
			DataTable dtCol = new DataTable();
			for (int i = 0; i < grd라인.Cols.Count; i++) dtCol.Columns.Add(grd라인.Cols[i].Name);			
			grd라인.Binding = dtCol;

			// ---------------------------------------------------------------------------------------------------- Line
			InitLineGrid(grd재고);			
			InitLineGrid(grd재고검색);
			InitLineGrid(grd실적);
			InitLineGrid(grd실적검색);
		}

		private void InitLineGrid(FlexGrid grid)
		{
			// ---------- 컬럼 추가
			grid.BeginSetting(2, 1, false);

			grid.SetCol("NO_FILTER"		, "필터"			, false);
			grid.SetCol("RN"			, "순위"			, 40);
			
			if (grid == grd재고 || grid == grd재고검색)
			{
				// 재고
				grid.SetCol("CD_ITEM"		, "재고코드"		, 70);
				grid.SetCol("GRP_MFG"		, "제품군"		, 80);
				grid.SetCol("NM_ITEM"		, "재고명"		, 200);
				grid.SetCol("DC_OFFER"		, "오퍼(비고)"	, 100);		// 재고마스터의 오퍼
			}
			else
			{
				// 기실적
				grid.SetCol("NO_FILE_EXT"	, "파일번호"		, 90);
				grid.SetCol("NO_LINE_EXT"	, "항번"			, false);
				grid.SetCol("LN_PARTNER"	, "매출처"		, 230);
				grid.SetCol("NO_DSP_EXT"	, "순번"			, 40);
				grid.SetCol("NM_SUBJECT_EXT" , "주제"		, false);
				grid.SetCol("CD_ITEM_PARTNER_EXT", "품목코드"		, 70);		// TEST너비:100,LIVE너비:70
				grid.SetCol("NM_ITEM_PARTNER_EXT", "품목명"		, 200);
				grid.SetCol("CD_UNIQ_PARTNER_EXT", "선사코드"		, 80);
				grid.SetCol("DC_OFFER"		, "오퍼(비고)"	, 100);		// 견적의 오퍼
				grid.SetCol("CD_ITEM"		, "재고코드"		, 80);
				grid.SetCol("GRP_MFG"		, "제품군"		, false);
				grid.SetCol("NM_ITEM"		, "재고명"		, 200);				
			}
		
			grid.SetCol("UNIT_EXT"		, "단위"			, 45);
			grid.SetCol("CD_SUPPLIER"	, "매입처코드"	, false);
			grid.SetCol("LN_SUPPLIER"	, "매입처"		, 180);			// 선용은 매입처명도 표시

			if (grid == grd재고 || grid == grd재고검색)
			{
				grid.SetCol("FG_UM"			, "유형"			, 45);
			}

			// 재고수량
			grid.SetCol("QT_AVL"		, "가용"			, 50	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grid.SetCol("QT_NONGR_AVL"	, "미입고"		, 50	, false	, typeof(decimal), FormatTpType.QUANTITY);

			// 매입단가
			grid.SetCol("CD_EXCH_P"		, "매입통화"		, false);
			grid.SetCol("RT_EXCH_P"		, "매입환율"		, false);
			grid.SetCol("UM_EX_E"		, "외화"			, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("UM_KR_E"		, "원화(￦)"		, 80	, false	, typeof(decimal), FormatTpType.MONEY);
			grid.SetCol("RT_DC_P"		, "매입DC\n(%)"	, 50	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("UM_EX_P"		, "외화"			, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("UM_KR_P"		, "원화(￦)"		, 80	, false	, typeof(decimal), FormatTpType.MONEY);

			// 매출단가
			grid.SetCol("CD_EXCH_S"		, "매출통화"		, false);
			grid.SetCol("RT_EXCH_S"		, "매출환율"		, false);
			grid.SetCol("RT_PROFIT"		, "이윤\n(%)"	, 50	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("UM_EX_Q"		, "외화"			, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("UM_KR_Q"		, "원화(￦)"		, 80	, false	, typeof(decimal), FormatTpType.MONEY);
			grid.SetCol("RT_DC"			, "DC\n(%)"		, 50	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("UM_EX_S"		, "외화"			, 80	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grid.SetCol("UM_KR_S"		, "원화(￦)"		, 80	, false	, typeof(decimal), FormatTpType.MONEY);
			grid.SetCol("RT_MARGIN"		, "최종\n(%)"	, 50	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			// 납기 및 비고
			grid.SetCol("LT"			, "납기"			, 40	, false	, typeof(decimal), FormatTpType.MONEY);

			if (grid == grd재고 || grid == grd재고검색)
			{
				grid.SetCol("DC_RMK"		, "비고"			, 300);		// 공장품목등록의 비고
			}
			else
			{
				grid.SetCol("YN_SO"			, "수주"			, 40);	
			}

			// ---------- 헤더 병합
			if (grid.Cols.Contains("QT_AVL"))
			{
				grid[0, grid.Cols["QT_AVL"].Index] = "재고수량";
				grid[0, grid.Cols["QT_NONGR_AVL"].Index] = "재고수량";
			}

			grid[0, grid.Cols["UM_EX_E"].Index] = "매입견적단가";
			grid[0, grid.Cols["UM_KR_E"].Index] = "매입견적단가";

			grid[0, grid.Cols["UM_EX_P"].Index] = "매입단가";
			grid[0, grid.Cols["UM_KR_P"].Index] = "매입단가";

			grid[0, grid.Cols["UM_EX_Q"].Index] = "매출견적단가";
			grid[0, grid.Cols["UM_KR_Q"].Index] = "매출견적단가";

			grid[0, grid.Cols["UM_EX_S"].Index] = "매출단가";
			grid[0, grid.Cols["UM_KR_S"].Index] = "매출단가";

			// ---------- 컬럼 속성
			grid.Cols["RN"].TextAlign = TextAlignEnum.CenterCenter;

			if (grid.Cols.Contains("NO_FILE_EXT"))
			{
				grid.Cols["NO_FILE_EXT"].TextAlign = TextAlignEnum.CenterCenter;
				grid.Cols["NO_DSP_EXT"].Format = "####.##";
				grid.Cols["NO_DSP_EXT"].TextAlign = TextAlignEnum.CenterCenter;
			}

			grid.SetDataMap("GRP_MFG", GetDb.Code("MA_B000066"), "CODE", "NAME");
			grid.Cols["UNIT_EXT"].TextAlign = TextAlignEnum.CenterCenter;			
			grid.SetDataMap("UNIT_EXT", GetDb.Code("MA_B000004"), "CODE", "NAME");

			if (grid.Cols.Contains("FG_UM"))
			{
				grid.Cols["FG_UM"].TextAlign = TextAlignEnum.CenterCenter;
				grid.SetDataMap("FG_UM", GetDb.Code("SA_B000021"), "CODE", "NAME");
			}

			// ---------- 마무리
			grid.SetDefault("18.07.24.01", SumPositionEnum.None);
			grid.LoadUserCache("P_CZ_SA_INQ_EXT");
		}

		private void InitEvent()
		{
			btnConfirm.Click += new EventHandler(btnConfirm_Click);
			btnCancel.Click += new EventHandler(btnCancel_Click);			
			btn매입처지정.Click += new EventHandler(btn매입처지정_Click);
			btnApplyPrice.Click += new EventHandler(btnApplyPrice_Click);

			tbx검색.KeyDown += new KeyEventHandler(tbxSearch_KeyDown);
			btnSearch.Click += new EventHandler(btnSearch_Click);

			grd라인.BeforeDoubleClick += new BeforeMouseDownEventHandler(flexGrid_BeforeDoubleClick);
			grd재고.BeforeDoubleClick += new BeforeMouseDownEventHandler(flexGrid_BeforeDoubleClick);
			grd재고검색.BeforeDoubleClick += new BeforeMouseDownEventHandler(flexGrid_BeforeDoubleClick);
			grd실적.BeforeDoubleClick += new BeforeMouseDownEventHandler(flexGrid_BeforeDoubleClick);
			grd실적검색.BeforeDoubleClick += new BeforeMouseDownEventHandler(flexGrid_BeforeDoubleClick);

			grd라인.DoubleClick += new EventHandler(grd헤드_DoubleClick);			
			grd라인.AfterRowChange += new RangeEventHandler(grdHead_AfterRowChange);

			
			KeyDown += P_CZ_SA_INQ_EXT_KeyDown;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.F3)
				btnApplyPrice_Click(null, null);

			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void P_CZ_SA_INQ_EXT_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F3)
			{
				btnApplyPrice_Click(null, null);
			}
		}

		protected override void InitPaint()
		{
			
		}

		public void Clear()
		{
			// 검색원그리드
			cbo단가기준1.Clear(true);
			cbo단가기준2.Clear(true);
			rdo단가대상1.Checked = true;
			cbo가격조건.Clear(false);
			rdo복사옵션1.Checked = true;			
			cbo검색.Clear(true);
			tbx검색.Text = "";
			rdo확정상태N.Checked = true;

			// 그리드
			grd라인.Clear(false);
			grd재고.Clear(false);
			grd재고검색.Clear(false);
			grd실적.Clear(false);
			grd실적검색.Clear(false);
		}

		public void 사용(bool enabled)
		{
			pnl버튼.Editable(enabled);
			one검색.Editable(enabled);
			grd라인.Editable(enabled);
		}

		#endregion
		
		#region ==================================================================================================== 컨트롤이벤트

		private void tbxSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				btnSearch_Click(null, null);
			}
		}

		#endregion

		#region ==================================================================================================== 버튼이벤트

		private void btn매입처지정_Click(object sender, EventArgs e)
		{
			DataRow[] item = grd라인.DataTable.Select("CHK = 'Y'");

			if (item.Length == 0)
			{
				ShowMessage(공통메세지.선택된자료가없습니다);
				return;
			}

			// 매입처 선택 팝업
			H_CZ_SUPPLIER f = new H_CZ_SUPPLIER("EXT");

			if (f.ShowDialog() != DialogResult.OK)
				return;

			if (f.Result.Rows.Count == 0)
			{
				ShowMessage("선택된 매입처가 없습니다.");
				return;
			}

			// 필수값 추가
			f.Result.Columns.Add("NO_FILE", typeof(string), "'" + FileNumber + "'").SetOrdinal(0);
			f.Result.Columns.Add("NO_HST", typeof(string), "'" + HistoryNumber + "'").SetOrdinal(1);

			try
			{
				DBMgr.ExecuteNonQuery("PX_CZ_SA_INQ_EXT_PINQ", GetTo.Xml(f.Result), GetTo.Xml(item));
				ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			}
		}

		private void btnConfirm_Click(object sender, EventArgs e)
		{
			DataRow[] drChecked = grd라인.DataTable.Select("CHK = 'Y'");

			if (drChecked.Length > 0)
			{
				try
				{
					DBMgr db = new DBMgr();
					db.Procedure = "PX_CZ_SA_INQ_EXT";
					db.DebugMode = DebugMode.Popup;
					db.AddParameter("@MODE"	 , rdo복사옵션1.Checked ? "P" : "PS");
					db.AddParameter("@XML"	 , GetTo.Xml(drChecked.CopyToDataTable()));
					db.AddParameter("@NO_REF", ((cbo단가기준1.GetValue() == "CONTRACT") ? "단가표-" : "기실적-") + Util.GetToday());
					db.ExecuteNonQuery();

					// DX테이블 저장
					키워드.견적저장(drChecked.ToDataTable());

					ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
					Search();
				}
				catch (Exception ex)
				{
					Util.ShowMessage(Util.GetErrorMessage(ex.Message));
				}
			}
			else
			{
				ShowMessage(공통메세지.선택된자료가없습니다);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DataRow[] drChecked = grd라인.DataTable.Select("CHK = 'Y'");

			if (drChecked.Length > 0)
			{
				try
				{
					DBMgr.ExecuteNonQuery("PX_CZ_SA_INQ_EXT", "C", GetTo.Xml(drChecked.CopyToDataTable()));

					ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
					Search();
				}
				catch (Exception ex)
				{
					Util.ShowMessage(Util.GetErrorMessage(ex.Message));
				}
			}
			else
			{
				ShowMessage(공통메세지.선택된자료가없습니다);
			}
		}

		private void btnApplyPrice_Click(object sender, EventArgs e)
		{
			// 그리드 선택
			FlexGrid grdLine;
			
			if (tabLine.SelectedTab == tabContract)
				grdLine = grd재고;
			else if (tabLine.SelectedTab == tabSearchC)
				grdLine = grd재고검색;
			else if (tabLine.SelectedTab == tabRecord)
				grdLine = grd실적;
			else
				grdLine = grd실적검색;
		
			// 선택 처리
			grd라인["CHK"]		 = "Y";

			// 단가 적용
			for (int i = 0; i < grd라인.Cols.Count; i++)
			{
				string colName = grd라인.Cols[i].Name;

				if (colName == "" || !grdLine.Cols.Contains(colName)) continue;

				// 단위의 경우 없으면 적용 안함
				if (colName == "UNIT_EXT" && GetTo.String(grdLine[colName]) == "") continue;

				// 적용
				grd라인[colName] = (GetTo.String(grdLine[colName]) == "") ? DBNull.Value : grdLine[colName];
			}
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void flexGrid_BeforeDoubleClick(object sender, BeforeMouseDownEventArgs e)
		{
			FlexGrid o = (FlexGrid)sender;
			int col = o.MouseCol;

			if (!o.HasNormalRow)
				return;

			if (col <= 0)
			{
				o.AutoRowSize();
				e.Cancel = true;
			}
		}

		private void grd헤드_DoubleClick(object sender, EventArgs e)
		{		
			// 헤더클릭
			if (grd라인.MouseRow == 0 && grd라인.MouseCol > 0)
			{
				SetGridStyle();
			}
		}

		private void SetGridStyle()
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				// 가용재고나 미입고가 있는 경우 색상 표시
				if (GetTo.Decimal(grd라인[i, "QT_AVL"]) > 0 || GetTo.Decimal(grd라인[i, "QT_NONGR_AVL"]) > 0)
					grd라인.Rows[i].Style = grd라인.Styles["ACTIVE"];
				else
					grd라인.Rows[i].Style = null;

				// INQ 단위와 다른 항목 색상 표시
				if (GetTo.String(grd라인[i, "UNIT"]) != GetTo.String(grd라인[i, "UNIT_EXT"]))
					SetGrid.CellRed(grd라인, i, grd라인.Cols["UNIT_EXT"].Index);

				// 재고코드1(임파검색)과 재고코드2(키워드검색)가 다른 경우 색상 표시
				if (GetTo.String(grd라인[i, "CD_ITEM_1"]) != ""
					&& GetTo.String(grd라인[i, "CD_ITEM_2"]) != ""
					&& GetTo.String(grd라인[i, "CD_ITEM_1"]) != GetTo.String(grd라인[i, "CD_ITEM_2"]))
					SetGrid.CellRed(grd라인, i, grd라인.Cols["CD_ITEM"].Index);

				// 선사품목코드와 재고코드가 다른 경우 색상 표시
				string itemCode = grd라인[i, "CD_ITEM_PARTNER"].ToString();
				string stockCode = grd라인[i, "CD_ITEM"].ToString();
				bool isImpa = false;

				if (stockCode.Length > 6)
					stockCode = stockCode.Left(6);

				if (itemCode.Length == 6 && GetTo.IsInt(itemCode))
					isImpa = true;
				
				if (isImpa && stockCode != "" && itemCode != stockCode)
					SetGrid.CellRed(grd라인, i, grd라인.Cols["CD_ITEM"].Index);
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public void Search()
		{
			// 디버그팝업 여부
			bool popupDebug = false;

			if (Control.ModifierKeys == Keys.Control)
				popupDebug = true;

			try
			{
				MsgControl.ShowMsg(DD("조회중입니다."));

				// 확정상태
				string ynExt = "";
				if (rdo확정상태N.Checked) ynExt = "N";
				if (rdo확정상태Y.Checked) ynExt = "Y";

				DBMgr db = new DBMgr();

				if (popupDebug)
					db.DebugMode = DebugMode.Popup;
				else
					db.DebugMode = DebugMode.None;

				if (cbo단가기준1.GetValue() == "CONTRACT")
				{
					db.Procedure = "PS_CZ_SA_INQ_EXT_H_CONTRACT";
					db.AddParameter("@CD_COMPANY"	 , CompanyCode);
					db.AddParameter("@NO_FILE"		 , FileNumber);
					db.AddParameter("@YN_EXT"		 , ynExt);
					db.AddParameter("@DC_PRICE_TERMS", cbo가격조건.GetValue());

					tabLine.SelectedTab = tabContract;
				}
				else
				{
					db.Procedure = "PS_CZ_SA_INQ_EXT_H_RECORD";
					db.AddParameter("@CD_COMPANY"	, CompanyCode);
					db.AddParameter("@NO_FILE"		, FileNumber);
					db.AddParameter("@YN_EXT"		, ynExt);
					db.AddParameter("@CR_PERIOD"	, cbo단가기준2.SelectedValue);
					db.AddParameter("@CR_TARGET"	, rdo단가대상1.Checked ? 1 : 2);
					db.AddParameter("@CR_PREFIX"	, GetFilePrefix());
					db.AddParameter("@YN_BUYER"		, chkBuyer.Checked ? PartnerCode : "");
					db.AddParameter("@YN_HULL"		, chkHull.Checked ? ImoNumber : "");
					db.AddParameter("@YN_ORDER"		, chkOrder.Checked ? "Y" : "N");

					tabLine.SelectedTab = tabRecord;
				}

				// 쿼리 실행 및 Linq 조인
				DataView dvHead = new DataView(db.GetDataTable());
				dvHead.Sort = "NO_DSP ASC";
				DataTable dtHead = JoinStockQuantity(dvHead.ToTable());
				Util.CalculateRate(dtHead, "YN_EXT = 'N'");

				// 컬럼 추가
				dtHead.Columns.Add("CHK", typeof(string));

				foreach (DataRow dr in dtHead.Rows)
				{
					if (cbo단가기준1.GetValue() == "CONTRACT")
					{
						if (GetTo.String(dr["YN_EXT"]) == "N" && GetTo.String(dr["CD_ITEM"]) != "" && GetTo.String(dr["CD_SUPPLIER"]) != "")
							dr["CHK"] = "Y";
						else
							dr["CHK"] = "N";
					}
					else
					{
						if (GetTo.String(dr["YN_EXT"]) == "N" && GetTo.String(dr["NO_FILE_EXT"]) != "")
							dr["CHK"] = "Y";
						else
							dr["CHK"] = "N";
					}
				}

				// 더미컬럼 추가
				if (!dtHead.Columns.Contains("CD_ITEM_PARTNER_EXT")) dtHead.Columns.Add("CD_ITEM_PARTNER_EXT", typeof(string));
				if (!dtHead.Columns.Contains("NM_ITEM_PARTNER_EXT")) dtHead.Columns.Add("NM_ITEM_PARTNER_EXT", typeof(string));
				if (!dtHead.Columns.Contains("NO_FILE_EXT")) dtHead.Columns.Add("NO_FILE_EXT", typeof(string));
				if (!dtHead.Columns.Contains("NO_LINE_EXT")) dtHead.Columns.Add("NO_LINE_EXT", typeof(int));
				if (!dtHead.Columns.Contains("YN_SO")) dtHead.Columns.Add("YN_SO", typeof(string));
				if (!dtHead.Columns.Contains("FG_UM")) dtHead.Columns.Add("FG_UM", typeof(string));

				// 바인딩
				grd라인.Binding = dtHead;
				grd라인.AutoSizeRows();
				grd라인.Rows[0].Height = 28;
				grd라인.Rows[1].Height = 28;
				SetGridStyle();

				// 단가기준에 따라 Head 그리드 컬럼조정
				grd라인.Cols["CNT_MEMBER"].Visible = false;
				grd라인.Cols["GRP_MFG"].Visible = false;
				grd라인.Cols["NM_ITEM"].Visible = false;
				grd라인.Cols["FG_UM"].Visible = false;

				grd라인.Cols["CD_ITEM_PARTNER_EXT"].Visible = false;
				grd라인.Cols["NM_ITEM_PARTNER_EXT"].Visible = false;
				grd라인.Cols["NO_FILE_EXT"].Visible = false;

				if (cbo단가기준1.GetValue() == "CONTRACT")
				{
					grd라인.Cols["CNT_MEMBER"].Visible = true;
					grd라인.Cols["GRP_MFG"].Visible = true;
					grd라인.Cols["NM_ITEM"].Visible = true;					
				}
				else
				{
					grd라인.Cols["CD_ITEM_PARTNER_EXT"].Visible = true;
					grd라인.Cols["NM_ITEM_PARTNER_EXT"].Visible = true;
					grd라인.Cols["NO_FILE_EXT"].Visible = true;
				}
			}
			catch (Exception ex)
			{				
				Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			}
			finally
			{
				MsgControl.CloseMsg();
			}
		}

		private void grdHead_AfterRowChange(object sender, RangeEventArgs e)
		{
			string filter = "NO_FILTER = " + grd라인["NO_LINE"];

			if (grd라인.DetailQueryNeed)
			{
				try
				{
					MsgControl.ShowMsg("조회중입니다.");
					tbxFocus.Focus();

					// 단가계약
					DBMgr dbContract = new DBMgr();
					dbContract.Procedure = "PS_CZ_SA_INQ_EXT_L_CONTRACT";
					dbContract.AddParameter("@CD_COMPANY"	, CompanyCode);
					dbContract.AddParameter("@NO_FILE"		, FileNumber);
					dbContract.AddParameter("@NO_LINE"		, grd라인["NO_LINE"]);

					DataView dvContract = new DataView(dbContract.GetDataTable());
					//dvContract.Sort = "RN ASC";
					DataTable dtContract = JoinStockQuantity(dvContract.ToTable());
					Util.CalculateRate(dtContract, "");

					// 기실적
					SQL sqlRecord = new SQL("PS_CZ_SA_INQ_EXT_L_RECORD", SQLType.Procedure, SQLDebug.Popup);
					sqlRecord.Parameter.Add2("@CD_COMPANY"	, CompanyCode);
					sqlRecord.Parameter.Add2("@NO_FILE"		, FileNumber);
					sqlRecord.Parameter.Add2("@NO_LINE"		, grd라인["NO_LINE"]);
					sqlRecord.Parameter.Add2("@CR_PERIOD"	, cbo단가기준2.SelectedValue);
					sqlRecord.Parameter.Add2("@CR_TARGET"	, rdo단가대상1.Checked ? 1 : 2);
					sqlRecord.Parameter.Add2("@CR_PREFIX"	, GetFilePrefix());
					sqlRecord.Parameter.Add2("@YN_BUYER"	, chkBuyer.Checked ? PartnerCode : "");
					sqlRecord.Parameter.Add2("@YN_HULL"	, chkHull.Checked ? ImoNumber : "");
					sqlRecord.Parameter.Add2("@YN_ORDER"	, chkOrder.Checked ? "Y" : "N");

					//DataTable dtR = dbRecord.GetDataTable();
					DataView dvRecord = new DataView(sqlRecord.GetDataTable());
					dvRecord.Sort = "RN ASC";
					DataTable dtRecord = JoinStockQuantity(dvRecord.ToTable());
					Util.CalculateRate(dtRecord, "");

					// 바인딩
					grd재고.BindingAdd(dtContract, filter);
					grd실적.BindingAdd(dtRecord, filter);
				}
				catch (Exception ex)
				{
					Util.ShowMessage(Util.GetErrorMessage(ex.Message));
				}
				finally
				{
					MsgControl.CloseMsg();
				}
			}
			else
			{
				grd재고.BindingAdd(null, filter);
				grd실적.BindingAdd(null, filter);
			}

			grd재고.AutoSizeRows();
			grd재고.Rows[0].Height = 28;
			grd재고.Rows[1].Height = 28;

			grd실적.AutoSizeRows();
			grd실적.Rows[0].Height = 28;
			grd실적.Rows[1].Height = 28;
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			if (grd라인.HasNormalRow && tbx검색.Text.Trim() != "")
			{
				try
				{
					MsgControl.ShowMsg("조회중입니다.");
					tbxFocus.Focus();

					if (cbo검색.GetValue() == "C")
					{
						DBMgr db = new DBMgr();
						db.Procedure = "PS_CZ_SA_INQ_EXT_L_CONTRACT";
						db.AddParameter("@CD_COMPANY", CompanyCode);
						db.AddParameter("@NO_FILE"	 , FileNumber);
						db.AddParameter("@SEARCH"	 , tbx검색.Text);

						DataTable dt = JoinStockQuantity(db.GetDataTable());
						Util.CalculateRate(dt, "");

						grd재고검색.Binding = dt;
						grd재고검색.AutoSizeRows();
						grd재고검색.Rows[0].Height = 28;
						grd재고검색.Rows[1].Height = 28;

						tabLine.SelectedTab = tabSearchC;
					}
					else
					{
						DBMgr db = new DBMgr();
						db.Procedure = "PS_CZ_SA_INQ_EXT_L_RECORD_GS";
						db.AddParameter("@CD_COMPANY"	, CompanyCode);
						db.AddParameter("@NO_FILE"		, FileNumber);
						db.AddParameter("@CR_PERIOD"	, cbo단가기준2.SelectedValue);
						db.AddParameter("@CR_TARGET"	, rdo단가대상1.Checked ? 1 : 2);
						db.AddParameter("@CR_PREFIX"	, GetFilePrefix());
						db.AddParameter("@YN_BUYER"		, chkBuyer.Checked ? PartnerCode : "");
						db.AddParameter("@YN_HULL"		, chkHull.Checked ? ImoNumber : "");
						db.AddParameter("@YN_ORDER"		, chkOrder.Checked ? "Y" : "N");
						db.AddParameter("@SEARCH"		, tbx검색.Text);

						DataTable dt = JoinStockQuantity(db.GetDataTable());
						Util.CalculateRate(dt, "");

						grd실적검색.Binding = dt;
						grd실적검색.AutoSizeRows();
						grd실적검색.Rows[0].Height = 28;
						grd실적검색.Rows[1].Height = 28;

						tabLine.SelectedTab = tabSearchR;
					}
				}
				catch (Exception ex)
				{
					Util.ShowMessage(Util.GetErrorMessage(ex.Message));
				}
				finally
				{
					MsgControl.CloseMsg();
				}
			}
		}

		private DataTable JoinStockQuantity(DataTable dtItem)
		{
			// YN_EXT 필드가 없는 경우 추가 (Line 테이블은 없음)
			if (!dtItem.Columns.Contains("YN_EXT"))
				dtItem.Columns.Add("YN_EXT", typeof(string), "'N'");

			// 재고 가져오기
			DataTable dtQuantity = DBMgr.GetDataTable("PS_CZ_MA_STOCK_QT", CompanyCode, GetTo.Xml(dtItem.DefaultView.ToTable(true, "CD_ITEM")));
			
			// 결과 테이블 생성 (구조만)
			DataTable dtResult = dtItem.Clone();
			dtResult.Columns.Add("QT_AVL", typeof(decimal));
			dtResult.Columns.Add("QT_NONGR_AVL", typeof(decimal));
		
			// 쿼리문
			var query = from itemList in dtItem.AsEnumerable()
						join quantity in dtQuantity.AsEnumerable()
							on new { key1 = itemList.Field<string>("CD_ITEM"), key2 = itemList.Field<string>("YN_EXT") } equals new { key1 = quantity.Field<string>("CD_ITEM"), key2 = "N" }
							into outer
						from result in outer.DefaultIfEmpty()
						select dtResult.LoadDataRow(itemList.ItemArray.Concat(new object[] {
			                  (result == null) ? null : result["QT_AVL"]
			                , (result == null) ? null : result["QT_NONGR_AVL"] }).ToArray<object>(), false);

			// 쿼리 실행
			query.Count();
			
			return dtResult;
		}

		private string GetFilePrefix()
		{
			string prefix = "";

			foreach (Control con in pnlFilePrefix.Controls)
			{
				if (con is CheckBoxExt)
				{
					CheckBoxExt chk = (CheckBoxExt)con;
					if (chk.Checked) prefix += ",'" + chk.Text + "'";
				}
			}

			if (prefix != "") prefix = prefix.Substring(1);

			return prefix;
		}

		#endregion

		#region ==================================================================================================== 종료

		public void Exit()
		{
			grd라인.SaveUserCache("P_CZ_SA_QTN_REG");
			grd재고.SaveUserCache("P_CZ_SA_QTN_REG");
		}

		#endregion
	}
}

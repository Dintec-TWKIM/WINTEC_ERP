using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DX;
using Duzon.Common.BpControls;
using System.Text.RegularExpressions;

namespace cz
{
	public partial class P_CZ_DX_PITEM_REG : PageBase
	{
		public P_CZ_DX_PITEM_REG()
		{
			InitializeComponent();
			this.SetConDefault();
		}

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			tbx검색.EnterSearch();
			tbx키워드.EnterSearch();

			//ctx대분류.CodeValue = "EQ";
			//ctx대분류.CodeName = "조선기자재";

			// 검색콤보 
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			dt.Rows.Add("검색", "SEARCH"		, "전체");
			dt.Rows.Add("검색", "CD_ITEM"	, "재고코드");
			dt.Rows.Add("검색", "NM_ITEM"	, "재고명");
			dt.Rows.Add("검색", "UCODE"		, "U코드");
			dt.Rows.Add("검색", "NO_STND"	, "도면번호");

			cbo검색.DataSource = dt.Select("TYPE = '검색'").CopyToDataTable();
			cbo검색.SelectedIndex = 1;


			dtp접수일자.StartDateToString = UT.Today(-365);
			dtp접수일자.EndDateToString = UT.Today();

			//grd라인.DetailGrids = new FlexGrid[] { grd사진 };


			btn메인키추가.Text = "";
			btn제외키추가.Text = "";
			btn서브키1추가.Text = "";
			btn서브키2추가.Text = "";

			btn메인키삭제.Text = "";
			btn제외키삭제.Text = "";
			btn서브키1삭제.Text = "";
			btn서브키2삭제.Text = "";

			btn메인키추가.Image = new Bitmap(아이콘.더하기_18x18, new Size(16, 16));
			btn제외키추가.Image = new Bitmap(아이콘.더하기_18x18, new Size(16, 16));
			btn서브키1추가.Image = new Bitmap(아이콘.더하기_18x18, new Size(16, 16));
			btn서브키2추가.Image = new Bitmap(아이콘.더하기_18x18, new Size(16, 16));

			btn메인키삭제.Image = new Bitmap(아이콘.빼기_18x18, new Size(16, 16));
			btn제외키삭제.Image = new Bitmap(아이콘.빼기_18x18, new Size(16, 16));
			btn서브키1삭제.Image = new Bitmap(아이콘.빼기_18x18, new Size(16, 16));
			btn서브키2삭제.Image = new Bitmap(아이콘.빼기_18x18, new Size(16, 16));

			MainGrids = new FlexGrid[] { grd라인, grd메인키, grd제외키, grd서브키1, grd서브키2, grd트레이닝1, grd트레이닝2 };
			grd라인.DetailGrids = new FlexGrid[] { grd메인키, grd제외키, grd서브키1, grd서브키2 };

			InitGrid();
			InitGrid_TRN(grd트레이닝1);
			InitGrid_TRN(grd트레이닝2);
			InitGrid_KEY(grd메인키, 4);
			InitGrid_KEY(grd제외키, 1);
			InitGrid_KEY(grd서브키1, 1);
			InitGrid_KEY(grd서브키2, 3);

			grd제외키.Cols["KEY1"].Caption = "제외";

			InitEvent();
		}

		protected override void InitPaint()
		{
			spc메인키.Panel1.BackColor = Color.Blue;
			spc메인키.Panel2.BackColor = Color.Red;
			spc서브키.Panel1.BackColor = Color.Green;
			spc서브키.Panel2.BackColor = Color.Blue;

			spc메인.SplitterDistance = spc메인.Width - 1320;
			spc메인키.SplitterDistance = spc메인키.Width - (180 + 80);
			spc서브키.SplitterDistance = 180 + 80;
			spc라인.SplitterDistance = spc라인.Height / 2;

			grd메인키.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
			grd제외키.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
			grd서브키1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
			grd서브키2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{
			// ********** 라인
			grd라인.BeginSetting(1, 1, false);
						
			grd라인.SetCol("CD_ITEM"			, "재고코드"		, 130);
			grd라인.SetCol("NM_ITEM"			, "재고명"		, 300);
			grd라인.SetCol("UCODE"			, "U코드"		, 110);
			grd라인.SetCol("UCODE2"			, "U코드2"		, 200);
			grd라인.SetCol("DC_RMK"			, "비고"			, 300);
			grd라인.SetCol("DC_RMK3"			, "비고2"		, 300);
			
			grd라인.SetDefault("20.12.08.04", SumPositionEnum.None);			
			grd라인.SetAlternateRow();
			grd라인.SetMalgunGothic();
			grd라인.Rows.DefaultSize = 41;

			// ********** 라인
			//grd트레이닝1.BeginSetting(1, 1, false);
						
			//grd트레이닝1.SetCol("NO_FILE"			, "파일번호"		, 90);
			//grd트레이닝1.SetCol("NO_LINE"			, "항번"			, false);
			//grd트레이닝1.SetCol("NO_DSP"			, "순번"			, 40);
			//grd트레이닝1.SetCol("NM_SUBJECT"		, "주제"			, 300);
			//grd트레이닝1.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 150);
			//grd트레이닝1.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 300);
			//grd트레이닝1.SetCol("LN_VENDOR"		, "매입처"		, 200);
			//grd트레이닝1.SetCol("CD_ITEM"			, "재고코드"		, false);
			//grd트레이닝1.SetCol("UM_KR_P"			, "원화단가"		, 75	, typeof(decimal), FormatTpType.MONEY);

			//grd트레이닝1.SetDefault("20.12.10.01", SumPositionEnum.None);
			//grd트레이닝1.SetAlternateRow();
			//grd트레이닝1.SetMalgunGothic();
			//grd트레이닝1.Rows.DefaultSize = 41;
		}

		private void InitGrid_TRN(FlexGrid flexGrid)
		{
			flexGrid.BeginSetting(1, 1, false);
						
			flexGrid.SetCol("CHK"				, "S"			, 30	, CheckTypeEnum.Y_N);
			flexGrid.SetCol("NO_FILE"			, "파일번호"		, 90	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("NO_LINE"			, "항번"			, false);
			flexGrid.SetCol("NO_DSP"			, "순번"			, 40	, typeof(decimal), "####.##", TextAlignEnum.CenterCenter);
			flexGrid.SetCol("NM_SUBJECT"		, "주제"			, 300);
			flexGrid.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 150);
			flexGrid.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 300);
			flexGrid.SetCol("UCODE"				, "U코드"		, 110);
			flexGrid.SetCol("LN_VENDOR"			, "매입처"		, 100);
			flexGrid.SetCol("LN_BUYER"			, "매출처"		, false);
			flexGrid.SetCol("NO_IMO"			, "IMO"			, false);
			flexGrid.SetCol("CD_ITEM"			, "재고코드"		, 110	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("CD_ITEM_NEW"		, "재고코드(후)"	, 110	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("UM_KR_P"			, "원화단가"		, 75	, typeof(decimal), FormatTpType.MONEY);

			flexGrid.SetDefault("22.04.01.02", SumPositionEnum.None);
			flexGrid.SetEditColumn("CHK", "CD_ITEM_NEW");
			flexGrid.SetAlternateRow();
			flexGrid.SetMalgunGothic();
		}

		private void InitGrid_KEY(FlexGrid flexGrid, int keyCount)
		{
			flexGrid.BeginSetting(1, 1, false);

			flexGrid.SetCol("CD_COMPANY", "회사코드"	, false);
			flexGrid.SetCol("CD_ITEM"	, "재고코드"	, false);
			flexGrid.SetCol("SEQ"		, "순번"		, false);

			if (flexGrid == grd메인키)
				flexGrid.SetCol("KEYU_DSP"	, "U코드"	, 180	, true);
						
			for (int i = 1; i <= keyCount; i++)
				flexGrid.SetCol("KEY" + i	, "포함"		, 180	, true);
			
			flexGrid.SetDefault("20.12.18.02", SumPositionEnum.None);
			flexGrid.SetAlternateRow();
			flexGrid.SetMalgunGothic();
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			grd라인.AfterRowChange += Grd라인_AfterRowChange;

			btn엑셀업로드.Click += Btn엑셀업로드_Click;
			btn동기화.Click += Btn동기화_Click;
			btn트레이닝.Click += Btn트레이닝_Click;
			btn테스트.Click += Btn테스트_Click;
			btn재고코드.Click += Btn재고코드_Click;

			ctx대분류.QueryBefore += Ctx대분류_QueryBefore;
			ctx대분류.QueryAfter += Ctx대분류_QueryAfter;			
			ctx중분류.QueryBefore += Ctx중분류_QueryBefore;
			ctx재고코드.QueryBefore += Ctx재고코드_QueryBefore;

			btn메인키추가.Click += Btn키워드추가_Click;
			btn메인키삭제.Click += Btn키워드삭제_Click;

			btn제외키추가.Click += Btn키워드추가_Click;
			btn제외키삭제.Click += Btn키워드삭제_Click;

			btn서브키1추가.Click += Btn키워드추가_Click;
			btn서브키1삭제.Click += Btn키워드삭제_Click;

			btn서브키2추가.Click += Btn키워드추가_Click;
			btn서브키2삭제.Click += Btn키워드삭제_Click;

			grd트레이닝1.ValidateEdit += Grd트레이닝_ValidateEdit;
		}

		

		private void Grd트레이닝_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			FlexGrid flexGrid = (FlexGrid)sender;

			//if (flexGrid.EditData == "")
			//	return;

			string colName = flexGrid.Cols[e.Col].Name;

			if (colName == "CD_ITEM_NEW")
			{
				string itemCode = flexGrid.EditData;
				string query = @"
SELECT
	CD_ITEM
FROM MA_PITEM WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + LoginInfo.CompanyCode + @"'
	AND CD_ITEM = '" + itemCode + @"'
	AND YN_USE = 'Y'";

				DataTable dt = DBMgr.GetDataTable(query);

				if (dt.Rows.Count == 1)
				{
					flexGrid["CD_ITEM_NEW"] = dt.Rows[0][0];
					flexGrid["CHK"] = "Y";
					flexGrid.SetRowBackColor(flexGrid.Row, Color.Yellow);
				}
				else
				{
					e.Cancel = true;
					flexGrid["CD_ITEM_NEW"] = "";
					flexGrid["CHK"] = "N";
					flexGrid.SetRowBackColor(flexGrid.Row, "");
					tbx포커스.Focus();
					flexGrid.Focus();
				}
			}
		}

		private void Btn엑셀업로드_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			EXCEL excel = new EXCEL();

			if (excel.OpenDialog() != DialogResult.OK)
				return;

			UT.ShowPgb("작업이 진행중입니다.");

			excel.HeaderRowIndex = 1;
			excel.StartDataIndex = 4;
			DataTable dt = excel.Read();

			SQL.ExecuteNonQuery("PX_CZ_MA_PITEM", sqlDebug, dt.ToXml());
			품목동기화(dt);

			UT.ClosePgb();
		}

		private void Btn동기화_Click(object sender, EventArgs e)
		{
			//품목동기화(grd라인.DataTable);
		}

		private void Btn트레이닝_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			if (cbm매입처.QueryWhereIn_Pipe == "")
			{
				if (ShowMessage("매입처를 지정하지 않을 경우 많은 시간이 소요됩니다.\n진행하시겠습니까?", "QY2") != DialogResult.Yes)
					return;
			}

			UT.ShowPgb("조회중입니다.");

			string proc;

			if (Regex.Match(grd라인["CD_ITEM"].문자(), "[0-9][0-9][0-9][0-9][0-9][0-9]").Value == "" || grd라인["CD_ITEM"].문자().시작("GSETC"))
				proc = "PS_CZ_DX_PITEM_TRN_EQ";
			else
				proc = "PS_CZ_DX_PITEM_TRN_GS";


			SQL sqlTrn = new SQL(proc, SQLType.Procedure, sqlDebug);
			sqlTrn.Parameter.Add2("@CD_COMPANY"	, LoginInfo.CompanyCode);
			sqlTrn.Parameter.Add2("@CD_ITEM"	, grd라인["CD_ITEM"]);
			sqlTrn.Parameter.Add2("@DT_F"		, dtp접수일자.StartDateToString);
			sqlTrn.Parameter.Add2("@DT_T"		, dtp접수일자.EndDateToString);
			sqlTrn.Parameter.Add2("@CD_VENDOR"	, cbm매입처.QueryWhereIn_Pipe);
			sqlTrn.Parameter.Add2("@YN_MK"		, chk메인키검색.GetValue());
			sqlTrn.Parameter.Add2("@SEQ_MK"		, rdo메인키전체.GetCheckedControl().GetTag() == "Y" ? 0 : grd메인키["SEQ"].ToInt());
			sqlTrn.Parameter.Add2("@YN_SK"		, chk서브키검색.GetValue());
			sqlTrn.Parameter.Add2("@SEQ_SK1"	, rdo서브키전체.GetCheckedControl().GetTag() == "Y" ? 0 : grd서브키1["SEQ"].ToInt());
			sqlTrn.Parameter.Add2("@SEQ_SK2"	, rdo서브키전체.GetCheckedControl().GetTag() == "Y" ? 0 : grd서브키2["SEQ"].ToInt());

			DataTable dtTrn = sqlTrn.GetDataTable();
			grd트레이닝1.Binding = dtTrn;

			// 해당 매입처이지만 키워드로 검색 안되는 리스트 (원인 분석용)			
			SQL sqlChk = new SQL("PS_CZ_DX_PITEM_REG_TRANING_CHK", SQLType.Procedure, sqlDebug);
			sqlChk.Parameter.Add2("@CD_COMPANY"	, LoginInfo.CompanyCode);
			sqlChk.Parameter.Add2("@CD_ITEM"	, grd라인["CD_ITEM"]);
			sqlChk.Parameter.Add2("@DT_F"		, dtp접수일자.StartDateToString);
			sqlChk.Parameter.Add2("@DT_T"		, dtp접수일자.EndDateToString);
			sqlChk.Parameter.Add2("@CD_VENDOR"	, cbm매입처.QueryWhereIn_Pipe);
			sqlChk.Parameter.Add2("@XML"		, dtTrn.ToXml("CD_COMPANY", "NO_FILE", "NO_LINE"));
			
			DataTable dtChk = sqlChk.GetDataTable();
			grd트레이닝2.Binding = dtChk;
			
			UT.ClosePgb();
		}

		private void Btn테스트_Click(object sender, EventArgs e)
		{
			//SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;
			//Util.ShowProgress(DD("조회중입니다."));

			////if (tbx파일번호2.Text == "")
			////	return;

			//SQL sql = new SQL("PS_CZ_DXVENDOR_REG_TEST", SQLType.Procedure, sqlDebug);			
			//sql.Parameter.Add2("@CD_COMPANY", _companyCode);
			//sql.Parameter.Add2("@DT_F"		, dtp접수일자2.StartDateToString);
			//sql.Parameter.Add2("@DT_T"		, dtp접수일자2.EndDateToString);
			//sql.Parameter.Add2("@DT_CHK"	, chk접수일자2.Checked ? "Y" : "");
			//sql.Parameter.Add2("@CD_VENDOR"	, cbm매입처2.QueryWhereIn_Pipe);
			//sql.Parameter.Add2("@NO_FILE"	, tbx파일번호2.Text);

			//DataTable dt = sql.GetDataTable();
			//fgd테스트.Binding = dt;

			//Util.CloseProgress();
		}

		private void Btn재고코드_Click(object sender, EventArgs e)
		{
			FlexGrid flexGrid;

			//if (tab메인.SelectedTab == tab역전개)
			//	flexGrid = fgd라인;
			//else
			//	flexGrid = fgd아이템;
			flexGrid = grd트레이닝1;

			foreach (DataRow row in flexGrid.DataTable.Select("CHK = 'Y'"))
			{
				row["CD_ITEM_NEW"] = ctx재고코드.CodeValue;
				//flexGrid.setcol
				
			}

			SetGridStyle();
		}

		private void Ctx대분류_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "MA_B000030";
		}

		private void Ctx대분류_QueryAfter(object sender, BpQueryArgs e)
		{
			ctx중분류.Clear();
		}		

		private void Ctx중분류_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
			e.HelpParam.P42_CD_FIELD2 = ctx대분류.CodeValue;
		}

		private void Ctx재고코드_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P42_CD_FIELD2 = "009";
			e.HelpParam.IsRequireSearchKey = true;
		}



		private void Btn키워드추가_Click(object sender, EventArgs e)
		{
			string name = "grd" + ((Button)sender).Name.Replace("btn", "").Replace("추가", "").Replace("삭제", "");
			FlexGrid flexGrid = (FlexGrid)Controls.Find(name, true)[0];

			flexGrid.Rows.Add();
			flexGrid.Row = flexGrid.Rows.Count - 1;
			flexGrid["CD_COMPANY"] = LoginInfo.CompanyCode;
			flexGrid["CD_ITEM"] = grd라인["CD_ITEM"];
			flexGrid["SEQ"] = (int)flexGrid.Aggregate(AggregateEnum.Max, "SEQ") + 1;
			flexGrid["CD_TYPE"] = flexGrid.GetTag();


			flexGrid.AddFinished();
		}

		private void Btn키워드삭제_Click(object sender, EventArgs e)
		{
			string name = "grd" + ((Button)sender).Name.Replace("btn", "").Replace("추가", "").Replace("삭제", "");
			FlexGrid flexGrid = (FlexGrid)Controls.Find(name, true)[0];

			if (flexGrid.HasNormalRow)
				flexGrid.Rows.Remove(flexGrid.Row);
		}

		private void Grd라인_AfterRowChange(object sender, RangeEventArgs e)
		{
			//if (fgd헤드["CD_PARTNER"].ToString() == "")
			//	return;

			string itemCode = grd라인["CD_ITEM"].ToString2();
			string filter = "CD_ITEM = '" + itemCode + "'";

			if (grd라인.DetailQueryNeed)
			{
				DataSet ds = DBMgr.GetDataSet("PS_CZ_DX_PITEM_REG_L", LoginInfo.CompanyCode, itemCode);
				grd메인키.BindingAdd(ds.Tables[0], filter);
				grd제외키.BindingAdd(ds.Tables[1], filter);
				grd서브키1.BindingAdd(ds.Tables[2], filter);
				grd서브키2.BindingAdd(ds.Tables[3], filter);

			}
			else
			{
				grd메인키.BindingAdd(null, filter);
				grd제외키.BindingAdd(null, filter);
				grd서브키1.BindingAdd(null, filter);
				grd서브키2.BindingAdd(null, filter);
			}

			//fgd메인키.BindingAdd(dt, "");
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			SQL sql = new SQL("PS_CZ_DX_PITEM_REG_H", SQLType.Procedure, sqlDebug);
			sql.Parameter.Add2("@CD_COMPANY", LoginInfo.CompanyCode);
			sql.Parameter.Add2("@" + cbo검색.GetValue(), tbx검색.Text);
			sql.Parameter.Add2("@KEYWORD"	, tbx키워드.Text);
			sql.Parameter.Add2("@CLS_L"		, ctx대분류.CodeValue);
			sql.Parameter.Add2("@CLS_M"		, ctx중분류.CodeValue);

			DataTable dt = sql.GetDataTable();
			grd라인.Binding = dt;
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSaveButtonClicked(sender, e);
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			// 그리드 검사
			if (!base.Verify())
				return;

			try
			{
				// ********** 키워드 저장
				SQL sql = new SQL("PX_CZ_DX_PITEM_REG", SQLType.Procedure, sqlDebug);
				sql.Parameter.Add2("@XML_MK"	, grd메인키.GetChanges().ToXml());
				sql.Parameter.Add2("@XML_EX"	, grd제외키.GetChanges().ToXml());
				sql.Parameter.Add2("@XML_SK1"	, grd서브키1.GetChanges().ToXml());
				sql.Parameter.Add2("@XML_SK2"	, grd서브키2.GetChanges().ToXml());
				sql.ExecuteNonQuery();

				// MA_PITEM에 U코드 동기화
				SQL sqlUcode = new SQL("PX_CZ_DX_PITEM_UCODE", SQLType.Procedure, sqlDebug);
				sqlUcode.Parameter.Add2("@XML"	, grd메인키.GetChanges().ToXml());
				sqlUcode.ExecuteNonQuery();

				// 검색테이블 업데이트
				품목동기화(grd메인키.GetChanges());

				grd메인키.AcceptChanges();
				grd제외키.AcceptChanges();
				grd서브키1.AcceptChanges();
				grd서브키2.AcceptChanges();

				// ********** 트레이닝 재고코드 변경 저장
				SQL.ExecuteNonQuery("PX_CZ_SA_QTN_SCODE", sqlDebug, grd트레이닝1.DataTable.Select("CHK = 'Y'").ToXml("CD_COMPANY", "NO_FILE", "NO_LINE", "CD_ITEM_NEW"));
				SQL.ExecuteNonQuery("PX_CZ_SA_QTN_SCODE", sqlDebug, grd트레이닝2.DataTable.Select("CHK = 'Y'").ToXml("CD_COMPANY", "NO_FILE", "NO_LINE", "CD_ITEM_NEW"));

				grd트레이닝1.AcceptChanges();
				grd트레이닝2.AcceptChanges();

				UT.ShowMsg(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		#endregion

		public static void 품목동기화(DataTable dt)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;
			
			try
			{
				SQL.ExecuteNonQuery("PX_CZ_DX_PITEM_SEARCH_SYNC", sqlDebug, dt.ToXml("CD_ITEM"));
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		private void SetGridStyle()
		{
			grd트레이닝1.Redraw = false;

			for (int i = grd트레이닝1.Rows.Fixed; i < grd트레이닝1.Rows.Count; i++)
			{
				if (grd트레이닝1[i, "CHK"].ToString() == "Y")
				{
					grd트레이닝1.SetRowBackColor(i, Color.Yellow);
				}
				else
				{
					grd트레이닝1.SetRowBackColor(i, "");
				}
			}

			grd트레이닝1.Redraw = true;
		}
	}
}

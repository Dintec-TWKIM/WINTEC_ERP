using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
using System.IO;
using Duzon.Common.Controls;
using DX;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using SelectPdf;
using Duzon.Common.Forms.Help;

namespace cz
{
	public partial class P_CZ_PU_PO_UM_MGT : PageBase
	{
		string[] VendCodes;

		public P_CZ_PU_PO_UM_MGT()
		{
			InitializeComponent();
		}

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{			
			InitGrid();
			InitEvent();

			MainGrids = new FlexGrid[] { grd목록, grd아이템, grd매입처 };
			grd목록.DetailGrids = new FlexGrid[] { grd아이템, grd매입처 };

			DataTable dtHead  = SQL.GetDataTable("PS_CZ_PU_STOCK_UM_HEAD", "", "");
			grd목록.Binding = dtHead;
		}

		protected override void InitPaint()
		{			
			spc메인.SplitterDistance = 500;

			// 확정 버튼 권한
			//if (!LoginInfo.UserID.In("S-343", "S-495"))
			//	btn확장.Visible = false;

			if (!btn확장.Visible)
			{
				spc메인.Panel1Collapsed = true;
			}
		}

		private void Init()
		{
			pnl매입처.Edit(true);
			ctx매입처.Init();

			grd목록.Clear2();
			grd아이템.Clear2();
			grd매입처.Clear2();
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			//dt.Rows.Add("YN_USE_LT", "Y", DD("사용"));
			//dt.Rows.Add("YN_USE_LT", "N", DD("미사용"));

			dt.Rows.Add("TP_DATA", "N", DD("숫자"));		// DECIMAL
			dt.Rows.Add("TP_DATA", "S", DD("문자"));		// STRING

			dt.Rows.Add("YN_USE_LT", "Y", DD("사용"));
			dt.Rows.Add("YN_USE_LT", "N", DD("미사용"));

			// ********** 목록
			grd목록.BeginSetting(2, 1, false);
			   
			grd목록.SetCol("CD_COMPANY"	, "*"		, false);
			grd목록.SetCol("CD_HEAD"		, "코드"		, 70	, TextAlignEnum.CenterCenter);
			grd목록.SetCol("NM_HEAD"		, "매업처명"	, 300);
			grd목록.SetCol("YN_USE"		, "사용"		, 40	, TextAlignEnum.CenterCenter);
			grd목록.SetCol("YN_NEW"		, "신규"		, false);

			grd목록.SetOneGridBinding(new object[] { }, one헤드);
			grd목록.SetDefault("21.07.01.03", SumPositionEnum.None);
			grd목록.SetAlternateRow();
			grd목록.SetMalgunGothic();

			// ********** 아이템
			grd아이템.BeginSetting(2, 1, false);

			grd아이템.컬럼세팅("CHK"			, "S"		, 30	, 포맷.체크);
			//grd아이템.SetCol("CHK"			, "S"	, 300);
			grd아이템.SetCol("CD_ITEM"		, "재고코드"	, 110	, TextAlignEnum.CenterCenter);
			grd아이템.SetCol("NM_ITEM"		, "재고명"	, 300);
			
			grd아이템.SetDefault("23.01.10.01", SumPositionEnum.None);
			grd아이템.SetAlternateRow();
			grd아이템.SetMalgunGothic();
			grd아이템.SetCopyAndPaste(BindItem);
			grd아이템.SetDummyColumn("CHK");
			grd아이템.SetEditColumn("CHK", "CD_ITEM");

			// ********** 매입처
			grd매입처.BeginSetting(1, 1, false);

			grd매입처.SetCol("CD_VEND"		, "*"			, false);
			grd매입처.SetCol("SEQ"			, "순번"			, 40	, typeof(int), "####", TextAlignEnum.CenterCenter);
			grd매입처.SetCol("CD_PARTNER"	, "매입처코드"	, 130	, TextAlignEnum.CenterCenter);
			grd매입처.SetCol("LN_PARTNER"	, "매입처명"		, 300);
			grd매입처.SetCol("NM_PARTNER"	, "표시이름"		, 300);
			
			grd매입처.SetCol("YN_LINK"		, "연동여부"	, 70	, TextAlignEnum.CenterCenter);
			grd매입처.SetCol("DC_PRICE_TERMS", "가격조건"	, 100);		// DC율 입력할수 있도록
			grd매입처.SetCol("RT_DC"			, "DC율"		, 100);		// DC율 입력할수 있도록

			grd매입처.SetCol("TP_DATA"		, "형식"		, 70	, TextAlignEnum.CenterCenter);		// 숫자, 텍스트
			grd매입처.SetCol("CD_EXCH"		, "통화"		, 70	, TextAlignEnum.CenterCenter);
			grd매입처.SetCol("YN_USE_LT"		, "납기"		, 70	, TextAlignEnum.CenterCenter);

			grd매입처.SetCol("EMP_INSERT"	, "등록자"	, 100	, TextAlignEnum.CenterCenter);
			grd매입처.SetCol("DTS_INSERT"	, "등록일"	, 140	, typeof(string), "####/##/## ##:##:##");
			grd매입처.SetCol("EMP_UPDATE"	, "수정자"	, 100	, TextAlignEnum.CenterCenter);
			grd매입처.SetCol("DTS_UPDATE"	, "수정일"	, 140	, typeof(string), "####/##/## ##:##:##");

			grd매입처.SetDataMap("YN_LINK", dt.Select("TYPE = 'YN_USE_LT'"));

			grd매입처.SetDataMap("TP_DATA", dt.Select("TYPE = 'TP_DATA'"));
			grd매입처.SetDataMap("CD_EXCH", CODE.ExchangeName2());
			grd매입처.SetDataMap("YN_USE_LT", dt.Select("TYPE = 'YN_USE_LT'"));

			grd매입처.VerifyNotNull = new string[] { "NM_PARTNER" };
			grd매입처.SetDefault("21.09.14.02", SumPositionEnum.None);
			grd매입처.SetAlternateRow();
			grd매입처.SetMalgunGothic();
			grd매입처.SetCopyAndPaste(BindVendor);
			grd매입처.SetEditColumn("SEQ", "CD_PARTNER", "NM_PARTNER", "YN_LINK", "DC_PRICE_TERMS", "RT_DC", "TP_DATA", "CD_EXCH", "YN_USE_LT");
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			btn테스트.Click += Btn테스트_Click;
			btn추가.Click += Btn추가_Click;
			btn삭제.Click += Btn삭제_Click;
			btn확장.Click += Btn확장_Click;
			ctx매입처.CodeChanged += Ctx매입처_CodeChanged;

			grd목록.BeforeRowChange += Grd목록_BeforeRowChange;
			grd목록.AfterRowChange += Grd목록_AfterRowChange;

			grd아이템.AfterEdit += Grd아이템_AfterEdit;
			grd매입처.AfterEdit += Grd매입처_AfterEdit;
		}

		private void Btn테스트_Click(object sender, EventArgs e)
		{
			
			DirectoryInfo dir = new DirectoryInfo(@"d:\선용품");
				
			foreach (DirectoryInfo d in dir.GetDirectories())
			{
				string query = @"INSERT INTO CZ_MA_PITEM_TEMP VALUES ('K100', '" + d.Name + "'";
				string val = "";

				for (int i = 0; i < 6; i++)
				{
					if (i < d.GetFiles().Length)
						val += ",'" + d.GetFiles()[i].Name + "'";
					else
						val += ",null";
				}

				query += val + ")";

				//디비.실행(query);
				DBMgr.ExecuteNonQuery(query);
			}
		}

		private void Btn추가_Click(object sender, EventArgs e)
		{
			if (tab메인.SelectedTab == tab아이템)
			{
				grd아이템.Rows.Add();
				grd아이템.AddFinished();
			}
			else if (tab메인.SelectedTab == tab매입처)
			{
				grd매입처.Rows.Add();
				grd매입처.Row = grd매입처.Rows.Count - 1;
				grd매입처["CD_VEND"] = string.Format("{0:000}", (int)grd매입처.Aggregate(AggregateEnum.Max, "CD_VEND") + 1);
				grd매입처["SEQ"] = (int)grd매입처.Aggregate(AggregateEnum.Max, "SEQ") + 1;
				grd매입처.AddFinished();
			}
		}

		private void Btn삭제_Click(object sender, EventArgs e)
		{
			if (tab메인.SelectedTab == tab아이템)
			{
				grd아이템.그리기중지();
				grd아이템.데이터테이블().선택("CHK = 'Y'").삭제();
				grd아이템.그리기시작();
			}
			else if (tab메인.SelectedTab == tab매입처)
			{
				grd매입처.Rows.Remove(grd매입처.Row);
			}
		}

		private void Btn확장_Click(object sender, EventArgs e)
		{
			if (spc메인.Panel1Collapsed)
			{
				// 접혀 있으면 펼치기
				spc메인.Panel1Collapsed = false;
				btn확장.Text = ">";
			}
			else
			{
				// 펼쳐져 있으면 접기
				spc메인.Panel1Collapsed = true;
				btn확장.Text = "<";
			}
		}

		private void Ctx매입처_CodeChanged(object sender, EventArgs e)
		{
			string comCd = LoginInfo.CompanyCode;
			string parCd = ctx매입처.CodeValue;
			string parNm = ctx매입처.CodeName;
			DataTable dtHead = SQL.GetDataTable("PS_CZ_PU_STOCK_UM_HEAD", comCd, parCd);
			
			if (dtHead.Rows.Count == 0)
			{
				// ********** 신규 모드
				if (grd목록.DataTable.Select("YN_NEW = 'Y'").Length == 0)
				{
					// 완전 처음
					grd목록.Rows.Add();
					grd목록.Row = grd목록.Rows.Count - 1;
					grd목록["CD_COMPANY"] = comCd;
					grd목록["NM_HEAD"] = parNm;
					grd목록["CD_HEAD"] = parCd;
					grd목록["YN_NEW"] = "Y";
					grd목록.AddFinished();
				}
				else
				{
					// 다른 매입처를 한번 선택했던지 해서 이미 행 추가가 되어있는 경우
					grd목록["NM_HEAD"] = parNm;
					grd목록["CD_HEAD"] = parCd;
				}
			}
			else
			{
				// ********** 조회 모드
				grd목록.DataBind(dtHead);
				pnl매입처.Edit(false);
			}
		}

		private void Grd아이템_AfterEdit(object sender, RowColEventArgs e)
		{
			BindItem((FlexGrid)sender, e.Row, e.Col);
		}


		private void Grd매입처_AfterEdit(object sender, RowColEventArgs e)
		{
			BindVendor((FlexGrid)sender, e.Row, e.Col);
		}

		#endregion

		#region ==================================================================================================== Search		

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{			
			base.OnToolBarSearchButtonClicked(sender, e);
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			if (!btn확장.Visible)
			{
				if (!ctx매입처.Verify("매입처를 선택하세요"))
					return;
			}

			try
			{
				UT.ShowPgb("조회중입니다.");

				DataTable dtHead = SQL.GetDataTable("PS_CZ_PU_STOCK_UM_HEAD", LoginInfo.CompanyCode, "");
				grd목록.DataBind(dtHead);

				UT.ClosePgb();
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}			
		}

		private void Grd목록_BeforeRowChange(object sender, RangeEventArgs e)
		{
			if (IsChanged())
			{
				UT.ShowMsg("편집 중에 이동할 수 없습니다.");
				e.Cancel = true;
			}
		}

		private void Grd목록_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				DataTable dtItem = SQL.GetDataTable("PS_CZ_PU_STOCK_UM_ITEM_2", SQLDebug.Print, grd목록["CD_COMPANY"], grd목록["CD_HEAD"]);
				DataTable dtVend = SQL.GetDataTable("PS_CZ_PU_STOCK_UM_VEND", SQLDebug.Print, grd목록["CD_COMPANY"], grd목록["CD_HEAD"]);

				// ********** 아이템 그리드
				grd아이템.BeginSetting(2, 1, false);
			
				// 매입처 컬럼 삭제
				for (int j = grd아이템.Cols.Count - 1; j > 0; j--)
				{
					if (new Regex("^[0-9][0-9][0-9]").IsMatch(grd아이템.Cols[j].Name))
						grd아이템.Cols.Remove(j);
				}

				// 매입처 동적 추가
				VendCodes = new string[dtVend.Rows.Count];
				List<string> editCols = new List<string>();

				for (int i = 0; i < dtVend.Rows.Count; i++)
				{
					string code = dtVend.Rows[i]["CD_VEND"].ToString();
					string name = dtVend.Rows[i]["NM_PARTNER"].ToString();
					VendCodes[i] = code;

					if (dtVend.Rows[i]["TP_DATA"].ToString() == "N")
					{
						// 숫자형(단가) 필드
						string exch = dtVend.Rows[i]["CD_EXCH"].ToString();
						string ynLt = dtVend.Rows[i]["YN_USE_LT"].ToString();
						string umColName = ynLt == "N" ? name : "단가";

						grd아이템.SetCol(code + "_UM", name, umColName, 100, typeof(decimal), exch == "000" ? "#,###" : "#,###.##", TextAlignEnum.RightCenter);
						editCols.Add(code + "_UM");

						// 원화가 아닐 경우 색상 변경
						if (exch != "000")
						{
							grd아이템.Cols[code + "_UM"].SetColor(Color.Red);

							// 컬럼 헤더 색상이 컬럼 색상으로 바뀌어 버리므로 다시 재정의 (editcolumn 에선 이미 정의되어 있는 스타일은 안건드림)
							CellStyle style = grd아이템.Styles.Add("COL_HEADER");
							style.ForeColor = Color.Blue;
							style.Font = new Font(grd아이템.Font, FontStyle.Bold);
							grd아이템.SetCellStyle(grd아이템.GetHeadRow(code + "_UM"), code + "_UM", "COL_HEADER");
						}

						if (ynLt == "Y")
						{
							grd아이템.SetCol(code + "_LT", name, "납기", 50, typeof(decimal), "#,###", TextAlignEnum.RightCenter);
							editCols.Add(code + "_LT");
						}
					}
					else
					{
						// 문자형 필드
						grd아이템.SetCol(code + "_RM", name, 300);
						editCols.Add(code + "_RM");
					}
				}
			
				grd아이템.SetDefault(grd아이템.SettingVersion + DateTime.Now.ToString("yyyyMMddHHmmss"), SumPositionEnum.None);
				grd아이템.SetAlternateRow();
				grd아이템.SetMalgunGothic();
				grd아이템.SetEditColumn(editCols.ToArray());

				// 바인딩
				grd아이템.DataBind(dtItem);

				// ********** 매입처
				grd매입처.DataBind(dtVend);

				// 마무리
				pnl매입처.Edit(false);
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarAddButtonClicked(sender, e);

			if (!MsgAndSave(PageActionMode.Search))
				return;

			Init();	// 리셋의 개념으로만 감 (리스트가 닫혀있을때의 추가 기능과 상충됨)			
		}

		#endregion

		#region ==================================================================================================== Delete

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarDeleteButtonClicked(sender, e);

			if (UT.ShowMsg(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes) return;
			if (!Util.CheckPW()) return;

			try
			{
				string query = @"
DELETE FROM CZ_PU_STOCK_UM_HEAD WHERE CD_COMPANY = '" + grd목록["CD_COMPANY"] + "' AND CD_HEAD = '" + grd목록["CD_HEAD"] + @"'
DELETE FROM CZ_PU_STOCK_UM_ITEM WHERE CD_COMPANY = '" + grd목록["CD_COMPANY"] + "' AND CD_HEAD = '" + grd목록["CD_HEAD"] + @"';
DELETE FROM CZ_PU_STOCK_UM_VEND WHERE CD_COMPANY = '" + grd목록["CD_COMPANY"] + "' AND CD_HEAD = '" + grd목록["CD_HEAD"] + "'";

				SQL.ExecuteNonQuery(query);

				Init();
				UT.ShowMsg(공통메세지.자료가정상적으로삭제되었습니다);
				
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSaveButtonClicked(sender, e);
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			if (!base.Verify()) return;


			// 조회 후 수정이 아닐 경우 중복 체크
			if (ctx매입처.ReadOnly != ReadOnly.TotalReadOnly)
			{
				string query = "SELECT 1 FROM CZ_PU_STOCK_UM_HEAD WHERE CD_COMPANY = '" + LoginInfo.CompanyCode + "' AND CD_HEAD = '" + ctx매입처.CodeValue + "'";
				if (SQL.GetDataTable(query).Rows.Count > 0)
				{
					UT.ShowMsg("중복 매입처입니다.");
					return;
				}
			}



			try
			{
				UT.ShowPgb("저장중입니다.");

				DataTable dtItemOrg = grd아이템.GetChanges(DataRowState.Added | DataRowState.Modified);
				DataTable dtItemUpd = new DataTable();
				DataTable dtItemDel = grd아이템.GetChanges(DataRowState.Deleted);

				// ********** 아이템 테이블
				if (dtItemOrg != null)
				{
					foreach (string s in VendCodes)
					{
						// 컬럼 추출
						DataTable dt = dtItemOrg.ToDataTable(false, "CD_ITEM", s + "_UM", s + "_LT", s + "_RM");

						// 테이블 합치기
						if (dt != null)
						{
							// 매입처 컬럼 추가 (PK임)
							DataColumn newCol = new DataColumn("CD_VEND") { DefaultValue = s };							
							dt.Columns.Add(newCol);
							newCol.SetOrdinal(0);

							// DB 실제 컬럼 이름으로 변경
							dt.Columns[s + "_UM"].ColumnName = "UM";
							dt.Columns[s + "_LT"].ColumnName = "LT";
							dt.Columns[s + "_RM"].ColumnName = "DC_RMK";
							dtItemUpd.Merge(dt);
						}
					}
				}

				// ********** 매입처 테이블
				DataTable dtVend = grd매입처.GetChanges();

				// ********** 저장
				SQL sql = new SQL("PX_CZ_PU_STOCK_UM", SQLType.Procedure, sqlDebug);
				sql.Parameter.Add2("@CD_COMPANY"	, grd목록["CD_COMPANY"]);
				sql.Parameter.Add2("@CD_HEAD"		, grd목록["CD_HEAD"]);
				sql.Parameter.Add2("@XML_ITEM"		, dtItemUpd.ToXml());
				sql.Parameter.Add2("@XML_ITEM_DEL"	, dtItemDel.ToXml("CD_ITEM"));
				sql.Parameter.Add2("@XML_VEND"		, dtVend.ToXml());
				sql.ExecuteNonQuery();

				pnl매입처.Edit(false);
				grd목록.AcceptChanges();
				grd아이템.AcceptChanges();
				grd매입처.AcceptChanges();
				UT.ShowMsg(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		#endregion

		private void BindItem(FlexGrid grid, int row, int col)
		{
			string colName = grid.Cols[col].Name;
			string data = grid[row, col].ToString();

			if (colName == "CD_ITEM")
			{
				string query = @"
SELECT
	CD_ITEM
,	NM_ITEM
FROM MA_PITEM WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + LoginInfo.CompanyCode + @"'
	AND CD_ITEM = '" + data + @"'";

				DataTable dt = SQL.GetDataTable(query);

				if (dt.Rows.Count == 1)
				{
					grid[row, "CD_ITEM"] = dt.Rows[0]["CD_ITEM"];
					grid[row, "NM_ITEM"] = dt.Rows[0]["NM_ITEM"];
				}
				else
				{
					grid[row, "CD_ITEM"] = "";
					grid[row, "NM_ITEM"] = "";
				}
			}
		}

		private void BindVendor(FlexGrid grid, int row, int col)
		{
			string colName = grid.Cols[col].Name;
			string data = grid[row, col].ToString();

			if (colName == "CD_PARTNER")
			{
				string query = @"
SELECT
	CD_PARTNER
,	LN_PARTNER
FROM MA_PARTNER WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + LoginInfo.CompanyCode + @"'
	AND CD_PARTNER = '" + data + @"'
	AND USE_YN = 'Y'";

				DataTable dt = SQL.GetDataTable(query);

				if (dt.Rows.Count == 1)
				{
					grid[row, "LN_PARTNER"] = dt.Rows[0]["LN_PARTNER"];
					grid[row, "NM_PARTNER"] = dt.Rows[0]["LN_PARTNER"];
				}
				else
				{
					grid[row, "CD_PARTNER"] = "";
					grid[row, "LN_PARTNER"] = "";
					grid[row, "NM_PARTNER"] = "";
				}

				// 자동증가
				if (grid[row, "CD_VEND"].ToString() == "")
					grid[row, "CD_VEND"] = (int)grid.Aggregate(AggregateEnum.Max, "CD_VEND") + 1;
			}
		}
	}
}
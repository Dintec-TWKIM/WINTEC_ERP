using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Controls;
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
	public partial class P_CZ_PR_ITEM_REPLACE_REG : PageBase
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public string NO_EMP { get; set; }

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_PR_ITEM_REPLACE_REG()
		{
			StartUp.Certify(this);
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			NO_EMP = Global.MainFrame.LoginInfo.UserID;
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid(flexS);
			InitGrid(flexE);
			InitEvent();

			Util.SetGRID_Edit(flexE, "CD_ITEM_AFTER", true);
			Util.SetGRID_Edit(flexE, "QT_IO", true);
		}

		private void InitControl()
		{
			dtp일자.StartDateToString = Util.GetToday(-30);
			dtp일자.EndDateToString = Util.GetToday();

			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			dt.Rows.Add("품목코드", "1", "품목코드");
			dt.Rows.Add("품목코드", "2", "재고코드");

			cbo품목코드.DataSource = dt.Select("TYPE = '품목코드'").CopyToDataTable();
			cbo품목코드.SelectedIndex = 0;

			MainGrids = new FlexGrid[] { flexE };

			// ==================================================================================================== 검색 그리드
			grdSearch.BeginSetting(1, 1, false);

			grdSearch.SetCol("NM_TYPE"			, "구분"			, 80);
			grdSearch.SetCol("NO_DSP"			, "순번"			, 40);
			grdSearch.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			grdSearch.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 200);
			grdSearch.SetCol("CD_ITEM_BEFORE"	, "재고코드(이전)", 100);
			grdSearch.SetCol("CD_ITEM_AFTER"	, "재고코드(이후)", 100);	
			grdSearch.SetCol("QT_IO_BEFORE"		, "수량"			,  50	, false	, typeof(decimal), FormatTpType.QUANTITY);

			grdSearch.Cols["NM_TYPE"].TextAlign = TextAlignEnum.CenterCenter;
			grdSearch.Cols["NO_DSP"].Format = "####.##";			
			grdSearch.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;

			grdSearch.KeyActionEnter = KeyActionEnum.MoveDown;
			grdSearch.SettingVersion = "18.04.04.02";
			grdSearch.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			grdSearch.Rows.DefaultSize = 22;
			grdSearch.Rows[0].Height = 28;
		}

		private void InitGrid(FlexGrid flex)
		{
			bool boEdit = (flex == flexS) ? false : true; // 그리드에 따라 에디트 여부 결정

			flex.BeginSetting(2, 1, false);
		
			flex.SetCol("CHK"				, "S"			, 30	, true	, CheckTypeEnum.Y_N);
			flex.SetCol("NO_SO"				, "수주번호"		, 100);
			flex.SetCol("NO_PO"				, "발주번호"		, 100);
			flex.SetCol("NO_IO"				, "수불번호"		, 100);
			flex.SetCol("NO_IOLINE"			, "수불항번"		, false);
			flex.SetCol("NO_DSP"			, "순번"			, 40);
			flex.SetCol("CD_PARTNER"		, "거래처코드"	, false);
			flex.SetCol("LN_PARTNER"		, "거래처"		, 180);
			flex.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			flex.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 200);
			flex.SetCol("CD_ITEM_BEFORE"	, "재고코드(이전)", false);
			flex.SetCol("CD_ITEM_AFTER"		, "재고코드"		, 80	, boEdit);	
			flex.SetCol("NM_ITEM"			, "재고명"		, false);
			flex.SetCol("QT_IO"				, "수량"			, 50	, 5		, boEdit, typeof(decimal), FormatTpType.QUANTITY);
			flex.SetCol("UM_EX"				, "단가"			, 100	, 11	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flex.SetCol("AM_EX"				, "금액"			, 100	, 11	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flex.SetCol("UM"				, "단가"			, 100	, 11	, false	, typeof(decimal), FormatTpType.MONEY);
			flex.SetCol("AM"				, "금액"			, 100	, 11	, false	, typeof(decimal), FormatTpType.MONEY);
			flex.SetCol("NO_IO_LINK"		, "대체번호"		, 100);
			flex.SetCol("NO_IOLINE_LINK"	, "대체항번"		, false);

			flex[0, flex.Cols["UM_EX"].Index] = "외화";
			flex[0, flex.Cols["AM_EX"].Index] = "외화";
			flex[0, flex.Cols["UM"].Index] = "원화";
			flex[0, flex.Cols["AM"].Index] = "원화";

			flex.Cols["NO_DSP"].Format = "####.##";
			flex.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			//flexS.Cols["CD_UNIT_MM"].TextAlign = TextAlignEnum.CenterCenter;
			//flexS.SetDataMap("CD_UNIT_MM", Util.GetDB_CODE("MA_B000004"), "CODE", "NAME");

			flex.KeyActionEnter = KeyActionEnum.MoveDown;
			flex.SettingVersion = "16.08.17.01";
			flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			flex.SetExceptSumCol("NO_DSP", "UM_EX", "UM");

			
		}

		private void InitEvent()
		{
			txt번호.KeyDown += new KeyEventHandler(txt번호_KeyDown);
			rdo입고.CheckedChanged += new EventHandler(rdo입고_CheckedChanged);
			rdo반품.CheckedChanged += new EventHandler(rdo입고_CheckedChanged);
			ctx창고.QueryBefore += new BpQueryHandler(ctx창고_QueryBefore);

			btn추가.Click += new EventHandler(btn추가_Click);
			btn삭제.Click += new EventHandler(btn삭제_Click);

			flexS.DoubleClick += new EventHandler(flexS_DoubleClick);
			flexE.ValidateEdit += new ValidateEditEventHandler(flexE_ValidateEdit);
		}

		protected override void InitPaint()
		{
			// 바인딩 초기화
			DataTable dtS = DBMgr.GetDataTable("PS_CZ_PR_ITEM_REPLACE_REG_TARGET", new object[] { "", "" });
			DataTable dtE = dtS.Clone();

			flexS.Binding = dtS;
			flexE.Binding = dtE;

			rdo입고_CheckedChanged(null, null);

			this.Focus();
		}

		#endregion

		#region ==================================================================================================== Event

		private void txt번호_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				OnToolBarSearchButtonClicked(null, null);
			}
		}

		private void rdo입고_CheckedChanged(object sender, EventArgs e)
		{
			// 검색콤보
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			if (rdo입고.Checked)
			{
				dt.Rows.Add("번호", "C.NO_SO", "수주번호");
				dt.Rows.Add("번호", "D.NO_PO", "발주번호");

				dt.Rows.Add("일자", "C.DT_SO", "수주일자");
				dt.Rows.Add("일자", "D.DT_PO", "발주일자");

				lbl거래처.Text = "매입처";
			}
			else
			{
				dt.Rows.Add("번호", "C.NO_SO", "수주번호");
				dt.Rows.Add("번호", "A.NO_IO", "반품번호");

				dt.Rows.Add("일자", "C.DT_SO", "수주일자");
				dt.Rows.Add("일자", "A.DT_IO", "반품일자");

				lbl거래처.Text = "매출처";
			}

			cbo번호.DataSource = dt.Select("TYPE = '번호'").CopyToDataTable();
			cbo번호.SelectedIndex = 0;

			cbo일자.DataSource = dt.Select("TYPE = '일자'").CopyToDataTable();
			cbo일자.SelectedIndex = 0;
		}

		private void ctx창고_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P09_CD_PLANT = Global.MainFrame.LoginInfo.CdPlant;
		}

		private void btn추가_Click(object sender, EventArgs e)
		{
			DataRow[] rows = flexS.DataTable.Select("CHK = 'Y'");
			if (rows.Length == 0)
			{
				ShowMessage("선택된 항목이 없습니다.");
				return;
			}

			flexE.Redraw = false;
			flexE.BindingAdd(rows.CopyToDataTable(), "", false);
			flexE.Redraw = true;
		}

		private void btn삭제_Click(object sender, EventArgs e)
		{
			DataRow[] rows = flexE.DataTable.Select("CHK = 'Y'");
			if (rows.Length == 0)
			{
				ShowMessage("선택된 항목이 없습니다.");
				return;
			}

			flexE.Redraw = false;

			for (int i = 0; i < rows.Length; i++)
			{
				MsgControl.ShowMsg("삭제중입니다. (" + (i + 1) + "/" + rows.Length + ")");
				rows[i].Delete();
			}

			flexE.Redraw = true;
			MsgControl.CloseMsg();
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트
		
		private void flexS_DoubleClick(object sender, EventArgs e)
		{
			// 헤더클릭
			if (flexS.MouseRow == 0 && flexS.MouseCol > 0)
			{
				SetGridEffects();
			}
		}

		private void SetGridEffects()
		{
			for (int i = flexS.Rows.Fixed; i < flexS.Rows.Count; i++)
			{
				if (flexS[i, "NO_IO_LINK"].ToString() != "") Util.SetGRID_Highlight(flexS, i);
			}
		}

		private void flexE_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			string COLNAME = flexE.Cols[e.Col].Name;
			
			if (COLNAME == "CD_ITEM_AFTER")
			{
				string CD_ITEM = flexE.EditData;

				if (CD_ITEM == "")
				{
					
				}
				else
				{
					string query = @"
SELECT
*
FROM MA_PITEM
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND CD_ITEM = '" + CD_ITEM + @"'
	AND YN_USE = 'Y'";

					DataTable dt = DBMgr.GetDataTable(query);

					if (dt.Rows.Count == 1)
					{
						flexE["CD_ITEM_AFTER"] = dt.Rows[0]["CD_ITEM"];						
					}
					else
					{
						H_CZ_MA_PITEM help = new H_CZ_MA_PITEM(CD_ITEM);

						if (help.ShowDialog() == DialogResult.OK)
						{
							flexE["CD_ITEM_AFTER"] = help.ITEM["CD_ITEM"];
						}
						else
						{
							e.Cancel = true;
							SendKeys.Send("{ESC}");
						}
					}
				}
			}
			else if (COLNAME == "QT_IO")
			{
				decimal QT_IO = Util.GetTO_Decimal(flexE["QT_IO"]);

				flexE["AM_EX"]	= QT_IO * Util.GetTO_Decimal(flexE["UM_EX"]);
				flexE["AM"]		= QT_IO * Util.GetTO_Decimal(flexE["UM"]);
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			if (rdo입고.Checked)
			{			
				flexS[0, "CD_PARTNER"] = "매입처코드";
				flexS[0, "LN_PARTNER"] = "매입처";

				flexS[1, "CD_PARTNER"] = "매입처코드";
				flexS[1, "LN_PARTNER"] = "매입처";
			}
			else
			{				
				flexS[0, "CD_PARTNER"] = "매출처코드";
				flexS[0, "LN_PARTNER"] = "매출처";

				flexS[1, "CD_PARTNER"] = "매출처코드";
				flexS[1, "LN_PARTNER"] = "매출처";
			}

			DBMgr db = new DBMgr();
			db.Procedure = "PS_CZ_PR_ITEM_REPLACE_REG_TARGET";
			db.AddParameter("CD_COMPANY", CD_COMPANY);
			db.AddParameter("NO", cbo번호.SelectedValue);
			db.AddParameter("NO_TXT", txt번호.Text);
			if (txt번호.Text == "")
			{
				db.AddParameter("DT", cbo일자.SelectedValue);
				db.AddParameter("DT_F", dtp일자.StartDateToString);
				db.AddParameter("DT_T", dtp일자.EndDateToString);
			}
			db.AddParameter("TP_TARGET", rdo입고.Checked ? "GR" : "RT");
			db.AddParameter("CD_PARTNER", ctx거래처.CodeValue);
			if (cbo품목코드.SelectedIndex == 0) db.AddParameter("CD_ITEM_PARTNER", txt품목코드.Text);
			if (cbo품목코드.SelectedIndex == 1) db.AddParameter("CD_ITEM", txt품목코드.Text);
			db.AddParameter("NM_ITEM_PARTNER", txt품목명.Text);

			DataTable dt = db.GetDataTable();
			flexS.Binding = dt;
			SetGridEffects();

			// 조회
			if (txt번호.Text != "")
			{
				grdSearch.Binding = DBMgr.GetDataTable("PS_CZ_PR_ITEM_REPLACE_RPT", CD_COMPANY, txt번호.Text);
				grdSearch.AutoSizeRows();
				grdSearch.Rows[0].Height = 28;
			}
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			if (!MsgAndSave(PageActionMode.Search)) return;
			Util.Clear(flexE);
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			if (!BeforeSave()) return;
			if (!MsgAndSave(PageActionMode.Save)) return;

			ShowMessage(PageResultMode.SaveGood);

			Util.Clear(flexE);
			OnToolBarSearchButtonClicked(null, null);
		}

		protected override bool BeforeSave()
		{
			if (!base.BeforeSave()) return false;
			
			if (!flexE.HasNormalRow) { ShowMessage(공통메세지.선택된자료가없습니다); return false; }
			if (ctx창고.CodeValue == "") { ShowMessage("창고가 선택되지 않았습니다!"); return false; }
			
			return true;
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !base.Verify()) return false;

			MsgControl.ShowMsg("잠시만 기다려주세요.");

			try
			{
				// ========== H
				DataTable dtH = new DataTable();
				dtH.Columns.Add("NO_EMP");
				dtH.Columns.Add("DC_RMK");

				dtH.Rows.Add();
				dtH.Rows[0]["NO_EMP"] = NO_EMP;
				dtH.Rows[0]["DC_RMK"] = txt비고.Text;

				// ========== L
				DataTable dtL = flexE.DataTable;

				// 창고 및 로케이션 지정
				foreach (DataRow row in dtL.Rows)
				{
					row["CD_SL"] = ctx창고.CodeValue;
				}

				// ========== Save
				string xmlH = Util.GetTO_Xml(dtH, RowState.Added);
				string xmlL = Util.GetTO_Xml(dtL, RowState.Added);
				DBMgr.ExecuteNonQuery("PX_CZ_PR_ITEM_REPLACE_REG", new object[] { xmlH, xmlL });
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				Util.ShowMessage(Util.GetErrorMessage(ex.Message));
				return false;
			}

			MsgControl.CloseMsg();
			return true;
		}

		#endregion
	}
}

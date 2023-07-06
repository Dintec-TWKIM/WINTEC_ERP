using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using DX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_MM_SUPPLIES_QTIO_RPT : PageBase
	{
		//public string NO_DOCU
		//{
		//	get
		//	{
		//		return this._flex입출고현황.HasNormalRow ? Global.MainFrame.LoginInfo.CompanyCode + "-" + _flex입출고현황["NO_IO"].ToString() : "";
		//	}
		//}

		P_CZ_MM_SUPPLIES_QTIO_RPT_BIZ _biz = new P_CZ_MM_SUPPLIES_QTIO_RPT_BIZ();
		P_CZ_MM_SUPPLIES_QTIO_RPT_GW _gw = new P_CZ_MM_SUPPLIES_QTIO_RPT_GW();
		public P_CZ_MM_SUPPLIES_QTIO_RPT()
		{
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			this.MainGrids = new FlexGrid[] { this._flex품목, this._flex입출고현황 };
			this._flex품목.DetailGrids = new FlexGrid[] { this._flex입출고현황 };

			#region _flex품목
			this._flex품목.BeginSetting(2, 1, false);

			this._flex품목.SetCol("NM_ITEM", "품목명", 200);
			this._flex품목.SetCol("NM_MAKER", "MAKER", 100);
			this._flex품목.SetCol("NO_MODEL", "MODEL", 100);
			this._flex품목.SetCol("UNIT", "단위", 45);
			this._flex품목.SetCol("NM_DEPT", "관리부서", 100);
			this._flex품목.SetCol("QT_OPEN", "기초재고", 80, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flex품목.SetCol("QT_GR", "입고", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex품목.SetCol("QT_GI", "출고", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex품목.SetCol("QT_INV", "현재고", 80, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flex품목[0, _flex품목.Cols["QT_GR"].Index] = "입출고현황";
			this._flex품목[0, _flex품목.Cols["QT_GI"].Index] = "입출고현황";
			this._flex품목[0, _flex품목.Cols["QT_INV"].Index] = "입출고현황";

			this._flex품목.SetDataMap("UNIT", MA.GetCode("MA_B000004"), "CODE", "NAME");
			SetGrid.ColumnStyle(_flex품목, _flex품목.Cols["QT_INV"].Index, GridStyle.Bold);
			this._flex품목.Cols["UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			this._flex품목.Cols["NM_DEPT"].TextAlign = TextAlignEnum.CenterCenter;
			this._flex품목.SettingVersion = "22.11.11.01";
			this._flex품목.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region _flex입출고현황
			this._flex입출고현황.BeginSetting(2, 1, false);

			//this._flex입출고현황.SetCol("NM_GW_STATUS", "결재상태", 60);
			this._flex입출고현황.SetCol("FG_PS", "구분", 40);
			this._flex입출고현황.SetCol("DT_IO", "입/출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex입출고현황.SetCol("QT_OPEN", "기초재고", 57, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flex입출고현황.SetCol("UM_GR", "단가", 75, false, typeof(decimal), FormatTpType.MONEY);
			this._flex입출고현황.SetCol("QT_GR", "수량", 57, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex입출고현황.SetCol("AM_GR", "매입가", 75, false, typeof(decimal), FormatTpType.MONEY);

			this._flex입출고현황.SetCol("QT_GI", "수량", 57, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex입출고현황.SetCol("AM_GI", "매출가", 75, false, typeof(decimal), FormatTpType.MONEY);

			this._flex입출고현황.SetCol("QT_INV", "현재고", 57, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex입출고현황.SetCol("NO_EMP", "담당자코드", 80);
			this._flex입출고현황.SetCol("NM_KOR", "담당자", 80);
			this._flex입출고현황.SetCol("CD_PARTNER", "매입/매출처코드", 80);
			this._flex입출고현황.SetCol("LN_PARTNER", "매입/매출처명", 150);
			this._flex입출고현황.SetCol("NO_DOCU", "문서번호", 100);
			this._flex입출고현황.SetCol("DC_RMK", "비고", 200);

			this._flex입출고현황[0, _flex입출고현황.Cols["UM_GR"].Index] = "입고";
			this._flex입출고현황[0, _flex입출고현황.Cols["QT_GR"].Index] = "입고";
			this._flex입출고현황[0, _flex입출고현황.Cols["AM_GR"].Index] = "입고";

			this._flex입출고현황[0, _flex입출고현황.Cols["QT_GI"].Index] = "출고";
			this._flex입출고현황[0, _flex입출고현황.Cols["AM_GI"].Index] = "출고";

			this._flex입출고현황.SetDataMap("FG_PS", MA.GetCodeUser(new string[] { "1", "2" }, new string[] { "입고", "출고" }), "CODE", "NAME");

			this._flex입출고현황.SetOneGridBinding(new object[] { }, new IUParentControl[] { this.oneGrid2 });
			this._flex입출고현황.SetBindningRadioButton(new RadioButtonExt[]{ this.rdo입고1, this.rdo출고1 }, new string[] { "1", "2" });

			this._flex입출고현황.Cols["FG_PS"].Visible = false;
			this._flex입출고현황.Cols["NO_EMP"].Visible = false;
			this._flex입출고현황.Cols["CD_PARTNER"].Visible = false;

			this._flex입출고현황.Cols["NM_KOR"].TextAlign = TextAlignEnum.CenterCenter;
			SetGrid.ColumnStyle(_flex입출고현황, _flex입출고현황.Cols["QT_GR"].Index, GridStyle.Bold, GridStyle.FontBlue);
			SetGrid.ColumnStyle(_flex입출고현황, _flex입출고현황.Cols["QT_GI"].Index, GridStyle.Bold, GridStyle.FontRed);
			SetGrid.ColumnStyle(_flex입출고현황, _flex입출고현황.Cols["QT_INV"].Index, GridStyle.Bold, GridStyle.FontGreen);

			this._flex입출고현황.SettingVersion = "22.11.11.01";
			this._flex입출고현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			this._flex입출고현황.SetExceptSumCol("UM_GR", "AM_GR", "AM_GI", "QT_INV");
			#endregion
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.splitContainer1.SplitterDistance = 1824;

			this.dtp조회기간.StartDateToString = DateTime.Now.Year.ToString() + "0101";
			this.dtp조회기간.EndDateToString = Util.GetToday();
		}

		private void InitEvent()
		{
			this._flex품목.AfterRowChange += new RangeEventHandler(this._flex품목_AfterRowChange);
			this._flex입출고현황.AfterRowChange += new RangeEventHandler(this._flex입출고현황_AfterRowChange);
			this.rdo입고1.CheckedChanged += new EventHandler(this._rdo_CheckedChanged);
			this.btn소모품관리.Click += new EventHandler(this.Btn소모품관리_Click);
			this.btn전자결재.Click += new EventHandler(this.Btn전자결재_Click);

			this.cur매입단가.TextChanged += new EventHandler(this.cur_TextChanged);
			this.cur입고수량.TextChanged += new EventHandler(this.cur_TextChanged);
		}

		private void cur_TextChanged(object sender, EventArgs e)
		{
			try
			{
				switch (((CurrencyTextBox)sender).Name)
				{
					case "cur매입단가":
					case "cur입고수량":
						this.cur매입가.Text = (D.GetDecimal(this.cur매입단가.Text) * D.GetDecimal(this.cur입고수량.Text)).ToString();
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _rdo_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.rdo입고1.Checked)
				{
					this.bpPnl매입단가.Enabled = true;
					this.bpPnl입고수량.Enabled = true;
					this.bpPnl출고수량.Enabled = false;
					this.bpPnl매출가.Enabled = false;
					this.cur출고수량.Text = "";
					this.cur매출가.Text = "";
				}
				else
				{
					this.bpPnl매입단가.Enabled = false;
					this.bpPnl입고수량.Enabled = false;
					this.bpPnl출고수량.Enabled = true;
					this.bpPnl매출가.Enabled = true;
					this.cur매입단가.Text = "";
					this.cur입고수량.Text = "";
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex품목_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt = null;
			string filter;
			string cdItem = this._flex품목["CD_ITEM"].ToString();
			string nmItem = this._flex품목["NM_ITEM"].ToString();
			decimal qtOpen = D.GetDecimal(this._flex품목["QT_OPEN"]);
			try
			{
				if (!this._flex품목.HasNormalRow) return;
				filter = "CD_ITEM = '" + this._flex품목["CD_ITEM"].ToString() + "'";

				if (this._flex품목.DetailQueryNeed)
				{
					string FG_PS = "";
					if (rdo입고.Checked) FG_PS = "1";
					if (rdo출고.Checked) FG_PS = "2";
					object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
													   cdItem,
													   qtOpen,
													   this.dtp조회기간.StartDateToString,
													   this.dtp조회기간.EndDateToString,
													   FG_PS };
					dt = this._biz.SearchDetail(objArray);
					dt.Columns.Add("QT_OPEN", typeof(decimal));
					dt.Rows.InsertAt(dt.NewRow(), 0);
					dt.Rows[0]["CD_ITEM"] = cdItem;
					dt.Rows[0]["NM_ITEM"] = nmItem;
					dt.Rows[0]["RN_DATE"] = "000000";
					dt.Rows[0]["QT_OPEN"] = qtOpen;
					dt.Rows[0]["QT_INV"] = qtOpen;
				}

				this._flex입출고현황.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex입출고현황_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				if (!this._flex품목.HasNormalRow) return;

				if (!string.IsNullOrEmpty(this._flex입출고현황["FG_PS"].ToString()) && !string.IsNullOrEmpty(this._flex입출고현황["RN_DATE"].ToString()))
				{
					this.oneGrid2.Enabled = true;
					this.rdo입고1.Enabled = false;
					this.rdo출고1.Enabled = false;
				}
				else if (string.IsNullOrEmpty(this._flex입출고현황["DT_IO"].ToString()) && !string.IsNullOrEmpty(this._flex입출고현황["RN_DATE"].ToString()))
				{
					this.oneGrid2.Enabled = false;
				}
				else
				{
					this.oneGrid2.Enabled = true;
					this.rdo입고1.Enabled = true;
					this.rdo출고1.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn소모품관리_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_MM_SUPPLIES_QTIO_RPT_SUB dialog = new P_CZ_MM_SUPPLIES_QTIO_RPT_SUB();

				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn전자결재_Click(object sender, EventArgs e)
		{
			string 년;
			DataTable dt;
			try
			{
				if (!_flex입출고현황.HasNormalRow) return;
				if (string.IsNullOrEmpty(this._flex입출고현황["FG_PS"].ToString())) return;
				if (this._flex입출고현황.IsDataChanged)
				{
					this.ShowMessage("변경 사항이 있습니다. 저장 후 다시 시도해 주세요.");
					return;
				}
				년 = this._flex입출고현황["DT_IO"].ToString().Left(4);
				dt = this._flex입출고현황.데이터테이블("YY = '" + 년 + "' AND CD_ITEM = '" + this._flex품목["CD_ITEM"].ToString() + "'");
				int len = dt.Rows.Count;
				if (len > 10)
				{
					for (int i = 0; i < len - 10; i++) dt.Rows[i].Delete();
				}
				dt.AcceptChanges();
				if (this._gw.전자결재(this._flex품목.GetDataRow(this._flex품목.Row),
									 this._flex입출고현황.GetDataRow(this._flex입출고현황.Row),
									 dt))
				{
					this.ShowMessage(공통메세지._작업을완료하였습니다, this.DD("전자결재"));
					this.OnToolBarSearchButtonClicked(null, null);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!base.BeforeSearch()) return;

				this._flex품목.Binding = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
																		 this.txt품목명.Text,
																		 this.dtp조회기간.StartDateToString,
																		 this.dtp조회기간.EndDateToString });
				if (!this._flex품목.HasNormalRow)
					this.ShowMessage(PageResultMode.SearchNoData);

				this._flex입출고현황.Cols["UM_GR"].Visible = true;
				this._flex입출고현황.Cols["QT_GR"].Visible = true;
				this._flex입출고현황.Cols["AM_GR"].Visible = true;

				this._flex입출고현황.Cols["QT_GI"].Visible = true;
				this._flex입출고현황.Cols["AM_GI"].Visible = true;

				if (rdo입고.Checked)
				{
					this._flex입출고현황.Cols["QT_GI"].Visible = false;
					this._flex입출고현황.Cols["AM_GI"].Visible = false;
				}
				if (rdo출고.Checked)
				{
					this._flex입출고현황.Cols["UM_GR"].Visible = false;
					this._flex입출고현황.Cols["QT_GR"].Visible = false;
					this._flex입출고현황.Cols["AM_GR"].Visible = false;
				}

				this._flex입출고현황.AcceptChanges();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarAddButtonClicked(sender, e);
				if (!this.BeforeAdd()) return;
				if (!this._flex품목.HasNormalRow) return;

				this._flex입출고현황.Rows.Add();
				this._flex입출고현황.Row = this._flex입출고현황.Rows.Count - 1;

				this._flex입출고현황["CD_ITEM"] = this._flex품목["CD_ITEM"].ToString();
				this._flex입출고현황["NM_ITEM"] = this._flex품목["NM_ITEM"].ToString();
				this._flex입출고현황["FG_PS"] = "1";
				this._flex입출고현황["DT_IO"] = Global.MainFrame.GetDateTimeToday().ToString("yyyyMMdd");
				this._flex입출고현황["NO_EMP"] = Global.MainFrame.LoginInfo.UserID;
				this._flex입출고현황["NM_KOR"] = Global.MainFrame.LoginInfo.UserName;

				this._flex입출고현황.AddFinished();
				this._flex입출고현황.Focus();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool BeforeDelete()
		{
			if (!base.BeforeDelete()) return false;

			if (this._flex입출고현황["RN_DATE"].ToString() == "000000") return false;

			return true;
		}

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarDeleteButtonClicked(sender, e);
				if (!this.BeforeDelete() || !this._flex입출고현황.HasNormalRow) return;

				//if (this._flex입출고현황["CD_GW_STATUS"].ToString() == "0" || this._flex입출고현황["CD_GW_STATUS"].ToString() == "1")
				//{
				//	this.ShowMessage("CZ_@ 상태는 삭제할 수 없습니다.", D.GetString(this._flex입출고현황["NM_GW_STATUS"]));
				//	return;
				//}

				this._flex입출고현황.Rows.Remove(this._flex입출고현황.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool BeforeSave()
		{
			if (!base.BeforeSave()) return false;

			DataRow[] dataRowArray = this._flex입출고현황.DataTable.AsEnumerable().Where(x => x.RowState == DataRowState.Modified || x.RowState == DataRowState.Added).ToArray();

			if (dataRowArray.AsEnumerable().Where(x => x["FG_PS"].ToString() == "1" && D.GetDecimal(x["QT_GR"]) == 0).Count() > 0)
			{
				this.ShowMessage(공통메세지._은는필수입력항목입니다, "수량");
				return false;
			}

			if (dataRowArray.AsEnumerable().Where(x => x["FG_PS"].ToString() == "2" && D.GetDecimal(x["QT_GI"]) == 0).Count() > 0)
			{
				this.ShowMessage(공통메세지._은는필수입력항목입니다, "수량");
				return false;
			}

			if (dataRowArray.AsEnumerable().Where(x => x["FG_PS"].ToString() == "1" && string.IsNullOrEmpty(x["NO_DOCU"].ToString())).Count() > 0)
			{
				this.ShowMessage(공통메세지._은는필수입력항목입니다, "입고시 문서번호");
				return false;
			}

			return true;
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSaveButtonClicked(sender, e);

				if (!this.BeforeSave()) return;

				if (this.MsgAndSave(PageActionMode.Save))
					this.ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			string seq = "";
			if (!base.SaveData() || !base.Verify()) return false;

			if (!this._flex입출고현황.IsDataChanged) return false;

			DataTable changes = this._flex입출고현황.GetChanges();
			if (changes == null) return true;
			foreach (DataRow dr in changes.Rows)
			{
				if (dr.RowState == DataRowState.Added)
				{
					if (string.IsNullOrEmpty(dr["NO_IO"].ToString()))
					{
						seq = (string)this.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "15", dr["DT_IO"].ToString().Substring(0, 6));
						dr["NO_IO"] = seq;
					}
				}
			}
			if (!this._biz.Save(changes)) return false;

			this._flex입출고현황.AcceptChanges();
			this.OnToolBarSearchButtonClicked(null, null);

			return true;
		}

		private void txt비고1_TextChanged(object sender, EventArgs e)
		{

		}
	}
}

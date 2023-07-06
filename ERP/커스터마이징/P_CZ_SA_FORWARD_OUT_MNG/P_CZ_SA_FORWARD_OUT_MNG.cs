using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
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
	public partial class P_CZ_SA_FORWARD_OUT_MNG : PageBase
	{
		P_CZ_SA_FORWARD_OUT_MNG_BIZ _biz = new P_CZ_SA_FORWARD_OUT_MNG_BIZ();

		public P_CZ_SA_FORWARD_OUT_MNG()
		{
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			string query = @"SELECT '' AS CODE,
	   '' AS NAME
UNION ALL
SELECT MC.CD_COMPANY AS CODE,
	   MC.NM_COMPANY AS NAME
FROM MA_COMPANY MC WITH(NOLOCK)
WHERE MC.CD_COMPANY IN ('K100', 'K200', 'S100')
ORDER BY CODE ASC";

			DataTable dt = DBHelper.GetDataTable(query);

			this.cbo회사.DataSource = dt;
			this.cbo회사.DisplayMember = "NAME";
			this.cbo회사.ValueMember = "CODE";
			this.cbo회사.SelectedValue = Global.MainFrame.LoginInfo.CompanyCode;
		}

		private void InitGrid()
		{
			this.MainGrids = new FlexGrid[] { this._flex };
			this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

			#region 등록
			this._flex.BeginSetting(2, 1, false);

			this._flex.SetCol("NM_COMPANY", "회사명", 100);
			this._flex.SetCol("NO_GIR", "의뢰번호", 100, true);
			this._flex.SetCol("NO_PACK", "포장번호", 100, true);
			this._flex.SetCol("CD_PARTNER", "포워더코드", 100, true);
			this._flex.SetCol("LN_PARTNER", "포워더명", 100);
			this._flex.SetCol("DT_FORWARD", "출고일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex.SetCol("DC_RMK", "비고", 100, true);

			this._flex.SettingVersion = "0.0.0.1";
			this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			this._flex.VerifyNotNull = new string[] { "CD_PARTNER", "DT_FORWARD", "NO_GIR" };

			this._flex.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_TABLE_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }, new string[] { "CODE", "NAME" }, ResultMode.FastMode);
			#endregion

			#region 조회

			#region Header
			this._flexH.BeginSetting(2, 1, false);

			this._flexH.SetCol("NM_COMPANY", "회사명", 100);
			this._flexH.SetCol("CD_PARTNER", "포워더코드", 100);
			this._flexH.SetCol("LN_PARTNER", "포워더명", 100);
			this._flexH.SetCol("DT_FORWARD", "출고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("QT_BOX", "포장박스", 100, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flexH.SettingVersion = "0.0.0.1";
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region Line
			this._flexL.BeginSetting(2, 1, false);

			this._flexL.SetCol("NO_GIR", "의뢰번호", 100);
			this._flexL.SetCol("NO_PACK", "포장번호", 100);

			this._flexL.SettingVersion = "0.0.0.1";
			this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#endregion
		}

		private void InitEvent()
		{
			this.ctx납품처.QueryBefore += Control_QueryBefore;

			this._flex.BeforeCodeHelp += _flex_BeforeCodeHelp;
			this._flexH.AfterRowChange += _flexH_AfterRowChange;
		}

		private void Control_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.cbo회사.SelectedValue.ToString()))
				{
					this.ShowMessage("회사가 선택되어 있지 않습니다.");
					return;
				}

				e.HelpParam.P00_CHILD_MODE = "납품처";
				e.HelpParam.P61_CODE1 = "CD_PARTNER AS CODE, LN_PARTNER AS NAME";
				e.HelpParam.P62_CODE2 = "CZ_MA_DELIVERY";
				e.HelpParam.P63_CODE3 = string.Format("WHERE CD_COMPANY = '{0}' AND ISNULL(YN_USE, 'N') = 'Y'", this.cbo회사.SelectedValue.ToString());
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
		{
			FlexGrid grid;

			try
			{
				if (string.IsNullOrEmpty(this.cbo회사.SelectedValue.ToString()))
				{
					this.ShowMessage("회사가 선택되어 있지 않습니다.");
					return;
				}

				grid = ((FlexGrid)sender);

				switch (grid.Cols[e.Col].Name)
				{
					case "CD_PARTNER":
						e.Parameter.P00_CHILD_MODE = "납품처";

						e.Parameter.P61_CODE1 = @"MD.CD_PARTNER AS CODE,
												  MD.LN_PARTNER AS NAME";
						e.Parameter.P62_CODE2 = @"CZ_MA_DELIVERY MD WITH(NOLOCK)";
						e.Parameter.P63_CODE3 = "WHERE MD.CD_COMPANY = '" + this.cbo회사.SelectedValue.ToString() + "'";
						break;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			string key, key1, key2, filter;
			DataTable dt = null;

			try
			{
				key = this._flexH["CD_COMPANY"].ToString();
				key1 = this._flexH["CD_PARTNER"].ToString();
				key2 = this._flexH["DT_FORWARD"].ToString();
				
				filter = "CD_COMPANY = '" + key + "' AND CD_PARTNER ='" + key1 + "' AND DT_FORWARD = '" + key2 + "'";

				if (this._flexH.DetailQueryNeed)
				{
					dt = this._biz.SearchLine(new object[] { key,
														     key1,
															 key2 });
				}

				this._flexL.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (string.IsNullOrEmpty(this.cbo회사.SelectedValue.ToString()))
				{
					this.ShowMessage("회사가 선택되어 있지 않습니다.");
					return;
				}

				if (!this.BeforeSearch()) return;

				if (this.tabControl1.SelectedTab == this.tpg등록)
				{
					this._flex.Binding = this._biz.Search(new object[] { this.cbo회사.SelectedValue.ToString(),
																		 this.dtp출고일자.StartDateToString,
																		 this.dtp출고일자.EndDateToString,
																		 this.ctx납품처.CodeValue });

					if (!this._flex.HasNormalRow)
						ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
				else
				{
					this._flexH.Binding = this._biz.SearchHeader(new object[] { this.cbo회사.SelectedValue.ToString(),
																				this.dtp출고일자.StartDateToString,
																				this.dtp출고일자.EndDateToString,
																				this.ctx납품처.CodeValue });

					if (!this._flexH.HasNormalRow)
						ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSaveButtonClicked(sender, e);

				if (!this.BeforeSave()) return;

				if (MsgAndSave(PageActionMode.Save))
					ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !base.Verify()) return false;
			if (this._flex.IsDataChanged == false) return false;

			if (!this._biz.Save(this._flex.GetChanges())) return false;

			this._flex.AcceptChanges();

			return true;
		}

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarAddButtonClicked(sender, e);

				if (!BeforeAdd()) return;

				if (string.IsNullOrEmpty(this.cbo회사.SelectedValue.ToString()))
				{
					this.ShowMessage("회사가 선택되어 있지 않습니다.");
					return;
				}

				this._flex.Rows.Add();
				this._flex.Row = _flex.Rows.Count - 1;

				this._flex["CD_COMPANY"] = this.cbo회사.SelectedValue.ToString();
				this._flex["DT_FORWARD"] = Global.MainFrame.GetStringToday;

				this._flex.AddFinished();
				this._flex.Focus();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarDeleteButtonClicked(sender, e);

				if (!this.BeforeDelete() || !this._flex.HasNormalRow) return;

				this._flex.Rows.Remove(this._flex.Row);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
	}
}

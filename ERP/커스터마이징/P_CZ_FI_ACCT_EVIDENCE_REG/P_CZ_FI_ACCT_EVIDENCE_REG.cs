using System;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Forms.Help;

namespace cz
{
	public partial class P_CZ_FI_ACCT_EVIDENCE_REG : PageBase
	{
		#region 초기화
		P_CZ_FI_ACCT_EVIDENCE_REG_BIZ _biz;

		public P_CZ_FI_ACCT_EVIDENCE_REG()
		{
			StartUp.Certify(this);
			InitializeComponent();

			this._biz = new P_CZ_FI_ACCT_EVIDENCE_REG_BIZ();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
			this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

			#region Header
			this._flexH.BeginSetting(1, 1, false);

			this._flexH.SetCol("CD_ACCT", "계정코드", 100);
			this._flexH.SetCol("NM_ACCT", "계정명", 100);

			this._flexH.SettingVersion = "1.0.0.0";
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			this._flexH.SetCodeHelpCol("CD_ACCT", HelpID.P_FI_ACCTCODE_SUB, ShowHelpEnum.Always, new string[] { "CD_ACCT", "NM_ACCT" }, new string[] { "CD_ACCT", "NM_ACCT" });
			#endregion

			#region Line
			this._flexL.BeginSetting(1, 1, false);

			this._flexL.SetCol("FG_TAX", "과세구분코드", 100, true);
			this._flexL.SetCol("NM_TAX", "과세구분", 100);
			this._flexL.SetCol("TP_EVIDENCE", "증빙코드", 100, true);
			this._flexL.SetCol("NM_EVIDENCE", "증빙", 100);

			this._flexL.SetCodeHelpCol("FG_TAX", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "FG_TAX", "NM_TAX" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });
            this._flexL.SetCodeHelpCol("TP_EVIDENCE", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "TP_EVIDENCE", "NM_EVIDENCE" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });

            this._flexL.VerifyDuplicate = new string[] { "FG_TAX" };
            this._flexL.VerifyNotNull = new string[] { "TP_EVIDENCE" };

			this._flexL.SettingVersion = "1.0.0.0";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			#endregion
		}

		private void InitEvent()
		{
			this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
			this._flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flexL_BeforeCodeHelp);

			this.btn추가.Click += new EventHandler(this.btn추가_Click);
			this.btn제거.Click += new EventHandler(this.btn제거_Click);
		}
		#endregion

		#region 메인버튼 이벤트
		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!BeforeSearch()) return;

				this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode });

				if (!_flexH.HasNormalRow)
					ShowMessage(공통메세지.조건에해당하는내용이없습니다);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarAddButtonClicked(sender, e);

				if (!BeforeAdd()) return;

				this._flexH.Rows.Add();
				this._flexH.Row = _flexH.Rows.Count - 1;

				this._flexH.AddFinished();
				this._flexH.Focus();
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

				if (!this.BeforeDelete() || !this._flexH.HasNormalRow) return;

                if (this._flexL.DataTable.Select("CD_ACCT = '" + D.GetString(this._flexH["CD_ACCT"]) + "'").Length > 0)
                {
                    this.ShowMessage("하위 항목이 있어서 삭제 할 수 없습니다.");
                    return;
                }

				this._flexH.Rows.Remove(this._flexH.Row);
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
			if (this._flexH.IsDataChanged == false && this._flexL.IsDataChanged == false) return false;

			if (!_biz.Save(this._flexH.GetChanges(), this._flexL.GetChanges())) return false;

			this._flexH.AcceptChanges();
			this._flexL.AcceptChanges();

			return true;
		}
		#endregion

		#region 그리드 이벤트
		private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt;
			string key, filter;

			try
			{
				if (this._flexH.RowState() == DataRowState.Added)
				{
					this.btn추가.Enabled = false;
					this.btn제거.Enabled = false;
				}
				else
				{
					this.btn추가.Enabled = true;
					this.btn제거.Enabled = true;
				}

				dt = null;
				key = this._flexH["CD_ACCT"].ToString();
				filter = "CD_ACCT = '" + key + "'";

				if (this._flexH.DetailQueryNeed == true)
				{
					dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															   key });
				}

				this._flexL.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexL_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
		{
			FlexGrid grid;

			try
			{
				grid = ((FlexGrid)sender);

				switch (grid.Cols[e.Col].Name)
				{
					case "FG_TAX":
						e.Parameter.P41_CD_FIELD1 = "MA_B000046";
						break;
					case "TP_EVIDENCE":
						e.Parameter.P41_CD_FIELD1 = "FI_F000105";
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion

		#region 컨트롤 이벤트
		private void btn추가_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow) return;

				this._flexL.Rows.Add();
				this._flexL.Row = this._flexL.Rows.Count - 1;

				this._flexL["CD_ACCT"] = D.GetString(this._flexH["CD_ACCT"]);
                this._flexL["NO_LINE"] = (D.GetDecimal(this._flexL.DataTable.Compute("MAX(NO_LINE)", "CD_ACCT = '" + D.GetString(this._flexH["CD_ACCT"]) + "'")) + 1);

				this._flexL.AddFinished();
				this._flexL.Col = this._flexL.Cols.Fixed;
				this._flexL.Focus();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn제거_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexL.HasNormalRow) return;

				this._flexL.GetDataRow(this._flexL.Row).Delete();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		#endregion
	}
}

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
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
	public partial class P_CZ_PR_MARKING_REG_SUB : Duzon.Common.Forms.CommonDialog
	{
		P_CZ_PR_MARKING_REG_SUB_BIZ _biz = new P_CZ_PR_MARKING_REG_SUB_BIZ();

		public P_CZ_PR_MARKING_REG_SUB()
		{
			InitializeComponent();

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			this._flex.BeginSetting(1, 1, false);

			this._flex.SetCol("CD_ITEM", "품목코드", 100, true);
			this._flex.SetCol("NM_ITEM", "품목명", 100);
			this._flex.SetCol("CD_PARTNER", "매출처코드", 100, true);
			this._flex.SetCol("LN_PARTNER", "매출처명", 100);
			this._flex.SetCol("YN_CERT", "CERT번호출력여부", 100, true, CheckTypeEnum.Y_N);

            this._flex.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM" }, new string[] { "CD_ITEM", "NM_ITEM" });
            this._flex.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });

            this._flex.SettingVersion = "0.0.0.1";
			this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
            this.btn조회.Click += Btn조회_Click;
            this.btn추가.Click += Btn추가_Click;
            this.btn제거.Click += Btn제거_Click;
            this.btn저장.Click += Btn저장_Click;

			this._flex.BeforeCodeHelp += _flex_BeforeCodeHelp;

			this.ctx품목.QueryBefore += Ctx품목_QueryBefore;
        }

        private void Ctx품목_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P09_CD_PLANT = Global.MainFrame.LoginInfo.CdPlant;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
		{
            try
            {
                e.Parameter.P09_CD_PLANT = Global.MainFrame.LoginInfo.CdPlant;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

		private void Btn조회_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                     this.ctx품목.CodeValue,
                                                     this.ctx매출처.CodeValue });

                this._flex.Binding = dt;

                if (!this._flex.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;

                this._flex.AddFinished();
                this._flex.Col = this._flex.Cols.Fixed;
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Btn저장_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._biz.Save(this._flex.GetChanges())) return;

                this._flex.AcceptChanges();

                Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Btn제거_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                this._flex.GetDataRow(this._flex.Row).Delete();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

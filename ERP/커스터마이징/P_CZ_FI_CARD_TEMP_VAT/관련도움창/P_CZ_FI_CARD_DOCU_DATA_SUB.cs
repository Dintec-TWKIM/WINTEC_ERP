using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_FI_CARD_DOCU_DATA_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_FI_CARD_TEMP_VAT_BIZ _biz = new P_CZ_FI_CARD_TEMP_VAT_BIZ();
        
        public P_CZ_FI_CARD_DOCU_DATA_SUB()
        {
            this.InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("S", "S", 60, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("ACCT_NO", "카드번호", 200, false);
            this._flex.SettingVersion = "1.1";
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btnCopy.Click += new EventHandler(this.btnCopy_Click);
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            this.Search();
        }

        private void btnOk_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.OK;

        private void btnCopy_Click(object sender, EventArgs e)
        {
            DataTable table = new DataView(this._flex.DataTable, "S = 'Y'", "", DataViewRowState.CurrentRows).ToTable();
            if (table == null || table.Rows.Count == 0)
            {
                int num1 = (int)Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
            }
            else
            {
                if (!this._biz.AcctNOChageCompany(Duzon.ERPU.MF.Common.Common.MultiString(table, "ACCT_NO", "|")))
                    return;
                int num2 = (int)Global.MainFrame.ShowMessage("계좌코드가 정상적으로 처리되었습니다");
                this.Search();
            }
        }

        private void Search()
        {
            this._flex.Binding = (object)this._biz.AcctNoCodeCopy();
            if (this._flex.HasNormalRow)
                return;
            int num = (int)Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
        }
    }
}
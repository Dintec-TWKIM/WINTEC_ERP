using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_FI_Z_ISEC_GWSEND_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_FI_CARD_TEMP_VAT_BIZ _biz;
        private DataRow[] _rows;

        public P_FI_Z_ISEC_GWSEND_SUB()
        {
            this.InitializeComponent();
            this._biz = new P_CZ_FI_CARD_TEMP_VAT_BIZ();
        }

        public P_FI_Z_ISEC_GWSEND_SUB(DataRow[] SeletedRows)
          : this()
          => this._rows = SeletedRows;

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitEvent();
            this.InitOneGrid();
            this.InitGird();
        }

        private void InitEvent()
        {
            this.btn검색.Click += new EventHandler(this.btn검색_Click);
            this.btn확인.Click += new EventHandler(this.btn확인_Click);
            this.btn취소.Click += new EventHandler(this.btn취소_Click);
        }

        private void InitOneGrid()
        {
            this.oneGrid1.UseCustomLayout = true;
            this.bppnl검색.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
        }

        private void InitGird()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("CODE", "지출결의 코드", 100);
            this._flex.SetCol("NAME", "지출결의 명", 100);
            this._flex.UseMultySorting = true;
            this._flex.ExtendLastCol = true;
            this._flex.SettingVersion = "1.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        protected override void InitPaint() => this.조회();

        private void btn검색_Click(object sender, EventArgs e)
        {
            try
            {
                this.조회();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn확인_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dataRow = this._flex.GetDataRow(this._flex.Row);
                bool flag = true;
                foreach (DataRow row in this._rows)
                {
                    if (!this._biz.Save_ISEC_SUB(new List<object>()
          {
            row["ACCT_NO"],
            row["BANK_CODE"],
            row["TRADE_DATE"],
            row["TRADE_TIME"],
            row["SEQ"],
            dataRow["CODE"]
          }.ToArray()))
                        flag = false;
                }
                if (flag)
                {
                    int num = (int)Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                    this.DialogResult = DialogResult.Yes;
                }
                else
                {
                    int num = (int)Global.MainFrame.ShowMessage(공통메세지.자료저장중오류가발생하였습니다);
                    this.DialogResult = DialogResult.No;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn취소_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 조회() => this._flex.Binding = (object)this._biz.Search_ISEC_SUB(new List<object>()
    {
      (object) MA.Login.회사코드,
      (object) this.txt검색.Text
    }.ToArray());
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.ERPU;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;

namespace cz
{
    public partial class P_CZ_FI_BANK_SEND_BAN_DOCU : Duzon.Common.Forms.CommonDialog
    {
        public string _sCdTacct;
        public string _sProcess;

        public P_CZ_FI_BANK_SEND_BAN_DOCU()
        {
            this.InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitEvent();
        }

        private void InitEvent()
        {
            this.rdo입력계정.CheckedChanged += new EventHandler(this.Control_CheckedChanged);
            this.rdo등록계정.CheckedChanged += new EventHandler(this.Control_CheckedChanged);
            this.btn전표처리.Click += new EventHandler(this.btn전표처리_Click);
            this.ctx입력계정.QueryBefore += new BpQueryHandler(this.ctx입력계정_QueryBefore);
        }

        private void btn전표처리_Click(object sender, EventArgs e)
        {
            if (this.rdo입력계정.Checked)
            {
                if (this.ctx입력계정.IsEmpty())
                {
                    Global.MainFrame.ShowMessage("WK1_004", this.rdo입력계정.Text);
                    this.ctx입력계정.Focus();
                    return;
                }
            }
            try
            {
                this._sCdTacct = !this.rdo등록계정.Checked ? this.ctx입력계정.CodeValue : "";
                this._sProcess = !this.rdo일괄처리.Checked ? "2" : "1";
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Control_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdo등록계정.Checked)
            {
                this.ctx입력계정.Clear();
                this.ctx입력계정.ReadOnly = ReadOnly.TotalReadOnly;
            }
            else
            {
                this.ctx입력계정.ReadOnly = ReadOnly.None;
            }
        }

        private void ctx입력계정_QueryBefore(object sender, BpQueryArgs e)
        {
            e.HelpParam.P33_TP_ACSTATS = "2";
        }
    }
}
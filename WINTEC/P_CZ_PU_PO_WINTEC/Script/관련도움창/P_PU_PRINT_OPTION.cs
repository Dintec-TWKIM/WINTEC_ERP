using Duzon.Common.Forms;
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
    public partial class P_PU_PRINT_OPTION : Duzon.Common.Forms.CommonDialog
    {
        private string _parent_dt_po = string.Empty;
        private string _gstr_return = string.Empty;
        public P_PU_PRINT_OPTION(string ls_dt_po)
        {
            this.InitializeComponent();
            this._parent_dt_po = ls_dt_po;
        }
        protected override void InitLoad()
        {
            base.InitLoad();
            this.tb_DT_PO.Text = this._parent_dt_po;
        }

        private void InitEvent()
        {
            this.m_btn_exit.Click += new EventHandler(this.m_btn_exit_Click);
            this.m_btn_apply.Click += new EventHandler(this.m_btn_apply_Click);
        }

        private void m_btn_apply_Click(object sender, EventArgs e)
        {
            try
            {
                this._gstr_return = this.tb_DT_PO.Text;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void m_btn_exit_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.Cancel;

        public string gstr_return => this._gstr_return;
    }
}

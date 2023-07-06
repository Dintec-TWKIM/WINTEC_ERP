using Duzon.Common.Controls;
using Duzon.Common.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_FI_Z_SEAHST_NOTICE_SUB : Duzon.Common.Forms.CommonDialog
    {
        public P_FI_Z_SEAHST_NOTICE_SUB()
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
            this.btn확인.Click += new EventHandler(this.btn확인_Click);
        }

        private void btn확인_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
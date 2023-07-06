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
    public partial class P_CZ_SA_DEFERRED_DELIVERY_MNG_SUB : Duzon.Common.Forms.CommonDialog
    {
        public P_CZ_SA_DEFERRED_DELIVERY_MNG_SUB()
        {
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitEvent();
        }

        private void InitEvent()
        {
            this.btn반영.Click += new EventHandler(this.btn반영_Click);
            this.btn취소.Click += new EventHandler(this.btn취소_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.rdo납기도래.Checked = false;
            this.rdo납기문의.Checked = false;

            this.rdo발송자표시.Checked = false;
            this.rdo발송자미표시.Checked = false;

            this.rdo서명표시.Checked = false;
            this.rdo서명미표시.Checked = false;

            this.rdo수동발송.Checked = false;
            this.rdo자동발송.Checked = false;

            this.rdo자동발송문구표시.Checked = false;
            this.rdo자동발송문구미표시.Checked = false;
 
            if (Settings.Default.메일유형 == this.rdo납기문의.Text)
                this.rdo납기문의.Checked = true;
            else
                this.rdo납기도래.Checked = true;

            if (Settings.Default.발송자표시 == true)
                this.rdo발송자표시.Checked = true;
            else
                this.rdo발송자미표시.Checked = true;

            if (Settings.Default.서명표시 == true)
                this.rdo서명표시.Checked = true;
            else
                this.rdo서명미표시.Checked = true;

            if (Settings.Default.자동발송여부 == true)
                this.rdo자동발송.Checked = true;
            else
                this.rdo수동발송.Checked = true;

            if (Settings.Default.자동발송문구 == true)
                this.rdo자동발송문구표시.Checked = true;
            else
                this.rdo자동발송문구미표시.Checked = true;

            this.txt메일내용.Text = Settings.Default.메일내용;
        }

        private void btn반영_Click(object sender, EventArgs e)
        {
            if (this.rdo납기문의.Checked)
                Settings.Default.메일유형 = this.rdo납기문의.Text;
            else
                Settings.Default.메일유형 = this.rdo납기도래.Text;

            Settings.Default.발송자표시 = this.rdo발송자표시.Checked;
            Settings.Default.서명표시 = this.rdo서명표시.Checked;
            Settings.Default.자동발송여부 = this.rdo자동발송.Checked;
            Settings.Default.자동발송문구 = this.rdo자동발송문구표시.Checked;
            Settings.Default.메일내용 = this.txt메일내용.Text;
            
            Settings.Default.Save();

            this.DialogResult = DialogResult.OK;
        }

        private void btn취소_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

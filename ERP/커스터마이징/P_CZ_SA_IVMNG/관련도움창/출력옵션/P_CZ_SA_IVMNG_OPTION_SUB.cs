using System;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_SA_IVMNG_OPTION_SUB : Duzon.Common.Forms.CommonDialog
    {
        public P_CZ_SA_IVMNG_OPTION_SUB()
        {
            InitializeComponent();

            this.InitEvent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();
        }

        private void InitEvent()
        {
            this.btn반영.Click += new EventHandler(this.btn반영_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.rdo일괄.Checked = false;
            this.rdo건별.Checked = false;

            this.rdo자동.Checked = false;
            this.rdo매출처주소.Checked = false;
            this.rdo계산서주소.Checked = false;

            this.rdo없음.Checked = false;
            this.rdoOriginal.Checked = false;
            this.rdoDuplicate.Checked = false;
            this.rdoPaid.Checked = false;

            this.rdo딘텍.Checked = false;
            this.rdo동진.Checked = false;

            this.rdo표시.Checked = false;
            this.rdo표시.Checked = false;

            if (Settings.Default.품목표시 == this.rdo일괄.Text)
                this.rdo일괄.Checked = true;
            else
                this.rdo건별.Checked = true;

            if (Settings.Default.주소표시 == this.rdo자동.Text)
                this.rdo자동.Checked = true;
            else if (Settings.Default.주소표시 == this.rdo매출처주소.Text)
                this.rdo매출처주소.Checked = true;
            else if (Settings.Default.주소표시 == this.rdo계산서주소.Text)
                this.rdo계산서주소.Checked = true;

            if (Settings.Default.스탬프표시 == this.rdo없음.Text)
                this.rdo없음.Checked = true;
            else if (Settings.Default.스탬프표시 == this.rdoOriginal.Text.ToUpper())
                this.rdoOriginal.Checked = true;
            else if (Settings.Default.스탬프표시 == this.rdoDuplicate.Text.ToUpper())
                this.rdoDuplicate.Checked = true;
            else if (Settings.Default.스탬프표시 == this.rdoPaid.Text.ToUpper())
                this.rdoPaid.Checked = true;

            if (Settings.Default.로고표시 == this.rdo딘텍.Text)
                this.rdo딘텍.Checked = true;
            else if (Settings.Default.로고표시 == this.rdo동진.Text)
                this.rdo동진.Checked = true;

            if (Settings.Default.서명표시 == this.rdo표시.Text)
                this.rdo표시.Checked = true;
            else if (Settings.Default.서명표시 == this.rdo미표시.Text)
                this.rdo미표시.Checked = true;

            if (Settings.Default.직인표시 == this.rdo직인표시.Text)
                this.rdo직인표시.Checked = true;
            else if (Settings.Default.직인표시 == this.rdo직인미표시.Text)
                this.rdo직인미표시.Checked = true;
        }

        private void btn반영_Click(object sender, EventArgs e)
        {
            Settings.Default.내역표시 = this.txt내역표시.Text;

            if (this.rdo일괄.Checked == true)
                Settings.Default.품목표시 = this.rdo일괄.Text;
            else
                Settings.Default.품목표시 = this.rdo건별.Text;

            if (this.rdo자동.Checked == true)
                Settings.Default.주소표시 = this.rdo자동.Text;
            else if (this.rdo매출처주소.Checked == true)
                Settings.Default.주소표시 = this.rdo매출처주소.Text;
            else if (this.rdo계산서주소.Checked == true)
                Settings.Default.주소표시 = this.rdo계산서주소.Text;

            if (this.rdo없음.Checked == true)
                Settings.Default.스탬프표시 = this.rdo없음.Text;
            else if (this.rdoOriginal.Checked == true)
                Settings.Default.스탬프표시 = this.rdoOriginal.Text.ToUpper();
            else if (this.rdoDuplicate.Checked == true)
                Settings.Default.스탬프표시 = this.rdoDuplicate.Text.ToUpper();
            else if (this.rdoPaid.Checked == true)
                Settings.Default.스탬프표시 = this.rdoPaid.Text.ToUpper();

            if (this.rdo딘텍.Checked == true)
                Settings.Default.로고표시 = this.rdo딘텍.Text;
            else if (this.rdo동진.Checked == true)
                Settings.Default.로고표시 = this.rdo동진.Text;

            if (this.rdo표시.Checked == true)
                Settings.Default.서명표시 = this.rdo표시.Text;
            else if (this.rdo미표시.Checked == true)
                Settings.Default.서명표시 = this.rdo미표시.Text;

            if (this.rdo직인표시.Checked == true)
                Settings.Default.직인표시 = this.rdo직인표시.Text;
            else if (this.rdo직인미표시.Checked == true)
                Settings.Default.직인표시 = this.rdo직인미표시.Text;

            Settings.Default.Save();

            this.DialogResult = DialogResult.OK;
        }
    }
}
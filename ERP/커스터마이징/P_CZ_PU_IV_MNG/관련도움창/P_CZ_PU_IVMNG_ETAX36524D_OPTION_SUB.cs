using System;
using System.Windows.Forms;
using Duzon.Common.Forms;

namespace cz
{
    public partial class P_CZ_PU_IVMNG_ETAX36524D_OPTION_SUB : Duzon.Common.Forms.CommonDialog
    {
        public P_CZ_PU_IVMNG_ETAX36524D_OPTION_SUB()
        {
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitEvent();

            if (Settings.Default.Tx_내역표시구분 == "1")
            {
                this.rdo모두.Checked = false;
                this.rdo임의.Checked = true;
            }
            else
            {
                this.rdo모두.Checked = true;
                this.rdo임의.Checked = false;
            }

            this.txt임의.Text = Settings.Default.Tx_내역표시_Text;
            
            if (Settings.Default.Tx_품목표시구분 == "0")
            {
                this.rdo표시안함.Checked = true;
                this.rdo기본코드.Checked = false;
                this.rdo영문명.Checked = false;
            }
            else if (Settings.Default.Tx_품목표시구분 == "1")
            {
                this.rdo표시안함.Checked = false;
                this.rdo기본코드.Checked = true;
                this.rdo영문명.Checked = false;
            }
            else
            {
                if (!(Settings.Default.Tx_품목표시구분 == "2"))
                    return;
                this.rdo표시안함.Checked = false;
                this.rdo기본코드.Checked = false;
                this.rdo영문명.Checked = true;
            }
        }

        private void InitEvent()
        {
            this.btn반영.Click += new EventHandler(this.btn반영_Click);
            this.btn취소.Click += new EventHandler(this.btn취소_Click);
        }

        private void btn반영_Click(object sender, EventArgs e)
        {
            string str1 = string.Empty;
            string str2 = (!this.rdo임의.Checked ? "0" : "1");
            string str3 = string.Empty;

            if (this.rdo표시안함.Checked)
                str3 = "0";
            else if (this.rdo기본코드.Checked)
                str3 = "1";
            else if (this.rdo영문명.Checked)
                str3 = "2";

            Settings.Default.Tx_내역표시구분 = str2;
            Settings.Default.Tx_내역표시_Text = this.txt임의.Text;
            Settings.Default.Tx_품목표시구분 = str3;
            Settings.Default.Save();
            
            this.DialogResult = DialogResult.OK;
        }

        private void btn취소_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Net;
using System.Windows.Forms;
using Duzon.BizOn.Erpu.BusinessConfig;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_MA_PARTNER_ACCOUNT : Duzon.Common.Forms.CommonDialog
    {
        private string cdPartner = string.Empty;
        private string inPartner = string.Empty;
        private string noCompany = string.Empty;
        private string cdBank = string.Empty;
        private string noDeposit = string.Empty;
        private string nmDeposit = string.Empty;

        public P_CZ_MA_PARTNER_ACCOUNT()
        {
            InitializeComponent();
        }

        public P_CZ_MA_PARTNER_ACCOUNT(string cd_partner, string ln_partner, string no_company, string cd_bank, string no_deposit, string nm_deposit)
        {
            this.InitializeComponent();
            this.cdPartner = cd_partner;
            this.inPartner = ln_partner;
            this.noCompany = no_company;
            this.cdBank = cd_bank;
            this.noDeposit = no_deposit;
            this.nmDeposit = nm_deposit;
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitEvent();
            this.InitControl();
        }

        private void InitEvent()
        {
            this.btn확인.Click += new EventHandler(this.btn_Click);
        }

        private void InitControl()
        {
            new SetControl().SetCombobox(this.cbo은행, MA.GetCode("MA_B000043", true));
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            this.Search();
        }

        private void Search()
        {
            try
            {
                string str = string.Empty;
                string address = "http://" + BusinessInfo.SysInfo.IpCms + "/xbank-server/hanacms/GWbicnet.jsp?rcvData=HANA002/" + this.cdBank + "/" + this.noDeposit;
                WebClient webClient = new WebClient();
                this.txt조회내역.Text = webClient.DownloadString(address);
                this.txt조회내역.Text = this.txt조회내역.Text.Replace("\r\n", "");
                webClient.Dispose();
                this.txt가져올회사코드.Text = this.cdPartner;
                this.txt거래처명.Text = this.inPartner;
                this.meb사업자등록번호.Text = this.noCompany;
                this.cbo은행.SelectedValue = (object)this.cdBank;
                this.txt계좌번호.Text = this.noDeposit;
                this.txt예금주.Text = this.nmDeposit;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}

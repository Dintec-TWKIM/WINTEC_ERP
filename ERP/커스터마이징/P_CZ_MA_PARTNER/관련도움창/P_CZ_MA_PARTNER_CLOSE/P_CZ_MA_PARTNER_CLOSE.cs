using System;
using System.Net;
using System.Windows.Forms;
using Duzon.BizOn.Erpu.BusinessConfig;
using Duzon.Common.Forms;

namespace cz
{
    public partial class P_CZ_MA_PARTNER_CLOSE : Duzon.Common.Forms.CommonDialog
    {
        private string cdPartner = string.Empty;
        private string inPartner = string.Empty;
        private string noCompany = string.Empty;

        public P_CZ_MA_PARTNER_CLOSE()
        {
            InitializeComponent();
        }

        public P_CZ_MA_PARTNER_CLOSE(string cdPartner, string lnPartner, string noCompany)
        {
            this.InitializeComponent();

            this.cdPartner = cdPartner;
            this.inPartner = lnPartner;
            this.noCompany = noCompany;
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitEvent();
        }

        private void InitEvent()
        {
            this.btn확인.Click += new EventHandler(this.btn_Click);
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
                string address = "http://" + BusinessInfo.SysInfo.IpCms + "/xbank-server/hanacms/GWbicnet.jsp?rcvData=HANA001/" + this.noCompany;
                WebClient webClient = new WebClient();
                this.txt조회내역.Text = webClient.DownloadString(address);
                this.txt조회내역.Text = this.txt조회내역.Text.Replace("\r\n", "");
                webClient.Dispose();
                this.txt가져올회사코드.Text = this.cdPartner;
                this.txt거래처명.Text = this.inPartner;
                this.meb사업자등록번호.Text = this.noCompany;
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

using System;
using System.Data;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Duzon.Common.Forms;

namespace Dintec.AutoWeb
{    
    public class Glovis
    {
        private AutoWebBroswer _awb = new AutoWebBroswer();
		private bool _downloadComplete = false;

		public string RefNumber
        {
            get; set;
        }

        public string WebLink
        {
            get; set;
        }

		public string ErrorMessage
		{
			get; set;
		}

		private void Login()
        {
			if (_awb.WebBrowser.IsExists("ctl00_LeftMenu1_txtID"))
			{
				_awb.WebBrowser.SetValue("ctl00_LeftMenu1_txtID", "db@dintec.co.kr");
				_awb.WebBrowser.SetValue("ctl00_LeftMenu1_txtPass", "djsm5771");
				_awb.WebBrowser.Click("ctl00_LeftMenu1_btnLogin");
			}
        }

        public string DownloadInquiry()
        {
            //_awb.Show();

            // 임시폴더 생성
            string inqPath = Application.StartupPath + @"\temp\";
            string inqFile = "";

			// 사이트 이동 및 로그인
			_awb.WebBrowser.GoTo("https://supplier.gmarineservice.com");
			Login();

			// 다운로드 준비
			inqFile = inqPath + FileMgr.GetUniqueFileName(inqPath + RefNumber + ".pdf");

			// 견적서 다운로드
			string rfqid = RefNumber.Substring(0, 4) + "00" + RefNumber.Substring(4);
			WebClient client = new WebClient();
			client.DownloadFileCompleted += Client_DownloadFileCompleted;
			client.DownloadFileAsync(new Uri("https://supplier.gmarineservice.com/quote/vp_a_purchase_quote_supplier_rfq_report.aspx?rfqid=F" + rfqid), inqFile);

			while (!_downloadComplete)
			{
				Application.DoEvents();
				Thread.Sleep(100);
			}

			return inqFile;
        }

		private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			_downloadComplete = true;
		}

		public void Submit(DataTable dtHead, DataTable dtLine)
        {
            _awb.Show();

            // 사이트 이동 및 로그인
            _awb.WebBrowser.GoTo("https://supplier.gmarineservice.com/");
            Login();

            // RFQ 열기 => 여기 가야지만 quoid를 알 수 있음
            string rfqid = RefNumber.Substring(0, 4) + "00" + RefNumber.Substring(4);
            _awb.WebBrowser.GoTo("https://supplier.gmarineservice.com/quote/vp_a_purchase_quote_supplier_rfq_detail.aspx?rfqid=F" + rfqid);

            // 견적창 열기
            string quoid = _awb.WebBrowser.GetValue("hidQuoId");
            _awb.WebBrowser.GoTo("https://supplier.gmarineservice.com/quote/vp_a_purchase_quote_supplier_quote_detail.aspx?quoid=" + quoid);

            // ********** 헤더 입력
            _awb.WebBrowser.SetValue("txtRefNo", (string)dtHead.Rows[0]["NO_FILE"]);

            // ********** 라인 입력
            string errorMsg = "";

            foreach (DataRow row in dtLine.Rows)
            {
                // NO_DSP
                string number = string.Format("{0:00}", row["NO_DSP"]);

                // 수량 비교
                decimal qty = GetTo.Decimal(_awb.WebBrowser.GetValue("grdQuo_ctl" + number + "_txtQuoteQty"));

                if (qty != (decimal)row["QT_QTN"])
                    errorMsg += ", " + number;

                // 수량, 납기 입력
                _awb.WebBrowser.SetValue("grdQuo_ctl" + number + "_txtQuotePrice", string.Format("{0:#,##0.00}", row["UM_KR_S"]));
                _awb.WebBrowser.SetValue("grdQuo_ctl" + number + "_txtDaysDelive", row["LT"].ToString());
            }

            _awb.WebBrowser.RunScript("fnChangeQuoteInfo");

            if (errorMsg != "")
                Global.MainFrame.ShowMessage("수량오류 : " + errorMsg.Substring(2));            
        }
    }
}

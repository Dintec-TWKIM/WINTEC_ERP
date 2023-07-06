using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Dintec;
using Duzon.Common.Forms;

namespace cz
{
    public partial class P_CZ_MA_FAX_SUB : Duzon.Common.Forms.CommonDialog
    {
        P_CZ_MA_FAX_SUB_BIZ _biz;
        private string FTP서버경로;
        private string 파일번호;
        private string 받는사람명;
        private string 임시폴더;

        public P_CZ_MA_FAX_SUB()
        {
            this.InitializeComponent();
        }

        public P_CZ_MA_FAX_SUB(string 받는사람, string 받는사람명, string 첨부파일, string 파일번호)
        {
            this.InitializeComponent();

            this.txt받는사람.Text = 받는사람;
            this.txt첨부파일.Text = 첨부파일;
            this.파일번호 = 파일번호;
            this.받는사람명 = 받는사람명;
        } 

        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_MA_FAX_SUB_BIZ();

            this.FTP서버경로 = "ftp://192.168.1.149/";
            this.임시폴더 = "temp";

            this.첨부파일설정();
            this.InitEvent();
        }

        private void 첨부파일설정()
        {
            try
            {
                this.txt첨부파일.Text = FileMgr.Download_WF(Global.MainFrame.LoginInfo.CompanyCode, this.파일번호, this.txt첨부파일.Text, false);
                this.web첨부파일뷰어.Navigate(Path.Combine(Application.StartupPath, this.임시폴더) + "/" + this.txt첨부파일.Text);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                this.임시파일제거();
                base.OnClosed(e);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 임시파일제거()
        {
            DirectoryInfo dirInfo;
            bool isExistFile;

            try
            {
                dirInfo = new DirectoryInfo(Path.Combine(Application.StartupPath, this.임시폴더));
                isExistFile = false;

                if (dirInfo.Exists == true)
                {
                    foreach (FileInfo file in dirInfo.GetFiles("*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch
                        {
                            isExistFile = true;
                            continue;
                        }
                    }

                    if (isExistFile == false)
                        dirInfo.Delete(true);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void InitEvent()
        {
            this.btn보내기.Click += new EventHandler(btn보내기_Click);
        }

        private void btn보내기_Click(object sender, EventArgs e)
        {
            WebClient wc;
            string 날짜, 로컬경로, 서버경로;

            try
            {
                if (Global.MainFrame.ShowMessage("CZ_진행하시겠습니까 ?", "QY2") == DialogResult.No)
                    return;

                날짜 = DateTime.Now.ToString("yyyyMMddHHmmss");
                로컬경로 = Path.Combine(Application.StartupPath, this.임시폴더) + "/" + this.txt첨부파일.Text;
                서버경로 = this.FTP서버경로 + "FaxData/Send/" + 날짜 + "_" + this.txt첨부파일.Text;

                wc = new WebClient();
                wc.Credentials = new NetworkCredential("cti", "cti_2015");
                wc.UploadFile(서버경로, 로컬경로);

                this._biz.팩스보내기(this.txt받는사람.Text, 받는사람명, this.txt첨부파일.Text, 날짜, this.파일번호);

                Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn보내기.Text);
                this.Close();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

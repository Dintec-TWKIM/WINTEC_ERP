using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System.IO;
using System.Net.Mail;
using System.Net;
using Dintec;
using Duzon.Common.Forms.Help;
using Duzon.Common.Controls;
using Aspose.Email.Outlook;

namespace cz
{
    public partial class P_CZ_MA_EMAIL_SUB : Duzon.Common.Forms.CommonDialog
    {
        private string 임시폴더, 파일번호, 거래처코드, 기본받는사람, 기본참조, 기본숨은참조, html;
        private string[] 기본파일, 추가파일;
        private bool 자동보내기여부;
        private DataTable 첨부파일;
        private string _회사코드;

		public string To
		{
			get
			{
				return txt받는사람.Text;
			}
		}

        public string CompanyCode
        {
            get
            {
                if (string.IsNullOrEmpty(this._회사코드))
                    return Global.MainFrame.LoginInfo.CompanyCode;
                else
                    return this._회사코드;
            }
            set
            {
                this._회사코드 = value;
            }
        }

        public P_CZ_MA_EMAIL_SUB(string 보내는사람, string 받는사람, string 참조, string 숨은참조, string 제목, string[] 기본파일, string[] 추가파일, string 본문, string 파일번호, string 거래처코드, bool 자동보내기여부)
        {
            InitializeComponent();

            this.txt보내는사람.Text = 보내는사람;
            this.기본받는사람 = 받는사람;
            this.기본참조 = 참조;
            this.기본숨은참조 = 숨은참조;
            this.txt제목.Text = 제목;
            this.기본파일 = 기본파일;
            this.추가파일 = 추가파일;
            this.txt본문.Text = 본문;
            this.html = string.Empty;

            this.tabControl1.TabPages.RemoveAt(0);

            this.파일번호 = 파일번호;
            this.거래처코드 = 거래처코드;
            this.자동보내기여부 = 자동보내기여부;
        }

        public P_CZ_MA_EMAIL_SUB(string 보내는사람, string 받는사람, string 참조, string 숨은참조, string 제목, string[] 기본파일, string[] 추가파일, string 본문, string html, string 파일번호, string 거래처코드, bool 자동보내기여부)
        {
            InitializeComponent();

            this.txt보내는사람.Text = 보내는사람;
            this.기본받는사람 = 받는사람;
            this.기본참조 = 참조;
            this.기본숨은참조 = 숨은참조;
            this.txt제목.Text = 제목;
            this.기본파일 = 기본파일;
            this.추가파일 = 추가파일;
            this.txt본문.Text = 본문;
            this.html = html;

            this.webHtml.DocumentText = html;

            this.파일번호 = 파일번호;
            this.거래처코드 = 거래처코드;
            this.자동보내기여부 = 자동보내기여부;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            // 라이선스 인증
            Aspose.Email.License license = new Aspose.Email.License();
            license.SetLicense("Aspose.Email.lic");

            this.InitDataTable();
            this.InitEvent();
        }

        private void InitDataTable()
        {
            this.첨부파일 = new DataTable();

            this.첨부파일.Columns.Add("NAME_FILE");
            this.첨부파일.Columns.Add("PATH_FILE");
        }

        private void InitEvent()
        {
            this.DragOver += new DragEventHandler(this.Control_DragOver);
            this.DragDrop += new DragEventHandler(this.Control_DragDrop);

            this.chk미리보기.CheckedChanged += new EventHandler(this.chk미리보기_CheckedChanged);

            this.btn보내기.Click += new EventHandler(this.btn보내기_Click);
            this.btn첨부추가.Click += new EventHandler(this.btn첨부추가_Click);

            this.lbl받는사람.Click += new EventHandler(this.btn메일주소_Click);
            this.lbl참조.Click += new EventHandler(this.btn메일주소_Click);
            this.lbl숨은참조.Click += new EventHandler(this.btn메일주소_Click);
            this.lbl첨부파일.Click += new EventHandler(this.btn첨부파일_Click);

            this.txt받는사람.KeyDown += new KeyEventHandler(this.Control_KeyDown);
            this.txt참조.KeyDown += new KeyEventHandler(this.Control_KeyDown);
            this.txt숨은참조.KeyDown += new KeyEventHandler(this.Control_KeyDown);
            this.txt첨부파일.KeyDown += new KeyEventHandler(this.Control_KeyDown);
            this.txt첨부파일.MouseClick += new MouseEventHandler(this.txt첨부파일_MouseClick);
        }

        protected override void InitPaint()
        {
            bool 기본설정여부;

            try
            {
                base.InitPaint();

                this.임시폴더 = "temp";
                기본설정여부 = this.기본설정();

                if (this.자동보내기여부 && 기본설정여부)
                    this.btn보내기_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Control_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Control_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths, fileNames;

            try
            {
                paths = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                foreach (string path in paths)
                {
                    if (Directory.Exists(path) == true)
                    {
                        fileNames = Directory.GetFiles(path, "*.*");

                        foreach (string fileName in fileNames)
                        {
                            this.첨부파일추가(fileName);
                        }
                    }
                    else if (File.Exists(path) == true)
                    {
                        this.첨부파일추가(path);
                    }
                }
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

        private void chk미리보기_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chk미리보기.Checked == false) return;

                if (this.기본파일[0].IndexOf('|') > -1)
                {
                    string[] temp = this.기본파일[0].Split('|');

                    this.web미리보기.Navigate(temp[0]);
                }
                else
				{
                    this.web미리보기.Navigate(Path.Combine(Application.StartupPath, this.임시폴더) + "\\" + this.기본파일[0]);
                }    
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn보내기_Click(object sender, EventArgs e)
        {
            MailMessage mailMessage;
            SmtpClient smtpClient;
            DBMgr dbMgr;
            DataTable dt;
            string address, name, id, pw, domain, query;
            string[] tempText;

            try
            {
                if (!this.자동보내기여부 && Global.MainFrame.ShowMessage("CZ_진행하시겠습니까 ?", "QY2") == DialogResult.No)
                    return;
                
                if (string.IsNullOrEmpty(this.txt받는사람.Text))
                {
                    if (this.자동보내기여부)
                    {
                        this.DialogResult = DialogResult.Cancel;
                        return;
                    }
                    else
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, Global.MainFrame.DD("받는사람"));
                        return;
                    }
                }

                #region 기본설정
                mailMessage = new MailMessage();
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                #endregion

                #region 보내는사람
                tempText = this.txt보내는사람.Text.Split('/');

                if (tempText.Length == 1)
                {
                    address = tempText[0];
                    name = tempText[0];
                }
                else if (tempText.Length == 2)
                {
                    address = tempText[0];
                    name = tempText[1];
                }
                else
                    return;

                tempText = address.Split('@');

                if (tempText.Length != 2) return;

                id = tempText[0];
                domain = tempText[1];

                query = @"SELECT DM.DM_NAME,
                                 DU.DU_USERID,
                                 DU.DU_PWD
                          FROM MCDOMAINUSER DU WITH(NOLOCK)
                          LEFT JOIN MCDOMAIN DM WITH(NOLOCK) ON DM.DM_ID = DU.DM_ID
                          WHERE DM.DM_NAME = '" + domain + "'" + Environment.NewLine +
                         "AND DU.DU_USERID = '" + id + "'";

                dbMgr = new DBMgr(DBConn.Mail);
                dbMgr.Query = query;
                dt = dbMgr.GetDataTable();
                pw = D.GetString(dt.Rows[0]["DU_PWD"]);
                #endregion

                #region 메일정보
                mailMessage.From = new MailAddress(address, name, Encoding.UTF8);

                foreach (string 받는사람 in this.txt받는사람.Text.Split(';'))
                {
                    if (받는사람.Trim() != "")
                        mailMessage.To.Add(new MailAddress(받는사람));
                }

                foreach (string 참조 in this.txt참조.Text.Split(';'))
                {
                    if (참조.Trim() != "")
                        mailMessage.CC.Add(new MailAddress(참조));
                }

                foreach (string 숨은참조 in this.txt숨은참조.Text.Split(';'))
                {
                    if (숨은참조.Trim() != "")
                        mailMessage.Bcc.Add(new MailAddress(숨은참조));
                }

                mailMessage.Subject = this.txt제목.Text;

				// 본문 html로 변환할 시 <a>태그 앞뒤로 하고 <a>태그 내부는 건드리지 않음
				string body = "";
				string bodyA = "";
				string bodyB = "";
				string bodyC = "";

				int index = txt본문.Text.IndexOf("<a href=");

				if (index > 0)
				{
					bodyA = txt본문.Text.Substring(0, index);
					bodyB = txt본문.Text.Substring(index, txt본문.Text.IndexOf("</a>") + 4 - index);
					bodyC =  txt본문.Text.Substring(txt본문.Text.IndexOf("</a>") + 4);

					body = ""
						+ bodyA.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />")
						+ bodyB
						+ bodyC.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />");
				}
				else
				{
					body = txt본문.Text.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />"); ;
				}

				mailMessage.Body = "<div style='font-family:맑은 고딕; font-size:9pt'>" + body + "</div>" + html;

                foreach (DataRow dr in this.첨부파일.Rows)
                {
                    mailMessage.Attachments.Add(new Attachment(D.GetString(dr["PATH_FILE"])));
                }
                #endregion

                #region 메일보내기
                smtpClient = new SmtpClient("113.130.254.131", 587);
                smtpClient.EnableSsl = false;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(address, pw);
                smtpClient.Send(mailMessage);
                #endregion

                if (!this.자동보내기여부)
                    Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn보내기.Text);
                
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn첨부추가_Click(object sender, EventArgs e)
        {
            string 파일경로, 파일이름;

            try
            {
                if (this.추가파일 != null && this.추가파일.Length > 0)
                {
                    for (int i = 0; i < this.추가파일.Length; i++)
                    {
                        if (this.추가파일[i].IndexOf('\\') > -1)
                            파일이름 = FileMgr.Download(this.추가파일[i], false);
                        else
                            파일이름 = FileMgr.Download_WF(this.CompanyCode, this.파일번호, this.추가파일[i], false);

                        파일경로 = Path.Combine(Application.StartupPath, this.임시폴더) + "\\" + 파일이름;
                        this.첨부파일추가(파일경로);
                    }
                }

                this.btn첨부추가.Enabled = false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn메일주소_Click(object sender, EventArgs e)
        {
            P_CZ_MA_RECIPIENT_SUB sub;
            string name;
            
            try
            {
                name = ((Control)sender).Name;
                sub = new P_CZ_MA_RECIPIENT_SUB(this.거래처코드);

                if (sub.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (DataRow dr in sub.Result)
                    {
                        if (name == this.lbl받는사람.Name)
                            this.수신자추가(this.txt받는사람, D.GetString(dr["NO_EMAIL"]));
                        else if (name == this.lbl참조.Name)
                            this.수신자추가(this.txt참조, D.GetString(dr["NO_EMAIL"]));
                        else if (name == this.lbl숨은참조.Name)
                            this.수신자추가(this.txt숨은참조, D.GetString(dr["NO_EMAIL"]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn첨부파일_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;

                string[] fileNames = openFileDialog.FileNames;

                foreach (string fileName in fileNames)
                {
                    this.첨부파일추가(fileName);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            DataRow[] dataRowArray;
            TextBox control;
            string 문자열, query;
            
            try
            {
                control = ((TextBox)sender);

                if (e.KeyData == Keys.Back)
                {
                    if (control.Name == this.txt받는사람.Name || control.Name == this.txt참조.Name)
                    {
                        if (control.SelectionStart != control.Text.Length)
                            this.선택문자열(control, true);
                    }
                    else
                    {
                        문자열 = this.선택문자열(control, true);

                        if (!string.IsNullOrEmpty(문자열))
                        {
                            query = ("NAME_FILE = '" + 문자열 + "'");
                            dataRowArray = this.첨부파일.Select(query);

                            if (dataRowArray.Length != 0)
                            {
                                dataRowArray[0].Delete();

                                control.Text = string.Empty;
                                foreach (DataRow row in this.첨부파일.Rows)
                                {
                                    문자열 = D.GetString(row["NAME_FILE"]);
                                    control.Text += (string.IsNullOrEmpty(control.Text) ? 문자열 : ";" + 문자열);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void txt첨부파일_MouseClick(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (this.chk미리보기.Checked == false) return;

                dataRowArray = this.첨부파일.Select("NAME_FILE = '" + this.선택문자열(this.txt첨부파일, false) + "'");

                if (dataRowArray.Length > 0)
                    this.web미리보기.Navigate(D.GetString(dataRowArray[0]["PATH_FILE"]));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool 기본설정()
        {
            string[] temp;
            string 파일경로;

            try
            {
                this.수신자추가(this.txt받는사람, this.기본받는사람);
                this.수신자추가(this.txt참조, this.기본참조);
                this.수신자추가(this.txt숨은참조, this.기본숨은참조);

                if (this.기본파일 != null && this.기본파일.Length > 0)
                {
                    for (int i = 0; i < this.기본파일.Length; i++)
                    {
                        if (this.기본파일[i].IndexOf('|') > -1)
                        {
                            temp = this.기본파일[i].Split('|');

                            if (temp[1].StartsWith("BINARY"))
                            {
                                byte[] fileData = Convert.FromBase64String(this.기본파일[i].Split(new string[] { "|BINARY" }, StringSplitOptions.None)[1]);
                                this.기본파일[i] = FILE.DownloadBinary(temp[0], fileData, false);
                            }
                            else if (temp[1] == "LOCAL")
							{
                                this.첨부파일추가(this.기본파일[i].Split(new string[] { "|LOCAL" }, StringSplitOptions.None)[0]);
                                continue;
							}
                            else if (this.기본파일[i].IndexOf('\\') > -1)
                                this.기본파일[i] = FileMgr.Download(temp[1], temp[0], false);
                            else
                                this.기본파일[i] = FileMgr.Download_WF(this.CompanyCode, this.파일번호, temp[1], temp[0], false);
                        }
                        else
                        {
                            if (this.기본파일[i].IndexOf('\\') > -1)
                                this.기본파일[i] = FileMgr.Download(this.기본파일[i], false);
                            else
                                this.기본파일[i] = FileMgr.Download_WF(this.CompanyCode, this.파일번호, this.기본파일[i], false);
                        }
                        
                        파일경로 = Path.Combine(Application.StartupPath, this.임시폴더) + "\\" + this.기본파일[i];
                        this.첨부파일추가(파일경로);
                    }

                    //this.web미리보기.Navigate(Path.Combine(Application.StartupPath, this.임시폴더) + "\\" + this.기본파일[0]);
                }

                return true;
            }
            catch (Exception ex)
            {
                if (this.자동보내기여부)
                    this.DialogResult = DialogResult.Cancel;
                else
                    Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }

        private void 수신자추가(TextBoxExt textBox, string 메일)
        {
            try
            {
                if (string.IsNullOrEmpty(메일)) return;

                textBox.Text += (string.IsNullOrEmpty(textBox.Text) ? 메일 : ";" + 메일);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 파일추가(string fileName)
        {
            FileInfo fileInfo;
            DataRow row;

            try
            {
                fileInfo = new FileInfo(fileName);

                row = this.첨부파일.NewRow();

                row["NAME_FILE"] = fileInfo.Name;
                row["PATH_FILE"] = fileInfo.FullName;

                this.첨부파일.Rows.Add(row);

                this.txt첨부파일.Text += (string.IsNullOrEmpty(this.txt첨부파일.Text) ? row["NAME_FILE"].ToString() : ";" + row["NAME_FILE"].ToString());
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private string 선택문자열(TextBox textBox, bool isSelectText)
        {
            int selectIndex, startIndex, endIndex, length;
            string 문자열 = string.Empty;

            try
            {
                if (textBox.SelectionStart <= 0) return 문자열;
                
                selectIndex = textBox.SelectionStart - 1;
                startIndex = 0;
                length = 0;

                startIndex = textBox.Text.IndexOf(';', selectIndex);
                endIndex = textBox.Text.LastIndexOf(';', selectIndex);

                if (startIndex <= 0 && endIndex <= 0)
                {
                    startIndex = 0;
                    length = textBox.Text.Length;
                }
                else if (startIndex <= 0 && endIndex > 0)
                {
                    startIndex = (endIndex + 1);
                    length = textBox.Text.Length - startIndex;
                }
                else if (startIndex > 0 && endIndex <= 0)
                {
                    length = startIndex;
                    startIndex = 0;
                }
                else
                {
                    length = startIndex - (endIndex + 1);
                    startIndex = (endIndex + 1);
                }

                if (startIndex < 0 || length < 0) return 문자열;

                if (isSelectText == true)
                    textBox.Select(startIndex, length);

                문자열 = textBox.Text.Substring(startIndex, length).Replace(";", "");

                System.Diagnostics.Debug.WriteLine("SelectIndex : " + selectIndex.ToString());
                System.Diagnostics.Debug.WriteLine("StartIndex : " + startIndex.ToString());
                System.Diagnostics.Debug.WriteLine("Length : " + length.ToString());
                System.Diagnostics.Debug.WriteLine("Text : " + 문자열);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return 문자열;
        }

        private void 첨부파일추가(string filePath)
        {
            FileInfo fileInfo;
            List<string> imageList;
            string[] separator, temp;
            string localpath, fileName, image1;

            try
            {
                fileInfo = new FileInfo(filePath);

                if (fileInfo.Extension.ToUpper() == ".MSG")
                {
                    //MapiMessage msg = MapiMessage.FromFile(filePath);

                    //#region 본문 이미지 제거
                    //separator = new string[] { "cid:" };
                    //temp = msg.BodyHtml.Split(separator, StringSplitOptions.None);

                    //temp[0] = string.Empty;
                    //imageList = new List<string>();

                    //foreach (string image in temp)
                    //{
                    //    if (string.IsNullOrEmpty(image))
                    //        continue;

                    //    image1 = image.Split('.')[0];

                    //    if (!imageList.Contains(image1))
                    //        imageList.Add(image1);
                    //}
                    //#endregion

                    //foreach (MapiAttachment item in msg.Attachments)
                    //{
                    //    if (imageList.Contains(item.LongFileName.Split('.')[0])) continue;

                    //    fileName = FileMgr.GetUniqueFileName(Path.Combine(Application.StartupPath, this.임시폴더) + "\\" + item.FileName);
                    //    localpath = Path.Combine(Application.StartupPath, this.임시폴더) + "\\" + fileName;
                    //    item.Save(localpath);
                    //    this.파일추가(localpath);
                    //}
                }
                else
                {
                    this.파일추가(filePath);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

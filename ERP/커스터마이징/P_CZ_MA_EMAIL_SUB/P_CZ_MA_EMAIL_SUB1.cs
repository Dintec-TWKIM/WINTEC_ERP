using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.BizOn.Erpu.Net.Mail;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.Windows.Print;
using Duzon.ERPU.MF.EMail;
using System.Drawing;
using Duzon.BizOn.Erpu.BusinessConfig;
using Duzon.BizOn.Erpu.Resource;

namespace cz
{
    public partial class P_CZ_MA_EMAIL_SUB1 : Duzon.Common.Forms.CommonDialog
    {
        private string _setSendUser = string.Empty;
        private string _setRefTab = string.Empty;
        private string _setPartTabVisible = string.Empty;
        private DataTable _dt_temp1 = (DataTable)null;
        private DataTable _dt_temp2 = (DataTable)null;
        private ReportHelper[] _rptHelper;
        private string[] _cd_partner;
        private string _id_object;
        private string _rptname;
        private Dictionary<string, string> _dic;
        private string _filepath;
        private string _str_title;
        private string _send_email_add;
        private string _str_ntext;
        private string _sendref_email_add;
        private string[] _str_histext_temp;
        public string[] _str_rt_data;

        public string SetSendUser
        {
            set
            {
                this._setSendUser = value;
            }
        }

        public string SetRefTab
        {
            set
            {
                this._setRefTab = value;
            }
        }

        public string SetPartTabVisible
        {
            set
            {
                this._setPartTabVisible = value;
            }
        }

        public DataTable dt_temp1
        {
            set
            {
                this._dt_temp1 = value;
            }
        }

        public DataTable dt_temp2
        {
            set
            {
                this._dt_temp2 = value;
            }
        }

        public P_CZ_MA_EMAIL_SUB1(string[] cd_partner, string id_object, ReportHelper[] rptHelper, Dictionary<string, string> dic)
        {
            this.InitializeComponent();
            this._cd_partner = cd_partner;
            this._id_object = id_object;
            this._rptHelper = rptHelper;
            this._rptname = string.Empty;
            if (dic == null)
                this._dic = new Dictionary<string, string>();
            else
                this._dic = dic;
        }

        public P_CZ_MA_EMAIL_SUB1(string[] cd_partner, string id_object, ReportHelper[] rptHelper, Dictionary<string, string> dic, string rptname)
        {
            this.InitializeComponent();
            this._cd_partner = cd_partner;
            this._id_object = id_object;
            this._rptHelper = rptHelper;
            this._rptname = rptname;
            this._dic = dic != null ? dic : new Dictionary<string, string>();
            if (Global.MainFrame.CurrentPageID.CompareTo("P_SU_PO_REG") != 0)
                return;
        }

        public P_CZ_MA_EMAIL_SUB1(string[] cd_partner, string id_object, ReportHelper[] rptHelper, Dictionary<string, string> dic, string rptname, string[] str_histext)
        {
            this.InitializeComponent();
            this._cd_partner = cd_partner;
            this._id_object = id_object;
            this._rptHelper = rptHelper;
            this._rptname = rptname;
            this._dic = dic != null ? dic : new Dictionary<string, string>();
            this._str_title = str_histext[0];
            this._send_email_add = str_histext[1];
            this._str_ntext = str_histext[2];
            this._str_histext_temp = str_histext;
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            if (this._setRefTab == "Y")
                this._sendref_email_add = this._str_histext_temp[3];
            else
                this.tb_mail_add.TabPages.Remove(this.tb_ref);
            if (this._setPartTabVisible == "N")
                this.tb_mail_add.TabPages.Remove(this.tb_partner);
            this.InitGrid();
            this.InitImage();
            this.InitEvent();
            if (BASIC.GetMAEXC("물류E-MAIL설정") == "100")
                this.rdo_outlook.Checked = true;
            else
                this.rdo_smtp.Checked = true;
        }

        private void InitImage()
        {
            this.pictureBox1.Image = (Image)HelpMultiLangResource.POP_TITLE_SENDMAIL;
            this.m_pnlMain.BackgroundImage = (Image)HelpFormResource.POP_BG_MAILFORM;
        }

        private void InitGrid()
        {
            this.flexPart.BeginSetting(1, 1, false);

            this.flexPart.SetCol("S", "S", 35, true, CheckTypeEnum.Y_N);
            this.flexPart.SetCol("NM_PTR", "담당자", 100, true, typeof(string));
            this.flexPart.SetCol("NM_EMAIL", "메일주소", 200, true, typeof(string));
            this.flexPart.SetCol("CD_PARTNER", "거래처코드", false);
            this.flexPart.SetCol("FILE_PATH", "파일경로", false);
            
            this.flexPart.ExtendLastCol = true;
            this.flexPart.SettingVersion = "0.3";
            this.flexPart.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this.flexPart.AfterCodeHelp += new AfterCodeHelpEventHandler(this.Flex_AfterCodeHelp);
            
            this.flexEmp.BeginSetting(1, 1, false);

            this.flexEmp.SetCol("S", "S", 35, true, CheckTypeEnum.Y_N);
            this.flexEmp.SetCol("NM_PTR", "담당자", 100, true, typeof(string));
            this.flexEmp.SetCol("NM_EMAIL", "메일주소", 200, true, typeof(string));
            this.flexEmp.SetCol("FILE_PATH", "파일경로", false);
            
            this.flexEmp.ExtendLastCol = true;
            this.flexEmp.SettingVersion = "0.3";
            this.flexEmp.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this.flexEmp.SetCodeHelpCol("NM_PTR", HelpID.P_MA_EMP_SUB1, ShowHelpEnum.Always, new string[0], new string[0], ResultMode.SlowMode);
            this.flexEmp.AfterCodeHelp += new AfterCodeHelpEventHandler(this.Flex_AfterCodeHelp);
            
            if (this._setRefTab == "Y")
            {
                this.flexRef.BeginSetting(1, 1, false);
                
                this.flexRef.SetCol("S", "S", 35, true, CheckTypeEnum.Y_N);
                this.flexRef.SetCol("NM_PTR", "담당자", 100, true, typeof(string));
                this.flexRef.SetCol("NM_EMAIL", "메일주소", 200, true, typeof(string));
                this.flexRef.SetCol("FILE_PATH", "파일경로", false);
                
                this.flexRef.ExtendLastCol = true;
                this.flexRef.SettingVersion = "0.3";
                this.flexRef.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
                this.flexRef.SetCodeHelpCol("NM_PTR", HelpID.P_MA_EMP_SUB1, ShowHelpEnum.Always, new string[0], new string[0], ResultMode.SlowMode);
                this.flexRef.AfterCodeHelp += new AfterCodeHelpEventHandler(this.Flex_AfterCodeHelp);
            }

            this.SetEmpGrid();
        }

        private void InitEvent()
        {
            this.btnAdd.Click += new EventHandler(this.BtnAdd_Click);
            this.btnDel.Click += new EventHandler(this.BtnDel_Click);
            this.btnFileUpload.Click += new EventHandler(this.BtnFileUpload_Click);
            this.btnSendMail.Click += new EventHandler(this.BtnSendMail_Click);
            this.FormClosing += new FormClosingEventHandler(this.P_MF_EMAIL_FormClosing);
            this.tb_mail_add.SelectedIndexChanged += new EventHandler(this.tb_mail_add_SelectedIndexChanged);
        }

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();
                DataTable dataTable = DBHelper.GetDataTable("UP_CM_REPORT_OBJECT_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                         this._id_object });
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    DataRow row = dataTable.NewRow();
                    row["ID_OBJECT"] = (this._id_object + "01");
                    row["NM_OBJECT"] = this._rptname;
                    dataTable.Rows.Add(row);
                }

                this.cbo_Print.DataSource = dataTable;
                this.cbo_Print.DisplayMember = "NM_OBJECT";
                this.cbo_Print.ValueMember = "ID_OBJECT";
                
                if (!(Global.MainFrame.ServerKeyCommon.ToUpper() == "SEMI"))
                    return;
                
                this.tb_mail_add.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void BtnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._txt보내는사람.Text))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this._lbl보내는사람.Text);
                }
                else
                {
                    //this.MakePdf();
                    string path1 = this.GetPath();
                    FlexGrid flexGrid1 = new FlexGrid();
                    FlexGrid flexGrid2 = !(this.tb_mail_add.SelectedTab.Name == "tb_partner") ? this.flexEmp : this.flexPart;
                    if (flexGrid2.DataTable.Select("S = 'Y'").Length == 0)
                        return;
                    List<Duzon.BizOn.Erpu.Net.Mail.MailMessage> message = new List<Duzon.BizOn.Erpu.Net.Mail.MailMessage>();
                    if (this._setRefTab == "Y")
                    {
                        string str1 = string.Empty;
                        Duzon.BizOn.Erpu.Net.Mail.MailMessage mailMessage = new Duzon.BizOn.Erpu.Net.Mail.MailMessage();
                        if (this.rdo_smtp.Checked)
                        {
                            mailMessage.Server = BusinessInfo.ServerInfo.SMTP;
                            mailMessage.MailSenderType = MailSenderType.SMTP;
                        }
                        else
                            mailMessage.MailSenderType = MailSenderType.Outlook;
                        string text1 = this._txt보내는사람.Text;
                        string text2 = this._txt보내는사람.Text;
                        mailMessage.From = new MailAddress(text1, text2);
                        if (this._setRefTab == "Y")
                        {
                            foreach (DataRow dataRow in this.flexRef.DataTable.Select("S = 'Y'"))
                                mailMessage.CC.Add(dataRow["NM_EMAIL"].ToString());
                        }
                        if (this.rdo_outlook.Checked)
                            mailMessage.IsBodyHtml = false;
                        else
                            mailMessage.IsBodyHtml = true;
                        mailMessage.Subject = this._txt제목.Text;
                        mailMessage.SubjectEncoding = Encoding.UTF8;
                        if (this.rdo_outlook.Checked)
                            mailMessage.Body = this._txt내용.Text;
                        else
                            mailMessage.Body = this.ConvertRtbToHTML(this._txt내용);
                        mailMessage.BodyEncoding = Encoding.UTF8;
                        for (int @fixed = flexGrid2.Rows.Fixed; @fixed < flexGrid2.Rows.Count; ++@fixed)
                        {
                            if (!(D.GetString(flexGrid2[@fixed, "S"]) != "Y"))
                            {
                                string @string = D.GetString(flexGrid2[@fixed, "NM_EMAIL"]);
                                mailMessage.To.Add(@string);
                            }
                        }
                        string str2 = flexGrid2.DataTable.Rows[0]["FILE_PATH"].ToString();
                        string path2 = path1 + "\\" + D.GetString(str2);
                        if (File.Exists(path2))
                            mailMessage.AttachmentFiles.Add(path2);
                        string str3 = this._filepath;
                        if (D.GetString(str3) != "")
                        {
                            string[] strArray1 = str3.Split('?');
                            for (int index = 0; index < strArray1.Length; ++index)
                            {
                                if (!(D.GetString(strArray1[index]) == ""))
                                {
                                    string[] strArray2 = strArray1[index].Split('|');
                                    if (strArray2.Length >= 3)
                                        mailMessage.AttachmentFiles.Add(D.GetString(strArray2[0]));
                                }
                            }
                        }
                        message.Add(mailMessage);
                        Thread.Sleep(500);
                        IMailSender senderFormInstance = new MailSenderFactory().CreateMailSenderFormInstance();
                        ((Form)senderFormInstance).Owner = (Form)this;
                        if (senderFormInstance.Send(message))
                        {
                            Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, Global.MainFrame.DD("전송"));
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    else
                    {
                        for (int @fixed = flexGrid2.Rows.Fixed; @fixed < flexGrid2.Rows.Count; ++@fixed)
                        {
                            if (!(D.GetString(flexGrid2[@fixed, "S"]) != "Y"))
                            {
                                Duzon.BizOn.Erpu.Net.Mail.MailMessage mailMessage = new Duzon.BizOn.Erpu.Net.Mail.MailMessage();
                                if (this.rdo_smtp.Checked)
                                {
                                    mailMessage.Server = BusinessInfo.ServerInfo.SMTP;
                                    mailMessage.MailSenderType = MailSenderType.SMTP;
                                }
                                else
                                    mailMessage.MailSenderType = MailSenderType.Outlook;
                                string text1 = this._txt보내는사람.Text;
                                string text2 = this._txt보내는사람.Text;
                                mailMessage.From = new MailAddress(text1, text2);
                                if (this.rdo_outlook.Checked)
                                    mailMessage.IsBodyHtml = false;
                                else
                                    mailMessage.IsBodyHtml = true;
                                mailMessage.Subject = this._txt제목.Text;
                                mailMessage.SubjectEncoding = Encoding.UTF8;
                                if (this.rdo_outlook.Checked)
                                    mailMessage.Body = this._txt내용.Text;
                                else
                                    mailMessage.Body = this.ConvertRtbToHTML(this._txt내용);
                                mailMessage.BodyEncoding = Encoding.UTF8;
                                string @string = D.GetString(flexGrid2[@fixed, "NM_EMAIL"]);
                                mailMessage.To.Add(@string);
                                string path2 = path1 + "\\" + D.GetString(flexGrid2[@fixed, "FILE_PATH"]);
                                if (File.Exists(path2))
                                    mailMessage.AttachmentFiles.Add(path2);
                                string str = this._filepath;
                                if (D.GetString(str) != "")
                                {
                                    string[] strArray1 = str.Split('?');
                                    for (int index = 0; index < strArray1.Length; ++index)
                                    {
                                        if (!(D.GetString(strArray1[index]) == ""))
                                        {
                                            string[] strArray2 = strArray1[index].Split('|');
                                            if (strArray2.Length >= 3)
                                                mailMessage.AttachmentFiles.Add(D.GetString(strArray2[0]));
                                        }
                                    }
                                }
                                message.Add(mailMessage);
                                Thread.Sleep(500);
                            }
                        }
                        IMailSender senderFormInstance = new MailSenderFactory().CreateMailSenderFormInstance();
                        ((Form)senderFormInstance).Owner = (Form)this;
                        bool flag = senderFormInstance.Send(message);
                        if (flag)
                        {
                            Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, Global.MainFrame.DD("전송"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tb_mail_add.SelectedTab.Name == "tb_partner")
                {
                    this.flexPart.Rows.Add();
                    this.flexPart.Row = this.flexPart.Rows.Count - 1;
                    this.flexPart["FILE_PATH"] = this.FilePath(D.GetString(this._cd_partner[0]));
                    this.flexPart["CD_PARTNER"] = D.GetString(this._cd_partner[0]);
                    this.flexPart["NM_PTR"] = "";
                    this.flexPart.Col = this.flexPart.Cols["NM_PTR"].Index;
                    this.flexPart.AddFinished();
                    this.flexPart.Focus();
                }
                if (this.tb_mail_add.SelectedTab.Name == "tb_emp")
                {
                    this.flexEmp.Rows.Add();
                    this.flexEmp.Row = this.flexEmp.Rows.Count - 1;
                    this.flexEmp["FILE_PATH"] = this.FilePath(D.GetString(this._cd_partner[0]));
                    this.flexEmp["CD_PARTNER"] = D.GetString(this._cd_partner[0]);
                    this.flexEmp["NM_PTR"] = "";
                    this.flexEmp["NM_EMAIL"] = "";
                    this.flexEmp.Col = this.flexEmp.Cols["NM_PTR"].Index;
                    this.flexEmp.AddFinished();
                    this.flexEmp.Focus();
                }
                if (!(this.tb_mail_add.SelectedTab.Name == "tb_ref"))
                    return;
                this.flexRef.Rows.Add();
                this.flexRef.Row = this.flexRef.Rows.Count - 1;
                this.flexRef["FILE_PATH"] = this.FilePath(D.GetString(this._cd_partner[0]));
                this.flexRef["CD_PARTNER"] = D.GetString(this._cd_partner[0]);
                this.flexRef["NM_PTR"] = string.Empty;
                this.flexRef["NM_EMAIL"] = string.Empty;
                this.flexRef.Col = this.flexRef.Cols["NM_PTR"].Index;
                this.flexRef.AddFinished();
                this.flexRef.Focus();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flexGrid = new FlexGrid();
                if (this.tb_mail_add.SelectedTab.Name == "tb_partner")
                    flexGrid = this.flexPart;
                if (this.tb_mail_add.SelectedTab.Name == "tb_emp")
                    flexGrid = this.flexEmp;
                if (this.tb_mail_add.SelectedTab.Name == "tb_ref")
                    flexGrid = this.flexRef;
                foreach (DataRow dataRow in flexGrid.DataTable.Select("S = 'Y'"))
                    dataRow.Delete();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void BtnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                P_MAIL_FILE_SUB pMailFileSub = new P_MAIL_FILE_SUB(D.GetString(this._filepath));
                if (pMailFileSub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                    this._filepath = D.GetString(pMailFileSub.ret_str_file);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                FlexGrid flexGrid = sender as FlexGrid;
                HelpReturn helpReturn = e.Result;
                switch (flexGrid.Cols[e.Col].Name)
                {
                    case "NM_PTR":
                        if (e.Result.DialogResult == DialogResult.Cancel)
                            break;
                        bool flag = true;
                        flexGrid.Redraw = false;
                        foreach (DataRow dataRow in helpReturn.Rows)
                        {
                            if (flexGrid.Name == "flexEmp")
                            {
                                if (flag)
                                {
                                    this.flexEmp[e.Row, "FILE_PATH"] = this.FilePath(D.GetString(this._cd_partner[0]));
                                    this.flexEmp[e.Row, "CD_PARTNER"] = D.GetString(this._cd_partner[0]);
                                    this.flexEmp[e.Row, "NM_PTR"] = D.GetString(dataRow["NM_KOR"]);
                                    this.flexEmp[e.Row, "NM_EMAIL"] = D.GetString(dataRow["NO_EMAIL"]);
                                    this.flexEmp.Col = this.flexEmp.Cols["NM_PTR"].Index;
                                    this.flexEmp.AddFinished();
                                    flag = false;
                                }
                                else
                                {
                                    this.flexEmp.Rows.Add();
                                    this.flexEmp.Row = this.flexEmp.Rows.Count - 1;
                                    this.flexEmp["FILE_PATH"] = this.FilePath(D.GetString(this._cd_partner[0]));
                                    this.flexEmp["CD_PARTNER"] = D.GetString(this._cd_partner[0]);
                                    this.flexEmp["NM_PTR"] = D.GetString(dataRow["NM_KOR"]);
                                    this.flexEmp["NM_EMAIL"] = D.GetString(dataRow["NO_EMAIL"]);
                                    this.flexEmp.Col = this.flexEmp.Cols["NM_PTR"].Index;
                                    this.flexEmp.AddFinished();
                                }
                            }
                            if (flexGrid.Name == "flexRef")
                            {
                                if (flag)
                                {
                                    this.flexRef[e.Row, "FILE_PATH"] = this.FilePath(D.GetString(this._cd_partner[0]));
                                    this.flexRef[e.Row, "CD_PARTNER"] = D.GetString(this._cd_partner[0]);
                                    this.flexRef[e.Row, "NM_PTR"] = D.GetString(dataRow["NM_KOR"]);
                                    this.flexRef[e.Row, "NM_EMAIL"] = D.GetString(dataRow["NO_EMAIL"]);
                                    this.flexRef.Col = this.flexEmp.Cols["NM_PTR"].Index;
                                    this.flexRef.AddFinished();
                                    flag = false;
                                }
                                else
                                {
                                    this.flexRef.Rows.Add();
                                    this.flexRef.Row = this.flexRef.Rows.Count - 1;
                                    this.flexRef["FILE_PATH"] = this.FilePath(D.GetString(this._cd_partner[0]));
                                    this.flexRef["CD_PARTNER"] = D.GetString(this._cd_partner[0]);
                                    this.flexRef["NM_PTR"] = D.GetString(dataRow["NM_KOR"]);
                                    this.flexRef["NM_EMAIL"] = D.GetString(dataRow["NO_EMAIL"]);
                                    this.flexRef.Col = this.flexRef.Cols["NM_PTR"].Index;
                                    this.flexRef.AddFinished();
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this.flexEmp.Redraw = true;
                this.flexRef.Redraw = true;
            }
        }

        private void P_MF_EMAIL_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                string str = string.Empty;
                if (this.flexEmp.HasNormalRow)
                {
                    foreach (DataRow dataRow in this.flexEmp.DataTable.Select())
                    {
                        if (D.GetString(dataRow["YN_SAVE"]) != "N")
                            str = str + D.GetString(dataRow["NM_PTR"]) + "|" + D.GetString(dataRow["NM_EMAIL"]) + "|" + D.GetString(dataRow["YN_SAVE"]) + "?";
                    }
                }
                this._str_rt_data = new string[1]
        {
          str
        };
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void tb_mail_add_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tb_mail_add.SelectedTab.Name == "tb_ref")
                this.btnFileUpload.Visible = false;
            else
                this.btnFileUpload.Visible = true;
        }

        public string ConvertRtbToHTML(RichTextBox rtb)
        {
            if (rtb.Text.Length == 0)
                return (string)null;
            string str1 = "";
            int start = 0;
            int length = rtb.Text.Length;
            rtb.Select(0, 1);
            string str2 = ColorTranslator.ToHtml(rtb.SelectionColor);
            bool bold = rtb.SelectionFont.Bold;
            bool italic = rtb.SelectionFont.Italic;
            string name = rtb.SelectionFont.FontFamily.Name;
            short num = (short)rtb.SelectionFont.Size;
            string str3 = str1 + "<span style=\"font-family: " + name + "; font-size: " + num.ToString() + "pt; color: " + str2 + "\">";
            if (bold)
                str3 += "<b>";
            if (italic)
                str3 += "<i>";
            string str4 = str3 + rtb.Text.Substring(0, 1);
            for (int index1 = 2; index1 <= rtb.Text.Length; ++index1)
            {
                rtb.Select(index1 - 1, 1);
                string str5 = rtb.Text.Substring(index1 - 1, 1);
                switch (str5)
                {
                    case "\n":
                        str4 += "<br>";
                        break;
                    case "\t":
                        str4 += "&nbsp;&nbsp;&nbsp;&nbsp;";
                        str5 = "";
                        break;
                    case " ":
                        str4 += "&nbsp;";
                        str5 = "";
                        break;
                    case "<":
                        str4 += "&lt;";
                        str5 = "";
                        break;
                    case ">":
                        str4 += "&gt;";
                        str5 = "";
                        break;
                    case "&":
                        str4 += "&amp;";
                        str5 = "";
                        break;
                }
                Color selectionColor = rtb.SelectionColor;
                if (selectionColor.ToKnownColor().ToString() != str2 || rtb.SelectionFont.FontFamily.Name != name || (double)rtb.SelectionFont.Size != (double)num)
                {
                    object[] objArray1 = new object[8]
          {
             str4,
             "</span><span style=\"font-family: ",
             rtb.SelectionFont.FontFamily.Name,
             "; font-size: ",
             rtb.SelectionFont.Size,
             "pt; color: ",
            null,
            null
          };
                    object[] objArray2 = objArray1;
                    int index2 = 6;
                    selectionColor = rtb.SelectionColor;
                    string str6 = selectionColor.ToKnownColor().ToString();
                    objArray2[index2] = str6;
                    objArray1[7] = "\">";
                    str4 = string.Concat(objArray1);
                }
                if (rtb.SelectionFont.Bold != bold)
                    str4 = rtb.SelectionFont.Bold ? str4 + "<b>" : str4 + "</b>";
                if (rtb.SelectionFont.Italic != italic)
                    str4 = rtb.SelectionFont.Italic ? str4 + "<i>" : str4 + "</i>";
                str4 += str5;
                selectionColor = rtb.SelectionColor;
                str2 = selectionColor.ToKnownColor().ToString();
                bold = rtb.SelectionFont.Bold;
                italic = rtb.SelectionFont.Italic;
                name = rtb.SelectionFont.FontFamily.Name;
                num = (short)rtb.SelectionFont.Size;
            }
            if (bold)
                str4 = str4 ?? "";
            if (italic)
                str4 = str4 ?? "";
            string str7 = str4 + "</span>";
            rtb.Select(start, length);
            return str7;
        }

        private void MakePdf()
        {
            try
            {
                string path = this.GetPath();
                DataTable dataTable = (DataTable)this.cbo_Print.DataSource;
                string str1 = string.Empty;
                if (dataTable.Columns.Contains("TP_REPORT"))
                    str1 = D.GetString(dataTable.Select("ID_OBJECT ='" + D.GetString(this.cbo_Print.SelectedValue) + "'")[0]["TP_REPORT"]);
                if (this._cd_partner.Length == 0)
                {
                    string str2 = this.FilePath("");
                    if (str1 == "1")
                        this._rptHelper[0].PrintDirect(D.GetString(this.cbo_Print.SelectedValue) + ".DRF", false, true, path + "\\" + str2, this._dic);
                    else
                        this._rptHelper[0].PrintDirect(D.GetString(this.cbo_Print.SelectedValue) + ".RDF", false, true, path + "\\" + str2, this._dic);
                }
                else
                {
                    for (int index = 0; index < this._rptHelper.Length; ++index)
                    {
                        string str2 = this.FilePath(this._cd_partner[index]);
                        if (str1 == "1")
                            this._rptHelper[index].PrintDirect(D.GetString(this.cbo_Print.SelectedValue) + ".DRF", false, true, path + "\\" + str2, this._dic);
                        else
                            this._rptHelper[index].PrintDirect(D.GetString(this.cbo_Print.SelectedValue) + ".RDF", false, true, path + "\\" + str2, this._dic);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void SetEmpGrid()
        {
            try
            {
                string str1 = string.Empty;
                foreach (string str2 in this._cd_partner)
                    str1 = str1 + "|" + D.GetString(str2);
                string str3 = str1 + "|";
                DataSet dataSet = DBHelper.GetDataSet("UP_MF_EMAIL_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                           str3,
                                                                                           Global.MainFrame.LoginInfo.UserID });
                this.flexPart.Binding = dataSet.Tables[1];
                foreach (DataRow dataRow in (InternalDataCollectionBase)dataSet.Tables[1].Rows)
                    dataRow["FILE_PATH"] = this.FilePath(D.GetString(dataRow["CD_PARTNER"]));
                DataTable dataTable1 = dataSet.Tables[1].Clone();
                dataTable1.Columns.Add("YN_SAVE", typeof(string), "");
                this.flexEmp.Binding = dataTable1;
                DataTable dataTable2 = new DataTable();
                dataTable2.Columns.Add("MAIL_ADD", typeof(string), "");
                dataTable2.Columns.Add("USER_ADD", typeof(string), "");
                if (D.GetString(this._send_email_add) != "")
                {
                    string[] strArray1 = this._send_email_add.Split('?');
                    if (strArray1.Length > 0)
                    {
                        for (int index = 0; index < strArray1.Length; ++index)
                        {
                            if (!(strArray1[index].Trim() == ""))
                            {
                                string[] strArray2 = strArray1[index].Split('|');
                                if (strArray2.Length >= 2)
                                {
                                    if (dataTable2.Select("MAIL_ADD = '" + D.GetString(strArray2[1]) + "' AND USER_ADD = '" + D.GetString(strArray2[0]) + "'").Length <= 0)
                                    {
                                        dataTable2.Rows.Add(D.GetString(strArray2[1]), D.GetString(strArray2[0]));
                                        this.flexEmp.Rows.Add();
                                        this.flexEmp.Row = this.flexEmp.Rows.Count - 1;
                                        this.flexEmp["FILE_PATH"] = this.FilePath(D.GetString(this._cd_partner[0]));
                                        this.flexEmp["CD_PARTNER"] = D.GetString(this._cd_partner[0]);
                                        this.flexEmp["NM_PTR"] = strArray2[0];
                                        this.flexEmp["NM_EMAIL"] = strArray2[1];
                                        if (strArray2.Length > 2)
                                            this.flexEmp["YN_SAVE"] = strArray2[2];
                                        this.flexEmp.Col = this.flexEmp.Cols["NM_PTR"].Index;
                                        this.flexEmp.AddFinished();
                                    }
                                }
                            }
                        }
                    }
                }
                if (this._setRefTab == "Y")
                {
                    DataTable dataTable3 = dataSet.Tables[1].Clone();
                    dataTable3.Columns.Add("YN_SAVE", typeof(string), "");
                    this.flexRef.Binding = dataTable3;
                    DataTable dataTable4 = new DataTable();
                    dataTable4.Columns.Add("MAIL_ADD", typeof(string), "");
                    dataTable4.Columns.Add("USER_ADD", typeof(string), "");
                    if (D.GetString(this._sendref_email_add) != "")
                    {
                        string[] strArray1 = this._sendref_email_add.Split('?');
                        if (strArray1.Length > 0)
                        {
                            for (int index = 0; index < strArray1.Length; ++index)
                            {
                                if (!(strArray1[index].Trim() == ""))
                                {
                                    string[] strArray2 = strArray1[index].Split('|');
                                    if (strArray2.Length >= 2)
                                    {
                                        if (dataTable4.Select("MAIL_ADD = '" + D.GetString(strArray2[1]) + "' AND USER_ADD = '" + D.GetString(strArray2[0]) + "'").Length <= 0)
                                        {
                                            dataTable4.Rows.Add(D.GetString(strArray2[1]), D.GetString(strArray2[0]));
                                            this.flexRef.Rows.Add();
                                            this.flexRef.Row = this.flexRef.Rows.Count - 1;
                                            this.flexRef["FILE_PATH"] = this.FilePath(D.GetString(this._cd_partner[0]));
                                            this.flexRef["CD_PARTNER"] = D.GetString(this._cd_partner[0]);
                                            this.flexRef["NM_PTR"] = strArray2[0];
                                            this.flexRef["NM_EMAIL"] = strArray2[1];
                                            if (strArray2.Length > 2)
                                                this.flexRef["YN_SAVE"] = strArray2[2];
                                            this.flexRef.Col = this.flexRef.Cols["NM_PTR"].Index;
                                            this.flexRef.AddFinished();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                this.MailSetting(dataSet);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void MailSetting(DataSet ds)
        {
            try
            {
                if (this._setSendUser == "EMP_USER")
                {
                    if (ds.Tables[2].Rows.Count != 0)
                        this._txt보내는사람.Text = D.GetString(ds.Tables[2].Rows[0]["EMAIL"]);
                    else
                        this._txt보내는사람.Text = string.Empty;
                }
                else if (ds.Tables[0].Rows.Count != 0)
                    this._txt보내는사람.Text = D.GetString(ds.Tables[0].Rows[0]["EMAIL"]);
                else
                    this._txt보내는사람.Text = string.Empty;
                if (!string.IsNullOrEmpty(this._str_title))
                    this._txt제목.Text = D.GetString(this._str_title);
                if (string.IsNullOrEmpty(this._str_ntext))
                    return;
                this._txt내용.Text = D.GetString(this._str_ntext);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private string GetPath()
        {
            string path = Application.StartupPath + "\\download\\mail";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        public string FilePath(string cd_partner)
        {
            try
            {
                return D.GetString(cd_partner) + "_" + this._id_object + ".pdf";
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
                return string.Empty;
            }
        }

        private void cbo_Print_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this._id_object != "R_TR_EXINVVI_MAIL_0")
                return;
            if (D.GetString(this.cbo_Print.Text) == "INVOICE")
            {
                this._rptHelper[0].SetDataTable(this._dt_temp1, 1);
                this._rptHelper[0].SetDataTable(this._dt_temp1, 2);
            }
            else
            {
                if (!(D.GetString(this.cbo_Print.Text) == "PACKING LIST"))
                    return;
                this._rptHelper[0].SetDataTable(this._dt_temp2, 1);
                this._rptHelper[0].SetDataTable(this._dt_temp2, 2);
            }
        }
    }
}

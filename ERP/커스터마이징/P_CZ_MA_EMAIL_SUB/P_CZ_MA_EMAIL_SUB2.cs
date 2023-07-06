using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using System.IO;
using System.Net.Mail;
using Dintec;
using System.Net;

namespace cz
{
    public partial class P_CZ_MA_EMAIL_SUB2 : Duzon.Common.Forms.CommonDialog
    {
        public P_CZ_MA_EMAIL_SUB2()
        {
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitEvent();
            this.InitGrid();
        }

        private void InitEvent()
        {
            this.btn사원조회.Click += new EventHandler(this.btn사원조회_Click);
            this.btn첨부파일경로설정.Click += new EventHandler(this.btn첨부파일경로설정_Click);
            this.btn메일발송.Click += new EventHandler(this.btn메일발송_Click);

            this._flex사원정보.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
        }

        private void InitGrid()
        {
            this._flex사원정보.BeginSetting(1, 1, false);

            this._flex사원정보.SetCol("S", "S", 40, true, CheckTypeEnum.Y_N);
            this._flex사원정보.SetCol("NO_EMP", "사번", 80);
            this._flex사원정보.SetCol("NM_KOR", "이름", 80);
            this._flex사원정보.SetCol("NO_EMAIL", "메일주소", 150);
            this._flex사원정보.SetCol("NM_FILE", "파일경로", 150);

            this._flex사원정보.SetOneGridBinding(null, this.pnl메일정보);

            this._flex사원정보.ExtendLastCol = true;

            this._flex사원정보.SettingVersion = "0.0.0.1";
            this._flex사원정보.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex사원정보.Styles.Add("미확인").BackColor = Color.White;
            this._flex사원정보.Styles.Add("확인").BackColor = Color.Yellow;
        }

        private void btn사원조회_Click(object sender, EventArgs e)
        {
            string query;

            try
            {
                query = @"SELECT 'N' AS S,
                                 NO_EMP, 
                          	     NM_KOR,
                                 NM_KOR + ' 님' AS NM_RECIPIENT,
                          	     NO_EMAIL,
                                 '' AS NM_FILE
                          FROM MA_EMP WITH(NOLOCK)
                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                        @"AND CD_INCOM <> '099'
                          AND ISNULL(DT_RETIRE, '00000000') = '00000000'
                          AND TP_EMP IN ('100', '200')
                          ORDER BY NO_EMP";

                this._flex사원정보.Binding = DBHelper.GetDataTable(query);

                if (!this._flex사원정보.HasNormalRow)
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn첨부파일경로설정_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.ShowNewFolderButton = false;

                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                this.txt첨부파일경로.Text = dialog.SelectedPath;

                dt = this._flex사원정보.DataTable.Copy();

                foreach (DataRow dr in dt.Rows)
                {
                    if (File.Exists(this.txt첨부파일경로.Text + "\\" + dr["NO_EMP"].ToString() + "." + this.txt파일확장자.Text))
                        dr["NM_FILE"] = (this.txt첨부파일경로.Text + "\\" + dr["NO_EMP"].ToString() + "." + this.txt파일확장자.Text);
                    else
                        dr["NM_FILE"] = string.Empty;
                }

                this._flex사원정보.Binding = dt;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn메일발송_Click(object sender, EventArgs e)
        {
            SmtpClient smtpClient;
            MailMessage mailMessage;
            DBMgr dbMgr;
            DataTable dt;
            DataRow[] dataRowArray, dataRowArray1;
            StringBuilder 메일발송리스트;
            string id, pw, domain, query;
            string[] tempText;

            try
            {
                if (string.IsNullOrEmpty(this.txt보내는사람메일주소.Text) || string.IsNullOrEmpty(this.txt보내는사람이름.Text))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl보내는사람.Text);
                    return;
                }

                if (string.IsNullOrEmpty(this.txt첨부파일경로.Text))
                {
                    Global.MainFrame.ShowMessage("첨부파일 경로가 설정되어 있지 않습니다.");
                    return;
                }

                dataRowArray = this._flex사원정보.DataTable.Select("S = 'Y'").OrderBy(x => x["NO_EMP"].ToString()).ToArray();

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage("선택된 사원이 없습니다.");
                    return;
                }
                else
                {
                    dataRowArray1 = this._flex사원정보.DataTable.Select("S = 'Y' AND ISNULL(NM_FILE, '') = ''");
                    if (dataRowArray1 != null && dataRowArray1.Length > 0)
                    {
                        if (Global.MainFrame.ShowMessage("첨부파일이 없는 사원이 선택되어 있습니다.\n진행하시겠습니까 ?", "QY2") == DialogResult.No)
                            return;
                    }
                    else
                    {
                        if (Global.MainFrame.ShowMessage("단체 메일 발송을 시작합니다.\n진행하시겠습니까 ?", "QY2") == DialogResult.No)
                            return;
                    }

                    #region 메일보내기

                    #region 보내는사람
                    tempText = this.txt보내는사람메일주소.Text.Split('@');

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

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        Global.MainFrame.ShowMessage("보내는사람 메일 주소가 메일서버에 존재하지 않습니다.\n위와 같은 오류가 나는 경우는 아래와 같습니다.\n1. 정상적인 메일 주소가 아닐 경우\n2. 회사메일계정이 아닌경우\n3. 메일링리스트 일 경우");
                        return;
                    }

                    pw = D.GetString(dt.Rows[0]["DU_PWD"]);
                    #endregion

                    #region SMTP Client 설정
                    smtpClient = new SmtpClient("113.130.254.131", 587);
                    smtpClient.EnableSsl = false;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(this.txt보내는사람메일주소.Text, pw);
                    #endregion

                    메일발송리스트 = new StringBuilder();
                    foreach (DataRow dr in dataRowArray)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(dr["NM_FILE"].ToString()))
                                mailMessage = this.메일메시지생성(dr["NO_EMAIL"].ToString(),
                                                                  dr["NM_RECIPIENT"].ToString(),
                                                                  dr["NM_FILE"].ToString());
                            else
                                mailMessage = this.메일메시지생성(dr["NO_EMAIL"].ToString(),
                                                                  dr["NM_RECIPIENT"].ToString(),
                                                                  string.Empty);

                            smtpClient.Send(mailMessage);

                            메일발송리스트.AppendLine("성공 : " + dr["NO_EMP"].ToString() + "/" 
                                                                + dr["NM_KOR"].ToString() + "/"
                                                                + dr["NO_EMAIL"].ToString());
                        }
                        catch (Exception ex)
                        {
                            메일발송리스트.AppendLine("실패 : " + dr["NO_EMP"].ToString() + "/" 
                                                                + dr["NM_KOR"].ToString() + "/" 
                                                                + dr["NO_EMAIL"].ToString() + "/"
                                                                + ex.Message);
                        }
                    }
                    #endregion

                    Global.MainFrame.ShowDetailMessage("단체메일발송 작업을 완료 했습니다.\n▼ 버튼을 눌러서 메일 발송 결과를 확인하세요!", 메일발송리스트.ToString());
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);

                if (!grid.HasNormalRow) return;

                CellStyle cellStyle = grid.Rows[e.Row].Style;

                if (string.IsNullOrEmpty(grid[e.Row, "NM_FILE"].ToString()))
                {
                    if (cellStyle == null || cellStyle.Name != "미확인")
                        grid.Rows[e.Row].Style = grid.Styles["미확인"];
                }
                else
                {
                    if (cellStyle == null || cellStyle.Name != "확인")
                        grid.Rows[e.Row].Style = grid.Styles["확인"];
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private MailMessage 메일메시지생성(string 받는사람메일주소, string 받는사람, string 첨부파일경로)
        {
            MailMessage mailMessage;

            try
            {
                #region 기본설정
                mailMessage = new MailMessage();
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                #endregion

                #region 메일정보
                mailMessage.From = new MailAddress(this.txt보내는사람메일주소.Text, this.txt보내는사람이름.Text, Encoding.UTF8);
                mailMessage.To.Add(new MailAddress(받는사람메일주소, 받는사람));
                mailMessage.Subject = string.Format(this.txt제목.Text, 받는사람);

                // 본문 html로 변환할 시 <a>태그 앞뒤로 하고 <a>태그 내부는 건드리지 않음
                string body = "";
                string bodyA = "";
                string bodyB = "";
                string bodyC = "";

                int index = this.txt메시지내용.Text.IndexOf("<a href=");

                if (index > 0)
                {
                    bodyA = this.txt메시지내용.Text.Substring(0, index);
                    bodyB = this.txt메시지내용.Text.Substring(index, this.txt메시지내용.Text.IndexOf("</a>") + 4 - index);
                    bodyC = this.txt메시지내용.Text.Substring(this.txt메시지내용.Text.IndexOf("</a>") + 4);

                    body = ""
                        + bodyA.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />")
                        + bodyB
                        + bodyC.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />");
                }
                else
                {
                    body = this.txt메시지내용.Text.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />"); ;
                }

                mailMessage.Body = "<div style='font-family:맑은 고딕; font-size:9pt'>" + body + "</div>";

                if (!string.IsNullOrEmpty(첨부파일경로))
                    mailMessage.Attachments.Add(new Attachment(첨부파일경로));
                #endregion

                return mailMessage;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return null;
        }
    }
}

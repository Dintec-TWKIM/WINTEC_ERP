 using Aspose.Email.Outlook;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;



namespace cz
{
    public partial class P_CZ_MAIL_SERVICE_REG : PageBase
    {
        P_CZ_MAIL_SERVICE_REG_BIZ _biz = new P_CZ_MAIL_SERVICE_REG_BIZ();

        public int NO_MAIL { get; set; }

        string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
        string NO_EMP = Global.MainFrame.LoginInfo.UserID;

        string YN_PARSING = string.Empty;
        string NM_FILE_REAL = string.Empty;

        Outlook.Application app = null;
        Outlook._NameSpace ns = null;
        Outlook.MAPIFolder inboxFolder = null;

        string folderChange = string.Empty;

        int itemCount = 0;
        int itemCountD = 0;

        string fileName = "";
        string mailFileName = string.Empty;

        string imoNumber = string.Empty;
        string vessel = string.Empty;
        string reference = string.Empty;
        string partner = string.Empty;
        string tnid = string.Empty;
        string deliveryDt = string.Empty;

        string returnFileNo = string.Empty;
        string returnToMail = string.Empty;

        string origSender = string.Empty;
        string origCC = string.Empty;
        string origBCC = string.Empty;
        string origSize = string.Empty;
        string origSubject = string.Empty;
        string origTo = string.Empty;
        string sendTo = string.Empty;

        DataTable dbDatatb;


        // 키워드 DB 검색 후 값		
        string resultTo = string.Empty;
        string resultBCC = string.Empty;
        string resultCC = string.Empty;

        string resultCate1 = string.Empty;
        string resultCate2 = string.Empty;
        string resultCate3 = string.Empty;
        string resultCate4 = string.Empty;

        string resultKeyword = string.Empty;
        string resultHostMail = string.Empty;
        string resultMoveMail = string.Empty;
        string resultRmk = string.Empty;

        // NON DB 입력값
        string nonTo = string.Empty;
        string nonSubject = string.Empty;


        // 첨부파일등록 파싱 값 가져오기
        string fileNoStr = string.Empty;
        string fileSuplStr = string.Empty;
        string fileNameStr = string.Empty;


        #region ==================================================================================================== Property

        public string ImoNumber
        {
            get
            {
                return imoNumber;
            }
        }

        public string Vessel
        {
            get
            {
                return vessel;
            }
        }

        public string Reference
        {
            get
            {
                return reference;
            }
        }


        public string Partner
        {
            get
            {
                return partner;
            }
        }


        public string Tnid
        {
            get
            {
                return tnid;
            }
        }
        #endregion


        public P_CZ_MAIL_SERVICE_REG()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }


        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex };

            this._flex.BeginSetting(1, 1, true);

            this._flex.SetCol("NO_MAIL", "번호", false);
            this._flex.SetCol("MAIL_TO", "받는사람", 100);
            this._flex.SetCol("MAIL_CC", "참조", 100);
            this._flex.SetCol("MAIL_BCC", "숨은참조", 100);
            this._flex.SetCol("FOLDER", "폴더", 100);
            this._flex.SetCol("CATEGORY_1", "분류", 100);
            this._flex.SetCol("CATEGORY_2", "종류", 100);
            this._flex.SetCol("CATEGORY_3", "검색유무", 100);
            this._flex.SetCol("CATEGORY_4", "Maker", 100);
            this._flex.SetCol("KEYWORD", "키워드", 300);
            this._flex.SetCol("HOST_MAIL", "전달받을사람", false);
            this._flex.SetCol("MOVE_MAIL", "이동폴더", false);
            this._flex.SetCol("USE_YN", "사용여부", 100);
            this._flex.SetCol("DC_RMK", "비고", false);
            this._flex.SetCol("ID_INSERT", "등록자", false);
            this._flex.SetCol("DTS_INSERT", "등록일자", false);
            this._flex.SetCol("ID_UPDATE", "수정자", false);
            this._flex.SetCol("DTS_UPDATE", "수정일자", false);

            this._flex.ExtendLastCol = true;

            this._flex.SettingVersion = "19.01.18.02";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            timer1.Tick += new EventHandler(timer1_Tick);
            btn시작.Click += new EventHandler(btn시작_Click);
            btn중지.Click += new EventHandler(btn중지_Click);

            timer1.Stop();
        }




        #region 메인
        private void MailSave()
        {
            try
            {
                GetDBSearch();

                if (dbDatatb != null)
                {
                    app = new Outlook.Application();
                    ns = app.GetNamespace("MAPI");
                    ns.Logon(null, null, false, false);

                    inboxFolder = ns.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);


                    //if(string.IsNullOrEmpty(folderChange) || folderChange.Equals("Dongjin"))
                    //{
                    //    folderChange = "Service";
                    //}
                    //else if (folderChange.Equals("Service"))
                    //{
                    //    folderChange = "Dongjin";
                    //}

                    //folderChange = "Service";
                    folderChange = "1TEST";

                    Outlook.MAPIFolder uFolder = inboxFolder.Folders[folderChange];              // 메인폴더 SHIPSERV
                    Outlook.MAPIFolder moveFolder = inboxFolder.Folders["백업"];              // 이동폴더


                    Outlook.Items JunkItems = default(Outlook.Items);
                    JunkItems = uFolder.Items;



                    // 다른 폴더 확인 용
                    //Outlook.Folders folders = ns.Folders;

                    //foreach (Outlook.MAPIFolder f in folders)
                    //{
                    //    string test = f.Name;
                    //}

                    //for (int i = 1; i <= uFolder.Items.Count; i++)
                    //{

                    itemCount += 1;


                    int mailCountCheck = uFolder.Items.Count;

                    if (mailCountCheck != 0)
                    {
                        if (itemCount > mailCountCheck)
                            itemCount = 1;

                        // 메일함 비워졌는지 확인해야함.
                        Outlook.MailItem mailitem = (Microsoft.Office.Interop.Outlook.MailItem)uFolder.Items[itemCount];

                        string mailSubject = mailitem.Subject.ToString().Trim();                            // 제목
                        string mailHost = mailitem.SenderEmailAddress.ToLower().ToString().Trim();          // 보낸사람

                        if (!string.IsNullOrEmpty(mailSubject))
                        {
                            textBox1.AppendText("\r\n" + itemCount + ": " + mailitem.Subject.ToString());

                            if (mailSubject.ToUpper().Contains("RE:") && mailSubject.ToUpper().Contains("DINTEC - INQUIRY("))
                            {
                                int idx_s = mailSubject.ToUpper().IndexOf("RE:");
                                int idx_e = mailSubject.ToUpper().IndexOf("DINTEC - INQUIRY(");

                                string subjStrTail = mailSubject.Substring(idx_e, mailSubject.Length - idx_e).Trim();
                                string subjStrHead = mailSubject.Substring(idx_s, idx_e).Trim();

                                mailSubject = "RE: " + subjStrTail.Trim();

                                mailSubject = mailSubject.Replace("]", "").Trim();


                            }
                            else if (mailSubject.ToUpper().Contains("[FW]DINTEC - INQUIRY("))
                            {
                                int idx_s = mailSubject.ToUpper().IndexOf("[FW]DINTEC");

                                mailSubject = mailSubject.Substring(idx_s, mailSubject.Length - idx_s).Replace("[FW]", "RE: ").Trim();
                            }

                            // EWS 메일
                            if (mailSubject.Contains("Account Alert: Payment reminder for overdue invoice"))
                            {
                                string returnCompay = string.Empty;

                                FileLocalSave(mailitem);

                                MapiMessage msg = MapiMessage.FromFile(fileName);

                                string mailBodyStr = msg.Body.ToString();

                                string[] mailSpl = mailBodyStr.Trim().Split(new string[] { "\r\n" }, StringSplitOptions.None);

                                if (mailSpl.Length > 2)
                                {
                                    for (int loopC = 0; loopC < mailSpl.Length - 1; loopC++)
                                    {
                                        if (mailSpl[loopC].ToString().StartsWith("Dear"))
                                        {
                                            returnCompay = mailSpl[loopC].ToString().Replace("Dear", "").Replace(",", "").Trim();
                                            break;
                                        }
                                    }
                                }


                                if (!string.IsNullOrEmpty(returnCompay))
                                {
                                    string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                    query = query + "AND CATEGORY_1 = 'EWS'";
                                    query = query + "AND KEYWORD LIKE '%" + returnCompay + "%' ";
                                    query = query + "AND USE_YN = 'Y'";
                                    DataTable dt = DBMgr.GetDataTable(query);

                                    if (dt.Rows.Count > 0)
                                    {
                                        ResultEWS(mailitem, dt);
                                    }
                                    else
                                    {
                                        nonSubject = mailSubject;
                                        nonTo = mailHost;

                                        DBInsert_Non(mailHost, mailSubject);
                                    }
                                }
                            }
                            else if (mailHost.Contains("@shipnet.net"))
                            {
                                string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                query = query + "AND KEYWORD LIKE '%" + mailHost + "%' ";
                                query = query + "AND USE_YN = 'Y'";
                                DataTable dt = DBMgr.GetDataTable(query);

                                if (dt.Rows.Count > 0)
                                {
                                    ResultMail(mailitem, dt);
                                }
                                else
                                {
                                    nonSubject = mailSubject;
                                    nonTo = mailHost;

                                    DBInsert_Non(mailHost, mailSubject);
                                }
                            }
                            else if (mailHost.Contains("@milaha.com"))
                            {
                                string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                query = query + "AND KEYWORD LIKE '%@milaha.com%' ";
                                query = query + "AND USE_YN = 'Y'";
                                DataTable dt = DBMgr.GetDataTable(query);

                                if (dt.Rows.Count > 0)
                                {
                                    ResultMail(mailitem, dt);
                                }
                                else
                                {
                                    nonSubject = mailSubject;
                                    nonTo = mailHost;

                                    DBInsert_Non(mailHost, mailSubject);
                                }
                            }
                            else if (mailSubject.Contains("SPS Cyprus") || mailSubject.Contains("SPS S.A"))
                            {
                                string subjStr = string.Empty;
                                if (mailSubject.Contains("SPS Cyprus"))
                                    subjStr = "SPS Cyprus";
                                else if (mailSubject.Contains("SPS S.A"))
                                    subjStr = "SPS S.A";

                                if (!string.IsNullOrEmpty(subjStr))
                                {
                                    string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                    query = query + "AND KEYWORD LIKE '%" + subjStr + "%' ";
                                    query = query + "AND USE_YN = 'Y'";
                                    DataTable dt = DBMgr.GetDataTable(query);

                                    if (dt.Rows.Count > 0)
                                    {
                                        ResultSubject(mailitem, dt);
                                    }
                                    else
                                    {
                                        nonSubject = mailSubject;
                                        nonTo = mailHost;

                                        DBInsert_Non(mailHost, mailSubject);
                                    }
                                }
                            }
                            else if (mailSubject.Contains("CMA Ships France for Vessel APL"))
                            {
                                string _To = "judy.seo@dintec.co.kr";
                                string _BCC = "service2@dintec.co.kr";
                                string _CC = "";

                                sendMailSys(mailitem, _To, _CC, _BCC);
                            }
                            else if (mailSubject.StartsWith("VARIANCE IN:"))
                            {
                                ReferenceParseShipServ(mailitem);

                                if (!string.IsNullOrEmpty(returnFileNo))
                                {
                                    DBSelect();

                                    ReturnToSend(mailitem);
                                }
                            }
                            // ShipServ 파싱용
                            else if (mailSubject.ToLower().Contains("from") && mailSubject.ToLower().Contains("for"))
                            {
                                int idx_s = mailSubject.IndexOf("from");
                                string subjectStr = mailSubject.Substring(idx_s, mailSubject.Length - idx_s);

                                int idx_e = 0;

                                if (subjectStr.Contains("Co.Ltd."))
                                {
                                    idx_e = subjectStr.IndexOf("Co.Ltd.");
                                    subjectStr = subjectStr.Substring(0, idx_e).Replace("from", "").Trim();
                                }
                                else if (subjectStr.Contains("Ltd") && !subjectStr.Contains("Cyprus"))
                                {
                                    idx_e = subjectStr.IndexOf("Ltd");
                                    subjectStr = subjectStr.Substring(0, idx_e).Replace("from", "").Trim();
                                }
                                else
                                {
                                    idx_e = subjectStr.IndexOf("for");
                                    subjectStr = subjectStr.Substring(0, idx_e).Replace("from", "").Trim();
                                }

                                if (subjectStr.Contains(" for "))
                                {
                                    idx_e = subjectStr.IndexOf(" for");
                                    subjectStr = subjectStr.Substring(0, idx_e).Trim();
                                }

                                if (subjectStr.Contains("Hoegh LNG"))
                                {
                                    string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                    query = query + "AND KEYWORD LIKE '%" + "Hoegh" + "%' ";
                                    query = query + "AND USE_YN = 'Y'";
                                    DataTable dt = DBMgr.GetDataTable(query);

                                    if (dt.Rows.Count > 0)
                                    {
                                        ResultSubject(mailitem, dt);
                                    }
                                    else
                                    {
                                        nonSubject = mailSubject;
                                        nonTo = mailHost;

                                        DBInsert_Non(mailHost, mailSubject);
                                    }
                                }
                                else if (!string.IsNullOrEmpty(subjectStr))
                                {
                                    string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                    query = query + "AND KEYWORD LIKE '%" + subjectStr + "%' ";
                                    query = query + "AND USE_YN = 'Y'";
                                    DataTable dt = DBMgr.GetDataTable(query);

                                    if (dt.Rows.Count > 0)
                                    {
                                        ResultSubject(mailitem, dt);
                                    }
                                    else
                                    {
                                        nonSubject = mailSubject;
                                        nonTo = mailHost;

                                        DBInsert_Non(mailHost, mailSubject);
                                    }
                                }
                            }
                            else if (mailHost.Contains("@vships.com") || mailHost.Contains("@marcassupplychain.com"))
                            {
                                string subjectStr = string.Empty;

                                int idx_s = 0;
                                int idx_e = 0;

                                if (mailSubject.Contains(">") && mailSubject.Contains("-"))
                                {
                                    idx_s = mailSubject.IndexOf(">");


                                    subjectStr = mailSubject.Substring(idx_s, mailSubject.Length - idx_s);

                                    idx_e = subjectStr.IndexOf("-");
                                    subjectStr = subjectStr.Substring(0, idx_e).Replace(">", "").Trim();
                                }
                                else if (mailSubject.Contains("-") && mailSubject.Contains("RFQ"))
                                {
                                    idx_e = mailSubject.IndexOf("-");

                                    subjectStr = mailSubject.Substring(0, idx_e).Trim();
                                }
                                else if (mailSubject.StartsWith("V.Ships"))
                                {
                                    idx_e = mailSubject.IndexOf(" - ");

                                    subjectStr = mailSubject.Substring(0, idx_e).Trim();
                                }


                                if (mailSubject.Contains("Quote Successfully saved!"))
                                {
                                    ReferenceParseVship(mailitem);

                                    if (!string.IsNullOrEmpty(returnFileNo))
                                    {

                                        DBSelect();

                                        ReturnToSend(mailitem);
                                    }
                                }
                                else
                                {
                                    subjectStr = subjectStr.Replace("RE", "").Replace(":", "").Trim();

                                    if (!string.IsNullOrEmpty(subjectStr))
                                    {
                                        string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                        query = query + "AND KEYWORD LIKE '%" + subjectStr + "%' ";
                                        query = query + "AND USE_YN = 'Y'";
                                        DataTable dt = DBMgr.GetDataTable(query);

                                        if (dt.Rows.Count > 0)
                                        {
                                            ResultSubject(mailitem, dt);
                                        }
                                        else
                                        {
                                            nonSubject = mailSubject;
                                            nonTo = mailHost;

                                            DBInsert_Non(nonTo, mailSubject);
                                        }
                                    }
                                }

                            }
                            else if (mailSubject.Contains("발주서 접수 확인 되었습니다."))
                            {
                                ReferenceParseWooYang(mailitem);

                                if (!string.IsNullOrEmpty(returnFileNo))
                                {
                                    DBSelect();

                                    ReturnToSend(mailitem);
                                }
                                else
                                {
                                    nonSubject = mailSubject;
                                    nonTo = mailHost;

                                    DBInsert_Non(nonTo, mailSubject);
                                }
                            }
                            // V.Ships 파싱용
                            else if (mailSubject.ToLower().Contains("quote successfully saved!"))
                            {
                                ReferenceParseVship(mailitem);

                                if (!string.IsNullOrEmpty(returnFileNo))
                                {
                                    DBSelect();

                                    ReturnToSend(mailitem);
                                }
                            }
                            else if (mailHost.Contains("@flynn-refrigeration.com"))
                            {
                                ReferenceParseFLYNN(mailitem);

                                if (!string.IsNullOrEmpty(returnFileNo))
                                {
                                    DBSelect();

                                    ReturnToSend(mailitem);
                                }
                            }
                            // 견적등록 해야 할 것들
                            else if (mailSubject.Replace("긴급문의건-", "").Replace("*견적 부탁드리겠습니다.*", "").Replace("*코드확인부탁드리겠습니다*", "").Replace("[견적회신]", "").Replace("RE: RE:", "RE:").Replace("유선문의건-", "").Replace("[SPAM]", "").Replace("[RE]", "RE: ").ToUpper().Trim().StartsWith("RE: DINTEC - INQUIRY("))
                            {
                                if (mailSubject.Contains("긴급문의건-"))
                                    mailSubject = mailSubject.Replace("긴급문의건-", "").Trim();

                                if (mailSubject.Contains("*견적 부탁드리겠습니다.*"))
                                    mailSubject = mailSubject.Replace("*견적 부탁드리겠습니다.*", "").Trim();

                                if (mailSubject.Contains("*코드확인부탁드리겠습니다*"))
                                    mailSubject = mailSubject.Replace("*코드확인부탁드리겠습니다*", "").Trim();

                                if (mailSubject.Contains("[견적회신]"))
                                    mailSubject = mailSubject.Replace("[견적회신]", "").Trim();

                                if (mailSubject.Contains("RE: RE:"))
                                    mailSubject = mailSubject.Replace("RE: RE:", "RE: ").Trim();

                                if (mailSubject.Contains("유선문의건-"))
                                    mailSubject = mailSubject.Replace("유선문의건-", "").Trim();

                                if (mailSubject.Contains("[SPAM]"))
                                    mailSubject = mailSubject.Replace("[SPAM]", "").Trim();

                                if (mailSubject.Contains("[RE]"))
                                    mailSubject = mailSubject.Replace("[RE]", "RE: ").Trim();

                                int idx_s = mailSubject.IndexOf("INQUIRY(");

                                // 파일번호
                                fileNoStr = mailSubject.Substring(idx_s, 18).Trim();
                                fileNoStr = fileNoStr.Replace("INQUIRY(", "").Trim();

                                int idx_e = mailSubject.IndexOf("_");

                                // 메일 제목이 변형이 되었는지 판단, "_" 유무
                                if (idx_e == -1)
                                    fileSuplStr = mailSubject.Substring(mailSubject.Length - 5, 5).Trim();
                                else
                                    fileSuplStr = mailSubject.Substring(idx_e, mailSubject.Length - idx_e);

                                idx_s = -1;
                                idx_s = mailSubject.IndexOf("IMO");

                                string imoStr = string.Empty;

                                if(idx_s != -1)
                                    imoStr = mailSubject.Substring(idx_s, 10).Trim();

                                if (!string.IsNullOrEmpty(imoStr) && imoStr.Contains(fileSuplStr))
                                {
                                    textBox1.AppendText(" -> 메일 제목을 확인하세요.");
                                }
                                else
                                {

                                    if (string.IsNullOrEmpty(fileSuplStr))
                                    {
                                        fileSuplStr = mailSubject.Substring(mailSubject.Length - 5, 5);
                                    }

                                    if (fileSuplStr.Length > 10)
                                    {
                                        fileSuplStr = fileSuplStr.Substring(1, 5).Trim();
                                    }
                                    else if (fileSuplStr.Length == 6)
                                    {
                                        fileSuplStr = fileSuplStr.Replace("_", "").Trim();
                                    }

                                    

                                    // 거래처코드
                                    //fileSuplStr = mailSubject.Substring(mailSubject.Length - 5, 5);

                                    AttSave(mailitem, moveFolder);
                                }

                            }
                            else if (mailSubject.ToUpper().StartsWith("[RE: DINTEC - INQUIRY("))
                            {
                                int idx_s = mailSubject.IndexOf("INQUIRY(");

                                // 파일번호
                                fileNoStr = mailSubject.Substring(idx_s, 18).Trim();
                                fileNoStr = fileNoStr.Replace("INQUIRY(", "").Trim();

                                int idx_e = mailSubject.IndexOf("_");

                                fileSuplStr = mailSubject.Substring(idx_e, mailSubject.Length - idx_e);

                                if (fileSuplStr.Length > 10)
                                {
                                    fileSuplStr = fileSuplStr.Substring(1, 5).Trim();
                                }
                                else if (fileSuplStr.Length == 6)
                                {
                                    fileSuplStr = fileSuplStr.Replace("_", "").Trim();
                                }

                                if (fileSuplStr.Length != 5)
                                    return;

                                // 거래처코드
                                //fileSuplStr = mailSubject.Substring(mailSubject.Length - 5, 5);

                                AttSave(mailitem, moveFolder);
                            }
                            else if (mailSubject.ToUpper().StartsWith("FW: DINTEC - INQUIRY(") || mailSubject.ToUpper().StartsWith("FWD: DINTEC - INQUIRY("))
                            {
                                int idx_s = mailSubject.IndexOf("INQUIRY(");

                                // 파일번호
                                fileNoStr = mailSubject.Substring(idx_s, 18).Trim();
                                fileNoStr = fileNoStr.Replace("INQUIRY(", "").Trim();

                                int idx_e = mailSubject.IndexOf("_");

                                fileSuplStr = mailSubject.Substring(idx_e, mailSubject.Length - idx_e);

                                if (fileSuplStr.Length > 10)
                                {
                                    fileSuplStr = fileSuplStr.Substring(1, 5).Trim();
                                }
                                else if (fileSuplStr.Length == 6)
                                {
                                    fileSuplStr = fileSuplStr.Replace("_", "").Trim();
                                }

                                if (fileSuplStr.Length != 5)
                                    return;

                                // 거래처코드
                                //fileSuplStr = mailSubject.Substring(mailSubject.Length - 5, 5);

                                AttSave(mailitem, moveFolder);
                            }
                            else if (mailSubject.StartsWith("New RFQ from "))
                            {
                                string[] subjStrSpl = mailSubject.Split('|');
                                string subjStr = string.Empty;
                                if(subjStrSpl.Length == 3)
                                {
                                    subjStr = subjStrSpl[0].ToString().Replace("New RFQ from","").Trim();
                                }

                                if (!string.IsNullOrEmpty(subjStr))
                                {
                                    string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                    query = query + "AND KEYWORD LIKE '%" + subjStr + "%' ";
                                    query = query + "AND USE_YN = 'Y'";
                                    DataTable dt = DBMgr.GetDataTable(query);

                                    if (dt.Rows.Count > 0)
                                    {
                                        resultTo = dt.Rows[0]["MAIL_TO"].ToString();
                                        resultCC = dt.Rows[0]["MAIL_CC"].ToString();
                                        resultBCC = dt.Rows[0]["MAIL_BCC"].ToString();

                                        resultCate1 = dt.Rows[0]["CATEGORY_1"].ToString();
                                        resultCate2 = dt.Rows[0]["CATEGORY_2"].ToString();
                                        resultCate3 = dt.Rows[0]["CATEGORY_3"].ToString();
                                        resultCate4 = dt.Rows[0]["CATEGORY_4"].ToString();

                                        resultKeyword = dt.Rows[0]["KEYWORD"].ToString();
                                        resultHostMail = dt.Rows[0]["HOST_MAIL"].ToString();
                                        resultMoveMail = dt.Rows[0]["MOVE_MAIL"].ToString();
                                        resultRmk = dt.Rows[0]["DC_RMK"].ToString();

                                        if (resultCate1.Equals("Subject"))
                                            sendMailSys(mailitem, resultTo, resultCC, resultBCC);
                                    }
                                    else
                                    {
                                        nonSubject = mailSubject;
                                        nonTo = mailHost;

                                        DBInsert_Non(nonTo, mailSubject);
                                    }
                                }


                            }
                            else if (mailSubject.StartsWith("[우양선기]") || mailSubject.StartsWith("견적서"))
                            {
                                int idx_s = mailSubject.IndexOf("견적서/");

                                // 파일번호
                                fileNoStr = mailSubject.Substring(idx_s, 14).Trim();
                                fileNoStr = fileNoStr.Replace("견적서/", "").Trim();

                                if (!fileNoStr.StartsWith("A-"))
                                {
                                    // 거래처코드
                                    fileSuplStr = "07249";
                                    AttSave(mailitem, moveFolder);
                                }
                            }
                            // BSM
                            else if (mailHost.Contains("@bs-shipmanagement.com"))
                            {
                                string subjectStr = string.Empty;

                                int idx_s = 0;
                                int idx_e = 0;

                                idx_s = mailSubject.IndexOf("BSM");
                                idx_e = mailSubject.IndexOf(")");

                                if (idx_s != 0 && idx_e != 0)
                                {
                                    subjectStr = mailSubject.Substring(idx_s, idx_e - idx_s + 1);
                                }

                                //subjectStr = subjectStr.Replace("RE", "").Replace(":", "").Trim();
                                if (!string.IsNullOrEmpty(subjectStr))
                                {
                                    string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                    query = query + "AND KEYWORD LIKE '%" + subjectStr + "%' ";
                                    query = query + "AND USE_YN = 'Y'";
                                    DataTable dt = DBMgr.GetDataTable(query);

                                    if (dt.Rows.Count > 0)
                                    {
                                        resultTo = dt.Rows[0]["MAIL_TO"].ToString();
                                        resultCC = dt.Rows[0]["MAIL_CC"].ToString();
                                        resultBCC = dt.Rows[0]["MAIL_BCC"].ToString();

                                        resultCate1 = dt.Rows[0]["CATEGORY_1"].ToString();
                                        resultCate2 = dt.Rows[0]["CATEGORY_2"].ToString();
                                        resultCate3 = dt.Rows[0]["CATEGORY_3"].ToString();
                                        resultCate4 = dt.Rows[0]["CATEGORY_4"].ToString();

                                        resultKeyword = dt.Rows[0]["KEYWORD"].ToString();
                                        resultHostMail = dt.Rows[0]["HOST_MAIL"].ToString();
                                        resultMoveMail = dt.Rows[0]["MOVE_MAIL"].ToString();
                                        resultRmk = dt.Rows[0]["DC_RMK"].ToString();

                                        if (resultCate1.Equals("Subject"))
                                            sendMailSys(mailitem, resultTo, resultCC, resultBCC);
                                    }
                                    else
                                    {
                                        nonSubject = mailSubject;
                                        nonTo = mailHost;

                                        DBInsert_Non(nonTo, mailSubject);
                                    }
                                }
                            }
                            else
                            {
                                string mailAdd = string.Empty;

                                int idx_s = mailHost.IndexOf("@");

                                mailAdd = mailHost.Substring(idx_s, mailHost.Length - idx_s).Trim();

                                if (!string.IsNullOrEmpty(mailAdd))
                                {
                                    string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                    query = query + "AND KEYWORD LIKE '%" + mailAdd + "%' ";
                                    query = query + "AND USE_YN = 'Y'";
                                    DataTable dt = DBMgr.GetDataTable(query);


                                    if (dt.Rows.Count > 0)
                                    {
                                        ResultMail(mailitem, dt);
                                    }
                                    else
                                    {
                                        nonSubject = mailSubject;
                                        nonTo = mailHost;

                                        DBInsert_Non(nonTo, nonSubject);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //MailSave();
            }
        }
        #endregion 메인

        #region 동진
        private void MailSaveDongjin()
        {
            try
            {
                GetDBSearch();

                if (dbDatatb != null)
                {
                    app = new Outlook.Application();
                    ns = app.GetNamespace("MAPI");
                    ns.Logon(null, null, false, false);

                    inboxFolder = ns.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);


                    //if(string.IsNullOrEmpty(folderChange) || folderChange.Equals("Dongjin"))
                    //{
                    //    folderChange = "Service";
                    //}
                    //else if (folderChange.Equals("Service"))
                    //{
                    //    folderChange = "Dongjin";
                    //}

                    folderChange = "Dongjin";
                    //folderChange = "1TEST";

                    Outlook.MAPIFolder uFolder = inboxFolder.Folders[folderChange];              // 메인폴더 SHIPSERV
                    Outlook.MAPIFolder moveFolder = inboxFolder.Folders["동진백업"];              // 이동폴더


                    Outlook.Items JunkItems = default(Outlook.Items);
                    JunkItems = uFolder.Items;



                    // 다른 폴더 확인 용
                    //Outlook.Folders folders = ns.Folders;

                    //foreach (Outlook.MAPIFolder f in folders)
                    //{
                    //    string test = f.Name;
                    //}

                    //for (int i = 1; i <= uFolder.Items.Count; i++)
                    //{

                    itemCountD += 1;

                    int mailCountCheck = uFolder.Items.Count;

                    if (mailCountCheck != 0)
                    {
                        if (itemCountD > mailCountCheck)
                            itemCountD = 1;

                        // 메일함 비워졌는지 확인해야함.
                        Outlook.MailItem mailitem = (Microsoft.Office.Interop.Outlook.MailItem)uFolder.Items[itemCountD];

                        string mailSubject = mailitem.Subject.ToString().Trim();                            // 제목
                        string mailHost = mailitem.SenderEmailAddress.ToLower().ToString().Trim();          // 보낸사람

                        if (!string.IsNullOrEmpty(mailSubject))
                        {

                            textBox1.AppendText("\r\n" + itemCountD + ": " + mailitem.Subject.ToString());


                            if (mailHost.Contains("@shipnet.net"))
                            {
                                string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                query = query + "AND KEYWORD LIKE '%" + mailHost + "%' ";
                                query = query + "AND USE_YN = 'Y'";
                                DataTable dt = DBMgr.GetDataTable(query);

                                if (dt.Rows.Count > 0)
                                {
                                    ResultMail(mailitem, dt);
                                }
                                else
                                {
                                    nonSubject = mailSubject;
                                    nonTo = mailHost;

                                    DBInsert_Non(mailHost, mailSubject);
                                }
                            }
                            else if (mailHost.Contains("@milaha.com"))
                            {
                                string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                query = query + "AND KEYWORD LIKE '%@milaha.com%' ";
                                query = query + "AND USE_YN = 'Y'";
                                DataTable dt = DBMgr.GetDataTable(query);

                                if (dt.Rows.Count > 0)
                                {
                                    ResultMail(mailitem, dt);
                                }
                                else
                                {
                                    nonSubject = mailSubject;
                                    nonTo = mailHost;

                                    DBInsert_Non(mailHost, mailSubject);
                                }
                            }
                            else if (mailSubject.Contains("SPS Cyprus") || mailSubject.Contains("SPS S.A"))
                            {
                                string subjStr = string.Empty;
                                if (mailSubject.Contains("SPS Cyprus"))
                                    subjStr = "SPS Cyprus";
                                else if (mailSubject.Contains("SPS S.A"))
                                    subjStr = "SPS S.A";

                                if (!string.IsNullOrEmpty(subjStr))
                                {
                                    string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                    query = query + "AND KEYWORD LIKE '%" + subjStr + "%' ";
                                    query = query + "AND USE_YN = 'Y'";
                                    DataTable dt = DBMgr.GetDataTable(query);

                                    if (dt.Rows.Count > 0)
                                    {
                                        ResultSubject(mailitem, dt);
                                    }
                                    else
                                    {
                                        nonSubject = mailSubject;
                                        nonTo = mailHost;

                                        DBInsert_Non(mailHost, mailSubject);
                                    }
                                }
                            }
                            else if (mailSubject.Contains("CMA Ships France for Vessel APL"))
                            {
                                string _To = "judy.seo@dintec.co.kr";
                                string _BCC = "service2@dintec.co.kr";
                                string _CC = "";

                                sendMailSys(mailitem, _To, _CC, _BCC);
                            }
                            else if (mailSubject.StartsWith("VARIANCE IN:"))
                            {
                                ReferenceParseShipServ(mailitem);

                                if (!string.IsNullOrEmpty(returnFileNo))
                                {
                                    DBSelect();

                                    ReturnToSend(mailitem);
                                }
                            }
                            // ShipServ 파싱용
                            else if (mailSubject.ToLower().Contains("from") && mailSubject.ToLower().Contains("for"))
                            {
                                int idx_s = mailSubject.IndexOf("from");
                                string subjectStr = mailSubject.Substring(idx_s, mailSubject.Length - idx_s);

                                int idx_e = 0;

                                if (subjectStr.Contains("Co.Ltd."))
                                {
                                    idx_e = subjectStr.IndexOf("Co.Ltd.");
                                    subjectStr = subjectStr.Substring(0, idx_e).Replace("from", "").Trim();
                                }
                                else if (subjectStr.Contains("Ltd") && !subjectStr.Contains("Cyprus"))
                                {
                                    idx_e = subjectStr.IndexOf("Ltd");
                                    subjectStr = subjectStr.Substring(0, idx_e).Replace("from", "").Trim();
                                }
                                else
                                {
                                    idx_e = subjectStr.IndexOf("for");
                                    subjectStr = subjectStr.Substring(0, idx_e).Replace("from", "").Trim();
                                }

                                if (subjectStr.Contains(" for "))
                                {
                                    idx_e = subjectStr.IndexOf(" for");
                                    subjectStr = subjectStr.Substring(0, idx_e).Trim();
                                }

                                if (!string.IsNullOrEmpty(subjectStr))
                                {
                                    string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                    query = query + "AND KEYWORD LIKE '%" + subjectStr + "%' ";
                                    query = query + "AND USE_YN = 'Y'";
                                    DataTable dt = DBMgr.GetDataTable(query);

                                    if (dt.Rows.Count > 0)
                                    {
                                        ResultSubject(mailitem, dt);
                                    }
                                    else
                                    {
                                        nonSubject = mailSubject;
                                        nonTo = mailHost;

                                        DBInsert_Non(mailHost, mailSubject);
                                    }
                                }
                            }
                            else if (mailHost.Contains("@vships.com") || mailHost.Contains("@marcassupplychain.com"))
                            {
                                string subjectStr = string.Empty;

                                int idx_s = 0;
                                int idx_e = 0;

                                if (mailSubject.Contains(">") && mailSubject.Contains("-"))
                                {
                                    idx_s = mailSubject.IndexOf(">");


                                    subjectStr = mailSubject.Substring(idx_s, mailSubject.Length - idx_s);

                                    idx_e = subjectStr.IndexOf("-");
                                    subjectStr = subjectStr.Substring(0, idx_e).Replace(">", "").Trim();
                                }
                                else if (mailSubject.Contains("-") && mailSubject.Contains("RFQ"))
                                {
                                    idx_e = mailSubject.IndexOf("-");

                                    subjectStr = mailSubject.Substring(0, idx_e).Trim();
                                }
                                else if (mailSubject.StartsWith("V.Ships"))
                                {
                                    idx_e = mailSubject.IndexOf(" - ");

                                    subjectStr = mailSubject.Substring(0, idx_e).Trim();
                                }

                                subjectStr = subjectStr.Replace("RE", "").Replace(":", "").Trim();

                                if (!string.IsNullOrEmpty(subjectStr))
                                {
                                    string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                    query = query + "AND KEYWORD LIKE '%" + subjectStr + "%' ";
                                    query = query + "AND USE_YN = 'Y'";
                                    DataTable dt = DBMgr.GetDataTable(query);

                                    if (dt.Rows.Count > 0)
                                    {
                                        ResultSubject(mailitem, dt);
                                    }
                                    else
                                    {
                                        nonSubject = mailSubject;
                                        nonTo = mailHost;

                                        DBInsert_Non(nonTo, mailSubject);
                                    }
                                }
                            }
                            else if (mailSubject.Contains("발주서 접수 확인 되었습니다."))
                            {
                                ReferenceParseWooYang(mailitem);

                                if (!string.IsNullOrEmpty(returnFileNo))
                                {
                                    DBSelect();

                                    ReturnToSend(mailitem);
                                }
                                else
                                {
                                    nonSubject = mailSubject;
                                    nonTo = mailHost;

                                    DBInsert_Non(nonTo, mailSubject);
                                }
                            }
                            // V.Ships 파싱용
                            else if (mailSubject.ToLower().Contains("quote successfully saved!"))
                            {
                                ReferenceParseVship(mailitem);

                                if (!string.IsNullOrEmpty(returnFileNo))
                                {

                                    DBSelect();

                                    ReturnToSend(mailitem);
                                }
                            }
                            else if (mailHost.Contains("@flynn-refrigeration.com"))
                            {
                                ReferenceParseFLYNN(mailitem);

                                if (!string.IsNullOrEmpty(returnFileNo))
                                {
                                    DBSelect();

                                    ReturnToSend(mailitem);
                                }
                            }
                            // 견적등록 해야 할 것들
                            else if (mailSubject.Replace("긴급문의건-", "").Replace("*견적 부탁드리겠습니다.*", "").Replace("*코드확인부탁드리겠습니다*", "").Replace("[견적회신]", "").Replace("RE: RE:", "RE:").Replace("유선문의건-", "").Replace("[SPAM]", "").Replace("[RE]", "RE: ").ToUpper().Trim().StartsWith("RE: DINTEC - INQUIRY("))
                            {
                                if (mailSubject.Contains("긴급문의건-"))
                                    mailSubject = mailSubject.Replace("긴급문의건-", "").Trim();

                                if (mailSubject.Contains("*견적 부탁드리겠습니다.*"))
                                    mailSubject = mailSubject.Replace("*견적 부탁드리겠습니다.*", "").Trim();

                                if (mailSubject.Contains("*코드확인부탁드리겠습니다*"))
                                    mailSubject = mailSubject.Replace("*코드확인부탁드리겠습니다*", "").Trim();

                                if (mailSubject.Contains("[견적회신]"))
                                    mailSubject = mailSubject.Replace("[견적회신]", "").Trim();

                                if (mailSubject.Contains("RE: RE:"))
                                    mailSubject = mailSubject.Replace("RE: RE:", "RE: ").Trim();

                                if (mailSubject.Contains("유선문의건-"))
                                    mailSubject = mailSubject.Replace("유선문의건-", "").Trim();

                                if (mailSubject.Contains("[SPAM]"))
                                    mailSubject = mailSubject.Replace("[SPAM]", "").Trim();

                                if (mailSubject.Contains("[RE]"))
                                    mailSubject = mailSubject.Replace("[RE]", "RE: ").Trim();

                                int idx_s = mailSubject.IndexOf("INQUIRY(");

                                // 파일번호
                                fileNoStr = mailSubject.Substring(idx_s, 18).Trim();
                                fileNoStr = fileNoStr.Replace("INQUIRY(", "").Trim();

                                int idx_e = mailSubject.IndexOf("_");

                                // 메일 제목이 변형이 되었는지 판단, "_" 유무
                                if (idx_e == -1)
                                    fileSuplStr = mailSubject.Substring(mailSubject.Length - 5, 5).Trim();
                                else
                                    fileSuplStr = mailSubject.Substring(idx_e, mailSubject.Length - idx_e);

                                idx_s = -1;
                                idx_s = mailSubject.IndexOf("IMO");

                                string imoStr = string.Empty;

                                if (idx_s != -1)
                                    imoStr = mailSubject.Substring(idx_s, 10).Trim();

                                if (!string.IsNullOrEmpty(imoStr) && imoStr.Contains(fileSuplStr))
                                {
                                    textBox1.AppendText("메일 제목을 확인하세요.");
                                }
                                else
                                {

                                    if (string.IsNullOrEmpty(fileSuplStr))
                                    {
                                        fileSuplStr = mailSubject.Substring(mailSubject.Length - 5, 5);
                                    }

                                    if (fileSuplStr.Length > 10)
                                    {
                                        fileSuplStr = fileSuplStr.Substring(1, 5).Trim();
                                    }
                                    else if (fileSuplStr.Length == 6)
                                    {
                                        fileSuplStr = fileSuplStr.Replace("_", "").Trim();
                                    }

                                    if (fileSuplStr.Length != 5)
                                        return;

                                    // 거래처코드
                                    //fileSuplStr = mailSubject.Substring(mailSubject.Length - 5, 5);

                                    AttSave(mailitem, moveFolder);
                                }

                            }
                            else if (mailSubject.ToUpper().StartsWith("[RE: DINTEC - INQUIRY("))
                            {
                                int idx_s = mailSubject.IndexOf("INQUIRY(");

                                // 파일번호
                                fileNoStr = mailSubject.Substring(idx_s, 18).Trim();
                                fileNoStr = fileNoStr.Replace("INQUIRY(", "").Trim();

                                int idx_e = mailSubject.IndexOf("_");

                                fileSuplStr = mailSubject.Substring(idx_e, mailSubject.Length - idx_e);

                                if (fileSuplStr.Length > 10)
                                {
                                    fileSuplStr = fileSuplStr.Substring(1, 5).Trim();
                                }
                                else if (fileSuplStr.Length == 6)
                                {
                                    fileSuplStr = fileSuplStr.Replace("_", "").Trim();
                                }

                                if (fileSuplStr.Length != 5)
                                    return;

                                // 거래처코드
                                //fileSuplStr = mailSubject.Substring(mailSubject.Length - 5, 5);

                                AttSave(mailitem, moveFolder);
                            }
                            else if (mailSubject.ToUpper().StartsWith("FW: DINTEC - INQUIRY(") || mailSubject.ToUpper().StartsWith("FWD: DINTEC - INQUIRY("))
                            {
                                int idx_s = mailSubject.IndexOf("INQUIRY(");

                                // 파일번호
                                fileNoStr = mailSubject.Substring(idx_s, 18).Trim();
                                fileNoStr = fileNoStr.Replace("INQUIRY(", "").Trim();

                                int idx_e = mailSubject.IndexOf("_");

                                fileSuplStr = mailSubject.Substring(idx_e, mailSubject.Length - idx_e);

                                if (fileSuplStr.Length > 10)
                                {
                                    fileSuplStr = fileSuplStr.Substring(1, 5).Trim();
                                }
                                else if (fileSuplStr.Length == 6)
                                {
                                    fileSuplStr = fileSuplStr.Replace("_", "").Trim();
                                }

                                if (fileSuplStr.Length != 5)
                                    return;

                                // 거래처코드
                                //fileSuplStr = mailSubject.Substring(mailSubject.Length - 5, 5);

                                AttSave(mailitem, moveFolder);
                            }
                            else if (mailSubject.StartsWith("New RFQ from "))
                            {
                                string[] subjStrSpl = mailSubject.Split('|');
                                string subjStr = string.Empty;
                                if (subjStrSpl.Length == 3)
                                {
                                    subjStr = subjStrSpl[0].ToString().Replace("New RFQ from", "").Trim();
                                }

                                if (!string.IsNullOrEmpty(subjStr))
                                {
                                    string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                    query = query + "AND KEYWORD LIKE '%" + subjStr + "%' ";
                                    query = query + "AND USE_YN = 'Y'";
                                    DataTable dt = DBMgr.GetDataTable(query);

                                    if (dt.Rows.Count > 0)
                                    {
                                        resultTo = dt.Rows[0]["MAIL_TO"].ToString();
                                        resultCC = dt.Rows[0]["MAIL_CC"].ToString();
                                        resultBCC = dt.Rows[0]["MAIL_BCC"].ToString();

                                        resultCate1 = dt.Rows[0]["CATEGORY_1"].ToString();
                                        resultCate2 = dt.Rows[0]["CATEGORY_2"].ToString();
                                        resultCate3 = dt.Rows[0]["CATEGORY_3"].ToString();
                                        resultCate4 = dt.Rows[0]["CATEGORY_4"].ToString();

                                        resultKeyword = dt.Rows[0]["KEYWORD"].ToString();
                                        resultHostMail = dt.Rows[0]["HOST_MAIL"].ToString();
                                        resultMoveMail = dt.Rows[0]["MOVE_MAIL"].ToString();
                                        resultRmk = dt.Rows[0]["DC_RMK"].ToString();

                                        if (resultCate1.Equals("Subject"))
                                            sendMailSys(mailitem, resultTo, resultCC, resultBCC);
                                    }
                                    else
                                    {
                                        nonSubject = mailSubject;
                                        nonTo = mailHost;

                                        DBInsert_Non(nonTo, mailSubject);
                                    }
                                }
                            }
                            else if (mailSubject.StartsWith("[우양선기]") || mailSubject.StartsWith("견적서"))
                            {
                                int idx_s = mailSubject.IndexOf("견적서/");

                                // 파일번호
                                fileNoStr = mailSubject.Substring(idx_s, 14).Trim();
                                fileNoStr = fileNoStr.Replace("견적서/", "").Trim();

                                // 거래처코드
                                fileSuplStr = "07249";

                                AttSave(mailitem, moveFolder);
                            }
                            // BSM
                            else if (mailHost.Contains("@bs-shipmanagement.com"))
                            {
                                string subjectStr = string.Empty;

                                int idx_s = 0;
                                int idx_e = 0;

                                idx_s = mailSubject.IndexOf("BSM");
                                idx_e = mailSubject.IndexOf(")");

                                if (idx_s != 0 && idx_e != 0)
                                {
                                    subjectStr = mailSubject.Substring(idx_s, idx_e - idx_s + 1);
                                }

                                //subjectStr = subjectStr.Replace("RE", "").Replace(":", "").Trim();
                                if (!string.IsNullOrEmpty(subjectStr))
                                {
                                    string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                    query = query + "AND KEYWORD LIKE '%" + subjectStr + "%' ";
                                    query = query + "AND USE_YN = 'Y'";
                                    DataTable dt = DBMgr.GetDataTable(query);

                                    if (dt.Rows.Count > 0)
                                    {
                                        resultTo = dt.Rows[0]["MAIL_TO"].ToString();
                                        resultCC = dt.Rows[0]["MAIL_CC"].ToString();
                                        resultBCC = dt.Rows[0]["MAIL_BCC"].ToString();

                                        resultCate1 = dt.Rows[0]["CATEGORY_1"].ToString();
                                        resultCate2 = dt.Rows[0]["CATEGORY_2"].ToString();
                                        resultCate3 = dt.Rows[0]["CATEGORY_3"].ToString();
                                        resultCate4 = dt.Rows[0]["CATEGORY_4"].ToString();

                                        resultKeyword = dt.Rows[0]["KEYWORD"].ToString();
                                        resultHostMail = dt.Rows[0]["HOST_MAIL"].ToString();
                                        resultMoveMail = dt.Rows[0]["MOVE_MAIL"].ToString();
                                        resultRmk = dt.Rows[0]["DC_RMK"].ToString();

                                        if (resultCate1.Equals("Subject") && resultCate4.Equals("BSM"))
                                            sendMailSys(mailitem, resultTo, resultCC, resultBCC);
                                    }
                                    else
                                    {
                                        nonSubject = mailSubject;
                                        nonTo = mailHost;

                                        DBInsert_Non(nonTo, mailSubject);
                                    }
                                }
                            }
                            else
                            {
                                string mailAdd = string.Empty;

                                int idx_s = mailHost.IndexOf("@");

                                mailAdd = mailHost.Substring(idx_s, mailHost.Length - idx_s).Trim();

                                if (!string.IsNullOrEmpty(mailAdd))
                                {
                                    string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE 1=1 ";
                                    query = query + "AND KEYWORD LIKE '%" + mailAdd + "%' ";
                                    query = query + "AND USE_YN = 'Y'";
                                    DataTable dt = DBMgr.GetDataTable(query);

                                    if (dt.Rows.Count > 0)
                                    {
                                        ResultMail(mailitem, dt);
                                    }
                                    else
                                    {
                                        nonSubject = mailSubject;
                                        nonTo = mailHost;

                                        DBInsert_Non(nonTo, nonSubject);
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                //MailSave();
            }
        }
        #endregion 동진

        #region Send Mail
        private void sendMail(Microsoft.Office.Interop.Outlook.MailItem mail, string mailTo, string mailCC, string mailBCC)
        {
            mail.To = mailTo; //my fixed email adress 
            mail.CC = mailCC;   //removing any carboncopy users
            mail.BCC = mailBCC; //removing any blind carbon copy users
            mail.Send();
        }

        private void sendMailSys(Outlook.MailItem mailitem, string sendTo, string sendCC, string sendBCC)
        {
            try
            {
                FileLocalSave(mailitem);
                Parse(sendTo);


                //TEST
                //sendTo = "sangwon.ha@dintec.co.kr";
                //sendCC = "";
                //sendBCC = "";

                textBox1.AppendText("\r\n" + itemCount + ": " + mailitem.Subject.ToString());
                textBox2.AppendText("\r\n" + itemCount + ": " + "최근완료: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " / " + sendTo + " / " + sendCC);

                sendMail(mailitem, sendTo, sendCC, sendBCC);

                //itemCount -= 1;

                textBox1.Select(textBox1.Text.Length, 0);
                textBox1.ScrollToCaret();

                textBox2.Select(textBox2.Text.Length, 0);
                textBox2.ScrollToCaret();

                if (textBox1.Text.Length > 10000)
                {
                    textBox1.Text = "";
                }

                if (textBox2.Text.Length > 10000)
                {
                    textBox2.Text = "";
                }


                imoNumber = string.Empty;
                vessel = string.Empty;
                partner = string.Empty;
                tnid = string.Empty;
                reference = string.Empty;

                fileName = "";
                mailFileName = string.Empty;

                deliveryDt = string.Empty;

                returnFileNo = string.Empty;
                returnToMail = string.Empty;

                origSender = string.Empty;
                origCC = string.Empty;
                origBCC = string.Empty;
                origSize = string.Empty;
                origSubject = string.Empty;
                origTo = string.Empty;
                sendTo = string.Empty;
            }
            catch (Exception e)
            {

            }
        }

        // 이동 및 삭제 디비 저장
        private void endMail(Outlook.MailItem mailitem)
        {
            FileLocalSave(mailitem);
            Parse(sendTo);

            //TEST
            //sendTo = "sangwon.ha@dintec.co.kr";
            //sendCC = "";
            //sendBCC = "";

            textBox1.AppendText("\r\n" + itemCount + ": " + mailitem.Subject.ToString());
            textBox2.AppendText("\r\n" + itemCount + ": " + "최근완료: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " / " + "견적등록");


            //itemCount -= 1;

            textBox1.Select(textBox1.Text.Length, 0);
            textBox1.ScrollToCaret();

            textBox2.Select(textBox2.Text.Length, 0);
            textBox2.ScrollToCaret();

            if (textBox1.Text.Length > 10000)
            {
                textBox1.Text = "";
            }

            if (textBox2.Text.Length > 10000)
            {
                textBox2.Text = "";
            }


            imoNumber = string.Empty;
            vessel = string.Empty;
            partner = string.Empty;
            tnid = string.Empty;
            reference = string.Empty;

            fileName = "";
            mailFileName = string.Empty;

            deliveryDt = string.Empty;

            returnFileNo = string.Empty;
            returnToMail = string.Empty;

            origSender = string.Empty;
            origCC = string.Empty;
            origBCC = string.Empty;
            origSize = string.Empty;
            origSubject = string.Empty;
            origTo = string.Empty;
            sendTo = string.Empty;
        }
        #endregion Send Mail

        #region ShipServ Parsing
        public void Parse(string _sendTo)
        {
            try
            {
                MapiMessage msg = MapiMessage.FromFile(fileName);

                string mailBodyStr = string.Empty;

                if (!string.IsNullOrEmpty(msg.Body))
                {
                    mailBodyStr = msg.Body;

                    origCC = msg.DisplayCc;
                    origBCC = msg.DisplayBcc;

                    if (origCC == null)
                        origCC = string.Empty;

                    if (origBCC == null)
                        origBCC = string.Empty;

                    origSender = msg.SenderEmailAddress;
                    origSubject = msg.Subject;
                    origTo = msg.DisplayTo;

                    if (origSender == null)
                        origSender = string.Empty;

                    if (origSubject == null)
                        origSubject = string.Empty;

                    if (origTo == null)
                        origTo = string.Empty;

                    deliveryDt = msg.DeliveryTime.ToString("yyyyMMddHHmmss");


                    if (origSender.Equals("info@shipserv.com") && !msg.Subject.ToString().StartsWith("VARIANCE IN"))
                    {
                        // reference
                        int idx_lts = mailBodyStr.IndexOf("RFQ Ref:");
                        int idx_lte = mailBodyStr.IndexOf("Subject:");

                        if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
                        {
                            reference = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("RFQ Ref:", "").Trim();
                        }

                        idx_lts = -1; idx_lte = -1;


                        // vessel
                        idx_lts = mailBodyStr.IndexOf("Vessel:");
                        idx_lte = mailBodyStr.IndexOf("Vessel No.:");

                        if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
                        {
                            vessel = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Vessel:", "").Trim();
                        }



                        // imonumber
                        idx_lts = -1; idx_lte = -1;

                        idx_lts = mailBodyStr.IndexOf("Vessel No.:");
                        idx_lte = mailBodyStr.IndexOf("Class:");

                        if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
                        {
                            imoNumber = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Vessel No.:", "").Trim();
                        }


                        string mailBodyStr2 = string.Empty;

                        // partner 하기전에 전초단계
                        idx_lts = -1; idx_lte = -1;

                        idx_lts = mailBodyStr.IndexOf("From:");
                        idx_lte = mailBodyStr.IndexOf("Request for Quote Details:");

                        if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
                        {
                            mailBodyStr2 = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Vessel No.:", "").Trim();
                        }



                        // partner
                        idx_lts = -1; idx_lte = -1;

                        idx_lts = mailBodyStr2.IndexOf("From:");
                        idx_lte = mailBodyStr2.IndexOf("(TNID");

                        if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1) && !string.IsNullOrEmpty(mailBodyStr2))
                        {
                            partner = mailBodyStr2.Substring(idx_lts, idx_lte - idx_lts).Replace("From:", "").Trim();
                        }

                        // tnid
                        idx_lts = -1; idx_lte = -1;

                        idx_lts = mailBodyStr2.IndexOf("(TNID:");
                        idx_lte = mailBodyStr2.IndexOf(") \r\n-");

                        if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1) && !string.IsNullOrEmpty(mailBodyStr2))
                        {
                            tnid = mailBodyStr2.Substring(idx_lts, idx_lte - idx_lts).Replace("(TNID:", "").Trim();
                        }

                    }
                    else if (msg.Subject.ToString().StartsWith("VARIANCE IN"))
                    {
                        int idx_s = mailBodyStr.IndexOf("(TNID:");

                        tnid = mailBodyStr.Substring(idx_s, 12).Replace("(TNID: ", "").Trim();


                        imoNumber = string.Empty;
                        vessel = string.Empty;
                        partner = string.Empty;
                        reference = string.Empty;
                    }
                    else
                    {
                        imoNumber = string.Empty;
                        vessel = string.Empty;
                        partner = string.Empty;
                        tnid = string.Empty;
                        reference = string.Empty;
                    }

                    DBInsert(_sendTo);
                }
                else
                {
                    imoNumber = string.Empty;
                    vessel = string.Empty;
                    partner = string.Empty;
                    tnid = string.Empty;
                    reference = string.Empty;
                }
            }
            catch (Exception e)
            {
            }
        }
        #endregion ShipServ Parsing

        #region Reference Parsing
        // V.SHIPS
        public void ReferenceParseVship(Outlook.MailItem mailitem)
        {
            FileLocalSave(mailitem);

            MapiMessage msg = MapiMessage.FromFile(fileName);

            string mailBodyStr = msg.Body.ToString();

            int idx_s = mailBodyStr.IndexOf("Reference");

            returnFileNo = mailBodyStr.Substring(idx_s, 20).Replace("Reference", "").Trim();
        }

        // SHIPSERV
        public void ReferenceParseShipServ(Outlook.MailItem mailitem)
        {
            FileLocalSave(mailitem);

            MapiMessage msg = MapiMessage.FromFile(fileName);

            string mailBodyStr = msg.Body.ToString();

            int idx_s = mailBodyStr.IndexOf("QOT:");

            returnFileNo = mailBodyStr.Substring(idx_s, 20).Replace("QOT", "").Replace(":", "").Replace(".", "").Trim();
        }

        // WOOYANG
        public void ReferenceParseWooYang(Outlook.MailItem mailitem)
        {
            FileLocalSave(mailitem);

            MapiMessage msg = MapiMessage.FromFile(fileName);

            string mailSubject = msg.Subject.ToString();

            int idx_e = mailSubject.IndexOf("/");

            returnFileNo = mailSubject.Substring(0, idx_e).Trim();
        }

        // FLYNN
        public void ReferenceParseFLYNN(Outlook.MailItem mailitem)
        {
            FileLocalSave(mailitem);

            MapiMessage msg = MapiMessage.FromFile(fileName);

            string mailBodyStr = msg.Body.ToString();

            int idx_s = mailBodyStr.IndexOf("Your Reference:");

            returnFileNo = mailBodyStr.Substring(idx_s, 26).Replace("Your Reference", "").Replace(":", "").Replace(".", "").Trim();
        }

        #endregion Reference Parsing

        #region 데이터베이스

        private void GetDBSearch()
        {
            string query = "SELECT * FROM CZ_MAIL_SERVICE_REG WHERE USE_YN = 'Y'";

            dbDatatb = DBMgr.GetDataTable(query);
        }

        private void DBSelect()
        {
            string query = "SELECT NO_EMAIL FROM MA_EMP WHERE CD_COMPANY = 'K100' AND NO_EMP IN (SELECT NO_EMP FROM CZ_SA_QTNH WHERE NO_FILE = '" + returnFileNo + "')";

            DataTable dt = DBMgr.GetDataTable(query);

            returnToMail = dt.Rows[0][0].ToString();
        }

        private void DBInsert(string _sendTo)
        {
            string connectStr = "server = 192.168.1.143; uid = NEOE; pwd = NEOE; database = NEOE";

            SqlConnection sqlConn = new SqlConnection(connectStr);
            SqlCommand sqlComm = new SqlCommand();
            sqlComm.Connection = sqlConn;
            sqlComm.CommandText = "INSERT INTO CZ_MAIL_SERVICE (NO_IMO, NM_VESSEL, NM_PARTNER, CD_TRADE, REFERENCE, ORIG_TO, ORIG_CC, ORIG_BCC, ORIG_SENDER, ORIG_SUBJECT, SEND_TO, DTS_DELIVERY, DTS_INSERT) VALUES(@NO_IMO, @NM_VESSEL, @NM_PARTNER, @CD_TRADE, @REFERENCE, @ORIG_TO, @ORIG_CC, @ORIG_BCC, @ORIG_SENDER, @ORIG_SUBJECT, @SEND_TO, @DTS_DELIVERY, @DTS_INSERT)";
            sqlComm.Parameters.AddWithValue("@NO_IMO", imoNumber);
            sqlComm.Parameters.AddWithValue("@NM_VESSEL", vessel);
            sqlComm.Parameters.AddWithValue("@NM_PARTNER", partner);
            sqlComm.Parameters.AddWithValue("@CD_TRADE", tnid);
            sqlComm.Parameters.AddWithValue("@REFERENCE", reference);
            sqlComm.Parameters.AddWithValue("@ORIG_TO", origTo);
            sqlComm.Parameters.AddWithValue("@ORIG_CC", origCC);
            sqlComm.Parameters.AddWithValue("@ORIG_BCC", origBCC);
            sqlComm.Parameters.AddWithValue("@ORIG_SENDER", origSender);
            sqlComm.Parameters.AddWithValue("@ORIG_SUBJECT", origSubject);
            sqlComm.Parameters.AddWithValue("@SEND_TO", _sendTo);
            sqlComm.Parameters.AddWithValue("@DTS_DELIVERY", deliveryDt);
            sqlComm.Parameters.AddWithValue("@DTS_INSERT", DateTime.Now.ToString("yyyyMMddHHmmss"));

            sqlConn.Open();
            sqlComm.ExecuteNonQuery();
            sqlConn.Close();

            imoNumber = string.Empty;
            vessel = string.Empty;
            partner = string.Empty;
            tnid = string.Empty;
            reference = string.Empty;
            deliveryDt = string.Empty;

            returnFileNo = string.Empty;
            returnToMail = string.Empty;


            // 로컬 파일 삭제
            FileInfo fileDel = new FileInfo(fileName);
            if (fileDel.Exists)
            {
                fileDel.Delete();
            }
        }


        private void DBInsert_Non(string _nonTo, string _nonSubject)
        {

            // 잠시 주석처리
            //_nonSubject = _nonSubject.Replace("'", "").Trim();

            //string connectStr = "server = 192.168.1.143; uid = NEOE; pwd = NEOE; database = NEOE";

            //string query = "SELECT * FROM CZ_MAIL_SERVICE_NONREG WHERE 1=1 ";
            //query = query + "AND KEYWORD = '" + _nonSubject + "' ";
            //query = query + "AND MAIL_TO = '" + _nonTo + "'";
            //DataTable dt = DBMgr.GetDataTable(query);


            //if (dt.Rows.Count <= 0)
            //{
            //    SqlConnection sqlConn = new SqlConnection(connectStr);
            //    SqlCommand sqlComm = new SqlCommand();
            //    sqlComm.Connection = sqlConn;
            //    sqlComm.CommandText = "INSERT INTO CZ_MAIL_SERVICE_NONREG (MAIL_TO, KEYWORD, DTS_INSERT) VALUES(@MAIL_TO, @KEYWORD, @DTS_INSERT)";
            //    sqlComm.Parameters.AddWithValue("@MAIL_TO", _nonTo);
            //    sqlComm.Parameters.AddWithValue("@KEYWORD", _nonSubject);
            //    sqlComm.Parameters.AddWithValue("@DTS_INSERT", DateTime.Now.ToString("yyyyMMddHHmmss"));

            //    sqlConn.Open();
            //    sqlComm.ExecuteNonQuery();
            //    sqlConn.Close();
            //}
            //nonTo = string.Empty;
            //nonSubject = string.Empty;

        }

        private void ProcedureInsert(string fileNo, string realName)
        {
            SqlConnection sqlConn;

            try
            {
                //string connectStr = "server = 192.168.1.143; uid = NEOE; pwd = NEOE; database = NEOE";

                string query = "SELECT * FROM CZ_MA_WORKFLOWH WHERE 1=1 ";
                query = query + "AND NO_KEY = '" + fileNo + "' ";
                query = query + "AND TP_STEP = '01'";
                DataTable dt = DBMgr.GetDataTable(query);



                query = "SELECT * FROM CZ_MA_WORKFLOWH WHERE 1=1 ";
                query = query + "AND NO_KEY = '" + fileNo + "' ";
                query = query + "AND TP_STEP = '04'";
                DataTable dt2 = DBMgr.GetDataTable(query);

                if (dt != null)
                {
                    string id_sales = dt.Rows[0]["ID_SALES"].ToString();
                    string id_typist = dt.Rows[0]["ID_TYPIST"].ToString();

                    sqlConn = new SqlConnection("server = 192.168.1.143; uid = NEOE; pwd = NEOE; database = NEOE");
                    sqlConn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = sqlConn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = null;

                    if (dt2 == null)
                    {
                        cmd.CommandText = "SP_CZ_SA_INQ_REGH_I";

                        cmd.Parameters.Add("P_CD_COMPANY", SqlDbType.NVarChar, 50);
                        cmd.Parameters.Add("P_TP_STEP", SqlDbType.NVarChar, 50);
                        cmd.Parameters.Add("P_NO_KEY", SqlDbType.NVarChar, 50);
                        cmd.Parameters.Add("P_TP_SALES", SqlDbType.NVarChar, 50);
                        cmd.Parameters.Add("P_ID_SALES", SqlDbType.NVarChar, 50);
                        cmd.Parameters.Add("P_ID_TYPIST", SqlDbType.NVarChar, 50);
                        cmd.Parameters.Add("P_ID_PUR", SqlDbType.NVarChar, 50);
                        cmd.Parameters.Add("P_ID_LOG", SqlDbType.NVarChar, 50);
                        cmd.Parameters.Add("P_DC_RMK", SqlDbType.NVarChar, 50);
                        cmd.Parameters.Add("P_ID_INSERT", SqlDbType.NVarChar, 50);

                        cmd.Parameters["P_CD_COMPANY"].Value = CD_COMPANY;
                        cmd.Parameters["P_TP_STEP"].Value = "04";
                        cmd.Parameters["P_NO_KEY"].Value = fileNo;
                        cmd.Parameters["P_TP_SALES"].Value = "";
                        cmd.Parameters["P_ID_SALES"].Value = id_sales;
                        cmd.Parameters["P_ID_TYPIST"].Value = id_typist;
                        cmd.Parameters["P_ID_PUR"].Value = "";
                        cmd.Parameters["P_ID_LOG"].Value = "";
                        cmd.Parameters["P_DC_RMK"].Value = "";
                        cmd.Parameters["P_ID_INSERT"].Value = NO_EMP;

                        reader = cmd.ExecuteReader();
                        reader.Close();
                    }

                    cmd.CommandText = "SP_CZ_SA_INQ_REGL_I";

                    cmd.Parameters.Add("P_CD_COMPANY", SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("P_TP_STEP", SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("P_NO_KEY", SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("P_NM_FILE", SqlDbType.NVarChar, 9000);
                    cmd.Parameters.Add("P_NM_FILE_REAL", SqlDbType.NVarChar, 9000);
                    cmd.Parameters.Add("P_CD_SUPPLIER", SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("P_YN_PARSING", SqlDbType.NVarChar, 50);
                    cmd.Parameters.Add("P_ID_INSERT", SqlDbType.NVarChar, 50);

                    cmd.Parameters["P_CD_COMPANY"].Value = CD_COMPANY;
                    cmd.Parameters["P_TP_STEP"].Value = "04";
                    cmd.Parameters["P_NO_KEY"].Value = fileNo;
                    cmd.Parameters["P_NM_FILE"].Value = fileNameStr + ".msg";
                    cmd.Parameters["P_NM_FILE_REAL"].Value = realName;
                    cmd.Parameters["P_CD_SUPPLIER"].Value = fileSuplStr;
                    cmd.Parameters["P_YN_PARSING"].Value = YN_PARSING;
                    cmd.Parameters["P_ID_INSERT"].Value = NO_EMP;

                    reader = cmd.ExecuteReader();
                    reader.Close();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                //sqlConn.Close();
            }

            //SpInfo si = new SpInfo();
            ////si.DataValue = dtH;
            //si.CompanyID = CD_COMPANY;
            //si.UserID = Global.MainFrame.LoginInfo.UserID;
            //si.SpNameInsert = "SP_CZ_SA_INQ_REGH_I";
            //si.SpParamsInsert = new string[] { CD_COMPANY,
            //                                       "04",		
            //                                       fileNoStr,
            //                                       "TP_SALES",		
            //                                       "ID_SALES",		
            //                                       "ID_TYPIST",	
            //                                       "ID_PUR",		
            //                                       "ID_LOG",		
            //                                       "",		
            //                                       NO_EMP };



            ////si.DataValue = dtL;
            //si.CompanyID = CD_COMPANY;
            //si.UserID = Global.MainFrame.LoginInfo.UserID;
            //si.SpNameInsert = "SP_CZ_SA_INQ_REGL_I";
            //si.SpParamsInsert = new string[] { "CD_COMPANY",	
            //                                       "TP_STEP",			
            //                                       "NO_KEY",
            //                                       "NM_FILE",
            //                                       "NM_FILE_REAL",
            //                                       "CD_SUPPLIER",
            //                                       "YN_PARSING",
            //                                       "ID_INSERT" };
        }

        #endregion 데이터베이스

        #region 메소드 20190130
        private void ReturnToSend(Outlook.MailItem mailitem)
        {
            if (!string.IsNullOrEmpty(returnToMail))
            {
                string _To = returnToMail;
                string _BCC = "service2@dintec.co.kr";
                string _CC = "";

                // 로컬 파일 삭제
                FileInfo fileDel = new FileInfo(fileName);
                if (fileDel.Exists)
                {
                    fileDel.Delete();
                }

                sendMailSys(mailitem, _To, _CC, _BCC);
            }
        }

        private void ResultEWS(Outlook.MailItem mailitem, DataTable dt)
        {
            resultTo = dt.Rows[0]["MAIL_TO"].ToString();
            resultCC = dt.Rows[0]["MAIL_CC"].ToString();
            resultBCC = dt.Rows[0]["MAIL_BCC"].ToString();

            resultCate1 = dt.Rows[0]["CATEGORY_1"].ToString();
            resultCate2 = dt.Rows[0]["CATEGORY_2"].ToString();
            resultCate3 = dt.Rows[0]["CATEGORY_3"].ToString();
            resultCate4 = dt.Rows[0]["CATEGORY_4"].ToString();

            resultKeyword = dt.Rows[0]["KEYWORD"].ToString();
            resultHostMail = dt.Rows[0]["HOST_MAIL"].ToString();
            resultMoveMail = dt.Rows[0]["MOVE_MAIL"].ToString();
            resultRmk = dt.Rows[0]["DC_RMK"].ToString();

            if (resultCate1.Equals("EWS"))
                sendMailSys(mailitem, resultTo, resultCC, resultBCC);
        }

        private void ResultMail(Outlook.MailItem mailitem, DataTable dt)
        {
            resultTo = dt.Rows[0]["MAIL_TO"].ToString();
            resultCC = dt.Rows[0]["MAIL_CC"].ToString();
            resultBCC = dt.Rows[0]["MAIL_BCC"].ToString();

            resultCate1 = dt.Rows[0]["CATEGORY_1"].ToString();
            resultCate2 = dt.Rows[0]["CATEGORY_2"].ToString();
            resultCate3 = dt.Rows[0]["CATEGORY_3"].ToString();
            resultCate4 = dt.Rows[0]["CATEGORY_4"].ToString();

            resultKeyword = dt.Rows[0]["KEYWORD"].ToString();
            resultHostMail = dt.Rows[0]["HOST_MAIL"].ToString();
            resultMoveMail = dt.Rows[0]["MOVE_MAIL"].ToString();
            resultRmk = dt.Rows[0]["DC_RMK"].ToString();

            if (resultCate1.Equals("Mail"))
                sendMailSys(mailitem, resultTo, resultCC, resultBCC);
        }


        private void ResultSubject(Outlook.MailItem mailitem, DataTable dt)
        {
            resultTo = dt.Rows[0]["MAIL_TO"].ToString();
            resultCC = dt.Rows[0]["MAIL_CC"].ToString();
            resultBCC = dt.Rows[0]["MAIL_BCC"].ToString();

            resultCate1 = dt.Rows[0]["CATEGORY_1"].ToString();
            resultCate2 = dt.Rows[0]["CATEGORY_2"].ToString();
            resultCate3 = dt.Rows[0]["CATEGORY_3"].ToString();
            resultCate4 = dt.Rows[0]["CATEGORY_4"].ToString();

            resultKeyword = dt.Rows[0]["KEYWORD"].ToString();
            resultHostMail = dt.Rows[0]["HOST_MAIL"].ToString();
            resultMoveMail = dt.Rows[0]["MOVE_MAIL"].ToString();
            resultRmk = dt.Rows[0]["DC_RMK"].ToString();

            if (resultCate1.Equals("Subject"))
                sendMailSys(mailitem, resultTo, resultCC, resultBCC);
        }


        private void FileLocalSave(Outlook.MailItem mailitem)
        {
            fileNameStr = mailitem.Subject;
            string _fileStr = @"\savemsg\" + fileNameStr;
            _fileStr = "C:" + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";
            System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr);
            if (!fi.Exists)
            {
                mailitem.SaveAs(_fileStr, Outlook.OlSaveAsType.olMSG);
            }

            fileName = _fileStr;
        }

        private void AttSave(Outlook.MailItem mailitem, Outlook.MAPIFolder moveFolder)
        {
            // 최종파일이름
            fileNameStr = fileNoStr + "_" + fileSuplStr;

            // 둘중 하나라도 없으면 리턴
            if (string.IsNullOrEmpty(fileNoStr) || string.IsNullOrEmpty(fileSuplStr))
                return;

            // 파일 로컬 저장
            FileLocalSave(mailitem);

            try
            {
                if (QuotationFinder.IsPossible(CD_COMPANY, fileSuplStr, fileName) == true)
                    YN_PARSING = "Y";
                else
                    YN_PARSING = "N";
            }
            catch
            {
                YN_PARSING = "N";
            }

            try
            {
                textBox2.AppendText("\r\n" + fileName);
                NM_FILE_REAL = FileMgr.Upload_WF("K100", fileNoStr, fileName, false);

                // 기존 값이 있는지 없는지 체크
                // 체크 후에 값 가지고 오기 CZ_MA_WORKFLOWH > ID_SALES, ID_TYPIST 
                // CZ_MA_WORKFLOWL > 

                // 프로시저 저장
                ProcedureInsert(fileNoStr, NM_FILE_REAL);

                // 로컬 파일 삭제
                FileInfo fileDel = new FileInfo(fileName);
                if (fileDel.Exists)
                {
                    fileDel.Delete();
                }

                mailitem.Move(moveFolder);
            }
            catch (Exception e)
            {
                textBox2.AppendText(e.Message.ToString());
            }
        }

        private void AllClear()
        {
            resultTo = string.Empty;
            resultBCC = string.Empty;
            resultCC = string.Empty;

            resultCate1 = string.Empty;
            resultCate2 = string.Empty;
            resultCate3 = string.Empty;
            resultCate4 = string.Empty;

            resultKeyword = string.Empty;
            resultHostMail = string.Empty;
            resultMoveMail = string.Empty;
            resultRmk = string.Empty;

            nonTo = string.Empty;
            nonSubject = string.Empty;

            fileNoStr = string.Empty;
            fileSuplStr = string.Empty;
            fileNameStr = string.Empty;

            fileName = string.Empty;
            mailFileName = string.Empty;

            imoNumber = string.Empty;
            vessel = string.Empty;
            reference = string.Empty;
            partner = string.Empty;
            tnid = string.Empty;
            deliveryDt = string.Empty;

            returnFileNo = string.Empty;
            returnToMail = string.Empty;

            origSender = string.Empty;
            origCC = string.Empty;
            origBCC = string.Empty;
            origSize = string.Empty;
            origSubject = string.Empty;
            origTo = string.Empty;
            sendTo = string.Empty;

            YN_PARSING = string.Empty;
            NM_FILE_REAL = string.Empty;

            NO_MAIL = 0;
        }


        #endregion 메소드 20190130

        #region 메일 등록 화면
        #region 조회
        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                this._flex.Binding = this._biz.Search(new object[] {
                                                                      tbxKEY.Text,
                                                                      tbxCate1.Text,
                                                                      tbxCate2.Text,
                                                                      tbxCate3.Text,
                                                                      tbxCate4.Text
                                                                       });

                if (!_flex.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion 조회

        #region 추가
        protected override bool BeforeAdd()
        {
            return base.BeforeAdd();
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTB = _flex.DataTable;

                if (dtTB.Rows.Count >= 1)
                    NO_MAIL = Convert.ToInt16(dtTB.Compute("MAX(NO_MAIL)", "")) + 1;

                _flex.Rows.Add();
                _flex.Row = _flex.Rows.Count - 1;
                _flex["NO_MAIL"] = NO_MAIL;
                _flex["MAIL_BCC"] = "service2@dintec.co.kr";
                _flex["FOLDER"] = "Service";
                _flex["CATEGORY_1"] = "Mail";
                _flex["CATEGORY_4"] = "ETC";
                _flex["USE_YN"] = "Y";

                _flex.AddFinished();
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion 추가

        #region 저장
        protected override bool BeforeSave()
        {
            if (!base.BeforeSave()) return false;

            return true;
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            if (!BeforeSave()) return;
            if (!MsgAndSave(PageActionMode.Save)) return;

            ShowMessage(PageResultMode.SaveGood);
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify()) return false;

            try
            {
                _flex["ID_INSERT"] = Global.MainFrame.LoginInfo.UserName;
                _flex["ID_UPDATE"] = Global.MainFrame.LoginInfo.UserName;

                DataTable dt = this._flex.GetChanges();

                if (!_biz.Save(dt))
                {
                    return false;
                }

                this._flex.AcceptChanges();

                this._flex.Focus();
            }
            catch (Exception ex)
            {
                return Util.ShowMessage(Util.GetErrorMessage(ex.Message));
            }

            return true;
        }
        #endregion 저장

        #region 삭제
        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete())
                return false;

            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeDelete() || !this._flex.HasNormalRow)
                    return;

                this._flex.Rows.Remove(this._flex.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion 삭제
        #endregion 메일 등록 화면

        #region 이벤트

        void btn시작_Click(object sender, EventArgs e)
        {
            timer1.Start();
            textBox2.Text = "\r\n타이머 시작 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        void btn중지_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            textBox2.Text = "\r\n타이머 중지 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            // 폴더 내 모든 파일 삭제
            allDeleteF();
            // 모든 변수 초기화
            AllClear();

            MailSave();

            AllClear();

            MailSaveDongjin();
        }


        private void allDeleteF()
        {
            string srcPath = @"C:\savemsg\";

            if (System.IO.Directory.Exists(srcPath))
            {
                string[] files = System.IO.Directory.GetFiles(srcPath);

                foreach (string s in files)
                {
                    string fileName = System.IO.Path.GetFileName(s);
                    string deletefile = srcPath + fileName;

                    System.IO.File.Delete(deletefile);
                }
            }
        }


        #endregion 이벤트
    }
}

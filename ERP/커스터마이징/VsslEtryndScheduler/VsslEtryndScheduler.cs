using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Dintec;
using System.Xml;

namespace cz
{
    public partial class VesslEtryndScheduler : Form
    {
        #region 생성자 & 전역변수
        SqlConnection _sqlConnetion;
        SqlDataAdapter _sqlDataAdapter;
        SqlCommand _sqlCommand;
        string _connectionString;
        DataTable _dtHeader, _dtLine;

        public VesslEtryndScheduler()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.WriteLog("프로그램 시작");

            this.InitControl();
            this.InitEvent();
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                base.OnClosed(e);
                this.WriteLog("프로그램 종료");
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
            finally
            {
                timer.Stop();
                this.DB접속해제();
            }
        }

        private void InitControl()
        {
            this.txt회사코드.Text = "K100";
            this.txtDB.Text = "NEOE";
            this.txt서버IP.Text = "192.168.1.143";
            this.txt포트번호.Text = "1433";
            this.txt사용자.Text = "NEOE";
            this.txt패스워드.Text = "NEOE";

            this.timer.Interval = 1000 * 60 * 60; //1시간
            this.timer.Start();
        }

        private void InitEvent()
        {
            this.chk타이머.CheckedChanged += new EventHandler(this.chk타이머_CheckedChanged);
            this.timer.Tick += new EventHandler(this.btn운항정보가져오기_Click);
            this.btn운항정보가져오기.Click += new EventHandler(this.btn운항정보가져오기_Click);
        }
        #endregion

        #region 이벤트
        private void chk타이머_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk타이머.Checked == true)
                this.timer.Start();
            else
                this.timer.Stop();
        }

        private void btn운항정보가져오기_Click(object sender, EventArgs e)
        {
            DataTable dt;
            DataRow drHeader, drLine;
            Uri address;
            WebClient webClient = null;
            XmlDocument xmlDoc;
            XmlElement root;
            string serviceKey = "cMH4TGwG5uMJEK2xMwxAy2%2Fcpt5T1XdR04BQNkFxoygn6i2RELFeoLmlugqth9Kep0Clnn0ScWlC%2FQLVJk0RqA%3D%3D";
            string xml, resultCode, resultMsg;
            int totalCount, totalPage;

            try
            {
                this.txt로그.Text = string.Empty;

                #region DataTable
                this._dtHeader = this.CreateHeaderTable();
                this._dtLine = this.CreateLineTable();
                #endregion

                this.DB접속();

                dt = this.항만청정보();

                foreach (DataRow dr in dt.Rows)
                {
                    #region 토탈페이지수 계산
                    address = new Uri("http://apis.data.go.kr/1192000/VsslEtrynd2/Info?serviceKey=" + serviceKey +
                                      "&pageNo=1&startPage=1&numOfRows=1&pageSize=1&prtAgCd=" + dr["CD_SYSDEF"].ToString() +
                                      "&sde=" + DateTime.Today.ToString("yyyyMMdd") +
                                      "&ede=" + DateTime.Today.AddMonths(3).ToString("yyyyMMdd"));

                    webClient = new WebClient() { Encoding = Encoding.UTF8 };
                    xml = webClient.DownloadString(address);
                    xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xml);

                    root = xmlDoc.DocumentElement;

                    totalCount = Convert.ToInt32(root.GetElementsByTagName("totalCount")[0].InnerText);
                    totalPage = ((totalCount % 50) > 0 ? (totalCount / 50) + 1 : (totalCount / 50));

                    resultCode = root.GetElementsByTagName("resultCode")[0].InnerText;
                    resultMsg = root.GetElementsByTagName("resultMsg")[0].InnerText;
                    #endregion

                    this.WriteLog(resultCode + " : " + resultMsg);

                    if (resultCode != "00")
                        continue;
                    else
                        this.WriteLog(dr["NM_SYSDEF"].ToString() + " => TotalCount : " + totalCount.ToString() + " TotalPage : " + totalPage.ToString());
                    
                    for (int pageNo = 1; pageNo <= totalPage; pageNo++)
                    {
                        this.WriteLog("Page No : " + pageNo.ToString());

                        address = new Uri("http://apis.data.go.kr/1192000/VsslEtrynd2/Info?serviceKey=" + serviceKey +
                                          "&pageNo=" + pageNo.ToString() +
                                          "&startPage=" + pageNo.ToString() +
                                          "&numOfRows=50&pageSize=50&prtAgCd=" + dr["CD_SYSDEF"].ToString() +
                                          "&sde=" + DateTime.Today.ToString("yyyyMMdd") +
                                          "&ede=" + DateTime.Today.AddMonths(3).ToString("yyyyMMdd"));

                        webClient = new WebClient() { Encoding = Encoding.UTF8 };
                        xml = webClient.DownloadString(address);
                        xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xml);

                        root = xmlDoc.DocumentElement;

                        foreach (XmlElement header in root.GetElementsByTagName("item"))
                        {
                            drHeader = this._dtHeader.NewRow();

                            if (header["prtAgCd"] != null) drHeader["CD_PRTAG"] = header["prtAgCd"].InnerText;
                            if (header["prtAgNm"] != null) drHeader["NM_PRTAG"] = header["prtAgNm"].InnerText;
                            if (header["etryptYear"] != null) drHeader["DT_ETRYPT_YEAR"] = header["etryptYear"].InnerText;
                            if (header["etryptCo"] != null) drHeader["CNT_ETRYPT"] = header["etryptCo"].InnerText;
                            if (header["clsgn"] != null) drHeader["CLSGN"] = header["clsgn"].InnerText;
                            if (header["vsslNm"] != null) drHeader["NM_VSSL"] = header["vsslNm"].InnerText;
                            if (header["vsslNltyCd"] != null) drHeader["CD_VSSL_NLTY"] = header["vsslNltyCd"].InnerText;
                            if (header["vsslNltyNm"] != null) drHeader["NM_VSSL_NLTY"] = header["vsslNltyNm"].InnerText;
                            if (header["vsslKndNm"] != null) drHeader["NM_VSSL_KND"] = header["vsslKndNm"].InnerText;
                            if (header["etryptPurpsCd"] != null) drHeader["CD_ETRYPT_PURPS"] = header["etryptPurpsCd"].InnerText;
                            if (header["etryptPurpsNm"] != null) drHeader["NM_ETRYPT_PURPS"] = header["etryptPurpsNm"].InnerText;
                            if (header["frstDpmprtNatPrtCd"] != null) drHeader["CD_FRST_DPMPRT_NAT_PRT"] = header["frstDpmprtNatPrtCd"].InnerText;
                            if (header["frstDpmprtPrtNm"] != null) drHeader["NM_FRST_DPMPRT_PRT"] = header["frstDpmprtPrtNm"].InnerText;
                            if (header["prvsDpmprtNatPrtCd"] != null) drHeader["CD_PRVS_DPMPRT_NAT_PRT"] = header["prvsDpmprtNatPrtCd"].InnerText;
                            if (header["prvsDpmprtPrtNm"] != null) drHeader["NM_PRVS_DPMPRT_PRT"] = header["prvsDpmprtPrtNm"].InnerText;
                            if (header["nxlnptNatPrtCd"] != null) drHeader["CD_NX_INPT_NAT_PRT"] = header["nxlnptNatPrtCd"].InnerText;
                            if (header["nxlnptPrtNm"] != null) drHeader["NM_NX_INPT_PRT"] = header["nxlnptPrtNm"].InnerText;
                            if (header["dstnNatPrtCd"] != null) drHeader["CD_DSTN_NAT_PRT"] = header["dstnNatPrtCd"].InnerText;
                            if (header["dstnPrtNm"] != null) drHeader["NM_DSTN_PRT"] = header["dstnPrtNm"].InnerText;

                            foreach (XmlElement line in header.GetElementsByTagName("detail"))
                            {
                                drLine = this._dtLine.NewRow();

                                if (header["prtAgCd"] != null) drLine["CD_PRTAG"] = header["prtAgCd"].InnerText;
                                if (header["prtAgNm"] != null) drLine["NM_PRTAG"] = header["prtAgNm"].InnerText;
                                if (header["etryptYear"] != null) drLine["DT_ETRYPT_YEAR"] = header["etryptYear"].InnerText;
                                if (header["etryptCo"] != null) drLine["CNT_ETRYPT"] = header["etryptCo"].InnerText;
                                if (header["clsgn"] != null) drLine["CLSGN"] = header["clsgn"].InnerText;
                                if (header["vsslNm"] != null) drLine["NM_VSSL"] = header["vsslNm"].InnerText;

                                if (line["etryndNm"] != null) drLine["NM_ETRYND"] = line["etryndNm"].InnerText;
                                if (line["etryptDt"] != null) drLine["DT_ETRYPT"] = line["etryptDt"].InnerText;
                                if (line["tkoffDt"] != null) drLine["DT_TKOFF"] = line["tkoffDt"].InnerText;
                                if (line["ibobprtNm"] != null) drLine["NM_IBOBPRT"] = line["ibobprtNm"].InnerText;
                                if (line["laidupFcltyCd"] != null) drLine["CD_LAIDUPFCLTY"] = line["laidupFcltyCd"].InnerText;
                                if (line["laidupFcltySubCd"] != null) drLine["CD_LAIDUPFCLTY_SUB"] = line["laidupFcltySubCd"].InnerText;
                                if (line["laidupFcltyNm"] != null) drLine["NM_LAIDUPFCLTY"] = line["laidupFcltyNm"].InnerText;
                                if (line["piltgYn"] != null) drLine["YN_PILTG"] = line["piltgYn"].InnerText;
                                if (line["ldadngFrghtClCd"] != null) drLine["CD_LDADNG_FRGHT_CL"] = line["ldadngFrghtClCd"].InnerText;
                                if (line["ldadngTon"] != null) drLine["TON_LDADNG"] = line["ldadngTon"].InnerText;
                                if (line["trnpdtTon"] != null) drLine["TON_TRNPDT"] = line["trnpdtTon"].InnerText;
                                if (line["landngFrghtTon"] != null) drLine["TON_LANDNG_FRGHT"] = line["landngFrghtTon"].InnerText;
                                if (line["ldFrghtTon"] != null) drLine["TON_LD_FRGHT"] = line["ldFrghtTon"].InnerText;
                                if (line["grtg"] != null) drLine["GRTG"] = line["grtg"].InnerText;
                                if (line["intrlGrtg"] != null) drLine["INTRL_GRTG"] = line["intrlGrtg"].InnerText;
                                if (line["satmntEntrpsNm"] != null) drLine["NM_SATMN_ENTRPS"] = line["satmntEntrpsNm"].InnerText;
                                if (line["crewCo"] != null) drLine["CNT_CREW"] = line["crewCo"].InnerText;
                                if (line["koranCrewCo"] != null) drLine["CNT_KORAN_CREW"] = line["koranCrewCo"].InnerText;
                                if (line["frgnrCrewCo"] != null) drLine["CNT_FRGNR_CREW"] = line["frgnrCrewCo"].InnerText;

                                this._dtLine.Rows.Add(drLine);
                            }

                            this._dtHeader.Rows.Add(drHeader);
                        }
                    }
                }

                this.grdHeader.DataSource = this._dtHeader;
                this.grdLine.DataSource = this._dtLine;

                this.운항정보저장H();
                this.운항정보저장L();
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
            finally
            {
                this.DB접속해제();

                if (this.chk로그파일저장.Checked == true)
                    this.파일쓰기(DateTime.Today.ToString("yyyyMMdd") + "_log", this.txt로그.Text);
            }
        }
        #endregion

        #region 메소드
        private void DBConnection생성()
        {
            this._connectionString = string.Format("Data Source={0},{1};Initial Catalog={2};User id={3};Password={4}",
                                                   this.txt서버IP.Text,
                                                   this.txt포트번호.Text,
                                                   this.txtDB.Text,
                                                   this.txt사용자.Text,
                                                   this.txt패스워드.Text);

            this._sqlConnetion = new SqlConnection(this._connectionString);
        }

        private void DB접속()
        {
            try
            {
                if (this._sqlConnetion != null)
                {
                    if (this._sqlConnetion.State == ConnectionState.Closed)
                    {
                        this._sqlConnetion.Open();
                        this.WriteLog("DB 접속");
                    }
                }
                else
                {
                    this.DBConnection생성();
                    this._sqlConnetion.Open();
                    this.WriteLog("DB 접속");
                }
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
        }

        private void DB접속해제()
        {
            try
            {
                if (this._sqlConnetion != null)
                {
                    if (this._sqlConnetion.State == ConnectionState.Open)
                    {
                        this._sqlConnetion.Close();
                        this.WriteLog("DB 접속해제");
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
        }

        private void WriteLog(string log)
        {
            this.txt로그.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + log + Environment.NewLine;
        }

        private void 파일쓰기(string fileName, string text)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            string filePath;

            try
            {
                if (string.IsNullOrEmpty(text.Trim())) return;

                filePath = Application.StartupPath + "\\VsslEtrynd\\" + fileName + ".txt";

                if (!Directory.Exists(Application.StartupPath + "\\VsslEtrynd"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\VsslEtrynd");
                }

                fileStream = new FileStream(filePath, FileMode.Append);

                streamWriter = new StreamWriter(fileStream, Encoding.Default);
                streamWriter.Write("Log Time : " + DateTime.Now.ToString("yyyy-MM-dd tt hh:mm:ss") + Environment.NewLine + text + Environment.NewLine);
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }
        }

        private void 운항정보저장H()
        {
            string procedure;

            try
            {
                if (this._dtHeader == null || this._dtHeader.Rows.Count == 0) return;

                procedure = "SP_CZ_SA_VSSL_ETRYNDH";

                this._sqlCommand = new SqlCommand(procedure, this._sqlConnetion);
                this._sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (DataRow dataRow in this._dtHeader.Rows)
                {
                    this._sqlCommand.Parameters.Clear();

                    this._sqlCommand.Parameters.AddWithValue("@P_CD_PRTAG", dataRow["CD_PRTAG"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_PRTAG", dataRow["NM_PRTAG"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_DT_ETRYPT_YEAR", dataRow["DT_ETRYPT_YEAR"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CNT_ETRYPT", dataRow["CNT_ETRYPT"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CLSGN", dataRow["CLSGN"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_VSSL", dataRow["NM_VSSL"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CD_VSSL_NLTY", dataRow["CD_VSSL_NLTY"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_VSSL_NLTY", dataRow["NM_VSSL_NLTY"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_VSSL_KND", dataRow["NM_VSSL_KND"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CD_ETRYPT_PURPS", dataRow["CD_ETRYPT_PURPS"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_ETRYPT_PURPS", dataRow["NM_ETRYPT_PURPS"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CD_FRST_DPMPRT_NAT_PRT", dataRow["CD_FRST_DPMPRT_NAT_PRT"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_FRST_DPMPRT_PRT", dataRow["NM_FRST_DPMPRT_PRT"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CD_PRVS_DPMPRT_NAT_PRT", dataRow["CD_PRVS_DPMPRT_NAT_PRT"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_PRVS_DPMPRT_PRT", dataRow["NM_PRVS_DPMPRT_PRT"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CD_NX_INPT_NAT_PRT", dataRow["CD_NX_INPT_NAT_PRT"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_NX_INPT_PRT", dataRow["NM_NX_INPT_PRT"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CD_DSTN_NAT_PRT", dataRow["CD_DSTN_NAT_PRT"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_DSTN_PRT", dataRow["NM_DSTN_PRT"]);

                    this._sqlCommand.ExecuteNonQuery();
                }

                this.WriteLog("Header 정보저장 : " + this._dtHeader.Rows.Count.ToString());
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
        }

        private void 운항정보저장L()
        {
            string procedure;

            try
            {
                if (this._dtLine == null || this._dtLine.Rows.Count == 0) return;

                procedure = "SP_CZ_SA_VSSL_ETRYNDL";

                this._sqlCommand = new SqlCommand(procedure, this._sqlConnetion);
                this._sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (DataRow dataRow in this._dtLine.Rows)
                {
                    this._sqlCommand.Parameters.Clear();

                    this._sqlCommand.Parameters.AddWithValue("@P_CD_PRTAG", dataRow["CD_PRTAG"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_PRTAG", dataRow["NM_PRTAG"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_DT_ETRYPT_YEAR", dataRow["DT_ETRYPT_YEAR"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CNT_ETRYPT", dataRow["CNT_ETRYPT"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CLSGN", dataRow["CLSGN"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_VSSL", dataRow["NM_VSSL"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_ETRYND", dataRow["NM_ETRYND"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_DT_ETRYPT", dataRow["DT_ETRYPT"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_DT_TKOFF", dataRow["DT_TKOFF"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_IBOBPRT", dataRow["NM_IBOBPRT"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CD_LAIDUPFCLTY", dataRow["CD_LAIDUPFCLTY"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CD_LAIDUPFCLTY_SUB", dataRow["CD_LAIDUPFCLTY_SUB"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_LAIDUPFCLTY", dataRow["NM_LAIDUPFCLTY"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_YN_PILTG", dataRow["YN_PILTG"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CD_LDADNG_FRGHT_CL", dataRow["CD_LDADNG_FRGHT_CL"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_TON_LDADNG", dataRow["TON_LDADNG"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_TON_TRNPDT", dataRow["TON_TRNPDT"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_TON_LANDNG_FRGHT", dataRow["TON_LANDNG_FRGHT"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_TON_LD_FRGHT", dataRow["TON_LD_FRGHT"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_GRTG", dataRow["GRTG"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_INTRL_GRTG", dataRow["INTRL_GRTG"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_NM_SATMN_ENTRPS", dataRow["NM_SATMN_ENTRPS"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CNT_CREW", dataRow["CNT_CREW"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CNT_KORAN_CREW", dataRow["CNT_KORAN_CREW"]);
                    this._sqlCommand.Parameters.AddWithValue("@P_CNT_FRGNR_CREW", dataRow["CNT_FRGNR_CREW"]);


                    this._sqlCommand.ExecuteNonQuery();
                }

                this.WriteLog("Line 정보저장 : " + this._dtLine.Rows.Count.ToString());
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }
        }

        private DataTable 항만청정보()
        {
            DataTable dt;
            string sql;

            try
            {
                dt = new DataTable();

                sql = @"SELECT CD_SYSDEF,
                               NM_SYSDEF 
                        FROM MA_CODEDTL WITH(NOLOCK)
                        WHERE CD_COMPANY = '" + this.txt회사코드.Text + "'" +
                       "AND CD_FIELD = 'CZ_MA00025'";

                Console.WriteLine(sql);

                this._sqlCommand = new SqlCommand(sql, this._sqlConnetion);
                this._sqlCommand.CommandType = CommandType.Text;
                this._sqlDataAdapter = new SqlDataAdapter();
                this._sqlDataAdapter.SelectCommand = this._sqlCommand;
                this._sqlDataAdapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }

            return null;
        }

        private DataTable CreateHeaderTable()
        {
            DataTable dt;

            try
            {
                dt = new DataTable();

                dt.Columns.Add("CD_PRTAG");
                dt.Columns.Add("NM_PRTAG");
                dt.Columns.Add("DT_ETRYPT_YEAR");
                dt.Columns.Add("CNT_ETRYPT");
                dt.Columns.Add("CLSGN");
                dt.Columns.Add("NM_VSSL");
                dt.Columns.Add("CD_VSSL_NLTY");
                dt.Columns.Add("NM_VSSL_NLTY");
                dt.Columns.Add("NM_VSSL_KND");
                dt.Columns.Add("CD_ETRYPT_PURPS");
                dt.Columns.Add("NM_ETRYPT_PURPS");
                dt.Columns.Add("CD_FRST_DPMPRT_NAT_PRT");
                dt.Columns.Add("NM_FRST_DPMPRT_PRT");
                dt.Columns.Add("CD_PRVS_DPMPRT_NAT_PRT");
                dt.Columns.Add("NM_PRVS_DPMPRT_PRT");
                dt.Columns.Add("CD_NX_INPT_NAT_PRT");
                dt.Columns.Add("NM_NX_INPT_PRT");
                dt.Columns.Add("CD_DSTN_NAT_PRT");
                dt.Columns.Add("NM_DSTN_PRT");

                return dt;
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }

            return null;
        }

        private DataTable CreateLineTable()
        {
            DataTable dt;

            try
            {
                dt = new DataTable();

                dt.Columns.Add("CD_PRTAG");
                dt.Columns.Add("NM_PRTAG");
                dt.Columns.Add("DT_ETRYPT_YEAR");
                dt.Columns.Add("CNT_ETRYPT");
                dt.Columns.Add("CLSGN");
                dt.Columns.Add("NM_VSSL");

                dt.Columns.Add("NM_ETRYND");
                dt.Columns.Add("DT_ETRYPT");
                dt.Columns.Add("DT_TKOFF");
                dt.Columns.Add("NM_IBOBPRT");
                dt.Columns.Add("CD_LAIDUPFCLTY");
                dt.Columns.Add("CD_LAIDUPFCLTY_SUB");
                dt.Columns.Add("NM_LAIDUPFCLTY");
                dt.Columns.Add("YN_PILTG");
                dt.Columns.Add("CD_LDADNG_FRGHT_CL");
                dt.Columns.Add("TON_LDADNG");
                dt.Columns.Add("TON_TRNPDT");
                dt.Columns.Add("TON_LANDNG_FRGHT");
                dt.Columns.Add("TON_LD_FRGHT");
                dt.Columns.Add("GRTG");
                dt.Columns.Add("INTRL_GRTG");
                dt.Columns.Add("NM_SATMN_ENTRPS");
                dt.Columns.Add("CNT_CREW");
                dt.Columns.Add("CNT_KORAN_CREW");
                dt.Columns.Add("CNT_FRGNR_CREW");

                return dt;
            }
            catch (Exception ex)
            {
                this.WriteLog(ex.ToString());
            }

            return null;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Xml;
using System.IO;

namespace VsslEtryndConsole
{
    class Program
    {
        public static SqlConnection _sqlConnetion;
        public static SqlDataAdapter _sqlDataAdapter;
        public static SqlCommand _sqlCommand;
        public static string _connectionString;
        public static DataTable _dtHeader, _dtLine;

        static void Main(string[] args)
        {
            운항정보가져오기();
        }

        public static void 운항정보가져오기()
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
                #region DataTable
                _dtHeader = CreateHeaderTable();
                _dtLine = CreateLineTable();
                #endregion

                DB접속();

                dt = 항만청정보();

                WriteLog("Start Date : " + DateTime.Today.ToString("yyyyMMdd"));
                WriteLog("End Date : " + DateTime.Today.AddMonths(3).ToString("yyyyMMdd"));

                foreach (DataRow dr in dt.Rows)
                {
                    #region 토탈페이지수 계산
                    try
                    {
                        address = new Uri("http://apis.data.go.kr/1192000/VsslEtrynd2/Info?serviceKey=" + serviceKey +
                                      "&pageNo=1&startPage=1&numOfRows=1&pageSize=1&prtAgCd=" + dr["CD_SYSDEF"].ToString() +
                                      "&sde=" + DateTime.Today.ToString("yyyyMMdd") +
                                      "&ede=" + DateTime.Today.AddMonths(3).ToString("yyyyMMdd"));

                        webClient = new WebClient() { Encoding = Encoding.UTF8 };
                        xml = webClient.DownloadString(address);

                        xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xml);

                        root = xmlDoc.DocumentElement;

                        totalCount = 0;

                        totalCount = Convert.ToInt32(root.GetElementsByTagName("totalCount")[0].InnerText);
                        totalPage = ((totalCount % 50) > 0 ? (totalCount / 50) + 1 : (totalCount / 50));

                        resultCode = root.GetElementsByTagName("resultCode")[0].InnerText;
                        resultMsg = root.GetElementsByTagName("resultMsg")[0].InnerText;
                    }
                    catch(Exception ex)
                    {
                        WriteLog(ex.ToString());
                        continue;
                    }
                    #endregion

                    if (resultCode != "00")
                    {
                        WriteLog(resultCode + " : " + resultMsg);
                        continue;
                    }
                    else
                        WriteLog(dr["NM_SYSDEF"].ToString() + " => TotalCount : " + totalCount.ToString() + " TotalPage : " + totalPage.ToString());

                    for (int pageNo = 1; pageNo <= totalPage; pageNo++)
                    {
                        WriteLog("Page No : " + pageNo.ToString());

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
                            drHeader = _dtHeader.NewRow();

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
                                drLine = _dtLine.NewRow();

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

                                _dtLine.Rows.Add(drLine);
                            }

                            _dtHeader.Rows.Add(drHeader);
                        }
                    }
                }

                운항정보저장H();
                운항정보저장L();
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            finally
            {
                DB접속해제();
            }
        }

        public static void DBConnection생성()
        {
            _connectionString = string.Format("Data Source={0},{1};Initial Catalog={2};User id={3};Password={4}",
                                                   "192.168.1.143",
                                                   "1433",
                                                   "NEOE",
                                                   "NEOE",
                                                   "NEOE");

            _sqlConnetion = new SqlConnection(_connectionString);
        }

        public static void DB접속()
        {
            try
            {
                if (_sqlConnetion != null)
                {
                    if (_sqlConnetion.State == ConnectionState.Closed)
                    {
                        _sqlConnetion.Open();
                        WriteLog("DB 접속");
                    }
                }
                else
                {
                    DBConnection생성();
                    _sqlConnetion.Open();
                    WriteLog("DB 접속");
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void DB접속해제()
        {
            try
            {
                if (_sqlConnetion != null)
                {
                    if (_sqlConnetion.State == ConnectionState.Open)
                    {
                        _sqlConnetion.Close();
                        WriteLog("DB 접속해제");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void WriteLog(string log)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + log + Environment.NewLine);
            파일쓰기(DateTime.Today.ToString("yyyyMMdd") + "_log", log);
        }

        private static void 파일쓰기(string fileName, string text)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            string filePath;

            try
            {
                if (string.IsNullOrEmpty(text.Trim())) return;

                filePath = "C:\\VsslEtrynd\\log\\" + fileName + ".txt";

                if (!Directory.Exists("C:\\VsslEtrynd\\log"))
                {
                    Directory.CreateDirectory("C:\\VsslEtrynd\\log");
                }

                fileStream = new FileStream(filePath, FileMode.Append);

                streamWriter = new StreamWriter(fileStream, Encoding.Default);
                streamWriter.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + text + Environment.NewLine);
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }
        }

        public static void 운항정보저장H()
        {
            string procedure;

            try
            {
                if (_dtHeader == null || _dtHeader.Rows.Count == 0) return;

                procedure = "SP_CZ_SA_VSSL_ETRYNDH";

                _sqlCommand = new SqlCommand(procedure, _sqlConnetion);
                _sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (DataRow dataRow in _dtHeader.Rows)
                {
                    _sqlCommand.Parameters.Clear();

                    _sqlCommand.Parameters.AddWithValue("@P_CD_PRTAG", dataRow["CD_PRTAG"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_PRTAG", dataRow["NM_PRTAG"]);
                    _sqlCommand.Parameters.AddWithValue("@P_DT_ETRYPT_YEAR", dataRow["DT_ETRYPT_YEAR"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CNT_ETRYPT", dataRow["CNT_ETRYPT"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CLSGN", dataRow["CLSGN"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_VSSL", dataRow["NM_VSSL"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CD_VSSL_NLTY", dataRow["CD_VSSL_NLTY"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_VSSL_NLTY", dataRow["NM_VSSL_NLTY"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_VSSL_KND", dataRow["NM_VSSL_KND"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CD_ETRYPT_PURPS", dataRow["CD_ETRYPT_PURPS"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_ETRYPT_PURPS", dataRow["NM_ETRYPT_PURPS"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CD_FRST_DPMPRT_NAT_PRT", dataRow["CD_FRST_DPMPRT_NAT_PRT"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_FRST_DPMPRT_PRT", dataRow["NM_FRST_DPMPRT_PRT"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CD_PRVS_DPMPRT_NAT_PRT", dataRow["CD_PRVS_DPMPRT_NAT_PRT"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_PRVS_DPMPRT_PRT", dataRow["NM_PRVS_DPMPRT_PRT"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CD_NX_INPT_NAT_PRT", dataRow["CD_NX_INPT_NAT_PRT"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_NX_INPT_PRT", dataRow["NM_NX_INPT_PRT"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CD_DSTN_NAT_PRT", dataRow["CD_DSTN_NAT_PRT"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_DSTN_PRT", dataRow["NM_DSTN_PRT"]);

                    _sqlCommand.ExecuteNonQuery();
                }

                WriteLog("Header 정보저장 : " + _dtHeader.Rows.Count.ToString());
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static void 운항정보저장L()
        {
            string procedure;

            try
            {
                if (_dtLine == null || _dtLine.Rows.Count == 0) return;

                procedure = "SP_CZ_SA_VSSL_ETRYNDL";

                _sqlCommand = new SqlCommand(procedure, _sqlConnetion);
                _sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (DataRow dataRow in _dtLine.Rows)
                {
                    _sqlCommand.Parameters.Clear();

                    _sqlCommand.Parameters.AddWithValue("@P_CD_PRTAG", dataRow["CD_PRTAG"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_PRTAG", dataRow["NM_PRTAG"]);
                    _sqlCommand.Parameters.AddWithValue("@P_DT_ETRYPT_YEAR", dataRow["DT_ETRYPT_YEAR"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CNT_ETRYPT", dataRow["CNT_ETRYPT"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CLSGN", dataRow["CLSGN"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_VSSL", dataRow["NM_VSSL"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_ETRYND", dataRow["NM_ETRYND"]);
                    _sqlCommand.Parameters.AddWithValue("@P_DT_ETRYPT", dataRow["DT_ETRYPT"]);
                    _sqlCommand.Parameters.AddWithValue("@P_DT_TKOFF", dataRow["DT_TKOFF"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_IBOBPRT", dataRow["NM_IBOBPRT"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CD_LAIDUPFCLTY", dataRow["CD_LAIDUPFCLTY"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CD_LAIDUPFCLTY_SUB", dataRow["CD_LAIDUPFCLTY_SUB"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_LAIDUPFCLTY", dataRow["NM_LAIDUPFCLTY"]);
                    _sqlCommand.Parameters.AddWithValue("@P_YN_PILTG", dataRow["YN_PILTG"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CD_LDADNG_FRGHT_CL", dataRow["CD_LDADNG_FRGHT_CL"]);
                    _sqlCommand.Parameters.AddWithValue("@P_TON_LDADNG", dataRow["TON_LDADNG"]);
                    _sqlCommand.Parameters.AddWithValue("@P_TON_TRNPDT", dataRow["TON_TRNPDT"]);
                    _sqlCommand.Parameters.AddWithValue("@P_TON_LANDNG_FRGHT", dataRow["TON_LANDNG_FRGHT"]);
                    _sqlCommand.Parameters.AddWithValue("@P_TON_LD_FRGHT", dataRow["TON_LD_FRGHT"]);
                    _sqlCommand.Parameters.AddWithValue("@P_GRTG", dataRow["GRTG"]);
                    _sqlCommand.Parameters.AddWithValue("@P_INTRL_GRTG", dataRow["INTRL_GRTG"]);
                    _sqlCommand.Parameters.AddWithValue("@P_NM_SATMN_ENTRPS", dataRow["NM_SATMN_ENTRPS"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CNT_CREW", dataRow["CNT_CREW"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CNT_KORAN_CREW", dataRow["CNT_KORAN_CREW"]);
                    _sqlCommand.Parameters.AddWithValue("@P_CNT_FRGNR_CREW", dataRow["CNT_FRGNR_CREW"]);


                    _sqlCommand.ExecuteNonQuery();
                }

                WriteLog("Line 정보저장 : " + _dtLine.Rows.Count.ToString());
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static DataTable 항만청정보()
        {
            DataTable dt;
            string sql;

            try
            {
                dt = new DataTable();

                sql = @"SELECT CD_SYSDEF,
                               NM_SYSDEF 
                        FROM MA_CODEDTL WITH(NOLOCK)
                        WHERE CD_COMPANY = 'K100'" +
                       "AND CD_FIELD = 'CZ_MA00025'";

                Console.WriteLine(sql);

                _sqlCommand = new SqlCommand(sql, _sqlConnetion);
                _sqlCommand.CommandType = CommandType.Text;
                _sqlDataAdapter = new SqlDataAdapter();
                _sqlDataAdapter.SelectCommand = _sqlCommand;
                _sqlDataAdapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }

            return null;
        }

        public static DataTable CreateHeaderTable()
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
                WriteLog(ex.ToString());
            }

            return null;
        }

        public static DataTable CreateLineTable()
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
                WriteLog(ex.ToString());
            }

            return null;
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace PaymentDueDateConsole
{
    class Program
    {
        public static SqlConnection _sqlConnetion;
        public static SqlDataAdapter _sqlDataAdapter;
        public static SqlCommand _sqlCommand;
        public static string _connectionString;
        public static string _회사코드;

        static void Main(string[] args)
        {
            _회사코드 = args[0];
            미지급내역메일발송();
        }

        public static void 미지급내역메일발송()
        {
            DataTable dt;
            string 보내는사람, 받는사람, 숨은참조, 제목, html, color;

            try
            {
                DBConnection생성();
                DB접속();

                dt = GetDataTable1("SP_CZ_PU_PAYMENT_DUE_DATE_S", _회사코드);

                DB접속해제();

                if (dt != null && dt.Rows.Count > 0)
                {
                    WriteLog("총 건수 : " + dt.Rows.Count.ToString() + "건");

                    보내는사람 = "admin@dintec.co.kr/관리자";
                    숨은참조 = "khkim@dintec.co.kr";

                    if (_회사코드 == "K100")
                        받는사람 = "adm@dintec.co.kr";
                    else if (_회사코드 == "K200")
                        받는사람 = "adm@dubheco.com";
                    else
                        return;

                    제목 = "미지급 내역 알람";

                    html = @"<br/>
								 <div style='text-align:left; font-weight: bold;'>*** 미지급 내역</div>

							 <table style='width:1000px; border:2px solid black; font-size: 9pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;'>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
                                <colgroup width='120px' align='center'></colgroup>
								<tbody>
								<tr style='height:30px'>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>지급예정일</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>경과일수</th>                                    
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>매입번호</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>매입처</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>통화</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>외화금액</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>원화금액</th>
                                    <th style='border:solid 1px black; text-align:center; background-color:Silver'>지급금액</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>잔액</th>
								</tr>";

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt32(dr["DT_DIFF"]) >= 90)
                            color = "color: #FF0000;";
                        else if (Convert.ToInt32(dr["DT_DIFF"]) >= 30)
                            color = "color: #0000FF;";
                        else
                            color = "color: #000000;";

                        if (dr["NM_EXCH"].ToString() == "KRW")
                        {
                            html += @"<tr style='height:30px'>
										 <th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + GetTo_DateStringS(dr["DT_PAY_PREARRANGED"]) + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + dr["DT_DIFF"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_IV"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["LN_PARTNER"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NM_EXCH"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:right; padding-right:10px; font-weight:normal'>0</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:right; padding-right:10px; font-weight:normal'>" + GetTO_Money(dr["AM_TOTAL"]) + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:right; padding-right:10px; font-weight:normal'>" + GetTO_Money(dr["AM_DOCU1"]) + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:right; padding-right:10px; font-weight:bold'>" + GetTO_Money(dr["AM_REMAIN"]) + "</th>" + Environment.NewLine +
                                     "</tr>";
                        }
                        else
                        {
                            html += @"<tr style='height:30px'>
										 <th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + GetTo_DateStringS(dr["DT_PAY_PREARRANGED"]) + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + dr["DT_DIFF"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_IV"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["LN_PARTNER"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NM_EXCH"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:right; padding-right:10px; font-weight:normal'>" + GetTO_Money(dr["AM_EX"]) + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:right; padding-right:10px; font-weight:normal'>" + GetTO_Money(dr["AM_TOTAL"]) + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:right; padding-right:10px; font-weight:normal'>" + GetTO_Money(dr["AM_DOCU1"]) + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:right; padding-right:10px; font-weight:bold'>" + GetTO_Money(dr["AM_REMAIN"]) + "</th>" + Environment.NewLine +
                                     "</tr>";
                        }
                    }

                    html += @"</tbody>
					  </table>";

                    if (메일발송(보내는사람, 받는사람, string.Empty, 숨은참조, 제목, string.Empty, html, true) == true)
                        WriteLog("발송완료 : " + 받는사람);
                    else
                        WriteLog("발송실패 : " + 받는사람);
                }

                WriteLog("완료");
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

        public static bool 메일발송(string 보내는사람, string 받는사람, string 참조, string 숨은참조, string 제목, string 본문, string html, bool sendMail)
        {
            MailMessage mailMessage;
            SmtpClient smtpClient;
            string[] tempText;
            string address, name, id, pw, domain, query;

            try
            {
                #region 기본설정
                mailMessage = new MailMessage();
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                #endregion

                #region 보내는사람
                tempText = 보내는사람.Split('/');

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
                    return false;

                tempText = address.Split('@');

                if (tempText.Length != 2) return false;

                id = tempText[0];
                domain = tempText[1];

                DBConnection생성1();
                DB접속();

                query = @"SELECT DM.DM_NAME,
								 DU.DU_USERID,
								 DU.DU_PWD
						  FROM MCDOMAINUSER DU WITH(NOLOCK)
						  LEFT JOIN MCDOMAIN DM WITH(NOLOCK) ON DM.DM_ID = DU.DM_ID
						  WHERE DM.DM_NAME = '" + domain + "'" + Environment.NewLine +
                         "AND DU.DU_USERID = '" + id + "'";

                pw = GetDataTable(query).Rows[0]["DU_PWD"].ToString();

                DB접속해제();
                #endregion

                #region 메일정보
                mailMessage.From = new MailAddress(address, name, Encoding.UTF8);

                foreach (string 받는사람1 in 받는사람.Split(';'))
                {
                    if (받는사람1.Trim() != "")
                        mailMessage.To.Add(new MailAddress(받는사람1.Replace(";", "")));
                }

                foreach (string 참조1 in 참조.Split(';'))
                {
                    if (참조1.Trim() != "")
                        mailMessage.CC.Add(new MailAddress(참조1.Replace(";", "")));
                }

                foreach (string 숨은참조1 in 숨은참조.Split(';'))
                {
                    if (숨은참조1.Trim() != "")
                        mailMessage.Bcc.Add(new MailAddress(숨은참조1.Replace(";", "")));
                }

                mailMessage.Subject = 제목;

                // 본문 html로 변환할 시 <a>태그 앞뒤로 하고 <a>태그 내부는 건드리지 않음
                string body = "";
                string bodyA = "";
                string bodyB = "";
                string bodyC = "";

                int index = 본문.IndexOf("<a href=");

                if (index > 0)
                {
                    bodyA = 본문.Substring(0, index);
                    bodyB = 본문.Substring(index, 본문.IndexOf("</a>") + 4 - index);
                    bodyC = 본문.Substring(본문.IndexOf("</a>") + 4);

                    body = ""
                        + bodyA.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />")
                        + bodyB
                        + bodyC.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />");
                }
                else
                {
                    body = 본문.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />"); ;
                }

                mailMessage.Body = "<div style='font-family:맑은 고딕; font-size:9pt'>" + body + "</div>" + html;
                #endregion

                #region 메일보내기
                smtpClient = new SmtpClient("113.130.254.131", 25);
                smtpClient.EnableSsl = false;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(address, pw);
                if (sendMail == true) smtpClient.Send(mailMessage);
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            finally
            {
                DB접속해제();
            }

            return false;
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

        public static void DBConnection생성1()
        {
            _connectionString = string.Format("Data Source={0},{1};Initial Catalog={2};User id={3};Password={4}",
                                              "192.168.1.2",
                                              "1433",
                                              "MCE7",
                                              "sa",
                                              "!q7hfnl3sh62@");

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
                        //WriteLog("DB 접속");
                    }
                }
                else
                {
                    DBConnection생성();
                    _sqlConnetion.Open();
                    //WriteLog("DB 접속");
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
                        //WriteLog("DB 접속해제");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        public static DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();

            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandText = query;
            _sqlCommand.CommandTimeout = 300;
            _sqlCommand.CommandType = CommandType.Text;
            _sqlCommand.Connection = _sqlConnetion;

            _sqlDataAdapter = new SqlDataAdapter();
            _sqlDataAdapter.SelectCommand = _sqlCommand;
            _sqlDataAdapter.Fill(dt);

            return dt;
        }

        public static DataTable GetDataTable1(string procedure, string param)
        {
            DataTable dt = new DataTable();

            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandText = procedure;
            _sqlCommand.CommandTimeout = 300;
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            _sqlCommand.Connection = _sqlConnetion;

            _sqlCommand.Parameters.Add(new SqlParameter("@P_CD_COMPANY", param));

            _sqlDataAdapter = new SqlDataAdapter();
            _sqlDataAdapter.SelectCommand = _sqlCommand;
            _sqlDataAdapter.Fill(dt);

            return dt;
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

                filePath = "C:\\PaymentDueDateConsole\\log\\" + fileName + ".txt";

                if (!Directory.Exists("C:\\PaymentDueDateConsole\\log"))
                {
                    Directory.CreateDirectory("C:\\PaymentDueDateConsole\\log");
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

        public static string GetTo_DateStringS(object value)
        {
            if (value == null || value.ToString() == "" || value.ToString().Length != 8) return "";
            return value.ToString().Substring(0, 4) + "/" + value.ToString().Substring(4, 2) + "/" + value.ToString().Substring(6, 2);
        }

        public static string GetTO_Money(object value)
        {
            if (value == null) return "";
            return string.Format("{0:#,##0.##}", value);
        }
    }
}

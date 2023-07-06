using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoMailConsole
{
	class AutoMailConsole
	{
		public static SqlConnection _sqlConnetion;
		public static SqlDataAdapter _sqlDataAdapter;
		public static SqlCommand _sqlCommand;
		public static string _connectionString;
		public static string _log;

		static void Main(string[] args)
		{
			try
			{
				WriteLog("작업 시작");

				if (DateTime.Now.Day == 25) //매달 25일
					VSHIPS();

				if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday || //매주 화요일
					DateTime.Now.DayOfWeek == DayOfWeek.Thursday) //매주 목요일
					납기지연현황();

				WriteLog("작업 종료");
			}
			catch (Exception ex)
			{
				WriteLog(ex.ToString());
			}
		}

		public static void VSHIPS()
		{
			Microsoft.Office.Interop.Excel.Application application = null;
			Microsoft.Office.Interop.Excel.Workbook workBook = null;
			Microsoft.Office.Interop.Excel.Worksheet workSheet = null;
			Microsoft.Office.Interop.Excel.Range range;
			string 보내는사람, 받는사람, 참조, 숨은참조, html, query;

			try
			{
				보내는사람 = "service@dintec.co.kr/DINTEC CO., LTD.";
				받는사람 = "marcasdisputeandresolutionteam@marcassupplychain.com";
				참조 = "accounting@marcassupplychain.com";
				숨은참조 = "olivier.barrois@marcassupplychain.com;sd@dintec.co.kr;khkim@dintec.co.kr;syshin@dintec.co.kr";

				html = @"<div>Dear Dispute Resolution Team,<br><br>
Thanks for your good cooperation and effort on following outstanding invoices.<br><br>
Please refer to the attached “V.SHIP GROUP OUTSTANDING INVOICE LIST”<br><br>
It will be highly appreciated, if you give us payment detail or expected payment schedule as soon as possible.<br><br>
Thanks in advance.<br><br>
Have a good day.<br><br>
<span style='color: #1f497d; font-weight: bold;'>
Account Manager / Account Dept. of Dintec<br>
</span>
<span style='color: #1f497d;'>
DINTEC CO., ltd. Busan, Korea<br>
As a parts sales agent of Hyundai Engine<br> 
Tel : + 82 51 664 1018<br> 
Fax : +82 51 462 7907~9<br>
Mobile: +82 10 7151 3232<br>
E-mail : service@dintec.co.kr<br>
Homepage : www.dintec.co.kr
</span></div>";

				#region 데이터 조회
				query = @"SELECT MP.LN_PARTNER,
       (SELECT STRING_AGG(SH.NO_PO_PARTNER, ',') AS NO_PO_PARTNER
        FROM SA_SOH SH
        WHERE EXISTS (SELECT 1 
                      FROM SA_IVL IL WITH(NOLOCK)
                      WHERE IL.CD_COMPANY = SH.CD_COMPANY
                      AND IL.NO_SO = SH.NO_SO
                      AND IL.NO_IV = IH.NO_IV)) AS NO_PO_PARTNER,
       (SELECT STRING_AGG(MH.NM_VESSEL, ',') AS NM_VESSEL
        FROM SA_SOH SH
        LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
        WHERE EXISTS (SELECT 1 
                      FROM SA_IVL IL WITH(NOLOCK)
                      WHERE IL.CD_COMPANY = SH.CD_COMPANY
                      AND IL.NO_SO = SH.NO_SO
                      AND IL.NO_IV = IH.NO_IV)) AS NM_VESSEL,
       IH.NO_IV,
       FORMAT(CONVERT(DATETIME, IH.DT_PROCESS), 'yyyy-MM-dd') AS DT_IV,
       MC.NM_SYSDEF AS NM_EXCH,
       FORMAT(IH.AM_EX, 'N') AS AM_IV
FROM SA_IVH IH WITH(NOLOCK)
LEFT JOIN MA_PARTNER MP WITH(NOLOCK) ON MP.CD_COMPANY = IH.CD_COMPANY AND MP.CD_PARTNER = IH.CD_PARTNER
LEFT JOIN MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = IH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = IH.CD_EXCH
WHERE IH.CD_COMPANY = 'K100'
AND IH.CD_PARTNER IN 
(
    '01677',
    '02064',
    '02065',
    '02070',
    '09527',
    '10253',
    '12470',
    '12715',
    '12848',
    '12858',
	'02067',
	'17003',
	'02066'
)
AND DATEDIFF(DAY, IH.DT_PROCESS, GETDATE()) >= 60
AND (ISNULL(IH.AM_K, 0) + ISNULL(IH.VAT_TAX, 0)) > IH.AM_BAN
ORDER BY MP.LN_PARTNER ASC, IH.DT_PROCESS ASC";

				DBConnection생성();
				DB접속();

				DataTable dt = GetDataTable(query);

				DB접속해제();
				#endregion

				string localPath = "C:\\AUTO_MAIL\\V.SHIPS.xlsx";
				string serverPath = "C:\\AUTO_MAIL\\V.SHIPS_FORMAT.xlsx";

				File.Copy(serverPath, localPath, true);

				#region 엑셀 생성
				application = new Microsoft.Office.Interop.Excel.Application();
				application.Visible = false;
				workBook = application.Workbooks.Open(localPath);
				workSheet = workBook.Worksheets[1];

				int index = 2;

				foreach (DataRow dr in dt.Rows)
				{
					range = workSheet.Cells[index, 1];
					range.Value = dr["LN_PARTNER"].ToString();

					range = workSheet.Cells[index, 2];
					range.Value = dr["NO_PO_PARTNER"].ToString();

					range = workSheet.Cells[index, 3];
					range.Value = dr["NM_VESSEL"].ToString();

					range = workSheet.Cells[index, 4];
					range.Value = dr["NO_IV"].ToString();

					range = workSheet.Cells[index, 5];
					range.Value = dr["DT_IV"].ToString();

					range = workSheet.Cells[index, 6];
					range.Value = dr["NM_EXCH"].ToString();

					range = workSheet.Cells[index, 7];
					range.Value = dr["AM_IV"].ToString();

					index++;
				}

				workBook.Close(true);
				application.Quit();

				ReleaseExcelObject(workSheet);
				ReleaseExcelObject(workBook);
				ReleaseExcelObject(application);
				#endregion

				메일발송(보내는사람, 받는사람, 참조, 숨은참조, "V.SHIP GROUP OUTSTANDING INVOICE LIST (" + DateTime.Now.ToString("yyyy.MM") + ")", string.Empty, html, localPath, true);
			}
			catch (Exception ex)
			{
				WriteLog(ex.ToString());
			}
		}

		public static void 납기지연현황()
		{
			Microsoft.Office.Interop.Excel.Application application = null;
			Microsoft.Office.Interop.Excel.Workbook workBook = null;
			Microsoft.Office.Interop.Excel.Worksheet workSheet = null;
			Microsoft.Office.Interop.Excel.Range range;
			string 제목, 보내는사람, 받는사람, 숨은참조, html, query;

			try
			{
				제목 = "[알림] 수주 납기지연 리스트 (" + DateTime.Now.AddYears(-2).ToString("yyyy-MM-dd") + "~" + DateTime.Now.ToString("yyyy-MM-dd") + ")";
				보내는사람 = "admin@dintec.co.kr";
				받는사람 = "sd@dintec.co.kr";
				숨은참조 = "khkim@dintec.co.kr";

				html = @"<div>수신 : 수신자제위<br><br>
						 본 메일은 매주 화, 목요일 자동으로 발송되는 수주 납기 지연 리스트 입니다.<br><br>
						 유첨된 파일 참고 하시기 바랍니다.<br><br>
						 감사합니다.<br><br>";

				#region 데이터 조회
				query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

;WITH A AS
(
    SELECT SL.CD_COMPANY,
    	   SL.NO_SO, 
    	   SL.NO_SO AS NO_KEY,
    	   (CASE WHEN ISNULL(SH.DT_SO, '') = '' THEN NULL ELSE CONVERT(CHAR(10), CONVERT(DATETIME, SH.DT_SO), 23) END) AS DT_SO,
    	   (CASE WHEN ISNULL(SL.DT_DUEDATE, '') = '' THEN NULL ELSE CONVERT(CHAR(10), CONVERT(DATETIME, SL.DT_DUEDATE), 23) END) AS DT_LIMIT,
    	   SH.NO_PO_PARTNER,
    	   SH.CD_PARTNER,
    	   MP.LN_PARTNER AS NM_PARTNER,
    	   SL.NM_SUPPLIER,
    	   SL.NO_ORDER,
    	   MH.NM_VESSEL,
    	   SH.CD_SALEGRP,
    	   SG.NM_SALEGRP,
    	   SH.CD_PARTNER_GRP,
    	   CD.NM_SYSDEF AS NM_PARTNER_GRP,
    	   MP.CD_NATION,
    	   CD1.NM_SYSDEF AS NM_NATION,
    	   SH.NO_EMP,
    	   ME.NM_KOR AS NM_SO_EMP,
    	   WH.ID_LOG,
    	   MU.NM_USER AS NM_LOG_EMP,
    	   FORMAT(SL.AM_SO, '#,#') AS AM_SO,
    	   SL.QT_SO,
    	   SL.QT_PO,
    	   SL.QT_IN,
    	   SL.QT_GIR,
    	   SL.QT_OUT,
    	   (CASE WHEN SL.QT_SO > SL.QT_OUT AND SL.DT_DUEDATE <> '' THEN DATEDIFF(DAY, SL.DT_DUEDATE, GETDATE()) ELSE '' END) AS DT_DELAY,
    	   ISNULL(SH.YN_CLOSE, 'N') AS YN_CLOSE,
    	   (CASE WHEN SL.QT_PO > 0 AND SL.QT_PO = SL.QT_IN THEN 'Y' ELSE 'N' END) AS YN_ALL_IN,
    	   SH.DC_RMK_TEXT,
    	   SH.DC_RMK_TEXT2,
    	   (CASE WHEN ISNULL(DD.DT_EXPECT, '') = '' THEN NULL ELSE CONVERT(CHAR(10), CONVERT(DATETIME, DD.DT_EXPECT), 23) END) AS DT_EXPECT,
    	   (CASE WHEN ISNULL(SL.DT_EXPECT, '') = '' THEN NULL ELSE CONVERT(CHAR(10), CONVERT(DATETIME, SL.DT_EXPECT), 23) END) AS DT_EXPECT_IN,
    	   DD.DC_RMK,
    	   DD.TP_SEND,
    	   DD.DTS_SEND
    FROM SA_SOH SH
    JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
    		     SL.DT_DUEDATE,
    		     MAX(PL.CD_SUPPLIER) AS CD_SUPPLIER, 
    		     MAX(PL.NM_SUPPLIER) AS NM_SUPPLIER,
    		     MAX(PL.NO_ORDER) AS NO_ORDER,
    		     ISNULL(SUM(SL.AM_KR_S), 0) AS AM_SO,
    		     COUNT(SL.CD_ITEM) AS QT_SO,
    		     ISNULL(SUM(PL.QT_PO), 0) AS QT_PO,
    		     ISNULL(SUM(PL.QT_IN), 0) AS QT_IN,
    		     ISNULL(SUM(CASE WHEN SL.QT_SO = GL.QT_GIR THEN 1 ELSE 0 END), 0) AS QT_GIR,
    		     ISNULL(SUM(CASE WHEN SL.QT_SO = GL.QT_OUT THEN 1 ELSE 0 END), 0) AS QT_OUT,
    			 MAX(PL.DT_EXPECT) AS DT_EXPECT
    	  FROM SA_SOL SL
    	  LEFT JOIN (SELECT PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE,
    						MAX(PH.CD_PARTNER) AS CD_SUPPLIER, 
    						MAX(MP.LN_PARTNER) AS NM_SUPPLIER,
    						MAX(PH.NO_ORDER) AS NO_ORDER,
    						COUNT(PL.CD_ITEM) AS QT_PO,
    						SUM(CASE WHEN PL.QT_PO = IL.QT_IN THEN 1 ELSE 0 END) AS QT_IN,
    						MAX(DD.DT_EXPECT) AS DT_EXPECT 
    				 FROM PU_POL PL
    				 LEFT JOIN PU_POH PH ON PH.CD_COMPANY = PL.CD_COMPANY AND PH.NO_PO = PL.NO_PO
    				 LEFT JOIN (SELECT IL.CD_COMPANY, IL.NO_PSO_MGMT, IL.NO_PSOLINE_MGMT, ISNULL(SUM(IL.QT_IO), 0) AS QT_IN 
    							FROM MM_QTIO IL
    							JOIN MM_QTIOH IH ON IH.CD_COMPANY = IL.CD_COMPANY AND IH.NO_IO = IL.NO_IO
    							WHERE IH.YN_RETURN = 'N'
    							GROUP BY IL.CD_COMPANY, IL.NO_PSO_MGMT, IL.NO_PSOLINE_MGMT) IL
    				 ON IL.CD_COMPANY = PL.CD_COMPANY AND IL.NO_PSO_MGMT = PL.NO_PO AND IL.NO_PSOLINE_MGMT = PL.NO_LINE
    				 LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = PH.CD_COMPANY AND MP.CD_PARTNER = PH.CD_PARTNER
    				 LEFT JOIN (SELECT CD_COMPANY, NO_SO, NO_KEY, MAX(DT_EXPECT) AS DT_EXPECT 
    							FROM CZ_SA_DEFERRED_DELIVERY
    							WHERE TP_TYPE = '2'
    							GROUP BY CD_COMPANY, NO_SO, NO_KEY) DD 
    				 ON DD.CD_COMPANY = PL.CD_COMPANY AND DD.NO_SO = PL.NO_SO AND DD.NO_KEY = PL.NO_PO
    				 WHERE PL.CD_ITEM NOT LIKE 'SD%'
    				 GROUP BY PL.CD_COMPANY, PL.NO_SO, PL.NO_SOLINE
    				 UNION ALL
    				 SELECT SB.CD_COMPANY, SB.NO_FILE, SB.NO_LINE,
    						'STOCK' AS CD_SUPPLIER,
    						'STOCK' AS NM_SUPPLIER,
    						'STOCK' AS NO_ORDER,
    						1 AS QT_PO,
    						SUM(CASE WHEN SB.QT_STOCK = SB.QT_BOOK THEN 1 ELSE 0 END) AS QT_IN,
    						MAX(DD.DT_EXPECT) AS DT_EXPECT
    				 FROM CZ_SA_STOCK_BOOK SB
    				 LEFT JOIN (SELECT CD_COMPANY, NO_SO, NO_KEY, MAX(DT_EXPECT) AS DT_EXPECT 
    							FROM CZ_SA_DEFERRED_DELIVERY
    							WHERE TP_TYPE = '2'
    							GROUP BY CD_COMPANY, NO_SO, NO_KEY) DD 
    				 ON DD.CD_COMPANY = SB.CD_COMPANY AND DD.NO_SO = SB.NO_FILE AND DD.NO_KEY = 'STOCK'
    				 GROUP BY SB.CD_COMPANY, SB.NO_FILE, SB.NO_LINE) PL
    	  ON PL.CD_COMPANY = SL.CD_COMPANY AND PL.NO_SO = SL.NO_SO AND PL.NO_SOLINE = SL.SEQ_SO
    	  LEFT JOIN (SELECT GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO, 
    						ISNULL(SUM(GL.QT_GIR), 0) AS QT_GIR,
    						ISNULL(SUM(OL.QT_OUT), 0) AS QT_OUT
    				 FROM SA_GIRL GL
    				 LEFT JOIN (SELECT OL.CD_COMPANY, OL.NO_ISURCV, OL.NO_ISURCVLINE, SUM(OL.QT_IO) AS QT_OUT
    							FROM MM_QTIO OL
    							JOIN MM_QTIOH IH ON IH.CD_COMPANY = OL.CD_COMPANY AND IH.NO_IO = OL.NO_IO
    							WHERE IH.YN_RETURN = 'N'
    							GROUP BY OL.CD_COMPANY, OL.NO_ISURCV, OL.NO_ISURCVLINE) OL
    				 ON OL.CD_COMPANY = GL.CD_COMPANY AND OL.NO_ISURCV = GL.NO_GIR AND OL.NO_ISURCVLINE = GL.SEQ_GIR
    				 GROUP BY GL.CD_COMPANY, GL.NO_SO, GL.SEQ_SO) GL
    	   ON GL.CD_COMPANY = SL.CD_COMPANY AND GL.NO_SO = SL.NO_SO AND GL.SEQ_SO = SL.SEQ_SO
    	   WHERE SL.CD_ITEM NOT LIKE 'SD%'
    	   GROUP BY SL.CD_COMPANY, SL.NO_SO, SL.DT_DUEDATE) SL
    ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
    LEFT JOIN FI_GWDOCU GW ON GW.CD_COMPANY = 'K100' AND GW.CD_PC = '010000' AND GW.NO_DOCU = SH.NO_DOCU
    LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
    LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
    LEFT JOIN MA_SALEGRP SG ON SG.CD_COMPANY = SH.CD_COMPANY AND SG.CD_SALEGRP = SH.CD_SALEGRP
    LEFT JOIN MA_CODEDTL CD ON CD.CD_COMPANY = SH.CD_COMPANY AND CD.CD_FIELD = 'MA_B000065' AND CD.CD_SYSDEF = SH.CD_PARTNER_GRP
    LEFT JOIN MA_CODEDTL CD1 ON CD1.CD_COMPANY = MP.CD_COMPANY AND CD1.CD_FIELD = 'MA_B000020' AND CD1.CD_SYSDEF = MP.CD_NATION
    LEFT JOIN CZ_SA_DEFERRED_DELIVERY DD ON DD.CD_COMPANY = SL.CD_COMPANY AND DD.TP_TYPE = '0' AND DD.NO_SO = SL.NO_SO AND DD.NO_KEY = SL.NO_SO AND DD.DT_LIMIT = SL.DT_DUEDATE
    LEFT JOIN CZ_MA_WORKFLOWH WH ON WH.CD_COMPANY = SH.CD_COMPANY AND WH.NO_KEY = SH.NO_SO AND WH.TP_STEP = '08'
    LEFT JOIN MA_USER MU ON MU.CD_COMPANY = WH.CD_COMPANY AND MU.ID_USER = WH.ID_LOG
    LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = SH.NO_EMP
    WHERE SH.CD_COMPANY = 'K100'
    AND SH.DT_SO BETWEEN CONVERT(CHAR(8), DATEADD(YEAR, -2, GETDATE()), 112) AND CONVERT(CHAR(8), GETDATE(), 112)
    AND (ISNULL(SH.YN_CLOSE, 'N') <> 'Y' OR GW.ST_STAT <> '1' OR SL.QT_GIR > 0)
    AND (ISNULL(SH.YN_CLOSE, 'N') <> 'Y' OR GW.ST_STAT <> '1')
    AND (SL.QT_PO > 0 AND SL.QT_PO > SL.QT_IN)
    AND SL.QT_SO > SL.QT_GIR
    AND SL.QT_SO > SL.QT_OUT
    AND ((CASE WHEN SL.QT_SO > SL.QT_OUT AND SL.DT_DUEDATE <> '' THEN DATEDIFF(DAY, SL.DT_DUEDATE, GETDATE()) ELSE '' END) >= 30)
)
SELECT * 
FROM A
ORDER BY A.DT_DELAY DESC";

				DBConnection생성();
				DB접속();

				DataTable dt = GetDataTable(query);

				DB접속해제();
				#endregion

				string localPath = "C:\\AUTO_MAIL\\납기지연리스트.xlsx";
				string serverPath = "C:\\AUTO_MAIL\\DEFERRED_DELIVERY_FORMAT.xlsx";

				File.Copy(serverPath, localPath, true);

				#region 엑셀 생성
				application = new Microsoft.Office.Interop.Excel.Application();
				application.Visible = false;
				workBook = application.Workbooks.Open(localPath);
				workSheet = workBook.Worksheets[1];

				int index = 2;

				foreach (DataRow dr in dt.Rows)
				{
					range = workSheet.Cells[index, 1];
					range.Value = dr["NO_SO"].ToString();

					range = workSheet.Cells[index, 2];
					range.Value = dr["NO_PO_PARTNER"].ToString();

					range = workSheet.Cells[index, 3];
					range.Value = dr["NO_ORDER"].ToString();

					range = workSheet.Cells[index, 4];
					range.Value = dr["NM_PARTNER"].ToString();

					range = workSheet.Cells[index, 5];
					range.Value = dr["NM_SUPPLIER"].ToString();

					range = workSheet.Cells[index, 6];
					range.Value = dr["NM_VESSEL"].ToString();

					range = workSheet.Cells[index, 7];
					range.Value = dr["NM_SALEGRP"].ToString();

					range = workSheet.Cells[index, 8];
					range.Value = dr["NM_PARTNER_GRP"].ToString();

					range = workSheet.Cells[index, 9];
					range.Value = dr["NM_NATION"].ToString();

					range = workSheet.Cells[index, 10];
					range.Value = dr["NM_SO_EMP"].ToString();

					range = workSheet.Cells[index, 11];
					range.Value = dr["NM_LOG_EMP"].ToString();

					range = workSheet.Cells[index, 12];
					range.Value = dr["DT_SO"].ToString();

					range = workSheet.Cells[index, 13];
					range.Value = dr["DT_LIMIT"].ToString();

					range = workSheet.Cells[index, 14];
					range.Value = dr["AM_SO"].ToString();

					range = workSheet.Cells[index, 15];
					range.Value = dr["QT_SO"].ToString();

					range = workSheet.Cells[index, 16];
					range.Value = dr["QT_PO"].ToString();

					range = workSheet.Cells[index, 17];
					range.Value = dr["QT_IN"].ToString();

					range = workSheet.Cells[index, 18];
					range.Value = dr["QT_GIR"].ToString();

					range = workSheet.Cells[index, 19];
					range.Value = dr["QT_OUT"].ToString();

					range = workSheet.Cells[index, 20];
					range.Value = dr["DT_DELAY"].ToString();

					range = workSheet.Cells[index, 21];
					range.Value = dr["YN_CLOSE"].ToString();

					range = workSheet.Cells[index, 22];
					range.Value = dr["YN_ALL_IN"].ToString();

					range = workSheet.Cells[index, 23];
					range.Value = dr["DC_RMK_TEXT"].ToString();

					range = workSheet.Cells[index, 24];
					range.Value = dr["DC_RMK_TEXT2"].ToString();

					range = workSheet.Cells[index, 25];
					range.Value = dr["DT_EXPECT_IN"].ToString();

					range = workSheet.Cells[index, 26];
					range.Value = dr["DT_EXPECT"].ToString();

					range = workSheet.Cells[index, 27];
					range.Value = dr["DC_RMK"].ToString();

					index++;
				}

				workBook.Close(true);
				application.Quit();

				ReleaseExcelObject(workSheet);
				ReleaseExcelObject(workBook);
				ReleaseExcelObject(application);
				#endregion

				메일발송(보내는사람, 받는사람, string.Empty, 숨은참조, 제목, string.Empty, html, localPath, true);
			}
			catch (Exception ex)
			{
				WriteLog(ex.ToString());
			}
		}

		public static void ReleaseExcelObject(object obj)
		{
			try
			{
				if (obj != null)
				{
					Marshal.ReleaseComObject(obj);
					obj = null;
				}
			}
			catch (Exception ex)
			{
				obj = null;
				throw ex;
			}
			finally
			{
				GC.Collect();
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

		public static bool 메일발송(string 보내는사람, string 받는사람, string 참조, string 숨은참조, string 제목, string 본문, string html, string 첨부파일, bool sendMail)
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

				if (!string.IsNullOrEmpty(첨부파일))
					mailMessage.Attachments.Add(new Attachment(첨부파일));

				#region 메일보내기
				smtpClient = new SmtpClient("113.130.254.131", 587);
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

		public static void WriteLog(string log)
		{
			Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + log + Environment.NewLine);
			_log += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + log + Environment.NewLine;
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

				filePath = "C:\\AUTO_MAIL\\log\\" + fileName + ".txt";

				if (!Directory.Exists("C:\\AUTO_MAIL\\log"))
				{
					Directory.CreateDirectory("C:\\AUTO_MAIL\\log");
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
	}
}

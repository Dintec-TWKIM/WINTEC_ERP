using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace OutstandingInvoiceConsole
{
	class Program
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
				string 현재일자 = DateTime.Now.ToString("yyyyMMdd");

				string 발송일자1 = DateTime.Now.ToString("yyyyMM") + "05"; //K200
				string 발송일자2 = DateTime.Now.ToString("yyyyMM") + "01"; //K100
				string 발송일자3 = DateTime.Now.ToString("yyyyMM") + "20"; //K200
				string 발송일자4 = DateTime.Now.ToString("yyyyMM") + "15"; //K100
				string 발송일자5 = DateTime.Now.ToString("yyyyMM") + "15"; //S100

				if (Convert.ToInt32(발송일자1) < Convert.ToInt32(현재일자))
					발송일자1 = DateTime.Now.AddMonths(1).ToString("yyyyMM") + "05";
				if (Convert.ToInt32(발송일자2) < Convert.ToInt32(현재일자))
					발송일자2 = DateTime.Now.AddMonths(1).ToString("yyyyMM") + "01";
				if (Convert.ToInt32(발송일자3) < Convert.ToInt32(현재일자))
					발송일자3 = DateTime.Now.AddMonths(1).ToString("yyyyMM") + "20";
				if (Convert.ToInt32(발송일자4) < Convert.ToInt32(현재일자))
					발송일자4 = DateTime.Now.AddMonths(1).ToString("yyyyMM") + "15";
				if (Convert.ToInt32(발송일자5) < Convert.ToInt32(현재일자))
					발송일자5 = DateTime.Now.AddMonths(1).ToString("yyyyMM") + "15";

				string query = @"SELECT TOP 1 DT_CAL
							     FROM MA_CALENDAR
							     WHERE CD_COMPANY = 'K100'
							     AND DT_CAL < '{0}'
							     AND FG1_HOLIDAY = 'W'
							     ORDER BY DT_CAL DESC";

				DBConnection생성();
				DB접속();

				DataTable dt = GetDataTable(string.Format(query, 발송일자1));
				string 알림일자1 = dt.Rows[0]["DT_CAL"].ToString();
				dt = GetDataTable(string.Format(query, 발송일자2));
				string 알림일자2 = dt.Rows[0]["DT_CAL"].ToString();
				dt = GetDataTable(string.Format(query, 발송일자3));
				string 알림일자3 = dt.Rows[0]["DT_CAL"].ToString();
				dt = GetDataTable(string.Format(query, 발송일자4));
				string 알림일자4 = dt.Rows[0]["DT_CAL"].ToString();
				dt = GetDataTable(string.Format(query, 발송일자5));
				string 알림일자5 = dt.Rows[0]["DT_CAL"].ToString();

				ExecuteNonQuery("SP_CZ_SA_EWS_LOG_I", "K100");
				ExecuteNonQuery("SP_CZ_SA_EWS_LOG_I", "K200");
				ExecuteNonQuery("SP_CZ_SA_EWS_LOG_I", "S100");

				DB접속해제();

				WriteLog("현재일자 : " + 현재일자);
				WriteLog("발송일자1 : " + 발송일자1);
				WriteLog("발송일자2 : " + 발송일자2);
				WriteLog("발송일자3 : " + 발송일자3);
				WriteLog("발송일자4 : " + 발송일자4);
				WriteLog("발송일자5 : " + 발송일자5);
				WriteLog("알림일자1 : " + 알림일자1);
				WriteLog("알림일자2 : " + 알림일자2);
				WriteLog("알림일자3 : " + 알림일자3);
				WriteLog("알림일자4 : " + 알림일자4);
				WriteLog("알림일자5 : " + 알림일자5);

				메일발송("admin@dintec.co.kr", "khkim@dintec.co.kr", string.Empty, string.Empty, "미수금메일발송날짜알림", _log, string.Empty, true);

				if (현재일자 == 알림일자1)
				{
					_log = string.Empty;
					미수금메일발송전알림("K200", 발송일자1);
				}
				
				if (현재일자 == 발송일자1)
				{
					_log = string.Empty;
					미수금메일발송("K200");
				}
				
				if (현재일자 == 알림일자2)
				{
					_log = string.Empty;
					미수금메일발송전알림("K100", 발송일자2);
				}
				
				if (현재일자 == 발송일자2)
				{
					_log = string.Empty;
					미수금메일발송("K100");
				}
				
				if (현재일자 == 알림일자3)
				{
					_log = string.Empty;
					미수금메일발송전알림("K200", 발송일자3);
				}
				
				if (현재일자 == 발송일자3)
				{
					_log = string.Empty;
					미수금메일발송("K200");
				}
				
				if (현재일자 == 알림일자4)
				{
					_log = string.Empty;
					미수금메일발송전알림("K100", 발송일자4);
				}
				
				if (현재일자 == 발송일자4)
				{
					_log = string.Empty;
					미수금메일발송("K100");
				}
				
				if (현재일자 == 알림일자5)
				{
					_log = string.Empty;
					미수금메일발송전알림("S100", 발송일자5);
				}
				
				if (현재일자 == 발송일자5)
				{
					_log = string.Empty;
					미수금메일발송("S100");
				}
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

		public static void 미수금메일발송전알림(string 회사코드, string 발송예정일)
		{
			string 보내는사람, 받는사람, 숨은참조, 본문;
			string 대상매출처, 기준일자, 매출처코드, 매출처명, 메일주소;
			DataTable dt, dt1;

			try
			{
				보내는사람 = "admin@dintec.co.kr/관리자";

				if (회사코드 == "K100")
				{
					받는사람 = "sd@dintec.co.kr";
					숨은참조 = "erp@dintec.co.kr";
				}
				else if (회사코드 == "K200")
				{
					받는사람 = "notice@dubheco.com";
					숨은참조 = "erp@dintec.co.kr";
				}
				else if (회사코드 == "S100")
				{
					받는사람 = "service@dintec.com.sg";
					숨은참조 = "erp@dintec.co.kr";
				}
				else
				{
					return;
				}

				DBConnection생성();
				DB접속();

				dt = GetDataTable1("SP_CZ_SA_OUTSTANDING_INVOICE_S", 회사코드);

				DB접속해제();

				대상매출처 = string.Empty;

				foreach (DataRow dr in dt.Rows)
				{
					기준일자 = dr["DT_TODAY"].ToString();
					매출처코드 = dr["CD_PARTNER"].ToString();

					DBConnection생성();
					DB접속();

					dt1 = GetDataTable(@"SELECT MP.LN_PARTNER,
												MP.CD_AREA,
												FP.NM_EMAIL,
												FP.YN_OUTSTANDING_INV 
										 FROM MA_PARTNER MP WITH(NOLOCK)
										 LEFT JOIN FI_PARTNERPTR FP WITH(NOLOCK) ON FP.CD_COMPANY = MP.CD_COMPANY AND FP.CD_PARTNER = MP.CD_PARTNER
										 WHERE MP.CD_COMPANY = '" + 회사코드 + "'" + Environment.NewLine +
										"AND MP.CD_PARTNER = '" + 매출처코드 + "'");

					DB접속해제();

					매출처명 = dt1.Rows[0]["LN_PARTNER"].ToString();

					if (dt1.Rows[0]["CD_AREA"].ToString() == string.Empty)
					{
						대상매출처 += 매출처코드 + " : 해당 매출처에 지역구분이 지정되어 있지 않습니다." + Environment.NewLine;
						continue;
					}

					메일주소 = string.Empty;

					foreach (DataRow dr1 in dt1.Select("YN_OUTSTANDING_INV = 'Y'"))
					{
						메일주소 += dr1["NM_EMAIL"].ToString() + ";";
					}

					if (string.IsNullOrEmpty(메일주소))
					{
						대상매출처 += 매출처코드 + " : 해당 매출처에 미수금 관리 담당자가 지정되어 있지 않습니다." + Environment.NewLine;
						continue;
					}

					DBConnection생성();
					DB접속();

					dt1 = GetDataTable(@"SELECT 1
									     FROM SA_IVH IH WITH(NOLOCK)
									     LEFT JOIN MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = IH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = IH.CD_EXCH
									     WHERE IH.CD_COMPANY = '" + 회사코드 + "'" + Environment.NewLine +
									    "AND IH.CD_PARTNER = '" + 매출처코드 + "'" + Environment.NewLine +
									    "AND IH.DT_PROCESS <= CONVERT(CHAR(8), DATEADD(DAY, -7, '" + 기준일자 + "'), 112)" + Environment.NewLine +
									   @"AND (ISNULL(IH.AM_K, 0) + ISNULL(IH.VAT_TAX, 0)) > ISNULL(IH.AM_BAN, 0)
									     ORDER BY IH.DT_PROCESS ASC");

					if (dt1 == null || dt1.Rows.Count == 0)
					{
						대상매출처 += 매출처코드 + " : 미수금메일 발송대상 매출처이나, 기준일자 - 7일 이전의 미수금이 없습니다." + Environment.NewLine;
						continue;
					}

					DB접속해제();

					대상매출처 += 매출처코드 + " : " + 메일주소 + Environment.NewLine;
				}

				본문 = @"Payment Reminder Letter가 " + 발송예정일.Substring(0, 4) + "." + 발송예정일.Substring(4, 2) + "." + 발송예정일.Substring(6, 2) + @"일 발송될 예정입니다.  

조기경보시스템(EWS) – 미수채권 메뉴를 통해 발송대상 거래처 확인 및 적정성 검토 바랍니다.
거래처정보관리 메뉴에서 신규 발송 거래처의 메일 수신처 등록 및 적정성 검토 바랍니다.

미수금 메일 발송 대상 리스트 입니다."  + Environment.NewLine + 
Environment.NewLine +
대상매출처 + @"

감사합니다.";

				메일발송(보내는사람, 받는사람, string.Empty, 숨은참조, "[사전공지] Payment Reminder Letter 발송 사전 알림", 본문, string.Empty, true);
			}
			catch (Exception ex)
			{
				WriteLog(ex.ToString());
			}
		}

		public static void 미수금메일발송(string 회사코드)
		{
			string 보내는사람, 받는사람, 숨은참조, 제목, 내용, html, 미수금;
			string 매출처코드, 매출처명, 기준일자;
			DataTable dt, dt1;

			try
			{
				DBConnection생성();
				DB접속();

				dt = GetDataTable1("SP_CZ_SA_OUTSTANDING_INVOICE_S", 회사코드);

				DB접속해제();

				WriteLog("총 건수 : " + dt.Rows.Count.ToString() + "건");

				if (회사코드 == "K100")
				{
					보내는사람 = "service@dintec.co.kr/DINTEC CO., LTD.";
					숨은참조 = "sd@dintec.co.kr";
				}
				else if (회사코드 == "K200")
				{
					보내는사람 = "service@dubheco.com/DUBHECO CO., LTD.";
                    숨은참조 = "notice@dubheco.com";
				}
				else if (회사코드 == "S100")
				{
					보내는사람 = "service@dintec.com.sg/DINTEC SINGAPORE PTE. LTD.";
                    숨은참조 = "service@dintec.com.sg";
				}
				else
				{
					return;
				}

				foreach (DataRow dr in dt.Rows)
				{
					매출처코드 = dr["CD_PARTNER"].ToString();
					기준일자 = dr["DT_TODAY"].ToString();

					DBConnection생성();
					DB접속();

					dt1 = GetDataTable(@"SELECT MP.LN_PARTNER,
												MP.CD_AREA,
												FP.NM_EMAIL,
												FP.YN_OUTSTANDING_INV 
										 FROM MA_PARTNER MP WITH(NOLOCK)
										 LEFT JOIN FI_PARTNERPTR FP WITH(NOLOCK) ON FP.CD_COMPANY = MP.CD_COMPANY AND FP.CD_PARTNER = MP.CD_PARTNER
										 WHERE MP.CD_COMPANY = '" + 회사코드 + "'" + Environment.NewLine +
										"AND MP.CD_PARTNER = '" + 매출처코드 + "'");

					DB접속해제();

					매출처명 = dt1.Rows[0]["LN_PARTNER"].ToString().Replace("(사용불가)", "").Replace("(사용중지)", "");

					if (회사코드 == "K200")
						제목 = "Account Alert: Payment reminder for overdue invoice (Dubhe co.,ltd)";
					else
						제목 = "Account Alert: Payment reminder for overdue invoice";

					if (dt1.Rows[0]["CD_AREA"].ToString() == "200") //해외
					{
						if (회사코드 == "K100")
						{
							내용 = 메일문구(회사코드, dr["WN_LEVEL"].ToString(), 매출처명, true);
							미수금 = 미수금내역(회사코드, 매출처코드, 기준일자, true);
							html = 내용 + 미수금;
						}
						else if (회사코드 == "K200")
						{
							내용 = 메일문구(회사코드, dr["WN_LEVEL"].ToString(), 매출처명, true);
							미수금 = 미수금내역(회사코드, 매출처코드, 기준일자, true);
							html = 내용 + 미수금;
						}
						else if (회사코드 == "S100")
						{
							내용 = 메일문구(회사코드, dr["WN_LEVEL"].ToString(), 매출처명, true);
							미수금 = 미수금내역(회사코드, 매출처코드, 기준일자, true);
							html = 내용 + 미수금;
						}
						else
						{
							continue;
						}
					}
					else if (dt1.Rows[0]["CD_AREA"].ToString() == "100") //국내
					{
						if (회사코드 == "K100")
						{
							내용 = 메일문구(회사코드, dr["WN_LEVEL"].ToString(), 매출처명, true);
							미수금 = 미수금내역(회사코드, 매출처코드, 기준일자, true);
							html = 내용 + 미수금;
						}
						else if (회사코드 == "K200")
						{
							내용 = 메일문구(회사코드, dr["WN_LEVEL"].ToString(), 매출처명, true);
							미수금 = 미수금내역(회사코드, 매출처코드, 기준일자, true);
							html = 내용 + 미수금;
						}
						else if (회사코드 == "S100")
						{
							내용 = 메일문구(회사코드, dr["WN_LEVEL"].ToString(), 매출처명, true);
							미수금 = 미수금내역(회사코드, 매출처코드, 기준일자, true);
							html = 내용 + 미수금;
						}
						else
						{
							continue;
						}
					}
					else
					{
						WriteLog(매출처코드 + "(" + 회사코드 + ") : " + "해당 매출처에 지역구분이 지정되어 있지 않습니다.");
						continue;
					}

					if (string.IsNullOrEmpty(미수금))
					{
						WriteLog(매출처코드 + "(" + 회사코드 + ") : " + "미수금메일 발송대상 매출처이나, 기준일자 - 7일 이전의 미수금이 없습니다.");
						continue;
					}

					받는사람 = string.Empty;

					foreach (DataRow dr1 in dt1.Select("YN_OUTSTANDING_INV = 'Y'"))
					{
						받는사람 = 받는사람 + dr1["NM_EMAIL"].ToString() + ";";
					}

					if (string.IsNullOrEmpty(받는사람))
					{
						WriteLog(매출처코드 + "(" + 회사코드 + ") : " + "해당 매출처에 미수금 관리 담당자가 지정되어 있지 않습니다.");
						continue;
					}

					if (메일발송(보내는사람, 받는사람, string.Empty, 숨은참조, 제목, string.Empty, html, true) == true)
						WriteLog(매출처코드 + "(" + 회사코드 + ") : " + "발송완료 : " + 받는사람);
					else
						WriteLog(매출처코드 + "(" + 회사코드 + ") : " + "발송실패 : " + 받는사람);
				}

				WriteLog("완료");

				메일발송(보내는사람, "erp@dintec.co.kr", string.Empty, 숨은참조, "미수금메일발송결과", _log, string.Empty, true);
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

		private static string 미수금내역(string 회사코드, string 매출처코드, string 기준일자, bool 영문여부)
		{
			string html = string.Empty, color, query;
			DataTable dt;

			try
			{
				DBConnection생성();
				DB접속();

                query = @"SELECT IH.NO_IV,
								 STUFF((SELECT DISTINCT ',' + IL.NO_SO
								 	    FROM SA_IVL IL WITH(NOLOCK)
								 	    WHERE IL.CD_COMPANY = IH.CD_COMPANY
								 	    AND IL.NO_IV = IH.NO_IV
								 	    FOR XML PATH('')),1,1,'') AS NO_SO,
								 STUFF((SELECT DISTINCT ',' + SH.NO_PO_PARTNER
								 	    FROM SA_IVL IL WITH(NOLOCK)
								 	    LEFT JOIN SA_SOH SH WITH(NOLOCK) ON SH.CD_COMPANY = IL.CD_COMPANY AND SH.NO_SO = IL.NO_SO
								 	    WHERE IL.CD_COMPANY = IH.CD_COMPANY
								 	    AND IL.NO_IV = IH.NO_IV
								 	    FOR XML PATH('')),1,1,'') AS NO_PO_PARTNER, 
								 STUFF((SELECT DISTINCT ',' + MH.NM_VESSEL
								 	    FROM SA_IVL IL WITH(NOLOCK)
								 	    LEFT JOIN SA_SOH SH WITH(NOLOCK) ON SH.CD_COMPANY = IL.CD_COMPANY AND SH.NO_SO = IL.NO_SO
								 	    LEFT JOIN CZ_MA_HULL MH WITH(NOLOCK) ON MH.NO_IMO = SH.NO_IMO
								 	    WHERE IL.CD_COMPANY = IH.CD_COMPANY
								 	    AND IL.NO_IV = IH.NO_IV
								 	    FOR XML PATH('')),1,1,'') AS NM_VESSEL,
								 IH.DT_PROCESS,
								 DATEDIFF(DAY, IH.DT_PROCESS, '" + 기준일자 + @"') AS DT_DIFF,
								 MC.NM_SYSDEF AS NM_EXCH,
								 (ISNULL(IH.AM_EX, 0) - ISNULL(IH.AM_BAN_EX, 0)) AS AM_EX_REMAIN,
								 (ISNULL(IH.AM_K, 0) + ISNULL(IH.VAT_TAX, 0)) - ISNULL(IH.AM_BAN, 0) AS AM_REMAIN
						  FROM SA_IVH IH WITH(NOLOCK)
						  LEFT JOIN MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = IH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = IH.CD_EXCH
						  WHERE IH.CD_COMPANY = '" + 회사코드 + "'" + Environment.NewLine +
                         "AND IH.CD_PARTNER = '" + 매출처코드 + "'" + Environment.NewLine +
                         "AND IH.DT_PROCESS <= CONVERT(CHAR(8), DATEADD(DAY, -7, '" + 기준일자 + "'), 112)" + Environment.NewLine +
                        @"AND (ISNULL(IH.AM_K, 0) + ISNULL(IH.VAT_TAX, 0)) > ISNULL(IH.AM_BAN, 0)
						  ORDER BY IH.DT_PROCESS ASC";

                dt = GetDataTable(query);

				DB접속해제();

				if (dt != null && dt.Rows.Count > 0)
				{
					if (영문여부)
					{
						html = @"<br/>
								 <div style='text-align:left; font-weight: bold;'>*** Outstanding Invoice List</div>

							 <table style='width:1000px; border:2px solid black; font-size: 9pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;'>
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
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Invoice Date</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Overdue Day</th>                                    
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Invoice No</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Our Ref</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Your PO</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Vessel Name</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Currency</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Amount</th>
								</tr>";
					}
					else
					{
						html = @"<br/>
								 <div style='text-align:left; font-weight: bold;'>*** 미수금 내역</div>

							 <table style='width:1000px; border:2px solid black; font-size: 9pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;'>
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
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>발행일자</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>경과일수</th>                                    
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>계산서번호</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>수주번호</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>발주번호</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>호선명</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>통화</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>금액</th>
								</tr>";
					}

					foreach (DataRow dr in dt.Rows)
					{
						if (Convert.ToInt32(dr["DT_DIFF"]) >= 90)
							color = "color: #FF0000;";
						else if (Convert.ToInt32(dr["DT_DIFF"]) >= 45)
							color = "color: #0000FF;";
						else
							color = "color: #000000;";

						if (dr["NM_EXCH"].ToString() == "KRW")
						{
							html += @"<tr style='height:30px'>
										 <th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + GetTo_DateStringS(dr["DT_PROCESS"]) + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + dr["DT_DIFF"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_IV"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_SO"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_PO_PARTNER"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NM_VESSEL"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + dr["NM_EXCH"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:right; padding-right:10px; font-weight:bold'>" + GetTO_Money(dr["AM_REMAIN"]) + "</th>" + Environment.NewLine +
									 "</tr>";
						}
						else
						{
							html += @"<tr style='height:30px'>
										 <th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + GetTo_DateStringS(dr["DT_PROCESS"]) + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + dr["DT_DIFF"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_IV"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_SO"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_PO_PARTNER"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NM_VESSEL"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + dr["NM_EXCH"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:right; padding-right:10px; font-weight:bold'>" + GetTO_Money(dr["AM_EX_REMAIN"]) + "</th>" + Environment.NewLine +
									 "</tr>";
						}
					}

					html += @"</tbody>
					  </table>";
				}
				else
					html = string.Empty;
			}
			catch (Exception ex)
			{
				WriteLog(ex.ToString());
			}
			finally
			{
				DB접속해제();
			}

			return html;
		}

		public static string 메일문구(string 회사코드, string level, string 매출처명, bool 영문여부)
		{
			string 문구;

			try
			{
				if (level == "2") //사용불가
				{
					#region 사용불가
					if (영문여부)
					{
						if (회사코드 == "K100")
						{
							문구 = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
Dear " + 매출처명 + @",<br/><br/>
First of all, we would like to express the appreciation for the good cooperation and business with us.<br/><br/>
As most companies are already implementing Risk Management, we also have introduced an Early Warning System as a part of Risk Management.<br/><br/>
To provide the best service to our customers is what we are the most focusing on.
<span style='color: #FF0000;'>
But we are unable to provide any of our services such as quotation, order and delivery at the present because the amount of your long-term outstanding invoices exceeds the range we can afford.
</span><br/><br/>
Currently, your outstanding invoice is as followings. We would like you to settle overdue invoices as soon as possible and we hope that we can keep supporting your good company. It will be appreciated to inform us the expected payment plan.
</div>";
						}
						else if (회사코드 == "S100")
						{
							문구 = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
Dear " + 매출처명 + @",<br/><br/>
This is Dintec Singapore and We would like to remind you to settle your account balance with us immediately.<br/><br/>
We are attaching our files on the following unsettled accounts and order your collection of same.<br/><br/>
As a result of credit check by our CMS(Credit Management System), your order has been suspended and our quotation service being interrupted.<br/><br/>
Please kindly understand our situation and your prompt payment of these invoices will be greatly appreciated.
</div>";
						}
						else
						{
							문구 = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
Dear " + 매출처명 + @",<br/><br/>
This is to remind you to settle your account balance with us immediately.<br/>
We are attaching our files on the following unsettled accounts and order your collection of same.<br/>
As a result of credit check by our CMS(Credit Management System), 
<span style='color: #FF0000;'>your order has been suspended and our quotation service being interrupted</span><br/>
Please kindly understand our situation and your prompt payment of these invoices will be greatly appreciated.
</div>";
						}
					}
					else
					{
						문구 = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
수신 : " + 매출처명 + @" 담당자 님<br/><br/>
귀사의 일익 번창을 기원합니다.<br/><br/>
아래의 세금계산서(인보이스)에 대하여 물품대금 지급일이 이미 경과하였음에도 불구하고 미수금 상환이 이루어지지 않고 있으며,<br/><br/>
당사의 위험관리 일환인 조기경보시스템에 따라 아래와 같이 미수금 리스트를 송부 드립니다.<br/><br/>
미수채권의 지급이 이행되지 않을 경우 부득이 귀사를 대상으로 견적, 수주 및 물품공급 등의 서비스를 제공할 수 없사오니<br/><br/>
귀사에 대한 지속적인 최상의 서비스를 제공할 수 있도록 부디 미수금의 지불 또는 계획에 대하여 회신 부탁 드립니다.
</div>";
					}
					#endregion
				}
				else
				{
					#region 주의요망
					if (영문여부)
					{
						if (회사코드 == "K100")
						{
							문구 = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
Dear " + 매출처명 + @",<br/><br/>
First of all, we would like to express the appreciation for the good cooperation and business with us.<br/><br/>
As most companies are already implementing Risk Management, we also have introduced an Early Warning System as a part of Risk Management.<br/><br/>
To provide the best service to our customers is what we are the most focusing on.<br/>
But, there may be a case where our service provision is restricted because of long-term outstanding invoices caused by missing invoices and the lack of management of invoices between each other.<br/>
<span style='font-size: 9pt; font-family: 맑은 고딕; color: #0000FF;'>
The main purpose of “Early Warning System” is in order to complement problems and prevent any restrictions on providing our good service.
</span><br/><br/>
Currently, your outstanding invoice is as followings. We would like you to settle overdue invoices as soon as possible. It will be appreciated to inform us the expected payment plan.<br/><br/>
Please also kindly note that our basic services such as quotation, order and delivery can be restricted, if outstanding invoices is not settled longer or increase.
</div>";
						}
						else if (회사코드 == "S100")
						{
							문구 = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
Dear " + 매출처명 + @",<br/><br/>
This is Dintec Singapore and we would like to express the appreciation for the good cooperation and business with us!<br/><br/>
As most companies are already implementing Risk Management, we also have introduced an Early Warning System as a part of Risk Management.<br/><br/>
Currently, your outstanding invoice is as followings. We would like you to settle overdue invoices as per the terms of payment. It will be appreciated to inform us the expected payment plan.<br/><br/>
Please also kindly note that our basic services such as quotation, order and delivery can be restricted, if outstanding invoices is not settled longer or increase.
</div>";
						}
						else
						{
							문구 = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
Dear " + 매출처명 + @",<br/><br/>
This is to remind you to settle your account balance with us immediately.<br/>
We are attaching our files on the following unsettled accounts and order your collection of same.<br/>
If the current outstanding amounts is maintained
<span style='text-decoration: underline; color: #0000FF'>
your inquiries and orders will be interrupted/delayed
</span> 
as a result of credit check by our CMS(Credit Management System)<br/>
Please kindly understand our situation and your prompt payment of these invoices will be greatly appreciated.
</div>";
						}
					}
					else
					{
						문구 = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
수신 : " + 매출처명 + @" 담당자 님<br/><br/>
귀사의 일익 번창을 기원합니다.<br/><br/>
아래의 세금계산서(인보이스)에 대하여 물품대금 지급일이 이미 경과하였음에도 불구하고 미수금 상환이 이루어지지 않고 있으며,<br/><br/>
당사의 위험관리 일환인 조기경보시스템에 따라 아래와 같이 미수금 리스트를 송부 드립니다.<br/><br/>
미수채권의 지급이 이행되지 않을 경우 부득이 귀사를 대상으로 견적, 수주 및 물품공급 등의 서비스에 제한이 될 수 있사오니<br/><br/>
귀사에 대한 지속적인 최상의 서비스를 제공할 수 있도록 부디 미수금의 지불 계획에 대하여 회신 부탁 드립니다.
</div>";
					}
					#endregion
				}

				return 문구;
			}
			catch (Exception ex)
			{
				WriteLog(ex.ToString());
			}

			return string.Empty;
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

		public static int ExecuteNonQuery(string procedure, string param)
		{
			_sqlCommand = new SqlCommand();
			_sqlCommand.CommandText = procedure;
			_sqlCommand.CommandTimeout = 300;
			_sqlCommand.CommandType = CommandType.StoredProcedure;
			_sqlCommand.Connection = _sqlConnetion;

			_sqlCommand.Parameters.Add(new SqlParameter("@P_CD_COMPANY", param));

			int result = _sqlCommand.ExecuteNonQuery();

			return result;
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

				filePath = "C:\\OutstandingInvoice\\log\\" + fileName + ".txt";

				if (!Directory.Exists("C:\\OutstandingInvoice\\log"))
				{
					Directory.CreateDirectory("C:\\OutstandingInvoice\\log");
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

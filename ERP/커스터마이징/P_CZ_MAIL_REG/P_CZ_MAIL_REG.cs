using Aspose.Email.Outlook;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net.Mail;
using Parsing;
using System.Xml;
using Dintec.DHL;
using System.Windows.Threading;
using System.Diagnostics;
using System.Drawing;
using DX;
using System.Linq;
using System.Globalization;

namespace cz
{
	public partial class P_CZ_MAIL_REG : PageBase
	{
		readonly DispatcherTimer timer1;
		string Printer = string.Empty;
		string loginID = Global.MainFrame.LoginInfo.EmployeeNo.ToString();


		#region ==================== 선언
		Outlook.MailItem mailitem = null;
		Outlook.Application app = null;
		Outlook._NameSpace ns = null;
		Outlook.MAPIFolder inboxFolder = null;
		Outlook.MAPIFolder moveFolder = null;
		Outlook.MAPIFolder uFolder = null;
		Outlook.MAPIFolder unClass = null;
		Outlook.MAPIFolder uploadFolder = null;
		Outlook.MAPIFolder backupFolder = null;
		Outlook.Items MailItems;


		Outlook.MAPIFolder dumoveFolder = null;
		Outlook.MAPIFolder duunClass = null;
		Outlook.MAPIFolder duuploadFolder = null;
		Outlook.MAPIFolder dubackupFolder = null;


		Outlook.MAPIFolder invoiceFolder = null;
		Outlook.MAPIFolder invoiceFolderdu = null;

		Outlook.MAPIFolder receiptFolder = null;
		Outlook.MAPIFolder receiptFolderdu = null;


		Outlook.MAPIFolder MDSDFolder = null;
		Outlook.MAPIFolder MDSDFolderdu = null;


		Outlook.MAPIFolder FWFolder = null;
		Outlook.MAPIFolder FWFolderdu = null;

		Outlook.MAPIFolder SKFolder = null;

		AutoInquiry inquiryMailUp = new AutoInquiry();
		JiBe jibe = new JiBe();
		LogWrite log = new LogWrite();

		delegate void TimerMailEvent();


		#region 원격 서버 접속
		// 구조체 선언
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct NETRESOURCE
		{
			public uint dwScope;
			public uint dwType;
			public uint dwDisplayType;
			public uint dwUsage;
			public string lpLocalName;
			public string lpRemoteName;
			public string lpComment;
			public string lpProvider;
		}

		// API 함수 선언
		[DllImport("mpr.dll", CharSet = CharSet.Auto)]
		public static extern int WNetUseConnection(
					IntPtr hwndOwner,
					[MarshalAs(UnmanagedType.Struct)] ref NETRESOURCE lpNetResource,
					string lpPassword,
					string lpUserID,
					uint dwFlags,
					StringBuilder lpAccessName,
					ref int lpBufferSize,
					out uint lpResult);

		// API 함수 선언 (공유해제)
		[DllImport("mpr.dll", EntryPoint = "WNetCancelConnection2", CharSet = CharSet.Auto)]
		public static extern int WNetCancelConnection2A(string lpName, int dwFlags, int fForce);
		#endregion 원격 서버 접속

		public int itemCount = 0;

		DataTable dbDatatbResult;
		DataTable sortTable;

		public string CD_COMPANY = string.Empty;
		public string CATEGORY = string.Empty;
		public string CODE = string.Empty;
		public string MAIL_FROM = string.Empty;
		public string MAIL_FROM_DB_S = string.Empty;
		public string MAIL_TO_DB_S = string.Empty; // to + cc
		public string MAIL_TO_RESULT = string.Empty;
		public string NM_KOR = string.Empty;
		public string NO_EMP = string.Empty;
		public string CD_TEAM = string.Empty;
		public string NM_PARTER = string.Empty;
		public string CD_PARTNER = string.Empty;
		public string SUBJECT_KEY = string.Empty;
		public string SUBJECT_DEL = string.Empty;
		public string BODY_KEY = string.Empty;
		public string BODY_DEL = string.Empty;
		public string YN_USE = string.Empty;
		public string YN_CLOUDOC = string.Empty;
		public string YN_MAIL = string.Empty;
		public string YN_INQINSERT = string.Empty;
		public string RECEIVE_MAIL = string.Empty;
		public string RECEIVE_MAIL2 = string.Empty;
		public string ETC2 = string.Empty;
		public string DC_RMK = string.Empty;
		public string ID_INSERT = string.Empty;
		public string DTS_INSERT = string.Empty;
		public string ID_UPDATE = string.Empty;
		public string DTS_UPDATE = string.Empty;
		public string NO_MAIL = string.Empty;
		public string NO_EMAIL = string.Empty;

		public string DELIVERYDT = string.Empty;
		public string MAIL_SUBJECT = string.Empty;
		public string MAIL_BODY = string.Empty;

		public string NO_IMO = string.Empty;
		public string MAIL_TO = string.Empty;
		public string MAIL_CC = string.Empty;
		public string MAIL_BCC = string.Empty;

		public string NO_PREREG = string.Empty;

		public string AFTER_MAIL_NAME = string.Empty;

		string fileName = string.Empty;

		string FILE_NO = string.Empty;
		string FILE_SUPPLY = string.Empty;

		bool startSig = false;

		private string mailKey = string.Empty;
		private string msgId = string.Empty;
		private string folderId = string.Empty;

		private string cd_send = string.Empty;
		private string errorMsg = string.Empty;

		public string msgStr = string.Empty;

		private bool ingSig = false;

		public string doubleCompany = string.Empty;

		public string resultCode = string.Empty;

		public bool InqCheck = false;
		public bool UpCheck = false;


		// 자이브 용 
		string requisition_code = string.Empty;
		string client_uid = string.Empty;
		string supplier_uid = string.Empty;
		string quotation_code = string.Empty;
		string quotation_due_date = string.Empty;
		string priority = string.Empty;
		string delivery_remarks = string.Empty;
		string vessel_name = string.Empty;
		string total_items = string.Empty;
		string purchaser_name = string.Empty;
		string function = string.Empty;
		string system = string.Empty;
		string model = string.Empty;
		string particulars = string.Empty;
		string maker = string.Empty;
		string delivery_data = string.Empty;
		string delivery_port = string.Empty;
		string contact_number = string.Empty;
		string purchaser_remarks = string.Empty;
		string currency = string.Empty;
		string vat = string.Empty;
		string document_type = string.Empty;
		string rfq_attachments = string.Empty;
		string supplier_name = string.Empty;
		string issued_by = string.Empty;
		string port = string.Empty;
		string purchaser_email = string.Empty;
		string imo = string.Empty;

		string item_serial_number = string.Empty;
		string drawing_number = string.Empty;
		string part_number = string.Empty;
		string description = string.Empty;
		string detail_description = string.Empty;
		string unit = string.Empty;
		string requested_quantity = string.Empty;




		#endregion ==================== 선언

		#region ==================== 설정
		public P_CZ_MAIL_REG()
		{
			StartUp.Certify(this);
			InitializeComponent();

			timer1 = new DispatcherTimer();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();


			//if (loginID.Equals("SYSADMIN"))
			//{
			//	bpPanelControl9.Visible = false;
			//	Btn시작_Click(null, null);
			//}
		}


		private void InitGrid()
		{
			tbx받은폴더.Text = "Service";
			tbx이동폴더.Text = "백업";
			tbx미분류.Text = "미분류";
			tbx보관.Text = "보관";
			tbxUPload.Text = "업로드";
		}

		private void InitEvent()
		{
			btn시작.Click += Btn시작_Click;
			btn중지.Click += Btn중지_Click;
			btn로그저장.Click += Btn로그저장_Click;

			btnClear.Click += BtnClear_Click;

			btnPullRequest.Click += btnPullRequest_Click;
			btnStatus.Click += BtnStatus_Click;
			btnQuotation.Click += btnQuotation_Click;
			btnPULLPO.Click += BtnPULLPO_Click;

			docTest.Click += DocTest_Click;


			btnTCRS.Click += BtnTCRS_Click;

			timer1.Interval = new TimeSpan(0, 0, 1);
			timer1.Tick += Timer1_Tick;
			timer1.Start();
		}



		#endregion ==================== 설정



		private void Timer1_Tick(object sender, EventArgs e)
		{
			MailReg();
		}


		#region ▣ 메일 분류
		private void MailReg()
		{
			try
			{
				while (startSig)
				{
					string dateNowStr = DateTime.Now.ToString("mm");

					// NO_TEST 업데이트
					//if (dateNowStr.Equals("00") || dateNowStr.Equals("01"))
					//{
					//	1-UpdateQuery();
					//}


					// 자이브 10분에 한번씩
					if (dateNowStr.EndsWith("0"))
					{
						btnPullRequest_Click(null, null);
					}



					if (ingSig)
					{
						ClearDataStart();

						ingSig = false;

						itemCount += 1;

						int mailCountCheck = uFolder.Items.Count;


						if (mailCountCheck != 0)
						{
							if (itemCount > mailCountCheck)
								itemCount = 1;


							if (uFolder.Items[itemCount] != null)
							{
								if (uFolder.Items[itemCount] is Outlook.MailItem)
									mailitem = (Outlook.MailItem)uFolder.Items[itemCount];
								else
									uFolder.Items[itemCount].Move(unClass);
							}
							else
								Btn시작_Click(null, null);


							if (mailitem != null)
							{
								fileName = FileLocalSave(mailitem);
								fileName = fileName + ".msg";

								// 시작
								MailSort(fileName);
							}
						}
						else
						{
							itemCount = 0;
							startSig = false;
							ClearData();
							break;
						}

						ingSig = true;
					}
					else
					{
						startSig = false;
					}

					//  로그저장 및 삭제
					if (txtLog.Text.Length > 500000)
					{
						txtLog.SelectionColor = Color.Black;

						log.WriteText(txtLog.Text);
						txtLog.Text = string.Empty;

						LogWrite2("로그저장");
					}
				}
			}
			catch (Exception ex)
			{
				errorMsg = errorMsg + ex.Message;
				if (!ex.ToString().Contains("'System.__ComObject'"))
				{
					MailMoveFolderUn();
					DBInsert(NO_EMAIL, msgId, folderId, cd_send, "N", errorMsg, msgStr);
				}

				Messenger.SendMSG(new string[] { "S-458" }, "메일분류 오류!!!!\r\n" + errorMsg);

				LogWrite2(ex + " / " + MAIL_SUBJECT);
				
				startSig = false;
				
				ClearData();
			}
		}




		private void MailSort(string _filename)
		{
			fileName = _filename;

			MapiMessage msg = MapiMessage.FromFile(fileName);

			DELIVERYDT = msg.DeliveryTime.ToString("yyyyMMddHHmmss");             // 받은시간
			if (mailitem.Subject != null)
				MAIL_SUBJECT = mailitem.Subject.ToString().Trim();                    // 제목
			if (mailitem.SenderEmailAddress != null)
				MAIL_FROM_DB_S = mailitem.SenderEmailAddress.ToLower().ToString().Trim();  // 보낸사람


			if (!string.IsNullOrEmpty(mailitem.Body) && mailitem.Body != null)
				MAIL_BODY = mailitem.Body.ToString();                                 // 본문내용



			MailToSearch mts = new MailToSearch();
			// TO SEARCH
			MAIL_TO = mts.mailToSearch(msg, mailitem, MAIL_TO);
			//mailToSearch(msg);

			// CC SEARCH
			MAIL_CC = mts.mailCCSearch(msg, mailitem, MAIL_CC);
			//mailCCSearch(msg);

			// TO, CC 정리
			mailToCcSearch(msg);

			// 메일 DB 검색
			mailDBSearch();


			if (MAIL_FROM_DB_S.ToLower().Equals("teamadmin@dintec.co.kr"))
			{
				// teamadmin이 보낸 메일은 무조건 이동
				MailMoveFolderUn();
			}
			else
			{
				// 원본 테이블로 VALUE 가져오고
				if (dbDatatbResult != null && dbDatatbResult.Rows.Count > 0)
				{
					// 분류 안 된 팀메일 찾기
					MAIL_TO_RESULT = MAIL_TO_DB_S;

					for (int r = 0; r < dbDatatbResult.Rows.Count; r++)
					{
						MAIL_TO_RESULT = MAIL_TO_RESULT.Replace(dbDatatbResult.Rows[r]["value"].ToString(), "");
					}

					string mailCCStr = MAIL_TO_RESULT;

					mailCCStr = mailCCStr.Replace(";", "','").Replace(" ", "");
					mailCCStr = mailCCStr.Replace(",", "','").Trim();
					mailCCStr = mailCCStr.Replace("''", "'").Trim();

					DataTable isTeamMailYN = DBTeamMailSelectDT(mailCCStr);


					for (int c = 0; c < isTeamMailYN.Rows.Count; c++)
					{
						RECEIVE_MAIL2 += isTeamMailYN.Rows[c]["CD_FLAG2"].ToString().Replace("\n", "").Replace("\r", "").Trim() + ";";
					}


					// 테이블에서 중복 삭제하기
					string[] columnNames = { "NO_EMAIL", "CODE", "CD_COMPANY" };

					DataTable distinctTable = dbDatatbResult.AsEnumerable()
		   .GroupBy(row => string.Join("|", columnNames.Select(column => row.Field<string>(column))))
		   .Select(group => group.First())
		   .CopyToDataTable();


					sortTable = distinctTable;
				}


				resultCode = string.Empty;
				UpCheck = false;
				InqCheck = false;

				string 최종분류코드 = string.Empty;


				// 분류값 있을 때
				if (sortTable != null && sortTable.Rows.Count > 0)
				{
					// 수신인 만큼 발신 하기
					for (int dtR = 0; dtR < sortTable.Rows.Count; dtR++)
					{
						// 값 담기
						MailSortMain(msg, dtR);

						if (!resultCode.Equals("BACKUP"))
						{
							최종분류코드 = resultCode;
						}

						// Upload 완료시에 끝.
						if (CODE.StartsWith("UPLOAD"))
							break;


					}
				}
				// 분류값 없을 때
				else
				{
					MailSortMain(msg, 0);
				}

				if (!string.IsNullOrEmpty(최종분류코드))
					resultCode = 최종분류코드;


				// 처리 다 된 메일 이동하기
				if (resultCode.Equals("BACKUP"))
				{
					MailMoveFolderMo();
				}
				else if (resultCode.Equals("INVOICE") && CD_COMPANY.Equals("K200"))
				{
					MailMoveFolderInvoice_K200();
				}
				else if (resultCode.Equals("INVOICE"))
				{
					MailMoveFolderInvoice();
				}
				else if (resultCode.Equals("RECEIPT") && CD_COMPANY.Equals("K200"))
				{
					MailMoveFolderReceipt_K200();
				}
				else if (resultCode.Equals("RECEIPT"))
				{
					MailMoveFolderReceipt();
				}
				else if (resultCode.Equals("MDSD") && CD_COMPANY.Equals("K200"))
				{
					FileServerSaveMDSD_K200(mailitem);
					MailMoveFolderMDSD_K200();
				}
				else if (resultCode.Equals("MDSD"))
				{
					FileServerSaveMDSD_K100(mailitem);
					MailMoveFolderMDSD();
				}
				else if (resultCode.Equals("FW"))
				{
					FileServerSaveFORWARDER_K100(mailitem);
					MailMoveFolderFW();
				}
				else if (resultCode.Equals("SKLOGIN"))
				{
					FileServerSaveSKLOGIN(mailitem);
					MailMoveFolderSKLOGIN();
				}
				else if (resultCode.Equals("REPLY"))
				{
					MailMoveFolderRE();
				}

				ClearData();
			}
		}


		public void MailSortMain(MapiMessage msg, int dtR)
		{
			if (sortTable != null && sortTable.Rows.Count > 0)
				SetData(sortTable, dtR);
			else
				NO_EMAIL = string.Empty;


			mailKey = msg.Headers["Message-ID"];                                        // 아웃룩 <-> 서버 연동 키

			//mailKey = msg.Headers["X-MC-MsgID"];			// 20191031 수정
			//msgId = msg.Headers["X-MC-MsgID"];          // 20191031 수정

			if (string.IsNullOrEmpty(mailKey))
				errorMsg = errorMsg + "메일키 누락";

			DataTable dtmsgID = null;

			if (!string.IsNullOrEmpty(mailKey))
			{
				dtmsgID = DBMailBoxKeySelect(mailKey);

				if (dtmsgID.Rows.Count == 1)
					msgId = dtmsgID.Rows[0]["mm_id"].ToString().Trim().ToUpper();
			}
			else
			{
				msgId = msg.Headers["X-MC-MsgID"];
			}

			if (CODE.Equals("DUBHE_ETC"))
			{
				MAIL_TO = RECEIVE_MAIL;
				CD_COMPANY = "K200";
			}



			RECEIVE_MAIL = RECEIVE_MAIL.Replace(",", ";");


			if (!string.IsNullOrEmpty(doubleCompany))
			{
				RECEIVE_MAIL = RECEIVE_MAIL.Trim() + ";" + doubleCompany;
			}


			// 자동 메일 회신 사용할때만 ON
			//mailAutoReply();

			// line color
			if (CD_COMPANY.Equals("K100"))
				txtLog.SelectionColor = Color.Black;
			else
				txtLog.SelectionColor = Color.DarkBlue;


			// 메일 처리
			if (!string.IsNullOrEmpty(NO_MAIL) && !CODE.Equals("DUBHE_ETC"))
			{
				// 파싱, 자동등록 사용

				if (CODE.Equals("INQ"))
				{
					if (!InqCheck)
					{
						AutoInquiryInsert();

						if (!string.IsNullOrEmpty(RECEIVE_MAIL))
						{
							NO_EMAIL = RECEIVE_MAIL;

							if (NO_EMAIL.EndsWith(";"))
							{
								NO_EMAIL += RECEIVE_MAIL2;
								RECEIVE_MAIL2 = string.Empty;
							}
							else
							{
								NO_EMAIL = NO_EMAIL + ";" + RECEIVE_MAIL2;
								RECEIVE_MAIL2 = string.Empty;
							}
						}


						if (YN_MAIL.Equals("Y") && !InqCheck)
							SendMailServer();
					}

					InqCheck = true;

				}
				// 메일분류 기본
				else if (CODE.Equals("SEND_ETC"))
				{
					if (!string.IsNullOrEmpty(RECEIVE_MAIL))
					{
						NO_EMAIL = RECEIVE_MAIL;

						if (NO_EMAIL.EndsWith(";"))
						{
							NO_EMAIL += RECEIVE_MAIL2;
							RECEIVE_MAIL2 = string.Empty;
						}
						else
						{
							NO_EMAIL = NO_EMAIL + ";" + RECEIVE_MAIL2;
							RECEIVE_MAIL2 = string.Empty;
						}
					}

					if (YN_MAIL.Equals("Y"))
						SendMailServer();
				}
				// JIBE 자동 등록 건 확인
				else if (CODE.Equals("SEND_JIBE"))
				{
					JibeMailCheck();

					if (!string.IsNullOrEmpty(RECEIVE_MAIL))
					{
						NO_EMAIL = RECEIVE_MAIL;

						if (NO_EMAIL.EndsWith(";"))
						{
							NO_EMAIL += RECEIVE_MAIL2;
							RECEIVE_MAIL2 = string.Empty;
						}
						else
						{
							NO_EMAIL = NO_EMAIL + ";" + RECEIVE_MAIL2;
							RECEIVE_MAIL2 = string.Empty;
						}
					}

					if (YN_MAIL.Equals("Y"))
						SendMailServer();
				}
				else if (CODE.StartsWith("DRAWING_UPLOAD"))
				{
					DrawingUploadFunc(mailitem);
				}
				else if (CODE.Equals("UPLOAD_ORDER"))
				{
					if (!UpCheck)
					{
						OrderUploadFunc(mailitem);
					}

					UpCheck = true;
				}
				else if (CODE.Equals("UPLOAD_DELIVERY"))
				{
					if (!UpCheck)
					{
						DeliveryUploadFunc(mailitem);
					}

					UpCheck = true;
				}
				else if (CODE.Equals("UPLOAD_LEADTIME"))
				{
					if (!UpCheck)
					{
						LeadtimeUploadFunc(mailitem);
					}

					UpCheck = true;
				}
				// 메일 업로드 처리 : DINTEC - INQUIRY(
				else if (CODE.StartsWith("UPLOAD_"))
				{
					if (!UpCheck)
					{
						QuotationFunc(mailitem);
					}

					UpCheck = true;
				}
				// 파일번호 따서 메일 전송
				else if (CODE.StartsWith("SEND_"))
				{
					FileNoGet();

					if (YN_MAIL.Equals("Y"))
						SendMailServer();
				}
				else if (CODE.StartsWith("BACKUP"))
				{
					cd_send = "BACKU";
					msgStr = "백업설정 메일";

					DBInsert(NO_EMAIL, msgId, folderId, cd_send, "N", errorMsg, msgStr);

					if (CD_COMPANY.Equals("K100"))
					{
						if (mailitem != null)
							mailitem.Move(backupFolder);
					}
					else if (CD_COMPANY.Equals("K200"))
					{
						if (mailitem != null)
							mailitem.Move(dubackupFolder);
					}
				}
				else if (CODE.StartsWith("RPA_INQ"))
				{
					AutoInquiryInsert();

					if (!string.IsNullOrEmpty(RECEIVE_MAIL))
					{
						NO_EMAIL = RECEIVE_MAIL;

						if (NO_EMAIL.EndsWith(";"))
						{
							NO_EMAIL += RECEIVE_MAIL2;
							RECEIVE_MAIL2 = string.Empty;
						}
						else
						{
							NO_EMAIL = NO_EMAIL + ";" + RECEIVE_MAIL2;
							RECEIVE_MAIL2 = string.Empty;
						}
					}

					if (YN_MAIL.Equals("Y"))
						SendMailServer();
				}
				else if (CODE.StartsWith("RPA_MIDEAST"))
				{
					string codeStr = string.Empty;
					string dtStr = msg.DeliveryTime.ToString("yyyy년MM월dd일 HH시mm분ss초");


					if (!string.IsNullOrEmpty(msg.Body.ToString()))
					{
						string mideastCodeStr = msg.Body.ToString();

						if (mideastCodeStr.Contains("authorization code") && mideastCodeStr.Contains("Regards,"))
						{
							int idx_s = mideastCodeStr.IndexOf("is:");
							int idx_e = mideastCodeStr.IndexOf("Regards,");

							if (idx_s != -1 && idx_e != -1)
							{
								codeStr = mideastCodeStr.Substring(idx_s, idx_e - idx_s).Replace("is:", "").Trim();
							}
						}

						InsertMideastCode(CD_COMPANY, codeStr, dtStr);
						MIDEASTCode(codeStr, dtStr);
					}



					if (!string.IsNullOrEmpty(RECEIVE_MAIL))
					{
						NO_EMAIL = RECEIVE_MAIL;

						if (NO_EMAIL.EndsWith(";"))
						{
							NO_EMAIL += RECEIVE_MAIL2;
							RECEIVE_MAIL2 = string.Empty;
						}
						else
						{
							NO_EMAIL = NO_EMAIL + ";" + RECEIVE_MAIL2;
							RECEIVE_MAIL2 = string.Empty;
						}
					}

					if (YN_MAIL.Equals("Y"))
						SendMailServer();
				}
				else if (CODE.StartsWith("RPA_PIL"))
				{
					string codeStr = string.Empty;
					string dtStr = msg.DeliveryTime.ToString("yyyy년MM월dd일 HH시mm분ss초");


					if (!string.IsNullOrEmpty(msg.Body.ToString()))
					{
						string PILCodeStr = msg.Body.ToString();

						if (PILCodeStr.Contains("Please use this 2FA code") && PILCodeStr.Contains("for eInvoice application"))
						{
							int idx_s = PILCodeStr.IndexOf("Please use this 2FA code");
							int idx_e = PILCodeStr.IndexOf("for eInvoice application");

							if (idx_s != -1 && idx_e != -1)
							{
								codeStr = PILCodeStr.Substring(idx_s, idx_e - idx_s).Replace("Please use this 2FA code", "").Trim();
							}
						}


						PILCode(codeStr, dtStr);
					}



					if (!string.IsNullOrEmpty(RECEIVE_MAIL))
					{
						NO_EMAIL = RECEIVE_MAIL;

						if (NO_EMAIL.EndsWith(";"))
						{
							NO_EMAIL += RECEIVE_MAIL2;
							RECEIVE_MAIL2 = string.Empty;
						}
						else
						{
							NO_EMAIL = NO_EMAIL + ";" + RECEIVE_MAIL2;
							RECEIVE_MAIL2 = string.Empty;
						}
					}

					if (YN_MAIL.Equals("Y"))
						SendMailServer();
				}
				else if (CODE.StartsWith("RPA_MISC"))
				{
					string codeStr = string.Empty;
					string dtStr = msg.DeliveryTime.ToString("yyyy년MM월dd일 HH시mm분ss초");

					if (!string.IsNullOrEmpty(msg.Body.ToString()))
					{
						string MISCCodeStr = msg.Body.ToString();

						if (MISCCodeStr.Contains("Password (OTP) is ") && MISCCodeStr.Contains("This OTP will"))
						{
							int idx_s = MISCCodeStr.IndexOf("Password (OTP) is ");
							int idx_e = MISCCodeStr.IndexOf("This OTP will");

							if (idx_s != -1 && idx_e != -1)
							{
								codeStr = MISCCodeStr.Substring(idx_s, idx_e - idx_s).Replace("Password (OTP) is", "").Trim();
							}
						}

						MISCCode(codeStr, dtStr);
					}



					if (!string.IsNullOrEmpty(RECEIVE_MAIL))
					{
						NO_EMAIL = RECEIVE_MAIL;

						if (NO_EMAIL.EndsWith(";"))
						{
							NO_EMAIL += RECEIVE_MAIL2;
							RECEIVE_MAIL2 = string.Empty;
						}
						else
						{
							NO_EMAIL = NO_EMAIL + ";" + RECEIVE_MAIL2;
							RECEIVE_MAIL2 = string.Empty;
						}
					}

					if (YN_MAIL.Equals("Y"))
						SendMailServer();
				}
				else if (CODE.StartsWith("RPA_MDSDOC"))
				{
					resultCode = "MDSD";
				}
				else if (CODE.StartsWith("RPA_FW"))
				{
					resultCode = "FW";
				}
				else if (CODE.StartsWith("RPA_SKLOGIN"))
				{
					resultCode = "SKLOGIN";
				}
			}
			else
			{
				if (MAIL_FROM_DB_S.Contains("@dintec.co.kr") || MAIL_FROM_DB_S.Contains("@dubheco.com"))
				{
					string mailFromStr = MAIL_FROM_DB_S;

					mailFromStr = mailFromStr.Replace(";", "','").Replace(" ", "");
					mailFromStr = mailFromStr.Replace(",", "','").Trim();
					mailFromStr = mailFromStr.Replace("''", "'").Trim();

					DataTable isTeamMailStr = DBTeamMailSelectDT(mailFromStr);

					if (isTeamMailStr.Rows.Count > 0)
					{
						MailToSearch mts = new MailToSearch();

						// CC SEARCH
						mts.mailCCSearch(msg, mailitem, MAIL_TO);
						//mailCCSearch(msg);

						if (MAIL_CC.Contains("@dintec.co.kr") || MAIL_CC.Contains("@dubheco.com"))
						{
							string mailCCStr = MAIL_CC;


							mailCCStr = mailCCStr.Replace(";", "','").Replace(" ", "");
							mailCCStr = mailCCStr.Replace(",", "','").Trim();
							mailCCStr = mailCCStr.Replace("''", "'").Trim();

							DataTable isTeamMailYN = DBTeamMailSelectDT(mailCCStr);

							if (isTeamMailYN.Rows.Count == 0)
							{
								MAIL_CC = string.Empty;
							}
						}
					}
				}

				NO_EMAIL = string.Empty;
				if (MAIL_TO.ToLower().Contains("@dintec.co.kr") || MAIL_TO.ToLower().Contains("@dubheco.com"))
				{
					if ((MAIL_TO.ToLower().Contains("service@dintec.co.kr") && !MAIL_TO.ToLower().Contains("logservice@dintec.co.kr")) || MAIL_TO.ToLower().Contains("dongjin@dintec.co.kr"))
					{
						MAIL_TO = MAIL_TO.ToLower().Replace("service@dintec.co.kr", "").Replace("dongjin@dintec.co.kr", "").Trim();

						if ((MAIL_TO.ToLower().Contains("service@dintec.co.kr") && !MAIL_TO.ToLower().Contains("logservice@dintec.co.kr")) || MAIL_TO.ToLower().Contains("dongjin@dintec.co.kr"))
						{
							NO_EMAIL = tbx전달.Text;
							NO_EMP = "*";
						}
						else
						{
							NO_EMAIL = MAIL_TO;

							if (MAIL_CC.ToLower().Contains("@dintec.co.kr") || MAIL_CC.ToLower().Contains("@dubheco.com"))
							{
								if ((MAIL_CC.ToLower().Contains("service@dintec.co.kr") && !MAIL_CC.ToLower().Contains("logservice@dintec.co.kr")) || MAIL_CC.ToLower().Contains("dongjin@dintec.co.kr"))
								{
								}
								else
								{
									NO_EMAIL += ";" + MAIL_CC;
									NO_EMP = "*";
								}
							}
						}
					}
					else
					{
						NO_EMAIL = MAIL_TO;
						NO_EMP = "*";

						if (MAIL_CC.ToLower().Contains("@dintec.co.kr") || MAIL_CC.ToLower().Contains("@dubheco.com"))
						{
							if ((MAIL_CC.ToLower().Contains("service@dintec.co.kr") && !MAIL_CC.ToLower().Contains("logservice@dintec.co.kr")) || MAIL_CC.ToLower().Contains("dongjin@dintec.co.kr"))
							{

							}
							else
							{
								NO_EMAIL += ";" + MAIL_CC;
								NO_EMP = "*";
							}
						}
					}
				}
				else if (MAIL_CC.ToLower().Contains("@dintec.co.kr") || MAIL_CC.ToLower().Contains("@dubheco.com"))
				{
					if ((MAIL_CC.ToLower().Contains("service@dintec.co.kr") && !MAIL_CC.ToLower().Contains("logservice@dintec.co.kr")) || MAIL_CC.ToLower().Contains("dongjin@dintec.co.kr"))
					{
						MAIL_CC = MAIL_CC.ToLower().Replace("service@dintec.co.kr", "").Replace("dongjin@dintec.co.kr", "").Trim();

						if ((MAIL_CC.ToLower().Contains("service@dintec.co.kr") && !MAIL_CC.ToLower().Contains("logservice@dintec.co.kr")) || MAIL_CC.ToLower().Contains("dongjin@dintec.co.kr"))
						{
							NO_EMAIL = tbx전달.Text;
							NO_EMP = "*";
							NM_KOR = "CC전달";
						}
						else
						{
							NO_EMAIL = MAIL_CC;
							NO_EMP = "*";
							NM_KOR = "CC전달";
						}
					}
					else
					{
						NO_EMAIL = MAIL_CC;
						NO_EMP = "*";
						NM_KOR = "CC전달";
					}
				}
				else
				{
					NO_EMAIL = tbx전달.Text;
					NO_EMP = "*";
					NM_KOR = "전달";
				}

				string sendUser = string.Empty;

				if (CD_COMPANY.Equals("K200"))
					sendUser = "notice@dubheco.com";
				else if (NO_EMAIL.Contains("solve@dintec.co.kr"))
					sendUser = MAIL_FROM_DB_S;
				else
					sendUser = tbx전달.Text;

				//if(NO_EMAIL.Contains("invoice@dintec.co.kr") || NO_EMAIL.Contains("invoice2@dintec.co.kr"))
				//{
				//	sendUser = "teamadmin@dintec.co.kr";
				//}

				NO_EMAIL = NO_EMAIL.Replace(";;", ";").Replace("; ;", "").Trim(); //.ToLower().Replace("invoice@dintec.co.kr","").Replace("invoice2@dintec.co.kr","").Trim();
				string emailStr = NO_EMAIL;
				DataTable sendToCc = null;
				string _mailTo = NO_EMAIL.Replace(";", "','").Replace(" ", "");
				_mailTo = _mailTo.Replace(",", "','").Trim();
				_mailTo = _mailTo.Replace("''", "'").Trim();


				sendToCc = DBTeamMailSelectDT(_mailTo);
				NO_EMAIL = NO_EMAIL.Replace(",", ";").Trim();


				if (NO_EMAIL.ToLower().Contains("invoice@dintec.co.kr") || NO_EMAIL.ToLower().Contains("invoice2@dintec.co.kr"))
				{
					FileServerSave(mailitem);

					if (!string.IsNullOrEmpty(msgId) && sendToCc.Rows.Count > 0)
					{
						NO_EMAIL = string.Empty;
						for (int c = 0; c < sendToCc.Rows.Count; c++)
						{
							NO_EMAIL += sendToCc.Rows[c]["CD_FLAG2"].ToString().Replace("\n", "").Replace("\r", "").Trim() + ";";
							//emailStr = emailStr.ToLower().Replace(sendToCc.Rows[c]["CD_FLAG1"].ToString(), ";").Trim();
						}


						if (msgId.Contains(","))
						{
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, NO_EMAIL, "", "");
						}
						else if (NO_EMAIL.Replace(";", "").Trim().Length > 1)
						{
							SendMailServer();
						}
						else if (emailStr.Contains("invoice"))
						{
							resultCode = "INVOICE";
						}
					}
					else
					{
						MailMoveFolderInvoice();
					}
				}
				else if (NO_EMAIL.ToLower().Contains("invoice@dubheco.com"))
				{
					FileServerSave_K200(mailitem);

					if (!string.IsNullOrEmpty(msgId) && sendToCc.Rows.Count > 0)
					{
						NO_EMAIL = string.Empty;
						for (int c = 0; c < sendToCc.Rows.Count; c++)
						{
							NO_EMAIL += sendToCc.Rows[c]["CD_FLAG2"].ToString().Replace("\n", "").Replace("\r", "").Trim() + ";";
						}


						if (msgId.Contains(","))
						{
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, NO_EMAIL, "", "");
						}
						else if (NO_EMAIL.Replace(";", "").Trim().Length > 1)
						{
							SendMailServer();
						}
						else if (emailStr.Contains("invoice"))
						{
							resultCode = "INVOICE";
						}
					}
					else
					{
						MailMoveFolderInvoice_K200();
					}
				}
				else if (NO_EMAIL.ToLower().Contains("receipt@dintec.co.kr"))
				{
					FileServerSaveReceipt_K100(mailitem);

					if (!string.IsNullOrEmpty(msgId) && sendToCc.Rows.Count > 0)
					{
						NO_EMAIL = string.Empty;
						for (int c = 0; c < sendToCc.Rows.Count; c++)
						{
							NO_EMAIL += sendToCc.Rows[c]["CD_FLAG2"].ToString().Replace("\n", "").Replace("\r", "").Trim() + ";";
							//emailStr = emailStr.ToLower().Replace(sendToCc.Rows[c]["CD_FLAG1"].ToString(), ";").Trim();
						}


						if (msgId.Contains(","))
						{
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, NO_EMAIL, "", "");
						}
						else if (NO_EMAIL.Replace(";", "").Trim().Length > 1)
						{
							SendMailServer();
						}
						else if (emailStr.Contains("receipt"))
						{
							resultCode = "RECEIPT";
						}
					}
					else
					{
						MailMoveFolderReceipt();
					}
				}
				else if (NO_EMAIL.ToLower().Contains("receipt@dubheco.com"))
				{
					FileServerSaveReceipt_K200(mailitem);

					if (!string.IsNullOrEmpty(msgId) && sendToCc.Rows.Count > 0)
					{
						NO_EMAIL = string.Empty;
						for (int c = 0; c < sendToCc.Rows.Count; c++)
						{
							NO_EMAIL += sendToCc.Rows[c]["CD_FLAG2"].ToString().Replace("\n", "").Replace("\r", "").Trim() + ";";
							//emailStr = emailStr.ToLower().Replace(sendToCc.Rows[c]["CD_FLAG1"].ToString(), ";").Trim();
						}


						if (msgId.Contains(","))
						{
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, NO_EMAIL, "", "");
						}
						else if (NO_EMAIL.Replace(";", "").Trim().Length > 1)
						{
							SendMailServer();
						}
						else if (emailStr.Contains("receipt"))
						{
							resultCode = "RECEIPT";
						}
					}
					else
					{
						MailMoveFolderReceipt_K200();
					}
				}
				else if (!string.IsNullOrEmpty(NO_EMAIL) && !string.IsNullOrEmpty(msgId) && sendToCc.Rows.Count > 0)
				{
					NO_EMAIL = string.Empty;
					for (int c = 0; c < sendToCc.Rows.Count; c++)
					{
						NO_EMAIL += sendToCc.Rows[c]["CD_FLAG2"].ToString().Replace("\n", "").Replace("\r", "").Trim() + ";";
						emailStr = emailStr.ToLower().Replace(sendToCc.Rows[c]["CD_FLAG1"].ToString(), ";").Trim();
					}

					NO_EMAIL = NO_EMAIL + ";" + emailStr;

					if (msgId.Contains(","))
					{
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, NO_EMAIL, "", "");
					}
					else
						SendMailServer();
				}
				else if (string.IsNullOrEmpty(msgId))
				{
					NO_EMAIL = string.Empty;
					for (int c = 0; c < sendToCc.Rows.Count; c++)
					{
						NO_EMAIL += sendToCc.Rows[c]["CD_FLAG2"].ToString().Replace("\n", "").Trim() + ";";
					}

					if (NO_EMAIL.Contains("solve@dintec.co.kr") || string.IsNullOrEmpty(NO_EMAIL.Replace(";", "")))
						NO_EMAIL = MAIL_FROM_DB_S;

					resultCode = "SENDMAIL";
					sendMailSys(mailitem, NO_EMAIL, "", "");
				}
				else if (!string.IsNullOrEmpty(NO_EMAIL))
				{
					resultCode = "SENDMAIL";
					sendMailSys(mailitem, sendUser, "", "");
				}
				else if (string.IsNullOrEmpty(NO_EMAIL))
				{
					resultCode = "SENDMAIL";
					sendMailSys(mailitem, sendUser, "", "");
				}

			}
		}

		// P_CZ_MAIL_SORTER에서도 사용
		public DataTable MailSort_Test(string _filename)
		{
			fileName = _filename;

			MapiMessage msg = MapiMessage.FromFile(fileName);

			DELIVERYDT = msg.DeliveryTime.ToString("yyyyMMddHHmmss");             // 받은시간
																				  //if (mailitem.Subject != null)
			MAIL_SUBJECT = msg.Subject.ToString();                   // 제목
																	 //if (mailitem.SenderEmailAddress != null)
			MAIL_FROM_DB_S = msg.SenderEmailAddress.ToLower().ToString().Trim();  // 보낸사람


			//if (!string.IsNullOrEmpty(mailitem.Body) && mailitem.Body != null)
			MAIL_BODY = msg.Body.ToString();                                 // 본문내용

			MailToSearch mts = new MailToSearch();
			// TO SEARCH
			MAIL_TO = mts.mailToSearch(msg, mailitem, MAIL_TO);
			//mailToSearch(msg);

			// CC SEARCH
			MAIL_CC = mts.mailCCSearch(msg, mailitem, MAIL_TO);
			//mailCCSearch(msg);

			// TO, CC 정리
			mailToCcSearch(msg);

			// 메일 DB 검색
			mailDBSearch();

			// TO, CC 정리
			mailToCcSearch(msg);

			// 메일 DB 검색
			mailDBSearch();


			return dbDatatbResult;
		}
		#endregion ▣ 메일 분류

		#region ▣ 메일 이동
		private void MailMoveFolderUn()
		{
			try
			{
				if (CD_COMPANY.Equals("K100"))
				{
					if (mailitem != null)
					{
						if (NO_EMAIL.Equals(tbx전달.Text))
							mailitem.Move(backupFolder);
						else
							mailitem.Move(unClass);
					}
				}
				else if (CD_COMPANY.Equals("K200"))
				{
					if (mailitem != null)
						mailitem.Move(duunClass);
				}
				else
				{
					if (mailitem != null)
					{
						if (NO_EMAIL.Equals(tbx전달.Text))
							mailitem.Move(backupFolder);
						else
							mailitem.Move(unClass);
					}
				}
			}
			catch
			{

			}
		}

		private void MailMoveFolderMo()
		{
			try
			{
				if (CD_COMPANY.Equals("K100"))
				{
					if (mailitem != null)
					{
						if (NO_EMAIL.Equals(tbx전달.Text))
							mailitem.Move(backupFolder);
						else
							mailitem.Move(moveFolder);
						//mailitem.Move(moveFolder);
					}
				}
				else if (CD_COMPANY.Equals("K200"))
				{
					if (mailitem != null)
						mailitem.Move(dumoveFolder);
				}
			}
			catch
			{
				if (mailitem != null)
					mailitem.Move(backupFolder);
			}
		}

		private void MailMoveFolderInvoice()
		{
			try
			{

				if (mailitem != null)
					mailitem.Move(invoiceFolder);

				LogWrite2("[INVOICE]	" + mailitem.Subject + " / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
			}
			catch
			{
				if (mailitem != null)
					mailitem.Move(backupFolder);
			}
		}

		private void MailMoveFolderInvoice_K200()
		{
			try
			{

				if (mailitem != null)
					mailitem.Move(invoiceFolderdu);

				LogWrite2("[INVOICE]	" + mailitem.Subject + " / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
			}
			catch
			{
				if (mailitem != null)
					mailitem.Move(backupFolder);
			}
		}




		private void MailMoveFolderReceipt()
		{
			try
			{

				if (mailitem != null)
					mailitem.Move(receiptFolder);

				LogWrite2("[RECEIPT]	" + mailitem.Subject + " / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
			}
			catch
			{
				if (mailitem != null)
					mailitem.Move(backupFolder);
			}
		}

		private void MailMoveFolderReceipt_K200()
		{
			try
			{

				if (mailitem != null)
					mailitem.Move(receiptFolderdu);

				LogWrite2("[RECEIPT]	" + mailitem.Subject + " / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
			}
			catch
			{
				if (mailitem != null)
					mailitem.Move(backupFolder);
			}
		}



		private void MailMoveFolderMDSD()
		{
			try
			{

				if (mailitem != null)
					mailitem.Move(MDSDFolder);

				LogWrite2("[MDSDOC]	" + mailitem.Subject + " / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
			}
			catch
			{
				if (mailitem != null)
					mailitem.Move(backupFolder);
			}
		}




		private void MailMoveFolderMDSD_K200()
		{
			try
			{

				if (mailitem != null)
					mailitem.Move(MDSDFolderdu);

				LogWrite2("[MDSDOC]	" + mailitem.Subject + " / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
			}
			catch
			{
				if (mailitem != null)
					mailitem.Move(backupFolder);
			}
		}



		private void MailMoveFolderFW()
		{
			try
			{

				if (mailitem != null)
					mailitem.Move(FWFolder);

				LogWrite2("[FORWARDER]	" + mailitem.Subject + " / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
			}
			catch
			{
				if (mailitem != null)
					mailitem.Move(backupFolder);
			}
		}

		private void MailMoveFolderRE()
		{
			try
			{

				if (mailitem != null)
					mailitem.Move(FWFolder);

				LogWrite2("[납기회신]	" + mailitem.Subject + " / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
			}
			catch
			{
				if (mailitem != null)
					mailitem.Move(backupFolder);
			}
		}


		private void MailMoveFolderSKLOGIN()
		{
			try
			{

				if (mailitem != null)
					mailitem.Move(SKFolder);

				LogWrite2("[SKLOGIN]	" + mailitem.Subject + " / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
			}
			catch
			{
				if (mailitem != null)
					mailitem.Move(backupFolder);
			}
		}


		#endregion ▣ 메일 이동

		#region ▣ 메일 발송
		private void SendMailServer()
		{
			// folderId
			DataTable dtfolderID = null;
			DataTable dtfolderID2 = null;

			string _no_email = string.Empty;
			string _mailTo = string.Empty;
			string[] _mailToSpl = null;

			//if (NO_EMAIL.Contains("@dubheco.com"))
			//	CD_COMPANY = "K200";
			//if (NO_EMAIL.Contains("@dintec.co.kr"))
			//	CD_COMPANY = "K100";



			if (NO_EMAIL.ToLower().Contains("@dintec.co.kr") && NO_EMAIL.ToLower().Contains("@dubheco.com"))
			{
				CD_COMPANY = "K100";
				_no_email = NO_EMAIL.ToLower().Replace("@dintec.co.kr", "").Replace("@dubheco.com", "").Replace("'", "").Trim();
				_mailTo = _no_email.Replace(";", "','").Replace(" ", "");
				_mailToSpl = _mailTo.Split(',');
				dtfolderID = DBMailBoxListSelect(_mailTo);

				CD_COMPANY = "K200";
				_no_email = NO_EMAIL.ToLower().Replace("@dintec.co.kr", "").Replace("@dubheco.com", "").Replace("'", "").Trim();
				_mailTo = _no_email.Replace(";", "','").Replace(" ", "");
				_mailToSpl = _mailTo.Split(',');
				dtfolderID2 = DBMailBoxListSelect(_mailTo);

				if (dtfolderID2 != null)
				{
					//DataSet ds = new DataSet();
					//ds.Merge(dtfolderID);
					//ds.Merge(dtfolderID2);

					dtfolderID.Merge(dtfolderID2);

					// dataset to datatble
					//dtfolderID = ds.Tables[0];
				}
			}
			else if (NO_EMAIL.ToLower().Contains("@dubheco.com"))
			{
				CD_COMPANY = "K200";
				_no_email = NO_EMAIL.ToLower().Replace("@dintec.co.kr", "").Replace("@dubheco.com", "").Replace("'", "").Trim();
				_mailTo = _no_email.Replace(";", "','").Replace(" ", "");
				_mailToSpl = _mailTo.Split(',');
				dtfolderID = DBMailBoxListSelect(_mailTo);
			}
			else
			{
				CD_COMPANY = "K100";
				_no_email = NO_EMAIL.ToLower().Replace("@dintec.co.kr", "").Replace("@dubheco.com", "").Replace("'", "").Trim();
				_mailTo = _no_email.Replace(";", "','").Replace(" ", "");
				_mailToSpl = _mailTo.Split(',');
				dtfolderID = DBMailBoxListSelect(_mailTo);
			}



			string userNameId = string.Empty;

			if (!_no_email.Contains("@") || dtfolderID.Rows.Count > 0)
			{
				if (dtfolderID.Rows.Count > 1)
				{
					for (int c = 0; c < dtfolderID.Rows.Count; c++)
					{
						folderId = dtfolderID.Rows[c]["mb_id"].ToString().Trim().ToUpper();
						userNameId = dtfolderID.Rows[c]["du_name"].ToString().Trim() + "/" + dtfolderID.Rows[c]["du_userId"].ToString().Trim();

						if (c != dtfolderID.Rows.Count - 1)
						{
							string resultMsg = JsonWriteCopy(msgId, folderId, AFTER_MAIL_NAME);

							if (resultMsg.Contains("success"))
							{
								LogWrite2("[메일발송]	" + mailitem.Subject + " / " + userNameId + " / " + NO_MAIL + " / " + AFTER_MAIL_NAME);

								cd_send = "COPY+";
								DBInsert(userNameId, msgId, folderId, cd_send, "Y", errorMsg, msgStr);
							}
							else
							{
								errorMsg = errorMsg + resultMsg;
								resultCode = "SENDMAIL";
								sendMailSys(mailitem, NO_EMAIL, "", "");
								break;
							}

							System.Threading.Thread.Sleep(100);
						}
						else if (c == dtfolderID.Rows.Count - 1)
						{
							string resultMsg = JsonWriteCopy(msgId, folderId, AFTER_MAIL_NAME);

							if (resultMsg.Contains("success"))
							{
								LogWrite2("[메일발송]	" + mailitem.Subject + " / " + userNameId + " / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
								
								cd_send = "COPY+";
								resultCode = "BACKUP";

								DBInsert(userNameId, msgId, folderId, cd_send, "Y", errorMsg, msgStr);
							}
							else
							{
								errorMsg = errorMsg + resultMsg;
								resultCode = "SENDMAIL";
								sendMailSys(mailitem, NO_EMAIL, "", "");
								break;
							}

							System.Threading.Thread.Sleep(100);
						}
					}
				}
				else if (dtfolderID.Rows.Count == 1)
				{
					folderId = dtfolderID.Rows[0]["mb_id"].ToString().Trim().ToUpper();
					userNameId = dtfolderID.Rows[0]["du_name"].ToString().Trim() + "/" + dtfolderID.Rows[0]["du_userId"].ToString().Trim();
					string resultMsg = JsonWriteCopy(msgId, folderId, AFTER_MAIL_NAME);

					if (resultMsg.Contains("success"))
					{
						LogWrite2("[메일발송]	" + mailitem.Subject + " / " + userNameId + " / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
						
						cd_send = "COPY";
						resultCode = "BACKUP";

						DBInsert(userNameId, msgId, folderId, cd_send, "Y", errorMsg, msgStr);
					}
					else
					{
						errorMsg = errorMsg + resultMsg;
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, NO_EMAIL, "", "");
					}
				}
				else
				{
					errorMsg = errorMsg + "수신자 메일의 폴더ID가 검색 되지 않음";
					resultCode = "SENDMAIL";
					sendMailSys(mailitem, NO_EMAIL, "", "");
				}
			}
			else
			{
				cd_send = "SEND";
				resultCode = "SENDMAIL";
				sendMailSys(mailitem, NO_EMAIL, "", "teamadmin@dintec.co.kr");
			}
		}
		#endregion ▣ 메일 발송

		#region ▣ 메일 전달
		private void sendMailSys(Outlook.MailItem mailitem, string sendTo, string sendCC, string sendBCC)
		{
			try
			{
				//TEST
				//sendTo = "sangwon.ha@dintec.co.kr";
				//sendCC = "";
				sendBCC = "teamadmin@dintec.co.kr";
				msgStr = "메일 전달";
				LogWrite2("[메일전달]	" + mailitem.Subject + " / " + sendTo + " / " + sendCC);

				// 보내기
				sendMail(mailitem, sendTo, sendCC, sendBCC);


			}
			catch (Exception e)
			{
				MailMoveFolderUn();
				msgStr = "미분류";

				LogWrite2("[미분류]	" + mailitem.Subject + " / " + NO_EMAIL + " / " + e);
			}

			NO_EMAIL = sendTo;

			DBInsert(NO_EMAIL, msgId, folderId, cd_send, "O", errorMsg, msgStr);

			sendTo = string.Empty;
			sendBCC = string.Empty;
			sendCC = string.Empty;
			mailitem = null;
		}


		private void sendMail(Outlook.MailItem mail, string mailTo, string mailCC, string mailBCC)
		{
			if (mail != null)
			{
				mail.To = mailTo; //my fixed email adress 
				mail.CC = mailCC;   //removing any carbon copy users
				mail.BCC = mailBCC; //removing any blind carbon copy users
				mail.Send();
			}
		}

		#endregion ▣ 메일 전달

		private void DrawingUploadFunc(Outlook.MailItem mailitem)
		{
			try
			{
				if (CODE.Equals("DRAWING_UPLOAD"))
				{
					if (MAIL_FROM_DB_S.ToLower().Contains("@dintec.co.kr") || MAIL_FROM_DB_S.ToLower().Contains("@dubheco.com"))
					{
						NO_EMAIL = NO_EMAIL + ";" + MAIL_FROM_DB_S;
					}


					int idx_s = -1;
					int idx_e = -1;

					idx_s = MAIL_SUBJECT.IndexOf("INQUIRY(");

					if (idx_s != -1)
						FILE_NO = MAIL_SUBJECT.Substring(idx_s, 18).Replace("INQUIRY(", "").Trim();

					if (FILE_NO.StartsWith("ST"))
					{
						FILE_NO = MAIL_SUBJECT.Substring(idx_s, 20).Replace("INQUIRY(", "").Trim();
					}

					if (!FILE_NO.Contains("-"))
					{
						FILE_NO = FILE_NO.Substring(0, 10).Trim();
					}

					idx_e = MAIL_SUBJECT.IndexOf("_");

					if (idx_e == -1)
						FILE_SUPPLY = MAIL_SUBJECT.Substring(MAIL_SUBJECT.Length - 5, 5).Trim();
					else
						FILE_SUPPLY = MAIL_SUBJECT.Substring(idx_e, MAIL_SUBJECT.Length - idx_e);

					idx_s = -1;
					idx_s = MAIL_SUBJECT.IndexOf("IMO");


					//// 서원텍
					//if (FILE_SUPPLY.StartsWith("_126"))
					//	FILE_SUPPLY = "12609";

					if (MAIL_FROM_DB_S.Equals("swt-valve@daum.net"))
						FILE_SUPPLY = "12609";

					if (idx_s != -1 && MAIL_SUBJECT.Length > idx_s + 10)
						NO_IMO = MAIL_SUBJECT.Substring(idx_s, 10).Trim();

					if ((string.IsNullOrEmpty(FILE_NO) || NO_IMO.Contains(FILE_SUPPLY)) && MAIL_TO.ToLower().Equals("service@dintec.co.kr"))
					{
						msgStr = "미분류";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
					else if ((string.IsNullOrEmpty(FILE_NO) || NO_IMO.Contains(FILE_SUPPLY)) && MAIL_TO.ToLower().Equals("service@dubheco.com"))
					{
						msgStr = "미분류";
						if (string.IsNullOrEmpty(NO_EMAIL.Replace(";", "").Trim()))
						{
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, "notice@dubheco.com", "", "");
						}
						else
						{
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, NO_EMAIL, "", "");
						}
					}
					else if (MAIL_TO.Contains("service@dintec.co.kr") && MAIL_FROM_DB_S.Contains("@dintec.co.kr"))
					{
						msgStr = "미분류";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, NO_EMAIL, "", "");
					}
					else if (MAIL_TO.Contains("service@dubheco.com") && MAIL_FROM_DB_S.Contains("@dubheco.com"))
					{
						msgStr = "미분류";
						if (string.IsNullOrEmpty(NO_EMAIL.Replace(";", "").Trim()))
						{
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, "notice@dubheco.com", "", "");
						}
						else
						{
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, NO_EMAIL, "", "");
						}
					}
					else
					{
						if (string.IsNullOrEmpty(FILE_SUPPLY))
							FILE_SUPPLY = MAIL_SUBJECT.Substring(MAIL_SUBJECT.Length - 5, 5);

						if (FILE_SUPPLY.EndsWith("]"))
							FILE_SUPPLY = FILE_SUPPLY.Replace("]", "").Trim();

						if (FILE_SUPPLY.Length > 10)
						{
							string _supplyName = FILE_SUPPLY.Substring(1, 5).Trim();
							if (!GetTo.IsInt(_supplyName))
							{
								FILE_SUPPLY = FILE_SUPPLY.Substring(FILE_SUPPLY.Length - 5, 5).Trim();
							}
							else
							{
								FILE_SUPPLY = _supplyName;
							}
						}

						else if (FILE_SUPPLY.Length == 6)
							FILE_SUPPLY = FILE_SUPPLY.Replace("_", "").Trim();


						if (FILE_SUPPLY.Length != 5 && CD_COMPANY.Equals("K100"))
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
							return;
						}
						else if (FILE_SUPPLY.Length != 5 && CD_COMPANY.Equals("K200"))
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
							return;
						}

						if (FILE_SUPPLY.Length == 5 && !string.IsNullOrEmpty(FILE_NO) && GetTo.IsInt(FILE_SUPPLY))
						{
							AttSave();
						}
						else
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
						}
					}
				}
				else if (CODE.Equals("UPLOAD_ETC"))
				{
					int idx_s = -1;

					idx_s = MAIL_SUBJECT.IndexOf("INQUIRY(");

					if (idx_s != -1)
						FILE_NO = MAIL_SUBJECT.Substring(idx_s, 18).Replace("INQUIRY(", "").Trim();


					FILE_SUPPLY = CD_PARTNER;

					if (!string.IsNullOrEmpty(FILE_SUPPLY) && !string.IsNullOrEmpty(FILE_NO))
					{
						AttSave();

						if (FILE_SUPPLY == "00047")
						{
							DataTable dt = SQL.GetDataTable(@"SELECT 1 FROM CZ_SA_QTNH QH
JOIN CZ_PU_QTNH QH1 ON QH1.CD_COMPANY = QH.CD_COMPANY AND QH1.NO_FILE = QH.NO_FILE
JOIN MA_EMP ME ON ME.CD_COMPANY = QH.CD_COMPANY AND ME.NO_EMP = QH.NO_EMP_QTN
WHERE QH.CD_COMPANY = 'K100'
AND QH.NO_FILE = '" + FILE_NO + @"'
AND QH1.CD_PARTNER = '00047'
AND ME.CD_DEPT = '010900'");

							if (dt != null && dt.Rows.Count > 0)
							{
								string contents = @"** 매입견적서 등록 알림

- 매입처 : {0}
- 파일번호 : {1}

※ 본 쪽지는 발신 전용 입니다.";

								contents = string.Format(contents, FILE_SUPPLY, FILE_NO);

								Messenger.SendMSG(new string[] { "S-495", "S-579", "S-596" }, contents);

								LogWrite(Color.DodgerBlue, "[쪽지발송]	" + mailitem.Subject + " / 동화엔텍 수신 쪽지 / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
							}
						}
					}
					else
					{
						msgStr = "거래처코드 확인 안됨";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
				}
				else if (CODE.Equals("UPLOAD_SWT"))
				{
					int idx_s = -1;

					idx_s = MAIL_SUBJECT.IndexOf("INQUIRY(");

					if (idx_s != -1)
						FILE_NO = MAIL_SUBJECT.Substring(idx_s, 18).Replace("INQUIRY(", "").Trim();

					FILE_SUPPLY = CD_PARTNER;

					if (FILE_SUPPLY.Length == 5 && !string.IsNullOrEmpty(FILE_NO) && GetTo.IsInt(FILE_SUPPLY))
					{
						AttSave();
					}
					else
					{
						msgStr = "거래처코드 확인 안됨";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
				}
				else if (CODE.Equals("UPLOAD_WOOYANG"))
				{
					int idx_s = -1;

					idx_s = MAIL_SUBJECT.IndexOf("견적서/");

					if (idx_s != -1 && !FILE_NO.StartsWith("A-"))
					{
						FILE_SUPPLY = CD_PARTNER;

						FILE_NO = MAIL_SUBJECT.Substring(idx_s, 14).Replace("견적서/", "").Trim();

						if (FILE_SUPPLY.Length == 5 && !string.IsNullOrEmpty(FILE_NO) && GetTo.IsInt(FILE_SUPPLY))
						{
							AttSave();
						}
						else
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
						}
					}

					else
					{
						msgStr = "거래처코드 확인 안됨";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
				}
				else if (CODE.Equals("UPLOAD_OMCEAST"))
				{
					int idx_s = MAIL_SUBJECT.IndexOf("(");

					if (idx_s != -1)
					{
						FILE_SUPPLY = CD_PARTNER;
						FILE_NO = MAIL_SUBJECT.Substring(idx_s, 11).Replace("(", "").Trim();

						if (FILE_SUPPLY.Length == 5 && !string.IsNullOrEmpty(FILE_NO) && GetTo.IsInt(FILE_SUPPLY))
						{
							AttSave();
						}
						else
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
						}
					}
					else
					{
						msgStr = "거래처코드 확인 안됨";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
				}
				else if (CODE.Equals("UPLOAD_YOUNGNAM"))
				{
					int idx_s = MAIL_SUBJECT.IndexOf("유닉스스틸 견적서 입니다.");

					if (idx_s != -1)
					{
						FILE_SUPPLY = CD_PARTNER;
						FILE_NO = MAIL_SUBJECT.Replace("유닉스스틸 견적서 입니다.", "").Trim();

						if (FILE_SUPPLY.Length == 5 && !string.IsNullOrEmpty(FILE_NO) && GetTo.IsInt(FILE_SUPPLY))
						{
							AttSave();
						}
						else
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
						}
					}
					else
					{
						msgStr = "거래처코드 확인 안됨";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
				}
				else if (CODE.Equals("UPLOAD_DHET"))
				{
					FILE_SUPPLY = "00047";

					int idx_s = -1;

					idx_s = MAIL_SUBJECT.IndexOf("QUOTATION FOR");

					if (idx_s != -1)
					{
						string noStr = MAIL_SUBJECT.Substring(idx_s, 24).Replace("QUOTATION FOR", "").Trim();
						FILE_NO = noStr;

						if (FILE_SUPPLY.Length == 5 && !string.IsNullOrEmpty(FILE_NO) && GetTo.IsInt(FILE_SUPPLY))
						{
							AttSave();

							if (CD_COMPANY == "K100")
							{
								DataTable dt = SQL.GetDataTable(@"SELECT 1 FROM CZ_SA_QTNH QH
JOIN CZ_PU_QTNH QH1 ON QH1.CD_COMPANY = QH.CD_COMPANY AND QH1.NO_FILE = QH.NO_FILE
JOIN MA_EMP ME ON ME.CD_COMPANY = QH.CD_COMPANY AND ME.NO_EMP = QH.NO_EMP_QTN
WHERE QH.CD_COMPANY = 'K100'
AND QH.NO_FILE = '" + FILE_NO + @"'
AND QH1.CD_PARTNER = '00047'
AND ME.CD_DEPT = '010900'");

								if (dt != null && dt.Rows.Count > 0)
								{
									string contents = @"** 매입견적서 등록 알림

- 매입처 : {0}
- 파일번호 : {1}

※ 본 쪽지는 발신 전용 입니다.";

									contents = string.Format(contents, FILE_SUPPLY, FILE_NO);

									Messenger.SendMSG(new string[] { "S-495", "S-579", "S-596" }, contents);

									LogWrite(Color.DodgerBlue, "[쪽지발송]	" + mailitem.Subject + " / 동화엔텍 수신 쪽지 / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
								}

							}
						}
						else
						{
							msgStr = "거래처코드 확인 안됨";
							if (CD_COMPANY == "K200")
							{
								resultCode = "SENDMAIL";
								sendMailSys(mailitem, "hasup.kim@dubheco.com", "", "");
							}
							else
							{
								resultCode = "SENDMAIL";
								sendMailSys(mailitem, tbx전달.Text, "", "");
							}
						}
					}
					else
					{
						msgStr = "거래처코드 확인 안됨";
						if (CD_COMPANY == "K200")
						{
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, "hasup.kim@dubheco.com", "", "");
						}
						else
						{
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
						}
					}
				}
				else
				{
					msgStr = "거래처코드 확인 안됨";
					resultCode = "SENDMAIL";
					sendMailSys(mailitem, tbx전달.Text, "", "");
				}
			}
			catch (Exception ex)
			{
				msgStr = "거래처코드 확인 안됨";
				resultCode = "SENDMAIL";
				sendMailSys(mailitem, tbx전달.Text, "", "");
			}
		}



		private void OrderUploadFunc(Outlook.MailItem mailitem)
		{
			try
			{
				string 메일제목 = mailitem.Subject.ToString();

				if (메일제목.StartsWith("#발주서"))
				{
					// 파일코드
					string fileCode = string.Join("|", FileCode());
					FILE_NO = string.Join(",", Regex.Matches(메일제목, "(" + fileCode + ")" + @"2\d{5,7}-{0,1}\d{0,2}").Cast<Match>());
				}
				else
				{
					InquiryParser parser = new InquiryParser(fileName);
					parser.Parse(true);

					string orderfileno = string.Empty;
					string ordertotal = string.Empty;
					string ordercurrency = string.Empty;
					string orderreference = string.Empty;
					string orderimo = string.Empty;
					string ordervessel = string.Empty;

					string partnercode = string.Empty;
					string ordercurrency_cd = string.Empty;


					orderfileno = parser.orderfileno;
					ordertotal = parser.ordertotal;
					ordercurrency = parser.ordercurrency;
					orderreference = parser.orderreference;
					orderimo = parser.orderimo;
					ordervessel = parser.ordervessel;



					// 호선 가져오기
					string query = @"
SELECT
	  A.NO_IMO
	, A.NO_HULL
	, A.NM_VESSEL
	, B.CD_PARTNER
	, B.LN_PARTNER
	, B.CD_PARTNER_GRP
FROM	  CZ_MA_HULL	AS A WITH(NOLOCK)
LEFT JOIN MA_PARTNER	AS B WITH(NOLOCK) ON A.CD_PARTNER = B.CD_PARTNER AND B.CD_COMPANY = @CD_COMPANY
WHERE A.NO_IMO = @NO_IMO OR (@NO_IMO IS NULL AND A.NM_VESSEL LIKE '%' + @NM_VESSEL + '%')";

					DBMgr dbm = new DBMgr();
					dbm.DebugMode = DebugMode.Print;
					dbm.Query = query;
					dbm.AddParameter("@CD_COMPANY", CD_COMPANY);
					dbm.AddParameter("@NO_IMO", orderimo);
					dbm.AddParameter("@NM_VESSEL", ordervessel);
					DataTable dt = dbm.GetDataTable();

					if (dt.Rows.Count == 1)
					{
						if (dt.Rows[0]["CD_PARTNER"].ToString() != "")
							partnercode = dt.Rows[0]["CD_PARTNER"].ToString();

						if (string.IsNullOrEmpty(orderimo))
							orderimo = dt.Rows[0]["NO_IMO"].ToString();
					}



					//CD_EXCH 가지고 오기
					query = "SELECT CD_SYSDEF FROM MA_CODEDTL WHERE CD_COMPANY = '" + CD_COMPANY + "' AND CD_FIELD = 'MA_B000005' AND NM_SYSDEF = '" + ordercurrency + "'";
					DataTable dtCode = DBMgr.GetDataTable(query);

					if (dtCode != null)
						ordercurrency_cd = dtCode.Rows[0]["CD_SYSDEF"].ToString();

					// 파일번호 확인
					query = "SELECT * FROM CZ_SA_QTNH WHERE CD_COMPANY = '" + CD_COMPANY + "' AND NO_FILE ='" + orderfileno + "'";
					DataTable dtFile = DBMgr.GetDataTable(query);


					if (dtFile.Rows.Count > 0 && !string.IsNullOrEmpty(orderfileno))
					{
						// DC_RMK 추후 업로드
						InsertOrderUpload(CD_COMPANY, orderfileno, partnercode, orderimo, ordervessel, orderreference, ordercurrency, ordercurrency_cd, ordertotal, "");
						FILE_NO = orderfileno;
					}
					else
					{
						FILE_NO = "";
					}
				}

				AttOrderUp();

			}
			catch (Exception ex)
			{
				msgStr = "고객발주서 업로드 오류";
				resultCode = "SENDMAIL";
				sendMailSys(mailitem, MAIL_FROM_DB_S, "", "");
			}
		}


		public string FormatDateString(string dateString)
		{
			if (DateTime.TryParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
			{
				parsedDate = parsedDate.AddDays(3); // 날짜에 3일 추가
				return parsedDate.ToString("yyyy/MM/dd");
			}
			else
			{
				return dateString;
			}
		}

		public string FormatEmailReply(string originalBody, string replyContent)
		{
			string boundary = "-----Original Message-----";

			string formattedBody = $"{replyContent}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{boundary}{Environment.NewLine}{originalBody.Trim()}{Environment.NewLine}{boundary}";

			return formattedBody;
		}


		private void LeadtimeUploadFunc(Outlook.MailItem mailitem)
		{
			try
			{
				Outlook.MailItem replyMail = null;

				string 메일제목 = mailitem.Subject.ToString();
				string 본문 = mailitem.Body;
				string 날짜 = string.Empty;
				string 회신계정 = string.Empty;
				string 회신제목 = string.Empty;

				if (메일제목.Contains("#납기회신"))
				{
					회신제목 = 메일제목.Replace("#납기회신", "").Trim();
					// 파일코드
					string fileCode = string.Join("|", FileCode());
					FILE_NO = string.Join(",", Regex.Matches(메일제목, "(" + fileCode + ")" + @"2\d{5,7}-{0,1}\d{0,2}").Cast<Match>());

					string pattern = @"[\w\.-]+@[\w\.-]+\.\w+";
					Regex regex = new Regex(pattern);

					MatchCollection matches = regex.Matches(메일제목);

					foreach (Match match in matches)
					{
						회신계정 += match.Value + ";";
						회신제목 = 회신제목.Replace(match.Value, "");
					}

					if (string.IsNullOrEmpty(회신계정))
					{
						pattern = @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b";
						regex = new Regex(pattern);

						matches = Regex.Matches(본문, pattern);

						HashSet<string> uniqueEmails = new HashSet<string>();

						// 매치된 이메일 주소 중 회신계정에 포함되는 주소 추가
						foreach (Match match in matches)
						{
							string email = match.Value;
							if (!email.Contains("dintec.co.kr"))
								uniqueEmails.Add(email);
						}

						회신계정 = string.Join(";", uniqueEmails);
					}



					string query = string.Format(@"
DECLARE @MaxDate1 DATE, @MaxDate2 DATE

SELECT @MaxDate1 = ISNULL(MAX(DT_EXPECT), '19000101')
FROM CZ_SA_DEFERRED_DELIVERY
WHERE TP_TYPE = 2 AND NO_SO = '{0}'

SELECT @MaxDate2 = ISNULL(MAX(DT_DUEDATE),'19000101')
FROM SA_SOL
WHERE NO_SO = '{0}' AND DT_DUEDATE IS NOT NULL AND DT_DUEDATE != ''

SELECT CONVERT(VARCHAR, CASE 
    WHEN @MaxDate1 > @MaxDate2 THEN @MaxDate1
    ELSE @MaxDate2
END, 112) AS MaxDate
", FILE_NO);

					DataTable dtFile = DBMgr.GetDataTable(query);

					Outlook.Accounts accounts = app.Session.Accounts;
					Outlook.Account senderAccount = null;

					foreach (Outlook.Account account in accounts)
					{
						if (account.SmtpAddress.Equals("log1@dintec.co.kr", StringComparison.OrdinalIgnoreCase))
						{
							senderAccount = account;
							break;
						}
					}

					if (dtFile.Rows.Count > 0 && !string.IsNullOrEmpty(FILE_NO))
					{
						날짜 = dtFile.Rows[0][0].ToString().Trim();
						// 데이터 표시 형식 변경
						날짜 = FormatDateString(날짜);

						if (!string.IsNullOrEmpty(날짜))
						{

							replyMail = mailitem.Reply();
							replyMail.SendUsingAccount = senderAccount;

							//replyMail.SenderEmailAddress = "log1@dintec.co.kr";
							//replyMail.SenderName = "Log / Dintec";
							replyMail.To = 회신계정;
							replyMail.BCC = mailitem.Sender.Address;
							replyMail.Subject = "FW: " + 회신제목;
							string replyBodyStr = string.Format(@"Good day, Sir & Madam

Order will be ready {0} as per the original lead time. 

Please do not hesitate to get in touch with us for any assistance.

Thank you for your working with us.", 날짜);

							string replyBody = FormatEmailReply(mailitem.Body, replyBodyStr);

							replyMail.Body = replyBody;

							replyMail.Send();
						}
						else
						{
							replyMail = mailitem.Reply();
							replyMail.SendUsingAccount = senderAccount;
							replyMail.To = mailitem.Sender.Address;
							replyMail.Subject = "FW: 파일번호 오류 " + 회신제목;

							replyMail.Send();
						}
					}
					else
					{
						replyMail = mailitem.Reply();
						replyMail.SendUsingAccount = senderAccount;
						replyMail.To = mailitem.Sender.Address;
						replyMail.Subject = "FW: 파일번호 누락 " + 회신제목;

						replyMail.Send();
					}


					resultCode = "REPLY";

					System.Runtime.InteropServices.Marshal.ReleaseComObject(replyMail);
				}
			}
			catch (Exception ex)
			{
				msgStr = "납기회신 오류";
				resultCode = "SENDMAIL";
				sendMailSys(mailitem, tbx전달.Text, "", "");
			}
		}



		private void DeliveryUploadFunc(Outlook.MailItem mailitem)
		{
			try
			{
				string 메일제목 = mailitem.Subject.ToString();

				if (메일제목.Contains("#납품지시"))
				{
					// 파일코드
					string fileCode = string.Join("|", FileCode());
					FILE_NO = string.Join(",", Regex.Matches(메일제목, "(" + fileCode + ")" + @"2\d{5,7}-{0,1}\d{0,2}").Cast<Match>());

					string query = "SELECT EMP.*, PA.LN_PARTNER  FROM V_CZ_SA_QTN_LOG_EMP AS EMP JOIN CZ_SA_QTNH AS QT ON QT.CD_COMPANY = EMP.CD_COMPANY AND QT.NO_FILE = EMP.NO_FILE JOIN CZ_MA_PARTNER AS PA  ON PA.CD_COMPANY = QT.CD_COMPANY AND QT.CD_PARTNER = PA.CD_PARTNER WHERE EMP.NO_FILE = '";
					query = query + FILE_NO + "'";
					DataTable dtFile = DBMgr.GetDataTable(query);


					if (dtFile.Rows.Count > 0 && !string.IsNullOrEmpty(FILE_NO))
					{
						string contents = @"** 납품지시서 등록 알림

- 파일번호 : {0}
- 매출처 : {1}

※ 본 쪽지는 발신 전용 입니다.";

						contents = string.Format(contents, FILE_NO, dtFile.Rows[0]["LN_PARTNER"].ToString());

						Messenger.SendMSG(new string[] { dtFile.Rows[0]["CD_FLAG1"].ToString(), dtFile.Rows[0]["CD_FLAG2"].ToString() }, contents);
					}
				}

				AttDeliveryUp();
			}
			catch (Exception ex)
			{
				msgStr = "납품지시서 업로드 오류";
				resultCode = "SENDMAIL";
				sendMailSys(mailitem, tbx전달.Text, "", "");
			}
		}

		#region ▣ 첨부파일업로드 QUOTATION
		private void QuotationFunc(Outlook.MailItem mailitem)
		{
			try
			{
				if (CODE.Equals("UPLOAD_QT"))
				{
					if ((MAIL_FROM_DB_S.ToLower().Contains("@dintec.co.kr") || MAIL_FROM_DB_S.ToLower().Contains("@dubheco.com"))
						&& (!MAIL_FROM_DB_S.Contains("service@dubheco.com") && !MAIL_FROM_DB_S.Contains("service@dintec.co.kr")))
					{
						NO_EMAIL = NO_EMAIL + ";" + MAIL_FROM_DB_S;
					}


					int idx_s = -1;
					int idx_e = -1;

					idx_s = MAIL_SUBJECT.IndexOf("INQUIRY(");

					if (idx_s != -1)
						FILE_NO = MAIL_SUBJECT.Substring(idx_s, 18).Replace("INQUIRY(", "").Trim();

					if (FILE_NO.StartsWith("ST"))
					{
						FILE_NO = MAIL_SUBJECT.Substring(idx_s, 20).Replace("INQUIRY(", "").Trim();
					}

					if (!FILE_NO.Contains("-"))
					{
						FILE_NO = FILE_NO.Substring(0, 10).Trim();
					}

					idx_e = MAIL_SUBJECT.IndexOf("_");

					if (idx_e == -1)
						FILE_SUPPLY = MAIL_SUBJECT.Substring(MAIL_SUBJECT.Length - 5, 5).Trim();
					else
						FILE_SUPPLY = MAIL_SUBJECT.Substring(idx_e, MAIL_SUBJECT.Length - idx_e);

					idx_s = -1;
					idx_s = MAIL_SUBJECT.IndexOf("IMO");


					// 서원텍
					//if (FILE_SUPPLY.StartsWith("_126"))
					//	if(!FILE_SUPPLY.Contains("12629"))
					//		if(!FILE_SUPPLY.Contains("12606"))
					//			FILE_SUPPLY = "12609";

					if (MAIL_FROM_DB_S.Equals("swt-valve@daum.net"))
						FILE_SUPPLY = "12609";

					if (idx_s != -1 && MAIL_SUBJECT.Length > idx_s + 10)
						NO_IMO = MAIL_SUBJECT.Substring(idx_s, 10).Trim();

					if ((string.IsNullOrEmpty(FILE_NO) || NO_IMO.Contains(FILE_SUPPLY)) && MAIL_TO.ToLower().Equals("service@dintec.co.kr"))
					{

						msgStr = "미분류";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
					else if ((string.IsNullOrEmpty(FILE_NO) || NO_IMO.Contains(FILE_SUPPLY)) && MAIL_TO.ToLower().Equals("service@dubheco.com"))
					{
						msgStr = "미분류";
						if (string.IsNullOrEmpty(NO_EMAIL.Replace(";", "").Trim()))
						{
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, "notice@dubheco.com", "", "");
						}
						else
						{
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, NO_EMAIL, "", "");
						}
					}
					else if (MAIL_TO.Contains("service@dintec.co.kr") && MAIL_FROM_DB_S.Contains("@dintec.co.kr"))
					{
						msgStr = "미분류";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, NO_EMAIL, "", "");
					}
					else if (MAIL_TO.Contains("service@dubheco.com") && MAIL_FROM_DB_S.Contains("@dubheco.com"))
					{
						msgStr = "미분류";

						if (string.IsNullOrEmpty(NO_EMAIL.Replace(";", "").Trim()))
						{
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, "notice@dubheco.com", "", "");
						}
						else
						{
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, NO_EMAIL, "", "");
						}
					}
					else
					{
						if (string.IsNullOrEmpty(FILE_SUPPLY))
							FILE_SUPPLY = MAIL_SUBJECT.Substring(MAIL_SUBJECT.Length - 5, 5);

						if (FILE_SUPPLY.EndsWith("]"))
							FILE_SUPPLY = FILE_SUPPLY.Replace("]", "").Trim();

						if (FILE_SUPPLY.Length > 10)
						{
							string _supplyName = FILE_SUPPLY.Substring(1, 5).Trim();
							if (!GetTo.IsInt(_supplyName))
							{
								FILE_SUPPLY = FILE_SUPPLY.Substring(FILE_SUPPLY.Length - 5, 5).Trim();
							}
							else
							{
								FILE_SUPPLY = _supplyName;
							}
						}

						else if (FILE_SUPPLY.Length == 6)
							FILE_SUPPLY = FILE_SUPPLY.Replace("_", "").Trim();


						if (FILE_SUPPLY.Length != 5 && CD_COMPANY.Equals("K100"))
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
							return;
						}
						else if (FILE_SUPPLY.Length != 5 && CD_COMPANY.Equals("K200"))
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
							return;
						}

						if (FILE_SUPPLY.Length == 5 && !string.IsNullOrEmpty(FILE_NO) && GetTo.IsInt(FILE_SUPPLY))
						{
							AttSave();
						}
						else
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
						}
					}
				}
				else if (CODE.Equals("UPLOAD_ETC"))
				{
					int idx_s = -1;

					idx_s = MAIL_SUBJECT.IndexOf("INQUIRY(");

					if (idx_s != -1)
						FILE_NO = MAIL_SUBJECT.Substring(idx_s, 18).Replace("INQUIRY(", "").Trim();


					FILE_SUPPLY = CD_PARTNER;

					if (!string.IsNullOrEmpty(FILE_SUPPLY) && !string.IsNullOrEmpty(FILE_NO))
					{
						AttSave();

						if (FILE_SUPPLY == "00047")
						{
							DataTable dt = SQL.GetDataTable(@"SELECT 1 FROM CZ_SA_QTNH QH
JOIN CZ_PU_QTNH QH1 ON QH1.CD_COMPANY = QH.CD_COMPANY AND QH1.NO_FILE = QH.NO_FILE
JOIN MA_EMP ME ON ME.CD_COMPANY = QH.CD_COMPANY AND ME.NO_EMP = QH.NO_EMP_QTN
WHERE QH.CD_COMPANY = 'K100'
AND QH.NO_FILE = '" + FILE_NO + @"'
AND QH1.CD_PARTNER = '00047'
AND ME.CD_DEPT = '010900'");

							if (dt != null && dt.Rows.Count > 0)
							{
								string contents = @"** 매입견적서 등록 알림

- 매입처 : {0}
- 파일번호 : {1}

※ 본 쪽지는 발신 전용 입니다.";

								contents = string.Format(contents, FILE_SUPPLY, FILE_NO);

								Messenger.SendMSG(new string[] { "S-495", "S-579", "S-596" }, contents);

								LogWrite(Color.DodgerBlue, "[쪽지발송]	" + mailitem.Subject + " / 동화엔텍 수신 쪽지 / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
								LogWrite(Color.DodgerBlue, "[쪽지발송]	" + mailitem.Subject + " / 동화엔텍 수신 쪽지 / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
							}
						}
					}
					else
					{
						msgStr = "거래처코드 확인 안됨";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
				}
				else if (CODE.Equals("UPLOAD_SWT"))
				{
					int idx_s = -1;

					idx_s = MAIL_SUBJECT.IndexOf("INQUIRY(");

					if (idx_s != -1)
						FILE_NO = MAIL_SUBJECT.Substring(idx_s, 18).Replace("INQUIRY(", "").Trim();

					FILE_SUPPLY = CD_PARTNER;

					if (FILE_SUPPLY.Length == 5 && !string.IsNullOrEmpty(FILE_NO) && GetTo.IsInt(FILE_SUPPLY))
					{
						AttSave();
					}
					else
					{
						msgStr = "거래처코드 확인 안됨";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
				}
				else if (CODE.Equals("UPLOAD_WOOYANG"))
				{
					int idx_s = -1;

					idx_s = MAIL_SUBJECT.IndexOf("견적서/");

					if (idx_s != -1 && !FILE_NO.StartsWith("A-"))
					{
						FILE_SUPPLY = CD_PARTNER;

						FILE_NO = MAIL_SUBJECT.Substring(idx_s, 14).Replace("견적서/", "").Trim();

						if (FILE_SUPPLY.Length == 5 && !string.IsNullOrEmpty(FILE_NO) && GetTo.IsInt(FILE_SUPPLY))
						{
							AttSave();
						}
						else
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
						}
					}

					else
					{
						msgStr = "거래처코드 확인 안됨";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
				}
				else if (CODE.Equals("UPLOAD_OMCEAST"))
				{
					int idx_s = MAIL_SUBJECT.IndexOf("(");

					if (idx_s != -1)
					{
						FILE_SUPPLY = CD_PARTNER;
						FILE_NO = MAIL_SUBJECT.Substring(idx_s, 11).Replace("(", "").Trim();

						if (FILE_SUPPLY.Length == 5 && !string.IsNullOrEmpty(FILE_NO) && GetTo.IsInt(FILE_SUPPLY))
						{
							AttSave();
						}
						else
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
						}
					}
					else
					{
						msgStr = "거래처코드 확인 안됨";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
				}
				else if (CODE.Equals("UPLOAD_YOUNGNAM"))
				{
					int idx_s = MAIL_SUBJECT.IndexOf("유닉스스틸 견적서 입니다.");

					if (idx_s != -1)
					{
						FILE_SUPPLY = CD_PARTNER;
						FILE_NO = MAIL_SUBJECT.Replace("유닉스스틸 견적서 입니다.", "").Trim();

						if (FILE_SUPPLY.Length == 5 && !string.IsNullOrEmpty(FILE_NO) && GetTo.IsInt(FILE_SUPPLY))
						{
							AttSave();
						}
						else
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
						}
					}
					else
					{
						msgStr = "거래처코드 확인 안됨";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
				}
				else if (CODE.Equals("UPLOAD_HSD"))
				{
					string[] fileNoSpl = MAIL_SUBJECT.Split('_');


					if (fileNoSpl.Length == 2)
					{
						FILE_SUPPLY = "01340";
						FILE_NO = fileNoSpl[1].ToString().Trim();

						if (FILE_NO.Length > 10)
						{
							FILE_NO = FILE_NO.Substring(0, 10).Trim();
						}

						if (FILE_SUPPLY.Length == 5 && !string.IsNullOrEmpty(FILE_NO) && GetTo.IsInt(FILE_SUPPLY))
						{
							AttSave();
						}
						else
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
						}
					}
					else
					{
						msgStr = "거래처코드 확인 안됨";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
				}
				else if (CODE.Equals("UPLOAD_HSD_ORDER"))
				{
					string[] fileNoSpl = MAIL_SUBJECT.Split('_');


					if (fileNoSpl.Length == 2)
					{
						FILE_SUPPLY = "01340";
						FILE_NO = fileNoSpl[1].ToString().Trim();

						if (FILE_NO.Length > 10)
						{
							FILE_NO = FILE_NO.Substring(0, 10).Trim();
						}

						if (FILE_SUPPLY.Length == 5 && !string.IsNullOrEmpty(FILE_NO) && GetTo.IsInt(FILE_SUPPLY))
						{
							AttSaveOrder();
						}
						else
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
						}
					}
					else
					{
						msgStr = "거래처코드 확인 안됨";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
				}
				else if (CODE.Equals("UPLOAD_DHET"))
				{
					FILE_SUPPLY = "00047";

					int idx_s = -1;

					idx_s = MAIL_SUBJECT.IndexOf("QUOTATION FOR");

					if (idx_s != -1)
					{
						string noStr = MAIL_SUBJECT.Substring(idx_s, 24).Replace("QUOTATION FOR", "").Trim();
						FILE_NO = noStr;

						if (FILE_SUPPLY.Length == 5 && !string.IsNullOrEmpty(FILE_NO) && GetTo.IsInt(FILE_SUPPLY))
						{
							AttSave();

							if (CD_COMPANY == "K100")
							{
								DataTable dt = SQL.GetDataTable(@"SELECT 1 FROM CZ_SA_QTNH QH
JOIN CZ_PU_QTNH QH1 ON QH1.CD_COMPANY = QH.CD_COMPANY AND QH1.NO_FILE = QH.NO_FILE
JOIN MA_EMP ME ON ME.CD_COMPANY = QH.CD_COMPANY AND ME.NO_EMP = QH.NO_EMP_QTN
WHERE QH.CD_COMPANY = 'K100'
AND QH.NO_FILE = '" + FILE_NO + @"'
AND QH1.CD_PARTNER = '00047'
AND ME.CD_DEPT = '010900'");

								if (dt != null && dt.Rows.Count > 0)
								{
									string contents = @"** 매입견적서 등록 알림

- 매입처 : {0}
- 파일번호 : {1}

※ 본 쪽지는 발신 전용 입니다.";

									contents = string.Format(contents, FILE_SUPPLY, FILE_NO);

									Messenger.SendMSG(new string[] { "S-495", "S-579", "S-596" }, contents);

									LogWrite(Color.DodgerBlue, "[쪽지발송]	" + mailitem.Subject + " / 동화엔텍 수신 쪽지 / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
								}

							}
						}
						else
						{
							msgStr = "거래처코드 확인 안됨";
							resultCode = "SENDMAIL";
							sendMailSys(mailitem, tbx전달.Text, "", "");
						}
					}
					else
					{
						msgStr = "거래처코드 확인 안됨";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, tbx전달.Text, "", "");
					}
				}
				else
				{
					msgStr = "거래처코드 확인 안됨";
					resultCode = "SENDMAIL";
					sendMailSys(mailitem, tbx전달.Text, "", "");
				}
			}
			catch (Exception ex)
			{
				msgStr = "거래처코드 확인 안됨";
				resultCode = "SENDMAIL";
				sendMailSys(mailitem, tbx전달.Text, "", "");
			}
		}

		private void AttSave()
		{
			// 둘중 하나라도 없으면 리턴
			if ((string.IsNullOrEmpty(FILE_NO) || string.IsNullOrEmpty(FILE_SUPPLY)))
				return;

			try
			{
				string query = "SELECT NO_FILE FROM CZ_SA_QTNH WHERE CD_COMPANY = '" + CD_COMPANY + "' AND NO_FILE_OLD = '" + FILE_NO + "'";
				DataTable dt = DBMgr.GetDataTable(query);

				if (dt.Rows.Count > 0)
				{
					FILE_NO = dt.Rows[0][0].ToString();
				}

				inquiryMailUp.SaveEmail(CD_COMPANY, FILE_NO, "", "04", "", FILE_SUPPLY, fileName);

				System.Threading.Thread.Sleep(100);

				cd_send = "UP";

				LogWrite(Color.DodgerBlue, "[메일업로드]	" + mailitem.Subject + " / " + FILE_SUPPLY + " / " + FILE_NO);

				DBUserSelect("FILE");

				if (!msgId.Contains(","))
				{
					AFTER_MAIL_NAME = "[" + FILE_NO + "/UPLOAD]";
					msgStr = "매입견적서";


					SendMailServer();

					if (CD_COMPANY.Equals("K100"))
					{
						if (mailitem != null)
							mailitem.Move(uploadFolder);
					}
					else if (CD_COMPANY.Equals("K200"))
					{
						if (mailitem != null)
							mailitem.Move(duuploadFolder);
					}
				}
				else
				{
					errorMsg = errorMsg + "메일키 2개 이상";
					resultCode = "SENDMAIL";
					sendMailSys(mailitem, NO_EMAIL, "", "");
				}
			}
			catch (Exception e)
			{
				txtLog.AppendText(e + " / " + MAIL_SUBJECT);
				MailMoveFolderUn();
			}
		}

		private void AttDeliveryUp()
		{
			if (string.IsNullOrEmpty(FILE_NO))
			{
				msgStr = "납품지시서 파싱 실패";
				resultCode = "SENDMAIL";
				sendMailSys(mailitem, NO_MAIL, "", "");
			}
			else
			{
				try
				{
					string query = "SELECT NO_FILE FROM CZ_SA_QTNH WHERE CD_COMPANY = '" + CD_COMPANY + "' AND NO_FILE_OLD = '" + FILE_NO + "'";
					DataTable dt = DBMgr.GetDataTable(query);

					if (dt.Rows.Count > 0)
					{
						FILE_NO = dt.Rows[0][0].ToString();
					}

					inquiryMailUp.SaveEmail(CD_COMPANY, FILE_NO, "", "51", "", FILE_SUPPLY, fileName);

					System.Threading.Thread.Sleep(100);

					cd_send = "UP";

					LogWrite(Color.DodgerBlue, "[납품지시서]	" + mailitem.Subject + " / " + FILE_NO);

					DBUserSelect("FILE");

					msgStr = "납품지시서";

					if (CD_COMPANY.Equals("K100"))
					{
						if (mailitem != null)
							mailitem.Move(uploadFolder);
					}
					else if (CD_COMPANY.Equals("K200"))
					{
						if (mailitem != null)
							mailitem.Move(duuploadFolder);
					}
				}
				catch (Exception e)
				{
					txtLog.AppendText(e + " / " + MAIL_SUBJECT);
					MailMoveFolderUn();
				}
			}
		}

		private void AttOrderUp()
		{
			if (string.IsNullOrEmpty(FILE_NO))
			{
				msgStr = "고객발주서파일번호파싱실패";
				resultCode = "SENDMAIL";
				sendMailSys(mailitem, tbx전달.Text, "", "");
			}
			else
			{
				try
				{
					string query = "SELECT NO_FILE FROM CZ_SA_QTNH WHERE CD_COMPANY = '" + CD_COMPANY + "' AND NO_FILE_OLD = '" + FILE_NO + "'";
					DataTable dt = DBMgr.GetDataTable(query);

					if (dt.Rows.Count > 0)
					{
						FILE_NO = dt.Rows[0][0].ToString();
					}

					inquiryMailUp.SaveEmail(CD_COMPANY, FILE_NO, "", "08", "", FILE_SUPPLY, fileName);

					System.Threading.Thread.Sleep(100);

					cd_send = "UP";

					LogWrite(Color.DodgerBlue, "[발주업로드]	" + mailitem.Subject + " / " + FILE_NO);

					DBUserSelect("FILE");

					if (!msgId.Contains(","))
					{
						AFTER_MAIL_NAME = "[" + FILE_NO + "/ORDER UPLOAD]";
						msgStr = "고객발주서";

						SendMailServer();

						if (CD_COMPANY.Equals("K100"))
						{
							if (mailitem != null)
								mailitem.Move(uploadFolder);
						}
						else if (CD_COMPANY.Equals("K200"))
						{
							if (mailitem != null)
								mailitem.Move(duuploadFolder);
						}
					}
					else
					{
						errorMsg = errorMsg + "메일키 2개 이상";
						resultCode = "SENDMAIL";
						sendMailSys(mailitem, NO_EMAIL, "", "");
					}
				}
				catch (Exception e)
				{
					txtLog.AppendText(e + " / " + MAIL_SUBJECT);
					MailMoveFolderUn();
				}
			}
		}


		private void AttSaveOrder()
		{
			// 매입확정서 업로드 두베코  50
			// 둘중 하나라도 없으면 리턴
			if (string.IsNullOrEmpty(FILE_NO) || string.IsNullOrEmpty(FILE_SUPPLY))
				return;

			try
			{
				string query = "SELECT NO_FILE FROM CZ_SA_QTNH WHERE CD_COMPANY = '" + CD_COMPANY + "' AND NO_FILE_OLD = '" + FILE_NO + "'";
				DataTable dt = DBMgr.GetDataTable(query);

				if (dt.Rows.Count > 0)
				{
					FILE_NO = dt.Rows[0][0].ToString();
				}

				inquiryMailUp.SaveEmail(CD_COMPANY, FILE_NO, "", "50", "", FILE_SUPPLY, fileName);

				System.Threading.Thread.Sleep(100);

				cd_send = "UP";

				LogWrite(Color.DodgerBlue, "[메일업로드]	" + mailitem.Subject + " / " + FILE_SUPPLY + " / " + FILE_NO);

				DBUserSelect("FILE");

				if (!msgId.Contains(","))
				{
					AFTER_MAIL_NAME = "[" + FILE_NO + "/UPLOAD]";
					msgStr = "매입확정서 업로드";
					SendMailServer();

					if (CD_COMPANY.Equals("K100"))
					{
						if (mailitem != null)
							mailitem.Move(uploadFolder);
					}
					else if (CD_COMPANY.Equals("K200"))
					{
						if (mailitem != null)
							mailitem.Move(duuploadFolder);
					}
				}
				else
				{
					errorMsg = errorMsg + "메일키 2개 이상";
					resultCode = "SENDMAIL";
					sendMailSys(mailitem, NO_EMAIL, "", "");
				}
			}
			catch (Exception e)
			{
				txtLog.AppendText(e + " / " + MAIL_SUBJECT);
				MailMoveFolderUn();
			}
		}


		private void ProcedureInsert(string fileNo, string realName, string fileNameStr, string FILE_SUPPLY, string YN_PARSING, string YN_INCLUDED)
		{
			SqlConnection sqlConn;

			try
			{
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
					cmd.Parameters.Add("P_YN_INCLUDED", SqlDbType.NVarChar, 50);
					cmd.Parameters.Add("P_ID_INSERT", SqlDbType.NVarChar, 50);

					//@P_YN_INCLUDED

					cmd.Parameters["P_CD_COMPANY"].Value = CD_COMPANY;
					cmd.Parameters["P_TP_STEP"].Value = "04";
					cmd.Parameters["P_NO_KEY"].Value = fileNo;
					cmd.Parameters["P_NM_FILE"].Value = fileNameStr + ".msg";
					cmd.Parameters["P_NM_FILE_REAL"].Value = realName;
					cmd.Parameters["P_CD_SUPPLIER"].Value = FILE_SUPPLY;
					cmd.Parameters["P_YN_PARSING"].Value = YN_PARSING;
					cmd.Parameters["P_YN_INCLUDED"].Value = YN_INCLUDED;
					cmd.Parameters["P_ID_INSERT"].Value = NO_EMP;

					reader = cmd.ExecuteReader();
					reader.Close();
					sqlConn.Close();
				}

			}
			catch (Exception e)
			{
				txtLog.AppendText(e + " / " + MAIL_SUBJECT);
			}
			finally
			{
				//sqlConn.Close();
			}
		}

		#endregion ▣ 첨부파일업로드 QUOTATION

		#region ▣ 파일번호 추출, 담당자 선택

		private void FileNoGet()
		{
			if (CODE.Equals("SEND_VSHIP"))
			{
				int idx_s = MAIL_BODY.IndexOf("Reference");

				if (idx_s != -1)
					FILE_NO = MAIL_BODY.Substring(idx_s, 20).Replace("Reference", "").Trim();


				if (!string.IsNullOrEmpty(FILE_NO))
				{
					DBUserSelect("FILE");
				}
			}
			else if (CODE.Equals("SEND_WOOYANG"))
			{
				if (MAIL_SUBJECT.Length > 11)
					FILE_NO = MAIL_SUBJECT.Substring(0, 10).Trim();

				if (!string.IsNullOrEmpty(FILE_NO))
				{
					DBUserSelect("FILE");
				}
			}
			else if (CODE.Equals("SEND_VSHIPS_INVOICE"))
			{
				int idx_s = -1;

				idx_s = MAIL_BODY.IndexOf("Invoice Number");

				if (idx_s != -1)
					FILE_NO = MAIL_BODY.Substring(idx_s, 26).Replace("Invoice Number:", "").Trim();

				if (!string.IsNullOrEmpty(FILE_NO))
				{
					DBUserSelect("INVOICE");
				}

			}
			else if (CODE.Equals("SEND_ORDER"))
			{
				int idx_s = -1;

				idx_s = MAIL_SUBJECT.IndexOf("ORDER SHEET(");

				if (idx_s != -1)
					FILE_NO = MAIL_SUBJECT.Substring(idx_s, 22).Replace("ORDER SHEET(", "").Replace(")", "").Trim();

				if (!string.IsNullOrEmpty(FILE_NO))
				{
					DBUserSelect("FILE");
				}
			}

			msgStr = "파일번호로 담당자 검색";
		}

		private void DBUserSelect(string fileNoKind)
		{
			string query = string.Empty;
			DataTable dt = null;

			if (fileNoKind.Equals("FILE"))
			{
				query = "SELECT NO_EMAIL,NO_EMP,NM_KOR FROM MA_EMP WHERE CD_COMPANY = '" + CD_COMPANY + "' AND NO_EMP IN (SELECT NO_EMP FROM CZ_SA_QTNH WHERE NO_FILE = '" + FILE_NO + "' OR NO_FILE_OLD = '" + FILE_NO + "')";

				dt = DBMgr.GetDataTable(query);
			}
			else if (fileNoKind.Equals("INVOICE"))
			{
				query = "SELECT NO_EMAIL,NO_EMP,NM_KOR FROM MA_EMP WHERE CD_COMPANY = '" + CD_COMPANY + "' AND NO_EMP IN (SELECT TOP 1 NO_EMP FROM SA_IVL WHERE NO_IV = '" + FILE_NO + "')";

				dt = DBMgr.GetDataTable(query);
			}

			if (dt.Rows.Count > 0)
			{
				NO_EMAIL = NO_EMAIL.ToLower().Replace("sangwon.ha@dintec.co.kr", "").Replace("dayoung.kim@dubheco.com", "") + ";" + dt.Rows[0]["NO_EMAIL"].ToString();
				NO_EMP = dt.Rows[0]["NO_EMP"].ToString();
				NM_KOR = dt.Rows[0]["NM_KOR"].ToString();
			}
		}

		#endregion ▣ 파일번호 추출, 담당자 검색

		#region ▣ 사용자 메일 ID 검색
		// 사용자 검색해서 테이블 저장
		private DataTable DBMailBoxListSelect(string mail)
		{
			DataTable dt = null;
			System.Data.DataSet ds = new System.Data.DataSet();
			string connectStr = "Server=192.168.1.2; Database=MCE7; Uid=sa; Password=!q7hfnl3sh62@";
			string query = string.Empty;

			if (CD_COMPANY == "K100")
			{
				query = "select r.mb_id, du_name, du_userId from McDomainUser u, McRsMailBox r, McMailboxFolder f where u.du_id = r.mb_owner_id and r.mb_id = f.mb_id AND mb_name = 'mcinbox'";
				query = query + " AND md_parent = '00000000-0000-0000-0000-000000000000'";
				query = query + " AND dm_id = '12DB6239-B536-4495-8D6D-203E5E902252'";
				query = query + " AND du_userId in( '" + mail + "')";
			}
			else if (CD_COMPANY == "K200")
			{
				query = "select r.mb_id, du_name, du_userId from McDomainUser u, McRsMailBox r, McMailboxFolder f where u.du_id = r.mb_owner_id and r.mb_id = f.mb_id AND mb_name = 'mcinbox'";
				query = query + " AND md_parent = '00000000-0000-0000-0000-000000000000'";
				query = query + " AND dm_id = '702D2019-D54B-44A8-80D5-086626D50D06'";
				query = query + " AND du_userId in( '" + mail + "')";
			}

			SqlConnection sqlConn = new SqlConnection(connectStr);

			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = connectStr;
				conn.Open();

				SqlCommand sqlComm = new SqlCommand(query, conn);
				SqlDataReader reader = sqlComm.ExecuteReader();

				dt = GetTable(reader);
			}

			return dt;
		}
		#endregion 사용자 메일 ID 검색

		#region ▣ 메일 key 검색
		// 메일키를 가지고 메일 검색해서 테이블로 저장
		private DataTable DBMailBoxKeySelect(string mailkey)
		{
			DataTable dt = null;
			System.Data.DataSet ds = new System.Data.DataSet();
			string connectStr = "Server=192.168.1.2; Database=MCE7; Uid=sa; Password=!q7hfnl3sh62@";

			string query = "select * from McMessageId2Mid where msg_id = '" + mailkey.Trim() + "'";
			SqlConnection sqlConn = new SqlConnection(connectStr);

			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = connectStr;
				conn.Open();

				SqlCommand sqlComm = new SqlCommand(query, conn);
				SqlDataReader reader = sqlComm.ExecuteReader();

				dt = GetTable(reader);
			}

			return dt;
		}
		#endregion 메일 key 검색

		#region ▣ 메일 TO, CC 검색

		//private void mailToSearch(MapiMessage msg)
		//{
		//	if (!string.IsNullOrEmpty(msg.Headers["To"]))              // 받는사람
		//	{
		//		string mail_to = msg.Headers["To"];
		//		int idx_s = -1;

		//		if (mail_to.Contains("<") && mail_to.Contains(">"))
		//		{
		//			string[] mailToSpl = mail_to.Split(',');

		//			if (mailToSpl.Length > 0)
		//			{
		//				for (int c = 0; c < mailToSpl.Length; c++)
		//				{
		//					idx_s = mailToSpl[c].IndexOf("<");
		//					if (idx_s != -1)
		//					{
		//						MAIL_TO += mailToSpl[c].Substring(idx_s, mailToSpl[c].Length - idx_s).Replace("<", "").Replace(">", "").Trim() + ";";
		//					}
		//					else
		//					{
		//						MAIL_TO += mailToSpl[c] + ";";
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			MAIL_TO = mail_to;
		//		}

		//		if (mailitem != null)
		//		{
		//			if (!string.IsNullOrEmpty(mailitem.To) && mailitem.To != null && string.IsNullOrEmpty(MAIL_TO))
		//				MAIL_TO = mailitem.To.ToString().Replace("'", "");
		//		}
		//	}
		//	else
		//	{
		//		if (mailitem != null)
		//		{
		//			if (!string.IsNullOrEmpty(mailitem.To) && mailitem.To != null)
		//				MAIL_TO = mailitem.To.ToString().Replace("'", "");
		//		}
		//	}

		//	MAIL_TO = MAIL_TO.Replace("'", "").Replace(",", ";").Trim();
		//}


		//private void mailCCSearch(MapiMessage msg)
		//{
		//	if (!string.IsNullOrEmpty(msg.Headers["Cc"]))
		//	{
		//		string mail_cc = msg.Headers["Cc"];
		//		int idx_s = -1;

		//		if (mail_cc.Contains("<") && mail_cc.Contains(">"))
		//		{
		//			string[] mailCcSpl = mail_cc.Split(',');

		//			if (mailCcSpl.Length > 0)
		//			{
		//				for (int c = 0; c < mailCcSpl.Length; c++)
		//				{
		//					idx_s = mailCcSpl[c].IndexOf("<");
		//					if (idx_s != -1)
		//					{
		//						MAIL_CC += mailCcSpl[c].Substring(idx_s, mailCcSpl[c].Length - idx_s).Replace("<", "").Replace(">", "").Trim() + ";";
		//						idx_s = -1;
		//					}
		//					else
		//					{
		//						MAIL_CC += mailCcSpl[c] + ";";
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			MAIL_CC = mail_cc;
		//		}

		//		if (mailitem != null)
		//		{
		//			if (!string.IsNullOrEmpty(mailitem.CC) && mailitem.CC != null && string.IsNullOrEmpty(MAIL_CC))
		//				MAIL_CC = mailitem.CC.ToString();                                        // 참조
		//		}
		//	}
		//	else
		//	{
		//		if (mailitem != null)
		//		{
		//			if (!string.IsNullOrEmpty(mailitem.CC) && mailitem.CC != null)
		//				MAIL_CC = mailitem.CC.ToString();                                        // 참조
		//		}
		//	}


		//	MAIL_CC = MAIL_CC.Replace("'", "").Replace(",", ";").Trim();
		//}

		private void mailToCcSearch(MapiMessage msg)
		{
			string[] mailToSplSt = MAIL_TO.Split(';');
			string reMail_to = string.Empty;
			if (mailToSplSt.Length > 0)
			{

				for (int c = 0; c < mailToSplSt.Length; c++)
				{
					if (mailToSplSt[c].ToString().ToLower().Contains("@dintec.co.kr") || mailToSplSt[c].ToString().ToLower().Contains("@dubheco.com"))
					{
						reMail_to += mailToSplSt[c].ToString().Trim() + ";";
					}
				}
			}

			string[] mailCcSplSt = MAIL_CC.Split(';');
			string reMail_cc = string.Empty;
			if (mailCcSplSt.Length > 0)
			{

				for (int c = 0; c < mailCcSplSt.Length; c++)
				{
					if (mailCcSplSt[c].ToString().ToLower().Contains("@dintec.co.kr") || mailCcSplSt[c].ToString().ToLower().Contains("@dubheco.com"))
					{
						reMail_cc += mailCcSplSt[c].ToString().Trim() + ";";
					}
				}
			}

			if (!string.IsNullOrEmpty(reMail_to))
				MAIL_TO = reMail_to.ToLower().Trim();

			if (!string.IsNullOrEmpty(reMail_cc))
				MAIL_CC = reMail_cc.ToLower().Trim();


			if (MAIL_TO.EndsWith(";"))
				MAIL_TO = MAIL_TO.Substring(0, MAIL_TO.Length - 1).Trim();

			if (MAIL_CC.EndsWith(";"))
				MAIL_CC = MAIL_CC.Substring(0, MAIL_CC.Length - 1).Trim();

			if (mailitem != null)
			{
				if (!string.IsNullOrEmpty(mailitem.BCC) && mailitem.BCC != null)
					MAIL_BCC = mailitem.BCC.ToString();                                   // 숨은참조
			}


			if (MAIL_TO.ToLower().Contains("@dintec.co.kr") || MAIL_CC.ToLower().Contains("@dintec.co.kr"))
				CD_COMPANY = "K100";
			else if (MAIL_TO.ToLower().Contains("@dubheco.com") || MAIL_CC.ToLower().Contains("@dubheco.com"))
				CD_COMPANY = "K200";
			else
				CD_COMPANY = "K100";

			// 숨은 참조 수정 사항
			if (!MAIL_TO.ToLower().Contains("@dintec.co.kr") && !MAIL_TO.ToLower().Contains("@dubheco.com") &&
				!MAIL_CC.ToLower().Contains("@dintec.co.kr") && !MAIL_CC.ToLower().Contains("@dubheco.com"))
			{
				string _bcc = msg.Headers["Received"];

				int idx_s = _bcc.IndexOf("<");
				int idx_e = _bcc.IndexOf(">");

				if (idx_s != -1 && idx_e != -1)
					MAIL_TO = _bcc.Substring(idx_s, idx_e - idx_s).Replace("<", "").Replace(">", "");
			}


			if (MAIL_CC.Contains(MAIL_FROM_DB_S.Replace(";", "")))
			{
				MAIL_CC = MAIL_CC.Replace(MAIL_FROM_DB_S.Replace(";", ""), "");
			}
		}

		#endregion ▣ 메일 TO, CC 검색

		#region ▣ 메일 분류기 검색
		private void mailDBSearch()
		{
			dbDatatbResult = null;
			sortTable = null;

			if (!MAIL_TO.Contains(MAIL_CC))
				MAIL_TO_DB_S = MAIL_TO + ";" + MAIL_CC;
			else
				MAIL_TO_DB_S = MAIL_TO;


			if ((MAIL_SUBJECT.Contains("DINTEC - ORDER") || MAIL_SUBJECT.Contains("DINTEC - INQUIRY")) && MAIL_FROM_DB_S.ToLower().Equals("offshore@dintec.co.kr"))
			{
				NO_EMAIL = string.Empty;
			}
			else if ((MAIL_FROM_DB_S.ToLower().EndsWith("@dintec.co.kr") || MAIL_FROM_DB_S.ToLower().EndsWith("@dubheco.com")) && !MAIL_SUBJECT.ToUpper().Contains("DINTEC - INQUIRY") && !MAIL_SUBJECT.ToUpper().Contains("DUBHECO - INQUIRY") && (!MAIL_SUBJECT.ToUpper().Contains("INQUIRY(") && MAIL_SUBJECT.Contains("IMO:"))
				&& !MAIL_SUBJECT.ToUpper().Contains("[우양선기] 견적서") && (!MAIL_SUBJECT.ToLower().Contains("quotation(") && MAIL_TO.ToLower().Contains("upload@dubheco.com")) && (!MAIL_SUBJECT.ToLower().Contains("[order] ") && MAIL_TO.ToLower().Contains("upload@dubheco.com")))
			{
				NO_EMAIL = string.Empty;
			}
			else
			{
				// test
				//MAIL_SUBJECT = "TEST중입니다. DSFSDFDF";
				//MAIL_FROM_DB_S = "licadiom@gmail.com";
				//MAIL_TO_DB_S = "dintec.sales1@dintec.co.kr;sb@dintec.co.kr;db@dintec.co.kr";
				// DB 검색
				GetDBJoin(MAIL_SUBJECT, MAIL_FROM_DB_S, MAIL_BODY, MAIL_TO_DB_S, "", "");
			}

			InqCheck = false;
			UpCheck = false;
		}
		#endregion ▣ 메일 분류기 검색

		#region ▣ 자동 메일 회신
		private void mailAutoReply()
		{
			string query = string.Empty;
			DataTable dt = null;

			string[] receiveSpl = MAIL_TO.Split(';');

			if (receiveSpl.Length > 1)
			{
				for (int c = 0; c < receiveSpl.Length; c++)
				{
					query = "SELECT * FROM CZ_MA_CODEDTL WHERE 1=1 AND CD_FIELD='CZ_MA00037' AND NM_SYSDEF IN ('" + receiveSpl[c].ToString().Replace(";", "").Trim() + "')  AND YN_USE = 'Y' AND CD_FLAG5 >= '" + DateTime.Now.ToString("yyyyMMdd") + "'";
					dt = DBMgr.GetDataTable(query);

					if (dt.Rows.Count > 0)
					{
						ReMailSend(MAIL_FROM_DB_S, dt.Rows[0]["NM_SYSDEF"].ToString(), dt.Rows[0]["CD_FLAG3"].ToString(), "", dt.Rows[0]["CD_FLAG1"].ToString(), dt.Rows[0]["CD_FLAG2"].ToString(), dt.Rows[0]["CD_FLAG4"].ToString());
					}
				}
			}
			else if (receiveSpl.Length == 1)
			{
				query = "SELECT * FROM CZ_MA_CODEDTL WHERE 1=1 AND CD_FIELD='CZ_MA00037' AND NM_SYSDEF IN ('" + receiveSpl[0].ToString().Replace(";", "").Trim() + "')  AND YN_USE = 'Y' AND CD_FLAG5 >= '" + DateTime.Now.ToString("yyyyMMdd") + "'";
				dt = DBMgr.GetDataTable(query);

				if (dt.Rows.Count > 0)
				{
					ReMailSend(MAIL_FROM_DB_S, dt.Rows[0]["NM_SYSDEF"].ToString(), dt.Rows[0]["CD_FLAG3"].ToString(), "", dt.Rows[0]["CD_FLAG1"].ToString(), dt.Rows[0]["CD_FLAG2"].ToString(), dt.Rows[0]["CD_FLAG4"].ToString());
				}
			}
			else if (string.IsNullOrEmpty(MAIL_TO))
			{
				query = "SELECT * FROM CZ_MA_CODEDTL WHERE 1=1 AND CD_FIELD='CZ_MA00037' AND NM_SYSDEF IN ('" + MAIL_TO.Replace(";", "").Trim() + "')  AND YN_USE = 'Y' AND CD_FLAG5 >= '" + DateTime.Now.ToString("yyyyMMdd") + "'";
				dt = DBMgr.GetDataTable(query);

				if (dt.Rows.Count > 0)
				{
					ReMailSend(MAIL_FROM_DB_S, dt.Rows[0]["NM_SYSDEF"].ToString(), dt.Rows[0]["CD_FLAG3"].ToString(), "", dt.Rows[0]["CD_FLAG1"].ToString(), dt.Rows[0]["CD_FLAG2"].ToString(), dt.Rows[0]["CD_FLAG4"].ToString());
				}
			}
		}
		#endregion ▣ 자동 메일 회신

		#region ▣ 팀메일 검색
		// 팀메일 검색해서 테이블로 저장
		private DataTable DBTeamMailSelectDT(string mail)
		{
			DataTable dt = null;
			System.Data.DataSet ds = new System.Data.DataSet();
			string connectStr = "Server=192.168.1.143; Database=NEOE; Uid=NEOE; Password=NEOE";
			string query = string.Empty;

			query = "SELECT * FROM CZ_MA_CODEDTL WHERE 1=1 AND CD_FIELD='CZ_MA00002' AND CD_FLAG1 IN ('" + mail + "')  AND YN_USE = 'Y'";

			SqlConnection sqlConn = new SqlConnection(connectStr);

			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = connectStr;
				conn.Open();

				SqlCommand sqlComm = new SqlCommand(query, conn);
				SqlDataReader reader = sqlComm.ExecuteReader();

				dt = GetTable(reader);
			}

			return dt;
		}



		//private void DBTeamMailSelect(string mailTo)
		//{
		//	string query = string.Empty;
		//	DataTable dt = null;


		//	query = "SELECT * FROM CZ_MA_CODEDTL WHERE 1=1 AND CD_FIELD='CZ_MA00002' AND CD_FLAG1 IN ('" + mailTo + "')  AND YN_USE = 'Y'";
		//	dt = DBMgr.GetDataTable(query);

		//	if (dt.Rows.Count > 0)
		//	{
		//		NO_EMAIL = dt.Rows[0]["CD_FLAG2"].ToString();
		//		NO_EMP = dt.Rows[0]["CD_SYSDEF"].ToString();
		//		NM_KOR = dt.Rows[0]["NM_SYSDEF"].ToString();
		//	}
		//	else
		//	{
		//		NO_EMAIL = tbx전달.Text;
		//		NO_EMP = "*";
		//		NM_KOR = "전달";
		//	}
		//}

		//private void DBTeamCodeSelect(string cdTeam)
		//{
		//	string query = string.Empty;
		//	DataTable dt = null;


		//	query = "SELECT * FROM CZ_MA_CODEDTL WHERE 1=1 AND CD_FIELD='CZ_MA00002' AND CD_SYSDEF ='" + cdTeam + "' AND YN_USE = 'Y'";
		//	dt = DBMgr.GetDataTable(query);

		//	if (dt.Rows.Count == 1)
		//	{
		//		NO_EMAIL = dt.Rows[0]["CD_FLAG2"].ToString().Replace("\n", "");
		//		NO_EMP = dt.Rows[0]["CD_SYSDEF"].ToString();
		//		NM_KOR = dt.Rows[0]["NM_SYSDEF"].ToString();
		//	}
		//}

		#endregion 팀메일 검색


		#region MIDEAST RE SEND CODE 확인
		private void MIDEASTCode(string Rcode, string dtStr)
		{
			if (CD_COMPANY == "K100")
			{
				string contents = @"** MIDEAST CODE 등록 알림

- 코드 : {0}
- 수신시간 : {1}

※ 본 쪽지는 발신 전용 입니다.";

				contents = string.Format(contents, Rcode, dtStr);

				Messenger.SendMSG(new string[] { "S-458", "S-415", "S-229", "S-625" }, contents);
			}
			else if (CD_COMPANY == "K200")
			{
				string contents = @"** MIDEAST CODE 등록 알림

- 코드 : {0}
- 수신시간 : {1}

※ 본 쪽지는 발신 전용 입니다.";

				contents = string.Format(contents, Rcode, dtStr);

				Messenger.SendMSG(new string[] { "S-458", "D-011", "D-024", "D-048", "D-010", "D-053" }, contents);
			}


			LogWrite(Color.DodgerBlue, "[쪽지발송]	" + mailitem.Subject + " / MIDEAST CODE 수신 쪽지 / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
		}
		#endregion ▣ MIDEAST RE SEND CODE 확인


		#region PIL CODE 확인
		private void PILCode(string Rcode, string dtStr)
		{
			if (CD_COMPANY == "K100")
			{
				string contents = @"** PIL CODE 등록 알림

- 코드 : {0}
- 수신시간 : {1}

※ 본 쪽지는 발신 전용 입니다.";

				contents = string.Format(contents, Rcode, dtStr);

				Messenger.SendMSG(new string[] { "S-458", "S-415", "S-229", "S-625" }, contents);
			}
			else if (CD_COMPANY == "K200")
			{
				string contents = @"** PIL CODE 등록 알림

- 코드 : {0}
- 수신시간 : {1}

※ 본 쪽지는 발신 전용 입니다.";

				contents = string.Format(contents, Rcode, dtStr);

				Messenger.SendMSG(new string[] { "S-458", "D-011", "D-045" }, contents);
			}


			LogWrite(Color.DodgerBlue, "[쪽지발송]	" + mailitem.Subject + " / PIL CODE 수신 쪽지 / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
		}
		#endregion ▣ PIL CODE 확인


		#region MISC CODE 확인
		private void MISCCode(string Rcode, string dtStr)
		{
			if (CD_COMPANY == "K100")
			{
				string contents = @"** MISC CODE 등록 알림

- 코드 : {0}
- 수신시간 : {1}

※ 본 쪽지는 발신 전용 입니다.";

				contents = string.Format(contents, Rcode, dtStr);

				Messenger.SendMSG(new string[] { "S-458" }, contents);
			}
			else if (CD_COMPANY == "K200")
			{
				string contents = @"** MISC CODE 등록 알림

- 코드 : {0}
- 수신시간 : {1}

※ 본 쪽지는 발신 전용 입니다.";

				contents = string.Format(contents, Rcode, dtStr);

				Messenger.SendMSG(new string[] { "S-458", "D-011", "D-034" }, contents);
			}

			LogWrite(Color.DodgerBlue, "[쪽지발송]	" + mailitem.Subject + " / PIL CODE 수신 쪽지 / " + NO_MAIL + " / " + AFTER_MAIL_NAME);
		}
		#endregion ▣ MISC CODE 확인




		#region ▣ JIBE 메일 중복 확인
		private void JibeMailCheck()
		{
			if (MAIL_SUBJECT.Contains("RFQ No."))
			{
				int idx_s = -1;
				int idx_e = -1;

				idx_s = MAIL_SUBJECT.IndexOf("RFQ No.");
				idx_e = MAIL_SUBJECT.IndexOf("from");

				if (idx_s != -1 && idx_e != -1)
				{
					string jibeRFQno = MAIL_SUBJECT.Substring(idx_s, idx_e - idx_s).Trim();
					jibeRFQno = jibeRFQno.Replace("RFQ No.", "").Trim();

					string query = string.Empty;
					DataTable dt = null;

					query = "SELECT * FROM CZ_SA_QTN_PREREG WHERE CD_COMPANY = '" + CD_COMPANY + "' AND NO_REF = '" + jibeRFQno + "'";

					dt = DBMgr.GetDataTable(query);

					if (dt.Rows.Count > 0)
					{
						AFTER_MAIL_NAME = dt.Rows[0]["NO_FILE"].ToString() + "/JiBe등록";
					}
				}
			}
		}
		#endregion ▣ JIBE 메일 중복 확인

		#region ▣ 자동등록 및 파싱
		private void AutoInquiryInsert()
		{
			try
			{
				//Python python = new Python();

				InquiryParser parser = new InquiryParser(fileName);
				parser.Parse(true);

				string query2 = string.Empty;
				string imonumber = string.Empty;
				string partnercode = string.Empty;
				string referencenumber = string.Empty;

				string no_file = string.Empty;

				int no_line = 1;
				string nm_subject = string.Empty;
				string unit = string.Empty;
				string cd_item_partner = string.Empty;
				string nm_item_partner = string.Empty;
				string cd_uniq_partner = string.Empty;
				string qt = string.Empty;
				string cd_po_partner = string.Empty;
				string cd_stock = string.Empty;
				string cd_rate = string.Empty;
				string vesselName = string.Empty;
				string tnid = string.Empty;
				string buyer = string.Empty;

				string contact = string.Empty;
				string filenameStr = string.Empty;

				string shipservatt = string.Empty;


				DataTable dtItem = new DataTable();
				dtItem.Columns.Add("CD_COMPANY");
				dtItem.Columns.Add("NO_PREREG");
				dtItem.Columns.Add("NO_LINE");
				dtItem.Columns.Add("CD_PARTNER");
				dtItem.Columns.Add("NO_IMO");
				dtItem.Columns.Add("SHIPSERV_TNID");
				dtItem.Columns.Add("NM_VESSEL");
				dtItem.Columns.Add("NO_REF");
				dtItem.Columns.Add("NO_REQREF");
				dtItem.Columns.Add("NM_SUBJECT");
				dtItem.Columns.Add("CD_ITEM_PARTNER");
				dtItem.Columns.Add("NM_ITEM_PARTNER");
				dtItem.Columns.Add("CD_UNIQ_PARTNER");
				dtItem.Columns.Add("UNIT");
				dtItem.Columns.Add("QT");
				dtItem.Columns.Add("SORTED_BY");
				dtItem.Columns.Add("COMMENT");
				dtItem.Columns.Add("CONTACT");
				dtItem.Columns.Add("NM_FILE");

				dtItem.Columns.Add("DXCOMB1");
				dtItem.Columns.Add("DXCOMB2");
				dtItem.Columns.Add("DXDICT");
				dtItem.Columns.Add("DXDICT_PART");
				dtItem.Columns.Add("DXDICT_POS");

				if (parser.Item.Rows.Count > 0)
				{
					// 호선 가져오기
					string query = @"
SELECT
	  A.NO_IMO
	, A.NO_HULL
	, A.NM_VESSEL
	, B.CD_PARTNER
	, B.LN_PARTNER
	, B.CD_PARTNER_GRP
FROM	  CZ_MA_HULL	AS A WITH(NOLOCK)
LEFT JOIN MA_PARTNER	AS B WITH(NOLOCK) ON A.CD_PARTNER = B.CD_PARTNER AND B.CD_COMPANY = @CD_COMPANY
WHERE A.NO_IMO = @NO_IMO OR (@NO_IMO IS NULL AND A.NM_VESSEL LIKE '%' + @NM_VESSEL + '%')";

					DBMgr dbm = new DBMgr();
					dbm.DebugMode = DebugMode.Print;
					dbm.Query = query;
					dbm.AddParameter("@CD_COMPANY", CD_COMPANY);
					dbm.AddParameter("@NO_IMO", parser.ImoNumber);
					dbm.AddParameter("@NM_VESSEL", parser.Vessel);

					DataTable dt = dbm.GetDataTable();

					//PREREG 번호 가지고 오기
					query = "DECLARE @NO_FILE NVARCHAR(10) ";
					query = query + "SELECT @NO_FILE = 'PR'+RIGHT('00000000' + CONVERT(varchar,MAX(CONVERT(INT, RIGHT(NO_PREREG,8))+1)),8) FROM CZ_MA_MAIL_SORTING_LOG ";
					query = query + "SELECT @NO_FILE AS NO_FILE";
					DataTable dtFileNo = DBMgr.GetDataTable(query);

					if (dtFileNo != null)
						no_file = dtFileNo.Rows[0]["NO_FILE"].ToString();

					NO_PREREG = no_file;

					imonumber = parser.ImoNumber;

					if (dt.Rows.Count == 1)
					{
						if (dt.Rows[0]["CD_PARTNER"].ToString() != "")
							partnercode = dt.Rows[0]["CD_PARTNER"].ToString();

						if (string.IsNullOrEmpty(imonumber))
							imonumber = dt.Rows[0]["NO_IMO"].ToString();
					}

					if (string.IsNullOrEmpty(partnercode))
						partnercode = CD_PARTNER;


					if (string.IsNullOrEmpty(imonumber))
						imonumber = NO_IMO;


					// 문의번호
					referencenumber = parser.Reference;

					if (referencenumber.Equals(DateTime.Now.ToString("yyyy.MM.dd")))
						referencenumber = referencenumber + "." + partnercode + DateTime.Now.ToString("mmm");


					tnid = parser.Tnid;
					vesselName = parser.Vessel;
					// BUYER COMMENT 따로 저장
					buyer = parser.Buyer;
					contact = parser.Contact;
					filenameStr = parser.InqFileName;



					//dtItem.Columns.Add("DXCODE_SUBJ");
					//dtItem.Columns.Add("DXCODE_ITEM");


					foreach (DataRow row in parser.Item.Rows)
					{
						nm_subject = row["SUBJ"].ToString().ToUpper();
						unit = row["UNIT"].ToString().ToUpper();
						cd_item_partner = row["ITEM"].ToString().ToUpper();
						nm_item_partner = row["DESC"].ToString().ToUpper();
						cd_uniq_partner = row["UNIQ"].ToString().ToUpper();
						qt = row["QT"].ToString();

						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_COMPANY"] = CD_COMPANY;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_PREREG"] = no_file;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_LINE"] = no_line;
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_PARTNER"] = partnercode;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_IMO"] = imonumber;
						dtItem.Rows[dtItem.Rows.Count - 1]["SHIPSERV_TNID"] = tnid;
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_VESSEL"] = vesselName;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_REF"] = referencenumber;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_REQREF"] = "";
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_SUBJECT"] = nm_subject;
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_ITEM_PARTNER"] = cd_item_partner;
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_ITEM_PARTNER"] = nm_item_partner;
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_UNIQ_PARTNER"] = cd_uniq_partner;
						if (unit.Contains(",")) unit = "";
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = unit;
						if (string.IsNullOrEmpty(qt)) qt = "0";
						if (qt.Contains(",")) qt = qt.Replace(",", "").Trim();
						dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = qt;
						dtItem.Rows[dtItem.Rows.Count - 1]["SORTED_BY"] = NO_MAIL;
						dtItem.Rows[dtItem.Rows.Count - 1]["COMMENT"] = buyer;
						dtItem.Rows[dtItem.Rows.Count - 1]["CONTACT"] = contact;
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_FILE"] = filenameStr;

						dtItem.Rows[dtItem.Rows.Count - 1]["DXCOMB1"] = UT.GetDxDesc(cd_item_partner + " " + nm_item_partner);
						dtItem.Rows[dtItem.Rows.Count - 1]["DXCOMB2"] = UT.GetDxDesc(nm_subject + " ‡ " + cd_item_partner + " " + nm_item_partner);
						dtItem.Rows[dtItem.Rows.Count - 1]["DXDICT"] = UT.GetDxDictByTrain(nm_subject + " ‡ " + cd_item_partner + " ‡ " + nm_item_partner, "");
						dtItem.Rows[dtItem.Rows.Count - 1]["DXDICT_PART"] = UT.GetDxDictByTrain(nm_subject + " ‡ " + cd_item_partner + " ‡ " + nm_item_partner, "PART");
						dtItem.Rows[dtItem.Rows.Count - 1]["DXDICT_POS"] = UT.GetDxDictByTrain(nm_subject + " ‡ " + cd_item_partner + " ‡ " + nm_item_partner, "POS");

						//dtItem.Rows[dtItem.Rows.Count - 1]["DXCODE_SUBJ"] = Util.GetDxCode(nm_subject.ToUpper());
						//dtItem.Rows[dtItem.Rows.Count - 1]["DXCODE_ITEM"] = Util.GetDxCode(cd_item_partner.ToUpper()) + "‡" + nm_item_partner.ToUpper();

						no_line += 1;
					}

					string xml = Util.GetTO_Xml(dtItem);


					// 중복제거 한달로 설정 => 202004
					DateTime datetime = new DateTime();
					datetime = Convert.ToDateTime(DateTime.Now.ToLongTimeString());
					datetime = datetime.AddMonths(-1);

					string dateStr = datetime.ToString("yyyyMMddHHmmss");

					if (!string.IsNullOrEmpty(imonumber))
					{
						query = "SELECT NO_PREREG FROM CZ_SA_QTN_PREREG WHERE 1=1  ";
						query = query + " AND CD_COMPANY = '" + CD_COMPANY + "'";
						query = query + " AND NO_LINE = '1'";
						query = query + " AND NO_REF = '" + referencenumber + "'";
						query = query + " AND DTS_INSERT > '" + dateStr + "'";
						query = query + " AND NO_IMO = '" + imonumber + "'";
					}
					else if (!string.IsNullOrEmpty(partnercode))
					{
						query = "SELECT NO_PREREG FROM CZ_SA_QTN_PREREG WHERE 1=1  ";
						query = query + " AND CD_COMPANY = '" + CD_COMPANY + "'";
						query = query + " AND NO_LINE = '1'";
						query = query + " AND NO_REF = '" + referencenumber + "'";
						query = query + " AND DTS_INSERT > '" + dateStr + "'";
						query = query + " AND CD_PARTNER = '" + partnercode + "'";
					}
					else
					{
						query = "SELECT NO_PREREG FROM CZ_SA_QTN_PREREG WHERE 1=1  ";
						query = query + " AND CD_COMPANY = '" + CD_COMPANY + "'";
						query = query + " AND NO_LINE = '1'";
						query = query + " AND NO_REF = '" + referencenumber + "'";
						query = query + " AND DTS_INSERT > '" + dateStr + "'";
					}



					if (!string.IsNullOrEmpty(imonumber))
					{
						query2 = "SELECT NO_FILE FROM CZ_SA_QTNH WHERE 1=1  ";
						query2 = query2 + " AND CD_COMPANY = '" + CD_COMPANY + "'";
						query2 = query2 + " AND NO_REF = '" + referencenumber + "'";
						query2 = query2 + " AND DTS_INSERT > '" + dateStr + "'";
						query2 = query2 + " AND NO_IMO = '" + imonumber + "'";
					}
					else if (!string.IsNullOrEmpty(partnercode))
					{
						query2 = "SELECT NO_FILE FROM CZ_SA_QTNH WHERE 1=1  ";
						query2 = query2 + " AND CD_COMPANY = '" + CD_COMPANY + "'";
						query2 = query2 + " AND NO_REF = '" + referencenumber + "'";
						query2 = query2 + " AND DTS_INSERT > '" + dateStr + "'";
						query2 = query2 + " AND CD_PARTNER = '" + partnercode + "'";
					}
					else
					{
						query2 = "SELECT NO_FILE FROM CZ_SA_QTNH WHERE 1=1  ";
						query2 = query2 + " AND CD_COMPANY = '" + CD_COMPANY + "'";
						query2 = query2 + " AND NO_REF = '" + referencenumber + "'";
						query2 = query2 + " AND DTS_INSERT > '" + dateStr + "'";
					}


					string queryIMO_SCF = string.Empty;

					// 20221031 SCF관련 호선 제외
					if (!string.IsNullOrEmpty(imonumber))
					{
						queryIMO_SCF = "SELECT * FROM CZ_MA_CODEDTL WHERE 1=1 AND CD_COMPANY = 'K100' AND CD_FIELD = 'CZ_SA00058' AND YN_USE = 'Y'";
						queryIMO_SCF = queryIMO_SCF + "AND CD_SYSDEF = '" + imonumber + "'";
					}

					DataTable scfCheck = DBMgr.GetDataTable(queryIMO_SCF);

					DataTable insertResultDt = DBMgr.GetDataTable(query);
					DataTable insertResultQtnh = DBMgr.GetDataTable(query2);


					// 중복파일 검사하여 없을경우
					if (insertResultDt.Rows.Count == 0 && insertResultQtnh.Rows.Count == 0 && !string.IsNullOrEmpty(referencenumber) && scfCheck.Rows.Count == 0)
					{
						DBMgr.ExecuteNonQuery("SP_CZ_SA_QTN_PREREG", new object[] { xml });



						// 딥러닝 제외 20201125
						//DataTable partnerCompany = python.FindSupplier(CD_COMPANY, no_file);    // 매입처
						//DataTable partnerStock = python.FindStock(CD_COMPANY, no_file);		// 재고


						//if (partnerCompany != null && partnerCompany.Rows.Count > 0)
						//{
						//	//NO_LINE // CD_SUPPLIER // RATE
						//	for (int c = 0; c < partnerCompany.Rows.Count; c++)
						//	{
						//		no_line = Convert.ToInt32(partnerCompany.Rows[c]["NO_LINE"].ToString());
						//		cd_po_partner = partnerCompany.Rows[c]["CD_SUPPLIER"].ToString();
						//		cd_rate = partnerCompany.Rows[c]["RATE"].ToString();

						//		// 값 있을 경우 업데이트
						//		DBMgr.ExecuteNonQuery("PS_CZ_SA_QTN_PREREG_UPDATE", new object[] { CD_COMPANY, no_file, no_line, cd_po_partner, cd_rate });
						//	}
						//}


						bool PreregTnF = false;
						//  자동인콰리등록, 메일관리자 사용
						if (YN_INQINSERT.Equals("Y"))
						{
							//Preregister p = new Preregister();
							DXr p = new DXr();


							// 보낸메일 등록
							p.FromAddress = MAIL_FROM_DB_S;

							PreregTnF = p.Inquiry(CD_COMPANY, no_file, fileName);

							// T일경우 자동등록 됨
							if (PreregTnF)
							{
								AFTER_MAIL_NAME = string.Format("[{0}]", p.FileNumber + p.Tag);

								if (p.Message != null)
									msgStr = p.Message.ToString();

								if (string.IsNullOrEmpty(msgStr))
									msgStr = "등록/" + p.FileNumber + p.Tag;

								//if (!string.IsNullOrEmpty(parser.ShipServAtt) && !p.FileNumber.StartsWith("DS"))
								if (!string.IsNullOrEmpty(parser.ShipServAtt))
								{
									if (CD_COMPANY.Equals("K100"))
										InsertQue(CD_COMPANY, p.FileNumber, partnercode, "SHIPSERV_INQ", "1-8");
									else if (CD_COMPANY.Equals("K200"))
										InsertQue(CD_COMPANY, p.FileNumber, partnercode, "SHIPSERV_INQ_K200", "1-8");
									else if (CD_COMPANY.Equals("K100") && p.FileNumber.StartsWith("DS"))
										InsertQue(CD_COMPANY, p.FileNumber, partnercode, "SHIPSERV_INQ_S100", "1-8");
									else
										InsertQue(CD_COMPANY, p.FileNumber, partnercode, "SHIPSERV_INQ", "1-8");
								}
								else if (partnercode.Equals("03578") || partnercode.Equals("01220"))
								{
									InsertQue(CD_COMPANY, p.FileNumber, partnercode, "EURONAV_INQ", "1-4");
								}
								else if (partnercode.Equals("15122") || partnercode.Equals("11397") || partnercode.Equals("05645") || partnercode.Equals("11409") || partnercode.Equals("12637")
									|| partnercode.Equals("12954") || partnercode.Equals("09230") || partnercode.Equals("05229") || partnercode.Equals("12563") || partnercode.Equals("12869")
									|| partnercode.Equals("04925") || partnercode.Equals("03706"))
								{
									InsertQue(CD_COMPANY, p.FileNumber, partnercode, "PROCURE_INQ", "1-4");
								}
							}
							else
							{
								if (p.Message != null)
									errorMsg = errorMsg + p.Message.ToString();
							}
						}
						else if (YN_INQINSERT.Equals("N"))
						{
							msgStr = "자동등록 미사용";
						}


						imonumber = string.Empty;
						partnercode = string.Empty;

						LogWrite2("[파싱완료/" + PreregTnF + "]	" + mailitem.Subject + " / " + NO_EMAIL + " / " + NO_EMP + " / " + AFTER_MAIL_NAME + " / " + msgStr + "/" + errorMsg);
					}
					else if (string.IsNullOrEmpty(referencenumber))
					{
						if (insertResultDt.Rows.Count > 0)
							msgStr = "문의번호누락/" + insertResultDt.Rows[0]["NO_PREREG"].ToString();
						else
							msgStr = "문의번호누락/" + insertResultQtnh.Rows[0]["NO_FILE"].ToString();
						NO_PREREG = string.Empty;
						resultCode = "BACKUP";

						LogWrite2("[파싱실패] " + mailitem.Subject + " / " + NO_EMAIL + " / " + NO_EMP + " / " + AFTER_MAIL_NAME + " / " + msgStr);
					}
					else if (scfCheck.Rows.Count != 0)
					{
						if (insertResultDt.Rows.Count > 0)
							msgStr = "SCF 호선/" + insertResultDt.Rows[0]["NO_PREREG"].ToString();
						else
							msgStr = "SCF 호선/" + insertResultQtnh.Rows[0]["NO_FILE"].ToString();
						NO_PREREG = string.Empty;
						resultCode = "BACKUP";

						LogWrite2("[파싱실패] " + mailitem.Subject + " / " + NO_EMAIL + " / " + NO_EMP + " / " + AFTER_MAIL_NAME + " / " + msgStr);
					}
					else
					{
						if (insertResultDt.Rows.Count > 0)
							msgStr = "중복/" + insertResultDt.Rows[0]["NO_PREREG"].ToString();
						else
							msgStr = "중복/" + insertResultQtnh.Rows[0]["NO_FILE"].ToString();
						NO_PREREG = string.Empty;
						resultCode = "BACKUP";

						LogWrite2("[중복제거] " + mailitem.Subject + " / " + NO_EMAIL + " / " + NO_EMP + " / " + AFTER_MAIL_NAME + " / " + msgStr);
					}
				}
				else if (CODE.StartsWith("RPA_INQ"))
				{
					string query = string.Empty;
					//PREREG 번호 가지고 오기
					query = "DECLARE @NO_FILE NVARCHAR(10) ";
					query = query + "SELECT @NO_FILE = 'PR'+RIGHT('00000000' + CONVERT(varchar,MAX(CONVERT(INT, RIGHT(NO_PREREG,8))+1)),8) FROM CZ_MA_MAIL_SORTING_LOG ";
					query = query + "SELECT @NO_FILE AS NO_FILE";
					DataTable dtFileNo = DBMgr.GetDataTable(query);

					if (dtFileNo != null)
						no_file = dtFileNo.Rows[0]["NO_FILE"].ToString();

					NO_PREREG = no_file;

					dtItem.Rows.Add();
					dtItem.Rows[dtItem.Rows.Count - 1]["CD_COMPANY"] = CD_COMPANY;
					dtItem.Rows[dtItem.Rows.Count - 1]["NO_PREREG"] = NO_PREREG;
					dtItem.Rows[dtItem.Rows.Count - 1]["NO_LINE"] = "1";
					dtItem.Rows[dtItem.Rows.Count - 1]["CD_PARTNER"] = CD_PARTNER;

					if (CODE.Contains("PROCURE"))
					{
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_IMO"] = "9505833";
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_VESSEL"] = "STAR JANNI";
					}
					else if (CODE.Contains("TORM") || CODE.Contains("ULTRA") || CODE.Contains("KLINE"))
					{
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_IMO"] = "0000000";
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_VESSEL"] = "UNKNOWN";
					}

					dtItem.Rows[dtItem.Rows.Count - 1]["SHIPSERV_TNID"] = tnid;
					dtItem.Rows[dtItem.Rows.Count - 1]["NO_REF"] = referencenumber;
					dtItem.Rows[dtItem.Rows.Count - 1]["NO_REQREF"] = "";
					dtItem.Rows[dtItem.Rows.Count - 1]["NM_SUBJECT"] = nm_subject;
					dtItem.Rows[dtItem.Rows.Count - 1]["CD_ITEM_PARTNER"] = cd_item_partner;
					dtItem.Rows[dtItem.Rows.Count - 1]["NM_ITEM_PARTNER"] = nm_item_partner;
					dtItem.Rows[dtItem.Rows.Count - 1]["CD_UNIQ_PARTNER"] = cd_uniq_partner;
					if (unit.Contains(",")) unit = "";
					dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = unit;
					if (string.IsNullOrEmpty(qt)) qt = "0";
					if (qt.Contains(",")) qt = qt.Replace(",", "").Trim();
					dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = qt;
					dtItem.Rows[dtItem.Rows.Count - 1]["SORTED_BY"] = NO_MAIL;
					dtItem.Rows[dtItem.Rows.Count - 1]["COMMENT"] = buyer;
					dtItem.Rows[dtItem.Rows.Count - 1]["CONTACT"] = contact;
					dtItem.Rows[dtItem.Rows.Count - 1]["NM_FILE"] = filenameStr;

					dtItem.Rows[dtItem.Rows.Count - 1]["DXCOMB1"] = UT.GetDxDesc(cd_item_partner + " " + nm_item_partner);
					dtItem.Rows[dtItem.Rows.Count - 1]["DXCOMB2"] = UT.GetDxDesc(nm_subject + " ‡ " + cd_item_partner + " " + nm_item_partner);
					dtItem.Rows[dtItem.Rows.Count - 1]["DXDICT"] = UT.GetDxDictByTrain(nm_subject + " ‡ " + cd_item_partner + " ‡ " + nm_item_partner, "");
					dtItem.Rows[dtItem.Rows.Count - 1]["DXDICT_PART"] = UT.GetDxDictByTrain(nm_subject + " ‡ " + cd_item_partner + " ‡ " + nm_item_partner, "PART");
					dtItem.Rows[dtItem.Rows.Count - 1]["DXDICT_POS"] = UT.GetDxDictByTrain(nm_subject + " ‡ " + cd_item_partner + " ‡ " + nm_item_partner, "POS");


					string xml = Util.GetTO_Xml(dtItem);


					DBMgr.ExecuteNonQuery("SP_CZ_SA_QTN_PREREG", new object[] { xml });



					bool PreregTnF = false;
					//  자동인콰리등록, 메일관리자 사용
					if (YN_INQINSERT.Equals("Y"))
					{
						DXr p = new DXr();
						//Preregister p = new Preregister();

						// 보낸메일 등록
						p.FromAddress = MAIL_FROM_DB_S;

						PreregTnF = p.Inquiry(CD_COMPANY, no_file, fileName);

						// T일경우 자동등록 됨
						if (PreregTnF)
						{
							AFTER_MAIL_NAME = string.Format("[{0}]", p.FileNumber + p.Tag);

							if (p.Message != null)
								msgStr = p.Message.ToString();

							if (string.IsNullOrEmpty(msgStr))
								msgStr = "등록/" + p.FileNumber + p.Tag;


							// 등록
							if (CODE.Contains("ULTRA"))
								InsertQue(CD_COMPANY, p.FileNumber, CD_PARTNER, "ULTRASHIP_INQ", "1-11");
							else if (CODE.Contains("TORM"))
								InsertQue(CD_COMPANY, p.FileNumber, CD_PARTNER, "TORM_INQ", "1-11");
							else if (CODE.Contains("PROCURE"))
								InsertQue(CD_COMPANY, p.FileNumber, CD_PARTNER, "PROCURE_INQ", "1-11");
							else if (CODE.Contains("KLINE"))
								InsertQue(CD_COMPANY, p.FileNumber, CD_PARTNER, "TORM_INQ", "1-11");
						}
						else
						{
							if (p.Message != null)
								errorMsg = errorMsg + p.Message.ToString();
						}
					}
					else if (YN_INQINSERT.Equals("N"))
					{
						msgStr = "자동등록 미사용";
					}

					imonumber = string.Empty;
					partnercode = string.Empty;

					LogWrite(Color.DodgerBlue, "[RPA완료/" + PreregTnF + "]	" + mailitem.Subject + " / " + NO_EMAIL + " / " + NO_EMP + " / " + AFTER_MAIL_NAME + " / " + msgStr + "/" + errorMsg);
				}
			}
			catch (Exception e)
			{
				errorMsg = e.Message;

				if (!string.IsNullOrEmpty(RECEIVE_MAIL))
				{
					NO_EMAIL = RECEIVE_MAIL;

					if (NO_EMAIL.EndsWith(";"))
						NO_EMAIL += RECEIVE_MAIL2;
					else
						NO_EMAIL = NO_EMAIL + ";" + RECEIVE_MAIL2;
				}

				if (YN_MAIL.Equals("Y"))
				{
					InqCheck = true;
					SendMailServer();
				}
			}
		}

		private void InsertQue(string cdCompany, string noFile, string cdPartner, string cdRPA, string noBot)
		{
			string query = @"EXEC PX_CZ_RPA_WORK_QUEUE_4 @CD_COMPANY = @CD_COMPANY , @CD_RPA = @CD_RPA, @NO_FILE = @NO_FILE, @CD_PARTNER = @CD_PARTNER, @NO_BOTS = @NO_BOTS";

			DBMgr dbm = new DBMgr();
			dbm.DebugMode = DebugMode.Print;
			dbm.Query = query;
			dbm.AddParameter("@CD_COMPANY", cdCompany);
			dbm.AddParameter("@CD_RPA", cdRPA);
			dbm.AddParameter("@NO_FILE", noFile);
			dbm.AddParameter("@CD_PARTNER", cdPartner);
			dbm.AddParameter("@NO_BOTS", noBot);

			dbm.ExecuteNonQuery();
		}




		private void InsertBotQue(string cdCompany, string noFile, string cdRPA, int urgent, string noBot)
		{
			//InsertBotQue(CD_COMPANY, "INVOICE", "INV_ETAX_REG", 5, "6");
			string query = @"EXEC PX_CZ_RPA_WORK_QUEUE_4 @CD_COMPANY = @CD_COMPANY , @CD_RPA = @CD_RPA, @NO_FILE = @NO_FILE, @URGENT = @URGENT, @NO_BOTS = @NO_BOTS, @CD_PARTNER = NULL";

			DBMgr dbm = new DBMgr();
			dbm.DebugMode = DebugMode.Print;
			dbm.Query = query;
			dbm.AddParameter("@CD_COMPANY", cdCompany);
			dbm.AddParameter("@CD_RPA", cdRPA);
			dbm.AddParameter("@NO_FILE", noFile);
			dbm.AddParameter("@URGENT", urgent);
			dbm.AddParameter("@NO_BOTS", noBot);

			dbm.ExecuteNonQuery();
		}


		private void InsertMideastCode(string cdCompany, string cd, string insertDt)
		{
			string query = @"EXEC PS_CZ_MA_CODE_MIDEAST @CD_COMPANY = @CD_COMPANY, @CD_CODE = @CD_CODE, @DT_MAIL = @DT_MAIL";

			DBMgr dbm = new DBMgr();
			dbm.DebugMode = DebugMode.Print;
			dbm.Query = query;
			dbm.AddParameter("@CD_COMPANY", cdCompany);
			dbm.AddParameter("@CD_CODE", cd);
			dbm.AddParameter("@DT_MAIL", insertDt);

			dbm.ExecuteNonQuery();
		}


		private void InsertOrderUpload(string cdCompany, string nofile, string cd_partner, string noimo, string vessel, string noref, string nmexch, string cdexch, string amex, string dcrmk)
		{
			string query = @"EXEC PS_CZ_MA_ORDER_PARSINGH @CD_COMPANY=@CD_COMPANY, @NO_FILE = @NO_FILE, @CD_PARTNER=@CD_PARTNER, @NO_IMO=@NO_IMO, @NM_VESSEL=@NM_VESSEL, @NO_REF=@NO_REF, @NM_EXCH=@NM_EXCH, @CD_EXCH=@CD_EXCH, @AM_EX=@AM_EX, @DC_RMK=@DC_RMK";

			DBMgr dbm = new DBMgr();
			dbm.DebugMode = DebugMode.Print;
			dbm.Query = query;
			dbm.AddParameter("@CD_COMPANY", cdCompany);
			dbm.AddParameter("@NO_FILE", nofile);
			dbm.AddParameter("@CD_PARTNER", cd_partner);
			dbm.AddParameter("@NO_IMO", noimo);
			dbm.AddParameter("@NM_VESSEL", vessel);
			dbm.AddParameter("@NO_REF", noref);
			dbm.AddParameter("@NM_EXCH", nmexch);
			dbm.AddParameter("@CD_EXCH", cdexch);
			dbm.AddParameter("@AM_EX", amex);
			dbm.AddParameter("@DC_RMK", dcrmk);

			dbm.ExecuteNonQuery();
		}



		#endregion ▣ 자동등록 및 파싱

		#region ▣ JSON
		private string JsonWriteMove(string mid, string fid)
		{
			string resultValue = string.Empty;

			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.1.2:8000/MailService.asmx/Move");
			httpWebRequest.ContentType = "application/json; charset=utf-8";
			httpWebRequest.Method = "POST";

			HttpWebResponse responseData = null;

			using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				Dictionary<string, string> result = new Dictionary<string, string>();
				result.Add("msgId", mid);
				result.Add("folderId", fid);
				string json = JsonConvert.SerializeObject(result);

				streamWriter.Write(json);
				streamWriter.Flush();
				streamWriter.Close();
			}

			responseData = (HttpWebResponse)httpWebRequest.GetResponse();

			HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				var result = streamReader.ReadToEnd();
				resultValue = result;
			}

			JObject json1 = JObject.Parse(resultValue);

			string dValue = (string)json1["d"];

			JObject json2 = JObject.Parse(dValue);

			string testresult = (string)json2["result"];

			if (testresult.Equals("fail"))
				resultValue = (string)json2["error"];
			else if (testresult.Equals("success"))
				resultValue = testresult;

			return resultValue;
		}

		private string JsonWriteCopy(string mid, string fid, string subject)
		{
			string resultValue = string.Empty;

			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.1.2:8000/MailService.asmx/Copy");
			httpWebRequest.ContentType = "application/json; charset=utf-8";
			httpWebRequest.Method = "POST";

			HttpWebResponse responseData = null;

			using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				Dictionary<string, string> result = new Dictionary<string, string>();
				result.Add("msgId", mid);
				result.Add("folderId", fid);
				result.Add("subject", subject);
				string json = JsonConvert.SerializeObject(result);

				streamWriter.Write(json);
				streamWriter.Flush();
				streamWriter.Close();
			}

			responseData = (HttpWebResponse)httpWebRequest.GetResponse();

			HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				var result = streamReader.ReadToEnd();
				resultValue = result;
			}

			JObject json1 = JObject.Parse(resultValue);

			string dValue = (string)json1["d"];

			JObject json2 = JObject.Parse(dValue);

			string testresult = (string)json2["result"];

			if (testresult.Equals("fail"))
				resultValue = (string)json2["error"];
			else if (testresult.Equals("success"))
				resultValue = testresult;

			return resultValue;
		}
		#endregion ▣ JSON

		#region ▣ 로그저장
		//private void WriteText(string txtLog)
		//{
		//	string folder = Application.StartupPath + @"MailLog";

		//	DirectoryInfo dirinfo = new DirectoryInfo(folder);
		//	if (!dirinfo.Exists) dirinfo.Create();

		//	string txtFileName = folder + @"\Mail_Log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

		//	FileStream fileStream = new FileStream(txtFileName, FileMode.Append, FileAccess.Write);
		//	StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.Default);

		//	streamWriter.Write(String.Format("[{0}] ", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
		//	streamWriter.WriteLine(txtLog);
		//	streamWriter.Flush();
		//	streamWriter.Close();
		//	fileStream.Close();
		//}

		//private void WriteTextTCRS(string txtLog)
		//{
		//	string folder = Application.StartupPath + @"TCRSLog";

		//	DirectoryInfo dirinfo = new DirectoryInfo(folder);
		//	if (!dirinfo.Exists) dirinfo.Create();

		//	string txtFileName = folder + @"\TCRSLog" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

		//	FileStream fileStream = new FileStream(txtFileName, FileMode.Append, FileAccess.Write);
		//	StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.Default);

		//	streamWriter.Write(String.Format("[{0}] ", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
		//	streamWriter.WriteLine(txtLog);
		//	streamWriter.Flush();
		//	streamWriter.Close();
		//	fileStream.Close();
		//}

		//private void WriteTextTCRS_RE(string txtLog)
		//{
		//	string folder = Application.StartupPath + @"TCRSLog";

		//	DirectoryInfo dirinfo = new DirectoryInfo(folder);
		//	if (!dirinfo.Exists) dirinfo.Create();

		//	string txtFileName = folder + @"\TCRSLog_RE" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

		//	FileStream fileStream = new FileStream(txtFileName, FileMode.Append, FileAccess.Write);
		//	StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.Default);

		//	streamWriter.Write(String.Format("[{0}] ", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
		//	streamWriter.WriteLine(txtLog);
		//	streamWriter.Flush();
		//	streamWriter.Close();
		//	fileStream.Close();
		//}
		#endregion ▣ 로그저장


		#region ▣ 값 초기화
		// 값 초기화
		private void ClearData()
		{
			CD_COMPANY = string.Empty;
			CATEGORY = string.Empty;
			CODE = string.Empty;
			MAIL_FROM = string.Empty;
			NM_KOR = string.Empty;
			NO_EMP = string.Empty;
			CD_TEAM = string.Empty;
			NM_PARTER = string.Empty;
			CD_PARTNER = string.Empty;
			SUBJECT_KEY = string.Empty;
			SUBJECT_DEL = string.Empty;
			BODY_KEY = string.Empty;
			BODY_DEL = string.Empty;
			YN_USE = string.Empty;
			YN_CLOUDOC = string.Empty;
			YN_MAIL = string.Empty;
			YN_INQINSERT = string.Empty;
			RECEIVE_MAIL = string.Empty;
			RECEIVE_MAIL2 = string.Empty;
			ETC2 = string.Empty;
			DC_RMK = string.Empty;
			ID_INSERT = string.Empty;
			DTS_INSERT = string.Empty;
			ID_UPDATE = string.Empty;
			DTS_UPDATE = string.Empty;
			NO_MAIL = string.Empty;
			NO_EMAIL = string.Empty;
			DELIVERYDT = string.Empty;
			MAIL_SUBJECT = string.Empty;
			MAIL_BODY = string.Empty;

			NO_IMO = string.Empty;
			MAIL_TO = string.Empty;
			MAIL_CC = string.Empty;
			MAIL_BCC = string.Empty;

			NO_PREREG = string.Empty;

			AFTER_MAIL_NAME = string.Empty;

			FILE_NO = string.Empty;
			FILE_SUPPLY = string.Empty;

			MAIL_FROM_DB_S = string.Empty;
			MAIL_TO_DB_S = string.Empty;
			MAIL_TO_RESULT = string.Empty;

			mailKey = string.Empty;

			msgId = string.Empty;
			folderId = string.Empty;

			cd_send = string.Empty;
			errorMsg = string.Empty;
			msgStr = string.Empty;


			if (System.IO.Directory.Exists(@"C:\savemsg"))
			{
				string[] files = System.IO.Directory.GetFiles(@"C:\savemsg");
				foreach (string s in files)
				{
					string fileName = System.IO.Path.GetFileName(s);
					string deletefile = @"C:\savemsg\" + fileName;
					System.IO.File.Delete(deletefile);
				}
			}

			fileName = string.Empty;
			ingSig = true;

			mailitem = null;

			doubleCompany = string.Empty;

			InqCheck = false;
			UpCheck = false;

		}

		private void ClearDataStart()
		{
			CD_COMPANY = string.Empty;
			CATEGORY = string.Empty;
			CODE = string.Empty;
			MAIL_FROM = string.Empty;
			NM_KOR = string.Empty;
			NO_EMP = string.Empty;
			CD_TEAM = string.Empty;
			NM_PARTER = string.Empty;
			CD_PARTNER = string.Empty;
			SUBJECT_KEY = string.Empty;
			SUBJECT_DEL = string.Empty;
			BODY_KEY = string.Empty;
			BODY_DEL = string.Empty;
			YN_USE = string.Empty;
			YN_CLOUDOC = string.Empty;
			YN_MAIL = string.Empty;
			YN_INQINSERT = string.Empty;
			RECEIVE_MAIL = string.Empty;
			RECEIVE_MAIL2 = string.Empty;
			ETC2 = string.Empty;
			DC_RMK = string.Empty;
			ID_INSERT = string.Empty;
			DTS_INSERT = string.Empty;
			ID_UPDATE = string.Empty;
			DTS_UPDATE = string.Empty;
			NO_MAIL = string.Empty;
			NO_EMAIL = string.Empty;
			DELIVERYDT = string.Empty;
			MAIL_SUBJECT = string.Empty;
			MAIL_BODY = string.Empty;

			NO_IMO = string.Empty;
			MAIL_TO = string.Empty;
			MAIL_CC = string.Empty;
			MAIL_BCC = string.Empty;

			NO_PREREG = string.Empty;

			AFTER_MAIL_NAME = string.Empty;

			FILE_NO = string.Empty;
			FILE_SUPPLY = string.Empty;

			MAIL_FROM_DB_S = string.Empty;
			MAIL_TO_DB_S = string.Empty;
			MAIL_TO_RESULT = string.Empty;

			mailKey = string.Empty;

			msgId = string.Empty;
			folderId = string.Empty;

			cd_send = string.Empty;
			errorMsg = string.Empty;
			msgStr = string.Empty;

			fileName = string.Empty;

			mailitem = null;

			doubleCompany = string.Empty;

			InqCheck = false;
			UpCheck = false;

		}
		#endregion ▣ 값 초기화

		#region ▣ 값 저장
		// 값 저장
		private void SetData(DataTable ResultData, int rowCount)
		{
			DataRow row = ResultData.Rows[0];

			//NO_EMAIL = row["NO_EMAIL"].ToString();

			CD_COMPANY = ResultData.Rows[rowCount]["CD_COMPANY"].ToString();    //row["CD_COMPANY"].ToString();
			CATEGORY = ResultData.Rows[rowCount]["CATEGORY"].ToString();        //row["CATEGORY"].ToString();
			CODE = ResultData.Rows[rowCount]["CODE"].ToString();                //row["CODE"].ToString();
			MAIL_FROM = ResultData.Rows[rowCount]["MAIL_FROM1"].ToString();     //row["MAIL_FROM1"].ToString();
			NO_EMP = ResultData.Rows[rowCount]["NO_EMP"].ToString();            //row["NO_EMP"].ToString();
																				//NM_KOR = row["NM_KOR"].ToString();

			CD_TEAM = ResultData.Rows[rowCount]["CD_TEAM"].ToString();          //row["CD_TEAM"].ToString();
			CD_PARTNER = ResultData.Rows[rowCount]["CD_PARTNER"].ToString(); //row["CD_PARTNER"].ToString();
																			 //SUBJECT_KEY = row["SUBJECT_CONTAIN1"].ToString();
																			 //SUBJECT_DEL = row["SUBJECT_DELETE1"].ToString();
																			 //BODY_KEY = row["BODY_KEY"].ToString();
																			 //BODY_DEL = row["BODY_DEL"].ToString();
			YN_USE = ResultData.Rows[rowCount]["YN_USE"].ToString(); //row["YN_USE"].ToString();
			YN_CLOUDOC = ResultData.Rows[rowCount]["YN_CLOUDOC"].ToString();  //row["YN_CLOUDOC"].ToString();
			YN_MAIL = ResultData.Rows[rowCount]["YN_MAIL"].ToString();  //row["YN_MAIL"].ToString();
			YN_INQINSERT = ResultData.Rows[rowCount]["YN_INQINSERT"].ToString();  //row["YN_INQINSERT"].ToString();

			//RECEIVE_MAIL = ResultData.Rows[rowCount]["RECEIVE_MAIL"].ToString();  //row["RECEIVE_MAIL"].ToString();

			ETC2 = ResultData.Rows[rowCount]["ETC2"].ToString();  //row["ETC2"].ToString();
			DC_RMK = ResultData.Rows[rowCount]["DC_RMK"].ToString();  //row["DC_RMK"].ToString();
			ID_INSERT = ResultData.Rows[rowCount]["ID_INSERT"].ToString();  //row["ID_INSERT"].ToString();
			DTS_INSERT = ResultData.Rows[rowCount]["DTS_INSERT"].ToString();  //row["DTS_INSERT"].ToString();
			ID_UPDATE = ResultData.Rows[rowCount]["ID_UPDATE"].ToString();  //row["ID_UPDATE"].ToString();
			DTS_UPDATE = ResultData.Rows[rowCount]["DTS_UPDATE"].ToString();  //row["DTS_UPDATE"].ToString();
			NO_MAIL = ResultData.Rows[rowCount]["NO_MAIL"].ToString();  //row["NO_MAIL"].ToString();

			if (!string.IsNullOrEmpty(ResultData.Rows[rowCount]["RECEIVE_MAIL"].ToString()))
			{
				RECEIVE_MAIL = ResultData.Rows[rowCount]["RECEIVE_MAIL"].ToString();
			}

			string receiveMailStr = ResultData.Rows[rowCount]["RECEIVE_MAIL"].ToString().Trim();
			string noEmailStr = ResultData.Rows[rowCount]["NO_EMAIL"].ToString().Trim();
			string noEmpStr = ResultData.Rows[rowCount]["NM_KOR"].ToString().Trim();


			if (!receiveMailStr.Equals("MA00313"))
			{
				if (string.IsNullOrEmpty(receiveMailStr))
				{
					if (!RECEIVE_MAIL.Contains(noEmailStr))
						RECEIVE_MAIL += ";" + noEmailStr;
				}
				else
				{
					if (!RECEIVE_MAIL.Contains(receiveMailStr))
						RECEIVE_MAIL += ";" + receiveMailStr;
				}

				if (!NM_KOR.Contains(noEmpStr))
					NM_KOR += "/" + noEmpStr;
			}

			if (RECEIVE_MAIL.StartsWith(";"))
				RECEIVE_MAIL = RECEIVE_MAIL.Substring(1, RECEIVE_MAIL.Length - 1).Trim();

			if (NM_KOR.StartsWith("/"))
				NM_KOR = NM_KOR.Substring(1, NM_KOR.Length - 1).Trim();


			//if (ResultData.Rows.Count > 1)
			//{
			//	RECEIVE_MAIL = string.Empty;
			//	NM_KOR = string.Empty;

			//	for (int c = 0; c < ResultData.Rows.Count; c++)
			//	{
			//		string receiveMailStr = ResultData.Rows[c]["RECEIVE_MAIL"].ToString().Trim();
			//		string noEmailStr = ResultData.Rows[c]["NO_EMAIL"].ToString().Trim();
			//		string noEmpStr = ResultData.Rows[c]["NM_KOR"].ToString().Trim();
			//		//20200401 최수진 요청으로 중복 발신 제외함 / MA00313
			//		if (!receiveMailStr.Equals("MA00313"))
			//		{

			//			if (string.IsNullOrEmpty(receiveMailStr))
			//			{
			//				if (!RECEIVE_MAIL.Contains(noEmailStr))
			//					RECEIVE_MAIL += ";" + noEmailStr;
			//			}
			//			else
			//			{
			//				if (!RECEIVE_MAIL.Contains(receiveMailStr))
			//					RECEIVE_MAIL += ";" + receiveMailStr;
			//			}

			//			if(!NM_KOR.Contains(noEmpStr))
			//				NM_KOR += "/" + noEmpStr;
			//		}
			//	}

			//	if (RECEIVE_MAIL.StartsWith(";"))
			//		RECEIVE_MAIL = RECEIVE_MAIL.Substring(1, RECEIVE_MAIL.Length - 1).Trim();

			//	if (NM_KOR.StartsWith("/"))
			//		NM_KOR = NM_KOR.Substring(1, NM_KOR.Length - 1).Trim();
			//}
			//else if (ResultData.Rows.Count == 1)
			//{
			//	if (string.IsNullOrEmpty(row["RECEIVE_MAIL"].ToString()))
			//		RECEIVE_MAIL = row["NO_EMAIL"].ToString();
			//	else
			//		RECEIVE_MAIL = row["RECEIVE_MAIL"].ToString();

			//	NM_KOR = row["NM_KOR"].ToString();
			//}
		}
		#endregion ▣ 값 저장

		#region ▣ 버튼 
		private void Btn로그저장_Click(object sender, EventArgs e)
		{
			txtLog.SelectionColor = Color.Black;
			log.WriteText(txtLog.Text);
			//WriteText(txtLog.Text);

			txtLog.Text = string.Empty;

			LogWrite2("로그저장");
		}

		private void BtnClear_Click(object sender, EventArgs e)
		{
			txtLog.Text = "";
		}

		private void Btn중지_Click(object sender, System.EventArgs e)
		{
			startSig = false;
			MailItems.ItemAdd -= new Outlook.ItemsEvents_ItemAddEventHandler(items_ItemAdd);
			timer1.Stop();
			ClearData();

			LogWrite(Color.Black, "중지");
		}

		private void Btn시작_Click(object sender, System.EventArgs e)
		{
			app = new Outlook.Application();
			ns = app.GetNamespace("MAPI");
			ns.Logon(null, null, false, false);

			inboxFolder = ns.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);

			moveFolder = inboxFolder.Folders[tbx이동폴더.Text];      // 이동편지함
			uFolder = inboxFolder.Folders[tbx받은폴더.Text];         // 받은편지함
			unClass = inboxFolder.Folders[tbx미분류.Text];         // 미분류
			uploadFolder = inboxFolder.Folders[tbxUPload.Text];     // 업로드 폴더
			backupFolder = inboxFolder.Folders[tbx보관.Text];     // 보관폴더

			dumoveFolder = inboxFolder.Folders[tbx이동폴더.Text + "(두)"];      // 이동편지함
			duunClass = inboxFolder.Folders[tbx미분류.Text + "(두)"];         // 미분류
			duuploadFolder = inboxFolder.Folders[tbxUPload.Text + "(두)"];     // 업로드 폴더
			dubackupFolder = inboxFolder.Folders[tbx보관.Text + "(두)"];     // 보관폴더



			invoiceFolder = inboxFolder.Folders["인보이스"];            // 보관폴더
			invoiceFolderdu = inboxFolder.Folders["인보이스(두)"];   // 보관폴더


			receiptFolder = inboxFolder.Folders["인수증"];         // 보관폴더
			receiptFolderdu = inboxFolder.Folders["인수증(두)"];     // 보관폴더


			MDSDFolder = inboxFolder.Folders["MDSDOC"];         // MDSDOC
			MDSDFolderdu = inboxFolder.Folders["MDSDOC(두)"];     // MDSDOC


			FWFolder = inboxFolder.Folders["FORWARDER"];         // FORWARDER
			FWFolderdu = inboxFolder.Folders["FORWARDER(두)"];     // FORWARDER

			SKFolder = inboxFolder.Folders["SK로그인코드"];     // SK로그인코드

			MailItems = uFolder.Items;
			MailItems.ItemAdd += new Outlook.ItemsEvents_ItemAddEventHandler(items_ItemAdd);


			LogWrite(Color.Black, "시작");

			timer1.Start();

			ingSig = true;
			startSig = true;
		}

		private void DocTest_Click(object sender, EventArgs e)
		{
			// TEST FB21031765 ,  아이템 92개 SB21018283 / 
			//string test = Parsing.EXCEL.SDOC.SDOC_EXCEL("FB21099609");


			string test = Parsing.EXCEL.SDOC.SDOC_EXCEL(tbxFileNo.Text);

			ShowMessage("작업을 완료하였습니다.");


			// TEST 수출면장
			//Parsing.Parser.UNIPASSParser parser = new Parsing.Parser.UNIPASSParser();

			//parser.Parse();

			//DataTable dt = parser.Item;
		}



		#endregion ▣ 버튼

		#region ▣ 타이머 관련

		void items_ItemAdd(object Item)
		{
			if (ingSig)
			{
				startSig = true;
				//BeginInvoke(new TimerMailEvent(MailReg));
				timer1.Start();
			}
		}

		#endregion ▣ 반복 타이머 관련

		#region ▣ DB

		// DB 검색
		private void GetDBJoin(string mailSubject, string mailHost, string mailBody, string mailTo, string mailCC, string mailBCC)
		{
			doubleCompany = string.Empty;

			DataTable dt;

			if (mailTo.ToLower().Contains("@dintec.co.kr") && mailTo.ToLower().Contains("@dubheco.com"))
			{
				CD_COMPANY = "K100";
				DataTable teamDt = DBTeamMailSelectDT("service@dubheco.com");
				if (teamDt.Rows.Count > 0)
				{
					doubleCompany = teamDt.Rows[0]["CD_FLAG2"].ToString();
				}
			}
			else if (mailTo.ToLower().Contains("@dintec.co.kr") && !MAIL_SUBJECT.Contains("DUBHECO - INQU"))
				CD_COMPANY = "K100";
			else if (mailTo.ToLower().Contains("@dubheco.com"))
				CD_COMPANY = "K200";
			else
				CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;

			dt = SQL.GetDataTable("P_CZ_MAIL_GROUP_JOIN", CD_COMPANY, mailSubject, mailHost, mailBody, mailTo);

			dbDatatbResult = dt;
		}

		private void DBInsert(string _sendTo, string _msgId, string _folderId, string _sendKind, string _resultValue, string _error, string _msg)
		{
			if (_sendTo.StartsWith(";"))
				_sendTo = _sendTo.Substring(1, _sendTo.Length - 1).Trim();

			if (string.IsNullOrEmpty(_msgId))
				_msgId = string.Empty;

			if (string.IsNullOrEmpty(_folderId))
				_folderId = string.Empty;

			if (string.IsNullOrEmpty(NO_MAIL))
				NO_MAIL = string.Empty;

			if (string.IsNullOrEmpty(_sendKind))
				_sendKind = string.Empty;


			SqlConnection sqlConn;

			sqlConn = new SqlConnection("server = 192.168.1.143; uid = NEOE; pwd = NEOE; database = NEOE");
			sqlConn.Open();

			SqlCommand sqlComm = new SqlCommand();
			sqlComm.Connection = sqlConn;
			sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

			SqlDataReader reader = null;


			sqlComm.CommandText = "PS_CZ_MA_MAIL_SORTING_LOG";

			sqlComm.Parameters.Add("P_CD_COMPANY", SqlDbType.NVarChar, 7);
			sqlComm.Parameters.Add("P_DTS_INSERT", SqlDbType.NVarChar, 14);
			sqlComm.Parameters.Add("P_MAIL_FROM", SqlDbType.NVarChar, 128);
			sqlComm.Parameters.Add("P_MAIL_TO", SqlDbType.NVarChar, 128);
			sqlComm.Parameters.Add("P_MAIL_CC", SqlDbType.NVarChar, 128);
			sqlComm.Parameters.Add("P_DC_SUBJECT", SqlDbType.NVarChar, 100);
			sqlComm.Parameters.Add("P_DC_CONTENT", SqlDbType.NVarChar, 100);
			sqlComm.Parameters.Add("P_SORTED_BY", SqlDbType.NVarChar, 10);
			sqlComm.Parameters.Add("P_MSG_ID", SqlDbType.NVarChar, 200);
			sqlComm.Parameters.Add("P_FOLDER_ID", SqlDbType.NVarChar, 200);
			sqlComm.Parameters.Add("P_CD_SEND", SqlDbType.NVarChar, 10);
			sqlComm.Parameters.Add("P_MAIL_SEND", SqlDbType.NVarChar, 128);
			sqlComm.Parameters.Add("P_YN_RESULT", SqlDbType.NVarChar, 1);
			sqlComm.Parameters.Add("P_DC_MESSAGE", SqlDbType.NVarChar, 100);
			sqlComm.Parameters.Add("P_DC_ERROR", SqlDbType.NVarChar, 500);
			sqlComm.Parameters.Add("P_NO_PREREG", SqlDbType.NVarChar, 20);


			sqlComm.Parameters["P_CD_COMPANY"].Value = CD_COMPANY;
			sqlComm.Parameters["P_DTS_INSERT"].Value = DateTime.Now.ToString("yyyyMMddHHmmss");
			sqlComm.Parameters["P_MAIL_FROM"].Value = MAIL_FROM_DB_S;
			sqlComm.Parameters["P_MAIL_TO"].Value = MAIL_TO;
			sqlComm.Parameters["P_MAIL_CC"].Value = MAIL_CC;
			if (MAIL_SUBJECT.Length > 100)
				sqlComm.Parameters["P_DC_SUBJECT"].Value = MAIL_SUBJECT.Substring(0, 99);
			else
				sqlComm.Parameters["P_DC_SUBJECT"].Value = MAIL_SUBJECT;
			if (MAIL_BODY.Length > 100)
				sqlComm.Parameters["P_DC_CONTENT"].Value = MAIL_BODY.Substring(0, 99);
			else
				sqlComm.Parameters["P_DC_CONTENT"].Value = MAIL_BODY;
			sqlComm.Parameters["P_SORTED_BY"].Value = NO_MAIL;
			sqlComm.Parameters["P_MSG_ID"].Value = _msgId.Replace("<", "").Replace(">", "");
			sqlComm.Parameters["P_FOLDER_ID"].Value = _folderId.Replace("<", "").Replace(">", "");
			sqlComm.Parameters["P_CD_SEND"].Value = _sendKind;
			sqlComm.Parameters["P_MAIL_SEND"].Value = _sendTo;
			sqlComm.Parameters["P_YN_RESULT"].Value = _resultValue;
			if (_msg.Length > 100)
				sqlComm.Parameters["P_DC_MESSAGE"].Value = _msg.Substring(0, 99);
			else
				sqlComm.Parameters["P_DC_MESSAGE"].Value = _msg;
			if (_error.Length > 100)
				sqlComm.Parameters["P_DC_ERROR"].Value = _error.Substring(0, 99);
			else
				sqlComm.Parameters["P_DC_ERROR"].Value = _error;
			sqlComm.Parameters["P_NO_PREREG"].Value = NO_PREREG;

			reader = sqlComm.ExecuteReader();
			reader.Close();
			sqlConn.Close();
		}

		#endregion ▣ DB

		#region ▣ 메일 저장
		// 로컬 저장
		private string FileLocalSave(Outlook.MailItem mailitem)
		{
			string fileNameStr = string.Empty;
			if (mailitem.Subject != null)
				fileNameStr = DateTime.Now.ToString("yyyyMMddHHmmssfff");
			/*mailitem.Subject.Replace("\\", "");*/
			else
				fileNameStr = "subject_null";

			//if (fileNameStr.Length > 249)
			//	fileNameStr = fileNameStr.Substring(0, 248).Trim();

			fileNameStr = fileNameStr.Replace("\\", "").Replace("\t", "").Replace("/", "").Replace(@"\u", "").Trim();

			string _fileStr = @"\savemsg\" + fileNameStr;

			string _fileStxpath = "C:\\savemsg";
			_fileStr = "C:" + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim();

			if (Directory.Exists(_fileStxpath) == false)
				Directory.CreateDirectory(_fileStxpath);


			System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr + ".msg");
			if (!fi.Exists)
			{
				mailitem.SaveAs(_fileStr + ".msg", Outlook.OlSaveAsType.olMSG);
			}

			return _fileStr;
		}


		private string mailSaveAs(Outlook.MailItem mailitem)
		{
			string fileNameStr = string.Empty;
			if (mailitem.Subject != null)
				fileNameStr = mailitem.Subject.ToString().Trim() + "_" + mailitem.SenderEmailAddress.ToLower().ToString().Trim();
			else
				fileNameStr = mailitem.SenderEmailAddress.ToLower().ToString().Trim();

			if (fileNameStr.Length > 249)
				fileNameStr = fileNameStr.Substring(0, 248).Trim();

			fileNameStr = fileNameStr.Replace("\\", "").Replace("\t", "").Replace("/", "").Replace(@"\u", "").Trim();

			string _fileStr = @"\INVOICE\" + fileNameStr;

			string _fileStxpath = "C:\\INVOICE";
			_fileStr = "C:" + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim();

			if (Directory.Exists(_fileStxpath) == false)
				Directory.CreateDirectory(_fileStxpath);


			System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr + ".msg");
			if (!fi.Exists)
			{
				mailitem.SaveAs(_fileStr + ".msg", Outlook.OlSaveAsType.olMSG);
			}

			return _fileStr;
		}


		// 8번 BOT 저장
		private void FileServerSave(Outlook.MailItem mailitem)
		{
			string serverIp = @"\\192.168.0.246";

			try
			{
				int resultNum = ConnectRemoteServer(serverIp);

				if (resultNum == 0)
				{

					string fileNameStr = string.Empty;
					if (mailitem.Subject != null)
						fileNameStr = mailitem.Subject.ToString().Trim() + "_" + mailitem.SenderEmailAddress.ToLower().ToString().Trim();
					else
						fileNameStr = mailitem.SenderEmailAddress.ToLower().ToString().Trim();



					if (fileNameStr.Length > 240)
						fileNameStr = fileNameStr.Substring(0, 240).Trim();

					fileNameStr = fileNameStr + "_" + DateTime.Now.ToString("hhmmss_ff");

					fileNameStr = fileNameStr.Replace("\\", "").Replace("\t", "").Replace("/", "").Replace(@"\u", "").Trim();


					string _fileStr = @"" + fileNameStr;
					//string _fileStxpath = @"\\192.168.0.246\INVOICE\";

					//					string _fileStxpathDate = @"\\192.168.0.246\INVOICE\K100\" + DateTime.Now.ToString("yyyyMMdd")+@"\";
					string _fileStxpathDate = @"\\192.168.0.246\INVOICE\K100\";


					//_fileStr = _fileStxpath + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";
					_fileStr = _fileStxpathDate + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";


					_fileStr = Regex.Replace(_fileStr, @"[\/?:*""><|]+", "", RegexOptions.Compiled);


					// 폴더 생성
					if (Directory.Exists(_fileStxpathDate) == false)
						Directory.CreateDirectory(_fileStxpathDate);

					System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr);
					if (!fi.Exists)
					{
						mailitem.SaveAs(_fileStr, Outlook.OlSaveAsType.olMSG);
					}

					string fileName = _fileStr;
				}

				CencelRemoteServer(serverIp);


				// QUEUE INSERT
				InsertBotQue(CD_COMPANY, "INVOICE", "INV_ETAX_REG", 5, "8");
			}
			catch (Exception e)
			{
				CencelRemoteServer(serverIp);
			}
		}


		private void FileServerSave_K200(Outlook.MailItem mailitem)
		{
			string serverIp = @"\\192.168.0.246";

			try
			{
				int resultNum = ConnectRemoteServer(serverIp);

				if (resultNum == 0)
				{

					string fileNameStr = string.Empty;
					if (mailitem.Subject != null)
						fileNameStr = mailitem.Subject.ToString().Trim() + "_" + mailitem.SenderEmailAddress.ToLower().ToString().Trim();
					else
						fileNameStr = mailitem.SenderEmailAddress.ToLower().ToString().Trim();



					if (fileNameStr.Length > 240)
						fileNameStr = fileNameStr.Substring(0, 240).Trim();

					fileNameStr = fileNameStr + "_" + DateTime.Now.ToString("hhmmss_ff");

					fileNameStr = fileNameStr.Replace("\\", "").Replace("\t", "").Replace("/", "").Replace(@"\u", "").Trim();


					string _fileStr = @"" + fileNameStr;
					//string _fileStxpath = @"\\192.168.0.246\INVOICE\";

					string _fileStxpathDate = @"\\192.168.0.246\INVOICE\K200\";
					//				string _fileStxpathDate = @"\\192.168.0.246\INVOICE\K200\" + DateTime.Now.ToString("yyyyMMdd") + @"\";


					//_fileStr = _fileStxpath + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";
					_fileStr = _fileStxpathDate + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";


					_fileStr = Regex.Replace(_fileStr, @"[\/?:*""><|]+", "", RegexOptions.Compiled);


					// 폴더 생성
					if (Directory.Exists(_fileStxpathDate) == false)
						Directory.CreateDirectory(_fileStxpathDate);

					System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr);
					if (!fi.Exists)
					{
						mailitem.SaveAs(_fileStr, Outlook.OlSaveAsType.olMSG);
					}

					string fileName = _fileStr;
				}

				CencelRemoteServer(serverIp);


				// QUEUE INSERT
				InsertBotQue(CD_COMPANY, "INVOICE", "INV_ETAX_REG", 5, "8");
			}
			catch (Exception e)
			{
				CencelRemoteServer(serverIp);
			}
		}


		private void FileServerSaveReceipt_K100(Outlook.MailItem mailitem)
		{
			string serverIp = @"\\192.168.0.246";

			try
			{
				int resultNum = ConnectRemoteServer(serverIp);

				if (resultNum == 0)
				{

					string fileNameStr = string.Empty;
					if (mailitem.Subject != null)
						fileNameStr = mailitem.Subject.ToString().Trim() + "_" + mailitem.SenderEmailAddress.ToLower().ToString().Trim();
					else
						fileNameStr = mailitem.SenderEmailAddress.ToLower().ToString().Trim();



					if (fileNameStr.Length > 240)
						fileNameStr = fileNameStr.Substring(0, 240).Trim();

					fileNameStr = fileNameStr + "_" + DateTime.Now.ToString("hhmmss_ff");

					fileNameStr = fileNameStr.Replace("\\", "").Replace("\t", "").Replace("/", "").Replace(@"\u", "").Trim();


					string _fileStr = @"" + fileNameStr;
					//string _fileStxpath = @"\\192.168.0.246\INVOICE\";

					//					string _fileStxpathDate = @"\\192.168.0.246\INVOICE\K100\" + DateTime.Now.ToString("yyyyMMdd")+@"\";
					string _fileStxpathDate = @"\\192.168.0.246\RECEIPT\K100\";


					//_fileStr = _fileStxpath + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";
					_fileStr = _fileStxpathDate + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";


					_fileStr = Regex.Replace(_fileStr, @"[\/?:*""><|]+", "", RegexOptions.Compiled);


					// 폴더 생성
					if (Directory.Exists(_fileStxpathDate) == false)
						Directory.CreateDirectory(_fileStxpathDate);

					System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr);
					if (!fi.Exists)
					{
						mailitem.SaveAs(_fileStr, Outlook.OlSaveAsType.olMSG);
					}

					string fileName = _fileStr;
				}

				CencelRemoteServer(serverIp);


				// QUEUE INSERT
				InsertBotQue(CD_COMPANY, "RECEIPT", "RECEIPT_REG", 5, "8");
			}
			catch (Exception e)
			{
				CencelRemoteServer(serverIp);
			}
		}
		private void FileServerSaveReceipt_K200(Outlook.MailItem mailitem)
		{
			string serverIp = @"\\192.168.0.246";

			try
			{
				int resultNum = ConnectRemoteServer(serverIp);

				if (resultNum == 0)
				{

					string fileNameStr = string.Empty;
					if (mailitem.Subject != null)
						fileNameStr = mailitem.Subject.ToString().Trim() + "_" + mailitem.SenderEmailAddress.ToLower().ToString().Trim();
					else
						fileNameStr = mailitem.SenderEmailAddress.ToLower().ToString().Trim();



					if (fileNameStr.Length > 240)
						fileNameStr = fileNameStr.Substring(0, 240).Trim();

					fileNameStr = fileNameStr + "_" + DateTime.Now.ToString("hhmmss_ff");

					fileNameStr = fileNameStr.Replace("\\", "").Replace("\t", "").Replace("/", "").Replace(@"\u", "").Trim();


					string _fileStr = @"" + fileNameStr;
					//string _fileStxpath = @"\\192.168.0.246\INVOICE\";

					//					string _fileStxpathDate = @"\\192.168.0.246\INVOICE\K100\" + DateTime.Now.ToString("yyyyMMdd")+@"\";
					string _fileStxpathDate = @"\\192.168.0.246\RECEIPT\K100\";


					//_fileStr = _fileStxpath + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";
					_fileStr = _fileStxpathDate + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";


					_fileStr = Regex.Replace(_fileStr, @"[\/?:*""><|]+", "", RegexOptions.Compiled);


					// 폴더 생성
					if (Directory.Exists(_fileStxpathDate) == false)
						Directory.CreateDirectory(_fileStxpathDate);

					System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr);
					if (!fi.Exists)
					{
						mailitem.SaveAs(_fileStr, Outlook.OlSaveAsType.olMSG);
					}

					string fileName = _fileStr;

				}

				CencelRemoteServer(serverIp);


				// QUEUE INSERT
				InsertBotQue(CD_COMPANY, "RECEIPT", "RECEIPT_REG", 5, "8");
			}
			catch (Exception e)
			{
				CencelRemoteServer(serverIp);
			}
		}




		private void FileServerSaveMDSD_K100(Outlook.MailItem mailitem)
		{
			string serverIp = @"\\192.168.0.147";

			try
			{
				int resultNum = ConnectRemoteServer(serverIp);

				if (resultNum == 0)
				{

					string fileNameStr = string.Empty;
					if (mailitem.Subject != null)
						fileNameStr = mailitem.Subject.ToString().Trim() + "_" + mailitem.SenderEmailAddress.ToLower().ToString().Trim();
					else
						fileNameStr = mailitem.SenderEmailAddress.ToLower().ToString().Trim();



					if (fileNameStr.Length > 240)
						fileNameStr = fileNameStr.Substring(0, 240).Trim();

					fileNameStr = fileNameStr + "_" + DateTime.Now.ToString("hhmmss_ff");

					fileNameStr = fileNameStr.Replace("\\", "").Replace("\t", "").Replace("/", "").Replace(@"\u", "").Trim();


					string _fileStr = @"" + fileNameStr;

					string _fileStxpathDate = @"\\192.168.0.147\MDSDOC\K100\";


					_fileStr = _fileStxpathDate + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";


					_fileStr = Regex.Replace(_fileStr, @"[\/?:*""><|]+", "", RegexOptions.Compiled);


					// 폴더 생성
					if (Directory.Exists(_fileStxpathDate) == false)
						Directory.CreateDirectory(_fileStxpathDate);

					System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr);
					if (!fi.Exists)
					{
						mailitem.SaveAs(_fileStr, Outlook.OlSaveAsType.olMSG);
					}

					string fileName = _fileStr;
				}

				CencelRemoteServer(serverIp);
			}
			catch (Exception e)
			{
				CencelRemoteServer(serverIp);
			}
		}
		private void FileServerSaveMDSD_K200(Outlook.MailItem mailitem)
		{
			string serverIp = @"\\192.168.0.147";

			try
			{
				int resultNum = ConnectRemoteServer(serverIp);

				if (resultNum == 0)
				{

					string fileNameStr = string.Empty;
					if (mailitem.Subject != null)
						fileNameStr = mailitem.Subject.ToString().Trim() + "_" + mailitem.SenderEmailAddress.ToLower().ToString().Trim();
					else
						fileNameStr = mailitem.SenderEmailAddress.ToLower().ToString().Trim();



					if (fileNameStr.Length > 240)
						fileNameStr = fileNameStr.Substring(0, 240).Trim();

					fileNameStr = fileNameStr + "_" + DateTime.Now.ToString("hhmmss_ff");

					fileNameStr = fileNameStr.Replace("\\", "").Replace("\t", "").Replace("/", "").Replace(@"\u", "").Trim();


					string _fileStr = @"" + fileNameStr;

					string _fileStxpathDate = @"\\192.168.0.147\MDSDOC\K200\";


					_fileStr = _fileStxpathDate + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";


					_fileStr = Regex.Replace(_fileStr, @"[\/?:*""><|]+", "", RegexOptions.Compiled);


					// 폴더 생성
					if (Directory.Exists(_fileStxpathDate) == false)
						Directory.CreateDirectory(_fileStxpathDate);

					System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr);
					if (!fi.Exists)
					{
						mailitem.SaveAs(_fileStr, Outlook.OlSaveAsType.olMSG);
					}

					string fileName = _fileStr;
				}

				CencelRemoteServer(serverIp);
			}
			catch (Exception e)
			{
				CencelRemoteServer(serverIp);
			}
		}


		private void FileServerSaveFORWARDER_K100(Outlook.MailItem mailitem)
		{
			string serverIp = @"\\192.168.0.246";

			try
			{
				int resultNum = ConnectRemoteServer(serverIp);

				if (resultNum == 0)
				{

					string fileNameStr = string.Empty;
					if (mailitem.Subject != null)
						fileNameStr = mailitem.Subject.ToString().Trim() + "_" + mailitem.SenderEmailAddress.ToLower().ToString().Trim();
					else
						fileNameStr = mailitem.SenderEmailAddress.ToLower().ToString().Trim();



					if (fileNameStr.Length > 240)
						fileNameStr = fileNameStr.Substring(0, 240).Trim();

					fileNameStr = fileNameStr + "_" + DateTime.Now.ToString("hhmmss_ff");

					fileNameStr = fileNameStr.Replace("\\", "").Replace("\t", "").Replace("/", "").Replace(@"\u", "").Trim();


					string _fileStr = @"" + fileNameStr;
					//string _fileStxpath = @"\\192.168.0.246\INVOICE\";

					//					string _fileStxpathDate = @"\\192.168.0.246\INVOICE\K100\" + DateTime.Now.ToString("yyyyMMdd")+@"\";
					string _fileStxpathDate = @"\\192.168.0.246\FORWARDER\K100\";


					//_fileStr = _fileStxpath + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";
					_fileStr = _fileStxpathDate + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";


					_fileStr = Regex.Replace(_fileStr, @"[\/?:*""><|]+", "", RegexOptions.Compiled);


					// 폴더 생성
					if (Directory.Exists(_fileStxpathDate) == false)
						Directory.CreateDirectory(_fileStxpathDate);

					System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr);
					if (!fi.Exists)
					{
						mailitem.SaveAs(_fileStr, Outlook.OlSaveAsType.olMSG);
					}

					string fileName = _fileStr;


					// QUEUE INSERT
					InsertBotQue(CD_COMPANY, "FORWARDER", "FORWARDER_REG", 5, "8");
				}

				CencelRemoteServer(serverIp);

			}
			catch (Exception e)
			{
				CencelRemoteServer(serverIp);
			}
		}


		private void FileServerSaveSKLOGIN(Outlook.MailItem mailitem)
		{
			string serverIp = @"\\192.168.0.246";

			try
			{
				int resultNum = ConnectRemoteServer(serverIp);

				if (resultNum == 0)
				{

					string fileNameStr = string.Empty;
					if (mailitem.Subject != null)
						fileNameStr = mailitem.Subject.ToString().Trim() + "_" + mailitem.SenderEmailAddress.ToLower().ToString().Trim();
					else
						fileNameStr = mailitem.SenderEmailAddress.ToLower().ToString().Trim();



					if (fileNameStr.Length > 240)
						fileNameStr = fileNameStr.Substring(0, 240).Trim();

					fileNameStr = fileNameStr + "_" + DateTime.Now.ToString("hhmmss_ff");

					fileNameStr = fileNameStr.Replace("\\", "").Replace("\t", "").Replace("/", "").Replace(@"\u", "").Trim();


					string _fileStr = @"" + fileNameStr;

					string _fileStxpathDate = @"\\192.168.0.246\SKLOGIN\";


					_fileStr = _fileStxpathDate + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";


					_fileStr = Regex.Replace(_fileStr, @"[\/?:*""><|]+", "", RegexOptions.Compiled);


					// 폴더 생성
					if (Directory.Exists(_fileStxpathDate) == false)
						Directory.CreateDirectory(_fileStxpathDate);

					System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr);
					if (!fi.Exists)
					{
						mailitem.SaveAs(_fileStr, Outlook.OlSaveAsType.olMSG);
					}

					string fileName = _fileStr;


					// QUEUE INSERT
					InsertBotQue(CD_COMPANY, "SKLOGIN", "SKLOGIN", 10, "8");
				}

				CencelRemoteServer(serverIp);

			}
			catch (Exception e)
			{
				CencelRemoteServer(serverIp);
			}
		}


		//클라우독 미분류 저장
		private void FileServerCommonSave(Outlook.MailItem mailitem, string UserEmp, string UserName)
		{
			string serverIp = @"\\192.168.0.246";
			int resultNum = ConnectRemoteServer(serverIp);

			if (resultNum == 0)
			{
				UserEmp = UserEmp.ToLower();

				string fileNameStr = mailitem.Subject.Replace("\\", "");
				string _fileStr = @"" + fileNameStr;
				string _fileStxpath = "\\\\192.168.1.145\\c:\\INVOICE\\";
				_fileStr = _fileStxpath + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";

				// 폴더 생성
				if (Directory.Exists(_fileStxpath) == false)
					Directory.CreateDirectory(_fileStxpath);

				System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr);
				if (!fi.Exists)
				{
					mailitem.SaveAs(_fileStr, Outlook.OlSaveAsType.olMSG);
				}

				string fileName = _fileStr;
			}

			CencelRemoteServer(serverIp);
		}

		//클라우독 전달파일 저장
		private void FileServerBackupSave(Outlook.MailItem mailitem, string UserEmp, string UserName)
		{
			string serverIp = @"\\192.168.0.246";
			int resultNum = ConnectRemoteServer(serverIp);

			if (resultNum == 0)
			{
				UserEmp = UserEmp.ToLower();

				string fileNameStr = mailitem.Subject.Replace("\\", "");
				string _fileStr = @"" + fileNameStr;
				string _fileStxpath = "\\\\192.168.1.145\\e$\\plusdrive\\orgcowork\\00001\\data\\메일함\\2.전달\\";
				_fileStr = _fileStxpath + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";

				// 폴더 생성
				if (Directory.Exists(_fileStxpath) == false)
					Directory.CreateDirectory(_fileStxpath);

				System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr);
				if (!fi.Exists)
				{
					mailitem.SaveAs(_fileStr, Outlook.OlSaveAsType.olMSG);
				}

				string fileName = _fileStr;
			}

			CencelRemoteServer(serverIp);

			LogWrite(Color.OliveDrab, "[메일이동]	" + mailitem.Subject);
		}

		// 로그 저장
		private void LogWrite(Color color, string viewStr)
		{
			txtLog.SelectionColor = color;
			txtLog.AppendText("\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + viewStr);
			txtLog.Select(txtLog.Text.Length, 0);
			txtLog.ScrollToCaret();
		}

		private void LogWrite2(string viewStr)
		{
			txtLog.AppendText("\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + viewStr);
			txtLog.Select(txtLog.Text.Length, 0);
			txtLog.ScrollToCaret();
		}

		// 클라우독 파일 삭제
		private void CloudocDelete()
		{
			string serverIp = @"\\192.168.0.246";
			int resultNum = ConnectRemoteServer(serverIp);

			if (resultNum == 0)
			{
				string fileStxpath = "\\\\192.168.1.145\\c:\\INVOICE\\";

				DirectoryInfo dir = new DirectoryInfo(fileStxpath);
				System.IO.FileInfo[] files = dir.GetFiles("*.msg", SearchOption.AllDirectories);

				foreach (System.IO.FileInfo file in files)
				{
					file.Attributes = FileAttributes.Normal;
				}

				//Directory.Delete(Path, true);

				foreach (string Folder in Directory.GetDirectories(fileStxpath))

					foreach (string file in Directory.GetFiles(fileStxpath))
					{
						FileInfo fi = new FileInfo(file);
						if (fi.Extension.ToLower() == ".msg") continue; //ini파일은 남겨두고
						fi.Delete();
					}
			}

			CencelRemoteServer(serverIp);

			LogWrite(Color.OliveDrab, "[메일함삭제]	ClouDoc");
		}


		#region 클라우독
		// 클라우독 서버 저장
		//private void FileServerSave(Outlook.MailItem mailitem, string UserEmp, string UserName)
		//{
		//	string serverIp = @"\\192.168.1.145";
		//	int resultNum = ConnectRemoteServer(serverIp);

		//	if (resultNum == 0)
		//	{
		//		UserEmp = UserEmp.ToLower();

		//		string fileNameStr = mailitem.Subject;
		//		string _fileStr = @"" + fileNameStr;
		//		string _fileStxpath = "\\\\192.168.1.145\\e$\\plusdrive\\home\\00000\\data\\" + UserName + "(" + UserEmp + ")\\";
		//		_fileStr = _fileStxpath + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";


		//		_fileStr = Regex.Replace(_fileStr, @"[\/?:*""><|]+", "", RegexOptions.Compiled);


		//		// 폴더 생성
		//		if (Directory.Exists(_fileStxpath) == false)
		//			Directory.CreateDirectory(_fileStxpath);

		//		System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr);
		//		if (!fi.Exists)
		//		{
		//			mailitem.SaveAs(_fileStr, Outlook.OlSaveAsType.olMSG);
		//		}

		//		string fileName = _fileStr;
		//	}

		//	CencelRemoteServer(serverIp);
		//}

		// 클라우독 미분류 저장
		//private void FileServerCommonSave(Outlook.MailItem mailitem, string UserEmp, string UserName)
		//{
		//	string serverIp = @"\\192.168.1.145";
		//	int resultNum = ConnectRemoteServer(serverIp);

		//	if (resultNum == 0)
		//	{
		//		UserEmp = UserEmp.ToLower();

		//		string fileNameStr = mailitem.Subject.Replace("\\", "");
		//		string _fileStr = @"" + fileNameStr;
		//		string _fileStxpath = "\\\\192.168.1.145\\e$\\plusdrive\\orgcowork\\00001\\data\\메일함\\1.미분류\\";
		//		_fileStr = _fileStxpath + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";

		//		// 폴더 생성
		//		if (Directory.Exists(_fileStxpath) == false)
		//			Directory.CreateDirectory(_fileStxpath);

		//		System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr);
		//		if (!fi.Exists)
		//		{
		//			mailitem.SaveAs(_fileStr, Outlook.OlSaveAsType.olMSG);
		//		}

		//		string fileName = _fileStr;
		//	}

		//	CencelRemoteServer(serverIp);
		//}

		// 클라우독 전달파일 저장
		//private void FileServerBackupSave(Outlook.MailItem mailitem, string UserEmp, string UserName)
		//{
		//	string serverIp = @"\\192.168.1.145";
		//	int resultNum = ConnectRemoteServer(serverIp);

		//	if (resultNum == 0)
		//	{
		//		UserEmp = UserEmp.ToLower();

		//		string fileNameStr = mailitem.Subject.Replace("\\", "");
		//		string _fileStr = @"" + fileNameStr;
		//		string _fileStxpath = "\\\\192.168.1.145\\e$\\plusdrive\\orgcowork\\00001\\data\\메일함\\2.전달\\";
		//		_fileStr = _fileStxpath + _fileStr.Replace("-", "_").Replace("/", "_").Replace(" ", "_").Replace(".", "_").Replace(",", "").Replace(":", "").Replace("[", "").Replace("]", "").Replace("\"", "").Replace("*", "").Replace("?", "").Replace("=", "").Replace("<", "").Replace(">", "").Replace("|", "").Trim() + ".msg";

		//		// 폴더 생성
		//		if (Directory.Exists(_fileStxpath) == false)
		//			Directory.CreateDirectory(_fileStxpath);

		//		System.IO.FileInfo fi = new System.IO.FileInfo(_fileStr);
		//		if (!fi.Exists)
		//		{
		//			mailitem.SaveAs(_fileStr, Outlook.OlSaveAsType.olMSG);
		//		}

		//		string fileName = _fileStr;
		//	}

		//	CencelRemoteServer(serverIp);

		//	txtLog.SelectionColor = Color.OliveDrab;
		//	txtLog.AppendText("\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + "[메일이동]	" + mailitem.Subject);
		//	txtLog.Select(txtLog.Text.Length, 0);
		//	txtLog.ScrollToCaret();

		//}

		//// 클라우독 파일 삭제
		//private void CloudocDelete()
		//{
		//	string serverIp = @"\\192.168.1.145";
		//	int resultNum = ConnectRemoteServer(serverIp);

		//	if (resultNum == 0)
		//	{
		//		string fileStxpath = "\\\\192.168.1.145\\e$\\plusdrive\\orgcowork\\00001\\data\\RECYCLER\\";

		//		DirectoryInfo dir = new DirectoryInfo(fileStxpath);
		//		System.IO.FileInfo[] files = dir.GetFiles("*.msg", SearchOption.AllDirectories);

		//		foreach (System.IO.FileInfo file in files)
		//		{
		//			file.Attributes = FileAttributes.Normal;
		//		}

		//		//Directory.Delete(Path, true);

		//		foreach (string Folder in Directory.GetDirectories(fileStxpath))

		//			foreach (string file in Directory.GetFiles(fileStxpath))
		//			{
		//				FileInfo fi = new FileInfo(file);
		//				if (fi.Extension.ToLower() == ".msg") continue; //ini파일은 남겨두고
		//				fi.Delete();
		//			}
		//	}

		//	CencelRemoteServer(serverIp);

		//	txtLog.SelectionColor = Color.OliveDrab;
		//	txtLog.AppendText("\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + "[메일함삭제]	ClouDoc");
		//	txtLog.Select(txtLog.Text.Length, 0);
		//	txtLog.ScrollToCaret();
		//}

		#endregion 클라우독

		#endregion ▣ 메일 저장

		#region ▣ 네트워크 드라이브 (메일 1번 pc)
		// 공유연결
		public int ConnectRemoteServer(string server)
		{
			int capacity = 64;
			uint resultFlags = 0;
			uint flags = 0;
			System.Text.StringBuilder sb = new System.Text.StringBuilder(capacity);
			NETRESOURCE ns = new NETRESOURCE();
			ns.dwType = 1;              // 공유 디스크
			ns.lpLocalName = null;   // 로컬 드라이브 예비
			ns.lpRemoteName = server;
			ns.lpProvider = null;
			int result = 0;

			if (server == @"\\192.168.0.246") // 8번
			{
				result = WNetUseConnection(IntPtr.Zero, ref ns, "dintec5771", "administrator", flags,
											sb, ref capacity, out resultFlags);
			}
			else if (server == @"\\192.168.0.236") // 2번
			{
				result = WNetUseConnection(IntPtr.Zero, ref ns, "dintec5771", "administrator", flags,
											sb, ref capacity, out resultFlags);
			}
			else if (server == @"\\192.168.0.147") // MDSDOC
			{
				result = WNetUseConnection(IntPtr.Zero, ref ns, "dintec5771", "administrator", flags,
											sb, ref capacity, out resultFlags);
			}

			return result;
		}

		// 공유해제
		public void CencelRemoteServer(string server)
		{
			WNetCancelConnection2A(server, 1, 0);
		}
		#endregion ▣ 네트워크 드라이브

		#region ▣ 메일자동응답
		private void ReMailSend(string mailTo, string mailFrom, string mailDPFrom, string mailBCC, string mailSubject, string mailBody, string mailPW)
		{
			MailMessage message = new MailMessage();
			message.To.Add(mailTo);
			message.From = new MailAddress(mailFrom, mailDPFrom, System.Text.Encoding.UTF8);
			//MailAddress bcc = new MailAddress(mailBCC);

			////참조 메일계정 
			//message.Bcc.Add(bcc); 
			message.Subject = mailSubject;
			message.SubjectEncoding = UTF8Encoding.UTF8;
			message.Body = mailBody;
			message.BodyEncoding = UTF8Encoding.UTF8;
			//message.IsBodyHtml = true; 
			//메일내용이 HTML형식임 
			message.Priority = MailPriority.Normal;
			//중요도 높음 
			message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
			//메일 배달 실패시 알림 
			//Attachment attFile = new Attachment("d\\image1.jpg");
			//첨부파일 
			SmtpClient client = new SmtpClient("113.130.254.131", 587);
			//client.Host = "smtp.gmail.com"; 
			//SMTP(발송)서버 도메인 
			//client.Port = 587; 
			//25, SMTP서버 포트 
			client.EnableSsl = false;
			//SSL 사용 
			client.Timeout = 10000;
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.UseDefaultCredentials = false;
			client.Credentials = new System.Net.NetworkCredential(mailFrom, mailPW);
			//보내는 사람 메일 서버접속계정, 암호, Anonymous이용시 생략 
			client.Send(message);
			message.Dispose();

			LogWrite(Color.OliveDrab, "[회신메일]	" + mailSubject + " / " + mailDPFrom + " / " + NO_EMP + " / " + NO_MAIL + " / " + MAIL_SUBJECT);
		}

		#endregion ▣ 메일자동응답

		#region ▣ 기타
		public System.Data.DataTable GetTable(System.Data.SqlClient.SqlDataReader reader)
		{
			System.Data.DataTable table = reader.GetSchemaTable();
			System.Data.DataTable dt = new System.Data.DataTable();
			System.Data.DataColumn dc;
			System.Data.DataRow row;
			System.Collections.ArrayList aList = new System.Collections.ArrayList();

			for (int i = 0; i < table.Rows.Count; i++)
			{
				dc = new System.Data.DataColumn();

				if (!dt.Columns.Contains(table.Rows[i]["ColumnName"].ToString()))
				{
					dc.ColumnName = table.Rows[i]["ColumnName"].ToString();
					dc.Unique = Convert.ToBoolean(table.Rows[i]["IsUnique"]);
					dc.AllowDBNull = Convert.ToBoolean(table.Rows[i]["AllowDBNull"]);
					dc.ReadOnly = Convert.ToBoolean(table.Rows[i]["IsReadOnly"]);
					aList.Add(dc.ColumnName);
					dt.Columns.Add(dc);
				}
			}

			while (reader.Read())
			{
				row = dt.NewRow();
				for (int i = 0; i < aList.Count; i++)
				{
					row[((System.String)aList[i])] = reader[(System.String)aList[i]];
				}
				dt.Rows.Add(row);
			}
			return dt;
		}





		private void UpdateQuery()
		{
			string connectStr = "server = 192.168.1.143; uid = NEOE; pwd = NEOE; database = NEOE";

			SqlConnection sqlConn = new SqlConnection(connectStr);
			SqlCommand sqlComm = new SqlCommand();
			sqlComm.Connection = sqlConn;
			sqlComm.CommandText = "UPDATE A SET NO_TEST = B.NO_FILE FROM CZ_SA_QTN_PREREG   AS A JOIN CZ_SA_QTNH AS B ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_REF = B.NO_REF WHERE 1 = 1 	AND ISNULL(A.NO_REF, '') != ''";

			sqlConn.Open();
			sqlComm.ExecuteNonQuery();
			sqlConn.Close();
		}
		#endregion ▣ 기타

		#region ▣ 현대웹 등록
		private void HGSAutoInquiryInsert(string fileHGSNo, string cd_companyHGS, string no_empHGS, string fileHgsName)
		{
			try
			{
				InquiryParser parser = new InquiryParser(fileHgsName);
				parser.Parse(true);

				if (parser.Item.Rows.Count > 0)
				{
					// 호선 가져오기
					string query = @"
SELECT
	  A.NO_IMO
	, A.NO_HULL
	, A.NM_VESSEL
	, B.CD_PARTNER
	, B.LN_PARTNER
	, B.CD_PARTNER_GRP
FROM	  CZ_MA_HULL	AS A WITH(NOLOCK)
LEFT JOIN MA_PARTNER	AS B WITH(NOLOCK) ON A.CD_PARTNER = B.CD_PARTNER AND B.CD_COMPANY = @CD_COMPANY
WHERE A.NO_IMO = @NO_IMO OR (@NO_IMO IS NULL AND A.NM_VESSEL LIKE '%' + @NM_VESSEL + '%')";

					DBMgr dbm = new DBMgr();
					dbm.DebugMode = DebugMode.Print;
					dbm.Query = query;
					dbm.AddParameter("@CD_COMPANY", cd_companyHGS);
					dbm.AddParameter("@NO_IMO", parser.ImoNumber);
					dbm.AddParameter("@NM_VESSEL", parser.Vessel);
					DataTable dt = dbm.GetDataTable();

					string imonumber = string.Empty;
					string partnercode = string.Empty;
					string referencenumber = string.Empty;

					string no_file = string.Empty;

					int no_line = 1;
					string nm_subject = string.Empty;
					string unit = string.Empty;
					string cd_item_partner = string.Empty;
					string nm_item_partner = string.Empty;
					string cd_uniq_partner = string.Empty;
					string qt = string.Empty;
					string cd_po_partner = string.Empty;
					string cd_stock = string.Empty;
					string cd_rate = string.Empty;
					string vesselName = string.Empty;
					string tnid = string.Empty;
					string buyer = string.Empty;

					string contact = string.Empty;
					string filenameStr = string.Empty;


					NO_PREREG = fileHGSNo;
					partnercode = "11823";
					imonumber = parser.ImoNumber;

					if (string.IsNullOrEmpty(imonumber))
						imonumber = NO_IMO;


					// 문의번호
					referencenumber = parser.Reference;

					if (referencenumber.Equals(DateTime.Now.ToString("yyyy.MM.dd")))
						referencenumber = referencenumber + "." + partnercode + DateTime.Now.ToString("mmm");


					tnid = parser.Tnid;
					vesselName = parser.Vessel;
					// BUYER COMMENT 따로 저장
					buyer = parser.Buyer;
					contact = parser.Contact;
					filenameStr = parser.InqFileName;

					DataTable dtItem = new DataTable();


					dtItem.Columns.Add("CD_COMPANY");
					dtItem.Columns.Add("NO_PREREG");
					dtItem.Columns.Add("NO_LINE");
					dtItem.Columns.Add("CD_PARTNER");
					dtItem.Columns.Add("NO_IMO");
					dtItem.Columns.Add("SHIPSERV_TNID");
					dtItem.Columns.Add("NM_VESSEL");
					dtItem.Columns.Add("NO_REF");
					dtItem.Columns.Add("NO_REQREF");
					dtItem.Columns.Add("NM_SUBJECT");
					dtItem.Columns.Add("CD_ITEM_PARTNER");
					dtItem.Columns.Add("NM_ITEM_PARTNER");
					dtItem.Columns.Add("CD_UNIQ_PARTNER");
					dtItem.Columns.Add("UNIT");
					dtItem.Columns.Add("QT");
					dtItem.Columns.Add("SORTED_BY");
					dtItem.Columns.Add("COMMENT");
					dtItem.Columns.Add("CONTACT");
					dtItem.Columns.Add("NM_FILE");
					dtItem.Columns.Add("DXCODE_SUBJ");
					dtItem.Columns.Add("DXCODE_ITEM");


					DataTable dtItemVendor = new DataTable();
					dtItemVendor.Columns.Add("CD_COMPANY");
					dtItemVendor.Columns.Add("NO_PREREG");
					dtItemVendor.Columns.Add("NO_LINE");
					dtItemVendor.Columns.Add("DXVENDOR_CODE");


					DataTable dtItemHead = new DataTable();
					dtItemHead.Columns.Add("CD_COMPANY");
					dtItemHead.Columns.Add("NO_PREREG");
					dtItemHead.Columns.Add("NO_FILE");
					dtItemHead.Columns.Add("NO_EMP");
					dtItemHead.Columns.Add("CD_PARTNER");
					dtItemHead.Columns.Add("NO_IMO");



					foreach (DataRow row in parser.Item.Rows)
					{
						nm_subject = row["SUBJ"].ToString().ToUpper();
						unit = row["UNIT"].ToString().ToUpper();
						cd_item_partner = row["ITEM"].ToString().ToUpper();
						nm_item_partner = row["DESC"].ToString().ToUpper();
						cd_uniq_partner = row["UNIQ"].ToString().ToUpper();
						qt = row["QT"].ToString();

						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_COMPANY"] = cd_companyHGS;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_PREREG"] = no_file;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_LINE"] = no_line;
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_PARTNER"] = partnercode;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_IMO"] = imonumber;
						dtItem.Rows[dtItem.Rows.Count - 1]["SHIPSERV_TNID"] = tnid;
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_VESSEL"] = vesselName;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_REF"] = referencenumber;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_REQREF"] = "";
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_SUBJECT"] = nm_subject;
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_ITEM_PARTNER"] = cd_item_partner;
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_ITEM_PARTNER"] = nm_item_partner;
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_UNIQ_PARTNER"] = cd_uniq_partner;
						if (unit.Contains(",")) unit = "";
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = unit;
						if (string.IsNullOrEmpty(qt)) qt = "0";
						if (qt.Contains(",")) qt = qt.Replace(",", "").Trim();
						dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = qt;
						dtItem.Rows[dtItem.Rows.Count - 1]["SORTED_BY"] = "HGS";
						dtItem.Rows[dtItem.Rows.Count - 1]["COMMENT"] = buyer;
						dtItem.Rows[dtItem.Rows.Count - 1]["CONTACT"] = contact;
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_FILE"] = filenameStr;
						dtItem.Rows[dtItem.Rows.Count - 1]["DXCODE_SUBJ"] = Util.GetDxCode(nm_subject.ToUpper());
						dtItem.Rows[dtItem.Rows.Count - 1]["DXCODE_ITEM"] = Util.GetDxCode(cd_item_partner.ToUpper()) + "‡" + nm_item_partner.ToUpper();

						dtItemVendor.Rows.Add();
						dtItemVendor.Rows[dtItem.Rows.Count - 1]["CD_COMPANY"] = cd_companyHGS;
						dtItemVendor.Rows[dtItem.Rows.Count - 1]["NO_PREREG"] = fileHGSNo;
						dtItemVendor.Rows[dtItem.Rows.Count - 1]["NO_LINE"] = no_line;
						dtItemVendor.Rows[dtItem.Rows.Count - 1]["DXVENDOR_CODE"] = partnercode;

						no_line += 1;
					}

					dtItemHead.Rows.Add();
					dtItemHead.Rows[0]["CD_COMPANY"] = cd_companyHGS;
					dtItemHead.Rows[0]["NO_PREREG"] = fileHGSNo;
					dtItemHead.Rows[0]["NO_FILE"] = fileHGSNo;
					dtItemHead.Rows[0]["NO_EMP"] = no_empHGS;
					dtItemHead.Rows[0]["CD_PARTNER"] = partnercode;
					dtItemHead.Rows[0]["NO_IMO"] = imonumber;

					string xml = Util.GetTO_Xml(dtItem);
					string xmlVendor = Util.GetTO_Xml(dtItemVendor);
					string xmlHead = Util.GetTO_Xml(dtItemHead);

					SQL.ExecuteNonQuery("SP_CZ_SA_QTN_PREREG", SQLDebug.Print, xml);
					SQL.ExecuteNonQuery("SP_CZ_SA_QTN_PREREG_VENDOR", SQLDebug.Print, xmlVendor);
					SQL.ExecuteNonQuery("SP_CZ_SA_QTN_PREREG_HEAD", SQLDebug.Print, xmlHead);

					RPA rpa = new RPA() { Process = "INQ", FileNumber = fileHGSNo, PartnerCode = "11823" };
					rpa.AddQueue();
					//rpa.AddQueue();
				}
			}
			catch (Exception e)
			{

			}
		}




		#endregion ▣ 현대웹 등록
		public class sendData  // 송신 형식

		{

			public string sendKey { get; set; }

			public string sendDate { get; set; }

			public string sendDesc { get; set; }

		}



		public class Result  // 수신 형식

		{

			public string retnCode { get; set; }

			public string retnDate { get; set; }

			public string retnDesc { get; set; }

		}

		#region 테크로스
		private void BtnTCRS_Click(object sender, EventArgs e)
		{
			TcrsSend p = new TcrsSend();
			p.TcrsSend_Re();

			ShowMessage("작업을 완료하였습니다.");
		}
		#endregion 테크로스



		#region ==================== JIBE

		#region ========================= PULL RFQ =========================

		private void btnPullRequest_Click(object sender, EventArgs e)
		{
			try
			{
				string jsonFromFile = string.Empty;

				// PULL REQUEST
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://jcds.jibe.solutions/api/supplier/pull_json/");
				//HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://jcds-test.jibe.solutions/api/supplier/pull_json/");
				httpWebRequest.ContentType = "application/json";
				httpWebRequest.Method = "POST";

				//HttpWebResponse responseData = null;

				using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				{

					var JibeValue = jibe.JibeWrite("rfq");

					var jsonToWrite = JsonConvert.SerializeObject(JibeValue, Newtonsoft.Json.Formatting.Indented);

					streamWriter.Write(jsonToWrite);
				}


				//responseData = (HttpWebResponse)httpWebRequest.GetResponse();

				HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

				using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();
					jsonFromFile = result;
				}

				//jsonFromFile = System.IO.File.ReadAllText(@"c:\jibe11.json");


				JArray obj = JArray.Parse(jsonFromFile);

				if (obj.Count == 1)
				{
					if (!string.IsNullOrEmpty(jsonFromFile) && !jsonFromFile.Contains("status"))
					{
						LogWrite(Color.DimGray, "[JiBe]	" + "\r\n" + jsonFromFile);

						// 문서가 있을때.
						foreach (JObject itemObj in obj)
						{
							string testBody = itemObj["body"].ToString();

							JArray objBody = JArray.Parse(itemObj["body"].ToString());

							// rfq 갯수 만큼 반복
							//foreach (JObject itemObj2 in objBody)
							//{
							JArray objBody_body = JArray.Parse(itemObj["body"].ToString());

							jibe.RequestInsertDb(objBody, objBody_body);

							LogWrite(Color.DimGray, "[JiBe]	자동 등록 완료");
						}
					}
					else if (jsonFromFile.Contains("status"))
					{
						LogWrite(Color.DimGray, "[JiBe]	수신 데이터 없음");
					}
				}
			}
			catch (Exception ex)
			{

			}
		}

		#endregion ========================= PULL RFQ =========================



		#region ========================= PUSH QUTATION =========================


		private void btnQuotation_Click(object sender, EventArgs e)
		{
			string fileKey = tbxFileNo.Text;

			string query = string.Empty;
			query = "SELECT A.*, B.NM_ENG, CODE.NM_SYSDEF AS NM_EXCH FROM CZ_SA_QTNH AS A JOIN MA_EMP AS B ON B.NO_EMP = A.NO_EMP JOIN MA_CODEDTL AS CODE ON A.CD_EXCH = CODE.CD_SYSDEF AND A.CD_COMPANY = CODE.CD_COMPANY AND CODE.CD_FIELD = 'MA_B000005' WHERE NO_FILE = '" + fileKey + "'";
			DataTable qtnH = DBMgr.GetDataTable(query);

			query = "SELECT * FROM CZ_SA_QTNL WHERE NO_FILE = '" + fileKey + "'";
			DataTable qtnL = DBMgr.GetDataTable(query);

			query = "SELECT * FROM CZ_SA_QTN_PREREG_HEAD AS HEAD LEFT JOIN CZ_SA_QTN_PREREG AS PRE ON HEAD.NO_PREREG = PRE.NO_PREREG AND HEAD.CD_COMPANY = PRE.CD_COMPANY WHERE 1=1 AND HEAD.CD_COMPANY = 'K100' AND HEAD.NO_FILE = '" + fileKey + "'";
			DataTable preg = DBMgr.GetDataTable(query);

			string tokenvalue = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkYXRhIjp7ImNsaWVudF91aWQiOiJkM2Q5ODkxMi1hMTY1LTRhODYtOGY4YS1jNWQ3Y2IyMjZkMTMiLCJzdXBwbGllcl91aWQiOiJCMEJFNjQ4My01MkVCLTQzODktQkE5OC0yRkQ1Q0EyMkE5M0IifSwiaWF0IjoxNTY3NzYzNTE0LCJleHAiOjE1OTkyOTk1MTR9.c3jbm6laoe2aFLcXwvRDt6AIRMWT51ZCaEzv0pE3Q2g";
			//string tokenvalue = "c3jbm6laoe2aFLcXwvRDt6AIRMWT51ZCaEzv0pE3Q2g";

			if (qtnH != null && qtnL != null)
			{
				// QUOTATION
				//HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://jcds.jibe.solutions/api/supplier/quotation_response/");
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://jcds-test.jibe.solutions/api/supplier/quotation_response/");
				httpWebRequest.ContentType = "application/json";
				httpWebRequest.Method = "POST";
				httpWebRequest.Headers.Add("token", tokenvalue);


				HttpWebResponse responseData = null;

				using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				{
					var JibeValue = jibe.JibeQuotationWrite(qtnH, qtnL, preg);

					var jsonToWrite = JsonConvert.SerializeObject(JibeValue, Newtonsoft.Json.Formatting.Indented);

					streamWriter.Write(jsonToWrite);
				}

				responseData = (HttpWebResponse)httpWebRequest.GetResponse();

				HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
				{
					var result = streamReader.ReadToEnd();

					txtLog.AppendText(result);
				}
			}
		}

		#endregion ========================= PUSH QUTATION =========================



		#region ========================= STATUS =========================


		private void BtnStatus_Click(object sender, EventArgs e)
		{
			// supplier_uid :  B0BE6483-52EB-4389-BA98-2FD5CA22A93B
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://jcds.jibe.solutions/api/supplier/pull_json_status/");
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";

			HttpWebResponse responseData = null;

			using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				var JibeValue = "";// JibeWrite("");

				var jsonToWrite = JsonConvert.SerializeObject(JibeValue, Newtonsoft.Json.Formatting.Indented);

				streamWriter.Write(jsonToWrite);
			}

			responseData = (HttpWebResponse)httpWebRequest.GetResponse();

			HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				var result = streamReader.ReadToEnd();
			}
		}

		#endregion ========================= STATUS =========================



		#region ========================= PULL PO =========================

		private void BtnPULLPO_Click(object sender, EventArgs e)
		{
			try
			{

				//string jsonFromFile = string.Empty;

				//// PULL REQUEST
				//// supplier_uid :  B0BE6483-52EB-4389-BA98-2FD5CA22A93B
				////HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://jcds.jibe.solutions/api/supplier/pull_json/");
				//HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://jcds-test.jibe.solutions/api/supplier/pull_json/");
				//httpWebRequest.ContentType = "application/json";
				//httpWebRequest.Method = "POST";

				//HttpWebResponse responseData = null;

				//using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				//{

				//	var JibeValue = JibeWrite("po");

				//	var jsonToWrite = JsonConvert.SerializeObject(JibeValue, Formatting.Indented);

				//	streamWriter.Write(jsonToWrite);
				//}

				//responseData = (HttpWebResponse)httpWebRequest.GetResponse();

				//HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

				//using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
				//{
				//	var result = streamReader.ReadToEnd();
				//	jsonFromFile = result;
				//}

				//#region 자이브 파일 처리
				//if (jsonFromFile.StartsWith("["))
				//{
				//	jsonFromFile = jsonFromFile.Substring(1, jsonFromFile.Length - 1).Trim();

				//	if (jsonFromFile.EndsWith("]"))
				//		jsonFromFile = jsonFromFile.Substring(0, jsonFromFile.Length - 1).Trim();

				//	if (jsonFromFile.Contains("\"body\":[{\"keys\""))
				//	{
				//		jsonFromFile = jsonFromFile.Replace("\"body\":[{\"keys\"", "\"body\":{\"keys\"");
				//		jsonFromFile = jsonFromFile.Replace("}}]}", "}}}");
				//	}
				//}
				//#endregion 자이브 파일 처리

				//txtLog.Text = jsonFromFile;

				////requestGetRoot rootRequest = JsonConvert.DeserializeObject<requestGetRoot>(jsonFromFile);

				//JObject obj = JObject.Parse(jsonFromFile);

				//JObject objStatus = JObject.Parse(obj["status"].ToString());

				////if (rootRequest.status != null && rootRequest.status.Count > 0)
				//if(objStatus == null)
				//{
				//	// 문서가 없을때.
				//	JArray statusArray = JArray.Parse(obj["status"].ToString());

				//	foreach (JObject itemObj in statusArray)
				//	{
				//		txtLog.Text += Environment.NewLine;
				//		txtLog.Text += " none : " + itemObj["code"].ToString() + " / " + itemObj["message"].ToString();
				//	}
				//}
				//else
				//{

				//		// 문서가 있을때.
				//		JArray objBody = JArray.Parse(obj["body"].ToString());
				//	JArray objBody_body = JArray.Parse(objBody["body"].ToString());
				//	JArray itemArray = JArray.Parse(objBody_body["items"].ToString());

				//	RequestInsertDb(objBody, objBody_body, itemArray);
				//}
			}
			catch (Exception ex)
			{

			}
		}
		#endregion ========================= PULL PO =========================

		#endregion ==================== JIBE

		public static List<string> FileCode()
		{
			List<string> fileCode = new List<string>();

			if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
			{
				fileCode.AddRange(new string[] { "FB", "DB", "NB", "SB", "NS", "TE" });  // 대표 파일
				fileCode.AddRange(new string[] { "CL", "DS", "ST" });                    // 특별 케이스
			}
			else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
			{
				fileCode.AddRange(new string[] { "A-", "D-" });
				fileCode.AddRange(new string[] { "CN" });
			}

			return fileCode;
		}


	}
}

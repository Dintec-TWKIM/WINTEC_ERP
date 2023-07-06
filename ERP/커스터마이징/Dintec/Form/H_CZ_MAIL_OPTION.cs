using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using DzHelpFormLib;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Windows.Print;

using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using Duzon.Common.BpControls;
using cz;
using Aspose.Email.Outlook;

namespace Dintec
{
	public partial class H_CZ_MAIL_OPTION : Duzon.Common.Forms.CommonDialog
	{
		public string FileNumber { get; set; }
		public string RefNumber { get; set; }
		public MailType Type { get; set; }

		public string From { get; set; }
		public string To { get; set; }
		public string Cc { get; set; }

		public string Company1 { get; set; }
		public string Homepage { get; set; }

		public string Title { get; set; }
		public string Signature { get; set; }

		public string[] Attachment { get; set; }

		public string EmpCode { get; set; }
		public string EmpNameKr { get; set; }
		public string EmpNameEn { get; set; }
		public string EmpTelNumber { get; set; }
		public string EmpFaxNumber { get; set; }

		public string PartnerCode { get; set; }
		public string AreaCode { get; set; }



		string CompanyCode;
		string CompanyCodeWf;
		readonly string AddedCc;

		MapiMessage ReplyTarget;


		#region ==================================================================================================== Constructor

		public H_CZ_MAIL_OPTION(string companyCode, MailType type, string fileNumber, string empCode, string addedCc, string companyCodeWf)
		{
			InitializeComponent();

			CompanyCode = companyCode;
			CompanyCodeWf = companyCodeWf;
			Type = type;
			FileNumber = fileNumber;
			EmpCode = empCode;

			AddedCc = addedCc;	
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitEvent();

			// 라이선스 인증
			Aspose.Email.License license = new Aspose.Email.License();
			license.SetLicense("Aspose.Email.lic");
		}

		private void InitEvent()
		{
			btn보내기.Click += new EventHandler(btn보내기_Click);
		}

		protected override void InitPaint()
		{
			btn보내기.Focus();
			string query = "";

			// ********** 답장 대상 메일
			// 답장 해야할 워크플로우 단계
			string replyStep = "";

			if (Type == MailType.Quotation)
				replyStep = "01";
			else if (Type == MailType.Acknowledge)
				replyStep = "08";

			// 답장 대상 메일 가져오기
			query = @"
SELECT
	  NM_FILE_REAL
	, ROW_NUMBER() OVER (ORDER BY NO_LINE DESC)	AS RN
FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + CompanyCodeWf + @"'
	AND NO_KEY = '" + FileNumber + @"'
	AND TP_STEP = '" + replyStep + @"'
	AND RIGHT(NM_FILE_REAL, 3) = 'MSG'";

			DataTable dtMsg = DBMgr.GetDataTable(query);

			if (dtMsg.Rows.Count > 0)
			{
				string fileName = FileMgr.Download_WF(CompanyCodeWf, FileNumber, dtMsg.Select("RN = 1")[0][0].ToString() , false);
				ReplyTarget = MapiMessage.FromFile(Application.StartupPath + @"\temp\" + fileName);
			}

			// ********** 딘텍 담당자 이메일
			query = @"
SELECT
	A.*
,	C.E_MAIL	AS NO_EMAIL_TEAM
FROM V_CZ_MA_EMP	AS A WITH(NOLOCK)
JOIN MA_USER		AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_EMP = B.ID_USER
JOIN MA_SALEGRP		AS C WITH(NOLOCK) ON B.CD_COMPANY = C.CD_COMPANY AND B.CD_SALEGRP = C.CD_SALEGRP
WHERE 1 = 1
	AND A.CD_COMPANY = '" + CompanyCode + @"'
	AND A.NO_EMP = '" + EmpCode + "'";

			DataTable dtEmp = DBMgr.GetDataTable(query);

			if (Type == MailType.Quotation || Type == MailType.Acknowledge)
				From = dtEmp.Rows[0]["NO_EMAIL_TEAM"].ToString();
			else
				From = dtEmp.Rows[0]["NO_EMAIL_EMP"].ToString();

			Cc = dtEmp.Rows[0]["NO_EMAIL_EMP"].ToString();

			EmpNameKr = dtEmp.Rows[0]["NM_EMP"].ToString();
			EmpNameEn = dtEmp.Rows[0]["EN_EMP"].ToString();
			EmpTelNumber = dtEmp.Rows[0]["NO_TEL_EMP"].ToString();
			EmpFaxNumber = dtEmp.Rows[0]["NO_FAX_EMP"].ToString();

			// ********** 거래처 담당자 이메일
			if (Type == MailType.Quotation)
			{
				query = @"
SELECT
	A.CD_PARTNER
,	A.NO_REF
,	CASE WHEN ISNULL(A.SEQ_ATTN, 0) = 0
		THEN B.E_MAIL
		ELSE C.NM_EMAIL
	END			AS E_MAIL
FROM	 CZ_SA_QTNH		AS A WITH(NOLOCK)
LEFT JOIN MA_PARTNER	AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_PARTNER = B.CD_PARTNER
LEFT JOIN FI_PARTNERPTR	AS C WITH(NOLOCK) ON A.CD_COMPANY = C.CD_COMPANY AND A.CD_PARTNER = C.CD_PARTNER AND A.SEQ_ATTN = C.SEQ
WHERE 1 = 1
	AND A.CD_COMPANY = '" + CompanyCode + @"'
	AND A.NO_FILE = '" + FileNumber + "'";

				DataTable dtQtn = DBMgr.GetDataTable(query);
				PartnerCode = dtQtn.Rows[0]["CD_PARTNER"].ToString();
				RefNumber = dtQtn.Rows[0]["NO_REF"].ToString();
				To = dtQtn.Rows[0]["E_MAIL"].ToString();
			}
			else if (Type == MailType.Acknowledge)
			{
				query = "SELECT CD_PARTNER, NO_PO_PARTNER FROM SA_SOH WHERE CD_COMPANY = '" + CompanyCode + "' AND NO_SO = '" + FileNumber + "'";

				DataTable dtSo = DBMgr.GetDataTable(query);
				RefNumber = dtSo.Rows[0]["NO_PO_PARTNER"].ToString();
				PartnerCode = dtSo.Rows[0]["CD_PARTNER"].ToString();
				To = ReplyTarget.SenderEmailAddress;
			}

			// ********** 거래처 지역구분
			query = "SELECT CD_AREA FROM MA_PARTNER WHERE CD_COMPANY = '" + CompanyCode + "' AND CD_PARTNER = '" + PartnerCode + "'";

			DataTable dtPartner = DBMgr.GetDataTable(query);			
			AreaCode = dtPartner.Rows[0]["CD_AREA"].ToString();

			// 싱가폴의 경우 무조건 200
			if (CompanyCode == "S100")
				AreaCode = "200";

			// ********** 본문 만들기
			// 서명에 회사이름
			if (CompanyCode == "TEST" && AreaCode == "100") Company1 = "(주) 딘 텍";
			if (CompanyCode == "K100" && AreaCode == "100") Company1 = "(주) 딘 텍";
			if (CompanyCode == "K200" && AreaCode == "100") Company1 = "(주) 두 베 코";
			if (CompanyCode == "S100" && AreaCode == "100") Company1 = "(주) 딘 텍";
			if (CompanyCode == "TEST" && AreaCode != "100") Company1 = "DINTEC CO., LTD.";
			if (CompanyCode == "K100" && AreaCode != "100") Company1 = "DINTEC CO., LTD.";
			if (CompanyCode == "K200" && AreaCode != "100") Company1 = "Dubhe Co., Ltd.";
			if (CompanyCode == "S100" && AreaCode != "100") Company1 = "DINTEC (SINGAPORE) PTE. LTD.";

			// 서명에 홈페이지
			if (CompanyCode == "TEST") Homepage = "www.dintec.co.kr";
			if (CompanyCode == "K100") Homepage = "www.dintec.co.kr";
			if (CompanyCode == "K200") Homepage = "www.dubheco.com";
			if (CompanyCode == "S100") Homepage = "www.dintec.co.kr";

			// 제목 및 서명
			if (Type == MailType.Quotation)
			{
				// 메일 제목
				if (CompanyCode == "TEST") Title = "DINTEC - QUOTATION(" + FileNumber + ")" + "_" + PartnerCode;
				if (CompanyCode == "K100") Title = "DINTEC - QUOTATION(" + FileNumber + ")" + "_" + PartnerCode;
				if (CompanyCode == "K200") Title = "DUBHECO - QUOTATION(" + FileNumber + ")" + "_" + PartnerCode;
				if (CompanyCode == "S100") Title = "DINTEC - QUOTATION(" + FileNumber + ")" + "_" + PartnerCode;

				// 서명
				Signature = SignQuotation;
			}
			else if (Type == MailType.Acknowledge)
			{
				// 메일 제목
				if (CompanyCode == "TEST") Title = "DINTEC - ACKNOWLEDGE(" + FileNumber + ")" + "_" + PartnerCode;
				if (CompanyCode == "K100") Title = "DINTEC - ACKNOWLEDGE(" + FileNumber + ")" + "_" + PartnerCode;
				if (CompanyCode == "K200") Title = "DUBHECO - ACKNOWLEDGE(" + FileNumber + ")" + "_" + PartnerCode;
				if (CompanyCode == "S100") Title = "DINTEC - ACKNOWLEDGE(" + FileNumber + ")" + "_" + PartnerCode;

				// 서명
				Signature = SignAcknowledge;
			}

			// ********** 첨부파일
			// 타입에 따라 단계 설정
			string attachedStep = "";

			if (Type == MailType.Quotation)
				attachedStep = "'05','58'";
			else if (Type == MailType.Acknowledge)
				attachedStep = "'09'";

			query = @"
SELECT
	NM_FILE			AS DC_FILE
,	NM_FILE_REAL	AS NM_FILE
,	FILE_DATA		AS FILE_DATA
FROM FT_CZ_MA_WORKFLOWL('" + CompanyCodeWf + "', '" + FileNumber + @"')
WHERE 1 = 1
	AND TP_STEP IN (" + attachedStep + @")
	AND RN = 1";

			DataTable dtFile = DBMgr.GetDataTable(query);
			List<string> fileList = new List<string>();

			foreach (DataRow row in dtFile.Rows)
			{
				string fileDisp = row["DC_FILE"].ToString();
				string fileName = row["NM_FILE"].ToString();
				var fileData = row["FILE_DATA"];

				if (fileName != "")
					fileList.Add(fileDisp + "|" + fileName);
				else if (fileData.ToString() != "")				
					fileList.Add(fileDisp + "|BINARY" + Convert.ToBase64String((byte[])fileData));					
			}

			// Cert 및 Letter 첨부
			if (Type == MailType.Quotation && CompanyCode == "K100")
			{			
				query = @"
SELECT
	'Upload\' + ID_MENU + '\' + CD_FILE + '\' + FILE_NAME
FROM MA_FILEINFO AS A WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + CompanyCode + @"'
	AND CD_MODULE = 'MA'
	AND ID_MENU = 'P_CZ_MA_PARTNER'
	AND CD_FILE IN (SELECT CD_SUPPLIER FROM CZ_SA_QTNL WHERE CD_COMPANY = A.CD_COMPANY AND NO_FILE = '" + FileNumber + @"')
	AND (FILE_NAME LIKE 'CERT%.PDF' OR FILE_NAME LIKE 'LETTER%.PDF')";

				DataTable dtCert = DBMgr.GetDataTable(query);

				for (int i = 0; i < dtCert.Rows.Count; i++)
					fileList.Add(dtCert.Rows[i][0].ToString());
			}

			Attachment = fileList.ToArray();
		}

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btn보내기_Click(object sender, EventArgs e)
		{
			string html = "";

			if (rdo본문방식2.Checked && ReplyTarget != null)
			{
				// 작성메일 제목
				Title = "RE: " + ReplyTarget.Subject + " (" + FileNumber + ")";

				// 원본메일 본문
				string oBody = ReplyTarget.BodyHtml;

				// 원본메일 보내는 사람
				string oFrom = ReplyTarget.SenderName + "&lt;" + ReplyTarget.SenderEmailAddress + "&gt;";

				// 원본메일 받는사람, 참조
				string oTo = "";
				string oCc = "";

				foreach (MapiRecipient r in ReplyTarget.Recipients)
				{
					if (r.RecipientType == MapiRecipientType.MAPI_TO)
					{
						if (r.DisplayName == r.EmailAddress)
							oTo += "; " + r.DisplayName;
						else
							oTo += "; " + r.DisplayName + "&lt;" + r.EmailAddress + "&gt;";
					}
					else if (r.RecipientType == MapiRecipientType.MAPI_CC)
					{
						if (r.DisplayName == r.EmailAddress)
							oCc += "; " + r.DisplayName;
						else
							oCc += "; " + r.DisplayName + "&lt;" + r.EmailAddress + "&gt;";
					}
				}

				if (oTo != "")
					oTo = oTo.Substring(2);

				if (oCc != "")
					oCc = oCc.Substring(2);

				// 원본메일 발송시간
				string oSent = ReplyTarget.DeliveryTime.ToString();

				// 원본메일 제목
				string oSubject = ReplyTarget.Subject;

					// Html 만들기
				html = @"
<br /><br />
<div>
-----Original Message-----<br />
<strong>From:</strong> " + oFrom + @"<br />
<strong>To:</strong> " + oTo + @"<br />
<strong>Cc:</strong> " + oCc + @"<br />
<strong>Sent:</strong> " + oSent + @"<br />
<strong>Subject:</strong> " + oSubject + @"<br />
</div>
<br /><br />" + oBody;

			}
			
			// 메일발송 팝업
			P_CZ_MA_EMAIL_SUB f = new P_CZ_MA_EMAIL_SUB(From, To, Cc + AddedCc, "", Title, Attachment, null, Signature, html, FileNumber, PartnerCode, false);
			f.ShowDialog();
			DialogResult = f.DialogResult;
		}

		#endregion

		#region ==================================================================================================== 서명

		public string SignQuotation
		{
			get
			{
				if (CompanyCode == "S100")
				{
					return @"
Dear Sir & Madam,

Thank you for your inquiry.

Please find the attached quotation " + FileNumber + " for your reference " + RefNumber + @"

For future inquiries and requests, please send your email to:" + From + @"

Best regards,

" + EmpNameEn + @"
" + Company1 + @"
Tel     : " + EmpTelNumber.Substring(1) + @"
E-mail : " + From + @"
Homepage : " + Homepage;
				}
				else
				{
					if (AreaCode == "100")
					{
						return "";
					}
					else
					{
						if (CompanyCode == "K100")
							return @"
Dear Sir & Madam,

Thank you for your inquiry.

Please find the attached quotation " + FileNumber + " for your reference " + RefNumber + @"

For future inquiries and requests, please send your email to:" + From + @"

Best regards,

" + EmpNameEn + @"
" + Company1 + @"
Tel     : +82-" + EmpTelNumber.Substring(1) + @"
E-mail : " + From + @"
Homepage : " + Homepage;
						else if (CompanyCode == "K200")
							return @"
Dear Sir & Madam,

Thank you for your inquiry.

Please find the attached quotation " + FileNumber + " for your reference " + RefNumber + @"

For future inquiries and requests, please send your email to:" + From + @"

Best regards,

" + EmpNameEn + @"
" + Company1 + @"
The authorized sales agent of the HSD(ex-Doosan) engine
3FL, 297, Jungang-daero, Dong-gu, Busan, Republic of Korea / postcode: 48792
Tel     : +82-" + EmpTelNumber.Substring(1) + @"
E-mail : " + From + @"
Homepage : " + Homepage + @"
SHIP SERV TRADENET ID# 228673";
						else
							return "";
					}
				}
			}
		}

		public string SignAcknowledge
		{
			get
			{
				if (AreaCode == "100")
				{
					return "";
				}
				else
				{
					if (CompanyCode == "K100")
					{
						return @"
Good day.

Thank you for your valuable order.

We are pleased to send order confirmation for your PO# " + RefNumber + " / " + FileNumber + @"

When the order gets ready, we will inform you.

Have a nice day,

Thanks.
Best Regards,

Do you want to know order status? Please click here right now!! (http://tracking.dintec.co.kr)

" + EmpNameEn + @"
" + Company1 + @"
Tel     : +82-" + EmpTelNumber.Substring(1) + @"
E-mail : " + From + @", service@dintec.co.kr
Homepage : " + Homepage;
					}
					else
					{
						return @"
Good day.

Thank you for your valuable order.

We are pleased to send order confirmation for your PO# " + RefNumber + " / " + FileNumber + @"

When the order gets ready, we will inform you.

Have a nice day,

Thanks.
Best Regards,

" + EmpNameEn + @"
" + Company1 + @"
Tel     : +82-" + EmpTelNumber.Substring(1) + @"
E-mail : " + From + @"
Homepage : " + Homepage;
					}
				}
			}
		}

		#endregion
	}

	public enum MailType
	{
		Quotation
		, Acknowledge
		, PurchaseOrder
		, Inquiry
	}
}

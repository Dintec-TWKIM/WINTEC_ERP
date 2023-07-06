using System.Data;
using Duzon.Common.Forms;
using Aspose.Email.Outlook;

namespace Dintec
{
	public class MailAddr
	{
		public string CD_COMPANY { get; set; }
		public string NO_FILE { get; set; }
		public string NO_REF { get; set; }
		public MailType Type { get; set; }

		public string From { get; set; }
		public string To { get; set; }
		public string Dear { get; set; }

		public string Company1 { get; set; }
		public string Company2 { get; set; }
		public string Homepage { get; set; }

		public string Title { get; set; }
		public string Signature { get; set; }

		public string[] Attachment { get; set; }

		public string NO_EMP { get; set; }
		public string NM_EMP { get; set; }
		public string EN_EMP { get; set; }
		public string NO_TEL_EMP { get; set; }
		public string NO_FAX_EMP { get; set; }

		public string CD_PARTNER { get; set; }
		public string CD_AREA { get; set; }

		

		

		public MailAddr()
		{
			this.CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
		}

		public MailAddr(MailType type, string NO_FILE, string NO_EMP)
		{
			this.CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			this.Type = MailType.Quotation;
			this.NO_FILE = NO_FILE;
			this.NO_EMP = NO_EMP;

			Binding();
		}

		public void Binding()
		{			
			string query = "";

			// ***** 딘텍 담당자 이메일
			query = @"
SELECT
	*
FROM V_CZ_MA_EMP
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND NO_EMP = '" + NO_EMP + "'";

			DataTable dtEMP = DBMgr.GetDataTable(query);
			From	   = dtEMP.Rows[0]["NO_EMAIL_EMP"].ToString();
			NM_EMP	   = dtEMP.Rows[0]["NM_EMP"].ToString();
			EN_EMP	   = dtEMP.Rows[0]["EN_EMP"].ToString();
			NO_TEL_EMP = dtEMP.Rows[0]["NO_TEL_EMP"].ToString();
			NO_FAX_EMP = dtEMP.Rows[0]["NO_FAX_EMP"].ToString();

			// ***** 거래처 담당자 이메일
			// 타입에 따라 조인 테이블 및 키 컬럼 결정
			string table = "";
			string field = "";

			if (Type == MailType.Quotation)
			{
				table = "CZ_SA_QTNH";
				field = "NO_FILE";
			}
			else if (Type == MailType.Acknowledge)
			{
				table = "SA_SOH";
				field = "NO_SO";
			}
			else if (Type == MailType.Inquiry)
			{
				table = "CZ_PU_QTNH";
				field = "NO_FILE";
			}
			else if (Type == MailType.PurchaseOrder)
			{
				table = "PU_POH";
				field = "NO_PO";
			}			

			// 거래처 이메일
			query = @"
SELECT
	  A.NO_REF
	, A.CD_PARTNER
	, B.CD_AREA
	, IIF(A.SEQ_ATTN IS NULL, B.E_MAIL, C.NM_EMAIL)	AS E_MAIL
FROM	  " + table + @"	AS A
LEFT JOIN MA_PARTNER	AS B ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_PARTNER = B.CD_PARTNER
LEFT JOIN FI_PARTNERPTR	AS C ON A.CD_COMPANY = C.CD_COMPANY AND A.CD_PARTNER = C.CD_PARTNER AND A.SEQ_ATTN = C.SEQ
WHERE 1 = 1
	AND A.CD_COMPANY = '" + CD_COMPANY + @"'
	AND A." + field + " = '" + NO_FILE + "'";

			DataTable dtPartner = DBMgr.GetDataTable(query);
			To		   = GetTo.String(dtPartner.Rows[0]["E_MAIL"]);
			NO_REF	   = GetTo.String(dtPartner.Rows[0]["NO_REF"]);
			CD_PARTNER = GetTo.String(dtPartner.Rows[0]["CD_PARTNER"]);
			CD_AREA	   = GetTo.String(dtPartner.Rows[0]["CD_AREA"]);

			// ***** 본문 만들기
			// 서명에 회사이름
			if (CD_AREA == "100" && CD_COMPANY == "TEST") Company1 = "(주) 딘 텍";
			if (CD_AREA == "100" && CD_COMPANY == "K100") Company1 = "(주) 딘 텍";
			if (CD_AREA == "100" && CD_COMPANY == "K200") Company1 = "(주) 두 베 코";
			if (CD_AREA == "100" && CD_COMPANY == "S100") Company1 = "(주) 딘 텍";
			if (CD_AREA != "100" && CD_COMPANY == "TEST") Company1 = "DINTEC CO., LTD.";
			if (CD_AREA != "100" && CD_COMPANY == "K100") Company1 = "DINTEC CO., LTD.";
			if (CD_AREA != "100" && CD_COMPANY == "K200") Company1 = "Dubhe Co., Ltd.";
			if (CD_AREA != "100" && CD_COMPANY == "S100") Company1 = "DINTEC CO., LTD.";

			// 제목에 회사이름 및 서명에 홈페이지
			if (CD_COMPANY == "TEST") { Company2 = "DINTEC"; Homepage = "www.dintec.co.kr"; }
			if (CD_COMPANY == "K100") { Company2 = "DINTEC"; Homepage = "www.dintec.co.kr"; }
			if (CD_COMPANY == "K200") { Company2 = "DUBHECO"; Homepage = "www.dubheco.com"; }
			if (CD_COMPANY == "S100") { Company2 = "DINTEC"; Homepage = "www.dintec.co.kr"; }

			// 제목 및 서명 만들기
			if (Type == MailType.Quotation)
			{				
				// 메일 제목
				if (CD_COMPANY == "TEST") Title  = "DINTEC - QUOTATION(" + NO_FILE + ")" + "_" + CD_PARTNER;
				if (CD_COMPANY == "K100") Title  = "DINTEC - QUOTATION(" + NO_FILE + ")" + "_" + CD_PARTNER;
				if (CD_COMPANY == "K200") Title = "DUBHECO - QUOTATION(" + NO_FILE + ")" + "_" + CD_PARTNER;
				if (CD_COMPANY == "S100") Title  = "DINTEC - QUOTATION(" + NO_FILE + ")" + "_" + CD_PARTNER;

				// 서명
				Signature = SignQuotation;				
			}

			// ***** 첨부파일
			// 타입에 따라 단계 설정
			string step = "";

			if (Type == MailType.Quotation)
			{
				step = "TP_STEP = '05'";
			}
			else if (Type == MailType.Acknowledge)
			{
				step = "";
			}
			else if (Type == MailType.Inquiry)
			{
				step = "";
			}
			else if (Type == MailType.PurchaseOrder)
			{
				step = "";
			}

			// 쿼리
			query = @"
SELECT
	*
FROM
(
	SELECT
		  NM_FILE_REAL								AS NM_FILE
		, ROW_NUMBER() OVER (ORDER BY NO_LINE DESC)	AS RN
	FROM CZ_MA_WORKFLOWL
	WHERE 1 = 1
		AND CD_COMPANY = '" + CD_COMPANY + @"'
		AND NO_KEY = '" + NO_FILE + @"'
		AND " + step + @"
) AS A
WHERE RN = 1";

			DataTable dtFile = DBMgr.GetDataTable(query);
			string[] file = new string[dtFile.Rows.Count];

			for (int i = 0; i < dtFile.Rows.Count; i++) file[i] = dtFile.Rows[i]["NM_FILE"].ToString();

			this.Attachment = file;
		}

		#region ==================================================================================================== 서명

		public string SignQuotation
		{
			get
			{
				if (CD_AREA == "100")
				{
					return "";
				}
				else
				{
					//Dear 담당자 이름 (담당자 이름이 어려우면 Dear Sir/Madam으로 처리)
					return @"
Dear Sir & Madam,

Thank you for your inquiry.

Please find the attached quotation " + NO_FILE + " for your reference " + NO_REF + @"

For future inquiries and requests, please send your email to:" + From + @"

Best regards,

" + NM_EMP + @"
" + Company1 + @"
Tel     : +82-" + NO_TEL_EMP.Substring(1) + @"
Fax     : +82-" + NO_FAX_EMP.Substring(1) + @"
E-mail : " + From + @"
Homepage : " + Homepage;
				}
			}
		}

		public string SignInquiry
		{
			get
			{
				if (CD_AREA == "100")
				{
					return @"
- 귀사의 일익 번창하심을 기원합니다.

- 유첨건 관련, 견적 요청 하오니 검토하시어 빠른 회신 부탁 드립니다.



(주) 딘 텍
Tel     : +82-51-664-1000 (REP)
Fax     : +82-51-462-7907~9
E-mail : service@dintec.co.kr
Homepage : www.dintec.co.kr";
				}
				else
				{
					return @"
Dear Sir & Madam,

Thanks for your cooperation.
Please let us receive your quotation soon.


DINTEC CO., LTD.
Tel     : +82-51-664-1000 (REP)
Fax     : +82-51-462-7907~9
E-mail : service@dintec.co.kr
Homepage : www.dintec.co.kr";
				}
			}
		}		

		public string SIGN_INQUIRY2
		{
			get
			{
				string NM_COMPANY = "";
				string HOMEPAGE = "";

				//if (CD_COMPANY == "TEST") { NM_COMPANY = "딘 텍"; HOMEPAGE = "www.dintec.co.kr"; }
				//if (CD_COMPANY == "K100") { NM_COMPANY = "딘 텍"; HOMEPAGE = "www.dintec.co.kr"; }
				//if (CD_COMPANY == "K200") { NM_COMPANY = "두 베 코"; HOMEPAGE = "www.dubheco.com"; }
				//if (CD_COMPANY == "S100") { NM_COMPANY = "딘 텍"; HOMEPAGE = "www.dintec.co.kr"; }

				return @"
- 귀사의 일익 번창하심을 기원합니다.

- 유첨건 관련, 견적 요청 하오니 검토하시어 빠른 회신 부탁 드립니다.


" + NM_EMP + @"
(주) " + NM_COMPANY + @"
Tel     : " + NO_TEL_EMP + @"
Fax     : " + NO_FAX_EMP + @"
E-mail : " + From + @"
Homepage : " + HOMEPAGE;
			}
		}

		public static string GetInquiryEN(string NM_EMP, string NO_TEL_EMP, string NO_FAX_EMP, string NO_EMAIL_EMP)
		{
			string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			string NM_COMPANY = "";
			string HOMEPAGE = "";

			//if (CD_COMPANY == "TEST") { NM_COMPANY = "DINTEC CO., LTD."; HOMEPAGE = "www.dintec.co.kr"; }
			//if (CD_COMPANY == "K100") { NM_COMPANY = "DINTEC CO., LTD."; HOMEPAGE = "www.dintec.co.kr"; }
			//if (CD_COMPANY == "K200") { NM_COMPANY = "Dubhe Co., Ltd."; HOMEPAGE = "www.dubheco.com"; }
			//if (CD_COMPANY == "S100") { NM_COMPANY = "DINTEC CO., LTD."; HOMEPAGE = "www.dintec.co.kr"; }

			return @"
Dear Sir & Madam,

Thanks for your cooperation.
Please let us receive your quotation soon.

" + NM_EMP + @"
" + NM_COMPANY + @"
Tel     : +82-" + NO_TEL_EMP.Substring(1) + @"
Fax     : +82-" + NO_FAX_EMP.Substring(1) + @"
E-mail : " + NO_EMAIL_EMP + @"
Homepage : " + HOMEPAGE;
		}

		

		public static string GetPOrderKR(string NM_EMP, string NO_TEL_EMP, string NO_FAX_EMP, string NO_EMAIL_EMP)
		{
			string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			string NM_COMPANY = "";
			string HOMEPAGE = "";

			if (CD_COMPANY == "TEST") { NM_COMPANY = "딘 텍"; HOMEPAGE = "www.dintec.co.kr"; }
			if (CD_COMPANY == "K100") { NM_COMPANY = "딘 텍"; HOMEPAGE = "www.dintec.co.kr"; }
			if (CD_COMPANY == "K200") { NM_COMPANY = "두 베 코"; HOMEPAGE = "www.dubheco.com"; }
			if (CD_COMPANY == "S100") { NM_COMPANY = "딘 텍"; HOMEPAGE = "www.dintec.co.kr"; }

			return @"
- 귀사의 일익 번창하심을 기원합니다.

- 유첨건 관련, 발주 요청 하오니 검토하시어 빠른 진행 부탁 드립니다.


" + NM_EMP + @"
(주) " + NM_COMPANY + @"
Tel     : " + NO_TEL_EMP + @"
Fax     : " + NO_FAX_EMP + @"
E-mail : " + NO_EMAIL_EMP + @"
Homepage : " + HOMEPAGE;
		}

		public static string GetPOrderEN(string NM_EMP, string NO_TEL_EMP, string NO_FAX_EMP, string NO_EMAIL_EMP)
		{
			string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			string NM_COMPANY = "";
			string HOMEPAGE = "";

			if (CD_COMPANY == "TEST") { NM_COMPANY = "DINTEC CO., LTD."; HOMEPAGE = "www.dintec.co.kr"; }
			if (CD_COMPANY == "K100") { NM_COMPANY = "DINTEC CO., LTD."; HOMEPAGE = "www.dintec.co.kr"; }
			if (CD_COMPANY == "K200") { NM_COMPANY = "Dubhe Co., Ltd."; HOMEPAGE = "www.dubheco.com"; }
			if (CD_COMPANY == "S100") { NM_COMPANY = "DINTEC CO., LTD."; HOMEPAGE = "www.dintec.co.kr"; }

			return @"
Dear Sir & Madam,

Thanks for your cooperation.
Please find the order sheet attached and notify us if the items are ready for dispatch.

" + NM_EMP + @"
" + NM_COMPANY + @"
Tel     : +82-" + NO_TEL_EMP.Substring(1) + @"
Fax     : +82-" + NO_FAX_EMP.Substring(1) + @"
E-mail : " + NO_EMAIL_EMP + @"
Homepage : " + HOMEPAGE;
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

using Duzon.Common.Forms;

// 20190917
namespace Dintec
{
	public class MailSign
	{
		public static string GetInquiryKR(string url)
		{
			string s = @"
- 귀사의 일익 번창하심을 기원합니다.

- 유첨건 관련, 견적 요청 하오니 검토하시어 빠른 회신 부탁 드립니다.


" + url + @"



(주) 딘 텍
Tel     : +82-51-664-1000 (REP)
Fax     : +82-51-462-7907/8
E-mail : service@dintec.co.kr
Homepage : www.dintec.co.kr";

			return s;
		}

		public static string GetInquiryKR(string NM_EMP, string NO_TEL_EMP, string NO_FAX_EMP, string NO_EMAIL_EMP, string url)
		{
			return GetInquiryKR(NM_EMP, NO_TEL_EMP, NO_EMAIL_EMP, url);
		}

		public static string GetInquiryKR(string NM_EMP, string NO_TEL_EMP, string NO_EMAIL_EMP, string url)
		{
			string a = "";
			string companyCode = Global.MainFrame.LoginInfo.CompanyCode;
			string NM_COMPANY = "";
			string HOMEPAGE = "";

			if (companyCode == "TEST") { NM_COMPANY = "딘 텍"; HOMEPAGE = "www.dintec.co.kr"; }
			if (companyCode == "K100") { NM_COMPANY = "딘 텍"; HOMEPAGE = "www.dintec.co.kr"; }
			if (companyCode == "K200") { NM_COMPANY = "두 베 코"; HOMEPAGE = "www.dubheco.com"; }
			if (companyCode == "S100") { NM_COMPANY = "딘 텍"; HOMEPAGE = "www.dintec.co.kr"; }

			string s = @"
- 귀사의 일익 번창하심을 기원합니다.

- 유첨건 관련, 견적 요청 하오니 검토하시어 빠른 회신 부탁 드립니다.
		

" + url + @"


 
" + NM_EMP + @"
(주) " + NM_COMPANY + @"
Tel     : " + NO_TEL_EMP + @"
E-mail : " + NO_EMAIL_EMP + @"
Homepage : " + HOMEPAGE;

			return s;
		}

		public static string GetInquiryEN(string NM_EMP, string NO_TEL_EMP, string NO_FAX_EMP, string NO_EMAIL_EMP, string url)
		{
			return GetInquiryEN(NM_EMP, NO_TEL_EMP, NO_EMAIL_EMP, url);
		}

		public static string GetInquiryEN(string NM_EMP, string NO_TEL_EMP, string NO_EMAIL_EMP, string url)
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
Please let us receive your quotation soon.

" + NM_EMP + @"
" + NM_COMPANY + @"
Tel     : +82-" + NO_TEL_EMP.Substring(1) + @"
E-mail : " + NO_EMAIL_EMP + @"
Homepage : " + HOMEPAGE;
		}

		public static string GetInquiryEN(string url)
		{
			return @"
Dear Sir & Madam,

Thanks for your cooperation.
Please let us receive your quotation soon.


DINTEC CO., LTD.
Tel     : +82-51-664-1000 (REP)
Fax     : +82-51-462-7907/8
E-mail : service@dintec.co.kr
Homepage : www.dintec.co.kr";
		}

		public static string GetPOrderKR(string NM_EMP, string NO_TEL_EMP, string NO_FAX_EMP, string NO_EMAIL_EMP, string 파일번호)
		{
			string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			string NM_COMPANY = "";
			string HOMEPAGE = "";

			if (CD_COMPANY == "TEST") { NM_COMPANY = "딘 텍"; HOMEPAGE = "www.dintec.co.kr"; }
			if (CD_COMPANY == "K100") { NM_COMPANY = "딘 텍"; HOMEPAGE = "www.dintec.co.kr"; }
			if (CD_COMPANY == "K200") { NM_COMPANY = "두 베 코"; HOMEPAGE = "www.dubheco.com"; }
			if (CD_COMPANY == "S100") { NM_COMPANY = "딘 텍"; HOMEPAGE = "www.dintec.co.kr"; }

			string 비고 = 파일번호.Left(2).In("SB", "NS") ? "" : "- <b>납품 시 필수 협조 사항 : 첨부 발주서 및 거래명세서 동봉하여 포장 BOX 안에 넣어주십시오.</b>" ;

			return @"
- 귀사의 일익 번창하심을 기원합니다.

- 유첨건 관련, 발주 요청 하오니 검토하시어 빠른 진행 부탁 드립니다.

" + 비고 + @"

" + NM_EMP + @"
(주) " + NM_COMPANY + @"
Tel     : " + NO_TEL_EMP + @"
E-mail : " + NO_EMAIL_EMP + @"
Homepage : " + HOMEPAGE;
		}

		public static string GetPOrderEN(string NM_EMP, string NO_TEL_EMP, string NO_FAX_EMP, string NO_EMAIL_EMP, string url)
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
E-mail : " + NO_EMAIL_EMP + @"
Homepage : " + HOMEPAGE;
		}
	}
}

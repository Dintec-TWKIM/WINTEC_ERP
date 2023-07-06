using System.Data;
using Duzon.Common.Forms;

namespace Dintec
{
	public class MailAddrTo
	{
		public string MAIL { get; set; }

		public MailAddrTo(string NO_FILE, MailToType type)
		{
			string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;

			// 거래처 정보 조회
			string query = "";

			if (type == MailToType.Quotation)
			{
				query = @"
SELECT
	IIF(A.SEQ_ATTN IS NULL, B.E_MAIL, C.NM_EMAIL)	AS E_MAIL
FROM	  CZ_SA_QTNH	AS A
LEFT JOIN MA_PARTNER	AS B ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_PARTNER = B.CD_PARTNER
LEFT JOIN FI_PARTNERPTR	AS C ON A.CD_COMPANY = C.CD_COMPANY AND A.CD_PARTNER = C.CD_PARTNER AND A.SEQ_ATTN = C.SEQ
WHERE 1 = 1
	AND A.CD_COMPANY = '" + CD_COMPANY + @"'
	AND A.NO_FILE = '" + NO_FILE + "'";
			}

			DataTable dt = DBMgr.GetDataTable(query);
			this.MAIL = dt.Rows[0]["E_MAIL"].ToString();
		}
	}

	public enum MailToType
	{
		  Quotation
		, PurchaseOrder
		, Inquiry
	}
}

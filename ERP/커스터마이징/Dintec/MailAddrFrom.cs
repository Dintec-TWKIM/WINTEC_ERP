using System.Data;
using Duzon.Common.Forms;

namespace Dintec
{
	public class MailAddrFrom
	{
		public string MAIL { get; set; }
		public string NO_EMP { get; set; }
		public string NM_EMP { get; set; }
		public string EN_EMP { get; set; }
		public string NO_TEL_EMP { get; set; }
		public string NO_FAX_EMP { get; set; }

		public MailAddrFrom(string NO_EMP)
		{
			string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;

			// 사원정보 조회
			string query = @"
SELECT
	*
FROM V_CZ_MA_EMP
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND NO_EMP = '" + NO_EMP + "'";

			DataTable dt = DBMgr.GetDataTable(query);
			this.MAIL = dt.Rows[0]["NO_EMAIL_EMP"].ToString();
			this.NM_EMP = dt.Rows[0]["NM_EMP"].ToString();
			this.EN_EMP = dt.Rows[0]["EN_EMP"].ToString();
			this.NO_TEL_EMP = dt.Rows[0]["NO_TEL_EMP"].ToString();
			this.NO_FAX_EMP = dt.Rows[0]["NO_FAX_EMP"].ToString();			
		}
	}
}

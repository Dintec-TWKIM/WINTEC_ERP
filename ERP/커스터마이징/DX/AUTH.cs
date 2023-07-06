using Dintec;
using Duzon.Common.Forms;
using System.Data;

namespace DX
{
	public class AUTH
	{
		public static bool IsAdmin()
		{
			return IsAdmin(Global.MainFrame.LoginInfo.UserID);
		}

		public static bool IsAdmin(string empNumber)
		{
			string query = @"
SELECT
	1
FROM V_CZ_MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND CD_FIELD = 'CZ_MA00030'
	AND CD_SYSDEF = '" + empNumber + @"'
	AND CD_FLAG1 = 'Y'";

			DataTable dt = SQL.GetDataTable(query);
			return dt.Rows.Count == 1;
		}
	}
}
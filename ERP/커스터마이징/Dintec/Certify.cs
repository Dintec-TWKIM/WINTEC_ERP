using Duzon.Common.Forms;
using System.Data;

namespace Dintec
{
	public class Certify
	{
		public static bool IsLive()
		{

			return true;

			//if (ClientRepository.DatabaseCallType == "Direct")
			//	return false;
			//else
			//	return true;
		}

		public static bool IsAdminEquip()
		{
			return IsAdminEquip(Global.MainFrame.LoginInfo.EmployeeNo);
		}

		public static bool IsAdminEquip(string empNumber)
		{
			string query = @"
SELECT
	1
FROM V_CZ_MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND CD_FIELD = 'CZ_MA00030'
	AND CD_SYSDEF = '" + empNumber + @"'
	AND CD_FLAG3 = 'Y'";

			DataTable dt = DBMgr.GetDataTable(query);
			return dt.Rows.Count == 1 ? true : false;
		}
	}
}

using System.Data;
using Duzon.Common.Forms;
using Dintec;

namespace DX
{
	public class SHORTNER
	{
		public static string 상업송장(string data)
		{
			SQL sql = new SQL("PX_CZ_DX_URL_BASE62", SQLType.Procedure, SQLDebug.None);
			sql.Parameter.Add2("@REDIRECT"	, "");
			sql.Parameter.Add2("@DATA"		, data);
			sql.Parameter.Add2("@TYPE"		, "상업송장");
			sql.Parameter.Add2("@ID_USER"	, Global.MainFrame.LoginInfo.UserID);
			DataTable dt = sql.GetDataTable();

			return "dint.ec/" + dt.Rows[0]["ENCODED"];
		}

		public static string 포장명세서(string data)
		{
			SQL sql = new SQL("PX_CZ_DX_URL_BASE62", SQLType.Procedure, SQLDebug.None);
			sql.Parameter.Add2("@REDIRECT"	, "");
			sql.Parameter.Add2("@DATA"		, data);
			sql.Parameter.Add2("@TYPE"		, "포장명세서");
			sql.Parameter.Add2("@ID_USER"	, Global.MainFrame.LoginInfo.UserID);
			DataTable dt = sql.GetDataTable();

			return "dint.ec/" + dt.Rows[0]["ENCODED"];
		}
		public static string 인수증(string data)
		{
			SQL sql = new SQL("PX_CZ_DX_URL_BASE62", SQLType.Procedure, SQLDebug.None);
			sql.Parameter.Add2("@REDIRECT"	, "http://dint.ec/log/receipt/upload");
			sql.Parameter.Add2("@DATA"		, data);
			sql.Parameter.Add2("@TYPE"		, "인수증");
			sql.Parameter.Add2("@ID_USER"	, Global.MainFrame.LoginInfo.UserID);
			DataTable dt = sql.GetDataTable();

			return "dint.ec/" + dt.Rows[0]["ENCODED"];
		}
	}
}

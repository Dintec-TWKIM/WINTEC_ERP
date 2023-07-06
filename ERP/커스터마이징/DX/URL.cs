using System.Data;
using Dintec;
using Duzon.Common.Forms;

namespace DX
{
	public class URL
	{
		public static string GetShortner(string pathCode, string data)
		{
			SQL sql = new SQL("PX_CZ_DX_URL_BASE62", SQLType.Procedure, SQLDebug.None);
			sql.Parameter.Add2("@URL"	 , "http://dintec.kr/" + pathCode);
			sql.Parameter.Add2("@DATA"	 , data);
			sql.Parameter.Add2("@ACTION" , "LO");
			sql.Parameter.Add2("@ID_USER", Global.MainFrame.LoginInfo.UserID);
			DataTable dt = sql.GetDataTable();

			return "http://dintec.kr/" + dt.Rows[0]["ENCODED"];
		}
	}
}

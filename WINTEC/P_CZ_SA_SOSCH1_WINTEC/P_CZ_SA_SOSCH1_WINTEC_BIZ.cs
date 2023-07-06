using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Data;

namespace cz
{
    internal class P_CZ_SA_SOSCH1_WINTEC_BIZ
	{
		private string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
		private string 로그인언어 = Global.SystemLanguage.MultiLanguageLpoint;


		public DataTable Search(string 날짜구분, object[] obj)
		{
			DataTable dt;

			if (날짜구분 == "SO")
			{
				dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH1_WINTEC_SO", obj);
			}
			else if (날짜구분 == "DU")
			{
				dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH1_WINTEC_DU", obj);
			}
			else if (날짜구분 == "GI")
			{
				dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH1_WINTEC_GI", obj);
			}
            else
            {
				dt = DBHelper.GetDataTable("SP_CZ_SA_SOSCH1_WINTEC_IP", obj);
            }
            return dt;
		}

		public DataTable SearchSerial(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_SO_MNG_SERIAL_WINTEC_S", obj);
			return dt;
		}

		public DataSet SearchTrust(object[] obj)
		{
			DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_SO_MNG_TRUST_WINTEC_S", obj);
			return ds;
		}

		internal bool IsConfirm(string NO_SO)
		{
			string empty = string.Empty;
			DataTable dataTable = DBHelper.GetDataTable(@"SELECT STA_SO  
														  FROM SA_SOL WITH(NOLOCK)  
														  WHERE CD_COMPANY = '" + MA.Login.회사코드 + "' " +
														 "AND NO_SO = '" + NO_SO + "' " +
														 "AND STA_SO <> 'O'");

			return dataTable != null && dataTable.Rows.Count != 0;
		}

    }
}
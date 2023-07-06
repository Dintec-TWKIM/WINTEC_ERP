using Duzon.Common.Forms;
using Duzon.ERPU;
using System.Data;

namespace cz
{
	internal class P_CZ_PR_WORK_SUB2_BIZ
	{
        public DataSet Get_Plant_Cfg(object[] obj) => DBHelper.GetDataSet("UP_PR_CFG_PLANT_SELECT", obj);

        public DataTable GetComboData(object[] obj)
        {
            string empty = string.Empty;
            string str;
            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
                str = "SELECT  RTRIM(CD_SYSDEF) AS CODE, NM_SYSDEF +'('+ RTRIM(CD_SYSDEF) +')' AS NAME, FG1_SYSCODE AS SYSCODE    FROM\tDZSN_MA_CODEDTL                         WHERE   CD_COMPANY = '" + obj[0] + "'                AND\t\tCD_FIELD = '" + obj[1] + "'        AND\t\tISNULL(USE_YN, '') = 'Y'       AND     (ISNULL('" + obj[2] + "', '') = '' OR CD_FLAG1 = '" + obj[2] + "')    ORDER BY CODE ASC  ";
            else
                str = "SELECT  RTRIM(CD_SYSDEF) AS CODE, NM_SYSDEF ||'('|| RTRIM(CD_SYSDEF) ||')' AS NAME, FG1_SYSCODE AS SYSCODE    FROM    DZSN_MA_CODEDTL                         WHERE   CD_COMPANY = '" + obj[0] + "'                AND\t\tCD_FIELD = '" + obj[1] + "'        AND\t\tNVL(USE_YN, ' ') = 'Y'       AND     (NVL('" + obj[2] + "', ' ') = ' ' OR CD_FLAG1 = '" + obj[2] + "')    ORDER BY CODE ASC  ";
            return DBHelper.GetDataTable(str);
        }

        public DataTable Get_Bad_Cd_SL_Search(object[] obj) => DBHelper.GetDataTable("UP_PR_WCOP_DTL_S2", obj);

        public DataTable SearchCode(string CD_FIELD, string ColumnName) => DBHelper.GetDataTable("SELECT CD_SYSDEF AS " + ColumnName + ", NM_SYSDEF FROM   DZSN_MA_CODEDTL WHERE  CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND    CD_FIELD   = '" + CD_FIELD + "'");

        public DataTable SearchCD_SL(string CD_PLANT) => DBHelper.GetDataTable("SELECT CD_SL AS CD_SL_BAD, NM_SL FROM   DZSN_MA_SL WHERE  CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND    CD_PLANT   = '" + CD_PLANT + "'");
    }
}
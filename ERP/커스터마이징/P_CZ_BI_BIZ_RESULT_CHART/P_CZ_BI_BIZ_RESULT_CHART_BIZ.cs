using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace cz
{
    internal class P_CZ_BI_BIZ_RESULT_CHART_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_BI_BIZ_RESULT_CHART_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataSet Search1(object[] args)
        {
            return DBHelper.GetDataSet("UP_FI_PLQ_2011_S", args);
        }

        internal bool Save(DataTable dt, string 회사코드)
        {
            if (dt == null || dt.Rows.Count == 0) return false;

            SpInfo si = new SpInfo();

            si.DataValue = dt;
            si.CompanyID = 회사코드;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameInsert = "SP_CZ_BI_BIZ_RESULT_CHART_I";
            si.SpParamsInsert = new string[] { "CD_COMPANY",
											   "DT_ACCT",
                                               "AM_ACCT",
											   "ID_INSERT" };

            return DBHelper.Save(si);
        }

        internal bool 회계환경설정_결산평가손익환급(string 회사코드)
        {
            DataTable dataTable = DBHelper.GetDataTable("SELECT CD_ENV FROM MA_ENVD WHERE CD_COMPANY = '" + 회사코드 + "' AND TP_ENV = 'YN_CLOSEIN2007_IV'");
            return dataTable != null && dataTable.Rows.Count == 1 && D.GetString(dataTable.Rows[0]["CD_ENV"]) == "Y";
        }
    }
}

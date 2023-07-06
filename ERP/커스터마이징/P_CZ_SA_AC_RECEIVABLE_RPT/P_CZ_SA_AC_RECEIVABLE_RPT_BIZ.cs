using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_SA_AC_RECEIVABLE_RPT_BIZ
    {
        internal DataSet Search(object[] obj)
        {
            DataSet dataSet = DBHelper.GetDataSet("SP_CZ_SA_AC_RECEIVABLE_RPT_S", obj);

            T.SetDefaultValue(dataSet);
            return dataSet;
        }

        internal DataSet Search1(object[] obj)
        {
            DataSet dataSet = DBHelper.GetDataSet("SP_CZ_SA_AC_RECEIVABLE_RPT_S1", obj);

            T.SetDefaultValue(dataSet);
            return dataSet;
        }

        internal DataSet Search2(object[] obj)
        {
            DataSet dataSet = DBHelper.GetDataSet("SP_CZ_SA_AC_RECEIVABLE_RPT_S2", obj);

            T.SetDefaultValue(dataSet);
            return dataSet;
        }

        internal DataSet Search3(object[] obj)
        {
            DataSet dataSet = DBHelper.GetDataSet("SP_CZ_SA_AC_RECEIVABLE_RPT_S3", obj);

            T.SetDefaultValue(dataSet);
            return dataSet;
        }

        internal DataSet Search4(object[] obj)
        {
            DataSet dataSet = DBHelper.GetDataSet("SP_CZ_SA_AC_RECEIVABLE_RPT_S4", obj);

            T.SetDefaultValue(dataSet);
            return dataSet;
        }

        internal DataSet Search5(object[] obj)
        {
            DataSet dataSet = DBHelper.GetDataSet("SP_CZ_SA_AC_RECEIVABLE_RPT_S5", obj);

            T.SetDefaultValue(dataSet);
            return dataSet;
        }

        public bool SaveData(DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_SA_AC_RECEIVABLE_RPT_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "NO_KEY",
                                               "NO_EMP",
                                               "DC_RMK",
                                               "ID_UPDATE" };

            return DBHelper.Save(si);
        }
    }
}

using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.Data;

namespace cz
{
	internal class P_CZ_PR_DEPT_EQUIP_MNG_BIZ
	{
        public DataTable Search1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_DEPT_EQUIP_MNG_S1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search2(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_DEPT_EQUIP_MNG_S2", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search3(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_DEPT_EQUIP_MNG_S3", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public bool Save(DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameUpdate = "SP_CZ_PR_DEPT_EQUIP_MNG_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "NO_WO",
                                               "NO_LINE",
                                               "CD_USERDEF1",
                                               "ID_UPDATE" };

            return DBHelper.Save(si);
        }
    }
}

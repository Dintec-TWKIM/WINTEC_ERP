using Duzon.Common.Forms;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Util;
namespace cz
{
    internal class P_CZ_HR_PEVALU_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_PEVALU_S1", obj);
        }

        internal DataTable SearchDetail(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_PEVALU_S2", obj);
        }

        internal DataTable Search1(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_EVALU_DLG", obj);
        }

        internal DataTable Search2(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_EVALU_DLG1", obj);
        }

        internal bool Save(DataTable dtH, DataTable dtL)
        {
            SpInfoCollection spInfoColletion = new SpInfoCollection();

            if (dtH != null && dtH.Rows.Count > 0)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtH;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;

                spInfo.SpNameUpdate = "SP_CZ_HR_PEVALUH_U";
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "CD_EVALU",
                                                       "CD_EVTYPE",
                                                       "CD_EVNUMBER",
                                                       "NO_EMPM",
                                                       "NO_EMPAN",
                                                       "COMMENT1",
                                                       "ID_INSERT" };

                spInfoColletion.Add(spInfo);
            }

            if (dtL != null && dtL.Rows.Count > 0)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtL;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;

                spInfo.SpNameUpdate = "SP_CZ_HR_PEVALUL_U";
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "CD_EVALU",
                                                       "CD_EVTYPE",
                                                       "CD_EVITEM",
                                                       "CD_EVNUMBER",
                                                       "CD_GROUP",
                                                       "NO_EMPM",
                                                       "NO_EMPAN",
                                                       "LB_EVITEM",
                                                       "NUM_EWEIGHT",
                                                       "PT_SCORE",
                                                       "CD_GRADE",
                                                       "PT_HSCORE",
                                                       "DC_COMMENT1",
                                                       "ID_INSERT" };

                spInfoColletion.Add(spInfo);
            }

            return DBHelper.Save(spInfoColletion);
        }
    }
}
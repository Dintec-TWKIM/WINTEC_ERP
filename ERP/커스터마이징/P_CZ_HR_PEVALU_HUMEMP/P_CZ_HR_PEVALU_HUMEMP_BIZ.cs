using Duzon.Common.Forms;
using Duzon.ERPU;
using System.Data;
using Duzon.Common.Util;

namespace cz
{
    internal class P_CZ_HR_PEVALU_HUMEMP_BIZ
    {
        internal DataTable Search평가자(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_PEVALU_HUMEMP_S1", obj);
        }

        internal DataTable Search피평가자(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_PEVALU_HUMEMP_S2", obj);
        }

        internal DataTable SearchSub()
        {
            return DBHelper.GetDataTable("SP_CZ_HR_PEVALU_HUMEMP_SUB");
        }

        internal DataTable SearchTab(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_HR_PEVALU_HUMEMP_TAB1", obj);
        }

        internal bool Save(DataTable dt평가자, DataTable dt피평가자)
        {
            SpInfoCollection spInfoCollection = new SpInfoCollection();

            if (dt평가자 != null && dt평가자.Rows.Count > 0)
            {
                SpInfo spInfo = new SpInfo();

                spInfo.DataValue = dt평가자;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                
                spInfo.SpNameInsert = "SP_CZ_HR_PEVALU_HUMEMPM_I";
                spInfo.SpNameUpdate = "SP_CZ_HR_PEVALU_HUMEMPM_U";
                spInfo.SpNameDelete = "SP_CZ_HR_PEVALU_HUMEMPM_D";

                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "CD_EVALU",
                                                       "CD_EVTYPE",
                                                       "CD_EVNUMBER",
                                                       "NO_EMPM",
                                                       "CD_BIZAREA",
                                                       "CD_DEPT",
                                                       "CD_EMP",
                                                       "TP_EMP",
                                                       "CD_DUTY_RANK",
                                                       "CD_DUTY_STEP",
                                                       "CD_DUTY_RESP",
                                                       "CD_DUTY_TYPE",
                                                       "CD_PAY_STEP",
                                                       "CD_DUTY_WORK",
                                                       "CD_JOB_SERIES",
                                                       "CD_CC",
                                                       "CD_PJT",
                                                       "NUM_EWEIGHT",
                                                       "ID_INSERT",
                                                       "CD_GROUP" };
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "CD_EVALU",
                                                       "CD_EVTYPE",
                                                       "CD_EVNUMBER",
                                                       "CD_GROUP",
                                                       "NO_EMPM",
                                                       "NUM_EWEIGHT",
                                                       "ID_UPDATE" };
                spInfo.SpParamsDelete = new string[] { "CD_EVALU",
                                                       "CD_EVTYPE",
                                                       "CD_EVNUMBER",
                                                       "CD_GROUP",
                                                       "NO_EMPM" };

                spInfoCollection.Add(spInfo);
            }

            if (dt피평가자 != null && dt피평가자.Rows.Count > 0)
            {
                SpInfo spInfo = new SpInfo();

                spInfo.DataValue = dt피평가자;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                
                spInfo.SpNameInsert = "SP_CZ_HR_PEVALU_HUMEMPAN_I";
                spInfo.SpNameDelete = "SP_CZ_HR_PEVALU_HUMEMPAN_D";

                spInfo.SpParamsInsert = new string[]{ "CD_COMPANY",
                                                      "CD_EVALU",
                                                      "CD_EVTYPE",
                                                      "CD_EVNUMBER",
                                                      "CD_GROUP",
                                                      "NO_EMPM",
                                                      "NO_EMPAN",
                                                      "CD_BIZAREA",
                                                      "CD_DEPT",
                                                      "CD_EMP",
                                                      "TP_EMP",
                                                      "CD_DUTY_RANK",
                                                      "CD_DUTY_STEP",
                                                      "CD_DUTY_RESP",
                                                      "CD_DUTY_TYPE",
                                                      "CD_PAY_STEP",
                                                      "CD_DUTY_WORK",
                                                      "CD_JOB_SERIES",
                                                      "CD_CC",
                                                      "CD_PJT",
                                                      "ID_INSERT" };
                spInfo.SpParamsDelete = new string[] { "CD_EVALU",
                                                       "CD_EVTYPE",
                                                       "CD_EVNUMBER",
                                                       "CD_GROUP",
                                                       "NO_EMPM",
                                                       "NO_EMPAN" };

                spInfoCollection.Add(spInfo);
            }

            return DBHelper.Save(spInfoCollection);
        }
    }
}

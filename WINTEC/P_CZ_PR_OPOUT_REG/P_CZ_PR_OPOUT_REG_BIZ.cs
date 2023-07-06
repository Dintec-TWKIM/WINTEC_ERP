using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.Data;

namespace cz
{
    internal class P_CZ_PR_OPOUT_REG_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_OPOUT_REG_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_OPOUT_REG_ID_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public bool Save(DataTable dtA, DataTable dtM, DataTable dtD, DataTable dtIDD)
        {
            SpInfoCollection spCollection = new SpInfoCollection();

            if (dtA != null)
            {
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtA,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_CZ_PR_OPOUT_REG_INSERT",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_WO",
                                                    "DT_PR",
                                                    "CD_OP",
                                                    "CD_WC",
                                                    "CD_WCOP",
                                                    "QT_PR",
                                                    "CD_ITEM",
                                                    "ID_INSERT",
                                                    "DC_RMK" }
                });
            }

            if (dtM != null)
            {
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtM,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameUpdate = "UP_CZ_PR_OPOUT_REG_UPDATE",
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_WO",
                                                    "CD_OP",
                                                    "QT_PR",
                                                    "ID_UPDATE",
                                                    "DC_RMK" }
                });
            }

            if (dtD != null)
            {
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtD,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameDelete = "UP_CZ_PR_OPOUT_REG_DELETE",
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_WO",
                                                    "CD_OP" }
                });
            }

            if (dtIDD != null)
            {
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtIDD,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameDelete = "UP_CZ_PR_OPOUT_REG_ID_DELETE",
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "NO_WO",
                                                    "NO_WO_LINE",
                                                    "SEQ_WO" }
                });
            }
            return DBHelper.Save(spCollection);
        }
    }
}
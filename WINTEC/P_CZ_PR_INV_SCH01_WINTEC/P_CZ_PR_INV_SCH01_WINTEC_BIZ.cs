using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Data;

namespace cz
{
    internal class P_CZ_PR_INV_SCH01_WINTEC_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_CZ_PR_INV_L_SCH_SELECT", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search2(object[] obj)
        {
            DataTable dataValue = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_CZ_PU_INV_SCH2_SELECT",
                SpParamsSelect = obj
            })).DataValue;
            foreach (DataColumn column in dataValue.Columns)
            {
                if (column.DataType == typeof(decimal))
                    column.DefaultValue = 0;
                if (column.DataType == typeof(string))
                    column.DefaultValue = "";
            }
            return dataValue;
        }

        public bool Save(DataTable change1, DataTable change2, DataTable change3, DataTable change4, DataTable change5, DataTable change6)
        {
            SpInfoCollection spCollection = new SpInfoCollection();
            if (change1 != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = change1,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameUpdate = "UP_CZ_PR_INV_SCH_ROUT_L",
                    SpParamsUpdate = new string[]
                    {
                        "CD_COMPANY",
                        "CD_PLANT",
                        "CD_ITEM",
                        "CD_OP",
                        "DC_RMK"
                    }
                });
            if (change2 != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = change2,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameUpdate = "UP_CZ_PR_INV_SCH_ROUT_L",
                    SpParamsUpdate = new string[]
                    {
                        "CD_COMPANY",
                        "CD_PLANT",
                        "CD_ITEM",
                        "CD_OP",
                        "DC_RMK"
                    }
                });
            if (change3 != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = change3,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameUpdate = "UP_CZ_PR_INV_SCH_ROUT_L",
                    SpParamsUpdate = new string[]
                    {
                        "CD_COMPANY",
                        "CD_PLANT",
                        "CD_ITEM",
                        "CD_OP",
                        "DC_RMK"
                    }
                });
            if (change4 != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = change4,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameUpdate = "UP_CZ_PR_INV_SCH_ROUT_L",
                    SpParamsUpdate = new string[]
                    {
                        "CD_COMPANY",
                        "CD_PLANT",
                        "CD_ITEM",
                        "CD_OP",
                        "DC_RMK"
                    }
                });
            if (change5 != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = change5,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameUpdate = "UP_CZ_PR_INV_SCH_ROUT_L",
                    SpParamsUpdate = new string[]
                    {
                        "CD_COMPANY",
                        "CD_PLANT",
                        "CD_ITEM",
                        "CD_OP",
                        "DC_RMK"
                    }
                });
            if (change6 != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = change6,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameUpdate = "UP_CZ_PR_INV_SCH_ROUT_L",
                    SpParamsUpdate = new string[]
                    {
                        "CD_COMPANY",
                        "CD_PLANT",
                        "CD_ITEM",
                        "CD_OP",
                        "DC_RMK"
                    }
                });

            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spCollection))
            {
                if (!resultData.Result)
                    return false;
            }

            return true;
        }
    }
}
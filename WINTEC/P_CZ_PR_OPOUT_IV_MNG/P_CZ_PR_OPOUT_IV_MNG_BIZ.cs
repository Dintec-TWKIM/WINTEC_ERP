using Duzon.Common.Forms;
using Duzon.Common.Util;
using System;
using System.Data;

namespace cz
{
	internal class P_CZ_PR_OPOUT_IV_MNG_BIZ
	{
        public DataTable Search(object[] obj)
        {
            DataTable dataValue = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_PR_OPOUT_IV_MNG_H_SELECT",
                SpParamsSelect = obj
            })).DataValue;
            foreach (DataColumn column in dataValue.Columns)
            {
                if (column.DataType == typeof(decimal))
                    column.DefaultValue = 0;
            }
            return dataValue;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dataValue = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_PR_OPOUT_IV_MNG_L_SELECT",
                SpParamsSelect = obj
            })).DataValue;
            foreach (DataColumn column in dataValue.Columns)
            {
                if (column.DataType == typeof(decimal))
                    column.DefaultValue = 0;
            }
            return dataValue;
        }

        public bool Save(DataTable dtH, DataTable dtL)
        {
            SpInfoCollection spCollection = new SpInfoCollection();
            if (dtH != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtH,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameDelete = "UP_PR_OPOUT_IVH_DELETE",
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_IV" }
                });
            if (dtL != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtL,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameDelete = "UP_PR_OPOUT_IVL_DELETE",
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_IV",
                                                    "NO_LINE" }
                });
            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spCollection))
            {
                if (!resultData.Result)
                    return false;
            }
            return true;
        }

        public bool TransferDocu(DataTable dt)
        {
            SpInfoCollection spCollection = new SpInfoCollection();
            if (dt != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dt,
                    DataState = DataValueState.Added,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpNameInsert = "UP_CZ_PR_OPOUT_IV_TRANS_DOCU",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_IV",
                                                    "NO_MODULE" },
                    SpParamsValues = { { ActionState.Insert, "NO_MODULE", "800" } }
                });
            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spCollection))
            {
                if (!resultData.Result)
                    return false;
            }
            return true;
        }

        public bool TransferDocuCancel(DataTable dt)
        {
            SpInfoCollection spCollection = new SpInfoCollection();
            if (dt != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dt,
                    DataState = DataValueState.Deleted,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameDelete = "UP_PR_FI_DOCU_AUTODEL",
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "NO_MODULE",
                                                    "NO_IV",
                                                    "ID_UPDATE" },
                    SpParamsValues = { { ActionState.Delete, "NO_MODULE", "800" } }
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
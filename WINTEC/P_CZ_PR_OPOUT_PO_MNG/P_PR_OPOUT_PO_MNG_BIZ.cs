using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Data;

namespace cz
{
    internal class P_PR_OPOUT_PO_MNG_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_PR_OPOUT_PO_MNG_H_SELECT", obj);
            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.DataType == typeof(decimal))
                    column.DefaultValue = 0;
            }
            return dataTable;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_CZ_PR_OPOUT_PO_MNG_L_SELECT", obj);
            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.DataType == typeof(decimal))
                    column.DefaultValue = 0;
            }
            return dataTable;
        }

        public bool Save(DataTable dtH, DataTable dtL)
        {
            SpInfoCollection spCollection = new SpInfoCollection();
            if (dtH != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtH;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.SpNameDelete = "UP_CZ_PR_OPOUT_POH_DELETE";
                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_PO",
                                                       "P_NM_PAGE" };
                if (!dtH.Columns.Contains("P_NM_PAGE"))
                    spInfo.SpParamsValues.Add(ActionState.Delete, "P_NM_PAGE", Global.MainFrame.CurrentPageID);
                spCollection.Add(spInfo);
            }
            if (dtL != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtL;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.SpNameDelete = "UP_CZ_PR_OPOUT_POL_DELETE";
                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "NO_PO",
                                                       "NO_LINE",
                                                       "P_NM_PAGE" };
                if (!dtL.Columns.Contains("P_NM_PAGE"))
                    spInfo.SpParamsValues.Add(ActionState.Delete, "P_NM_PAGE", Global.MainFrame.CurrentPageID);
                spCollection.Add(spInfo);
            }
            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spCollection))
            {
                if (!resultData.Result)
                    return false;
            }
            return true;
        }
    }
}
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Data;

namespace cz
{
    internal class P_CZ_PR_OPOUT_WORK_MNG_BIZ
    {
        public DataTable Search(object[] obj) => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_CZ_PR_OPOUT_WORK_MNG_H_SELECT",
            SpParamsSelect = obj
        })).DataValue;

        public DataTable SearchDetail1(object[] obj)
        {
            DataTable dataValue = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_CZ_PR_OPOUT_WORK_MNG_L_SELECT",
                SpParamsSelect = obj
            })).DataValue;
            foreach (DataColumn column in dataValue.Columns)
            {
                if (column.DataType == typeof(decimal))
                    column.DefaultValue = 0;
            }
            dataValue.AcceptChanges();
            return dataValue;
        }

        public bool Save(DataTable dtL)
        {
            SpInfoCollection spInfoCollection = new SpInfoCollection();
            if (dtL != null)
            {
                DataTable dataTable = dtL.Clone();
                for (int index = dtL.Rows.Count - 1; index >= 0; --index)
                    dataTable.ImportRow(dtL.Rows[index]);
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dataTable,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpNameUpdate = "UP_CZ_PR_OPOUT_WORK_MNG_U",
                    SpNameDelete = "UP_CZ_PR_WORK_REG_DELETE",
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_WORK",
                                                    "DC_RMK1",
                                                    "DC_RMK2",
                                                    "DC_RMK3",
                                                    "ID_UPDATE" },
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "NO_WORK",
                                                    "NO_WO",
                                                    "CD_PLANT",
                                                    "CD_OP",
                                                    "QT_WORK",
                                                    "QT_REJECT",
                                                    "QT_MOVE",
                                                    "CD_WC" }
                });
            }
            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spInfoCollection))
            {
                if (!resultData.Result)
                    return false;
            }
            return true;
        }
    }
}

using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;

namespace cz
{
    internal class P_CZ_PR_OPOUT_PO_CNT_RPT_BIZ
    {
        public DataTable Search(object[] obj) => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_CZ_PR_OPOUT_PO_ING_H_RPT_SEL",
            SpParamsSelect = obj
        })).DataValue;

        public DataTable SearchDetail(object[] obj) => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_CZ_PR_OPOUT_PO_ING_L_RPT_SEL",
            SpParamsSelect = obj
        })).DataValue;
    }
}
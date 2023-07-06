using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;

namespace cz
{
    public class P_CZ_PR_OPOUT_WORK_LIST_BIZ
    {
        public DataTable search(object[] obj) => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_CZ_PR_OPOUT_WORK_LIST_SELECT",
            SpParamsSelect = obj
        })).DataValue;
    }
}

using Duzon.Common.Forms;
using Duzon.Common.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
    internal class P_CZ_PR_OPOUT_PO_SUB_BIZ
    {
        public DataTable search(object[] obj)
        {
            SpInfo spinfo = new SpInfo();

            spinfo.SpNameSelect = "UP_PR_OPOUT_PO_SUB_H_SELECT";
            spinfo.SpParamsSelect = obj;

            return (DataTable)((ResultData)Global.MainFrame.FillDataTable(spinfo)).DataValue;
        }

        public DataTable SearchDetail(object[] obj)
        {
            SpInfo spinfo = new SpInfo();

            spinfo.SpNameSelect = "SP_CZ_PR_OPOUT_PO_SUB_L_S";
            spinfo.SpParamsSelect = obj;

            return (DataTable)((ResultData)Global.MainFrame.FillDataTable(spinfo)).DataValue;
        }
    }
}

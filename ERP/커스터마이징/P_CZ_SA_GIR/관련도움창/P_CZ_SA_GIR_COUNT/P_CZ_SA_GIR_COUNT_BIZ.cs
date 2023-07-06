using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Forms;

namespace cz
{
    class P_CZ_SA_GIR_COUNT_BIZ
    {
        public DataTable 협조전진행현황조회(bool 포장업무협조전)
        {
            DataTable dt;

            if (포장업무협조전)
                dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_PACK_COUNT", new object[] { Global.MainFrame.LoginInfo.CompanyCode });
            else
                dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_COUNT", new object[] { Global.MainFrame.LoginInfo.CompanyCode });

            return dt;
        }
    }
}

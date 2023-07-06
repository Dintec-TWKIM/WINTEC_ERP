using Duzon.ERPU;
using Duzon.ERPU.MF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
	internal class P_CZ_PR_OPOUT_PO_WORK_SUB_BIZ
	{
        public DataTable search(object[] obj)
        {
            if (ComFunc.전용코드("공정외주발주등록-적용기준") != "100")
                return DBHelper.GetDataTable("SP_CZ_PR_OPOUT_PO_WORK_SUB_S", obj);
            else
                return DBHelper.GetDataTable("UP_PR_OPOUT_PO_WORK_SUB_100_S", obj);
        }

        public DataTable search_Chcoef(object[] obj)
        {
            if (ComFunc.전용코드("공정외주발주등록-적용기준") != "100")
                return DBHelper.GetDataTable("UP_PR_OPOUT_PO_WORK_SUB_CHC_S", obj);
            else
                return DBHelper.GetDataTable("UP_PR_OPOUT_PO_WORK_SUB_C_100S", obj);
        }
    }
}

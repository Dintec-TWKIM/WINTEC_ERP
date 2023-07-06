using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_SA_PACK_REG_SUB_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_SA_PACK_REG_SUBH_S", obj);
        }

        internal DataTable SearchDetail(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_SA_PACK_REG_SUBL_S", obj);
        }
    }
}

using Duzon.ERPU;
using Duzon.ERPU.MF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace cz
{
    internal class P_CZ_PR_OPOUT_REG_SUB_BIZ
    {
        public DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PR_OPOUT_REG_SUB_S", obj);
        }
    }
}
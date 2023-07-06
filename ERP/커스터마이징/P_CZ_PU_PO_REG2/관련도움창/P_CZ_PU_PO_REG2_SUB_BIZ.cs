using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_PU_PO_SUB2_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PU_PO_REG2_SUBH", obj);
        }

        internal DataTable SearchDetail(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PU_PO_REG2_SUBL", obj);
        }
    }
}
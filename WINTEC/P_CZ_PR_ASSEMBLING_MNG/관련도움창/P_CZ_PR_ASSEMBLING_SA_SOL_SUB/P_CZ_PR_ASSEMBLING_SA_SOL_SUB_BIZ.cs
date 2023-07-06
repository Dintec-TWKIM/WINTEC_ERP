using Duzon.Common.Forms;
using Duzon.ERPU;
using DX;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
    internal class P_CZ_PR_ASSEMBLING_SA_SOL_SUB_BIZ
	{
        internal DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PR_ASSEMBLING_SA_SOL_SUB_S", obj);
        }

        internal DataTable Search1(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PR_ASSEMBLING_SA_SOL_SUB_S1", obj);
        }

        internal bool Save(DataTable dt)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_PR_ASSEMBLING_ID_OLD_SUB_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
        }
    }
}

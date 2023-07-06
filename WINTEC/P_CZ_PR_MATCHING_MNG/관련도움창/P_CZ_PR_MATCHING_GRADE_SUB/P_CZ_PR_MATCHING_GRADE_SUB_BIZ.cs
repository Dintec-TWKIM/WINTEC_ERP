using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using DX;
using System.Data;

namespace cz
{
	internal class P_CZ_PR_MATCHING_GRADE_SUB_BIZ
	{
        internal DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PR_MATCHING_GRADE_SUB_S", obj);
        }

        internal bool Save(DataTable dt)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_PR_MATCHING_GRADE_SUB_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
        }
    }
}

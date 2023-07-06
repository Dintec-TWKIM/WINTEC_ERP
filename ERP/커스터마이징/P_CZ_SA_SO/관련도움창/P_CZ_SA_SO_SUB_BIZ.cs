using System.Data;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    class P_CZ_SA_SO_SUB_BIZ
    {
        public DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_SA_SO_SUBH_S", obj, "NO_SO");
        }

        public DataTable SearchDetail(string NO_SO)
        {
            return DBHelper.GetDataTable("SP_CZ_SA_SO_SUBL_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                              NO_SO }, "SEQ_SO");
        }
    }
}

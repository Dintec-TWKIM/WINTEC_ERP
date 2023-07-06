using System.Data;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_SA_TRCREDITSCH_BIZ
    {
        private string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;

        public DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_SA_TRCREDITSCH_S", obj);
        }

        internal void SaveContent(ContentType contentType, Dass.FlexGrid.CommandType commandType, string noIV, string value, string gubun)
        {
            string query = string.Empty;
            string str = string.Empty;
            
            if (contentType == ContentType.Memo)
                str = "MEMO_CD";
            else if (contentType == ContentType.CheckPen)
                str = "CHECK_PEN";

            if (commandType == Dass.FlexGrid.CommandType.Add)
                query = "UPDATE SA_IVH SET " + str + " = '" + value + "' WHERE NO_IV = '" + noIV + "' AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
            else if (commandType == Dass.FlexGrid.CommandType.Delete)
                query = "UPDATE SA_IVH SET " + str + " = NULL WHERE NO_IV = '" + noIV + "' AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
            
            Global.MainFrame.ExecuteScalar(query);
        }
    }
}

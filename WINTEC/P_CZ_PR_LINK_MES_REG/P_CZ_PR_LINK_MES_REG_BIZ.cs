using Duzon.Common.Util;
using Duzon.ERPU;
using System.Data;

namespace cz
{
    internal class P_CZ_PR_LINK_MES_REG_BIZ
    {
        private string CD_COMPANY = MA.Login.회사코드;
        private string ID_USER = MA.Login.사용자아이디;

        public DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PR_LINK_MES_REG_S", obj);
        }

        public DataTable SearchSub(object[] obj)
        {
            return DBHelper.GetDataTable("UP_PR_LINK_MES_REG_SUB_S", obj);
        }

        public DataTable SearchWCOP(object[] obj)
        {
            return DBHelper.GetDataTable("UP_PR_LINK_MES_REG_WCOP_S", obj);
        }

        public string SearchURL()
        {
            DataTable dataTable = DBHelper.GetDataTable("SELECT * FROM MA_MES10_BATCH_INFO WHERE CD_COMPANY = '" + this.CD_COMPANY + "'");
            return dataTable.Rows.Count < 1 ? string.Empty : D.GetString(dataTable.Rows[0]["TXT_URL"]);
        }

        public bool Save(DataTable dt, string state)
        {
            if (dt == null || dt.Rows.Count < 1)
                return false;

            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = this.CD_COMPANY;
            si.UserID = this.ID_USER;
            
            if (state == "I")
            {
                si.DataState = DataValueState.Added;
                si.SpNameInsert = "SP_CZ_PR_LINK_MES_REG_I";
                si.SpParamsInsert = new string[] { "CD_COMPANY",
                                                   "CD_PLANT",
                                                   "NO_MES" };
            }
            else if (state == "U")
            {
                si.DataState = DataValueState.Modified;
                si.SpNameUpdate = "UP_PR_LINK_MES_REG_U";
                si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                   "CD_PLANT",
                                                   "NO_MES",
                                                   "CD_ITEM",
                                                   "CD_SL_IN",
                                                   "CD_WC",
                                                   "CD_OP",
                                                   "CD_WCOP",
                                                   "ID_UPDATE" };
            }
            else if (state == "D")
            {
                si.DataState = DataValueState.Deleted;
                si.SpNameDelete = "UP_PR_LINK_MES_REG_D";
                si.SpParamsDelete = new string[] { "CD_COMPANY", 
                                                   "CD_PLANT",
                                                   "NO_MES" };
            }
            return DBHelper.Save(si);
        }
    }
}

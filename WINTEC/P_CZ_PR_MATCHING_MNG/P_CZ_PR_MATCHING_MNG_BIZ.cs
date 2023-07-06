using Duzon.Common.Forms;
using Duzon.ERPU;
using DX;
using System.Data;

namespace cz
{
	internal class P_CZ_PR_MATCHING_MNG_BIZ
	{
        public DataTable Search(object[] obj)
        {

            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_MATCHING_MNG_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_MATCHING_MNG_S1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search2(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_MATCHING_MNG_S2", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_MATCHING_MNGD_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchID(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_MATCHING_MNG_ID", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}

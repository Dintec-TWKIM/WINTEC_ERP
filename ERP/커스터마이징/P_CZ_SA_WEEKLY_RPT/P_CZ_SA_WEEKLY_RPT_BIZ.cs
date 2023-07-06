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
	internal class P_CZ_SA_WEEKLY_RPT_BIZ
	{
        public DataTable SearchHeader(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_H_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_L1_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail2(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_L2_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail3(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_L3_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail4(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_L4_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail5(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_L5_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_S1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search2(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_S2", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search3(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_S3", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search4(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_WEEKLY_RPT_S4", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        internal bool SaveJson(DataTable dt담당자, DataTable dt견적L, DataTable dt수주L, DataTable dt프로젝트, DataTable dt클레임, DataTable dt미수금)
        {
            bool result = true;

            result = DBHelper.ExecuteNonQuery("SP_CZ_SA_WEEKLY_RPT_H_JSON", new object[] { dt담당자.Json(), Global.MainFrame.LoginInfo.UserID });
            result = DBHelper.ExecuteNonQuery("SP_CZ_SA_WEEKLY_RPT_L_JSON", new object[] { dt견적L.Json(), Global.MainFrame.LoginInfo.UserID });
            result = DBHelper.ExecuteNonQuery("SP_CZ_SA_WEEKLY_RPT_L_JSON", new object[] { dt수주L.Json(), Global.MainFrame.LoginInfo.UserID });
            result = DBHelper.ExecuteNonQuery("SP_CZ_SA_WEEKLY_RPT_L_JSON", new object[] { dt프로젝트.Json(), Global.MainFrame.LoginInfo.UserID });
            result = DBHelper.ExecuteNonQuery("SP_CZ_SA_WEEKLY_RPT_L_JSON", new object[] { dt클레임.Json(), Global.MainFrame.LoginInfo.UserID });
            result = DBHelper.ExecuteNonQuery("SP_CZ_SA_WEEKLY_RPT_L_JSON", new object[] { dt미수금.Json(), Global.MainFrame.LoginInfo.UserID });

            return result;
        }
    }
}

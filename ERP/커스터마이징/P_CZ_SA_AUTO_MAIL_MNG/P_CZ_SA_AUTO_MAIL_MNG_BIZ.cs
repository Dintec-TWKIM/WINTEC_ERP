using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using DX;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
	internal class P_CZ_SA_AUTO_MAIL_MNG_BIZ
	{
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_AUTO_MAIL_PU_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search1(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_AUTO_MAIL_SA_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDetail(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_AUTO_MAIL_NOT_GR", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDetail1(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_AUTO_MAIL_ORDER_STATUS", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDetail2(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_AUTO_MAIL_READY_INFO", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDetail3(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_AUTO_MAIL_READY_PACK", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDetail4(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_AUTO_MAIL_CATEGORY_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dt)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_SA_AUTO_MAIL_MNG_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
        }

        internal bool Save1(DataTable dt)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_SA_AUTO_MAIL_MNG_JSON1", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
        }

        internal bool Save2(DataTable dt)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_SA_AUTO_MAIL_CATEGORY_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
        }

        public DataSet 협조전데이터(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_PACK_MNG_GIR_S", obj);
            T.SetDefaultValue(ds);
            return ds;
        }

        public DataSet 포장데이터(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_GIRSCH_PACK_S", obj);
            T.SetDefaultValue(ds);
            return ds;
        }
    }
}

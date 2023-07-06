using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
    class P_CZ_SA_GIR_DAILY_PLAN_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_DAILY_PLAN_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_DAILY_PLAN_S1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search2(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_DAILY_PLAN_S2", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search3(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_DAILY_PLAN_S3", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable Search4(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_DAILY_PLAN_S4", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public bool SaveData(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();

            if (dt != null)
            {
                spInfo.DataValue = Util.GetXmlTable(dt);
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;

                spInfo.SpNameInsert = "SP_CZ_SA_GIR_DAILY_PLAN_XML";
                spInfo.SpParamsInsert = new string[] { "XML", "ID_INSERT" };
            }

            return DBHelper.Save(spInfo);
        }
    }
}

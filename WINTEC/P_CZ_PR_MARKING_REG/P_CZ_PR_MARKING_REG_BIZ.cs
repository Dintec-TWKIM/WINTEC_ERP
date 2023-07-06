using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
	internal class P_CZ_PR_MARKING_REG_BIZ
	{
        public DataTable SearchWO(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_MARKING_REG_WO_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchTM(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_MARKING_REG_TM_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchGIR(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_MARKING_REG_GIR_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchWODetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_MARKING_REG_WO_S1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchTMDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_MARKING_REG_TM_S1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchGIRDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_MARKING_REG_GIR_S1", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
    }
}

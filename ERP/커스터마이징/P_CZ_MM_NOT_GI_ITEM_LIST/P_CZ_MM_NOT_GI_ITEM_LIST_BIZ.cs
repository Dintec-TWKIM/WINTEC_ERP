using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
    internal class P_CZ_MM_NOT_GI_ITEM_LIST_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("P_CZ_MM_NOT_GI_ITEM_LIST_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }


        public DataTable Search1()
        {
            DataTable dt = DBHelper.GetDataTable("P_CZ_MM_NOT_GI_ITEM_LIST_S1");
            T.SetDefaultValue(dt);
            return dt;
        }


    }
}

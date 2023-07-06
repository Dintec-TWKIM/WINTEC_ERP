using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Duzon.ERPU;
using Duzon.Common.Forms;
using System.Data;


namespace cz
{
   internal class P_CZ_BI_QTN_NOT_DONE_BIZ
   {
       internal DataTable Search1(object[] obj)
       {
           DataTable dataTable = DBHelper.GetDataTable("SP_CZ_BI_QTN_NOT_DONE_1", obj);

           T.SetDefaultValue(dataTable);
           return dataTable;
       }

       internal DataTable Search2(object[] obj)
       {
           DataTable dataTable = DBHelper.GetDataTable("SP_CZ_BI_QTN_NOT_DONE_2", obj);

           T.SetDefaultValue(dataTable);
           return dataTable;
       }


       internal DataTable Search3(object[] obj)
       {
           DataTable dataTable = DBHelper.GetDataTable("SP_CZ_BI_QTN_NOT_DONE_3", obj);

           T.SetDefaultValue(dataTable);
           return dataTable;
       }


       internal DataTable Search4(object[] obj)
       {
           DataTable dataTable = DBHelper.GetDataTable("SP_CZ_BI_QTN_NOT_DONE_4", obj);

           T.SetDefaultValue(dataTable);
           return dataTable;
       }

       internal DataTable Search5(object[] obj)
       {
           DataTable dataTable = DBHelper.GetDataTable("SP_CZ_BI_QTN_NOT_DONE_5", obj);

           T.SetDefaultValue(dataTable);
           return dataTable;
       }


       internal DataTable Search6(object[] obj)
       {
           DataTable dataTable = DBHelper.GetDataTable("SP_CZ_BI_QTN_NOT_DONE_6", obj);

           T.SetDefaultValue(dataTable);
           return dataTable;
       }

        // 담당자원인별미견적
        internal DataTable Search7(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_BI_QTN_NOT_DONE_7", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }
    }
}

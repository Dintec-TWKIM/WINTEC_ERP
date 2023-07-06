using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace cz
{
    class P_CZ_SA_CRM_EMP_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_EMP_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search거래내역(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_EMP_HISTORY_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search미수금(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_EMP_OUTSTANDING_INV_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search클레임(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_EMP_CLAIM_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search일정(string 사원번호)
        {
            DBMgr dbMgr = new DBMgr(DBConn.GroupWare);
            dbMgr.Procedure = "BX.SP_CZ_SA_CRM_EMP_SCHEDULE_S";
            dbMgr.AddParameter("@P_NO_EMP", 사원번호);
            
            DataTable dataTable = dbMgr.GetDataTable();

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search할일(string 사원번호)
        {
            DBMgr dbMgr = new DBMgr(DBConn.GroupWare);
            dbMgr.Procedure = "BX.SP_CZ_SA_CRM_EMP_TODOLIST_S";
            dbMgr.AddParameter("@P_NO_EMP", 사원번호);

            DataTable dataTable = dbMgr.GetDataTable();

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable Search메모(string 사원번호)
        {
            DBMgr dbMgr = new DBMgr(DBConn.GroupWare);
            dbMgr.Procedure = "BX.SP_CZ_SA_CRM_EMP_MEMO_S";
            dbMgr.AddParameter("@P_NO_EMP", 사원번호);

            DataTable dataTable = dbMgr.GetDataTable();

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dt일정, DataTable dt할일, DataTable dt메모)
        {
            if (dt일정 != null && dt일정.Rows.Count != 0)
            {
                DBMgr dbMgr = new DBMgr(DBConn.GroupWare);
                dbMgr.Procedure = "BX.SP_CZ_SA_CRM_EMP_SCHEDULE_XML";
                dbMgr.AddParameter("@P_XML", Util.GetTO_Xml(dt일정));
                dbMgr.AddParameter("@P_NO_EMP", Global.MainFrame.LoginInfo.EmployeeNo);

                dbMgr.ExecuteNonQuery();
            }

            if (dt할일 != null && dt할일.Rows.Count != 0)
            {
                DBMgr dbMgr = new DBMgr(DBConn.GroupWare);
                dbMgr.Procedure = "BX.SP_CZ_SA_CRM_EMP_TODOLIST_XML";
                dbMgr.AddParameter("@P_XML", Util.GetTO_Xml(dt할일));
                dbMgr.AddParameter("@P_NO_EMP", Global.MainFrame.LoginInfo.EmployeeNo);

                dbMgr.ExecuteNonQuery();
            }

            if (dt메모 != null && dt메모.Rows.Count != 0)
            {
                DBMgr dbMgr = new DBMgr(DBConn.GroupWare);
                dbMgr.Procedure = "BX.SP_CZ_SA_CRM_EMP_MEMO_XML";
                dbMgr.AddParameter("@P_XML", Util.GetTO_Xml(dt메모));
                dbMgr.AddParameter("@P_NO_EMP", Global.MainFrame.LoginInfo.EmployeeNo);

                dbMgr.ExecuteNonQuery();
            }

            return true;
        }
    }
}

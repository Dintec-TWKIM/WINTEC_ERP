using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Dintec;

namespace cz
{
    class P_CZ_SA_QTNSO_RPT_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            string query = (string)DBHelper.ExecuteScalar("SP_CZ_SA_QTNSO_RPTH_S", obj);
            DataTable dataTable = this.DirectQuery(D.GetString(obj[0]), query);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal DataTable SearchDetail(object[] obj)
        {
            string query = (string)DBHelper.ExecuteScalar("SP_CZ_SA_QTNSO_RPTL_S", obj);
            DataTable dataTable = this.DirectQuery(D.GetString(obj[0]), query);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        public DataTable 담당자정보(string 회사)
        {
            DataTable dt = null;
            string tablePrefix, query;
            
            try
            {
                if (회사 == "002")
                    tablePrefix = "R";
                else
                    tablePrefix = "T";

                query = @"SELECT DISTINCT A.USER_ID AS CODE,
                                 B.KOR_NAME AS NAME" + Environment.NewLine +
                         "FROM " + tablePrefix + "_PS003 A," + Environment.NewLine +
                         "     " + tablePrefix + "_BC001 B" + Environment.NewLine +
                         "WHERE B.USER_ID = A.USER_ID" + Environment.NewLine +
                         "ORDER BY B.KOR_NAME";

                dt = this.DirectQuery(회사, query);
                this.AddEmptyRow(dt);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return dt;
        }

        public DataTable 지역코드정보(string 회사)
        {
            DataTable dt = null;
            string tablePrefix, query;

            try
            {
                if (회사 == "002")
                    tablePrefix = "R";
                else
                    tablePrefix = "T";

                query = @"SELECT LEVEL_2 AS CODE,
                                 CODE_NAME AS NAME" + Environment.NewLine +
                         "FROM " + tablePrefix + "_BC006" + Environment.NewLine +
                         "WHERE LEVEL_1 = '15'" + Environment.NewLine +
                         "ORDER BY CODE_NAME ASC";

                dt = this.DirectQuery(회사, query);
                this.AddEmptyRow(dt);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return dt;
        }

        public DataTable 국가코드정보(string 회사)
        {
            DataTable dt = null;
            string tablePrefix, query;

            try
            {
                if (회사 == "002")
                    tablePrefix = "R";
                else
                    tablePrefix = "T";

                query = @"SELECT LEVEL_2 AS CODE,
                                 CODE_NAME AS NAME" + Environment.NewLine +
                         "FROM " + tablePrefix + "_BC006" + Environment.NewLine +
                         "WHERE LEVEL_1 = '36'" + Environment.NewLine +
                         "ORDER BY CODE_NAME ASC";

                dt = this.DirectQuery(회사, query);
                this.AddEmptyRow(dt);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return dt;
        }

        public DataTable OpenQuery(string Query)
        {
            DataTable dt = null;
            
            try
            {
                return DBHelper.GetDataTable("SELECT * FROM OPENQUERY(DINTEC, '" + Query.Replace("'", "''") + "')");
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return dt;
        }

        public DataTable DirectQuery(string 회사, string Query)
        {
            DataTable dt = null;
            DBMgr dbMgr;
 
            try
            {
                dbMgr = new DBMgr((DBConn)D.GetInt(회사));
                dbMgr.Query = Query;
                return dbMgr.GetDataTable();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return dt;
        }

        private void AddEmptyRow(DataTable dt)
        {
            DataRow emptyRow;

            try
            {
                if (dt == null || dt.Rows.Count == 0) return;

                emptyRow = dt.NewRow();
                dt.Rows.InsertAt(emptyRow, 0);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
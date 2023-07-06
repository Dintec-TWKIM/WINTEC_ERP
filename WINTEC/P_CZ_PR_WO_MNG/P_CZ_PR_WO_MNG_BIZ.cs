using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Data;

namespace cz
{
    internal class P_CZ_PR_WO_MNG_BIZ
    {
        public DataSet Search(object[] obj)
        {
            DataSet dataSet = DBHelper.GetDataSet("SP_CZ_PR_WO_MNG_S", obj);

            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.DataType == typeof(decimal))
                        column.DefaultValue = 0;
                }
            }

            return dataSet;
        }

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_WO_MNGDH_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_WO_MNGDL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public bool Save(DataTable dtM, DataTable dtD, DataTable dtDL)
        {
            SpInfoCollection spc = new SpInfoCollection();

            if (dtM != null)
			{
                spc.Add(new SpInfo()
                {
                    DataValue = dtM,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameUpdate = "SP_CZ_PR_WO_MNG_U1",
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_WO",
                                                    "DC_RMK",
                                                    "ID_UPDATE",
                                                    "CD_USERDEF1",
                                                    "CD_USERDEF2",
                                                    "CD_USERDEF3",
                                                    "CD_USERDEF4",
                                                    "CD_USERDEF5",
                                                    "NO_LOT",
                                                    "DT_LIMIT",
                                                    "NUM_USERDEF1",
                                                    "NUM_USERDEF2",
                                                    "DT_CLOSE",
                                                    "DC_RMK2",
                                                    "NO_HEAT" } 
                });
            }

            if (dtD != null)
			{
                spc.Add(new SpInfo()
                {
                    DataValue = dtD,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameUpdate = "SP_CZ_PR_WO_MNG_U",
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_WO",
                                                    "NO_LINE",
                                                    "CD_EQUIP",
                                                    "DC_RMK",
                                                    "DC_RMK_1",
                                                    "ID_UPDATE" }
                });
            }

            if (dtDL != null)
			{
                spc.Add(new SpInfo()
                {
                    DataValue = Util.GetXmlTable(dtDL),
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "SP_CZ_PR_WO_MNG_INSP_XML",
                    SpParamsInsert = new string[] { "XML", "ID_INSERT" }
                });
            }
            
            return DBHelper.Save(spc);
        }

        public bool delete(DataTable dt)
        {
            SpInfoCollection spCollection = new SpInfoCollection();

            if (dt != null)
			{
                spCollection.Add(new SpInfo()
                {
                    DataValue = dt,
                    DataState = DataValueState.Deleted,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpNameDelete = "SP_CZ_PR_WO_REG_NEW_DELETE",
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_WO" }
                });
            }
                
            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spCollection))
            {
                if (!resultData.Result)
                    return false;
            }

            return true;
        }

        public ResultData WO_CLOSE(DataTable dtClose)
		{
            return (ResultData)Global.MainFrame.Save(new SpInfo() 
            {
                DataValue = dtClose,
                SpNameInsert = "UP_PR_WO_CLOSE",
                SpParamsInsert = new string[] { "CD_COMPANY",
                                                "CD_PLANT",
                                                "NO_WO",
                                                "DT_CLOSE" },
                CompanyID = Global.MainFrame.LoginInfo.CompanyCode
            });
        }

        public ResultData WO_CLOSECANCEL(DataTable dtClose)
        {
            return (ResultData)Global.MainFrame.Save(new SpInfo()
            {
                DataValue = dtClose,
                SpNameInsert = "UP_PR_WO_CLOSECANCEL",
                SpParamsInsert = new string[] { "CD_COMPANY",
                                                "CD_PLANT",
                                                "NO_WO" },
                CompanyID = Global.MainFrame.LoginInfo.CompanyCode
            });
        }

        public ResultData WO_PLANCONFIRMCANCEL(DataTable dtPlanConfirm)
        {
            return (ResultData)Global.MainFrame.Save(new SpInfo()
            {
                DataValue = dtPlanConfirm,
                SpNameInsert = "UP_PR_WO_PLANCONFIRM",
                SpParamsInsert = new string[] { "CD_COMPANY",
                                                "CD_PLANT",
                                                "NO_WO",
                                                "ST_WO" },
                CompanyID = Global.MainFrame.LoginInfo.CompanyCode
            });
        }

        public DataTable Get전체경로유형(string str공장)
        {
            string sql = string.Empty;

            sql = @"SELECT NULL AS CODE,
                           NULL AS NAME
                    UNION ALL
                    SELECT NO_OPPATH AS CODE,
                           NO_OPPATH + '-' + DC_OPPATH AS NAME
                    FROM PR_ROUT
                    WHERE CD_COMPANY = '{0}'
                    AND CD_PLANT = '{1}'
                    GROUP BY NO_OPPATH, DC_OPPATH
                    ORDER BY CODE, NAME";

            sql = string.Format(sql, Global.MainFrame.LoginInfo.CompanyCode, str공장);

            return DBHelper.GetDataTable(sql);
        }

        public DataSet Search_AUTH(object[] obj)
        {
            return DBHelper.GetDataSet("UP_MA_MFG_AUTH_SELECT", obj);
        }

        public DataTable Search_Tp_Wo(object[] obj)
        {
            string query;

            query = @"SELECT TP_WO,
                             NM_TP_WO
                      FROM PR_TPWO
                      WHERE CD_COMPANY = '{0}'
                      AND YN_USE = 'Y'
                      ORDER BY TP_WO, NM_TP_WO";

            query = string.Format(query, obj[0].ToString());

            return DBHelper.GetDataTable(query);
        }

        public DataTable Search_Cd_Wc(object[] obj)
        {
            string query;

            query = @"SELECT CD_WC,
                             NM_WC
                      FROM MA_WC
                      WHERE CD_COMPANY = '{0}'
                      AND CD_PLANT = '{1}'
                      ORDER BY CD_WC,NM_WC";

            query = string.Format(query, obj[0].ToString(), obj[1].ToString());

            return DBHelper.GetDataTable(query);
        }

        public DataSet GetSchemaDelete()
        {
            DataTable table1 = new DataTable();
            table1.Columns.Add("CD_PLANT", typeof(string));
            table1.Columns.Add("NO_WO", typeof(string));

            DataTable table2 = new DataTable();
            table2.Columns.Add("CD_PLANT", typeof(string));
            table2.Columns.Add("NO_WO", typeof(string));
            table2.Columns.Add("ST_WO_INSERT", typeof(string));
            table2.Columns.Add("DT_RELEASE_INSERT", typeof(string));
            table2.Columns.Add("QT_ITEM", typeof(decimal));
            table2.Columns.Add("NO_LOT", typeof(string));
            table2.Columns.Add("DC_RMK", typeof(string));
            table2.Columns.Add("PATN_ROUT", typeof(string));

            DataTable table3 = new DataTable();
            table3.Columns.Add("CD_PLANT", typeof(string));
            table3.Columns.Add("NO_LINE", typeof(decimal));
            table3.Columns.Add("CD_OP", typeof(string));
            table3.Columns.Add("CD_WC", typeof(string));
            table3.Columns.Add("CD_WCOP", typeof(string));
            table3.Columns.Add("NO_WO", typeof(string));
            table3.Columns.Add("NO_BAR_REL", typeof(string));

            DataTable table4 = new DataTable();
            table4.Columns.Add("CD_PLANT", typeof(string));
            table4.Columns.Add("NO_WO", typeof(string));

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(table1);
            dataSet.Tables.Add(table2);
            dataSet.Tables.Add(table3);
            dataSet.Tables.Add(table4);
            dataSet.AcceptChanges();
            
            return dataSet;
        }

        public bool Delete_WO(DataTable dt_Work, DataTable dt_Rel, DataTable dt_RelRout, DataTable dt_Wo)
        {
            SpInfoCollection spc = new SpInfoCollection();
            if (dt_Work != null && dt_Work.Rows.Count != 0)
			{
                spc.Add(new SpInfo()
                {
                    DataValue = dt_Work,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpNameInsert = "UP_PR_WORK_REG_NO_WO_DELETE",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_WO" }
                });
            }

            if (dt_Rel != null && dt_Rel.Rows.Count != 0)
			{
                spc.Add(new SpInfo()
                {
                    DataValue = dt_Rel,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.UserID,
                    SpNameInsert = "UP_PR_WO_REL_INSERT",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_WO",
                                                    "ST_WO_INSERT",
                                                    "DT_RELEASE_INSERT",
                                                    "QT_ITEM",
                                                    "ID_INSERT",
                                                    "NO_LOT",
                                                    "DC_RMK",
                                                    "PATN_ROUT" }
                });
            }

            if (dt_RelRout != null && dt_RelRout.Rows.Count != 0)
			{
                spc.Add(new SpInfo()
                {
                    DataValue = dt_RelRout,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpNameInsert = "UP_PR_WO_ROUT_REL_UPDATE",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_LINE",
                                                    "CD_OP",
                                                    "CD_WC",
                                                    "CD_WCOP",
                                                    "NO_WO",
                                                    "NO_BAR_REL" }
                });
            }
                
            if (dt_Wo != null && dt_Wo.Rows.Count != 0)
			{
                spc.Add(new SpInfo()
                {
                    DataValue = dt_Wo,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpNameInsert = "SP_CZ_PR_WO_REG_NEW_DELETE",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_WO" }
                });
            }
                
            return DBHelper.Save(spc);
        }

        public string SearchFileCount(string CD_FILE)
        {
            string query;

            query = @"SELECT COUNT(FILE_NAME) AS CNT
                      FROM MA_FILEINFO
                      WHERE CD_COMPANY = '{0}'
                      AND CD_MODULE = 'PR'
                      AND ID_MENU = 'P_PR_WO_REG02'
                      AND CD_FILE = '{1}'";

            query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, CD_FILE);

            DataTable dataTable = DBHelper.GetDataTable(query);
            return dataTable.Rows.Count < 1 ? "0건" : D.GetString(dataTable.Rows[0]["CNT"]) + "건";
        }
    }
}
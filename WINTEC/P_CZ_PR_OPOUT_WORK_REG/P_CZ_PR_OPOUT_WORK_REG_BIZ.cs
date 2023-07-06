using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Data;
using Dintec;

namespace cz
{
    internal class P_CZ_PR_OPOUT_WORK_REG_BIZ
    {
        public string SELECT_YN_PR_MNG_LOT(object[] obj)
        {
            DataTable dataValue = (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_PR_YN_LOT_SELECT",
                SpParamsSelect = obj
            })).DataValue;
            string str = "N";
            if (dataValue.Rows.Count == 1)
                str = dataValue.Rows[0]["MNG_LOT"].ToString() == "Y" ? "Y" : "N";
            return str;
        }

        public DataTable Search(object[] obj) => DBHelper.GetDataTable("UP_CZ_PR_OPOUT_WORK_SELECT1", obj);

        public DataTable Search2(object[] obj) => DBHelper.GetDataTable("UP_PR_OPOUT_WORK_SELECT2", obj);

        public DataTable Search_Chcoef(object[] obj) => DBHelper.GetDataTable("UP_PR_OPOUT_WORK_CHC_S", obj);

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_CZ_PR_OPOUT_WORK_REG_SELECT", obj);
            dataTable.Columns.Add("NO_WORK", typeof(string));
            dataTable.Columns.Add("NO_EMP", typeof(string));
            dataTable.Columns.Add("NM_KOR", typeof(string));
            dataTable.Columns.Add("CD_DEPT", typeof(string));
            dataTable.Columns.Add("QT_WORK", typeof(decimal));
            dataTable.Columns.Add("QT_REJECT", typeof(decimal));
            dataTable.Columns.Add("CD_RSRC_LABOR", typeof(string));
            dataTable.Columns.Add("TM_LABOR", typeof(decimal));
            dataTable.Columns.Add("TM_MACH", typeof(decimal));
            dataTable.Columns.Add("CD_RSRC_MACH", typeof(string));
            dataTable.Columns.Add("YN_REWORK", typeof(string));
            dataTable.Columns.Add("YN_BAD_PROC", typeof(string));
            dataTable.Columns.Add("TP_REWORK", typeof(string));
            dataTable.Columns.Add("CD_REWORK", typeof(string));
            dataTable.Columns.Add("CD_REJECT", typeof(string));
            dataTable.Columns.Add("DC_REJECT", typeof(string));
            if (!dataTable.Columns.Contains("CD_DEPT_WORK"))
                dataTable.Columns.Add("CD_DEPT_WORK", typeof(string));
            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.DataType == typeof(decimal))
                    column.DefaultValue = 0;
            }
            foreach (DataRow row in dataTable.Rows)
            {
                row["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
                row["NM_KOR"] = Global.MainFrame.LoginInfo.EmployeeName;
                row["CD_DEPT"] = Global.MainFrame.LoginInfo.DeptCode;
                row["QT_WORK"] = 0;
                row["QT_REJECT"] = 0;
                row["TM_LABOR"] = 0;
                row["TM_MACH"] = 0;
            }
            dataTable.AcceptChanges();
            return dataTable;
        }

        public DataTable SearchDetail1(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_OPOUT_WORK_ID_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable SearchDetail_Chcoef(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_PR_OPOUT_WORK_REG_CHC_S", obj);
            dataTable.Columns.Add("NO_WORK", typeof(string));
            dataTable.Columns.Add("NO_EMP", typeof(string));
            dataTable.Columns.Add("NM_KOR", typeof(string));
            dataTable.Columns.Add("CD_DEPT", typeof(string));
            dataTable.Columns.Add("QT_WORK", typeof(decimal));
            dataTable.Columns.Add("QT_REJECT", typeof(decimal));
            dataTable.Columns.Add("CD_RSRC_LABOR", typeof(string));
            dataTable.Columns.Add("TM_LABOR", typeof(decimal));
            dataTable.Columns.Add("TM_MACH", typeof(decimal));
            dataTable.Columns.Add("CD_RSRC_MACH", typeof(string));
            dataTable.Columns.Add("YN_REWORK", typeof(string));
            dataTable.Columns.Add("YN_BAD_PROC", typeof(string));
            dataTable.Columns.Add("TP_REWORK", typeof(string));
            dataTable.Columns.Add("CD_REWORK", typeof(string));
            dataTable.Columns.Add("CD_REJECT", typeof(string));
            dataTable.Columns.Add("DC_REJECT", typeof(string));
            dataTable.Columns.Add("CD_DEPT_WORK", typeof(string));
            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.DataType == typeof(decimal))
                    column.DefaultValue = 0;
            }
            foreach (DataRow row in dataTable.Rows)
            {
                row["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
                row["NM_KOR"] = Global.MainFrame.LoginInfo.EmployeeName;
                row["CD_DEPT"] = Global.MainFrame.LoginInfo.DeptCode;
                row["QT_WORK"] = 0;
                row["QT_REJECT"] = 0;
                row["TM_LABOR"] = 0;
                row["TM_MACH"] = 0;
            }
            dataTable.AcceptChanges();
            return dataTable;
        }

        public bool DELETE_NO_WO(object[] obj) => ((ResultData)Global.MainFrame.ExecSp("UP_PR_OPOUT_WORK_NO_WO_DELETE", obj)).Result;

        public bool DELETE_PO_WO(object[] obj) => ((ResultData)Global.MainFrame.ExecSp("UP_PR_OPOUT_WORK_PO_WO_DELETE", obj)).Result;

        public bool Save( DataTable dtWork,
                          DataTable dtReject,
                          DataTable _dtLotItem,
                          DataTable _dtMatl,
                          DataTable _dtLot,
                          DataTable _dtSERL,
                          string Noline,
                          string strNO_II,
                          string str입고창고,
                          string str발주번호,
                          decimal d발주항번,
                          DataTable dt_Auto_Bad_Work,
                          DataTable dt_Auto_Bad,
                          DataTable dt_AutoBad_ReqH,
                          DataTable dt_AutoBad_ReqL,
                          DataTable dt_Manday,
                          DataTable _dtInsp)
        {
            SpInfoCollection spCollection = new SpInfoCollection();
            if (dtWork != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtWork,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    DataState = DataValueState.Added,
                    SpNameInsert = "UP_PR_WORK_REG_INSERT",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_WORK",
                                                    "NO_WO",
                                                    "CD_PLANT",
                                                    "CD_OP",
                                                    "CD_ITEM",
                                                    "NO_EMP",
                                                    "DT_WORK",
                                                    "QT_WORK",
                                                    "QT_REJECT",
                                                    "QT_MOVE",
                                                    "YN_REWORK",
                                                    "TM_LABOR",
                                                    "CD_RSRC_LABOR",
                                                    "TM_MACH",
                                                    "CD_RSRC_MACH",
                                                    "CD_WC",
                                                    "ID_INSERT",
                                                    "FG_MOVE",
                                                    "NO_LOT",
                                                    "NO_SFT",
                                                    "CD_EQUIP",
                                                    "NO_IO_202_102",
                                                    "NO_IO_203",
                                                    "NO_LINE_202",
                                                    "NO_LINE_102",
                                                    "NO_LINE_203",
                                                    "CD_SL",
                                                    "CD_WCOP_SUB",
                                                    "DC_RMK1",
                                                    "DC_RMK2",
                                                    "YN_SUBCON",
                                                    "NO_PO",
                                                    "NO_POLINE",
                                                    "NO_REL",
                                                    "QT_RSRC_LABOR",
                                                    "NO_WORK_TRACKING",
                                                    "DC_RMK3",
                                                    "QT_RATE_CALC",
                                                    "DT_LIMIT",
                                                    "QT_WO",
                                                    "QT_WO_WORK",
                                                    "QT_CHCOEF",
                                                    "QT_WORK_CHCOEF",
                                                    "QT_WORK_BAD_CHCOEF",
                                                    "CD_MNG1",
                                                    "CD_MNG2",
                                                    "CD_MNG3",
                                                    "CD_MNG4",
                                                    "CD_MNG5",
                                                    "CD_MNG6",
                                                    "CD_MNG7",
                                                    "CD_MNG8",
                                                    "CD_MNG9",
                                                    "CD_MNG10",
                                                    "CD_MNG11",
                                                    "CD_MNG12",
                                                    "CD_MNG13",
                                                    "CD_MNG14",
                                                    "CD_MNG15",
                                                    "CD_MNG16",
                                                    "CD_MNG17",
                                                    "CD_MNG18",
                                                    "CD_MNG19",
                                                    "CD_MNG20",
                                                    "CD_POST",
                                                    "CD_USERDEF1",
                                                    "CD_DEPT_WORK",
                                                    "TXT_USERDEF1",
                                                    "TXT_USERDEF2",
                                                    "TXT_USERDEF3",
                                                    "CD_USERDEF2",
                                                    "CD_USERDEF3",
                                                    "NUM_USERDEF1",
                                                    "NUM_USERDEF2",
                                                    "NUM_USERDEF3",
                                                    "NUM_USERDEF4",
                                                    "NUM_USERDEF5",
                                                    "NUM_USERDEF6",
                                                    "NUM_USERDEF7",
                                                    "NUM_USERDEF8",
                                                    "NUM_USERDEF9",
                                                    "NUM_USERDEF10" },
                    SpParamsValues = 
                    {
                        {
                          ActionState.Insert,
                          "CD_SL",
                           str입고창고
                        },
                        {
                          ActionState.Insert,
                          "CD_WCOP_SUB",
                           ""
                        }
                    }
                });
            if (_dtLotItem != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = _dtLotItem,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    DataState = DataValueState.Added,
                    SpNameInsert = "UP_PR_WORK_ITEMLOT_INSERT",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_IO_202_102",
                                                    "NO_IO_203",
                                                    "NO_LINE_202",
                                                    "NO_LINE_102",
                                                    "NO_LINE_203",
                                                    "NO_IOLINE2",
                                                    "NO_LOT",
                                                    "NO_IO",
                                                    "NO_IOLINE",
                                                    "NO_IOLINE2",
                                                    "NO_LOT",
                                                    "CD_ITEM",
                                                    "DT_IO",
                                                    "CD_SL",
                                                    "QT_GOOD_MNG",
                                                    "YN_RETURN",
                                                    "NO_WO",
                                                    "NO_WORK",
                                                    "CD_OP",
                                                    "CD_WC",
                                                    "YN_FINAL",
                                                    "ID_INSERT" },
                    SpParamsValues = 
                    {
                        {
                          ActionState.Insert,
                          "YN_RETURN",
                           "N"
                        },
                        {
                          ActionState.Insert,
                          "YN_FINAL",
                           dtWork.Rows[0]["YN_FINAL"].ToString()
                        },
                        {
                          ActionState.Insert,
                          "NO_WORK",
                           dtWork.Rows[0]["NO_WORK"].ToString()
                        }
                    }
                });
            if (_dtMatl != null)
            {
                bool flag = false;
                string str = "";
                foreach (DataRow row in (InternalDataCollectionBase)_dtMatl.Rows)
                {
                    if (row["YN_BF"].ToString() == "Y")
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                    str = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "PR", "08", dtWork.Rows[0]["DT_WORK"].ToString().Substring(0, 6));
                spCollection.Add(new SpInfo()
                {
                    DataValue = _dtMatl,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    DataState = DataValueState.Added,
                    SpNameInsert = "UP_PR_WORK_MATL_INSERT",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_WO",
                                                    "NO_WORK",
                                                    "CD_OP",
                                                    "CD_WC",
                                                    "CD_WCOP",
                                                    "DT_WORK",
                                                    "CD_MATL",
                                                    "QT_INPUT",
                                                    "NO_IO_201",
                                                    "NO_IO_101",
                                                    "NO_IO_MM",
                                                    "NO_LINE",
                                                    "NO_REQ",
                                                    "CD_DEPT",
                                                    "NO_EMP",
                                                    "CD_SL",
                                                    "NO_II",
                                                    "ID_INSERT" },
                    SpParamsValues = 
                    {
                        {
                          ActionState.Insert,
                          "NO_WORK",
                           dtWork.Rows[0]["NO_WORK"].ToString()
                        },
                        {
                          ActionState.Insert,
                          "NO_REQ",
                           str
                        },
                        {
                          ActionState.Insert,
                          "CD_DEPT",
                           dtWork.Rows[0]["CD_DEPT"].ToString()
                        },
                        {
                          ActionState.Insert,
                          "NO_EMP",
                           dtWork.Rows[0]["NO_EMP"].ToString()
                        },
                        {
                          ActionState.Insert,
                          "NO_II",
                           strNO_II
                        }
                    }
                });
            }
            if (_dtLot != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = _dtLot,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    DataState = DataValueState.Added,
                    SpNameInsert = "UP_PR_WORK_QTIOLOT_INSERT",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_IO",
                                                    "NO_IOLINE",
                                                    "NO_IOLINE2",
                                                    "NO_IO_201",
                                                    "NO_IO_101",
                                                    "NO_IO_MM",
                                                    "NO_LINE",
                                                    "NO_LOT",
                                                    "CD_ITEM",
                                                    "DT_IO",
                                                    "CD_SL",
                                                    "QT_GOOD_MNG",
                                                    "YN_RETURN",
                                                    "NO_WO",
                                                    "NO_WORK",
                                                    "CD_OP",
                                                    "CD_WC",
                                                    "YN_BF",
                                                    "ID_INSERT" },
                    SpParamsValues = 
                    {
                        {
                          ActionState.Insert,
                          "YN_RETURN",
                           "N"
                        },
                        {
                          ActionState.Insert,
                          "NO_WO",
                           dtWork.Rows[0]["NO_WO"].ToString()
                        },
                        {
                          ActionState.Insert,
                          "NO_WORK",
                           dtWork.Rows[0]["NO_WORK"].ToString()
                        },
                        {
                          ActionState.Insert,
                          "CD_OP",
                           dtWork.Rows[0]["CD_OP"].ToString()
                        },
                        {
                          ActionState.Insert,
                          "CD_WC",
                           dtWork.Rows[0]["CD_WC"].ToString()
                        }
                    }
                });
            if (_dtSERL != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = _dtSERL,
                    SpNameInsert = "UP_PR_MM_QTIODS_INSERT",
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_WORK",
                                                    "NO_SERIAL",
                                                    "NO_IO",
                                                    "NO_IOLINE",
                                                    "CD_ITEM",
                                                    "CD_QTIOTP",
                                                    "FG_IO",
                                                    "CD_MNG1",
                                                    "CD_MNG2",
                                                    "CD_MNG3",
                                                    "CD_MNG4",
                                                    "CD_MNG5",
                                                    "CD_MNG6",
                                                    "CD_MNG7",
                                                    "CD_MNG8",
                                                    "CD_MNG9",
                                                    "CD_MNG10",
                                                    "CD_MNG11",
                                                    "CD_MNG12",
                                                    "CD_MNG13",
                                                    "CD_MNG14",
                                                    "CD_MNG15",
                                                    "CD_MNG16",
                                                    "CD_MNG17",
                                                    "CD_MNG18",
                                                    "CD_MNG19",
                                                    "CD_MNG20" },
                    SpParamsValues = 
                    {
                        {
                          ActionState.Insert,
                          "NO_WORK",
                           dtWork.Rows[0]["NO_WORK"].ToString()
                        }
                    }
                });
            if (dtReject != null && dtReject.Rows.Count > 0)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtReject,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    DataState = DataValueState.Added,
                    SpNameInsert = "UP_PR_WORKL_INSERT",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_WO",
                                                    "NO_WORK",
                                                    "NO_LINE",
                                                    "CD_REJECT",
                                                    "CD_RESOURCE",
                                                    "TM_WORK",
                                                    "QT_WORK",
                                                    "QT_REJECT",
                                                    "DC_RMK",
                                                    "NO_WOLINE",
                                                    "NO_SFT",
                                                    "ID_INSERT" },
                    SpParamsValues = 
                    {
                        {
                          ActionState.Insert,
                          "NO_WOLINE",
                           Convert.ToDecimal(Noline)
                        },
                        {
                          ActionState.Update,
                          "NO_WOLINE",
                           Convert.ToDecimal(Noline)
                        }
                    }
                });
            if (dt_Manday != null && dt_Manday.Rows.Count > 0)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dt_Manday,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.EmployeeNo,
                    DataState = DataValueState.Added,
                    SpNameInsert = "UP_PR_WORK_MANDAY_SUB_INSERT",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "CD_PLANT",
                                                    "NO_WORK",
                                                    "NO_MANDAY_LINE",
                                                    "NO_WO",
                                                    "NO_ROUT_LINE",
                                                    "CD_OP",
                                                    "CD_WCOP",
                                                    "CD_WC",
                                                    "QT_WORK",
                                                    "QT_MOVE",
                                                    "QT_BAD",
                                                    "QT_REWORK",
                                                    "NO_OPOUT_PO",
                                                    "NO_OPOUT_PO_LINE",
                                                    "YN_SUBCON",
                                                    "TM_READ",
                                                    "TM_WO_WAIT",
                                                    "TM_WORK",
                                                    "TM_MOVE",
                                                    "TM_WAIT",
                                                    "TM_WO_S",
                                                    "TM_WO_E",
                                                    "TM_WO_T",
                                                    "QT_WO_ROLL",
                                                    "CD_EQUIP",
                                                    "TM_EQ_S",
                                                    "TM_EQ_E",
                                                    "TM_EQ_T",
                                                    "QT_EQ_ROLL",
                                                    "DC_RMK",
                                                    "ID_INSERT",
                                                    "TM_PLAN_STOP",
                                                    "FG_RUNNING",
                                                    "TM_RUNNING",
                                                    "DT_WO_S",
                                                    "DT_WO_E",
                                                    "NM_USERDER1",
                                                    "NM_USERDER2",
                                                    "NM_USERDER3",
                                                    "NM_USERDER4",
                                                    "NM_USERDER5",
                                                    "NM_USERDER6",
                                                    "NM_USERDER7",
                                                    "NM_USERDER8",
                                                    "NM_USERDER9",
                                                    "NM_USERDER10",
                                                    "TM_BREAK" }
                });
            if (dt_Auto_Bad != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dt_Auto_Bad_Work;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.DataState = DataValueState.Added;
                spInfo.SpNameInsert = "UP_PR_BADWORK_REG_INSERT";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                        "NO_WORK",
                                                        "NO_WO",
                                                        "CD_PLANT",
                                                        "CD_OP",
                                                        "CD_ITEM",
                                                        "NO_EMP",
                                                        "DT_WORK",
                                                        "QT_WORK",
                                                        "QT_REJECT",
                                                        "CD_REJECT",
                                                        "QT_MOVE",
                                                        "YN_REWORK",
                                                        "YN_BAD_PROC",
                                                        "TM_LABOR",
                                                        "CD_RSRC_LABOR",
                                                        "TM_MACH",
                                                        "CD_RSRC_MACH",
                                                        "CD_WC",
                                                        "DC_REJECT",
                                                        "ID_INSERT",
                                                        "FG_MOVE",
                                                        "TP_REWORK",
                                                        "CD_REWORK",
                                                        "NO_LOT",
                                                        "NO_SFT",
                                                        "CD_EQUIP",
                                                        "NO_IO_202_102",
                                                        "NO_IO_203",
                                                        "NO_LINE_202",
                                                        "NO_LINE_102",
                                                        "NO_LINE_203",
                                                        "YN_SUBCON",
                                                        "NO_PO",
                                                        "NO_POLINE",
                                                        "NO_REL",
                                                        "CD_MNG1",
                                                        "CD_MNG2",
                                                        "CD_MNG3",
                                                        "CD_MNG4",
                                                        "CD_MNG5",
                                                        "CD_MNG6",
                                                        "CD_MNG7",
                                                        "CD_MNG8",
                                                        "CD_MNG9",
                                                        "CD_MNG10",
                                                        "CD_MNG11",
                                                        "CD_MNG12",
                                                        "CD_MNG13",
                                                        "CD_MNG14",
                                                        "CD_MNG15",
                                                        "CD_MNG16",
                                                        "CD_MNG17",
                                                        "CD_MNG18",
                                                        "CD_MNG19",
                                                        "CD_MNG20" };
                if (!dt_Auto_Bad_Work.Columns.Contains("NO_REL"))
                    spInfo.SpParamsValues.Add(ActionState.Insert, "NO_REL", dtWork.Rows[0]["NO_REL"].ToString());
                spCollection.Add(spInfo);
                spCollection.Add(new SpInfo()
                {
                    DataValue = dt_Auto_Bad,
                    CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                    UserID = Global.MainFrame.LoginInfo.UserID,
                    DataState = DataValueState.Added,
                    SpNameInsert = "UP_PR_WORK_BAD_INSERT",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_WORK",
                                                    "NO_WORK_ORIGIN",
                                                    "NO_LINE",
                                                    "NO_WO",
                                                    "QT_REJECT",
                                                    "ID_INSERT" },
                    SpParamsValues = 
                    {
                        {
                          ActionState.Insert,
                          "NO_WORK_ORIGIN",
                           dtWork.Rows[0]["NO_WORK"].ToString()
                        }
                    }
                });
                if (dt_AutoBad_ReqH != null)
                    spCollection.Add(new SpInfo()
                    {
                        DataValue = dt_AutoBad_ReqH,
                        CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                        UserID = Global.MainFrame.LoginInfo.UserID,
                        DataState = DataValueState.Added,
                        SpNameInsert = "UP_PR_WORK_BAD_AUTO_H_INSERT",
                        SpParamsInsert = new string[] { "CD_COMPANY",
                                                        "CD_PLANT",
                                                        "NO_REQ",
                                                        "DT_REQ",
                                                        "NO_EMP",
                                                        "DC_RMK",
                                                        "CD_DEPT",
                                                        "NO_IO",
                                                        "YN_AUTOBAD_RCV",
                                                        "ID_INSERT" }
                    });
                if (dt_AutoBad_ReqL != null)
                    spCollection.Add(new SpInfo()
                    {
                        DataValue = dt_AutoBad_ReqL,
                        CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                        UserID = Global.MainFrame.LoginInfo.UserID,
                        DataState = DataValueState.Added,
                        SpNameInsert = "UP_PR_WORK_BAD_AUTO_L_INSERT",
                        SpParamsInsert = new string[] { "CD_COMPANY",
                                                        "CD_PLANT",
                                                        "NO_REQ",
                                                        "NO_LINE",
                                                        "CD_WC",
                                                        "CD_ITEM",
                                                        "DT_REQ",
                                                        "QT_REQ",
                                                        "QT_REQ_W",
                                                        "QT_REQ_B",
                                                        "YN_QC",
                                                        "QT_RCV",
                                                        "CD_SL",
                                                        "NO_WO",
                                                        "NO_WORK",
                                                        "TP_WB",
                                                        "TP_GR",
                                                        "NO_IO",
                                                        "YN_AUTOBAD_RCV",
                                                        "NO_EMP",
                                                        "NO_LOT",
                                                        "NO_IO_203",
                                                        "NO_LINE_203",
                                                        "DT_LIMIT",
                                                        "CD_MNG1",
                                                        "CD_MNG2",
                                                        "CD_MNG3",
                                                        "CD_MNG4",
                                                        "CD_MNG5",
                                                        "CD_MNG6",
                                                        "CD_MNG7",
                                                        "CD_MNG8",
                                                        "CD_MNG9",
                                                        "CD_MNG10",
                                                        "CD_MNG11",
                                                        "CD_MNG12",
                                                        "CD_MNG13",
                                                        "CD_MNG14",
                                                        "CD_MNG15",
                                                        "CD_MNG16",
                                                        "CD_MNG17",
                                                        "CD_MNG18",
                                                        "CD_MNG19",
                                                        "CD_MNG20" }
                    });
                if (_dtInsp != null)
                    spCollection.Add(new SpInfo()
					{
                        DataValue = _dtInsp,
                        CompanyID = Global.MainFrame.LoginInfo.CompanyCode,
                        UserID = Global.MainFrame.LoginInfo.UserID,
                        SpNameUpdate = "SP_CZ_PR_OPOUT_WORK_INSP_U",
                        SpParamsUpdate = new string[] { "CD_COMPANY",
                                                        "NO_WO",
                                                        "NO_LINE",
                                                        "SEQ_WO",
                                                        "NO_INSP",
                                                        "NO_HEAT",
                                                        "USER_ID",
                                                        "NO_OPOUT_WORK",
                                                        "CD_REJECT",
                                                        "CD_RESOURCE" }
                    });
            }
            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spCollection))
            {
                if (!resultData.Result)
                    return false;
            }
            return true;
        }

        public DataTable print(string 공장코드, string 작지번호) => (DataTable)((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
        {
            SpNameSelect = "UP_PR_WORK_PRINT_SELECT",
            SpParamsSelect = new object[]
            {
                Global.MainFrame.LoginInfo.CompanyCode,
                공장코드,
                작지번호,
                Global.SystemLanguage.MultiLanguageLpoint
            }
        })).DataValue;

        public DataSet Search_AUTH(object[] obj) => DBHelper.GetDataSet("UP_MA_MFG_AUTH_SELECT", obj);

        public DataSet Search_AutoBad_Req(object[] obj)
        {
            DataSet dataSet = DBHelper.GetDataSet("UP_PR_ITEMRCV_SELECT", obj);
            foreach (DataTable table in (InternalDataCollectionBase)dataSet.Tables)
            {
                foreach (DataColumn column in (InternalDataCollectionBase)table.Columns)
                {
                    if (column.DataType == typeof(decimal))
                        column.DefaultValue = 0;
                }
            }
            return dataSet;
        }

        public DataSet Search_PlantSetting(object[] obj) => DBHelper.GetDataSet("UP_PR_CFG_PLANT_SELECT", obj);
    }
}

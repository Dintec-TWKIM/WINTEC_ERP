using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

// ★ 생산품자동입고의뢰처리 i01
namespace prd
{
    public class P_PR_WO_REG_NEW_BIZ
    {
        #region -> 툴바조회

        public DataSet Search(string 공장코드, string 작업지시번호)
        {
            ResultData rtn = (ResultData)Global.MainFrame.FillDataSet("UP_PR_WO_REG_NEW_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 공장코드, 작업지시번호 });
            DataSet ds = (DataSet)rtn.DataValue;

            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn Col in dt.Columns)
                {
                    if (Col.DataType == Type.GetType("System.Decimal"))
                        Col.DefaultValue = 0;
                }
            }

            // PR_WO 디폴트값 셋팅
            ds.Tables[0].Columns["CD_PLANT"].DefaultValue = 공장코드;
            ds.Tables[0].Columns["FG_WO"].DefaultValue = "003";
            ds.Tables[0].Columns["ST_WO"].DefaultValue = "O";
            ds.Tables[0].Columns["TP_ROUT"].DefaultValue = "1";
            ds.Tables[0].Columns["DT_REL"].DefaultValue = Global.MainFrame.GetStringToday;
            ds.Tables[0].Columns["DT_DUE"].DefaultValue = Global.MainFrame.GetStringToday;
            ds.Tables[0].Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            ds.Tables[0].Columns["YN_AUTOREQ"].DefaultValue = "N";
            ds.Tables[0].Columns["YN_AUTORCV"].DefaultValue = "N";
            ds.Tables[0].Columns["YN_CTRL_QTIO"].DefaultValue = "N";
            ds.Tables[0].Columns["YN_AUTOPO"].DefaultValue = "N";
            ds.Tables[0].Columns["YN_AUTO_CLS"].DefaultValue = "N";
            ds.Tables[0].Columns["YN_AUTORCV_REQ"].DefaultValue = "N";  //★ i01

            // 경로정보 디폴트값 셋팅
            if (!ds.Tables[1].Columns.Contains("S"))
                ds.Tables[1].Columns.Add("S", typeof(string));

            ds.Tables[1].Columns["S"].DefaultValue = "N";
            ds.Tables[1].Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            ds.Tables[1].Columns["YN_BF"].DefaultValue = "N";
            ds.Tables[1].Columns["ST_OP"].DefaultValue = "O";
            ds.Tables[1].Columns["YN_QC"].DefaultValue = "N";
            ds.Tables[1].Columns["YN_FINAL"].DefaultValue = "N";
            ds.Tables[1].Columns["YN_SUBCON"].DefaultValue = "N";

            // 소요자재 디폴트값 셋팅
            if (!ds.Tables[2].Columns.Contains("S"))
                ds.Tables[2].Columns.Add("S", typeof(string));

            ds.Tables[2].Columns["S"].DefaultValue = "N";
            ds.Tables[2].Columns["DT_NEED"].DefaultValue = Global.MainFrame.GetStringToday;
            ds.Tables[2].Columns["FG_GIR"].DefaultValue = "Y";
            ds.Tables[2].Columns["YN_BF"].DefaultValue = "N";

            ds.Tables[2].Columns.Add("ROW_COLOR", typeof(string));

            string s_대체품적용여부 = isReplace(공장코드);

            if (ds.Tables[0].Rows.Count > 0 && 작업지시번호 != string.Empty && s_대체품적용여부 == "Y")
            {
                string s_Multi_Matl = Duzon.ERPU.MF.Common.Common.MultiString(ds.Tables[2], "CD_MATL", "|");

                object[] obj_Replace = new object[]
                {
                    Global.MainFrame.LoginInfo.CompanyCode,
                    공장코드,
                    D.GetString(ds.Tables[0].Rows[0]["DT_REL"]).Substring(0, 4) + "0101",
                    D.GetString(ds.Tables[0].Rows[0]["DT_REL"]),
                    s_Multi_Matl,
                    string.Empty,
                    "Y"
                };

                DataTable dtD = GetATPSearch(obj_Replace);

                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    DataRow[] drs = dtD.Select("CD_ITEM = '" + D.GetString(dr["CD_MATL"]) + "' AND CD_ITEM_O <> '" + D.GetString(dr["CD_MATL"]) + "'");

                    if (drs.Length == 0)
                    {
                        dr["ROW_COLOR"] = "W";
                        continue;
                    }

                    decimal d_ISU_CAN_SO = D.GetDecimal(drs[0]["ISU_CAN_SO"]);

                    if (d_ISU_CAN_SO > 0)
                        dr["ROW_COLOR"] = "Y";
                    else if (d_ISU_CAN_SO == 0)
                        dr["ROW_COLOR"] = "W";
                    else
                        dr["ROW_COLOR"] = "B";
                }

                ds.Tables[2].AcceptChanges();
            }

            return ds;
        }

        #endregion

        #region -> 툴바삭제

        public void Delete(string 공장코드, string 작업지시번호)
        {
            Global.MainFrame.ExecSp("UP_PR_WO_REG_NEW_DELETE", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 공장코드, 작업지시번호 });
        }

        #endregion

        #region -> 작업품목에 대한 경로유형, 리드타임 조회

        public object[] 경로유형(string 공장코드, string 품목코드, string DtFrom)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PR_ROUT_SELECT";
            si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, 공장코드, 품목코드, DtFrom };
            ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);

            object[] Return = new object[] { (DataTable)rtn.DataValue, rtn.OutParamsSelect[0, 0].ToString() };
            return Return;
        }

        #endregion

        #region -> 공정경로셋팅

        public DataTable getOPPathSetting(object[] obj)
        {
            string selectQuery = string.Empty;

            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
            {
                selectQuery = "  SELECT  NO_OPPATH AS CODE, NO_OPPATH + '-' + DC_OPPATH AS NAME " +
                                 "    FROM  PR_ROUT " +
                                 "   WHERE  CD_COMPANY = '" + obj[0].ToString() + "'" +
                                 "     AND  CD_PLANT = '" + obj[0].ToString() + "'" +
                                 "     AND  CD_ITEM  = '" + obj[0].ToString() + "'";
            }
            else if (Global.MainFrame.DatabaseType == EnumDbType.ORACLE)
            {
                selectQuery = "  SELECT  NO_OPPATH AS CODE, NO_OPPATH || '-' || DC_OPPATH AS NAME " +
                                 "    FROM  PR_ROUT " +
                                 "   WHERE  CD_COMPANY = '" + obj[0].ToString() + "'" +
                                 "     AND  CD_PLANT = '" + obj[0].ToString() + "'" +
                                 "     AND  CD_ITEM  = '" + obj[0].ToString() + "'";
            }
            return DBHelper.GetDataTable(selectQuery);
        }

        #endregion

        #region -> 경로재전개

        public DataTable 경로재전개(string 공장코드, string 품목코드, string 경로유형)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PR_ROUT_L_LIST";
            si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, 공장코드, 품목코드, 경로유형, "", "" };
            ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);
            return (DataTable)rtn.DataValue;
        }

        public DataTable PR_ROUT_MAX_LIST(string 공장코드, string 품목코드, string 경로유형)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PR_ROUT_MAX_LIST";
            si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, 공장코드, 품목코드, 경로유형, "", "" };
            ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);
            return (DataTable)rtn.DataValue;
        }
        
        #endregion
        
        #region -> 표준경로가아닌소요량재전개
        public DataTable ReSearch_Material(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_PR_ROUT_ASN_LIST", obj);

            dt.Columns.Add("ROW_COLOR", typeof(string));

            string s_대체품적용여부 = isReplace(D.GetString(obj[1]));

            if (s_대체품적용여부 == "Y")
            {
                string s_Multi_Matl = Duzon.ERPU.MF.Common.Common.MultiString(dt, "CD_MATL", "|");

                object[] obj_Replace = new object[]
                {
                    Global.MainFrame.LoginInfo.CompanyCode,
                    obj[1],
                    D.GetString(obj[5]).Substring(0, 4) + "0101",
                    obj[5],
                    s_Multi_Matl,
                    string.Empty,
                    "Y"
                };

                DataTable dtD = GetATPSearch(obj_Replace);

                foreach (DataRow dr in dt.Rows)
                {
                    DataRow[] drs = dtD.Select("CD_ITEM = '" + D.GetString(dr["CD_MATL"]) + "'");

                    if (drs.Length == 0)
                    {
                        dr["ROW_COLOR"] = "W";
                        continue;
                    }

                    string s_NO_GRP = D.GetString(drs[0]["NO_GRP"]);
                    string s_CD_ITEM = D.GetString(drs[0]["CD_ITEM"]);
                    string s_Filter = "NO_GRP = '" + s_NO_GRP + "' AND CD_ITEM = '" + s_CD_ITEM + "'";

                    decimal d_ISU_CAN_SO = D.GetDecimal(drs[0]["ISU_CAN_SO"]);

                    if (d_ISU_CAN_SO > 0)
                        dr["ROW_COLOR"] = "Y";
                    else if (d_ISU_CAN_SO == 0)
                        dr["ROW_COLOR"] = "W";
                    else
                        dr["ROW_COLOR"] = "B";
                }

                dt.AcceptChanges();
            }

            return dt;
        }
        #endregion

        #region -> ReSearch_Opt_Material
        public DataTable ReSearch_Opt_Material(object[] obj)
        {
            return DBHelper.GetDataTable("UP_PR_ROUT_OPT_LIST", obj);
        }
        #endregion

        #region ReSearch_Formula
        public DataTable ReSearch_Formula(object[] obj)
        {
            string CD_EXC = Duzon.ERPU.MF.Common.BASIC.GetMAEXC("작업지시등록-배합표별OP번호적용");

            DataTable dt = new DataTable();

            if (CD_EXC == "100")
                dt = DBHelper.GetDataTable("UP_PR_ROUT_FORMULA_LIST_100", obj);

            else
                dt = DBHelper.GetDataTable("UP_PR_ROUT_FORMULA_LIST", obj);

            if (!dt.Columns.Contains("ROW_COLOR"))
                dt.Columns.Add("ROW_COLOR", typeof(string));

            return dt;
        }
        #endregion

        #region Search_Formula_List
        public DataTable Search_Formula_List(object[] obj)
        {
            return DBHelper.GetDataTable("UP_PR_WO_FORMULA_LIST", obj);
        }
        #endregion

        #region PR_MATL_MAX_LIST
        public DataTable PR_MATL_MAX_LIST(object[] obj)
        {
            return DBHelper.GetDataTable("UP_PR_MATL_MAX_LIST", obj);
        }
        #endregion

        #region -> 저장

        public bool Save(DataTable dtH, DataTable dt01, DataTable dt02, DataTable dt03, DataTable dt04, DataRow drHeader, DataTable dt_Release, bool 복사여부)
        {
            SpInfoCollection sc = new SpInfoCollection();
            SpInfo si = null;

            if (dtH != null)
            {
                si = new SpInfo();

                if (복사여부)
                    si.DataState = DataValueState.Added;

                si.DataValue = dtH;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameInsert = "UP_PR_WO_INSERT";
                si.SpNameUpdate = "UP_PR_WO_UPDATE";
                si.SpNameDelete = "UP_PR_WO_DELETE";
                si.SpParamsInsert = new string[] {  "CD_COMPANY",   "NO_WO",            "CD_PLANT",     "NO_EMP",           "CD_ITEM", 
                                                    "QT_ITEM",      "FG_WO",            "ST_WO",        "PATN_ROUT",        "TP_ROUT", 
                                                    "NO_LOT",       "NO_SO",            "NO_PJT",       "DT_REL",           "DT_DUE", 
                                                    "QT_WORK",      "NO_LINE_SO",       "DT_CLOSE",     "TP_GI",            "TP_GR", 
                                                    "DT_RELEASE",   "YN_AUTOREQ",       "YN_AUTORCV",   "YN_CTRL_QTIO",     "YN_AUTOPO", 
                                                    "YN_AUTO_CLS",  "FG_TPPURCHASE",    "DC_RMK",       "YN_AUTORCV_REQ",   "QT_LOSS", 
                                                    "CD_PARTNER",   "NO_SOURCE",        "NO_EMP_DESIGN","DC_RMK_CONTENT",   "FG_WO_ST",    
                                                    "DC_REV",       "ID_INSERT",        "DT_DESIGN",    "DT_INSERT",        "DC_RMK2",    
                                                    "DC_RMK3",      "NO_FR",            "QT_BATCH_SIZE","DT_LIMIT",         "CD_USERDEF1",
                                                    "CD_USERDEF2",  "CD_USERDEF3",      "CD_USERDEF4",  "NUM_USERDEF1",     "NUM_USERDEF2",
                                                    "CD_PACKUNIT" };

                si.SpParamsUpdate = new string[] {  "CD_COMPANY",   "NO_WO",            "CD_PLANT",     "NO_EMP",           "CD_ITEM", 
                                                    "QT_ITEM",      "FG_WO",            "ST_WO",        "PATN_ROUT",        "TP_ROUT", 
                                                    "NO_LOT",       "NO_SO",            "NO_PJT",       "DT_REL",           "DT_DUE", 
                                                    "QT_WORK",      "NO_LINE_SO",       "DT_CLOSE",     "TP_GI",            "TP_GR", 
                                                    "DT_RELEASE",   "YN_AUTOREQ",       "YN_AUTORCV",   "YN_CTRL_QTIO",     "YN_AUTOPO", 
                                                    "YN_AUTO_CLS",  "FG_TPPURCHASE",    "DC_RMK",       "YN_AUTORCV_REQ",   "QT_LOSS", 
                                                    "CD_PARTNER",   "NO_SOURCE",        "NO_EMP_DESIGN","DC_RMK_CONTENT",   "FG_WO_ST",    
                                                    "DC_REV",       "ID_UPDATE",        "DT_DESIGN",    "DT_INSERT",        "DC_RMK2",    
                                                    "DC_RMK3",      "NO_FR",            "QT_BATCH_SIZE","DT_LIMIT",         "CD_USERDEF1",
                                                    "CD_USERDEF2",  "CD_USERDEF3",      "CD_USERDEF4",  "NUM_USERDEF1",     "NUM_USERDEF2",
                                                    "CD_PACKUNIT" };

                si.SpParamsDelete = new string[] {  "CD_COMPANY",   "NO_WO",            "CD_PLANT" };
                sc.Add(si);
            }

            if (dt01 != null)
            {
                si = new SpInfo();
                si.DataValue = dt01;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameInsert = "UP_PR_WO_ROUT_INSERT";//동일 SP 사용 화면 : 일괄지시등록, 작업일정계획
                si.SpNameUpdate = "UP_PR_WO_ROUT_U1"; // UPDATE 프로시저에 절대로 = NULL 추가하지 말 것
                si.SpNameDelete = "UP_PR_WO_ROUT_DELETE";//동일 SP 사용 화면 : 일괄지시등록, 작업일정계획
                si.SpParamsInsert = new string[] {  "CD_COMPANY",   "NO_WO",        "NO_LINE",      "CD_OP",        "CD_PLANT", 
                                                    "CD_WC",        "CD_WCOP",      "DT_REL",       "DT_DUE",       "ST_OP", 
                                                    "FG_WC",        "YN_WORK",      "QT_WO",        "QT_WIP",       "QT_WORK", 
                                                    "QT_REJECT",    "QT_REWORK",    "QT_MOVE",      "TM_SETUP",     "CD_RSRC1", 
                                                    "TM_LABOR",     "CD_RSRC2",     "TM_MACH",      "TM_MOVE",      "DY_SUBCON", 
                                                    "TM_LABOR_ACT", "TM_MACH_ACT",  "CD_TOOL",      "TM_REL",       "TM_DUE", 
                                                    "YN_BF",        "YN_RECEIPT",   "TM",           "YN_PAR",       "YN_QC", 
                                                    "QT_CLS",       "CD_OP_BASE",   "YN_FINAL",     "QT_START",     "QT_OUTPO", 
                                                    "QT_RCV",       "DT_CLOSE",     "YN_SUBCON",    "ID_INSERT",    "DC_RMK",   
                                                    "NO_SFT",       "DC_RMK_1",     "CD_EQUIP",     "RT_YIELD",     "YN_ROUT_SU_IV",
                                                    "CD_POST",      "CD_USERDEF1",  "CD_USERDEF2",  "CD_USERDEF3",  "NUM_USERDEF1",
                                                    "NUM_USERDEF2", "NUM_USERDEF3", "TXT_USERDEF1", "TXT_USERDEF2", "TXT_USERDEF3" };
                si.SpParamsUpdate = new string[] {  "CD_COMPANY",   "NO_WO",        "NO_LINE",      "CD_OP",        "CD_PLANT", 
                                                    "CD_WC",        "CD_WCOP",      "DT_REL",       "DT_DUE",       "ST_OP", 
                                                    "FG_WC",        "YN_WORK",      "QT_WO",        "QT_WIP",       "QT_WORK", 
                                                    "QT_REJECT",    "QT_REWORK",    "QT_MOVE",      "TM_SETUP",     "CD_RSRC1", 
                                                    "TM_LABOR",     "CD_RSRC2",     "TM_MACH",      "TM_MOVE",      "DY_SUBCON", 
                                                    "TM_LABOR_ACT", "TM_MACH_ACT",  "CD_TOOL",      "TM_REL",       "TM_DUE", 
                                                    "YN_BF",        "YN_RECEIPT",   "TM",           "YN_PAR",       "YN_QC", 
                                                    "QT_CLS",       "CD_OP_BASE",   "YN_FINAL",     "QT_START",     "QT_OUTPO", 
                                                    "QT_RCV",       "DT_CLOSE",     "YN_SUBCON",    "ID_UPDATE",    "DC_RMK",   
                                                    "NO_SFT",       "DC_RMK_1",     "CD_EQUIP",     "RT_YIELD",     "YN_ROUT_SU_IV",
                                                    "CD_POST",      "CD_USERDEF1",  "CD_USERDEF2",  "CD_USERDEF3",  "NUM_USERDEF1",
                                                    "NUM_USERDEF2", "NUM_USERDEF3", "TXT_USERDEF1", "TXT_USERDEF2", "TXT_USERDEF3" };
                si.SpParamsDelete = new string[] {  "CD_COMPANY",   "NO_WO",        "NO_LINE" };
                sc.Add(si);
            }

            if (dt02 != null)
            {
                si = new SpInfo();
                si.DataValue = dt02;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameInsert = "UP_PR_WO_BILL_INSERT";//동일 SP 사용 화면 : 일괄지시등록, 작업지시등록 
                si.SpNameUpdate = "UP_PR_WO_BILL_UPDATE";//동일 SP 사용 화면 : 일괄지시등록, 작업지시등록 
                si.SpNameDelete = "UP_PR_WO_BILL_DELETE";//동일 SP 사용 화면 : 일괄지시등록, 작업지시등록
                si.SpParamsInsert = new string[] {  "CD_COMPANY",   "NO_WO",        "NO_LINE",      "CD_PLANT",         "CD_OP", 
                                                    "CD_MATL",      "CD_WC",        "DT_NEED",      "FG_NEED",          "QT_NEED", 
                                                    "QT_REQ",       "QT_ISU",       "QT_USE",       "QT_RTN",           "NO_REQ", 
                                                    "YN_BF",        "QT_NEED_NET",  "CD_WCOP",      "QT_REQ_RETURN",    "QT_TRN", 
                                                    "FG_GIR",       "ID_INSERT",    "CD_SL_IN",     "CD_SL_OT",         "DC_RMK",
                                                    "QT_ADJUST",    "NO_SORT",      "NO_LOT",       "CD_TUIP_GROUP",    "NO_TUIP_SEQ",
                                                    "QT_NEED_BOM",	"RT_SCRAP",     "CD_MATL_O",    "NO_LINE_O",        "QT_NEED_O",
                                                    "YN_REPLACE"};
                si.SpParamsUpdate = new string[] {  "CD_COMPANY",   "NO_WO",        "NO_LINE",      "CD_PLANT",         "CD_OP", 
                                                    "CD_MATL",      "CD_WC",        "DT_NEED",      "FG_NEED",          "QT_NEED", 
                                                    "QT_REQ",       "QT_ISU",       "QT_USE",       "QT_RTN",           "NO_REQ", 
                                                    "YN_BF",        "QT_NEED_NET",  "CD_WCOP",      "QT_REQ_RETURN",    "QT_TRN", 
                                                    "FG_GIR",       "ID_INSERT",    "CD_SL_IN",     "CD_SL_OT",         "DC_RMK", 
                                                    "QT_ADJUST",    "NO_SORT",      "NO_LOT",       "CD_TUIP_GROUP",    "NO_TUIP_SEQ",
                                                    "QT_NEED_BOM",	"RT_SCRAP",     "CD_MATL_O",    "NO_LINE_O",        "QT_NEED_O",
                                                    "YN_REPLACE"};
                si.SpParamsDelete = new string[] {  "CD_COMPANY",   "NO_WO",        "NO_LINE" };
                sc.Add(si);
            }

            if (dt03 != null)
            {
                si = new SpInfo();
                si.DataValue = dt03;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameInsert = "UP_PR_SA_SOL_PR_WO_MAPPING_I";
                si.SpNameUpdate = "UP_PR_SA_SOL_PR_WO_MAPPING_U";
                si.SpNameDelete = "UP_PR_SA_SOL_PR_WO_MAPPING_D";
                si.SpParamsInsert = new string[] { "CD_COMPANY", "CD_PLANT", "NO_SO", "SEQ_SO", "NO_WO_I", "CD_ITEM", "QT_APPLY", "ID_INSERT" };
                si.SpParamsUpdate = new string[] { "CD_COMPANY", "CD_PLANT", "NO_SO", "SEQ_SO", "NO_WO_I", "CD_ITEM", "QT_APPLY", "ID_UPDATE" };
                si.SpParamsDelete = new string[] { "CD_COMPANY", "CD_PLANT", "NO_SO", "SEQ_SO", "NO_WO_I" };
                si.SpParamsValues.Add(ActionState.Insert, "NO_WO_I", drHeader["NO_WO"].ToString());
                si.SpParamsValues.Add(ActionState.Update, "NO_WO_I", drHeader["NO_WO"].ToString());
                si.SpParamsValues.Add(ActionState.Delete, "NO_WO_I", drHeader["NO_WO"].ToString());
                sc.Add(si);
            }

            if (dt04 != null)
            {
                si = new SpInfo();
                si.DataValue = dt04;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameInsert = "UP_PR_PRQ_WO_LINK_INSERT";
                si.SpNameUpdate = "UP_PR_PRQ_WO_LINK_UPDATE";
                si.SpNameDelete = "UP_PR_PRQ_WO_LINK_DELETE";
                si.SpParamsInsert = new string[] {  "CD_COMPANY",   "CD_PLANT",     "NO_PRQ",   "NO_PRQ_LINE",  "NO_WO_I", 
                                                    "DT_DLV",       "CD_ITEM",      "QT_ACT",   "QT_APPLY",     "QT_WORK", 
                                                    "NO_PRQD",		"NO_PRQD_LINE",	"CD_PJT",	"CD_PJT_SEQ",	"NO_SO", 
                                                    "NO_SO_LINE",   "ID_INSERT" };
                si.SpParamsUpdate = new string[] {  "CD_COMPANY",   "CD_PLANT",     "NO_PRQ",   "NO_PRQ_LINE",  "NO_WO_I", 
                                                    "DT_DLV",       "CD_ITEM",      "QT_ACT",   "QT_APPLY",     "QT_WORK", 
                                                    "NO_PRQD",		"NO_PRQD_LINE",	"CD_PJT",	"CD_PJT_SEQ",	"NO_SO", 
                                                    "NO_SO_LINE",   "ID_UPDATE" };
                si.SpParamsDelete = new string[] {  "CD_COMPANY",   "CD_PLANT",     "NO_PRQ",   "NO_PRQ_LINE",  "NO_WO_I", 
                                                    "NO_PRQD",		"NO_PRQD_LINE"  };
                si.SpParamsValues.Add(ActionState.Insert, "NO_WO_I", drHeader["NO_WO"].ToString());
                si.SpParamsValues.Add(ActionState.Update, "NO_WO_I", drHeader["NO_WO"].ToString());
                si.SpParamsValues.Add(ActionState.Delete, "NO_WO_I", drHeader["NO_WO"].ToString());
                sc.Add(si);
            }

            if (dt_Release != null)
            {
                si = new SpInfo();
                si.DataValue = dt_Release;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                //INSERT만 타면 돼는데 일단 다 넣어주었음 : 업데이트시 수량 바뀌면 어쩔껀디...
                si.SpNameInsert = "UP_PR_WO_REL_INSERT";
                si.SpNameUpdate = "UP_PR_WO_REL_INSERT";
                si.SpParamsInsert = new string[] { "CD_COMPANY", "CD_PLANT", "NO_WO", "ST_WO", "DT_REL", "QT_ITEM", "ID_INSERT", "NO_LOT","DC_RMK" };
                si.SpParamsUpdate = new string[] { "CD_COMPANY", "CD_PLANT", "NO_WO", "ST_WO", "DT_REL", "QT_ITEM", "ID_INSERT", "NO_LOT","DC_RMK" };

                sc.Add(si);
            }

            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sc);
            for (int i = 0; i < rtn.Length; i++)
            {
                if (!rtn[i].Result) return false;
            }

            return true;
        }

        #endregion

        #region -> 작업지시구분이 MRP 일때 상태값 확정 버튼이 활성화 되고 이때 계획을 확정 해줄때 상태값 업데이트 시켜준다. 
        //(작업지시관리의 계획 작업지시확정 버튼 프로시져 그대로 재사용함)
        public void getST_WO_update(object[] obj)
        {
            DBHelper.ExecuteNonQuery("UP_PR_WO_PLANCONFIRM", obj);
        } 
        #endregion

        #region -> 현재고 가져오기
        internal DataTable get_QtInv(object[] obj)
        {
            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
            {
                return DBHelper.GetDataTable(@"   SELECT	(MV.QT_GOOD_OPEN + MV.QT_REJECT_OPEN + MV.QT_INSP_OPEN + MV.QT_TRANS_OPEN) +  
		                                                    (MV.QT_GOOD_GR + MV.QT_REJECT_GR + MV.QT_INSP_GR + MV.QT_TRANS_GR) -   
		                                                    (MV.QT_GOOD_GI + MV.QT_REJECT_GI + MV.QT_INSP_GI + MV.QT_TRANS_GI) AS QT_INV, MI.CD_ZONE 
                                                      FROM	MA_PITEM MI INNER JOIN MM_PINVN MV ON MV.CD_COMPANY = MI.CD_COMPANY AND MV.CD_PLANT = MI.CD_PLANT AND MV.CD_ITEM = MI.CD_ITEM AND  MV.P_YR = CONVERT(NVARCHAR(4),GETDATE(),112) 
                                                 WHERE  MI.CD_COMPANY = '" + obj[0].ToString() + "'" +
                                                "  AND  MI.CD_PLANT = '" + obj[1].ToString() + "'" +
                                                "  AND  MI.CD_ITEM = '" + obj[2].ToString() + "'" +
                                                "  AND  MV.CD_SL = '" + obj[3].ToString() + "'");
            }
            else
            {
                return DBHelper.GetDataTable(@"   SELECT	(MV.QT_GOOD_OPEN + MV.QT_REJECT_OPEN + MV.QT_INSP_OPEN + MV.QT_TRANS_OPEN) +  
		                                                    (MV.QT_GOOD_GR + MV.QT_REJECT_GR + MV.QT_INSP_GR + MV.QT_TRANS_GR) -   
		                                                    (MV.QT_GOOD_GI + MV.QT_REJECT_GI + MV.QT_INSP_GI + MV.QT_TRANS_GI) AS QT_INV, MI.CD_ZONE 
                                                      FROM	MA_PITEM MI INNER JOIN MM_PINVN MV ON MV.CD_COMPANY = MI.CD_COMPANY AND MV.CD_PLANT = MI.CD_PLANT AND MV.CD_ITEM = MI.CD_ITEM AND  MV.P_YR = TO_CHAR(SYSDATE,'YYYY') 
                                                 WHERE  MI.CD_COMPANY = '" + obj[0].ToString() + "'" +
                                                "  AND  MI.CD_PLANT = '" + obj[1].ToString() + "'" +
                                                "  AND  MI.CD_ITEM = '" + obj[2].ToString() + "'" +
                                                "  AND  MV.CD_SL = '" + obj[3].ToString() + "'");
            }
        }
        #endregion

        #region -> BOM과 소요자재 Check 불일치 여부 체크

        public DataSet SearchCfg(string sPlant)
        {
            ResultData rtn = (ResultData)Global.MainFrame.FillDataSet("UP_PR_CFG_PLANT_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode, sPlant });
            DataSet ds = (DataSet)rtn.DataValue;
            return ds;
        }

        //public DataTable BomChk(string sPlant, string sItem, string sRout)
        //{
        //    return DBHelper.GetDataTable("UP_PR_WO_REG02_BOMCHK", new object[] { Global.MainFrame.LoginInfo.CompanyCode, sPlant, sItem, sRout });
        //}

        public DataTable BomChk(object[] obj)
        {
            return DBHelper.GetDataTable("UP_PR_WO_REG02_BOMCHK", obj);
        }
        
        #endregion

        #region Get_SOLIDTECH_SN
        internal DataTable Get_SOLIDTECH_SN(object[] obj)
        {
            return DBHelper.GetDataTable("UP_PR_WO_SOLIDTECH_SN", obj);
        }
        #endregion

        #region Get_Pr_TpWo_Std_Rout(오더형태별기준공정경로등록의 기준 공정유형을 조회)
        /// /// <summary>
        /// 오더형태별기준공정경로등록의 기준 공정유형을 조회함
        /// </summary>
        /// <param name="obj">[0]공장, [1]품목, [2]오더형태</param>
        /// <returns></returns>
        public string Get_Pr_TpWo_Std_Rout(object[] obj)
        {
            DataTable dt = null;

            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
            {
                dt = DBHelper.GetDataTable(@"SELECT R.NO_OPPATH AS CODE, R.NO_OPPATH + '-' +  T.DC_OPPATH AS NAME, T.TP_OPPATH "
                                             + " FROM PR_TPWO_ROUT R "
                                             + " LEFT JOIN PR_ROUT T ON R.CD_COMPANY = T.CD_COMPANY AND R.CD_PLANT = T.CD_PLANT AND R.CD_ITEM = T.CD_ITEM AND R.NO_OPPATH = T.NO_OPPATH "
                                             + " WHERE R.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'"
                                             + " AND R.CD_PLANT = '" + obj[0] + "'"
                                             + " AND R.CD_ITEM = '" + obj[1] + "'"
                                             + " AND R.TP_WO = '" + obj[2] + "'"
                                             + " AND R.YN_STAND = 'Y'");
            }
            else
            {
                dt = DBHelper.GetDataTable(@"SELECT R.NO_OPPATH AS CODE, R.NO_OPPATH || '-' ||  T.DC_OPPATH AS NAME, T.TP_OPPATH "
                                            + " FROM PR_TPWO_ROUT R "
                                            + " LEFT JOIN PR_ROUT T ON R.CD_COMPANY = T.CD_COMPANY AND R.CD_PLANT = T.CD_PLANT AND R.CD_ITEM = T.CD_ITEM AND R.NO_OPPATH = T.NO_OPPATH "
                                            + " WHERE R.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'"
                                            + " AND R.CD_PLANT = '" + obj[0] + "'"
                                            + " AND R.CD_ITEM = '" + obj[1] + "'"
                                            + " AND R.TP_WO = '" + obj[2] + "'"

                                            + " AND R.YN_STAND = 'Y'");
            }

            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;

            return dt.Rows[0]["CODE"].ToString();

        }
        #endregion

        #region Get_Pr_Cd_SL
        public DataTable Get_Pr_Cd_SL(object[] obj)
        {
            string selectQuery = string.Empty;

            selectQuery = " SELECT A.CD_SL		AS CD_SL_OT,     " +
                          "        B.NM_SL		AS NM_SL_OT      " +
                          "   FROM PR_WCOP A INNER JOIN MA_SL B  " +
                          "     ON A.CD_COMPANY = B.CD_COMPANY   " +
                          "    AND A.CD_PLANT   = B.CD_PLANT     " +
                          "    AND A.CD_SL      = B.CD_SL        " +
                          "  WHERE A.CD_COMPANY = '" + obj[0] + "'" +
                          "    AND A.CD_PLANT   = '" + obj[1] + "'" +
                          "    AND A.CD_WC      = '" + obj[2] + "'" +
                          "    AND A.CD_WCOP    = '" + obj[3] + "'";

            return DBHelper.GetDataTable(selectQuery);
        }
        #endregion

        #region Get_No_Project
        public DataTable Get_No_Project(object[] obj)
        {
            string selectQuery = string.Empty;

            selectQuery = " SELECT A.NO_PROJECT		AS NO_PROJECT, " +
                          "        A.NM_PROJECT		AS NM_PROJECT  " +
                          "   FROM SA_PROJECTH A " +
                          "  WHERE A.CD_COMPANY = '" + obj[0] + "'" +
                          "    AND A.NO_PROJECT = '" + obj[1] + "'";

            return DBHelper.GetDataTable(selectQuery);
        }
        #endregion

        #region 공장 품목 MASTER
        public DataTable Search_MA_Pitem(string sCd_Plant, string sCd_Item)
        {
            return DBHelper.GetDataTable("UP_PR_COMMON_ITEM_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode, sCd_Plant, sCd_Item });
        }
        #endregion

        #region 작업장-공정 MASTER
        public DataTable Search_MA_WCOP(string sCd_Plant, string sCd_Wc)
        {
            return DBHelper.GetDataTable("UP_PR_WCOP_DTL_S2", new object[] { Global.MainFrame.LoginInfo.CompanyCode, sCd_Plant, sCd_Wc });
        }
        #endregion

        #region 창고 MASTER
        public DataTable Search_SL(string p_cd_plant, string p_cd_sl_pipe)
        {
            return DBHelper.GetDataTable("UP_PR_COMMON_SL_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode, p_cd_plant, p_cd_sl_pipe });
        }
        #endregion

        #region -> 품목의 ATP 조회

        public DataTable GetATPSearch(object[] obj_Replace)
        {
            DataSet ds_Replace = DBHelper.GetDataSet("UP_PR_MATL_REPLACE_SUB2_D_S", obj_Replace);

            Dass.FlexGrid.FlexGrid flex = new Dass.FlexGrid.FlexGrid();

            DataTable dtCrossD = flex.GetCrossTable(ds_Replace.Tables[1], "NM_SL", "QT_INV");
            DataTable dtD = ds_Replace.Tables[0].Copy();
            dtD.Merge(dtCrossD);
            dtD.DefaultView.Sort = "CD_ITEM";

            int start = dtD.Columns["UNIT_IM"].Ordinal;// UNIT_IM에서 FG_ABC로 바꾸었음. 제일 끝 컬럼을 찍어줘야하므로

            string strExpression = string.Empty;
            string strExpression2 = string.Empty;
            string strExpression3 = string.Empty;

            for (int i = start + 1; i < dtD.Columns.Count; i++)
            {
                int iColumnLength = dtD.Columns[i].ColumnName.Length * 18;
                iColumnLength = iColumnLength < 90 ? 90 : iColumnLength;
                flex.SetCol(dtD.Columns[i].ColumnName, dtD.Columns[i].Caption, iColumnLength, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                strExpression += "[" + dtD.Columns[i].ColumnName + "]";
                if (i < dtD.Columns.Count - 1)
                    strExpression += " + ";
            }

            //CorssTab으로 변경하면서 null 값으로 바뀐 값들을 0으로 변경하여 준다.(합계컬럼을 위해서)

            foreach (DataRow dr in dtD.Rows)
            {
                for (int i = start + 1; i < dtD.Columns.Count; i++)
                {
                    if (dr[i] == DBNull.Value)
                    {
                        dr[i] = 0m;
                    }

                    if (dr["FG_VAL"].ToString() == "FALSE" && D.GetDecimal(dr[i]) != 0)
                    {
                        dr["FG_VAL"] = "TRUE";
                    }
                }
            }

            strExpression2 = strExpression + " - [QT_ISU_1] - [QT_ISU_2] - [QT_ISU_3] - [QT_ISU_4] - [QT_ISU_5]";
            strExpression3 = strExpression + " + [QT_RCV_1] + [QT_RCV_2] + [QT_RCV_3] + [QT_RCV_4] - [QT_ISU_1] - [QT_ISU_2] - [QT_ISU_3] - [QT_ISU_4] - [QT_ISU_5]";

            dtD.Columns.Add("QT_SUM", typeof(decimal), strExpression);
            dtD.Columns.Add("QT_CAN_SO2", typeof(decimal), strExpression3);
            dtD.Columns.Add("ISU_CAN_SO", typeof(decimal), strExpression2);

            dtD.AcceptChanges();

            return dtD;
        }

        #endregion

        #region 대체품사용여부

        public string isReplace(string CD_PLANT)
        {
            string SQL = " SELECT YN_REPLACE "
                       + " FROM   SA_ATP_ENV "
                       + " WHERE  CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' "
                       + " AND    CD_PLANT = '" + CD_PLANT + "' ";

            string s_YN_REPLACE = "N";

            DataTable dt = DBHelper.GetDataTable(SQL);

            if (dt != null && dt.Rows.Count > 0 && D.GetString(dt.Rows[0]["YN_REPLACE"]) == "Y")
                s_YN_REPLACE = "Y";

            return s_YN_REPLACE;
        }

        #endregion

        #region -> Get수주추적

        public DataTable Get수주추적(object[] obj)
        {
            return DBHelper.GetDataTable("UP_PR_SA_SO_TRACKING_S", obj);
        }

        #endregion

        #region -> 공장환경설정의 SHIFT 조회

        public DataTable Get_PR_SHIFT(string CD_COMPANY, string CD_PLANT, string NO_SFT)
        {
            string SQL = "SELECT NO_SFT, NM_SFT "
                       + "FROM   PR_SHIFT "
                       + "WHERE  CD_COMPANY = '" + CD_COMPANY + "' "
                       + "AND    CD_PLANT   = '" + CD_PLANT + "' "
                       + "AND    NO_SFT     = '" + NO_SFT + "' ";

            return DBHelper.GetDataTable(SQL);
        }

        #endregion

        #region -> PR_SHIFT에 넣어줌
        
        public void Insert_PR_SHIFT(string CD_COMPANY, string CD_PLANT, string NO_SFT, string NM_SFT)
        {
            string SQL = "INSERT INTO PR_SHIFT "
                       + "( CD_COMPANY, CD_PLANT, NO_SFT, NM_SFT, "
                       + "TP_START, TM_START, TP_END, TM_END, TM_STOP, TM_SFT, QT_WORKER, CD_DEPT, YN_USE ) "
                       + "VALUES "
                       + "( '" + CD_COMPANY + "', '" + CD_PLANT + "', '" + NO_SFT + "', '" + NM_SFT + "', "
                       + "'1', '000000', '1', '000000', '000000', '000000', 0, NULL, 'Y' )";

            DBHelper.ExecuteScalar(SQL);
        }

        #endregion

        #region -> 안전공업 전용 프린트(Z_ANJUN_Print)

        public DataTable Z_ANJUN_Print(string 공장코드, string 작업지시번호)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PR_Z_ANJUN_WO_REG_NEW_P";
            si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, 공장코드, 작업지시번호 };

            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;

            foreach (DataColumn Col in dt.Columns)
            {
                if (Col.DataType == Type.GetType("System.Decimal"))
                    Col.DefaultValue = 0;
            }

            dt.Columns["CD_PLANT"].DefaultValue = 공장코드;
            dt.Columns["FG_WO"].DefaultValue = "003";
            dt.Columns["ST_WO"].DefaultValue = "O";
            dt.Columns["TP_ROUT"].DefaultValue = "1";
            dt.Columns["DT_REL"].DefaultValue = Global.MainFrame.GetStringToday;
            dt.Columns["DT_DUE"].DefaultValue = Global.MainFrame.GetStringToday;
            dt.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dt.Columns["YN_AUTOREQ"].DefaultValue = "N";
            dt.Columns["YN_AUTORCV"].DefaultValue = "N";
            dt.Columns["YN_CTRL_QTIO"].DefaultValue = "N";
            dt.Columns["YN_AUTOPO"].DefaultValue = "N";
            dt.Columns["YN_AUTO_CLS"].DefaultValue = "N";
            dt.Columns["YN_AUTORCV_REQ"].DefaultValue = "N";  //★ i01

            return dt;
        }

        #endregion

        #region -> 케이에스시스템 전용 프린트(Z_KSSYSTEM_Print)

        public DataSet Z_KSSYSTEM_Print(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("UP_PR_Z_KSSYSTEM_WO_REG_NEW_P", obj);
            return ds;
        }

        #endregion

        #region -> 안전공업 전용 최근작지번호조회(Z_ANJUN_Search_WO)

        public string Z_ANJUN_Search_WO(string 공장코드, string 품목코드)
        {
            string selectQuery = string.Empty;

            selectQuery = " SELECT	NO_WO " +
                          " FROM    PR_WO " +
                          " WHERE   CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                          " AND     CD_PLANT   = '" + 공장코드 + "'" +
                          " AND     CD_ITEM    = '" + 품목코드 + "'" +
                          " AND     DTS_INSERT = (SELECT MAX(DTS_INSERT) " +
                                                " FROM   PR_WO " +
                                                " WHERE  CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                                " AND    CD_PLANT   = '" + 공장코드 + "'" +
                                                " AND    CD_ITEM    = '" + 품목코드 + "')";

            DataTable dt = DBHelper.GetDataTable(selectQuery);

            if (dt.Rows.Count == 1)
                return D.GetString(dt.Rows[0]["NO_WO"]);
            else
                return string.Empty;
        }

        #endregion

        #region -> 안전공업 전용 계획량조회(Z_ANJUN_Search_계획량)

        public decimal Z_ANJUN_Search_계획량(string 공장코드, string 품목코드, string 기준월)
        {
            기준월 = D.GetString((D.GetDecimal(기준월) - 1));

            string selectQuery = string.Empty;

            selectQuery = " SELECT	SUM(QT_SO) AS QT_SO " +
                          " FROM    SA_Z_WOFIT_REG_AN " +
                          " WHERE   CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                          " AND     CD_ITEM    = '" + 품목코드 + "'" +
                          " AND     STND_DT    = '" + 기준월 + "'";

            DataTable dt = DBHelper.GetDataTable(selectQuery);

            if (dt.Rows.Count == 1)
                return D.GetDecimal(dt.Rows[0]["QT_SO"]);
            else
                return decimal.Zero;
        }

        #endregion

        #region -> 안전공업 전용 발행량조회(Z_ANJUN_Search_발행량)

        public decimal Z_ANJUN_Search_발행량(string 공장코드, string 품목코드, string 기준월)
        {
            string selectQuery = string.Empty;

            selectQuery = " SELECT	SUM(QT_ITEM) AS QT_ITEM " +
                          " FROM    PR_WO " +
                          " WHERE   CD_COMPANY  = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                          " AND     CD_PLANT    = '" + 공장코드 + "'" +
                          " AND     CD_ITEM     = '" + 품목코드 + "'" +
                          " AND     CD_USERDEF1 = '" + 기준월 + "'";

            DataTable dt = DBHelper.GetDataTable(selectQuery);

            if (dt.Rows.Count == 1)
                return D.GetDecimal(dt.Rows[0]["QT_ITEM"]);
            else
                return decimal.Zero;
        }

        #endregion

        #region -> 한일도요 전용 인쇄(Z_HANILTOYO_Print)

        public DataSet Z_HANILTOYO_Print(object[] obj)
        {
            return DBHelper.GetDataSet("UP_PR_Z_HANILTOYO_WO_REG_NEW_P", obj);
        }

        #endregion

        #region -> 대흥화학공업 전용 인쇄(Z_DHC_Print)

        public DataTable Z_DHC_Print(object[] obj)
        {
            return DBHelper.GetDataTable("UP_PR_Z_DHC_WO_BILL_P", obj);
        }

        #endregion

        #region -> PMS 조회

        public DataTable Search_PMS(string CD_PLANT, string NO_WO)
        {
            string SQL = "SELECT ID_MEMO, CD_WBS, NO_PJT, NO_SHARE, NO_ISSUE "
                       + "FROM   PR_WO "
                       + "WHERE  CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' "
                       + "AND    CD_PLANT = '" + CD_PLANT + "' "
                       + "AND    NO_WO = '" + NO_WO + "'";

            return DBHelper.GetDataTable(SQL);
        }

        public DataTable Search_PMS_NO_WO(string ID_MEMO)
        {
            string SQL = "SELECT NO_WO, CD_PLANT "
                       + "FROM   PR_WO "
                       + "WHERE  CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' "
                       + "AND    ID_MEMO = '" + ID_MEMO + "'";

            return DBHelper.GetDataTable(SQL);
        }

        #endregion

        #region -> 쏠리드 전용 장납자재전개(Z_SOLIDTECH_Search_Material)
        public DataTable Z_SOLIDTECH_Search_Material(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_PR_Z_SOLIDTECH_MATL_S", obj);

            dt.Columns.Add("ROW_COLOR", typeof(string));

            string s_대체품적용여부 = isReplace(D.GetString(obj[1]));

            if (s_대체품적용여부 == "Y")
            {
                string s_Multi_Matl = Duzon.ERPU.MF.Common.Common.MultiString(dt, "CD_MATL", "|");

                object[] obj_Replace = new object[]
                {
                    Global.MainFrame.LoginInfo.CompanyCode,
                    obj[1],
                    D.GetString(obj[5]).Substring(0, 4) + "0101",
                    obj[5],
                    s_Multi_Matl,
                    string.Empty,
                    "Y"
                };

                DataTable dtD = GetATPSearch(obj_Replace);

                foreach (DataRow dr in dt.Rows)
                {
                    DataRow[] drs = dtD.Select("CD_ITEM = '" + D.GetString(dr["CD_MATL"]) + "'");

                    if (drs.Length == 0)
                    {
                        dr["ROW_COLOR"] = "W";
                        continue;
                    }

                    string s_NO_GRP = D.GetString(drs[0]["NO_GRP"]);
                    string s_CD_ITEM = D.GetString(drs[0]["CD_ITEM"]);
                    string s_Filter = "NO_GRP = '" + s_NO_GRP + "' AND CD_ITEM = '" + s_CD_ITEM + "'";

                    decimal d_ISU_CAN_SO = D.GetDecimal(drs[0]["ISU_CAN_SO"]);

                    if (d_ISU_CAN_SO > 0)
                        dr["ROW_COLOR"] = "Y";
                    else if (d_ISU_CAN_SO == 0)
                        dr["ROW_COLOR"] = "W";
                    else
                        dr["ROW_COLOR"] = "B";
                }

                dt.AcceptChanges();
            }

            return dt;
        }
        #endregion

        #region -> SK케미칼 전용 유효기간조회(Z_SKCHEMICAL_Search_DY_VALID)
        public int Z_SKCHEMICAL_Search_DY_VALID(string CD_PLANT, string CD_ITEM)
        {
            DataTable dt = DBHelper.GetDataTable(@"
                SELECT  DY_VALID
                FROM    MA_PITEM
                WHERE   CD_COMPANY  = '" + Global.MainFrame.LoginInfo.CompanyCode + @"' 
                AND     CD_PLANT    = '" + CD_PLANT + @"' 
                AND     CD_ITEM     = '" + CD_ITEM + "' " 
             );

            int returnData = 0;
            if (dt.Rows.Count > 0)
            {
                returnData = D.GetInt(dt.Rows[0]["DY_VALID"]);
            }

            return returnData;
        }
        #endregion
    }
}
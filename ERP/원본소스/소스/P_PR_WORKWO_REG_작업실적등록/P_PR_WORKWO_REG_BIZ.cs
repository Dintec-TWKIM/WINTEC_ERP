using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;
using Duzon.ERPU;

namespace prd
{
    class P_PR_WORKWO_REG_BIZ
    {
        #region -> 생산 LOTNO 사용여부

        public string SELECT_YN_PR_MNG_LOT(object[] obj)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PR_YN_LOT_SELECT";
            si.SpParamsSelect = obj;
            ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)rtn.DataValue;
            string YN_MNG_LOT = "N";
            if (dt.Rows.Count == 1)
                YN_MNG_LOT = dt.Rows[0]["MNG_LOT"].ToString() == "Y" ? "Y" : "N";
            return YN_MNG_LOT;
        }

        #endregion

        #region -> 툴바조회

        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_PR_WORKWO_REG_SELECT", obj);
            return dt;
        }

        #endregion
       
        #region -> 작업지시번호선택시 조회

        public DataTable SearchDetail(object[] obj)
        {
            //SpInfo si = new SpInfo();
            //si.SpNameSelect = "UP_PR_WORK_REG_SELECT";
            //si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, 공장코드, 작지번호 };
            //ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);
            //DataTable dt = (DataTable)rtn.DataValue;

            DataTable dt = DBHelper.GetDataTable("UP_PR_WORK_REG_SELECT", obj);

            // 세부내역에 들어가는 컬럼들은 DB에 없는 컬럼이므로 만들어 준다..나중에 저장시 PR_WORK 테이블에 입력됨
           
            dt.Columns.Add("NO_WORK", typeof(string));
            dt.Columns.Add("NO_EMP", typeof(string));
            dt.Columns.Add("NM_KOR", typeof(string));
            dt.Columns.Add("CD_DEPT", typeof(string));
            //dt.Columns.Add("QT_WORK", typeof(decimal));
            dt.Columns.Add("QT_REJECT", typeof(decimal));
            dt.Columns.Add("CD_RSRC_LABOR", typeof(string));
            dt.Columns.Add("TM_LABOR", typeof(decimal));
            dt.Columns.Add("TM_MACH", typeof(decimal));
            dt.Columns.Add("CD_RSRC_MACH", typeof(string));
            dt.Columns.Add("YN_REWORK", typeof(string));
            dt.Columns.Add("YN_BAD_PROC", typeof(string));
            dt.Columns.Add("TP_REWORK", typeof(string)); //공정재작업처리시 필요
            dt.Columns.Add("CD_REWORK", typeof(string)); //공정재작업처리시 필요
            dt.Columns.Add("CD_REJECT", typeof(string)); //공정재작업처리시 필요
            dt.Columns.Add("DC_REJECT", typeof(string)); //공정재작업처리시 필요
            dt.Columns.Add("QT_RSRC_LABOR", typeof(decimal)); //작업인원 추가로 인한 컬럼추가 20111213 최인성 정기현 김성호

            //
            // 2014.07.29 D20140725035 윈플러스
            // 윈플러스에서 C/C별 원가를 사용하고 싶은데 실적의 C/C는 PR_WORK_MANDAY 테이블에 있음.
            // 윈플러스에서 최초 사용자교육시 C/C별 원가 사용을 위한 공수입력을 교육했어야 하지만,
            // 그렇게 하지 않아 실적에 대한 C/C별 원가를 적용할 방법이 없어서 PR_WORK 테이블에 부서코드를 추가함.
            //
            dt.Columns.Add("CD_DEPT_WORK", typeof(string)); 

            // 디폴트값 셋팅
            foreach (DataColumn Col in dt.Columns)
            {
                if (Col.DataType == typeof(decimal))
                    Col.DefaultValue = 0;
            }
            
            foreach (DataRow row in dt.Rows)
            {
                row["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
                row["NM_KOR"] = Global.MainFrame.LoginInfo.EmployeeName;
                row["CD_DEPT"] = Global.MainFrame.LoginInfo.DeptCode; 

                if (!Global.MainFrame.ServerKeyCommon.Contains("SRPACK"))  
                    row["QT_WORK"] = 0;

                row["QT_REJECT"] = 0;
                row["TM_LABOR"] = 0;
                row["TM_MACH"] = 0;
                row["QT_RSRC_LABOR"] = 0;

                //
                // 2014.07.29 D20140725035 윈플러스
                //
                if (Global.MainFrame.ServerKeyCommon.Contains("WINPLUS") ||
                    Global.MainFrame.ServerKeyCommon.Contains("DZSQL") ||
                    Global.MainFrame.ServerKeyCommon.Contains("SQL_"))
                    row["CD_DEPT_WORK"] = Global.MainFrame.LoginInfo.DeptCode;
            }

            dt.AcceptChanges();

            return dt;
        }

        #endregion

        #region -> 삭제

        public bool DELETE_NO_WO(object[] obj)
        {
            ResultData rtn = (ResultData)Global.MainFrame.ExecSp("UP_PR_WORK_REG_NO_WO_DELETE", obj);
            bool bResult = rtn.Result;
            return bResult;
        }

        public bool DELETE_CD_OP(object[] obj)
        {
            ResultData rtn = (ResultData)Global.MainFrame.ExecSp("UP_PR_WORK_REG_CD_OP_DELETE", obj);
            bool bResult = rtn.Result;
            return bResult;
        }

        #endregion

        #region -> 저장

        public bool Save(DataTable dtWork, DataTable dtReject, DataTable _dtLotItem, DataTable _dtMatl, DataTable _dtLot, DataTable _dtSERL, DataTable dt_Manday, DataTable dt_Auto_Bad_Work, DataTable dt_Auto_Bad,
                         DataTable dt_AutoBad_ReqH, DataTable dt_AutoBad_ReqL,
                         DataTable dt_Item_Location, DataTable dt_Matl_Location,
                         DataTable dt_Use_Matl,
                         string Noline, string strNO_II, string str입고창고)
        {
            SpInfoCollection sc = new SpInfoCollection();
            string str청구번호 = "";
           
            if (dtWork != null)
            {
                #region 작업실적
                dtWork.RemotingFormat = SerializationFormat.Binary;
                
                SpInfo si = new SpInfo();

                si.DataValue = dtWork;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;

                //
                // select절에 PR_WO_ROUT.ID_INSERT가 있어 저장이 잘못되는 오류.
                // (SpInfo.UserID [ID_ISNERT] -> 새로운파라메터로변경 [ID_ISNERT_TEMP])
                //
                string sID_INSERT = string.Empty;
                //USER ID가 패키지 같은데 기존업체가 있어서 일단 서버키로 변경하도록한다.
                if (Global.MainFrame.ServerKeyCommon.Contains("TRIGEM")
                    || Global.MainFrame.ServerKeyCommon.Contains("SQL")
                    )
                {
                    si.UserID = Global.MainFrame.LoginInfo.UserID;
                    sID_INSERT = Global.MainFrame.LoginInfo.UserID;
                }
                else
                {
                    si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                    sID_INSERT = Global.MainFrame.LoginInfo.EmployeeNo;
                }

                si.DataState = DataValueState.Added;
                si.SpNameInsert = "UP_PR_WORK_REG_INSERT";
                si.SpParamsInsert = new string[] { "CD_COMPANY",        "NO_WORK",              "NO_WO",            "CD_PLANT",         "CD_OP", 
                                                   "CD_ITEM",           "NO_EMP",               "DT_WORK",          "QT_WORK",          "QT_REJECT", 
                                                   "QT_MOVE",           "YN_REWORK",            "TM_LABOR",         "CD_RSRC_LABOR",    "TM_MACH", 
                                                   "CD_RSRC_MACH",      "CD_WC",                "ID_INSERT_TEMP",   "FG_MOVE",          "NO_LOT", 
                                                   "NO_SFT",            "CD_EQUIP",             "NO_IO_202_102",    "NO_IO_203",        "NO_LINE_202", 
                                                   "NO_LINE_102",       "NO_LINE_203",          "CD_SL",            "CD_WCOP_SUB",      "DC_RMK1", 
                                                   "DC_RMK2",           "YN_SUBCON",            "NO_OPOUT_PO",      "NO_OPOUT_LINE",    "NO_REL", 
                                                   "QT_RSRC_LABOR",     "NO_WORK_TRACKING",     "DC_RMK3",          "QT_RATE_CALC",     "DT_LIMIT", 
                                                   "QT_WO",             "QT_WO_WORK",           "QT_CHCOEF",        "QT_WORK_CHCOEF",   "QT_WORK_BAD_CHCOEF",
                                                   "CD_MNG1",           "CD_MNG2",              "CD_MNG3",          "CD_MNG4",          "CD_MNG5",
                                                   "CD_MNG6",           "CD_MNG7",              "CD_MNG8",          "CD_MNG9",          "CD_MNG10",
                                                   "CD_MNG11",          "CD_MNG12",             "CD_MNG13",         "CD_MNG14",         "CD_MNG15",
                                                   "CD_MNG16",          "CD_MNG17",             "CD_MNG18",         "CD_MNG19",         "CD_MNG20",
                                                   "CD_POST",           "CD_USERDEF1",          "CD_DEPT_WORK",     "TXT_USERDEF1",     "TXT_USERDEF2",
                                                   "TXT_USERDEF3",      "CD_USERDEF2",          "CD_USERDEF3",      "NUM_USERDEF1",     "NUM_USERDEF2",
                                                   "NUM_USERDEF3",      "NUM_USERDEF4",         "NUM_USERDEF5",     "NUM_USERDEF6",     "NUM_USERDEF7",
                                                   "NUM_USERDEF8",      "NUM_USERDEF9",         "NUM_USERDEF10" };
                si.SpParamsDelete = new string[] { "CD_COMPANY",        "NO_WORK",              "NO_WO",            "CD_PLANT",         "CD_OP",
                                                   "CD_ITEM",           "NO_EMP",               "DT_WORK",          "QT_WORK",          "QT_REJECT",
                                                   "CD_REJECT",         "QT_MOVE",              "CD_WC",            "QT_CLS" };
                si.SpParamsValues.Add(ActionState.Insert, "CD_SL", str입고창고);
                si.SpParamsValues.Add(ActionState.Insert, "ID_INSERT_TEMP", sID_INSERT);
                sc.Add(si);
                #endregion
            }

            if (_dtLotItem != null)
            {
                #region Lot 품목
                _dtLotItem.RemotingFormat = SerializationFormat.Binary;

                SpInfo si = new SpInfo();

                si.DataValue = _dtLotItem;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si.DataState = DataValueState.Added;
                si.SpNameInsert = "UP_PR_WORK_ITEMLOT_INSERT";

                si.SpParamsInsert = new string[] { "CD_COMPANY", "CD_PLANT", "NO_IO_202_102", "NO_IO_203", "NO_LINE_202", 
                    "NO_LINE_102", "NO_LINE_203", "NO_IOLINE2", "NO_LOT", "NO_IO", "NO_IOLINE", "NO_IOLINE2", "NO_LOT", "CD_ITEM", 
                    "DT_IO", "CD_SL", "QT_GOOD_MNG", "YN_RETURN", "NO_WO", "NO_WORK", "CD_OP", "CD_WC", "YN_FINAL", "ID_INSERT" };

                si.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", "N");
                si.SpParamsValues.Add(ActionState.Insert, "YN_FINAL", dtWork.Rows[0]["YN_FINAL"].ToString());
                si.SpParamsValues.Add(ActionState.Insert, "NO_WORK", dtWork.Rows[0]["NO_WORK"].ToString());
                
                if(!_dtLotItem.Columns.Contains("NO_WO"))
                    si.SpParamsValues.Add(ActionState.Insert, "NO_WO", dtWork.Rows[0]["NO_WO"].ToString());

                sc.Add(si);
                #endregion
            }

            if (_dtMatl != null)
            {
                //청구번호 생성여부 판단 구문
                #region 청구자재
                bool bReq = false;
                

                foreach (DataRow dr in _dtMatl.Rows)
                {
                    if (dr["YN_BF"].ToString() == "Y")
                    {
                        bReq = true;
                        break;
                    }
                }

                if (bReq)
                {
                    str청구번호 = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "PR", "08", dtWork.Rows[0]["DT_WORK"].ToString().Substring(0, 6));
                }

                _dtMatl.RemotingFormat = SerializationFormat.Binary;

                //청구번호 생성여부 판단 구문
                SpInfo si = new SpInfo();

                si.DataValue = _dtMatl;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si.DataState = DataValueState.Added;
                si.SpNameInsert = "UP_PR_WORK_MATL_INSERT";

                si.SpParamsInsert = new string[] { "CD_COMPANY", "CD_PLANT", "NO_WO", "NO_WORK", "CD_OP", "CD_WC", "CD_WCOP", "DT_WORK", "CD_MATL", "QT_INPUT", "NO_IO_201", "NO_IO_101",
                    "NO_IO_MM", "NO_LINE", "NO_REQ", "CD_DEPT", "NO_EMP", "CD_SL", "NO_II","ID_INSERT" };

                si.SpParamsValues.Add(ActionState.Insert, "NO_WORK", dtWork.Rows[0]["NO_WORK"].ToString());
                si.SpParamsValues.Add(ActionState.Insert, "NO_REQ", str청구번호);
                si.SpParamsValues.Add(ActionState.Insert, "CD_DEPT", dtWork.Rows[0]["CD_DEPT"].ToString());
                si.SpParamsValues.Add(ActionState.Insert, "NO_EMP", dtWork.Rows[0]["NO_EMP"].ToString());
                si.SpParamsValues.Add(ActionState.Insert, "NO_II", strNO_II);

                sc.Add(si);
                #endregion
            }

            if (_dtLot != null)
            {
                #region Lot
                _dtLot.RemotingFormat = SerializationFormat.Binary;

                SpInfo si = new SpInfo();

                si.DataValue = _dtLot;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si.DataState = DataValueState.Added;
                si.SpNameInsert = "UP_PR_WORK_QTIOLOT_INSERT";

                si.SpParamsInsert = new string[] { "CD_COMPANY", "CD_PLANT", "NO_IO", "NO_IOLINE", "NO_IOLINE2", "NO_IO_201", "NO_IO_101", "NO_IO_MM", "NO_LINE", "NO_LOT", "CD_ITEM", "DT_IO", "CD_SL", "QT_GOOD_MNG", "YN_RETURN", "NO_WO", "NO_WORK", "CD_OP", "CD_WC", "YN_BF", "ID_INSERT" };

                si.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", "N");
                si.SpParamsValues.Add(ActionState.Insert, "NO_WO", dtWork.Rows[0]["NO_WO"].ToString());
                si.SpParamsValues.Add(ActionState.Insert, "NO_WORK", dtWork.Rows[0]["NO_WORK"].ToString());
                si.SpParamsValues.Add(ActionState.Insert, "CD_OP", dtWork.Rows[0]["CD_OP"].ToString());
                si.SpParamsValues.Add(ActionState.Insert, "CD_WC", dtWork.Rows[0]["CD_WC"].ToString());

                sc.Add(si);
                #endregion
            }

            if (_dtSERL != null)
            {
                #region 시리얼
                _dtSERL.RemotingFormat = SerializationFormat.Binary;

                SpInfo si = new SpInfo();

                si.DataValue = _dtSERL;
                si.SpNameInsert = "UP_PR_MM_QTIODS_INSERT";
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.SpParamsInsert = new string[] { 
	            "CD_COMPANY", "NO_WORK", "NO_SERIAL", "NO_IO", "NO_IOLINE", "CD_ITEM", "CD_QTIOTP", "FG_IO",
		        "CD_MNG1",	"CD_MNG2",	"CD_MNG3",	"CD_MNG4",	"CD_MNG5",	"CD_MNG6",	"CD_MNG7",	"CD_MNG8",	"CD_MNG9",	"CD_MNG10",
		        "CD_MNG11",	"CD_MNG12",	"CD_MNG13",	"CD_MNG14",	"CD_MNG15",	"CD_MNG16",	"CD_MNG17",	"CD_MNG18",	"CD_MNG19",	"CD_MNG20"
                };

                si.SpParamsValues.Add(ActionState.Insert, "NO_WORK", dtWork.Rows[0]["NO_WORK"].ToString());

                sc.Add(si);
                #endregion
            }

            if (dtReject != null)
            {
                #region 재작업
                dtReject.RemotingFormat = SerializationFormat.Binary;

                SpInfo si = new SpInfo();

                si.DataValue = dtReject;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si.DataState = DataValueState.Added;
                si.SpNameInsert = "UP_PR_WORKL_INSERT";

                si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_WO", "NO_WORK", "NO_LINE", "CD_REJECT", "CD_RESOURCE", "TM_WORK", "QT_WORK", "QT_REJECT", "DC_RMK", "NO_WOLINE", "NO_SFT", "ID_INSERT","CD_SL_BAD", "NO_REL" };
                
                si.SpParamsValues.Add(ActionState.Insert, "NO_WOLINE", Convert.ToDecimal(Noline));
                si.SpParamsValues.Add(ActionState.Update, "NO_WOLINE", Convert.ToDecimal(Noline));


                if (!dtReject.Columns.Contains("NO_REL"))
                    si.SpParamsValues.Add(ActionState.Insert, "NO_REL", dtWork.Rows[0]["NO_REL"].ToString());

                sc.Add(si);
                #endregion
            }

            if (dt_Manday != null)
            {
                #region 작업실적 건에 대한 공수 관리
                dt_Manday.RemotingFormat = SerializationFormat.Binary;

                SpInfo si = new SpInfo();

                si.DataValue = dt_Manday;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si.DataState = DataValueState.Added;
                si.SpNameInsert = "UP_PR_WORK_MANDAY_SUB_INSERT";

                si.SpParamsInsert = new string[] { "CD_COMPANY",		"CD_PLANT",		    "NO_WORK",		"NO_MANDAY_LINE",	"NO_WO",		"NO_ROUT_LINE",		"CD_OP",				"CD_WCOP",
                                                   "CD_WC",				"QT_WORK",		    "QT_MOVE",		"QT_BAD",			"QT_REWORK",	"NO_OPOUT_PO",		"NO_OPOUT_PO_LINE",		"YN_SUBCON",
                                                   "TM_READ",			"TM_WO_WAIT",		"TM_WORK",		"TM_MOVE",			"TM_WAIT",		"TM_WO_S",			"TM_WO_E",				"TM_WO_T",    
                                                   "QT_WO_ROLL",		"CD_EQUIP",		    "TM_EQ_S",		"TM_EQ_E",			"TM_EQ_T",		"QT_EQ_ROLL",		"DC_RMK",				"ID_INSERT", 
                                                   "TM_PLAN_STOP",      "FG_RUNNING",       "TM_RUNNING",   "DT_WO_S",          "DT_WO_E" ,
                                                   "NM_USERDER1",		"NM_USERDER2",		"NM_USERDER3",	"NM_USERDER4",		"NM_USERDER5",	"NM_USERDER6",		"NM_USERDER7",			"NM_USERDER8",
                                                   "NM_USERDER9",		"NM_USERDER10",     "TM_BREAK"
                                                 };

                sc.Add(si);
                #endregion
            }

            //자동불량처리를 탈경우에만 불량처리내역이 존재함으로.
            if (dt_Auto_Bad != null)
            {
                #region 자동불량내역처리

                //불량처리에 대한 실적 적용
                SpInfo si = new SpInfo();
                si.DataValue = dt_Auto_Bad_Work;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.DataState = DataValueState.Added;
                si.SpNameInsert = "UP_PR_BADWORK_REG_INSERT";
                si.SpParamsInsert = new string[] { "CD_COMPANY",    "NO_WORK",      "NO_WO",            "CD_PLANT",     "CD_OP",
                                                   "CD_ITEM",       "NO_EMP",       "DT_WORK",          "QT_WORK",      "QT_REJECT",
                                                   "CD_REJECT",     "QT_MOVE",      "YN_REWORK",        "YN_BAD_PROC",  "TM_LABOR",
                                                   "CD_RSRC_LABOR", "TM_MACH",      "CD_RSRC_MACH",     "CD_WC",        "DC_REJECT",
                                                   "ID_INSERT",     "FG_MOVE",      "TP_REWORK",        "CD_REWORK",    "NO_LOT",
                                                   "NO_SFT",        "CD_EQUIP",     "NO_IO_202_102",    "NO_IO_203",    "NO_LINE_202",
                                                   "NO_LINE_102",   "NO_LINE_203",  "YN_SUBCON",        "NO_PO",        "NO_POLINE", 
                                                   "NO_REL",        "CD_MNG1",      "CD_MNG2",          "CD_MNG3",      "CD_MNG4",
                                                   "CD_MNG5",       "CD_MNG6",      "CD_MNG7",          "CD_MNG8",      "CD_MNG9",
                                                   "CD_MNG10",      "CD_MNG11",     "CD_MNG12",         "CD_MNG13",     "CD_MNG14",
                                                   "CD_MNG15",      "CD_MNG16",     "CD_MNG17",         "CD_MNG18",     "CD_MNG19",
                                                   "CD_MNG20" };

                if (!dt_Auto_Bad_Work.Columns.Contains("NO_REL"))
                    si.SpParamsValues.Add(ActionState.Insert, "NO_REL", dtWork.Rows[0]["NO_REL"].ToString());

                sc.Add(si);

                //불량내역에 대한 불량처리
                si = new SpInfo();
                si.DataValue = dt_Auto_Bad;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.DataState = DataValueState.Added;
                si.SpNameInsert = "UP_PR_WORK_BAD_INSERT";
                si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_WORK", "NO_WORK_ORIGIN", "NO_LINE", "NO_WO", "QT_REJECT", "ID_INSERT" };
                si.SpParamsValues.Add(ActionState.Insert, "NO_WORK_ORIGIN", dtWork.Rows[0]["NO_WORK"].ToString());

                sc.Add(si);
                #endregion

                #region 불량내역자동 의뢰입고처리
                if (dt_AutoBad_ReqH != null)
                {
                    si = new SpInfo();
                    si.DataValue = dt_AutoBad_ReqH;
                    si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    si.UserID = Global.MainFrame.LoginInfo.UserID;
                    si.DataState = DataValueState.Added;
                    si.SpNameInsert = "UP_PR_WORK_BAD_AUTO_H_INSERT";
                    si.SpParamsInsert = new string[] { "CD_COMPANY", "CD_PLANT", "NO_REQ", "DT_REQ", "NO_EMP", "DC_RMK", "CD_DEPT", "NO_IO", "YN_AUTOBAD_RCV", "ID_INSERT" };

                    sc.Add(si);
                }

                if (dt_AutoBad_ReqL != null)
                {
                    si = new SpInfo();
                    si.DataValue = dt_AutoBad_ReqL;
                    si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    si.UserID = Global.MainFrame.LoginInfo.UserID;
                    si.DataState = DataValueState.Added;
                    si.SpNameInsert = "UP_PR_WORK_BAD_AUTO_L_INSERT";
                    si.SpParamsInsert = new string[] { "CD_COMPANY",    "CD_PLANT",     "NO_REQ",       "NO_LINE",          "CD_WC", 
                                                       "CD_ITEM",       "DT_REQ",       "QT_REQ",       "QT_REQ_W",         "QT_REQ_B", 
                                                       "YN_QC",         "QT_RCV",       "CD_SL",        "NO_WO",            "NO_WORK", 
                                                       "TP_WB",         "TP_GR",        "NO_IO",        "YN_AUTOBAD_RCV",   "NO_EMP",
                                                       "NO_LOT",        "NO_IO_203",    "NO_LINE_203",  "DT_LIMIT",         "CD_MNG1",
                                                       "CD_MNG2",       "CD_MNG3",      "CD_MNG4",      "CD_MNG5",          "CD_MNG6",
                                                       "CD_MNG7",       "CD_MNG8",      "CD_MNG9",      "CD_MNG10",         "CD_MNG11",
                                                       "CD_MNG12",      "CD_MNG13",     "CD_MNG14",     "CD_MNG15",         "CD_MNG16",
                                                       "CD_MNG17",      "CD_MNG18",     "CD_MNG19",     "CD_MNG20"  };
                    sc.Add(si);
                }
                #endregion
            }

            if (dt_Item_Location != null)
            {
                #region 제품 Location
                SpInfo si = new SpInfo();

                si.DataValue = dt_Item_Location;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.DataState = DataValueState.Added;
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameInsert = "UP_PR_MM_QTIO_LOCATION_ITEM_I";

                si.SpParamsInsert = new string[] { "CD_COMPANY", "CD_LOCATION", "CD_PLANT", "CD_SL", "CD_ITEM", "QT_IO_LOCATION", "NO_WO", "NO_WORK" };

                sc.Add(si);
                #endregion
            }

            if (dt_Matl_Location != null)
            {
                #region 자재 Location
                SpInfo si = new SpInfo();

                si.DataValue = dt_Matl_Location;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.DataState = DataValueState.Added;
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameInsert = "UP_PR_MM_QTIO_LOCATION_MATL_I";
                si.SpParamsInsert = new string[] {  "CD_COMPANY",           "CD_LOCATION",          "CD_PLANT",             "CD_SL",            "CD_ITEM", 
                                                    "QT_IO_LOCATION",       "NO_IO_MM",             "NO_REQ",               "NO_WO",            "TEMP_MATL_LINE",
                                                    "NO_WORK"};
                si.SpParamsValues.Add(ActionState.Insert, "NO_REQ", str청구번호);
                
                sc.Add(si);
                #endregion
            }


            //원지 사용량 Update
            if(dt_Use_Matl != null)
            {
                #region 원지 사용량
                SpInfo si = new SpInfo();

                si.DataValue = dt_Use_Matl;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si.SpNameInsert = "UP_MM_QTIO_VIEW_UPDATE";
                si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_WO", "CD_PLANT", "NO_LOT", "BARCODE", "QT_BL", "YN_INSERT", "MES_NO_WO"};
                si.SpNameUpdate = "UP_MM_QTIO_VIEW_UPDATE";
                si.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_WO", "CD_PLANT", "NO_LOT", "BARCODE", "QT_BL", "YN_INSERT", "MES_NO_WO"};

                sc.Add(si);
                #endregion
            }

            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sc);

            for (int i = 0; i < rtn.Length; i++)
                if(!rtn[i].Result) return false;

            return true;
        }

        #endregion

        #region -> 출력

        public DataTable print(string 공장코드, string 작지번호)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PR_WORK_PRINT_SELECT";
            si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, 공장코드, 작지번호 };
            ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)rtn.DataValue;
            return dt;
        }

        #endregion

        #region -> 세부권한 조회
        /// <summary>
        /// 1.권한사용자, 2.영업그룹, 3.구매그룹, 4.수주유형, 5.발주유형, 6.수불형태,7.W/C, 8.오더형태
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataSet Search_AUTH(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("UP_MA_MFG_AUTH_SELECT", obj);

            return ds;
        }

        #endregion

        #region -> 공장환경설정 조회
        /// <summary>
        /// 공장환경설정 조회
        /// </summary>
        /// <param name="obj">회사코드, 공장</param>
        /// <returns></returns>
        public DataSet Search_PlantSetting(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("UP_PR_CFG_PLANT_SELECT", obj);

            return ds;
        }

        #endregion
        
        #region Search_AutoBad_Req 스키마 조회
        /// /// <summary>
        /// 자동불량처리입고 스키마를 가져오기 위한 조회
        /// </summary>
        /// <param name="obj">[0]공장, [1]요청번호</param>
        /// <returns></returns>
        public DataSet Search_AutoBad_Req(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("UP_PR_ITEMRCV_SELECT", obj);

            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn Col in dt.Columns)
                {
                    if (Col.DataType == typeof(decimal))
                        Col.DefaultValue = 0;
                }
            }

            return ds;
        }
        #endregion

        #region Search_Ma_Sl_Location_YN 창고 location 사용유무 조회
        /// <summary>
        /// Location 사용 창고만 조회 함.
        /// </summary>
        /// <param name="obj">[0]공장, [1]창고</param>
        /// <returns></returns>
        public DataTable Search_Ma_Sl_Location_YN(object[] obj)
        {
            string sQuery = string.Empty;

            if(Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
            {
                sQuery = "SELECT CD_SL "
                        + " FROM MA_SL "
                        + " WHERE CD_COMPANY =  '" + Global.MainFrame.LoginInfo.CompanyCode +"'"
                        + " AND CD_PLANT ='"+obj[0].ToString() +"'"
                        + " AND (CD_SL = '" + obj[1].ToString() + "' OR ISNULL('" + obj[1].ToString() + "', '') = '')"
                        + " AND YN_LOCATION = 'Y'";
            }
            else if (Global.MainFrame.DatabaseType == EnumDbType.ORACLE)
            {
                sQuery = "SELECT CD_SL "
                        + " FROM MA_SL "
                        + " WHERE CD_COMPANY =  '" + Global.MainFrame.LoginInfo.CompanyCode + "'"
                        + " AND CD_PLANT ='" + obj[0].ToString() + "'"
                        + " AND (CD_SL = '" + obj[1].ToString() + "' OR NVL('" + obj[1].ToString() + "', ' ') = ' ')"
                        + " AND YN_LOCATION = 'Y'";
            }

            DataTable dt = DBHelper.GetDataTable(sQuery);

            return dt;
        }
        #endregion


        #region PR_WO_MES에서 실적 수량 불러옴
        public DataTable GetPrWoMes_Qt(object[] obj)
        {
            string sQuery = string.Empty;

            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
            {
                sQuery = "SELECT QT*CD_USERDEF1 AS QT_WORK "
                        + " FROM PR_WO_MES "
                        + " WHERE CD_COMPANY =  '" + Global.MainFrame.LoginInfo.CompanyCode + "'"
                        + " AND CD_PLANT ='" + obj[0].ToString() + "'"
                        + " AND (NO_WO = '" + obj[1].ToString() + "' OR ISNULL('" + obj[1].ToString() + "', '') = '')";
            }
            else if (Global.MainFrame.DatabaseType == EnumDbType.ORACLE)
            {
                sQuery = "SELECT QT*CD_USERDEF1 AS QT_WORK "
                        + " FROM PR_WO_MES "
                        + " WHERE CD_COMPANY =  '" + Global.MainFrame.LoginInfo.CompanyCode + "'"
                        + " AND CD_PLANT ='" + obj[0].ToString() + "'"
                        + " AND (NO_WO = '" + obj[1].ToString() + "' OR NVL('" + obj[1].ToString() + "', ' ') = ' ')";
            }

            DataTable dt = DBHelper.GetDataTable(sQuery);

            return dt;
        }
        #endregion

        #region -> 마감

        public bool Wo_Close(DataTable dtClose)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dtClose;
            si.SpNameInsert = "UP_PR_WO_CLOSE";
            si.SpParamsInsert = new String[] { "CD_COMPANY", "CD_PLANT", "NO_WO", "DT_CLOSE" };
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;

            ResultData rs = (ResultData)Global.MainFrame.Save(si);
            
            if (rs.Result)
                return true;
            else
                return false;
        }

        #endregion

        #region -> 마감취소

        public bool Wo_CloseCancel(DataTable dtClose)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dtClose;
            si.SpNameInsert = "UP_PR_WO_CLOSECANCEL";
            si.SpParamsInsert = new String[] { "CD_COMPANY", "CD_PLANT", "NO_WO" };
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            ResultData rs = (ResultData)Global.MainFrame.Save(si);

            if (rs.Result)
                return true;
            else
                return false;
        }

        #endregion

    }
}

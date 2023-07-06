using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Dass.FlexGrid; 
///<summary>
/// 기존프로시저
/// SP_PU_PO_SELECT => UP_PU_PO_REG_SELECT
/// SP_PU_POH_DELETE => UP_PU_POH_DELETE
/// SP_PU_POH_INSERT => UP_PU_PO_REG_POH_INSERT
/// SP_PU_POH_UPDATE => UP_PU_PO_REG_POH_UPDATE
/// SP_PU_POL_INSERT => UP_PU_PO_REG_POL_INSERT
/// SP_PU_POL_UPDATE => UP_PU_PO_REG_POL_UPDATE( 관련프로시저:UP_PU_PRL_FROM_APP_UPDATE)
/// SP_PU_POL_DELETE => UP_PU_PO_REG_POL_DELETE
/// SP_PU_PO_ITEMINFO_SELECT => UP_PU_PO_ITEMINFO_SELECT (품목추가정보)
///</summary>

namespace pur
{
    class P_PU_PO_REG2_BIZ
    {
        DataTable _dt_fg_post = new DataTable();
        bool b_first_clone = false;// 코드관리 명 가져올때 사용하는것임 다른데에서 사용하지마세요
        #region ♣ 조회

        #region -> 툴바조회

        public DataSet Search(string NO_PO)
        {
            ResultData rtn = (ResultData)Global.MainFrame.FillDataSet("UP_PU_PO_REG_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode, NO_PO});
            DataSet ds = (DataSet)rtn.DataValue;

            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn Col in dt.Columns)
                {
                    if (Col.DataType == Type.GetType("System.Decimal"))
                    {
                        if (Col.ColumnName == "RT_PO")
                        {
                            Col.DefaultValue = 1;
                        }
                        else
                        {
                            Col.DefaultValue = 0;
                        }
                    }
                    if (Col.ColumnName == "FG_POCON")
                        Col.DefaultValue = "001";
                    if (Col.ColumnName == "FG_POST")
                        Col.DefaultValue = "O";
                }
            }

            // 헤더테이블 디퐅트값
            DataTable dtHeader = ds.Tables[0];

            dtHeader.Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            dtHeader.Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;
            dtHeader.Columns["NM_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptName;
            dtHeader.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dtHeader.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            dtHeader.Columns["DT_PO"].DefaultValue = Global.MainFrame.GetStringToday;
            dtHeader.Columns["FG_TAXP"].DefaultValue = "001";
            dtHeader.Columns["TP_PROCESS"].DefaultValue = "2";

            //20091130추가됨 (예산통제기능)
            dtHeader.Columns["YN_BUDGET"].DefaultValue = "N"; //예산통제확인여부
            dtHeader.Columns["BUDGET_PASS"].DefaultValue = "N"; //예산통제PASS여부 (예산통제대상Y가 아니면 의미없슴)

            ds.Tables[1].Columns["DATE_USERDEF1"].DefaultValue = Global.MainFrame.GetStringToday;
            ds.Tables[1].Columns["DATE_USERDEF2"].DefaultValue = Global.MainFrame.GetStringToday; 


            return (DataSet)rtn.DataValue;
        }

        #endregion

        #region -> 품목정보조회
        public DataSet ItemInfo_Search(object[] m_obj)
        {
            ResultData rtn = (ResultData)Global.MainFrame.FillDataSet("UP_PU_PO_ITEMINFO_SELECT", m_obj);
            DataSet ds = (DataSet)rtn.DataValue;

            return (DataSet)rtn.DataValue;
        }

		/// <summary>
		/// 품목 SERIAL 채번 : 2011/03/29 아이큐브 개발팀 김현철
		/// </summary>
		/// <param name="obj">{회사코드, 거래처코드, (제조년월일+품목코드)리스트}</param>
		/// <returns>시리얼 번호 리스트</returns>
		public DataTable GetItemSerial(object[] obj)
		{
			SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_PO_ITEMINFO_SERIAL_SELECT";
            si.SpParamsSelect = obj;
			return (Global.MainFrame.FillDataTable(si) as ResultData).DataValue as DataTable;
		}
        #endregion

        #endregion

        #region -> 상세조회

        public DataTable SearchDetail(string strCD_PLANT, string strNO_PO, decimal dNO_LINE)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_PO_REG_SELECT_L";
            si.SpParamsSelect = new Object[] { Global.MainFrame.LoginInfo.CompanyCode, strCD_PLANT, strNO_PO, dNO_LINE };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;

            dt.Columns["CHK"].DefaultValue = 'N';

            foreach (DataColumn col in dt.Columns)
            {
                if (col.DataType == typeof(decimal))
                    col.DefaultValue = 0;
            }

            return dt;
        }

        #endregion

        #region ♣ 삭제

        public bool Delete(string NO_PO)
        {
            Global.MainFrame.ExecSp("UP_PU_POH_DELETE", new object[] { NO_PO, Global.MainFrame.LoginInfo.CompanyCode,Global.MainFrame.LoginInfo.UserID });

            return true;
        }

        #endregion

        #region ♣ 저장

        #region -> 저장

        public bool Save(DataTable dtH, DataTable dtL, bool lb_RcvSave, DataTable dt_RCVH, DataTable dt_RCVL, string 구분
            , DataTable dt_budget, string 구매발주번호, SpInfo si_subinfo, bool lb_RevSave, DataTable dtDD, DataTable dtTH)
        {
            SpInfoCollection sc = new SpInfoCollection();

            if (dtH != null)
            {
                SpInfo si01 = new SpInfo();

                //  dtH.RemotingFormat = SerializationFormat.Binary;

                if (구분 == "COPY")
                    si01.DataState = DataValueState.Added;

                si01.DataValue = dtH;
                si01.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si01.UserID = Global.MainFrame.LoginInfo.UserID;
                si01.SpNameInsert = "UP_PU_POH_I";
                si01.SpNameUpdate = "UP_PU_POH_UPDATE";
                si01.SpParamsInsert = new string[] { "NO_PO",     "CD_COMPANY","CD_PLANT",    "CD_PARTNER","DT_PO",
                                                     "CD_PURGRP", "NO_EMP",    "CD_TPPO",     "FG_UM",     "FG_PAYMENT",
                                                     "FG_TAX",    "TP_UM_TAX", "CD_PJT",      "CD_EXCH",   "RT_EXCH",
                                                     "AM_EX",     "AM",        "VAT",         "DC50_PO",   "TP_PROCESS", 
                                                     "FG_TAXP",   "YN_AM",     "DTS_INSERT",  "ID_USER", "FG_TRANS",
                                                     "FG_TRACK",  "DC_RMK2",   "TP_TRANSPORT","COND_PAY",  "COND_PAY_DLV",
                                                     "COND_PRICE","ARRIVER",   "LOADING",     "YN_BUDGET", "BUDGET_PASS",
                                                     "COND_PRICE_DLV", "CD_ARRIVER", "CD_LOADING", "DC_RMK_TEXT", "CD_AGENCY",
                                                     "AM_NEGO", "COND_SHIPMENT", "FREIGHT_CHARGE", "DC_RMK_TEXT2", "STND_PAY",
                                                     "COND_DAYS", "CD_ORGIN","DELIVERY_TERMS","DELIVERY_TIME","VALIDITY","TP_PACKING",
                                                     "DELIVERY_COST","INSPECTION","DOCUMENT_REQUIRED","SUPPLIER","MANUFACTURER","NO_ORDER",
                                                     "NM_PACKING",
                                                     "SHIP_DATE","DACU_NO","TP_GR","DT_PROCESS_IV","DT_PAY_PRE_IV","DT_DUE_IV","FG_PAYBILL_IV","CD_DOCU_IV","AM_K_IV","VAT_TAX_IV",
                                                     "AM_EX_IV",
                                                     "TXT_USERDEF4","DC_RMK_IV"
                };
                si01.SpParamsUpdate = new string[] { "NO_PO",    "CD_COMPANY", "DT_PO", "CD_PURGRP", "NO_EMP", "CD_PJT", 
                                                     "AM_EX",    "AM",         "VAT",   "DC50_PO",   "FG_TAXP","DTS_INSERT", 
                                                     "ID_USER","DC_RMK2",    "TP_TRANSPORT","COND_PAY",  "COND_PAY_DLV",
                                                     "COND_PRICE","ARRIVER",   "LOADING",   "YN_BUDGET",    "BUDGET_PASS",
                                                     "COND_PRICE_DLV", "CD_ARRIVER", "CD_LOADING", "DC_RMK_TEXT", "FG_PAYMENT",
                                                     "COND_SHIPMENT", "FREIGHT_CHARGE", "DC_RMK_TEXT2" , "STND_PAY" ,  "COND_DAYS", 
                                                     "CD_ORGIN","DELIVERY_TERMS","DELIVERY_TIME","VALIDITY","TP_PACKING",
                                                     "DELIVERY_COST","INSPECTION","DOCUMENT_REQUIRED","SUPPLIER","MANUFACTURER","NO_ORDER",
                                                     "NM_PACKING",
                                                     "SHIP_DATE","DACU_NO" };
                sc.Add(si01);

                si01.SpParamsValues.Add(ActionState.Insert, "ID_USER", Global.MainFrame.LoginInfo.UserID);
                si01.SpParamsValues.Add(ActionState.Update, "ID_USER", Global.MainFrame.LoginInfo.UserID);
            }


            if (dtL != null)
            {
                SpInfo si02 = new SpInfo();

                //dtL.RemotingFormat = SerializationFormat.Binary;

                if (구분 == "COPY")
                    si02.DataState = DataValueState.Added;

                si02.DataValue = dtL;
                si02.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si02.UserID = Global.MainFrame.LoginInfo.UserID;
                si02.SpNameInsert = "UP_PU_POL_INSERT";			//Insert 프로시저명
                si02.SpNameUpdate = "UP_PU_POL_UPDATE";			//Update 프로시저명
                si02.SpNameDelete = "UP_PU_POL_DELETE";
                si02.SpParamsInsert = new string[] { 	"NO_PO", "NO_LINE","CD_COMPANY","CD_PLANT","NO_CONTRACT","NO_CTLINE",
                                                        "NO_PR", "NO_PRLINE","FG_TRANS","CD_ITEM", "CD_UNIT_MM","FG_RCV","FG_PURCHASE",
                                                        "DT_LIMIT","QT_PO_MM","QT_PO","QT_REQ","QT_RCV","FG_TAX","UM_EX_PO","UM_EX","AM_EX","UM","AM",
														"VAT","CD_SL","FG_POST","FG_POCON","YN_RCV","YN_AUTORCV","YN_RETURN","YN_ORDER",
                                                        "YN_SUBCON","YN_IMPORT","RT_PO","YN_REQ","CD_PJT","NO_APP","NO_APPLINE","DC1", "DC2", 
                                                        "SEQ_PROJECT","UMVAT_PO","AMVAT_PO","CD_CC",
                                                        "CD_BUDGET","CD_BGACCT","NO_SO","NO_SOLINE","GI_PARTNER","DC3",
				                                        "DUMMY_TEMP_P_NO_VMI",
				                                        "DUMMY_TEMP_P_SEQ_VMI",
				                                        "DUMMY_TEMP_P_NO_IO_MGMT",
				                                        "DUMMY_TEMP_P_NO_IOLINE_MGMT",
				                                        "DT_PLAN",
				                                        "DC4",
                                                        "UM_EX_AR",
                                                        "NO_WBS",
                                                        "NO_CBS",
                                                        "CD_USERDEF1",
                                                        "CD_USERDEF2",
                                                        "NM_USERDEF1",
                                                        "NM_USERDEF2",
                                                        "DUMMY_TEMP_NO_PREIV",
                                                        "DUMMY_TEMP_NO_PREIVLINE",
                                                        "TP_UM_TAX",
                                                        "DT_EXDATE",
                                                        "CD_ITEM_ORIGIN",
                                                        "AM_EX_TRANS",
                                                        "AM_TRANS",
                                                        "NO_LINE_PJTBOM",
                                                        "FG_PACKING", "NUM_USERDEF1",
                                                        "AM_REBATE_EX","AM_REBATE","UM_REBATE","ID_INSERT",
                                                        "DATE_USERDEF1","DATE_USERDEF2","TXT_USERDEF1","TXT_USERDEF2","CDSL_USERDEF1",
                                                        "NUM_USERDEF2", "CLS_L","CLS_M","CLS_S", "GRP_ITEM", 
                                                        "NUM_STND_ITEM_1", "NUM_STND_ITEM_2", "NUM_STND_ITEM_3", "NUM_STND_ITEM_4", "NUM_STND_ITEM_5",
                                                        "UM_WEIGHT", "WEIGHT","TOT_WEIGHT", "STND_ITEM","NO_RELATION","SEQ_RELATION"
                };

				si02.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_P_NO_VMI", string.Empty);
				si02.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_P_SEQ_VMI", 0);
				si02.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_P_NO_IO_MGMT", string.Empty);
				si02.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_P_NO_IOLINE_MGMT", 0);
                si02.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_NO_PREIV", string.Empty);
                si02.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_NO_PREIVLINE", 0);


                si02.SpParamsUpdate = new string[] {    "NO_PO", "NO_LINE","CD_COMPANY","CD_PLANT","CD_UNIT_MM","DT_LIMIT","QT_PO_MM","QT_PO",
                                                        "UM_EX_PO","UM_EX","AM_EX","UM","AM", "VAT","CD_SL","RT_PO","CD_PJT", "DC1", "DC2", "UMVAT_PO", "AMVAT_PO", "CD_CC",
                                                        "CD_BUDGET","CD_BGACCT","GI_PARTNER", "DC3", "DT_PLAN", "DC4", "UM_EX_AR","NO_WBS","NO_CBS","CD_ITEM",
                                                        "CD_USERDEF1", "CD_USERDEF2", "NM_USERDEF1", "NM_USERDEF2","TP_UM_TAX","DT_EXDATE",
                                                        "CD_ITEM_ORIGIN","AM_EX_TRANS", "AM_TRANS","NO_LINE_PJTBOM","FG_PACKING", "NUM_USERDEF1",
                                                        "AM_REBATE_EX","AM_REBATE","UM_REBATE","ID_INSERT",
                                                        "DATE_USERDEF1","DATE_USERDEF2","TXT_USERDEF1","TXT_USERDEF2","CDSL_USERDEF1","FG_TAX",
                                                        "NUM_USERDEF2", "CLS_L","CLS_M","CLS_S", "GRP_ITEM", 
                                                        "NUM_STND_ITEM_1", "NUM_STND_ITEM_2", "NUM_STND_ITEM_3", "NUM_STND_ITEM_4", "NUM_STND_ITEM_5",
                                                        "UM_WEIGHT", "WEIGHT","TOT_WEIGHT", "STND_ITEM","NO_RELATION","SEQ_RELATION"};
                si02.SpParamsDelete = new string[] { "NO_PO", "NO_LINE", "CD_COMPANY" };
                sc.Add(si02);

            }
            if(lb_RevSave) //자동가입고
            {
                if (dtL != null && dtH != null)
                {
                    SpInfo si07 = new SpInfo();

                    si07.DataState = DataValueState.Added;
                    si07.DataValue = dtH;
                    si07.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    si07.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                    si07.SpNameInsert = "UP_PU_REV_INSERT_AUTO";			//Insert 프로시저명			

                    si07.SpParamsInsert = new string[] { "CD_COMPANY", "NO_PO", "ID_INSERT", "DT_PO", "CD_PARTNER", "CD_EXCH", "RT_EXCH", "NO_EMP" };


                    sc.Add(si07);
                }
            }

            if (lb_RcvSave)  // 자동의뢰 
            {
                if (dt_RCVH != null && dt_RCVL != null && dt_RCVH.Rows.Count > 0 && dt_RCVL.Rows.Count > 0)
                {
                    SpInfo si03 = new SpInfo();
                    si03.DataValue = dt_RCVH; 					//저장할 데이터 테이블
                    si03.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    si03.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                    si03.SpNameInsert = "UP_PU_RCVH_INSERT";			//Insert 프로시저명
                    si03.SpNameUpdate = "UP_PU_RCVH_UPDATE";			//Update 프로시저명					

                    /*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/
                    si03.SpParamsInsert = new string[] { "NO_RCV", "CD_COMPANY", "CD_PLANT", "CD_PARTNER", "DT_REQ", 
                                                         "NO_EMP", "FG_TRANS", "FG_PROCESS", "CD_EXCH", "CD_SL", 
                                                         "YN_RETURN", "YN_AM", "DC_RMK", "ID_INSERT", "FG_RCV"};
                    si03.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_RCV", "DC_RMK", "ID_INSERT" };
                    /*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/

                    sc.Add(si03);

                    SpInfo si04 = new SpInfo();
                    si04.DataValue = dt_RCVL; 					//저장할 데이터 테이블
                    si04.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    si04.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                    si04.SpNameInsert = "UP_PU_REQ_INSERT";			//Insert 프로시저명


                    /*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/
                    si04.SpParamsInsert = new string[] { 	 "NO_RCV","NO_LINE","CD_COMPANY","NO_PO","NO_POLINE", "CD_PURGRP","DT_LIMIT","CD_ITEM",
															 "QT_REQ" ,"YN_INSP" ,"QT_PASS" ,"QT_REJECTION" ,"CD_UNIT_MM","QT_REQ_MM" ,"CD_EXCH",
															 "RT_EXCH" ,"UM_EX_PO" ,"UM_EX" ,"AM_EX" ,/*"AM_EXREQ" ,*/"UM" ,"AM" ,/*"AM_REQ" ,*/"VAT" ,
															 "RT_CUSTOMS" ,"CD_PJT","YN_PURCHASE","YN_RETURN","FG_TPPURCHASE","FG_RCV",
															 "FG_TRANS","FG_TAX", "FG_TAXP","YN_AUTORCV", "YN_REQ","CD_SL","NO_LC","NO_LCLINE" ,
															 "RT_SPEC" ,"NO_EMP","NO_TO","NO_TO_LINE","CD_PLANT","CD_PARTNER", "DT_REQ", "DC_RMK","TP_UM_TAX"	};
                    si04.SpParamsValues.Add(ActionState.Insert, "DC_RMK", string.Empty);

                    /*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/

                    sc.Add(si04);

                    // 자동 입고 이면.
                    if (dt_RCVH.Rows[0]["YN_AUTORCV"].ToString() == "Y" && dt_RCVH.Rows[0].RowState.ToString() == "Added")
                    {
                        string no_ioseq = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "PU", "06", dtH.Rows[0]["DT_PO"].ToString().Substring(0, 6));

                        SpInfo si05 = new SpInfo();

                        si05.DataValue = dt_RCVH;  					//저장할 데이터 테이블
                        si05.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                        si05.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                        si05.SpNameInsert = "UP_PU_MM_QTIOH_INSERT";			//Insert 프로시저명

                        /*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/
                        si05.SpParamsInsert = new string[] { "NO_IO1", "CD_COMPANY", "CD_PLANT", "CD_PARTNER", "FG_TRANS", "YN_RETURN", "DT_IO", "GI_PARTNER", "CD_DEPT", "NO_EMP", "DC_RMK", "ID_INSERT", "FG_RCV" };

                        /*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/
                        si05.SpParamsValues.Add(ActionState.Insert, "NO_IO1", no_ioseq);
                        sc.Add(si05);

                        SpInfo si06 = new SpInfo();
                        //	si.DataState = DataValueState.Added; 
                        si06.DataValue = dt_RCVL; 					//저장할 데이터 테이블
                        si06.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                        si06.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                        si06.SpNameInsert = "UP_PU_GR_INSERT";			//Insert 프로시저명
                        //si.spType = "Added";

                        /*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/
                        si06.SpParamsInsert = new string[] { "YN_RETURN", "NO_IO1","NO_LINE", "CD_COMPANY", "CD_PLANT", "CD_SL", "DT_IO", "NO_RCV", "NO_LINE", "NO_PO", "NO_POLINE", "FG_PS1", 
																 "FG_TPPURCHASE", "FG_IO1", "FG_RCV", "FG_TRANS", "FG_TAX", "CD_PARTNER","CD_ITEM", "QT_REQ","QT_BAD1", "CD_EXCH", "RT_EXCH", "UM_EX", "UM", "VAT", "FG_TAXP",
																 "YN_AM1", "CD_PJT", "NO_LC", "NO_LCLINE", "NO_EMP1", "CD_PURGRP","CD_UNIT_MM", "QT_REQ_MM","QT_BAD_MM1", "UM_EX_PO", "YN_INSP","TP_UM_TAX"};
                        /*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/
                        si06.SpParamsValues.Add(ActionState.Insert, "YN_AM1", dtH.Rows[0]["YN_AM"].ToString());
                        si06.SpParamsValues.Add(ActionState.Insert, "NO_IO1", no_ioseq);
                        si06.SpParamsValues.Add(ActionState.Insert, "FG_PS1", "1");
                        si06.SpParamsValues.Add(ActionState.Insert, "FG_IO1", "001");
                        si06.SpParamsValues.Add(ActionState.Insert, "NO_EMP1", dtH.Rows[0]["NO_EMP"].ToString());
                        si06.SpParamsValues.Add(ActionState.Insert, "QT_BAD1", 0);
                        si06.SpParamsValues.Add(ActionState.Insert, "QT_BAD_MM1", 0);
                        sc.Add(si06);
                    }
                }

            }

            //_PU_BUDGET_HST를 일단 소거하고 다시 INSERT한다.
            Global.MainFrame.ExecSp("UP_PU_BUDGET_HST_DELETE", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 구매발주번호, "PU_PO_REG" });

            if (dt_budget != null && dt_budget.Rows.Count > 0)
            {
                SpInfo si03 = new SpInfo();
                si03.DataValue = dt_budget;
                si03.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si03.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si03.SpNameInsert = "UP_PU_BUDGET_HST_INSERT";
                //si03.SpNameUpdate = "none";
                //si03.SpNameDelete = "none";
                si03.SpParamsInsert = new string[] { "CD_COMPANY","NO_PU","NENU_TYPE", /*"NO_HST",*/
                                                    "CD_BUDGET","NM_BUDGET","CD_BGACCT","NM_BGACCT",
                                                    "AM_ACTSUM","AM_JSUM","RT_JSUM","AM","AM_JAN",
                                                    "TP_BUNIT","ERROR_MSG","ID_INSERT"  };
                sc.Add(si03);
            }

            if (si_subinfo.DataValue != null)
            {
                si_subinfo.SpParamsValues.Add(ActionState.Insert, "NO_PK", 구매발주번호);
                sc.Add(si_subinfo);

            }
            if (dtDD != null)
            {

                SpInfo siDD = new SpInfo();

                if (구분 == "COPY")
                    siDD.DataState = DataValueState.Added;

                siDD.DataValue = dtDD;
                siDD.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siDD.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siDD.SpNameInsert = "UP_PU_POLL_INSERT";
                siDD.SpNameUpdate = "UP_PU_POLL_UPDATE";
                siDD.SpNameDelete = "UP_PU_POLL_DELETE";
                siDD.SpParamsInsert = new string[] { "CD_COMPANY", "CD_PLANT", "NO_PO", "NO_POLINE", "NO_LINE", "CD_MATL", "QT_NEED", "QT_NEED_UNIT", "ID_INSERT" };
                siDD.SpParamsUpdate = new string[] { "CD_COMPANY", "CD_PLANT", "NO_PO", "NO_POLINE", "NO_LINE", "CD_MATL", "QT_NEED", "QT_NEED_UNIT", "ID_UPDATE" };
                siDD.SpParamsDelete = new string[] { "CD_COMPANY", "CD_PLANT", "NO_PO", "NO_POLINE", "NO_LINE" };
                sc.Add(siDD);
            }

            if (dtTH != null)
            {

                SpInfo siTH = new SpInfo();

                //if (구분 == "COPY")
                //    siDD.DataState = DataValueState.Added;

                siTH.DataValue = dtTH;
                siTH.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;

                siTH.SpNameInsert = "UP_PU_Z_TOPES_PO_TAB_I";
                siTH.SpNameUpdate = "UP_PU_Z_TOPES_PO_TAB_U";
                siTH.SpNameDelete = "UP_PU_Z_TOPES_PO_TAB_D";
                siTH.SpParamsInsert = new string[] { "CD_COMPANY", "CD_PLANT", "NO_PO", "SQ_1", "FG_IV", "DT_IV_PLAN", "RT_IV", "AM", "VAT", "DT_BAN_PLAN", "RT_BAN", "AM_BAN", "AM_BANK", "SERVERKEY", "ID" };
                siTH.SpParamsUpdate = new string[] { "CD_COMPANY", "CD_PLANT", "NO_PO", "SQ_1", "FG_IV", "DT_IV_PLAN", "RT_IV", "AM", "VAT", "DT_BAN_PLAN", "RT_BAN", "AM_BAN", "AM_BANK", "SERVERKEY", "ID" };
                siTH.SpParamsDelete = new string[] { "CD_COMPANY", "NO_PO", "SQ_1","SERVERKEY" };

                siTH.SpParamsValues.Add(ActionState.Insert, "SERVERKEY", Global.MainFrame.ServerKeyCommon);
                siTH.SpParamsValues.Add(ActionState.Update, "SERVERKEY", Global.MainFrame.ServerKeyCommon);
                siTH.SpParamsValues.Add(ActionState.Delete, "SERVERKEY", Global.MainFrame.ServerKeyCommon);
                siTH.SpParamsValues.Add(ActionState.Insert, "ID", Global.MainFrame.LoginInfo.UserID);
                siTH.SpParamsValues.Add(ActionState.Update, "ID", Global.MainFrame.LoginInfo.UserID);


                sc.Add(siTH);
      
            }



            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sc);
            for (int i = 0; i < rtn.Length; i++)
                if (!rtn[i].Result) return false;

            return true;
        }


        #endregion

        #endregion

        #region ♣ 엑셀 관련

        #region -> 예전 소스
        //internal DataTable ExcelColAdd(string 공장, string 멀티품목코드, DataTable 엑셀)
        //{
        //    ArrayListExt arrList = arr엑셀( 멀티품목코드 );
        //    DataTable dt결과 = null;
        //    for ( int k = 0 ; k < arrList.Count ; k++ )
        //    {
        //        SpInfo si = new SpInfo();
        //        si.SpNameSelect = "UP_PU_PO_EXCEL_SELECT";
        //        si.SpParamsSelect = new object [] { Global.MainFrame.LoginInfo.CompanyCode, 공장, arrList [k].ToString() };
        //        ResultData rtn = ( ResultData )Global.MainFrame.FillDataTable( si );
        //        DataTable dt = ( DataTable )rtn.DataValue;                                                                           

        //        if ( dt결과 == null )
        //            dt결과 = dt;
        //        else
        //        {
        //            foreach ( DataRow row in dt.Rows )
        //                dt결과.ImportRow( row );
        //        }
        //    }

        //    DataTable 엑셀결과 = 엑셀컬럼추가( 엑셀 );

        //    foreach ( DataRow row in 엑셀결과.Rows )  /* DB값(dt결과)을 엑셀에 Matching */
        //    {
        //        DataRow [] NewRow = dt결과.Select( "CD_ITEM = '" + row ["CD_ITEM"].ToString() + "'" );

        //        //if ( NewRow.Length == 0 )
        //        //{
        //        //    엑셀결과 = null;
        //        //    return 엑셀결과;
        //        //}
        //        if (NewRow.Length == 0)
        //            return 엑셀결과;

        //        row ["CD_ITEM"] = NewRow [0] ["CD_ITEM"].ToString();
        //        row ["NM_ITEM"] = NewRow [0] ["NM_ITEM"].ToString();
        //        row ["STND_ITEM"] = NewRow [0] ["STND_ITEM"].ToString();
        //        row ["UNIT_PO"] = NewRow [0] ["UNIT_PO"].ToString();
        //        row ["UNIT_IM"] = NewRow [0] ["UNIT_IM"].ToString();
        //        row ["CD_SL"] = NewRow [0] ["CD_SL"].ToString();
        //        row ["NM_SL"] = NewRow [0] ["NM_SL"].ToString();
        //        row ["UNIT_PO_FACT"] = NewRow [0] ["UNIT_PO_FACT"].ToString();
        //    }

        //    return 엑셀결과;
        //}

        //private DataTable 엑셀컬럼추가(DataTable 엑셀)
        //{
        //    엑셀.Columns.Add( "NM_ITEM", typeof( string ) );
        //    엑셀.Columns.Add( "STND_ITEM", typeof( string ) );
        //    엑셀.Columns.Add( "UNIT_PO", typeof( string ) );
        //    엑셀.Columns.Add( "UNIT_IM", typeof( string ) );


        //    엑셀.Columns.Add( "NO_PO", typeof( string ) );
        //    엑셀.Columns.Add( "NO_LINE", typeof( string ) );
        //    엑셀.Columns.Add( "CD_PJT", typeof( string ) );
        //    엑셀.Columns.Add( "NM_PJT", typeof( string ) );
        //    엑셀.Columns.Add( "NM_SYSDEF", typeof( string ) );
        //    엑셀.Columns.Add( "UM_EX_PO", typeof( string ) );
        //    엑셀.Columns.Add( "AM_EX", typeof( decimal ) );
        //    엑셀.Columns.Add( "QT_PO", typeof( decimal ) );
        // //   UM_EX_PO    AM_EX

        //    엑셀.Columns.Add( "FG_TRANS", typeof( string ) );
        //    엑셀.Columns.Add( "FG_TPPURCHASE", typeof( string ) );
        //    엑셀.Columns.Add( "YN_AUTORCV", typeof( string ) );
        //    엑셀.Columns.Add( "YN_RCV", typeof( string ) );
        //    엑셀.Columns.Add( "YN_RETURN", typeof( string ) );
        //    엑셀.Columns.Add( "YN_IMPORT", typeof( string ) );
        //    엑셀.Columns.Add( "YN_ORDER", typeof( string ) );
        //    엑셀.Columns.Add( "YN_REQ", typeof( string ) );
        //    엑셀.Columns.Add( "FG_RCV", typeof( string ) );
        //    엑셀.Columns.Add( "YN_SUBCON", typeof( string ) );
        //    엑셀.Columns.Add( "FG_PURCHASE", typeof( string ) );
        //    엑셀.Columns.Add( "FG_TAX", typeof( string ) );
        //    엑셀.Columns.Add( "NO_PR", typeof( string ) );
        //    엑셀.Columns.Add( "CD_EXCH", typeof( string ) );

        //    엑셀.Columns.Add( "CD_SL", typeof( string ) );
        //    엑셀.Columns.Add( "NM_SL", typeof( string ) );
        //    엑셀.Columns.Add( "TP_ITEM", typeof( string ) );
        //    엑셀.Columns.Add( "UNIT_PO_FACT", typeof( string ) );

        //    return 엑셀;
        //}
        #endregion

        internal DataTable 공장품목(string 멀티품목코드, String 공장)
        {
            ArrayListExt arrList = arr엑셀(멀티품목코드);
            DataTable dt_DB결과 = null;

            for (int k = 0; k < arrList.Count; k++)
            {
                SpInfo si = new SpInfo();
                si.SpNameSelect = "UP_PU_PO_EXCEL_SELECT";
                si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, 공장, arrList[k].ToString() };
                ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);
                DataTable dt = (DataTable)rtn.DataValue;

                if (dt_DB결과 == null)
                {
                    dt_DB결과 = dt;
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                        dt_DB결과.ImportRow(row);
                }
            }
            return dt_DB결과;
        }

        internal DataTable 엑셀(DataTable dt_엑셀) //엑셀컬럼추가(DataTable dt_엑셀) // (DataTable 엑셀)
        {
            if (!dt_엑셀.Columns.Contains("NM_ITEM"))
            {
                dt_엑셀.Columns.Add("NM_ITEM", typeof(string));
                dt_엑셀.Columns.Add("STND_ITEM", typeof(string));
                dt_엑셀.Columns.Add("UNIT_PO", typeof(string));
                dt_엑셀.Columns.Add("UNIT_IM", typeof(string));


                dt_엑셀.Columns.Add("NO_PO", typeof(string));
                dt_엑셀.Columns.Add("NO_LINE", typeof(string));
                //dt_엑셀.Columns.Add("CD_PJT", typeof(string));
                dt_엑셀.Columns.Add("NM_PJT", typeof(string));
                dt_엑셀.Columns.Add("NM_SYSDEF", typeof(string));
                if (!dt_엑셀.Columns.Contains("UM_EX_PO"))
                    dt_엑셀.Columns.Add("UM_EX_PO", typeof(string));
                if (!dt_엑셀.Columns.Contains("AM_EX"))
                    dt_엑셀.Columns.Add("AM_EX", typeof(decimal));
                dt_엑셀.Columns.Add("QT_PO", typeof(decimal));
                //   UM_EX_PO    AM_EX

                dt_엑셀.Columns.Add("FG_TRANS", typeof(string));
                dt_엑셀.Columns.Add("FG_TPPURCHASE", typeof(string));
                dt_엑셀.Columns.Add("YN_AUTORCV", typeof(string));
                dt_엑셀.Columns.Add("YN_RCV", typeof(string));
                dt_엑셀.Columns.Add("YN_RETURN", typeof(string));
                dt_엑셀.Columns.Add("YN_IMPORT", typeof(string));
                dt_엑셀.Columns.Add("YN_ORDER", typeof(string));
                dt_엑셀.Columns.Add("YN_REQ", typeof(string));
                dt_엑셀.Columns.Add("FG_RCV", typeof(string));
                dt_엑셀.Columns.Add("YN_SUBCON", typeof(string));
                dt_엑셀.Columns.Add("FG_PURCHASE", typeof(string));
                dt_엑셀.Columns.Add("FG_TAX", typeof(string));
                dt_엑셀.Columns.Add("NO_PR", typeof(string));
                dt_엑셀.Columns.Add("CD_EXCH", typeof(string));

                dt_엑셀.Columns.Add("CD_SL", typeof(string));
                dt_엑셀.Columns.Add("NM_SL", typeof(string));
                dt_엑셀.Columns.Add("TP_ITEM", typeof(string));
                dt_엑셀.Columns.Add("UNIT_PO_FACT", typeof(string));
            }


            return dt_엑셀;
        }

        private ArrayListExt arr엑셀(string 멀티품목코드)
        {
            int MaxCnt = 50;
            int Cnt = 1;
            string 품목코드 = string.Empty;

            ArrayListExt arrList = new ArrayListExt();
            string[] arrstr = 멀티품목코드.Split('|');

            for (int i = 0; i < arrstr.Length - 1; i++)
            {
                품목코드 += arrstr[i].ToString() + "|";
                if (Cnt == MaxCnt)
                {
                    arrList.Add(품목코드);
                    품목코드 = string.Empty;
                    Cnt = 0;
                }
                Cnt++;
            }

            if (품목코드 != string.Empty)
                arrList.Add(품목코드);
            return arrList;
        }

        #endregion

        #region ♣ 전자결제관련

        #region 전자결제 상신여부 조회 :
        public int GetFI_GWDOCU(string p_no_po)
         {
            int rtn_value = 999;
            string SelectQuery = "SELECT ST_STAT" +
                                "  FROM FI_GWDOCU" +
                                " WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                "  AND CD_PC = '" + Global.MainFrame.LoginInfo.CdPc + "'" +
                                "  AND NO_DOCU = '" + p_no_po + "'"
                                ;


            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    rtn_value = Convert.ToInt32(dt.Rows[0]["ST_STAT"].ToString());
                }

            }

            return rtn_value;
        }

        public DataTable GetFI_GWDOCU_L(string p_no_po)
        {
            string SelectQuery =  "SELECT ST_STAT" +
                                  "  FROM MA_GWDOCU_DATA" +
                                  " WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                  "  AND CD_TABLE = 'PU_POL'" +
                                  "  AND NO_DOCU = '" + p_no_po + "'" +
                                  " GROUP BY ST_STAT"
                                  ;
            

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            return dt;
        }
        #endregion

        #region -> 108 개발서버 결재상신

        public bool 전자결재_108(DataRow drHeader, /*DataTable dtLine,*/ string Html_Code/*, bool 최종결재상신체크여부*/)
        {
            string YN_REPORT = string.Empty;

            string strFI_GWDOCU = @"INSERT FI_GWDOCU(CD_COMPANY, CD_PC, NO_DOCU, ID_WRITE, DT_ACCT, NM_PUMM, NM_NOTE, ST_STAT, AMT, APP_FORM_KIND)
                                    VALUES('" + Global.MainFrame.LoginInfo.CompanyCode + "', '" +
                                                Global.MainFrame.LoginInfo.CdPc + "', '" +
                                                drHeader["NO_PO"].ToString() + "', '" +
                                                drHeader["NO_EMP"].ToString() + "', '" +
                                                drHeader["DT_PO"].ToString() + "', '', '" +
                                                Html_Code.Replace("'", "''") + @"', '0', 0, 3000)

                                    UPDATE FI_GWDOCU
                                    SET ST_STAT = '1'
                                    WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
                                        AND NO_DOCU = '" + drHeader["NO_PO"].ToString() + @"'
                                        AND CD_PC = '" + Global.MainFrame.LoginInfo.CdPc + @"' 
                                    ";



            Global.MainFrame.ExecuteScalar(strFI_GWDOCU);
            return true;
        }

        #endregion

        #region -> 결재_실제사용

        public bool 전자결재_실제사용(DataRow drHeader, string Html_Code, string App_Form_Kind, string Nm_Pumm)
        {
            object[] obj = new object[9];
            obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
            obj[1] = Global.MainFrame.LoginInfo.CdPc;
            obj[2] = drHeader["NO_PO"].ToString();
            obj[3] = drHeader["NO_EMP"].ToString();
            obj[4] = drHeader["DT_PO"].ToString();
            obj[5] = App_Form_Kind;// "3010";  // 이건 정해진것
            obj[6] = Html_Code;
            obj[7] = Nm_Pumm; //nm_pumm
            obj[8] = 테이블구분.PU_POH.GetHashCode();

            ResultData result = (ResultData)Global.MainFrame.ExecSp("UP_PU_GWDOCU", obj); //업데이트 트리거도 같이 고쳐야함
            return result.Result;
        }

        public bool 전자결재_실제사용_L(DataTable dt)
        {
            SpInfoCollection sc = new SpInfoCollection();

            SpInfo si01 = new SpInfo();
            SpInfo si02 = new SpInfo();

            if (dt != null)
            {
                DataTable dtTop = dt.Clone();

                dtTop.LoadDataRow(dt.Rows[0].ItemArray, false);

                si01.DataState = DataValueState.Added;
                si01.DataValue = dtTop;
                si01.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si01.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si01.SpNameInsert = "UP_PU_GWDOCU_DATA_RESET";
                si01.SpParamsInsert = new string[] { "CD_COMPANY", "NO_DOCU", "CD_TABLE", "FG_MODULE" };

                sc.Add(si01);

                si02.DataState = DataValueState.Added;
                si02.DataValue = dt;
                si02.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si02.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si02.SpNameInsert = "UP_PU_GWDOCU_DATA";
                si02.SpParamsInsert = new string[] {"CD_COMPANY", "NO_DOCU", "NO_DOCULINE", "CD_TABLE", "NM_HEAD_DATA", "NM_MIDDLE_DATA",
								                    "NM_BOTTOM_DATA", "NO_EMP", "APP_FORM_KIND", "FG_MODULE","ID_INSERT"};

                sc.Add(si02);
            }



            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sc);
            for (int i = 0; i < rtn.Length; i++)
                if (!rtn[i].Result) return false;

            return true;

        }

        #endregion




        #region -> 전자결재 데이터 출력 
        public DataTable DataSearch_GW_RPT(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_PU_PO_REG2_GW_RPT", obj);

            return dt;
        }

        public DataTable DataSearch_GW_RPT_ONLY(object[] obj)
        {

            DataTable dt = null;

            if (D.GetString(obj[4]) == "WONIK")
                dt = DBHelper.GetDataTable("UP_PU_PO_REG2_GW_RPT_WONIK", new object[] { D.GetString(obj[0]), D.GetString(obj[1]), D.GetString(obj[2]), D.GetString(obj[3]) });

            else if(D.GetString(obj[4]) == "SAMTECH")
                dt = DBHelper.GetDataTable("UP_PU_PO_REG_PRINT_SAMTECH", new object[] { D.GetString(obj[0]), D.GetString(obj[1]) });
            //else if (D.GetString(obj[4]) == "DEMAC")
            //    dt = DBHelper.GetDataTable("UP_PU_PO_REG_PRINT_DMC", new object[] { D.GetString(obj[0]), D.GetString(obj[1]) });
            else
                dt = DBHelper.GetDataTable("UP_PU_PO_REG2_GW_RPT_SANTEC", new object[] { D.GetString(obj[0]), D.GetString(obj[1])});

            return dt;
        }

        #endregion

        #endregion

        #region ♣ 기타조회

        #region 전용코드 조회 : 영우 전용설정
        public DataTable GetPartnerCodeSearch()
        {
            string SelectQuery = " SELECT CD_EXC " +
                                 "   FROM MA_EXC " +
                                 "  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                 "    AND EXC_TITLE = '여신한도' ";

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            return dt;
        }
        #endregion

        #region 사업장명 조회
        public DataTable GetBizAreaCodeSearch(string arg_no_emp)
        {
            string ls_nm_bizarea = string.Empty;
            if (arg_no_emp == string.Empty)
                return null;

            string SelectQuery = "SELECT  B.NM_BIZAREA, B.NO_BIZAREA, B.NO_COMPANY " +
                                 "  FROM MA_EMP A" +
                                 "       INNER JOIN MA_BIZAREA B ON A.CD_BIZAREA = B.CD_BIZAREA AND A.CD_COMPANY = B.CD_COMPANY" +
                                 " WHERE A.NO_EMP = '" + arg_no_emp + "'" +
                                 "   AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";


            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);


            //if (dt.Rows.Count > 0)
            //{

            //    if (dt.Rows[0]["NM_BIZAREA"] != System.DBNull.Value && dt.Rows[0]["NM_BIZAREA"].ToString().Trim() != String.Empty)
            //    {
            //        ls_nm_bizarea = dt.Rows[0]["NM_BIZAREA"].ToString().Trim();
            //    }
            //    else
            //        ls_nm_bizarea = string.Empty;
            //}

            return dt;
        }
        #endregion

        #region 환율정보조회
        public decimal ExchangeSearch(string dt_po, string nm_exch)
        {
            decimal ld_rate_base = 1;
            string SelectQuery = "SELECT RATE_BASE " +
                                 "  FROM MA_EXCHANGE " +
                                 " WHERE YYMMDD = '" + dt_po + "' " +
                                 "   AND CURR_SOUR = '" + nm_exch + "' " +
                                 "   AND CURR_DEST = '000' " +
                                 "   AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' "
                ;


            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["RATE_BASE"] != System.DBNull.Value && dt.Rows[0]["RATE_BASE"].ToString().Trim() != String.Empty)
                {
                    ld_rate_base = Convert.ToDecimal(dt.Rows[0]["RATE_BASE"]);
                }

                if (ld_rate_base == 0) ld_rate_base = 1;

            }

            return ld_rate_base;


        }
        #endregion

        #region 환경설정정보조회
        public string EnvSearch()
        {
            string ls_ContEdit = "N";
            string SelectQuery = "SELECT CD_TP " +
                                 "  FROM PU_ENV " +
                                 " WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' " +
                                 "   AND FG_TP = '001' "
                                 ;


            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["CD_TP"] != System.DBNull.Value && dt.Rows[0]["CD_TP"].ToString().Trim() != String.Empty)
                {
                    ls_ContEdit = dt.Rows[0]["CD_TP"].ToString();
                }
            }

            return ls_ContEdit;


        }
        #endregion

        #region CC명 조회
        public string GetCCCodeSearch(string arg_cd_cc)
        {
            string ls_nm_cc = string.Empty;
            if (arg_cd_cc == string.Empty)
                return "";

            string SelectQuery = "SELECT NM_CC " +
                                 "  FROM MA_CC " +
                                 " WHERE CD_CC = '" + arg_cd_cc + "'" +
                                 "   AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";


            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);


            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["NM_CC"] != System.DBNull.Value && dt.Rows[0]["NM_CC"].ToString().Trim() != String.Empty)
                {
                    ls_nm_cc = dt.Rows[0]["NM_CC"].ToString().Trim();
                }
            }

            return ls_nm_cc;
        }
        #endregion

        #region 그룹의 CC 조회
        public DataTable GetCD_CC_CodeSearch(string arg_cd_purgrp)
        {
            string ls_cd_cc = string.Empty;

            string SelectQuery = "SELECT A.CD_CC, MC.NM_CC " +
                                 "  FROM MA_PURGRP A " +
                                 "       LEFT OUTER JOIN MA_CC MC ON A.CD_CC = MC.CD_CC AND A.CD_COMPANY = MC.CD_COMPANY " +
                                 "WHERE A.CD_PURGRP = '" + arg_cd_purgrp + "'" +
                                 "  AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            return dt;
        }
        #endregion

        #region 프로젝트의 CC 조회
        public DataTable GetCD_CC_CodeSearch_pjt(string arg_cd_pjt)
        {
            string ls_cd_cc = string.Empty;

            string SelectQuery = "SELECT A.CD_CC, MC.NM_CC " +
                                 "  FROM SA_PROJECTH A " +
                                 "       LEFT OUTER JOIN MA_CC MC ON A.CD_CC = MC.CD_CC AND A.CD_COMPANY = MC.CD_COMPANY " +
                                 "WHERE A.NO_PROJECT = '" + arg_cd_pjt + "'" +
                                 "  AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            return dt;
        }
        #endregion

        #region 품목의 CC 조회
        public DataTable GetCD_CC_CodeSearch_cd_item(string str_cd_item, string str_cd_plant)
        {
            string ls_cd_cc = string.Empty;

            string SelectQuery = "SELECT A.CD_CC, MC.NM_CC " +
                                 "  FROM MA_PITEM A " +
                                 "       LEFT OUTER JOIN MA_CC MC ON A.CD_CC = MC.CD_CC AND A.CD_COMPANY = MC.CD_COMPANY " +
                                 "WHERE A.CD_ITEM = '" + str_cd_item + "'" +
                                 "   AND A.CD_PLANT = '" +  str_cd_plant  +"'" +
                                 "   AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            return dt;
        }
        #endregion

        #region 환경설정정보조회(CC관련)
        public DataTable EnvSearch_CC()
        {
            string ls_evn = string.Empty;
            string SelectQuery = "SELECT EXC_TITLE,CD_EXC " +
                                 "  FROM MA_EXC " +
                                 " WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'  " +
                                 "   AND(EXC_TITLE = '발주등록-C/C설정' OR  EXC_TITLE ='발주라인-C/C설정수정유무') " +
                                 "   AND CD_MODULE = 'PU'"
                                 ;
            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            return dt;

        }

        #endregion

        #region 발주유형, 구매그룹조회
        public DataSet Get_TPPO_PURGRP(Object [] obj)
        {
            DataSet ds = DBHelper.GetDataSet("UP_PU_TPPO_PURGRP_INFO_SELECT", obj);

            return ds;

        }        
        #endregion

        #region 구분코드 조회
        public string GetGubunCodeSearch(string p_cd_field, string p_cd_sysdef)
        {
            if (!b_first_clone)
            {
                _dt_fg_post.Columns.Add("CD_FIELD", typeof(string));
                _dt_fg_post.Columns.Add("CD_SYSDEF", typeof(string));
                _dt_fg_post.Columns.Add("NM_SYSDEF", typeof(string));
                b_first_clone = true;
            }
              
            string rtn_string = string.Empty;
            Object[] obj = new Object[3];
            obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
            obj[1] = p_cd_field;
            obj[2] = p_cd_sysdef;

            //있던건 디비에서 찾지말고 넘겨준다
            DataRow[] drRow = _dt_fg_post.Select("CD_FIELD = '" + p_cd_field + "' AND CD_SYSDEF = '" + p_cd_sysdef + "'");
            if (drRow.Length == 0)
            {
                DataTable dt_ma_code = Duzon.ERPU.MF.ComFunc.GetTableSearch("MA_CODEDTL", obj);
                foreach (DataRow dr in dt_ma_code.Rows)
                {
                    DataRow drnew = _dt_fg_post.NewRow();
                    drnew["CD_FIELD"] = D.GetString(dr["CD_FIELD"]);
                    drnew["CD_SYSDEF"] = D.GetString(dr["CD_SYSDEF"]);
                    drnew["NM_SYSDEF"] = D.GetString(dr["NM_SYSDEF"]);
                    _dt_fg_post.Rows.Add(drnew);
                }
                drRow = _dt_fg_post.Select("CD_FIELD = '" + p_cd_field + "' AND CD_SYSDEF = '" + p_cd_sysdef + "'");
            }

            if (drRow.Length > 0)
            {
                rtn_string = D.GetString(drRow[0]["NM_SYSDEF"]);
            }

            return rtn_string;
        }

        #endregion

        #region -> 거래처정보 가져오기 (전자결재할때 사용)
        public DataTable search_partner(string cd_partner)
        {
            string SelectQuery = "SELECT NO_TEL, NO_FAX,FG_PAYBILL, DC_ADS1_H, DC_ADS1_D, CD_EMP_PARTNER, NO_TEL, NO_FAX, E_MAIL " +
                                 "  FROM MA_PARTNER " +
                                 " WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'  " +
                                 "   AND CD_PARTNER = '" + cd_partner + "'";
                                 
                                 ;
            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            return dt;
        }
        #endregion

        public DataTable Get_PJTInfo(string  p_cd_pjt_pipe)
        {
            return DBHelper.GetDataTable("UP_PU_COMMON_PJT_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode, p_cd_pjt_pipe }  );
        }

        #endregion

        #region ♣ 출력
        public DataSet Print(string NO_PO)
        {

            object[] m_obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, NO_PO,Global.MainFrame.LoginInfo.UserID };
            DataSet ds = DBHelper.GetDataSet("UP_PU_PO_REG_PRINT", m_obj);

            return ds;


        }

        public DataTable Print_Detail(string NO_PO)
        {

            object[] m_obj = new object[] { Global.MainFrame.LoginInfo.CompanyCode, NO_PO, Global.MainFrame.LoginInfo.UserID };
            DataTable dt = DBHelper.GetDataTable("UP_PU_PO_REG_PRINT_L", m_obj);

            return dt;


        }

        #endregion

        #region ♣ 예산관련
        #region -> 예산체크
        public DataTable CheckBUDGET(string 예산단위, string 예산계정, string 체크일자)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("AM_ACTSUM", typeof(decimal)); //실행예산
            dt.Columns.Add("AM_JSUM", typeof(decimal)); //집행실적
            dt.Columns.Add("TP_BUNIT", typeof(string)); //예산통제구분
            dt.Columns.Add("ERROR_MSG", typeof(string)); //ERROR MSG 

            //리턴값 : OUT파라미터
            //첫번째값 : 실행예산금액
            //두번째값 : 집행예산금액
            //세번째값 : 예산통제구분
            //(예산통제구분값이 '4' 일경우 금액초과시 통제함)
            //예산통제구분 코드 "FI_B000009"

            ResultData _rtn = new ResultData();

            String err_msg = "";

            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_FI_BUDGET_ACTSUM_CHECK";
            si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, 예산단위, 예산계정, 체크일자 };

            try
            {
                _rtn = (ResultData)Global.MainFrame.FillDataTable(si);
            }
            catch (Exception ex)
            {
                err_msg = ex.Message;
            }

            DataRow NewRow;

            NewRow = dt.NewRow();


            if (err_msg != string.Empty)
            {
                NewRow["ERROR_MSG"] = err_msg;
            }
            else
            {

                NewRow["AM_ACTSUM"] = _rtn.OutParamsSelect[0, 0];
                NewRow["AM_JSUM"] = _rtn.OutParamsSelect[0, 1];
                NewRow["TP_BUNIT"] = _rtn.OutParamsSelect[0, 2].ToString();
            }

            dt.Rows.Add(NewRow);

            //if (_rtn.OutParamsSelect[0, 0] != null && _rtn.OutParamsSelect[0, 0].ToString() != "")
            //{
            //    if (_rtn.OutParamsSelect[0, 2].ToString() == "002")
            //    {
            //        if (Global.MainFrame.ShowMessage("여신금액을 초과하였습니다. 저장하시겠습니까 ?\n(여신총액 : " + _rtn.OutParamsSelect[0, 0].ToString() + ", 잔액 : " + _rtn.OutParamsSelect[0, 1].ToString() + ")", "QY2") == DialogResult.Yes)
            //            return true;
            //        else
            //            return false;
            //    }
            //    else if (_rtn.OutParamsSelect[0, 2].ToString() == "003")
            //    {
            //        Global.MainFrame.ShowMessage("여신금액을 초과하여 저장할 수 없습니다. \n(여신총액 : " + _rtn.OutParamsSelect[0, 0].ToString() + " 잔액 : " + _rtn.OutParamsSelect[0, 1].ToString() + ")");
            //        return false;
            //    }
            //}
            return dt;

        }
        #endregion
        //예산chk내역 조회
        public DataSet PU_BUDGET_HST_SELECT(string _no_pr)
        {
            ResultData rtn = (ResultData)Global.MainFrame.FillDataSet("UP_PU_BUDGET_HST_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode, _no_pr, "PU_PO_REG" });
            DataSet ds = (DataSet)rtn.DataValue;

            return (DataSet)rtn.DataValue;
        }

        //예산chk내역 서식
        internal DataTable PU_BUDGET_HST()
        {
            DataTable dt = new DataTable();

            //dt.Columns.Add("CD_COMPANY", typeof(string)); 
            dt.Columns.Add("NO_PU", typeof(string));
            dt.Columns.Add("NENU_TYPE", typeof(string));
            dt.Columns.Add("NO_HST", typeof(string));

            dt.Columns.Add("CD_BUDGET", typeof(string));
            dt.Columns.Add("NM_BUDGET", typeof(string));
            dt.Columns.Add("CD_BGACCT", typeof(string));
            dt.Columns.Add("NM_BGACCT", typeof(string));

            dt.Columns.Add("AM_ACTSUM", typeof(decimal)); //실행예산
            dt.Columns.Add("AM_JSUM", typeof(decimal)); //집행실적
            dt.Columns.Add("RT_JSUM", typeof(decimal)); //집행율
            dt.Columns.Add("AM", typeof(decimal)); //집행신청 (청구금액)
            dt.Columns.Add("AM_JAN", typeof(decimal)); //잔여예산
            dt.Columns.Add("TP_BUNIT", typeof(string)); //예산통제구분

            dt.Columns.Add("ERROR_MSG", typeof(string)); //ERROR_MSG


            dt.Columns.Add("DTS_INSERT", typeof(string));
            dt.Columns.Add("ID_INSERT", typeof(string));
            dt.Columns.Add("DTS_UPDATE", typeof(string));
            dt.Columns.Add("ID_UPDATE", typeof(string));


            return dt;
        }


        #endregion

        #region ♣ 구매품의자 메일정보 가져오기
        internal DataTable getMail_Adress(DataTable no_app)
        {
            DataTable dt = no_app.Clone();

            foreach (DataRow dr in no_app.Rows)
            {
                string SelectQuery = @"SELECT   PA.EMP_WRITE,
                                            ME.NM_KOR,
                                            ME.NO_EMAIL,
                                            ME.NO_EMP
                                   FROM     PU_APPH PA INNER JOIN MA_EMP ME ON PA.EMP_WRITE = ME.NO_EMP AND PA.CD_COMPANY = ME.CD_COMPANY 
                                   WHERE    PA.CD_COMPANY ='" + Global.MainFrame.LoginInfo.CompanyCode + @"'
                                   AND      NO_APP ='" + D.GetString(dr["NO_APP"]) + "'";
                DataTable merge = Global.MainFrame.FillDataTable(SelectQuery);
                if (merge != null && merge.Rows.Count != 0)
                    dt.Merge(merge);
            }

            return dt;

        }
        #endregion

        #region 저장하기전에 품목불러올때 품목별로 현재고를 가져오는 프로시져

        internal DataTable item_pinvn(object[] obj)
        {
            
            DataTable dt = DBHelper.GetDataTable("UP_PU_PINVN_USE_QT", obj);
            return dt;
        }

        #endregion

        #region - > 입고 단가 가져오기
        internal DataSet Search_um_prioritize_item(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("UP_PU_UM_TOTAL_SELECT", obj);
            return ds;
        }
        #endregion

        #region ♣ 의제매입 프로젝트별 세율코드가져오기
        internal string pjt_item_josun(string cd_pjt)
        {
            string SelectQuery = @"SELECT   CD_USERDEF13
                               FROM     SA_PROJECTH_SUB 
                               WHERE    CD_COMPANY ='" + Global.MainFrame.LoginInfo.CompanyCode + @"'
                               AND      NO_PROJECT ='" + cd_pjt + "'";
            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);


            if (dt == null || dt.Rows.Count == 0)
                return "";

            return D.GetString(dt.Rows[0]["CD_USERDEF13"]);

        }
        #endregion
         
        #region ♣ 메모  & 체크팬 
        internal void SaveContent(ContentType contentType, Dass.FlexGrid.CommandType commandType, string noIo, decimal no_ioline, string value)
        {
            string sqlQuery = string.Empty;
            string columnName = string.Empty;

            if (contentType == ContentType.Memo)
                columnName = "MEMO_CD";
            else if (contentType == ContentType.CheckPen)
                columnName = "CHECK_PEN";

            if (commandType == Dass.FlexGrid.CommandType.Add)
                sqlQuery = "UPDATE PU_POL SET " + columnName + " = '" + value + "' WHERE NO_PO  = '" + noIo + "' AND CD_COMPANY = '" + MA.Login.회사코드 + "'  AND NO_LINE = " + no_ioline;
            else if (commandType == Dass.FlexGrid.CommandType.Delete)
                sqlQuery = "UPDATE PU_POL SET " + columnName + " = NULL WHERE NO_PO  = '" + noIo + "' AND CD_COMPANY = '" + MA.Login.회사코드 + "' AND NO_LINE = " + no_ioline;

            Global.MainFrame.ExecuteScalar(sqlQuery);

        }
        #endregion

        #region -> 한국민속촌 전용
        internal DataTable GetInfo_ZOO(string NO_PROJECT)
        {
            DataTable dt = DBHelper.GetDataTable("UP_PU_Z_ZOO_PROJECT_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode, NO_PROJECT });
            T.SetDefaultValue(dt);
            return dt;
        }
        #endregion

        #region 외주사용유무조회
        public DataTable GetYN_SU(string sCD_TPPO)
        {
            string ls_cd_cc = string.Empty;

            string SelectQuery = "SELECT YN_SU " +
                                 "  FROM PU_TPPO " +
                                 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                 "   AND CD_TPPO = '" + sCD_TPPO + "'";

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            return dt;
        }
        #endregion

        #region -> 외주BOM(SU_BOM) 조회

        public DataTable GET_SU_BOM(string strCD_PLANT, string strCD_PARTNER, string strCD_ITEM)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_PO_REG_SU_BOM_SELECT";
            si.SpParamsSelect = new Object[] { Global.MainFrame.LoginInfo.CompanyCode, strCD_PLANT, strCD_PARTNER, strCD_ITEM };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;
            foreach (DataColumn col in dt.Columns)
            {
                if (col.DataType == typeof(decimal))
                    col.DefaultValue = 0;
            }
            return dt;
        }

        #endregion

        #region 신진SM 중량단가 불러오기
        public DataTable Check_ITEMGRP_SG(string CD_ITEM_GRP)
        {

            string SelectQuery = "SELECT  CD_1 SG_TYPE, UM_RT_ETC_1 QT_SG, AM_1 UM_WEIGHT" +
                                 "  FROM PU_CUST_PUBLIC" +
                                 " WHERE  CD_NO_PK_1 = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                 "   AND  CD_CUST_SERVER_KEY = '" + Global.MainFrame.ServerKey + "'" +
                                 "   AND  NM_BUSINESS    =  'P_PU_Z_SINJINSM_ITEMGRP_SG'" +
                                 "   AND  SQ_BUSINESS    =  '1'" +
                                 "   AND  CD_NO_PK_2     = '" + CD_ITEM_GRP + "'";


            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);


            return dt;
        }
        public DataTable Check_ITEMGRP_UM(string CD_PARTNER)
        {

            string SelectQuery = "SELECT  UM_RT_ETC_1,CD_NO_PK_3 AS CLS_L,CD_NO_PK_4 AS GRP_ITEM,CD_NO_PK_2 AS CD_PARTNER" +
                                 "  FROM PU_CUST_PUBLIC" +
                                 " WHERE  CD_NO_PK_1 = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                 "   AND  CD_CUST_SERVER_KEY = '" + Global.MainFrame.ServerKeyCommon + "'" +
                                 "   AND  NM_BUSINESS    =  'P_PU_Z_SINJINSM_PO_AM_RT_REG'" +
                                 "   AND  SQ_BUSINESS    =  '1'" +
                                 "   AND  CD_NO_PK_2    IN (" + CD_PARTNER + ")";


            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);


            return dt;
        }
        public DataTable Check_EMP_SG(string NO_EMP)
        {

            string SelectQuery = "SELECT  DC_RMK" +
                                 "  FROM  MA_EMP" +
                                 " WHERE  CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                 "   AND  NO_EMP     = '" + NO_EMP + "'";


            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);


            return dt;
        }
        #endregion


        #region 신진SM 공장품목이 존재하는지 CHECK
        public DataTable Check_PITEM(string CD_ITEM, string CD_PLANT, string CLS_S)
        {

            string SelectQuery = "SELECT 1" +
                                 "  FROM MA_PITEM" +
                                 " WHERE  CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                 "   AND  CD_ITEM = '" + CD_ITEM + "'" +
                                 "   AND  CD_PLANT = '" + CD_PLANT + "'" +
                                 "   AND  CLS_S = '" + CLS_S + "'";


            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);


            return dt;
        }
        #endregion

        #region 유니포인트 프로젝트 정보 가져오기
        public DataTable Get_Project_Detail(string CD_PJT)
        {

            string SelectQuery = "SELECT  H.CD_PARTNER, P.LN_PARTNER, H.NO_EMP, E.NM_KOR, H.END_USER" +
                                 "  FROM SA_PROJECTH H" +
                                 "  LEFT JOIN MA_PARTNER P ON P.CD_PARTNER = H.CD_PARTNER AND P.CD_COMPANY = H.CD_COMPANY" +
                                 "  LEFT JOIN MA_EMP E ON E.NO_EMP = H.NO_EMP AND E.CD_COMPANY = H.CD_COMPANY" +
                                 " WHERE  H.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                 "   AND  H.NO_PROJECT = '" + CD_PJT + "'";

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);


            return dt;
        }
        #endregion


        #region ♣토페스
        internal DataTable GetTopes(string strNoPo)
        {
            return DBHelper.GetDataTable("UP_PU_Z_TOPES_PO_TAB_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode, strNoPo, Global.MainFrame.ServerKeyCommon });
        }


        internal bool GetMHIK(string strNoPo)
        {
            bool bReturn = false;
            DataTable dt = DBHelper.GetDataTable("UP_PU_Z_MHIK_PO_TAB_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode, strNoPo, Global.MainFrame.ServerKeyCommon });

            if (dt.Rows.Count == 0)
                bReturn = true;

            return bReturn;
        }
        #endregion

        #region ♣KPIC
        public DataTable search_dt(string strDate)
        {
            string SelectQuery = "SELECT DT_CAL" +
                                 "  FROM MA_CALENDAR " +
                                 " WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'  " +
                                 "   AND DT_CAL > '" + strDate + "' " +
                                 "   AND FG1_HOLIDAY = 'W' " +
                                 " ORDER BY DT_CAL";

            ;
            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            return dt;
        }

        internal DataRow 계약잔량체크(string NO_CONTRACT, decimal NO_LINE)
        {
            string SQL = string.Empty;
            SQL = " SELECT ISNULL(H.CD_TYPE,'001') CD_TYPE, L.QT_CON - ISNULL(SL.QT_PO_MM,0) QT_CON "
                + " FROM   SA_Z_KPCI_CONTRACT_H H "
                + "        INNER JOIN SA_Z_KPCI_CONTRACT_L L ON H.CD_COMPANY = L.CD_COMPANY AND H.NO_CONTRACT = L.NO_CONTRACT "
                + "        LEFT JOIN(SELECT NO_RELATION, SEQ_RELATION, SUM(QT_PO_MM) QT_PO_MM "
                + "                  FROM  PU_POL  "
                + "                  WHERE  CD_COMPANY   = '" + MA.Login.회사코드 + "'"
                + "                  AND    NO_RELATION  = '" + NO_CONTRACT + "'"
                + "                  AND    SEQ_RELATION = '" + NO_LINE + "'"
                + "                  GROUP BY NO_RELATION, SEQ_RELATION "
                + "                  )SL ON SL.NO_RELATION = L.NO_CONTRACT AND SL.SEQ_RELATION = L.NO_LINE "
                + " WHERE  L.CD_COMPANY = '" + MA.Login.회사코드 + "'"
                + " AND    L.NO_CONTRACT = '" + NO_CONTRACT + "'"
                + " AND    L.NO_LINE = '" + NO_LINE + "'";

            DataTable dt = DBHelper.GetDataTable(SQL);
            return (dt == null || dt.Rows.Count == 0) ? null : dt.Rows[0];
        }
        #endregion
    }
}


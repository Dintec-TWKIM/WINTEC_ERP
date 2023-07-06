using System;
using System.Data;
using System.Windows.Forms;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPIU.Data;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA.Common;

namespace sale
{
    class P_SA_SO_BIZ
    {
        string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;

        bool is수주반품사용여부 = false;
        string 수주등록_특수단가적용 = "000";
        string 수주등록_할인율적용   = "000";
        string 수주등록_예상이익산출 = "000";
        string 수주등록_WH적용       = "000";
        string 수주라인_과세변경유무 = "N";
        string ATP사용여부           = "000";
        string 업체별프로세스        = "000";  //000:기본, 001:아사히카세이
        string _excCredit            = "000";
        string 수주등록_프로젝트적용 = "000";
        string 수주등록_사양등록     = "000";

        private 수주관리.Config 수주Config = new 수주관리.Config();

        public P_SA_SO_BIZ()
        {
            BASIC.CacheDataClear(BASIC.CacheEnums.ALL);

            is수주반품사용여부    = BASIC.GetMAEXC("수주반품사용여부") == "Y" ? true : false;
            수주등록_특수단가적용 = BASIC.GetMAEXC("수주등록-특수단가적용");
            수주등록_할인율적용   = BASIC.GetMAEXC("수주등록-할인율적용");
            수주등록_예상이익산출 = BASIC.GetMAEXC("수주등록-예상이익산출");
            수주등록_WH적용       = BASIC.GetMAEXC("W/H 정보사용");
            수주라인_과세변경유무 = BASIC.GetMAEXC("수주라인-과세변경유무");
            ATP사용여부           = BASIC.GetMAEXC("ATP사용여부");
            업체별프로세스        = BASIC.GetMAEXC("업체별프로세스");
            _excCredit            = BASIC.GetMAEXC("여신한도");
            수주등록_프로젝트적용 = BASIC.GetMAEXC("수주등록-프로젝트 적용");
            수주등록_사양등록     = BASIC.GetMAEXC("수주등록-사양등록 사용여부");
        }

        #region ♣ 조회
        public DataSet Search(string NO_SO)
        {
            DataSet ds = null;
           
            ds = DBHelper.GetDataSet("UP_SA_SO_SELECT", new object[] { CD_COMPANY, NO_SO });

            T.SetDefaultValue(ds); 

            // 헤더테이블 디퐅트값
            DataTable dtHeader = ds.Tables[0];

            dtHeader.Columns["DT_SO"].DefaultValue = Global.MainFrame.GetStringToday;           //수주일자
            dtHeader.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;    //사번
            dtHeader.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;  //이름
            dtHeader.Columns["CD_EXCH"].DefaultValue = "000";                                   //화폐단위
            dtHeader.Columns["TP_PRICE"].DefaultValue = "001";                                   //단가유형
            dtHeader.Columns["TP_VAT"].DefaultValue = "11";                                    //VAT구분
            dtHeader.Columns["FG_VAT"].DefaultValue = "N";                                    //부가세
            dtHeader.Columns["FG_TAXP"].DefaultValue = "001";                                   //계산서처리
            dtHeader.Columns["NO_PROJECT"].DefaultValue = "";                                   //프로젝트
            dtHeader.Columns["DT_PROCESS"].DefaultValue = Global.MainFrame.GetStringToday;      //매출일자
            dtHeader.Columns["DT_RCP_RSV"].DefaultValue = DateTime.Now.AddDays(10).ToShortDateString().Replace("-", "");//수금예정일자

            //dtHeader.Columns["FG_TRANSPORT"].DefaultValue = "001";            // 운송방법 디폴트값 제외 요청(PIMS : D20110310011) : 2011.03.14 장은경
            
            return ds;
        }
        #endregion

        #region ♣ 대용산업 패킹정보 조회
        public DataTable Search_DAEYONG(string NO_SO)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_Z_DAEYONG_SO_PACKING_S", new object[] { CD_COMPANY, NO_SO });
            T.SetDefaultValue(dt);
            return dt;
        }
        
        #endregion

        #region ♣ 대용산업 패킹정보 삭제
        public bool DeleteDaeyongPacking(string NO_SO)
        {
            return DBHelper.ExecuteNonQuery("UP_SA_Z_DAEYONG_SO_PACKING_D1", new object[] { CD_COMPANY, NO_SO });
        }
        #endregion

        #region ♣ 단가통제
        public object UmSearch(string 품목, string 거래처, string 단가유형, string 환종, string 단가적용형태, string 수주일자)
        {
            object[] outs;
            DBHelper.ExecuteNonQuery("UP_SA_SO_SELECT1", new object[] { CD_COMPANY, 품목, 거래처, 단가유형, 환종, 단가적용형태, 수주일자 }, out outs);
            return outs[0];
        }
        #endregion

        #region ♣ 거래구분조회
        /// <summary>
        /// GetTpBusi() : 수주형태에 따른 해당 거래유형의 값을 가져와서 VAT구분에 반영한다.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="[0] : COMPANY"></param>
        /// <param name="[1] : 수주형태"></param>
        /// <param name="[2] : 부가세코드"></param>
        /// <returns>[0] : 거래구분</returns>
        /// <returns>[1] : VAT구분</returns>
        /// <returns>[2] : 수주상태</returns>
        public string[] 거래구분(string 수주형태, string 부가세코드)
        {
            object[] outs;
            DBHelper.ExecuteNonQuery("UP_SA_SO_TPBUSI_SELECT", new object[] { CD_COMPANY, 수주형태, 부가세코드 }, out outs);

            string[] ret = new string[3];
            ret[0] = D.GetString(outs[0]);
            ret[1] = D.GetString(outs[1]);
            ret[2] = D.GetString(outs[2]);

            return ret;
        }

        #endregion

        #region ♣ 여신체크
        public bool CheckCredit(string 거래처, decimal 금액, string 의뢰여부, string 출하여부, ref string 수주상태)
        {
            string checkLevel = "001";
            object[] objOut = new object[3];

            if (의뢰여부 == "Y")
            {
                if (출하여부 == "Y") checkLevel = "101";
                else checkLevel = "100";
            }

            DBHelper.GetDataTable("UP_SA_CHECKCREDIT_SELECT", new object[] { CD_COMPANY, 거래처, 금액, checkLevel }, out objOut);

            if (objOut[0] != DBNull.Value)
            {
                string 여신총액 = D.GetDecimal(objOut[0]).ToString("###,###,###,###,##0.####");
                string 잔액     = D.GetDecimal(objOut[1]).ToString("###,###,###,###,##0.####");

                if (D.GetString(objOut[2]).ToString() == "002")
                {
                    if (Global.MainFrame.ShowMessage("여신금액을 초과하였습니다. 저장하시겠습니까 ?\n(여신총액 : " + 여신총액 + ", 잔액 : " + 잔액 + ")", "QY2") == DialogResult.Yes)
                    {
                        if (Get업체별프로세스 == "009")    //에스디바이오센서(주)
                            수주상태 = "O";
                        return true;
                    }
                    else
                        return false;
                }
                else if (D.GetString(objOut[2]).ToString() == "003")
                {
                    Global.MainFrame.ShowMessage("여신금액을 초과하여 저장할 수 없습니다. \n(여신총액 : " + 여신총액 + ", 잔액 : " + 잔액 + ")");
                    return false;
                }
            }
            return true;
        }

        internal bool CheckCreditExec(string cdPartner, string cdExch, decimal amEx)
        {
            object[] objOut = new object[3];

            DBHelper.GetDataTable("UP_SA_CHECKCREDIT_EXCH_SELECT", new object[] { CD_COMPANY, cdPartner, cdExch, amEx, "001" }, out objOut);

            if (objOut[0] != DBNull.Value)
            {
                string 여신총액 = D.GetDecimal(objOut[0]).ToString("###,###,###,###,##0.####");
                string 잔액     = D.GetDecimal(objOut[1]).ToString("###,###,###,###,##0.####");

                if (D.GetString(objOut[2]) == "002")
                {
                    if (Global.MainFrame.ShowMessage("여신금액을 초과하였습니다. 저장하시겠습니까 ?\n(여신총액 : " + 여신총액 + ", 잔액 : " + 잔액 + ")", "QY2") == DialogResult.Yes)
                        return true;
                    else
                        return false;
                }
                else if (D.GetString(objOut[2]) == "003")
                {
                    Global.MainFrame.ShowMessage("여신금액을 초과하여 저장할 수 없습니다. \n(여신총액 : " + 여신총액 + " 잔액 : " + 잔액 + ")");
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region ♣ 삭제
        public bool Delete(string NO_SO)
        {
            if (MA.ServerKey(false, new string[] { "HKCOS" ,"TOKIMEC", "BROTHER" }))
                return DBHelper.ExecuteNonQuery("UP_SA_Z_HKCOS_SO_D", new object[] { CD_COMPANY, NO_SO });
            else
                return DBHelper.ExecuteNonQuery("UP_SA_SO_DELETE", new object[] { CD_COMPANY, NO_SO });
        }
        #endregion

        #region ♣ 저장
        public bool Save(DataTable dtH, DataTable dtL, DataTable dtLL, DataTable dtLot, string[] strArr, bool Use루미시트)
        {
            SpInfoCollection sic = new SpInfoCollection();
            string NO_SO    = strArr[0];     string 수주상태 = strArr[1];    string 거래구분 = strArr[2];
            string 출하형태  = strArr[3];     string 매출형태 = strArr[4];    string 의뢰여부 = strArr[5];
            string 출하여부  = strArr[6];     string 매출여부 = strArr[7];    string 수출여부 = strArr[8];
            string 구분      = strArr[9];     string 반품여부 = strArr[10];   string 매출자동여부 = strArr[11];
            string 자동승인여부 = strArr[12];

            #region -> 헤더
            if (dtH != null)
            {
                string cdBizarea = string.Empty;
                if (dtH.Rows.Count > 0)
                {
                    if (구분 == "복사" || dtH.Rows[0].RowState == DataRowState.Added)
                    {
                        cdBizarea = D.GetString(Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(Duzon.ERPU.MF.MasterSearch.MA_PLANT, new object[] { CD_COMPANY, D.GetString(dtL.Rows[0]["CD_PLANT"]) })["CD_BIZAREA"]);
                    }
                }

                SpInfo siM = new SpInfo();

                if (구분 == "복사")
                    siM.DataState = DataValueState.Added; //-> 처음 복사할때 바인딩 되기전에 처리해줬음

                siM.DataValue = dtH;
                siM.CompanyID = CD_COMPANY;
                siM.UserID = Global.MainFrame.LoginInfo.UserID;
                siM.SpNameInsert = "UP_SA_SOH_I";
                siM.SpNameUpdate = "UP_SA_SOH_U";
                siM.SpParamsInsert = new string[] { "CD_COMPANY",       "NO_SO",            "CD_BIZAREA",       "DT_SO",        "CD_PARTNER",
                                                    "CD_SALEGRP",       "NO_EMP",           "TP_SO",            "CD_EXCH",      "RT_EXCH",
                                                    "TP_PRICE",         "NO_PROJECT",       "TP_VAT",           "RT_VAT",       "FG_VAT",
                                                    "FG_TAXP",          "DC_RMK",           "FG_BILL",          "FG_TRANSPORT", "NO_CONTRACT",
                                                    "STA_SO",           "FG_TRACK",         "NO_PO_PARTNER",    "NO_EST",       "NO_EST_HST",
                                                    "CD_EXPORT",        "CD_PRODUCT",       "COND_TRANS",       "COND_PAY",     "COND_DAYS",
                                                    "TP_PACKING",       "TP_TRANS",         "TP_TRANSPORT",     "NM_INSPECT",   "PORT_LOADING",
                                                    "PORT_ARRIVER",     "CD_ORIGIN",        "DESTINATION",      "DT_EXPIRY",    "CD_NOTIFY",
                                                    "CD_CONSIGNEE",     "CD_TRANSPORT",     "DC_RMK_TEXT",      "ID_INSERT",    "RMA_REASON",
                                                    "DC_RMK1",          "COND_PRICE",       "DT_USERDEF1",      "DT_USERDEF2",  "TXT_USERDEF1",
                                                    "TXT_USERDEF2",     "TXT_USERDEF3",     "CD_USERDEF1",      "CD_USERDEF2",  "CD_USERDEF3",
                                                    "NUM_USERDEF1",     "NUM_USERDEF2",     "NUM_USERDEF3",     "NUM_USERDEF4", "NUM_USERDEF5",
                                                    "CD_BANK_SO",       "DC_RMK_TEXT2",     "NO_INV",           "TXT_USERDEF4" };
                siM.SpParamsUpdate = new string[] { "CD_COMPANY",       "NO_SO",            "DT_SO",            "CD_PARTNER",   "CD_SALEGRP",
                                                    "NO_EMP",           "CD_EXCH",          "RT_EXCH",          "TP_PRICE",     "NO_PROJECT",
                                                    "TP_VAT",           "FG_VAT",           "FG_TAXP",          "DC_RMK",       "FG_BILL",
                                                    "FG_TRANSPORT",     "NO_CONTRACT",      "NO_PO_PARTNER",    "NO_EST",       "NO_EST_HST",
                                                    "CD_EXPORT",        "CD_PRODUCT",       "COND_TRANS",       "COND_PAY",     "COND_DAYS",
                                                    "TP_PACKING",       "TP_TRANS",         "TP_TRANSPORT",     "NM_INSPECT",   "PORT_LOADING",
                                                    "PORT_ARRIVER",     "CD_ORIGIN",        "DESTINATION",      "DT_EXPIRY",    "CD_NOTIFY",
                                                    "CD_CONSIGNEE",     "CD_TRANSPORT",     "DC_RMK_TEXT",      "ID_UPDATE",    "RMA_REASON",
                                                    "DC_RMK1",          "COND_PRICE",       "DT_USERDEF1",      "DT_USERDEF2",  "TXT_USERDEF1",
                                                    "TXT_USERDEF2",     "TXT_USERDEF3",     "CD_USERDEF1",      "CD_USERDEF2",  "CD_USERDEF3",
                                                    "NUM_USERDEF1",     "NUM_USERDEF2",     "NUM_USERDEF3",     "NUM_USERDEF4", "NUM_USERDEF5",
                                                    "CD_BANK_SO",       "DC_RMK_TEXT2",     "NO_INV",           "TXT_USERDEF4" };
                siM.SpParamsValues.Add(ActionState.Insert, "STA_SO", 수주상태);
                siM.SpParamsValues.Add(ActionState.Insert, "CD_BIZAREA", cdBizarea);
                sic.Add(siM);
            }
            #endregion

            #region -> 헤더SUB
            if (MA.ServerKey(false, "YWD"))
            {
                if (매출자동여부 == "Y" && 자동승인여부 == "Y")
                {
                    SpInfo si = new SpInfo();

                    if (구분 == "복사")
                        si.DataState = DataValueState.Added; //-> 처음 복사할때 바인딩 되기전에 처리해줬음
                    
                    si.DataValue = dtH;
                    si.CompanyID = CD_COMPANY;
                    si.UserID = Global.MainFrame.LoginInfo.UserID;
                    si.SpNameInsert = "UP_SA_SOH_SUB_I";
                    si.SpNameUpdate = "UP_SA_SOH_SUB_U";
                    si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_SO", "DT_SO", "DT_RCP_RSV", "FG_AR_EXC", "AM_IV", "AM_IV_EX", "AM_IV_VAT", "NM_PTR", "EX_EMIL", "EX_HP", "ID_INSERT", "DC_RMK_IVH" };
                    si.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_SO", "DT_SO", "DT_RCP_RSV", "FG_AR_EXC", "AM_IV", "AM_IV_EX", "AM_IV_VAT", "NM_PTR", "EX_EMIL", "EX_HP", "ID_UPDATE", "DC_RMK_IVH" };
                    sic.Add(si);
                }
            }
            else
            {
                if (Get과세변경유무 == "N" && 매출자동여부 == "Y")
                {
                    SpInfo si = new SpInfo();

                    if (구분 == "복사")
                        si.DataState = DataValueState.Added; //-> 처음 복사할때 바인딩 되기전에 처리해줬음

                    si.DataValue = dtH;
                    si.CompanyID = CD_COMPANY;
                    si.UserID = Global.MainFrame.LoginInfo.UserID;
                    si.SpNameInsert = "UP_SA_SOH_SUB_I";
                    si.SpNameUpdate = "UP_SA_SOH_SUB_U";
                    si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_SO", "DT_PROCESS", "DT_RCP_RSV", "FG_AR_EXC", "AM_IV", "AM_IV_EX", "AM_IV_VAT", "NM_PTR", "EX_EMIL", "EX_HP", "ID_INSERT", "DC_RMK_IVH" };
                    si.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_SO", "DT_PROCESS", "DT_RCP_RSV", "FG_AR_EXC", "AM_IV", "AM_IV_EX", "AM_IV_VAT", "NM_PTR", "EX_EMIL", "EX_HP", "ID_UPDATE", "DC_RMK_IVH" };
                    sic.Add(si);
                }
            }
            #endregion

            #region -> 라인
            if (dtL != null)
            {
                SpInfo siD = new SpInfo();

                //if (구분 == "복사")
                //    siD.DataState = DataValueState.Added;

                siD.DataValue = dtL;
                siD.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siD.UserID = Global.MainFrame.LoginInfo.UserID;
                siD.SpNameInsert = "UP_SA_SOL_I";
                if (MA.ServerKey(false, new string[] { "HKCOS", "TOKIMEC", "DAEYONG", "BROTHER" }))
                    siD.SpNameUpdate = "UP_SA_Z_HKCOS_SOL_U";
                else
                    siD.SpNameUpdate = "UP_SA_SOL_U";
                if (MA.ServerKey(false, new string[] { "HKCOS", "TOKIMEC", "DAEYONG", "BROTHER" }))
                    siD.SpNameDelete = "UP_SA_Z_HKCOS_SOL_D";
                else
                    siD.SpNameDelete = "UP_SA_SOL_DELETE";

                string colName = string.Empty;
                if (구분 == "복사")
                    colName = "STA_SO1";
                else
                    colName = "STA_SO";

                siD.SpParamsInsert = new string[] { "SERVERKEY",        "CD_COMPANY",       "NO_SO",            "SEQ_SO",           "CD_PLANT",         
                                                    "CD_ITEM",          "UNIT_SO",          "DT_DUEDATE",       "DT_REQGI",         "QT_SO",
                                                    "UM_SO",            "AM_SO",            "AM_WONAMT",        "AM_VAT",           "UNIT_IM",
                                                    "QT_IM",            "CD_SL",            "TP_ITEM",          colName,            "TP_BUSI",
                                                    "TP_GI",            "TP_IV",            "GIR",              "GI",               "IV",
                                                    "TRADE",            "TP_VAT",           "RT_VAT",           "GI_PARTNER",       "ID_INSERT",
                                                    "NO_PROJECT",       "SEQ_PROJECT",      "CD_ITEM_PARTNER",  "NM_ITEM_PARTNER",  "DC1",
                                                    "DC2",              "UMVAT_SO",         "AMVAT_SO",         "CD_SHOP",          "CD_SPITEM",
                                                    "CD_OPT",           "RT_DSCNT",         "UM_BASE",          "NM_CUST_DLV",      "CD_ZIP",
                                                    "ADDR1",            "ADDR2",            "NO_TEL_D1",        "NO_TEL_D2",        "TP_DLV",
                                                    "DC_REQ",           "FG_TRACK",         "TP_DLV_DUE",       "FG_USE",           "CD_CC",
                                                    "NO_IO_MGMT",       "NO_IOLINE_MGMT",   "NO_POLINE_PARTNER","UM_OPT",           "NO_PO_PARTNER",
                                                    "CD_WH",            "NO_SO_ORIGINAL",   "SEQ_SO_ORIGINAL",  "NUM_USERDEF1",     "NUM_USERDEF2",
                                                    "NUM_USERDEF3",     "NUM_USERDEF4",     "NUM_USERDEF5",     "NUM_USERDEF6",     "CD_MNGD1",
                                                    "CD_MNGD2",         "CD_MNGD3",         "CD_MNGD4",         "TXT_USERDEF1",     "TXT_USERDEF2",
                                                    "YN_OPTION",        "NO_ORDER",         "NM_CUST",          "NO_TEL1",          "NO_TEL2",
                                                    "DLV_TXT_USERDEF1", "CD_ITEM_REF",      "YN_PICKING",       "DLV_CD_USERDEF1",  "FG_USE2",
                                                    "NO_LINK",          "NO_LINE_LINK",     "NO_RELATION",      "SEQ_RELATION",     "CD_USERDEF1"  };
                siD.SpParamsUpdate = new string[] { "CD_COMPANY",       "NO_SO",            "SEQ_SO",           "CD_PLANT",         "CD_ITEM", 
                                                    "UNIT_SO",          "DT_DUEDATE",       "DT_REQGI",         "QT_SO",            "UM_SO", 
                                                    "AM_SO",            "AM_WONAMT",        "AM_VAT",           "UNIT_IM",          "QT_IM", 
                                                    "CD_SL",            "TP_ITEM",          "ID_UPDATE",        "GI_PARTNER",       "NO_PROJECT", 
                                                    "SEQ_PROJECT",      "CD_ITEM_PARTNER",  "NM_ITEM_PARTNER",  "DC1",              "DC2", 
                                                    "UMVAT_SO",         "AMVAT_SO",         "CD_SHOP",          "CD_SPITEM",        "CD_OPT", 
                                                    "RT_DSCNT",         "UM_BASE",          "NM_CUST_DLV",      "CD_ZIP",           "ADDR1", 
                                                    "ADDR2",            "NO_TEL_D1",        "NO_TEL_D2",        "TP_DLV",           "DC_REQ", 
                                                    "FG_TRACK",         "TP_DLV_DUE",       "FG_USE",           "TP_VAT",           "RT_VAT",           
                                                    "CD_CC",            "NO_POLINE_PARTNER","UM_OPT",           "NO_PO_PARTNER",    "CD_WH",
                                                    "NO_SO_ORIGINAL",   "SEQ_SO_ORIGINAL",  "NUM_USERDEF1",     "NUM_USERDEF2",     "NUM_USERDEF3",
                                                    "NUM_USERDEF4",     "NUM_USERDEF5",     "NUM_USERDEF6",     "CD_MNGD1",         "CD_MNGD2",
                                                    "CD_MNGD3",         "CD_MNGD4",         "TXT_USERDEF1",     "TXT_USERDEF2",     "YN_OPTION",
                                                    "NO_ORDER",         "NM_CUST",          "NO_TEL1",          "NO_TEL2",          "DLV_TXT_USERDEF1",
                                                    "CD_ITEM_REF",      "YN_PICKING",       "DLV_CD_USERDEF1",  "FG_USE2",          "CD_USERDEF1" };
                siD.SpParamsDelete = new string[] { "CD_COMPANY", "NO_SO", "SEQ_SO" };

                siD.SpParamsValues.Add(ActionState.Insert, "NO_SO", NO_SO);
                siD.SpParamsValues.Add(ActionState.Insert, "SERVERKEY", Global.MainFrame.ServerKeyCommon.ToUpper());

                if (구분 != "복사")
                    siD.SpParamsValues.Add(ActionState.Insert, "STA_SO", 수주상태);
                
                siD.SpParamsValues.Add(ActionState.Insert, "TP_BUSI", 거래구분);
                siD.SpParamsValues.Add(ActionState.Insert, "TP_GI", 출하형태);
                siD.SpParamsValues.Add(ActionState.Insert, "GIR", 의뢰여부);
                siD.SpParamsValues.Add(ActionState.Insert, "GI", 출하여부);
                siD.SpParamsValues.Add(ActionState.Insert, "IV", 매출여부);
                siD.SpParamsValues.Add(ActionState.Insert, "TRADE", 수출여부);
                siD.SpParamsValues.Add(ActionState.Update, "NO_SO", NO_SO);
                siD.SpParamsValues.Add(ActionState.Delete, "NO_SO", NO_SO);
                sic.Add(siD);
            }
            #endregion

            #region -> 소요자재 / 사양등록
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "DAEYONG")
            {
                #region -> 대용산업전용 패킹 데이터 저장
                if (dtLL != null)
                {
                    SpInfo si08 = new SpInfo();
                    si08.DataValue = dtLL;
                    si08.CompanyID = CD_COMPANY;
                    si08.UserID = Global.MainFrame.LoginInfo.UserID;
                    si08.SpNameInsert = "UP_SA_Z_DAEYONG_SO_PACKING_I";
                    si08.SpNameUpdate = "UP_SA_Z_DAEYONG_SO_PACKING_U";
                    si08.SpNameDelete = "UP_SA_Z_DAEYONG_SO_PACKING_D";
                    si08.SpParamsInsert = new string[] {"CD_CUST_SERVER_KEY", "NM_BUSINESS",  "SQ_BUSINESS", "CD_COMPANY",    "NO_SO", 	           
                                                        "NO_BOX",             "NO_BOX_WORK",  "SEQ_SO",      "NO_CT",         "FG_CT",  	           
                                                        "CT_SIZE",            "DESTINATION",  "QT_PACKING",  "PACKING_STND",  "CBM",  	           
                                                        "NET_WEIGHT",         "GROSS_WEIGHT", "FG_ISU",      "DT_CREATE",     "DC_RMK1",  	       
                                                        "DC_RMK2",            "ID_INSERT"  };

                    si08.SpParamsUpdate = new string[] {"CD_CUST_SERVER_KEY", "NM_BUSINESS",  "SQ_BUSINESS", "CD_COMPANY",    "NO_SO", 	           
                                                        "NO_BOX",             "NO_BOX_WORK",  "SEQ_SO",      "NO_CT",         "FG_CT",  	           
                                                        "CT_SIZE",            "DESTINATION",  "QT_PACKING",  "PACKING_STND",  "CBM",  	           
                                                        "NET_WEIGHT",         "GROSS_WEIGHT", "FG_ISU",      "DT_CREATE",     "DC_RMK1",  	       
                                                        "DC_RMK2",            "ID_UPDATE"  };

                    si08.SpParamsDelete = new string[] { "CD_COMPANY", "NO_SO", "SEQ_SO", "NO_BOX", "NO_BOX_WORK" };

                    si08.SpParamsValues.Add(ActionState.Insert, "NO_SO", NO_SO);
                    si08.SpParamsValues.Add(ActionState.Insert, "CD_CUST_SERVER_KEY", Global.MainFrame.ServerKeyCommon.ToUpper());
                    si08.SpParamsValues.Add(ActionState.Insert, "NM_BUSINESS", Global.MainFrame.CurrentPageID);
                    si08.SpParamsValues.Add(ActionState.Insert, "SQ_BUSINESS", 1);
                    si08.SpParamsValues.Add(ActionState.Update, "NO_SO", NO_SO);
                    si08.SpParamsValues.Add(ActionState.Update, "CD_CUST_SERVER_KEY", Global.MainFrame.ServerKeyCommon.ToUpper());
                    si08.SpParamsValues.Add(ActionState.Update, "NM_BUSINESS", Global.MainFrame.CurrentPageID);
                    si08.SpParamsValues.Add(ActionState.Update, "SQ_BUSINESS", 1);
                    si08.SpParamsValues.Add(ActionState.Delete, "NO_SO", NO_SO);
                    sic.Add(si08);
                }

                #endregion
            }
            else
            {
                if (dtLL != null && Use루미시트)
                {
                    SpInfo siL = new SpInfo();

                    if (구분 == "복사")
                        siL.DataState = DataValueState.Added; //-> 처음 복사할때 바인딩 되기전에 처리해줬음

                    siL.DataValue = dtLL;
                    siL.CompanyID = CD_COMPANY;
                    siL.UserID = Global.MainFrame.LoginInfo.UserID;
                    siL.SpNameInsert = "UP_SA_Z_FAWOO_SO_I";
                    siL.SpNameUpdate = "UP_SA_Z_FAWOO_SO_U";
                    siL.SpNameDelete = "UP_SA_Z_FAWOO_SO_D";
                    siL.SpParamsInsert = new string[] { "CD_COMPANY",   "NO_SO_NEW",    "SEQ_SO",   "SEQ_SO_LINE",  "CD_MATL",
                                                    "QT_NEED",      "QT_NEED_UNIT", "DC_RMK_1", "DC_RMK_2",     "DC_RMK_3",
                                                    "UNIT_GI_FACT", "UNIT_GI2",     "ID_INSERT"};
                    siL.SpParamsUpdate = new string[] { "CD_COMPANY",   "NO_SO_NEW",    "SEQ_SO",   "SEQ_SO_LINE",  "CD_MATL",
                                                    "QT_NEED",      "QT_NEED_UNIT", "DC_RMK_1", "DC_RMK_2",     "DC_RMK_3",
                                                    "UNIT_GI_FACT", "UNIT_GI2",     "ID_UPDATE"};
                    siL.SpParamsDelete = new string[] { "CD_COMPANY", "NO_SO_NEW", "SEQ_SO", "SEQ_SO_LINE" };
                    siL.SpParamsValues.Add(ActionState.Insert, "NO_SO_NEW", NO_SO);
                    siL.SpParamsValues.Add(ActionState.Update, "NO_SO_NEW", NO_SO);
                    siL.SpParamsValues.Add(ActionState.Delete, "NO_SO_NEW", NO_SO);
                    sic.Add(siL);
                }
                else if (dtLL != null && !Use루미시트)
                {
                    SpInfo si06 = new SpInfo();
                    si06.DataValue = dtLL;
                    si06.CompanyID = CD_COMPANY;
                    si06.UserID = Global.MainFrame.LoginInfo.UserID;
                    si06.SpNameInsert = "UP_SA_SOL_OPT_I";
                    si06.SpNameDelete = "UP_SA_SOL_OPT_D";
                    si06.SpParamsInsert = new string[] { "CD_COMPANY", "NO_SO", "SEQ_SO", "CD_PLANT", "NO_SPEC",
                                                     "CD_ITEM", "CD_MATL", "QT_INPUT", "QT", "DC_RMK", "ID_INSERT" };
                    si06.SpParamsDelete = new string[] { "CD_COMPANY", "NO_SO", "SEQ_SO", "CD_PLANT", "NO_SPEC",
                                                     "CD_ITEM", "CD_MATL" };
                    si06.SpParamsValues.Add(ActionState.Insert, "NO_SO", NO_SO);
                    si06.SpParamsValues.Add(ActionState.Delete, "NO_SO", NO_SO);
                    sic.Add(si06);
                }
            }
            #endregion

            #region -> LOT정보
            if (dtLot != null)
            {
                SpInfo si03 = new SpInfo();
                si03.DataValue = dtLot;
                si03.DataState = DataValueState.Added;
                si03.SpNameInsert = "UP_MM_QTIOLOT_INSERT";
                si03.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                if (반품여부 == "N")
                {
                    si03.SpParamsInsert = new string[] { "CD_COMPANY",  "출고번호", "출고항번",  "NO_LOT",     "CD_ITEM",
                                                         "출고일",      "FG_PS",    "수불구분",  "수불형태",   "창고코드",
                                                         "QT_GOOD_MNG", "NO_IO",    "NO_IOLINE", "NO_IOLINE2", "YN_RETURN",
                                                         "A", "B", "C", "D", "E", "F", "NO_SO"};
                }
                else
                {
                    si03.SpParamsInsert = new string[] { "CD_COMPANY",  "NO_IO",    "NO_IOLINE",    "NO_LOT",       "CD_ITEM", 
                                                         "DT_IO",       "FG_PS",    "FG_IO",        "CD_QTIOTP",    "CD_SL", 
                                                         "QT_IO",       "NO_IO",    "NO_IOLINE",    "NO_IOLINE2",   "YN_RETURN",
                                                         "A", "B", "C", "D", "E", "F", "NO_SO"};
                    si03.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", "Y");
                }
                si03.SpParamsValues.Add(ActionState.Insert, "A", string.Empty);
                si03.SpParamsValues.Add(ActionState.Insert, "B", string.Empty);
                si03.SpParamsValues.Add(ActionState.Insert, "C", 0);
                si03.SpParamsValues.Add(ActionState.Insert, "D", 0);
                si03.SpParamsValues.Add(ActionState.Insert, "E", string.Empty);
                si03.SpParamsValues.Add(ActionState.Insert, "F", string.Empty);
                si03.SpParamsValues.Add(ActionState.Insert, "NO_SO", NO_SO);
                sic.Add(si03);
            }
            #endregion

            #region -> 프로젝트
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "FORTIS")
            {
                SpInfo si04 = new SpInfo();

                if (구분 == "복사")
                    si04.DataState = DataValueState.Added; //-> 처음 복사할때 바인딩 되기전에 처리해줬음

                si04.DataValue = dtH;
                si04.CompanyID = CD_COMPANY;
                si04.UserID = Global.MainFrame.LoginInfo.UserID;
                si04.SpNameInsert = "UP_SA_SO_PROJECT_I";
                si04.SpNameUpdate = "UP_SA_SO_PROJECT_U";
                si04.SpParamsInsert = new string[] { "CD_COMPANY",       "NO_SO",            "DT_SO",            "CD_PARTNER",    "CD_BIZAREA",
                                                     "CD_SALEGRP",       "NO_EMP",           "CD_EXCH",          "RT_EXCH",       "FG_VAT",
                                                     "DC_RMK",           "ID_INSERT" };
                si04.SpParamsUpdate = new string[] { "CD_COMPANY",       "NO_SO",            "DT_SO",            "CD_PARTNER",
                                                     "CD_SALEGRP",       "NO_EMP",           "CD_EXCH",          "RT_EXCH",       "FG_VAT",
                                                     "DC_RMK",           "ID_UPDATE" };
                si04.SpParamsValues.Add(ActionState.Insert, "CD_BIZAREA", Global.MainFrame.LoginInfo.BizAreaCode);
                sic.Add(si04);
            }
            #endregion

            #region -> 결제조건
            if (수주Config.결제조건도움창사용() && (dtH != null))
            {
                if (구분 == "복사" || dtH.Rows[0].RowState == DataRowState.Added)
                {
                    string noProject = D.GetString(dtL.Rows[0]["NO_PROJECT"]);
                    decimal seqProject = D.GetDecimal(dtL.Rows[0]["SEQ_PROJECT"]);

                    //프로젝트항번이 존재하는 것은 프로젝트 적용을 받아온 것임
                    if (noProject != string.Empty && seqProject > 0m)
                    {
                        DataTable dt = DBHelper.GetDataTable("UP_SA_SO_PAYCOND_S", new object[] { CD_COMPANY, noProject, "PRE" });

                        SpInfo si05 = new SpInfo();
                        si05.DataState = DataValueState.Added;
                        si05.DataValue = dt;
                        si05.CompanyID = CD_COMPANY;
                        si05.UserID = Global.MainFrame.LoginInfo.UserID;
                        si05.SpNameInsert = "UP_SA_SO_PAYCOND_I";
                        si05.SpParamsInsert = new string[] { "CD_COMPANY", "FG_TRACK_SO", "NO_SO_NEW", "NO_LINE_PAYCOND", "CD_PAYCOND",
                                                             "MONTH_PAYCOND", "DAY_PAYCOND", "RT_PAYCOND", "DC_RMK", "ID_INSERT" };
                        si05.SpParamsValues.Add(ActionState.Insert, "FG_TRACK_SO", "SO");
                        si05.SpParamsValues.Add(ActionState.Insert, "NO_SO_NEW", D.GetString(dtH.Rows[0]["NO_SO"]));
                        sic.Add(si05);
                    }
                }
            }

            #endregion

            #region -> 자동프로세서 데이터 보정

            if (MA.ServerKey(false, "YWD"))
            {
                if (매출자동여부 == "Y" && 자동승인여부 == "Y")
                {
                    SpInfo si07 = new SpInfo();
                    si07.DataValue = dtH;
                    si07.CompanyID = CD_COMPANY;
                    si07.UserID = Global.MainFrame.LoginInfo.UserID;
                    si07.SpNameInsert = "UP_SA_SO_MODIFY";
                    si07.SpParamsInsert = new string[] { "NO_SO", "CD_COMPANY" };
                    sic.Add(si07);
                }
            }
            else if (Get과세변경유무 == "N" && 매출자동여부 == "Y")
            {
                SpInfo si07 = new SpInfo();
                si07.DataValue = dtH;
                si07.CompanyID = CD_COMPANY;
                si07.UserID = Global.MainFrame.LoginInfo.UserID;
                si07.SpNameInsert = "UP_SA_SO_MODIFY";
                si07.SpParamsInsert = new string[] { "NO_SO", "CD_COMPANY" };
                sic.Add(si07);
            } 
            #endregion

            return DBHelper.Save(sic);
        }
        #endregion

        #region ♣ Excel Upload
        internal DataTable 공장품목(string 멀티품목코드, string cdPlant)
        {
            ArrayListExt arrList = arr엑셀(멀티품목코드);
            DataTable dt_DB결과 = null;

            for (int k = 0; k < arrList.Count; k++)
            {
                DataTable dt = DBHelper.GetDataTable("UP_SA_SO_EXCEL_SELECT", new object[] { CD_COMPANY, D.GetString(arrList[k]), cdPlant });

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

        internal DataTable 엑셀(DataTable 엑셀)
        {
            //엑셀.Columns.Add("NM_ITEM", typeof(string));
            //엑셀.Columns.Add("STND_ITEM", typeof(string));
            //엑셀.Columns.Add("UNIT_SO", typeof(string));
            //엑셀.Columns.Add("UNIT_IM", typeof(string));
            //엑셀.Columns.Add("NM_SL", typeof(string));
            //엑셀.Columns.Add("TP_ITEM", typeof(string));
            //엑셀.Columns.Add("LT_GI", typeof(string));
            //엑셀.Columns.Add("UNIT_SO_FACT", typeof(string));
            //엑셀.Columns.Add("SEQ_SO", typeof(string));
            //엑셀.Columns.Add("DT_REQGI", typeof(string));
            //엑셀.Columns.Add("STA_SO1", typeof(string));

            //엑셀 업로드시 엑셀 업로드 양식이 1.단가미포함 2.단가포함 이 있는데 단가가 있고 단가적용이 사용안하는 품목의 단가를 셋팅한다.
            if (!엑셀.Columns.Contains("UM_SO"))
                엑셀.Columns.Add("UM_SO", typeof(string));
            if (!엑셀.Columns.Contains("DC1"))
                엑셀.Columns.Add("DC1", typeof(string));
            if (!엑셀.Columns.Contains("DC2"))
                엑셀.Columns.Add("DC2", typeof(string));
            if (!엑셀.Columns.Contains("GI_PARTNER"))
                엑셀.Columns.Add("GI_PARTNER", typeof(string));
            if (!엑셀.Columns.Contains("CD_SL"))
                엑셀.Columns.Add("CD_SL", typeof(string));
            if (!엑셀.Columns.Contains("FG_USE"))
                엑셀.Columns.Add("FG_USE", typeof(string));
            if (!엑셀.Columns.Contains("AM_SO"))
                엑셀.Columns.Add("AM_SO", typeof(string));
            return 엑셀;
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

        internal DataTable Get프로젝트()
        {
            string sql = "SELECT NO_PROJECT, NM_PROJECT FROM SA_PROJECTH WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'";
            DataTable dt = DBHelper.GetDataTable(sql);
            return dt;
        }
        #endregion

        #region ♣ 거래처 배송정보를 가져오기 위함
        public DataTable GetPartnerInfoSearch(object[] obj)
        {
            string SelectQuery = " SELECT	CD_PARTNER,            " +
                                 "          NO_POST2,              " +
		                         "          DC_ADS2_H,             " +
		                         "          DC_ADS2_D,             " +
		                         "          NO_TEL2,               " +
		                         "          CD_EMP_PARTNER,        " +
		                         "          NO_HPEMP_PARTNER,      " +
                                 "          CD_AREA                " +
                                 "     FROM MA_PARTNER             " +
                                 "    WHERE CD_COMPANY = '" + obj[0].ToString() + "' " +
                                 "      AND CD_PARTNER = '" + obj[1].ToString() + "' ";

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            return dt;
        }
        #endregion

        #region ♣ 설정값셋팅 및 조회

        internal string Get설정(DefaultSettings 코드)
        {
            switch (코드)
            {
                case DefaultSettings.영업그룹코드:
                    return Properties.Settings.Default.영업그룹코드;
                case DefaultSettings.영업그룹명:
                    return Properties.Settings.Default.영업그룹명;
                case DefaultSettings.수주형태코드:
                    return Properties.Settings.Default.수주형태코드;
                case DefaultSettings.수주형태명:
                    return Properties.Settings.Default.수주형태명;
                case DefaultSettings.회사코드:
                    return Properties.Settings.Default.회사코드;
                case DefaultSettings.화폐단위:
                    return Properties.Settings.Default.화폐단위;
                case DefaultSettings.부가세포함:
                    return Properties.Settings.Default.부가세포함;
                case DefaultSettings.계산서처리:
                    return Properties.Settings.Default.계산서처리;
                case DefaultSettings.MAIL_ADDR:
                    return Properties.Settings.Default.MAIL_ADDR;
                default :
                    return "";
            }
        }
        internal void Set설정(DefaultSettings 코드, string val)
        {
            switch (코드)
            {
                case DefaultSettings.영업그룹코드:
                    Properties.Settings.Default.영업그룹코드 = val;
                    break;
                case DefaultSettings.영업그룹명:
                    Properties.Settings.Default.영업그룹명 = val;
                    break;
                case DefaultSettings.수주형태코드:
                    Properties.Settings.Default.수주형태코드 = val;
                    break;
                case DefaultSettings.수주형태명:
                    Properties.Settings.Default.수주형태명 = val;
                    break;
                case DefaultSettings.회사코드:
                    Properties.Settings.Default.회사코드 = val;
                    break;
                case DefaultSettings.화폐단위:
                    Properties.Settings.Default.화폐단위 = val;
                    break;
                case DefaultSettings.부가세포함:
                    Properties.Settings.Default.부가세포함 = val;
                    break;
                case DefaultSettings.계산서처리:
                    Properties.Settings.Default.계산서처리 = val;
                    break;
                case DefaultSettings.MAIL_ADDR:
                    Properties.Settings.Default.MAIL_ADDR = val;
                    break;
                default:
                    break;
            }
            Properties.Settings.Default.Save();
        }

        #endregion

        #region ♣ 기타메소드

        internal DataTable 할인율(string 공장, string 거래처, DataRow[] dr품목)
        {
            품목관리.조회 품목조회 = new 품목관리.조회();
            DataTable dt = 품목조회.거래처그룹품목군할인율(공장, 거래처, dr품목);
            dt.PrimaryKey = new DataColumn[] { dt.Columns["CD_ITEM"] };
            return dt;
        }

        internal DataTable 예상이익(string 공장, string 수주일자, DataRow[] dr품목)
        {
            품목관리.조회 품목조회 = new 품목관리.조회();
            DataTable dt = 품목조회.예상이익(공장, 수주일자, dr품목);
            dt.PrimaryKey = new DataColumn[] { dt.Columns["CD_ITEM"] };
            return dt;
        }

        internal void 예상이익(FlexGrid flex, int idx)
        {
            decimal 수주수량 = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(flex[idx, "QT_SO"]));
            decimal 재고단가 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(flex[idx, "UM_INV"]));
            decimal 원화금액 = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(flex[idx, "AM_WONAMT"]));

            수주관리.Calc 수주관리계산 = new 수주관리.Calc();
            flex[idx, "AM_PROFIT"] = 수주관리계산.예상이익계산(수주수량, 재고단가, 원화금액);
        }

        #endregion

        #region ♣ 속성

        internal bool 수주반품사용여부 { get { return is수주반품사용여부; } }

        internal 특수단가적용 Get특수단가적용
        {
            get
            {
                if (수주등록_특수단가적용 == "001") return 특수단가적용.중량단가;
                if (수주등록_특수단가적용 == "002") return 특수단가적용.조선호텔베이커리단가;
                if (수주등록_특수단가적용 == "003") return 특수단가적용.거래처별고정단가;
                return 특수단가적용.NONE;
            }
        }

        internal 수주관리.할인율적용 Get할인율적용
        {
            get
            {
                수주관리.Config soconfig = new 수주관리.Config();

                return soconfig.할인율();
            }
        }

        internal 예상이익산출 Get예상이익산출적용
        {
            get
            {
                if (수주등록_예상이익산출 == "001") return 예상이익산출.재고단가를원가로산출;
                return 예상이익산출.NONE;
            }
        }

        internal string Get_WH적용 { get { return 수주등록_WH적용; } }
        internal string Get과세변경유무 { get { return 수주라인_과세변경유무; } }
        internal string GetATP사용여부 { get { return ATP사용여부; } }
        internal string Get업체별프로세스 { get { return 업체별프로세스; } }
        internal string GetExcCredit { get { return _excCredit; } }
        internal string Get프로젝트적용 { get { return 수주등록_프로젝트적용; } }
        internal bool Get사양등록사용여부 { get { return 수주등록_사양등록 == "000" ? false : true; } }

        #endregion

        #region ♣ 의뢰된건 조회

        public DataSet 의뢰된건조회(string 수주번호)
        {
            DataSet ds = DBHelper.GetDataSet("UP_SA_SOL_TO_GIRL_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 수주번호 });
            T.SetDefaultValue(ds);
            return ds;
        }

        #endregion

        #region ♣ _flexUser 조회
        /// <summary>
        /// 소요자재
        /// </summary>
        internal DataTable SearchDetail(string CD_PLANT, string NO_SO)
        {
            return DBHelper.GetDataTable("UP_SA_Z_FAWOO_SO_DETAIL_S", new object[] { CD_COMPANY, CD_PLANT, NO_SO });
        }
        /// <summary>
        /// 사양등록
        /// </summary>
        internal DataTable SearchOption(string NO_SO)
        {
            return DBHelper.GetDataTable("UP_SA_Z_LK_SO_OPTION_S", new object[] { CD_COMPANY, NO_SO });
        }

        #endregion

        #region ♣ 화우테크놀러지 루미시트 관련

        #region -> BOM적용

        public DataTable BOM적용(object[] obj)
        {
            return DBHelper.GetDataTable("UP_SA_SO_BOM_S", obj);
        }

        #endregion

        #region -> 루미시트인쇄

        public DataTable 루미시트인쇄(string NO_SO)
        {
            return DBHelper.GetDataTable("UP_SA_Z_FAWOO_SO_P", new object[] { CD_COMPANY, NO_SO }, "SEQ_SO ASC, SEQ_SO_LINE ASC");
        }

        #endregion

        #endregion

        #region ♣ 해외적용건존재여부 조회

        public bool 해외적용건존재여부(string NO_SO)
        {
            string SQL = string.Empty;
            string 회사코드 = MA.Login.회사코드;

            SQL = " SELECT NO_OFFER NO_SO FROM TR_LCEXH WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_OFFER = '" + NO_SO + "' "
                + " UNION ALL "
                + " SELECT NO_PO    NO_SO FROM TR_INVL  WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_PO    = '" + NO_SO + "' ";

            DataTable dt = DBHelper.GetDataTable(SQL);

            if (dt == null || dt.Rows.Count == 0)
                return false;

            return true;
        }

        #endregion

        #region ♣ Lot_Schema

        internal DataTable dtLot_Schema(DataTable dtLot)
        {
            dtLot.Columns.Add("NO_IO",           typeof(string));
            dtLot.Columns.Add("NO_IOLINE",       typeof(decimal));
            dtLot.Columns.Add("NO_ISURCV",       typeof(string));
            dtLot.Columns.Add("NO_GIR",          typeof(string));
            dtLot.Columns.Add("DT_DUEDATE",      typeof(string));
            dtLot.Columns.Add("FG_TRANS",        typeof(string));
            dtLot.Columns.Add("CD_QTIOTP",       typeof(string));
            dtLot.Columns.Add("NM_QTIOTP",       typeof(string));
            dtLot.Columns.Add("DT_IO",           typeof(string));
            dtLot.Columns.Add("CD_SL",           typeof(string));
            dtLot.Columns.Add("NM_SL",           typeof(string));
            dtLot.Columns.Add("CD_ITEM",         typeof(string));
            dtLot.Columns.Add("NM_ITEM",         typeof(string));
            dtLot.Columns.Add("STND_ITEM",       typeof(string));
            dtLot.Columns.Add("UNIT",            typeof(string));
            dtLot.Columns.Add("UNIT_IM",         typeof(string));
            dtLot.Columns.Add("FG_IO",           typeof(string));
            dtLot.Columns.Add("QT_GIR",          typeof(decimal));
            dtLot.Columns.Add("UNIT_SO_FACT",    typeof(string));
            dtLot.Columns.Add("QT_GIR_IM",       typeof(decimal));
            dtLot.Columns.Add("QT_IO",           typeof(decimal));
            dtLot.Columns.Add("QT_GOOD_INV",     typeof(decimal));
            dtLot.Columns.Add("CD_PLANT",        typeof(string));
            dtLot.Columns.Add("CD_PJT",          typeof(string));
            dtLot.Columns.Add("NO_PROJECT",      typeof(string));
            dtLot.Columns.Add("NM_PROJECT",      typeof(string));
            dtLot.Columns.Add("NO_EMP",          typeof(string));
            dtLot.Columns.Add("NO_LOT",          typeof(string));
            dtLot.Columns.Add("NO_SERL",         typeof(string));
            dtLot.Columns.Add("NO_PSO_MGMT",     typeof(string));
            dtLot.Columns.Add("NO_PSOLINE_MGMT", typeof(decimal));
            dtLot.Columns.Add("NO_IO_MGMT",      typeof(string)); 
            dtLot.Columns.Add("NO_IOLINE_MGMT",  typeof(decimal));

            return dtLot;
        }

        #endregion

        #region ♣ WEBORDER

        internal void WEBORDER(string Date)
        {
            try
            {
                string Query = "SELECT NVL(YN_DIRECT, 'N') YN_DIRECT FROM CM_SERVER_CONFIG WHERE SERVER_KEY = '" + Global.MainFrame.ServerKeyCommon.ToUpper() + "'";
                DataTable dt = DBHelper.GetDataTable(Query);

                if (D.GetString(dt.Rows[0]["YN_DIRECT"]) == "Y")
                {
                    DBDirectConnector dbdirect = new DBDirectConnector();
                    Exception ex = dbdirect.Test();

                    if (ex != null)
                    {
                        Global.MainFrame.ShowMessage(ex.Message);

                        if (Global.MainFrame.ShowMessage("WEB으로 접속하시겠습니까?", "QY2") != DialogResult.Yes) return;
                    }
                    else
                    {
                        dbdirect.ExecuteNonQuery("UP_SA_Z_HKCOS_ISU_WEBORDER", new object[] { Date });
                        return;
                    }
                }

                DBHelper.ExecuteNonQuery("UP_SA_Z_HKCOS_ISU_WEBORDER", new object[] { Date });
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        #endregion

        #region ♣ 창고별 현재고 조회
        public DataTable 창고별현재고조회(string noSo, string cd_item_multi)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_SO_SL_INV_S", new object[] { CD_COMPANY, noSo, cd_item_multi });
            return dt;
        }
        #endregion

        #region ♣ 공장별 사업장 조회
        internal int SearchBizarea(string multiCdPlant)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_BIZAREA_OF_PLANT_S", new object[] { CD_COMPANY, multiCdPlant });
            return dt.Rows.Count;
        }
        #endregion

        #region ♣ 확정여부 조회

        internal bool IsConfirm(string NO_SO)
        {
            string SQL = string.Empty;
            string 회사코드 = MA.Login.회사코드;

            SQL = " SELECT STA_SO "
                + " FROM   SA_SOL "
                + " WHERE  CD_COMPANY = '" + 회사코드 + "' AND NO_SO = '" + NO_SO + "' AND STA_SO <> 'O'";

            DataTable dt = DBHelper.GetDataTable(SQL);

            if (dt == null || dt.Rows.Count == 0)
                return false;

            return true;
        }

        #endregion

        #region ♣ 조선호텔베이커리 전용 단가 조회

        internal object 조선호텔베이커리단가(string cdPlant, string cdItem, string cdPartner, string dtSo, string cdExch)
        {
            object[] outs;
            DBHelper.ExecuteNonQuery("UP_SA_Z_CHOSUNHOTELBA_SO_UM_S", new object[] { MA.Login.회사코드, cdPlant, cdItem, cdPartner, dtSo, cdExch }, out outs);
            return outs[0];
        }

        #endregion

        #region ♣ 고정단가 조회

        internal DataTable SearchUmFixed(string cdPartner, string cdPlant, string multiItem)
        {
            return DBHelper.GetDataTable("UP_SA_SO_UM_FIXED_S", new object[] { MA.Login.회사코드, cdPartner, cdPlant, multiItem });
        }

        #endregion

        #region ♣ 수주번호 체크

        internal DataTable SearchNoSo(string NO_SO)
        {
            string SQL = string.Empty;
            string 회사코드 = MA.Login.회사코드;

            SQL = " SELECT NO_SO, DT_SO "
                + " FROM   SA_SOH "
                + " WHERE  CD_COMPANY = '" + 회사코드 + "' AND NO_SO = '" + NO_SO + "'";

            DataTable dt = DBHelper.GetDataTable(SQL);

            return dt;
        }

        #endregion

        #region ♣ 미출하 수주수량 조회

        internal DataTable SearchMiGi(string cdPlant, string cdSl, string multiItem)
        {
            return DBHelper.GetDataTable("UP_SA_SO_MI_GI_S", new object[] { MA.Login.회사코드, cdPlant, cdSl, multiItem });
        }

        #endregion

        #region ♣ 인쇄시 재고평가단가 조회

        internal void Print(DataTable dt)
        {
            dt.Columns.Add("UM_INV2", typeof(decimal));              //재고평가단가
            dt.Columns.Add("AM_INV2", typeof(decimal));              //재고평가단가 * 수량
            dt.Columns.Add("AM_PROFIT2", typeof(decimal));           //이익
            dt.Columns.Add("DT_INV2", typeof(string));               //재고평가월

            string multiCdItem = Common.MultiString(dt, "CD_ITEM", "|");
            DataTable dtUmInv = DBHelper.GetDataTable("UP_SA_ESTIMATE_COST_S", new object[] { MA.Login.회사코드, D.GetString(dt.Rows[0]["CD_PLANT"]), multiCdItem });
            dtUmInv.PrimaryKey = new DataColumn[] { dtUmInv.Columns["CD_ITEM"] };
            decimal umInv = decimal.Zero;
            string dtUnv = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                DataRow rowUmInv = dtUmInv.Rows.Find(row["CD_ITEM"]);
                if (rowUmInv == null)
                {
                    umInv = decimal.Zero;
                    dtUnv = string.Empty;
                }
                else
                {
                    umInv = D.GetDecimal(rowUmInv["UM_INV"]);
                    dtUnv = D.GetString(rowUmInv["DT_INV"]);
                }

                row["UM_INV2"] = umInv;
                row["AM_INV2"] = D.GetDecimal(row["QT_SO"]) * umInv;
                row["AM_PROFIT2"] = D.GetDecimal(row["AM_WONAMT"]) - D.GetDecimal(row["AM_INV2"]);
                row["DT_INV2"] = dtUnv;
            }
        }

        #endregion

        #region ♣ 수주번호 체크

        internal DataRow SearchSo(string NO_SO, out bool 헤더수정유무, out string 단가적용형태)
        {
            string SQL = string.Empty;
            string 회사코드 = MA.Login.회사코드;
            헤더수정유무 = true;

            SQL = " SELECT H.CD_SALEGRP, H.STA_SO, L.TP_BUSI, L.TP_GI, L.TP_IV, L.GIR, L.GI, L.IV, L.TRADE, L.STA_SO AS STA_SO_LINE"
                + " FROM   SA_SOH H"
                + "        INNER JOIN SA_SOL L ON H.CD_COMPANY = L.CD_COMPANY AND H.NO_SO = L.NO_SO"
                + " WHERE  H.CD_COMPANY = '" + 회사코드 + "' AND H.NO_SO = '" + NO_SO + "'";

            DataTable dt = DBHelper.GetDataTable(SQL);

            foreach (DataRow row in dt.Rows)
            {
                if (D.GetString(row["STA_SO_LINE"]) != "O")
                {
                    헤더수정유무 = false;
                    break;
                }
            }
            
            단가적용형태 = D.GetString(BASIC.GetSaleGrp(D.GetString(dt.Rows[0]["CD_SALEGRP"]))["TP_SALEPRICE"]);

            return dt.Rows[0];
        }

        #endregion

        /// <summary>
        /// 분할의뢰, 출하되었는지 체크
        /// </summary>
        /// <param name="수주번호">수주번호</param>
        /// <param name="수주항번">수주항번</param>
        /// <returns></returns>
        internal bool LineCheck(string 수주번호, decimal 수주항번)
        {
            string SQL = string.Empty;
            string 회사코드 = MA.Login.회사코드;

            SQL = " SELECT SUM(QT_SO) QT_SO, SUM(QT_GIR) QT_GIR, SUM(QT_GI) QT_GI "
                + " FROM   SA_SOL "
                + " WHERE  CD_COMPANY = '" + 회사코드 + "' AND NO_SO = '" + 수주번호 + "' AND SEQ_SO = " + 수주항번 + "";

            DataTable dt = DBHelper.GetDataTable(SQL);

            decimal sumQtSo = D.GetDecimal(dt.Rows[0]["QT_SO"]);
            decimal sumQtGir = D.GetDecimal(dt.Rows[0]["QT_GIR"]);
            decimal sumQtGi = D.GetDecimal(dt.Rows[0]["QT_GI"]);

            if (sumQtGi > 0)
            {
                Global.MainFrame.ShowMessage("이미 출하등록 된 건이 존재합니다.");
                return false;
            }

            if (sumQtGir != 0m && sumQtSo != sumQtGir)
            {
                Global.MainFrame.ShowMessage("수주수량이 분할되어 작업을 수행 할 수 없습니다.");
                return false;
            }

            return true;
        }

        internal DataTable SearchMfgAuth()
        {
            string query = " SELECT A.CD_AUTH CODE, B.NM_SYSDEF NAME"
                         + " FROM   MA_MFG_AUTH A"
                         + "        INNER JOIN MA_CODEDTL B ON A.CD_COMPANY = B.CD_COMPANY AND B.CD_FIELD = 'SA_B000021' AND A.CD_AUTH = B.CD_SYSDEF"
                         + " WHERE  A.CD_COMPANY = '" + MA.Login.회사코드 + "'"
                         + " AND    A.NO_EMP = '" + MA.Login.사원번호 + "'"
                         + " AND    A.FG_AUTH = 'TP_PRICE'"
                         + " AND    B.USE_YN = 'Y'";
            return DBHelper.GetDataTable(query);
        }

        #region ♣ 첨부파일 도움창 체크

        internal DataTable IsFileHelpCheck()
        {
            string query = string.Empty;

            query = " SELECT TP_FILESERVER"
                  + " FROM   MA_ENV "
                  + " WHERE  CD_COMPANY = '" + MA.Login.회사코드 + "'";

            DataTable dt = DBHelper.GetDataTable(query);
            return dt;
        } 
        #endregion

        #region ♣ 인쇄시 사업장정보 조회

        internal DataRow SerchBizarea(string cdPlant)
        {
            string query = string.Empty;

            query = " SELECT B.NO_BIZAREA, B.NM_BIZAREA, B.NM_MASTER, B.ADS_H, B.ADS_D, B.TP_JOB, B.CLS_JOB, B.NO_TEL "
                  + " FROM   MA_PLANT P "
                  + "        INNER JOIN MA_BIZAREA B ON P.CD_COMPANY = B.CD_COMPANY AND P.CD_BIZAREA = B.CD_BIZAREA "
                  + " WHERE  P.CD_COMPANY = '" + MA.Login.회사코드 + "'"
                  + " AND    P.CD_PLANT = '" + cdPlant + "'";

            DataTable dt = DBHelper.GetDataTable(query);
            return dt.Rows[0];
        }
        #endregion

        #region ♣ 출력
        public DataTable Search_Print(string NO_SO)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_SO_PRINT_S", new object[] { MA.Login.회사코드, NO_SO });
            T.SetDefaultValue(dt);
            return dt;
        }
        #endregion

        #region ♣ 물류사용자별세부메뉴설정 체크
        public DataTable AuthUserMenu(string pageId)
        {
            string sql = string.Empty;

            sql = " SELECT ISNULL(YN_UM, 'N') YN_UM, ISNULL(YN_AM, 'N') YN_AM"
                + " FROM   MA_MFG_MENU_AUTH"
                + " WHERE  CD_COMPANY = '" + MA.Login.회사코드 + "'"
                + " AND    NO_EMP = '" + MA.Login.사원번호 + "'"
                + " AND    FG_AUTH = '001'"
                + " AND    ID_MENU = '" + pageId + "'";

            return DBHelper.GetDataTable(sql);
        } 
        #endregion

        internal DataTable SearchItemGrpUm(string cdPlant, string tpSo, string dtSo)
        {
            string query = " SELECT CD_ITEMGRP, UM_SALES, UM_CONTRIBUTE"
                         + " FROM   SA_Z_SDBIO_ITEMGRP_UM"
                         + " WHERE  CD_COMPANY = '" + MA.Login.회사코드 + "'"
                         + " AND    CD_PLANT = '" + cdPlant + "'"
                         + " AND    TP_SO = '" + tpSo + "'"
                         + " AND    '" + dtSo + "' BETWEEN SDT_UM AND EDT_UM";
            DataTable dt = DBHelper.GetDataTable(query);
            T.SetDefaultValue(dt);
            dt.PrimaryKey = new DataColumn[] { dt.Columns["CD_ITEMGRP"] };
            return dt;
        }

        internal DataTable Search_SDBIO_UM(string cdPlant, string multiItem, string cdPartner, string dtSo, string fgUm, string cdExch)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_Z_SDBIO_ITEM_UMPARTNER_S", new object[] { MA.Login.회사코드, cdPlant, multiItem, cdPartner, dtSo, fgUm, cdExch });
            T.SetDefaultValue(dt);
            return dt;
        }

        /// <summary>
        /// 가장 최근에 입고된(입고일자기준) LOT 번호 표시(차앤박 전용)
        /// </summary>
        internal object SearchNoLotCNP(string cdSl, string cdItem)
        {
            string query = " SELECT  TOP 1 NO_LOT"
                         + " FROM    MM_QTIOLOT"
                         + " WHERE   CD_COMPANY = '" + MA.Login.회사코드 + "'"
                         + " AND     FG_PS = '1'"
                         + " AND     CD_SL = '" + cdSl + "'"
                         + " AND     CD_ITEM = '" + cdItem + "'"
                         + " AND     QT_IO - ISNULL(QT_MGMT, 0) > 0"
                         + " ORDER BY DT_IO DESC, NO_IO DESC";

            DataTable dt = DBHelper.GetDataTable(query);

            if (dt == null || dt.Rows.Count == 0) return string.Empty;
            else return D.GetString(dt.Rows[0]["NO_LOT"]);
        }

        #region ♣ 상보기업 할인율

        internal DataTable 상보할인율(string 공장, string 거래처, string 수주일자, DataRow[] dr품목)
        {
            string 멀티품목 = "";
            foreach(DataRow row in dr품목)
            {
                멀티품목 += D.GetString(row["CD_ITEM"]) + "|";
            }

            DataTable dt = DBHelper.GetDataTable("UP_SA_Z_SANGBO_SO_DC_S", new object[] { MA.Login.회사코드, 공장, 거래처, 수주일자, 멀티품목 });
            dt.PrimaryKey = new DataColumn[] { dt.Columns["CD_ITEM"] };
            return dt;
        }

        internal void 상보할인율Save(DataTable dt)
        {
            if (dt == null) return;

            SpInfoCollection sc = new SpInfoCollection();

            SpInfo siL = new SpInfo();

            if (dt != null)
            {
                siL.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siL.UserID = Global.MainFrame.LoginInfo.UserID;
                siL.DataValue = dt;
                siL.SpNameInsert = "UP_SA_Z_SANGBO_UM_PARTNER_I";
                siL.SpNameUpdate = "UP_SA_Z_SANGBO_UM_PARTNER_I";
                siL.SpParamsInsert = new string[] { "CD_COMPANY", "CD_PARTNER", "CD_ITEM", "CD_PLANT", "SDT_UM", "ID_INSERT", "NUM_USERDEF1", "CLS_DT" };
                siL.SpParamsUpdate = new string[] { "CD_COMPANY", "CD_PARTNER", "CD_ITEM", "CD_PLANT", "SDT_UM", "ID_INSERT", "NUM_USERDEF1", "CLS_DT" };

                sc.Add(siL);
            }

            DBHelper.Save(sc);
        }

        #endregion

        #region ♣ 신진에스엠 수주가계산
        internal DataSet 수주가계산(object[] arg, string 수주종류)
        {
            string procName = string.Empty;

            if (수주종류 == "100")
                procName = "UP_CZ_Z_SINJINSM_BR_ORDER_SUJUGA_CALC1";
            else if (수주종류 == "200")
                procName = "UP_CZ_Z_SINJINSM_BR_ORDER_SUJUGA_CALC2";
            else if (수주종류 == "300" || 수주종류 == "400")
                procName = "UP_CZ_Z_SINJINSM_BR_ORDER_SUJUGA_CALC3";

            DataSet ds = DBHelper.GetDataSet(procName, arg);
            return ds;
        }
        #endregion

        #region ♣ 엠에이치파워시스템즈코리아 전용수주번호체크
        internal bool Chk전용수주번호(string 전용수주번호)
        {
            string SQL = string.Empty;
            SQL = " SELECT NO_SO "
                + " FROM   SA_SOH "
                + " WHERE  CD_COMPANY = '" + MA.Login.회사코드 + "' AND TXT_USERDEF4 = '" + 전용수주번호 + "'";
            DataTable dt = DBHelper.GetDataTable(SQL);
            return (dt == null || dt.Rows.Count == 0) ? true : false;
        }
        #endregion

        #region ♣ 한일도요 원료비
        internal DataRow 원료비(string 공장, string 품목코드)
        {
            string SQL = string.Empty;
            SQL = " SELECT ROUND(TOT_MATERIAL,0) TOT_MATERIAL "
                + " FROM   SA_Z_HANILTOYO_MATERIAL_COST_H "
                + " WHERE  CD_COMPANY = '" + MA.Login.회사코드 + "' AND CD_PLANT = '" + 공장 + "' AND CD_ITEM = '" + 품목코드 + "'";
            DataTable dt = DBHelper.GetDataTable(SQL);
            return (dt == null || dt.Rows.Count == 0) ? null : dt.Rows[0];
        } 
        #endregion

        #region 현재고가져오기

        public DataTable 현재고가져오기(string 품목, string 일자)
        {
            string 년도 = 일자.Substring(0, 4);
            DataTable dt = DBHelper.GetDataTable("UP_SA_Z_SANGBO_STOCK_S_DETAIL", new object[] { MA.Login.회사코드, 년도, 품목 });
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable 현재고가져오기(string 공장, string 창고, string 품목, string 일자)
        {
            string 년도 = 일자.Substring(0, 4);
            DataTable dt = DBHelper.GetDataTable("UP_SA_Z_SANGBO_STOCK_S_DETAIL2", new object[] { MA.Login.회사코드, 공장, 창고, 년도, 품목 });
            T.SetDefaultValue(dt);
            return dt;
        }
        #endregion

        #region ♣ 영우디지탈 특별여신
        public bool GetPrjCreditChk(object[] obj)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_PJTCREDIT_SELECT_CHK";
            si.SpParamsSelect = obj;
            ResultData _rtn = (ResultData)Global.MainFrame.FillDataTable(si);

            if (_rtn.OutParamsSelect[0, 1] != null && _rtn.OutParamsSelect[0, 1].ToString() != "")
            {
                if (_rtn.OutParamsSelect[0, 3].ToString() == "0")
                {
                    string AcceptCredit = string.Empty;
                    //적용여신 (001:프로젝트, 002:일반, 003:특별여신잔액)  
                    if (_rtn.OutParamsSelect[0, 0].ToString() == "001")
                        AcceptCredit = "프로젝트 특별여신";
                    else if (_rtn.OutParamsSelect[0, 0].ToString() == "002")
                        AcceptCredit = "일반여신";
                    else if (_rtn.OutParamsSelect[0, 0].ToString() == "003")
                        AcceptCredit = "특별여신잔액";

                    Global.MainFrame.ShowMessage("여신금액을 초과하여 저장할 수 없습니다. \n( " +
                                                 " 적용여신 : " + AcceptCredit + "," +
                                                 " 여신총액 : " + Duzon.ERPU.D.GetDecimal(_rtn.OutParamsSelect[0, 1].ToString()).ToString("###,###,###") + "," +
                                                     " 잔액 : " + Duzon.ERPU.D.GetDecimal(_rtn.OutParamsSelect[0, 2].ToString()).ToString("###,###,###") + ")");

                    return false;
                }
            }
            return true;
        } 
        #endregion

        #region ♣ 케이피아이씨전용 계약잔량체크
        internal DataRow 계약잔량체크(string NO_CONTRACT, decimal NO_LINE)
        {
            string SQL = string.Empty;
            SQL = " SELECT ISNULL(H.CD_TYPE,'001') CD_TYPE, L.QT_CON - ISNULL(SL.QT_SO,0) QT_CON "
                + " FROM   SA_Z_KPCI_CONTRACT_H H "
                + "        INNER JOIN SA_Z_KPCI_CONTRACT_L L ON H.CD_COMPANY = L.CD_COMPANY AND H.NO_CONTRACT = L.NO_CONTRACT "
                + "        LEFT JOIN(SELECT NO_RELATION, SEQ_RELATION, SUM(QT_SO) QT_SO "
                + "                  FROM   SA_SOL "
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

        #region ♣ 디엔컴퍼니전용 미출하수량
        internal DataRow Get_SearchMiGi(string CD_PLANT, string CD_ITEM)
        {
            string SQL = string.Empty;
            SQL = " SELECT CD_ITEM, SUM(QT_SO) - SUM(QT_GI) QT_GI_REMAIN "
                + " FROM   SA_SOL "
                + " WHERE  CD_COMPANY = '" + MA.Login.회사코드 + "'"
                + " AND    CD_PLANT = '" + CD_PLANT + "'"
                + " AND    CD_ITEM = '" + CD_ITEM + "'"
                + " GROUP BY CD_ITEM";

            DataTable dt = DBHelper.GetDataTable(SQL);
            return (dt == null || dt.Rows.Count == 0) ? null : dt.Rows[0];
        } 
        #endregion 
    }
}
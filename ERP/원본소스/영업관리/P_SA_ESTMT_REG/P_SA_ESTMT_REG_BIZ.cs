using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA.Common;
using System;

namespace sale
{
    class P_SA_ESTMT_REG_BIZ
    {
        string 서버키 = Global.MainFrame.ServerKeyCommon.ToUpper();
        string 회사코드 = MA.Login.회사코드;
        string 로그인 = MA.Login.사용자아이디;
        string 수주등록_예상이익산출 = "000";

        견적관리 _cf견적관리 = new 견적관리();
        수주관리 sobiz = new 수주관리();

        public P_SA_ESTMT_REG_BIZ()
        {
            BASIC.CacheDataClear(BASIC.CacheEnums.ALL);
            수주등록_예상이익산출 = BASIC.GetMAEXC("수주등록-예상이익산출");
        }

        internal DataTable Search(object[] Args)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_ESTMT_REG_S", Args, "DT_EST, NO_EST, NO_HST");
            T.SetDefaultValue(dt);

            dt.Columns["CD_EXCH"].DefaultValue = "000";
            dt.Columns["RT_EXCH"].DefaultValue = 1M;
            dt.Columns["DT_EST"].DefaultValue = Global.MainFrame.GetStringToday;
            dt.Columns["STA_EST"].DefaultValue = "0";
            dt.Columns["FG_VAT"].DefaultValue = "N";
            return dt;
        }

        internal DataTable DetailSearch(string 견적번호, decimal 차수)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_ESTMT_REG_S1", new object[] { 회사코드, 견적번호, 차수 }, "SEQ_EST");
            T.SetDefaultValue(dt);
            dt.Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            dt.Columns["NM_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.NmPlant;
            return dt;
        }

        internal void Delete(string 견적번호)
        {
            DBHelper.ExecuteNonQuery("UP_SA_ESTMT_REG_ALL_D", new object[] { 회사코드, 견적번호 });
        }

        internal DataSet SearchPrint(string 번호)
        {
            //string 번호 = 견적번호 + "-" + D.GetString(차수) + "|";
            return DBHelper.GetDataSet("UP_SA_ESTMT_PRT_PRINT", new object[] { 회사코드, 번호 }, new string[] { "NO_EST, NO_HST, SEQ_EST", "NO_EST, NO_HST, SEQ_EST, CD_CITEM" });
        }

        internal void 확정(string 견적번호, decimal 차수)
        {
            DBHelper.ExecuteNonQuery("UP_SA_ESTMT_REG_ACCEPT", new object[] { 회사코드, 견적번호, 차수, "1", 로그인 });
        }

        internal void 확정취소(string 견적번호, decimal 차수)
        {
            DBHelper.ExecuteNonQuery("UP_SA_ESTMT_REG_ACCEPT", new object[] { 회사코드, 견적번호, 차수, "0", 로그인 });
        }

        internal void Save(DataTable dtH, DataTable dtL)
        {
            if (dtH == null && dtL == null) return;

            DataTable dtH추가 = null;
            DataTable dtH수정 = null;
            DataTable dtH삭제 = null;

            if (dtH != null)
            {
                dtH추가 = dtH.GetChanges(DataRowState.Added);
                dtH수정 = dtH.GetChanges(DataRowState.Modified);
                dtH삭제 = dtH.GetChanges(DataRowState.Deleted);
            }

            SpInfoCollection sc = new SpInfoCollection();
            SpInfo si = null;

            if (dtH추가 != null)
            {
                si = new SpInfo();
                si.DataValue = dtH추가;
                si.CompanyID = 회사코드;
                si.UserID = 로그인;
                si.SpNameInsert = "UP_SA_ESTMT_H_INSERT";
                si.SpParamsInsert = new string[] {  "DT_EST",       "NO_HST",           "NO_EST",       "CD_COMPANY",   "NO_EST_NM",    "TP_SO", 
                                                    "CD_PARTNER",   "NM_PARTNER_IMSI",  "NO_PO",        "CD_BIZAREA",   "CD_SALEGRP",   "NO_EMP", 
                                                    "CD_EXCH",      "RT_EXCH",          "TP_VAT",       "FG_VAT",       "RT_VAT",       "DC_RMK1", 
                                                    "DC_RMK2",      "DC_RMK3",          "FG_BILL",      "DT_VALID",     "DT_DUEDATE",   "CD_EXPORT", 
                                                    "CD_PRODUCT",   "COND_TRANS",       "COND_PAY",     "COND_DAYS",    "TP_PACKING",   "TP_TRANS",
                                                    "TP_TRANSPORT", "NM_INSPECT",       "PORT_LOADING", "PORT_ARRIVER", "CD_ORIGIN",    "DESTINATION", 
                                                    "DT_EXPIRY",    "STA_EST",          "DT_CONT",      "NO_SO",        "ID_INSERT",    "NM_EMP_PARTNER", 
                                                    "DC_RMK4",      "DC_RMK5",          "DC_RMK6",      "DC_RMK_TEXT",  "COND_PRICE",   "DATE_USERDEF1", 
                                                    "DATE_USERDEF2","TEXT_USERDEF1",    "TEXT_USERDEF2","TEXT_USERDEF3","TEXT_USERDEF4","TEXT_USERDEF5", 
                                                    "TEXT_USERDEF6","CD_USERDEF1",      "CD_USERDEF2",  "CD_USERDEF3",  "CD_USERDEF4",  "CD_USERDEF5",
                                                    "CD_USERDEF6",  "NUM_USERDEF1",     "NUM_USERDEF2", "NUM_USERDEF3", "NUM_USERDEF4", "NUM_USERDEF5",
                                                    "NUM_USERDEF6", "NO_PROJECT"
                                                };
                sc.Add(si);
            }

            if (dtH수정 != null)
            {
                si = new SpInfo();
                si.DataValue = dtH수정;
                si.CompanyID = 회사코드;
                si.UserID = 로그인;
                si.SpNameUpdate = "UP_SA_ESTMT_H_UPDATE";
                si.SpParamsUpdate = new string[] {  "DT_EST",       "NO_HST",           "NO_EST",       "CD_COMPANY",   "NO_EST_NM",    "TP_SO", 
                                                    "CD_PARTNER",   "NM_PARTNER_IMSI",  "NO_PO",        "CD_BIZAREA",   "CD_SALEGRP",   "NO_EMP", 
                                                    "CD_EXCH",      "RT_EXCH",          "TP_VAT",       "FG_VAT",       "RT_VAT",       "DC_RMK1", 
                                                    "DC_RMK2",      "DC_RMK3",          "FG_BILL",      "DT_VALID",     "DT_DUEDATE",   "CD_EXPORT", 
                                                    "CD_PRODUCT",   "COND_TRANS",       "COND_PAY",     "COND_DAYS",    "TP_PACKING",   "TP_TRANS",
                                                    "TP_TRANSPORT", "NM_INSPECT",       "PORT_LOADING", "PORT_ARRIVER", "CD_ORIGIN",    "DESTINATION", 
                                                    "DT_EXPIRY",    "STA_EST",          "DT_CONT",      "NO_SO",        "ID_UPDATE",    "NM_EMP_PARTNER", 
                                                    "DC_RMK4",      "DC_RMK5",          "DC_RMK6",      "DC_RMK_TEXT",  "COND_PRICE",   "DATE_USERDEF1", 
                                                    "DATE_USERDEF2","TEXT_USERDEF1",    "TEXT_USERDEF2","TEXT_USERDEF3","TEXT_USERDEF4","TEXT_USERDEF5", 
                                                    "TEXT_USERDEF6","CD_USERDEF1",      "CD_USERDEF2",  "CD_USERDEF3",  "CD_USERDEF4",  "CD_USERDEF5",
                                                    "CD_USERDEF6",  "NUM_USERDEF1",     "NUM_USERDEF2", "NUM_USERDEF3", "NUM_USERDEF4", "NUM_USERDEF5", 
                                                    "NUM_USERDEF6", "NO_PROJECT"
                                                };
                sc.Add(si);
            }

            if (dtL != null)
            {
                si = new SpInfo();
                si.DataValue = dtL;
                si.CompanyID = 회사코드;
                si.UserID = 로그인;
                si.SpNameInsert = "UP_SA_ESTMT_L_INSERT";
                si.SpNameUpdate = "UP_SA_ESTMT_L_UPDATE";
                si.SpNameDelete = "UP_SA_ESTMT_L_DELETE";
                si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_EST", "NO_HST", "SEQ_EST", "CD_PLANT", "CD_ITEM", "QT_EST", "UM_STD", "RT_DC", "UM_EST", "AM_EST", "AM_K_EST", "AM_VAT", "ID_INSERT", "DT_DUEDATE", "NM_USERDEF1", "NM_USERDEF2", "NM_USERDEF3", "NUM_USERDEF1", "NUM_USERDEF2" };
                si.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_EST", "NO_HST", "SEQ_EST", "CD_PLANT", "CD_ITEM", "QT_EST", "UM_STD", "RT_DC", "UM_EST", "AM_EST", "AM_K_EST", "AM_VAT", "ID_UPDATE", "DT_DUEDATE", "NM_USERDEF1", "NM_USERDEF2", "NM_USERDEF3", "NUM_USERDEF1", "NUM_USERDEF2" };
                si.SpParamsDelete = new string[] { "CD_COMPANY", "NO_EST", "NO_HST", "SEQ_EST" };
                sc.Add(si);
            }

            if (dtH삭제 != null)
            {
                si = new SpInfo();
                si.DataValue = dtH삭제;
                si.CompanyID = 회사코드;
                si.UserID = 로그인;
                si.SpNameDelete = "UP_SA_ESTMT_H_DELETE";
                si.SpParamsDelete = new string[] { "NO_HST", "NO_EST", "CD_COMPANY" };
                sc.Add(si);
            }

            DBHelper.Save(sc);
        }

        internal DataTable GetSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NO_EST", typeof(string));
            dt.Columns.Add("NO_HST", typeof(decimal));
            return dt;
        }

        internal void 견적기준가(string 공장, string 품목, string 환종, out decimal 기준가)
        {
            decimal 원가, 마진율;
            _cf견적관리.Get견적기준가(공장, 품목, 환종, out 원가, out 마진율, out 기준가);
        }

        #region -> 할인율조회

        public decimal 할인율조회(object[] obj)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_UM_ITEMGRP_PARTGRP_DC_S";
            si.SpParamsSelect = obj;
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;

            decimal d할인율 = 0m;

            if (dt.Rows.Count > 0)
                d할인율 = Duzon.ERPU.D.GetDecimal(dt.Rows[0]["DC_RATE"]);

            return d할인율;
        }

        #endregion

        #region -> 견적기준가조회_수주등록_유형별할인율적용

        public decimal 견적기준가조회_수주등록_유형별할인율적용(object[] obj)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_UM_TPPRICE_SELECT";
            si.SpParamsSelect = obj;
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;

            decimal d견적기준가 = 0m;

            if (dt.Rows.Count > 0)
                d견적기준가 = Duzon.ERPU.D.GetDecimal(dt.Rows[0]["UM_ITEM"]);

            return d견적기준가;
        }

        #endregion

        #region -> 견적기준가조회_수주등록_거래처별할인율적용

        public decimal 견적기준가조회_수주등록_거래처별할인율적용(object[] obj)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_UM_PARTNER_SELECT";
            si.SpParamsSelect = obj;
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;

            decimal d견적기준가 = 0m;

            if (dt.Rows.Count > 0)
                d견적기준가 = Duzon.ERPU.D.GetDecimal(dt.Rows[0]["UM_ITEM"]);

            return d견적기준가;
        }

        #endregion

        //internal decimal Max견적차수(string 견적번호)
        //{
        //    string sql = "SELECT MAX(NO_HST) NO_HST FROM SA_ESTMT_H WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_EST = '" + 견적번호 + "'";
        //    DataTable dt = DBHelper.GetDataTable(sql);
        //    if (dt == null || dt.Rows.Count == 0) return 0;

        //    return D.GetDecimal(dt.Rows[0]["NO_HST"]);
        //}

        internal 수주관리.예상이익산출 Get예상이익산출적용
        {
            get
            {
                if (수주등록_예상이익산출 == "001") return 수주관리.예상이익산출.재고단가를원가로산출;
                return 수주관리.예상이익산출.NONE;
            }
        }

        internal DataTable 예상이익(string 공장, string 수주일자, DataRow[] dr품목)
        {
            품목관리.조회 품목조회 = new 품목관리.조회();
            DataTable dt = 품목조회.예상이익(공장, 수주일자, dr품목);
            dt.PrimaryKey = new DataColumn[] { dt.Columns["CD_ITEM"] };
            return dt;
        }

        internal DataTable 현재고(string 회사, string 공장, string 품목 )
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_QT_INV_SELECT", new object[] { 회사, 공장, 품목 });
            return dt;
        }

        internal DataTable 기발주수량(string 회사, string 공장, string 품목)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_QT_PO_SELECT", new object[] { 회사, 공장, 품목 });
            return dt;
        }

        internal DataTable 할인율코드()
        {
            string sql품목군 = "SELECT FG_DISCOUNT AS CODE, NM_DISCOUNT AS NAME FROM SA_DISCOUNTH WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'";
                                                                        
            DataTable dt = DBHelper.GetDataTable(sql품목군);
            return dt;
        }

        internal DataTable 할인율(string 공장, decimal 수량, string 견적일자, string 할인구분코드)
        {
            string sql할인율 = "SELECT MAX(L.NO_LINE) AS NO_LINE,(SELECT RT_DISCOUNT FROM SA_DISCOUNTL WHERE CD_COMPANY = L.CD_COMPANY AND CD_PLANT = L.CD_PLANT AND FG_DISCOUNT = '"+할인구분코드 +"' AND NO_LINE = MAX(L.NO_LINE) AND DT_START <= '"+ 견적일자 + "'AND DT_END >= '"+ 견적일자 + "') AS RT_DISCOUNT"
                              + " FROM SA_DISCOUNTL L "
                              + " WHERE L.CD_COMPANY  = '" + MA.Login.회사코드 + "'"
                              + " AND L.QT_MIN       <= '" + 수량 + "'"
                              + " AND L.QT_MAX       >= '" + 수량 + "'"
                              + " AND L.DT_START     <= '" + 견적일자 + "'"
                              + " AND L.DT_END       >= '" + 견적일자 + "'"
                              + " AND L.FG_DISCOUNT  >= '" + 할인구분코드 + "'"
                              + " GROUP BY L.CD_COMPANY , L.CD_PLANT";

            DataTable dt = DBHelper.GetDataTable(sql할인율);
            return dt;
        }

       
    }
}

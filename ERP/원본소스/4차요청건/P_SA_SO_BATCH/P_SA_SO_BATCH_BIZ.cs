using System.Data;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

namespace sale
{
    public class P_SA_SO_BATCH_BIZ
    {
        DataTable dt프로젝트 = null;
        string ATP사용여부 = "000";
        string 수주라인_과세변경유무 = "N";

        public P_SA_SO_BATCH_BIZ()
        {
            BASIC.CacheDataClear(BASIC.CacheEnums.ALL);
            ATP사용여부 = BASIC.GetMAEXC("ATP사용여부");
            수주라인_과세변경유무 = BASIC.GetMAEXC("수주라인-과세변경유무");
        }

        #region ♣ 저장
        public bool Save(DataTable dt_SoH, DataTable dt_SoL, string 매출자동여부)
        {
            SpInfoCollection sic = new SpInfoCollection();

            if (dt_SoH != null)
            {
                SpInfo siM = new SpInfo();

                siM.DataValue = dt_SoH;
                siM.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siM.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siM.SpNameInsert = "UP_SA_SOH_INSERT";
                siM.SpParamsInsert = new string[] { "CD_COMPANY",   "NO_SO",        "CD_BIZAREA",       "DT_SO",        "CD_PARTNER", 
                                                    "CD_SALEGRP",   "NO_EMP",       "TP_SO",            "CD_EXCH",      "RT_EXCH", 
                                                    "TP_PRICE",     "NO_PROJECT",   "TP_VAT",           "RT_VAT",       "FG_VAT",       
                                                    "FG_TAXP",      "DC_RMK",       "FG_BILL",          "FG_TRANSPORT", "NO_CONTRACT",  
                                                    "STA_SO",       "FG_TRACK",     "NO_PO_PARTNER",    "ID_INSERT" ,   "RMA_REASON",
                                                    "DC_RMK1"};
                sic.Add(siM);
            }

            if (MA.ServerKey(false, new string[] { "MCIRCLE", "LINEP", "DZSQL", "CNP", "DINTEC" }))
            {
                if (Get과세변경유무 == "N" && 매출자동여부 == "Y")
                {
                    SpInfo si = new SpInfo();
                    si.DataValue = dt_SoH;
                    si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                    si.SpNameInsert = "UP_SA_SOH_SUB_I";
                    si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_SO", "DT_PROCESS", "DT_RCP_RSV", "FG_AR_EXC", "AM_IV", "AM_IV_EX", "AM_IV_VAT", "NM_PTR", "EX_EMIL", "EX_HP", "ID_INSERT" };
                    sic.Add(si);
                }
            }

            if (dt_SoL != null)
            {
                SpInfo siD = new SpInfo();

                siD.DataValue = dt_SoL;
                siD.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siD.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siD.SpNameInsert = "UP_SA_SOL_INSERT"; 
                siD.SpParamsInsert = new string[] { "CD_COMPANY",       "NO_SO",            "SEQ_SO",           "CD_PLANT",         "CD_ITEM", 
                                                    "UNIT_SO",          "DT_DUEDATE",       "DT_REQGI",         "QT_SO",            "UM_SO",      
                                                    "AM_SO",            "AM_WONAMT",        "AM_VAT",           "UNIT_IM",          "QT_IM",      
                                                    "CD_SL",            "TP_ITEM",          "STA_SO",           "TP_BUSI",          "TP_GI",     
                                                    "TP_IV",            "GIR",              "GI",               "IV",               "TRADE",            
                                                    "TP_VAT",           "RT_VAT",           "GI_PARTNER",       "ID_INSERT",        "NO_PROJECT",       
                                                    "SEQ_PROJECT",      "CD_ITEM_PARTNER",  "NM_ITEM_PARTNER",  "DC1",              "DC2",              
                                                    "UMVAT_SO",         "AMVAT_SO",         "CD_SHOP",          "CD_SPITEM",        "CD_OPT",           
                                                    "RT_DSCNT",         "UM_BASE",          "NM_CUST_DLV",      "CD_ZIP",           "ADDR1",            
                                                    "ADDR2",            "NO_TEL_D1",        "NO_TEL_D2",        "TP_DLV",           "DC_REQ",           
                                                    "FG_TRACK",         "TP_DLV_DUE",       "FG_USE",           "CD_CC",            "NO_IO_MGMT", 
                                                    "NO_IOLINE_MGMT",   "NO_POLINE_PARTNER","NO_ORDER",         "NM_CUST",          "NO_TEL1",
                                                    "NO_TEL2",          "TXT_USERDEF1",     "NO_PO_PARTNER",    "FG_USE2",          "NO_RELATION",
                                                    "SEQ_RELATION",     "NUM_USERDEF1",     "NUM_USERDEF2",     "SOL_TXT_USERDEF1", "SOL_TXT_USERDEF2", 
                                                    "CD_MNGD1",         "CD_MNGD2",         "CD_MNGD3",         "CD_MNGD4" };
                siD.SpParamsValues.Add(ActionState.Insert, "NO_IO_MGMT", string.Empty);
                siD.SpParamsValues.Add(ActionState.Insert, "NO_IOLINE_MGMT", 0);
                siD.SpParamsValues.Add(ActionState.Insert, "FG_USE2", string.Empty);
                sic.Add(siD);
            }

            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sic);

            for (int i = 0; i < rtn.Length; i++)
                if (!rtn[i].Result) return false;

            return true;
        }
        #endregion

        #region ♣ 수주형태에 따른 해당 거래유형의 코드값 가져오기
        /// <summary>
        /// GetTpBusi() : 수주형태에 따른 해당 거래유형의 값을 가져와서 VAT구분에 반영한다.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="[0] : COMPANY"></param>
        /// <param name="[1] : TP_SO"></param>
        /// <param name="[2] : TP_VAT"></param>
        /// <returns>[0] : 거래구분</returns>
        /// <returns>[1] : VAT구분</returns>
        /// <returns>[2] : 수주상태</returns>
        public string[] GetTpBusi(object[] obj)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_SO_TPBUSI_SELECT";
            si.SpParamsSelect = obj;
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);

            //SP (UP_SA_SO_TPBUSI_SELECT) 에서 리턴값을 3개 던져준다.
            string[] ret = new string[3];
            ret[0] = result.OutParamsSelect[0, 0].ToString();
            ret[1] = result.OutParamsSelect[0, 1].ToString();
            ret[2] = result.OutParamsSelect[0, 2].ToString();

            return ret;
        }
        #endregion

        #region ♣ 엑셀데이터 검사 하려고 가져오는 마스터 데이터들
        public DataSet SO_PK_Check(object[] obj)
        {
            ResultData rd = (ResultData)Global.MainFrame.FillDataSet("UP_SA_SO_BATCH_CHECK_SELECT", obj);
            DataSet ds = (DataSet)rd.DataValue;

            return ds;
        }
        #endregion

        internal DataTable Get_dt프로젝트()
        {
            string sql품목군 = "SELECT NO_PROJECT, NM_PROJECT FROM SA_PROJECTH WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'";
            dt프로젝트 = DBHelper.GetDataTable(sql품목군);
            return dt프로젝트;
        }

        #region ♣ 단가적용
        public decimal UmSearch(object[] obj)
        {
            FlexGrid _flex = new FlexGrid();

            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_SO_SELECT1";
            si.SpParamsSelect = obj;
            ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);

            return _flex.CDecimal(rtn.OutParamsSelect[0, 0].ToString());
            //return _flex.CDecimal(rtn.DataValue.ToString());
        }
        #endregion

        #region ♣ 단가통제 적용 여부 조회
        public string GetSaleOrgUmCheck(object[] obj)
        {
            string SelectQuery = string.Empty;

            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
            {
                SelectQuery = " SELECT ISNULL(SO.SO_PRICE, 'N') AS SO_PRICE " +
                                     "   FROM MA_SALEGRP SG " +
                                     "   LEFT JOIN MA_SALEORG SO ON SG.CD_COMPANY = SO.CD_COMPANY AND SG.CD_SALEORG = SO.CD_SALEORG " +
                                     "  WHERE SG.CD_COMPANY = '" + obj[0].ToString() + "' " +
                                     "    AND SG.CD_SALEGRP = '" + obj[1].ToString() + "'";
            }
            else if (Global.MainFrame.DatabaseType == EnumDbType.ORACLE)
            {
                SelectQuery = " SELECT NVL(SO.SO_PRICE, 'N') AS SO_PRICE " +
                                     "   FROM MA_SALEGRP SG " +
                                     "   LEFT JOIN MA_SALEORG SO ON SG.CD_COMPANY = SO.CD_COMPANY AND SG.CD_SALEORG = SO.CD_SALEORG " +
                                     "  WHERE SG.CD_COMPANY = '" + obj[0].ToString() + "' " +
                                     "    AND SG.CD_SALEGRP = '" + obj[1].ToString() + "'";
            }

            string so_Price = D.GetString(Global.MainFrame.ExecuteScalar(SelectQuery));

            return so_Price;
        }
        #endregion

        #region ♣ 환율정보조회
        public decimal ExchangeSearch(object[] obj)
        {
            decimal rt_exch = 1;
            string SelectQuery = "SELECT RATE_BASE " +
                                 "  FROM MA_EXCHANGE " +
                                 " WHERE YYMMDD = '" + obj[1].ToString() + "' " +
                                 "   AND CURR_SOUR = '" + obj[2].ToString() + "' " +
                                 "   AND CURR_DEST = '000' " +
                                 "   AND CD_COMPANY = '" + obj[0].ToString() + "'";

            DataTable dt = DBHelper.GetDataTable(SelectQuery);

            if (dt.Rows.Count > 0)
            {
                if (D.GetDecimal(dt.Rows[0]["RATE_BASE"]) != 0)
                    rt_exch = D.GetDecimal(dt.Rows[0]["RATE_BASE"]);
            }

            return rt_exch;
        }
        #endregion

        #region ♣ 영업환경설정 가져오기
        public DataTable search_EnvMng()
        {
            object[] obk = new object[1];
            obk[0] = Global.MainFrame.LoginInfo.CompanyCode;

            //재고단위EDIT여부(2중단위관리여부)사용여부CHK
            return DBHelper.GetDataTable("UP_SA_ENV_SELECT", obk);
        }
        #endregion

        internal DataTable SearchCpItem(string cdPartner, string multiCpItem)
        {
            return DBHelper.GetDataTable("UP_SA_CPITEM_S", new object[] { MA.Login.회사코드, cdPartner, multiCpItem });
        }

        #region ♣ 공장별 사업장 조회
        internal int SearchBizarea(string multiCdPlant)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_BIZAREA_OF_PLANT_S", new object[] { MA.Login.회사코드, multiCdPlant });
            return dt.Rows.Count;
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
                case DefaultSettings.단가유형:
                    return Properties.Settings.Default.단가유형;
                default:
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
                case DefaultSettings.단가유형:
                    Properties.Settings.Default.단가유형 = val;
                    break;
                default:
                    break;
            }
            Properties.Settings.Default.Save();
        }

        #endregion

        internal string Get과세변경유무 { get { return 수주라인_과세변경유무; } }

        #region ♣ 품목정보
        internal DataTable 품목정보(string multi_item)
        {
            string SelectQuery = string.Empty;

            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
            {
                SelectQuery = " SELECT P.CD_PLANT, P.CD_ITEM, P.NM_ITEM, P.STND_ITEM, P.UNIT_SO, P.UNIT_IM, ISNULL(P.UNIT_SO_FACT,0) AS UNIT_SO_FACT, " +
                                  " P.LT_GI, ISNULL(P.CD_GISL, '') AS CD_GISL, P.TP_ITEM, RTRIM(P.FG_TAX_SA) AS FG_TAX_SA, VT.CD_FLAG1 AS RT_TAX_SA, " +
                                  " P.CD_USERDEF1, P.CD_USERDEF2, P.CD_USERDEF3, P.CD_USERDEF4, P.CD_USERDEF5, P.CD_USERDEF6, P.CD_USERDEF7, " +
                                  " P.CD_USERDEF8, P.CD_USERDEF9 " +
                                  "   FROM MA_PITEM P " +
                                  " LEFT OUTER JOIN MA_CODEDTL VT ON P.CD_COMPANY = VT.CD_COMPANY AND VT.CD_FIELD = 'MA_B000040' AND P.FG_TAX_SA = VT.CD_SYSDEF " +
                                  "  WHERE P.CD_COMPANY = '" + MA.Login.회사코드 + "' " +
                                  " AND (P.CD_ITEM IN (SELECT CD_STR FROM GETTABLEFROMSPLIT( '" + multi_item + "'))) "; 
            }
            else if (Global.MainFrame.DatabaseType == EnumDbType.ORACLE)
            {

                SelectQuery = " SELECT P.CD_PLANT, P.CD_ITEM, P.NM_ITEM, P.STND_ITEM, P.UNIT_SO, P.UNIT_IM, ISNULL(P.UNIT_SO_FACT,0) AS UNIT_SO_FACT, " +
                                 " P.LT_GI, ISNULL(P.CD_GISL, '') AS CD_GISL, P.TP_ITEM, RTRIM(P.FG_TAX_SA) AS FG_TAX_SA, VT.CD_FLAG1 AS RT_TAX_SA, " +
                                 " P.CD_USERDEF1, P.CD_USERDEF2, P.CD_USERDEF3, P.CD_USERDEF4, P.CD_USERDEF5, P.CD_USERDEF6, P.CD_USERDEF7, " +
                                 " P.CD_USERDEF8, P.CD_USERDEF9 " +
                                 "   FROM MA_PITEM P " +
                                 " LEFT OUTER JOIN MA_CODEDTL VT ON P.CD_COMPANY = VT.CD_COMPANY AND VT.CD_FIELD = 'MA_B000040' AND P.FG_TAX_SA = VT.CD_SYSDEF " +
                                 "  WHERE P.CD_COMPANY = '" + MA.Login.회사코드 + "' " +
                                 "  AND ((P.CD_ITEM IN (SELECT CD_STR FROM TABLE(CAST(GETTABLEFROMSPLIT('" + multi_item + "') AS TB_GETTABLEFROMSPLITTAB)))))";

                   
            }
           

            DataTable dt = DBHelper.GetDataTable(SelectQuery);
          
          

            return dt;
        }
        #endregion


    }
}

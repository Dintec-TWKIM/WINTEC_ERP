using System;
using System.Data;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA.Common;

namespace sale
{
    class P_SA_GIR_BIZ
    {
        string ATP사용여부 = "000";

        public P_SA_GIR_BIZ()
        {
            ATP사용여부 = BASIC.GetMAEXC("ATP사용여부");
        }

        #region ♣ 조회
        public DataSet Search(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("UP_SA_GIR_SELECT", obj);
            T.SetDefaultValue(ds);
            ds.Tables[1].Columns.Add("S", typeof(string));
            return ds;
        }
        #endregion

        #region ♣ 저장
        public bool Save(DataTable dtH, DataTable dtL)
        { 
            SpInfoCollection sic = new SpInfoCollection();

            if (dtH != null)
            {
                SpInfo siM = new SpInfo();
                siM.DataValue = dtH;
                siM.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siM.UserID = Global.MainFrame.LoginInfo.UserID;//USERID로 변경
                siM.SpNameInsert = "UP_SA_GIR_INSERT";
                siM.SpNameUpdate = "UP_SA_GIR_UPDATE";
                siM.SpParamsInsert = new string[] { "CD_COMPANY", "NO_GIR", "DT_GIR", "CD_PARTNER", "CD_PLANT", "NO_EMP", "TP_BUSI", "DC_RMK", "DC_RMK1", "DC_RMK2", "YN_RETURN", "ID_INSERT",
                                                    "TXT_USERDEF1", "TXT_USERDEF2", "TXT_USERDEF3", "TXT_USERDEF4", "CD_USERDEF1", "CD_USERDEF2", "CD_USERDEF3", "CD_USERDEF4", "NUM_USERDEF1", "NUM_USERDEF2"};
                siM.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_GIR", "DT_GIR", "NO_EMP", "DC_RMK", "DC_RMK1", "DC_RMK2", "ID_UPDATE",
                                                    "TXT_USERDEF1", "TXT_USERDEF2", "TXT_USERDEF3", "TXT_USERDEF4", "CD_USERDEF1", "CD_USERDEF2", "CD_USERDEF3", "CD_USERDEF4", "NUM_USERDEF1", "NUM_USERDEF2"};
                sic.Add(siM);
            }

            if (dtL != null)
            {
                SpInfo siD = new SpInfo();
                siD.DataValue = dtL;
                siD.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siD.UserID = Global.MainFrame.LoginInfo.UserID;//USERID로 변경
                siD.SpNameInsert = "UP_SA_GIR_INSERT1";
                siD.SpNameUpdate = "UP_SA_GIR_UPDATE1";
                siD.SpNameDelete = "UP_SA_GIR_DELETE1";
                siD.SpParamsInsert = new string[] { "CD_COMPANY",   "NO_GIR",       "SEQ_GIR",      "CD_ITEM",      "TP_ITEM", 
                                                    "DT_DUEDATE",   "DT_REQGI",     "YN_INSPECT",   "CD_SL",        "TP_GI",
                                                    "QT_GIR",       "CD_EXCH",      "UM",           "AM_GIR",       "AM_GIRAMT",
                                                    "AM_VAT",       "UNIT",         "QT_GIR_IM",    "GI_PARTNER",   "NO_PROJECT",
                                                    "RT_EXCH",      "RT_VAT",       "NO_SO",        "SEQ_SO",       "CD_SALEGRP",
                                                    "TP_VAT",       "NO_EMP",       "TP_IV",        "FG_TAXP",      "TP_BUSI",
                                                    "NO_LC",        "SEQ_LC",       "FG_LC_OPEN",   "DC_RMK",       "GIR",
                                                    "IV",           "ID_INSERT",    "RET",          "CD_WH",        "NO_PMS",
                                                    "SEQ_PROJECT",  "YN_PICKING",   "L_CD_USERDEF1","NO_INV",       "TP_UM_TAX",
                                                    "UMVAT_GIR" };
                siD.SpParamsUpdate = new string[] { "CD_COMPANY",   "NO_GIR",       "SEQ_GIR",      "YN_INSPECT",   "CD_SL", 
                                                    "QT_GIR",       "UM",           "AM_GIR",       "AM_GIRAMT",    "AM_VAT",
                                                    "QT_GIR_IM",    "GI_PARTNER",   "ID_UPDATE",    "RT_EXCH",      "CD_WH",
                                                    "DT_DUEDATE",   "DT_REQGI",     "DC_RMK",       "YN_PICKING",   "L_CD_USERDEF1",
                                                    "NO_INV",       "UMVAT_GIR"};
                siD.SpParamsDelete = new string[] { "CD_COMPANY",   "NO_GIR",       "SEQ_GIR" };
                siD.SpParamsValues.Add(ActionState.Insert, "RET", "N");  //반품구분
                sic.Add(siD);
            }

            if (Global.MainFrame.ServerKeyCommon == "ANJUN" || Global.MainFrame.ServerKeyCommon == "DZSQL")
            {
                if (dtL != null)
                {
                    SpInfo siD = new SpInfo();
                    siD.DataValue = dtL;
                    siD.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    siD.UserID = Global.MainFrame.LoginInfo.UserID;//USERID로 변경
                    siD.SpNameDelete = "UP_SA_Z_ANJUN_GIR_D";
                    siD.SpParamsDelete = new string[] { "CD_COMPANY", "NO_GIR", "SEQ_GIR" };
                    sic.Add(siD);
                }
            }

            //저장 내역이 하나도 없을때 Exception 처리를 한다.
            if (sic.List == null) return false;

            return DBHelper.Save(sic);
        }
        #endregion

        #region ♣ 삭제
        public bool Delete(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("UP_SA_GIR_DELETE", obj);
        }

        public bool Delete안전공업(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("UP_SA_Z_ANJUN_GIR_DELETE", obj);
        }

        #endregion

        #region ♣ 여신체크
        public bool CheckCredit(string cd_Partner, decimal am_sum)
        {
            object[] obj = new object[4];
            obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
            obj[1] = cd_Partner;
            obj[2] = am_sum;
            obj[3] = "002"; //수주확정

            object[] outs = null;
            DataTable dt = DBHelper.GetDataTable("UP_SA_CHECKCREDIT_SELECT", obj, out outs);

            if (outs == null || outs.Length != 3)
                return false;

            if (outs[0] != null && outs[0].ToString() != "")
            {
                if (outs[2].ToString() == "002")
                {
                    if (Global.MainFrame.ShowMessage("여신금액을 초과하였습니다. 저장하시겠습니까 ?\n(여신총액 : " + outs[0].ToString() + ", 잔액 : " + outs[1].ToString() + ")", "QY2") == DialogResult.Yes)
                        return true;
                    else
                        return false;
                }
                else if (outs[2].ToString() == "003")
                {
                    Global.MainFrame.ShowMessage("여신금액을 초과하여 저장할 수 없습니다. \n(여신총액 : " + outs[0].ToString() + " 잔액 : " + outs[1].ToString() + ")");
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region ♣ 의뢰수량체크
        public bool Check(object[] obj, out decimal 분할의뢰수량)
        {
            분할의뢰수량 = 0;
            object[] outs = null;
            DataTable dt = DBHelper.GetDataTable("UP_SA_GIR_S", obj, out outs);

            분할의뢰수량 = D.GetDecimal(outs[1]);

            bool isreturn = false;
            if (D.GetDecimal(outs[0]) == 1)
                isreturn = true;

            return isreturn;
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

            string so_Price = Global.MainFrame.ExecuteScalar(SelectQuery).ToString();

            return so_Price;
        }
        #endregion

        #region ♣ 영업환경설정 가져오기
        public DataTable search_EnvMng()
        {
            object[] obk = new object[1];
            obk[0] = Global.MainFrame.LoginInfo.CompanyCode;

            //수주수량 초과허용
            return DBHelper.GetDataTable("UP_SA_ENV_SELECT", obk);
        }
        #endregion

        #region ♣ 속성
        internal string GetATP사용여부
        {
            get
            {
                return ATP사용여부;
            }
        }
        #endregion

        #region -> SearchPartnerInfo

        public DataTable SearchPartnerInfo(string str거래처)
        {
            DataTable dt = DBHelper.GetDataTable(@"
                                SELECT DC_ADS1_H, DC_ADS1_D, NO_TEL1, NO_FAX1
                                FROM MA_PARTNER
                                WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
                                AND CD_PARTNER = '" + str거래처 + @"'
                                ");
            return dt;
        }

        #endregion


        #region -> 자사정보가져오기(2011-10-18)

        public DataTable SearchMyInfo()
        {
            DataTable dt = DBHelper.GetDataTable(@"
                                    SELECT    BI.NO_BIZAREA AS MY_NO_BIZAREA
                                            , C.NM_COMPANY  AS MY_NM_COMPANY
                                            , C.NM_CEO      AS MY_NM_CEO
                                            , C.NO_TEL      AS MY_NO_TEL
                                            , C.NO_FAX      AS  MY_NO_FAX
		                                    , (C.ADS_H + C.ADS_D) AS MY_ADS
                                            , BI.TP_JOB     AS  MY_TP_JOB
                                            , BI.CLS_JOB    AS  MY_CLS_JOB
                                    FROM MA_COMPANY C, MA_BIZAREA BI
                                    WHERE C.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
                                    AND BI.CD_COMPANY  = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
                                    AND BI.CD_BIZAREA  = '" + Global.MainFrame.LoginInfo.BizAreaCode + @"'");

            return dt;
        }

        #endregion

        #region -> 의뢰기준 송장등록이 되어 있는지 체크

        internal bool IsInvReq(string noGir, decimal seqGir)
        {
            string sql = " SELECT NO_INV"
                       + " FROM   TR_INVL_REQ"
                       + " WHERE  NO_GIR      = '" + noGir + "'"
                       + " AND    NO_LINE_GIR = " + seqGir + ""
                       + " AND    CD_COMPANY  = '" + MA.Login.회사코드 + "'";

            DataTable dt = DBHelper.GetDataTable(sql);

            if (dt == null || dt.Rows.Count == 0)
                return false;

            return true;
        }

        #endregion

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
    }
}

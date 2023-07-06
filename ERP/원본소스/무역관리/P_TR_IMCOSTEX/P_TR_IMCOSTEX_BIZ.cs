using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace trade
{
    class P_TR_IMCOSTEX_BIZ
    {
        #region ♣조회
        public DataSet Search(string 부대비용번호)
        {
            ResultData rtn = (ResultData)Global.MainFrame.FillDataSet("UP_TR_IMCOSTEX_SELECT", new object[] { 부대비용번호, Global.MainFrame.LoginInfo.CompanyCode });
            DataSet ds = (DataSet)rtn.DataValue;

            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn Col in dt.Columns)
                {
                    if (Col.DataType == Type.GetType("System.Decimal"))
                        Col.DefaultValue = 0;
                }
            }
            ds.Tables[0].Columns["YN_JEONJA"].DefaultValue = "N";
            ds.Tables[0].Columns["CD_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaCode;
            return (DataSet)rtn.DataValue;
        }
        #endregion
 

        #region ♣저장
        public bool Save(DataTable dtH, DataTable dtL)
        {
            SpInfoCollection sic = new SpInfoCollection();

            if (dtH != null)
            {
                SpInfo siM = new SpInfo();
                siM.DataValue = dtH;
                
                siM.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siM.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siM.SpNameInsert = "UP_TR_IMCOSTHEX_INSERT";
                siM.SpNameUpdate = "UP_TR_IMCOSTHEX_INSERT";
                siM.SpParamsInsert = new String[] { "NO_COST", "CD_COMPANY", "FG_STEP", "NO_LC", "NO_HST", "NO_BL", "NO_TO", "CD_DEPT", "NO_EMP", "CD_BIZAREA", "DT_BALLOT", "CD_PARTNER", "CD_CC","TY_COST", "YN_JEONJA" };
                siM.SpParamsUpdate = new String[] { "NO_COST", "CD_COMPANY", "FG_STEP", "NO_LC", "NO_HST", "NO_BL", "NO_TO", "CD_DEPT", "NO_EMP", "CD_BIZAREA", "DT_BALLOT", "CD_PARTNER", "CD_CC", "TY_COST", "YN_JEONJA"};

                sic.Add(siM);
            }

            if (dtL != null)
            {
                SpInfo siD = new SpInfo();
                siD.DataValue = dtL;
                siD.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siD.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siD.SpNameInsert = "UP_TR_IMCOSTLEX_INSERT";
                siD.SpNameUpdate = "UP_TR_IMCOSTLEX_INSERT";
                siD.SpNameDelete = "UP_TR_IMCOSTLEX_DELETE";
                siD.SpParamsInsert = new String[] { "NO_COST", "CD_COMPANY", "NO_LINE", "CD_COST", "AM_COST", "FG_TAX", "VAT", "RT_VAT", "CD_EXCH", "AM_EX", "CD_PARTNER", "AM_DISTRIBUT", "AM_TAXSTD", "DT_PAY", "COND_PAY","DT_DUE", "CD_POP","CD_PJT", "SEQ_PROJECT", "DC_RMK"};
                siD.SpParamsUpdate = new String[] { "NO_COST", "CD_COMPANY", "NO_LINE", "CD_COST", "AM_COST", "FG_TAX", "VAT", "RT_VAT", "CD_EXCH", "AM_EX", "CD_PARTNER", "AM_DISTRIBUT", "AM_TAXSTD", "DT_PAY", "COND_PAY", "DT_DUE", "CD_POP", "CD_PJT", "SEQ_PROJECT", "DC_RMK" };
                siD.SpParamsDelete = new String[] { "CD_COMPANY", "NO_COST", "NO_LINE" };
                sic.Add(siD);
            }

            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sic);
            for (int i = 0; i < rtn.Length; i++)
                if (!rtn[i].Result) return false;

            return true;


        }

      //  public bool Imd_Insert(string TO번호)
        //{
        //        ResultData rt = (ResultData)Global.MainFrame.ExecSp("UP_TR_TO_IMD_INSERT", new object[] { TO번호, Global.MainFrame.LoginInfo.CompanyCode });
        //        return rt.Result;
        //}

        public void Delete(string 부대비용번호)
        {
            Global.MainFrame.ExecSp("UP_TR_IMCOSTHEX_DELETE", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 부대비용번호 });
        }

        #endregion


        #region ♣전표처리
        public bool 미결전표처리(string 부대비용번호, string Process)
        {
            ResultData result = (ResultData)Global.MainFrame.ExecSp("UP_TR_IMCOSTEX_DOCU", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 부대비용번호, Process });
            return result.Result;
        }
       

        public bool 미결전표취소(string 부대비용번호)
        {
            ResultData result = (ResultData)Global.MainFrame.ExecSp("UP_FI_DOCU_AUTODEL", new object[] { Global.MainFrame.LoginInfo.CompanyCode, "003", 부대비용번호, Global.MainFrame.LoginInfo.EmployeeNo });
            return result.Result;
        }

        #endregion


        #region ♣기타 조회
        public DataSet Search_Chk_Excel()
        {
               string SelectQuery = " SELECT CD_SYSDEF " +
                                     "   FROM MA_CODEDTL  " +
                                     "  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                     "    AND CD_FIELD  = 'TR_IM00007'" +

                                     " SELECT CD_SYSDEF, CD_FLAG1 " +
                                     "   FROM MA_CODEDTL  " +
                                     "  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                     "    AND CD_FIELD  = 'MA_B000046'" +

                                     " SELECT CD_SYSDEF" +
                                     "   FROM MA_CODEDTL" +
                                     "  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                     "    AND CD_FIELD  = 'PU_C000044'" +

                                     " SELECT CD_PARTNER, LN_PARTNER " +
                                     "   FROM MA_PARTNER  " +
                                     "  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
               
                DataSet ds = DBHelper.GetDataSet(SelectQuery);

                return ds;
        }

        public DataRow Get_BL_CC(string cdBl)
        {
            DataRow dr_return = null;
            int NO_LINE = 1;
            string sQuery = @"SELECT MIN(IML.NO_LINE) AS NO_LINE
                                FROM TR_BL_IML IML
                               WHERE NO_BL = '{0}' 
                                 AND CD_COMPANY = '{1}'";
            sQuery = string.Format(sQuery, cdBl, Global.MainFrame.LoginInfo.CompanyCode);
            DataTable dt = Global.MainFrame.FillDataTable(sQuery);

            if (dt != null && dt.Rows.Count > 0)
                NO_LINE = D.GetInt(dt.Rows[0]["NO_LINE"]);

            sQuery = @"SELECT IML.CD_CC,D.CD_CC CD_CC,
                                   CASE WHEN IML.CD_CC IS NULL THEN CC_GRP.NM_CC ELSE CC.NM_CC END AS NM_CC
                              FROM TR_BL_IML IML
                                   INNER JOIN TR_BL_IMH IMH ON IMH.CD_COMPANY = IML.CD_COMPANY AND IMH.NO_BL = IML.NO_BL                       
                                   LEFT OUTER JOIN MA_CC CC ON CC.CD_COMPANY = IML.CD_COMPANY AND CC.CD_CC = IML.CD_CC
                                   LEFT OUTER JOIN MA_PURGRP D ON D.CD_COMPANY = IMH.CD_COMPANY AND D.CD_PURGRP = IMH.CD_PURGRP  
                                   LEFT OUTER JOIN MA_CC CC_GRP ON CC_GRP.CD_COMPANY = D.CD_COMPANY AND CC_GRP.CD_CC = D.CD_CC
                             WHERE IML.NO_BL = '{0}'
                               AND IML.NO_LINE = '{1}'
                               AND IML.CD_COMPANY ='{2}' ";

            sQuery = string.Format(sQuery, cdBl, NO_LINE,Global.MainFrame.LoginInfo.CompanyCode);
;
            dt = Global.MainFrame.FillDataTable(sQuery);
            T.SetDefaultValue(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                dr_return = dt.Rows[0];
            }

            return dr_return;

        }
        //public string GetCC(string p_cd_dept)
        //{

        //    DataTable dt = Global.MainFrame.FillDataTable("SELECT CD_CC FROM MA_DEPT WHERE CD_COMPANY = '" + 
        //                                                  Global.MainFrame.LoginInfo.CompanyCode +
        //                                                  "' AND CD_DEPT = '" + p_cd_dept + "' ");


        //    return dt.Rows[0]["CD_CC"].ToString();
        //}

        //public decimal Get_정산금액(string BL번호)
        //{
        //    DataTable dt2 = Global.MainFrame.FillDataTable("SELECT AM_DISTRIBU   FROM TR_TO_IML WHERE CD_COMPANY = '" + 
        //                                                  Global.MainFrame.LoginInfo.CompanyCode +
        //                                                  "' AND NO_BL = '" + BL번호 + "' "); 
        //    return D.GetDecimal( dt2.Rows[0]["AM_DISTRIBU"]) ;
        //} 

        public DataTable Get_BL_PJT(string cdBl)
        {
            string sQuery = @"SELECT IML.CD_PJT, PJH.NM_PROJECT NM_PJT, IML.SEQ_PROJECT, PJL.CD_ITEM CD_UNIT, ITM.NM_ITEM NM_UNIT, ITM.STND_ITEM STND_UNIT
                             FROM	 TR_BL_IML	   IML
                             LEFT JOIN SA_PROJECTH PJH ON IML.CD_COMPANY = PJH.CD_COMPANY AND IML.CD_PJT = PJH.NO_PROJECT
                             LEFT JOIN SA_PROJECTL PJL ON IML.CD_COMPANY = PJL.CD_COMPANY AND IML.CD_PJT = PJL.NO_PROJECT AND IML.SEQ_PROJECT = PJL.SEQ_PROJECT    	  
                             LEFT JOIN MA_PITEM    ITM ON PJL.CD_COMPANY = ITM.CD_COMPANY AND PJL.CD_PLANT = ITM.CD_PLANT AND PJL.CD_ITEM = ITM.CD_ITEM      
                             WHERE IML.NO_BL	  = '" + cdBl + @"' 
                             AND   IML.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' ";

            return Global.MainFrame.FillDataTable(sQuery);
        }

        public DataTable Get_DT_PAY_PREARRANGED(string cd_partner)
        {
            string sQuery = @"SELECT  M.DT_PAY_PREARRANGED
                             FROM	 MA_PARTNER	   M
                             WHERE M.CD_PARTNER	  = '" + cd_partner + @"' 
                             AND   M.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' ";

            return Global.MainFrame.FillDataTable(sQuery);
        }

        #endregion 
 
        
    }
}

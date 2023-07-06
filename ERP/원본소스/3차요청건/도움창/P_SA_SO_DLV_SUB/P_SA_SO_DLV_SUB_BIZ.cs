using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Duzon.Common.Util;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace sale
{
    class P_SA_SO_DLV_SUB_BIZ
    {
        #region 거래처 배송정보를 검색하기 위함
        public DataTable GetPartnerSearch(object[] obj)
        {
            string SelectQuery = string.Empty;

            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
            {
                SelectQuery = " SELECT  DISTINCT SD.NM_CUST_DLV, SD.CD_ZIP, SD.ADDR1, SD.ADDR2, SD.NO_TEL_D1, SD.NO_TEL_D2, SD.TP_DLV, SH.DT_SO " +
                                 "   FROM  SA_SOL_DLV SD " +
                                 "   JOIN  SA_SOH	  SH ON SD.CD_COMPANY = SH.CD_COMPANY AND SD.NO_SO = SH.NO_SO" +
                                 "  WHERE  SD.CD_COMPANY = '" + obj[0].ToString() + "' " +
                                 "    AND  SH.CD_PARTNER = '" + obj[1].ToString() + "' " +
                                 "    AND (SD.NM_CUST_DLV = '" + obj[2].ToString() + "' OR '" + obj[2].ToString() + "' = '' OR '" + obj[2].ToString() + "' IS NULL OR SD.NM_CUST_DLV LIKE '%'+ '" + obj[2].ToString() + "'+'%') " +
                                 "    AND (SD.CD_ZIP = '" + obj[3].ToString() + "' OR '" + obj[3].ToString() + "' = '' OR '" + obj[3].ToString() + "' IS NULL OR SD.CD_ZIP LIKE '%'+ '" + obj[3].ToString() + "'+'%') " +
                                 "    AND (SD.ADDR1 = '" + obj[4].ToString() + "' OR '" + obj[4].ToString() + "' = '' OR '" + obj[4].ToString() + "' IS NULL OR SD.ADDR1 LIKE '%'+ '" + obj[4].ToString() + "'+'%') " +
                                 "    AND (SD.ADDR2 = '" + obj[5].ToString() + "' OR '" + obj[5].ToString() + "' = '' OR '" + obj[5].ToString() + "' IS NULL OR SD.ADDR2 LIKE '%'+ '" + obj[5].ToString() + "'+'%') " +
                                 "    AND (SD.NO_TEL_D1 = '" + obj[6].ToString() + "' OR '" + obj[6].ToString() + "' = '' OR '" + obj[6].ToString() + "' IS NULL OR SD.NO_TEL_D1 LIKE '%'+ '" + obj[6].ToString() + "'+'%') " +
                                 "    AND (SD.NO_TEL_D2 = '" + obj[7].ToString() + "' OR '" + obj[7].ToString() + "' = '' OR '" + obj[7].ToString() + "' IS NULL OR SD.NO_TEL_D2 LIKE '%'+ '" + obj[7].ToString() + "'+'%') " +
                                 "    AND  ISNULL(SD.NM_CUST_DLV, '') != '' " +
                                 "  ORDER  BY SH.DT_SO DESC ";
            }
            else if (Global.MainFrame.DatabaseType == EnumDbType.ORACLE)
            {
                //ORDER BY 시 오류로 인하여 SH.DT_SO 추가
                SelectQuery = " SELECT  DISTINCT SH.DT_SO, SD.NM_CUST_DLV, SD.CD_ZIP, SD.ADDR1, SD.ADDR2, SD.NO_TEL_D1, SD.NO_TEL_D2, SD.TP_DLV, SH.DT_SO " +
                                 "   FROM  SA_SOL_DLV SD " +
                                 "   JOIN  SA_SOH	  SH ON SD.CD_COMPANY = SH.CD_COMPANY AND SD.NO_SO = SH.NO_SO" +
                                 "  WHERE  SD.CD_COMPANY = '" + obj[0].ToString() + "' " +
                                 "    AND  SH.CD_PARTNER = '" + obj[1].ToString() + "' " +
                                 "    AND (SD.NM_CUST_DLV = '" + obj[2].ToString() + "' OR '" + obj[2].ToString() + "' = '' OR '" + obj[2].ToString() + "' IS NULL OR SD.NM_CUST_DLV LIKE '%'+ '" + obj[2].ToString() + "'+'%') " +
                                 "    AND (SD.CD_ZIP = '" + obj[3].ToString() + "' OR '" + obj[3].ToString() + "' = '' OR '" + obj[3].ToString() + "' IS NULL OR SD.CD_ZIP LIKE '%'+ '" + obj[3].ToString() + "'+'%') " +
                                 "    AND (SD.ADDR1 = '" + obj[4].ToString() + "' OR '" + obj[4].ToString() + "' = '' OR '" + obj[4].ToString() + "' IS NULL OR SD.ADDR1 LIKE '%'+ '" + obj[4].ToString() + "'+'%') " +
                                 "    AND (SD.ADDR2 = '" + obj[5].ToString() + "' OR '" + obj[5].ToString() + "' = '' OR '" + obj[5].ToString() + "' IS NULL OR SD.ADDR2 LIKE '%'+ '" + obj[5].ToString() + "'+'%') " +
                                 "    AND (SD.NO_TEL_D1 = '" + obj[6].ToString() + "' OR '" + obj[6].ToString() + "' = '' OR '" + obj[6].ToString() + "' IS NULL OR SD.NO_TEL_D1 LIKE '%'+ '" + obj[6].ToString() + "'+'%') " +
                                 "    AND (SD.NO_TEL_D2 = '" + obj[7].ToString() + "' OR '" + obj[7].ToString() + "' = '' OR '" + obj[7].ToString() + "' IS NULL OR SD.NO_TEL_D2 LIKE '%'+ '" + obj[7].ToString() + "'+'%') " +
                                 "    AND  NVL(SD.NM_CUST_DLV, '') != '' " +
                                 "  ORDER  BY SH.DT_SO DESC ";
            }

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            return dt;
        }
        #endregion

        public DataTable 조회(string NO_SO)
        {
            return DBHelper.GetDataTable("UP_SA_SOL_DLV_S", new object[] {MA.Login.회사코드, NO_SO });
        }

        #region Save
        public bool Save(DataTable dt)
        {
            SpInfoCollection sic = new SpInfoCollection();

            if (dt != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = dt;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameUpdate = "UP_SA_SOL_DLV_U1";
                si.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_SO", "SEQ_SO", "NM_CUST_DLV", "CD_ZIP", "ADDR1", "ADDR2", "NO_TEL_D1", "NO_TEL_D2", "TP_DLV", "DC_REQ", "TP_DLV_DUE", "NO_ORDER", "NM_CUST", "NO_TEL1", "NO_TEL2", "DLV_TXT_USERDEF1", "DLV_CD_USERDEF1" };
                sic.Add(si);
            }

            return DBHelper.Save(sic);
        } 
        #endregion
    }
}

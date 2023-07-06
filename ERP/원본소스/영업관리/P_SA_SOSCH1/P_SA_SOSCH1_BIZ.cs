using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace sale
{
    class P_SA_SOSCH1_BIZ
    {
        #region -> 출력
        public DataTable Print_type1(object[] obj, string prt_name)
        {
            string sp_name = String.Empty; ;
            if (prt_name.ToUpper().Contains("R_SA_SOSCH_001"))
                sp_name = "UP_SA_SOSCH1_SELECT_PRINT_SO";
            else if (prt_name.ToUpper().Contains("R_SA_SOSCH_002"))
                sp_name = "UP_SA_SOSCH1_SELECT_PRINT_PI";
            else if (prt_name.ToUpper().Contains("R_SA_SOSCH_100"))
                sp_name = "UP_SA_SOSCH1_S_PNT_ORDER";
            else if (prt_name.ToUpper().Contains("R_SA_SOSCH_200"))
                sp_name = "UP_SA_SOSCH1_S_PNT_ORDETAIL";
            else
                sp_name = "UP_SA_SOSCH1_SELECT_PRINT";

            DataTable dt = DBHelper.GetDataTable(sp_name, obj);

            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "KORAVL" || Global.MainFrame.ServerKeyCommon.ToUpper() == "SQL_") //2012.05.25 품목이 ZZ9999인것 XML에서 제외
                dt = new DataView(dt, "CD_ITEM <> 'ZZ9999'", "", DataViewRowState.CurrentRows).ToTable();

            return dt;
        }
        #endregion

        #region -> 조회
        public DataTable Search(object[] obj) // 헤더부분
        {
            string sp_name = "UP_SA_SOSCH1_SELECT";
            DataTable dt = DBHelper.GetDataTable(sp_name, obj);
            return dt;
        }

        public DataTable SearchDetail(object[] obj) // 라인부분
        {
            DataTable dt = null;
            string sp_name = "UP_SA_SOSCH1_SELECT1";

            if (MA.ServerKey(false, new string[] { "KOREAF" })) // 제원인터내쇼날날
                dt = DBHelper.GetDataTable(sp_name, obj, "NO_SO, SEQ_SO");
            else
                dt = DBHelper.GetDataTable(sp_name, obj);

            return dt;
        }
        #endregion

        #region -> 제원인터내쇼날 전용 출력
        public DataTable Print_KOREAF(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_Z_KOREAF_SOSCH1_P", obj);
            return dt;
        }
        #endregion

        #region -> 신진에스엠 전용 출력 (컬럼조회/이력저장)

        public string Z_SINJINSM_PRINT_Search(string NO_SO)
        {
            DataTable dt = DBHelper.GetDataTable(@"
                SELECT  MAX(C.NM_CUST_PARTNER) AS NM_CUST_PARTNER
                FROM    SA_SOL A 
                LEFT JOIN CZ_SINJINSM_PM_ORDERSUB B ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_RELATION = B.PONO AND A.SEQ_RELATION = B.POSUBNO
                LEFT JOIN CZ_SINJINSM_BR_CUST C ON B.BRANCH = C.CD_PARTNER AND B.ENDUSER = C.CD_CUST_PARTNER
                WHERE	A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"' 
                AND		A.NO_SO = '" + NO_SO + "' ");

            if (dt.Rows.Count > 0)
                return D.GetString(dt.Rows[0]["NM_CUST_PARTNER"]);
            else
                return string.Empty;
        }

        public bool Z_SINJINSM_PRINT_Save(DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.DataState = DataValueState.Modified;

            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;

            si.SpNameUpdate = "UP_SA_Z_SINJINSM_SOL_PRINT_U";

            si.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_SO", "SEQ_SO", "CD_USERDEF4", "TXT_USERDEF6", "NUM_USERDEF10" };

            //저장 내역이 하나도 없을때 Exception 처리를 한다.
            if (si == null) return false;

            return DBHelper.Save(si);
        }

        #endregion
    }
}

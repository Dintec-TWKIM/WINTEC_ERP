using System;
using System.Data;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace sale
{
    public class P_SA_ESTIMATE_BIZ
    {
        FlexGrid _flex = new FlexGrid();

        #region ♣ 조회
        /// <summary>
        /// 헤더, 라인, 라인디테일 조회
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataSet Search(object[] obj)
        {

            //Data Set
            DataSet ds = DBHelper.GetDataSet("UP_SA_EST_HST_SEARCH", obj);

            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn Col in ds.Tables[1].Columns)
                {
                    if (Col.DataType == Type.GetType("System.Decimal"))
                        Col.DefaultValue = 0;
                }
            }

            return ds;
        }
        #endregion

        #region ♣ 저장
        /// <summary>
        /// insert, update, delete 처리 함수
        /// </summary>
        /// <param name="dtH">헤더 테이블</param>
        /// <param name="dtL">라인 테이블</param>
        /// <param name="dtD">라인 디테일 테이블</param>
        /// <returns></returns>
        public bool Save(DataTable dtH, DataTable dtL, DataTable dtD)
        {

            SpInfoCollection sic = new SpInfoCollection();

            if (dtH != null)
            {
                SpInfo siH = new SpInfo();
                siH.DataValue = dtH;
                siH.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siH.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siH.SpNameInsert = "UP_SA_ESTIMATE_HEADER_INSERT";
                siH.SpNameUpdate = "UP_SA_ESTIMATE_HEADER_UPDATE";
                siH.SpNameDelete = "UP_SA_ESTIMATE_HEADER_DELETE";
                siH.SpParamsInsert = new string[] { "CD_COMPANY", "CD_BIZAREA", "NO_EST", "NO_EST_NM", "NO_HST", "CD_SALEGRP", "CD_PARTNER", "DT_EST", "NO_EMP", "TP_VAT", "FG_VAT", "CD_EXCH", "RT_EXCH", "FG_BILL", "DC_RMK", "DT_VALID", "STA_EST", "DT_CONT" };
                siH.SpParamsUpdate = new string[] { "CD_COMPANY", "CD_BIZAREA", "NO_EST", "NO_EST_NM", "NO_HST", "CD_SALEGRP", "CD_PARTNER", "DT_EST", "NO_EMP", "TP_VAT", "FG_VAT", "CD_EXCH", "RT_EXCH", "FG_BILL", "DC_RMK", "DT_VALID", "STA_EST", "DT_CONT" };
                siH.SpParamsDelete = new string[] { "CD_COMPANY", "NO_EST", "NO_HST" };

                sic.Add(siH);

            }

            if (dtL != null)
            {
                SpInfo siL = new SpInfo();
                siL.DataValue = dtL;
                siL.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siL.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siL.SpNameInsert = "UP_SA_ESTIMATE_LINE_INSERT";
                siL.SpNameUpdate = "UP_SA_ESTIMATE_LINE_UPDATE";
                siL.SpNameDelete = "UP_SA_ESTIMATE_LINE_DELETE";
                siL.SpParamsInsert = new string[] { "CD_COMPANY", "NO_EST", "NO_HST", "NO_LINE", "CD_PLANT", "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT", "QT", "RT_CUT", "UM", "AM_STD", "AM_EST", "AM_KEST", "AM_VAT", "DC_RMK", "AM_STD_PO"};
                siL.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_EST", "NO_HST", "NO_LINE", "CD_PLANT", "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT", "QT", "RT_CUT", "UM", "AM_STD", "AM_EST", "AM_KEST", "AM_VAT", "DC_RMK", "AM_STD_PO" };
                siL.SpParamsDelete = new string[] { "CD_COMPANY", "NO_EST", "NO_HST", "NO_LINE" };

                sic.Add(siL);
            }

            if (dtD != null)
            {
                SpInfo siD = new SpInfo();
                siD.DataValue = dtD;
                siD.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siD.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siD.SpNameInsert = "UP_SA_ESTIMATE_LINE_DTL_INSERT";
                siD.SpNameUpdate = "UP_SA_ESTIMATE_LINE_DTL_UPDATE";
                siD.SpNameDelete = "UP_SA_ESTIMATE_LINE_DTL_DELETE";
                siD.SpParamsInsert = new string[] { "CD_COMPANY", "NO_EST", "NO_HST", "NO_LINE", "SEQ", "CD_ITEM", "NM_ITEM", "QT", "UM", "DC_RMK" };
                siD.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_EST", "NO_HST", "NO_LINE", "SEQ", "CD_ITEM", "NM_ITEM", "QT", "UM", "DC_RMK" };
                siD.SpParamsDelete = new string[] { "CD_COMPANY", "NO_EST", "NO_HST", "NO_LINE", "SEQ" };

                sic.Add(siD);
            }


            if (sic.List.Count == 0)
            {
                return false;
            }

            return DBHelper.Save(sic);
        }
        #endregion

        #region 단가 구하기
        /// <summary>
        /// 단가 구하기
        /// </summary>
        /// <param name="CD_ITEM">품목</param>
        /// <param name="CD_PARTNER">거래처</param>
        /// <param name="FG_UM">단가유형</param>
        /// <param name="CD_EXCH">환종</param>
        /// <param name="TP_SALEPRICE">단가적용형태</param>
        /// <param name="DT_SO">적용일자</param>
        /// <returns>품목 단가</returns>
        public decimal UM_ITEM(string CD_ITEM, string CD_PARTNER, string FG_UM, string CD_EXCH, string TP_SALEPRICE, string DT_SO)
        {

            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_SO_SELECT1";
            si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, CD_ITEM, CD_PARTNER, FG_UM, CD_EXCH, TP_SALEPRICE, DT_SO };
            ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);
            return _flex.CDecimal(rtn.OutParamsSelect[0, 0].ToString());
        }
        #endregion

        #region ♣ 환율정보조회
        /// <summary>
        /// 환율정보 구하기
        /// </summary>
        /// <param name="CD_COMPANY">기업코드</param>
        /// <param name="CURR_SOUR">환율코드</param>
        /// <param name="YYMMDD">적용일자</param>
        /// <returns></returns>
        public decimal ExchangeSearch(string CD_COMPANY, string CURR_SOUR, string YYMMDD)
        {
            decimal rt_exch = 1;
            string SelectQuery = "SELECT RATE_BASE " +
                                 "  FROM MA_EXCHANGE " +
                                 " WHERE CD_COMPANY = '" + CD_COMPANY + "'" +
                                 "   AND CURR_SOUR = '" + CURR_SOUR + "' " +
                                 "   AND CURR_DEST = '000' " +
                                 "   AND YYMMDD = '" + YYMMDD + "' ";

            DataTable dt = DBHelper.GetDataTable(SelectQuery);

            if (dt.Rows.Count > 0)
            {
                if (D.GetDecimal(dt.Rows[0]["RATE_BASE"]) != 0)
                    rt_exch = D.GetDecimal(dt.Rows[0]["RATE_BASE"]);
            }

            return rt_exch;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;
using Duzon.ERPU;

namespace trade
{
    class P_TR_TO_IN_BIZ
    {
        #region -> 저장
        internal bool Save(DataTable m_dtTOHead, DataTable m_dtTODetail, string 통관번호)//, string 신고일, string 검사일, string 검역일)
        {
            // 등록 화면에서 저장버튼 누르면 Header 와 Line까지 동시에 저장하는 로직 구현 2007.04.16
            SpInfoCollection sic = new SpInfoCollection();

            if (m_dtTOHead != null)
            {
                SpInfo si = new SpInfo();
                si.DataValue = m_dtTOHead; 					//저장할 데이터 테이블
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                
                si.SpNameInsert = "UP_TR_TO_IN_INSERT";			
                si.SpNameUpdate = "UP_TR_TO_IN_UPDATE";			
                si.SpNameDelete = "UP_TR_TO_IN_DELETE";		
       
                si.SpParamsInsert = new string [] { "NO_TO", "CD_COMPANY", "CD_PURGRP", "CD_PARTNER", "NO_SCBL", "CD_BANK", "DT_TO", "FG_LC", "CD_EXCH", 
                                                    "RT_EXCH", "AM_EX", "AM_LICENSE", "AM", "COND_PRICE", "CD_CUSTOMS", "NO_LICENSE", "DT_LICENSE", "NO_INSP", 
                                                    "DT_INSP", "NO_QUAR", "DT_QUAR",  "REMARK", "TOT_WEIGHT", "CD_UNIT", "WEIGHT", "YN_DISTRIBU", "ID_INSERT",  
                                                    "NO_EMP",  "DT_DISTRIBU", "DT_CUSTOMS" };

                si.SpParamsUpdate = new string [] { "NO_TO", "CD_COMPANY", "CD_PURGRP", "CD_PARTNER", "NO_SCBL", "CD_BANK",  "DT_TO", "FG_LC", "CD_EXCH", 
                                                    "RT_EXCH", "AM_EX", "AM_LICENSE", "AM", "COND_PRICE", "CD_CUSTOMS", "NO_LICENSE", "DT_LICENSE", "NO_INSP", 
                                                    "DT_INSP","NO_QUAR", "DT_QUAR", "REMARK", "TOT_WEIGHT", "CD_UNIT", "WEIGHT", "YN_DISTRIBU", "ID_UPDATE",  
                                                    "NO_EMP",  "DT_DISTRIBU" , "DT_CUSTOMS" }; 

                si.SpParamsDelete = new string[] { "NO_TO", "CD_COMPANY" };
                sic.Add(si);
            }
        
            if (m_dtTODetail != null)
            {
                SpInfo sid = new SpInfo();
                sid.DataValue = m_dtTODetail; 					
                //sid.DataState = DataValueState.Added;
                sid.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                sid.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                sid.SpNameInsert = "UP_TR_TO_LINE_INSERT";
                sid.SpNameDelete = "UP_TR_TO_LINE_DELETE";

                sid.SpParamsInsert = new string[] { 	"NO_TO", "NO_LINE", "CD_COMPANY", "NO_BL", "NO_BLLINE", 
                                                        "CD_ITEM", "QT_TO", "CD_UNIT_MM", "QT_TO_MM", "UM_EX_PO", 
                                                        "UM_EX", "AM_EX", "UM", "AM", "CD_PJT", 
                                                        "YN_PURCHASE", "FG_TPPURCHASE", "CD_QTIOTP", "RT_CUSTOMS", "RT_SPEC",
														"YN_AUTORCV", "CD_PLANT", "CD_SL", "DT_DELIVERY","AM_BL","CD_CC",
                                                         "AM_REBATE_EX","AM_REBATE","UM_REBATE","SEQ_PROJECT","NUM_USERDEF1"

                };
                
                sid.SpParamsDelete = new string[] { "NO_TO", "NO_LINE", "CD_COMPANY"};



                //sid.SpParamsValues.Add(ActionState.Insert, "NO_TO", 통관번호);
                //sid.SpParamsValues.Add(ActionState.Insert, "RT_CUSTOMS", 0 );     // 관세율
                //sid.SpParamsValues.Add(ActionState.Insert, "RT_SPEC", 0 );        // 특소세율 (우선 LINE단위 초기값 넣고 내역상에서 작성후 저장하면 될듯 )

                sic.Add(sid);
            }


            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sic);
            for (int i = 0; i < rtn.Length; i++)
                if (!rtn[i].Result) return false;


            return true;
        }
         
        // 프로시저에서 관세배부 직접처리하므로 추후 지워질 예정
        //internal bool Imd_Insert(string 통관번호)
        //{
        //        //string TO번호 = m_dtTOHead.Rows[1]["NO_TO"].ToString();
        //    ResultData rt = (ResultData)Global.MainFrame.ExecSp("UP_TR_TO_IMD_INSERT", new object[] { 통관번호, Global.MainFrame.LoginInfo.CompanyCode });
        //    return rt.Result;
        //}
        #endregion

        #region -> 조회
        //1:1 통관건 조회
        public DataTable SearchLine(string BL번호)        
        {
            Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo(); 
            si.SpNameSelect = "UP_TR_BL_SELECT_SAVE";
            si.SpParamsSelect = new Object[] { Global.MainFrame.LoginInfo.CompanyCode, BL번호 };
            ResultData rd = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)rd.DataValue;

            return dt;
        }

        //통관분할시 BL정보 조회 2011-08-31,신규생성
        public DataTable SearchLine2(string 멀티BL번호)
        {
            Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
            si.SpNameSelect = "UP_TR_BL_SELECT_SAVE2";
            si.SpParamsSelect = new Object[] { Global.MainFrame.LoginInfo.CompanyCode, 멀티BL번호 };
            ResultData rd = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)rd.DataValue;

            return dt;
        }


        public DataTable Search_Line(string 통관번호)
        {
            Object[] obj = new Object[] {  Global.MainFrame.LoginInfo.CompanyCode,Global.MainFrame.LoginInfo.CompanyCode };

            DataTable dt = DBHelper.GetDataTable("UP_TR_TO_LINE_SELECT", obj);

            return dt;
        }
        
        public DataSet Search(string 통관번호)
        {
            //Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
            //si.SpNameSelect = "UP_TR_TO_NO_SELECT_VIEW";
            //si.SpParamsSelect = new Object [] { Global.MainFrame.LoginInfo.CompanyCode, 통관번호 };
            //ResultData result = ( ResultData )Global.MainFrame.FillDataTable( si );
            //DataTable dt = ( DataTable )result.DataValue;

            Object[] obj = new Object[] { Global.MainFrame.LoginInfo.CompanyCode, 통관번호 };

            DataSet dt = DBHelper.GetDataSet("UP_TR_TO_NO_SELECT_VIEW", obj);


            foreach (DataColumn Col in dt.Tables[0].Columns)
            {
                if ( Col.DataType == Type.GetType( "System.Decimal" ) )
                    Col.DefaultValue = 0;
            }

            dt.Tables[0].Columns["CD_COMPANY"].DefaultValue = Global.MainFrame.LoginInfo.CompanyCode;
            dt.Tables[0].Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dt.Tables[0].Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;

            dt.Tables[0].Columns["CD_EXCH"].DefaultValue = "000";
            dt.Tables[0].Columns["RT_EXCH"].DefaultValue = 1;
            dt.Tables[0].Columns["YN_DISTRIBU"].DefaultValue = "N";
            dt.Tables[0].Columns["DT_LICENSE"].DefaultValue = Global.MainFrame.GetStringToday;
            dt.Tables[0].Columns["DT_DISTRIBU"].DefaultValue = "";
            dt.Tables[0].Columns["DT_TO"].DefaultValue = Global.MainFrame.GetStringToday;
            dt.Tables[0].Columns["DT_INSP"].DefaultValue = Global.MainFrame.GetStringToday;
            dt.Tables[0].Columns["DT_QUAR"].DefaultValue = Global.MainFrame.GetStringToday;
            dt.Tables[0].Columns["FG_LC"].DefaultValue = "003";
            dt.Tables[0].Columns["CD_PURGRP"].DefaultValue = Global.MainFrame.LoginInfo.PurchaseGroupCode;
            dt.Tables[0].Columns["NM_PURGRP"].DefaultValue = Global.MainFrame.LoginInfo.PurchaseGroupName;
            dt.Tables[0].Columns["DT_CUSTOMS"].DefaultValue = Global.MainFrame.GetStringToday;

            return dt;

        }

        #endregion

        #region -> 삭제
        internal bool Delete(string 통관번호)
        {
            ResultData result = ( ResultData )Global.MainFrame.ExecSp( "UP_TR_TO_IN_DELETE", new object [] { 통관번호, Global.MainFrame.LoginInfo.CompanyCode} );
            return result.Result;
        }
        #endregion

        ////--삭제예정
        //public bool 라인데이타보정(string TO번호)
        //{
        //    SpInfo si = new SpInfo();
        //    si.SpNameSelect = "UP_TR_TO_MODIFY";
        //    si.SpParamsSelect = new object[] { TO번호, Global.MainFrame.LoginInfo.CompanyCode };
        //    ResultData _rtn = (ResultData)Global.MainFrame.FillDataTable(si);

        //    return true;
        //}

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
    }
}

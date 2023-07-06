using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;

using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;

namespace pur
{
    public class P_PU_IV_REG_BIZ
    {
        private IMainFrame _mf;

        public P_PU_IV_REG_BIZ()
        {
        }

        public P_PU_IV_REG_BIZ(IMainFrame mf)
        {
            _mf = mf;
        }

        public DataTable GetHeadTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CD_BIZAREA", typeof(string));		
            dt.Columns.Add("CD_COMPANY", typeof(string));		
            dt.Columns.Add("NO_BIZAREA", typeof(string));		
            dt.Columns.Add("DT_PROCESS", typeof(string));		
            dt.Columns.Add("TP_SUMTAX", typeof(string));		
            dt.Columns.Add("CD_DEPT", typeof(string));		
            dt.Columns.Add("NO_EMP", typeof(string));	    
            dt.Columns.Add("ID_USER", typeof(string));	    
            dt.Columns.Add("YN_PURSUB", typeof(string));		
            dt.Columns.Add("NM_TP_GI", typeof(string));		
            dt.Columns.Add("MODULE", typeof(string));	
            dt.Columns.Add("FG_FGTAXP", typeof(string));
            dt.Columns.Add("CD_BIZAREA_TAX", typeof(string)); 
            dt.Columns.Add("CD_PC_USER", typeof(string));
            dt.Columns.Add("NM_PC_USER", typeof(string));

            return dt;
        }

        public DataTable GetLineTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("S", typeof(string));
            dt.Columns.Add("CHK", typeof(string));
            dt.Columns.Add("NO_IV", typeof(string));
            dt.Columns.Add("CD_BIZAREA", typeof(string));
            dt.Columns.Add("NM_BIZAREA", typeof(string));
            dt.Columns.Add("CD_BIZAREA_TAX", typeof(string));
            dt.Columns.Add("NM_BIZAREA_TAX", typeof(string));
            dt.Columns.Add("CD_PARTNER", typeof(string)); 
            dt.Columns.Add("LN_PARTNER", typeof(string));
            dt.Columns.Add("NO_BIZAREA", typeof(string));
            dt.Columns.Add("NM_TAX", typeof(string));
            dt.Columns.Add("AM_K", typeof(decimal));
            dt.Columns.Add("VAT_TAX", typeof(decimal));
            dt.Columns.Add("AM_TOTAL", typeof(decimal));
            dt.Columns.Add("FG_TAX", typeof(string));
            dt.Columns.Add("NO_TEMP", typeof(string));
            dt.Columns.Add("FG_TRANS", typeof(string));
            dt.Columns.Add("CD_COMPANY", typeof(string));
           // dt.Columns.Add("CD_BIZAREA", typeof(string));
            dt.Columns.Add("YN_RETURN", typeof(string));
            dt.Columns.Add("AM_KR", typeof(decimal));
            dt.Columns.Add("VAT_TAXR", typeof(decimal));
            dt.Columns.Add("AM_TOTALR", typeof(decimal));
            dt.Columns.Add("CD_CC", typeof(string));
            dt.Columns.Add("CD_EXCH", typeof(string));
            dt.Columns.Add("RT_EXCH", typeof(decimal));
            dt.Columns.Add("AM_EX", typeof(decimal));
            dt.Columns.Add("FG_TPPURCHASE", typeof(string));
            dt.Columns.Add("CD_DOCU", typeof(string));
            dt.Columns.Add("FG_PAYBILL", typeof(string));
            dt.Columns.Add("FG_PAYMENT", typeof(string));
            dt.Columns.Add("DT_PAY_PREARRANGED", typeof(string));
            dt.Columns.Add("NO_IO", typeof(string));
            dt.Columns.Add("NO_IOLINE", typeof(string));
            //dt.Columns.Add("DC_RMK", typeof(string));
            dt.Columns.Add("NO_LC", typeof(string));
            dt.Columns.Add("NUM_USERDEF1", typeof(decimal));
            
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].DataType == Type.GetType("System.Decimal"))
                    dt.Columns[dt.Columns[i].ColumnName].DefaultValue = 0;
            }

            dt.Columns.Add("DC_RMK", typeof(string));	//비고추가 20081226
            dt.Columns.Add("DT_DUE", typeof(string));   //만기일 추가 20110822
            dt.Columns.Add("TP_PAY_DD", typeof(string));      // 지급예정일구분 차월, 당월 20111103
            dt.Columns.Add("DT_PAY_DD", typeof(string));      // 지급예정일 xx일 20111103
            dt.Columns.Add("DT_PAY_DAY", typeof(decimal));
            //if (Config.MA_ENV.PJT형여부 == "Y")
            //{
                dt.Columns.Add("CD_PJT_ITEM", typeof(string));
                dt.Columns.Add("NM_PJT_ITEM", typeof(string));
                dt.Columns.Add("PJT_ITEM_STND", typeof(string));
                dt.Columns.Add("NO_WBS", typeof(string));
                dt.Columns.Add("NO_CBS", typeof(string));
                dt.Columns.Add("CD_ACTIVITY", typeof(string));
                dt.Columns.Add("NM_ACTIVITY", typeof(string));
                dt.Columns.Add("CD_COST", typeof(string));
                dt.Columns.Add("NM_COST", typeof(string));
                dt.Columns.Add("SEQ_PROJECT", typeof(decimal));
                
            //}

            dt.Columns.Add("TP_UM_TAX", typeof(string));
                //dt.Columns.Add("AM_TOTAL", typeof(decimal));
            dt.Columns.Add("YN_JEONJA", typeof(string));
            dt.Columns.Add("PI_PARTNER", typeof(string));
            dt.Columns.Add("PI_LN_PARTNER", typeof(string));
            dt.Columns.Add("GI_PARTNER", typeof(string));
            dt.Columns.Add("GI_LN_PARTNER", typeof(string));
            dt.Columns.Add("CD_PJT", typeof(string));
            dt.Columns.Add("NM_PROJECT", typeof(string));
            dt.Columns.Add("CD_PC_USER", typeof(string));
            dt.Columns.Add("NM_PC_USER", typeof(string));
            dt.Columns.Add("TXT_USERDEF1", typeof(string));
            //if (Global.MainFrame.ServerKeyCommon == "UNIPOINT")
            //{
            //    dt.Columns.Add("CD_PARTNER_PJT", typeof(string));
            //    dt.Columns.Add("LN_PARTNER_PJT", typeof(string));
            //    dt.Columns.Add("NO_EMP_PJT", typeof(string));
            //    dt.Columns.Add("NM_KOR_PJT", typeof(string));
            //    dt.Columns.Add("END_USER", typeof(string));
            //} 

            return dt;
        }

        public object Save(DataTable dt, DataTable dt1, DataTable ds, string CD_CC, string FG_TRANS, string 매입일자)
        {
            SpInfoCollection sic = new SpInfoCollection();

            Duzon.Common.Util.SpInfo si = new Duzon.Common.Util.SpInfo();
            si.DataState = DataValueState.Added;

            dt.RemotingFormat = SerializationFormat.Binary;
       

            si.DataValue = dt; 					//저장할 데이터 테이블
            si.CompanyID = _mf.LoginInfo.CompanyCode;
            si.UserID = _mf.LoginInfo.EmployeeNo;
            si.SpNameInsert = "UP_PU_IVH_INSERT";			//Insert 프로시저명

            /*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/
            si.SpParamsInsert = new string[] { "NO_IV", "CD_COMPANY1", "CD_BIZAREA", "NO_BIZAREA1", "FG_TPPURCHASE", "CD_PARTNER", "FG_TRANS", "AM_K", "VAT_TAX", "RT_EXCH", "CD_EXCH", "AM_EX","DT_PROCESS1", 
											   "FG_TAX", "TP_FD1", "TP_SUMTAX1", "FG_TAXP1", "CD_DEPT1", "NO_EMP1", "YN_PURSUB1", "YN_EXPIV1", "ID_INSERT1", "DC_RMK","CD_DOCU","FG_PAYBILL","DT_PAY_PREARRANGED","CD_BIZAREA_TAX","DT_DUE", "YN_JEONJA",
                                               "FG_PAYMENT","CD_PC_USER","TXT_USERDEF1"
            }; 	
                                                                                                                                                                    		
            /*데이터테이블에는 존재하지 않지 않는 컬럼이지만 모든 데이터로우에 공통적으로 들어가는 값을 정의한다.*/
            si.SpParamsValues.Add(ActionState.Insert, "CD_COMPANY1", ds.Rows[0]["CD_COMPANY"].ToString().Trim());  // 
            //si.SpParamsValues.Add(ActionState.Insert, "CD_BIZAREA1", ds.Rows[0]["CD_BIZAREA"].ToString().Trim()); // 
            si.SpParamsValues.Add(ActionState.Insert, "NO_BIZAREA1", ds.Rows[0]["NO_BIZAREA"].ToString().Trim());
            //si.SpParamsValues.Add(ActionState.Insert, "DT_PROCESS1", ds.Rows[0]["DT_PROCESS"].ToString());  tb_DT_PO.Text.ToString()
            si.SpParamsValues.Add(ActionState.Insert, "DT_PROCESS1", 매입일자);  
            //si.SpParamsValues.Add(ActionState.Insert, "FG_TPPURCHASE1", " ");
            si.SpParamsValues.Add(ActionState.Insert, "TP_FD1", "D");
            si.SpParamsValues.Add(ActionState.Insert, "TP_SUMTAX1", ds.Rows[0]["TP_SUMTAX"].ToString());

            si.SpParamsValues.Add(ActionState.Insert, "FG_TAXP1", ds.Rows[0]["FG_FGTAXP"].ToString());
            si.SpParamsValues.Add(ActionState.Insert, "CD_DEPT1", ds.Rows[0]["CD_DEPT"].ToString().Trim());
            si.SpParamsValues.Add(ActionState.Insert, "NO_EMP1", ds.Rows[0]["NO_EMP"].ToString().Trim());
            si.SpParamsValues.Add(ActionState.Insert, "YN_PURSUB1", ds.Rows[0]["YN_PURSUB"].ToString());
            si.SpParamsValues.Add(ActionState.Insert, "YN_EXPIV1", "N");
            si.SpParamsValues.Add(ActionState.Insert, "ID_INSERT1", this._mf.LoginInfo.UserID);

            sic.Add(si);

            Duzon.Common.Util.SpInfo si1 = new Duzon.Common.Util.SpInfo();
            si1 = new Duzon.Common.Util.SpInfo();
            si1.DataState = DataValueState.Added;
            dt1.RemotingFormat = SerializationFormat.Binary;

            si1.DataValue = dt1; 					//저장할 데이터 테이블
            si1.CompanyID = _mf.LoginInfo.CompanyCode;
            si1.UserID = _mf.LoginInfo.EmployeeNo;
            si1.SpNameInsert = "UP_PU_IVL_INSERT";			//Insert 프로시저명

            /*위 데이터테이블에서 각 프로시저에서 사용할 파라미터 Value에 대한 컬럼을 정의한다.*/
            si1.SpParamsInsert = new string[] { "NO_IV", "NO_IVLINE", "CD_COMPANY1", "CD_PLANT", "NO_IO", "NO_IOLINE", "CD_ITEM",
												"CD_GROUP", "CD_CC", "DT_TAX1", "QT_IV", "UM", "AM_IV", "VAT_IV", "NO_EMP1", "CD_PJT",  
												"FG_TPPURCHASE", "NO_PO", "NO_POLINE", "CD_EXCH", "RT_EXCH", "YN_RETURN", "UM_EX", "AM_EX", 
												"QT_CLS", "NO_LC", "NO_LCLINE" , "CD_QTIOTP", "YN_PURSUB1","CD_PARTNER","CD_BIZAREA1","FG_TRANS1",
                                                "SEQ_PROJECT","NO_WBS", "NO_CBS","TP_UM_TAX", "UM_WEIGHT", "TOT_WEIGHT", "NUM_USERDEF1"

            };

            si1.SpParamsValues.Add(ActionState.Insert, "CD_COMPANY1", ds.Rows[0]["CD_COMPANY"].ToString().Trim());
            si1.SpParamsValues.Add(ActionState.Insert, "DT_TAX1", 매입일자);
            si1.SpParamsValues.Add(ActionState.Insert, "NO_EMP1", ds.Rows[0]["NO_EMP"].ToString());
            si1.SpParamsValues.Add(ActionState.Insert, "CD_CC1", CD_CC);            //  CD_CC 추가
            si1.SpParamsValues.Add(ActionState.Insert, "YN_PURSUB1", ds.Rows[0]["YN_PURSUB"].ToString());
            si1.SpParamsValues.Add(ActionState.Insert, "CD_BIZAREA1", ds.Rows[0]["CD_BIZAREA"].ToString());
            si1.SpParamsValues.Add(ActionState.Insert, "FG_TRANS1", FG_TRANS);

            sic.Add(si1);

            if (Global.MainFrame.ServerKeyCommon == "UNIPOINT")
            {
                SpInfo si2 = new SpInfo();
                si2.DataState = DataValueState.Added;
                si2.DataValue = dt;
                si2.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si2.SpNameInsert = "UP_PU_Z_UNIPOINT_PRERCV";

                si2.SpParamsInsert = new string[] { "CD_COMPANY1", "NO_IV", "ID_INSERT_N" };
                si2.SpParamsValues.Add(ActionState.Insert, "ID_INSERT_N", Global.MainFrame.LoginInfo.UserID);
                si2.SpParamsValues.Add(ActionState.Insert, "CD_COMPANY1", Global.MainFrame.LoginInfo.CompanyCode);

                sic.Add(si2);
            }
            //
            if (Global.MainFrame.ServerKeyCommon == "CGBIO" || Global.MainFrame.ServerKeyCommon == "DNCOMPANY")
            {
                SpInfo si2 = new SpInfo();
                si2.DataState = DataValueState.Added;
                si2.DataValue = dt1;
                si2.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si2.SpNameInsert = "UP_PU_Z_CGBIO_UM_U";

                si2.SpParamsInsert = new string[] { "CD_COMPANY1", "NO_PO", "NO_POLINE" };
                si2.SpParamsValues.Add(ActionState.Insert, "CD_COMPANY1", Global.MainFrame.LoginInfo.CompanyCode);

                sic.Add(si2);
            }
            
            return _mf.Save(sic);
        }

        public bool 부가세변경(string 마감번호)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_IVH_MODIFY";
            si.SpParamsSelect = new object [] { 마감번호, Global.MainFrame.LoginInfo.CompanyCode };
           ResultData _rtn = ( ResultData )Global.MainFrame.FillDataTable( si );

            return true;
        }

        // 사용안함
        public string 전표유형(string p_FG_TPPURCHASE)
        { 
            String str_cd_docu = string.Empty;
            String SelectQuery = string.Empty;

            SelectQuery = "SELECT CD_DOCU "  + 
                          "  FROM MA_AISPOSTH " +
                          " WHERE CD_COMPANY = '"+_mf.LoginInfo.CompanyCode+"' " +
                          "   AND CD_TP = '"+p_FG_TPPURCHASE+"' " +
                          "   AND FG_TP = '200' "
                                      ;
            DataTable dt = DBHelper.GetDataTable(SelectQuery);

            if (dt == null || dt.Rows.Count == 0)
                str_cd_docu = "45";
            else
            {
                str_cd_docu = DBHelper.GetDataTable(SelectQuery).Rows[0][0].ToString();

                if (str_cd_docu == string.Empty)
                    str_cd_docu = "45";
            }

            

            return str_cd_docu;
        }

        public DataTable getCd_Pc(string strCd_Bizarea)
        {

            string SelectQuery = "";

            SelectQuery = "SELECT M.CD_PC, P.NM_PC, B.CD_BIZAREA, B.NM_BIZAREA " +
                          "  FROM MA_BIZAREA M " +
                          " INNER JOIN MA_PC P ON M.CD_PC = P.CD_PC AND M.CD_COMPANY = P.CD_COMPANY " +
                          " LEFT JOIN MA_BIZAREA B ON M.VAT_BIZAREA = B.CD_BIZAREA AND M.CD_COMPANY = B.CD_COMPANY " +
                          " WHERE M.CD_COMPANY = '" + _mf.LoginInfo.CompanyCode + "' " +
                          "   AND M.CD_BIZAREA = '" + strCd_Bizarea + "' ";

            DataTable dtCd_Pc = DBHelper.GetDataTable(SelectQuery);

            return dtCd_Pc;
        }


        //internal DataTable Search_Line(string key, string 매입번호, string 항번, string 입고번호, string 품목코드, string 품목명, string 규격, string 입고수량, string 단가, string 금액, string 원화단가, string 원화금액, string 매입형태)
        //{

            //SpInfo si = new SpInfo();
            //si.SpNameSelect = "UP_PU_GIIV_SUB_SELECT_L";
            //si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            //si.SpParamsSelect = new string[] { key, Global.MainFrame.LoginInfo.CompanyCode, 발주일fr, 발주일to, 사업장, 과세구분, 거래구분, 거래처, 프로젝트, gs_FGTAX, 발주번호, 담당자, 구매그룹, 요청그룹, bp수불형태, 창고 };
            //ResultData resultdata = (ResultData)Global.MainFrame.FillDataTable(si);
            //DataTable dt = (DataTable)resultdata.DataValue;

            //return dt;
        //}

        internal DataTable Search_Line1(string key, string 발주일fr, string 발주일to, string 사업장, string 과세구분, string 거래구분, string 거래처, string 프로젝트, string gs_FGTAX, string 발주번호, string 담당자, string 구매그룹, string 요청그룹, string bp수불형태, string 창고)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_GIIV_SUB_SELECT_L";
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.SpParamsSelect = new string[] { key, Global.MainFrame.LoginInfo.CompanyCode, 발주일fr, 발주일to, 사업장, 과세구분, 거래구분, 거래처, 프로젝트, gs_FGTAX, 발주번호, 담당자, 구매그룹, 요청그룹, bp수불형태, 창고 };
            ResultData resultdata = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)resultdata.DataValue;

            return dt;
        }



    }
}

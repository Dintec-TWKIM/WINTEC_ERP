using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

namespace pur
{
    public class P_PU_STS_REG_BIZ
    {
        string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;
        string 담당자 = Global.MainFrame.LoginInfo.EmployeeNo;
        string 공장코드 = string.Empty;

        public string biz_cd_plant = string.Empty;
        

        #region -> ♣Search

        public DataTable Search_LOT()
        {

            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_MNG_LOT_SELECT";
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.SpParamsSelect = new string[] { Global.MainFrame.LoginInfo.CompanyCode };
            ResultData resultdata = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)resultdata.DataValue;

            return dt;
        }

        public String Search_SERIAL()
        {
            //SERIAL사용여부CHK
            String str_rtn = "N";
            Object[] obj = new string[] { Global.MainFrame.LoginInfo.CompanyCode };
            DataTable dt = DBHelper.GetDataTable("UP_PU_MNG_SER_SELECT", obj);

            if (dt.Rows.Count > 0)
            {
                str_rtn = dt.Rows[0]["YN_SERIAL"].ToString();
                if (str_rtn ==  string.Empty)
                    str_rtn = "N";
            }

            return str_rtn;
        }

        public DataSet Search(string NO_IO,string CD_PLANT, string CD_PJT)
        {

            object[] m_obj = new object[2];
            m_obj[0] = NO_IO;
            m_obj[1] = Global.MainFrame.LoginInfo.CompanyCode;

            DataSet ds = null;

            if (Global.MainFrame.ServerKeyCommon.Contains("HANSU") || Global.MainFrame.ServerKeyCommon.Contains("DZSQL"))
                ds = DBHelper.GetDataSet("UP_PU_Z_HANSU_STS_SELECT", m_obj);
            else
                ds = DBHelper.GetDataSet("UP_PU_STS_SELECT", m_obj);
            
            //SpInfo si = new SpInfo();
            //ResultData result = (ResultData)Global.MainFrame.FillDataSet("UP_PU_STS_SELECT", m_obj);
            //DataSet ds = (DataSet)result.DataValue;
            ds.Tables[1].Columns.Add("FLAG", typeof(string));
            //ds.Tables[1].Columns.Add("YN_AM", typeof(string));

            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn Col in dt.Columns)
                {
                    if (Col.DataType == Type.GetType("System.Decimal"))
                        Col.DefaultValue = 0;
                }
            }

            DataTable dtHeader = ds.Tables[0];
            DataTable dtLine = ds.Tables[1];

            dtHeader.Columns["DT_IO"].DefaultValue = Global.MainFrame.GetStringToday;
            dtHeader.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dtHeader.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            dtHeader.Columns["CD_PLANT"].DefaultValue = CD_PLANT;
            dtHeader.Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;

            return ds;
        }

        public DataSet dt_print(string NO_IO, string SERVERKEY)
        {

            object[] m_obj = new object[2];
            m_obj[0] = NO_IO;
            m_obj[1] = Global.MainFrame.LoginInfo.CompanyCode;

            string strSpname = "";

            if (SERVERKEY == "WJIS")
                strSpname = "UP_PU_Z_WJIS_STS_PRINT_S";
            else
                strSpname = "UP_PU_STS_PRINT_S";

            DataSet ds = DBHelper.GetDataSet(strSpname, m_obj);


            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn Col in dt.Columns)
                {
                    if (Col.DataType == Type.GetType("System.Decimal"))
                        Col.DefaultValue = 0;
                }
            }


            return ds;
        }

        public DataTable Search_요청(string NO_IO)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_REQ_SELECT_STS";
            si.CompanyID = 회사코드;
            si.SpParamsSelect = new string[] { 회사코드, NO_IO };
            ResultData resultdata = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)resultdata.DataValue;

            return dt;
        }

        #endregion

        #region -> ♣Save

        public bool Save(DataTable dtH, DataTable dtL, DataTable dtLOT, DataTable dtSERL, DataTable dt_location, DataTable dtQCl)
        {

            SpInfoCollection sic = new SpInfoCollection();

            if (dtH != null)
            {
                SpInfo si = new SpInfo();

            //dtH.Rows[0]["NO_IO"] = NO_IO;

                si.DataValue = dtH; 	//저장할 데이터 테이블
                si.CompanyID = 회사코드;
                si.UserID = 담당자;
                si.SpNameInsert = "UP_PU_MM_QTIOH_INSERT";	//Insert 프로시저명
                si.SpNameUpdate = "UP_PU_MM_QTIOH_UPDATE";

                si.SpParamsInsert = new string[] { "NO_IO", "CD_COMPANY", "CD_PLANT", "CD_PARTNER", "FG_TRANS", "YN_RETURN", "DT_IO", "GI_PARTNER", "CD_DEPT", "NO_EMP", "DC_RMK", "ID_INSERT", "CD_QTIOTP" };
                si.SpParamsUpdate = new string[] { "NO_IO", "CD_COMPANY", "ID_INSERT", "DC_RMK" };

                //si.SpParamsValues.Add(ActionState.Insert, "ID_INSERT1", Global.MainFrame.LoginInfo.UserID);

                sic.Add(si);
            }
            if (dtL != null)
            {
                공장코드 = biz_cd_plant; //dtL.Rows[0]["CD_PLANT"].ToString();  //  공장코드 (LINE에 데이터가 없으면 LOT나 SERIAL도 없다)
                SpInfo siL = new SpInfo();
                //	si.DataState = DataValueState.Added; 
                siL.DataValue = dtL; 					//저장할 데이터 테이블
                siL.CompanyID = 회사코드;
                siL.UserID = 담당자;
                siL.SpNameInsert = "UP_PU_STS_INSERT";	//Insert 프로시저명
                siL.SpNameUpdate = "UP_PU_STS_UPDATE";
                siL.SpNameDelete = "UP_PU_STS_DELETE";
                
          
               siL.SpParamsInsert = new string[] { "YN_RETURN","NO_IO","NO_IOLINE","CD_COMPANY","CD_PLANT","CD_SL","DT_IO",
												"CD_QTIOTP", "CD_ITEM",
												"QT_GOOD_INV","QT_REJECT_INV","NO_EMP","CD_SL_REF","YN_AM",
                                                "NO_ISURCV","NO_ISURCVLINE","NO_PSO_MGMT", "NO_PSOLINE_MGMT",
                                                "FLAG","NO_GIREQ","NO_LINE" ,"QT_UNIT_PO","P_NO_POP","P_NO_POP_LINE","CD_PARTNER","DC_RMK","CD_PROJECT","SEQ_PROJECT","DC_RMK1",
                                                "NO_WBS", "NO_CBS","NO_IO_MGMT","NO_IOLINE_MGMT","NO_MREQ","NO_MREQLINE","GI_PARTNER","CD_ITEM_REF","NO_TRACK","NO_TRACK_LINE"};
         
                siL.SpParamsUpdate = new string[] { "NO_IO", "NO_IOLINE", "CD_COMPANY", "QT_GOOD_OLD", "QT_GOOD_INV", "DC_RMK", "DC_RMK1","NO_MREQ","NO_MREQLINE" };
                siL.SpParamsDelete = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_MREQ", "NO_MREQLINE" };
                siL.SpParamsValues.Add(ActionState.Insert, "NO_GIREQ", "");
                siL.SpParamsValues.Add(ActionState.Insert, "NO_LINE", 0);
                //siL.SpParamsValues.Add(ActionState.Insert, "QT_UNIT_PO", 0);
                siL.SpParamsValues.Add(ActionState.Insert, "P_NO_POP", "");
                siL.SpParamsValues.Add(ActionState.Insert, "P_NO_POP_LINE", 0);
                siL.SpParamsValues.Add(ActionState.Insert, "CD_ITEM_REF", "");
                //if (!Global.MainFrame.ServerKeyCommon.Contains("HANSU") && !Global.MainFrame.ServerKeyCommon.Contains("DZSQL"))
                //{
                //    siL.SpParamsValues.Add(ActionState.Insert, "GI_PARTNER", "");
                //}
                //if (dtH != null)
                //{
                //    //siL.SpParamsValues.Add는 해당 DATATABLE에 동일한 컬럼이 있을경우 에러를 발생함에 유의할것 

                //    siL.SpParamsValues.Add(ActionState.Insert, "CD_PLANT1", dtH.Rows[0]["CD_PLANT"].ToString());
                //    siL.SpParamsValues.Add(ActionState.Insert, "CD_QTIOTP1", dtH.Rows[0]["CD_QTIOTP"].ToString());
                //    siL.SpParamsValues.Add(ActionState.Insert, "DT_IO1", dtH.Rows[0]["DT_IO"].ToString());
                //    siL.SpParamsValues.Add(ActionState.Insert, "NO_EMP1", dtH.Rows[0]["NO_EMP"]);

                //    //siL.SpParamsValues.Add(ActionState.Insert, "CD_SL1", dtH.Rows[0]["CD_SL_REF"].ToString());
                //    //siL.SpParamsValues.Add(ActionState.Insert, "GR_SL1", dtH.Rows[0]["CD_SL"].ToString());
                   // siL.SpParamsValues.Add(ActionState.Insert, "YN_AM", "N");
                //}
                sic.Add(siL);
            }

            if (dtLOT != null)
            {
                SpInfo si03 = new SpInfo();
                si03.DataValue = dtLOT;
                si03.DataState = DataValueState.Added;
                //si03.SpNameInsert = "UP_MM_QTIOLOT_INSERT";
                //si03.SpNameUpdate = "UP_MM_QTIOLOT_UPDATE";
                //si03.SpNameDelete = "UP_MM_QTIOLOT_DELETE";
                si03.SpNameInsert = "UP_PU_STS_LOT_INSERT";
                si03.SpNameUpdate = "UP_PU_STS_LOT_UPDATE";
            //    si03.SpNameDelete = "UP_PU_STS_LOT_DELETE";

                si03.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si03.SpParamsInsert = new string[] { "CD_COMPANY", "출고번호", "출고항번", "NO_LOT", "CD_ITEM", "출고일", "FG_PS",
                                                     "수불구분", "수불형태", "창고코드", "QT_GOOD_MNG", "NO_IO", "NO_IOLINE", "NO_IOLINE2", "YN_RETURN",
                                                     "입고창고", "DT_LIMIT",
                                                     "CD_MNG1","CD_MNG2","CD_MNG3","CD_MNG4","CD_MNG5","CD_MNG6","CD_MNG7","CD_MNG8","CD_MNG9","CD_MNG10","CD_MNG11","CD_MNG12","CD_MNG13","CD_MNG14","CD_MNG15","CD_MNG16","CD_MNG17","CD_MNG18","CD_MNG19","CD_MNG20","BARCODE"};
                si03.SpParamsUpdate = new string[] { "CD_COMPANY", "출고번호", "출고항번", "NO_LOT", "QT_IO", "QT_IO_OLD", "DT_LIMIT" };
              //  si03.SpParamsDelete = new string[] { "CD_COMPANY", "출고번호", "출고항번", "NO_LOT" };
                //si03.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", dtH.Rows[0]["YN_RETURN"].ToString());
                //si03.SpParamsValues.Add(ActionState.Insert, "FG_PS1", "2");
                //si03.SpParamsValues.Add(ActionState.Insert, "수불구분", "022");

                sic.Add(si03);
            }


            if (dtSERL != null)
            {
                SpInfo si04 = new SpInfo();
                si04.DataValue = dtSERL;

                si04.SpNameInsert = "UP_MM_QTIODS_INSERT";
                si04.SpNameUpdate = "UP_MM_QTIODS_UPDATE";
                si04.SpNameDelete = "UP_MM_QTIODS_DELETE";
                si04.CompanyID = 회사코드;
                si04.UserID = 담당자;
                si04.SpParamsInsert = new string[] { 
	            "CD_COMPANY", "NO_SERIAL", "NO_IO", "NO_IOLINE", "CD_ITEM", "CD_QTIOTP", "FG_IO",
		        "CD_MNG1",	"CD_MNG2",	"CD_MNG3",	"CD_MNG4",	"CD_MNG5",	"CD_MNG6",	"CD_MNG7",	"CD_MNG8",	"CD_MNG9",	"CD_MNG10",
		        "CD_MNG11",	"CD_MNG12",	"CD_MNG13",	"CD_MNG14",	"CD_MNG15",	"CD_MNG16",	"CD_MNG17",	"CD_MNG18",	"CD_MNG19",	"CD_MNG20",
                "CD_PLANT", "ID_INSERT"
                };
                si04.SpParamsUpdate = new string[] { 
	            "CD_COMPANY", "NO_SERIAL", "NO_IO", "NO_IOLINE", "CD_ITEM", "CD_QTIOTP", "FG_IO",
		        "CD_MNG1",	"CD_MNG2",	"CD_MNG3",	"CD_MNG4",	"CD_MNG5",	"CD_MNG6",	"CD_MNG7",	"CD_MNG8",	"CD_MNG9",	"CD_MNG10",
		        "CD_MNG11",	"CD_MNG12",	"CD_MNG13",	"CD_MNG14",	"CD_MNG15",	"CD_MNG16",	"CD_MNG17",	"CD_MNG18",	"CD_MNG19",	"CD_MNG20"
                };
                si04.SpParamsDelete = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_SERIAL" };

                si04.SpParamsValues.Add(ActionState.Insert, "CD_PLANT", 공장코드);
                si04.SpParamsValues.Add(ActionState.Insert, "ID_INSERT", 담당자);

                sic.Add(si04);
            }

            if (dt_location != null && dt_location.Rows.Count > 0)
            {
                SpInfo si05 = new SpInfo();
                si05.DataValue = dt_location;
                si05.DataState = DataValueState.Added;
                si05.SpNameInsert = "UP_MM_QTIO_LOCATION_I";
                si05.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si05.SpParamsInsert = new string[] { 
	            "CD_COMPANY", "NO_IO", "NO_IOLINE", "CD_LOCATION", "CD_ITEM", "DT_IO", "FG_PS", "FG_IO", "CD_QTIOTP","CD_PLANT", "CD_SL", "QT_IO_LOCATION", "YN_RETURN"};

                sic.Add(si05);
            }

            if (dtQCl != null && dtQCl.Rows.Count > 0)
            {
                SpInfo siQCH = new SpInfo();
                siQCH.DataValue = dtH;
                siQCH.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siQCH.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siQCH.SpNameInsert = "UP_MM_QC_INSERT";
                siQCH.SpParamsInsert = new string[] { "CD_COMPANY", "NO_IO", "DT_IO", "CD_PARTNER", "NO_EMP", "FG_IO" };

                siQCH.SpParamsValues.Add(ActionState.Insert, "FG_IO", "022");

                sic.Add(siQCH);

                SpInfo siQCL = new SpInfo();
                siQCL.DataValue = dtQCl;
                siQCL.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siQCL.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siQCL.SpNameInsert = "UP_MM_QCL_INSERT";
                siQCL.SpParamsInsert = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "CD_PLANT", "CD_ITEM", "QT_GOOD_INV", "DC_RMK", "CD_PROJECT", "SEQ_PROJECT" };

                sic.Add(siQCL);
            }
         
            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sic);
            for (int i = 0; i < rtn.Length; i++)
                if (!rtn[i].Result) return false;
            return true;


        }

        #endregion

        #region ♣ 삭제

        #region -> 삭제

        public void Delete(string NO_IO)
        {
            Global.MainFrame.ExecSp("UP_PU_GRM_DELETE", new object[] { NO_IO, 회사코드, Global.MainFrame.LoginInfo.UserID });
        }
        #endregion

        #endregion

        #region - > 처음 초기화 바인딩할 테이블 작업
        internal DataSet Initial_DataSet() //처음 초기화 테이블을 디비를 갔다 오지않고 수동으로 만들어 주는 작업임
        {// 여기를 수정한다면....조회 프로시져도 같이 수정을 해주어야한다....

            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            ds.Tables[0].Columns.Add("NO_IO", typeof(string));
            ds.Tables[0].Columns.Add("CD_PLANT", typeof(string));
            ds.Tables[0].Columns.Add("CD_PARTNER", typeof(string));
            ds.Tables[0].Columns.Add("FG_TRANS", typeof(string));
            ds.Tables[0].Columns.Add("YN_RETURN", typeof(string));
            ds.Tables[0].Columns.Add("DT_IO", typeof(string));
            ds.Tables[0].Columns.Add("GI_PARTNER", typeof(string));
            ds.Tables[0].Columns.Add("CD_DEPT", typeof(string));
            ds.Tables[0].Columns.Add("NO_EMP", typeof(string));
            ds.Tables[0].Columns.Add("DC_RMK", typeof(string));
            ds.Tables[0].Columns.Add("CD_QTIOTP", typeof(string));
            ds.Tables[0].Columns.Add("NM_QTIOTP", typeof(string));
            ds.Tables[0].Columns.Add("NM_DEPT", typeof(string));
            ds.Tables[0].Columns.Add("NM_KOR", typeof(string));
            ds.Tables[0].Columns.Add("CD_SL_REF", typeof(string));
            ds.Tables[0].Columns.Add("CD_SL", typeof(string));
            ds.Tables[0].Columns.Add("IN_SL", typeof(string));
            ds.Tables[0].Columns.Add("OUT_SL", typeof(string));
            ds.Tables[0].Columns.Add("YN_AM", typeof(string));
            ds.Tables[0].Columns.Add("NO_QC", typeof(string));

            ds.Tables[1].Columns.Add("S", typeof(string));
            ds.Tables[1].Columns.Add("NO_IO", typeof(string));
            ds.Tables[1].Columns.Add("NO_IOLINE", typeof(decimal));
            ds.Tables[1].Columns.Add("CD_ITEM", typeof(string));
            ds.Tables[1].Columns.Add("NM_ITEM", typeof(string));
            ds.Tables[1].Columns.Add("STND_ITEM", typeof(string));
            ds.Tables[1].Columns.Add("UNIT_IM", typeof(string));
            ds.Tables[1].Columns.Add("UNIT_PO", typeof(string));
            ds.Tables[1].Columns.Add("NO_LOT", typeof(string));
            ds.Tables[1].Columns.Add("QT_GOOD_INV", typeof(decimal));
            ds.Tables[1].Columns.Add("QT_GOOD_OLD", typeof(decimal));
            ds.Tables[1].Columns.Add("QT_UNIT_PO", typeof(decimal));
            ds.Tables[1].Columns.Add("QT_REJECT_INV", typeof(decimal));
            ds.Tables[1].Columns.Add("UM", typeof(decimal));
            ds.Tables[1].Columns.Add("AM", typeof(decimal));
            ds.Tables[1].Columns.Add("PROJECT", typeof(string));
            ds.Tables[1].Columns.Add("NO_ISURCV", typeof(string));
            ds.Tables[1].Columns.Add("NO_ISURCVLINE", typeof(decimal));
            ds.Tables[1].Columns.Add("NO_PSO_MGMT", typeof(string));
            ds.Tables[1].Columns.Add("NO_PSOLINE_MGMT", typeof(decimal));
            ds.Tables[1].Columns.Add("CD_SL", typeof(string));
            ds.Tables[1].Columns.Add("출고창고", typeof(string));
            ds.Tables[1].Columns.Add("CD_SL_REF", typeof(string));
            ds.Tables[1].Columns.Add("입고창고", typeof(string));
            ds.Tables[1].Columns.Add("요청번호", typeof(string));
            ds.Tables[1].Columns.Add("요청일자", typeof(string));
            ds.Tables[1].Columns.Add("CD_DEPT", typeof(string));
            ds.Tables[1].Columns.Add("NO_EMP", typeof(string));
            ds.Tables[1].Columns.Add("요청부서", typeof(string));
            ds.Tables[1].Columns.Add("요청자", typeof(string));
            ds.Tables[1].Columns.Add("CD_QTIOTP", typeof(string));
            ds.Tables[1].Columns.Add("CD_PLANT", typeof(string));
            ds.Tables[1].Columns.Add("YN_RETURN", typeof(string));
            ds.Tables[1].Columns.Add("DT_IO", typeof(string));
            ds.Tables[1].Columns.Add("FLAG", typeof(string));
            ds.Tables[1].Columns.Add("YN_AM", typeof(string));
            ds.Tables[1].Columns.Add("NO_SERL", typeof(string));
            ds.Tables[1].Columns.Add("FG_IO", typeof(string));
            ds.Tables[1].Columns.Add("NM_QTIOTP", typeof(string));
            ds.Tables[1].Columns.Add("CD_PARTNER", typeof(string));
            ds.Tables[1].Columns.Add("QT_GOODS", typeof(decimal));
            ds.Tables[1].Columns.Add("DC_RMK", typeof(string));
            ds.Tables[1].Columns.Add("CD_ZONE", typeof(string));
            ds.Tables[1].Columns.Add("NM_ITEMGRP", typeof(string));
            ds.Tables[1].Columns.Add("FG_SERNO", typeof(string));
            ds.Tables[1].Columns.Add("NO_SO", typeof(string));
            ds.Tables[1].Columns.Add("DC_RMK1", typeof(string));
            ds.Tables[1].Columns.Add("NO_IO_MGMT", typeof(string));
            ds.Tables[1].Columns.Add("NO_IOLINE_MGMT", typeof(decimal));
            ds.Tables[1].Columns.Add("NO_MREQ", typeof(string));
            ds.Tables[1].Columns.Add("NO_MREQLINE", typeof(string));
            ds.Tables[1].Columns.Add("BARCODE", typeof(string));
            ds.Tables[1].Columns.Add("QT_GIREQ", typeof(decimal));
            ds.Tables[1].Columns.Add("FG_SLQC", typeof(string));
            ds.Tables[1].Columns.Add("CLS_ITEM", typeof(string));
            ds.Tables[1].Columns.Add("UNIT_PO_FACT", typeof(decimal));
            ds.Tables[1].Columns.Add("NO_TRACK", typeof(string));
            ds.Tables[1].Columns.Add("NO_TRACK_LINE", typeof(decimal));
            ds.Tables[1].Columns.Add("LN_PARTNER", typeof(string)); 
            ds.Tables[1].Columns.Add("NM_MAKER", typeof(string));
            ds.Tables[1].Columns.Add("NO_DESIGN", typeof(string));

          //  if (Config.MA_ENV.PJT형여부 == "Y")
          //  {
                ds.Tables[1].Columns.Add("CD_PROJECT", typeof(string));
                ds.Tables[1].Columns.Add("NM_PROJECT", typeof(string));
                ds.Tables[1].Columns.Add("SEQ_PROJECT", typeof(decimal));
                ds.Tables[1].Columns.Add("CD_PJT_ITEM", typeof(string));
                ds.Tables[1].Columns.Add("NM_PJT_ITEM", typeof(string));
                ds.Tables[1].Columns.Add("NO_WBS", typeof(string));
                ds.Tables[1].Columns.Add("NO_CBS", typeof(string));
                ds.Tables[1].Columns.Add("STND_UNIT", typeof(string));
                ds.Tables[1].Columns.Add("FG_GUBUN", typeof(string));
                //  }
            ds.Tables[1].Columns.Add("GI_PARTNER", typeof(string));
            ds.Tables[1].Columns.Add("LN_GI_PARTNER", typeof(string));


            if (Global.MainFrame.ServerKeyCommon.Contains("HANSU") || Global.MainFrame.ServerKeyCommon.Contains("DZSQL"))
            {
                ds.Tables[1].Columns.Add("CD_ITEM_PARTNER", typeof(string));
                ds.Tables[1].Columns.Add("NM_ITEM_PARTNER", typeof(string));
                ds.Tables[1].Columns.Add("CD_PACK", typeof(string));
                ds.Tables[1].Columns.Add("NM_PACK", typeof(string));
                ds.Tables[1].Columns.Add("CD_TRANSPORT", typeof(string));
                ds.Tables[1].Columns.Add("CD_PART", typeof(string));
                ds.Tables[1].Columns.Add("NM_PART", typeof(string));
                ds.Tables[1].Columns.Add("YN_TEST_RPT", typeof(string));
                ds.Tables[1].Columns.Add("DC_DESTINATION", typeof(string));
                ds.Tables[1].Columns.Add("DC_RMK_REQ", typeof(string));
                ds.Tables[1].Columns.Add("DT_DELIVERY", typeof(string));
                ds.Tables[1].Columns.Add("NUM_USERDEF1", typeof(string));
                ds.Tables[1].Columns.Add("CD_SALEGRP", typeof(string));
                ds.Tables[1].Columns.Add("PRIOR_GUBUN", typeof(string));
            }

            ds.Tables[1].Columns.Add("NM_CLS_L", typeof(string));
            ds.Tables[1].Columns.Add("NM_CLS_M", typeof(string));
            ds.Tables[1].Columns.Add("NM_CLS_S", typeof(string));

            
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn Col in dt.Columns)
                {
                    if (Col.DataType == Type.GetType("System.Decimal"))
                        Col.DefaultValue = 0;
                    else if (Col.DataType == Type.GetType("System.String"))
                        Col.DefaultValue = "";
                }
            }

            ds.Tables[0].Columns["DT_IO"].DefaultValue = Global.MainFrame.GetStringToday;
            ds.Tables[0].Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            ds.Tables[0].Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            ds.Tables[0].Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant; //D.GetString(cb_cd_plant.SelectedValue);
            ds.Tables[0].Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;

            ds.Tables[1].Columns["FG_IO"].DefaultValue = "022";
            

            return ds;
        }
        #endregion

        #region - > 품목조회
        internal DataTable Item_List_search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_MM_PITEM_SELECT", obj);
            return dt;

        }
        #endregion

        #region - > 창고조회
        internal DataTable CD_SL_search(string cd_plant)
        {
            string sql = "SELECT CD_SL, NM_SL, YN_QC FROM MA_SL ";
                    sql += " WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
                    sql += " AND CD_PLANT = '" + cd_plant +"'";

            DataTable dt = DBHelper.GetDataTable(sql);
            return dt;

        }
        #endregion 

        #region - > 품목,창고검사유무
        internal string FG_SLQC(string gubon, string CD_ITEM, string CD_PLANT, string CD_SL)
        {
            string Query = "";
            if(gubon == "ITEM")
                Query = "SELECT FG_SLQC FROM MA_PITEM WHERE CD_COMPANY ='" + Global.MainFrame.LoginInfo.CompanyCode + "' AND CD_ITEM='" + CD_ITEM + "' AND CD_PLANT ='" + CD_PLANT + "'";
            else
                Query = "SELECT YN_QC FROM MA_SL WHERE CD_COMPANY ='" + Global.MainFrame.LoginInfo.CompanyCode + "' AND CD_SL='" + CD_SL + "' AND CD_PLANT ='" + CD_PLANT + "'";

            DataTable dt = DBHelper.GetDataTable(Query);

            string result = "";

            if (dt.Rows.Count == 0 || dt == null)
                result = "N";
            else
                if(gubon == "ITEM")
                    result = D.GetString(dt.Rows[0]["FG_SLQC"]);
                else
                    result = D.GetString(dt.Rows[0]["YN_QC"]);

            return result;

        }
        #endregion

        #region - > 안전공업전용 출고요청적용데이터 체크로직
        internal DataTable YN_Pallet(string NO_ISURCV)
        {
            string sql = "SELECT  CASE WHEN ISNULL(SUM(CASE WHEN ISNULL(SH.STA_GIR,'') = 'TRQ' THEN 1 ELSE 0 END),0) > 0 THEN ";
			sql += " CASE WHEN COUNT(1) <> ISNULL(SUM(CASE WHEN ISNULL(SH.STA_GIR,'') = 'TRQ' THEN 1 ELSE 0 END),0) THEN 'E' ELSE 'Y' END ";
            sql += " WHEN ISNULL(SUM(CASE WHEN ISNULL(SH.STA_GIR,'') = 'TRQ' THEN 1 ELSE 0 END),0) = 0 THEN 'N' END YN_PALLET ";
            sql += " FROM	MM_GIREQL GL INNER JOIN SA_GIRH SH ON GL.CD_USERDEF1 = SH.NO_GIR AND GL.CD_COMPANY = SH.CD_COMPANY ";
            sql += " WHERE	GL.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
            sql += " AND     GL.NO_GIREQ IN ( SELECT * FROM GETTABLEFROMSPLIT2('" + NO_ISURCV + "'))";
            
            DataTable dt = DBHelper.GetDataTable(sql);
            
            return dt;

        }
        #endregion 

    }
}

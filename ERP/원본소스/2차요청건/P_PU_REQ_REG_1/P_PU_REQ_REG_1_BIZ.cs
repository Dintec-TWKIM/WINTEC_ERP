using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using System.Data;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

namespace pur
{
    class P_PU_REQ_REG_1_BIZ
    {
        #region -> Search

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

        public DataTable Search_SERIAL()
        {
            //SERIAL사용여부CHK
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_MNG_SER_SELECT";
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.SpParamsSelect = new string[] { Global.MainFrame.LoginInfo.CompanyCode };
            ResultData resultdata = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)resultdata.DataValue;

            return dt;
        }

        public DataSet Search(string NO_RCV, string YN_AUTORCV)
        {
            ResultData resultdata = (ResultData)Global.MainFrame.FillDataSet("UP_PU_REQ_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode, NO_RCV, YN_AUTORCV });
            DataSet ds = (DataSet)resultdata.DataValue;

            DataTable dtHeader = ds.Tables[0];

            dtHeader.Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            dtHeader.Columns["DT_REQ"].DefaultValue = Global.MainFrame.GetStringToday;
            dtHeader.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dtHeader.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            dtHeader.Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;
            dtHeader.Columns["FG_TRANS"].DefaultValue = "001";
            dtHeader.Columns["FG_PROCESS"].DefaultValue = "001";
            dtHeader.Columns["YN_AUTORCV"].DefaultValue = "Y";
            //dtHeader.Columns["FG_UM"].DefaultValue = "001";

            ds.Tables[1].Columns["DATE_USERDEF1"].DefaultValue = Global.MainFrame.GetStringToday;

            return ds;
        }

        public DataTable Search_MATL(string NO_PO)
        {
            DataTable dt = DBHelper.GetDataTable("UP_PU_REQ_MATL_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode, NO_PO });

            return dt;
        }

        
        internal DataTable Search_MATL(string NO_PO_MULTI, object[] obj)
        {
            DataTable dt결과 = null;
            try
            {
                string[] arr = D.StringConvert.GetPipes(NO_PO_MULTI, 150);
                string arrLen = D.GetString(arr.Length);
                int cnt = 1;

                foreach (string NO_PO in arr)
                {
                    obj[1] = NO_PO;
                    DataTable dt = DBHelper.GetDataTable("UP_PU_REQ_MATL_MUTI_S", obj);

                    if (dt결과 == null)
                        dt결과 = dt;
                    else
                        dt결과.Merge(dt);
                }
            }
            finally
            {
                MsgControl.CloseMsg();
            }
            return dt결과;
        }

        #endregion

        #region -> Save

        public bool Save(DataTable dtH, DataTable dtL, DataTable dtLOT, string no_ioseq, DataTable dtSERL, string CD_PLANT, string NO_REQ, DataTable dtLOCATION, string YN_SPECIAL, DataTable dtHH, DataTable dtLL)
        {
            SpInfoCollection sic = new SpInfoCollection();

            if (dtH != null)
            {
                SpInfo siH = new SpInfo();

                dtH.RemotingFormat = SerializationFormat.Binary;

                siH.DataValue = dtH;
                siH.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siH.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siH.SpNameInsert = "UP_PU_RCVH_INSERT";
                siH.SpNameUpdate = "UP_PU_RCVH_UPDATE";
                siH.SpParamsInsert = new string[] { "NO_RCV", "CD_COMPANY", "CD_PLANT", "CD_PARTNER", "DT_REQ", "NO_EMP", "FG_TRANS", "FG_PROCESS", "CD_EXCH", 
                                                "CD_SL", "YN_RETURN", "YN_AM", "DC_RMK", "ID_INSERT", "FG_RCV","CD_DEPT", "DC_RMK_TEXT"};
                siH.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_RCV", "DC_RMK", "ID_INSERT", "DC_RMK_TEXT", "UM_WEIGHT", "TOT_WEIGHT" };
                sic.Add(siH);
            }

            if (dtL != null)
            {

                SpInfo siL = new SpInfo();

                dtL.RemotingFormat = SerializationFormat.Binary;

                siL.DataValue = dtL;
                siL.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siL.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siL.SpNameInsert = "UP_PU_REQ_INSERT";
                siL.SpNameUpdate = "UP_PU_REQ_UPDATE";
                siL.SpNameDelete = "UP_PU_REQ_AUTO_DELETE";

                siL.SpParamsInsert = new string[] { "NO_RCV", "NO_LINE",  "CD_COMPANY", "NO_PO", "NO_POLINE", "CD_PURGRP", "DT_LIMIT", "CD_ITEM", "QT_REQ" , "YN_INSP" ,
                                            "QT_PASS", "QT_REJECTION" , "CD_UNIT_MM", "QT_REQ_MM" , "CD_EXCH", "RT_EXCH", "UM_EX_PO" , "UM_EX", "AM_EXREQ", "UM" ,
                                            "AM_REQ", "VAT", "RT_CUSTOMS", "CD_PJT", "YN_PURCHASE", "YN_RETURN", "FG_TPPURCHASE", "FG_RCV", "FG_TRANS", "FG_TAX",
                                            "FG_TAXP", "YN_AUTORCV", "YN_REQ", "CD_SL", "NO_LC", "NO_LCLINE", "RT_SPEC", "NO_EMP", "NO_TO", "NO_TO_LINE",
                                            "CD_PLANT","CD_PARTNER","DT_REQ","DC_RMK", "YN_AUTOSTOCK","NO_REV","NO_REVLINE","CD_WH","DC_RMK2",
                                             "SEQ_PROJECT", "NO_WBS","NO_CBS", "TP_UM_TAX","FG_SPECIAL","DATE_USERDEF1","CDSL_USERDEF1","NUM_USERDEF1","NUM_USERDEF2",
                                             "UM_WEIGHT", "TOT_WEIGHT"};

                siL.SpParamsUpdate = new string[] {  "NO_RCV","NO_LINE", "CD_COMPANY", "DT_LIMIT", "QT_REQ" ,"QT_REQ_MM" ,														
                                                     "UM_EX_PO" ,"UM_EX" ,"AM_EXREQ" ,"UM" ,"AM_REQ" ,"VAT", "CD_SL","YN_INSP","DC_RMK" ,"DC_RMK2", /*"TP_UM_TAX",*/
                                                     "DATE_USERDEF1","CDSL_USERDEF1", "UM_WEIGHT", "TOT_WEIGHT" 	};
                siL.SpParamsDelete = new string[] { "NO_RCV", "NO_LINE", "CD_COMPANY" };

                siL.SpParamsValues.Add(ActionState.Insert, "YN_AUTOSTOCK", "Y"); // 입고등록 저장시에 자동입고유무를 'Y'으로 세팅 

                sic.Add(siL);
            }
           
            if (dtH != null && dtL != null)
            {
                SpInfo si = new SpInfo();
                dtH.RemotingFormat = SerializationFormat.Binary;

                si.DataValue = dtH;
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si.SpNameInsert = "UP_PU_MM_QTIOH_INSERT";
                si.SpParamsInsert = new string[] { "NO_IO1", "CD_COMPANY", "CD_PLANT", "CD_PARTNER", "FG_TRANS", "YN_RETURN", "DT_IO1", "GI_PARTNER", "CD_DEPT", "NO_EMP", "DC_RMK", "ID_INSERT", "FG_RCV" };

                si.SpParamsValues.Add(ActionState.Insert, "NO_IO1", no_ioseq);
                si.SpParamsValues.Add(ActionState.Insert, "DT_IO1", dtH.Rows[0]["DT_REQ"]);
                si.SpParamsValues.Add(ActionState.Insert, "GI_PARTNER", "");
                //si.SpParamsValues.Add(ActionState.Insert, "CD_DEPT", "");

                 
                sic.Add(si);


                si = new Duzon.Common.Util.SpInfo();

                dtL.RemotingFormat = SerializationFormat.Binary;

                si.DataValue = dtL; 					//저장할 데이터 테이블
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si.SpNameInsert = "UP_PU_GR_INSERT";			//Insert 프로시저명
                si.SpParamsInsert = new string[] { "YN_RETURN1", "NO_IO","NO_IOLINE", "CD_COMPANY", "CD_PLANT", "CD_SL", "DT_IO", "NO_RCV", "NO_LINE", "NO_PO", "NO_POLINE", "FG_PS1", 
											       "FG_TPPURCHASE", "FG_IO", "FG_RCV", "FG_TRANS", "FG_TAX", "CD_PARTNER1","CD_ITEM", "QT_REQ","QT_BAD1", "CD_EXCH", "RT_EXCH", "UM_EX", "UM", "AM_EX", "AM", 
                                                   "VAT", "FG_TAXP", "YN_AM1", "CD_PJT", "NO_LC", "NO_LCLINE", "NO_EMP", "CD_PURGRP","CD_UNIT_MM", "QT_REQ_MM","QT_BAD_MM1", "UM_EX_PO","YN_INSP", "YN_PURCHASE","DC_RMK",
                                                    "CD_WH","SEQ_PROJECT", "NO_WBS","NO_CBS","TP_UM_TAX","DC_RMK2","UM_WEIGHT","TOT_WEIGHT"};
                si.SpParamsValues.Add(ActionState.Insert, "YN_RETURN1", "N");
                //si.SpParamsValues.Add(ActionState.Insert, "CD_PLANT1", dtH.Rows[0]["CD_PLANT"].ToString());
                si.SpParamsValues.Add(ActionState.Insert, "CD_PARTNER1", dtH.Rows[0]["CD_PARTNER"].ToString());
                si.SpParamsValues.Add(ActionState.Insert, "YN_AM1", dtH.Rows[0]["YN_AM"].ToString());
                si.SpParamsValues.Add(ActionState.Insert, "FG_PS1", "1");
                si.SpParamsValues.Add(ActionState.Insert, "QT_BAD1", 0);
                si.SpParamsValues.Add(ActionState.Insert, "QT_BAD_MM1", 0);
                //si.SpParamsValues.Add(ActionState.Insert, "YN_INSP1", "N");

                sic.Add(si);


                ////20110406 최창종
                //if (D.GetString(BASIC.GetMAEXC("구매입고등록_검사")) == "200" || D.GetString(BASIC.GetMAEXC("구매입고등록_검사")) == "200")
                //{
                //    si = new Duzon.Common.Util.SpInfo();
                //    dtH.RemotingFormat = SerializationFormat.Binary;

                //    si.DataValue = dtH;
                //    si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                //    si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                //    si.SpNameInsert = "UP_MM_QC_INSERT";
                //    si.SpNameDelete = "UP_MM_QC_DELETE";
                //    si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_REQ", "DT_REQ", "CD_PARTNER", "NO_EMP", "FG_IO" };
                //    si.SpParamsDelete = new string[] { "CD_COMPANY", "NO_REQ" };

                //    si.SpParamsValues.Add(ActionState.Insert, "FG_IO", dtL.Rows[0]["FG_IO"]);
                //    si.SpParamsValues.Add(ActionState.Insert, "NO_REQ", NO_REQ);
                //    sic.Add(si);


                //    si = new Duzon.Common.Util.SpInfo();
                //    dtL.RemotingFormat = SerializationFormat.Binary;

                //    si.DataValue = dtL; 					//저장할 데이터 테이블
                //    si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                //    si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                //    si.SpNameInsert = "UP_MM_QCL_INSERT";
                //    si.SpNameInsert = "UP_MM_QCL_DELETE";
                //    si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_REQ", "NO_LINE", "CD_PLANT", "CD_ITEM", "QT_REQ" };
                //    si.SpParamsDelete = new string[] { "CD_COMPANY", "NO_REQ", "NO_LINE" };
                //    si.SpParamsValues.Add(ActionState.Insert, "NO_REQ", NO_REQ);
                //    si.SpParamsValues.Add(ActionState.Delete, "NO_REQ", NO_REQ);
                //    sic.Add(si);
                //}
            }

            if (YN_SPECIAL == "Y" && dtL != null) //특채수량 사용시..
            {
                SpInfo siSpecial = new SpInfo();
                siSpecial.DataValue = dtL;
                siSpecial.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siSpecial.SpNameInsert = "UP_PU_REV_UPDATE_SPECIAL";
                siSpecial.SpParamsInsert = new string[] { "CD_COMPANY", "NO_REV", "NO_REVLINE", "JAN_QT_PASS", "JAN_QT_SPECIAL" };
                sic.Add(siSpecial);

            }

            if (dtLOT != null)
            {
                SpInfo si03 = new SpInfo();

                dtLOT.RemotingFormat = SerializationFormat.Binary;

                si03.DataValue = dtLOT;
                //si03.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;

                si03.SpNameInsert = "UP_MM_QTIOLOT_INSERT";
                si03.SpNameUpdate = "UP_MM_QTIOLOT_UPDATE";
                si03.SpNameDelete = "UP_MM_QTIOLOT_DELETE";
                si03.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                /*QT_REJECT(불량수량) , QT_INSP(검사수량)*/
                si03.SpParamsInsert = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_LOT", "CD_ITEM", "DT_IO", "FG_PS",
                                                     "FG_IO", "CD_QTIOTP", "CD_SL", "QT_IO", "NO_IO", "NO_IOLINE", "NO_IOLINE2", 
                                                     "YN_RETURN","CD_PLANT_PR", "NO_IO_PR", "NO_LINE_IO_PR", "NO_LINE_IO2_PR", "FG_SLIP_PR", "NO_LOT_PR", 
                                                     "P_NO_SO", "DT_LIMIT", "DC_LOTRMK", "CD_PLANT", "ROOT_NO_LOT", "ID_INSERT","BEF_NO_LOT","FG_LOT_ADD", "BARCODE",
                                                     "CD_MNG1","CD_MNG2","CD_MNG3","CD_MNG4","CD_MNG5","CD_MNG6","CD_MNG7","CD_MNG8","CD_MNG9","CD_MNG10","CD_MNG11",
                                                     "CD_MNG12","CD_MNG13","CD_MNG14","CD_MNG15","CD_MNG16","CD_MNG17","CD_MNG18","CD_MNG19","CD_MNG20"};
                si03.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_LOT", "QT_IO", "QT_IO_OLD" };
                si03.SpParamsDelete = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_IOLINE2", "NO_LOT" };
                si03.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", dtH.Rows[0]["YN_RETURN"].ToString());
                si03.SpParamsValues.Add(ActionState.Insert, "CD_PLANT_PR", "");
                si03.SpParamsValues.Add(ActionState.Insert, "NO_IO_PR", "");
                si03.SpParamsValues.Add(ActionState.Insert, "NO_LINE_IO_PR", 0);
                si03.SpParamsValues.Add(ActionState.Insert, "NO_LINE_IO2_PR", 0);
                si03.SpParamsValues.Add(ActionState.Insert, "FG_SLIP_PR", "");
                si03.SpParamsValues.Add(ActionState.Insert, "NO_LOT_PR", "");
                si03.SpParamsValues.Add(ActionState.Insert, "P_NO_SO", "");
                if (!dtLOT.Columns.Contains("CD_PLANT"))
                    si03.SpParamsValues.Add(ActionState.Insert, "CD_PLANT", "");
                if (!dtLOT.Columns.Contains("ROOT_NO_LOT"))
                    si03.SpParamsValues.Add(ActionState.Insert, "ROOT_NO_LOT", "");
                if (!dtLOT.Columns.Contains("ID_INSERT"))
                    si03.SpParamsValues.Add(ActionState.Insert, "ID_INSERT", "");
                if (!dtLOT.Columns.Contains("FG_LOT_ADD"))
                    si03.SpParamsValues.Add(ActionState.Insert, "FG_LOT_ADD", "");
                if (!dtLOT.Columns.Contains("BARCODE"))
                    si03.SpParamsValues.Add(ActionState.Insert, "BARCODE", "");
                if (!dtLOT.Columns.Contains("BEF_NO_LOT"))
                    si03.SpParamsValues.Add(ActionState.Insert, "BEF_NO_LOT", "N");

                sic.Add(si03);
            }

            if (dtSERL != null)
            {
                SpInfo si04 = new SpInfo();
                si04.DataValue = dtSERL;
                si04.UserID = Global.MainFrame.LoginInfo.UserID;
                si04.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;

                si04.SpNameInsert = "UP_MM_QTIODS_INSERT";
                si04.SpNameUpdate = "UP_MM_QTIODS_UPDATE";
                si04.SpNameDelete = "UP_MM_QTIODS_DELETE";
                si04.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si04.SpParamsInsert = new string[] { 
	            "CD_COMPANY", "NO_SERIAL", "NO_IO", "NO_IOLINE", "CD_ITEM", "CD_QTIOTP", "FG_IO",
		        "CD_MNG1",	"CD_MNG2",	"CD_MNG3",	"CD_MNG4",	"CD_MNG5",	"CD_MNG6",	"CD_MNG7",	"CD_MNG8",	"CD_MNG9",	"CD_MNG10",
		        "CD_MNG11",	"CD_MNG12",	"CD_MNG13",	"CD_MNG14",	"CD_MNG15",	"CD_MNG16",	"CD_MNG17",	"CD_MNG18",	"CD_MNG19",	"CD_MNG20", "CD_PLANT", "ID_INSERT"
                };
                si04.SpParamsUpdate = new string[] { 
	            "CD_COMPANY", "NO_SERIAL", "NO_IO", "NO_IOLINE", "CD_ITEM", "CD_QTIOTP", "FG_IO",
		        "CD_MNG1",	"CD_MNG2",	"CD_MNG3",	"CD_MNG4",	"CD_MNG5",	"CD_MNG6",	"CD_MNG7",	"CD_MNG8",	"CD_MNG9",	"CD_MNG10",
		        "CD_MNG11",	"CD_MNG12",	"CD_MNG13",	"CD_MNG14",	"CD_MNG15",	"CD_MNG16",	"CD_MNG17",	"CD_MNG18",	"CD_MNG19",	"CD_MNG20"
                };
                si04.SpParamsDelete = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_SERIAL" };

                si04.SpParamsValues.Add(ActionState.Insert, "CD_PLANT", CD_PLANT);
                sic.Add(si04);
            }

            if (dtLOCATION != null && dtLOCATION.Rows.Count > 0)
            {
                SpInfo si05 = new SpInfo();
                si05.DataValue = dtLOCATION;
                si05.DataState = DataValueState.Added;
                si05.SpNameInsert = "UP_MM_QTIO_LOCATION_I";
                si05.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si05.SpParamsInsert = new string[] { 
	            "CD_COMPANY", "NO_IO", "NO_IOLINE", "CD_LOCATION", "CD_ITEM", "DT_IO", "FG_PS", "FG_IO", "CD_QTIOTP","CD_PLANT", "CD_SL", "QT_IO_LOCATION", "YN_RETURN"};

                sic.Add(si05);
            }

            if (dtHH != null)
            {
                SpInfo si06 = new SpInfo();
                si06.DataValue = dtHH;
                si06.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si06.UserID = Global.MainFrame.LoginInfo.UserID;
                si06.SpNameInsert = "UP_PU_MM_QTIOH_INSERT";
                si06.SpParamsInsert = new String[] { "NO_IO", "CD_COMPANY", "CD_PLANT", "CD_PARTNER", "FG_TRANS", "YN_RETURN", "DT_IO", "GI_PARTNER", "CD_DEPT", "NO_EMP",  "DC_RMK", "ID_INSERT", "CD_QTIOTP" };
                sic.Add(si06);
            }
            if (dtLL != null)
            {
                SpInfo si07 = new SpInfo();
                si07.DataValue = dtLL;
                si07.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si07.UserID = Global.MainFrame.LoginInfo.UserID;
                si07.SpNameInsert = "UP_PU_REQ_MATL_I";
                si07.SpNameDelete = "UP_PU_REQ_MATL_D";
                si07.SpParamsInsert = new string[] { "YN_RETURN","NO_IO","NO_IOLINE", "CD_COMPANY","CD_PLANT","CD_SL","DT_IO", "NO_ISURCV","NO_ISURCVLINE",
                                                    "NO_PO","NO_POLINE","NO_PO_MAL_LINE","CD_PARTNER", "CD_MATL","QT_NEED", "CD_PJT","GI_PARTNER","NO_EMP","CD_GROUP",
                                                    "CD_BIN_REF","FG_IO","CD_QTIOTP","NO_IO_MGMT","NO_IOLINE_MGMT","YN_BF_ALL","DC_RMK","CD_ITEM","SEQ_PROJECT"};
                si07.SpParamsDelete = new string[] {"CD_COMPANY","NO_IO","NO_IOLINE" };


                si07.SpParamsValues.Add(ActionState.Insert, "CD_BIN_REF", "99");
                si07.SpParamsValues.Add(ActionState.Insert, "NO_ISURCV", "");
                si07.SpParamsValues.Add(ActionState.Insert, "NO_ISURCVLINE", 0);
                si07.SpParamsValues.Add(ActionState.Insert, "YN_BF_ALL", "");

                sic.Add(si07);
            }

            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sic);

            for (int i = 0; i < rtn.Length; i++)
                if (!rtn[i].Result) return false;

            return true;
        }



        #endregion

        #region -> Delete

        public void Delete(object[] p_param1, object[] p_param2)
        {
            Global.MainFrame.ExecSp("UP_PU_RCVH_DELETE_1", p_param1);
            
            ////20110406 최창종
            //if (D.GetString(BASIC.GetMAEXC("구매입고등록_검사")) == "200" || D.GetString(BASIC.GetMAEXC("구매입고등록_검사")) == "200")
            //{
            //    Global.MainFrame.ExecSp("UP_MM_QC_DELETE", p_param2);
            //}
        }

        #endregion

        #region -> 환경설정정보조회
        public string EnvSearch()
        {
            string ls_ContEdit = "N";
            string SelectQuery = "SELECT CD_TP " +
                                 "  FROM PU_ENV " +
                                 " WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' " +
                                 "   AND FG_TP = '001' "
                                 ;

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["CD_TP"] != System.DBNull.Value && dt.Rows[0]["CD_TP"].ToString().Trim() != String.Empty)
                {
                    ls_ContEdit = dt.Rows[0]["CD_TP"].ToString();
                }
            }

            return ls_ContEdit;
        }
        #endregion

        #region -> 구매품의자 메일정보 가져오기
        internal DataTable getMail_Adress(DataTable no_po)
        {
            DataTable dt = no_po.Clone();
            foreach (DataRow dr in no_po.Rows)
            {
                string SelectQuery = @"SELECT   PA.EMP_WRITE,
                                                ME.NO_EMP,
                                                ME.NM_KOR,
                                                ME.NO_EMAIL
                                   FROM         PU_POL PP INNER JOIN PU_APPH PA ON PP.NO_APP = PA.NO_APP AND PP.CD_COMPANY = PA.CD_COMPANY 
                                                          INNER JOIN MA_EMP ME ON PA.EMP_WRITE = ME.NO_EMP AND PA.CD_COMPANY = ME.CD_COMPANY
                                   WHERE        PA.CD_COMPANY ='" + Global.MainFrame.LoginInfo.CompanyCode + @"'
                                   AND          PP.NO_PO ='" + D.GetString(dr["NO_PO"]) + "'";
                DataTable merge = Global.MainFrame.FillDataTable(SelectQuery);
                if (merge != null && merge.Rows.Count != 0)
                    dt.Merge(merge);
            }

            return dt;
        }
        #endregion

        #region -> ICD 구매요청 메일정보 가져오기
        internal DataTable getMail_Adress_ICD(DataTable no_po)
        {
            DataTable dt = no_po.Clone();
            foreach (DataRow dr in no_po.Rows)
            {
                string SelectQuery = @"SELECT 
                                                ME.NO_EMP,
                                                ME.NM_KOR,
                                                ME.NO_EMAIL
                                   FROM         PU_POL PP INNER JOIN PU_PRL PA ON PP.NO_PR = PA.NO_PR AND PP.NO_PRLINE = PA.NO_PRLINE AND PP.CD_COMPANY = PA.CD_COMPANY 
                                                          INNER JOIN MA_EMP ME ON PA.CD_USERDEF4 = ME.NO_EMP AND PA.CD_COMPANY = ME.CD_COMPANY
                                   WHERE        PA.CD_COMPANY ='" + Global.MainFrame.LoginInfo.CompanyCode + @"'
                                   AND          PP.NO_PO ='"+ D.GetString(dr["NO_PO"]) + @"'
                                   AND          PP.NO_LINE ='" + D.GetString(dr["NO_POLINE"]) + @"'
                                   GROUP BY     ME.NO_EMP, ME.NM_KOR, ME.NO_EMAIL";

                DataTable merge = Global.MainFrame.FillDataTable(SelectQuery);
                if (merge != null && merge.Rows.Count != 0)
                    dt.Merge(merge);
            }

            return dt;
        }
        #endregion

        #region -> 출고번호 가지고오기
        public string getNO_IO(string NO_IO)
        {
            string NO_IO_GI = string.Empty;
            string SelectQuery = "SELECT MAX(NO_IO) AS NO_IO " +
                                 "  FROM MM_QTIO "+
                                 " WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' " +
                                 "   AND NO_IO_MGMT = '" + NO_IO + "'" +
                                 "   AND CD_BIN_REF = '99'"
                                 ;

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["NO_IO"] != System.DBNull.Value && dt.Rows[0]["NO_IO"].ToString().Trim() != String.Empty)
                {
                    NO_IO_GI = dt.Rows[0]["NO_IO"].ToString();
                }
            }

            return NO_IO_GI;
        }

        public string getNO_IO_MGMT(string NO_ISURCV)
        {
            string NO_IO_GI = string.Empty;
            string SelectQuery = "SELECT MAX(NO_IO) AS NO_IO " +
                                 "  FROM MM_QTIO " +
                                 " WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' " +
                                 "   AND NO_ISURCV = '" + NO_ISURCV + "'"
                                 ;

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["NO_IO"] != System.DBNull.Value && dt.Rows[0]["NO_IO"].ToString().Trim() != String.Empty)
                {
                    NO_IO_GI = dt.Rows[0]["NO_IO"].ToString();
                }
            }

            return NO_IO_GI;
        }
       
        #endregion

        #region -> 외주처창고
        public string getCD_SL(string CD_PARTNER, string CD_PLANT)
        {
            string NO_IO_GI = string.Empty;
            string SelectQuery = "SELECT MAX(CD_SL) AS CD_SL " +
                                 "  FROM SU_PARTSL" +
                                 " WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' " +
                                 "   AND CD_PARTNER = '" + CD_PARTNER + "'" +
                                 "   AND CD_PLANT = '" + CD_PLANT + "'"
                                 ;

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["CD_SL"] != System.DBNull.Value && dt.Rows[0]["CD_SL"].ToString().Trim() != String.Empty)
                {
                    NO_IO_GI = dt.Rows[0]["CD_SL"].ToString();
                }
            }

            return NO_IO_GI;
        }

        #endregion

        #region -> 데이터테이블 초기화
        internal DataSet Initial_DataSet() 
        {
            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            ds.Tables[0].Columns.Add("S", typeof(string));
            ds.Tables[0].Columns.Add("NO_RCV", typeof(string));
            ds.Tables[0].Columns.Add("CD_PLANT", typeof(string));
            ds.Tables[0].Columns.Add("CD_PARTNER", typeof(string));
            ds.Tables[0].Columns.Add("DT_REQ", typeof(string));
            ds.Tables[0].Columns.Add("NO_EMP", typeof(string));
            ds.Tables[0].Columns.Add("FG_TRANS", typeof(string));
            ds.Tables[0].Columns.Add("FG_PROCESS", typeof(string));
            ds.Tables[0].Columns.Add("CD_EXCH", typeof(string));
            ds.Tables[0].Columns.Add("CD_SL", typeof(string));
            ds.Tables[0].Columns.Add("YN_RETURN", typeof(string));
            ds.Tables[0].Columns.Add("YN_AM", typeof(string));
            ds.Tables[0].Columns.Add("DC_RMK", typeof(string));
            ds.Tables[0].Columns.Add("YN_SUBCON", typeof(string));
            ds.Tables[0].Columns.Add("CD_DEPT", typeof(string));
            ds.Tables[0].Columns.Add("NM_PLANT", typeof(string));
            ds.Tables[0].Columns.Add("LN_PARTNER", typeof(string));
            ds.Tables[0].Columns.Add("NM_KOR", typeof(string));
            ds.Tables[0].Columns.Add("NM_SL", typeof(string));
            ds.Tables[0].Columns.Add("NM_EXCH", typeof(string));
            ds.Tables[0].Columns.Add("FG_RCV", typeof(string));
            ds.Tables[0].Columns.Add("NM_FG_RCV", typeof(string));
            ds.Tables[0].Columns.Add("YN_AUTORCV", typeof(string));
            ds.Tables[0].Columns.Add("DC_RMK_TEXT", typeof(string));

            ds.Tables[1].Columns.Add("S",typeof(string));
            ds.Tables[1].Columns.Add("NO_IO",typeof(string));
            ds.Tables[1].Columns.Add("NO_RCV",typeof(string));
            ds.Tables[1].Columns.Add("NO_LINE", typeof(decimal));
            ds.Tables[1].Columns.Add("CD_PURGRP",typeof(string));
            ds.Tables[1].Columns.Add("QT_REQ", typeof(decimal));
            ds.Tables[1].Columns.Add("QT_GOOD_INV", typeof(decimal));
            ds.Tables[1].Columns.Add("DT_LIMIT",typeof(string));
            ds.Tables[1].Columns.Add("DT_GRLAST",typeof(string));
            ds.Tables[1].Columns.Add("CD_ITEM",typeof(string));
            ds.Tables[1].Columns.Add("QT_PASS", typeof(decimal));
            ds.Tables[1].Columns.Add("YN_INSP",typeof(string));
            ds.Tables[1].Columns.Add("QT_REJECTION", typeof(decimal));
            ds.Tables[1].Columns.Add("QT_GR", typeof(decimal));
            ds.Tables[1].Columns.Add("CD_UNIT_MM",typeof(string));
            ds.Tables[1].Columns.Add("CD_ZONE", typeof(string));
            ds.Tables[1].Columns.Add("QT_REQ_MM", typeof(decimal));
            ds.Tables[1].Columns.Add("QT_GR_MM", typeof(decimal));
            ds.Tables[1].Columns.Add("CD_EXCH",typeof(string));
            ds.Tables[1].Columns.Add("RT_EXCH", typeof(decimal));
            ds.Tables[1].Columns.Add("UM_EX_PO", typeof(decimal));
            ds.Tables[1].Columns.Add("UM_EX", typeof(decimal));
            ds.Tables[1].Columns.Add("UM", typeof(decimal));
            ds.Tables[1].Columns.Add("AM_EX", typeof(decimal));
            ds.Tables[1].Columns.Add("AM_EXREQ", typeof(decimal));
            ds.Tables[1].Columns.Add("AM", typeof(decimal));
            ds.Tables[1].Columns.Add("AM_REQ", typeof(decimal));
            ds.Tables[1].Columns.Add("VAT", typeof(decimal));
            ds.Tables[1].Columns.Add("AM_EXRCV", typeof(decimal));
            ds.Tables[1].Columns.Add("AM_RCV", typeof(decimal));
            ds.Tables[1].Columns.Add("RT_CUSTOMS", typeof(decimal));
            ds.Tables[1].Columns.Add("CD_PJT",typeof(string));
            ds.Tables[1].Columns.Add("YN_PURCHASE",typeof(string));
            ds.Tables[1].Columns.Add("YN_RETURN",typeof(string));
            ds.Tables[1].Columns.Add("FG_TPPURCHASE",typeof(string));
            ds.Tables[1].Columns.Add("FG_RCV",typeof(string));
            ds.Tables[1].Columns.Add("FG_TRANS",typeof(string));
            ds.Tables[1].Columns.Add("FG_TAX",typeof(string));
            ds.Tables[1].Columns.Add("FG_TAXP",typeof(string));
            ds.Tables[1].Columns.Add("YN_AUTORCV",typeof(string));
            ds.Tables[1].Columns.Add("YN_REQ",typeof(string));
            ds.Tables[1].Columns.Add("CD_SL",typeof(string));
            ds.Tables[1].Columns.Add("NO_LC",typeof(string));
            ds.Tables[1].Columns.Add("NO_LCLINE", typeof(decimal));
            ds.Tables[1].Columns.Add("RT_SPEC", typeof(decimal));
            ds.Tables[1].Columns.Add("NO_EMP",typeof(string));
            ds.Tables[1].Columns.Add("NO_PO",typeof(string));
            ds.Tables[1].Columns.Add("NO_POLINE", typeof(decimal));
            ds.Tables[1].Columns.Add("NO_IO_MGMT",typeof(string));
            ds.Tables[1].Columns.Add("NO_IOLINE_MGMT", typeof(decimal));
            ds.Tables[1].Columns.Add("NO_PO_MGMT",typeof(string));
            ds.Tables[1].Columns.Add("NO_POLINE_MGMT", typeof(decimal));
            ds.Tables[1].Columns.Add("YN_AM",typeof(string));
            ds.Tables[1].Columns.Add("CD_PLANT",typeof(string));
            ds.Tables[1].Columns.Add("CD_PARTNER",typeof(string));
            ds.Tables[1].Columns.Add("LN_PARTNER",typeof(string));
            ds.Tables[1].Columns.Add("DT_REQ",typeof(string));
            ds.Tables[1].Columns.Add("NO_TO",typeof(string));
            ds.Tables[1].Columns.Add("NO_TO_LINE", typeof(string));
            ds.Tables[1].Columns.Add("RATE_EXCHG", typeof(decimal));
            ds.Tables[1].Columns.Add("RT_VAT", typeof(string));
            ds.Tables[1].Columns.Add("VAT_CLS", typeof(decimal));
            ds.Tables[1].Columns.Add("DC_RMK",typeof(string));
            ds.Tables[1].Columns.Add("NM_ITEM",typeof(string));
            ds.Tables[1].Columns.Add("STND_ITEM",typeof(string));
            ds.Tables[1].Columns.Add("UNIT_IM",typeof(string));
            ds.Tables[1].Columns.Add("NO_LOT",typeof(string));
            ds.Tables[1].Columns.Add("NM_SL",typeof(string));
            ds.Tables[1].Columns.Add("NM_FG_RCV",typeof(string));
            ds.Tables[1].Columns.Add("NM_PROJECT", typeof(string));
            ds.Tables[1].Columns.Add("NM_KOR",typeof(string));
            ds.Tables[1].Columns.Add("NM_SYSDEF", typeof(string));
            ds.Tables[1].Columns.Add("RATE_ITEM",typeof(string));
            ds.Tables[1].Columns.Add("FG_POST",typeof(string));
            ds.Tables[1].Columns.Add("NM_FG_POST",typeof(string));
            ds.Tables[1].Columns.Add("NO_NUM",typeof(string));
            ds.Tables[1].Columns.Add("QT_REAL",typeof(Int32));
            ds.Tables[1].Columns.Add("NO_IO1",typeof(string));
            ds.Tables[1].Columns.Add("NO_IOLINE", typeof(Int32));
            ds.Tables[1].Columns.Add("DT_IO",typeof(string));
            ds.Tables[1].Columns.Add("FG_IO",typeof(string));
            ds.Tables[1].Columns.Add("CD_QTIOTP",typeof(string));
            ds.Tables[1].Columns.Add("NM_QTIOTP",typeof(string));
            ds.Tables[1].Columns.Add("NO_BL",typeof(string));
            ds.Tables[1].Columns.Add("NO_BLLINE", typeof(Int32));
            ds.Tables[1].Columns.Add("NM_PURGRP",typeof(string));
            ds.Tables[1].Columns.Add("USE_YN",typeof(string));
            ds.Tables[1].Columns.Add("PO_PRICE",typeof(string));
            ds.Tables[1].Columns.Add("PO_UNIT",typeof(string));
            ds.Tables[1].Columns.Add("TP_PURPRICE",typeof(string));
            ds.Tables[1].Columns.Add("NO_REV",typeof(string));
            ds.Tables[1].Columns.Add("NO_REVLINE", typeof(decimal));
            ds.Tables[1].Columns.Add("QT_REQ_M", typeof(Int32));
            ds.Tables[1].Columns.Add("AM_TOTAL", typeof(decimal));
            ds.Tables[1].Columns.Add("NO_SERL",typeof(string));
            ds.Tables[1].Columns.Add("QT_CLS", typeof(decimal));
            ds.Tables[1].Columns.Add("CD_WH", typeof(string));
            ds.Tables[1].Columns.Add("DC_RMK2", typeof(string));

            ds.Tables[1].Columns.Add("REV_QT_REQ_MM", typeof(decimal));         // 2011-06-08, 최승애 추가
            ds.Tables[1].Columns.Add("REV_AM_EXREQ", typeof(decimal));          // 2011-06-08, 최승애 추가
            ds.Tables[1].Columns.Add("REV_AM_REQ", typeof(decimal));            // 2011-06-08, 최승애 추가


            //프로젝트형 추가
            ds.Tables[1].Columns.Add("CD_PJT_ITEM", typeof(string));         
            ds.Tables[1].Columns.Add("NM_PJT_ITEM", typeof(string));         
            ds.Tables[1].Columns.Add("PJT_ITEM_STND", typeof(string));         
            ds.Tables[1].Columns.Add("NO_WBS", typeof(string));         
            ds.Tables[1].Columns.Add("NO_CBS", typeof(string));         
            ds.Tables[1].Columns.Add("SEQ_PROJECT", typeof(decimal)); 
            ds.Tables[1].Columns.Add("TP_UM_TAX", typeof(string));

            // 관련항목추가(발주적용에서만 사용)
            ds.Tables[1].Columns.Add("CD_ITEM_ORIGIN", typeof(string));          // 2012-03-14, 
            ds.Tables[1].Columns.Add("NM_ITEM_ORIGIN", typeof(string));            // 2012-03-14, 
            ds.Tables[1].Columns.Add("STND_ITEM_ORIGIN", typeof(string));

            ds.Tables[1].Columns.Add("REV_QT_PASS", typeof(decimal));         // 2012-03-14, 
            ds.Tables[1].Columns.Add("REV_QT_REV_MM", typeof(decimal));          // 2012-03-14, 

            ds.Tables[1].Columns.Add("GI_PARTNER", typeof(string));
            ds.Tables[1].Columns.Add("NM_GI_PARTER", typeof(string));

            ds.Tables[1].Columns.Add("JAN_QT_SPECIAL", typeof(decimal));
            ds.Tables[1].Columns.Add("JAN_QT_PASS", typeof(decimal));
            ds.Tables[1].Columns.Add("FG_SPECIAL", typeof(string));
            ds.Tables[1].Columns.Add("NM_USERDEF1", typeof(string));
            ds.Tables[1].Columns.Add("NM_USERDEF2", typeof(string));
            ds.Tables[1].Columns.Add("DT_PLAN", typeof(string));
            ds.Tables[1].Columns.Add("CLS_ITEM", typeof(string));
            ds.Tables[1].Columns.Add("DATE_USERDEF1", typeof(string));
            ds.Tables[1].Columns.Add("CDSL_USERDEF1", typeof(string));
            ds.Tables[1].Columns.Add("NMSL_USERDEF1", typeof(string));
            ds.Tables[1].Columns.Add("NUM_USERDEF1", typeof(decimal));
            ds.Tables[1].Columns.Add("NUM_USERDEF2", typeof(decimal));

            ds.Tables[1].Columns.Add("PI_PARTNER", typeof(string));
            ds.Tables[1].Columns.Add("PI_LN_PARTNER", typeof(string));

            ds.Tables[1].Columns.Add("UM_WEIGHT", typeof(decimal));
            ds.Tables[1].Columns.Add("TOT_WEIGHT", typeof(decimal));
            ds.Tables[1].Columns.Add("WEIGHT", typeof(decimal));
            ds.Tables[1].Columns.Add("MAT_ITEM", typeof(string));


            if (Global.MainFrame.ServerKeyCommon == "UNIPOINT")
            {
                ds.Tables[1].Columns.Add("CD_PARTNER_PJT", typeof(string));
                ds.Tables[1].Columns.Add("LN_PARTNER_PJT", typeof(string));
                ds.Tables[1].Columns.Add("NO_EMP_PJT", typeof(string));
                ds.Tables[1].Columns.Add("NM_KOR_PJT", typeof(string));
                ds.Tables[1].Columns.Add("END_USER", typeof(string));
            } 

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



            ds.Tables[0].Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            ds.Tables[0].Columns["DT_REQ"].DefaultValue = Global.MainFrame.GetStringToday;
            ds.Tables[0].Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            ds.Tables[0].Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            ds.Tables[0].Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;
            ds.Tables[0].Columns["FG_TRANS"].DefaultValue = "001";
            ds.Tables[0].Columns["FG_PROCESS"].DefaultValue = "001";
            ds.Tables[0].Columns["YN_AUTORCV"].DefaultValue = "Y";

            return ds;
        }
        #endregion

        #region -> 바코드가입고번호조회
        public DataTable search_barcode_rev(object[] obj)
        {
            DataTable dt = new DataTable();
            dt = DBHelper.GetDataTable("UP_PU_REQ_REG_1_BARCODE_REV_S", obj);

            return dt;
        }

        public DataTable search_barcode_iqc(object[] obj)
        {
            DataTable dt = new DataTable();
            dt = DBHelper.GetDataTable("UP_PU_REQ_REG_1_BARCODE_IQC_S", obj);

            return dt;
        }

        public string strTelcon(string NO_REV)
        {
            string strReturn = "NOT";
            string SelectQuery = "SELECT FG_QC " +
                                 "  FROM PU_REV " +
                                 " WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' " +
                                 "   AND NO_REV = '" + NO_REV + "'"
                                 ;

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["FG_QC"] != System.DBNull.Value && dt.Rows[0]["FG_QC"].ToString().Trim() != String.Empty)
                {
                    strReturn = D.GetString(dt.Rows[0]["FG_QC"]);
                }
            }

            return strReturn;
        }



        #endregion
    }
}
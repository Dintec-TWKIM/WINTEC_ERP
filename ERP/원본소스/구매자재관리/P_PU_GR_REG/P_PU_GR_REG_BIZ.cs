using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Dass.FlexGrid;
using Duzon.ERPU; 

namespace pur
{
    class P_PU_GR_REG_BIZ
    {
        string 구매입고등록_검사 = "000";

        public P_PU_GR_REG_BIZ()
        {
            구매입고등록_검사 = BASIC.GetMAEXC("구매입고등록_검사");
        }


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

        public DataTable Search_Main(string DT_FROM, string DT_TO, string CD_PLANT, string CD_PARTNER,string NO_EMP, string CD_SL, string FG_RCV, string CD_PJT )
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_GR_SELECT_H";
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.SpParamsSelect = new string[] { Global.MainFrame.LoginInfo.CompanyCode, DT_FROM, DT_TO, CD_PLANT, CD_PARTNER, NO_EMP, CD_SL, FG_RCV, CD_PJT };
            ResultData resultdata = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)resultdata.DataValue;

            return dt;
        }

        public DataTable Search_Detail(string NO_RCV,string CD_PJT )
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_PU_GR_SELECT_L";
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.SpParamsSelect = new string[] { Global.MainFrame.LoginInfo.CompanyCode, NO_RCV, CD_PJT };
            ResultData resultdata = (ResultData)Global.MainFrame.FillDataTable(si); 
            DataTable dt = (DataTable)resultdata.DataValue;

            return dt;
        }

        #endregion

        #region -> Save

        public bool Save(DataTable dt_Qtio, DataTable dtD, DataTable dtLOT, DataTable dtSERL, DataTable dt_location)
        {
            SpInfoCollection sic = new SpInfoCollection();
            
            SpInfo si_Qtio = new SpInfo();
            si_Qtio.DataState = DataValueState.Added;
            si_Qtio.DataValue = dt_Qtio;
            si_Qtio.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si_Qtio.UserID    = Global.MainFrame.LoginInfo.EmployeeNo;
            si_Qtio.SpNameInsert = "UP_PU_MM_QTIOH_INSERT";

            si_Qtio.SpParamsInsert = new string[] { "NO_IO", "CD_COMPANY", "CD_PLANT", "CD_PARTNER", "FG_TRANS", "YN_RETURN", "DT_IO", "GI_PARTNER", "CD_DEPT", "NO_EMP", "DC_RMK", "ID_INSERT", "CD_QTIOTP" };
            
            si_Qtio.SpParamsValues.Add(ActionState.Insert, "GI_PARTNER", "");
            si_Qtio.SpParamsValues.Add(ActionState.Insert, "CD_DEPT", "");

            sic.Add(si_Qtio);

            SpInfo si_Gr = new SpInfo();
            si_Gr.DataState = DataValueState.Added;
            si_Gr.DataValue = dtD;
            si_Gr.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si_Gr.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
            si_Gr.SpNameInsert = "UP_PU_GR_INSERT";

            si_Gr.SpParamsInsert = new string[] { "YN_RETURN", "NO_IO","NO_IOLINE", "CD_COMPANY", "CD_PLANT", "CD_SL", "DT_IO", "NO_RCV", "NO_LINE", "NO_PO", "NO_POLINE", "FG_PS", 
												  "FG_TPPURCHASE", "FG_IO", "CD_QTIOTP", "FG_TRANS", "FG_TAX", "CD_PARTNER","CD_ITEM", "QT_GOOD_INV","QT_REJECT_REQ", "CD_EXCH", "RT_EXCH", "UM_EX", "UM", "AM_EX","AM","VAT", "FG_TAXP",
												  "YN_AM", "CD_PJT", "NO_LC", "NO_LCLINE", "NO_EMP", "CD_PURGRP","CD_UNIT_MM", "QT_REQ_RCV","QT_REJECT_REQ_MM", "UM_EX_PO","YN_INSP", "YN_PURCHASE", "DC_RMK","CD_WH","SEQ_PROJECT", "NO_WBS","NO_CBS","TP_UM_TAX"};
            si_Gr.SpParamsValues.Add(ActionState.Insert, "FG_PS", "1"); 
            //si_Gr.SpParamsValues.Add(ActionState.Insert, "SEQ_PROJECT", 0);
            si_Gr.SpParamsValues.Add(ActionState.Insert, "NO_WBS", "");
            si_Gr.SpParamsValues.Add(ActionState.Insert, "NO_CBS", "");  
            sic.Add(si_Gr);

            if (dtLOT != null)
            {
                SpInfo si03 = new SpInfo();
                si03.DataValue = dtLOT;
                //si03.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;

                si03.SpNameInsert = "UP_MM_QTIOLOT_INSERT";
                si03.SpNameUpdate = "UP_MM_QTIOLOT_UPDATE";
                si03.SpNameDelete = "UP_MM_QTIOLOT_DELETE";
                si03.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                /*QT_REJECT(불량수량) , QT_INSP(검사수량)*/
                si03.SpParamsInsert = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_LOT", "CD_ITEM", "DT_IO", "FG_PS",
                                                     "FG_IO", "CD_QTIOTP", "CD_SL", "QT_IO", "NO_IO", "NO_IOLINE", "NO_IOLINE2", "YN_RETURN", "CD_PLANT_PR", "NO_IO_PR", "NO_LINE_IO_PR", "NO_LINE_IO2_PR", "FG_SLIP_PR", "NO_LOT_PR", "P_NO_SO", "DT_LIMIT", "DC_LOTRMK",
                                                     "P_CD_PLANT", "P_ROOT_NO_LOT", "P_ID_INSERT", "P_BEF_NO_LOT", "P_FG_LOT_ADD", "P_BARCODE",
                                                     "CD_MNG1","CD_MNG2","CD_MNG3","CD_MNG4","CD_MNG5","CD_MNG6","CD_MNG7","CD_MNG8","CD_MNG9","CD_MNG10","CD_MNG11","CD_MNG12","CD_MNG13","CD_MNG14","CD_MNG15","CD_MNG16","CD_MNG17","CD_MNG18","CD_MNG19","CD_MNG20" };
                si03.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_LOT", "QT_IO", "QT_IO_OLD" };
                si03.SpParamsDelete = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_IOLINE2", "NO_LOT" };
                si03.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", dt_Qtio.Rows[0]["YN_RETURN"]);
                si03.SpParamsValues.Add(ActionState.Insert, "CD_PLANT_PR", "");
                si03.SpParamsValues.Add(ActionState.Insert, "NO_IO_PR", "");
                si03.SpParamsValues.Add(ActionState.Insert, "NO_LINE_IO_PR", 0);
                si03.SpParamsValues.Add(ActionState.Insert, "NO_LINE_IO2_PR", 0);
                si03.SpParamsValues.Add(ActionState.Insert, "FG_SLIP_PR", "");
                si03.SpParamsValues.Add(ActionState.Insert, "NO_LOT_PR", "");
                si03.SpParamsValues.Add(ActionState.Insert, "P_NO_SO", "");

                si03.SpParamsValues.Add(ActionState.Insert, "P_CD_PLANT", "");
                si03.SpParamsValues.Add(ActionState.Insert, "P_ROOT_NO_LOT", "");
                si03.SpParamsValues.Add(ActionState.Insert, "P_ID_INSERT", "");
                si03.SpParamsValues.Add(ActionState.Insert, "P_BEF_NO_LOT", "");
                si03.SpParamsValues.Add(ActionState.Insert, "P_FG_LOT_ADD", "N");
                si03.SpParamsValues.Add(ActionState.Insert, "P_BARCODE", "");


                sic.Add(si03);
            }

            if (dtSERL != null)
            {
                SpInfo si04 = new SpInfo();
                si04.DataValue = dtSERL;

                si04.SpNameInsert = "UP_MM_QTIODS_INSERT";
                si04.SpNameUpdate = "UP_MM_QTIODS_UPDATE";
                si04.SpNameDelete = "UP_MM_QTIODS_DELETE";
                si04.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si04.SpParamsInsert = new string[] { 
	            "CD_COMPANY", "NO_SERIAL", "NO_IO", "NO_IOLINE", "CD_ITEM", "CD_QTIOTP", "FG_IO",
		        "CD_MNG1",	"CD_MNG2",	"CD_MNG3",	"CD_MNG4",	"CD_MNG5",	"CD_MNG6",	"CD_MNG7",	"CD_MNG8",	"CD_MNG9",	"CD_MNG10",
		        "CD_MNG11",	"CD_MNG12",	"CD_MNG13",	"CD_MNG14",	"CD_MNG15",	"CD_MNG16",	"CD_MNG17",	"CD_MNG18",	"CD_MNG19",	"CD_MNG20", "CD_PLANT","ID_INSERT"
                };
                si04.SpParamsUpdate = new string[] { 
	            "CD_COMPANY", "NO_SERIAL", "NO_IO", "NO_IOLINE", "CD_ITEM", "CD_QTIOTP", "FG_IO",
		        "CD_MNG1",	"CD_MNG2",	"CD_MNG3",	"CD_MNG4",	"CD_MNG5",	"CD_MNG6",	"CD_MNG7",	"CD_MNG8",	"CD_MNG9",	"CD_MNG10",
		        "CD_MNG11",	"CD_MNG12",	"CD_MNG13",	"CD_MNG14",	"CD_MNG15",	"CD_MNG16",	"CD_MNG17",	"CD_MNG18",	"CD_MNG19",	"CD_MNG20"
                };
                si04.SpParamsDelete = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_SERIAL" };

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
                if (dt_location.Columns.Contains("YN_RETURN") == false)
                { si05.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", dtD.Rows[0]["YN_RETURN"].ToString()); }
                sic.Add(si05);
            }


            ResultData[] result = (ResultData[])Global.MainFrame.Save(sic);

            for (int i = 0; i < result.Length; i++)
                if (!result[i].Result) return false;

            return true;
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

        internal string Get구매입고등록_검사 { get { return 구매입고등록_검사; } }

        #endregion

        #region -> 메모  & 체크팬

        internal void SaveContent(ContentType contentType, Dass.FlexGrid.CommandType commandType, string noIo, decimal no_ioline, string value)
        {
            string sqlQuery = string.Empty;
            string columnName = string.Empty;

            if (contentType == ContentType.Memo)
                columnName = "MEMO_CD";
            else if (contentType == ContentType.CheckPen)
                columnName = "CHECK_PEN";

            if (commandType == Dass.FlexGrid.CommandType.Add)
                sqlQuery = "UPDATE PU_RCVL SET " + columnName + " = '" + value + "' WHERE NO_RCV  = '" + noIo + "' AND CD_COMPANY = '" + MA.Login.회사코드 + "'  AND NO_LINE = " + no_ioline;
            else if (commandType == Dass.FlexGrid.CommandType.Delete)
                sqlQuery = "UPDATE PU_RCVL SET " + columnName + " = NULL WHERE NO_RCV  = '" + noIo + "' AND CD_COMPANY = '" + MA.Login.회사코드 + "' AND NO_LINE = " + no_ioline;

            Global.MainFrame.ExecuteScalar(sqlQuery);

        }

        #endregion
    }
}

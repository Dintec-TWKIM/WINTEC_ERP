using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    public class P_CZ_PU_STS_REG_BIZ
    {
        private string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;
        private string 담당자 = Global.MainFrame.LoginInfo.EmployeeNo;
        private string 공장코드 = string.Empty;
        public string biz_cd_plant = string.Empty;

        public DataTable Search_LOT()
        {
            return DBHelper.GetDataTable("UP_PU_MNG_LOT_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode });
        }

        public string Search_SERIAL()
        {
            string str = "N";

            DataTable dataTable = DBHelper.GetDataTable("UP_PU_MNG_SER_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode });

            if (dataTable.Rows.Count > 0)
            {
                str = dataTable.Rows[0]["YN_SERIAL"].ToString();
                if (str == string.Empty)
                    str = "N";
            }

            return str;
        }

        public DataSet Search(string NO_IO, string CD_PLANT, string CD_PJT)
        {
            object[] parameters = new object[] { NO_IO,
                                                 Global.MainFrame.LoginInfo.CompanyCode,
                                                 "",
                                                 Global.SystemLanguage.MultiLanguageLpoint };

            DataSet dataSet = !Global.MainFrame.ServerKeyCommon.Contains("HANSU") ? DBHelper.GetDataSet("UP_PU_STS_SELECT", parameters) : DBHelper.GetDataSet("UP_PU_Z_HANSU_STS_SELECT", parameters);
            dataSet.Tables[1].Columns.Add("FLAG", typeof(string));
            
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.DataType == Type.GetType("System.Decimal"))
                        column.DefaultValue = 0;
                }
            }

            DataTable table1 = dataSet.Tables[0];
            DataTable table2 = dataSet.Tables[1];
            table1.Columns["DT_IO"].DefaultValue = Global.MainFrame.GetStringToday;
            table1.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            table1.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            table1.Columns["CD_PLANT"].DefaultValue = CD_PLANT;
            table1.Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;

            return dataSet;
        }

        public DataSet dt_print(string NO_IO, string SERVERKEY)
        {
            object[] parameters = new object[] { NO_IO,
                                                 Global.MainFrame.LoginInfo.CompanyCode,
                                                 Global.SystemLanguage.MultiLanguageLpoint };

            DataSet dataSet = DBHelper.GetDataSet(!(SERVERKEY == "WJIS") ? (!(SERVERKEY == "SANSUNG") && !(SERVERKEY == "BIOLAND") && !SERVERKEY.Contains("MIRAE") ? "UP_PU_STS_PRINT_S" : "UP_PU_Z_SANSUNG_STS_PRINT_S") : "UP_PU_Z_WJIS_STS_PRINT_S", parameters);
            
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.DataType == Type.GetType("System.Decimal"))
                        column.DefaultValue = 0;
                }
            }

            return dataSet;
        }

        public DataTable Search_요청(string NO_IO)
        {
            return DBHelper.GetDataTable("UP_PU_REQ_SELECT_STS", new object[] { this.회사코드,
                                                                                NO_IO,
                                                                                Global.SystemLanguage.MultiLanguageLpoint });
        }

        public bool Save(DataTable dtH, DataTable dtL, DataTable dtLOT, DataTable dtSERL, DataTable dt_location, DataTable dtQCl)
        {
            SpInfoCollection spCollection = new SpInfoCollection();
            SpInfo spInfo;

            if (dtH != null)
            {
                spInfo = new SpInfo();

                spInfo.DataValue = dtH;
                spInfo.CompanyID = this.회사코드;
                spInfo.UserID = this.담당자;
                spInfo.SpNameInsert = "UP_PU_MM_QTIOH_INSERT";
                spInfo.SpNameUpdate = "UP_PU_MM_QTIOH_UPDATE";

                spInfo.SpParamsInsert = new string[] { "NO_IO",
                                                       "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "CD_PARTNER",
                                                       "FG_TRANS",
                                                       "YN_RETURN",
                                                       "DT_IO",
                                                       "GI_PARTNER",
                                                       "CD_DEPT",
                                                       "NO_EMP",
                                                       "DC_RMK",
                                                       "ID_INSERT",
                                                       "CD_QTIOTP" };

                spInfo.SpParamsUpdate = new string[] { "NO_IO",
                                                       "CD_COMPANY",
                                                       "ID_INSERT",
                                                       "DC_RMK" };

                spCollection.Add(spInfo);
            }

            if (dtL != null)
            {
                this.공장코드 = this.biz_cd_plant;

                spInfo = new SpInfo();

                spInfo.DataValue = dtL;
                spInfo.CompanyID = this.회사코드;
                spInfo.UserID = this.담당자;
                spInfo.SpNameInsert = "UP_PU_STS_INSERT";
                spInfo.SpNameUpdate = "UP_PU_STS_UPDATE";
                spInfo.SpNameDelete = "UP_PU_STS_DELETE";

                spInfo.SpParamsInsert = new string[] { "YN_RETURN",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "CD_COMPANY",
                                                       "CD_PLANT",
                                                       "CD_SL",
                                                       "DT_IO",
                                                       "CD_QTIOTP",
                                                       "CD_ITEM",
                                                       "QT_GOOD_INV",
                                                       "QT_REJECT_INV",
                                                       "NO_EMP",
                                                       "CD_SL_REF",
                                                       "YN_AM",
                                                       "NO_ISURCV",
                                                       "NO_ISURCVLINE",
                                                       "NO_PSO_MGMT",
                                                       "NO_PSOLINE_MGMT",
                                                       "FLAG",
                                                       "NO_GIREQ",
                                                       "NO_LINE",
                                                       "QT_UNIT_PO",
                                                       "P_NO_POP",
                                                       "P_NO_POP_LINE",
                                                       "CD_PARTNER",
                                                       "DC_RMK",
                                                       "CD_PROJECT",
                                                       "SEQ_PROJECT",
                                                       "DC_RMK1",
                                                       "NO_WBS",
                                                       "NO_CBS",
                                                       "NO_IO_MGMT",
                                                       "NO_IOLINE_MGMT",
                                                       "NO_MREQ",
                                                       "NO_MREQLINE",
                                                       "GI_PARTNER",
                                                       "CD_ITEM_REF",
                                                       "NO_TRACK",
                                                       "NO_TRACK_LINE",
                                                       "CD_USERDEF1_QTIO",
                                                       "CD_USERDEF2_QTIO",
                                                       "NM_USERDEF1_QTIO",
                                                       "NM_USERDEF2_QTIO",
                                                       "DATE_USERDEF1_QTIO",
                                                       "NUM_USERDEF1_QTIO",
                                                       "DT_DELIVERY_IO",
                                                       "FG_TRACK" };

                spInfo.SpParamsUpdate = new string[] { "NO_IO",
                                                       "NO_IOLINE",
                                                       "CD_COMPANY",
                                                       "QT_GOOD_OLD",
                                                       "QT_GOOD_INV",
                                                       "DC_RMK",
                                                       "DC_RMK1",
                                                       "NO_MREQ",
                                                       "NO_MREQLINE",
                                                       "CD_USERDEF1_QTIO",
                                                       "CD_USERDEF2_QTIO",
                                                       "NM_USERDEF1_QTIO",
                                                       "NM_USERDEF2_QTIO",
                                                       "DATE_USERDEF1_QTIO",
                                                       "DT_DELIVERY_IO",
                                                       "QT_UNIT_PO" };

                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "NO_MREQ",
                                                       "NO_MREQLINE" };

                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_GIREQ", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_LINE", 0);
                spInfo.SpParamsValues.Add(ActionState.Insert, "P_NO_POP", "");
                spInfo.SpParamsValues.Add(ActionState.Insert, "P_NO_POP_LINE", 0);
                spInfo.SpParamsValues.Add(ActionState.Insert, "CD_ITEM_REF", "");
               
                spCollection.Add(spInfo);
            }

            if (dtLOT != null)
            {
                spInfo = new SpInfo();

                spInfo.DataValue = dtLOT;
                spInfo.DataState = DataValueState.Added;
                spInfo.SpNameInsert = "UP_PU_STS_LOT_INSERT";
                spInfo.SpNameUpdate = "UP_PU_STS_LOT_UPDATE";
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;

                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "출고번호",
                                                       "출고항번",
                                                       "NO_LOT",
                                                       "CD_ITEM",
                                                       "출고일",
                                                       "FG_PS",
                                                       "수불구분",
                                                       "수불형태",
                                                       "창고코드",
                                                       "QT_GOOD_MNG",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "NO_IOLINE2",
                                                       "YN_RETURN",
                                                       "입고창고",
                                                       "DT_LIMIT",
                                                       "CD_MNG1",
                                                       "CD_MNG2",
                                                       "CD_MNG3",
                                                       "CD_MNG4",
                                                       "CD_MNG5",
                                                       "CD_MNG6",
                                                       "CD_MNG7",
                                                       "CD_MNG8",
                                                       "CD_MNG9",
                                                       "CD_MNG10",
                                                       "CD_MNG11",
                                                       "CD_MNG12",
                                                       "CD_MNG13",
                                                       "CD_MNG14",
                                                       "CD_MNG15",
                                                       "CD_MNG16",
                                                       "CD_MNG17",
                                                       "CD_MNG18",
                                                       "CD_MNG19",
                                                       "CD_MNG20",
                                                       "BARCODE" };

                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "출고번호",
                                                       "출고항번",
                                                       "NO_LOT",
                                                       "QT_IO",
                                                       "QT_IO_OLD",
                                                       "DT_LIMIT" };

                spCollection.Add(spInfo);
            }

            if (dtSERL != null)
            {
                spInfo = new SpInfo();

                spInfo.DataValue = dtSERL;
                spInfo.SpNameInsert = "UP_MM_QTIODS_INSERT";
                spInfo.SpNameUpdate = "UP_MM_QTIODS_UPDATE";
                spInfo.SpNameDelete = "UP_MM_QTIODS_DELETE";
                spInfo.CompanyID = this.회사코드;
                spInfo.UserID = this.담당자;

                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "NO_SERIAL",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "CD_ITEM",
                                                       "CD_QTIOTP",
                                                       "FG_IO",
                                                       "CD_MNG1",
                                                       "CD_MNG2",
                                                       "CD_MNG3",
                                                       "CD_MNG4",
                                                       "CD_MNG5",
                                                       "CD_MNG6",
                                                       "CD_MNG7",
                                                       "CD_MNG8",
                                                       "CD_MNG9",
                                                       "CD_MNG10",
                                                       "CD_MNG11",
                                                       "CD_MNG12",
                                                       "CD_MNG13",
                                                       "CD_MNG14",
                                                       "CD_MNG15",
                                                       "CD_MNG16",
                                                       "CD_MNG17",
                                                       "CD_MNG18",
                                                       "CD_MNG19",
                                                       "CD_MNG20",
                                                       "CD_PLANT",
                                                       "ID_INSERT" };

                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "NO_SERIAL",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "CD_ITEM",
                                                       "CD_QTIOTP",
                                                       "FG_IO",
                                                       "CD_MNG1",
                                                       "CD_MNG2",
                                                       "CD_MNG3",
                                                       "CD_MNG4",
                                                       "CD_MNG5",
                                                       "CD_MNG6",
                                                       "CD_MNG7",
                                                       "CD_MNG8",
                                                       "CD_MNG9",
                                                       "CD_MNG10",
                                                       "CD_MNG11",
                                                       "CD_MNG12",
                                                       "CD_MNG13",
                                                       "CD_MNG14",
                                                       "CD_MNG15",
                                                       "CD_MNG16",
                                                       "CD_MNG17",
                                                       "CD_MNG18",
                                                       "CD_MNG19",
                                                       "CD_MNG20" };

                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "NO_SERIAL" };

                spInfo.SpParamsValues.Add(ActionState.Insert, "CD_PLANT", this.공장코드);
                spInfo.SpParamsValues.Add(ActionState.Insert, "ID_INSERT", this.담당자);

                spCollection.Add(spInfo);
            }

            if (dt_location != null && dt_location.Rows.Count > 0)
            {
                spInfo = new SpInfo();

                spInfo.DataValue = dt_location;
                spInfo.DataState = DataValueState.Added;
                spInfo.SpNameInsert = "UP_MM_QTIO_LOCATION_I";
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "CD_LOCATION",
                                                       "CD_ITEM",
                                                       "DT_IO",
                                                       "FG_PS",
                                                       "FG_IO",
                                                       "CD_QTIOTP",
                                                       "CD_PLANT",
                                                       "CD_SL",
                                                       "QT_IO_LOCATION",
                                                       "YN_RETURN" };

                spCollection.Add(spInfo);
            }
                
            if (dtQCl != null && dtQCl.Rows.Count > 0)
            {
                spInfo = new SpInfo();

                spInfo.DataValue = dtH;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "UP_MM_QC_INSERT";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "NO_IO",
                                                       "DT_IO",
                                                       "CD_PARTNER",
                                                       "NO_EMP",
                                                       "FG_IO" };
                spInfo.SpParamsValues.Add(ActionState.Insert, "FG_IO", "022");

                spCollection.Add(spInfo);

                spInfo = new SpInfo();

                spInfo.DataValue = dtQCl;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "UP_MM_QCL_INSERT";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "NO_IO",
                                                       "NO_IOLINE",
                                                       "CD_PLANT",
                                                       "CD_ITEM",
                                                       "QT_GOOD_INV",
                                                       "DC_RMK",
                                                       "CD_PROJECT",
                                                       "SEQ_PROJECT" };
                
                spCollection.Add(spInfo);
            }

            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spCollection))
            {
                if (!resultData.Result)
                    return false;
            }

            return true;
        }

        public void Delete(string NO_IO)
        {
            Global.MainFrame.ExecSp("UP_PU_GRM_DELETE", new object[] { NO_IO,
                                                                       this.회사코드,
                                                                       Global.MainFrame.LoginInfo.UserID });
        }

        internal DataSet Initial_DataSet()
        {
            DataSet dataSet = new DataSet();
            DataTable table1 = new DataTable();
            DataTable table2 = new DataTable();

            dataSet.Tables.Add(table1);
            dataSet.Tables.Add(table2);
            dataSet.Tables[0].Columns.Add("NO_IO", typeof(string));
            dataSet.Tables[0].Columns.Add("CD_PLANT", typeof(string));
            dataSet.Tables[0].Columns.Add("CD_PARTNER", typeof(string));
            dataSet.Tables[0].Columns.Add("FG_TRANS", typeof(string));
            dataSet.Tables[0].Columns.Add("YN_RETURN", typeof(string));
            dataSet.Tables[0].Columns.Add("DT_IO", typeof(string));
            dataSet.Tables[0].Columns.Add("GI_PARTNER", typeof(string));
            dataSet.Tables[0].Columns.Add("CD_DEPT", typeof(string));
            dataSet.Tables[0].Columns.Add("NO_EMP", typeof(string));
            dataSet.Tables[0].Columns.Add("DC_RMK", typeof(string));
            dataSet.Tables[0].Columns.Add("CD_QTIOTP", typeof(string));
            dataSet.Tables[0].Columns.Add("NM_QTIOTP", typeof(string));
            dataSet.Tables[0].Columns.Add("NM_DEPT", typeof(string));
            dataSet.Tables[0].Columns.Add("NM_KOR", typeof(string));
            dataSet.Tables[0].Columns.Add("CD_SL_REF", typeof(string));
            dataSet.Tables[0].Columns.Add("CD_SL", typeof(string));
            dataSet.Tables[0].Columns.Add("IN_SL", typeof(string));
            dataSet.Tables[0].Columns.Add("OUT_SL", typeof(string));
            dataSet.Tables[0].Columns.Add("YN_AM", typeof(string));
            dataSet.Tables[0].Columns.Add("NO_QC", typeof(string));
            dataSet.Tables[1].Columns.Add("S", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_IOLINE", typeof(decimal));
            dataSet.Tables[1].Columns.Add("CD_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("STND_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("UNIT_IM", typeof(string));
            dataSet.Tables[1].Columns.Add("UNIT_PO", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_LOT", typeof(string));
            dataSet.Tables[1].Columns.Add("QT_GOOD_INV", typeof(decimal));
            dataSet.Tables[1].Columns.Add("QT_GOOD_OLD", typeof(decimal));
            dataSet.Tables[1].Columns.Add("QT_UNIT_PO", typeof(decimal));
            dataSet.Tables[1].Columns.Add("QT_REJECT_INV", typeof(decimal));
            dataSet.Tables[1].Columns.Add("UM", typeof(decimal));
            dataSet.Tables[1].Columns.Add("AM", typeof(decimal));
            dataSet.Tables[1].Columns.Add("PROJECT", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_ISURCV", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_ISURCVLINE", typeof(decimal));
            dataSet.Tables[1].Columns.Add("NO_PSO_MGMT", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_PSOLINE_MGMT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("CD_SL", typeof(string));
            dataSet.Tables[1].Columns.Add("출고창고", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_SL_REF", typeof(string));
            dataSet.Tables[1].Columns.Add("입고창고", typeof(string));
            dataSet.Tables[1].Columns.Add("요청번호", typeof(string));
            dataSet.Tables[1].Columns.Add("요청일자", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_DEPT", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_EMP", typeof(string));
            dataSet.Tables[1].Columns.Add("요청부서", typeof(string));
            dataSet.Tables[1].Columns.Add("요청자", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_QTIOTP", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_PLANT", typeof(string));
            dataSet.Tables[1].Columns.Add("YN_RETURN", typeof(string));
            dataSet.Tables[1].Columns.Add("DT_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("FLAG", typeof(string));
            dataSet.Tables[1].Columns.Add("YN_AM", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_SERL", typeof(string));
            dataSet.Tables[1].Columns.Add("FG_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_QTIOTP", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_PARTNER", typeof(string));
            dataSet.Tables[1].Columns.Add("QT_GOODS", typeof(decimal));
            dataSet.Tables[1].Columns.Add("DC_RMK", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_ZONE", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_ITEMGRP", typeof(string));
            dataSet.Tables[1].Columns.Add("FG_SERNO", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_SO", typeof(string));
            dataSet.Tables[1].Columns.Add("DC_RMK1", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_IO_MGMT", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_IOLINE_MGMT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("NO_MREQ", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_MREQLINE", typeof(string));
            dataSet.Tables[1].Columns.Add("BARCODE", typeof(string));
            dataSet.Tables[1].Columns.Add("QT_GIREQ", typeof(decimal));
            dataSet.Tables[1].Columns.Add("FG_SLQC", typeof(string));
            dataSet.Tables[1].Columns.Add("CLS_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("UNIT_PO_FACT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("NO_TRACK", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_TRACK_LINE", typeof(decimal));
            dataSet.Tables[1].Columns.Add("LN_PARTNER", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_MAKER", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_DESIGN", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_PROJECT", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_PROJECT", typeof(string));
            dataSet.Tables[1].Columns.Add("SEQ_PROJECT", typeof(decimal));
            dataSet.Tables[1].Columns.Add("CD_PJT_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_PJT_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_WBS", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_CBS", typeof(string));
            dataSet.Tables[1].Columns.Add("STND_UNIT", typeof(string));
            dataSet.Tables[1].Columns.Add("FG_GUBUN", typeof(string));
            dataSet.Tables[1].Columns.Add("GI_PARTNER", typeof(string));
            dataSet.Tables[1].Columns.Add("LN_GI_PARTNER", typeof(string));

            if (Global.MainFrame.ServerKeyCommon.Contains("HANSU"))
            {
                dataSet.Tables[1].Columns.Add("CD_ITEM_PARTNER", typeof(string));
                dataSet.Tables[1].Columns.Add("NM_ITEM_PARTNER", typeof(string));
                dataSet.Tables[1].Columns.Add("CD_PACK", typeof(string));
                dataSet.Tables[1].Columns.Add("NM_PACK", typeof(string));
                dataSet.Tables[1].Columns.Add("CD_TRANSPORT", typeof(string));
                dataSet.Tables[1].Columns.Add("CD_PART", typeof(string));
                dataSet.Tables[1].Columns.Add("NM_PART", typeof(string));
                dataSet.Tables[1].Columns.Add("YN_TEST_RPT", typeof(string));
                dataSet.Tables[1].Columns.Add("DC_DESTINATION", typeof(string));
                dataSet.Tables[1].Columns.Add("DC_RMK_REQ", typeof(string));
                dataSet.Tables[1].Columns.Add("DT_DELIVERY", typeof(string));
                dataSet.Tables[1].Columns.Add("NUM_USERDEF1", typeof(string));
                dataSet.Tables[1].Columns.Add("CD_SALEGRP", typeof(string));
                dataSet.Tables[1].Columns.Add("PRIOR_GUBUN", typeof(string));
            }

            dataSet.Tables[1].Columns.Add("DT_DELIVERY_IO", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_CLS_L", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_CLS_M", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_CLS_S", typeof(string));

            if (App.SystemEnv.PMS사용)
            {
                dataSet.Tables[1].Columns.Add("CD_CSTR", typeof(string));
                dataSet.Tables[1].Columns.Add("DL_CSTR", typeof(string));
                dataSet.Tables[1].Columns.Add("NM_CSTR", typeof(string));
                dataSet.Tables[1].Columns.Add("SIZE_CSTR", typeof(string));
                dataSet.Tables[1].Columns.Add("UNIT_CSTR", typeof(string));
                dataSet.Tables[1].Columns.Add("QTY_ACT", typeof(string));
                dataSet.Tables[1].Columns.Add("UNT_ACT", typeof(string));
                dataSet.Tables[1].Columns.Add("AMT_ACT", typeof(string));
            }

            dataSet.Tables[1].Columns.Add("CD_USERDEF1_QTIO", typeof(string));
            dataSet.Tables[1].Columns.Add("CD_USERDEF2_QTIO", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_USERDEF1_QTIO", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_USERDEF2_QTIO", typeof(string));
            dataSet.Tables[1].Columns.Add("DATE_USERDEF1_QTIO", typeof(string));
            dataSet.Tables[1].Columns.Add("NUM_USERDEF1_QTIO", typeof(decimal));
            dataSet.Tables[1].Columns.Add("NM_CUST_DLV", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_TEL_D1", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_TEL_D2", typeof(string));
            dataSet.Tables[1].Columns.Add("ADDR1", typeof(string));
            dataSet.Tables[1].Columns.Add("ADDR2", typeof(string));
            dataSet.Tables[1].Columns.Add("TP_DLV", typeof(string));
            dataSet.Tables[1].Columns.Add("DC_REQ", typeof(string));
            dataSet.Tables[1].Columns.Add("AM_REQ", typeof(decimal));
            dataSet.Tables[1].Columns.Add("UM_REQ", typeof(decimal));

            if (Global.MainFrame.ServerKeyCommon == "SANSUNG")
            {
                dataSet.Tables[1].Columns.Add("NUM_STND_ITEM_1", typeof(decimal));
                dataSet.Tables[1].Columns.Add("QT_BOX", typeof(decimal));
                dataSet.Tables[1].Columns.Add("QT_SAP", typeof(decimal));
                dataSet.Tables[1].Columns.Add("QT_BOX_RE", typeof(decimal));
            }

            dataSet.Tables[1].Columns.Add("STND_DETAIL_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("MAT_ITEM", typeof(string));
            dataSet.Tables[1].Columns.Add("NM_GRP_MFG", typeof(string));
            dataSet.Tables[1].Columns.Add("FG_TRACK", typeof(string));
            dataSet.Tables[1].Columns.Add("NO_PO_PARTNER", typeof(string));
            dataSet.Tables[1].Columns.Add("DC1_SOL", typeof(string));

            foreach (DataTable table3 in dataSet.Tables)
            {
                foreach (DataColumn column in table3.Columns)
                {
                    if (column.DataType == Type.GetType("System.Decimal"))
                        column.DefaultValue = 0;
                    else if (column.DataType == Type.GetType("System.String"))
                        column.DefaultValue = "";
                }
            }

            dataSet.Tables[0].Columns["DT_IO"].DefaultValue = Global.MainFrame.GetStringToday;
            dataSet.Tables[0].Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dataSet.Tables[0].Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            dataSet.Tables[0].Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
            dataSet.Tables[0].Columns["CD_DEPT"].DefaultValue = Global.MainFrame.LoginInfo.DeptCode;
            dataSet.Tables[1].Columns["FG_IO"].DefaultValue = "022";

            return dataSet;
        }

        internal DataTable Item_List_search(object[] obj)
        {
            return DBHelper.GetDataTable("UP_MM_PITEM_SELECT", obj);
        }

        internal DataTable CD_SL_search(string cd_plant)
        {
            return DBHelper.GetDataTable(@"SELECT CD_SL,
                                                  NM_SL,
                                                  YN_QC
                                           FROM DZSN_MA_SL WITH(NOLOCK)
                                           WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                          "AND CD_PLANT = '" + cd_plant + "'");
        }

        private ArrayListExt KEY배열(string 멀티KEY)
        {
            ArrayListExt arrayListExt = new ArrayListExt();
            string[] strArray = 멀티KEY.Split('|');
            int num1 = 200;
            int num2 = 1;
            string str = string.Empty;

            for (int index = 0; index < strArray.Length - 1; ++index)
            {
                str = str + strArray[index] + "|";
                ++num2;

                if (num2 == num1 || index == strArray.Length - 2)
                {
                    arrayListExt.Add(str);
                    str = string.Empty;
                    num2 = 1;
                }
            }

            if (str != string.Empty)
                arrayListExt.Add(str);

            return arrayListExt;
        }

        internal DataTable SearchPJT(string NO_KEY)
        {
            ArrayListExt arrayListExt = this.KEY배열(NO_KEY);
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = new DataTable();
            DataTable dataTable3 = (DataTable)null;
            dataTable1.Columns.Add("멀티KEY", typeof(string));

            for (int index = 0; index < arrayListExt.Count; ++index)
            {
                DataRow row = dataTable1.NewRow();
                row["멀티KEY"] = arrayListExt[index].ToString();
                dataTable1.Rows.Add(row);
            }

            foreach (DataRow row1 in dataTable1.Rows)
            {
                DataTable dataTable4 = this.PJT_SEARCH(row1["멀티KEY"].ToString());

                if (dataTable3 == null)
                    dataTable3 = dataTable4.Clone();
                
                foreach (DataRow row2 in dataTable4.Rows)
                    dataTable3.ImportRow(row2);
            }

            return dataTable3;
        }

        private DataTable PJT_SEARCH(string NO_KEY)
        {
            return DBHelper.GetDataTable("UP_PU_PJT_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                            NO_KEY,
                                                                            Global.SystemLanguage.MultiLanguageLpoint });
        }

        internal string FG_SLQC(string gubon, string CD_ITEM, string CD_PLANT, string CD_SL)
        {
            string sql;

            if (gubon == "ITEM")
                sql = @"SELECT FG_SLQC 
                        FROM MA_PITEM WITH(NOLOCK) 
                        WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                       "AND CD_ITEM = '" + CD_ITEM + "'" + Environment.NewLine + 
                       "AND CD_PLANT = '" + CD_PLANT + "'";
            else
                sql = @"SELECT YN_QC 
                        FROM MA_SL WITH(NOLOCK) 
                        WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                       "AND CD_SL='" + CD_SL + "'" + Environment.NewLine + 
                       "AND CD_PLANT ='" + CD_PLANT + "'";
            
            DataTable dataTable = DBHelper.GetDataTable(sql);
            
            return dataTable.Rows.Count != 0 && dataTable != null ? (!(gubon == "ITEM") ? D.GetString(dataTable.Rows[0]["YN_QC"]) : D.GetString(dataTable.Rows[0]["FG_SLQC"])) : "N";
        }

        internal DataTable YN_Pallet(string NO_ISURCV)
        {
            return DBHelper.GetDataTable(@"SELECT (CASE WHEN ISNULL(SUM(CASE WHEN ISNULL(SH.STA_GIR,'') = 'TRQ' THEN 1 ELSE 0 END), 0) > 0 THEN " + " CASE WHEN COUNT(1) <> ISNULL(SUM(CASE WHEN ISNULL(SH.STA_GIR,'') = 'TRQ' THEN 1 ELSE 0 END),0) THEN 'E' ELSE 'Y' END " + " WHEN ISNULL(SUM(CASE WHEN ISNULL(SH.STA_GIR,'') = 'TRQ' THEN 1 ELSE 0 END),0) = 0 THEN 'N' END YN_PALLET " +
                                         @"FROM MM_GIREQL GL WITH(NOLOCK)
                                           JOIN SA_GIRH SH WITH(NOLOCK) ON GL.CD_USERDEF1 = SH.NO_GIR AND GL.CD_COMPANY = SH.CD_COMPANY
                                           WHERE GL.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine + 
                                          "AND GL.NO_GIREQ IN (SELECT * FROM GETTABLEFROMSPLIT2('" + NO_ISURCV + "'))");
        }

        public DataTable GetNoIO(string id_memo)
        {
            string query = @"SELECT A.NO_IO,
                                    A.CD_PLANT
                             FROM MM_QTIO A WITH(NOLOCK)   
                             WHERE A.ID_MEMO = '" + id_memo + "'" + Environment.NewLine +
                            "AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
            
            return Global.MainFrame.FillDataTable(query);
        }

        internal DataTable Search_ITEMPART(string multi_sl)
        {
            string[] pipes = D.StringConvert.GetPipes(multi_sl, 200);
            DataTable dataTable = (DataTable)null;
            
            for (int index = 0; index < pipes.Length; ++index)
            {
                DataTable table = this.ITEMPART_SEARCH(pipes[index]);
                
                if (table != null && table.Rows.Count > 0)
                {
                    if (dataTable == null)
                        dataTable = table.Clone();
                    
                    dataTable.Merge(table);
                }
            }
            return dataTable;
        }

        private DataTable ITEMPART_SEARCH(string multi_cd_sl)
        {
            return DBHelper.GetDataTable("UP_PU_CUSTITEM_EXCEL_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                       multi_cd_sl,
                                                                                       Global.SystemLanguage.MultiLanguageLpoint });
        }

        internal DataTable SaveCheck(string NO_IO, string NO_IOLINE)
        {
            return DBHelper.GetDataTable(@"SELECT DS.NO_IO,
                                                  DS.NO_SERIAL,
                                                  E.NM_QTIOTP
                                           FROM MM_QTIODS DS WITH(NOLOCK) 
                                           JOIN MM_QTIO L WITH(NOLOCK) ON DS.NO_IO = L.NO_IO AND DS.NO_IOLINE = L.NO_IOLINE AND DS.CD_COMPANY = L.CD_COMPANY
                                           JOIN MM_QTIOH H WITH(NOLOCK) ON H.NO_IO = L.NO_IO AND H.CD_COMPANY = L.CD_COMPANY
                                           JOIN (SELECT CD_COMPANY,
                                                        CD_ITEM,
                                                        NO_SERIAL
                                                 FROM MM_QTIODS WITH(NOLOCK)
                                                 WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                "AND NO_IO = '" + NO_IO + "'" + Environment.NewLine +
                                                "AND NO_IOLINE = " + NO_IOLINE + ") VT" + Environment.NewLine +
                                         @"ON DS.CD_ITEM = VT.CD_ITEM AND DS.NO_SERIAL = VT.NO_SERIAL AND DS.CD_COMPANY = VT.CD_COMPANY
                                           LEFT JOIN MM_EJTP E WITH(NOLOCK) ON E.CD_QTIOTP = L.CD_QTIOTP AND E.CD_COMPANY = L.CD_COMPANY
                                           WHERE DS.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                         @"AND ((L.FG_PS = '2' AND H.YN_RETURN = 'N') OR (L.FG_PS = '1' AND H.YN_RETURN = 'Y'))                
                                           AND DS.NO_IO <> '" + NO_IO + "'");
        }
    }
}
using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;

namespace sale
{
    class P_SA_GIRE_REG_BIZ
    {
        string 출하반품_검사 = "000";

        public P_SA_GIRE_REG_BIZ()
        {
            출하반품_검사 = BASIC.GetMAEXC("출하반품_검사");
        }

        #region ♣ 조회
        public DataSet Search(object[] obj)
        {
            //UP_SA_GI_SELECT
            DataSet ds = DBHelper.GetDataSet("UP_SA_GIRE_REG_H_S", obj, new string[] { "", "NO_GIR ASC" });

            //헤더테이블 디퐅트값 Setting
            DataTable dt_Header = ds.Tables[0];
            dt_Header.Columns["DT_IO"].DefaultValue = Global.MainFrame.GetStringToday;
            dt_Header.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dt_Header.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;

            T.SetDefaultValue(ds);

            return ds;
        }
        #endregion

        #region ♣ SearchDetail

        public DataTable SearchDetail(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_GIRE_REG_L_S", obj, "NO_GIR ASC, SEQ_GIR ASC");
            T.SetDefaultValue(dt);
            return dt;
        }

        #endregion

        #region ♣ 전용코드 조회

        public DataTable 거래처거래처부가정보영업그룹수주형태적용()
        {
            string SelectQuery = " SELECT CD_EXC " +
                                 "   FROM MA_EXC " +
                                 "  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                 "    AND EXC_TITLE = '거래처-거래처부가정보영업그룹수주형태적용' ";

            DataTable dt = Global.MainFrame.FillDataTable(SelectQuery);

            return dt;
        }

        #endregion

        #region ♣ 저장

        public bool Save(DataTable dt_H, DataTable dt_L, DataTable dt_LOT, DataTable dt_SERIAL, DataTable dt_Location)
        {
            SpInfoCollection sic = new SpInfoCollection();

            if (dt_H != null)
            {
                SpInfo siM = new SpInfo();
                siM.DataValue = dt_H;
                siM.DataState = DataValueState.Added; //무조건 인서트로 반응~ 
                siM.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siM.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siM.SpNameInsert = "UP_SA_GI_INSERT";
                siM.SpParamsInsert = new String[] { "CD_COMPANY",   "NO_IO",    "CD_PLANT", "CD_PARTNER",   "FG_TRANS", 
                                                    "DT_IO",        "CD_DEPT",  "NO_EMP",   "DC_RMK",       "YN_RETURN", 
                                                    "ID_INSERT" };
                sic.Add(siM);
            }

            if (dt_L != null)
            {
                SpInfo siD = new SpInfo();
                siD.DataValue = dt_L;
                siD.DataState = DataValueState.Added; //무조건 인서트로 반응~ 
                siD.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siD.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siD.SpNameInsert = "UP_SA_GI_INSERT1";
                siD.SpParamsInsert = new String[] { "CD_COMPANY",       "NO_IO",        "NO_IOLINE",    "CD_PLANT",         "CD_BIZAREA", 
                                                    "CD_SL",            "DT_IO",        "NO_ISURCV",    "NO_ISURCVLINE",    "NO_PSO_MGMT", 		
                                                    "NO_PSOLINE_MGMT",  "YN_PURSALE",   "FG_TPIO",      "CD_QTIOTP",        "FG_TRANS", 
                                                    "FG_TAX",           "CD_PARTNER",   "CD_ITEM",      "QT_IO",            "QT_GOOD_INV", 
                                                    "CD_EXCH",          "RT_EXCH",      "UM_EX",        "AM_EX",            "UM", 
                                                    "AM",               "VAT",          "FG_TAXP",      "CD_PJT",           "NO_LC", 
                                                    "NO_LCLINE",        "GI_PARTNER",   "NO_EMP",       "CD_GROUP",         "CD_UNIT_MM", 
                                                    "QT_UNIT_MM",       "UM_EX_PSO",    "YN_AM",        "FG_IO",            "NO_IO_MGMT", 
                                                    "NO_IOLINE_MGMT",   "FG_LC_OPEN",   "ID_INSERT",    "QT_GR_PASS",       "QT_GR_BAD",
                                                    "QT_BAD_INV",       "CD_WH",        "YN_INSPECT",   "DC_RMK",           "SEQ_PROJECT",
                                                    "CD_USERDEF1",      "CD_USERDEF2",  "TP_UM_TAX",    "UMVAT_GI"};

                //siD.SpParamsValues.Add(ActionState.Insert, "SEQ_PROJECT", 0);
                siD.SpParamsValues.Add(ActionState.Insert, "CD_USERDEF1", "");
                siD.SpParamsValues.Add(ActionState.Insert, "CD_USERDEF2", "");

                sic.Add(siD);

                if (Global.MainFrame.ServerKeyCommon == "KOWA")
                {
                    SpInfo siKowa = new SpInfo();
                    siKowa.DataValue = dt_L;
                    siKowa.DataState = DataValueState.Added;
                    siKowa.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    siKowa.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                    siKowa.SpNameInsert = "UP_SA_Z_KOWA_PH_AR_FROM_GI_U";
                    siKowa.SpParamsInsert = new String[] { "CD_COMPANY", "CD_PARTNER", "DT_IO", "YN_AM", "AM",
                                                           "VAT", "GI_PARTNER", "NO_PSO_MGMT" };
                    sic.Add(siKowa);
                }
            }

            if (dt_LOT != null)
            {  
                SpInfo si03 = new SpInfo();
                si03.DataValue = dt_LOT;
                si03.DataState = DataValueState.Added; //무조건 인서트로 반응~ 
                si03.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si03.UserID = Global.MainFrame.LoginInfo.EmployeeNo;

                /*QT_REJECT(불량수량) , QT_INSP(검사수량)*/
                si03.SpNameInsert = "UP_MM_QTIOLOT_INSERT";
                si03.SpParamsInsert = new string[]
                {
                    "CD_COMPANY",  "NO_IO",    "NO_IOLINE",    "NO_LOT",       "CD_ITEM", 
                    "DT_IO",       "FG_PS",    "FG_IO",        "CD_QTIOTP",    "CD_SL", 
                    "QT_IO",       "NO_IO",    "NO_IOLINE",    "NO_IOLINE2",   "YN_RETURN",
                    "DUMMY_TEMP_CD_PLANT_PR",
					"DUMMY_TEMP_NO_IO_PR",
					"DUMMY_TEMP_NO_LINE_IO_PR",
					"DUMMY_TEMP_NO_LINE_IO2_PR",
					"DUMMY_TEMP_FG_SLIP_PR",
					"DUMMY_TEMP_NO_LOT_PR",
					"DUMMY_TEMP_P_NO_SO",
					"DT_LIMIT",
                    "DC_LOTRMK",
                    "DUMMY_TEMP_CD_PLANT", "DUMMY_TEMP_ROOT_NO_LOT",
                    "ID_INSERT",
                    "DUMMY_BEF_NO_LOT", "DUMMY_TEMP_FG_LOT_ADD", "DUMMY_TEMP_BARCODE",
                    "CD_MNG1", "CD_MNG2", "CD_MNG3", "CD_MNG4", "CD_MNG5", "CD_MNG6", "CD_MNG7", "CD_MNG8", "CD_MNG9", "CD_MNG10",
                    "CD_MNG11", "CD_MNG12", "CD_MNG13", "CD_MNG14", "CD_MNG15", "CD_MNG16", "CD_MNG17", "CD_MNG18", "CD_MNG19", "CD_MNG20"};
                si03.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", "Y");
                si03.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_CD_PLANT_PR", string.Empty);
                si03.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_NO_IO_PR", string.Empty);
                si03.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_NO_LINE_IO_PR", 0);
                si03.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_NO_LINE_IO2_PR", 0);
                si03.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_FG_SLIP_PR", string.Empty);
                si03.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_NO_LOT_PR", string.Empty);
                si03.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_P_NO_SO", string.Empty);
                si03.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_CD_PLANT", string.Empty);
                si03.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_ROOT_NO_LOT", string.Empty);
                si03.SpParamsValues.Add(ActionState.Insert, "DUMMY_BEF_NO_LOT", string.Empty);
                si03.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_FG_LOT_ADD", "N");
                si03.SpParamsValues.Add(ActionState.Insert, "DUMMY_TEMP_BARCODE", string.Empty);
                sic.Add(si03);
            }

            if (dt_SERIAL != null)
            {
                SpInfo si04 = new SpInfo();
                si04.DataValue = dt_SERIAL;

                si04.SpNameInsert = "UP_MM_QTIODS_INSERT";
                si04.SpNameUpdate = "UP_MM_QTIODS_UPDATE";
                si04.SpNameDelete = "UP_MM_QTIODS_DELETE";
                si04.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si04.SpParamsInsert = new string[] { "CD_COMPANY", "NO_SERIAL", "NO_IO",    "NO_IOLINE", "CD_ITEM", 
		                                             "CD_QTIOTP",  "FG_IO",     "CD_MNG1",	"CD_MNG2",	 "CD_MNG3",	
                                                     "CD_MNG4",	   "CD_MNG5",	"CD_MNG6",	"CD_MNG7",	 "CD_MNG8",	
                                                     "CD_MNG9",	   "CD_MNG10",  "CD_MNG11",	"CD_MNG12",	 "CD_MNG13",	
                                                     "CD_MNG14",   "CD_MNG15",	"CD_MNG16",	"CD_MNG17",	 "CD_MNG18",	
                                                     "CD_MNG19",   "CD_MNG20" };
                si04.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_SERIAL", "NO_IO",    "NO_IOLINE", "CD_ITEM", 
		                                             "CD_QTIOTP",  "FG_IO",     "CD_MNG1",	"CD_MNG2",	 "CD_MNG3",	
                                                     "CD_MNG4",	   "CD_MNG5",	"CD_MNG6",	"CD_MNG7",	 "CD_MNG8",	
                                                     "CD_MNG9",	   "CD_MNG10",  "CD_MNG11",	"CD_MNG12",	 "CD_MNG13",	
                                                     "CD_MNG14",   "CD_MNG15",	"CD_MNG16",	"CD_MNG17",	 "CD_MNG18",	
                                                     "CD_MNG19",   "CD_MNG20" };
                si04.SpParamsDelete = new string[] { "CD_COMPANY", "NO_IO",     "NO_IOLINE", "NO_SERIAL" };

                sic.Add(si04);
            }

            if (dt_Location != null)
            {
                SpInfo si05 = new SpInfo();
                si05.DataValue = dt_Location;
                si05.DataState = DataValueState.Added;
                si05.SpNameInsert = "UP_MM_QTIO_LOCATION_I";
                si05.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si05.SpParamsInsert = new string[] { "CD_COMPANY", "NO_IO",          "NO_IOLINE",   "CD_LOCATION", "CD_ITEM",
                                                     "DT_IO",      "FG_PS",          "FG_IO",       "CD_QTIOTP",   "CD_PLANT",
                                                     "CD_SL",      "QT_IO_LOCATION", "YN_RETURN"};

                sic.Add(si05);
            }

            return DBHelper.Save(sic);
        }

        #endregion

        #region ♣ 시리얼 사용여부 알아오기(search_SERIAL)

        public string search_SERIAL(object[] obj)
        {
            //SERIAL사용여부CHK
            return D.GetString(DBHelper.ExecuteScalar("UP_PU_MNG_SER_SELECT", obj));
        }

        #endregion

        #region ♣ 재고단위EDIT여부(2중단위관리여부) 알아오기(search_EnvMng)

        public DataTable search_EnvMng()
        {
            object[] obk = new object[1];
            obk[0] = Global.MainFrame.LoginInfo.CompanyCode;

            //재고단위EDIT여부(2중단위관리여부)사용여부CHK
            return DBHelper.GetDataTable("UP_SA_ENV_SELECT", obk);
        }

        #endregion

        #region ♣ Lot_Schema

        internal DataTable dtLot_Schema(DataTable dtLot)
        {
            dtLot.Columns.Add("NO_IO", typeof(string));
            dtLot.Columns.Add("NO_IOLINE", typeof(decimal));
            dtLot.Columns.Add("NO_IOLINE2", typeof(decimal));
            dtLot.Columns.Add("NO_LOT", typeof(string));
            dtLot.Columns.Add("CD_ITEM", typeof(string));
            dtLot.Columns.Add("DT_IO", typeof(string));
            dtLot.Columns.Add("DT_LIMIT", typeof(string));
            dtLot.Columns.Add("FG_PS", typeof(string));
            dtLot.Columns.Add("FG_IO", typeof(string));
            dtLot.Columns.Add("CD_QTIOTP", typeof(string));
            dtLot.Columns.Add("CD_SL", typeof(string));
            dtLot.Columns.Add("QT_IO", typeof(decimal));
            dtLot.Columns.Add("DC_LOTRMK", typeof(string));
            dtLot.Columns.Add("CD_MNG1", typeof(string));
            dtLot.Columns.Add("CD_MNG2", typeof(string));
            dtLot.Columns.Add("CD_MNG3", typeof(string));
            dtLot.Columns.Add("CD_MNG4", typeof(string));
            dtLot.Columns.Add("CD_MNG5", typeof(string));
            dtLot.Columns.Add("CD_MNG6", typeof(string));
            dtLot.Columns.Add("CD_MNG7", typeof(string));
            dtLot.Columns.Add("CD_MNG8", typeof(string));
            dtLot.Columns.Add("CD_MNG9", typeof(string));
            dtLot.Columns.Add("CD_MNG10", typeof(string));
            dtLot.Columns.Add("CD_MNG11", typeof(string));
            dtLot.Columns.Add("CD_MNG12", typeof(string));
            dtLot.Columns.Add("CD_MNG13", typeof(string));
            dtLot.Columns.Add("CD_MNG14", typeof(string));
            dtLot.Columns.Add("CD_MNG15", typeof(string));
            dtLot.Columns.Add("CD_MNG16", typeof(string));
            dtLot.Columns.Add("CD_MNG17", typeof(string));
            dtLot.Columns.Add("CD_MNG18", typeof(string));
            dtLot.Columns.Add("CD_MNG19", typeof(string));
            dtLot.Columns.Add("CD_MNG20", typeof(string));

            return dtLot;
        }

        #endregion

        #region ♣ LOT번호 조회(수주라인)(search_LOT_KOWA)

        public string search_LOT_KOWA(string NO_SO, decimal SEQ_SO)
        {
            DataTable dt = DBHelper.GetDataTable(@"
                            SELECT  NO_LOT
                            FROM    SA_SOL
                            WHERE   CD_COMPANY  = '" + Global.MainFrame.LoginInfo.CompanyCode + @"' 
                            AND     NO_SO       = '" + NO_SO + @"' 
                            AND     SEQ_SO      = "  + SEQ_SO       );


            return D.GetString(dt.Rows[0]["NO_LOT"]);
        }

        #endregion

        internal string Get출하반품_검사 { get { return 출하반품_검사; } }
    }
}

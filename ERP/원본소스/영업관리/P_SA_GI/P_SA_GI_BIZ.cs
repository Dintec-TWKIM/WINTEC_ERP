using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;
using System.Windows.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA;
using Duzon.ERPU.SA.Common;

namespace sale
{
    class P_SA_GI_BIZ
    {

        string 출하등록_검사 = "000";

        public P_SA_GI_BIZ()
        {
            출하등록_검사 = BASIC.GetMAEXC("출하등록_검사");

        }

        #region ♣ 조회
        //public DataSet Search(object[] obj)
        //{
        //    ResultData rd = (ResultData)Global.MainFrame.FillDataSet("UP_SA_GI_H_S", obj);
        //    DataSet ds = (DataSet)rd.DataValue;

        //    T.SetDefaultValue(ds);

        //    //헤더테이블 디퐅트값 Setting
        //    DataTable dt_Header = ds.Tables[0];
        //    dt_Header.Columns["DT_IO"].DefaultValue = Global.MainFrame.GetStringToday;
        //    dt_Header.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
        //    dt_Header.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;

        //    return ds;
        //}

        public DataTable Search(object[] obj, string str조회일자구분)
        {
            string strProc = "XXXXXXXXXX_XXXXXXXXXX";

            switch (str조회일자구분)
            {
                case "GI":
                    strProc = "UP_SA_GI_H_GI_S";
                    break;
                case "DU":
                    strProc = "UP_SA_GI_H_DU_S";
                    break;
                case "RQ":
                    strProc = "UP_SA_GI_H_RQ_S";
                    break;
            }
            DataTable dt = DBHelper.GetDataTable(strProc, obj);
            T.SetDefaultValue(dt);

            //헤더테이블 디퐅트값 Setting
            dt.Columns["DT_IO"].DefaultValue = Global.MainFrame.GetStringToday;
            dt.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dt.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;

            return dt;
        }
        #endregion

        public DataTable SearchDetail(object[] obj, string str조회일자구분)
        {
            string strProc = "XXXXXXXXXX_XXXXXXXXXX";

            switch (str조회일자구분)
            {
                case "GI":
                    strProc = "UP_SA_GI_L_GI_S";
                    break;
                case "DU":
                    strProc = "UP_SA_GI_L_DU_S";
                    break;
                case "RQ":
                    strProc = "UP_SA_GI_L_RQ_S";
                    break;
            }

            DataTable dt = DBHelper.GetDataTable(strProc, obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        internal DataTable SearchCheckHeader(string 멀티의뢰번호, object[] obj)
        {
            DataTable dt결과 = null;
            try
            {
                MsgControl.ShowMsg("자료를 조회중 입니다.");
                string[] arr = D.StringConvert.GetPipes(멀티의뢰번호, 150);

                foreach (string 의뢰번호 in arr)
                {
                    obj[1] = 의뢰번호;
                    DataTable dt = DBHelper.GetDataTable("UP_SA_GI_S1", obj);

                    if (dt결과 == null)
                        dt결과 = dt;
                    else
                        dt결과.Merge(dt);
                }

                for (int i = 0; i < dt결과.Rows.Count; i++)
                    dt결과.Rows[i]["S"] = "Y";
            }
            finally
            {
                MsgControl.CloseMsg();
            }
            return dt결과;
        }

        #region ♣ 여신체크
        public DataRow CheckCredit(string cd_Partner, string cd_Partner_name, decimal am_sum, DataRow ex_Dr)
        {
            string GI = "003";
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_CREDIT_POINT_SELECT";
            si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, cd_Partner, am_sum, GI };
            ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);

            if (rtn.OutParamsSelect[0, 2] != System.DBNull.Value)
            {
                //여신잔액 < 원화금액 + 부가세    ===>>> ERROR 
                if (Convert.ToDecimal(rtn.OutParamsSelect[0, 2]) < am_sum)
                {
                    ex_Dr["S"] = "N";
                    ex_Dr["CD_PARTNER"] = cd_Partner;
                    ex_Dr["CD_PARTNER_NAME"] = cd_Partner_name;
                    ex_Dr["CREDIT_TOT"] = Convert.ToDecimal(rtn.OutParamsSelect[0, 3]);     //여신총액
                    ex_Dr["MISU_REMAIN"] = Convert.ToDecimal(rtn.OutParamsSelect[0, 1]);    //미수잔액
                    ex_Dr["CREDIT_RAMAIN"] = Convert.ToDecimal(rtn.OutParamsSelect[0, 2]);  //여신잔액
                    ex_Dr["AM_SUM"] = am_sum;                                               //출하금액

                    if (rtn.OutParamsSelect[0, 0].ToString() == "002")                      //002 : WARNING
                        ex_Dr["EX_CONTENT"] = "WARNING";                                    //WARNING or ERROR
                    else if (rtn.OutParamsSelect[0, 0].ToString() == "003")                 //003 : ERROR
                        ex_Dr["EX_CONTENT"] = "ERROR";                                      //WARNING or ERROR
                }
            }
            //else
            //    ex_Dr.Delete();

            return ex_Dr;
        }
        #endregion

        #region ♣ 저장
        public bool Save(DataTable dt_H, DataTable dt_L, DataTable dtLocation, DataTable dt_LOT, DataTable dt_SERIAL, string 출하일자, DataTable dt_ASN)
        {
            SpInfoCollection sic = new SpInfoCollection();

            if (dt_H != null)
            {
                dt_H.RemotingFormat = SerializationFormat.Binary;  //<<---데이터SET용량클때

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
                dt_L.RemotingFormat = SerializationFormat.Binary;  //<<---데이터SET용량클때

                SpInfo siD = new SpInfo();
                siD.DataValue = dt_L;
                siD.DataState = DataValueState.Added; //무조건 인서트로 반응~ 
                siD.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siD.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                siD.SpNameInsert = "UP_SA_GI_INSERT1";
                siD.SpParamsInsert = new String[] { "CD_COMPANY",       "NO_IO",           "NO_IOLINE",    "CD_PLANT",         "CD_BIZAREA", 
                                                    "CD_SL",            "DT_IO",           "NO_ISURCV",    "NO_ISURCVLINE",    "NO_PSO_MGMT", 		
                                                    "NO_PSOLINE_MGMT",  "YN_PURSALE",      "FG_TPIO",      "CD_QTIOTP",        "FG_TRANS", 
                                                    "FG_TAX",           "CD_PARTNER",      "CD_ITEM",      "QT_IO",            "QT_GOOD_INV", 
                                                    "CD_EXCH",          "RT_EXCH",         "UM_EX",        "AM_EX",            "UM", 
                                                    "AM",               "VAT",             "FG_TAXP",      "CD_PJT",           "NO_LC", 
                                                    "NO_LCLINE",        "GI_PARTNER",      "NO_EMP",       "CD_GROUP",         "CD_UNIT_MM", 
                                                    "QT_UNIT_MM",       "UM_EX_PSO",       "YN_AM",        "FG_IO",            "NO_IO_MGMT", 
                                                    "NO_IOLINE_MGMT",   "FG_LC_OPEN",      "ID_INSERT",    "QT_GR_PASS_NEW",   "QT_GR_BAD_NEW",
                                                    "QT_BAD_INV_NEW",   "CD_WH",           "YN_INSPECT",   "DC_RMK",           "SEQ_PROJECT",
                                                    "QTIO_CD_USERDEF1", "QTIO_CD_USERDEF2","TP_UM_TAX",    "UMVAT_GI"};
                siD.SpParamsValues.Add(ActionState.Insert, "QT_GR_PASS_NEW", 0);
                siD.SpParamsValues.Add(ActionState.Insert, "QT_GR_BAD_NEW", 0);
                siD.SpParamsValues.Add(ActionState.Insert, "QT_BAD_INV_NEW", 0);
                sic.Add(siD);
            }

            if (dt_LOT != null)
            {
                dt_LOT.RemotingFormat = SerializationFormat.Binary;  //<<---데이터SET용량클때

                SpInfo si03 = new SpInfo();
                si03.DataValue = dt_LOT;
                si03.DataState = DataValueState.Added; //무조건 인서트로 반응~ 
                si03.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si03.UserID = Global.MainFrame.LoginInfo.EmployeeNo;

                /*QT_REJECT(불량수량) , QT_INSP(검사수량)*/
                /* 컬럼명 한글로 써진 놈은 출고 LOT 에서 스키마를 한글명으로 준놈 때문이다. */


                si03.SpNameInsert = "UP_MM_QTIOLOT_INSERT";
                si03.SpParamsInsert = new string[] {
					"CD_COMPANY",  "출고번호", "출고항번",     "NO_LOT",       "CD_ITEM", 
					"출고일",      "FG_PS",    "수불구분",     "수불형태",     "창고코드", 
					"QT_GOOD_MNG", "NO_IO",    "NO_IOLINE",    "NO_IOLINE2",   "YN_RETURN",
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
                    "CD_MNG11", "CD_MNG12", "CD_MNG13", "CD_MNG14", "CD_MNG15", "CD_MNG16", "CD_MNG17", "CD_MNG18", "CD_MNG19", "CD_MNG20"
                };
                //DT_LIMIT 를 살리고, 나머진 죽인다. 2011/03/30 아이큐브 개발팀 김현철
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
                dt_SERIAL.RemotingFormat = SerializationFormat.Binary;  //<<---데이터SET용량클때

                SpInfo si04 = new SpInfo();
                si04.DataValue = dt_SERIAL;

                si04.SpNameInsert = "UP_MM_QTIODS_INSERT";
                si04.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si04.SpParamsInsert = new string[] { "CD_COMPANY", "NO_SERIAL", "NO_IO",    "NO_IOLINE", "CD_ITEM", 
		                                             "CD_QTIOTP",  "FG_IO",     "CD_MNG1",	"CD_MNG2",	 "CD_MNG3",	
                                                     "CD_MNG4",	   "CD_MNG5",	"CD_MNG6",	"CD_MNG7",	 "CD_MNG8",	
                                                     "CD_MNG9",	   "CD_MNG10",  "CD_MNG11",	"CD_MNG12",	 "CD_MNG13",	
                                                     "CD_MNG14",   "CD_MNG15",	"CD_MNG16",	"CD_MNG17",	 "CD_MNG18",	
                                                     "CD_MNG19",   "CD_MNG20" };

                sic.Add(si04);
            }

            if (dtLocation != null && dtLocation.Rows.Count > 0)
            {
                dtLocation.RemotingFormat = SerializationFormat.Binary;  //<<---데이터SET용량클때

                SpInfo siLocation = new SpInfo();
                siLocation.DataValue = dtLocation;
                siLocation.DataState = DataValueState.Added;
                siLocation.SpNameInsert = "UP_MM_QTIO_LOCATION_I";
                siLocation.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                siLocation.SpParamsInsert = new string[] { 
	            "CD_COMPANY", "NO_IO", "NO_IOLINE", "CD_LOCATION", "CD_ITEM", "DT_IO", "FG_PS", "FG_IO", "CD_QTIOTP","CD_PLANT", "CD_SL", "QT_IO_LOCATION", "YN_RETURN"};
                if (!dtLocation.Columns.Contains("DT_IO"))
                    siLocation.SpParamsValues.Add(ActionState.Insert, "DT_IO", 출하일자);
                if (!dtLocation.Columns.Contains("FG_IO"))
                    siLocation.SpParamsValues.Add(ActionState.Insert, "FG_IO", "010");
                if (!dtLocation.Columns.Contains("YN_RETURN"))
                    siLocation.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", "N");
                sic.Add(siLocation);
            }

            if (MA.ServerKey(false, new string[] { "ANJUN" })) //안전공업
            {
                if (dt_ASN != null)
                {
                    dt_ASN.RemotingFormat = SerializationFormat.Binary;  //<<---데이터SET용량클때

                    SpInfo si05 = new SpInfo();
                    si05.DataValue = dt_ASN;
                    si05.SpNameInsert = "UP_SA_Z_ANJUN_ASNNO_REG_I";
                    si05.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                    si05.UserID = Global.MainFrame.LoginInfo.UserID;
                    si05.SpParamsInsert = new string[] { "CD_COMPANY", "NO_IO", "NO_IOLINE", "NO_ASN", "CD_PLANT", "CD_ITEM", "DT_IO", "CD_PLANT_DELV", "QT", "ID_INSERT" };
                    sic.Add(si05);
                }
            }

            //저장 내역이 하나도 없을때 Exception 처리를 한다.
            if (sic.List == null) return false;

            ResultData[] rtn = (ResultData[])Global.MainFrame.Save(sic);
            for (int i = 0; i < rtn.Length; i++)
                if (!rtn[i].Result) return false;

            return true;
        }
        #endregion

        #region ♣ 시리얼 사용여부 알아오기
        public string search_SERIAL(object[] obj)
        {
            //SERIAL사용여부CHK
            return D.GetString(DBHelper.ExecuteScalar("UP_PU_MNG_SER_SELECT", obj));//.ToString();
        }
        #endregion

        #region ♣ 재고단위EDIT여부(2중단위관리여부) 알아오기
        public DataTable search_EnvMng()
        {
            object[] obk = new object[1];
            obk[0] = Global.MainFrame.LoginInfo.CompanyCode;

            //재고단위EDIT여부(2중단위관리여부)사용여부CHK
            return DBHelper.GetDataTable("UP_SA_ENV_SELECT", obk);
        }
        #endregion

        internal string Get출하등록_검사 { get { return 출하등록_검사; } }

        #region -> SearchInv

        public DataTable SearchInv(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_GI_INV_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        #endregion
    }
}




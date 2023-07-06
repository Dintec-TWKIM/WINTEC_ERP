using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Data;

namespace cz
{
    internal class P_CZ_SA_BILL_MNG_BIZ
    {
        private IMainFrame _mf = null;

        public P_CZ_SA_BILL_MNG_BIZ(IMainFrame mf) => this._mf = mf;

        public DataSet Search(object[] obj)
        {
            DataSet dataSet;
            if (MA.ServerKey(false, new string[] { "KOREAF",
                                                   "DDGRBR",
                                                   "SPK" }))
                dataSet = DBHelper.GetDataSet("UP_SA_BILL_MNG_SELECT", obj, new string[] { "NO_RCP" });
            else if (MA.ServerKey(false, new string[] { "TYPHC" }))
                dataSet = DBHelper.GetDataSet("UP_SA_BILL_MNG_SELECT_TYPHC", obj, new string[] { "DT_RCP", "NO_RCP" });
            else if (Global.MainFrame.ServerKeyCommon.Contains("DELIF"))
                dataSet = DBHelper.GetDataSet("UP_SA_BILL_MNG_S_BATCH", obj, new string[] { "DT_RCP", "NO_RCP" });
            else
                dataSet = DBHelper.GetDataSet("SP_CZ_SA_BILL_MNG_S", obj, new string[] { "DT_RCP", "NO_RCP" });
            return dataSet;
        }

        public DataSet Search_Jump(object[] obj) => DBHelper.GetDataSet("UP_SA_BILL_MNG_SELECT_J", obj, new string[] { "DT_RCP", "NO_RCP" });

        public bool 미결전표처리(string NoRcp)
        {
            ResultData resultData;
            if (MA.ServerKey(false, new string[] { "SKHLG" }))
                resultData = (ResultData)Global.MainFrame.ExecSp("UP_SA_Z_SKHLG_BILL_AUTO_SLIP", new object[] { this._mf.LoginInfo.CompanyCode,
                                                                                                                NoRcp,
                                                                                                                this._mf.LoginInfo.EmployeeNo });
            else if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("MONAMI") || Global.MainFrame.ServerKeyCommon.ToUpper().Contains("MONAMI1"))
                resultData = (ResultData)Global.MainFrame.ExecSp("UP_SA_Z_MONAMI_BILL_AUTO_SLIP", new object[] { this._mf.LoginInfo.CompanyCode,
                                                                                                                 NoRcp,
                                                                                                                 this._mf.LoginInfo.EmployeeNo });
            else if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("KPYRO"))
                resultData = (ResultData)Global.MainFrame.ExecSp("UP_SA_Z_KPYRO_BILL_AUTO_SLIP", new object[] { this._mf.LoginInfo.CompanyCode,
                                                                                                                NoRcp,
                                                                                                                this._mf.LoginInfo.EmployeeNo });
            else if (MA.ServerKey(false, new string[] { "CSFOOD" }) && (Global.MainFrame.LoginInfo.CompanyCode == "TEST" || Global.MainFrame.LoginInfo.CompanyCode == "1000"))
                resultData = (ResultData)Global.MainFrame.ExecSp("UP_SA_Z_CSFOOD_BILL_AUTO_SLIP", new object[] { this._mf.LoginInfo.CompanyCode,
                                                                                                                 NoRcp,
                                                                                                                 this._mf.LoginInfo.EmployeeNo });
            else if (MA.ServerKey(false, new string[] { "DAOU" }))
                resultData = (ResultData)Global.MainFrame.ExecSp("UP_SA_Z_DAOUBILL_AUTO_SLIP", new object[] { this._mf.LoginInfo.CompanyCode,
                                                                                                              NoRcp,
                                                                                                              this._mf.LoginInfo.EmployeeNo });
            else if (MA.ServerKey(false, new string[] { "NNX" }))
                resultData = (ResultData)Global.MainFrame.ExecSp("UP_SA_Z_NNX_BILL_AUTO_SLIP", new object[] { this._mf.LoginInfo.CompanyCode,
                                                                                                              NoRcp,
                                                                                                              this._mf.LoginInfo.EmployeeNo });
            else if (MA.ServerKey(false, new string[] { "CUDO2" }))
                resultData = (ResultData)Global.MainFrame.ExecSp("UP_SA_Z_CUDO2_BILL_AUTO_SLIP", new object[] { this._mf.LoginInfo.CompanyCode,
                                                                                                                NoRcp,
                                                                                                                this._mf.LoginInfo.EmployeeNo });
            else if (MA.ServerKey(false, new string[] { "FDWL" }))
                resultData = (ResultData)Global.MainFrame.ExecSp("UP_SA_Z_FDWL_BILL_AUTO_SLIP", new object[] { this._mf.LoginInfo.CompanyCode,
                                                                                                               NoRcp,
                                                                                                               this._mf.LoginInfo.EmployeeNo });
            else if (MA.ServerKey(false, new string[] { "OHSUNG" }))
                resultData = (ResultData)Global.MainFrame.ExecSp("UP_SA_Z_OHSUNG_BILL_AUTO_SLIP", new object[] { this._mf.LoginInfo.CompanyCode,
                                                                                                                 NoRcp,
                                                                                                                 this._mf.LoginInfo.EmployeeNo });
            else
                resultData = (ResultData)Global.MainFrame.ExecSp("UP_SA_BILL_AUTO_SLIP", new object[] { this._mf.LoginInfo.CompanyCode,
                                                                                                        NoRcp,
                                                                                                        this._mf.LoginInfo.EmployeeNo });
            return resultData.Result;
        }

        public bool 미결전표취소(string NoRcp) => ((ResultData)Global.MainFrame.ExecSp("UP_FI_DOCU_AUTODEL", new object[] { this._mf.LoginInfo.CompanyCode,
                                                                                                                           "130",
                                                                                                                           NoRcp,
                                                                                                                           this._mf.LoginInfo.EmployeeNo })).Result;

        public DataTable 자동반제(
          string CdPartner,
          string BillPartner,
          string TpBusi,
          string MultiNoTx,
          decimal AmRcpTot,
          decimal AmRcpTx,
          string FG_MAP)
        {
            SpInfo spInfo = new SpInfo();
            string[] strArray = new string[] { "FDWL" };
            spInfo.SpNameSelect = !MA.ServerKey(true, strArray) ? "UP_SA_BILL_AUTO_BANJAE" : "UP_SA_Z_FDWL_BILL_AUTO_BANJAE";
            spInfo.SpParamsSelect = new object[] { this._mf.LoginInfo.CompanyCode,
                                                   CdPartner,
                                                   BillPartner,
                                                   TpBusi,
                                                   MultiNoTx,
                                                   AmRcpTot,
                                                   AmRcpTx,
                                                   FG_MAP };
            return (DataTable)((ResultData)this._mf.FillDataTable(spInfo)).DataValue;
        }

        public DataTable 자동반제2(
          string CdPartner,
          string BillPartner,
          string TpBusi,
          string MultiNoTx,
          decimal AmRcpTot,
          decimal AmRcpTx,
          string FG_MAP)
        {
            return (DataTable)((ResultData)this._mf.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_SA_BILL_AUTO_BANJAE2",
                SpParamsSelect = new object[] { this._mf.LoginInfo.CompanyCode,
                                                CdPartner,
                                                BillPartner,
                                                TpBusi,
                                                MultiNoTx,
                                                AmRcpTot,
                                                AmRcpTx,
                                                FG_MAP }
            })).DataValue;
        }

        internal void 통합전표처리(string 멀티수금번호, string 통합멀티번호)
        {
            if (MA.ServerKey(false, new string[] { "TYPHC" }))
                DBHelper.ExecuteNonQuery("UP_SA_BILL_AUTO_SLIP_TYPHC", new object[] { MA.Login.회사코드,
                                                                                      "",
                                                                                      MA.Login.사원번호,
                                                                                      "Y",
                                                                                      멀티수금번호,
                                                                                      D.GetNull(null),
                                                                                      통합멀티번호 });
            else if (Global.MainFrame.ServerKeyCommon.Contains("DELIF"))
                DBHelper.ExecuteNonQuery("UP_SA_BILL_AUTO_SLIP_DELIF", new object[] { MA.Login.회사코드,
                                                                                      "",
                                                                                      MA.Login.사원번호,
                                                                                      "Y",
                                                                                      멀티수금번호,
                                                                                      D.GetNull(null),
                                                                                      통합멀티번호 });
            else if (MA.ServerKey(false, new string[] { "FDWL" }))
                DBHelper.ExecuteNonQuery("UP_SA_Z_FDWL_BILL_AUTO_SLIP", new object[] { MA.Login.회사코드,
                                                                                      "",
                                                                                      MA.Login.사원번호,
                                                                                      "Y",
                                                                                      멀티수금번호 });
            else
                DBHelper.ExecuteNonQuery("UP_SA_BILL_AUTO_SLIP", new object[] { MA.Login.회사코드,
                                                                                "",
                                                                                MA.Login.사원번호,
                                                                                "Y",
                                                                                멀티수금번호 });
        }

        internal void 통합전표취소(string 멀티수금번호, string 통합멀티번호)
        {
            if (MA.ServerKey(true, new string[] { "TYPHC" }) || Global.MainFrame.ServerKeyCommon.Contains("DELIF"))
                DBHelper.ExecuteNonQuery("UP_SA_DOCU_AUTODEL_RCP_BATCH_TYPHC", new object[] { MA.Login.회사코드,
                                                                                              "130",
                                                                                              MA.Login.사원번호,
                                                                                              통합멀티번호 });
            else
                DBHelper.ExecuteNonQuery("UP_FI_DOCU_AUTODEL_RCP_BATCH", new object[] { MA.Login.회사코드,
                                                                                        "130",
                                                                                        MA.Login.사원번호,
                                                                                        멀티수금번호 });
        }

        public bool Delete(string 수금번호) => ((ResultData)Global.MainFrame.ExecSp("UP_SA_RCPH_DELETE", new object[] { 수금번호,
                                                                                                                        this._mf.LoginInfo.CompanyCode,
                                                                                                                        this._mf.LoginInfo.EmployeeNo })).Result;

        public bool Save(DataTable dtH, DataTable dtB)
        {
            SpInfoCollection spInfoCollection = new SpInfoCollection();
            string str = "";
            if (dtH != null)
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dtH,
                    CompanyID = this._mf.LoginInfo.CompanyCode,
                    UserID = this._mf.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_SA_RCPH_INSERT",
                    SpNameUpdate = "UP_SA_RCPH_MNG_UPDATE",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_RCP",
                                                    "DT_RCP",
                                                    "CD_TP",
                                                    "TP_BUSI",
                                                    "CD_PARTNER",
                                                    "BILL_PARTNER",
                                                    "CD_SALEGRP",
                                                    "NO_EMP",
                                                    "NO_PROJECT",
                                                    "AM_RCP",
                                                    "AM_RCP_TX",
                                                    "AM_RCP_A",
                                                    "AM_RCPS",
                                                    "TP_AIS",
                                                    "DC_RMK",
                                                    "ID_INSERT",
                                                    "CD_EXCH",
                                                    "RT_EXCH",
                                                    "AM_RCP_EX",
                                                    "AM_RCP_A_EX",
                                                    "FG_MAP",
                                                    "AM_RCP_EX_TX",
                                                    "CD_BIZAREA" },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "NO_RCP",
                                                    "DT_RCP",
                                                    "CD_TP",
                                                    "TP_BUSI",
                                                    "CD_PARTNER",
                                                    "BILL_PARTNER",
                                                    "CD_SALEGRP",
                                                    "NO_EMP",
                                                    "NO_PROJECT",
                                                    "AM_RCP",
                                                    "AM_RCP_TX",
                                                    "AM_RCP_A",
                                                    "AM_RCPS",
                                                    "TP_AIS",
                                                    "DC_RMK",
                                                    "ID_UPDATE",
                                                    "CD_EXCH",
                                                    "RT_EXCH",
                                                    "AM_RCP_EX",
                                                    "AM_RCP_A_EX",
                                                    "FG_MAP",
                                                    "AM_RCP_EX_TX",
                                                    "CD_BIZAREA",
                                                    "TP_BILLS",
                                                    "AM_RCP_K_TX",
                                                    "AM_RCP_VAT_TX" } });
            if (dtB != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtB;
                spInfo.CompanyID = this._mf.LoginInfo.CompanyCode;
                spInfo.UserID = this._mf.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "UP_SA_RCPD_INSERT";
                spInfo.SpNameUpdate = "UP_SA_RCPD_UPDATE";
                spInfo.SpNameDelete = "UP_SA_RCPD_DELETE";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
                                                       "NO_RCP",
                                                       "NO_TX",
                                                       "DT_IV",
                                                       "TP_SO",
                                                       "AM_IV_EX",
                                                       "AM_IV",
                                                       "RT_EXCH_IV",
                                                       "AM_RCP_TX_EX",
                                                       "AM_RCP_TX",
                                                       "AM_PL",
                                                       "BILL_PARTNER",
                                                       "TYPE",
                                                       "ID",
                                                       "CD_USERDEF1",
                                                       "AM_IV_VAT",
                                                       "AM_RCP_VAT",
                                                       "AM_IV_K",
                                                       "AM_RCP_K" };
                spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
                                                       "NO_RCP",
                                                       "NO_TX",
                                                       "DT_IV",
                                                       "TP_SO",
                                                       "AM_IV_EX",
                                                       "AM_IV",
                                                       "RT_EXCH_IV",
                                                       "AM_RCP_TX_EX",
                                                       "AM_RCP_TX",
                                                       "AM_PL",
                                                       "ID",
                                                       "CD_USERDEF1",
                                                       "AM_IV_VAT",
                                                       "AM_RCP_VAT",
                                                       "AM_IV_K",
                                                       "AM_RCP_K" };
                spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                       "NO_RCP",
                                                       "NO_TX",
                                                       "DT_IV",
                                                       "BILL_PARTNER",
                                                       "ID_UPDATE" };
                spInfo.SpParamsValues.Add(ActionState.Insert, "BILL_PARTNER", str);
                spInfo.SpParamsValues.Add(ActionState.Insert, "TYPE", "0");
                spInfo.SpParamsValues.Add(ActionState.Delete, "BILL_PARTNER", str);
                spInfo.SpParamsValues.Add(ActionState.Insert, "ID", this._mf.LoginInfo.EmployeeNo);
                spInfo.SpParamsValues.Add(ActionState.Update, "ID", this._mf.LoginInfo.EmployeeNo);
                spInfoCollection.Add(spInfo);
            }
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "SLFIRE" && dtH != null)
                spInfoCollection.Add(new SpInfo()
                {
                    DataValue = dtH,
                    CompanyID = this._mf.LoginInfo.CompanyCode,
                    UserID = this._mf.LoginInfo.EmployeeNo,
                    SpNameUpdate = "UP_SA_BILL_MNG_H_U_SLFIRE",
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "NO_RCP",
                                                    "DC_RMK",
                                                    "ID_UPDATE" }
                });
            foreach (ResultData resultData in (ResultData[])this._mf.Save(spInfoCollection))
            {
                if (!resultData.Result)
                    return false;
            }
            return true;
        }

        public bool Save_SLFIRE(DataTable dtH) => DBHelper.Save(new SpInfo()
        {
            DataValue = dtH,
            CompanyID = this._mf.LoginInfo.CompanyCode,
            UserID = this._mf.LoginInfo.EmployeeNo,
            SpNameUpdate = "UP_SA_BILL_MNG_H_U_SLFIRE",
            SpParamsUpdate = new string[] { "CD_COMPANY",
                                            "NO_RCP",
                                            "DC_RMK",
                                            "ID_UPDATE" }
        });

        public DataTable Check_동양파일(string MULTI) => DBHelper.GetDataTable(" SELECT DISTINCT NO_MGMT  FROM  SA_RCPL  WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'  AND NO_RCP IN (SELECT CD_STR FROM GETTABLEFROMSPLIT2('" + MULTI + "'))");

        public DataTable Check_동양파일_DOCU(string CD_MGMT) => DBHelper.GetDataTable(" SELECT NO_DOCU  FROM  FI_DOCU  WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'  AND CD_MNG = '" + CD_MGMT + "'");

        public bool 선수금체크(string 수금번호)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_SA_RCPBILL_CHECK_SELECT", new object[] { MA.Login.회사코드,
                                                                                                     수금번호 });
            bool flag = true;
            if (dataTable != null && dataTable.Rows.Count > 0)
                flag = !(D.GetString(dataTable.Rows[0][0]) == "Y");
            return flag;
        }
    }
}
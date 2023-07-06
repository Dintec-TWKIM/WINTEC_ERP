using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    public class P_CZ_SA_BILL_BIZ
    {
        private IMainFrame _mf;

        public P_CZ_SA_BILL_BIZ(IMainFrame mf)
        {
            this._mf = mf;
        }

        public DataSet Search(string NoRcp)
        {
            ResultData resultData = (ResultData)this._mf.FillDataSet("SP_CZ_SA_BILL_S", new object[] { this._mf.LoginInfo.CompanyCode,
                                                                                                       this._mf.LoginInfo.Language,
                                                                                                       NoRcp });
            DataSet dataSet = (DataSet)resultData.DataValue;

            foreach (DataTable dataTable in dataSet.Tables)
            {
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    if (dataColumn.DataType == Type.GetType("System.Decimal"))
                        dataColumn.DefaultValue = 0;
                }
            }

            DataTable dataTable1 = dataSet.Tables[0];
            dataTable1.Columns["DT_RCP"].DefaultValue = this._mf.GetStringToday;
            dataTable1.Columns["TP_BUSI"].DefaultValue = "001";
            dataTable1.Columns["NO_EMP"].DefaultValue = this._mf.LoginInfo.EmployeeNo;
            dataTable1.Columns["NM_KOR"].DefaultValue = this._mf.LoginInfo.EmployeeName;
            dataTable1.Columns["TP_AIS"].DefaultValue = "N";
            dataTable1.Columns["CD_EXCH"].DefaultValue = "000";
            dataTable1.Columns["RT_EXCH"].DefaultValue = 1;
            dataTable1.Columns["AM_RCP_EX"].DefaultValue = 0;
            dataTable1.Columns["AM_RCP_A_EX"].DefaultValue = 0;
            dataTable1.Columns["AM_RCP_EX_TX"].DefaultValue = 0;
            dataTable1.Columns["CD_TP"].DefaultValue = "001";

            return (DataSet)resultData.DataValue;
        }

        public void Delete(string 수금번호)
        {
            this._mf.ExecSp("UP_SA_RCPH_DELETE", new object[] { 수금번호,
                                                                this._mf.LoginInfo.CompanyCode,
                                                                this._mf.LoginInfo.EmployeeNo });
        }

        public bool Save(string DtRcp, string BillPartner, DataTable dtH, DataTable dtL, DataTable dtB)
        {
            SpInfoCollection spCollection = new SpInfoCollection();
            
            if (dtH != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtH,
                    CompanyID = this._mf.LoginInfo.CompanyCode,
                    UserID = this._mf.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_SA_RCPH_INSERT",
                    SpNameUpdate = "UP_SA_RCPH_UPDATE",

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
                                                    "CD_BIZAREA",
                                                    "TP_BILLS" },

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
                                                    "TP_BILLS" }
                });
            if (dtL != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtL,
                    CompanyID = this._mf.LoginInfo.CompanyCode,
                    UserID = this._mf.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_SA_RCPL_INSERT",
                    SpNameUpdate = "UP_SA_RCPL_UPDATE",
                    SpNameDelete = "UP_SA_RCPL_DELETE",

                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_RCP",
                                                    "NO_LINE",
                                                    "FG_RCP",
                                                    "NO_MGMT",
                                                    "FG_JATA",
                                                    "AM_RCP",
                                                    "AM_RCP_A",
                                                    "CD_BANK",
                                                    "NM_ISSUE",
                                                    "DT_DUE",
                                                    "DY_TURN",
                                                    "DT_RCP",
                                                    "BILL_PARTNER",
                                                    "ID_INSERT",
                                                    "AM_RCP_EX",
                                                    "AM_RCP_A_EX",
                                                    "DC_BANK" },   
                                     
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "NO_RCP",
                                                    "NO_LINE",
                                                    "FG_RCP",
                                                    "NO_MGMT",
                                                    "FG_JATA",
                                                    "AM_RCP",
                                                    "AM_RCP_A",
                                                    "CD_BANK",
                                                    "NM_ISSUE",
                                                    "DT_DUE",
                                                    "DY_TURN",
                                                    "AM_RCP_ORG",
                                                    "DT_RCP",
                                                    "BILL_PARTNER",
                                                    "ID_UPDATE",
                                                    "AM_RCP_EX",
                                                    "AM_RCP_A_EX",
                                                    "DC_BANK" },

                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "NO_RCP",
                                                    "NO_LINE",
                                                    "ID_UPDATE" },

                    SpParamsValues = { { ActionState.Insert, "DT_RCP", DtRcp },
                                       { ActionState.Insert, "BILL_PARTNER", BillPartner },
                                       { ActionState.Update, "DT_RCP", DtRcp },
                                       { ActionState.Update, "BILL_PARTNER", BillPartner } }
                });
            if (dtB != null)
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtB,
                    CompanyID = this._mf.LoginInfo.CompanyCode,
                    UserID = this._mf.LoginInfo.EmployeeNo,
                    SpNameInsert = "UP_SA_RCPD_INSERT",
                    SpNameUpdate = "UP_SA_RCPD_UPDATE",
                    SpNameDelete = "UP_SA_RCPD_DELETE",

                    SpParamsInsert = new string[] { "CD_COMPANY",
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
                                                    "ID_INSERT",
                                                    "CD_USERDEF1" },

                    SpParamsUpdate = new string[] { "CD_COMPANY",
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
                                                    "ID_UPDATE",
                                                    "CD_USERDEF1" },

                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "NO_RCP",
                                                    "NO_TX",
                                                    "DT_IV",
                                                    "BILL_PARTNER",
                                                    "ID_UPDATE" },

                    SpParamsValues = { { ActionState.Insert, "BILL_PARTNER", BillPartner },
                                       { ActionState.Insert, "TYPE", "0" },
                                       { ActionState.Delete, "BILL_PARTNER", BillPartner } }
                });

            foreach (ResultData resultData in (ResultData[])this._mf.Save(spCollection))
            {
                if (!resultData.Result) return false;
            }
            return true;
        }

        public void 미결전표처리(string NoRcp, string CdTp)
        {
            this._mf.ExecSp("SP_CZ_SA_BILL_AUTO_SLIP", new object[] { this._mf.LoginInfo.CompanyCode,
                                                                      NoRcp,
                                                                      this._mf.LoginInfo.UserID });
        }

        public void 미결전표취소(string NoRcp)
        {
            this._mf.ExecSp("SP_CZ_FI_DOCU_AUTODEL", new object[] { this._mf.LoginInfo.CompanyCode,
                                                                    "130",
                                                                    NoRcp,
                                                                    this._mf.LoginInfo.UserID });
        }

        public bool 선수금체크(string 수금번호)
        {
            SpInfo spInfo = new SpInfo()
            {
                SpNameSelect = "UP_SA_RCPBILL_CHECK_SELECT",
                SpParamsSelect = new object[] { this._mf.LoginInfo.CompanyCode,
                                                수금번호 }
            };

            DataTable dataTable = DBHelper.GetDataTable("UP_SA_RCPBILL_CHECK_SELECT", new object[] { MA.Login.회사코드,
                                                                                                     수금번호 });

            bool flag = true;
            
            if (dataTable != null && dataTable.Rows.Count > 0)
                flag = !(D.GetString(dataTable.Rows[0][0]) == "Y");
            
            return flag;
        }

        public DataTable 자동반제(string CdPartner, string BillPartner, string TpBusi, string MultiNoTx, Decimal AmRcpTot, Decimal AmRcpTx, string fgMap)
        {
            return (DataTable)((ResultData)this._mf.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_SA_BILL_AUTO_BANJAE",
                SpParamsSelect = new object[] { this._mf.LoginInfo.CompanyCode,
                                                CdPartner,
                                                BillPartner,
                                                TpBusi,
                                                MultiNoTx,
                                                AmRcpTot,
                                                AmRcpTx,
                                                fgMap }
            })).DataValue;
        }

        internal DataTable SelectAM_RCP_A(object[] param)
        {
            return DBHelper.GetDataTable("SP_SA_BILL_SCH_PRE_SELECT", param);
        }

        internal Decimal SearchOutstandingBond(string cdPartner)
        {
            object[] outParameters = new object[3];

            DBHelper.ExecuteNonQuery("UP_SA_OUTSTANDING_BOND_S", new object[] { MA.Login.회사코드,
                                                                                Global.MainFrame.GetStringToday,
                                                                                cdPartner }, out outParameters);
            
            return D.GetDecimal(outParameters[0]);
        }

        internal DataTable CheckDuplication(string noRcp, string noMgmt)
        {
            return DBHelper.GetDataTable(@"SELECT MIN(NO_RCP) AS NO_RCP
                                           FROM SA_RCPL WITH(NOLOCK)
                                           WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'" + Environment.NewLine +
                                          "AND NO_MGMT = '" + noMgmt + "'" + Environment.NewLine +
                                          "AND NO_RCP <> '" + noRcp + "'");
        }

        internal DataTable 영업그룹(string 계산서번호)
        {
            if (!string.IsNullOrEmpty(계산서번호))
            {
                return DBHelper.GetDataTable(@"SELECT TOP 1 SI.CD_SALEGRP,
                                                      MS.NM_SALEGRP
                                               FROM SA_IVL SI WITH(NOLOCK)
                                               LEFT JOIN MA_SALEGRP MS WITH(NOLOCK) ON MS.CD_COMPANY = SI.CD_COMPANY AND MS.CD_SALEGRP = SI.CD_SALEGRP
                                               WHERE SI.CD_COMPANY = '" + MA.Login.회사코드 + "'" + Environment.NewLine +
                                              "AND SI.NO_IV = '" + 계산서번호 + "'" + Environment.NewLine +
                                              "GROUP BY SI.CD_SALEGRP, MS.NM_SALEGRP");
            }
            else
            {
                return DBHelper.GetDataTable(@"SELECT TOP 1 CD_SALEGRP,
                                                      NM_SALEGRP 
                                               FROM MA_SALEGRP WITH(NOLOCK)
                                               WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'" + Environment.NewLine +
                                              "ORDER BY CD_SALEGRP");
            }
        }
    }
}

using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    class P_CZ_SA_RCPBILL_BIZ
    {
        private IMainFrame _mainFrame;

        public P_CZ_SA_RCPBILL_BIZ(IMainFrame mf)
        {
            this._mainFrame = mf;
        }

        public DataSet Search(string 수금번호)
        {
            ResultData resultData = (ResultData)this._mainFrame.FillDataSet("SP_CZ_SA_RCPBILL_S", new object[] { this._mainFrame.LoginInfo.CompanyCode, 수금번호 });
            DataSet dataSet = (DataSet)resultData.DataValue;

            foreach (DataTable dataTable in (InternalDataCollectionBase)dataSet.Tables)
            {
                foreach (DataColumn dataColumn in (InternalDataCollectionBase)dataTable.Columns)
                {
                    if (dataColumn.DataType == Type.GetType("System.Decimal"))
                        dataColumn.DefaultValue = 0;
                }
            }

            DataTable dataTable1 = dataSet.Tables[0];
            dataTable1.Columns["DT_BILLS"].DefaultValue = this._mainFrame.GetStringToday;
            dataTable1.Columns["CD_TP"].DefaultValue = "001";
            dataTable1.Columns["TP_BUSI"].DefaultValue = "001";
            dataTable1.Columns["NO_EMP"].DefaultValue = this._mainFrame.LoginInfo.EmployeeNo;
            dataTable1.Columns["NM_KOR"].DefaultValue = this._mainFrame.LoginInfo.EmployeeName;
            dataTable1.Columns["ST_BILL"].DefaultValue = "N";
            dataTable1.Columns["TP_BILLS"].DefaultValue = "N";

            return (DataSet)resultData.DataValue;
        }

        public void Delete(string 수금번호)
        {
            this._mainFrame.ExecSp("UP_SA_BILLS_DELETE", new object[] { this._mainFrame.LoginInfo.CompanyCode, 수금번호 });
        }

        public bool Save(string DtRcp, string BillPartner, DataTable dtH, DataTable dtL, DataTable dtB)
        {
            SpInfoCollection spCollection = new SpInfoCollection();

            if (dtH != null)
            {
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtH,
                    CompanyID = this._mainFrame.LoginInfo.CompanyCode,
                    UserID = this._mainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "SP_SA_BILLSH_INSERT",
                    SpNameUpdate = "SP_SA_BILLSH_UPDATE",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_BILLS",
                                                    "DT_BILLS",
                                                    "CD_BILLTGRP",
                                                    "CD_PARTNER",
                                                    "BILL_PARTNER",
                                                    "NO_EMP",
                                                    "TP_BUSI",
                                                    "AM_BILLS",
                                                    "AM_IV",
                                                    "DC_RMK",
                                                    "CD_TP",
                                                    "ST_BILL",
                                                    "TP_BILLS",
                                                    "CD_DOCU" },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "NO_BILLS",
                                                    "DT_BILLS",
                                                    "CD_BILLTGRP",
                                                    "CD_PARTNER",
                                                    "BILL_PARTNER",
                                                    "NO_EMP",
                                                    "TP_BUSI",
                                                    "AM_BILLS",
                                                    "AM_IV",
                                                    "DC_RMK",
                                                    "CD_TP",
                                                    "TP_BILLS",
                                                    "CD_DOCU" }
                });
            }

            if (dtL != null)
            {
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtL,
                    CompanyID = this._mainFrame.LoginInfo.CompanyCode,
                    UserID = this._mainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "SP_CZ_SA_BILLSL_I",
                    SpNameUpdate = "SP_CZ_SA_BILLSL_U",
                    SpNameDelete = "SP_CZ_SA_BILLSL_D",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_BILLS",
                                                    "NO_LINE",
                                                    "AM_TARGET",
                                                    "AM_BILLS",
                                                    "NO_RCP",
                                                    "CD_EXCH",
                                                    "RT_EXCH",
                                                    "AM_TARGET_EX",
                                                    "AM_BILLS_EX",
                                                    "NO_MGMT",
                                                    "NO_DOCU",
                                                    "NO_DOLINE",
                                                    "CD_PJT" },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "NO_BILLS",
                                                    "NO_LINE",
                                                    "AM_TARGET",
                                                    "AM_BILLS",
                                                    "NO_RCP",
                                                    "CD_EXCH",
                                                    "RT_EXCH",
                                                    "AM_TARGET_EX",
                                                    "AM_BILLS_EX",
                                                    "NO_MGMT",
                                                    "NO_DOCU",
                                                    "NO_DOLINE",
                                                    "CD_PJT" },
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "NO_BILLS",
                                                    "NO_LINE" }
                });
            }

            if (dtB != null)
            {
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtB,
                    CompanyID = this._mainFrame.LoginInfo.CompanyCode,
                    UserID = this._mainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "SP_CZ_SA_BILLSD_I",
                    SpNameUpdate = "SP_CZ_SA_BILLSD_U",
                    SpNameDelete = "SP_CZ_SA_BILLSD_D",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_BILLS",
                                                    "NO_IV",
                                                    "DT_IV",
                                                    "AM_TARGET",
                                                    "AM_RCPS",
                                                    "GUBUN",
                                                    "CD_PARTNER",
                                                    "CD_EXCH_IV",
                                                    "RT_EXCH_IV",
                                                    "AM_TARGET_EX",
                                                    "AM_RCPS_EX",
                                                    "AM_PL",
                                                    "NO_RCP",
                                                    "NO_DOCU_RCP",
                                                    "NO_DOLINE_RCP",
                                                    "NO_DOCU_IV",
                                                    "NO_DOLINE_IV",
                                                    "TP_SO" },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "NO_BILLS",
                                                    "NO_IV",
                                                    "DT_IV",
                                                    "AM_TARGET",
                                                    "AM_RCPS",
                                                    "GUBUN",
                                                    "CD_PARTNER",
                                                    "CD_EXCH_IV",
                                                    "RT_EXCH_IV",
                                                    "AM_TARGET_EX",
                                                    "AM_RCPS_EX",
                                                    "AM_PL",
                                                    "NO_RCP",
                                                    "NO_DOCU_RCP",
                                                    "NO_DOLINE_RCP",
                                                    "NO_DOCU_IV",
                                                    "NO_DOLINE_IV",
                                                    "TP_SO" },
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "NO_BILLS",
                                                    "NO_IV",
                                                    "GUBUN",
                                                    "CD_PARTNER",
                                                    "NO_RCP" },
                    SpParamsValues = { { ActionState.Insert, "CD_PARTNER", BillPartner },
                                       { ActionState.Update, "CD_PARTNER", BillPartner },
                                       { ActionState.Delete, "CD_PARTNER", BillPartner } }
                });
            }

            foreach (ResultData resultData in (ResultData[])this._mainFrame.Save(spCollection))
            {
                if (!resultData.Result)
                    return false;
            }
            return true;
        }

        public void 미결전표처리(string NoRcp)
        {
            this._mainFrame.ExecSp("SP_CZ_SA_RCPBILL_TRANSFER_DOCU", new object[] { this._mainFrame.LoginInfo.CompanyCode, 
                                                                                    NoRcp, 
                                                                                    this._mainFrame.LoginInfo.UserID });
        }

        public void 미결전표취소(string NoRcp)
        {
            this._mainFrame.ExecSp("SP_CZ_FI_DOCU_AUTODEL", new object[] { this._mainFrame.LoginInfo.CompanyCode, 
                                                                           "151", 
                                                                           NoRcp, 
                                                                           this._mainFrame.LoginInfo.UserID });
        }

        public DataTable 자동반제(string CdPartner, string BillPartner, string TpBusi, string MultiNoTx, Decimal AmRcpTot, Decimal AmRcpTx)
        {
            return (DataTable)((ResultData)this._mainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_SA_BILL_AUTO_BANJAE",
                SpParamsSelect = new object[] { this._mainFrame.LoginInfo.CompanyCode,
                                                CdPartner,
                                                BillPartner,
                                                TpBusi,
                                                MultiNoTx,
                                                AmRcpTot,
                                                AmRcpTx }
            })).DataValue;
        }

        public Decimal 환율(string 수금일자, string 환종)
        {
            return Convert.ToDecimal(((ResultData)Global.MainFrame.FillDataTable(new SpInfo()
            {
                SpNameSelect = "UP_SA_BILL_SELECT_EXCH",
                SpParamsSelect = new object[] { this._mainFrame.LoginInfo.CompanyCode,
                                                수금일자,
                                                환종 }
            })).OutParamsSelect[0, 0]);
        }

        internal DataTable 매출반제()
        {
            return new DataTable()
            {
                Columns = { { "NO_DOCU", typeof (string) },
                            { "NO_DOLINE", typeof (string) },
                            { "AM_BAN_EX", typeof (decimal) },
                            { "AM_BAN", typeof (decimal) } }
            };
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

using System;
using System.Data;
using System.IO;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    class P_CZ_PU_ADPAYMENT_BILL_BIZ
    {
        private IMainFrame _mainFrame;

        public P_CZ_PU_ADPAYMENT_BILL_BIZ(IMainFrame mf)
        {
            this._mainFrame = mf;
        }

        public DataSet Search(string 정리번호)
        {
            ResultData resultData = (ResultData)this._mainFrame.FillDataSet("SP_CZ_PU_ADPAYMENT_BILL_S", new object[] { this._mainFrame.LoginInfo.CompanyCode,
                                                                                                                        this._mainFrame.LoginInfo.Language,
                                                                                                                        정리번호 });
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

            return (DataSet)resultData.DataValue;
        }

        public bool 선지급체크(string 정리번호)
        {
            string query;

            query = @"SELECT COUNT(1)
                      FROM CZ_PU_ADPAYMENT_BILL_L WITH(NOLOCK)
                      WHERE CD_COMPANY = '" + this._mainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                     "AND NO_BILLS = '" + 정리번호 + "'";

            if (D.GetDecimal(this._mainFrame.ExecuteScalar(query)) > 0)
                return true;
            else
                return false;
        }

        public void Delete(string 정리번호)
        {
            this._mainFrame.ExecSp("SP_CZ_PU_ADPAYMENT_BILL_D", new object[] { this._mainFrame.LoginInfo.CompanyCode, 정리번호 });
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
                    SpNameInsert = "SP_CZ_PU_ADPAYMENT_BILL_HI",
                    SpNameUpdate = "SP_CZ_PU_ADPAYMENT_BILL_HU",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_BILLS",
                                                    "DT_BILLS",
                                                    "CD_BILLTGRP",
                                                    "CD_PARTNER",
                                                    "NO_EMP",
                                                    "TP_BUSI",
                                                    "AM_BILLS",
                                                    "AM_IV",
                                                    "DC_RMK",
                                                    "CD_TP",
                                                    "ST_BILL",
                                                    "CD_DOCU",
                                                    "ID_INSERT" },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "NO_BILLS",
                                                    "DT_BILLS",
                                                    "CD_BILLTGRP",
                                                    "CD_PARTNER",
                                                    "NO_EMP",
                                                    "TP_BUSI",
                                                    "AM_BILLS",
                                                    "AM_IV",
                                                    "DC_RMK",
                                                    "CD_TP",
                                                    "CD_DOCU",
                                                    "ID_UPDATE" }
                });
            }

            if (dtL != null)
            {
                spCollection.Add(new SpInfo()
                {
                    DataValue = dtL,
                    CompanyID = this._mainFrame.LoginInfo.CompanyCode,
                    UserID = this._mainFrame.LoginInfo.EmployeeNo,
                    SpNameInsert = "SP_CZ_PU_ADPAYMENT_BILL_LI",
                    SpNameUpdate = "SP_CZ_PU_ADPAYMENT_BILL_LU",
                    SpNameDelete = "SP_CZ_PU_ADPAYMENT_BILL_LD",
                    SpParamsInsert = new string[] { "CD_COMPANY",
                                                    "NO_BILLS",
                                                    "NO_LINE",
                                                    "AM_TARGET",
                                                    "AM_BILLS",
                                                    "NO_ADPAY",
                                                    "CD_EXCH",
                                                    "RT_EXCH",
                                                    "AM_TARGET_EX",
                                                    "AM_BILLS_EX",
                                                    "NO_MGMT",
                                                    "CD_PJT",
                                                    "NO_DOCU",
                                                    "NO_DOLINE",
                                                    "ID_INSERT" },
                    SpParamsUpdate = new string[] { "CD_COMPANY",
                                                    "NO_BILLS",
                                                    "NO_LINE",
                                                    "AM_TARGET",
                                                    "AM_BILLS",
                                                    "NO_ADPAY",
                                                    "CD_EXCH",
                                                    "RT_EXCH",
                                                    "AM_TARGET_EX",
                                                    "AM_BILLS_EX",
                                                    "NO_MGMT",
                                                    "CD_PJT",
                                                    "NO_DOCU",
                                                    "NO_DOLINE",
                                                    "ID_UPDATE" },
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
                    SpNameInsert = "SP_CZ_PU_ADPAYMENT_BILL_DI",
                    SpNameUpdate = "SP_CZ_PU_ADPAYMENT_BILL_DU",
                    SpNameDelete = "SP_CZ_PU_ADPAYMENT_BILL_DD",
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
                                                    "NO_ADPAY",
                                                    "NO_DOCU_ADPAY",
                                                    "NO_DOLINE_ADPAY",
                                                    "NO_DOCU_IV",
                                                    "NO_DOLINE_IV",
                                                    "ID_INSERT" },
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
                                                    "NO_ADPAY",
                                                    "NO_DOCU_ADPAY",
                                                    "NO_DOLINE_ADPAY",
                                                    "NO_DOCU_IV",
                                                    "NO_DOLINE_IV",
                                                    "ID_UPDATE" },
                    SpParamsDelete = new string[] { "CD_COMPANY",
                                                    "NO_BILLS",
                                                    "NO_IV",
                                                    "GUBUN",
                                                    "CD_PARTNER",
                                                    "NO_ADPAY" },
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

        public void 미결전표처리(string 정리번호)
        {
            this._mainFrame.ExecSp("SP_CZ_PU_ADPAYMENT_BILL_DOCU", new object[] { this._mainFrame.LoginInfo.CompanyCode,
                                                                                  this._mainFrame.LoginInfo.Language,
                                                                                  정리번호,
                                                                                  this._mainFrame.LoginInfo.UserID });
        }

        public void 미결전표취소(string 정리번호)
        {
            this._mainFrame.ExecSp("SP_CZ_FI_DOCU_AUTODEL", new object[] { this._mainFrame.LoginInfo.CompanyCode, 
                                                                           "251",
                                                                           정리번호,
                                                                           this._mainFrame.LoginInfo.UserID });
        }

        internal DataTable 선지급반제()
        {
            return new DataTable()
            {
                Columns = { { "NO_DOCU", typeof (string) },
                            { "NO_DOLINE", typeof (string ) },
                            { "AM_BAN_EX", typeof (decimal) },
                            { "AM_BAN", typeof (decimal) } }
            };
        }

        internal DataTable 구매그룹(string 매입번호)
        {
            if (!string.IsNullOrEmpty(매입번호))
            {
                return DBHelper.GetDataTable(@"SELECT TOP 1 PI.CD_PURGRP,
                                                      MP.NM_PURGRP 
                                               FROM PU_IVL PI WITH(NOLOCK)
                                               LEFT JOIN MA_PURGRP MP WITH(NOLOCK) ON MP.CD_COMPANY = PI.CD_COMPANY AND MP.CD_PURGRP = PI.CD_PURGRP
                                               WHERE PI.CD_COMPANY = '" + MA.Login.회사코드 + "'" + Environment.NewLine +
                                              "AND PI.NO_IV = '" + 매입번호 + "'" + Environment.NewLine +
                                              "GROUP BY PI.CD_PURGRP, MP.NM_PURGRP");
            }
            else
            {
                return DBHelper.GetDataTable(@"SELECT TOP 1 CD_PURGRP,
                                                      NM_PURGRP 
                                               FROM MA_PURGRP WITH(NOLOCK)
                                               WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'" + Environment.NewLine +
                                              "ORDER BY CD_PURGRP");
            }
        }

        public void SaveFileInfo(string cdCompany, string cdFile, FileInfo fileInfo, string 업로드위치, string 메뉴명)
        {
            string query;

            try
            {
                query = @"SELECT MAX(NO_SEQ) 
                          FROM MA_FILEINFO WITH(NOLOCK)
                          WHERE CD_COMPANY = '" + cdCompany + "'" + Environment.NewLine +
                        @"AND CD_MODULE = 'CZ'
                          AND ID_MENU = '" + 메뉴명 + "'" + Environment.NewLine +
                         "AND CD_FILE = '" + cdFile + "'";

                DBHelper.ExecuteNonQuery("UP_MA_FILEINFO_INSERT", new object[] { cdCompany,
                                                                                 "CZ",
                                                                                 메뉴명,
                                                                                 cdFile,
                                                                                 (D.GetDecimal(DBHelper.ExecuteScalar(query)) + 1),
                                                                                 fileInfo.Name,
                                                                                 업로드위치,
                                                                                 fileInfo.Extension.Replace(".", ""),
                                                                                 fileInfo.LastWriteTime.ToString("yyyyMMdd"),
                                                                                 fileInfo.LastWriteTime.ToString("hhmm"),
                                                                                 string.Format("{0:0.00}M", ((Decimal)fileInfo.Length / new Decimal(1048576))),
                                                                                 Global.MainFrame.LoginInfo.UserID });
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

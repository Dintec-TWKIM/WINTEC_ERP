using System;
using System.Data;
using System.Windows.Forms;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    public class P_CZ_SA_IVMNG_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_SA_IVMNGH_S", obj);
        }

        internal DataTable SearchDetail(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_SA_IVMNGL_S", obj);
        }

        internal bool Save(DataTable dtH, DataTable dtL)
        {
            SpInfoCollection spCollection = new SpInfoCollection();
            SpInfo spInfo = new SpInfo();

            spInfo.DataValue = dtH;
            spInfo.SpNameDelete = "SP_SA_IVH_DELETE";
            spInfo.SpParamsDelete = new string[] { "NO_IV",
                                                   "CD_COMPANY" };

            spInfo.SpNameUpdate = "UP_SA_IVH_UPDATE";
            spInfo.SpParamsUpdate = new string[] { "NO_IV",
                                                   "CD_COMPANY",
                                                   "ID_UPDATE",
                                                   "AM_K",
                                                   "VAT_TAX",
                                                   "NO_VOLUME",
                                                   "NO_HO",
                                                   "NO_SEQ",
                                                   "DC_REMARK",
                                                   "DT_RCP_RSV",
                                                   "CD_DOCU",
                                                   "ETAX_SEND_TYPE",
                                                   "ETAX_SELL_DAM_NM",
                                                   "ETAX_SELL_DAM_MOBIL",
                                                   "ETAX_SELL_DAM_EMAIL",
                                                   "NM_PTR",
                                                   "EX_EMIL",
                                                   "EX_HP",
                                                   "CD_DEPT",
                                                   "NO_EMP",
                                                   "TP_RECEIPT" };
            spCollection.Add(spInfo);

            spCollection.Add(new SpInfo()
            {
                DataValue = dtL,
                SpNameUpdate = "UP_SA_IVL_UPDATE",
                SpParamsUpdate = new string[] { "NO_IV",
                                                "NO_LINE",
                                                "CD_COMPANY",
                                                "ID_UPDATE",
                                                "UM_ITEM_CLS",
                                                "AM_CLS",
                                                "VAT" }
            });

            return DBHelper.Save(spCollection);
        }

        public bool 미결전표처리(object[] obj)
        {
            return ((ResultData)(Global.MainFrame.ExecSp("SP_CZ_SA_IVMNG_TRANSFER_DOCU", obj))).Result;
        }

        public bool 미결전표취소(string 전표유형, string 전표번호)
        {
            return ((ResultData)(Global.MainFrame.ExecSp("SP_CZ_FI_DOCU_AUTODEL", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                 전표유형,
                                                                                                 전표번호,
                                                                                                 Global.MainFrame.LoginInfo.UserID }))).Result;
        }

        public DataSet 전표출력(string 회계단위, string 전표번호)
        {
            DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_IVMNG_RPT2_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                     회계단위, 
                                                                                     전표번호 });

            if (ds != null)
            {
                if (ds.Tables[0] != null)
                {
                    ds.Tables[0].Columns.Add("NO_SO");
                    ds.Tables[0].Columns.Add("NO_IO");
                }
            }

            return ds;
        }

        public bool change_status(DataTable dtL)
        {
            SpInfoCollection spInfoCollection = new SpInfoCollection();
            if (dtL != null)
            {
                SpInfo spInfo = new SpInfo();
                spInfo.DataValue = dtL;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                spInfo.SpNameInsert = "UP_SA_ETAX36524_H_UPDATE";
                spInfo.SpParamsInsert = new string[2] { "NO", "CMP_TAX_ST" };
                spInfoCollection.Add(spInfo);
            }

            foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spInfoCollection))
            {
                if (!resultData.Result)
                    return false;
            }

            return true;
        }

        internal DataTable etax_LoginSelect(string 사업장, string 모듈)
        {
            SpInfo spInfo = new SpInfo();
            spInfo.SpNameSelect = "UP_SA_ETAXSYS_SELECT";
            spInfo.SpParamsSelect = new object[3] { Global.MainFrame.LoginInfo.CompanyCode, 사업장, 모듈 };

            return (DataTable)((ResultData)Global.MainFrame.FillDataTable(spInfo)).DataValue;
        }

        public bool etax_Update(string NO_IV, string TAX_ID, string 상태, string 발행EMAIL)
        {
            return ((ResultData)Global.MainFrame.FillDataSet("UP_SA_ETAXSYS_UPDATE", new object[5] { Global.MainFrame.LoginInfo.CompanyCode, NO_IV, TAX_ID, 상태, 발행EMAIL })).Result;
        }

        internal void SaveContent(ContentType contentType, Dass.FlexGrid.CommandType commandType, string noIV, string value)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;

            if (contentType == ContentType.Memo)
                str2 = "MEMO_CD";
            else if (contentType == ContentType.CheckPen)
                str2 = "CHECK_PEN";
            if (commandType == Dass.FlexGrid.CommandType.Add)
                str1 = "UPDATE SA_IVH SET " + str2 + " = '" + value + "' WHERE NO_IV = '" + noIV + "' AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
            else if (commandType == Dass.FlexGrid.CommandType.Delete)
                str1 = "UPDATE SA_IVH SET " + str2 + " = NULL WHERE NO_IV = '" + noIV + "' AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
            
            Global.MainFrame.ExecuteScalar(str1);
        }

        internal DataTable TB_TAX()
        {
            return new DataTable()
            {
                Columns = { { "NO", typeof (string) },
                            { "NO_TAX", typeof (string) },
                            { "NO_CUST", typeof (string) },
                            { "NO_USER", typeof (string) },
                            { "YMD_WRITE", typeof (string) },
                            { "FG_FINAL", typeof (string) },
                            { "FG_PC", typeof (string) },
                            { "FG_IO", typeof (string) },
                            { "FG_VAT", typeof (string) },
                            { "FG_BILL", typeof (string) },
                            { "YN_CSMT", typeof (string) },
                            { "YN_TURN", typeof (string) },
                            { "SELL_NO_BIZ", typeof (string) },
                            { "SELL_NM_CORP", typeof (string) },
                            { "SELL_NM_CEO", typeof (string) },
                            { "SELL_ADDR1", typeof (string) },
                            { "SELL_ADDR2", typeof (string) },
                            { "SELL_BIZ_STATUS", typeof (string) },
                            { "SELL_BIZ_TYPE", typeof (string) },
                            { "SELL_DAM_NM", typeof (string) },
                            { "SELL_DAM_EMAIL", typeof (string) },
                            { "SELL_DAM_MOBIL1", typeof (string) },
                            { "SELL_DAM_MOBIL2", typeof (string) },
                            { "SELL_DAM_MOBIL3", typeof (string) },
                            { "SELL_DAM_DEPT", typeof (string) },
                            { "SELL_DAM_TEL1", typeof (string) },
                            { "SELL_DAM_TEL2", typeof (string) },
                            { "SELL_DAM_TEL3", typeof (string) },
                            { "BUY_NO_BIZ", typeof (string) },
                            { "BUY_NM_CORP", typeof (string) },
                            { "BUY_NM_CEO", typeof (string) },
                            { "BUY_ADDR1", typeof (string) },
                            { "BUY_ADDR2", typeof (string) },
                            { "BUY_BIZ_STATUS", typeof (string) },
                            { "BUY_BIZ_TYPE", typeof (string) },
                            { "BUY_DAM_NM", typeof (string) },
                            { "BUY_DAM_EMAIL", typeof (string) },
                            { "BUY_DAM_MOBIL1", typeof (string) },
                            { "BUY_DAM_MOBIL2", typeof (string) },
                            { "BUY_DAM_MOBIL3", typeof (string) },
                            { "BUY_DAM_DEPT", typeof (string) },
                            { "BUY_DAM_TEL1", typeof (string) },
                            { "BUY_DAM_TEL2", typeof (string) },
                            { "BUY_DAM_TEL3", typeof (string) },
                            { "AM", typeof (decimal) },
                            { "AM_VAT", typeof (decimal) },
                            { "NO_VOL", typeof (string) },
                            { "NO_ISSUE", typeof (string) },
                            { "NO_SERIAL", typeof (string) },
                            { "NO_BLK", typeof (decimal) },
                            { "DC_RMK", typeof (string) },
                            { "AMT_CASH", typeof (decimal) },
                            { "AMT_CHECK", typeof (decimal) },
                            { "AMT_NOTE", typeof (decimal) },
                            { "AMT_AR", typeof (decimal) },
                            { "AMT", typeof (decimal) },
                            { "NO_ISS", typeof (string) },
                            { "YN_ISS", typeof (string) },
                            { "YMD_ISS", typeof (string) },
                            { "YN_SIGNS", typeof (string) },
                            { "YN_MAGAM", typeof (string) },
                            { "YN_DEL", typeof (string) },
                            { "APP_NO_USER", typeof (string) },
                            { "IP_APP", typeof (string) },
                            { "NO_SENDER_PK", typeof (string) },
                            { "NM_SENDER_PK", typeof (string) },
                            { "NM_SENDER_SYS", typeof (string) },
                            { "NO_PIN", typeof (string) },
                            { "UPDATE_USER", typeof (string) },
                            { "DT_UPDATE", typeof (string) },
                            { "ENTER_USER", typeof (string) },
                            { "DT_ENTER", typeof (string) },
                            { "ERR_MSG", typeof (string) },
                            { "CMP_USE_ID", typeof (string) },
                            { "CMP_USE_TXT", typeof (string) },
                            { "CMP_TAX_ST", typeof (string) } }
            };
       }

        public void 인보이스정보등록(object[] obj)
        {
            DBHelper.ExecuteScalar("SP_CZ_SA_IVMNG_INVOICE_I", obj);
        }
    }
}

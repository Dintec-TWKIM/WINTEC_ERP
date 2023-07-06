using System.Data;
using Duzon.BizOn.Erpu.Security;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.UEncryption;

namespace cz
{
    public class P_CZ_MA_PARTNER_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            string[] columns = new string[] { "NO_RES", "NO_DEPOSIT", "E_MAIL" };
            DataType[] dataType = new DataType[] { DataType.Jumin, DataType.Account, DataType.Mail };
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_PARTNER_S", obj);

            dataTable = new UEncryption().SearchDecryptionTable(dataTable, columns, dataType);
            dataTable.AcceptChanges();
            return dataTable;
        }

        internal string Delete(object CD_PARTNER, object OLD_NO_COMPANY)
        {
            return D.GetString(((ResultData)Global.MainFrame.ExecSp("SP_CZ_MA_PARTNER_D_BEFORE", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                                CD_PARTNER,
                                                                                                                OLD_NO_COMPANY,
                                                                                                                string.Empty })).OutParamsSelect[0, 0]);
        }

        internal bool Save(DataTable dt)
        {
            string[] columns = new string[] { "NO_RES", "NO_DEPOSIT", "E_MAIL" };
            DataType[] dataType = new DataType[] { DataType.Jumin, DataType.Account, DataType.Mail };
            DataTable dataTable = new UEncryption().SaveEncryptionTable(dt, columns, dataType);

            SpInfo spInfo = new SpInfo();

            spInfo.DataValue = dataTable;
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;

            spInfo.SpNameInsert = "SP_CZ_MA_PARTNER_I";
            spInfo.SpParamsInsert = new string[] { "CD_PARTNER",
                                                   "CD_COMPANY",
                                                   "LN_PARTNER",
                                                   "DT_OPEN",
                                                   "AM_CAP",
                                                   "MANU_EMP",
                                                   "MANA_EMP",
                                                   "SD_PARTNER",
                                                   "LT_TRANS",
                                                   "SN_PARTNER",
                                                   "EN_PARTNER",
                                                   "TP_PARTNER",
                                                   "FG_PARTNER",
                                                   "NM_CEO",
                                                   "NO_COMPANY",
                                                   "NO_RES",
                                                   "TP_JOB",
                                                   "CLS_JOB",
                                                   "URL_PARTNER",
                                                   "CLS_PARTNER",
                                                   "CD_NATION",
                                                   "STA_CREDIT",
                                                   "E_MAIL",
                                                   "NO_TEL",
                                                   "NO_FAX",
                                                   "NO_POST1",
                                                   "DC_ADS1_H",
                                                   "DC_ADS1_D",
                                                   "NO_TEL1",
                                                   "NO_FAX1",
                                                   "NO_POST2",
                                                   "DC_ADS2_H",
                                                   "DC_ADS2_D",
                                                   "NO_TEL2",
                                                   "NO_FAX2",
                                                   "NO_POST3",
                                                   "DC_ADS3_H",
                                                   "DC_ADS3_D",
                                                   "NO_TEL3",
                                                   "NO_FAX3",
                                                   "NM_PTR",
                                                   "USE_YN",
                                                   "FG_CREDIT",
                                                   "TP_TAX",
                                                   "NO_MERCHANT",
                                                   "NO_DEPOSIT",
                                                   "CD_BANK",
                                                   "ID_COMPANY",
                                                   "PW_COMPANY",
                                                   "CD_PARTNER_GRP",
                                                   "CD_EMP_SALE",
                                                   "CD_EMP_PARTNER",
                                                   "NO_HPEMP_PARTNER",
                                                   "CD_AREA",
                                                   "DT_RCP_PREARRANGED",
                                                   "NM_DEPOSIT",
                                                   "CD_PARTNER_GRP_2",
                                                   "FG_BILL",
                                                   "FG_PAYBILL",
                                                   "DT_PAY_PREARRANGED",
                                                   "CD_CON",
                                                   "ID_INSERT",
                                                   "DT_CLOSE",
                                                   "YN_BIZTAX",
                                                   "NO_BIZTAX",
                                                   "YN_JEONJA",
                                                   "TP_DEFER",
                                                   "TP_RCP_DD",
                                                   "DT_RCP_DD",
                                                   "TP_PAY_DD",
                                                   "DT_PAY_DD",
                                                   "FG_CORP",
                                                   "NM_CURE_AGENCY",
                                                   "NO_CUER_AGENCY",
                                                   "NM_TEXT",
                                                   "TP_NATION",
                                                   "NM_DCDEPOSIT",
                                                   "FG_CORPCODE",
                                                   "DT_RCP_PRETOLERANCE",
                                                   "NO_COR",
                                                   "CD_EXCH1",
                                                   "TP_SO",
                                                   "TP_VAT",
                                                   "FG_BILL1",
                                                   "FG_BILL2",
                                                   "TP_COND_PRICE",
                                                   "CD_SALEGRP",
                                                   "TXT_USERDEF2",
                                                   "TXT_USERDEF3",
                                                   "CD_EXCH2",
                                                   "CD_TPPO",
                                                   "FG_PAYMENT",
                                                   "FG_TAX",
                                                   "TP_UM_TAX",
                                                   "TP_INQ_PO",
                                                   "TP_PO",
                                                   "TP_INQ",
                                                   "TP_QTN",
                                                   "TP_DELIVERY_CONDITION",
                                                   "TP_TERMS_PAYMENT",
                                                   "RT_COMMISSION",
                                                   "DC_COMMISSION",
                                                   "RT_SALES_PROFIT",
                                                   "RT_SALES_DC",
                                                   "RT_PURCHASE_DC",
                                                   "DC_PURCHASE_MAIL",
                                                   "YN_CERT",
                                                   "TP_PRINT",
                                                   "TP_ATTACH_EMAIL",
                                                   "CD_ORIGIN",
                                                   "YN_USE_GIR",
                                                   "YN_GR_LABEL",
                                                   "DC_CEO_E_MAIL",
                                                   "CD_PARTNER_RELATION",
                                                   "NO_VAT",
												   "TP_PRICE_SENS",
                                                   "SHIPSERV_TNID",
                                                   "YN_HOLD",
                                                   "TP_INV",
                                                   "YN_COLOR",
                                                   "TP_PAY",
                                                   "TP_DELIVERY",
                                                   "TP_PACKING"
                                                                };

            spInfo.SpNameUpdate = "SP_CZ_MA_PARTNER_U";
            spInfo.SpParamsUpdate = new string[] { "CD_PARTNER",
                                                   "CD_COMPANY",
                                                   "LN_PARTNER",
                                                   "DT_OPEN",
                                                   "AM_CAP",
                                                   "MANU_EMP",
                                                   "MANA_EMP",
                                                   "SD_PARTNER",
                                                   "LT_TRANS",
                                                   "SN_PARTNER",
                                                   "EN_PARTNER",
                                                   "TP_PARTNER",
                                                   "FG_PARTNER",
                                                   "NM_CEO",
                                                   "NO_COMPANY",
                                                   "NO_RES",
                                                   "TP_JOB",
                                                   "CLS_JOB",
                                                   "URL_PARTNER",
                                                   "CLS_PARTNER",
                                                   "CD_NATION",
                                                   "STA_CREDIT",
                                                   "E_MAIL",
                                                   "NO_TEL",
                                                   "NO_FAX",
                                                   "NO_POST1",
                                                   "DC_ADS1_H",
                                                   "DC_ADS1_D",
                                                   "NO_TEL1",
                                                   "NO_FAX1",
                                                   "NO_POST2",
                                                   "DC_ADS2_H",
                                                   "DC_ADS2_D",
                                                   "NO_TEL2",
                                                   "NO_FAX2",
                                                   "NO_POST3",
                                                   "DC_ADS3_H",
                                                   "DC_ADS3_D",
                                                   "NO_TEL3",
                                                   "NO_FAX3",
                                                   "NM_PTR",
                                                   "USE_YN",
                                                   "FG_CREDIT",
                                                   "TP_TAX",
                                                   "NO_MERCHANT",
                                                   "NO_DEPOSIT",
                                                   "CD_BANK",
                                                   "OLD_NO_COMPANY",
                                                   "ID_COMPANY",
                                                   "PW_COMPANY",
                                                   "CD_PARTNER_GRP",
                                                   "CD_EMP_SALE",
                                                   "CD_EMP_PARTNER",
                                                   "NO_HPEMP_PARTNER",
                                                   "CD_AREA",
                                                   "DT_RCP_PREARRANGED",
                                                   "NM_DEPOSIT",
                                                   "CD_PARTNER_GRP_2",
                                                   "FG_BILL",
                                                   "FG_PAYBILL",
                                                   "DT_PAY_PREARRANGED",
                                                   "CD_CON",
                                                   "ID_UPDATE",
                                                   "DT_CLOSE",
                                                   "YN_BIZTAX",
                                                   "NO_BIZTAX",
                                                   "YN_JEONJA",
                                                   "TP_DEFER",
                                                   "TP_RCP_DD",
                                                   "DT_RCP_DD",
                                                   "TP_PAY_DD",
                                                   "DT_PAY_DD",
                                                   "FG_CORP",
                                                   "NM_CURE_AGENCY",
                                                   "NO_CUER_AGENCY",
                                                   "NM_TEXT",
                                                   "TP_NATION",
                                                   "NM_DCDEPOSIT",
                                                   "FG_CORPCODE",
                                                   "DT_RCP_PRETOLERANCE",
                                                   "NO_COR",
                                                   "CD_EXCH1",
                                                   "TP_SO",
                                                   "TP_VAT",
                                                   "FG_BILL1",
                                                   "FG_BILL2",
                                                   "TP_COND_PRICE",
                                                   "CD_SALEGRP",
                                                   "TXT_USERDEF2",
                                                   "TXT_USERDEF3",
                                                   "CD_EXCH2",
                                                   "CD_TPPO",
                                                   "FG_PAYMENT",
                                                   "FG_TAX",
                                                   "TP_UM_TAX",
                                                   "TP_INQ_PO",
                                                   "TP_PO",
                                                   "TP_INQ",
                                                   "TP_QTN",
                                                   "TP_DELIVERY_CONDITION",
                                                   "TP_TERMS_PAYMENT",
                                                   "RT_COMMISSION",
                                                   "DC_COMMISSION",
                                                   "RT_SALES_PROFIT",
                                                   "RT_SALES_DC",
                                                   "RT_PURCHASE_DC",
                                                   "DC_PURCHASE_MAIL",
                                                   "YN_CERT",
                                                   "TP_PRINT",
                                                   "TP_ATTACH_EMAIL",
                                                   "CD_ORIGIN",
                                                   "YN_USE_GIR",
                                                   "YN_GR_LABEL",
                                                   "DC_CEO_E_MAIL",
                                                   "CD_PARTNER_RELATION",
                                                   "NO_VAT",
												   "TP_PRICE_SENS",
                                                   "SHIPSERV_TNID",
                                                   "YN_HOLD",
                                                   "TP_INV",
                                                   "YN_COLOR",
                                                   "TP_PAY",
                                                   "TP_DELIVERY",
                                                   "TP_PACKING"
                                                                };

            spInfo.SpNameDelete = "SP_CZ_MA_PARTNER_D";
            spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
                                                   "CD_PARTNER",
                                                   "INSERT_ID" };

            spInfo.SpParamsValues.Add(ActionState.Delete, "INSERT_ID", Global.MainFrame.LoginInfo.UserID);
            spInfo.SpParamsValues.Add(ActionState.Delete, "SERVER_KEY", Global.MainFrame.ServerKey);

            return DBHelper.Save(spInfo);
        }

        internal void 주민번호체크(string 주민번호, ref string 등록유무)
        {
            if (주민번호 == "" || 주민번호 == null)
            {
                등록유무 = "N";
            }
            else
            {
                object[] outParameters = null;
                DBHelper.GetDataTable("SP_CZ_MA_PARTNER_S_N", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             주민번호 }, out outParameters);
                등록유무 = D.GetString(outParameters[0]);
            }
        }

        internal void 사업자등록번호체크(string 사업자번호, out string 거래처코드, out string 거래처명)
        {
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT CD_PARTNER, LN_PARTNER 
                                                          FROM MA_PARTNER WITH(NOLOCK)  
                                                          WHERE NO_COMPANY = '" + 사업자번호 + "'" +
                                                        @"AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");
            if (dataTable.Rows.Count == 0)
            {
                거래처코드 = 거래처명 = string.Empty;
            }
            else
            {
                거래처코드 = D.GetString(dataTable.Rows[0]["CD_PARTNER"]);
                거래처명 = D.GetString(dataTable.Rows[0]["LN_PARTNER"]);
            }
        }

        internal DataTable GetPost(string PostNo)
        {
            return DBHelper.GetDataTable(@"SELECT NO_POST, NM_ADDRESS 
                                           FROM CM_POST2 WITH(NOLOCK)  
                                           WHERE NO_POST = '" + PostNo + "'");
        }

        internal DataTable Get지급관리()
        {
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT CD_PAYMENT AS CODE, NM_PAYMENT AS NAME 
                                                          FROM FI_PAYMENT WITH(NOLOCK)  
                                                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                                        @"ORDER BY CD_PAYMENT ASC");
            DataRow row = dataTable.NewRow();
            row["CODE"] = "";
            row["NAME"] = "";
            dataTable.Rows.InsertAt(row, 0);
            dataTable.AcceptChanges();
            return dataTable;
        }

        internal DataTable DataCode()
        {
            return DBHelper.GetDataTable(@"SELECT CD_SYSDEF 
                                           FROM MA_CODEDTL WITH(NOLOCK)  
                                           WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + 
                                         @"AND CD_FIELD IN ('MA_B000001', 
                                                            'MA_B000094',
                                                            'MA_B000095',
                                                            'MA_B000043',
                                                            'MA_B000073',
                                                            'HR_T000003',
                                                            'MA_B000003',
                                                            'MA_B000002',
                                                            'MA_B000065',
                                                            'MA_B000067',
                                                            'SA_B000018',
                                                            'SA_B000002',
                                                            'PU_C000044',
                                                            'MA_B000097',
                                                            'MA_B000096',
                                                            'MA_E000001',
                                                            'MA_B000020',
                                                            'MA_B000062')");
        }

        internal string GetEnv_PartnerSeq()
        {
            string str1 = string.Empty;
            string str2 = "Y";
            DataTable dataTable = null;

            if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
                dataTable = DBHelper.GetDataTable(@"SELECT ISNULL(YN_PARTNERSEQ,'Y') AS YN_PARTNERSEQ 
                                                    FROM MA_ENV WITH(NOLOCK)  
                                                    WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");
            else
                dataTable = DBHelper.GetDataTable(@"SELECT NVL(YN_PARTNERSEQ,'Y') AS YN_PARTNERSEQ 
                                                    FROM MA_ENV WITH(NOLOCK)  
                                                    WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");

            if (dataTable != null || dataTable.Rows.Count != 0)
                str2 = D.GetString(dataTable.Rows[0]["YN_PARTNERSEQ"]);
            
            return str2;
        }
    }
}
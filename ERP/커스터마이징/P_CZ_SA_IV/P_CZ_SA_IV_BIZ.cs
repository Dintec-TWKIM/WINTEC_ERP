using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Dintec;
using System.IO;
using System;

namespace cz
{
    internal class P_CZ_SA_IV_BIZ
    {
        public DataTable Search()
        {
            return new DataTable()
            {
                Columns = { { "S", typeof (string) },
                            { "NO_IV", typeof (string) },
                            { "NO_IO", typeof (string) },
                            { "PARTNER_SOURCE", typeof (string) },
                            { "CD_PARTNER", typeof (string) },
                            { "LN_PARTNER", typeof (string) },
                            { "BILL_PARTNER", typeof (string) },
                            { "BILL_LN_PARTNER", typeof (string) },
                            { "FG_TAX", typeof (string) },
                            { "FG_TAX_VAT", typeof (string) },
                            { "AM_K", typeof (decimal) },
                            { "VAT_TAX", typeof (decimal) },
                            { "AM_TOTAL", typeof (decimal) },
                            { "VAT_MINUS", typeof (decimal) },
                            { "DT_RCP_RSV", typeof (string) },
                            { "DC_REMARK", typeof (string) },
                            { "CD_BIZAREA", typeof (string) },
                            { "NO_BIZAREA", typeof (string) },
                            { "TP_FD", typeof (string) },
                            { "TP_RECEIPT", typeof (string) },
                            { "TP_SUMTAX", typeof (string) },
                            { "TP_IV", typeof (string) },
                            { "FG_TAXP", typeof (string) },
                            { "TP_AIS", typeof (string) },
                            { "YN_EXPIV", typeof (string) },
                            { "NO_LC", typeof (string) },
                            { "DT_RCP_PREARRANGED", typeof (decimal) },
                            { "MAX_LINE", typeof (decimal) },
                            { "CD_EXCH", typeof (string) },
                            { "RT_EXCH", typeof (decimal) },
                            { "AM_EX", typeof (decimal) },
                            { "FG_MAP", typeof (string) },
                            { "CD_PJT", typeof (string) },
                            { "CD_BIZAREA_TAX", typeof (string) },
                            { "FG_BILL", typeof (string) },
                            { "CD_DOCU", typeof (string) },
                            { "CD_CON", typeof (string) },
                            { "AM_TOT", typeof (decimal) },
                            { "ETAX_SEND_TYPE", typeof (string) },
                            { "ETAX_SELL_DAM_NM", typeof (string) },
                            { "ETAX_SELL_DAM_MOBIL", typeof (string) },
                            { "ETAX_SELL_DAM_EMAIL", typeof (string) },
                            { "NM_PTR", typeof (string) },
                            { "EX_EMIL", typeof (string) },
                            { "EX_HP", typeof (string) },
                            { "NO_IV_PRE", typeof (string) },
                            { "AMT_MAX", typeof (decimal) },
                            { "FG_CLS_TYPE", typeof (string) },
                            { "AMT_DC", typeof (decimal) },
                            { "FG_AR_EXC", typeof (string) },
                            { "SH_T_TP_BILL", typeof (string) },
                            { "SH_T_TP_CD", typeof (string) },
                            { "DT_RCP_PRETOLERANCE", typeof (decimal) },
                            { "DT_RCP_RSV1", typeof (string) },
                            { "CD_PJTLINE", typeof (decimal) },
                            { "CD_UNIT", typeof (string) },
                            { "NM_UNIT", typeof (string) },
                            { "STND_UNIT", typeof (string) },
                            { "CD_ITEM_REF", typeof (string) },
                            { "NM_ITEM_REF", typeof (string) },
                            { "STND_ITEM_REF", typeof (string) },
                            { "YN_PICKING", typeof (string) },
                            { "NM_USERDEF1", typeof (string) },
                            { "TXT_USERDEF1",  typeof (string) },
                            { "TXT_USERDEF2", typeof (string) },
                            { "TXT_USERDEF3", typeof (string) },
                            { "YN_AUTO_NUM", typeof (string) } }
            };
        }

        public DataTable SearchAuto(object[] obj)
		{
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_IV_AUTO_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public bool SaveAutoLog(object[] obj)
		{
            return DBHelper.ExecuteNonQuery("SP_CZ_SA_IV_AUTO_LOG_I", obj);
        }

        public bool Save(DataTable dtH, DataTable dtL, string 거래구분, string 매출일자, string 부서, string 작성자)
        {
            SpInfoCollection spInfoCollection = new SpInfoCollection();
            SpInfo spInfo;

            if (dtH != null)
            {
                dtH.RemotingFormat = SerializationFormat.Binary;
                spInfo = new SpInfo();
                spInfo.DataValue = dtH;
                spInfo.DataState = DataValueState.Added;
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
                spInfo.SpNameInsert = "UP_SA_IV_INSERT";
                spInfo.SpParamsInsert = new string[46] { "CD_COMPANY",
                                                         "NO_IV",
                                                         "NO_BIZAREA",
                                                         "CD_BIZAREA",
                                                         "CD_PARTNER",
                                                         "BILL_PARTNER",
                                                         "FG_TRANS",
                                                         "AM_K",
                                                         "VAT_TAX",
                                                         "DT_PROCESS",
                                                         "FG_TAX",
                                                         "TP_FD",
                                                         "TP_RECEIPT",
                                                         "TP_SUMTAX",
                                                         "FG_TAXP",
                                                         "TP_AIS",
                                                         "CD_DEPT",
                                                         "NO_EMP",
                                                         "YN_EXPIV",
                                                         "NO_LC",
                                                         "DT_RCP_RSV",
                                                         "DC_REMARK",
                                                         "ID_INSERT",
                                                         "CD_EXCH",
                                                         "RT_EXCH",
                                                         "AM_EX",
                                                         "FG_MAP",
                                                         "CD_BIZAREA_TAX",
                                                         "FG_BILL",
                                                         "CD_DOCU",
                                                         "ETAX_SEND_TYPE",
                                                         "ETAX_SELL_DAM_NM",
                                                         "ETAX_SELL_DAM_EMAIL",
                                                         "ETAX_SELL_DAM_MOBIL",
                                                         "NM_PTR",
                                                         "EX_EMIL",
                                                         "EX_HP",
                                                         "FG_CLS_TYPE",
                                                         "FG_AR_EXC",
                                                         "SH_T_TP_BILL",
                                                         "SH_T_TP_CD",
                                                         "DT_RCP_RSV1",
                                                         "NM_USERDEF1",
                                                         "TXT_USERDEF1",
                                                         "TXT_USERDEF2",
                                                         "TXT_USERDEF3" };

                spInfo.SpParamsValues.Add(ActionState.Insert, "FG_TRANS", 거래구분);
                spInfo.SpParamsValues.Add(ActionState.Insert, "DT_PROCESS", 매출일자);
                spInfo.SpParamsValues.Add(ActionState.Insert, "CD_DEPT", 부서);
                spInfo.SpParamsValues.Add(ActionState.Insert, "NO_EMP", 작성자);

                spInfoCollection.Add(spInfo);
            }

            if (dtL != null)
            {
                spInfo = new SpInfo();
                spInfo.DataValue = Util.GetXmlTable(dtL);
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;

                spInfo.SpNameInsert = "SP_CZ_SA_IVL_XML";
                spInfo.SpParamsInsert = new string[] { "CD_COMPANY", "XML", "DT_TAX", "ID_INSERT" };

                spInfo.SpParamsValues.Add(ActionState.Insert, "DT_TAX", 매출일자);

                spInfoCollection.Add(spInfo);
            }

            //if (dtH != null)
            //{
            //    spInfo = new SpInfo();
            //    spInfo.DataValue = dtH;
            //    spInfo.CompanyID = MA.Login.회사코드;
            //    spInfo.UserID = MA.Login.사용자아이디;
            //    spInfo.SpNameInsert = "UP_SA_IV_MODIFY";
            //    spInfo.SpParamsInsert = new string[] { "NO_IV",
            //                                           "CD_COMPANY" };
            //    spInfoCollection.Add(spInfo);
            //}

            DBHelper.Save(spInfoCollection);
            return true;
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

        public DataSet 포장데이터(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_GIRSCH_PACK_S", obj);
            T.SetDefaultValue(ds);
            return ds;
        }
    }
}

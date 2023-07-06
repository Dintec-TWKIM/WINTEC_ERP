using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
    class P_CZ_MA_EXCHANGE_BIZ
    {
        internal DataTable MaxNoSeq(string YM)
        {
            string str = string.Empty;
            string sql;

            sql = @"SELECT 0 CODE,
                           '' NAME 
                    UNION ALL
                    SELECT NO_SEQ CODE,
                           CONVERT(NVARCHAR, NO_SEQ) NAME
                    FROM MA_EXCHANGE WITH(NOLOCK)
                    WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                   "AND YYMMDD LIKE '" + YM + "%'" + Environment.NewLine +
                   "GROUP BY NO_SEQ ORDER BY CODE ASC";

            return DBHelper.GetDataTable(sql);
        }

        internal DataTable Search(string 년월, decimal 고시회차, string 외화화폐, string 원화화폐)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_EXCHANGE_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                              Global.MainFrame.LoginInfo.Language,
                                                                                              년월,
                                                                                              고시회차,
                                                                                              외화화폐,
                                                                                              원화화폐 });

            dataTable.Columns["RATE_BASE"].DefaultValue = 0;
            dataTable.Columns["RATE_BUY"].DefaultValue = 0;
            dataTable.Columns["RATE_SALE"].DefaultValue = 0;
            dataTable.Columns["CD_COMPANY"].DefaultValue = Global.MainFrame.LoginInfo.CompanyCode;

            return dataTable;
        }

        internal void Save(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;
            si.SpNameInsert = "SP_CZ_MA_EXCHANGE_I";
            si.SpParamsInsert = new string[] { "CD_COMPANY",
                                               "YYMMDD",
                                               "NO_SEQ",
                                               "QUOTATION_TIME",
                                               "CURR_SOUR",
                                               "CURR_DEST",
                                               "RATE_BASE",
                                               "RATE_SALE",
                                               "RATE_BUY",
                                               "RATE_PURCHASE",
                                               "RATE_SALES",
                                               "ID_INSERT" };

            si.SpNameUpdate = "SP_CZ_MA_EXCHANGE_U";
            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "YYMMDD",
                                               "NO_SEQ",
                                               "QUOTATION_TIME",
                                               "CURR_SOUR",
                                               "CURR_DEST",
                                               "RATE_BASE",
                                               "RATE_SALE",
                                               "RATE_BUY",
                                               "RATE_PURCHASE",
                                               "RATE_SALES",
                                               "ID_UPDATE" };

            si.SpNameDelete = "SP_CZ_MA_EXCHANGE_D";
            si.SpParamsDelete = new string[] { "CD_COMPANY",
                                               "YYMMDD",
                                               "CURR_SOUR",
                                               "CURR_DEST",
                                               "NO_SEQ" };

            DBHelper.Save(si);
        }

        internal void ExcRateInfo(DataTable dtExcRateInfo)
        {
            if (dtExcRateInfo == null || dtExcRateInfo.Rows.Count == 0) return;

            foreach (DataRow dataRow in dtExcRateInfo.Rows)
            {
                DBHelper.ExecuteNonQuery("SP_CZ_MA_EXCHANGE_RATE_INFO", new object[] { dataRow["YYMMDD"],
                                                                                       dataRow["NO_SEQ"],
                                                                                       dataRow["QUOTATION_TIME"],
                                                                                       dataRow["CURR_SOUR"],
                                                                                       dataRow["CURR_DEST"],
                                                                                       Global.MainFrame.LoginInfo.CompanyCode,
                                                                                       dataRow["RATE_BASE"],
                                                                                       dataRow["RATE_SALE"],
                                                                                       dataRow["RATE_BUY"],
                                                                                       Global.MainFrame.LoginInfo.UserID });
            }
        }

        internal void 환율정보동기화(object[] obj)
        {
            DBHelper.ExecuteScalar("SP_CZ_MA_EXCHANGE_SYNC", obj);
        }
    }
}

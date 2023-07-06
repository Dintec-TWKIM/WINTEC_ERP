using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace master
{
    class P_MA_PROJECT_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_MA_PROJECTH_S", obj, "NO_PROJECT");
            dt.Columns["YN_USE"].DefaultValue = "Y"; 
            return dt;
        }

        internal void Save(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            SpInfo si = new SpInfo();
            si.DataValue = dt;
            
            si.SpNameInsert = "UP_MA_PROJECTH_I";
            si.SpParamsInsert = new string[] {  "NO_PROJECT",   "NO_SEQ",       "CD_COMPANY",   "NM_PROJECT",   "CD_PARTNER",   "CD_BIZAREA",   "CD_SALEGRP",   "NO_EMP",
											    "AM_CONTRACT",  "CD_EXCH",      "RT_EXCH",      "FG_VAT",       "AM_BASE",      "AM_PROFIT",    "AM_WONAMT",    "AM_MATERIAL", 
                                                "AM_LABOR",     "AM_MANUFACT",  "AM_EXPENSE",   "SD_PROJECT",   "ED_PROJECT",   "STA_PROJECT",  "DT_CHANGE",    "DC_RMK", 
                                                "ID_INSERT",    "ID_UPDATE",    "AM_VAT",       "AM_HAP",       "DT_SHIP",      "DT_DUE",       "CD_MNG_DT1",   "CD_MNG_DT2", 
                                                "CD_MNG_DT3",   "CD_MNG_DT4",   "CD_MNG_AM1",   "CD_MNG_AM2",   "CD_MNG_AM3",   "CD_MNG_AM4",   "CD_MNG1",      "CD_MNG2", 
                                                "CD_MNG3",      "CD_MNG4",      "CD_BUDGET",    "FG_PJT1",      "FG_PJT2",      "FG_PJT3",      "FG_PJT4",      "FG_PJT5", 
                                                "CD_DEPT",      "YN_USE"};

            si.SpNameUpdate = "UP_MA_PROJECTH_U";
            si.SpParamsUpdate = new string[] {  "NO_PROJECT",   "NO_SEQ",       "CD_COMPANY",   "NM_PROJECT",   "CD_PARTNER",   "CD_BIZAREA",   "CD_SALEGRP",   "NO_EMP",
											    "AM_CONTRACT",  "CD_EXCH",      "RT_EXCH",      "FG_VAT",       "AM_BASE",      "AM_PROFIT",    "AM_WONAMT",    "AM_MATERIAL", 
                                                "AM_LABOR",     "AM_MANUFACT",  "AM_EXPENSE",   "SD_PROJECT",   "ED_PROJECT",   "STA_PROJECT",  "DT_CHANGE",    "DC_RMK",   
                                                "ID_INSERT",    "ID_UPDATE",    "AM_VAT",       "AM_HAP",       "DT_SHIP",      "DT_DUE",       "CD_MNG_DT1",   "CD_MNG_DT2", 
                                                "CD_MNG_DT3",   "CD_MNG_DT4",   "CD_MNG_AM1",   "CD_MNG_AM2",   "CD_MNG_AM3",   "CD_MNG_AM4",   "CD_MNG1",      "CD_MNG2", 
                                                "CD_MNG3",      "CD_MNG4",      "CD_BUDGET",    "FG_PJT1",      "FG_PJT2",      "FG_PJT3",      "FG_PJT4",      "FG_PJT5", 
                                                "CD_DEPT",      "YN_USE" };

            si.SpNameDelete = "UP_MA_PROJECTH_D";
            si.SpParamsDelete = new string[] {  "NO_PROJECT", "NO_SEQ", "CD_COMPANY" };

            DBHelper.Save(si);
        }

        internal DataTable IsFileHelpCheck()
        {
            string query = string.Empty;

            query = " SELECT TP_FILESERVER"
                  + " FROM   MA_ENV "
                  + " WHERE  CD_COMPANY = '" + MA.Login.회사코드 + "'";

            DataTable dt = DBHelper.GetDataTable(query);
            return dt;
        }
    }
}
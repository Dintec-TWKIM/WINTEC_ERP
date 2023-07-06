using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;

namespace sale
{
    class P_SA_TYPE041_BIZ
    {
        public bool Save(DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;
            si.SpNameInsert = "UP_SA_TPSO_INSERT";			// Insert 프로시저명
            si.SpNameUpdate = "UP_SA_TPSO_UPDATE";			// Update 프로시저명
            si.SpNameDelete = "UP_SA_TPSO_DELETE";			// Delete 프로시저명
            si.SpParamsInsert = new string[] { "TP_SO", "CD_COMPANY", "NM_SO", "CONF", "TRADE", "YN_USE", "SUBCONT", "TP_IV", "TP_GI", "TP_BUSI", "RET", "TP_VAT", "CD_CC", "AUTO_PROCESS", "ID_INSERT" };
            si.SpParamsUpdate = new string[] { "TP_SO", "CD_COMPANY", "NM_SO", "CONF", "TRADE", "YN_USE", "SUBCONT", "TP_IV", "TP_GI", "TP_BUSI", "RET", "TP_VAT", "CD_CC", "AUTO_PROCESS", "ID_UPDATE" };
            si.SpParamsDelete = new string[] { "TP_SO", "CD_COMPANY" };

            return DBHelper.Save(si);
        }

        public DataTable Search()
        {
            object[] obj = new Object[] { Global.MainFrame.LoginInfo.CompanyCode };
            DataTable dt = DBHelper.GetDataTable("UP_SA_TPSO_SELECT", obj);

            foreach (DataColumn Col in dt.Columns)
            {
                if (Col.DataType == Type.GetType("System.Decimal"))
                    Col.DefaultValue = 0;
            }

            return dt;
        }
    }
}
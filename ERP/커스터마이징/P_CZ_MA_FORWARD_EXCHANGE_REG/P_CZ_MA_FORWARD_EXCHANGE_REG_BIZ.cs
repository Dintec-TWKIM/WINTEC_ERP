using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using Dintec;

namespace cz
{
    internal class P_CZ_MA_FORWARD_EXCHANGE_REG_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_MA_FORWARD_EXCHANGE_REG_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return false;

            SpInfo si = new SpInfo();

            si.DataValue = Util.GetXmlTable(dt);
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameInsert = "SP_CZ_MA_FORWARD_EXCHANGE_REG_XML";
            si.SpParamsInsert = new string[] { "XML", "CD_COMPANY", "ID_INSERT" };

            return DBHelper.Save(si);
        }
    }
}

using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace cz
{
    internal class P_CZ_SA_CRM_PARTNER_HIST_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CRM_PARTNER_HIST_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

        internal bool Save(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();

            spInfo.DataValue = Util.GetXmlTable(dt);
            spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            spInfo.UserID = Global.MainFrame.LoginInfo.UserID;

            spInfo.SpNameInsert = "SP_CZ_SA_CRM_PARTNER_HIST_XML";
            spInfo.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

            return DBHelper.Save(spInfo);
        }
    }
}

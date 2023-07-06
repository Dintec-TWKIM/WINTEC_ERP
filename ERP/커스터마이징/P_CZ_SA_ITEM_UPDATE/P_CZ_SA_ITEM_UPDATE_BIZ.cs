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
    internal class P_CZ_SA_ITEM_UPDATE_BIZ
    {
        internal bool Save(DataTable dt)
        {
            SpInfo si = new SpInfo();

            si.DataValue = Util.GetXmlTable(dt);
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameInsert = "SP_CZ_SA_ITEM_UPDATE_XML";
            si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

            return DBHelper.Save(si);
        }
    }
}

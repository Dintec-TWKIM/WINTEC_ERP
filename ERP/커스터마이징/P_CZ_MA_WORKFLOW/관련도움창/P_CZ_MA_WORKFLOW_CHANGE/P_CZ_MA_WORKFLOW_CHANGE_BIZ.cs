using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.Common.Util;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_MA_WORKFLOW_CHANGE_BIZ
    {
        internal bool Save(DataTable dt)
        {
            if (dt != null && dt.Rows.Count != 0)
            {
                SpInfo si = new SpInfo();

                si.DataValue = Util.GetXmlTable(dt);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_MA_WORKFLOW_CHANGE_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

                return DBHelper.Save(si);
            }

            return false;
        }
    }
}

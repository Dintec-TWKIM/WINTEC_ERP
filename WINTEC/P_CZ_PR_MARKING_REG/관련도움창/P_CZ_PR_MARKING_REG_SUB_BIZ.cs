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
	internal class P_CZ_PR_MARKING_REG_SUB_BIZ
	{
        public DataTable Search(object[] obj)
        {
            return DBHelper.GetDataTable("SP_CZ_PR_MARKING_REG_SUB_S", obj);
        }

        internal bool Save(DataTable dt)
        {
            SpInfo si = new SpInfo();

            if (dt != null)
            {
                si.DataValue = Util.GetXmlTable(dt);
                si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_PR_MARKING_REG_SUB_XML";
                si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };
            }

            return DBHelper.Save(si);
        }
    }
}

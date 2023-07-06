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
    internal class P_CZ_SA_PTR_PLAN_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_PTR_PLAN_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        internal bool Save(DataTable dt)
        {
            SpInfo si = new SpInfo();

            si.DataValue = Util.GetXmlTable(dt);
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameInsert = "SP_CZ_SA_PTR_PLAN_XML";
            si.SpParamsInsert = new string[] { "XML", "CD_COMPANY", "ID_INSERT" };

            return DBHelper.Save(si);
        }
    }
}

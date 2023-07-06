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
    class P_CZ_SA_GI_STOCK_BIZ
    {
        public DataTable Search(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GI_STOCK_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public bool Save(DataTable dt)
        {
            SpInfo spInfo = new SpInfo();

            if (dt != null)
            {
                spInfo.DataValue = Util.GetXmlTable(dt);
                spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                spInfo.UserID = Global.MainFrame.LoginInfo.UserID;

                spInfo.SpNameInsert = "SP_CZ_SA_GI_STOCK_XML";
                spInfo.SpParamsInsert = new string[] { "XML", "ID_INSERT" };
            }

            return DBHelper.Save(spInfo);
        }
    }
}

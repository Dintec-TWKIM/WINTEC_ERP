using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Duzon.ERPU;
using System.Data;
using Duzon.Common.Util;
using Dintec;
using Duzon.Common.Forms;

namespace cz
{
    internal class P_CZ_PR_MATCHING_REG_BIZ
    {
        public DataSet Search(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("SP_CZ_PR_MATCHING_REG_S", obj);
            T.SetDefaultValue(ds);
            return ds;
        }

        internal bool Save(string 공장코드, DataTable dt)
        {
            SpInfo si = new SpInfo();

            si.DataValue = Util.GetXmlTable(dt);
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameInsert = "SP_CZ_PR_MATCHING_REG_XML";
            si.SpParamsInsert = new string[] { "CD_COMPANY", "CD_PLANT", "XML", "ID_INSERT" };

            si.SpParamsValues.Add(ActionState.Insert, "CD_PLANT", 공장코드);

            return DBHelper.Save(si);
        }
    }
}

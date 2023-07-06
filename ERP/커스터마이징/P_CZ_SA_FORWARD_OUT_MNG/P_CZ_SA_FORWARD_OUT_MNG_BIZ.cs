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
	internal class P_CZ_SA_FORWARD_OUT_MNG_BIZ
	{
		public DataTable Search(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_FORWARD_OUT_MNG_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchHeader(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_FORWARD_OUT_MNGH_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

		public DataTable SearchLine(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_FORWARD_OUT_MNGL_S", obj);
			T.SetDefaultValue(dt);
			return dt;
		}

        internal bool Save(DataTable dt)
        {
			SpInfo si = new SpInfo();

			si.DataValue = Util.GetXmlTable(dt);
			//si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
			si.UserID = Global.MainFrame.LoginInfo.UserID;

			si.SpNameInsert = "SP_CZ_SA_FORWARD_OUT_MNG_XML";
			si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };

			return DBHelper.Save(si);
        }
    }
}

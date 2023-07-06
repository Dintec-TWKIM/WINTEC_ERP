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
	internal class P_CZ_PU_IV_CONFIRM_MNG_BIZ
	{
		public DataTable Search(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PU_IV_CONFIRM_MNG_S", obj);
		}

		internal bool Save(DataTable dt)
		{
			SpInfo si = new SpInfo();

			if (dt != null && dt.Rows.Count != 0)
			{
				si.DataValue = Util.GetXmlTable(dt);
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.UserID;

				si.SpNameInsert = "SP_CZ_PU_IV_CONFIRM_XML";
				si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };
			}

			return DBHelper.Save(si);
		}
	}
}

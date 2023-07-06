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
	internal class P_CZ_PR_WO_SA_SOL_CHANGE_BIZ
	{
		public DataTable Search(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PR_WO_SA_SOL_CHANGE_S", obj);
		}

		public DataTable Search1(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PR_WO_SA_SOL_CHANGE_S1", obj);
		}

		public DataTable Search2(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PR_WO_SA_SOL_CHANGE_S2", obj);
		}

		public DataTable Search3(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PR_WO_SA_SOL_CHANGE_S3", obj);
		}

		public DataTable Search4(object[] obj)
		{
			return DBHelper.GetDataTable("SP_CZ_PR_WO_SA_SOL_CHANGE_S4", obj);
		}

		internal bool Save(DataTable dt)
        {
			SpInfo si = new SpInfo();

			if (dt != null && dt.Rows.Count != 0)
			{
				si.DataValue = Util.GetXmlTable(dt);
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.UserID;

				si.SpNameInsert = "SP_CZ_PR_WO_SA_SOL_CHANGE_XML";
				si.SpParamsInsert = new string[] { "XML", "ID_INSERT" };
			}

			return DBHelper.Save(si);
        }
    }
}

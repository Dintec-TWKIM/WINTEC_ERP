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
    internal class P_CZ_SA_WOODEN_PACK_RPT_BIZ
    {
        internal DataTable Search(object[] obj)
        {
            DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_WOODEN_PACK_RPT_S", obj);

            T.SetDefaultValue(dataTable);
            return dataTable;
        }

		internal bool Save(DataTable dt)
		{
			SpInfo si = new SpInfo();

			si.DataValue = dt;
			si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
			si.UserID = Global.MainFrame.LoginInfo.UserID;

			si.SpNameUpdate = "SP_CZ_SA_WOODEN_PACK_RPT_U";
			si.SpParamsUpdate = new string[]
			{
				"CD_COMPANY",
				"NO_GIR",
				"NO_PACK",
				"QT_NET_WEIGHT",
				"QT_GROSS_WEIGHT",
				"QT_WIDTH",
				"QT_LENGTH",
				"QT_HEIGHT",
				"ID_UPDATE"
			};

			si.SpNameDelete = "SP_CZ_SA_PACK_MNG_D";
			si.SpParamsDelete = new string[]
			{
				"CD_COMPANY",
				"NO_GIR",
				"NO_PACK",
				"ID_DELETE"
			};

			si.SpParamsValues.Add(ActionState.Delete, "ID_DELETE", Global.MainFrame.LoginInfo.UserID);

			return DBHelper.Save(si);
		}

		public bool 회계전표처리(object[] obj)
		{
			return DBHelper.ExecuteNonQuery("SP_CZ_SA_WOODEN_PACK_DOCU", obj);
		}

		public bool 회계전표취소(string 전표유형, string 전표번호)
		{
			return DBHelper.ExecuteNonQuery("SP_CZ_FI_DOCU_AUTODEL", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					전표유형,
																					전표번호,
																					Global.MainFrame.LoginInfo.UserID });
		}
	}
}

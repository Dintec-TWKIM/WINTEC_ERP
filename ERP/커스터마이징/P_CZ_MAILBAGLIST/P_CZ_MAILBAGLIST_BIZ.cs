using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using DX;
using System.Data;

namespace cz
{
	class P_CZ_MAILBAGLIST_BIZ
	{
		public DataTable Search_리스트(object[] obj)
		{
			return DBHelper.GetDataTable("CZ_MAILBAGLIST_SH", obj);
		}
		public DataTable Search_작성(object[] obj)
		{
			return DBHelper.GetDataTable("CZ_MAILBAGLIST_SL", obj);
		}
		public DataTable Msg_Send(object[] obj)
		{
			return DBHelper.GetDataTable("CZ_MAILBAGLIST_MSG", obj);
		}

		internal bool Save_H(DataTable dt)
		{
			SpInfo si = new SpInfo();
			si.DataValue = dt;
			si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
			si.UserID = Global.MainFrame.LoginInfo.UserID;
			si.SpNameInsert = "CZ_MAILBAGLIST_IH";
			si.SpNameUpdate = "CZ_MAILBAGLIST_UH";
			si.SpNameDelete = "CZ_MAILBAGLIST_DH";
			si.SpParamsInsert = new string[] {	"CD_COMPANY",
												"NO_EMP",
												"CD_SEND",
												"DT_ACCT"};
			si.SpParamsUpdate = new string[] {  "CD_COMPANY",
												"NO_EMP",
												"CD_SEND",
												"DT_ACCT"};
			si.SpParamsDelete = new string[] {  "CD_SEND",
												"DT_ACCT"};

			return DBHelper.Save(si);
		}

		internal bool Save_L(DataTable dt)
		{
			SpInfo si = new SpInfo();
			si.DataValue = dt;
			si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
			si.UserID = Global.MainFrame.LoginInfo.UserID;
			si.SpNameInsert = "CZ_MAILBAGLIST_IL";
			si.SpNameUpdate = "CZ_MAILBAGLIST_UL";
			si.SpNameDelete = "CZ_MAILBAGLIST_DL";
			si.SpParamsInsert = new string[] { 	"CD_SEND",
												"DT_ACCT",
												"SEQ",
												"NM_ITEM",
												"QT",
												"NO_EMP_SEND",
												"NO_EMP_RECEIVE",
												"NO_EMP_INSPECT",
												"DC_RMK",
												"DTS_INSERT",
												"ID_INSERT"};
			si.SpParamsUpdate = new string[] {  "CD_SEND",
												"DT_ACCT",
												"SEQ",
												"NM_ITEM",
												"QT",
												"NO_EMP_SEND",
												"NO_EMP_RECEIVE",
												"NO_EMP_INSPECT",
												"DC_RMK",
												"DTS_UPDATE",
												"ID_UPDATE"};
			si.SpParamsDelete = new string[] {  "CD_SEND",
												"DT_ACCT",
												"SEQ"};

			return DBHelper.Save(si);
		}

	}
}

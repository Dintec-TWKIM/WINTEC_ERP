using System.Data;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace cz
{
	public class P_CZ_SA_CLAIM_BIZ
	{
		internal DataTable Search(object[] obj)
		{
			DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CLAIMH_S", obj);

			T.SetDefaultValue(dataTable);
			return dataTable;
		}

		internal DataTable SearchDetail(object[] obj)
		{
			DataTable dataTable = DBHelper.GetDataTable("SP_CZ_SA_CLAIML_S", obj);

			T.SetDefaultValue(dataTable);
			return dataTable;
		}

		public bool Save(DataTable dtH, DataTable dtL)
		{
			SpInfoCollection sc = new SpInfoCollection();

			if (dtH != null && dtH.Rows.Count > 0)
			{
				SpInfo si = new SpInfo();
				si.DataValue = dtH;
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.UserID;
				si.SpNameInsert = "SP_CZ_SA_CLAIMH_I";
				si.SpParamsInsert = new string[] { "CD_COMPANY", 
												   "NO_CLAIM", 
												   "NO_SO", 
												   "CD_STATUS", 
												   "NO_IMO", 
												   "CD_PARTNER", 
												   "NO_SALES_EMP",
												   "TP_CLAIM", 
												   "TP_CAUSE", 
												   "TP_ITEM", 
												   "AM_ITEM_RCV", 
												   "AM_ADD_RCV", 
												   "AM_ITEM_PRO", 
												   "AM_ADD_PRO", 
												   "AM_ITEM_CLS", 
												   "AM_ADD_CLS", 
												   "DC_PROGRESS", 
												   "DC_RESULT", 
												   "DC_PREVENTION", 
												   "DC_CLOSING", 
												   "NO_EMP",
												   "DT_INPUT", 
                                                   "DT_EXPECTED_CLOSING_PRO",
												   "DT_EXPECTED_CLOSING",
												   "DT_CLOSING",
                                                   "CD_SUPPLIER_REWARD",
                                                   "DC_SUPPLIER_REWARD",
                                                   "DC_CREDIT_NOTE",
                                                   "CD_CREDIT_EXCH",
                                                   "AM_CREDIT",
												   "TP_REASON",
												   "TP_RETURN",
												   "TP_REQUEST",
												   "DC_RECEIVE",
												   "IMAGE1",
												   "IMAGE2",
												   "IMAGE3",
												   "IMAGE4",
												   "IMAGE5",
												   "IMAGE6",
												   "DC_IMAGE1",
												   "DC_IMAGE2",
												   "DC_IMAGE3",
												   "DC_IMAGE4",
												   "DC_IMAGE5",
												   "DC_IMAGE6",
												   "QT_PACK_WEIGHT",
												   "DC_PACK_SIZE",
												   "ID_INSERT" };
				si.SpNameUpdate = "SP_CZ_SA_CLAIMH_U";
				si.SpParamsUpdate = new string[] { "CD_COMPANY", 
												   "NO_CLAIM", 
												   "NO_SO", 
												   "CD_STATUS", 
												   "NO_IMO", 
												   "CD_PARTNER", 
												   "NO_SALES_EMP",
												   "TP_CLAIM", 
												   "TP_CAUSE", 
												   "TP_ITEM", 
												   "AM_ITEM_RCV", 
												   "AM_ADD_RCV", 
												   "AM_ITEM_PRO", 
												   "AM_ADD_PRO", 
												   "AM_ITEM_CLS", 
												   "AM_ADD_CLS",  
												   "DC_PROGRESS", 
												   "DC_RESULT", 
												   "DC_PREVENTION", 
												   "DC_CLOSING", 
												   "NO_EMP",
												   "DT_INPUT", 
                                                   "DT_EXPECTED_CLOSING_PRO",
												   "DT_EXPECTED_CLOSING",
												   "DT_CLOSING",
                                                   "CD_SUPPLIER_REWARD",
                                                   "DC_SUPPLIER_REWARD",
                                                   "DC_CREDIT_NOTE",
                                                   "CD_CREDIT_EXCH",
                                                   "AM_CREDIT",
												   "TP_REASON",
												   "TP_RETURN",
												   "TP_REQUEST",
												   "DC_RECEIVE",
												   "IMAGE1",
												   "IMAGE2",
												   "IMAGE3",
												   "IMAGE4",
												   "IMAGE5",
												   "IMAGE6",
												   "DC_IMAGE1",
												   "DC_IMAGE2",
												   "DC_IMAGE3",
												   "DC_IMAGE4",
												   "DC_IMAGE5",
												   "DC_IMAGE6",
												   "QT_PACK_WEIGHT",
												   "DC_PACK_SIZE",
												   "ID_UPDATE" };
				si.SpNameDelete = "SP_CZ_SA_CLAIMH_D";
				si.SpParamsDelete = new string[] { "CD_COMPANY",
												   "NO_CLAIM" };

				sc.Add(si);
			}

			if (dtL != null && dtL.Rows.Count > 0)
			{
				SpInfo si = new SpInfo();
				si.DataValue = Util.GetXmlTable(dtL);
				si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si.UserID = Global.MainFrame.LoginInfo.UserID;
                si.SpNameInsert = "SP_CZ_SA_CLAIML_XML";
                si.SpParamsInsert = new string[] { "CD_COMPANY", "XML", "ID_INSERT" };
				sc.Add(si);
			}

			if (sc.List != null && sc.List.Count > 0)
				return DBHelper.Save(sc);
			else
				return true;
		}

		//internal bool Save(DataTable dtMaster, DataTable dtDetail)
		//{
		//    SpInfoCollection sic = new SpInfoCollection();
		//    SpInfo si;

		//    #region Master Data
		//    if (dtMaster != null && dtMaster.Rows.Count > 0)
		//    {
		//        si = new SpInfo();
		//        si.DataValue = this.CreateXmlTable(dtMaster);
		//        si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
		//        si.SpNameInsert = "SP_CZ_SA_CLAIMH_XML";
		//        si.SpParamsInsert = new string[] { "CD_COMPANY", "XML", "ID_USER" };

		//        si.SpParamsValues.Add(ActionState.Insert, "ID_USER", Global.MainFrame.LoginInfo.UserID);
		//        sic.Add(si);
		//    }
		//    #endregion

		//    #region Detail Data
		//    if (dtDetail != null && dtDetail.Rows.Count > 0)
		//    {
		//        si = new SpInfo();
		//        si.DataValue = this.CreateXmlTable(dtDetail);
		//        si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
		//        si.SpNameInsert = "SP_CZ_SA_CLAIML_XML";
		//        si.SpParamsInsert = new string[] { "CD_COMPANY", "XML", "ID_USER" };

		//        si.SpParamsValues.Add(ActionState.Insert, "ID_USER", Global.MainFrame.LoginInfo.UserID);
		//        sic.Add(si);
		//    }
		//    #endregion

		//    return DBHelper.Save(sic);
		//}

		//internal bool Save(DataTable dtOrg)
		//{
		//    StringWriter sw = new StringWriter();
		//    dtOrg.WriteXml(sw, XmlWriteMode.IgnoreSchema);

		//    dtOrg.GetChanges();

		//    return DBHelper.ExecuteNonQuery("SP_CZ_SA_CLAIMH_XML", new object[4]
		//    {
		//        (object) Global.MainFrame.LoginInfo.CompanyCode
		//        , sw.ToString()
		//        , Global.MainFrame.LoginInfo.UserID
		//        , Global.MainFrame.GetStringToday
		//    });
		//}
	}
}

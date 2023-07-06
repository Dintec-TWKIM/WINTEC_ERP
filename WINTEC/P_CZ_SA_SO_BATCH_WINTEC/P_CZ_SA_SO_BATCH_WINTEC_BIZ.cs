using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using System;
using System.Data;

namespace cz
{
	public class P_CZ_SA_SO_BATCH_WINTEC_BIZ
	{
		private DataTable dt프로젝트 = null;
		private string ATP사용여부 = "000";
		private string 수주라인_과세변경유무 = "N";
		private string 로그인언어 = Global.SystemLanguage.MultiLanguageLpoint;

		public P_CZ_SA_SO_BATCH_WINTEC_BIZ()
		{
			BASIC.CacheDataClear(BASIC.CacheEnums.ALL);
			this.ATP사용여부 = BASIC.GetMAEXC("ATP사용여부");
			this.수주라인_과세변경유무 = BASIC.GetMAEXC("수주라인-과세변경유무");
		}

		public bool Save(DataTable dt_SoH, DataTable dt_SoL, string 매출자동여부)
		{
			SpInfoCollection spInfoCollection = new SpInfoCollection();

			if (dt_SoH != null)
			{
				SpInfo spInfo = new SpInfo();
				spInfo.DataValue = dt_SoH;
				spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
				spInfo.SpNameInsert = "SP_CZ_SA_SOH_BATCH_I_WINTEC";

				spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
													   "NO_SO",
													   "CD_BIZAREA",
													   "DT_SO",
													   "CD_PARTNER",
													   "CD_SALEGRP",
													   "NO_EMP",
													   "TP_SO",
													   "CD_EXCH",
													   "RT_EXCH",
													   "TP_PRICE",
													   "NO_PROJECT",
													   "TP_VAT",
													   "RT_VAT",
													   "FG_VAT",
													   "FG_TAXP",
													   "DC_RMK",
													   "FG_BILL",
													   "FG_TRANSPORT",
													   "NO_CONTRACT",
													   "STA_SO",
													   "FG_TRACK",
													   "NO_PO_PARTNER",
													   "ID_INSERT",
													   "RMA_REASON",
													   "DC_RMK1",
													   "TXT_USERDEF1",
													   "NUM_USERDEF1",
													   "NUM_USERDEF2",
													   "NUM_USERDEF3",
													   "NO_NEGO" };

				spInfoCollection.Add(spInfo);
			}

			if (this.Get과세변경유무 == "N" && 매출자동여부 == "Y")
			{
				SpInfo spInfo = new SpInfo();
				spInfo.DataValue = dt_SoH;
				spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
				spInfo.SpNameInsert = "UP_SA_SOH_SUB_I";

				spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
													   "NO_SO",
													   "DT_PROCESS",
													   "DT_RCP_RSV",
													   "FG_AR_EXC",
													   "AM_IV",
													   "AM_IV_EX",
													   "AM_IV_VAT",
													   "NM_PTR",
													   "EX_EMIL",
													   "EX_HP",
													   "ID_INSERT",
													   "DC_RMK" };

				spInfoCollection.Add(spInfo);
			}

			if (dt_SoL != null)
			{
				SpInfo spInfo = new SpInfo();
				spInfo.DataValue = dt_SoL;
				spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
				spInfo.SpNameInsert = "SP_CZ_SA_SOL_BATCH_I_WINTEC";

				spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
													   "NO_SO",
													   "SEQ_SO",
													   "CD_PLANT",
													   "CD_ITEM",
													   "UNIT_SO",
													   "DT_EXPECT",
													   "DT_DUEDATE",
													   "DT_REQGI",
													   "QT_SO",
													   "UM_SO",
													   "AM_SO",
													   "AM_WONAMT",
													   "AM_VAT",
													   "UNIT_IM",
													   "QT_IM",
													   "CD_SL",
													   "TP_ITEM",
													   "STA_SO",
													   "TP_BUSI",
													   "TP_GI",
													   "TP_IV",
													   "GIR",
													   "GI",
													   "IV",
													   "TRADE",
													   "TP_VAT",
													   "RT_VAT",
													   "GI_PARTNER",
													   "ID_INSERT",
													   "NO_PROJECT",
													   "SEQ_PROJECT",
													   "CD_ITEM_PARTNER",
													   "NM_ITEM_PARTNER",
													   "DC1",
													   "DC2",
													   "UMVAT_SO",
													   "AMVAT_SO",
													   "CD_SHOP",
													   "CD_SPITEM",
													   "CD_OPT",
													   "RT_DSCNT",
													   "UM_BASE",
													   "NM_CUST_DLV",
													   "CD_ZIP",
													   "ADDR1",
													   "ADDR2",
													   "NO_TEL_D1",
													   "NO_TEL_D2",
													   "TP_DLV",
													   "DC_REQ",
													   "FG_TRACK",
													   "TP_DLV_DUE",
													   "FG_USE",
													   "CD_CC",
													   "NO_IO_MGMT",
													   "NO_IOLINE_MGMT",
													   "NO_POLINE_PARTNER",
													   "NO_ORDER",
													   "NM_CUST",
													   "NO_TEL1",
													   "NO_TEL2",
													   "TXT_USERDEF1",
													   "NO_PO_PARTNER",
													   "FG_USE2",
													   "NO_RELATION",
													   "SEQ_RELATION",
													   "NUM_USERDEF1",
													   "NUM_USERDEF2",
													   "SOL_TXT_USERDEF1",
													   "SOL_TXT_USERDEF2",
													   "CD_MNGD1",
													   "CD_MNGD2",
													   "CD_MNGD3",
													   "CD_MNGD4",
													   "TXT_USERDEF3",
													   "TXT_USERDEF4",
													   "TXT_USERDEF5",
													   "TXT_USERDEF6",
													   "YN_OPTION",
													   "CD_USERDEF1",
													   "CD_USERDEF2",
													   "CD_USERDEF3" };

				spInfo.SpParamsValues.Add(ActionState.Insert, "NO_IO_MGMT", string.Empty);
				spInfo.SpParamsValues.Add(ActionState.Insert, "NO_IOLINE_MGMT", 0);
				spInfo.SpParamsValues.Add(ActionState.Insert, "FG_USE2", string.Empty);

				spInfoCollection.Add(spInfo);
			}

			foreach (ResultData resultData in (ResultData[])Global.MainFrame.Save(spInfoCollection))
			{
				if (!resultData.Result)
					return false;
			}

			return true;
		}

		public string[] GetTpBusi(object[] obj)
		{
			SpInfo spInfo = new SpInfo();
			spInfo.SpNameSelect = "UP_SA_SO_TPBUSI_SELECT";
			spInfo.SpParamsSelect = obj;
			ResultData resultData = (ResultData)Global.MainFrame.FillDataTable(spInfo);
			return new string[] { resultData.OutParamsSelect[0, 0].ToString(),
								  resultData.OutParamsSelect[0, 1].ToString(),
								  resultData.OutParamsSelect[0, 2].ToString() };
		}

		public DataSet SO_PK_Check(object[] obj)
		{
			return (DataSet)((ResultData)Global.MainFrame.FillDataSet("UP_SA_SO_BATCH_CHECK_SELECT", obj)).DataValue;
		}

		internal DataTable Get_dt프로젝트()
		{
			this.dt프로젝트 = DBHelper.GetDataTable(@"SELECT NO_PROJECT, 
															 NM_PROJECT 
													  FROM DZSN_SA_PROJECTH WITH(NOLOCK) 
													  WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'");
			return this.dt프로젝트;
		}

		public Decimal UmSearch(object[] obj)
		{
			Dass.FlexGrid.FlexGrid flexGrid = new Dass.FlexGrid.FlexGrid();
			SpInfo spInfo = new SpInfo();
			spInfo.SpNameSelect = "UP_SA_SO_SELECT1";
			spInfo.SpParamsSelect = obj;
			ResultData resultData = (ResultData)Global.MainFrame.FillDataTable(spInfo);

			return flexGrid.CDecimal(resultData.OutParamsSelect[0, 0].ToString());
		}

		public string GetSaleOrgUmCheck(object[] obj)
		{
			string str = string.Empty;

			if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
				str = @"SELECT ISNULL(SO.SO_PRICE, 'N') AS SO_PRICE    
						FROM MA_SALEGRP SG WITH(NOLOCK)   
						LEFT JOIN MA_SALEORG SO WITH(NOLOCK) ON SG.CD_COMPANY = SO.CD_COMPANY AND SG.CD_SALEORG = SO.CD_SALEORG   
						WHERE SG.CD_COMPANY = '" + obj[0].ToString() + "'     " +
					   "AND SG.CD_SALEGRP = '" + obj[1].ToString() + "'";
			else if (Global.MainFrame.DatabaseType == EnumDbType.ORACLE)
				str = @"SELECT NVL(SO.SO_PRICE, 'N') AS SO_PRICE    
						FROM MA_SALEGRP SG WITH(NOLOCK)    
						LEFT JOIN MA_SALEORG SO WITH(NOLOCK) ON SG.CD_COMPANY = SO.CD_COMPANY AND SG.CD_SALEORG = SO.CD_SALEORG   
						WHERE SG.CD_COMPANY = '" + obj[0].ToString() + "'     " +
					   "AND SG.CD_SALEGRP = '" + obj[1].ToString() + "'";

			return D.GetString(Global.MainFrame.ExecuteScalar(str));
		}

		public DataTable search_EnvMng()
		{
			return DBHelper.GetDataTable("UP_SA_ENV_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode });
		}

		internal DataTable SearchCpItem(string cdPartner, string multiCpItem)
		{
			return DBHelper.GetDataTable("UP_SA_CPITEM_S", new object[] { MA.Login.회사코드,
																		  cdPartner,
																		  multiCpItem,
																		  this.로그인언어 });
		}

		internal string Get과세변경유무
		{
			get
			{
				return this.수주라인_과세변경유무;
			}
		}

		internal DataTable 품목정보(string multi_item)
		{
			string[] pipes = D.StringConvert.GetPipes(multi_item, 78);
			string empty = string.Empty;
			DataTable dataTable1 = null;

			if (Global.MainFrame.DatabaseType == EnumDbType.MSSQL)
			{
				foreach (string str in pipes)
				{
					DataTable dataTable2 = DBHelper.GetDataTable(@"SELECT P.CD_PLANT, 
																		  P.CD_ITEM, 
																		  P.NM_ITEM, 
																		  P.STND_ITEM, 
																		  P.UNIT_SO, 
																		  P.UNIT_IM, 
																		  ISNULL(P.UNIT_SO_FACT,0) AS UNIT_SO_FACT,  
																		  P.LT_GI, 
																		  ISNULL(P.CD_GISL, '') AS CD_GISL, 
																		  P.TP_ITEM, 
																		  RTRIM(P.FG_TAX_SA) AS FG_TAX_SA, 
																		  VT.CD_FLAG1 AS RT_TAX_SA,  
																		  P.CD_USERDEF1, 
																		  P.CD_USERDEF2, 
																		  P.CD_USERDEF3, 
																		  P.CD_USERDEF4, 
																		  P.CD_USERDEF5, 
																		  P.CD_USERDEF6, 
																		  P.CD_USERDEF7,  
																		  P.CD_USERDEF8, 
																		  P.CD_USERDEF9    
																   FROM DZSN_MA_PITEM P WITH(NOLOCK) 
																   LEFT JOIN DZSN_MA_CODEDTL VT WITH(NOLOCK) ON P.CD_COMPANY = VT.CD_COMPANY AND VT.CD_FIELD = 'MA_B000040' AND P.FG_TAX_SA = VT.CD_SYSDEF   
																   WHERE P.CD_COMPANY = '" + MA.Login.회사코드 + "'  " +
																  "AND (P.CD_ITEM IN (SELECT CD_STR FROM GETTABLEFROMSPLIT( '" + str + "'))) ");
					if (dataTable1 == null)
						dataTable1 = dataTable2;
					else
						dataTable1.Merge(dataTable2);
				}
			}
			else if (Global.MainFrame.DatabaseType == EnumDbType.ORACLE)
			{
				foreach (string str in pipes)
				{
					DataTable dataTable2 = DBHelper.GetDataTable(@"SELECT P.CD_PLANT, 
																		  P.CD_ITEM, 
																		  P.NM_ITEM, 
																		  P.STND_ITEM, 
																		  P.UNIT_SO, 
																		  P.UNIT_IM, 
																		  NVL(P.UNIT_SO_FACT,0) AS UNIT_SO_FACT,  
																		  P.LT_GI, 
																		  NVL(P.CD_GISL, '') AS CD_GISL, 
																		  P.TP_ITEM, RTRIM(P.FG_TAX_SA) AS FG_TAX_SA, 
																		  VT.CD_FLAG1 AS RT_TAX_SA,  
																		  P.CD_USERDEF1, 
																		  P.CD_USERDEF2, 
																		  P.CD_USERDEF3, 
																		  P.CD_USERDEF4, 
																		  P.CD_USERDEF5, 
																		  P.CD_USERDEF6, 
																		  P.CD_USERDEF7,  
																		  P.CD_USERDEF8, 
																		  P.CD_USERDEF9    
																   FROM DZSN_MA_PITEM P WITH(NOLOCK) 
																   LEFT JOIN DZSN_MA_CODEDTL VT WITH(NOLOCK) ON P.CD_COMPANY = VT.CD_COMPANY AND VT.CD_FIELD = 'MA_B000040' AND P.FG_TAX_SA = VT.CD_SYSDEF   
																   WHERE P.CD_COMPANY = '" + MA.Login.회사코드 + "'   " +
																  "AND ((P.CD_ITEM IN (SELECT CD_STR FROM TABLE(CAST(GETTABLEFROMSPLIT('" + str + "') AS TB_GETTABLEFROMSPLITTAB)))))");
					if (dataTable1 == null)
						dataTable1 = dataTable2;
					else
						dataTable1.Merge(dataTable2);
				}
			}
			return dataTable1;
		}
	}
}

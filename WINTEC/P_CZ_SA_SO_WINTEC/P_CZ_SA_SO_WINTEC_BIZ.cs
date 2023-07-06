using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA.Common;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using static Duzon.ERPU.SA.Common.수주관리;

namespace cz
{
	internal class P_CZ_SA_SO_WINTEC_BIZ
	{
		private string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
		private string 로그인언어 = Global.SystemLanguage.MultiLanguageLpoint;
		private bool is수주반품사용여부 = false;
		private string 수주등록_특수단가적용 = "000";
		private string 수주등록_할인율적용 = "000";
		private string 수주등록_예상이익산출 = "000";
		private string 수주등록_WH적용 = "000";
		private string 수주라인_과세변경유무 = "N";
		private string ATP사용여부 = "000";
		private string _excCredit = "000";
		private string 수주등록_프로젝트적용 = "000";
		private string 수주등록_사양등록 = "000";
		private 수주관리.Config 수주Config = new 수주관리.Config();

		public P_CZ_SA_SO_WINTEC_BIZ()
		{
			BASIC.CacheDataClear(BASIC.CacheEnums.ALL);
			this.is수주반품사용여부 = BASIC.GetMAEXC("수주반품사용여부") == "Y";
			this.수주등록_특수단가적용 = BASIC.GetMAEXC("수주등록-특수단가적용");
			this.수주등록_할인율적용 = BASIC.GetMAEXC("수주등록-할인율적용");
			this.수주등록_예상이익산출 = BASIC.GetMAEXC("수주등록-예상이익산출");
			this.수주등록_WH적용 = BASIC.GetMAEXC("W/H 정보사용");
			this.수주라인_과세변경유무 = BASIC.GetMAEXC("수주라인-과세변경유무");
			this.ATP사용여부 = BASIC.GetMAEXC("ATP사용여부");
			this._excCredit = BASIC.GetMAEXC("여신한도");
			this.수주등록_프로젝트적용 = BASIC.GetMAEXC("수주등록-프로젝트 적용");
			this.수주등록_사양등록 = BASIC.GetMAEXC("수주등록-사양등록 사용여부");
		}

		public DataSet Search(string NO_SO)
		{
			DataSet dataSet = DBHelper.GetDataSet("SP_CZ_SA_SO_S_WINTEC", new object[] { this.CD_COMPANY, NO_SO, this.로그인언어 });
			T.SetDefaultValue(dataSet);
			DataTable table = dataSet.Tables[0];
			table.Columns["DT_SO"].DefaultValue = Global.MainFrame.GetStringToday;
			table.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
			table.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
			table.Columns["CD_EXCH"].DefaultValue = "000";
			table.Columns["TP_PRICE"].DefaultValue = "001";
			table.Columns["TP_VAT"].DefaultValue = "11";
			table.Columns["FG_VAT"].DefaultValue = "N";
			table.Columns["FG_TAXP"].DefaultValue = "001";
			table.Columns["NO_PROJECT"].DefaultValue = "";
			table.Columns["DT_PROCESS"].DefaultValue = Global.MainFrame.GetStringToday;
			DataColumn column = table.Columns["DT_RCP_RSV"];
			DateTime dateTime = DateTime.Now;
			dateTime = dateTime.AddDays(10.0);
			string str = dateTime.ToShortDateString().Replace("-", "");
			column.DefaultValue = str;

			dataSet.Tables[1].Columns.Add("UM_SO_ORI", typeof(decimal));

			return dataSet;
		}

		public string[] 거래구분(string 수주형태, string 부가세코드)
		{
			object[] objArray;
			DBHelper.ExecuteNonQuery("UP_SA_SO_TPBUSI_SELECT", new object[] { this.CD_COMPANY, 수주형태, 부가세코드 }, out objArray);
			return new string[] { D.GetString(objArray[0]), D.GetString(objArray[1]), D.GetString(objArray[2]) };
		}

		public bool CheckCredit(string 거래처, decimal 금액, string 의뢰여부, string 출하여부, ref string 수주상태)
		{
			string str1 = "001";
			object[] objArray = new object[3];

			if (의뢰여부 == "Y")
				str1 = 출하여부 != "Y" ? "100" : "101";

			DBHelper.GetDataTable("UP_SA_CHECKCREDIT_SELECT", new object[] { this.CD_COMPANY, 거래처, 금액, str1 }, out objArray);

			if (objArray[0] != DBNull.Value)
			{
				string str2 = D.GetDecimal(objArray[0]).ToString("###,###,###,###,##0.####");
				string str3 = D.GetDecimal(objArray[1]).ToString("###,###,###,###,##0.####");
				string empty = string.Empty;

				if (D.GetString(objArray[2]).ToString() == "002")
				{
					if (Global.MainFrame.ShowMessage("여신금액을 초과하였습니다. 저장하시겠습니까 ?\n(여신총액 : " + str2 + ", 잔액 : " + str3 + ")", "QY2") != DialogResult.Yes)
						return false;

					return true;
				}

				if (D.GetString(objArray[2]).ToString() == "003")
				{
					Global.MainFrame.ShowMessage("여신금액을 초과하여 저장할 수 없습니다. \n(여신총액 : " + str2 + ", 잔액 : " + str3 + ")");
					return false;
				}
			}

			return true;
		}

		internal bool CheckCreditExec(string cdPartner, string cdExch, decimal amEx)
		{
			object[] objArray = new object[3];
			DBHelper.GetDataTable("UP_SA_CHECKCREDIT_EXCH_SELECT", new object[] { this.CD_COMPANY, cdPartner, cdExch, amEx, "001" }, out objArray);

			if (objArray[0] != DBNull.Value)
			{
				string str1 = D.GetDecimal(objArray[0]).ToString("###,###,###,###,##0.####");
				string str2 = D.GetDecimal(objArray[1]).ToString("###,###,###,###,##0.####");

				if (D.GetString(objArray[2]) == "002")
					return Global.MainFrame.ShowMessage("여신금액을 초과하였습니다. 저장하시겠습니까 ?\n(여신총액 : " + str1 + ", 잔액 : " + str2 + ")", "QY2") == DialogResult.Yes;

				if (D.GetString(objArray[2]) == "003")
				{
					Global.MainFrame.ShowMessage("여신금액을 초과하여 저장할 수 없습니다. \n(여신총액 : " + str1 + " 잔액 : " + str2 + ")");
					return false;
				}
			}

			return true;
		}

		public bool Delete(string NO_SO)
		{
			return DBHelper.ExecuteNonQuery("SP_CZ_SA_SOH_D_WINTEC", new object[] { this.CD_COMPANY, NO_SO });
		}

		public bool Save(DataTable dtH, DataTable dtL, DataTable dtLL, DataTable dtLot, string[] strArr, DataTable dtSerial, string SERIAL_FG_PS)
		{
			SpInfoCollection spInfoCollection = new SpInfoCollection();
			string 수주번호 = strArr[0];
			string 수주상태 = strArr[1];
			string 거래구분 = strArr[2];
			string 출하형태 = strArr[3];
			string 매출형태 = strArr[4];
			string 의뢰여부 = strArr[5];
			string 출하여부 = strArr[6];
			string 매출여부 = strArr[7];
			string 수출여부 = strArr[8];
			string 구분 = strArr[9];
			string 반품여부 = strArr[10];
			string 매출자동여부 = strArr[11];
			string 자동승인여부 = strArr[12];

			if (dtH != null)
			{
				string empty = string.Empty;

				if (dtH.Rows.Count > 0 && (구분 == "복사" || dtH.Rows[0].RowState == DataRowState.Added))
					empty = D.GetString(CodeSearch.GetCodeInfo(MasterSearch.MA_PLANT, new object[] { this.CD_COMPANY, D.GetString(dtL.Rows[0]["CD_PLANT"]) })["CD_BIZAREA"]);

				SpInfo spInfo = new SpInfo();

				if (구분 == "복사")
					spInfo.DataState = DataValueState.Added;

				spInfo.DataValue = dtH;
				spInfo.CompanyID = this.CD_COMPANY;
				spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
				spInfo.SpNameInsert = "SP_CZ_SA_SOH_I_WINTEC";
				spInfo.SpNameUpdate = "SP_CZ_SA_SOH_U_WINTEC";
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
													   "NO_EST",
													   "NO_EST_HST",
													   "CD_EXPORT",
													   "CD_PRODUCT",
													   "COND_TRANS",
													   "COND_PAY",
													   "COND_DAYS",
													   "TP_PACKING",
													   "TP_TRANS",
													   "TP_TRANSPORT",
													   "NM_INSPECT",
													   "PORT_LOADING",
													   "PORT_ARRIVER",
													   "CD_ORIGIN",
													   "DESTINATION",
													   "DT_EXPIRY",
													   "CD_NOTIFY",
													   "CD_CONSIGNEE",
													   "CD_TRANSPORT",
													   "DC_RMK_TEXT",
													   "ID_INSERT",
													   "RMA_REASON",
													   "DC_RMK1",
													   "COND_PRICE",
													   "DT_USERDEF1",
													   "DT_USERDEF2",
													   "TXT_USERDEF1",
													   "TXT_USERDEF2",
													   "TXT_USERDEF3",
													   "CD_USERDEF1",
													   "CD_USERDEF2",
													   "CD_USERDEF3",
													   "NUM_USERDEF1",
													   "NUM_USERDEF2",
													   "NUM_USERDEF3",
													   "NUM_USERDEF4",
													   "NUM_USERDEF5",
													   "CD_BANK_SO",
													   "DC_RMK_TEXT2",
													   "NO_INV",
													   "TXT_USERDEF4",
													   "CD_CHANCE",
													   "NO_NEGO" };
				spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
													   "NO_SO",
													   "DT_SO",
													   "CD_PARTNER",
													   "CD_SALEGRP",
													   "NO_EMP",
													   "CD_EXCH",
													   "RT_EXCH",
													   "TP_PRICE",
													   "NO_PROJECT",
													   "TP_VAT",
													   "FG_VAT",
													   "FG_TAXP",
													   "DC_RMK",
													   "FG_BILL",
													   "FG_TRANSPORT",
													   "NO_CONTRACT",
													   "NO_PO_PARTNER",
													   "NO_EST",
													   "NO_EST_HST",
													   "CD_EXPORT",
													   "CD_PRODUCT",
													   "COND_TRANS",
													   "COND_PAY",
													   "COND_DAYS",
													   "TP_PACKING",
													   "TP_TRANS",
													   "TP_TRANSPORT",
													   "NM_INSPECT",
													   "PORT_LOADING",
													   "PORT_ARRIVER",
													   "CD_ORIGIN",
													   "DESTINATION",
													   "DT_EXPIRY",
													   "CD_NOTIFY",
													   "CD_CONSIGNEE",
													   "CD_TRANSPORT",
													   "DC_RMK_TEXT",
													   "ID_UPDATE",
													   "RMA_REASON",
													   "DC_RMK1",
													   "COND_PRICE",
													   "DT_USERDEF1",
													   "DT_USERDEF2",
													   "TXT_USERDEF1",
													   "TXT_USERDEF2",
													   "TXT_USERDEF3",
													   "CD_USERDEF1",
													   "CD_USERDEF2",
													   "CD_USERDEF3",
													   "NUM_USERDEF1",
													   "NUM_USERDEF2",
													   "NUM_USERDEF3",
													   "NUM_USERDEF4",
													   "NUM_USERDEF5",
													   "CD_BANK_SO",
													   "DC_RMK_TEXT2",
													   "NO_INV",
													   "TXT_USERDEF4",
													   "CD_CHANCE",
													   "TP_SO",
													   "RT_VAT",
													   "STA_SO",
													   "NO_NEGO" };

				spInfo.SpParamsValues.Add(ActionState.Insert, "STA_SO", 수주상태);
				spInfo.SpParamsValues.Add(ActionState.Insert, "CD_BIZAREA", empty);

				spInfo.SpParamsValues.Add(ActionState.Update, "STA_SO", 수주상태);
				spInfoCollection.Add(spInfo);
			}

			if (this.Get과세변경유무 == "N" && 매출자동여부 == "Y")
			{
				SpInfo spInfo = new SpInfo();

				if (구분 == "복사")
					spInfo.DataState = DataValueState.Added;

				spInfo.DataValue = dtH;
				spInfo.CompanyID = this.CD_COMPANY;
				spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
				spInfo.SpNameInsert = "UP_SA_SOH_SUB_I";
				spInfo.SpNameUpdate = "UP_SA_SOH_SUB_U";
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
													   "DC_RMK_IVH" };
				spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
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
													   "ID_UPDATE",
													   "DC_RMK_IVH" };
				spInfoCollection.Add(spInfo);
			}

			if (dtL != null)
			{
				SpInfo spInfo = new SpInfo();

				spInfo.DataValue = dtL;
				spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
				spInfo.SpNameInsert = "SP_CZ_SA_SOL_I_WINTEC";
				spInfo.SpNameUpdate = "SP_CZ_SA_SOL_U_WINTEC";
				spInfo.SpNameDelete = "SP_CZ_SA_SOL_D_WINTEC";

				string str14 = !(구분 == "복사") ? "STA_SO" : "STA_SO1";
				spInfo.SpParamsInsert = new string[] { "SERVERKEY",
													   "CD_COMPANY",
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
													   str14,
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
													   "UM_OPT",
													   "NO_PO_PARTNER",
													   "CD_WH",
													   "NO_SO_ORIGINAL",
													   "SEQ_SO_ORIGINAL",
													   "NUM_USERDEF1",
													   "NUM_USERDEF2",
													   "NUM_USERDEF3",
													   "NUM_USERDEF4",
													   "NUM_USERDEF5",
													   "NUM_USERDEF6",
													   "CD_MNGD1",
													   "CD_MNGD2",
													   "CD_MNGD3",
													   "CD_MNGD4",
													   "TXT_USERDEF1",
													   "TXT_USERDEF2",
													   "YN_OPTION",
													   "NO_ORDER",
													   "NM_CUST",
													   "NO_TEL1",
													   "NO_TEL2",
													   "DLV_TXT_USERDEF1",
													   "CD_ITEM_REF",
													   "YN_PICKING",
													   "DLV_CD_USERDEF1",
													   "FG_USE2",
													   "NO_LINK",
													   "NO_LINE_LINK",
													   "NO_RELATION",
													   "SEQ_RELATION",
													   "CD_USERDEF1",
													   "CD_USERDEF2",
													   "UM_EX",
													   "CD_USERDEF4",
													   "TXT_USERDEF3",
													   "TXT_USERDEF4",
													   "TXT_USERDEF5",
													   "TXT_USERDEF6",
													   "TXT_USERDEF7",
													   "TXT_USERDEF8",
													   "TXT_USERDEF9",
													   "TXT_USERDEF10",
													   "TXT_USERDEF11",
													   "UM_EX_PO",
													   "AM_EX_PO",
													   "NO_EST",
													   "NO_EST_HST",
													   "SEQ_EST",
													   "CD_USERDEF3" };
				spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
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
													   "ID_UPDATE",
													   "GI_PARTNER",
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
													   "TP_VAT",
													   "RT_VAT",
													   "CD_CC",
													   "NO_POLINE_PARTNER",
													   "UM_OPT",
													   "NO_PO_PARTNER",
													   "CD_WH",
													   "NO_SO_ORIGINAL",
													   "SEQ_SO_ORIGINAL",
													   "NUM_USERDEF1",
													   "NUM_USERDEF2",
													   "NUM_USERDEF3",
													   "NUM_USERDEF4",
													   "NUM_USERDEF5",
													   "NUM_USERDEF6",
													   "CD_MNGD1",
													   "CD_MNGD2",
													   "CD_MNGD3",
													   "CD_MNGD4",
													   "TXT_USERDEF1",
													   "TXT_USERDEF2",
													   "YN_OPTION",
													   "NO_ORDER",
													   "NM_CUST",
													   "NO_TEL1",
													   "NO_TEL2",
													   "DLV_TXT_USERDEF1",
													   "CD_ITEM_REF",
													   "YN_PICKING",
													   "DLV_CD_USERDEF1",
													   "FG_USE2",
													   "CD_USERDEF1",
													   "CD_USERDEF2",
													   "UM_EX",
													   "CD_USERDEF4",
													   "TXT_USERDEF3",
													   "TXT_USERDEF4",
													   "TXT_USERDEF5",
													   "TXT_USERDEF6",
													   "TXT_USERDEF7",
													   "TXT_USERDEF8",
													   "TXT_USERDEF9",
													   "TXT_USERDEF10",
													   "TXT_USERDEF11",
													   "UM_EX_PO",
													   "AM_EX_PO",
													   "NO_EST",
													   "NO_EST_HST",
													   "SEQ_EST",
													   "TP_BUSI",
													   "TP_GI",
													   "GIR",
													   "GI",
													   "IV",
													   "TRADE",
													   "STA_SO",
													   "CD_USERDEF3" };
				spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
													   "NO_SO",
													   "SEQ_SO" };

				spInfo.SpParamsValues.Add(ActionState.Insert, "NO_SO", 수주번호);
				spInfo.SpParamsValues.Add(ActionState.Insert, "SERVERKEY", Global.MainFrame.ServerKeyCommon.ToUpper());

				if (구분 != "복사")
				{
					spInfo.SpParamsValues.Add(ActionState.Insert, "STA_SO", 수주상태);
					spInfo.SpParamsValues.Add(ActionState.Update, "STA_SO", 수주상태);
				}

				spInfo.SpParamsValues.Add(ActionState.Insert, "TP_BUSI", 거래구분);
				spInfo.SpParamsValues.Add(ActionState.Insert, "TP_GI", 출하형태);
				spInfo.SpParamsValues.Add(ActionState.Insert, "GIR", 의뢰여부);
				spInfo.SpParamsValues.Add(ActionState.Insert, "GI", 출하여부);
				spInfo.SpParamsValues.Add(ActionState.Insert, "IV", 매출여부);
				spInfo.SpParamsValues.Add(ActionState.Insert, "TRADE", 수출여부);

				spInfo.SpParamsValues.Add(ActionState.Update, "NO_SO", 수주번호);
				spInfo.SpParamsValues.Add(ActionState.Update, "TP_BUSI", 거래구분);
				spInfo.SpParamsValues.Add(ActionState.Update, "TP_GI", 출하형태);
				spInfo.SpParamsValues.Add(ActionState.Update, "GIR", 의뢰여부);
				spInfo.SpParamsValues.Add(ActionState.Update, "GI", 출하여부);
				spInfo.SpParamsValues.Add(ActionState.Update, "IV", 매출여부);
				spInfo.SpParamsValues.Add(ActionState.Update, "TRADE", 수출여부);

				spInfo.SpParamsValues.Add(ActionState.Delete, "NO_SO", 수주번호);
				spInfoCollection.Add(spInfo);
			}

			if (dtLot != null)
			{
				SpInfo spInfo = new SpInfo();
				spInfo.DataValue = dtLot;
				spInfo.DataState = DataValueState.Added;
				spInfo.SpNameInsert = "UP_MM_QTIOLOT_INSERT";
				spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				if (반품여부 == "N")
				{
					spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
														   "출고번호",
														   "출고항번",
														   "NO_LOT",
														   "CD_ITEM",
														   "출고일",
														   "FG_PS",
														   "수불구분",
														   "수불형태",
														   "창고코드",
														   "QT_GOOD_MNG",
														   "NO_IO",
														   "NO_IOLINE",
														   "NO_IOLINE2",
														   "YN_RETURN",
														   "A",
														   "B",
														   "C",
														   "D",
														   "E",
														   "F",
														   "NO_SO" };
				}
				else
				{
					spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
														   "NO_IO",
														   "NO_IOLINE",
														   "NO_LOT",
														   "CD_ITEM",
														   "DT_IO",
														   "FG_PS",
														   "FG_IO",
														   "CD_QTIOTP",
														   "CD_SL",
														   "QT_IO",
														   "NO_IO",
														   "NO_IOLINE",
														   "NO_IOLINE2",
														   "YN_RETURN",
														   "A",
														   "B",
														   "C",
														   "D",
														   "E",
														   "F",
														   "NO_SO" };
					spInfo.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", "Y");
				}

				spInfo.SpParamsValues.Add(ActionState.Insert, "A", string.Empty);
				spInfo.SpParamsValues.Add(ActionState.Insert, "B", string.Empty);
				spInfo.SpParamsValues.Add(ActionState.Insert, "C", 0);
				spInfo.SpParamsValues.Add(ActionState.Insert, "D", 0);
				spInfo.SpParamsValues.Add(ActionState.Insert, "E", string.Empty);
				spInfo.SpParamsValues.Add(ActionState.Insert, "F", string.Empty);
				spInfo.SpParamsValues.Add(ActionState.Insert, "NO_SO", 수주번호);
				spInfoCollection.Add(spInfo);
			}

			if (this.수주Config.결제조건도움창사용() && 
				dtH != null && 
				(구분 == "복사" || dtH.Rows[0].RowState == DataRowState.Added))
			{
				string str14 = D.GetString(dtL.Rows[0]["NO_PROJECT"]);
				decimal num = D.GetDecimal(dtL.Rows[0]["SEQ_PROJECT"]);

				if (str14 != string.Empty && num > 0)
				{
					DataTable dataTable = DBHelper.GetDataTable("UP_SA_SO_PAYCOND_S", new object[] { this.CD_COMPANY, str14, "PRE" });
					SpInfo spInfo = new SpInfo();
					spInfo.DataState = DataValueState.Added;
					spInfo.DataValue = dataTable;
					spInfo.CompanyID = this.CD_COMPANY;
					spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
					spInfo.SpNameInsert = "UP_SA_SO_PAYCOND_I";
					spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
														   "FG_TRACK_SO",
														   "NO_SO_NEW",
														   "NO_LINE_PAYCOND",
														   "CD_PAYCOND",
														   "MONTH_PAYCOND",
														   "DAY_PAYCOND",
														   "RT_PAYCOND",
														   "DC_RMK",
														   "ID_INSERT" };
					spInfo.SpParamsValues.Add(ActionState.Insert, "FG_TRACK_SO", "SO");
					spInfo.SpParamsValues.Add(ActionState.Insert, "NO_SO_NEW", D.GetString(dtH.Rows[0]["NO_SO"]));
					spInfoCollection.Add(spInfo);
				}
			}

			if (this.Get과세변경유무 == "N" && 매출자동여부 == "Y")
			{
				SpInfo spInfo = new SpInfo();
				spInfo.DataValue = dtH;
				spInfo.CompanyID = this.CD_COMPANY;
				spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
				spInfo.SpNameInsert = "UP_SA_SO_MODIFY";
				spInfo.SpParamsInsert = new string[] { "NO_SO", "CD_COMPANY" };
				spInfoCollection.Add(spInfo);
			}

			if (dtSerial != null)
			{
				dtSerial.RemotingFormat = SerializationFormat.Binary;
				SpInfo spInfo = new SpInfo();
				spInfo.DataValue = dtSerial;
				spInfo.SpNameInsert = "UP_MM_QTIODS_INSERT";
				spInfo.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
				spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				if (반품여부 == "N")
					spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
														   "NO_SERIAL",
														   "NO_IO",
														   "NO_IOLINE",
														   "CD_ITEM",
														   "CD_QTIOTP",
														   "FG_IO",
														   "CD_MNG1",
														   "CD_MNG2",
														   "CD_MNG3",
														   "CD_MNG4",
														   "CD_MNG5",
														   "CD_MNG6",
														   "CD_MNG7",
														   "CD_MNG8",
														   "CD_MNG9",
														   "CD_MNG10",
														   "CD_MNG11",
														   "CD_MNG12",
														   "CD_MNG13",
														   "CD_MNG14",
														   "CD_MNG15",
														   "CD_MNG16",
														   "CD_MNG17",
														   "CD_MNG18",
														   "CD_MNG19",
														   "CD_MNG20",
														   "CD_PLANT",
														   "ID_INSERT",
														   "NO_REV",
														   "NO_REVLINE",
														   "NO_SO",
														   "FG_PS" };
				else
					spInfo.SpParamsInsert = new string[] { "CD_COMPANY",
														   "NO_SERIAL",
														   "NO_IO",
														   "NO_IOLINE",
														   "CD_ITEM",
														   "CD_QTIOTP",
														   "FG_IO",
														   "CD_MNG1",
														   "CD_MNG2",
														   "CD_MNG3",
														   "CD_MNG4",
														   "CD_MNG5",
														   "CD_MNG6",
														   "CD_MNG7",
														   "CD_MNG8",
														   "CD_MNG9",
														   "CD_MNG10",
														   "CD_MNG11",
														   "CD_MNG12",
														   "CD_MNG13",
														   "CD_MNG14",
														   "CD_MNG15",
														   "CD_MNG16",
														   "CD_MNG17",
														   "CD_MNG18",
														   "CD_MNG19",
														   "CD_MNG20",
														   "CD_PLANT",
														   "ID_INSERT",
														   "NO_REV",
														   "NO_REVLINE",
														   "NO_SO",
														   "FG_PS" };

				spInfo.SpParamsValues.Add(ActionState.Insert, "NO_SO", 수주번호);
				spInfo.SpParamsValues.Add(ActionState.Insert, "FG_PS", SERIAL_FG_PS);
				spInfoCollection.Add(spInfo);
			}

			return DBHelper.Save(spInfoCollection);
		}

		internal DataTable 공장품목(string 멀티품목코드, string cdPlant)
		{
			ArrayListExt arrayListExt = this.arr엑셀(멀티품목코드);
			DataTable dataTable1 = null;

			for (int index = 0; index < arrayListExt.Count; ++index)
			{
				DataTable dataTable2 = DBHelper.GetDataTable("UP_SA_SO_EXCEL_SELECT", new object[] { this.CD_COMPANY, D.GetString(arrayListExt[index]), cdPlant, this.로그인언어 });

				if (dataTable1 == null)
				{
					dataTable1 = dataTable2;
				}
				else
				{
					foreach (DataRow row in dataTable2.Rows)
						dataTable1.ImportRow(row);
				}
			}

			return dataTable1;
		}

		internal DataTable 엑셀(DataTable 엑셀)
		{
			if (!엑셀.Columns.Contains("UM_SO"))
				엑셀.Columns.Add("UM_SO", typeof(string));

			if (!엑셀.Columns.Contains("DC1"))
				엑셀.Columns.Add("DC1", typeof(string));

			if (!엑셀.Columns.Contains("DC2"))
				엑셀.Columns.Add("DC2", typeof(string));

			if (!엑셀.Columns.Contains("GI_PARTNER"))
				엑셀.Columns.Add("GI_PARTNER", typeof(string));

			if (!엑셀.Columns.Contains("CD_SL"))
				엑셀.Columns.Add("CD_SL", typeof(string));

			if (!엑셀.Columns.Contains("FG_USE"))
				엑셀.Columns.Add("FG_USE", typeof(string));

			if (!엑셀.Columns.Contains("AM_SO"))
				엑셀.Columns.Add("AM_SO", typeof(string));

			if (!엑셀.Columns.Contains("TXT_USERDEF1"))
				엑셀.Columns.Add("TXT_USERDEF1", typeof(string));

			if (!엑셀.Columns.Contains("TXT_USERDEF2"))
				엑셀.Columns.Add("TXT_USERDEF2", typeof(string));

			if (!엑셀.Columns.Contains("NO_PO_PARTNER"))
				엑셀.Columns.Add("NO_PO_PARTNER", typeof(string));

			if (!엑셀.Columns.Contains("NO_POLINE_PARTNER"))
				엑셀.Columns.Add("NO_POLINE_PARTNER", typeof(string));

			return 엑셀;
		}

		private ArrayListExt arr엑셀(string 멀티품목코드)
		{
			int num1 = 50;
			int num2 = 1;
			string str = string.Empty;
			ArrayListExt arrayListExt = new ArrayListExt();
			string[] strArray = 멀티품목코드.Split('|');

			for (int index = 0; index < strArray.Length - 1; ++index)
			{
				str = str + strArray[index].ToString() + "|";

				if (num2 == num1)
				{
					((ArrayList)arrayListExt).Add(str);
					str = string.Empty;
					num2 = 0;
				}

				++num2;
			}

			if (str != string.Empty)
				arrayListExt.Add(str);

			return arrayListExt;
		}

		internal DataTable Get프로젝트()
		{
			return DBHelper.GetDataTable(@"SELECT H.NO_PROJECT, 
												  H.NM_PROJECT	
										   FROM DZSN_SA_PROJECTH H WITH(NOLOCK)     
										   JOIN (SELECT CD_COMPANY, 
													    NO_PROJECT, 
														MAX(NO_SEQ) AS NO_SEQ     
												 FROM SA_PROJECTH WITH(NOLOCK)    
												 WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'   " +
			 "									 GROUP BY NO_PROJECT, CD_COMPANY) A " +
	"									   ON A.CD_COMPANY = H.CD_COMPANY AND A.NO_PROJECT = H.NO_PROJECT AND A.NO_SEQ = H.NO_SEQ");
		}

		public DataTable GetPartnerInfoSearch(object[] obj)
		{
			string str = @"SELECT CD_PARTNER,                      
								  NO_POST2,                        
								  DC_ADS2_H,                       
								  DC_ADS2_D,                       
								  NO_TEL2,                         
								  CD_EMP_PARTNER,                  
								  NO_HPEMP_PARTNER,                
								  CD_AREA,                         
								  FG_CORP                     
						   FROM DZSN_MA_PARTNER WITH(NOLOCK)                
						   WHERE CD_COMPANY = '" + obj[0].ToString() + "'       " +
						  "AND CD_PARTNER = '" + obj[1].ToString() + "'";

			return Global.MainFrame.FillDataTable(str);
		}

		internal DataTable 할인율(string 공장, string 거래처, DataRow[] dr품목)
		{
			DataTable dataTable = new 품목관리.조회().거래처그룹품목군할인율(공장, 거래처, dr품목);
			dataTable.PrimaryKey = new DataColumn[1] { dataTable.Columns["CD_ITEM"] };

			return dataTable;
		}

		internal DataTable 예상이익(string 공장, string 수주일자, DataRow[] dr품목)
		{
			DataTable dataTable = new 품목관리.조회().예상이익(공장, 수주일자, dr품목);
			dataTable.PrimaryKey = new DataColumn[1] { dataTable.Columns["CD_ITEM"] };

			return dataTable;
		}

		internal void 예상이익(FlexGrid flex, int idx)
		{
			decimal num1 = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(flex[idx, "QT_SO"]));
			decimal num2 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(flex[idx, "UM_INV"]));
			decimal num3 = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(flex[idx, "AM_WONAMT"]));
			수주관리.Calc calc = new 수주관리.Calc();
			flex[idx, "AM_PROFIT"] = calc.예상이익계산(num1, num2, num3);
		}

		internal bool 수주반품사용여부
		{
			get
			{
				return this.is수주반품사용여부;
			}
		}

		internal 특수단가적용 Get특수단가적용
		{
			get
			{
				if (this.수주등록_특수단가적용 == "001")
					return 특수단가적용.중량단가;

				if (this.수주등록_특수단가적용 == "002")
					return 특수단가적용.조선호텔베이커리단가;

				return this.수주등록_특수단가적용 == "003" ? 특수단가적용.거래처별고정단가 : 특수단가적용.NONE;
			}
		}

		internal 수주관리.할인율적용 Get할인율적용
		{
			get
			{
				return new 수주관리.Config().할인율();
			}
		}

		internal 예상이익산출 Get예상이익산출적용
		{
			get
			{
				return this.수주등록_예상이익산출 == "001" ? 예상이익산출.재고단가를원가로산출 : 예상이익산출.NONE;
			}
		}

		internal string WH적용
		{
			get
			{
				return this.수주등록_WH적용;
			}
		}

		internal string Get과세변경유무
		{
			get
			{
				return this.수주라인_과세변경유무;
			}
		}

		internal string GetATP사용여부
		{
			get
			{
				return this.ATP사용여부;
			}
		}

		internal string GetExcCredit
		{
			get
			{
				return this._excCredit;
			}
		}

		internal string Get프로젝트적용
		{
			get
			{
				return this.수주등록_프로젝트적용;
			}
		}

		public DataSet 의뢰된건조회(string 수주번호)
		{
			DataSet dataSet = DBHelper.GetDataSet("UP_SA_SOL_TO_GIRL_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 수주번호, this.로그인언어 });
			T.SetDefaultValue(dataSet);

			return dataSet;
		}

		public DataTable BOM적용(object[] obj)
		{
			return DBHelper.GetDataTable("UP_SA_SO_BOM_S", obj);
		}

		public bool 해외적용건존재여부(string NO_SO)
		{
			string empty = string.Empty;
			string 회사코드 = MA.Login.회사코드;

			DataTable dataTable = DBHelper.GetDataTable(@"SELECT NO_OFFER NO_SO 
														  FROM TR_LCEXH WITH(NOLOCK)
														  WHERE CD_COMPANY = '" + 회사코드 + "' " +
														 "AND NO_OFFER = '" + NO_SO + "'  " +
														 "UNION ALL  " +
														 "SELECT NO_PO NO_SO " +
														 "FROM TR_INVL WITH(NOLOCK) " +
														 "WHERE CD_COMPANY = '" + 회사코드 + "' " +
														 "AND NO_PO    = '" + NO_SO + "' ");

			return dataTable != null && dataTable.Rows.Count != 0;
		}

		internal DataTable dtLot_Schema(DataTable dtLot)
		{
			dtLot.Columns.Add("NO_IO", typeof(string));
			dtLot.Columns.Add("NO_IOLINE", typeof(decimal));
			dtLot.Columns.Add("NO_ISURCV", typeof(string));
			dtLot.Columns.Add("NO_GIR", typeof(string));
			dtLot.Columns.Add("DT_DUEDATE", typeof(string));
			dtLot.Columns.Add("FG_TRANS", typeof(string));
			dtLot.Columns.Add("CD_QTIOTP", typeof(string));
			dtLot.Columns.Add("NM_QTIOTP", typeof(string));
			dtLot.Columns.Add("DT_IO", typeof(string));
			dtLot.Columns.Add("CD_SL", typeof(string));
			dtLot.Columns.Add("NM_SL", typeof(string));
			dtLot.Columns.Add("CD_ITEM", typeof(string));
			dtLot.Columns.Add("NM_ITEM", typeof(string));
			dtLot.Columns.Add("STND_ITEM", typeof(string));
			dtLot.Columns.Add("UNIT", typeof(string));
			dtLot.Columns.Add("UNIT_IM", typeof(string));
			dtLot.Columns.Add("FG_IO", typeof(string));
			dtLot.Columns.Add("QT_GIR", typeof(decimal));
			dtLot.Columns.Add("UNIT_SO_FACT", typeof(string));
			dtLot.Columns.Add("QT_GIR_IM", typeof(decimal));
			dtLot.Columns.Add("QT_IO", typeof(decimal));
			dtLot.Columns.Add("QT_GOOD_INV", typeof(decimal));
			dtLot.Columns.Add("CD_PLANT", typeof(string));
			dtLot.Columns.Add("CD_PJT", typeof(string));
			dtLot.Columns.Add("NO_PROJECT", typeof(string));
			dtLot.Columns.Add("NM_PROJECT", typeof(string));
			dtLot.Columns.Add("NO_EMP", typeof(string));
			dtLot.Columns.Add("NO_LOT", typeof(string));
			dtLot.Columns.Add("NO_SERL", typeof(string));
			dtLot.Columns.Add("NO_PSO_MGMT", typeof(string));
			dtLot.Columns.Add("NO_PSOLINE_MGMT", typeof(decimal));
			dtLot.Columns.Add("NO_IO_MGMT", typeof(string));
			dtLot.Columns.Add("NO_IOLINE_MGMT", typeof(decimal));
			dtLot.Columns.Add("CD_USERDEF1", typeof(string));
			dtLot.Columns.Add("CD_MNG2", typeof(string));
			dtLot.Columns.Add("CD_MNG3", typeof(string));

			return dtLot;
		}

		internal DataTable dtSerial_Schema(DataTable dtSerial)
		{
			dtSerial.Columns.Add("NO_IO", typeof(string));
			dtSerial.Columns.Add("NO_IOLINE", typeof(decimal));
			dtSerial.Columns.Add("NO_ISURCV", typeof(string));
			dtSerial.Columns.Add("NO_GIR", typeof(string));
			dtSerial.Columns.Add("DT_DUEDATE", typeof(string));
			dtSerial.Columns.Add("FG_TRANS", typeof(string));
			dtSerial.Columns.Add("CD_QTIOTP", typeof(string));
			dtSerial.Columns.Add("NM_QTIOTP", typeof(string));
			dtSerial.Columns.Add("DT_IO", typeof(string));
			dtSerial.Columns.Add("CD_SL", typeof(string));
			dtSerial.Columns.Add("NM_SL", typeof(string));
			dtSerial.Columns.Add("CD_ITEM", typeof(string));
			dtSerial.Columns.Add("NM_ITEM", typeof(string));
			dtSerial.Columns.Add("STND_ITEM", typeof(string));
			dtSerial.Columns.Add("UNIT", typeof(string));
			dtSerial.Columns.Add("UNIT_IM", typeof(string));
			dtSerial.Columns.Add("FG_IO", typeof(string));
			dtSerial.Columns.Add("QT_GIR", typeof(decimal));
			dtSerial.Columns.Add("UNIT_SO_FACT", typeof(string));
			dtSerial.Columns.Add("QT_GIR_IM", typeof(decimal));
			dtSerial.Columns.Add("QT_IO", typeof(decimal));
			dtSerial.Columns.Add("QT_GOOD_INV", typeof(decimal));
			dtSerial.Columns.Add("CD_PLANT", typeof(string));
			dtSerial.Columns.Add("CD_PJT", typeof(string));
			dtSerial.Columns.Add("NO_PROJECT", typeof(string));
			dtSerial.Columns.Add("NM_PROJECT", typeof(string));
			dtSerial.Columns.Add("NO_EMP", typeof(string));
			dtSerial.Columns.Add("NO_LOT", typeof(string));
			dtSerial.Columns.Add("NO_SERL", typeof(string));
			dtSerial.Columns.Add("NO_PSO_MGMT", typeof(string));
			dtSerial.Columns.Add("NO_PSOLINE_MGMT", typeof(decimal));
			dtSerial.Columns.Add("NO_IO_MGMT", typeof(string));
			dtSerial.Columns.Add("NO_IOLINE_MGMT", typeof(decimal));
			dtSerial.Columns.Add("CD_USERDEF1", typeof(string));
			dtSerial.Columns.Add("CD_MNG2", typeof(string));
			dtSerial.Columns.Add("CD_MNG3", typeof(string));

			return dtSerial;
		}

		public DataTable 창고별현재고조회(string noSo, string cd_item_multi)
		{
			return DBHelper.GetDataTable("UP_SA_SO_SL_INV_S", new object[] { this.CD_COMPANY, noSo, cd_item_multi, this.로그인언어 });
		}

		internal int SearchBizarea(string multiCdPlant)
		{
			return DBHelper.GetDataTable("UP_SA_BIZAREA_OF_PLANT_S", new object[] { this.CD_COMPANY, multiCdPlant }).Rows.Count;
		}

		internal bool IsConfirm(string NO_SO)
		{
			string empty = string.Empty;

			DataTable dataTable = DBHelper.GetDataTable(@"SELECT STA_SO  
														  FROM SA_SOL WITH(NOLOCK)
														  WHERE CD_COMPANY = '" + MA.Login.회사코드 + "' " +
														 "AND NO_SO = '" + NO_SO + "' " +
														 "AND STA_SO <> 'O'");

			return dataTable != null && dataTable.Rows.Count != 0;
		}

		internal DataTable SearchUmFixed(string cdPartner, string cdPlant, string multiItem)
		{
			return DBHelper.GetDataTable("UP_SA_SO_UM_FIXED_S", new object[] { MA.Login.회사코드, cdPartner, cdPlant, multiItem });
		}

		internal DataTable SearchNoSo(string NO_SO)
		{
			string empty = string.Empty;

			return DBHelper.GetDataTable(@"SELECT NO_SO, 
												  DT_SO  
										   FROM SA_SOH WITH(NOLOCK)  
										   WHERE  CD_COMPANY = '" + MA.Login.회사코드 + "' " +
										  "AND NO_SO = '" + NO_SO + "'");
		}

		internal DataRow SearchSo(string NO_SO, out bool 헤더수정유무, out string 단가적용형태)
		{
			string 회사코드 = MA.Login.회사코드;
			헤더수정유무 = true;

			DataTable dataTable = DBHelper.GetDataTable(@"SELECT H.CD_SALEGRP, 
																 H.STA_SO, 
																 L.TP_BUSI, 
																 L.TP_GI, 
																 L.TP_IV, 
																 L.GIR, 
																 L.GI, 
															     L.IV, 
																 L.TRADE, 
																 L.STA_SO AS STA_SO_LINE 
														  FROM SA_SOH H WITH(NOLOCK)
														  JOIN SA_SOL L WITH(NOLOCK) ON H.CD_COMPANY = L.CD_COMPANY AND H.NO_SO = L.NO_SO
														  WHERE H.CD_COMPANY = '" + 회사코드 + "' " +
														 "AND H.NO_SO = '" + NO_SO + "'");

			foreach (DataRow row in dataTable.Rows)
			{
				if (D.GetString(row["STA_SO_LINE"]) != "O")
				{
					헤더수정유무 = false;
					break;
				}
			}

			단가적용형태 = D.GetString(BASIC.GetSaleGrp(D.GetString(dataTable.Rows[0]["CD_SALEGRP"]))["TP_SALEPRICE"]);

			return dataTable.Rows[0];
		}

		internal DataTable IsFileHelpCheck()
		{
			string empty = string.Empty;

			return DBHelper.GetDataTable(@"SELECT TP_FILESERVER 
										   FROM MA_ENV WITH(NOLOCK)
										   WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'");
		}

		internal DataRow SerchBizarea(string cdPlant)
		{
			string empty = string.Empty;

			return DBHelper.GetDataTable(@"SELECT B.NO_BIZAREA, 
												  B.NM_BIZAREA, 
												  B.NM_MASTER, 
												  B.ADS_H, 
												  B.ADS_D, 
												  B.TP_JOB, 
												  B.CLS_JOB, 
												  B.NO_TEL  
										   FROM MA_PLANT P WITH(NOLOCK)        
										   JOIN DZSN_MA_BIZAREA B WITH(NOLOCK) ON P.CD_COMPANY = B.CD_COMPANY AND P.CD_BIZAREA = B.CD_BIZAREA  
										   WHERE P.CD_COMPANY = '" + MA.Login.회사코드 + "' " +
										  "AND P.CD_PLANT = '" + cdPlant + "'").Rows[0];
		}

		public DataTable Search_Print(string NO_SO)
		{
			DataTable dataTable = DBHelper.GetDataTable("UP_SA_SO_PRINT_S", new object[] { MA.Login.회사코드, NO_SO, D.GetNull(null), this.로그인언어 });
			T.SetDefaultValue(dataTable);

			return dataTable;
		}

		public string GetNoSo(string id_memo)
		{
			string str = @"SELECT A.NO_SO   
						   FROM SA_SOL A WITH(NOLOCK)  
						   WHERE A.ID_MEMO = '" + id_memo + "' " +
						  "AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' " +
						  "GROUP BY NO_SO";

			DataTable dataTable = Global.MainFrame.FillDataTable(str);

			return dataTable == null || dataTable.Rows.Count <= 0 ? string.Empty : D.GetString(dataTable.Rows[0]["NO_SO"]);
		}

		internal DataRow 수주유형(string 회사, string 공장, string 품목, string 거래처, string 환종, string 단가유형)
		{
			DataTable dataTable = DBHelper.GetDataTable(@"SELECT UP.CD_COMPANY, 
																 UP.CD_PLANT, 
																 UP.CD_ITEM, 
																 UP.UM_ITEM  
														  FROM MA_ITEM_UMPARTNER UP WITH(NOLOCK)
														  WHERE UP.CD_PARTNER ='" + 거래처 + "'  " +
														 "AND UP.FG_UM ='" + 단가유형 + "'   " +
														 "AND UP.CD_EXCH='" + 환종 + "'  " +
														 "AND UP.TP_UMMODULE = '002'  " +
														 "AND UP.CD_COMPANY ='" + 회사 + "'  " +
														 "AND UP.CD_PLANT = '" + 공장 + "'  " +
														@"AND UP.SDT_UM = (SELECT MAX(SDT_UM) 
																		   FROM MA_ITEM_UMPARTNER WITH(NOLOCK)
																		   WHERE CD_PARTNER = UP.CD_PARTNER  
																		   AND FG_UM = UP.FG_UM  
																		   AND CD_ITEM = UP.CD_ITEM  
																		   AND CD_EXCH = UP.CD_EXCH  
																		   AND TP_UMMODULE = UP.TP_UMMODULE  
																		   AND CD_COMPANY = UP.CD_COMPANY  
																		   AND CD_PLANT = UP.CD_PLANT  
																		   AND SDT_UM <= GETDATE())  
														  AND CD_ITEM IN (SELECT CD_STR FROM GETTABLEFROMSPLIT('" + 품목 + "'))");

			return dataTable == null || dataTable.Rows.Count == 0 ? null : dataTable.Rows[0];
		}

		internal DataTable Search_프로젝트_unit정보(string 프로젝트번호, int 프로젝트항번)
		{
			return DBHelper.GetDataTable(@"SELECT PL.SEQ_PROJECT, 
												  PL.CD_ITEM AS CD_UNIT, 
												  I2.NM_ITEM AS NM_UNIT, 
												  I2.STND_ITEM AS STND_UNIT
										   FROM DZSN_SA_PROJECTH H WITH(NOLOCK)
										   LEFT JOIN SA_PROJECTL PL WITH(NOLOCK) ON H.CD_COMPANY = PL.CD_COMPANY AND H.NO_PROJECT = PL.NO_PROJECT
										   LEFT JOIN DZSN_MA_PITEM I2 WITH(NOLOCK) ON PL.CD_COMPANY = I2.CD_COMPANY AND PL.CD_PLANT = I2.CD_PLANT AND PL.CD_ITEM = I2.CD_ITEM
										   WHERE H.CD_COMPANY = '" + MA.Login.회사코드 + "' " +
										  "AND PL.NO_PROJECT = '" + 프로젝트번호 + "' " +
										  "AND PL.SEQ_PROJECT = '" + 프로젝트항번 + "'");
		}
	}
}
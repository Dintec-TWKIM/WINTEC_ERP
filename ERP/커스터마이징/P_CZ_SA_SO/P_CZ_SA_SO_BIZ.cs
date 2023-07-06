using System;
using System.Data;
using System.Windows.Forms;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA.Common;

namespace cz
{
	class P_CZ_SA_SO_BIZ
	{
		string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;

		bool is수주반품사용여부 = false;
		string 수주등록_특수단가적용 = "000";
		string 수주등록_할인율적용   = "000";
		string 수주등록_예상이익산출 = "000";
		string 수주등록_WH적용       = "000";
		string 수주라인_과세변경유무 = "N";
		string ATP사용여부           = "000";
		string _excCredit            = "000";
		string 수주등록_프로젝트적용 = "000";
		string 수주등록_사양등록     = "000";

		private 수주관리.Config 수주Config = new 수주관리.Config();

		public P_CZ_SA_SO_BIZ()
		{
			BASIC.CacheDataClear(BASIC.CacheEnums.ALL);

			is수주반품사용여부    = BASIC.GetMAEXC("수주반품사용여부") == "Y" ? true : false;
			수주등록_특수단가적용 = BASIC.GetMAEXC("수주등록-특수단가적용");
			수주등록_할인율적용   = BASIC.GetMAEXC("수주등록-할인율적용");
			수주등록_예상이익산출 = BASIC.GetMAEXC("수주등록-예상이익산출");
			수주등록_WH적용       = BASIC.GetMAEXC("W/H 정보사용");
			수주라인_과세변경유무 = BASIC.GetMAEXC("수주라인-과세변경유무");
			ATP사용여부           = BASIC.GetMAEXC("ATP사용여부");
			_excCredit            = BASIC.GetMAEXC("여신한도");
			수주등록_프로젝트적용 = BASIC.GetMAEXC("수주등록-프로젝트 적용");
			수주등록_사양등록     = BASIC.GetMAEXC("수주등록-사양등록 사용여부");
		}

		#region ♣ 조회
		public DataSet Search(string NO_SO)
		{
			DataSet ds = null;

            ds = DBHelper.GetDataSet("SP_CZ_SA_SO_S", new object[] { CD_COMPANY, Global.MainFrame.LoginInfo.Language, NO_SO });

			T.SetDefaultValue(ds); 

			// 헤더테이블 디퐅트값
			DataTable dtHeader = ds.Tables[0];

			dtHeader.Columns["DT_SO"].DefaultValue = Global.MainFrame.GetStringToday;           //수주일자
			dtHeader.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;    //사번
			dtHeader.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;  //이름
			dtHeader.Columns["CD_EXCH"].DefaultValue = "000";                                   //화폐단위
			dtHeader.Columns["TP_PRICE"].DefaultValue = "001";                                   //단가유형
			dtHeader.Columns["TP_VAT"].DefaultValue = "11";                                    //VAT구분
			dtHeader.Columns["FG_VAT"].DefaultValue = "N";                                    //부가세
			dtHeader.Columns["FG_TAXP"].DefaultValue = "001";                                   //계산서처리
			dtHeader.Columns["NO_PROJECT"].DefaultValue = "";                                   //프로젝트
			dtHeader.Columns["DT_PROCESS"].DefaultValue = Global.MainFrame.GetStringToday;      //매출일자
			dtHeader.Columns["DT_RCP_RSV"].DefaultValue = DateTime.Now.AddDays(10).ToShortDateString().Replace("-", "");//수금예정일자

			//dtHeader.Columns["FG_TRANSPORT"].DefaultValue = "001";            // 운송방법 디폴트값 제외 요청(PIMS : D20110310011) : 2011.03.14 장은경
			
			return ds;
		}
		#endregion

		#region ♣ 거래구분조회
		/// <summary>
		/// GetTpBusi() : 수주형태에 따른 해당 거래유형의 값을 가져와서 VAT구분에 반영한다.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="[0] : COMPANY"></param>
		/// <param name="[1] : 수주형태"></param>
		/// <param name="[2] : 부가세코드"></param>
		/// <returns>[0] : 거래구분</returns>
		/// <returns>[1] : VAT구분</returns>
		/// <returns>[2] : 수주상태</returns>
		public string[] 거래구분(string 수주형태, string 부가세코드)
		{
			object[] outs;
			DBHelper.ExecuteNonQuery("UP_SA_SO_TPBUSI_SELECT", new object[] { CD_COMPANY, 수주형태, 부가세코드 }, out outs);

			string[] ret = new string[3];
			ret[0] = D.GetString(outs[0]);
			ret[1] = D.GetString(outs[1]);
			ret[2] = D.GetString(outs[2]);

			return ret;
		}

		#endregion

		#region ♣ 여신체크
		public bool CheckCredit(string 거래처, decimal 금액, string 의뢰여부, string 출고여부, ref string 수주상태)
		{
			string checkLevel = "001";
			object[] objOut = new object[3];

			if (의뢰여부 == "Y")
			{
				if (출고여부 == "Y") checkLevel = "101";
				else checkLevel = "100";
			}

			DBHelper.GetDataTable("UP_SA_CHECKCREDIT_SELECT", new object[] { CD_COMPANY, 거래처, 금액, checkLevel }, out objOut);

			if (objOut[0] != DBNull.Value)
			{
				string 여신총액 = D.GetDecimal(objOut[0]).ToString("###,###,###,###,##0.####");
				string 잔액     = D.GetDecimal(objOut[1]).ToString("###,###,###,###,##0.####");

				if (D.GetString(objOut[2]).ToString() == "002")
				{
					if (Global.MainFrame.ShowMessage("여신금액을 초과하였습니다. 저장하시겠습니까 ?\n(여신총액 : @, 잔액 : @)", new string[] { 여신총액, 잔액 }, "QY2") == DialogResult.Yes)
					{
						return true;
					}
					else
						return false;
				}
				else if (D.GetString(objOut[2]).ToString() == "003")
				{
					Global.MainFrame.ShowMessage("여신금액을 초과하여 저장할 수 없습니다. \n(여신총액 : @, 잔액 : @)", new string[] { 여신총액, 잔액 });
					return false;
				}
			}
			return true;
		}

		internal bool CheckCreditExec(string cdPartner, string cdExch, decimal amEx)
		{
			object[] objOut = new object[3];

			DBHelper.GetDataTable("UP_SA_CHECKCREDIT_EXCH_SELECT", new object[] { CD_COMPANY, cdPartner, cdExch, amEx, "001" }, out objOut);

			if (objOut[0] != DBNull.Value)
			{
				string 여신총액 = D.GetDecimal(objOut[0]).ToString("###,###,###,###,##0.####");
				string 잔액     = D.GetDecimal(objOut[1]).ToString("###,###,###,###,##0.####");

				if (D.GetString(objOut[2]) == "002")
				{
                    if (Global.MainFrame.ShowMessage("여신금액을 초과하였습니다. 저장하시겠습니까 ?\n(여신총액 : @, 잔액 : @)", new string[] { 여신총액, 잔액 }, "QY2") == DialogResult.Yes)
						return true;
					else
						return false;
				}
				else if (D.GetString(objOut[2]) == "003")
				{
                    Global.MainFrame.ShowMessage("여신금액을 초과하여 저장할 수 없습니다. \n(여신총액 : @ 잔액 : @)", new string[] { 여신총액, 잔액 });
					return false;
				}
			}

			return true;
		}
		#endregion

		#region ♣ 삭제
		public bool Delete(string NO_SO)
		{
			return DBHelper.ExecuteNonQuery("UP_SA_SO_DELETE", new object[] { CD_COMPANY, NO_SO });
		}

        public bool AutoDelete(string NO_SO)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_SA_SO_PROCESS_D", new object[] { CD_COMPANY, NO_SO, "Y" });
        }
		#endregion

		#region ♣ 저장
		public bool Save(DataTable dtH, DataTable dtL, DataTable dtLL, DataTable dtLot, string[] strArr)
		{
			SpInfoCollection sic = new SpInfoCollection();
			string NO_SO    = strArr[0];     string 수주상태 = strArr[1];    string 거래구분 = strArr[2];
			string 출고형태  = strArr[3];     string 매출형태 = strArr[4];    string 의뢰여부 = strArr[5];
			string 출고여부  = strArr[6];     string 매출여부 = strArr[7];    string 수출여부 = strArr[8];
			string 구분      = strArr[9];     string 반품여부 = strArr[10];   string 매출자동여부 = strArr[11];
			string 자동승인여부 = strArr[12];

			#region -> 헤더
			if (dtH != null)
			{
				string cdBizarea = string.Empty;
				if (dtH.Rows.Count > 0)
				{
					if (구분 == "복사" || dtH.Rows[0].RowState == DataRowState.Added)
					{
						cdBizarea = D.GetString(Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(Duzon.ERPU.MF.MasterSearch.MA_PLANT, new object[] { CD_COMPANY, D.GetString(dtL.Rows[0]["CD_PLANT"]) })["CD_BIZAREA"]);
					}
				}

				SpInfo siM = new SpInfo();

				if (구분 == "복사")
					siM.DataState = DataValueState.Added; //-> 처음 복사할때 바인딩 되기전에 처리해줬음

				siM.DataValue = dtH;
				siM.CompanyID = CD_COMPANY;
				siM.UserID = Global.MainFrame.LoginInfo.UserID;
				siM.SpNameInsert = "SP_CZ_SA_SOH_I";
				siM.SpNameUpdate = "SP_CZ_SA_SOH_U";
				siM.SpParamsInsert = new string[] { "CD_COMPANY",       "NO_SO",            "CD_BIZAREA",       "DT_SO",        "CD_PARTNER",
													"CD_SALEGRP",       "NO_EMP",           "TP_SO",            "CD_EXCH",      "RT_EXCH",
													"TP_PRICE",         "NO_PROJECT",       "TP_VAT",           "RT_VAT",       "FG_VAT",
													"FG_TAXP",          "DC_RMK",           "FG_BILL",          "FG_TRANSPORT", "NO_CONTRACT",
													"STA_SO",           "FG_TRACK",         "NO_PO_PARTNER",    "NO_EST",       "NO_EST_HST",
													"CD_EXPORT",        "CD_PRODUCT",       "COND_TRANS",       "COND_PAY",     "COND_DAYS",
													"TP_PACKING",       "TP_TRANS",         "TP_TRANSPORT",     "NM_INSPECT",   "PORT_LOADING",
													"PORT_ARRIVER",     "CD_ORIGIN",        "DESTINATION",      "DT_EXPIRY",    "CD_NOTIFY",
													"CD_CONSIGNEE",     "CD_TRANSPORT",     "DC_RMK_TEXT",      "ID_INSERT",    "RMA_REASON",
													"DC_RMK1",          "COND_PRICE",       "DT_USERDEF1",      "DT_USERDEF2",  "TXT_USERDEF1",
													"TXT_USERDEF2",     "TXT_USERDEF3",     "CD_USERDEF1",      "CD_USERDEF2",  "CD_USERDEF3",
													"NUM_USERDEF1",     "NUM_USERDEF2",     "NUM_USERDEF3",     "NUM_USERDEF4", "NUM_USERDEF5",
													"CD_BANK_SO",       "DC_RMK_TEXT2",     "NO_INV",           "TXT_USERDEF4", "NO_IMO" };
				siM.SpParamsUpdate = new string[] { "CD_COMPANY",       "NO_SO",            "DT_SO",            "CD_PARTNER",   "CD_SALEGRP",
													"NO_EMP",           "CD_EXCH",          "RT_EXCH",          "TP_PRICE",     "NO_PROJECT",
													"TP_VAT",           "FG_VAT",           "FG_TAXP",          "DC_RMK",       "FG_BILL",
													"FG_TRANSPORT",     "NO_CONTRACT",      "NO_PO_PARTNER",    "NO_EST",       "NO_EST_HST",
													"CD_EXPORT",        "CD_PRODUCT",       "COND_TRANS",       "COND_PAY",     "COND_DAYS",
													"TP_PACKING",       "TP_TRANS",         "TP_TRANSPORT",     "NM_INSPECT",   "PORT_LOADING",
													"PORT_ARRIVER",     "CD_ORIGIN",        "DESTINATION",      "DT_EXPIRY",    "CD_NOTIFY",
													"CD_CONSIGNEE",     "CD_TRANSPORT",     "DC_RMK_TEXT",      "ID_UPDATE",    "RMA_REASON",
													"DC_RMK1",          "COND_PRICE",       "DT_USERDEF1",      "DT_USERDEF2",  "TXT_USERDEF1",
													"TXT_USERDEF2",     "TXT_USERDEF3",     "CD_USERDEF1",      "CD_USERDEF2",  "CD_USERDEF3",
													"NUM_USERDEF1",     "NUM_USERDEF2",     "NUM_USERDEF3",     "NUM_USERDEF4", "NUM_USERDEF5",
													"CD_BANK_SO",       "DC_RMK_TEXT2",     "NO_INV",           "TXT_USERDEF4", "NO_IMO" };
				siM.SpParamsValues.Add(ActionState.Insert, "STA_SO", 수주상태);
				siM.SpParamsValues.Add(ActionState.Insert, "CD_BIZAREA", cdBizarea);
				sic.Add(siM);
			}
			#endregion

			#region -> 헤더SUB
			if (Get과세변경유무 == "N" && 매출자동여부 == "Y")
			{
				SpInfo si = new SpInfo();

				if (구분 == "복사")
					si.DataState = DataValueState.Added; //-> 처음 복사할때 바인딩 되기전에 처리해줬음

				si.DataValue = dtH;
				si.CompanyID = CD_COMPANY;
				si.UserID = Global.MainFrame.LoginInfo.UserID;
				si.SpNameInsert = "UP_SA_SOH_SUB_I";
				si.SpNameUpdate = "UP_SA_SOH_SUB_U";
				si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_SO", "DT_PROCESS", "DT_RCP_RSV", "FG_AR_EXC", "AM_IV", "AM_IV_EX", "AM_IV_VAT", "NM_PTR", "EX_EMIL", "EX_HP", "ID_INSERT", "DC_RMK_IVH" };
				si.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_SO", "DT_PROCESS", "DT_RCP_RSV", "FG_AR_EXC", "AM_IV", "AM_IV_EX", "AM_IV_VAT", "NM_PTR", "EX_EMIL", "EX_HP", "ID_UPDATE", "DC_RMK_IVH" };
				sic.Add(si);
			}
			#endregion

			#region -> 라인
			if (dtL != null)
			{
				SpInfo siD = new SpInfo();

				//if (구분 == "복사")
				//    siD.DataState = DataValueState.Added;

				siD.DataValue = dtL;
				siD.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				siD.UserID = Global.MainFrame.LoginInfo.UserID;
                siD.SpNameInsert = "SP_CZ_SA_SOL_I";
                siD.SpNameUpdate = "SP_CZ_SA_SOL_U";
                siD.SpNameDelete = "SP_CZ_SA_SOL_D";

				string colName = string.Empty;
				if (구분 == "복사")
					colName = "STA_SO1";
				else
					colName = "STA_SO";

				siD.SpParamsInsert = new string[] { "SERVERKEY",        "CD_COMPANY",       "NO_SO",            "SEQ_SO",           "CD_PLANT",         
													"CD_ITEM",          "UNIT_SO",          "DT_DUEDATE",       "DT_REQGI",         "QT_SO",
													"UM_SO",            "AM_SO",            "AM_WONAMT",        "AM_VAT",           "UNIT_IM",
													"QT_IM",            "CD_SL",            "TP_ITEM",          colName,            "TP_BUSI",
													"TP_GI",            "TP_IV",            "GIR",              "GI",               "IV",
													"TRADE",            "TP_VAT",           "RT_VAT",           "GI_PARTNER",       "ID_INSERT",
													"NO_PROJECT",       "SEQ_PROJECT",      "CD_ITEM_PARTNER",  "NM_ITEM_PARTNER",  "DC1",
													"DC2",              "UMVAT_SO",         "AMVAT_SO",         "CD_SHOP",          "CD_SPITEM",
													"CD_OPT",           "RT_DSCNT",         "UM_BASE",          "NM_CUST_DLV",      "CD_ZIP",
													"ADDR1",            "ADDR2",            "NO_TEL_D1",        "NO_TEL_D2",        "TP_DLV",
													"DC_REQ",           "FG_TRACK",         "TP_DLV_DUE",       "FG_USE",           "CD_CC",
													"NO_IO_MGMT",       "NO_IOLINE_MGMT",   "NO_POLINE_PARTNER","UM_OPT",           "NO_PO_PARTNER",
													"CD_WH",            "NO_SO_ORIGINAL",   "SEQ_SO_ORIGINAL",  "NUM_USERDEF1",     "NUM_USERDEF2",
													"NUM_USERDEF3",     "NUM_USERDEF4",     "NUM_USERDEF5",     "NUM_USERDEF6",     "CD_MNGD1",
													"CD_MNGD2",         "CD_MNGD3",         "CD_MNGD4",         "TXT_USERDEF1",     "TXT_USERDEF2",
													"YN_OPTION",        "NO_ORDER",         "NM_CUST",          "NO_TEL1",          "NO_TEL2",
													"DLV_TXT_USERDEF1", "CD_ITEM_REF",      "YN_PICKING",       "DLV_CD_USERDEF1",  "FG_USE2",
													"NO_LINK",          "NO_LINE_LINK",     "NO_RELATION",      "SEQ_RELATION",     "CD_USERDEF1"  };
				siD.SpParamsUpdate = new string[] { "CD_COMPANY",       "NO_SO",            "SEQ_SO",           "CD_PLANT",         "CD_ITEM", 
													"UNIT_SO",          "DT_DUEDATE",       "DT_REQGI",         "QT_SO",            "UM_SO", 
													"AM_SO",            "AM_WONAMT",        "AM_VAT",           "UNIT_IM",          "QT_IM", 
													"CD_SL",            "TP_ITEM",          "ID_UPDATE",        "GI_PARTNER",       "NO_PROJECT", 
													"SEQ_PROJECT",      "CD_ITEM_PARTNER",  "NM_ITEM_PARTNER",  "DC1",              "DC2", 
													"UMVAT_SO",         "AMVAT_SO",         "CD_SHOP",          "CD_SPITEM",        "CD_OPT", 
													"RT_DSCNT",         "UM_BASE",          "NM_CUST_DLV",      "CD_ZIP",           "ADDR1", 
													"ADDR2",            "NO_TEL_D1",        "NO_TEL_D2",        "TP_DLV",           "DC_REQ", 
													"FG_TRACK",         "TP_DLV_DUE",       "FG_USE",           "TP_VAT",           "RT_VAT",           
													"CD_CC",            "NO_POLINE_PARTNER","UM_OPT",           "NO_PO_PARTNER",    "CD_WH",
													"NO_SO_ORIGINAL",   "SEQ_SO_ORIGINAL",  "NUM_USERDEF1",     "NUM_USERDEF2",     "NUM_USERDEF3",
													"NUM_USERDEF4",     "NUM_USERDEF5",     "NUM_USERDEF6",     "CD_MNGD1",         "CD_MNGD2",
													"CD_MNGD3",         "CD_MNGD4",         "TXT_USERDEF1",     "TXT_USERDEF2",     "YN_OPTION",
													"NO_ORDER",         "NM_CUST",          "NO_TEL1",          "NO_TEL2",          "DLV_TXT_USERDEF1",
													"CD_ITEM_REF",      "YN_PICKING",       "DLV_CD_USERDEF1",  "FG_USE2",          "CD_USERDEF1" };
				siD.SpParamsDelete = new string[] { "CD_COMPANY", "NO_SO", "SEQ_SO" };

				siD.SpParamsValues.Add(ActionState.Insert, "NO_SO", NO_SO);
				siD.SpParamsValues.Add(ActionState.Insert, "SERVERKEY", Global.MainFrame.ServerKeyCommon.ToUpper());

				if (구분 != "복사")
					siD.SpParamsValues.Add(ActionState.Insert, "STA_SO", 수주상태);
				
				siD.SpParamsValues.Add(ActionState.Insert, "TP_BUSI", 거래구분);
				siD.SpParamsValues.Add(ActionState.Insert, "TP_GI", 출고형태);
				siD.SpParamsValues.Add(ActionState.Insert, "GIR", 의뢰여부);
				siD.SpParamsValues.Add(ActionState.Insert, "GI", 출고여부);
				siD.SpParamsValues.Add(ActionState.Insert, "IV", 매출여부);
				siD.SpParamsValues.Add(ActionState.Insert, "TRADE", 수출여부);
				siD.SpParamsValues.Add(ActionState.Update, "NO_SO", NO_SO);
				siD.SpParamsValues.Add(ActionState.Delete, "NO_SO", NO_SO);
				sic.Add(siD);
			}
			#endregion

			#region -> 소요자재 / 사양등록
			if (dtLL != null)
			{
				SpInfo si06 = new SpInfo();
				si06.DataValue = dtLL;
				si06.CompanyID = CD_COMPANY;
				si06.UserID = Global.MainFrame.LoginInfo.UserID;
				si06.SpNameInsert = "UP_SA_SOL_OPT_I";
				si06.SpNameDelete = "UP_SA_SOL_OPT_D";
				si06.SpParamsInsert = new string[] { "CD_COMPANY", "NO_SO", "SEQ_SO", "CD_PLANT", "NO_SPEC",
													 "CD_ITEM", "CD_MATL", "QT_INPUT", "QT", "DC_RMK", "ID_INSERT" };
				si06.SpParamsDelete = new string[] { "CD_COMPANY", "NO_SO", "SEQ_SO", "CD_PLANT", "NO_SPEC",
													 "CD_ITEM", "CD_MATL" };
				si06.SpParamsValues.Add(ActionState.Insert, "NO_SO", NO_SO);
				si06.SpParamsValues.Add(ActionState.Delete, "NO_SO", NO_SO);
				sic.Add(si06);
			}
			#endregion

			#region -> LOT정보
			if (dtLot != null)
			{
				SpInfo si03 = new SpInfo();
				si03.DataValue = dtLot;
				si03.DataState = DataValueState.Added;
				si03.SpNameInsert = "UP_MM_QTIOLOT_INSERT";
				si03.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				if (반품여부 == "N")
				{
					si03.SpParamsInsert = new string[] { "CD_COMPANY",  "출고번호", "출고항번",  "NO_LOT",     "CD_ITEM",
														 "출고일",      "FG_PS",    "수불구분",  "수불형태",   "창고코드",
														 "QT_GOOD_MNG", "NO_IO",    "NO_IOLINE", "NO_IOLINE2", "YN_RETURN",
														 "A", "B", "C", "D", "E", "F", "NO_SO"};
				}
				else
				{
					si03.SpParamsInsert = new string[] { "CD_COMPANY",  "NO_IO",    "NO_IOLINE",    "NO_LOT",       "CD_ITEM", 
														 "DT_IO",       "FG_PS",    "FG_IO",        "CD_QTIOTP",    "CD_SL", 
														 "QT_IO",       "NO_IO",    "NO_IOLINE",    "NO_IOLINE2",   "YN_RETURN",
														 "A", "B", "C", "D", "E", "F", "NO_SO"};
					si03.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", "Y");
				}
				si03.SpParamsValues.Add(ActionState.Insert, "A", string.Empty);
				si03.SpParamsValues.Add(ActionState.Insert, "B", string.Empty);
				si03.SpParamsValues.Add(ActionState.Insert, "C", 0);
				si03.SpParamsValues.Add(ActionState.Insert, "D", 0);
				si03.SpParamsValues.Add(ActionState.Insert, "E", string.Empty);
				si03.SpParamsValues.Add(ActionState.Insert, "F", string.Empty);
				si03.SpParamsValues.Add(ActionState.Insert, "NO_SO", NO_SO);
				sic.Add(si03);
			}
			#endregion

			#region -> 결제조건
			if (수주Config.결제조건도움창사용() && (dtH != null))
			{
				if (구분 == "복사" || dtH.Rows[0].RowState == DataRowState.Added)
				{
					string noProject = D.GetString(dtL.Rows[0]["NO_PROJECT"]);
					decimal seqProject = D.GetDecimal(dtL.Rows[0]["SEQ_PROJECT"]);

					//프로젝트항번이 존재하는 것은 프로젝트 적용을 받아온 것임
					if (noProject != string.Empty && seqProject > 0m)
					{
						DataTable dt = DBHelper.GetDataTable("UP_SA_SO_PAYCOND_S", new object[] { CD_COMPANY, noProject, "PRE" });

						SpInfo si05 = new SpInfo();
						si05.DataState = DataValueState.Added;
						si05.DataValue = dt;
						si05.CompanyID = CD_COMPANY;
						si05.UserID = Global.MainFrame.LoginInfo.UserID;
						si05.SpNameInsert = "UP_SA_SO_PAYCOND_I";
						si05.SpParamsInsert = new string[] { "CD_COMPANY", "FG_TRACK_SO", "NO_SO_NEW", "NO_LINE_PAYCOND", "CD_PAYCOND",
															 "MONTH_PAYCOND", "DAY_PAYCOND", "RT_PAYCOND", "DC_RMK", "ID_INSERT" };
						si05.SpParamsValues.Add(ActionState.Insert, "FG_TRACK_SO", "SO");
						si05.SpParamsValues.Add(ActionState.Insert, "NO_SO_NEW", D.GetString(dtH.Rows[0]["NO_SO"]));
						sic.Add(si05);
					}
				}
			}

			#endregion

			#region -> 자동프로세서 데이터 보정
			if (Get과세변경유무 == "N" && 매출자동여부 == "Y")
			{
				SpInfo si07 = new SpInfo();
				si07.DataValue = dtH;
				si07.CompanyID = CD_COMPANY;
				si07.UserID = Global.MainFrame.LoginInfo.UserID;
				si07.SpNameInsert = "UP_SA_SO_MODIFY";
				si07.SpParamsInsert = new string[] { "NO_SO", "CD_COMPANY" };
				sic.Add(si07);
			} 
			#endregion

			return DBHelper.Save(sic);
		}
		#endregion

		#region ♣ Excel Upload
		private ArrayListExt arr엑셀(string 멀티품목코드)
		{
			int MaxCnt = 50;
			int Cnt = 1;
			string 품목코드 = string.Empty;

			ArrayListExt arrList = new ArrayListExt();
			string[] arrstr = 멀티품목코드.Split('|');

			for (int i = 0; i < arrstr.Length - 1; i++)
			{
				품목코드 += arrstr[i].ToString() + "|";
				if (Cnt == MaxCnt)
				{
					arrList.Add(품목코드);
					품목코드 = string.Empty;
					Cnt = 0;
				}
				Cnt++;
			}

			if (품목코드 != string.Empty)
				arrList.Add(품목코드);
			return arrList;
		}
		#endregion

		#region ♣ 기타메소드

		internal DataTable 할인율(string 공장, string 거래처, DataRow[] dr품목)
		{
			품목관리.조회 품목조회 = new 품목관리.조회();
			DataTable dt = 품목조회.거래처그룹품목군할인율(공장, 거래처, dr품목);
			dt.PrimaryKey = new DataColumn[] { dt.Columns["CD_ITEM"] };
			return dt;
		}

		internal DataTable 예상이익(string 공장, string 수주일자, DataRow[] dr품목)
		{
			품목관리.조회 품목조회 = new 품목관리.조회();
			DataTable dt = 품목조회.예상이익(공장, 수주일자, dr품목);
			dt.PrimaryKey = new DataColumn[] { dt.Columns["CD_ITEM"] };
			return dt;
		}

		internal void 예상이익(FlexGrid flex, int idx)
		{
			decimal 수주수량 = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(flex[idx, "QT_SO"]));
			decimal 재고단가 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(flex[idx, "UM_INV"]));
			decimal 원화금액 = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(flex[idx, "AM_WONAMT"]));

			수주관리.Calc 수주관리계산 = new 수주관리.Calc();
			flex[idx, "AM_PROFIT"] = 수주관리계산.예상이익계산(수주수량, 재고단가, 원화금액);
		}

		#endregion

		#region ♣ 속성

		internal bool 수주반품사용여부 { get { return is수주반품사용여부; } }

		internal 특수단가적용 Get특수단가적용
		{
			get
			{
				if (수주등록_특수단가적용 == "001") return 특수단가적용.중량단가;
				if (수주등록_특수단가적용 == "002") return 특수단가적용.조선호텔베이커리단가;
				if (수주등록_특수단가적용 == "003") return 특수단가적용.거래처별고정단가;
				return 특수단가적용.NONE;
			}
		}

		internal 수주관리.할인율적용 Get할인율적용
		{
			get
			{
				수주관리.Config soconfig = new 수주관리.Config();

				return soconfig.할인율();
			}
		}

		internal 예상이익산출 Get예상이익산출적용
		{
			get
			{
				if (수주등록_예상이익산출 == "001") return 예상이익산출.재고단가를원가로산출;
				return 예상이익산출.NONE;
			}
		}

		internal string Get_WH적용 { get { return 수주등록_WH적용; } }
		internal string Get과세변경유무 { get { return 수주라인_과세변경유무; } }
		internal string GetATP사용여부 { get { return ATP사용여부; } }
		internal string GetExcCredit { get { return _excCredit; } }
		internal bool Get사양등록사용여부 { get { return 수주등록_사양등록 == "000" ? false : true; } }

		#endregion

		#region ♣ Lot_Schema

		internal DataTable dtLot_Schema(DataTable dtLot)
		{
			dtLot.Columns.Add("NO_IO",           typeof(string));
			dtLot.Columns.Add("NO_IOLINE",       typeof(decimal));
			dtLot.Columns.Add("NO_ISURCV",       typeof(string));
			dtLot.Columns.Add("NO_GIR",          typeof(string));
			dtLot.Columns.Add("DT_DUEDATE",      typeof(string));
			dtLot.Columns.Add("FG_TRANS",        typeof(string));
			dtLot.Columns.Add("CD_QTIOTP",       typeof(string));
			dtLot.Columns.Add("NM_QTIOTP",       typeof(string));
			dtLot.Columns.Add("DT_IO",           typeof(string));
			dtLot.Columns.Add("CD_SL",           typeof(string));
			dtLot.Columns.Add("NM_SL",           typeof(string));
			dtLot.Columns.Add("CD_ITEM",         typeof(string));
			dtLot.Columns.Add("NM_ITEM",         typeof(string));
			dtLot.Columns.Add("STND_ITEM",       typeof(string));
			dtLot.Columns.Add("UNIT",            typeof(string));
			dtLot.Columns.Add("UNIT_IM",         typeof(string));
			dtLot.Columns.Add("FG_IO",           typeof(string));
			dtLot.Columns.Add("QT_GIR",          typeof(decimal));
			dtLot.Columns.Add("UNIT_SO_FACT",    typeof(string));
			dtLot.Columns.Add("QT_GIR_IM",       typeof(decimal));
			dtLot.Columns.Add("QT_IO",           typeof(decimal));
			dtLot.Columns.Add("QT_GOOD_INV",     typeof(decimal));
			dtLot.Columns.Add("CD_PLANT",        typeof(string));
			dtLot.Columns.Add("CD_PJT",          typeof(string));
			dtLot.Columns.Add("NO_PROJECT",      typeof(string));
			dtLot.Columns.Add("NM_PROJECT",      typeof(string));
			dtLot.Columns.Add("NO_EMP",          typeof(string));
			dtLot.Columns.Add("NO_LOT",          typeof(string));
			dtLot.Columns.Add("NO_SERL",         typeof(string));
			dtLot.Columns.Add("NO_PSO_MGMT",     typeof(string));
			dtLot.Columns.Add("NO_PSOLINE_MGMT", typeof(decimal));
			dtLot.Columns.Add("NO_IO_MGMT",      typeof(string)); 
			dtLot.Columns.Add("NO_IOLINE_MGMT",  typeof(decimal));

			return dtLot;
		}

		#endregion

		#region ♣ 창고별 현재고 조회
		public DataTable 창고별현재고조회(string noSo, string cd_item_multi)
		{
			DataTable dt = DBHelper.GetDataTable("UP_SA_SO_SL_INV_S", new object[] { CD_COMPANY, noSo, cd_item_multi });
			return dt;
		}
		#endregion

		#region ♣ 공장별 사업장 조회
		internal int SearchBizarea(string multiCdPlant)
		{
			DataTable dt = DBHelper.GetDataTable("UP_SA_BIZAREA_OF_PLANT_S", new object[] { CD_COMPANY, multiCdPlant });
			return dt.Rows.Count;
		}
		#endregion

		#region ♣ 고정단가 조회

		internal DataTable SearchUmFixed(string cdPartner, string cdPlant, string multiItem)
		{
			return DBHelper.GetDataTable("UP_SA_SO_UM_FIXED_S", new object[] { MA.Login.회사코드, cdPartner, cdPlant, multiItem });
		}

		#endregion

		#region ♣ 인쇄시 사업장정보 조회
		internal DataRow SerchBizarea(string cdPlant)
		{
			string query = string.Empty;

			query = @"SELECT B.NO_BIZAREA,
                             B.NM_BIZAREA,
                             B.NM_MASTER,
                             B.ADS_H,
                             B.ADS_D,
                             B.TP_JOB,
                             B.CLS_JOB,
                             B.NO_TEL
				      FROM MA_PLANT P WITH(NOLOCK)
                      JOIN MA_BIZAREA B WITH(NOLOCK) ON P.CD_COMPANY = B.CD_COMPANY AND P.CD_BIZAREA = B.CD_BIZAREA
				      WHERE P.CD_COMPANY = '" + MA.Login.회사코드 + "'"
				   + "AND P.CD_PLANT = '" + cdPlant + "'";

			DataTable dt = DBHelper.GetDataTable(query);
			return dt.Rows[0];
		}
		#endregion

		#region ♣ 출력
		public DataTable Search_Print(string NO_SO)
		{
			DataTable dt = DBHelper.GetDataTable("UP_SA_SO_PRINT_S", new object[] { MA.Login.회사코드, NO_SO });
			T.SetDefaultValue(dt);
			return dt;
		}
		#endregion
	}
}
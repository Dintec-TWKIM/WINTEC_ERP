using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using DX;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace cz
{
	internal class P_CZ_SA_SO_MNG_WINTEC_BIZ
	{
		private string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
		private string 로그인언어 = Global.SystemLanguage.MultiLanguageLpoint;
		private string _excCredit = "000";
		private string ATP사용여부 = "000";

		public P_CZ_SA_SO_MNG_WINTEC_BIZ()
		{
			BASIC.CacheDataClear(BASIC.CacheEnums.ALL);
			this._excCredit = BASIC.GetMAEXC("여신한도");
			this.ATP사용여부 = BASIC.GetMAEXC("ATP사용여부");
		}

		public DataTable Search(string 날짜구분, string 조회구분, object[] obj)
		{
			DataTable dt;

			if (날짜구분 == "SO")
			{
				if (조회구분 == "1")
					dt = DBHelper.GetDataTable("SP_CZ_SA_SO_MNGH_WINTEC_SO_S", obj);
				else
					dt = DBHelper.GetDataTable("SP_CZ_SA_SO_MNGH_WINTEC_SO_S1", obj);
			}
			else if (날짜구분 == "DU")
			{
				if (조회구분 == "1")
					dt = DBHelper.GetDataTable("SP_CZ_SA_SO_MNGH_WINTEC_DU_S", obj);
				else
					dt = DBHelper.GetDataTable("SP_CZ_SA_SO_MNGH_WINTEC_DU_S1", obj);
			}
			else if (날짜구분 == "GI")
			{
				if (조회구분 == "1")
					dt = DBHelper.GetDataTable("SP_CZ_SA_SO_MNGH_WINTEC_GI_S", obj);
				else
					dt = DBHelper.GetDataTable("SP_CZ_SA_SO_MNGH_WINTEC_GI_S1", obj);
			}
            else
            {
				if (조회구분 == "1")
					dt = DBHelper.GetDataTable("SP_CZ_SA_SO_MNGH_WINTEC_IP_S", obj);
				else
					dt = DBHelper.GetDataTable("SP_CZ_SA_SO_MNGH_WINTEC_IP_S1", obj);
			}

			return dt;
		}

		public DataTable SearchDetail(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_SO_MNGL_WINTEC_S", obj);
			return dt;
		}

		public DataTable SearchSerial(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_SO_MNG_SERIAL_WINTEC_S", obj);
			return dt;
		}

		public DataSet SearchTrust(object[] obj)
		{
			DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_SO_MNG_TRUST_WINTEC_S", obj);
			return ds;
		}

		internal DataTable SearchCheckHeader(string 멀티수주번호, object[] obj)
		{
			DataTable dataTable1 = null;
			try
			{
				string[] pipes = D.StringConvert.GetPipes(멀티수주번호, 150);
				string str1 = D.GetString(pipes.Length);
				int num = 1;

				foreach (string str2 in pipes)
				{
					MsgControl.ShowMsg("자료를 조회중 입니다.(" + D.GetString(num++) + "/" + str1 + ")");

					obj[1] = str2;
					DataTable dataTable2 = DBHelper.GetDataTable("SP_CZ_SA_SO_MNG_WINTEC_CHECK_S", obj);

					if (dataTable1 == null)
						dataTable1 = dataTable2;
					else
						dataTable1.Merge(dataTable2);
				}
			}
			finally
			{
				MsgControl.CloseMsg();
			}

			return dataTable1;
		}

		public bool Save(DataTable dtH, DataTable dtL, DataTable dtItem, string 버튼유형, DataTable daou_dtH, DataTable daou_dtL)
		{
			SpInfoCollection spc = new SpInfoCollection();

			if (dtH != null) dtH.RemotingFormat = SerializationFormat.Binary;
			if (dtL != null) dtL.RemotingFormat = SerializationFormat.Binary;

			if ((dtL != null && (버튼유형 == "종결" || 버튼유형 == "종결취소")))
			{
				SpInfo spInfo = new SpInfo();

				spInfo.DataValue = dtL;
				spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
				spInfo.SpNameUpdate = "UP_SA_SOL_STA_LOG_I";
				spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
													   "CD_PLANT",
													   "NO_SO",
													   "SEQ_SO",
													   "CD_ITEM",
													   "FG_GUBUN",
													   "ID_INSERT",
													   "NM_KOR1",
													   "NM_INSERT1",
													   "NO_EMP1" };

				spInfo.SpParamsValues.Add(ActionState.Update, "FG_GUBUN", 버튼유형);
				spInfo.SpParamsValues.Add(ActionState.Update, "NM_INSERT1", MA.Login.사용자명);
				spInfo.SpParamsValues.Add(ActionState.Update, "NM_KOR1", MA.Login.사원명);
				spInfo.SpParamsValues.Add(ActionState.Update, "NO_EMP1", MA.Login.사원번호);

				spc.Add(spInfo);
			}

			if (dtH != null)
			{
				SpInfo spInfo = new SpInfo();

				spInfo.DataValue = dtH;
				spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
				spInfo.SpNameUpdate = "UP_SA_SOH_MNG_UPDATE";
				spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
													   "NO_SO",
													   "DC_RMK",
													   "NO_LC",
													   "DC_RMK_TEXT",
													   "NO_EMP",
													   "DC_RMK1" };

				spc.Add(spInfo);
			}
				
			if (dtL != null)
			{
				SpInfo spInfo = new SpInfo();

				spInfo.DataValue = dtL;
				spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
				spInfo.SpNameUpdate = "SP_CZ_SA_SO_SOL_U";
				spInfo.SpNameDelete = "SP_CZ_SA_SO_MNG_WINTEC_D";
				spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
													   "NO_SO",
													   "SEQ_SO",
													   "STA_SO",
													   "DC1",
													   "DC2",
													   "FG_USE",
													   "FG_USE2",
													   "ID_UPDATE",
													   "TXT_USERDEF1",
													   "TXT_USERDEF2",
													   "TXT_USERDEF3",
													   "DT_EXPECT",
													   "DT_DUEDATE",
													   "DT_REQGI",
													   "NUM_USERDEF3",
													   "QT_SCORE",
													   "TXT_USERDEF5",
													   "CD_USERDEF3" };
				spInfo.SpParamsDelete = new string[] { "CD_COMPANY",
												       "NO_SO",
												       "SEQ_SO" };

				spc.Add(spInfo);
			}

			if (dtItem != null)
			{
				SpInfo spInfo = new SpInfo();

				spInfo.DataValue = dtItem;
				spInfo.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				spInfo.UserID = Global.MainFrame.LoginInfo.UserID;
				spInfo.SpNameUpdate = "SP_CZ_SA_SO_MNG_WINTEC_U";
				spInfo.SpParamsUpdate = new string[] { "CD_COMPANY",
													   "NO_SO",
													   "SEQ_SO",
													   "STA_SO",
													   "DC1",
													   "DC2",
													   "FG_USE",
													   "FG_USE2",
													   "ID_UPDATE",
													   "TXT_USERDEF1",
													   "TXT_USERDEF2",
													   "TXT_USERDEF3",
													   "DT_EXPECT",
													   "DT_DUEDATE",
													   "DT_REQGI",
													   "NUM_USERDEF3",
													   "QT_SCORE",
													   "NO_LC",
													   "TXT_USERDEF5",
													   "CD_USERDEF3",
													   "TXT_USERDEF7",
													   "DC_RMK_TEXT",
													   "DC_RMK1" };

				spc.Add(spInfo);
			}
			return DBHelper.Save(spc);
		}

		internal bool SaveJson(DataTable dt)
		{
			return DBHelper.ExecuteNonQuery("SP_CZ_SA_SO_MNG_SERIAL_WINTEC_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
		}

		internal bool SaveJson1(DataTable dt)
		{
			return DBHelper.ExecuteNonQuery("SP_CZ_SA_SO_MNG_TRUST_WINTEC_JSON", new object[] { dt.Json(), Global.MainFrame.LoginInfo.UserID });
		}

		internal bool Save2(DataTable dt)
        {
			SpInfo si = new SpInfo();
			si.DataValue = dt;
			si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
			si.UserID = Global.MainFrame.LoginInfo.UserID;

			si.SpNameInsert = "SP_CZ_SA_SOL_DTS_UPDATE_U";
			si.SpNameUpdate = "SP_CZ_SA_SOL_DTS_UPDATE_U";
			si.SpNameDelete = "SP_CZ_SA_SOL_DTS_UPDATE_U";
			si.SpParamsInsert = new string[] { "CD_COMPANY",
											   "NO_SO",
											   "SEQ_SO" };
			si.SpParamsUpdate = new string[] { "CD_COMPANY",
											   "NO_SO",
											   "SEQ_SO" };
			si.SpParamsDelete = new string[] { "CD_COMPANY",
											   "NO_SO",
											   "SEQ_SO" };

			return DBHelper.Save(si);
		}

		internal void SaveContent(ContentType contentType, Dass.FlexGrid.CommandType commandType, string noSo, string value)
		{
			string query = string.Empty;
			string str = string.Empty;

			switch (contentType)
			{
				case ContentType.Memo:
					str = "MEMO_CD";
					break;
				case ContentType.CheckPen:
					str = "CHECK_PEN";
					break;
			}

			switch (commandType)
			{
				case Dass.FlexGrid.CommandType.Add:
					query = @"UPDATE SA_SOH 
							  SET " + str + " = '" + value + "' " +
							 "WHERE NO_SO = '" + noSo + "' " +
							 "AND CD_COMPANY = '" + this.CD_COMPANY + "'";
					break;
				case Dass.FlexGrid.CommandType.Delete:
					query = @"UPDATE SA_SOH 
							  SET " + str + " = NULL " +
							 "WHERE NO_SO = '" + noSo + "' " +
						     "AND CD_COMPANY = '" + this.CD_COMPANY + "'";
					break;
			}

			Global.MainFrame.ExecuteScalar(query);
		}

		public bool Delete(string 수주번호)
		{
			return DBHelper.ExecuteNonQuery("UP_SA_SO_DELETE_HL", new object[] { this.CD_COMPANY, 수주번호 });
		}

		public bool CheckCredit(string 거래처, Decimal 금액)
		{
			string str1 = "001";
			object[] outParameters = new object[3];
			List<object> objectList = new List<object>();
			objectList.Add(this.CD_COMPANY);
			objectList.Add(거래처);
			objectList.Add(금액);
			objectList.Add(str1);

			if (BASIC.GetMAEXC("수주관리-여신체크방법") == "001")
			{
				objectList.Add("001");
				objectList.Add(Global.MainFrame.GetStringToday);
			}

			DBHelper.GetDataTable("UP_SA_CHECKCREDIT_SELECT", objectList.ToArray(), out outParameters);

			if (outParameters[0] != DBNull.Value)
			{
				string str2 = D.GetDecimal(outParameters[0]).ToString("###,###,###,###,##0.####").PadLeft(15);
				string str3 = D.GetDecimal(outParameters[1]).ToString("###,###,###,###,##0.####").PadLeft(15);
				string str4 = "- 거 래 처  :   " + D.GetString(CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER, new object[] { MA.Login.회사코드, 거래처 })["LN_PARTNER"]) + " (" + 거래처 + ") \n- 여신잔액  : " + str2 + "\n- 수주금액  : " + str3 + "\n";

				if (D.GetString(outParameters[2]) == "002")
				{
					string code = "여신금액을 초과하였습니다. 그래도 확정 하시겠습니까?\n\n" + str4;
					return Global.MainFrame.ShowMessage(code, "QY2") == DialogResult.Yes;
				}

				if (D.GetString(outParameters[2]) == "003")
				{
					string code = "여신금액을 초과하여 확정 할 수 없습니다.\n\n" + str4;
					Global.MainFrame.ShowMessage(code);
					return false;
				}
			}

			return true;
		}

		internal bool CheckCreditExec(string cdPartner, string cdExch, Decimal amEx)
		{
			object[] outParameters = new object[3];
			DBHelper.GetDataTable("UP_SA_CHECKCREDIT_EXCH_SELECT", new object[] { this.CD_COMPANY,
																				  cdPartner,
																				  cdExch,
																				  amEx,
																				  "001" }, out outParameters);

			if (D.GetString(outParameters[0]) != string.Empty)
			{
				if (D.GetString(outParameters[2]) == "002")
					return Global.MainFrame.ShowMessage("여신금액을 초과하였습니다. [거래처 : " + cdPartner + "]\n(여신총액(" + cdExch + ") : " + D.GetString(outParameters[0]) + ", 잔액 : " + D.GetString(outParameters[1]) + ")\n저장하시겠습니까 ?", "QY2") == DialogResult.Yes;

				if (D.GetString(outParameters[2]) == "003")
				{
					Global.MainFrame.ShowMessage("여신금액을 초과하여 저장할 수 없습니다. \n(여신총액(" + cdExch + ") : " + D.GetString(outParameters[0]) + " 잔액 : " + D.GetString(outParameters[1]) + ")");
					return false;
				}
			}

			return true;
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

		internal string GetExcCredit
		{
			get
			{
				return this._excCredit;
			}
		}

		internal string GetATP사용여부
		{
			get
			{
				return this.ATP사용여부;
			}
		}

		public DataTable Search_Print(string NO_SO, string SEQ_SO)
		{
			DataTable dataTable = DBHelper.GetDataTable("UP_SA_SO_PRINT_S", new object[] { MA.Login.회사코드,
																						   NO_SO,
																						   SEQ_SO,
																						   this.로그인언어 });
			return dataTable;
		}

		internal DataTable IsFileHelpCheck()
		{
			return DBHelper.GetDataTable(@"SELECT TP_FILESERVER 
										   FROM MA_ENV WITH(NOLOCK) 
										   WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'");
		}

		public DataTable Get_FileName(string NO_SO)
		{
			string query = @"SELECT COUNT(1) FILE_PATH_MNG  
							 FROM MA_FILEINFO MF WITH(NOLOCK)  
						     WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'  " +
							"AND CD_FILE = '" + NO_SO + "'";

			return Global.MainFrame.FillDataTable(query);
		}

		internal DataTable Search_iv_avg(string cd_partner)
		{
			string query = @"SELECT SUM(AM_IV)/3 as IV_AVG
							 FROM SA_AR WITH(NOLOCK)
							 WHERE CD_COMPANY= '" + MA.Login.회사코드 + "' " +
							"AND YYMM >= LEFT(CONVERT(VARCHAR,dateadd(month,-2,getdate()),112),6)  " +
							"AND CD_PARTNER = '" + cd_partner + "'";

			return Global.MainFrame.FillDataTable(query);
		}

		internal DataTable Search_credit(string cd_partner)
		{
			string query = @"SELECT CD_PARTNER, 
									TOT_CREDIT, 
									AM_REAL, 
									AM_CREDIT, 
									AM_OTHERS
							 FROM SA_PTRCREDIT WITH(NOLOCK)
							 WHERE CD_COMPANY=  '" + MA.Login.회사코드 + "'   " +
							"AND CD_PARTNER = '" + cd_partner + "'";
			return Global.MainFrame.FillDataTable(query);
		}

		internal DataTable Search_fg_use()
		{
			string query = @"SELECT NM_SYSDEF, 
									CD_SYSDEF
							 FROM MA_CODEDTL WITH(NOLOCK)
							 WHERE CD_COMPANY=  '" + MA.Login.회사코드 + "'   " +
							"AND CD_FIELD = 'SA_B000057'";

			return Global.MainFrame.FillDataTable(query);
		}

		internal DataTable Search_umpartner(DataRow drL, string dt_so, string cd_partner)
		{
			string query = @"SELECT UM_ITEM
							 FROM MA_ITEM_UMPARTNER WITH(NOLOCK)
							 WHERE CD_COMPANY=  '" + MA.Login.회사코드 + "'    " +
							"AND CD_PARTNER = '" + cd_partner + "'    " +
							"AND CD_PLANT = '" + D.GetString(drL["CD_PLANT"]) + "'    " +
							"AND CD_EXCH =  '" + D.GetString(drL["CD_EXCH"]) + "'    " +
							"AND CD_ITEM = '" + D.GetString(drL["CD_ITEM"]) + "'    " +
							"AND SDT_UM <= '" + dt_so + "'    " +
						    "AND EDT_UM >= '" + dt_so + "'    " +
							"ORDER BY SDT_UM DESC ";

			return Global.MainFrame.FillDataTable(query);
		}

		internal DataTable get재고(string 공장, string 품목코드)
		{
			DataTable dataTable = DBHelper.GetDataTable(@"SELECT N.CD_COMPANY,       
																 N.CD_PLANT,       
																 N.CD_ITEM,       
																 SUM((QT_GOOD_OPEN + QT_REJECT_OPEN + QT_INSP_OPEN + QT_TRANS_OPEN) + 
																	 (QT_GOOD_GR + QT_REJECT_GR + QT_INSP_GR + QT_TRANS_GR) - 
																	 (QT_GOOD_GI + QT_REJECT_GI + QT_INSP_GI + QT_TRANS_GI)) AS QT_INV 
														  FROM MM_PINVN N WITH(NOLOCK)        
														  LEFT JOIN MA_PITEM I WITH(NOLOCK) ON N.CD_COMPANY = I.CD_COMPANY AND N.CD_PLANT = I.CD_PLANT AND N.CD_ITEM = I.CD_ITEM 
														  WHERE N.P_YR = LEFT(CONVERT(NVARCHAR, GETDATE(), 121), 4)  
														  AND I.CLS_ITEM IN ('001', '002', '003', '004', '005', '009')  
														  AND N.CD_PLANT = '" + 공장 + "'  " +
														 "AND N.CD_ITEM = '" + 품목코드 + "'  " +
														 "GROUP BY N.CD_COMPANY, N.CD_PLANT, N.CD_ITEM ");
			return dataTable;
		}
	}
}
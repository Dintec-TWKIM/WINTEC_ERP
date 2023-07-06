using System.Data;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using System;

namespace cz
{
	public class P_CZ_SA_GIRR_REG_BIZ
	{
		FlexGrid _flex = new FlexGrid();

		string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;
		string 출고반품_검사 = "000";
		string 거래처선택_영업그룹적용 = "N";
		string 거래처선택_담당자적용 = "N";
		string 거래처선택_단가유형적용 = "N";
		string 수주등록_특수단가적용 = "000";
		string 수주라인_과세변경유무 = "N";

		#region -> 생성자
		public P_CZ_SA_GIRR_REG_BIZ()
		{
			DataTable dt = DBHelper.GetDataTable("UP_SA_MA_EXC_S", new object[] { 회사코드, "수주반품사용여부" });

			출고반품_검사 = BASIC.GetMAEXC("출고반품_검사");
			거래처선택_영업그룹적용 = BASIC.GetMAEXC("거래처선택-영업그룹적용");
			거래처선택_담당자적용 = BASIC.GetMAEXC("거래처선택-담당자적용");
			거래처선택_단가유형적용 = BASIC.GetMAEXC("거래처선택-단가유형적용");
			수주등록_특수단가적용 = BASIC.GetMAEXC("수주등록-특수단가적용");
			수주라인_과세변경유무 = BASIC.GetMAEXC("수주라인-과세변경유무");
		}
		#endregion

		#region -> 조회
		public DataSet Search(object[] obj)
		{
			DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_GIRR_REG_S", obj);

			T.SetDefaultValue(ds);

			// 헤더테이블 디퐅트값
			DataTable dtHeader = ds.Tables[0];

			dtHeader.Columns["DT_GIR"].DefaultValue = Global.MainFrame.GetStringToday;
			dtHeader.Columns["CD_PLANT"].DefaultValue = Global.MainFrame.LoginInfo.CdPlant;
			dtHeader.Columns["TP_BUSI"].DefaultValue = "001";
			dtHeader.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
			dtHeader.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
			dtHeader.Columns["TP_VAT"].DefaultValue = "11";
			dtHeader.Columns["CD_EXCH"].DefaultValue = "";
			dtHeader.Columns["FG_TAXP"].DefaultValue = "001";
			dtHeader.Columns["FG_UM"].DefaultValue = "001";

			return ds;
		}
		#endregion

		#region -> 수량체크
		public bool Check(string 의뢰번호, decimal 의뢰항번, string 수불번호, decimal 수불항번, decimal 의뢰수량)
		{
			SpInfo si = new SpInfo();
			si.SpNameSelect = "UP_SA_GIRR_REG_SELECT1";
			si.SpParamsSelect = new object[] { 회사코드, 의뢰번호, 의뢰항번, 수불번호, 수불항번, 의뢰수량 };
			ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);
			string Result = rtn.OutParamsSelect[0, 0].ToString();

			if (Result == "1")
				return true;
			else
				return false;
		}
		#endregion

		#region -> 삭제
		public bool Delete(string 의뢰번호)
		{
			ResultData rtn = (ResultData)Global.MainFrame.ExecSp("UP_SA_GIRR_REG_DELETE", new object[] { 회사코드, 의뢰번호 });
			return rtn.Result;
		}
		#endregion

		#region -> 단가 구하기
		public decimal UmSearch(string 품목, string 거래처, string 단가유형, string 환종, string 단가적용형태, string 의뢰일자)
		{

			SpInfo si = new SpInfo();
			si.SpNameSelect = "UP_SA_SO_SELECT1";
			si.SpParamsSelect = new object[] { 회사코드, 품목, 거래처, 단가유형, 환종, 단가적용형태, 의뢰일자 };
			ResultData rtn = (ResultData)Global.MainFrame.FillDataTable(si);
			return _flex.CDecimal(rtn.OutParamsSelect[0, 0].ToString());
		}
		#endregion

		#region -> 저장관련

		public bool Save(DataTable dtH, DataTable dtL, DataTable dtLot, DataTable dtSer, string 의뢰번호, bool 추가모드여부, bool 라인헤더적용여부, P_CZ_SA_GIRR_REG main, string 수주번호)
		{
			SpInfoCollection sic = new SpInfoCollection();

			if (dtH != null)
			{
				SpInfo siM = new SpInfo();
				siM.DataValue = dtH;
				siM.CompanyID = 회사코드;
				siM.UserID = Global.MainFrame.LoginInfo.UserID;

				if (추가모드여부)
					siM.DataState = DataValueState.Added;

				siM.SpNameInsert = "UP_SA_GIRR_REG_INSERT";
				siM.SpNameUpdate = "UP_SA_GIRR_REG_UPDATE";
				siM.SpParamsInsert = new string[] { "CD_COMPANY", "NO_GIR", "DT_GIR", "CD_PARTNER", "CD_PLANT", "NO_EMP", "TP_BUSI", "DC_RMK", "ID_INSERT", "GI_PARTNER", "TP_GI", "FG_UM" };
                siM.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_GIR", "DT_GIR", "CD_PARTNER", "NO_EMP", "DC_RMK", "ID_UPDATE", "TXT_USERDEF4" };

				SaveData(siM, dtH, "NO_EMP", main.Get담당자, true, ActionState.Insert);
				SaveData(siM, dtH, "GI_PARTNER", main.Get납품처, true, ActionState.Insert);
				SaveData(siM, dtH, "DC_RMK", main.Get비고, true, ActionState.Insert);
				SaveData(siM, dtH, "TP_GI", main.Get반품형태, true, ActionState.Insert);
				SaveData(siM, dtH, "FG_UM", main.Get단가유형, true, ActionState.Insert);

				sic.Add(siM);
			}

			if (dtL != null)
			{
				SpInfo siD = new SpInfo();
				siD.DataValue = dtL;
				siD.CompanyID = 회사코드;
				siD.UserID = Global.MainFrame.LoginInfo.UserID;

				if (추가모드여부)
				{
					siD.DataState = DataValueState.Added;
				}

				siD.SpNameInsert = "UP_SA_GIRR_REG_INSERT1";
				siD.SpNameUpdate = "UP_SA_GIRR_REG_UPDATE1";
				siD.SpNameDelete = "UP_SA_GIRR_REG_DELETE1";
				siD.SpParamsInsert = new string[] { "CD_COMPANY",       "NO_GIR",       "SEQ_GIR",          "CD_ITEM",         "TP_ITEM",
														"DT_DUEDATE",       "DT_REQGI",     "YN_INSPECT",       "CD_SL",           "TP_GI",
														"QT_GIR",           "CD_EXCH",      "UM",               "AM_GIR",          "AM_GIRAMT",
														"AM_VAT",           "UNIT",         "QT_GIR_IM",        "GI_PARTNER",      "NO_PROJECT",
														"CD_SALEGRP",       "RT_EXCH",      "RT_VAT",           "TP_VAT",          "NO_EMP",
														"TP_IV",            "FG_TAXP",      "TP_BUSI",          "IV",              "NO_IO_MGMT",
														"NO_IOLINE_MGMT",   "NO_SO_MGMT",   "NO_SOLINE_MGMT",   "NO_LC",           "SEQ_LC",
														"ID_INSERT",        "NO_SO",        "SEQ_SO",           "NM_CUST_DLV",     "CD_ZIP",
														"ADDR1",            "ADDR2",        "NO_TEL_D1",        "NO_TEL_D2",       "TP_DLV",
														"DC_REQ",           "FG_TRACK",     "DC_RMK",           "YN_PICKING",      "DLV_TXT_USERDEF1",
														"DLV_CD_USERDEF1",  "TP_UM_TAX",    "UMVAT_GIR",        "SEQ_PROJECT" };
				siD.SpParamsUpdate = new string[] { "CD_COMPANY",       "NO_GIR",       "SEQ_GIR",          "CD_SL",           "QT_GIR",
														"UM",               "AM_GIR",       "AM_GIRAMT",        "AM_VAT",          "QT_GIR_IM", 
														"NO_PROJECT",       "GI_PARTNER",   "ID_UPDATE",        "NM_CUST_DLV",     "CD_ZIP",
														"ADDR1",            "ADDR2",        "NO_TEL_D1",        "NO_TEL_D2",       "TP_DLV",
														"DC_REQ",           "FG_TRACK",     "DC_RMK",           "YN_PICKING",      "DLV_TXT_USERDEF1",
														"DLV_CD_USERDEF1",  "TP_UM_TAX",    "UMVAT_GIR" };
				siD.SpParamsDelete = new string[] { "CD_COMPANY", "NO_GIR", "SEQ_GIR" };
				SaveData(siD, dtL, "NO_GIR", 의뢰번호, true, ActionState.Update);
				SaveData(siD, dtL, "NO_GIR", 의뢰번호, true, ActionState.Delete);

				SaveData(siD, dtL, "NO_GIR", 의뢰번호, 라인헤더적용여부, ActionState.Insert);
				SaveData(siD, dtL, "TP_BUSI", main.Get거래구분, 라인헤더적용여부, ActionState.Insert);
				SaveData(siD, dtL, "CD_SALEGRP", main.Get영업그룹, 라인헤더적용여부, ActionState.Insert);
				SaveData(siD, dtL, "NO_EMP", main.Get담당자, 라인헤더적용여부, ActionState.Insert);
				SaveData(siD, dtL, "TP_GI", main.Get반품형태, 라인헤더적용여부, ActionState.Insert);
				SaveData(siD, dtL, "TP_VAT", main.Get과세구분, 라인헤더적용여부, ActionState.Insert);
				SaveData2(siD, dtL, main.Get과세구분, main.Get부가세율, 라인헤더적용여부, ActionState.Insert);
				SaveData(siD, dtL, "IV", main.Get유무환구분, 라인헤더적용여부, ActionState.Insert);
				SaveData(siD, dtL, "CD_EXCH", main.Get환종, 라인헤더적용여부, ActionState.Insert);
				SaveData(siD, dtL, "RT_EXCH", main.Get환율, 라인헤더적용여부, ActionState.Insert);
				SaveData(siD, dtL, "FG_TAXP", main.Get계산서처리, 라인헤더적용여부, ActionState.Insert);
				SaveData(siD, dtL, "GI_PARTNER", main.Get납품처, 라인헤더적용여부, ActionState.Insert);

				sic.Add(siD);
			}

			if (dtLot != null)
			{
				dtLot.RemotingFormat = SerializationFormat.Binary;

				SpInfo si03 = new SpInfo();
				si03.DataValue = dtLot;
				si03.DataState = DataValueState.Added;
				si03.SpNameInsert = "UP_MM_QTIOLOT_I_R_INSERT";
				si03.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				//si03.SpParamsInsert = new string[] { "CD_COMPANY",  "출고번호", "출고항번",  "NO_LOT",     "CD_ITEM",
				//                                     "출고일",      "FG_PS",    "수불구분",  "수불형태",   "창고코드",
				//                                     "QT_GOOD_MNG", "NO_IO",    "NO_IOLINE", "NO_IOLINE2", "YN_RETURN",
				//                                     "NO_GIR",      "DT_LIMIT", "DC_LOTRMK", "NO_SO" };
				si03.SpParamsInsert = new string[] { "CD_COMPANY",  "NO_IO",    "NO_IOLINE", "NO_LOT",     "CD_ITEM", 
													 "DT_IO",       "FG_PS",    "FG_IO",     "CD_QTIOTP",  "CD_SL", 
													 "QT_IO",       "NO_IO",    "NO_IOLINE", "NO_IOLINE2", "YN_RETURN",
													 "NO_GIR",      "DT_LIMIT", "DC_LOTRMK", "NO_SO"};
				si03.SpParamsValues.Add(ActionState.Insert, "YN_RETURN", "Y");
				si03.SpParamsValues.Add(ActionState.Insert, "NO_GIR", 의뢰번호);
				si03.SpParamsValues.Add(ActionState.Insert, "NO_SO", 수주번호);
				sic.Add(si03);
			}

			if (dtSer != null)
			{
				dtSer.RemotingFormat = SerializationFormat.Binary;

				SpInfo si04 = new SpInfo();
				si04.DataValue = dtSer;

				si04.SpNameInsert = "UP_MM_QTIODS_I_R_INSERT";
				si04.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
				si04.SpParamsInsert = new string[] { "CD_COMPANY", "NO_SERIAL", "NO_IO",    "NO_IOLINE", "CD_ITEM", 
													 "CD_QTIOTP",  "FG_IO",     "CD_MNG1",	"CD_MNG2",	 "CD_MNG3",	
													 "CD_MNG4",	   "CD_MNG5",	"CD_MNG6",	"CD_MNG7",	 "CD_MNG8",	
													 "CD_MNG9",	   "CD_MNG10",  "CD_MNG11",	"CD_MNG12",	 "CD_MNG13",	
													 "CD_MNG14",   "CD_MNG15",	"CD_MNG16",	"CD_MNG17",	 "CD_MNG18",	
													 "CD_MNG19",   "CD_MNG20",  "NO_GIR",   "NO_SO" };
				si04.SpParamsValues.Add(ActionState.Insert, "NO_GIR", 의뢰번호);
				si04.SpParamsValues.Add(ActionState.Insert, "NO_SO", 수주번호);
				sic.Add(si04);
			}

			return DBHelper.Save(sic);
		}

		public void SaveData(SpInfo si, DataTable dt, string ColName, object val, bool isUpdate, ActionState state)
		{
			if (!dt.Columns.Contains(ColName))
			{
				si.SpParamsValues.Add(state, ColName, val);
				return;
			}

			DataRowState rowstate = DataRowState.Unchanged;

			if(state == ActionState.Insert)
				rowstate = DataRowState.Added;
			else if(state == ActionState.Update)
				rowstate = DataRowState.Modified;

			foreach (DataRow row in dt.Rows)
			{
				if (row.RowState == DataRowState.Deleted || !isUpdate) continue;

				if (row.RowState == rowstate)
				{
					if (dt.Columns[ColName].DataType == typeof(string))
						row[ColName] = D.GetString(row[ColName]) == string.Empty ? val : row[ColName];
					else
						row[ColName] = D.GetDecimal(row[ColName]) == 0M ? val : row[ColName];
				}
			}
		}

		public void SaveData2(SpInfo si, DataTable dt, object tpVat, object rtVat, bool isUpdate, ActionState state)
		{
			DataRowState rowstate = DataRowState.Unchanged;

			if(state == ActionState.Insert)
				rowstate = DataRowState.Added;
			else if(state == ActionState.Update)
				rowstate = DataRowState.Modified;

			foreach (DataRow row in dt.Rows)
			{
				if (row.RowState == DataRowState.Deleted || !isUpdate) continue;

				if (row.RowState == rowstate)
				{
					if (row["TP_VAT"] == tpVat)
						row["RT_VAT"] = rtVat;
				}
			}
		}
		#endregion

		#region -> 전용코드 조회
		internal string Get영업그룹적용유무
		{
			get
			{
				return 거래처선택_영업그룹적용;
			}
		}

		internal string Get담당자적용유무
		{
			get
			{
				return 거래처선택_담당자적용;
			}
		}
		#endregion

		#region -> 인쇄
		internal DataTable Print(object[] obj)
		{
			if (Global.MainFrame.ServerKeyCommon.ToUpper() == "TRIGEM")
				return DBHelper.GetDataTable("UP_SA_Z_TRIGEM_GIRR_REG_P", obj, "SEQ_GIR ASC");
			else
				return DBHelper.GetDataTable("UP_SA_GIRR_REG_P", obj, "SEQ_GIR ASC");
		}
		#endregion

		internal string Get특수단가적용 { get { return 수주등록_특수단가적용; } }

		public DataTable GetMAENV(string CD_EXC)
		{
			DataTable dt = DBHelper.GetDataTable(@"SELECT CD_EXC 
												   FROM MA_EXC WITH(NOLOCK) 
												   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
												  "AND EXC_TITLE = '" + CD_EXC + "'");
			return dt;
		}

		public DataTable Get거래처영업담당자(string 거래처)
		{
			DataTable dt = DBHelper.GetDataTable(@"SELECT A.CD_EMP_SALE, B.NM_KOR NM_EMP_SALE
                                                   FROM MA_PARTNER A WITH(NOLOCK)
	                                               LEFT JOIN MA_EMP B WITH(NOLOCK) ON B.CD_COMPANY = A.CD_COMPANY AND B.NO_EMP = A.CD_EMP_SALE
                                                   WHERE A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"' 
	                                               AND A.CD_PARTNER = '" + 거래처 + "'");
			return dt;
		}

		#region -> 고정단가 조회

		internal DataTable SearchUmFixed(string cdPartner, string cdPlant, string multiItem)
		{
			return DBHelper.GetDataTable("UP_SA_SO_UM_FIXED_S", new object[] { MA.Login.회사코드, cdPartner, cdPlant, multiItem });
		}

		#endregion
	}
}

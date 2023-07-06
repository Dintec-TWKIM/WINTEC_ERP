using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using System.IO;
using Dintec;

namespace cz
{
	class P_CZ_SA_GIR_PACK_BIZ
	{
        private string 회사코드;

        public P_CZ_SA_GIR_PACK_BIZ(string 회사코드)
        {
            this.회사코드 = 회사코드;
        }

		#region 기본정보
		public DataSet 기본정보검색(object[] obj)
		{
			DataSet ds;

            ds = DBHelper.GetDataSet("SP_CZ_SA_GIR_PACK_S", obj);

            T.SetDefaultValue(ds);

			ds.Tables[0].Columns["NO_GIR"].DefaultValue = string.Empty;
			ds.Tables[0].Columns["DT_GIR"].DefaultValue = Global.MainFrame.GetStringToday;
			ds.Tables[0].Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
			ds.Tables[0].Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.UserName;

			ds.Tables[0].Columns["NO_IMO"].DefaultValue = string.Empty;

            ds.Tables[0].Columns["NM_VESSEL"].DefaultValue = string.Empty;
			ds.Tables[0].Columns["CD_PARTNER"].DefaultValue = string.Empty;
			ds.Tables[0].Columns["LN_PARTNER"].DefaultValue = string.Empty;

            ds.Tables[0].Columns["YN_PACKING"].DefaultValue = "Y";

            ds.Tables[0].Columns["DC_RMK"].DefaultValue = string.Empty;
            
            ds.Tables[0].Columns["DT_COMPLETE"].DefaultValue = string.Empty;
			
			return ds;
		}

		public bool 기본정보저장(DataTable dt)
		{
			SpInfo si = new SpInfo();
			si.DataValue = dt;
            si.CompanyID = this.회사코드;
			si.UserID = Global.MainFrame.LoginInfo.UserID;

            si.SpNameInsert = "SP_CZ_SA_GIRH_PACK_I";
            si.SpNameUpdate = "SP_CZ_SA_GIRH_PACK_U";

            si.SpParamsInsert = new string[] { "CD_COMPANY",
                                               "NO_GIR",
                                               "DT_GIR",
                                               "CD_PARTNER",
                                               "CD_PLANT",
                                               "NO_EMP",
                                               "TP_BUSI",
                                               "CD_RMK",
                                               "DC_RMK",
                                               "DC_RMK1",
                                               "DC_RMK2",
                                               "DC_RMK3",
                                               "DC_RMK4",
                                               "DC_RMK5",
                                               "YN_RETURN",
                                               "CD_PACK_CATEGORY",
                                               "CD_SUB_CATEGORY",
                                               "YN_PACKING",
                                               "CD_COLLECT_FROM",
                                               "SEQ_COLLECT_PIC",
                                               "DT_START",
                                               "DT_COMPLETE",
                                               "NO_IMO",
                                               "ID_INSERT" };

            si.SpParamsUpdate = new string[] { "CD_COMPANY",
                                               "NO_GIR",
                                               "DT_GIR",
                                               "NO_EMP",
                                               "CD_PACK_CATEGORY",
                                               "CD_SUB_CATEGORY",
                                               "YN_PACKING",
                                               "CD_COLLECT_FROM",
                                               "SEQ_COLLECT_PIC",
                                               "DT_START",
                                               "DT_COMPLETE",
                                               "CD_RMK",
                                               "DC_RMK",
                                               "DC_RMK1",
                                               "DC_RMK2",
                                               "DC_RMK3",
                                               "DC_RMK4",
                                               "DC_RMK5",
                                               "NO_IMO",
                                               "ID_UPDATE" };

            return DBHelper.Save(si);
		}

		public bool 기본정보제거(string 의뢰번호)
		{
			ResultData result;

            result = (ResultData)Global.MainFrame.ExecSp("SP_CZ_SA_GIR_PACK_D", new object[] { this.회사코드, 의뢰번호 });

            return result.Result;
		}

		public bool 의뢰번호중복체크(string 의뢰번호)
		{
			string query;

            query = "SELECT COUNT(1)" + Environment.NewLine +
                    "FROM CZ_SA_GIRH_PACK WITH(NOLOCK)" + Environment.NewLine +
                    "WHERE CD_COMPANY = '" + this.회사코드 + "'" + Environment.NewLine +
					"AND NO_GIR = '" + 의뢰번호 + "'";

			DataTable dt = Global.MainFrame.FillDataTable(query);

			if (Convert.ToDecimal(dt.Rows[0][0]) > 0)
				return true;
			else
				return false;
		}

        public string 의뢰상태확인(string 의뢰번호)
        {
            string query;

            query = "SELECT STA_GIR" + Environment.NewLine +
                    "FROM CZ_SA_GIRH_PACK WITH(NOLOCK)" + Environment.NewLine +
                    "WHERE CD_COMPANY = '" + this.회사코드 + "'" + Environment.NewLine +
                    "AND NO_GIR = '" + 의뢰번호 + "'";

            return D.GetString(Global.MainFrame.ExecuteScalar(query));
        }

        public bool 의뢰상태갱신(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_SA_GIR_STA", obj);
        }
		#endregion

		#region 송장정보
		public DataTable 송장정보검색(object[] obj)
		{
			DataTable dt = DBHelper.GetDataTable("SP_CZ_TR_EXINV_S", obj);
			T.SetDefaultValue(dt);

			dt.Columns["NO_INV"].DefaultValue = string.Empty;
			dt.Columns["DT_BALLOT"].DefaultValue = Global.MainFrame.GetStringToday;  // 발행일자
			dt.Columns["YN_RETURN"].DefaultValue = "N";

			dt.Columns["CD_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaCode;
			dt.Columns["NM_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaName;
			dt.Columns["NO_EMP_INV"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
			dt.Columns["NM_KOR_INV"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
			dt.Columns["CD_EXCH"].DefaultValue = "000";       //통화

			dt.Columns["CD_ORIGIN"].DefaultValue = "001";
            dt.Columns["NM_ORIGIN"].DefaultValue = "KOREA";

			dt.Columns["CD_PRODUCT"].DefaultValue = string.Empty;

			dt.Columns["DT_LOADING"].DefaultValue = Global.MainFrame.GetStringToday;// 선적예정일
			dt.Columns["DT_TO"].DefaultValue = Global.MainFrame.GetStringToday;     // 통관예정일
			dt.Columns["TP_TRANSPORT"].DefaultValue = string.Empty;                 // INCOMES 지역
			dt.Columns["TP_TRANS"].DefaultValue = string.Empty;                     // 선적조건

			dt.Columns["PORT_LOADING"].DefaultValue = "BUSAN, KOREA";                     // 선적지

			dt.Columns["DTS_INSERT"].DefaultValue = Global.MainFrame.GetStringToday;
			dt.Columns["ID_INSERT"].DefaultValue = Global.MainFrame.LoginInfo.UserID;
			dt.Columns["DTS_UPDATE"].DefaultValue = Global.MainFrame.GetStringToday;
			dt.Columns["ID_UPDATE"].DefaultValue = Global.MainFrame.LoginInfo.UserID;

			return dt;
		}

		public bool 송장정보저장(DataTable dt)
		{
			SpInfo si = new SpInfo();
			si.DataValue = dt;
            si.CompanyID = this.회사코드;
			si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
			si.SpNameInsert = "SP_CZ_TR_EXINVH_I";
			si.SpNameUpdate = "SP_CZ_TR_EXINVH_U";

			si.SpParamsInsert = new string[] {  "NO_INV",       
                                                "CD_COMPANY",       
                                                "DT_BALLOT",        
                                                "CD_BIZAREA",       
                                                "CD_SALEGRP",   
                                                "NO_EMP_INV", 
												"FG_LC",        
                                                "CD_PARTNER",       
                                                "CD_EXCH",          
                                                "AM_EX",            
                                                "DT_LOADING", 
												"CD_ORIGIN",    
                                                "CD_AGENT",         
                                                "CD_EXPORT",        
                                                "CD_PRODUCT",       
                                                "SHIP_CORP", 
												"NM_VESSEL",    
                                                "COND_TRANS",       
                                                "TP_TRANSPORT",     
                                                "TP_TRANS",         
                                                "TP_PACKING", 
												"CD_WEIGHT",    
                                                "GROSS_WEIGHT",     
                                                "NET_WEIGHT",       
                                                "PORT_LOADING",     
                                                "PORT_ARRIVER", 
												"DESTINATION",  
                                                "NO_SCT",           
                                                "NO_ECT",           
                                                "CD_NOTIFY",        
                                                "DT_TO", 
												"NO_LC",        
                                                "NO_SO",            
                                                "REMARK1",          
                                                "REMARK2",          
                                                "REMARK3",
												"REMARK4",      
                                                "REMARK5",          
                                                "DTS_INSERT",       
                                                "ID_INSERT",        
                                                "DTS_UPDATE",
												"ID_UPDATE",    
                                                "NM_NOTIFY",        
                                                "ADDR1_NOTIFY",     
                                                "ADDR2_NOTIFY",     
                                                "CD_CONSIGNEE",
												"NM_CONSIGNEE", 
                                                "ADDR1_CONSIGNEE",  
                                                "ADDR2_CONSIGNEE",  
                                                "REMARK",           
                                                "NM_PARTNER",
												"ADDR1_PARTNER", 
                                                "ADDR2_PARTNER",   
                                                "NM_EXPORT",        
                                                "ADDR1_EXPORT",     
                                                "ADDR2_EXPORT",
												"COND_PRICE",   
                                                "DESCRIPTION",      
                                                "GROSS_VOLUME",     
                                                "FG_FREIGHT",       
                                                "AM_FREIGHT",
												"YN_RETURN",    
                                                "DT_SAILING_ON",    
                                                "TXT_REMARK2",      
                                                "CD_BANK",          
                                                "COND_PAY",
                                                "ARRIVER_COUNTRY",
                                                "YN_INSURANCE" };
			si.SpParamsUpdate = new string[] {  "NO_INV",       
                                                "CD_COMPANY",       
                                                "DT_BALLOT",        
                                                "CD_BIZAREA",       
                                                "CD_SALEGRP",   
                                                "NO_EMP_INV", 
												"FG_LC",        
                                                "CD_PARTNER",       
                                                "CD_EXCH",          
                                                "AM_EX",            
                                                "DT_LOADING", 
												"CD_ORIGIN",    
                                                "CD_AGENT",         
                                                "CD_EXPORT",        
                                                "CD_PRODUCT",       
                                                "SHIP_CORP", 
												"NM_VESSEL",    
                                                "COND_TRANS",       
                                                "TP_TRANSPORT",     
                                                "TP_TRANS",         
                                                "TP_PACKING", 
												"CD_WEIGHT",    
                                                "GROSS_WEIGHT",     
                                                "NET_WEIGHT",       
                                                "PORT_LOADING",     
                                                "PORT_ARRIVER", 
												"DESTINATION",  
                                                "NO_SCT",           
                                                "NO_ECT",           
                                                "CD_NOTIFY",        
                                                "DT_TO", 
												"NO_LC",        
                                                "NO_SO",            
                                                "REMARK1",          
                                                "REMARK2",          
                                                "REMARK3", 
												"REMARK4",      
                                                "REMARK5",          
                                                "DTS_INSERT",       
                                                "ID_INSERT",        
                                                "DTS_UPDATE",   
												"ID_UPDATE",    
                                                "NM_NOTIFY",        
                                                "ADDR1_NOTIFY",     
                                                "ADDR2_NOTIFY",     
                                                "CD_CONSIGNEE", 
												"NM_CONSIGNEE", 
                                                "ADDR1_CONSIGNEE",  
                                                "ADDR2_CONSIGNEE",  
                                                "REMARK",           
                                                "NM_PARTNER",
												"ADDR1_PARTNER", 
                                                "ADDR2_PARTNER",   
                                                "NM_EXPORT",        
                                                "ADDR1_EXPORT",     
                                                "ADDR2_EXPORT",
												"COND_PRICE",   
                                                "DESCRIPTION",      
                                                "GROSS_VOLUME",     
                                                "FG_FREIGHT",       
                                                "AM_FREIGHT" ,
												"YN_RETURN",    
                                                "DT_SAILING_ON",    
                                                "TXT_REMARK2",      
                                                "CD_BANK",          
                                                "COND_PAY",
                                                "ARRIVER_COUNTRY",
                                                "YN_INSURANCE" };
			return DBHelper.Save(si);
		}

		public bool 송장정보제거(string 송장번호)
		{
            ResultData result = (ResultData)Global.MainFrame.ExecSp("SP_CZ_TR_EXINV_D", new object[] { this.회사코드, 송장번호 });
			return result.Result;
		}

		public bool 송장번호중복체크(string 송장번호)
		{
			DataTable dt = Global.MainFrame.FillDataTable(@"SELECT COUNT(1)
							                                FROM CZ_TR_INVH WITH(NOLOCK)
							                                WHERE CD_COMPANY = '" + this.회사코드 + @"'
						                                    AND NO_INV = '" + 송장번호 + @"'");
			if (Convert.ToDecimal(dt.Rows[0][0]) > 0)
				return true;

			return false;
		}
		#endregion

		#region 품목정보
		public bool 품목정보저장(DataTable dt, string 의뢰번호, string 송장번호)
		{
			SpInfoCollection sic = new SpInfoCollection();
			SpInfo si;

			if (dt != null)
			{
				#region 품목정보
				si = new SpInfo();
                si.DataValue = Util.GetXmlTable(dt);
                si.CompanyID = this.회사코드;
				si.UserID = Global.MainFrame.LoginInfo.UserID;

                si.SpNameInsert = "SP_CZ_SA_GIRL_PACK_XML";

                si.SpParamsInsert = new string[] { "CD_COMPANY", "NO_GIR", "NO_INV", "XML", "ID_INSERT" };

                si.SpParamsValues.Add(ActionState.Insert, "NO_GIR", 의뢰번호);
                si.SpParamsValues.Add(ActionState.Insert, "NO_INV", 송장번호);

				sic.Add(si);
				#endregion
			}

			if (sic.List == null) return false;

			return DBHelper.Save(sic);
		}
		#endregion

		#region 의뢰수량체크
		public bool Check(object[] obj, out decimal 분할의뢰수량)
		{
			분할의뢰수량 = 0;
			object[] outs = null;
			DataTable dt;

            dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_PACK_CHK", obj, out outs);

            분할의뢰수량 = D.GetDecimal(outs[1]);

			bool isreturn = false;
			if (D.GetDecimal(outs[0]) == 1)
				isreturn = true;

			return isreturn;
		}
		#endregion

        #region 리포트정보
        public DataTable 협조전헤더(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIRSCHH_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }

        public DataTable 협조전라인(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIRSCHL_S", obj);
            T.SetDefaultValue(dt);
            return dt;
        }
        #endregion

        #region 자동제출
        public DataSet 자동제출대상검색(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_GIR_AUTO_SUBMIT_S", obj);
            T.SetDefaultValue(ds);

            return ds;
        }
        #endregion

        #region 협조전진행수량체크
        public bool 협조전진행수량체크(string 의뢰번호, DataTable dt)
        {
            string xml = Util.GetTO_Xml(dt);

            DataTable tempdt = DBHelper.GetDataTable("SP_CZ_SA_GIRL_CHECK_XML", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                               의뢰번호,
                                                                                               xml, 
                                                                                               Global.MainFrame.LoginInfo.UserID });

            if (tempdt != null && tempdt.Rows.Count > 0)
                return false;
            else
                return true;
        }
        #endregion

        public DataSet DHL(object[] obj)
        {
            DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_GIRSCH_DHL", obj);
            T.SetDefaultValue(ds);
            return ds;
        }

        public DataTable 기포장정보(object[] obj)
		{
            DataTable dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_PACK_INFO", obj);
            return dt;
        }
    }
}

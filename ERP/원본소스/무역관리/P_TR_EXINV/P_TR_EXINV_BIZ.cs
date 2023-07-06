using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;
using Duzon.ERPU;

namespace trade
{
    public class P_TR_EXINV_BIZ
    {
        string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;

        public DataTable Search(string 송장번호)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_TR_EXINV_SELECT";
            si.SpParamsSelect = new object[] { 회사코드, 송장번호 };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;

            foreach (DataColumn Col in dt.Columns)
            {
                if (Col.DataType == Type.GetType("System.Decimal"))
                    Col.DefaultValue = 0;
            }

            dt.Columns["DT_BALLOT"].DefaultValue = Global.MainFrame.GetStringToday; // 발행일자
            dt.Columns["FG_LC"].DefaultValue = "004";

            dt.Columns["CD_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaCode;
            dt.Columns["NM_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaName;
            dt.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dt.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            dt.Columns["CD_EXCH"].DefaultValue = "000";       //통화

            dt.Columns["DT_LOADING"].DefaultValue = Global.MainFrame.GetStringToday;// 선적예정일
            dt.Columns["DT_TO"].DefaultValue = Global.MainFrame.GetStringToday;     // 통관예정일
            dt.Columns["TP_TRANSPORT"].DefaultValue = "001";                        // 운송형태
            dt.Columns["TP_TRANS"].DefaultValue = "001";                            // 운송방법

            dt.Columns["DTS_INSERT"].DefaultValue = Global.MainFrame.GetStringToday;     
            dt.Columns["ID_INSERT"].DefaultValue = Global.MainFrame.LoginInfo.UserID;    
            dt.Columns["DTS_UPDATE"].DefaultValue = Global.MainFrame.GetStringToday;    
            dt.Columns["ID_UPDATE"].DefaultValue = Global.MainFrame.LoginInfo.UserID;

            dt.Columns["YN_RETURN"].DefaultValue = "N";
   
            return dt;
        }

        public bool Delete(string 송장번호)
        {
            ResultData result = (ResultData)Global.MainFrame.ExecSp("UP_TR_EXINV_DELETE", new object[] { 회사코드, 송장번호 });
            return result.Result;
        }

        public bool Save(DataTable dt)
        {
            SpInfo si = new SpInfo();
            si.DataValue = dt;
            si.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
            si.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
            si.SpNameInsert = "UP_TR_EXINV_INSERT";
            si.SpNameUpdate = "UP_TR_EXINV_UPDATE";

            si.SpParamsInsert = new string[] {  "NO_INV",       "CD_COMPANY",       "DT_BALLOT",        "CD_BIZAREA",       "CD_SALEGRP",   "NO_EMP", 
                                                "FG_LC",        "CD_PARTNER",       "CD_EXCH",          "AM_EX",            "DT_LOADING", 
                                                "CD_ORIGIN",    "CD_AGENT",         "CD_EXPORT",        "CD_PRODUCT",       "SHIP_CORP", 
                                                "NM_VESSEL",    "COND_TRANS",       "TP_TRANSPORT",     "TP_TRANS",         "TP_PACKING", 
                                                "CD_WEIGHT",    "GROSS_WEIGHT",     "NET_WEIGHT",       "PORT_LOADING",     "PORT_ARRIVER", 
                                                "DESTINATION",  "NO_SCT",           "NO_ECT",           "CD_NOTIFY",        "DT_TO", 
                                                "NO_LC",        "NO_SO",            "REMARK1",          "REMARK2",          "REMARK3",
                                                "REMARK4",      "REMARK5",          "DTS_INSERT",       "ID_INSERT",        "DTS_UPDATE",
                                                "ID_UPDATE",    "NM_NOTIFY",        "ADDR1_NOTIFY",     "ADDR2_NOTIFY",     "CD_CONSIGNEE",
	                                            "NM_CONSIGNEE", "ADDR1_CONSIGNEE",  "ADDR2_CONSIGNEE",  "REMARK",           "NM_PARTNER",
                                                "ADDR1_PARTNER", "ADDR2_PARTNER",   "NM_EXPORT",        "ADDR1_EXPORT",     "ADDR2_EXPORT",
                                                "COND_PRICE",   "DESCRIPTION",      "GROSS_VOLUME",     "FG_FREIGHT",       "AM_FREIGHT",
                                                "YN_RETURN",    "DT_SAILING_ON",    "TXT_REMARK2",      "CD_BANK",          "COND_PAY"
                                             };
            si.SpParamsUpdate = new string[] {  "NO_INV",       "CD_COMPANY",       "DT_BALLOT",        "CD_BIZAREA",       "CD_SALEGRP",   "NO_EMP", 
                                                "FG_LC",        "CD_PARTNER",       "CD_EXCH",          "AM_EX",            "DT_LOADING", 
                                                "CD_ORIGIN",    "CD_AGENT",         "CD_EXPORT",        "CD_PRODUCT",       "SHIP_CORP", 
                                                "NM_VESSEL",    "COND_TRANS",       "TP_TRANSPORT",     "TP_TRANS",         "TP_PACKING", 
                                                "CD_WEIGHT",    "GROSS_WEIGHT",     "NET_WEIGHT",       "PORT_LOADING",     "PORT_ARRIVER", 
                                                "DESTINATION",  "NO_SCT",           "NO_ECT",           "CD_NOTIFY",        "DT_TO", 
                                                "NO_LC",        "NO_SO",            "REMARK1",          "REMARK2",          "REMARK3", 
                                                "REMARK4",      "REMARK5",          "DTS_INSERT",       "ID_INSERT",        "DTS_UPDATE",   
                                                "ID_UPDATE",    "NM_NOTIFY",        "ADDR1_NOTIFY",     "ADDR2_NOTIFY",     "CD_CONSIGNEE", 
                                                "NM_CONSIGNEE", "ADDR1_CONSIGNEE",  "ADDR2_CONSIGNEE",  "REMARK",           "NM_PARTNER",
                                                "ADDR1_PARTNER", "ADDR2_PARTNER",   "NM_EXPORT",        "ADDR1_EXPORT",     "ADDR2_EXPORT",
                                                "COND_PRICE",   "DESCRIPTION",      "GROSS_VOLUME",     "FG_FREIGHT",       "AM_FREIGHT" ,
                                                "YN_RETURN",    "DT_SAILING_ON",    "TXT_REMARK2",      "CD_BANK",          "COND_PAY"
                                            };
            ResultData result = (ResultData)Global.MainFrame.Save(si);
            return result.Result;
        }

        internal DataTable SearchInvBF(string INV번호)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_TR_EXINV_COSTBF_SELECT";
            si.SpParamsSelect = new object[] { 회사코드, INV번호 };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            return (DataTable)result.DataValue;
        }

        internal DataRow SearchPacking(string 송장번호)
        {
            string sql = string.Empty;
            sql = " SELECT	NO_SCT, NO_ECT, NET_WEIGHT, GROSS_WEIGHT, GROSS_VOLUME ";
            sql += "  FROM	TR_INVH ";
            sql += " WHERE	CD_COMPANY	= '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND NO_INV = '" + 송장번호 + "'";
            DataTable dt = DBHelper.GetDataTable(sql);
            if (dt == null || dt.Rows.Count == 0) return null;

            return dt.Rows[0];
        }

        internal DataRow SearchLC내도정보(string 송장번호)
        {
            DataTable dt = DBHelper.GetDataTable("UP_TR_EXINVL_S", new object[] { MA.Login.회사코드, 송장번호 });
            if (dt == null || dt.Rows.Count == 0) return null;
       
            return dt.Rows[0];
        }

        public bool IS_NO_INV_EXIST(string 송장번호)
        {
            DataTable dt = Global.MainFrame.FillDataTable(@"
                            SELECT COUNT(1)
                            FROM TR_INVH
                            WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
                                AND NO_INV = '" + 송장번호 + @"'
                            ");
            if (Convert.ToDecimal(dt.Rows[0][0]) > 0)
                return true;
            return false;
        }
    }
}

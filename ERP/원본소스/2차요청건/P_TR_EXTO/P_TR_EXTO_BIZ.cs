using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using System.Data;
using Duzon.Common.Util;

namespace trade
{
    internal class P_TR_EXTO_BIZ
    {
        private string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;

        internal DataTable Search(string 통관번호)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_TR_EXTO_SELECT";
            si.SpParamsSelect = new object[] { 회사코드, 통관번호 };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;

            foreach (DataColumn Col in dt.Columns)
            {
                if (Col.DataType == Type.GetType("System.Decimal"))
                    Col.DefaultValue = 0;
            }
            
            dt.Columns["FG_EXLICENSE"].DefaultValue = "001";
            dt.Columns["FG_LC"].DefaultValue = "003";
            dt.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dt.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            dt.Columns["CD_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaCode;
            dt.Columns["NM_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaName;
            //dt.Columns["CD_EXPORT"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            //dt.Columns["NM_EXPORT"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            dt.Columns["CD_EXCH"].DefaultValue = "000";
            dt.Columns["YN_BL"].DefaultValue = "N";
            dt.Columns["DT_LICENSE"].DefaultValue = Global.MainFrame.GetStringToday;    // 면허일자
            dt.Columns["DT_DECLARE"].DefaultValue = Global.MainFrame.GetStringToday;    // 신고일자
            dt.Columns["DT_INSP"].DefaultValue = Global.MainFrame.GetStringToday;       // 검사증발급일
            dt.Columns["DT_QUAR"].DefaultValue = Global.MainFrame.GetStringToday;       // 검역증발급일

            dt.Columns["YN_RETURN"].DefaultValue = "N";       // 검역증발급일

            return dt;
        }

        internal bool Delete(string 통관번호)
        {
            try
            {
                ResultData result = (ResultData)Global.MainFrame.ExecSp("UP_TR_EXTO_DELETE", new object[] { 회사코드, 통관번호 });
                return result.Result;
            }
            catch (coDbException ex)
            {
                if (ex.DbErrorNo == -5000)
                {
                    Global.MainFrame.ShowMessage("판매경비가 등록되어 있으므로 삭제할 수 없습니다.");
                }
                else
                {
                    throw ex;
                }
            }

            return false;
        }

        public bool Save(DataTable dt, DataTable dtLine)
        {
            SpInfoCollection sc = new SpInfoCollection();

            //통관등록 헤더
            if (dt != null)
            {
                SpInfo si1 = new SpInfo();
                si1.DataValue = dt;
                si1.CompanyID = 회사코드;
                si1.SpNameInsert = "UP_TR_EXTO_INSERT";			// Insert 프로시저명
                si1.SpNameUpdate = "UP_TR_EXTO_UPDATE";			// Update 프로시저명
                si1.SpParamsInsert = new string[] { "NO_TO", "CD_COMPANY", "NO_INV", "CD_BIZAREA", "CD_SALEGRP", "NO_EMP", "FG_LC", "CD_PARTNER", "FG_EXLICENSE", "NO_EXLICENSE", "DT_LICENSE", "CD_EXCH", "RT_LICENSE", "RT_EXCH", "AM_EX", "AM", "AM_EXFOB", "AM_FOB", "AM_FREIGHT", "AM_INSUR", "CD_AGENT", "CD_PRODUCT", "CD_EXPORT", "FG_RETURN", "DT_DECLARE", "DC_DECLARE", "CD_CUSTOMS", "DC_CY", "NO_INSP", "DT_INSP", "NO_QUAR", "DT_QUAR", "TP_PACKING", "CNT_PACKING", "REMARK1", "REMARK2", "REMARK3", "YN_BL", "ID_INSERT", "YN_RETURN" };
                si1.SpParamsUpdate = new string[] { "NO_TO", "CD_COMPANY", "NO_INV", "CD_BIZAREA", "CD_SALEGRP", "NO_EMP", "FG_LC", "CD_PARTNER", "FG_EXLICENSE", "NO_EXLICENSE", "DT_LICENSE", "CD_EXCH", "RT_LICENSE", "RT_EXCH", "AM_EX", "AM", "AM_EXFOB", "AM_FOB", "AM_FREIGHT", "AM_INSUR", "CD_AGENT", "CD_PRODUCT", "CD_EXPORT", "FG_RETURN", "DT_DECLARE", "DC_DECLARE", "CD_CUSTOMS", "DC_CY", "NO_INSP", "DT_INSP", "NO_QUAR", "DT_QUAR", "TP_PACKING", "CNT_PACKING", "REMARK1", "REMARK2", "REMARK3", "YN_BL", "ID_UPDATE", "YN_RETURN" };
                sc.Add(si1);
            }

            //통관등록 라인
            if (dtLine != null)
            {
                SpInfo si2 = new SpInfo();
                si2.DataValue = dtLine;
                si2.CompanyID = 회사코드;
                si2.SpNameInsert = "UP_TR_EXTO_LINE_INSERT";			// Insert 프로시저명
                si2.SpNameUpdate = "UP_TR_EXTO_LINE_INSERT";			// Update 프로시저명
                si2.SpParamsInsert = new string[] { "CD_COMPANY", "NO_TO", "NO_INV", "NO_BL", "DT_DECLARE", "DTS_INSERT", "ID_INSERT", "DTS_UPDATE", "ID_UPDATE" };
                si2.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_TO", "NO_INV", "NO_BL", "DT_DECLARE", "DTS_INSERT", "ID_INSERT", "DTS_UPDATE", "ID_UPDATE" };
                sc.Add(si2);
            }

            ResultData[] result = (ResultData[])Global.MainFrame.Save(sc);

            for (int i = 0; i < result.Length; i++)
                if (!result[i].Result) return false;

            return true;
        }


        internal DataTable SearchToBF(string 통관번호)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_TR_EXTO_COSTBF_SELECT";
            si.SpParamsSelect = new object[] { 회사코드, 통관번호 };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            return (DataTable)result.DataValue;
        }

        internal DataTable 송장번호Search(string Multi참조송장번호)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_TR_EXTO_INVOICE_SELECT";
            si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, Multi참조송장번호 };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;

            return dt;
        }
    }
}

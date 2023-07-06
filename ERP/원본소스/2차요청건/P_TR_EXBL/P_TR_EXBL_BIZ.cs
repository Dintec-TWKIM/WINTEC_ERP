using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using System.Data;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace trade
{
    internal class P_TR_EXBL_BIZ
    {
        private string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;

        public DataTable Search(string 선적번호)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_TR_EXBL_SELECT";
            si.SpParamsSelect = new object[] { 회사코드, 선적번호 };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;
            

            foreach (DataColumn Col in dt.Columns)
            {
                if (Col.DataType == Type.GetType("System.Decimal"))
                    Col.DefaultValue = 0;
            }

            dt.Columns["NO_EMP"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            dt.Columns["NM_KOR"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            dt.Columns["CD_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaCode;
            dt.Columns["NM_BIZAREA"].DefaultValue = Global.MainFrame.LoginInfo.BizAreaName;
            //dt.Columns["CD_EXPORT"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeNo;
            //dt.Columns["NM_EXPORT"].DefaultValue = Global.MainFrame.LoginInfo.EmployeeName;
            dt.Columns["DT_BALLOT"].DefaultValue = Global.MainFrame.GetStringToday;
            dt.Columns["DT_LOADING"].DefaultValue = Global.MainFrame.GetStringToday;
            dt.Columns["DT_ARRIVAL"].DefaultValue = Global.MainFrame.GetStringToday;
            dt.Columns["DT_PAYABLE"].DefaultValue = Global.MainFrame.GetStringToday;

            dt.Columns["YN_RETURN"].DefaultValue = "N";

            dt.Columns["FG_BL"].DefaultValue = "001";

            return dt;
        }

        public DataTable 송장번호Search(string Multi송장번호)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_TR_EXBL_INV_SELECT";
            si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, Multi송장번호 };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            DataTable dt = (DataTable)result.DataValue;

            return dt;
        }  

        public bool Delete(string 선적번호)
        {
            SpInfoCollection sc = new SpInfoCollection();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("NO_BL"));
            DataRow dr = dt.NewRow();
            dr["NO_BL"] = 선적번호;
            dt.Rows.Add(dr);
            dt.AcceptChanges();

            //MM_QTIO에 있는 QT_CLS, AM_CLS, QT_CLS_MM의 값을 0으로 초기화해준다.
            SpInfo si2 = new SpInfo();
            si2.DataValue = dt;
            si2.DataState = DataValueState.Deleted;
            si2.CompanyID = 회사코드;
            si2.SpNameDelete = "UP_TR_EXBL_QTIO_DELETE_UPDATE";
            si2.SpParamsDelete = new string[] { "CD_COMPANY", "NO_BL" };
            //si2.SpParamsDelete = new string[] { 회사코드, 선적번호 };
            sc.Add(si2);

            SpInfo si1 = new SpInfo();
            si1.DataValue = dt;
            si1.DataState = DataValueState.Deleted;
            si1.CompanyID = 회사코드;
            si1.SpNameDelete = "UP_TR_EXBL_DELETE";
            si1.SpParamsDelete = new string[] { "CD_COMPANY", "NO_BL" };
            //si1.SpParamsDelete = new string[] { 회사코드, 선적번호 };
            sc.Add(si1);
            
            ResultData[] result = (ResultData[])Global.MainFrame.Save(sc);

            for (int i = 0; i < result.Length; i++)
                if (!result[i].Result) return false;

            return true;
        }

        public bool Save(DataTable dt, DataTable dtLine)
        {
            SpInfoCollection sc = new SpInfoCollection();

            //선적등록 헤더
            if (dt != null)
            {
                SpInfo si1 = new SpInfo();
                si1.DataValue = dt;
                si1.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si1.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si1.SpNameInsert = "UP_TR_EXBL_INSERT";			// Insert 프로시저명
                si1.SpNameUpdate = "UP_TR_EXBL_UPDATE";			// Update 프로시저명
                si1.SpParamsInsert = new string[] { "NO_BL", "CD_COMPANY", "NO_TO", "NO_INV", "CD_BIZAREA", "CD_SALEGRP", "NO_EMP", "CD_PARTNER", "DT_BALLOT", "CD_EXCH", "RT_EXCH", "AM_EX", "AM", "AM_EXNEGO", "AM_NEGO", "YN_SLIP", "NO_SLIP", "CD_EXPORT", "DT_LOADING", "DT_ARRIVAL", "SHIP_CORP", "NM_VESSEL", "PORT_LOADING", "PORT_NATION", "PORT_ARRIVER", "COND_SHIPMENT", "FG_BL", "FG_LC", "COND_PAY", "COND_DAYS", "DT_PAYABLE", "REMARK1", "REMARK2", "REMARK3", "NO_MNG", "ID_INSERT", "YN_RETURN" };
                si1.SpParamsUpdate = new string[] { "NO_BL", "CD_COMPANY", "NO_TO", "NO_INV", "CD_BIZAREA", "CD_SALEGRP", "NO_EMP", "CD_PARTNER", "DT_BALLOT", "CD_EXCH", "RT_EXCH", "AM_EX", "AM", "AM_EXNEGO", "AM_NEGO", "YN_SLIP", "NO_SLIP", "CD_EXPORT", "DT_LOADING", "DT_ARRIVAL", "SHIP_CORP", "NM_VESSEL", "PORT_LOADING", "PORT_NATION", "PORT_ARRIVER", "COND_SHIPMENT", "FG_BL", "FG_LC", "COND_PAY", "COND_DAYS", "DT_PAYABLE", "REMARK1", "REMARK2", "REMARK3", "NO_MNG", "ID_UPDATE", "YN_RETURN" };
                sc.Add(si1);
            }

            //선적등록 라인
            if (dtLine != null)
            {
                SpInfo si2 = new SpInfo();
                si2.DataValue = dtLine;
                si2.CompanyID = Global.MainFrame.LoginInfo.CompanyCode;
                si2.UserID = Global.MainFrame.LoginInfo.EmployeeNo;
                si2.SpNameInsert = "UP_TR_EXBL_LINE_INSERT";			// Insert 프로시저명
                si2.SpNameUpdate = "UP_TR_EXBL_LINE_INSERT";			// Update 프로시저명
                si2.SpParamsInsert = new string[] { "CD_COMPANY", "NO_BL", "NO_TO", "NO_INV", "DTS_INSERT", "ID_INSERT", "DTS_UPDATE", "ID_UPDATE" };
                si2.SpParamsUpdate = new string[] { "CD_COMPANY", "NO_BL", "NO_TO", "NO_INV", "DTS_INSERT", "ID_INSERT", "DTS_UPDATE", "ID_UPDATE" };
                sc.Add(si2);
            }

            ResultData[] result = (ResultData[])Global.MainFrame.Save(sc);

            for (int i = 0; i < result.Length; i++)
                if (!result[i].Result) return false;

            return true;
        }

        public bool 미결전표처리(string 선적번호)
        {
            ResultData result;

            switch (Global.MainFrame.ServerKeyCommon.ToUpper())
            {
                case "KORAVL":
                    result = (ResultData)Global.MainFrame.ExecSp("UP_TR_EXBL_DOCU_KORAVL", new object[] { 회사코드, 선적번호 });
                    break;
                case "SEEGENE":
                    result = (ResultData)Global.MainFrame.ExecSp("UP_TR_EXBL_DOCU_SEEGENE", new object[] { 회사코드, 선적번호 });
                    break;
                case "NEOPH":
                    result = (ResultData)Global.MainFrame.ExecSp("UP_TR_EXBL_DOCU_NEOPH", new object[] { 회사코드, 선적번호 });
                    break;
                default:
                    result = (ResultData)Global.MainFrame.ExecSp("UP_TR_EXBL_DOCU", new object[] { 회사코드, 선적번호 });
                    break;
            }

            return result.Result;
        }

        public bool 미결전표취소(string 선적번호)
        {
            ResultData result = (ResultData)Global.MainFrame.ExecSp("UP_FI_DOCU_AUTODEL", new object[] { Global.MainFrame.LoginInfo.CompanyCode, "170", 선적번호, Global.MainFrame.LoginInfo.EmployeeNo });
            return result.Result;
        }

        internal DataTable SearchBlBF(string 선적번호)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_TR_EXBL_COSTBF_SELECT";
            si.SpParamsSelect = new object[] { 회사코드, 선적번호 };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            return (DataTable)result.DataValue;
        }

        //원화금액 보정값을 얻어온다.
        public DataSet 보정값Search(string _선적번호)
        {
            ResultData result = (ResultData)Global.MainFrame.FillDataSet("UP_TR_EXBL_QTIO_SELECT", new object[] { Global.MainFrame.LoginInfo.CompanyCode, _선적번호 });
            DataSet ds = (DataSet)result.DataValue;

            return ds;
        }

        //원화금액 MM_QTIO에 업데이트 한다.
        //NO_QTIO, NO_LINE_QTIO, AM_EXSO, QT_INVENT, UM_INVENT, QT_SO
        public void 보정값Update(string 출하번호, string 출하항번, decimal 보정값, decimal QT_INVENT, decimal UM_INVENT, decimal QT_SO )
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_TR_EXBL_QTIO_UPDATE";
            si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, 출하번호, 출하항번, 보정값, QT_INVENT, UM_INVENT, QT_SO };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
        }

        //DELETE에서 한 트랙잭션으로 처리하므로 삭제
        //public void 보정값초기화Update(string 출하번호)
        //{
        //    SpInfo si = new SpInfo();
        //    si.SpNameSelect = "UP_TR_EXBL_QTIO_DELETE_UPDATE";
        //    si.SpParamsSelect = new object[] { Global.MainFrame.LoginInfo.CompanyCode, 출하번호 };
        //    ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
        //}

        internal DataRow SearchSoh(string noInv)
        {
            string sql = "SELECT  INVL.NO_PO NO_SO, SOH.COND_PAY, SOH.COND_DAYS " +
                         "FROM    TR_INVL INVL " +
                         "        LEFT OUTER JOIN SA_SOH SOH ON INVL.CD_COMPANY = SOH.CD_COMPANY AND INVL.NO_PO = SOH.NO_SO " +
                         "WHERE   INVL.CD_COMPANY = '" + MA.Login.회사코드 + "'" +
                         "AND     INVL.NO_INV = '" + noInv + "' " +
                         "ORDER BY INVL.NO_PO";

            DataTable dt = DBHelper.GetDataTable(sql);
            if (dt == null || dt.Rows.Count == 0) return null;
            return dt.Rows[0];
        }
    }
}

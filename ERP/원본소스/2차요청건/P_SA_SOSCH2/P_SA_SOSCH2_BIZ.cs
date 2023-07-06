using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace sale
{
    class P_SA_SOSCH2_BIZ
    {
        string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;

        public DataTable Search(string 수주일자FROM, string 수주일자TO, string 사업장, string 영업그룹, string 거래구분, string 수주상태, string 진행상태, string 수주형태, string 탭구분, string 거래처, string 수주담당자, string 창고, string 제품군, string  거래처PO번호)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_SOSCH2_SELECT";
            si.SpParamsSelect = new Object[] { 회사코드, 수주일자FROM, 수주일자TO, 사업장, 영업그룹, 거래구분, 수주상태, 진행상태, 수주형태, 탭구분, 거래처, 수주담당자, 창고, 제품군 ,거래처PO번호 };
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);
            return (DataTable)result.DataValue;
        }

        public DataTable SearchDetail(string KEY, string 수주일자FROM, string 수주일자TO, string 사업장, string 영업그룹, string 거래구분, string 단위선택, string 수주상태, string 진행상태, string 수주형태, string 탭구분, string 창고, string 거래처, string 제품군,string 거래처PO번호, string MRP창고)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_SOSCH2_SELECT1";
            si.SpParamsSelect = new Object[] { 회사코드, KEY, 수주일자FROM, 수주일자TO, 사업장, 영업그룹, 거래구분, 단위선택, 수주상태, 진행상태, 수주형태, 탭구분, 창고, 거래처, 제품군 ,거래처PO번호, MRP창고};
            ResultData result = (ResultData)Global.MainFrame.FillDataTable(si);

            return (DataTable)result.DataValue;
        }

        public DataTable 출력(string 수주일자fr, string 수주일자to, string 사업장, string 영업그룹, string 거래구분, string 수주상태, string 진행상태, string 단위선택, string 수주형태, string 탭, string PK번호, string 창고, string 거래처, string 제품군 , string 거래처PO번호)
        {
            SpInfo si = new SpInfo();

            si.SpNameSelect = "UP_SA_SOSCH2_SELECT_PRINT";
            si.SpParamsSelect = new Object[] { 회사코드, 수주일자fr, 수주일자to, 사업장, 영업그룹, 거래구분, 수주상태, 진행상태, 단위선택, 수주형태, 탭, PK번호, 창고, 거래처, 제품군, 거래처PO번호 };
            ResultData resultdata = ( ResultData )Global.MainFrame.FillDataTable( si );
            return ( DataTable )resultdata.DataValue;
        }

        public DataTable PageDefaultSetting(object[] obj)
        {
            DataTable dt = DBHelper.GetDataTable("UP_SA_SOSCH2_OPEN", obj);
            return dt;
        }

        internal DataTable SearchWINPLUS(string KEY, string 수주일자FROM, string 수주일자TO, string 사업장, string 영업그룹, string 거래구분, string 단위선택, string 수주상태, string 진행상태, string 수주형태, string 탭구분, string 창고, string 거래처, string 제품군, string 거래처PO번호, string MRP창고)
        {
            return DBHelper.GetDataTable("UP_SA_Z_WINPLUS_SOSCH2_S", new Object[] { 회사코드, KEY, 수주일자FROM, 수주일자TO, 사업장, 영업그룹, 거래구분, 단위선택, 수주상태, 진행상태, 수주형태, 탭구분, 창고, 거래처, 제품군, 거래처PO번호, MRP창고 });
        }
    }
}

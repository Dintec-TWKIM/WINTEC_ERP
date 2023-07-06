using System;
using System.Data;
using Duzon.Common.Forms;
using Duzon.Common.Util;


namespace sale
{
    class P_SA_GIRSCH_BIZ
    {
        string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;

        public DataTable Search_req(string CD_COMPANY, string DtGirStart, string DtGirEnd, string BizArea, string TpBusi, string PlantGir, string Nm_Sl, string StaGir, string FgGi, string SlUnit, string TP_SO, string tabC, string 운송수단, string PK번호, string 출고검사요청, string 영업조직s, string 영업그룹s, string 의뢰일자체크, string 납기일자체크, string 납기일자FROM, string 납기일자TO)
        {
            SpInfo si = new SpInfo();

            si.SpNameSelect = "UP_SA_GIRSCH_SELECT_PRINT";
            si.SpParamsSelect = new Object[] { CD_COMPANY, DtGirStart, DtGirEnd, BizArea, TpBusi, PlantGir, Nm_Sl, StaGir, FgGi, SlUnit, TP_SO, tabC, 운송수단, PK번호, 출고검사요청, 영업조직s, 영업그룹s, 의뢰일자체크 , 납기일자체크, 납기일자FROM, 납기일자TO };
            ResultData resultdata = ( ResultData )Global.MainFrame.FillDataTable( si );
            return ( DataTable )resultdata.DataValue;
        }

        public DataTable Search(string 의뢰일자FROM, string 의뢰일자TO, string 사업장, string 거래구분, string 공장, string 창고, string 처리상태, string 출하구분, string 단위선택, string 수주형태, string 탭구분, string 운송방법, string 계정구분, string 의뢰담당자, string 거래처그룹, string 거래처, string 영업조직s, string 영업그룹s, string 의뢰일자체크, string 납기일자체크, string 납기일자FROM, string 납기일자TO, string 수주상태)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_GIRSCH_SELECT_H";
            si.SpParamsSelect = new Object[] { 회사코드, 의뢰일자FROM, 의뢰일자TO, 사업장, 거래구분, 공장, 창고, 처리상태, 출하구분, 단위선택, 수주형태, 탭구분, 운송방법, 계정구분, 의뢰담당자, 거래처그룹, 거래처, 영업조직s, 영업그룹s, 의뢰일자체크, 납기일자체크, 납기일자FROM, 납기일자TO, Global.MainFrame.LoginInfo.EmployeeNo, 수주상태 }; // 2013.02.07 최창종 : 권한 추가
            ResultData result = ( ResultData )Global.MainFrame.FillDataTable( si );
            return ( DataTable )result.DataValue;
        }

        public DataTable SearchDetail(string KEY, string 의뢰일자FROM, string 의뢰일자TO, string 사업장, string 거래구분, string 공장, string 창고, string 처리상태, string 출하구분, string 단위선택, string 수주형태, string 탭구분, string 운송방법, string 계정구분, string 의뢰담당자, string 거래처그룹, string 거래처, string 영업조직s, string 영업그룹s, string 의뢰일자체크, string 납기일자체크, string 납기일자FROM, string 납기일자TO)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_GIRSCH_SELECT_L";
            si.SpParamsSelect = new Object[] { 회사코드, KEY, 의뢰일자FROM, 의뢰일자TO, 사업장, 거래구분, 공장, 창고, 처리상태, 출하구분, 단위선택, 수주형태, 탭구분, 운송방법, 계정구분, 의뢰담당자, 거래처그룹, 거래처, 영업조직s, 영업그룹s, 의뢰일자체크, 납기일자체크, 납기일자FROM, 납기일자TO }; 
            ResultData result = ( ResultData )Global.MainFrame.FillDataTable( si );

            return ( DataTable )result.DataValue;
        }
    }
}

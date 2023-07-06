using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using System.Data;

namespace sale
{
    class P_SA_GIRR_REG_SUB_BIZ
    {
        string 회사코드 = Global.MainFrame.LoginInfo.CompanyCode;

        public DataTable Search(string 수불일자FROM, string 수불일자TO, string 공장, string 영업그룹, string 거래처, string 과세구분, string 창고, string 출하형태, string 담당자, string 프로젝트, string 멀티수주번호, string 수주등록출하적용여부, string 수주담당자, string 유무환구분, string str품목)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_GIRR_REG_SUB_SELECT";
            si.SpParamsSelect = new Object[] { 회사코드, 수불일자FROM, 수불일자TO, 공장, 영업그룹, 거래처, 과세구분, 창고, 출하형태, 담당자, 프로젝트, 멀티수주번호, 수주등록출하적용여부, 수주담당자, 유무환구분, str품목 };
            ResultData resultdata = (ResultData)Global.MainFrame.FillDataTable(si);
            return (DataTable)resultdata.DataValue;
        }

        public DataTable SearchDetail(string 수불번호, string 영업그룹, string 거래처, string 과세구분, string 창고, string 출하형태, string 담당자, string 프로젝트, string 멀티수주번호, string 수주등록출하적용여부, string 수주담당자, string 유무환구분, string str품목)
        {
            SpInfo si = new SpInfo();
            si.SpNameSelect = "UP_SA_GIRR_REG_SUB_SELECT1";
            si.SpParamsSelect = new Object[] { 회사코드, 수불번호, 영업그룹, 거래처, 과세구분, 창고, 출하형태, 담당자, 프로젝트, 멀티수주번호, 수주등록출하적용여부, 수주담당자, 유무환구분, str품목 };
            ResultData resultdata = (ResultData)Global.MainFrame.FillDataTable(si);
            return (DataTable)resultdata.DataValue;
        }

        #region 프로젝트 명 조회
        public DataSet ProjectSearch(object[] obj)
        {
            string SelectQuery = " SELECT  PH.NO_PROJECT, PH.NM_PROJECT, PH.CD_PARTNER, MP.LN_PARTNER, PH.CD_SALEGRP, MS.NM_SALEGRP " +
                                 "   FROM  SA_PROJECTH PH " +
                                 "   LEFT  JOIN MA_PARTNER MP ON PH.CD_COMPANY = MP.CD_COMPANY AND PH.CD_PARTNER = MP.CD_PARTNER " +
                                 "   LEFT  JOIN MA_SALEGRP MS ON PH.CD_COMPANY = MS.CD_COMPANY AND PH.CD_SALEGRP = MS.CD_SALEGRP " +
                                 "  WHERE  PH.CD_COMPANY = '" + obj[0].ToString() + "'  " +
                                 "    AND (PH.NO_PROJECT  = '" + obj[1].ToString() + "' OR '" + obj[1].ToString() + "' = '' OR '" + obj[1].ToString() + "' IS NULL OR PH.NO_PROJECT LIKE '%'+ '" + obj[1].ToString() + "' +'%'   " +
                                 "     OR  PH.NM_PROJECT  = '" + obj[1].ToString() + "' OR '" + obj[1].ToString() + "' = '' OR '" + obj[1].ToString() + "' IS NULL OR PH.NM_PROJECT LIKE '%'+ '" + obj[1].ToString() + "' +'%')   " +
                                 "  ORDER  BY PH.NO_PROJECT ";

            DataSet ds = Global.MainFrame.FillDataSet(SelectQuery);

            return ds;
        }
        #endregion
    }
}

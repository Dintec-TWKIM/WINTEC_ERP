using System;
using System.Collections.Generic;
using System.Text;
using Duzon.ERPU;
using System.Data;
using Duzon.Common.Forms;
using Duzon.ERPU.PU.Common;
using Duzon.ERPU.PU;

namespace pur
{
    class P_PU_PO_REG2_GW_L
    {
        public DataTable getGwSearch(string str, DataTable dt, out string URL)
        {
            URL = "";
            DataTable dtGW = null;

            if (dt == null || dt.Rows.Count == 0)
                return null;

            string HTML_FILE_NAME = string.Empty;
            string App_Form_Kind = string.Empty;

            switch (str)    //시스템 통제설정에 따라 업체마다 파일 이름과 문서번호와 업체 URL 경로를 셋팅한다.
            {

                case "026": //구일엔지니어링
                    App_Form_Kind = "LGDISP2_1000";
                    URL = "http://61.106.24.20/KOR_WEBROOT/SRC/CM/TIMS/ICD_TIMS_0100100.aspx?";
                    break;
                default:
                    return null;
            }

            URL += "cd_company=" + Duzon.Common.Forms.Global.MainFrame.LoginInfo.CompanyCode + "&no_docu=" + System.Web.HttpUtility.UrlEncode(D.GetString(dt.Rows[0]["NO_PO"]), Encoding.UTF8) + "&login_id=" + Global.MainFrame.LoginInfo.EmployeeNo + "&cd_pc=" + "" + "&erp_div=U&app_form_kind=" + App_Form_Kind + "&doc_id=";

            //strE[0] = 전표번호컬럼이름, strE[1] = 항번컬럼이름, strE[2] = 테이블, strE[3] = 담당자 , strE[4] = Formkind, strE[5] = 모듈  
            switch (str)   
            {
                case "026":
                    string sum_qt_po_mm = D.GetDecimal(dt.Compute("SUM(QT_PO_MM)", "")).ToString("#,###,###,###.###");
                    string sum_am_ex = D.GetDecimal(dt.Compute("SUM(AM_EX)", "")).ToString("#,###,###,###.###");
                    string dt_po = string.Empty;

                    if(D.GetString(dt.Rows[0]["DT_PO"]) != string.Empty)
                        dt_po = D.GetString(dt.Rows[0]["DT_PO"]).Substring(0, 4) + "/" + D.GetString(dt.Rows[0]["DT_PO"]).Substring(4, 2) + "/" + D.GetString(dt.Rows[0]["DT_PO"]).Substring(6, 2);


                    string[] tempH = new string[] {"발주번호", D.GetString(dt.Rows[0]["NO_PO"]),"발주일자",dt_po,"업체명",D.GetString(dt.Rows[0]["LN_PARTNER"]),
                                                   "사업자번호",D.GetString(dt.Rows[0]["BIZ_NUM1"]),"경북 구미시 산동면 산호대로 1105-19","대표자",D.GetString(dt.Rows[0]["NM_CEO1"]),D.GetString(dt.Rows[0]["NO_TEL_PUR"]),
                                                   "주소",D.GetString(dt.Rows[0]["ADS1"])+" "+D.GetString(dt.Rows[0]["ADS_DETAIL1"]),D.GetString(dt.Rows[0]["NO_FAX_PUR"]),"","",D.GetString(dt.Rows[0]["NO_TEL_EMER"]),"TEL",D.GetString(dt.Rows[0]["NO_TEL1"]),
                                                   D.GetString(dt.Rows[0]["NO_EMAIL_EMP"]),
                                                   "FAX",D.GetString(dt.Rows[0]["NO_FAX1"]),D.GetString(dt.Rows[0]["NM_PTR"]),"H/P",D.GetString(dt.Rows[0]["NO_HPEMP_PARTNER"]),"","",
                                                   "담당자",D.GetString(dt.Rows[0]["NM_PTR1"]),"","",
                                                   "통화단위",D.GetString(dt.Rows[0]["NM_EXCH"]),"No","품목코드","품명","규격","단위","수량","단가","금액","납기일","프로젝트"};
                    string[] tempM = new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_IM", "@@통화포맷|#,###,###,###.###|QT_PO_MM", "@@통화포맷|#,###,###,###.###|UM_EX_PO", "@@통화포맷|#,###,###,###.###|AM_EX", "@@날짜포맷|-|DT_LIMIT", "NM_PROJECT"};
                    string[] tempD = new string[] { "합계", sum_qt_po_mm, "", sum_am_ex, "", "", D.GetString(dt.Rows[0]["DC50_PO"]) ,"정기결제 (마감 후 익월말)","30일 전자채권"};
                    string[] tmepE = new string[] { "NO_PO", "NO_LINE", "PU_POL", D.GetString(dt.Rows[0]["NO_EMP"]),App_Form_Kind,"PU" };

                    dtGW = SETGWDATA.GW_ICD_html(dt, tempH, tempM, tempD, tmepE, true);

                    break;
                default:
                    return null;
            }

            return dtGW;
        }
    }
}

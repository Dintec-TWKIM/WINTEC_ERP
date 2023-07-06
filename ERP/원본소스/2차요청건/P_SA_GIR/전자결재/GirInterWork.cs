using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using System.Data;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

namespace sale
{
    internal class GirInterWork
    {
        string html = string.Empty;

        internal bool 전자결재(DataRow rowH, DataTable dtL)
        {
            bool isSuccess = true;
            string strURL = string.Empty;

            List<object> List = new List<object>();
            List.Add(MA.Login.회사코드);
            List.Add(Global.MainFrame.LoginInfo.CdPc);
            List.Add(D.GetString(rowH["NO_GIR"]));
            List.Add(MA.Login.사원번호);
            List.Add(Global.MainFrame.GetStringToday);
            List.Add(string.Empty);
            List.Add("출하요청서");
            List.Add("Y");
            List.Add(3000); //APP_FORM_KIND에 들어갈 값
            List.Add("29991231");

            switch (Global.MainFrame.ServerKeyCommon.ToUpper())
            {
                case "DZSQL":       //84번 개발서버
                    List[5] = GetYPPHtml(rowH, dtL);
                    isSuccess = 결재상신_개발서버(List.ToArray());
                    break;
                case "YPP":         //와이피피
                    List[5] = GetYPPHtml(rowH, dtL);
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    strURL = "http://gw.yppdt.com/kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(D.GetString(rowH["NO_GIR"]), Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;
                case "ENTEC":      //인텍전기전자
                    List[5] = GetENTECHtml(rowH, dtL);
                    List[6] = "출하의뢰서";
                    List[8] = 1002;
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    strURL = "http://gw.entecene.co.kr/kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(D.GetString(rowH["NO_GIR"]), Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;
                default:
                    break;
            }

            return isSuccess;
        }

        #region -> 와이피피 HTML양식

        private string GetYPPHtml(DataRow rowH, DataTable dtL)
        {
            string body = string.Empty;
            string line = string.Empty;
            int number = 0;

            body = @"
<head>
<meta http-equiv='Content-Language' content='ko'>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
</head>
<center>
<body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0'>
<table width='645' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: collapse; border: 0;'>
<colgroup width='17%' align='center'></colgroup>
<colgroup width='33%' align='center'></colgroup>
<colgroup width='17%' align='center'></colgroup>
<colgroup width='33%' align='center'></colgroup>
<tr height='30'>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>문 서 번 호</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@문서번호</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>거&nbsp; 래&nbsp; 처</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@거래처</td>
</tr>
<tr height='30'>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>수 주 일 자</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수주일자</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>거래처 PO번호</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@PO번호</td>
</tr>
<tr height='30'>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>영업팀/분야</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@영업그룹</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>프로젝트번호</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@프로젝트번호</td>
</tr>
<tr height='30'>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>영업담당자</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@담당자</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>프로젝트명</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@프로젝트명</td>
</tr>
<tr height='5'>
<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
</tr>
<tr height='30'>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>납품장소/납품처</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@납품장소</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>출하일시</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@출하일시</td>
</tr>
<tr height='30'>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>납품시첨부서류/<br>특기사항</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@특기사항</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>운송종류</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@운송종류</td>
</tr>
<tr height='30'>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>인수담당자</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@인수담당자</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>전화번호</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@전화번호</td>
</tr>
</tr>
</table>
<table width='645' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: collapse; border: 0;'>
<colgroup width='5%' align='center'></colgroup>
<colgroup width='22%' align='center'></colgroup>
<colgroup width='23%' align='center'></colgroup>
<colgroup width='10%' align='center'></colgroup>
<colgroup width='12%' align='center'></colgroup>
<colgroup width='10%' align='center'></colgroup>
<colgroup width='18%' align='center'></colgroup>
<tr height='5'>
<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
</tr>
<tr height='26'>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='7' align='left' bgcolor='#DBE5F1'>&nbsp; ▶ 출하목록</td>
</tr>
<tr height='26'>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>NO.</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>품목</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>상세사양</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>수량</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>단가</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>검사유무</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>비고</td>
</tr>
@@라인추가
</table>
</body>
</center>";

            DataRow rowSoInfo = GetSoInfo(D.GetString(dtL.Rows[0]["NO_SO"]));

            body = body.Replace("@@문서번호", D.GetString(rowH["NO_GIR"]));
            body = body.Replace("@@거래처", D.GetString(rowH["LN_PARTNER"]));
            body = body.Replace("@@수주일자", D.GetString(rowSoInfo["DT_SO"]).Substring(0, 4) + "." + D.GetString(rowSoInfo["DT_SO"]).Substring(4, 2) + "." + D.GetString(rowSoInfo["DT_SO"]).Substring(6, 2));
            body = body.Replace("@@PO번호", D.GetString(rowSoInfo["NO_PO_PARTNER"]));
            body = body.Replace("@@영업그룹", D.GetString(BASIC.GetSaleGrp(D.GetString(dtL.Rows[0]["CD_SALEGRP"]))["NM_SALEORG"]) + " / " + D.GetString(dtL.Rows[0]["NM_SALEGRP"]));
            body = body.Replace("@@담당자", D.GetString(rowSoInfo["NM_KOR"]));
            body = body.Replace("@@프로젝트번호", D.GetString(dtL.Rows[0]["NO_PROJECT"]));
            body = body.Replace("@@프로젝트명", D.GetString(dtL.Rows[0]["NM_PROJECT"]));

            body = body.Replace("@@납품장소", D.GetString(rowH["TXT_USERDEF1"]));
            body = body.Replace("@@출하일시", D.GetString(dtL.Rows[0]["DT_REQGI"]).Substring(0, 4) + "." + D.GetString(dtL.Rows[0]["DT_REQGI"]).Substring(4, 2) + "." + D.GetString(dtL.Rows[0]["DT_REQGI"]).Substring(6, 2));
            body = body.Replace("@@특기사항", D.GetString(rowH["TXT_USERDEF2"]));
            body = body.Replace("@@운송종류", D.GetString(rowSoInfo["NM_TRANSPORT"]));
            body = body.Replace("@@인수담당자", D.GetString(rowH["TXT_USERDEF3"]));
            body = body.Replace("@@전화번호", D.GetString(rowH["TXT_USERDEF4"]));

            foreach (DataRow rowL in dtL.Rows)
            {
                string tr = string.Empty;

                tr = GetLineYPP();
                tr = tr.Replace("@@번호", D.GetString(++number));
                tr = tr.Replace("@@품목", D.GetString(rowL["CD_ITEM"]));
                tr = tr.Replace("@@상세사양", D.GetString(rowL["STND_ITEM"]));
                tr = tr.Replace("@@수량", D.GetDecimal(rowL["QT_GIR"]).ToString("###,###,##0.####"));
                tr = tr.Replace("@@단가", D.GetDecimal(rowL["UM"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@검사유무", D.GetString(rowL["YN_INSPECT"]) == "Y" ? "Y" : "N");
                tr = tr.Replace("@@비고", D.GetString(rowL["DC_RMK"]));
                line += tr;
            }

            body = body.Replace("@@라인추가", line);

            return body;
        }

        private string GetLineYPP()
        {
            string tr = @"
<tr height='26'>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@번호</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@품목</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@상세사양</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수량</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'><p align='right'>@@단가&nbsp;&nbsp;</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@검사유무</td>
<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@비고</td>
</tr>";

            return tr;
        }

        #endregion

        #region -> 인텍전기전자 HTML양식
        private object GetENTECHtml(DataRow rowH, DataTable dtL)
        {
            string body = string.Empty;
            string line = string.Empty;
            int number = 0;

            body = @"
<head>
<meta http-equiv='Content-Language' content='ko'>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
</head>

<body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0'>
<center>
	<table width='100%' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size=10pt; border-collapse: collapse; border: 0;'>
		<colgroup width='5%' align='center'></colgroup>
		<colgroup width='10%' align='center'></colgroup>
		<colgroup width='9%' align='center'></colgroup>
		<colgroup width='21%' align='center'></colgroup>
		<colgroup width='21%' align='center'></colgroup>
		<colgroup width='5%' align='center'></colgroup>
		<colgroup width='9%' align='center'></colgroup>
		<colgroup width='10%' align='center'></colgroup>
		<colgroup width='10%' align='center'></colgroup>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 1 0; border-color: #000000' colspan='3' align='left'>&nbsp;의뢰 NO. @@의뢰번호</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		<tr height='5'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>순번</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>Project<br>번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>출하<br>근거</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>제 품 명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>규 격</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>출하<br>의뢰일</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>거래처</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>제조번호</td>
		</tr>

@@라인추가

<tr height='50'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'>특기사항</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='7'></td>
		</tr>
		
		<tr height='10'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='9'>위와 같이 출하를 의뢰합니다.</td>
		</tr>

</table>
</body>
</center>";

            body = body.Replace("@@의뢰번호", D.GetString(rowH["NO_GIR"]));

            foreach (DataRow rowL in dtL.Rows)
            {
                string tr = string.Empty;

                tr = GetLineENTEC();
                tr = tr.Replace("@@NO", D.GetString(++number));
                tr = tr.Replace("@@프로젝트명", D.GetString(rowL["NM_PROJECT"]));
                tr = tr.Replace("@@품목명", D.GetString(rowL["NM_ITEM"]));
                tr = tr.Replace("@@규격", D.GetString(rowL["STND_ITEM"]));
                tr = tr.Replace("@@의뢰수량", D.GetDecimal(rowL["QT_GIR"]).ToString("###,###,##0.####"));
                tr = tr.Replace("@@출하예정일", D.GetString(rowL["DT_REQGI"]).Substring(0, 4) + "." + D.GetString(rowL["DT_REQGI"]).Substring(4, 2) + "." + D.GetString(rowL["DT_REQGI"]).Substring(6, 2));
                tr = tr.Replace("@@납품처명", D.GetString(rowL["LN_PARTNER"]));
                line += tr;
            }

            body = body.Replace("@@라인추가", line);

            return body;
        }

        private string GetLineENTEC()
        {
            string tr = @"
<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@NO</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@프로젝트명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>납품</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@품목명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@규격</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@의뢰수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@출하예정일</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@납품처명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
		</tr>";

            return tr;
        } 
        #endregion

        #region ♣ 전자결재

        #region -> 개발서버용 결재상신(개발서버에서 TEST용)

        bool 결재상신_개발서버(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("UP_SA_GWDOCU_DUZON", obj);
        }

        #endregion

        #region -> 실제 업체사용 결재상신

        bool 결재상신_업체(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("UP_SA_GWDOCU", obj);
        }

        #endregion

        #endregion

        internal DataRow GetSoInfo(string noSo)
        {
            string sqlQuery = " SELECT  DT_SO, NO_PO_PARTNER, NM_KOR, C.NM_SYSDEF AS NM_TRANSPORT "
                            + " FROM    SA_SOH H "
                            + "         LEFT OUTER JOIN MA_EMP E ON H.CD_COMPANY = E.CD_COMPANY AND H.NO_EMP = E.NO_EMP"
                            + "         LEFT OUTER JOIN MA_CODEDTL C ON H.CD_COMPANY = C.CD_COMPANY AND C.CD_FIELD = 'TR_IM00008' AND H.FG_TRANSPORT = C.CD_SYSDEF"
                            + " WHERE   H.CD_COMPANY = '" + MA.Login.회사코드 + "'"
                            + " AND     H.NO_SO = '" + noSo + "'";

            DataTable dt = DBHelper.GetDataTable(sqlQuery);
            return dt.Rows[0];
        }
    }
}
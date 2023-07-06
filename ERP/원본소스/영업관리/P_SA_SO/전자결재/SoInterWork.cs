using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;
using System.Data;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA.Common;

namespace sale
{
    internal class SoInterWork
    {
        string html = string.Empty;

        internal bool 전자결재(DataRow rowH, DataTable dtL)
        {
            bool isSuccess = true;
            string strURL = string.Empty;

            List<object> List = new List<object>();
            List.Add(MA.Login.회사코드);
            List.Add(Global.MainFrame.LoginInfo.CdPc);
            List.Add(D.GetString(rowH["TXT_USERDEF1"]) == string.Empty ? D.GetString(rowH["NO_SO"]) : D.GetString(rowH["TXT_USERDEF1"]));
            List.Add(MA.Login.사원번호);
            List.Add(Global.MainFrame.GetStringToday);
            List.Add(string.Empty);
            List.Add("수주품의서");
            List.Add("Y");
            List.Add(2000);     //APP_FORM_KIND에 들어갈 값
            List.Add("29991231");

            switch (Global.MainFrame.ServerKeyCommon.ToUpper())
            {
                case "DZSQL":       //84번 개발서버
                    List[5] = GetLEDLITEKHtml(rowH, dtL);
                    isSuccess = 결재상신_개발서버(List.ToArray());
                    break;
                case "FORTIS":      //포티스
                    List[5] = GetFortisHtml(rowH, dtL);
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    strURL = "http://112.216.190.50:8088/kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(D.GetString(rowH["NO_SO"]), Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;
                case "YPP":         //와이피피
                    List[5] = GetYPPHtml(rowH, dtL);
                    List[6] = "수주통보서";
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    strURL = "http://gw.yppdt.com/kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(D.GetString(rowH["TXT_USERDEF1"]) == string.Empty ? D.GetString(rowH["NO_SO"]) : D.GetString(rowH["TXT_USERDEF1"]), Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;
                case "DYPC":        //동양이화
                    List[5] = GetDYPCHtml(rowH, dtL);
                    List[6] = D.GetString(rowH["NO_SO"]) + "_" + D.GetString(rowH["LN_PARTNER"]) + "_" + D.GetString(rowH["NO_PO_PARTNER"]);
                    List[8] = 1000;
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    strURL = "http://59.29.21.86/kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(D.GetString(rowH["TXT_USERDEF1"]) == string.Empty ? D.GetString(rowH["NO_SO"]) : D.GetString(rowH["TXT_USERDEF1"]), Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;
                case "LEDLITEK":    //엘이디라이텍
                    List[5] = GetLEDLITEKHtml(rowH, dtL);
                    List[6] = "";
                    List[8] = 5000;
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    strURL = "http://gw.ledlitek.com/kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(D.GetString(rowH["TXT_USERDEF1"]) == string.Empty ? D.GetString(rowH["NO_SO"]) : D.GetString(rowH["TXT_USERDEF1"]), Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;
                case "ENTEC":      //인텍전기전자
                    List[5] = GetENTECHtml(rowH, dtL);
                    List[6] = "생산의뢰서";
                    List[8] = 1001;
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    strURL = "http://gw.entecene.co.kr/kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(D.GetString(rowH["TXT_USERDEF1"]) == string.Empty ? D.GetString(rowH["NO_SO"]) : D.GetString(rowH["TXT_USERDEF1"]), Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;
                case "SDBIO":      //에스디바이오센서
                    List[5] = GetSDBIOHtml(rowH, dtL);
                    List[8] = 1000;
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    strURL = "http://1.244.193.178/kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(D.GetString(rowH["TXT_USERDEF1"]) == string.Empty ? D.GetString(rowH["NO_SO"]) : D.GetString(rowH["TXT_USERDEF1"]), Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;
                case "BROTHER":      //유니콘미싱공업(주)
                    List[5] = GetBROTHERHtml(rowH, dtL);
                    List[8] = 3001;
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    strURL = "http://121.134.77.7/kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(D.GetString(rowH["TXT_USERDEF1"]) == string.Empty ? D.GetString(rowH["NO_SO"]) : D.GetString(rowH["TXT_USERDEF1"]), Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;

                default:
                    break;
            }

            return isSuccess;
        }

        #region -> 포티스 HTML양식

        private string GetFortisHtml(DataRow rowH, DataTable dtL)
        {
            string body = string.Empty;
            string line = string.Empty;
            int number = 0;

            body = @"
<head>
<meta http-equiv='Content-Language' content='ko'>
</head>

<center>
    <table border='0' cellspacing='0' cellpadding='0' width='645'>
    <colgroup width='50%'></colgroup>
	<colgroup width='5%'></colgroup>
    	<tr height='30'>
    		<td colspan='2'>
    		<font size='2'>당사와 거래선간 수주 계약을 체결하여 하기와 같이 생산 드리오니 검토 후 재가 바랍니다.</font></td>
    	</tr>
       	<tr height='30'>
       		<td colspan='2' align='center'>
       		　</td>
       	</tr>
       	<tr height='30'>
       		<td colspan='2'>
       		<font size='2'>* 하&nbsp;&nbsp; 기*</font></td>
       	</tr>
       	<tr height='30'>
       		<td colspan='2' align='center'>
       		　</td>
       	</tr>
       	<tr height='30'>
       		<td colspan='2'>
       		<font size='2'>1. 수주 기본사항</font></td>
       	</tr>
       	<tr height='30'>
       		<td align='left'>
       		<font size='2'>- 거래선명 : @@거래선명</font></td>
       		<td>
       		<font size='2'>　</font></td>
       	</tr>
       	<tr height='30'>
       		<td align='left'>
       		<font size='2'>- 수주번호 : @@수주번호</font></td>
       		<td>
       		<font size='2'>　</font></td>
       	</tr>
       	<tr height='30'>
       		<td align='left'>
       		<font size='2'>- 수주일자 : @@수주일자</font></td>
       		<td>
       		<font size='2'>　</font></td>
       	</tr>
       	<tr height='30'>
       		<td align='left'>
       		<font size='2'>- 담당부서 : @@담당부서</font></td>
       		<td>
       		<font size='2'>　</font></td>
       	</tr>
       	<tr height='30'>
       		<td align='left'>
       		<font size='2'>- 담 당 자 : @@담당자</font></td>
       		<td>
       		<font size='2'>　</font></td>
       	</tr>
       	<tr height='30'>
       		<td colspan='2' align='center'>
       		　</td>
       	</tr>
    </table>
    <table border='0' cellspacing='0' cellpadding='0' width='645'>
    <colgroup width='5%'></colgroup>
	<colgroup width='15%'></colgroup>
	<colgroup width='17%'></colgroup>
	<colgroup width='15%'></colgroup>
	<colgroup width='9%'></colgroup>
	<colgroup width='9%'></colgroup>
	<colgroup width='8%'></colgroup>
	<colgroup width='11%'></colgroup>
	<colgroup width='11%'></colgroup>
       	<tr height='30'>
    		<td colspan='9'>
    		<font size='2'>2.생산 요청 상세 내용</font></td>
       	</tr>
       	<tr height='30'>
	   		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;' bgcolor='#C0C0C0'>
    		<font size='2'>No.</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;' bgcolor='#C0C0C0'>
    		<font size='2'>품목코드</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;' bgcolor='#C0C0C0'>
    		<font size='2'>품명</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;' bgcolor='#C0C0C0'>
    		<font size='2'>규격</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;' bgcolor='#C0C0C0'>
    		<font size='2'>제품향지</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;' bgcolor='#C0C0C0'>
    		<font size='2'>브랜드</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;' bgcolor='#C0C0C0'>
    		<font size='2'>수량</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;' bgcolor='#C0C0C0'>
    		<font size='2'>출고요청일</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top:1px solid #000000; border-right: 1px solid #000000;' bgcolor='#C0C0C0'>
    		<font size='2'>매출예정일</font></td>
       	</tr>
       	@@추가
       	<tr height='30'>
       		<td colspan='9' style='border-top:1px solid #000000;'>
       		　</td>
       	</tr>
       	<tr height='30'>
       		<td colspan='9'>
       		<font size='2'>**제품 별상세 내역은 첨부 참조 바람</font></td>
       	</tr>
    </table>
</center>";

            body = body.Replace("@@거래선명", D.GetString(rowH["LN_PARTNER"]));
            body = body.Replace("@@수주번호", D.GetString(rowH["NO_SO"]));
            body = body.Replace("@@수주일자", D.GetString(rowH["DT_SO"]).Substring(0, 4) + "." + D.GetString(rowH["DT_SO"]).Substring(4, 2) + "." + D.GetString(rowH["DT_SO"]).Substring(6, 2));
            body = body.Replace("@@담당부서", D.GetString(rowH["NM_SALEGRP"]));
            body = body.Replace("@@담당자",   D.GetString(rowH["NM_KOR"]));

            DataTable dt = MA.GetCode("SA_B000057", true);
            dt.PrimaryKey = new DataColumn[] { dt.Columns["CODE"] };

            foreach (DataRow rowL in dtL.Rows)
            {
                string tr = string.Empty;
                string nmFgUse = string.Empty;
                DataRow rowFgUse = dt.Rows.Find(D.GetString(rowL["FG_USE"]));
                if (rowFgUse == null) nmFgUse = string.Empty;
                else nmFgUse = D.GetString(rowFgUse["NAME"]);

                tr = GetLineFortis();
                tr = tr.Replace("@@번호", D.GetString(++number));
                tr = tr.Replace("@@품목코드", D.GetString(rowL["CD_ITEM"]));
                tr = tr.Replace("@@품명", D.GetString(rowL["NM_ITEM"]));
                tr = tr.Replace("@@규격", D.GetString(rowL["STND_ITEM"]));
                tr = tr.Replace("@@제품향지", nmFgUse);
                tr = tr.Replace("@@브랜드", D.GetString(rowL["NM_MAKER"]));
                tr = tr.Replace("@@수량", D.GetDecimal(rowL["QT_SO"]).ToString("###,###,##0"));
                tr = tr.Replace("@@출고요청일", D.GetString(rowL["DT_DUEDATE"]).Substring(0, 4) + "." + D.GetString(rowL["DT_DUEDATE"]).Substring(4, 2) + "." + D.GetString(rowL["DT_DUEDATE"]).Substring(6, 2));
                tr = tr.Replace("@@매출예정일", D.GetString(rowL["DT_REQGI"]).Substring(0, 4) + "." + D.GetString(rowL["DT_REQGI"]).Substring(4, 2) + "." + D.GetString(rowL["DT_REQGI"]).Substring(6, 2));

                line += tr;
            }

            body = body.Replace("@@추가", line);

            return body;
        }

        private string GetLineFortis()
        {
            string tr = @"
		<tr height='30'>
	   		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;'>
    		<font size='2'>@@번호&nbsp;</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;'>
    		<font size='2'>@@품목코드&nbsp;</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;'>
    		<font size='2'>@@품명&nbsp;</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;'>
    		<font size='2'>@@규격&nbsp;</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;'>
    		<font size='2'>@@제품향지&nbsp;</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;'>
    		<font size='2'>@@브랜드&nbsp;</font></td>
    		<td align='right' style='border-left: 1px solid #000000; border-top: 1px solid #000000;'>
    		<font size='2'>@@수량&nbsp;</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top: 1px solid #000000;'>
    		<font size='2'>@@출고요청일&nbsp;</font></td>
    		<td align='center' style='border-left: 1px solid #000000; border-top:1px solid #000000; border-right: 1px solid #000000;'>
    		<font size='2'>@@매출예정일&nbsp;</font></td>
       	</tr>";

            return tr;
        }
        
        #endregion

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
		<colgroup width='14%' align='center'></colgroup>
		<colgroup width='36%' align='center'></colgroup>
		<colgroup width='14%' align='center'></colgroup>
		<colgroup width='36%' align='center'></colgroup>
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>문서번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@문서번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>거래처</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@거래처</td>
		</tr>
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>수주일자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수주일자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>거래처 PO번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@PO번호</td>
		</tr>
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>영업팀/분야</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@영업그룹</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>프로젝트 번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@프로젝트번호</td>
		</tr>
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 0 1; border-color: #000000'>영업담당자</td>
			<td style='border-style: solid; border-width: 1 1 0 1; border-color: #000000'>@@담당자</td>
			<td style='border-style: solid; border-width: 1 1 0 1; border-color: #000000'>프로젝트명</td>
			<td style='border-style: solid; border-width: 1 1 0 1; border-color: #000000'>@@프로젝트명</td>
		</tr>
	</table>
	<table width='645' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: collapse; border: 0;'>
		<colgroup width='5%' align='center'></colgroup>
		<colgroup width='20%' align='center'></colgroup>
		<colgroup width='25%' align='center'></colgroup>
		<colgroup width='5%' align='center'></colgroup>
		<colgroup width='9%' align='center'></colgroup>
		<colgroup width='12%' align='center'></colgroup>
		<colgroup width='12%' align='center'></colgroup>
		<colgroup width='12%' align='center'></colgroup>
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='8' bgcolor='#99CCFF'>
			<p align='left'>&nbsp;&nbsp; ▶ 상세내역</td>
		</tr>
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>NO.</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>품목</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>상세사양</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>금액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>납기일</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>비고</td>
		</tr>
@@라인추가		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='5'>총계약금액(부가세별도)</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='3'>@@총계약금액</td>
		</tr>
		<tr height='10'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='8'></td>
		</tr>
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='8' bgcolor='#99CCFF'>
			<p align='left'>&nbsp; ▶ 특기사항</td>
		</tr>
		<tr height='60'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; padding: 10px' colspan='8'>
            <p align='left'>@@텍스트비고</td>
		</tr>
		<tr height='10'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='8'></td>
		</tr>
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='8' bgcolor='#99CCFF'>
			<p align='left'>&nbsp; ▶ 첨부서류</td>
		</tr>
		<tr height='60'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; padding: 10px' colspan='8'>
            <p align='left'>@@비고1&nbsp;</td>
		</tr>
		</table>
	</body>
</center>";

            body = body.Replace("@@문서번호", D.GetString(rowH["TXT_USERDEF1"]) == string.Empty ? D.GetString(rowH["NO_SO"]) : D.GetString(rowH["TXT_USERDEF1"]));
            body = body.Replace("@@거래처", D.GetString(rowH["LN_PARTNER"]));
            body = body.Replace("@@수주일자", D.GetString(rowH["DT_SO"]).Substring(0, 4) + "." + D.GetString(rowH["DT_SO"]).Substring(4, 2) + "." + D.GetString(rowH["DT_SO"]).Substring(6, 2));
            body = body.Replace("@@PO번호", D.GetString(rowH["NO_PO_PARTNER"]));
            body = body.Replace("@@영업그룹", D.GetString(BASIC.GetSaleGrp(D.GetString(rowH["CD_SALEGRP"]))["NM_SALEORG"]) + " / " + D.GetString(rowH["NM_SALEGRP"]));
            body = body.Replace("@@담당자", D.GetString(rowH["NM_KOR"]));
            body = body.Replace("@@프로젝트번호", D.GetString(rowH["NO_PROJECT"]));
            body = body.Replace("@@프로젝트명", D.GetString(rowH["NM_PROJECT"]));
            body = body.Replace("@@총계약금액", "(" + D.GetString(CodeSearch.GetCodeInfo(Duzon.ERPU.MF.MasterSearch.MA_CODEDTL, new object[] { MA.Login.회사코드, "MA_B000005", D.GetString(rowH["CD_EXCH"]) })["NM_SYSDEF"]) + ") " + D.GetDecimal(dtL.Compute("SUM(AM_SO)", "")).ToString("###,###,###,##0.####"));
            body = body.Replace("@@텍스트비고", D.GetString(rowH["DC_RMK_TEXT"]).Replace("\n", "<br>"));
            body = body.Replace("@@비고1", D.GetString(rowH["DC_RMK1"]));

            foreach (DataRow rowL in dtL.Rows)
            {
                string tr = string.Empty;

                tr = GetLineYPP();
                tr = tr.Replace("@@번호", D.GetString(++number));
                tr = tr.Replace("@@품목", D.GetString(rowL["NM_ITEM"]));
                tr = tr.Replace("@@상세사양", D.GetString(rowL["STND_ITEM"]));
                tr = tr.Replace("@@수량", D.GetDecimal(rowL["QT_SO"]).ToString("###,###,##0"));
                tr = tr.Replace("@@단가", D.GetDecimal(rowL["UM_SO"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@금액", D.GetDecimal(rowL["AM_SO"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@납기일", D.GetString(rowL["DT_DUEDATE"]).Substring(0, 4) + "." + D.GetString(rowL["DT_DUEDATE"]).Substring(4, 2) + "." + D.GetString(rowL["DT_DUEDATE"]).Substring(6, 2));
                tr = tr.Replace("@@비고", D.GetString(rowL["DC1"]));
                line += tr;
            }

            body = body.Replace("@@라인추가", line);

            return body;
        }

        private string GetLineYPP()
        {
            string tr = @"
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@품목</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@상세사양</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'><p align='right'>@@단가&nbsp;&nbsp;</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'><p align='right'>@@금액&nbsp;&nbsp;</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@납기일</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@비고</td>
		</tr>";

            return tr;
        }

        #endregion

        #region -> 동양이화 HTML양식
        private object GetDYPCHtml(DataRow rowH, DataTable dtL)
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
	<table width='100%' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: collapse; border: 0;'>
		<colgroup width='8%' align='center'></colgroup>
		<colgroup width='11%' align='center'></colgroup>
		<colgroup width='11%' align='center'></colgroup>
		<colgroup width='11%' align='center'></colgroup>
		<colgroup width='10%' align='center'></colgroup>
		<colgroup width='8%' align='center'></colgroup>
		<colgroup width='11%' align='center'></colgroup>
		<colgroup width='11%' align='center'></colgroup>
		<colgroup width='11%' align='center'></colgroup>
		<colgroup width='8%' align='center'></colgroup>
		
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>수주번호</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='3'>@@수주번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>수주일자</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='5'>@@수주일자</td>
		</tr>
		
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>거래처명</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='3'>@@거래처명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>담당자</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='3'>@@담당자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'></td>
		</tr>
		
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>TEL</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='3'>@@TEL</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>FAX</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='5'>@@FAX</td>
		</tr>
		
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>비고</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='9'>@@비고</td>
		</tr>
		
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>NO</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>품목코드</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>품목명</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>규격</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>단위</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>수량</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>단가</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>금액</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>납기요청일</font></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080'><font color='#FFFFFF'>비고</font></td>
		</tr>

        @@라인추가

            <tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#808080' colspan = '5'>합&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 계</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@합계수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@합계금액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
		
		</tr>
      </table>
	</body>
</center>";
            body = body.Replace("@@수주번호", D.GetString(rowH["NO_SO"]));
            body = body.Replace("@@수주일자", D.GetString(rowH["DT_SO"]).Substring(0, 4) + "." + D.GetString(rowH["DT_SO"]).Substring(4, 2) + "." + D.GetString(rowH["DT_SO"]).Substring(6, 2));
            body = body.Replace("@@거래처명", D.GetString(rowH["LN_PARTNER"]));
            body = body.Replace("@@담당자", D.GetString(rowH["CD_EMP_PARTNER"]));
            body = body.Replace("@@TEL", D.GetString(rowH["NO_TEL"]));
            body = body.Replace("@@FAX", D.GetString(rowH["NO_FAX"]));
            body = body.Replace("@@비고", D.GetString(rowH["DC_RMK"]));
            body = body.Replace("@@합계수량", D.GetDecimal(dtL.Compute("SUM(QT_SO)", "")).ToString("###,###,##0"));
            body = body.Replace("@@합계금액", D.GetDecimal(dtL.Compute("SUM(AM_SO)", "")).ToString("###,###,###,##0.####"));

            foreach (DataRow rowL in dtL.Rows)
            {
                string tr = string.Empty;

                tr = GetLineDYPC();
                tr = tr.Replace("@@NO", D.GetString(++number));
                tr = tr.Replace("@@품목코드", D.GetString(rowL["CD_ITEM"]));
                tr = tr.Replace("@@품목명", D.GetString(rowL["NM_ITEM"]));
                tr = tr.Replace("@@규격", D.GetString(rowL["STND_ITEM"]));
                tr = tr.Replace("@@단위", D.GetString(rowL["UNIT_IM"]));
                tr = tr.Replace("@@수량", D.GetDecimal(rowL["QT_SO"]).ToString("###,###,##0"));
                tr = tr.Replace("@@단가", D.GetDecimal(rowL["UM_SO"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@금액", D.GetDecimal(rowL["AM_SO"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@납기요청일", D.GetString(rowL["DT_DUEDATE"]).Substring(0, 4) + "." + D.GetString(rowL["DT_DUEDATE"]).Substring(4, 2) + "." + D.GetString(rowL["DT_DUEDATE"]).Substring(6, 2));
                tr = tr.Replace("@@비고", D.GetString(rowL["DC1"]));
                line += tr;
            }

            body = body.Replace("@@라인추가", line);

            return body;

        }

        private string GetLineDYPC()
        {
            string tr = @"
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@NO</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@품목코드</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@품목명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@규격</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@단위</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@금액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@납기요청일</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@비고</td>
		</tr>";

            return tr;
        }
        
        #endregion

        #region -> 엘이디라이텍 HTML양식

        private string GetLEDLITEKHtml(DataRow rowH, DataTable dtL)
        {
            string body = string.Empty;
            string line = string.Empty;
            int number = 0;

            body = @"
<HEAD>
  <STYLE>
    P {
    LINE-HEIGHT: 1.3; MARGIN-TOP: 1pt; MARGIN-BOTTOM: 1pt; FONT-SIZE: 12pt
    }
  </STYLE>

  <META name='GENERATOR' content='MSHTML 9.00.8112.16464'>
</HEAD>

<center>
<BODY style='FONT-FAMILY: 돋움; FONT-SIZE: 9pt'>
    <P>
      <TABLE style='BORDER-BOTTOM: medium none; TEXT-ALIGN: justify; BORDER-LEFT: medium none; BORDER-COLLAPSE: collapse; BORDER-TOP: medium none; BORDER-RIGHT: medium none; mso-border-alt: solid windowtext .5pt; mso-yfti-tbllook: 1184; mso-padding-alt: 0cm 5.4pt 0cm 5.4pt' class='MsoTableGrid' border='1' cellSpacing='0' cellPadding='0'>
        <TBODY>
          <TR style='mso-yfti-irow: 0; mso-yfti-firstrow: yes'>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 20pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt' width='64'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    No.<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 77.55pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' width='66'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <FONT face='맑은 고딕'>
                  <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>ERP</SPAN>
                </FONT>
              </P>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <FONT face='맑은 고딕'>
                  <SPAN style='FONT-SIZE: 9pt' lang='EN-US'></SPAN>
                  <SPAN style='FONT-SIZE: 9pt'>
                    <FONT style='FONT-FAMILY: 맑은 고딕; FONT-SIZE: 9pt'></FONT>코드<SPAN lang='EN-US'>
                      <o:p></o:p>
                    </SPAN>
                  </SPAN>
                </FONT>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 134.65pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' width='180'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt'>
                  <FONT face='맑은 고딕'>
                    품목명<SPAN lang='EN-US'>
                      <o:p></o:p>
                    </SPAN>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 55.05pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' width='66'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt'>
                  <FONT face='맑은 고딕'>
                    규격<SPAN lang='EN-US'>
                      <o:p></o:p>
                    </SPAN>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 30pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' width='47'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt'>
                  <FONT face='맑은 고딕'>
                    수량<SPAN lang='EN-US'>
                      <o:p></o:p>
                    </SPAN>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 2.2cm; PADDING-RIGHT: 5.4pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' width='76'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt'>
                  <FONT face='맑은 고딕'>납기</FONT>
                </SPAN>
              </P>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt'>
                  <FONT face='맑은 고딕'>
                    <FONT style='FONT-FAMILY: 맑은 고딕; FONT-SIZE: 9pt'></FONT>요구일<SPAN lang='EN-US'>
                      <o:p></o:p>
                    </SPAN>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 2.2cm; PADDING-RIGHT: 5.4pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' width='76'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt'>
                  <FONT face='맑은 고딕'>출하</FONT>
                </SPAN>
              </P>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt'>
                  <FONT face='맑은 고딕'>
                    <FONT style='FONT-FAMILY: 맑은 고딕; FONT-SIZE: 9pt'></FONT>예정일<SPAN lang='EN-US'>
                      <o:p></o:p>
                    </SPAN>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 30.55pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' width='41'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt'>
                  <FONT face='맑은 고딕'>
                    비고<SPAN lang='EN-US'>
                      <o:p></o:p>
                    </SPAN>
                  </FONT>
                </SPAN>
              </P>
            </TD>
          </TR>
          @@라인추가
          <TR style='mso-yfti-irow: 11'>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 281.85pt; HEIGHT = 20pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='376' colSpan='4'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    TOTAL<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 30pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='47'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    @@합계수량<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 2.2cm; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='76'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <o:p>
                    <FONT face='맑은 고딕'>&nbsp;</FONT>
                  </o:p>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 2.2cm; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='76'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <o:p>
                    <FONT face='맑은 고딕'>&nbsp;</FONT>
                  </o:p>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 30.55pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='41'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <o:p>
                    <FONT face='맑은 고딕'>&nbsp;</FONT>
                  </o:p>
                </SPAN>
              </P>
            </TD>
          </TR>
          <TR style='mso-yfti-irow: 12'>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 20pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' rowSpan='3' width='64'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt'>
                  <FONT face='맑은 고딕'>
                    기타<SPAN lang='EN-US'>
                      <o:p></o:p>
                    </SPAN>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 77.55pt; HEIGHT = 20pt;PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='66'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    1<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 363.65pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='485' colSpan='6'>
              <P style='TEXT-ALIGN: left; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    @@헤더비고<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
          </TR>
          <TR style='mso-yfti-irow: 13'>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 77.55pt; HEIGHT = 20pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='66'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    2<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 363.65pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='485' colSpan='6'>
              <P style='TEXT-ALIGN: left; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    &nbsp;<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
          </TR>
          <TR style='mso-yfti-irow: 14'>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 77.55pt; HEIGHT = 20pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='66'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    3<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 363.65pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='485' colSpan='6'>
              <P style='TEXT-ALIGN: left; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    &nbsp;<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
          </TR>
          <TR style='mso-yfti-irow: 15; mso-yfti-lastrow: yes'>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 20pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='64'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt'>
                  <FONT face='맑은 고딕'>특이</FONT>
                </SPAN>
              </P>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt'>
                  <FONT face='맑은 고딕'>
                    <FONT style='FONT-FAMILY: 맑은 고딕; FONT-SIZE: 9pt'></FONT>사항<SPAN lang='EN-US'>
                      <o:p></o:p>
                    </SPAN>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 413.25pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='551' colSpan='7'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <o:p>
                    <FONT face='맑은 고딕'>&nbsp;</FONT>
                  </o:p>
                </SPAN>
              </P>
            </TD>
          </TR>
        </TBODY>
      </TABLE>
    </P>
  </BODY>
</center>
";
            body = body.Replace("@@합계수량", D.GetDecimal(dtL.Compute("SUM(QT_SO)", "")).ToString("###,###,###,##0.####"));
            body = body.Replace("@@헤더비고", D.GetString(rowH["DC_RMK"]));

            foreach (DataRow rowL in dtL.Rows)
            {
                string tr = string.Empty;

                tr = GetLineLEDLITEK();
                tr = tr.Replace("@@NO", D.GetString(++number));
                tr = tr.Replace("@@품목코드", D.GetString(rowL["CD_ITEM"]));
                tr = tr.Replace("@@품목명", D.GetString(rowL["NM_ITEM"]));
                tr = tr.Replace("@@규격", D.GetString(rowL["STND_ITEM"]));
                tr = tr.Replace("@@수량", D.GetDecimal(rowL["QT_SO"]).ToString("###,###,##0"));
                tr = tr.Replace("@@납기요구일", D.GetString(rowL["DT_DUEDATE"]).Substring(0, 4) + "/" + D.GetString(rowL["DT_DUEDATE"]).Substring(4, 2) + "/" + D.GetString(rowL["DT_DUEDATE"]).Substring(6, 2));
                tr = tr.Replace("@@출하예정일", D.GetString(rowL["DT_REQGI"]).Substring(0, 4) + "/" + D.GetString(rowL["DT_REQGI"]).Substring(4, 2) + "/" + D.GetString(rowL["DT_REQGI"]).Substring(6, 2));
                tr = tr.Replace("@@라인비고", D.GetString(rowL["DC1"]));
                line += tr;
            }

            body = body.Replace("@@라인추가", line);

            return body;
        }

        private string GetLineLEDLITEK()
        {
            string tr = @"
		<TR style='mso-yfti-irow: 1'>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 20pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='64'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    @@NO<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 77.55pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='66'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    @@품목코드<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 134.65pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='180'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    @@품목명<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 55.05pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='66'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    @@규격<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 30pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='47'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    @@수량<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 2.2cm; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='76'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    @@납기요구일<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 2.2cm; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='76'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    @@출하예정일<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
            <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: #f0f0f0; PADDING-BOTTOM: 0cm; BACKGROUND-COLOR: transparent; PADDING-LEFT: 5.4pt; WIDTH: 30.55pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: #f0f0f0; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width='41'>
              <P style='TEXT-ALIGN: center; LINE-HEIGHT: normal; MARGIN: 0cm 0cm 0pt' class='MsoNormal' align='center'>
                <SPAN style='FONT-SIZE: 9pt' lang='EN-US'>
                  <FONT face='맑은 고딕'>
                    @@라인비고<o:p></o:p>
                  </FONT>
                </SPAN>
              </P>
            </TD>
          </TR>";

            return tr;
        }

        #endregion

        #region -> 인텍전기전가 HTML양식
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
		<colgroup width='7%' align='center'></colgroup>
		<colgroup width='13%' align='center'></colgroup>
		<colgroup width='11%' align='center'></colgroup>
		<colgroup width='30%' align='center'></colgroup>
		<colgroup width='6%' align='center'></colgroup>
		<colgroup width='11%' align='center'></colgroup>
		<colgroup width='11%' align='center'></colgroup>
		<colgroup width='11%' align='center'></colgroup>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='2' align='left'>수&nbsp;&nbsp; 신 :</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>발&nbsp;&nbsp; 신 :</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>번 호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>고 객 명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>원가부분</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>품명/규격</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>예상납기</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>납기일</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>비고</td>
		</tr>

        @@라인추가

<tr height='85'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'>특기사항</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='3'>&nbsp;</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='3'>위와 같이 생산을 의뢰합니다.<br>20&nbsp;&nbsp;&nbsp; .&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; .&nbsp;&nbsp;&nbsp; <br>작성 : 영업부 (인)</td>
		</tr>

      </table>
	</body>
</center>";

            foreach (DataRow rowL in dtL.Rows)
            {
                string tr = string.Empty;

                tr = GetLineENTEC();
                tr = tr.Replace("@@NO", D.GetString(++number));
                tr = tr.Replace("@@고객명", D.GetString(rowL["LN_PARTNER"]));
                tr = tr.Replace("@@원가부분", D.GetString(rowL["NM_PROJECT"]));
                tr = tr.Replace("@@품명규격", D.GetString(rowL["NM_ITEM"]) + "," + D.GetString(rowL["STND_ITEM"]));
                tr = tr.Replace("@@수량", D.GetDecimal(rowL["QT_SO"]).ToString("###,###,##0"));
                tr = tr.Replace("@@예상납기", D.GetString(rowL["DT_REQGI"]).Substring(0, 4) + "." + D.GetString(rowL["DT_REQGI"]).Substring(4, 2) + "." + D.GetString(rowL["DT_REQGI"]).Substring(6, 2));
                tr = tr.Replace("@@납기일", D.GetString(rowL["DT_REQGI"]).Substring(0, 4) + "." + D.GetString(rowL["DT_REQGI"]).Substring(4, 2) + "." + D.GetString(rowL["DT_REQGI"]).Substring(6, 2));
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
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@고객명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@원가부분</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@품명규격</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@예상납기</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@납기일</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
		</tr>";

            return tr;
        } 
        #endregion

        #region -> 에스디바이오센서 HTML양식
        private object GetSDBIOHtml(DataRow rowH, DataTable dtL)
        {
            string body = string.Empty;
            string line = string.Empty;
            string NM_FG_USE = string.Empty;
            int number = 0;

            DataTable dt_code1 = MA.GetCode("SA_B000057");
            dt_code1.PrimaryKey = new DataColumn[] { dt_code1.Columns["CODE"] };

            DataTable dt_code2 = MA.GetCode("SA_B000063");
            dt_code2.PrimaryKey = new DataColumn[] { dt_code2.Columns["CODE"] };

            DataRow[] drs1 = dtL.Select("FG_USE = '010'", "", DataViewRowState.CurrentRows);            // 판매
            DataRow[] drs2 = dtL.Select("FG_USE = '020'", "", DataViewRowState.CurrentRows);            // 할증
            string CodeMin  = D.GetString(dtL.Compute("MIN(FG_USE)", "FG_USE NOT IN ('010','020') AND ISNULL(FG_USE,'') <> '' "));  // 그외

            if (drs1.Length > 0)
            {
                DataRow dr = dt_code1.Rows.Find("010");
                if (dr != null)
                    NM_FG_USE = D.GetString(dr["NAME"]);
            }
            else if (drs2.Length > 0)
            {
                DataRow dr = dt_code1.Rows.Find("020");
                if (dr != null)
                    NM_FG_USE = D.GetString(dr["NAME"]);
            }
            else
            {
                DataRow dr = dt_code1.Rows.Find(CodeMin);
                if (dr != null)
                    NM_FG_USE = D.GetString(dr["NAME"]);
            }
            
            DataTable dt = DBHelper.GetDataTable("UP_SA_Z_SDBIO_SO_GW_INFO_S", new object[] { MA.Login.회사코드, D.GetString(rowH["CD_PARTNER"]), D.GetString(rowH["DT_SO"]) });

            body = @"
<head>
		<meta http-equiv='Content-Language' content='ko'>
		<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
	</head>

<center>
	<body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0'>
		<table width='100%' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size=10pt; border-collapse: collapse; border: 0;'>
			<colgroup width='16%' align='center'></colgroup>
			<colgroup width='34%' align='center'></colgroup>
			<colgroup width='4%' align='center'></colgroup>
			<colgroup width='18%' align='center'></colgroup>
			<colgroup width='28%' align='center'></colgroup>

		<tr height='23'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>오더타입</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@오더타입</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>수주용도</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@수주용도</td>
		</tr>

		<tr height='23'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>주문번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@주문번호</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>여신한도</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@여신한도</td>
		</tr>

		<tr height='23'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>거래처명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@거래처명</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>담보</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@담보</td>
		</tr>

		<tr height='23'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>거래처구분</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@거래처구분</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>90일이상되는 잔고</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@90일이상잔고</td>
		</tr>

		<tr height='23'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>팀/담당자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@담당자</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>미수채권기한</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@미수채권기한</td>
		</tr>

		<tr height='23'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>3개월 평균매출액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@3개월평균매출액</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>3개월 원가액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@3개월원가액</td>
		</tr>

		<tr height='23'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>비고</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt' colspan='4'>@@비고</td>
		</tr>

		<tr height='10'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>

	</table>
		<table width='100%' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size=10pt; border-collapse: collapse; border: 0;'>
			<colgroup width='5%' align='center'></colgroup>
			<colgroup width='12%' align='center'></colgroup>
			<colgroup width='19%' align='center'></colgroup>
			<colgroup width='6%' align='center'></colgroup>
			<colgroup width='12%' align='center'></colgroup>
			<colgroup width='12%' align='center'></colgroup>
			<colgroup width='12%' align='center'></colgroup>
			<colgroup width='12%' align='center'></colgroup>
			<colgroup width='10%' align='center'></colgroup>

		<tr height='25'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'>부가세별도</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000; font-size:9pt'></td>
		</tr>

		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>No.</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>품목코드</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>품목명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>금액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>결재단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>기준단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>수주구분</td>
		</tr>


        @@라인추가

<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>소계</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@수량소계</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@금액소계</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'></td>
		</tr>

      </table>
	</body>
</center>";
            body = body.Replace("@@오더타입", D.GetString(rowH["NM_SO"]));
            body = body.Replace("@@주문번호", D.GetString(rowH["NO_SO"]));
            body = body.Replace("@@거래처명", D.GetString(rowH["LN_PARTNER"]));
            body = body.Replace("@@거래처구분", D.GetString(dt.Rows[0]["NM_PARTNER_GRP"]));
            body = body.Replace("@@담당자", D.GetString(rowH["NM_SALEGRP"]) + "/" + D.GetString(rowH["NM_KOR"]));
            body = body.Replace("@@3개월평균매출액", D.GetDecimal(dt.Rows[0]["AM_IV"]).ToString("###,###,###,##0"));
            body = body.Replace("@@수주용도", NM_FG_USE);
            body = body.Replace("@@여신한도", D.GetDecimal(dt.Rows[0]["TOT_CREDIT"]).ToString("###,###,###,##0"));
            body = body.Replace("@@담보", D.GetDecimal(dt.Rows[0]["AM_REAL"]).ToString("###,###,###,##0"));
            body = body.Replace("@@90일이상잔고", D.GetDecimal(dt.Rows[0]["AM_JANGO"]).ToString("###,###,###,##0.####"));
            body = body.Replace("@@미수채권기한", D.GetString(dt.Rows[0]["DT_MISU"]) == string.Empty ? string.Empty : D.GetString(dt.Rows[0]["DT_MISU"]) + "일");
            body = body.Replace("@@3개월원가액", D.GetDecimal(dt.Rows[0]["NUM_USERDEF2"]).ToString("###,###,###,##0"));
            body = body.Replace("@@비고", D.GetString(rowH["DC_RMK"]));
            body = body.Replace("@@수량소계", D.GetDecimal(dtL.Compute("SUM(QT_SO)", "")).ToString("###,###,##0"));
            body = body.Replace("@@금액소계", D.GetDecimal(dtL.Compute("SUM(AM_SO)", "")).ToString("###,###,###,##0.####"));

            foreach (DataRow rowL in dtL.Rows)
            {
                string tr = string.Empty;

                tr = GetLineSDBIO();
                tr = tr.Replace("@@NO", D.GetString(++number));
                tr = tr.Replace("@@품목코드", D.GetString(rowL["CD_ITEM"]));
                tr = tr.Replace("@@품목명", D.GetString(rowL["NM_ITEM"]));
                tr = tr.Replace("@@수량", D.GetDecimal(rowL["QT_SO"]).ToString("###,###,##0"));
                tr = tr.Replace("@@단가", D.GetDecimal(rowL["UM_SO"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@금액", D.GetDecimal(rowL["AM_SO"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@결재단가", D.GetDecimal(rowL["NUM_USERDEF6"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@기준단가", D.GetDecimal(rowL["NUM_USERDEF5"]).ToString("###,###,###,##0.####"));

                DataRow dr = dt_code2.Rows.Find(D.GetString(rowL["FG_USE2"]));
                tr = tr.Replace("@@수주구분", dr == null ? string.Empty : D.GetString(dr["NAME"]));
                line += tr;
            }

            body = body.Replace("@@라인추가", line);

            return body;
        }

        private string GetLineSDBIO()
        {
            string tr = @"
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@NO</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@품목코드</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@품목명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@금액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@결재단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@기준단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; font-size:9pt'>@@수주구분</td>
		</tr>";

            return tr;
        } 
        #endregion 

        #region -> 유니콘미싱공업 HTML양식
        private object GetBROTHERHtml(DataRow rowH, DataTable dtL)
        {
            string body = string.Empty;
            string line = string.Empty;
            int number = 0;
            decimal AM_COST = 0m;
            decimal UM_INV = 0m;
            string query = string.Empty;
            query = " SELECT B.NO_BIZAREA, B.NM_BIZAREA, B.NM_MASTER, B.ADS_H, B.ADS_D, B.TP_JOB, B.CLS_JOB, B.NO_TEL "
                  + " FROM   MA_PLANT P "
                  + "        INNER JOIN MA_BIZAREA B ON P.CD_COMPANY = B.CD_COMPANY AND P.CD_BIZAREA = B.CD_BIZAREA "
                  + " WHERE  P.CD_COMPANY = '" + MA.Login.회사코드 + "'"
                  + " AND    P.CD_PLANT = '" + D.GetString(dtL.Rows[0]["CD_PLANT"]) + "'";

            DataTable dt_BizInfo = DBHelper.GetDataTable(query);

            품목관리.조회 품목조회 = new 품목관리.조회();
            DataTable dt = 품목조회.예상이익(D.GetString(dtL.Rows[0]["CD_PLANT"]), D.GetString(rowH["DT_SO"]), dtL.Select());
            dt.PrimaryKey = new DataColumn[] { dt.Columns["CD_ITEM"] };

            body = @"
<html>
<head>
<title>Untitled Document</title>
<meta http-equiv='Content-Type' content='text/html; charset=euc-kr'>
<style type='text/css'>
BODY {font-family:굴림; font-size:12px; color:#000000;}  P   {font-family:굴림; font-size:12px; color:#000000;} td   {font-family:굴림; font-size:12px; color:#000000;}                  a:link    {font-family:굴림; font-size:12px; color:#666666; text-decoration:none;}         a:visited {font-family:굴림; font-size:12px; color:#666666; text-decoration:none;}         a:active  {font-family:굴림; font-size:12px; color:#666666; text-decoration:none;}         a:hover   {font-family:굴림; font-size:12px; color:#666666; text-decoration:none;}
</style>
</head>
<body bgcolor='#FFFFFF' text='#000000'>
<table width='650' border='0' cellspacing='0' cellpadding='0'>
<tr>
	<td width='300'>
		<table width='300' border='0' cellspacing='0' cellpadding='2'>
		<tr>
			<td width='85' height='21'> <div align='right'> 수주번호: </div> </td>
			<td colspan='3' height='21'> @@수주번호 </td>
		</tr>
		<tr>
			<td width='85' height='21'><div align='right'>거래처명:</div></td>
			<td colspan='3' height='21'><div align='left'>@@거래처명</div></td>
		</tr>
		<tr>    
            <td width='85' height='21'><div align='right'>수주일자:</div></td>
			<td colspan='3' height='21'><div align='left'>@@수주일자</div></td>
        </tr>
		<tr>
			<td width='85' height='21'><div align='right'>거래처담당자:</div></td>
			<td colspan='3' height='21'><div align='left'>@@거래처담당자</div></td>
		</tr>
		<tr>
			<td width='85' height='21'><div align='right'>전화번호:</div></td>
			<td width='90' height='21'><div align='left'>@@전화번호</div></td>
			<td width='35' height='20'><div align='right'>FAX:</div></td>
			<td width='90' height='20'><div align='left'>@@FAX</div></td>
		</tr>
		<tr>
			<td colspan='4' height='21'><table width='300' border='0' cellspacing='0' cellpadding='2' height='20'>
				                   <tr><td width='35'><div align='right'>주소:</div></td>
                                       <td width='265'>@@주소</td>
                                   </tr>
				</table>
			</td>
		</tr>
		</table>
	</td>
	<td width='330'>
		<table width='350' border='1' cellspacing='0' cellpadding='2' bordercolordark='white' bordercolorlight='#000000' bordercolor='#000000'>
		<tr>
			<td rowspan='5' bgcolor='#D5F9F9'><div align='center'>공<br><br>급<br><br>자</div></td>
			<td width='80' height='23' bgcolor='#F0FBFA'><div align='right'>등록번호</div></td>
			<td colspan='3' height='23'>@@등록번호&nbsp;&nbsp;</td></tr>
		<tr>
			<td width='80' height='23' bgcolor='#F0FBFA'><div align='right'>상호(법인명)</div></td>
			<td colspan='3' height='23'>@@상호법인명&nbsp;&nbsp;</td>
		</tr>
		<tr>
			<td width='80' height='23' bgcolor='#F0FBFA'><div align='center'>대 표</div></td>
			<td width='100' height='23'>@@대표&nbsp;&nbsp;</td>
			<td width='40' height='23' bgcolor='#F0FBFA'><div align='center'>TEL</div></td>
			<td width='100' height='23'>@@TEL&nbsp;&nbsp;</td>
		</tr>
		<tr>
			<td width='80' height='23' bgcolor='#F0FBFA'><div align='center'>업 태</div></td>
			<td width='100' height='23'>@@업태&nbsp;&nbsp;</td>
			<td width='40' height='23' bgcolor='#F0FBFA'><div align='center'>업종</div></td>
			<td width='100' height='23'>@@업종&nbsp;&nbsp;</td>
		</tr>
		<tr>
			<td width='80' height='23' bgcolor='#F0FBFA'><div align='right'>주 소</div></td>
			<td colspan='3' height='23'>@@공급자주소&nbsp;&nbsp;</td>
		</tr>
		</table>
	</td>
</tr>
<tr>
	<td colspan='2'></td>
</tr>
<tr>
	<td colspan='2'></td>
</tr>
<tr>
	<td colspan='2' height='10'></td>
</tr>
<tr>
	<td colspan='2' height='20'><div align='center'>- 아래와 같이 수주합니다. -</div></td>
</tr>
<tr>
	<td colspan='2' height='20'><div align='right'>&nbsp;&nbsp;&nbsp;(금액단위 : US$ )</div></td>
</tr>
<tr>
	<td colspan='2'>
		<table width='650' border='1' cellspacing='0' cellpadding='2' bordercolordark='white' bordercolorlight='#000000' bordercolor='#000000'>
		<tr bgcolor='#F0FBFA'><td bgcolor='#F0FBFA' width='20' height='20'><div align='center'>NO</div></td>
                                                <td width='110' height='20'><div align='center'>품목명</div></td>
			                                    <td width='80' height='20'><div align='center'>규격</div></td>
			                                    <td width='40' height='20'><div align='center'>단위</div></td>
			                                    <td width='80' height='20'><div align='center'>수량</div></td>
			                                    <td width='80' height='20'><div align='center'>단가</div></td>
			                                    <td width='90' bgcolor='#F0FBFA' height='20'><div align='center'>금액</div></td>
			                                    <td width='90' bgcolor='#F0FBFA' height='20'><div align='center'>원화금액</div></td>
                                                <td width='100' height='20'><div align='center'>원가</div></td>
                                                <td width='100' height='20'><div align='center'>이익율</div></td>
</tr>


@@라인추가


	    <tr bgcolor='#F0FBFA'><td bgcolor='#FCF8E9' colspan='4' height='20'><div align='center'>합 계</div></td>
		<td bgcolor='#FFFFFF' height='20' valign='top' width='90'><div align='right'>@@합계수량&nbsp;</div></td>
        <td bgcolor='#FFFFFF' height='20' valign='top' width='90'><div align='right'>&nbsp;</div></td>
        <td bgcolor='#FFFFFF' height='20' valign='top' width='90'><div align='right'>@@합계금액&nbsp;</div></td>
        <td bgcolor='#FFFFFF' height='20' valign='top' width='90'><div align='right'>@@합계원화금액&nbsp;</div></td>
        <td bgcolor='#FFFFFF' height='20' valign='top' width='90'><div align='right'>@@합계원가&nbsp;</div></td>
        <td bgcolor='#FFFFFF' height='20' valign='top' width='100'><div align='right'>@@합계이익율&nbsp;</div></td>
  </tr>
		<tr bgcolor='#FFFFFF'><td colspan='10' height='20'><div align='center'><p>- 이 하 여 백 -</p>
		              @@헤더비고
				</div>
			</td>
		</tr>
		</table>
	</td>
</tr>

	</table>
	</body>

</center>";
            
            body = body.Replace("@@수주번호", D.GetString(rowH["NO_SO"]));
            body = body.Replace("@@거래처명", D.GetString(rowH["LN_PARTNER"]));
            body = body.Replace("@@수주일자", D.GetString(rowH["DT_SO"]).Substring(0, 4) + "." + D.GetString(rowH["DT_SO"]).Substring(4, 2) + "." + D.GetString(rowH["DT_SO"]).Substring(6, 2));
            body = body.Replace("@@전화번호", D.GetString(rowH["NO_TEL"]));
            body = body.Replace("@@FAX", D.GetString(rowH["NO_FAX"]));
            body = body.Replace("@@거래처담당자", D.GetString(rowH["CD_EMP_PARTNER"]));
            body = body.Replace("@@주소", D.GetString(rowH["DC_ADS2_H"]) + " " + D.GetString(rowH["DC_ADS2_D"]));
            body = body.Replace("@@등록번호", D.GetString(dt_BizInfo.Rows[0]["NO_BIZAREA"]).Substring(0, 3) + "-" + D.GetString(dt_BizInfo.Rows[0]["NO_BIZAREA"]).Substring(3, 2) + "-" + D.GetString(dt_BizInfo.Rows[0]["NO_BIZAREA"]).Substring(5, 5));
            body = body.Replace("@@상호법인명", D.GetString(dt_BizInfo.Rows[0]["NM_BIZAREA"]));
            body = body.Replace("@@대표", D.GetString(dt_BizInfo.Rows[0]["NM_MASTER"]));
            body = body.Replace("@@TEL", D.GetString(dt_BizInfo.Rows[0]["NO_TEL"]));
            body = body.Replace("@@업태", D.GetString(dt_BizInfo.Rows[0]["TP_JOB"]));
            body = body.Replace("@@업종", D.GetString(dt_BizInfo.Rows[0]["CLS_JOB"]));
            body = body.Replace("@@공급자주소", D.GetString(dt_BizInfo.Rows[0]["ADS_H"]) + " " + D.GetString(dt_BizInfo.Rows[0]["ADS_D"]));
            body = body.Replace("@@합계수량", D.GetDecimal(dtL.Compute("SUM(QT_SO)", "")).ToString("###,###,##0"));
            body = body.Replace("@@합계금액", D.GetDecimal(dtL.Compute("SUM(AM_SO)", "")).ToString("###,###,###,##0.####"));
            body = body.Replace("@@합계원화금액", D.GetDecimal(dtL.Compute("SUM(AM_WONAMT)", "")).ToString("###,###,###,##0"));
            body = body.Replace("@@헤더비고", D.GetString(rowH["DC_RMK"]));
            
            foreach (DataRow rowL in dtL.Rows)
            {
                string tr = string.Empty;
                tr = GetLineBROTHER();
                tr = tr.Replace("@@NO", D.GetString(++number));
                tr = tr.Replace("@@품목명", D.GetString(rowL["NM_ITEM"]));
                tr = tr.Replace("@@규격", D.GetString(rowL["STND_ITEM"]));
                tr = tr.Replace("@@단위", D.GetString(rowL["UNIT_IM"]));
                tr = tr.Replace("@@수량", D.GetDecimal(rowL["QT_SO"]).ToString("###,###,##0"));
                tr = tr.Replace("@@단가", D.GetDecimal(rowL["UM_SO"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@금액", D.GetDecimal(rowL["AM_SO"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@원화금액", D.GetDecimal(rowL["AM_WONAMT"]).ToString("###,###,###,##0"));
                DataRow rowFine = dt.Rows.Find(rowL["CD_ITEM"]);
                UM_INV = rowFine == null ? 0m : D.GetDecimal(rowFine["UM_INV"]);
                tr = tr.Replace("@@원가", (UM_INV * D.GetDecimal(rowL["QT_SO"])).ToString("###,###,###,##0"));
                tr = tr.Replace("@@이익율", D.GetDecimal(rowL["AM_WONAMT"]) == 0 ? "0" : (((D.GetDecimal(rowL["AM_WONAMT"]) - (UM_INV * D.GetDecimal(rowL["QT_SO"]))) / D.GetDecimal(rowL["AM_WONAMT"])) * 100).ToString("###,###.##"));
                AM_COST += UM_INV * D.GetDecimal(rowL["QT_SO"]);
                line += tr;
            }

            body = body.Replace("@@합계원가", AM_COST.ToString("###,###,###,##0"));
            body = body.Replace("@@라인추가", line);
            body = body.Replace("@@합계이익율", D.GetDecimal(dtL.Compute("SUM(AM_WONAMT)", "")) == 0 ? "0" : (((D.GetDecimal(dtL.Compute("SUM(AM_WONAMT)", "")) - AM_COST) / D.GetDecimal(dtL.Compute("SUM(AM_WONAMT)", ""))) * 100).ToString("###,###.##"));

            return body;
        }

        private string GetLineBROTHER()
        {
            string tr = @"
            <tr bgcolor='#F0FBFA'><td bgcolor='#F6F6F6' width='20' height='20'><div align='center'>@@NO&nbsp;</div></td>
			                                        <td width='110' bgcolor='#FFFFFF' valign='top' height='20'>@@품목명&nbsp;</td>
			                                        <td width='80' bgcolor='#FFFFFF' height='20'>@@규격&nbsp;&nbsp;</td>
			                                        <td width='40' bgcolor='#FFFFFF' height='20'>@@단위&nbsp;</td>
			                                        <td width='80' bgcolor='#FFFFFF' height='20'><div align='right'>@@수량&nbsp;</div></td>
			                           <td valign='top' width='80' bgcolor='#FFFFFF' height='20'><div align='right'>@@단가&nbsp;</div></td>
		 <td bgcolor='#FFFFFF' height='20' valign='top' width='90'><div align='right'>@@금액&nbsp;</div></td>
		 <td bgcolor='#FFFFFF' height='20' valign='top' width='90'><div align='right'>@@원화금액&nbsp;</div></td>
         <td bgcolor='#FFFFFF' height='20' valign='top' width='90'><div align='right'>@@원가&nbsp;</div></td>
         <td bgcolor='#FFFFFF' height='20' valign='top' width='100'><div align='right'>@@이익율&nbsp;</td></tr>";


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
    }
}
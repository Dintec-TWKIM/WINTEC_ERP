using System;
using System.Collections.Generic;
using System.Text;
using Duzon.ERPU;
using Duzon.Common.Forms;
using System.Data;

namespace sale
{
    class NP_GW
    {
        internal bool 전자결재(DataRow rowH, DataTable dtL)
        {
            bool isSuccess = true;

            List<object> List = new List<object>();
            List.Add(MA.Login.회사코드);
            List.Add(Global.MainFrame.LoginInfo.CdPc);
            List.Add(D.GetString(rowH["NO_EST"]));
            List.Add(MA.Login.사원번호);
            List.Add(Global.MainFrame.GetStringToday);
            List.Add(string.Empty);
            List.Add("통합견적서");
            List.Add("Y");
            List.Add(100); //APP_FORM_KIND에 들어갈 값
            List.Add("29991231");

            //switch (Global.MainFrame.ServerKeyCommon.ToUpper())
            //{
                //case "DZSQL":       // 84번 개발서버
                //    List[5] = GetNP(rowH, dtL);
                //    isSuccess = 결재상신_개발서버(List.ToArray());
                //    break;

                //default:
                    List[5] = GetNP(rowH, dtL);
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    //string strURL = "http://gw.cisro.co.kr/kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(D.GetString(rowH["NO_EST"]), Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    string strURL = "http://1.244.118.117:8090/gw/anonymous/erpGwLink.do?menu=/edoc/eapproval/docCommonDrafWrite.do&form_id=100&id=" + MA.Login.사용자아이디 + "&isPopup=Y&IU_KEY=" + System.Web.HttpUtility.UrlEncode(MA.Login.회사코드 + "/" + Global.MainFrame.LoginInfo.CdPc + "/" + D.GetString(rowH["NO_EST"]), Encoding.UTF8) + "&IU_ERP=Y"; 

                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    //break;
            //}

            return isSuccess;
        }

        #region -> NP Html양식
        private string GetNP(DataRow rowH, DataTable dtL)
        {
            string body = string.Empty;
            string line = string.Empty;

            int number = 0;

            body = @"
<HEAD>
<META NAME='Generator' CONTENT='Haansoft HWP 7.5.12.699'>
<META HTTP-EQUIV='Content-Type' CONTENT='text/html; charset=euc-kr'>
<TITLE></TITLE>
<STYLE>
<!--
P.HStyle0, LI.HStyle0, DIV.HStyle0
	{style-name:'바탕글'; margin-left:0.0pt; margin-right:0.0pt; margin-top:0.0pt; margin-bottom:0.0pt; text-align:justify; text-indent:0.0pt; line-height:160%; font-size:10.0pt; font-family:바탕; letter-spacing:0.0pt; font-weight:'normal'; font-style:'normal'; color:#000000;}
P.HStyle1, LI.HStyle1, DIV.HStyle1
	{style-name:'본문'; margin-left:15.0pt; margin-right:0.0pt; margin-top:0.0pt; margin-bottom:0.0pt; text-align:justify; text-indent:0.0pt; line-height:160%; font-size:10.0pt; font-family:바탕; letter-spacing:0.0pt; font-weight:'normal'; font-style:'normal'; color:#000000;}
P.HStyle2, LI.HStyle2, DIV.HStyle2
	{style-name:'개요 1'; margin-left:10.0pt; margin-right:0.0pt; margin-top:0.0pt; margin-bottom:0.0pt; text-align:justify; text-indent:0.0pt; line-height:160%; font-size:10.0pt; font-family:바탕; letter-spacing:0.0pt; font-weight:'normal'; font-style:'normal'; color:#000000;}
P.HStyle3, LI.HStyle3, DIV.HStyle3
	{style-name:'개요 2'; margin-left:20.0pt; margin-right:0.0pt; margin-top:0.0pt; margin-bottom:0.0pt; text-align:justify; text-indent:0.0pt; line-height:160%; font-size:10.0pt; font-family:바탕; letter-spacing:0.0pt; font-weight:'normal'; font-style:'normal'; color:#000000;}
P.HStyle4, LI.HStyle4, DIV.HStyle4
	{style-name:'개요 3'; margin-left:30.0pt; margin-right:0.0pt; margin-top:0.0pt; margin-bottom:0.0pt; text-align:justify; text-indent:0.0pt; line-height:160%; font-size:10.0pt; font-family:바탕; letter-spacing:0.0pt; font-weight:'normal'; font-style:'normal'; color:#000000;}
P.HStyle5, LI.HStyle5, DIV.HStyle5
	{style-name:'개요 4'; margin-left:40.0pt; margin-right:0.0pt; margin-top:0.0pt; margin-bottom:0.0pt; text-align:justify; text-indent:0.0pt; line-height:160%; font-size:10.0pt; font-family:바탕; letter-spacing:0.0pt; font-weight:'normal'; font-style:'normal'; color:#000000;}
P.HStyle6, LI.HStyle6, DIV.HStyle6
	{style-name:'개요 5'; margin-left:50.0pt; margin-right:0.0pt; margin-top:0.0pt; margin-bottom:0.0pt; text-align:justify; text-indent:0.0pt; line-height:160%; font-size:10.0pt; font-family:바탕; letter-spacing:0.0pt; font-weight:'normal'; font-style:'normal'; color:#000000;}
P.HStyle7, LI.HStyle7, DIV.HStyle7
	{style-name:'개요 6'; margin-left:60.0pt; margin-right:0.0pt; margin-top:0.0pt; margin-bottom:0.0pt; text-align:justify; text-indent:0.0pt; line-height:160%; font-size:10.0pt; font-family:바탕; letter-spacing:0.0pt; font-weight:'normal'; font-style:'normal'; color:#000000;}
P.HStyle8, LI.HStyle8, DIV.HStyle8
	{style-name:'개요 7'; margin-left:70.0pt; margin-right:0.0pt; margin-top:0.0pt; margin-bottom:0.0pt; text-align:justify; text-indent:0.0pt; line-height:160%; font-size:10.0pt; font-family:바탕; letter-spacing:0.0pt; font-weight:'normal'; font-style:'normal'; color:#000000;}
P.HStyle9, LI.HStyle9, DIV.HStyle9
	{style-name:'쪽 번호'; margin-left:0.0pt; margin-right:0.0pt; margin-top:0.0pt; margin-bottom:0.0pt; text-align:justify; text-indent:0.0pt; line-height:160%; font-size:10.0pt; font-family:굴림; letter-spacing:0.0pt; font-weight:'normal'; font-style:'normal'; color:#000000;}
P.HStyle10, LI.HStyle10, DIV.HStyle10
	{style-name:'머리말'; margin-left:0.0pt; margin-right:0.0pt; margin-top:0.0pt; margin-bottom:0.0pt; text-align:justify; text-indent:0.0pt; line-height:150%; font-size:9.0pt; font-family:굴림; letter-spacing:0.0pt; font-weight:'normal'; font-style:'normal'; color:#000000;}
P.HStyle11, LI.HStyle11, DIV.HStyle11
	{style-name:'각주'; margin-left:13.1pt; margin-right:0.0pt; margin-top:0.0pt; margin-bottom:0.0pt; text-align:justify; text-indent:-13.1pt; line-height:130%; font-size:9.0pt; font-family:바탕; letter-spacing:0.5pt; font-weight:'normal'; font-style:'normal'; color:#000000;}
P.HStyle12, LI.HStyle12, DIV.HStyle12
	{style-name:'미주'; margin-left:13.1pt; margin-right:0.0pt; margin-top:0.0pt; margin-bottom:0.0pt; text-align:justify; text-indent:-13.1pt; line-height:130%; font-size:9.0pt; font-family:바탕; letter-spacing:0.5pt; font-weight:'normal'; font-style:'normal'; color:#000000;}
P.HStyle13, LI.HStyle13, DIV.HStyle13
	{style-name:'메모'; margin-left:0.0pt; margin-right:0.0pt; margin-top:0.0pt; margin-bottom:0.0pt; text-align:justify; text-indent:0.0pt; line-height:160%; font-size:9.0pt; font-family:굴림; letter-spacing:0.5pt; font-weight:'normal'; font-style:'normal'; color:#000000;}
-->
</STYLE>
</HEAD>

<BODY>

<P CLASS=HStyle0>
<TABLE border='1' cellspacing='0' cellpadding='0' style='border-collapse:collapse;border:none;'>
<TR>
	<TD colspan='6' width='374' height='21' valign='middle' style='border-left:none;border-right:solid #000000 1.1pt;border-top:none;border-bottom:none;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'><SPAN STYLE='font-size:9.0pt;font-weight:bold;line-height:160%;'>&nbsp;견적일자 : @@견적일자 </SPAN></P></TD>
	<TD colspan='2' width='104' height='21' valign='middle' style='border-left:solid #000000 1.1pt;border-right:solid #000000 0.4pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;font-weight:bold;line-height:160%;'>사업자번호</SPAN></P></TD>
	<TD colspan='3' width='156' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@사업자번호&nbsp;</SPAN></P></TD>
</TR>
<TR>
	<TD colspan='6' width='374' height='21' valign='middle' style='border-left:none;border-right:solid #000000 1.1pt;border-top:none;border-bottom:none;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'><SPAN STYLE='font-size:9.0pt;font-weight:bold;line-height:160%;'>&nbsp;거 래 처 : @@거래처</SPAN></P></TD>
	<TD colspan='2' width='104' height='21' valign='middle' style='border-left:solid #000000 1.1pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;font-weight:bold;line-height:160%;'>상호(법인명)</SPAN></P></TD>
	<TD colspan='3' width='156' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@상호&nbsp;</SPAN></P></TD>
</TR>
<TR>
	<TD colspan='6' width='374' height='21' valign='middle' style='border-left:none;border-right:solid #000000 1.1pt;border-top:none;border-bottom:none;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'><SPAN STYLE='font-size:9.0pt;font-weight:bold;line-height:160%;'>&nbsp;견적 요청자 : @@견적요청자</SPAN></P></TD>
	<TD rowspan='2' colspan='2' width='104' height='42' valign='middle' style='border-left:solid #000000 1.1pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;font-weight:bold;line-height:160%;'>사업장주소</SPAN></P></TD>
	<TD colspan='3' width='156' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@주소1&nbsp;</SPAN></P></TD>
</TR>
<TR>
	<TD colspan='6' width='374' height='21' valign='middle' style='border-left:none;border-right:solid #000000 1.1pt;border-top:none;border-bottom:none;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'><SPAN STYLE='font-size:9.0pt;font-weight:bold;line-height:160%;'>&nbsp;TEL : @@TEL</SPAN></P></TD>
	<TD colspan='3' width='156' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@주소2&nbsp;</SPAN></P></TD>
</TR>
<TR>
	<TD rowspan='2' colspan='6' width='374' height='42' valign='middle' style='border-left:none;border-right:solid #000000 1.1pt;border-top:none;border-bottom:none;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;아래와 같이 견적합니다.</SPAN></P></TD>
	<TD colspan='2' width='104' height='21' valign='middle' style='border-left:solid #000000 1.1pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;font-weight:bold;line-height:160%;'>업&nbsp; 태</SPAN></P></TD>
	<TD colspan='3' width='156' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@업태&nbsp;</SPAN></P></TD>
</TR>
<TR>
	<TD colspan='2' width='104' height='21' valign='middle' style='border-left:solid #000000 1.1pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;font-weight:bold;line-height:160%;'>업&nbsp; 종</SPAN></P></TD>
	<TD colspan='3' width='156' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@업종&nbsp;</SPAN></P></TD>
</TR>
<TR>
	<TD width='24' height='21' valign='middle' style='border-left:none;border-right:none;border-top:none;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='92' height='21' valign='middle' style='border-left:none;border-right:none;border-top:none;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='108' height='21' valign='middle' style='border-left:none;border-right:none;border-top:none;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='58' height='21' valign='middle' style='border-left:none;border-right:none;border-top:none;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='31' height='21' valign='middle' style='border-left:none;border-right:none;border-top:none;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='62' height='21' valign='middle' style='border-left:none;border-right:none;border-top:none;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:none;border-right:none;border-top:none;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
</TR>
<TR>
	<TD width='24' height='21' valign='middle' style='border-left:solid #000000 1.1pt;border-right:solid #000000 0.4pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>NO</SPAN></P></TD>
	<TD width='92' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>품목코드</SPAN></P></TD>
	<TD width='108' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>품목명</SPAN></P></TD>
	<TD width='58' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>규격</SPAN></P></TD>
	<TD width='31' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>수량</SPAN></P></TD>
	<TD width='62' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>견적단가</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>견적금액</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>견적부가세</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>견적총액</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>매출원가</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>매출이익</SPAN></P></TD>
</TR>

@@추가

		
	<TR>
	<TD colspan='4' width='281' height='21' valign='middle' style='border-left:solid #000000 1.1pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>소&nbsp; 계</SPAN></P></TD>
	<TD width='31' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt' align='right'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@합수량&nbsp;</SPAN></P></TD>
	<TD width='62' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt' align='right'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt' align='right'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@합견적금액&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt' align='right'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@합견적부가세&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt' align='right'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@합견적총액&nbsp;</SPAN></P></TD>
	<TD colspan='2' width='104' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt' align='right'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
</TR>
<TR>
	<TD width='24' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='92' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='108' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='58' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='31' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='62' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:none;border-right:none;border-top:solid #000000 1.1pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>&nbsp;</SPAN></P></TD>
</TR>
<TR>
	<TD colspan='5' width='312' height='21' valign='middle' style='border-left:solid #000000 1.1pt;border-right:solid #000000 0.4pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'><SPAN STYLE='font-size:9.0pt;font-weight:bold;line-height:160%;'>&nbsp;견적조건</SPAN></P></TD>
	<TD colspan='6' width='323' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 1.1pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'><SPAN STYLE='font-size:9.0pt;font-weight:bold;line-height:160%;'>&nbsp;견적유효기간</SPAN> : @@견적유효기간</P></TD>
</TR>
<TR>
	<TD colspan='5' width='312' height='21' valign='middle' style='border-left:solid #000000 1.1pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'>
	<SPAN STYLE='font-size:9.0pt;line-height:160%'>&nbsp;1. @@견적조건1</SPAN></P></TD>
	<TD colspan='6' width='323' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'>
	<SPAN STYLE='font-size:9.0pt;line-height:160%'>&nbsp;2. @@견적조건2</SPAN></P></TD>
</TR>
<TR>
	<TD colspan='5' width='312' height='21' valign='middle' style='border-left:solid #000000 1.1pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'>
	<SPAN STYLE='font-size:9.0pt;line-height:160%'>&nbsp;3. @@견적조건3</SPAN></P></TD>
	<TD colspan='6' width='323' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'>
	<SPAN STYLE='font-size:9.0pt;line-height:160%'>&nbsp;4. @@견적조건4</SPAN></P></TD>
</TR>
<TR>
	<TD colspan='5' width='312' height='21' valign='middle' style='border-left:solid #000000 1.1pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'><SPAN STYLE='font-size:9.0pt;font-weight:bold;line-height:160%;'>&nbsp;특이사항</SPAN></P></TD>
	<TD colspan='6' width='323' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'><SPAN STYLE='font-size:9.0pt;font-weight:'bold';line-height:160%;'>&nbsp;</SPAN></P></TD>
</TR>
<TR>
	<TD colspan='5' width='312' height='21' valign='middle' style='border-left:solid #000000 1.1pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'>
	<SPAN STYLE='font-size:9.0pt;line-height:160%'>&nbsp;1. @@특이사항1</SPAN></P></TD>
	<TD colspan='6' width='323' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'>
	<SPAN STYLE='font-size:9.0pt;line-height:160%'>&nbsp;2. @@특이사항2</SPAN></P></TD>
</TR>
<TR>
	<TD colspan='5' width='312' height='21' valign='middle' style='border-left:solid #000000 1.1pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'>
	<SPAN STYLE='font-size:9.0pt;line-height:160%'>&nbsp;3. @@특이사항3</SPAN></P></TD>
	<TD colspan='6' width='323' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 1.1pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:left;'>
	<SPAN STYLE='font-size:9.0pt;line-height:160%'>&nbsp; </SPAN></P></TD>
</TR>
</TABLE>
</P>
</BODY>
</center>";

            body = body.Replace("@@견적일자", D.GetString(rowH["DT_EST"]).Substring(0, 4) + "년 " + D.GetString(rowH["DT_EST"]).Substring(4, 2) + "월 " + D.GetString(rowH["DT_EST"]).Substring(6, 2) + "일");
            body = body.Replace("@@거래처", D.GetString(rowH["CD_PARTNER"]) + ". " + D.GetString(rowH["LN_PARTNER"]));
            body = body.Replace("@@견적요청자", D.GetString(rowH["NM_EMP_PARTNER"]));
            body = body.Replace("@@TEL", D.GetString(rowH["NO_TEL"]));
            body = body.Replace("@@사업자번호", D.GetString(rowH["NO_BIZAREA"]));
            body = body.Replace("@@상호", D.GetString(rowH["NM_BIZAREA"]));
            body = body.Replace("@@주소1", D.GetString(rowH["DC_ADS1_H"]));
            body = body.Replace("@@주소2", D.GetString(rowH["DC_ADS1_D"]));
            body = body.Replace("@@업태", D.GetString(rowH["TP_JOB"]));
            body = body.Replace("@@업종", D.GetString(rowH["CLS_JOB"]));
            body = body.Replace("@@견적유효기간", D.GetString(rowH["DT_VALID"]).Substring(0, 4) + "/" + D.GetString(rowH["DT_VALID"]).Substring(4, 2) + "/" + D.GetString(rowH["DT_VALID"]).Substring(6, 2));
            body = body.Replace("@@견적조건1", D.GetString(rowH["DC_RMK1"]));
            body = body.Replace("@@견적조건2", D.GetString(rowH["DC_RMK2"]));
            body = body.Replace("@@견적조건3", D.GetString(rowH["DC_RMK3"]));
            body = body.Replace("@@견적조건4", D.GetString(rowH["DC_RMK4"]));
            body = body.Replace("@@특이사항1", D.GetString(rowH["DC_RMK5"]));
            body = body.Replace("@@특이사항2", D.GetString(rowH["DC_RMK6"]));
            body = body.Replace("@@특이사항3", D.GetString(rowH["DC_RMK_TEXT"]));

            body = body.Replace("@@합수량", D.GetDecimal(dtL.Compute("SUM(QT_EST)", "")).ToString("###,###,##0"));
            body = body.Replace("@@합견적금액", D.GetDecimal(dtL.Compute("SUM(AM_EST)", "")).ToString("###,###,###,##0.####"));
            body = body.Replace("@@합견적부가세", D.GetDecimal(dtL.Compute("SUM(AM_VAT)", "")).ToString("###,###,###,##0.####"));
            body = body.Replace("@@합견적총액", D.GetDecimal(dtL.Compute("SUM(AM_K_EST) + SUM(AM_VAT) ", "")).ToString("###,###,##0"));
            
            foreach (DataRow rowL in dtL.Rows)
            {
                string tr = string.Empty;

                tr = GetLine();
                tr = tr.Replace("@@NO", D.GetString(++number));
                tr = tr.Replace("@@품목코드", D.GetString(rowL["CD_ITEM"]));
                tr = tr.Replace("@@품목명", D.GetString(rowL["NM_ITEM"]));
                tr = tr.Replace("@@규격", D.GetString(rowL["STND_ITEM"]));
                tr = tr.Replace("@@수량", D.GetDecimal(rowL["QT_EST"]).ToString("###,###,##0"));
                tr = tr.Replace("@@견적단가", D.GetDecimal(rowL["UM_EST"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@견적금액", D.GetDecimal(rowL["AM_EST"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@견적부가세", D.GetDecimal(rowL["AM_VAT"]).ToString("###,###,##0"));
                tr = tr.Replace("@@견적총액", (D.GetDecimal(rowL["AM_K_EST"]) + D.GetDecimal(rowL["AM_VAT"])).ToString("###,###,##0"));
                tr = tr.Replace("@@매출원가", D.GetDecimal(rowL["UM_INV_GW"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@매출이익", (D.GetDecimal(rowL["AM_EST"]) - D.GetDecimal(rowL["UM_INV_GW"])).ToString("###,###,###,##0.####"));
                line += tr;
            }

            body = body.Replace("@@추가", line);

            return body;
        }
        #endregion

        private string GetLine()
        {
            string tr = @"
		          <TR>
	<TD width='24' height='21' valign='middle' style='border-left:solid #000000 1.1pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@NO&nbsp;</SPAN></P></TD>
	<TD width='92' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@품목코드&nbsp;</SPAN></P></TD>
	<TD width='108' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@품목명&nbsp;</SPAN></P></TD>
	<TD width='58' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:center;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@규격&nbsp;</SPAN></P></TD>
	<TD width='31' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:right;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@수량&nbsp;</SPAN></P></TD>
	<TD width='62' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:right;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@견적단가&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:right;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@견적금액&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:right;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@견적부가세&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:right;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@견적총액&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 0.4pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:right;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@매출원가&nbsp;</SPAN></P></TD>
	<TD width='52' height='21' valign='middle' style='border-left:solid #000000 0.4pt;border-right:solid #000000 1.1pt;border-top:solid #000000 0.4pt;border-bottom:solid #000000 0.4pt;padding:0.0pt 0.0pt 0.0pt 0.0pt'>
	<P CLASS=HStyle0 STYLE='text-align:right;'><SPAN STYLE='font-size:9.0pt;line-height:160%;'>@@매출이익&nbsp;</SPAN></P></TD>
</TR>";

            return tr;
        }
        
        #region ♣ 전자결재
        #region -> 결재상신_개발서버
        bool 결재상신_개발서버(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("UP_SA_GWDOCU_DUZON", obj);
        }
        #endregion

        #region -> 결재상신_업체

        bool 결재상신_업체(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("UP_SA_GWDOCU", obj);
        }
        #endregion
        #endregion
    }
    
}

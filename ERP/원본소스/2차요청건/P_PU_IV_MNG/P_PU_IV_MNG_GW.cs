using System;
using System.Collections.Generic;
using System.Text;
using Duzon.ERPU;
using System.Data;
using Duzon.ERPU.MF.Common;
using System.IO;
using System.Windows.Forms;

namespace pur
{
    class P_PU_IV_MNG_GW
    {
        public string[] getGwSearch(string str, DataRow[] drH, DataRow[] drL)
        {

            string App_Form_Kind = string.Empty;
            string GW_URL = string.Empty;

            string[] strs = new string[4];

            switch (str)    //시스템 통제설정에 따라 업체마다 파일 이름과 문서번호와 업체 URL 경로를 셋팅한다.
            {

                case "023": //CIS
                    App_Form_Kind = "4000";
                    GW_URL = "http://gw.cisro.co.kr/kor_webroot/src/cm/tims/index.aspx?";
                    break;
                case "050": //CNP
                    App_Form_Kind = "2000";
                    GW_URL = BASIC.GetMAENV("GROUPWARE_URL") + "/kor_webroot/src/cm/tims/index.aspx?";
                    break;
                default:
                    return null;
            }

            switch (str)    //시스템 통제설정에 따라 업체마다 파일을 만들어주는곳이다.
            {

                case "023": //NTS
                    strs[0] = GW_CIS_html( drH,  drL);   //HTML 을 가공한 Contents
                    strs[3] = /*D.GetString(dt.Rows[0]["NM_PROJECT"]) + "&nbsp" + D.GetString(dt.Rows[0]["LN_PARTNER"]) + "&nbsp" + "발주서 입니다"*/"";
                    break;
                case "050": //CNP
                    strs[0] = GetCnpWebService(drH, drL);
                    break;
                default:
                    return null;
            }

            strs[1] = App_Form_Kind;
            strs[2] = GW_URL;

            return strs;
        }


        private string GW_CIS_html(DataRow[] drH, DataRow[] drL)
        {
            string html_source = string.Empty;
            string html_source_LINE = string.Empty;
            string dt_process = string.Empty;
            string dt_pay_prearranged = string.Empty;


            #region  thml 양식
            html_source += @"
                                <HTML><HEAD><META content='text/html; charset=utf-8' http-equiv=Content-Type><STYLE>P {	MARGIN-TOP: 1px; MARGIN-BOTTOM: 1px} BLOCKQUOTE {MARGIN-TOP: 1px; MARGIN-BOTTOM: 1px}</STYLE></HEAD><HTML xmlns:v = 'urn:schemas-microsoft-com:vml' xmlns:o = 'urn:schemas-microsoft-com:office:office' xmlns:w = 'urn:schemas-microsoft-com:office:word' xmlns:m = 'http://schemas.microsoft.com/office/2004/12/omml'><HEAD><TITLE>HTML Document</TITLE>
                                <META content='text/html; charset=utf-8' http-equiv=Content-Type>
                                <STYLE>P {
	                                MARGIN-TOP: 1px; MARGIN-BOTTOM: 1px
                                }
                                BLOCKQUOTE {
	                                MARGIN-TOP: 1px; MARGIN-BOTTOM: 1px
                                }
                                </STYLE>

                                <STYLE>P {	MARGIN-TOP: 1px; MARGIN-BOTTOM: 1px}BLOCKQUOTE {	MARGIN-TOP: 1px; MARGIN-BOTTOM: 1px}</STYLE>

                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=GENERATOR content='MSHTML 8.00.6001.19258'>
                                <META name=Originator content='Microsoft Word 14'><LINK rel=File-List href='_dstmp0015066e.files/filelist.xml'><LINK rel=themeData href='_dstmp0015066e.files/themedata.thmx'><LINK rel=colorSchemeMapping href='_dstmp0015066e.files/colorschememapping.xml'>
                                <STYLE>@font-face {	font-family: 굴림;}@font-face {	font-family: 굴림;}@font-face {	font-family: @굴림;}@page  {mso-footnote-separator: url('_dstmp0015066e.files/header.htm') fs; mso-footnote-continuation-separator: url('_dstmp0015066e.files/header.htm') fcs; mso-endnote-separator: url('_dstmp0015066e.files/header.htm') es; mso-endnote-continuation-separator: url('_dstmp0015066e.files/header.htm') ecs; }@page WordSection1 {size: 595.3pt 841.9pt; margin: 3.0cm 72.0pt 72.0pt 72.0pt; mso-header-margin: 42.55pt; mso-footer-margin: 49.6pt; mso-paper-source: 0; }P.MsoNormal {	MARGIN: 0cm 0cm 0pt; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-style-unhide: no; mso-style-qformat: yes; mso-style-parent: ''; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림}LI.MsoNormal {	MARGIN: 0cm 0cm 0pt; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-style-unhide: no; mso-style-qformat: yes; mso-style-parent: ''; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림}DIV.MsoNormal {	MARGIN: 0cm 0cm 0pt; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-style-unhide: no; mso-style-qformat: yes; mso-style-parent: ''; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림}P.MsoHeader {	MARGIN: 0cm 0cm 0pt; LAYOUT-GRID-MODE: char; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: '머리글 Char'; tab-stops: center 225.65pt right 451.3pt}LI.MsoHeader {	MARGIN: 0cm 0cm 0pt; LAYOUT-GRID-MODE: char; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: '머리글 Char'; tab-stops: center 225.65pt right 451.3pt}DIV.MsoHeader {	MARGIN: 0cm 0cm 0pt; LAYOUT-GRID-MODE: char; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: '머리글 Char'; tab-stops: center 225.65pt right 451.3pt}P.MsoFooter {	MARGIN: 0cm 0cm 0pt; LAYOUT-GRID-MODE: char; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: '바닥글 Char'; tab-stops: center 225.65pt right 451.3pt}LI.MsoFooter {	MARGIN: 0cm 0cm 0pt; LAYOUT-GRID-MODE: char; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: '바닥글 Char'; tab-stops: center 225.65pt right 451.3pt}DIV.MsoFooter {	MARGIN: 0cm 0cm 0pt; LAYOUT-GRID-MODE: char; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: '바닥글 Char'; tab-stops: center 225.65pt right 451.3pt}P {	MARGIN: 1pt 0cm; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림; mso-style-priority: 99}SPAN.Char {	FONT-FAMILY: 굴림; mso-style-unhide: no; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: 머리글; mso-style-name: '머리글 Char'; mso-style-locked: yes; mso-ansi-font-size: 12.0pt; mso-bidi-font-size: 12.0pt; mso-ascii-font-family: 굴림; mso-fareast-font-family: 굴림; mso-hansi-font-family: 굴림}SPAN.Char0 {	FONT-FAMILY: 굴림; mso-style-unhide: no; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: 바닥글; mso-style-name: '바닥글 Char'; mso-style-locked: yes; mso-ansi-font-size: 12.0pt; mso-bidi-font-size: 12.0pt; mso-ascii-font-family: 굴림; mso-fareast-font-family: 굴림; mso-hansi-font-family: 굴림}SPAN.GramE {	mso-style-name: ''; mso-gram-e: yes}.MsoChpDefault {	mso-bidi-font-size: 10.0pt; mso-ascii-font-family: 'Times New Roman'; mso-hansi-font-family: 'Times New Roman'; mso-style-type: export-only; mso-default-props: yes; mso-font-kerning: 0pt}DIV.WordSection1 {	page: WordSection1}</STYLE>

                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler>
                                <META name=DuzonNewturns content=DocuStyler></HEAD>
                                <BODY style='FONT-FAMILY: 돋움; FONT-SIZE: 9pt; tab-interval: 40.0pt' lang=KO>
                                <DIV class=WordSection1>
                                <TABLE style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; WIDTH: 477.75pt; BORDER-COLLAPSE: collapse; BORDER-TOP: medium none; BORDER-RIGHT: medium none; mso-border-alt: solid windowtext .5pt; mso-yfti-tbllook: 1184; mso-padding-alt: 0cm 5.4pt 0cm 5.4pt' class=MsoNormalTable border=1 cellSpacing=0 cellPadding=0 width=637>
                                <TBODY>
                                <TR style='HEIGHT: 17.25pt; mso-yfti-irow: 0; mso-yfti-firstrow: yes'>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 79.55pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt' width=106 colSpan=2>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>사용부서<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 164.95pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' width=220 colSpan=7>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US><o:p>@@사용부서&nbsp;</o:p></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 21.5pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' rowSpan=4 width=29>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>기<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 10pt' lang=EN-US><o:p>&nbsp;</o:p></SPAN></P>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>타<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 85.25pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' width=114 colSpan=3>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>지출요청일자<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 126.5pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' width=169 colSpan=3>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>참조부서<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD></TR>
                                <TR style='HEIGHT: 17.25pt; mso-yfti-irow: 1'>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 79.55pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=106 colSpan=2>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>결제방법<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 164.95pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=220 colSpan=7>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>@@결제방법</SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 85.25pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=114 colSpan=3>
                                <P style='TEXT-ALIGN: center' align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US>@@지출요청일자&nbsp;<o:p></o:p></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 126.5pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=169 colSpan=3>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US><o:p>&nbsp;</o:p></SPAN></P></TD></TR>
                                <TR style='HEIGHT: 17.25pt; mso-yfti-irow: 2'>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 79.55pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' rowSpan=2 width=106 colSpan=2>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>계좌번호<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 57.45pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=77 colSpan=2>
                                <P style='TEXT-ALIGN: right' class=MsoNormal align=right><SPAN style='FONT-SIZE: 9pt'>@@은행은행<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 107.5pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=143 colSpan=5>
                                <P style='TEXT-ALIGN: left' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>&nbsp;예금주<SPAN lang=EN-US>: @@예금주<o:p></o:p></SPAN></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 85.25pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=114 colSpan=3>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>신용카드번호<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 126.5pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=169 colSpan=3>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>회계부서전표등록<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD></TR>
                                <TR style='HEIGHT: 17.25pt; mso-yfti-irow: 3'>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 164.95pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=220 colSpan=7>
                                <P style='TEXT-ALIGN: center' align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US>@@계좌번호<o:p></o:p></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 85.25pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=114 colSpan=3>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US><o:p>&nbsp;</o:p></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 126.5pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=169 colSpan=3>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US><o:p>@@회계</o:p></SPAN></P></TD></TR>
                                <TR style='HEIGHT: 17.25pt; mso-yfti-irow: 4'>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 79.55pt; PADDING-RIGHT: 5.4pt; BACKGROUND: #eee7df; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=106 colSpan=2>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>사용일자<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 77.25pt; PADDING-RIGHT: 5.4pt; BACKGROUND: #eee7df; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=103 colSpan=3>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN class=GramE><SPAN style='FONT-SIZE: 9pt'>사<SPAN lang=EN-US><SPAN style='mso-spacerun: yes'>&nbsp; </SPAN></SPAN>용</SPAN></SPAN><SPAN style='FONT-SIZE: 9pt' lang=EN-US><SPAN style='mso-spacerun: yes'>&nbsp; </SPAN></SPAN><SPAN style='FONT-SIZE: 9pt'>처<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 145.15pt; PADDING-RIGHT: 5.4pt; BACKGROUND: #eee7df; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=194 colSpan=6>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>사용내역<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 92.9pt; PADDING-RIGHT: 5.4pt; BACKGROUND: #eee7df; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=124 colSpan=3>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN class=GramE><SPAN style='FONT-SIZE: 9pt'>공급가액<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 82.9pt; PADDING-RIGHT: 5.4pt; BACKGROUND: #eee7df; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=111 colSpan=2>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>부가세<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD></TR>
                                <TR style='HEIGHT: 17.25pt; mso-yfti-irow: 5'>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 79.55pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=106 colSpan=2>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US>@@사용일자&nbsp;<o:p></o:p></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 77.25pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=103 colSpan=3>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US>@@사용처&nbsp;</SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 145.15pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=194 colSpan=6>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US><o:p>@@사용내역&nbsp;</o:p></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 92.9pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=124 colSpan=3>
                                <P style='TEXT-ALIGN: right' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US>@@금액&nbsp;<o:p></o:p></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 82.9pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=111 colSpan=2>
                                <P style='TEXT-ALIGN: right' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US>@@부가세&nbsp;<o:p></o:p></SPAN></P></TD></TR>

                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 301.95pt; PADDING-RIGHT: 5.4pt; BACKGROUND: #eee7df; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=403 colSpan=11>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>합<SPAN lang=EN-US><SPAN style='mso-spacerun: yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </SPAN></SPAN>계<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 92.9pt; PADDING-RIGHT: 5.4pt; BACKGROUND: #eee7df; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=124 colSpan=5>
                                <P style='TEXT-ALIGN: right' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US>\ @@총금액<o:p></o:p></SPAN></P></TD></TR>
                                <TR style='HEIGHT: 78.55pt; mso-yfti-irow: 14'>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 477.75pt; PADDING-RIGHT: 5.4pt; HEIGHT: 78.55pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=637 colSpan=16>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>위 금액을 청구하오니 지급 바랍니다<SPAN lang=EN-US>.<o:p></o:p></SPAN></SPAN></P>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US><o:p>&nbsp;</o:p></SPAN></P>
                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US><o:p>&nbsp;</o:p></SPAN></P>
                                <P class=MsoNormal><SPAN style='FONT-SIZE: 9pt'>※<SPAN class=GramE>첨부서류<SPAN lang=EN-US> :</SPAN></SPAN><SPAN lang=EN-US> </SPAN>품의서 사본<SPAN lang=EN-US>,</SPAN>카드영수증<SPAN lang=EN-US>,</SPAN>간이영수증<SPAN lang=EN-US>(3</SPAN>만원이상 지출시에는 세금계산서 첨부<SPAN lang=EN-US>)<o:p></o:p></SPAN></SPAN></P>
                                <P class=MsoNormal><SPAN style='FONT-SIZE: 9pt' lang=EN-US><SPAN style='mso-spacerun: yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </SPAN></SPAN><SPAN style='FONT-SIZE: 9pt'>출장여비정산서 사본<SPAN lang=EN-US>(</SPAN>국내<SPAN class=GramE><SPAN lang=EN-US>,</SPAN>해외</SPAN> 출장자<SPAN lang=EN-US>)<o:p></o:p></SPAN></SPAN></P></TD></TR>
                                <TR style='HEIGHT: 66.95pt; mso-yfti-irow: 15'>
                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 477.75pt; PADDING-RIGHT: 5.4pt; HEIGHT: 66.95pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' vAlign=top width=637 colSpan=16>
                                <P style='TEXT-JUSTIFY: inter-ideograph; TEXT-ALIGN: justify' class=MsoNormal><SPAN style='FONT-SIZE: 9pt'>※특기사항 <BR>@@특기사항</BR><SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD></TR>
                                <TR style='mso-yfti-irow: 16'>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 42pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=56></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 37.55pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=50></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 25.5pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=34></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 65.8pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=88 colSpan=3></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 30.95pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=41></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 31.6pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=42></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 79.65pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=106 colSpan=4></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 103.6pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=138 colSpan=3></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 61.1pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=81></TD></TR>
                                <TR style='mso-yfti-irow: 17; mso-yfti-lastrow: yes'>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 42pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=56></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 37.55pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=50></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 25.5pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=34></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 31.95pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=43></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 19.8pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=26></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 14.05pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=19></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 30.95pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=41></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 31.6pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=42></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 11.1pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=15></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 21.5pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=29></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 35.95pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=48></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 11.1pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=15></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 38.2pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=51></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 43.6pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=58></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 21.8pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=29></TD>
                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 61.1pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=81></TD></TR></TBODY></TABLE>
                                <P class=MsoNormal><SPAN style='FONT-SIZE: 10pt' lang=EN-US><o:p>&nbsp;</o:p></SPAN></P></DIV></BODY></HTML></HTML>
                                            ";
            #endregion

            if( D.GetString(drH[0]["DT_PAY_PREARRANGED"]) != string.Empty)
                dt_pay_prearranged = D.GetString(drH[0]["DT_PAY_PREARRANGED"]).Substring(0, 4) + "/" + D.GetString(drH[0]["DT_PAY_PREARRANGED"]).Substring(4, 2) + "/" + D.GetString(drH[0]["DT_PAY_PREARRANGED"]).Substring(6, 2);

            if (D.GetString(drH[0]["DT_PROCESS"]) != string.Empty)
                dt_process = D.GetString(drH[0]["DT_PROCESS"]).Substring(0, 4) + "/" + D.GetString(drH[0]["DT_PROCESS"]).Substring(4, 2) + "/" + D.GetString(drH[0]["DT_PROCESS"]).Substring(6, 2);



            html_source = html_source.Replace("@@사용부서", D.GetString(drH[0]["NM_DEPT"]) + "&nbsp;");
            html_source = html_source.Replace("@@지출요청일자", dt_pay_prearranged + "&nbsp;");
            html_source = html_source.Replace("@@사용일자", dt_process + "&nbsp;");
            html_source = html_source.Replace("@@사용처", D.GetString(drH[0]["LN_PARTNER"]) + "&nbsp;");

            if (drL.Length > 1)
                html_source = html_source.Replace("@@사용내역", D.GetString(drL[0]["NM_ITEM"]) + "&nbsp;외" + "&nbsp;");
            else
                html_source = html_source.Replace("@@사용내역", D.GetString(drL[0]["NM_ITEM"]) + "&nbsp;");

            html_source = html_source.Replace("@@총금액", D.GetDecimal(drH[0]["AM_SUP"]).ToString("###,###,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@부가세", D.GetDecimal(drH[0]["VAT_TAX"]).ToString("###,###,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@금액", D.GetDecimal(drH[0]["AM_K"]).ToString("###,###,###,###,##0.####") + "&nbsp;");

            html_source = html_source.Replace("@@결제방법", D.GetString(drH[0]["NM_FG_PAYBILL"]) + "&nbsp;");
            html_source = html_source.Replace("@@특기사항", D.GetString(drH[0]["DC_RMK"]) + "&nbsp;");
            html_source = html_source.Replace("@@은행", D.GetString(drH[0]["NM_BANK"]) + "&nbsp;");
            html_source = html_source.Replace("@@예금주", D.GetString(drH[0]["NM_DEPOSIT"]) + "&nbsp;");
            html_source = html_source.Replace("@@회계", D.GetString(drH[0]["TP_AIS"]) + "&nbsp;");
            html_source = html_source.Replace("@@계좌번호", D.GetString(drH[0]["NO_DEPOSIT"]) + "&nbsp;");
            return html_source;


        }

        #region -> CNP전용
        private string GetCnpWebService(DataRow[] drH, DataRow[] drL)
        {
            DataRow rowH = drH[0];

            // 매입번호
            string ERP_PKEY = D.GetString(rowH["NO_IV"]);
            // 사번
            string SABUN = D.GetString(MA.Login.사원번호);
            // 구매외주구분(구매 : 1, 외주  :2)
            string FORM_TYPE = "1";
            // 지급금액
            string JI_PAY = GetAmtFormat(rowH["AM_SUP"]);
            // 지급금액 한글
            string KO_PAY = "일금 " + GetHangulAmt(D.GetInt64(rowH["AM_SUP"])) + "원정";
            // 거래처명
            string PARTNER = D.GetString(rowH["LN_PARTNER"]);
            // 지급요청일
            string REQ_DATE = getDayFormat(D.GetString(rowH["DT_PAY_PREARRANGED"]));
            // 지급조건
            string REQ_CONDITION = D.GetString(rowH["NM_FG_PAYBILL"]);
            // 은행
            string BANK_NM = D.GetString(drH[0]["NM_BANK"]);
            // 계좌번호
            string BANK_NO = D.GetString(drH[0]["NO_DEPOSIT"]);
            // 예금주
            string BANK_NAME = D.GetString(drH[0]["NM_DEPOSIT"]);
            // 연락처
            string PHONE_NO = D.GetString(drH[0]["NO_TEL"]);

            string dt_io, nm_item, qt_rcv_cls, um_item_cls, am_cls, vat, am_sum;

            WebServiceCNP.FICNPPurchaseRequestprovidersCNPPurchaseRequest_Provider cnp = new pur.WebServiceCNP.FICNPPurchaseRequestprovidersCNPPurchaseRequest_Provider();
            List<WebServiceCNP.ITEMS> list = new List<pur.WebServiceCNP.ITEMS>();

            // 매입내역
            for (int i = 0; i < drL.Length; i++)
            {
                // 날짜
                dt_io = getDayFormat(D.GetString(drL[i]["DT_IO"]));
                // 제품명
                nm_item = D.GetString(drL[i]["NM_ITEM"]);
                // 수량
                qt_rcv_cls = D.GetDecimal(drL[i]["QT_RCV_CLS"]).ToString("#,###,###,###.####");
                // 단가
                um_item_cls = D.GetDecimal(drL[i]["UM_ITEM_CLS"]).ToString("###,###,###,###.####");
                // 매입액
                am_cls = D.GetDecimal(drL[i]["AM_CLS"]).ToString("###,###,###,###.####");
                // 부가세
                vat = D.GetDecimal(drL[i]["VAT"]).ToString("###,###,###,###.####");
                // 합계
                am_sum = D.GetDecimal(drL[i]["AM_TOTAL"]).ToString("###,###,###,###.####");

                WebServiceCNP.ITEMS items = new pur.WebServiceCNP.ITEMS();
                items.ARR_DATE = dt_io;
                items.ARR_PRODUCTION = nm_item;
                items.ARR_AMT = qt_rcv_cls;
                items.ARR_UNIT = um_item_cls;
                items.ARR_PAY = am_cls;
                items.ARR_VAT = vat;
                items.ARR_SUM = am_sum;

                list.Add(items);
            }
            // 합계라인
            if (drL.Length > 0)
            {
                dt_io = "합 계";
                qt_rcv_cls = D.GetDecimal(drH[0]["QT_RCV_CLS"]).ToString("#,###,###,###.####");
                am_cls = D.GetDecimal(drH[0]["AM_K"]).ToString("###,###,###,###.####");
                vat = D.GetDecimal(drH[0]["VAT_TAX"]).ToString("###,###,###,###.####");
                am_sum = D.GetDecimal(drH[0]["AM_SUP"]).ToString("###,###,###,###.####");

                WebServiceCNP.ITEMS items = new pur.WebServiceCNP.ITEMS();
                items.ARR_DATE = dt_io;
                items.ARR_PRODUCTION = "";
                items.ARR_AMT = qt_rcv_cls;
                items.ARR_UNIT = "";
                items.ARR_PAY = am_cls;
                items.ARR_VAT = vat;
                items.ARR_SUM = am_sum;

                list.Add(items);
            }
            string RECODE;
            string resultmsg = cnp.CNPPurchaseRequest_TgtGW_FL(ERP_PKEY, SABUN, FORM_TYPE, JI_PAY, KO_PAY, PARTNER, REQ_DATE, REQ_CONDITION, BANK_NM, BANK_NO, BANK_NAME, PHONE_NO, list.ToArray(),out RECODE); 

            if(resultmsg == "00")
                return resultmsg;
            else
                return RECODE;
        }

        private string GetCnpHtml(DataRow[] drH, DataRow[] drL)
        {
            string htmlForm = string.Empty;
            string downForm = string.Empty;
            string htmlLine = string.Empty;

            downForm = Application.StartupPath + "\\download\\gw\\HT_PU_Z_CNP_IV_MNG.htm";
            htmlForm = File.ReadAllText(downForm, System.Text.UTF8Encoding.UTF8);

            string dt_io, nm_item, qt_rcv_cls, um_item_cls, am_cls, vat, am_sum;

            //매입거래처정보 
            htmlForm = htmlForm.Replace("@@AM_SUM", D.GetDecimal(drH[0]["AM_SUP"]).ToString("###,###,###,###.####"));
            htmlForm = htmlForm.Replace("@@AM_HAN", "일금 " + GetHangulAmt(D.GetInt64(drH[0]["AM_SUP"])) + "원정");
            htmlForm = htmlForm.Replace("@@LN_PARTNER", D.GetString(drH[0]["LN_PARTNER"]));
            htmlForm = htmlForm.Replace("@@DT_PAY", getDayFormat(D.GetString(drH[0]["DT_PAY_PREARRANGED"])));
            htmlForm = htmlForm.Replace("@@NM_FG_PAYBILL", D.GetString(drH[0]["NM_FG_PAYBILL"]));
            htmlForm = htmlForm.Replace("@@NM_BANK", D.GetString(drH[0]["NM_BANK"]));
            htmlForm = htmlForm.Replace("@@NO_DEPOSIT", D.GetString(drH[0]["NO_DEPOSIT"]));
            htmlForm = htmlForm.Replace("@@NM_DEPOSIT", D.GetString(drH[0]["NM_DEPOSIT"]));
            htmlForm = htmlForm.Replace("@@NO_TEL", D.GetString(drH[0]["NO_TEL"]));

            //매입내역
            for (int i = 0; i < drL.Length; i++)
            {
                dt_io = getDayFormat(D.GetString(drL[i]["DT_IO"]));
                nm_item = D.GetString(drL[i]["NM_ITEM"]);
                qt_rcv_cls = D.GetDecimal(drL[i]["QT_RCV_CLS"]).ToString("#,###,###,###.####");
                um_item_cls = D.GetDecimal(drL[i]["UM_ITEM_CLS"]).ToString("###,###,###,###.####");
                am_cls = D.GetDecimal(drL[i]["AM_CLS"]).ToString("###,###,###,###.####");
                vat = D.GetDecimal(drL[i]["VAT"]).ToString("###,###,###,###.####");
                am_sum = D.GetDecimal(drL[i]["AM_TOTAL"]).ToString("###,###,###,###.####");

                htmlLine += @"<tr height='25'>              
                        <td  class ='a3'>" + dt_io + @"&nbsp;</td>
                        <td  class ='a1'>" + nm_item + @"&nbsp;</td>
                        <td  class ='a2'>" + qt_rcv_cls + @"&nbsp;</td>
                        <td  class ='a2'>" + um_item_cls + @"&nbsp;</td>	
                        <td  class ='a2'>" + am_cls + @"&nbsp;</td>	
                        <td  class ='a2'>" + vat + @"&nbsp;</td>	
                        <td  class ='a2'>" + am_sum + @"&nbsp;</td>		
                        </tr>";
            }

            if (drL.Length > 0)
            {
                dt_io = "합 계";
                qt_rcv_cls = D.GetDecimal(drH[0]["QT_RCV_CLS"]).ToString("#,###,###,###.####");
                am_cls = D.GetDecimal(drH[0]["AM_K"]).ToString("###,###,###,###.####");
                vat = D.GetDecimal(drH[0]["VAT_TAX"]).ToString("###,###,###,###.####");
                am_sum = D.GetDecimal(drH[0]["AM_SUP"]).ToString("###,###,###,###.####");

                htmlLine += @"<tr height='25'>              
                        <td  class ='a3'>" + dt_io + @"&nbsp;</td>
                        <td  class ='a1'>" + "" + @"&nbsp;</td>
                        <td  class ='a2'>" + qt_rcv_cls + @"&nbsp;</td>
                        <td  class ='a2'>" + "" + @"&nbsp;</td>	
                        <td  class ='a2'>" + am_cls + @"&nbsp;</td>	
                        <td  class ='a2'>" + vat + @"&nbsp;</td>	
                        <td  class ='a2'>" + am_sum + @"&nbsp;</td>		
                        </tr>";
            }

            htmlForm = htmlForm.Replace("@@LINEDATA", htmlLine);

            return htmlForm;
        }
        #endregion

        #region -> GetHangulAmt(금액->한글로 변환)
        private string GetHangulAmt(decimal nAmt)
        {
            int nCount, i;
            string sAmt = null, cAmt = null;

            string[] dan_hangul = { "", "", "십", "백", "천", "만", "십", "백", "천", "억", "십", "백", "천", "조", "십", "백" };
            string[] digit_hangul = { "", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구" };

            if (nAmt < 0)
                sAmt = nAmt.ToString().Substring(1);
            else
                sAmt = nAmt.ToString();

            nCount = sAmt.Length;

            i = 1;

            if (nCount < 1)
                return "0";

            if (nCount == 1)
            {
                cAmt = digit_hangul[int.Parse(sAmt)];
                return cAmt;
            }

            if (nCount > 15 || sAmt.Substring(1, 1) == ".")
            {
                MessageBox.Show("확인! 백조단위가 넘습니다.");
                return "";
            }

            bool IsMan = false;
            bool IsUk = false;
            bool IsCho = false;

            if (nCount > 5)
            {
                if (decimal.Parse(sAmt.Substring(nCount - 5, 2)) < 10)
                    IsMan = true;
            }
            if (nCount > 9)
            {
                if (decimal.Parse(sAmt.Substring(nCount - 9, 2)) < 10)
                    IsUk = true;
            }
            if (nCount > 13)
            {
                if (decimal.Parse(sAmt.Substring(nCount - 13, 2)) < 10)
                    IsCho = true;
            }

            for (i = 0; i < nCount; i++)
            {
                if (sAmt.Substring(i, 1) != "0")
                {
                    if (nCount - i == 12)
                    {
                        if (IsCho)
                            cAmt = cAmt + dan_hangul[nCount - i + 1];
                    }

                    if (nCount - i == 8)
                    {
                        if (IsUk)
                            cAmt = cAmt + dan_hangul[nCount - i + 1];
                    }

                    if (nCount - i == 4)
                    {
                        if (IsMan)
                            cAmt = cAmt + dan_hangul[nCount - i + 1];
                    }

                    cAmt = cAmt + digit_hangul[int.Parse(sAmt.Substring(i, 1))];
                    if (nCount - i > 0)
                        cAmt = cAmt + dan_hangul[nCount - i];
                }
                else
                {
                    int nCnt = nCount - i;
                    if (nCnt == 4 || nCnt == 8 || nCnt == 12)
                    {
                        if ((cAmt.Substring(cAmt.Length - 1, 1) != "만") && (cAmt.Substring(cAmt.Length - 1, 1) != "억") && (cAmt.Substring(cAmt.Length - 1, 1) != "조"))
                            cAmt = cAmt + dan_hangul[nCount - i + 1];
                    }
                }
            }

            if (nAmt < 0)
                return "-" + cAmt;

            return cAmt;
        }
        #endregion

        #region -> getDayFormat
        private string getDayFormat(string strDay)
        {
            if (strDay.Length == 0) return null;

            string retDay = string.Empty;

            if (strDay.Length >= 8)
                retDay = strDay.Substring(0, 4) + "." + strDay.Substring(4, 2) + "." + strDay.Substring(6, 2);

            return retDay;
        }
        #endregion

        string GetAmtFormat(object val)
        {
            decimal str = Convert.ToDecimal(val);

            return str.ToString("#,###,###,###.####");
        }

        #region -> 결재를 올릴지 안올릴지 결정하는 메소드
        public bool save_TF(int st_stat)
        {
            bool TF_gw = false;

            if (st_stat == 4) st_stat = -1;

            if (st_stat == 0 || st_stat == 1)
                TF_gw = false;
            else if (st_stat == -1 || st_stat == 2 || st_stat == 3 || st_stat == 999) //999 : 전자결재 한번도 올리지않은상태
                TF_gw = true;

            return TF_gw;
        }
        #endregion
    }
}

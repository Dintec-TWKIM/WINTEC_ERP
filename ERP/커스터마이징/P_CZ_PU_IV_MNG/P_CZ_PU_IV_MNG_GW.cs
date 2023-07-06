using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.ERPU.MF.Common;
using System.IO;
using System.Windows.Forms;
using Duzon.ERPU;
using Duzon.Common.Forms;

namespace cz
{
    internal class P_CZ_PU_IV_MNG_GW
    {
        private P_CZ_PU_IV_MNG_BIZ _biz = new P_CZ_PU_IV_MNG_BIZ();

        public string[] getGwSearch(string str, DataRow[] drH, DataRow[] drL)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string[] strArray = new string[4];
            string str4;
            string str5;

            switch (str)
            {
                case "023":
                    str4 = "4000";
                    str5 = "http://gw.cisro.co.kr/kor_webroot/src/cm/tims/index.aspx?";
                    break;
                case "050":
                    str4 = "2000";
                    str5 = BASIC.GetMAENV("GROUPWARE_URL") + "/kor_webroot/src/cm/tims/index.aspx?";
                    break;
                case "055":
                    str4 = "1008";
                    str5 = BASIC.GetMAENV("GROUPWARE_URL") + "/kor_webroot/src/cm/tims/index.aspx?";
                    str1 = "HT_PU_IV_MNG_DYPNF";
                    break;
                case "066":
                    str4 = "1000";
                    str5 = BASIC.GetMAENV("GROUPWARE_URL") + "/kor_webroot/src/cm/tims/index.aspx?";
                    str1 = "HT_PU_IV_MNG_KORR";
                    break;
                case "065":
                    str4 = "1007";
                    str5 = BASIC.GetMAENV("GROUPWARE_URL") + "/kor_webroot/src/cm/tims/index.aspx?";
                    str1 = "HT_PU_IV_MNG_DMENG";
                    break;
                case "068":
                    str4 = "1008";
                    str5 = BASIC.GetMAENV("GROUPWARE_URL") + "/kor_webroot/src/cm/tims/index.aspx?";
                    str1 = "HT_PU_IV_MNG_SGA";
                    break;
                case "069":
                    str4 = "1111";
                    str5 = BASIC.GetMAENV("GROUPWARE_URL") + "/kor_webroot/src/cm/tims/index.aspx?";
                    str1 = "HT_PU_IV_MNG_YGENT";
                    break;
                default:
                    return null;
            }

            string str6 = string.Empty;
            string html_source = string.Empty;

            if (str1 != string.Empty)
                html_source = File.ReadAllText(Application.StartupPath + "\\download\\gw\\" + str1 + ".htm", Encoding.UTF8);
            
            switch (str)
            {
                case "023":
                    strArray[0] = this.GW_CIS_html(drH, drL);
                    strArray[3] = "";
                    break;
                case "050":
                    break;
                case "055":
                    strArray[0] = this.GW_DYPNF_html(html_source, D.GetString(drH[0]["NO_IV"]));
                    break;
                case "066":
                    strArray[0] = this.GW_KORR_html(html_source, drH, drL);
                    break;
                case "065":
                    strArray[0] = this.GW_DMENG_html(html_source, D.GetString(drH[0]["NO_IV"]));
                    break;
                case "068":
                    strArray[0] = this.GW_SGA_html(html_source, D.GetString(drH[0]["NO_IV"]));
                    break;
                case "069":
                    strArray[0] = this.GW_YGENT_html(html_source, D.GetString(drH[0]["NO_IV"]));
                    break;
                default:
                    return null;
            }

            strArray[1] = str4;
            strArray[2] = str5;

            return strArray;
        }

        private string GW_CIS_html(DataRow[] drH, DataRow[] drL)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            string str4 = string.Empty;
            string str5 = str1 + "\r\n                                <HTML><HEAD><META content='text/html; charset=utf-8' http-equiv=Content-Type><STYLE>P {\tMARGIN-TOP: 1px; MARGIN-BOTTOM: 1px} BLOCKQUOTE {MARGIN-TOP: 1px; MARGIN-BOTTOM: 1px}</STYLE></HEAD><HTML xmlns:v = 'urn:schemas-microsoft-com:vml' xmlns:o = 'urn:schemas-microsoft-com:office:office' xmlns:w = 'urn:schemas-microsoft-com:office:word' xmlns:m = 'http://schemas.microsoft.com/office/2004/12/omml'><HEAD><TITLE>HTML Document</TITLE>\r\n                                <META content='text/html; charset=utf-8' http-equiv=Content-Type>\r\n                                <STYLE>P {\r\n\t                                MARGIN-TOP: 1px; MARGIN-BOTTOM: 1px\r\n                                }\r\n                                BLOCKQUOTE {\r\n\t                                MARGIN-TOP: 1px; MARGIN-BOTTOM: 1px\r\n                                }\r\n                                </STYLE>\r\n\r\n                                <STYLE>P {\tMARGIN-TOP: 1px; MARGIN-BOTTOM: 1px}BLOCKQUOTE {\tMARGIN-TOP: 1px; MARGIN-BOTTOM: 1px}</STYLE>\r\n\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=GENERATOR content='MSHTML 8.00.6001.19258'>\r\n                                <META name=Originator content='Microsoft Word 14'><LINK rel=File-List href='_dstmp0015066e.files/filelist.xml'><LINK rel=themeData href='_dstmp0015066e.files/themedata.thmx'><LINK rel=colorSchemeMapping href='_dstmp0015066e.files/colorschememapping.xml'>\r\n                                <STYLE>@font-face {\tfont-family: 굴림;}@font-face {\tfont-family: 굴림;}@font-face {\tfont-family: @굴림;}@page  {mso-footnote-separator: url('_dstmp0015066e.files/header.htm') fs; mso-footnote-continuation-separator: url('_dstmp0015066e.files/header.htm') fcs; mso-endnote-separator: url('_dstmp0015066e.files/header.htm') es; mso-endnote-continuation-separator: url('_dstmp0015066e.files/header.htm') ecs; }@page WordSection1 {size: 595.3pt 841.9pt; margin: 3.0cm 72.0pt 72.0pt 72.0pt; mso-header-margin: 42.55pt; mso-footer-margin: 49.6pt; mso-paper-source: 0; }P.MsoNormal {\tMARGIN: 0cm 0cm 0pt; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-style-unhide: no; mso-style-qformat: yes; mso-style-parent: ''; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림}LI.MsoNormal {\tMARGIN: 0cm 0cm 0pt; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-style-unhide: no; mso-style-qformat: yes; mso-style-parent: ''; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림}DIV.MsoNormal {\tMARGIN: 0cm 0cm 0pt; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-style-unhide: no; mso-style-qformat: yes; mso-style-parent: ''; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림}P.MsoHeader {\tMARGIN: 0cm 0cm 0pt; LAYOUT-GRID-MODE: char; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: '머리글 Char'; tab-stops: center 225.65pt right 451.3pt}LI.MsoHeader {\tMARGIN: 0cm 0cm 0pt; LAYOUT-GRID-MODE: char; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: '머리글 Char'; tab-stops: center 225.65pt right 451.3pt}DIV.MsoHeader {\tMARGIN: 0cm 0cm 0pt; LAYOUT-GRID-MODE: char; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: '머리글 Char'; tab-stops: center 225.65pt right 451.3pt}P.MsoFooter {\tMARGIN: 0cm 0cm 0pt; LAYOUT-GRID-MODE: char; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: '바닥글 Char'; tab-stops: center 225.65pt right 451.3pt}LI.MsoFooter {\tMARGIN: 0cm 0cm 0pt; LAYOUT-GRID-MODE: char; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: '바닥글 Char'; tab-stops: center 225.65pt right 451.3pt}DIV.MsoFooter {\tMARGIN: 0cm 0cm 0pt; LAYOUT-GRID-MODE: char; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: '바닥글 Char'; tab-stops: center 225.65pt right 451.3pt}P {\tMARGIN: 1pt 0cm; FONT-FAMILY: 굴림; FONT-SIZE: 12pt; mso-pagination: widow-orphan; mso-bidi-font-family: 굴림; mso-style-priority: 99}SPAN.Char {\tFONT-FAMILY: 굴림; mso-style-unhide: no; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: 머리글; mso-style-name: '머리글 Char'; mso-style-locked: yes; mso-ansi-font-size: 12.0pt; mso-bidi-font-size: 12.0pt; mso-ascii-font-family: 굴림; mso-fareast-font-family: 굴림; mso-hansi-font-family: 굴림}SPAN.Char0 {\tFONT-FAMILY: 굴림; mso-style-unhide: no; mso-bidi-font-family: 굴림; mso-style-priority: 99; mso-style-link: 바닥글; mso-style-name: '바닥글 Char'; mso-style-locked: yes; mso-ansi-font-size: 12.0pt; mso-bidi-font-size: 12.0pt; mso-ascii-font-family: 굴림; mso-fareast-font-family: 굴림; mso-hansi-font-family: 굴림}SPAN.GramE {\tmso-style-name: ''; mso-gram-e: yes}.MsoChpDefault {\tmso-bidi-font-size: 10.0pt; mso-ascii-font-family: 'Times New Roman'; mso-hansi-font-family: 'Times New Roman'; mso-style-type: export-only; mso-default-props: yes; mso-font-kerning: 0pt}DIV.WordSection1 {\tpage: WordSection1}</STYLE>\r\n\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler>\r\n                                <META name=DuzonNewturns content=DocuStyler></HEAD>\r\n                                <BODY style='FONT-FAMILY: 돋움; FONT-SIZE: 9pt; tab-interval: 40.0pt' lang=KO>\r\n                                <DIV class=WordSection1>\r\n                                <TABLE style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; WIDTH: 477.75pt; BORDER-COLLAPSE: collapse; BORDER-TOP: medium none; BORDER-RIGHT: medium none; mso-border-alt: solid windowtext .5pt; mso-yfti-tbllook: 1184; mso-padding-alt: 0cm 5.4pt 0cm 5.4pt' class=MsoNormalTable border=1 cellSpacing=0 cellPadding=0 width=637>\r\n                                <TBODY>\r\n                                <TR style='HEIGHT: 17.25pt; mso-yfti-irow: 0; mso-yfti-firstrow: yes'>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 79.55pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt' width=106 colSpan=2>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>사용부서<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 164.95pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' width=220 colSpan=7>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US><o:p>@@사용부서&nbsp;</o:p></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 21.5pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' rowSpan=4 width=29>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>기<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 10pt' lang=EN-US><o:p>&nbsp;</o:p></SPAN></P>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>타<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 85.25pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' width=114 colSpan=3>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>지출요청일자<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 126.5pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: windowtext 1pt solid; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt' width=169 colSpan=3>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>참조부서<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD></TR>\r\n                                <TR style='HEIGHT: 17.25pt; mso-yfti-irow: 1'>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 79.55pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=106 colSpan=2>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>결제방법<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 164.95pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=220 colSpan=7>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>@@결제방법</SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 85.25pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=114 colSpan=3>\r\n                                <P style='TEXT-ALIGN: center' align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US>@@지출요청일자&nbsp;<o:p></o:p></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 126.5pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=169 colSpan=3>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US><o:p>&nbsp;</o:p></SPAN></P></TD></TR>\r\n                                <TR style='HEIGHT: 17.25pt; mso-yfti-irow: 2'>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 79.55pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' rowSpan=2 width=106 colSpan=2>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>계좌번호<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 57.45pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=77 colSpan=2>\r\n                                <P style='TEXT-ALIGN: right' class=MsoNormal align=right><SPAN style='FONT-SIZE: 9pt'>@@은행은행<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 107.5pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=143 colSpan=5>\r\n                                <P style='TEXT-ALIGN: left' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>&nbsp;예금주<SPAN lang=EN-US>: @@예금주<o:p></o:p></SPAN></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 85.25pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=114 colSpan=3>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>신용카드번호<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 126.5pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=169 colSpan=3>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>회계부서전표등록<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD></TR>\r\n                                <TR style='HEIGHT: 17.25pt; mso-yfti-irow: 3'>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 164.95pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=220 colSpan=7>\r\n                                <P style='TEXT-ALIGN: center' align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US>@@계좌번호<o:p></o:p></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 85.25pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=114 colSpan=3>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US><o:p>&nbsp;</o:p></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 126.5pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=169 colSpan=3>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US><o:p>@@회계</o:p></SPAN></P></TD></TR>\r\n                                <TR style='HEIGHT: 17.25pt; mso-yfti-irow: 4'>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 79.55pt; PADDING-RIGHT: 5.4pt; BACKGROUND: #eee7df; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=106 colSpan=2>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>사용일자<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 77.25pt; PADDING-RIGHT: 5.4pt; BACKGROUND: #eee7df; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=103 colSpan=3>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN class=GramE><SPAN style='FONT-SIZE: 9pt'>사<SPAN lang=EN-US><SPAN style='mso-spacerun: yes'>&nbsp; </SPAN></SPAN>용</SPAN></SPAN><SPAN style='FONT-SIZE: 9pt' lang=EN-US><SPAN style='mso-spacerun: yes'>&nbsp; </SPAN></SPAN><SPAN style='FONT-SIZE: 9pt'>처<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 145.15pt; PADDING-RIGHT: 5.4pt; BACKGROUND: #eee7df; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=194 colSpan=6>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>사용내역<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 92.9pt; PADDING-RIGHT: 5.4pt; BACKGROUND: #eee7df; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=124 colSpan=3>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN class=GramE><SPAN style='FONT-SIZE: 9pt'>공급가액<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 82.9pt; PADDING-RIGHT: 5.4pt; BACKGROUND: #eee7df; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=111 colSpan=2>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>부가세<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD></TR>\r\n                                <TR style='HEIGHT: 17.25pt; mso-yfti-irow: 5'>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 79.55pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=106 colSpan=2>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US>@@사용일자&nbsp;<o:p></o:p></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 77.25pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=103 colSpan=3>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US>@@사용처&nbsp;</SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 145.15pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=194 colSpan=6>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US><o:p>@@사용내역&nbsp;</o:p></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 92.9pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=124 colSpan=3>\r\n                                <P style='TEXT-ALIGN: right' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US>@@금액&nbsp;<o:p></o:p></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 82.9pt; PADDING-RIGHT: 5.4pt; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=111 colSpan=2>\r\n                                <P style='TEXT-ALIGN: right' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US>@@부가세&nbsp;<o:p></o:p></SPAN></P></TD></TR>\r\n\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 301.95pt; PADDING-RIGHT: 5.4pt; BACKGROUND: #eee7df; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=403 colSpan=11>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>합<SPAN lang=EN-US><SPAN style='mso-spacerun: yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </SPAN></SPAN>계<SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 92.9pt; PADDING-RIGHT: 5.4pt; BACKGROUND: #eee7df; HEIGHT: 17.25pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-left-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=124 colSpan=5>\r\n                                <P style='TEXT-ALIGN: right' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US>\\ @@총금액<o:p></o:p></SPAN></P></TD></TR>\r\n                                <TR style='HEIGHT: 78.55pt; mso-yfti-irow: 14'>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 477.75pt; PADDING-RIGHT: 5.4pt; HEIGHT: 78.55pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' width=637 colSpan=16>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt'>위 금액을 청구하오니 지급 바랍니다<SPAN lang=EN-US>.<o:p></o:p></SPAN></SPAN></P>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US><o:p>&nbsp;</o:p></SPAN></P>\r\n                                <P style='TEXT-ALIGN: center' class=MsoNormal align=center><SPAN style='FONT-SIZE: 9pt' lang=EN-US><o:p>&nbsp;</o:p></SPAN></P>\r\n                                <P class=MsoNormal><SPAN style='FONT-SIZE: 9pt'>※<SPAN class=GramE>첨부서류<SPAN lang=EN-US> :</SPAN></SPAN><SPAN lang=EN-US> </SPAN>품의서 사본<SPAN lang=EN-US>,</SPAN>카드영수증<SPAN lang=EN-US>,</SPAN>간이영수증<SPAN lang=EN-US>(3</SPAN>만원이상 지출시에는 세금계산서 첨부<SPAN lang=EN-US>)<o:p></o:p></SPAN></SPAN></P>\r\n                                <P class=MsoNormal><SPAN style='FONT-SIZE: 9pt' lang=EN-US><SPAN style='mso-spacerun: yes'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </SPAN></SPAN><SPAN style='FONT-SIZE: 9pt'>출장여비정산서 사본<SPAN lang=EN-US>(</SPAN>국내<SPAN class=GramE><SPAN lang=EN-US>,</SPAN>해외</SPAN> 출장자<SPAN lang=EN-US>)<o:p></o:p></SPAN></SPAN></P></TD></TR>\r\n                                <TR style='HEIGHT: 66.95pt; mso-yfti-irow: 15'>\r\n                                <TD style='BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: windowtext 1pt solid; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 477.75pt; PADDING-RIGHT: 5.4pt; HEIGHT: 66.95pt; BORDER-TOP: medium none; BORDER-RIGHT: windowtext 1pt solid; PADDING-TOP: 0cm; mso-border-alt: solid windowtext .5pt; mso-border-top-alt: solid windowtext .5pt' vAlign=top width=637 colSpan=16>\r\n                                <P style='TEXT-JUSTIFY: inter-ideograph; TEXT-ALIGN: justify' class=MsoNormal><SPAN style='FONT-SIZE: 9pt'>※특기사항 <BR>@@특기사항</BR><SPAN lang=EN-US><o:p></o:p></SPAN></SPAN></P></TD></TR>\r\n                                <TR style='mso-yfti-irow: 16'>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 42pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=56></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 37.55pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=50></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 25.5pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=34></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 65.8pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=88 colSpan=3></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 30.95pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=41></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 31.6pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=42></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 79.65pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=106 colSpan=4></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 103.6pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=138 colSpan=3></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 61.1pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=81></TD></TR>\r\n                                <TR style='mso-yfti-irow: 17; mso-yfti-lastrow: yes'>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 42pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=56></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 37.55pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=50></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 25.5pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=34></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 31.95pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=43></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 19.8pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=26></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 14.05pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=19></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 30.95pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=41></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 31.6pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=42></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 11.1pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=15></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 21.5pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=29></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 35.95pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=48></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 11.1pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=15></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 38.2pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=51></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 43.6pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=58></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 21.8pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=29></TD>\r\n                                <TD style='BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0cm; PADDING-LEFT: 5.4pt; WIDTH: 61.1pt; PADDING-RIGHT: 5.4pt; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0cm' width=81></TD></TR></TBODY></TABLE>\r\n                                <P class=MsoNormal><SPAN style='FONT-SIZE: 10pt' lang=EN-US><o:p>&nbsp;</o:p></SPAN></P></DIV></BODY></HTML></HTML>\r\n                                            ";
            
            if (D.GetString(drH[0]["DT_PAY_PREARRANGED"]) != string.Empty)
                str4 = D.GetString(drH[0]["DT_PAY_PREARRANGED"]).Substring(0, 4) + "/" + D.GetString(drH[0]["DT_PAY_PREARRANGED"]).Substring(4, 2) + "/" + D.GetString(drH[0]["DT_PAY_PREARRANGED"]).Substring(6, 2);
            
            if (D.GetString(drH[0]["DT_PROCESS"]) != string.Empty)
                str3 = D.GetString(drH[0]["DT_PROCESS"]).Substring(0, 4) + "/" + D.GetString(drH[0]["DT_PROCESS"]).Substring(4, 2) + "/" + D.GetString(drH[0]["DT_PROCESS"]).Substring(6, 2);
            
            string str6 = str5.Replace("@@사용부서", D.GetString(drH[0]["NM_DEPT"]) + "&nbsp;").Replace("@@지출요청일자", str4 + "&nbsp;").Replace("@@사용일자", str3 + "&nbsp;").Replace("@@사용처", D.GetString(drH[0]["LN_PARTNER"]) + "&nbsp;");
            string str7 = (drL.Length <= 1 ? str6.Replace("@@사용내역", D.GetString(drL[0]["NM_ITEM"]) + "&nbsp;") : str6.Replace("@@사용내역", D.GetString(drL[0]["NM_ITEM"]) + "&nbsp;외&nbsp;")).Replace("@@총금액", D.GetDecimal(drH[0]["AM_SUP"]).ToString("###,###,###,###,##0.####") + "&nbsp;");
            string oldValue1 = "@@부가세";
            
            Decimal @decimal = D.GetDecimal(drH[0]["VAT_TAX"]);
            
            string newValue1 = @decimal.ToString("###,###,###,###,##0.####") + "&nbsp;";
            string str8 = str7.Replace(oldValue1, newValue1);
            string oldValue2 = "@@금액";
            
            @decimal = D.GetDecimal(drH[0]["AM_K"]);
            
            string newValue2 = @decimal.ToString("###,###,###,###,##0.####") + "&nbsp;";
            
            return str8.Replace(oldValue2, newValue2).Replace("@@결제방법", D.GetString(drH[0]["NM_FG_PAYBILL"]) + "&nbsp;").Replace("@@특기사항", D.GetString(drH[0]["DC_RMK"]) + "&nbsp;").Replace("@@은행", D.GetString(drH[0]["NM_BANK"]) + "&nbsp;").Replace("@@예금주", D.GetString(drH[0]["NM_DEPOSIT"]) + "&nbsp;").Replace("@@회계", D.GetString(drH[0]["TP_AIS"]) + "&nbsp;").Replace("@@계좌번호", D.GetString(drH[0]["NO_DEPOSIT"]) + "&nbsp;");
        }

        private string GW_DYPNF_html(string html_source, string no_iv)
        {
            DataTable dataTable = this._biz.Gw_DYPNF(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                    no_iv });

            if (dataTable == null || dataTable.Rows.Count == 0)
                return html_source;
            
            string str1 = string.Empty;
            string str2 = string.Empty;
            string newValue1;
            
            if (D.GetString(dataTable.Rows[0]["DT_PAY_PREARRANGED"]) != string.Empty)
                newValue1 = D.GetString(dataTable.Rows[0]["DT_PAY_PREARRANGED"]).Substring(0, 4) + "-" + D.GetString(dataTable.Rows[0]["DT_PAY_PREARRANGED"]).Substring(4, 2) + "-" + D.GetString(dataTable.Rows[0]["DT_PAY_PREARRANGED"]).Substring(6, 2);
            else
                newValue1 = string.Empty;
            
            html_source = html_source.Replace("@@NM_PROJECT", D.GetString(dataTable.Rows[0]["NM_PROJECT"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_PO", D.GetString(dataTable.Rows[0]["NO_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_IV", D.GetString(dataTable.Rows[0]["NO_IV"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_ITEM", D.GetString(dataTable.Rows[0]["NM_ITEM"]) + "&nbsp;");
            html_source = html_source.Replace("@@LN_PARTNER", D.GetString(dataTable.Rows[0]["LN_PARTNER"]) + "&nbsp;");
            string str3 = html_source;
            string oldValue1 = "@@AM_TOT";
            
            Decimal @decimal = D.GetDecimal(dataTable.Rows[0]["AM_TOT"]);
            
            string newValue2 = @decimal.ToString("###,###,###,###,##0.####") + "&nbsp;";
            html_source = str3.Replace(oldValue1, newValue2);
            html_source = html_source.Replace("@@NM_FG_PAYMENT", D.GetString(dataTable.Rows[0]["NM_FG_PAYMENT"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_USERDEF1", D.GetString(dataTable.Rows[0]["NM_USERDEF1"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_TPPO", D.GetString(dataTable.Rows[0]["NM_TPPO"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_CD_USERDEF1", D.GetString(dataTable.Rows[0]["NM_CD_USERDEF1"]) + "&nbsp;");
            html_source = html_source.Replace("@@DT_PAY_PREARRANGED", newValue1);
            html_source = html_source.Replace("@@NM_CD_EXCH", D.GetString(dataTable.Rows[0]["NM_CD_EXCH"]) + "&nbsp;");
            string str4 = html_source;
            string oldValue2 = "@@AM_PBL_TOT";
            
            @decimal = D.GetDecimal(dataTable.Rows[0]["AM_PBL_TOT"]);
            
            string newValue3 = @decimal.ToString("###,###,###,###,##0.####") + "&nbsp;";
            
            html_source = str4.Replace(oldValue2, newValue3);
            
            string str5 = html_source;
            string oldValue3 = "@@AM_K_IV_TOT";
            
            @decimal = D.GetDecimal(dataTable.Rows[0]["AM_K_IV_TOT"]);
            
            string newValue4 = @decimal.ToString("###,###,###,###,##0.####") + "&nbsp;";
            
            html_source = str5.Replace(oldValue3, newValue4);
            
            string str6 = html_source;
            string oldValue4 = "@@AM_CLS_TOT";
            
            @decimal = D.GetDecimal(dataTable.Rows[0]["AM_CLS_TOT"]);
            
            string newValue5 = @decimal.ToString("###,###,###,###,##0.####") + "&nbsp;";
            
            html_source = str6.Replace(oldValue4, newValue5);
            html_source = html_source.Replace("@@AM_PBL_PER", D.GetString(dataTable.Rows[0]["AM_PBL_PER"]) + "&nbsp;");
            html_source = html_source.Replace("@@AM_K_IV_PER", D.GetString(dataTable.Rows[0]["AM_K_IV_PER"]) + "&nbsp;");
            html_source = html_source.Replace("@@AM_CLS_PER", D.GetString(dataTable.Rows[0]["AM_CLS_PER"]) + "&nbsp;");
            return html_source;
        }

        private string GW_KORR_html(string html_source, DataRow[] drH, DataRow[] drL)
        {
            string str = "";
            
            if (D.GetString(drH[0]["DT_PROCESS"]) != string.Empty)
                str = D.GetString(drH[0]["DT_PROCESS"]).Substring(0, 4) + "/" + D.GetString(drH[0]["DT_PROCESS"]).Substring(4, 2) + "/" + D.GetString(drH[0]["DT_PROCESS"]).Substring(6, 2);
            
            html_source = html_source.Replace("@@NM_DEPT", D.GetString(drH[0]["NM_DEPT"]) + "&nbsp;");
            html_source = html_source.Replace("@@DT_TAX", str + "&nbsp;");
            html_source = html_source.Replace("@@LN_PARTNER", D.GetString(drH[0]["LN_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@CD_PARTNER", D.GetString(drH[0]["CD_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_DOCU", D.GetString(drH[0]["NO_DOCU"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_KOR", D.GetString(drH[0]["NM_KOR"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_TAX", D.GetString(drH[0]["NM_TAX"]) + "&nbsp;");
            
            if (D.GetString(drH[0]["NO_BIZAREA"]) != string.Empty)
                str = D.GetString(drH[0]["NO_BIZAREA"]).Substring(0, 3) + "-" + D.GetString(drH[0]["NO_BIZAREA"]).Substring(3, 2) + "-" + D.GetString(drH[0]["NO_BIZAREA"]).Substring(5, 5);
            
            html_source = html_source.Replace("@@NO_BIZAREA", str + "&nbsp;");
            html_source = html_source.Replace("@@AM_TOTAL", D.GetDecimal(drH[0]["AM_SUP"]).ToString("###,###,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@VAT", D.GetDecimal(drH[0]["VAT_TAX"]).ToString("###,###,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@AM_CLS", D.GetDecimal(drH[0]["AM_K"]).ToString("###,###,###,###,##0.####") + "&nbsp;");
            
            return html_source;
        }

        private string GW_DMENG_html(string html_source, string strNOIV)
        {
            DataTable dataTable = this._biz.DataSearch_GW_RPT(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             strNOIV });

            string str1 = "";
            string str2 = D.GetDecimal(dataTable.Rows[0]["NUM_USERDEF1"]).ToString("###,###,###,###,##0.####");
            
            if (D.GetString(dataTable.Rows[0]["DT_PROCESS"]) != string.Empty)
                str1 = D.GetString(dataTable.Rows[0]["DT_PROCESS"]).Substring(0, 4) + "/" + D.GetString(dataTable.Rows[0]["DT_PROCESS"]).Substring(4, 2) + "/" + D.GetString(dataTable.Rows[0]["DT_PROCESS"]).Substring(6, 2);
            
            html_source = html_source.Replace("@@DT_PROCESS", str1 + "&nbsp;");
            
            if (D.GetString(dataTable.Rows[0]["DT_PR"]) != string.Empty)
                str1 = D.GetString(dataTable.Rows[0]["DT_PR"]).Substring(0, 4) + "/" + D.GetString(dataTable.Rows[0]["DT_PR"]).Substring(4, 2) + "/" + D.GetString(dataTable.Rows[0]["DT_PR"]).Substring(6, 2);
            
            html_source = html_source.Replace("@@NO_PO", D.GetString(dataTable.Rows[0]["NO_PO"]) + "&nbsp;");
            html_source = html_source.Replace("@@CD_PJT", D.GetString(dataTable.Rows[0]["NM_PROJECT"]) + "&nbsp;");
            html_source = html_source.Replace("@@LN_PARTNER", D.GetString(dataTable.Rows[0]["LN_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_KOR", D.GetString(dataTable.Rows[0]["NM_KOR"]) + "&nbsp;");
            html_source = html_source.Replace("@@DT_PR", str1 + "&nbsp;");
            
            if (D.GetString(dataTable.Rows[0]["DT_PO"]) != string.Empty)
                str1 = D.GetString(dataTable.Rows[0]["DT_PO"]).Substring(0, 4) + "/" + D.GetString(dataTable.Rows[0]["DT_PO"]).Substring(4, 2) + "/" + D.GetString(dataTable.Rows[0]["DT_PO"]).Substring(6, 2);
            
            html_source = html_source.Replace("@@DT_PO", str1 + "&nbsp;");
            
            if (D.GetDecimal(dataTable.Rows[0]["NUM_USERDEF1"]) == new Decimal(0))
                str2 = D.GetDecimal(dataTable.Rows[0]["AM_EX_PO"]).ToString("###,###,###,###,##0.####");
            
            html_source = html_source.Replace("@@AM_EX", str2 + "&nbsp;");
            
            if (D.GetString(dataTable.Rows[0]["DT_IO"]) != string.Empty)
                str1 = D.GetString(dataTable.Rows[0]["DT_IO"]).Substring(0, 4) + "/" + D.GetString(dataTable.Rows[0]["DT_IO"]).Substring(4, 2) + "/" + D.GetString(dataTable.Rows[0]["DT_IO"]).Substring(6, 2);
            
            html_source = html_source.Replace("@@DT_IO", str1 + "&nbsp;");
            
            return html_source;
        }

        private string GW_SGA_html(string html_source, string strNOIV)
        {
            DataTable dataTable = this._biz.DataSearch_GW_RPT(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             strNOIV });

            if (dataTable == null || dataTable.Rows.Count == 0)
                return "";
            
            string newValue = string.Empty;
            string str1 = string.Empty;
            string str2 = string.Empty;
            int num1 = 0;
            
            Decimal num2 = 0;
            Decimal num3 = 0;
            
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
            {
                ++num1;

                string @string = D.GetString(dataRow["NM_ITEM"]);
                Decimal num4 = D.GetDecimal(dataRow["QT_RCV_CLS"]);
                string str3 = num4.ToString("#,###,###,##0.####");
                num4 = D.GetDecimal(dataRow["UM_ITEM_CLS"]);
                string str4 = num4.ToString("#,###,###,##0.####");
                num4 = D.GetDecimal(dataRow["AM_CLS"]);
                string str5 = num4.ToString("#,###,###,##0.####");
                num4 = D.GetDecimal(dataRow["VAT"]);
                string str6 = num4.ToString("#,###,###,##0.####");
                num4 = D.GetDecimal(dataRow["AM_CLS"]) + D.GetDecimal(dataRow["VAT"]);
                string str7 = num4.ToString("#,###,###,##0.####");
                num3 += D.GetDecimal(dataRow["AM_CLS"]);
                num2 += D.GetDecimal(dataRow["VAT"]);
                newValue = newValue + "<tr height='25'>\r\n\t\t\t                                <td style='border: 1px solid #4DA6FF' align='center'>" + num1.ToString() + "</td>\r\n\t\t\t                                <td style='border: 1px solid #4DA6FF' align='center'>" + @string + "</td>\r\n\t\t\t                                <td style='border: 1px solid #4DA6FF' align='center'>" + str3 + "</td>\r\n\t\t\t                                <td style='border: 1px solid #4DA6FF; padding-right:2pt' align='right'>" + str4 + "</td>\r\n\t\t\t                                <td style='border: 1px solid #4DA6FF; padding-right:2pt' align='right'>" + str5 + "</td>\r\n\t\t\t                                <td style='border: 1px solid #4DA6FF; padding-right:2pt' align='right'>" + str6 + "</td>\r\n\t\t\t                                <td style='border: 1px solid #4DA6FF; padding-right:2pt' align='right'>" + str7 + "</td>\r\n\t\t                                </tr>";
            }

            if (D.GetString(dataTable.Rows[0]["DT_PROCESS"]) != string.Empty)
                str1 = D.GetString(dataTable.Rows[0]["DT_PROCESS"]).Substring(0, 4) + "-" + D.GetString(dataTable.Rows[0]["DT_PROCESS"]).Substring(4, 2) + "-" + D.GetString(dataTable.Rows[0]["DT_PROCESS"]).Substring(6, 2);
            
            if (D.GetString(dataTable.Rows[0]["DT_PAY_PREARRANGED"]) != string.Empty)
                str2 = D.GetString(dataTable.Rows[0]["DT_PAY_PREARRANGED"]).Substring(0, 4) + "-" + D.GetString(dataTable.Rows[0]["DT_PAY_PREARRANGED"]).Substring(4, 2) + "-" + D.GetString(dataTable.Rows[0]["DT_PAY_PREARRANGED"]).Substring(6, 2);
            
            html_source = html_source.Replace("@@NM_PURORG", D.GetString(dataTable.Rows[0]["NM_PURORG"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_PURGRP", D.GetString(dataTable.Rows[0]["NM_PURGRP"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_KOR", D.GetString(dataTable.Rows[0]["NM_KOR"]) + "&nbsp;");
            html_source = html_source.Replace("@@CD_PJT", D.GetString(dataTable.Rows[0]["CD_PJT"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_PROJECT", D.GetString(dataTable.Rows[0]["NM_PROJECT"]) + "&nbsp;");
            html_source = html_source.Replace("@@NO_IV", D.GetString(dataTable.Rows[0]["NO_IV"]) + "&nbsp;");
            html_source = html_source.Replace("@@DT_PROCESS", str1 + "&nbsp;");
            html_source = html_source.Replace("@@DT_PAY_PREARRANGED", str2 + "&nbsp;");
            html_source = html_source.Replace("@@LN_PARTNER", D.GetString(dataTable.Rows[0]["LN_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@BIZ_NUM1", D.GetString(dataTable.Rows[0]["BIZ_NUM1"]) + "&nbsp;");
            html_source = html_source.Replace("@@CD_PARTNER", D.GetString(dataTable.Rows[0]["CD_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@AM_CLS_SUM", num3.ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@VAT_SUM", num2.ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@AM_TOTAL_SUM", (num3 + num2).ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@LINEDATA", newValue);
            
            return html_source;
        }

        private string GW_YGENT_html(string html_source, string strNOIV)
        {
            DataTable dataTable = this._biz.DataSearch_GW_RPT(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             strNOIV });

            if (dataTable == null || dataTable.Rows.Count == 0)
                return "";
            
            string newValue = string.Empty;
            string str1 = string.Empty;
            string str2 = string.Empty;
            int num1 = 0;
            
            Decimal num2 = new Decimal(0);
            Decimal num3 = new Decimal(0);
            Decimal num4 = new Decimal(0);
            
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
            {
                ++num1;

                string string1 = D.GetString(dataRow["CD_ITEM"]);
                string string2 = D.GetString(dataRow["NM_ITEM"]);
                Decimal num5 = D.GetDecimal(dataRow["QT_RCV_CLS"]);
                string str3 = num5.ToString("#,###,###,##0.####");
                num5 = D.GetDecimal(dataRow["UM_ITEM_CLS"]);
                string str4 = num5.ToString("#,###,###,##0.####");
                num5 = D.GetDecimal(dataRow["AM_CLS"]);
                string str5 = num5.ToString("#,###,###,##0.####");
                num5 = D.GetDecimal(dataRow["VAT"]);
                string str6 = num5.ToString("#,###,###,##0.####");
                num5 = D.GetDecimal(dataRow["AM_CLS"]) + D.GetDecimal(dataRow["VAT"]);
                string str7 = num5.ToString("#,###,###,##0.####");
                num3 += D.GetDecimal(dataRow["AM_CLS"]);
                num2 += D.GetDecimal(dataRow["VAT"]);
                num4 += D.GetDecimal(dataRow["QT_RCV_CLS"]);
                newValue = newValue + "<tr height='25'>\r\n\t\t\t                                <td style='border: 1px solid #000; padding-left:2pt' align='left'>" + string1 + "</td>\r\n\t\t\t                                <td style='border: 1px solid #000; padding-left:2pt' align='left'>" + string2 + "</td>\r\n\t\t\t                                <td style='border: 1px solid #000; padding-right:2pt' align='right'>" + str3 + "</td>\r\n\t\t\t                                <td style='border: 1px solid #000; padding-right:2pt' align='right'>" + str4 + "</td>\r\n\t\t\t                                <td style='border: 1px solid #000; padding-right:2pt' align='right'>" + str5 + "</td>\r\n\t\t\t                                <td style='border: 1px solid #000; padding-right:2pt' align='right'>" + str6 + "</td>\r\n\t\t\t                                <td style='border: 1px solid #000; padding-right:2pt' align='right'>" + str7 + "</td>\r\n\t\t                                </tr>";
            }

            if (D.GetString(dataTable.Rows[0]["DT_PROCESS"]) != string.Empty)
                str1 = D.GetString(dataTable.Rows[0]["DT_PROCESS"]).Substring(0, 4) + "-" + D.GetString(dataTable.Rows[0]["DT_PROCESS"]).Substring(4, 2) + "-" + D.GetString(dataTable.Rows[0]["DT_PROCESS"]).Substring(6, 2);
            
            if (D.GetString(dataTable.Rows[0]["DT_PAY_PREARRANGED"]) != string.Empty)
                str2 = D.GetString(dataTable.Rows[0]["DT_PAY_PREARRANGED"]).Substring(0, 4) + "-" + D.GetString(dataTable.Rows[0]["DT_PAY_PREARRANGED"]).Substring(4, 2) + "-" + D.GetString(dataTable.Rows[0]["DT_PAY_PREARRANGED"]).Substring(6, 2);
            
            html_source = html_source.Replace("@@NO_IV", D.GetString(dataTable.Rows[0]["NO_IV"]) + "&nbsp;");
            html_source = html_source.Replace("@@DT_PROCESS", str1 + "&nbsp;");
            html_source = html_source.Replace("@@DT_PAY_PREARRANGED", str2 + "&nbsp;");
            html_source = html_source.Replace("@@LN_PARTNER", D.GetString(dataTable.Rows[0]["LN_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@BIZ_NUM1", D.GetString(dataTable.Rows[0]["BIZ_NUM1"]) + "&nbsp;");
            html_source = html_source.Replace("@@NM_CEO", D.GetString(dataTable.Rows[0]["NM_CEO"]) + "&nbsp;");
            html_source = html_source.Replace("@@CD_EMP_PARTNER", D.GetString(dataTable.Rows[0]["CD_EMP_PARTNER"]) + "&nbsp;");
            html_source = html_source.Replace("@@PTR_EMAIL", D.GetString(dataTable.Rows[0]["PTR_EMAIL"]) + "&nbsp;");
            html_source = html_source.Replace("@@AM_CLS_SUM", num3.ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@VAT_SUM", num2.ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@QT_CLS_SUM", num4.ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@AM_TOTAL_SUM", (num3 + num2).ToString("#,###,###,##0.####") + "&nbsp;");
            html_source = html_source.Replace("@@LINEDATA", newValue);
            
            return html_source;
        }

        private string GetHangulAmt(Decimal nAmt)
        {
            string str = null;
            string[] strArray1 = new string[] { "",
                                                "",
                                                "십",
                                                "백",
                                                "천",
                                                "만",
                                                "십",
                                                "백",
                                                "천",
                                                "억",
                                                "십",
                                                "백",
                                                "천",
                                                "조",
                                                "십",
                                                "백" };

            string[] strArray2 = new string[] { "",
                                                "일",
                                                "이",
                                                "삼",
                                                "사",
                                                "오",
                                                "육",
                                                "칠",
                                                "팔",
                                                "구" };

            string s = !(nAmt < 0) ? nAmt.ToString() : nAmt.ToString().Substring(1);
            int length = s.Length;
            
            if (length < 1)
                return "0";
            
            if (length == 1)
                return strArray2[int.Parse(s)];
            
            if (length > 15 || s.Substring(1, 1) == ".")
            {
                MessageBox.Show("확인! 백조단위가 넘습니다.");
                return "";
            }
            else
            {
                bool flag1 = false;
                bool flag2 = false;
                bool flag3 = false;

                if (length > 5 && Decimal.Parse(s.Substring(length - 5, 2)) < 10)
                    flag1 = true;
                
                if (length > 9 && Decimal.Parse(s.Substring(length - 9, 2)) < 10)
                    flag2 = true;
                
                if (length > 13 && Decimal.Parse(s.Substring(length - 13, 2)) < 10)
                    flag3 = true;
                
                for (int startIndex = 0; startIndex < length; ++startIndex)
                {
                    if (s.Substring(startIndex, 1) != "0")
                    {
                        if (length - startIndex == 12 && flag3)
                            str = str + strArray1[length - startIndex + 1];

                        if (length - startIndex == 8 && flag2)
                            str = str + strArray1[length - startIndex + 1];
                        
                        if (length - startIndex == 4 && flag1)
                            str = str + strArray1[length - startIndex + 1];
                        
                        str = str + strArray2[int.Parse(s.Substring(startIndex, 1))];
                        
                        if (length - startIndex > 0)
                            str = str + strArray1[length - startIndex];
                    }
                    else
                    {
                        int num1 = length - startIndex;
                        int num2;

                        switch (num1)
                        {
                            case 4:
                            case 8:
                                num2 = 0;
                                break;
                            default:
                                num2 = num1 != 12 ? 1 : 0;
                                break;
                        }

                        if (num2 == 0 && (str.Substring(str.Length - 1, 1) != "만" && str.Substring(str.Length - 1, 1) != "억" && str.Substring(str.Length - 1, 1) != "조"))
                            str = str + strArray1[length - startIndex + 1];
                    }
                }

                if (nAmt < 0)
                    return "-" + str;
                else
                    return str;
            }
        }

        private string getDayFormat(string strDay)
        {
            if (strDay.Length == 0)
                return null;

            string str = string.Empty;
            
            if (strDay.Length >= 8)
                str = strDay.Substring(0, 4) + "." + strDay.Substring(4, 2) + "." + strDay.Substring(6, 2);
            
            return str;
        }

        private string GetAmtFormat(object val)
        {
            return Convert.ToDecimal(val).ToString("#,###,###,###.####");
        }

        public bool save_TF(int st_stat)
        {
            bool flag = false;

            if (st_stat == 4)
                st_stat = -1;
            
            if (st_stat == 0 || st_stat == 1)
                flag = false;
            else if (st_stat == -1 || st_stat == 2 || st_stat == 3 || st_stat == 999)
                flag = true;
            
            return flag;
        }
    }
}

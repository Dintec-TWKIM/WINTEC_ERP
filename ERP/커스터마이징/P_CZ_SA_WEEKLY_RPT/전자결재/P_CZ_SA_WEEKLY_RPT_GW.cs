using System.Data;
using System.Diagnostics;
using System.Text;
using System.Web;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System.Windows.Forms;
using System.IO;
using System;
using Dintec;

namespace cz
{
    internal class P_CZ_SA_WEEKLY_RPT_GW
    {
        internal bool 전자결재(DataRow header)
        {
            bool isSuccess;
            string strURL, key;

            key = (MA.Login.회사코드 + "_" + header["CD_CC"].ToString() + "_" + header["DT_YEAR"].ToString() + "_" + header["QT_WEEK"].ToString());

            DataTable dt = DBHelper.GetDataTable(@"DECLARE @V_DATE AS DATETIME
SET @V_DATE = GETDATE()
SELECT CONVERT(NVARCHAR, DATEPART(YEAR, @V_DATE)) + '년 ' +  
       CONVERT(NVARCHAR, DATEPART(MONTH, @V_DATE)) + '월 ' + 
       CONVERT(NVARCHAR, CEILING((DAY(@V_DATE) + DATEPART(DW, CONVERT(CHAR(6), @V_DATE, 112) + '01') - 1) / 7.0)) + '주' AS DT_CAL");

            isSuccess = 결재상신(new object[] { GroupWare.GetERP_CD_COMPANY(),
                                                GroupWare.GetERP_CD_PC(),
                                                key,
                                                MA.Login.사원번호,
                                                Global.MainFrame.GetStringToday,
                                                this.GetHtml(header),
                                                header["NM_CC"].ToString() + " 주간 업무 계획/보고 " + "(" + dt.Rows[0]["DT_CAL"].ToString() + ")",
                                                "Y",
                                                2005 });

            if (!isSuccess) return false;

            strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
                     + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
                     + "&cd_pc=" + GroupWare.GetERP_CD_PC()
                     + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
                     + "&login_id=" + MA.Login.사원번호;

            Process.Start("msedge.exe", strURL);

            return isSuccess;
        }

        internal void 미리보기(DataRow header)
        {
            string html;

            html = this.GetHtml(header);
            P_CZ_MA_HTML_VIEWER dialog = new P_CZ_MA_HTML_VIEWER("주간 업무 계획/보고", html);

            dialog.ShowDialog();
        }

        private string GetHtml(DataRow header)
        {
            string path, body;

            body = string.Empty;

            path = Application.StartupPath + "\\download\\gw\\HT_P_CZ_SA_WEEKLY_RPT.htm";

            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("@@TB_QTN", this.Get견적업무(header));
            body = body.Replace("@@TB_SO", this.Get수주업무(header));
            body = body.Replace("@@TB_CLAIM", this.Get클레임(header));
            body = body.Replace("@@TB_INVOICE", this.Get미수금(header));
            body = body.Replace("@@TB_PROJECT", this.Get프로젝트(header));

            return body;
        }

        private string Get견적업무(DataRow header)
		{
            DataTable dt;
            string html, html1, html2, query;

            if (header["CD_CC"].ToString() == "010420")
			{
                #region 영업6팀
                html = @"<table style='width:943px; border:2px solid black; margin-bottom: 20px; font-size: 9pt; font-family: 굴림; border-collapse:collapse; border-spacing:0;'>
    <colgroup width='134px' align='center'></colgroup>
    <colgroup width='234px' align='center'></colgroup>
    <colgroup width='100px' align='center'></colgroup>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='50px' align='center'></colgroup>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='265px' align='center'></colgroup>
    <tbody>
        <tr style='height:30px'>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>호선명</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>매출처</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>매출처그룹</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>유형</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>종수</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>금액</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>특이사항</th>
        </tr>
        {0}
    </tbody>
</table>";
                html1 = @"<tr style='height:30px'>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{0}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{1}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{2}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{3}</td>
            <td style='border:solid 1px black; padding-right:10px; text-align:right'>{4}</td>
            <td style='border:solid 1px black; padding-right:10px; text-align:right'>{5}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{6}</td>
        </tr>";

                query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT MH.NM_VESSEL,
       MP.LN_PARTNER,
       MC.NM_SYSDEF AS NM_PARTNER_GRP,
       MAX(IG.NM_ITEMGRP) AS NM_ITEMGRP,
       COUNT(1) AS QT_ITEM,
       SUM(QL.AM_KR_S) AS AM,
       MAX(RL.DC_RMK) AS DC_RMK
FROM CZ_SA_WEEKLY_RPT_H RH
JOIN CZ_SA_WEEKLY_RPT_L RL ON RL.CD_COMPANY = RH.CD_COMPANY AND RL.NO_EMP = RH.NO_EMP AND RL.DT_YEAR = RH.DT_YEAR AND RL.QT_WEEK = RH.QT_WEEK
JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = RL.CD_COMPANY AND QH.NO_FILE = RL.NO_KEY
LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = QH.NO_IMO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = QH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000065' AND MC.CD_SYSDEF = QH.CD_PARTNER_GRP
LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = QL.CD_COMPANY AND IG.CD_ITEMGRP = QL.GRP_ITEM
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = RH.CD_COMPANY AND ME.NO_EMP = RH.NO_EMP
WHERE RH.CD_COMPANY = '{0}'
AND RH.YN_POST = 'Y'
AND ME.CD_CC = '{1}'
AND RH.DT_YEAR = '{2}'
AND RH.QT_WEEK = '{3}'
AND RL.TP_GUBUN = '001' 
AND ISNULL(QL.CD_ITEM, '') NOT LIKE 'SD%'
GROUP BY MH.NM_VESSEL, MP.LN_PARTNER, MC.NM_SYSDEF
ORDER BY SUM(QL.AM_KR_S) DESC";

                dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                header["CD_CC"].ToString(),
                                                                header["DT_YEAR"].ToString(),
                                                                header["QT_WEEK"].ToString()));

                html2 = string.Empty;

                foreach (DataRow dr in dt.Rows)
                {
                    html2 += string.Format(html1, dr["NM_VESSEL"].ToString(),
                                                  dr["LN_PARTNER"].ToString(),
                                                  dr["NM_PARTNER_GRP"].ToString(),
                                                  dr["NM_ITEMGRP"].ToString(),
                                                  dr["QT_ITEM"].ToString(),
                                                  Util.GetTO_Money(dr["AM"]),
                                                  dr["DC_RMK"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp;"));
                }
                #endregion
            }
			else
			{
                #region 나머지 팀
                html = @"<table style='width:943px; border:2px solid black; margin-bottom: 20px; font-size: 9pt; font-family: 굴림; border-collapse:collapse; border-spacing:0;'>
    <colgroup width='100px' align='center'></colgroup>
    <colgroup width='150px' align='center'></colgroup>
    <colgroup width='60px' align='center'></colgroup>
    <colgroup width='60px' align='center'></colgroup>
    <colgroup width='100px' align='center'></colgroup>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='50px' align='center'></colgroup>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='263px' align='center'></colgroup>
    <tbody>
        <tr style='height:30px'>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>파일번호</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>매출처</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>호선번호</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>담당자</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>매입처</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>유형</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>종수</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>금액</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>특이사항</th>
        </tr>
        {0}
    </tbody>
</table>";

                html1 = @"<tr style='height:30px'>
            <td style='border:solid 1px black; text-align:center'>{0}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{1}</td>
            <td style='border:solid 1px black; text-align:center'>{2}</td>
            <td style='border:solid 1px black; text-align:center'>{3}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{4}</td>
            <td style='border:solid 1px black; text-align:center'>{5}</td>
            <td style='border:solid 1px black; text-align:center'>{6}</td>
            <td style='border:solid 1px black; padding-right:10px; text-align:right'>{7}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{8}</td>
        </tr>";

                query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

;WITH A AS
(
    SELECT RH.CD_COMPANY,
           RL.NO_KEY,
           RH.NO_EMP,
           ME.NM_KOR,
           ME.CD_CC,
           MC.NM_CC,
           ISNULL(MP.SN_PARTNER, MP.LN_PARTNER) AS LN_PARTNER,
           MH.NO_HULL,
           ISNULL(QL.QT_ITEM, 0) AS QT_ITEM,
           IG.NM_ITEMGRP,
           MD.NM_SYSDEF AS NM_EXCH,
           ISNULL(QL.AM_EX_S, 0) AS AM_EX,
           ISNULL(QL.AM_KR_S, 0) AS AM,
           RL.DC_RMK
    FROM CZ_SA_WEEKLY_RPT_H RH
    JOIN CZ_SA_WEEKLY_RPT_L RL ON RL.CD_COMPANY = RH.CD_COMPANY AND RL.NO_EMP = RH.NO_EMP AND RL.DT_YEAR = RH.DT_YEAR AND RL.QT_WEEK = RH.QT_WEEK
    JOIN CZ_SA_QTNH QH ON QH.CD_COMPANY = RL.CD_COMPANY AND QH.NO_FILE = RL.NO_KEY
    LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = RH.CD_COMPANY AND ME.NO_EMP = RH.NO_EMP
    LEFT JOIN MA_CC MC ON MC.CD_COMPANY = ME.CD_COMPANY AND MC.CD_CC = ME.CD_CC
    LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = QH.CD_COMPANY AND MP.CD_PARTNER = QH.CD_PARTNER
    LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = QH.NO_IMO
    LEFT JOIN MA_CODEDTL MD ON MD.CD_COMPANY = QH.CD_COMPANY AND MD.CD_FIELD = 'MA_B000005' AND MD.CD_SYSDEF = QH.CD_EXCH
    LEFT JOIN (SELECT QL.CD_COMPANY, QL.NO_FILE,
                      MAX(QL.GRP_ITEM) AS GRP_ITEM,
                      COUNT(1) AS QT_ITEM,
                      SUM(QL.AM_EX_S) AS AM_EX_S,
                      SUM(QL.AM_KR_S) AS AM_KR_S
               FROM CZ_SA_QTNL QL
               WHERE ISNULL(QL.CD_ITEM, '') NOT LIKE 'SD%'
               GROUP BY QL.CD_COMPANY, QL.NO_FILE) QL 
    ON QL.CD_COMPANY = QH.CD_COMPANY AND QL.NO_FILE = QH.NO_FILE
    LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = QL.CD_COMPANY AND IG.CD_ITEMGRP = QL.GRP_ITEM
    WHERE RH.CD_COMPANY = '{0}'
    AND RH.YN_POST = 'Y'
    AND ME.CD_CC = '{1}'
    AND RH.DT_YEAR = '{2}'
    AND RH.QT_WEEK = '{3}'
    AND RL.TP_GUBUN = '001'
)
SELECT A.*,
       (SELECT (CASE WHEN ISNULL(QL.CNT, 0) = 0 THEN ''
                     WHEN ISNULL(QL.CNT, 0) = 1 THEN ISNULL(MP.SN_PARTNER, MP.LN_PARTNER) 
                                                ELSE ISNULL(MP.SN_PARTNER, MP.LN_PARTNER) + ' 외' + CONVERT(NVARCHAR, ISNULL(QL.CNT, 0) - 1) + '개' END) AS NM_SUPPLIER
        FROM (SELECT QL.CD_COMPANY, QL.NO_FILE, QL.CD_SUPPLIER,
                     ROW_NUMBER() OVER (PARTITION BY QL.CD_COMPANY, QL.NO_FILE ORDER BY QL.NO_DSP ASC) AS IDX,
                     SUM(1) OVER (PARTITION BY QL.CD_COMPANY, QL.NO_FILE ORDER BY QL.NO_DSP DESC) AS CNT  
              FROM (SELECT QL.CD_COMPANY, QL.NO_FILE, QL.CD_SUPPLIER,
                           MIN(QL.NO_DSP) AS NO_DSP
                    FROM CZ_SA_QTNL QL
                    WHERE QL.CD_COMPANY = A.CD_COMPANY
                    AND QL.NO_FILE = A.NO_KEY 
                    AND QL.CD_SUPPLIER IS NOT NULL
                    GROUP BY QL.CD_COMPANY, QL.NO_FILE, QL.CD_SUPPLIER) QL) QL
        JOIN MA_PARTNER MP ON MP.CD_COMPANY = QL.CD_COMPANY AND MP.CD_PARTNER = QL.CD_SUPPLIER
        WHERE QL.IDX = 1) AS NM_SUPPLIER
FROM A
ORDER BY A.AM DESC";

                dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                header["CD_CC"].ToString(),
                                                                header["DT_YEAR"].ToString(),
                                                                header["QT_WEEK"].ToString()));

                html2 = string.Empty;

                foreach (DataRow dr in dt.Rows)
                {
                    html2 += string.Format(html1, dr["NO_KEY"].ToString(),
                                                  dr["LN_PARTNER"].ToString(),
                                                  dr["NO_HULL"].ToString(),
                                                  dr["NM_KOR"].ToString(),
                                                  dr["NM_SUPPLIER"].ToString(),
                                                  dr["NM_ITEMGRP"].ToString(),
                                                  dr["QT_ITEM"].ToString(),
                                                  Util.GetTO_Money(dr["AM"]),
                                                  dr["DC_RMK"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp;"));
                }
                #endregion
            }

			html = string.Format(html, html2);

            return html;
		}

        private string Get수주업무(DataRow header)
        {
            DataTable dt;
            string html, html1, html2, query;

            if (header["CD_CC"].ToString() == "010420")
			{
                #region 영업6팀
                html = @"<table style='width:943px; border:2px solid black; margin-bottom: 20px; font-size: 9pt; font-family: 굴림; border-collapse:collapse; border-spacing:0;'>
     <colgroup width='134px' align='center'></colgroup>
    <colgroup width='234px' align='center'></colgroup>
    <colgroup width='100px' align='center'></colgroup>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='50px' align='center'></colgroup>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='265px' align='center'></colgroup>
    <tbody>
        <tr style='height:30px'>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>호선명</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>매출처</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>매출처그룹</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>유형</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>종수</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>금액</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>특이사항</th>
        </tr>
        {0}
    </tbody>
</table>";
                html1 = @"<tr style='height:30px'>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{0}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{1}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{2}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{3}</td>
            <td style='border:solid 1px black; padding-right:10px; text-align:right'>{4}</td>
            <td style='border:solid 1px black; padding-right:10px; text-align:right'>{5}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{6}</td>
        </tr>";

                query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT MH.NM_VESSEL,
       MP.LN_PARTNER,
       MC.NM_SYSDEF AS NM_PARTNER_GRP,
       MAX(IG.NM_ITEMGRP) AS NM_ITEMGRP,
       COUNT(1) AS QT_ITEM,
       SUM(SL.AM_KR_S) AS AM,
       MAX(RL.DC_RMK) AS DC_RMK
FROM CZ_SA_WEEKLY_RPT_H RH
JOIN CZ_SA_WEEKLY_RPT_L RL ON RL.CD_COMPANY = RH.CD_COMPANY AND RL.NO_EMP = RH.NO_EMP AND RL.DT_YEAR = RH.DT_YEAR AND RL.QT_WEEK = RH.QT_WEEK
JOIN SA_SOH SH ON SH.CD_COMPANY = RL.CD_COMPANY AND SH.NO_SO = RL.NO_KEY
LEFT JOIN SA_SOL SL ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = SL.CD_COMPANY AND QL.NO_FILE = SL.NO_SO AND QL.NO_LINE = SL.SEQ_SO
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = SH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000065' AND MC.CD_SYSDEF = SH.CD_PARTNER_GRP
LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = QL.CD_COMPANY AND IG.CD_ITEMGRP = QL.GRP_ITEM
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = RH.CD_COMPANY AND ME.NO_EMP = RH.NO_EMP
WHERE RH.CD_COMPANY = '{0}'
AND RH.YN_POST = 'Y'
AND ME.CD_CC = '{1}'
AND RH.DT_YEAR = '{2}'
AND RH.QT_WEEK = '{3}'
AND RL.TP_GUBUN = '002' 
AND ISNULL(SL.CD_ITEM, '') NOT LIKE 'SD%'
GROUP BY MH.NM_VESSEL, MP.LN_PARTNER, MC.NM_SYSDEF
ORDER BY SUM(SL.AM_KR_S) DESC";

                dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                header["CD_CC"].ToString(),
                                                                header["DT_YEAR"].ToString(),
                                                                header["QT_WEEK"].ToString()));

                html2 = string.Empty;

                foreach (DataRow dr in dt.Rows)
                {
                    html2 += string.Format(html1, dr["NM_VESSEL"].ToString(),
                                                  dr["LN_PARTNER"].ToString(),
                                                  dr["NM_PARTNER_GRP"].ToString(),
                                                  dr["NM_ITEMGRP"].ToString(),
                                                  dr["QT_ITEM"].ToString(),
                                                  Util.GetTO_Money(dr["AM"]),
                                                  dr["DC_RMK"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp;"));
                }

                html = string.Format(html, html2);
                #endregion
            }
			else
			{
                #region 나머지 팀
                html = @"<table style='width:943px; border:2px solid black; margin-bottom: 20px; font-size: 9pt; font-family: 굴림; border-collapse:collapse; border-spacing:0;'>
    <colgroup width='100px' align='center'></colgroup>
    <colgroup width='150px' align='center'></colgroup>
    <colgroup width='60px' align='center'></colgroup>
    <colgroup width='60px' align='center'></colgroup>
    <colgroup width='100px' align='center'></colgroup>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='50px' align='center'></colgroup>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='263px' align='center'></colgroup>
    <tbody>
        <tr style='height:30px'>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>파일번호</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>매출처</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>호선번호</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>담당자</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>매입처</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>유형</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>종수</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>금액</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>특이사항</th>
        </tr>
        {0}
    </tbody>
</table>";

                html1 = @"<tr style='height:30px'>
            <td style='border:solid 1px black; text-align:center'>{0}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{1}</td>
            <td style='border:solid 1px black; text-align:center'>{2}</td>
            <td style='border:solid 1px black; text-align:center'>{3}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{4}</td>
            <td style='border:solid 1px black; text-align:center'>{5}</td>
            <td style='border:solid 1px black; text-align:center'>{6}</td>
            <td style='border:solid 1px black; padding-right:10px; text-align:right'>{7}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{8}</td>
        </tr>";

                query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

;WITH A AS
(
    SELECT RH.CD_COMPANY,
           RL.NO_KEY,
           RH.NO_EMP,
           ME.NM_KOR,
           ME.CD_CC,
           MC.NM_CC,
           ISNULL(MP.SN_PARTNER, MP.LN_PARTNER) AS LN_PARTNER,
           MH.NO_HULL,
           IG.NM_ITEMGRP,
           ISNULL(SL.QT_ITEM, 0) AS QT_ITEM,
           MD.NM_SYSDEF AS NM_EXCH,
           ISNULL(SL.AM_EX_S, 0) AS AM_EX,
           ISNULL(SL.AM_KR_S, 0) AS AM,
           ISNULL(SL.QT_STOCK, 0) AS QT_STOCK,
           RL.DC_RMK
    FROM CZ_SA_WEEKLY_RPT_H RH
    JOIN CZ_SA_WEEKLY_RPT_L RL ON RL.CD_COMPANY = RH.CD_COMPANY AND RL.NO_EMP = RH.NO_EMP AND RL.DT_YEAR = RH.DT_YEAR AND RL.QT_WEEK = RH.QT_WEEK
    JOIN SA_SOH SH ON SH.CD_COMPANY = RL.CD_COMPANY AND SH.NO_SO = RL.NO_KEY
    LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = RH.CD_COMPANY AND ME.NO_EMP = RH.NO_EMP
    LEFT JOIN MA_CC MC ON MC.CD_COMPANY = ME.CD_COMPANY AND MC.CD_CC = ME.CD_CC
    LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
    LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
    LEFT JOIN MA_CODEDTL MD ON MD.CD_COMPANY = SH.CD_COMPANY AND MD.CD_FIELD = 'MA_B000005' AND MD.CD_SYSDEF = SH.CD_EXCH
    LEFT JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
                      MAX(QL.GRP_ITEM) AS GRP_ITEM,
                      COUNT(1) AS QT_ITEM,
                      SUM(SL.AM_EX_S) AS AM_EX_S,
                      SUM(SL.AM_KR_S) AS AM_KR_S,
                      SUM(SB.QT_STOCK) AS QT_STOCK
               FROM SA_SOL SL
               LEFT JOIN CZ_SA_QTNL QL ON QL.CD_COMPANY = SL.CD_COMPANY AND QL.NO_FILE = SL.NO_SO AND QL.NO_LINE = SL.SEQ_SO
               LEFT JOIN CZ_SA_STOCK_BOOK SB ON SB.CD_COMPANY = SL.CD_COMPANY AND SB.NO_FILE = SL.NO_SO AND SB.NO_LINE = SL.SEQ_SO
               WHERE SL.CD_ITEM NOT LIKE 'SD%'
               GROUP BY SL.CD_COMPANY, SL.NO_SO) SL 
    ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
    LEFT JOIN MA_ITEMGRP IG ON IG.CD_COMPANY = SL.CD_COMPANY AND IG.CD_ITEMGRP = SL.GRP_ITEM
    WHERE RH.CD_COMPANY = '{0}'
    AND RH.YN_POST = 'Y'
    AND ME.CD_CC = '{1}'
    AND RH.DT_YEAR = '{2}'
    AND RH.QT_WEEK = '{3}'
    AND RL.TP_GUBUN = '002'
)
SELECT A.*,
       (SELECT (CASE WHEN A.QT_STOCK > 0 AND ISNULL(QL.CNT, 0) = 0 THEN 'STOCK'
                     WHEN A.QT_STOCK > 0 AND ISNULL(QL.CNT, 0) > 0 THEN 'STOCK 외' + CONVERT(NVARCHAR, QL.CNT) + '개'
                     WHEN A.QT_STOCK = 0 AND ISNULL(QL.CNT, 0) = 0 THEN ''
                     WHEN A.QT_STOCK = 0 AND ISNULL(QL.CNT, 0) = 1 THEN ISNULL(MP.SN_PARTNER, MP.LN_PARTNER) 
                                                                   ELSE ISNULL(MP.SN_PARTNER, MP.LN_PARTNER) + ' 외' + CONVERT(NVARCHAR, QL.CNT-1) + '개' END) AS NM_SUPPLIER
        FROM (SELECT PH.CD_COMPANY, PH.CD_PARTNER,
                     ROW_NUMBER() OVER (PARTITION BY PH.CD_COMPANY ORDER BY PH.NO_PO ASC) AS IDX,
                     SUM(1) OVER (PARTITION BY PH.CD_COMPANY ORDER BY PH.NO_PO DESC) AS CNT  
              FROM PU_POH PH
              WHERE PH.CD_COMPANY = A.CD_COMPANY
              AND PH.CD_PJT = A.NO_KEY) QL
        JOIN MA_PARTNER MP ON MP.CD_COMPANY = QL.CD_COMPANY AND MP.CD_PARTNER = QL.CD_PARTNER
        WHERE QL.IDX = 1) AS NM_SUPPLIER
FROM A
ORDER BY A.AM DESC";

                dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                header["CD_CC"].ToString(),
                                                                header["DT_YEAR"].ToString(),
                                                                header["QT_WEEK"].ToString()));

                html2 = string.Empty;

                foreach (DataRow dr in dt.Rows)
                {
                    html2 += string.Format(html1, dr["NO_KEY"].ToString(),
                                                  dr["LN_PARTNER"].ToString(),
                                                  dr["NO_HULL"].ToString(),
                                                  dr["NM_KOR"].ToString(),
                                                  dr["NM_SUPPLIER"].ToString(),
                                                  dr["NM_ITEMGRP"].ToString(),
                                                  dr["QT_ITEM"].ToString(),
                                                  Util.GetTO_Money(dr["AM"]),
                                                  dr["DC_RMK"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp;"));
                }

                html = string.Format(html, html2);
                #endregion
            }

            return html;
        }

        private string Get클레임(DataRow header)
        {
            DataTable dt;
            string html, html1, html2, query;

            html = @"<table style='width:943px; border:2px solid black; margin-bottom: 20px; font-size: 9pt; font-family: 굴림; border-collapse:collapse; border-spacing:0;'>
    <colgroup width='50px' align='center'></colgroup>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='60px' align='center'></colgroup>
    <colgroup width='100px' align='center'></colgroup>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='253px' align='center'></colgroup>
    <tbody>
        <tr style='height:30px'>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>상태</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>클레임번호</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>담당자</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>매출처</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>매입처</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>사유</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>원인구분</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>항목분류</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>원인</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>특이사항</th>
        </tr>
        {0}
    </tbody>
</table>";

            html1 = @"<tr style='height:30px'>
            <td style='border:solid 1px black; text-align:center'>{0}</td>
            <td style='border:solid 1px black; text-align:center'>{1}</td>
            <td style='border:solid 1px black; text-align:center'>{2}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{3}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{4}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{5}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{6}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{7}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{8}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{9}</td>
        </tr>";

            query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT RH.CD_COMPANY,
       RL.NO_KEY,
       CH.NO_CLAIM,
       CH.NO_SO,
       CONVERT(DATETIME, CH.DT_INPUT) AS DT_INPUT,
       ISNULL(MP.SN_PARTNER, MP.LN_PARTNER) AS NM_PARTNER,
       ISNULL(MP1.SN_PARTNER, MP1.LN_PARTNER) AS NM_SUPPLIER,
       MH.NM_VESSEL,
       RL.NO_EMP,
       ME.NM_KOR,
       MC.NM_SYSDEF AS NM_CLAIM,
       MC1.NM_SYSDEF AS NM_CAUSE,
       MC2.NM_SYSDEF AS NM_ITEM,
       MC3.NM_SYSDEF AS NM_GW_STAT,
       MC4.NM_SYSDEF AS NM_STATUS,
       MC5.NM_SYSDEF AS NM_REASON,
       MC6.NM_SYSDEF AS NM_RETURN,
       MC7.NM_SYSDEF AS NM_REQUEST,
       (CH.AM_ITEM_RCV + CH.AM_ADD_RCV) AS AM_TOTAL_RCV,
       RL.DC_RMK
FROM CZ_SA_WEEKLY_RPT_H RH
JOIN CZ_SA_WEEKLY_RPT_L RL ON RL.CD_COMPANY = RH.CD_COMPANY AND RL.NO_EMP = RH.NO_EMP AND RL.DT_YEAR = RH.DT_YEAR AND RL.QT_WEEK = RH.QT_WEEK
LEFT JOIN CZ_SA_CLAIMH CH ON CH.CD_COMPANY = RL.CD_COMPANY AND CH.NO_CLAIM = RL.NO_KEY
LEFT JOIN (SELECT CL.CD_COMPANY, CL.NO_CLAIM, 
		   		  MAX(CL.CD_SUPPLIER) AS CD_SUPPLIER 
		   FROM CZ_SA_CLAIML CL
		   GROUP BY CL.CD_COMPANY, CL.NO_CLAIM) CL
ON CL.CD_COMPANY = CH.CD_COMPANY AND CL.NO_CLAIM = CH.NO_CLAIM 
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = CH.CD_COMPANY AND MP.CD_PARTNER = CH.CD_PARTNER
LEFT JOIN MA_PARTNER MP1 ON MP1.CD_COMPANY = CH.CD_COMPANY AND MP1.CD_PARTNER = CL.CD_SUPPLIER
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = CH.NO_IMO
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = CH.CD_COMPANY AND ME.NO_EMP = CH.NO_EMP
LEFT JOIN FI_GWDOCU GW ON GW.CD_COMPANY = 'K100' AND GW.CD_PC = '010000' AND GW.NO_DOCU = CH.CD_COMPANY + '-' + CH.NO_CLAIM + '-' + CH.CD_STATUS
LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = CH.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00001' AND MC.CD_SYSDEF = CH.TP_CLAIM
LEFT JOIN MA_CODEDTL MC1 ON MC1.CD_COMPANY = CH.CD_COMPANY AND MC1.CD_FIELD = 'CZ_SA00002' AND MC1.CD_SYSDEF = CH.TP_CAUSE
LEFT JOIN MA_CODEDTL MC2 ON MC2.CD_COMPANY = CH.CD_COMPANY AND MC2.CD_FIELD = 'CZ_SA00003' AND MC2.CD_SYSDEF = CH.TP_ITEM
LEFT JOIN MA_CODEDTL MC3 ON MC3.CD_COMPANY = GW.CD_COMPANY AND MC3.CD_FIELD = 'PU_C000065' AND MC3.CD_SYSDEF = GW.ST_STAT
LEFT JOIN MA_CODEDTL MC4 ON MC4.CD_COMPANY = CH.CD_COMPANY AND MC4.CD_FIELD = 'CZ_SA00004' AND MC4.CD_SYSDEF = CH.CD_STATUS
LEFT JOIN MA_CODEDTL MC5 ON MC5.CD_COMPANY = CH.CD_COMPANY AND MC5.CD_FIELD = 'CZ_SA00048' AND MC5.CD_SYSDEF = CH.TP_REASON
LEFT JOIN MA_CODEDTL MC6 ON MC6.CD_COMPANY = CH.CD_COMPANY AND MC6.CD_FIELD = 'CZ_SA00050' AND MC6.CD_SYSDEF = CH.TP_RETURN
LEFT JOIN MA_CODEDTL MC7 ON MC7.CD_COMPANY = CH.CD_COMPANY AND MC7.CD_FIELD = 'CZ_SA00049' AND MC7.CD_SYSDEF = CH.TP_REQUEST
WHERE RH.CD_COMPANY = '{0}'
AND RH.YN_POST = 'Y'
AND ME.CD_CC = '{1}'
AND RH.DT_YEAR = '{2}'
AND RH.QT_WEEK = '{3}'
AND RL.TP_GUBUN = '004'";

            dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                            header["CD_CC"].ToString(), 
                                                            header["DT_YEAR"].ToString(),
                                                            header["QT_WEEK"].ToString()));

            html2 = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                html2 += string.Format(html1, dr["NM_STATUS"].ToString(),
                                              dr["NO_CLAIM"].ToString(),
                                              dr["NM_KOR"].ToString(),
                                              dr["NM_PARTNER"].ToString(),
                                              dr["NM_SUPPLIER"].ToString(),
                                              dr["NM_CLAIM"].ToString(),
                                              dr["NM_CAUSE"].ToString(),
                                              dr["NM_ITEM"].ToString(),
                                              dr["NM_REASON"].ToString(),
                                              dr["DC_RMK"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp;"));
            }

            html = string.Format(html, html2);

            return html;
        }

        private string Get미수금(DataRow header)
        {
            DataTable dt;
            string html, html1, html2, query;

            html = @"<table style='width:943px; border:2px solid black; margin-bottom: 20px; font-size: 9pt; font-family: 굴림; border-collapse:collapse; border-spacing:0;'>
    <colgroup width='80px' align='center'></colgroup>
    <colgroup width='70px' align='center'></colgroup>
    <colgroup width='60px' align='center'></colgroup>
    <colgroup width='70px' align='center'></colgroup>
    <colgroup width='120px' align='center'></colgroup>
    <colgroup width='70px' align='center'></colgroup>
    <colgroup width='70px' align='center'></colgroup>
    <colgroup width='273px' align='center'></colgroup>
    <tbody>
        <tr style='height:30px'>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>매출번호</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>매출일자</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>경과일수</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>담당자</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>매출처</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>매출금액</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>미수금액</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>특이사항</th>
        </tr>
        {0}
    </tbody>
</table>";

            html1 = @"<tr style='height:30px'>
            <td style='border:solid 1px black; text-align:center'>{0}</td>
            <td style='border:solid 1px black; text-align:center'>{1}</td>
            <td style='border:solid 1px black; text-align:center'>{2}</td>
            <td style='border:solid 1px black; text-align:center'>{3}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{4}</td>
            <td style='border:solid 1px black; padding-right:10px; text-align:right'>{5}</td>
            <td style='border:solid 1px black; padding-right:10px; text-align:right'>{6}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{7}</td>
        </tr>";

            query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @V_DT_TODAY NVARCHAR(8) = CONVERT(CHAR(8), GETDATE(), 112)

SELECT RH.CD_COMPANY,
       RL.NO_KEY,
	   IH.DT_PROCESS AS DT_IV,
	   RH1.DT_RCP,
	   DATEDIFF(DAY, DATEADD(DAY, IH.DT_RCP_PREARRANGED, IH.DT_PROCESS), @V_DT_TODAY) AS DT_IV_DAY,
	   IH.NM_PARTNER,
	   RH.NO_EMP,
       ME.NM_KOR AS NM_EMP,
	   IH.NM_EXCH,
	   IH.RT_EXCH,
	   ISNULL(IL.AM_EX_CLS, 0) AS AM_EX_CLS,
	   ISNULL(IL.AM_CLS, 0) AS AM_CLS,
	   ISNULL(RH1.AM_RCP_EX, 0) AS AM_RCP_EX,
	   ISNULL(RH1.AM_RCP, 0) AS AM_RCP,
	   (ISNULL(IL.AM_EX_CLS, 0) - ISNULL(RH1.AM_RCP_EX, 0)) AS AM_EX_REMAIN,
	   (ISNULL(IL.AM_CLS, 0) - ISNULL(RH1.AM_RCP, 0)) AS AM_REMAIN,
       RL.DC_RMK
FROM CZ_SA_WEEKLY_RPT_H RH
JOIN CZ_SA_WEEKLY_RPT_L RL ON RL.CD_COMPANY = RH.CD_COMPANY AND RL.NO_EMP = RH.NO_EMP AND RL.DT_YEAR = RH.DT_YEAR AND RL.QT_WEEK = RH.QT_WEEK
LEFT JOIN (SELECT IH.CD_COMPANY, 
				  IH.NO_IV,
		   	  	  IH.DT_PROCESS,
		   	  	  IH.RT_EXCH,
		   	  	  ISNULL(MP.SN_PARTNER, MP.LN_PARTNER) AS NM_PARTNER,
				  MP.DT_RCP_PREARRANGED,
		   	  	  (CASE WHEN AR.NO_EMP IS NOT NULL THEN AR.NO_EMP
		   	  		 	WHEN AR.NO_EMP IS NULL AND IL.NO_SO_EMP IS NOT NULL THEN IL.NO_SO_EMP 
		   	  		 	ELSE IL.NO_PJ_EMP END) AS NO_EMP,
		   	  	  MC.NM_SYSDEF AS NM_EXCH
		   FROM SA_IVH IH
		   JOIN (SELECT IL.CD_COMPANY, IL.NO_IV,
		   			    MAX(SH.NO_EMP) AS NO_SO_EMP,
		   			    MAX(PH.NO_EMP) AS NO_PJ_EMP
		   	     FROM SA_IVL IL
		   	     LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = IL.CD_COMPANY AND SH.NO_SO = IL.NO_SO
		   	     LEFT JOIN SA_PROJECTH PH ON PH.CD_COMPANY = IL.CD_COMPANY AND PH.NO_PROJECT = IL.CD_PJT
		   	     GROUP BY IL.CD_COMPANY, IL.NO_IV) IL
		   ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IV = IH.NO_IV
		   LEFT JOIN CZ_SA_AC_RECEIVABLE AR ON AR.CD_COMPANY = IH.CD_COMPANY AND AR.NO_KEY = IH.NO_IV
		   LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = IH.CD_COMPANY AND MP.CD_PARTNER = IH.CD_PARTNER
		   LEFT JOIN MA_CODEDTL MC ON MC.CD_COMPANY = IH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = IH.CD_EXCH
		   WHERE IH.DT_PROCESS <= @V_DT_TODAY) IH
ON IH.CD_COMPANY = RL.CD_COMPANY AND IH.NO_IV = RL.NO_KEY
LEFT JOIN (SELECT IH.CD_COMPANY, IH.NO_IV, IH.DT_PROCESS,
	       	      SUM(CASE WHEN IL.YN_RETURN = 'Y' THEN -(IL.AM_EX_CLS + CONVERT(NUMERIC(17, 4), ROUND(IL.VAT / IH.RT_EXCH, 2))) 
	       		 							       ELSE (IL.AM_EX_CLS + CONVERT(NUMERIC(17, 4), ROUND(IL.VAT / IH.RT_EXCH, 2))) END) AS AM_EX_CLS,
	       	      SUM(CASE WHEN IL.YN_RETURN = 'Y' THEN -(IL.AM_CLS + IL.VAT) 
	       									       ELSE (IL.AM_CLS + IL.VAT) END) AS AM_CLS 
	       FROM SA_IVH IH
	       JOIN SA_IVL IL ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IV = IH.NO_IV
	       GROUP BY IH.CD_COMPANY, IH.NO_IV, IH.DT_PROCESS) IL
ON IL.CD_COMPANY = IH.CD_COMPANY AND IL.NO_IV = IH.NO_IV AND IL.DT_PROCESS = IH.DT_PROCESS
LEFT JOIN (SELECT RH.CD_COMPANY,
				  RD.NO_IV,
				  MAX(RH.DT_RCP) AS DT_RCP,
				  SUM(RD.AM_RCP_EX) AS AM_RCP_EX,
				  SUM(RD.AM_RCP) AS AM_RCP
		   FROM SA_RCPH RH
		   LEFT JOIN (SELECT RD.CD_COMPANY, RD.NO_RCP, RD.NO_TX AS NO_IV,
  							 (ISNULL(RD.AM_RCP_TX, 0) + ISNULL(RD.AM_PL, 0)) AS AM_RCP,
  							 ISNULL(RD.AM_RCP_TX_EX, 0) AS AM_RCP_EX 
					  FROM SA_RCPD RD 
					  UNION ALL
					  SELECT BD.CD_COMPANY, BD.NO_RCP, BD.NO_IV,
  							 (ISNULL(BD.AM_RCPS, 0) + ISNULL(BD.AM_PL, 0)) AS AM_RCP,
  							 ISNULL(BD.AM_RCPS_EX, 0) AS AM_RCP_EX 
					  FROM SA_BILLSD BD) RD
		   ON RD.CD_COMPANY = RH.CD_COMPANY AND RD.NO_RCP = RH.NO_RCP
		   WHERE RD.NO_IV IS NOT NULL
		   AND RH.DT_RCP <= @V_DT_TODAY
		   GROUP BY RH.CD_COMPANY, RD.NO_IV) RH1
ON RH1.CD_COMPANY = IH.CD_COMPANY AND RH1.NO_IV = IH.NO_IV
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = IH.CD_COMPANY AND ME.NO_EMP = IH.NO_EMP
WHERE RH.CD_COMPANY = '{0}'
AND RH.YN_POST = 'Y'
AND ME.CD_CC = '{1}'
AND RH.DT_YEAR = '{2}'
AND RH.QT_WEEK = '{3}'
AND RL.TP_GUBUN = '005'
ORDER BY DATEDIFF(DAY, DATEADD(DAY, IH.DT_RCP_PREARRANGED, IH.DT_PROCESS), @V_DT_TODAY) DESC";

            dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                            header["CD_CC"].ToString(),
                                                            header["DT_YEAR"].ToString(),
                                                            header["QT_WEEK"].ToString()));

            html2 = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                html2 += string.Format(html1, dr["NO_KEY"].ToString(),
                                              Util.GetTo_DateStringS(dr["DT_IV"]),
                                              dr["DT_IV_DAY"].ToString(),
                                              dr["NM_EMP"].ToString(),
                                              dr["NM_PARTNER"].ToString(),
                                              Util.GetTO_Money(dr["AM_CLS"]),
                                              Util.GetTO_Money(dr["AM_REMAIN"]),
                                              dr["DC_RMK"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp;"));
            }

            html = string.Format(html, html2);

            return html;
        }

        private string Get프로젝트(DataRow header)
        {
            DataTable dt;
            string html, html1, html2, query;

            html = @"<table style='width:943px; border:2px solid black; margin-bottom: 20px; font-size: 9pt; font-family: 굴림; border-collapse:collapse; border-spacing:0;'>
    <colgroup width='200px' align='center'></colgroup>
    <colgroup width='743px' align='center'></colgroup>
    <tbody>
        <tr style='height:30px'>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>제목</th>
            <th style='border:solid 1px black; text-align:center; background-color:Silver'>내용</th>
        </tr>
        {0}
    </tbody>
</table>";

            html1 = @"<tr style='height:30px'>
            <td style='border:solid 1px black; text-align:center'>{0}</td>
            <td style='border:solid 1px black; padding-left:10px; text-align:left'>{1}</td>
        </tr>";

            query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT RH.CD_COMPANY,
       RH.NO_EMP,
       ME.NM_KOR,
       ME.CD_CC,
       MC.NM_CC,
       RL.NO_KEY,
       RL.DC_CATEGORY,
       RL.DT_FROM,
       RL.DT_TO,
       RL.DC_RMK
FROM CZ_SA_WEEKLY_RPT_H RH
JOIN CZ_SA_WEEKLY_RPT_L RL ON RL.CD_COMPANY = RH.CD_COMPANY AND RL.NO_EMP = RH.NO_EMP AND RL.DT_YEAR = RH.DT_YEAR AND RL.QT_WEEK = RH.QT_WEEK
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = RH.CD_COMPANY AND ME.NO_EMP = RH.NO_EMP
LEFT JOIN MA_CC MC ON MC.CD_COMPANY = ME.CD_COMPANY AND MC.CD_CC = ME.CD_CC
WHERE RH.CD_COMPANY = '{0}'
AND RH.YN_POST = 'Y'
AND ME.CD_CC = '{1}'
AND RH.DT_YEAR = '{2}'
AND RH.QT_WEEK = '{3}'
AND RL.TP_GUBUN = '003'";

            dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                            header["CD_CC"].ToString(),
                                                            header["DT_YEAR"].ToString(),
                                                            header["QT_WEEK"].ToString()));

            html2 = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                html2 += string.Format(html1, dr["DC_CATEGORY"].ToString(),
                                              dr["DC_RMK"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp;"));
            }

            html = string.Format(html, html2);

            return html;
        }

        private bool 결재상신(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_FI_GWDOCU", obj);
        }
    }
}

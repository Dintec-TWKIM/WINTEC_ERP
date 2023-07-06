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
    internal class P_CZ_SA_CLAIM_GW
    {
        internal bool 전자결재(DataRow header, DataRow[] line, DataTable 매입처, decimal 대상금액, DateTime 발행일자, DateTime 진행예상종결일자, DateTime 종결일자, 진행단계 선택단계)
        {
            bool isSuccess;
            string strURL, key;

            key = (MA.Login.회사코드 + "-" + D.GetString(header["NO_CLAIM"]) + "-" + (int)선택단계);

            isSuccess = 결재상신(new object[] { GroupWare.GetERP_CD_COMPANY(),
                                                GroupWare.GetERP_CD_PC(),
                                                key,
                                                MA.Login.사원번호,
                                                Global.MainFrame.GetStringToday,
                                                this.GetHtml(header, line, 매입처, 대상금액, 발행일자, 진행예상종결일자, 종결일자, 선택단계),
                                                (Global.MainFrame.DD("클레임보고서") + "(" + Global.MainFrame.DD(((진행단계)선택단계).ToString()) + ") : " + D.GetString(header["NO_CLAIM"]) + "/" + D.GetString(header["NO_SO"])),
                                                "Y",
                                                (Global.MainFrame.LoginInfo.Language == "US" ? 1007 : 1005) });

            if (!isSuccess) return false;

            strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
                     + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
                     + "&cd_pc=" + GroupWare.GetERP_CD_PC()
                     + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
                     + "&login_id=" + MA.Login.사원번호;

            Process.Start("msedge.exe", strURL);

            return isSuccess;
        }

        internal bool 선발행전자결재(DataRow header, DataTable 매입처, DateTime 발행일자, string 선발행통화명)
        {
            bool isSuccess;
            string strURL, key;

            key = (MA.Login.회사코드 + "-" + D.GetString(header["NO_CLAIM"]) + "-CDT");

            isSuccess = 결재상신(new object[] { GroupWare.GetERP_CD_COMPANY(),
                                                GroupWare.GetERP_CD_PC(),
                                                key,
                                                MA.Login.사원번호,
                                                Global.MainFrame.GetStringToday,
                                                this.선발행Html(header, 매입처, 발행일자, 선발행통화명),
                                                (Global.MainFrame.DD("Credit Note 선발행 신청서")+ " : " + D.GetString(header["NO_CLAIM"]) + "/" + D.GetString(header["NO_SO"])),
                                                "Y",
                                                1012 });

            if (!isSuccess) return false;

            strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
                     + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
                     + "&cd_pc=" + GroupWare.GetERP_CD_PC()
                     + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
                     + "&login_id=" + MA.Login.사원번호;

            Process.Start("msedge.exe", strURL);

            return isSuccess;
        }

        internal void 미리보기(DataRow header, DataRow[] line, DataTable 매입처, decimal 대상금액, DateTime 발행일자, DateTime 진행예상종결일자, DateTime 종결일자, 진행단계 선택단계)
        {
            string html;

            html = this.GetHtml(header, line, 매입처, 대상금액, 발행일자, 진행예상종결일자, 종결일자, 선택단계);
            P_CZ_MA_HTML_VIEWER dialog = new P_CZ_MA_HTML_VIEWER(Global.MainFrame.DD("클레임보고서"), html);

            dialog.ShowDialog();
        }

        internal void 선발행미리보기(DataRow header, DataTable 매입처, DateTime 발행일자, string 선발행통화명)
        {
            string html;

            html = this.선발행Html(header, 매입처, 발행일자, 선발행통화명);
            P_CZ_MA_HTML_VIEWER dialog = new P_CZ_MA_HTML_VIEWER(Global.MainFrame.DD("Credit Note 선발행 신청서"), html);

            dialog.ShowDialog();
        }

        private string GetHtml(DataRow header, DataRow[] line, DataTable 매입처, decimal 대상금액, DateTime 발행일자, DateTime 진행예상종결일자, DateTime 종결일자, 진행단계 선택단계)
        {
            string path, body, supplier, title;

            body = string.Empty;

            if (Global.MainFrame.LoginInfo.Language == "US")
                path = Application.StartupPath + "\\download\\gw\\HT_P_CZ_SA_CLAIM_BODY_" + (int)선택단계 + "_ENG.htm";
            else
                path = Application.StartupPath + "\\download\\gw\\HT_P_CZ_SA_CLAIM_BODY_" + (int)선택단계 + ".htm";

            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("@@NO_CLAIM", D.GetString(header["NO_CLAIM"]));
            body = body.Replace("@@DT_INPUT", 발행일자.ToShortDateString());

            body = body.Replace("@@NO_SO", D.GetString(header["NO_SO"]));
            body = body.Replace("@@NO_CONTRACT", D.GetString(header["NO_CONTRACT"]));

            body = body.Replace("@@NM_PARTNER", D.GetString(header["LN_PARTNER"]));
            body = body.Replace("@@NM_VESSEL", D.GetString(header["NM_VESSEL"]));

            if (매입처.Rows.Count > 0)
            {
                if (매입처.Rows.Count == 1)
                    supplier = D.GetString(매입처.Rows[0]["LN_PARTNER"]);
                else
                    supplier = D.GetString(매입처.Rows[0]["LN_PARTNER"]) + " 외 " + (매입처.Rows.Count - 1) + "건";
            }
            else
                supplier = string.Empty;

            body = body.Replace("@@NM_SUPPLIER", supplier);
            body = body.Replace("@@NM_CLAIM", D.GetString(header["NM_CLAIM"]));

            body = body.Replace("@@NM_ITEM", D.GetString(header["NM_ITEM"]));
            body = body.Replace("@@NM_CAUSE", D.GetString(header["NM_CAUSE"]));

            body = body.Replace("@@NM_REQUEST", D.GetString(header["NM_REQUEST"]));
            body = body.Replace("@@NM_RETURN", D.GetString(header["NM_RETURN"]));
            body = body.Replace("@@NM_REASON", D.GetString(header["NM_REASON"]));

            body = body.Replace("@@QT_PACK_WEIGHT", D.GetString(header["QT_PACK_WEIGHT"]));
            body = body.Replace("@@DC_PACK_SIZE", D.GetString(header["DC_PACK_SIZE"]));

            body = body.Replace("@@TXT_CLAIM_ITEM_LIST", this.클레임품목리스트(line));
            body = body.Replace("@@TXT_INVENTORY_INPUT_LIST", this.재고입고내역(D.GetString(header["NO_CLAIM"]), D.GetString(header["NO_SO"])));

            switch (선택단계)
            {
                case 진행단계.접수:
                    {
                        body = body.Replace("@@DC_RECEIVE", header["DC_RECEIVE"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp;"));

                        body = body.Replace("@@AM_CLAIM", 대상금액.ToString("N"));
                        body = body.Replace("@@AM_TOTAL_RCV", D.GetDecimal(header["AM_TOTAL_RCV"]).ToString("N"));
                    }
                    break;
                case 진행단계.진행:
                    {
                        body = body.Replace("@@DC_PROGRESS", header["DC_PROGRESS"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp;"));

                        body = body.Replace("@@AM_TOTAL_RCV", D.GetDecimal(header["AM_TOTAL_RCV"]).ToString("N"));

                        body = body.Replace("@@DT_EXPECTED_CLOSING_PRO", 진행예상종결일자.ToShortDateString());
                        body = body.Replace("@@AM_TOTAL_PRO", D.GetDecimal(header["AM_TOTAL_PRO"]).ToString("N"));
                    }
                    break;
                case 진행단계.종결:
                    {
                        body = body.Replace("@@DC_CLOSING", header["DC_CLOSING"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp;"));

                        if (header["TP_CAUSE"].ToString() == "1")
                        {
                            if (Global.MainFrame.LoginInfo.Language == "US")
                                title = "Methods of Supplier Compensation";
                            else
                                title = "매 입 처 보 상 방 안";

                            body = body.Replace("@@DC_SUPPLIER_REWARD", @"<tr style='height:130px'>
                                                                          <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + title + "</th>" +
                                                                         "<td style='padding: 10px; border: solid 1px black; text-align:left' colspan='3' valign='top'>" +
                                                                          ("** " + header["NM_SUPPLIER_REWARD"].ToString() + Environment.NewLine + Environment.NewLine + header["DC_SUPPLIER_REWARD"].ToString()).Replace(Environment.NewLine, "<br>") + "</td>" +
                                                                         "</tr>");
                        }
                        else
                        {
                            body = body.Replace("@@DC_SUPPLIER_REWARD", string.Empty);
                        }

                        body = body.Replace("@@DT_CLOSING", 종결일자.ToShortDateString());
                        body = body.Replace("@@AM_TOTAL_CLS", D.GetDecimal(header["AM_TOTAL_CLS"]).ToString("N"));
                    }
                    break;
            }

            return body;
        }

        private string 선발행Html(DataRow header, DataTable 매입처, DateTime 발행일자, string 선발행통화명)
        {
            string path, body, supplier;

            body = string.Empty;
            path = Application.StartupPath + "\\download\\gw\\HT_P_CZ_SA_CLAIM_CREDIT_NOTE_DETAIL.htm";

            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("@@NO_CLAIM", D.GetString(header["NO_CLAIM"]));
            body = body.Replace("@@DT_INPUT", 발행일자.ToShortDateString());

            body = body.Replace("@@NO_SO", D.GetString(header["NO_SO"]));
            body = body.Replace("@@NO_CONTRACT", D.GetString(header["NO_CONTRACT"]));

            body = body.Replace("@@NM_PARTNER", D.GetString(header["LN_PARTNER"]));
            body = body.Replace("@@NM_VESSEL", D.GetString(header["NM_VESSEL"]));

            if (매입처.Rows.Count > 0)
            {
                if (매입처.Rows.Count == 1)
                    supplier = D.GetString(매입처.Rows[0]["LN_PARTNER"]);
                else
                    supplier = D.GetString(매입처.Rows[0]["LN_PARTNER"]) + " 외 " + (매입처.Rows.Count - 1) + "건";
            }
            else
                supplier = string.Empty;

            body = body.Replace("@@NM_SUPPLIER", supplier);
            body = body.Replace("@@NM_CLAIM", D.GetString(header["NM_CLAIM"]));

            body = body.Replace("@@NM_ITEM", D.GetString(header["NM_ITEM"]));
            body = body.Replace("@@NM_CAUSE", D.GetString(header["NM_CAUSE"]));

            body = body.Replace("@@NM_REQUEST", D.GetString(header["NM_REQUEST"]));
            body = body.Replace("@@NM_RETURN", D.GetString(header["NM_RETURN"]));
            body = body.Replace("@@NM_REASON", D.GetString(header["NM_REASON"]));

            body = body.Replace("@@DC_CREDIT_NOTE", header["DC_CREDIT_NOTE"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp;"));

            body = body.Replace("@@NM_CREDIT_EXCH", 선발행통화명);
            body = body.Replace("@@AM_CREDIT", D.GetDecimal(header["AM_CREDIT"]).ToString("N"));
            
            return body;
        }

        private bool 결재상신(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_FI_GWDOCU", obj);
        }

        private string 클레임품목리스트(DataRow[] line)
        {
            string html, html1 = string.Empty, subject = string.Empty;

            if (line == null || line.Length == 0)
                html = string.Empty;
            else
            {
                foreach (DataRow dr in line)
                {
                    if (string.IsNullOrEmpty(subject) && !string.IsNullOrEmpty(D.GetString(dr["NM_SUBJECT"])))
                        subject = D.GetString(dr["NM_SUBJECT"]);

                    html1 += @"<tr style='height:30px'>
                                    <th style='padding: 10px; border:solid 1px black; text-align:center; font-weight:normal'>" + D.GetString(dr["NO_DSP"]) + @"</th>
                                    <th style='padding: 10px; border:solid 1px black; text-align:center; font-weight:normal'>" + D.GetString(dr["CD_ITEM_PARTNER"]) + @"</th>
                                    <th style='padding: 10px; border:solid 1px black; text-align:center; font-weight:normal' colspan='2'>" + D.GetString(dr["NM_ITEM_PARTNER"]) + @"</th>
                                    <th style='padding: 10px; border:solid 1px black; text-align:center; font-weight:normal'>" + D.GetInt(dr["QT_CLAIM"]) + @"</th>
                                    <th style='padding: 10px; border:solid 1px black; text-align:center; font-weight:normal'>" + D.GetString(dr["LN_PARTNER"]) + @"</th>
                                    <th style='padding: 10px; border:solid 1px black; text-align:center; font-weight:normal'>" + Util.GetTO_Money(dr["AM_CLAIM"]) + @"</th>
                                    <th style='padding: 10px; border:solid 1px black; text-align:center; font-weight:normal'>" + Util.GetTO_Money(dr["AM_CLAIM_PO"]) + @"</th>
                                    <th style='padding: 10px; border:solid 1px black; text-align:center; font-weight:normal'>" + Util.GetTO_Money(dr["AM_PROFIT"]) + @"</th>
                                    <th style='padding: 10px; border:solid 1px black; text-align:center; font-weight:normal'>" + Decimal.Round(D.GetDecimal(dr["RT_PROFIT"]), 2, MidpointRounding.AwayFromZero) + @"%</th>
                                    <th style='padding: 10px; border:solid 1px black; text-align:center; font-weight:normal'>" + Util.GetTo_DateStringS(dr["DT_OUT"]) + @"</th>
                               </tr>";
                }

                html = "<div style='text-align:left; font-weight: bold;'> *** " + (Global.MainFrame.LoginInfo.Language == "US" ? "Claim Item List" : "클레임 품목 리스트") + @"</div>

                        <table style='width:100%; border:2px solid black; margin-bottom: 20px; font-size: 9pt; font-family: 굴림; border-collapse:collapse; border-spacing:0;'>
                            <colgroup width='10%' align='center'></colgroup>
                            <colgroup width='10%' align='center'></colgroup>
                            <colgroup width='10%' align='center'></colgroup>
                            <colgroup width='10%' align='center'></colgroup>
                            <colgroup width='10%' align='center'></colgroup>
                            <colgroup width='10%' align='center'></colgroup>
                            <colgroup width='10%' align='center'></colgroup>
                            <colgroup width='10%' align='center'></colgroup>
                            <colgroup width='10%' align='center'></colgroup>
                            <colgroup width='10%' align='center'></colgroup>                            
                            <tbody>
                            <tr style='height:30px'>
                                <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "Subject" : "주제") + @"</th>
                                <th style='padding: 10px; border:solid 1px black; text-align:center; font-weight:normal' colspan='10'>" + subject + @"</th>
                            </tr>
                            <tr style='height:30px'>
                                <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "Seq." : "순번") + @"</th>
                                <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "Item Code" : "품목코드") + @"</th>
                                <th style='border:solid 1px black; text-align:center; background-color:Silver' colspan='2'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "Item Name" : "품목명") + @"</th>
                                <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "Quantity" : "수량") + @"</th>
                                <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "Supplier" : "매입처") + @"</th>
                                <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "SO Amt." : "수주금액") + @"</th>
                                <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "PO Amt." : "발주금액") + @"</th>
                                <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "Profit" : "이윤") + @"</th>
                                <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "Profit Rate" : "이윤율") + @"</th>
                                <th style='border:solid 1px black; text-align:center; background-color:Silver'>" + (Global.MainFrame.LoginInfo.Language == "US" ? "GI Date" : "출고일자") + @"</th>
                            </tr>";
                html += html1;

                html += @"  </tbody>
                          </table>";
            }

            return html;
        }

        private string 재고입고내역(string 클레임번호, string 수주번호)
        {
            string html, query;
            DataTable dt;

            query = @"SELECT IL2.DT_IO,
                      	     ME.NM_KOR,
                      	     IL2.CD_ITEM,
                      	     MI.NM_ITEM,
                      	     IL1.QT_IO AS QT_IO_OUT,
                      	     IL2.QT_IO AS QT_IO_IN
                      FROM CZ_SA_CLAIML CL WITH(NOLOCK)
                      JOIN PU_POL PL WITH(NOLOCK) ON PL.CD_COMPANY = CL.CD_COMPANY AND PL.NO_SO = CL.NO_SO AND PL.NO_SOLINE = CL.SEQ_SO
                      JOIN MM_QTIO IL WITH(NOLOCK) ON IL.CD_COMPANY = PL.CD_COMPANY AND IL.NO_PSO_MGMT = PL.NO_PO AND IL.NO_PSOLINE_MGMT = PL.NO_LINE
                      JOIN MM_QTIO IL1 WITH(NOLOCK) ON IL1.CD_COMPANY = IL.CD_COMPANY AND IL1.NO_IO_MGMT = IL.NO_IO AND IL1.NO_IOLINE_MGMT = IL.NO_IOLINE
                      JOIN MM_QTIO IL2 WITH(NOLOCK) ON IL2.CD_COMPANY = IL.CD_COMPANY AND IL2.NO_IO_MGMT = IL1.NO_IO AND IL2.NO_IOLINE_MGMT = IL1.NO_IOLINE
                      LEFT JOIN MA_EMP ME WITH(NOLOCK) ON ME.CD_COMPANY = IL2.CD_COMPANY AND ME.NO_EMP = IL2.NO_EMP
                      LEFT JOIN MA_PITEM MI WITH(NOLOCK) ON MI.CD_COMPANY = IL2.CD_COMPANY AND MI.CD_PLANT = IL2.CD_PLANT AND MI.CD_ITEM = IL2.CD_ITEM
                      WHERE CL.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                     "AND CL.NO_CLAIM = '" + 클레임번호 + "'" + Environment.NewLine +
                     "AND CL.NO_SO = '" + 수주번호 + "'" + Environment.NewLine +
                    @"AND IL1.CD_QTIOTP = '400'
                      AND IL2.CD_QTIOTP = '410'" + Environment.NewLine +
                    @"UNION ALL
                      SELECT IL2.DT_IO,
                      	     ME.NM_KOR,
                      	     IL2.CD_ITEM,
                      	     MI.NM_ITEM,
                      	     IL1.QT_IO AS QT_IO_OUT,
                      	     IL2.QT_IO AS QT_IO_IN
                      FROM CZ_SA_CLAIML CL WITH(NOLOCK)
                      JOIN SA_SOL SL WITH(NOLOCK) ON SL.CD_COMPANY = CL.CD_COMPANY AND SL.NO_SO = CL.NO_SO AND SL.SEQ_SO = CL.SEQ_SO
                      JOIN MM_QTIO IL WITH(NOLOCK) ON IL.CD_COMPANY = SL.CD_COMPANY AND IL.NO_PSO_MGMT = SL.NO_SO AND IL.NO_PSOLINE_MGMT = SL.SEQ_SO
                      JOIN MM_QTIO IL1 WITH(NOLOCK) ON IL1.CD_COMPANY = IL.CD_COMPANY AND IL1.NO_IO_MGMT = IL.NO_IO AND IL1.NO_IOLINE_MGMT = IL.NO_IOLINE
                      JOIN MM_QTIO IL2 WITH(NOLOCK) ON IL2.CD_COMPANY = IL.CD_COMPANY AND IL2.NO_IO_MGMT = IL1.NO_IO AND IL2.NO_IOLINE_MGMT = IL1.NO_IOLINE
                      LEFT JOIN MA_EMP ME WITH(NOLOCK) ON ME.CD_COMPANY = IL2.CD_COMPANY AND ME.NO_EMP = IL2.NO_EMP
                      LEFT JOIN MA_PITEM MI WITH(NOLOCK) ON MI.CD_COMPANY = IL2.CD_COMPANY AND MI.CD_PLANT = IL2.CD_PLANT AND MI.CD_ITEM = IL2.CD_ITEM
                      WHERE CL.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                     "AND CL.NO_CLAIM = '" + 클레임번호 + "'" + Environment.NewLine +
                     "AND CL.NO_SO = '" + 수주번호 + "'" + Environment.NewLine +
					@"AND IL1.CD_QTIOTP = '400'
                      AND IL2.CD_QTIOTP = '410'
					  UNION ALL
					  SELECT IL2.DT_IO,
                      	     ME.NM_KOR,
                      	     IL2.CD_ITEM,
                      	     MI.NM_ITEM,
                      	     IL1.QT_IO AS QT_IO_OUT,
                      	     IL2.QT_IO AS QT_IO_IN
					  FROM PU_POL PL WITH(NOLOCK)
					  JOIN MM_QTIO IL WITH(NOLOCK) ON IL.CD_COMPANY = PL.CD_COMPANY AND IL.NO_PSO_MGMT = PL.NO_PO AND IL.NO_PSOLINE_MGMT = PL.NO_LINE
					  JOIN MM_QTIO IL1 WITH(NOLOCK) ON IL1.CD_COMPANY = IL.CD_COMPANY AND IL1.NO_IO_MGMT = IL.NO_IO AND IL1.NO_IOLINE_MGMT = IL.NO_IOLINE
					  JOIN MM_QTIO IL2 WITH(NOLOCK) ON IL2.CD_COMPANY = IL.CD_COMPANY AND IL2.NO_IO_MGMT = IL1.NO_IO AND IL2.NO_IOLINE_MGMT = IL1.NO_IOLINE
					  LEFT JOIN MA_EMP ME WITH(NOLOCK) ON ME.CD_COMPANY = IL2.CD_COMPANY AND ME.NO_EMP = IL2.NO_EMP
					  LEFT JOIN MA_PITEM MI WITH(NOLOCK) ON MI.CD_COMPANY = IL2.CD_COMPANY AND MI.CD_PLANT = IL2.CD_PLANT AND MI.CD_ITEM = IL2.CD_ITEM
					  WHERE PL.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
					 "AND PL.NO_SO = '" + 클레임번호 + "'" + Environment.NewLine +
					@"AND IL1.CD_QTIOTP = '400'
					  AND IL2.CD_QTIOTP = '410'";

            dt = DBHelper.GetDataTable(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (Global.MainFrame.LoginInfo.Language == "US")
                {
                    html = @"<div style='text-align:left; font-weight: bold;'>*** Inventory Receving List</div>

                             <table style='width:100%; border:2px solid black; margin-bottom: 20px; font-size: 9pt; font-family: 굴림; border-collapse:collapse; border-spacing:0;'>
                                <colgroup width='10%' align='center'></colgroup>
                                <colgroup width='10%' align='center'></colgroup>
                                <colgroup width='10%' align='center'></colgroup>
                                <colgroup width='40%' align='center'></colgroup>
                                <colgroup width='10%' align='center'></colgroup>
                                <colgroup width='10%' align='center'></colgroup>
                                <tbody>
                                <tr style='height:30px'>
                                    <th style='border:solid 1px black; text-align:center; background-color:Silver'>G/R Date</th>
                                    <th style='border:solid 1px black; text-align:center; background-color:Silver'>P.I.C</th>
                                    <th style='border:solid 1px black; text-align:center; background-color:Silver'>Stock Code</th>
                                    <th style='border:solid 1px black; text-align:center; background-color:Silver'>Stock Name</th>
                                    <th style='border:solid 1px black; text-align:center; background-color:Silver'>Req. Qty</th>
                                    <th style='border:solid 1px black; text-align:center; background-color:Silver'>G/R Qty</th>
                                </tr>";
                }
                else
                {
                    html = @"<div style='text-align:left; font-weight: bold;'>*** 재고 입고 내역</div>

                             <table style='width:100%; border:2px solid black; margin-bottom: 20px; font-size: 9pt; font-family: 굴림; border-collapse:collapse; border-spacing:0;'>
                                <colgroup width='10%' align='center'></colgroup>
                                <colgroup width='10%' align='center'></colgroup>
                                <colgroup width='10%' align='center'></colgroup>
                                <colgroup width='40%' align='center'></colgroup>
                                <colgroup width='10%' align='center'></colgroup>
                                <colgroup width='10%' align='center'></colgroup>
                                <tbody>
                                <tr style='height:30px'>
                                    <th style='border:solid 1px black; text-align:center; background-color:Silver'>입 고 일 자</th>
                                    <th style='border:solid 1px black; text-align:center; background-color:Silver'>입 고 자</th>
                                    <th style='border:solid 1px black; text-align:center; background-color:Silver'>재 고 코 드</th>
                                    <th style='border:solid 1px black; text-align:center; background-color:Silver'>재 고 명</th>
                                    <th style='border:solid 1px black; text-align:center; background-color:Silver'>요 청 수 량</th>
                                    <th style='border:solid 1px black; text-align:center; background-color:Silver'>입 고 수 량</th>
                                </tr>";
                }

                foreach (DataRow dr in dt.Rows)
                {
                    html += @"<tr style='height:30px'>
                                 <th style='border:solid 1px black; text-align:center; font-weight:normal'>" + Util.GetTo_DateStringS(dr["DT_IO"]) + "</th>" + Environment.NewLine +
                                "<th style='border:solid 1px black; text-align:center; font-weight:normal'>" + D.GetString(dr["NM_KOR"]) + "</th>" + Environment.NewLine +
                                "<th style='border:solid 1px black; text-align:center; font-weight:bold'>" + D.GetString(dr["CD_ITEM"]) + "</th>" + Environment.NewLine +
                                "<th style='border:solid 1px black; text-align:center; font-weight:normal'>" + D.GetString(dr["NM_ITEM"]) + "</th>" + Environment.NewLine +
                                "<th style='border:solid 1px black; text-align:center; font-weight:normal'>" + D.GetInt(dr["QT_IO_OUT"]) + "</th>" + Environment.NewLine +
                                "<th style='border:solid 1px black; text-align:center; font-weight:normal'>" + D.GetInt(dr["QT_IO_IN"]) + "</th>" + Environment.NewLine +
                             "</tr>";
                }

                html += @"</tbody>
                      </table>";
            }
            else
                html = string.Empty;

            return html;
        }

        
    }
}

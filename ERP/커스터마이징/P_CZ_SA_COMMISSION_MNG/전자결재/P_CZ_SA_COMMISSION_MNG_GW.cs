using System.Diagnostics;
using System.Text;
using System.Web;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System;
using Dass.FlexGrid;
using Duzon.ERPU.MF;

namespace cz
{
    internal class P_CZ_SA_COMMISSION_MNG_GW
    {
        internal bool 전자결재(string 커미션번호, DataRow[] 커미션)
        {
            bool isSuccess;
            string strURL, key, 결재문서제목;

            DataTable dt = ComFunc.getGridGroupBy(커미션, new string[] { "LN_PARTNER" }, true);

            key = (MA.Login.회사코드 + "-" + 커미션번호);

            if (dt.Rows.Count == 1 && string.IsNullOrEmpty(dt.Rows[0]["LN_PARTNER"].ToString()))
                결재문서제목 = Global.MainFrame.DD("커미션지급품의서");
            else if (dt.Rows.Count == 1)
                결재문서제목 = (Global.MainFrame.DD("커미션지급품의서") + " - " + dt.Rows[0]["LN_PARTNER"]);
            else
                결재문서제목 = (Global.MainFrame.DD("커미션지급품의서") + " - " + dt.Rows[0]["LN_PARTNER"] + " 외 " + (dt.Rows.Count - 1).ToString());

            isSuccess = 결재상신(new object[] { GroupWare.GetERP_CD_COMPANY(),
                                                GroupWare.GetERP_CD_PC(),
                                                key,
                                                MA.Login.사원번호,
                                                Global.MainFrame.GetStringToday,
                                                this.Get커미션Html(커미션),
                                                결재문서제목,
                                                "Y",
                                                1015 });

            if (!isSuccess) return false;

            strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
                     + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
                     + "&cd_pc=" + GroupWare.GetERP_CD_PC()
                     + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
                     + "&login_id=" + MA.Login.사원번호;

            Process.Start("msedge.exe", strURL);

            return isSuccess;
        }

        private string Get커미션Html(DataRow[] 커미션)
        {
            DataTable dt, dt지급내역;
            string path, body, format;
            string 거래처정보, 지급이력, 지급내용;

            body = string.Empty;
            path = Application.StartupPath + "\\download\\gw\\HT_P_CZ_SA_COMMISSION_DETAIL.htm";

            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                body = reader.ReadToEnd();
            }

            거래처정보 = string.Empty;
            지급이력 = string.Empty;
            지급내용 = string.Empty;

            dt지급내역 = new DataTable();
            dt지급내역.Columns.Add("NM_EXCH");
            dt지급내역.Columns.Add("AM_COMMISSION", typeof(decimal));

            foreach (DataRow dr in 커미션)
            {
                거래처정보 += "<div style ='font-weight: bold; font-size: 10pt; font-family: 굴림;'>" + dr["LN_PARTNER"].ToString() + (dr["YN_ADDED"].ToString() == "Y" ? " (추가지급)" : "")  + "</div>";

                dt = DBHelper.GetDataTable("SP_CZ_SA_COMMISSION_MNGL_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                        dr["NO_COMMISSION"].ToString() });

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        if (!string.IsNullOrEmpty(dr1["NM_EMAIL"].ToString()) &&
                            !string.IsNullOrEmpty(dr1["NM_DEPT"].ToString()) &&
                            !string.IsNullOrEmpty(dr1["NM_DUTY_RESP"].ToString()))
                            거래처정보 += "<div style ='font-weight: normal; font-size: 9pt; font-family: 굴림;'>" + "&nbsp&nbsp&nbsp&nbsp◈ " + dr1["NM_PTR"].ToString() + " (" + dr1["NM_EMAIL"].ToString() + ")" + " " + dr1["NM_DEPT"].ToString() + "/" + dr1["NM_DUTY_RESP"].ToString() + "</div>";
                        else if (!string.IsNullOrEmpty(dr1["NM_EMAIL"].ToString()) &&
                                !string.IsNullOrEmpty(dr1["NM_DEPT"].ToString()))
                            거래처정보 += "<div style ='font-weight: normal; font-size: 9pt; font-family: 굴림;'>" + "&nbsp&nbsp&nbsp&nbsp◈ " + dr1["NM_PTR"].ToString() + " (" + dr1["NM_EMAIL"].ToString() + ")" + " " + dr1["NM_DEPT"].ToString() + "</div>";
                        else if (!string.IsNullOrEmpty(dr1["NM_EMAIL"].ToString()))
                            거래처정보 += "<div style ='font-weight: normal; font-size: 9pt; font-family: 굴림;'>" + "&nbsp&nbsp&nbsp&nbsp◈ " + dr1["NM_PTR"].ToString() + " (" + dr1["NM_EMAIL"].ToString() + ")" + "</div>";
                        else
                            거래처정보 += "<div style ='font-weight: normal; font-size: 9pt; font-family: 굴림;'>" + "&nbsp&nbsp&nbsp&nbsp◈ " + dr1["NM_PTR"].ToString() + "</div>";
                    }
                }

                거래처정보 += "<br>";
                
                if (커미션.Length > 1)
                {
                    지급이력 += @"<tr style='height:30px'>
                                    <td style='padding:10px; border:solid 1px black; text-align:left; font-weight:normal'' colspan='7'>" + dr["LN_PARTNER"].ToString() + @"</td>
                                  </tr>";
                }
                
                dt = DBHelper.GetDataTable("SP_CZ_SA_COMMISSION_MNG_GW", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                        dr["CD_PARTNER"].ToString(),
                                                                                        dr["NO_COMMISSION"].ToString() });

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        if (커미션.Length == 1)
                        {
                            #region 1건 결재시
                            if (dr1["NO_COMMISSION"].ToString() == dr["NO_COMMISSION"].ToString())
                            {
                                #region 이번에 올리는 건
                                DataRow dataRow = dt지급내역.Rows.Add();
                                dataRow["NM_EXCH"] = dr1["NM_EXCH"].ToString();
                                dataRow["AM_COMMISSION"] = D.GetDecimal(dr1["AM_COMMISSION"]);

                                if (dt.Rows.Count > 1)
                                {
                                    format = @"<tr style='height:30px'>
                                                <td style='padding:10px; border:solid 1px black; text-align:center; font-weight:bold'>{0}</td>
                                                <td style='padding:10px; border:solid 1px black; text-align:center; font-weight:bold'>{1}</td>
                                                <td style='padding:10px; border:solid 1px black; text-align:right; font-weight:bold'>{2}</td>
                                                <td style='padding:10px; border:solid 1px black; text-align:right; font-weight:bold{8}'>{3}</td>
                                                <td style='padding:10px; border:solid 1px black; text-align:right; font-weight:bold'>{4}</td>
                                                <td style='padding:10px; border:solid 1px black; text-align:right; font-weight:bold'>{5}</td>
                                                <td style='padding:10px; border:solid 1px black; text-align:right; font-weight:bold'>{6}</td>
                                                <td style='padding:10px; border:solid 1px black; text-align:right; font-weight:bold'>{7}</td>
                                               </tr>";

                                    지급이력 += string.Format(format, dr1["DT_PERIOD"].ToString(),
                                                                      dr1["NM_EXCH"].ToString(),
                                                                      Util.GetTO_Money(dr1["AM_TARGET"]),
                                                                      Util.GetTO_Money(dr1["AM_OUTSTANDING"]),
                                                                      Util.GetTO_Money(dr1["AM_COMMISSION_BEFORE"]),
                                                                      Util.GetTO_Money(dr1["AM_COMMISSION"]),
                                                                      string.Format("{0:0.00}", D.GetDecimal(dr1["RT_PROFIT"])) + " %",
                                                                      string.Format("{0:0.00}", D.GetDecimal(dr1["RT_PROFIT1"])) + " %",
                                                                      (D.GetDecimal(dr1["AM_OUTSTANDING"]) > 0 ? "; color: #FF0000;" : string.Empty));
                                }
                                else
                                {
                                    format = @"<tr style='height:30px'>
                                                <td style='padding:10px; border:solid 1px black; text-align:center'>{0}</td>
                                                <td style='padding:10px; border:solid 1px black; text-align:center'>{1}</td>
                                                <td style='padding:10px; border:solid 1px black; text-align:right'>{2}</td>
                                                <td style='padding:10px; border:solid 1px black; text-align:right{8}'>{3}</td>
                                                <td style='padding:10px; border:solid 1px black; text-align:right'>{4}</td>
                                                <td style='padding:10px; border:solid 1px black; text-align:right'>{5}</td>
                                                <td style='padding:10px; border:solid 1px black; text-align:right'>{6}</td>
                                                <td style='padding:10px; border:solid 1px black; text-align:right'>{7}</td>
                                               </tr>";

                                    지급이력 += string.Format(format, dr1["DT_PERIOD"].ToString(),
                                                                      dr1["NM_EXCH"].ToString(),
                                                                      Util.GetTO_Money(dr1["AM_TARGET"]),
                                                                      Util.GetTO_Money(dr1["AM_OUTSTANDING"]),
                                                                      Util.GetTO_Money(dr1["AM_COMMISSION_BEFORE"]),
                                                                      Util.GetTO_Money(dr1["AM_COMMISSION"]),
                                                                      string.Format("{0:0.00}", D.GetDecimal(dr1["RT_PROFIT"])) + " %",
                                                                      string.Format("{0:0.00}", D.GetDecimal(dr1["RT_PROFIT1"])) + " %",
                                                                      (D.GetDecimal(dr1["AM_OUTSTANDING"]) > 0 ? "; color: #FF0000;" : string.Empty));
                                }
                                #endregion
                            }
                            else
                            {
                                #region 이전에 올린 건
                                format = @"<tr style='height:30px'>
                                            <td style='padding:10px; border:solid 1px black; text-align:center'>{0}</td>
                                            <td style='padding:10px; border:solid 1px black; text-align:center'>{1}</td>
                                            <td style='padding:10px; border:solid 1px black; text-align:right'>{2}</td>
                                            <td style='padding:10px; border:solid 1px black; text-align:right{8}'>{3}</td>
                                            <td style='padding:10px; border:solid 1px black; text-align:right'>{4}</td>
                                            <td style='padding:10px; border:solid 1px black; text-align:right'>{5}</td>
                                            <td style='padding:10px; border:solid 1px black; text-align:right'>{6}</td>
                                            <td style='padding:10px; border:solid 1px black; text-align:right'>{7}</td>
                                           </tr>";

                                지급이력 += string.Format(format, dr1["DT_PERIOD"].ToString(),
                                                                  dr1["NM_EXCH"].ToString(),
                                                                  Util.GetTO_Money(dr1["AM_TARGET"]),
                                                                  Util.GetTO_Money(dr1["AM_OUTSTANDING"]),
                                                                  Util.GetTO_Money(dr1["AM_COMMISSION_BEFORE"]),
                                                                  Util.GetTO_Money(dr1["AM_COMMISSION"]),
                                                                  string.Format("{0:0.00}", D.GetDecimal(dr1["RT_PROFIT"])) + " %",
                                                                  string.Format("{0:0.00}", D.GetDecimal(dr1["RT_PROFIT1"])) + " %",
                                                                  (D.GetDecimal(dr1["AM_OUTSTANDING"]) > 0 ? "; color: #FF0000;" : string.Empty));
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region 여러 건 결재시
                            if (dr1["NO_COMMISSION"].ToString() == dr["NO_COMMISSION"].ToString())
                            {
                                DataRow dataRow = dt지급내역.Rows.Add();
                                dataRow["NM_EXCH"] = dr1["NM_EXCH"].ToString();
                                dataRow["AM_COMMISSION"] = D.GetDecimal(dr1["AM_COMMISSION"]);

                                format = @"<tr style='height:30px'>
                                            <td style='padding:10px; border:solid 1px black; text-align:center'>{0}</td>
                                            <td style='padding:10px; border:solid 1px black; text-align:center'>{1}</td>
                                            <td style='padding:10px; border:solid 1px black; text-align:right'>{2}</td>
                                            <td style='padding:10px; border:solid 1px black; text-align:right{8}'>{3}</td>
                                            <td style='padding:10px; border:solid 1px black; text-align:right'>{4}</td>
                                            <td style='padding:10px; border:solid 1px black; text-align:right'>{5}</td>
                                            <td style='padding:10px; border:solid 1px black; text-align:right'>{6}</td>
                                            <td style='padding:10px; border:solid 1px black; text-align:right'>{7}</td>
                                           </tr>";

                                지급이력 += string.Format(format, dr1["DT_PERIOD"].ToString(),
                                                                  dr1["NM_EXCH"].ToString(),
                                                                  Util.GetTO_Money(dr1["AM_TARGET"]),
                                                                  Util.GetTO_Money(dr1["AM_OUTSTANDING"]),
                                                                  Util.GetTO_Money(dr1["AM_COMMISSION_BEFORE"]),
                                                                  Util.GetTO_Money(dr1["AM_COMMISSION"]),
                                                                  string.Format("{0:0.00}", D.GetDecimal(dr1["RT_PROFIT"])) + " %",
                                                                  string.Format("{0:0.00}", D.GetDecimal(dr1["RT_PROFIT1"])) + " %",
                                                                  (D.GetDecimal(dr1["AM_OUTSTANDING"]) > 0 ? "; color: #FF0000;" : string.Empty));
                            }
                            #endregion
                        }
                    }
                }

                if (!string.IsNullOrEmpty(dr["DC_COMMISSION"].ToString()))
                {
                    if (커미션.Length > 1)
                        지급내용 += "<div style ='font-weight: bold; font-size: 9pt; font-family: 굴림;'>" + dr["LN_PARTNER"].ToString().Replace(Environment.NewLine, "<br>") + "</div>" + "<br>";

                    지급내용 += "<div style ='font-weight: normal; font-size: 9pt; font-family: 굴림;'>" + dr["DC_COMMISSION"].ToString().Replace(Environment.NewLine, "<br>") + "</div>";

                    if (커미션.Length > 1)
                        지급내용 += "<br>";
                }
            }

            if (커미션.Length > 1)
            {
                DataTable dt1 = ComFunc.getGridGroupBy(dt지급내역, new string[] { "NM_EXCH" }, true);

                for (int index = 0; index < dt1.Rows.Count; index ++)
                {
                    format = @"<tr style='height:30px'>
                                <th style='padding:10px; border:solid 1px black; text-align:center; background-color:Silver' colspan='5'>총지급금액</td>
                                <td style='padding:10px; border:solid 1px black; text-align:center'>{0}</td>
                                <td style='padding:10px; border:solid 1px black; text-align:right'>{1}</td>
                               </tr>";

                    지급이력 += string.Format(format, dt1.Rows[index]["NM_EXCH"].ToString(),
                                                      Util.GetTO_Money(dt지급내역.Compute("SUM(AM_COMMISSION)", "NM_EXCH = '" + dt1.Rows[index]["NM_EXCH"].ToString() + "'")));
                }
            }

            body = body.Replace("@@PARTNER_INFO", 거래처정보);
            body = body.Replace("@@COMMISSION_HISTORY", 지급이력);
            body = body.Replace("@@DC_COMMISSION", 지급내용);

            return body;
        }

        internal void 문서보기(DataRow[] 커미션)
        {
            string html;

            html = this.Get커미션Html(커미션);
            P_CZ_MA_HTML_VIEWER dialog = new P_CZ_MA_HTML_VIEWER(Global.MainFrame.DD("커미션지급품의서"), html);

            dialog.ShowDialog();
        }

        private bool 결재상신(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_FI_GWDOCU", obj);
        }
    }
}

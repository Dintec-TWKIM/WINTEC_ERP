using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using System.Windows.Forms;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    internal class P_CZ_SA_BUSINESS_TRIP_MNG_GW
    {
        internal bool 전자결재(DataRow 출장보고서, DataRow[] 출장자, DataRow[] 출장일정, FlexGrid 출장경비, DataRow[] 미팅메모)
        {
            bool isSuccess;
            string strURL, key;

            key = (MA.Login.회사코드 + "-" + D.GetString(출장보고서["NO_BIZ_TRIP"]));

            isSuccess = 결재상신(new object[] { GroupWare.GetERP_CD_COMPANY(),
                                                GroupWare.GetERP_CD_PC(),
                                                key,
                                                MA.Login.사원번호,
                                                Global.MainFrame.GetStringToday,
                                                this.GetHtml(출장보고서, 출장자, 출장일정, 출장경비, 미팅메모),
                                                Global.MainFrame.DD("출장보고서"),
                                                "Y",
                                                1018 });

            if (!isSuccess) return false;

            strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
                     + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
                     + "&cd_pc=" + GroupWare.GetERP_CD_PC()
                     + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
                     + "&login_id=" + MA.Login.사원번호;

            Process.Start("msedge.exe", strURL);

            return isSuccess;
        }

        private string GetHtml(DataRow 출장보고서, DataRow[] dr출장자, DataRow[] dr출장일정, FlexGrid grid출장경비, DataRow[] dr미팅메모)
        {
            string path, body, 출장자, 출장기간, 출장일정, 출장경비, 미팅메모, 참석자외부, 참석자내부;
            decimal 출장경비합계;
            DataRow[] dr출장경비;
            DataSet ds;

            body = string.Empty;
            path = Application.StartupPath + "\\download\\gw\\HT_P_CZ_SA_BUSINESS_TRIP_DETAIL.htm";

            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                body = reader.ReadToEnd();
            }

            #region 출장자
            출장자 = string.Empty;

            if (dr출장자 != null && dr출장자.Length > 0)
            {
                foreach (DataRow dr in dr출장자)
                {
                    출장자 += D.GetString(dr["NM_KOR"]) + "/" + D.GetString(dr["NM_COMPANY"]) + "/" + D.GetString(dr["NM_DEPT"]) + "/" + D.GetString(dr["NM_DUTY_RANK"]) + "<br>";
                }
            }
            #endregion

            #region 출장기간
            출장기간 = (string.Format("{0:0000/00/00}", D.GetDecimal(출장보고서["DT_START"])) + " ~ " + string.Format("{0:0000/00/00}", D.GetDecimal(출장보고서["DT_END"])) + " " + D.GetString(출장보고서["DC_DATE"]));
            #endregion
            
            #region 출장일정
            출장일정 = string.Empty;
            if (dr출장일정 != null && dr출장일정.Length > 0)
            {
                출장일정 = @"<div style='text-align:left; font-weight:bold; font-size:9pt; font-family:굴림; margin-bottom:10px'>▶ 출 장 일 정</div>
                             <table style='width:100%; border:2px solid black; margin-bottom: 20px; font-size: 9pt; font-family: 굴림; border-collapse:collapse; border-spacing:0;'>
                                 <colgroup width='20%' align='center'></colgroup>
                                 <colgroup width='20%' align='center'></colgroup>
                             	 <colgroup width='60%' align='center'></colgroup>
                                 <tbody>
                                     <tr style='height:30px'>
                             			<th style='padding:10px; border:solid 1px black; text-align:center; background-color:Silver'>시 작 일 자</th>
                                        <th style='padding:10px; border:solid 1px black; text-align:center; background-color:Silver'>종 료 일 자</th>
                             			<th style='padding:10px; border:solid 1px black; text-align:center; background-color:Silver'>내 용</th>
                             		 </tr>" + Environment.NewLine;

                foreach (DataRow dr in dr출장일정)
                {
                    출장일정 += @"<tr style='height:30px'>
		                          	<td style='padding:10px; border:solid 1px black; text-align:center'>" + string.Format("{0:0000/00/00}", D.GetDecimal(dr["DT_FROM"])) + "</td>" + Environment.NewLine +
                                   "<td style='padding:10px; border:solid 1px black; text-align:center'>" + string.Format("{0:0000/00/00}", D.GetDecimal(dr["DT_TO"])) + " </td> " + Environment.NewLine +
                                   "<td style='padding:10px; border:solid 1px black; text-align:left; line-height:20px'>" + dr["DC_SCHEDULE"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp") + "</td>" + Environment.NewLine +
                                 "</tr>" + Environment.NewLine;
                }

                출장일정 += @"   </tbody>
                              </table>";
            }
            #endregion

            #region 미팅메모
            미팅메모 = string.Empty;

            if (dr미팅메모 != null && dr미팅메모.Length > 0)
            {
                미팅메모 = @"<div style='text-align:left; font-weight:bold; font-size:9pt; font-family:굴림; margin-bottom:10px'>▶ 미 팅 메 모</div>" + Environment.NewLine;

                foreach (DataRow dr in dr미팅메모)
                {
                    ds = DBHelper.GetDataSet("SP_CZ_SA_MEETING_ATTENDEE_S", new object[] { D.GetString(dr["CD_COMPANY"]),
                                                                                           D.GetString(dr["NO_MEETING"]),
                                                                                           D.GetString(dr["CD_PARTNER"]) });

                    참석자외부 = string.Empty;
                    참석자내부 = string.Empty;

                    if (ds != null && ds.Tables != null)
                    {
                        foreach (DataRow dr1 in ds.Tables[0].Rows)
                        {
                            참석자외부 += D.GetString(dr1["NM_ATTENDEE"]) + "/" + D.GetString(dr1["NM_DEPT"]) + "/" + D.GetString(dr1["NM_DUTY_RESP"]) + "<br>";
                        }

                        foreach (DataRow dr1 in ds.Tables[1].Rows)
                        {
                            참석자내부 += D.GetString(dr1["NM_ATTENDEE"]) + "/" + D.GetString(dr1["NM_DEPT"]) + "/" + D.GetString(dr1["NM_DUTY_RANK"]) + "<br>";
                        }
                    }

                    미팅메모 += @"<div style='text-align:center; font-weight:bold; font-size:12pt; font-family:굴림; margin-bottom:10px; color:#0000FF'>" + D.GetString(dr["LN_PARTNER"]) + "</div>" + Environment.NewLine +
                                @"<table style='width:100%; border:2px solid black; margin-bottom: 20px; font-size: 9pt; font-family: 굴림; border-collapse:collapse; border-spacing:0;'>
                                  	<colgroup width='20%' align='center'></colgroup>
                                  	<colgroup width='80%' align='center'></colgroup>
                                  	<tbody>
                                  		<tr style='height:30px'>
                                  			<th style='padding:10px; border:solid 1px black; text-align:right; background-color:Silver'>일 자</th>
                                  			<td style='padding:10px; border:solid 1px black; text-align:left'>" + string.Format("{0:0000/00/00}", D.GetDecimal(dr["DT_MEETING"])) + " " + D.GetString(dr["DC_TIME"]) + "</td>" + Environment.NewLine +
                                      @"</tr>
                                  		<tr style='height:30px'>
                                  			<th style='padding:10px; border:solid 1px black; text-align:right; background-color:Silver'>장 소</th>
                                  			<td style='padding:10px; border:solid 1px black; text-align:left'>" + D.GetString(dr["DC_LOCATION"]) + "</td>" + Environment.NewLine +
                                      @"</tr>
                                  		<tr style='height:30px'>
                                  			<th style='padding:10px; border:solid 1px black; text-align:right; background-color:Silver'>참 석 자 (외 부)</th>
                                  			<td style='padding:10px; border:solid 1px black; text-align:left; line-height:20px'>" + 참석자외부 + "</td>" + Environment.NewLine +
                                      @"</tr>
                                  		<tr style='height:30px'>
                                  			<th style='padding:10px; border:solid 1px black; text-align:right; background-color:Silver'>참 석 자 (내 부)</th>
                                  			<td style='padding:10px; border:solid 1px black; text-align:left; line-height:20px'>" + 참석자내부 + "</td>" + Environment.NewLine +
                                      @"</tr>
                                  		<tr style='height:30px'>
                                  			<th style='padding:10px; border:solid 1px black; text-align:right; background-color:Silver'>주 제</th>
                                  			<td style='padding:10px; border:solid 1px black; text-align:left'>" + D.GetString(dr["DC_SUBJECT"]) + "</td>" + Environment.NewLine +
                                      @"</tr>
                                  		<tr style='height:30px'>
                                  			<th style='padding:10px; border:solid 1px black; text-align:right; background-color:Silver'>목 적</th>
                                  			<td style='padding:10px; border:solid 1px black; text-align:left; line-height:20px'>" + dr["DC_PURPOSE"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp") + "</td>" + Environment.NewLine +
                                      @"</tr>
                                  		<tr style='height:30px'>
                                  			<td style='padding:10px; border:solid 1px black; text-align:left; line-height:20px' colspan='4'>" + dr["DC_MEETING"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp") + "</td>" + Environment.NewLine +
                                      @"</tr>
                                  	</tbody>
                                  </table>" + Environment.NewLine;
                }
            }
            #endregion

            #region 출장경비
            출장경비 = string.Empty;
            출장경비합계 = 0;

            dr출장경비 = grid출장경비.DataTable.Select("NO_BIZ_TRIP = '" + D.GetString(출장보고서["NO_BIZ_TRIP"]) + "'");

            if (dr출장경비 != null && dr출장경비.Length > 0)
            {
                출장경비 = @"<div style='text-align:left; font-weight:bold; font-size:9pt; font-family:굴림; margin-bottom:10px'>▶ 출 장 경 비</div>
                             <table style='width:100%; border:2px solid black; margin-bottom: 20px; font-size: 9pt; font-family: 굴림; border-collapse:collapse; border-spacing:0;'>
                                 <colgroup width='20%' align='center'></colgroup>
                                 <colgroup width='20%' align='center'></colgroup>
                             	 <colgroup width='60%' align='center'></colgroup>
                                 <tbody>
                                     <tr style='height:30px'>
                             			<th style='padding:10px; border:solid 1px black; text-align:center; background-color:Silver'>분 류</th>
                                        <th style='padding:10px; border:solid 1px black; text-align:center; background-color:Silver'>금 액</th>
                             			<th style='padding:10px; border:solid 1px black; text-align:center; background-color:Silver'>내 용</th>
                             		 </tr>" + Environment.NewLine;

                foreach (DataRow dr in dr출장경비)
                {
                    출장경비합계 += D.GetDecimal(dr["AM_EXPENSE"]);
                    출장경비 += @"<tr style='height:30px'>
		                          	<td style='padding:10px; border:solid 1px black; text-align:center'>" + grid출장경비.GetDataMapDisplayValue("TP_EXPENSE", D.GetString(dr["TP_EXPENSE"])) + "</td>" + Environment.NewLine +
                                   "<td style='padding:10px; border:solid 1px black; text-align:right'> \\" + string.Format("{0:N0}", D.GetDecimal(dr["AM_EXPENSE"])) + "</td>" + Environment.NewLine +
                                   "<td style='padding:10px; border:solid 1px black; text-align:left; line-height:20px'>" + dr["DC_EXPENSE"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp") + "</td>" + Environment.NewLine +
                                 "</tr>" + Environment.NewLine;
                }

                출장경비 += @"<tr style='height:30px'>
		                        <th style='padding:10px; border:solid 1px black; text-align:center; background-color:Silver'>합 계</th>
                                <td style='padding:10px; border:solid 1px black; text-align:right' colspan='2'> \" + string.Format("{0:N0}", 출장경비합계) + "</td>" + Environment.NewLine +
                            @"</tr>
                                 </tbody>
                              </table>";
            }
            #endregion

            body = body.Replace("@@DC_START", 출장보고서["DC_START"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp"));
            body = body.Replace("@@NM_EMP", 출장자);
            body = body.Replace("@@DT_BIZ_TRIP", 출장기간);
            body = body.Replace("@@DC_LOCATION", D.GetString(출장보고서["DC_LOCATION"]));
            body = body.Replace("@@DC_PURPOSE", 출장보고서["DC_PURPOSE"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp"));
            body = body.Replace("@@DC_SCHEDULE", 출장일정);
            body = body.Replace("@@DC_MEETING_MEMO", 미팅메모);
            body = body.Replace("@@DC_EXPENSE", 출장경비);
            body = body.Replace("@@DC_END", 출장보고서["DC_END"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp"));

            return body;
        }

        internal void 문서보기(DataRow 출장보고서, DataRow[] 출장자, DataRow[] 출장일정, FlexGrid 출장경비, DataRow[] 미팅메모)
        {
            string html;

            html = this.GetHtml(출장보고서, 출장자, 출장일정, 출장경비, 미팅메모);
            P_CZ_MA_HTML_VIEWER dialog = new P_CZ_MA_HTML_VIEWER(Global.MainFrame.DD("출장보고서"), html);

            dialog.ShowDialog();
        }

        private bool 결재상신(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_FI_GWDOCU", obj);
        }
    }
}

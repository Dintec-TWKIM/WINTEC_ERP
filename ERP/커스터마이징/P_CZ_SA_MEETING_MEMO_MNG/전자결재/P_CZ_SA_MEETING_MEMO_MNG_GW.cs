using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using System.Windows.Forms;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;

namespace cz
{
    internal class P_CZ_SA_MEETING_MEMO_MNG_GW
    {
        internal bool 전자결재(string 미팅번호, DataRow[] 미팅메모)
        {
            bool isSuccess;
            string strURL, key, 결재문서제목;

            DataTable dt = ComFunc.getGridGroupBy(미팅메모, new string[] { "LN_PARTNER" }, true);

            key = (MA.Login.회사코드 + "-" + 미팅번호);

            if (dt.Rows.Count == 1 && string.IsNullOrEmpty(dt.Rows[0]["LN_PARTNER"].ToString()))
                결재문서제목 = Global.MainFrame.DD("미팅메모");
            else if (dt.Rows.Count == 1)
                결재문서제목 = (Global.MainFrame.DD("미팅메모") + " - " + dt.Rows[0]["LN_PARTNER"]);
            else
                결재문서제목 = (Global.MainFrame.DD("미팅메모") + " - " + dt.Rows[0]["LN_PARTNER"] + " 외 " + (dt.Rows.Count - 1).ToString());

            isSuccess = 결재상신(new object[] { GroupWare.GetERP_CD_COMPANY(),
                                                GroupWare.GetERP_CD_PC(),
                                                key,
                                                MA.Login.사원번호,
                                                Global.MainFrame.GetStringToday,
                                                this.GetHtml(미팅메모),
                                                결재문서제목,
                                                "Y",
                                                1014 });

            if (!isSuccess) return false;

            strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
                     + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
                     + "&cd_pc=" + GroupWare.GetERP_CD_PC()
                     + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
                     + "&login_id=" + MA.Login.사원번호;

            Process.Start("msedge.exe", strURL);

            return isSuccess;
        }

        private string GetHtml(DataRow[] 미팅메모)
        {
            DataSet ds;
            string path, body, body1, 참석자외부, 참석자내부, result;

            result = string.Empty;
            body = string.Empty;
            path = Application.StartupPath + "\\download\\gw\\HT_P_CZ_SA_MEETING_MEMO_DETAIL.htm";

            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                body = reader.ReadToEnd();
            }

            foreach (DataRow dr in 미팅메모)
            {
                body1 = body;

                body1 = body1.Replace("@@LN_PARTNER", D.GetString(dr["LN_PARTNER"]));
                body1 = body1.Replace("@@DT_MEETING", string.Format("{0:0000/00/00}", D.GetDecimal(dr["DT_MEETING"])) + " " + D.GetString(dr["DC_TIME"]));
                body1 = body1.Replace("@@DC_LOCATION", D.GetString(dr["DC_LOCATION"]));

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
                        참석자내부 += D.GetString(dr1["NM_ATTENDEE"]) + "/" + D.GetString(dr1["NM_COMPANY"]) + "/" + D.GetString(dr1["NM_DEPT"]) + "/" + D.GetString(dr1["NM_DUTY_RANK"]) + "<br>";
                    }
                }

                body1 = body1.Replace("@@NM_ATTENDEE_OUT", 참석자외부);
                body1 = body1.Replace("@@NM_ATTENDEE_IN", 참석자내부);
                body1 = body1.Replace("@@DC_SUBJECT", D.GetString(dr["DC_SUBJECT"]));
                body1 = body1.Replace("@@DC_PURPOSE", dr["DC_PURPOSE"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp"));
                body1 = body1.Replace("@@DC_MEETING", dr["DC_MEETING"].ToString().Replace(Environment.NewLine, "<br>").Replace(" ", "&nbsp"));

                result += body1;
            }

            return result;
        }

        internal void 문서보기(DataRow[] 미팅메모)
        {
            string html;

            html = this.GetHtml(미팅메모);
            P_CZ_MA_HTML_VIEWER dialog = new P_CZ_MA_HTML_VIEWER(Global.MainFrame.DD("미팅메모"), html);

            dialog.ShowDialog();
        }
        
        private bool 결재상신(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_FI_GWDOCU", obj);
        }
    }
}

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
    internal class P_CZ_SA_GIRSCH_GW
    {
        internal bool 전자결재(DataRow header)
        {
            bool isSuccess;
            string strURL, key;

            key = (header["CD_COMPANY"].ToString() + "-" + header["NO_IO"].ToString() + "-D");

            isSuccess = 결재상신(new object[] { GroupWare.GetERP_CD_COMPANY(),
                                                GroupWare.GetERP_CD_PC(),
                                                key,
                                                MA.Login.사원번호,
                                                Global.MainFrame.GetStringToday,
                                                this.GetHtml(header),
                                                Global.MainFrame.DD("출고취소신청서") + "-" + header["NO_GIR"].ToString(),
                                                "Y",
                                                1021 });

            if (!isSuccess) return false;

            strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
                     + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
                     + "&cd_pc=" + GroupWare.GetERP_CD_PC()
                     + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
                     + "&login_id=" + MA.Login.사원번호;

            Process.Start("msedge.exe", strURL);

            return isSuccess;
        }

        private string GetHtml(DataRow header)
        {
            string path, body;

            body = string.Empty;
            path = Application.StartupPath + "\\download\\gw\\HT_P_CZ_SA_GIRSCH_BODY.htm";
            
            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("@@NO_GIR", header["NO_GIR"].ToString());
            body = body.Replace("@@DT_GIR", header["DT_GIR"].ToString());
            body = body.Replace("@@QT_ITEM", header["QT_ITEM"].ToString() + " 종");
            body = body.Replace("@@DT_IO", header["DT_IO"].ToString());
            body = body.Replace("@@LN_PARTNER", header["LN_PARTNER"].ToString());
            body = body.Replace("@@NM_VESSEL", header["NM_VESSEL"].ToString());
            
            return body;
        }

        private bool 결재상신(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_FI_GWDOCU", obj);
        }
    }
}

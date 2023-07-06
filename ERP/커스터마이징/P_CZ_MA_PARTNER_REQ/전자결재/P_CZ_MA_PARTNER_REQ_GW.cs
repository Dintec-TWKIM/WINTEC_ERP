using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Forms;
using System.Diagnostics;
using System.Web;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace cz
{
    internal class P_CZ_MA_PARTNER_GW
    {
        internal bool 전자결재(DataRow header, object[] obj)
        {
            bool isSuccess;
            string strURL, key;

            key = (D.GetString(header["CD_COMPANY"]) + "-" + D.GetString(header["NO_REQ"]));

            isSuccess = 결재상신(new object[] { GroupWare.GetERP_CD_COMPANY(),
                                                GroupWare.GetERP_CD_PC(),
                                                key,
                                                MA.Login.사원번호,
                                                Global.MainFrame.GetStringToday,
                                                this.GetHtml(header, obj),
                                                (Global.MainFrame.DD("거래처 등록 신청서") + " - " + D.GetString(header["LN_PARTNER"])),
                                                "Y",
                                                1010 });

            if (!isSuccess) return false;

            strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
                     + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
                     + "&cd_pc=" + GroupWare.GetERP_CD_PC()
                     + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
                     + "&login_id=" + MA.Login.사원번호;

            Process.Start("msedge.exe", strURL);

            return isSuccess;
        }

        internal void 미리보기(DataRow header, object[] obj)
        {
            string html;

            html = this.GetHtml(header, obj);
            P_CZ_MA_HTML_VIEWER dialog = new P_CZ_MA_HTML_VIEWER(Global.MainFrame.DD("거래처 등록신청서"), html);

            dialog.ShowDialog();
        }

        private string GetHtml(DataRow header, object[] obj)
        {
            string path, body;

            body = string.Empty;

            path = Application.StartupPath + "\\download\\gw\\HT_P_CZ_MA_PARTNER_REQ_BODY.htm";

            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                body = reader.ReadToEnd();
            }

            #region 기본 정보
            body = body.Replace("@@NO_REQ", D.GetString(header["NO_REQ"]));
            body = body.Replace("@@NM_FG_PARTNER", D.GetString(obj[0]));
            body = body.Replace("@@NM_CLS_PARTNER", D.GetString(obj[1]));
            body = body.Replace("@@LN_PARTNER", D.GetString(header["LN_PARTNER"]));
            body = body.Replace("@@NO_COMPANY", string.Format("{0:000-00-00000}", D.GetDecimal(header["NO_COMPANY"])));
            body = body.Replace("@@NM_CEO", D.GetString(header["NM_CEO"]));
            body = body.Replace("@@NO_RES", string.Format("{0:000000-0000000}", D.GetDecimal(header["NO_RES"])));
            body = body.Replace("@@NM_AREA", D.GetString(obj[2]));
            body = body.Replace("@@NM_NATION", D.GetString(obj[3]));
            body = body.Replace("@@DC_ADDRESS", D.GetString(header["DC_ADS1_H"]) + " " + D.GetString(header["DC_ADS1_D"]));
            body = body.Replace("@@NM_PARTNER_GRP", D.GetString(obj[4]));
            body = body.Replace("@@NM_SALES_GRP", D.GetString(obj[5]));
            body = body.Replace("@@NO_TEL", D.GetString(header["NO_TEL1"]));
            body = body.Replace("@@NO_FAX", D.GetString(header["NO_FAX1"]));
            body = body.Replace("@@TP_JOB", D.GetString(header["TP_JOB"]));
            body = body.Replace("@@CLS_JOB", D.GetString(header["CLS_JOB"]));
            body = body.Replace("@@DC_RMK", D.GetString(header["NM_TEXT"]));
            #endregion

            #region 매입정보
            body = body.Replace("@@NM_TP_PO", D.GetString(header["NM_TPPO"]));
            body = body.Replace("@@NM_EXCH_PO", D.GetString(obj[6]));
            body = body.Replace("@@NM_TP_INQ_SEND", D.GetString(obj[7]));
            body = body.Replace("@@NM_PO_SEND", D.GetString(obj[8]));
            body = body.Replace("@@NM_FG_PAYMENT_PO", D.GetString(obj[9]));
            body = body.Replace("@@NM_TP_COND_PRICE", D.GetString(obj[10]));
            body = body.Replace("@@NM_FG_TAX", D.GetString(obj[11]));
            body = body.Replace("@@NM_RT_DC_PO", D.GetDecimal(header["RT_PURCHASE_DC"]).ToString("N"));
            body = body.Replace("@@NM_ORIGIN_RPT", D.GetString(obj[26]));
            body = body.Replace("@@NM_ORIGIN", D.GetString(obj[25]));
            #endregion

            #region 매출정보
            body = body.Replace("@@NM_TP_SO", D.GetString(header["NM_SO"]));
            body = body.Replace("@@NM_EXCH_SO", D.GetString(obj[12]));
            body = body.Replace("@@NM_TP_INQ_RCV", D.GetString(obj[13]));
            body = body.Replace("@@NM_TP_QTN_SEND", D.GetString(obj[14]));
            body = body.Replace("@@NM_FG_PAYMENT_SO", D.GetString(obj[15]));
            body = body.Replace("@@NM_TP_DELIVERY_CONDITION", D.GetString(obj[16]));
            body = body.Replace("@@RT_SALES_PROFIT", D.GetDecimal(header["RT_SALES_PROFIT"]).ToString("N"));
            body = body.Replace("@@RT_SALES_DC", D.GetDecimal(header["RT_SALES_DC"]).ToString("N"));
            body = body.Replace("@@RT_COMMISSION", D.GetDecimal(header["RT_COMMISSION"]).ToString("N"));
            body = body.Replace("@@DC_COMMISSION", D.GetString(header["DC_COMMISSION"]));
            body = body.Replace("@@NM_TP_VAT", D.GetString(obj[17]));
            body = body.Replace("@@NM_TP_INV", D.GetString(obj[23]));
            body = body.Replace("@@NM_YN_JEONJA", D.GetString(obj[24]));
            #endregion

            #region 거래처담당자
            body = body.Replace("@@NM_TP_PTR",  D.GetString(obj[18]));
            body = body.Replace("@@NM_EMP_PARTNER", D.GetString(header["CD_EMP_PARTNER"]));
            body = body.Replace("@@NO_EMP_TEL", D.GetString(header["NO_TEL"]));
            body = body.Replace("@@NO_EMP_HP", D.GetString(header["NO_HPEMP_PARTNER"]));
            body = body.Replace("@@NO_EMP_EMAIL", D.GetString(header["E_MAIL"]));
            body = body.Replace("@@NO_EMP_FAX", D.GetString(header["NO_FAX"]));
            #endregion

            #region 계좌정보
            body = body.Replace("@@NM_TP_DEPOSIT", D.GetString(obj[19]));
            body = body.Replace("@@NO_DEPOSIT", D.GetString(header["NO_DEPOSIT"]));
            body = body.Replace("@@NM_CD_BANK", D.GetString(obj[20]));
            body = body.Replace("@@NM_BANK", D.GetString(header["NM_BANK"]));
            body = body.Replace("@@BANK_NATION", D.GetString(obj[21]));
            body = body.Replace("@@NO_SORT", D.GetString(header["NO_SORT"]));
            body = body.Replace("@@NM_DEPOSIT_NATION", D.GetString(obj[22]));
            body = body.Replace("@@NO_SWIFT", D.GetString(header["NO_SWIFT"]));
            body = body.Replace("@@NM_DEPOSIT", D.GetString(header["NM_DEPOSIT"]));
            body = body.Replace("@@DC_DEPOSIT_TEL", D.GetString(header["DC_DEPOSIT_TEL"]));
            body = body.Replace("@@DC_DEPOSIT_ADDRESS", D.GetString(header["DC_DEPOSIT_ADDRESS"]));
            body = body.Replace("@@NO_BANK_BIC", D.GetString(header["NO_BANK_BIC"]));
            body = body.Replace("@@DEPOSIT_RMK", D.GetString(header["DC_RMK"]));
            #endregion

            return body;
        }

        private bool 결재상신(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("SP_CZ_FI_GWDOCU", obj);
        }
    }
}

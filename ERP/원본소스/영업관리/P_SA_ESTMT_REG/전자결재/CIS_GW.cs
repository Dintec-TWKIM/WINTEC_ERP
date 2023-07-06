using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Forms;

namespace sale
{
    class CIS_GW
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
            List.Add(3000); //APP_FORM_KIND에 들어갈 값
            List.Add("29991231");

            switch (Global.MainFrame.ServerKeyCommon.ToUpper())
            {
                case "DZSQL":       // 84번 개발서버
                    List[5] = GetCIS(rowH, dtL);
                    isSuccess = 결재상신_개발서버(List.ToArray());
                    break;

                case "CIS":         // 씨아이에스(주)
                    List[5] = GetCIS(rowH, dtL);
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    string strURL = "http://gw.cisro.co.kr/kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(D.GetString(rowH["NO_EST"]), Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;
                default:
                    break;
            }

            return isSuccess;
        }

        #region -> 씨아이에스(주) Html양식
        private string GetCIS(DataRow rowH, DataTable dtL)
        {
            string body = string.Empty;
            string line = string.Empty;

            body = @"
<head>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
</head>

<body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0'>
	<table width='100%' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: collapse; border: 0;'>
		<colgroup width='5%' align='center'></colgroup>
		<colgroup width='95%' align='center'></colgroup>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' colspan='2'>아래와 같이 품의서를 제출하고자 하오니 승인하여 주시기 바랍니다.</td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>-&nbsp; 아&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 래&nbsp; -</td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>1.</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>업&nbsp;&nbsp;&nbsp; 체 : @@업체명</td>
		</tr>
		
		<tr height='20'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>2.</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>견적번호 : @@견적번호</td>
		</tr>
		
		<tr height='20'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>3.</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>품목명 및 규격 수량 단위 : @@품명규격수량단위</td>
		</tr>
		
		<tr height='20'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>4.</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>금&nbsp;&nbsp;&nbsp; 액(VAT 별도) : @@금액</td>
		</tr>
		
		<tr height='20'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'>5.</td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000' align='left'>납기일자 : @@납기일자</td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		</table>
	</body>
</center>";
            int Cnt = 0;

            foreach (DataRow dr in dtL.Rows)
            {
                Cnt++;
            }

            body = body.Replace("@@업체명", D.GetString(rowH["LN_PARTNER"]));
            body = body.Replace("@@견적번호", D.GetString(rowH["NO_EST"]));
            if (Cnt > 1)
                body = body.Replace("@@품명규격수량단위", "&nbsp&nbsp" + D.GetString(dtL.Rows[0]["NM_ITEM"]) + "&nbsp&nbsp" + D.GetString(dtL.Rows[0]["STND_ITEM"]) + "&nbsp&nbsp" + D.GetDecimal(dtL.Rows[0]["QT_EST"]).ToString("##,##0.###") + "&nbsp&nbsp" + D.GetString(dtL.Rows[0]["UNIT_IM"]) + "&nbsp&nbsp" + "외" + "&nbsp&nbsp" + D.GetString((Cnt - 1) + "개"));

            else
                body = body.Replace("@@품명규격수량단위", "&nbsp&nbsp" + D.GetString(dtL.Rows[0]["NM_ITEM"]) + "&nbsp&nbsp" + D.GetString(dtL.Rows[0]["STND_ITEM"]) + "&nbsp&nbsp" + D.GetDecimal(dtL.Rows[0]["QT_EST"]).ToString("##,##0.###") + "&nbsp&nbsp" + D.GetString(dtL.Rows[0]["UNIT_IM"]));

            if (D.GetString(rowH["DT_DUEDATE"]) != string.Empty)
                body = body.Replace("@@납기일자", D.GetString(rowH["DT_DUEDATE"]).Substring(0, 4) + "년" + D.GetString(rowH["DT_DUEDATE"]).Substring(4, 2) + "월" + D.GetString(rowH["DT_DUEDATE"]).Substring(6, 2) + "일");

            else
                body = body.Replace("@@납기일자", string.Empty);


            body = body.Replace("@@금액", D.GetDecimal(dtL.Rows[0]["AM_EST"]).ToString("##,##0.##"));

            return body;
        }
        #endregion
        
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

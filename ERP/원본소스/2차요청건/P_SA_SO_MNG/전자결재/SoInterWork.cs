using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

namespace sale
{
    internal class SoInterWork
    {
        string html = string.Empty;

        internal bool 전자결재(DataRow rowH, DataTable dtL, bool is예정)
        {
            bool isSuccess = true;
            string strURL = string.Empty;
            string noDocu = is예정 ? D.GetString(rowH["NO_SO"]) + "_1" : D.GetString(rowH["NO_SO"]) + "_2";
            string project = D.GetString(rowH["NO_PROJECT"]) + "," + D.GetString(rowH["NM_PROJECT"]);
            string nmPumm = is예정 ? "수주예정 통보서(" + project + ")" : "수주확정 통보서(" + project + ")";

            if (!is예정) //수주확정품의서 일 때
            {
                if (!Chk수주예정품의(D.GetString(rowH["NO_SO"]) + "_1")) return false;
            }

            if (!Chk상신체크(noDocu, nmPumm)) return false;

            List<object> List = new List<object>();
            List.Add(MA.Login.회사코드);
            List.Add(Global.MainFrame.LoginInfo.CdPc);
            List.Add(noDocu);
            List.Add(MA.Login.사원번호);
            List.Add(Global.MainFrame.GetStringToday);
            List.Add(string.Empty);
            List.Add(nmPumm);
            List.Add("Y");
            List.Add(is예정 ? 2500 : 3500); //APP_FORM_KIND에 들어갈 값
            List.Add("29991231");

            switch (Global.MainFrame.ServerKeyCommon.ToUpper())
            {
                case "DZSQL":       //84번 개발서버
                    List[5] = Get우진산전Html(rowH, dtL);
                    isSuccess = 결재상신_개발서버(List.ToArray());
                    break;
                case "WJIS":        //우진산전
                    List[5] = Get우진산전Html(rowH, dtL);
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    strURL = "http://gw.wjis.co.kr/kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(noDocu, Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;
                default:
                    break;
            }

            return isSuccess;
        }

        internal bool 전자결재(DataRow rowH, DataTable dtL)
        {
            bool isSuccess = true;
            string strURL = string.Empty;
            string noDocu = D.GetString(rowH["NO_SO"]);
            string nmPumm = "수주품의서";

            if (!Chk상신체크(noDocu, nmPumm)) return false;

            List<object> List = new List<object>();
            List.Add(MA.Login.회사코드);
            List.Add(Global.MainFrame.LoginInfo.CdPc);
            List.Add(noDocu);
            List.Add(MA.Login.사원번호);
            List.Add(Global.MainFrame.GetStringToday);
            List.Add(string.Empty);
            List.Add(nmPumm);
            List.Add("Y");
            List.Add(1001); //APP_FORM_KIND에 들어갈 값
            List.Add("29991231");

            switch (Global.MainFrame.ServerKeyCommon.ToUpper())
            {
                case "DZSQL":       //84번 개발서버
                    List[5] = GetGALAXIAHtml(rowH, dtL);
                    isSuccess = 결재상신_개발서버(List.ToArray());
                    break;
                case "GALAXIA":     //갤럭시아일렉트로닉스
                    List[5] = GetGALAXIAHtml(rowH, dtL);
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    strURL = "http://gw.galaxia.co.kr/kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(noDocu, Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;
                case "WONIK":     //원익
                    if (D.GetString(rowH["RET"]) == "Y") //반품의뢰서
                    {
                        List[5] = WonikReturnHtml(rowH, dtL);
                        List[6] = "반품의뢰서";
                        List[8] = 2011;
                    }
                    else
                    {
                        List[5] = WonikHtml(rowH, dtL);
                        List[6] = "수주의뢰서";
                        List[8] = 2012;
                    }
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    strURL = "http://gw.wonik.co.kr//kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(noDocu, Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;
                default:
                    break;
            }

            return isSuccess;
        }

        private bool Chk수주예정품의(string 수주예정품의번호)
        {
            string stStat = 결재상신상태조회(수주예정품의번호);
            if (stStat != "1")
            {
                Global.MainFrame.ShowMessage("해당수주는 수주예정품의가 완료되지 않았습니다.[상태 : " + stStat + "]\n\n(-1:반려, 0:진행, 1:완료, 2:미상신, 3:취소)");
                return false;
            }
            return true;
        }

        private bool Chk상신체크(string noDocu, string nmPumm)
        {
            string stStat = 결재상신상태조회(noDocu);

            switch (stStat)
            {
                case "-1":
                    if (Global.MainFrame.ShowMessage("해당수주는 " + nmPumm + "가 반려된 건입니다." + Environment.NewLine + "재상신 하시겠습니까?", "QY2") == DialogResult.Yes)
                        return true;
                    else
                        return false;
                case "0":
                    Global.MainFrame.ShowMessage("해당수주는 " + nmPumm + "가 진행중인 건입니다.");
                    return false;
                case "1":
                    Global.MainFrame.ShowMessage("해당수주는 " + nmPumm + "가 완료된 건입니다.");
                    return false;
                case "3":
                    if (Global.MainFrame.ShowMessage("해당수주는 " + nmPumm + "가 취소된 건입니다." + Environment.NewLine + "재상신 하시겠습니까?", "QY2") == DialogResult.Yes)
                        return true;
                    else
                        return false;
                case "2":   //미상신
                default:
                    return true;
            }
        }

        #region -> 우진산전 HTML양식

        private string Get우진산전Html(DataRow rowH, DataTable dtL)
        {
            string body = string.Empty;
            string line = string.Empty;
            int number = 0;

            body = @"
<head>
<meta http-equiv='Content-Language' content='ko'>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
</head>
<center>
<body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0'>
	<table width='100%' border='0' bordercolor='white' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: collapse'>
		<colgroup width='5%' align='center'></colgroup>
		<colgroup width='9%' align='center'></colgroup>
		<colgroup width='12%' align='center'></colgroup>
		<colgroup width='8%' align='center'></colgroup>
		<colgroup width='12%' align='center'></colgroup>
		<colgroup width='8%' align='center'></colgroup>
		<colgroup width='9%' align='center'></colgroup>
		<colgroup width='12%' align='center'></colgroup>
		<colgroup width='13%' align='center'></colgroup>
		<colgroup width='12%' align='center'></colgroup>
		<tr height='30'>
			<td style='border:1px solid #000000; border-top:medium none #000000; border-left:0px' >No</td>
			<td style='border:1px solid #000000; border-top:medium none #000000' colspan='2'>@@수주번호</td>
			<td style='border-left:0px solid #000000; border-top:medium none #000000; border-right:1px solid #000000;  border-bottom:medium none #000000; border-bottom-color:#000000'>작성일자</td>
			<td style='border-left:0px solid #000000; border-top:medium none #000000; border-right:1px solid #000000;  border-bottom:medium none #000000; border-bottom-color:#000000'>@@작성일자</td>
			<td style='border-left:0px solid #000000; border-top:medium none #000000; border-right:1px solid #000000;  border-bottom:medium none #000000; border-bottom-color:#000000'>작성자</td>
			<td style='border-left:0px solid #000000; border-top:medium none #000000; border-right-color:#000000; border-bottom-color:#000000'>@@작성자</td>
			<td style='border:1px solid #000000; border-top:medium none #000000'>프로젝트코드</td>
			<td style='border:1px solid #000000; border-top:medium none #000000; border-right:0px; padding-left:10px' colspan='2'>
                <p align='left'>@@프로젝트코드</p>
            </td>
		</tr>
		<tr height='30'>
			<td style='border:1px solid #000000; border-left:0px' colspan='2'>수주처</td>
			<td style='border-left:0px solid #000000; border-top:1px solid #000000; border-bottom:1px solid #000000; border-right-color:#000000' colspan='5'><p align='left'>&nbsp;&nbsp;@@수주처</td>
			<td style='border:1px solid #000000; '>프로젝트명</td>
			<td style='border:1px solid #000000; border-right:0px; padding-left:10px' colspan='2'>
                <p align='left'>@@프로젝트명</p>
            </td>
		</tr>
		<tr height='30'>
			<td style='border:1px solid #000000; border-left:0px' colspan='2'>납기일자</td>
			<td style='border-left:0px solid #000000; border-top:1px solid #000000; border-bottom:1px solid #000000; border-right-color:#000000' colspan='5'><p align='left'>&nbsp;&nbsp;@@납기일자</td>
			<td style='border:1px solid #000000; '>통화 / 환율</td>
			<td style='border:1px solid #000000; border-right:0px; padding-left:10px' colspan='2'>
                <p align='left'>@@통화환율</p>
            </td>
		</tr>
		<tr height='30'>
			<td style='border:1px solid #000000; border-left:0px' colspan='2'>주문처 Order No</td>
			<td style='border-left:0px solid #000000; border-top:1px solid #000000; border-bottom:1px solid #000000; border-right-color:#000000' colspan='5'><p align='left'>&nbsp;&nbsp;@@주문처번호</td>
			<td style='border:1px solid #000000; '>납품장소</td>
			<td style='border:1px solid #000000; border-right:0px; padding-left:10px' colspan='2'>
                <p align='left'>@@납품장소</p>
            </td>
		</tr>
		<tr height='30'>
			<td style='border-left:0px solid #000000; border-right:medium none #000000; border-bottom:medium none #000000; border-top-color:#000000' colspan='2'>&nbsp;</td>
			<td style='border-left:medium none #000000; border-right:medium none #000000; border-bottom:medium none #000000; border-top:1px solid #000000'>&nbsp;</td>
			<td style='border-left:medium none #000000; border-right:medium none #000000; border-bottom:medium none #000000; border-top:1px solid #000000'>&nbsp;</td>
			<td style='border-left:medium none #000000; border-right:medium none #000000; border-bottom:medium none #000000; border-top:1px solid #000000'>&nbsp;</td>
			<td style='border-left:medium none #000000; border-right:medium none #000000; border-bottom:medium none #000000; border-top:1px solid #000000'>&nbsp;</td>
			<td style='border-left:medium none #000000; border-bottom:medium none #000000; border-right-color:#000000; border-top:1px solid #000000'>&nbsp;</td>
			<td style='border:1px solid #000000; '>설계납기</td>
			<td style='border:1px solid #000000; border-right:0px; padding-left:10px' colspan='2'>
                <p align='left'>@@설계납기</p>
            </td>
		</tr>
		<tr height='30'>
			<td style='border-left:0px solid #000000; border-right:medium none #000000; border-top:medium none #000000; border-bottom:medium none #000000; ' colspan='2'><p align='left'>&nbsp;&nbsp;&nbsp; <u>특이사항</u></td>
			<td style='border:medium none #000000; '>&nbsp;</td>
			<td style='border:medium none #000000; '>&nbsp;</td>
			<td style='border:medium none #000000; '>&nbsp;</td>
			<td style='border:medium none #000000; '>&nbsp;</td>
			<td style='border-left:medium none #000000; border-top:medium none #000000; border-bottom:medium none #000000; border-right-color:#000000'>&nbsp;</td>
			<td style='border:1px solid #000000; '>자재납기</td>
			<td style='border:1px solid #000000; border-right:0px; padding-left:10px' colspan='2'>
                <p align='left'>@@자재납기</p>
            </td>
		</tr>
		<tr height='30'>
			<td style='border-left:0px solid #000000; border-right:medium none #000000; border-top:medium none #000000; border-bottom:medium none #000000; ' colspan='2'>&nbsp;</td>
			<td style='border:medium none #000000; '>&nbsp;</td>
			<td style='border:medium none #000000; '>&nbsp;</td>
			<td style='border:medium none #000000; '>&nbsp;</td>
			<td style='border:medium none #000000; '>&nbsp;</td>
			<td style='border-left:medium none #000000; border-top:medium none #000000; border-bottom:medium none #000000; border-right-color:#000000'>&nbsp;</td>
			<td style='border:1px solid #000000; '>생산납기</td>
			<td style='border:1px solid #000000; border-right:0px; padding-left:10px' colspan='2'>
                <p align='left'>@@생산납기</p>
            </td>
		</tr>
		<tr height='30'>
			<td style='border-left:0px solid #000000; border-right:0px solid #000000; border-top:medium none #000000; border-bottom:medium none #000000; ' colspan='10'>
			<p align='left'>&nbsp;&nbsp;&nbsp;@@특이사항</td>
		</tr>
		<tr height='30'>
			<td style='border-left:0px solid #000000; border-right:medium none #000000; border-top:medium none #000000; border-bottom:medium none #000000; ' colspan='2'>&nbsp;</td>
			<td style='border:medium none #000000; '>&nbsp;</td>
			<td style='border:medium none #000000; '>&nbsp;</td>
			<td style='border:medium none #000000; '>&nbsp;</td>
			<td style='border:medium none #000000; '>&nbsp;</td>
			<td style='border:medium none #000000; '>&nbsp;</td>
			<td style='border:medium none #000000; '>&nbsp;</td>
			<td style='border-left:medium none #000000; border-right:0px solid #000000; border-top:medium none #000000; border-bottom:medium none #000000; ' colspan='2'>&nbsp;</td>
		</tr>
		<tr height='30'>
			<td style='border-left:0px solid #000000; border-right:medium none #000000; border-top:medium none #000000; border-bottom-color:#000000'>&nbsp;</td>
			<td style='border-left:medium none #000000; border-right:medium none #000000; border-top:medium none #000000; border-bottom-color:#000000'>&nbsp;</td>
			<td style='border-left:medium none #000000; border-right:medium none #000000; border-top:medium none #000000; border-bottom-color:#000000'>&nbsp;</td>
			<td style='border-left:medium none #000000; border-right:medium none #000000; border-top:medium none #000000; border-bottom-color:#000000'>&nbsp;</td>
			<td style='border-left:medium none #000000; border-right:medium none #000000; border-top:medium none #000000; border-bottom-color:#000000'>&nbsp;</td>
			<td style='border-left:medium none #000000; border-right:medium none #000000; border-top:medium none #000000; border-bottom-color:#000000'>&nbsp;</td>
			<td style='border-left:medium none #000000; border-right:medium none #000000; border-top:medium none #000000; border-bottom-color:#000000'>&nbsp;</td>
			<td style='border-left:medium none #000000; border-right:0px solid #000000; border-top:medium none #000000; border-bottom-color:#000000' colspan='3'>&nbsp;</td>
		</tr>
		<tr height='40'>
			<td style='border:1px solid #000000; border-left:0px'>No</td>
			<td style='border:1px solid #000000; ' colspan='2'>품명</td>
			<td style='border:1px solid #000000; '>품목계정<br>(매출구분)</td>
			<td style='border:1px solid #000000; ' colspan='2'>거래처품번</td>
			<td style='border:1px solid #000000; '>수량</td>
			<td style='border:1px solid #000000; '>단가</td>
			<td style='border:1px solid #000000; '>금액</td>
			<td style='border:1px solid #000000; border-right:0px'>비고</td>
		</tr>
        @@라인추가
		<tr height='30'>
			<td style='border:1px solid #000000; border-bottom:0px none #000000; border-left:0px; ' colspan='8'>Total</td>
			<td style='border:1px solid #000000; border-bottom:0px none #000000; padding-right:3px; font-size: 8pt'>
                <p align='right'>@@총금액</p>
            </td>
			<td style='border:1px solid #000000; border-bottom:0px none #000000; border-right:0px'>&nbsp;</td>
		</tr>
		</table>
	</body>
</center>";

            string 현재날짜 = Global.MainFrame.GetStringToday;
            decimal 총금액 = D.GetDecimal(dtL.Compute("SUM(AM_SO)", ""));
            decimal min납기일 = D.GetDecimal(dtL.Compute("MIN(DT_DUEDATE)", ""));
            decimal max납기일 = D.GetDecimal(dtL.Compute("MAX(DT_DUEDATE)", ""));
            DataTable dt환종 = MA.GetCode("MA_B000005", true);
            dt환종.PrimaryKey = new DataColumn[] { dt환종.Columns["CODE"] };
            string 환종 = D.GetString(dt환종.Rows.Find(D.GetString(rowH["CD_EXCH"]))["NAME"]);
            string noProject = D.GetString(rowH["NO_PROJECT"]);
            string 설계납기 = "&nbsp;";
            string 자재납기 = "&nbsp;";
            string 생산납기 = "&nbsp;";

            if (noProject != string.Empty)
            {
                DataRow row = GetProjectHHInfo(noProject);
                if (row != null)
                {
                    if (D.GetString(row["CD_MNG_DT1"]).Length == 8)
                        설계납기 = D.GetString(row["CD_MNG_DT1"]).Substring(0, 4) + "-" + D.GetString(row["CD_MNG_DT1"]).Substring(4, 2) + "-" + D.GetString(row["CD_MNG_DT1"]).Substring(6, 2);
                    if (D.GetString(row["CD_MNG_DT2"]).Length == 8)
                        자재납기 = D.GetString(row["CD_MNG_DT2"]).Substring(0, 4) + "-" + D.GetString(row["CD_MNG_DT2"]).Substring(4, 2) + "-" + D.GetString(row["CD_MNG_DT2"]).Substring(6, 2);
                    if (D.GetString(row["CD_MNG_DT3"]).Length == 8)
                        생산납기 = D.GetString(row["CD_MNG_DT3"]).Substring(0, 4) + "-" + D.GetString(row["CD_MNG_DT3"]).Substring(4, 2) + "-" + D.GetString(row["CD_MNG_DT3"]).Substring(6, 2);
                }
            }

            body = body.Replace("@@수주번호", D.GetString(rowH["NO_SO"]));
            body = body.Replace("@@작성일자", 현재날짜.Substring(0, 4) + "-" + 현재날짜.Substring(4, 2) + "-" + 현재날짜.Substring(6, 2));
            body = body.Replace("@@작성자", D.GetString(rowH["NM_KOR"]));
            body = body.Replace("@@프로젝트코드", D.GetString(rowH["NO_PROJECT"]));
            body = body.Replace("@@프로젝트명", D.GetString(rowH["NM_PROJECT"]));
            body = body.Replace("@@수주처", D.GetString(rowH["LN_PARTNER"]));
            body = body.Replace("@@납기일자", D.GetDecimal(rowH["DT_SO"]).ToString("####-##-##") + " ~ " + max납기일.ToString("####-##-##"));
            body = body.Replace("@@통화환율", 환종 + " / " + D.GetDecimal(rowH["RT_EXCH"]).ToString("###,##0.####"));
            body = body.Replace("@@주문처번호", D.GetString(rowH["NO_PO_PARTNER"]));
            body = body.Replace("@@납품장소", D.GetString(rowH["DC_RMK"]));
            body = body.Replace("@@특이사항", 수주텍스트비고조회(D.GetString(rowH["NO_SO"])).Replace("\n", "<br>&nbsp;&nbsp;&nbsp;"));
            body = body.Replace("@@설계납기", 설계납기);
            body = body.Replace("@@자재납기", 자재납기);
            body = body.Replace("@@생산납기", 생산납기);
            body = body.Replace("@@총금액", 총금액.ToString("###,###,###,##0.####"));

            DataTable dtClsItem = MA.GetCode("MA_B000010");
            dtClsItem.PrimaryKey = new DataColumn[] { dtClsItem.Columns["CODE"] };

            foreach (DataRow rowL in dtL.Rows)
            {
                string tr = string.Empty;
                tr = GetLineWJIS();
                tr = tr.Replace("@@번호", D.GetString(++number));
                tr = tr.Replace("@@품명", D.GetString(rowL["CD_ITEM"]) + " / " + D.GetString(rowL["NO_DESIGN"]) + "<br>" + D.GetString(rowL["NM_ITEM"]));
                tr = tr.Replace("@@품목계정", D.GetString(dtClsItem.Rows.Find(D.GetString(rowL["CLS_ITEM"]))["NAME"]));
                tr = tr.Replace("@@거래처품번", D.GetString(rowL["TXT_USERDEF1"]));
                tr = tr.Replace("@@수량", D.GetDecimal(rowL["QT_SO"]).ToString("###,###,##0"));
                tr = tr.Replace("@@단가", D.GetDecimal(rowL["UM_SO"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@금액", D.GetDecimal(rowL["AM_SO"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@비고", D.GetString(rowL["DC2"]));

                line += tr;
            }

            body = body.Replace("@@라인추가", line);

            return body;
        }

        private string GetLineWJIS()
        {
            string tr = @"
<tr height='30'>
<td style='border:1px solid #000000; border-left:0px'>@@번호</td>
<td style='border:1px solid #000000; padding-left:3px' colspan='2'>
    <p style='Line-height:150%' align='left'>@@품명</p>
</td>
<td style='border:1px solid #000000; padding-right:3px'>
    <p align='center'>@@품목계정</p>
</td>
<td style='border:1px solid #000000; padding-left:3px' colspan='2'>
    <p align='left'>@@거래처품번</p>
</td>
<td style='border:1px solid #000000; padding-right:3px'>
    <p align='right'>@@수량</p>
</td>
<td style='border:1px solid #000000; padding-right:3px; font-size: 8pt'>
    <p align='right'>@@단가</p>
</td>
<td style='border:1px solid #000000; padding-right:3px; font-size: 8pt'>
    <p align='right'>@@금액</p>
</td>
<td style='border:1px solid #000000; padding-right:3px; border-right:0px'>
    <p align='right'>@@비고</p>
</td>
</tr>";
            return tr;
        }

        #endregion

        #region -> 갤럭시아일렉트로닉스 HTML양식

        private string GetGALAXIAHtml(DataRow rowH, DataTable dtL)
        {
            string body = string.Empty;
            string line = string.Empty;
            int number = 0;

            body = @"
<head>
<meta http-equiv='Content-Language' content='ko'>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
</head>

<center>
<body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0'>
	<table width='100%' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size=10pt; border-collapse: collapse; border: 0;'>
		<colgroup width='6%' align='center'></colgroup>
		<colgroup width='14%' align='center'></colgroup>
		<colgroup width='9%' align='center'></colgroup>
		<colgroup width='10%' align='center'></colgroup>
		<colgroup width='9%' align='center'></colgroup>
		<colgroup width='10%' align='center'></colgroup>
		<colgroup width='13%' align='center'></colgroup>
		<colgroup width='13%' align='center'></colgroup>
		<colgroup width='16%' align='center'></colgroup>
		
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2' bgcolor='#D8D8D8'>수주일자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='4'>@@수주일자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#D8D8D8'>작성자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'>@@작성자</td>
		</tr>
		
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2' bgcolor='#D8D8D8'>수주번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='4'>@@수주번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#D8D8D8'>프로젝트코드</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'>@@프로젝트코드</td>
		</tr>
		
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2' bgcolor='#D8D8D8'>원청업체명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='4'>@@원청업체명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#D8D8D8'>수주형태</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'>@@수주형태</td>
		</tr>
		
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2' bgcolor='#D8D8D8'>수주처</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='4'>@@수주처</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#D8D8D8'>프로젝트명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'>@@프로젝트명</td>
		</tr>
		
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2' bgcolor='#D8D8D8'>납기일자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='4'>@@납기일자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#D8D8D8'>통화/환율</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'>@@통화환율</td>
		</tr>
		
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2' bgcolor='#D8D8D8'>주문처 Order No</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='4'>@@주문처</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#D8D8D8'>납품장소</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'>@@납품장소</td>
		</tr>
		
		<tr height='80'>
			<td style='border-style: solid; border-width: 1 0 1 1; border-color: #000000; padding: 10px 10px 10px 10px' align='left' valign='top'><u>비고</u><br>&nbsp;</td>
			<td style='border-style: solid; border-width: 1 1 1 0; border-color: #000000; padding: 10px 10px 10px 10px' colspan='8' align='left' valign='top'>@@헤더비고</td>
		</tr>
		
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#D8D8D8'>No</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2' bgcolor='#D8D8D8'>품명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2' bgcolor='#D8D8D8'>규격</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#D8D8D8'>수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#D8D8D8'>단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#D8D8D8'>금액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#D8D8D8'>비고</td>
		</tr>
        @@라인추가		
		<tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='7' bgcolor='#D8D8D8'>합계</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; padding-right:10px' align='right'>@@합계금액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
		</tr>		
		</table>
	</body>
</center>";

            decimal 총금액 = D.GetDecimal(dtL.Compute("SUM(AM_SO)", ""));
            decimal min납기일 = D.GetDecimal(dtL.Compute("MIN(DT_DUEDATE)", ""));
            decimal max납기일 = D.GetDecimal(dtL.Compute("MAX(DT_DUEDATE)", ""));
            DataTable dt환종 = MA.GetCode("MA_B000005", true);
            dt환종.PrimaryKey = new DataColumn[] { dt환종.Columns["CODE"] };
            string 환종 = D.GetString(dt환종.Rows.Find(D.GetString(rowH["CD_EXCH"]))["NAME"]);

            body = body.Replace("@@수주일자", D.GetDecimal(rowH["DT_SO"]).ToString("####/##/##"));
            body = body.Replace("@@작성자", D.GetString(rowH["NM_KOR"]));
            body = body.Replace("@@수주번호", D.GetString(rowH["NO_SO"]));
            body = body.Replace("@@프로젝트코드", D.GetString(rowH["NO_PROJECT"]));
            body = body.Replace("@@원청업체명", D.GetString(dtL.Rows[0]["NM_GI_PARTNER"]));
            body = body.Replace("@@수주형태", D.GetString(rowH["NM_SO"]));
            body = body.Replace("@@수주처", D.GetString(rowH["LN_PARTNER"]));
            body = body.Replace("@@프로젝트명", D.GetString(rowH["NM_PROJECT"]));
            body = body.Replace("@@납기일자", min납기일.ToString("####/##/##") + " ~ " + max납기일.ToString("####/##/##"));
            body = body.Replace("@@통화환율", 환종 + " / " + D.GetDecimal(rowH["RT_EXCH"]).ToString("###,##0.####"));
            body = body.Replace("@@주문처", D.GetString(rowH["NO_PO_PARTNER"]));
            body = body.Replace("@@납품장소", D.GetString(rowH["DC_RMK"]));
            body = body.Replace("@@헤더비고", 수주텍스트비고조회(D.GetString(rowH["NO_SO"])).Replace("\n", "<br>"));
            body = body.Replace("@@합계금액", 총금액.ToString("###,###,###,##0.####"));

            foreach (DataRow rowL in dtL.Rows)
            {
                string tr = string.Empty;
                tr = GetLineGALAXIA();
                tr = tr.Replace("@@NO", D.GetString(++number));
                tr = tr.Replace("@@품명", "[" + D.GetString(rowL["CD_ITEM"]) + "] " + D.GetString(rowL["NM_ITEM"]));
                tr = tr.Replace("@@규격", D.GetString(rowL["STND_ITEM"]));
                tr = tr.Replace("@@수량", D.GetDecimal(rowL["QT_SO"]).ToString("###,###,##0.####"));
                tr = tr.Replace("@@단가", D.GetDecimal(rowL["UM_SO"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@금액", D.GetDecimal(rowL["AM_SO"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@라인비고", D.GetString(rowL["DC2"]));

                line += tr;
            }

            body = body.Replace("@@라인추가", line);

            return body;
        }

        private string GetLineGALAXIA()
        {
            string tr = @"
        <tr height='25'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@NO</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'>@@품명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2'>@@규격</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; padding-right:10px' align='right'>@@수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; padding-right:10px' align='right'>@@단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000; padding-right:10px' align='right'>@@금액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@라인비고</td>
		</tr>";
            return tr;
        }

        #endregion

        #region -> 원익 출하의뢰서 html

        private string WonikHtml(DataRow rowH, DataTable dt)
        {
            string body = string.Empty;
            string line = string.Empty;
            
            #region -> 기존소스(주석처리)
            /*
            DataTable dt_Gw_Info = DBHelper.GetDataTable("UP_SA_Z_WONIK_EST_GW_INFO_S", new object[] { MA.Login.회사코드, D.GetString(rowH["CD_PARTNER"]), D.GetString(rowH["DT_SO"]) });

            body =

@"<head>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
</head>
<center>
<body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0'>
	<table width='945' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: collapse; border: 0;'>
		<colgroup width='12%' align='center'></colgroup>
		<colgroup width='13%' align='center'></colgroup>
		<colgroup width='12%' align='center'></colgroup>
		<colgroup width='13%' align='center'></colgroup>
		<colgroup width='12%' align='center'></colgroup>
		<colgroup width='13%' align='center'></colgroup>
		<colgroup width='12%' align='center'></colgroup>
		<colgroup width='13%' align='center'></colgroup>
		
		<tr height='10'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>수주번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수주번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>수주일자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수주일자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>프로젝트</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@프로젝트명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>담당자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@담당자명</td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>거래처명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@거래처명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>거래구분</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@거래구분</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>출고구분</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@출고구분</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>출고창고</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@출고창고명</td>
		</tr>
		
        <tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>여신한도</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@여신한도</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>총채권잔액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@총채권잔액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>60일이상채권</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@60일이상채권</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>외상매출금</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@전체외상매출금</td>
		</tr>

		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>매출액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@전체매출액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>매출이익</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@전체매출이익</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>매출이익율</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@전체_매출이익율</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>매출할인율</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@할인율</td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>수금예정일</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수금예정일</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>환율</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@환율</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>구분</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>특가판매</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
		</tr>
		
		<tr height='10'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
	</table>
	
	<table width='945' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: collapse; border: 0;'>
		<colgroup width='4%' align='center'></colgroup>
		<colgroup width='10%' align='center'></colgroup>
		<colgroup width='4%' align='center'></colgroup>
		<colgroup width='4%' align='center'></colgroup>
		<colgroup width='4%' align='center'></colgroup>
		<colgroup width='8%' align='center'></colgroup>
		<colgroup width='9%' align='center'></colgroup>
		<colgroup width='9%' align='center'></colgroup>
		<colgroup width='9%' align='center'></colgroup>
		<colgroup width='9%' align='center'></colgroup>
		<colgroup width='8%' align='center'></colgroup>
		<colgroup width='8%' align='center'></colgroup>
        <colgroup width='10%' align='center'></colgroup>
		<colgroup width='5%' align='center'></colgroup>
		
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>NO</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>품목코드</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>규격</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>단위</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>기준단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>매출액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>외상매출금</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>매출이익</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>이익율(%)</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>할인율(%)</td>
            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>예상원가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>평가월</td>
		</tr>
		
		@@추가
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='3'>합&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 계</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SUM수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SUM매출액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SUM외상매출금</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SUM매출이익</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SUM매출_이익율</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SUM할인율</td>
            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SUM예상원가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
		</tr>
		
	</table>
<table width='945' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: collapse; border: 0 none'>
	<colgroup width='10%' align='center'>
	</colgroup>
	<colgroup width='15%' align='center'>
	</colgroup>
	<colgroup width='10%' align='center'>
	</colgroup>
	<colgroup width='15%' align='center'>
	</colgroup>
	<colgroup width='10%' align='center'>
	</colgroup>
	<colgroup width='15%' align='center'>
	</colgroup>
	<colgroup width='10%' align='center'>
	</colgroup>
	<colgroup width='15%' align='center'>
	</colgroup>
	<tr height='10'>
		<td style='border: 0 solid #000000'></td>
		<td style='border: 0 solid #000000'></td>
		<td style='border: 0 solid #000000'></td>
		<td style='border: 0 solid #000000'></td>
		<td style='border: 0 solid #000000'></td>
		<td style='border: 0 solid #000000'></td>
		<td style='border: 0 solid #000000'></td>
		<td style='border: 0 solid #000000'></td>
	</tr>
	<tr height='30'>
		<td style='border: 1 solid #000000' bgcolor='#CCCCCC'>비고</td>
		<td style='border: 1 solid #000000' colspan='7'>
		<p align='left'>　</td>
	</tr>
</table>
	</body>
</center>";
            decimal Sum_매출이익 = D.GetDecimal(dt.Compute("SUM(AM_PROFIT)", ""));
            decimal Sum_매출액 = D.GetDecimal(dt.Compute("SUM(AM_WONAMT)", ""));
            decimal Sum_기준판매금액 = D.GetDecimal(dt.Compute("SUM(AM_STD_SALE)", ""));

            body = body.Replace("@@수주번호", D.GetString(rowH["NO_SO"]));
            body = body.Replace("@@수주일자", D.GetString(rowH["DT_SO"]).Substring(0, 4) + "년" + D.GetString(rowH["DT_SO"]).Substring(4, 2) + "월" + D.GetString(rowH["DT_SO"]).Substring(6, 2) + "일");
            body = body.Replace("@@프로젝트명", D.GetString(dt.Rows[0]["NM_PROJECT"]));
            body = body.Replace("@@담당자명", D.GetString(rowH["NM_KOR"]));
            body = body.Replace("@@거래처명", D.GetString(rowH["LN_PARTNER"]));
            body = body.Replace("@@거래구분", D.GetString(rowH["NM_SO"]));
            body = body.Replace("@@출고구분", D.GetString(CodeSearch.GetCodeInfo(Duzon.ERPU.MF.MasterSearch.MA_CODEDTL, new object[] { MA.Login.회사코드, "PU_C000027", D.GetString(rowH["RET"]) })["NM_SYSDEF"]));
            body = body.Replace("@@출고창고명", D.GetString(dt.Rows[0]["NM_SL"]));
            body = body.Replace("@@전체외상매출금", D.GetDecimal(dt.Compute("SUM(AM_SUM)", "")).ToString("###,###,###,##0"));
            body = body.Replace("@@전체매출액", D.GetDecimal(dt.Compute("SUM(AM_SO)", "")).ToString("##,##0.####"));
            body = body.Replace("@@전체매출이익", Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dt.Compute("SUM(AM_PROFIT)", ""))).ToString("##,##0.####"));
            body = body.Replace("@@전체_매출이익율", (Sum_매출액 == 0 ? "0" : ((Sum_매출이익 / Sum_매출액) * 100).ToString("N2")));
            body = body.Replace("@@할인율", string.Empty);
            body = body.Replace("@@환율", D.GetString(dt.Rows[0]["RT_EXCH"]));
            body = body.Replace("@@SUM수량", D.GetDecimal(dt.Compute("SUM(QT_SO)", "")).ToString("###,###,###,##0"));
            body = body.Replace("@@SUM매출액", D.GetDecimal(dt.Compute("SUM(AM_WONAMT)", "")).ToString("###,###,###,##0"));
            body = body.Replace("@@SUM외상매출금", D.GetDecimal(dt.Compute("SUM(AM_SUM)", "")).ToString("###,###,###,##0"));
            body = body.Replace("@@SUM예상원가", Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dt.Compute("SUM(AM_INV)", ""))).ToString("##,##0.####"));
            body = body.Replace("@@SUM매출이익", Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dt.Compute("SUM(AM_PROFIT)", ""))).ToString("##,##0.####"));
            body = body.Replace("@@SUM매출_이익율", (Sum_매출액 == 0 ? "0" : ((Sum_매출이익 / Sum_매출액) * 100).ToString("N2")));
            body = body.Replace("@@SUM할인율", Sum_기준판매금액 == 0 ? "0" : (((Sum_기준판매금액 - Sum_매출액) / Sum_기준판매금액) * 100).ToString("N2"));
            body = body.Replace("@@수금예정일", D.GetString(dt.Rows[0]["DT_DUEDATE"]) == string.Empty ? string.Empty: D.GetString(dt.Rows[0]["DT_DUEDATE"]).Substring(0, 4) + "년" + D.GetString(dt.Rows[0]["DT_DUEDATE"]).Substring(4, 2) + "월" + D.GetString(dt.Rows[0]["DT_DUEDATE"]).Substring(6, 2) + "일");
            body = body.Replace("@@여신한도", D.GetDecimal(dt_Gw_Info.Rows[0]["TOT_CREDIT"]).ToString("###,###,###,##0"));
            body = body.Replace("@@총채권잔액", D.GetDecimal(dt_Gw_Info.Rows[0]["AM_RAMAIN"]).ToString("###,###,###,##0"));
            body = body.Replace("@@60일이상채권", D.GetDecimal(dt_Gw_Info.Rows[0]["IN_DAY_60_UP"]).ToString("###,###,###,##0"));

            int Cnt = 1;
            foreach (DataRow dr in dt.Rows)
            {
                string tr = string.Empty;

                tr = GetLine();
                tr = tr.Replace("@@순번", D.GetString(Cnt));
                tr = tr.Replace("@@품목코드", D.GetString(dr["CD_ITEM"]));
                tr = tr.Replace("@@규격", D.GetString(dr["STND_ITEM"]));
                tr = tr.Replace("@@수량", D.GetDecimal(dr["QT_SO"]).ToString("###,###,##0"));
                tr = tr.Replace("@@단위", D.GetString(dr["UNIT_IM"]));
                tr = tr.Replace("@@기준단가", D.GetDecimal(dr["PITEM_NUM_USERDEF1"]).ToString("##,##0.##"));
                tr = tr.Replace("@@단가", D.GetDecimal(dr["UM_SO"]).ToString("##,##0.####"));
                tr = tr.Replace("@@매출액", D.GetDecimal(dr["AM_WONAMT"]).ToString("###,###,###,##0"));
                tr = tr.Replace("@@외상매출금", D.GetDecimal(dr["AM_SUM"]).ToString("###,###,###,##0"));
                tr = tr.Replace("@@예상원가", D.GetDecimal(dr["AM_INV"]).ToString("###,###,###,##0"));
                tr = tr.Replace("@@매출이익", D.GetDecimal(dr["AM_PROFIT"]).ToString("###,###,###,##0"));
                tr = tr.Replace("@@매출_이익율", D.GetDecimal(dr["AM_WONAMT"]) == 0 ? "0" : D.GetDecimal((D.GetDecimal(dr["AM_PROFIT"]) / D.GetDecimal(dr["AM_WONAMT"])) * 100).ToString("N2"));
                tr = tr.Replace("@@할인율", D.GetDecimal(dr["RT_STD_SALE"]).ToString("N2"));
                tr = tr.Replace("@@평가월", D.GetString(dr["DT_INV"]));

                line += tr;

                Cnt++;
            }

            body = body.Replace("@@추가", line);
            */
            #endregion


            body =
            #region html body
 @"
<head>
<meta http-equiv='Content-Language' content='ko'>
<meta http-equiv='Content-Type' content='text/html; charset=ks_c_5601-1987'>
<title>수주번호</title>
</head>

<center>
<body>
<table width='100%' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: collapse; border: 0 none'>
	<colgroup>
		<col width='12%'><col width='13%'><col width='12%'><col width='13%'>
		<col width='12%'><col width='13%'><col width='12%'><col width='13%'>
	</colgroup>
	<tr height='23'>
		<td align='center' bgcolor='#92D050'><b>수주번호</b></td>
		<td align='center'>@@수주번호</td>
		<td align='center' bgcolor='#92D050'><b>수주일자</b></td>
		<td align='center'>@@수주일자</td>
		<td align='center' bgcolor='#92D050'><b>납기일</b></td>
		<td align='center'>@@납기일</td>
		<td align='center' bgcolor='#92D050'><b>수금예정일</b></td>
		<td align='center'>@@수금예정일</td>
	</tr>
	<tr height='23'>
		<td align='center' bgcolor='#92D050'><b>부서코드</b></td>
		<td align='center'>@@부서코드</td>
		<td align='center' bgcolor='#92D050'><b>부서명</b></td>
		<td align='center'>@@부서명</td>
		<td align='center' bgcolor='#92D050'><b>프로젝트코드</b></td>
		<td align='center'>@@프로젝트코드</td>
		<td align='center' bgcolor='#92D050'><b>프로젝트명</b></td>
		<td align='center'>@@프로젝트명</td>
	</tr>
	<tr height='23'>
		<td align='center' bgcolor='#92D050'><b>거래처코드</b></td>
		<td align='center'>@@거래처코드</td>
		<td align='center' bgcolor='#92D050'><b>거래처명</b></td>
		<td align='center'>@@거래처명</td>
		<td align='center' bgcolor='#92D050'><b>영업담당자</b></td>
		<td align='center'>@@영업담당자</td>
		<td align='center' bgcolor='#92D050'><b>거래구분</b></td>
		<td align='center'>@@거래구분</td>
	</tr>
	<tr height='23'>
		<td align='center' bgcolor='#92D050'><b>결제조건</b></td>
		<td align='center'>@@결제조건</td>
		<td align='center' bgcolor='#92D050'><b>출고창고</b></td>
		<td align='center'>@@출고창고</td>
		<td align='center' bgcolor='#92D050'><b>출고구분</b></td>
		<td align='center'>@@출고구분</td>
		<td align='center' bgcolor='#92D050'><b>환율</b></td>
		<td align='center'>@@환율</td>
	</tr>
	<tr height='23'>
		<td align='center' bgcolor='#92D050'><b>매출액(VAT포함)</b></td>
		<td align='center'>@@매출액</td>
		<td align='center' bgcolor='#92D050'><b>매출이익</b></td>
		<td align='center'>@@매출이익</td>
		<td align='center' bgcolor='#92D050'><b>매출이익율</b></td>
		<td align='center'>@@매출_이익율</td>
		<td align='center' bgcolor='#92D050'><b>매출할인율</b></td>
		<td align='center'>@@매출할인율</td>
	</tr>
	<tr height='10'>
		<td style='border: 0px none' align='center' colspan='8'>　</td>
	</tr>
	<tr height='23'>
		<td align='center' bgcolor='#92D050'><b>총여신한도</b></td>
		<td align='center' bgcolor='#92D050'><b>(담보여신)</b></td>
		<td align='center' bgcolor='#92D050'><b>미수금</b></td>
		<td align='center' bgcolor='#92D050'><b>미발행</b></td>
		<td align='center' bgcolor='#92D050'><b>자수어음</b></td>
		<td align='center' bgcolor='#92D050'>
		<font style='font-size: 8pt; font-weight: 700'>합계(미수금+미발행<br>
		+자수어음)</font></td>
		<td align='center' bgcolor='#92D050'><b>타수어음</b></td>
		<td align='center' bgcolor='#92D050'><b>과부족</b></td>
	</tr>
	<tr height='23'>
		<td align='center'>@@총여신한도</td>
		<td align='center'>@@담보여신</td>
		<td align='center'>@@미수금</td>
		<td align='center'>@@미발행</td>
		<td align='center'>@@자수어음</td>
		<td align='center'>@@합계</td>
		<td align='center'>@@타수어음</td>
		<td align='center'>@@과부족</td>
	</tr>
	<tr height='10'>
		<td style='border: 0px none' align='center' colspan='8'>　</td>
	</tr>
	<tr height='23'>
		<td align='center'></td>
		<td align='center' bgcolor='#92D050'><b>M-0</b></td>
		<td align='center' bgcolor='#92D050'><b>M-1</b></td>
		<td align='center' bgcolor='#92D050'><b>M-2</b></td>
		<td align='center' bgcolor='#92D050'><b>M-3</b></td>
		<td align='center' bgcolor='#92D050'><b>M-4</b></td>
		<td align='center' bgcolor='#92D050'><b>M-5(이상)</b></td>
		<td align='center' bgcolor='#92D050'><b>연체금액</b></td>
	</tr>
	<tr height='23'>
		<td align='center' bgcolor='#92D050'><b>미수금현황</b></td>
		<td align='center'>@@M0미수금</td>
		<td align='center'>@@M1미수금</td>
		<td align='center'>@@M2미수금</td>
		<td align='center'>@@M3미수금</td>
		<td align='center'>@@M4미수금</td>
		<td align='center'>@@M5미수금</td>
		<td align='center'>@@연체미수금</td>
	</tr>
	<tr height='23'>
		<td align='center' bgcolor='#92D050'><b>수금현황</b></td>
		<td align='center'>@@M0수금</td>
		<td align='center'>@@M1수금</td>
		<td align='center'>@@M2수금</td>
		<td align='center'>@@M3수금</td>
		<td align='center'>@@M4수금</td>
		<td align='center'>@@M5수금</td>
		<td align='center'></td>
	</tr>
</table>

<table width='100%' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: collapse; border: 0 none'>
	<tr height='10'>
		<td style='border: 0px none' align='center' colspan='14'>　</td>
	</tr>
	<tr height='23'>
		<td align='center' bgcolor='#92D050'><b>No.</b></td>
		<td align='center' bgcolor='#92D050'><b>품목코드</b></td>
		<td align='center' bgcolor='#92D050'><b>규격</b></td>
		<td align='center' bgcolor='#92D050'><b>수량</b></td>
		<td align='center' bgcolor='#92D050'><b>단위</b></td>
		<td align='center' bgcolor='#92D050'>
		<span style='font-weight: 700; font-size: 8pt'>기준단가</span></td>
		<td align='center' bgcolor='#92D050'><b>단가</b></td>
		<td align='center' bgcolor='#92D050'><b>매출액</b></td>
		<td align='center' bgcolor='#92D050'><b>외상매출금</b></td>
		<td align='center' bgcolor='#92D050'><b>매출이익</b></td>
		<td align='center' bgcolor='#92D050'><b>이익율(%)</b></td>
		<td align='center' bgcolor='#92D050'><b>할인율(%)</b></td>
		<td align='center' bgcolor='#92D050'><b>예상원가</b></td>
		<td align='center' bgcolor='#92D050'><b>평가월</b></td>
	</tr>
@@추가
	<tr height='23'>
		<td align='center' colspan='3'><b>합&nbsp;&nbsp;&nbsp;&nbsp; 계</b></td>
		<td align='center'>@@SUM수량</td>
		<td align='center'></td>
		<td align='center'></td>
		<td align='center'></td>
		<td align='center'>@@SUM매출액</td>
		<td align='center'>@@SUM외상매출금</td>
		<td align='center'>@@SUM매출이익</td>
		<td align='center'>@@SUM매출_이익율</td>
		<td align='center'>@@SUM할인율</td>
		<td align='center'>@@SUM예상원가</td>
		<td align='center'></td>
	</tr>
	<tr height='10'>
		<td style='border: 0px none' align='center' colspan='14'>　</td>
	</tr>
</table>

</body>
</center>
";
            #endregion

            //
            // ds_Gw.Tables[0] : 수주정보(헤더&라인)
            // ds_Gw.Tables[1] : 거래처별 수금 & 미수금
            // ds_Gw.Tables[2] : 기타 (미발행, 자수어음, 타수어음)
            // 

            DataSet ds_Gw = DBHelper.GetDataSet("UP_SA_Z_WONIK_EST_GW_INFO_S2", new object[] { MA.Login.회사코드, D.GetString(dt.Rows[0]["CD_PLANT"]), D.GetString(rowH["NO_SO"]) });

            decimal Sum_매출이익 = D.GetDecimal(ds_Gw.Tables[0].Compute("SUM(AM_PROFIT)", ""));
            decimal Sum_매출액 = D.GetDecimal(ds_Gw.Tables[0].Compute("SUM(AM_WONAMT)", ""));
            decimal Sum_기준판매금액 = D.GetDecimal(ds_Gw.Tables[0].Compute("SUM(AM_STD_SALE)", ""));

            body = body.Replace("@@수주번호", D.GetString(ds_Gw.Tables[0].Rows[0]["NO_SO"]));
            body = body.Replace("@@수주일자", D.GetString(ds_Gw.Tables[0].Rows[0]["DT_SO"]).Substring(0, 4) + "년" + D.GetString(ds_Gw.Tables[0].Rows[0]["DT_SO"]).Substring(4, 2) + "월" + D.GetString(ds_Gw.Tables[0].Rows[0]["DT_SO"]).Substring(6, 2) + "일");

            if (D.GetString(ds_Gw.Tables[0].Rows[0]["DT_DUEDATE"]).Length == 8)
                body = body.Replace("@@납기일", D.GetString(ds_Gw.Tables[0].Rows[0]["DT_DUEDATE"]).Substring(0, 4) + "년" + D.GetString(ds_Gw.Tables[0].Rows[0]["DT_DUEDATE"]).Substring(4, 2) + "월" + D.GetString(ds_Gw.Tables[0].Rows[0]["DT_DUEDATE"]).Substring(6, 2) + "일");
            else 
                body = body.Replace("@@납기일", "");

            body = body.Replace("@@수금예정일", D.GetString(ds_Gw.Tables[0].Rows[0]["DT_RCP_PREARRANGED"]) + "일");
            body = body.Replace("@@부서코드", D.GetString(ds_Gw.Tables[0].Rows[0]["CD_DEPT"]));
            body = body.Replace("@@부서명", D.GetString(ds_Gw.Tables[0].Rows[0]["NM_DEPT"]));
            body = body.Replace("@@프로젝트코드", D.GetString(ds_Gw.Tables[0].Rows[0]["NO_PROJECT"]));
            body = body.Replace("@@프로젝트명", D.GetString(ds_Gw.Tables[0].Rows[0]["NM_PROJECT"]));
            body = body.Replace("@@거래처코드", D.GetString(ds_Gw.Tables[0].Rows[0]["CD_PARTNER"]));
            body = body.Replace("@@거래처명", D.GetString(ds_Gw.Tables[0].Rows[0]["LN_PARTNER"]));
            body = body.Replace("@@영업담당자", D.GetString(ds_Gw.Tables[0].Rows[0]["NM_EMP_SALE"]));
            body = body.Replace("@@거래구분", D.GetString(ds_Gw.Tables[0].Rows[0]["NM_TP_SO"]));
            body = body.Replace("@@결제조건", D.GetString(ds_Gw.Tables[0].Rows[0]["NM_FG_PAYBILL"]));
            body = body.Replace("@@출고창고", D.GetString(ds_Gw.Tables[0].Rows[0]["NM_SL"]));
            body = body.Replace("@@출고구분", D.GetString(ds_Gw.Tables[0].Rows[0]["NM_FG_TRANSPORT"]));
            body = body.Replace("@@환율", D.GetString(ds_Gw.Tables[0].Rows[0]["RT_EXCH"]));
            body = body.Replace("@@매출액", D.GetDecimal(ds_Gw.Tables[0].Compute("SUM(AM_SUM)", "")).ToString("##,##0.####"));
            body = body.Replace("@@매출이익", Unit.원화금액(DataDictionaryTypes.SA, Sum_매출이익).ToString("##,##0.####"));
            body = body.Replace("@@매출_이익율", (Sum_매출액 == 0 ? "0" : ((Sum_매출이익 / Sum_매출액) * 100).ToString("N2")));
            body = body.Replace("@@매출할인율", Sum_기준판매금액 == 0 ? "0" : (((Sum_기준판매금액 - Sum_매출액) / Sum_기준판매금액) * 100).ToString("N2"));

            decimal 총여신한도 = D.GetDecimal(ds_Gw.Tables[0].Rows[0]["TOT_CREDIT"]);
            decimal 미수금 = D.GetDecimal(ds_Gw.Tables[1].Rows[0]["미수금TOT"]);
            decimal 미발행 = D.GetDecimal(ds_Gw.Tables[2].Rows[0]["미발행"]);
            decimal 자수어음 = D.GetDecimal(ds_Gw.Tables[2].Rows[0]["자수어음"]);
            decimal 합계 = (미수금 + 미발행 + 자수어음);

            body = body.Replace("@@총여신한도", 총여신한도.ToString("###,###,###,##0"));
            body = body.Replace("@@담보여신", D.GetDecimal(ds_Gw.Tables[0].Rows[0]["AM_REAL"]).ToString("###,###,###,##0"));
            body = body.Replace("@@미수금", 미수금.ToString("###,###,###,##0"));
            body = body.Replace("@@미발행", 미발행.ToString("###,###,###,##0"));
            body = body.Replace("@@자수어음", 자수어음.ToString("###,###,###,##0"));
            body = body.Replace("@@합계", 합계.ToString("###,###,###,##0"));
            body = body.Replace("@@타수어음", D.GetDecimal(ds_Gw.Tables[2].Rows[0]["타수어음"]).ToString("###,###,###,##0"));
            body = body.Replace("@@과부족", (합계 - 총여신한도).ToString("###,###,###,##0"));

            body = body.Replace("@@M0미수금", D.GetDecimal(ds_Gw.Tables[1].Rows[0]["미수금0"]).ToString("###,###,###,##0"));
            body = body.Replace("@@M1미수금", D.GetDecimal(ds_Gw.Tables[1].Rows[0]["미수금1"]).ToString("###,###,###,##0"));
            body = body.Replace("@@M2미수금", D.GetDecimal(ds_Gw.Tables[1].Rows[0]["미수금2"]).ToString("###,###,###,##0"));
            body = body.Replace("@@M3미수금", D.GetDecimal(ds_Gw.Tables[1].Rows[0]["미수금3"]).ToString("###,###,###,##0"));
            body = body.Replace("@@M4미수금", D.GetDecimal(ds_Gw.Tables[1].Rows[0]["미수금4"]).ToString("###,###,###,##0"));
            body = body.Replace("@@M5미수금", D.GetDecimal(ds_Gw.Tables[1].Rows[0]["미수금5"]).ToString("###,###,###,##0"));
            body = body.Replace("@@연체미수금", D.GetDecimal(ds_Gw.Tables[1].Rows[0]["미수금0"]).ToString("###,###,###,##0"));

            body = body.Replace("@@M0수금", D.GetDecimal(ds_Gw.Tables[1].Rows[0]["수금0"]).ToString("###,###,###,##0"));
            body = body.Replace("@@M1수금", D.GetDecimal(ds_Gw.Tables[1].Rows[0]["수금1"]).ToString("###,###,###,##0"));
            body = body.Replace("@@M2수금", D.GetDecimal(ds_Gw.Tables[1].Rows[0]["수금2"]).ToString("###,###,###,##0"));
            body = body.Replace("@@M3수금", D.GetDecimal(ds_Gw.Tables[1].Rows[0]["수금3"]).ToString("###,###,###,##0"));
            body = body.Replace("@@M4수금", D.GetDecimal(ds_Gw.Tables[1].Rows[0]["수금4"]).ToString("###,###,###,##0"));
            body = body.Replace("@@M5수금", D.GetDecimal(ds_Gw.Tables[1].Rows[0]["수금5"]).ToString("###,###,###,##0"));

            body = body.Replace("@@SUM수량", D.GetDecimal(ds_Gw.Tables[0].Compute("SUM(QT_SO)", "")).ToString("###,###,###,##0"));
            body = body.Replace("@@SUM매출액", D.GetDecimal(ds_Gw.Tables[0].Compute("SUM(AM_WONAMT)", "")).ToString("##,##0.####"));
            body = body.Replace("@@SUM외상매출금", D.GetDecimal(ds_Gw.Tables[0].Compute("SUM(AM_SUM)", "")).ToString("##,##0.####"));
            body = body.Replace("@@SUM매출이익", Unit.원화금액(DataDictionaryTypes.SA, Sum_매출이익).ToString("##,##0.####"));
            body = body.Replace("@@SUM매출_이익율", (Sum_매출액 == 0 ? "0" : ((Sum_매출이익 / Sum_매출액) * 100).ToString("N2")));
            body = body.Replace("@@SUM할인율", Sum_기준판매금액 == 0 ? "0" : (((Sum_기준판매금액 - Sum_매출액) / Sum_기준판매금액) * 100).ToString("N2"));
            body = body.Replace("@@SUM예상원가", D.GetDecimal(ds_Gw.Tables[0].Compute("SUM(AM_INV)", "")).ToString("###,###,###,##0"));

            int Cnt = 1;
            foreach (DataRow dr in ds_Gw.Tables[0].Rows)
            {
                string tr = string.Empty;

                tr = GetLine();
                tr = tr.Replace("@@순번", D.GetString(Cnt));
                tr = tr.Replace("@@품목코드", D.GetString(dr["CD_ITEM"]));
                tr = tr.Replace("@@규격", D.GetString(dr["STND_ITEM"]));
                tr = tr.Replace("@@수량", D.GetDecimal(dr["QT_SO"]).ToString("###,###,##0"));
                tr = tr.Replace("@@단위", D.GetString(dr["UNIT_IM"]));
                tr = tr.Replace("@@기준단가", D.GetDecimal(dr["UM_STND"]).ToString("##,##0.##"));
                tr = tr.Replace("@@단가", D.GetDecimal(dr["UM_SO"]).ToString("##,##0.####"));
                tr = tr.Replace("@@매출액", D.GetDecimal(dr["AM_WONAMT"]).ToString("###,###,###,##0"));
                tr = tr.Replace("@@외상매출금", D.GetDecimal(dr["AM_SUM"]).ToString("###,###,###,##0"));
                tr = tr.Replace("@@매출이익", D.GetDecimal(dr["AM_PROFIT"]).ToString("###,###,###,##0"));
                tr = tr.Replace("@@매출_이익율", D.GetDecimal(dr["AM_WONAMT"]) == 0 ? "0" : D.GetDecimal((D.GetDecimal(dr["AM_PROFIT"]) / D.GetDecimal(dr["AM_WONAMT"])) * 100).ToString("N2"));
                tr = tr.Replace("@@할인율", D.GetDecimal(dr["AM_STD_SALE"]) == 0 ? "0" : (((D.GetDecimal(dr["AM_STD_SALE"]) - D.GetDecimal(dr["AM_WONAMT"])) / D.GetDecimal(dr["AM_STD_SALE"])) * 100).ToString("N2"));
                tr = tr.Replace("@@예상원가", D.GetDecimal(dr["AM_INV"]).ToString("###,###,###,##0"));
                tr = tr.Replace("@@평가월", D.GetString(dr["DT_INV"]));

                line += tr;

                Cnt++;
            }

            body = body.Replace("@@추가", line);

            return body;
        }

        #endregion

        #region -> 원익 출하의뢰서 라인추가

        private string GetLine()
        {
            #region -> 기존소스(주석처리)
            /*
            string tr =
        @"<tr>
	        <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@순번</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@품목코드</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@규격</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@단위</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@기준단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@매출액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@외상매출금</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@매출이익</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@매출_이익율</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@할인율</td>
            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@예상원가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@평가월</td>

        </tr>";
            */
            #endregion


            string tr =
            #region html Line
 @"
	<tr height='23'>
		<td align='center'>@@순번</td>
		<td align='center'>@@품목코드</td>
		<td align='center'>@@규격</td>
		<td align='center'>@@수량</td>
		<td align='center'>@@단위</td>
		<td align='center'>@@기준단가</td>
		<td align='center'>@@단가</td>
		<td align='center'>@@매출액</td>
		<td align='center'>@@외상매출금</td>
		<td align='center'>@@매출이익</td>
		<td align='center'>@@매출_이익율</td>
		<td align='center'>@@할인율</td>
		<td align='center'>@@예상원가</td>
		<td align='center'>@@평가월</td>
	</tr>
";
            #endregion

            return tr;
        }

        #endregion

        #region -> 원익 반품의뢰서 html

        private string WonikReturnHtml(DataRow rowH, DataTable dt)
        {
            string body = string.Empty;
            string line = string.Empty;

            body =
@"<head>

<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
</head>

<center>
<body leftmargin='0' marginwidth='0' topmargin='0' marginheight='0'>
	<table width='945' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: collapse; border: 0;'>
		<colgroup width='11%' align='center'></colgroup>
		<colgroup width='14%' align='center'></colgroup>
		<colgroup width='11%' align='center'></colgroup>
		<colgroup width='14%' align='center'></colgroup>
		<colgroup width='11%' align='center'></colgroup>
		<colgroup width='14%' align='center'></colgroup>
		<colgroup width='11%' align='center'></colgroup>
		<colgroup width='14%' align='center'></colgroup>
		
		<tr height='10'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>반품수주번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@반품수주번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>반품수주일자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@반품수주일자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>수주형태</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수주형태</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>거래처명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@거래처명</td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>반품총액<br/>(부가세포함)</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@반품총액부가세포함</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>반품금액<br/>(부가세제외)</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@반품금액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>반품창고</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@창고명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>환종</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@환종</td>
		</tr>
		
		<tr height='10'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
	</table>
	
	<table width='945' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: collapse; border: 0;'>
		<colgroup width='4%' align='center'></colgroup>
		<colgroup width='10%' align='center'></colgroup>
		<colgroup width='12%' align='center'></colgroup>
		<colgroup width='4%' align='center'></colgroup>
		<colgroup width='8%' align='center'></colgroup>
		<colgroup width='12%' align='center'></colgroup>
		<colgroup width='12%' align='center'></colgroup>
		<colgroup width='12%' align='center'></colgroup>
        <colgroup width='12%' align='center'></colgroup>
<colgroup width='12%' align='center'></colgroup>
		
		<tr height='10'>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
            <td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>NO</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>품목코드</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>품 목 명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>규&nbsp; 격</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>단위</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>금&nbsp; 액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>부가세</td>
            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>합계금액</td>
		</tr>
		
		 @@추가
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='5'>합&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 계</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SUM수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SUM금액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SUM부가세</td>
            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SUM합계금액</td>
		</tr>
		
	</table>
	</body>
</center>";

            body = body.Replace("@@반품수주번호", D.GetString(rowH["NO_SO"]));
            body = body.Replace("@@반품수주일자", D.GetString(rowH["DT_SO"]).Substring(0, 4) + "년" + D.GetString(rowH["DT_SO"]).Substring(4, 2) + "월" + D.GetString(rowH["DT_SO"]).Substring(6, 2) + "일");
            body = body.Replace("@@거래처명", D.GetString(rowH["LN_PARTNER"]));
            body = body.Replace("@@수주형태", D.GetString(rowH["NM_SO"]));
            body = body.Replace("@@창고명", D.GetString(dt.Rows[0]["NM_SL"]));
            body = body.Replace("@@환종", D.GetString(CodeSearch.GetCodeInfo(Duzon.ERPU.MF.MasterSearch.MA_CODEDTL, new object[] { MA.Login.회사코드, "MA_B000005", D.GetString(dt.Rows[0]["CD_EXCH"]) })["NM_SYSDEF"]));
            body = body.Replace("@@SUM수량", D.GetDecimal(dt.Compute("SUM(QT_SO)", "")).ToString("###,###,###,##0"));
            body = body.Replace("@@SUM금액", D.GetDecimal(dt.Compute("SUM(AM_SO)", "")).ToString("###,###,###,##0.####"));
            body = body.Replace("@@SUM부가세", D.GetDecimal(dt.Compute("SUM(AM_VAT)", "")).ToString("###,###,###,##0"));
            body = body.Replace("@@SUM합계금액", D.GetDecimal(dt.Compute("SUM(AM_SUM)", "")).ToString("###,###,###,##0"));
            body = body.Replace("@@반품총액부가세포함", D.GetDecimal(dt.Compute("SUM(AM_SUM)", "")).ToString("###,###,###,##0"));
            body = body.Replace("@@반품금액", D.GetDecimal(dt.Compute("SUM(AM_SO)", "")).ToString("###,###,###,##0.####"));

            int Cnt = 1;

            foreach (DataRow dr in dt.Rows)
            {
                string tr = string.Empty;

                tr = GetReturnLine();
                tr = tr.Replace("@@순번", D.GetString(Cnt));
                tr = tr.Replace("@@품목코드", D.GetString(dr["CD_ITEM"]));
                tr = tr.Replace("@@품목명", D.GetString(dr["NM_ITEM"]));
                tr = tr.Replace("@@규격", D.GetString(dr["STND_ITEM"]));
                tr = tr.Replace("@@단위", D.GetString(dr["UNIT_IM"]));
                tr = tr.Replace("@@단가", D.GetDecimal(dr["UM_SO"]).ToString("##,##0.####"));
                tr = tr.Replace("@@수량", D.GetDecimal(dr["QT_SO"]).ToString("###,###,###,##0"));
                tr = tr.Replace("@@금액", D.GetDecimal(dr["AM_SO"]).ToString("###,###,###,##0.####"));
                tr = tr.Replace("@@부가세", D.GetDecimal(dr["AM_VAT"]).ToString("###,###,###,##0"));
                tr = tr.Replace("@@합계금액", D.GetDecimal(dr["AM_SUM"]).ToString("###,###,###,##0"));

                line += tr;
                Cnt++;
            }

            body = body.Replace("@@추가", line);

            return body;
        }

        #endregion

        #region -> 원익 반품의뢰서 라인추가

        private string GetReturnLine()
        {
            string tr =
          @"<tr>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@순번</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@품목코드</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@품목명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@규격</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@단위</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@단가</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@금액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@부가세</td>
            <td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@합계금액</td>
            </tr>";

            return tr;
        }

        #endregion

        #region ♣ 전자결재

        #region -> 개발서버용 결재상신(개발서버에서 TEST용)

        bool 결재상신_개발서버(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("UP_SA_GWDOCU_DUZON", obj);
        }

        #endregion

        #region -> 실제 업체사용 결재상신

        bool 결재상신_업체(object[] obj)
        {
            return DBHelper.ExecuteNonQuery("UP_SA_GWDOCU", obj);
        }

        #endregion

        internal DataRow GetProjectHHInfo(string noProject)
        {
            string sqlQuery = " SELECT  TOP 1 CD_MNG_DT1, CD_MNG_DT2, CD_MNG_DT3 "
                            + " FROM    SA_PROJECTHH H "
                            + " WHERE   H.CD_COMPANY = '" + MA.Login.회사코드 + "'"
                            + " AND     H.NO_PROJECT = '" + noProject + "'"
                            + " ORDER BY NO_SEQ DESC";

            DataTable dt = DBHelper.GetDataTable(sqlQuery);

            if (dt == null || dt.Rows.Count == 0)
                return null;

            return dt.Rows[0];
        }

        private string 결재상신상태조회(string noDocu)
        {
            string sqlQuery = " SELECT  ST_STAT "
                            + " FROM    FI_GWDOCU "
                            + " WHERE   CD_COMPANY = '" + MA.Login.회사코드 + "'"
                            + " AND     CD_PC = '" + Global.MainFrame.LoginInfo.CdPc + "'"
                            + " AND     NO_DOCU = '" + noDocu + "'";

            DataTable dt = DBHelper.GetDataTable(sqlQuery);

            if (dt == null || dt.Rows.Count == 0)
                return "2"; //미상신

            return D.GetString(dt.Rows[0]["ST_STAT"]);
        }

        private string 수주텍스트비고조회(string noSo)
        {
            string sqlQuery = " SELECT  DC_RMK_TEXT "
                            + " FROM    SA_SOH "
                            + " WHERE   CD_COMPANY = '" + MA.Login.회사코드 + "'"
                            + " AND     NO_SO = '" + noSo + "'";

            DataTable dt = DBHelper.GetDataTable(sqlQuery);

            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;

            return D.GetString(dt.Rows[0]["DC_RMK_TEXT"]);
        }

        #endregion
    }
}
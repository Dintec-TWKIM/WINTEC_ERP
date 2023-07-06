using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Forms;

namespace sale
{
    class LUXCO_GW 
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
            List.Add(9060); //APP_FORM_KIND에 들어갈 값
            List.Add("29991231");

            switch (Global.MainFrame.ServerKeyCommon.ToUpper())
            {
                case "DZSQL":       //84번 개발서버
                    List[5] = GetLuxcoHtml(rowH, dtL);
                    isSuccess = 결재상신_개발서버(List.ToArray());
                    break;

                case "LUXCO":      //럭스코
                    List[5] = GetLuxcoHtml(rowH, dtL);
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    string strURL = "http://mail.luxco.co.kr//kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(D.GetString(rowH["NO_EST"]), Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;
                default:
                    break;
            }

            return isSuccess;
        }

        #region -> 럭스코 Html양식
        private string GetLuxcoHtml(DataRow rowH, DataTable dtL)
        {
            string body = string.Empty;
            string line = string.Empty;
            string am_sum = string.Empty;

            body = @"
            <head>
            <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
            </head>
 
            <body style='margin: 0; padding: 0;'>
            <table width='645' border='0' cellspacing='0' cellpadding='0' style='border-collapse: collapse; font-family: '굴림'; font-size: 9pt; line-height: 13pt;'>
	        <tr>
		    <td>
			<table width='100%' border='0' cellspacing='0' cellpadding='0' style='border-collapse: collapse; font-family: '굴림'; font-size: 9pt; line-height: 13pt;'>
				<col style='width: 293px;' />
				<col style='width: 80px;' />
				<col style='width: 146px;' />
				<col style='width: 60px;' />
				<col style='width: 146px;' />
				<tr>
					<td style='border: 1px solid #000;'>&nbsp;서기 @@견적일</td>
					<td align='center' style='border: 1px solid #000;'>상&nbsp;&nbsp;호</td>
					<td align='center' colspan='3' style='border: 1px solid #000;'>@@사업장명&nbsp;</td>
				</tr>
				<tr>
					<td style='border: 1px solid #000; border-bottom: 0;'>&nbsp;@@거래처명</td>
					<td align='center' style='border: 1px solid #000;'>대&nbsp;&nbsp;표</td>
					<td align='center' colspan='3' style='border: 1px solid #000;'>@@대표자명&nbsp;</td>
				</tr>
				<tr>
					<td style='border: 1px solid #000; border-top: 0;'>&nbsp;참조 : @@담당부서 </td>
					<td rowspan='2' align='center' style='border: 1px solid #000;'>등록번호</td>
					<td rowspan='2' align='center' style='border: 1px solid #000;'>@@사업자등록번호&nbsp;</td>
					<td align='center' style='border: 1px solid #000;'>업태</td>
					<td align='center' style='border: 1px solid #000;'>@@업태&nbsp;</td>
				</tr>
				<tr>
					<td style='border: 1px solid #000;'>&nbsp;견적의뢰번호 : @@견적번호</td>
					<td align='center' style='border: 1px solid #000;'>종목</td>
					<td align='center' style='border: 1px solid #000;'>@@종목&nbsp;</td>
				</tr>
				<tr>
					<td style='border: 1px solid #000; border-bottom: 0;'>&nbsp;PROJ : </td>
					<td rowspan='3' align='center' style='border: 1px solid #000;'>지&nbsp;&nbsp;점 <br>
						및<br>
						공&nbsp;&nbsp;장 </td>
					<td align='center' colspan='3' style='border: 1px solid #000; border-bottom: 0;'>&nbsp;@@주소</td>
				</tr>
				<tr>
					<td style='border: 1px solid #000; border-top: 0;'>&nbsp;</td>
					<td align='center' colspan='3' style='border: 1px solid #000; border-top: 0; border-bottom: 0;'>&nbsp;TEL : @@전화번호</td>
				</tr>
				<tr>
					<td style='border: 1px solid #000;'>&nbsp;아래와 같이 견적합니다.</td>
					<td align='center' colspan='3' style='border: 1px solid #000; border-top: 0;'>&nbsp;FAX : @@팩스번호</td>
				</tr>
				<tr>
					<td colspan='5' style='border: 1px solid #000;'>&nbsp;금&nbsp;&nbsp;액 :&nbsp;@@한글금액&nbsp;원정&nbsp;&nbsp;&nbsp;(&nbsp;\@@숫자금액&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;VAT 별도</td>
				</tr>
			</table>
			<table width='100%' border='0' cellspacing='0' cellpadding='0' style='border-collapse: collapse; font-family: '굴림'; font-size: 9pt; line-height: 13pt;'>
				<tr>
					<td align='center' style='border: 1px solid #000; border-top: 0; border-right: 1px dotted #000;'>품명 및 사양</td>
					<td align='center' style='border: 1px solid #000; border-top: 0; border-left: 0; border-right: 1px dotted #000;'>단위</td>
					<td align='center' style='border: 1px solid #000; border-top: 0; border-left: 0; border-right: 1px dotted #000;'>수량</td>
					<td align='center' style='border: 1px solid #000; border-top: 0; border-left: 0; border-right: 1px dotted #000;'>단&nbsp;&nbsp;가</td>
					<td align='center' style='border: 1px solid #000; border-top: 0; border-left: 0;'>금&nbsp;&nbsp;액</td>
				</tr>

       	@@추가

               <tr>
					<td colspan='4' align='center' style='border: 1px solid #000; border-right: 1px dotted #000;'>총&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;계</td>
					<td align='right' style='border: 1px solid #000; border-left: 0;'>- 척당</td>
				</tr>
			</table>
			<table width='100%' border='0' cellspacing='0' cellpadding='0' style='border-collapse: collapse; font-family: '굴림'; font-size: 9pt; line-height: 13pt;'>
				<col style='width: 90px;' />
				<col style='width: 555px;' />
				<tr>
					<td align='center' style='border: 1px solid #000; border-top: 0;'>비&nbsp;&nbsp;고</td>
					<td style='border: 1px solid #000; border-top: 0;'>&nbsp;</td>
				</tr>
			</table>
			<table width='100%' border='0' cellspacing='0' cellpadding='0' style='border-collapse: collapse; font-family: '굴림'; font-size: 9pt; line-height: 13pt;'>
				<col style='width: 130px;' />
				<col style='width: 202px;' />
				<col style='width: 120px;' />
				<col style='width: 203px;' />
				<tr>
					<td align='center' style='border: 1px solid #000; border-top: 0;'>납기,제작 완료일</td>
					<td align='center' style='border: 1px solid #000; border-top: 0;'>&nbsp;발주후 15 일 이내</td>
					<td align='center' style='border: 1px solid #000; border-top: 0;'>대금 조건</td>
					<td align='center' style='border: 1px solid #000; border-top: 0;'>&nbsp;귀사 결재 방법에 의함</td>
				</tr>
				<tr>
					<td align='center' style='border: 1px solid #000;'>납품 장소</td>
					<td align='center' style='border: 1px solid #000;'>&nbsp;귀사 공장</td>
					<td align='center' style='border: 1px solid #000;'>유효 기간</td>
					<td align='center' style='border: 1px solid #000;'>&nbsp;제출일로부터 30일</td>
		   </tr>
		   </table>
		   </td>
	       </tr>
           

        </table>
        </center>";

            body = body.Replace("@@견적일", D.GetString(rowH["DT_EST"]).Substring(0, 4) + "년" + D.GetString(rowH["DT_EST"]).Substring(4, 2) + "월" + D.GetString(rowH["DT_EST"]).Substring(6, 2) + "일");
            body = body.Replace("@@사업장명", D.GetString(rowH["NM_BIZAREA"]));
            body = body.Replace("@@거래처명", D.GetString(rowH["LN_PARTNER"]));
            body = body.Replace("@@담당부서", Global.MainFrame.LoginInfo.DeptName);
            body = body.Replace("@@대표자명", D.GetString(rowH["NM_CEO"]));
            body = body.Replace("@@사업자등록번호", D.GetString(rowH["NO_COMPANY"]) != string.Empty ? D.GetString(rowH["NO_COMPANY"]).Substring(0, 3) + "-" + D.GetString(rowH["NO_COMPANY"]).Substring(3, 2) + "-" + D.GetString(rowH["NO_COMPANY"]).Substring(5, 5) : string.Empty);
            body = body.Replace("@@업태", D.GetString(rowH["TP_JOB"]));
            body = body.Replace("@@종목", D.GetString(rowH["CLS_JOB"]));
            body = body.Replace("@@견적번호", D.GetString(rowH["NO_EST"]));
            body = body.Replace("@@주소", D.GetString(rowH["DC_ADS1_D"]));
            body = body.Replace("@@전화번호", D.GetString(rowH["NO_TEL1"]));
            body = body.Replace("@@팩스번호", D.GetString(rowH["NO_FAX1"]));

            am_sum = "일금 " + GetHanjaAmt(Convert.ToInt64(dtL.Compute("SUM(AM_K_EST)", "")));
            body = body.Replace("@@한글금액", am_sum);
            body = body.Replace("@@숫자금액", D.GetDecimal(dtL.Compute("SUM(AM_K_EST)", "")).ToString("###,###,##0"));

            foreach (DataRow rowL in dtL.Rows)
            {
                string tr = string.Empty;

                tr = GetLine();
                tr = tr.Replace("@@품목명", D.GetString(rowL["NM_ITEM"]));
                tr = tr.Replace("@@규격", D.GetString(rowL["STND_ITEM"]));
                tr = tr.Replace("@@단위", D.GetString(rowL["UNIT_IM"]));
                tr = tr.Replace("@@수량", D.GetDecimal(rowL["QT_EST"]).ToString("###,###,##0.####"));
                tr = tr.Replace("@@단가", D.GetDecimal(rowL["UM_EST"]).ToString("###,###,##0.####"));
                tr = tr.Replace("@@금액", D.GetDecimal(rowL["AM_K_EST"]).ToString("###,###,##0"));
                line += tr;
            }

            body = body.Replace("@@추가", line);

            return body;
        } 
       

        private string GetLine()
        {
            string tr = @"
		            <tr>
					<td style='border-left: 1px solid #000; border-right: 1px dotted #000;'>&nbsp; @@품목명 - &nbsp; @@규격 </td>
					<td align='center' style='border-right: 1px dotted #000;'>@@단위</td>
					<td align='center' style='border-right: 1px dotted #000;'>@@수량</td>
					<td align='right' style='border-right: 1px dotted #000;'>&nbsp;@@단가</td>
					<td align='right' style='border-right: 1px solid #000;'>&nbsp;@@금액</td>
				    </tr>";

            return tr;
        }
        #endregion

        #region -> 숫자값을 받아 한글/한자로 변환
        private string GetHanjaAmt(decimal nAmt)
        {
            int nCount, i;
            string sAmt, cAmt = string.Empty;

            string[] dan_hangul = { "", "", "십", "백", "천", "만", "십", "백", "천", "억", "십", "백", "천", "조", "십", "백" };
            string[] digit_hangul = { "", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구" };

            // -(마이너스)일 경우
            if (nAmt < 0)
                sAmt = nAmt.ToString().Substring(1);
            else
                sAmt = nAmt.ToString();		// Argument값을 string로 변환

            nCount = sAmt.Length;			// Argument값의 길이 Check

            i = 1;

            if (nCount < 1)
                return "0";

            if (nCount == 1)
            {
                cAmt = digit_hangul[int.Parse(sAmt)];
                return cAmt;
            }


            //if (nCount > 15 || sAmt.Substring(1, 1) == ".")
            //{
            //    if (Global.MainFrame.LoginInfo.Language == "KR")
            //        this.ShowMessage("확   인 !", "백조단위가 넘습니다.");
                    
            //    else
            //        this.ShowMessage("Check!. 100,000,000,000,000 unit went over");

            //    return "";
            //}

            // 특정 데이터 1033003000 같은 경우 억, 만 이 빠지는데 이것을 추가해 주기 위한 부분임
            //-----------------------------------------------------
            bool IsMan = false;
            bool IsUk = false;
            bool IsCho = false;

            if (nCount > 5)
            {
                if (decimal.Parse(sAmt.Substring(nCount - 5, 2)) < 10)
                    IsMan = true;
            }
            if (nCount > 9)
            {
                if (decimal.Parse(sAmt.Substring(nCount - 9, 2)) < 10)
                    IsUk = true;
            }
            if (nCount > 13)
            {
                if (decimal.Parse(sAmt.Substring(nCount - 13, 2)) < 10)
                    IsCho = true;
            }
            //-----------------------------------------------------

            // 숫자값을 한글로 변환
            for (i = 0; i < nCount; i++)
            {
                if (sAmt.Substring(i, 1) != "0")
                {
                    // 조, 억, 만 이 빠졌을때 보정
                    //-----------------------------------------------------
                    if (nCount - i == 12)
                    {
                        if (IsCho) cAmt = cAmt + dan_hangul[nCount - i + 1];
                    }

                    if (nCount - i == 8)
                    {
                        if (IsUk) cAmt = cAmt + dan_hangul[nCount - i + 1];
                    }

                    if (nCount - i == 4)
                    {
                        if (IsMan) cAmt = cAmt + dan_hangul[nCount - i + 1];
                    }
                    //-----------------------------------------------------

                    cAmt = cAmt + digit_hangul[int.Parse(sAmt.Substring(i, 1))];
                    if (nCount - i > 0)
                    {
                        cAmt = cAmt + dan_hangul[nCount - i];
                    }
                }
                else
                {
                    int nCnt = nCount - i;
                    if (nCnt == 4 || nCnt == 8 || nCnt == 12)
                    {
                        if ((cAmt.Substring(cAmt.Length - 1, 1) != "만") && (cAmt.Substring(cAmt.Length - 1, 1) != "억") && (cAmt.Substring(cAmt.Length - 1, 1) != "조"))
                        {
                            cAmt = cAmt + dan_hangul[nCount - i + 1];
                        }
                    }
                }
            }

            return cAmt;
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

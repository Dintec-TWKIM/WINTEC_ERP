using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Duzon.ERPU;
using Duzon.Common.Forms;

namespace sale
{
    class WONIK_GW
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
            List.Add("수익검토서");
            List.Add("Y");
            List.Add(2013);       //APP_FORM_KIND에 들어갈 값
            List.Add("29991231");

            switch (Global.MainFrame.ServerKeyCommon.ToUpper())
            {
                case "DZSQL":       //84번 개발서버
                    List[5] = GetWonikHtml(rowH, dtL);
                    isSuccess = 결재상신_개발서버(List.ToArray());
                    break;

                case "WONIK":      //원익
                    List[5] = GetWonikHtml(rowH, dtL);
                    isSuccess = 결재상신_업체(List.ToArray());
                    if (!isSuccess) return false;
                    string strURL = "http://gw.wonik.co.kr/kor_webroot/src/cm/tims/index.aspx?cd_company=" + MA.Login.회사코드 + "&cd_pc=" + Global.MainFrame.LoginInfo.CdPc + "&no_docu=" + System.Web.HttpUtility.UrlEncode(D.GetString(rowH["NO_EST"]), Encoding.UTF8) + "&login_id=" + MA.Login.사원번호;
                    System.Diagnostics.Process.Start("IExplore.exe", strURL);
                    break;
                default:
                    break;
            }

            return isSuccess;
        }

        private string GetWonikHtml(DataRow rowH, DataTable dtL)
        {
            string body = string.Empty;
            string line = string.Empty;

            DataTable dt_temp1 = MA.GetCode("SA_B000090");
            dt_temp1.PrimaryKey = new DataColumn[] { dt_temp1.Columns["CODE"] };
           
            DataRow dr1 =  dt_temp1.Rows.Find(new object[] { D.GetString(rowH["CD_USERDEF1"])});

            DataTable dt_temp2 = MA.GetCode("SA_B000091");
            dt_temp2.PrimaryKey = new DataColumn[] { dt_temp2.Columns["CODE"] };

            DataRow dr2 = dt_temp2.Rows.Find(new object[] { D.GetString(rowH["CD_USERDEF2"]) });
            
            DataTable dt = dtL.Clone();

            dt.Columns.Add("CUSTOMS_FEE_P", typeof(string));
            dt.Columns.Add("CUSTOMS_FEE_WON", typeof(decimal));
            dt.Columns.Add("COST_PRICE", typeof(decimal));
            dt.Columns.Add("SALES_MARGIN_WON", typeof(decimal));
            dt.Columns.Add("SALES_MARGIN_P", typeof(string));
            dt.Columns.Add("VAT", typeof(decimal));

            dt.Columns.Add("SUM_CONTRACT_PRICE", typeof(decimal));
            dt.Columns.Add("SUM_TOTAL_OCV", typeof(decimal));
            dt.Columns.Add("SUM_TOTAL_AMOUNT", typeof(decimal));
            dt.Columns.Add("SUM_SALES_MARGIN", typeof(decimal));
            dt.Columns.Add("SUM_SALES_MARGIN_RATE", typeof(string));
            dt.Columns.Add("SUM_TOTAL_COST_PRICE", typeof(decimal));

            dt.Columns["NM_USERDEF1"].DataType = System.Type.GetType("System.Decimal");
            dt.Columns["NM_USERDEF2"].DataType = System.Type.GetType("System.Decimal");

            for (int i = 0; i < dtL.Rows.Count; i++)
            {
                dt.ImportRow(dtL.Rows[i]);
            }

            foreach (DataRow dr in dt.Rows)
            {
                dr["CUSTOMS_FEE_P"] = D.GetDecimal(dr["NM_USERDEF1"]);
                dr["CUSTOMS_FEE_WON"] = (D.GetDecimal(dr["AM_K_EST"]) * D.GetDecimal(dr["NM_USERDEF1"]) / 100);

                dr["COST_PRICE"] = D.GetDecimal(dr["AM_K_EST"]) + D.GetDecimal(dr["CUSTOMS_FEE_WON"]);
                dr["SALES_MARGIN_WON"] = D.GetDecimal(dr["NM_USERDEF2"]) - (D.GetDecimal(dr["AM_K_EST"]) + D.GetDecimal(dr["CUSTOMS_FEE_WON"]));
                
                if (D.GetDecimal(dr["NM_USERDEF2"]) == 0)
                    dr["SALES_MARGIN_P"] = 0;
                else
                {
                    double sales_margin_p = Convert.ToDouble(D.GetDecimal(dr["SALES_MARGIN_WON"]) / D.GetDecimal(dr["NM_USERDEF2"])) * 10000;
                    dr["SALES_MARGIN_P"] = Math.Truncate(sales_margin_p) / 100;
                }

                dr["SUM_CONTRACT_PRICE"] = D.GetDecimal(dt.Compute("SUM(NM_USERDEF2)", "NO_EST = '" + D.GetString(dr["NO_EST"]) + "'"));
                dr["VAT"] = D.GetDecimal(dt.Compute("SUM(NM_USERDEF2)", "NO_EST = '" + D.GetString(dr["NO_EST"]) + "'")) * 10 / 100;
                dr["SUM_TOTAL_AMOUNT"] = D.GetDecimal(dr["SUM_CONTRACT_PRICE"]) + D.GetDecimal(dr["VAT"]);
                dr["SUM_TOTAL_OCV"] = D.GetDecimal(rowH["NUM_USERDEF1"]) + D.GetDecimal(rowH["NUM_USERDEF2"]) + D.GetDecimal(rowH["NUM_USERDEF3"]) + D.GetDecimal(rowH["NUM_USERDEF4"]) + D.GetDecimal(rowH["NUM_USERDEF5"]) + D.GetDecimal(rowH["NUM_USERDEF6"]) + D.GetDecimal(rowH["TEXT_USERDEF3"]);

            }

            dt.AcceptChanges();

            foreach (DataRow dr_row in dt.Rows)
            {
                dr_row["CUSTOMS_FEE_P"] = D.GetString(dr_row["CUSTOMS_FEE_P"]) + "%";
                dr_row["SALES_MARGIN_P"] = D.GetString(dr_row["SALES_MARGIN_P"]) + "%";
                dr_row["SUM_TOTAL_COST_PRICE"] = D.GetDecimal(dt.Compute("SUM(COST_PRICE)", "NO_EST = '" + D.GetString(dr_row["NO_EST"]) + "'")) + D.GetDecimal(dr_row["SUM_TOTAL_OCV"]);
                dr_row["SUM_SALES_MARGIN"] = D.GetDecimal(dr_row["SUM_CONTRACT_PRICE"]) - D.GetDecimal(dr_row["SUM_TOTAL_COST_PRICE"]);

                if (D.GetDecimal(dr_row["SUM_CONTRACT_PRICE"]) == 0)
                    dr_row["SUM_SALES_MARGIN_RATE"] = 0;
                else
                {
                    double SUM_SALES_MARGIN_RATE = Convert.ToDouble(D.GetDecimal(dr_row["SUM_SALES_MARGIN"]) / D.GetDecimal(dr_row["SUM_CONTRACT_PRICE"])) * 10000;
                    dr_row["SUM_SALES_MARGIN_RATE"] = (Math.Truncate(SUM_SALES_MARGIN_RATE) / 100).ToString() + "%";
                }
            }

            DataTable dt_Gw_Info = DBHelper.GetDataTable("UP_SA_Z_WONIK_EST_GW_INFO_S", new object[] { MA.Login.회사코드, D.GetString(rowH["CD_PARTNER"]), D.GetString(rowH["DT_EST"]) });

            body = @"
            <head>
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
		
		<tr height='5'>
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
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>OA No.</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@견적번호</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>Date</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@견적일자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>견적명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@견적명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>Customer</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@거래처명</td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>부서명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@부서명</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>담당자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@담당자</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>Dealer</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@Dealer</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>Warranty</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@WP</td>
		</tr>
	     <tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>여신한도</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@여신한도</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>총채권</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@총채권</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>60일이상채권</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@60일이상채권</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>납기예정일</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@납기예정일</td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>매출액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@CP</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>부가세</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@VAT</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>외상매출액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@외상매출액</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>수금예정일</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수금예정일</td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>Total Cost Price</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@TCP</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'><b>예상이익</b></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SM</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>예상이익율(%)</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SalesMRate</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>수금조건</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수금조건</td>
		</tr>
		
		<tr height='5'>
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
		<colgroup width='9%' align='center'></colgroup>
		<colgroup width='4%' align='center'></colgroup>
		<colgroup width='6%' align='center'></colgroup>
		<colgroup width='6%' align='center'></colgroup>
		<colgroup width='6%' align='center'></colgroup>
		<colgroup width='6%' align='center'></colgroup>
		<colgroup width='6%' align='center'></colgroup>
		<colgroup width='6%' align='center'></colgroup>
		<colgroup width='6%' align='center'></colgroup>
		<colgroup width='6%' align='center'></colgroup>
		<colgroup width='6%' align='center'></colgroup>
		<colgroup width='4%' align='center'></colgroup>
		<colgroup width='4%' align='center'></colgroup>
		<colgroup width='4%' align='center'></colgroup>
		<colgroup width='17%' align='center'></colgroup>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>FX</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@환율</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>환종</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@환종</td>
			<td style='border-style: solid; border-width: 0 0 1 1; border-color: #000000' colspan='4'></td>
			<td style='border-style: solid; border-width: 0 0 1 0; border-color: #000000' colspan='8'></td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' rowspan='2' bgcolor='#CCCCCC'>ITEM<br>CODE</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' rowspan='2' bgcolor='#CCCCCC'>DESCRIPTION</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' rowspan='2' bgcolor='#CCCCCC'>Q’ty</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' rowspan='2' bgcolor='#CCCCCC'>Unit Price</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2' bgcolor='#CCCCCC'>Invoice Value</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2' bgcolor='#CCCCCC'>Customs fee</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' rowspan='2' bgcolor='#CCCCCC'>Cost price<br>(원)</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' rowspan='2' bgcolor='#CCCCCC'>Contract<br>Price(원)</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2' bgcolor='#CCCCCC'>Sales margin</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' rowspan='2' bgcolor='#CCCCCC'>현재<br>고량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' rowspan='2' bgcolor='#CCCCCC'>기발<br>주량</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' rowspan='2' bgcolor='#CCCCCC'>합계</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' rowspan='2' bgcolor='#CCCCCC'>비고</td>
		</tr>
		
		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>USD</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>원</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>%</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>원</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>원</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' bgcolor='#CCCCCC'>%</td>
		</tr>
	
		@@추가

		<tr height='30'>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000' colspan='2' bgcolor='#CCCCCC'>합계</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@수량합계</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@금액합</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@원화금액합</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@CFW합</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@COST_P합</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@CONTRACT_P합</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SalesMW합</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@SUMSalesMWP</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@현재고량합</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@기발주량합</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'>@@합계합</td>
			<td style='border-style: solid; border-width: 1 1 1 1; border-color: #000000'></td>
		</tr>
		
		<tr height='5'>
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
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
			<td style='border-style: solid; border-width: 0 0 0 0; border-color: #000000'></td>
		</tr>
	</body>

	<table width='945' border='1' bordercolor='black' cellpadding='0' cellspacing='0' style='font-size: 10pt; border-collapse: collapse; border: 0;'>
		<colgroup width='2%' align='center'></colgroup>
		<colgroup width='15%' align='center'></colgroup>
		<colgroup width='15%' align='center'></colgroup>
		<colgroup width='10%' align='center'></colgroup>
		<colgroup width='15%' align='center'></colgroup>
		<colgroup width='10%' align='center'></colgroup>
		<colgroup width='15%' align='center'></colgroup>
		
		<tr height='30'>
		<td style='border: 1 solid #000000' bgcolor='#CCCCCC'>비고</td>
		<td style='border: 1 solid #000000' colspan='7'>
		<p align='left'>　</td>
	</tr>


		</table>	
</center>";
       
            
            DataTable dt_code = MA.GetCode("MA_B000005");
            dt_code.PrimaryKey = new DataColumn[] { dt_code.Columns["CODE"] };
            DataRow rowCd_Exch = dt_code.Rows.Find(new object[] { D.GetString(rowH["CD_EXCH"]) });

            body = body.Replace("@@견적번호", D.GetString(rowH["NO_EST"]));
            body = body.Replace("@@견적명", D.GetString(rowH["NO_EST_NM"]));
            body = body.Replace("@@거래처명", D.GetString(rowH["LN_PARTNER"]));
            body = body.Replace("@@견적일자", D.GetString(rowH["DT_EST"]).Substring(0, 4) + "년" + D.GetString(rowH["DT_EST"]).Substring(4, 2) + "월" + D.GetString(rowH["DT_EST"]).Substring(6, 2) + "일");
            body = body.Replace("@@부서명", D.GetString(rowH["NM_SALEGRP"]));
            body = body.Replace("@@담당자", D.GetString(rowH["NM_KOR"]));
            body = body.Replace("@@Dealer", D.GetString(rowH["TEXT_USERDEF1"]));
            body = body.Replace("@@WP", D.GetString(rowH["CD_USERDEF2"]) == string.Empty ? string.Empty : D.GetString(dr2["NAME"])); // Warranty Period
            body = body.Replace("@@CP", D.GetDecimal(dt.Rows[0]["SUM_CONTRACT_PRICE"]).ToString("##,##0"));                          // Contract Price
            body = body.Replace("@@VAT", D.GetDecimal(dt.Rows[0]["VAT"]).ToString("##,##0.##"));                                     // VAT
            //body = body.Replace("@@TA", D.GetDecimal(dt.Rows[0]["SUM_TOTAL_AMOUNT"]).ToString("##,##0.##"));                         // Total Amount
    
            body = body.Replace("@@납기예정일", D.GetString(rowH["DT_DUEDATE"]) == string.Empty ? string.Empty : D.GetString(rowH["DT_DUEDATE"]).Substring(0, 4) + "년" + D.GetString(rowH["DT_DUEDATE"]).Substring(4, 2) + "월" + D.GetString(rowH["DT_DUEDATE"]).Substring(6, 2) + "일");
            body = body.Replace("@@TCP", D.GetDecimal(dt.Rows[0]["SUM_TOTAL_COST_PRICE"]).ToString("##,##0"));                      // Total Cost Price
            body = body.Replace("@@SM", D.GetDecimal(dt.Rows[0]["SUM_SALES_MARGIN"]).ToString("##,##0"));                           // Sales Margin
            body = body.Replace("@@SalesMRate", D.GetString(dt.Rows[0]["SUM_SALES_MARGIN_RATE"]));
            body = body.Replace("@@수금조건", D.GetString(rowH["CD_USERDEF1"]) == string.Empty ? string.Empty : D.GetString(dr1["NAME"]));
            body = body.Replace("@@환율", D.GetDecimal(rowH["RT_EXCH"]).ToString("##,##0.##"));
            body = body.Replace("@@환종", rowCd_Exch == null ? string.Empty : D.GetString(rowCd_Exch["NAME"]));

            //body = body.Replace("@@DEALERCOMMISSION", D.GetDecimal(rowH["NUM_USERDEF1"]).ToString("##,##0.##"));           // DEALER COMMISSION
            //body = body.Replace("@@WARRANTYCOST", D.GetDecimal(rowH["NUM_USERDEF2"]).ToString("##,##0.##"));               // WARRANTY COST
            //body = body.Replace("@@TRAINING", D.GetDecimal(rowH["NUM_USERDEF3"]).ToString("##,##0.##"));                   // TRAINING
            //body = body.Replace("@@운반비", D.GetDecimal(rowH["NUM_USERDEF4"]).ToString("##,##0.##"));
            //body = body.Replace("@@장비설치비", D.GetDecimal(rowH["NUM_USERDEF5"]).ToString("##,##0.##"));
            //body = body.Replace("@@OPENINGCEREMONY", D.GetDecimal(rowH["NUM_USERDEF6"]).ToString("##,##0.##"));
            //body = body.Replace("@@기타", D.GetDecimal(rowH["TEXT_USERDEF3"]).ToString("##,##0"));
            //body = body.Replace("@@TOTALOVC", D.GetDecimal(dt.Rows[0]["SUM_TOTAL_OCV"]).ToString("##,##0.##"));            // TOTAL OVC (예상부대비용합계)
            body = body.Replace("@@수량합계", D.GetDecimal(dt.Compute("SUM(QT_EST)", "")).ToString("##,##0.###"));
            body = body.Replace("@@금액합", D.GetDecimal(dt.Compute("SUM(AM_EST)", "")).ToString("##,##0.##"));
            body = body.Replace("@@원화금액합", D.GetDecimal(dt.Compute("SUM(AM_K_EST)", "")).ToString("##,##0"));
            body = body.Replace("@@CFW합", D.GetDecimal(dt.Compute("SUM(NM_USERDEF1)", "")).ToString("##,##0"));           // CUSTOMS_FEE_WON
            body = body.Replace("@@CONTRACT_P합", D.GetDecimal(dt.Compute("SUM(NM_USERDEF2)", "")).ToString("##,##0"));
            body = body.Replace("@@현재고량합", D.GetDecimal(dt.Compute("SUM(QT_INV)", "")).ToString("##,##0.##"));
            body = body.Replace("@@기발주량합", D.GetDecimal(dt.Compute("SUM(QT_PO)", "")).ToString("##,##0.##"));
            body = body.Replace("@@합계합", (D.GetDecimal(dt.Compute("SUM(QT_INV)", "")) + D.GetDecimal(dt.Compute("SUM(QT_PO)", ""))).ToString("##,##0.##"));
            body = body.Replace("@@SalesMW합", D.GetDecimal(dt.Rows[0]["SUM_SALES_MARGIN"]).ToString("###,###,##0"));       // Sales Margin WON합
            body = body.Replace("@@TOTALCOST_P", D.GetDecimal(dt.Rows[0]["SUM_TOTAL_COST_PRICE"]).ToString("##,##0"));      // Total Cost Price
            body = body.Replace("@@COST_P합", D.GetDecimal(dt.Compute("SUM(COST_PRICE)", "")).ToString("##,##0"));          // COST_PRICE합
            body = body.Replace("@@SUMSalesMWP", D.GetString(dt.Rows[0]["SUM_SALES_MARGIN_RATE"]));

            body = body.Replace("@@여신한도", D.GetDecimal(dt_Gw_Info.Rows[0]["TOT_CREDIT"]).ToString("###,###,###,##0"));
            body = body.Replace("@@총채권", D.GetDecimal(dt_Gw_Info.Rows[0]["AM_RAMAIN"]).ToString("###,###,###,##0"));
            body = body.Replace("@@60일이상채권", D.GetDecimal(dt_Gw_Info.Rows[0]["IN_DAY_60_UP"]).ToString("###,###,###,##0"));
            body = body.Replace("@@외상매출액", D.GetDecimal(dtL.Compute("SUM(AM_TOT)","")).ToString("###,###,###,##0"));
            body = body.Replace("@@수금예정일", D.GetString(rowH["DT_DUEDATE"]) == string.Empty ? string.Empty : D.GetString(rowH["DT_DUEDATE"]).Substring(0, 4) + "년" + D.GetString(rowH["DT_DUEDATE"]).Substring(4, 2) + "월" + D.GetString(rowH["DT_DUEDATE"]).Substring(6, 2) + "일");

            foreach (DataRow rowL in dt.Rows)
            {
                string tr = string.Empty;

                tr = GetLine();
                tr = tr.Replace("@@품목코드", D.GetString(rowL["CD_ITEM"]));
                tr = tr.Replace("@@규격", D.GetString(rowL["STND_ITEM"]));
                tr = tr.Replace("@@수량", D.GetDecimal(rowL["QT_EST"]).ToString("##,##0.###"));
                tr = tr.Replace("@@단가", D.GetDecimal(rowL["UM_EST"]).ToString("##,##0.##"));
                tr = tr.Replace("@@금액", D.GetDecimal(rowL["AM_EST"]).ToString("##,##0.##"));
                tr = tr.Replace("@@원화금액", D.GetDecimal(rowL["AM_K_EST"]).ToString("##,##0"));
                tr = tr.Replace("@@CFP", D.GetString(rowL["CUSTOMS_FEE_P"]));                             // CUSTOMS_FEE_P
                tr = tr.Replace("@@CFW", D.GetDecimal(rowL["CUSTOMS_FEE_WON"]).ToString("##,##0"));       // CUSTOMS_FEE_WON
                tr = tr.Replace("@@COST_P", D.GetDecimal(rowL["COST_PRICE"]).ToString("##,##0"));
                tr = tr.Replace("@@CONTRACT_P", D.GetDecimal(rowL["NM_USERDEF2"]).ToString("##,##0"));
                tr = tr.Replace("@@SMW", D.GetDecimal(rowL["SALES_MARGIN_WON"]).ToString("##,##0"));      // Sales Margin WON
                tr = tr.Replace("@@SMP", D.GetString(rowL["SALES_MARGIN_P"]));                            // Sales Margin_P
                tr = tr.Replace("@@현재고량", D.GetDecimal(rowL["QT_INV"]).ToString("##,##0.##"));
                tr = tr.Replace("@@기발주량", D.GetDecimal(rowL["QT_PO"]).ToString("##,##0.##"));
                tr = tr.Replace("@@합계", (D.GetDecimal(rowL["QT_INV"]) + D.GetDecimal(rowL["QT_PO"])).ToString("##,##0.##"));
                tr = tr.Replace("@@비고", string.Empty);

                line += tr;
            }

            body = body.Replace("@@추가", line);

            return body;
        }
        
        private string GetLine()
        {
            string tr = @"
		          <tr>
					<td height='20'>&nbsp;@@품목코드</td>
					<td>&nbsp;@@규격</td>
					<td>&nbsp;@@수량</td>
					<td>&nbsp;@@단가</td>
					<td>&nbsp;@@금액</td>
					<td>&nbsp;@@원화금액</td>
					<td>&nbsp;@@CFP</td>
					<td>&nbsp;@@CFW</td>
					<td>&nbsp;@@COST_P</td>
					<td>&nbsp;@@CONTRACT_P</td>
					<td>&nbsp;@@SMW</td>
					<td>&nbsp;@@SMP</td>
					<td>&nbsp;@@현재고량</td>
					<td>&nbsp;@@기발주량</td>
					<td>&nbsp;@@합계</td>
					<td>&nbsp;@@비고</td>
				</tr>";

            return tr;
        }

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
    }
}

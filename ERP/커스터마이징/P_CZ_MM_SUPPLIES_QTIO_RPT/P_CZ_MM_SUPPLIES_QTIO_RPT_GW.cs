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
using DX;

namespace cz
{
	internal class P_CZ_MM_SUPPLIES_QTIO_RPT_GW
	{
		internal bool 전자결재(DataRow header, DataRow line, DataTable rows)
		{
			bool isSuccess;
			string strURL, key;

			key = Global.MainFrame.LoginInfo.CompanyCode + "-" + D.GetString(line["NO_IO"]);

			isSuccess = 결재상신(new object[] { GroupWare.GetERP_CD_COMPANY(),
											   GroupWare.GetERP_CD_PC(),
											   key,
											   Global.MainFrame.LoginInfo.EmployeeNo,
											   Global.MainFrame.GetStringToday,
											   this.GetHtml(header, line, rows),
											   Global.MainFrame.DD("소모품 입/출고 신청서"),
											   "Y",
											   2004 });

			if (!isSuccess) return false;

			strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
					 + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
					 + "&cd_pc=" + GroupWare.GetERP_CD_PC()
					 + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
					 + "&login_id=" + MA.Login.사원번호;
			Process.Start("msedge.exe", strURL);


			return isSuccess;
		}

		private bool 결재상신(object[] obj)
		{
			return DBHelper.ExecuteNonQuery("SP_CZ_FI_GWDOCU", obj);
		}

		private string GetHtml(DataRow header, DataRow line, DataTable rows)
		{
			string path, body;

			body = string.Empty;
			path = Application.StartupPath + "\\download\\gw\\HT_P_CZ_MM_SUPPLIES_BODY.html";

			using (StreamReader reader = new StreamReader(path, Encoding.Default))
			{
				body = reader.ReadToEnd();
			}

			body = body.Replace("@@NM_KOR", D.GetString(line["NM_KOR"]));
			body = body.Replace("@@FG_PS", line["FG_PS"].ToString() == "1" ? "입고" : "출고");
			body = body.Replace("@@DT_IO", line["DT_IO"].문자("yyyy-MM-dd"));

			body = body.Replace("@@NM_ITEM", header["NM_ITEM"].ToString());
			body = body.Replace("@@UNIT", header["UNIT"].ToString());

			body = body.Replace("@@NM_MAKER", header["NM_MAKER"].ToString());
			body = body.Replace("@@NO_MODEL", header["NO_MODEL"].ToString());
			body = body.Replace("@@LN_PARTNER", line["LN_PARTNER"].ToString());

			body = body.Replace("@@QT_IO", line["FG_PS"].ToString() == "1" ? line["QT_GR"].문자("#,##0") : line["QT_GI"].문자("#,##0"));
			body = body.Replace("@@UM", line["FG_PS"].ToString() == "1" ? line["UM_GR"].문자("#,##0") : "");
			body = body.Replace("@@AM", line["FG_PS"].ToString() == "1" ? line["AM_GR"].문자("#,##0") : line["AM_GI"].문자("#,##0"));

			body = body.Replace("@@DC_RMK", line["DC_RMK"].ToString().Replace("\n", "</br>"));

			body = body.Replace("@@TXT_SUPPLIES_IO_LIST", this.입출고현황(rows));
			return body;
		}

		private string 입출고현황(DataTable rows)
		{
			string html, html1 = string.Empty;

			foreach (DataRow dr in rows.Rows)
			{
				html1 += @"		<tr style='height:30px'>
			<td style='border:solid 1px black; text-align:center'>" + dr["DT_IO"].문자("yyyy-MM-dd") + @"</td>
			<td style='border:solid 1px black; text-align:center'>" + dr["QT_GR"].문자("#,##0") + @"</td>
			<td style='border:solid 1px black; text-align:center'>" + dr["QT_GI"].문자("#,##0") + @"</td>
			<td style='border:solid 1px black; text-align:center'>" + dr["QT_INV"].문자("#,##0") + @"</td>
			<td style='border:solid 1px black; text-align:center'>" + dr["NM_KOR"].ToString() + @"</td>
			<td style='border:solid 1px black; text-align:left'>" + dr["DC_RMK"].ToString().Replace("\n", "</br>") + @"</td>
		</tr>";
			}

			html = @"<div style='text-align:left; font-weight: bold;'>2. 입/출고현황</div>
</br>
<table style='width:100%; border:2px solid black; margin-bottom: 20px; font-size: 9pt; font-family: 굴림; border-collapse:collapse; border-spacing:0;'>
	<colgroup width='12%' align='center'></colgroup>
	<colgroup width='12%' align='center'></colgroup>
	<colgroup width='12%' align='center'></colgroup>
	<colgroup width='12%' align='center'></colgroup>
	<colgroup width='12%' align='center'></colgroup>
	<colgroup width='40%' align='center'></colgroup>
	<tbody>
		<tr style='height:30px'>
			<th style='border:solid 1px black; text-align:center; background-color:Silver'>입/출고일자</th>
			<th style='border:solid 1px black; text-align:center; background-color:Silver'>입고수량</th>
			<th style='border:solid 1px black; text-align:center; background-color:Silver'>출고수량</th>
			<th style='border:solid 1px black; text-align:center; background-color:Silver'>현재고</th>
			<th style='border:solid 1px black; text-align:center; background-color:Silver'>담당자</th>
			<th style='border:solid 1px black; text-align:center; background-color:Silver'>비고</th>
		</tr>";

			html += html1;

			html += @"	</tbody>
</table>";
			return html;
		}
	}
}
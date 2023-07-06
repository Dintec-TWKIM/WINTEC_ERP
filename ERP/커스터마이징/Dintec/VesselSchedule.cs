using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Text;

namespace Dintec
{
    public static class VesselSchedule
    {
        public static DataRow[] 입출항정보조회(string 회사코드, string imo번호)
	    {
			HttpWebRequest request;
			HttpWebResponse response;
			TextReader reader;
			DataTable result, result1;
			DataRow[] dataRowArray;
			DataRow newRow;
			DateTime date;
			int time;
			string text, tmpText;
			string address, vesselDetail, query;

			if (string.IsNullOrEmpty(imo번호))
				return null;

			switch(회사코드)
            {
				case "K100":
				case "K200":
					time = 9;
					break;
				case "S100":
					time = 8;
					break;
				default:
					return null;
            }

			result = new DataTable();
			result.Columns.Add("COUNTRY");
			result.Columns.Add("PORT");
			result.Columns.Add("ARRIVAL");
			result.Columns.Add("DEPARTURE");
			result.Columns.Add("DC_SCHEDULE");

			#region 해외 입출항정보
			//request = (HttpWebRequest)WebRequest.Create("https://www.myshiptracking.com/vessels?name=" + imo번호);
			//response = (HttpWebResponse)request.GetResponse();
			//reader = (TextReader)new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));

			//address = reader.ReadToEnd();

			//if (address.Contains("Vessel Details"))
   //         {
			//	address = address.Split(new string[] { "Vessel Details" }, StringSplitOptions.None)[0].Split(new string[] { "Show on Live Map" }, StringSplitOptions.None)[1].Split(new string[] { "href=\"" }, StringSplitOptions.None)[1].Split('"')[0];
			//	response.Close();
			//	reader.Close();

			//	if (!string.IsNullOrEmpty(address))
			//	{
			//		request = (HttpWebRequest)WebRequest.Create("https://www.myshiptracking.com/vessels" + address);
			//		response = (HttpWebResponse)request.GetResponse();
			//		reader = (TextReader)new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));

			//		vesselDetail = reader.ReadToEnd();

			//		text = vesselDetail.Split(new string[] { "Destination" }, StringSplitOptions.None)[2].Split(new string[] { "Activity" }, StringSplitOptions.None)[1];
			//		response.Close();
			//		reader.Close();

			//		newRow = result.NewRow();

			//		if (text.Contains("title="))
			//		{
			//			newRow["COUNTRY"] = text.Split(new string[] { "title=" }, StringSplitOptions.None)[1].Split(new string[] { "/>" }, StringSplitOptions.None)[0].Replace("\"", "");
			//			newRow["PORT"] = text.Split(new string[] { "title=" }, StringSplitOptions.None)[1].Split(new string[] { "</a>" }, StringSplitOptions.None)[0].Split('>')[2];

			//			if (text.Split(new string[] { "<div class=\"col\">" }, StringSplitOptions.None).Length > 4)
			//			{
			//				tmpText = text.Split(new string[] { "<div class=\"col\">" }, StringSplitOptions.None)[4].Split(new string[] { "</div>" }, StringSplitOptions.None)[0].Replace("<b>", " ").Replace("</b>", "").Replace("-", "").Replace(":", "").Replace(" ", "") + "00";

			//				if (DateTime.TryParseExact(tmpText, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
			//				{
			//					date = date.AddHours(time);

			//					newRow["ARRIVAL"] = date.ToString("yyyyMMddHHmmss");
			//					newRow["DC_SCHEDULE"] = newRow["COUNTRY"].ToString() + "-" + newRow["PORT"].ToString() + " " + String.Format("{0:yyyy-MM-dd HH:mm:ss}", date) + " →";
			//				}
			//			}

			//			if (text.Split(new string[] { "<div class=\"col\">" }, StringSplitOptions.None).Length > 8)
			//			{
			//				tmpText = text.Split(new string[] { "<div class=\"col\">" }, StringSplitOptions.None)[8].Split(new string[] { "</div>" }, StringSplitOptions.None)[0].Replace("<b>", " ").Replace("</b>", "").Replace("-", "").Replace(":", "").Replace(" ", "") + "00";

			//				if (DateTime.TryParseExact(tmpText, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
			//				{
			//					date = date.AddHours(time);

			//					newRow["ARRIVAL"] = date.ToString("yyyyMMddHHmmss");
			//					newRow["DC_SCHEDULE"] = newRow["COUNTRY"].ToString() + "-" + newRow["PORT"].ToString() + " " + String.Format("{0:yyyy-MM-dd HH:mm:ss}", date) + " →";
			//				}
			//			}
			//		}
			//		else
			//		{
			//			newRow["PORT"] = text.Split(new string[] { "<tbody>" }, StringSplitOptions.None)[1].Split(new string[] { "</tbody>" }, StringSplitOptions.None)[0].Split(new string[] { "<td>" }, StringSplitOptions.None)[1].Split(new string[] { "</td>" }, StringSplitOptions.None)[0];
			//			tmpText = text.Split(new string[] { "<tbody>" }, StringSplitOptions.None)[1].Split(new string[] { "</tbody>" }, StringSplitOptions.None)[0].Split(new string[] { "<td>" }, StringSplitOptions.None)[2].Split(new string[] { "</td>" }, StringSplitOptions.None)[0].Replace("-", "").Replace(" ", "").Replace(":", "").Replace("UTC", "") + "00";

			//			if (DateTime.TryParseExact(tmpText, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
			//			{
			//				date = date.AddHours(time);

			//				newRow["ARRIVAL"] = date.ToString("yyyyMMddHHmmss");
			//				newRow["DC_SCHEDULE"] = newRow["COUNTRY"].ToString() + "-" + newRow["PORT"].ToString() + " " + String.Format("{0:yyyy-MM-dd HH:mm:ss}", date) + " →";
			//			}
			//		}

			//		result.Rows.Add(newRow);
			//	}
			//}
			#endregion

			#region 국내 입출항정보
			if (회사코드 == "K100" || 
				회사코드 == "K200")
            {
				query = @"SELECT 'Korea' AS COUNTRY,
						  	     VE.NM_PRTAG AS PORT,
						  	     VE.DTS_ETRYPT AS ARRIVAL,
						  	     VE.DTS_TKOFF AS DEPARTURE,
								 VE.NM_PRTAG + ' ' + ISNULL(CONVERT(NVARCHAR(19), CONVERT(DATETIME, LEFT(VE.DTS_ETRYPT, 8)), 120), '') + ' → ' + ISNULL(CONVERT(NVARCHAR(19), CONVERT(DATETIME, LEFT(VE.DTS_TKOFF, 8)), 120), '') AS DC_SCHEDULE
						  FROM CZ_MA_HULL MH WITH(NOLOCK)
						  JOIN CZ_SA_VSSL_ETRYNDH VE WITH(NOLOCK) ON VE.CALL_SIGN = MH.CALL_SIGN
						  WHERE MH.NO_IMO = '" + imo번호 + "'";

				result1 = DBMgr.GetDataTable(query);

				result.Merge(result1);
			}
			#endregion

			dataRowArray = result.Select("ARRIVAL >= '" + DateTime.Now.ToString("yyyyMMdd") + "'", "ARRIVAL ASC");

			return dataRowArray;
		}
    }
}

using SelectPdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Dintec;
using System.Text.RegularExpressions;
using GemBox.Spreadsheet;

namespace DX
{
	public class HTML
	{

		



















		public static string GetHtmlDocument(string head, string body)
		{
			string html = @"
<html>
<head>
  <meta http-equiv='X-UA-Compatible' content='IE=edge' />
  <link rel='stylesheet' type='text/css' href='http://tracking.dintec.co.kr/css/common.css' />
  <style type='text/css'>
    body        { background:none; }
    div, th, td { font-family:맑은 고딕; }
  </style>" + head + @"
</head>
<body>
<form>" + body + @"
</form>
</body>
</html>";

			return html;
		}

		public static void ConvertToImage(string fileName, string html)
		{
			GlobalProperties.LicenseKey = "JQ4UBRcQFAUWFBYcBRQdCxUFFhQLFBcLHBwcHA==";
			HtmlToImage imgConverter = new HtmlToImage();
			imgConverter.WebPageWidth = 0;
			//imgConverter.WebPageHeight = 500;

			Image image = imgConverter.ConvertHtmlString(html);
			image.Save(fileName, ImageFormat.Jpeg);
		}

		/// <summary>
		/// 문자열을 HTML 인코딩된 문자열로 변환
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static string 인코딩(object value)
		{
			return WebUtility.HtmlEncode(value.ToString());
		}

		/// <summary>
		/// HTML을 파일로 저장
		/// </summary>
		/// <param name="Html"></param>
		/// <param name="파일이름"></param>
		/// <returns></returns>
		public static string 저장(string Html, string 파일이름)
		{
			// 경로 없이 파일이름만 있는 경우 임시 경로를 붙여줌
			if (파일이름.IndexOf(@"\") < 0)
				파일이름 = FILE.경로_임시() + 파일이름;

			// 확장자 가져오기
			string 확장자 = FILE.확장자_점제외(파일이름).ToLower();

			// ********** 저장 (확장자 유형에 따라)
			if (확장자.In("htm", "html"))
			{
				File.WriteAllText(파일이름, Html);
			}
			else if (확장자.In("pdf"))
			{
				GlobalProperties.LicenseKey = "JQ4UBRcQFAUWFBYcBRQdCxUFFhQLFBcLHBwcHA==";
				HtmlToPdf converter = new HtmlToPdf();
				converter.Options.EmbedFonts = true;

				PdfDocument doc = converter.ConvertHtmlString(Html);
				doc.Save(파일이름);
				doc.Close();
			}
			else if (확장자.In("xls", "xlsx"))
			{
				var stream = new MemoryStream();
				var writer = new StreamWriter(stream);
				writer.Write(Html);
				writer.Flush();
				stream.Position = 0;

				SpreadsheetInfo.SetLicense("EAAN-UCCU-1F8C-X668");
				ExcelFile.Load(stream, LoadOptions.HtmlDefault).Save(파일이름);
				
			}

			return 파일이름;
		}

		/// <summary>
		/// 
		/// </summary>		
		public static string 만들기(string head, string body)
		{
			string html = @"
<html>
<head>
  <meta http-equiv='X-UA-Compatible' content='IE=edge' />
  <link rel='stylesheet' type='text/css' href='http://tracking.dintec.co.kr/css/common.css' />
  <style type='text/css'>
    body        { background:none; }
    div, th, td { font-family:맑은 고딕; }
  </style>" + head + @"
</head>
<body>
<form>" + body + @"
</form>
</body>
</html>";

			return html;
		}
	}
}

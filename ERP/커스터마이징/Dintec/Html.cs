using SelectPdf;
using System.IO;
using System.Net;

namespace Dintec
{
	public class Html
	{
		public static string MakeHtml(string head, string body)
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

		public static string Encode(object o)
		{
			return WebUtility.HtmlEncode(o.ToString());
		}

		public static string Decode(object o)
		{
			return WebUtility.HtmlDecode(o.ToString());
		}

		public static string MakeFile(string fileName, string html)
		{
			// 경로 없이 파일이름만 있는 경우 임시 경로를 붙여줌
			if (fileName.IndexOf(@"\") < 0)
				fileName = FileMgr.GetTempPath() + fileName;

			// 변환
			File.WriteAllText(fileName, html);

			return fileName;
		}

		public static void ConvertPdf(string fileName, string html)
		{
			GlobalProperties.LicenseKey = "JQ4UBRcQFAUWFBYcBRQdCxUFFhQLFBcLHBwcHA==";
			HtmlToPdf converter = new HtmlToPdf();
			converter.Options.EmbedFonts = true;

			PdfDocument doc = converter.ConvertHtmlString(html);
			doc.Save(fileName);
			doc.Close();
		}

		public static void ConvertPdfWithPage(string fileName, string html)
		{
			// 경로 없이 파일이름만 있는 경우 임시 경로를 붙여줌
			if (fileName.IndexOf(@"\") < 0)
				fileName = FileMgr.GetTempPath() + fileName;

			// 페이지 세팅
			GlobalProperties.LicenseKey = "JQ4UBRcQFAUWFBYcBRQdCxUFFhQLFBcLHBwcHA==";
			HtmlToPdf converter = new HtmlToPdf();
			converter.Options.EmbedFonts = true;
			converter.Options.PdfPageSize = PdfPageSize.A4;
			converter.Options.MarginTop = 30;
			converter.Options.MarginBottom = 10;
			converter.Options.DisplayFooter = true;

			// 페이지 넘버링
			PdfTextSection text = new PdfTextSection(0, 10, "PAGE : {page_number} / {total_pages}", new System.Drawing.Font("Arial", 8));
			text.HorizontalAlign = PdfTextHorizontalAlign.Center;
			converter.Footer.Add(text);

			// 변환
			PdfDocument doc = converter.ConvertHtmlString(html);
			doc.Save(fileName);
			doc.Close();
		}
	}
}

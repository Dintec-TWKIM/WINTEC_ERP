using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dintec
{
	public class NOITCE
	{
		


		WebBrowser Web;

		public NOITCE(WebBrowser webBrowser)
		{
			Web = webBrowser;
			Web.Navigate("about:blank");
			Web.Document.OpenNew(false);
			
			string a = @"
<html>
	<head>
		<style type='text/css'>
			html, body, div, span, table, thead, tbody, tfoot, tr, th, td, img { margin:0; padding:0; border:0; outline:0; line-height:1; font-family:맑은 고딕; font-size:10pt; }
			body { background-color:#f6f7f8; }
		</style>
	</head>
	<body>
test
	</body>
</html>
";

			Web.Document.Write(a);
			//Web.Refresh();
		}

		public void Add(string notice)
		{
			string a = Web.DocumentText;


			Web.Navigate("about:blank");
			Web.Document.OpenNew(false);

			
			int index = a.IndexOf("</body>");
			a = a.Insert(index, notice + "\r\n");


			Web.Document.Write(a);
			Web.Refresh();


			//html += "\n" + noitce;
		}

		public void Show()
		{

		}
		

		public string GetHtml()
		{
			string html = @"
<html>
	<head>
		<style type='text/css'>
			html, body, div, span, table, thead, tbody, tfoot, tr, th, td, img { margin:0; padding:0; border:0; outline:0; line-height:1; font-family:맑은 고딕; font-size:10pt; }
			body { background-color:#f6f7f8; }
		</style>
	</head>
	<body>
		<span style='color:blue; font-weight:bold;'>테크로스</span> 커미션 적용 선사 : {0}%
	</body>
</html>";


			return "";
		}
	}
}

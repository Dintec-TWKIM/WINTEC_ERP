using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DX
{
	public static class EXT_WEB
	{
		public static void SetDefault(this WebBrowser o)
		{
			// 기존거 무시하고 새로 작성
			string html = @"
<!DOCTYPE html>

<html>
	<head>
		<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>
		<style type='text/css'>
			html, body, div, span, table, thead, tbody, tfoot, tr, th, td, img { margin:0; padding:0; border:0; outline:0; line-height:1; font-family:맑은 고딕; font-size:10pt; }			
			table { border-collapse:collapse; border-spacing:0; table-layout:fixed; }

			.dx-viewbox		{ border:1px solid black; }
			.dx-viewbox tr > * { border-left:1px solid #dedede; border-right:1px solid #dedede; border-bottom:1px solid #dedede; }
			.dx-viewbox th  { font-weight:normal; background-color:#f0f0f0; }
			.dx-viewbox td  { line-height:15px; padding:0px 7px; text-overflow:ellipsis; white-space:nowrap; overflow:hidden; }

			/* 뷰박스2는 세로로만 제목 내용 반복되는 형태 */
			.dx-viewbox2		{ width:100%; }
			.dx-viewbox2 tr > *	{ text-align:left; padding:10px; border-top:1px solid #646464; border-bottom:1px solid #646464; }
			.dx-viewbox2 th		{ background-color:#f0f0f0; }
			.dx-viewbox2 td		{ line-height:17px; padding-top:8px; padding-bottom:8px; text-overflow:ellipsis; overflow:hidden; }
			.dx-viewbox2 .first	{ border-top:none; }			
			.dx-viewbox2 .last	{ border-bottom:none; }
		</style>
	</head>
	<body>
	</body>
</html>";
			
			o.Navigate("about:blank");
			o.Document.OpenNew(false);
			o.Document.Write(html);
			o.Refresh();
		}
	}
}

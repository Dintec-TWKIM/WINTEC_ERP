using System;
using System.Data;
using System.Net;
using System.Windows.Forms;
using Duzon.Common.Forms;
using GemBox.Spreadsheet;

namespace Dintec.AutoWeb
{
	public class Sps
	{
		enum Col { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P};



		// 11397 : SHIP PROCUREMENT SERVICES S.A.
		private AutoWebBroswer _awb = new AutoWebBroswer();

		public string RefNumber
		{
			get; set;
		}

		public string WebLink
		{
			get; set;
		}

		public string ExcelFileName
		{
			get; set;
		}

		public string ErrorMessage
		{
			get; set;
		}

		private void Login()
		{
			if (_awb.WebBrowser.IsExists("username"))
			{
				_awb.WebBrowser.SetValue("username", "dongjin@dintec.co.kr");
				_awb.WebBrowser.SetValue("password", "gkfnzl1");
				_awb.WebBrowser.Click("loginButton");
			}
		}

		public string DownloadInquiry()
		{
			//_awb.Show();

			// 임시폴더 생성
			string inqPath = Application.StartupPath + @"\temp\";
			string inqFile = "";

			// 사이트 이동 및 로그인
			_awb.WebBrowser.GoTo(WebLink);
			Login();

			// 견적서 다운로드 (엑셀 파일)
			if (_awb.WebBrowser.IsExists("downloadLink"))
			{
				// 다운로드 준비
				string downLink = _awb.WebBrowser.GetValue("downloadLink");
				inqFile = inqPath + FileMgr.GetUniqueFileName(inqPath + RefNumber + ".xlsx");

				// 견적서 다운로드
				WebClient client = new WebClient();
				//client.Headers.Add(HttpRequestHeader.Cookie, UWebBrowser.GetGlobalCookies(_awb.WebBrowser.Document.Url.AbsoluteUri));
				client.DownloadFile(new Uri(downLink), inqFile);
			}
			else
			{
				ErrorMessage = "이미 완료된 건입니다.";
			}

			return inqFile;
		}

		public void Submit(DataTable dtHead, DataTable dtLine)
		{
			// 엑셀 견적서 추출
			SpreadsheetInfo.SetLicense("EAAN-UCCU-1F8C-X668");
			ExcelFile excel = ExcelFile.Load(ExcelFileName);

			// 기본시트 불러오기
			ExcelWorksheet sheet = excel.Worksheets[0];

			for (int i = 15; i < sheet.Rows.Count; i++)
			{
				int no = GetTo.Int(sheet.Cells[i, (int)Col.B].Value);

				if (no > 0)
				{
					DataRow[] row = dtLine.Select("NO_DSP = " + no);

					// 수량
					sheet.Cells[i, (int)Col.J].Value = row[0]["QT_QTN"];
					sheet.Cells[i, (int)Col.L].Value = row[0]["UM_EX_S"];
					sheet.Cells[i, (int)Col.O].Value = row[0]["LT"];
				}
			}

			excel.Save(ExcelFileName);
			return;
		}
	}
}

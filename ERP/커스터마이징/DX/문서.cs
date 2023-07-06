using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Dintec;
using System.Web;

namespace DX
{
	public class 문서
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="문서종류">웹서비스 메서드 이름</param>
		/// <param name="json"></param>
		private static string 다운로드(string 문서종류, string Json)
		{
			bool 라이브 = true;

			//if (상수.사원번호 == "S-343")
			//	라이브 = false;

			string url = (라이브 ? "http://dint.ec/" : "http://localhost/dx/") + "Doc/Service/" + 문서종류;
			
			// ********** Json 요청
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
			request.ContentType = "application/json; charset=utf-8";
			request.Method = "POST";

			using (StreamWriter stream = new StreamWriter(request.GetRequestStream()))
			{
				stream.Write(Json);
				stream.Flush();
				stream.Close();
			}

			// ********** Json 응답
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Dictionary<string, string> resultDict;

			using (StreamReader reader = new StreamReader(response.GetResponseStream()))
			{
				string resultJson = reader.ReadToEnd();
				resultDict = JSON.역직렬화<Dictionary<string, string>>(resultJson);
			}

			if (resultDict["result"] == "success")
			{
				string download = resultDict["download"];
				
				// 웹서버에서 파일 다운로드
				WebClient client = new WebClient();
				client.DownloadFile(download, FileMgr.GetTempPath() + 파일.파일이름(download));
				
				
				return FileMgr.GetTempPath() + 파일.파일이름(download);
			}

			return "";
		}


		public static string 발주서(string 회사코드, string 발주번호) => 다운로드("PurchaseOrderToPdf", JSON.직렬화(new Dictionary<string, string> { { "회사코드", 회사코드 }, { "발주번호", 발주번호 }, { "블라인드", "" } }));

		public static string 발주서_블라인드(string 회사코드, string 발주번호) => 다운로드("PurchaseOrderToPdf", JSON.직렬화(new Dictionary<string, string> { { "회사코드", 회사코드 }, { "발주번호", 발주번호 }, { "블라인드", "BLIND" } }));

		public static string 발주서_재고예약(string 회사코드, string 발주번호) => 다운로드("PurchaseOrderToPdf", JSON.직렬화(new Dictionary<string, string> { { "회사코드", 회사코드 }, { "발주번호", 발주번호 }, { "재고예약", "STBOOK" } }));


		public static string 급여명세서(string 회사코드, string 귀속년월, string 사원번호) => 다운로드("PaystubToPdf", JSON.직렬화(new Dictionary<string, string> { { "회사코드", 회사코드 }, { "귀속년월", 귀속년월 }, { "사원번호", 사원번호 } }));

		//public static string 급여명세서(string 회사코드, string 지급년월, string 사원번호) => 다운로드("PurchaseOrderToPdf", JSON.직렬화(new Dictionary<string, string> { { "회사코드", 회사코드 }, { "발주번호", "FB22000549-01" }, { "블라인드", "" } }));
	}
}

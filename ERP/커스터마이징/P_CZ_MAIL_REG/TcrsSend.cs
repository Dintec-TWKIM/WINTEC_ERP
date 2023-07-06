using Dintec;
using Parsing;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace cz
{
	class TcrsSend
	{
		JiBe jibe = new JiBe();

		public void TcrsSend_Re()
		{
			string strCharSet = "UTF-8"; // EUC-KR
			string strUrl = "https://api.klcsm.co.kr/v1/QR?OrderNo=10177-0422";


			System.Text.Encoding enc = System.Text.Encoding.GetEncoding(strCharSet);

			HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(strUrl);
			webReq.Method = "GET";
			webReq.ContentType = "application/x-www-form-urlencoded";

			HttpWebResponse webRes = (HttpWebResponse)webReq.GetResponse();

			System.Text.Encoding responseEncoding = System.Text.Encoding.GetEncoding(webRes.CharacterSet);

			Stream respGetStream = webRes.GetResponseStream();
			StreamReader readerGet = new StreamReader(respGetStream, responseEncoding);


			string resultGet = readerGet.ReadToEnd();

			webRes.Close();

			string query = string.Empty;
			query = @"EXEC PS_CZ_MM_QTIO_TCRS_OUT";
			DataTable dth = DBMgr.GetDataTable(query);

			string jsonFromFile = string.Empty;

			if (dth.Rows.Count > 0)
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://scm.techcross.com/KONE_API/index.jsp");
				httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8"; // application/json
				httpWebRequest.Method = "POST";


				using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				{

					// get
					var tcrsValue = jibe.TCRSWrite(dth);
					var jsonToWrite = JsonConvert.SerializeObject(tcrsValue, Newtonsoft.Json.Formatting.Indented);
					jsonToWrite = "[" + jsonToWrite + "]";

					streamWriter.Write(jsonToWrite);


				}

				HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				string test = httpResponse.CharacterSet.ToString();

				using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
				{
					var result = streamReader.ReadToEnd();
					jsonFromFile = result;

					//txtLog.Text = jsonFromFile;

					WriteTextTCRS_RE(jsonFromFile); // 받은데이터 저장
				}
			}
		}


		// return 값에 다 포함 되어있음.
		private void WriteTextTCRS(string txtLog)
		{
			string folder = Application.StartupPath + @"TCRSLog";

			DirectoryInfo dirinfo = new DirectoryInfo(folder);
			if (!dirinfo.Exists) dirinfo.Create();

			string txtFileName = folder + @"\TCRSLog" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

			FileStream fileStream = new FileStream(txtFileName, FileMode.Append, FileAccess.Write);
			StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.Default);

			streamWriter.Write(String.Format("[{0}] ", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
			streamWriter.WriteLine(txtLog);
			streamWriter.Flush();
			streamWriter.Close();
			fileStream.Close();
		}

		private void WriteTextTCRS_RE(string txtLog)
		{
			string folder = Application.StartupPath + @"TCRSLog";

			DirectoryInfo dirinfo = new DirectoryInfo(folder);
			if (!dirinfo.Exists) dirinfo.Create();

			string txtFileName = folder + @"\TCRSLog_RE" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

			FileStream fileStream = new FileStream(txtFileName, FileMode.Append, FileAccess.Write);
			StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.Default);

			streamWriter.Write(String.Format("[{0}] ", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
			streamWriter.WriteLine(txtLog);
			streamWriter.Flush();
			streamWriter.Close();
			fileStream.Close();
		}
	}

}

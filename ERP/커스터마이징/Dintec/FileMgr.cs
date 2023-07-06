using Duzon.Common.Forms;
using Duzon.ERPU.Utils;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;



namespace Dintec
{
	public class FileMgr
	{
		public static string GetTempPath()
		{
			string path = Application.StartupPath + @"\temp\";
			FileMgr.CreateDirectory(path);
			return path;
		}

		public static void CreateDirectory(string path)
		{
			Directory.CreateDirectory(path);
		}

		


		public static string GetPath(string fullPathFile)
		{
			return fullPathFile.Substring(0, fullPathFile.LastIndexOf(@"\"));
		}

		public static string GetUniqueFileName(string url, string localFile)
		{
			FileInfo file = new FileInfo(localFile);
			string newName = Regex.Replace(file.Name.Replace(file.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_ \-]", string.Empty, RegexOptions.Singleline);
			int index = 0;

			while (Exists(url, newName + file.Extension))
			{
				index++;
				newName = Regex.Replace(file.Name.Replace(file.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_ \-]", string.Empty, RegexOptions.Singleline) + "(" + index + ")";
			}

			return newName + file.Extension;
		}

		public static string GetUniqueFileName(string fullPathFile)
		{
			FileInfo file = new FileInfo(fullPathFile);

			string newName = string.Empty;
			if (!string.IsNullOrEmpty(file.Name))
				newName = Regex.Replace(file.Name.Replace(file.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_ \-]", string.Empty, RegexOptions.Singleline);
			else
				newName = "temp";


			string path = GetPath(fullPathFile);

			int index = 0;

			while (File.Exists(path + @"\" + newName + file.Extension))
			{
				index++;
				newName = Regex.Replace(file.Name.Replace(file.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_ \-]", string.Empty, RegexOptions.Singleline) + "(" + index + ")";
			}

			return newName + file.Extension;
		}

		public static bool Exists(string url, string fileName)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "/" + fileName);
			HttpWebResponse response = null;
			bool flag = false;

			try
			{
				response = (HttpWebResponse)request.GetResponse();
				flag = true;
			}
			catch
			{
				flag = false;
			}
			finally
			{
				if (response != null) response.Close();
			}

			return flag;
		}


		public static string Upload(string localFile, string serverPath)
		{
			string fileName = Path.GetFileName(localFile);

			string result = FileUploader.UploadFile(fileName, localFile, "", serverPath);

			if (result.IndexOf("Fail") >= 0)
				throw new Exception(result); ;

			return fileName;
		}

		// localFile : 경로 및 확장자까지 몽땅 포함
		public static string Upload_WF(string companyCode, string fileNumber, string localFile, bool overwrite)
		{
			// 경로 없이 파일이름만 있는 경우 임시 경로를 붙여줌
			if (localFile.IndexOf(@"\") < 0)
				localFile = Application.StartupPath + @"\temp\" + localFile;

			// 해당 견적파일의 년도 가져오기
			DataTable dt = DBMgr.GetDataTable("SELECT * FROM CZ_MA_WORKFLOWH WHERE CD_COMPANY = '" + companyCode + "' AND NO_KEY = '" + fileNumber + "' ORDER BY TP_STEP");
			string yyyy = dt.Rows.Count > 0 ? dt.Rows[0]["DTS_INSERT"].ToString().Substring(0, 4) : Util.GetToday().Substring(0, 4);

			// 업로드 폴더 (ERP-U 루트에 생성됨)
			string serverPath = "WorkFlow/" + companyCode + "/" + yyyy + "/" + fileNumber;

			// 업로드 파일 이름 결정
			string localPath = Path.GetDirectoryName(localFile);
			string fileName = Path.GetFileName(localFile).Trim();

			// 엎어쓰기가 아닐경우 리네임
			if (!overwrite)
			{
				string url = Global.MainFrame.HostURL + "/" + serverPath;
				fileName = GetUniqueFileName(url, localPath + @"\" + fileName);
			}

			// 업로드
			string result = FileUploader.UploadFile(fileName, localFile, "", serverPath);

			if (result.IndexOf("Fail") >= 0)
				throw new Exception(result); ;

			return fileName;
		}


		public static string Download(string serverFile, bool run)
		{
			// 경로 표시에 "\" 것이 들어오면 "/" 으로 바꿔줌
			if (serverFile.IndexOf(@"\") >= 0)
				serverFile = serverFile.Replace(@"\", "/");

			string serverPath = serverFile.Substring(0, serverFile.LastIndexOf("/"));
			string localPath = Application.StartupPath + @"\temp";

			string downFileName = serverFile.Substring(serverFile.LastIndexOf("/") + 1);
			string newFileName = GetUniqueFileName(localPath + @"\" + downFileName);

			// 디렉토리 추가
			CreateDirectory(localPath);

			// 다운로드
			WebClient wc = new WebClient();
			wc.DownloadFile(Global.MainFrame.HostURL + "/" + serverPath + "/" + Uri.EscapeDataString(downFileName), localPath + @"\" + newFileName);


			// 첨부파일 실행
			if (run)
				Process.Start(Util.GetTO_String(localPath + @"\" + newFileName));

			return newFileName;
		}

		public static string Download(string serverFile, string newFileName, bool run)
		{
			string serverPath = serverFile.Substring(0, serverFile.LastIndexOf(@"\"));
			string localPath = Application.StartupPath + @"\temp";

			string downFileName = serverFile.Substring(serverFile.LastIndexOf(@"\") + 1);
			newFileName = GetUniqueFileName(localPath + @"\" + newFileName);

			// 디렉토리 추가
			CreateDirectory(localPath);

			// 다운로드
			WebClient wc = new WebClient();
			wc.DownloadFile(Global.MainFrame.HostURL + "/" + serverPath.Replace(@"\", "/") + "/" + Uri.EscapeDataString(downFileName), localPath + @"\" + newFileName);

			// 첨부파일 실행
			if (run)
				Process.Start(Util.GetTO_String(localPath + @"\" + newFileName));

			return newFileName;
		}

		public static string Download_WF(string companyCode, string fileNumber, string serverFile, bool run)
		{
			string query = "SELECT * FROM CZ_MA_WORKFLOWH WITH(NOLOCK) WHERE CD_COMPANY = '" + companyCode + "' AND NO_KEY = '" + fileNumber + "' ORDER BY TP_STEP";
			DataTable dt = DBMgr.GetDataTable(query);

			string yyyy = dt.Rows.Count > 0 ? dt.Rows[0]["DTS_INSERT"].ToString().Substring(0, 4) : Util.GetToday().Substring(0, 4);
			string serverPath = "WorkFlow/" + companyCode + "/" + yyyy + "/" + fileNumber;
			string localPath = Application.StartupPath + @"\temp";

			// 다운로드 파일 이름 결정
			string downFileName = serverFile.Substring(serverFile.LastIndexOf(@"\") + 1);
			string newFileName = GetUniqueFileName(localPath + @"\" + downFileName);

			// 디렉토리 추가
			CreateDirectory(localPath);

			// 다운로드
			WebClient wc = new WebClient();
			wc.DownloadFile(Global.MainFrame.HostURL + "/" + serverPath + "/" + Uri.EscapeDataString(downFileName), localPath + @"\" + newFileName);
			string a = Global.MainFrame.HostURL + "/" + serverPath + "/" + Uri.EscapeDataString(downFileName);
			//Global.MainFrame.ShowMessage(Global.MainFrame.HostURL + "/" + serverPath + "/" + Uri.EscapeDataString(downFileName));

			// 첨부파일 실행
			if (run)
			{
				Process.Start(Util.GetTO_String(localPath + @"\" + newFileName));
			}

			return newFileName;
		}

		public static string Download_WF(string companyCode, string fileNumber, string serverFile, string fileName, bool run)
		{
			// 경로 표시에 "\" 것이 들어오면 "/" 으로 바꿔줌
			//newFileName = newFileName.Replace(@"\", "/");


			string query = "SELECT * FROM CZ_MA_WORKFLOWH WITH(NOLOCK) WHERE CD_COMPANY = '" + companyCode + "' AND NO_KEY = '" + fileNumber + "' ORDER BY TP_STEP";
			DataTable dt = DBMgr.GetDataTable(query);

			string yyyy = dt.Rows.Count > 0 ? dt.Rows[0]["DTS_INSERT"].ToString().Substring(0, 4) : Util.GetToday().Substring(0, 4);
			string serverPath = "WorkFlow/" + companyCode + "/" + yyyy + "/" + fileNumber;
			string localPath = Application.StartupPath + @"\temp";

			// 파일이름 자체에 경로가 포함된 경우
			string newFileName;

			if (fileName.IndexOf(@"\") >= 0)
			{
				newFileName = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
				localPath = fileName.Substring(0, fileName.LastIndexOf(@"\"));
			}
			else
			{
				newFileName = fileName;
			}

			// 다운로드 파일 이름 결정
			string downFileName = serverFile.Substring(serverFile.LastIndexOf(@"\") + 1);
			newFileName = GetUniqueFileName(localPath + @"\" + newFileName);

			// 디렉토리 추가
			CreateDirectory(localPath);

			// 다운로드
			WebClient wc = new WebClient();
			wc.DownloadFile(Global.MainFrame.HostURL + "/" + serverPath + "/" + Uri.EscapeDataString(downFileName), localPath + @"\" + newFileName);
			string a = Global.MainFrame.HostURL + "/" + serverPath + "/" + Uri.EscapeDataString(downFileName);
			//Global.MainFrame.ShowMessage(Global.MainFrame.HostURL + "/" + serverPath + "/" + Uri.EscapeDataString(downFileName));

			// 첨부파일 실행
			if (run)
			{
				Process.Start(Util.GetTO_String(localPath + @"\" + newFileName));
			}

			return newFileName;
		}


		//public static void Download_WF(string NO_FILE, string DT_INQ, string serverFile, string localPath, bool run)
		//{
		//	string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
		//	string yyyy = DT_INQ.Substring(0, 4);
		//	string fileName = serverFile.Substring(serverFile.LastIndexOf(@"\") + 1);
		//	string serverPath = "WorkFlow/" + CD_COMPANY + "/" + yyyy + "/" + NO_FILE;

		//	CreateDirectory(localPath);

		//	WebClient wc = new WebClient();
		//	wc.DownloadFile(Global.MainFrame.HostURL + "/" + serverPath + "/" + Uri.EscapeDataString(fileName), localPath + @"\" + fileName);

		//	if (run)
		//	{
		//		Process.Start(Util.GetTO_String(localPath + @"\" + fileName));
		//	}
		//}

		public static void DownloadBinary(string fileName, object fileData)
		{
			// 다운로드 경로
			string path = Application.StartupPath + @"\temp\";
			FileMgr.CreateDirectory(path);

			// 다운로드 파일 (경로포함)
			string download = path + FileMgr.GetUniqueFileName(path + fileName);

			// 파일 데이터
			byte[] documentBinary = (byte[])fileData;

			// 파일 다운로드
			FileStream fStream = new FileStream(download, FileMode.Create);
			fStream.Write(documentBinary, 0, documentBinary.Length);
			fStream.Close();
			fStream.Dispose();

			// 실행
			Process.Start(download);
		}
	}
}

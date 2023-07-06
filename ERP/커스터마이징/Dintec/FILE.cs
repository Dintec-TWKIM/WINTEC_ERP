using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data;
using Duzon.Common.Forms;
using Duzon.ERPU.Utils;

namespace Dintec
{
	public static class FILE
	{

		public static void Copy(string sourceFileName, string destFileName)
		{
			File.Copy(sourceFileName, destFileName);
		}

		public static string GetUniqueFileName(string fileName)
		{
			FileInfo file = new FileInfo(fileName);
			string newName = file.Name.Replace(file.Extension, "");
			string path = DIR.GetPath(fileName);
			int index = 0;

			while (File.Exists(path + @"\" + newName + file.Extension))
			{
				index++;
				newName = file.Name.Replace(file.Extension, "") + "(" + index + ")";
			}

			return newName + file.Extension;
		}

		public static string GetUniqueFileName(string url, string fileName)
		{
			// 도메인이 없는 경우 기본 ERP 도메인으로 변경
			if (url.IndexOf("http://") < 0)
				url = Global.MainFrame.HostURL + "/" + url;

			FileInfo file = new FileInfo(fileName);
			string newName = Regex.Replace(file.Name.Replace(file.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_() ]", "", RegexOptions.Singleline);
			int index = 0;

			while (Exists(url, newName + file.Extension))
			{
				index++;
				newName = Regex.Replace(file.Name.Replace(file.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_() ]", "", RegexOptions.Singleline) + "(" + index + ")";
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
				if (response != null)
					response.Close();
			}

			return flag;
		}

		#region ==================================================================================================== 다운로드
		/// <param name="webPath">웹서버(ERP) 경로를 포함한 파일명</param>
		/// <param name="locPath">로컬서버 경로를 포함한 파일명</param>
		public static void Download(string webPath, string locPath)
		{
			// 어떤 유형이 들어와도 다 받아줌
			webPath = webPath.Replace(Global.MainFrame.HostURL, "");
			webPath = webPath.TrimStart('/');

			WebClient wc = new WebClient();
			wc.DownloadFile(Global.MainFrame.HostURL + "/" + webPath, locPath);
		}

		/// <param name="fileNumber">워크플로우 첨부파일의 파일번호</param>
		/// <param name="fileName">첨부파일명</param>
		/// <param name="run">실행여부</param>
		public static string Download(object fileNumber, object fileName, bool run)
		{
			return Download(PATH.GetWorkFlowPath(fileNumber.ToString()) + "/" + fileName, run);
		}

		/// <param name="path">웹서버(ERP) 경로를 포함한 파일명</param>
		/// <param name="run">실행여부</param>
		public static string Download(object path, bool run)
		{
			// 경로 표시에 "\" 것이 들어오면 "/" 으로 바꿔줌
			path = path.ToString().Replace(@"\", "/");

			// 웹서버경로와 로컬경로
			string webPath = PATH.GetPath(path);
			string locPath = PATH.GetTempPath();

			// 다운로드할 파일과 로컬에 저장할 파일
			string fileName = GetFileName(path);
			string uniqName = GetUniqueFileName(locPath + @"\" + fileName);

			// 다운로드
			WebClient wc = new WebClient();
			wc.DownloadFile(Global.MainFrame.HostURL + "/" + webPath + "/" + Uri.EscapeDataString(fileName), locPath + @"\" + uniqName);

			// 파일 실행
			if (run)
				Process.Start(locPath + @"\" + uniqName);

			return locPath + @"\" + uniqName;
		}

		public static string DownloadBinary(object fileName, object fileData, bool run)
		{
			// 로컬 경로 및 다운로드할 파일 이름
			string locPath = PATH.GetTempPath();
			string uniqName = GetUniqueFileName(locPath + @"\" + fileName);

			// 파일 데이터
			byte[] documentBinary = (byte[])fileData;

			// 파일 다운로드
			FileStream fStream = new FileStream(locPath + @"\" + uniqName, FileMode.Create);
			fStream.Write(documentBinary, 0, documentBinary.Length);
			fStream.Close();
			fStream.Dispose();

			// 파일 실행
			if (run)
				Process.Start(locPath + @"\" + uniqName);

			return uniqName;
		}

		#endregion

		public static string DownloadWF(string companyCode, string fileNumber, string fileName, bool run)
		{
			// 로컬 경로
			string localPath = Application.StartupPath + @"\temp";
			DIR.CreateDirectory(localPath);

			// 다운로드 파일 이름 결정
			string fileNameNew = GetUniqueFileName(localPath + @"\" + fileName);

			// 다운로드
			WebClient wc = new WebClient();
			wc.DownloadFile(Global.MainFrame.HostURL + "/" + DIR.GetServerPathWF(companyCode, fileNumber) + "/" + Uri.EscapeDataString(fileName), localPath + @"\" + fileNameNew);

			// 첨부파일 실행
			if (run)
				Process.Start(localPath + @"\" + fileNameNew);

			return fileNameNew;
		}

		// ********** 업로드 (워크플로우)
		/// <param name="locPath">로컬 경로를 포함한 파일명</param>
		/// <param name="webPath">웹서버(ERP) 경로, 파일이름은 제외한 경로만, Upload + 페이지ID 경로까지는 자동 부여</param>
		public static string Upload(string locPath, string webPath)
		{
			string fileName = GetFileName(locPath);

			if (webPath.IndexOf("/") < 0)
				webPath = "Upload/" + Global.MainFrame.CurrentPageID + "/" + webPath;

			string result = FileUploader.UploadFile(fileName, locPath, "", webPath);

			if (result.IndexOf("Fail") >= 0)
				throw new Exception(result);

			return fileName;
		}


		public static string UploadWF(string fileNumber, string fileName, bool overwrite)
		{			
			return UploadWF(Global.MainFrame.LoginInfo.CompanyCode, fileNumber, fileName, overwrite);
		}

		public static string UploadWF(string companyCode, string fileNumber, string fileName, bool overwrite)
		{
			// 임시 경로로 복사 (파일이 열려있을때 업로드 안되는것을 해결하기 위해 복하 한번 해줌)
			string tempFileName = DIR.GetTempPath() + @"\" + Path.GetFileName(fileName);
			File.Copy(fileName, tempFileName, true);

			// 업로드 폴더, 파일
			string uploadDir = DIR.GetServerPathWF(companyCode, fileNumber);
			string newFileName = overwrite ? Path.GetFileName(tempFileName) : GetUniqueFileName(uploadDir, tempFileName);

			// 업로드
			string result = FileUploader.UploadFile(newFileName, tempFileName, "", uploadDir);

			if (result.IndexOf("Fail") >= 0)
				throw new Exception(result);

			return newFileName;
		}

		public static string UploadWF(string companyCode, string fileNumber, string sourceFileName, string destFileName)
		{
			// 경로 없이 파일이름만 있는 경우 임시 경로를 붙여줌
			if (sourceFileName.IndexOf(@"\") < 0)
				sourceFileName = DIR.GetTempPath() + @"\" + sourceFileName;

			// 업로드 폴더
			string destUrl = DIR.GetServerPathWF(companyCode, fileNumber);

			// 업로드
			string result = FileUploader.UploadFile(destFileName, sourceFileName, "", destUrl);

			if (result.IndexOf("Fail") >= 0)
				throw new Exception(result);

			return destFileName;
		}

		public static bool DeleteWF(string companyCode, string fileNumber, string fileName)
		{
			string destUrl = DIR.GetServerPathWF(companyCode, fileNumber);
			FileUploader.DeleteFile(destUrl, "", fileName);			

			return true;
		}







		public static string GetFileName(object path)
		{
			return Path.GetFileName(path.ToString());
		}

		public static string GetExtension(object fileName)
		{
			return Path.GetExtension(fileName.ToString());
		}

		public static string GetFileNameWithoutExtension(object fileName)
		{
			return Path.GetFileNameWithoutExtension(fileName.ToString());
		}

	}
}

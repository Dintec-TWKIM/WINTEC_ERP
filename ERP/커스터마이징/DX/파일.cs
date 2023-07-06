using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data;
using Duzon.Common.Forms;
using Duzon.ERPU.Utils;
using Dintec;


namespace DX
{
	public class 파일
	{
		FileInfo file;
		string[] files;

		public 파일()
		{

		}

		public 파일(string 파일이름)
		{
			file = new FileInfo(파일이름);
		}

		public string 이름 => file.Name;

		public string 이름_경로포함() => file.FullName;

		public string[] 이름s_경로포함 => files;

		public string 확장자() => file.Extension;

		public string 확장자_점제외() => 확장자().Replace(".", "");

		/// <summary>
		/// 현재 파일의 크기(바이트)
		/// </summary>
		public long 크기() => file.Length;

		public DateTime 수정한날짜() => file.LastWriteTime;









		public static void 복사(string sourceFileName, string destFileName)
		{
			//COPY
			File.Copy(sourceFileName, destFileName);
		}



		public static void 이름변경(string 원래이름, string 바꿀이름)
		{
			바꿀이름 = 파일.경로만(원래이름) + @"\" + 바꿀이름;
			삭제(바꿀이름);
			File.Move(원래이름, 바꿀이름);
		}

		public static void 삭제(string 파일이름)
		{
			File.Delete(파일이름);
		}

		/// <summary>
		/// 파일이름이 중복될 경우 파일이름 뒤에 인덱스를 붙임, 경로 포함해서 넘기면 경로 뺀 파일이름만 리턴됨, 경로 없으면 임시경로로 지정됨
		/// </summary>
		/// <param name="파일이름"></param>
		/// <returns></returns>
		public static string 파일이름_고유(string 파일이름)
		{
			// 아무런 경로 문자가 없으면 로컬 temp폴더를 붙여줌 (웹은 디폴트를 지정하는게 불가능하므로)
			if (!파일이름.발생(@"\", "/"))
				파일이름 = 경로.임시() + @"\" + 파일이름;

			// 시작
			FileInfo 파일 = new FileInfo(파일이름);
			string 새이름 = Regex.Replace(파일.Name.Replace(파일.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_() ]", "", RegexOptions.Singleline);
			int 인덱스 = 0;

			if (파일이름.발생(@"\"))
			{
				while (File.Exists(경로.경로만(파일이름) + @"\" + 새이름 + 파일.Extension))
				{
					인덱스++;
					새이름 = Regex.Replace(파일.Name.Replace(파일.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_() ]", "", RegexOptions.Singleline) + "(" + 인덱스 + ")";
				}
			}
			else
			{
				while (파일유무(경로.경로만(파일이름), 새이름 + 파일.Extension))
				{
					인덱스++;
					새이름 = Regex.Replace(파일.Name.Replace(파일.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_() ]", "", RegexOptions.Singleline) + "(" + 인덱스 + ")";
				}
			}
			
			return 새이름 + 파일.Extension;
		}

		/// <summary>
		/// 파일이름이 중복될 경우 파일이름 뒤에 인덱스를 붙임
		/// </summary>
		/// <param name="웹경로">도메인이 없는 경우는 ERP 도메인으로  변경</param>
		/// <param name="파일이름">변경할 파일이름</param>
		/// <returns></returns>
		public static string 파일이름_고유(string 웹경로, string 파일이름)
		{
			// 도메인이 없는 경우 기본 ERP 도메인으로 변경
			if (!웹경로.발생("http://"))
				웹경로 = 상수.호스트URL + "/" + 웹경로;

			FileInfo 파일 = new FileInfo(파일이름);
			string 새이름 = Regex.Replace(파일.Name.Replace(파일.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_() ]", "", RegexOptions.Singleline);
			int 인덱스 = 0;

			while (파일유무(웹경로, 새이름 + 파일.Extension))
			{
				인덱스++;
				새이름 = Regex.Replace(파일.Name.Replace(파일.Extension, ""), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ\[\]_() ]", "", RegexOptions.Singleline) + "(" + 인덱스 + ")";
			}

			return 새이름 + 파일.Extension;
		}

		public static bool 파일유무(string url, string fileName)
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

		/// <param name="웹경로파일이름">웹서버(ERP) 경로를 포함한 파일이름</param>
		public static string 다운로드(string 웹경로파일이름, bool 실행여부)
		{
			// temp 폴더에 저장
			string 로컬경로파일이름 = 경로_임시() + @"\" + 파일이름(웹경로파일이름);
			return 다운로드(웹경로파일이름, 로컬경로파일이름, 실행여부);
		}

		/// <param name="웹경로파일이름">웹서버(ERP) 경로를 포함한 파일이름</param>
		/// <param name="로컬경로파일이름">로컬경로를 포함한 파일이름</param>
		public static string 다운로드(string 웹경로파일이름, string 로컬경로파일이름, bool 실행여부)
		{
			// 경로가 없는 경우 현패 페이지를 경로로 함
			if (!웹경로파일이름.시작("UPLOAD"))
				웹경로파일이름 = "UPLOAD/" + 상수.페이지ID + "/" + 웹경로파일이름;

			// 다운로드
			WebClient wc = new WebClient();
			wc.DownloadFile(상수.호스트URL + "/" + 웹경로파일이름, 로컬경로파일이름);

			// 파일 실행
			if (실행여부) 실행(로컬경로파일이름);

			return 로컬경로파일이름;
		}

		public static bool DeleteWF(string companyCode, string fileNumber, string fileName)
		{
			string destUrl = DIR.GetServerPathWF(companyCode, fileNumber);
			FileUploader.DeleteFile(destUrl, "", fileName);

			return true;
		}


		public static void 서버파일삭제(string 웹경로파일이름)
		{
			// 경로가 없는 경우 현패 페이지를 경로로 함
			if (!웹경로파일이름.시작("UPLOAD"))
				웹경로파일이름 = "UPLOAD/" + 상수.페이지ID + "/" + 웹경로파일이름;

			FileUploader.DeleteFile(경로만(웹경로파일이름), "", 파일이름(웹경로파일이름));
		}

		#endregion


		#region ==================================================================================================== 업로드

		/// <param name="ERP웹경로"></param>
		public string 업로드(string ERP웹경로) => 업로드(file.FullName, ERP웹경로, true);

		/// <param name="파일이름_경로포함">로컬 경로를 포함한 파일이름, 경로없이 파일이름만 오면 ERP 임시경로 붙여줌 ex) d:\applib\temp\a.pdf</param>
		/// <param name="웹경로">도메인이 없는 경우는 ERP 도메인으로 변경, UPLOAD가 없는 경우는 UPLOAD + 페이지ID 자동 부여</param>
		/// <param name="덮어쓰기">덮어쓰기 false면 파일이름 뒤에 인덱스 붙임</param>
		/// <returns></returns>
		public static string 업로드(string 파일이름_경로포함, string 웹경로, bool 덮어쓰기)
		{
			// 경로가 없으면 ERP 임시 경로를 붙여줌
			if (!파일이름_경로포함.발생(@"\"))
				파일이름_경로포함 = 경로_임시() + @"\" + 파일이름_경로포함;

			// 웹경로가 없는 경우 현패 페이지를 경로로 함
			if (!웹경로.시작("UPLOAD"))
				웹경로 = "UPLOAD/" + 상수.페이지ID + "/" + 웹경로;

			// 업로드할 파일이름
			string 업로드 = 파일이름(파일이름_경로포함);

			if (!덮어쓰기)
				업로드 = 파일이름_고유(웹경로, 업로드);
			
			// 업로드
			string 결과 = FileUploader.UploadFile(업로드, 파일이름_경로포함, "", 웹경로);

			if (결과.발생("Fail"))
				throw new Exception(결과);

			return 웹경로 + "/" + 업로드;
		}

		//파일이름_고유


		//public static string 업로드_워크(string fileNumber, string fileName, bool overwrite)
		//{
		//	return 업로드_워크(Global.MainFrame.LoginInfo.CompanyCode, fileNumber, fileName, overwrite);
		//}

		//public static string 업로드_워크(string 회사코드, string 파일번호, string 파일이름, bool 덮어쓰기)
		//{
		//	// 임시 경로로 복사 (파일이 열려있을때 업로드 안되는것을 해결하기 위해 복하 한번 해줌)
		//	string tempFileName = DIR.GetTempPath() + @"\" + Path.GetFileName(파일이름);
		//	File.Copy(파일이름, tempFileName, true);

		//	// 업로드 폴더, 파일
		//	string uploadDir = DIR.GetServerPathWF(회사코드, 파일번호);
		//	string newFileName = 덮어쓰기 ? Path.GetFileName(tempFileName) : 고유파일이름(uploadDir, tempFileName);

		//	// 업로드
		//	string result = FileUploader.UploadFile(newFileName, tempFileName, "", uploadDir);

		//	if (result.IndexOf("Fail") >= 0)
		//		throw new Exception(result);

		//	return newFileName;
		//}

		//public static string 업로드_워크(string companyCode, string fileNumber, string sourceFileName, string destFileName)
		//{
		//	// 경로 없이 파일이름만 있는 경우 임시 경로를 붙여줌
		//	if (sourceFileName.IndexOf(@"\") < 0)
		//		sourceFileName = DIR.GetTempPath() + @"\" + sourceFileName;

		//	// 업로드 폴더
		//	string destUrl = DIR.GetServerPathWF(companyCode, fileNumber);

		//	// 업로드
		//	string result = FileUploader.UploadFile(destFileName, sourceFileName, "", destUrl);

		//	if (result.IndexOf("Fail") >= 0)
		//		throw new Exception(result);

		//	return destFileName;


		//}

		#endregion






		public static string GetExtension(object fileName)
		{
			return Path.GetExtension(fileName.ToString());
		}

		public static string GetFileNameWithoutExtension(object fileName)
		{
			return Path.GetFileNameWithoutExtension(fileName.ToString());
		}











		/// <summary>
		/// 경로를 포함한 파일이름 중 파일이름만 가져오기
		/// </summary>		
		public static string 파일이름(object 파일이름)
		{
			return Path.GetFileName(파일이름.ToString());
		}

		/// <summary>
		/// 경로를 포함한 파일이름 중 경로만 가져오기
		/// </summary>		
		public static string 경로만(object 파일이름)
		{			
			string 파일 = 파일이름.문자();

			if (파일.발생(@"/"))
				return 파일.Substring(0, 파일.LastIndexOf(@"/"));
			else if (파일.발생(@"\"))
				return 파일.Substring(0, 파일.LastIndexOf(@"\"));
			else
				return 파일;
		}



		/// <summary>
		/// 확장자 가져오기
		/// </summary>
		public static string 확장자(object 파일이름) => Path.GetExtension(파일이름.문자()).Replace(".", "");


		/// <summary>
		/// 워크플로우 경로 가져오기
		/// </summary>
		public static string 경로_워크플로우(string 파일번호)
		{
			return 경로_워크플로우(Global.MainFrame.LoginInfo.CompanyCode, 파일번호);
		}

		public static string 경로_워크플로우(string 회사코드, string 파일번호)
		{
			string query = "SELECT YYYY FROM V_CZ_MA_WORKFLOWH WITH(NOLOCK) WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_FILE = '" + 파일번호 + "' ORDER BY TP_STEP";
			DataTable dt = SQL.GetDataTable(query);
			string yyyy;

			if (dt.Rows.Count > 0)
				yyyy = CT.String(dt.Rows[0]["YYYY"]);
			else
				yyyy = "20" + Regex.Match(파일번호, @"\d+").Value.Left(2);

			return "WorkFlow/" + 회사코드 + "/" + yyyy + "/" + 파일번호;
		}

		/// <summary>
		/// ERP 임시 경로롤 가져옴
		/// </summary>
		/// <returns></returns>
		public static string 경로_임시()
		{
			string path = Application.StartupPath + @"\temp";
			Directory.CreateDirectory(path);
			return path;
		}

		/// <summary>
		/// 경로가 없는 경우 임시경로에 있는 파일을 실행
		/// </summary>
		public static void 실행(string 파일이름_경로포함)
		{
			if (!파일이름_경로포함.발생(@"\"))
				파일이름_경로포함 = 경로_임시() + @"\" + 파일이름_경로포함;

			Process.Start(파일이름_경로포함);
		}
	}
}

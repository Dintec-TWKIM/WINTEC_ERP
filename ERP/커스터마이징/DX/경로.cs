using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace DX
{
	public class 경로
	{
		/// <summary>
		/// 경로를 포함한 파일이름중 경로 부분만 가져오기
		/// </summary>
		/// <param name="파일이름"></param>
		/// <returns></returns>
		public static string 경로만(string 파일이름)
		{			
			if (파일이름.발생(@"\"))			
				return 파일이름.Substring(0, 파일이름.LastIndexOf(@"\"));
			else
				return 파일이름.Substring(0, 파일이름.LastIndexOf("/"));
		}

		/// <summary>
		/// 폴더 만듬
		/// </summary>
		public static void 만들기(string 폴더)
		{
			Directory.CreateDirectory(폴더);
		}

		/// <summary>
		/// 폴더 이름 변경
		/// </summary>
		public static void 이름변경(string 원본폴더이름, string 신규폴더이름)
		{
			Directory.Move(원본폴더이름, 신규폴더이름);
		}

		/// <summary>
		/// ERP 임시 경로롤 가져옴, 경로 마지막에 \는 없음, 없으면 만들어줌
		/// </summary>
		public static string 임시()
		{
			string 경로 = Application.StartupPath + @"\temp";
			Directory.CreateDirectory(경로);
			return 경로;
		}



		/// <summary>
		/// 워크플로우 서버 경로를 가져옴 (web 주소 형태인데 공통부분 빼고 뒷부분만), 첫경로 /로 시작안함
		/// </summary>
		public static string 워크플로우(string 파일번호) => 워크플로우(상수.회사코드, 파일번호);		

		/// <summary>
		/// 워크플로우 서버 경로를 가져옴 (web 주소 형태인데 공통부분 빼고 뒷부분만), 첫경로 /로 시작안함
		/// </summary>
		public static string 워크플로우(string 회사코드, string 파일번호)
		{
			string query = "SELECT TOP 1 DTS_INSERT FROM CZ_MA_WORKFLOWH WITH(NOLOCK) WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_KEY = '" + 파일번호 + "' ORDER BY TP_STEP";
			DataTable dt = 디비.결과(query);
			string yyyy = dt.존재() ? dt.첫행("DTS_INSERT").문자().왼쪽(4) : "20" + Regex.Match(파일번호, @"\d+").Value.왼쪽(2);

			return "WorkFlow/" + 회사코드 + "/" + yyyy + "/" + 파일번호;
		}
	}
}

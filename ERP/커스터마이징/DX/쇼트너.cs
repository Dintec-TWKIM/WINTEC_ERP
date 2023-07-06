using System.Data;

namespace DX
{
	public class 쇼트너
	{
		private static string URL => "http://dint.ec/";

		public static string 적재허가서(string data)
		{
			TSQL sql = new TSQL("PX_CZ_DX_URL_BASE62");
			sql.변수.추가("@REDIRECT", "");
			sql.변수.추가("@DATA"		, data);
			sql.변수.추가("@TYPE"		, "적재허가서");
			sql.변수.추가("@ID_USER"	, 상수.사원번호);
			DataTable dt = sql.실행<DataTable>();

			return dt.Rows[0]["ENCODED"].문자();
		}

		public static string 이미지(string data)
		{
			TSQL sql = new TSQL("PX_CZ_DX_URL_BASE62");
			sql.변수.추가("@REDIRECT", URL + "download");
			sql.변수.추가("@DATA"		, data);
			sql.변수.추가("@TYPE"		, "이미지");
			sql.변수.추가("@ID_USER"	, 상수.사원번호);
			DataTable dt = sql.실행<DataTable>();

			return URL + dt.Rows[0]["ENCODED"];
		}

		public static string 다운로드(string data)
		{
			TSQL sql = new TSQL("PX_CZ_DX_URL_BASE62");
			sql.변수.추가("@REDIRECT", URL + "Download");
			sql.변수.추가("@DATA"		, data);
			sql.변수.추가("@TYPE"		, "다운로드");
			sql.변수.추가("@ID_USER"	, 상수.사원번호);
			DataTable dt = sql.결과();

			return URL + dt.Rows[0]["ENCODED"];
		}

		public static string 그룹웨어(string data)
		{
			// 문서번호로 그룹웨어 링크 찾아보기
			string query = "SELECT * FROM BX.TEAG_APPDOC WHERE doc_no = '" + data + "'";
			DataTable dt = TSQL.결과(디비연결.그룹웨어, query);
			string url = dt.존재() ? "http://gw.dintec.co.kr/Ea/EADocPop/EAAppDocViewPop/?doc_id=" + dt.첫행()["doc_id"] + "&form_id=" + dt.첫행()["form_id"] : "";
			
			// 쇼트너
			TSQL sql = new TSQL("PX_CZ_DX_URL_BASE62");
			sql.변수.추가("@REDIRECT", URL + "Groupware");
			sql.변수.추가("@DATA"		, url);
			sql.변수.추가("@TYPE"		, "그룹웨어");
			sql.변수.추가("@ID_USER"	, 상수.사원번호);
			DataTable dtRet = sql.결과();

			return URL + dtRet.첫행()["ENCODED"];
		}
	}
}

using System.Data;

namespace DX
{
	public class 설정
	{

		public static void 내보내기(string 페이지ID, string 설정, object 값)
		{
			디비 db = new 디비("PX_CZ_MA_USER_CONFIG");
			db.변수.추가("@CD_COMPANY"	, 상수.회사코드);
			db.변수.추가("@NO_EMP"		, 상수.사원번호);
			db.변수.추가("@PAGE_ID"		, 페이지ID);
			db.변수.추가("@CNFG_NAME"		, 설정);
			db.변수.추가("@CNFG_VALUE"	, 값);
			db.실행();
		}

		public static object 가져오기(string 페이지ID, string 설정)
		{
			string query = @"
SELECT
	CNFG_VALUE
FROM CZ_MA_USER_CONFIG WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + 상수.회사코드 + @"'
	AND NO_EMP = '" + 상수.사원번호 + @"'
	AND PAGE_ID = '" + 페이지ID + @"'
	AND CNFG_NAME = '" + 설정 + "'";

			DataTable dt = 디비.결과(query);
			return dt.존재() ? dt.첫행(0).문자() : "";
		}

		public static void 내보내기(string 설정, object 값) => 내보내기(상수.페이지ID, 설정, 값);

		public static object 가져오기(string 설정) => 가져오기(상수.페이지ID, 설정);
	}
}

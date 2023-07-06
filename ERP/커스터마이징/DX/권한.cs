using Duzon.Common.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DX
{
	public class 권한
	{
		public static bool 라이브서버()
		{
			return ClientRepository.DatabaseCallType == "Remoting";
		}

		//코드.사원(상수.사원번호).첫행("CD_DUTY_RESP").문자().포함("200", "400"))    // 200:부서장,400:팀장

		public static bool 부서장() => 코드.사원(상수.사원번호).첫행("CD_DUTY_RESP").문자().포함("200");		// 부서장

		public static bool 팀장() => 코드.사원(상수.사원번호).첫행("CD_DUTY_RESP").문자().포함("200", "400"); // 부서장, 팀장	=> 부서장은 팀장의 모든 권한을 자동으로 받음

		


		public static bool 재고입출고현황_매입처()
		{
			string query = @"
SELECT
	COL = REPLACE(CD_FLAG1, '관련', '')
FROM V_CZ_MA_CODEDTL
WHERE 1 = 1
	AND CD_COMPANY = 'K100'
	AND CD_FIELD = 'CZ_MA00054'
	AND NM_SYSDEF LIKE '%재고입출고현황%'
	AND NM_SYSDEF LIKE '%매입처%'";

			DataTable dt = 디비.결과(query);

			if (dt.Rows.Count == 1)
				return 코드관리(dt.Rows[0][0].정수());

			메시지.오류발생("권한 찾기 실패");
			return false;
		}

		public static bool 코드관리(int 관련번호)
		{			
			string query = @"
SELECT
	1
FROM V_CZ_MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + 상수.회사코드 + @"'
	AND CD_SYSDEF = '" + 상수.사원번호 + @"'	
	AND CD_FIELD = 'CZ_MA00055'	
	AND CD_FLAG" + 관련번호 + " = 'Y'";

			return 디비.결과(query).Rows.Count == 1;
		}
	}
}

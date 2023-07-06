using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DX
{
	public class 메일서명
	{
		private static string 코드관리(string 종류, string 회사코드, string 언어)
		{
			string query = @"
SELECT
	SIGN_TEXT = CD_FLAG1
FROM V_CZ_MA_CODEDTL 
WHERE 1 = 1
	AND CD_COMPANY = 'K100'
	AND CD_FIELD = 'CZ_DX00018'
	AND CD_FLAG2 = '" + 종류 + @"'
	AND CD_FLAG3 = '" + 회사코드 + @"'
	AND CD_FLAG4 IN ('*', '" + 언어 + @"')";

			return 디비.결과(query).첫행(0).문자();
		}


		public static string 발주서(DataTable dtEmp, string 언어)
		{
			string 담당자 = dtEmp.첫행("NM_EMP").문자();
			string 담당자_영문 = dtEmp.첫행("NM_EMP_EN").문자();
			string 전화번호 = dtEmp.첫행("NO_TEL").문자();
			string 이메일 = dtEmp.첫행("NM_EMAIL").문자();

			// 한국회사이고 영문일 경우 영문형식으로 변환
			if (상수.회사코드.포함("K100", "K200") && 언어 == "EN")
			{
				담당자 = 담당자_영문;
				전화번호 = "+82-" + 전화번호.Substring(1);
			}

			// 서명
			string 서명 = 코드관리("PO", 상수.회사코드, 언어);
			서명 = 서명.Replace("{담당자}", 담당자);
			서명 = 서명.Replace("{전화번호}", 전화번호);
			서명 = 서명.Replace("{이메일}", 이메일);

			return 서명;
		}
	}
}

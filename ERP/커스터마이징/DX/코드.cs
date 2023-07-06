using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Dintec;
using System.Collections.Generic;

namespace DX
{
	public class 코드
	{
		public static DataTable 단위() => 디비.결과(코드관리쿼리("MA_B000004"));

		public static DataTable 단위_통합_선용() => 디비.결과("SELECT * FROM CZ_DX_UNIT_REP WHERE CD_COMPANY = 'K100' AND CD_TYPE = 'GS'");

		public static DataTable 소분류() => 디비.결과(코드관리쿼리("MA_B000032"));

		public static DataTable 통화() => 디비.결과(코드관리쿼리("MA_B000005"));


		/// <param name="구분">일반,재고 둘중 하나 스트링으로 넘기기</param>
		/// <returns></returns>
		public static DataTable 발주유형(string 구분)
		{
			string query = @"
SELECT
	CODE = CD_TPPO
,	NAME = NM_TPPO
FROM PU_TPPO
WHERE 1 = 1
	AND CD_COMPANY = '" + 상수.회사코드 + @"'
	AND YN_USE = 'Y'
	AND CD_TPPO IN (" + (구분 == "일반" ? "'1000', '1200', '2000', '2200'" : "'1300', '1400', '2300', '2400'") + ")";

			return 디비.결과(query);
		}

		public static DataTable 코드관리(string 구분코드) => 디비.결과(코드관리쿼리(구분코드));

		public static DataSet 코드관리(params string[] 구분코드)
		{
			string query = "";
			foreach (string s in 구분코드) query += 코드관리쿼리(s);
			return TSQL.결과s(query);
		}

		private static string 코드관리쿼리(string 구분코드)
		{
			string query = @"
SELECT
	CODE		= CD_SYSDEF
,	NAME		= " + (상수.언어 == "KR" ? "NM_SYSDEF" : "ISNULL(NULLIF(NM_SYSDEF_E, ''), NM_SYSDEF)") + @"
,	CD_FLAG1	= ISNULL(CD_FLAG1, '')
,	CD_FLAG2	= ISNULL(CD_FLAG2, '')
,	CD_FLAG3	= ISNULL(CD_FLAG3, '')
,	CD_FLAG4	= ISNULL(CD_FLAG4, '')
,	CD_FLAG5	= ISNULL(CD_FLAG5, '')
,	CD_FLAG6	= ISNULL(CD_FLAG6, '')
,	CD_FLAG7	= ISNULL(CD_FLAG7, '')
,	CD_FLAG8	= ISNULL(CD_FLAG8, '')
FROM V_CZ_MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + 상수.회사코드 + @"'
	AND CD_FIELD = '" + 구분코드 + @"'";

			return query;
		}


		public static DataTable 회사()
		{
			string query = @"
SELECT 
	CODE = CD_COMPANY
,	NAME = NM_COMPANY
FROM MA_COMPANY WITH(NOLOCK)";

			return TSQL.실행<DataTable>(query);
		}

		public static DataTable 근태()
		{
			string query = @"
SELECT
	CODE = CD_WCODE
,	NAME = NM_WCODE
FROM HR_WCODE
WHERE 1 = 1
	AND CD_COMPANY = '" + 상수.회사코드 + @"'
	AND YN_USE = 'Y'";

			return TSQL.결과(query);
		}


		public static DataTable 사원(string 사원번호)
		{
			string query = @"
SELECT
	NO_EMP		= NO_EMP
,	NM_EMP		= NM_KOR
,	NM_EMP_EN	= NM_ENG
,	NO_TEL		= NO_TEL
,	NO_HP		= NO_TEL_EMER
,	NO_FAX		= DC_RMK1
,	NM_EMAIL	= NO_EMAIL
,	CD_DUTY_RESP
FROM MA_EMP
WHERE 1 = 1
	AND CD_COMPANY = '" + 상수.회사코드 + @"'
	AND NO_EMP = '" + 사원번호 + "'";

			return 디비.결과(query);
		}

		public static DataTable 거래처(string 거래처코드)
		{
			string query = @"
SELECT
	A.CD_PARTNER
,	A.LN_PARTNER
,	  CD_EXCH_S	= B.CD_EXCH1
,	  CD_EXCH_P = B.CD_EXCH2
,	S.TP_SO
,	S.NM_SO
,	B.TP_VAT
,	P.CD_TPPO
,	P.NM_TPPO	
,	B.FG_PAYMENT
,	B.FG_TAX
,	A.CD_AREA
,	CD_PARTNER_GRP
,	  LANG		= IIF(A.CD_AREA = '100', 'KR', 'EN')
FROM	  MA_PARTNER	AS A
LEFT JOIN CZ_MA_PARTNER	AS B ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_PARTNER = B.CD_PARTNER
LEFT JOIN SA_TPSO		AS S ON B.CD_COMPANY = S.CD_COMPANY AND B.TP_SO = S.TP_SO
LEFT JOIN PU_TPPO		AS P ON B.CD_COMPANY = P.CD_COMPANY AND B.CD_TPPO = P.CD_TPPO
WHERE 1 = 1
	AND A.CD_COMPANY = '" + 상수.회사코드 + "'";

			if (!거래처코드.발생(")"))
				query += "\n" + "	AND A.CD_PARTNER = '" + 거래처코드 + "'";
			else
				query += "\n" + "	AND A.CD_PARTNER IN " + 거래처코드;

			return 디비.결과(query);
		}

		public static DataTable 거래처담당자(string 거래처코드, 담당자구분 구분)
		{
			string 구분코드 = "";

			if		(구분 == 담당자구분.매출견적)	구분코드 = "'000','001','002'";
			else if (구분 == 담당자구분.매출수주)	구분코드 = "'000','001','003'";
			
			else if (구분 == 담당자구분.매입견적)	구분코드 = "'000','004','005'";
			else if (구분 == 담당자구분.매입발주)	구분코드 = "'000','004','006'";

			else if (구분 == 담당자구분.매입재고)	구분코드 = "'008'";

			string query = @"
SELECT
	*
FROM FI_PARTNERPTR
WHERE 1 = 1
	AND CD_COMPANY = '" + 상수.회사코드 + @"'
	AND CD_PARTNER " + (!거래처코드.발생(")") ? "= '" + 거래처코드 + "'" : "IN " + 거래처코드) + @"
	AND TP_PTR IN (" + 구분코드 + @")
ORDER BY IIF(USE_YN = 'Y', 1, 9), TP_PTR DESC, NM_PTR";

			return 디비.결과(query);
		}

		public static DataTable 호선(string IMO번호)
		{
			string query = @"
SELECT
	A.NO_IMO
,	A.NO_HULL
,	A.NM_VESSEL
,	NM_VESSEL2		= A.NM_VESSEL + ' (' + A.NO_HULL + ')'
,	  NM_TYPE       = C.NM_SYSDEF
,     NM_TYPE2      = IIF(C.CD_FLAG1 = 'NB', '(NB)', '') + C.NM_SYSDEF
,     YN_SPECIAL    = IIF(C.CD_FLAG1 = 'NB', 'Y', 'N')
--,	A.DT_SHIP_DLV
,	A.CD_PARTNER
,	B.LN_PARTNER
,	B.USE_YN

,	YN_PARTNER		= ISNULL(B.USE_YN, 'N')
,	DT_SHIP_DLV		= CASE 
						WHEN LEN(DT_SHIP_DLV) = 6 THEN LEFT(DT_SHIP_DLV, 4) + '-' + RIGHT(DT_SHIP_DLV, 2)
						WHEN LEN(DT_SHIP_DLV) = 4 THEN DT_SHIP_DLV
						ELSE ''
					  END
FROM	  CZ_MA_HULL		AS A WITH(NOLOCK)
LEFT JOIN MA_PARTNER		AS B WITH(NOLOCK) ON B.CD_COMPANY = '" + 상수.회사코드 + @"' AND A.CD_PARTNER = B.CD_PARTNER
LEFT JOIN V_CZ_MA_CODEDTL   AS C WITH(NOLOCK) ON C.CD_COMPANY = 'K100' AND C.CD_FIELD = 'CZ_MA00002' AND A.TP_SHIP = C.CD_SYSDEF
WHERE 1 = 1
	AND NO_IMO = '" + IMO번호 + @"'";

			return SQL.GetDataTable(query);
		}

		public static DataTable 호선(string IMO번호, int 엔진번호)
		{
			string query = @"
SELECT
	A.NO_IMO
,	A.NO_HULL
,	A.NM_VESSEL
,	B.NM_MODEL
FROM CZ_MA_HULL			AS A WITH(NOLOCK)
JOIN CZ_MA_HULL_ENGINE	AS B WITH(NOLOCK) ON A.NO_IMO = B.NO_IMO
WHERE 1 = 1
	AND A.NO_IMO = '" + IMO번호 + @"'
	AND B.NO_ENGINE = " + 엔진번호;

			return SQL.GetDataTable(query);
		}

		public static DataTable 품목(string 품목코드)
		{			
			string query = @"
SELECT
	CD_ITEM
,	NM_ITEM
,	UCODE
--,	KCODE
FROM MA_PITEM WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND CD_ITEM = '" + 품목코드 + @"'";

			return SQL.GetDataTable(query);
		}

		public static DataTable 품목그룹()
		{
			string query = @"
SELECT
	CODE = CD_ITEMGRP
,	NAME = " + (상수.언어 == "KR" ? "NM" : "EN") + @"_ITEMGRP
FROM MA_ITEMGRP WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + 상수.회사코드 + @"'
	AND USE_YN = 'Y'";

			return SQL.GetDataTable(query);

		}

		public static DataTable 부대비용()
		{
			string query = @"
SELECT
	CODE	= CD_ITEM
,	NAME	= NM_ITEM
,	UNIT	= UNIT_IM
,	NO_PART	= STND_ITEM
FROM MA_PITEM WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND CD_ITEM LIKE 'SD%'
	AND YN_USE = 'Y'";

			return SQL.GetDataTable(query);
		}

		public static DataTable 매입부대비용()
		{
			string query = @"
SELECT
	CODE	= CD_ITEM
,	NAME	= NM_ITEM
,	UNIT	= UNIT_IM
,	NO_PART	= STND_ITEM
FROM MA_PITEM WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + 상수.회사코드 + @"'
	AND CD_ITEM != 'SD0010'
	AND (CD_ITEM LIKE 'SD%' OR CD_ITEM = 'ADM007')
	AND YN_USE = 'Y'
ORDER BY NM_ITEM
";

			return SQL.GetDataTable(query);
		}

		public static DataTable 창고()
		{
			string query = @"
SELECT 
	CODE = CD_SL
,	NAME = NM_SL
FROM MA_SL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + 상수.회사코드 + @"'
	AND YN_USE = 'Y'";

			return TSQL.실행<DataTable>(query);
		}





		


		public static DataTable 영업구매그룹(string 사원번호)
		{
			string query = @"
SELECT
	B.CD_SALEGRP
,	B.NM_SALEGRP
,	C.CD_PURGRP
,	C.NM_PURGRP
FROM MA_USER			AS A WITH(NOLOCK)
LEFT JOIN MA_SALEGRP	AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_SALEGRP = B.CD_SALEGRP
LEFT JOIN MA_PURGRP		AS C WITH(NOLOCK) ON A.CD_COMPANY = C.CD_COMPANY AND A.CD_PURGRP = C.CD_PURGRP	
WHERE 1 = 1
	AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND A.NO_EMP = '" + 사원번호 + "'";

			return SQL.GetDataTable(query);
		}


		#region ==================================================================================================== 환율

		public static decimal 환율(object 통화, string 구분) => 환율(유틸.오늘(), 통화, 구분);

		/// <param name="날짜">YYYYMMDD 형식</param>
		/// <param name="통화">통화코드</param>
		/// <param name="구분">B:기준,S:매출,P:매입</param>
		public static decimal 환율(string 날짜, object 통화, string 구분)
		{			
			DataTable dt = SQL.GetDataTable("PS_CZ_MA_EXCHANGE_2", 상수.회사코드, 날짜, 통화);

			if (구분 == "B") return dt.Rows[0]["RT_BASE"].ToDecimal();
			if (구분 == "S") return dt.Rows[0]["RT_SALES"].ToDecimal();
			if (구분 == "P") return dt.Rows[0]["RT_PURCHASE"].ToDecimal();
			
			return 1;
		}

		public static DataTable 환율(string 날짜)
		{
			DataTable dt = SQL.GetDataTable("PS_CZ_MA_EXCHANGES", 상수.회사코드, 날짜);
			return dt;
		}

		#endregion
	}

	public enum 담당자구분
	{
		공통

	,	매출공통
	,	매출견적
	,	매출수주

	,	매입공통
	,	매입견적
	,	매입발주

	,	매입재고
	}
}

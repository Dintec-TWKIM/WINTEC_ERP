using Dintec;
using Duzon.Common.Forms;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DX
{
	public class CODE
	{
		private static string 회사코드 => Global.MainFrame.LoginInfo.CompanyCode;

		public static List<string> FileCode()
		{
			List<string> fileCode = new List<string>();

			if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
			{
				fileCode.AddRange(new string[] { "FB", "DB", "NB", "SB", "NS", "TE" });  // 대표 파일
				fileCode.AddRange(new string[] { "CL", "DS", "ST" });                    // 특별 케이스
			}
			else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
			{
				fileCode.AddRange(new string[] { "A-", "D-", "CN" });
			}

			return fileCode;
		}

		public static DataTable ExchangeName()
		{
			return SQL.GetDataTable(GetCodeQuery("MA_B000005"));
		}

		public static DataTable ExchangeName2()
		{
			string query = @"
		  SELECT '000' AS CODE, 'KRW' AS NAME
UNION ALL SELECT '001' AS CODE, 'USD' AS NAME
UNION ALL SELECT '002' AS CODE, 'JPY' AS NAME
UNION ALL SELECT '003' AS CODE, 'EUR' AS NAME";
			return SQL.GetDataTable(query);
		}

		public static DataTable Unit()
		{
			return SQL.GetDataTable(GetCodeQuery("MA_B000004"));
		}

		public static DataTable ClsS()
		{
			return SQL.GetDataTable(GetCodeQuery("MA_B000032"));
		}

		public static DataTable Key(string keyCode)
		{
			return SQL.GetDataTable(GetCodeQuery(keyCode));
		}

		public static DataTable Code(string keyCode)
		{
			return SQL.GetDataTable(GetCodeQuery(keyCode));
		}

		public static DataTable 코드관리(string 구분코드)
		{
			return SQL.GetDataTable(GetCodeQuery(구분코드));
		}

		public static DataSet 코드관리(params string[] 구분코드)
		{
			string query = "";
			foreach (string s in 구분코드) query += GetCodeQuery(s);
			DataSet ds = SQL.GetDataSet(query);

			return ds;
		}

		public static DataTable 거래처(string 거래처코드)
		{
			string query = @"
SELECT
	A.CD_PARTNER
,	A.LN_PARTNER
,	B.CD_EXCH1		AS CD_EXCH_S
,	B.CD_EXCH2		AS CD_EXCH_P
,	S.TP_SO
,	S.NM_SO
,	B.TP_VAT
,	B.FG_BILL1
,	B.FG_BILL2
,	P.CD_TPPO
,	P.NM_TPPO
,	B.FG_PAYMENT
,	B.FG_TAX
,	A.CD_AREA
FROM	  MA_PARTNER			AS A
LEFT JOIN CZ_MA_PARTNER	        AS B ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_PARTNER = B.CD_PARTNER
LEFT JOIN SA_TPSO				AS S ON B.CD_COMPANY = S.CD_COMPANY AND B.TP_SO = S.TP_SO
LEFT JOIN PU_TPPO				AS P ON B.CD_COMPANY = P.CD_COMPANY AND B.CD_TPPO = P.CD_TPPO
WHERE 1 = 1
	AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";

			if (!거래처코드.Contains(")"))
				query += "\n" + "	AND A.CD_PARTNER = '" + 거래처코드 + "'";
			else
				query += "\n" + "	AND A.CD_PARTNER IN " + 거래처코드;

			return SQL.GetDataTable(query);
		}

		public static DataTable 호선(string IMO번호)
		{
			string query = @"
SELECT
	NO_IMO
,	NO_HULL
,	NM_VESSEL
FROM CZ_MA_HULL WITH(NOLOCK)
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

		public static DataTable Partner(string partnerCode)
		{
			string query = @"
SELECT
	A.*
,	B.TP_SO		AS CD_TPSO
,	B.NM_SO		AS NM_TPSO
,	C.CD_TPPO
,	C.NM_TPPO
FROM	  V_CZ_MA_PARTNER	AS A WITH(NOLOCK)
LEFT JOIN SA_TPSO			AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.TP_SO = B.TP_SO
LEFT JOIN PU_TPPO			AS C WITH(NOLOCK) ON A.CD_COMPANY = C.CD_COMPANY AND A.CD_TPPO = C.CD_TPPO
WHERE 1 = 1
	AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";

			if (partnerCode.IndexOf(")") < 0) query += "\n" + "	AND A.CD_PARTNER = '" + partnerCode + "'";
			if (partnerCode.IndexOf(")") > 0) query += "\n" + "	AND A.CD_PARTNER IN " + partnerCode;

			return SQL.GetDataTable(query);
		}

		public static DataTable SPGroup(string empNumber)
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
	AND A.NO_EMP = '" + empNumber + "'";

			return SQL.GetDataTable(query);
		}

		public static DataTable Hull(string imoNumber)
		{
			string query = @"
SELECT
	A.NO_IMO
,	A.NO_HULL
,	A.NM_VESSEL
,	A.DT_SHIP_DLV
,	A.CD_PARTNER
,	B.LN_PARTNER
,	B.USE_YN
FROM	  CZ_MA_HULL	AS A WITH(NOLOCK)
LEFT JOIN MA_PARTNER	AS B WITH(NOLOCK) ON B.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"' AND A.CD_PARTNER = B.CD_PARTNER
WHERE 1 = 1
	AND A.NO_IMO = '" + imoNumber + "'";

			return SQL.GetDataTable(query);
		}

		private static string GetCodeQuery(string keyCode)
		{
			string companyCode = Global.MainFrame.LoginInfo.CompanyCode;
			string language = Global.MainFrame.LoginInfo.Language;
			string col = language == "KR" ? "NM_SYSDEF" : "ISNULL(NULLIF(NM_SYSDEF_E, ''), NM_SYSDEF)";
			string query = @"
SELECT
	CODE	= CD_SYSDEF
,	NAME	= " + col + @"
,	CD_FLAG1
,	CD_FLAG2
,	CD_FLAG3
,	CD_FLAG4
,	CD_FLAG5
,	CD_FLAG6
,	CD_FLAG7
,	CD_FLAG8
FROM V_CZ_MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND CD_FIELD = '" + keyCode + @"'";

			return query;
		}

		public static DataTable JoinStockQuantity4(string companyCode, DataTable dtItemList)
		{
			// ********** 재고수량
			// Distinct된 재고코드만 가져오기
			DataTable dtStock = new DataTable();
			dtStock.Columns.Add("CD_COMPANY", typeof(string));
			dtStock.Columns.Add("CD_ITEM", typeof(string));

			// 쿼리문
			var q1 = from r in dtItemList.AsEnumerable()
					 where !string.IsNullOrEmpty(r.Field<string>("CD_ITEM"))
					 group r by r.Field<string>("CD_ITEM")
					 into g
					 select dtStock.LoadDataRow(new object[] { companyCode, g.Key }, false);

			// 실행
			q1.Count();

			// 재고수량 가져오기
			SQL sql = new SQL("PS_CZ_MM_STOCK_QT_3", SQLType.Procedure);
			sql.Parameter.Add2(new SqlParameter("@ITEM", SqlDbType.Structured) { TypeName = "ITEM", Value = dtStock });
			DataTable dtQuantity = sql.GetDataTable();

			// ********** 최종 조인된 테이블 생성
			// 관련 필드가 이미 있을 경우는 삭제함 (삭제 안하면 컬럼 순서 꼬임)
			if (dtItemList.Columns.Contains("UM_STK"))
				dtItemList.Columns.Remove("UM_STK");

			// 구조만 복사해서 재고수량 관련 컬럼 추가
			DataTable dtResult = dtItemList.Clone();
			dtResult.Columns.Add("QT_AVSUM", typeof(decimal));
			dtResult.Columns.Add("QT_ST", typeof(decimal));
			dtResult.Columns.Add("QT_BOOK", typeof(decimal));
			dtResult.Columns.Add("QT_AVST", typeof(decimal));
			dtResult.Columns.Add("QT_PO", typeof(decimal));
			dtResult.Columns.Add("QT_HOLD", typeof(decimal));
			dtResult.Columns.Add("QT_AVPO", typeof(decimal));
			dtResult.Columns.Add("UM_STK", typeof(decimal));
			dtResult.Columns.Add("YN_BAD", typeof(string));
			dtResult.Columns.Add("SN_PARTNER_ST", typeof(string));

			// 쿼리문
			var q2 =
				from a in dtItemList.AsEnumerable()
				join b in dtQuantity.AsEnumerable() on new { key1 = a.Field<string>("CD_ITEM") } equals new { key1 = b.Field<string>("CD_ITEM") }
				into c
				from d in c.DefaultIfEmpty()
				select dtResult.LoadDataRow(a.ItemArray.Concat(new object[] {
					d?["QT_AVSUM"]
				,   d?["QT_ST"]
				,   d?["QT_BOOK"]
				,   d?["QT_AVST"]
				,   d?["QT_PO"]
				,   d?["QT_HOLD"]
				,   d?["QT_AVPO"]
				,   d?["UM_STK"]
				,   d?["YN_BAD"] }).ToArray(), false);

			// 쿼리 실행
			q2.Count();

			// 커밋
			//dtResult.Columns.Add("SN_PARTNER_ST", typeof(string));
			dtResult.AcceptChanges();

			return dtResult;
		}

		public static DataTable JoinStock(string companyCode, DataTable dtItemList)
		{
			// ********** 재고수량
			// Distinct된 재고코드만 가져오기
			DataTable dtStock = new DataTable();
			dtStock.Columns.Add("CD_COMPANY", typeof(string));
			dtStock.Columns.Add("CD_ITEM", typeof(string));

			// 쿼리문
			var q1 = from r in dtItemList.AsEnumerable()
					 where !string.IsNullOrEmpty(r.Field<string>("CD_ITEM"))
					 group r by r.Field<string>("CD_ITEM")
					 into g
					 select dtStock.LoadDataRow(new object[] { companyCode, g.Key }, false);

			// 실행
			q1.Count();

			// 재고수량 가져오기
			SQL sql = new SQL("PS_CZ_MM_STOCK_QT_3", SQLType.Procedure);
			sql.Parameter.Add2(new SqlParameter("@ITEM", SqlDbType.Structured) { TypeName = "ITEM", Value = dtStock });
			DataTable dtQuantity = sql.GetDataTable();

			// ********** 최종 조인된 테이블 생성
			// 관련 필드가 이미 있을 경우는 삭제함 (삭제 안하면 컬럼 순서 꼬임)
			if (dtItemList.Columns.Contains("UM_STK"))
				dtItemList.Columns.Remove("UM_STK");

			// 구조만 복사해서 재고수량 관련 컬럼 추가
			DataTable dtResult = dtItemList.Clone();
			dtResult.Columns.Add("QT_AVSUM", typeof(decimal));
			dtResult.Columns.Add("QT_ST", typeof(decimal));
			dtResult.Columns.Add("QT_BOOK", typeof(decimal));
			dtResult.Columns.Add("QT_AVST", typeof(decimal));
			dtResult.Columns.Add("QT_PO", typeof(decimal));
			dtResult.Columns.Add("QT_HOLD", typeof(decimal));
			dtResult.Columns.Add("QT_AVPO", typeof(decimal));
			dtResult.Columns.Add("UM_STK", typeof(decimal));
			dtResult.Columns.Add("YN_BAD", typeof(string));
			dtResult.Columns.Add("SN_PARTNER_ST", typeof(string));

			// 쿼리문
			var q2 =
				from a in dtItemList.AsEnumerable()
				join b in dtQuantity.AsEnumerable() on new { key1 = a.Field<string>("CD_ITEM") } equals new { key1 = b.Field<string>("CD_ITEM") }
				into c
				from d in c.DefaultIfEmpty()
				select dtResult.LoadDataRow(a.ItemArray.Concat(new object[] {
					d?["QT_AVSUM"]
				,   d?["QT_ST"]
				,   d?["QT_BOOK"]
				,   d?["QT_AVST"]
				,   d?["QT_PO"]
				,   d?["QT_HOLD"]
				,   d?["QT_AVPO"]
				,   d?["UM_STK"]
				,   d?["YN_BAD"] }).ToArray(), false);

			// 쿼리 실행
			q2.Count();

			// 커밋
			//dtResult.Columns.Add("SN_PARTNER_ST", typeof(string));
			dtResult.AcceptChanges();

			return dtResult;
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

		#region ==================================================================================================== 초기화

		/// <param name="날짜">YYYYMMDD 형식</param>
		/// <param name="통화">통화코드</param>
		/// <param name="구분">B:기준,S:매출,P:매입</param>
		/// <returns></returns>
		public static decimal 환율(string 날짜, object 통화, string 구분)
		{
			DataTable dt = SQL.GetDataTable("PS_CZ_MA_EXCHANGE_2", 회사코드, 날짜, 통화);

			if (구분 == "B") return dt.Rows[0]["RT_BASE"].ToDecimal();
			if (구분 == "S") return dt.Rows[0]["RT_SALES"].ToDecimal();
			if (구분 == "P") return dt.Rows[0]["RT_PURCHASE"].ToDecimal();

			return 1;
		}

		public static DataTable 환율(string 날짜)
		{
			DataTable dt = SQL.GetDataTable("PS_CZ_MA_EXCHANGES", 회사코드, 날짜);
			return dt;
		}

		#endregion ==================================================================================================== 초기화
	}
}
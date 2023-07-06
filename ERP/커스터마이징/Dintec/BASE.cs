using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Duzon.Common.Controls;
using Duzon.Common.Forms;

namespace Dintec
{
	public class BASE
	{
		public static bool IsAdmin(string empNumber)
		{
			string query = @"
SELECT
	1
FROM CZ_MA_CODEDTL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND CD_FIELD = 'CZ_MA00030'
	AND CD_SYSDEF = '" + empNumber + @"'
	AND YN_USE = 'Y'";

			DataTable dt = DBMgr.GetDataTable(query);
			return dt.Rows.Count == 1 ? true : false;
		}

		public static DataTable Company()
		{
			DBMgr dbm = new DBMgr();
			dbm.Query = @"
SELECT
	  CD_COMPANY
	, NM_COMPANY
FROM MA_COMPANY";

			return dbm.GetDataTable();
		}

		public static DataTable Employee(object empNumber)
		{
			DBMgr dbm = new DBMgr();
			dbm.Query = "SELECT * FROM V_CZ_MA_EMP WHERE CD_COMPANY = @CD_COMPANY AND NO_EMP = @NO_EMP";
			dbm.AddParameter("@CD_COMPANY", Global.MainFrame.LoginInfo.CompanyCode);
			dbm.AddParameter("@NO_PO", empNumber);

			return dbm.GetDataTable();
		}

		public static DataTable ExtraCost()
		{
			string query = @"
SELECT
	  CD_ITEM	AS CODE
	, NM_ITEM	AS NAME
	, UNIT_IM	AS UNIT
	, STND_ITEM	AS NO_PART
FROM MA_PITEM WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND CD_ITEM LIKE 'SD%'
	AND YN_USE = 'Y'";

			return DBMgr.GetDataTable(query);
		}

		public static DataTable Location()
		{
			string query = @"
SELECT
	  CD_LOCATION AS CODE
	, NM_LOCATION AS NAME
FROM MA_LOCATION
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND YN_USE = 'Y'";

			return DBMgr.GetDataTable(query);
		}

		public static DataTable Storage()
		{
			return Storage("");
		}

		public static DataTable Storage(string CD_SL)
		{
			string query = @"
SELECT
	  CD_SL
	, NM_SL
FROM MA_SL
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'";

			if (CD_SL != "")
				query += @"
	AND CD_SL = '" + CD_SL + "'";

			return DBMgr.GetDataTable(query);
		}

		public static DataTable IOType()
		{
			string query = @"
SELECT
	  CD_QTIOTP
	, NM_QTIOTP
FROM MM_EJTP
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'";

			return DBMgr.GetDataTable(query);
		}

		public static DataTable SalesGroup()
		{
			string query = @"
SELECT
	CD_SALEGRP	AS CODE
,	NM_SALEGRP	AS NAME
FROM MA_SALEGRP WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND USE_YN = 'Y'";

			return SQL.GetDataTable(query);
		}

		public static DataTable Code(string keyCode)
		{
			string query = Query(keyCode);
			DataTable dt = DBMgr.GetDataTable(query);

			return dt;
		}

		public static DataSet Code(params string[] keyCode)
		{
			string query = "";
			foreach (string s in keyCode) query += Query(s);
			DataSet ds = DBMgr.GetDataSet(query);

			return ds;
		}

		public static string Query(string keyCode)
		{
			string companyCode = Global.MainFrame.LoginInfo.CompanyCode;
			string language = Global.MainFrame.LoginInfo.Language;
			string query;

			if (keyCode == "GRP_ITEM")
			{
				string col = (language == "KR") ? "NM_ITEMGRP" : "EN_ITEMGRP";
				query = @"
SELECT
	CODE = CD_ITEMGRP
,	NAME = " + col + @"
FROM MA_ITEMGRP
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND USE_YN = 'Y'";
			}
			else if (keyCode == "YN_USE")
			{
				query = @"
SELECT 'Y' AS CODE, 'Y' AS NAME
UNION ALL
SELECT 'N' AS CODE, 'N' AS NAME";
			}
			else if (keyCode == "CD_WCODE")
			{
				query = @"
SELECT
	CODE = CD_WCODE
,	NAME = NM_WCODE
,	DY_WOCCUR
FROM HR_WCODE
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND CD_WTYPE = '001'
	AND YN_PROPOSAL = 'Y' 
	AND YN_USE = 'Y'";
			}			
			else if (keyCode == "CD_FILE")
			{
				query = @"
SELECT
	CODE = CD_FLAG3
,	NAME = CD_FLAG3
FROM V_CZ_MA_CODEDTL
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND CD_FIELD = 'CZ_SA00023'
	AND ISNULL(CD_FLAG3, '') != ''";
			}
			else
			{
				string col = (language == "KR") ? "NM_SYSDEF" : "ISNULL(NM_SYSDEF_E, NM_SYSDEF)";
				query = @"
SELECT
	CODE	= CD_SYSDEF
,	NAME	= " + col + @"
,	CD_FLAG1
,	CD_FLAG2
,	CD_FLAG3
FROM V_CZ_MA_CODEDTL
WHERE 1 = 1
	AND CD_COMPANY = '" + companyCode + @"'
	AND CD_FIELD = '" + keyCode + @"'";
			}

			return query;
		}


		public static string CodeFlag1(DropDownComboBox comboBox)
		{
			if (comboBox.SelectedIndex == -1) return "";

			return ((DataTable)comboBox.DataSource).Rows[comboBox.SelectedIndex]["CD_FLAG1"].ToString();
		}

		public static DataTable PartnerPic(string partnerCode, PicType type)
		{
			string partnerCond;
			string typeCode = "";

			// 거래처
			if (partnerCode.IndexOf(")") < 0)
				partnerCond = "= '" + partnerCode + "'";
			else
				partnerCond = "IN " + partnerCode;

			// 구분코드					
			if (type == PicType.SalesQuotation)
				typeCode = "'001','002'";
			else if (type == PicType.SalesOrder)
				typeCode = "'001','003'";
			else if (type == PicType.PurchaseQuotation)
				typeCode = "'004','005'";
			else if (type == PicType.PurchaseOrder)
				typeCode = "'004','006'";

			string query = @"
SELECT
	*
FROM FI_PARTNERPTR
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND CD_PARTNER " + partnerCond + @"
	AND (ISNULL(TP_PTR, '') = '' OR TP_PTR IN (" + typeCode + @"))
ORDER BY NM_PTR";

			return DBMgr.GetDataTable(query);
		}

		// 환율
		public static decimal ExchangeRate(object yymmdd, object currency, string mode)
		{
			DataTable dt = SQL.GetDataTable("PS_CZ_MA_EXCHANGE_R2", Global.MainFrame.LoginInfo.CompanyCode, yymmdd, currency);

			if (dt.Rows.Count == 1)
				return CT.Decimal(dt.Rows[0]["RT_EXCH_"] + mode);
			else
				return 1;
		}

		public static DataTable ExchangeRates(object yymmdd)
		{
			return SQL.GetDataTable("PS_CZ_MA_EXCHANGE_R2", Global.MainFrame.LoginInfo.CompanyCode, yymmdd);
		}

		public static DataTable StdExchangeePartner()
		{
			string query = @"
SELECT 
	CD_SYSDEF	AS CD_PARTNER
FROM CZ_MA_CODEDTL
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND CD_FIELD = 'CZ_SA00001'";

			return DBMgr.GetDataTable(query);
		}


		public static DataTable MailCodeGet()
		{
			string query = @"
SELECT 
	CD_SYSDEF AS CODE, NM_SYSDEF AS NAME
FROM CZ_MA_CODEDTL
WHERE 1 = 1
	--AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND CD_FIELD = 'CZ_MA00002'
	AND YN_USE = 'Y'
	ORDER BY CONVERT(INT, CD_FLAG4)";


			return DBMgr.GetDataTable(query);
		}

		public static DataTable ShipServCommentGet(string tnid)
		{
			string query = @"
SELECT 
	CD_SYSDEF
FROM CZ_MA_CODEDTL
WHERE 1 = 1
	--AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND CD_SYSDEF = '"+tnid+@"'
	AND CD_FIELD = 'CZ_MA00036'
	AND CD_FLAG2 = 'COMMENT'
	AND YN_USE = 'Y'";


			return DBMgr.GetDataTable(query);
		}


		public static string HiddenYn()
		{
			string query = @"
SELECT
	CD_EXC
FROM MA_EXC
WHERE 1 = 1
	AND CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND EXC_TITLE = 'ITEM HIDDEN 실행'";

			DataTable dt = DBMgr.GetDataTable(query);
			string hiddenYn = dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : "N";

			return hiddenYn;
		}

		public static DataTable Partner(string partnerCode)
		{
			string query = @"
SELECT
	  A.*
	, B.CD_EXCH1	AS CD_EXCH_S
	, B.CD_EXCH2	AS CD_EXCH_P
	, C.TP_SO
	, C.NM_SO
	, B.TP_VAT
	, B.FG_BILL1
	, B.FG_BILL2
	, D.CD_TPPO
	, D.NM_TPPO	
	, B.FG_PAYMENT
	, B.FG_TAX
FROM	  MA_PARTNER	AS A WITH(NOLOCK)
LEFT JOIN CZ_MA_PARTNER	AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_PARTNER = B.CD_PARTNER
LEFT JOIN SA_TPSO		AS C WITH(NOLOCK) ON B.CD_COMPANY = C.CD_COMPANY AND B.TP_SO = C.TP_SO
LEFT JOIN PU_TPPO		AS D WITH(NOLOCK) ON B.CD_COMPANY = D.CD_COMPANY AND B.CD_TPPO = D.CD_TPPO
WHERE 1 = 1
	AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";

			if (partnerCode.IndexOf(")") < 0) query += "\n" + "	AND A.CD_PARTNER = '" + partnerCode + "'";
			if (partnerCode.IndexOf(")") > 0) query += "\n" + "	AND A.CD_PARTNER IN " + partnerCode;

			return DBMgr.GetDataTable(query);
		}

		public static DataTable SalePurGroup(string empNumber)
		{
			string query = @"
SELECT
	  B.CD_SALEGRP
	, B.NM_SALEGRP
	, C.CD_PURGRP
	, C.NM_PURGRP
FROM MA_USER			AS A WITH(NOLOCK)
LEFT JOIN MA_SALEGRP	AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_SALEGRP = B.CD_SALEGRP
LEFT JOIN MA_PURGRP		AS C WITH(NOLOCK) ON A.CD_COMPANY = C.CD_COMPANY AND A.CD_PURGRP = C.CD_PURGRP	
WHERE 1 = 1
	AND A.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
	AND A.NO_EMP = '" + empNumber + "'";

			return DBMgr.GetDataTable(query);
		}

		public static DataTable JoinStockQuantity(string companyCode, DataTable dtItemList)
		{
			// 재고 가져오기
			DataTable dtQuantity = DBMgr.GetDataTable("PS_CZ_MM_STOCK_QT_R2", companyCode, GetTo.Xml(dtItemList, "", "CD_ITEM"));

			// 결과 테이블 생성 (구조만 복사)
			DataTable dtResult = dtItemList.Clone();

			// QT_BOOK 이 있는 경우는 가용재고만 조인
			if (dtResult.Columns.Contains("QT_BOOK"))
			{
				dtResult.Columns.Add("QT_INV", typeof(decimal));
				dtResult.Columns.Add("QT_AVL", typeof(decimal));
				dtResult.Columns.Add("QT_NGR", typeof(decimal));
				dtResult.Columns.Add("QT_NGR_AVL", typeof(decimal));
				dtResult.Columns.Add("UM_STK", typeof(decimal));
				dtResult.Columns.Add("SN_PARTNER_ST", typeof(string));

				// 쿼리문
				var query = from itemList in dtItemList.AsEnumerable()
							join quantity in dtQuantity.AsEnumerable()
								on new { key1 = itemList.Field<string>("CD_ITEM") } equals new { key1 = quantity.Field<string>("CD_ITEM") }
								into outer
							from result in outer.DefaultIfEmpty()
							select dtResult.LoadDataRow(itemList.ItemArray.Concat(new object[] {
								  (result == null) ? null : result["QT_INV"]
								, (result == null) ? null : result["QT_AVL"]
								, (result == null) ? null : result["QT_NGR"]
								, (result == null) ? null : result["QT_NGR_AVL"]
								, (result == null) ? null : result["UM_STK"]
								, (result == null) ? null : result["SN_PARTNER_ST"] }).ToArray<object>(), false);

				// 쿼리 실행
				query.Count();
			}
			else
			{
				dtResult.Columns.Add("QT_INV", typeof(decimal));
				dtResult.Columns.Add("QT_BOOK", typeof(decimal));
				dtResult.Columns.Add("QT_AVL", typeof(decimal));
				dtResult.Columns.Add("QT_NGR", typeof(decimal));
				dtResult.Columns.Add("QT_HOLD", typeof(decimal));
				dtResult.Columns.Add("QT_NGR_AVL", typeof(decimal));
				dtResult.Columns.Add("UM_STK", typeof(decimal));
				dtResult.Columns.Add("SN_PARTNER_ST", typeof(string));

				// 쿼리문
				var query = from itemList in dtItemList.AsEnumerable()
							join quantity in dtQuantity.AsEnumerable()
								on new { key1 = itemList.Field<string>("CD_ITEM") } equals new { key1 = quantity.Field<string>("CD_ITEM") }
								into outer
							from result in outer.DefaultIfEmpty()
							select dtResult.LoadDataRow(itemList.ItemArray.Concat(new object[] {
								  (result == null) ? null : result["QT_INV"]
								, (result == null) ? null : result["QT_BOOK"]
								, (result == null) ? null : result["QT_AVL"]
								, (result == null) ? null : result["QT_NGR"]
								, (result == null) ? null : result["QT_HOLD"]
								, (result == null) ? null : result["QT_NGR_AVL"]
								, (result == null) ? null : result["UM_STK"]
								, (result == null) ? null : result["SN_PARTNER_ST"] }).ToArray<object>(), false);

				// 쿼리 실행
				query.Count();
			}

			// 커밋
			dtResult.AcceptChanges();

			return dtResult;
		}

		public static DataTable JoinStockQuantityR2(string companyCode, DataTable dtItemList)
		{
			// 재고 가져오기
			DataTable dtQuantity = DBMgr.GetDataTable("PS_CZ_MM_STOCK_QT_R3", companyCode, GetTo.Xml(dtItemList, "", "CD_ITEM"));

			// 결과 테이블 생성 (구조만 복사)
			DataTable dtResult = dtItemList.Clone();

			dtResult.Columns.Add("QT_INV", typeof(decimal));
			dtResult.Columns.Add("QT_BOOK", typeof(decimal));
			dtResult.Columns.Add("QT_AVST", typeof(decimal));
			dtResult.Columns.Add("QT_NGR", typeof(decimal));
			dtResult.Columns.Add("QT_HOLD", typeof(decimal));
			dtResult.Columns.Add("QT_AVGR", typeof(decimal));
			dtResult.Columns.Add("UM_STK", typeof(decimal));
			dtResult.Columns.Add("SN_PARTNER_ST", typeof(string));

			// 쿼리문
			var query = from itemList in dtItemList.AsEnumerable()
						join quantity in dtQuantity.AsEnumerable()
							on new { key1 = itemList.Field<string>("CD_ITEM") } equals new { key1 = quantity.Field<string>("CD_ITEM") }
							into outer
						from result in outer.DefaultIfEmpty()
						select dtResult.LoadDataRow(itemList.ItemArray.Concat(new object[] {
							  (result == null) ? null : result["QT_INV"]
							, (result == null) ? null : result["QT_BOOK"]
							, (result == null) ? null : result["QT_AVST"]
							, (result == null) ? null : result["QT_NGR"]
							, (result == null) ? null : result["QT_HOLD"]
							, (result == null) ? null : result["QT_AVGR"]
							, (result == null) ? null : result["UM_STK"]
							, (result == null) ? null : result["SN_PARTNER_ST"] }).ToArray<object>(), false);

			// 쿼리 실행
			query.Count();

			// 커밋
			dtResult.AcceptChanges();

			return dtResult;
		}

		public static DataTable JoinStockQuantityR3(string companyCode, DataTable dtItemList)
		{
			// ********** 재고수량
			// Distinct된 재고코드만 가져오기
			DataTable dtStock = new DataTable();
			dtStock.Columns.Add("CD_COMPANY", typeof(string));
			dtStock.Columns.Add("CD_ITEM", typeof(string));

			// group by 쿼리문
			var q1 = from itemCode in dtItemList.AsEnumerable()
					 where !itemCode.IsNull("CD_ITEM") && itemCode.Field<string>("CD_ITEM") != ""
					 group itemCode by itemCode.Field<string>("CD_ITEM")
					 into g
					 select dtStock.LoadDataRow(new object[] { companyCode, g.Key }, false);

			// 실행
			q1.Count();

			// 사용자정의테이블형식 파라미터 선언
			SqlParameter param = new SqlParameter("@ITEM", SqlDbType.Structured);
			param.TypeName = "UT_CZ_ITEM";
			param.Value = dtStock;

			// 재고수량 가져오기
			DBMgr dbm = new DBMgr();
			dbm.Procedure = "PS_CZ_MM_STOCK_QT_R5";
			dbm.AddParameter(param);
			DataTable dtQuantity = dbm.GetDataTable();

			// ********** 최종 조인된 테이블 생성
			// 관련 필드가 이미 있을 경우는 삭제함 (삭제 안하면 컬럼 순서 꼬임)
			if (dtItemList.Columns.Contains("UM_STK"))
				dtItemList.Columns.Remove("UM_STK");

			// 구조만 복사해서 재고수량 관련 컬럼 추가
			DataTable dtResult = dtItemList.Clone();

			dtResult.Columns.Add("QT_INV", typeof(decimal));
			dtResult.Columns.Add("QT_BOOK", typeof(decimal));
			dtResult.Columns.Add("QT_AVST", typeof(decimal));
			dtResult.Columns.Add("QT_NGR", typeof(decimal));
			dtResult.Columns.Add("QT_HOLD", typeof(decimal));
			dtResult.Columns.Add("QT_AVGR", typeof(decimal));
			dtResult.Columns.Add("UM_STK", typeof(decimal));

			// 쿼리문
			var q2 = from itemList in dtItemList.AsEnumerable()
					 join quantity in dtQuantity.AsEnumerable() on new { key1 = itemList.Field<string>("CD_ITEM") } equals new { key1 = quantity.Field<string>("CD_ITEM") }
					 into outer
					 from result in outer.DefaultIfEmpty()
					 select dtResult.LoadDataRow(itemList.ItemArray.Concat(new object[] {
						  (result == null) ? null : result["QT_INV"]
						, (result == null) ? null : result["QT_BOOK"]
						, (result == null) ? null : result["QT_AVST"]
						, (result == null) ? null : result["QT_NGR"]
						, (result == null) ? null : result["QT_HOLD"]
						, (result == null) ? null : result["QT_AVGR"]
						, (result == null) ? null : result["UM_STK"] }).ToArray<object>(), false);

			// 쿼리 실행
			q2.Count();

			// 커밋
			dtResult.Columns.Add("SN_PARTNER_ST", typeof(string));

			dtResult.AcceptChanges();

			return dtResult;
		}

		public static DataTable JoinPartner(DataTable dtItemList)
		{
			string companyCode = "K100";

			// ********** 재고수량
			// Distinct된 거래처코드만 가져오기
			DataTable dtPartner = new DataTable();
			dtPartner.Columns.Add("CD_COMPANY", typeof(string));
			dtPartner.Columns.Add("CD_PARTNER", typeof(string));

			// group by 쿼리문
			var q1 = from itemCode in dtItemList.AsEnumerable()
					 where !itemCode.IsNull("CD_PARTNER") && itemCode.Field<string>("CD_PARTNER") != ""
					 group itemCode by itemCode.Field<string>("CD_PARTNER")
						 into g
					 select dtPartner.LoadDataRow(new object[] { companyCode, g.Key }, false);

			// 실행
			q1.Count();

			// 사용자정의테이블형식 파라미터 선언
			SqlParameter param = new SqlParameter("@MA_PARTNER_PK", SqlDbType.Structured);
			param.TypeName = "MA_PARTNER_PK";
			param.Value = dtPartner;

			// 재고수량 가져오기
			DBMgr dbm = new DBMgr();
			dbm.Procedure = "PS_CZ_MA_PARTNER_LINQ";
			dbm.AddParameter(param);
			DataTable dtQuantity = dbm.GetDataTable();

			// ********** 최종 조인된 테이블 생성
			// 구조만 복사해서 재고수량 관련 컬럼 추가
			DataTable dtResult = dtItemList.Clone();

			dtResult.Columns.Add("LN_PARTNER", typeof(string));

			// 쿼리문
			var q2 = from itemList in dtItemList.AsEnumerable()
					 join quantity in dtQuantity.AsEnumerable() on new { key1 = itemList.Field<string>("CD_PARTNER") } equals new { key1 = quantity.Field<string>("CD_PARTNER") }
					 into outer
					 from result in outer.DefaultIfEmpty()
					 select dtResult.LoadDataRow(itemList.ItemArray.Concat(new object[] {
						  (result == null) ? null : result["LN_PARTNER"] }).ToArray<object>(), false);

			// 쿼리 실행
			q2.Count();

			// 커밋

			dtResult.AcceptChanges();

			return dtResult;
		}




		public static string Today()
		{
			return Today(0);
		}

		public static string Today(int addedDay)
		{
			return string.Format("{0:yyyyMMdd}", Global.MainFrame.GetDateTimeToday().AddDays(addedDay));
		}
	}
}

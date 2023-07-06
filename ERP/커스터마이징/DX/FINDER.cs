using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using Duzon.Common.Forms;
using Dintec;
using System.Linq;
using System;

namespace DX
{
	public class FINDER
	{
		// 파일코드
		//List<string> fileCodes = new List<string>();
		//fileCodes.AddRange(new string[] { "FB", "DB", "NB", "SB", "NS" });  // 대표 파일
		//		fileCodes.AddRange(new string[] { "CL", "DS" });                    // 특별 케이스

		//public static void GetFileCodes


		public static void ETaxRelatedNumber(string taxNo)
		{			
			string comCd = Global.MainFrame.LoginInfo.CompanyCode;
			string query = @"
SELECT
	A.*
,	DC_RMK_ITEM	= ISNULL(B.DC_RMK, '')
FROM CZ_PU_ETAXH	AS A
LEFT JOIN
(
	SELECT
		CD_COMPANY
	,	NO_ETAX
	,	DC_RMK	= STRING_AGG(ISNULL(NM_PART, '') + ' ' + ISNULL(DC_RMK, ''), ' ')
	FROM CZ_PU_ETAXL
	GROUP BY CD_COMPANY, NO_ETAX

)					AS B ON A.CD_COMPANY = B.CD_COMPANY AND A.NO_ETAX = B.NO_ETAX
WHERE 1 = 1
	AND A.CD_COMPANY = '" + comCd + @"'
	AND A.NO_ETAX = '" + taxNo + "'";

			DataTable dt = SQL.GetDataTable(query);

			// ********** 파일번호 찾기 : C# 코드에서 찾음
			string text = (dt.Rows[0]["DC_RMK1"] + " " + dt.Rows[0]["DC_RMK2"] + " " + dt.Rows[0]["DC_RMK_ITEM"]).ToUpper();

			// 파일코드
			string fileCode = string.Join("|", CODE.FileCode());			
			string fileNo = string.Join(",", Regex.Matches(text, "(" + fileCode + ")" + @"2\d{5,7}-{0,1}\d{0,2}").Cast<Match>());

			// 파일번호 업데이트
			query = "UPDATE CZ_PU_ETAXH SET NO_FILE = '" + fileNo + "' WHERE CD_COMPANY = '" + comCd + "' AND NO_ETAX = '" + taxNo + "'";
			SQL.ExecuteNonQuery(query);

			// ********** 입고번호 찾기 : 프로시져로 찾음
			SQL.ExecuteNonQuery("PX_CZ_PU_ETAX_IO_FINDER", SQLDebug.Print, comCd, taxNo);
		}


		public static DataTable ETaxDetail(string partnerCode, string month, string fileName)
		{
			// 엑셀파일 읽기
			EXCEL excel = new EXCEL(fileName) { HeaderRowIndex = 0, StartDataIndex = 1, ToUpper = true };
			excel.Read();

			// 거래명세서 테이블 준비
			DataTable dt = new DataTable();
			dt.Columns.Add("SEQ"	, typeof(int));
			dt.Columns.Add("NO_FILE", typeof(string));
			dt.Columns.Add("AM"		, typeof(decimal));

			dt.Columns["SEQ"].AutoIncrement = true;
			dt.Columns["SEQ"].AutoIncrementSeed = 1;

			string fileCode = string.Join("|", CODE.FileCode());
			decimal amount = 0;
			string log = "";
			string miss = "";

			try
			{ 
				// ********** 컬럼헤드 찾기
				DataTable dtColHead = CODE.Code("CZ_DX00017");
				DataRow rowColHead = dtColHead.Select("CODE = '" + partnerCode + "'").GetFirstRow();

				string[] 파일번호;
				string[] 공급가액;
				string[] 부가세;
				string[] 합계;
				string[] 총합계;

				if (rowColHead == null)
				{
					foreach (DataRow row in dtColHead.Rows) row["CD_FLAG1"] = row["CD_FLAG1"].ToStr().Replace(" ", "");  // 공백제거

					파일번호 = dtColHead.Select("CODE = '001'")[0]["CD_FLAG1"].ToStr().Split(',');
					공급가액 = dtColHead.Select("CODE = '002'")[0]["CD_FLAG1"].ToStr().Split(',');
					부가세 = dtColHead.Select("CODE = '003'")[0]["CD_FLAG1"].ToStr().Split(',');
					합계 = new[] { "!@#" }; 
					총합계 = dtColHead.Select("CODE = '004'")[0]["CD_FLAG1"].ToStr().Split(',');
				}
				else
				{
					파일번호 = new[] { rowColHead["CD_FLAG1"].ToStr() };
					공급가액 = new[] { rowColHead["CD_FLAG2"].ToStr() };
					부가세 = new[] { rowColHead["CD_FLAG3"].ToStr() };
					합계 = new[] { rowColHead["CD_FLAG4"].ToStr() };
					총합계 = new[] { rowColHead["CD_FLAG5"].ToStr() };
				}

				int 헤드로우 = -1;
				int 합계로우 = -1;

				int 파일번호컬럼 = -1;
				int 공급가액컬럼 = -1;
				int 부가세컬럼 = -1;

				int 파일번호후보컬럼 = -1;
				int 공급가액후보컬럼 = -1;
				int 부가세후보컬럼 = -1;

				Regex rgxFile = new Regex("(" + fileCode + ")" + @"2\d{5,7}-{0,1}\d{0,2}");
				Regex rgxNumr = new Regex(@"^\d{3}");

				// ***** 헤드 찾기
				for (int i = 0; i < excel.Data.Rows.Count; i++)
				{
					for (int j = 0; j < excel.Data.Columns.Count; j++)
					{
						string data = excel.Data.Rows[i][j].ToStr().Replace(" ", "");
					
						// 미리 세팅된 컬럼명으로 찾기
						if (data.In(파일번호)) 파일번호컬럼 = j;
						if (data.In(공급가액)) 공급가액컬럼 = j;
						if (data.In(부가세))	  부가세컬럼 = j;

						// 세팅된 컬럼명으로 못찾을 경우 후보 찾기
						if (rgxFile.Match(data).Value != "")
							파일번호후보컬럼 = j;

						if (rgxNumr.Match(data).Value != "" && int.TryParse(data, out _) && data != "6048107065")
						{
							if (공급가액후보컬럼 == -1)
							{
								// 첫번째 숫자 등장이면 그냥 저장
								공급가액후보컬럼 = j;
							}
							else
							{
								// 두번째 숫자 등장이면 첫번째 날짜 형식인지 체크
								string prevData = excel.Data.Rows[i][공급가액후보컬럼].ToStr();

								if (prevData.Length == 8 && prevData.Substring(0, 3) == "202")
									공급가액후보컬럼 = j;
							}
						}
					}

					// 필수 컬럼 찾으면 종료
					if (파일번호컬럼 >= 0 && 공급가액컬럼 >= 0)
					{
						헤드로우 = i;
						break;
					}
					else if (파일번호후보컬럼 >= 0 && 공급가액후보컬럼 >= 0)
					{
						// 못찾았으면 후보군에서 찾아보기 (반드시 파일번호, 공급가액 다 찾았을 경우만)
						파일번호컬럼 = 파일번호컬럼 >= 0 ? 파일번호컬럼 : 파일번호후보컬럼;
						공급가액컬럼 = 공급가액컬럼 >= 0 ? 공급가액컬럼 : 공급가액후보컬럼;
						부가세컬럼 = 부가세컬럼 >= 0 ? 부가세컬럼 : 부가세후보컬럼;
						break;
					}
				
					파일번호후보컬럼 = -1;
					공급가액후보컬럼 = -1;
					부가세후보컬럼 = -1;
				}

				if (파일번호컬럼 + 공급가액컬럼 + 부가세컬럼 == -3)
				{
					log += ", 컬럼 찾기 실패";
				}

				// ***** 합계 찾기 (전체 합계)
				for (int i = 0; i < excel.Data.Rows.Count; i++)
				{
					if (i == 헤드로우) continue;

					for (int j = 0; j < excel.Data.Columns.Count; j++)
					{
						string data = excel.Data.Rows[i][j].ToStr().Replace(" ", "").Replace(" ", "").Replace(":", "").Split('(')[0];    // 두번째 공백은 띄어쓰기도 아닌것이 특수문자 인듯

						if (rowColHead != null)
							data = excel.Data.Rows[i][j].ToStr().Replace(" ", "");

						if (data.In(총합계))
						{
							합계로우 = i;
							break;
						}
					}

					if (합계로우 >= 0)
						break;
				}

				// ********** 찾은 행, 컬럼 값으로 값 넣기 ★★★★★
				int 이전로우 = -1;

				for (int i = 헤드로우 + 1; i < excel.Data.Rows.Count; i++)
				{					
					string 파일번호데이터 = excel.Data.Rows[i][파일번호컬럼].ToStr().Replace(" ", "");
					string 공급가액데이터 = excel.Data.Rows[i][공급가액컬럼].ToStr();

					if (rgxFile.Match(파일번호데이터).Value != "" && int.TryParse(공급가액데이터, out _))
					{
						DataRow newRow = dt.NewRow();
						newRow["NO_FILE"] = string.Join(",", rgxFile.Matches(파일번호데이터).Cast<Match>());
						newRow["AM"] = 공급가액데이터;
						dt.Rows.Add(newRow);

						// 행이 연속되는지 확인
						if (이전로우 >= 0 && 이전로우 + 1 != i)
							miss += "," + i;

						이전로우 = i;
					}
				}

				if (miss != "")
					log += ", 행 누락";

				// ********** 합계 일치하는지 확인
				bool 합계찾기여부 = false;
				bool 합계일치여부 = false;

				if (총합계[0] == "[+1]")
				{
					// 공급가액 마지막행 바로 아래에 총합계가 있음
					string 합계데이터 = excel.Data.Rows[이전로우 + 1][공급가액컬럼].ToStr();

					if (decimal.TryParse(합계데이터, out _))
					{
						amount = 합계데이터.ToDecimal();
						합계찾기여부 = true;						
						합계일치여부 = dt.Compute("SUM(AM)", "").ToDecimal() == amount;
					}
				}
				else if (합계로우 >= 0)
				{
					for (int j = 0; j < excel.Data.Columns.Count; j++)
					{
						string 합계데이터 = excel.Data.Rows[합계로우][j].ToStr();

						if (decimal.TryParse(합계데이터, out _))
						{
							합계찾기여부 = true;
							amount = 합계데이터.ToDecimal();

							if (dt.Compute("SUM(AM)", "").ToDecimal() == amount || dt.Compute("SUM(AM)", "").ToDecimal() * 1.1m == amount)
							{
								합계일치여부 = true;
								break;
							}
						}
					}									
				}

				if (합계찾기여부 && !합계일치여부)
					log += ", 합계금액 불일치";

				if (합계찾기여부 && 합계일치여부)
					log = log.Replace(", 행 누락", "");

				// ********** 최종
				if (dt.Rows.Count == 0)
					log += ", 파싱실패";
			}
			catch (Exception ex)
			{
				log += ", " + ex.Message;
			}

			if (log.StartsWith(", ")) log = log.Substring(2);
			if (miss.StartsWith(",")) miss = miss.Substring(1);

			// 저장
			SQL sql = new SQL("PX_CZ_PU_ETAX_DETAIL", SQLType.Procedure, SQLDebug.Print);
			sql.Parameter.Add2("@CD_COMPANY"	, Global.MainFrame.LoginInfo.CompanyCode);
			sql.Parameter.Add2("@CD_PARTNER"	, partnerCode);
			sql.Parameter.Add2("@DT_MONTH"		, month);
			sql.Parameter.Add2("@AM"			, amount);
			sql.Parameter.Add2("@MISS_ROW"		, miss);
			sql.Parameter.Add2("@DC_LOG"		, log);
			sql.Parameter.Add2("@YN_OVERWRITE"	, Global.MainFrame.LoginInfo.UserID == "S-343" ? "Y" : "N");
			sql.Parameter.Add2("@XML"			, dt.ToXml());
			sql.ExecuteNonQuery();

			return dt;
		}
	}
}

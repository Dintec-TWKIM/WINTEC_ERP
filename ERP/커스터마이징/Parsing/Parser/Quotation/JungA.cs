using Dintec;
using Dintec.Parser;

using System;
using System.Data;
using System.Text.RegularExpressions;

namespace Parsing
{
	class JungA
	{
		DataTable dtIteml;

		string lt;
		string rmk;
		string reference;

		string fileName;
		UnitConverter uc;

		#region ==================================================================================================== Property

		public DataTable ItemL
		{
			get
			{
				return dtIteml;
			}
		}

		public string Reference
		{
			get
			{
				return reference;
			}
		}

		public string Rmk
		{
			get
			{
				return rmk;
			}
		}

		public string Lt
		{
			get
			{
				return lt;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public JungA(string fileName)
		{
			lt = string.Empty;
			rmk = string.Empty;
			reference = string.Empty;

			dtIteml = new DataTable();
			dtIteml.Columns.Add("NO");          // 순번
			dtIteml.Columns.Add("DESC");        // 품목명
			dtIteml.Columns.Add("ITEM");        // 품목코드
			dtIteml.Columns.Add("UNIT");        // 단위
			dtIteml.Columns.Add("QT");          // 수량
			dtIteml.Columns.Add("UNIQ");          // 고유코드
			dtIteml.Columns.Add("UM");          // 단가
			dtIteml.Columns.Add("AM");          // 금액
			dtIteml.Columns.Add("LT");          // 납기
			dtIteml.Columns.Add("RMK");         // 비고

			this.fileName = fileName;
			this.uc = new UnitConverter();
		}

		#endregion

		#region ==================================================================================================== Logic

		public void Parse()
		{
			string iTemNo = string.Empty;
			string iTemUm = string.Empty;
			string iTemDC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;
			string iTemRMK = string.Empty;
			string iTemAm = string.Empty;
			string iTemDesc = string.Empty;
			string iTemTotal = string.Empty;
			string iTemCode = string.Empty;
			string iTemType = string.Empty;
			string iTemUniq = string.Empty;

			string iTemRef = string.Empty;
			string itemRMKH = string.Empty;
			string leadTimeStr = string.Empty;


			int _itemDesc = -1;
			int _itemQt = -1;
			int _itemUnit = -1;
			int _itemPrice = -1;
			int _itemValue = -1;


			string typeStr = string.Empty;
			string partStr = string.Empty;

			DataSet ds = ExcelReader.ToDataSet(fileName);


			DataTable dt = ds.Tables[0];    // 엑셀에서 1번 시트만 가져옴

			if (ds.Tables[0].Rows.Count > 5)
				dt = ds.Tables[0];  // 엑셀에서 1번 시트만 가져옴
			else
				dt = ds.Tables[1];  // 엑셀에서 1번 시트만 가져옴


			for (int i = 0; i < dt.Rows.Count; i++)
			{
				string firstColStr = dt.Rows[i][0].ToString();
				//string secondColStr = dt.Rows[i][1].ToString();


				//if (string.IsNullOrEmpty(reference))
				//reference = DateTime.Now.ToString("yyyy.MM.dd") + "(E-MAIL)";

				//if (string.IsNullOrEmpty(lt))
				//{
				//    for (int c = 1; c < dt.Columns.Count; c++)
				//    {
				//        //if (dt.Rows[i][c].ToString().Equals("Delivery Term:"))
				//        //{
				//        //    lt = dt.Rows[i][c + 1].ToString().Replace("DAYS", "").Replace("days", "").Replace("Days", "").Trim();
				//        //}

				//    }
				//}

				if (firstColStr.StartsWith("VALIDITY"))
				{
					lt = firstColStr.Replace("VALIDITY", "").Replace(":", "").Trim();

					if (lt.Contains("ONE MONTHS"))
					{
						lt = "30";
					}
				}
				else if (firstColStr.StartsWith("REFER NO"))
				{
					for (int c = 1; c < dt.Columns.Count; c++)
					{
						if (string.IsNullOrEmpty(reference))
							reference = dt.Rows[i][c].ToString().Trim();
					}
				}

				else if (firstColStr.StartsWith("MANUFACTURING PERIOD"))
				{
					for (int c = 1; c < dt.Columns.Count; c++)
					{
						// DAYS AFTER P/O
						if (string.IsNullOrEmpty(lt))
							lt = dt.Rows[i][c].ToString().ToUpper().Trim();
					}

					if (!string.IsNullOrEmpty(lt))
					{
						if (lt.Contains("DAY"))
							lt = Regex.Replace(lt, @"\D", "");
						else if (lt.Contains("WEEK"))
						{
							lt = Regex.Replace(lt, @"\D", "");
							int _lt = Convert.ToInt32(lt) * 7;
							lt = Convert.ToString(_lt);
						}
						else if (lt.Contains("ONE MONTH"))
						{
							lt = "30";
						}
						else if (lt.Contains("MONTH"))
						{
							lt = Regex.Replace(lt, @"\D", "");
							int _lt = Convert.ToInt32(lt) * 30;
							lt = Convert.ToString(_lt);
						}
					}
				}
				else if (firstColStr.StartsWith("Item"))
				{
					for (int c = 1; c < dt.Columns.Count; c++)
					{
						if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
						else if (dt.Rows[i][c].ToString().Contains("QTY")) _itemQt = c;
						else if (dt.Rows[i][c].ToString().Equals("Packing")) _itemUnit = c;
						else if (dt.Rows[i][c].ToString().Equals("Unit Price")) _itemPrice = c;
						else if (dt.Rows[i][c].ToString().Contains("Total")) _itemValue = c;
					}
				}
				else if (GetTo.IsInt(firstColStr))
				{
					if (!_itemQt.Equals(-1))
						iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

					if (!_itemUnit.Equals(-1))
						iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

					if (!_itemPrice.Equals(-1))
						iTemUm = dt.Rows[i][_itemPrice].ToString().Trim();

					if (!_itemDesc.Equals(-1))
						iTemDesc = dt.Rows[i][_itemDesc].ToString().Trim();


					if (string.IsNullOrEmpty(iTemDesc))
						break;


					//ITEM ADD START
					dtIteml.Rows.Add();
					dtIteml.Rows[dtIteml.Rows.Count - 1]["NO"] = firstColStr;
					dtIteml.Rows[dtIteml.Rows.Count - 1]["DESC"] = iTemDesc;
					dtIteml.Rows[dtIteml.Rows.Count - 1]["ITEM"] = typeStr;
					dtIteml.Rows[dtIteml.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
					dtIteml.Rows[dtIteml.Rows.Count - 1]["QT"] = iTemQt;
					dtIteml.Rows[dtIteml.Rows.Count - 1]["UNIQ"] = iTemCode;
					dtIteml.Rows[dtIteml.Rows.Count - 1]["UM"] = iTemUm;
					dtIteml.Rows[dtIteml.Rows.Count - 1]["AM"] = iTemAm;
					dtIteml.Rows[dtIteml.Rows.Count - 1]["LT"] = lt;
					dtIteml.Rows[dtIteml.Rows.Count - 1]["RMK"] = iTemRMK;

					iTemDC = string.Empty;
					iTemUnit = string.Empty;
					iTemRMK = string.Empty;
					iTemQt = string.Empty;
					iTemUm = string.Empty;
					iTemAm = string.Empty;
					iTemCode = string.Empty;
					iTemDesc = string.Empty;
				}

			}
		}

		#endregion
	}
}

using Dintec;
using Dintec.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parsing.Parser.Inquiry
{
	class HMM_SB_excel
	{
		string contact;
		string reference;
		string vessel;
		string imoNumber;
		DataTable dtItem;

		string fileName;
		UnitConverter uc;

		#region ==================================================================================================== Property

		public string Contact
		{
			get
			{
				return contact;
			}
		}

		public string Reference
		{
			get
			{
				return reference;
			}
		}

		public string ImoNumber
		{
			get
			{
				return imoNumber;
			}
		}

		public string Vessel
		{
			get
			{
				return vessel;
			}
		}

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public HMM_SB_excel(string fileName)
		{
			reference = string.Empty;
			vessel = string.Empty; ;
			imoNumber = string.Empty;

			dtItem = new DataTable();
			dtItem.Columns.Add("NO");
			dtItem.Columns.Add("SUBJ");
			dtItem.Columns.Add("ITEM");
			dtItem.Columns.Add("DESC");
			dtItem.Columns.Add("UNIT");
			dtItem.Columns.Add("QT");
			dtItem.Columns.Add("UNIQ");         //선사코드

			this.fileName = fileName;
			this.uc = new UnitConverter();
		}

		#endregion


		public void Parse()
		{
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;
			string iTemUniq = string.Empty;

			int _itemDesc = -1;
			int _itemQt = -1;
			int _itemVessel = -1;
			int _itemUnit = -1;
			int _itemSpec = -1;

			int _itemDrwing = -1;
			int _itemPart = -1;
			string reference2 = string.Empty;


			// 엑셀 읽기
			DataSet ds = ExcelReader.ToDataSet(fileName);
			DataTable dt = ds.Tables[0];    // 엑셀에서 1번 시트만 가져옴


			for (int i = 0; i < dt.Rows.Count; i++)
			{

				string firstColStr = dt.Rows[i][0].ToString().ToLower();
				string secondColStr = dt.Rows[i][2].ToString().ToLower();

				if (string.IsNullOrEmpty(reference))
				{
					for (int c = 1; c < dt.Columns.Count; c++)
					{
						if (dt.Rows[i][c].ToString().ToLower().Contains("rfq no"))
						{
							for (int cc = c; cc < dt.Columns.Count; cc++)
							{
								if (string.IsNullOrEmpty(reference))
									reference = dt.Rows[i][cc].ToString().Replace("RFQ No.", "").Trim();
							}
						}
					}
				}

				if (firstColStr.StartsWith("vessel name"))
				{
					for (int c = 1; c < dt.Columns.Count; c++)
					{
						if (string.IsNullOrEmpty(vessel))
							vessel = dt.Rows[i][c].ToString().Trim();
						else
							break;
					}
				}
				else if (firstColStr.StartsWith("no."))
				{
					for (int c = 0; c < dt.Columns.Count; c++)
					{
						if (dt.Rows[i][c].ToString().ToLower().StartsWith("drawing")) _itemDrwing = c;
						else if (dt.Rows[i][c].ToString().ToLower().StartsWith("part no")) _itemPart = c;
						else if (dt.Rows[i][c].ToString().ToLower().Equals("qty")) _itemQt = c;
						else if (dt.Rows[i][c].ToString().ToLower().Contains("mach. desc")) _itemDesc = c;
						else if (dt.Rows[i][c].ToString().ToLower().StartsWith("vessel name")) _itemVessel = c;
					}
				}
				else if (secondColStr.StartsWith("no."))
				{
					for (int c = 0; c < dt.Columns.Count; c++)
					{
						if (dt.Rows[i][c].ToString().ToLower().StartsWith("impa code")) _itemPart = c;
						else if (dt.Rows[i][c].ToString().ToLower().Equals("qty")) _itemQt = c;
						else if (dt.Rows[i][c].ToString().ToLower().Contains("description")) _itemDesc = c;
						else if (dt.Rows[i][c].ToString().ToLower().Contains("specfication")) _itemSpec = c;
						else if (dt.Rows[i][c].ToString().ToLower().Equals("unit")) _itemUnit = c;
					}
				}
				else if (GetTo.IsInt(secondColStr))
				{
					if (!_itemQt.Equals(-1))
						iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

					if (_itemUnit != -1)
						iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

					if (_itemPart != -1)
						iTemCode = dt.Rows[i][_itemPart].ToString().Trim();

					if (_itemDesc != -1)
					{
						for (int c = _itemDesc; c <= _itemSpec; c++)
						{
							iTemDESC = iTemDESC + " " + dt.Rows[i][c].ToString().Trim();
						}
					}




					iTemDESC = iTemDESC.Trim();
					iTemSUBJ = iTemSUBJ.Trim();


					dtItem.Rows.Add();
					dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
					if (!string.IsNullOrEmpty(iTemSUBJ))
						dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
					dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
					dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;
					dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
					if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
					dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);

					iTemDESC = string.Empty;
					iTemCode = string.Empty;
					iTemQt = string.Empty;
					iTemUnit = string.Empty;
					iTemSUBJ = string.Empty;
				}
			}
		}
	}
}

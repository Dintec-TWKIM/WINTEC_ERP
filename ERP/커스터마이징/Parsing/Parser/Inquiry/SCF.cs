using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class SCF
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

		public SCF(string fileName)
		{
			contact = "";
			reference = "";
			vessel = "";
			imoNumber = "";

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

		#region ==================================================================================================== Logic		

		public void Parse()
		{
			int _itemDesc = -1;
			int _itemCode = -1;
			int _itemType = -1;
			int _itemSerial = -1;
			int _itemUnit = -1;
			int _itemQt = -1;
			int _itemParts = -1;

			string iTemDesc = string.Empty;
			string iTemCode = string.Empty;
			string iTemQt = string.Empty;
			string iTemUnit = string.Empty;

			string subjStr = string.Empty;
			string subjStr2 = string.Empty;
			string typeStr = string.Empty;
			string serialStr = string.Empty;

			string descStr = string.Empty;

			bool subjCheck = false;

			// 엑셀 읽기
			DataSet ds = ExcelReader.ToDataSet(fileName);
			DataTable dt = ds.Tables[0];    // 엑셀에서 1번 시트만 가져옴

			// ********** 문서 검색 모드
			// 선명, 문의번호
			vessel = dt.Rows[5][3].ToString();
			reference = dt.Rows[4][6].ToString();

			string iTemSUBJ = string.Empty;
			string[] subjSplit = { };
			string iTemDWG = string.Empty;

			// ********** 아이템 추가 모드
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				string firstColString = dt.Rows[i][0].ToString();

				if (firstColString.Contains("SUBJECT:") && !subjCheck)
				{
					for (int k = 1; k < dt.Columns.Count; k++)
					{
						subjStr = subjStr.Trim() + dt.Rows[i][k].ToString().Trim();
					}
					subjCheck = true;
				}

				if (firstColString.Contains("Purchaser's remarks"))
				{
					for (int k = 1; k < dt.Columns.Count; k++)
					{
						subjStr2 = subjStr2.Trim() + dt.Rows[i][k].ToString().Trim();
					}

					if (string.IsNullOrEmpty(dt.Rows[i + 1][0].ToString()))
					{
						for (int k = 1; k < dt.Columns.Count; k++)
						{
							subjStr2 = subjStr2.Trim() + dt.Rows[i + 1][k].ToString().Trim();
						}
					}

				}

				if (firstColString.Contains("Pos"))
				{
					for (int c = 0; c < dt.Columns.Count; c++)
					{
						if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
						else if (dt.Rows[i][c].ToString().Contains("Impa code")) _itemCode = c;
						else if (dt.Rows[i][c].ToString().Contains("Parts Ref")) _itemParts = c;
						else if (dt.Rows[i][c].ToString().Contains("Serial")) _itemSerial = c;
						else if (dt.Rows[i][c].ToString().Contains("Q-ty")) _itemQt = c;
						else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
						else if (dt.Rows[i][c].ToString().Contains("Type")) _itemType = c;
					}
				}


				if (GetTo.IsInt(firstColString))
				{
					if (!_itemDesc.Equals(-1))
						iTemDesc = dt.Rows[i][_itemDesc].ToString().Trim();

					if (!_itemQt.Equals(-1))
						iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

					if (!_itemUnit.Equals(-1))
						iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

					if (!_itemCode.Equals(-1))
						iTemCode = dt.Rows[i][_itemCode].ToString().Replace("Parts list", "").Replace("ITEM NO", "").Replace(".", "").Trim();

					if (!_itemType.Equals(-1))
						typeStr = dt.Rows[i][_itemType].ToString().Trim();

					if (!_itemSerial.Equals(-1))
						serialStr = dt.Rows[i][_itemSerial].ToString().Replace("Serial no:", "").Trim();

					if (!_itemParts.Equals(-1))
					{
						if (dt.Rows[i][_itemParts].ToString().ToLower().Contains("rs stock no") || dt.Rows[i][_itemParts].ToString().ToLower().Contains("impa"))
							iTemCode = dt.Rows[i][_itemParts].ToString().Trim();
						else
							descStr = dt.Rows[i][_itemParts].ToString().Trim();
					}

					if (!string.IsNullOrEmpty(descStr))
						iTemDesc = iTemDesc.Trim() + Environment.NewLine + descStr;

					if (!string.IsNullOrEmpty(subjStr))
						iTemSUBJ = subjStr.Trim();

					if (!string.IsNullOrEmpty(serialStr))
						iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialStr.Trim();

					if (!string.IsNullOrEmpty(typeStr))
						iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

					if (!string.IsNullOrEmpty(subjStr2))
					{
						//int indexSubj2 = subjStr2.IndexOf("\r\n\r\n");

						//if (indexSubj2 > 0)
						//	subjStr2 = subjStr2.Substring(indexSubj2, subjStr2.Length - indexSubj2).Trim();

						iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr2.Trim();

					}


					//iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr2.Replace("Please quote for the following:", "").Trim();
					//iTemSUBJ = iTemSUBJ.Replace("\r\n\r\n", "\r\n").Trim();

					if(iTemSUBJ.Contains("\r\n "))
					{
						while(iTemSUBJ.Contains("\r\n "))
						{
							iTemSUBJ = iTemSUBJ.Replace("\r\n ", "\r\n").Trim();
						}
					}

					iTemCode = iTemCode.ToLower().Replace("rs", "").Replace("stock", "").Replace("no", "").Replace(":", "").Replace("maker", "").Replace("catalogue", "").Replace("impa", "").Replace("catalog","").Trim();

					dtItem.Rows.Add();
					dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColString;
					if (!string.IsNullOrEmpty(iTemSUBJ))
						dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
					dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
					dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDesc;
					dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
					if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;

					iTemDWG = string.Empty;
					iTemDesc = string.Empty;
					iTemQt = string.Empty;
					iTemUnit = string.Empty;
					descStr = string.Empty;

				}
			}
		}

		#endregion
	}
}

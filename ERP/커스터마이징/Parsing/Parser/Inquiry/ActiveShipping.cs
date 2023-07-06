using Dintec;
using Dintec.Parser;
using System;
using System.Data;
using System.Linq;

namespace Parsing
{
	internal class ActiveShipping
	{
		private string vessel;
		private string reference;
		private DataTable dtItem;

		private string fileName;
		private UnitConverter uc;

		#region ==================================================================================================== Property

		public string Vessel
		{
			get
			{
				return vessel;
			}
		}

		public string Reference
		{
			get
			{
				return reference;
			}
		}

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		#endregion ==================================================================================================== Property

		public ActiveShipping(string fileName)
		{
			vessel = "";                        // 선명
			reference = "";                     // 문의번호

			dtItem = new DataTable();
			dtItem.Columns.Add("NO");           // 순번
			dtItem.Columns.Add("SUBJ");         // 주제
			dtItem.Columns.Add("ITEM");         // 품목코드
			dtItem.Columns.Add("DESC");         // 품목명
			dtItem.Columns.Add("UNIT");         // 단위
			dtItem.Columns.Add("QT");           // 수량
			dtItem.Columns.Add("UNIQ");         // 선사
			this.fileName = fileName;
			this.uc = new UnitConverter();
		}

		public void Parse()
		{
			string iTemNo = string.Empty;
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;

			int _itemCatInt = -1;
			int _itemProNodInt = -1;
			int _itemProNmInt = -1;
			int _itemQtInt = -1;
			int _itemUnitInt = -1;
			int _itemRemarkInt = -1;
			int _itemCategoryInt = -1;

			bool forCheck = false;

			string subjStr = string.Empty;
			string subjRemarkStr = string.Empty;

			int _subjRemarkInt = -1;

			string itemRemarkStr = string.Empty;

			#region ########### READ ###########

			// Pdf를 Xml로 변환해서 분석 (1000$ 짜리로 해야함, 500$ 짜리로 하면 Description 부분에 CRLF가 안됨)
			// 1. 우선 500$ 짜리로 Xml 변환함 (1000$ 짜리의 경우 도면이 붙어 있으면 시간이 엄청 오래 걸림)
			string xmlTemp = PdfReader.ToXml(fileName);

			// 2. 도면을 제외한 Page 카운트 가져오기
			int pageCount = xmlTemp.Count("<page>");

			// 3. 앞서 나온 Page를 근거로 파싱 시작
			string xml = string.Empty;//PdfReader.GetXml(fileName, 1, pageCount);
			xml = PdfReader.GetXml(fileName);
			DataSet ds = PdfReader.ToDataSet(xml);

			//DataSet Table 병합을 위한 Table
			DataTable dsAll = new DataTable();

			//DataSet Table의 Count Get
			int dsCount = ds.Tables.Count;

			for (int i = 0; i <= dsCount - 1; i++)
			{
				dsAll.Merge(ds.Tables[i]);
			}

			ds.Clear();
			ds.Tables.Add(dsAll);

			#endregion ########### READ ###########

			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string firstColStr = dt.Rows[i][0].ToString();
					string secondColStr = dt.Rows[i][1].ToString();

					if (firstColStr.Contains("SHIP NAME"))
					{
						vessel = dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim() + dt.Rows[i][3].ToString().Trim();

						if (!string.IsNullOrEmpty(vessel))
						{
							string[] vesselSpl = vessel.Split(' ');

							vessel = vesselSpl[vesselSpl.Length - 1].ToString().Trim();
						}
					}
					else if (firstColStr.Contains("REQUISITION NO"))
					{
						reference = dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim() + dt.Rows[i][3].ToString().Trim();
					}
					else if (firstColStr.Contains("Report Date"))
					{
						// break;
					}
					else if (firstColStr.Contains("REQUISITION NAME"))
					{
						for (int c = 2; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("REMARKS")) _subjRemarkInt = c;
						}

						int _i = i;
						while (!dt.Rows[_i][1].ToString().Contains("CATALOGUE"))
						{
							if (!_subjRemarkInt.Equals(-1))
							{
								for (int c = 1; c < _subjRemarkInt; c++)
								{
									subjStr = subjStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
								}

								for (int c = _subjRemarkInt; c < dt.Columns.Count; c++)
								{
									subjRemarkStr = subjRemarkStr.Trim() + " " + dt.Rows[_i][c].ToString().Replace("REMARKS", "").Replace(":", "").Trim();
								}
							}
							else
							{
								for (int c = 1; c < 4; c++)
								{
									subjStr = subjStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
								}
							}
							_i += 1;
						}
					}
					else if (firstColStr.Contains("CATALOGUE"))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("CATALOGUE")) _itemCatInt = c;
							else if (dt.Rows[i][c].ToString().Contains("PRODUCT NO")) _itemProNodInt = c;
							else if (dt.Rows[i][c].ToString().Contains("PRODUCT NAME")) _itemProNmInt = c;
							else if (dt.Rows[i][c].ToString().Contains("REQUESTED QT")) _itemQtInt = c;
							else if (dt.Rows[i][c].ToString().Contains("UNIT")) _itemUnitInt = c;
							else if (dt.Rows[i][c].ToString().Contains("REMARK")) _itemRemarkInt = c;
							else if (dt.Rows[i][c].ToString().Contains("CATEGORY")) _itemCategoryInt = c;

							forCheck = true;
						}
					}
					else if (secondColStr.Contains("CATALOGUE"))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("CATALOGUE")) _itemCatInt = c;
							else if (dt.Rows[i][c].ToString().Contains("PRODUCT NO")) _itemProNodInt = c;
							else if (dt.Rows[i][c].ToString().Contains("PRODUCT NAME")) _itemProNmInt = c;
							else if (dt.Rows[i][c].ToString().Contains("REQUESTED QT")) _itemQtInt = c;
							else if (dt.Rows[i][c].ToString().Contains("UNIT")) _itemUnitInt = c;
							else if (dt.Rows[i][c].ToString().Contains("REMARK")) _itemRemarkInt = c;
							else if (dt.Rows[i][c].ToString().Contains("CATEGORY")) _itemCategoryInt = c;

							forCheck = true;
						}
					}
					else if (GetTo.IsInt(firstColStr))
					{
						if (!forCheck)
						{
							for (int c = 1; c < dt.Columns.Count; c++)
							{
								if (dt.Rows[i - 1][c].ToString().Contains("CATALOGUE")) _itemCatInt = c;
								else if (dt.Rows[i - 1][c].ToString().Contains("PRODUCT NO")) _itemProNodInt = c;
								else if (dt.Rows[i - 1][c].ToString().Contains("PRODUCT NAME")) _itemProNmInt = c;
								else if (dt.Rows[i - 1][c].ToString().Contains("REQUESTED QT")) _itemQtInt = c;
								else if (dt.Rows[i - 1][c].ToString().Contains("UNIT")) _itemUnitInt = c;
								else if (dt.Rows[i - 1][c].ToString().Contains("REMARK")) _itemRemarkInt = c;
								else if (dt.Rows[i - 1][c].ToString().Contains("CATEGORY")) _itemCategoryInt = c;

								forCheck = true;
							}
						}

						// row 값 가져와서 배열에 넣은후 값 추가하기
						string[] rowValueSpl = new string[20];
						int columnCount = 0;
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
							{
								rowValueSpl[columnCount] = c.ToString();
								columnCount++;
							}
						}

						// NO / CATALOGUE / PRODUCT NO / PRODUCT NAME / CATEGORY / ON STOCK / REQUESTED QTY / UNIT / REMARKS
						if ((rowValueSpl[6] != null) && rowValueSpl[7] == null)
						{
							//_itemCatInt = Convert.ToInt16(rowValueSpl[1].ToString());
							//_itemProNodInt = Convert.ToInt16(rowValueSpl[2].ToString());
							//_itemProNmInt = Convert.ToInt16(rowValueSpl[3].ToString());
							//_itemQtInt = Convert.ToInt16(rowValueSpl[5].ToString());
							//_itemUnitInt = Convert.ToInt16(rowValueSpl[6].ToString());

							//_itemCategoryInt = -1;
							//_itemRemarkInt = -1;
						}
						else if (rowValueSpl[5] != null && rowValueSpl[6] == null)
						{
						}

						//_itemProNmInt = 3;

						if (!_itemProNodInt.Equals(-1))
							iTemCode = dt.Rows[i][_itemProNodInt].ToString().Trim();

						if (!_itemQtInt.Equals(-1))
						{
							iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();

							if (iTemQt.Contains(",") || !string.IsNullOrEmpty(iTemQt))
							{
								string[] qt = iTemQt.Split(',');

								iTemQt = qt[0].ToString().Trim();
							}
						}

						if (!_itemProNmInt.Equals(-1))
							iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();

						if (!_itemProNmInt.Equals(-1))
						{
							if (!dt.Rows[i - 1][_itemProNmInt].ToString().Contains("PRODUCT NAME") && string.IsNullOrEmpty(dt.Rows[i - 1][0].ToString()))
							{
								iTemDESC = dt.Rows[i - 1][_itemProNmInt].ToString().Trim() + Environment.NewLine + dt.Rows[i][_itemProNmInt].ToString().Trim();

								if (string.IsNullOrEmpty(iTemDESC) || (!dt.Rows[i][1].ToString().Contains("CATALOG") && !dt.Rows[i - 2][1].ToString().Contains("CATALOG") && firstColStr.Equals("1")))
									iTemDESC = dt.Rows[i - 2][_itemProNmInt].ToString().Trim();
							}
							else
								iTemDESC = dt.Rows[i][_itemProNmInt].ToString().Trim();

							int _i = i + 1;
							while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && !GetTo.IsInt(dt.Rows[_i + 1][0].ToString()))
							{
								for (int c = _itemProNmInt; c < _itemCategoryInt; c++)
								{
									iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
								}
								iTemDESC = iTemDESC.Trim() + Environment.NewLine;

								_i += 1;
							}
							iTemDESC = iTemDESC.Trim();
						}

						if (!_itemRemarkInt.Equals(-1))
						{
							itemRemarkStr = dt.Rows[i][_itemRemarkInt].ToString().Trim();

							int _i = i + 1;
							while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
							{
								itemRemarkStr = itemRemarkStr.Trim() + " " + dt.Rows[_i][_itemRemarkInt].ToString().Trim();

								_i += 1;
							}
							itemRemarkStr = itemRemarkStr.Trim();
						}

						if (!string.IsNullOrEmpty(itemRemarkStr))
							iTemDESC = iTemDESC.Trim() + Environment.NewLine + itemRemarkStr.Trim();

						iTemSUBJ = subjStr.Trim();

						if (!string.IsNullOrEmpty(subjRemarkStr.Trim()))
							iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjRemarkStr.Trim();

						iTemSUBJ = iTemSUBJ.Trim();
						iTemDESC = iTemDESC.Trim();

						//ITEM ADD START
						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
						dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
						if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
						if (!string.IsNullOrEmpty(iTemSUBJ))
							dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
						dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

						iTemDESC = string.Empty;
						iTemUnit = string.Empty;
						iTemCode = string.Empty;
						iTemQt = string.Empty;
						iTemSUBJ = string.Empty;
					}
				}
			}
		}
	}
}
using Dintec;
using Dintec.Parser;
using System;
using System.Data;
using System.Linq;

namespace Parsing
{
	internal class Adnatco
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

		public Adnatco(string fileName)
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
			string iTemUniq = string.Empty;

			int _itemDesc = -1;
			int _itemQt = -1;
			int _itemProd = -1;
			int _itemCat = -1;
			int _itemCode = -1;

			string subjStr = string.Empty;

			string descStr = string.Empty;

			int _vesselColInt = -1;

			string[] vesselValue = null;

			bool subjCheck = false;

			string supllierStr = string.Empty;

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
					if (_vesselColInt.Equals(-1))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Equals("RFx"))
							{
								_vesselColInt = c;
							}
						}
					}

					if (!_vesselColInt.Equals(-1))
					{
						if (dt.Rows[i][_vesselColInt].ToString().Contains("Reference"))
						{
							string tempValue = dt.Rows[i][_vesselColInt + 1].ToString();

							if (string.IsNullOrEmpty(tempValue))
							{
								if (!dt.Rows[i][_vesselColInt].ToString().Contains("number:"))
									vesselValue = dt.Rows[i + 1][_vesselColInt].ToString().Split('-');
							}
							else
							{
								vesselValue = dt.Rows[i][_vesselColInt + 1].ToString().Split('-');
							}
						}
						else if (dt.Rows[i][_vesselColInt].ToString().Contains("number"))
						{
							reference = dt.Rows[i][_vesselColInt + 1].ToString().Trim();

							if (vesselValue != null && string.IsNullOrEmpty(vessel))
							{
								reference = reference + "-" + vesselValue[1].ToString();
								vessel = vesselValue[0].ToString().Trim();
							}
						}
					}

					string dataFirstValue = dt.Rows[i][0].ToString();

					if (dataFirstValue.Equals("Item"))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("Item")) _itemCat = c;
							else if (dt.Rows[i][c].ToString().Contains("category")) _itemCat = c;
							else if (dt.Rows[i][c].ToString().Contains("Product no.")) _itemProd = c;
							else if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
							else if (dt.Rows[i][c].ToString().Contains("Quantity")) _itemQt = c;
							else if (dt.Rows[i][c].ToString().Contains("Makers Ref")) _itemCode = c;
							else if (dt.Rows[i][c].ToString().Contains("number")) _itemCode = c;
						}
					}

					if (dataFirstValue.Contains("RFx Header text"))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
							subjStr = subjStr.Trim() + dt.Rows[i][c].ToString().Trim();

						int _i = i + 1;
						while (!dt.Rows[_i][0].ToString().Equals("Item"))
						{
							for (int c = 0; c < dt.Columns.Count; c++)
							{
								if (!dt.Rows[_i][c].ToString().Contains("Vessel Name:") && !subjCheck)
									subjStr = subjStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
								else
									subjCheck = true;
							}
							_i += 1;

							if (_i >= dt.Rows.Count)
								break;

							if (dt.Rows[_i][0].ToString().Equals("Item")) break;
						}

						subjStr = subjStr.Replace("Equipment:", "").Trim();
					}

					if (GetTo.IsInt(dataFirstValue))
					{
						if (!_itemDesc.Equals(-1))
						{
							iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

							if (i <= dt.Rows.Count - 2 && !GetTo.IsInt(dt.Rows[i + 1][0].ToString()))
							{
								iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i + 1][_itemDesc].ToString().Trim();
							}
						}
						//iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim() + " " + dt.Rows[i + 1][_itemDesc].ToString().Trim(); ;

						if (!_itemQt.Equals(-1))
						{
							if (_itemQt + 1 == dt.Columns.Count)
							{
								iTemQt = dt.Rows[i][_itemQt - 1].ToString().Trim();
								iTemUnit = dt.Rows[i][_itemQt].ToString().Trim();
							}
							else
							{
								iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
								iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Trim();
							}
						}

						if (!_itemProd.Equals(-1))
							iTemUniq = dt.Rows[i][_itemProd].ToString().Trim();

						if (!_itemCode.Equals(-1))
						{
							iTemCode = dt.Rows[i][_itemCode].ToString().Replace("'", "").Trim();

							if (i <= dt.Rows.Count - 2)
							{
								int _i = i + 1;
								while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
								{
									iTemCode = iTemCode.Trim() + dt.Rows[_i][_itemCode].ToString().Trim();

									_i += 1;

									if (_i >= dt.Rows.Count)
										break;
								}
							}
						}

						subjStr = subjStr.Trim();
						if (subjStr.StartsWith("-"))
							subjStr = subjStr.Substring(1, subjStr.Length - 1);

						if (!string.IsNullOrEmpty(subjStr))
							iTemSUBJ = subjStr.Replace("RFx details", "").Trim();

						if (i <= dt.Rows.Count - 2 && dt.Rows[i + 1][0].ToString().Contains("Supplier Item"))
						{
							supllierStr = string.Empty;

							int _i = i + 1;
							while (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && !GetTo.IsInt(dt.Rows[_i][0].ToString()) && _i < dt.Rows.Count - 2)
							{
								supllierStr = supllierStr.Trim() + " " + dt.Rows[_i][0].ToString().Trim();

								_i += 1;

								if (_i >= dt.Rows.Count)
									break;
							}

							if (!string.IsNullOrEmpty(supllierStr))
								iTemDESC = iTemDESC.Trim() + Environment.NewLine + supllierStr.Replace("Supplier Item Text:", "").Trim();
						}
						else if (i <= dt.Rows.Count - 3 && dt.Rows[i + 2][0].ToString().Contains("Supplier Item"))
						{
							supllierStr = string.Empty;

							int _i = i + 2;
							while (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && !GetTo.IsInt(dt.Rows[_i][0].ToString()) && _i < dt.Rows.Count - 2)
							{
								supllierStr = supllierStr.Trim() + " " + dt.Rows[_i][0].ToString().Trim();

								_i += 1;
								if (_i >= dt.Rows.Count)
									break;
							}

							if (!string.IsNullOrEmpty(supllierStr))
								iTemDESC = iTemDESC.Trim() + Environment.NewLine + supllierStr.Replace("Supplier Item Text:", "").Trim();
						}

						//ITEM ADD START
						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
						dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
						if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
						if (!string.IsNullOrEmpty(iTemSUBJ))
							dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
						dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;

						iTemUniq = string.Empty;
						iTemDESC = string.Empty;
						iTemQt = string.Empty;
						iTemUnit = string.Empty;
						iTemCode = string.Empty;

						subjCheck = false;
					}
				}
			}
		}
	}
}
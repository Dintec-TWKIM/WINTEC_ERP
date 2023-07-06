using Dintec;
using System;
using System.Data;
using System.Linq;

namespace Parsing
{
	internal class Doosan_1
	{
		private DataTable dtItem;

		private string fileName;

		#region ==================================================================================================== Property

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		#endregion ==================================================================================================== Property

		public Doosan_1(string fileName)
		{
			dtItem = new DataTable();
			dtItem.Columns.Add("CD");         //
			dtItem.Columns.Add("NM");         // 주제
			this.fileName = fileName;
		}

		public void Parse()
		{
			string iTemCode = string.Empty;
			string iTemName = string.Empty;

			int _itemCode = -1;
			int _itemName = -1;
			int _itemCode2 = -1;
			int _itemName2 = -1;

			string CodeStr = string.Empty;

			bool contiBool = false;

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

					if (firstColStr.Equals("Item") || secondColStr.Equals("Item"))
					{
						CodeStr = string.Empty;

						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (!string.IsNullOrEmpty(dt.Rows[i - 1][c].ToString()))
							{
								if ((GetTo.IsInt(dt.Rows[i - 1][c].ToString().Substring(1, 1)) && dt.Rows[i - 1][c].ToString().Contains("-")))
									CodeStr = CodeStr.Trim() + dt.Rows[i - 1][c].ToString().Trim();
							}
						}
					}
					else if (!string.IsNullOrEmpty(firstColStr))
					{
						if ((GetTo.IsInt(firstColStr.Substring(0, 1)) || GetTo.IsInt(secondColStr)))
						{
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

							if ((rowValueSpl[1] != null) && rowValueSpl[2] == null)
							{
								_itemCode = Convert.ToInt16(rowValueSpl[0].ToString());
								_itemName = Convert.ToInt16(rowValueSpl[1].ToString());
							}
							else if (rowValueSpl[3] != null && rowValueSpl[4] == null)
							{
								_itemCode = Convert.ToInt16(rowValueSpl[0].ToString());
								_itemName = Convert.ToInt16(rowValueSpl[1].ToString());
								_itemCode2 = Convert.ToInt16(rowValueSpl[2].ToString());
								_itemName2 = Convert.ToInt16(rowValueSpl[3].ToString());
							}
							else if (rowValueSpl[1] != null)
							{
								_itemCode = Convert.ToInt16(rowValueSpl[0].ToString());
								_itemName = Convert.ToInt16(rowValueSpl[1].ToString());
							}

							if (!_itemCode.Equals(-1))
								iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

							if (!_itemName.Equals(-1))
							{
								iTemName = dt.Rows[i][_itemName].ToString().Trim();

								int _i = i + 1;

								if (_itemName - 1 >= 0)
								{
									while (string.IsNullOrEmpty(dt.Rows[_i][_itemName - 1].ToString()) && !string.IsNullOrEmpty(dt.Rows[_i][_itemName].ToString()))
									{
										iTemName = iTemName.Trim() + " " + dt.Rows[_i][_itemName].ToString().Trim();
										_i += 1;
										if (_i >= dt.Rows.Count)
											break;
									}
								}
							}

							iTemCode = CodeStr.Trim() + "-" + iTemCode.Trim();

							if (!_itemCode2.Equals(-1))
							{
								iTemCode = dt.Rows[i][_itemCode2].ToString().Trim();

								if (!GetTo.IsInt(iTemCode))
									contiBool = true;
							}

							if (!string.IsNullOrEmpty(iTemName) && !contiBool)
							{
								if (iTemCode.Contains(" "))
								{
									int idx_e = iTemCode.IndexOf(" ");
									iTemCode = iTemCode.Substring(0, idx_e).Trim();
								}

								//ITEM ADD START
								dtItem.Rows.Add();
								dtItem.Rows[dtItem.Rows.Count - 1]["CD"] = iTemCode;
								dtItem.Rows[dtItem.Rows.Count - 1]["NM"] = iTemName;

								if (!_itemCode2.Equals(-1))
								{
									iTemCode = dt.Rows[i][_itemCode2].ToString().Trim();

									if (!_itemName2.Equals(-1))
									{
										iTemName = dt.Rows[i][_itemName2].ToString().Trim();

										int _i = i + 1;

										if (_itemName2 - 1 >= 0)
										{
											while (string.IsNullOrEmpty(dt.Rows[_i][_itemName2 - 1].ToString()) && !string.IsNullOrEmpty(dt.Rows[_i][_itemName2].ToString()))
											{
												iTemName = iTemName.Trim() + " " + dt.Rows[_i][_itemName2].ToString().Trim();
												_i += 1;
												if (_i >= dt.Rows.Count)
													break;
											}
										}
									}

									iTemCode = CodeStr.Trim() + "-" + iTemCode.Trim();

									if (!string.IsNullOrEmpty(iTemName))
									{
										if (iTemCode.Contains(" "))
										{
											int idx_e = iTemCode.IndexOf(" ");
											iTemCode = iTemCode.Substring(0, idx_e).Trim();
										}

										dtItem.Rows.Add();
										dtItem.Rows[dtItem.Rows.Count - 1]["CD"] = iTemCode;
										dtItem.Rows[dtItem.Rows.Count - 1]["NM"] = iTemName;
									}
								}
							}
							else if (contiBool)
							{
								//iTemName = iTemName.Trim() + " " + dt.Rows[i][_itemCode2].ToString().Trim();
								iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

								iTemCode = CodeStr.Trim() + "-" + iTemCode.Trim();

								if (!_itemName2.Equals(-1))
								{
									iTemName = iTemName.Trim() + " " + dt.Rows[i][_itemName2].ToString().Trim();

									int _i = i + 1;

									if (_itemName2 - 1 >= 0)
									{
										while (string.IsNullOrEmpty(dt.Rows[_i][_itemName2 - 1].ToString()) && !string.IsNullOrEmpty(dt.Rows[_i][_itemName2].ToString()))
										{
											iTemName = iTemName.Trim() + " " + dt.Rows[_i][_itemName2].ToString().Trim();
											_i += 1;
											if (_i >= dt.Rows.Count)
												break;
										}
									}
								}

								if (!string.IsNullOrEmpty(iTemName))
								{
									if (iTemCode.Contains(" "))
									{
										int idx_e = iTemCode.IndexOf(" ");
										iTemCode = iTemCode.Substring(0, idx_e).Trim();
									}

									dtItem.Rows.Add();
									dtItem.Rows[dtItem.Rows.Count - 1]["CD"] = iTemCode;
									dtItem.Rows[dtItem.Rows.Count - 1]["NM"] = iTemName;
								}

								contiBool = false;
							}

							iTemCode = string.Empty;
							iTemName = string.Empty;
						}
					}
				}
			}
		}
	}
}
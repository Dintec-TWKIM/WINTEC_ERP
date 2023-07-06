using Dintec;
using Dintec.Parser;
using System;
using System.Data;
using System.Linq;

namespace Parsing
{
	class Bernhard_UK_pdf
	{
		string vessel;
		string reference;
		DataTable dtItem;

		string fileName;
		UnitConverter uc;



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

		#endregion



		#region ==================================================================================================== Constructor

		public Bernhard_UK_pdf(string fileName)
		{
			vessel = "";                        // 선명
			reference = "";                     // 문의번호

			dtItem = new DataTable();
			dtItem.Columns.Add("NO");           // 아이템 순번
			dtItem.Columns.Add("SUBJ");         // 주제
			dtItem.Columns.Add("ITEM");         // 품목코드
			dtItem.Columns.Add("DESC");         // 품목명
			dtItem.Columns.Add("UNIT");         // 단위
			dtItem.Columns.Add("QT");           // 수량
			dtItem.Columns.Add("UNIQ");         // 선사코드
			this.fileName = fileName;
			this.uc = new UnitConverter();
		}

		#endregion


		public void Parse()
		{
			string iTemNo = string.Empty;
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;
			string iTemUniq = string.Empty;
			string itemRmk = string.Empty;
			string itemPart = string.Empty;
			string itemDwg = string.Empty;

			int _itemCode = -1;
			int _itemDescription = -1;
			int _itemDrw = -1;
			int _itemPart = -1;
			int _itemUnit = -1;
			int _itemQt = -1;
			int _itemRemark = -1;

			string equpmentStr = string.Empty;
			string modelStr = string.Empty;
			string makerStr = string.Empty;
			string drawingStr = string.Empty;
			string serialStr = string.Empty;
			string partstring = string.Empty;
			string subjectStr = string.Empty;

			string endRemarkString = string.Empty;

			bool subjStatus = false;

			bool sbCheck = false;


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


			// 시작
			bool itemStart = false;

			string[] itemName = { };

			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					try
					{
						string firstColString = dt.Rows[i][0].ToString();
						string secondColStr = dt.Rows[i][1].ToString();


						if (firstColString.ToLower().StartsWith("item details") || secondColStr.ToLower().StartsWith("item details")) itemStart = true;

						if (string.IsNullOrEmpty(vessel))
						{
							for (int c = 0; c < dt.Columns.Count; c++)
							{
								if (dt.Rows[i][c].ToString().Contains("Vessel"))
									vessel = dt.Rows[i][c].ToString().Trim() + dt.Rows[i][c + 1].ToString().Trim() + dt.Rows[i][c + 2].ToString().Trim();
							}

							vessel = vessel.ToLower().Replace("vessel", "").Replace("name", "").Trim();
						}

						if (string.IsNullOrEmpty(reference))
						{
							for (int c = 0; c < dt.Columns.Count; c++)
							{
								if (dt.Rows[i][c].ToString().Contains("Requisition No") || dt.Rows[i][c].ToString().StartsWith("Enquiry Number"))
									reference = dt.Rows[i][c].ToString().Trim() + dt.Rows[i][c + 1].ToString().Trim() + dt.Rows[i][c + 2].ToString().Trim();
							}

							reference = reference.ToLower().Replace("enquiry", "").Replace("number", "").Replace("requisition", "").Replace("no", "").Trim();
						}



						if (string.IsNullOrEmpty(subjectStr))
						{
							for (int c = 0; c < dt.Columns.Count; c++)
							{
								if (dt.Rows[i][c].ToString().Contains("Title"))
								{
									for (int col = c; col < dt.Columns.Count; col++)
									{
										if (dt.Rows[i][col].ToString().ToLower().Contains("delivery")) break;

										subjectStr = subjectStr.Trim() + " " + dt.Rows[i][col].ToString().Trim();
									}

									if (string.IsNullOrEmpty(dt.Rows[i + 1][c].ToString()))
									{
										for (int col = c; col < dt.Columns.Count; col++)
										{
											if (dt.Rows[i + 1][col].ToString().ToLower().Contains("delivery")) break;

											subjectStr = subjectStr.Trim() + " " + dt.Rows[i + 1][col].ToString().Trim();
										}
									}
								}
							}
						}


						if (firstColString.StartsWith("S.No"))
						{
							//for (int c = 0; c < dt.Columns.Count; c++)
							//{
							//	//if (dt.Rows[i][c].ToString() == "Item Code") _itemCode = c;                          //선사코드
							//	//else if (dt.Rows[i][c].ToString() == "Item Description") _itemDescription = c;       //품목명
							//	//else if (dt.Rows[i][c].ToString().Contains("Drawing N")) _itemDrw = c;                 //DWG NO
							//	//else if (dt.Rows[i][c].ToString() == "Part Number") _itemPart = c;                   //품목코드
							//	//else if (dt.Rows[i][c].ToString() == "Unit" || dt.Rows[i][c].ToString().Equals("UOM")) _itemUnit = c;                          //단위
							//	//else if (dt.Rows[i][c].ToString() == "Quantity") _itemQt = c;                    //수량
							//	//else if (dt.Rows[i][c].ToString() == "Remarks") _itemRemark = c;                     //비고
							//}
						}
						else if ((GetTo.IsInt(firstColString) || GetTo.IsInt(secondColStr)) && itemStart)
						{
							int _c = 0;

							if (GetTo.IsInt(firstColString))
							{
								_c = 0;
							}
							else if (GetTo.IsInt(secondColStr))
							{
								_c = 1;
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


							if (rowValueSpl[7] != null && rowValueSpl[8] == null)
							{
								_itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
								_itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
								_itemDrw = Convert.ToInt16(rowValueSpl[3].ToString());
								_itemPart = Convert.ToInt16(rowValueSpl[3].ToString());
								_itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
								_itemQt = Convert.ToInt16(rowValueSpl[5].ToString());

								iTemQt = dt.Rows[i][_itemQt].ToString();

								if (!GetTo.IsInt(iTemQt))
								{
									_itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
									_itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
									_itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
									_itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
									_itemRemark = Convert.ToInt16(rowValueSpl[7].ToString());

									if (dt.Rows[i][_itemRemark].ToString().Equals("No"))
									{
										_itemPart = Convert.ToInt16(rowValueSpl[3].ToString());
										_itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
										_itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
										_itemDrw = -1;
									}
									else
									{
										_itemPart = -1;
										_itemDrw = -1;
									}
								}
							}
							else if (rowValueSpl[6] != null && rowValueSpl[7] == null)
							{
								_itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
								_itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
								_itemDrw = Convert.ToInt16(rowValueSpl[3].ToString());
								_itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
								_itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
								_itemRemark = Convert.ToInt16(rowValueSpl[6].ToString());
								
							}
							else if (rowValueSpl[5] != null && rowValueSpl[6] == null)
							{
								_itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
								_itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
								_itemPart = Convert.ToInt16(rowValueSpl[3].ToString());
								_itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
								_itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
							}
							else if (rowValueSpl[8] != null && rowValueSpl[9] == null)
							{
								_itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
								_itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
								_itemPart = Convert.ToInt16(rowValueSpl[3].ToString());
								_itemDrw = Convert.ToInt16(rowValueSpl[4].ToString());
								_itemUnit = Convert.ToInt16(rowValueSpl[5].ToString());
								_itemQt = Convert.ToInt16(rowValueSpl[6].ToString());
							}



							if (_itemUnit != -1)
								iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

							if (_itemQt != -1)
								iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

							// itemcode 안씀!!!
							//if (_itemCode != -1)
							//{
							//	iTemCode = dt.Rows[i][_itemCode].ToString().Trim();
							//}

							if (_itemPart != -1)
								itemPart = dt.Rows[i][_itemPart].ToString().Trim();

							if (_itemDrw != -1)
								itemDwg = dt.Rows[i][_itemDrw].ToString().Trim();

							if (_itemRemark != -1)
							{
								itemRmk = dt.Rows[i][_itemRemark].ToString().Trim();

								int _i = i + 1;

								while (string.IsNullOrEmpty(dt.Rows[_i][_c].ToString()))
								{
									itemRmk = itemRmk.Trim() + " " + dt.Rows[_i][_itemRemark].ToString().Trim();


									if (itemRmk.Length >= 6 && itemRmk.Contains(" of "))
									{
										itemRmk = string.Empty;
									}

									_i += 1;

									if (_i >= dt.Rows.Count)
										break;
								}

							}

							if (_itemDescription != -1)
							{
								iTemDESC = dt.Rows[i][_itemDescription].ToString().Trim();

								int _i = i + 1;

								while (string.IsNullOrEmpty(dt.Rows[_i][_c].ToString()))
								{
									// equipment 부분
									if (dt.Rows[_i][_itemDescription].ToString().ToLower().Contains("equipment name"))
									{
										int _r = _i + 1;

										// name 부터..
										while (string.IsNullOrEmpty(dt.Rows[_r][_c].ToString()))
										{
											equpmentStr = equpmentStr.Trim() + " " + dt.Rows[_r][_itemDescription].ToString().Trim();

											_r += 1;

											if (_r >= dt.Rows.Count)
												break;
										}

										// 각 컬럼마다 추가하기
										for (int c = _itemDescription; c < dt.Columns.Count; c++)
										{
											if (dt.Rows[_i][c].ToString().ToLower().StartsWith("model"))
											{
												int _modelr = _i + 1;

												// name 부터..
												while (string.IsNullOrEmpty(dt.Rows[_modelr][_c].ToString()))
												{
													modelStr = modelStr.Trim() + " " + dt.Rows[_modelr][c].ToString().Trim();

													_modelr += 1;

													if (_modelr >= dt.Rows.Count)
														break;
												}
											}
											else if (dt.Rows[_i][c].ToString().ToLower().StartsWith("maker"))
											{
												int _makerr = _i + 1;

												while (string.IsNullOrEmpty(dt.Rows[_makerr][0].ToString()))
												{
													makerStr = makerStr.Trim() + " " + dt.Rows[_makerr][c].ToString().Trim();

													_makerr += 1;

													if (_makerr >= dt.Rows.Count)
														break;
												}
											}
											else if (dt.Rows[_i][c].ToString().ToLower().StartsWith("drawing"))
											{
												int _drawingr = _i + 1;

												while (string.IsNullOrEmpty(dt.Rows[_drawingr][_c].ToString()))
												{
													drawingStr = drawingStr.Trim() + " " + dt.Rows[_drawingr][c].ToString().Trim();

													_drawingr += 1;

													if (_drawingr >= dt.Rows.Count)
														break;
												}
											}
											else if (dt.Rows[_i][c].ToString().ToLower().StartsWith("serial"))
											{
												int _serialr = _i + 1;

												while (string.IsNullOrEmpty(dt.Rows[_serialr][_c].ToString()))
												{
													serialStr = serialStr.Trim() + " " + dt.Rows[_serialr][c].ToString().Trim();

													_serialr += 1;

													if (_serialr >= dt.Rows.Count)
														break;
												}
											}
											else if (dt.Rows[_i][c].ToString().ToLower().StartsWith("part number"))
											{
												int _partr = _i + 1;

												while (string.IsNullOrEmpty(dt.Rows[_partr][_c].ToString()))
												{
													partstring = partstring.Trim() + " " + dt.Rows[_partr][c].ToString().Trim();

													_partr = +1;

													if (_partr >= dt.Rows.Count)
														break;
												}
											}
										}

										break;
									}

									iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][_itemDescription].ToString().Trim();

									_i += 1;

									if (_i >= dt.Rows.Count)
										break;
								}

							}



							if (!string.IsNullOrEmpty(itemRmk))
							{
								iTemDESC = iTemDESC.Trim() + Environment.NewLine + itemRmk;
							}

							if (!string.IsNullOrEmpty(partstring))
								iTemCode = partstring;
							else
								iTemCode = itemPart;



							if (string.IsNullOrEmpty(equpmentStr))
								iTemSUBJ = subjectStr.Trim();
							else
								iTemSUBJ = equpmentStr.Trim();

							makerStr = makerStr.Trim();
							modelStr = modelStr.Trim();


							if (!string.IsNullOrEmpty(makerStr.Trim()))
								iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr;

							if (!string.IsNullOrEmpty(modelStr.Trim()))
								iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr;

							if (!string.IsNullOrEmpty(drawingStr.Trim()))
								iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG.NO: " + drawingStr;
							else if (!string.IsNullOrEmpty(itemDwg.Trim()))
								iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG.NO: " + itemDwg;

							if (!string.IsNullOrEmpty(serialStr.Trim()))
								iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr;

							iTemSUBJ = iTemSUBJ.Replace("See attached files.", "");
							iTemSUBJ = iTemSUBJ.Replace("Need for stock replenish.", "");
							iTemSUBJ = iTemSUBJ.Replace("see the attached files", "");


							dtItem.Rows.Add();
							dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColString;

							if (!string.IsNullOrEmpty(iTemSUBJ))
							{
								if (!(iTemSUBJ.ToUpper().IndexOf("FOR") == 0))
									dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
								else
									dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
							}

							dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode.Trim();
							dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;// 품목명
							dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);//PCS 안됨
							if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
							dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = "";

							iTemSUBJ = string.Empty;
							iTemCode = string.Empty;
							iTemDESC = string.Empty;
							iTemUnit = string.Empty;
							iTemQt = string.Empty;
							itemDwg = string.Empty;

							makerStr = string.Empty;
							modelStr = string.Empty;
							equpmentStr = string.Empty;
							drawingStr = string.Empty;
							partstring = string.Empty;
							serialStr = string.Empty;
						}
						else if (firstColString.ToLower().StartsWith("remarks to vendor"))
						{
							for (int c = 0; c < 4; c++)
							{
								endRemarkString = endRemarkString.Trim() + " " + dt.Rows[i][c].ToString().Trim();
							}

							int _i = i + 1;

							while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
							{
								for (int c = 0; c < 4; c++)
								{
									endRemarkString = endRemarkString.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
								}

								_i += 1;

								if (_i >= dt.Rows.Count)
									break;
							}

							if (!string.IsNullOrEmpty(endRemarkString))
							{
								endRemarkString = endRemarkString.ToLower().Replace("remarks to vendor :", "").Trim();
							}


							for (int _c = 0; _c < dtItem.Rows.Count; _c++)
							{
								dtItem.Rows[_c]["SUBJ"] = dtItem.Rows[_c]["SUBJ"] + Environment.NewLine + endRemarkString;
							}
						}



					}
					catch (Exception ex)
					{
						//Util.ShowMessage(Util.GetErrorMessage(ex.Message));
					}
				}
			}

		}
	}
}



using Dintec;
using System;
using System.Data;

namespace Parsing
{
	internal class sangsangin_1
	{
		private DataTable dtItem;
		private DataTable dtItemAll;

		private string fileName;
		private string selectValue;
		private int pageNo;

		private int _itemDesc = -1;
		private int _itemMalt = -1;
		private int _itemQt = -1;
		private int _itemWt = -1;
		private int _itemRmk = -1;
		private int _itemNo = -1;
		private int _itemSize = -1;
		private int _itemSymbol = -1;
		private int _itemSpec = -1;
		private int _itemModel = -1;
		private int _itemMaker = -1;
		private int _itemCode = -1;
		private int _itemLoc = -1;
		private int _itemDwg = -1;

		private int _itemDesc2 = -1;
		private int _itemMalt2 = -1;
		private int _itemQt2 = -1;
		private int _itemWt2 = -1;
		private int _itemRmk2 = -1;
		private int _itemNo2 = -1;
		private int _itemSize2 = -1;
		private int _itemSymbol2 = -1;
		private int _itemSpec2 = -1;
		private int _itemModel2 = -1;
		private int _itemMaker2 = -1;
		private int _itemCode2 = -1;
		private int _itemLoc2 = -1;
		private int _itemDwg2 = -1;

		private string itemDesc = string.Empty;
		private string itemMalt = string.Empty;
		private string itemQt = string.Empty;
		private string itemWt = string.Empty;
		private string itemRmk = string.Empty;
		private string itemSize = string.Empty;
		private string itemSymbol = string.Empty;
		private string itemSpec = string.Empty;
		private string itemModel = string.Empty;
		private string itemMaker = string.Empty;
		private string itemCode = string.Empty;
		private string itemLoc = string.Empty;
		private string itemDwg = string.Empty;
		private string itemNo = string.Empty;

		#region ==================================================================================================== Property

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		public DataTable ItemAll
		{
			get
			{
				return dtItemAll;
			}
		}

		#endregion ==================================================================================================== Property

		public sangsangin_1(string fileName, int pageNo)
		{
			dtItem = new DataTable();
			dtItem.Columns.Add("PAGENO");
			dtItem.Columns.Add("SHIPYARD");
			dtItem.Columns.Add("PROJECT");
			dtItem.Columns.Add("TITLE");
			dtItem.Columns.Add("HULLNO");
			dtItem.Columns.Add("DESC");
			dtItem.Columns.Add("MALT");
			dtItem.Columns.Add("QT");
			dtItem.Columns.Add("WT");
			dtItem.Columns.Add("RMK");
			dtItem.Columns.Add("DWGNO");
			dtItem.Columns.Add("SIZE");
			dtItem.Columns.Add("SYMBOL");
			dtItem.Columns.Add("CODE");
			dtItem.Columns.Add("SPECIFICATION");
			dtItem.Columns.Add("MODEL");
			dtItem.Columns.Add("MAKER");
			dtItem.Columns.Add("LOC");
			dtItem.Columns.Add("NO");
			this.fileName = fileName;
			this.pageNo = pageNo;

			// 전체 데이터 테이블 표시 하기
			dtItemAll = new DataTable();
		}

		public void Parse()
		{
			int _itemStartSig = -1;
			int _itemEnd = -1;
			string itemUpDown = string.Empty;
			string symbolSig = string.Empty;

			string selectValue = string.Empty;

			string dwgNo = string.Empty;

			string parsingCategory = string.Empty;

			int _firstColInt = 0;

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
			//DataTable dsAll = new DataTable();

			////DataSet Table의 Count Get
			//int dsCount = ds.Tables.Count;

			//for (int i = pageNo; i <= dsCount - 1; i++)
			//{
			//	dsAll.Merge(ds.Tables[i]);
			//}

			//ds.Clear();
			//ds.Tables.Add(dsAll);

			#endregion ########### READ ###########

			//DataTable dt = ds.Tables[pageNo];

			int tableColCount = 0;

			for (int i = 0; i < ds.Tables.Count; i++)
			{
				ds.Tables[i].Rows.Add();
				ds.Tables[i].Rows.Add();
				ds.Tables[i].Rows.Add();
				ds.Tables[i].Rows.Add();

				tableColCount = ds.Tables[i].Columns.Count;

				for (int c = 0; c < tableColCount; c++)
				{
					ds.Tables[i].Rows[ds.Tables[i].Rows.Count - 4][c] = "----------------------";
					ds.Tables[i].Rows[ds.Tables[i].Rows.Count - 3][c] = "----------------------";
					ds.Tables[i].Rows[ds.Tables[i].Rows.Count - 2][c] = "----------------------";
					ds.Tables[i].Rows[ds.Tables[i].Rows.Count - 1][c] = i + 1 + " PAGE";
				}

				ItemAll.Merge(ds.Tables[i]);
			}

			int tableCount = ds.Tables.Count;
			int startPage = 2;

			foreach (DataTable dt in ds.Tables)
			{
				if (startPage >= pageNo && dt.Columns.Count > 5)
				{
					dwgNo = string.Empty;

					int dwgRow = 0;

					for (int i = 0; i < dt.Rows.Count; i++)
					{
						string firstColStr = dt.Rows[i][_firstColInt].ToString();
						string secondColStr = dt.Rows[i][1].ToString();
						string col3 = dt.Rows[i][2].ToString();
						string col4 = dt.Rows[i][3].ToString();
						string col5 = dt.Rows[i][4].ToString();

						string categoryStr = string.Empty;
						//  파일 유형 & DWG NO 가져오기
						while (dwgRow < dt.Rows.Count)
						{
							for (int ColumnCount = 0; ColumnCount < dt.Columns.Count; ColumnCount++)
							{
								categoryStr = dt.Rows[dwgRow][ColumnCount].ToString().ToUpper();

								if (categoryStr.Contains("PART LIST") && categoryStr.Length < 20)
								{
									parsingCategory = "PART LIST";
									break;
								}
								else if (categoryStr.Contains("SCOPE OF SUPPLY LIST"))
								{
									parsingCategory = "SCOPE OF SUPPLY LIST";
									break;
								}
								else if ((categoryStr.Contains("MATERIAL") || categoryStr.Contains("BILL OF")) && !categoryStr.Contains("REVISION HISTORY"))
								{
									parsingCategory = "DWG LIST";
									break;
								}
								else if (categoryStr.Contains("REVISION HISTORY"))
								{
									parsingCategory = "DWG LIST";
									break;
								}
							}

							if (string.IsNullOrEmpty(dwgNo))
							{
								for (int c = 0; c < dt.Columns.Count - 2; c++)
								{
									string dwgCol = (dt.Rows[dwgRow][c].ToString().ToUpper() + dt.Rows[dwgRow][c + 1].ToString().ToUpper());

									if (dwgCol.StartsWith("DWGNO") || dwgCol.StartsWith("DWG NO"))
									{
										for (int cc = c; cc < dt.Columns.Count; cc++)
										{
											dwgNo = dwgNo + dt.Rows[dwgRow][cc].ToString().Replace("NO", "").Replace("DWG", "").Replace(".", "").Replace(" ", "").Trim();
										}

										if (dwgNo.ToUpper().Contains("R") && dwgNo.ToUpper().Contains("E") && dwgNo.ToUpper().Contains("V"))
										{
											int idx_s = dwgNo.ToUpper().IndexOf("R");

											dwgNo = dwgNo.Substring(0, idx_s).Trim();
										}

										if (dwgNo.Length < 5)
											dwgNo = String.Empty;
									}
									else if (dwgCol.StartsWith("DRAWING"))
									{
										dwgNo = dt.Rows[dwgRow + 1][c].ToString().Trim();
									}

									if (!string.IsNullOrEmpty(dwgNo))
										break;
								}
							}
							dwgRow += 1;
						}

						#region PART LIST

						// part list
						if (parsingCategory.Equals("PART LIST"))
						{
							if (firstColStr.ToUpper().Contains("NO") || (secondColStr.ToUpper().Contains("NO") && secondColStr.Length < 4))
							{
								standardSelect(dt, i);
							}
							else if (col3.ToUpper().Contains("SYMB") || col4.ToUpper().Contains("SYMB") || col5.ToUpper().Contains("SYMB"))
							{
								symbolSig = "SYMBOL";

								standardSelect(dt, i);
							}
							else if (col3.ToUpper().StartsWith("LOC") || col4.ToUpper().StartsWith("LOC") || col5.ToUpper().StartsWith("LOC") || secondColStr.ToUpper().StartsWith("LOC"))
							{
								symbolSig = "LOC";

								standardSelect(dt, i);
							}
							else if (firstColStr.Equals("CODE") || secondColStr.Equals("CODE"))
							{
								symbolSig = "CODE";

								standardSelect(dt, i);
							}
							else if ((GetTo.IsInt(firstColStr) || GetTo.IsInt(secondColStr)) ||
								(!string.IsNullOrEmpty(symbolSig) && _itemDesc != -1 && !string.IsNullOrEmpty(dt.Rows[i][_itemDesc].ToString())) ||
								(!string.IsNullOrEmpty(symbolSig) && _itemDesc != -1 && _itemCode != -1 && !string.IsNullOrEmpty(dt.Rows[i][_itemCode].ToString()))
								)
							{
								if (_itemQt == -1 && _itemRmk == -1 && _itemMalt == -1)
								{
									int _i = i;

									while (_itemDesc != -1 || _i < dt.Rows.Count)
									{
										for (int c = 0; c < dt.Columns.Count; c++)
										{
											if ((dt.Rows[_i][c].ToString().ToUpper().Equals("NO") || dt.Rows[_i][c].ToString().ToUpper().Equals("NO.")) && _itemNo == -1)
											{
												standardSelect(dt, _i);

												if (_itemNo != -1)
													_firstColInt = _itemNo;
											}
										}

										_i += 1;

										if (_i >= dt.Rows.Count)
											break;
									}
								}

								if (_itemSymbol != -1 && _itemNo != -1)
								{
									for (int c = _itemNo + 1; c <= _itemSymbol; c++)
									{
										itemSymbol = itemSymbol.Trim() + " " + dt.Rows[i][c].ToString().Trim();
									}

									if (_itemDesc != -1)
									{
										for (int c = _itemSymbol + 1; c <= _itemDesc; c++)
										{
											itemDesc = itemDesc.Trim() + "  " + dt.Rows[i][c].ToString().Trim();
										}
									}
								}
								else if (_itemLoc != -1 && _itemSymbol != -1)
								{
									if (_itemDesc > _itemSymbol + 1)
									{
										for (int c = _itemSymbol + 1; c <= _itemDesc; c++)
										{
											itemDesc = itemDesc.Trim() + dt.Rows[i][c].ToString().Trim();
										}
									}
									else
									{
										itemDesc = dt.Rows[i][_itemDesc].ToString().Trim();
									}

									for (int c = _itemLoc + 1; c <= _itemSymbol; c++)
									{
										itemSymbol = itemSymbol.Trim() + " " + dt.Rows[i][c].ToString().Trim();
									}
								}
								else
								{
									if (_itemDesc != -1)
									{
										if (_itemSize != -1)
										{
											for (int c = _itemNo + 1; c < _itemSize; c++)
											{
												itemDesc = itemDesc.Trim() + dt.Rows[i][c].ToString().Trim();
											}
										}
										else if (_itemSymbol != -1)
										{
											for (int c = _itemSymbol + 1; c <= _itemDesc; c++)
											{
												itemDesc = itemDesc.Trim() + dt.Rows[i][c].ToString().Trim();
											}
										}
										else if (_itemCode != -1 && _itemDesc > _itemCode)
										{
											for (int c = _itemCode + 1; c <= _itemDesc; c++)
											{
												itemDesc = itemDesc.Trim() + dt.Rows[i][c].ToString().Trim();
											}
										}
										else
										{
											for (int c = _itemNo + 1; c <= _itemDesc; c++)
											{
												itemDesc = itemDesc.Trim() + dt.Rows[i][c].ToString().Trim();
											}
										}
									}
								}

								if (_itemSize != -1)
									itemSize = dt.Rows[i][_itemSize].ToString().Trim();

								if (_itemMalt != -1)
									itemMalt = dt.Rows[i][_itemMalt].ToString().Trim();

								if (_itemQt != -1)
									itemQt = dt.Rows[i][_itemQt].ToString().Trim();

								if (_itemWt != -1)
									itemWt = dt.Rows[i][_itemWt].ToString().Trim();

								if (_itemRmk != -1)
									itemRmk = dt.Rows[i][_itemRmk].ToString().Trim();

								if (_itemMaker != -1)
									itemMaker = dt.Rows[i][_itemMaker].ToString().Trim();

								if (_itemLoc != -1)
									itemLoc = dt.Rows[i][_itemLoc].ToString().Trim();

								if (_itemSpec != -1)
								{
									for (int c = _itemDesc + 1; c <= _itemSpec; c++)
									{
										itemSpec = itemSpec.Trim() + dt.Rows[i][c].ToString().Trim();
									}
								}
								if (_itemCode != -1)
									itemCode = dt.Rows[i][_itemCode].ToString().Trim();

								if (_itemModel != -1)
								{
									for (int c = _itemDesc + 1; c <= _itemModel; c++)
									{
										itemModel = itemModel.Trim() + " " + dt.Rows[i][c].ToString().Trim();
									}
								}

								if (_itemDwg != -1)
									itemDwg = dt.Rows[i][_itemDwg].ToString().Trim();

								itemQt = itemQt.Replace("-", "").Replace("EA", "").Replace("ST", "").Trim();

								dtItem.Rows.Add();
								dtItem.Rows[dtItem.Rows.Count - 1]["PAGENO"] = Convert.ToString(startPage - 1);
								dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = itemNo;
								dtItem.Rows[dtItem.Rows.Count - 1]["SYMBOL"] = itemSymbol;
								dtItem.Rows[dtItem.Rows.Count - 1]["MAKER"] = itemMaker;
								dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = itemDesc;
								dtItem.Rows[dtItem.Rows.Count - 1]["SIZE"] = itemSize;
								dtItem.Rows[dtItem.Rows.Count - 1]["MALT"] = itemMalt;
								dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = itemQt;
								dtItem.Rows[dtItem.Rows.Count - 1]["MODEL"] = itemModel;
								dtItem.Rows[dtItem.Rows.Count - 1]["LOC"] = itemLoc;
								dtItem.Rows[dtItem.Rows.Count - 1]["SPECIFICATION"] = itemSpec;
								dtItem.Rows[dtItem.Rows.Count - 1]["CODE"] = itemCode;

								if (GetTo.IsInt(itemWt) || itemWt.Contains("."))
									dtItem.Rows[dtItem.Rows.Count - 1]["WT"] = itemWt;
								else
									dtItem.Rows[dtItem.Rows.Count - 1]["RMK"] = itemWt;

								if (!string.IsNullOrEmpty(itemRmk))
									dtItem.Rows[dtItem.Rows.Count - 1]["RMK"] = itemRmk;

								if (!string.IsNullOrEmpty(itemDwg))
									dtItem.Rows[dtItem.Rows.Count - 1]["DWGNO"] = itemDwg;
								else
									dtItem.Rows[dtItem.Rows.Count - 1]["DWGNO"] = dwgNo.Trim();

								insertReset();
							}
						}

						#endregion PART LIST

						#region SCOPE OF SUPPLY LIST

						else if (parsingCategory.Equals("SCOPE OF SUPPLY LIST"))
						{
						}

						#endregion SCOPE OF SUPPLY LIST

						#region DWG LIST

						else if (parsingCategory.Equals("DWG LIST"))
						{
							if (_itemStartSig == -1)
							{
								int _i = i;
								string noStr = string.Empty;

								while (_itemStartSig == -1)
								{
									for (int c = 0; c < dt.Columns.Count; c++)
									{
										noStr = dt.Rows[_i][c].ToString().ToUpper().Trim();

										if ((noStr.StartsWith("NO") && noStr.Length < 6 && !noStr.StartsWith("NOTE")) || noStr.Equals("NO.") || noStr.Equals("ITEM"))
										{
											if (_itemNo == -1)
											{
												_itemNo = c;

												// 진짜가 맞는지 판별하기 위해서
												string trueValue = string.Empty;
												for (int factDetec = 0; factDetec < dt.Rows.Count; factDetec++)
												{
													trueValue = trueValue + dt.Rows[factDetec][_itemNo].ToString().Trim();
												}

												if (string.IsNullOrEmpty(trueValue.Replace("NO", "").Replace(".", "").Trim()))
												{
													_itemNo = -1;
												}
												else
												{
													_itemEnd = _i;
													//_itemStartSig = c;

													if (_itemEnd > 5)
														itemUpDown = "UP";
													else
														itemUpDown = "DOWN";

													for (int cc = c; cc < dt.Columns.Count; cc++)
													{
														selectValue = dt.Rows[_i][cc].ToString().Replace(" ", "").ToUpper();

														if (selectValue.Contains("DESCRIPTION") || dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("DESCRIPTION") || (selectValue.Contains("PART") && selectValue.Contains("NAME")) || selectValue.Contains("NAME OF PART")) _itemDesc = cc;
														else if (selectValue.Contains("MAT") || (dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("MAT") && !dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("MATERIAL"))) _itemMalt = cc;
														else if ((selectValue.Contains("Q") && selectValue.Contains("TY")) || (dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("Q") && dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("TY"))) _itemQt = cc;
														else if (selectValue.Contains("WT") || dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("WT")) _itemWt = cc;
														else if (selectValue.Contains("REMARKS") || dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("REMARKS")) _itemRmk = cc;
														else if (selectValue.Contains("DIMENSION")) _itemSpec = cc;
													}
												}
											}
											else if (_itemNo2 == -1)
											{
												_itemNo2 = c;

												// 진짜가 맞는지 판별하기 위해서
												string trueValue = string.Empty;
												for (int factDetec = _i; factDetec < dt.Rows.Count; factDetec++)
												{
													trueValue = trueValue + dt.Rows[factDetec][_itemNo2].ToString().Trim();
												}

												if (string.IsNullOrEmpty(trueValue.Replace("NO", "").Replace(".", "").Trim()))
												{
													_itemNo2 = -1;
												}
												else
												{
													_itemEnd = _i;
													//_itemStartSig = c;

													if (_itemEnd > 5)
														itemUpDown = "UP";
													else
														itemUpDown = "DOWN";

													for (int cc = c; cc < dt.Columns.Count; cc++)
													{
														selectValue = dt.Rows[_i][cc].ToString().Replace(" ", "").ToUpper();

														if (dt.Rows.Count > _i + 1)
														{
															if (selectValue.Contains("DESCRIPTION") || dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("DESCRIPTION") || (selectValue.Contains("PART") && selectValue.Contains("NAME"))) _itemDesc2 = cc;
															else if (selectValue.Contains("MAT") || (dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("MAT") && !dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("MATERIAL"))) _itemMalt2 = cc;
															else if ((selectValue.Contains("Q") && selectValue.Contains("TY")) || (dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("Q") && dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("TY"))) _itemQt2 = cc;
															else if (selectValue.Contains("WT") || dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("WT")) _itemWt2 = cc;
															else if (selectValue.Contains("REMARKS") || dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("REMARKS")) _itemRmk2 = cc;
															else if (selectValue.Contains("DIMENSION")) _itemSpec2 = cc;
														}
														else
														{
															if (selectValue.Contains("DESCRIPTION") || selectValue.Contains("PART NAME")) _itemDesc2 = cc;
															else if (selectValue.Contains("MAT")) _itemMalt2 = cc;
															else if ((selectValue.Contains("Q") && selectValue.Contains("TY"))) _itemQt2 = cc;
															else if (selectValue.Contains("WT")) _itemWt2 = cc;
															else if (selectValue.Contains("REMARKS")) _itemRmk2 = cc;
															else if (selectValue.Contains("DIMENSION")) _itemSpec2 = cc;
														}
													}
												}
											}
										}
									}

									_i += 1;

									if (_i >= dt.Rows.Count)
									{
										_itemStartSig = 0;

										int __i = i;

										#region 다시 한번 더 돌리기

										while (_itemStartSig == -1)
										{
											for (int c = 0; c < dt.Columns.Count; c++)
											{
												if (((dt.Rows[__i][c].ToString().ToUpper().StartsWith("NO") && dt.Rows[__i][c].ToString().Length < 6) || dt.Rows[__i][c].ToString().ToUpper().Equals("NO.")) || dt.Rows[__i][c].ToString().ToUpper().Equals("ITEM"))
												{
													if (_itemNo == -1)
													{
														_itemNo = c;

														// 진짜가 맞는지 판별하기 위해서
														string trueValue = string.Empty;
														for (int factDetec = _i; factDetec < dt.Rows.Count; factDetec++)
														{
															trueValue = trueValue + dt.Rows[factDetec][_itemNo].ToString().Trim();
														}

														if (string.IsNullOrEmpty(trueValue.Replace("NO", "").Replace(".", "").Trim()))
														{
															_itemNo = -1;
														}
														else
														{
															_itemEnd = _i;
															//_itemStartSig = c;

															if (_itemEnd > 5)
																itemUpDown = "UP";
															else
																itemUpDown = "DOWN";

															for (int cc = c; cc < dt.Columns.Count; cc++)
															{
																selectValue = dt.Rows[_i][cc].ToString().Replace(" ", "").ToUpper();

																if (selectValue.Contains("DESCRIPTION") || dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("DESCRIPTION") || (selectValue.Contains("PART") && selectValue.Contains("NAME")) || selectValue.Contains("NAME OF PART")) _itemDesc = cc;
																else if (selectValue.Contains("MAT") || (dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("MAT") && !dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("MATERIAL"))) _itemMalt = cc;
																else if ((selectValue.Contains("Q") && selectValue.Contains("TY")) || (dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("Q") && dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("TY"))) _itemQt = cc;
																else if (selectValue.Contains("WT") || dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("WT")) _itemWt = cc;
																else if (selectValue.Contains("REMARKS") || dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("REMARKS")) _itemRmk = cc;
																else if (selectValue.Contains("DIMENSION")) _itemSpec = cc;
															}
														}
													}
													else if (_itemNo2 == -1)
													{
														_itemNo2 = c;

														// 진짜가 맞는지 판별하기 위해서
														string trueValue = string.Empty;
														for (int factDetec = _i; factDetec < dt.Rows.Count; factDetec++)
														{
															trueValue = trueValue + dt.Rows[factDetec][_itemNo2].ToString().Trim();
														}

														if (string.IsNullOrEmpty(trueValue.Replace("NO", "").Replace(".", "").Trim()))
														{
															_itemNo2 = -1;
														}
														else
														{
															_itemEnd = _i;
															//_itemStartSig = c;

															if (_itemEnd > 5)
																itemUpDown = "UP";
															else
																itemUpDown = "DOWN";

															for (int cc = c; cc < dt.Columns.Count; cc++)
															{
																selectValue = dt.Rows[_i][cc].ToString().Replace(" ", "").ToUpper();

																if (dt.Rows.Count > _i + 1)
																{
																	if (selectValue.Contains("DESCRIPTION") || dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("DESCRIPTION") || (selectValue.Contains("PART") && selectValue.Contains("NAME")) || selectValue.Contains("NAME OF PART")) _itemDesc2 = cc;
																	else if (selectValue.Contains("MAT") || (dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("MAT") && !dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("MATERIAL"))) _itemMalt2 = cc;
																	else if ((selectValue.Contains("Q") && selectValue.Contains("TY")) || (dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("Q") && dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("TY"))) _itemQt2 = cc;
																	else if (selectValue.Contains("WT") || dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("WT")) _itemWt2 = cc;
																	else if (selectValue.Contains("REMARKS") || dt.Rows[_i + 1][cc].ToString().ToUpper().Contains("REMARKS")) _itemRmk2 = cc;
																	else if (selectValue.Contains("DIMENSION")) _itemSpec2 = cc;
																}
																else
																{
																	if (selectValue.Contains("DESCRIPTION") || selectValue.Contains("PART NAME")) _itemDesc2 = cc;
																	else if (selectValue.Contains("MAT")) _itemMalt2 = cc;
																	else if ((selectValue.Contains("Q") && selectValue.Contains("TY"))) _itemQt2 = cc;
																	else if (selectValue.Contains("WT")) _itemWt2 = cc;
																	else if (selectValue.Contains("REMARKS")) _itemRmk2 = cc;
																	else if (selectValue.Contains("DIMENSION")) _itemSpec2 = cc;
																}
															}
														}
													}
												}
											}

											_i += 1;

											if (_i >= dt.Rows.Count)
											{
												_itemStartSig = 0;
												break;
											}
										}

										#endregion 다시 한번 더 돌리기

										break;
									}
								}
							}
							else
							{
								if (_itemEnd <= i && itemUpDown.Equals("UP"))
									break;

								if (_itemMalt != -1)
									itemMalt = dt.Rows[i][_itemMalt].ToString().Trim();

								if (_itemRmk != -1)
									itemRmk = dt.Rows[i][_itemRmk].ToString().Trim();

								if (_itemWt != -1)
									itemWt = dt.Rows[i][_itemWt].ToString().Trim();

								if (_itemQt != -1)
									itemQt = dt.Rows[i][_itemQt].ToString().Trim();

								if (_itemNo != -1)
									itemNo = dt.Rows[i][_itemNo].ToString().Trim();

								if (_itemDesc != -1)
								{
									if (GetTo.IsInt(itemNo.Trim()))
									{
										if (_itemMalt != -1)
										{
											for (int c = _itemNo + 1; c < _itemMalt; c++)
											{
												itemDesc = itemDesc.Trim() + " " + dt.Rows[i][c].ToString().Trim();
											}
										}
										else
										{
											for (int c = _itemNo + 1; c <= _itemDesc; c++)
											{
												itemDesc = itemDesc.Trim() + " " + dt.Rows[i][c].ToString().Trim();
											}
										}
									}
									else
									{
										if (_itemNo != -1)
										{
											for (int c = _itemNo; c < _itemMalt; c++)
											{
												itemDesc = itemDesc.Trim() + " " + dt.Rows[i][c].ToString().Trim();
											}
										}
										else
										{
											for (int c = _itemNo + 1; c < _itemMalt; c++)
											{
												itemDesc = itemDesc.Trim() + " " + dt.Rows[i][c].ToString().Trim();
											}
										}
									}
								}

								itemDesc = itemDesc.Trim();
								itemMalt = itemMalt.Trim();
								itemQt = itemQt.Replace("-", "").Trim();
								itemWt = itemWt.Trim();
								itemRmk = itemRmk.Trim();

								if (itemDesc.EndsWith("-"))
									itemDesc = itemDesc.Substring(0, itemDesc.Length - 1).Trim();

								if (itemDesc.Equals("DATE"))
									break;

								if (itemDesc.StartsWith("DWN") || itemDesc.Equals("A 0"))
									break;

								if (string.IsNullOrEmpty(itemQt) && _itemQt == -1 && _itemMalt + 1 < _itemWt)
								{
									_itemQt = _itemMalt + 1;

									itemQt = dt.Rows[i][_itemQt].ToString().Trim();
								}

								itemQt = itemQt.Replace("-", "").Replace("EA", "").Replace("ST", "").Trim();

								if (!string.IsNullOrEmpty(itemDesc.Trim()) && !itemDesc.Contains("SET/SHIP") && !itemDesc.Contains("WEIGHT") && !itemDesc.Contains("DIMENSIONS") && !itemDesc.Contains("CAPACITY") && !itemDesc.Equals("A") && !itemDesc.StartsWith("0")
									&& !itemDesc.Contains("DESCRIPTION") && !itemDesc.ToUpper().Equals("PORT SIDE") && !itemDesc.ToUpper().Contains("SETS / DAVIT") && itemDesc.Length > 2 && !itemDesc.ToUpper().Contains("BENDING") && !itemDesc.ToUpper().StartsWith("APPROVAL COMMENT BY")
									&& !itemDesc.ToUpper().Contains("SET / SHIP") && !itemDesc.ToUpper().Contains("COLOR OF TEXT") && !itemDesc.ToUpper().Contains("STYLE OF TEXT") && !itemDesc.ToUpper().StartsWith("REVISED BY")
									&& !itemDesc.ToUpper().Contains("EA/SHIP") && !itemDesc.ToUpper().Contains("REVISION HISTORY") && !itemDesc.ToUpper().StartsWith("DWG") && !itemMalt.ToUpper().StartsWith("IS") && !itemDesc.ToUpper().StartsWith("REV. NO.")
									&& !itemDesc.ToUpper().Contains("THIS DRAWING") && !itemDesc.ToUpper().StartsWith("COPIED OR REPRODUCE") && !itemDesc.ToUpper().StartsWith("PURPOSE AGAINST") && !itemDesc.ToUpper().Contains("ANY PURPOSE")
									&& !itemDesc.ToUpper().Contains("YARD COMMENT")
									)
								{
									dtItem.Rows.Add();
									dtItem.Rows[dtItem.Rows.Count - 1]["PAGENO"] = Convert.ToString(startPage - 1);
									dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = itemNo;
									dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = itemDesc;
									dtItem.Rows[dtItem.Rows.Count - 1]["MALT"] = itemMalt;
									dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = itemQt;

									if (GetTo.IsInt(itemWt) || itemWt.Contains("."))
										dtItem.Rows[dtItem.Rows.Count - 1]["WT"] = itemWt;
									else
										dtItem.Rows[dtItem.Rows.Count - 1]["RMK"] = itemWt;

									if (!string.IsNullOrEmpty(itemRmk))
										dtItem.Rows[dtItem.Rows.Count - 1]["RMK"] = itemRmk;

									dtItem.Rows[dtItem.Rows.Count - 1]["DWGNO"] = dwgNo.Trim();
								}

								insertReset();

								// 두번째
								if (_itemNo2 != -1)
								{
									if (_itemEnd <= i && itemUpDown.Equals("UP"))
										break;

									if (_itemMalt2 != -1)
										itemMalt = dt.Rows[i][_itemMalt2].ToString().Trim();

									if (_itemRmk2 != -1)
										itemRmk = dt.Rows[i][_itemRmk2].ToString().Trim();

									if (_itemWt2 != -1)
										itemWt = dt.Rows[i][_itemWt2].ToString().Trim();

									if (_itemQt2 != -1)
										itemQt = dt.Rows[i][_itemQt2].ToString().Trim();

									if (_itemNo2 != -1)
										itemNo = dt.Rows[i][_itemNo2].ToString().Trim();

									if (_itemDesc2 != -1)
									{
										if (GetTo.IsInt(itemNo.Trim()))
										{
											for (int c = _itemNo2 + 1; c < _itemMalt2; c++)
											{
												itemDesc = itemDesc.Trim() + " " + dt.Rows[i][c].ToString().Trim();
											}
										}
										else
										{
											if (_itemNo2 != -1)
											{
												for (int c = _itemNo2; c < _itemMalt2; c++)
												{
													itemDesc = itemDesc.Trim() + " " + dt.Rows[i][c].ToString().Trim();
												}
											}
											else
											{
												for (int c = _itemNo2 + 1; c < _itemMalt2; c++)
												{
													itemDesc = itemDesc.Trim() + " " + dt.Rows[i][c].ToString().Trim();
												}
											}
										}
									}

									itemDesc = itemDesc.Trim();
									itemMalt = itemMalt.Trim();
									itemQt = itemQt.Replace("-", "").Replace("EA", "").Replace("ST", "").Trim();
									itemWt = itemWt.Trim();
									itemRmk = itemRmk.Trim();

									if (itemDesc.EndsWith("-"))
										itemDesc = itemDesc.Substring(0, itemDesc.Length - 1).Trim();

									if (itemDesc.Equals("DATE"))
										break;

									if (itemDesc.StartsWith("DWN") || itemDesc.Equals("A 0"))
										break;

									if (!string.IsNullOrEmpty(itemDesc.Trim()) && !itemDesc.Contains("SET/SHIP") && !itemDesc.Contains("WEIGHT") && !itemDesc.Contains("DIMENSIONS") && !itemDesc.Contains("CAPACITY") && !itemDesc.Equals("A") && !itemDesc.StartsWith("0")
										&& !itemDesc.Contains("DESCRIPTION") && !itemDesc.ToUpper().Equals("PORT SIDE") && !itemDesc.ToUpper().Contains("SETS / DAVIT") && itemDesc.Length > 2 && !itemDesc.ToUpper().Contains("BENDING") && !itemDesc.ToUpper().StartsWith("APPROVAL COMMENT BY")
										&& !itemDesc.ToUpper().Contains("SET / SHIP") && !itemDesc.ToUpper().Contains("COLOR OF TEXT") && !itemDesc.ToUpper().Contains("STYLE OF TEXT") && !itemDesc.ToUpper().StartsWith("REVISED BY")
										&& !itemDesc.ToUpper().Contains("EA/SHIP") && !itemDesc.ToUpper().Contains("REVISION HISTORY") && !itemDesc.ToUpper().StartsWith("DWG") && !itemMalt.ToUpper().StartsWith("IS") && !itemDesc.ToUpper().StartsWith("REV. NO.")
										&& !itemDesc.ToUpper().Contains("THIS DRAWING") && !itemDesc.ToUpper().StartsWith("COPIED OR REPRODUCE") && !itemDesc.ToUpper().StartsWith("PURPOSE AGAINST") && !itemDesc.ToUpper().Contains("ANY PURPOSE")
										)
									{
										dtItem.Rows.Add();
										dtItem.Rows[dtItem.Rows.Count - 1]["PAGENO"] = Convert.ToString(startPage - 1);
										dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = itemNo;
										dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = itemDesc;
										dtItem.Rows[dtItem.Rows.Count - 1]["MALT"] = itemMalt;
										dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = itemQt;

										if (GetTo.IsInt(itemWt) || itemWt.Contains("."))
											dtItem.Rows[dtItem.Rows.Count - 1]["WT"] = itemWt;
										else
											dtItem.Rows[dtItem.Rows.Count - 1]["RMK"] = itemWt;

										if (!string.IsNullOrEmpty(itemRmk))
											dtItem.Rows[dtItem.Rows.Count - 1]["RMK"] = itemRmk;

										dtItem.Rows[dtItem.Rows.Count - 1]["DWGNO"] = dwgNo.Trim();
									}

									insertReset();
								}
							}
						}

						#endregion DWG LIST

						#region REVISION

						else if (parsingCategory.Equals("REVISION"))
						{
						}

						#endregion REVISION
					}

					_itemStartSig = -1;
					_itemEnd = -1;

					itemUpDown = string.Empty;

					_itemDesc = -1;
					_itemMalt = -1;
					_itemQt = -1;
					_itemWt = -1;
					_itemRmk = -1;
					_itemNo = -1;
					_itemSize = -1;
					_itemSymbol = -1;
					_itemSpec = -1;
					_itemModel = -1;
					_itemMaker = -1;
					_itemCode = -1;
					_itemLoc = -1;

					_itemDesc2 = -1;
					_itemMalt2 = -1;
					_itemQt2 = -1;
					_itemWt2 = -1;
					_itemRmk2 = -1;
					_itemNo2 = -1;
					_itemSize2 = -1;
					_itemSymbol2 = -1;
					_itemSpec2 = -1;
					_itemModel2 = -1;
					_itemMaker2 = -1;
					_itemCode2 = -1;
					_itemLoc2 = -1;

					_firstColInt = 0;

					symbolSig = string.Empty;
				}
				dtItem.Rows.Add();
				dtItem.Rows[dtItem.Rows.Count - 1]["PAGENO"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["SHIPYARD"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["HULLNO"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["DWGNO"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["PROJECT"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["TITLE"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["SYMBOL"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["MAKER"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["SIZE"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["MALT"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["WT"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["CODE"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["RMK"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["SPECIFICATION"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["MODEL"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["LOC"] = "---------------------";
				dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = "---------------------";

				startPage += 1;
			}
		}

		private void standardSelect(DataTable dt, int i)
		{
			for (int partNo = 0; partNo < dt.Columns.Count; partNo++)
			{
				selectValue = dt.Rows[i][partNo].ToString().Replace(" ", "").ToUpper();

				//DRSCRIPTION DRSCRIPTION
				if (selectValue.StartsWith("DESCRIPTION") || dt.Rows[i][partNo].ToString().ToUpper().StartsWith("PART NAME") || selectValue.Contains("SCRIPT") || selectValue.StartsWith("NAME") || selectValue.Contains("SCR1")) _itemDesc = partNo;
				else if (selectValue.StartsWith("SIZE") || selectValue.StartsWith("CM")) _itemSize = partNo;
				else if (selectValue.StartsWith("MAT")) _itemMalt = partNo;
				else if (selectValue.Contains("Q") && selectValue.Contains("TY")) _itemQt = partNo;
				else if (selectValue.Contains("W") && selectValue.Contains("HT")) _itemWt = partNo;
				else if (selectValue.StartsWith("REMARKS")) _itemRmk = partNo;
				else if (selectValue.StartsWith("NO")) _itemNo = partNo;
				else if (selectValue.StartsWith("DIMENSION")) _itemSpec = partNo;
				else if (selectValue.StartsWith("TAG") || selectValue.StartsWith("SYMB")) _itemSymbol = partNo;
				else if (selectValue.StartsWith("MANUFACTURE") || selectValue.Contains("FACTURE") || dt.Rows[i][partNo].ToString().ToUpper().StartsWith("MAN")) _itemMaker = partNo;
				else if (selectValue.StartsWith("MODEL NO")) _itemModel = partNo;
				else if (selectValue.StartsWith("CODE")) _itemCode = partNo;
				else if (selectValue.StartsWith("CATALOGUE") || selectValue.StartsWith("CATALOG")) _itemSpec = partNo;
				else if (selectValue.StartsWith("LOC")) _itemLoc = partNo;
				else if (selectValue.StartsWith("DRAWING")) _itemDwg = partNo;
				else if (selectValue.StartsWith("MODEL")) _itemModel = partNo;
			}
		}

		private void insertReset()
		{
			itemDesc = string.Empty;
			itemMalt = string.Empty;
			itemQt = string.Empty;
			itemWt = string.Empty;
			itemRmk = string.Empty;
			itemSize = string.Empty;
			itemSymbol = string.Empty;
			itemSpec = string.Empty;
			itemModel = string.Empty;
			itemMaker = string.Empty;
			itemCode = string.Empty;
			itemLoc = string.Empty;
			itemDwg = string.Empty;
			itemNo = string.Empty;
		}
	}
}
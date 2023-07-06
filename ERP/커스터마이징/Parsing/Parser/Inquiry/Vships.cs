using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class Vships
	{
		string vessel;
		string reference;
		string imoNumber;
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

		public string Imonumber
		{
			get
			{
				return imoNumber;
			}
		}

		#endregion ==================================================================================================== Constructor



		public Vships(string fileName)
		{
			vessel = "";                        // 선명
			reference = "";                     // 문의번호
			imoNumber = "";

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

			int _itemDescInt = -1;
			int _itemUnitInt = -1;
			int _itemQtInt = -1;
			int _itemDrwInt = -1;
			int _itemCodeInt = -1;
			int _itemSheetNo = -1;

			bool itemStart = false;

			string subjStr = string.Empty;

			string descDrwStr = string.Empty;
			string descPlateSheetNo = string.Empty;





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

					if (firstColStr.Contains("Full Quote")) break;
					if (firstColStr.Contains("V.Ships takes the matter of")) break;

					if (firstColStr.Contains("Vessel Name"))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (string.IsNullOrEmpty(vessel))
								vessel = dt.Rows[i][c].ToString().Trim();
							else
								break;
							//if (dt.Rows[i][c].ToString().Contains("IMO"))
							//    break;
							//else
							//    vessel = vessel + dt.Rows[i][c].ToString().Trim();
						}
					}
					else if (firstColStr.Contains("Request For Q"))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (string.IsNullOrEmpty(reference))
								reference = dt.Rows[i][c].ToString().Trim();
							else
								break;
							//if (dt.Rows[i][c].ToString().Contains("Status"))
							//    break;
							//else
							//    reference = reference + dt.Rows[i][c].ToString().Trim();
						}
					}
					else if (firstColStr.Contains("IMO Num"))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (string.IsNullOrEmpty(imoNumber))
								imoNumber = dt.Rows[i][c].ToString().Trim();
							else
								break;
							//if (dt.Rows[i][c].ToString().Contains("Status"))
							//    break;
							//else
							//    reference = reference + dt.Rows[i][c].ToString().Trim();
						}
					}
					else if (firstColStr.Contains("For Component"))
					{
						for(int c = 0; c < dt.Columns.Count; c++)
						{
							subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
						}

						subjStr = subjStr + Environment.NewLine;

						int _i = i + 1;

						while(!dt.Rows[_i][0].ToString().ToLower().StartsWith("item"))
						{
							

							for(int c = 0; c < dt.Columns.Count; c++)
							{
								subjStr = subjStr + " " + dt.Rows[_i][c].ToString().Trim();
							}

							subjStr = subjStr + Environment.NewLine;

							_i += 1;

							if (_i > dt.Rows.Count)
								break;
						}
					}
					//else if (firstColStr.Contains("Equipment Name"))
					//{
					//	for (int c = 1; c < dt.Columns.Count; c++)
					//	{
					//		subjStr = subjStr.Trim() + dt.Rows[i][c].ToString().Trim();
					//	}
					//}
					//else if (firstColStr.Contains("Equipment Type"))
					//{
					//	for (int c = 1; c < dt.Columns.Count; c++)
					//	{
					//		subjTypeStr = subjTypeStr.Trim() + dt.Rows[i][c].ToString().Trim();
					//	}
					//}
					//else if (firstColStr.Contains("Serial Number"))
					//{
					//	for (int c = 1; c < dt.Columns.Count; c++)
					//	{
					//		subjSerialStr = subjSerialStr.Trim() + dt.Rows[i][c].ToString().Trim();
					//	}
					//}
					//else if (firstColStr.Contains("Manufacturer"))
					//{
					//	for (int c = 1; c < dt.Columns.Count; c++)
					//	{
					//		subjMakerStr = subjMakerStr.Trim() + dt.Rows[i][c].ToString().Trim();
					//	}
					//}
					//else if (firstColStr.Contains("Component Notes"))
					//{
					//	int _i = i + 1;
					//	while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
					//	{
					//		if (dt.Rows[_i][0].ToString().Contains("Originated Date"))
					//			break;

					//		for (int c = 1; c < dt.Columns.Count; c++)
					//		{
					//			subjNoteStr = subjNoteStr + dt.Rows[_i][c].ToString().Trim();
					//		}
					//		subjNoteStr = subjNoteStr.Trim() + Environment.NewLine;
					//		_i += 1;
					//	}
					//}
					//else if (firstColStr.Equals("PF 1/2'';"))
					//{

					//}
					else if (firstColStr.Equals("Item"))
					{
						itemStart = true;
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("Description")) _itemDescInt = c;
							else if (dt.Rows[i][c].ToString().Contains("Makers Reference")) _itemCodeInt = c;
							else if (dt.Rows[i][c].ToString().Contains("Drawing")) _itemDrwInt = c;
							else if (dt.Rows[i][c].ToString().Equals("UOM")) _itemUnitInt = c;
							else if (dt.Rows[i][c].ToString().Contains("Qty")) _itemQtInt = c;
							else if (dt.Rows[i][c].ToString().StartsWith("Plate Sheet")) _itemSheetNo = c;
							
						}
					}
					else if (((GetTo.IsInt(firstColStr) || firstColStr.Contains("''")) && firstColStr.Length < 4 && itemStart) || ((GetTo.IsInt(secondColStr) || secondColStr.Contains("''")) && secondColStr.Length < 4 && itemStart))
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

						if (rowValueSpl[4] != null && rowValueSpl[5] == null)
						{
							_itemDescInt = Convert.ToInt16(rowValueSpl[3].ToString());
							_itemCodeInt = Convert.ToInt16(rowValueSpl[4].ToString());
							_itemUnitInt = Convert.ToInt16(rowValueSpl[2].ToString());
							_itemQtInt = Convert.ToInt16(rowValueSpl[1].ToString());
						}
						else if (rowValueSpl[5] != null && rowValueSpl[6] == null)
						{
							_itemDescInt = Convert.ToInt16(rowValueSpl[3].ToString());
							_itemCodeInt = Convert.ToInt16(rowValueSpl[4].ToString());
							_itemUnitInt = Convert.ToInt16(rowValueSpl[2].ToString());
							_itemQtInt = Convert.ToInt16(rowValueSpl[1].ToString());
							_itemDrwInt = Convert.ToInt16(rowValueSpl[5].ToString());
						}
						else if (rowValueSpl[6] != null && rowValueSpl[7] == null)
						{
							_itemDescInt = Convert.ToInt16(rowValueSpl[3].ToString());
							_itemCodeInt = Convert.ToInt16(rowValueSpl[4].ToString());
							_itemUnitInt = Convert.ToInt16(rowValueSpl[2].ToString());
							_itemQtInt = Convert.ToInt16(rowValueSpl[1].ToString());
							_itemSheetNo = Convert.ToInt16(rowValueSpl[5].ToString());
							_itemDrwInt = Convert.ToInt16(rowValueSpl[6].ToString());
						}
						else if (rowValueSpl[7] != null && rowValueSpl[8] == null)
						{
							_itemDescInt = Convert.ToInt16(rowValueSpl[3].ToString());
							_itemCodeInt = Convert.ToInt16(rowValueSpl[4].ToString());
							_itemUnitInt = Convert.ToInt16(rowValueSpl[2].ToString());
							_itemQtInt = Convert.ToInt16(rowValueSpl[1].ToString());
							_itemSheetNo = Convert.ToInt16(rowValueSpl[5].ToString());
							_itemDrwInt = Convert.ToInt16(rowValueSpl[6].ToString());
						}
						else if (rowValueSpl[8] != null && rowValueSpl[9] == null)
						{
							_itemDescInt = Convert.ToInt16(rowValueSpl[3].ToString());
							_itemCodeInt = Convert.ToInt16(rowValueSpl[4].ToString());
							_itemUnitInt = Convert.ToInt16(rowValueSpl[2].ToString());
							_itemQtInt = Convert.ToInt16(rowValueSpl[1].ToString());
							//_itemSheetNo = Convert.ToInt16(rowValueSpl[5].ToString());
							_itemDrwInt = Convert.ToInt16(rowValueSpl[5].ToString());
						}


						



						if (!_itemDescInt.Equals(-1))
						{
							iTemDESC = dt.Rows[i][_itemDescInt].ToString().Trim();

							int _i = i + 1;

							if(GetTo.IsInt(firstColStr))
							{
								while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
								{
									if (string.IsNullOrEmpty(dt.Rows[_i][_itemDescInt].ToString()) && string.IsNullOrEmpty(dt.Rows[_i][_itemUnitInt].ToString()))
										break;

									for (int c = _itemUnitInt; c <= _itemCodeInt; c++)
										iTemDESC = iTemDESC + " " + dt.Rows[_i][c].ToString().Trim();

									_i += 1;

									if (_i > dt.Rows.Count)
										break;

								}
							}
							else if(GetTo.IsInt(secondColStr))
							{
								while (string.IsNullOrEmpty(dt.Rows[_i][1].ToString()))
								{
									if (string.IsNullOrEmpty(dt.Rows[_i][_itemDescInt].ToString()) && !dt.Rows[_i +1][_itemDescInt].ToString().Contains("Order Line Notes"))
										break;

									if(dt.Rows[_i][_itemDescInt].ToString().Contains("Order Line Notes"))
									{
										for (int c = _itemDescInt; c <= _itemCodeInt; c++)
											iTemDESC = iTemDESC + " " + dt.Rows[_i][c].ToString().Trim();

										int _r = _i + 1;

										while(string.IsNullOrEmpty(dt.Rows[_r][1].ToString()))
										{
											for (int c = _itemDescInt; c <= _itemCodeInt; c++)
											{
												iTemDESC = iTemDESC + Environment.NewLine + dt.Rows[_r][c].ToString().Trim();
											}

											_r += 1;

											if (_r >= dt.Rows.Count)
												break;
										}
									}
									else
									{
										iTemDESC = iTemDESC + Environment.NewLine + dt.Rows[_i][_itemDescInt].ToString().Trim();
									}


									
									_i += 1;

									if (_i > dt.Rows.Count)
										break;

								}
							}
						}

						if (_itemCodeInt != -1)
						{
							iTemCode = dt.Rows[i][_itemCodeInt].ToString().Trim();

							int _i = i + 1;

							if (GetTo.IsInt(firstColStr))
							{
								while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
								{
									if (string.IsNullOrEmpty(dt.Rows[_i][_itemCodeInt].ToString()))
										break;

									iTemCode = iTemCode + Environment.NewLine + dt.Rows[_i][_itemCodeInt].ToString().Trim();
									_i += 1;

									if (_i > dt.Rows.Count)
										break;

								}
							}
							else if (GetTo.IsInt(secondColStr))
							{
								while (string.IsNullOrEmpty(dt.Rows[_i][1].ToString()))
								{
									if (string.IsNullOrEmpty(dt.Rows[_i][_itemCodeInt].ToString()) || dt.Rows[_i][_itemDescInt].ToString().Contains("Order Line Notes"))
										break;

									iTemCode = iTemCode + Environment.NewLine + dt.Rows[_i][_itemCodeInt].ToString().Trim();
									_i += 1;

									if (_i > dt.Rows.Count)
										break;

								}
							}
						}


						if (!_itemUnitInt.Equals(-1))
						{
							iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();

							if (iTemUnit.Contains("m"))
								iTemUnit = "MTR";
						}

						if (!_itemQtInt.Equals(-1))
							iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();


						//if (!string.IsNullOrEmpty(descNoteStr))
						//	iTemDESC = iTemDESC + Environment.NewLine + descNoteStr.Trim();


						if (!string.IsNullOrEmpty(subjStr))
							iTemSUBJ = subjStr.Trim();

						//if (!string.IsNullOrEmpty(subjTypeStr))
						//	iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + subjTypeStr.Trim();

						//if (!string.IsNullOrEmpty(subjSerialStr))
						//	iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + subjSerialStr.Trim();

						//if (!string.IsNullOrEmpty(subjMakerStr))
						//	iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + subjMakerStr.Trim();

						//if (!string.IsNullOrEmpty(subjNoteStr))
						//	iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjNoteStr.Trim();

						iTemSUBJ = iTemSUBJ.Trim();

						if (_itemDrwInt != -1)
						{
							descDrwStr = dt.Rows[i][_itemDrwInt].ToString().Trim();

							if(string.IsNullOrEmpty(descDrwStr))
							{
								if(_itemSheetNo != -1)
								{
									if (_itemSheetNo < _itemDrwInt - 1)
										descDrwStr = dt.Rows[i][_itemDrwInt - 1].ToString();
								}
							}

						}

						if (_itemSheetNo != -1)
						{
							descPlateSheetNo = dt.Rows[i][_itemSheetNo].ToString().Trim();

							if(string.IsNullOrEmpty(descPlateSheetNo))
							{
								if(_itemCodeInt < _itemSheetNo -1)
								{
									descPlateSheetNo = dt.Rows[i][_itemSheetNo - 1].ToString().Trim();
								}
							}
						}
						 
						if (iTemCode.Contains(descDrwStr))
							descDrwStr = string.Empty;

						if (!string.IsNullOrEmpty(descDrwStr))
							iTemDESC = iTemDESC.Trim() + Environment.NewLine + descDrwStr.Trim();

						if (!string.IsNullOrEmpty(descPlateSheetNo))
							iTemDESC = iTemDESC.Trim() + Environment.NewLine + descPlateSheetNo.Trim();


						iTemDESC = iTemDESC.ToLower().Replace("order line notes", "").Replace("\r\n\r\n","\r\n").Trim();

						iTemCode = iTemCode.ToLower().Replace("impa", "").Replace("null","").Trim();



						// 공백 제거 
						if (iTemDESC.Contains("\r\n "))
						{
							while (iTemDESC.Contains("\r\n "))
							{
								iTemDESC = iTemDESC.Replace("\r\n ", "\r\n").Trim();
							}
						}

						if (iTemSUBJ.Contains("\r\n "))
						{
							while (iTemSUBJ.Contains("\r\n "))
							{
								iTemSUBJ = iTemSUBJ.Replace("\r\n ", "\r\n").Trim();
							}
						}



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

						descDrwStr = string.Empty;
						
						//   }
					}
				}
			}
		}
	}
}

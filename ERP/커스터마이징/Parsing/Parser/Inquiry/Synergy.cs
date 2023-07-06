using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class Synergy
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

		#endregion ==================================================================================================== Constructor



		public Synergy(string fileName)
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
			string iTemRmk = string.Empty;
			string iTemDwg = string.Empty;
			string iTemCode2 = string.Empty;

			int _itemDesc = -1;
			int _itemQt = -1;
			int _itemDwg = -1;
			int _itemCode = -1;
			int _itemUnit = -1;
			int _itemRmk = -1;
			int _itemCode2 = -1;


			string subjStr = string.Empty;
			string subjStr2 = string.Empty;
			string subjStr3 = string.Empty;
			string modelStr = string.Empty;


			string subjTotal = string.Empty;

			string serialStr = string.Empty;


			try
			{
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

						if(string.IsNullOrEmpty(reference))
						{
							for(int c =0; c < dt.Columns.Count; c++)
							{
								if(dt.Rows[i][c].ToString().StartsWith("PR Number"))
								{
									for(int c2 = c+1; c2 < dt.Columns.Count; c2++)
									{
										if (string.IsNullOrEmpty(reference))
											reference = dt.Rows[i][c2].ToString().Trim();
									}
								}
							}
						}

						if(string.IsNullOrEmpty(vessel))
						{
							for(int c = 0; c < dt.Columns.Count; c++)
							{
								if(dt.Rows[i][c].ToString().StartsWith("Vessel"))
								{
									for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
									{
										if (string.IsNullOrEmpty(vessel))
											vessel = dt.Rows[i][c2].ToString().Trim();


									}
								}
							}

							if (vessel.Contains("-"))
							{
								string[] vesselSpl = vessel.Split('-');

								if (vesselSpl.Length > 1)
									vessel = vesselSpl[1].ToString().Trim();
							}
						}

						//if (firstColStr.Equals("PR Number"))
						//{
						//	//reference = dt.Rows[i][1].ToString().Trim();

						//	for (int c = 2; c < dt.Columns.Count; c++)
						//	{
						//		if (dt.Rows[i][c].ToString().Equals("Vessel"))
						//		{

						//			//if (string.IsNullOrEmpty(reference))
						//			//{
						//			//	reference = dt.Rows[i][c - 1].ToString().Trim();
						//			//}

						//			vessel = dt.Rows[i][c + 1].ToString().Trim();

						//			if (vessel.Contains("-"))
						//			{
						//				string[] vesselSpl = vessel.Split('-');

						//				if (vesselSpl.Length > 1)
						//					vessel = vesselSpl[1].ToString().Trim();
						//			}
						//		}
						//	}
						//}
						//else 
						if (firstColStr.Equals("PR Description"))
						{
							for (int c = 1; c < dt.Columns.Count; c++)
							{
								subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
							}
						}
						else if (firstColStr.Equals("Machine Name"))
						{
							for (int c = 1; c < dt.Columns.Count; c++)
							{
								subjStr2 = subjStr2.Trim() + " " + dt.Rows[i][c].ToString().Trim();
								if (string.IsNullOrEmpty(dt.Rows[i + 1][0].ToString()))
									subjStr2 = subjStr2.Trim() + " " + dt.Rows[i][c].ToString().Trim() + " " + dt.Rows[i + 1][c].ToString().Trim();

								if(dt.Rows[i][c].ToString().ToLower().Contains("model"))
								{
								//	modelStr = dt.Rows[i][c + 1].ToString().Trim() + " " + dt.Rows[i][c + 2].ToString().Trim() + " " + dt.Rows[i][c + 3].ToString().Trim() + " " + dt.Rows[i][c + 4].ToString().Trim();
								}
							}

							//while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
							//{
							//    for (int c = 1; c < dt.Columns.Count; c++)
							//    {
							//        subjStr2 = subjStr2.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
							//    }

							//    _i += 1;

							//    if (_i >= dt.Rows.Count)
							//        break;

							//    if (dt.Rows[_i][0].ToString().Equals("SNo"))
							//        break;
							//}
						}
						else if (firstColStr.Equals("Maker"))
						{
							for (int c = 0; c < dt.Columns.Count; c++)
							{
								subjStr3 = subjStr3.Trim() + " " + dt.Rows[i][c].ToString().Trim();
							}

							int _i = i + 1;

							while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
							{
								for (int c = 1; c < dt.Columns.Count; c++)
									subjStr3 = subjStr3.Trim() + " " + dt.Rows[_i][c].ToString().Trim();

								_i += 1;

								if (_i >= dt.Rows.Count)
									break;

								if (dt.Rows[_i][0].ToString().Equals("SNo"))
									break;
							}
						}
						else if (firstColStr.Equals("SNo"))
						{
							for (int c = 1; c < dt.Columns.Count; c++)
							{
								if (dt.Rows[i][c].ToString().Equals("Item Description")) _itemDesc = c;
								else if (dt.Rows[i][c].ToString().Equals("Drawing No.")) _itemDwg = c;
								else if (dt.Rows[i][c].ToString().Equals("Component") || dt.Rows[i][c].ToString().ToLower().StartsWith("product")) _itemCode = c;
								else if (dt.Rows[i][c].ToString().Equals("Req")) _itemQt = c;
								else if (dt.Rows[i][c].ToString().Equals("Uom")) _itemUnit = c;
								else if (dt.Rows[i][c].ToString().Equals("Remarks")) _itemRmk = c;
								else if (dt.Rows[i][c].ToString().Equals("Item No")) _itemCode2 = c;
							}
						}
						else if (GetTo.IsInt(firstColStr))
						{

							// row 값 가져와서 배열에 넣은후 값 추가하기
							//                     string[] rowValueSpl = new string[20];
							//                     int columnCount = 0;
							//                     for (int c = 0; c < dt.Columns.Count; c++)
							//                     {
							//                         if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
							//                         {
							//                             rowValueSpl[columnCount] = c.ToString();
							//                             columnCount++;
							//                         }
							//                     }


							//                     if (rowValueSpl[4] != null && rowValueSpl[5] == null)
							//                     {
							//                         _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
							//                         _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
							//                         _itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
							//                         _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
							//                         _itemRmk = _itemUnit + 1;

							//                         iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

							//                         if (!GetTo.IsInt(iTemQt))
							//                         {
							//                             _itemCode = -1;
							//                             _itemQt = Convert.ToInt16(rowValueSpl[2].ToString());
							//                             _itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
							//                             _itemRmk = Convert.ToInt16(rowValueSpl[4].ToString());
							//                         }
							//                     }
							//else if (rowValueSpl[5] != null && rowValueSpl[6] == null)
							//{
							//	_itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
							//	_itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
							//	_itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
							//	_itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
							//	_itemRmk = _itemUnit + 1;
							//}
							//else if(rowValueSpl[6] != null && rowValueSpl[7] == null)
							//{
							//	_itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
							//	_itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
							//                         _itemDwg = Convert.ToInt16(rowValueSpl[3].ToString());
							//                         _itemCode2 = Convert.ToInt16(rowValueSpl[4].ToString());
							//	_itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
							//	_itemUnit = Convert.ToInt16(rowValueSpl[6].ToString());
							//	_itemRmk = _itemUnit + 1;
							//}

							if (!_itemDesc.Equals(-1))
							{
								for (int c = 1; c <= _itemDesc; c++)
								{
									iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
								}

								int _i = i + 1;

								while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
								{
									for (int c = 1; c <= _itemDesc; c++)
									{
										iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
									}

									_i += 1;

									if (_i >= dt.Rows.Count)
										break;
								}

								iTemDESC = iTemDESC.Trim();
							}

							if (!_itemCode.Equals(-1))
								iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

							if (!_itemQt.Equals(-1))
								iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

							if (!_itemUnit.Equals(-1))
								iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

							if (string.IsNullOrEmpty(iTemQt) && GetTo.IsInt(iTemUnit))
							{
								if (!_itemQt.Equals(-1))
									iTemQt = dt.Rows[i][_itemQt + 1].ToString().Trim();

								if (!_itemUnit.Equals(-1))
									iTemUnit = dt.Rows[i][_itemUnit + 1].ToString().Trim();
							}


							if (!_itemRmk.Equals(-1))
							{
								if (_itemRmk > _itemUnit + 1)
								{
									for (int c = _itemUnit + 1; c <= _itemRmk; c++)
										iTemRmk = iTemRmk.Trim() + " " + dt.Rows[i][c].ToString().Trim();

									int _ii = i + 1;

									while (string.IsNullOrEmpty(dt.Rows[_ii][0].ToString()))
									{
										for (int c = _itemUnit; c <= _itemRmk; c++)
											iTemRmk = iTemRmk.Trim() + " " + dt.Rows[_ii][c].ToString().Trim();

										_ii += 1;

										if (_ii >= dt.Rows.Count)
											break;
									}
								}
								else
								{
									iTemRmk = dt.Rows[i][_itemRmk].ToString().Trim();

									int _i = i + 1;

									while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
									{
										for (int c = 3; c < dt.Columns.Count; c++)
											iTemRmk = iTemRmk.Trim() + " " + dt.Rows[_i][c].ToString().Trim();

										_i += 1;

										if (_i >= dt.Rows.Count)
											break;
									}
								}
							}

							if (!_itemCode2.Equals(-1))
								iTemCode2 = dt.Rows[i][_itemCode2].ToString().Trim();

							if (!string.IsNullOrEmpty(subjStr))
								subjTotal = subjStr.Trim();

							if (!string.IsNullOrEmpty(subjStr2))
								subjTotal = subjTotal.Trim() + Environment.NewLine + subjStr2.Trim();

							if (!string.IsNullOrEmpty(subjStr3))
								subjTotal = subjTotal.Trim() + subjStr3.Trim();


							if (!string.IsNullOrEmpty(subjTotal))
								iTemSUBJ = subjTotal.Replace("Model", "\r\nModel:").Replace("Address", "").Replace("Maker", "\r\nMaker:").Replace("Serial No", "\r\nSerial No").Replace("Address","").Trim();

							if (iTemSUBJ.EndsWith("Serial No"))
								iTemSUBJ = iTemSUBJ.Replace("\r\nSerial No", "").Trim();



							int idx_s = iTemSUBJ.IndexOf("Serial No");

							if (idx_s > 0)
							{
								serialStr = iTemSUBJ.Substring(idx_s, iTemSUBJ.Length - idx_s).Trim();

								if (serialStr.Contains("602"))
								{
									idx_s = serialStr.IndexOf("602");

									serialStr = serialStr.Substring(0, idx_s).Trim();
								}
							}

							idx_s = iTemSUBJ.IndexOf("Year Of");



							if (idx_s > 0)
								iTemSUBJ = iTemSUBJ.Substring(0, idx_s).Trim();

							if (!string.IsNullOrEmpty(serialStr))
								iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + serialStr.Trim();

							if(!string.IsNullOrEmpty(modelStr))
								iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + modelStr.ToLower().Replace("model", "").Trim();



							if (!_itemDwg.Equals(-1))
							{
								iTemDwg = dt.Rows[i][_itemDwg].ToString().Trim();

								int _i = i + 1;

								while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
								{
									iTemDwg = iTemDwg + dt.Rows[_i][_itemDwg].ToString().Trim();

									_i += 1;

									if (_i >= dt.Rows.Count)
										break;
								}
							}

							if (!string.IsNullOrEmpty(iTemDwg))
								iTemDESC = iTemDESC.Trim() + Environment.NewLine + "DWG NO: " + iTemDwg.Trim();

							if (!string.IsNullOrEmpty(iTemCode2))
								iTemDESC = iTemDESC.Trim() + Environment.NewLine + "ITEM NO: " + iTemCode2.Trim();

							if (!string.IsNullOrEmpty(iTemRmk.Trim()))
								iTemDESC = iTemDESC.Trim() + Environment.NewLine + iTemRmk.Trim();


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
							iTemDwg = string.Empty;
							iTemSUBJ = string.Empty;
							iTemRmk = string.Empty;
							iTemCode2 = string.Empty;

							subjStr = string.Empty;
							subjStr2 = string.Empty;
							subjStr3 = string.Empty;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Util.ShowMessage(ex.Message);
			}
		}
	}
}

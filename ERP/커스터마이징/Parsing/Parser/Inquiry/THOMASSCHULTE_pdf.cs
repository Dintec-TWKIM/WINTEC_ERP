using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class THOMASSCHULTE_pdf
	{
		string vessel;
		string reference;
		DataTable dtItem;
		string contact;
		string imoNumber;

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

		public string Contact
		{
			get
			{
				return contact;
			}
		}

		public string Imonumber
		{
			get
			{
				return imoNumber;
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



		public THOMASSCHULTE_pdf(string fileName)
		{
			vessel = "";                        // 선명
			reference = "";                     // 문의번호
			contact = string.Empty;
			imoNumber = string.Empty;

			dtItem = new DataTable();
			dtItem.Columns.Add("NO");           // 순번
			dtItem.Columns.Add("SUBJ");         // 주제
			dtItem.Columns.Add("ITEM");         // 품목코드
			dtItem.Columns.Add("DESC");         // 품목명
			dtItem.Columns.Add("UNIT");         // 단위
			dtItem.Columns.Add("QT");           // 수량
			dtItem.Columns.Add("UNIQ");         //선사코드
			this.fileName = fileName;
			this.uc = new UnitConverter();
		}

		public void Parse()
		{
			int _itemDesc = -1;
			int _itemQt = -1;
			int _itemUnit = -1;
			int _itemCode = -1;

			int _colNo = -1;

			string iTemNo = string.Empty;
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;
			string iTemUniq = string.Empty;

			string subjStr = string.Empty;
			string componentStr = string.Empty;
			string makerStr = string.Empty;
			string typeStr = string.Empty;
			string serialStr = string.Empty;
			string dwgStr = string.Empty;


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


			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					if (_colNo == -1)
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							for (int r = 0; r < dt.Rows.Count; r++)
							{
								if (dt.Rows[r][c].ToString().StartsWith("10000"))
									_colNo = c;
							}
						}
					}

					if(string.IsNullOrEmpty(subjStr))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							for (int r = 0; r < dt.Rows.Count; r++)
							{
								if (dt.Rows[r][c].ToString().ToLower().StartsWith("details of equipment"))
								{
									if(string.IsNullOrEmpty(subjStr))
									{
										int _i = r + 1;

										if (_i >= dt.Rows.Count)
											break;

										while(!dt.Rows[_i][c].ToString().ToLower().StartsWith("no."))
										{
											subjStr = subjStr.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
											_i += 1;

											if (_i >= dt.Rows.Count)
												break;
										}
									}
								}
							}
						}
					}

					string firstStr = dt.Rows[i][0].ToString().ToLower();
					string secondStr = string.Empty;

					if(_colNo != -1)
					{
						secondStr = dt.Rows[i][_colNo].ToString().ToLower();
					}

					if(string.IsNullOrEmpty(reference))
					{
						for(int c =0; c < dt.Columns.Count; c++)
						{
							if(dt.Rows[i][c].ToString().ToLower().StartsWith("our inquiry"))
							{
								reference = dt.Rows[i][c].ToString().Replace("Our","").Replace("Inquiry","").Trim();
							}
						}
					}

					if(string.IsNullOrEmpty(imoNumber))
					{
						for(int c = 0; c < dt.Columns.Count; c++)
						{
							if(dt.Rows[i][c].ToString().ToLower().Contains("imo"))
							{
								imoNumber = dt.Rows[i][c].ToString().ToLower().Trim();
								int idx_s = imoNumber.IndexOf("imo");
								
								if(idx_s != -1)
									imoNumber = imoNumber.Substring(idx_s, imoNumber.Length - idx_s).Replace("imo","").Trim();
							}
						}
					}


					if(firstStr.StartsWith("no."))
					{
						string forStr = string.Empty;

						for(int c = 1; c < dt.Columns.Count; c++)
						{
							forStr = dt.Rows[i][c].ToString().ToLower();

							if (forStr.Contains("item/article")) _itemDesc = c;
							else if (forStr.Contains("unit")) _itemUnit = c;
							else if (forStr.Contains("quantity")) _itemQt = c;
						}
					}
					else  if ((GetTo.IsInt(secondStr) && secondStr.Length > 4) || (GetTo.IsInt(firstStr) && firstStr.Length > 4))
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


						if (rowValueSpl[3] != null && rowValueSpl[4] == null)
						{

							_itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
							_itemQt = Convert.ToInt16(rowValueSpl[2].ToString());
							_itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
						}

						if (_itemUnit != -1)
							iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

						if (_itemQt != -1)
							iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

						if (_itemDesc != -1)
						{
							iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

							int _i = i + 1;

							if (_i >= dt.Rows.Count)
								break;

							if (!GetTo.IsInt(dt.Rows[_i][_colNo].ToString()))
							{
								if (_colNo != _itemDesc)
									iTemDESC = iTemDESC + Environment.NewLine + dt.Rows[_i][_colNo].ToString().Trim() + dt.Rows[_i][_itemDesc].ToString().Trim();
								else
									iTemDESC = iTemDESC + Environment.NewLine + dt.Rows[_i][_colNo].ToString().Trim();
							}
						}

						if (subjStr.EndsWith("Comments:"))
						{
							int idx_s = subjStr.IndexOf("Comments:");

							if (idx_s != -1)
								subjStr = subjStr.Substring(0, idx_s).Trim();
						}


						if (!string.IsNullOrEmpty(subjStr))
							iTemSUBJ = subjStr;

						//ITEM ADD START
						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = "";
						dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
						if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
						// 주제가 없는 경우가 있음, 없을때는 FOR 제거
						if (!string.IsNullOrEmpty(iTemSUBJ))
							dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ.Trim();
						dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;

						iTemDESC = string.Empty;
						iTemUnit = string.Empty;
						iTemQt = string.Empty;
						iTemCode = string.Empty;
					}
				}
			}
		}
	}
}

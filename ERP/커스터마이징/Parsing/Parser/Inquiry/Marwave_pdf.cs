using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class Marwave_pdf
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



		public Marwave_pdf(string fileName)
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

			string iTemNo = string.Empty;
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;
			string iTemUniq = string.Empty;

			string subjStr = string.Empty;
			string componentStr = string.Empty;


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
					string firstStr = dt.Rows[i][0].ToString();
					string secondStr = dt.Rows[i][1].ToString();


					if(string.IsNullOrEmpty(reference))
					{
						for(int c =0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().StartsWith("RFQ N"))
							{
								reference = dt.Rows[i][c].ToString().Trim();
							}
						}

						if (!string.IsNullOrEmpty(reference))
							reference = reference.Replace("RFQ N°:", "").Trim();
					}

					if(string.IsNullOrEmpty(vessel))
					{
						for(int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().StartsWith("M/T"))
								vessel = dt.Rows[i][c].ToString().Replace("M/T", "").Trim();
						}
					}

					if(firstStr.StartsWith("Object details"))
					{

						componentStr = string.Empty;
						int _i = i + 1;

						while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
						{
							for(int c = 0; c < dt.Columns.Count; c++)
							{
								componentStr = componentStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
							}

							_i += 1;

							if (_i >= dt.Rows.Count)
								break;
						}

						if(!string.IsNullOrEmpty(componentStr))
						{
							componentStr = componentStr.Replace("Hierarchy code:", "\r\nHierarchy code:").Trim();
							componentStr = componentStr.Replace("Code:", "\r\nCode:").Replace("Model:","\r\nModel:").Trim();
							componentStr = componentStr.Replace("Instr. Book:", "\r\nInstr. Book:").Replace("Sr No:", "\r\nSr No:").Trim();
							componentStr = componentStr.Replace("Maker:", "\r\nMaker:").Replace("Location:", "\r\nLocation:").Trim();
						}
					}
					else if (firstStr.StartsWith("Subject"))
					{
						subjStr = string.Empty;

						for (int c = 0; c < dt.Columns.Count; c++)
							subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();

						if (!string.IsNullOrEmpty(subjStr))
							subjStr = subjStr.Replace("Subject", "").Replace(":", "").Trim();
					}
					else if (firstStr.StartsWith("No"))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("Account No")) _itemCode = c;
							else if (dt.Rows[i][c].ToString().StartsWith("Q.ty")) _itemQt = c;
							else if (dt.Rows[i][c].ToString().StartsWith("Item")) _itemDesc = c;
							else if (dt.Rows[i][c].ToString().StartsWith("UOM")) _itemUnit = c;
						}
					}
					else if (GetTo.IsInt(firstStr) || (string.IsNullOrEmpty(firstStr) && GetTo.IsInt(secondStr)))
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
							_itemUnit = Convert.ToInt16(rowValueSpl[2].ToString());
							_itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
						}

						if(_itemDesc != -1)
						{
							iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

							int _i = i + 1;

							while(!GetTo.IsInt(dt.Rows[_i][0].ToString()))
							{
								for(int c = 0; c <= _itemDesc; c++)
								{
									iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
								}

								if(iTemDESC.Contains("Item Code:"))
								{
									int idx_s = iTemDESC.IndexOf("Item Code:");

									if(idx_s != -1)
									{
										iTemCode = iTemDESC.Substring(idx_s, iTemDESC.Length - idx_s).Trim();

										iTemDESC = iTemDESC.Replace(iTemCode, "").Trim();

										iTemCode = iTemCode.Replace("Item Code:", "").Trim();
									}
								}

								_i += 1;

								if (_i >= dt.Rows.Count)
									break;

								if (dt.Rows[_i][0].ToString().Contains("Printed on") || dt.Rows[_i][0].ToString().Contains("Quotation to be received"))
									break;
							}
						}

						//if (_itemDesc != -1)
						//{
						//	for (int c = _itemDesc; c < _itemQt-1; c++)
						//	{
						//		iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
						//	}

						//	int _i = i + 1;

						//	while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || dt.Rows[_i][0].ToString().StartsWith("SHIP Notes:") || dt.Rows[_i][0].ToString().StartsWith("Lined Notes:"))
						//	{

						//		for (int c = _itemDesc; c < _itemQt-1; c++)
						//		{
						//			if (dt.Rows[_i][c].ToString().StartsWith("Part No.:"))
						//				iTemCode = dt.Rows[_i][c].ToString().Trim();
						//			else
						//				iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
						//		}

						//		_i += 1;

						//		if (_i >= dt.Rows.Count)
						//			break;
						//	}
						//}

						if (_itemQt != -1)
							iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

						if (_itemUnit != -1)
							iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

						if (!string.IsNullOrEmpty(iTemCode))
							iTemCode = iTemCode.Replace("Part No", "").Replace(".:", "").Trim();

						if (!string.IsNullOrEmpty(subjStr))
							iTemSUBJ = subjStr.Trim();

						if (!string.IsNullOrEmpty(componentStr))
							iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + componentStr.Trim();

						if(!string.IsNullOrEmpty(iTemDESC))
						{
							iTemDESC = iTemDESC.Replace("Dwg", "\r\nDwg").Trim();
							iTemDESC = iTemDESC.Replace("Remarks", "\r\nRemarks").Trim();
							iTemDESC = iTemDESC.Replace("Line Notes", "\r\nLine Notes").Trim();
							iTemDESC = iTemDESC.Replace("SHIP Notes", "\r\nSHIP Notes").Trim();

						}

						//ITEM ADD START
						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstStr;
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

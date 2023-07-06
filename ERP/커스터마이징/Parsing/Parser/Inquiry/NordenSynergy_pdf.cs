using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class NordenSynergy_pdf
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



		public NordenSynergy_pdf(string fileName)
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
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;

			int _itemDescInt = -1;
			int _itemUnitInt = -1;
			int _itemQtInt = -1;
			int _itemCodeInt = -1;


			string subjStr = string.Empty;






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
					string firstColStr = dt.Rows[i][0].ToString().ToLower();


					if (firstColStr.Contains("vessel"))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (string.IsNullOrEmpty(vessel))
								vessel = dt.Rows[i][c].ToString().Trim();
							else
								break;
						}
					}
					else if (firstColStr.Contains("rfq no."))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (string.IsNullOrEmpty(reference))
								reference = dt.Rows[i][c].ToString().Trim();
							else
								break;
						}
					}
					else if (firstColStr.Contains("imo number"))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (string.IsNullOrEmpty(imoNumber))
								imoNumber = dt.Rows[i][c].ToString().Trim();
							else
								break;
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
					else if (firstColStr.Equals("item"))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().Contains("item description")) _itemDescInt = c;
							else if (dt.Rows[i][c].ToString().ToLower().Contains("item no.")) _itemCodeInt = c;
							else if (dt.Rows[i][c].ToString().ToLower().Equals("unit")) _itemUnitInt = c;
							else if (dt.Rows[i][c].ToString().ToLower().Contains("qty.")) _itemQtInt = c;
							
						}
					}
					else if (GetTo.IsInt(firstColStr.Replace(".","")))
					{
						if (!_itemDescInt.Equals(-1))
						{
							iTemDESC = dt.Rows[i][_itemDescInt].ToString().Trim();

							int _i = i + 1;

							while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
							{
								iTemDESC = iTemDESC + " " + dt.Rows[_i][_itemDescInt].ToString().Trim();

								_i += 1;

								if (_i >= dt.Rows.Count)
									break;
							}
						}

						if (_itemCodeInt != -1)
						{
							iTemCode = dt.Rows[i][_itemCodeInt].ToString().Trim();

							int _i = i + 1;

							while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
							{
								iTemCode = iTemCode + Environment.NewLine + dt.Rows[_i][_itemCodeInt].ToString().Trim();
								_i += 1;

								if (_i >= dt.Rows.Count)
									break;
							}
						}


						if (!_itemUnitInt.Equals(-1))
							iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();

						if (!_itemQtInt.Equals(-1))
							iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();


						if (!string.IsNullOrEmpty(subjStr))
							iTemSUBJ = subjStr.Trim();


						iTemSUBJ = iTemSUBJ.Trim();

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
						dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr.Replace(".","");
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

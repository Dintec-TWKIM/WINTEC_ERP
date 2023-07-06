using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class Pronav_pdf
	{
		string vessel;
		string reference;
		string partner;
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

		public string Partner
		{
			get
			{
				return partner;
			}
		}

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		public string ImoNumber
		{
			get
			{
				return imoNumber;
			}
		}

		#endregion ==================================================================================================== Constructor



		public Pronav_pdf(string fileName)
		{
			vessel = "";                        // 선명
			reference = "";                     // 문의번호
			partner = "";                       // 매입처 담당자
			imoNumber = "";                     // 호선번호

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

			string subjStr = string.Empty;

			int _itemQt = -1;
			int _itemDesc = -1;
			int _itemUnit = -1;
			int _itemCode = -1;






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



			// xml row 나누기
			string[] xmlSpl = { };

			if (!string.IsNullOrEmpty(xmlTemp))
			{
				xmlSpl = xmlTemp.Split(new string[] { "<row>" }, StringSplitOptions.None);
			}

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


					if (firstColStr.StartsWith("vessel"))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
								if (string.IsNullOrEmpty(vessel))
								{
									vessel = dt.Rows[i][c].ToString().Replace(":", "").Trim();
								}
								else
									break;
						}
					}


					//if (string.IsNullOrEmpty(imoNumber))
					//{
					//	for (int c = 0; c < dt.Columns.Count; c++)
					//	{
					//		if (dt.Rows[i][c].ToString().Contains("IMO #"))
					//		{
					//			for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
					//			{
					//				if (string.IsNullOrEmpty(imoNumber))
					//				{
					//					imoNumber = dt.Rows[i][c2].ToString().Replace(":", "").Trim();
					//				}
					//				else
					//					break;
					//			}
					//		}
					//	}
					//}


					if (firstColStr.StartsWith("request for quotation"))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (string.IsNullOrEmpty(reference))
							{
								reference = dt.Rows[i][c].ToString().ToLower().Replace("request for quotation", "").Trim();
							}
							else
								break;
						}
					}


					if (firstColStr.Equals("item"))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Equals("Aricle")) _itemDesc = c;
							else if (dt.Rows[i][c].ToString().Equals("Order Ref.")) _itemCode = c;
							else if (dt.Rows[i][c].ToString().Equals("Qty.")) _itemQt = c;
							//else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
						}
					}
					else if (GetTo.IsInt(firstColStr))
					{

						iTemNo = firstColStr;


						if(_itemQt != -1)
						{
							string[] qtSpl = dt.Rows[i][_itemQt].ToString().Split(' ');

							if(qtSpl.Length > 0)
							{
								iTemQt = qtSpl[0].ToString().Trim();
								iTemUnit = qtSpl[1].ToString().Remove('(').Remove(')').Trim();
							}
						}


						if (_itemDesc != -1)
						{
							iTemDESC = dt.Rows[i][_itemDesc].ToString();
						}


						if (!string.IsNullOrEmpty(iTemUnit))
						{
							if (xmlTemp.Contains(iTemUnit))
							{
								for (int r = 0; r < xmlSpl.Length; r++)
								{
									if (xmlSpl[r].ToString().Contains(iTemUnit))
									{
										string xmlRow_1 = xmlSpl[r].ToString();

										string[] xmlSplUnit = xmlRow_1.Split(new string[] { "<cell>" }, StringSplitOptions.None);

										if (xmlSplUnit.Length == 5)
										{
											iTemDESC = xmlSplUnit[2].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
											iTemQt = xmlSplUnit[3].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
											//iTemUnit = xmlSplUnit[4].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
										}
										else
										{
											iTemCode = xmlSplUnit[2].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Replace("PLATE", "").Trim();
											iTemDESC = xmlSplUnit[3].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
											iTemQt = xmlSplUnit[4].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
											//iTemUnit = xmlSplUnit[5].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
										}
										xmlSpl[r] = "";
										break;
									}

								}
							}
						}
						else if (!string.IsNullOrEmpty(iTemDESC))
						{
							if (xmlTemp.Contains(iTemDESC))
							{
								for (int r = 0; r < xmlSpl.Length; r++)
								{
									if (xmlSpl[r].ToString().Contains(iTemDESC))
									{
										string xmlRow_1 = xmlSpl[r].ToString();

										string[] xmlSplDesc = xmlRow_1.Split(new string[] { "<cell>" }, StringSplitOptions.None);

										if (xmlSplDesc.Length == 5)
										{
											iTemCode = xmlSplDesc[2].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Replace("PLATE", "").Trim();
											iTemDESC = xmlSplDesc[3].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
											iTemQt = xmlSplDesc[4].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();

										}
										else
										{
											iTemCode = xmlSplDesc[2].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Replace("PLATE", "").Trim();
											iTemDESC = xmlSplDesc[3].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
											iTemQt = xmlSplDesc[4].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
											iTemUnit = xmlSplDesc[5].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
										}

										xmlSpl[r] = "";
										break;
									}
								}
							}
						}
						else
						{
							if (_itemDesc != -1)
								iTemDESC = dt.Rows[i - 1][_itemDesc].ToString();

							if (!string.IsNullOrEmpty(iTemDESC))
							{
								if (xmlTemp.Contains(iTemDESC))
								{
									for (int r = 0; r < xmlSpl.Length; r++)
									{
										if (xmlSpl[r].ToString().Contains(iTemDESC))
										{
											string xmlRow_1 = xmlSpl[r].ToString();

											string[] xmlSplDesc = xmlRow_1.Split(new string[] { "<cell>" }, StringSplitOptions.None);

											if (xmlSplDesc.Length == 4)
											{
												//iTemCode = xmlSplDesc[1].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Replace("PLATE", "").Trim();
												iTemDESC = xmlSplDesc[2].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
												iTemQt = xmlSplDesc[3].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
											}
											else if (xmlSplDesc.Length == 5)
											{
												iTemCode = xmlSplDesc[2].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Replace("PLATE", "").Trim();
												iTemDESC = xmlSplDesc[3].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
												iTemQt = xmlSplDesc[4].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();

											}
											else
											{
												iTemCode = xmlSplDesc[2].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Replace("PLATE", "").Trim();
												iTemDESC = xmlSplDesc[3].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
												iTemQt = xmlSplDesc[4].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
												iTemUnit = xmlSplDesc[5].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Replace("</row>", "").Trim();
											}

											xmlSpl[r] = "";
											break;
										}
									}
								}
							}
						}





						if (!string.IsNullOrEmpty(subjStr))
							iTemSUBJ = subjStr.Trim();


						//ITEM ADD START
						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
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
					}
				}
			}
		}
	}
}

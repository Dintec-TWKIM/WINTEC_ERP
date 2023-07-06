using Dintec;
using Dintec.Parser;
using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parsing
{
	class 보성_pdf
	{
		string vessel;
		string reference;
		string imoNumber;
		DataTable dtItem;

		string fileName;


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

		public string ImoNumber
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



		public 보성_pdf(string fileName)
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
			dtItem.Columns.Add("UNIQ");           // 수량
			this.fileName = fileName;
		}

		public void Parse()
		{
			string iTemNo = string.Empty;
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;

			int _itemDesc = -1;
			int _itemCode = -1;
			int _itemQt = -1;
			int _itemUnit = -1;

			string subjStr = string.Empty;

			bool itemStart = false;

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


			//Regex regex = new Regex(@"^[a-z]*[0-9]\.\s*");
			Regex regex = new Regex(@".+\.");

			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string firstColStr = dt.Rows[i][0].ToString();


					if (!itemStart)
					{
						if (string.IsNullOrEmpty(reference))
						{
							for (int c = 1; c < dt.Columns.Count; c++)
							{
								if (dt.Rows[i][c].ToString().StartsWith("OUR REF NO"))
								{
									reference = dt.Rows[i][c + 1].ToString().Trim();

									if (string.IsNullOrEmpty(reference))
									{
										reference = dt.Rows[i][c + 2].ToString().Trim();
									}
								}

							}
						}

						if (firstColStr.StartsWith("SUBJECT"))
						{
							for (int c = 0; c < dt.Columns.Count; c++)
							{
								if (string.IsNullOrEmpty(vessel))
									vessel = dt.Rows[i][c + 1].ToString().Trim();
								else
									break;
							}
						}
						else if (firstColStr.StartsWith("NO."))
						{
							itemStart = true;

							string lineStr = string.Empty;
							for (int c = 0; c < dt.Columns.Count; c++)
							{
								lineStr = dt.Rows[i][c].ToString().ToLower().Trim();

								if (lineStr.Contains("description")) _itemDesc = c;
								else if (lineStr.StartsWith("code")) _itemCode = c;
								else if (lineStr.StartsWith("q'ty")) _itemQt = c;
								else if (lineStr.StartsWith("unit")) _itemUnit = c;
							}



							int _i = i + 1;


							subjStr = string.Empty;

							while (!regex.IsMatch(dt.Rows[_i][0].ToString()) || (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && !GetTo.IsInt(dt.Rows[_i][0].ToString().Substring(0, 1))))
							{

								subjStr = subjStr + Environment.NewLine + dt.Rows[_i][0].ToString().Trim() + dt.Rows[_i][1].ToString().Trim();

								_i += 1;
								i += 1;

								if (_i >= dt.Rows.Count)
									break;

								if (regex.IsMatch(dt.Rows[_i][0].ToString()))
								{
									if (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && GetTo.IsInt(dt.Rows[_i][0].ToString().Substring(0, 1).Trim()))
										break;
								}


							}


						}
					}
					else // 아이템 라인 시작
					{
						if (!string.IsNullOrEmpty(firstColStr))
						{
							if (regex.IsMatch(firstColStr))
							{
								if (!string.IsNullOrEmpty(firstColStr) && !GetTo.IsInt(firstColStr.Substring(0, 1).Trim()))
								{

								}
								else
								{

									// UNIT QTY
									if (!_itemQt.Equals(-1))
									{
										iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

										if (string.IsNullOrEmpty(iTemQt))
											iTemQt = dt.Rows[i][_itemQt + 1].ToString().Trim();
									}

									// CODE
									if (!_itemCode.Equals(-1))
										iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

									// UNIT
									if (_itemUnit != -1)
										iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

									// DESC
									if (!_itemDesc.Equals(-1))
									{
										iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

										int _i = i + 1;

										while (!regex.IsMatch(dt.Rows[_i][0].ToString()))
										{
											iTemDESC = iTemDESC + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim();

											_i += 1;

											if (_i >= dt.Rows.Count)
												break;
										}
									}


									if (!string.IsNullOrEmpty(subjStr))
										iTemSUBJ = subjStr.Trim();

									if (!string.IsNullOrEmpty(iTemCode))
										iTemDESC = iTemDESC.Trim() + Environment.NewLine + iTemCode.Trim();


									//ITEM ADD START
									dtItem.Rows.Add();
									dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr.Replace(".","");
									dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
									dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = iTemUnit;
									if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
									if (!string.IsNullOrEmpty(iTemSUBJ))
										dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
									dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

									iTemDESC = string.Empty;
									iTemUnit = string.Empty;
									iTemCode = string.Empty;
									iTemQt = string.Empty;
									iTemSUBJ = string.Empty;

									//subjStr = string.Empty;
								}
							}
						}
					}
				}
			}
		}
	}
}

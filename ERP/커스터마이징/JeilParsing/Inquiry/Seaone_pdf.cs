using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace OutParsing
{
	class Seaone_pdf
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



		public Seaone_pdf(string fileName)
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


			string[] xmlSpl = { };

			if (!string.IsNullOrEmpty(xmlTemp))
			{
				xmlSpl = xmlTemp.Split(new string[] { "<row>" }, StringSplitOptions.None);
			}

			for (int r = 0; r < xmlSpl.Length; r++)
			{
				//string parttern = @"<cell\s*/>\r\n\s*<cell>(\d+)\.</cell>";
				string parttern = @"<cell\s*(?:colspan="".+?"")?>\s*(\d+)\.</cell>";
				Regex regexM = new Regex(parttern);
				Match match = regexM.Match(xmlSpl[r].ToString());


				if (match.Success)
				{
					string getStr = xmlSpl[r].ToString().Replace("</cell>", "\\").Trim();

					string patternCell = "<cell"; // "<cell " 문자열을 찾는 정규식

					int count = Regex.Matches(getStr, patternCell).Count;

					string pattern = @"<[^>]+>";
					string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

					while (result.Contains("  "))
					{
						result = result.Replace("  ", " ").Trim();
					}

					string[] getSpl = result.Split(new string[] { "\\" }, StringSplitOptions.None);




					if (count > 4)
					{
						iTemNo = getSpl[0].ToString().Trim();
						iTemQt = getSpl[getSpl.Length - 3].ToString().Trim();
						iTemUnit = getSpl[getSpl.Length - 2].ToString().Trim();

						for (int c = 1; c < getSpl.Length - 3; c++)
							iTemDESC = iTemDESC.Trim() + Environment.NewLine + getSpl[c].ToString().Trim();


						int _r = r + 1;

						match = regexM.Match(xmlSpl[_r].ToString());

						if (!match.Success)
						{
							while (!match.Success)
							{
								getStr = xmlSpl[_r].ToString().Replace("</cell>", "\\").Trim();

								if (!getStr.Contains("담당"))
								{

									pattern = @"<[^>]+>";
									result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

									while (result.Contains("  "))
									{
										result = result.Replace("  ", " ").Trim();
									}

									getSpl = result.Split(new string[] { "\\" }, StringSplitOptions.None);

									for (int c = 0; c < getSpl.Length - 1; c++)
									{
										iTemDESC = iTemDESC + " " + getSpl[c].ToString().Trim();
									}

									_r = _r + 1;

									if (_r >= xmlSpl.Length)
										break;

									match = regexM.Match(xmlSpl[_r].ToString());
								}
								else
									break;
							}
						}
						else
						{

						}


						if (!string.IsNullOrEmpty(subjStr))
							iTemSUBJ = subjStr.Trim();



						//ITEM ADD START
						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo.Replace(".", "").Trim();
						dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC.Trim();
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

						subjStr = string.Empty;
					}
					else
					{

					}
				}


			}


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
								if (dt.Rows[i][c].ToString().StartsWith("서류번호"))
								{
									reference = dt.Rows[i][c + 1].ToString().Trim();

									if (string.IsNullOrEmpty(reference))
									{
										reference = dt.Rows[i][c + 2].ToString().Trim();
									}
								}

							}
						}

						if (firstColStr.StartsWith("선"))
						{
							for (int c = 0; c < dt.Columns.Count; c++)
							{
								if (string.IsNullOrEmpty(vessel))
									vessel = dt.Rows[i][c + 1].ToString().Replace("명", "").Replace(":", "").Trim();
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
								else if (lineStr.StartsWith("q'ty")) _itemQt = c;
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

								if (GetTo.IsInt(dt.Rows[_i][0].ToString()))
								{
									if (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && GetTo.IsInt(dt.Rows[_i][0].ToString().Substring(0, 1).Trim()))
										break;
								}
							}

							if (dtItem.Rows.Count > 0)
							{
								for (int r = 0; r < dtItem.Rows.Count; r++)
								{
									dtItem.Rows[r]["SUBJ"] = subjStr.Trim();
								}
							}

						}
					}
					//else // 아이템 라인 시작
					//{
					//	if (!string.IsNullOrEmpty(firstColStr))
					//	{
					//		if (GetTo.IsInt(firstColStr) && firstColStr.Length < 3)
					//		{

					//			// row 값 가져와서 배열에 넣은후 값 추가하기
					//			string[] rowValueSpl = new string[20];
					//			int columnCount = 0;
					//			for (int c = 0; c < dt.Columns.Count; c++)
					//			{
					//				if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
					//				{
					//					rowValueSpl[columnCount] = c.ToString();
					//					columnCount++;
					//				}
					//			}


					//			if (rowValueSpl[3] != null && rowValueSpl[4] == null)
					//			{
					//				_itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
					//				_itemQt = Convert.ToInt16(rowValueSpl[2].ToString());
					//				_itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
					//			}

					//			else if (rowValueSpl[4] != null && rowValueSpl[5] == null)
					//			{
					//				_itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
					//				_itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
					//				_itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
					//			}
					//			else if (rowValueSpl[5] != null && rowValueSpl[6] == null)
					//			{
					//				_itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
					//				_itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
					//				_itemUnit = Convert.ToInt16(rowValueSpl[5].ToString());
					//			}
					//			else if (rowValueSpl[6] != null && rowValueSpl[7] == null)
					//			{
					//				_itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
					//				_itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
					//				_itemUnit = Convert.ToInt16(rowValueSpl[6].ToString());
					//			}

					//			if (!_itemQt.Equals(-1))
					//				iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

					//			// UNIT
					//			if (_itemUnit != -1)
					//				iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

					//			// DESC
					//			if (!_itemDesc.Equals(-1))
					//			{
					//				iTemDESC = string.Join(" ", dt.Rows[i].ItemArray.Skip(_itemDesc).Take(_itemQt - _itemDesc).Select(row => row.ToString().Trim()));
					//			}


					//			if (!string.IsNullOrEmpty(subjStr))
					//				iTemSUBJ = subjStr.Trim();


					//			//ITEM ADD START
					//			dtItem.Rows.Add();
					//			dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr.Replace(".","").Trim();
					//			dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
					//			dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = iTemUnit;
					//			if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
					//			if (!string.IsNullOrEmpty(iTemSUBJ))
					//				dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
					//			dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

					//			iTemDESC = string.Empty;
					//			iTemUnit = string.Empty;
					//			iTemCode = string.Empty;
					//			iTemQt = string.Empty;
					//			iTemSUBJ = string.Empty;

					//			//subjStr = string.Empty;
					//		}
					//	}
					//}
				}
			}
		}
	}
}

using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace OutParsing
{
	class gnp_pdf
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



		public gnp_pdf(string fileName)
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

			string subjStr = string.Empty;

			bool itemStart = false;

			#region ########### READ ###########
			// Pdf를 Xml로 변환해서 분석 (1000$ 짜리로 해야함, 500$ 짜리로 하면 Description 부분에 CRLF가 안됨)
			// 1. 우선 500$ 짜리로 Xml 변환함 (1000$ 짜리의 경우 도면이 붙어 있으면 시간이 엄청 오래 걸림)
			string xmlTemp = PdfReader.ToXml(fileName);

			//// 2. 도면을 제외한 Page 카운트 가져오기
			//int pageCount = xmlTemp.Count("<page>");

			//// 3. 앞서 나온 Page를 근거로 파싱 시작			
			string xml = string.Empty;//PdfReader.GetXml(fileName, 1, pageCount);
			xml = PdfReader.GetXml(fileName);
			//DataSet ds = PdfReader.ToDataSet(xml);

			////DataSet Table 병합을 위한 Table
			//DataTable dsAll = new DataTable();

			////DataSet Table의 Count Get
			//int dsCount = ds.Tables.Count;

			//for (int i = 0; i <= dsCount - 1; i++)
			//{
			//	dsAll.Merge(ds.Tables[i]);
			//}

			//ds.Clear();
			//ds.Tables.Add(dsAll);
			#endregion ########### READ ###########



			// xml row 나누기
			string[] xmlSpl = { };

			if (!string.IsNullOrEmpty(xmlTemp))
			{
				xmlSpl = xmlTemp.Split(new string[] { "<row>" }, StringSplitOptions.None);
			}


			string[] xmlSpl2 = { };

			if (!string.IsNullOrEmpty(xml))
			{
				xmlSpl2 = xml.Split(new string[] { "<row>" }, StringSplitOptions.None);
			}

			if (xmlSpl.Length > 10)
			{
				for (int r = 0; r < xmlSpl.Length; r++)
				{
					// 주제 패턴
					string partternSubject = @"<cell>\.</cell>(?!(\r\n\s+<cell>\d+\.</cell>|\r\n\s+<cell>\d+\\[\r\n]))";
					Regex regexSub = new Regex(partternSubject);
					Match matchSub = regexSub.Match(xmlSpl[r].ToString());


					string patternItem = @"<cell[^>]*>(\d+\.\s*.*?)<\/cell>";
					Regex regexItem = new Regex(patternItem);
					Match match = regexItem.Match(xmlSpl[r].ToString());


					if (matchSub.Success)
					{
						if (itemStart)
							subjStr = string.Empty;

						string getStr = xmlSpl[r].ToString().Replace("</cell>", "\\").Trim();

						string pattern = @"<[^>]+>";
						string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

						while (result.Contains("  "))
						{
							result = result.Replace("  ", " ").Trim();
						}

						string[] getSpl = result.Split(new string[] { "\\" }, StringSplitOptions.None);

						if (getSpl.Length == 3)
						{
							subjStr = subjStr + Environment.NewLine + getSpl[1].ToString().Trim();
						}
						else if (getSpl.Length == 4)
						{
							subjStr = subjStr + Environment.NewLine + getSpl[1].ToString().Trim();
						}
						else
						{

						}

						subjStr = subjStr.Trim();
						itemStart = false;
					}
					else if (match.Success)
					{
						itemStart = true;

						string getStr = xmlSpl[r].ToString().Replace("</cell>", "\\").Trim();

						string pattern = @"<[^>]+>";
						string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

						while (result.Contains("  "))
						{
							result = result.Replace("  ", " ").Trim();
						}

						string[] getSpl = result.Split(new string[] { "\\" }, StringSplitOptions.None);

						if (getSpl.Length == 6)
						{
							iTemNo = getSpl[0].ToString().Replace(".", "").Trim();
							for (int c = 1; c < 3; c++)
							{
								iTemDESC = iTemDESC + " " + getSpl[c].ToString().Trim();
							}
							iTemQt = getSpl[3].ToString().Trim();
							iTemUnit = getSpl[4].ToString().Trim();
						}
						else if (getSpl.Length == 5)
						{
							iTemNo = getSpl[0].ToString().Replace(".", "").Trim();
							for (int c = 1; c < 2; c++)
							{
								iTemDESC = iTemDESC + " " + getSpl[c].ToString().Trim();
							}
							iTemQt = getSpl[2].ToString().Trim();
							iTemUnit = getSpl[3].ToString().Trim();
						}
						else
						{

						}

						if (!string.IsNullOrEmpty(iTemCode))
							iTemDESC = iTemDESC + Environment.NewLine + iTemCode;

						if (!string.IsNullOrEmpty(subjStr))
							iTemSUBJ = subjStr.Trim();

						iTemDESC = iTemDESC.Trim();


						//ITEM ADD START
						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
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

						subjStr = string.Empty;
					}

					if (xmlSpl[r].ToString().Contains("선") && xmlSpl[r].ToString().Contains("명"))
					{
						// VESSEL
						string getStr = xmlSpl[r].ToString().Trim();

						string pattern = @"<[^>]+>";
						string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

						while (result.Contains("  "))
						{
							result = result.Replace("  ", " ").Trim();
						}

						if (result.Replace(" ", "").Contains("FAX"))
						{
							string[] vesselSpl = result.Split(':');

							if (vesselSpl.Length > 2)
							{
								vessel = vesselSpl[2].ToString().Trim();
							}
						}
					}
					else if (xmlSpl[r].ToString().Contains("HULL"))
					{
						// VESSEL
						string getStr = xmlSpl[r].ToString().Trim();

						string pattern = @"<[^>]+>";
						string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

						while (result.Contains("  "))
						{
							result = result.Replace("  ", " ").Trim();
						}

						if (result.Replace(" ", "").Contains("HULL"))
						{
							string[] vesselSpl = result.Split(':');

							if (vesselSpl.Length > 2)
							{
								vessel = vessel + "/" + vesselSpl[2].ToString().Trim();
							}
						}
					}
					else if (xmlSpl[r].ToString().Contains("REF") && xmlSpl[r].ToString().Contains("NO"))
					{
						// reference
						string getStr = xmlSpl[r].ToString().Trim();

						string pattern = @"<[^>]+>";
						string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

						while (result.Contains("  "))
						{
							result = result.Replace("  ", " ").Trim();
						}

						if (result.Replace(" ", "").Contains("수신"))
						{
							string[] vesselSpl = result.Split(':');

							if (vesselSpl.Length > 2)
							{
								reference = vesselSpl[2].ToString().Trim();
							}
						}
					}
				}


				// 마지막 줄이 subject와 동일하게 들어와서, 그거 체크하여 마지막 description에 추가 하는 로직
				if (!itemStart)
				{
					dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = dtItem.Rows[dtItem.Rows.Count - 1]["DESC"].ToString() + Environment.NewLine + subjStr;
				}
			}


			//Regex regex = new Regex(@".+\.");

			//foreach (DataTable dt in ds.Tables)
			//{
			//	for (int i = 0; i < dt.Rows.Count; i++)
			//	{
			//		string firstColStr = dt.Rows[i][0].ToString();


			//		if (!itemStart)
			//		{
			//			//if (string.IsNullOrEmpty(reference))
			//			//{
			//			//	for (int c = 1; c < dt.Columns.Count; c++)
			//			//	{
			//			//		if (dt.Rows[i][c].ToString().StartsWith("REF NO"))
			//			//		{
			//			//			reference = dt.Rows[i][c + 1].ToString().Trim();
			//			//
			//			//			if (string.IsNullOrEmpty(reference))
			//			//			{
			//			//				reference = dt.Rows[i][c -1].ToString().Trim();
			//			//			}
			//			//		}
			//			//	}
			//			//}
			//			//
			//			//if (firstColStr.Contains("선명"))
			//			//{
			//			//	for (int c = 0; c < dt.Columns.Count; c++)
			//			//	{
			//			//		vessel = vessel + " " + dt.Rows[i][c].ToString().Replace(":", "").Replace("선명","").Trim();
			//			//	}
			//			//}
			//			if (firstColStr.StartsWith("NO."))
			//			{
			//				itemStart = true;

			//				string lineStr = string.Empty;
			//				for (int c = 0; c < dt.Columns.Count; c++)
			//				{
			//					lineStr = dt.Rows[i][c].ToString().ToLower().Trim();

			//					if (lineStr.Contains("description")) _itemDesc = c;
			//					else if (lineStr.StartsWith("q'ty")) _itemQt = c;
			//					else if (lineStr.StartsWith("unit")) _itemUnit = c;
			//				}



			//				int _i = i + 1;


			//				subjStr = string.Empty;

			//				while (!regex.IsMatch(dt.Rows[_i][0].ToString()) || (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && !GetTo.IsInt(dt.Rows[_i][0].ToString().Substring(0, 1))))
			//				{

			//					subjStr = subjStr + Environment.NewLine + dt.Rows[_i][0].ToString().Trim() + dt.Rows[_i][1].ToString().Trim();

			//					_i += 1;
			//					i += 1;

			//					if (_i >= dt.Rows.Count)
			//						break;

			//					if (GetTo.IsInt(dt.Rows[_i][0].ToString()))
			//					{
			//						if (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && GetTo.IsInt(dt.Rows[_i][0].ToString().Substring(0, 1).Trim()))
			//							break;
			//					}


			//				}


			//			}
			//		}
			//		else // 아이템 라인 시작
			//		{
			//			if (!string.IsNullOrEmpty(firstColStr))
			//			{
			//				if (GetTo.IsInt(firstColStr))
			//				{

			//					// row 값 가져와서 배열에 넣은후 값 추가하기
			//					string[] rowValueSpl = new string[20];
			//					int columnCount = 0;
			//					for (int c = 0; c < dt.Columns.Count; c++)
			//					{
			//						if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
			//						{
			//							rowValueSpl[columnCount] = c.ToString();
			//							columnCount++;
			//						}
			//					}

			//					if (_itemDesc == -1 && _itemQt == -1)
			//					{
			//						if (rowValueSpl[3] != null && rowValueSpl[4] == null)
			//						{
			//							_itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
			//							_itemQt = Convert.ToInt16(rowValueSpl[2].ToString());
			//							_itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
			//						}
			//					}

			//					if (!_itemQt.Equals(-1))
			//						iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

			//					// UNIT
			//					if (_itemUnit != -1)
			//					{
			//						iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

			//						if (iTemUnit.Length > 8)
			//							iTemDESC = iTemUnit;

			//					}



			//					// DESC
			//					if (!_itemDesc.Equals(-1))
			//					{
			//						iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][_itemDesc].ToString().Trim();

			//						int _i = i + 1;

			//						while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || dt.Rows[_i][0].Equals("."))
			//						{
			//							iTemDESC = iTemDESC + " " + dt.Rows[_i][_itemDesc].ToString();

			//							_i += 1;

			//							if (dt.Rows.Count <= _i)
			//								break;
			//						}

			//					}


			//					if (!string.IsNullOrEmpty(subjStr))
			//						iTemSUBJ = subjStr.Trim();


			//					//ITEM ADD START
			//					dtItem.Rows.Add();
			//					dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = "";
			//					dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
			//					dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = iTemUnit;
			//					if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
			//					if (!string.IsNullOrEmpty(iTemSUBJ))
			//						dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
			//					dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

			//					iTemDESC = string.Empty;
			//					iTemUnit = string.Empty;
			//					iTemCode = string.Empty;
			//					iTemQt = string.Empty;
			//					iTemSUBJ = string.Empty;

			//					//subjStr = string.Empty;
			//				}
			//			}
			//		}
			//	}
			//}
		}
	}
}

using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace OutParsing
{
	class DHMARINE_pdf
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



		public DHMARINE_pdf(string fileName)
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
			bool subjectReset = false;

			int _itemdesc = -1;
			int _itemcode = -1;
			int _itemqt = -1;
			int _itemunit = -1;


			#region ########### READ ###########
			// Pdf를 Xml로 변환해서 분석 (1000$ 짜리로 해야함, 500$ 짜리로 하면 Description 부분에 CRLF가 안됨)
			// 1. 우선 500$ 짜리로 Xml 변환함 (1000$ 짜리의 경우 도면이 붙어 있으면 시간이 엄청 오래 걸림)
			string xmlTemp = PdfReader.ToXml(fileName);

			//// 2. 도면을 제외한 Page 카운트 가져오기
			int pageCount = xmlTemp.Count("<page>");
			//
			//// 3. 앞서 나온 Page를 근거로 파싱 시작			
			string xml = string.Empty;//PdfReader.GetXml(fileName, 1, pageCount);
			xml = PdfReader.GetXml(fileName);
			DataSet ds = PdfReader.ToDataSet(xml);
			//PdfReader.GetXml(fileName, 1, pageCount);
			//xml = PdfReader.GetXml(fileName);
			#endregion ########### READ ###########


			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string firstStr = dt.Rows[i][0].ToString();

					// SUBJECT 패턴
					string patternSubject = @"^\d+\.\s\*.+$";
					Regex regexSubject = new Regex(patternSubject);
					Match match = regexSubject.Match(firstStr);


					string patternItem = @"^\d+\.\s.+";
					Regex regexItem = new Regex(patternItem);
					Match matchItem = regexItem.Match(firstStr);


					if (string.IsNullOrEmpty(reference))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("REF"))
							{
								for (int cc = c; cc < dt.Columns.Count; cc++)
								{
									reference = reference + dt.Rows[i][cc].ToString().Trim().Replace("REF.", "").Replace("NO.", "").Replace(":", "");
								}
							}
						}
					}

					if (match.Success || firstStr.StartsWith("*M") || firstStr.StartsWith("*N") || firstStr.StartsWith("*A"))
					{
						// 새로운 주제 들어오면 reset 후 다시 진행!
						if (subjectReset)
							subjStr = string.Empty;

						for (int c = 0; c < dt.Columns.Count; c++)
						{
							subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
						}

						subjectReset = false;
					}
					else if (firstStr.StartsWith("선"))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("F") && dt.Rows[i][c].ToString().Contains("A") && dt.Rows[i][c].ToString().Contains("X"))
								break;
							else
								vessel = vessel.Trim() + " " + dt.Rows[i][c].ToString().Replace("선", "").Replace("명", "").Replace(":", "").Trim();
						}
					}
					else if (firstStr.StartsWith("NO."))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							string forStr = dt.Rows[i][c].ToString().ToUpper().Trim();

							if (forStr.StartsWith("CODE")) _itemcode = c;
							else if (forStr.Contains("DESCRIPTION")) _itemdesc = c;
							else if (forStr.StartsWith("Q'TY")) _itemqt = c;

							if (forStr.Contains("UNIT")) _itemunit = c;
						}
					}
					else if (matchItem.Success)
					{
						if (_itemqt != -1 && _itemqt == _itemunit)
						{
							string[] qtSpl = dt.Rows[i][_itemqt].ToString().Split(' ');

							if (qtSpl.Length == 2)
							{
								iTemQt = qtSpl[0].ToString();
								iTemUnit = qtSpl[1].ToString();
							}
							else
							{
								string[] qtSpl2 = dt.Rows[i][_itemqt + 1].ToString().Split(' ');

								if (qtSpl2.Length == 2)
								{
									iTemQt = qtSpl2[0].ToString();
									iTemUnit = qtSpl2[1].ToString();
								}
							}
						}


						if (_itemcode != -1)
						{
							for (int codeCol = _itemcode; codeCol < _itemqt; codeCol++)
							{
								iTemCode = iTemCode.Trim() + " " + dt.Rows[i][codeCol].ToString().Trim();
							}
						}


						if (_itemdesc != -1)
						{
							iTemDESC = dt.Rows[i][_itemdesc].ToString().Trim();
							int _i = i + 1;

							string descStr = dt.Rows[_i][_itemdesc].ToString().Trim();

							match = regexSubject.Match(descStr);
							matchItem = regexItem.Match(descStr);

							if (!descStr.Contains("디에이치마린"))
							{
								while (!match.Success && !matchItem.Success)
								{
									iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][_itemdesc].ToString().Trim();

									_i += 1;

									descStr = dt.Rows[_i][_itemdesc].ToString().Trim();
									match = regexSubject.Match(descStr);
									matchItem = regexItem.Match(descStr);

									if (dt.Rows[_i][_itemdesc].ToString().Contains("디에이치마린"))
										break;
								}
							}
						}

						string pattern2 = @"\b(\d+)\.";
						Regex regex = new Regex(pattern2);
						MatchCollection matches = regex.Matches(firstStr);

						foreach (Match match2 in matches)
						{
							iTemNo = match2.Groups[1].Value;
						}


						if (!string.IsNullOrEmpty(iTemDESC))
						{
							if (iTemNo.Length == 1)
							{
								iTemDESC = iTemDESC.Substring(2, iTemDESC.Length - 2).Trim();
							}
							else if (iTemNo.Length == 2)
							{
								iTemDESC = iTemDESC.Substring(3, iTemDESC.Length - 3).Trim();
							}
						}

						if (!string.IsNullOrEmpty(iTemCode))
							iTemDESC = iTemDESC.Trim() + Environment.NewLine + iTemCode.Trim();


						if (!string.IsNullOrEmpty(subjStr))
							iTemSUBJ = subjStr.Trim();


						if (!string.IsNullOrEmpty(iTemSUBJ))
						{
							if (iTemNo.Length == 1 && GetTo.IsInt(iTemSUBJ.Substring(0, 1).ToString()))
							{
								iTemSUBJ = iTemSUBJ.Substring(2, iTemSUBJ.Length - 2).Trim();
							}
							else if (iTemNo.Length == 2 && GetTo.IsInt(iTemSUBJ.Substring(0, 2).ToString()))
							{
								iTemSUBJ = iTemSUBJ.Substring(3, iTemSUBJ.Length - 3).Trim();
							}
						}

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


						// 아이템라인에 주제가 한번 들어갔기 때문에 주제가 다시 나오면 삭제후 다시 ㄱㄱ
						subjectReset = true;
					}
					else if (firstStr.Contains("*R"))
					{
						string result = firstStr.Trim();

						int _i = i + 1;



						while (!dt.Rows[_i][0].ToString().Contains("(주)"))
						{
							result = result + Environment.NewLine + dt.Rows[_i][0].ToString().Trim();

							_i += 1;

							if (dt.Rows.Count <= _i)
								break;
						}

						if (!string.IsNullOrEmpty(result.Replace("**REMARK", "").Trim()))
						{
							dtItem.Rows.Add();
							dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = result;
						}
					}

				}
			}



			//// xml row 나누기
			//string[] xmlSpl = { };

			//if (!string.IsNullOrEmpty(xml))
			//{
			//	xmlSpl = xml.Split(new string[] { "<row>" }, StringSplitOptions.None);
			//}

			//if (xml.Contains("AMOUNT"))
			//{
			//	int idx_s = xml.IndexOf("AMOUNT");
			//	int idx_e = xml.IndexOf("<cell colspan=\"4\">1.");

			//	if (idx_s != -1 && idx_e != -1)
			//	{
			//		string getStr = xmlTemp.Substring(idx_s, idx_e - idx_s).Trim();

			//		string pattern = @"<[^>]+>";
			//		string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Replace("AMOUNT", "").Trim();

			//		//string[] getSpl = result.Split(new string[] { "의뢰번호" }, StringSplitOptions.None);

			//		while (result.Contains("  "))
			//		{
			//			result = result.Replace("  ", " ").Trim();
			//		}

			//		subjStr = result.Replace(" . ", "\r\n").Trim();
			//	}
			//}


			//if (xmlSpl.Length > 10)
			//{
			//	for (int r = 0; r < xmlSpl.Length; r++)
			//	{
			//		// 주제 패턴
			//		//string partternSubject = @"<cell colspan=""\d+"">\d+\.\s\*\S*<\/cell>";
			//		//Regex regexSub = new Regex(partternSubject);
			//		//Match matchSub = regexSub.Match(xmlSpl[r].ToString());

			//		// 
			//		//string patternItem = @"<cell[^>]*>(\d+\.\s*.*?)<\/cell>";
			//		string patternItem = @"<column>\r\n\s*<text fontName=""Times""[^>]*>(\d+\.).";
			//		Regex regexItem = new Regex(patternItem);
			//		Match match = regexItem.Match(xmlSpl[r].ToString());

			//		//if (xmlSpl[r].ToString().Contains("선") && xmlSpl[r].ToString().Contains("명"))
			//		//{
			//		//	// VESSEL
			//		//	string getStr = xmlSpl[r].ToString().Trim();

			//		//	string pattern = @"<[^>]+>";
			//		//	string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

			//		//	while (result.Contains("  "))
			//		//	{
			//		//		result = result.Replace("  ", " ").Trim();
			//		//	}

			//		//	if (result.Replace(" ", "").Contains("FAX"))
			//		//	{
			//		//		string[] vesselSpl = result.Split(':');

			//		//		if (vesselSpl.Length > 2)
			//		//		{
			//		//			vessel = vesselSpl[1].ToString().Replace("F A X", "").Trim();
			//		//		}
			//		//	}
			//		//}
			//		if (xmlSpl[r].ToString().Contains("REF.NO."))
			//		{
			//			// REFERENCE
			//			string getStr = xmlSpl[r].ToString().Trim();

			//			string pattern = @"<[^>]+>";
			//			string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

			//			while (result.Contains("  "))
			//			{
			//				result = result.Replace("  ", " ").Trim();
			//			}

			//			string[] getSpl = result.Split(new string[] { "REF.NO." }, StringSplitOptions.None);

			//			if (getSpl.Length > 1)
			//			{
			//				reference = getSpl[1].ToString().Replace(":", "").Replace(".", "").Replace("(EVAL PDF Extractor SDK 8803021-2098472311)", "").Trim();
			//			}
			//		}
			//		//else if (xmlSpl[r].ToString().Contains("*N") || xmlSpl[r].ToString().Contains("*M") || xmlSpl[r].ToString().Contains("*A") || xmlSpl[r].ToString().Contains("*S") || xmlSpl[r].ToString().Contains("*E"))
			//		//{
			//		//	if (itemStart)
			//		//		subjStr = string.Empty;

			//		//	string getStr = xmlSpl[r].ToString().Trim();

			//		//	string pattern = @"<[^>]+>";
			//		//	string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

			//		//	while (result.Contains("  "))
			//		//	{
			//		//		result = result.Replace("  ", " ").Trim();
			//		//	}

			//		//	subjStr = subjStr.Trim() + Environment.NewLine + result.Trim();
			//		//	subjStr = subjStr.Replace("1.", "").Replace("\r\n ", "\r\n").Trim();

			//		//	itemStart = false;
			//		//}
			//		else if (xmlSpl[r].ToString().Contains("*R"))
			//		{
			//			//string getStr = xmlSpl[r].ToString().Trim();

			//			//string pattern = @"<[^>]+>";
			//			//string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

			//			//while (result.Contains("  "))
			//			//{
			//			//	result = result.Replace("  ", " ").Trim();
			//			//}

			//			//if (!string.IsNullOrEmpty(result.Replace("**REMARK", "").Trim()))
			//			//{
			//			//	dtItem.Rows.Add();
			//			//	dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = result;
			//			//}
			//		}

			//		//else if (match.Success && !string.IsNullOrEmpty(reference))
			//		//{
			//		//	itemStart = true;
			//		//	string getStr = xmlSpl[r].ToString().Replace("</text>", "\\").Trim();

			//		//	string pattern = @"<[^>]+>";
			//		//	string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

			//		//	while (result.Contains("  "))
			//		//	{
			//		//		result = result.Replace("  ", " ").Trim();
			//		//	}

			//		//	string[] getSpl = result.Split(new string[] { "\\" }, StringSplitOptions.None);

			//		//	string[] filteredArray = getSpl
			//		//							.Where(s => !string.IsNullOrWhiteSpace(s))
			//		//							.Where(s => !s.Contains("PDF Extractor"))
			//		//							.ToArray();

			//		//	if (filteredArray.Length == 2)
			//		//	{
			//		//		string pattern2 = @"\b(\d+)\.";
			//		//		Regex regex = new Regex(pattern2);
			//		//		MatchCollection matches = regex.Matches(filteredArray[0].ToString());

			//		//		foreach (Match match2 in matches)
			//		//		{
			//		//			iTemNo = match2.Groups[1].Value;
			//		//		}


			//		//		iTemDESC = filteredArray[0].ToString().Trim();

			//		//		string[] qtSpl = filteredArray[1].ToString().Split(' ');
			//		//		string[] qtSpl2 = qtSpl.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

			//		//		if (qtSpl2.Length == 2)
			//		//		{
			//		//			iTemQt = qtSpl2[0];
			//		//			iTemUnit = qtSpl2[1];
			//		//		}
			//		//	}
			//		//	else if (filteredArray.Length == 3)
			//		//	{
			//		//		//iTemNo = Regex.Replace(getSpl[0].ToString(), @"\D", "");

			//		//		string pattern2 = @"\b(\d+)\.";
			//		//		Regex regex = new Regex(pattern2);
			//		//		MatchCollection matches = regex.Matches(filteredArray[0].ToString());

			//		//		foreach (Match match2 in matches)
			//		//		{
			//		//			iTemNo = match2.Groups[1].Value;
			//		//		}

			//		//		iTemDESC = filteredArray[0].ToString().Trim() + Environment.NewLine + filteredArray[1].ToString().Trim();

			//		//		string[] qtSpl = filteredArray[2].ToString().Split(' ');
			//		//		string[] qtSpl2 = qtSpl.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

			//		//		if (qtSpl2.Length == 2)
			//		//		{
			//		//			iTemQt = qtSpl2[0];
			//		//			iTemUnit = qtSpl2[1];
			//		//		}
			//		//	}
			//		//	else if (filteredArray.Length == 4)
			//		//	{
			//		//		//iTemNo = Regex.Replace(getSpl[0].ToString(), @"\D", "");

			//		//		string pattern2 = @"\b(\d+)\.";
			//		//		Regex regex = new Regex(pattern2);
			//		//		MatchCollection matches = regex.Matches(filteredArray[0].ToString());

			//		//		foreach (Match match2 in matches)
			//		//		{
			//		//			iTemNo = match2.Groups[1].Value;
			//		//		}

			//		//		iTemDESC = filteredArray[0].ToString().Trim() + Environment.NewLine + filteredArray[1].ToString().Trim() + Environment.NewLine + filteredArray[2].ToString().Trim();

			//		//		string[] qtSpl = filteredArray[3].ToString().Split(' ');
			//		//		string[] qtSpl2 = qtSpl.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

			//		//		if (qtSpl2.Length == 2)
			//		//		{
			//		//			iTemQt = qtSpl2[0];
			//		//			iTemUnit = qtSpl2[1];
			//		//		}
			//		//		else if (qtSpl2.Length == 1)
			//		//		{
			//		//			qtSpl = filteredArray[1].ToString().Split(' ');
			//		//			qtSpl2 = qtSpl.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

			//		//			if(qtSpl2.Length == 2)
			//		//			{
			//		//				iTemQt = qtSpl2[0];
			//		//				iTemUnit = qtSpl2[1];
			//		//			}
			//		//		}
			//		//	}

			//		//	int _r = r + 1;
			//		//	match = regexItem.Match(xmlSpl[_r].ToString());

			//		//	while (!match.Success && !xmlSpl[_r].ToString().Contains("*N") && !xmlSpl[_r].ToString().Contains("*M") && !xmlSpl[_r].ToString().Contains("*A") && !xmlSpl[_r].ToString().Contains("*S") && !xmlSpl[_r].ToString().Contains("Continue")
			//		//		&& !xmlSpl[_r].ToString().Contains("DH MARINE") && !xmlSpl[_r].ToString().Contains("디에이치마린"))
			//		//	{
			//		//		getStr = xmlSpl[_r].ToString().Trim();

			//		//		if (getStr.ToUpper().Contains("REMARK"))
			//		//			break;
			//		//		//if (getStr.Contains("</table>"))
			//		//		iTemDESC = iTemDESC + " " + Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

			//		//		_r += 1;

			//		//		if (_r >= xmlSpl.Length)
			//		//			break;

			//		//		match = regexItem.Match(xmlSpl[_r].ToString());

			//		//		//if (match.Success)
			//		//		//	break;
			//		//	}

			//		//	if (!string.IsNullOrEmpty(subjStr))
			//		//		iTemSUBJ = subjStr.Trim();

			//		//	if (!string.IsNullOrEmpty(iTemUnit) && !string.IsNullOrEmpty(iTemQt))
			//		//	{
			//		//		if(!string.IsNullOrEmpty(iTemDESC))
			//		//		{
			//		//			if(iTemNo.Length == 1)
			//		//			{
			//		//				iTemDESC = iTemDESC.Substring(2, iTemDESC.Length - 2).Trim();
			//		//			}
			//		//		}

			//		//		//ITEM ADD START
			//		//		dtItem.Rows.Add();
			//		//		dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
			//		//		dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC.Trim();
			//		//		dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = iTemUnit;
			//		//		if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
			//		//		if (!string.IsNullOrEmpty(iTemSUBJ))
			//		//			dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
			//		//		dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

			//		//		iTemDESC = string.Empty;
			//		//		iTemUnit = string.Empty;
			//		//		iTemCode = string.Empty;
			//		//		iTemQt = string.Empty;
			//		//		iTemSUBJ = string.Empty;

			//		//		subjStr = string.Empty;
			//		//	}
			//		//}
			//}
			//}
		}
		//string vessel;
		//string reference;
		//string imoNumber;
		//DataTable dtItem;

		//string fileName;


		//#region ==================================================================================================== Property

		//public string Vessel
		//{
		//	get
		//	{
		//		return vessel;
		//	}
		//}

		//public string Reference
		//{
		//	get
		//	{
		//		return reference;
		//	}
		//}

		//public string ImoNumber
		//{
		//	get
		//	{
		//		return imoNumber;
		//	}
		//}

		//public DataTable Item
		//{
		//	get
		//	{
		//		return dtItem;
		//	}
		//}

		//#endregion ==================================================================================================== Constructor



		//public DHMARINE_pdf(string fileName)
		//{
		//	vessel = "";                        // 선명
		//	reference = "";                     // 문의번호
		//	imoNumber = "";

		//	dtItem = new DataTable();
		//	dtItem.Columns.Add("NO");           // 순번
		//	dtItem.Columns.Add("SUBJ");         // 주제
		//	dtItem.Columns.Add("ITEM");         // 품목코드
		//	dtItem.Columns.Add("DESC");         // 품목명
		//	dtItem.Columns.Add("UNIT");         // 단위
		//	dtItem.Columns.Add("QT");           // 수량
		//	dtItem.Columns.Add("UNIQ");           // 수량
		//	this.fileName = fileName;
		//}

		//public void Parse()
		//{
		//	string iTemNo = string.Empty;
		//	string iTemSUBJ = string.Empty;
		//	string iTemCode = string.Empty;
		//	string iTemDESC = string.Empty;
		//	string iTemUnit = string.Empty;
		//	string iTemQt = string.Empty;

		//	string subjStr = string.Empty;

		//	bool itemStart = false;


		//	#region ########### READ ###########
		//	// Pdf를 Xml로 변환해서 분석 (1000$ 짜리로 해야함, 500$ 짜리로 하면 Description 부분에 CRLF가 안됨)
		//	// 1. 우선 500$ 짜리로 Xml 변환함 (1000$ 짜리의 경우 도면이 붙어 있으면 시간이 엄청 오래 걸림)
		//	string xmlTemp = PdfReader.ToXml(fileName);

		//	//// 2. 도면을 제외한 Page 카운트 가져오기
		//	//int pageCount = xmlTemp.Count("<page>");
		//	//
		//	//// 3. 앞서 나온 Page를 근거로 파싱 시작			
		//	string xml = string.Empty;//PdfReader.GetXml(fileName, 1, pageCount);
		//	xml = PdfReader.GetXml(fileName);
		//	DataSet ds = PdfReader.ToDataSet(xml);
		//	//PdfReader.GetXml(fileName, 1, pageCount);
		//	//xml = PdfReader.GetXml(fileName);
		//	#endregion ########### READ ###########

		//	// xml row 나누기
		//	string[] xmlSpl = { };

		//	if (!string.IsNullOrEmpty(xml))
		//	{
		//		xmlSpl = xml.Split(new string[] { "<row>" }, StringSplitOptions.None);
		//	}

		//	if (xml.Contains("AMOUNT"))
		//	{
		//		int idx_s = xml.IndexOf("AMOUNT");
		//		int idx_e = xml.IndexOf("<cell colspan=\"4\">1.");

		//		if (idx_s != -1 && idx_e != -1)
		//		{
		//			string getStr = xmlTemp.Substring(idx_s, idx_e - idx_s).Trim();

		//			string pattern = @"<[^>]+>";
		//			string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Replace("AMOUNT", "").Trim();

		//			//string[] getSpl = result.Split(new string[] { "의뢰번호" }, StringSplitOptions.None);

		//			while (result.Contains("  "))
		//			{
		//				result = result.Replace("  ", " ").Trim();
		//			}

		//			subjStr = result.Replace(" . ", "\r\n").Trim();


		//		}
		//	}


		//	if (xmlSpl.Length > 10)
		//	{
		//		for (int r = 0; r < xmlSpl.Length; r++)
		//		{
		//			// 주제 패턴
		//			//string partternSubject = @"<cell colspan=""\d+"">\d+\.\s\*\S*<\/cell>";
		//			//Regex regexSub = new Regex(partternSubject);
		//			//Match matchSub = regexSub.Match(xmlSpl[r].ToString());

		//			// 
		//			//string patternItem = @"<cell[^>]*>(\d+\.\s*.*?)<\/cell>";
		//			string patternItem = @"<column>\r\n\s*<text fontName=""Times""[^>]*>(\d+).";
		//			Regex regexItem = new Regex(patternItem);
		//			Match match = regexItem.Match(xmlSpl[r].ToString());

		//			if (xmlSpl[r].ToString().Contains("선") && xmlSpl[r].ToString().Contains("명"))
		//			{
		//				// VESSEL
		//				string getStr = xmlSpl[r].ToString().Trim();

		//				string pattern = @"<[^>]+>";
		//				string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

		//				while (result.Contains("  "))
		//				{
		//					result = result.Replace("  ", " ").Trim();
		//				}

		//				if (result.Replace(" ", "").Contains("FAX"))
		//				{
		//					string[] vesselSpl = result.Split(':');

		//					if (vesselSpl.Length > 2)
		//					{
		//						vessel = vesselSpl[1].ToString().Replace("F A X", "").Trim();
		//					}
		//				}
		//			}
		//			else if (xmlSpl[r].ToString().Contains("REF.NO."))
		//			{
		//				// REFERENCE
		//				string getStr = xmlSpl[r].ToString().Trim();

		//				string pattern = @"<[^>]+>";
		//				string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

		//				while (result.Contains("  "))
		//				{
		//					result = result.Replace("  ", " ").Trim();
		//				}

		//				string[] getSpl = result.Split(new string[] { "REF.NO." }, StringSplitOptions.None);

		//				if (getSpl.Length > 1)
		//				{
		//					reference = getSpl[1].ToString().Replace(":", "").Replace(".", "").Trim();
		//				}
		//			}
		//			else if (xmlSpl[r].ToString().Contains("*N") || xmlSpl[r].ToString().Contains("*M") || xmlSpl[r].ToString().Contains("*A") || xmlSpl[r].ToString().Contains("*S"))
		//			{
		//				if (itemStart)
		//					subjStr = string.Empty;

		//				string getStr = xmlSpl[r].ToString().Trim();

		//				string pattern = @"<[^>]+>";
		//				string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

		//				while (result.Contains("  "))
		//				{
		//					result = result.Replace("  ", " ").Trim();
		//				}

		//				subjStr = subjStr.Trim() + Environment.NewLine + result.Trim();
		//				subjStr = subjStr.Replace("1.", "").Replace("\r\n ", "\r\n").Trim();

		//				itemStart = false;
		//			}
		//			else if (xmlSpl[r].ToString().Contains("*R"))
		//			{
		//				string getStr = xmlSpl[r].ToString().Trim();

		//				string pattern = @"<[^>]+>";
		//				string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

		//				while (result.Contains("  "))
		//				{
		//					result = result.Replace("  ", " ").Trim();
		//				}

		//				if (!string.IsNullOrEmpty(result.Replace("**REMARK", "").Trim()))
		//				{
		//					dtItem.Rows.Add();
		//					dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = result;
		//				}
		//			}
		//			else if (match.Success && !string.IsNullOrEmpty(reference))
		//			{
		//				itemStart = true;
		//				string getStr = xmlSpl[r].ToString().Replace("</text>", "\\").Trim();

		//				string pattern = @"<[^>]+>";
		//				string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

		//				while (result.Contains("  "))
		//				{
		//					result = result.Replace("  ", " ").Trim();
		//				}

		//				string[] getSpl = result.Split(new string[] { "\\" }, StringSplitOptions.None);

		//				string[] filteredArray = getSpl
		//										.Where(s => !string.IsNullOrWhiteSpace(s))
		//										.Where(s => !s.Contains("PDF Extractor"))
		//										.ToArray();

		//				if (filteredArray.Length == 2)
		//				{
		//					string pattern2 = @"\b(\d+)\.";
		//					Regex regex = new Regex(pattern2);
		//					MatchCollection matches = regex.Matches(filteredArray[0].ToString());

		//					foreach (Match match2 in matches)
		//					{
		//						iTemNo = match2.Groups[1].Value;
		//					}


		//					iTemDESC = filteredArray[0].ToString().Trim();

		//					string[] qtSpl = filteredArray[1].ToString().Split(' ');
		//					string[] qtSpl2 = qtSpl.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

		//					if (qtSpl2.Length == 2)
		//					{
		//						iTemQt = qtSpl2[0];
		//						iTemUnit = qtSpl2[1];
		//					}
		//				}
		//				else if (filteredArray.Length == 3)
		//				{
		//					//iTemNo = Regex.Replace(getSpl[0].ToString(), @"\D", "");

		//					string pattern2 = @"\b(\d+)\.";
		//					Regex regex = new Regex(pattern2);
		//					MatchCollection matches = regex.Matches(filteredArray[0].ToString());

		//					foreach (Match match2 in matches)
		//					{
		//						iTemNo = match2.Groups[1].Value;
		//					}

		//					iTemDESC = filteredArray[0].ToString().Trim() + Environment.NewLine + filteredArray[1].ToString().Trim();

		//					string[] qtSpl = filteredArray[2].ToString().Split(' ');
		//					string[] qtSpl2 = qtSpl.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

		//					if (qtSpl2.Length == 2)
		//					{
		//						iTemQt = qtSpl2[0];
		//						iTemUnit = qtSpl2[1];
		//					}
		//				}
		//				else if (filteredArray.Length == 4)
		//				{
		//					//iTemNo = Regex.Replace(getSpl[0].ToString(), @"\D", "");

		//					string pattern2 = @"\b(\d+)\.";
		//					Regex regex = new Regex(pattern2);
		//					MatchCollection matches = regex.Matches(filteredArray[0].ToString());

		//					foreach (Match match2 in matches)
		//					{
		//						iTemNo = match2.Groups[1].Value;
		//					}

		//					iTemDESC = filteredArray[0].ToString().Trim() + Environment.NewLine + filteredArray[1].ToString().Trim() + Environment.NewLine + filteredArray[2].ToString().Trim();

		//					string[] qtSpl = filteredArray[3].ToString().Split(' ');
		//					string[] qtSpl2 = qtSpl.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

		//					if (qtSpl2.Length == 2)
		//					{
		//						iTemQt = qtSpl2[0];
		//						iTemUnit = qtSpl2[1];
		//					}
		//					else if (qtSpl2.Length == 1)
		//					{
		//						qtSpl = filteredArray[1].ToString().Split(' ');
		//						qtSpl2 = qtSpl.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

		//						if (qtSpl2.Length == 2)
		//						{
		//							iTemQt = qtSpl2[0];
		//							iTemUnit = qtSpl2[1];
		//						}
		//					}
		//				}

		//				int _r = r + 1;
		//				match = regexItem.Match(xmlSpl[_r].ToString());

		//				while (!match.Success && !xmlSpl[_r].ToString().Contains("*N") && !xmlSpl[_r].ToString().Contains("*M") && !xmlSpl[_r].ToString().Contains("*A") && !xmlSpl[_r].ToString().Contains("*S") && !xmlSpl[_r].ToString().Contains("Continue")
		//					&& !xmlSpl[_r].ToString().Contains("DH MARINE") && !xmlSpl[_r].ToString().Contains("디에이치마린"))
		//				{
		//					getStr = xmlSpl[_r].ToString().Trim();

		//					if (getStr.ToUpper().Contains("REMARK"))
		//						break;
		//					//if (getStr.Contains("</table>"))
		//					iTemDESC = iTemDESC + " " + Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

		//					_r += 1;

		//					if (_r >= xmlSpl.Length)
		//						break;

		//					match = regexItem.Match(xmlSpl[_r].ToString());

		//					//if (match.Success)
		//					//	break;
		//				}

		//				if (!string.IsNullOrEmpty(subjStr))
		//					iTemSUBJ = subjStr.Trim();

		//				if (!string.IsNullOrEmpty(iTemUnit) && !string.IsNullOrEmpty(iTemQt))
		//				{

		//					if (!string.IsNullOrEmpty(iTemDESC))
		//					{
		//						if (iTemNo.Length == 1)
		//						{
		//							iTemDESC = iTemDESC.Substring(2, iTemDESC.Length - 2).Trim();
		//						}
		//					}

		//					//ITEM ADD START
		//					dtItem.Rows.Add();
		//					dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
		//					dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC.Trim();
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

		//					subjStr = string.Empty;
		//				}
		//			}
		//		}
		//	}
		//}
	}
}

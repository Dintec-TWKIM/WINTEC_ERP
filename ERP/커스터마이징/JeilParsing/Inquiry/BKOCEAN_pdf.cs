using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace OutParsing
{
	class BKOCEAN_pdf
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



		public BKOCEAN_pdf(string fileName)
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

			bool iTemStart = false;


			#region ########### READ ###########
			// Pdf를 Xml로 변환해서 분석 (1000$ 짜리로 해야함, 500$ 짜리로 하면 Description 부분에 CRLF가 안됨)
			// 1. 우선 500$ 짜리로 Xml 변환함 (1000$ 짜리의 경우 도면이 붙어 있으면 시간이 엄청 오래 걸림)
			string xmlTemp = PdfReader.ToXml(fileName);

			// 2. 도면을 제외한 Page 카운트 가져오기
			int pageCount = xmlTemp.Count("<page>");

			// 3. 앞서 나온 Page를 근거로 파싱 시작			
			string xml = string.Empty;//PdfReader.GetXml(fileName, 1, pageCount);
			xml = PdfReader.GetXml(fileName);
			#endregion ########### READ ###########


			//dtItem.Rows.Add();
			//dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = no;
			//dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = description;
			//dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = unit;
			//dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = qty;
			////if (!string.IsNullOrEmpty(iTemSUBJ))
			////	dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
			//dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = partNo;


			// xml row 나누기
			string[] xmlSpl = { };

			if (!string.IsNullOrEmpty(xmlTemp))
			{
				xmlSpl = xmlTemp.Split(new string[] { "<row>" }, StringSplitOptions.None);
			}

			if (xmlTemp.Contains("AMOUNT"))
			{
				int idx_s = xmlTemp.IndexOf("AMOUNT");
				int idx_e = xmlTemp.IndexOf("<cell colspan=\"4\">1.");

				if (idx_s != -1 && idx_e != -1)
				{
					string getStr = xmlTemp.Substring(idx_s, idx_e - idx_s).Trim();

					string pattern = @"<[^>]+>";
					string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Replace("AMOUNT", "").Trim();

					//string[] getSpl = result.Split(new string[] { "의뢰번호" }, StringSplitOptions.None);

					while (result.Contains("  "))
					{
						result = result.Replace("  ", " ").Trim();
					}

					subjStr = result.Replace(" . ", "\r\n").Trim();
				}
			}


			if (xmlSpl.Length > 10)
			{
				for (int r = 0; r < xmlSpl.Length; r++)
				{
					// 주제 패턴
					string partternSubject = @"\r\n\s+<cell colspan=""\d+"">\d+\.\s[^\r\n]+</cell>\r\n\s+";
					//string partternSubject = @"\r\n        <cell colspan=""2"">\r\n        <cell colspan=""14"">";
					Regex regexSub = new Regex(partternSubject);
					Match matchSub = regexSub.Match(xmlSpl[r].ToString());


					string patternItem = @"(<cell colspan=""\d+"">)(.*?)(</cell>)";
					Regex regexItem = new Regex(patternItem);
					Match match = regexItem.Match(xmlSpl[r].ToString());

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

						if (result.Replace(" ", "").Contains("전화번호"))
						{
							string[] vesselSpl = result.Split(':');

							if (vesselSpl.Length > 2)
							{
								vessel = vesselSpl[1].ToString().Replace("전화번호", "").Trim();
							}
						}
					}
					else if (xmlSpl[r].ToString().Contains("서류번호"))
					{
						// REFERENCE
						string getStr = xmlSpl[r].ToString().Trim();

						string pattern = @"<[^>]+>";
						string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

						while (result.Contains("  "))
						{
							result = result.Replace("  ", " ").Trim();
						}

						string[] getSpl = result.Split(new string[] { "서류번호" }, StringSplitOptions.None);

						if (getSpl.Length > 1)
						{
							reference = getSpl[1].ToString().Replace(":", "").Replace(".", "").Trim();
						}
					}
					else if (xmlSpl[r].ToString().Contains("NO") && xmlSpl[r].ToString().Contains("DESCRIPTION"))
					{
						iTemStart = true;
					}
					else if (matchSub.Success && iTemStart)
					{
						string getStr = xmlSpl[r].ToString().Trim();

						string pattern = @"<[^>]+>";
						string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

						while (result.Contains("  "))
						{
							result = result.Replace("  ", " ").Trim();
						}

						subjStr = subjStr.Trim() + Environment.NewLine + result.Trim();
					}
					else if (match.Success && iTemStart)
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



						if (count > 6)
						{
							if (getSpl.Length > 2)
							{
								iTemNo = Regex.Replace(getSpl[0].ToString(), @"\D", "");

								for (int line = 0; line < getSpl.Length - 3; line++)
								{
									iTemDESC = iTemDESC.Trim() + " " + getSpl[line].ToString().Trim();
								}

								iTemQt = getSpl[getSpl.Length - 3].ToString().Trim();
								iTemUnit = getSpl[getSpl.Length - 2].ToString().Trim();

								if (!string.IsNullOrEmpty(subjStr))
									iTemSUBJ = subjStr.Trim();


								//ITEM ADD START
								dtItem.Rows.Add();
								dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
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
						}
						else
						{
							subjStr = subjStr.Trim() + Environment.NewLine + result.Trim();
						}


						//else if (getSpl.Length == 8)
						//{
						//	iTemNo = Regex.Replace(getSpl[0].ToString(), @"\D", "");
						//	iTemDESC = getSpl[0].ToString().Trim() + " " +  getSpl[1].ToString().Trim() + " " + getSpl[2].ToString().Trim() + " " + getSpl[3].ToString().Trim() + " " + getSpl[4].ToString().Trim();
						//	//iTemCode = getSpl[1].ToString().Trim();
						//	iTemQt = getSpl[5].ToString().Trim();
						//	iTemUnit = getSpl[6].ToString().Trim();

						//	if (!string.IsNullOrEmpty(subjStr))
						//		iTemSUBJ = subjStr.Trim();


						//	//ITEM ADD START
						//	dtItem.Rows.Add();
						//	dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
						//	dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
						//	dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = iTemUnit;
						//	if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
						//	if (!string.IsNullOrEmpty(iTemSUBJ))
						//		dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
						//	dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

						//	iTemDESC = string.Empty;
						//	iTemUnit = string.Empty;
						//	iTemCode = string.Empty;
						//	iTemQt = string.Empty;
						//	iTemSUBJ = string.Empty;
						//}
						//else if (getSpl.Length == 6)
						//{
						//	iTemNo = Regex.Replace(getSpl[0].ToString(), @"\D", "");
						//	iTemDESC = getSpl[0].ToString().Trim() + " " + getSpl[1].ToString().Trim() + " " + getSpl[2].ToString().Trim();
						//	//iTemCode = getSpl[1].ToString().Trim();
						//	iTemQt = getSpl[3].ToString().Trim();
						//	iTemUnit = getSpl[4].ToString().Trim();

						//	if (!string.IsNullOrEmpty(subjStr))
						//		iTemSUBJ = subjStr.Trim();


						//	//ITEM ADD START
						//	dtItem.Rows.Add();
						//	dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
						//	dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
						//	dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = iTemUnit;
						//	if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
						//	if (!string.IsNullOrEmpty(iTemSUBJ))
						//		dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
						//	dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

						//	iTemDESC = string.Empty;
						//	iTemUnit = string.Empty;
						//	iTemCode = string.Empty;
						//	iTemQt = string.Empty;
						//	iTemSUBJ = string.Empty;
						//}

					}
				}
			}
		}
	}
}

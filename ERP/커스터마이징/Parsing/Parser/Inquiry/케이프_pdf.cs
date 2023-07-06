using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Dintec;
using Dintec.Parser;

namespace Parsing
{
	class 케이프_pdf
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



		public 케이프_pdf(string fileName)
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


			#region ########### READ ###########
			// Pdf를 Xml로 변환해서 분석 (1000$ 짜리로 해야함, 500$ 짜리로 하면 Description 부분에 CRLF가 안됨)
			// 1. 우선 500$ 짜리로 Xml 변환함 (1000$ 짜리의 경우 도면이 붙어 있으면 시간이 엄청 오래 걸림)
			string xmlTemp = PdfReader.ToXml(fileName);

			// 2. 도면을 제외한 Page 카운트 가져오기
			//int pageCount = xmlTemp.Count("<page>");

			// 3. 앞서 나온 Page를 근거로 파싱 시작			
			//string xml = string.Empty;//PdfReader.GetXml(fileName, 1, pageCount);
			//xml = PdfReader.GetXml(fileName);
			#endregion ########### READ ###########

			// xml row 나누기
			string[] xmlSpl = { };

			if (!string.IsNullOrEmpty(xmlTemp))
			{
				xmlSpl = xmlTemp.Split(new string[] { "<row>" }, StringSplitOptions.None);
			}

			if (xmlTemp.Contains("REMARK") && xmlTemp.Contains("<cell>1</cell>"))
			{
				int idx_s = xmlTemp.IndexOf("REMARK");
				int idx_e = xmlTemp.IndexOf("<cell>1</cell>");

				if (idx_s != -1 && idx_e != -1)
				{
					string getStr = xmlTemp.Substring(idx_s, idx_e - idx_s).Trim();

					string pattern = @"<[^>]+>";
					string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Replace("REMARK", "").Trim();

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
					string patternItem = @"<cell>\d+</cell>";
					Regex regexItem = new Regex(patternItem);

					Match match = regexItem.Match(xmlSpl[r].ToString());

					if (xmlSpl[r].ToString().Contains("VESSEL"))
					{
						// VESSEL
						string getStr = xmlSpl[r].ToString().Trim();

						string pattern = @"<[^>]+>";
						string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

						while (result.Contains("  "))
						{
							result = result.Replace("  ", " ").Trim();
						}

						vessel = result.Replace("VESSEL", "").Replace(":", "").Trim();


					}
					else if (xmlSpl[r].ToString().Contains("INQ-NO"))
					{
						// REFERENCE
						string getStr = xmlSpl[r].ToString().Trim();

						string pattern = @"<[^>]+>";
						string result = Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

						while (result.Contains("  "))
						{
							result = result.Replace("  ", " ").Trim();
						}

						string[] getSpl = result.Split(new string[] { "INQ-NO" }, StringSplitOptions.None);

						if (getSpl.Length > 1)
						{
							reference = getSpl[1].ToString().Replace(":", "").Replace(".", "").Trim();
						}
					}
					else if (match.Success)
					{
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
							iTemDESC = getSpl[2].ToString().Trim();
							iTemCode = getSpl[1].ToString().Trim();
							iTemQt = getSpl[3].ToString().Trim();
							iTemUnit = getSpl[4].ToString().Trim();


							int _r = r + 1;
							match = regexItem.Match(xmlSpl[_r].ToString());

							while (!match.Success)
							{
								getStr = xmlSpl[_r].ToString().Trim();

								if (getStr.ToUpper().Contains("REMARK"))
									break;
								//if (getStr.Contains("</table>"))
								iTemDESC = iTemDESC + " " + Regex.Replace(getStr, pattern, "").Replace("\r\n", "").Trim();

								_r += 1;

								if (_r >= xmlSpl.Length)
									break;

								match = regexItem.Match(xmlSpl[_r].ToString());

								//if (match.Success)
								//	break;
							}

							if (!string.IsNullOrEmpty(subjStr))
								iTemSUBJ = subjStr.Trim();

							if (!string.IsNullOrEmpty(iTemCode))
								iTemDESC = iTemDESC.Trim() + Environment.NewLine + iTemCode.Trim();


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
						}
						else
						{

						}
					}
				}
			}
		}
	}
}

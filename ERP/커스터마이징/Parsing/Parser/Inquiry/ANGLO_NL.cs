using Dintec;
using Dintec.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parsing.Parser.Inquiry
{
	class ANGLO_NL
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



		public ANGLO_NL(string fileName)
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

			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;
			string iTemUniq = string.Empty;

			string subjStr = string.Empty;
			string makerStr = string.Empty;


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
					string firstStr = dt.Rows[i][0].ToString().ToLower();

					//if (string.IsNullOrEmpty(reference))
					//{
					//	for (int c = 0; c < dt.Columns.Count; c++)
					//	{
					//		if (dt.Rows[i][c].ToString().ToLower().StartsWith("our inquiry"))
					//		{
					//			reference = dt.Rows[i][c].ToString().Replace("Our", "").Replace("Inquiry", "").Trim();
					//		}
					//	}
					//}

					if (firstStr.StartsWith("end of quotation reques")) break;



					if (string.IsNullOrEmpty(imoNumber))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().Contains("imo"))
							{
								imoNumber = dt.Rows[i][c].ToString().ToLower().Trim();
								int idx_s = imoNumber.IndexOf("imo");

								if (idx_s != -1)
									imoNumber = imoNumber.Substring(idx_s, imoNumber.Length - idx_s).Replace("imo", "").Trim();
							}
						}
					}

					if(firstStr.StartsWith("working air dryer"))
					{
						for(int c = 1; c < dt.Columns.Count; c++)
							reference = reference.Trim() + dt.Rows[i][c].ToString().Trim();
					}
					else if (firstStr.StartsWith("note to supplier"))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							subjStr = subjStr + " " + dt.Rows[i][c].ToString().Trim();
						}

						int _i = i + 1;

						while (!GetTo.IsInt(dt.Rows[_i][0].ToString()))
						{
							for (int c = 0; c < dt.Columns.Count; c++)
							{
								subjStr = subjStr + " " + dt.Rows[_i][c].ToString().Trim();
							}

							subjStr = subjStr.Trim() + Environment.NewLine;

							_i += 1;

							if (_i >= dt.Rows.Count)
								break;
						}
					}
					else if (firstStr.Equals("item"))
					{
						string forStr = string.Empty;

						for (int c = 1; c < dt.Columns.Count; c++)
						{
							forStr = dt.Rows[i][c].ToString().ToLower();

							if (forStr.Contains("description")) _itemDesc = c;
							else if (forStr.Contains("unit")) _itemUnit = c;
							else if (forStr.Contains("quantity")) _itemQt = c;
						}
					}
					else if (GetTo.IsInt(firstStr))
					{
						if (dt.Rows[i - 1][0].ToString().ToLower().StartsWith("maker name"))
						{
							for (int c = 0; c < dt.Columns.Count; c++)
							{
								makerStr = dt.Rows[i][c].ToString().Trim();
							}
						}

						if (_itemUnit != -1)
							iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

						if (_itemQt != -1)
							iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

						if (_itemDesc != -1)
						{
							for (int c = _itemDesc; c < _itemQt; c++)
							{
								iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
							}

							int _i = i + 1;

							while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
							{
								for (int c = _itemDesc; c < dt.Columns.Count; c++)
								{
									iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
								}

								_i += 1;

								if (_i >= dt.Rows.Count)
									break;
							}
						}


						if (!string.IsNullOrEmpty(subjStr))
							iTemSUBJ = subjStr;

						if (!string.IsNullOrEmpty(makerStr))
							iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + makerStr;

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

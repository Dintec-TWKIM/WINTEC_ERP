using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class Shell_pdf
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



		public Shell_pdf(string fileName)
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


					if (string.IsNullOrEmpty(vessel))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("Ship:"))
							{
								vessel = dt.Rows[i][c + 1].ToString().Trim();
							}
						}
					}


					if (firstColStr.StartsWith("quotation reference:"))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (string.IsNullOrEmpty(reference))
							{
								reference = dt.Rows[i][c].ToString().ToLower().Replace("quotation reference:", "").Trim();
							}
							else
								break;
						}
					}
					else if (firstColStr.Equals("item ref"))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
							//else if (dt.Rows[i][c].ToString().Equals("Order Ref.")) _itemCode = c;
							else if (dt.Rows[i][c].ToString().Equals("Quantity")) _itemQt = c;
							//else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
						}

						int _i = i + 1;

						subjStr = string.Empty;

						while (!GetTo.IsInt(dt.Rows[_i][0].ToString()) && _i < dt.Rows.Count)
						{
							if (!dt.Rows[_i][0].ToString().Contains("__________"))
							{
								for (int c = 0; c < dt.Columns.Count; c++)
								{
									subjStr = subjStr.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
								}
							}

							_i += 1;

							if (dt.Rows.Count <= _i)
								break;
						}
					}
					else if (GetTo.IsInt(firstColStr))
					{
						iTemNo = firstColStr;


						if (_itemQt != -1)
						{
							string[] qtSpl = dt.Rows[i][_itemQt].ToString().Split(' ');

							if (qtSpl.Length > 0)
							{
								iTemQt = qtSpl[0].ToString().Trim();
								iTemUnit = qtSpl[1].ToString().Trim();
							}
						}


						if (_itemDesc != -1)
						{
							iTemDESC = dt.Rows[i][_itemDesc].ToString();

							int _i = i + 1;

							if (_i < dt.Rows.Count - 1)
							{
								while (!GetTo.IsInt(dt.Rows[_i][0].ToString()))
								{
									if (dt.Rows[_i][_itemQt].ToString().StartsWith("Manu Ref"))
									{
										iTemCode = dt.Rows[_i][_itemQt].ToString().Replace("Manu Ref", "").Replace(":", "").Trim();
									}

									_i += 1;

									if (dt.Rows.Count >= _i)
										break;
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

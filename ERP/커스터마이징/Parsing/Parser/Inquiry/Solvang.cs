using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class Solvang
	{
		string vessel;
		string reference;
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

		#endregion ==================================================================================================== Constructor



		public Solvang(string fileName)
		{
			vessel = "";                        // 선명
			reference = "";                     // 문의번호

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
			string iTemNo = string.Empty;
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;

			int _itemunit = -1;
			int _itemqt = -1;
			int _itemdesc = -1;
			int _itemCode = -1;


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


			bool itemStart = false;

			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string dataValue = dt.Rows[i][0].ToString().ToLower().Trim();


					// Reference
					if (string.IsNullOrEmpty(reference))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().StartsWith("request no."))
							{
								reference = dt.Rows[i][c].ToString().ToLower().Replace("request no.", "").Replace(":", "").Trim();
							}
						}
					}

					// Vessel
					if (dataValue.StartsWith("for:"))
					{
						vessel = dt.Rows[i][1].ToString().Trim();
					}
					// Subject
					else if (dataValue.StartsWith("equipment"))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
						}

						int _i = i + 1;

						while (!dt.Rows[_i][0].ToString().ToLower().Contains("delivery address"))
						{
							for (int c = 0; c < dt.Columns.Count; c++)
							{
								subjStr = subjStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
							}

							_i += 1;

							if (_i >= dt.Rows.Count)
								break;
						}

						subjStr = subjStr.ToLower().Replace("capacity", "\r\ncapacity").Replace("maker", "\r\nmaker").Replace("drawing", "\r\ndrawing").Replace("type", "\r\ntype").Replace("description", "\r\ndescription")
							.Replace("serial", "\r\nserial").Replace("model", "\r\nmodel").Trim();
					}
					// ITEM
					else if (dataValue.StartsWith("no.:"))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().Contains("item name")) _itemdesc = c;
							else if (dt.Rows[i][c].ToString().ToLower().Contains("part number")) _itemCode = c;
							else if (dt.Rows[i][c].ToString().Contains("Quantity:")) _itemqt = c;
							else if (dt.Rows[i][c].ToString().Contains("Unit:")) _itemunit = c;
						}
					}
					else if (GetTo.IsInt(dataValue))
					{
						iTemNo = dataValue.Trim();

						if (_itemdesc != -1)
							iTemDESC = dt.Rows[i][_itemdesc].ToString().Trim();

						if (_itemCode != -1)
							iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

						if (_itemqt != -1)
							iTemQt = dt.Rows[i][_itemqt].ToString().Trim();

						if (_itemunit != -1)
							iTemUnit = dt.Rows[i][_itemunit].ToString().Trim();

						if (!string.IsNullOrEmpty(subjStr))
							iTemSUBJ = subjStr;


						if (iTemQt.Contains(","))
							iTemQt = iTemQt.Replace(",", ".").Trim();


						// ########## ITEM ADD START
						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
						dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
						if (GetTo.IsInt(iTemQt.Replace(".", "")))
							if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
						if (!string.IsNullOrEmpty(iTemSUBJ))
							dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
						dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;


						iTemUnit = string.Empty;
						iTemQt = string.Empty;
						iTemDESC = string.Empty;
						iTemCode = string.Empty;
						iTemSUBJ = string.Empty;
					}
				}
			}
		}
	}
}

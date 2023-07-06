using Dintec;
using System.Data;
using System.Linq;

namespace Parsing
{
	internal class Doosan_2
	{
		private DataTable dtItem;

		private string fileName;

		#region ==================================================================================================== Property

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		#endregion ==================================================================================================== Property

		public Doosan_2(string fileName)
		{
			dtItem = new DataTable();
			dtItem.Columns.Add("CD");         //
			dtItem.Columns.Add("NM");         // 주제
			this.fileName = fileName;
		}

		public void Parse()
		{
			string iTemCode = string.Empty;
			string iTemName = string.Empty;

			int _itemCode = -1;
			int _itemName = -1;

			string CodeStr = string.Empty;

			bool _item1 = false;
			bool _item2 = false;
			bool _item3 = false;

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

			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string firstColStr = dt.Rows[i][0].ToString();
					string secondColStr = dt.Rows[i][1].ToString();
					string thirdColStr = dt.Rows[i][2].ToString();

					if (firstColStr.StartsWith("Item"))
					{
						_item1 = true;
						_item2 = false;
						_item3 = false;

						CodeStr = string.Empty;

						for (int c = 0; c < dt.Columns.Count; c++)
						{
							CodeStr = CodeStr.Trim() + dt.Rows[i - 1][c].ToString().Trim();
						}
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().StartsWith("Item No") || dt.Rows[i][c].ToString().Equals("Item no") || dt.Rows[i][c].ToString().Contains("No")) _itemCode = c;
							else if (dt.Rows[i][c].ToString().StartsWith("Item Des") || dt.Rows[i][c].ToString().StartsWith("Item des") || dt.Rows[i][c].ToString().Contains("esignation") || dt.Rows[i + 1][c].ToString().StartsWith("Item Des") || dt.Rows[i + 1][c].ToString().StartsWith("Item des") || dt.Rows[i + 1][c].ToString().Contains("esignation") || dt.Rows[i - 1][c].ToString().StartsWith("Item Des") || dt.Rows[i - 1][c].ToString().StartsWith("Item des") || dt.Rows[i - 1][c].ToString().Contains("esignation"))
							{
								_itemCode = c - 1;
								_itemName = c;
								break;
							}
						}
					}
					else if (secondColStr.StartsWith("Item N") || secondColStr.StartsWith("Item n") || secondColStr.StartsWith("Item"))
					{
						_item1 = false;
						_item2 = true;
						_item3 = false;

						CodeStr = string.Empty;

						for (int c = 0; c < dt.Columns.Count; c++)
						{
							CodeStr = CodeStr.Trim() + dt.Rows[i - 1][c].ToString().Trim();
						}
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().StartsWith("Item N") || dt.Rows[i][c].ToString().Equals("Item n") || dt.Rows[i][c].ToString().Contains("N")) _itemCode = c;
							else if (dt.Rows[i][c].ToString().StartsWith("Item Des") || dt.Rows[i][c].ToString().StartsWith("Item des") || dt.Rows[i][c].ToString().Contains("esignation") || dt.Rows[i + 1][c].ToString().StartsWith("Item Des") || dt.Rows[i + 1][c].ToString().StartsWith("Item des") || dt.Rows[i + 1][c].ToString().Contains("esignation") || dt.Rows[i - 1][c].ToString().StartsWith("Item Des") || dt.Rows[i - 1][c].ToString().StartsWith("Item des") || dt.Rows[i - 1][c].ToString().Contains("esignation"))
							{
								_itemCode = c - 1;
								_itemName = c;
								break;
							}
						}
					}
					else if (thirdColStr.StartsWith("Item N") || thirdColStr.StartsWith("Item n"))
					{
						_item1 = false;
						_item2 = false;
						_item3 = true;

						CodeStr = string.Empty;

						for (int c = 0; c < dt.Columns.Count; c++)
						{
							CodeStr = CodeStr.Trim() + dt.Rows[i - 1][c].ToString().Trim();
						}
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().StartsWith("Item No") || dt.Rows[i][c].ToString().Equals("Item no") || dt.Rows[i][c].ToString().Contains("No")) _itemCode = c;
							else if (dt.Rows[i][c].ToString().StartsWith("Item Des") || dt.Rows[i][c].ToString().StartsWith("Item des") || dt.Rows[i][c].ToString().Contains("esignation") || dt.Rows[i + 1][c].ToString().StartsWith("Item Des") || dt.Rows[i + 1][c].ToString().StartsWith("Item des") || dt.Rows[i + 1][c].ToString().Contains("esignation") || dt.Rows[i - 1][c].ToString().StartsWith("Item Des") || dt.Rows[i - 1][c].ToString().StartsWith("Item des") || dt.Rows[i - 1][c].ToString().Contains("esignation"))
							{
								_itemCode = c - 1;
								_itemName = c;
								break;
							}
						}
					}
					else if (!string.IsNullOrEmpty(firstColStr) && _item1)
					{
						if (GetTo.IsInt(firstColStr.Substring(0, 1)))
						{
							if (!_itemCode.Equals(-1))
							{
								iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

								if (!GetTo.IsInt(iTemCode.Substring(0, 1)))
								{
									iTemCode = dt.Rows[i][_itemCode - 1].ToString().Trim();
								}
							}

							if (!_itemName.Equals(-1))
							{
								iTemName = dt.Rows[i][_itemName].ToString().Trim();

								if (string.IsNullOrEmpty(iTemName))
								{
									iTemName = dt.Rows[i][_itemName - 1].ToString().Trim();
								}
							}

							iTemCode = CodeStr.Trim() + "-" + iTemCode.Trim();

							//ITEM ADD START
							dtItem.Rows.Add();
							dtItem.Rows[dtItem.Rows.Count - 1]["CD"] = iTemCode;
							dtItem.Rows[dtItem.Rows.Count - 1]["NM"] = iTemName;

							iTemCode = string.Empty;
							iTemName = string.Empty;
						}
					}
					else if (!string.IsNullOrEmpty(secondColStr) && _item2)
					{
						if (GetTo.IsInt(secondColStr.Substring(0, 1)))
						{
							if (!_itemCode.Equals(-1))
								iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

							if (!_itemName.Equals(-1))
								iTemName = dt.Rows[i][_itemName].ToString().Trim();

							iTemCode = CodeStr.Trim() + "-" + iTemCode.Trim();

							//ITEM ADD START
							dtItem.Rows.Add();
							dtItem.Rows[dtItem.Rows.Count - 1]["CD"] = iTemCode;
							dtItem.Rows[dtItem.Rows.Count - 1]["NM"] = iTemName;

							iTemCode = string.Empty;
							iTemName = string.Empty;
						}
					}
					else if (!string.IsNullOrEmpty(thirdColStr) && _item3)
					{
						if (GetTo.IsInt(thirdColStr.Substring(0, 1)))
						{
							if (!_itemCode.Equals(-1))
								iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

							if (!_itemName.Equals(-1))
								iTemName = dt.Rows[i][_itemName].ToString().Trim();

							iTemCode = CodeStr.Trim() + "-" + iTemCode.Trim();

							//ITEM ADD START
							dtItem.Rows.Add();
							dtItem.Rows[dtItem.Rows.Count - 1]["CD"] = iTemCode;
							dtItem.Rows[dtItem.Rows.Count - 1]["NM"] = iTemName;

							iTemCode = string.Empty;
							iTemName = string.Empty;
						}
					}
				}
			}
		}
	}
}
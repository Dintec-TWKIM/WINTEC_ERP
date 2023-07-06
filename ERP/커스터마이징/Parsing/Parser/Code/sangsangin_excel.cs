using Dintec;
using Dintec.Parser;
using System.Data;

namespace Parsing
{
	internal class sangsangin_excel
	{
		private string contact;
		private string reference;
		private string vessel;
		private string imoNumber;
		private DataTable dtItem;
		private DataTable dtItemAll;

		private string fileName;
		private UnitConverter uc;

		private int _itemDesc = -1;
		private int _itemMalt = -1;
		private int _itemQt = -1;
		private int _itemWt = -1;
		private int _itemRmk = -1;
		private int _itemNo = -1;
		private int _itemSize = -1;
		private int _itemSymbol = -1;
		private int _itemSpec = -1;
		private int _itemModel = -1;
		private int _itemMaker = -1;
		private int _itemCode = -1;
		private int _itemLoc = -1;
		private int _itemDwg = -1;

		private int _itemDesc2 = -1;
		private int _itemMalt2 = -1;
		private int _itemQt2 = -1;
		private int _itemWt2 = -1;
		private int _itemRmk2 = -1;
		private int _itemNo2 = -1;
		private int _itemSize2 = -1;
		private int _itemSymbol2 = -1;
		private int _itemSpec2 = -1;
		private int _itemModel2 = -1;
		private int _itemMaker2 = -1;
		private int _itemCode2 = -1;
		private int _itemLoc2 = -1;
		private int _itemDwg2 = -1;

		private string itemDesc = string.Empty;
		private string itemMalt = string.Empty;
		private string itemQt = string.Empty;
		private string itemWt = string.Empty;
		private string itemRmk = string.Empty;
		private string itemSize = string.Empty;
		private string itemSymbol = string.Empty;
		private string itemSpec = string.Empty;
		private string itemModel = string.Empty;
		private string itemMaker = string.Empty;
		private string itemCode = string.Empty;
		private string itemLoc = string.Empty;
		private string itemDwg = string.Empty;
		private string itemNo = string.Empty;

		#region ==================================================================================================== Property

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		public DataTable ItemAll
		{
			get
			{
				return dtItemAll;
			}
		}

		#endregion ==================================================================================================== Property

		#region ==================================================================================================== Constructor

		public sangsangin_excel(string fileName)
		{
			reference = "";
			vessel = "";

			dtItem = new DataTable();
			dtItem.Columns.Add("PAGENO");
			dtItem.Columns.Add("SHIPYARD");
			dtItem.Columns.Add("PROJECT");
			dtItem.Columns.Add("TITLE");
			dtItem.Columns.Add("HULLNO");
			dtItem.Columns.Add("DESC");
			dtItem.Columns.Add("MALT");
			dtItem.Columns.Add("QT");
			dtItem.Columns.Add("WT");
			dtItem.Columns.Add("RMK");
			dtItem.Columns.Add("DWGNO");
			dtItem.Columns.Add("SIZE");
			dtItem.Columns.Add("SYMBOL");
			dtItem.Columns.Add("CODE");
			dtItem.Columns.Add("SPECIFICATION");
			dtItem.Columns.Add("MODEL");
			dtItem.Columns.Add("MAKER");
			dtItem.Columns.Add("LOC");
			dtItem.Columns.Add("NO");
			this.fileName = fileName;

			// 전체 데이터 테이블 표시 하기
			dtItemAll = new DataTable();

			this.fileName = fileName;
			this.uc = new UnitConverter();
		}

		#endregion ==================================================================================================== Constructor

		public void Parse()
		{
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;
			string iTemUniq = string.Empty;

			int _itemDesc = -1;
			int _itemCode = -1;
			int _itemQt = -1;
			int _itemUnit = -1;

			string subjStr = string.Empty;
			string subjStr2 = string.Empty;

			// 엑셀 읽기
			DataSet ds = ExcelReader.ToDataSet(fileName);
			//DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴

			//DataSet Table 병합을 위한 Table
			//DataTable dsAll = new DataTable();

			int tableColCount = 0;

			//DataSet Table의 Count Get
			int dsCount = ds.Tables.Count;

			for (int i = 0; i <= dsCount - 1; i++)
			{
				ds.Tables[i].Rows.Add();
				ds.Tables[i].Rows.Add();
				ds.Tables[i].Rows.Add();
				ds.Tables[i].Rows.Add();

				tableColCount = ds.Tables[i].Columns.Count;

				for (int c = 0; c < tableColCount; c++)
				{
					ds.Tables[i].Rows[ds.Tables[i].Rows.Count - 4][c] = "----------------------";
					ds.Tables[i].Rows[ds.Tables[i].Rows.Count - 3][c] = "----------------------";
					ds.Tables[i].Rows[ds.Tables[i].Rows.Count - 2][c] = "----------------------";
					ds.Tables[i].Rows[ds.Tables[i].Rows.Count - 1][c] = i + 1 + " PAGE";
				}

				//dsAll.Merge(ds.Tables[i]);
				ItemAll.Merge(ds.Tables[i]);
			}

			ds.Clear();
			ds.Tables.Add(ItemAll);

			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string firstColStr = dt.Rows[i][1].ToString();

					if (firstColStr.StartsWith("No."))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
							else if (dt.Rows[i][c].ToString().Contains("Part No.")) _itemCode = c;
							else if (dt.Rows[i][c].ToString().Equals("Quantity Requested")) _itemQt = c;
							else if (dt.Rows[i][c].ToString().Equals("UoM")) _itemUnit = c;
						}
					}
					else if (GetTo.IsInt(firstColStr.Replace(".", "")))
					{
						//dtItem.Rows.Add();
						//dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
						//if (!string.IsNullOrEmpty(iTemSUBJ))
						//    dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
						//dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
						//dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;
						//dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
						//if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
						//dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
					}
				}
			}
		}
	}
}
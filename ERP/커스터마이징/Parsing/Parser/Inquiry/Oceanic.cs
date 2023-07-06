using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Oceanic
    {
         string contact;
		string reference;
		string vessel;
		string imoNumber;
		DataTable dtItem;

		string fileName;
		UnitConverter uc;

		#region ==================================================================================================== Property

		public string Contact
		{
			get
			{
				return contact;
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

		public string Vessel
		{
			get
			{
				return vessel;
			}
		}

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

        public Oceanic(string fileName)
		{
			contact = "";
			reference = "";
			vessel = "";
			imoNumber = "";

			dtItem = new DataTable();
			dtItem.Columns.Add("NO");
			dtItem.Columns.Add("SUBJ");
			dtItem.Columns.Add("ITEM");
			dtItem.Columns.Add("DESC");
			dtItem.Columns.Add("UNIT");
			dtItem.Columns.Add("QT");
            dtItem.Columns.Add("UNIQ");         //선사코드

			this.fileName = fileName;
			this.uc = new UnitConverter();
		}

		#endregion

		#region ==================================================================================================== Logic		

		public void Parse()
		{
            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemUnit = -1;
            int _itemQt = -1;
            int _itemSubj = -1;
            int _itemDesc2 = -1;
            int _itemUniq = -1;

            string iTemDesc = string.Empty;
            string iTemCode = string.Empty;
            string iTemQt = string.Empty;
            string iTemUnit = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemUniq = string.Empty;

            string descStr = string.Empty;

            bool itemStart = false;

            // 엑셀 읽기
			DataSet ds = ExcelReader.ToDataSet(fileName);
			DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴

            vessel = dt.Rows[5][3].ToString();
            reference = dt.Rows[4][6].ToString();

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				string firstColString = dt.Rows[i][0].ToString();

                if(i > 1)
                    if(dt.Rows[i-1][0].ToString().Equals("Family"))
                        itemStart = true;


                if (!itemStart)
                {
                    // ########## 문의번호, 선명 ########## //

					if(string.IsNullOrEmpty(reference))
					{
						if (firstColString.Contains("Quotation Request"))
							reference = dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim();
					}
					else if (string.IsNullOrEmpty(vessel))
                    {
                        if (firstColString.Contains("Vessel"))
                            vessel = dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim();
                    }

                    if (firstColString.Equals("ITEM LIST"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i + 1][c].ToString().Equals("Family")) _itemSubj = c;
                            else if (dt.Rows[i + 1][c].ToString().Equals("ISSA Code")) _itemCode = c;
                            else if (dt.Rows[i + 1][c].ToString().Equals("Item")) _itemDesc = c;
                            else if (dt.Rows[i + 1][c].ToString().Equals("Order Qty.")) _itemQt = c;
                            else if (dt.Rows[i + 1][c].ToString().Equals("Unit")) _itemUnit = c;
                            else if (dt.Rows[i + 1][c].ToString().Equals("Description")) _itemDesc2 = c;
                            else if (dt.Rows[i + 1][c].ToString().Equals("VSC Code")) _itemUniq = c;
                        }
                    }
                }
                else
                {
                    // ########## 아이템 추가모드 ########## //
                    
                    if(!_itemDesc.Equals(-1))
                        iTemDesc = dt.Rows[i][_itemDesc].ToString().Trim();

                    if(!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Replace("*","").Trim();

                    if(!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                    if(!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                    
                    if(!_itemSubj.Equals(-1))
                        iTemSUBJ = dt.Rows[i][_itemSubj].ToString().Trim();

                    if (!_itemUniq.Equals(-1))
                        iTemUniq = dt.Rows[i][_itemUniq].ToString().Trim();

                    if (!_itemDesc2.Equals(-1))
                    {
                        descStr = dt.Rows[i][_itemDesc2].ToString().Trim();
                    }

                    if(!string.IsNullOrEmpty(descStr))
                        iTemDesc = iTemDesc.Trim() + ", " + descStr.Trim();
                    


                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColString;
                    dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDesc;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                    if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                    if (!string.IsNullOrEmpty(iTemUniq))
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;

                    iTemDesc = string.Empty;
                    iTemQt = string.Empty;
                    iTemUnit = string.Empty;
                    iTemCode = string.Empty;
                    iTemUniq = string.Empty;

                    descStr = string.Empty;
                }
			}
		}

		#endregion
    }
}

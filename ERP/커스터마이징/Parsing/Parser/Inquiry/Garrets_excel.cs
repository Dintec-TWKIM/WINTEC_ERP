using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Garrets_excel
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

        public Garrets_excel(string fileName)
        {
            reference = "";
            vessel = "";

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


        public void Parse()
        {
            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;
            string iTemDrw = string.Empty;
            string iTemRmk = string.Empty;
			string iTemUniq = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemRmk = -1;
			int _itemImpa = -1;

            string subjStr = string.Empty;
            string itemDwg = string.Empty;

            string makerStr = string.Empty;
            string modelStr = string.Empty;
            string typeStr = string.Empty;
            string SerialStr = string.Empty;


            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string firstColStr = dt.Rows[i][0].ToString();

                if (string.IsNullOrEmpty(vessel))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("Customer Name"))
                            vessel = dt.Rows[i][c + 1].ToString().Trim() + dt.Rows[i][c + 2].ToString().Trim();
                    }
                }

                if (string.IsNullOrEmpty(reference))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("Request for Qu"))
                            reference = dt.Rows[i][c + 1].ToString().Trim() + dt.Rows[i][c + 2].ToString().Trim();
                    }
                }

               
                if (firstColStr.StartsWith("Line Number"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
						if (dt.Rows[i][c].ToString().Contains("Item Number")) _itemCode = c;
						else if (dt.Rows[i][c].ToString().Contains("Product Name")) _itemDesc = c;
						else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
						else if (dt.Rows[i][c].ToString().Contains("Quantity")) _itemQt = c;
						else if (dt.Rows[i][c].ToString().Contains("Remarks")) _itemRmk = c;
						else if (dt.Rows[i][c].ToString().Contains("IMPA Number")) _itemImpa = c;
                    }
                }
                else if (GetTo.IsInt(firstColStr.Replace(".","")))
                {
                    if (!_itemQt.Equals(-1))
                    {
                        iTemQt = dt.Rows[i][_itemQt].ToString().Replace(".00", "").Trim();


                        iTemQt = iTemQt.Replace(".", "").Replace(",", "").Trim();
                    }

                    if (!_itemCode.Equals(-1))
                        iTemUniq = dt.Rows[i][_itemCode].ToString().Replace(",","").Trim();

					if (!_itemImpa.Equals(-1))
						iTemCode = dt.Rows[i][_itemImpa].ToString().Replace(",", "").Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    if (!_itemDesc.Equals(-1))
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (_itemRmk != -1)
                        iTemRmk = dt.Rows[i][_itemRmk].ToString().Trim();

                    if (!string.IsNullOrEmpty(iTemRmk))
                        iTemDESC = iTemDESC.Trim() + Environment.NewLine + "**" + iTemRmk;

                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                    if (!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                    if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
					dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;

                    iTemDESC = string.Empty;
                    iTemCode = string.Empty;
                    iTemQt = string.Empty;
                    iTemUnit = string.Empty;
                    iTemDrw = string.Empty;
                    iTemSUBJ = string.Empty;
                    iTemRmk = string.Empty;
                }
            }
        }
    }
}

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class excutive_excel
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

        public excutive_excel(string fileName)
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
            string iTemUniq = string.Empty;
            string iTemRmk = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemMaker = -1;

            string subjStr = string.Empty;
           

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                 string firstColStr = dt.Rows[i][0].ToString().ToUpper();

                if (firstColStr.Equals("NUMBER"))
				{
                    
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Equals("Maker Reference")) _itemMaker = c;
                        else if (dt.Rows[i][c].ToString().Equals("Part Name")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().Equals("Order Qty")) _itemQt = c;
                        //else if (dt.Rows[i][c].ToString().Equals("ICN")) _itemUniq = c;
                        else if (dt.Rows[i][c].ToString().Equals("Drawing No/Position")) _itemCode = c;
                    }
                }
                else if (_itemQt != -1 && GetTo.IsInt(dt.Rows[i][_itemQt].ToString().Replace(".","")))
                {
                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                    if (_itemDesc != -1)
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();


                    //if (_itemUniq != -1)
                    //    iTemUniq = dt.Rows[i][_itemUniq].ToString().Trim();

                    string dwgStr = string.Empty;
                    if (_itemMaker != -1)
                        dwgStr = dt.Rows[i][_itemMaker].ToString().Trim();

                    if (!string.IsNullOrEmpty(dwgStr))
                        iTemDESC = iTemDESC + Environment.NewLine + dwgStr;

                    //if (!string.IsNullOrEmpty(subjStr))
                    //    iTemSUBJ = subjStr;



                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = "";
                    if (!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                    //dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                    if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);

                    iTemDESC = string.Empty;
                    iTemCode = string.Empty;
                    iTemQt = string.Empty;
                    iTemUnit = string.Empty;
                    iTemSUBJ = string.Empty;
                }
            }
        }
    }
}

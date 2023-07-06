using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Gulf
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

        public Gulf(string fileName)
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
            string iTemMaker = string.Empty;
            string iTemType = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemDrw = -1;

            string subjStr = string.Empty;
            string typeStr = string.Empty;
            string sizeStr = string.Empty;
            string serialStr = string.Empty;
            string drwStr = string.Empty;
            string bookStr = string.Empty;
            string makerStr = string.Empty;

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][0].ToString();

                if (string.IsNullOrEmpty(reference))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().StartsWith("Requisition  No"))
                            reference = dt.Rows[i][c + 1].ToString().Trim() + dt.Rows[i][c + 2].ToString().Trim() + dt.Rows[i][c + 3].ToString().Trim();
                    }
                }

                if (firstColStr.StartsWith("Ship Name"))
                {
                    vessel = dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim();
                }
                else if (firstColStr.StartsWith("Machinery Details"))
                {
                    if (dt.Rows[i][1].ToString().Equals("Equipment"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr.Trim() + dt.Rows[i][c].ToString().Trim();

                            if (dt.Rows[i + 1][1].ToString().Equals("Manufacturer"))
                                makerStr = makerStr.Trim() + dt.Rows[i + 1][c].ToString().Trim();

                            if (dt.Rows[i + 2][1].ToString().Equals("Type"))
                            {
                                if (!dt.Rows[i + 2][c].ToString().Equals("Serial no"))
                                    typeStr = typeStr.Trim() + dt.Rows[i + 2][c].ToString().Trim();
                                else
                                    break;
                            }
                        }
                    }
                }
                else if (firstColStr.Equals("Item No"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Equals("Name / Description")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().StartsWith("Makers RefNo")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().StartsWith("Drawing/Position")) _itemDrw = c;
                    }
                }
                else if (GetTo.IsInt(firstColStr))
                {
                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Replace(".", "").Trim();

                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Replace("-", "").Trim();

                    if (!_itemDesc.Equals(-1))
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (!_itemDrw.Equals(-1))
                        drwStr = dt.Rows[i][_itemDrw].ToString().Trim();


                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = subjStr.Trim();

                    if (!string.IsNullOrEmpty(makerStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                    if (!string.IsNullOrEmpty(typeStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                    if (!string.IsNullOrEmpty(drwStr))
                        iTemDESC = iTemDESC.Trim() + Environment.NewLine + "DWG NO/POS: " + drwStr.Trim();


                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                    if (!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
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

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class HLineExcel
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

        public HLineExcel(string fileName)
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
            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            int _itemCodeInt = -1;
            int _itemDescInt = -1;
            int _itemQtInt = -1;
            int _itemUnitInt = -1;
            int _itemVesselInt = -1;

            int _subjMach = -1;


            string equipStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string drwStr = string.Empty;
            string remarkStr = string.Empty;


            bool itemKind = false;


            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColString = dt.Rows[i][1].ToString();

                if (firstColString.Contains("Quotation No") && string.IsNullOrEmpty(reference))
                {
                    for (int c = 2; c < 6; c++)
                    {
                        reference = reference.Trim() + dt.Rows[i][c].ToString().Trim();
                    }
                }
                else if (firstColString.Contains("Quotation Title"))
                {
                    for (int c = 2; c < 6; c++)
                    {
                        iTemSUBJ = iTemSUBJ.Trim() + dt.Rows[i][c].ToString().Trim();
                    }
                }
                else if (firstColString.Contains("Item No") || firstColString.Contains("RFQ Item") || firstColString.Contains("Seq"))
                {
                    if (firstColString.Contains("Seq"))
                        itemKind = true;

                    if(itemKind)
					{ 
                        // 기자재
                        for (int c = 2; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Qty")) _itemQtInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("DRAW NO.")) _itemDescInt = c;
                            //else if (dt.Rows[i][c].ToString().Contains("Specification")) _itemDescInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Mach. Desc.")) _subjMach = c;
                            else if (dt.Rows[i][c].ToString().Contains("Vessel")) _itemVesselInt = c;

                        }
                    }
                    else
					{
                        // 선용
                        for (int c = 2; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Qty")) _itemQtInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Need Date")) _itemCodeInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Specification")) _itemDescInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("W.H Need Date")) _itemUnitInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Vessel")) _itemVesselInt = c;

                        }
                    }

                }
                else if (GetTo.IsInt(firstColString))
                {
                    if (!itemKind)
                    {
                        if (!_itemVesselInt.Equals(-1))
                            vessel = dt.Rows[i][_itemVesselInt].ToString().Trim();

                        if (!_itemCodeInt.Equals(-1))
                            iTemCode = dt.Rows[i + 2][_itemCodeInt].ToString().Replace("IMPA", "").Replace("-", "").Trim();

                        if (!_itemDescInt.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i + 2][_itemDescInt].ToString().Trim();
                            makerStr = dt.Rows[i + 1][_itemDescInt].ToString().Replace("NONE", "").Trim();
                        }

                        if (!_itemUnitInt.Equals(-1))
                            iTemUnit = dt.Rows[i + 2][_itemUnitInt].ToString().Trim();

                        if (!_itemQtInt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();
                    }
                    else
					{
                        if (!_itemVesselInt.Equals(-1))
                            vessel = dt.Rows[i][_itemVesselInt].ToString().Trim();

                        if (_itemDescInt != -1)
                        {
                            iTemDESC = dt.Rows[i + 1][_itemDescInt].ToString().Trim();
                            iTemCode = dt.Rows[i + 2][_itemDescInt].ToString().Trim();
                        }

                        if (_itemQtInt != -1)
                            iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();

                        if (_subjMach != -1)
                        {
                            iTemSUBJ = dt.Rows[i][_subjMach].ToString().Trim();
                            iTemSUBJ = iTemSUBJ + Environment.NewLine + "MAKER: " + dt.Rows[i+2][_subjMach].ToString().Trim();
                        }

                        iTemUnit = "PCS";
					}

                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                    if(!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                    if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);

                    iTemCode = string.Empty;
                    iTemDESC = string.Empty;
                    iTemQt = string.Empty;
                    iTemUnit = string.Empty;
                }
            }
        }

        #endregion
    }
}

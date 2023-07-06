using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Laurel
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

        public Laurel(string fileName)
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

            string subjStr = string.Empty;
            string makerStr = string.Empty;
            string TypeStr = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemDrw = -1;
            int _itemSpec = -1;

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColString = dt.Rows[i][1].ToString();

                if (firstColString.StartsWith("INQUIRY NO"))
                {
                    reference = dt.Rows[i][2].ToString().Trim();
                }
                else if (firstColString.StartsWith("VESSEL"))
                {
                    vessel = dt.Rows[i][2].ToString().Trim();
                }
                else if (firstColString.StartsWith("EQUIPMENT"))
                {
                    subjStr = dt.Rows[i][2].ToString().Trim();
                }
                else if (firstColString.StartsWith("MAKER"))
                {
                    makerStr = dt.Rows[i][2].ToString().Trim();
                }
                else if (firstColString.StartsWith("TYPE"))
                {
                    TypeStr = dt.Rows[i][2].ToString().Trim();
                }
                else if (firstColString.StartsWith("ITEM"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Equals("DESCRIPTION")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Contains("DWG")) _itemDrw = c;
                        else if (dt.Rows[i][c].ToString().Contains("Q'TY")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Contains("UNIT")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().Contains("CODE")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().Contains("SPEC")) _itemSpec = c;
                    }
                }
                else if (GetTo.IsInt(firstColString))
                {

                    if (!_itemDesc.Equals(-1))
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();


                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = subjStr.Trim();

                    if (!string.IsNullOrEmpty(makerStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                    if (!string.IsNullOrEmpty(TypeStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + TypeStr.Trim();

                    if (!_itemDrw.Equals(-1))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG: " + dt.Rows[i][_itemDrw].ToString().Trim();

                    if (!_itemSpec.Equals(-1))
                        iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[i][_itemSpec].ToString().Trim();

                    iTemSUBJ = iTemSUBJ.Trim();


                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColString;
                    if (!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                    if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);

                    iTemCode = string.Empty;
                    iTemDESC = string.Empty;
                    iTemQt = string.Empty;
                    iTemUnit = string.Empty;
                    iTemSUBJ = string.Empty;
                }
            }
        }

        #endregion
    }
}

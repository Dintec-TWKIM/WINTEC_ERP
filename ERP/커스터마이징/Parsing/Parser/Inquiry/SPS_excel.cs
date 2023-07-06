using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class SPS_excel
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

        public SPS_excel(string fileName)
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
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string firstColStr = dt.Rows[i][1].ToString();

                if (string.IsNullOrEmpty(vessel))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("Vessel:"))
                            vessel = dt.Rows[i][c + 1].ToString().Trim();
                    }
                }

                if (string.IsNullOrEmpty(reference))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("Requisition No."))
                            reference = dt.Rows[i][c + 1].ToString().Trim();
                    }
                }


                if (string.IsNullOrEmpty(imoNumber))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("IMO:"))
                            imoNumber = dt.Rows[i][c + 1].ToString().Trim();
                    }
                }


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
                else if (firstColStr.StartsWith("Equipment"))
                {
                    subjStr = firstColStr.Replace("Equipment:", "").Trim();
                }
                else if (firstColStr.StartsWith("Assembly"))
                {
                    subjStr2 = firstColStr.Replace("Assembly:", "").Trim();
                }
                else if (GetTo.IsInt(firstColStr.Replace(".", "")))
                {
                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Replace(".00", "").Trim();

                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Replace(",", "").Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    if (!_itemDesc.Equals(-1))
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = subjStr.Trim();

                    if (!string.IsNullOrEmpty(subjStr2))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr2.Trim();

                    iTemSUBJ = iTemSUBJ.Trim();


                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                    if (!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;
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

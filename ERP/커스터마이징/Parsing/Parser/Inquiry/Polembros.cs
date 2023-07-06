using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Polembros
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

        public Polembros(string fileName)
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

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;

            string subjStr = string.Empty;
            string subjStr1 = string.Empty;
            string subjStr2 = string.Empty;

            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string makerStr = string.Empty;

            string drwStr = string.Empty;

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][1].ToString();
                string secondColStr = dt.Rows[i][2].ToString();
                string thirdColStr = dt.Rows[i][3].ToString();

                if ((secondColStr.StartsWith("Vessel:") || thirdColStr.StartsWith("Vessel")) && string.IsNullOrEmpty(vessel))
                {
                     vessel = dt.Rows[i][3].ToString().Replace("M/V","").Replace("M/T","").Trim();
                }
                else if (secondColStr.StartsWith("Our Reference") || thirdColStr.StartsWith("Our Reference"))
                {
                    reference = dt.Rows[i][3].ToString().Replace("M/V","").Replace("M/T","").Trim();
                }
                else if (firstColStr.Equals("No.") || secondColStr.Equals("No."))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("Item Description")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Equals("Quantity")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().Contains("Item No")) _itemCode = c;
                    }

                    int _i = i + 1;

                    while (string.IsNullOrEmpty(dt.Rows[_i][1].ToString()))
                    {
                        subjStr = subjStr.Trim() + Environment.NewLine + dt.Rows[_i][2].ToString().Trim();

                        if (!string.IsNullOrEmpty(dt.Rows[_i][3].ToString()))
                        {
                            subjStr1 = string.Empty;
                            subjStr1 = dt.Rows[_i][3].ToString().Trim();
                        }

                        _i += 1;

                        if (_i >= dt.Rows.Count)
                            break;
                    }
                }
                else if (GetTo.IsInt(firstColStr) || GetTo.IsInt(secondColStr))
                {
                    if (!_itemQt.Equals(-1))
                    {
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        int _i = i + 1;

                        while(string.IsNullOrEmpty(dt.Rows[_i][1].ToString()))
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[_i][3].ToString()))
                            {
                                subjStr1 = string.Empty;
                                subjStr1 = dt.Rows[_i][3].ToString().Trim();
                            }
                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }
                    }

                    if (!_itemCode.Equals(-1))
                    {
                        iTemCode = dt.Rows[i][_itemCode].ToString().Replace(",", "").Trim();
                    }

                    if (!_itemDesc.Equals(-1))
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = subjStr.Trim();

                    if (!string.IsNullOrEmpty(subjStr1))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr1.Trim();


                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                    if (!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                    if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);

                    iTemDESC = string.Empty;
                    iTemCode = string.Empty;
                    iTemQt = string.Empty;
                    iTemUnit = string.Empty;
                    iTemDrw = string.Empty;
                    iTemSUBJ = string.Empty;
                }
            }
        }
    }
}

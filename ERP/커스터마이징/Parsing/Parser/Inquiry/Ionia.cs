using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{

    class Ionia
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

        public Ionia(string fileName)
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
            int _itemPos = -1;

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
                string thirdColStr = dt.Rows[i][6].ToString();

                if (secondColStr.StartsWith("VESSEL"))
                {
                     vessel = dt.Rows[i][3].ToString().Trim();
                }

                if (thirdColStr.StartsWith("REF"))
                {
                    reference = dt.Rows[i][7].ToString().Trim();
                }
                
             
                if (firstColStr.Equals("#"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Equals("Qnty")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().Contains("Code")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().Contains("PosNo")) _itemPos = c;
                    }
                }
                else if (secondColStr.StartsWith("Machinery"))
                {
                    subjStr = string.Empty;
                    for (int c = 2; c < dt.Columns.Count; c++)
                    {
                        subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                    }
                    subjStr = subjStr.Replace("Type", "\r\nType").Replace("Serial", "\r\nSerial").Trim();
                }
                else if (secondColStr.StartsWith("Maker"))
                {
                    subjStr1 = string.Empty;
                    for (int c = 2; c < dt.Columns.Count; c++)
                    {
                        subjStr1 = subjStr1.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                    }
                    subjStr1 = subjStr1.Replace("Model", "\r\nModel").Replace("Book", "\r\nBook").Trim();
                }
                else if (GetTo.IsInt(firstColStr))
                {
                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                    if (!_itemDesc.Equals(-1))
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    //subjStr = subjStr.Replace("Type", "\r\nType").Replace("Serial", "\r\nSerial").Trim();
                    //subjStr1 = subjStr1.Replace("Model", "\r\nModel").Replace("Book", "\r\nBook").Trim();


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

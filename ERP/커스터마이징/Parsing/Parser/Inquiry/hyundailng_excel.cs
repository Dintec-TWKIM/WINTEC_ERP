using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class hyundailng_excel
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

        public hyundailng_excel(string fileName)
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
            int _itemUniq = -1;
            int _itemRmk = -1;

            string subjStr = string.Empty;
            string subjRmk = string.Empty;
                
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

                string firstColStr = dt.Rows[i][1].ToString().ToUpper();

                if (string.IsNullOrEmpty(vessel))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("VESSEL"))
                        {
                            for (int c2 = c+1; c2 < dt.Columns.Count; c2++)
                            {
                                if (string.IsNullOrEmpty(vessel))
                                {
                                    vessel = dt.Rows[i][c2].ToString().Replace(":", "").Trim();
                                }
                                else
                                    break;
                            }
                        }
                    }
                }


                if (string.IsNullOrEmpty(reference))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("OUR REF NO") && dt.Rows[i+1][c].ToString().Contains("IN CHARGE"))
                        {
                            for (int c2 = c+1; c2 < dt.Columns.Count; c2++)
                            {
                                if (string.IsNullOrEmpty(reference))
                                {
                                    reference = dt.Rows[i][c2].ToString().Replace(":", "").Trim();
                                }
                                else
                                    break;
                            }
                        }
                    }
                }




                if (firstColStr.Equals("NO."))
				{

                    
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("CODE")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().Contains("DESCRIPTION")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Equals("UNIT")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().Equals("QTY")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Equals("REMARK(SHIP)")) _itemRmk = c;
                    }
                }
                else if (GetTo.IsInt(firstColStr.Replace(".", "")))
                {
                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Replace(".00", "").Trim();

                    if (_itemDesc != -1)
                    {
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                        iTemSUBJ = dt.Rows[i-2][_itemDesc].ToString().Trim() + Environment.NewLine + dt.Rows[i-1][_itemDesc].ToString().Trim();
                    }

                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();


                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = subjStr;

                    if (!string.IsNullOrEmpty(subjRmk))
                        iTemSUBJ = iTemSUBJ + Environment.NewLine + subjRmk;



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
                    iTemRmk = string.Empty;
                }
            }
        }
    }
}

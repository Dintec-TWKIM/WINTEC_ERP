using Dintec;
using Dintec.Parser;
using System.Text.RegularExpressions;

using System;
using System.Data;

namespace Parsing
{
    class Synergy_excel
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

        public Synergy_excel(string fileName)
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

                string firstColStr = dt.Rows[i][0].ToString();

                if (string.IsNullOrEmpty(vessel))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("Vessel Name"))
                            vessel = dt.Rows[i][c+1].ToString().Trim();
                    }
                }

                if (string.IsNullOrEmpty(imoNumber))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("IMO No"))
                            imoNumber = dt.Rows[i][c + 1].ToString().Trim();
                    }
                }


                if (firstColStr.Equals("No."))
				{
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                            reference = dt.Rows[i][c].ToString().Trim();

                        if (!string.IsNullOrEmpty(reference))
                            break;
                    }
                }
                else if (firstColStr.Equals("Description"))
				{
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                            subjStr = dt.Rows[i][c].ToString().Trim();

                        if (!string.IsNullOrEmpty(subjStr))
                            break;
                    }
                }
                else if (firstColStr.Equals("Buyer Remarks"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                            subjRmk = dt.Rows[i][c].ToString().Trim();

                        if (!string.IsNullOrEmpty(subjRmk))
                            break;
                    }
                }
                else if (firstColStr.StartsWith("#"))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("Component Details")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Equals("Req. UOM")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().Equals("Req. Qty")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Equals("Remarks")) _itemRmk = c;
                    }
                }
                else if (GetTo.IsInt(firstColStr.Replace(".", "")))
                {
                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Replace(".00", "").Trim();

                    if(_itemDesc != -1)
					{
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                        int _i = i + 1;

                        while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
						{
                            // Maker Ref: 150281 | Part No:
                            if (dt.Rows[_i][_itemDesc].ToString().StartsWith("Maker Ref:"))
                                iTemCode = Regex.Replace(dt.Rows[_i][_itemDesc].ToString(), @"\D", "");

                            iTemDESC = iTemDESC + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim();


                            if (_i < dt.Rows.Count)
                                _i += 1;
                            else
                                break;
						}
					}



                    if (!_itemCode.Equals(-1) && string.IsNullOrEmpty(iTemCode))
                    {
                        for (int r = _itemCode; r < _itemQt; r++)
                        {
                            //iTemCode = dt.Rows[i][_itemCode].ToString().Replace(",", "").Trim();
                            iTemCode = iTemCode + " " + dt.Rows[i][r].ToString().Trim();
                        }

                        int _i = i + 1;

                        while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
						{
                            for(int r = _itemCode; r < _itemQt; r++)
							{
                                iTemCode = iTemCode + Environment.NewLine + dt.Rows[_i][r].ToString().Trim();
							}


                            if (_i < dt.Rows.Count)
                                _i += 1;
                            else
                                break;

						}
                    }

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    if (_itemRmk != -1)
                        iTemRmk = dt.Rows[i][_itemRmk].ToString().Trim();

                    if (!string.IsNullOrEmpty(iTemRmk))
                        iTemDESC = iTemDESC + Environment.NewLine + iTemRmk;



                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = subjStr;

                    if (!string.IsNullOrEmpty(subjRmk))
                        iTemSUBJ = iTemSUBJ + Environment.NewLine + subjRmk;


                    if (!GetTo.IsInt(iTemCode))
                    {
                        if (!string.IsNullOrEmpty(iTemCode))
                            iTemSUBJ = iTemSUBJ + Environment.NewLine + iTemCode;
                    }


                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                    if (!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    if (GetTo.IsInt(iTemCode)) 
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

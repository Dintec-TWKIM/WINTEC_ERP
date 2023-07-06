using Dintec;
using Dintec.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parsing.Parser.Inquiry
{
	class sinokor_excel
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

        public sinokor_excel(string fileName)
        {
            reference = string.Empty;
            vessel = string.Empty; ;
            imoNumber = string.Empty;

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
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemCode = -1;


            string equipmentStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;


            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][0].ToString();


                if(firstColStr.Equals("Req. No."))
				{
                    reference = dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim();
				}
                else if (firstColStr.Equals("Vessel"))
				{
                    vessel = dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim();

                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Equals("Type"))
                            typeStr = dt.Rows[i][c + 1].ToString().Trim();
                    }
                }
                else if (firstColStr.Equals("Kind"))
				{
                    for(int c = 1; c < dt.Columns.Count; c++)
					{
                        if (dt.Rows[i][c].ToString().Equals("Equipment"))
                            equipmentStr = dt.Rows[i][c + 1].ToString().Trim();
					}
				}
                else if (firstColStr.Equals("Dept"))
				{
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Equals("Maker"))
                            makerStr = dt.Rows[i][c + 1].ToString().Trim();
                    }
                }
                else if (firstColStr.StartsWith("No."))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().ToLower().StartsWith("part no.")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().ToLower().Equals("description")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().ToLower().Equals("unit")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().ToLower().Equals("req. qty")) _itemQt = c;
                    }
                }
                else if (GetTo.IsInt(firstColStr))
                {
                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                    if (_itemUnit != -1)
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    if (_itemCode != -1)
                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                    if (_itemDesc != -1)
                    {
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                        int _i = i + 1;

                        while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
						{
                            for(int c = 1; c < dt.Columns.Count; c++)
							{
                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Replace("Vessel","").Replace(">","").Replace("[VSL]","").Replace("[REQ]","").Trim();
                            }

                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
						}
                    }


                    if (!string.IsNullOrEmpty(equipmentStr))
                        iTemSUBJ = equipmentStr.Trim();

                    if (!string.IsNullOrEmpty(makerStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr;

                    if (!string.IsNullOrEmpty(typeStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr;


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

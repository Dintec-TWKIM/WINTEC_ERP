using Dintec;
using Dintec.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parsing.Parser.Inquiry
{
	class HMM_excel
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

        public HMM_excel(string fileName)
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
            int _itemVessel = -1;

            int _itemDrwing = -1;
            int _itemPart = -1;
            string reference2 = string.Empty;




            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string firstColStr = dt.Rows[i][0].ToString().ToLower();

                if(string.IsNullOrEmpty(reference))
				{
                    for(int c = 1; c < dt.Columns.Count; c++)
					{
                        if (dt.Rows[i][c].ToString().ToLower().Contains("rfq no"))
                        {
                            for (int cc = c; cc < dt.Columns.Count; cc++)
                            {
                                if (string.IsNullOrEmpty(reference))
                                    reference = dt.Rows[i][cc].ToString().Replace("RFQ No.","").Trim();
                            }
                        }
					}
				}

                if (firstColStr.StartsWith("vessel name"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(vessel))
                            vessel = dt.Rows[i][c].ToString().Trim();
                        else
                            break;
                    }
                }
                else if (firstColStr.StartsWith("no."))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().ToLower().StartsWith("drawing")) _itemDrwing = c;
                        else if (dt.Rows[i][c].ToString().ToLower().StartsWith("part no")) _itemPart = c;
                        else if (dt.Rows[i][c].ToString().ToLower().Equals("qty")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().ToLower().Contains("mach. desc")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().ToLower().StartsWith("vessel name")) _itemVessel = c;
                    }
                }
                else if (GetTo.IsInt(firstColStr))
                {
                    if(string.IsNullOrEmpty(reference2))
					{
                        reference2 = dt.Rows[i + 2][_itemVessel].ToString().Trim();

                        reference = reference + "/" + reference2;
                    }

                    if (!_itemQt.Equals(-1))
                    {
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Trim();

                        if (string.IsNullOrEmpty(iTemUnit))
                            iTemUnit = dt.Rows[i][_itemQt + 2].ToString().Trim();
                    }

                    if(_itemDrwing != -1 && _itemPart != -1)
					{
                        iTemCode = dt.Rows[i][_itemDrwing].ToString() + "-" + dt.Rows[i][_itemPart].ToString();

                        iTemDESC = dt.Rows[i + 1][_itemDrwing].ToString() + ", " + dt.Rows[i + 1][_itemPart].ToString();
                        //iTemDESC = iTemDESC + Environment.NewLine + "MAKER: " + dt.Rows[i + 2][_itemPart].ToString();

                        iTemUniq = dt.Rows[i + 2][_itemDrwing].ToString().Trim();
                    }



                    if(_itemDesc != -1)
					{
                        iTemSUBJ = dt.Rows[i][_itemDesc].ToString().Trim() + Environment.NewLine +"TYPE: " + dt.Rows[i + 1][_itemDesc].ToString().Trim() + Environment.NewLine + "MAKER: " + dt.Rows[i + 2][_itemDesc].ToString().Trim();
                    }


                    iTemDESC = iTemDESC.Trim();
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

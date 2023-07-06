using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Procureship_excel
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

        public Procureship_excel(string fileName)
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
            string iTemRef = string.Empty;
            string iTemNote = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemDrw = -1;
            int _itemRef = -1;
            int _itemNOte = -1;

            string subjStr = string.Empty;
            string subjStr2 = string.Empty;
           

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                 string firstColStr = dt.Rows[i][1].ToString().ToUpper();


                if(string.IsNullOrEmpty(reference))
				{
                    for(int c = 0; c < dt.Columns.Count; c++)
					{
                        if(dt.Rows[i][c].ToString().StartsWith("Requisition No"))
						{
                            reference = dt.Rows[i][c + 1].ToString().Trim();
						}
					}
				}

                if (string.IsNullOrEmpty(vessel))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().StartsWith("Vessel"))
                        {
                            vessel = dt.Rows[i][c + 1].ToString().Trim();
                        }
                    }
                }

                if(string.IsNullOrEmpty(imoNumber))
				{
                    for(int c = 0; c < dt.Columns.Count; c++)
					{
                        if(dt.Rows[i][c].ToString().StartsWith("IMO"))
						{
                            imoNumber = dt.Rows[i][c + 1].ToString().Trim();
						}
					}
				}


                if (firstColStr.Equals("NO."))
				{
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        string LineStr = dt.Rows[i][c].ToString().ToLower().Trim();
                        if (LineStr.StartsWith("part no")) _itemCode = c;
                        else if (LineStr.StartsWith("description")) _itemDesc = c;
                        else if (LineStr.StartsWith("uom")) _itemUnit = c;
                        else if (LineStr.StartsWith("quantity requested")) _itemQt = c;
                        else if (LineStr.StartsWith("drawing no")) _itemDrw = c;
                        else if (LineStr.StartsWith("reference no")) _itemRef = c;
                        else if (LineStr.StartsWith("item office notes")) _itemNOte = c;
                    }
                }
                else if (firstColStr.StartsWith("EQUIPMENT:"))
				{
                    subjStr = firstColStr.Trim();
				}
                else if (firstColStr.StartsWith("ASSEMBLY:"))
				{
                    subjStr2 = firstColStr.Trim();
				}
                else if (GetTo.IsInt(firstColStr))
                {
                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                    if (_itemDesc != -1)
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    if (_itemRef != -1)
                        iTemRef = dt.Rows[i][_itemRef].ToString().Trim();

                    if (_itemDrw != -1)
                        iTemDrw = dt.Rows[i][_itemDrw].ToString().Trim();

                    if (_itemNOte != -1)
                        iTemNote = dt.Rows[i][_itemNOte].ToString().Trim();




                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = subjStr;

                    if (!string.IsNullOrEmpty(subjStr2))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr2.Trim();

                    if (!string.IsNullOrEmpty(iTemDrw))
                        iTemDESC = iTemDESC.Trim() + Environment.NewLine + "DWG NO. " + iTemDrw;

                    if (!string.IsNullOrEmpty(iTemRef))
                        iTemDESC = iTemDESC.Trim() + Environment.NewLine + "REF NO. " + iTemRef.Trim();

                    if (!string.IsNullOrEmpty(iTemNote))
                        iTemDESC = iTemDESC.Trim() + Environment.NewLine + iTemNote.Trim();



                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                    if (!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode.ToUpper().Replace("IMPA","").Trim();
                    //dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                    if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);

                    iTemDESC = string.Empty;
                    iTemCode = string.Empty;
                    iTemQt = string.Empty;
                    iTemUnit = string.Empty;
                    iTemSUBJ = string.Empty;
                    iTemDrw = string.Empty;
                    iTemNote = string.Empty;
                }
            }
        }
    }
}

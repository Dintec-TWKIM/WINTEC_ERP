using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class EverGreenMarineTAI
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

        public EverGreenMarineTAI(string fileName)
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
            string iTemUniq = string.Empty;
            string iTemMaker = string.Empty;
            string iTemType = string.Empty;
            string iTemDIMENSION = string.Empty;
            string iTemDwg = string.Empty;

            string iTemNote = string.Empty;


            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemUniq = -1;
            int _itemMaker = -1;
            int _itemType = -1;
            int _itemSerial = -1;
            int _itemMachine = -1;
            int _itemDIMENSION = -1;
            int _itemDwg = -1;

            int _itemNote = -1;

            string subjStr = string.Empty;


            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            // ********** 아이템 추가 모드

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][0].ToString();
                string secondColStr = dt.Rows[i][1].ToString().ToLower();


                if(string.IsNullOrEmpty(reference))
				{
                    for(int c = 6; c < dt.Columns.Count; c++)
					{
                        if(dt.Rows[i][c].ToString().ToLower().Contains("inquiry no."))
						{
                            for(int c2 = c+1; c2 < dt.Columns.Count; c2++)
							{
                                if(string.IsNullOrEmpty(reference))
								{
                                    reference = dt.Rows[i][c2].ToString().Trim();
								}
							}
						}
					}
				}


                if (firstColStr.StartsWith("NO."))
                {
                    for (int c = 2; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(reference))
                            reference = dt.Rows[i][c].ToString().Trim();
                    }
                }
                else if (firstColStr.StartsWith("VESSEL NAME"))
                {
                    for (int c = 2; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(vessel))
                        {
                            vessel = dt.Rows[i][c].ToString().Trim();
                            int idx_s = vessel.IndexOf("(");

                            if (idx_s != -1)
                                vessel = vessel.Substring(0, idx_s);
                        }
                    }
                }
                else if (secondColStr.Equals("seq."))
                {
                    string colName = string.Empty;
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        colName = dt.Rows[i][c].ToString().ToLower().Trim();
                        if (colName.StartsWith("stock") && colName.Contains("no.")) _itemUniq = c;
                        else if (colName.Contains("parts") && colName.Contains("name")) _itemDesc = c;
                        else if (colName.Contains("parts") && colName.Contains("maker")) _itemMaker = c;
                        //else if (colName.Equals("TYPE")) _itemType = c;
                        else if (colName.Contains("maker") && colName.Contains("ref.")) _itemCode = c;
                        //else if (colName.Equals("SERIAL NO.")) _itemSerial = c;
                        else if (colName.Equals("meterial")) _itemMachine = c;
                        else if (colName.StartsWith("qty")) _itemQt = c;
                        else if (colName.Equals("unit")) _itemUnit = c;
                        else if (colName.StartsWith("dimension")) _itemDIMENSION = c;
                        //else if (colName.StartsWith("DRAWING NO")) _itemDwg = c;
                        else if (colName.StartsWith("purchase notes")) _itemNote = c;
                    }
                }
                else if (GetTo.IsInt(secondColStr))
                {
                    if(dt.Rows[i-1][1].ToString().ToLower().StartsWith("component"))
					{
                        subjStr = dt.Rows[i - 1][1].ToString().Trim();
					}

                    //iTemNo = firstColStr;
                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                    if(!_itemDesc.Equals(-1))
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if(!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                    
                    if(!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    if (!_itemUniq.Equals(-1))
                        iTemUniq = dt.Rows[i][_itemUniq].ToString().Trim();

                    if (!_itemMaker.Equals(-1))
                    {
                        iTemMaker = dt.Rows[i][_itemMaker].ToString().Trim();

                        iTemDESC = iTemDESC + Environment.NewLine + dt.Rows[i][_itemMaker].ToString();
                    }

                    if (!_itemType.Equals(-1))
                        iTemType = dt.Rows[i][_itemType].ToString().Trim();

                    if (!_itemMachine.Equals(-1))
					{
                        string itemMetStr = dt.Rows[i][_itemMachine].ToString().Trim();

                        if (!string.IsNullOrEmpty(itemMetStr))
                            iTemDESC = iTemDESC + ", " + itemMetStr;
                    }
                        

                    if (_itemDwg != -1)
                        iTemDwg = dt.Rows[i][_itemDwg].ToString().Trim();

                    if(_itemDIMENSION != -1)
                    {
                        iTemDIMENSION = dt.Rows[i][_itemDIMENSION].ToString().Trim();

                        if(!string.IsNullOrEmpty(iTemDIMENSION))
                            iTemDESC = iTemDESC + ", " + iTemDIMENSION.Trim();
                    }

                    if (_itemNote != -1)
                    {
                        string itemNoteStr = dt.Rows[i][_itemNote].ToString().Trim();

                        if (!string.IsNullOrEmpty(itemNoteStr))
                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + itemNoteStr;
                    }



                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = subjStr.Trim();

                    if(!string.IsNullOrEmpty(iTemType))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + iTemType.Trim();

                    if (!string.IsNullOrEmpty(iTemMaker))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + iTemMaker.Trim();


                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                    if (!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;
                    if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
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

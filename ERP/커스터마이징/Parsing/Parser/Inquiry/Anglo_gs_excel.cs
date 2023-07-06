using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
	class Anglo_gs_excel
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

        public Anglo_gs_excel(string fileName)
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
            int _itemVessel = -1;

            string subjStr = string.Empty;
            string itemDwg = string.Empty;

            string makerStr = string.Empty;
            string modelStr = string.Empty;
            string typeStr = string.Empty;
            string SerialStr = string.Empty;
            string particStr = string.Empty;
            string cataloStr = string.Empty;
            string departStr = string.Empty;


            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string firstColStr = dt.Rows[i][0].ToString().ToLower();


                if (firstColStr.ToUpper().StartsWith("REQUEST FOR QUOTATION"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(reference))
                            reference = dt.Rows[i][c].ToString().Trim();
                    }
                }


                if(firstColStr.StartsWith("vessel name"))
                {
                    for(int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(vessel))
                            vessel = dt.Rows[i][c].ToString().Trim();
                    }
                }
                else if (firstColStr.StartsWith("rfq no"))
                {
                    for(int c =1; c< dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(reference))
                        {
                            reference = dt.Rows[i][c].ToString().Trim();

                            if (reference.StartsWith("QT"))
                            {
                                reference = reference.Substring(2, reference.Length - 5).Trim();
                            }
                        }
                    }
                }
                else if (firstColStr.StartsWith("department"))
                {
                    int _i = i + 1;

                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        string textStr = dt.Rows[i][c].ToString().ToLower().Trim();
                        if (textStr.StartsWith("model"))
                            modelStr = dt.Rows[i][c + 1].ToString().Trim();
                        else if (textStr.StartsWith("particula"))
                            particStr = dt.Rows[i][c + 1].ToString().Trim();
                        else if (textStr.StartsWith("maker"))
                            makerStr = dt.Rows[i][c + 1].ToString().Trim();
                        else if (textStr.StartsWith("catalogue"))
                            cataloStr = dt.Rows[i][c + 1].ToString().Trim();
                    }

                    

                    while(!dt.Rows[_i][0].ToString().ToLower().StartsWith("s. no"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            string textStr = dt.Rows[_i][c].ToString().ToLower().Trim();
                            if (textStr.StartsWith("model"))
                                modelStr = dt.Rows[_i][c + 1].ToString().Trim();
                            else if (textStr.StartsWith("particula"))
                                particStr = dt.Rows[_i][c + 1].ToString().Trim();
                            else if (textStr.StartsWith("maker"))
                                makerStr = dt.Rows[_i][c + 1].ToString().Trim();
                            else if (textStr.StartsWith("catalogue"))
                                cataloStr = dt.Rows[_i][c + 2].ToString().Trim();
                        }

                        _i += 1;

                        if (_i >= dt.Rows.Count)
                            break;
                    }
                }
                else if (firstColStr.StartsWith("s. no"))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().ToLower().StartsWith("part")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().ToLower().Equals("unit")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().ToLower().Equals("rsqt qty")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().ToLower().Contains("items")) _itemDesc = c;
                    }
                }
                else if (GetTo.IsInt(firstColStr))
                {
                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Replace(".00", "").Trim();

                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Replace(",", "").Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    if (!_itemDesc.Equals(-1))
                    {
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                        int _i = i + 1;

                        while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                        {
                            iTemDESC = iTemDESC + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim();

                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(cataloStr))
                        iTemSUBJ = cataloStr;

                    if (!string.IsNullOrEmpty(modelStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr;

                    if (!string.IsNullOrEmpty(particStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "PARTICULARS: " + particStr;

                    if (!string.IsNullOrEmpty(makerStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr;

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
                    iTemRmk = string.Empty;
                }
            }
        }
    }
}

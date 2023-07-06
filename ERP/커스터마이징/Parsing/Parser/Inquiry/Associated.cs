using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class Associated
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

        public Associated(string fileName)
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
            string iTemDwg = string.Empty;
            string iTemPos = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemDwg = -1;
            int _itemPosition = -1;

            string subjStr = string.Empty;
            string makerStr = string.Empty;
            string codeStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;


            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            // ********** 아이템 추가 모드

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][1].ToString();

                if (firstColStr.StartsWith("VESSEL'S NAME"))
                {
                    for (int c = 2; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(vessel))
                            vessel = dt.Rows[i][c].ToString().Trim();
                        else
                            break;
                    }
                }
                else if (firstColStr.StartsWith("INQUIRY NO"))
                {
                    for (int c = 2; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(reference))
                            reference = dt.Rows[i][c].ToString().Trim();
                        else
                            break;
                    }
                }
                else if (firstColStr.StartsWith("MAKER'S NAME"))
                {
                    makerStr = dt.Rows[i + 1][1].ToString().Trim();

                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().StartsWith("Equipment No"))
                        {
                            codeStr = dt.Rows[i][c].ToString().Trim() + dt.Rows[i][c + 1].ToString().Trim() + dt.Rows[i][c + 2].ToString().Trim();
                            subjStr = dt.Rows[i + 1][c].ToString().Trim()  + dt.Rows[i+1][c + 1].ToString().Trim() + dt.Rows[i+1][c + 2].ToString().Trim();
                            typeStr = dt.Rows[i + 2][c].ToString().Trim() + dt.Rows[i + 2][c + 1].ToString().Trim() + dt.Rows[i + 2][c + 2].ToString().Trim();
                            serialStr = dt.Rows[i + 3][c].ToString().Trim() + dt.Rows[i + 3][c + 1].ToString().Trim() + dt.Rows[i + 3][c + 2].ToString().Trim();
                        }
                    }
                }
                else if (firstColStr.StartsWith("ITEM"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Equals("PART NO.")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().Equals("ITEM DESCRIPTION")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Equals("QTY")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Equals("UNIT")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().StartsWith("DRAWING")) _itemDwg = c;
                        else if (dt.Rows[i][c].ToString().StartsWith("POSITION")) _itemPosition = c;
                    }
                }
                else if (GetTo.IsInt(firstColStr))
                {
                    iTemNo = firstColStr;

                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                    if (!_itemQt.Equals(-1))
                    {
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                    }

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    

                    if(_itemDwg != -1)
                        iTemDwg = dt.Rows[i][_itemDwg].ToString().Trim();

                    if(_itemPosition != -1)
                        iTemPos = dt.Rows[i][_itemPosition].ToString().Trim();



                    if (!_itemDesc.Equals(-1))
                    {
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                        int _i = i + 1;

                        while (string.IsNullOrEmpty(dt.Rows[_i][1].ToString()))
                        {
                            iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][_itemDesc].ToString().Trim();
                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(subjStr.Trim()))
                        iTemSUBJ = subjStr.Trim();

                    if (!string.IsNullOrEmpty(codeStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + codeStr;

                    if (!string.IsNullOrEmpty(typeStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + typeStr;

                    if (!string.IsNullOrEmpty(serialStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + serialStr;

                    if (!string.IsNullOrEmpty(makerStr))
                        iTemSUBJ = iTemSUBJ + Environment.NewLine + "MAKER: " + makerStr.Trim();


                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
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
                    iTemSUBJ = string.Empty;
                }
            }


        }
    }
}

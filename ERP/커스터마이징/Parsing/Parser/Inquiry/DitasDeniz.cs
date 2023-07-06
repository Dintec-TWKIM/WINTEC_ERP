﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class DitasDeniz
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

        public DitasDeniz(string fileName)
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
            string iTemRmk = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemRmk = -1;

            string subjStr = string.Empty;
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
                        if (dt.Rows[i][c].ToString().StartsWith("Bölüm"))
                            vessel = dt.Rows[i][c + 1].ToString().Trim();
                    }
                }

                if (string.IsNullOrEmpty(reference))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().StartsWith("İstek No"))
                            reference = dt.Rows[i][c + 1].ToString().Trim();
                    }
                }


                if (firstColStr.StartsWith("ADI"))
                {
                    for(int c = 0; c < dt.Columns.Count; c++)
                    {
                        subjStr = subjStr + " " + dt.Rows[i][c].ToString().Trim(); 
                    }

                    subjStr = subjStr.Trim() + Environment.NewLine;

                    int _i = i + 1;

                    while (!dt.Rows[_i][0].ToString().Contains("KALEM NO"))
                    {
                        for (int _c = 0; _c < dt.Columns.Count; _c++)
                        {
                            subjStr = subjStr + " " + dt.Rows[_i][_c].ToString().Trim();
                        }

                            _i += 1;

                        if (_i >= dt.Rows.Count || dt.Rows[_i][0].ToString().StartsWith("KALEM NO"))
                            break;
                    }
                }
                else if (firstColStr.StartsWith("(ITEM NO)"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("(IDENTIFICATION NO)")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().Equals("(ITEM)")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Contains("(REQUESTED AMOUNT)")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Equals("(REMARK)")) _itemRmk = c;
                    }
                }
                else if (GetTo.IsInt(firstColStr))
                {
                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                    if (!_itemDesc.Equals(-1))
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (_itemRmk != -1)
                        iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[i][_itemRmk].ToString().Trim();


                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = subjStr.Trim();


                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                    if (!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                    if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = "PCS";

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

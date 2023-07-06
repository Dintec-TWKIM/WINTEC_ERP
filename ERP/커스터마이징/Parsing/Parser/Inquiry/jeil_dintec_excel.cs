﻿using Dintec;
using Dintec.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parsing.Parser.Inquiry
{
	class jeil_dintec_excel
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

        public jeil_dintec_excel(string fileName)
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


                if(string.IsNullOrEmpty(reference))
				{
                    for(int c = 0; c < dt.Columns.Count; c++)
					{
                        if(dt.Rows[i][c].ToString().Contains("OUR REF NO"))
						{
                            reference = dt.Rows[i][c + 1].ToString() + dt.Rows[i][c + 2].ToString().Trim();
                            reference = reference.Replace(":", "").Trim();
						}
					}
				}

             
                if (firstColStr.StartsWith("VESSEL"))
				{
                    vessel = firstColStr.Replace("VESSEL :", "").Trim();

                    string[] vesselSpl = vessel.Split('/');

                    if(vesselSpl.Length == 2)
					{
                        vessel = vesselSpl[0].ToString().Trim();
                        imoNumber = vesselSpl[1].ToString().Trim();
					 }
                }
                if (firstColStr.StartsWith("NO"))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().ToLower().StartsWith("code")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().ToLower().Equals("description")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().ToLower().Equals("unit")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().ToLower().Equals("qty")) _itemQt = c;
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
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (string.IsNullOrEmpty(dt.Rows[i - 1][0].ToString()))
                        iTemSUBJ = dt.Rows[i - 1][_itemDesc].ToString().Trim();


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

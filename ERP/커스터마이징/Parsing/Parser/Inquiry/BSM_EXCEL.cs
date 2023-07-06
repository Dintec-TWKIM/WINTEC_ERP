using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class BSM_EXCEL
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

        public BSM_EXCEL(string fileName)
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

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemDwg = -1;

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
                string firstColStr = dt.Rows[i][1].ToString();


                if (firstColStr.StartsWith("IMO No"))
                {
                    imoNumber = dt.Rows[i][2].ToString().Trim();

                    string referStr = dt.Rows[0][3].ToString();

                    int idx_s = referStr.IndexOf("R-");
                    int idx_e = referStr.IndexOf(" / ");

                    reference = referStr.Substring(idx_s, idx_e - idx_s);
                    subjStr = referStr.Substring(idx_e, referStr.Length - idx_e).Replace("/","").Trim();
                }
                else if (firstColStr.StartsWith("S.No."))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("Part Number") || dt.Rows[i][c].ToString().Contains("Code")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().Contains("Item Description") || dt.Rows[i][c].ToString().Contains("Name")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Contains("Requested Qty") || dt.Rows[i][c].ToString().Contains("Required Qty")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Equals("UOM")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().Equals("Drawing No")) _itemDwg = c;
                    }
                }
                else if (firstColStr.ToUpper().Contains("MAKER"))
                {
                    makerStr = firstColStr.Replace("Maker","").Replace(":","").Trim();
                }
                else if (GetTo.IsInt(firstColStr))
                {
                    if(string.IsNullOrEmpty(makerStr))
                        makerStr = dt.Rows[1][5].ToString().Trim();
                    
                    modelStr = dt.Rows[2][5].ToString().Trim();
                    typeStr = dt.Rows[3][5].ToString().Trim();
                    SerialStr = dt.Rows[2][3].ToString().Trim();

                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    if (!_itemDesc.Equals(-1))
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (!_itemDwg.Equals(-1))
                        itemDwg = dt.Rows[i][_itemDwg].ToString().Trim();

                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = subjStr.Trim();

                    if (!string.IsNullOrEmpty(makerStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                    if (!string.IsNullOrEmpty(modelStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr.Trim();

                    if (!string.IsNullOrEmpty(typeStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                    if (!string.IsNullOrEmpty(typeStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                    if (!string.IsNullOrEmpty(SerialStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "SERIAL NO: " + SerialStr.Trim();

                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                    if (!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                    if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);

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

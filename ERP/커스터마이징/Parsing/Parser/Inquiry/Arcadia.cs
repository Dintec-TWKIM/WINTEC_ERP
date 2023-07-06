using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class Arcadia
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

        public Arcadia(string fileName)
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
            string iTemType = string.Empty;
            string iTemModel = string.Empty;
            string iTemPositin = string.Empty;
            string iTemDrw = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemDrw = -1;
            int _itemModel = -1;
            int _itemPosition = -1;

            string subjStr = string.Empty;
           

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][0].ToString();

                if (string.IsNullOrEmpty(reference))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().StartsWith("RFQ:"))
                            reference = dt.Rows[i][c + 1].ToString().Trim();
                    }
                }


                if (string.IsNullOrEmpty(vessel))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().StartsWith("Vessel:"))
                            vessel = dt.Rows[i][c + 1].ToString().Replace("M.T.","").Trim();
                    }
                }


                if (firstColStr.StartsWith("Component Details"))
                {
                    subjStr = firstColStr.Replace("Component Details", "").Replace(":", "").Trim();
                }
                else if (firstColStr.Equals("Requisition Number"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Equals("Part Description")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Equals("Quantity Required")) _itemQt = c;
                        //else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().StartsWith("Part Number")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().StartsWith("Drawing No")) _itemDrw = c;
                        else if (dt.Rows[i][c].ToString().StartsWith("Model No")) _itemModel = c;
                        else if (dt.Rows[i][c].ToString().StartsWith("Position No")) _itemPosition = c;
                    }
                }
                else if (firstColStr.StartsWith("REQ-"))
                {
                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Replace("-", "").Trim();

                    if (!_itemDesc.Equals(-1))
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (!_itemDrw.Equals(-1))
                        iTemDrw = dt.Rows[i][_itemDrw].ToString().Trim();

                    if (!_itemModel.Equals(-1))
                        iTemModel = dt.Rows[i][_itemModel].ToString().Trim();

                    if (!_itemPosition.Equals(-1))
                        iTemPositin = dt.Rows[i][_itemPosition].ToString().Trim();


                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = subjStr.Trim();


                    if (!string.IsNullOrEmpty(iTemModel))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + iTemModel.Trim();

                    if (!string.IsNullOrEmpty(iTemDrw))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG NO: " + iTemDrw.Trim();


                    dtItem.Rows.Add();
                    //dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
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
                    iTemSUBJ = string.Empty;
                }
            }
        }
    }
}

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Gimsco
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

        public Gimsco(string fileName)
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

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemRemark = -1;

            int idx_s = 0;
            int idx_e = 0;

            string subjStr = string.Empty;

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            // ********** 아이템 추가 모드

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][0].ToString();


                if (firstColStr.StartsWith("REQUEST FOR QUOTATION"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(reference))
                            reference = dt.Rows[i][c].ToString().Trim();
                    }
                    reference = reference.Trim();
                }
                else if (firstColStr.StartsWith("Equipment"))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                            subjStr = subjStr + Environment.NewLine + dt.Rows[i][c].ToString().Trim();
                    }

                    int _i = i + 1;

                    while (!dt.Rows[_i][0].ToString().Equals("No."))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[_i][c].ToString()))
                                subjStr = subjStr + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
                        }
                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                    }

                    subjStr = subjStr.Replace("EQUIPMENT:", "").Trim();

                }
                else if (firstColStr.Equals("No."))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().StartsWith("Part No")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Equals("Remark")) _itemRemark = c;
                        else if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                    }
                }
                else if (GetTo.IsInt(firstColStr))
                {
                    iTemNo = firstColStr;

                    if (!string.IsNullOrEmpty(subjStr))
                    {
                        idx_s = subjStr.IndexOf("Equipment", 0);
                        idx_e = subjStr.IndexOf("Serial No", 0);
                        if (!idx_s.Equals(-1) && !idx_e.Equals(-1))
                        {
                            iTemSUBJ = subjStr.Substring(idx_s, idx_e - idx_s).Replace("Part Description", "").Replace(":", "").Trim();
                        }

                        idx_s = subjStr.IndexOf("Manufacturer");
                        idx_e = subjStr.IndexOf("Project");

                        if (!idx_e.Equals(-1) && !idx_e.Equals(-1))
                        {
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr.Substring(idx_s, idx_e - idx_s).Trim();
                        }
                    }

                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                    if (!_itemDesc.Equals(-1))
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    //if (!string.IsNullOrEmpty(subjStr.Trim()))
                    //    iTemSUBJ = subjStr.Trim();


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

                    subjStr = string.Empty;
                }

            }
        }
    }
}

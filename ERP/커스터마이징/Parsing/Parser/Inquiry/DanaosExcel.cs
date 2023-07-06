using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class DanaosExcel
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

        public DanaosExcel(string fileName)
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

            int _colCount = 0;

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (_colCount == 0)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().StartsWith("VESSEL"))
                        {
                            _colCount = c;
                            vessel = dt.Rows[i][_colCount + 1].ToString().Trim();
                            vessel = vessel.Replace("M.V.", "").Replace("VESSEL", "").Replace(":", "").Replace("MT","").Trim();
                        }
                    }
                }
                else
                {


                    string firstColStr = dt.Rows[i][_colCount].ToString();

                    if (firstColStr.StartsWith("QUOTATION"))
                    {
                        reference = dt.Rows[i][_colCount + 1].ToString().Trim();
                    }
                    else if (firstColStr.ToUpper().StartsWith("ITEM CODE"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("ITEM CODE")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().ToUpper().Contains("ITEM DESCRIPTION")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().ToUpper().Contains("QUANTITY"))
                            {
                                int _i = i + 1;

                                while(string.IsNullOrEmpty(dt.Rows[_i][c].ToString()))
                                {
                                    subjStr = subjStr.Trim() + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim();

                                    _i +=1;

                                    if(dt.Rows.Count <= _i)
                                        break;
                                }

                                _itemQt = c;
                                _colCount = c;
                                _itemQt = c;
                            }

                            else if (dt.Rows[i][c].ToString().ToUpper().Equals("ITEM UNIT")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().ToUpper().Equals("ITEM COMMENTS")) _itemDwg = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (!_itemUnit.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (!_itemDesc.Equals(-1))
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

						if (_itemQt != -1)
						{
							iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

							if (string.IsNullOrEmpty(dt.Rows[i - 2][_itemQt].ToString()))
							{
								subjStr = dt.Rows[i - 2][_itemDesc].ToString().Trim();
								subjStr = subjStr + Environment.NewLine + dt.Rows[i - 1][_itemDesc].ToString().Trim();
							}
							else if (string.IsNullOrEmpty(dt.Rows[i - 1][_itemQt].ToString()))
							{
								subjStr = dt.Rows[i - 1][_itemDesc].ToString().Trim();
							}
						}


						if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();
                        



                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
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
                        iTemDrw = string.Empty;
                        iTemSUBJ = string.Empty;
                    }
                }
            }
        }
    }
}

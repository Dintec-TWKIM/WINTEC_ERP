using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Southern2
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

        public Southern2(string fileName)
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

            string subjStr = string.Empty;
            string makerStr = string.Empty;

            int subjInt = -1;

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            // ********** 아이템 추가 모드

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][0].ToString();

                if (firstColStr.Equals("VESSEL:"))
                {
                    vessel = dt.Rows[i][1].ToString().Trim();
                }
                else if (firstColStr.Equals("ENQ. NO."))
                {
                    reference = dt.Rows[i][1].ToString().Trim();
                }
                else if (firstColStr.Equals("MAKER:"))
                {
                    makerStr = dt.Rows[i][1].ToString().Trim();
                }
                else if (firstColStr.Equals("ITEM NO."))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Equals("PART NO.")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().Equals("DESCRIPTION")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Equals("QTY.")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Equals("UNIT")) _itemUnit = c;
                    }

                    subjInt = i;
                }
                else if (subjInt + 1 == i)
                {
                    subjStr = dt.Rows[i][_itemDesc].ToString().Trim();

                    int _i = i + 1;
                    while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                    {
                        subjStr = subjStr + " " + dt.Rows[_i][_itemDesc].ToString().Trim();

                        _i += 1;
                        if (_i >= dt.Rows.Count)
                            break;
                    }
                }
                else if (!_itemQt.Equals(-1))       // 수량 값으로 갯수 세아려야함
                {
                    if (GetTo.IsInt(dt.Rows[i][_itemQt].ToString()))
                    {
                        iTemNo = firstColStr;

                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (!_itemUnit.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();


                        if (!string.IsNullOrEmpty(subjStr.Trim()))
                            iTemSUBJ = subjStr.Trim();



                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][_itemDesc].ToString().Trim();
                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr;

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

                        subjStr = string.Empty;
                    }
                }

            }
        }
    }
}

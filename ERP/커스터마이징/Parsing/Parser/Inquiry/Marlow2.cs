using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Marlow2
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

        public Marlow2(string fileName)
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
            string iTemMaker = string.Empty;
            string iTemType = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;


          

            string subjStr = string.Empty;


            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            // ********** 아이템 추가 모드

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][0].ToString();

                if (firstColStr.StartsWith("Our Ref"))
                {
                    for (int c = 2; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(reference))
                            reference = dt.Rows[i][c].ToString().Replace("!!","").Replace("XX","").Trim();
                    }
                }
                else if (firstColStr.StartsWith("Vessel details"))
                {
                    if (string.IsNullOrEmpty(vessel))
                    {
                        vessel = dt.Rows[i+1][0].ToString().Trim();

                        int idx_s = 0;
                        int idx_e = vessel.IndexOf("IMO", idx_s);
                        vessel = vessel.Substring(idx_s, idx_e - idx_s).Trim();

                        vessel = vessel.Replace("m/v", "").Replace("M/V", "").Replace("M/T", "").Replace("m/t", "").Trim();
                    }
                }
                else if (firstColStr.Equals("Details of Request Items"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i + 1][c].ToString().StartsWith("Description")) _itemDesc = c;
                        else if (dt.Rows[i + 1][c].ToString().Equals("Item Reference")) _itemCode = c;
                        else if (dt.Rows[i + 1][c].ToString().Equals("Requested")) _itemQt = c;
                        else if (dt.Rows[i + 2][c].ToString().Equals("Unit") && _itemUnit.Equals(-1)) _itemUnit = c;
                    }
                }
                else if (GetTo.IsInt(firstColStr))
                {
                   
                    iTemNo = firstColStr;
                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                    if (!_itemDesc.Equals(-1))
                    {
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                        int _i = i + 1;
                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                        {
                            if (dt.Rows[_i][_itemDesc].ToString().Contains("Maker Reference"))
                            {
                                iTemMaker = string.Empty;
                                for (int c = _itemDesc; c < dt.Columns.Count; c++)
                                {
                                    iTemMaker = iTemMaker.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                }
                            }
                            else if (dt.Rows[_i][_itemDesc].ToString().Contains("Supplier Item"))
                            {
                                iTemType = string.Empty;
                                for (int c = _itemDesc; c < dt.Columns.Count; c++)
                                {
                                    iTemType = iTemType.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                }
                            }
                                _i += 1;

                            if(dt.Rows[_i][0].ToString().Contains("Your Additional Remarks")) break;
                        }
                    }

                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();


                    iTemMaker = iTemMaker.Replace("Maker Reference","").Replace(":","").Trim();
                    iTemType = iTemType.Replace("Supplier Item Description","").Replace(":","").Trim();


                    if (!string.IsNullOrEmpty(iTemMaker))
                        iTemSUBJ = iTemSUBJ.Trim() + " " + iTemMaker.Trim();

                    if (!string.IsNullOrEmpty(iTemType))
                        iTemSUBJ = iTemSUBJ.Trim() + " " + iTemDESC.Trim();

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

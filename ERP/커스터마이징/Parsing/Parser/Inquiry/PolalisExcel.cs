using Dintec;
using Dintec.Parser;

using System;
using System.Data;
using System.Text.RegularExpressions;

namespace Parsing
{
    class PolalisExcel
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

        public PolalisExcel(string fileName)
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
            string iTemCompo = string.Empty;
            string iTemRmk = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemMaker = -1;
            int _itemType = -1;
            int _itemEquip = -1;
            int _itemRemark = -1;
            int _itemCompo = -1;

            string subjStr2 = string.Empty;
            string subjStr = string.Empty;


            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            // ********** 아이템 추가 모드

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][0].ToString();


                if (firstColStr.StartsWith("RFQ No"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(reference))
                            reference = dt.Rows[i][c].ToString().Trim();
                    }
                }
                else if (firstColStr.StartsWith("Buyer"))
				{
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(contact))
                        {
                            contact = dt.Rows[i][c].ToString().Trim();

                            contact = Regex.Replace(contact, @"[^ㄱ-ㅎ가-힣]","");
                        }
                    }
                }
                else if (firstColStr.StartsWith("Vessel"))
                {
                    for (int c = 2; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(vessel))
                            vessel = dt.Rows[i][c].ToString().Trim();
                    }
                }
                else if (firstColStr.StartsWith("Subject"))
                {
                    subjStr2 = string.Empty;
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        subjStr2 = subjStr2.Trim() + dt.Rows[i][c].ToString().Trim();
                    }
                }
                else if (firstColStr.Equals("Line"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().StartsWith("Maker")) _itemMaker = c;
                        else if (dt.Rows[i][c].ToString().Equals("Type")) _itemType = c;
                        else if (dt.Rows[i][c].ToString().Equals("Equipment")) _itemEquip = c;
                        else if (dt.Rows[i][c].ToString().Equals("Component")) _itemCompo = c;
                        else if (dt.Rows[i][c].ToString().StartsWith("Part No")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Equals("Remark")) _itemRemark = c;
                        else if (dt.Rows[i][c].ToString().StartsWith("Qty")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().StartsWith("Unit")) _itemUnit = c;
                    }
                }
                else if (GetTo.IsInt(firstColStr))
                {
                    iTemNo = firstColStr;
                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Replace("IMPA", "").Replace("ISSA", "").Replace("-","").Trim();

                    if (!_itemDesc.Equals(-1))
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Replace(",","").Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    if (!_itemEquip.Equals(-1))
                        subjStr = dt.Rows[i][_itemEquip].ToString().Trim();

                    if(!_itemCompo.Equals(-1))
                        

                    if (!_itemMaker.Equals(-1))
                        iTemMaker = dt.Rows[i][_itemMaker].ToString().Replace("NONE", "").Trim();

                    if (!_itemType.Equals(-1))
                        iTemType = dt.Rows[i][_itemType].ToString().Replace("NONE","").Trim();

                    if (_itemRemark != -1)
                        iTemRmk = dt.Rows[i][_itemRemark].ToString().Trim();


                    if (!string.IsNullOrEmpty(subjStr2))
                        iTemSUBJ = subjStr2.Trim();

                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr.Trim();

                    if (!string.IsNullOrEmpty(iTemType))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + iTemType.Trim();

                    if (!string.IsNullOrEmpty(iTemMaker))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + iTemMaker.Trim();


                    if (!string.IsNullOrEmpty(iTemRmk))
                        iTemDESC = iTemDESC.Trim() + Environment.NewLine + iTemRmk;



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
                }

            }
        }
    }
}

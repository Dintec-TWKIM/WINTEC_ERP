using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class HongkongMing
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

        public HongkongMing(string fileName)
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
            string subjStr1 = string.Empty;
            string subjStr2 = string.Empty;

            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string makerStr = string.Empty;

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][0].ToString();

                if (firstColStr.StartsWith("船舶/SHIP NAME"))
                {
                    vessel = dt.Rows[i][2].ToString().Trim();
                }

                if (string.IsNullOrEmpty(reference))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().StartsWith("申请单号/REQUEST NO"))
                            reference = dt.Rows[i][c].ToString().Replace("申请单号/REQUEST NO", "").Replace("：", "").Trim() + dt.Rows[i][c + 1].ToString().Trim(); ;
                    }
                }


                if (firstColStr.StartsWith("设备/EQUIPMENT："))
                {
                    subjStr = dt.Rows[i][2].ToString().Trim();
                }
                else if (firstColStr.StartsWith("询价说明/Inquiry Desc"))
                {
                    subjStr1 = dt.Rows[i][2].ToString().Trim();
                }
                else if (firstColStr.StartsWith("报价说明/Quatation Desc"))
                {
                    subjStr2 = dt.Rows[i][2].ToString().Trim();
                }
                else if (firstColStr.StartsWith("型号/TYPE"))
                {
                    typeStr = dt.Rows[i][2].ToString().Trim();
                }
                else if (firstColStr.StartsWith("出厂号/SERIAL"))
                {
                    serialStr = dt.Rows[i][2].ToString().Trim();
                }
                else if (firstColStr.StartsWith("生产厂家/MANUAL"))
                {
                    makerStr = dt.Rows[i][2].ToString().Trim();
                }
                else if (firstColStr.StartsWith("NO."))
                {
                    iTemSUBJ = string.Empty;

                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("DESCRIPTION")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Contains("Inquiry Qty")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Contains("UNIT")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().Contains("PART NO.")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().Contains("Dwg. No.")) _itemDwg = c;
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

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    if (!_itemDwg.Equals(-1))
                        iTemDrw = dt.Rows[i][_itemDwg].ToString().Trim();



                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = subjStr.Trim();

                    if (!string.IsNullOrEmpty(typeStr.Trim()))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                    if (!string.IsNullOrEmpty(serialStr.Trim()))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();

                    if (!string.IsNullOrEmpty(makerStr.Trim()))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                    if (!string.IsNullOrEmpty(subjStr1))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr1.Trim();

                    if (!string.IsNullOrEmpty(subjStr2))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr2.Trim();

                    if (!string.IsNullOrEmpty(iTemDrw))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG NO: " + iTemDrw.Trim();


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
                }
            }
        }
    }
}

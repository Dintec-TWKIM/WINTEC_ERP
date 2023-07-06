using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Marinsa
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

        public Marinsa(string fileName)
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
            int _itemDrw = -1;
            int _itemUnit = -1;

            string subjStr = string.Empty;
            string modelStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string subjStr2 = string.Empty;

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][0].ToString();

                if (string.IsNullOrEmpty(reference))
                    reference = DateTime.Now.ToString("yyyy.MM.dd");

                if (string.IsNullOrEmpty(vessel))
                    vessel = dt.Rows[0][2].ToString().Replace("M/V", "").Trim();


                if (firstColStr.StartsWith("Adı - Name"))
                {
                    subjStr = string.Empty;
                    for (int c = 1; c < dt.Columns.Count; c++)
                        subjStr = subjStr.Trim() + dt.Rows[i][c].ToString().Trim();
                }
                else if (firstColStr.StartsWith("Modeli - Model"))
                {
                    modelStr = string.Empty;
                    for (int c = 1; c < dt.Columns.Count; c++)
                        modelStr = modelStr.Trim() + dt.Rows[i][c].ToString().Trim();
                }
                else if (firstColStr.StartsWith("Tipi - Type"))
                {
                    typeStr = string.Empty;
                    for (int c = 1; c < dt.Columns.Count; c++)
                        typeStr = typeStr.Trim() + dt.Rows[i][c].ToString().Trim();
                }
                else if (firstColStr.StartsWith("Seri No. - Serial No."))
                {
                    serialStr = string.Empty;
                    for (int c = 1; c < dt.Columns.Count; c++)
                        serialStr = serialStr.Trim() + dt.Rows[i][c].ToString().Trim();
                }
                else if (firstColStr.Contains("Spare Part Book"))
                {
                    subjStr2 = string.Empty;
                    for (int c = 1; c < dt.Columns.Count; c++)
                        subjStr2 = subjStr2.Trim() + dt.Rows[i][c].ToString().Trim();
                }
                else if (firstColStr.StartsWith("İndeks No"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Contains("Request")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Contains("Unit")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().Contains("Part No")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().Contains("Drawing No")) _itemDrw = c;
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

                    if (!_itemDrw.Equals(-1))
                        iTemDrw = dt.Rows[i][_itemDrw].ToString().Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();


                    
                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = subjStr.Trim();

                    if (!string.IsNullOrEmpty(modelStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr.Trim();

                    if (!string.IsNullOrEmpty(typeStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                    if (!string.IsNullOrEmpty(serialStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();

                    if (!string.IsNullOrEmpty(subjStr2))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr2.Trim();



                    if (!string.IsNullOrEmpty(iTemDrw))
                        iTemDESC = iTemDESC.Trim() + Environment.NewLine + iTemDrw.Trim();


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
                    iTemSUBJ = string.Empty;
                }
            }
        }
    }
}

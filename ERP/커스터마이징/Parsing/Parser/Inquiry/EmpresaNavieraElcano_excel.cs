using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class EmpresaNavieraElcano_excel
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

        public EmpresaNavieraElcano_excel(string fileName)
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
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            bool itemStart = false;

            int _itemcode = -1;
            int _itemdesc = -1;
            int _itemqt = -1;
            int _itemunit = -1;

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][0].ToString();

                if(string.IsNullOrEmpty(vessel))
                    vessel = dt.Rows[2][9].ToString().Trim();

                if(string.IsNullOrEmpty(reference))
                    reference = dt.Rows[3][9].ToString().Trim();


                if (firstColStr.Equals("Item")) itemStart = true;


                if (itemStart)
                {
                    if (firstColStr.Equals("Item"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Code No")) _itemcode = c;
                            else if (dt.Rows[i][c].ToString().Contains("Reference")) _itemdesc = c;
                            else if (dt.Rows[i][c].ToString().Contains("Quantity") && dt.Rows[i-1][c].ToString().Contains("REQUIRED")) _itemqt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {

                        // row 값 가져와서 배열에 넣은후 값 추가하기
                        string[] rowValueSpl = new string[20];
                        int columnCount = 0;
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                            {
                                rowValueSpl[columnCount] = c.ToString();
                                columnCount++;
                            }
                        }


                        if (rowValueSpl[4] != null && rowValueSpl[5] == null)
                        {
                            _itemcode = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemdesc = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemqt = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemunit = Convert.ToInt16(rowValueSpl[4].ToString());
                        }


                        if (_itemdesc != -1)
                            iTemDESC = dt.Rows[i][_itemdesc].ToString().Trim();

                        if (_itemqt != -1)
                            iTemQt = dt.Rows[i][_itemqt].ToString().Trim();

                        if (_itemunit != -1)
                            iTemUnit = dt.Rows[i][_itemunit].ToString().Trim();

                        if (_itemcode != -1)
                            iTemCode = dt.Rows[i][_itemcode].ToString().Trim();

                        if (string.IsNullOrEmpty(iTemUnit))
                            iTemUnit = dt.Rows[i][_itemqt + 1].ToString().Trim();
                        
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        if (GetTo.IsInt(iTemQt))
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
}

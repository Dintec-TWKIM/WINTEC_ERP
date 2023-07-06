using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class Advantage
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

        public Advantage(string fileName)
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

            string subjStr = string.Empty;
            string typeStr = string.Empty;
            string sizeStr = string.Empty;
            string serialStr = string.Empty;
            string drwStr = string.Empty;
            string bookStr = string.Empty;
            string makerStr = string.Empty;

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            // ********** 아이템 추가 모드

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][0].ToString();

                if (string.IsNullOrEmpty(vessel))
                {
                    vessel = dt.Rows[0][0].ToString().Replace("Mt","").Trim();
                }
                
                if (firstColStr.Equals("Index No"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Equals("Request")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Equals("Part No.")) _itemCode = c;
                    }
                }
                else if (firstColStr.StartsWith("Name"))
                {
                    if (dt.Rows[i - 1][0].ToString().Equals("MANUFACTURER"))
                    {
                        makerStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(makerStr))
                                makerStr = dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (dt.Rows[i - 1][0].ToString().Equals("PARTICULARS"))
                    {
                        subjStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(subjStr))
                                subjStr = dt.Rows[i][c].ToString().Trim();
                        }
                    }
                }
                else if (firstColStr.StartsWith("Type"))
                {
                    typeStr = string.Empty;
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(typeStr))
                            typeStr = dt.Rows[i][c].ToString().Trim();
                    }
                }
                else if (firstColStr.StartsWith("Size"))
                {
                    sizeStr = string.Empty;
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(sizeStr))
                            sizeStr = dt.Rows[i][c].ToString().Trim();
                    }
                }
                else if (firstColStr.StartsWith("Serial No."))
                {
                    serialStr = string.Empty;
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(serialStr))
                            serialStr = dt.Rows[i][c].ToString().Trim();
                    }
                }
                else if (firstColStr.StartsWith("Drawing No."))
                {
                    drwStr = string.Empty;
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(drwStr))
                            drwStr = dt.Rows[i][c].ToString().Trim();
                    }
                }
                else if (firstColStr.StartsWith("Instruction Book"))
                {
                    bookStr = string.Empty;
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (string.IsNullOrEmpty(bookStr))
                            bookStr = dt.Rows[i][c].ToString().Trim();
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


                    subjStr = subjStr.Trim();
                    typeStr = typeStr.Trim();
                    sizeStr = sizeStr.Trim();


                    if (!string.IsNullOrEmpty(subjStr))
                        iTemSUBJ = subjStr.Trim();

                    if (!string.IsNullOrEmpty(typeStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                    if (!string.IsNullOrEmpty(sizeStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "SIZE: " + sizeStr.Trim();

                    if (!string.IsNullOrEmpty(drwStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG NO: " + drwStr.Trim();

                    if (!string.IsNullOrEmpty(bookStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + bookStr.Trim();


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

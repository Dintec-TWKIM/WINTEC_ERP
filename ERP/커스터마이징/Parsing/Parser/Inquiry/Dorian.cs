using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Dorian
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

        public Dorian(string fileName)
        {
            contact = "";
            reference = "";
            vessel = "";
            imoNumber = "";

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

        #region ==================================================================================================== Logic

        public void Parse()
        {
            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            string subjStr = string.Empty;

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColString = dt.Rows[i][0].ToString();

                if (firstColString.Contains("Your Additional Remarks")) break;

                if (firstColString.Contains("Vessel name"))
                {
                    for (int c = 1; c < 5; c++)
                    {
                        vessel = vessel.Trim() + dt.Rows[i][c].ToString().Trim();
                    }
                }
                else if (firstColString.Contains("RFQ No"))
                {
                    for (int c = 1; c < 5; c++)
                    {
                        reference = reference.Trim() + dt.Rows[i][c].ToString().Trim();
                    }
                }
                else if (firstColString.Contains("Component Name"))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        subjStr = subjStr.Trim() + dt.Rows[i][c].ToString().Trim();
                    }
                }
                else if (firstColString.Contains("Item No"))
                {
                    for (int c = 2; c < dt.Columns.Count; c++)
                    {
                      

                    }
                }
                else if (GetTo.IsInt(firstColString))
                {
                    subjStr = subjStr.Replace("Component Name:", "").Trim();

                    string[] subjSpl = subjStr.Split(new string[] { "    " }, StringSplitOptions.None);

                    for (int r = 0; r < subjSpl.Length; r++)
                    {
                        if (!subjSpl[r].ToString().EndsWith(":"))
                        {
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjSpl[r].ToString().Trim();
                        }
                    }


                    iTemDESC = dt.Rows[i][1].ToString().Trim();
                    iTemCode = dt.Rows[i][2].ToString().Trim();
                    iTemQt = dt.Rows[i][3].ToString().Trim();
                    iTemUnit = dt.Rows[i][4].ToString().Trim();


                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColString;
                    if (!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                    if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);

                    iTemCode = string.Empty;
                    iTemDESC = string.Empty;
                    iTemQt = string.Empty;
                    iTemUnit = string.Empty;
                    iTemSUBJ = string.Empty;
                }
            }
        }

        #endregion
    }
}

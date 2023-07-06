using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Thenamaris
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

        public Thenamaris(string fileName)
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

            int _descInt = -1;
            int _qtInt = -1;
            int _unitInt = -1;

            string componentStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string imoStr = string.Empty;

            bool itemStart = false;

            string descMakerStr = string.Empty;


            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColString = dt.Rows[i][1].ToString();
                string secondColString = dt.Rows[i][2].ToString();

                if (firstColString.Equals("LINE ITEMS")) itemStart = true;

                if (firstColString.Contains("Total no. of items")) break;

                if (!itemStart)
                {
                    // ########## 선명, 문의번호 ########## //
                    reference = dt.Rows[4][3].ToString().Trim();

                    string[] _vesselValue = dt.Rows[1][3].ToString().Split(' ');
                    vessel = _vesselValue[0].ToString().Trim();



                    if (secondColString.Equals("For Component:"))
                    {
                        componentStr = string.Empty;
                        for(int c = 3; c < dt.Columns.Count; c++)
                        {
                            componentStr = componentStr + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (secondColString.Equals("Maker:"))
                    {
                        makerStr = string.Empty;
                        for(int c =3; c< dt.Columns.Count; c++)
                        {
                            makerStr = makerStr + dt.Rows[i][c].ToString().Trim();
                        }
                    
                    }
                    else if (secondColString.Equals("Type:"))
                    {
                        typeStr = string.Empty;
                        for(int c = 3; c < dt.Columns.Count; c++)
                        {
                            typeStr = typeStr + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (secondColString.Equals("Serial No:"))
                    {
                        serialStr = string.Empty;
                        for (int c = 3; c < dt.Columns.Count; c++)
                        {
                            serialStr = serialStr + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (secondColString.Equals("IMO No:"))
                    {
                        imoNumber = string.Empty;
                        for (int c = 3; c < dt.Columns.Count; c++)
                        {
                            imoNumber = imoNumber + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                }
                else
                {
                    if (secondColString.Equals("Maker:"))
                    {
                        descMakerStr = dt.Rows[i][3].ToString().Trim();
                    }

                    if (firstColString.Equals("Item"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Part Name / Makers Ref.")) _descInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Req. Qty")) _qtInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Req. Unit")) _unitInt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColString))
                    {
                        iTemQt = dt.Rows[i][_qtInt].ToString().Trim();
                        iTemUnit = dt.Rows[i][_unitInt].ToString().Trim();
                        iTemCode = dt.Rows[i][0].ToString().Trim();

                        if (string.IsNullOrEmpty(iTemCode))
                        {
                            iTemCode = dt.Rows[i][6].ToString().Replace("PART NO","").Replace(":","").Trim();
                        }

                        for (int c = _descInt; c < _unitInt; c++)
                        {
                            iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        int _i = i + 1;
                        while (string.IsNullOrEmpty(dt.Rows[_i][1].ToString()))
                        {
                            for (int c = _descInt; c < _unitInt; c++)
                            {
                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();

                            }
                            _i += 1;

                            if (_i == dt.Rows.Count - 1)
                                break;
                        }

                        iTemDESC = iTemDESC.Replace("Maker", "\r\nMaker").Replace("Details", "\r\nDetails").Trim();



                        if (iTemDESC.EndsWith("Details:") || iTemDESC.Trim().EndsWith("Details"))
                        {
                            iTemDESC = iTemDESC.Replace("Details:", "").Replace("details", "").Trim();
                        }


                        //iTemDESC = iTemDESC.Replace("Maker:\r\n", "").Replace("Details:\r\n", "").Trim();

                        if (!string.IsNullOrEmpty(componentStr))
                            iTemSUBJ = componentStr;

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr;
                        
                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr;

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialStr;

                        //if (!string.IsNullOrEmpty(descMakerStr))
                        //    iTemDESC = iTemDESC.Trim() + Environment.NewLine + "MAKER: " + descMakerStr;

                        


                        dtItem.Rows.Add();
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);

                        iTemSUBJ = string.Empty;
                        iTemDESC = string.Empty;
                        iTemQt = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                    }
                }
            }
        }

        #endregion
    }
}

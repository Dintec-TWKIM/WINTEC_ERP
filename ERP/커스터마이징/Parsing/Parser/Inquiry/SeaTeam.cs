using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class SeaTeam
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

        public SeaTeam(string fileName)
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

            int _itemCodeInt = -1;
            int _itemDescInt = -1;
            int _itemDrwInt = -1;
            int _itemMakerInt = -1;
            int _itemQtInt = -1;
            int _itemUnitInt = -1;

            string equipStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string drwStr = string.Empty;
            string remarkStr = string.Empty;

            int _refInt = -1;

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(vessel))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().StartsWith("Vessel Name"))
                        {
                            if (string.IsNullOrEmpty(vessel))
                            {
                                for (int ck = c; ck < dt.Columns.Count; ck++)
                                {
                                    if (string.IsNullOrEmpty(vessel))
                                        vessel = dt.Rows[i][ck].ToString().Replace("Vessel Name:", "");

                                    if (!string.IsNullOrEmpty(vessel))
                                    {
                                        string[] vesselSpl = vessel.Split(' ');
                                        vessel = vesselSpl[vesselSpl.Length - 1].ToString().Trim();
                                    }
                                }
                            }
                                
                        }
                    }
                }


                if (i == 2)
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().StartsWith("Our Reference No:"))
                            _refInt = c;

                        if (!_refInt.Equals(-1))
                        {
                            if(string.IsNullOrEmpty(reference))
                                reference = reference.Trim() + dt.Rows[i][c].ToString().Replace("Our Reference No:", "").Trim();
                        }
                    }
                }

                string firstColString = dt.Rows[i][0].ToString();
                string secondColStr = dt.Rows[i][1].ToString();
                

                if (secondColStr.Equals("EQUIPMENT:"))
                    equipStr = dt.Rows[i][2].ToString().Trim();
                else if (secondColStr.Equals("MAKER:"))
                    makerStr = dt.Rows[i][2].ToString().Trim();
                else if (secondColStr.Equals("MODEL/TYPE:"))
                    typeStr = dt.Rows[i][2].ToString().Trim();
                else if (secondColStr.Equals("SERIAL NO:"))
                    serialStr = dt.Rows[i][2].ToString().Trim();
                else if (secondColStr.Equals("DRAWING NO:"))
                    drwStr = dt.Rows[i][2].ToString().Trim();



                if (firstColString.Equals("Item"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("Part No")) _itemCodeInt = c;
                        else if (dt.Rows[i][c].ToString().Contains("Description")) _itemDescInt = c;
                        else if (dt.Rows[i][c].ToString().Contains("Drawing No")) _itemDrwInt = c;
                        else if (dt.Rows[i][c].ToString().Contains("Maker")) _itemMakerInt = c;
                        else if (dt.Rows[i][c].ToString().Equals("Quantity") && _itemQtInt.Equals(-1)) _itemQtInt = c;
                        else if (dt.Rows[i][c].ToString().Equals("Unit") && _itemUnitInt.Equals(-1)) _itemUnitInt = c;
                    }
                }
                else if (GetTo.IsInt(firstColString))
                {
                    iTemNo = firstColString;

                    if(!_itemDescInt.Equals(-1))
                        iTemDESC = dt.Rows[i][_itemDescInt].ToString().Trim();
                    
                    if(!_itemCodeInt.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCodeInt].ToString().Trim();
                    
                    if(!_itemQtInt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();

                    if(!_itemUnitInt.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();



                    dtItem.Rows.Add();if (!string.IsNullOrEmpty(equipStr))
                        iTemSUBJ = equipStr.Trim();

                    if (!string.IsNullOrEmpty(makerStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                    if (!string.IsNullOrEmpty(typeStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                    if (!string.IsNullOrEmpty(serialStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialStr.Trim();

                    if (!string.IsNullOrEmpty(drwStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DRAWING NO: " + drwStr.Trim();

                    iTemSUBJ = iTemSUBJ.Trim();

                    
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                    if(!string.IsNullOrEmpty(iTemSUBJ))
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

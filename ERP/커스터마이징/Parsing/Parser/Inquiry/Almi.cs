using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class Almi
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

        public Almi(string fileName)
        {
            //contact = "";
            reference = "";
            vessel = "";
            //imoNumber = "";

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
            string iTemNo   = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt   = string.Empty;

            string subjString = string.Empty;

            int _verCheckInt   = -1;
            int _itemColumnInt = -1;

            int _itemDesc   = -1;
            int _itemCode   = -1;
            int _itemQt     = -1;
            int _itemUnit   = -1;
            int _itemRemark = -1;

            int _endRowCount = 0;

            bool itemStart = false;
            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            // ********** 아이템 추가 모드

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (_endRowCount < 30)
                {
                    if (_verCheckInt.Equals(-1))
                    {
                        for (int c = 10; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("VERSION4"))
                            {
                                _verCheckInt = c;

                                _itemColumnInt = _verCheckInt + 1;
                            }

                            if (!_verCheckInt.Equals(-1))
                                break;
                        }
                    }
                    // 버전 체크 후 맞으면 계속
                    else
                    {
                        if (string.IsNullOrEmpty(vessel))
                        {
                            vessel = dt.Rows[0][_itemColumnInt + 1].ToString().Trim();
                            reference = dt.Rows[2][_itemColumnInt + 1].ToString().Trim();
                        }


                        string firstColString = dt.Rows[i][_verCheckInt].ToString();                       //순번
                        string lineStandardString = dt.Rows[i][_itemColumnInt].ToString();      //ITEM CODE CHECK

                        if (lineStandardString.Equals("ITEM CODE"))
                        {
                            itemStart = true;

                            for (int c = _itemColumnInt; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().Equals("ITEM DESCRIPTION")) _itemDesc = c;
                                else if (dt.Rows[i][c].ToString().Equals("ITEM CODE")) _itemCode = c;
                                else if (dt.Rows[i][c].ToString().Equals("QUANTITY")) _itemQt = c;
                                else if (dt.Rows[i][c].ToString().Equals("ITEM UNIT")) _itemUnit = c;
                                else if (dt.Rows[i][c].ToString().Equals("ITEM COMMENTS")) _itemRemark = c;
                                else if (!_itemRemark.Equals(-1)) break;
                            }
                        }

                        if (itemStart)
                        {
                            if (string.IsNullOrEmpty(dt.Rows[i][_verCheckInt].ToString()) && !lineStandardString.Equals("ITEM CODE"))
                            {
                                string[] subjSplit = dt.Rows[i][_itemDesc].ToString().Split('[');
                                subjString = subjString.Trim() + Environment.NewLine + subjSplit[0].ToString().Replace("Spare Parts List", "").Trim();
                            }

                            if (GetTo.IsInt(firstColString))
                            {
                                iTemNo   = firstColString;
                                iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                                if (_itemDesc != -1)
                                {
                                    iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                                    int _i = i + 1;

                                    while (string.IsNullOrEmpty(dt.Rows[_i][_verCheckInt].ToString()))
                                    {
                                        iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim();

                                        _i += 1;

                                        if (_i >= dt.Rows.Count)
                                            break;
                                    }
                                }
                                
                                iTemQt   = dt.Rows[i][_itemQt].ToString().Trim();
                                iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();
                                if (!string.IsNullOrEmpty(subjString))
                                    iTemSUBJ = subjString.Trim();

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


                                subjString = string.Empty;

                                if (string.IsNullOrEmpty(dt.Rows[i][_verCheckInt].ToString()))
                                    _endRowCount += 1;
                            }
                        }
                    }
                }
            }
        }
    }
}

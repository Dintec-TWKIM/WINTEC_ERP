using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class ColumbiaShip
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

        public ColumbiaShip(string fileName)
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
            int _itemCodeInt = -1;
            int _itemQtInt = -1;
            int _itemUnitInt = -1;
            int _itemDescInt = -1;

            string itemSUBJ = string.Empty;
            string itemDESC = string.Empty;
            string itemCODE = string.Empty;
            string itemUNIT = string.Empty;
            string itemQT = string.Empty;
            string itemUNIQ = string.Empty;

            string commentStr = string.Empty;

            bool chevronSt = false;


            // Pdf를 엑셀로 변환해서 분석 (엑셀이 편함)
            string excelFile = PdfReader.ToExcel(fileName);
            DataSet ds = ExcelReader.ToDataSet(excelFile);

            //DataSet Table 병합을 위한 Table
            DataTable dsAll = new DataTable();

            //DataSet Table의 Count Get
            int dsCount = ds.Tables.Count;

            for (int i = 0; i <= dsCount - 1; i++)
            {
                dsAll.Merge(ds.Tables[i]);
            }

            ds.Clear();
            ds.Tables.Add(dsAll);

            // 시작
            string subject = "";
            bool addible = false;

            int firstColumNum = -1;

            foreach (DataTable dt in ds.Tables)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string firstColString = string.Empty;
                    string secondColStr = string.Empty;

                    if (i == 0)
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Purchase"))
                                chevronSt = true;
                        }
                    }

                    if (firstColumNum.Equals(-1))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("ShipServ Supplier Record:"))
                            {
                                firstColumNum = c;
                                break;
                            }
                        }
                    }


                    if (firstColumNum.Equals(-1))
                        firstColString = dt.Rows[i][0].ToString();
                    else if (chevronSt)
                    {
                        firstColString = dt.Rows[i][firstColumNum].ToString();
                        secondColStr = dt.Rows[i][1].ToString();
                    }
                    else
                    {
                        firstColString = dt.Rows[i][firstColumNum].ToString();
                        secondColStr = dt.Rows[i][1].ToString();
                    }

                    

                    // ********** 문서 검색 모드
                    if (!addible)
                    {
                        int idx_s;
                        int idx_e;

                        // ***** 담당자 정보
                        if (firstColString.IndexOf("Contact:") == 0)
                        {
                            // 담당자
                            idx_s = 8;
                            idx_e = firstColString.IndexOf("\n", idx_s);
                            contact = firstColString.Substring(idx_s, idx_e - idx_s).Trim();
                        }
                        // ***** 견적 정보
                        else if (firstColString.IndexOf("RFQ Ref:") == 0 || firstColString.StartsWith("PO Ref"))
                        {
                            // 문의번호
                            idx_s = 8;
                            idx_e = firstColString.IndexOf("\n", idx_s);
                            reference = firstColString.Substring(idx_s, idx_e - idx_s).Trim();

                            if (reference.Contains("RFQ"))
                            {
                                int indexOfstr = reference.IndexOf("RFQ");

                                reference = reference.Substring(0, indexOfstr).Trim();
                            }


                            // 선명
                            idx_s = firstColString.IndexOf("Vessel:") + 7;
                            idx_e = firstColString.IndexOf("\n", idx_s);

                            if (idx_e > 0)
                                vessel = firstColString.Substring(idx_s, idx_e - idx_s).Trim();
                            else
                                vessel = firstColString.Substring(idx_s).Trim();

                            if (vessel.Contains("(") && vessel.Contains(")"))
                            {
                                idx_s = vessel.IndexOf("(") + 1;
                                idx_e = vessel.IndexOf(")");

                                vessel = vessel.Substring(idx_s, idx_e - idx_s).Trim();
                            }

                            vessel = vessel.Replace("MV", "").Trim();

                            // IMO 번호
                            idx_s = firstColString.IndexOf("Vessel No.:") + 11;

                            if (idx_s > 0)
                                imoNumber = firstColString.Substring(idx_s).Trim();
                        }
                        else if (firstColString.StartsWith("RFQ Deta"))
                        {
                            firstColString = dt.Rows[i+1][firstColumNum].ToString().Trim();

                            if (string.IsNullOrEmpty(firstColString))
                                firstColString = dt.Rows[i + 2][firstColumNum].ToString().Trim();

                            // 문의번호
                            idx_s = 0;
                            idx_e = firstColString.IndexOf("\n", idx_s);
                            reference = firstColString.Substring(idx_s, idx_e - idx_s).Trim();

                            reference = reference.Replace("RFQ Ref", "").Replace(":", "").Trim();


                            //if (reference.Contains("RFQ"))
                            //{
                            //    int indexOfstr = reference.IndexOf("RFQ");

                            //    reference = reference.Substring(0, indexOfstr).Trim();
                            //}


                            // 선명
                            idx_s = firstColString.IndexOf("Vessel:") + 7;
                            idx_e = firstColString.IndexOf("\n", idx_s);

                            if (idx_e > 0)
                                vessel = firstColString.Substring(idx_s, idx_e - idx_s).Trim();
                            else
                                vessel = firstColString.Substring(idx_s).Trim();

                            if (vessel.Contains("(") && vessel.Contains(")"))
                            {
                                idx_s = vessel.IndexOf("(") + 1;
                                idx_e = vessel.IndexOf(")");

                                vessel = vessel.Substring(idx_s, idx_e - idx_s).Trim();
                            }

                            vessel = vessel.Replace("MV", "").Trim();

                            // IMO 번호
                            idx_s = firstColString.IndexOf("Vessel No.:") + 11;

                            if (idx_s > 0)
                                imoNumber = firstColString.Substring(idx_s).Trim();
                        }
                        else if (firstColString.ToLower().StartsWith("comments"))
                        {
                            commentStr = firstColString.Replace("Comments:", "").Trim();
                        }
                        // ***** 아이템 정보
                        else if (firstColString.IndexOf("Line Items") == 0)
                        {
                            addible = true;
                            continue;
                        }
                    }
                    // ********** 아이템 추가 모드
                    else
                    {
                        // ***** 첫번째 글자가 No.(아이템 헤더) → 스킵						
                        if (firstColString == "No." || secondColStr == "No.")
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().Contains("Part No")) _itemCodeInt = c;
                                else if (dt.Rows[i][c].ToString().Contains("Qty")) _itemQtInt = c;
                                else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnitInt = c;
                                else if (dt.Rows[i][c].ToString().Contains("Description")) _itemDescInt = c;
                            }
                        }
                        // ***** 첫번째 글자가 문자면 → 서브젝트
                        else if ((!GetTo.IsInt(firstColString) && firstColString.Length > 10) )
                        {
                            subject = subject.Trim() + " " + firstColString.Trim();
                            subject = subject.Replace("Equipment Section:", "").Replace("For:", "FOR").Replace("Man:", " \r\nMAKER:").Replace("Serial", "\r\nSerial").Replace("Type:", "\r\nType:").Replace("Desc:", "\r\nDesc:").Replace("DESC", "\r\nDESC:").Replace("desc", "\r\ndesc").Trim();
                            subject = subject.Replace("CAPACITY", "\r\nCAPACITY").Replace("capacity", "\r\ncapacity").Replace("Capacity", "\r\ncapacity").Replace("Model","\r\nModel").Trim();

                            if (subject.ToUpper().StartsWith("FOR"))
                            {
                                subject = subject.Replace("For", "").Replace("FOR", "").Replace("for", "").Trim();
                            }

                            subject = subject.Replace("\r\n\r\n", "\r\n").Trim();
                        }
                        else if (GetTo.IsInt(firstColString) || GetTo.IsInt(secondColStr))
                        {
                            if (Convert.ToInt16(dt.TableName.ToString().Replace("Table", "")) > 1)
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


                                //       NO / Part Type / Part No. / Qty / Unit / Description
                                //       0  /      1    /  2       /  3 /  4    /   5 
                                if ((rowValueSpl[5] != null) && !firstColString.Equals("1"))
                                {
                                    _itemCodeInt = Convert.ToInt16(rowValueSpl[2].ToString());
                                    _itemQtInt = Convert.ToInt16(rowValueSpl[3].ToString());
                                    _itemUnitInt = Convert.ToInt16(rowValueSpl[4].ToString());
                                    _itemDescInt = Convert.ToInt16(rowValueSpl[5].ToString());
                                }
                                else if (rowValueSpl[5] == null && rowValueSpl[4] != null && !firstColString.StartsWith("1"))
                                {
                                    if (string.IsNullOrEmpty(dt.Rows[i + 1][0].ToString()) && !firstColString.Equals("1") && dt.Rows.Count > i + 3)
                                    {
                                        string rowValue = string.Empty;
                                        string rowValue2 = string.Empty;
                                        for (int c = 0; c < dt.Columns.Count; c++)
                                        {
                                            rowValue = rowValue + dt.Rows[i + 1][c].ToString();
                                            rowValue2 = rowValue2 + dt.Rows[i + 2][c].ToString();
                                            rowValue = rowValue.Trim();
                                            rowValue2 = rowValue2.Trim();
                                        }

                                        if (string.IsNullOrEmpty(rowValue) && string.IsNullOrEmpty(rowValue2))
                                        {
                                            _itemCodeInt = -1;
                                            _itemQtInt = Convert.ToInt16(rowValueSpl[2].ToString());
                                            _itemUnitInt = Convert.ToInt16(rowValueSpl[3].ToString());
                                            _itemDescInt = Convert.ToInt16(rowValueSpl[4].ToString());
                                        }
                                        else
                                        {
                                            // 바로 밑 row에 desc가 있는 경우가 있음
                                            _itemQtInt = Convert.ToInt16(rowValueSpl[3].ToString());
                                            _itemUnitInt = Convert.ToInt16(rowValueSpl[4].ToString());

                                            if (!GetTo.IsInt(dt.Rows[i][_itemQtInt].ToString()))
                                            {
                                                _itemQtInt = Convert.ToInt16(rowValueSpl[2].ToString());
                                                _itemUnitInt = Convert.ToInt16(rowValueSpl[3].ToString());
                                                _itemDescInt = Convert.ToInt16(rowValueSpl[4].ToString());
                                            }

                                            if (!GetTo.IsInt(dt.Rows[i][_itemQtInt].ToString().Replace(".","")))
                                            {
                                                _itemQtInt = Convert.ToInt16(rowValueSpl[3].ToString());
                                                _itemUnitInt = Convert.ToInt16(rowValueSpl[4].ToString());
                                            }

                                            if (!string.IsNullOrEmpty(rowValue))
                                            {
                                                itemDESC = dt.Rows[i][_itemDescInt].ToString() + " " + rowValue;
                                                _itemDescInt = -1;
                                            }

                                            if (!string.IsNullOrEmpty(rowValue2))
                                            {
                                                itemDESC = dt.Rows[i][_itemDescInt].ToString() + " " + rowValue2;
                                                _itemDescInt = -1;
                                            }

                                            //if (descCheck)
                                                //_itemDescInt = Convert.ToInt16(rowValueSpl[5].ToString());
                                        }
                                    }
                                    else
                                    {
                                        //       NO / Part Type / Qty / Unit / Description
                                        //       0  /      1    /  2   /  3 /  4
                                        _itemCodeInt = -1;
                                        _itemQtInt = Convert.ToInt16(rowValueSpl[2].ToString());
                                        _itemUnitInt = Convert.ToInt16(rowValueSpl[3].ToString());
                                        _itemDescInt = Convert.ToInt16(rowValueSpl[4].ToString());
                                    }
                                }
                                else if (rowValueSpl[3] != null && rowValueSpl[4] == null)
                                {
                                    _itemCodeInt = -1;
                                        _itemQtInt = Convert.ToInt16(rowValueSpl[1].ToString());
                                        _itemUnitInt = Convert.ToInt16(rowValueSpl[2].ToString());
                                        _itemDescInt = Convert.ToInt16(rowValueSpl[3].ToString());
                                }
                                else if (rowValueSpl[5] != null && rowValueSpl[6] == null)
                                {
                                    _itemCodeInt = Convert.ToInt16(rowValueSpl[2].ToString());
                                    _itemQtInt = Convert.ToInt16(rowValueSpl[3].ToString());
                                    _itemUnitInt = Convert.ToInt16(rowValueSpl[4].ToString());
                                    _itemDescInt = Convert.ToInt16(rowValueSpl[5].ToString());
                                }

                               
                                if (!string.IsNullOrEmpty(subject))
                                    itemSUBJ = subject.Trim();

                                if (!string.IsNullOrEmpty(commentStr))
                                {
                                    int idx_s = commentStr.IndexOf("http://");

                                    if(idx_s != -1)
                                        commentStr = commentStr.Substring(0, idx_s).Trim();

                                    itemSUBJ = itemSUBJ.Trim() + Environment.NewLine + commentStr.Trim();


                                    itemSUBJ = itemSUBJ.Trim();

                                }

                                if (!_itemCodeInt.Equals(-1))
                                    itemCODE = dt.Rows[i][_itemCodeInt].ToString().Trim();

                                if (!_itemUnitInt.Equals(-1))
                                {
                                    itemUNIT = dt.Rows[i][_itemUnitInt].ToString().Trim();

                                    if (itemUNIT.Length > 15)
                                    {
                                        itemUNIT = "PCS";
                                        //_itemQtInt = dt.Rows[i][_itemQtInt]
                                    }
                                }

                                if (!_itemQtInt.Equals(-1))
                                    itemQT = dt.Rows[i][_itemQtInt].ToString().Trim();

                                if (string.IsNullOrEmpty(itemDESC))
                                {
                                    for (int c = _itemUnitInt + 1; c <= _itemDescInt; c++)
                                    {
                                        itemDESC = itemDESC.Trim() + Environment.NewLine + dt.Rows[i][c].ToString().Trim();
                                    }

                                    int _i = i + 1;
                                    while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && _i < dt.Rows.Count - 1)
                                    {
                                        if (_i == i)
                                        {
                                            for (int c = _itemUnitInt + 1; c <= _itemDescInt; c++)
                                            {
                                                itemDESC = itemDESC.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
                                            }
                                        }
                                        else
                                        {
                                            for (int c = 1; c <= _itemDescInt; c++)
                                            {
                                                itemDESC = itemDESC.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
                                            }
                                        }


                                        if (!_itemCodeInt.Equals(-1))
                                            itemCODE = itemCODE.Trim() + dt.Rows[_i][_itemCodeInt].ToString().Trim();

                                        _i += 1;

                                        if (_i >= dt.Rows.Count)
                                            break;
                                    }
                                }

                                itemDESC = itemDESC.Replace("[EA]", "").Replace("[PC]", "").Replace("[PCS]","").Trim();
                                itemCODE = itemCODE.Replace("\n", "").Trim();

                                if (!itemDESC.Contains("Freight and Packing & Handling"))
                                {


                                    if (itemDESC.EndsWith(","))
                                        itemDESC = itemDESC.Substring(0, itemDESC.Length - 1).Trim();


                                    if (itemDESC.Contains("Comments"))
                                    {
                                        int idx_s = itemDESC.IndexOf("Comments");

                                        itemDESC = itemDESC.Substring(0, idx_s);
                                    }

                                    itemDESC = itemDESC.Replace("[EA]", "").Replace("[PC]", "").Replace("[PCS]", "").Trim();

                                    dtItem.Rows.Add();
                                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = dt.Rows[i][0];				// A컬럼 고정인듯
                                    if(!string.IsNullOrEmpty(itemSUBJ))
                                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + itemSUBJ.Trim();
                                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = itemCODE.Trim();		
                                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = itemDESC.Trim();
                                    if (!string.IsNullOrEmpty(itemQT))
                                    {
                                        if (GetTo.IsInt(itemQT.Substring(0,1)))
                                            dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = itemQT.Trim();
                                    }
                                    
                                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(itemUNIT.ToUpper().Replace("\n", "").Trim());

                                    subject = string.Empty;

                                    itemDESC = string.Empty;
                                    itemCODE = string.Empty;
                                    itemQT = string.Empty;
                                    itemUNIT = string.Empty;
                                }
                            }
                            else
                            {
                                if(!_itemCodeInt.Equals(-1))
                                    itemCODE = dt.Rows[i][_itemCodeInt].ToString().Trim();

                                if(!_itemUnitInt.Equals(-1))
                                    itemUNIT = dt.Rows[i][_itemUnitInt].ToString().Trim();

                                if(!_itemQtInt.Equals(-1))
                                    itemQT = dt.Rows[i][_itemQtInt].ToString().Trim();

                                if(!_itemDescInt.Equals(-1))
                                    itemDESC = dt.Rows[i][_itemDescInt].ToString().Trim();

                                if (!string.IsNullOrEmpty(subject))
                                    itemSUBJ = subject.Trim();

                                itemDESC = itemDESC.Replace("[EA]", "").Replace("[PC]", "").Replace("[PCS]","").Trim();
                                itemCODE = itemCODE.Replace("\n", "").Trim();


                                if (!itemDESC.Contains("Freight and Packing & Handling"))
                                {
                                    dtItem.Rows.Add();
                                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = dt.Rows[i][0];				// A컬럼 고정인듯
                                    if(!string.IsNullOrEmpty(itemSUBJ))
                                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + itemSUBJ.Trim();
                                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = itemCODE.Trim();
                                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = itemDESC.Trim();
                                    dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = itemQT.Trim();
                                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(itemUNIT.ToUpper().Replace("\n", "").Trim());

                                    itemDESC = string.Empty;
                                    itemCODE = string.Empty;
                                    itemQT = string.Empty;
                                    itemUNIT = string.Empty;
                                    subject = string.Empty;
                                }
                            }
                        }
                        // ***** 첫번째 글자가 빈칸 → Part No., Description 조합 (페이지가 넘어갔을 때 이것만 있는 경우가 있음, FB16113133)
                        else if (firstColString == "")
                        {
                            // 우선 종료 글자가 있는지 판단
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (dt.Rows[i][j].ToString().IndexOf("Generated by ShipServ") == 0)
                                {
                                    return;	// 완전 종료
                                }
                            }

                            // 종료글자가 없다면 이전 값의 연속이라고 판단
                            if (dtItem.Rows.Count > 0)
                            {
                                string prev = "";
                                string now = "";

                                // Part No.
                                prev = dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"].ToString();
                                now = dt.Rows[i][2].ToString();

                                if (now != "" && !prev.Contains(now))
                                {
                                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = prev + now;
                                }


                                // Description
                                prev = dtItem.Rows[dtItem.Rows.Count - 1]["DESC"].ToString();

                                for (int j = dt.Columns.Count - 1; j > 4; j--)
                                {
                                    now = dt.Rows[i][j].ToString().Replace("\n", "\r\n");

                                    if (now != "" && prev.Contains(now))
                                    {
                                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = prev + "\r\n" + now;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
#endregion
    }
}

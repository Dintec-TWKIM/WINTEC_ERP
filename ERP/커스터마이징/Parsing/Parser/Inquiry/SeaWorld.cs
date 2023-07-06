using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class SeaWorld
    {
        string vessel;
        string reference;
        DataTable dtItem;

        string fileName;
        UnitConverter uc;


        #region ==================================================================================================== Property

        public string Vessel
        {
            get
            {
                return vessel;
            }
        }

        public string Reference
        {
            get
            {
                return reference;
            }
        }

        public DataTable Item
        {
            get
            {
                return dtItem;
            }
        }

        #endregion ==================================================================================================== Constructor



        public SeaWorld(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호

            dtItem = new DataTable();
            dtItem.Columns.Add("NO");           // 순번
            dtItem.Columns.Add("SUBJ");         // 주제
            dtItem.Columns.Add("ITEM");         // 품목코드
            dtItem.Columns.Add("DESC");         // 품목명
            dtItem.Columns.Add("UNIT");         // 단위
            dtItem.Columns.Add("QT");           // 수량
            dtItem.Columns.Add("UNIQ");         // 선사
            this.fileName = fileName;
            this.uc = new UnitConverter();
        }

        public void Parse()
        {
            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            string equipStr = string.Empty;
            string makerStr = string.Empty;
            string modelStr = string.Empty;
            string bookStr = string.Empty;
            string serialStr = string.Empty;
            string partStr = string.Empty;
            string assemStr = string.Empty;
            string drwStr = string.Empty;

            string itemMakerStr = string.Empty;
            string itemSerialStr = string.Empty;
            string itemModelStr = string.Empty;

            int _itemDrw = -1;
            int _itemCode = -1;
            int _itemMaker = -1;
            int _itemDesc = -1;
            int _itemUnit = -1;


            #region ########### READ ###########
            // Pdf를 Xml로 변환해서 분석 (1000$ 짜리로 해야함, 500$ 짜리로 하면 Description 부분에 CRLF가 안됨)
            // 1. 우선 500$ 짜리로 Xml 변환함 (1000$ 짜리의 경우 도면이 붙어 있으면 시간이 엄청 오래 걸림)
            string xmlTemp = PdfReader.ToXml(fileName);

            // 2. 도면을 제외한 Page 카운트 가져오기
            int pageCount = xmlTemp.Count("<page>");

            // 3. 앞서 나온 Page를 근거로 파싱 시작			
            string xml = string.Empty;//PdfReader.GetXml(fileName, 1, pageCount);
            xml = PdfReader.GetXml(fileName);
            DataSet ds = PdfReader.ToDataSet(xml);

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
            #endregion ########### READ ###########

            foreach (DataTable dt in ds.Tables)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string firstColStr = dt.Rows[i][0].ToString();
                    string secondColStr = dt.Rows[i][0].ToString();

                    if (firstColStr.Contains("Total Items Requested")) break;

                    if (firstColStr.StartsWith("Request For Quotation") || secondColStr.StartsWith("Request For Quotation"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            reference = reference.Trim() + dt.Rows[i][c].ToString().Replace("Request For Quotation", "").Replace(":", "").Trim();
                            vessel = vessel.Trim() + dt.Rows[i + 1][c].ToString().Replace("Vessel", "").Replace(":", "").Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Equipment") || secondColStr.StartsWith("Equipment"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            equipStr = equipStr.Trim() + dt.Rows[i][c].ToString().Replace("Equipment", "").Replace(":", "").Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Maker:") || secondColStr.StartsWith("Maker:"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(makerStr))
                                makerStr = makerStr.Trim() + dt.Rows[i][c].ToString().Replace("Maker:", "").Trim();
                            else
                                itemMakerStr = itemMakerStr.Trim() + dt.Rows[i][c].ToString().Replace("Maker:", "").Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Model") || secondColStr.StartsWith("Model"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(modelStr))
                                modelStr = modelStr.Trim() + dt.Rows[i][c].ToString().Replace("Model:", "").Trim();
                            else
                                itemModelStr = itemModelStr.Trim() + dt.Rows[i][c].ToString().Replace("Model:", "").Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Book") || secondColStr.StartsWith("Book"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            bookStr = bookStr.Trim() + dt.Rows[i][c].ToString().Replace("Book No:", "").Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Serial") || secondColStr.StartsWith("Serial"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(serialStr))
                                serialStr = serialStr.Trim() + dt.Rows[i][c].ToString().Replace("Serial No.:", "").Trim();
                            else
                                itemSerialStr = itemSerialStr.Trim() + dt.Rows[i][c].ToString().Replace("Serial No.:", "").Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Assmbly") || secondColStr.StartsWith("Assmbly"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                             assemStr = assemStr.Trim() + dt.Rows[i][c].ToString().Replace("Assembly:", "").Trim();
                        }
                    }
                    else if (firstColStr.Equals("Item"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Drawing No.")) _itemDrw = c;
                            else if (dt.Rows[i][c].ToString().Equals("Part No.")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Maker")) _itemMaker = c;
                            else if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit of")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qty. Measure")) _itemUnit = c;
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
                            _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemMaker = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
                        }
                        else if (rowValueSpl[5] != null && rowValueSpl[6] == null)
                        {
                            _itemDrw = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemMaker = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[5].ToString());
                        }


                        if (!_itemUnit.Equals(-1))
                        {
                            string[] unitqtSpl = dt.Rows[i][_itemUnit].ToString().Split(' ');

                            if (unitqtSpl.Length >= 2)
                            {
                                iTemQt = unitqtSpl[0].ToString().Trim();
                                iTemUnit = unitqtSpl[1].ToString().Trim();
                            }
                            else
                            {
                                //iTemQt = dt.Rows[i][_itemqt] 

                            }
                        }

                        if (!_itemMaker.Equals(-1))
                        {
                            if (string.IsNullOrEmpty(itemMakerStr))
                            {
                                itemMakerStr = dt.Rows[i][_itemMaker].ToString().Trim();


                                int _i = i + 1;
                                while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                {
                                    itemMakerStr = itemMakerStr.Trim() + Environment.NewLine + dt.Rows[_i][_itemMaker].ToString().Trim();

                                    _i += 1;
                                    
                                    if (_i >= dt.Rows.Count)
                                        break;
                                }
                            }
                        }

                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim();
                                
                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        if (!_itemDrw.Equals(-1))
                        {
                            if (string.IsNullOrEmpty(drwStr))
                                drwStr = dt.Rows[i][_itemDrw].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(equipStr))
                            iTemSUBJ = equipStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(modelStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr.Trim();

                        if (!string.IsNullOrEmpty(bookStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "BOOK NO:" + bookStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(drwStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DRAWING NO: " + drwStr.Trim();

                        if (!string.IsNullOrEmpty(assemStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + assemStr.Trim();


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;
                    }
                }
            }
        }
    }
}

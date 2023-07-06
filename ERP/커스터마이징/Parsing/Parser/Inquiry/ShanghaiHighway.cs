using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class ShanghaiHighway
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



        public ShanghaiHighway(string fileName)
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
            dtItem.Columns.Add("UNIQ");         //선사코드

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
            string iTemDrw = string.Empty;

            string serialString = string.Empty;
            string makerString = string.Empty;
            string remarkString = string.Empty;
            string detailString = string.Empty;
            string modelString = string.Empty;
            string typeString = string.Empty;

            string codeTypeStr = string.Empty;

            int _itemCode = -1;
            int _itemDesc = -1;
            int _itemUnit = -1;

            int _itemCode_ = -1;
            int _itemDesc_ = -1;
            int _itemDrawing_ = -1;

            int _itemQt = -1;
            int _itemDrawing = -1;
            int _itemCodeType = -1;

            bool itemStart = false;

            //string excelFile = PdfReader.ToExcel(fileName);
            //DataSet ds1 = ExcelReader.ToDataSet(excelFile);


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
                    string firstColString = dt.Rows[i][0].ToString();

                    if (firstColString.Equals("Item")) itemStart = true;

                    if (!itemStart)
                    {
                        for (int o = 0; o < dt.Columns.Count; o++)
                        {
                            string dataValue = dt.Rows[i][o].ToString();

                            if (dataValue.Equals("Vessel :"))
                                vessel = dt.Rows[i][o + 1].ToString().Trim();

                            if (dataValue.Equals("Indent No :"))
                                reference = dt.Rows[i][o + 1].ToString().Trim();

                            if (dataValue.Equals("S.No :"))
                            {
                                if (!dt.Rows[i][o + 1].ToString().Equals("Type :"))
                                {
                                    serialString = dt.Rows[i][o + 1].ToString().Trim();
                                }
                            }

                            if (dataValue.Equals("Make :"))
                            {
                                makerString = dt.Rows[i][o + 1].ToString().Trim();

                                if (string.IsNullOrEmpty(makerString))
                                {
                                    makerString = dt.Rows[i][o + 2].ToString().Trim();

                                        if(makerString.Contains("Model"))
                                            makerString = makerString.Trim() + " " + dt.Rows[i][o+3].ToString().Trim();

                                }else
                                {
                                    if(makerString.Contains("Model"))
                                            makerString = makerString.Trim() + " " + dt.Rows[i][o+2].ToString().Trim();
                                }

                                makerString = makerString.Replace("Model", " Model").Trim();
                            }

                            if (dataValue.Equals("Type :"))
                            {
                                typeString = dt.Rows[i][o + 1].ToString().Trim();

                                if (typeString.StartsWith("Year of"))
                                    typeString = string.Empty;
                            }

                            if (dataValue.Equals("Model :"))
                                modelString = dt.Rows[i][o + 1].ToString().Trim();


                            if (dataValue.Equals("Remarks :"))
                            {
                                remarkString = dt.Rows[i][o + 1].ToString().Trim();

                                int _i = i + 1;
                                while (!string.IsNullOrEmpty(dt.Rows[_i][o + 1].ToString()))
                                {
                                    remarkString = remarkString.Trim() + Environment.NewLine + dt.Rows[_i][o + 1].ToString().Trim();

                                    _i += 1;

                                    if (dt.Rows[i][_i].ToString().Equals("Item"))
                                        break;
                                }
                                remarkString = remarkString.Trim();
                            }


                            if (dataValue.Equals("Other Details :"))
                            {
                                if (!dt.Rows[i - 1][0].ToString().Contains("Make"))
                                {
                                    detailString = dt.Rows[i - 1][o + 1].ToString().Trim() + dt.Rows[i - 1][o + 2].ToString().Trim();
                                }


                                if (!string.IsNullOrEmpty(dt.Rows[i][o + 1].ToString()))
                                    detailString = detailString.Trim() + " " + dt.Rows[i][o + 1].ToString().Trim();
                                else
                                    detailString = detailString.Trim() + " " + dt.Rows[i][o + 2].ToString().Trim();


                                int _i = i + 1;
                                while (!dt.Rows[_i][0].ToString().Equals("Item"))
                                {
                                    detailString = detailString.Trim() + " " + dt.Rows[_i][o + 1].ToString().Trim() + dt.Rows[_i][o + 2].ToString().Trim();

                                    _i += 1;
                                }

                                detailString = detailString.Trim();
                            }
                        }

                    }
                    else
                    {
                        if (firstColString.Equals("Item"))
                        {
                            for (int j = 1; j < dt.Columns.Count; j++)
                            {
                                if (dt.Rows[i][j].ToString().Contains("Item Code")) _itemCode = j;
                                else if (dt.Rows[i][j].ToString().Contains("Drawing No")) _itemDrawing = j;
                                else if (dt.Rows[i][j].ToString().Contains("D e s c r i p t i o n")) _itemDesc = j;
                                else if (dt.Rows[i][j].ToString().Contains("Unit")) _itemUnit = j;
                                else if (dt.Rows[i][j].ToString().Contains("Quantity")) _itemQt = j;
                                else if (dt.Rows[i][j].ToString().Contains("Code Type")) _itemCodeType = j;
                            }
                        }



                        if (GetTo.IsInt(firstColString))
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


                            //if (dt.Rows[i][1].ToString().ToUpper().Equals("NONE"))
                            //{
                            //    if (rowValueSpl[4] != null && rowValueSpl[5] == null)
                            //    {
                            //        _itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
                            //        _itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
                            //    }
                            //    else if (rowValueSpl[5] != null && rowValueSpl[6] == null)
                            //    {
                            //        _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
                            //        _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                            //    }
                            //}
                            
                            


                            //if ((rowValueSpl[8] != null) && rowValueSpl[9] == null)
                            //{
                                _itemUnit = Convert.ToInt16(rowValueSpl[columnCount - 2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[columnCount - 1].ToString());
                            //}
                            

                            if (!string.IsNullOrEmpty(makerString))
                                iTemSUBJ = "MAKER: " + makerString;

                            if (!string.IsNullOrEmpty(serialString))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialString;

                            if (!string.IsNullOrEmpty(typeString))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeString;

                            if (!string.IsNullOrEmpty(modelString))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelString;

                            if (!string.IsNullOrEmpty(remarkString))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + remarkString;

                            if (!string.IsNullOrEmpty(detailString))
                                iTemSUBJ = detailString + Environment.NewLine + iTemSUBJ.Trim();

                            iTemSUBJ = iTemSUBJ.Trim();


                            if (!_itemCodeType.Equals(-1))
                                codeTypeStr = dt.Rows[i][_itemCodeType].ToString().Trim();

                            if (codeTypeStr.ToUpper().Contains("NONE") || codeTypeStr.Contains("none") || string.IsNullOrEmpty(codeTypeStr))
                            {
                                //if (rowValueSpl[4] != null && rowValueSpl[5] == null)
                                //{
                                //    _itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
                                //    _itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
                                //}
                                //else if (rowValueSpl[5] != null && rowValueSpl[6] == null)
                                //{
                                //    _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
                                //    _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                                //}

                                _itemDesc_ = _itemCode;
                                _itemCode_ = -1;
                                _itemDrawing_ = -1;

                                int _ii = i;
                                if (_ii < dt.Rows.Count - 1)
                                    _ii += 1;

                                for (int c = _itemDesc_; c < _itemUnit; c++)
                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();

                                while (string.IsNullOrEmpty(dt.Rows[_ii][0].ToString()))
                                {
                                    for (int c = _itemDesc_; c < _itemUnit; c++)
                                        iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_ii][c].ToString().Trim();

                                    if (_ii < dt.Rows.Count - 1)
                                        _ii += 1;
                                    else
                                        break;
                                }
                            }
                            else
                            {
                                _itemDesc_ = _itemDesc;
                                _itemCode_ = _itemCode;
                                _itemDrawing_ = _itemDrawing;

                                if (!_itemDrawing_.Equals(-1))
                                    iTemDrw = dt.Rows[i][_itemDrawing_].ToString().Trim();
                                else
                                    _itemDrawing_ = _itemCode_;

                                if (!_itemCode_.Equals(-1))
                                    iTemCode = dt.Rows[i][_itemCode_].ToString().Trim();

                                if (iTemCode.Equals("NONE"))
                                    iTemCode = string.Empty;


                                int _ii = i;
                                if (_ii < dt.Rows.Count - 1)
                                    _ii += 1;

                                

                                    for (int c = _itemDrawing_ + 1; c < _itemUnit; c++)
                                        iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();

                                    while (string.IsNullOrEmpty(dt.Rows[_ii][0].ToString()))
                                    {
                                        for (int c = _itemDrawing_ + 1; c < _itemUnit; c++)
                                            iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_ii][c].ToString().Trim();

                                        if (_ii < dt.Rows.Count - 1)
                                            _ii += 1;
                                        else
                                            break;
                                    }


                                    int _i = i;
                                    if (_i < dt.Rows.Count - 1)
                                        _i += 1;
                                    // 반복되는 row 추가
                                    while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                    {
                                        // drawing, code 자리에 있는 경우가 있음.
                                        if (iTemDrw.Contains("(") && !iTemDrw.Contains(")"))
                                        {
                                            iTemDrw = iTemDrw.Trim() + " " + dt.Rows[_i][_itemCode_].ToString().Trim();
                                            if (!_itemDrawing_.Equals(-1))
                                                iTemDrw = iTemDrw.Trim() + Environment.NewLine + dt.Rows[_i][_itemDrawing_].ToString().Trim();
                                        }
                                        else
                                        {
                                            if (!_itemDrawing_.Equals(-1))
                                            {
                                                string dtValue = dt.Rows[_i][_itemDrawing_].ToString().ToUpper();
                                                if (dtValue.Contains("MAKER:") || string.IsNullOrEmpty(dt.Rows[_i][_itemDrawing_ + 1].ToString()))
                                                {
                                                    iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][_itemDrawing_].ToString().Trim();
                                                    iTemDrw = iTemDrw.Trim() + Environment.NewLine + dt.Rows[_i][_itemCode_].ToString().Trim();
                                                }
                                                else
                                                {
                                                    iTemDrw = iTemDrw.Trim() + Environment.NewLine + dt.Rows[_i][_itemDrawing_].ToString().Trim();
                                                    iTemCode = iTemCode.Trim() + Environment.NewLine + dt.Rows[_i][_itemCode_].ToString().Trim();
                                                }

                                            }
                                        }

                                        // 중단
                                        if (_i < dt.Rows.Count - 1)
                                            _i += 1;
                                        else
                                            break;
                                    }
                        
                            }

                            if (!_itemQt.Equals(-1))
                            {
                                iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                                if (string.IsNullOrEmpty(iTemQt))
                                {
                                    if(!(_itemQt + 1 >= dt.Columns.Count))
                                        iTemQt = dt.Rows[i][_itemQt + 1].ToString().Trim();
                                }
                            }

                            if (!_itemUnit.Equals(-1))
                                iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                            if (codeTypeStr.ToUpper().Equals("NONE"))
                            {
                                iTemDESC = iTemDrw.Trim() + iTemDESC.Trim();
                                iTemDrw = string.Empty;
                            }

                            if (!string.IsNullOrEmpty(iTemDrw))
                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + "DWG: " + iTemDrw;

                            iTemSUBJ = iTemSUBJ.Trim();
                            iTemDESC = iTemDESC.Trim();

                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColString;
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                            if (!string.IsNullOrEmpty(iTemSUBJ))
                                dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode.Trim();


                            iTemSUBJ = string.Empty;
                            iTemDESC = string.Empty;
                            iTemQt = string.Empty;
                            iTemUnit = string.Empty;
                            iTemCode = string.Empty;
                            iTemDrw = string.Empty;
                        }
                    }
                }
            }
        }
    }
}

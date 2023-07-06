using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Wallem
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



        public Wallem(string fileName)
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
            //int columnCount = 0;

            string rfqNum = string.Empty;

            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            string maker = string.Empty;

            int _itemCodeInt = -1;
            int _itemDescInt = -1;
            int _itemUnitInt = -1;
            int _itemQtInt = -1;
            int _itemDescInt2 = -1;
            int _itemRmk = -1;


            string makerString = string.Empty;
            string modelString = string.Empty;
            string serialString = string.Empty;
            string subjectString = string.Empty;
            string dwgString = string.Empty;

            string descStr = string.Empty;

            int firstNsec = 1;

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


            bool itemStart = false;

            bool pageChangeChk = false;

          
            foreach (DataTable dt in ds.Tables)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dataValue = dt.Rows[i][0].ToString();
                    string dataSecValue = dt.Rows[i][1].ToString();

                    // 첫 컬럼 값
                    if (dataValue.Equals("No")) itemStart = true;

                    if (!itemStart)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString().IndexOf("Requisition Ref No") == 0)
                            {
                                reference = dt.Rows[i][j + 1].ToString().Trim();

                                if (string.IsNullOrEmpty(reference))
                                {
                                    reference = dt.Rows[i][j + 2].ToString().Trim();
                                }
                            }
                        }


                        // 선명
                        if (dataValue.Equals("Vessel Name"))
                        {
                            vessel = dt.Rows[i][1].ToString().Trim();
                            if (string.IsNullOrEmpty(vessel))
                            {
                                vessel = dt.Rows[i][2].ToString().Trim();
                            }
                        }
                        else if (dataValue.Equals("RFQ Line"))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString() == "Item Description") _itemDescInt = c;            //품목명
                                else if (dt.Rows[i][c].ToString() == "Drawing Part No") _itemCodeInt = c;               //아이템코드
                                else if (dt.Rows[i][c].ToString() == "UOM") _itemUnitInt = c;                           //단위
                                else if (dt.Rows[i][c].ToString().Contains("Qty")) _itemQtInt = c;                             //수량
                                else if (dt.Rows[i][c].ToString().Contains("Requested")) _itemQtInt = c;
                                else if (dt.Rows[i][c].ToString().Contains("RFQ Info")) _itemDescInt2 = c;       // 품목명 + @
                            }
                        } 
                        else if (dataValue.Equals("Maker"))
                        {
                            makerString = dt.Rows[i][1].ToString().Trim();

                            if (string.IsNullOrEmpty(makerString))
                                makerString = dt.Rows[i][2].ToString().Trim();
                        }else if (dataValue.Equals("Equipment Type"))
                        {
                            subjectString = string.Empty;
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                subjectString = subjectString + " " +dt.Rows[i][c].ToString().Trim();
                            }
                            //    subjectString = dt.Rows[i][1].ToString().Trim();

                            //if (string.IsNullOrEmpty(subjectString))
                            //    subjectString = dt.Rows[i][2].ToString().Trim();
                        }
                        else if (dataValue.Equals("Model Details"))
                        {
                            modelString = dt.Rows[i][1].ToString().Trim();

                            if (string.IsNullOrEmpty(modelString))
                            {
                                for (int c = 0; c < dt.Columns.Count; c++)
                                {
                                    modelString = modelString.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                                }
                            }
                        }
                        else if (dataValue.Equals("Serial No"))
                        {
                            serialString = dt.Rows[i][1].ToString().Trim();

                            if (string.IsNullOrEmpty(serialString))
                                serialString = dt.Rows[i][2].ToString().Trim();

                            serialString = serialString.Replace("NA", "").Trim();
                        }
                    }
                    else
                    {
                        //################# 순번, 주제, 품목코드, 품목명, 단위, 수량
                        string firstColString = dt.Rows[i][0].ToString();
                        string secondColStr = dt.Rows[i][1].ToString();

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {

                            if (dt.Rows[i][c].ToString().Contains("Page") && dt.Rows[i][c].ToString().Contains("of"))
                            {
                                pageChangeChk = true;
                            }

                            if (pageChangeChk)
                            {
                                if (dt.Rows[i][c].ToString().Contains("PCE") || dt.Rows[i][c].ToString().Contains("SET"))
                                {
                                    _itemUnitInt = c;
                                    _itemDescInt = 2;
                                    _itemCodeInt = 1;
                                }
                                else if (GetTo.IsInt(dt.Rows[i][c].ToString()))
                                {
                                    _itemQtInt = c;
                                    _itemDescInt2 = c + 1;
                                }
                            }
                        }

                        if (GetTo.IsInt(firstColString.Replace("-", "").Trim()) || GetTo.IsInt(secondColStr.Replace("-", "").Trim()))
                            {
                                if (GetTo.IsInt(firstColString.Replace("-", "").Trim()))
                                {
                                    firstNsec = 0;
                                }
                                else if (GetTo.IsInt(secondColStr.Replace("-", "").Trim()))
                                {
                                    firstNsec = 1;
                                }


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
                                    if (firstColString.Contains("-") || secondColStr.Contains("-"))
                                    {
                                        _itemCodeInt = Convert.ToInt16(rowValueSpl[1].ToString());
                                        _itemDescInt = Convert.ToInt16(rowValueSpl[2].ToString());
                                        _itemUnitInt = Convert.ToInt16(rowValueSpl[3].ToString());
                                        _itemQtInt = Convert.ToInt16(rowValueSpl[4].ToString());
                                        _itemRmk = -1;
                                    }
                                    else
                                    {
                                        _itemCodeInt = Convert.ToInt16(rowValueSpl[1].ToString());
                                        _itemDescInt = Convert.ToInt16(rowValueSpl[2].ToString());
                                        _itemUnitInt = Convert.ToInt16(rowValueSpl[3].ToString());
                                        _itemQtInt = Convert.ToInt16(rowValueSpl[4].ToString());
                                        //_itemUnitInt = Convert.ToInt16(rowValueSpl[5].ToString());
                                    }
                                }


                                //iTemNo = firstColString;
                                iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();
                                iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();

                                // 페이지가 이동하면 한칸씩 당겨짐
                                if (!GetTo.IsInt(iTemQt))
                                {
                                    iTemQt = dt.Rows[i][_itemQtInt - 1].ToString().Trim();
                                    iTemUnit = dt.Rows[i][_itemUnitInt - 1].ToString().Trim();

                                    // 변경된 컬럼 값 반영
                                    _itemDescInt2 = _itemDescInt2 - 1;
                                }

                                if (string.IsNullOrEmpty(iTemUnit) && !string.IsNullOrEmpty(iTemQt))
                                {
                                    iTemUnit = dt.Rows[i][_itemQtInt - 1].ToString().Trim();
                                }
                                else if (iTemUnit.Contains("/") || iTemUnit.Contains("(") || iTemUnit.Contains("."))
                                {
                                    descStr = iTemUnit.Trim();

                                    iTemUnit = dt.Rows[i][_itemQtInt - 1].ToString().Trim();
                                }


                                iTemSUBJ = subjectString + Environment.NewLine;

                                if (!string.IsNullOrEmpty(makerString))
                                    iTemSUBJ = iTemSUBJ + "MAKER: " + makerString + Environment.NewLine;

                                if (!string.IsNullOrEmpty(modelString))
                                {
                                    string _model = modelString.Replace("No", "").Replace("no", "").Replace("Details", "").Replace("Model", "").Trim();
                                    iTemSUBJ = iTemSUBJ + "MODEL: " + _model.Trim() + Environment.NewLine;

                                    serialString = serialString.Replace(_model, "").Replace("Model", "").Replace("no", "").Replace("No", "").Trim();
                                }

                                if (!string.IsNullOrEmpty(serialString))
                                    iTemSUBJ = iTemSUBJ + "S/NO.: " + serialString.Replace("Model", "").Replace("no", "").Trim() + Environment.NewLine;




                                // 품목명 여러칸에 있을 경우
                                int _i = i + 1;
                                if ((_itemUnitInt - _itemDescInt) != 1)
                                {
                                    string _itemdesc = string.Empty;
                                    iTemDESC = dt.Rows[i][_itemDescInt].ToString() + " " + dt.Rows[i][_itemDescInt + 1].ToString();
                                    _itemdesc = " " + dt.Rows[i][_itemDescInt2].ToString() + " " + dt.Rows[i][_itemDescInt2 + 1].ToString();
                                    iTemDESC = iTemDESC.Trim() + Environment.NewLine;

                                    while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                    {
                                        iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][_itemDescInt].ToString().Trim() + " " + dt.Rows[_i][_itemDescInt + 1].ToString().Trim();
                                        if (_itemDescInt2 + 1 <= dt.Columns.Count)
                                            _itemdesc = _itemdesc.Trim() + " " + dt.Rows[_i][_itemDescInt2].ToString().Trim() + " " + dt.Rows[_i][_itemDescInt2 + 1].ToString().Trim();
                                        _i += 1;
                                    }

                                    if (string.IsNullOrEmpty(descStr))
                                        iTemDESC = iTemDESC.Trim() + " " + _itemdesc.Trim();
                                    else
                                        iTemDESC = iTemDESC.Trim() + " " + descStr + " " + _itemdesc.Trim();
                                }
                                else
                                {
                                    string _itemdesc = string.Empty;

                                    iTemDESC = dt.Rows[i][_itemDescInt].ToString();
                                    _itemdesc = dt.Rows[i][_itemDescInt2].ToString();
                                    iTemDESC = iTemDESC + Environment.NewLine;

                                    while (string.IsNullOrEmpty(dt.Rows[_i][firstNsec].ToString()))
                                    {
                                        iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][_itemDescInt].ToString().Trim();
                                        if (_itemDescInt2 + 1 <= dt.Columns.Count)
                                            _itemdesc = _itemdesc.Trim() + " " + dt.Rows[_i][_itemDescInt2].ToString().Trim();
                                        _i += 1;
                                    }

                                    if (string.IsNullOrEmpty(descStr))
                                        iTemDESC = iTemDESC.Trim() + " " + _itemdesc.Trim();
                                    else
                                        iTemDESC = iTemDESC.Trim() + " " + descStr + " " + _itemdesc.Trim();
                                }

                                if (iTemDESC.Contains("PCE") || iTemDESC.Contains("PCS"))
                                {
                                    iTemDESC = iTemDESC.Replace("PCE", "").Replace("PCS", "").Trim();
                                }

                                // 아이템들이 한칸씩 앞으로 올수있는 상황, 품목명 자리에 단위 + 수량이 들어감
                                if (iTemDESC.Contains("PCE") || iTemDESC.Contains("PCS"))
                                {
                                    int _j = i + 1;

                                    string _itemdesc = string.Empty;

                                    iTemDESC = dt.Rows[i][_itemDescInt - 1].ToString().Trim();
                                    _itemdesc = dt.Rows[i][_itemDescInt2 - 2].ToString().Trim();
                                    iTemDESC = iTemDESC + Environment.NewLine;

                                    while (string.IsNullOrEmpty(dt.Rows[_j][0].ToString()))
                                    {
                                        iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_j][_itemDescInt - 1].ToString().Trim();
                                        if (_itemDescInt2 - 1 <= dt.Columns.Count)
                                            _itemdesc = _itemdesc.Trim() + " " + dt.Rows[_j][_itemDescInt2].ToString().Trim();
                                        _j += 1;
                                    }

                                    if(string.IsNullOrEmpty(descStr))
                                        iTemDESC = iTemDESC.Trim() + " " + _itemdesc.Trim();
                                    else
                                        iTemDESC = iTemDESC.Trim() + " " + descStr + " " + _itemdesc.Trim();
                                }




                                int __i = i + 1;
                                if (_itemCodeInt != -1)
                                {

                                    iTemCode = dt.Rows[i][_itemCodeInt].ToString().Replace("DWG.", "").Replace("DWG.NO.", "").Trim();
                                    if (iTemCode.Contains("DWG"))
                                    {
                                        dwgString = dt.Rows[i][_itemCodeInt].ToString().Replace("DWG.", "").Replace("DWG.NO.", "").Trim();
                                    }

                                    while (string.IsNullOrEmpty(dt.Rows[__i][firstNsec].ToString()))
                                    {
                                        iTemCode = iTemCode.Trim() + dt.Rows[__i][_itemCodeInt].ToString().Replace("DWG.NO.", "").Replace("DWG.", "").Trim();
                                        __i += 1;
                                    }
                                }

                                
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + maker.Trim();
                                iTemSUBJ = iTemSUBJ.Trim();


                                if (!string.IsNullOrEmpty(iTemCode) && iTemCode.Contains("DWG"))
                                    iTemDESC = iTemDESC.Trim() + " DWG NO: " + dwgString;

                                iTemCode = iTemCode.Replace("NO.", "").Replace("na", "").Replace("NA", "").Trim();



                                //ITEM ADD START
                                dtItem.Rows.Add();
                                dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                                dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                                dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                                if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                                if (!string.IsNullOrEmpty(iTemSUBJ))
                                    dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                                dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                                iTemDESC = string.Empty;
                                iTemUnit = string.Empty;
                                iTemQt = string.Empty;
                                iTemCode = string.Empty;
                                maker = string.Empty;
                                descStr = string.Empty;

                                pageChangeChk = false;
                            }
                    }
                }
            }
        }
    }
}

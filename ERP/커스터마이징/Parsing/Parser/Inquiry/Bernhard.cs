using System;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    public class Bernhard
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

        #endregion



        #region ==================================================================================================== Constructor

        public Bernhard(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호

            dtItem = new DataTable();
            dtItem.Columns.Add("NO");           // 아이템 순번
            dtItem.Columns.Add("SUBJ");         // 주제
            dtItem.Columns.Add("ITEM");         // 품목코드
            dtItem.Columns.Add("DESC");         // 품목명
            dtItem.Columns.Add("UNIT");         // 단위
            dtItem.Columns.Add("QT");           // 수량
            dtItem.Columns.Add("UNIQ");         // 선사코드
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
            string iTemUniq = string.Empty;

            int _itemCode = 0;
            int _itemDescription = 0;
            int _itemDrw = 0;
            int _itemPart = 0;
            int _itemUnit = 0;
            int _itemQt = 0;
            int _itemRemark = 0;

            int equiprowno = 0;
            int equipmentno = 0;
            int modelno = 0;
            int makerno = 0;
            int drawingno = 0;
            int partno = 0;

            string equpmentstring = string.Empty;
            string modelstring = string.Empty;
            string makerstring = string.Empty;
            string drawingstring = string.Empty;
            string partstring = string.Empty;
            string subjectString = string.Empty;

            string endRemarkString = string.Empty;

            bool subjStatus = false;

            bool sbCheck = false;


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


            // 시작
            bool itemStart = false;

            string[] itemName = { };

            foreach (DataTable dt in ds.Tables)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        string firstColString = dt.Rows[i][0].ToString();
                        string secondColStr = dt.Rows[i][1].ToString();


                        //"Item Details" 나오면 항목 추가 시작
                        if (firstColString == "Item Details") itemStart = true;	// 아이템 추가 모드 시작

                        // ********** 문서 검색 모드
                        if (!itemStart)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                string text = dt.Rows[i][j].ToString();
                                // #################### 선명 ####################
                                if (text.IndexOf("Name") >= 0)
                                {
                                    if (j > 0)
                                    {
                                        if (dt.Rows[i][j - 1].ToString().Equals("Vessel"))
                                        {
                                            vessel = dt.Rows[i][j + 1].ToString().Trim();
                                        }
                                    }
                                }
                                // #################### 문의번호 ####################
                                else if (text.IndexOf("Enquiry Number") >= 0)
                                {
                                    reference = dt.Rows[i][j + 1].ToString().Trim();

                                    if (string.IsNullOrEmpty(reference))
                                    {
                                        reference = dt.Rows[i][j + 2].ToString().Trim();
                                    }
                                }
                            }
                        }
                        // ********** 아이템 추가 모드
                        else
                        {
                            int rowsCount = dt.Rows.Count - 1;
                            int columsCount = dt.Columns.Count - 1;

                            // 아이템 시작은 최소 11부터 대부분 14?
                            if (i >= 11)
                            {
                                // 첫번째 컬럼 값 가져오기
                                firstColString = dt.Rows[i][0].ToString(); // 순번은 확정

                                if (!subjStatus)
                                {
                                    for (int n = 0; n < rowsCount; n++)
                                    {
                                        if (dt.Rows[n][0].ToString().Contains("Remarks"))
                                        {
                                            int m = n;
                                            while (m < rowsCount)
                                            {
                                                for (int c = 1; c < columsCount; c++)
                                                {
                                                    subjectString = subjectString.Trim() + " " + dt.Rows[m][c].ToString().Trim();
                                                }
                                                m += 1;

                                            }
                                            subjStatus = true;
                                        }
                                    }
                                }

                                if (firstColString.Equals("Remarks To Vendor :")) break;	// 종료


                                // 컬럼명을 통해서 컬럼 위치 확인
                                if (firstColString == "S.No")
                                {
                                    for (int j = 0; j < dt.Columns.Count; j++)
                                    {
                                        if (dt.Rows[i][j].ToString() == "Item Code") _itemCode = j;                          //선사코드
                                        else if (dt.Rows[i][j].ToString() == "Item Description") _itemDescription = j;       //품목명
                                        else if (dt.Rows[i][j].ToString().Contains("Drawing N")) _itemDrw = j;                 //DWG NO
                                        else if (dt.Rows[i][j].ToString() == "Part Number") _itemPart = j;                   //품목코드
                                        else if (dt.Rows[i][j].ToString() == "Unit" || dt.Rows[i][j].ToString().Equals("UOM")) _itemUnit = j;                          //단위
                                        else if (dt.Rows[i][j].ToString() == "Quantity") _itemQt = j;                    //수량
                                        else if (dt.Rows[i][j].ToString() == "Remarks") _itemRemark = j;                     //비고
                                    }
                                }




                                if ((GetTo.IsInt(firstColString) || GetTo.IsInt(secondColStr)))
                                {
                                    if (!string.IsNullOrEmpty(firstColString) && secondColStr.Length <= 16)
                                    {
                                        //iTemNo = firstColString;
                                        iTemUniq = dt.Rows[i][_itemCode].ToString().Trim();

                                        if (sbCheck)
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


                                            if (rowValueSpl[7] != null)
                                            {
                                                _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                                                _itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
                                                _itemDrw = Convert.ToInt16(rowValueSpl[3].ToString());
                                                _itemPart = Convert.ToInt16(rowValueSpl[3].ToString());
                                                _itemUnit = Convert.ToInt16(rowValueSpl[5].ToString());
                                                _itemQt = Convert.ToInt16(rowValueSpl[6].ToString());
                                            }
                                            else if (rowValueSpl[6] != null)
                                            {
                                                _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                                                _itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
                                                _itemDrw = Convert.ToInt16(rowValueSpl[3].ToString());
                                                _itemPart = Convert.ToInt16(rowValueSpl[3].ToString());
                                                _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
                                                _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                                                _itemRemark = Convert.ToInt16(rowValueSpl[6].ToString());
                                            }
                                            else if ((rowValueSpl[5] != null))
                                            {
                                                _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                                                _itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
                                                _itemPart = Convert.ToInt16(rowValueSpl[3].ToString());
                                                _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
                                                _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                                            }
                                            //else if (rowValueSpl[5] == null && rowValueSpl[4] != null)
                                            //{
                                            //    _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                                            //    //_itemQtInt = Convert.ToInt16(rowValueSpl[2].ToString());
                                            //    //_itemUnitInt = Convert.ToInt16(rowValueSpl[3].ToString());
                                            //    //_itemDescInt = Convert.ToInt16(rowValueSpl[4].ToString());
                                            //}

                                            _itemDrw = _itemPart - 1;
                                        }


                                        int wk = i;
                                        // while ? for ? 반복되는 i를 다른 변수를 사용해서, 돌려야함. 여기 안에서 테이블 add까지 끝내는걸로 수정
                                        while (!GetTo.IsInt(dt.Rows[wk + 1][0].ToString()) || dt.Rows[wk][0].ToString().Equals(firstColString))
                                        {
                                            // 품목명 컬럼 확인
                                            for (int j = 0; j < dt.Columns.Count; j++)
                                            {
                                                int _i = wk + 1;
                                                if (_i < dt.Rows.Count)
                                                {
                                                    if (_i >= dt.Rows.Count)
                                                        break;

                                                    while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                                    {
                                                        if (dt.Rows[_i][j].ToString().Contains("Equipment Name"))
                                                        {
                                                            equiprowno = _i;
                                                            equipmentno = j;
                                                        }
                                                        else if (dt.Rows[_i][j].ToString().Contains("Model"))
                                                        {
                                                            modelno = j;
                                                        }
                                                        else if (dt.Rows[_i][j].ToString().Contains("Maker"))
                                                        {
                                                            makerno = j;
                                                        }
                                                        else if (dt.Rows[_i][j].ToString().Contains("Drawing Number"))
                                                        {
                                                            drawingno = j;
                                                        }
                                                        else if (dt.Rows[_i][j].ToString().Contains("Part Number"))
                                                        {
                                                            partno = j;
                                                        }

                                                        _i += 1;

                                                        if (_i >= dt.Rows.Count)
                                                            break;
                                                    }

                                                    
                                                }
                                            }


                                            if (equiprowno == wk)
                                            {
                                                if (equipmentno != 0)
                                                {
                                                    int _i = wk + 1;
                                                    while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                                    {
                                                        equpmentstring = equpmentstring.Trim() + " " + dt.Rows[_i][equipmentno].ToString().Trim();
                                                        _i += 1;
                                                    }
                                                }

                                                if (modelno != 0)
                                                {

                                                    modelstring = dt.Rows[wk + 1][modelno].ToString().Trim();
                                                    int _i = wk + 2;
                                                    while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                                    {
                                                        modelstring = modelstring.Trim() + " " + dt.Rows[_i][modelno].ToString().Trim();
                                                        _i += 1;
                                                    }
                                                }

                                                if (makerno != 0)
                                                {
                                                    int _i = wk + 1;
                                                    while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                                    {
                                                        makerstring = makerstring.Trim() + " " + dt.Rows[_i][makerno].ToString().Trim();
                                                        _i += 1;
                                                    }
                                                }

                                                if (drawingno != 0)
                                                {
                                                    int _i = wk + 1;
                                                    drawingstring = dt.Rows[_i][drawingno].ToString().Trim();
                                                }

                                                if (partno != 0)
                                                {
                                                    int _i = wk + 1;
                                                    partstring = dt.Rows[_i][partno].ToString().Trim();
                                                }
                                            }

                                           

                                            wk += 1;

                                            if (wk+1 >= dt.Rows.Count)
                                                break;


                                        }

                                        if (_itemQt != 0)
                                        {
                                            if (!string.IsNullOrEmpty(dt.Rows[i][_itemQt].ToString()))
                                            {
                                                iTemQt = dt.Rows[i][_itemQt].ToString();             // 수량
                                            }
                                            else
                                            {
                                                if (_itemQt + 1 < dt.Columns.Count)
                                                    iTemQt = dt.Rows[i][_itemQt + 1].ToString();
                                            }

                                            if (!GetTo.IsInt(iTemQt.Replace(".", "")))
                                            {
                                                if (_itemQt + 1 < dt.Columns.Count)
                                                {
                                                    iTemQt = dt.Rows[i][_itemQt + 1].ToString().Trim();

                                                    if (string.IsNullOrEmpty(iTemQt) || !GetTo.IsInt(iTemQt))
                                                    {
                                                        iTemQt = dt.Rows[i][_itemQt - 1].ToString().Trim();
                                                        _itemQt = _itemQt - 1;
                                                    }
                                                }

												if (_itemQt - 1 < dt.Columns.Count)
												{
													iTemUnit = dt.Rows[i][_itemQt - 1].ToString().Trim();
													if (string.IsNullOrEmpty(iTemUnit) || iTemUnit.Length > 5)
													{
														iTemUnit = dt.Rows[i][_itemUnit - 1].ToString().Trim();

														if (string.IsNullOrEmpty(iTemUnit))
														{
															iTemUnit = dt.Rows[i][_itemUnit - 2].ToString().Trim();
														}
													}
												}

												if (!GetTo.IsInt(iTemQt.Replace(".", "")))
                                                {
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


                                                    if (rowValueSpl[7] != null)
                                                    {
                                                        _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                                                        _itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
                                                        _itemDrw = Convert.ToInt16(rowValueSpl[4].ToString());
                                                        _itemPart = Convert.ToInt16(rowValueSpl[5].ToString());
                                                        _itemUnit = Convert.ToInt16(rowValueSpl[6].ToString());
                                                        _itemQt = Convert.ToInt16(rowValueSpl[7].ToString());
                                                    }
                                                    else if (rowValueSpl[6] != null)
                                                    {
                                                        _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                                                        _itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
                                                        _itemDrw = Convert.ToInt16(rowValueSpl[4].ToString());
                                                        _itemPart = Convert.ToInt16(rowValueSpl[4].ToString());
                                                        _itemUnit = Convert.ToInt16(rowValueSpl[5].ToString());
                                                        _itemQt = Convert.ToInt16(rowValueSpl[6].ToString());
                                                    }
                                                    else if ((rowValueSpl[5] != null))
                                                    {
                                                        _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                                                        _itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
                                                        _itemPart = Convert.ToInt16(rowValueSpl[3].ToString());
                                                        _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
                                                        _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                                                    }


                                                    iTemQt = dt.Rows[i][_itemQt].ToString();
                                                }
                                            }
                                        }

                                        if (string.IsNullOrEmpty(iTemQt))
                                            iTemQt = dt.Rows[i][_itemQt].ToString();


                                        if (!GetTo.IsInt(iTemQt.Replace(".", "")))
                                        {
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


                                            if (rowValueSpl[7] != null)
                                            {
                                                _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                                                _itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
                                                _itemDrw = Convert.ToInt16(rowValueSpl[3].ToString());
                                                _itemPart = Convert.ToInt16(rowValueSpl[3].ToString());
                                                _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
                                                _itemQt = Convert.ToInt16(rowValueSpl[6].ToString());
                                            }
                                            else if (rowValueSpl[6] != null)
                                            {
                                                _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                                                _itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
                                                _itemDrw = Convert.ToInt16(rowValueSpl[4].ToString());
                                                _itemPart = Convert.ToInt16(rowValueSpl[4].ToString());
                                                _itemUnit = Convert.ToInt16(rowValueSpl[5].ToString());
                                                _itemQt = Convert.ToInt16(rowValueSpl[6].ToString());
                                            }
                                            else if ((rowValueSpl[5] != null))
                                            {
                                                _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                                                _itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
                                                _itemPart = Convert.ToInt16(rowValueSpl[3].ToString());
                                                _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
                                                _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                                            }

                                            iTemQt = dt.Rows[i][_itemQt].ToString();
                                        }



                                        if (_itemUnit != 0 && string.IsNullOrEmpty(iTemUnit))
                                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();             // 단위

                                        if (_itemPart != 0)
                                            iTemCode = dt.Rows[i][_itemPart].ToString().Replace("Item", "").Replace("KERGER", "").Trim();         // 품목코드


                                        //숫자 아닐 경우 공백으로..
                                        double num;
                                        bool isNum = double.TryParse(iTemQt, out num);
                                        if (!isNum)
                                        {
                                            iTemQt = "";
                                        }

                                        // #################### 품목명####################
                                        int _itemdescInt = i + 1;
                                        for (int c = _itemDescription; c < _itemPart; c++)
                                        {
                                            iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                                        }

                                        while (string.IsNullOrEmpty(dt.Rows[_itemdescInt][0].ToString()))
                                        {
                                            if (dt.Rows[_itemdescInt][_itemDescription].ToString().Contains("Equipment Name") || dt.Rows[_itemdescInt][_itemDescription].ToString().Equals("Model") || string.IsNullOrEmpty(dt.Rows[_itemdescInt][_itemDescription].ToString()))
                                                break;

                                            for (int c = _itemCode + 1; c < _itemPart; c++)
                                            {
                                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_itemdescInt][c].ToString().Trim();
                                            }

                                            _itemdescInt += 1;

                                            if (_itemdescInt >= dt.Rows.Count)
                                                break;
                                        }

                                        string[] equString = equpmentstring.Split('-');
                                        equpmentstring = string.Empty;
                                        for (int eqC = 1; eqC < equString.Length; eqC++)
                                        {
                                            equpmentstring = equpmentstring + "-" + equString[eqC].ToString();
                                        }

                                        if (equpmentstring.StartsWith("-"))
                                        {
                                            equpmentstring = equpmentstring.Trim();
                                            equpmentstring = equpmentstring.Substring(1, equpmentstring.Length - 1).Trim();
                                        }

                                        if (_itemRemark != -1)
                                        {
                                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[i][_itemRemark].ToString().Trim();

                                            int _i = i+1;

                                            while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                            {
                                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][_itemRemark].ToString();

                                                _i += 1;

                                                if (_i >= dt.Rows.Count)
                                                    break;
                                            }

                                            //if (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                            //    iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][_itemRemark].ToString();
                                        }


                                        // #################### 주제 ####################
                                        if (string.IsNullOrEmpty(equpmentstring))
                                            iTemSUBJ = subjectString.Trim();
                                        else
                                            iTemSUBJ = equpmentstring.Trim();

                                        makerstring = makerstring.Trim();
                                        modelstring = modelstring.Trim();


                                        if (!string.IsNullOrEmpty(makerstring))
                                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerstring;

                                        if (!string.IsNullOrEmpty(modelstring))
                                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelstring;

                                        if (!string.IsNullOrEmpty(drawingstring))
                                        {
                                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG.NO: " + drawingstring;
                                            //sbCheck = true;
                                        }
                                        else
                                            sbCheck = true;

                                        iTemSUBJ = iTemSUBJ.Replace("See attached files.", "");
                                        iTemSUBJ = iTemSUBJ.Replace("Need for stock replenish.", "");
                                        iTemSUBJ = iTemSUBJ.Replace("see the attached files", "");


                                        dtItem.Rows.Add();
                                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColString;

                                        if (!string.IsNullOrEmpty(iTemSUBJ))
                                        {
                                            if (!(iTemSUBJ.ToUpper().IndexOf("FOR") == 0))
                                                dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                                            else
                                                dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
                                        }

                                        iTemDESC = iTemDESC.Trim();


                                        // 아이템 코드, 아이템 단위 안딸려 나오는거 수정해야함. 
                                        // 위에 컬럼 나눌때 잘 못 됨 (경우의 수가 생겼음) 양현정 SB20010522


                                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;// 품목명
                                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);//PCS 안됨
                                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = "";

                                        iTemSUBJ = string.Empty;
                                        iTemCode = string.Empty;
                                        iTemDESC = string.Empty;
                                        iTemUnit = string.Empty;
                                        iTemQt = string.Empty;

                                        makerstring = string.Empty;
                                        modelstring = string.Empty;
                                        drawingstring = string.Empty;
                                        partstring = string.Empty;
                                        equpmentstring = string.Empty;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Util.ShowMessage(Util.GetErrorMessage(ex.Message));
                    }
                }
            }
        }

    }
}

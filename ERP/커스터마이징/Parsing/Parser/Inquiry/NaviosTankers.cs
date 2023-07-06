using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class NaviosTankers
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



        public NaviosTankers(string fileName)
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
            int _itemQt = 0;
            int _itemDescription = 0;
            int _itemCode = 0;
            int _itemposno = -1;

            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;


            string makerString = string.Empty;
            string typeString = string.Empty;
            string modelString = string.Empty;
            string serialString = string.Empty;
            string subjFirst = string.Empty;
            string subjEnd = string.Empty;
            string subjEnd2 = string.Empty;
            string posnoDesc = string.Empty;

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

            string[] itemName = { };

            bool itemStart = false;


            // 품목명 수정 해야함 두줄 세줄이 될 수도 있음.
            foreach (DataTable dt in ds.Tables)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dataValue = dt.Rows[i][0].ToString();
                    string dataSecValue = dt.Rows[i][1].ToString();

                    if (dataValue.Contains("#")) 
                        itemStart = true;

                    if (dataValue.Contains("Please include in your offer"))
                        break;

                    if (!itemStart)
                    {
                        // #### 선명, 문의번호
                        if (dataSecValue.Contains("VESSEL"))
                        {
                            vessel = dataSecValue.Replace("VESSEL", "").Replace(":", "").Trim();
                            reference = dt.Rows[i - 4][3].ToString().Replace("REF", "").Replace(":", "").Trim() + dt.Rows[i - 4][4].ToString().Replace("REF", "").Replace(":", "").Trim() + dt.Rows[i - 4][5].ToString().Replace("REF", "").Replace(":", "").Trim();
                        }

                        if (dataValue.Contains("Machinery"))
                        {
                            subjFirst = dt.Rows[i][0].ToString().Replace("Machinery", "").Replace(":", "").Trim();

                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (dt.Rows[i][j].ToString().Contains("Type"))
                                {
                                    typeString = dt.Rows[i][j].ToString().Replace("Type", "").Replace(":", "").Trim();
                                }

                                if (dt.Rows[i][j].ToString().Contains("Serial"))
                                {
                                    serialString = dt.Rows[i][j].ToString().Replace("Serial No", "").Replace(":", "").Trim();
                                }
                            }

                        }

                        if (dataValue.ToLower().Contains("maker"))
                        {
                            makerString = dt.Rows[i][0].ToString().ToLower().Replace("maker", "").Replace(":", "").Trim();

                            if (string.IsNullOrEmpty(makerString))
                            {
                                if (!dt.Rows[i][1].ToString().ToLower().Contains("model"))
                                {
                                    makerString = dt.Rows[i][1].ToString().ToLower().Replace("maker", "").Replace(":", "").Trim();
                                }
                                makerString = makerString.Trim();
                            }

                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (dt.Rows[i][j].ToString().ToLower().Contains("model"))
                                {
                                    modelString = dt.Rows[i][j].ToString().ToLower().Replace("model", "").Replace(":", "").Trim();
                                }

                                modelString = modelString.Trim();
                            }

                            makerString = makerString.Trim();
                        }

                        if (dataValue.ToLower().Contains("s/n"))
                        {
                            if (string.IsNullOrEmpty(serialString))
                            {
                                serialString = dt.Rows[i][0].ToString().ToLower().Replace("s/n", "").Replace(":", "").Trim();

                                serialString = serialString.Trim();
                            }
                        }
                    }
                    else
                    {
                        //################# 순번, 주제, 품목코드, 품목명, 단위, 수량
                        bool itemNoStart = false;
                        string firstColString = dt.Rows[i][0].ToString();
                        string _firstColString = string.Empty;
                        if (firstColString.Equals("#"))
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (dt.Rows[i][j].ToString().Contains("Qnty")) _itemQt = j;                     //수량
                                else if (dt.Rows[i][j].ToString().Contains("Description")) _itemDescription = j;         //제목
                                else if (dt.Rows[i][j].ToString().Contains("Part No")) _itemCode = j;                  //코드
                                else if (dt.Rows[i][j].ToString().Contains("Pos No")) _itemposno = j;                   // POS NO
                            }

                            if (dt.Rows[i + 1][0].ToString().Length != 2)
                            {
                                subjEnd2 = dt.Rows[i + 1][0].ToString();
                            }
                        }


                        // 아이템 시작 번호:  1) 형식임. 
                        if (!string.IsNullOrEmpty(firstColString) && firstColString.Contains(")"))
                        {
                            _firstColString = firstColString.Substring(0, 1);

                            if (GetTo.IsInt(_firstColString))
                                itemNoStart = true;
                            else
                                itemNoStart = false;
                        }


                        //if (firstColString.Length == 2)
                        if (itemNoStart)
                        {
                            string _firstcolstring = firstColString.Substring(0, 1);

                            if (GetTo.IsInt(_firstcolstring))
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
                                    _itemDescription = Convert.ToInt16(rowValueSpl[1].ToString());
                                    _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                                    _itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
                                }
                                else if (rowValueSpl[5] != null && rowValueSpl[6] == null)
                                {
                                    _itemDescription = Convert.ToInt16(rowValueSpl[1].ToString());
                                    _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                                    _itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
                                }


                                iTemNo = _firstcolstring;
                                iTemCode = dt.Rows[i][_itemCode].ToString().Trim();
                                iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                                iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Trim();

                                if (!string.IsNullOrEmpty(firstColString) && firstColString.Contains(")") && !string.IsNullOrEmpty(dt.Rows[i - 1][0].ToString()))
                                {
                                    string _checkInt = dt.Rows[i - 1][0].ToString().Substring(0, 1);

                                    if (!string.IsNullOrEmpty(_checkInt) && !GetTo.IsInt(_checkInt))
                                    {
                                        string doubleCheck = dt.Rows[i - 1][0].ToString().Trim() + " " + dt.Rows[i - 1][1].ToString().Trim();
                                        doubleCheck = doubleCheck.Trim();

                                        
                                        subjEnd = string.Empty;

                                        if (!subjEnd2.Contains(doubleCheck))
                                            subjEnd = subjEnd2.Trim() + " " + dt.Rows[i - 1][0].ToString().Trim() + " " + dt.Rows[i - 1][1].ToString().Trim();
                                        else
                                            subjEnd = subjEnd2.Trim();

                                        subjEnd2 = string.Empty;
                                    }
                                }


                                if (_itemposno > -1)
                                    posnoDesc = dt.Rows[i][_itemposno].ToString();

                                int _i = i + 1;

                                if (dt.Rows[i][0].ToString().Length > 4)
                                {
                                    //_i = i + 1;
                                    for (int cc = 0; cc < _itemCode; cc++)
                                    {
                                        iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][cc].ToString().Trim();
                                    }

                                    while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                    {
                                        for (int c = 0; c < _itemCode; c++)
                                        {
                                            iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                        }

                                        //iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
                                        _i += 1;
                                    }
                                }
                                else
                                {
                                    for (int c = _itemDescription; c < _itemCode; c++)
                                    {
                                        iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();

                                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                        {
                                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
                                            _i += 1;
                                        }
                                    }
                                }


                                if (string.IsNullOrEmpty(iTemQt))
                                {
                                    iTemQt = dt.Rows[i][_itemQt + 2].ToString().Trim();
                                    iTemUnit = dt.Rows[i][_itemQt + 3].ToString().Trim();
                                }


                                //iTemSUBJ = "FOR " + subjFirst + Environment.NewLine + "TYPE: " + typeString + Environment.NewLine + "MAKER: " + makerString + Environment.NewLine + "S/N: " + serialString + Environment.NewLine + subjEnd;

                                iTemSUBJ = subjFirst.Trim();

                                if (!string.IsNullOrEmpty(typeString))
                                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeString;

                                if (!string.IsNullOrEmpty(makerString))
                                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerString;

                                if (!string.IsNullOrEmpty(serialString))
                                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/N: " + serialString;

                                if (!string.IsNullOrEmpty(modelString))
                                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelString;

                                if (!string.IsNullOrEmpty(subjEnd))
                                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjEnd;

                                iTemSUBJ = iTemSUBJ.Trim();

                                if (!string.IsNullOrEmpty(posnoDesc))
                                    iTemDESC = iTemDESC.Trim() + Environment.NewLine + "POS NO: " + posnoDesc;

                                string[] firstDescValue = iTemDESC.Split(' ');


                                if (!string.IsNullOrEmpty(firstDescValue[0].ToString()))
                                {
                                    if (GetTo.IsInt(firstDescValue[0].ToString().Substring(0, 1)))
                                    {
                                        iTemDESC = iTemDESC.Replace(firstDescValue[0], "").Trim();
                                    }
                                    else
                                    {
                                        iTemDESC = iTemDESC.Replace(iTemNo + ")", "").Trim();
                                    }
                                }
                                else
                                {
                                    iTemDESC = iTemDESC.Replace(iTemNo + ")", "").Trim();
                                }

                                //ITEM ADD START
                                dtItem.Rows.Add();
                                dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                                dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                                dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                                if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                                if (!string.IsNullOrEmpty(iTemSUBJ))
                                    dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                                dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                                iTemDESC = string.Empty;
                                iTemUnit = string.Empty;
                                iTemQt = string.Empty;
                                iTemCode = string.Empty;
                                iTemSUBJ = string.Empty;

                                posnoDesc = string.Empty;
                            }
                        }
                    }
                }
            }
        }
    }
}

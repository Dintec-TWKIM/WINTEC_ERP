using System;
using System.Data;
using System.Text.RegularExpressions;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    public class ConsolidatedMarine
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



        public ConsolidatedMarine(string fileName)
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
            int shipLineNum = 0;

            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            string[] sNoValue = new string[500];
            int[] sNoendValue = new int[500];
            int valueCount = 0;

            int _itemUnit = -1;
            int _itemQt = -1;


            string compoStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;


            int firstColInt = 0;

            // 끝 맺음말 확인 :  Best Regards
            int itemSubjEnd = 0;

            string subjStr = string.Empty;


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


            foreach (DataTable dt in ds.Tables)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dataValue = dt.Rows[i][0].ToString();

                    if (dataValue.Contains("Item")) itemStart = true;

                    if (!itemStart)
                    {
                        //################# 선명, 문의번호
                        if (dataValue.Contains("SHIP:"))
                        {
                            vessel = dataValue.Replace("SHIP:", "").Replace("M/T", "").Replace("LPG/C", "").Trim();
                            shipLineNum = i;

                            string enqNo = string.Empty;

                            for (int j = 1; j < dt.Columns.Count; j++)
                            {
                                enqNo = dt.Rows[shipLineNum][j].ToString();

                                if (enqNo.Contains("ENQ"))
                                {
                                    reference = enqNo.Replace("ENQ. No.:", "").Trim();
                                }
                            }

                            vessel = vessel.Trim();
                        }
                    }
                    else
                    {
                        //################# 순번, 주제, 품목코드, 품목명, 단위, 수량

                        string firstColString = dt.Rows[i][0].ToString();

                        if (sNoValue[0] == null && itemSubjEnd == 0)
                        {
                            for (int k = i; k < dt.Rows.Count; k++)
                            {
                                if (dt.Rows[k][0].ToString().Contains("Best Regards"))
                                {
                                    itemSubjEnd = k;
                                }

                                //아이템 갯수 확인
                                double Num;
                                bool isNum = double.TryParse(dt.Rows[k][0].ToString(), out Num);

                                if (isNum)
                                {
                                    sNoValue[valueCount] = dt.Rows[k][0].ToString();
                                    sNoendValue[valueCount] = k;
                                    valueCount = valueCount + 1;
                                }

                            }
                        }


                        if (firstColString.StartsWith("FOR COMPONENT"))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                compoStr = compoStr.Trim() + dt.Rows[i][c].ToString().Trim();
                            }
                        }
                        else if (firstColString.StartsWith("MANUFACTURER"))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                makerStr = makerStr.Trim() + dt.Rows[i][c].ToString().Trim();
                            }
                        }
                        else if (firstColString.StartsWith("TYPE"))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                typeStr = typeStr.Trim() + dt.Rows[i][c].ToString().Trim();
                            }
                        }
                        else if (firstColString.StartsWith("SERIAL NO"))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                serialStr = serialStr.Trim() + dt.Rows[i][c].ToString().Trim();
                            }
                        }
                        // 항목 갯수 만큼 ROW 추가
                        else if (firstColString.StartsWith("Item Description"))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().StartsWith("Unit")) _itemUnit = c;
                                //else if (dt.Rows[i][c].ToString().StartsWith("Quantity")) _itemQt = c;
                            }
                        }
                        
                        
                        if (GetTo.IsInt(firstColString))
                        {
                            // row 값 가져와서 배열에 넣은후 값 추가하기
                            string[] rowValueSpl = new string[20];
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                                {
                                    rowValueSpl[c] = c.ToString();
                                }
                            }


                            if (rowValueSpl[3] != null && rowValueSpl[4] == null)
                            {
                                _itemUnit = Convert.ToInt16(rowValueSpl[2].ToString());
                                _itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
                            }


                            double NumDetec;
                            bool isNumDetec = double.TryParse(dt.Rows[i][0].ToString(), out NumDetec);

                            firstColInt = Convert.ToInt16(firstColString);


                            // 새로운 아이템 항목이 시작할 때, 추가
                            if (isNumDetec)
                            {
                                if (!_itemQt.Equals(-1))
                                    iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                                if (!_itemUnit.Equals(-1))
                                {
                                    iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();
                                    iTemQt = dt.Rows[i][_itemUnit + 1].ToString().Trim();
                                    for (int c = 1; c < _itemUnit; c++)
                                    {
                                        iTemDESC = iTemDESC + dt.Rows[i][c].ToString();

                                        if (!GetTo.IsInt(dt.Rows[i + 1][0].ToString()) || string.IsNullOrEmpty(dt.Rows[i+1][0].ToString()))
                                            iTemCode = iTemCode.Trim() + " " + dt.Rows[i + 1][c].ToString().Trim(); 
                                    }

                                    iTemDESC = iTemDESC.Trim();

                                    iTemCode = iTemCode.Replace("Maker's Ref", "").Replace(":", "").Trim();
                                }


                                //int subjectColumnCount = 0;

                                //string _itemSubj = string.Empty;
                                //string _itemSubjSub = string.Empty;

                                //// ###SUBJECT   <---->   DESC    체인지!
                                //// 마지막 아이템의 경우에는 마지막 문구 BEST REGARD 앞까지 돌림.
                                //if (firstColInt == valueCount)
                                //{
                                //    for (int subjCount = sNoendValue[firstColInt - 1]; subjCount < itemSubjEnd - 1; subjCount++)
                                //    {
                                //        for (int scc = 0; scc <= subjectColumnCount; scc++)
                                //        {
                                //            _itemSubj = dt.Rows[subjCount][scc].ToString();

                                //            if (!string.IsNullOrEmpty(_itemSubj))
                                //                _itemSubjSub = _itemSubj.Substring(_itemSubj.Length - 1, 1);

                                //            if (!GetTo.IsInt(_itemSubj))
                                //            {
                                //                if (!_itemSubjSub.Equals(":"))
                                //                {
                                //                    if (_itemSubj.Contains("SECTION"))
                                //                    {
                                //                        iTemCode = dt.Rows[subjCount][scc].ToString();
                                //                    }
                                //                    else if (!_itemSubj.Contains("details") && !_itemSubj.Contains("Comment") && !_itemSubj.Contains("DETAILS") && !_itemSubj.Contains("SHIP:") && !_itemSubj.Contains("ENQ. No.:"))
                                //                    {
                                //                        iTemSUBJ = iTemSUBJ.Trim() + dt.Rows[subjCount][scc].ToString().Trim();
                                //                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine;
                                //                    }

                                //                    iTemCode = iTemCode.Replace("SECTION", "");
                                //                    iTemCode = iTemCode.Replace("Details:", "");

                                //                }
                                //            }
                                //        }
                                //        if (!string.IsNullOrEmpty(iTemSUBJ))
                                //            iTemSUBJ = iTemSUBJ + Environment.NewLine;
                                //    }
                                //}
                                //else
                                //{
                                //    for (int subjCount = sNoendValue[firstColInt - 1]; subjCount < sNoendValue[firstColInt]; subjCount++)
                                //    {
                                //        for (int scc = 0; scc <= subjectColumnCount; scc++)
                                //        {
                                //            _itemSubj = dt.Rows[subjCount][scc].ToString();

                                //            if (!string.IsNullOrEmpty(_itemSubj))
                                //                _itemSubjSub = _itemSubj.Substring(_itemSubj.Length - 1, 1);

                                //            if (!GetTo.IsInt(_itemSubj))
                                //            {
                                //                if (!_itemSubjSub.Equals(":"))
                                //                {
                                //                    if (_itemSubj.Contains("SECTION"))
                                //                    {
                                //                        iTemCode = dt.Rows[subjCount][scc].ToString().Trim();
                                //                    }
                                //                    else if (!_itemSubj.Contains("details") && !_itemSubj.Contains("Comment") && !_itemSubj.Contains("DETAILS") && !_itemSubj.Contains("SHIP:") && !_itemSubj.Contains("ENQ. No.:"))
                                //                    {
                                //                        iTemSUBJ = iTemSUBJ.Trim() + dt.Rows[subjCount][scc].ToString().Trim();
                                //                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine;
                                //                        iTemSUBJ = iTemSUBJ.Replace("Details:", "").Trim();
                                //                    }
                                //                    iTemCode = iTemCode.Replace("SECTION", "").Replace("Details:", "").Trim();
                                //                }
                                //            }
                                //        }
                                //        if (!string.IsNullOrEmpty(iTemSUBJ))
                                //            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine;
                                //    }
                                //}



                                int _i = i + 1;
                                while (!dt.Rows[_i][0].ToString().Contains("Best Regards") && !dt.Rows[_i][0].ToString().Contains("SHIP:") && !GetTo.IsInt(dt.Rows[_i][0].ToString()))
                                {
                                    for (int c = 0; c < dt.Columns.Count; c++)
                                    {
                                        subjStr = subjStr + " " + dt.Rows[_i][c].ToString().Trim();
                                    }
                                    _i += 1;

                                    subjStr = subjStr.Trim() + Environment.NewLine;

                                    if (_i >= dt.Rows.Count)
                                        break;
                                }


                                // 소숫점 숫자를 제외한 나머지 항목은 숫자만 가져오게 설정
                                if (!iTemQt.Contains("."))
                                {
                                    iTemQt = Regex.Replace(iTemQt, @"\D", "");
                                }


                                if (!string.IsNullOrEmpty(subjStr))
                                    iTemSUBJ = subjStr.Trim();

                                if (!string.IsNullOrEmpty(compoStr))
                                    iTemSUBJ = iTemSUBJ + Environment.NewLine + compoStr.Trim();

                                if (!string.IsNullOrEmpty(makerStr))
                                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                                if (!string.IsNullOrEmpty(typeStr))
                                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                                if (!string.IsNullOrEmpty(serialStr))
                                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();


                                //ITEM ADD START
                                dtItem.Rows.Add();
                                dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColInt;
                                dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                                dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                                if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                                if (!string.IsNullOrEmpty(iTemSUBJ))
                                    dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ.Trim();
                                dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                                iTemDESC = string.Empty;
                                iTemUnit = string.Empty;
                                iTemQt = string.Empty;
                                iTemCode = string.Empty;
                                iTemNo = string.Empty;

                                subjStr = string.Empty;
                            }
                        }
                    }
                }
            }
        }
    }
}

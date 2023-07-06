using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Premuda
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



        public Premuda(string fileName)
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

            string subjString = string.Empty;


            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemCode = -1;
            int _itemUnit = -1;

            string makerStr = string.Empty;
            string mainStr = string.Empty;
            string serialStr = string.Empty;
            string subsystemStr = string.Empty;
            string catStr = string.Empty;
            string partiStr = string.Empty;



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

                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("RFQ N°"))
                            {
                                reference = dt.Rows[i][c].ToString().Replace("RFQ N°", "").Replace(":", "").Trim();

                                if (dt.Rows[i][c + 1].ToString().ToUpper().StartsWith("SHIP"))
                                    vessel = dt.Rows[i][c + 1].ToString().Replace("SHIP", "").Replace(":", "").Trim();
                            }
                        }
                    }
                    else if (firstColString.Equals("Line"))
                    {
                        //QTY, PACKING, ITEM CODE, DESCRIPTION
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("QTY"))
                                _itemQt = c;

                            if (dt.Rows[i][c].ToString().Contains("REQ Number"))
                                _itemCode = c;

                            if (dt.Rows[i][c].ToString().Contains("UOM"))
                                _itemUnit = c;

                            if (dt.Rows[i][c].ToString().Contains("Maker"))
                                _itemDesc = c;
                        }
                    }
                    else if (firstColString.StartsWith("RFQ Notes"))
                    {
                        int _i = i + 1;

                        while (!dt.Rows[_i][0].ToString().Equals("RFQ") && !dt.Rows[_i][0].ToString().Equals("Line"))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                subjString = subjString.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
                            }

                            _i += 1;

                            if (_i > dt.Rows.Count)
                                break;
                        }
                    }
                    else if (GetTo.IsInt(firstColString))
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

                        if (rowValueSpl[3] != null && rowValueSpl[4] == null)
                        {
                            _itemQt = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[3].ToString());
                        }
                        else if (rowValueSpl[4] != null && rowValueSpl[5] == null)
                        {
                            _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
                        }
                      

                        if (!string.IsNullOrEmpty(subjString))
                            iTemSUBJ = subjString.Trim();


                        if (_itemDesc != -1)
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();
                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || dt.Rows[_i][0].ToString().Contains("Ship Line Notes") || dt.Rows[_i][0].ToString().Contains("Line Notes"))
                            {
                                for (int c = 0; c < dt.Columns.Count; c++)
                                {
                                    iTemDESC = iTemDESC + " " + dt.Rows[_i][c].ToString().Trim();
                                }

                                iTemDESC = iTemDESC.Trim() + Environment.NewLine;

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        if (string.IsNullOrEmpty(iTemCode))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (_itemCode != -1)
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (_itemUnit != -1)
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (_itemQt != -1)
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Replace(",",".").Trim();
                        }



                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemQt = string.Empty;
                        iTemUnit = string.Empty;
                        iTemDESC = string.Empty;
                        iTemSUBJ = string.Empty;
                    }
                }
            }
        }
    }
}

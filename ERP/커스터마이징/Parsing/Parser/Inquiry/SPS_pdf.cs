using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class SPS_pdf
	{
        string vessel;
        string reference;
        string imoNumber;
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

        public string ImoNumber
        {
            get
            {
                return imoNumber;
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



        public SPS_pdf(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            imoNumber = "";

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

            int _referenceCheckInt = -1;

            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemCode = -1;
            int _itemUnit = -1;
            int _itemDwg = -1;
            int _refNo = -1;

            string makerStr = string.Empty;
            string mainStr = string.Empty;
            string serialStr = string.Empty;
            string subsystemStr = string.Empty;
            string catStr = string.Empty;
            string partiStr = string.Empty;
            string dwgStr = string.Empty;
            string refStr = string.Empty;


            bool itemStart = false;


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
                    string firstColString = dt.Rows[i][0].ToString().ToLower();



                    if (string.IsNullOrEmpty(vessel))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Vessel:"))
                            {
                                for (int cc = c + 1; cc < dt.Columns.Count; cc++)
                                {
                                    if (string.IsNullOrEmpty(vessel))
                                    {
                                        vessel = dt.Rows[i][cc].ToString().Trim();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }

                            vessel = vessel.Replace("M/V", "").Replace("M/T", "").Trim();


                            if (!string.IsNullOrEmpty(vessel))
                                break;
                        }
                    }




                    if (string.IsNullOrEmpty(imoNumber))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToUpper().StartsWith("IMO:"))
                            {
                                for (int cc = c + 1; cc < dt.Columns.Count; cc++)
                                {
                                    if (string.IsNullOrEmpty(imoNumber))
                                    {
                                        imoNumber = dt.Rows[i][cc].ToString().Trim();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(imoNumber))
                                break;
                        }
                    }





                    if (_referenceCheckInt.Equals(-1))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToLower().StartsWith("request"))
                            {
                                _referenceCheckInt = c;
                                reference = dt.Rows[i][c + 1].ToString().Trim();
                                itemStart = true;
                            }
                        }
                    }
                    else if (itemStart)
                    {
                        if (string.IsNullOrEmpty(vessel))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().Equals("Request For Quotation:"))
                                    reference = dt.Rows[i][c + 1].ToString().Trim();
                                else if (dt.Rows[i][c].ToString().Equals("Vessel:"))
                                {
                                    for(int cc = c+1; cc < dt.Columns.Count; cc++)
                                    {
                                        if (string.IsNullOrEmpty(vessel))
                                        {
                                            vessel = dt.Rows[i][cc].ToString().Trim();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }

                                vessel = vessel.Replace("M/V","").Replace("M/T","").Trim();


                                if (!string.IsNullOrEmpty(vessel))
                                    break;
                            }
                        }

                        
                        if(firstColString.StartsWith("requisition no"))
                        {
                            reference = dt.Rows[i][1].ToString().Trim();
                        }
                        else if (firstColString.Equals("no."))
                        {
                            string forStr = string.Empty;

                            for (int c = 1; c < dt.Columns.Count; c++)
                            { 
                                forStr = dt.Rows[i][c].ToString().ToLower().Trim();

                                if (forStr.Contains("quantity")) _itemQt = c;
                                else if (forStr.Contains("description")) _itemDesc = c;
                                else if (forStr.Contains("part no.")) _itemCode = c;
                                else if (forStr.Contains("uom")) _itemUnit = c;
                                else if (forStr.Contains("drawing no.")) _itemDwg = c;
                                else if (forStr.Contains("ref. no.")) _refNo = c;
                            }
                        }
                        else if (firstColString.StartsWith("equipment"))
                        {
                            for(int c = 0; c < dt.Columns.Count; c++)
                            {
                                subjString = subjString.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                            }    
                        }
                        else if (firstColString.StartsWith("assembly"))
                        {
                            subjString = subjString.Trim() + Environment.NewLine;
                            for(int c = 0; c < dt.Columns.Count; c++)
                            {
                                if(!subjString.EndsWith(Environment.NewLine))
                                    subjString = subjString + " " + dt.Rows[i][c].ToString().Trim();
                                else
                                    subjString = subjString + dt.Rows[i][c].ToString().Trim();
                            }
                        }
                        else if (GetTo.IsInt(firstColString.Replace(".", "")))
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


                            if (rowValueSpl[6] != null && rowValueSpl[7] == null)
                            {
                                _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                                _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                                _refNo = Convert.ToInt16(rowValueSpl[3].ToString());
                                _itemDwg = Convert.ToInt16(rowValueSpl[4].ToString());
                                _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                                _itemUnit = Convert.ToInt16(rowValueSpl[6].ToString());
                            }


                            if (_itemDesc != -1)
                            {
                                iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                                int _i = i + 1;
                                while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                {
                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][_itemDesc].ToString().Trim();

                                    _i += 1;

                                    if (_i >= dt.Rows.Count)
                                        break;
                                }
                            }

                            if (_itemUnit != -1)
                                iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();


                            if (_itemQt != -1)
                            {
                                iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                                if(string.IsNullOrEmpty(iTemQt))
                                    iTemQt = dt.Rows[i][_itemQt+1].ToString().Trim();
                            }

                            if (_itemCode != -1)
                            {
                                iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                                int _i = i + 1;
                                while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                {
                                    iTemCode = iTemCode.Trim() + " " + dt.Rows[_i][_itemCode].ToString().Trim();

                                    _i += 1;

                                    if (_i >= dt.Rows.Count)
                                        break;
                                }
                            }

                            if (_itemDwg != -1)
                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + "DWG: " + dt.Rows[i][_itemDwg].ToString().Trim();

                            if (_refNo != -1)
                            {
                                refStr = dt.Rows[i][_refNo].ToString().Trim();

                                int _i = i + 1;
                                while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                {
                                    refStr = refStr.Trim() + " " + dt.Rows[_i][_refNo].ToString().Trim();

                                    _i += 1;

                                    if (_i >= dt.Rows.Count)
                                        break;
                                }
                            }

                            if (!string.IsNullOrEmpty(refStr))
                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + "Ref. No: " + refStr;

                            if (!string.IsNullOrEmpty(subjString))
                                iTemSUBJ = subjString.Trim();


                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                            if (!string.IsNullOrEmpty(iTemSUBJ))
                                dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                            iTemQt = string.Empty;
                            iTemUnit = string.Empty;
                            iTemDESC = string.Empty;
                            iTemSUBJ = string.Empty;
                            refStr = string.Empty;
                        }
                    }
                }
            }
        }
    }
}

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class NavigatorPDF
    {
        string vessel;
        string reference;
        string partner;
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

        public string Partner
        {
            get
            {
                return partner;
            }
        }

        public DataTable Item
        {
            get
            {
                return dtItem;
            }
        }

        public string ImoNumber
        {
            get
            {
                return imoNumber;
            }
        }

        #endregion ==================================================================================================== Constructor



        public NavigatorPDF(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            partner = "";                       // 매입처 담당자
            imoNumber = "";                     // 호선번호

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

            string subjStr = string.Empty;
            string subjStr1 = string.Empty;

            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;

            int _itemQt = -1;
            int _itemDesc = -1;
            int _itemUnit = -1;

            int _noStr = -1;

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
                    // 기준 라인 정하기
                    if (_noStr == -1)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToLower().StartsWith("pos."))
                            {
                                _noStr = c;
                            }
                        }

                        int _i = i + 1;

                        while (_noStr == -1)
                        {
                            for (int cc = 0; cc < dt.Columns.Count; cc++)
                            {
                                if (dt.Rows[_i][cc].ToString().ToLower().StartsWith("pos."))
                                {
                                    _noStr = cc;
                                }
                            }

                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }
                    }

                    string firstColStr = dt.Rows[i][_noStr].ToString().ToLower();
                    string zeroColStr = dt.Rows[i][0].ToString().ToLower();
                    //string secondColStr = dt.Rows[i][1].ToString();

                    if (string.IsNullOrEmpty(subjStr1))
                    {
                        for (int r = 10; r < dt.Rows.Count; r++)
                        {
                            if (dt.Rows[r][_noStr].ToString().ToLower().StartsWith("remarks to supplier"))
                            {
                                subjStr1 = string.Empty;

                                int _i = r + 1;
                                while (!string.IsNullOrEmpty(dt.Rows[_i][_noStr].ToString()))
                                {
                                    if (dt.Rows[_i][_noStr].ToString().ToLower().StartsWith("1. requirements for quotation")) break;

                                    subjStr1 = subjStr1.Trim() + Environment.NewLine + dt.Rows[_i][_noStr].ToString().Trim();

                                    _i += 1;

                                    if (_i >= dt.Rows.Count)
                                        break;
                                }
                            }
                        }
                    }

					if (string.IsNullOrEmpty(reference))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().Contains("request for"))
							{
								reference = dt.Rows[i][c + 1].ToString().Trim();

								if (string.IsNullOrEmpty(reference))
								{
                                    if(c+2 > dt.Columns.Count)
                                        reference = dt.Rows[i][c + 2].ToString().Trim();
								}

							}
						}
					}

					if (firstColStr.StartsWith("IMO No.:") && string.IsNullOrEmpty(imoNumber))
                    {
                        imoNumber = firstColStr.ToLower().Replace("imo no.:", "").Replace(".", "").Replace(":", "").Trim() + dt.Rows[i][_noStr+1].ToString().Replace("imo no", "").Replace(".", "").Replace(":", "").Trim();
                    }
                    else if (zeroColStr.StartsWith("imo no") && string.IsNullOrEmpty(imoNumber))
					{
                        imoNumber = zeroColStr.Replace("imo no", "").Replace(".", "").Replace(":", "").Trim() + dt.Rows[i][1].ToString().Replace("imo no", "").Replace(".", "").Replace(":", "").Trim();
                    }
                    else if (firstColStr.StartsWith("description"))
                    {
                        for(int c = 1; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr.Trim() + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("pos."))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("quantity")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Contains("description")) _itemDesc = c;
                        }

                    }
                    else if (GetTo.IsInt(firstColStr) && firstColStr.Length < 4)
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
                            _itemUnit = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[3].ToString());
                        }


                        if (_itemQt != -1)
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Replace(",",".").Trim();
                            iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Trim();
                        }

                        if (_itemDesc != -1)
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();


                            int _i = i + 1;

                            while (!GetTo.IsInt(dt.Rows[_i][_noStr].ToString()))
                            {
                                if (dt.Rows[_i][0].ToString().ToLower().Contains("remarks to supplier")) break;

								if (dt.Rows[_i][1].ToString().ToLower().Contains("component no") || dt.Rows[_i][_noStr].ToString().ToLower().Contains("component no")) break;
                                
                                else if (dt.Rows[_i][1].ToString().ToLower().Contains("subtotal") || dt.Rows[_i][_noStr].ToString().ToLower().Contains("subtotal")) break;
                                else if (dt.Rows[_i][1].ToString().ToLower().Contains("euronav ship management") || dt.Rows[_i][_noStr].ToString().ToLower().Contains("euronav ship management")) break;

                                for (int c = 0; c < dt.Columns.Count; c++)
                                {
                                    if (dt.Rows[_i][c].ToString().ToLower().Equals("printed") || dt.Rows[_i][c].ToString().ToLower().Contains("sub total") || dt.Rows[_i][c].ToString().ToLower().Contains("subtotal") || dt.Rows[_i][c].ToString().ToLower().Contains("discount") || dt.Rows[_i][c].ToString().ToLower().Contains("grand total"))
                                        break;

                                    iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
                                }

                                if (iTemDESC.ToLower().Contains("printed") || iTemDESC.ToLower().Contains("sub total") || iTemDESC.ToLower().Contains("subtotal") || iTemDESC.ToLower().Contains("discount") || iTemDESC.ToLower().Contains("grand total") || iTemDESC.Contains("Euronav Ship Management Hellas LTD."))
                                {
                                    iTemDESC = iTemDESC.ToLower().Replace("printed", "").Replace("sub total","").Replace(":","").Replace("subtotal","").Replace("Euronav Ship Management Hellas LTD.", "").Trim();
                                    break;
                                }

                                iTemDESC = iTemDESC.ToUpper().Replace("REMARKS TO SUPPLIER:", "").Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count) break;
                            }
                        }


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();


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
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;
                    }
                    else if (firstColStr.ToLower().Contains("remarks to supplier") || zeroColStr.ToLower().Contains("remarks to supplier"))
                    {
                        subjStr1 = string.Empty;

                        int _i = i + 1;
                        while ((!string.IsNullOrEmpty(dt.Rows[_i][_noStr].ToString()) && !string.IsNullOrEmpty(dt.Rows[_i][0].ToString())))
                        {
                            if (dt.Rows[_i][_noStr].ToString().ToLower().StartsWith("1. requirements for quotation") || dt.Rows[_i][0].ToString().ToLower().StartsWith("1. requirements for quotation")) break;

                            subjStr1 = subjStr1.Trim() + Environment.NewLine + dt.Rows[_i][0].ToString().Trim() + dt.Rows[_i][1].ToString().Trim();

                            //if (!dt.Rows[_i][_noStr].ToString().ToLower().StartsWith("--- remarks for position") && !dt.Rows[_i][0].ToString().ToLower().StartsWith("--- remarks for position"))
                            //{
                                
                            //}

                            _i += 1;


                            if (_i >= dt.Rows.Count)
                                break;
                        }

                        for (int _c = 0; _c < dtItem.Rows.Count; _c++)
                        {
                            dtItem.Rows[_c]["SUBJ"] = "FOR " + subjStr1;
                        }

                    }
                    //else if (firstColStr.Contains("1. requirements for quotation") || zeroColStr.Contains("1. requirements for quotation"))
                    //{
                    //    if (!string.IsNullOrEmpty(makerStr))
                    //        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr;

                    //    if (!string.IsNullOrEmpty(typeStr))
                    //        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE NO: " + typeStr;

                    //    if (!string.IsNullOrEmpty(serialStr))
                    //        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "SERIAL NO: " + serialStr;

                    //    for (int _i = 0; _i < dtItem.Rows.Count; _i++)
                    //    {
                    //        dtItem.Rows[_i]["SUBJ"] = "FOR " + iTemSUBJ;
                    //    }

                    //}
                    //else if (firstColStr.StartsWith("manufacturer") || zeroColStr.StartsWith("manufacturer"))
                    //{
                    //    makerStr = zeroColStr.Replace("manufacturer", "").Replace(":", "").Trim() + firstColStr.Replace("Manufacturer", "").Replace(":", "").Trim();
                    //}
                    //else if (firstColStr.StartsWith("type No") || zeroColStr.StartsWith("type No"))
                    //{
                    //    typeStr = zeroColStr.Replace("type No", "").Replace(".:", "").Trim() +  firstColStr.Replace("type No", "").Replace(".:", "").Trim();
                    //}
                    //else if (firstColStr.StartsWith("serial No") || zeroColStr.StartsWith("serial No"))
                    //{
                    //    serialStr = zeroColStr.Replace("serial No", "").Replace(".:", "").Trim() + firstColStr.Replace("serial No", "").Replace(".:", "").Trim() ;
                    //}
                }
            }
        }
    }
}

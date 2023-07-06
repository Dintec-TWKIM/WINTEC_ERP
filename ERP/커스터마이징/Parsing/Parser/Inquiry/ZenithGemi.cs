using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class ZenithGemi
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



        public ZenithGemi(string fileName)
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

            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemDesc2 = -1;

            string makerStr = string.Empty;
            string modelStr = string.Empty;
            string serialStr = string.Empty;
            string sizeStr = string.Empty;
            string typeStr = string.Empty;
            string subjStr = string.Empty;
            string dwgStr = string.Empty;

            string descStr = string.Empty;
            string descFirstStr = string.Empty;

            int startInt = -1;


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
                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("RFQ") || dt.Rows[i][c].ToString().Contains("Quotation #:"))
                            {
                                reference = dt.Rows[i][c].ToString().Replace("RFQ", "").Replace("#", "").Replace(":", "").Replace("Quotation", "").Trim();
                            }
                        }
                    }

                    string firstColStr = dt.Rows[i][0].ToString();

                    if (firstColStr.Contains("Vessel:") && string.IsNullOrEmpty(vessel))
                    {
                        vessel = firstColStr.Replace("Vessel:","").Trim();

                        startInt = vessel.IndexOf("(");

                        string _vessel = vessel.Substring(0, startInt);

                        vessel = _vessel.Trim();
 
                        int _i =i + 1;
                        while (!dt.Rows[_i][0].ToString().Contains("Manufacturer") && !dt.Rows[_i][0].ToString().Contains("Model") && !dt.Rows[_i][0].ToString().Contains("Serial No") && !GetTo.IsInt(dt.Rows[_i][0].ToString()))
                        {
                            subjStr = subjStr + Environment.NewLine + dt.Rows[_i][0].ToString().Trim();
                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }
                    }
                    else if (firstColStr.StartsWith("Manufacturer"))
                    {
                        makerStr = firstColStr.Replace("Manufacturer", "").Replace(":","").Trim();
                    }
                    else if (firstColStr.StartsWith("Model"))
                    {
                        modelStr = firstColStr.Replace("Model", "").Replace(":", "").Trim();

                        for (int c = 2; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Type"))
                                typeStr = dt.Rows[i][c].ToString().Replace("Type","").Replace(":","").Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Serial No"))
                    {
                        serialStr = firstColStr.Replace("Serial No","").Replace(":","").Trim();
                    }
                    else if (firstColStr.StartsWith("Size"))
                    {
                        sizeStr = firstColStr.Replace("Size", "").Replace(":", "").Trim();
                    }
                    else if (firstColStr.StartsWith("No."))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("Dlv")) _itemDesc2 = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qty") || dt.Rows[i][c].ToString().Equals("QtyUnit")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit Price") && _itemDesc2.Equals(-1)) _itemDesc2 = c-1;
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

                        if (rowValueSpl[9] != null && rowValueSpl[10] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDesc2 = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[6].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[7].ToString());
                        }
                        else if(rowValueSpl[8] != null && rowValueSpl[9] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDesc2 = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[6].ToString());
                        }
                        else if(rowValueSpl[7] != null && rowValueSpl[8] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            //_itemDesc2 = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[5].ToString());
                        }
                        //위랑 형식이 좀 다름!!!!!!
                        else if (rowValueSpl[4] != null && rowValueSpl[5] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDesc2 = Convert.ToInt16(rowValueSpl[3].ToString()) - 1;
                        }


                        if (!_itemQt.Equals(-1))
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                            if (!GetTo.IsInt(iTemQt.Replace(".", "")))
                            {
                                string[] qtSpl = iTemQt.Split('.');

                                if (qtSpl.Length >= 2)
                                {
                                    iTemQt = qtSpl[0].Trim();
                                    iTemUnit = qtSpl[1].Replace("00","").Trim();
                                }
                            }
                        }

                        if (!_itemUnit.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();


                        //DESCRIPTION
                        if (!_itemDesc.Equals(-1))
                        {
                            //descFirstStr = string.Empty;
                            descStr = string.Empty;
                            for (int c = _itemDesc; c <= _itemDesc2; c++)
                            {
                                descStr = descStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                            }
                                
                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                for (int c = _itemDesc; c <= _itemDesc2; c++)
                                {
                                    descStr = descStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                }
                                    _i += 1;

                                    if (_i >= dt.Rows.Count)
                                        break;
                            }


                            if (!string.IsNullOrEmpty(descStr))
                            {
                                iTemDESC = descStr.Trim();

                                // 선명
                                int idx_s = descStr.IndexOf("Dwg. No:");

                                int idx_e = 0;

                                // ITEM DESCRIPTION
                                if (!idx_s.Equals(-1))
                                {
                                    idx_e = descStr.IndexOf(",", idx_s);
                                    dwgStr = descStr.Substring(idx_s, idx_e - idx_s).Replace("Dwg. No", "").Replace(":", "").Trim();
                                }

                                idx_s = descStr.IndexOf("MANUAL:");

                                if (!idx_s.Equals(-1))
                                {
                                    iTemCode = descStr.Substring(idx_s).Replace("MANUAL", "").Replace(":", "").Trim();
                                }

                                idx_s = descStr.IndexOf("IMPA :");
                                idx_e = descStr.IndexOf("Account");

                                if (!idx_s.Equals(-1) && !idx_e.Equals(-1))
                                {
                                    iTemCode = descStr.Substring(idx_s, idx_e - idx_s).Replace("IMPA :", "").Trim();
                                    iTemDESC = iTemDESC.Substring(0, idx_s).Trim();
                                }
                                else if(!idx_e.Equals(-1))
                                {
                                    iTemDESC = iTemDESC.Substring(0, idx_e).Trim();
                                }
                            }
                        }


                        if(iTemDESC.Contains("Part #:"))
                        {
                            int idx_s = iTemDESC.IndexOf("Part #:");

                            if(idx_s != -1 && iTemDESC.Length > idx_s + 14)
                            {
                                iTemCode = iTemDESC.Substring(idx_s, 14).Replace("Part #:", "").Trim();
                            }
                            
                        }


                        //SUBJECT
                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = iTemSUBJ.Trim() + subjStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(modelStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                        if (!string.IsNullOrEmpty(sizeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "SIZE: " + sizeStr.Trim();

                        if (!string.IsNullOrEmpty(dwgStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG. NO:" + dwgStr.Trim();


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
                        iTemSUBJ = string.Empty;
                    }
                }
            }
        }
    }
}

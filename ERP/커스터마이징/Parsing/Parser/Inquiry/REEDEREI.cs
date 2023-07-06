using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class REEDEREI
    {
        string vessel;
        string reference;
        string partner;
        string contact;
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

        public string Contact
        {
            get
            {
                return contact;
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



        public REEDEREI(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            partner = "";                       // 매입처 담당자
            contact = string.Empty;
            imoNumber = string.Empty;

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
            string iTemDwg = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemPart = -1;
            int _itemUnit = -1;
            int _itemDwg = -1;


            string subjStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;

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

                    if (string.IsNullOrEmpty(contact))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Contact Person"))
                                contact = dt.Rows[i+1][c].ToString().Trim();
                        }
                    }


                    if(string.IsNullOrEmpty(imoNumber))
                    {
                        for(int c =0; c < dt.Columns.Count; c++)
                        {
                            if(dt.Rows[i][c].ToString().StartsWith("IMO No"))
                                imoNumber = dt.Rows[i][c + 1].ToString().Trim();
                        }
                    }

                    if(string.IsNullOrEmpty(vessel))
                    {
                        for(int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Name"))
                                vessel = dt.Rows[i][c + 1].ToString().Trim();
                        }
                    }

                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Reference No"))
                            {
                                for(int c2 = c+1; c2 < dt.Columns.Count; c2++ )
                                {
                                    if (string.IsNullOrEmpty(reference))
                                        reference = dt.Rows[i][c2].ToString().Trim();
                                }
                            }
                        }
                    }

                    //if (firstColStr.StartsWith("Name"))
                    //{
                    //    vessel = dt.Rows[i][1].ToString().Trim();
                    //}
                    //else if (firstColStr.StartsWith("Label"))
                    //{
                    //    subjStr = dt.Rows[i][1].ToString().Trim();
                    //}
                    //else if (firstColStr.StartsWith("Brand"))
                    //{
                    //    makerStr = dt.Rows[i][1].ToString().Trim();
                    //}
                    //else if (firstColStr.StartsWith("Type"))
                    //{
                    //    typeStr = dt.Rows[i][1].ToString().Trim();
                    //}
                    if (firstColStr.ToLower().Equals("component details"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        int _i = i + 1;

                        while (!GetTo.IsInt(dt.Rows[_i][0].ToString()))
                        {
                            subjStr = subjStr.Trim() + Environment.NewLine;

                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                subjStr = subjStr + " " + dt.Rows[_i][c].ToString().Trim();
                            }

                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }

                    }
                    else if (firstColStr.Equals("Pos") || firstColStr.Equals("Item"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("Code No.") || dt.Rows[i][c].ToString().Equals("Part No.") || dt.Rows[i][c].ToString().ToLower().Equals("part number")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Quantity")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Item No")) _itemPart = c;
                            else if (dt.Rows[i][c].ToString().ToLower().StartsWith("drawing no")) _itemDwg = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr) && firstColStr.Length < 5)
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


                        if (rowValueSpl[5] != null && rowValueSpl[6] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemDwg = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[5].ToString());
                        }
                        else if (rowValueSpl[4] != null && rowValueSpl[5] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemDwg = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemUnit = -1;
                        }

                        if (_itemDesc != -1)
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


                        if (_itemCode != -1)
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (_itemDwg != -1)
                            iTemDwg = dt.Rows[i][_itemDwg].ToString().Trim();

                        if (_itemQt != -1 && _itemUnit == -1)
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                            if (!GetTo.IsInt(iTemQt))
                            {
                                iTemQt = dt.Rows[i][_itemDwg].ToString().Trim();
                                iTemUnit = dt.Rows[i][_itemQt].ToString().Trim();
                            }
                            else
                            {
                                iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Trim();
                            }
                        }
                        else if (_itemQt != -1 && _itemUnit != -1)
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(iTemDwg))
                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + "DWG: " + iTemDwg.Trim();



                        //if (_itemCode != -1)
                        //{
                        //    iTemDESC = dt.Rows[i][1].ToString().Trim() + " " +
                        //    dt.Rows[i + 1][0].ToString().Trim() + " " + dt.Rows[i + 1][1].ToString().Trim() + " " +
                        //    dt.Rows[i + 2][0].ToString().Trim();

                        //    iTemCode = dt.Rows[i][_itemCode].ToString().Trim();
                        //    iTemSUBJ = dt.Rows[i + 2][_itemCode].ToString().Trim();
                        //}
                        //else if (_itemPart != -1)
                        //{
                        //    iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim() + ", " + dt.Rows[i][_itemPart].ToString().Trim();

                        //    iTemCode = dt.Rows[i + 1][_itemPart].ToString().Trim();
                        //    iTemSUBJ = dt.Rows[i + 4][_itemPart].ToString().Trim();
                        //}
                        //else
                        //{
                        //    iTemCode = dt.Rows[i][2].ToString().Trim();
                        //    iTemSUBJ = dt.Rows[i + 2][2].ToString().Trim();
                        //}

                        //if (_itemQt != -1)
                        //{
                        //    iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                        //    iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Trim();
                        //}
                        //else
                        //{
                        //    iTemQt = dt.Rows[i][6].ToString().Trim();
                        //    iTemUnit = dt.Rows[i][7].ToString().Trim();
                        //}


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr;

                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr;

                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit.Replace(".", ""));
                        if (GetTo.IsInt(iTemQt))
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

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Procurement
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



        public Procurement(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            partner = "";                       // 매입처 담당자
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

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemUniq = -1;
            int _itemDwg = -1;
            int _itemQt = -1;
            int _itemUnit = -1;

            string iTemDwg = string.Empty;

            string subjStr = string.Empty;


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


                    if (firstColStr.StartsWith("Requisition No."))
                    {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                if(string.IsNullOrEmpty(reference))
                                    reference = dt.Rows[i][c].ToString().Trim();
                            }

                        if (string.IsNullOrEmpty(vessel))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().Contains("Vessel"))
                                    vessel = dt.Rows[i][c + 1].ToString().Trim() + dt.Rows[i][c + 2].ToString().Trim();
                            }
                        }
                    }


                    if(string.IsNullOrEmpty(imoNumber))
                    {
                        for(int c= 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("IMO"))
                            {
                                for(int c2 = c+1; c2 < dt.Columns.Count; c2++)
                                {
                                    if (string.IsNullOrEmpty(imoNumber))
                                        imoNumber = imoNumber.Trim() + dt.Rows[i][c2].ToString().Trim();
                                    else
                                        break;
                                }
                                
                            }
                        }
                    }

                    if (firstColStr.StartsWith("Equipment:"))
                    {
                        for(int c = 0; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        subjStr = subjStr.Trim() + Environment.NewLine;

                        int _i = i + 1;

                        while (!dt.Rows[_i][0].ToString().StartsWith("No."))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (subjStr.EndsWith(Environment.NewLine))
                                    subjStr = subjStr + " " + dt.Rows[_i][c].ToString().Trim();
                                else
                                    subjStr = subjStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                            }

                            _i += 1;

                            if(_i >= dt.Rows.Count)
                                break;
                        }
                    }
                    else if (firstColStr.StartsWith("No."))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("Part No.")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Ref. No.")) _itemUniq = c;
                            else if (dt.Rows[i][c].ToString().Equals("Drawing No.")) _itemDwg = c;
                            else if (dt.Rows[i][c].ToString().Equals("Quantity")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("UoM")) _itemUnit = c;
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


                        if (rowValueSpl[6] != null && rowValueSpl[7] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemUniq = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemDwg = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[6].ToString());
                        }


                        if (_itemUnit != -1)
                        {
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();
                        }
                            
                        if (_itemQt != -1 && _itemUnit != -1)
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                            if (string.IsNullOrEmpty(iTemQt))
                            {
                                if (_itemQt + 1 == _itemUnit)
                                {
                                    iTemQt = dt.Rows[i][_itemQt + 1].ToString().Trim();
                                    iTemUnit = dt.Rows[i][_itemUnit + 1].ToString().Trim();
                                }
                                else if(_itemQt + 1 < _itemUnit)
                                {
                                    iTemQt = dt.Rows[i][_itemQt + 1].ToString().Trim();
                                }
                            }
                        }

                        if (_itemCode != -1)
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (_itemDesc != -1)
                        {
                            int _i = 0;

                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            if (string.IsNullOrEmpty(iTemDESC))
                            {
                                iTemDESC = dt.Rows[i - 1][_itemDesc].ToString().Trim();
                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i + 1][_itemDesc].ToString().Trim();
                                _i = i + 2;
                            }
                            else
                            {
                                _i = i + 1;
                            }

                            

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemDESC = iTemDESC + " " + dt.Rows[_i][_itemDesc].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }

                            
                        }

                        if (_itemUniq != -1)
                        {
                            for (int c = _itemUniq; c < _itemDwg; c++ )
                            {
                                if(!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                                    iTemDESC = iTemDESC + Environment.NewLine + "Ref. No. " + dt.Rows[i][c].ToString();
                            }
                        }

                        if (_itemDwg != -1)
                        {
                            iTemDESC = iTemDESC + Environment.NewLine + "Drawing No.  " + dt.Rows[i][_itemDwg].ToString();
                            // 두베코 요청
                            //iTemDwg = dt.Rows[i][_itemDwg].ToString().Trim();

                            //if (!string.IsNullOrEmpty(iTemDwg))
                            //    iTemCode = iTemDwg + "-" + iTemCode;
                        }


                        if(!string.IsNullOrEmpty(subjStr))
                        {
                            iTemSUBJ = subjStr.Trim();
                        }


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

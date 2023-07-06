using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Ultraship
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



        public Ultraship(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            partner = "";                       // 매입처 담당자
            imoNumber = "";

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

            string subjStr = string.Empty;

            string subjStr1 = string.Empty;

            bool subtotalCh = false;

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


                    if (string.IsNullOrEmpty(subjStr1))
                    {
                        for (int r = 10; r < dt.Rows.Count; r++)
                        {
                            if (dt.Rows[r][0].ToString().StartsWith("Remarks to Supplier"))
                            {
                                subjStr1 = string.Empty;

                                int _i = r + 1;
                                while (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && !dt.Rows[_i][0].ToString().StartsWith("Technical Info") && !dt.Rows[_i][0].ToString().ToLower().StartsWith("ultraship sps") &&
                                    !subtotalCh
                                    )
                                {
                                    if (dt.Rows[_i][0].ToString().ToLower().StartsWith("sub total")) { subtotalCh = true; break; }
                                    else if (dt.Rows[_i][0].ToString().StartsWith("Sub total")) { subtotalCh = true; break; }
                                    else if (dt.Rows[_i][0].ToString().StartsWith("Discount")) { subtotalCh = true; break; }
                                    else if (dt.Rows[_i][0].ToString().StartsWith("Grand total")) { subtotalCh = true; break; }
                                    else if (dt.Rows[_i][0].ToString().StartsWith("Printed")) { subtotalCh = true; break; }
                                    else if (dt.Rows[_i][0].ToString().ToLower().StartsWith("ultraship sps")) { subtotalCh = true; break; }

                                    if (subtotalCh)
                                        break;

                                    subjStr1 = subjStr1.Trim() + Environment.NewLine + dt.Rows[_i][0].ToString().Trim();

                                    _i += 1;

                                    if (_i >= dt.Rows.Count)
                                        break;
                                }

                                
                            }
                        }

                        subtotalCh = false;

                        if (subjStr1.Contains("UltraShip ApS is"))
                        {
                            int idx_s = subjStr1.IndexOf("UltraShip ApS is");

                            subjStr1 = subjStr1.Substring(0, idx_s);
                        }
                    }



                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Request for Quote:"))
                            {
                                reference = dt.Rows[i][c + 1].ToString().Trim();
                            }
                        }
                    }

                    if (firstColStr.StartsWith("Pos."))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Quantity")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
                        }
                    }
                    else if (firstColStr.StartsWith("IMO") && string.IsNullOrEmpty(imoNumber))
                    {
                        imoNumber = firstColStr.Trim() + dt.Rows[i][1].ToString().Trim();

                        imoNumber = imoNumber.ToLower().Replace("imo", "").Replace("no", "").Replace(".", "").Replace(":", "").Trim();
                    }
                    else if (firstColStr.StartsWith("Description"))
                    {
                        for(int c = 1; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                        
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        iTemNo = firstColStr;

                        if (_itemQt != -1)
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                            iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Trim();
                        }

                        if (_itemDesc != -1)
                        {
                            iTemDESC = string.Empty;

                            for (int c = _itemQt + 2; c < dt.Columns.Count; c++)
                            {
                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                            }

                            int _i = i + 1;

                            while (!GetTo.IsInt(dt.Rows[_i][0].ToString()) && !dt.Rows[_i][0].ToString().StartsWith("Remarks to Supplier") && !dt.Rows[_i][0].ToString().ToLower().StartsWith("ultraship sps"))
                            {
                                for (int c = 0; c < dt.Columns.Count; c++)
                                {
                                    if (dt.Rows[_i][c].ToString().ToLower().StartsWith("sub total")) { subtotalCh = true; break;}
                                    else if (dt.Rows[_i][c].ToString().StartsWith("Sub total")) { subtotalCh = true; break; }
                                    else if (dt.Rows[_i][c].ToString().StartsWith("Discount")) { subtotalCh = true; break; }
                                    else if (dt.Rows[_i][c].ToString().StartsWith("Grand total")) { subtotalCh = true; break; }
                                    else if (dt.Rows[_i][c].ToString().StartsWith("Printed")) { subtotalCh = true; break; }
                                    else if (dt.Rows[_i][c].ToString().ToLower().StartsWith("ultraship sps")) { subtotalCh = true; break; }
                                    
                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                }

                                if (subtotalCh)
                                    break;

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        subtotalCh = false;

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(subjStr1))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr1.Trim();

                        iTemQt = iTemQt.Replace(",", ".").Trim();


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
                    else if (firstColStr.StartsWith("Remarks to Supplier"))
                    {
                        iTemSUBJ = dt.Rows[i][0].ToString().Trim();

                        int _i = i + 1;

                        while (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && !dt.Rows[_i][0].ToString().StartsWith("UltraShip ApS"))
                        {
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + dt.Rows[_i][0].ToString().Trim() + dt.Rows[_i][1].ToString().Trim() + dt.Rows[_i][2].ToString().Trim();

                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }

                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    }
                }
            }
        }
    }
}

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class MTM
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



        public MTM(string fileName)
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

            string itemDrw = string.Empty;

            int _itemDesc = -1;
            int _itemDrw = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;

            int _vesselCol = -1;

            string subjStr = string.Empty;
            string modelStr = string.Empty;


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


                    if (_vesselCol.Equals(-1))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Vessel"))
                                _vesselCol = c;
                        }
                    }
                    else
                    {
                        for (int r = 0; r < 20; r++)
                        {
                            if (dt.Rows[r][_vesselCol].ToString().StartsWith("Vessel"))
                                vessel = dt.Rows[r][_vesselCol + 1].ToString().Replace(":", "").Trim() + dt.Rows[r][_vesselCol + 2].ToString().Replace(":", "").Trim();
                            else if (dt.Rows[r][_vesselCol].ToString().StartsWith("RFQ No"))
                                reference = dt.Rows[r][_vesselCol + 1].ToString().Replace(":", "").Trim() + dt.Rows[r][_vesselCol + 2].ToString().Replace(":", "").Trim();
                            else if (dt.Rows[r][_vesselCol].ToString().StartsWith("Equipment"))
                                subjStr = dt.Rows[r][_vesselCol + 1].ToString().Trim();
                            else if (dt.Rows[r][_vesselCol].ToString().StartsWith("Model/Type"))
                                modelStr = dt.Rows[r][_vesselCol + 1].ToString().Trim();
                        }
                    }


                    if (firstColStr.StartsWith("No."))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Part No."))
                            {
                                _itemCode = c;
                                _itemDesc = c;
                            }
                            else if (dt.Rows[i][c].ToString().Equals("Drawing No.")) _itemDrw = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qty") && dt.Rows[i-1][c].ToString().Contains("RFQ")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;


                            if (dt.Rows[i+1][c].ToString().Equals("Part No."))
                            {
                                _itemCode = c;
                                _itemDesc = c;
                            }
                            else if (dt.Rows[i + 1][c].ToString().Equals("Drawing No.")) _itemDrw = c;
                            else if (dt.Rows[i + 1][c].ToString().Contains("RFQ Qty")) _itemQt = c;
                            else if (dt.Rows[i + 1][c].ToString().Equals("Unit")) _itemUnit = c;


                            if (dt.Rows[i+2][c].ToString().Equals("Part No."))
                            {
                                _itemCode = c;
                                _itemDesc = c;
                            }
                            else if (dt.Rows[i + 2][c].ToString().Equals("Drawing No.")) _itemDrw = c;
                            else if (dt.Rows[i + 2][c].ToString().Contains("RFQ Qty")) _itemQt = c;
                            else if (dt.Rows[i + 2][c].ToString().Equals("Unit")) _itemUnit = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (!_itemQt.Equals(-1))
                        {
                            iTemQt = dt.Rows[i + 1][_itemQt].ToString().Trim();

                            if (string.IsNullOrEmpty(iTemQt))
                                iTemQt = dt.Rows[i + 2][_itemQt].ToString().Trim();
                        }

                        if (!_itemUnit.Equals(-1))
                        {
                            iTemUnit = dt.Rows[i + 1][_itemUnit].ToString().Trim();

                            if (string.IsNullOrEmpty(iTemUnit))
                                iTemUnit = dt.Rows[i + 2][_itemUnit].ToString().Trim();
                        }

                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;

                            while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemDESC = iTemDESC + " " + dt.Rows[_i][_itemDesc].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        if (!_itemDrw.Equals(-1))
                            itemDrw = dt.Rows[i+1][_itemDrw].ToString().Trim();

                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i + 1][_itemCode].ToString().Replace(" ","").Trim();


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(modelStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL/TYPE: " + modelStr.Trim();


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        //dtItem.Rows.Add();


                        iTemSUBJ = string.Empty;
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

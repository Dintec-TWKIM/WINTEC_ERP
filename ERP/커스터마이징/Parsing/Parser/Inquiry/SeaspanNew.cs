using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class SeaspanNew
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



        public SeaspanNew(string fileName)
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
            int _itemCode = -1;
            int _itemUnit = -1;


            string subjStr = string.Empty;
            string makerStr = string.Empty;
            string modelStr = string.Empty;
            string SerialStr = string.Empty;
            string rmkStr = string.Empty;
            string sizeStr = string.Empty;

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

                    if (firstColStr.StartsWith("Requisition No"))
                    {
                        reference = firstColStr.Replace("Requisition No", "").Replace(":", "").Replace(".","").Trim();
                    }
                    else if (firstColStr.StartsWith("Equipment"))
                    {
                        subjStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Manufacturer"))
                    {
                        makerStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Model"))
                    {
                        modelStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Serial No"))
                    {
                        if (dt.Rows[i][1].ToString().Contains("Size"))
                        {
                            sizeStr = dt.Rows[i][2].ToString().Trim();
                        }
                        else
                        {
                            SerialStr = dt.Rows[i][1].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Remarks:"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            rmkStr = rmkStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        int _i = i + 1;
                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                        {
                            for(int c = 1; c < dt.Columns.Count; c++)
                            {
                                rmkStr = rmkStr.Trim() + dt.Rows[i][c].ToString().Trim();
                            }

                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }
                    }
                    else if (firstColStr.StartsWith("Itm No."))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Item Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Part No.")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Qty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Unit")) _itemUnit = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr.Replace(")", "")))
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
                            _itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());

                            if (!_itemCode.Equals(-1))
                                iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                            
                        }
                        else if (rowValueSpl[4] != null && rowValueSpl[5] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
                        }


                        if (!_itemUnit.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Replace(",",".").Trim();

                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                if ((dt.Rows[_i][_itemDesc].ToString().StartsWith("Part No") || dt.Rows[_i][_itemDesc].ToString().StartsWith("PART NO")) && !dt.Rows[_i][_itemDesc].ToString().ToLower().Contains("impa"))
                                    iTemCode = dt.Rows[_i][_itemDesc].ToString().Trim();
                                else if(!iTemDESC.Contains(dt.Rows[_i][_itemDesc].ToString()) && !dt.Rows[_i][_itemDesc].ToString().ToLower().Contains("impa"))
                                    iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim();

                                iTemCode = iTemCode.Trim() + dt.Rows[_i][_itemCode].ToString().Trim();

                                _i += 1;
                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr;

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr;

                        if (!string.IsNullOrEmpty(modelStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr;

                        if (!string.IsNullOrEmpty(SerialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + SerialStr;

                        if (!string.IsNullOrEmpty(sizeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "SIZE: " + sizeStr;


                        if (!string.IsNullOrEmpty(rmkStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + rmkStr.Trim();
 

                        iTemCode = iTemCode.Replace("PART NO", "").Replace("Part No","").Replace(":", "").Replace(". ","").Replace(".  ","").Trim();


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr.Replace(")","").Trim();
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
                }
            }
        }
    }
}

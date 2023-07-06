using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class OSGShip
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



        public OSGShip(string fileName)
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

            int _itemDescInt = -1;
            int _itemQtInt = -1;
            int _itemUnitInt = -1;

            string makerStr = string.Empty;
            string modelStr = string.Empty;
            string serialStr = string.Empty;
            string sizeStr = string.Empty;
            string subjStr = string.Empty;
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
                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(reference) && dt.Rows[i][c].ToString().StartsWith("RFQ"))
                                reference = dt.Rows[i][c].ToString().Replace("RFQ","").Replace("#","").Replace(":","").Trim();
                        }
                    }

                    string firstColStr = dt.Rows[i][0].ToString();

                    if (firstColStr.Contains("Buyer:"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Vessels:"))
                                vessel = dt.Rows[i][c].ToString().Replace("Vessels:","").Trim();
                        }
                    }
                    else if (firstColStr.Equals("No."))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Description")) _itemDescInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQtInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnitInt = c;
                        }
                    }
                    else if (firstColStr.IndexOf("Manufacturer") == 0)
                    {
                        makerStr = firstColStr.Replace("Manufacturer:", "").Trim();
                    }
                    else if (firstColStr.IndexOf("Model:") == 0)
                    {
                        modelStr = firstColStr.Replace("Model:", "").Trim();

                        for (int c = 2; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Type"))
                                typeStr = dt.Rows[i][c].ToString().Replace("Type:","").Trim();
                        }
                    }
                    else if (firstColStr.IndexOf("Serial No") == 0)
                    {
                        serialStr = firstColStr.Replace("Serial No:", "").Trim();
                    }
                    else if (firstColStr.Contains(vessel) && !string.IsNullOrEmpty(vessel) && string.IsNullOrEmpty(subjStr))
                    {
                        subjStr = dt.Rows[i + 1][0].ToString().Replace(vessel, "").Trim() + " " + dt.Rows[i + 2][0].ToString().Trim();

                        if (subjStr.StartsWith("/"))
                            subjStr = subjStr.Substring(1, subjStr.Length - 1).Trim();
                    }
                    else if (GetTo.IsInt(firstColStr) && firstColStr.Length < 4)
                    {

                        iTemNo = firstColStr.Trim();
                        // row 값 가져와서 배열에 넣은후 값 추가하기
                        string[] rowValueSpl = new string[10];
                        int columnCount = 0;
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                            {
                                rowValueSpl[columnCount] = c.ToString();
                                columnCount++;
                            }
                        }

                        if (rowValueSpl[8] != null && rowValueSpl[9] == null)
                        {
                            _itemDescInt = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemQtInt = Convert.ToInt16(rowValueSpl[5].ToString());
                            _itemUnitInt = Convert.ToInt16(rowValueSpl[6].ToString());
                        }
                        else if (rowValueSpl[7] != null && rowValueSpl[8] == null)
                        {
                            _itemDescInt = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemQtInt = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemUnitInt = Convert.ToInt16(rowValueSpl[5].ToString());
                        }
                        else if ((rowValueSpl[5] != null))
                        {
                            _itemDescInt = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemQtInt = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemUnitInt = Convert.ToInt16(rowValueSpl[5].ToString());
                        }




                        iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();
                        iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();

                        iTemDESC = dt.Rows[i][_itemDescInt].ToString().Trim();

                        int _i = i + 1;
                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && _i < dt.Rows.Count - 3)
                        {
                            if ((dt.Rows[_i][_itemDescInt].ToString().StartsWith("PART NO") || dt.Rows[_i][_itemDescInt].ToString().StartsWith("P/N") || dt.Rows[_i][_itemDescInt].ToString().StartsWith("Part") || dt.Rows[_i][_itemDescInt].ToString().StartsWith("PART LIST")) && !dt.Rows[_i][_itemDescInt].ToString().Contains("DWG"))
                                iTemCode = dt.Rows[_i][_itemDescInt].ToString().Replace("PART NO","").Replace("P/N","").Replace("Part","").Replace(":","").Replace("#","").Replace("PART","").Replace("LIST","").Trim();
                            else
                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][_itemDescInt].ToString().Replace("PART NO", "").Replace("P/N", "").Replace("Part", "").Replace(":", "").Replace("#", "").Trim();

                            _i += 1;
                        }


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(modelStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr.Trim();

                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialStr.Trim();




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
                        iTemSUBJ = string.Empty;
                    }
                }
            }
        }
    }
}

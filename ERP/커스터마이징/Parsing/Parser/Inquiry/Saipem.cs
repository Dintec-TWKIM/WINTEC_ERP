using Aspose.Email.Outlook;
using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Saipem
    {
        string vessel;
        string reference;
        string remark;
        DataTable dtItem;

        string fileName;
        string mailfileName;
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

        public string Remark
        {
            get
            {
                return remark;
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



        public Saipem(string fileName, string mailfileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            remark = "";                        // 비고

            dtItem = new DataTable();
            dtItem.Columns.Add("NO");           // 순번
            dtItem.Columns.Add("SUBJ");         // 주제
            dtItem.Columns.Add("ITEM");         // 품목코드
            dtItem.Columns.Add("DESC");         // 품목명
            dtItem.Columns.Add("UNIT");         // 단위
            dtItem.Columns.Add("QT");           // 수량
            dtItem.Columns.Add("UNIQ");         // 선사
            this.fileName = fileName;
            this.mailfileName = mailfileName;
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

            string descStr = string.Empty;

            int _itemDesc = -1;
            int _itemUnit = -1;
            int _itemQt = -1;
            int _itemDate = -1;

            int _itemRowNum = -1;

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


            MapiMessage msg = MapiMessage.FromFile(mailfileName);

            string mailBodyStr = msg.Body;


            int idx_s = mailBodyStr.IndexOf("Important Notes:");
            int idx_e = mailBodyStr.IndexOf("If you need any additional information");

            if (idx_s > 0 && idx_s < idx_e)
            {
                remark = mailBodyStr.Substring(idx_s, idx_e - idx_s).Replace("\r\n\r\n", "\r\n").Trim();
            }

            foreach (DataTable dt in ds.Tables)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string firstColStr = dt.Rows[i][0].ToString();

                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("N.") || dt.Rows[i][c].ToString().StartsWith("Request for"))
                            {
                                reference = dt.Rows[i][c].ToString().Replace("Request for Offer No", "").Replace(".","").Trim();
                            }
                        }
                    }

                    if (firstColStr.StartsWith("Item"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("Quantity")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("UM")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Expected") || dt.Rows[i][c].ToString().StartsWith("Delivery")) _itemDate = c;
                        }

                        itemStart = true;
                    }
                    else if (GetTo.IsInt(firstColStr) && itemStart)
                    {
                        if (GetTo.IsInt(dt.Rows[i][_itemDate].ToString().Replace(".", "")))
                        {
                            _itemRowNum = i;
                        }
                        else
                        {
                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                if (GetTo.IsInt(dt.Rows[_i][_itemDate].ToString().Replace(".", "")))
                                {
                                    _itemRowNum = _i;

                                    break;
                                }

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        // row 값 가져와서 배열에 넣은후 값 추가하기
                        string[] rowValueSpl = new string[20];
                        int columnCount = 0;
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[_itemRowNum][c].ToString()))
                            {
                                rowValueSpl[columnCount] = c.ToString();
                                columnCount++;
                            }
                        }


                        if (rowValueSpl[5] != null && rowValueSpl[6] == null)
                        {
                            //_itemDesc = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemDate = Convert.ToInt16(rowValueSpl[5].ToString());
                        }
                        else if (rowValueSpl[4] != null && rowValueSpl[5] == null)
                        {
                            //_itemDesc = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemDate = Convert.ToInt16(rowValueSpl[4].ToString());
                        }
                        else if (rowValueSpl[3] != null && rowValueSpl[4] == null)
                        {
                            _itemQt = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemDate = Convert.ToInt16(rowValueSpl[3].ToString());
                        }
                        else if (rowValueSpl[2] != null && rowValueSpl[3] == null)
                        {
                            _itemQt = Convert.ToInt16(rowValueSpl[0].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDate = Convert.ToInt16(rowValueSpl[2].ToString());
                        }

                        if (!_itemQt.Equals(-1))
                        {
                            iTemQt = dt.Rows[_itemRowNum][_itemQt].ToString().Trim();

                            if (string.IsNullOrEmpty(iTemQt))
                                iTemQt = dt.Rows[_itemRowNum + 1][_itemQt].ToString().Trim();


                            if (string.IsNullOrEmpty(iTemQt))
                                iTemQt = dt.Rows[_itemRowNum][_itemUnit - 1].ToString().Trim();
                        }

                        if (!_itemUnit.Equals(-1))
                        {
                            iTemUnit = dt.Rows[_itemRowNum][_itemUnit].ToString().Trim();

                            if (string.IsNullOrEmpty(iTemUnit))
                                iTemUnit = dt.Rows[_itemRowNum + 1][_itemUnit].ToString().Trim();
                        }

                        if (!_itemDesc.Equals(-1))
                        {
                            descStr = dt.Rows[_itemRowNum][_itemDesc].ToString().Trim();

                            int _i2 = _itemRowNum + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i2][0].ToString()) && !dt.Rows[_i2][_itemDesc].ToString().ToUpper().Contains("CODE") && !dt.Rows[_i2][_itemDesc].ToString().ToUpper().Contains("NUMBER"))
                            {
                                descStr = descStr.Trim() + " " + dt.Rows[_i2][_itemDesc].ToString().Trim();
                                _i2 += 1;

                                if (_i2 >= dt.Rows.Count)
                                    break;
                            }

                            int _i3 = _itemRowNum + 1;
                            while(string.IsNullOrEmpty(dt.Rows[_i3][0].ToString()) || !GetTo.IsInt(dt.Rows[_i3][0].ToString()))
                            {
                                if (dt.Rows[_i3][_itemDesc].ToString().ToUpper().StartsWith("PART NUMBER"))
                                    iTemCode = dt.Rows[_i3][_itemDesc + 1].ToString().Trim();

                                _i3 += 1;

                                if (_i3 >= dt.Rows.Count)
                                    break;
                            }
                        }

                        // || dt.Rows[_i2][_itemDesc].ToString().ToUpper().StartsWith("PART NUMBER")

                        if (!string.IsNullOrEmpty(descStr))
                            iTemDESC = descStr.Trim();


                        if (!string.IsNullOrEmpty(remark))
                            iTemSUBJ = remark;

                        if (iTemUnit.ToUpper().Equals("NR"))
                            iTemUnit = "PCS";


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

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class SNJ
    {
        DataTable dtIteml;

        string lt;
        string rmk;
        string reference;

        string fileName;
        UnitConverter uc;


        #region ==================================================================================================== Property

        public DataTable ItemL
        {
            get
            {
                return dtIteml;
            }
        }

        public string Reference
        {
            get
            {
                return reference;
            }
        }

        public string Rmk
        {
            get
            {
                return rmk;
            }
        }

        public string Lt
        {
            get
            {
                return lt;
            }
        }

        #endregion ==================================================================================================== Constructor



        public SNJ(string fileName)
        {
            lt = string.Empty;
            rmk = string.Empty;
            reference = string.Empty;

            dtIteml = new DataTable();
            dtIteml.Columns.Add("NO");          // 순번
            dtIteml.Columns.Add("DESC");        // 품목명
            dtIteml.Columns.Add("ITEM");        // 품목코드
            dtIteml.Columns.Add("UNIT");        // 단위
            dtIteml.Columns.Add("QT");          // 수량
            dtIteml.Columns.Add("UNIQ");          // 고유코드
            dtIteml.Columns.Add("UM");          // 단가
            dtIteml.Columns.Add("AM");          // 금액
            dtIteml.Columns.Add("LT");          // 납기
            dtIteml.Columns.Add("RMK");         // 비고

            this.fileName = fileName;
            this.uc = new UnitConverter();
        }

        public void Parse()
        {
            string iTemNo = string.Empty;
            string iTemUm = string.Empty;
            string iTemDC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;
            string iTemRMK = string.Empty;
            string iTemAm = string.Empty;
            string iTemDesc = string.Empty;
            string iTemTotal = string.Empty;
            string iTemCode = string.Empty;
            string iTemType = string.Empty;
            string iTemUniq = string.Empty;

            string iTemRef = string.Empty;
            string itemRMKH = string.Empty;
            string leadTimeStr = string.Empty;


            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemPrice = -1;
            int _itemValue = -1;
            int _itemCode = -1;

            string typeStr = string.Empty;
            string partStr = string.Empty;



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
                    
                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Quotation No."))
                                reference = dt.Rows[i][c].ToString().Replace("Quotation No.","").Replace(":","").Trim();
                        }
                    }

                    if (firstColStr.StartsWith("2.Delivery Time"))
                    {
                        if (string.IsNullOrEmpty(rmk))
                        {
                            rmk = dt.Rows[i - 2][0].ToString().Trim();
                        }

                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            leadTimeStr = leadTimeStr.Trim() + dt.Rows[i][c].ToString().Trim();
                        }
                        lt = leadTimeStr.Replace("2.Delivery Time", "").Replace(":","").Replace("발주 후","").Replace("일","").Trim();
                    }
                    else if (firstColStr.StartsWith("Remark") || firstColStr.StartsWith("-"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            itemRMKH = itemRMKH.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                        rmk = itemRMKH.Replace("Remarks","").Replace(".","").Trim();

                    }
                    else if (firstColStr.StartsWith("NO."))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("PART NO")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Q'TY")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("U/PRICE")) _itemPrice = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("AMOUNT")) _itemValue = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr.Replace(".","")))
                    {
                        iTemUm = "0";
                        iTemAm = "0";

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

                        if ((rowValueSpl[4] != null) && (rowValueSpl[5] == null))
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = -1;
                            _itemQt = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemPrice = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemValue = Convert.ToInt16(rowValueSpl[4].ToString());
                        }
                        else if (rowValueSpl[3] != null && rowValueSpl[4] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemPrice = -1;
                            _itemValue = -1;
                        }


                        

                        if (!_itemQt.Equals(-1))
                        {
                            string[] qtunit = dt.Rows[i][_itemQt].ToString().Split(' ');

                            if (qtunit.Length == 2)
                            {
                                iTemQt = qtunit[0].ToString().Trim();
                                iTemUnit = qtunit[1].ToString().Trim();
                            }
                            else
                            {
                                string dtColStr = dt.Rows[i][_itemQt].ToString();

                                iTemQt = dtColStr.Substring(0, 1).Trim();
                                iTemUnit = dtColStr.Substring(1, dtColStr.Length - 1).Trim();
                            }
                        }

                        if (!_itemPrice.Equals(-1))
                            iTemUm = dt.Rows[i][_itemPrice].ToString().Trim();

                        if (!_itemValue.Equals(-1))
                            iTemAm = dt.Rows[i][_itemValue].ToString().Trim();

                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDesc = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;
                            while (!GetTo.IsInt(dt.Rows[_i][0].ToString()) && !string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemDesc = iTemDesc.Trim() + " " + dt.Rows[_i][0].ToString();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemDesc = iTemDesc.Trim() + " " + dt.Rows[_i][_itemDesc].ToString();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();


                        //ITEM ADD START
                        dtIteml.Rows.Add();
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["NO"] = firstColStr.Replace(".","");
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["DESC"] = iTemDesc;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["ITEM"] = "";
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["QT"] = iTemQt;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["UNIQ"] = iTemCode;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["UM"] = iTemUm;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["AM"] = iTemAm;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["LT"] = "";
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["RMK"] = iTemRMK;

                        iTemDC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemRMK = string.Empty;
                        iTemQt = string.Empty;
                        iTemUm = string.Empty;
                        iTemAm = string.Empty;
                        iTemCode = string.Empty;
                        iTemDesc = string.Empty;
                    }

                }
            }
        }
    }
}

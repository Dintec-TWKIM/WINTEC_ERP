using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class HiAirKorea
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



        public HiAirKorea(string fileName)
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
            int _itemUnit = -1;
            int _itemPrice = -1;
            int _itemValue = -1;

            bool itemStart = false;

            string typeStr = string.Empty;
            string partStr = string.Empty;

            bool noCheck = false;
            

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
                    string noStr = string.Empty;

                    if (!string.IsNullOrEmpty(firstColStr))
                    {
                        noStr = firstColStr.Substring(0, 1).Trim();

                        if (firstColStr.StartsWith("10"))
                            noCheck = true;

                        if (noCheck)
                            noStr = firstColStr.Substring(0, 2).Trim();
                        else if (GetTo.IsInt(firstColStr))
                            noStr = firstColStr;
                    }

                    if (firstColStr.StartsWith("Quotation no. / Date"))
                        itemStart = false;

                    //if(firstColStr.StartsWith("Quotation no"))


                    if (!itemStart)
                    {
                        if (firstColStr.StartsWith("Quotation no."))
                        {
                            reference = dt.Rows[i + 1][0].ToString().Replace("/","").Trim();

                            string[] refSpl = dt.Rows[i + 1][0].ToString().Split('/');

                            if (refSpl.Length == 2)
                            {
                                reference = refSpl[0].ToString().Trim();
                            }

                        }
                        else if (firstColStr.Contains("Lead Time"))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                leadTimeStr = leadTimeStr.Trim() + dt.Rows[i][c].ToString().Trim();
                            }
                            lt = leadTimeStr.Replace("working days after order", "").Trim();
                        }
                        else if (firstColStr.Equals("Remark"))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                itemRMKH = itemRMKH.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                            }

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                for (int c = 1; c < dt.Columns.Count; c++)
                                {
                                    itemRMKH = itemRMKH.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                }
                                
                                _i += 1;

                                if (dt.Rows[_i][0].ToString().StartsWith("No"))
                                    break;
                            }

                            rmk = itemRMKH.Trim();

                        }
                        else if (firstColStr.Equals("No."))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().StartsWith("Description")) _itemDesc = c;
                                else if (dt.Rows[i][c].ToString().StartsWith("Q'ty")) _itemQt = c;
                                else if (dt.Rows[i][c].ToString().StartsWith("Unit")) _itemUnit = c;
                                else if (dt.Rows[i][c].ToString().StartsWith("Price")) _itemPrice = c;
                                else if (dt.Rows[i][c].ToString().StartsWith("Value")) _itemValue = c;

                                itemStart = true;
                            }
                        }
                    }
                    else
                    {
                        if (GetTo.IsInt(noStr))
                        {
                            if (!_itemPrice.Equals(-1))
                                iTemUm = dt.Rows[i][_itemPrice].ToString().Trim();

                            if (!_itemQt.Equals(-1))
                            {
                                iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                                if (string.IsNullOrEmpty(iTemQt))
                                    iTemQt = dt.Rows[i][_itemValue + 1].ToString().Trim();
                            }

                            if (!_itemUnit.Equals(-1))
                                iTemUnit = dt.Rows[i][_itemUnit].ToString();

                            if (!_itemValue.Equals(-1))
                                iTemAm = dt.Rows[i][_itemValue].ToString().Trim();

                            if (!_itemDesc.Equals(-1))
                            {
                                iTemDesc = dt.Rows[i][_itemDesc].ToString().Trim();

                                if(dt.Rows[i +1][_itemDesc].ToString().StartsWith("Type"))
                                    typeStr = dt.Rows[i+1][_itemDesc].ToString().Replace("Type","").Replace(":","").Trim();

                                if (dt.Rows[i + 2][_itemDesc].ToString().StartsWith("Part"))
                                    partStr = dt.Rows[i + 2][_itemDesc].ToString().Replace("Part No","").Replace(".","").Trim();

                                if (string.IsNullOrEmpty(iTemDesc))
                                {
                                    iTemDesc = dt.Rows[i][0].ToString().Trim();

                                    if(!string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                                        iTemDesc = iTemDesc.Substring(1, iTemDesc.Length - 1).Trim();
                                }
                            }

                            iTemCode = partStr.Trim();
                      
                            //ITEM ADD START
                            dtIteml.Rows.Add();
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["NO"] = noStr;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["DESC"] = iTemDesc;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["ITEM"] = typeStr;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if(GetTo.IsInt(iTemQt))
                                dtIteml.Rows[dtIteml.Rows.Count - 1]["QT"] = iTemQt;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["UNIQ"] = iTemCode;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["UM"] = iTemUm;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["AM"] = iTemAm;
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["LT"] = lt;
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
                        else if (firstColStr.Equals("Total Amount"))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                iTemTotal = iTemTotal.Trim() + dt.Rows[i][c].ToString().Replace("KRW", "").Trim();
                            }
                        }
                    }
                }
            }
        }
    }
}

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Minerva
    {
        string vessel;
        string reference;
        DataTable dtItem;
        string imoNumber;

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



        public Minerva(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
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

            int _itemDescInt = -1;
            int _itemQtInt = -1;
            int _itemUnitpriceInt = -1;

            bool subjCheck = false;

            string subjStr = string.Empty;
            string subjStrTitle = string.Empty;

            string subjManuStr = string.Empty;
            string subjModelStr = string.Empty;
            string subjSizeStr = string.Empty;
            string subjSerialStr = string.Empty;
            string subjReqStr = string.Empty;
            string subjTypeStr = string.Empty;

            string subjStr1 = string.Empty;
            string subjStr2 = string.Empty;
            string subjStr3 = string.Empty;

            string descCode = string.Empty;


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
                    string secondColStr = dt.Rows[i][1].ToString();
                    string thirdColStr = dt.Rows[i][2].ToString();

                    if (firstColStr.Contains("THANK YOU AND BEST REGARDS")) break;


                    if(string.IsNullOrEmpty(imoNumber))
					{
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("IMO No"))
                                imoNumber = dt.Rows[i][c].ToString().Trim() + dt.Rows[i][c + 1].ToString().Trim();

                            imoNumber = imoNumber.Replace("IMO", "").Replace("No", "").Replace(":", "").Trim();
                        }
                    }

                    if (firstColStr.Contains("REQUEST FOR QUOTATION"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if(string.IsNullOrEmpty(reference))
                                reference = dt.Rows[i][c].ToString().Replace("REQUEST FOR QUOTATION No.:", "").ToString().Trim();
                        }
                    }
                    else if (firstColStr.Contains("Remarks:"))
                    {
                        i = i + 2;
                        vessel = dt.Rows[i][0].ToString().Trim();

                        if (vessel.Contains("("))
                        {
                            int idx_s = vessel.IndexOf("(");

                            vessel = vessel.Substring(0, idx_s).Trim();
                        }

                        subjCheck = true;

                    }
                    else if (firstColStr.StartsWith("Manufacturer"))
                    {
                        subjStr = dt.Rows[i - 1][0].ToString().Trim();
                        subjManuStr = dt.Rows[i][0].ToString().Trim();

                        int _i = i + 1;
                        while (!dt.Rows[_i][0].ToString().Equals("No."))
                        {
                            subjStr1 = subjStr1 + " " + dt.Rows[_i][0].ToString().Trim();

                            _i += 1;

                            if (_i >= dt.Rows.Count || dt.Rows[_i][0].ToString().ToUpper().Equals("No."))
                                break;

                            subjStr1 = subjStr1.Trim() + Environment.NewLine;
                        }

                        subjStr = subjStr.Replace("Manufacturer", "MAKER").Trim();

                    }
                    else if (secondColStr.StartsWith("Manufacturer"))
                    {
                        subjStr = dt.Rows[i - 1][1].ToString().Trim();
                        subjManuStr = dt.Rows[i][1].ToString().Trim();


                        int _i = i + 1;
                        while (!dt.Rows[_i][0].ToString().Equals("No."))
                        {
                            subjStr1 = subjStr1 + " " + dt.Rows[_i][1].ToString().Trim();

                            _i += 1;

                            if (_i >= dt.Rows.Count || dt.Rows[_i][0].ToString().ToUpper().Equals("No."))
                                break;

                            subjStr1 = subjStr1.Trim() + Environment.NewLine;
                        }

                        subjStr = subjStr.Replace("Manufacturer", "MAKER").Trim();
                    }
                    else if (thirdColStr.StartsWith("Manufacturer"))
                    {
                        subjStr = dt.Rows[i - 1][1].ToString().Trim() + " " +  dt.Rows[i - 1][2].ToString().Trim();
                        subjStr = subjStr.Trim() + Environment.NewLine + thirdColStr.Trim();

                        int _i = i + 1;
                        while (!dt.Rows[_i][0].ToString().Equals("No."))
                        {
                            subjStr1 = subjStr1 + " " + dt.Rows[_i][2].ToString().Trim() + " " + dt.Rows[_i][3].ToString().Trim();

                            _i += 1;

                            if (_i >= dt.Rows.Count || dt.Rows[_i][0].ToString().ToUpper().Equals("No."))
                                break;

                            subjStr1 = subjStr1.Trim() + Environment.NewLine;
                        }

                        subjStr = subjStr.Replace("Manufacturer", "MAKER").Trim();
                    }
                    else if (secondColStr.StartsWith("Size"))
                    {
                        subjSizeStr = dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim();
                    }
                    else if (secondColStr.StartsWith("Serial No"))
                    {
                        subjSerialStr = dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim();
                    }
                    else if (secondColStr.StartsWith("Model"))
                    {
                        subjModelStr = dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim();

                        subjModelStr = subjModelStr.Replace("Model", "").Replace(":", "").Trim();
                    }
                    else if (subjCheck)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            subjStrTitle = subjStrTitle.Trim() + " " + dt.Rows[i + 1][c].ToString().Trim();

                            if (dt.Rows[i + 2][c].ToString().Contains("Manufacturer"))
                            {
                                subjStr = string.Empty;
                                break;
                            }
                            else
                                subjStr = subjStr.Trim() + " " + dt.Rows[i + 2][c].ToString().Trim();
                        }

                        int _i = i + 1;
                        while (!dt.Rows[_i][0].ToString().Equals("No."))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[_i][c].ToString().Equals("Manufacturer:"))
                                    subjManuStr = dt.Rows[_i][c + 1].ToString().Trim();
                                else if (dt.Rows[_i][c].ToString().Contains("Model"))
                                {
                                    subjModelStr = dt.Rows[_i][c + 1].ToString().Trim();

                                    if (!dt.Rows[_i][c + 2].ToString().Equals("Type:"))
                                    {
                                        subjModelStr = subjModelStr.Trim() + " " + dt.Rows[_i][c + 2].ToString().Trim();
                                        subjModelStr = subjModelStr.Replace("Type:", "").Trim();
                                    }
                                    else if (subjModelStr.Equals("Type:"))
                                    {
                                        subjModelStr = string.Empty;
                                        subjTypeStr = dt.Rows[_i][c + 2].ToString().Trim();
                                    }

                                    if (string.IsNullOrEmpty(dt.Rows[_i + 1][c].ToString()))
                                        subjModelStr = subjModelStr.Trim() + " " + dt.Rows[_i][c + 1].ToString().Trim();
                                }

                                else if (dt.Rows[_i][c].ToString().Contains("Size"))
                                    subjSizeStr = dt.Rows[_i][c + 1].ToString().Trim();
                                else if (dt.Rows[_i][c].ToString().Contains("Serial"))
                                    subjSerialStr = dt.Rows[_i][c + 1].ToString().Trim();
                                else if (dt.Rows[_i][c].ToString().Contains("Req."))
                                {
                                    int _subji = _i + 1;
                                    while (!dt.Rows[_subji][0].ToString().Equals("No."))
                                    {
                                        subjStr1 = subjStr1.Trim() + Environment.NewLine + dt.Rows[_subji][c].ToString().Trim();
                                        _subji += 1;
                                    }

                                    subjReqStr = dt.Rows[_i][c + 1].ToString().Trim();

                                    if (!dt.Rows[_i][c + 2].ToString().Contains("Proj."))
                                        subjReqStr = subjReqStr.Trim() + dt.Rows[_i][c + 2].ToString().Trim();
                                }


                            }

                            _i += 1;

                            if (dt.Rows[_i][0].ToString().Equals("No."))
                                break;
                        }

                        subjCheck = false;
                    }
                    else if (firstColStr.Equals("No."))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Qty")) _itemQtInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Description")) _itemDescInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Unit Price")) _itemUnitpriceInt = c;
                        }

                        int _subji = i + 1;

                        while (!GetTo.IsInt(dt.Rows[_subji][0].ToString()))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                subjStr2 = subjStr2.Trim() + " " + dt.Rows[_subji][c].ToString().Trim();
                            }
                            _subji += 1;
                        }

                    }
                    else if (firstColStr.StartsWith("PLEASE ADVISE US AVAILABILITY, "))
                    {
                        if (string.IsNullOrEmpty(vessel))
                        {
                            vessel = dt.Rows[i + 1][0].ToString().Trim();

                            if (vessel.Contains("("))
                            {
                                int idx_s = vessel.IndexOf("(");

                                vessel = vessel.Substring(0, idx_s).Trim();
                            }
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {

                        if (!_itemQtInt.Equals(-1))
                        {
                            string[] qtunitValueSpl = dt.Rows[i][_itemQtInt].ToString().Split(' ');

                            if (qtunitValueSpl.Length > 1)
                            {
                                iTemQt = qtunitValueSpl[0].ToString().Trim();
                                iTemUnit = qtunitValueSpl[1].ToString().Trim();
                            }
                        }

                        if (!_itemDescInt.Equals(-1))
                        {
                            for (int c = _itemDescInt; c < _itemQtInt; c++)
                            {
                                descCode = descCode.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                            }

                            //iTemDESC = iTemDESC.Trim() + ",";

                            int _i = i + 1;
                            while (!GetTo.IsInt(dt.Rows[_i][0].ToString()))
                            {
                                if (dt.Rows[_i][0].ToString().Length > 2 || string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                {
                                    if (dt.Rows[_i][0].ToString().StartsWith("PLEASE REPLY") || dt.Rows[_i][0].ToString().StartsWith("IMPORTANT") || dt.Rows[_i][0].ToString().StartsWith("No") || dt.Rows[_i][0].ToString().StartsWith("REQUEST FOR QUOTATION"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        for (int c = 0; c < dt.Columns.Count; c++) // 수정 후
                                        //for (int c = 0; c < _itemQtInt; c++) // 수정 전
                                        {
                                            if (!dt.Rows[_i][c].ToString().Equals("Total:") && !string.IsNullOrEmpty(dt.Rows[_i][c].ToString()))
                                                iTemDESC = iTemDESC + " " + dt.Rows[_i][c].ToString().Trim();
                                        }
                                    }
                                }
                                _i += 1;



                                if (_i == dt.Rows.Count - 1)
                                    break;

                                //iTemDESC = iTemDESC.Trim() + ",";
                            }
                        }

                        if (!string.IsNullOrEmpty(subjStrTitle))
                            iTemSUBJ = subjStrTitle.Trim();

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr.Trim();

                        if (!string.IsNullOrEmpty(subjManuStr.Replace("Manufacturer", "").Replace(":", "").Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + subjManuStr.Replace("Manufacturer", "").Replace(":", "").Trim();

                        if (!string.IsNullOrEmpty(subjModelStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + subjModelStr.Trim();

                        if (!string.IsNullOrEmpty(subjSizeStr.Replace("Size", "").Replace(":", "").Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "SIZE: " + subjSizeStr.Replace("Size", "").Replace(":", "").Trim();

                        if (!string.IsNullOrEmpty(subjSerialStr.Replace("Serial No", "").Replace(":", "").Replace(".", "").Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + subjSerialStr.Replace("Serial No", "").Replace(":", "").Replace(".", "").Trim();

                        if (!string.IsNullOrEmpty(subjStr1))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr1.Trim();

                        if (!string.IsNullOrEmpty(subjStr2))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr2.Trim();

                        if (iTemDESC.EndsWith(","))
                            iTemDESC = iTemDESC.Substring(0, iTemDESC.Length - 1).Trim();

                        if (iTemDESC.EndsWith(","))
                            iTemDESC = iTemDESC.Substring(0, iTemDESC.Length - 1).Trim();

                        if (iTemDESC.EndsWith(","))
                            iTemDESC = iTemDESC.Substring(0, iTemDESC.Length - 1).Trim();

                        if (iTemDESC.EndsWith(","))
                            iTemDESC = iTemDESC.Substring(0, iTemDESC.Length - 1).Trim();

                        if (!string.IsNullOrEmpty(descCode))
                            iTemDESC = iTemDESC.Trim() + ", " + descCode.Trim();

                        //if (!string.IsNullOrEmpty(subjReqStr))
                        //    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "REQ. NO.: " + subjReqStr.Trim();

                        if (iTemSUBJ.Contains("\r\n "))
                        {
                            while (iTemSUBJ.Contains("\r\n "))
                            {
                                iTemSUBJ = iTemSUBJ.Replace("\r\n ", "\r\n").Trim();
                            }
                        }


                        iTemCode = iTemCode.Trim();
                        iTemDESC = iTemDESC.Trim();
                        iTemSUBJ = iTemSUBJ.Trim();

                        if (iTemDESC.StartsWith(","))
                            iTemDESC = iTemDESC.Substring(1, iTemDESC.Length - 1).Trim();

                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemSUBJ = string.Empty;
                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemQt = string.Empty;
                        iTemCode = string.Empty;
                        descCode = string.Empty;
                    }
                }
            }
        }
    }
}

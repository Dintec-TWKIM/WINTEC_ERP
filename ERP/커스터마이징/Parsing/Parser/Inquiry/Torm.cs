using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Torm
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



        public Torm(string fileName)
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

            int _itemQt = -1;
            int _itemDesc = -1;

            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string subjStr = string.Empty;
            string subjStr2 = string.Empty;
            string dwgStr = string.Empty;

            string rmkStr = string.Empty;

            bool remarkCh = false;


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


                    if (!remarkCh)
                    {
                        for (int r = 0; r < dt.Rows.Count; r++)
                        {
                            if (dt.Rows[r][0].ToString().Contains("Remark"))
                            {
                                int _r = r + 1;
                                if (remarkCh)
                                    break;

                                while (!dt.Rows[_r][1].ToString().Contains("c/o TORM A/S"))
                                {
                                    rmkStr = rmkStr + Environment.NewLine + dt.Rows[_r][0].ToString().Trim();

                                    _r += 1;

                                    if (_r >= dt.Rows.Count)
                                    {
                                        remarkCh = true;
                                        break;
                                    }
                                }

                                remarkCh = true;
                            }
                        }
                            
                    }

                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Request for Quote"))
                            {
                                reference = dt.Rows[i][c].ToString().Trim() + dt.Rows[i][c + 1].ToString().Trim();
                                reference = reference.Replace("Request for Quote:", "").Trim();

                                if (string.IsNullOrEmpty(reference))
                                    reference = dt.Rows[i][c + 2].ToString().Trim();
                            }
                        }
                    }

                    if (firstColStr.StartsWith("IMO") && string.IsNullOrEmpty(vessel))
                    {
                        vessel = dt.Rows[i - 1][0].ToString().Trim();
                    }
                    else if (firstColStr.ToLower().StartsWith("description"))
                    {
                        for(int c = 1; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        int _i = i + 1;

                        while(!dt.Rows[_i][0].ToString().ToLower().Contains("please quote your best") && !dt.Rows[_i][0].ToString().ToLower().Contains("pos."))
                        {
                            for(int c =0; c < dt.Columns.Count; c++)
                            {
                                subjStr = subjStr + " " + dt.Rows[_i][c].ToString().Trim();
                            }

                            _i += 1;

                            subjStr = subjStr.Trim() + Environment.NewLine;

                            if (_i >= dt.Rows.Count || GetTo.IsInt(dt.Rows[_i][0].ToString()))
                                break;
                        }

                        if (string.IsNullOrEmpty(subjStr))
                            subjStr = dt.Rows[i][1].ToString().Trim();
                    }
                    //else if (firstColStr.StartsWith("Name:") )
                    //{
                    //    if(string.IsNullOrEmpty(subjStr))
                    //        subjStr = firstColStr.Replace("Name","").Replace(":","").Trim();
                    //    else
                    //        subjStr2 = firstColStr.Replace("Name", "").Replace(":", "").Trim();
                    //}
                    //else if (firstColStr.StartsWith("Manufacturer"))
                    //{
                    //    makerStr = firstColStr.Replace("Manufacturer", "").Replace(":", "").Trim();
                    //}
                    //else if (secondColStr.StartsWith("Manufacturer"))
                    //{
                    //    makerStr = secondColStr.Replace("Manufacturer", "").Replace(":", "").Trim();
                    //}
                    //else if (firstColStr.StartsWith("Type No"))
                    //{
                    //    typeStr = firstColStr.Replace("Type No.", "").Replace(":", "").Trim();
                    //}
                    //else if (secondColStr.StartsWith("Type No"))
                    //{
                    //    typeStr = secondColStr.Replace("Type No.", "").Replace(":", "").Trim();
                    //}
                    //else if (firstColStr.StartsWith("Drawing No.:"))
                    //{
                    //    dwgStr = firstColStr.Replace("Drawing No.:", "").Trim();
                    //}
                    //else if (secondColStr.StartsWith("Drawing No.:"))
                    //{
                    //    dwgStr = secondColStr.Replace("Drawing No.:", "").Trim();
                    //}
                    else if (firstColStr.StartsWith("Pos."))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToLower().StartsWith("req. qty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().ToLower().StartsWith("quantity")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().ToLower().Equals("description")) _itemDesc = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        //if (!_itemQt.Equals(-1))
                        //{
                        //    iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                        //    iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Trim();
                        //}

                        //for (int c = _itemQt + 2; c < dt.Columns.Count; c++)
                        //{
                        //    iTemDESC = iTemDESC + dt.Rows[i][c].ToString().Trim();
                        //    iTemDESC = iTemDESC.Trim() + Environment.NewLine;
                        //}

                        if(_itemQt != -1)
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Replace(",",".").Trim();

                            if(iTemQt.Contains("/"))
                            {
                                string[] qtSpl = iTemQt.Split('/');

                                if(qtSpl.Length == 2)
                                {
                                    iTemQt = qtSpl[0].ToString();
                                }
                            }

                            iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Trim();

                            if(iTemUnit.Length > 4)
							{
                                iTemUnit = dt.Rows[i + 1][_itemQt].ToString().Trim();
							}
                        }

                        if(_itemDesc != -1)
                        {
                            if (dt.Rows[i][_itemQt + 1].ToString().Length > 7)
                            {
                                for (int c = _itemQt + 1; c <= _itemDesc; c++)
                                {
                                    iTemDESC = iTemDESC + " " + dt.Rows[i][c].ToString().Trim();
                                }
                            }
                            else
							{
                                for (int c = _itemDesc; c <= _itemDesc; c++)
                                {
                                    iTemDESC = iTemDESC + " " + dt.Rows[i][c].ToString().Trim();
                                }
                            }




							//int _i = i + 1;

							//while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
							//{
							//	for (int c = _itemQt + 2; c <= _itemDesc; c++)
							//	{
							//		if (dt.Rows[_i][c].ToString().Contains("Printed") || dt.Rows[_i][c].ToString().Contains("Page") || dt.Rows[_i][c].ToString().Contains("TORM A/S") || dt.Rows[_i][c].ToString().Contains("Phone") || dt.Rows[_i][c].ToString().Contains("Sub total") || dt.Rows[_i][c].ToString().Contains("Grand total") || dt.Rows[_i][c].ToString().Contains("Discount"))
							//			break;
							//		iTemDESC = iTemDESC + " " + dt.Rows[_i][c].ToString().Trim();
							//	}

							//	_i += 1;


							//	if (_i >= dt.Rows.Count || iTemDESC.Contains("Printed") || iTemDESC.Contains("Page") || iTemDESC.Contains("TORM A/S") || iTemDESC.Contains("Phone") || iTemDESC.Contains("Sub total") || iTemDESC.Contains("Grand total") || iTemDESC.Contains("Discount"))
							//	{
							//		iTemDESC = iTemDESC.Replace("Sub total", "").Trim();
							//		break;
							//	}
							//	else
							//	{
							//		iTemDESC = iTemDESC.Trim() + Environment.NewLine;
							//	}
							//}

							int idx_s = xmlTemp.IndexOf(iTemDESC.Replace("\r\n","").Trim());

                            string[] separatingStrings = { "\r\n" };


                            if (idx_s != -1)
                            {
                                string descTemp1 = xmlTemp.Substring(idx_s, xmlTemp.Length - idx_s);
                                string[] descTemp_Spl = descTemp1.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

                                if (descTemp_Spl.Length > 0)
                                {
                                    string descStr1 = descTemp_Spl[0].ToString();

                                    int idx_e = descStr1.IndexOf("</");

                                    if (idx_e != -1)
                                    {
                                        iTemDESC = descStr1.Substring(0, idx_e);
                                    }
                                }
                            }

                        }


                        //int _i = i + 1;
                        //while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                        //{
                        //    for (int c = _itemQt + 2; c < dt.Columns.Count; c++)
                        //    {
                        //        if (dt.Rows[_i][c].ToString().Contains("Printed") || dt.Rows[_i][c].ToString().Contains("Page") || dt.Rows[_i][c].ToString().Contains("TORM A/S") || dt.Rows[_i][c].ToString().Contains("Phone") || dt.Rows[_i][c].ToString().Contains("Sub total") || dt.Rows[_i][c].ToString().Contains("Grand total") || dt.Rows[_i][c].ToString().Contains("Discount"))
                        //            break;

                        //        iTemDESC = iTemDESC + dt.Rows[_i][c].ToString().Trim();
                        //    }

                        //    _i += 1;

                        //    if (_i >= dt.Rows.Count || iTemDESC.Contains("Printed") || iTemDESC.Contains("Page") || iTemDESC.Contains("TORM A/S") || iTemDESC.Contains("Phone") || iTemDESC.Contains("Sub total") || iTemDESC.Contains("Grand total") || iTemDESC.Contains("Discount"))
                        //    {
                        //        iTemDESC = iTemDESC.Replace("Sub total", "").Trim();
                        //        break;
                        //    }
                        //    else
                        //    {
                        //        iTemDESC = iTemDESC.Trim() + Environment.NewLine;
                        //    }
                        //}

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        //if (!string.IsNullOrEmpty(makerStr))
                        //    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        //if (!string.IsNullOrEmpty(typeStr))
                        //    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                        //if (!string.IsNullOrEmpty(subjStr2))
                        //    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr2.Trim();

                        //if(!string.IsNullOrEmpty(dwgStr))
                        //    iTemDESC = iTemDESC.Trim() + Environment.NewLine + "Drawing No: " + dwgStr.Trim();

                        //if (!string.IsNullOrEmpty(rmkStr))
                        //    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + rmkStr.Trim();


                        //iTemSUBJ = iTemSUBJ.Replace("\r\n\r\n", "\r\n").Trim();
                        
                        iTemDESC = iTemDESC.Trim();

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

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Nakilat
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



        public Nakilat(string fileName)
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
            dtItem.Columns.Add("UNIQ");         //선사코드

            this.fileName = fileName;
            this.uc = new UnitConverter();
        }

        public void Parse()
        {
            int _itemNo = 0;

            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemUnit = -1;

            int _itemVesselCol = -1;

            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string subjStr = string.Empty;

            bool descSt = false;


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
                    string firstColString = dt.Rows[i][0].ToString();
                    string thirdColStr = string.Empty; 

                    if(string.IsNullOrEmpty(vessel))
                    {
                        for(int c = 1; c < dt.Columns.Count; c++)
                        {
                            if(dt.Rows[i][c].ToString().Contains("Ship"))
                                vessel = dt.Rows[i][c].ToString().Replace(":", "").Replace("Ship", "").Trim() + dt.Rows[i][c+1].ToString().Replace(":", "").Replace("Ship", "").Trim();

                        }
                    }

                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("number/date"))
                            {
                             string[] refSpl = dt.Rows[i + 1][c].ToString().Split('/');

                                if (refSpl[0] != null)
                                    reference = refSpl[0].ToString().Trim();
                            }
                        }
                    }

                    // 선명 및 문의번호 컬럼 확인
                    //if (_itemVesselCol.Equals(-1))
                    //{
                    //    for (int c = 1; c < dt.Columns.Count; c++)
                    //    {
                    //        if (dt.Rows[i][c].ToString().Contains("Request For Quotation"))
                    //            _itemVesselCol = c;
	                    //}
                    //else if (string.IsNullOrEmpty(vessel))
                    //{
                    //    thirdColStr = dt.Rows[i][_itemVesselCol].ToString();
                    //}


                    //if (thirdColStr.Contains("RFQ number/date"))
                    //{
                    //    string[] refSpl = dt.Rows[i + 1][2].ToString().Split('/');

                    //    if (refSpl[0] != null)
                    //        reference = refSpl[0].ToString().Trim();
                    //}
                    //else if (thirdColStr.Contains("Ship"))
                    //{
                    //    for (int c = _itemVesselCol; c < dt.Columns.Count; c++)
                    //    {
                    //        vessel = vessel + " " + dt.Rows[i][c].ToString().Replace(":", "").Replace("Ship","").Trim();
                    //    }

                    //    vessel = vessel.Trim();
                    //}


                    if (firstColString.Equals("Item"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Material")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("RFQ Quantity") || dt.Rows[i][c].ToString().Contains("Quantity")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColString) && firstColString.StartsWith("0"))
                    {
                        _itemNo += 1;

                        if (!_itemNo.Equals(-1))
                            iTemNo = Convert.ToString(_itemNo);

                        if (!_itemDesc.Equals(-1))
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (!_itemUnit.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        int _i = i + 1;
                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                        {
                            for (int c = _itemDesc; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[_i][c].ToString().StartsWith("Manu"))
                                {
                                    for (int d = _itemDesc; d < dt.Columns.Count; d++)
                                    {
                                        iTemCode = iTemCode + dt.Rows[_i][d].ToString().Replace("Your material number", "").Replace("Manu Ref", "").Replace(":","").Trim();
                                    }
                                }
                                else if (dt.Rows[_i][c].ToString().StartsWith("Maker"))
                                {
                                    for (int d = _itemDesc; d < dt.Columns.Count; d++)
                                    {
                                        makerStr = makerStr + dt.Rows[_i][d].ToString().Replace("Maker", "").Replace(":","").Trim();
                                    }
                                }
                                else if (dt.Rows[_i][c].ToString().StartsWith("Type"))
                                {
                                    for (int d = _itemDesc; d < dt.Columns.Count; d++)
                                    {
                                        typeStr = typeStr + dt.Rows[_i][d].ToString().Replace("Type","").Trim();
                                    }
                                }
                                else if (dt.Rows[_i][c].ToString().StartsWith("Serl"))
                                {
                                    for (int d = _itemDesc; d < dt.Columns.Count; d++)
                                    {
                                        serialStr = serialStr + dt.Rows[_i][d].ToString().Replace("Serl.", "").Replace(":","").Trim();
                                    }
                                }
                                else if (dt.Rows[_i][c].ToString().StartsWith("Comp."))
                                {
                                    for (int d = _itemDesc; d < dt.Columns.Count; d++)
                                    {
                                        subjStr = subjStr + dt.Rows[_i][d].ToString().Replace("Comp.","").Replace(":","").Trim();
                                    }
                                }
                                else if (string.IsNullOrEmpty(dt.Rows[_i][c].ToString()))
                                {
                                    for (int d = _itemDesc; d < dt.Columns.Count; d++)
                                    {
                                        if (dt.Rows[_i][d].ToString().StartsWith("Your material"))
                                        {
                                            descSt = true;
                                            break;
                                        }

                                        if (dt.Rows[_i][d].ToString().StartsWith("Maker"))
                                        {
                                            descSt = true;
                                            break;
                                        }

                                        if (dt.Rows[_i][d].ToString().StartsWith("Type"))
                                        {
                                            descSt = true;
                                            break;
                                        }

                                        if (dt.Rows[_i][d].ToString().StartsWith("Comp."))
                                        {
                                            descSt = true;
                                            break;
                                        }

                                        if (!iTemDESC.Contains(dt.Rows[_i][d].ToString()) && !descSt)
                                            iTemDESC = iTemDESC + " " + dt.Rows[_i][d].ToString().Trim();
                                    }
                                }
                                else
                                {
                                    iTemDESC = iTemDESC + " " + dt.Rows[i][c].ToString().Trim();
                                }
                            }

                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();



                        iTemDESC = iTemDESC.Trim();

                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".","")))
                            if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                        subjStr = string.Empty;

                        typeStr = string.Empty;
                        serialStr = string.Empty;
                        subjStr = string.Empty;
                        makerStr = string.Empty;

                        descSt = false;
                        
                    }
                }
            }
        }
    }
}

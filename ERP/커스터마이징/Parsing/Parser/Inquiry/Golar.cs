using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Golar
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



        public Golar(string fileName)
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

            int _itemQtInt = -1;
            int _itemUnitInt = -1;
            int _itemDescInt = -1;
            int _itemNoInt = -1;

            string subjStr = string.Empty;
            string subjStr2 = string.Empty;

            string makerStr = string.Empty;
            string makerStr2 = string.Empty;
            string dwgStr = string.Empty;


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
                    string firstColStr = dt.Rows[i][0].ToString().ToLower();

                    string secondColStr = string.Empty;
                    if (!_itemNoInt.Equals(-1))
                        secondColStr = dt.Rows[i][_itemNoInt].ToString();

                    if(string.IsNullOrEmpty(reference))
                    {
                        for(int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToLower().StartsWith("inquiry no"))
                                reference = dt.Rows[i][c + 1].ToString().Trim() + dt.Rows[i][c + 2].ToString().Trim();
                        }
                    }
                    else if (firstColStr.Contains("vessel:"))
                    {
                        vessel = dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString();
                    }
                    else if (firstColStr.Contains("description"))
                    {
                        subjStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr.Trim() + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.Contains("component"))
                    {
                        subjStr2 = string.Empty;

                        subjStr2 = subjStr2 + Environment.NewLine;

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            subjStr2 = subjStr2 + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.Contains("maker"))
                    {
                        makerStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            makerStr = makerStr + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.ToUpper().Contains("ITEM NO") || firstColStr.ToUpper().Contains("INQUIRY NO:"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (GetTo.IsInt(dt.Rows[i + 1][c].ToString()))
                            {
                                _itemNoInt = c;
                                if (!string.IsNullOrEmpty(dt.Rows[i + 1][c + 1].ToString()))
                                {
                                    _itemQtInt = c + 1;
                                    _itemUnitInt = c + 2;
                                    _itemDescInt = c + 3;
                                }
                                else
                                {
                                    _itemQtInt = c + 2;
                                    _itemUnitInt = c + 3;
                                    _itemDescInt = c + 4;
                                }

                                break;
                            }
                        }
                    }
                    else if (GetTo.IsInt(secondColStr))
                    {
                        if (!_itemQtInt.Equals(-1))
                        {
                            iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();

                            if (iTemQt.Contains(","))
                            {
                                string[] qtSpl = iTemQt.Split(',');
                                iTemQt = qtSpl[0].ToString().Trim();
                            }
                        }

                        if (!_itemUnitInt.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();

                        if (!_itemDescInt.Equals(-1))
                        {
                            for (int c = _itemDescInt; c < dt.Columns.Count; c++)
                            {
                                iTemDESC = iTemDESC + dt.Rows[i][c].ToString().Trim();
                            }
                            int _i = i + 1;
                            while (!GetTo.IsInt(dt.Rows[_i][_itemNoInt].ToString()) && !dt.Rows[_i][0].ToString().Contains("INQUIRY NO:"))
                            {
                                for (int c = 0; c < dt.Columns.Count; c++)
                                {
                                    if (dt.Rows[_i][c].ToString().Equals("Makers:"))
                                        makerStr2 = dt.Rows[_i][c + 1].ToString().Trim();
                                    else if (dt.Rows[_i][c].ToString().Equals("Maker's No:"))
                                        iTemCode = dt.Rows[_i][c + 1].ToString().Trim();
                                    else if (dt.Rows[_i][c].ToString().Equals("Drawing No:"))
                                        dwgStr = dt.Rows[_i][c + 1].ToString().Trim();
                                }
                                
                                _i += 1;

                                if (!(_i < dt.Rows.Count - 3))
                                    break;
                            }
                        }

                        if (!string.IsNullOrEmpty(subjStr2))
                            iTemSUBJ = subjStr2.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(dwgStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG NO.: " + dwgStr.Trim();

                        iTemSUBJ = iTemSUBJ.Replace("Maker's No:", "").Replace("Model:", "\r\nModel:").Trim();


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = secondColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemDESC = string.Empty;
                        iTemQt = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                        iTemSUBJ = string.Empty;
                    }
                }
            }
        }
    }
}

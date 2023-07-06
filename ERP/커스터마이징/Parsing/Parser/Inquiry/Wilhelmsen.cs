using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Wilhelmsen
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



        public Wilhelmsen(string fileName)
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

            string makerStr = string.Empty;
            string subjStr = string.Empty;
            string modelStr = string.Empty;


            string descMaker = string.Empty;
            string descCode = string.Empty;
            string descDwg = string.Empty;
            string descOurCode = string.Empty;
            

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

                    if (firstColStr.Contains("INQUIRY NO"))
                    {
                        reference = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.Contains("Re: Vessel:"))
                    {
                        vessel = firstColStr.ToUpper().Replace("RE:", "").Replace("VESSEL:", "").Trim();
                    }
                    else if (firstColStr.Contains("Description"))
                    {
                        int _i = i +1;
                        while (!dt.Rows[_i][0].ToString().Contains("Item No"))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[_i][c].ToString().Contains("Component"))
                                {
                                    
                                }
                            }
                                _i += 1;
                        }

                        iTemSUBJ = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.Contains("Item No."))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Qty")) _itemQtInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Unit")) _itemUnitInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Description")) _itemDescInt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if(_itemQtInt != -1)
                            iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();

                        if(_itemUnitInt != -1)
                            iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();

                        if (_itemDescInt != -1)
                        {
                            iTemDESC = dt.Rows[i][_itemDescInt].ToString().Trim();

                            if (dt.Rows[i + 1][_itemDescInt].ToString().StartsWith("Makers"))
                            {
                                for (int c = _itemDescInt; c < dt.Columns.Count; c++)
                                {
                                    descMaker = descMaker.Trim() + " " + dt.Rows[i + 1][c].ToString().Trim();
                                }

                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + descMaker;
                            }


                            if (dt.Rows[i + 2][_itemDescInt].ToString().StartsWith("Maker"))
                            {
                                for (int c = _itemDescInt; c < dt.Columns.Count; c++)
                                {
                                    descCode = descCode.Trim() + " " + dt.Rows[i + 2][c].ToString().Trim();
                                }

                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + descCode;
                            }


                            if (dt.Rows[i + 3][_itemDescInt].ToString().StartsWith("Drawing No"))
                            {
                                for (int c = _itemDescInt; c < dt.Columns.Count; c++)
                                {
                                    descDwg = descDwg.Trim() + " " + dt.Rows[i + 3][c].ToString().Trim();
                                }

                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + descDwg;
                            }

                            if (dt.Rows[i + 4][_itemDescInt].ToString().StartsWith("Our Part"))
                            {
                                for (int c = _itemDescInt; c < dt.Columns.Count; c++)
                                {
                                    descOurCode = descOurCode.Trim() + " " + dt.Rows[i + 4][c].ToString().Trim();
                                }

                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + descOurCode;
                            }

                        }

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
                        iTemQt = string.Empty;
                        iTemCode = string.Empty;

                        descOurCode = string.Empty;
                        descCode = string.Empty;
                        descDwg = string.Empty;
                        descMaker = string.Empty;
                    }
                }
            }
        }
    }
}

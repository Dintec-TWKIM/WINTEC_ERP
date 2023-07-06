using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class QatarNavigation_pdf
    {
        string vessel;
        string reference;
        string contact;
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

        public string Contact
        {
            get
            {
                return contact;
            }
        }

        #endregion ==================================================================================================== Constructor



        public QatarNavigation_pdf(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            contact = string.Empty;

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

            string subjNameStr = string.Empty;
            string subjMakerStr = string.Empty;
            string subjTypeStr = string.Empty;
            string subjSerialStr = string.Empty;
            string subjDwgStr = string.Empty;

            int _subjNameInt = -1;
            int _subjMakerInt = -1;
            int _subjTypeInt = -1;
            int _subjSerialInt = -1;
            int _subjDrawingInt = -1;

            int _itemDescInt = -1;
            int _itemCodeInt = -1;
            int _itemQtInt = -1;
            int _itemUnitInt = -1;

            int _lineCount = -1;

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


                    if (string.IsNullOrEmpty(contact))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Contact Person:"))
                            {
                                contact = dt.Rows[i][c + 1].ToString().Trim() + dt.Rows[i][c + 2].ToString().Trim();
                            }
                        }
                    }

                    if (firstColStr.Contains("Vessel Name"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(vessel))
                                vessel = dt.Rows[i][c].ToString().Trim();
                            else
                                break;
                        }
                    }
                    else if (firstColStr.Contains("Our Reference"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(reference))
                                reference = dt.Rows[i][c].ToString().Trim();
                            else
                                break;
                        }
                    }
                    else if (firstColStr.ToUpper().Contains("NAME") || secondColStr.ToUpper().Contains("NAME") || thirdColStr.ToUpper().Contains("NAME"))
                    {
                        _subjNameInt = 0;
                        
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToUpper().Equals("NAME")) _subjNameInt = c;
                            else if (dt.Rows[i][c].ToString().ToUpper().Equals("MAKER")) _subjMakerInt = c;
                            else if (dt.Rows[i][c].ToString().ToUpper().Equals("TYPE")) _subjTypeInt = c;
                            else if (dt.Rows[i][c].ToString().ToUpper().Equals("SERIAL NO")) _subjSerialInt = c;
                            else if (dt.Rows[i][c].ToString().ToUpper().Equals("DRAWING")) _subjDrawingInt = c;
                        }


                        int _subji = i + 1;
                        while (!dt.Rows[_subji][0].ToString().ToUpper().Contains("LINE"))
                        {

                            if (!_subjNameInt.Equals(-1))
                            {
                                if(!dt.Rows[_subji][_subjNameInt].ToString().Contains("http://"))
                                    subjNameStr = subjNameStr.Trim() + " " + dt.Rows[_subji][_subjNameInt].ToString().Trim();
                            }

                            if (!_subjMakerInt.Equals(-1))
                                subjMakerStr = subjMakerStr.Trim() + " " + dt.Rows[_subji][_subjMakerInt].ToString().Trim();

                            if (!_subjTypeInt.Equals(-1))
                                subjTypeStr = subjTypeStr.Trim() + " " + dt.Rows[_subji][_subjTypeInt].ToString().Trim();

                            if (!_subjSerialInt.Equals(-1))
                                subjSerialStr = subjSerialStr.Trim() + " " + dt.Rows[_subji][_subjSerialInt].ToString().Trim();

                            if (!_subjDrawingInt.Equals(-1))
                                subjDwgStr = subjDwgStr.Trim() + " " + dt.Rows[_subji][_subjDrawingInt].ToString().Trim();

                            _subji += 1;

                            if (dt.Rows.Count <= _subji)
                                break;
                        }

                    }
                    else if (firstColStr.Contains("Line") || secondColStr.Contains("Line") || thirdColStr.Contains("Line"))
                    {
                        _lineCount = i + 1;

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Full Description")) _itemDescInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Quantity") || dt.Rows[i][c].ToString().Equals("Qty")) _itemQtInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Unit Type") || dt.Rows[i][c].ToString().Equals("Unit")) _itemUnitInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Part")) _itemCodeInt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr) || GetTo.IsInt(secondColStr) || GetTo.IsInt(thirdColStr))
                    {
                        iTemNo = firstColStr;

                        if (string.IsNullOrEmpty(iTemNo))
                            iTemNo = secondColStr;

                        if (!_itemUnitInt.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();

                        if (!_itemQtInt.Equals(-1))
                        {
                            iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();


                            if (!GetTo.IsInt(iTemQt) && string.IsNullOrEmpty(iTemQt))
                            {
                                iTemUnit = dt.Rows[i][_itemUnitInt + 1].ToString().Trim();
                                iTemQt = dt.Rows[i][_itemQtInt + 1].ToString().Trim();
                            }
                        }

                        if (!_itemDescInt.Equals(-1))
                        {
                            if (_lineCount < i)
                            {
                                for (int r = _lineCount; r < i; r++)
                                {
                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[r][_itemDescInt].ToString().Trim();
                                }
                            }

                            iTemDESC = iTemDESC.Trim() + " " +dt.Rows[i][_itemDescInt].ToString().Trim();

                            int _i = i + 1;
                            if (!string.IsNullOrEmpty(firstColStr))
                            {
                                while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                {
                                    iTemDESC = iTemDESC + " " + dt.Rows[_i][_itemDescInt].ToString().Trim();

                                    _i += 1;

                                    _lineCount = _i;

                                    if (_i == dt.Rows.Count - 1)
                                        break;
                                }
                            }
                            else
                            {
                                while (string.IsNullOrEmpty(dt.Rows[_i][1].ToString()))
                                {
                                    iTemDESC = iTemDESC + " " + dt.Rows[_i][_itemDescInt].ToString().Trim();

                                    _i += 1;

                                    _lineCount = _i;

                                    if (_i == dt.Rows.Count - 1)
                                        break;
                                }
                            }
                        }

                        if (!_itemCodeInt.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCodeInt].ToString().Trim();



                        
                        if (!string.IsNullOrEmpty(subjNameStr.Trim()))
                            iTemSUBJ = subjNameStr;

                        if (!string.IsNullOrEmpty(subjMakerStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + subjMakerStr.Trim();

                        if (!string.IsNullOrEmpty(subjTypeStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + subjTypeStr.Trim();

                        if (!string.IsNullOrEmpty(subjSerialStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + subjSerialStr.Trim();

                        if (!string.IsNullOrEmpty(subjDwgStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG NO: " + subjDwgStr.Trim();

                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC.Trim();
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemSUBJ = string.Empty;
                        iTemDESC = string.Empty;
                        iTemQt = string.Empty;
                        iTemUnit = string.Empty;
                    }
                }
            }
        }
    }
}

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Kyklades
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



        public Kyklades(string fileName)
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
            int _itemQt = -1;

            int _noNum = -1;

            string subjName = string.Empty;
            string makerStr = string.Empty;
            string modelStr = string.Empty;
            string typeStr = string.Empty;
            string sizeStr = string.Empty;
            string serialStr = string.Empty;
            string subjStr = string.Empty;
            string drwStr = string.Empty;



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

                    if (firstColStr.Contains("REQUEST FOR QUOTATION"))
                    {
                        reference = firstColStr.Replace("REQUEST FOR QUOTATION No.:", "").Trim();
                    }
                    else if (firstColStr.Contains("Ship Name:"))
                    {
                        vessel = firstColStr.Replace("Ship Name:", "").Replace("M/V","").Replace("M/T","").Trim();
                    }
                    else if (firstColStr.Equals("No."))
                    {
                        _noNum = i;

                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString() == "Description") _itemDescInt = c;            // 품목명
                            else if (dt.Rows[i][c].ToString().Contains("Qty")) _itemQt = c;             // 수량
                        }
                    }
                    else if (_noNum + 3 == i && !_noNum.Equals(-1))
                    {
                        subjName = string.Empty;
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            subjName = subjName.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                        subjName = subjName.Trim() + Environment.NewLine;
                        int _i = i + 1;
                        while (!dt.Rows[_i][0].ToString().Contains("Manufacturer"))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                subjName = subjName + dt.Rows[_i][c].ToString().Trim();
                            }

                            _i += 1;
                        }
                    }
                    else if (firstColStr.Contains("Manufacturer"))
                    {
                        makerStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.Contains("Model:"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Model"))
                                modelStr = dt.Rows[i][c + 1].ToString().Trim();
                            else if (dt.Rows[i][c].ToString().Contains("Type"))
                                typeStr = dt.Rows[i][c + 1].ToString().Trim();
                        }

                        int _i = i + 1;
                        while (!GetTo.IsInt(dt.Rows[_i][0].ToString()))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[_i][c].ToString().Contains("Serial No"))
                                    serialStr = dt.Rows[_i][c + 1].ToString().Trim();
                                else if (dt.Rows[_i][c].ToString().Contains("Size"))
                                    sizeStr = dt.Rows[_i][c + 1].ToString().Trim();
                                else if (dt.Rows[_i][c].ToString().Contains("Assembly"))
                                {
                                    subjStr = dt.Rows[_i][c].ToString().Trim();

                                    int _ii = _i + 1;
                                    while (string.IsNullOrEmpty(dt.Rows[_ii][0].ToString()) || !GetTo.IsInt(dt.Rows[_ii][0].ToString()))
                                    {
                                        subjStr = subjStr.Trim() + " " + dt.Rows[_ii][c].ToString().Trim();

                                        _ii += 1;
                                    }
                                }
                                else if (dt.Rows[_i][c].ToString().Contains("DRWG") || dt.Rows[_i][c].ToString().Contains("DRAWING"))
                                    drwStr = dt.Rows[_i][c].ToString().Trim();
                            }

                            _i += 1;
                        }


                        //for (int c = 0; c < dt.Columns.Count; c++)
                        //{
                        //    if (dt.Rows[i][c].ToString().Contains("Model:"))
                        //        modelStr = dt.Rows[i][c + 1].ToString().Trim();
                        //    else if (dt.Rows[i][c].ToString().Contains("Type:"))
                        //        typeStr = dt.Rows[i][c + 1].ToString().Trim();
                        //    else if (dt.Rows[i][c].ToString().Contains("Size:"))
                        //        sizeStr = dt.Rows[i][c + 1].ToString().Trim();
                        //    else if (dt.Rows[i][c].ToString().Contains("Serial No"))
                        //    {
                        //        serialStr = dt.Rows[i][c + 1].ToString().Trim();

                        //        if (string.IsNullOrEmpty(serialStr))
                        //            serialStr = dt.Rows[i][c].ToString().Trim();
                        //    }
                        //    //else if (dt.Rows[i][c].ToString().Contains("Req. No."))
                        //    //    subjStr = dt.Rows[i][c + 1].ToString().Trim();

                        //}
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {

                        if (!_itemDescInt.Equals(-1))
                        {
                            for (int c = _itemDescInt; c < dt.Columns.Count; c++)
                            {
                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                            }

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                for (int c = 1; c < dt.Columns.Count; c++)
                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                                

                            if (dt.Rows[i + 1][0].ToString().Contains("Part Number"))
                                iTemCode = dt.Rows[i + 1][0].ToString().Trim() + dt.Rows[i + 1][1].ToString().Trim();
                            else if (dt.Rows[i + 2][0].ToString().Contains("Part Number"))
                                iTemCode = dt.Rows[i + 2][0].ToString().Trim() + dt.Rows[i + 2][1].ToString().Trim();
                            else if (dt.Rows[i + 3][0].ToString().Contains("Part Number"))
                                iTemCode = dt.Rows[i + 3][0].ToString().Trim() + dt.Rows[i + 3][1].ToString().Trim();


                            iTemCode = iTemCode.Replace("Part Number", "").Replace(":","").Trim();
                        }

                        string[] itemQtUnitSpl = dt.Rows[i][_itemQt].ToString().Split(' ');

                        if (itemQtUnitSpl.Length > 1)
                        {
                            iTemQt = itemQtUnitSpl[0].ToString().Trim();
                            iTemUnit = itemQtUnitSpl[1].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(subjName))
                            iTemSUBJ = subjName.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: "+makerStr.Trim();

                        if (!string.IsNullOrEmpty(modelStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine +"MODEL: " +modelStr.Trim();

                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: "+typeStr.Trim();

                        if (!string.IsNullOrEmpty(sizeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "SIZE: "+sizeStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine +"S/N: " +serialStr.Trim();

                        if (!string.IsNullOrEmpty(drwStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + drwStr.Trim();

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr.Trim();



                        //ITEM ADD START
                        dtItem.Rows.Add();
                        //dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemCode = string.Empty;
                        iTemQt = string.Empty;
                        iTemUnit = string.Empty;
                        iTemDESC = string.Empty;
                    }
                }
            }
        }
    }
}

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class MolTech
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



        public MolTech(string fileName)
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
            int _itemQtInt = -1;
            int _itemUnitInt = -1;
            int _itemCodeInt = -1;

            string descStr = string.Empty;
            string partStr = string.Empty;

            string makerSubjStr = string.Empty;
            string nameSubjStr = string.Empty;
            string typeSubjStr = string.Empty;
            string serialSubjStr = string.Empty;

            string makerStr = string.Empty;
            string dwgStr = string.Empty;

            int _rowCount = 0;


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

                    if (firstColStr.Contains("Vessel Name"))
                    {
                        vessel = dt.Rows[i][1].ToString().Trim();
                        if (string.IsNullOrEmpty(vessel))
                            vessel = dt.Rows[i][2].ToString().Trim();
                    }
                    else if (firstColStr.Contains("Our Reference"))
                    {
                        reference = dt.Rows[i][1].ToString().Trim();
                        if (string.IsNullOrEmpty(reference))
                            reference = dt.Rows[i][2].ToString().Trim();
                    }
                    else if (firstColStr.Contains("Maker"))
                    {
                        makerStr = firstColStr;
                    }
                    else if (firstColStr.Contains("Dwg"))
                    {
                        dwgStr = firstColStr;
                    }
                    else if (firstColStr.Contains("Name"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Name"))
                            {
                                if (!dt.Rows[i + 1][c].ToString().Contains("Line"))
                                    nameSubjStr = dt.Rows[i + 1][c].ToString().Trim();
                                else
                                    break;

                            }
                            else if (dt.Rows[i][c].ToString().Equals("Maker"))
                            {
                                if (!dt.Rows[i + 1][c].ToString().Equals("Full Description"))
                                    makerSubjStr = dt.Rows[i + 1][c].ToString().Trim();
                                else
                                    break;
                            }
                            else if (dt.Rows[i][c].ToString().Equals("Type"))
                            {
                                typeSubjStr = dt.Rows[i + 1][c].ToString().Trim();
                            }
                            else if (dt.Rows[i][c].ToString().Equals("Serial No"))
                            {
                                serialSubjStr = dt.Rows[i + 1][c].ToString().Trim();
                            }
                        }
                    }
                    else if (firstColStr.Contains("Line"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Full Description")) _itemDescInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Quantity")) _itemQtInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit Type")) _itemUnitInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Part")) _itemCodeInt = c;
                        }

                        int _i = i + 1;
                        while (!GetTo.IsInt(dt.Rows[_i][0].ToString()))
                        {
                            _rowCount += 1;

                            _i += 1;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        iTemNo = firstColStr;
                        iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();
                        iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();


                        //for (int c = 0; c < _rowCount; c++)
                        //{

                        //}



                        if (!string.IsNullOrEmpty(nameSubjStr))
                            iTemSUBJ = nameSubjStr;

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + makerStr;
                        else if (!string.IsNullOrEmpty(makerSubjStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerSubjStr;

                        if (!string.IsNullOrEmpty(dwgStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + dwgStr;

                        if (!string.IsNullOrEmpty(typeSubjStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeSubjStr;

                        if (!string.IsNullOrEmpty(serialSubjStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialSubjStr;


                        int _i = i + 1;
                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                descStr = descStr.Trim() + dt.Rows[_i][_itemDescInt].ToString();
                            }
                            _i += 1;
                        }


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemQt = string.Empty;
                        iTemCode = string.Empty;
                    }
                }
            }
        }
    }
}

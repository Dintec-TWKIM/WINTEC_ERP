using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class ShinChungLin
    {
        string vessel;
        string reference;
        string imoNumber;
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

        public string ImoNumber
        {
            get
            {
                return imoNumber;
            }
        }

        #endregion ==================================================================================================== Constructor



        public ShinChungLin(string fileName)
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

            int _itemDesc = -1;
            int _itemQt = -1;

            string subjStr = string.Empty;
            string typeStr = string.Empty;
            string makerStr = string.Empty;
            string serialStr = string.Empty;


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


                    if(string.IsNullOrEmpty(imoNumber))
                    {
                        for(int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("IMO NO"))
                                imoNumber = dt.Rows[i][c].ToString().Replace("IMO NO", "").Replace(":", "").Replace(".", "").Trim();
                        }
                    }

                    if (firstColStr.StartsWith("MESSRS"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("ENQUIRY NO"))
                                reference = dt.Rows[i][c].ToString().Replace("ENQUIRY NO.:", "").Trim() + dt.Rows[i][c + 1].ToString().Replace("ENQUIRY NO.:", "").Trim();

                            if (dt.Rows[i + 1][c].ToString().StartsWith("M.V"))
                                vessel = dt.Rows[i + 1][c].ToString().Replace("M.V", "").Replace(".", "").Replace(":", "").Trim() + dt.Rows[i + 1][c + 1].ToString().Replace("M.V", "").Replace(".", "").Replace(":", "").Trim();
                        }


                    }
                    else if (firstColStr.StartsWith("("))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Name"))
                                subjStr = dt.Rows[i][c + 1].ToString().Replace(":","").Trim();
                            else if (dt.Rows[i][c].ToString().StartsWith("Type"))
                                typeStr = dt.Rows[i][c + 1].ToString().Replace(":", "").Trim();
                            else if (dt.Rows[i][c].ToString().StartsWith("Serial"))
                                serialStr = dt.Rows[i][c + 1].ToString().Replace(":", "").Trim();
                            else if (dt.Rows[i][c].ToString().StartsWith("Maker"))
                                makerStr = dt.Rows[i][c + 1].ToString().Replace(":", "").Trim();
                        }


                        int _i = i + 1;
                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[_i][c].ToString().StartsWith("Name"))
                                    subjStr = dt.Rows[_i][c + 1].ToString().Replace(":", "").Trim();
                                else if (dt.Rows[_i][c].ToString().StartsWith("Type"))
                                    typeStr = dt.Rows[_i][c + 1].ToString().Replace(":", "").Trim();
                                else if (dt.Rows[_i][c].ToString().StartsWith("Serial"))
                                    serialStr = dt.Rows[_i][c + 1].ToString().Replace(":", "").Trim();
                                else if (dt.Rows[_i][c].ToString().StartsWith("Maker"))
                                    makerStr = dt.Rows[_i][c + 1].ToString().Replace(":", "").Trim();
                            }
                            _i += 1;
                        }

                    }
                    else if (firstColStr.StartsWith("Items"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Contains("Qty")) _itemQt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemDESC = iTemDESC + dt.Rows[_i][_itemDesc].ToString().Trim();
                                _i += 1;
                                if (_i >= dt.Rows.Count)
                                    break;
                                else
                                    iTemDESC = iTemDESC.Trim() + Environment.NewLine;
                            }
                        }

                        if (!_itemQt.Equals(-1))
                        {
                            string[] qtUnitSpl = dt.Rows[i][_itemQt].ToString().Split(' ');

                            if (qtUnitSpl[1] != null)
                            {
                                iTemQt = qtUnitSpl[0].ToString().Trim();
                                iTemUnit = qtUnitSpl[1].ToString().Trim();
                            }
                        }


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine+"TYPE: " + typeStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/N: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine +"MAKER: " + makerStr.Trim();

                        iTemSUBJ = iTemSUBJ.Trim();
                        iTemDESC = iTemDESC.Trim();

                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemSUBJ = string.Empty;
                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;
                    }
                }
            }
        }
    }
}

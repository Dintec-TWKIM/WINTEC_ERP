using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class EasternMediterranean
    {
        string vessel;
        string reference;
        string partner;
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

        public string Partner
        {
            get
            {
                return partner;
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



        public EasternMediterranean(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            partner = "";                       // 매입처 담당자

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

            string subjStr = string.Empty;

            string subjStr2 = string.Empty;

            int _itemDesc = -1;
            int _itemQt = -1;


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

                    if (firstColStr.StartsWith("RFQ No.:"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(reference))
                                reference = dt.Rows[i][c].ToString().Replace("RFQ No", "").Replace(".", "").Replace(":", "").Trim();
                            else if (string.IsNullOrEmpty(vessel))
                                vessel = dt.Rows[i][c].ToString().Replace("Vessel", "").Replace(":", "").Trim();
                            
                        }
                    }
                    else if (secondColStr.StartsWith("Equipment:"))
                    {
                        subjStr = secondColStr.Replace("Equipment", "").Replace(":", "").Trim() + Environment.NewLine;

                        int _i = i + 1;

                        while (!GetTo.IsInt(dt.Rows[_i][0].ToString()))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                subjStr = subjStr + " " + dt.Rows[_i][c].ToString().Trim();
                            }

                            subjStr = subjStr.Trim() + Environment.NewLine;

                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }

                        subjStr = subjStr.Replace("\r\n ", "\r\n").Trim();

                    }
                    else if (secondColStr.StartsWith("Ass'y"))
                    {
                        subjStr2 = secondColStr.Replace("Ass'y: ", "").Trim();

                        int _i = i + 1;

                        while (!GetTo.IsInt(dt.Rows[_i][0].ToString()))
                        {
                            for(int c =1; c < dt.Columns.Count; c++)
                            {
                                subjStr2 = subjStr2.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                            }

                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        // row 값 가져와서 배열에 넣은후 값 추가하기
                        string[] rowValueSpl = new string[20];
                        int columnCount = 0;
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                            {
                                rowValueSpl[columnCount] = c.ToString();
                                columnCount++;
                            }
                        }

                        if (rowValueSpl[2] != null && rowValueSpl[3] == null)
                        {
                            _itemQt = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[2].ToString());
                        }


                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][_itemDesc].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;

                                if (dt.Rows[_i][_itemDesc].ToString().Contains("Total"))
                                    break;
                            }
                        }

                        if (!_itemQt.Equals(-1))
                        {
                            string[] qtSpl = dt.Rows[i][_itemQt].ToString().Split(' ');

                            if (qtSpl.Length == 2)
                            {
                                iTemQt = qtSpl[0].ToString().Trim();
                                iTemUnit = qtSpl[1].ToString().Trim();
                            }
                            else 
                            {
                                qtSpl = dt.Rows[i][_itemQt].ToString().Split('.');

                                if (qtSpl.Length == 2)
                                {
                                    iTemQt = qtSpl[0].ToString().Trim();
                                    iTemUnit = qtSpl[1].ToString().Replace("00","").Trim();
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(subjStr2))
                        {
                            if (string.IsNullOrEmpty(subjStr))
                                iTemSUBJ = subjStr2.Trim();

                            //iTemSUBJ = iTemSUBJ.Replace(subjStr2, "").Trim();
                            //iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr2.Trim();
                        }



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
                        subjStr = string.Empty;
                        subjStr2 = string.Empty;
                    }
                }
            }
        }
    }
}

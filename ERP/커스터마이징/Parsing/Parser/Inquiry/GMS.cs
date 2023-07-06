using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class GMS
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



        public GMS(string fileName)
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

            string subjStr = string.Empty;

            int _itemQt = -1;
            int _itemUnit = -1;

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

                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("GMS Reference No."))
                            {
                                reference = dt.Rows[i + 2][c].ToString().Trim();

                                if (string.IsNullOrEmpty(reference))
                                    reference = dt.Rows[i + 1][c].ToString().Trim();

                                break;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(vessel))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Vessel's Name"))
                                vessel = dt.Rows[i + 1][c].ToString().Trim();
                        }
                    }

                    if (firstColStr.Equals("A"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        subjStr = subjStr.Replace("======================================", "").Trim();

                        subjStr = subjStr.Trim() + Environment.NewLine;
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

                        // 아이템 컬럼 카운트
                        _itemQt = Convert.ToInt16(rowValueSpl[columnCount - 2].ToString());
                        _itemUnit = Convert.ToInt16(rowValueSpl[columnCount - 1].ToString());

                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        for (int c = 1; c < _itemQt; c++)
                        {
                            iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        int _i = i + 1;
                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                        {
                            if (dt.Rows[_i][1].ToString().Contains("Than"))
                                break;

                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                            }

                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }


                        // 주제 항 공백 채워지는거 비우기, 
                        // item     thank ~ 부분부터 안채우기

                        if (iTemDESC.Contains("Thank you"))
                        {
                            int idx_e = iTemDESC.IndexOf("Thank you");

                            iTemDESC = iTemDESC.Substring(0, idx_e).Trim();
                        }


                        if (!string.IsNullOrEmpty(subjStr.Trim()))
                            iTemSUBJ = subjStr.Trim();

                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt.Replace(",",".");
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;
                        subjStr = string.Empty;
                    }
                }
            }
        }
    }
}

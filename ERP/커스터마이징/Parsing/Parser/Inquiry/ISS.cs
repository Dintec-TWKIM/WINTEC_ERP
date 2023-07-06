using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{

    class ISS
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



        public ISS(string fileName)
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

            int _itemDesc = -1;
            int _itemQty = -1;

            string subjStr = string.Empty;

            bool itemStart = false;

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
                    string firstColNum = string.Empty;
                    if (!string.IsNullOrEmpty(firstColStr))
                        firstColNum = firstColStr.Substring(0, 1).Trim();

                    if (!itemStart)
                    {
                        if (firstColStr.StartsWith("VESSEL"))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                                    vessel = dt.Rows[i][c].ToString().Trim();
                            }
                        }
                        else if (string.IsNullOrEmpty(reference))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().StartsWith("OUR REF"))
                                    reference = dt.Rows[i][c + 1].ToString().Trim();
                            }
                        }
                        else if (firstColStr.Contains("REGULATIONS") || secondColStr.Contains("REGULATIONS"))
                        {
                            i += 1;
                            while (!dt.Rows[i][0].ToString().StartsWith("ITEM"))
                            {
                                subjStr = subjStr.Trim() + Environment.NewLine + dt.Rows[i][0].ToString().Trim();
                                i += 1;
                            }

                            i -= 1;
                        }
                        else if (firstColStr.StartsWith("ITEM"))
                        {
                            itemStart = true;

                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().StartsWith("DESCRIPTION")) _itemDesc = c;
                                else if (dt.Rows[i][c].ToString().StartsWith("QTY")) _itemQty = c;
                            }
                        }
                    }
                    else
                    {
                        if ((GetTo.IsInt(dt.Rows[i][_itemQty].ToString()) && itemStart) || (GetTo.IsInt(firstColNum) && itemStart))
                        {
                            if (!_itemDesc.Equals(-1))
                            {
                                for (int c = 1; c < _itemQty; c++)
                                {
                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                                }
                            }


                            if (!_itemQty.Equals(-1))
                            {
                                iTemQt = dt.Rows[i][_itemQty].ToString().Trim();
                                iTemUnit = dt.Rows[i][_itemQty + 1].ToString().Trim();
                            }

                            if (!string.IsNullOrEmpty(subjStr))
                                iTemSUBJ = subjStr.Trim();

                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                            if (!string.IsNullOrEmpty(iTemSUBJ))
                            {
                                if(iTemSUBJ.StartsWith("FOR"))
                                    dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
                                else
                                    dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                            }
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

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
}

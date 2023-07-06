using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class NorthernMarine
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



        public NorthernMarine(string fileName)
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

            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemCode = -1;
            int _itemDesc = -1;


            string subjStr = string.Empty;
            string modelStr = string.Empty;
            string makerStr = string.Empty;
            
                
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

                    if (dt.Rows[i][7].ToString().Equals("Reference:"))
                        reference = dt.Rows[i][8].ToString().Trim() + dt.Rows[i][9].ToString().Trim() + dt.Rows[i][10].ToString().Trim();
                    else if (dt.Rows[i][7].ToString().Equals("Ship Name:"))
                        vessel = dt.Rows[i][8].ToString().Trim() + dt.Rows[i][9].ToString().Trim() + dt.Rows[i][10].ToString().Trim();
                    else if (firstColStr.Equals("Ln"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("UoM")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Equals("Part No.")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                        }
                    }
                    else if (firstColStr.StartsWith("Equipment:"))
                    {
                        for(int c =1; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Model:"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            modelStr = modelStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Manufacturer:"))
                    {
                        for(int c = 1; c < dt.Columns.Count; c++)
                        {
                            makerStr = makerStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (GetTo.IsInt(firstColStr.Replace(".","")))
                    {
                        if (string.IsNullOrEmpty(iTemSUBJ))
                        {
                            for (int _i = i + 1; _i < dt.Rows.Count; _i++)
                            {
                                if (dt.Rows[_i][0].ToString().Contains("External Notes"))
                                {
                                    for(int _subjRow = _i; _subjRow < dt.Rows.Count; _subjRow++)
                                    {
                                        if(string.IsNullOrEmpty(dt.Rows[_subjRow][0].ToString()))
                                        {
                                            break;
                                        }
                                        for (int c = 0; c < dt.Columns.Count; c++)
                                        {
                                            if (dt.Rows[_subjRow + 1][c].ToString().Contains("Page")) break;

                                            iTemSUBJ = iTemSUBJ.Trim() + " " + dt.Rows[_subjRow + 1][c].ToString().Trim();
                                        }
                                    }
                                }
                            }
                        }

                        if (_itemQt != -1)
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (_itemUnit != -1)
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (_itemDesc != -1)
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim() + " " + dt.Rows[i][_itemDesc + 1].ToString().Trim(); ;

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim() + dt.Rows[_i][_itemDesc + 1].ToString().Trim();
                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        if (_itemCode != -1)
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr.Trim();

                        if (!string.IsNullOrEmpty(modelStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + makerStr.Trim();
                                


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
                        iTemSUBJ = string.Empty;
                    }
                }
            }
        }
    }
}

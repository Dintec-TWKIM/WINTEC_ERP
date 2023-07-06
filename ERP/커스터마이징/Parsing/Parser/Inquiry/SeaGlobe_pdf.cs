using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class SeaGlobe_pdf
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



        public SeaGlobe_pdf(string fileName)
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
            string iTemDwg = string.Empty;

            int _itemDwg = -1;
            int _itemCode = -1;
            int _itemMaker = -1;
            int _itemDesc = -1;
            int _itemQt = -1;

            string subjStr = string.Empty;
            string makerStr = string.Empty;
            string modelStr = string.Empty;
            string bookStr = string.Empty;
            string serialStr = string.Empty;
            string particylarsStr = string.Empty;
            string assemStr = string.Empty;
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
                    string firstColStr = dt.Rows[i][0].ToString();

                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Request For Quotation"))
                            {
                                reference = dt.Rows[i][c].ToString().Replace("Request For Quotation", "").Replace(":", "").Trim();
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(vessel))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Vessel"))
                            {
                                vessel = dt.Rows[i][c].ToString().Replace("Vessel", "").Replace(":", "").Trim();
                            }
                        }
                    }

                    if (firstColStr.ToLower().StartsWith("equipment:"))
                    {
                        subjStr = firstColStr.Replace("Equipment:", "").Trim() + dt.Rows[i][1].ToString().Replace("Equipment:", "").Trim() + dt.Rows[i][2].ToString().Replace("Equipment:", "").Trim();  
                    }
                    else if (firstColStr.ToLower().StartsWith("maker:"))
                    {
                        makerStr = firstColStr.Replace("Maker:", "").Trim() + dt.Rows[i][1].ToString().Replace("Maker:", "").Trim() + dt.Rows[i][2].ToString().Replace("Maker:", "").Trim();  
                    }
                    else if (firstColStr.ToLower().StartsWith("model:"))
                    {
                        modelStr = firstColStr.Replace("Model:", "").Trim() + dt.Rows[i][1].ToString().Replace("Model:", "").Trim() + dt.Rows[i][2].ToString().Replace("Model:", "").Trim();  
                    }
                    else if (firstColStr.ToLower().StartsWith("serial"))
                    {
                        serialStr = firstColStr.Replace("Serial:", "").Trim() + dt.Rows[i][1].ToString().Replace("Serial:", "").Trim() + dt.Rows[i][2].ToString().Replace("Serial:", "").Trim();  
                    }
                    else if (firstColStr.Equals("Item"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Drawing No.")) _itemDwg = c;
                            else if (dt.Rows[i][c].ToString().Equals("Part No.")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Maker")) _itemMaker = c;
                            else if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qty. Measure")) _itemQt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (_itemDwg != -1)
                            iTemDwg = dt.Rows[i][_itemDwg].ToString().Trim();

                        if (_itemCode != -1)
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (_itemDesc != -1)
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                        if (_itemQt != -1)
                        {
                            string qtStr = dt.Rows[i][_itemQt].ToString().Trim();

                            string[] qtUnit = qtStr.Split(' ');

                            if (qtUnit.Length == 2)
                            {
                                iTemQt = qtUnit[0].ToString().Trim();
                                iTemUnit = qtUnit[1].ToString().Trim();
                            }
                        }


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ + Environment.NewLine + "MAKER: " + makerStr;

                        if (!string.IsNullOrEmpty(modelStr))
                            iTemSUBJ = iTemSUBJ + Environment.NewLine + "MODEL: " + modelStr;

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ + Environment.NewLine + "SERIAL NO: " + serialStr;


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
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;
                    }
                }
            }
        }
    }
}

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Venturi
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



        public Venturi(string fileName)
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

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemNo = -1;

            string subjStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
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

                    if (firstColStr.StartsWith("Vessel's Name"))
                    {
                        vessel = dt.Rows[i][1].ToString().Trim();

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Ref No"))
                                reference = dt.Rows[i][c + 1].ToString().Trim();
                        }
                    }
                    else if (firstColStr.Equals("Machine"))
                    {
                        subjStr = dt.Rows[i][1].ToString().Trim();

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Maker"))
                                makerStr = dt.Rows[i][c + 1].ToString().Trim();
                        }
                    }
                    else if (firstColStr.Equals("Model/Type"))
                    {
                        typeStr = dt.Rows[i][1].ToString().Trim();

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Serial No"))
                                serialStr = dt.Rows[i][c + 1].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Capac/"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("DWG. NO."))
                                dwgStr = dt.Rows[i][c + 1].ToString().Trim();
                        }
                    }
                    else if (firstColStr.Equals("NO"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("DESCRIPTION")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("PART NO")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("QTY")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("UNIT")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("ITEM NO")) _itemNo = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (_itemQt != 0)
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                            if (!GetTo.IsInt(iTemQt))
                            {
                                iTemQt = dt.Rows[i][_itemQt - 1].ToString().Trim();
                                iTemUnit = dt.Rows[i][_itemQt].ToString().Trim();

                                iTemCode = dt.Rows[i][_itemNo].ToString().Trim();
                            }
                            else
                            {
                                iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                                if (_itemCode != 0)
                                    iTemCode = dt.Rows[i][_itemCode].ToString().Trim();
                            }
                        }

                        

                        if (_itemDesc != 0)
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "Model/Type: " + typeStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(dwgStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG. NO. " + dwgStr.Trim();


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
                    }
                }
            }
        }
    }
}

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Lomar_pdf
    {
        string vessel;
        string reference;
        DataTable dtItem;
        string contact;
        string imoNumber;

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

        public string Contact
        {
            get
            {
                return contact;
            }
        }

        public string Imonumber
        {
            get
            {
                return imoNumber;
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



        public Lomar_pdf(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            contact = string.Empty;
            imoNumber = string.Empty;

            dtItem = new DataTable();
            dtItem.Columns.Add("NO");           // 순번
            dtItem.Columns.Add("SUBJ");         // 주제
            dtItem.Columns.Add("ITEM");         // 품목코드
            dtItem.Columns.Add("DESC");         // 품목명
            dtItem.Columns.Add("UNIT");         // 단위
            dtItem.Columns.Add("QT");           // 수량
            dtItem.Columns.Add("UNIQ");         //선사코드
            this.fileName = fileName;
            this.uc = new UnitConverter();
        }

        public void Parse()
        {
            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemCode = -1;
            int _itemDwg = -1;

            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;
            string iTemUniq = string.Empty;

            string subjStr = string.Empty;
            string componentStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string dwgStr = string.Empty;


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


            foreach (DataTable dt in ds.Tables)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string firstStr = dt.Rows[i][0].ToString();

                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToUpper().StartsWith("RFQ NO"))
                                reference = dt.Rows[i][c + 1].ToString().Trim();
                        }
                    }

                    if (string.IsNullOrEmpty(vessel))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToUpper().Equals("VESSEL"))
                                vessel = dt.Rows[i][c + 1].ToString().Trim();
                        }
                    }

                    if (string.IsNullOrEmpty(imoNumber))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToUpper().Equals("IMO NO."))
                                imoNumber = dt.Rows[i][c + 1].ToString().Trim();
                        }
                    }


                    if (firstStr.Equals("No."))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Group")) _itemDwg = c;
                            else if (dt.Rows[i][c].ToString().Equals("Part Number")) _itemCode = c;
                        }
                    }
                    else if (firstStr.ToUpper().Equals("COMPONENT:"))
                    {
                        subjStr = dt.Rows[i - 1][0].ToString().Trim();

                        componentStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstStr.ToUpper().Contains("MAKER"))
                    {
                        makerStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstStr.ToUpper().Contains("TYPE"))
                    {
                        typeStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstStr.ToUpper().Contains("SERIAL NO"))
                    {
                        serialStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstStr.ToUpper().Contains("DRAWING NO"))
                    {
                        dwgStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (GetTo.IsInt(firstStr))
                    {
                        if (_itemDesc != -1)
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        if (_itemQt != -1)
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                            string[] qtSpl = iTemQt.Split(' ');

                            if (qtSpl.Length == 2)
                            {
                                iTemQt = qtSpl[0].ToString().Trim();
                                iTemUnit = qtSpl[1].ToString().Trim();
                            }
                        }



                        if (_itemUnit != -1 && string.IsNullOrEmpty(iTemUnit))
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(componentStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + componentStr;

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "SERIAL NO: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(dwgStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG NO: " + dwgStr.Trim();

                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        // 주제가 없는 경우가 있음, 없을때는 FOR 제거
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ.Trim();
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;

                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemQt = string.Empty;
                        iTemCode = string.Empty;
                        iTemSUBJ = string.Empty;
                    }
                }
            }
        }
    }
}

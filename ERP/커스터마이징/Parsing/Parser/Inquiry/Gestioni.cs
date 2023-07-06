using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Gestioni
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



        public Gestioni(string fileName)
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
            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string drwStr = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemUnit = -1;
            int _itemQt = -1;
            int _itemDrw = -1;


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
                            if (dt.Rows[i][c].ToString().Contains("Number"))
                            {
                                reference = dt.Rows[i + 1][c].ToString().Trim();
                                break;
                            }
                        }
                    }

                    if (firstColStr.Contains("No spare on board")) break;
                    else if (firstColStr.Contains("M/T") || firstColStr.Contains("M/V") || firstColStr.Contains("M/N"))
                    {
                        vessel = dt.Rows[i][0].ToString() + dt.Rows[i][1].ToString() + dt.Rows[i][2].ToString();

                        vessel = vessel.Replace("M/T", "").Replace("M/V", "").Trim();
                    }
                    else if (firstColStr.StartsWith("Machinery"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                            subjStr = subjStr.Trim() + dt.Rows[i][c].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Maker"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                            makerStr = makerStr.Trim() + dt.Rows[i][c].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Type"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                            typeStr = typeStr.Trim() + dt.Rows[i][c].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Serial Number"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                            serialStr = serialStr.Trim() + dt.Rows[i][c].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Item"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Q.ty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Un")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Equals("Code Number")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Drawing or")) _itemDrw = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (!_itemUnit.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (!_itemDesc.Equals(-1))
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                        if (!_itemDrw.Equals(-1))
                            drwStr = dt.Rows[i][_itemDrw].ToString().Trim();


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/N: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(drwStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG NO: " + drwStr.Trim();


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

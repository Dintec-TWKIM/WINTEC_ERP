using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class GeneralNational_pdf
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



        public GeneralNational_pdf(string fileName)
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
            int _itemRmk = -1;

            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;
            string iTemUniq = string.Empty;

            string subjStr = string.Empty;

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

                   

                    if (string.IsNullOrEmpty(vessel))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Ship Name"))
                                vessel = dt.Rows[i][c + 1].ToString().Replace("M/T", "").Replace("\"", "").Trim() + dt.Rows[i][c + 2].ToString().Replace("M/T", "").Replace("\"", "").Trim();
                        }
                    }

                    if (string.IsNullOrEmpty(imoNumber))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToUpper().Contains("IMO NO"))
                                imoNumber = dt.Rows[i][c].ToString().Replace("IMO no.","").Replace(":","").Trim() + dt.Rows[i][c + 1].ToString().Replace("IMO no","").Replace(":","").Trim();
                        }
                    }

                    if (firstStr.Contains("Requisition Form"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(reference))
                                reference = dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstStr.Contains("Subject:"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        int _i = i + 1;

                            while (!dt.Rows[_i][0].ToString().Contains("No.") && string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                for (int c = 0; c < dt.Columns.Count; c++)
                                    subjStr = subjStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }

                            subjStr = subjStr.Replace("Priority:", "\r\nPriority:").Replace("Acc. No.:", "").Replace("Equipment Speci", "\r\nEquipment S").Replace("Name", "\r\nName").Replace("Type", "\r\nType").Replace("Serial No", "\r\nSerial").Replace("Maker", "\r\nMaker").Trim();
                    }
                    else if (firstStr.StartsWith("No."))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Part no")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Contains("Unit")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Contains("Remarks")) _itemRmk = c;
                        }
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
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (_itemCode != -1)
                        {
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemCode = iTemCode.Trim() + Environment.NewLine + dt.Rows[_i][_itemCode].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }

                            iTemCode = iTemCode.Replace("Part no.", "").Replace("Part no", "").Replace("\r\n", "").Replace("Part No.", "").Replace("Part No", "").Trim();
                        }

                        if (_itemUnit != -1)
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

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
                    }
                }
            }
        }
    }
}

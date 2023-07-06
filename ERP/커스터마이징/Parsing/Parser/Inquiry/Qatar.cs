using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Qatar
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



        public Qatar(string fileName)
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

            string equipNameStr = string.Empty;
            string equipTypeStr = string.Empty;
            string otherStr = string.Empty;
            string serialStr = string.Empty;
            string makerStr = string.Empty;
            string DrwStr = string.Empty;
            string ratingStr = string.Empty;
            string mfgStr = string.Empty;
            string positionStr = string.Empty;


            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemCode = -1;
            int _itemDesc = -1;


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

                    if (firstColStr.Contains("---------------------------------")) break;
                    else if (firstColStr.Contains("---------------")) break;

                    if (string.IsNullOrEmpty(vessel))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("VESSEL NAME"))
                                vessel = dt.Rows[i][c].ToString().Trim() + dt.Rows[i][c + 1].ToString().Trim();

                            vessel = vessel.Replace("VESSEL NAME", "").Replace(":", "").Trim();

                        }
                    }
                    else if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("ENQUIRY #") || dt.Rows[i][c].ToString().Contains("PO #"))
                                reference = dt.Rows[i][c].ToString().Trim() + dt.Rows[i][c + 1].ToString().Trim();

                            reference = reference.Replace("ENQUIRY #", "").Replace("PO #","").Replace(":", "").Trim();
                        }
                    }
                    else if (firstColStr.Contains("Equip. Name"))
                    {
                        equipNameStr = firstColStr.Replace("Equip. Name", "").Replace(":", "").Trim();

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Serial Number"))
                                serialStr = dt.Rows[i][c].ToString().Replace("Serial Number", "").Replace(":", "").Trim();
                        }

                        if (!dt.Rows[i + 1][0].ToString().Contains("Equip. Type"))
                            equipNameStr = equipNameStr + " " + dt.Rows[i + 1][0].ToString().Trim();

                        int _i = i + 1;
                        while (!dt.Rows[_i][0].ToString().Contains("Equip. Type"))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[_i][c].ToString().Contains("Serial Number"))
                                    serialStr = dt.Rows[_i][c].ToString().Replace("Serial Number", "").Replace(":", "").Trim();
                            }
                            _i += 1;
                        }
                    }
                    else if (firstColStr.Contains("Equip. Type"))
                    {
                        equipTypeStr = firstColStr.Replace("Equip. Type", "").Replace(":", "").Trim();
                        //equipTypeStr = firstColStr.Trim();

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Maker:"))
                                makerStr = dt.Rows[i][c].ToString().Replace("Maker", "").Replace(":", "").Trim();
                        }


                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Maker:"))
                                makerStr = dt.Rows[i][c].ToString().Replace("Maker:", "").Trim();
                        }

                        int _i = i + 1;
                        while (!dt.Rows[_i][0].ToString().Contains("Item"))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[_i][c].ToString().Contains("Maker"))
                                    makerStr = dt.Rows[_i][c].ToString().Trim();
                                else if (dt.Rows[_i][c].ToString().Contains("Drawing No"))
                                    DrwStr = dt.Rows[_i][c].ToString().Replace("Drawing No", "").Replace(":","").Trim();
                                else if (dt.Rows[_i][c].ToString().Contains("Spares Mfg"))
                                    mfgStr = dt.Rows[_i][c].ToString().Replace("Spares Mfg", "").Replace(":","").Trim();
                                else if (dt.Rows[_i][c].ToString().Contains("Position No"))
                                    positionStr = dt.Rows[_i][c].ToString().Trim();
                            }
                            _i += 1;
                        }
                    }
                    else if (firstColStr.Equals("Item"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Equals("Part No")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr) && firstColStr.Length <= 3 )
                    {
                        string test = dt.Rows[i][0].ToString().Trim();

                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (!_itemUnit.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (!_itemDesc.Equals(-1))
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();


                        int _i = i + 1;
                        
                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                        {
                            iTemDESC = iTemDESC + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim();
                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }

                        iTemDESC = iTemDESC.Trim();

                        if (!string.IsNullOrEmpty(equipNameStr))
                            iTemSUBJ = equipNameStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/N: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(equipTypeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "Equip. Type: " + equipTypeStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(DrwStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "Drawing No: " + DrwStr.Trim();

                        if (!string.IsNullOrEmpty(mfgStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "Spares Mfg: " + mfgStr.Trim();


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

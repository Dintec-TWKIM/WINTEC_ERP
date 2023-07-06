using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class OOCL
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



        public OOCL(string fileName)
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

            int _itemDwg = -1;
            int _itemUnit = -1;
            int _itemOqt = -1;
            int _itemRqt = -1;

            string subjStr = string.Empty;
            string itemDwg = string.Empty;

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

                    if (firstColStr.StartsWith("Req No."))
                        reference = dt.Rows[i][1].ToString().Trim();
                    else if (firstColStr.StartsWith("Ship Name"))
                        vessel = dt.Rows[i][1].ToString().Trim();
                    else if (firstColStr.StartsWith("Equip."))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        int _i = i + 1;
                        while (!dt.Rows[_i][0].ToString().StartsWith("ITEM NO"))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                subjStr = subjStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                            }

                                _i += 1;

                            if (_i > dt.Rows.Count)
                                break;

                            if (subjStr.Contains("Model/Type"))
                                break;
                        }

                        subjStr = subjStr.Replace("Manufacturer", "\r\nManufacturer").Replace("Model/", "\r\nModel/").Replace("Specification", "\r\nSpecification").Trim();
                    }
                    else if (firstColStr.StartsWith("Mfg Part#"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Mfg Draw No")) _itemDwg = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Equals("Onhand Q'ty")) _itemOqt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Request Q'ty")) _itemRqt = c;
                        }
                    }
                    else if (!_itemOqt.Equals(-1))
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

                        if (rowValueSpl[4] != null && rowValueSpl[5] == null)
                        {
                            _itemRqt = Convert.ToInt16(rowValueSpl[4].ToString());
                        }
                        else if (rowValueSpl[5] != null && rowValueSpl[6] == null)
                        {
                            _itemRqt = Convert.ToInt16(rowValueSpl[5].ToString());
                        }
                        else if (rowValueSpl[6] != null && rowValueSpl[7] == null)
                        {
                            _itemRqt = Convert.ToInt16(rowValueSpl[6].ToString());
                        }

                        if (GetTo.IsInt(dt.Rows[i][_itemOqt].ToString()))
                        {
                            iTemDESC = dt.Rows[i+1][0].ToString().Trim();
                            iTemCode = dt.Rows[i][0].ToString().Replace("ITEM NO:","").Replace("PART NO:","").Trim();


                            if (_itemUnit != -1)
                                iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                            if (_itemDwg != -1)
                                itemDwg = dt.Rows[i][_itemDwg].ToString().Trim();

                            if (_itemRqt != -1)
                            {
                                iTemQt = dt.Rows[i][_itemRqt].ToString().Trim();

                                if (_itemRqt + 1 < dt.Columns.Count)
                                {
                                    if (!string.IsNullOrEmpty(dt.Rows[i][_itemRqt + 1].ToString()))
                                        iTemQt = dt.Rows[i][_itemRqt + 1].ToString().Trim();
                                }
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
}

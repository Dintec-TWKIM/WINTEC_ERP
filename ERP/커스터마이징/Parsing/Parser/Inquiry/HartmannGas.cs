using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class HartmannGas
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



        public HartmannGas(string fileName)
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

            string subjStr = string.Empty;

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

                    if (firstColStr.Equals("Inquiry") || firstColStr.Equals("Order"))
                    {
                        string _reference = string.Empty;

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            _reference = _reference + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(_reference))
                        {
                            int idx_s = _reference.IndexOf("MT");
                            int idx_e = _reference.IndexOf("MT");

                            if (!idx_e.Equals(-1))
                            {
                                reference = _reference.Substring(0, idx_e).Trim();

                                if (reference.EndsWith("-"))
                                    reference = reference.Substring(0, reference.Length - 2).Trim();
                            }

                            if (!idx_s.Equals(-1))
                            {
                                vessel = _reference.Substring(idx_s, _reference.Length - idx_s).Replace("MT \"", "").Replace("\"", "").Trim();
                            }
                        }
                    }
                    else if (secondColStr.StartsWith("Part No"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Part No.")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Quantity")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                        }

                        int _i = i + 1;
                        while (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                        {
                            for(int c = 0; c < dt.Columns.Count; c++)
                            {
                                subjStr = subjStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                            }

                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;

                            if (GetTo.IsInt(dt.Rows[_i][0].ToString()))
                                break;
                        }
                    }
                    else if (!_itemCode.Equals(-1))
                    {
                        subjStr = subjStr.Replace("Manufacturer", "\r\nManufacturer").Replace("Year of Constr", "\r\nYear of Constr").Replace("Serial No", "\r\nSERIAL NO").Trim();


                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                        iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Trim();


                        if (!_itemCode.Equals(-1))
                        {
                            for (int c = 0; c <= _itemCode; c++)
                            {
                                iTemCode = iTemCode.Trim() + dt.Rows[i][c].ToString().Trim();
                            }
                        }

                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDESC = string.Empty;

                            for(int c = _itemDesc; c < dt.Columns.Count; c++)
                            {
                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                            }

                            int _i = i + 1;

                            if (_i >= dt.Rows.Count)
                                break;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                for (int c = _itemDesc; c < dt.Columns.Count; c++)
                                {
                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                }

                                _i += 1;

                                if (_i > dt.Rows.Count)
                                    break;
                            }

                            iTemDESC = iTemDESC.Replace("Type", "\r\nType").Replace("Drawing", "\r\nDrawing").Replace("No", "\r\nNo").Trim();
                        }

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();


                        if (iTemQt.ToUpper().Contains("PCS"))
                        {
                            iTemUnit = iTemQt;
                            iTemQt = iTemCode;
                        }

                        if (GetTo.IsInt(iTemQt))
                        {
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

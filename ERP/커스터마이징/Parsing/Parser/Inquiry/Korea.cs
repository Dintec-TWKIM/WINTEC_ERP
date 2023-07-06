using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Korea
    {
        string vessel;
        string reference;
        string partner;
        string contact;
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

        public string Contact
        {
            get
            {
                return contact;
            }
        }

        #endregion ==================================================================================================== Constructor



        public Korea(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            partner = "";                       // 매입처 담당자
            contact = string.Empty;             // 매출처 담당자

            dtItem = new DataTable();
            dtItem.Columns.Add("NO");           // 순번
            dtItem.Columns.Add("SUBJ");         // 주제
            dtItem.Columns.Add("ITEM");         // 품목코드
            dtItem.Columns.Add("DESC");         // 품z목명
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

            int _itemCode = -1;
            int _itemDesc = -1;
            int _itemUnit = -1;
            int _itemQt = -1;
            int _itemEdit = -1;
            int _itemRmk = -1;

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


                    if(string.IsNullOrEmpty(partner))
                    {
                        for(int c = 1; c < dt.Columns.Count; c++)
                        {
                            if(dt.Rows[i][c].ToString().StartsWith("ISSUED :"))
                            {
                                partner = dt.Rows[i][c + 1].ToString().Trim();
                            }
                        }

                        if(partner.ToUpper().Contains("SUPPLY"))
                        {
                            partner = partner.Replace("SUPPLY", "").Replace("/", "").Trim();
                        }
                    }


                    if (firstColStr.StartsWith("VESSEL"))
                    {
                        for(int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(vessel))
                                vessel = dt.Rows[i][c].ToString().Trim();
                            else
                                break;
                        }
                    }
                    else if (firstColStr.StartsWith("DELIVERY DATE"))
                    {
                        for (int c = 2; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("INQUIRY NO"))
                            {
                                for (int c2 = c+1; c2 < dt.Columns.Count; c2++)
                                {
                                    if (string.IsNullOrEmpty(reference))
                                        reference = dt.Rows[i][c2].ToString().Trim();
                                    else
                                        break;
                                }
                            }
                        }
                    }
                    else if (firstColStr.StartsWith("DELIVERY PORT"))
                    {
                        for (int c = 2; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("ISSUED"))
                                partner = dt.Rows[i][c + 1].ToString().Replace("SUPPLY", "").Replace("/","").Trim();
                        }

                        int idx_e = partner.IndexOf("(");

                        if(idx_e != -1)
                            partner = partner.Substring(0, idx_e).Trim();

                    }
                    else if (firstColStr.StartsWith("No"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Part No")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Contains("Part Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Contains("Unit")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Contains("Q'TY")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Contains("OnhandEdition")) _itemEdit = c;
                            else if (dt.Rows[i][c].ToString().Contains("REMARK")) _itemRmk = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (!_itemUnit.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (!_itemDesc.Equals(-1))
                        {
                            if (string.IsNullOrEmpty(dt.Rows[i - 2][0].ToString()) && string.IsNullOrEmpty(dt.Rows[i - 1][0].ToString()) && !string.IsNullOrEmpty(dt.Rows[i - 2][_itemCode].ToString()))
                            {
                                iTemSUBJ = string.Empty;

                                iTemSUBJ = iTemSUBJ.Trim() + " " + dt.Rows[i - 2][_itemCode].ToString().Trim();

                                iTemSUBJ = iTemSUBJ.Trim() + " " + dt.Rows[i - 1][_itemCode].ToString().Trim();
                            }
                            else if (!string.IsNullOrEmpty(dt.Rows[i - 2][0].ToString()) && string.IsNullOrEmpty(dt.Rows[i - 1][0].ToString()) && !string.IsNullOrEmpty(dt.Rows[i - 1][_itemCode].ToString()))
                            {
                                iTemSUBJ = string.Empty;

                                iTemSUBJ = iTemSUBJ.Trim() + " " + dt.Rows[i - 1][_itemCode].ToString().Trim();
                            }

                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;

                            while (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && !GetTo.IsInt(dt.Rows[_i][0].ToString()))
                            {
                                iTemDESC = iTemDESC.Trim()  + " " + dt.Rows[_i][0].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }


                            if (string.IsNullOrEmpty(iTemDESC))
                                iTemDESC = dt.Rows[i - 1][_itemDesc].ToString().Trim();

                        }



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

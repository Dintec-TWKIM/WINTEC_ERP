using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class Diamond
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



        public Diamond(string fileName)
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

            int _itemUnitInt = -1;
            int _itemDescInt = -1;
            int _itemQtInt = -1;
            int _itemDescEnd = -1;

            string descStr = string.Empty;
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
                    if(string.IsNullOrEmpty(reference))
                        reference = dt.Rows[0][dt.Columns.Count - 1].ToString().Trim();

                    if (string.IsNullOrEmpty(vessel))
                        vessel = dt.Rows[1][dt.Columns.Count - 2].ToString().Trim() + dt.Rows[1][dt.Columns.Count - 1].ToString().Trim();


                    string firstColStr = dt.Rows[i][0].ToString();

                    if (firstColStr.Equals("Item"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnitInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Description")) _itemDescInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Quan")) _itemQtInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Last FR")) _itemDescEnd = c;
                        }
                    }

                    if (firstColStr.Equals("List"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnitInt = c;
                        }
                    }

                    if (GetTo.IsInt(firstColStr))
                    {
                        iTemNo = firstColStr;
                        iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();
                        iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();


                        for (int c = _itemDescInt; c < _itemDescEnd; c++)
                        {
                            if (dt.Rows[i][c-1].ToString().Equals("P/N:"))
                                iTemCode = dt.Rows[i][c].ToString().Trim();
                            else
                                descStr = descStr.Trim() + " " + dt.Rows[i][c].ToString().Trim(); 
                        }


                        int _i = i + 1;
                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && _i < dt.Rows.Count - 3)
                        {
                            for (int c = _itemDescInt; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[_i][c - 1].ToString().Equals("Mfr:"))
                                    makerStr = dt.Rows[_i][c].ToString().Trim();
                                else if (dt.Rows[_i][c].ToString().Contains("DIAMOND OFFSHORE") || dt.Rows[_i][c].ToString().Contains("Request for Quotation"))
                                {
                                    break;
                                }
                                else
                                    descStr = descStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                            }
                            _i += 1;
                        }

                        //string[] _descValue = descStr.Split(' ');

                        //iTemCode = _descValue[1].ToString().Trim();
                        //makerStr = _descValue[3].ToString().Trim();

                        //string removeDescStr = _descValue[0].ToString() + " " + _descValue[1].ToString() + " " + _descValue[2].ToString() + " " + _descValue[3].ToString();

                        iTemDESC = descStr.Replace("P/N:","").Replace("Mfr:","").Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = "MAKER: " + makerStr;

                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] =iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        descStr = string.Empty;
                        iTemUnit = string.Empty;
                        iTemQt = string.Empty;
                        iTemCode = string.Empty;
                        iTemDESC = string.Empty;
                    }

                    
                }
            }
        }
    }
}

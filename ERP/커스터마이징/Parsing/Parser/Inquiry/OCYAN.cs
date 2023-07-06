using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class OCYAN
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



        public OCYAN(string fileName)
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
            string iTemUniq = string.Empty;


            int _itemCode = -1;
            int _itemDesc = -1;
            int _itemMaker = -1;
            int _itemUnit = -1;
            int _itemQt = -1;
            int _itemDescS = -1;
            int _itemUniq = -1;

            string descMaker = string.Empty;


            bool whileSt = false;
            bool descPull = false;

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

                    if (i == 0)
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Request for Quotation Number") || dt.Rows[i][c].ToString().Contains("Solicitação de Cotação Número"))
                                reference = dt.Rows[i][c].ToString().Replace("Request for Quotation Number", "").Replace(":", "").Replace("Solicitação de Cotação Número", "").Trim();

                            if (reference.Trim().EndsWith("-"))
                                reference = reference.Substring(0, reference.Length - 1).Trim();

                        }
                    }
                    else if (firstColStr.StartsWith("Lin"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Item")) _itemUniq = c;
                            else if (dt.Rows[i][c].ToString().Equals("Description") && dt.Rows[i + 1][c].ToString().Equals("Translation")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Contains("Manufacturer")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit") || dt.Rows[i][c].ToString().Equals("Unidade")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Equals("Quant.")) _itemQt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
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


                        if ((rowValueSpl[7] != null) && rowValueSpl[8] == null)
                        {
                            _itemUniq = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[5].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[7].ToString());
                        }



                        if (_itemCode != -1)
                        {
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemCode = iTemCode + dt.Rows[_i][_itemCode].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }

                            if (iTemCode.Contains(":"))
                            {
                                string[] codeSpl = iTemCode.Split(':');

                                if (codeSpl.Length == 2)
                                {
                                    iTemCode = codeSpl[0].ToString().Trim();
                                    iTemSUBJ = "MAKER: " + codeSpl[1].ToString().Trim();
                                }
                            }
                        }


                        if (_itemDesc != -1)
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemDESC = iTemDESC + " " + dt.Rows[_i][_itemDesc].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }



                        if (_itemUniq != -1)
                        {
                            iTemUniq = dt.Rows[i][_itemUniq].ToString().Trim();

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemUniq = iTemUniq + dt.Rows[_i][_itemUniq].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        if (_itemUnit != -1)
                        {
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                            if (iTemUnit.Equals("PART"))
                                iTemUnit = "PCS";
                        }

                        if (_itemQt != -1)
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;

                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;
                        iTemSUBJ = string.Empty;
                        iTemUniq = string.Empty;
                    }
                }
            }
        }
    }
}

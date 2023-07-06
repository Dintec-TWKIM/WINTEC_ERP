﻿using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class shipsSurveys_pdf
    {
        string vessel;
        string reference;
        string partner;
        string imoNumber;
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

        public string ImoNumber
        {
            get
            {
                return imoNumber;
            }
        }

        #endregion ==================================================================================================== Constructor



        public shipsSurveys_pdf(string fileName)
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

            string subjStr = string.Empty;
            string componentStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string descStr = string.Empty;
            string bookStr = string.Empty;
            


            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemQt = -1;
            int _itemUnit = -1;


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
                    string lineStr = string.Empty;

                    if (firstColStr.Length > 5)
                    {
                        lineStr = firstColStr.Substring(0, 1);

                        if (GetTo.IsInt(lineStr))
                        {
                            if (firstColStr.Substring(2, 1).Equals("-"))
                                firstColStr = lineStr;
                        }
                    }


                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("P.O."))
                                reference = dt.Rows[i+1][c].ToString().Trim();
                        }
                    }

                    if (firstColStr.StartsWith("Equipment"))
                    {
                        subjStr = firstColStr.Replace("Equipment", "").Replace(":", "").Trim();
                    }
                    else if (firstColStr.StartsWith("manufacturer"))
                    {
                        makerStr = firstColStr.Replace("Manufacturer", "").Replace(":", "").Trim();
                    }
                    else if (firstColStr.StartsWith("Nameplate Data"))
                    {
                        componentStr = firstColStr.Replace("Nameplate Data", "").Replace(":","").Trim();
                    }
                    else if (firstColStr.StartsWith("Instruction Book"))
                    {
                        bookStr = firstColStr.Replace("Instruction Book", "").Replace(":", "").Trim();
                    }
                    else if (firstColStr.StartsWith("SerialNumber"))
                    {
                        serialStr = firstColStr.Replace("SerialNumber", "").Replace(":", "").Trim();
                    }
                    else if (firstColStr.Equals("P"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Type / Type Info")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Article")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Qty")) _itemQt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr) && !dt.Rows[i - 1][0].ToString().Equals("Cod. For."))
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


                        if (rowValueSpl[6] != null && rowValueSpl[7] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[0].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[2].ToString());
                        }
                        else if (rowValueSpl[7] != null && rowValueSpl[8] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[0].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
                        }


                        if (_itemQt != -1)
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                            if (iTemQt.Contains(","))
                            {
                                int idx_s = iTemQt.IndexOf(",");

                                iTemQt = iTemQt.Substring(0, idx_s).Trim();
                            }

                            iTemQt = iTemQt.Replace(",", ".").Trim();
                        }


                        if (_itemUnit != -1)
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (_itemDesc != -1)
                        {
                            for (int c = _itemDesc; c < _itemUnit; c++)
                            {
                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                            }


                            int _i = i + 1;

                            string textStr = string.Empty;
                            if (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                lineStr = dt.Rows[_i][0].ToString().Substring(0, 1);
                            }

                            if (GetTo.IsInt(lineStr))
                            {
                                if (dt.Rows[_i][0].ToString().Substring(2, 1).Equals("-"))
                                    textStr = lineStr;
                            }
                            else
                            {
                                textStr = string.Empty;
                            }

                            while (!string.IsNullOrEmpty(textStr))
                            {
                                for (int c = _itemDesc; c < _itemUnit; c++)
                                {
                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                }
                                _i += 1;

                                if (_i < dt.Rows.Count)
                                    break;
                            }
                        }

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(componentStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + componentStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(bookStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "BOOK: " + bookStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "SERIAL NO: " + serialStr.Trim();

                        if (iTemDESC.Contains("-"))
                        {
                            int idx_s = iTemDESC.IndexOf(" - ");

                            if (idx_s != 0)
                            {
                                iTemDESC = iTemDESC.Substring(3, iTemDESC.Length - 3).Trim();
                            }
                        }


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if (GetTo.IsInt(iTemQt))
                            if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;
                        iTemSUBJ = string.Empty;
                        descStr = string.Empty;
                    }
                }
            }
        }
    }
}

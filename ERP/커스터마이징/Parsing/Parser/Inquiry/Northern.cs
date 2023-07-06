﻿using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Northern
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



        public Northern(string fileName)
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

            int _itemDesc = -1;
            int _itemUnit = -1;
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
                    string secondStr = dt.Rows[i][1].ToString();

                    if (firstColStr.Contains("1. Orders"))
                        break;


                    if (i == 1)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            reference = reference + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (i == 3)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            vessel = vessel + dt.Rows[i][c].ToString().Trim();
                        }

                        vessel = vessel.Replace("Ship Name", "").Trim();
                    }
                    else if (firstColStr.Contains("Department:"))
                    {
                        int _i = i + 1;
                        while (!dt.Rows[_i][0].ToString().Contains("ln") && !dt.Rows[_i][1].ToString().Contains("ln"))
                        {
                            subjStr = subjStr + dt.Rows[_i][0].ToString() + dt.Rows[_i][1].ToString();
                            subjStr = subjStr.Trim() + Environment.NewLine;
                            _i += 1;
                        }
                    }
                    else if (firstColStr.Equals("ln") || secondStr.Equals("ln"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Contains("Uom")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Contains("Quantity")) _itemQt = c;
                        }
                    }
                    else if ((GetTo.IsInt(firstColStr) || GetTo.IsInt(secondStr)) && !firstColStr.Contains("."))
                    {
                        if (!_itemUnit.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                if (dt.Rows[_i][_itemDesc].ToString().Contains("Part Number"))
                                {
                                    iTemCode = dt.Rows[_i][_itemDesc + 1].ToString().Trim();
                                }
                                else
                                {
                                    iTemDESC = iTemDESC + " " + dt.Rows[_i][_itemDesc].ToString().Trim();
                                }
                                _i += 1;
                            }
                        }


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        //dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;


                        iTemSUBJ = string.Empty;
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

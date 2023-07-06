﻿using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class NGL
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



        public NGL(string fileName)
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
            string subjStr2 = string.Empty;
            string subjStr3 = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemPos = -1;
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

                    if (string.IsNullOrEmpty(vessel))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Vessel"))
                            {
                                vessel = dt.Rows[i][c + 2].ToString().Trim();

                                if(string.IsNullOrEmpty(vessel))
                                    vessel = dt.Rows[i][c + 1].ToString().Trim();
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Enq. No"))
                            {
                                reference = dt.Rows[i][c + 2].ToString().Trim();

                                if (string.IsNullOrEmpty(reference))
                                    reference = dt.Rows[i][c + 1].ToString().Trim();
                            }
                        }
                    }

                    if(firstColStr.ToLower().StartsWith("machinery"))
                    {
                        for(int c = 0; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        int _i = i + 1;

                        while(!dt.Rows[_i][0].ToString().StartsWith("1)"))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                subjStr = subjStr + " " + dt.Rows[_i][c].ToString().Trim();
                            }
                            _i += 1;

                            if (_i > dt.Rows.Count)
                                break;
                        }

                        subjStr = subjStr.Replace("Type", "\r\nType").Replace("Serial", "\r\nSerial").Replace("Machinery :", "").Trim();
                    }

                    //if (firstColStr.StartsWith("Machinery"))
                    //{
                    //    subjStr = string.Empty;

                    //    for (int c = 0; c < dt.Columns.Count; c++)
                    //    {
                    //        subjStr = subjStr.Trim() + dt.Rows[i][c].ToString().Trim();
                    //    }

                    //    subjStr = subjStr.Replace("Type", "\r\nType").Replace("Serial", "\r\nSerial").Replace("Machinery :", "").Trim();
                    //}
                    //else if (firstColStr.StartsWith("Maker"))
                    //{
                    //    subjStr2 = string.Empty;

                    //    for (int c = 0; c < dt.Columns.Count; c++)
                    //    {
                    //        subjStr2 = subjStr2.Trim() + dt.Rows[i][c].ToString().Trim();
                    //    }

                    //    subjStr2 = subjStr2.Replace("Model", "\r\nModel").Replace("Book", "\r\nBook").Trim();
                    //}
                    else if (firstColStr.Equals("#"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("Part No")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Pos No")) _itemPos = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qnty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                        }
                    }
                    else if (firstColStr.Contains(")") && GetTo.IsInt(firstColStr.Substring(0,1)))
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


                        if (rowValueSpl[5] != null && rowValueSpl[6] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[5].ToString());
                        }


                        if (_itemDesc != -1)
                        {
                            for (int c = 0; c <= _itemDesc; c++)
                            {
                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                            }

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                for (int c = 0; c <= _itemDesc; c++)
                                {
                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                }

                                    _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        if (iTemDESC.Contains(")"))
                        {
                            int idx_e = iTemDESC.IndexOf(")");

                            iTemDESC = iTemDESC.Substring(idx_e, iTemDESC.Length - idx_e).Replace(")","").Trim();

                            if (iTemDESC.EndsWith("-"))
                            {
                                iTemDESC = iTemDESC.Substring(0, iTemDESC.Length - 2);
                            }

                        }

                        if (_itemCode != -1)
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (_itemQt != -1)
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (_itemUnit != -1)
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();


                        if(_itemPos != -1)
                        {
                            iTemDESC = iTemDESC + Environment.NewLine +"POS NO."+ dt.Rows[i][_itemPos].ToString();
                        }


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(subjStr2))
                            iTemSUBJ = iTemSUBJ.Trim() + subjStr2.Trim();

                        if (!dt.Rows[i-1][0].ToString().Contains(")"))
                        {
                            subjStr3 = dt.Rows[i - 1][0].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(subjStr3.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr3.Trim();

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

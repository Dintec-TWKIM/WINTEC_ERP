﻿using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class tms
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



        public tms(string fileName)
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

            int _vesselColInt = -1;

            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemCode = -1;

            string systemStr = string.Empty;
            string particularsStr = string.Empty;
            string makerStr = string.Empty;
            string subsystemStr = string.Empty;
            string dwgStr = string.Empty;
            string serialStr = string.Empty;

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
                    if (_vesselColInt.Equals(-1))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("REQUEST FOR QUOTATION"))
                                _vesselColInt = c;
                        }
                    }
                    else if (!_vesselColInt.Equals(-1) && (string.IsNullOrEmpty(reference) || string.IsNullOrEmpty(vessel)))
                    {
                        if (dt.Rows[i][_vesselColInt].ToString().StartsWith("Request For Quotation:"))
                            reference = dt.Rows[i][_vesselColInt].ToString().Replace("Request For Quotation:", "").Trim() + dt.Rows[i][_vesselColInt + 1].ToString().Replace("Request For Quotation:", "").Trim();
                        else if (dt.Rows[i][_vesselColInt].ToString().StartsWith("Vessel:"))
                            vessel = dt.Rows[i][_vesselColInt].ToString().Replace("Vessel:", "").Trim() + dt.Rows[i][_vesselColInt + 1].ToString().Replace("Vessel:", "").Trim();
                    }

                    string firstColStr = dt.Rows[i][0].ToString();
                    string secondColStr = dt.Rows[i][1].ToString();

                    if (firstColStr.StartsWith("Catalogue Group") || secondColStr.StartsWith("Catalogue Group"))
                    {
                        systemStr = firstColStr.Replace("Catalogue Group", "").Replace(":", "").Trim() + dt.Rows[i][1].ToString().Replace("Catalogue Group", "").Replace(":", "").Trim();
                    }
                    else if ((firstColStr.StartsWith("Maker") || secondColStr.StartsWith("Maker")))
                    {
                        if ((!firstColStr.ToUpper().Contains("N/A") && !secondColStr.ToUpper().Contains("N/A")))
                            makerStr = firstColStr.Replace("Maker", "").Replace(":", "").Trim() + dt.Rows[i][1].ToString().Replace("Maker", "").Replace(":", "").Trim();
                    }
                    else if (firstColStr.StartsWith("Serial No") || secondColStr.StartsWith("Serial No"))
                    {
                        serialStr = dt.Rows[i][0].ToString().Trim() + dt.Rows[i][1].ToString().Trim();

                        serialStr = serialStr.Replace("Serial No", "").Replace(":", "").Trim();
                    }
                    else if (firstColStr.StartsWith("Particulars") || secondColStr.StartsWith("Particulars"))
                    {
                        particularsStr = dt.Rows[i][0].ToString().Trim() + dt.Rows[i][1].ToString().Trim();

                        particularsStr = particularsStr.Replace("Particulars", "").Replace(":", "").Trim();
                    }
                    else if (GetTo.IsInt(firstColStr.Replace(".", "")))
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
                            _itemQt = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[5].ToString());
                        }
                        else if (rowValueSpl[3] != null && rowValueSpl[4] == null)
                        {
                            _itemQt = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[3].ToString());
                        }
                        else if (rowValueSpl[2] != null && rowValueSpl[3] == null)
                        {
                            _itemQt = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = -1;
                            _itemDesc = Convert.ToInt16(rowValueSpl[2].ToString());
                        }


                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Replace("*", "").Trim();

                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                if (dt.Rows[_i][_itemDesc].ToString().Contains("Total for Items"))
                                    break;

                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim();

                                _i += 1;

                                if (_i > dt.Rows.Count)
                                    break;
                            }
                        }

                        if (!_itemQt.Equals(-1))
                        {
                            string[] qtUnitSpl = dt.Rows[i][_itemQt].ToString().Split(' ');

                            if (qtUnitSpl.Length >= 2)
                            {
                                iTemQt = qtUnitSpl[0].ToString().Trim();
                                iTemUnit = qtUnitSpl[1].ToString().Trim();
                            }
                            else
                            {
                                qtUnitSpl = dt.Rows[i][_itemQt + 1].ToString().Split(' ');

                                if (qtUnitSpl.Length >= 2)
                                {
                                    iTemQt = qtUnitSpl[0].ToString().Trim();
                                    iTemUnit = qtUnitSpl[1].ToString().Trim();
                                }
                            }
                        }





                        // SUBJECT
                        if (!string.IsNullOrEmpty(systemStr))
                            iTemSUBJ = systemStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "SERIAL NO: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(particularsStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + particularsStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr;


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr.Replace(".", "");
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
                        iTemSUBJ = string.Empty;
                    }
                }
            }
        }
    }
}

﻿using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class KSS
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



        public KSS(string fileName)
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

            string subjStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string rmkStr = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemUnit = -1;
            int _itemQt = -1;

            bool eqState = false;
            bool tyState = false;
            bool maState = false;
            bool seState = false;


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
                    string firstColStr = dt.Rows[i][0].ToString().ToLower();

                    if (firstColStr.Contains("requisition no"))
                    {
                        for(int c = 1; c < dt.Columns.Count; c++)
						{
                            if (dt.Rows[i][c].ToString().StartsWith("Issued"))
                                break;
                            
                            reference = reference.Trim() + dt.Rows[i][c].ToString().Trim();
						}

                        if (string.IsNullOrEmpty(reference))
                            reference = reference.Replace("Issued", "").Trim();

                    }

                    

                    if (firstColStr.Contains("equipment"))
                    {
                        eqState = true;
                        subjStr = string.Empty;
                        typeStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(subjStr) && eqState)
                                subjStr =subjStr + dt.Rows[i][c].ToString().Trim();

                            if (dt.Rows[i][c].ToString().Contains("TYPE / MODEL"))
                            {
                                eqState = false;
                                tyState = true;
                            }

                            if (string.IsNullOrEmpty(typeStr) && tyState)
                                typeStr = typeStr + dt.Rows[i][c].ToString().Replace("TYPE / MODEL", "").Trim();
                        }
                    }
                    else if (firstColStr.Contains("MAKER"))
                    {
                        maState = true;
                        makerStr = string.Empty;
                        serialStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(makerStr) && maState)
                                makerStr = makerStr.Trim() + dt.Rows[i][c].ToString().Trim();

                            if (dt.Rows[i][c].ToString().Contains("SERIAL NO"))
                            {
                                maState = false;
                                seState = true;
                            }

                            if (string.IsNullOrEmpty(serialStr) && seState)
                                serialStr = serialStr + dt.Rows[i][c].ToString().Replace("SERIAL NO.", "").Trim();
                        }
                    }
                    else if (firstColStr.Contains("REMARK"))
                    {
                        rmkStr = string.Empty;

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            rmkStr = rmkStr.Trim() + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.Equals("No"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Code")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Item Name") || dt.Rows[i][c].ToString().StartsWith("[Page No]")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (!_itemQt.Equals(-1))
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                            if (string.IsNullOrEmpty(iTemQt))
                            {
                                iTemQt = dt.Rows[i - 1][_itemQt].ToString().Trim() + " " + dt.Rows[i + 1][_itemQt].ToString().Trim(); 
                            }

                            if (iTemQt.Contains("/"))
                            {
                                int idx_lts = iTemQt.IndexOf("/");

                                iTemQt = iTemQt.Substring(0, idx_lts).Trim();
                            }
                        }

                        if (!_itemUnit.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            if (string.IsNullOrEmpty(iTemDESC))
                            {
                                iTemDESC = dt.Rows[i - 1][_itemDesc].ToString().Trim() + " " + dt.Rows[i + 1][_itemDesc].ToString().Trim(); 
                            }
                        }

                        if (!string.IsNullOrEmpty(subjStr.Trim()))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(typeStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(rmkStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "REMARK: " + rmkStr.Trim();


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

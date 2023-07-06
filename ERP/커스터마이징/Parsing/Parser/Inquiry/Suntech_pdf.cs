﻿using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Suntech_pdf
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



        public Suntech_pdf(string fileName)
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
            dtItem.Columns.Add("UNIQ");         //선사코드
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

            string subjString = string.Empty;
            string assemStr = string.Empty;

            string refStr = string.Empty;
            string drwStr = string.Empty;



            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemCode = -1;
            int _itemUnit = -1;

            int _itemRef = -1;
            int _itemDrw = -1;

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
                    string firstColString = dt.Rows[i][0].ToString();

                    if (firstColString.StartsWith("Requisition No"))
                    {
                        reference = dt.Rows[i][1].ToString().Trim();
                        vessel = dt.Rows[i][4].ToString().Trim() + dt.Rows[i][5].ToString().Trim() + dt.Rows[i][6].ToString().Trim();
                        vessel = vessel.Replace("Vessel", "").Replace(":", "").Trim();
                    }
                    else if (firstColString.StartsWith("Equipment:"))
                    {
                        subjString = string.Empty;

                        for (int c = 0; c < dt.Columns.Count; c++)
                            subjString = subjString.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                    }
                    else if (firstColString.StartsWith("Assembly"))
                    {
                        assemStr = string.Empty;

                        for (int c = 0; c < dt.Columns.Count; c++)
                            assemStr = assemStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                    }
                    else if (firstColString.Equals("No."))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("Part No.")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Quantity")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("UoM")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Contains("Ref. No.")) _itemRef = c;
                            else if (dt.Rows[i][c].ToString().Contains("Drawing No.")) _itemDrw = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColString))
                    {
                        if (_itemCode != -1)
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (_itemQt != -1)
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                            if (string.IsNullOrEmpty(iTemQt))
                                iTemQt = dt.Rows[i][_itemUnit - 1].ToString().Trim();
                        }

                        if (_itemUnit != -1)
                        {
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                            if (iTemUnit.ToLower().Equals("unit"))
                                iTemUnit = "PCS";
                        }

                        if (_itemDesc != -1)
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;

                            while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
							{
                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim();


                                iTemDESC = iTemDESC.Replace("Office Notes:\r\nOffice Notes:", "Office Notes:").Trim();
                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
							}
                        }

                        if (_itemRef != -1)
                            refStr = dt.Rows[i][_itemRef].ToString().Trim();

                        if (_itemDrw != -1)
                            drwStr = dt.Rows[i][_itemDrw].ToString().Trim();

                        if (!string.IsNullOrEmpty(refStr))
                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + "REF. NO: " + refStr;

                        if (!string.IsNullOrEmpty(drwStr))
                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + "DRAWING NO: " + drwStr;


                        if(!string.IsNullOrEmpty(subjString))
                            iTemSUBJ = subjString.Trim();

                        if (!string.IsNullOrEmpty(assemStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + assemStr.Trim();


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColString;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemQt = string.Empty;
                        iTemUnit = string.Empty;
                        iTemDESC = string.Empty;
                        iTemSUBJ = string.Empty;
                    }
                }
            }
        }
    }
}

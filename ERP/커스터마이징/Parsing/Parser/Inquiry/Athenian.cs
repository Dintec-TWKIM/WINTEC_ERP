using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class Athenian
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



        public Athenian(string fileName)
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
            int _itemCode = -1;
            int _itemDwg = -1;
            int _itemMaker = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemRmk = -1;

            string componentStr = string.Empty;
            string makerStr = string.Empty;
            string modelStr = string.Empty;
            string serialStr = string.Empty;


            string itemMaker = string.Empty;
            string itemDwg = string.Empty;
            string itemRmk = string.Empty;



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

                    if (firstColStr.StartsWith("Enquiry No"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                            {
                                reference = dt.Rows[i][c].ToString().Trim();
                                break;
                            }
                        }
                    }
                    else if (firstColStr.StartsWith("Vessel"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                            {
                                vessel = dt.Rows[i][c].ToString().Trim();
                                break;
                            }
                        }
                    }
                    else if (firstColStr.StartsWith("Component"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                            {
                                componentStr = dt.Rows[i][c].ToString().Trim();
                                break;
                            }
                        }
                    }
                    else if (firstColStr.StartsWith("Maker"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                            {
                                makerStr = dt.Rows[i][c].ToString().Trim();
                                break;
                            }
                        }
                    }
                    else if (firstColStr.StartsWith("Model/Type"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                            {
                                modelStr = dt.Rows[i][c].ToString().Trim();
                                break;
                            }
                        }
                    }
                    else if (firstColStr.StartsWith("Serial No"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                            {
                                serialStr = dt.Rows[i][c].ToString().Trim();
                                break;
                            }
                        }
                    }
                    else if (firstColStr.StartsWith("No"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("Part No")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("DWG No")) _itemDwg = c;
                            else if (dt.Rows[i][c].ToString().Equals("Maker")) _itemMaker = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qty Req")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Equals("Supply Specs")) _itemRmk = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (!_itemUnit.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][_itemDesc].ToString().Trim();
                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        if (!_itemRmk.Equals(-1))
                        {
                            itemRmk = dt.Rows[i][_itemRmk].ToString().Trim();

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                itemRmk = itemRmk.Trim() + " " + dt.Rows[_i][_itemRmk].ToString().Trim();
                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }
                            

                        if (!_itemDwg.Equals(-1))
                            itemDwg = dt.Rows[i][_itemDwg].ToString().Trim();

                        if (!_itemMaker.Equals(-1))
                            itemMaker = dt.Rows[i][_itemMaker].ToString().Trim();

                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();





                        if (!string.IsNullOrEmpty(componentStr))
                            iTemSUBJ = componentStr;

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr;

                        if (!string.IsNullOrEmpty(modelStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr;

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr;

                        if (!string.IsNullOrEmpty(itemDwg))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG NO: " + itemDwg;




                        if (!string.IsNullOrEmpty(itemRmk))
                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + itemRmk.Trim();

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
                        iTemSUBJ = string.Empty;
                        itemDwg = string.Empty;
                        itemMaker = string.Empty;
                        itemRmk = string.Empty;
                    }
                }
            }
        }
    }
}

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Everlast
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



        public Everlast(string fileName)
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
            string nameStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string makerStr = string.Empty;
            string componentStr = string.Empty;

            int _itemMaker = -1;
            int _itemDwg = -1;
            int _itemUnit = -1;
            int _itemQt = -1;
            int _itemDesc = -1;


            string descDwg = string.Empty;
            string descMaker = string.Empty;

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

                    if (firstColStr.StartsWith("Vessel Name"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if(string.IsNullOrEmpty(vessel))
                                vessel = dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Order Number"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if(string.IsNullOrEmpty(reference))
                                reference = dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Equipment Name"))
                    {
                        subjStr = string.Empty;

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Equipment Type"))
                    {
                        typeStr = string.Empty;

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            typeStr = typeStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Serial Number"))
                    {
                        serialStr = string.Empty;

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            serialStr = serialStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Component Notes"))
                    {
                        componentStr = string.Empty;

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            componentStr = componentStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                        int _i = i + 1;

                        while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                componentStr = componentStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                            }
                                _i += 1;
                        }
                    }
                    else if (firstColStr.Equals("Item"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Part Name")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Makers Reference")) _itemMaker = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Drawing")) _itemDwg = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        // row 값 가져와서 배열에 넣은후 값 추가하기
                        string[] rowValueSpl = new string[30];
                        int columnCount = 0;
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
                            {
                                rowValueSpl[columnCount] = c.ToString();
                                columnCount++;
                            }
                        }

                        // desc, maker, dwg, unit, qty, 0,0,0,0                       
                        if (rowValueSpl[9] != null && rowValueSpl[10] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemMaker = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemDwg = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                        }
                        else
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemMaker = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemDwg = -1;
                            _itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
                        }



                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if(!_itemUnit.Equals(-1))
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



                        if (!_itemMaker.Equals(-1))
                        {
                            descMaker = dt.Rows[i][_itemMaker].ToString().Trim();

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                descMaker = descMaker + " " + dt.Rows[_i][_itemMaker].ToString().Replace("Spare Part Notes:", "").Trim();
                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }
                            

                        if(!_itemDwg.Equals(-1))
                        {
                            descDwg = dt.Rows[i][_itemDwg].ToString().Trim();
                            int _i = i + 1;

                            while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                descDwg = descDwg + " " + dt.Rows[_i][_itemDwg].ToString().Trim();
                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                       


                        // DESCRIPTION
                        if (!string.IsNullOrEmpty(descMaker))
                            //iTemDESC = iTemDESC.Trim() + Environment.NewLine + descMaker.Trim();
                            iTemCode = descMaker.Trim();

                        if (!string.IsNullOrEmpty(descDwg))
                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + "DRW NO: " + descDwg.Trim();




                        // SUBJECT
                        if(!string.IsNullOrEmpty(subjStr.Trim()))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(typeStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(componentStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + componentStr.Trim();



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

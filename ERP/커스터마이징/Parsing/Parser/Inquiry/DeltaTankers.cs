using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class DeltaTankers
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



        public DeltaTankers(string fileName)
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

            int _itemDescInt = -1;
            int _itemQtInt = -1;

            string modelStr = string.Empty;
            string detailStr = string.Empty;
            string equipStr = string.Empty;
            string makerStr = string.Empty;
            string serialStr = string.Empty;

            string manualStr = string.Empty;



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

                    if (firstColStr.Equals("Vessel"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Vessel"))
                                vessel = dt.Rows[i][c + 1].ToString().Replace("M/T", "").Trim();
                            else if (dt.Rows[i][c].ToString().Contains("Our Ref"))
                                reference = dt.Rows[i][c + 1].ToString().Trim();
                        }
                    }
                    else if (firstColStr.Equals("Equipment"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Equipment"))
                                equipStr = dt.Rows[i][c + 1].ToString().Trim();
                            else if (dt.Rows[i][c].ToString().Equals("Maker"))
                                makerStr = dt.Rows[i][c + 1].ToString().Trim();
                        }
                    }
                    else if (firstColStr.Equals("Model/Type"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Model/Type"))
                                modelStr = dt.Rows[i][c + 1].ToString().Trim();
                            else if (dt.Rows[i][c].ToString().Contains("Serial"))
                            {
                                serialStr = dt.Rows[i][c + 1].ToString().Trim();

                                if (string.IsNullOrEmpty(serialStr))
                                    serialStr = dt.Rows[i][c].ToString().Replace("Serial No", "").Trim();
                            }
                        }
                    }
                    else if (firstColStr.Equals("Equip.Details"))
                    {
                        detailStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.Equals("Manual"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            manualStr = manualStr + dt.Rows[i][c].ToString().Trim();
                        }

                        manualStr = manualStr + Environment.NewLine;

                        int _i = i + 1;
                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                manualStr = manualStr + dt.Rows[_i][c].ToString().Trim();
                            }

                            _i += 1;

                            manualStr = manualStr + Environment.NewLine;
                        }
                        manualStr = manualStr.Replace("Requested", "").Trim();
                    }
                    else if (firstColStr.Equals("#"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Items")) _itemDescInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Quantity")) _itemQtInt = c;
                        }
                    }
                    else if (firstColStr.StartsWith("("))
                    {
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


                        if (rowValueSpl[3] != null && rowValueSpl[4] == null)
                        {
                            _itemDescInt = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemQtInt = Convert.ToInt16(rowValueSpl[2].ToString());
                        }

                        iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();
                        iTemUnit = dt.Rows[i][_itemQtInt + 1].ToString().Trim();

                        iTemDESC = dt.Rows[i][_itemDescInt].ToString().Trim() + Environment.NewLine;
                        

                        int _i = i + 1;

                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || !dt.Rows[_i][0].ToString().StartsWith("("))
                        {
                            for (int c = 0; c <= _itemDescInt; c++)
                            {
                                iTemDESC = iTemDESC + dt.Rows[_i][c].ToString().Trim();
                            }
                            _i += 1;

                            if (_i >= dt.Rows.Count - 1)
                                break;
                        }

                        if (!string.IsNullOrEmpty(equipStr))
                            iTemSUBJ = equipStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(modelStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(detailStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + detailStr.Trim();

                        if (!string.IsNullOrEmpty(manualStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + manualStr.Trim();

                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr.Replace("(", "").Replace(")", "").Trim();
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;


                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                    }
                }
            }
        }
    }
}

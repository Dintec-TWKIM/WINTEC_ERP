using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class Dole
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



        public Dole(string fileName)
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
            int _itemQt = -1;
            int _itemUnit = -1;

            string subjStr = string.Empty;
            string typeStr = string.Empty;
            string makerStr = string.Empty;
            string serialStr = string.Empty;
            string drwStr = string.Empty;


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
                    string secondColStr = dt.Rows[i][1].ToString();


                    if (firstColStr.StartsWith("Vessel"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(vessel))
                                vessel = dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Order no"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(reference))
                                reference = dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (secondColStr.StartsWith("For group"))
                    {
                        subjStr = dt.Rows[i][2].ToString().Trim();
                    }
                    else if (secondColStr.StartsWith("Type"))
                    {
                        typeStr = dt.Rows[i][2].ToString().Trim();
                    }
                    else if (secondColStr.StartsWith("Maker"))
                    {
                        makerStr = dt.Rows[i][2].ToString().Trim();
                    }
                    else if (secondColStr.StartsWith("Serial no"))
                    {
                        serialStr = dt.Rows[i][2].ToString().Trim();
                    }
                    else if (secondColStr.StartsWith("Draw./file"))
                    {
                        drwStr = dt.Rows[i][2].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Item"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Part no.")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Item description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                        }
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
                            _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
                        }
                        else if (rowValueSpl[6] != null && rowValueSpl[7] == null)
                        {
                            _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
                        }


                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Replace(".", "").Replace(" ", "").Trim();

                        if (!_itemDesc.Equals(-1))
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                        if (!_itemQt.Equals(-1))
                        {
                            if (GetTo.IsInt(dt.Rows[i][_itemQt].ToString()))
                            {
                                iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                                iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();
                            }
                            else
                            {
                                iTemQt = dt.Rows[i][_itemQt].ToString().Substring(0, 1).Trim();
                                iTemUnit = dt.Rows[i][_itemQt].ToString().Substring(1, dt.Rows[i][_itemQt].ToString().Length - 1).Trim();
                            }
                        }


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr;

                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(drwStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG: " + drwStr.Trim();



                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr.Replace(".", "").Trim();
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

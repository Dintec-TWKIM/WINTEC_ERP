using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Pantheon
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



        public Pantheon(string fileName)
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
            string partStr = string.Empty;
            string serialStr = string.Empty;
            string subjSub = string.Empty;
            string makerStr2 = string.Empty;

            string descStr = string.Empty;

            int _itemCode = -1;
            int _itemDesc = -1;
            int _itemQt = -1;

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


                    if (string.IsNullOrEmpty(vessel))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Vessel"))
                            {
                                vessel = dt.Rows[i][c].ToString().Replace("Vessel", "").Replace(":", "").Trim();

                                if (string.IsNullOrEmpty(vessel))
                                    vessel = dt.Rows[i][c+1].ToString().Trim();
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Inquiry No:"))
                            {
                                reference = dt.Rows[i][c].ToString().Replace("Inquiry No", "").Replace(":", "").Trim();

                                if (string.IsNullOrEmpty(reference))
                                    reference = dt.Rows[i][c+1].ToString().Trim();
                            }
                        }
                    }
                    
                    if (firstColStr.StartsWith("System"))
                    {
                        subjStr = dt.Rows[i][1].ToString().Replace("UNDEFINED", "").Replace("GROUP", "").Replace("[*]", "").Trim();
                    }
                    else if (firstColStr.StartsWith("Maker"))
                    {
                        if (string.IsNullOrEmpty(makerStr))
                            makerStr = dt.Rows[i][1].ToString().Trim();
                        else
                            makerStr2 = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Particulars"))
                    {
                        partStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Serial No"))
                    {
                        serialStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Subsystem"))
                    {
                        subjSub = dt.Rows[i][1].ToString().Replace("UNDEFINED", "").Replace("GROUP", "").Replace("[*]", "").Trim();
                    }
                    else if (!string.IsNullOrEmpty(firstColStr) && (GetTo.IsInt(firstColStr.Replace(".", "")) || (GetTo.IsInt(firstColStr.Substring(0,1)) && firstColStr.Contains("."))))
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

                        _itemCode = 2;
                        _itemDesc = 1;

                        if ((rowValueSpl[3] != null) && (rowValueSpl[4] == null))
                        {
                            _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[3].ToString());

                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();
                        }
                        else if ((rowValueSpl[2] != null) && (rowValueSpl[3] == null))
                        {
                            _itemCode = Convert.ToInt16(rowValueSpl[0].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[2].ToString());
                        }
                        else
                        {
                            string[] descStr1 = dt.Rows[i][_itemDesc].ToString().Split(',');

                            if (descStr1.Length == 3)
                            {
                                iTemDESC = descStr1[0].ToString().Trim();
                                iTemCode = descStr1[1].ToString().Replace("Part No.", "").Replace(":", "").Replace("Part. No.", "").Trim() + descStr1[2].ToString().Replace("Part No.", "").Replace(":", "").Replace("Part. No.", "").Trim();
                            }
                            else
                            {
                                iTemDESC = descStr1[0].ToString().Trim();
                                iTemCode = descStr1[1].ToString().Replace("Part No.", "").Replace(":", "").Replace("Part. No.", "").Trim();
                            }
                        }


                        if (string.IsNullOrEmpty(iTemDESC) && _itemDesc != -1)
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                        if (string.IsNullOrEmpty(iTemCode) && _itemCode != -1)
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();


                        
                        if(iTemCode.Contains(" ") && iTemCode.Contains("."))
                        {
                            string[] codeSpl = iTemCode.Split(' ');

                            if(codeSpl.Length >= 2)
                            {
                                firstColStr = codeSpl[0].ToString().Trim();
                                iTemCode = codeSpl[1].ToString().Trim();
                            }
                        }


                        string[] qtSpl = dt.Rows[i][_itemQt].ToString().Split(' ');

                        if (qtSpl.Length >= 2)
                        {
                            iTemQt = qtSpl[0].ToString().Trim();
                            iTemUnit = qtSpl[1].ToString().Trim();
                        }



                        if (!string.IsNullOrEmpty(subjStr.Trim()))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/N: " + serialStr.Trim();
                        
                        if (!string.IsNullOrEmpty(partStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + partStr.Trim();

                        if (!string.IsNullOrEmpty(subjSub.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjSub.Trim();

                        if (!string.IsNullOrEmpty(makerStr2.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr2.Trim();


                        int _i = i + 1;

                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                        {
                            if (string.IsNullOrEmpty(dt.Rows[_i][_itemDesc].ToString()))
                                break;

                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim();

                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr.Replace(".","");
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

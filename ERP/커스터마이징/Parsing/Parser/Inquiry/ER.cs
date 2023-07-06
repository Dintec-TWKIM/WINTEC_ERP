using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class ER
    {
        string vessel;
        string reference;
        string contact;
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

        public string Contact
        {
            get
            {
                return contact;
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



        public ER(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            contact = string.Empty;

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
            int _itemQt = -1;
            int _itemCode = -1;

            string systemStr = string.Empty;
            string particularsStr = string.Empty;
            string serialStr = string.Empty;
            string subsystemStr = string.Empty;


            string subjStr = string.Empty;
            string labelStr = string.Empty;
            string brandStr = string.Empty;
            string typeStr = string.Empty;


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
                    if (string.IsNullOrEmpty(vessel))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Vessel"))
                            {
                                if (c + 2 < dt.Columns.Count)
                                {
                                    vessel = dt.Rows[i][c].ToString().Trim() + dt.Rows[i][c + 1].ToString().Trim() + dt.Rows[i][c + 2].ToString().Trim();

                                    vessel = vessel.Replace("Vessel", "").Replace(":", "").Replace(",", "").Trim();

                                    string[] vesselSpl = vessel.Split(' ');

                                    if (vesselSpl.Length >= 2)
                                    {
                                        vessel = vesselSpl[vesselSpl.Length - 1].ToString().Trim();
                                    }

                                    reference = dt.Rows[i + 1][c].ToString().Trim() + dt.Rows[i + 1][c + 1].ToString().Trim() + dt.Rows[i + 1][c + 2].ToString().Trim();

                                    reference = reference.Replace("RFQ no.", "").Replace(":", "").Replace(",", "").Trim();

                                    if (reference.ToUpper().Contains("IMO"))
                                    {
                                        int idx_s = fileName.IndexOf("temp\\");

                                        reference = fileName.Substring(idx_s, fileName.Length - idx_s).Replace("temp\\", "").Replace(".pdf","");
                                    }
                                }
                            }
                        }
                    }



                    string firstColStr = dt.Rows[i][0].ToString();

                    if (firstColStr.StartsWith("System"))
                    {
                        systemStr = firstColStr.Replace("System", "").Replace(":", "").Trim();
                    }
                    else if (firstColStr.StartsWith("Particulars"))
                    {
                        particularsStr = firstColStr.Replace("Particulars", "").Replace(":", "").Trim();
                    }
                    else if (firstColStr.StartsWith("Serial No"))
                    {
                        serialStr = firstColStr.Replace("Serial No", "").Replace(":", "").Replace(".","").Trim();
                    }
                    else if (firstColStr.StartsWith("Subsystem"))
                    {
                        subsystemStr = firstColStr.Replace("Subsystem", "").Replace(":", "").Trim();
                    }
                    else if (firstColStr.StartsWith("Category"))
                    {
                        subjStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Label"))
                    {
                        labelStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Brand"))
                    {
                        brandStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Type"))
                    {
                        typeStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.Equals("No.") || firstColStr.StartsWith("Pos"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("QTY.") || dt.Rows[i][c].ToString().StartsWith("Quantity")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Item Code") || dt.Rows[i][c].ToString().StartsWith("Part N")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Product Description") || dt.Rows[i][c].ToString().StartsWith("Description")) _itemDesc = c;
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


                        if (rowValueSpl[4] != null && rowValueSpl[5] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
                        }
                        else if (rowValueSpl[3] != null && rowValueSpl[4] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = -1;
                            _itemQt = Convert.ToInt16(rowValueSpl[2].ToString());
                        }


                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString();

                        if (!_itemQt.Equals(-1))
                        {
                            string[] qtspl = dt.Rows[i][_itemQt].ToString().Split(' ');

                            if (qtspl.Length >= 2)
                            {
                                iTemQt = qtspl[0].ToString().Replace(",", ".").Trim();
                                iTemUnit = qtspl[1].ToString().Trim();
                            }

                            if (string.IsNullOrEmpty(iTemQt))
                            {
                                iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                                iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Trim();
                            }
                        }

                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][_itemDesc].ToString()))
                            {
                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString();
                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }


                        // SUBJECT
                        if (!string.IsNullOrEmpty(systemStr))
                            iTemSUBJ = systemStr;

                        if (!string.IsNullOrEmpty(particularsStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + particularsStr;

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr;

                        if (!string.IsNullOrEmpty(subsystemStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subsystemStr;


                        // SUBJECT2
                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(labelStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + labelStr.Trim();

                        if (!string.IsNullOrEmpty(brandStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + brandStr.Trim();

                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();


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

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Interorient
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



        public Interorient(string fileName)
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
            int _itemCodeInt = -1;
            int _itemUnitInt = -1;
            int _itemQtInt = -1;

            string subjStr = string.Empty;
            string subjModelStr = string.Empty;
            string subjMakerStr = string.Empty;
            string subjSerialStr = string.Empty;
            string subjEndStr = string.Empty;

            int subjInt = -1;


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

                    if (subjInt.Equals(-1))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("INTERORIENT")) subjInt = c;
                        }
                    }

                    if (firstColStr.ToUpper().Contains("VESSEL NAME"))
                    {
                        vessel = dt.Rows[i][1].ToString().Trim();
                        if (string.IsNullOrEmpty(vessel))
                            vessel = dt.Rows[i][2].ToString().Trim();
                    }
                    else if (firstColStr.ToUpper().Contains("ORDER REFERENCE"))
                    {
                        reference = dt.Rows[i][1].ToString().Trim();
                        if (string.IsNullOrEmpty(reference))
                        {
                            reference = dt.Rows[i][2].ToString().Trim();

                            if (string.IsNullOrEmpty(reference))
                            {
                                reference = dt.Rows[i + 1][1].ToString().Trim();

                                if (string.IsNullOrEmpty(reference))
                                    reference = dt.Rows[i + 1][2].ToString().Trim();
                            }
                        }
                    }
                    else if (firstColStr.ToUpper().Contains("TITLE / EQ"))
                    {
                        for (int c = 1; c <= subjInt; c++)
                        {
                            subjStr = subjStr.Trim() + dt.Rows[i][c].ToString().Trim();

                            //if (string.IsNullOrEmpty(dt.Rows[i + 1][0].ToString()))
                            //{

                            //}
                        }
                    }
                    else if (firstColStr.ToUpper().Contains("MODEL") && !subjInt.Equals(-1))
                    {
                        for (int c = 1; c < subjInt; c++)
                        {
                            subjModelStr = subjModelStr.Trim() + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.ToUpper().Contains("MANUFACTURER") && !subjInt.Equals(-1))
                    {
                        for (int c = 1; c < subjInt; c++)
                        {
                            subjMakerStr = subjMakerStr.Trim() + dt.Rows[i][1].ToString().Trim();
                        }
                    }
                    else if (firstColStr.ToUpper().Contains("SERIAL") && !subjInt.Equals(-1))
                    {
                        for (int c = 1; c < subjInt; c++)
                        {
                            subjSerialStr = subjSerialStr.Trim() + dt.Rows[i][1].ToString().Trim();
                        }
                    }
                    else if (firstColStr.ToUpper().Contains("REMARK") && !subjInt.Equals(-1))
                    {
                        for (int c = 1; c <= subjInt; c++)
                        {
                            subjEndStr = subjEndStr.Trim() + dt.Rows[i][1].ToString().Trim();
                        }

                        if (!dt.Rows[i + 1][0].ToString().Contains("Certificate"))
                            subjEndStr = subjEndStr.Trim() + dt.Rows[i + 1][0].ToString().Trim();
                    }
                    else if (firstColStr.ToUpper().Contains("NBR"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Item / Article")) _itemDescInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Part Number")) _itemCodeInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Unit")) _itemUnitInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Approved")) _itemQtInt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (!_itemDescInt.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i][_itemDescInt].ToString().Trim();
                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                if (dt.Rows[_i][_itemDescInt].ToString().ToUpper().Contains("ALL PARTS ARE TO BE") || dt.Rows[_i][0].ToString().ToUpper().Contains("ALL PARTS ARE TO BE"))
                                {
                                    break;
                                }
                                else
                                {
                                    iTemDESC = iTemDESC.Trim() + dt.Rows[_i][_itemDescInt].ToString().Trim();
                                    _i += 1;
                                }
                            }
                        }

                        if (!_itemCodeInt.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCodeInt].ToString().Trim();

                        if (!_itemQtInt.Equals(-1))
                        {
                            iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();

                            if (string.IsNullOrEmpty(iTemQt))
                            {
                                iTemQt = dt.Rows[i][_itemQtInt + 1].ToString().Trim();
                                //_itemQtInt = _itemQtInt + 1;

                                //iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();
                            }

                        }

                        if (!_itemUnitInt.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(subjModelStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + subjModelStr.Trim();

                        if (!string.IsNullOrEmpty(subjMakerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + subjMakerStr.Trim();

                        if (!string.IsNullOrEmpty(subjSerialStr.Replace("N/A", "")))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + subjSerialStr.Trim();

                        if (!string.IsNullOrEmpty(subjEndStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjEndStr.Trim();


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
                        iTemQt = string.Empty;
                        iTemCode = string.Empty;
                    }
                }
            }
        }
    }
}

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Glovis
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



        public Glovis(string fileName)
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
            string iTemUniq = string.Empty;

            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemCode = -1;

            int _itemMach = -1;
            int _itemEquip = -1;

            string descCode = string.Empty;

            string makerStr = string.Empty;
            string descStr = string.Empty;
            string modelStr = string.Empty;
            string equipStr = string.Empty;

            int vesselInt = -1;

            string vesselStr = string.Empty;



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

                    if (firstColStr.Contains("Quotation") && string.IsNullOrEmpty(reference))
                    {
                        reference = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.Contains("Vessel Name"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("DRAW NO.")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Contains("Item No")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Contains("Qty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Mach. Desc.")) _itemMach = c;
                            else if (dt.Rows[i][c].ToString().Contains("Equip.")) _itemEquip = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr) && firstColStr.Length < 3)
                    {
                        if (string.IsNullOrEmpty(vesselStr))
                        {
                            vesselStr = dt.Rows[i - 1][0].ToString().Trim();
                            vesselInt = 1;

                            if (string.IsNullOrEmpty(vesselStr))
                            {
                                vesselStr = dt.Rows[i - 2][0].ToString().Trim();
                                vesselInt = 2;
                            }

                            if (string.IsNullOrEmpty(vesselStr))
                            {
                                vesselStr = dt.Rows[i - 3][0].ToString().Trim();
                                vesselInt = 3;
                            }
                        }

                        // row 값 가져와서 배열에 넣은후 값 추가하기
                        string[] rowValueSpl = new string[20];
                        int columnCount = 0;
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            for (int r = i - vesselInt; r <= i; r++)
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[r][c].ToString()))
                                {
                                    rowValueSpl[columnCount] = c.ToString();
                                    columnCount++;
                                    break;
                                }

                                if (r >= dt.Rows.Count)
                                    break;
                            }

                            
                        }


                        if ((rowValueSpl[9] != null) && rowValueSpl[10] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                            _itemMach = Convert.ToInt16(rowValueSpl[7].ToString());
                            _itemEquip = Convert.ToInt16(rowValueSpl[8].ToString());
                        }
                        else if (rowValueSpl[8] != null && rowValueSpl[9] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemMach = Convert.ToInt16(rowValueSpl[6].ToString());
                            _itemEquip = Convert.ToInt16(rowValueSpl[7].ToString());
                        }
                        else if (rowValueSpl[7] != null && rowValueSpl[8] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemMach = Convert.ToInt16(rowValueSpl[6].ToString());
                            _itemEquip = Convert.ToInt16(rowValueSpl[7].ToString());
                        }
                        else if (rowValueSpl[6] != null && rowValueSpl[7] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemMach = Convert.ToInt16(rowValueSpl[5].ToString());
                            _itemEquip = Convert.ToInt16(rowValueSpl[6].ToString());
                        }

                        if (!_itemDesc.Equals(-1))
                        {
                            if (_itemDesc.Equals(2))
                            {
                                for (int r = i; r >= i - vesselInt; r--)
                                {
                                    for (int c = 1; c <= _itemCode; c++)
                                    {
                                        if (!string.IsNullOrEmpty(dt.Rows[r][c].ToString()))
                                        {
                                            iTemDESC = iTemDESC.Trim() + " " + dt.Rows[r][c].ToString().Trim();
                                            break;
                                        }
                                    }
                                }
                                //for (int c = 1; c <= _itemCode; c++)
                                //{
                                //    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                                //}
                            }
                            else
                            {
                                for (int c = _itemDesc; c <= _itemCode; c++)
                                {
                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                                }
                            }

                            // 선사코드
                            iTemUniq = dt.Rows[i+1][_itemDesc].ToString().Trim();


                            if (GetTo.IsInt(iTemUniq) || iTemUniq.Length < 10)
                            {
                                iTemUniq = dt.Rows[i + 1][_itemCode].ToString().Trim();

                                if (string.IsNullOrEmpty(iTemUniq))
                                    iTemUniq = dt.Rows[i + 2][_itemCode].ToString().Trim();
                            }

                            if (string.IsNullOrEmpty(iTemUniq))
                                iTemUniq = dt.Rows[i + 1][_itemDesc + 1].ToString().Trim();


                        }

                        if (!_itemCode.Equals(-1))
                        {
                            //iTemCode = dt.Rows[i][_itemCode].ToString().Trim();
                            descCode = dt.Rows[i - vesselInt][_itemCode].ToString().Trim();
                            iTemUnit = dt.Rows[i - vesselInt][_itemCode + 1].ToString().Trim();

                            if (GetTo.IsInt(iTemUnit))
                            {
                                iTemUnit = dt.Rows[i - vesselInt][_itemCode].ToString().Trim();

                                if (GetTo.IsInt(iTemUnit.Replace(" ", "")))
                                {
                                    iTemUnit = dt.Rows[i - vesselInt][_itemCode + 2].ToString().Trim();
                                    _itemQt = _itemQt + 1;
                                    _itemMach = _itemMach + 1;
                                    _itemEquip = _itemEquip + 1;
                                }
                            }
                            else if (!iTemUnit.Equals("PCS") && !iTemUnit.Equals("EA"))
                            {
                                if (_itemEquip + 1 < dt.Columns.Count)
                                {
                                    _itemQt = _itemQt + 1;
                                    _itemMach = _itemMach + 1;
                                    _itemEquip = _itemEquip + 1;
                                }

                                iTemUnit = dt.Rows[i - vesselInt][_itemCode + 2].ToString().Trim();
                            }

                            if (!string.IsNullOrEmpty(descCode))
                            {
                                if(!iTemDESC.Contains(descCode))
                                    iTemDESC = iTemDESC.Trim() + ", " + descCode.Trim();
                            }
                        }

                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i - vesselInt][_itemQt].ToString().Trim();

                        if (!_itemMach.Equals(-1))
                        {
                            descStr = dt.Rows[i - vesselInt][_itemMach].ToString().Trim();
                            modelStr = dt.Rows[i][_itemMach].ToString().Trim();
                            makerStr = dt.Rows[i + vesselInt][_itemMach].ToString().Trim();

                            if (!string.IsNullOrEmpty(descStr))
                                iTemSUBJ = descStr;

                            if (!string.IsNullOrEmpty(modelStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr.Trim();

                            if (!string.IsNullOrEmpty(makerStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();
                        }

                        if (!_itemEquip.Equals(-1))
                        {
                            equipStr = dt.Rows[i - vesselInt][_itemEquip].ToString().Trim();

                            if (!string.IsNullOrEmpty(equipStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + equipStr.Trim();
                        }



                        if (string.IsNullOrEmpty(vessel))
                        {
                            vessel = vesselStr.Trim();
                        }

                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);

                        if (GetTo.IsInt(iTemQt))
                            if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;

                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;

                        iTemSUBJ = string.Empty;
                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;
                        vesselStr = string.Empty;
                        iTemUniq = string.Empty;
                    }
                }
            }
        }
    }
}

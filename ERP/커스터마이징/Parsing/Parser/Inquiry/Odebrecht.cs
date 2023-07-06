using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Odebrecht
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



        public Odebrecht(string fileName)
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


            int _itemCode = -1;
            int _itemDesc = -1;
            int _itemMaker = -1;
            int _itemUnit = -1;
            int _itemQt = -1;
            int _itemDescS = -1;

            string descMaker = string.Empty;


            bool whileSt = false;
            bool descPull = false;

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

                    if (i == 0)
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Request for Quotation Number") || dt.Rows[i][c].ToString().Contains("Solicitação de Cotação Número"))
                                reference = dt.Rows[i][c].ToString().Replace("Request for Quotation Number", "").Replace(":", "").Replace("Solicitação de Cotação Número", "").Trim();

                            if (reference.Trim().EndsWith("-"))
                                reference = reference.Substring(0, reference.Length - 1).Trim();

                        }
                    }
                    else if (firstColStr.StartsWith("Lin"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Item")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Description") || dt.Rows[i][c].ToString().Equals("Descrição")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Contains("Manufacturer")) _itemMaker = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit") || dt.Rows[i][c].ToString().Equals("Unidade")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Equals("Quant.")) _itemQt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
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


                        if ((rowValueSpl[7] != null) && rowValueSpl[8] ==null)
                        {
                            _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDescS = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemMaker = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[5].ToString());
                            _itemQt= Convert.ToInt16(rowValueSpl[7].ToString());



                            if (!_itemMaker.Equals(-1))
                            {
                                descMaker = dt.Rows[i][_itemMaker].ToString().Trim();

                                int _i = i + 1;
                                while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || !(Convert.ToInt16(firstColStr) + 1 == Convert.ToInt16(dt.Rows[_i][0].ToString())) || (dt.Rows[_i][_itemMaker] != null))
                                {
                                    descMaker = descMaker.Trim() + dt.Rows[_i][_itemMaker].ToString().Trim();

                                    _i += 1;

                                    if (_i >= dt.Rows.Count)
                                        break;

                                    if (dt.Rows[_i][0].ToString().StartsWith("Upon receipt"))
                                        break;

                                    if (string.IsNullOrEmpty(dt.Rows[_i][_itemMaker].ToString()))
                                        break;
                                }
                            }



                            if (!_itemCode.Equals(-1))
                            {
                                iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                                int _i = i + 1;
                                while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || !(Convert.ToInt16(firstColStr) + 1 == Convert.ToInt16(dt.Rows[_i][0].ToString())))
                                {
                                    if (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                    {
                                        iTemCode = iTemCode.Trim() + dt.Rows[_i][0].ToString().Trim();
                                        descPull = true;
                                        break;
                                    }
                                    else
                                    {
                                        iTemCode = iTemCode.Trim() + dt.Rows[_i][_itemCode].ToString().Trim();
                                        descPull = false;
                                    }

                                    

                                    _i += 1;
                                    if (_i >= dt.Rows.Count)
                                        break;



                                    if (dt.Rows[_i][0].ToString().StartsWith("Upon receipt"))
                                        break;

                                    if (string.IsNullOrEmpty(dt.Rows[_i][_itemCode].ToString()))
                                        break;
                                }

                                if (iTemCode.Length == 12 || iTemCode.Length == 13)
                                {
                                    iTemCode = iTemCode.Trim() + dt.Rows[i + 1][0].ToString();
                                    whileSt = true;
                                }
                            }


                            if (!_itemUnit.Equals(-1))
                                iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                            if (!_itemQt.Equals(-1))
                            {
                                iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                                if (string.IsNullOrEmpty(iTemQt))
                                {
                                    iTemQt = dt.Rows[i][_itemQt + 1].ToString().Trim();
                                }
                            }

                            if (!_itemDesc.Equals(-1))
                            {
                                iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                                if (whileSt)
                                {
                                    if (descPull)
                                    {
                                        for (int c = _itemDesc - 1; c < _itemMaker; c++)
                                        {
                                            iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i + 1][c].ToString().Trim();
                                        }
                                    }
                                    else
                                    {
                                        for (int c = _itemDescS + 1; c < _itemMaker; c++)
                                        {
                                            iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i + 1][c].ToString().Trim();
                                        }
                                    }

                                    

                                    int _i = i + 2;

                                    while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || !(Convert.ToInt16(firstColStr) + 1 == Convert.ToInt16(dt.Rows[_i][0].ToString())))
                                    {
                                        if (descPull)
                                        {
                                            for (int c = _itemDesc - 1; c < _itemMaker; c++)
                                            {
                                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                            }
                                        }
                                        else
                                        {
                                            for (int c = _itemDescS + 1; c < _itemMaker; c++)
                                            {
                                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                            }
                                        }


                                        _i += 1;
                                        if (_i >= dt.Rows.Count)
                                            break;

                                        if (dt.Rows[_i][0].ToString().StartsWith("Upon receipt") || dt.Rows[_i][_itemDesc].ToString().StartsWith("Solicitação de Cotação Número") || dt.Rows[_i][0].ToString().StartsWith("Após"))
                                            break;

                                        if (string.IsNullOrEmpty(dt.Rows[_i][_itemDesc].ToString()) && string.IsNullOrEmpty(dt.Rows[_i][_itemDesc - 1].ToString()) && string.IsNullOrEmpty(dt.Rows[_i][_itemDesc + 1].ToString()))
                                            break;
                                    }
                                }
                                else
                                {
                                    int _i = i + 1;

                                    while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || !(Convert.ToInt16(firstColStr) + 1 == Convert.ToInt16(dt.Rows[_i][0].ToString())))
                                    {
                                        for (int c = _itemDescS + 1; c < _itemMaker; c++)
                                        {
                                            iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                        }


                                        _i += 1;
                                        if (_i >= dt.Rows.Count)
                                            break;

                                        if (dt.Rows[_i][0].ToString().StartsWith("Upon receipt") || dt.Rows[_i][_itemDesc].ToString().StartsWith("Solicitação de Cotação Número") || dt.Rows[_i][0].ToString().StartsWith("Após"))
                                            break;

                                        if (string.IsNullOrEmpty(dt.Rows[_i][_itemDesc].ToString()) && string.IsNullOrEmpty(dt.Rows[_i][_itemDesc - 1].ToString()) && string.IsNullOrEmpty(dt.Rows[_i][_itemDesc + 1].ToString()))
                                            break;
                                    }
                                }
                            }


                            if (!string.IsNullOrEmpty(descMaker))
                            {
                                string[] descSpl = descMaker.Split(':');

                                if (descSpl.Length == 2)
                                {
                                    iTemSUBJ = "MAKER: " + descSpl[1].ToString();
                                }
                            }
                                //iTemSUBJ = "MAKER: " + descMaker.Trim();

                            if (iTemUnit == "PART" || iTemUnit == "PECA")
                                iTemUnit = "PCS";

                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                            if (!string.IsNullOrEmpty(iTemSUBJ))
                                dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                            iTemDESC = string.Empty;
                            iTemUnit = string.Empty;
                            iTemCode = string.Empty;
                            iTemQt = string.Empty;
                            iTemSUBJ = string.Empty;
                            whileSt = false;
                        }
                        else if (rowValueSpl[6] != null && rowValueSpl[7] == null)
                        {
                            _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDescS = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemMaker = -1;
                            _itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[6].ToString());



                            if (!_itemMaker.Equals(-1))
                            {
                                descMaker = dt.Rows[i][_itemMaker].ToString().Trim();

                                int _i = i + 1;
                                while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || !(Convert.ToInt16(firstColStr) + 1 == Convert.ToInt16(dt.Rows[_i][0].ToString())) || (dt.Rows[_i][_itemMaker] != null))
                                {
                                    descMaker = descMaker.Trim() + dt.Rows[_i][_itemMaker].ToString().Trim();

                                    _i += 1;

                                    if (_i >= dt.Rows.Count)
                                        break;

                                    if (dt.Rows[_i][0].ToString().StartsWith("Upon receipt"))
                                        break;

                                    if (string.IsNullOrEmpty(dt.Rows[_i][_itemMaker].ToString()))
                                        break;
                                }
                            }



                            if (!_itemCode.Equals(-1))
                            {
                                iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                                int _i = i + 1;
                                while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || !(Convert.ToInt16(firstColStr) + 1 == Convert.ToInt16(dt.Rows[_i][0].ToString())))
                                {
                                    if (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                    {
                                        iTemCode = iTemCode.Trim() + dt.Rows[_i][0].ToString().Trim();
                                        descPull = true;
                                        break;
                                    }
                                    else
                                    {
                                        iTemCode = iTemCode.Trim() + dt.Rows[_i][_itemCode].ToString().Trim();
                                        descPull = false;
                                    }



                                    _i += 1;
                                    if (_i >= dt.Rows.Count)
                                        break;



                                    if (dt.Rows[_i][0].ToString().StartsWith("Upon receipt"))
                                        break;

                                    if (string.IsNullOrEmpty(dt.Rows[_i][_itemCode].ToString()))
                                        break;
                                }

                                if (iTemCode.Length == 13 || iTemCode.Length == 12)
                                {
                                    iTemCode = iTemCode.Trim() + dt.Rows[i + 1][0].ToString();
                                    whileSt = true;
                                }
                            }


                            if (!_itemUnit.Equals(-1))
                                iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                            if (!_itemQt.Equals(-1))
                            {
                                iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                                if (string.IsNullOrEmpty(iTemQt))
                                {
                                    iTemQt = dt.Rows[i][_itemQt + 1].ToString().Trim();
                                }
                            }

                            if (!_itemDesc.Equals(-1))
                            {


                                iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();



                                

                                if (whileSt)
                                {
                                    if (descPull)
                                    {
                                        iTemDESC =iTemDESC.Trim() + " " + dt.Rows[i][_itemDesc - 1].ToString().Trim();
                                    }
                                    else
                                    {
                                        iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][_itemDesc].ToString().Trim();
                                    }

                                    int _i = i + 2;

                                    while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || !(Convert.ToInt16(firstColStr) + 1 == Convert.ToInt16(dt.Rows[_i][0].ToString())))
                                    {
                                        
                                            iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][_itemDesc].ToString().Trim();


                                        _i += 1;
                                        if (_i >= dt.Rows.Count)
                                            break;

                                        if (dt.Rows[_i][0].ToString().StartsWith("Upon receipt") || dt.Rows[_i][_itemDesc].ToString().StartsWith("Solicitação de Cotação Número") || dt.Rows[_i][0].ToString().StartsWith("Após"))
                                            break;

                                        if (string.IsNullOrEmpty(dt.Rows[_i][_itemDesc].ToString()) && string.IsNullOrEmpty(dt.Rows[_i][_itemDesc - 1].ToString()) && string.IsNullOrEmpty(dt.Rows[_i][_itemDesc + 1].ToString()))
                                            break;
                                    }
                                }
                                else
                                {
                                    int _i = i + 1;

                                    while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || !(Convert.ToInt16(firstColStr) + 1 == Convert.ToInt16(dt.Rows[_i][0].ToString())))
                                    {
                                        for (int c = _itemDescS + 1; c < _itemMaker; c++)
                                        {
                                            iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                        }


                                        _i += 1;
                                        if (_i >= dt.Rows.Count)
                                            break;

                                        if (dt.Rows[_i][0].ToString().StartsWith("Upon receipt") || dt.Rows[_i][_itemDesc].ToString().StartsWith("Solicitação de Cotação Número") || dt.Rows[_i][0].ToString().StartsWith("Após"))
                                            break;

                                        if (string.IsNullOrEmpty(dt.Rows[_i][_itemDesc].ToString()) && string.IsNullOrEmpty(dt.Rows[_i][_itemDesc - 1].ToString()) && string.IsNullOrEmpty(dt.Rows[_i][_itemDesc + 1].ToString()))
                                            break;
                                    }
                                }
                            }


                            if (!string.IsNullOrEmpty(descMaker))
                            {
                                string[] descSpl = descMaker.Split(':');

                                if (descSpl.Length == 2)
                                {
                                    iTemSUBJ = "MAKER: " + descSpl[1].ToString();
                                }
                            }
                            //iTemSUBJ = "MAKER: " + descMaker.Trim();

                            if (iTemUnit == "PART" || iTemUnit == "PECA")
                                iTemUnit = "PCS";

                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                            if (!string.IsNullOrEmpty(iTemSUBJ))
                                dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                            iTemDESC = string.Empty;
                            iTemUnit = string.Empty;
                            iTemCode = string.Empty;
                            iTemQt = string.Empty;
                            iTemSUBJ = string.Empty;
                            whileSt = false;
                        }
                        else if (rowValueSpl[5] == null)
                        {

                        }
                    }
                }
            }
        }
    }
}

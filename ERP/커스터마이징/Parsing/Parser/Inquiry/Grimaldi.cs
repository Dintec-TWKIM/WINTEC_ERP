using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Grimaldi
    {
        string vessel;
        string reference;
        DataTable dtItem;
        string contact;

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

        public string Contact
        {
            get
            {
                return contact;
            }
        }

        #endregion ==================================================================================================== Constructor



        public Grimaldi(string fileName)
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

            int _itemDescInt = -1;
            int _itemQtInt = -1;
            int _itemCodeInt = -1;
            int _itemOurCodeInt = -1;
            int _itemUnit = -1;


            string componentSubjStr = string.Empty;
            string makerSubjStr = string.Empty;
            string typeSubjStr = string.Empty;
            string serialSubjStr = string.Empty;

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
                    string thirdColStr = dt.Rows[i][2].ToString();


                    if(string.IsNullOrEmpty(vessel))
                    {
                        for(int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Ship :") || dt.Rows[i][c].ToString().Equals("Nave"))
                            {
                                vessel = dt.Rows[i][c+1].ToString().Replace("M/N", "").Replace(",", "").Replace("ENGINE", "").Trim();
                            }
                        }
                    }

                    if(string.IsNullOrEmpty(reference))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Request No"))
                            {
                                reference = dt.Rows[i][c + 1].ToString().Trim();
                            }
                        }
                    }


                    // SUBJECT
                    if (secondColStr.Contains("For component") || secondColStr.Contains("omponente"))
                    {
                        componentSubjStr = string.Empty;
                        for (int c = 2; c < dt.Columns.Count; c++)
                        {
                            componentSubjStr = componentSubjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (secondColStr.Contains("Maker"))
                    {
                        makerSubjStr = string.Empty;
                        for (int c = 2; c < dt.Columns.Count; c++)
                        {
                            makerSubjStr = makerSubjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (secondColStr.Contains("Type"))
                    {
                        typeSubjStr = string.Empty;
                        for (int c = 2; c < dt.Columns.Count; c++)
                        {
                            typeSubjStr = typeSubjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim(); 
                        }
                    }
                    else if (secondColStr.Contains("Serial No"))
                    {
                        serialSubjStr = string.Empty;
                        for (int c = 2; c < dt.Columns.Count; c++)
                        {
                            serialSubjStr = serialSubjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        if (serialSubjStr.Equals("0"))
                            serialSubjStr = string.Empty;
                    }
                    else if (firstColStr.Contains("Buyer:"))
                    {
                        contact = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (secondColStr.Contains("Buyer:"))
                    {
                        contact = dt.Rows[i][2].ToString().Trim();
                    }


                    // SUBJECT 한칸 밀린 경우.. 대비
                    if (thirdColStr.Contains("For component") || thirdColStr.Contains("omponent"))
                    {
                        componentSubjStr = string.Empty;
                        for (int c = 3; c < dt.Columns.Count; c++)
                        {
                            componentSubjStr = componentSubjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (thirdColStr.Contains("Maker"))
                    {
                        makerSubjStr = string.Empty;
                        for (int c = 3; c < dt.Columns.Count; c++)
                        {
                            makerSubjStr = makerSubjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (thirdColStr.Contains("Type"))
                    {
                        typeSubjStr = string.Empty;
                        for (int c = 3; c < dt.Columns.Count; c++)
                        {
                            typeSubjStr = typeSubjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (thirdColStr.Contains("Serial No"))
                    {
                        serialSubjStr = string.Empty;
                        for (int c = 3; c < dt.Columns.Count; c++)
                        {
                            serialSubjStr = serialSubjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        if (serialSubjStr.Equals("0"))
                            serialSubjStr = string.Empty;
                    }

                    else if (firstColStr.Equals("Line") || firstColStr.Equals("Riga"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Your Cod.")) _itemCodeInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Description")) _itemDescInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQtInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Our Cod.")) _itemOurCodeInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Ums")) _itemUnit = c;

                            else if (dt.Rows[i][c].ToString().Equals("Cod. Nostro")) _itemCodeInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Descrizione")) _itemDescInt = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Qt")) _itemQtInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Cod. Vostro")) _itemOurCodeInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Ums")) _itemUnit = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (!_itemCodeInt.Equals(-1))
                        {
                            iTemCode = dt.Rows[i][_itemCodeInt].ToString().Trim();

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                iTemCode = iTemCode.Trim() + " " + dt.Rows[_i][_itemCodeInt].ToString().Trim();
                                //drwStr =  dt.Rows[_i][_itemCodeInt].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }

                            iTemCode = iTemCode.Trim();

                            if (iTemCode.EndsWith("/"))
                                iTemCode = iTemCode.Substring(0, iTemCode.Length - 2);

                            //iTemCode = iTemCode.Replace("Item", "").Replace("Drw. NO", "").Replace("Drw.", "").Replace("No","").Replace("no","").Replace(".","").Trim();
                            //iTemCode = iTemCode.Replace("Item", "").Replace(".", "").Trim();
                            iTemCode = iTemCode.Trim();
                        }

                        if (!_itemDescInt.Equals(-1))
                        {
                            for (int c = _itemOurCodeInt + 1; c < _itemCodeInt; c++)
                            {
                                iTemDESC = iTemDESC + " " +dt.Rows[i][c].ToString().Trim();
                            }

                            int _i = i +1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || dt.Rows[_i][0].ToString().ToUpper().Contains("DETAILS"))
                            {
                                for(int c = _itemOurCodeInt; c < _itemCodeInt; c++)
                                {
                                    iTemDESC = iTemDESC + " " + dt.Rows[_i][c].ToString().Trim();
                                }

                                iTemDESC = iTemDESC.Trim();

                                _i += 1;
                                iTemDESC = iTemDESC.Trim() + Environment.NewLine;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        if (!_itemQtInt.Equals(-1))
                        {
                            iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();

                            if (string.IsNullOrEmpty(iTemQt))
                                iTemQt = dt.Rows[i][_itemQtInt + 1].ToString().Trim();

                            if (iTemQt.Contains(","))
                            {
                                string[] unitSpl = iTemQt.Split(',');

                                iTemQt = unitSpl[0].ToString().Trim();
                            }
                        }

                        if (!_itemUnit.Equals(-1))
                        {
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                            if (iTemUnit.StartsWith("N") || iTemUnit.StartsWith("n"))
                                iTemUnit = "PCS";
                        }

                        if (string.IsNullOrEmpty(iTemUnit))
                            iTemUnit = "PCS";

                        if (!string.IsNullOrEmpty(componentSubjStr.Trim()))
                            iTemSUBJ = componentSubjStr;

                        if (!string.IsNullOrEmpty(makerSubjStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerSubjStr;

                        if (!string.IsNullOrEmpty(typeSubjStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeSubjStr;

                        if (!string.IsNullOrEmpty(serialSubjStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialSubjStr;

                        //if (!string.IsNullOrEmpty(drwStr))
                        //{
                        //    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DRW: " + drwStr.Trim();
                        //}

                        iTemDESC = iTemDESC.Trim();
                        iTemSUBJ = iTemSUBJ.Trim();
                        
                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemNo = string.Empty;
                        iTemDESC = string.Empty;
                        iTemQt = string.Empty;
                        iTemUnit = string.Empty;
                        iTemSUBJ = string.Empty;

                        drwStr = string.Empty;
                    }
                }
            }
        }
    }
}

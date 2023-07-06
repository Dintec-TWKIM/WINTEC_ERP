using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Universal
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



        public Universal(string fileName)
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
            dtItem.Columns.Add("UNIQ");         //선사코드
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

            string subjString = string.Empty;

            int _referenceCheckInt = -1;

            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemCode = -1;
            int _itemUnit = -1;

            string makerStr = string.Empty;
            string mainStr = string.Empty;
            string serialStr = string.Empty;
            string subsystemStr = string.Empty;
            string catStr = string.Empty;
            
            string motorStr = string.Empty;
            string partStr = string.Empty;
            string capStr = string.Empty;
            string dwgStr = string.Empty;

            bool itemStart = false;

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
                    string firstColString = dt.Rows[i][0].ToString();
                    string secondColStr = dt.Rows[i][1].ToString();

                    if (_referenceCheckInt.Equals(-1))
                    {
                        for (int c = 3; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Request For Quotation:"))
                            {
                                _referenceCheckInt = c;
                                reference = dt.Rows[i][c + 1].ToString().Trim();
                                itemStart = true; 
                            }

                        }
                    }
                    else if (itemStart)
                    {
                        if (string.IsNullOrEmpty(vessel))
                        {
                            for (int c = _referenceCheckInt; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().Equals("Vessel:"))
                                    vessel = dt.Rows[i][c + 1].ToString().Trim();

                                if (!string.IsNullOrEmpty(vessel))
                                    break;
                            }
                        }

                        if (firstColString.Equals("No."))
                        {
                            //QTY, PACKING, ITEM CODE, DESCRIPTION
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().Contains("QTY."))
                                    _itemQt = c;

                                if (dt.Rows[i][c].ToString().Equals("ITEM CODE"))
                                    _itemCode = c;

                                if (dt.Rows[i][c].ToString().Equals("PACKING"))
                                    _itemUnit = c;

                                if (dt.Rows[i][c].ToString().Contains("DESCRIPTION"))
                                    _itemDesc = c;
                            }
                        }
                        else if (firstColString.Contains("Maker") && !firstColString.Contains("Serial No"))
                        {
                            if(string.IsNullOrEmpty(makerStr))
                                makerStr = firstColString.Replace("Maker", "").Replace(":", "").Replace("-", "").Replace("N/A", "").Trim();
                            else
                                makerStr = makerStr + ", " + firstColString.Replace("Maker", "").Replace(":", "").Replace("-", "").Trim();
                        }
                        else if (secondColStr.Contains("Maker") && !secondColStr.Contains("Serial No"))
                        {
                            if (string.IsNullOrEmpty(makerStr))
                                makerStr = secondColStr.Replace("Maker", "").Replace(":", "").Replace("-", "").Replace("N/A", "").Trim();
                            else
                                makerStr = makerStr + ", " + secondColStr.Replace("Maker", "").Replace(":", "").Replace("-", "").Trim();
                        }
                        else if (firstColString.Contains("System"))
                        {
                            mainStr = firstColString.Replace("System", "").Replace(":", "").Trim();
                        }
                        else if (secondColStr.Contains("System"))
                        {
                            mainStr = secondColStr.Replace("System", "").Replace(":", "").Trim();
                        }
                        else if (firstColString.Contains("Subsystem"))
                        {
                            subsystemStr = firstColString.Replace("Subsystem", "").Replace(":", "").Trim();
                        }
                        else if (secondColStr.Contains("Subsystem"))
                        {
                            subsystemStr = secondColStr.Replace("Subsystem", "").Replace(":", "").Trim();
                        }
                        else if (firstColString.Contains("Catalogue"))
                        {
                            catStr = firstColString.Replace("Catalogue:", "").Trim();
                        }
                        else if (secondColStr.Contains("Catalogue"))
                        {
                            catStr = secondColStr.Replace("Catalogue:", "").Trim();
                        }
                        else if (firstColString.StartsWith("Particulars"))
                        {
                            partStr = firstColString.Trim();
                        }
                        else if (secondColStr.StartsWith("Particulars"))
                        {
                            partStr = secondColStr.Trim();
                        }
                        else if (firstColString.StartsWith("MOTOR"))
                        {
                            motorStr = firstColString.Trim();
                        }
                        else if (secondColStr.StartsWith("MOTOR"))
                        {
                            motorStr = secondColStr.Trim();
                        }
                        else if (firstColString.StartsWith("DWG No"))
                        {
                            dwgStr = firstColString.Trim();
                        }
                        else if (secondColStr.StartsWith("DWG No"))
                        {
                            dwgStr = secondColStr.Trim();
                        }
                        else if (firstColString.StartsWith("CAP"))
                        {
                            capStr = firstColString.Trim();
                        }
                        else if (secondColStr.StartsWith("CAP"))
                        {
                            capStr = secondColStr.Trim();
                        }


                        if (GetTo.IsInt(firstColString.Replace(".", "")))
                        {

                            string[] valueSplit = dt.Rows[i][_itemQt].ToString().Split(' ');

                            if (valueSplit.Length == 2)
                            {
                                iTemQt = valueSplit[0].ToString().Trim();
                                iTemUnit = valueSplit[1].ToString().Trim();

                                iTemCode = dt.Rows[i][_itemQt + 1].ToString().Trim();

                                if (string.IsNullOrEmpty(iTemCode))
                                    iTemCode = dt.Rows[i][_itemQt + 2].ToString().Trim();
                            }
                            else
                            {
                                string testCode = dt.Rows[i][_itemCode].ToString();

                                if (!string.IsNullOrEmpty(testCode))
                                {
                                   
                                        string[] valueSplit2 = dt.Rows[i][_itemQt + 1].ToString().Split(' ');

                                        if (valueSplit2.Length == 2)
                                        {
                                            iTemQt = valueSplit2[0].ToString().Trim();
                                            iTemUnit = valueSplit2[1].ToString().Trim();
                                        }

                                    if(string.IsNullOrEmpty(iTemCode))
                                        iTemCode = testCode;
                                }
                            }


                            if (!string.IsNullOrEmpty(mainStr))
                                iTemSUBJ = mainStr;

                            if (!string.IsNullOrEmpty(makerStr))
                                iTemSUBJ = iTemSUBJ + Environment.NewLine + "MAKER: " + makerStr;

                            if (!string.IsNullOrEmpty(subsystemStr))
                                iTemSUBJ = iTemSUBJ + Environment.NewLine + subsystemStr;

                            if (!string.IsNullOrEmpty(catStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + catStr;

                            if (!string.IsNullOrEmpty(capStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + capStr;

                            if (!string.IsNullOrEmpty(partStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + partStr;

                            if (!string.IsNullOrEmpty(motorStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + motorStr;

                            if (!string.IsNullOrEmpty(dwgStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + dwgStr;


                            iTemSUBJ = iTemSUBJ.Trim();



                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();
                            int _i = i+1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                if (!dt.Rows[_i][_itemDesc].ToString().Equals("TOTAL PRICE"))
                                {
                                    iTemDESC = iTemDESC.Trim() + Environment.NewLine +  dt.Rows[_i][_itemDesc].ToString().Trim();
                                    _i += 1;
                                }
                                else
                                {
                                    break;
                                }
                            }


                            if (string.IsNullOrEmpty(iTemCode))
                                iTemCode = dt.Rows[i][_itemCode].ToString().Trim();


                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                            if(!string.IsNullOrEmpty(iTemSUBJ))
                                dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                            iTemQt = string.Empty;
                            iTemUnit = string.Empty;
                            iTemDESC = string.Empty;
                            iTemSUBJ = string.Empty;
                        }
                    }
                }
            }
        }
    }
}

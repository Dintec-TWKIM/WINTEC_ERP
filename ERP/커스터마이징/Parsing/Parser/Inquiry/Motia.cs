using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Motia
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



        public Motia(string fileName)
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
            int _itemUnit = -1;
            int _itemQt = -1;

            string subjectStr = string.Empty;
            string subjStr = string.Empty;
            string subjCodeStr = string.Empty;
            string subjModelStr = string.Empty;
            string makerStr = string.Empty;
            string subjBookStr = string.Empty;
            string serialStr = string.Empty;

            string descStr1 = string.Empty;
            string descStr2 = string.Empty;
            string descStr3 = string.Empty;
            string descStr4 = string.Empty;
            string descStr5 = string.Empty;
            

            bool nameCheck = false;
            bool codeCheck = false;
            bool modelCheck = false;
            bool bookCheck = false;
            bool serialCheck = false;
            bool makerCheck = false;

            
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


                    if (firstColStr.StartsWith("RFQ N") && string.IsNullOrEmpty(reference))
                    {
                        reference = dt.Rows[i][0].ToString().Replace("RFQ N", "").Replace(":", "").Replace("°", "").Trim();
                    }
                    else if (firstColStr.StartsWith("Phone:"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                            vessel = vessel.Trim() + dt.Rows[i + 1][c].ToString().Replace("M/T","").Trim();

                    }
                    else if (firstColStr.Contains("Subject"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            subjectStr = subjectStr + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Dear Sirs"))
                    {
                        i = i + 3;

                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Name"))
                                nameCheck = true;
                            else if (dt.Rows[i][c].ToString().StartsWith("Hierarchy"))
                                nameCheck = false;

                            if (nameCheck)
                            {
                                subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                            }
                        }


                        int _i = i + 1;
                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || !GetTo.IsInt(dt.Rows[_i][0].ToString()))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[_i][c].ToString().StartsWith("Code"))
                                {
                                    codeCheck = true;
                                    modelCheck = false;
                                    bookCheck = false;
                                    serialCheck = false;
                                    makerCheck = false;
                                }
                                else if (dt.Rows[_i][c].ToString().StartsWith("Model"))
                                {
                                    codeCheck = false;
                                    modelCheck = true;
                                    bookCheck = false;
                                    serialCheck = false;
                                    makerCheck = false;
                                }
                                else if (dt.Rows[_i][c].ToString().StartsWith("Instr"))
                                {
                                    codeCheck = false;
                                    modelCheck = false;
                                    bookCheck = true;
                                    serialCheck = false;
                                    makerCheck = false;
                                }
                                else if (dt.Rows[_i][c].ToString().StartsWith("Sr No"))
                                {
                                    codeCheck = false;
                                    modelCheck = false;
                                    bookCheck = false;
                                    serialCheck = true;
                                    makerCheck = false;
                                }
                                else if (dt.Rows[_i][c].ToString().StartsWith("Maker"))
                                {
                                    codeCheck = false;
                                    modelCheck = false;
                                    bookCheck = false;
                                    serialCheck = false;
                                    makerCheck = true;
                                }
                                else if (dt.Rows[_i][c].ToString().StartsWith("Location"))
                                {
                                    codeCheck = false;
                                    modelCheck = false;
                                    bookCheck = false;
                                    serialCheck = false;
                                    makerCheck = false;
                                }


                                if (codeCheck)
                                    subjCodeStr = subjCodeStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                else if (modelCheck)
                                    subjModelStr = subjModelStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                else if (bookCheck)
                                    subjBookStr = subjBookStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                else if (serialCheck)
                                    serialStr = serialStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                else if (makerCheck)
                                    makerStr = makerStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                            }
                                _i += 1;
                        }

                    }
                    else if (secondColStr.Contains("No"))
                    {
                        for (int c = 2; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Item /")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Contains("UOM")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().Contains("Q.ty")) _itemQt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (!_itemDesc.Equals(-1))
                        {

                            for (int c = 1; c < _itemUnit; c++)
                            {
                                descStr1 = descStr1 + " " + dt.Rows[i][c].ToString().Trim();
                            }

                            int _i = i+1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                for (int c = 1; c < dt.Columns.Count; c++)
                                {
                                    if (_i == i + 1 && i + 1 <= dt.Rows.Count)
                                        descStr2 = descStr2 + dt.Rows[_i][c].ToString().Trim();
                                    else if (_i == i + 2 && i + 2 <= dt.Rows.Count)
                                        descStr3 = descStr3 + dt.Rows[_i][c].ToString().Trim();
                                    else if (_i == i + 4 && i + 4 <= dt.Rows.Count)
                                        descStr4 = descStr4 + dt.Rows[_i][c].ToString().Trim();
                                    else if (_i == i + 5 && i + 5 <= dt.Rows.Count)
                                        descStr5 = descStr5 + dt.Rows[_i][c].ToString().Trim();
                                }
                                _i += 1;
                            }
                        }


                        if (!_itemUnit.Equals(-1))
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();



                        if (!string.IsNullOrEmpty(descStr1))
                            iTemDESC = descStr1.Replace("Item:", "").Trim();

                        if (!string.IsNullOrEmpty(descStr4))
                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + descStr4.Trim();

                        if (!string.IsNullOrEmpty(descStr5))
                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + descStr5.Trim();

                        

                        if (!string.IsNullOrEmpty(descStr3))
                            iTemCode = descStr3.Replace("Part No.:", "").Trim();



                        if (!string.IsNullOrEmpty(subjectStr))
                            iTemSUBJ = subjectStr.Trim();

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr.Trim();

                        if (!string.IsNullOrEmpty(subjCodeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjCodeStr.Trim();

                        if (!string.IsNullOrEmpty(subjModelStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjModelStr.Trim();

                        if (!string.IsNullOrEmpty(subjBookStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjBookStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + serialStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + makerStr.Trim();

                        if (!string.IsNullOrEmpty(descStr2))
                            iTemSUBJ = iTemSUBJ + Environment.NewLine + descStr2.Trim();


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
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

                        descStr1 = string.Empty;
                        descStr2 = string.Empty;
                        descStr3 = string.Empty;
                    }
                }
            }
        }
    }
}

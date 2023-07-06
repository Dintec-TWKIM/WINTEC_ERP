using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Intergis
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



        public Intergis(string fileName)
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
            int _itemQt = -1;
            int _itemUnit = -1;

            bool shipCheck = false;
            bool machineryCheck = false;
            bool docCheck = false;
            bool makerCheck = false;
            bool typeCheck = false;
            bool serialCheck = false;

            string subjStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;



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

                    if (firstColStr.StartsWith("Ship Name"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Ship Name"))
                            {
                                shipCheck = true;
                                machineryCheck = false;
                            }
                            else if (dt.Rows[i][c].ToString().StartsWith("Machinery"))
                            {
                                machineryCheck = true;
                                shipCheck = false;
                            }


                            if (shipCheck)
                                vessel = vessel.Trim() + dt.Rows[i][c].ToString().Trim();
                            else if (machineryCheck)
                            {
                                if (dt.Rows[i][c].ToString().Contains("PIC") || dt.Rows[i][c].ToString().Contains("1/E"))
                                    break;
                                else
                                    subjStr = subjStr.Trim() + dt.Rows[i][c].ToString().Trim();
                            }
                        }

                        vessel = vessel.Replace("Ship Name", "").Trim();
                        subjStr = subjStr.Replace("Machinery", "").Trim();
                    }
                    else if (firstColStr.StartsWith("Doc. No"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Doc. No."))
                            {
                                docCheck = true;
                                makerCheck = false;
                                typeCheck = false;
                                serialCheck = false;
                            }
                            else if (dt.Rows[i][c].ToString().StartsWith("Maker"))
                            {
                                docCheck = false;
                                makerCheck = true;
                                typeCheck = false;
                                serialCheck = false;
                            }
                            else if (dt.Rows[i][c].ToString().StartsWith("Type"))
                            {
                                docCheck = false;
                                makerCheck = false;
                                typeCheck = true;
                                serialCheck = false;
                            }
                            else if (dt.Rows[i][c].ToString().StartsWith("Serial No."))
                            {
                                docCheck = false;
                                makerCheck = false;
                                typeCheck = false;
                                serialCheck = true;
                            }


                            if (docCheck)
                            {
                                reference = reference.Trim() + dt.Rows[i][c].ToString().Trim();
                            }
                            else if (makerCheck)
                            {
                                if (dt.Rows[i][c].ToString().Contains("C/E"))
                                    break;
                                else
                                    makerStr = makerStr.Trim() + dt.Rows[i][c].ToString().Trim();
                            }
                            else if (typeCheck)
                                
                                    typeStr = typeStr.Trim() + dt.Rows[i][c].ToString().Trim();
                            else if (serialCheck)
                                serialStr = serialStr.Trim() + dt.Rows[i][c].ToString().Trim();
                                

                        }

                        reference = reference.Replace("Doc. No.", "").Trim();
                        makerStr = makerStr.Replace("Maker", "").Trim();
                        

                    }
                    else if (firstColStr.StartsWith("Date Submitted"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Type"))
                            {
                                docCheck = false;
                                makerCheck = false;
                                typeCheck = true;
                                serialCheck = false;
                            }

                            if (typeCheck)
                            {
                                if (dt.Rows[i][c].ToString().Contains("MASTER"))
                                    break;
                                else
                                    typeStr = typeStr.Trim() + dt.Rows[i][c].ToString().Trim();
                            }
                        }
                        typeStr = typeStr.Replace("Type", "").Trim();
                    }
                    else if (firstColStr.StartsWith("Date Required"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Serial No"))
                            {
                                docCheck = false;
                                makerCheck = false;
                                typeCheck = false;
                                serialCheck = true;
                            }

                            if (serialCheck)
                                serialStr = serialStr.Trim() + dt.Rows[i][c].ToString().Trim();
                        }

                        serialStr = serialStr.Replace("Serial No.", "").Trim();
                    }
                    else if (firstColStr.StartsWith("No"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Part No.")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Contains("Specification")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Contains("Req")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Unit")) _itemUnit = c;
                        }
                    }
                    else if (!_itemQt.Equals(-1))
                    {
                        if (GetTo.IsInt(dt.Rows[i][_itemQt].ToString()))
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                            if (!_itemUnit.Equals(-1))
                                iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                            if (!_itemCode.Equals(-1))
                                iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                            if (!_itemDesc.Equals(-1))
                            {
                                //수량은 안빠지니까, 수량으로 체크
                                if (string.IsNullOrEmpty(dt.Rows[i-1][_itemQt].ToString()))
                                    iTemDESC = iTemDESC.Trim() + dt.Rows[i - 1][_itemDesc].ToString();

                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][_itemDesc].ToString().Trim();

                                if (string.IsNullOrEmpty(dt.Rows[i + 1][_itemQt].ToString()))
                                    iTemDESC = iTemDESC.Trim() +" " + dt.Rows[i + 1][_itemDesc].ToString().Trim();
                            }


                            iTemDESC = iTemDESC.Trim();


                            if (!string.IsNullOrEmpty(subjStr))
                                iTemSUBJ = subjStr.Trim();

                            if (!string.IsNullOrEmpty(makerStr))
                                iTemSUBJ = iTemSUBJ + Environment.NewLine +"MAKER: " + makerStr.Trim();

                            if (!string.IsNullOrEmpty(typeStr))
                                iTemSUBJ = iTemSUBJ + Environment.NewLine + "TYPE: " + typeStr.Trim();

                            if (!string.IsNullOrEmpty(serialStr))
                                iTemSUBJ = iTemSUBJ + Environment.NewLine +"S/NO: " + serialStr.Trim();

                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
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
}

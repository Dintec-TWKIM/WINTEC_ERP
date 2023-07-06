using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Transocean
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



        public Transocean(string fileName)
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
            string iTemUniq = string.Empty;

            string[] valueBox = { };

            string valueStr = string.Empty;

            bool itemCodeStart = false;

            string lineNo = string.Empty;

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

                    if (firstColString.StartsWith("Appendix A - Line Specifications"))
                        itemCodeStart = true;

                    if (firstColString.Equals("Event Description"))
                    {
                        string[] vesselRef = dt.Rows[i + 1][0].ToString().Replace("(00)", "").Split('_');
                        vessel = vesselRef[0].ToString().Trim();
                        reference = vesselRef[1].ToString().Trim();
                    }

                    if (!itemCodeStart)
                    {

                        if (firstColString.Equals("Line:"))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                valueStr = valueStr + "?" + dt.Rows[i][c].ToString().Trim();
                            }

                            valueBox = valueStr.Split('?');

                            for (int c = 0; c < valueBox.Length; c++)
                            {
                                iTemNo = valueBox[1].ToString().Trim();

                                if (valueBox[c].ToString().Contains("Item ID:"))
                                    iTemUniq = valueBox[c + 1].ToString().Trim();
                                else if (valueBox[c].ToString().Contains("UOM:"))
                                    iTemUnit = valueBox[c + 1].ToString().Trim();
                                else if (valueBox[c].ToString().Contains("Line Qty:"))
                                {
                                    iTemQt = valueBox[c + 1].ToString().Trim();

                                    if (string.IsNullOrEmpty(iTemQt))
                                        iTemQt = valueBox[c + 2].ToString().Trim();
                                }
                            }
                        }




                        if (firstColString.Equals("Description:"))
                        {
                            iTemDESC = dt.Rows[i][2].ToString().Trim();

                            iTemSUBJ = "MAKER: " + dt.Rows[i + 2][2].ToString().Trim();

                            for (int r = i; r < dt.Rows.Count; r++)
                            {
                                if (dt.Rows[r][2].ToString().Contains("Manufacturer:"))
                                    iTemSUBJ = "MAKER: " + dt.Rows[r][3].ToString().Trim();
                            }

                            int _i = i + 1;

                            while (_i < dt.Rows.Count || !dt.Rows[_i][0].ToString().StartsWith("Line"))
                            {
                                if (dt.Rows[_i][0].ToString().StartsWith("Comments"))
                                {
                                    iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][2].ToString().Replace("- EquipId: ; --", "").Trim();
                                    break;
                                }
                                _i += 1;
                            }
                        }

                        //if (firstColString.Equals("Comments:"))
                        //    iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[i][2].ToString().Replace("- EquipId: ; --", "").Trim();


                        if (GetTo.IsInt(iTemNo) && !string.IsNullOrEmpty(iTemDESC))
                        {
                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;

                            iTemNo = string.Empty;
                            iTemDESC = string.Empty;
                            iTemUnit = string.Empty;
                            iTemQt = string.Empty;
                            iTemUnit = string.Empty;

                            valueStr = string.Empty;
                        }
                    }
                    else
                    {
                        // itemcode가 뒤에 중복되는 항목에서 나옴
                        // itemcode를 먼저 위에서 넣고, 다시 수정하는 방식

                        if (string.IsNullOrEmpty(lineNo))
                        {
                            lineNo = dt.Rows[i][1].ToString().Trim();

                            if (!GetTo.IsInt(lineNo))
                                lineNo = string.Empty;
                        }



                        string secondColStr = dt.Rows[i][7].ToString();

                        if (secondColStr.StartsWith("Mfg Item ID:") && !string.IsNullOrEmpty(lineNo))
                        {
                            iTemCode = dt.Rows[i][8].ToString().Trim();

                            dtItem.Rows[Convert.ToInt16(lineNo) - 1]["ITEM"] = iTemCode;

                            lineNo = string.Empty;
                        }
                    }
                }
            }
        }
    }
}

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Norddeutsche
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



        public Norddeutsche(string fileName)
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

            int _itemQt = -1;
            int _itemDesc = -1;

            string subjStr = string.Empty;

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

                    

                    if (string.IsNullOrEmpty(reference))
                    {
                        string refStr = dt.Rows[i][3].ToString();



                        //if (refStr.Contains("Ordered by"))
                        //{
                        //    string[] vesselValue = dt.Rows[i + 1][3].ToString().Split('"');

                        //    if (vesselValue.Length > 1)
                        //        vessel = vesselValue[1].ToString().Trim();
                        //    else if (vesselValue.Length == 0)
                        //    {

                        //    }
                        //    else
                        //        vessel = vesselValue[0].ToString().Trim();
                        //}

                        if (refStr.Equals("Inquiry No."))
                        {
                            reference = dt.Rows[i][4].ToString().Trim();
                        }
                    }

                    if (firstColStr.ToUpper().Contains("VESSEL'S NAME"))
                        vessel = dt.Rows[i + 1][0].ToString().Trim();
                    else if (firstColStr.Equals("Pos."))
                    {
                        for(int c =0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Quantity Unit") || dt.Rows[i][c].ToString().Equals("Qty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
                        }
                    }else if (firstColStr.Contains("Machinery"))
                    {
                        subjStr = string.Empty;
                        int _i = i+1;
                        while (!dt.Rows[_i][0].ToString().Contains("Pos."))
                        {
                            subjStr = subjStr + Environment.NewLine + dt.Rows[_i][0].ToString().Trim();
                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }
                    }

                    
                    if (GetTo.IsInt(firstColStr))
                    {
                        if (_itemQt != -1)
                        {
                            string[] qtnUnit = dt.Rows[i][_itemQt].ToString().Split(' ');

                            if (qtnUnit.Length > 0)
                            {
                                iTemQt = qtnUnit[0].ToString().Trim();
                                iTemUnit = qtnUnit[1].ToString().Trim();
                            }
                        }

                        if (_itemDesc != -1)
                        {
                            // DESCRIPTION
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                if (dt.Rows[_i][_itemDesc].ToString().ToUpper().Contains("NON ASBESTOS"))
                                    break;
                                else
                                    iTemDESC = iTemDESC + Environment.NewLine + dt.Rows[_i][_itemDesc].ToString().Trim();

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                     


                        iTemSUBJ = subjStr.Trim();


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        //dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] =  "FOR "+ iTemSUBJ;
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

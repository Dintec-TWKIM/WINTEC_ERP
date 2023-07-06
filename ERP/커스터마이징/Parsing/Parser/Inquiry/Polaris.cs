using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Polaris
    {
        string vessel;
        string reference;
        string partner;
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

        public string Partner
        {
            get
            {
                return partner;
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



        public Polaris(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            partner = "";                       // 매출처담당자

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

            int _itemUnitInt = -1;
            int _itemQtInt = -1;

            string[] subjSpl1 = { };
            string subjStr = string.Empty;

            bool subjCheck = false;

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

                    if (firstColStr.ToUpper().Contains("VESSEL"))
                    {
                        vessel = dt.Rows[i][1].ToString().Trim();

                        if (string.IsNullOrEmpty(vessel))
                            vessel = dt.Rows[i][2].ToString().Trim();

                    }
                    else if (firstColStr.ToUpper().Contains("RFQ NO"))
                    {
                        reference = dt.Rows[i][1].ToString().Trim();

                        if(string.IsNullOrEmpty(reference))
                            reference = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.ToUpper().StartsWith("BUYER"))
                    {
                        partner = dt.Rows[i][1].ToString().Trim();

                        if (string.IsNullOrEmpty(partner))
                            partner = dt.Rows[i][2].ToString().Trim();


                        int idx_e = partner.IndexOf("(");

                        if(idx_e != -1)
                            partner = partner.Substring(0, idx_e).Trim();

                    }
                    else if (firstColStr.StartsWith("*"))
                    {
                        // 주제항
                        // maker / type / serial
                        // equipment / component
                        if (!firstColStr.Contains("MAKER") && !firstColStr.Contains("TYPE") && !subjCheck)
                        {
                            if (!firstColStr.Contains("EQUIPMENT") && !firstColStr.Contains("COMPONENT"))
                            {
                                subjSpl1 = firstColStr.Split(new string[] { " / " }, StringSplitOptions.None);
                                subjStr = dt.Rows[i + 1][0].ToString().Replace("*", "").Trim();
                                subjCheck = true;
                            }
                        }
                    }
                    else if (firstColStr.ToUpper().Contains("LINE"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Unit")) _itemUnitInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("RFQ Qty")) _itemQtInt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();
                        iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();

                        // vessel / part no / description
                        // unit
                        // remark
                        string[] valueSpl = dt.Rows[i - 1][_itemUnitInt].ToString().Split(new string[] { " / " }, StringSplitOptions.None);

                        if (valueSpl.Length == 3)
                        {
                            iTemCode = valueSpl[1].ToString().Trim();
                            iTemDESC = valueSpl[2].ToString().Trim();
                        }
                        else if (valueSpl.Length == 4)
                        {
                            iTemCode = valueSpl[1].ToString().Trim() + " / " + valueSpl[2].ToString().Trim();
                            iTemDESC = valueSpl[3].ToString().Trim();
                        }

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr;


                        if (subjSpl1.Length == 3)
                        {
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + subjSpl1[0].ToString().Replace("*", "").Trim();
                            if (!subjSpl1[1].ToString().ToUpper().Equals("NONE"))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + subjSpl1[1].ToString().Trim();
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + subjSpl1[2].ToString().Trim();
                        }
                        else if (subjSpl1.Length == 2)
                        {
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + subjSpl1[0].ToString().Replace("*", "").Trim();
                            if (!subjSpl1[1].ToString().ToUpper().Equals("NONE"))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + subjSpl1[1].ToString().Trim();
                        }


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
                        iTemQt = string.Empty;
                        iTemCode = string.Empty;

                        subjCheck = false;
                    }
                }
            }
        }
    }
}

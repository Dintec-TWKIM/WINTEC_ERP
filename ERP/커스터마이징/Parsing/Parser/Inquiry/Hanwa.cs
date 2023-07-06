using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Hanwa
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



        public Hanwa(string fileName)
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
            dtItem.Columns.Add("UNIQ");          // 선사
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

            string subjStr = string.Empty;
            string subjStr2 = string.Empty;
            string subjTechStr = string.Empty;
            

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
                    string dataFirstValue = dt.Rows[i][0].ToString().Trim();


                    if (dataFirstValue.Contains("Our Vessel"))
                    {
                        vessel = dataFirstValue.Replace("Our Vessel.", "").Replace(":", "").Trim();
                    }
                    else if (dataFirstValue.Contains("INQUIRY"))
                    {
                        reference = dataFirstValue.Replace("INQUIRY No..", "").Replace(":", "").Trim();
                    }
                    else if (dataFirstValue.Contains("System"))
                    {
                        subjStr = dt.Rows[i][0].ToString().Replace("System", "").Replace(".", "").Replace(":", "").Trim();
                    }
                    else if(dataFirstValue.Contains("Subsystem"))
                    {
                        subjStr2 = dt.Rows[i][0].ToString().Replace("Subsystem","").Replace(":","").Replace(".","").Trim();
                    }
                    else if (dataFirstValue.Contains("Techn"))
                    {
                        subjTechStr = dt.Rows[i][0].ToString().Replace("Techn. Data", "").Replace(":", "").Trim();
                    }
                    else if (dataFirstValue.Contains("Items"))
                    {
                        iTemCode = dt.Rows[i + 1][0].ToString().Trim();
                        iTemQt = dt.Rows[i + 1][1].ToString().Trim();
                        iTemUnit = dt.Rows[i + 1][2].ToString().Trim();

                        iTemDESC = dt.Rows[i + 2][0].ToString().Trim() + " " + dt.Rows[i + 3][0].ToString().Trim();
                        iTemSUBJ = subjStr + Environment.NewLine + subjStr2;

                        if (!string.IsNullOrEmpty(subjTechStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TECHN. DATA: " + subjTechStr;
                    }
                    else if (dataFirstValue.Contains("-----------------------------------------:---------:---------------------------"))
                    {
                        iTemDESC = iTemDESC.Replace("-----------------------------------------:---------:---------------------------", "");

                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemQt = string.Empty;
                        
                        if (i + 2 < dt.Rows.Count)
                        {
                            iTemCode = dt.Rows[i + 1][0].ToString().Trim();
                            iTemQt = dt.Rows[i + 1][1].ToString().Trim();
                            iTemUnit = dt.Rows[i + 1][2].ToString().Trim();

                            iTemDESC = dt.Rows[i + 2][0].ToString().Trim() + " " + dt.Rows[i + 3][0].ToString().Trim();

                            //iTemSUBJ = subjStr.Trim();
                            //if(string.IsNullOrEmpty(subjStr2))
                            //    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr2.Trim();

                            iTemSUBJ = subjStr + Environment.NewLine + subjStr2;
                        }
                    }
                }
            }
        }
    }
}

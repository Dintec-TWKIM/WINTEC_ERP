using Aspose.Email.Outlook;
using Dintec;
using Dintec.Parser;

using System;
using System.Data;
using System.Net;

namespace Parsing
{
    class Wisdom
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



        public Wisdom(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            partner = "";                       // 매입처 담당자

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

            string urlStr = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemUnit = -1;
            int _itemQt = -1;
            int _itemRmk = -1;

            string makerStr = string.Empty;
            string subjStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string dwgStr = string.Empty;


            MapiMessage msg = MapiMessage.FromFile(fileName);

            string mailBodyStr = msg.Body;


            // IMO
            int idx_lts = mailBodyStr.IndexOf("Spare Parts Pdf File\r\n<");
            int idx_lte = mailBodyStr.IndexOf("Wisdom Spare Parts Inquiry Website System");

            if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
            {
                urlStr = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Spare Parts Pdf File", "").Replace("<", "").Replace(">", "").Replace("\r\n", "").Trim(); ;
            }

            string url2 = urlStr;

            string filePathStr = @"c:\" + "\\" + "Wisdom.pdf";

            using (WebClient client = new WebClient())
            {
                //client.DownloadFile(url2, @"c:\" + "\\" + "Wisdom.pdf");
                client.DownloadFile(url2, filePathStr);
            }

            fileName = @"c:\" + "\\" + "Wisdom.pdf";

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

                    if (string.IsNullOrEmpty(vessel))
                        vessel = dt.Rows[1][3].ToString().Trim() + dt.Rows[1][2].ToString().Trim();

                    if (string.IsNullOrEmpty(reference))
                        reference = dt.Rows[2][3].ToString().Trim() + dt.Rows[2][2].ToString().Trim();

                    if (string.IsNullOrEmpty(subjStr))
                        subjStr = dt.Rows[3][7].ToString().Trim();

                    if (string.IsNullOrEmpty(typeStr))
                        typeStr = dt.Rows[4][7].ToString().Trim();

                    if (string.IsNullOrEmpty(serialStr))
                        serialStr = dt.Rows[6][7].ToString().Trim();

                    if(string.IsNullOrEmpty(dwgStr))
                        dwgStr = dt.Rows[7][7].ToString().Trim();


                    if (string.IsNullOrEmpty(makerStr))
                    {
                        if (!string.IsNullOrEmpty(firstColStr.Trim()))
                            makerStr = firstColStr.Trim();
                    }

                    if (GetTo.IsInt(firstColStr))
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


                        if (rowValueSpl[7] != null && rowValueSpl[8] == null)
                        {
                            _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                            _itemRmk = Convert.ToInt16(rowValueSpl[7].ToString());
                        }
                        else if (rowValueSpl[6] != null && rowValueSpl[7] == null)
                        {
                            _itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDesc = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                            _itemRmk = -1;
                        }

                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();
                        iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (!_itemRmk.Equals(-1))
                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[i][_itemRmk].ToString().Trim();



                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + "TYPE: " + typeStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + "S/NO: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(dwgStr))
                            iTemSUBJ = iTemSUBJ.Trim() + "DWG NO: " + dwgStr.Trim();


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
                    }
                }
            }
        }
    }
}

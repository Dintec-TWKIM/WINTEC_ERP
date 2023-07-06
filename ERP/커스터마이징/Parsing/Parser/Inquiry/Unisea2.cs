using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Unisea2
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



        public Unisea2(string fileName)
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

            string subjStr = string.Empty;
            string subjMaker = string.Empty;
            string subjModel = string.Empty;
            string subjSerial = string.Empty;

            int _itemCode = -1;
            int _itemDesc = -1;
            int _itemDwg = -1;
            int _itemMaker = -1;
            int _itemQt = -1;

            string itemDWG = string.Empty;
            string itemMaker = string.Empty;

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

                    if (firstColStr.StartsWith("Vessel"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                               vessel = vessel.Trim() + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Enquiry No"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                               reference = reference.Trim() + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.Equals("Component"))
                    {
                        subjStr = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                                subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.Equals("Maker"))
                    {
                        subjMaker = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            subjMaker = subjMaker.Trim()  + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.Equals("Model/Type"))
                    {
                        subjModel = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            subjModel = subjModel.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.Equals("Serial No"))
                    {
                        subjSerial = string.Empty;
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            subjSerial = subjSerial.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }
                    }
                    else if (firstColStr.Equals("No"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Part No")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("DWG No")) _itemDwg = c;
                            else if (dt.Rows[i][c].ToString().Equals("Maker")) _itemMaker = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Qty")) _itemQt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {

                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (!_itemQt.Equals(-1))
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                            if (string.IsNullOrEmpty(iTemQt))
                                iTemQt = dt.Rows[i][_itemQt + 1].ToString().Trim();
                        }
                       
                        if (!_itemDwg.Equals(-1))
                            itemDWG = dt.Rows[i][_itemDwg].ToString().Trim();

                        if (!_itemMaker.Equals(-1))
                        {
                            for (int c = _itemMaker; c < _itemQt; c++ )
                                itemMaker = itemMaker.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        if (!_itemDesc.Equals(-1))
                        {
                            for (int c = _itemDesc; c < _itemCode; c++)
                            {
                                iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                            }

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                for (int c = _itemDesc; c < _itemDwg; c++)
                                {
                                    iTemDESC = iTemDESC + " " + dt.Rows[_i][c].ToString().Trim();
                                }

                                _i += 1;
                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        if (!string.IsNullOrEmpty(itemMaker.Trim()))
                            iTemDESC = iTemDESC.Trim() + Environment.NewLine + "MAKER: " + itemMaker.Trim();


                        if (!string.IsNullOrEmpty(subjStr.Trim()))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(subjMaker.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + subjMaker.Trim();

                        if (!string.IsNullOrEmpty(subjModel.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL/TYPE: " + subjModel.Trim();

                        if (!string.IsNullOrEmpty(subjSerial.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/N: " + subjSerial.Trim();

                        if (!string.IsNullOrEmpty(itemDWG.Trim()))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG NO." + itemDWG.Trim();



                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        //dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = "PCS";
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;
                        itemMaker = string.Empty;
                    }
                }
            }
        }
    }
}

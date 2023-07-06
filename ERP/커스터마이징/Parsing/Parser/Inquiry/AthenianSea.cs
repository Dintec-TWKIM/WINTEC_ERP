using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class AthenianSea
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



        public AthenianSea(string fileName)
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
            string iTemDwg = string.Empty;

            string subjStr = string.Empty;
            string makerStr = string.Empty;
            string modelStr = string.Empty;
            string serialStr = string.Empty;

            int _itemDesc = -1;
            int _itemCode = -1;
            int _itemDwg = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemSpec = -1;

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

                    if (firstColStr.StartsWith("Enquiry No"))
                    {
                        reference = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Vessel"))
                    {
                        string[] vesselSpl = dt.Rows[i][1].ToString().Split('-');

                        if (vesselSpl.Length == 2)
                            vessel = vesselSpl[1].ToString().Trim();
                        else
                            vessel = vesselSpl[0].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Component"))
                    {
                        subjStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Maker"))
                    {
                        makerStr = dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Model/Type"))
                    {
                        modelStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Serial No"))
                    {
                        serialStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("No"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Part No")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("DWG No")) _itemDwg = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Supply Specs")) _itemSpec = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Qty Req")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Unit")) _itemUnit = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        iTemNo = firstColStr;

                        if (!_itemDesc.Equals(-1))
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (!_itemUnit.Equals(-1))
                        {
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();


                            if (iTemUnit.Contains("("))
                            {
                                int idx_s = iTemUnit.IndexOf("(");

                                iTemUnit = iTemUnit.Substring(0, idx_s);
                            }
                        }

                        if (!_itemSpec.Equals(-1))
                            iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][_itemSpec].ToString().Trim();

                        


                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if(!string.IsNullOrEmpty(modelStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "Model/Type: " + modelStr.Trim();

                        if(!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();

                        if (!_itemDwg.Equals(-1))
                            iTemDwg = dt.Rows[i][_itemDwg].ToString().Trim();

                        if (!string.IsNullOrEmpty(iTemDwg))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG NO: " + iTemDwg.Trim();


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
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
                        iTemDwg = string.Empty;
                    }
                }
            }
        }
    }
}

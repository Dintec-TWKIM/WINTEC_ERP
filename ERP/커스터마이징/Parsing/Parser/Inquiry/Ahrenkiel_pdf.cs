using Dintec;
using Dintec.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parsing.Parser.Inquiry
{
	class Ahrenkiel_pdf
	{
        string vessel;
        string reference;
        string imoNumber;
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

        public string ImoNumber
        {
            get
            {
                return imoNumber;
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



        public Ahrenkiel_pdf(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            imoNumber = "";

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
            int _itemCode = -1;
            int _itemQt = -1;

            string subjStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;


            string descPos = string.Empty;
            string descPart = string.Empty;
            string descDwg = string.Empty;

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
                    string firstColStr = dt.Rows[i][0].ToString().ToLower();

                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Reference No"))
                                reference = dt.Rows[i][c + 1].ToString().Trim();
                        }
                    }

                    if (firstColStr.StartsWith("name"))
                    {
                        vessel = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("imo"))
                    {
                        imoNumber = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("product"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                        }

                        int _i = i + 1;

                        while (!dt.Rows[_i][0].ToString().ToLower().Contains("request terms"))
                        {
                            if (dt.Rows[_i][0].ToString().ToLower().Contains("delivery term")) break;
                            else if (dt.Rows[_i][0].ToString().ToLower().Contains("request terms")) break;
                            else if (dt.Rows[_i][0].ToString().ToLower().StartsWith("delivery")) break;
                            else if (dt.Rows[_i][0].ToString().ToLower().Equals("pos")) break;

                            subjStr = subjStr + Environment.NewLine;


                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (subjStr.EndsWith(Environment.NewLine))
                                    subjStr = subjStr + dt.Rows[_i][c].ToString().Trim();
                                else
                                    subjStr = subjStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                            }

                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;
                        }
                    }
                    else if (firstColStr.StartsWith("pos"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Item No") || dt.Rows[i][c].ToString().StartsWith("Pos.") || dt.Rows[i][c].ToString().StartsWith("Part No.") || dt.Rows[i][c].ToString().StartsWith("Code No")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("Quantity")) _itemQt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (!_itemQt.Equals(-1))
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                            iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Replace(".", "").Trim();

                            if (string.IsNullOrEmpty(iTemQt) && _itemQt + 2 < dt.Columns.Count)
                            {
                                iTemUnit = dt.Rows[i][_itemQt + 2].ToString().Replace(".", "").Trim();
                                iTemQt = dt.Rows[i][_itemQt + 1].ToString().Trim();
                            }
                        }


                        if (!_itemCode.Equals(-1))
                        {
                            descPos = dt.Rows[i][_itemCode].ToString().Trim();

                            if (i + 2 >= dt.Rows.Count)
                                break;

                            if (string.IsNullOrEmpty(dt.Rows[i + 2][0].ToString()))
                            {
                                descDwg = dt.Rows[i + 2][_itemCode].ToString().Trim();
                            }
                        }

                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) || !GetTo.IsInt(dt.Rows[_i][0].ToString()))
                            {
                                if (dt.Rows[_i][0].ToString().Contains("458_-1/bd5697b2"))
                                    break;

                                for (int c = 0; c <= _itemDesc; c++)
                                {
                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                }

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }

                        if (!string.IsNullOrEmpty(descPos))
                            iTemCode = descPos.Trim();

                        if (iTemCode.EndsWith("-"))
                            iTemCode = iTemCode.Substring(0, iTemCode.Length - 2).Trim();

                        if (!string.IsNullOrEmpty(descPos))
                            iTemDESC = iTemDESC.Trim() + ", POS." + descPos.Trim();

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(descDwg))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG NO. " + descDwg.Trim();


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;

                        descPart = string.Empty;
                    }
                }
            }
        }
    }
}

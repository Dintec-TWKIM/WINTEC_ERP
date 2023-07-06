using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class BW
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

        public string SUBJ = string.Empty;
        public string ITEM = string.Empty;
        public string DESC = string.Empty;
        public string UNIT = string.Empty;
        public string QT = string.Empty;
        public string ITEMDESC = string.Empty;
        public string ITEMPART = string.Empty;

        #endregion ==================================================================================================== Constructor



        public BW(string fileName)
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

            string descString = string.Empty;

            int _itemdesc = 0;
            int _itemqt = 0;
            int _itemunit = 0;

            string componentString = string.Empty;
            string codeString = string.Empty;
            string makerString = string.Empty;
            string typeString = string.Empty;
            string numberString = string.Empty;
            string systemString = string.Empty;


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


            foreach (DataTable dt in ds.Tables)
            {
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    string firstColString = dt.Rows[i][0].ToString();
                    string secondColString = dt.Rows[i][1].ToString();

                    if (firstColString.Contains("VESSEL"))
                    {
                        vessel = dt.Rows[i][0].ToString().Replace("VESSEL", "").Replace(":", "").Trim();
                    }
                    else if (secondColString.Contains("Our Ref No"))
                    {
                        reference = dt.Rows[i][1].ToString().Replace("Our Ref No", "").Replace(":", "").Trim();
                    }
                    else if (firstColString.Contains("Component Name"))
                    {
                        componentString = firstColString.Replace("Component Name", "").Replace(":", "").Trim();

                        if (string.IsNullOrEmpty(componentString))
                            componentString = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColString.Contains("Component Number"))
                    {
                        numberString = firstColString.Replace("Component Number", "").Replace(":", "").Trim();
                    }
                    else if (firstColString.Contains("Component Type"))
                    {
                        typeString = firstColString.Replace("Component Type", "").Replace(":", "").Trim();

                        if (string.IsNullOrEmpty(typeString))
                            typeString = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColString.Contains("Component Maker Name"))
                    {
                        makerString = firstColString.Replace("Component Maker Name", "").Replace(":", "").Trim();

                        if (string.IsNullOrEmpty(makerString))
                            makerString = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColString.Contains("System no:"))
                    {
                        systemString = firstColString.Replace("System no", "").Replace(":", "").Trim();

                        if (string.IsNullOrEmpty(systemString))
                            systemString = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColString.Equals("Line"))
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].ToString().Contains("UOM")) _itemunit = j;                        //단위
                            else if (dt.Rows[i][j].ToString().Contains("Qty")) _itemqt = j;                     //수량
                            else if (dt.Rows[i][j].ToString().Contains("Description")) _itemdesc = j;           //제목
                        }
                    }
                    // #################### ITEM START ####################
                    else if (GetTo.IsInt(firstColString))
                    {
                        if (!string.IsNullOrEmpty(componentString))
                            iTemSUBJ = componentString;

                        if (!string.IsNullOrEmpty(numberString))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "NUMBER: " + numberString;

                        if (!string.IsNullOrEmpty(typeString))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeString;

                        if (!string.IsNullOrEmpty(makerString))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerString;

                        if (!string.IsNullOrEmpty(systemString))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "SYSTEM NO: " + systemString;

                        iTemSUBJ = iTemSUBJ.Trim();

                        iTemNo = firstColString;
                        iTemQt = dt.Rows[i][_itemqt].ToString().Trim();
                        iTemUnit = dt.Rows[i][_itemunit].ToString().Trim();

                        int _i = i + 1;
                        descString = dt.Rows[i][_itemdesc].ToString().Trim();
                        while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && _i < dt.Rows.Count)
                        {
                            descString = descString.Trim() + " " + dt.Rows[_i][_itemdesc].ToString().Trim();

                            _i += 1;

                            if (!(_i < dt.Rows.Count - 1))
                            {
                                break;
                            }
                        }

                        descString = descString.Replace("< BW Anti-Bribery and Anti-Corruption Policy to apply >", "");


                        string[] itemcode = descString.Split(';');

                        for (int j = 0; j < itemcode.Length; j++)
                        {
                            if (!itemcode[j].ToString().Contains("MKR REF"))
                            {
                                if (!itemcode[j].ToString().Contains("MKR"))
                                    iTemDESC = iTemDESC.Trim() + itemcode[j].ToString().Trim();
                            }
                            else
                            {
                                iTemCode = itemcode[j].ToString().Replace("MKR REF", "").Replace(":", "").Replace("No.", "").Trim();
                            }
                        }


                        if (!iTemDESC.Contains("Packing/Handing/Freight"))
                        {
                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC.Replace(";", "").Replace("< BW ANTI-BRIBERY AND ANTI-CORRUPTION POLICY TO APPLY >", "");
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                            if (!string.IsNullOrEmpty(iTemSUBJ))
                                dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ.Replace("[MKR]", "");
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                        }

                        iTemSUBJ = string.Empty;
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

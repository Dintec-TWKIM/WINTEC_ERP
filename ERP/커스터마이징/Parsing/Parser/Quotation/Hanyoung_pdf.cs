using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Hanyoung_pdf
    {
        DataTable dtIteml;

        string lt;
        string rmk;
        string reference;
        string currency;

        string fileName;
        UnitConverter uc;


        #region ==================================================================================================== Property

        public DataTable ItemL
        {
            get
            {
                return dtIteml;
            }
        }

        public string Reference
        {
            get
            {
                return reference;
            }
        }

        public string Rmk
        {
            get
            {
                return rmk;
            }
        }

        public string Lt
        {
            get
            {
                return lt;
            }
        }

        public string Currency
        {
            get
            {
                return currency;
            }
        }

        #endregion ==================================================================================================== Constructor



        public Hanyoung_pdf(string fileName)
        {
            lt = string.Empty;
            rmk = string.Empty;
            reference = string.Empty;
            currency = string.Empty;

            dtIteml = new DataTable();
            dtIteml.Columns.Add("NO");          // 순번
            dtIteml.Columns.Add("DESC");        // 품목명
            dtIteml.Columns.Add("ITEM");        // 품목코드
            dtIteml.Columns.Add("UNIT");        // 단위
            dtIteml.Columns.Add("QT");          // 수량
            dtIteml.Columns.Add("UNIQ");          // 고유코드
            dtIteml.Columns.Add("UM");          // 단가
            dtIteml.Columns.Add("AM");          // 금액
            dtIteml.Columns.Add("LT");          // 납기
            dtIteml.Columns.Add("RMK");         // 비고

            this.fileName = fileName;
            this.uc = new UnitConverter();
        }

        public void Parse()
        {
            string iTemNo = string.Empty;
            string iTemUm = string.Empty;
            string iTemDC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;
            string iTemRMK = string.Empty;
            string iTemAm = string.Empty;
            string iTemDesc = string.Empty;
            string iTemTotal = string.Empty;
            string iTemCode = string.Empty;
            string iTemType = string.Empty;
            string iTemUniq = string.Empty;
            string iTemLt = string.Empty;

            string iTemRef = string.Empty;
            string itemRMKH = string.Empty;
            string leadTimeStr = string.Empty;

            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemPrice = -1;
            int _itemValue = -1;
            int _itemCode = -1;
            int _itemLt = -1;

            string typeStr = string.Empty;
            string partStr = string.Empty;

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

                    if (firstColStr.Contains("일련번호"))
                        reference = dt.Rows[i][1].ToString().Trim();
                    else if (firstColStr.Contains("인도시기"))
                    {
                        lt = dt.Rows[i][1].ToString().Trim();
                        if (lt.Contains("발주") && lt.Contains("일"))
                        {
                            lt = lt.Replace("발주", "").Replace("일", "").Replace("후","").Trim();
                        }
                    }
                    else if (firstColStr.Contains("순번"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("품목코드")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Contains("품목명")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Contains("수량") || dt.Rows[i-1][c].ToString().Contains("수량")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("단가")) _itemPrice = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("공급가액")) _itemValue = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (_itemDesc != -1)
                            iTemDesc = dt.Rows[i][_itemDesc].ToString().Trim();

                        if (_itemCode != -1)
						{
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();
                            // 1ST 2ND 3RD
                            if (iTemCode.Contains("1ST") || iTemCode.Contains("2ND") || iTemCode.Contains("3RD"))
							{

                                int idx_s = iTemCode.ToUpper().IndexOf("1ST");

                                if(idx_s != -1)
								{
                                    iTemDesc = iTemCode.Substring(idx_s, iTemCode.Length - idx_s).Trim();
                                    iTemCode = iTemCode.Replace(iTemDesc, "").Trim();
								}

                                idx_s = iTemCode.ToUpper().IndexOf("2ND");

                                if (idx_s != -1)
                                {
                                    iTemDesc = iTemCode.Substring(idx_s, iTemCode.Length - idx_s).Trim();
                                    iTemCode = iTemCode.Replace(iTemDesc, "").Trim();
                                }

                                idx_s = iTemCode.ToUpper().IndexOf("3RD");

                                if (idx_s != -1)
                                {
                                    iTemDesc = iTemCode.Substring(idx_s, iTemCode.Length - idx_s).Trim();
                                    iTemCode = iTemCode.Replace(iTemDesc, "").Trim();
                                }
                            }
                        }
                            


                        if (_itemPrice != -1)
                            iTemUm = dt.Rows[i][_itemPrice].ToString().Trim();

                        if (_itemValue != -1)
                        {
                            iTemAm = dt.Rows[i][_itemValue].ToString().Trim();


                            if(iTemAm.Contains(" "))
							{
                                string[] itemamSpl = iTemAm.Split(' ');

                                if(itemamSpl.Length == 2)
								{
                                    iTemAm = itemamSpl[0].ToStr().Trim();
								}
							}
                        }


                        if (_itemQt != -1)
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Replace("PCS", "").Replace("EA", "").Replace("PCE", "").Replace("SET","").Trim();
                        }

                        if (string.IsNullOrEmpty(iTemQt))
                        {
                            iTemQt = "0";
                        }


                        iTemUm = iTemUm.Replace(",", "").Trim();
                        iTemAm = iTemAm.Replace(",", "").Trim();


                        //ITEM ADD START
                        dtIteml.Rows.Add();
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["NO"] = firstColStr;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["DESC"] = iTemDesc;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["ITEM"] = typeStr;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["QT"] = iTemQt;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["UNIQ"] = iTemCode;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["UM"] = iTemUm;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["AM"] = iTemAm;
                        if (GetTo.IsInt(lt))
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["LT"] = lt;
                        else
                            dtIteml.Rows[dtIteml.Rows.Count - 1]["LT"] = "0";

                        dtIteml.Rows[dtIteml.Rows.Count - 1]["RMK"] = iTemRMK;

                        iTemDC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemRMK = string.Empty;
                        iTemQt = string.Empty;
                        iTemUm = string.Empty;
                        iTemAm = string.Empty;
                        iTemCode = string.Empty;
                        iTemDesc = string.Empty;
                        iTemLt = string.Empty;
                    }

                }
            }
        }
    }
}

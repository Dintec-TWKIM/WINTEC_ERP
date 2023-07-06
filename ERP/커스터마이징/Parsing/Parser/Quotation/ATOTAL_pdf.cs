using Dintec;
using Dintec.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parsing
{
	class ATOTAL_pdf
	{
        DataTable dtIteml;

        string lt;
        string rmk;
        string reference;

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

        #endregion ==================================================================================================== Constructor



        public ATOTAL_pdf(string fileName)
        {
            lt = string.Empty;
            rmk = string.Empty;
            reference = string.Empty;

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


            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemPrice = -1;
            int _itemValue = -1;
            int _itemCode = -1;



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
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToUpper().Contains("OUR REFF"))
                            {
                                for (int _c = c; _c < dt.Columns.Count; _c++)
                                    reference = reference + dt.Rows[i][_c].ToString().Replace("REQNO", "").Replace(":", "").Trim();

                                reference = reference.Replace("OUR", "").Replace("REF.", "").Replace("NO.", "").Replace(":", "").Trim();

                            }
                        }
                    }

                    if(firstColStr.StartsWith("TIME OF DELIVERY"))
					{
                        for(int c = 0; c < dt.Columns.Count; c++)
						{
                            lt = lt + dt.Rows[i][c].ToString().Trim();
						}

                        if(!string.IsNullOrEmpty(lt))
						{
                            lt = lt.Replace("발", "").Replace("주", "").Replace("후", "").Replace("이", "").Replace("내", "").Replace("TIME OF DELIVERY", "").Replace(":","").Trim();
						}

                        if(lt.Contains("~"))
						{
                            string[] ltSpl = lt.Split('~');

                            if(ltSpl.Length == 2)
							{
                                lt = ltSpl[1].ToString().Trim();
							}
						}
					}
                    else if (firstColStr.StartsWith("NO."))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("DESCRIPTION")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("SPECIFICATION")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("QTY")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("UNIT")) _itemPrice = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("AMOUNT")) _itemValue = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr.Replace(".", "")))
                    {
                        iTemUm = "0";
                        iTemAm = "0";


                        if (!_itemQt.Equals(-1))
                        {
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                            string[] qtspl = iTemQt.Split(' ');

                            if(qtspl.Length == 2)
							{
                                iTemQt = qtspl[0].ToString();
                                iTemUnit = qtspl[1].ToString();
							}
                               
                        }

                        if (!_itemPrice.Equals(-1))
                            iTemUm = dt.Rows[i][_itemPrice].ToString().Trim();

                        if (!_itemValue.Equals(-1))
                            iTemAm = dt.Rows[i][_itemValue].ToString().Trim();

                        if (!_itemDesc.Equals(-1))
                            iTemDesc = dt.Rows[i][_itemDesc].ToString().Trim();

                        if (!_itemCode.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                        iTemUm = iTemUm.Replace(",", "").Replace("-","").Trim();
                        iTemAm = iTemAm.Replace(",", "").Replace("-","").Trim();

                        if (string.IsNullOrEmpty(iTemUm))
                            iTemUm = "0";

                        if (string.IsNullOrEmpty(iTemAm))
                            iTemAm = "0";


                        //ITEM ADD START
                        dtIteml.Rows.Add();
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["NO"] = firstColStr.Replace(".", "");
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["DESC"] = iTemDesc;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["ITEM"] = "";
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["QT"] = iTemQt;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["UNIQ"] = iTemCode;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["UM"] = iTemUm;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["AM"] = iTemAm;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["LT"] = "";
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["RMK"] = iTemRMK;

                        iTemDC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemRMK = string.Empty;
                        iTemQt = string.Empty;
                        iTemUm = string.Empty;
                        iTemAm = string.Empty;
                        iTemCode = string.Empty;
                        iTemDesc = string.Empty;
                    }

                }
            }
        }
    }
}

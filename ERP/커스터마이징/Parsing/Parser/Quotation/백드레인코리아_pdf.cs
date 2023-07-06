using Dintec;
using Dintec.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parsing
{
	class 백드레인코리아_pdf
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



        public 백드레인코리아_pdf(string fileName)
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
            dtIteml.Columns.Add("CHARGE");      // 부대비용

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
            string iTemCharge = string.Empty;


            string iTemRef = string.Empty;
            string itemRMKH = string.Empty;
            string leadTimeStr = string.Empty;



            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemPrice = -1;
            int _itemValue = -1;
            int _itemUnit = -1;

            string typeStr = string.Empty;
            string partStr = string.Empty;

            string noStr = string.Empty;


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


                    if (firstColStr.Contains("our ref."))
                    {
                        reference = DateTime.Now.ToString("yyyy.MM.dd") + "(E)";
                        //reference = dt.Rows[i][0].ToString().Trim() + dt.Rows[i][1].ToString().Trim();
                        //reference = reference.Replace("Our Ref.", "").Replace(":", "").Trim();

                    }
                    else if (firstColStr.Contains("납품기한"))
                    {
                        lt = dt.Rows[i][0].ToString().Trim() + dt.Rows[i][1].ToString().Trim();

                        lt = lt.Replace("납품기한", "").Replace("발주후", "").Replace("이내", "").Replace(":","").Trim();

                        if(lt.Contains("주"))
						{
                            lt = lt.Replace("주", "").Trim();

                            if(lt.Contains("~"))
							{
                                string[] ltSpl = lt.Split('~');

                                lt = ltSpl[ltSpl.Length - 1].Trim();
							}

                            lt = Convert.ToString(Convert.ToInt32(lt) * 7);
                        }
                        else if (lt.Contains("일"))
						{
                            lt = lt.Replace("일", "").Trim();

                            if (lt.Contains("~"))
                            {
                                string[] ltSpl = lt.Split('~');

                                lt = ltSpl[ltSpl.Length - 1].Trim();
                            }
                        }

                        iTemLt = lt;
                    }
                    else if (firstColStr.Equals("no."))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("품명")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("수량")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("단가")) _itemPrice = c;
                            else if (dt.Rows[i][c].ToString().Equals("금액")) _itemValue = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
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


                        if (rowValueSpl[5] != null && rowValueSpl[6] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemPrice = Convert.ToInt16(rowValueSpl[4].ToString());
                            _itemValue = Convert.ToInt16(rowValueSpl[5].ToString());

                        }
                        else if (rowValueSpl[4] != null && rowValueSpl[5] == null)
                        {
                            _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemQt = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemPrice = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemValue = Convert.ToInt16(rowValueSpl[4].ToString());
                        }

                        if (!_itemDesc.Equals(-1))
                            iTemDesc = dt.Rows[i][_itemDesc].ToString().Trim();

                        if (iTemDesc.ToLower().Contains("handling") || iTemDesc.ToLower().Contains("handiling"))
                        {

                            //FREIGHT
                            //HANDLING
                            //PACKING
                            //CERTIFICATE

                            iTemDesc = "HANDLING";

                            if (!_itemValue.Equals(-1))
                                iTemAm = dt.Rows[i][_itemValue].ToString().Trim();

                            iTemUm = iTemAm;

                            iTemCharge = "Y";

                            iTemQt = "1";
                        }
                        else
                        {
                            if (!_itemQt.Equals(-1))
                                iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                            if (!_itemPrice.Equals(-1))
                                iTemUm = dt.Rows[i][_itemPrice].ToString().Replace(",","").Trim();

                            if (!_itemValue.Equals(-1))
                                iTemAm = dt.Rows[i][_itemValue].ToString().Replace(",", "").Trim();
                        }

                        //ITEM ADD START
                        dtIteml.Rows.Add();
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["NO"] = firstColStr;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["DESC"] = iTemDesc;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["ITEM"] = "";
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["QT"] = iTemQt;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["UNIQ"] = iTemCode;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["UM"] = iTemUm;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["AM"] = iTemAm;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["LT"] = iTemLt;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["RMK"] = iTemRMK;
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["CHARGE"] = iTemCharge;

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

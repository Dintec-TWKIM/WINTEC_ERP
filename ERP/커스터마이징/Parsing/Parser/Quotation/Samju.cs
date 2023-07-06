﻿using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Samju
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



        public Samju(string fileName)
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
            int _itemCode = -1;

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
                    string firstColStr = dt.Rows[i][0].ToString();

                    if (string.IsNullOrEmpty(reference))
                    {
                        reference = DateTime.Now.ToString("yyyy.MM.dd") + "(E-MAIL)";
                    }

                    if (string.IsNullOrEmpty(lt) && firstColStr.StartsWith("납품조건"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if(dt.Rows[i][c].ToString().Contains("발주"))
                                lt = dt.Rows[i][c].ToString().Replace("발주후","").Replace("이내","").Trim();
                        }

                        if (lt.Contains("주"))
                        {
                            lt = lt.Replace("주", "").Trim();

                            int _lt = Convert.ToInt16(lt) * 7;

                            lt = Convert.ToString(_lt);
                        }
                        
                    }

                   if (firstColStr.Equals("번호"))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("품명")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("수량")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("단위")) _itemUnit = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("단가")) _itemPrice = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("계")) _itemValue = c;
                            else if (dt.Rows[i][c].ToString().StartsWith("규격")) _itemCode = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (!_itemDesc.Equals(-1))
                        {
                            iTemDesc = dt.Rows[i][_itemDesc].ToString().Trim();
                        }

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
                                iTemQt = dt.Rows[i][_itemQt].ToString().Replace(" ","").Trim();

                            if (!_itemUnit.Equals(-1))
                                iTemUnit = dt.Rows[i][_itemUnit].ToString().Replace(" ", "").Trim();
                            else
                                iTemUnit = dt.Rows[i][_itemQt + 1].ToString().Trim();

                            if (!_itemPrice.Equals(-1))
                                iTemUm = dt.Rows[i][_itemPrice].ToString().Replace(" ", "").Replace("₩", "").Replace("\\", "").Trim();

                            if (!_itemValue.Equals(-1))
                                iTemAm = dt.Rows[i][_itemValue].ToString().Replace(" ", "").Replace("₩", "").Replace("\\", "").Trim();

                            if (!_itemCode.Equals(-1))
                                iTemCode = dt.Rows[i][_itemCode].ToString().Replace(" ", "").Trim();
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
                        dtIteml.Rows[dtIteml.Rows.Count - 1]["LT"] = lt;
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

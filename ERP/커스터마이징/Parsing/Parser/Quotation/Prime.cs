using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Prime
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



        #endregion

        #region ==================================================================================================== Constructor

        public Prime(string fileName)
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

        #endregion

        #region ==================================================================================================== Logic

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

            string iTemRef = string.Empty;
            string itemRMKH = string.Empty;
            string leadTimeStr = string.Empty;

            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemPrice = -1;
            int _itemValue = -1;
            int _itemCode = -1;     
            int _itemRmk = -1;

            string typeStr = string.Empty;
            string partStr = string.Empty;

            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColStr = dt.Rows[i][0].ToString();

                if (firstColStr.StartsWith("ATTN"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().StartsWith("OUR REF"))
                            reference = dt.Rows[i][c].ToString().Replace("OUR REF No.","").Replace(":","").Trim();
                    }
                }
                else if (firstColStr.StartsWith("NO"))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Equals("DESCRIPTION")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Equals("Q'TY")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Equals("UNIT")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().Equals("U/PRICE")) _itemPrice = c;
                        else if (dt.Rows[i][c].ToString().Equals("AMOUNT")) _itemValue = c;
                        else if (dt.Rows[i][c].ToString().Equals("CODE")) _itemCode = c;
                        else if (dt.Rows[i][c].ToString().Equals("MAKER")) _itemRmk = c;
                    }
                }
                else if (GetTo.IsInt(firstColStr.Replace(".","")))
                {
                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                    if (!_itemUnit.Equals(-1))
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                    if (!_itemPrice.Equals(-1))
                        iTemUm = dt.Rows[i][_itemPrice].ToString().Trim();

                    if (!_itemDesc.Equals(-1))
                        iTemDesc = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (!_itemCode.Equals(-1))
                        iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                    if (!_itemRmk.Equals(-1))
                        iTemRMK = dt.Rows[i][_itemRmk].ToString().Trim();

                    if (!_itemValue.Equals(-1))
                        iTemAm = dt.Rows[i][_itemValue].ToString().Trim();

                    if (string.IsNullOrEmpty(iTemDesc))
                        break;


                    //ITEM ADD START
                    dtIteml.Rows.Add();
                    dtIteml.Rows[dtIteml.Rows.Count - 1]["NO"] = firstColStr.Replace(".","");
                    dtIteml.Rows[dtIteml.Rows.Count - 1]["DESC"] = iTemDesc;
                    dtIteml.Rows[dtIteml.Rows.Count - 1]["ITEM"] = typeStr;
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

        #endregion
    }
}

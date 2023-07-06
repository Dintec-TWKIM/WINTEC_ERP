using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Exmar
    {
        string contact;
        string reference;
        string vessel;
        string imoNumber;
        DataTable dtItem;

        string fileName;
        UnitConverter uc;

        #region ==================================================================================================== Property

        public string Contact
        {
            get
            {
                return contact;
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

        public string Vessel
        {
            get
            {
                return vessel;
            }
        }

        public DataTable Item
        {
            get
            {
                return dtItem;
            }
        }

        #endregion

        #region ==================================================================================================== Constructor

        public Exmar(string fileName)
        {
            contact = "";
            reference = "";
            vessel = "";
            imoNumber = "";

            dtItem = new DataTable();
            dtItem.Columns.Add("NO");
            dtItem.Columns.Add("SUBJ");
            dtItem.Columns.Add("ITEM");
            dtItem.Columns.Add("DESC");
            dtItem.Columns.Add("UNIT");
            dtItem.Columns.Add("QT");
            dtItem.Columns.Add("UNIQ");         //선사코드

            this.fileName = fileName;
            this.uc = new UnitConverter();
        }

        #endregion

        #region ==================================================================================================== Logic

        public void Parse()
        {
            string iTemDesc = string.Empty;
            string iTemCode = string.Empty;
            string iTemQt = string.Empty;
            string iTemUnit = string.Empty;
            string iTemSUBJ = string.Empty;

            int _endColumnCount = -1;
            int _vesselInt = -1;

            int _itemDesc = -1;
            int _itemPartNum = -1;
            int _itemModel = -1;
            int _itemDrawing = -1;
            int _itemQt = -1;
            int _itemPosition = -1;

            string subjStr = string.Empty;
            string modelStr = string.Empty;
            string dwgStr = string.Empty;


            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴

            // ********** 문서 검색 모드
            // 선명, 문의번호

            // ********** 아이템 추가 모드
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i.Equals(0) || i.Equals(2))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[2][c].ToString().Equals("Buyer")) _vesselInt = c;

                        if (dt.Rows[0][c].ToString().Equals("Requisition Number")) _endColumnCount = c;
                    }
                }


                string firstColString = dt.Rows[i][0].ToString();
                string buyerColString = dt.Rows[i][2].ToString();

                // 필요 없는 공백 ROW가 너무 많음


                if (string.IsNullOrEmpty(contact))
                {
                    for (int c = 1; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("Purchaser:"))
                            contact = dt.Rows[i][c + 1].ToString().Trim();
                    }
                }

                if (buyerColString.Contains("Company Name"))
                {
                    for (int c = _vesselInt + 1; c < _endColumnCount; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("RFQ"))
                            reference = dt.Rows[i][c + 1].ToString().Trim();
                    }
                }

                if (buyerColString.Contains("Vessel"))
                    vessel = dt.Rows[i][3].ToString().Trim();

                if (firstColString.Contains("fields are in grey")) break;
                else if (firstColString.Contains("Component Details"))
                {
                    for (int c = 0; c < _endColumnCount; c++)
                    {
                        subjStr = subjStr + dt.Rows[i][c].ToString().Replace("Component Details:","").Trim();
                    }
                    subjStr = subjStr + Environment.NewLine;

                }
                else if (firstColString.Contains("Requisition Number"))
                {
                    for (int c = 0; c < _endColumnCount; c++)
                    {
                        if (dt.Rows[i][c].ToString().Contains("Part Description")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Contains("Part Number")) _itemPartNum = c;
                        else if (dt.Rows[i][c].ToString().Contains("Model No")) _itemModel = c;
                        else if (dt.Rows[i][c].ToString().Contains("Quantity Required")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Contains("Drawing No")) _itemDrawing = c;
                        else if (dt.Rows[i][c].ToString().Contains("Position No")) _itemPosition = c;
                    }
                }


                if (firstColString.IndexOf("R-") == 0)
                {

                    if (!_itemDesc.Equals(-1))
                        iTemDesc = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (!_itemQt.Equals(-1))
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                    if (!_itemPartNum.Equals(-1))
                    {
                        iTemCode = dt.Rows[i][_itemPartNum].ToString().Trim();

                        if (string.IsNullOrEmpty(iTemCode))
                        {
                            if (!_itemPosition.Equals(-1))
                                iTemCode = dt.Rows[i][_itemPosition].ToString().Trim();
                        }
                        else
                        {
                            if (!_itemPosition.Equals(-1))
                                iTemDesc = iTemDesc + Environment.NewLine + "POS.NO.: " + dt.Rows[i][_itemPosition].ToString().Trim();
                        }
                    }


                    if (!_itemModel.Equals(-1))
                        modelStr = dt.Rows[i][_itemModel].ToString().Trim();

                    if (!_itemDrawing.Equals(-1))
                        dwgStr = dt.Rows[i][_itemDrawing].ToString().Trim();
                           
                    

                    iTemSUBJ = subjStr.Trim();

                    if (iTemCode.Contains("Serial"))
                    {
                        iTemCode = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(modelStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr.Trim();

                    if (!string.IsNullOrEmpty(dwgStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG.NO.: " + dwgStr.Trim();

                    iTemUnit = "PCS";


                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColString;
                    if (!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDesc;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                    if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;

                    iTemDesc = string.Empty;
                    iTemQt = string.Empty;
                    iTemUnit = string.Empty;
                    iTemSUBJ = string.Empty;
                }
            }

        #endregion
        }
    }
}

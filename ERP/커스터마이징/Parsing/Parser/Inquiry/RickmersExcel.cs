using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class RickmersExcel
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

        public RickmersExcel(string fileName)
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
            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            int _descInt = -1;
            int _qtInt = -1;
            int _unitInt = -1;
            int _partnoInt = -1;
            int _drawingInt = -1;
            int _internalInt = -1;

            bool itemStart = false;

            string partnoString = string.Empty;
            string drawingString = string.Empty;
            string internalString = string.Empty;
            string itemnameString = string.Empty;

            string subjString = string.Empty;



            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColString = dt.Rows[i][0].ToString();


                if (firstColString.Equals("With best regards")) break;
                
                // 선명
                if (firstColString.Equals("Vessel"))
                {
                    vessel = dt.Rows[i][1].ToString().Trim();
                }

                // 문의번호
                if (firstColString.Equals("Inquiry:"))
                {
                    string[] referValue = dt.Rows[i][1].ToString().Split('-');

                    for (int c = 0; c < referValue.Length-1; c++)
                    {
                        reference = reference.Trim() + "-" + referValue[c].ToString().Trim();
                    }

                    //첫 값이 -  =>> 제거
                    if (!string.IsNullOrEmpty(reference))
                    {
                        if (reference.Substring(0, 1).Equals("-"))
                            reference = reference.Substring(1, reference.Length - 1);
                    }
                    // 마지막 항목은 subject로 사용
                    subjString = referValue[referValue.Length - 1].ToString().Trim();
                }

                if (firstColString.Equals("Item No."))
                {
                    itemStart = true;

                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Equals("Item Name")) _descInt = c;
                        else if (dt.Rows[i][c].ToString().Equals("Requested Quantity")) _qtInt = c;
                        else if (dt.Rows[i][c].ToString().Equals("Requested Unit")) _unitInt = c;
                        else if (dt.Rows[i][c].ToString().Equals("Part No.")) _partnoInt = c;
                        else if (dt.Rows[i][c].ToString().Equals("Drawing No.")) _drawingInt = c;
                        else if (dt.Rows[i][c].ToString().Equals("Internal No.")) _internalInt = c;
                    }
                }


                if(!string.IsNullOrEmpty(firstColString))
                {
                    if (GetTo.IsInt(firstColString.Substring(0, 1)) && itemStart)
                    {

                        // DESCRIPTION 뒤에 오는 숫자들 제거
                        itemnameString = dt.Rows[i][_descInt].ToString().Trim();
                        if (!string.IsNullOrEmpty(itemnameString))
                        {
                            int positionNum = itemnameString.IndexOf("\r\n");

                            if(!positionNum.Equals(-1))
                                itemnameString = itemnameString.Substring(0, positionNum);
                            
                        }

                        iTemQt = dt.Rows[i][_qtInt].ToString().Trim();
                        iTemUnit = dt.Rows[i][_unitInt].ToString().Trim();

                        drawingString = dt.Rows[i][_drawingInt].ToString().Trim();
                        if (!string.IsNullOrEmpty(drawingString))
                        {
                            string[] drawingValue = drawingString.Split('/');

                            if (drawingValue.Length == 2)
                            {
                                iTemCode = drawingValue[0].ToString().Trim();
                                drawingString = drawingValue[1].ToString().Trim();
                            }
                            else
                            {
                                iTemCode = drawingString;
                            }
                        }


                        partnoString = dt.Rows[i][_partnoInt].ToString().Trim();

                        internalString = dt.Rows[i][_internalInt].ToString().Trim();

                        iTemDESC = itemnameString;
                        if (!string.IsNullOrEmpty(partnoString))
                            iTemDESC = iTemDESC.Trim() + ", " + partnoString;


                        if (!string.IsNullOrEmpty(subjString))
                            iTemSUBJ = subjString;

                        //if (!string.IsNullOrEmpty(drawingString))
                        //    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG: " + drawingString;


                        dtItem.Rows.Add();
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);

                        drawingString = string.Empty;
                        iTemSUBJ = string.Empty;
                        iTemDESC = string.Empty;
                        iTemQt = string.Empty;
                        iTemUnit = string.Empty;

                    }
                }
            }
        }

        #endregion
    }
}

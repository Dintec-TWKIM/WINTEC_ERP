using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Dynacom
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


        #endregion ==================================================================================================== Constructor



        public Dynacom(string fileName)
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
            string itemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string itemQt = string.Empty;

            int _itemCode = -1;
            int _itemDescription = -1;
            int _itemQt = -1;
            int _itemDescription2 = -1;


            string codeString = string.Empty;
            string dwgString = string.Empty;
            string subjStr = string.Empty;


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

            string[] vesselName = { };

            bool itemStart = false;

            foreach (DataTable dt in ds.Tables)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dataValue = dt.Rows[i][0].ToString();

                    // 첫 컬럼 값
                    if (dataValue.Equals("No")) itemStart = true;


                    if (!itemStart)
                    {
                        //선명, 문의번호
                        if (dataValue.Contains("OUR REF??"))
                        {
                            vesselName = dataValue.Split('/');

                            vessel = vesselName[0].ToString().Replace("OUR REF??:", "");
                            vessel = vessel.Replace("M.T.", "").Trim();
                            reference = vesselName[1].ToString();
                            if (vesselName.Length > 2)
                            {
                                reference = reference.Trim() + "/" + vesselName[2].ToString().Trim();

                                reference = vessel.Trim() + "/" + reference.Trim();
                            }

                        }

                        if (dataValue.Equals("Maker/Type/Licensee") || dataValue.Equals("Description"))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                                subjStr = subjStr.Trim() + " " + dt.Rows[i + 1][c].ToString();

                            int _ii = i + 2;
                            while (!dt.Rows[_ii][0].ToString().Equals("No"))
                            {
                                for (int c = 0; c < dt.Columns.Count; c++)
                                    subjStr = subjStr.Trim() + " " + dt.Rows[_ii][c].ToString().Trim();
                                _ii += 1;
                            }
                        }
                    }
                    else
                    {
                        //################# 순번, 주제, 품목코드, 품목명, 단위, 수량
                        string firstColString = dt.Rows[i][0].ToString();

                        if (firstColString.Equals("No"))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString() == "Description") _itemDescription = c;         //품목명
                                else if (dt.Rows[i][c].ToString() == "Part No") _itemCode = c;               //아이템코드
                                else if (dt.Rows[i][c].ToString().Contains("Quantity")) _itemQt = c;         //수량
                                else if (dt.Rows[i][c].ToString() == "Plate/Drg No") _itemDescription2 = c;  //품목명 ROW 길이
                            }
                        }


                        
                        if (GetTo.IsInt(firstColString))
                        {
                            iTemNo = firstColString;

                            if (!_itemDescription.Equals(-1))
                            {
                                iTemDESC = dt.Rows[i][_itemDescription].ToString();
                                int _i3 = i + 1;
                                while (dt.Rows[_i3][0].ToString().Length > 2 || string.IsNullOrEmpty(dt.Rows[_i3][0].ToString()))
                                {
                                    if (_i3 < dt.Rows.Count - 4 || !dt.Rows[_i3][0].ToString().ToUpper().Contains("AWAITING")) //
                                    {
                                        iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i3][_itemDescription - 1].ToString().Trim() + dt.Rows[_i3][_itemDescription].ToString().Trim();
                                        _i3 += 1;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }


                            if (!_itemDescription2.Equals(-1))
                            {
                                if (dt.Rows[i][_itemDescription2] != null)
                                {
                                    int _idrw = i + 1;

                                    if (_itemDescription2 - _itemDescription == 1)
                                    {
                                        dwgString = dt.Rows[i][_itemDescription2].ToString().Trim();

                                        while (string.IsNullOrEmpty(dt.Rows[_idrw][0].ToString()))
                                        {
                                            dwgString = dwgString + " " + dt.Rows[_idrw][_itemDescription2].ToString().Trim();
                                            _idrw += 1;
                                        }

                                        if (dwgString.Equals("null") || dwgString.Equals("NA") || dwgString.Equals("na") || dwgString.Equals("NULL"))
                                            dwgString = string.Empty;
                                    }
                                    else if (_itemDescription2 - _itemDescription == 2)
                                    {
                                        dwgString = dt.Rows[i][_itemDescription2 - 1].ToString().Trim() + dt.Rows[i][_itemDescription2].ToString().Trim();

                                        while (string.IsNullOrEmpty(dt.Rows[_idrw][0].ToString()))
                                        {
                                            dwgString = dwgString + " " + dt.Rows[_idrw][_itemDescription2 - 1].ToString().Trim() + dt.Rows[_idrw][_itemDescription2].ToString().Trim();
                                            _idrw += 1;
                                        }

                                        if (dwgString.Equals("null") || dwgString.Equals("NA") || dwgString.Equals("na") || dwgString.Equals("NULL"))
                                            dwgString = string.Empty;
                                    }
                                }
                            }


                            if (!_itemCode.Equals(-1))
                            {
                                if (dt.Rows[i][_itemCode] != null)
                                {
                                    int _i4 = i + 1;
                                    while (string.IsNullOrEmpty(dt.Rows[_i4][0].ToString()))
                                    {
                                        codeString = codeString.Trim() + dt.Rows[_i4][_itemCode].ToString().Trim();
                                        _i4 += 1;
                                    }

                                    itemCode = dt.Rows[i][_itemCode].ToString().Replace("Item No", "").Replace(".", "").Trim();
                                }
                            }

                            if (itemCode.EndsWith("-"))
                                itemCode = itemCode.Substring(0, itemCode.Length - 1).Trim();

                            iTemDESC = iTemDESC.Trim();
                            if (iTemDESC.EndsWith("-"))
                                iTemDESC = iTemDESC.Substring(0, iTemDESC.Length - 1).Trim();



                            if (!_itemQt.Equals(-1))
                                itemQt = dt.Rows[i][_itemQt].ToString().Trim();



                            if (!string.IsNullOrEmpty(subjStr.Trim()))
                                iTemSUBJ = subjStr.Trim();


                            if (!string.IsNullOrEmpty(dwgString.Trim()))
                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + "DWG: " + dwgString;

                            if (!string.IsNullOrEmpty(codeString))
                                itemCode = itemCode.Trim() + " " + codeString.Trim();

                            itemCode = itemCode.Replace("null-null", "").Replace("NA-NA", "").Replace("SEE ATTACHED", "").Replace("DRAWING", "").Replace("NA", "").Replace("na", "").Trim();

                            iTemUnit = "PCS";

                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if(GetTo.IsInt(itemQt))
                                dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = itemQt;
                            if(!string.IsNullOrEmpty(iTemSUBJ))
                                dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = itemCode;


                            iTemNo = string.Empty;
                            iTemUnit = string.Empty;
                            iTemDESC = string.Empty;
                            itemQt = string.Empty;
                            itemCode = string.Empty;
                            dwgString = string.Empty;
                            codeString = string.Empty;

                            //_itemCode = -1;
                            //_itemDescription = -1;
                            //_itemDescription2 = -1;
                            //_itemQt = -1;
                        }
                    }
                }
            }
        }
    }
}

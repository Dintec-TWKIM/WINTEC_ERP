using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Misuzu
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



        public Misuzu(string fileName)
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
            int rowCount = 0;
            int columnCount = 0;

            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            string eqnameStr = string.Empty;
            string makerStr = string.Empty;
            string snumStr = string.Empty;
            string dwgnumStr = string.Empty;
            string remarksStr = string.Empty;
            string modelnumStr = string.Empty;
            string itemnumStr = string.Empty;
            string itemcodeStr = string.Empty;

            int _itemCode = 0;
            int _itemDescription = 0;
            int _itemDrw = 0;
            int _itemUnit = 0;
            int _itemQt = 0;
            int _remark = 0;
            int _itemno = 0;


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

            bool itemStart = false;

            foreach (DataTable dt in ds.Tables)
            {
                rowCount = dt.Rows.Count - 1;
                columnCount = dt.Columns.Count - 1;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dataValue = dt.Rows[i][0].ToString();

                    if (dataValue.Equals("No.")) itemStart = true;

                    if (!itemStart)
                    {
                        // 선명과 문의번호는 같은 Row
                        if (dataValue.Equals("Vessel:"))
                        {
                            vessel = dt.Rows[i][1].ToString().Trim();

                            for (int j = 0; j < columnCount; j++)
                            {
                                if (dt.Rows[i][j].ToString().Equals("Req No:"))
                                    reference = dt.Rows[i][j + 1].ToString().Trim();
                            }
                        }
                        else if (dataValue.Equals("Shipyard:"))
                        {
                            for (int j = 0; j < columnCount; j++)
                            {
                                if (dt.Rows[i][j].ToString().Equals("Maker:"))
                                {
                                    makerStr = dt.Rows[i][j + 1].ToString().Trim();

                                    if (dt.Rows[i+1][j].ToString().Equals("Model No."))
                                        modelnumStr = dt.Rows[i + 1][j + 1].ToString().Trim();
                                }
                            }
                        }
                        else if (dataValue.Equals("Name of Equipment:"))
                        {
                            int _i = i+1;
                            eqnameStr = dt.Rows[i][1].ToString().Trim();

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                eqnameStr = eqnameStr.Trim() + Environment.NewLine + dt.Rows[_i][1].ToString().Trim();
                                _i += 1;
                            }

                            eqnameStr = eqnameStr.Trim();

                            for (int j = 0; j < columnCount; j++)
                            {
                                if (dt.Rows[i][j].ToString().Equals("Serial No.:"))
                                    snumStr = dt.Rows[i][j + 1].ToString().Trim();
                            }
                        }

                    }
                    else
                    {
                        if (dataValue.Contains("No"))
                        {
                            for (int c = 0; c < columnCount; c++)
                            {
                                if (dt.Rows[i][c].ToString() == "Name of Parts") _itemDescription = c;    //품목명
                                else if (dt.Rows[i][c].ToString().Contains("Part No")) _itemCode = c;              //아이템코드
                                else if (dt.Rows[i][c].ToString().Contains("Drawing ")) _itemDrw = c;        //DWG NO
                                else if (dt.Rows[i][c].ToString() == "Package") _itemUnit = c;             //단위
                                else if (dt.Rows[i][c].ToString() == "Qty") _itemQt = c;              //수량
                                else if (dt.Rows[i][c].ToString().Contains("Remarks")) _remark = c;             //리마크
                                else if (dt.Rows[i][c].ToString().Contains("Item No.")) _itemno = c;
                            }
                        }

                        
                        if (GetTo.IsInt(dataValue))
                        {
                            iTemNo = dataValue;

                            iTemDESC = dt.Rows[i][_itemDescription].ToString().Trim();
                            itemcodeStr = dt.Rows[i][_itemCode].ToString().Trim();
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                            itemnumStr = dt.Rows[i][_itemno].ToString().Trim();

                            if (itemcodeStr.Equals("NIL") || itemcodeStr.Equals("NA") || itemcodeStr.Equals("-"))
                                itemcodeStr = string.Empty;

                            if (itemnumStr.Equals("NIL") || itemnumStr.Equals("NA") || itemnumStr.Equals("-"))
                                itemnumStr = string.Empty;


                            if (_remark != 0)
                                remarksStr = dt.Rows[i][_remark].ToString().Trim();

                            //if (!string.IsNullOrEmpty(itemnum))
                            //{
                            //    if (!itemcode.Equals("-") && !string.IsNullOrEmpty(itemcode))
                            //        iTemCode = itemnum + "-" + itemcode;
                            //    else
                            //        iTemCode = itemnum;
                            //}
                            //else
                            //{
                            //    iTemCode = itemcode;

                            //    // 값이 없을때는 공백으로 처리
                            //    if (iTemCode.Equals("-"))
                            //        iTemCode = string.Empty;
                            //}
                            


                            // 품목명, DWG 여러 ROW 일때
                            int _i = i + 1;
                            dwgnumStr = dt.Rows[i][_itemDrw].ToString().Trim();
                            // dwgnum 값이 없을때에는 한칸 뒤에 가져옴
                            if (string.IsNullOrEmpty(dwgnumStr))
                            {
                                dwgnumStr = dt.Rows[i][_itemDrw + 1].ToString().Trim();

                                while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                {
                                    //같은 줄에 없을수도 있음. dwg
                                    dwgnumStr = dwgnumStr.Trim() + dt.Rows[_i][_itemDrw].ToString().Trim();
                                    dwgnumStr = dwgnumStr.Trim() + dt.Rows[_i][_itemDrw + 1].ToString().Trim();
                                    iTemDESC = iTemDESC.Trim() + dt.Rows[_i][_itemDescription].ToString().Trim();
                                    _i += 1;
                                }
                            }
                            else
                            {
                                // dwgnum이 같은 컬럼에 있을때엔, 여러줄만 반복 가져옴
                                while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                {
                                    dwgnumStr = dwgnumStr.Trim() + dt.Rows[_i][_itemDrw].ToString().Trim();
                                    iTemDESC = iTemDESC.Trim() + dt.Rows[_i][_itemDescription].ToString().Trim();
                                    _i += 1;
                                }
                            }


                            // dwg 마지막에 하이픈(-) 삭제
                            dwgnumStr = dwgnumStr.Trim();
                            if (dwgnumStr.EndsWith("-"))
                                dwgnumStr = dwgnumStr.Substring(0, dwgnumStr.Length - 1);

                            if (makerStr.Equals("NA"))
                                makerStr = string.Empty;

                            if (snumStr.Equals("NA"))
                                snumStr = string.Empty;

                            if (dwgnumStr.Equals("NA"))
                                dwgnumStr = string.Empty;

                            if (modelnumStr.Equals("NA"))
                                modelnumStr = string.Empty;


                            // 주제들어가는 값 : maker, serial no, dwg no, model no 값이 없을땐 제외
                            iTemSUBJ = eqnameStr + Environment.NewLine;

                            if (!string.IsNullOrEmpty(makerStr))
                                iTemSUBJ = iTemSUBJ + "MAKER: " + makerStr + Environment.NewLine;

                            if (!string.IsNullOrEmpty(snumStr))
                                iTemSUBJ = iTemSUBJ + "S/NO.: " + snumStr + Environment.NewLine;

                            if (!string.IsNullOrEmpty(dwgnumStr))
                                iTemSUBJ = iTemSUBJ + "DWG.NO.: " + dwgnumStr + Environment.NewLine;

                            if (!string.IsNullOrEmpty(modelnumStr))
                                iTemSUBJ = iTemSUBJ + "MODEL NO.: " + modelnumStr + Environment.NewLine;


                            // 추가하기로 결정 20170926
                            //remarks = remarks.Replace("FOR REPLENISHMENT OF SHIP'S SPARE", "").Trim();
                            //remarks = remarks.Replace("FOR REPLACEMENT", "").Trim();
                            //remarks = remarks.Replace("SEE PHOTOS DRAWING", "").Trim();
                            //remarks = remarks.Replace("REPLACE DEFECTIVE PART", "").Trim();
                            //remarks = remarks.Replace("FOR STOCK", "").Trim();


                            // remarks 있을 땐 품목명에 추가
                            if (!string.IsNullOrEmpty(remarksStr))
                                iTemDESC = iTemDESC + ", " + remarksStr;




                            iTemCode = itemcodeStr.Trim();
                            if (iTemCode.EndsWith("-"))
                                iTemCode = iTemCode.Substring(0, iTemCode.Length - 1);

                            if (!string.IsNullOrEmpty(itemnumStr))
                                iTemDESC = iTemDESC.Trim() + ", ITEM NO: " + itemnumStr;


                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR "+iTemSUBJ;
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                            iTemNo = string.Empty;
                            iTemDESC = string.Empty;
                            iTemUnit = string.Empty;
                            iTemQt = string.Empty;

                        }
                    }
                }
            }
        }
    }
}

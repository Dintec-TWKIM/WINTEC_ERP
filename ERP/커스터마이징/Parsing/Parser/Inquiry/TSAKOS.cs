using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class TSAKOS
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



        public TSAKOS(string fileName)
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
            dtItem.Columns.Add("UNIQ");         // 선사
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

            string subjStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string drwStr = string.Empty;
            string descStr = string.Empty;


            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemCode = -1;


            bool rfqNo = false;

            bool typeSt = false;
            bool serialSt = false;
            bool drwSt = false;


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
                    string secondColStr = dt.Rows[i][1].ToString();


                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Ship's Requisition No"))
                                rfqNo = true;

                            if (rfqNo)
                                reference = reference.Trim() + dt.Rows[i][c].ToString().Replace("Ship's Requisition No","").Trim();

                            reference = reference.Replace(" ", "").Trim();
                        }
                    }

                    if (firstColStr.StartsWith("Vessel:"))
                    {
                        vessel = firstColStr.Replace("Vessel:", "").Trim();

                        if (vessel.Contains(" "))
                        {
                            string[] vesselSpl = vessel.Split(' ');

                            vessel = vesselSpl[0].ToString().Trim();
                        }
                    }
                    else if (secondColStr.StartsWith("Vessel:"))
                    {
                        vessel = secondColStr.Replace("Vessel:", "").Trim();

                        if (vessel.Contains(" "))
                        {
                            string[] vesselSpl = vessel.Split(' ');

                            vessel = vesselSpl[0].ToString().Trim();
                        }
                    }
                    else if (firstColStr.StartsWith("Machinery"))
                    {
                        subjStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (firstColStr.StartsWith("Maker") || secondColStr.StartsWith("Maker"))
                    {
                        makerStr = dt.Rows[i][1].ToString().Replace("Maker:", "").Trim() + dt.Rows[i][2].ToString().Replace("Maker:", "").Trim();
                    }
                    else if (firstColStr.StartsWith("Type") || secondColStr.StartsWith("Type"))
                    {
                        typeSt = true; serialSt = false; drwSt = false;

                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Serial No:"))
                            {
                                typeSt = false; serialSt = true; drwSt = false;
                            }
                            else if (dt.Rows[i][c].ToString().StartsWith("Drawing No"))
                            {
                                typeSt = false; serialSt = false; drwSt = true;
                            }
                            else if (typeSt)
                                typeStr = dt.Rows[i][1].ToString().Replace("Type:", "").Trim();
                            else if (serialSt)
                                serialStr = serialStr.Trim() + dt.Rows[i][c].ToString().Replace("Serial No:", "").Trim();
                            else if (drwSt)
                                drwStr = drwStr.Trim() + dt.Rows[i][c].ToString().Replace("Drawing No/Instruction Book:", "").Trim();
                        }

                    }
                    else if (firstColStr.Equals("No"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Subsystem Description"))
                                _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Equals("Code"))
                                _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Req. Qnt."))
                                _itemQt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (!_itemQt.Equals(-1))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (!_itemDesc.Equals(-1))
                        {
                            if (dt.Rows[i + 1][0].ToString().Equals("Item Desc:") || dt.Rows[i + 1][1].ToString().Equals("Item Desc:"))
                            {
                                descStr = dt.Rows[i][_itemDesc].ToString().Trim();
                                iTemDESC = dt.Rows[i + 1][_itemDesc].ToString().Trim();
                            }
                            //int _i = i + 1;
                            //while (string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                            //{
                            //    iTemDESC = iTemDESC.Trim() + 
                            //               Environment.NewLine + 
                            //               dt.Rows[_i][_itemDesc].ToString().Trim();

                            //    _i += 1;
                            //    if (_i > dt.Rows.Count)
                            //        break;
                            //}
                        }


                        if (!_itemCode.Equals(-1))
                        {
                            iTemCode = dt.Rows[i][_itemCode].ToString().Replace("NA","").Trim();

                            if (string.IsNullOrEmpty(iTemCode))
                                iTemCode = dt.Rows[i][_itemCode + 1].ToString().Replace("NA", "").Trim();
                        }

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr;

                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                        if (!string.IsNullOrEmpty(typeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();

                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();

                        if (!string.IsNullOrEmpty(drwStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "Drawing No/Instruction Book: " + drwStr.Trim();

                        if (!string.IsNullOrEmpty(descStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + descStr.Trim();


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;
                        iTemDESC = string.Empty;

                        descStr = string.Empty;
                    }
                }
            }
        }
    }
}

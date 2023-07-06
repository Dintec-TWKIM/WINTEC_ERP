using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Yuan
    {
        string vessel;
        string reference;
        string contact;
        string imoNumber;
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

        public DataTable Item
        {
            get
            {
                return dtItem;
            }
        }

        #endregion ==================================================================================================== Constructor



        public Yuan(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            contact = string.Empty;
            imoNumber = string.Empty;           // imo

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

            int _itemDescInt = -1;
            int _itemQtInt = -1;
            int _itemCodeInt = -1;

            string subjEquip = string.Empty;
            string subjType = string.Empty;
            string subjMaker = string.Empty;
            string subjSpec = string.Empty;
            string subjSerial = string.Empty;



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
                    if (string.IsNullOrEmpty(reference))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Enquiry No."))
                            {
                                reference = dt.Rows[i][c + 1].ToString().Replace(":","").Trim();
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(contact))
                    {
                        if (string.IsNullOrEmpty(contact))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().Contains("PIC"))
                                {
                                    contact = dt.Rows[i][c].ToString().Replace("PIC", "").Replace(":", "").Trim() + dt.Rows[i][c + 1].ToString().Replace("PIC", "").Replace(":", "").Trim();
                                }
                            }
                        }

                        if(!string.IsNullOrEmpty(contact))
                        {
                            int idx_lts = contact.IndexOf("(");
                            int idx_lte = contact.IndexOf(")");


                            if(idx_lte != -1 && idx_lts != -1)
                            {
                                contact = contact.Substring(0, idx_lts).Trim();                            
                            }
                        }

                    }

                    string firstColStr = dt.Rows[i][0].ToString();

                    //if (firstColStr.Contains("M.V./Reference")) 
                    //{
                    //    vessel = dt.Rows[i][1].ToString().Replace(":", "").Trim();       //중국어 지워야함

                    //    if (vessel.Contains("中"))
                    //    {
                    //        string[] vesselSpl = vessel.Split('中');

                    //        vessel = vesselSpl[0].ToString().Trim();
                    //    }
                    //}

                    if(string.IsNullOrEmpty(vessel))
                    {
                        for(int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("IMO NO"))
                                imoNumber = dt.Rows[i][c + 1].ToString().Replace(":","").Trim();
                        }
                    }

                    if (firstColStr.Contains("ITEM"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("QUANTITY")) _itemQtInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("DESCRIPTION")) _itemDescInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("PART NO")) _itemCodeInt = c;
                        }
                    }
                    else if (firstColStr.StartsWith("(0"))
                    {
                        for (int r = i; r <i + 7; r++)
                        {
                            if (GetTo.IsInt(dt.Rows[i][0].ToString()))
                                break;

                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                string strText = dt.Rows[r][c].ToString().ToLower().Trim();

                                if (strText.StartsWith("equipment"))
                                    subjEquip = dt.Rows[r][c + 1].ToString().Replace(":", "").Trim();
                                
                                if (strText.StartsWith("type") && string.IsNullOrEmpty(subjType))
                                    subjType = dt.Rows[r][c + 1].ToString().Replace(":", "").Trim();
                                
                                if (strText.StartsWith("maker"))
                                    subjMaker = dt.Rows[r][c + 1].ToString().Replace(":", "").Trim();
                                
                                if (strText.StartsWith("specification"))
                                    subjSpec = dt.Rows[r][c + 1].ToString().Replace(":", "").Trim();
                                
                                if (strText.StartsWith("serial") && string.IsNullOrEmpty(subjSerial))
                                {
                                    subjSerial = dt.Rows[r][c + 1].ToString().Replace(":", "").Trim();

                                    if(string.IsNullOrEmpty(subjSerial))
                                    {
                                        subjSerial = dt.Rows[r - 1][c + 1].ToString().Trim().Replace(":", "").Trim() + dt.Rows[r + 1][c + 1].ToString().Trim().Replace(":", "").Trim();
                                    }
                                }
                            }
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
                    {
                        if (!_itemDescInt.Equals(-1))
                        {
                            iTemDESC = dt.Rows[i][_itemDescInt].ToString().Trim() + Environment.NewLine;

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                for (int c = 1; c < dt.Columns.Count; c++)
                                {
                                    if (dt.Rows[_i][c].ToString().StartsWith("** End"))
                                        break;
                                    else
                                        iTemDESC = iTemDESC  + dt.Rows[_i][c].ToString().Trim();
                                }
                                iTemDESC = iTemDESC.Trim() + Environment.NewLine;

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }
                        
                        if(!_itemCodeInt.Equals(-1))
                            iTemCode = dt.Rows[i][_itemCodeInt].ToString().Trim();

                        if (!_itemQtInt.Equals(-1))
                        {
                            string[] qtUnitSpl = dt.Rows[i][_itemQtInt].ToString().Split(' ');

                            if (qtUnitSpl.Length >= 2)
                            {
                                iTemQt = qtUnitSpl[0].ToString().Trim();
                                iTemUnit = qtUnitSpl[1].ToString().Trim();
                            }
                        }

                        if (!string.IsNullOrEmpty(subjEquip))
                            iTemSUBJ = subjEquip;

                        if (!string.IsNullOrEmpty(subjMaker))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + subjMaker;

                        if (!string.IsNullOrEmpty(subjType))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + subjType;

                        if (!string.IsNullOrEmpty(subjSerial))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + subjSerial;

                        if (!string.IsNullOrEmpty(subjSpec))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "SPECIFICATION: : " + subjSpec;


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR "+ iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemDESC = string.Empty;
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;
                        iTemUnit = string.Empty;
                        iTemSUBJ = string.Empty;
                    }
                }
            }
        }
    }
}

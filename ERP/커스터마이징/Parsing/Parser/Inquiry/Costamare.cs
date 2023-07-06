using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class Costamare
    {
        string vessel;
        string reference;
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

        public string ImoNumber
        {
            get
            {
                return imoNumber;
            }
        }

        #endregion ==================================================================================================== Constructor



        public Costamare(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            imoNumber = "";

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

            int _itemQt = -1;
            int _itemDesc = -1;

            int _zeroInt = -1;

            string subjStr = string.Empty;
            string manufacStr = string.Empty;
            string modelStr = string.Empty;
            string serialStr = string.Empty;
            string accountStr = string.Empty;
            string typeStr = string.Empty;
            string sizeStr = string.Empty;

            string deptStr = string.Empty;
            string deptStr2 = string.Empty;

            string asslyStr = string.Empty;
            string subasslyStr = string.Empty;

            string capStr = string.Empty;


            bool itemStart = false;

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
                    string zeroStr = dt.Rows[0][0].ToString();
                    string zeroStr2 = dt.Rows[0][1].ToString();

                    if (string.IsNullOrEmpty(imoNumber))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("IMO No"))
                                imoNumber = dt.Rows[i][c].ToString().Replace("IMO", "").Replace("No", "").Replace(".:", "").Trim();
                        }
                    }

                    if(string.IsNullOrEmpty(vessel))
                    {
                        for(int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().StartsWith("Vessel"))
                            {
                                vessel = dt.Rows[i][c].ToString().Replace("Vessel","").Replace(":","").Trim();

                                if(string.IsNullOrEmpty(vessel))
                                {
                                    vessel = dt.Rows[i][c + 1].ToString().Trim();
                                }
                            }
                        }
                    }

                    if(string.IsNullOrEmpty(reference))
                    {
                        for(int c = 0; c < dt.Columns.Count; c++)
                        {
                            if(dt.Rows[i][c].ToString().ToLower().StartsWith("request for quotation"))
                            {
                                for(int c2 = 0; c2 < dt.Columns.Count; c2++)
                                {
                                    if (string.IsNullOrEmpty(reference))
                                        reference = dt.Rows[i][c2].ToString().Replace("REQUEST FOR QUOTATION No.:", "").Trim();
                                    else
                                        break;

                                }
                            }
                        }
                    }


                    if (_zeroInt.Equals(-1))
                    {
                        if (!string.IsNullOrEmpty(zeroStr)) _zeroInt = 0;
                        else if (!string.IsNullOrEmpty(zeroStr2)) _zeroInt = 1;

                    }

                    else if (_zeroInt.Equals(0))
                    {
                        if (!string.IsNullOrEmpty(zeroStr)) _zeroInt = 0;
                        else if (!string.IsNullOrEmpty(zeroStr2)) _zeroInt = 1;
                    }

                    if (_zeroInt == -1)
                        _zeroInt = 0;

                    string dataFirstValue = dt.Rows[i][_zeroInt].ToString();


                    if (dataFirstValue.Equals("Item") || dataFirstValue.Equals("No."))
                    {
                        itemStart = true;

                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Qty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
                        }
                    }
                    else if (dataFirstValue.Equals("ASSLY:"))
                    {
                        asslyStr = dt.Rows[i][1].ToString().Trim();
                    }
                    else if (dataFirstValue.Equals("Subassly:"))
                    {
                        subasslyStr = dt.Rows[i][1].ToString().Trim();
                    }

                    if (!itemStart)
                    {
                        //if (string.IsNullOrEmpty(vessel))
                        //{
                        //    if (dataFirstValue.Contains("REQUEST FOR QUOTATION No.:"))
                        //        reference = dataFirstValue.Replace("REQUEST FOR QUOTATION No.:", "").Trim();

                        //    if (dataFirstValue.Contains("Vessel:"))
                        //        vessel = dataFirstValue.Replace("Vessel:", "").Trim();


                        //    if (vessel.Contains("-"))
                        //    {
                        //        string[] vesselSpl = vessel.Split('-');

                        //        vessel = vesselSpl[0].ToString().Trim();
                        //    }

                        //}

                        if (dataFirstValue.Contains("EQUIPMENT") || dataFirstValue.Contains("Manufacturer"))
                        {
                            subjStr = string.Empty;

                            for (int c = _zeroInt; c < dt.Columns.Count; c++)
                            {
                                subjStr = subjStr.Trim() + dt.Rows[i][c].ToString().Replace("EQUIPMENT", "").Replace(":","").Trim();
                            }

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][_zeroInt].ToString()))
                            {

                                subjStr = subjStr.Trim() + Environment.NewLine;

                                for (int c = _zeroInt; c < dt.Columns.Count; c++)
                                {
                                    subjStr = subjStr + " " + dt.Rows[_i][c].ToString().Trim();
                                    //subjStr = subjStr.Trim();


                                    //if (dt.Rows[_i][c].ToString().Contains("Manufacturer"))
                                    //{
                                    //    manufacStr = dt.Rows[_i][c + 1].ToString().Trim();
                                    //}
                                    //else if (dt.Rows[_i][c].ToString().Contains("Model"))
                                    //{
                                    //    if (!dt.Rows[_i][c + 1].ToString().Contains("Type"))
                                    //        modelStr = dt.Rows[_i][c + 1].ToString().Trim();
                                    //}
                                    //else if (dt.Rows[_i][c].ToString().Contains("Type"))
                                    //{
                                    //    typeStr = dt.Rows[_i][c + 1].ToString().Trim();
                                    //}
                                    //else if (dt.Rows[_i][c].ToString().Contains("Serial No"))
                                    //{
                                    //    if (!dt.Rows[_i][c + 1].ToString().Contains("Size"))
                                    //        serialStr = dt.Rows[_i][c + 1].ToString().Trim();
                                    //    else
                                    //        serialStr = dt.Rows[_i][c].ToString().Replace("Serial No","").Replace(":","").Replace(".","").Trim();

                                    //    if (!dt.Rows[_i + 1][c].ToString().Contains("Account No") || !dt.Rows[_i + 1][c].ToString().Contains("Dept") || !dt.Rows[_i + 1][c].ToString().Contains("Capacity"))
                                    //        serialStr = serialStr.Trim() + dt.Rows[_i + 1][c].ToString().Trim();
                                    //}
                                    //else if (dt.Rows[_i][c].ToString().Contains("Size"))
                                    //{
                                    //    sizeStr = dt.Rows[_i][c + 1].ToString().Trim();
                                    //}
                                    //else if (dt.Rows[_i][c].ToString().Contains("Capacity"))
                                    //{
                                    //    capStr = dt.Rows[_i][c + 1].ToString().Trim();

                                    //    if (string.IsNullOrEmpty(capStr))
                                    //        capStr = dt.Rows[_i][c].ToString().Replace("Capacity:", "").Trim();

                                    //    if (!dt.Rows[_i + 1][0].ToString().Equals("Item"))
                                    //        capStr = capStr.Trim() + " " + dt.Rows[_i + 1][c].ToString().Trim();
                                    //}
                                }
                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (GetTo.IsInt(dataFirstValue))
                        {
                            string[] itemQtUnitSplit = dt.Rows[i][_itemQt].ToString().Split(' ');

                            if (itemQtUnitSplit.Length == 2)
                            {
                                iTemQt = itemQtUnitSplit[0].ToString().Trim();
                                iTemUnit = itemQtUnitSplit[1].ToString().Trim();
                            }

                            if(_itemDesc != -1)
                                iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][_zeroInt].ToString()))
                            {
                                for (int c = _itemDesc; c < _itemDesc + 2; c++)
                                {
                                        iTemDESC = iTemDESC.Trim() + " "+ dt.Rows[_i][c].ToString().Trim();
                                }

                                _i += 1;

                                if (_i == dt.Rows.Count)
                                    break;
                            }
                            iTemDESC = iTemDESC.Replace("Total (USD):", "").Trim();


                            int idx_s = 0;

                            idx_s = iTemDESC.ToUpper().IndexOf("PART.NO:");


                            if (!idx_s.Equals(-1))
                                iTemCode = iTemDESC.Substring(idx_s, iTemDESC.Length - idx_s).ToString();
                            else
                            {
                                idx_s = iTemDESC.ToUpper().IndexOf("PART NO");

                                if (!idx_s.Equals(-1))
                                    iTemCode = iTemDESC.Substring(idx_s, iTemDESC.Length - idx_s).ToString();
                            }

                            if (!string.IsNullOrEmpty(iTemCode))
                                iTemDESC = iTemDESC.Replace(iTemCode, "").Trim();

                            iTemCode = iTemCode.Replace("Part.No", "").Replace(":","").Trim();


                            if (!string.IsNullOrEmpty(subjStr))
                                iTemSUBJ = subjStr.Replace("\n   ", "\n").Replace("\n    ", "").Replace("\n  ", "");

                            if (!string.IsNullOrEmpty(modelStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr;

                            if (!string.IsNullOrEmpty(typeStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr;

                            if (!string.IsNullOrEmpty(serialStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialStr;

                            if (!string.IsNullOrEmpty(sizeStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "SIZE: " + sizeStr;

                            if (!string.IsNullOrEmpty(capStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "CAPACITY: " + capStr;

                            if (!string.IsNullOrEmpty(asslyStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "ASSLY: " + asslyStr;

                            if (!string.IsNullOrEmpty(subasslyStr))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "SUBASSLY: " + subasslyStr;

                           


                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                            if(!string.IsNullOrEmpty(iTemSUBJ))
                                dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                            iTemDESC = string.Empty;
                            iTemSUBJ = string.Empty;
                            iTemUnit = string.Empty;
                            iTemQt = string.Empty;
                            iTemCode = string.Empty;

                            idx_s = -1;
                        }
                    }

                }
            }
        }
    }
}

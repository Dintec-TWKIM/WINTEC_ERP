using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
    class AlmiPdf
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



        public AlmiPdf(string fileName)
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
            dtItem.Columns.Add("UNIQ");         //선사코드
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

            string subjStr1 = string.Empty;
            string subjStr2 = string.Empty;

            int _referenceCheckInt = -1;

            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemCode = -1;
            int _itemUnit = -1;

            string makerStr = string.Empty;
            string mainStr = string.Empty;
            string serialStr = string.Empty;
            string subsystemStr = string.Empty;
            string catStr = string.Empty;
            string partiStr = string.Empty;

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
                    string firstColString = dt.Rows[i][0].ToString();
                    string secondColStr = dt.Rows[i][1].ToString();


                    if(string.IsNullOrEmpty(imoNumber))
                    {
                        for(int c = 1; c < dt.Columns.Count; c++)
                        {
                            if(dt.Rows[i][c].ToString().StartsWith("IMO"))
                            {
                                for(int c2 = c+1; c2 < dt.Columns.Count; c2++)
                                {
                                    if (string.IsNullOrEmpty(imoNumber))
                                        imoNumber = imoNumber.Trim() + dt.Rows[i][c2].ToString().Trim();
                                }
                            }    
                        }
                    }

                    if (string.IsNullOrEmpty(vessel))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToLower().StartsWith("vessel:"))
                            {
                                for (int c2 = c; c2 < dt.Columns.Count; c2++)
                                {
                                    if (string.IsNullOrEmpty(vessel))
                                        vessel = vessel.Trim() + dt.Rows[i][c2].ToString().Replace("vessel:", "").Trim();
                                }
                            }
                        }
                    }

                    if(reference.Length < 5 && firstColString.Contains("Requisition No.:"))
					{
                        reference = dt.Rows[i][1].ToString().Trim();
					}


                    if (_referenceCheckInt.Equals(-1))
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().ToLower().StartsWith("request"))
                            {
                                _referenceCheckInt = c;
                                reference = dt.Rows[i][c + 1].ToString().Trim();
                                itemStart = true; 
                            }

                            if(dt.Rows[i][c].ToString().ToLower().StartsWith("req. code/date:"))
                            {
                                _referenceCheckInt = c;
                                reference = dt.Rows[i][c].ToString().ToLower().Replace("req. code/date:", "").Trim();

                                string[] refSpl = reference.Split('/');

                                if(refSpl.Length == 2)
                                {
                                    reference = refSpl[0].ToString().Trim();
                                }

                                itemStart = true;
                            }
                        }
                    }
                    else if (itemStart)
                    {
                        if (string.IsNullOrEmpty(vessel))
                        {
                            for (int c = _referenceCheckInt; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().Equals("Request For Quotation:"))
                                    reference = dt.Rows[i][c + 1].ToString().Trim();
                                else if (dt.Rows[i][c].ToString().Equals("Vessel:"))
                                    vessel = dt.Rows[i][c + 1].ToString().Trim();

                                if (!string.IsNullOrEmpty(vessel))
                                    break;
                            }
                        }

                        if (firstColString.Equals("No.") || firstColString.Equals("Line"))
                        {
                            //QTY, PACKING, ITEM CODE, DESCRIPTION
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().Contains("QTY.") || dt.Rows[i][c].ToString().Contains("QTY") || dt.Rows[i][c].ToString().Contains("Quantity"))
                                    _itemQt = c;

                                if (dt.Rows[i][c].ToString().Equals("ITEM CODE") || dt.Rows[i][c].ToString().Contains("REQ Number") || dt.Rows[i][c].ToString().Contains("Part No"))
                                    _itemCode = c;

                                if (dt.Rows[i][c].ToString().Equals("PACKING") || dt.Rows[i][c].ToString().Contains("UOM") || dt.Rows[i][c].ToString().Contains("UoM"))
                                    _itemUnit = c;

                                if (dt.Rows[i][c].ToString().Contains("DESCRIPTION") || dt.Rows[i][c].ToString().Contains("Maker") || dt.Rows[i][c].ToString().Contains("Description"))
                                    _itemDesc = c;
                            }

                            if(!GetTo.IsInt(dt.Rows[i+1][0].ToString().Replace(".","")))
                            {
                                //subjStr1 = string.Empty;

                                //int _i = i+1;

                                //while(!GetTo.IsInt(dt.Rows[_i][0].ToString().Replace(".","")))
                                //{
                                //    subjStr1 = subjStr1.Trim() + Environment.NewLine + dt.Rows[_i][0].ToString().Trim();

                                //    _i += 1;

                                //    if (_i >= dt.Rows.Count)
                                //        break;
                                //}
                            }

                        }
                        //else if (firstColString.StartsWith("System:"))
                        //{
                        //    subjStr1 = firstColString.Trim();
                        //}
                        //else if (firstColString.StartsWith("Subsystem:"))
                        //{
                        //    subjStr2 = firstColString.Trim();
                        //}
                        //else if (firstColString.StartsWith("Maker"))
                        //{
                        //    makerStr = firstColString.Trim();
                        //}
                        //else if (secondColStr.ToLower().StartsWith("system:"))
                        //{
                        //    subjStr1 = secondColStr.Trim();
                        //}
                        //else if (secondColStr.ToLower().StartsWith("subsystem"))
                        //{
                        //    subjStr2 = secondColStr.Trim();
                        //}
                        else if (firstColString.StartsWith("Equipment:"))
						{
                            subjStr1 = string.Empty;

                            for(int c = 0; c < dt.Columns.Count; c++)
							{
                                subjStr1 = subjStr1 + " " + dt.Rows[i][c].ToString();
							}

                            int _i = i +1;

                            while(!dt.Rows[_i][0].ToString().Contains("No."))
							{
                                subjStr1 = subjStr1.Trim() + Environment.NewLine;
                                for (int c = 0; c < dt.Columns.Count; c++)
                                {
                                    subjStr1 = subjStr1 + " " + dt.Rows[_i][c].ToString();
                                }


                                if (_i >= dt.Rows.Count-1)
                                    break;
                                else
                                    _i += 1;
							}
						}
                        else if (GetTo.IsInt(firstColString.Replace(".", "")))
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


                            if (rowValueSpl[3] != null && rowValueSpl[4] == null)
                            {
                                _itemQt = Convert.ToInt16(rowValueSpl[1].ToString());
                                _itemUnit = Convert.ToInt16(rowValueSpl[1].ToString());
                                _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                                _itemDesc = Convert.ToInt16(rowValueSpl[3].ToString());
                            }
                            else if (rowValueSpl[4] != null && rowValueSpl[5] == null)
                            {
                                _itemQt = Convert.ToInt16(rowValueSpl[1].ToString());
                                _itemUnit = Convert.ToInt16(rowValueSpl[1].ToString());
                                _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                                _itemDesc = Convert.ToInt16(rowValueSpl[3].ToString());
                            }
                            else if (rowValueSpl[6] != null && rowValueSpl[7] == null)
                            {
                                _itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
                                _itemUnit = Convert.ToInt16(rowValueSpl[6].ToString());
                                _itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
                                _itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
                            }


                            string[] valueSplit = null;
                            //if (_itemQt != -1)
                            //{
                            //    valueSplit = dt.Rows[i][_itemQt].ToString().Split(' ');

                            //    if (valueSplit.Length == 2)
                            //    {
                            //        iTemQt = valueSplit[0].ToString().Trim();
                            //        iTemUnit = valueSplit[1].ToString().Trim();

                            //        iTemCode = dt.Rows[i][_itemQt + 1].ToString().Trim();

                            //        if (string.IsNullOrEmpty(iTemCode))
                            //            iTemCode = dt.Rows[i][_itemQt + 2].ToString().Trim();
                            //    }
                            //    else
                            //    {
                            //        string testCode = dt.Rows[i][_itemCode].ToString();

                            //        if (!string.IsNullOrEmpty(testCode))
                            //        {
                            //            if (!GetTo.IsInt(testCode.Substring(0, 1)))
                            //            {
                            //                string[] valueSplit2 = dt.Rows[i][_itemQt + 1].ToString().Split(' ');

                            //                if (valueSplit2.Length == 2)
                            //                {
                            //                    iTemQt = valueSplit2[0].ToString().Trim();
                            //                    iTemUnit = valueSplit2[1].ToString().Trim();
                            //                }
                            //            }
                            //        }
                            //    }
                            //}


                            



                            if(!string.IsNullOrEmpty(subjStr1))
                                iTemSUBJ = subjStr1.Trim();

                            //if (!string.IsNullOrEmpty(subjStr2))
                            //    iTemSUBJ = iTemSUBJ + Environment.NewLine + subjStr2.Trim();


                            if(_itemCode != -1)
							{
                                iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

                                int _i = i + 1;


                                while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
								{
                                    iTemCode = iTemCode + " " + dt.Rows[_i][_itemCode].ToString().Trim();

                                    if (_i >= dt.Rows.Count - 1)
                                        break;
                                    else
                                        _i += 1;
								}
							}

                            if (_itemDesc != -1)
                            {
                                iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                                int _i = i + 1;

                                while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
								{
                                    iTemDESC = iTemDESC + " " + dt.Rows[_i][_itemDesc].ToString();

                                    _i += 1;

                                    if (dt.Rows.Count <= _i)
                                        break;
								}


                                int idx_s = xmlTemp.IndexOf(iTemDESC);

                                string[] separatingStrings = { "\r\n" };


                                if (idx_s != -1)
                                {
                                    string descTemp1 = xmlTemp.Substring(idx_s, xmlTemp.Length - idx_s);
                                    string[] descTemp_Spl = descTemp1.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

                                    if(descTemp_Spl.Length > 0)
									{
                                        string descStr1 = descTemp_Spl[0].ToString();

                                        int idx_e = descStr1.IndexOf("</");

                                        if(idx_e != -1)
										{
                                            iTemDESC = descStr1.Substring(0, idx_e);
										}
									}
                                }
                            }



                           



                            //for (int c = _itemDesc; c < _itemCode; c++)
                            //{
                            //    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                            //}

                            //int _i = i+1;

                            //while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            //{
                            //    for (int c = 0; c < _itemCode; c++)
                            //    {
                            //        if (dt.Rows[_i][c].ToString().ToUpper().StartsWith("TOTAL") || dt.Rows[_i][c].ToString().ToUpper().Contains("NET TOTAL") || dt.Rows[_i][c].ToString().ToUpper().Contains("PAGE"))
                            //        {
                            //            break;
                            //        }
                            //        else
                            //        {
                            //            iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
                            //        }
                            //    }

                            //    _i += 1;

                            //    if (_i >= dt.Rows.Count)
                            //        break;
                            //}


                            if (string.IsNullOrEmpty(iTemCode))
                                iTemCode = dt.Rows[i][_itemCode].ToString().Replace("IMPA","").Trim();

                            if (string.IsNullOrEmpty(iTemQt))
                                iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                            if (_itemUnit != -1 && string.IsNullOrEmpty(iTemUnit))
                                iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();


                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                            if(!string.IsNullOrEmpty(iTemSUBJ))
                                dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                            iTemQt = string.Empty;
                            iTemUnit = string.Empty;
                            iTemDESC = string.Empty;
                            iTemSUBJ = string.Empty;
                            iTemCode = string.Empty;

                        }
                    }
                }
            }
        }
    }
}

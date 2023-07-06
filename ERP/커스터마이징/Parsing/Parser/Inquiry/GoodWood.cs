using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class GoodWood
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



        public GoodWood(string fileName)
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
			string iTemRmk = string.Empty;
				

            string subjNameStr = string.Empty;
            string subjTypeStr = string.Empty;
            string subjSerialStr = string.Empty;
            string subjMakerStr = string.Empty;
            string subjNoteStr = string.Empty;
			

            int _itemDescInt = -1;
            int _itemRefInt = -1;
            int _itemDrwInt = -1;
            int _itemUnitInt = -1;
            int _itemQtInt = -1;
            int _itemSubjInt = -1;
			int _itemRmk = -1;

            string descDrwStr = string.Empty;

            bool typeBool = false;
            bool makerBool = false;

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

                    // 선명
                    if (firstColStr.Contains("Vessel Name"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(vessel.Trim()))
                                vessel = dt.Rows[i][c].ToString().Trim();
                        }
                        vessel = vessel.Replace(":", "").Trim();
                    }
                    // 문의번호
                    else if (firstColStr.Contains("RFQ No."))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(reference))
                                reference = dt.Rows[i][c].ToString().Trim();
                        }
                        reference = reference.Replace(":", "").Trim();
                    }
                    else if (firstColStr.Contains("Equipment Name"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Type"))
                                typeBool = true;

                            if(!typeBool)
                                subjNameStr = subjNameStr.Trim() + dt.Rows[i][c].ToString().Trim();
                            else
                                subjNameStr = subjNameStr.Trim() + dt.Rows[i][c].ToString().Replace("Type/Model","").Trim();
                        }
                    }
                    else if (firstColStr.Contains("Serial Number") || firstColStr.Contains("Serial No"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Maker"))
                            {
                                makerBool = true;
                            }

                            if(!makerBool)
                                subjSerialStr = subjSerialStr.Trim() + dt.Rows[i][c].ToString().Trim();
                            else
                                subjMakerStr = subjMakerStr.Trim() + dt.Rows[i][c].ToString().Replace("Maker","").Trim();
                        }
                    }
                    else if (firstColStr.Contains("Remarks To Vendor"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            subjNoteStr = subjNoteStr + dt.Rows[i][c].ToString().Trim();
                        }

                        int _i = i + 1;
                        while (!dt.Rows[_i][0].ToString().Contains("Vendor Remarks"))
                        {
                            if (dt.Rows[_i][0].ToString().Contains("Vendor Remarks")) break;

                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                subjNoteStr = subjNoteStr + dt.Rows[_i][c].ToString().Trim();
                            }
                            subjNoteStr = subjNoteStr.Trim() + Environment.NewLine;

                            _i += 1;
                        }
                    }
                    else if (firstColStr.Contains("S. No."))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Equals("Item Description")) _itemDescInt = c;
                            else if (dt.Rows[i][c].ToString().Contains("IMPA No")) _itemRefInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("UOM")) _itemUnitInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qty.")) _itemQtInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Model")) _itemDrwInt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Equipment Name")) _itemSubjInt = c;
                        }
                    }
                    else if (GetTo.IsInt(firstColStr))
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


                        if (rowValueSpl[4] != null && rowValueSpl[5] == null)
                        {
                            _itemDescInt = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemRefInt= Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemUnitInt= Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemQtInt = Convert.ToInt16(rowValueSpl[4].ToString());
                        }
                        else if (rowValueSpl[5] != null && rowValueSpl[6] == null)
                        {
                            //_itemSubjInt = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemDescInt = Convert.ToInt16(rowValueSpl[1].ToString());
                            _itemRefInt = Convert.ToInt16(rowValueSpl[2].ToString());
                            _itemUnitInt = Convert.ToInt16(rowValueSpl[3].ToString());
                            _itemQtInt = Convert.ToInt16(rowValueSpl[4].ToString());
							_itemRmk = Convert.ToInt16(rowValueSpl[5].ToString());
						}


                        // UNIT
                        if (!_itemUnitInt.Equals(-1))
                        {
                            iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();

                            if (GetTo.IsInt(iTemUnit.Replace(".", "")))
                            {
                                iTemUnit = dt.Rows[i][_itemUnitInt - 1].ToString().Trim();
                                _itemQtInt = _itemUnitInt;
                                _itemRefInt = -1;
                            }
                        }

                        // QT
                        if (!_itemQtInt.Equals(-1))
                        {
                            iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();

                            if(string.IsNullOrEmpty(iTemQt))
							{
                                iTemQt = dt.Rows[i][_itemQtInt+1].ToString().Trim();
                            }

                            if (!GetTo.IsInt(iTemQt.Replace(".","")))
                            {
                                iTemQt = "0";
                            }
                        }

                        // CODE
                        if (!_itemRefInt.Equals(-1))
                            iTemCode = dt.Rows[i][_itemRefInt].ToString().Trim();


                        // DESC
                        if (!_itemDescInt.Equals(-1))
                        {
                            for (int c = _itemDrwInt + 1; c <= _itemDescInt; c++)
                            {
                                if(firstColStr != dt.Rows[i][c].ToString())
                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                            }

                            int _i = i + 1;
                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                for (int c = _itemDrwInt + 1; c <= _itemDescInt; c++)
                                {
                                    iTemDESC = iTemDESC.Trim() + dt.Rows[_i][c].ToString().Trim();
                                }
                                _i += 1;
                            }
                        }

                        
                        // SUBJ
                        if (!_itemDrwInt.Equals(-1))
                            descDrwStr = dt.Rows[i][_itemDrwInt].ToString().Trim();

                        if (!string.IsNullOrEmpty(subjNameStr))
                        {
                            iTemSUBJ = iTemSUBJ.Trim() + subjNameStr.Trim();

                            iTemSUBJ = iTemSUBJ.Replace("Type/Model", "\r\nType/Model:").Replace("Serial No.", "\r\nSerial No.:").Replace("Maker", "\r\nMaker:").Trim();
                        }

                        if (!string.IsNullOrEmpty(subjTypeStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + subjTypeStr.Trim();

                        if (!string.IsNullOrEmpty(subjMakerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + subjMakerStr.Trim();

                        if (!string.IsNullOrEmpty(subjSerialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + subjSerialStr.Trim();

                        if (!string.IsNullOrEmpty(subjNoteStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "COMPONENT NOTES: " + subjNoteStr.Trim();

                        if (!string.IsNullOrEmpty(descDrwStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG.NO: " + descDrwStr.Trim();


						if (_itemRmk != -1)
						{
							iTemRmk = dt.Rows[i][_itemRmk].ToString().Trim();

							int _i = i + 1;

							while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
							{
								iTemRmk = iTemRmk + " " + dt.Rows[_i][_itemRmk].ToString().Trim();

								_i += 1;

								if (_i >= dt.Rows.Count)
									break;
							}
						}



						if (!string.IsNullOrEmpty(iTemRmk))
							iTemDESC = iTemDESC + Environment.NewLine + iTemRmk.Trim();

                        


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit.ToUpper());
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemSUBJ = iTemSUBJ.Replace("DWG.NO: " + descDrwStr.Trim(), "").Trim();

                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;

                        descDrwStr = string.Empty;

                        subjNoteStr = string.Empty;
                        subjNameStr = string.Empty;
                        subjMakerStr = string.Empty;
                        subjTypeStr = string.Empty;
                        subjSerialStr = string.Empty;
                    }
                }
            }
        }
    }
}

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class SKShipping
    {
        string vessel;
        string reference;
        string partner;
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

        public string Partner
        {
            get
            {
                return partner;
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



        public SKShipping(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            partner = "";                       // 매출처담당자

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

            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;
            string iTemUniq = string.Empty;

            int itemCode = 0;
            int itemDescription = 0;
            int itemDrw = 0;
            int itemUnit = 0;
            int itemQt = 0;

            string machineryString = string.Empty;
            string makerString = string.Empty;
            string modelString = string.Empty;
            string serialString = string.Empty;
            string dwgString = string.Empty;


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

            string[] itemName = { };

            bool itemStart = false;

            foreach (DataTable dt in ds.Tables)
            {
                rowCount = dt.Rows.Count - 1;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dataValue = dt.Rows[i][0].ToString();
                    string dataSecValue = dt.Rows[i][1].ToString();


                    // 첫 컬럼 값
                    if (dataValue.Equals("No")) itemStart = true;

                    if (dataValue.StartsWith("Requisition"))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (string.IsNullOrEmpty(dt.Rows[i][c].ToString()) && string.IsNullOrEmpty(reference))
                            {
                                reference = dt.Rows[i][c].ToString().Trim();
                            }
                        }

                        if (string.IsNullOrEmpty(reference))
                        {
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                if (string.IsNullOrEmpty(dt.Rows[i + 1][c].ToString()) && string.IsNullOrEmpty(reference))
                                {
                                    reference = dt.Rows[i + 1][c].ToString().Trim();
                                }
                            }
                        }
                    }

                    //if (string.IsNullOrEmpty(partner))
                    //{
                    //    for (int c = 1; c < dt.Columns.Count; c++)
                    //    {
                    //        if (dt.Rows[i][c].ToString().Contains("P.I.C"))
                    //        {
                    //            partner = dt.Rows[i][c].ToString().Replace("P.I.C","").Replace(":","").Trim();
                    //        }
                    //    }

                    //    if (partner.Contains("("))
                    //    {
                    //        int idx_e = partner.IndexOf("(");
                    //        partner = partner.Substring(0, idx_e).Trim();
                    //    }
                    //}

     //               if(dataValue.Contains("Vessel Requisition"))
					//{
     //                   iTemUniq = string.Empty;
     //                   for (int cc = 1; cc < dt.Columns.Count; cc++)
     //                   {
     //                           iTemUniq = iTemUniq + dt.Rows[i][cc].ToString().Replace("Vessel Requisition", "").Replace("1)", "").Replace("2)", "").Replace(":", "").Trim();
     //                   }
     //               }

                    // No or Not No
                    if (!itemStart)
                    {

                          
                    }
                    else
                    {
                        //################# 순번, 주제, 품목코드, 품목명, 단위, 수량
                        string firstColString = dt.Rows[i][0].ToString();


                        if(firstColString.StartsWith("Equipment Information"))
						{
                            for(int c = 0; c < dt.Columns.Count; c++)
							{
                                iTemSUBJ = iTemSUBJ + " " + dt.Rows[i][c].ToString().Trim();
                            }


                            int _i = i + 1;

                            while(!dt.Rows[i][0].ToString().StartsWith("Requisition Manager"))
							{
                                for(int c = 0; c < dt.Columns.Count; c++)
								{
                                    iTemSUBJ = iTemSUBJ + dt.Rows[i][c].ToString().Trim();
								}

                                if (_i >= dt.Rows.Count - 1)
                                    break;
                                else
                                    _i += 1;

							}
						}
                        else if (firstColString.Equals("No"))
                        {
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                //if (dt.Rows[i][c].ToString() == "Detail Name") itemDescription = c;    //품목명
                                //else if (dt.Rows[i][c].ToString() == "P/N") itemCode = c;              //아이템코드
                                //else if (dt.Rows[i][c].ToString().Contains("DWG")) itemDrw = c;        //DWG NO
                                //else if (dt.Rows[i][c].ToString() == "Unit") itemUnit = c;             //단위
                                //else if (dt.Rows[i][c].ToString() == "Qty") itemQt = c;              //수량

                                if (dt.Rows[i][c].ToString() == "Item Name") itemDescription = c;    //품목명
                                else if (dt.Rows[i][c].ToString() == "Type") itemCode = c;              //아이템코드
                                //else if (dt.Rows[i][c].ToString().Contains("DWG")) itemDrw = c;        //DWG NO
                                //else if (dt.Rows[i][c].ToString() == "Unit") itemUnit = c;             //단위
                                else if (dt.Rows[i][c].ToString() == "Qty") itemQt = c;              //수량
                            }
                        }
                        else if (GetTo.IsInt(firstColString))
                        {
                            iTemNo = firstColString;
    

       //                     // row 값 가져와서 배열에 넣은후 값 추가하기
       //                     string[] rowValueSpl = new string[20];
       //                     int columnCount = 0;
       //                     for (int c = 0; c < dt.Columns.Count; c++)
       //                     {
       //                         if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
       //                         {
       //                             rowValueSpl[columnCount] = c.ToString();
       //                             columnCount++;
       //                         }
       //                     }


       //                     if (rowValueSpl[7] != null && rowValueSpl[8] == null)
       //                     {
       //                         itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
       //                         itemCode = Convert.ToInt16(rowValueSpl[3].ToString());
       //                         itemDrw = -1;
       //                         itemUnit = Convert.ToInt16(rowValueSpl[4].ToString());
       //                         itemQt = Convert.ToInt16(rowValueSpl[5].ToString());
       //                     }
       //                     else if (rowValueSpl[8] != null && rowValueSpl[9] == null)
       //                     {
       //                         itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
       //                         itemCode = Convert.ToInt16(rowValueSpl[3].ToString());
       //                         itemDrw = Convert.ToInt16(rowValueSpl[4].ToString());
       //                         itemUnit = Convert.ToInt16(rowValueSpl[5].ToString());
       //                         itemQt = Convert.ToInt16(rowValueSpl[6].ToString());
       //                     } 
       //                     else if (rowValueSpl[6] != null && rowValueSpl[7] == null)
							//{
       //                         itemCode = Convert.ToInt16(rowValueSpl[1].ToString());
       //                         itemDescription = Convert.ToInt16(rowValueSpl[2].ToString());
       //                         itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
       //                         itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
       //                     }


                            if(itemCode != -1)
                                iTemCode = dt.Rows[i][itemCode].ToString().Replace("TYPE:", "");

                            if(itemQt != -1)
                                iTemQt = dt.Rows[i][itemQt].ToString();
                            
                            if(itemUnit != -1)
                                iTemUnit = dt.Rows[i][itemUnit].ToString();
                            
                            if(itemDescription != -1)
                                iTemDESC = dt.Rows[i][itemDescription].ToString();

                            if (itemDrw != -1)
                            {
                                dwgString = dt.Rows[i][itemDrw].ToString().Replace("NONE", "").Replace("none", "").Trim();

                                int _i = i + 1;
                                while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                                {
                                    if (itemDrw != -1)
                                        dwgString = dwgString + dt.Rows[_i][itemDrw].ToString().Trim();

                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][itemDescription].ToString().Trim();
                                    iTemCode = iTemCode.Trim() + " " + dt.Rows[_i][itemCode].ToString().Trim();


                                    if (_i >= dt.Rows.Count-1)
                                        break;
                                    else
                                        _i += 1;
                                }
                            }

                            iTemSUBJ = machineryString;

                            if (!string.IsNullOrEmpty(modelString))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + modelString;

                            if (!string.IsNullOrEmpty(makerString))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerString;

                            if (!string.IsNullOrEmpty(serialString))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialString;

                            if (!string.IsNullOrEmpty(dwgString))
                                iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG: " + dwgString;

                            if (iTemCode.Contains("Page"))
                            {
                                int idx_s = iTemCode.IndexOf("Page");

                                if (idx_s != -1)
                                    iTemCode = iTemCode.Substring(0, idx_s).Trim();
                            }


                            if(iTemCode.ToUpper().Equals("NONE"))
							{
                                iTemCode = string.Empty;
							}

                            //ITEM ADD START
                            dtItem.Rows.Add();
                            dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                            if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                            if(!string.IsNullOrEmpty(iTemSUBJ))
                                dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;

                            iTemDESC = string.Empty;
                            iTemUnit = string.Empty;
                            iTemQt = string.Empty;
                            iTemCode = string.Empty;
                        }
                    }
                }
            }
        }
    }
}

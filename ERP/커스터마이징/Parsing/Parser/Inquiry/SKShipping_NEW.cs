using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class SKShipping_NEW
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



        public SKShipping_NEW(string fileName)
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

            int itemCode = -1;
            int itemDescription = -1;
            int itemDrw = -1;
            int itemUnit = -1;
            int itemQt = -1;
            int itemEq = -1;

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

            bool itemStart = false;

            // xml row 나누기
            string[] xmlSpl = { };

            if(!string.IsNullOrEmpty(xmlTemp))
			{
                xmlSpl = xmlTemp.Split(new string[] { "<row>" }, StringSplitOptions.None);
			}

            foreach (DataTable dt in ds.Tables)
            {
                rowCount = dt.Rows.Count - 1;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string dataValue = dt.Rows[i][0].ToString();
                    string dataSecValue = dt.Rows[i][1].ToString();


                    
                    // 첫 컬럼 값
                    if (dataValue.Equals("No")) itemStart = true;


                    if(dataValue.Contains(") V"))
					{
                        string uniqStr = string.Empty;
                           
                        for(int c=0; c < dt.Columns.Count; c++)
						{
                            uniqStr = uniqStr + dt.Rows[i][c].ToString();
						}

                        string[] uniqSpl = uniqStr.Split(':');

                        if (uniqSpl.Length > 2)
                        {
                            iTemUniq = uniqSpl[uniqSpl.Length - 1].ToString().Trim();
                        }
                        else if (uniqSpl.Length == 2)
                        {
                            iTemUniq = uniqSpl[1].ToString().Trim();
                        }

					}

					// No or Not No
					if (!itemStart)
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (string.IsNullOrEmpty(reference) && dt.Rows[i][c].ToString().StartsWith("Quotation No"))
							{
								reference = dt.Rows[i][c + 1].ToString().Trim();
							}

                            if (string.IsNullOrEmpty(vessel) && dt.Rows[i][c].ToString().StartsWith("Vessel"))
                            {
                                vessel = dt.Rows[i][c + 1].ToString().Trim();
                            }

                            if(string.IsNullOrEmpty(machineryString) && dt.Rows[i][c].ToString().StartsWith("Machinery"))
							{
                                machineryString = dt.Rows[i][c + 1].ToString().Trim();
							}

                            if (string.IsNullOrEmpty(makerString) && dt.Rows[i][c].ToString().StartsWith("Maker"))
                            {
                                makerString = dt.Rows[i][c + 1].ToString().Trim();
                            }

                            if (string.IsNullOrEmpty(modelString) && dt.Rows[i][c].ToString().StartsWith("Model/Type"))
                            {
                                modelString = dt.Rows[i][c + 1].ToString().Trim();
                            }

                            if (string.IsNullOrEmpty(serialString) && dt.Rows[i][c].ToString().StartsWith("Serial No"))
                            {
                                serialString = dt.Rows[i][c + 1].ToString().Trim();
                            }
                        }
					}
					else
                    {
                        //################# 순번, 주제, 품목코드, 품목명, 단위, 수량
                        string firstColString = dt.Rows[i][0].ToString();

                        if (firstColString.Equals("No"))
                        {
                            itemDescription = -1;
                            itemCode = -1;
                            itemUnit = -1;
                            itemDrw = -1;
                            itemQt = -1;
                            itemEq = -1;

                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString() == "Detail Name") itemDescription = c;    //품목명
                                else if (dt.Rows[i][c].ToString() == "P/N") itemCode = c;              //아이템코드
                                else if (dt.Rows[i][c].ToString().Contains("DWG")) itemDrw = c;        //DWG NO
                                else if (dt.Rows[i][c].ToString() == "Unit") itemUnit = c;             //단위
                                else if (dt.Rows[i][c].ToString() == "Qty") itemQt = c;              //수량
                                else if (dt.Rows[i][c].ToString() == "Equipment") itemEq = c;
                                else if (dt.Rows[i-1][c].ToString() == "Detail Name") itemDescription = c;    //품목명
                                else if (dt.Rows[i-1][c].ToString() == "P/N") itemCode = c;              //아이템코드
                                else if (dt.Rows[i-1][c].ToString().Contains("DWG")) itemDrw = c;        //DWG NO
                                else if (dt.Rows[i-1][c].ToString() == "Unit") itemUnit = c;             //단위
                                else if (dt.Rows[i-1][c].ToString() == "Qty") itemQt = c;              //수량
                                else if (dt.Rows[i-1][c].ToString() == "Equipment") itemEq = c;
                                else if (dt.Rows[i - 2][c].ToString() == "Detail Name") itemDescription = c;    //품목명
                                else if (dt.Rows[i - 2][c].ToString() == "P/N") itemCode = c;              //아이템코드
                                else if (dt.Rows[i - 2][c].ToString().Contains("DWG")) itemDrw = c;        //DWG NO
                                else if (dt.Rows[i - 2][c].ToString() == "Unit") itemUnit = c;             //단위
                                else if (dt.Rows[i - 2][c].ToString() == "Qty") itemQt = c;              //수량
                                else if (dt.Rows[i - 2][c].ToString() == "Equipment") itemEq = c;

                                if (itemQt != -1)
                                    break;
                            }
                        }
                        else if (GetTo.IsInt(firstColString))
                        {
                            iTemNo = firstColString;


                            if (itemUnit != -1)
                            {
                                iTemUnit = dt.Rows[i][itemUnit].ToString();

                                if (string.IsNullOrEmpty(iTemUnit))
                                    iTemUnit = dt.Rows[i - 1][itemUnit].ToString();

                                if(string.IsNullOrEmpty(iTemUnit))
                                    iTemUnit = dt.Rows[i - 2][itemUnit].ToString();
                            }


                            if (!string.IsNullOrEmpty(iTemUnit))
                            {
                                if (xmlTemp.Contains(iTemUnit))
                                {
                                    for (int r = 0; r < xmlSpl.Length; r++)
                                    {
                                        if (xmlSpl[r].ToString().Contains("<cell>"+iTemUnit+"</cell>")) // <cell>EA</cell>
                                        {
                                            string xmlRow_1 = xmlSpl[r].ToString();

                                            string[] xmlSplUnit = xmlRow_1.Split(new string[] { "<cell>" }, StringSplitOptions.None);

                                            iTemDESC = xmlSplUnit[2].ToString().Replace("</cell>", "").Replace("\r\n","").Replace("<cell />", "").Trim();
                                            iTemCode = xmlSplUnit[3].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Trim();
                                            dwgString = xmlSplUnit[4].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Trim();
                                            iTemUnit = xmlSplUnit[5].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Trim();
                                            iTemQt = xmlSplUnit[6].ToString().Replace("</cell>", "").Replace("\r\n", "").Replace("<cell />", "").Trim();


                                            xmlSpl[r] = "";
                                            break;
                                        }
                                    }
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
                                iTemDESC = iTemDESC.Trim() + Environment.NewLine + "DWG: " + dwgString;

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

			if (string.IsNullOrEmpty(dtItem.Rows[0]["DESC"].ToString()))
			{
                for(int line = 0; line < xmlSpl.Length; line++)
				{
                    if(xmlSpl[line].ToString().Contains("<cell rowspan=\"2\">") && !xmlSpl[line].ToString().Contains("Detail Name") && !xmlSpl[line].ToString().Contains("P.I.C"))
					{
                        string lineStr = xmlSpl[line].ToString().Replace("<cell />", "").Trim();

                        string[] xmlSplUnit = lineStr.Split(new string[] { "<cell>" }, StringSplitOptions.None);

                        if(xmlSplUnit.Length > 0)
						{
                            iTemDESC = xmlSplUnit[2].ToString().Replace("\r\n","").Replace("</cell>","").Trim();
                            iTemCode = xmlSplUnit[3].ToString().Replace("\r\n", "").Replace("</cell>", "").Trim();
                            iTemQt = xmlSplUnit[6].ToString().Replace("\r\n", "").Replace("</cell>", "").Trim();
                            iTemUnit = xmlSplUnit[5].ToString().Replace("\r\n", "").Replace("</cell>", "").Trim();
                            //itemDrw = xmlSplUnit[4].ToString().Replace("\r\n", "").Replace("</cell>", "").Trim();
                        }
                    }
				}
			}


		}
	}
}

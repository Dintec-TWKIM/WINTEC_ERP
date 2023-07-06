using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Scorpio_pdf
    {
        string vessel;
        string reference;
        DataTable dtItem;
        string contact;
        string imoNumber;

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

        public string Contact
        {
            get
            {
                return contact;
            }
        }

        public string Imonumber
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



        public Scorpio_pdf(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            contact = string.Empty;
            imoNumber = string.Empty;

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
            int _itemDesc = -1;
            int _itemQt = -1;
            int _itemUnit = -1;
            int _itemCode = -1;

            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;
            string iTemUniq = string.Empty;

            string subjStr = string.Empty;
            string componentStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string dwgStr = string.Empty;


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


            foreach (DataTable dt in ds.Tables)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string firstStr = dt.Rows[i][0].ToString();


					// 윤예영 20190926 요청건
					//vessel = dt.Rows[0][1].ToString().Trim();

					//if (i > 8)
					//{
					//	// row 값 가져와서 배열에 넣은후 값 추가하기
					//	string[] rowValueSpl = new string[20];
					//	int columnCount = 0;
					//	for (int c = 0; c < dt.Columns.Count; c++)
					//	{
					//		if (!string.IsNullOrEmpty(dt.Rows[i][c].ToString()))
					//		{
					//			rowValueSpl[columnCount] = c.ToString();
					//			columnCount++;
					//		}
					//	}


					//	if (rowValueSpl[2] != null && rowValueSpl[3] == null)
					//	{
					//		_itemDesc = Convert.ToInt16(rowValueSpl[0].ToString());
					//		_itemQt = Convert.ToInt16(rowValueSpl[1].ToString());
					//		_itemUnit = Convert.ToInt16(rowValueSpl[2].ToString());
					//	}
					//	else if (rowValueSpl[1] != null && rowValueSpl[2] == null)
					//	{
					//		_itemDesc = Convert.ToInt16(rowValueSpl[0].ToString());
					//		//_itemQt = Convert.ToInt16(rowValueSpl[1].ToString());
					//		_itemUnit = Convert.ToInt16(rowValueSpl[1].ToString());
					//	}
					//	else if (rowValueSpl[3] != null && rowValueSpl[4] == null)
					//	{
					//		_itemDesc = Convert.ToInt16(rowValueSpl[0].ToString());
					//		_itemQt = Convert.ToInt16(rowValueSpl[2].ToString());
					//		_itemUnit = Convert.ToInt16(rowValueSpl[3].ToString());
					//	}
					//	else if (rowValueSpl[0] != null && rowValueSpl[1] == null)
					//	{
					//		_itemDesc = -1;
					//		iTemSUBJ = dt.Rows[i][Convert.ToInt16(rowValueSpl[0].ToString())].ToString().Trim();
					//	}

					//	if (!_itemDesc.Equals(-1))
					//	{
					//		iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();
					//		if(_itemQt != -1)
					//			iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
					//		iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

					//		if (!iTemDESC.Equals("0"))
					//		{
					//			//ITEM ADD START
					//			dtItem.Rows.Add();
					//			dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstStr;
					//			dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
					//			dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
					//			if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
					//			// 주제가 없는 경우가 있음, 없을때는 FOR 제거
					//			if (!string.IsNullOrEmpty(iTemSUBJ))
					//				dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ.Trim();
					//			dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
					//			dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;

					//			iTemDESC = string.Empty;
					//			iTemUnit = string.Empty;
					//			iTemQt = string.Empty;
					//			iTemCode = string.Empty;
					//			_itemDesc = -1;
					//		}
					//	}

					//}


					if (string.IsNullOrEmpty(vessel))
                    {
                        for(int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Vessels"))
                                vessel = dt.Rows[i][c].ToString().Replace("Vessels", "").Replace(":", "").Trim();
                        }
                    }
					   
                    if (firstStr.StartsWith("Quotation"))
                        reference = firstStr.Replace("Quotation", "").Replace("#", "").Replace(".", "").Replace(":","").Trim();
                    else if (firstStr.Equals("No."))
                    {
                        for (int c = 1; c < dt.Columns.Count; c++)
                        {
                            if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;
                            else if (dt.Rows[i][c].ToString().Contains("Part No")) _itemCode = c;
                            else if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQt = c;
                            else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                        }

                        int _i = i + 1;

                        while(!GetTo.IsInt(dt.Rows[_i][0]))
                        {
                            for(int c = 0; c < dt.Columns.Count; c++)
                            {
                                if(!subjStr.EndsWith(Environment.NewLine))
                                    subjStr = subjStr + " " + dt.Rows[_i][c].ToString().Trim();
                                else
                                    subjStr = subjStr + dt.Rows[_i][c].ToString().Trim();
                            }

                            _i += 1;

                            if (_i >= dt.Rows.Count)
                                break;

                            subjStr = subjStr.Trim() + Environment.NewLine;
                        }
                    }
                    else if (GetTo.IsInt(firstStr))
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


						if (rowValueSpl[7] != null && rowValueSpl[8] == null)
						{
							_itemDesc = Convert.ToInt16(rowValueSpl[1].ToString());
							_itemQt = Convert.ToInt16(rowValueSpl[4].ToString());
							_itemUnit = Convert.ToInt16(rowValueSpl[5].ToString());
						//	_itemCode = Convert.ToInt16(rowValueSpl[2].ToString());
						}

						if (_itemDesc != -1)
                        {
                            iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                            int _i = i + 1;

                            while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                            {
                                for (int c = _itemDesc; c < dt.Columns.Count; c++)
                                {
                                    iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
                                }

                                _i += 1;

                                if (_i >= dt.Rows.Count)
                                    break;
                            }
                        }


                        if (_itemUnit != -1)
                            iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();


                        if (_itemQt != -1 && string.IsNullOrEmpty(iTemQt))
                            iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();


						if(iTemDESC.ToLower().Contains("part"))
						{
							int idx_s = iTemDESC.ToLower().IndexOf("part");

                            if (iTemDESC.Trim().ToLower().EndsWith("impa"))
                                iTemDESC = iTemDESC.ToLower().Replace("impa", "").ToString();
                                

							if (iTemDESC.ToLower().Contains("impa") && !iTemDESC.ToLower().EndsWith("impa"))
								idx_s = iTemDESC.ToLower().IndexOf("impa");

							iTemCode = iTemDESC.Substring(idx_s, iTemDESC.Length - idx_s).Replace("IMPA:", "").Replace("IMPA","").Replace("PART","").Replace(":","").Replace("#","").Replace("Part","").Replace("part","").Trim();


							idx_s = iTemDESC.ToLower().IndexOf("part");

							iTemDESC = iTemDESC.Substring(0, idx_s).Trim();
						}

                        //if (!string.IsNullOrEmpty(makerStr))
                        //    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();


                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstStr;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        // 주제가 없는 경우가 있음, 없을때는 FOR 제거
                        if (!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ.Trim();
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;

                        iTemDESC = string.Empty;
                        iTemUnit = string.Empty;
                        iTemQt = string.Empty;
                        iTemCode = string.Empty;
                        iTemSUBJ = string.Empty;




                    }
                }
            }
        }
    }
}

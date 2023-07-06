using Aspose.Email.Outlook;
using Dintec;
using Dintec.Parser;
using System.Linq;
using System;
using System.Data;

namespace Parsing
{
    class Fleet
    {
        string vessel;
        string reference;
        string contact;
        string imonumber;

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

        public string Contact
        {
            get
            {
                return contact;
            }
        }

        public string ImoNumber
        {
            get
            {
                return imonumber;
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


        public Fleet(string fileName)
        {
            vessel = "";                        // 선명
            reference = "";                     // 문의번호
            contact = "";
            imonumber = "";

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
            string iTemDesc = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            string bookingStr = string.Empty;
            string nameStr = string.Empty;
            string makerStr = string.Empty;
            string modelStr = string.Empty;
            string serialStr = string.Empty;


            MapiMessage msg = MapiMessage.FromFile(fileName);

            string mailBodyStr = msg.Body;

            // Vessel, Reference
            int idx_lts = mailBodyStr.IndexOf("\r\n\r\nShip\r\n\r\n");
            int idx_lte = mailBodyStr.IndexOf("Booking");

            if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
            {
                string vesselRef = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("발주 후", "").Replace(":", "").Trim();

                idx_lte = vesselRef.IndexOf("Department");

                string _vesselRef = vesselRef.Substring(0, idx_lte).Trim();

                string[] vesselSpl = _vesselRef.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

                if (vesselSpl.Length == 4)
                {
                    vessel = vesselSpl[1].ToString().Trim();
                    reference = vesselSpl[3].ToString().Trim();
                }
            }
            else if (idx_lts.Equals(-1))
            {
                idx_lte = -1;

                idx_lts = mailBodyStr.IndexOf("Requisition Ref.");
                idx_lte = mailBodyStr.IndexOf("Department");

                if(idx_lts != -1 && idx_lte != -1)
                    reference = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Department","").Replace("Requisition Ref.","").Trim();

                idx_lts = mailBodyStr.IndexOf("Ship");
                idx_lte = mailBodyStr.IndexOf("Requisition Ref.");

                if (idx_lts != -1 && idx_lte != -1)
                    vessel = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Department", "").Replace("Ship", "").Trim();
            }

            idx_lts = -1; idx_lte = -1;

            // Subject
            idx_lts = mailBodyStr.IndexOf("Booking Code");
            idx_lte = mailBodyStr.IndexOf("Spare Type");
            int idx_lte_gs = mailBodyStr.IndexOf("Date of Delivery");

            if(idx_lts == -1)
            {
                idx_lts = mailBodyStr.IndexOf("Description");
            }

            if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
            {
                string subjStr = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Trim();

                string[] subjStrSpl = subjStr.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

                if (subjStrSpl.Length == 10)
                {
                    if (subjStrSpl[0].ToString().Contains("Booking Code"))
                        bookingStr = subjStrSpl[1].ToString().Trim();

                    if (subjStrSpl[2].ToString().Contains("Description"))
                        nameStr = subjStrSpl[3].ToString().Trim();

                    if (subjStrSpl[4].ToString().Contains("Maker"))
                        makerStr = subjStrSpl[5].ToString().Trim();

                    if (subjStrSpl[6].ToString().Contains("Model"))
                        modelStr = subjStrSpl[7].ToString().Trim();

                    if (subjStrSpl[8].ToString().Contains("Serial No."))
                        serialStr = subjStrSpl[9].ToString().Trim();
                }
                else if (subjStrSpl.Length == 8)
                {
                    if (subjStrSpl[0].ToString().Contains("Description"))
                        nameStr = subjStrSpl[1].ToString().Trim();

                    if (subjStrSpl[2].ToString().Contains("Maker"))
                        makerStr = subjStrSpl[3].ToString().Trim();

                    if (subjStrSpl[4].ToString().Contains("Model"))
                        modelStr = subjStrSpl[5].ToString().Trim();

                    if (subjStrSpl[6].ToString().Contains("Serial No."))
                        serialStr = subjStrSpl[7].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(bookingStr))
                    iTemSUBJ = bookingStr.Trim();

                if (!string.IsNullOrEmpty(nameStr))
                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + nameStr.Trim();

                if (!string.IsNullOrEmpty(makerStr))
                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

                if (!string.IsNullOrEmpty(modelStr))
                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr.Trim();

                if (!string.IsNullOrEmpty(serialStr))
                    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();

                if (string.IsNullOrEmpty(iTemSUBJ))
                    iTemSUBJ = subjStr.Trim();
                
            }
            else if (!idx_lts.Equals(-1) && !idx_lte_gs.Equals(-1))
            {
                string subjStr = mailBodyStr.Substring(idx_lts, idx_lte_gs - idx_lts).Trim();

                string[] subjStrSpl = subjStr.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

                if (subjStrSpl.Length == 2)
                {
                    if (subjStrSpl[0].ToString().Equals("Description"))
                        nameStr = subjStrSpl[1].ToString().Trim();
                }
                else
                {
                    nameStr = subjStrSpl[0].ToString().ToLower().Replace("description", "").Trim();
                }



                if (!string.IsNullOrEmpty(nameStr))
                    iTemSUBJ = nameStr.Trim();
            }

            // IMO

            idx_lts = mailBodyStr.IndexOf("IMO Number");
            idx_lte = mailBodyStr.IndexOf("Yard Name");

            if(idx_lts != -1 && idx_lte != -1)
            {
                imonumber = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("IMO Number", "").Trim();
            }



            //Item
            idx_lts = mailBodyStr.IndexOf("Unit Price");
            idx_lte = mailBodyStr.IndexOf("Your Ref.");

            if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
            {
                if(idx_lts > idx_lte)
                {
                    idx_lte = mailBodyStr.IndexOf("Currency");
                }

                string itemStr = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Unit Price", "").Trim();

                //itemStr = itemStr.Replace("\t", "\r\n\r\n");
                //string itemStr2 = string.Empty;
                string itemStr3 = string.Empty;
				//itemStr2 = itemStr.Replace("\r\n", "\r\n\r\n");
				//itemStr2 = itemStr.Replace("\r\n", "\t");

				string[] descSpl = itemStr.Split(new string[] { "\r\n" }, StringSplitOptions.None);
				//string[] descSpl2 = itemStr2.Replace("\t  \t", "\t").Replace("\r\n","\r\n").Split(new string[] { "\t" }, StringSplitOptions.None);
				//string[] descSpl2 = itemStr2.Split(new string[] { "\t" }, StringSplitOptions.None);




				int itemRowNo = 1;
				string[] itemRowStr = new string[descSpl.Length];

				for (int r = 0; r < descSpl.Length; r++)
				{
					if (descSpl[r].ToString().StartsWith(Convert.ToString(itemRowNo) + "\t"))
					{

						itemRowStr[itemRowNo - 1] = descSpl[r].ToString();
                        itemStr3 = itemStr3 + "\t" + descSpl[r].ToString();
                        itemRowNo += 1;
					}
					else if (!string.IsNullOrEmpty(descSpl[r].ToString().Trim()))
					{
                        if (itemRowStr[itemRowNo - 2] != null)
                        {
                            int lineCountT = WordCheck(descSpl[r].ToString(), "\t");

                            if (lineCountT == 3)
                            {
                                itemRowStr[itemRowNo - 2] = itemRowStr[itemRowNo - 2] + "\t" + descSpl[r].ToString();
                                itemStr3 = itemStr3 + "\t" + descSpl[r].ToString();
                            }
                            else if (descSpl.Length == r + 1 && lineCountT == 1)
                            {
                                itemRowStr[itemRowNo - 2] = itemRowStr[itemRowNo - 2] + " " + descSpl[r].ToString();
                                itemStr3 = itemStr3 + " " + descSpl[r].ToString();
                            }
                            else if (descSpl.Length == r + 1)
                            {
                                itemRowStr[itemRowNo - 2] = itemRowStr[itemRowNo - 2] + "\t" + descSpl[r].ToString();
                                itemStr3 = itemStr3 + "\t" + descSpl[r].ToString();
                            }
                            else
                            {
                                itemRowStr[itemRowNo - 2] = itemRowStr[itemRowNo - 2] + " " + descSpl[r].ToString();
                                itemStr3 = itemStr3 + " " + descSpl[r].ToString();
                            }
                        }

					}
                    else
					{

					}
				}

                // 배열 공백 제거
                itemRowStr = itemRowStr.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                string[] descSpl3 = itemStr3.Split(new string[] { "\t" }, StringSplitOptions.None);

                descSpl3 = descSpl3.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();

                int cycleCount = descSpl3.Length / 3;
                int resultInt = descSpl3.Length % 3;

                if(resultInt.Equals(0))
				{
                    for (int c = 0; c < cycleCount; c++)
                    {
                        iTemNo = descSpl3[c * 3].Trim();
                        iTemDesc = descSpl3[(c * 3) + 1].Trim();


                        string[] qtSpl = descSpl3[(c * 3) + 2].Split(' ');

                        if (qtSpl.Length == 2)
                        {
                            iTemQt = qtSpl[0].Trim();
                            iTemUnit = qtSpl[1].Trim();
                        }
                        else if (qtSpl.Length == 1)
						{
                            if(GetTo.IsInt(qtSpl[0].ToString()))
							{
                                iTemQt = qtSpl[0].ToString();
							}
                            else
							{
                                iTemUnit = qtSpl[0].ToString();
							}
						}



                        if (iTemDesc.Contains("(") && iTemDesc.Contains(")") && iTemDesc.Contains("IMPA"))
                        {
                            int idx_s = iTemDesc.IndexOf("(");
                            int idx_e = iTemDesc.IndexOf(")");

                            if (idx_s != -1 && idx_e != -1)
                            {
                                iTemCode = iTemDesc.Substring(idx_s, idx_e - idx_s);

                                iTemCode = iTemCode.Replace("(", "").Replace(")", "").Replace("\r\n", "").Replace("IMPA", "").Replace("-","").Trim();
                            }

                        }

                        //ITEM ADD START
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDesc;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                        if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

                        iTemNo = string.Empty;
                        iTemDesc = string.Empty;
                        iTemUnit = string.Empty;
                        iTemQt = string.Empty;
                        iTemCode = string.Empty;
                    }
                }




    //            //int cycleCount = descSpl.Length / 5;
    //            //int resultInt = descSpl.Length % 5;

    //            int cycleCount = itemRowStr.Length / 5;
    //            int resultInt = itemRowStr.Length % 5;

    //            // \t 로 구분 : 단위와 갯수가 하나에 들어있음.
    //            int cycleCount2 = descSpl2.Length / 3;
				//int resultInt2 = descSpl2.Length % 3;

				//if (resultInt.Equals(0) && !itemRowStr[0].Contains("\t"))
    //            {
    //                for (int c = 0; c < cycleCount; c++)
    //                {
    //                    //iTemNo = descSpl[c * 5].Trim();
    //                    //iTemDesc = descSpl[(c*5) + 1].Trim();

    //                    //string[] qtSpl = descSpl[(c*5) + 2].Split(' ');



    //                    iTemNo = descSpl[c * 5].Trim();
    //                    iTemDesc = descSpl[(c * 5) + 1].Trim();

    //                    string[] qtSpl = descSpl[(c * 5) + 2].Split(' ');

    //                    if (qtSpl.Length == 2)
    //                    {
    //                        iTemQt = qtSpl[0].Trim();
    //                        iTemUnit = qtSpl[1].Trim();
    //                    }


    //                    if(iTemDesc.Contains("(") && iTemDesc.Contains(")") && iTemDesc.Contains("IMPA"))
				//		{
    //                        int idx_s = iTemDesc.IndexOf("(");
    //                        int idx_e = iTemDesc.IndexOf(")");

    //                        if (idx_s != -1 && idx_e != -1)
    //                        {
    //                            iTemCode = iTemDesc.Substring(idx_s, iTemDesc.Length - idx_e);

    //                            iTemCode = iTemCode.Replace("(", "").Replace(")", "").Replace("\r\n", "").Replace("IMPA", "").Trim();
    //                        }

    //                        //iTemCode = 
    //                    }


    //                    //ITEM ADD START
    //                    dtItem.Rows.Add();
    //                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
    //                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDesc;
    //                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
    //                    if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
    //                    dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
    //                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

    //                    iTemNo = string.Empty;
    //                    iTemDesc = string.Empty;
    //                    iTemUnit = string.Empty;
    //                    iTemQt = string.Empty;
    //                    iTemCode = string.Empty;
    //                }
    //            }
    //            else if (resultInt2.Equals(0))
    //            {
    //                for (int c = 0; c < cycleCount2; c++)
    //                {
    //                    iTemNo = descSpl2[c * 3].Trim();
    //                    iTemDesc = descSpl2[(c * 3) + 1].Trim();

    //  //                  if(!GetTo.IsInt(iTemNo.Trim()) && GetTo.IsInt(iTemDesc.Trim()))
				//		//{
    //  //                      c = c + 1;

    //  //                      iTemNo = descSpl2[c * 3].Trim();
    //  //                      iTemDesc = descSpl2[(c * 3) + 1].Trim();
    //  //                  }

    //                    string[] qtSpl = descSpl2[(c * 3) + 2].Split(' ');

    //                    if (qtSpl.Length == 2)
    //                    {
    //                        iTemQt = qtSpl[0].Trim();
    //                        iTemUnit = qtSpl[1].Trim();
    //                    }



    //                    if (iTemDesc.Contains("(") && iTemDesc.Contains(")") && iTemDesc.Contains("IMPA"))
    //                    {
    //                        int idx_s = iTemDesc.IndexOf("(");
    //                        int idx_e = iTemDesc.IndexOf(")");

    //                        if(idx_s != -1 && idx_e != -1)
				//			{
    //                            iTemCode = iTemDesc.Substring(idx_s, iTemDesc.Length - idx_e);

    //                            iTemCode = iTemCode.Replace("(", "").Replace(")", "").Replace("\r\n", "").Replace("IMPA","").Trim();
				//			}

    //                    }

    //                    //ITEM ADD START
    //                    dtItem.Rows.Add();
    //                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
    //                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDesc;
    //                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
    //                    if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
    //                    dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
    //                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

    //                    iTemNo = string.Empty;
    //                    iTemDesc = string.Empty;
    //                    iTemUnit = string.Empty;
    //                    iTemQt = string.Empty;
    //                    iTemCode = string.Empty;
    //                }
    //            }
                
            }
        }


        public int WordCheck(string String, string Word)
        {
            string[] StringArray = String.Split(new string[] { Word }, StringSplitOptions.None);

            return StringArray.Length - 1;
        }
    }
}

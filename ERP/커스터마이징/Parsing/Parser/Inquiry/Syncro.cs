using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class Syncro
    {
        string contact;
		string reference;
		string vessel;
		string imoNumber;
		DataTable dtItem;

		string fileName;
		UnitConverter uc;

		#region ==================================================================================================== Property

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

		public string Vessel
		{
			get
			{
				return vessel;
			}
		}

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

        public Syncro(string fileName)
		{
			contact = "";
			reference = "";
			vessel = "";
			imoNumber = "";

			dtItem = new DataTable();
			dtItem.Columns.Add("NO");
			dtItem.Columns.Add("SUBJ");
			dtItem.Columns.Add("ITEM");
			dtItem.Columns.Add("DESC");
			dtItem.Columns.Add("UNIT");
			dtItem.Columns.Add("QT");
            dtItem.Columns.Add("UNIQ");         //선사코드

			this.fileName = fileName;
			this.uc = new UnitConverter();
		}

		#endregion

		#region ==================================================================================================== Logic		

		public void Parse()
		{
            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            string remarkStr = string.Empty;

            string subjStr1 = string.Empty;
            string subjStr2 = string.Empty;
            string typeStr = string.Empty;
            string serialStr = string.Empty;
            string makerStr = string.Empty;
            string drawStr = string.Empty;
            string etcStr = string.Empty;

            string partnoDesc = string.Empty;
            string dwgnoDesc = string.Empty;
            string specDesc = string.Empty;
            string fileDesc = string.Empty;
            string typeDesc = string.Empty;
            string remarkDesc = string.Empty;

            int _itemdesc = 0;
            int _itemqt = 0;
            int _itemunit = 0;
            int _itemremark = 0;
            int _itemcode = 0;

			// 엑셀 읽기
			DataSet ds = ExcelReader.ToDataSet(fileName);
			DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴

			// ********** 문서 검색 모드
			// 선명
            vessel = dt.Rows[9][5].ToString().Trim();
			// 문의번호
            reference = dt.Rows[9][1].ToString().Replace("Inquiry No.", "").Replace(":","").Trim();

			// ********** 아이템 추가 모드
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				string firstColString = dt.Rows[i][0].ToString();
                string secondColStr = dt.Rows[i][1].ToString();


                //if (secondColStr.Contains("Inquiry No."))
                //{
                //    //reference = secondColStr.Replace("Inquiry No.", "").Replace(":", "").Trim();
                //    vessel = dt.Rows[i][5].ToString().Trim();
                //    reference = dt.Rows[i + 1][5].ToString().Trim();
                //    if (string.IsNullOrEmpty(vessel))
                //    {
                //        vessel = dt.Rows[i][6].ToString().Trim();
                //        reference = dt.Rows[i+1][6].ToString().Trim();
                //    }
                //}

                if (secondColStr.Contains("NAME OF MACHINERY"))
                    subjStr1 = secondColStr.Replace("NAME OF MACHINERY", "").Replace(":", "").Trim();

                if (secondColStr.Contains("NAME OF EQUIPMENT"))
                    subjStr2 = secondColStr.Replace("NAME OF EQUIPMENT", "").Replace(":", "").Trim();

                if(secondColStr.Contains("TYPE"))
                    typeStr = secondColStr.Replace("TYPE", "").Replace(":", "").Trim();

                if (secondColStr.Contains("DRAWING NO."))
                    drawStr = secondColStr.Replace("DRAWING NO.", "").Replace(":", "").Trim();

                if (secondColStr.Contains("MAKER"))
                    makerStr = secondColStr.Replace("MAKER", "").Replace(":", "").Trim();

                if (secondColStr.Contains("ETC"))
                    etcStr = secondColStr.Replace("ETC", "").Replace(":", "").Trim();

                if (secondColStr.Contains("SERIAL NO"))
                    serialStr = secondColStr.Replace("SERIAL NO.","").Replace(":", "").Trim();


                if(string.IsNullOrEmpty(reference))
				{
                    for(int c = 1; c < dt.Columns.Count; c++)
					{
                        if(dt.Rows[i][c].ToString().Contains("Inquiry No"))
						{
                            reference = dt.Rows[i][c].ToString().Replace("Inquiry No.", "").Replace(":","").Trim();
						}
					}
				}

                if(string.IsNullOrEmpty(vessel))
				{
                    for(int c = 1; c < dt.Columns.Count; c++)
					{
                        if(dt.Rows[i][c].ToString().Contains("Supply place :"))
						{
                            for(int cc = c+1; cc < dt.Columns.Count; cc++)
							{
                                if(!string.IsNullOrEmpty(dt.Rows[i][cc].ToString()))
								{
                                    vessel = dt.Rows[i][cc].ToString().Trim();
                                    break;
								}
							}
						}
					}
				}


                if (firstColString.Equals("NO"))
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Equals("DESCRIPTION") || dt.Rows[i][c].ToString().Contains("PART NAME")) _itemdesc = c;
                        else if (dt.Rows[i][c].ToString().Contains("IMPA NO")) _itemcode = c;
                        else if (dt.Rows[i][c].ToString().Equals("Q'TY")) _itemqt = c;
                        else if (dt.Rows[i][c].ToString().Equals("UNIT")) _itemunit = c;
                        else if (dt.Rows[i][c].ToString().Equals("REMARK")) _itemremark = c;
                    }
                }

                if (GetTo.IsInt(firstColString))
                {
                    iTemDESC = dt.Rows[i][_itemdesc].ToString().Trim();
                    iTemUnit = dt.Rows[i][_itemunit].ToString().Trim();
                    iTemQt = dt.Rows[i][_itemqt].ToString().Trim();
                    iTemCode = dt.Rows[i][_itemcode].ToString().Trim();

                    int _i = i+1;
                    while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
                    {
                        if (dt.Rows[_i][_itemdesc].ToString().Contains("Part No") || dt.Rows[_i][_itemdesc].ToString().Contains("Item no") || dt.Rows[_i][_itemdesc].ToString().Contains("ITEM NO"))
                            partnoDesc = dt.Rows[_i][_itemdesc].ToString().Replace("Part No","").Replace("Item no","").Replace("ITEM NO","").Replace(":","").Trim();

                        else if (dt.Rows[_i][_itemdesc].ToString().Contains("Dwg"))
                            dwgnoDesc = dt.Rows[_i][_itemdesc].ToString().Trim();

                        else if (dt.Rows[_i][_itemdesc].ToString().Contains("Spec"))
                        {
                            specDesc = dt.Rows[_i][_itemdesc].ToString().Trim();

                            if (!dt.Rows[_i+1][_itemdesc].ToString().StartsWith("Remark") && !GetTo.IsInt(dt.Rows[_i+1][0].ToString()))
                            {
                                specDesc = specDesc + dt.Rows[_i+1][_itemdesc].ToString().Trim();
                            }
                        }
                            

                        else if (dt.Rows[_i][_itemdesc].ToString().Contains("FILE NAME"))
                            fileDesc = dt.Rows[_i][_itemdesc].ToString().Trim();

                        else if (dt.Rows[_i][_itemdesc].ToString().Contains("TYPE") && !dt.Rows[_i][_itemdesc].ToString().Contains("Spec"))
                            typeDesc = dt.Rows[_i][_itemdesc].ToString().Trim();

                        else
                            iTemDESC = iTemDESC + Environment.NewLine + dt.Rows[_i][_itemdesc].ToString().Trim();

                        if (_i >= dt.Rows.Count - 1)
                            break;

                        

                          _i += 1;
                    }

                    if (!string.IsNullOrEmpty(partnoDesc))
                        iTemCode = partnoDesc.Replace(".","").Replace(",","").Trim();


                    // ITEMDESC 추가
                    if (!string.IsNullOrEmpty(specDesc))
                        iTemDESC = iTemDESC.Trim() + ", " + specDesc.Trim();
                    if (!string.IsNullOrEmpty(typeDesc))
                        iTemDESC = iTemDESC.Trim() + ", " + typeDesc.Trim();
                    if (!string.IsNullOrEmpty(fileDesc))
                        iTemDESC = iTemDESC.Trim() + ", " + fileDesc.Trim();
                    if (!string.IsNullOrEmpty(dwgnoDesc))
                        iTemDESC = iTemDESC.Trim() + ", " + dwgnoDesc.Trim();
                    //if (!string.IsNullOrEmpty(remarkDesc))
                    //    iTemDESC = iTemDESC + ", " + remarkDesc;


                    // ITEMSUBJ 추가
                    if (!string.IsNullOrEmpty(subjStr1))
                        iTemSUBJ = subjStr1;
                    if (!string.IsNullOrEmpty(subjStr2))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr2.Trim();
                    if (!string.IsNullOrEmpty(typeStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();
                    if (!string.IsNullOrEmpty(serialStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialStr.Trim();
                    if (!string.IsNullOrEmpty(drawStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DRAWING NO.: " + drawStr.Trim();
                    if (!string.IsNullOrEmpty(makerStr))
                        iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();


                    // 단위, 수량 컬럼 변경(경우대비)
                    if (GetTo.IsInt(iTemUnit))
                    { 
                        string valueChange = iTemUnit;
                        iTemUnit = iTemQt;
                        iTemQt = valueChange;
                    }


                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColString;
                    if(!string.IsNullOrEmpty(iTemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                    if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);

                    // 초기화(주제항목제외)
                    iTemCode = string.Empty;
                    iTemDESC = string.Empty;
                    iTemQt = string.Empty;
                    iTemUnit = string.Empty;

                    partnoDesc = string.Empty;
                    dwgnoDesc = string.Empty;
                    fileDesc = string.Empty;
                    specDesc = string.Empty;
                    remarkDesc = string.Empty;
                    typeDesc = string.Empty;
                }
			}
		}

		#endregion
    }
}

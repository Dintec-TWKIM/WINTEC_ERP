using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class ShipServMaerskLine
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

        public ShipServMaerskLine(string fileName)
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
			// Pdf를 엑셀로 변환해서 분석 (엑셀이 편함)
			string excelFile = PdfReader.ToExcel(fileName);
			DataSet ds = ExcelReader.ToDataSet(excelFile);
            
			// 시작
			string subject = "";
			bool addible = false;

            string subject1 = string.Empty;

            string itemSUBJ = string.Empty;
            string itemDESC = string.Empty;
            string itemCODE = string.Empty;
            string itemUNIT = string.Empty;
            string itemQT = string.Empty;
            string itemUNIQ = string.Empty;

            int _itemCodeInt = -1;
            int _itemQtInt = -1;
            int _itemUnitInt = -1;
            int _itemDescInt = -1;

            int idx_s;
            int idx_e;
           
            string descStr = string.Empty;
            string descStr2 = string.Empty;

            string makerStr = string.Empty;


		
			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string firstColString = dt.Rows[i][0].ToString();

					// ********** 문서 검색 모드
					if (!addible)
					{
						

						// ***** 담당자 정보
						if (firstColString.IndexOf("Contact:") == 0)
						{
							// 담당자
							idx_s = 8;
							idx_e = firstColString.IndexOf("\n", idx_s);
							contact = firstColString.Substring(idx_s, idx_e - idx_s).Trim();
						}
						// ***** 견적 정보
                        else if (firstColString.Contains("Subject:"))
						{
							// SUBJECT
                            idx_s = firstColString.IndexOf("Subject:", 0);
                            idx_e = firstColString.IndexOf("Vessel:", 0);


                            if (!idx_s.Equals(-1) && !idx_e.Equals(-1))
                            {
                                subject1 = firstColString.Substring(idx_s, idx_e - idx_s).Replace("Subject:", "").Trim();

                                // 문의번호
                                reference = firstColString.Substring(0, idx_s).Trim();
                            }

							// 선명
							idx_s = firstColString.IndexOf("Vessel:",0);
							idx_e = firstColString.IndexOf("Vessel No",0);

                            if (!idx_s.Equals(-1) && !idx_e.Equals(-1))
                            {
                                vessel = firstColString.Substring(idx_s, idx_e - idx_s).Replace("Vessel:", "").Trim();
                            }
							// IMO 번호
                            idx_s = firstColString.IndexOf("Vessel No.:", 0);

							if (idx_s > 0)
                                imoNumber = firstColString.Substring(idx_s).Replace("Vessel No.:", "").Trim();
						}
						// ***** 아이템 정보
						else if (firstColString.IndexOf("Line Items") == 0)
						{
							addible = true;
							continue;
						}	
					}
					// ********** 아이템 추가 모드
					else
					{
						// ***** 첫번째 글자가 No.(아이템 헤더) → 스킵						
						if (firstColString == "No.")
						{
                            for (int c = 1; c < dt.Columns.Count; c++)
                            {
                                if (dt.Rows[i][c].ToString().Contains("Part No")) _itemCodeInt = c;
                                else if (dt.Rows[i][c].ToString().Contains("Qty")) _itemQtInt = c;
                                else if (dt.Rows[i][c].ToString().Contains("Unit")) _itemUnitInt = c;
                                else if (dt.Rows[i][c].ToString().Contains("Description")) _itemDescInt = c;
                            }
						}
						// ***** 첫번째 글자가 문자면 → 서브젝트
                        else if (firstColString.Contains("Equipment Section:") || firstColString.Contains("Additional Item"))
						{

                            if (!string.IsNullOrEmpty(firstColString.Replace("Equipment Section:", "").Trim()))
                            {
                                subject = string.Empty;
                                subject = subject1.Trim() + Environment.NewLine + firstColString.Replace("Equipment Section:", "").Replace("Desc: Additional Item Information:", "").Trim();
                            }

                            subject = subject.Replace("Type: Vessel equipment", "\r\nMAKER: ").Replace("\\","").Trim();
                            subject = subject.Replace("\r\n\r\n", "\r\n");

                            idx_s = subject.IndexOf("MAKER:", 0);

                            if (!idx_s.Equals(-1))
                            {
                                makerStr = subject.Substring(idx_s, subject.Length - idx_s);

                                subject = subject.Replace(makerStr, "").Trim();

                                makerStr = makerStr.Replace("MAKER:", "").Trim();
                                
                                string codeStr = makerStr.Substring(0, 1);
                                string codeStr2 = makerStr.Substring(1, 1);

                                if (codeStr == codeStr2)
                                {
                                    makerStr = makerStr.Substring(1, makerStr.Length - 1).Trim();
                                }

                                if(!string.IsNullOrEmpty(makerStr))
                                    subject = subject.Trim() + ", MAKER: " + makerStr.Trim();
                            }
						}
						// ***** 첫번째 글자가 숫자면 → 아이템
						else if (GetTo.IsInt(firstColString))
						{
                            idx_s = -1;
                            idx_e = -1;

                            itemSUBJ = subject;
                            itemCODE = dt.Rows[i][_itemCodeInt].ToString().Replace("\n", "").Trim();
                            itemUNIT = dt.Rows[i][_itemUnitInt].ToString().Trim();
                            itemQT = dt.Rows[i][_itemQtInt].ToString().Trim();


                            if (!GetTo.IsInt(itemQT.Replace(".","")))
                            {
                                itemUNIT = dt.Rows[i][_itemUnitInt - 1].ToString().Trim();
                                itemQT = dt.Rows[i][_itemQtInt - 1].ToString().Trim();
                            }


                            itemDESC = dt.Rows[i][_itemDescInt].ToString().Trim();

                            if (string.IsNullOrEmpty(itemDESC))
                            {
                                itemDESC = dt.Rows[i][_itemDescInt - 1].ToString().Trim();
                            }

                            
                            
                            // 품목명
                            idx_s = itemDESC.IndexOf("Comments:", 0);
                            idx_e = itemDESC.IndexOf("Remark 4:", 0);

                            if (!idx_s.Equals(-1) && !idx_e.Equals(-1))
                                descStr = itemDESC.Substring(idx_s, idx_e - idx_s).Replace("Comments:", "").Replace("Remark 3:", "").Replace(";","").Trim();
                            else if (!idx_s.Equals(-1))
                                descStr = itemDESC.Substring(idx_s, itemDESC.Length - idx_s).Replace("Comments:", "").Replace("Remark 3:", "").Replace(";","").Trim();

                            if (string.IsNullOrEmpty(descStr))
                            {
                                descStr = itemDESC.Replace("Name:", "").Trim();
                            }

                            if (descStr.Contains("Remark 2"))
                            {
                                idx_s = 0;
                                idx_e = itemDESC.IndexOf("Comments:", 0);

                                descStr = itemDESC.Substring(0, idx_e).Replace("Name:", "").Trim() + ", " + descStr.Trim();
                            }


                            idx_s = itemDESC.IndexOf("Name 2:", 0);
                            idx_e = itemDESC.IndexOf("Comments:", 0);

                            if (!idx_s.Equals(-1) && !idx_e.Equals(-1))
                                descStr2 = itemDESC.Substring(idx_s, idx_e - idx_s).Replace("Name 2:","").Trim();


                            // 선사코드
                            idx_s = itemDESC.IndexOf("article", 0);
                            if (!idx_s.Equals(-1))
                                itemUNIQ = itemDESC.Substring(idx_s).Replace("article number:", "").Replace(";", "").Replace("article\nnumber:", "").Trim();


                            if (!string.IsNullOrEmpty(descStr2))
                                itemDESC = descStr.Trim() + ", " + descStr2.Trim();
                            else
                                itemDESC = descStr.Trim();



							dtItem.Rows.Add();
							dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = dt.Rows[i][0];
                            if(!string.IsNullOrEmpty(itemSUBJ))
                                dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + itemSUBJ.Trim();
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = itemCODE.Trim();
                            dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = itemQT.Trim();
                            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = itemDESC.Trim();
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(itemUNIT.Trim());
                            dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = itemUNIQ.Trim();


                            itemSUBJ = string.Empty;
                            itemCODE = string.Empty;
                            itemUNIT = string.Empty;
                            itemQT = string.Empty;
                            itemDESC = string.Empty;
                            itemUNIQ = string.Empty;

                            descStr = string.Empty;
                            descStr2 = string.Empty;


						}
						// ***** 첫번째 글자가 빈칸 → Part No., Description 조합 (페이지가 넘어갔을 때 이것만 있는 경우가 있음, FB16113133)
						else if (firstColString == "")
						{
							// 우선 종료 글자가 있는지 판단
							for (int j = 0; j < dt.Columns.Count; j++)
							{
								if (dt.Rows[i][j].ToString().IndexOf("Generated by ShipServ") == 0)
								{
									return;	// 완전 종료
								}
							}
						}
					}
				}
			}
		}

		#endregion
    }
}

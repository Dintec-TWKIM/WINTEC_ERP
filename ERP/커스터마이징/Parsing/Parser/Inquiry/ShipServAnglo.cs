using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class ShipServAnglo
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

		public ShipServAnglo(string fileName)
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

			string subjStr = string.Empty;

			// Pdf를 엑셀로 변환해서 분석 (엑셀이 편함)
			string excelFile = PdfReader.ToExcel(fileName);
			DataSet ds = ExcelReader.ToDataSet(excelFile);

		   
			// 시작
			string subject = "";
			bool addible = false;

			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string firstColString = dt.Rows[i][0].ToString();

					// ********** 문서 검색 모드
					if (!addible)
					{
						int idx_s;
						int idx_e;

						// ***** 담당자 정보
						if (firstColString.IndexOf("Contact:") == 0)
						{
							// 담당자
							idx_s = 8;
							idx_e = firstColString.IndexOf("\n", idx_s);
							contact = firstColString.Substring(idx_s, idx_e - idx_s).Trim();
						}
						// ***** 견적 정보
						else if (firstColString.IndexOf("RFQ Ref:") == 0)
						{

							subjStr = string.Empty;
							// SUBJECT
							idx_s = firstColString.IndexOf("Subject:", 0);
							idx_e = firstColString.IndexOf("Vessel:", 0);

							if (!idx_s.Equals(-1) && !idx_e.Equals(-1))
							{
								subjStr = firstColString.Substring(idx_s, idx_e - idx_s).Replace("Subject:", "").Trim();
							}
							// 문의번호
							idx_s = 8;
							idx_e = firstColString.IndexOf("\n", idx_s);
							if (!idx_s.Equals(-1) && !idx_e.Equals(-1))
							{
								reference = firstColString.Substring(idx_s, idx_e - idx_s).Trim();
							}
							// 선명
							idx_s = firstColString.IndexOf("Vessel:") + 7;
							idx_e = firstColString.IndexOf("\n", idx_s);

							if (idx_e > 0)
								vessel = firstColString.Substring(idx_s, idx_e - idx_s).Trim();
							else
								vessel = firstColString.Substring(idx_s).Trim();

							// IMO 번호
							idx_s = firstColString.IndexOf("Vessel No.:") + 11;

							if (idx_s > 0)
								imoNumber = firstColString.Substring(idx_s).Trim();
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
						else if (!GetTo.IsInt(firstColString) && firstColString.Length > 10)
						{
							subject = firstColString;
							subject = subject.Replace("Equipment Section:", "").Replace("For:", "").Replace("Man:", " \r\nMAKER:");
							subject = subject.Trim();

							if (!string.IsNullOrEmpty(subjStr))
							{
								subject = subjStr.Trim() + Environment.NewLine + subject.Trim();
								subjStr = string.Empty;
							}
						}
						// ***** 첫번째 글자가 숫자면 → 아이템
						else if (GetTo.IsInt(firstColString))
						{
							if (Convert.ToInt16(dt.TableName.ToString().Replace("Table", "")) > 1)
							{
								dtItem.Rows.Add();
								dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = dt.Rows[i][0];				// A컬럼 고정인듯
								dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = subject.Trim();
								dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = dt.Rows[i][2].ToString().Replace("\n", " ").Trim();		// C컬럼 고정인듯	

								// Qty 컬럼 : D(3), E(4), F(5) 중에 나오는듯 함
								int qtCol = 0;

								for (int j = 3; j < 5; j++)
								{
									if (GetTo.IsInt(dt.Rows[i][j].ToString().Replace(".0", "")))
									{
										dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = dt.Rows[i][j];
										qtCol = j;
										break;
									}
								}

								// Description 컬럼 : 뒤에서 부터 검색
								int descCol = 0;

								for (int j = dt.Columns.Count - 1; j > 4; j--)
								{
									if (dt.Rows[i][j].ToString() != "")
									{
										dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = dt.Rows[i][j].ToString().Replace("\n", "\r\n").Trim();
										descCol = j;
										break;
									}
								}


								// Unit 컬럼 : Qty와 Description 사이를 검색
								for (int j = qtCol + 1; j < descCol; j++)
								{
									if (dt.Rows[i][j].ToString() != "")
									{
										dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(dt.Rows[i][j]);
										break;
									}
								}
							}
							else
							{
								if(!_itemCodeInt.Equals(-1))
									itemCODE = dt.Rows[i][_itemCodeInt].ToString().Trim();

								if(!_itemUnitInt.Equals(-1))
									itemUNIT = dt.Rows[i][_itemUnitInt].ToString().Trim();
								
								if(!_itemQtInt.Equals(-1))
									itemQT = dt.Rows[i][_itemQtInt].ToString().Trim();

								if(!_itemDescInt.Equals(-1))
									itemDESC = dt.Rows[i][_itemDescInt].ToString().Trim();

								dtItem.Rows.Add();
								dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = dt.Rows[i][0];				// A컬럼 고정인듯
								dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = subject.Trim();
								dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = itemCODE.Trim();
								dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = itemDESC.Trim();
								dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = itemQT.Trim();
								dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = itemUNIT.Trim();

								itemDESC = string.Empty;
								itemCODE = string.Empty;
								itemQT = string.Empty;
								itemUNIT = string.Empty;
							}
						}
						// ***** 첫번째 글자가 빈칸 → Part No., Description 조합 (페이지가 넘어갔을 때 이것만 있는 경우가 있음, FB16113133)
						//else if (firstColString == "")
						//{
						//    // 우선 종료 글자가 있는지 판단
						//    for (int j = 0; j < dt.Columns.Count; j++)
						//    {
						//        if (dt.Rows[i][j].ToString().IndexOf("Generated by ShipServ") == 0)
						//        {
						//            return;	// 완전 종료
						//        }
						//    }

						//        // 종료글자가 없다면 이전 값의 연속이라고 판단
						//        if (dtItem.Rows.Count > 0)
						//        {
						//            string prev = "";
						//            string now = "";

						//            // Part No.
						//            prev = dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"].ToString();
						//            now = dt.Rows[i][2].ToString();

						//            if (now != "")
						//            {
						//                dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = prev + now;
						//            }


						//            // Description
						//            prev = dtItem.Rows[dtItem.Rows.Count - 1]["DESC"].ToString();

						//            for (int j = dt.Columns.Count - 1; j > 4; j--)
						//            {
						//                now = dt.Rows[i][j].ToString().Replace("\n", "\r\n");

						//                if (now != "")
						//                {
						//                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = prev + "\r\n" + now;
						//                    break;
						//                }
						//            }
						//        }
						//}
					}
				}
			}
		}

		#endregion
	}
}

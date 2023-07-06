using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class ShipServScorpio
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

		public ShipServScorpio(string fileName)
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
							// 문의번호
							idx_s = 8;
							idx_e = firstColString.IndexOf("\n", idx_s);
							reference = firstColString.Substring(idx_s, idx_e - idx_s).Trim();

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
							subject = subject.Trim() + " " + firstColString;
							subject = subject.Replace("Equipment Section:", "").Replace("For:", "").Replace("Man:", " \r\nMAKER:").Replace("Serial", "\r\nSerial").Replace("Type:", "\r\nType:").Replace("Desc:", "\r\nDesc:").Replace("DESC", "\r\nDESC:").Replace("desc", "\r\ndesc").Trim();
							subject = subject.Replace("CAPACITY", "\r\nCAPACITY").Replace("capacity", "\r\ncapacity").Replace("Capacity", "\r\ncapacity").Trim();

							if (subject.ToUpper().StartsWith("FOR"))
							{
								subject = subject.Replace("For", "").Replace("FOR", "").Replace("for", "").Trim();
							}

							subject = subject.Replace("\r\n\r\n", "\r\n").Trim();
						}
						// ***** 첫번째 글자가 숫자면 → 아이템
						else if (GetTo.IsInt(firstColString))
						{
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

							if ((rowValueSpl[5] != null) && !firstColString.Equals("1"))
							{
								_itemCodeInt = Convert.ToInt16(rowValueSpl[2].ToString());
								_itemQtInt = Convert.ToInt16(rowValueSpl[3].ToString());
								_itemUnitInt = Convert.ToInt16(rowValueSpl[4].ToString());
								_itemDescInt = Convert.ToInt16(rowValueSpl[5].ToString());
							}
							else if (rowValueSpl[5] == null && rowValueSpl[4] != null && !firstColString.Equals("1"))
							{
								if (string.IsNullOrEmpty(dt.Rows[i + 1][0].ToString()) && !firstColString.Equals("1") && dt.Rows.Count > i + 3)
								{
									string rowValue = string.Empty;
									for (int c = 0; c < dt.Columns.Count; c++)
									{
										rowValue = " " + dt.Rows[i + 1][c].ToString();
										rowValue = rowValue.Trim();
									}

									if (string.IsNullOrEmpty(rowValue))
									{
										_itemCodeInt = -1;
										_itemQtInt = Convert.ToInt16(rowValueSpl[2].ToString());
										_itemUnitInt = Convert.ToInt16(rowValueSpl[3].ToString());
										_itemDescInt = Convert.ToInt16(rowValueSpl[4].ToString());
									}
								}
                                else if (string.IsNullOrEmpty(dt.Rows[i][_itemDescInt].ToString()))
                                {
                                    _itemCodeInt = Convert.ToInt16(rowValueSpl[2].ToString());
                                    _itemQtInt = Convert.ToInt16(rowValueSpl[3].ToString());
                                    _itemUnitInt = Convert.ToInt16(rowValueSpl[4].ToString());
                                    _itemDescInt = -1;
                                }
                                else
                                {
                                    //       NO / Part Type / Qty / Unit / Description
                                    //       0  /      1    /  2   /  3 /  4
                                    _itemCodeInt = -1;
                                    _itemQtInt = Convert.ToInt16(rowValueSpl[2].ToString());
                                    _itemUnitInt = Convert.ToInt16(rowValueSpl[3].ToString());
                                    _itemDescInt = Convert.ToInt16(rowValueSpl[4].ToString());
                                }
							}
                            else if (rowValueSpl[3] != null && rowValueSpl[4] == null)
                            {
                                _itemCodeInt = -1;
                                _itemQtInt = Convert.ToInt16(rowValueSpl[1].ToString());
                                _itemUnitInt = Convert.ToInt16(rowValueSpl[2].ToString());
                                _itemDescInt = Convert.ToInt16(rowValueSpl[3].ToString());
                            }


							if(!_itemCodeInt.Equals(-1))
                                itemCODE = dt.Rows[i][_itemCodeInt].ToString().Replace("\n", "").Trim();

							if(!_itemUnitInt.Equals(-1))
								itemUNIT = dt.Rows[i][_itemUnitInt].ToString().Trim();

							if(!_itemQtInt.Equals(-1))
								itemQT = dt.Rows[i][_itemQtInt].ToString().Trim();

							if(!_itemDescInt.Equals(-1))
								itemDESC = dt.Rows[i][_itemDescInt].ToString().Trim();

							if (itemUNIT.Equals("Piece(PCE)"))
								itemUNIT = "PCS";

							dtItem.Rows.Add();
							dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = dt.Rows[i][0];				// A컬럼 고정인듯
							dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + subject.Trim();
							dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = itemCODE.Trim();
							dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = itemDESC.Trim();
							dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = itemQT.Trim();
							dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = itemUNIT.Trim();

							itemDESC = string.Empty;
							itemCODE = string.Empty;
							itemQT = string.Empty;
							itemUNIT = string.Empty;
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

								// 종료글자가 없다면 이전 값의 연속이라고 판단
								if (dtItem.Rows.Count > 0)
								{
									string prev = "";
									string now = "";

									// Part No.
									prev = dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"].ToString();
									now = dt.Rows[i][2].ToString();

									if (now != "")
									{
										dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = prev + now;
									}


									// Description
									prev = dtItem.Rows[dtItem.Rows.Count - 1]["DESC"].ToString();

									for (int j = dt.Columns.Count - 1; j > 4; j--)
									{
										now = dt.Rows[i][j].ToString().Replace("\n", "\r\n");

										if (now != "")
										{
											dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = prev + "\r\n" + now;
											break;
										}
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

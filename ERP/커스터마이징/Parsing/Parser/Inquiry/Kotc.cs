using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	public class Kotc
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

		#endregion

		#region ==================================================================================================== Constructor

		public Kotc(string fileName)
		{
			vessel = "";
			reference = "";
			
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
			//// Pdf를 Xml로 변환해서 분석 (1000$ 짜리로 해야함, 500$ 짜리로 하면 Description 부분에 CRLF가 안됨)

			//// 1. 우선 500$ 짜리로 Xml 변환함 (1000$ 짜리의 경우 도면이 붙어 있으면 시간이 엄청 오래 걸림)
			//string xmlTemp = PdfReader.ToXml(fileName);

			//// 2. 도면을 제외한 Page 카운트 가져오기
			//int pageCount = xmlTemp.Count("<page>");

			//// 3. 앞서 나온 Page를 근거로 파싱 시작			
			//string xml = string.Empty;//PdfReader.GetXml(fileName, 1, pageCount);			
			//DataSet ds = PdfReader.ToDataSet(xml);

			//// 시작
			string subject = "";
			bool itemStart = false;
			////bool systemStart = false;

			string subjStr = string.Empty;
			string subjStr2 = string.Empty;

			string rowStr2 = string.Empty;

			string iTemSubj = string.Empty;
			string itemQt = string.Empty;
			string itemDesc = string.Empty;
            string iTemCode = string.Empty;
            string iTemUnit = string.Empty;

			int noCol = -1;
			int itemCol = -1;
			int descCol = -1;
			int unitCol = -1;
			int qtCol = -1;
            

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
                    string firstColNoStr = dt.Rows[i][0].ToString().Replace(" ","");
                    string[] noSpl = null;

                    if (firstColNoStr.Contains("."))
                    {
                        noSpl = firstColNoStr.Split('.');

                        if (noSpl.Length == 2)
                        {
                            firstColNoStr = noSpl[0].ToString().Replace(".", "").Trim();
                            iTemCode = noSpl[1].ToString().Trim();
                        }
                    }



					if(string.IsNullOrEmpty(vessel))
					{
						for(int c = 1; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().StartsWith("Vessel"))
								vessel = dt.Rows[i][c].ToString().Replace("Vessel", "").Replace(":", "").Trim();
						}
					}

					if (firstColString.IndexOf("Produced on:") >= 0) continue;	// Page 넘김 글자는 스킵
					//if (firstColString.IndexOf("Form Contains") >= 0) return;	// 아이템 종료 글자
					if (firstColString.StartsWith("No.")) itemStart = true;	// 아이템 추가 모드 시작

					// ********** 문서 검색 모드
					if (!itemStart)
					{
						for (int j = 0; j < dt.Columns.Count; j++)
						{
							string text = dt.Rows[i][j].ToString();

							// ***** 선명
							if (text.IndexOf("Vessel: ") >= 0)
							{
								vessel = text.Replace("Vessel: ", "");
								break;
							}
							// ***** 문의번호
							else if (text.IndexOf("Req. Code/Date: ") >= 0)
							{
								reference = text.Replace("Req. Code/Date: ", "");
								if (reference.Right(1) == "/") reference = reference.Substring(0, reference.Length - 1);
								reference = reference.Trim();
								break;
							}
						}
					}
					// ********** 아이템 추가 모드
					else
					{
						string rowString = "";

						for (int j = 0; j < dt.Columns.Count; j++)
						{
							rowString += " " + dt.Rows[i][j];
							rowString = rowString.Trim();
						}

						// ***** 첫번째 글자가 No.(아이템 헤더) → 컬럼 위치 설정
						if (firstColString.StartsWith("No."))
						{
							for (int j = 0; j < dt.Columns.Count; j++)
							{
								noCol = 0;

								// KOTC
								if (dt.Rows[i][j].ToString() == "Item") itemCol = j;
								else if (dt.Rows[i][j].ToString() == "Product Description") descCol = j;
								else if (dt.Rows[i][j].ToString() == "Packing") unitCol = j;
								else if (dt.Rows[i][j].ToString() == "Req.") qtCol = j;

								// OCEANGOLD TANKERS
								//else if (dt.Rows[i][j].ToString() == "ITEM CODE") COL_ITEM = j;
								//else if (dt.Rows[i][j].ToString() == "QTY.") COL_QT = j;
							}

							if (!descCol.Equals(-1))
							{
								if (itemCol.Equals(-1))
									itemCol = descCol - 1; 
							}
						}
						// ***** 서브젝트1
						else if (rowString.IndexOf("System:") >= 0)
						{
							//systemStart = true;

							//subject = "";
							//subject = "FOR " + rowString.Replace("System:", "").Trim();
							//subject = subject.Substring(0, subject.IndexOf("[")).Trim();

							subjStr = rowString.Replace("System:", "").Trim();


							int _i = i + 1;
							while (!dt.Rows[_i][0].ToString().Contains("Subsystem:") && !dt.Rows[_i][1].ToString().Contains("Subsystem:") && !GetTo.IsInt(dt.Rows[_i][0].ToString()))
							{
								rowStr2 = string.Empty;

								for (int j = 0; j < dt.Columns.Count; j++)
								{
									rowStr2 += " " + dt.Rows[_i][j];
									rowStr2 = rowStr2.Trim();
								}

								subjStr = subjStr.Trim() + Environment.NewLine + rowStr2.Trim();

								_i += 1;

								if (_i >= dt.Rows.Count)
									break;
							}
						}
						// ***** 서브젝트2 → Subsystem이 단독으로 오는 경우만 서브젝트 재구성 (System 아래에 Subsystem이 오는 경우는 무시)
//						else if (rowString.IndexOf("Subsystem:") >= 0 && !systemStart)
						else if (rowString.IndexOf("Subsystem:") >= 0)
						{
							//subject = "";
							//subject = "FOR " + rowString.Replace("Subsystem:", "").Trim();
							//subject = subject.Substring(0, subject.IndexOf("[")).Trim();

							subjStr2 = rowString.Replace("Subsystem:", "").Trim();

							int _i = i + 1;
							while (!GetTo.IsInt(dt.Rows[_i][0].ToString()))
							{
								rowStr2 = string.Empty;

								for (int j = 0; j < dt.Columns.Count; j++)
								{
									rowStr2 += " " + dt.Rows[_i][j];
								}

								subjStr2 = subjStr2.Trim() + Environment.NewLine + rowStr2.Trim();

								_i += 1;

								if (_i >= dt.Rows.Count)
									break;
							}

							subjStr2 = subjStr2.Replace("Maker: N/A", "").Replace("Maker : N/A", "").Trim();
						}
						// ***** 첫번째 글자가 서브젝트 관련 → 기존 서브젝트에 합치기
						else if (rowString.IndexOf("Particulars :") >= 0)
						{
							rowString = rowString.Replace("Particulars :", "").Trim();
							if (rowString != "") subject += "\r\n" + rowString;
						}
						// ***** 첫번째 글자가 서브젝트 관련 → 기존 서브젝트에 합치기
						else if (rowString.IndexOf("Maker :") >= 0)
						{
							if (rowString.Contains("N/A"))
								rowString = string.Empty;
							else
							{
								if (rowString.Contains("Serial No"))
								{
									rowString = rowString.Replace("Serial No :", "");
								}

								rowString = rowString.Replace("Maker :", "").Trim();
								if (rowString.IndexOf("** NOT AVAILABLE **") == -1) subject += "\r\n" + "MAKER : " + rowString;
							}
						}
						// ***** 첫번째 글자가 숫자 → 아이템
						else if ((GetTo.IsInt(firstColString.Replace(".","")) && firstColString.Replace(".","").Length <= 2) || GetTo.IsInt(firstColNoStr))
						{

							if(descCol != -1)
								itemDesc = dt.Rows[i][descCol].ToString().Trim();

							if(qtCol != -1)
								itemQt = dt.Rows[i][qtCol].ToString().Trim();


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
                                    itemCol = Convert.ToInt16(rowValueSpl[0].ToString());
                                    descCol = Convert.ToInt16(rowValueSpl[1].ToString());
                                    //_itemDwg = Convert.ToInt16(rowValueSpl[4].ToString());
                                    unitCol = Convert.ToInt16(rowValueSpl[2].ToString());
                                    qtCol = Convert.ToInt16(rowValueSpl[4].ToString());
                                }
                                else if (rowValueSpl[5] != null && rowValueSpl[6] == null)
                                {
									itemCol = Convert.ToInt16(rowValueSpl[1].ToString());
                                    descCol = Convert.ToInt16(rowValueSpl[2].ToString());
                                    unitCol = Convert.ToInt16(rowValueSpl[3].ToString());
                                    qtCol = Convert.ToInt16(rowValueSpl[5].ToString());
                                }
                                else if (rowValueSpl[6] != null && rowValueSpl[7] == null)
                                {
                                    descCol = Convert.ToInt16(rowValueSpl[1].ToString());
                                    unitCol = Convert.ToInt16(rowValueSpl[4].ToString());
                                    qtCol = Convert.ToInt16(rowValueSpl[6].ToString());
                                }



							if (itemCol != -1)
							{
								iTemCode = dt.Rows[i][itemCol].ToString().Trim();

								int _r = i + 1;

								while(string.IsNullOrEmpty(dt.Rows[_r][0].ToString()))
								{
									iTemCode = iTemCode + "/" + dt.Rows[_r][itemCol].ToString().Trim();

									_r += 1;

									if (_r >= dt.Rows.Count)
										break;
								}

								if (iTemCode.Contains(".") && iTemCode.Contains(" "))
								{
									string[] codeSpl = iTemCode.Split(' ');

									if (codeSpl.Length == 2)
									{
										iTemCode = codeSpl[1].ToString().Trim();
									}
								}
							}


							string item = "";
							

                            if (noSpl != null && noSpl.Length > 1)
                            {
                                item = noSpl[1].ToString().Trim();
                            }

                            if(string.IsNullOrEmpty(item))
                                for (int j = itemCol; j < descCol; j++) item += dt.Rows[i][j].ToString();

							// Item 컬럼은 유동적으로 변함
							


							if(!string.IsNullOrEmpty(subjStr))
								iTemSubj = subjStr.Trim();

							if (!string.IsNullOrEmpty(subjStr2))
								iTemSubj = iTemSubj.Trim() + Environment.NewLine + subjStr2.Trim();

							if (!string.IsNullOrEmpty(subjStr))
								iTemSubj = subjStr.Trim();

							iTemSubj = iTemSubj.Trim();
							itemQt = dt.Rows[i][qtCol].ToString().Trim();

							if (string.IsNullOrEmpty(itemQt))
							{
								if (GetTo.IsInt(dt.Rows[i][qtCol - 1].ToString()))
								{
									itemQt = dt.Rows[i][qtCol - 1].ToString().Trim();
								}
								else if (GetTo.IsInt(dt.Rows[i][qtCol + 1].ToString()))
								{
									itemQt = dt.Rows[i][qtCol + 1].ToString().Trim();
								}

								if (itemQt.Equals("0"))
								{
									itemQt = dt.Rows[i][qtCol + 1].ToString().Trim();
								}
							}

                            for (int c = descCol; c < unitCol; c++)
                            {
								if(!itemDesc.Contains(dt.Rows[i][c].ToString()))
									itemDesc = itemDesc.Trim() + " " + dt.Rows[i][c].ToString().Trim();
                            }


							int _i = i + 1;
							while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
							{
								if (dt.Rows[_i][descCol].ToString().StartsWith("ATTACHED"))
								{
									break;
								}
								else
								{
									for (int c = 0; c < dt.Columns.Count; c++)
									{
										itemDesc = itemDesc.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
									}
								}

								_i +=1;

								if (_i >= dt.Rows.Count)
									break;
							}

                            if (itemDesc.Contains("IMPA CODE:"))
                            {
                                int idx_s = itemDesc.IndexOf("IMPA CODE");

                                if(idx_s != -1)
                                    iTemCode = itemDesc.Substring(idx_s, 16).Replace("IMPA CODE:", "").Trim();

                            }
                            else if (itemDesc.Contains("IMPA CODE"))
                            {
                                int idx_s = itemDesc.IndexOf("IMPA CODE");

                                if (idx_s != -1)
                                    iTemCode = itemDesc.Substring(idx_s, 16).Replace("IMPA CODE", "").Trim();
                            }
                            else if (itemDesc.Contains("IMPA") && !itemDesc.Contains("IMPACT") && !itemDesc.Contains("IMPA-"))
                            {
                                int idx_s = itemDesc.IndexOf("IMPA");

                                if (idx_s != -1)
                                    iTemCode = itemDesc.Substring(idx_s, 11).Replace("IMPA", "").Trim();
                            }


                            if (string.IsNullOrEmpty(iTemCode))
                            {
                                if (itemCol != -1)
                                    iTemCode = dt.Rows[i][itemCol].ToString().Trim();
                            }


							if (iTemCode.Replace(".", "").Trim().Length < 2)
								iTemCode = string.Empty;


							// DataTable에 삽입
							dtItem.Rows.Add();
							dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColNoStr;
							if(!string.IsNullOrEmpty(iTemSubj))
								dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSubj;
                            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
							dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = itemDesc;
							dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(dt.Rows[i][unitCol]);
                            if(GetTo.IsInt(itemQt)) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = itemQt;
                            

							//dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = dt.Rows[i][5].ToString().Trim();

							itemQt = string.Empty;
							itemDesc = string.Empty;
                            noSpl = null;
                            iTemCode = string.Empty;


							//// 아이템 글자가 긴 경우 Description이랑 붙어버리는 경우가 있음 (FB17007598,FB16128539 등등 많음), Description에는 공백이 들어감
							//string desc = dt.Rows[i][descCol].ToString();


							//if (desc == "")
							//{
							//    int idx = item.IndexOf("/");

							//    for (int j = idx + 1; j < item.Length - 1; j++)
							//    {
							//        char c = item[j];

							//        if (!GetTo.IsInt(c))
							//        {
							//            dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = item.Substring(0, j).Trim();
							//            dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = item.Substring(j).Trim();
							//            break;
							//        }
							//    }
							//}
						}
						// ***** 첫번째 글자가 빈칸 → Description 합치기
						else if (firstColString == "" && dt.Rows[i][descCol].ToString() != "")
						{
							if (dtItem.Rows.Count > 0)
							{
								//string prev = dtItem.Rows[dtItem.Rows.Count - 1]["DESC"].ToString();
								//string text = dt.Rows[i][descCol].ToString();

								//// Item 컬럼에 TEL, FAX, EMAIL로 시작하면 무시
								//if (text.IndexOf("TEL") == 0 || text.IndexOf("FAX") == 0 || text.IndexOf("EMAIL") == 0) continue;

								//// 합치기
								//if (text != "") dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = prev + "\r\n" + text;
							}
						}
					}
				}
			}
		}

		#endregion
	}
}

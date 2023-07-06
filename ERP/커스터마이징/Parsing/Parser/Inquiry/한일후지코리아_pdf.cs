using Dintec;
using Dintec.Parser;
using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parsing.Parser.Inquiry
{
	class 한일후지코리아_pdf
	{
		string vessel;
		string reference;
		string imoNumber;
		DataTable dtItem;

		string fileName;


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

		public string ImoNumber
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



		public 한일후지코리아_pdf(string fileName)
		{
			vessel = "";                        // 선명
			reference = "";                     // 문의번호
			imoNumber = "";

			dtItem = new DataTable();
			dtItem.Columns.Add("NO");           // 순번
			dtItem.Columns.Add("SUBJ");         // 주제
			dtItem.Columns.Add("ITEM");         // 품목코드
			dtItem.Columns.Add("DESC");         // 품목명
			dtItem.Columns.Add("UNIT");         // 단위
			dtItem.Columns.Add("QT");           // 수량
			dtItem.Columns.Add("UNIQ");           // 수량
			this.fileName = fileName;
		}

		public void Parse()
		{
			string iTemNo = string.Empty;
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;

			int _itemDesc = -1;
			int _itemCode = -1;
			int _itemQt = -1;

			string subjStr = string.Empty;

			bool itemStart = false;

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


			//Regex regex = new Regex(@"^[a-z]*[0-9]\.\s*");
			Regex regex = new Regex(@".+\. .+");

			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string firstColStr = dt.Rows[i][0].ToString().ToLower();
					string secondColStr = dt.Rows[i][1].ToString().ToLower();


					if (firstColStr.StartsWith("담당자 :")) break;




					if (!itemStart)
					{
						if (string.IsNullOrEmpty(reference))
						{
							for (int c = 1; c < dt.Columns.Count; c++)
							{
								if (dt.Rows[i][c].ToString().StartsWith("서류번호"))
								{
									reference = dt.Rows[i][c + 1].ToString().Trim();

									if (string.IsNullOrEmpty(reference))
									{
										reference = dt.Rows[i][c + 2].ToString().Trim();
									}
								}

							}
						}

						if (firstColStr.StartsWith("선"))
						{
							for (int c = 0; c < dt.Columns.Count; c++)
							{
								if (string.IsNullOrEmpty(vessel))
									vessel = dt.Rows[i][c].ToString().Replace("선", "").Replace("명", "").Trim();
								else
									break;
							}

							if(!string.IsNullOrEmpty(vessel) && vessel.Contains("(") && vessel.Contains(")"))
							{
								int idx_s = vessel.IndexOf("(");
								int idx_e = vessel.IndexOf(")");

								if(idx_s != -1 && idx_e != -1)
								{
									imoNumber = vessel.Substring(idx_s, idx_e - idx_s).Trim();
									imoNumber = imoNumber.Replace(")","").Replace("(","").Trim();
									vessel = vessel.Substring(0, idx_s).Trim();
								}	

							}

							vessel = vessel.Replace("전화번호", "").Trim();
						}
						else if (firstColStr.StartsWith("no."))
						{
							itemStart = true;

							string lineStr = string.Empty;
							for (int c = 0; c < dt.Columns.Count; c++)
							{
								lineStr = dt.Rows[i][c].ToString().ToLower().Trim();

								if (lineStr.Contains("description")) _itemDesc = c;
								else if (lineStr.StartsWith("code")) _itemCode = c;
								else if (lineStr.StartsWith("q'ty")) _itemQt = c;
							}



							int _i = i + 1;


							subjStr = string.Empty;

							while (!regex.IsMatch(dt.Rows[_i][0].ToString()) || (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && !GetTo.IsInt(dt.Rows[_i][0].ToString().Substring(0, 1))))
							{

								subjStr = subjStr + Environment.NewLine + dt.Rows[_i][0].ToString().Trim() + dt.Rows[_i][1].ToString().Trim();

								_i += 1;
								i += 1;

								if (_i >= dt.Rows.Count)
									break;

								if (regex.IsMatch(dt.Rows[_i][0].ToString()))
								{
									if (!string.IsNullOrEmpty(dt.Rows[_i][0].ToString()) && GetTo.IsInt(dt.Rows[_i][0].ToString().Substring(0, 1).Trim()))
										break;
								}


							}


						}
					}
					else // 아이템 라인 시작
					{
						if (!string.IsNullOrEmpty(firstColStr))
						{
							if (regex.IsMatch(firstColStr))
							{
								if (!string.IsNullOrEmpty(firstColStr) && !GetTo.IsInt(firstColStr.Substring(0, 1).Trim()))
								{

								}
								else
								{

									// UNIT QTY
									if (!_itemQt.Equals(-1))
									{
										iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

										if (string.IsNullOrEmpty(iTemQt))
										{
											iTemQt = dt.Rows[i][_itemQt + 1].ToString().Trim();

											if (string.IsNullOrEmpty(iTemQt))
											{
												iTemQt = dt.Rows[i][_itemQt - 1].ToString().Trim();
											}
										}

										string[] qtunitSpl = iTemQt.Split(' ');

										if (qtunitSpl.Length == 2)
										{
											iTemQt = qtunitSpl[0].ToString().Trim();
											iTemUnit = qtunitSpl[1].ToString().Trim();
										}
										else
										{
											iTemQt = dt.Rows[i][_itemQt + 1].ToString();

											qtunitSpl = iTemQt.Split(' ');

											if (qtunitSpl.Length == 2)
											{
												iTemQt = qtunitSpl[0].ToString().Trim();
												iTemUnit = qtunitSpl[1].ToString().Trim();

												_itemCode = _itemCode + 1;

											}
										}


									}

									// CODE
									if (!_itemCode.Equals(-1))
										iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

									// DESC
									if (!_itemDesc.Equals(-1))
									{
										if (!_itemCode.Equals(-1))
										{
											for (int c = _itemDesc; c < _itemCode; c++)
											{
												iTemDESC = iTemDESC + " " + dt.Rows[i][c].ToString().Trim();
											}
										}
										else
										{
											for (int c = _itemDesc; c < _itemQt; c++)
											{
												iTemDESC = iTemDESC + " " + dt.Rows[i][c].ToString().Trim();
											}
										}

										// 앞에 번호 삭제
										string[] descSpl = iTemDESC.Split('.');
										string delDesc = string.Empty;

										if (descSpl.Length > 1)
										{
											delDesc = descSpl[0].ToString() + ".";

											iTemDESC = iTemDESC.Replace(delDesc, "").Trim();
										}


										// 반복 넣기
										int _i = i + 1;
										string nextLineStr = dt.Rows[_i][0].ToString();


										while (!regex.IsMatch(nextLineStr) || (!string.IsNullOrEmpty(nextLineStr) && !GetTo.IsInt(nextLineStr.Substring(0,1))))
										{
											if (nextLineStr.StartsWith("담당자") || nextLineStr.StartsWith("No. DESCRIPTION") ||
												(!string.IsNullOrEmpty(vessel) && nextLineStr.ToUpper().StartsWith(vessel.ToUpper())) || nextLineStr.StartsWith("TYPE :") || nextLineStr.StartsWith("DESCRIPTION :") ||
												nextLineStr.StartsWith("DRAW. NO."))
												break;

											if (!_itemCode.Equals(-1))
											{
												for (int c = _itemDesc; c < _itemCode; c++)
												{
													iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
												}
											}
											else
											{
												for (int c = _itemDesc; c < _itemQt; c++)
												{
													iTemDESC = iTemDESC.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
												}
											}

											_i += 1;
											i += 1;


											if (_i >= dt.Rows.Count)
												break;

											nextLineStr = dt.Rows[_i][0].ToString().Trim();

											if (nextLineStr.StartsWith("담당자") || nextLineStr.StartsWith("No. DESCRIPTION") ||
												(!string.IsNullOrEmpty(vessel) && nextLineStr.ToUpper().StartsWith(vessel.ToUpper())) || nextLineStr.StartsWith("TYPE :") || nextLineStr.StartsWith("DESCRIPTION :") ||
												nextLineStr.StartsWith("DRAW. NO."))
												break;


											if (regex.IsMatch(nextLineStr))
											{
												if (!string.IsNullOrEmpty(nextLineStr) && GetTo.IsInt(nextLineStr.Substring(0, 1).Trim()))
													break;
											}


										}
									}


									if (!string.IsNullOrEmpty(subjStr))
										iTemSUBJ = subjStr.Trim();


									//ITEM ADD START
									dtItem.Rows.Add();
									dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = "";
									dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
									dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = iTemUnit;
									if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
									if (!string.IsNullOrEmpty(iTemSUBJ))
										dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = iTemSUBJ;
									dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

									iTemDESC = string.Empty;
									iTemUnit = string.Empty;
									iTemCode = string.Empty;
									iTemQt = string.Empty;
									iTemSUBJ = string.Empty;

									subjStr = string.Empty;
								}
							}

							else
							{
								if (i >= dt.Rows.Count || firstColStr.StartsWith("담당자") || firstColStr.ToUpper().StartsWith("NO. DESCRIPTION") ||
											(!string.IsNullOrEmpty(vessel) && firstColStr.ToUpper().StartsWith(vessel.ToUpper())))
								{

								}
								else
								{
									subjStr = subjStr.Trim() + Environment.NewLine + firstColStr.Trim();
								}
							}
							}
						}
				}
			}
		}
	}
}

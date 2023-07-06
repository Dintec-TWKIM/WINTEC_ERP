using Dintec;
using Dintec.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Parsing.Parser.Inquiry
{
	class ShipServNew_pdf
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



		public ShipServNew_pdf(string fileName)
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

			int lineNo = -1;

			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;
			string iTemUniq = string.Empty;

			string subjStr = string.Empty;
			string sectionStr = string.Empty;

			string tnid = string.Empty;
			string buyerComment = string.Empty; // 헤더 코멘트
			string buyerStr = string.Empty; // 아이템 코멘트

			bool buyerStatus = false;
			bool notCode = false;
			bool notCodeOnly = false;
			

			DataTable commentdt = new DataTable();



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



			// xml row 나누기
			string[] xmlSpl = { };

			if (!string.IsNullOrEmpty(xmlTemp))
			{
				xmlSpl = xmlTemp.Split(new string[] { "<row>" }, StringSplitOptions.None);
			}


			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string firstStr = dt.Rows[i][0].ToString().ToLower();

					if (string.IsNullOrEmpty(reference))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().StartsWith("rfq ref"))
							{
								for (int c2 = c; c2 < dt.Columns.Count; c2++)
								{
									if (string.IsNullOrEmpty(reference))
									{
										reference = dt.Rows[i][c2].ToString().ToLower().Replace("rfq", "").Replace("ref", "").Replace(":", "").Trim();

										// 딘텍, 두베코, 딘싱 
										if (reference.Contains("[") && reference.Contains("]"))
										{
											string[] referenceSpl = reference.Split(' ');

											if (referenceSpl.Length == 2)
											{
												reference = referenceSpl[0].ToString().Trim();

												if (reference.Contains("(") && reference.Contains(")"))
												{
													int idx_s = reference.IndexOf("(");

													reference = reference.Substring(0, idx_s);
												}
											}
										}
									}
									else
										break;
								}
							}
						}

						if(reference.StartsWith("/"))
						{
							reference = "RFQ" + reference;
						}
					}

					if (string.IsNullOrEmpty(imoNumber))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().Equals("vessel:"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									vessel = vessel.Trim() + dt.Rows[i][c2].ToString().Trim();
								}

								if (vessel.Length > 9)
								{
									imoNumber = vessel.Right(7);

									if (GetTo.IsInt(imoNumber))
										vessel = vessel.Substring(0, vessel.Length - 7).Trim();
									else
										imoNumber = string.Empty;
								}

								if(tnid.Equals("10777")) // GAZOCEAN SA 20210112 서혜주 요청으로 호선
								{
									vessel = vessel.Substring(0, vessel.Length - 2).Trim();
								}

							}
						}

						if(string.IsNullOrEmpty(imoNumber))
						{
							for(int c = 0; c < dt.Columns.Count; c++)
							{
								if(dt.Rows[i][c].ToString().ToLower().Contains("vessel:"))
								{
									for(int c2 = c; c2 < dt.Columns.Count; c2++)
									{
										imoNumber = imoNumber.Trim() + dt.Rows[i+1][c2].ToString().Trim();
									}

									if (imoNumber.Length > 9)
									{
										imoNumber = imoNumber.Right(7);
									}
								}
							}

							if(!string.IsNullOrEmpty(imoNumber) && !GetTo.IsInt(imoNumber))
							{
								imoNumber = string.Empty;
							}
						}

						if(vessel.Contains("(") && vessel.Contains(")") && vessel.Contains("-") && vessel.Contains("[") && vessel.Contains("]"))
						{
							int idx_s = vessel.IndexOf("(");
							int idx_e = vessel.IndexOf(")");

							if(idx_s != -1 && idx_e != -1)
							{
								vessel = vessel.Substring(idx_s, idx_e - idx_s).Replace("(","").Trim();
							}
						}
					}

					if (string.IsNullOrEmpty(tnid))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().StartsWith("tnid"))
							{
								tnid = dt.Rows[i][c].ToString() + dt.Rows[i][c + 1].ToString();

								if (tnid.Length > 11)
								{
									tnid = tnid.Substring(6, 5).Trim();
									commentdt = GetDb.ShipServCommentGet(tnid);

									if(tnid.Equals("13095"))
									{
										if(!string.IsNullOrEmpty(reference) && reference.Contains("("))
										{
											int idx_s = reference.IndexOf("(");

											reference = reference.Substring(0, idx_s).Replace("(","").Trim();
										}
									}
								}
							}
						}
					}

					if (string.IsNullOrEmpty(subjStr))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().Contains("subject:"))
							{
								for (int c2 = c; c2 < dt.Columns.Count; c2++)
								{
									if (dt.Rows[i][c2].ToString().ToLower().Contains("vessel"))
									{
										break;
									}
									else
									{
										if (string.IsNullOrEmpty(subjStr.Trim()))
										{
											subjStr = subjStr.Trim() + " " + dt.Rows[i][c2].ToString().ToLower().Replace("subject:", "").Trim();

											if (string.IsNullOrEmpty(dt.Rows[i + 1][c].ToString()))
												subjStr = subjStr.Trim() + " " + dt.Rows[i + 1][c2].ToString().Trim();
										}
									}
								}


							}
						}
					}

					if (string.IsNullOrEmpty(buyerComment) && !buyerStatus)
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().Contains("buyer comments:") || (dt.Rows[i][c].ToString().Contains("buyer") && dt.Rows[i+1][c].ToString().Contains("comments")))
							{
								for (int c2 = c; c2 < dt.Columns.Count; c2++)
								{
									buyerComment = buyerComment.Trim() + " " + dt.Rows[i][c2].ToString().Trim();
								}

								int _i = i + 1;


								while (!dt.Rows[_i][0].ToString().ToLower().StartsWith("currency:") && !dt.Rows[_i][0].ToString().ToLower().StartsWith("#") && !dt.Rows[_i][0].ToString().Contains("…") && !dt.Rows[_i][0].ToString().ToLower().Contains("sent from"))
								{
									for (int c2 = 0; c2 < dt.Columns.Count; c2++)
									{
										buyerComment = buyerComment.Trim() + " " + dt.Rows[_i][c2].ToString().Trim();
									}

									_i += 1;

									if (_i >= dt.Rows.Count)
										break;
								}
							}
						}
					}



					// subject 2
					if (string.IsNullOrEmpty(sectionStr))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().StartsWith("equipment section"))
							{
								for (int c2 = c; c2 < dt.Columns.Count; c2++)
								{
									sectionStr = sectionStr.Trim() + " " + dt.Rows[i][c2].ToString().Trim();
								}

								int _i = i + 1;

								while (!GetTo.IsInt(dt.Rows[_i][0].ToString()) && !dt.Rows[_i][0].ToString().ToLower().StartsWith("sent from") && !dt.Rows[_i][0].ToString().ToLower().StartsWith("to"))
								{
									for (int c2 = 0; c2 < dt.Columns.Count; c2++)
									{
										sectionStr = sectionStr.Trim() + " " + dt.Rows[_i][c2].ToString().Trim();
									}

									_i += 1;

									if (_i >= dt.Rows.Count)
										break;
								}
							}
						}
					}



					if (firstStr.StartsWith("equipment section"))
					{
						sectionStr = string.Empty;

						for (int c2 = 0; c2 < dt.Columns.Count; c2++)
						{
							sectionStr = sectionStr.Trim() + " " + dt.Rows[i][c2].ToString().Trim();
						}

						int _i = i + 1;

						while (!GetTo.IsInt(dt.Rows[_i][0].ToString()) && !dt.Rows[_i][0].ToString().ToLower().StartsWith("sent from") && !dt.Rows[_i][0].ToString().ToLower().StartsWith("to"))
						{
							for (int c2 = 0; c2 < dt.Columns.Count; c2++)
							{
								sectionStr = sectionStr.Trim() + " " + dt.Rows[_i][c2].ToString().Trim();
							}

							_i += 1;

							if (_i >= dt.Rows.Count)
								break;
						}
					}
					else if (firstStr.Equals("#"))
					{
						// 코멘트 상태 확인하기
						buyerStatus = true;
						string forStr = string.Empty;

						for (int c = 1; c < dt.Columns.Count; c++)
						{
							forStr = dt.Rows[i][c].ToString().ToLower();

							if (forStr.Contains("description")) _itemDesc = c;
							else if (forStr.Contains("unit")) _itemUnit = c;
							else if (forStr.StartsWith("part")) _itemCode = c;
							else if (forStr.Contains("qty")) _itemQt = c;
						}
					}
					else if (firstStr.StartsWith("buyer comments"))
					{
						//for (int c = 0; c < dt.Columns.Count; c++)
						//	buyerStr = buyerStr + dt.Rows[i][c].ToString().Trim();


						//buyerStr = buyerStr.ToLower().Replace("buyer","").Replace("comments","").Replace(":","").Trim();
					}
					else if (firstStr.ToLower().StartsWith("1. all purchase orders are"))
					{
						break;
					}
					else if (GetTo.IsInt(firstStr) && !firstStr.StartsWith("0"))
					{

						if ((lineNo == -1 || (lineNo + 11 > Convert.ToInt32(firstStr))) || tnid.Contains("10678"))
						{
							lineNo = Convert.ToInt32(firstStr);


							// 수량
							if (_itemQt != -1)
							{
								iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

								if (!GetTo.IsInt(iTemQt))
								{
									iTemQt = dt.Rows[i][_itemQt - 1].ToString().Trim();

									if(!GetTo.IsInt(iTemQt))
									{
										iTemQt = dt.Rows[i][_itemQt + 1].ToString().Trim();

										if (_itemDesc != -1)
											iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim() + dt.Rows[i][_itemQt].ToString().Trim();

										if (_itemUnit != -1)
										{
											iTemUnit = dt.Rows[i][_itemUnit + 1].ToString().Trim();
										}
									}
									else
									{
										if (_itemUnit != -1)
										{
											iTemUnit = dt.Rows[i][_itemUnit - 1].ToString().Trim();
										}
									}

									
								}
								else
								{
									if (_itemUnit != -1)
									{
										iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();
									}
								}
							}


							if (_itemDesc != -1 && string.IsNullOrEmpty(iTemDESC))
								iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

							if (_itemCode != -1)
								iTemCode = dt.Rows[i][_itemCode].ToString().Trim();
									

							// 코드 & 아이템
							if (_itemCode != -1 && _itemDesc != -1)
							{
								if (!string.IsNullOrEmpty(iTemCode.ToLower().Replace("none", "")))
								{
									int _i = i + 1;
									if (iTemCode.EndsWith("-") || iTemCode.EndsWith(":") || iTemCode.EndsWith(",") || iTemCode.ToLower().EndsWith("plate"))
									{
										while (!GetTo.IsInt(dt.Rows[_i][0].ToString()))
										{
											if (dt.Rows[_i][0].ToString().ToLower().StartsWith("sent from")) break;
											else if (dt.Rows[_i][0].ToString().ToLower().StartsWith("buyer")) break;
											else if (dt.Rows[_i][0].ToString().ToLower().StartsWith("▋ terms and conditions:")) break;
											else if (dt.Rows[_i][0].ToString().ToLower().Contains("terms and conditions:")) break;
											else if (dt.Rows[_i][0].ToString().ToLower().Equals("#")) break;
											else if (dt.Rows[_i][0].ToString().ToLower().StartsWith("shipserv")) break;
											else if (GetTo.IsInt(dt.Rows[_i][0].ToString())) break;

											for (int c = 0; c <= _itemCode; c++)
											{
												iTemCode = iTemCode.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
											}

											iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][_itemDesc].ToString().Trim();

											_i += 1;

											if (_i >= dt.Rows.Count)
												break;


										}
									}
									else
									{
										while (!GetTo.IsInt(dt.Rows[_i][0].ToString()))
										{
											if (dt.Rows[_i][0].ToString().ToLower().StartsWith("sent from")) break;
											//else if (dt.Rows[_i][0].ToString().ToLower().StartsWith("buyer")) break;
											else if (dt.Rows[_i][0].ToString().ToLower().StartsWith("▋ terms and conditions:")) break;
											else if (dt.Rows[_i][0].ToString().ToLower().Contains("▋")) break;
											else if (dt.Rows[_i][0].ToString().ToLower().Contains("terms and conditions:")) break;
											else if (dt.Rows[_i][0].ToString().ToLower().Equals("#")) break;
											else if (dt.Rows[_i][0].ToString().ToLower().StartsWith("equipment section")) break;
											else if (dt.Rows[_i][0].ToString().ToLower().StartsWith("shipserv buyer record:")) break;
											else if (GetTo.IsInt(dt.Rows[_i][0].ToString())) break;


											//string[] rowValueSpl = new string[20];
											//int columnCount = 0;
											//for (int c = 0; c < dt.Columns.Count; c++)
											//{
											//	if (!string.IsNullOrEmpty(dt.Rows[_i][c].ToString()))
											//	{
											//		rowValueSpl[columnCount] = c.ToString();
											//		columnCount++;
											//	}
											//}



											//if (rowValueSpl[1] != null && rowValueSpl[2] == null)
											//{

											//}


											for (int c = 0; c <= _itemDesc; c++)
											{
												//if(!(dt.Rows[i][c].ToString().ToLower().Contains("buyer") && dt.Rows[i+1][c].ToString().ToLower().Contains("comments")))
													iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
											}

											


											if (dt.Rows[_i][_itemCode - 1].ToString().ToLower().Contains("buyer")) //|| iTemDESC.Contains(dt.Rows[_i][_itemCode].ToString())) && !notCodeOnly
											{
												notCode = true;
												notCodeOnly = false;
											}

											if (!notCode)
											{

												if (dt.Rows[_i][_itemCode].ToString().Length > 2)
												{
													if ((iTemDESC.Contains(dt.Rows[_i][_itemCode].ToString()) && dt.Rows[_i][_itemCode].ToString().ToLower().Contains("fig"))
														|| (iTemDESC.Contains(dt.Rows[_i][_itemCode].ToString()) && dt.Rows[_i][_itemCode].ToString().ToLower().Contains("item"))
														|| (iTemDESC.Contains(dt.Rows[_i][_itemCode].ToString()) && dt.Rows[_i][_itemCode].ToString().ToLower().Contains("pos."))
														|| (iTemDESC.Contains(dt.Rows[_i][_itemCode].ToString()) && dt.Rows[_i][_itemCode].ToString().ToLower().Contains("plate"))
														|| (iTemDESC.Contains(dt.Rows[_i][_itemCode].ToString()) && dt.Rows[_i][_itemCode].ToString().ToLower().Contains("number"))
														|| (iTemDESC.Contains(dt.Rows[_i][_itemCode].ToString()) && dt.Rows[_i][_itemCode].ToString().ToLower().Contains("spec")))
													{
														int idx_s = -1;

														if (iTemDESC.ToLower().Contains("item"))
														{
															//idx_s = iTemDESC.ToLower().IndexOf("item");

															//if (idx_s != -1)
															//{
															//	iTemDESC = iTemDESC.Substring(0, idx_s).Trim();
															//}
														}

														if (iTemDESC.ToLower().Contains("fig"))
														{
															idx_s = iTemDESC.ToLower().IndexOf("fig");

															if (idx_s != -1)
															{
																iTemDESC = iTemDESC.Substring(0, idx_s).Trim();
															}
														}

														if (iTemDESC.ToLower().Contains("spec") && !iTemDESC.ToUpper().Contains("INSPECTION") && !iTemDESC.ToUpper().Contains("SPECIFICATION"))
														{
															idx_s = iTemDESC.ToLower().IndexOf("spec");

															if (idx_s != -1)
															{
																iTemDESC = iTemDESC.Substring(0, idx_s).Trim();
															}
														}
														notCodeOnly = true;
													}
												}
											}


											if (notCodeOnly)
											{
												iTemCode = iTemCode.Trim() + " " + dt.Rows[_i][_itemCode].ToString().Trim();
												iTemDESC = iTemDESC.Replace(dt.Rows[_i][_itemCode].ToString().Trim(), "");
											}

											_i += 1;

											if (_i >= dt.Rows.Count)
												break;

											
										}

										if(!iTemDESC.ToLower().Contains("buyer part"))
											iTemDESC = iTemDESC.ToLower().Replace("buyer comments:", "").Trim();
										else
										{
											iTemDESC = iTemDESC.ToLower();
											int idx_s = iTemDESC.IndexOf("buyer part");

											if (idx_s != -1)
											{
												iTemUniq = iTemDESC.Substring(idx_s, iTemDESC.Length - idx_s).Trim();

												string uniqStr = Regex.Replace(iTemUniq, @"\D", "");
												iTemUniq = uniqStr;
											}

											iTemDESC = iTemDESC.Replace("buyer comments:", "").Replace("buyer part :", "").Replace(iTemUniq,"").Trim();
										}

									}
								}
								// 코드가 None 일 경우 품목명으로 넣기
								else
								{
									if (string.IsNullOrEmpty(iTemDESC.Trim()))
									{
										for (int c = 1; c <= _itemDesc; c++)
											iTemDESC = iTemDESC.Trim() + " " + dt.Rows[i][c].ToString().Trim();
									}

									int _i = i + 1;

									while (!GetTo.IsInt(dt.Rows[_i][0].ToString()))
									{
										if (dt.Rows[_i][0].ToString().ToLower().StartsWith("sent from")) break;
										//else if (dt.Rows[_i][0].ToString().ToLower().StartsWith("buyer comment")) break;
										else if (dt.Rows[_i][0].ToString().ToLower().StartsWith("▋ terms and conditions:")) break;
										else if (dt.Rows[_i][0].ToString().ToLower().Contains("terms and conditions:")) break;
										else if (dt.Rows[_i][0].ToString().ToLower().Contains("shipserv buyer record")) break;
										else if (GetTo.IsInt(dt.Rows[_i][0].ToString())) break;

										for (int c = 0; c <= _itemDesc; c++)
										{
											iTemDESC = iTemDESC.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
										}

										_i += 1;

										if (_i >= dt.Rows.Count)
											break;
									}

									iTemDESC = iTemDESC.ToLower().Replace("buyer comments:", "").Trim();
								}
							}


							if (iTemDESC.ToLower().Contains("maersk article number"))
							{
								int desc_s = iTemDESC.ToLower().IndexOf("maersk article number");

								if (desc_s != -1)
								{
									iTemUniq = iTemDESC.Substring(desc_s, iTemDESC.Length - desc_s).ToLower().Replace("maersk article number", "").Replace(":", "").Trim();
									iTemDESC = iTemDESC.Substring(0, desc_s).Trim();
								}
							}


							// CHEVRON [Buyer Item #:240148] -> 선사코드
							if (iTemDESC.ToLower().Contains("[buyer item"))
							{
								int idx_lts = iTemDESC.IndexOf("[buyer item");
								int idx_lte = iTemDESC.IndexOf("]");

								if (idx_lts > idx_lte)
								{
									if (idx_lts != -1 && idx_lte != -1)
									{
										iTemUniq = iTemDESC.Substring(idx_lts, iTemDESC.Length - idx_lts).Trim();

										iTemUniq = iTemUniq.Replace("[buyer item", "").Replace(":", "").Replace("#", "").Replace("]", "").Trim();
									}
								}
								else
								{
									if (idx_lts != -1 && idx_lte != -1)
									{
										iTemUniq = iTemDESC.Substring(idx_lts, idx_lte - idx_lts).Trim();

										iTemUniq = iTemUniq.Replace("[buyer item", "").Replace(":", "").Replace("#", "").Trim();
									}
								}
							}

							if (iTemDESC.Length > 7 && iTemDESC.StartsWith("[") && iTemDESC.Contains("]"))
							{
								string delDes = iTemDESC.Substring(0, 7);
								int idx_e = delDes.IndexOf("]");
								if (idx_e != -1)
									delDes = delDes.Substring(0, idx_e + 1);

								iTemDESC = iTemDESC.Replace(delDes, "").Trim();
							}



							if (iTemDESC.ToLower().Contains("Imatech No.:"))
							{
								int idx_s = iTemDESC.IndexOf("Imatech No.:");

								iTemCode = iTemCode + " " + iTemDESC.Substring(idx_s, 19).Replace("Imatech No.:", "");
								iTemCode = iTemCode.Trim();
							}

							// NONE => ''
							if (iTemCode.ToLower().Equals("none"))
								iTemCode = string.Empty;


							if (!string.IsNullOrEmpty(subjStr))
								iTemSUBJ = subjStr.ToLower().Replace("subject:", "").Trim();

							if (!string.IsNullOrEmpty(sectionStr))
								iTemSUBJ = iTemSUBJ + Environment.NewLine + sectionStr.ToLower().Replace("equipment", "").Replace("section:", "").Trim();

							if (!string.IsNullOrEmpty(buyerComment) && commentdt.Rows.Count > 0)
								iTemSUBJ = iTemSUBJ + Environment.NewLine + buyerComment.Trim();

							if(!string.IsNullOrEmpty(buyerStr) && commentdt.Rows.Count > 0)
								iTemDESC = iTemDESC + Environment.NewLine + buyerStr.Trim();


							iTemSUBJ= iTemSUBJ.ToUpper().Replace("DESCRIPTION:", "\r\nDESCRIPTION:").Trim();
//							iTemSUBJ = iTemSUBJ.ToUpper().Replace("NAME:", "\r\nNAME:").Trim();

							if(iTemDESC.ToUpper().Contains("[ ITEM #"))
							{
								int idx_s = iTemDESC.ToUpper().IndexOf("[ ITEM #");

								if(idx_s != -1)
								{
									string _itemcodeStr = string.Empty;

									_itemcodeStr = iTemDESC.Substring(idx_s, 16).ToUpper();

									iTemDESC = iTemDESC.ToUpper().Replace(_itemcodeStr,"").Trim();

									iTemUniq = _itemcodeStr.Replace("[ ITEM #", "").Replace(":","").Replace("]", "").Trim();
								}
							}


							if(iTemDESC.ToLower().Contains("buyer") && iTemDESC.ToLower().Contains("comments"))
							{
								iTemDESC = iTemDESC.ToLower().Replace("buyer", "").Replace("comments:", "").Trim();
							}


							iTemUniq = iTemUniq.Replace("[buyer item","").Trim();


							//ITEM ADD START
							dtItem.Rows.Add();
							dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstStr;
							dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
							dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
							if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
							if (!string.IsNullOrEmpty(iTemSUBJ)) dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ.Trim();
							dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode.Replace("Buyer comments:", "").Trim();
							dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;

							iTemDESC = string.Empty;
							iTemUnit = string.Empty;
							iTemQt = string.Empty;
							iTemCode = string.Empty;
							//sectionStr = string.Empty;
							iTemSUBJ = string.Empty;

							notCode = false;
							notCodeOnly = false;
						}
					}
				}
			}
		}
	}
}

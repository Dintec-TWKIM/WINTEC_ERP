using Aspose.Email.Outlook;
using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class mailShipServ
	{
		string vessel;
		string reference;
		string partner;
		string imonumber;
		string tnid;
		string contact;
		string buyer;


		DataTable dtItem;

		string fileName;
		UnitConverter uc;

		string mailOrigBody = string.Empty;

		string subjHead = string.Empty;

		string subjStr = string.Empty;
		string buyerStr = string.Empty;
		string subjStr2 = string.Empty;

		string iTemDesc = string.Empty;
		string iTemQt = string.Empty;
		string iTemCode = string.Empty;
		string iTemUnit = string.Empty;
		string iTemSubj = string.Empty;
		string iTemSubj2 = string.Empty;
		string iTemNo = string.Empty;
		string iTemUniq = string.Empty;



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


		public string Buyer
		{
			get
			{
				return buyer;
			}
		}

		public string Partner
		{
			get
			{
				return partner;
			}
		}

		public string ImoNumber
		{
			get
			{
				return imonumber;
			}
		}

		public string Tnid
		{
			get
			{
				return tnid;
			}
		}

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		public string Contact
		{
			get
			{
				return contact;
			}
		}

		#endregion ==================================================================================================== Constructor



		public mailShipServ(string fileName)
		{
			vessel = "";                        // 선명
			reference = "";                     // 문의번호
			partner = "";                       // 매입처 담당자

			tnid = "";
			imonumber = "";
			contact = string.Empty;
			buyer = string.Empty;
				

			dtItem = new DataTable();
			dtItem.Columns.Add("NO");           // 순번
			dtItem.Columns.Add("SUBJ");         // 주제
			dtItem.Columns.Add("ITEM");         // 품목코드
			dtItem.Columns.Add("DESC");         // 품목명
			dtItem.Columns.Add("UNIT");         // 단위
			dtItem.Columns.Add("QT");           // 수량
			dtItem.Columns.Add("UNIQ");         // 선사
			this.fileName = fileName;
			this.uc = new UnitConverter();
		}

		public void Parse()
		{
			MapiMessage msg = MapiMessage.FromFile(fileName);

			string mailBodyStr = msg.Body;
			int idx_lts = -1;
			int idx_lte = -1;

			if (mailBodyStr.Contains("*** THIS IS AN AUTOMATED E-MAIL ALERT FROM SHIPSERV ***"))
			{
				idx_lts = mailBodyStr.IndexOf("*** THIS IS AN AUTOMATED E-MAIL ALERT FROM SHIPSERV ***");

				mailBodyStr = mailBodyStr.Substring(idx_lts, mailBodyStr.Length - idx_lts).Trim();
			}




			// reference
			idx_lts = mailBodyStr.IndexOf("RFQ Ref:");
			idx_lte = mailBodyStr.IndexOf("Subject:");

			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
				reference = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("RFQ Ref:", "").Trim();



			// vessel
			idx_lts = -1; idx_lte = -1;

			idx_lts = mailBodyStr.IndexOf("Vessel:");
			idx_lte = mailBodyStr.IndexOf("Vessel No.:");

			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
				vessel = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Vessel:", "").Trim();



			// imonumber
			idx_lts = -1; idx_lte = -1;

			idx_lts = mailBodyStr.IndexOf("Vessel No.:");
			idx_lte = mailBodyStr.IndexOf("Class:");

			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
				imonumber = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Vessel No.:", "").Trim();




			// subject
			idx_lts = -1; idx_lte = -1;

			idx_lts = mailBodyStr.IndexOf("Subject");
			idx_lte = mailBodyStr.IndexOf("Vessel:");

			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
				subjHead = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Subject:", "").Trim();



			string mailBodyStr2 = string.Empty;


			// partner 하기전에 전초단계
			idx_lts = -1; idx_lte = -1;

			idx_lts = mailBodyStr.IndexOf("From:");
			idx_lte = mailBodyStr.IndexOf("Request for Quote Details:");

			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
				mailBodyStr2 = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Vessel No.:", "").Trim();


			// contact
			idx_lts = -1; idx_lte = -1;

			idx_lts = mailBodyStr2.IndexOf("Contact");
			idx_lte = mailBodyStr2.IndexOf("Tel");

			if (idx_lte == -1 || idx_lte < idx_lts)
				idx_lte = mailBodyStr2.IndexOf("Email:");


			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
				contact = mailBodyStr2.Substring(idx_lts, idx_lte - idx_lts).Replace("Contact", "").Replace(":", "").Trim();



			// partner
			idx_lts = -1; idx_lte = -1;

			idx_lts = mailBodyStr2.IndexOf("From:");
			idx_lte = mailBodyStr2.IndexOf("(TNID");

			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1) && !string.IsNullOrEmpty(mailBodyStr2))
				partner = mailBodyStr2.Substring(idx_lts, idx_lte - idx_lts).Replace("From:", "").Trim();



			// tnid
			idx_lts = -1; idx_lte = -1;

			idx_lts = mailBodyStr2.IndexOf("(TNID:");
			idx_lte = mailBodyStr2.IndexOf(") \r\n-");

			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1) && !string.IsNullOrEmpty(mailBodyStr2))
				tnid = mailBodyStr2.Substring(idx_lts, idx_lte - idx_lts).Replace("(TNID:", "").Trim();



			// BUYER COMMENTS
			idx_lts = -1; idx_lte = -1;

			idx_lts = mailBodyStr.IndexOf("Buyer Comments:");
			idx_lte = mailBodyStr.IndexOf("Line Items");

			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
				buyer = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Buyer Comments:", "").Trim();



			// BUYER COMMENTS 추가 할 선사들 // CZ_MA00036 // COMMETN
			DataTable commentdt = new DataTable();
			commentdt = GetDb.ShipServCommentGet(tnid);
			if(commentdt.Rows.Count > 0)
			{
				buyerStr = buyer;
			}




			//if(!tnid.Equals("12126"))
			//{
			if (reference.Contains("("))
			{
				idx_lts = reference.IndexOf("(");

				reference = reference.Substring(0, idx_lts).Trim();
			}
			//}




			idx_lts = -1; idx_lte = -1;

			idx_lts = mailBodyStr.IndexOf("Currency");
			idx_lte = mailBodyStr.IndexOf("Other Information:");

			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
			{
				mailOrigBody = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Trim();
			}




			// SUBJECT
			idx_lts = -1; idx_lte = -1;

			idx_lts = mailOrigBody.IndexOf("Equipment Section:");
			idx_lte = mailOrigBody.IndexOf("Qty");

			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
			{
				subjStr = mailOrigBody.Substring(idx_lts, idx_lte - idx_lts).Trim();

				subjStr = subjStr.ToLower().Replace("equipment section:", "").Trim();
			}




			if (!string.IsNullOrEmpty(subjHead))
				iTemSubj = subjHead.Trim();

			if (!string.IsNullOrEmpty(subjStr))
				iTemSubj = iTemSubj.Trim() + Environment.NewLine + subjStr.Trim();

			if (!string.IsNullOrEmpty(buyerStr))
				iTemSubj = iTemSubj.Trim() + Environment.NewLine + buyerStr.Trim();

			iTemSubj = iTemSubj.Replace("FOR", "").Replace("For:", "").Trim();

			if (iTemSubj.EndsWith("#"))
				iTemSubj = iTemSubj.Substring(0, iTemSubj.Length - 1).Trim();


			


			// ITEM
			idx_lts = -1; idx_lte = -1;

			idx_lts = mailOrigBody.IndexOf("Qty");
			idx_lte = mailOrigBody.IndexOf("Attachments:");

			if (idx_lte.Equals(-1))
				idx_lte = mailOrigBody.Length;

			int _eq_idx_lts = mailOrigBody.IndexOf("Equipment Section");

			if (idx_lts > _eq_idx_lts)
			{
				subjStr = mailOrigBody.Substring(_eq_idx_lts, idx_lts - _eq_idx_lts).Replace("#", "").Trim();
			}

			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
			{
				string mailItemStr = mailOrigBody.Substring(idx_lts, idx_lte - idx_lts).Trim();

				// 나눌 문자열 추출
				idx_lts = -1; idx_lte = -1;

				idx_lts = mailItemStr.IndexOf("Qty");
				idx_lte = mailItemStr.IndexOf("Unit");

				if (idx_lte > idx_lts + 20)
				{
					idx_lts = mailItemStr.IndexOf("#\t Qty");

					if (idx_lte < idx_lts + 20)
					{
						mailItemStr = mailItemStr.Substring(idx_lts, mailItemStr.Length - idx_lts);

						if (mailItemStr.StartsWith("#"))
							mailItemStr = mailItemStr.Substring(1, mailItemStr.Length - 1).Trim();

						idx_lts = mailItemStr.IndexOf("Qty");
						idx_lte = mailItemStr.IndexOf("Unit");
					}
				}

				string splStr = string.Empty;
				string splStr1 = string.Empty;
				string splStr2 = string.Empty;

				if (idx_lts != -1 && idx_lte != -1)
					splStr1 = mailItemStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Qty", "").Replace("#", "");

				if (splStr1.Length > 6)
					splStr1 = "\t";
				//splStr = "\t";

				idx_lts = mailItemStr.IndexOf("Description");
				idx_lte = mailItemStr.IndexOf(splStr1 + "\r\n");

				if (idx_lte == -1)
					idx_lte = mailItemStr.IndexOf("2");


				if (idx_lts != -1 && idx_lte != -1)
				{
					if (!(idx_lte - idx_lts > 40))
					{
						if (idx_lts != -1 && idx_lte != -1)
							splStr2 = mailItemStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Description", "");

						if (!splStr1.Equals(splStr2) && !string.IsNullOrEmpty(splStr2))
							splStr = splStr2;
						else if (string.IsNullOrEmpty(splStr1) && !string.IsNullOrEmpty(splStr2))
							splStr = splStr2;

						if (splStr2.Contains(splStr1))
							splStr = splStr1;
					}
					else
					{
						splStr = splStr1;
					}

					if (string.IsNullOrEmpty(splStr))
						splStr = splStr1;
				}
				else
				{
					splStr = splStr1;
				}




				mailItemStr = mailItemStr.Replace("#" + splStr1, "").Replace("Qty" + splStr1, "").Replace("Unit" + splStr1, "").Replace("IdType / IdCode" + splStr1, "").Replace("Description" + splStr1, "").Trim();

				mailItemStr = mailItemStr.Replace("\r\nComments:", "Comments:").Replace(splStr + "\r\n", splStr).Replace("\t\r\n", "\t \r\n").Trim();

				mailItemStr = mailItemStr.Replace("\r\n1 ", splStr + "1");
				mailItemStr = mailItemStr.Replace("\r\n2 ", splStr + "2");
				mailItemStr = mailItemStr.Replace("\r\n3 ", splStr + "3");
				mailItemStr = mailItemStr.Replace("\r\n4 ", splStr + "4");
				mailItemStr = mailItemStr.Replace("\r\n5 ", splStr + "5");
				mailItemStr = mailItemStr.Replace("\r\n6 ", splStr + "6");
				mailItemStr = mailItemStr.Replace("\r\n7 ", splStr + "7");
				mailItemStr = mailItemStr.Replace("\r\n8 ", splStr + "8");
				mailItemStr = mailItemStr.Replace("\r\n9 ", splStr + "9");
				mailItemStr = mailItemStr.Replace("\r\n0 ", splStr + "0");

				mailItemStr = mailItemStr.Replace("\r\n1\r\n", splStr + "1\r\n");
				mailItemStr = mailItemStr.Replace("\r\n2\r\n", splStr + "2\r\n");
				mailItemStr = mailItemStr.Replace("\r\n3\r\n", splStr + "3\r\n");
				mailItemStr = mailItemStr.Replace("\r\n4\r\n", splStr + "4\r\n");
				mailItemStr = mailItemStr.Replace("\r\n5\r\n", splStr + "5\r\n");
				mailItemStr = mailItemStr.Replace("\r\n6\r\n", splStr + "6\r\n");
				mailItemStr = mailItemStr.Replace("\r\n7\r\n", splStr + "7\r\n");
				mailItemStr = mailItemStr.Replace("\r\n8\r\n", splStr + "8\r\n");
				mailItemStr = mailItemStr.Replace("\r\n9\r\n", splStr + "9\r\n");
				mailItemStr = mailItemStr.Replace("\r\n0\r\n", splStr + "0\r\n");

				// 20190611 FB19078058 \r\n 제거
				if (!splStr.Contains("\r\n"))
					mailItemStr = mailItemStr.Replace(splStr + splStr, splStr).Replace("\r\n((c)) \t \r\n", "((c))").Trim();

				//idx_lts = mailItemStr.IndexOf("\r\n");

				if (mailItemStr.Contains("\t "))
				{
					mailItemStr = mailItemStr.Replace("\r\n", "\t ");
					splStr = "\t ";
				}

				mailItemStr = mailItemStr.Replace("\t \t", "\t ").Trim();
				mailItemStr = mailItemStr.Replace("Equipment Section:\t For:", "Equipment Section: For:");

				string[] itemSpl = null;
				int splLenth = 0;
				string changeNo = string.Empty;

				mailItemStr = mailItemStr.Replace(splStr + " " + splStr, splStr);
				mailItemStr = mailItemStr.Replace(splStr + "  " + splStr, splStr);

				itemSpl = mailItemStr.Split(new string[] { splStr }, StringSplitOptions.None);
				splLenth = itemSpl.Length;

				for (int v = 0; v < itemSpl.Length; v++)
				{
					iTemSubj = string.Empty;

					if (itemSpl.Length > v + 4)
					{
						if (itemSpl[v].ToString().Contains("Equipment"))
						{
							subjStr = itemSpl[v].ToString().Trim();

							subjStr = subjStr.Replace("Equipment Section:", "").Trim();

							int _i = v + 1;
							while (!GetTo.IsInt(itemSpl[_i].ToString()))
							{
								subjStr = subjStr.Trim() + Environment.NewLine + itemSpl[_i].ToString();

								_i += 1;

								if (_i >= itemSpl.Length)
									break;
							}
							//if (itemSpl[v + 1].ToString().Contains("For") || GetTo.IsInt(itemSpl[v + 1].ToString().Replace("\r", "").Replace("\n", "")) ||
							//    itemSpl[v + 1].ToString().Contains("Model:") || !itemSpl[v + 1].ToString().Contains("Serial:"))
							//    subjStr = subjStr.Trim() + Environment.NewLine + itemSpl[v + 1].ToString().Trim();
						}



						if (!string.IsNullOrEmpty(subjHead))
							iTemSubj = subjHead.Trim();

						if (!string.IsNullOrEmpty(subjStr))
							iTemSubj = iTemSubj.Trim() + Environment.NewLine + subjStr.Trim();

						if (!string.IsNullOrEmpty(buyerStr))
							iTemSubj = iTemSubj.Trim() + Environment.NewLine + buyerStr.Trim();

						iTemSubj = iTemSubj.Replace("FOR", "").Replace("For:", "").Trim();

						if (iTemSubj.EndsWith("#"))
							iTemSubj = iTemSubj.Substring(0, iTemSubj.Length - 1).Trim();


						iTemDesc = itemSpl[v + 4].ToString().Trim();

						if((iTemDesc.ToLower().Contains("part no") && !iTemDesc.ToLower().Contains("part no-") && !iTemDesc.ToLower().Contains("part no.")) || iTemDesc.ToLower().Contains("pec"))
						{
							iTemDesc = string.Empty;
						}

						if (iTemDesc.Contains("Equipment Section"))
						{
							iTemDesc = string.Empty;
						}
						else
						{
							string itemSplStr5 = string.Empty;
							string itemSplStr6 = string.Empty;

							if (v + 5 < itemSpl.Length)
								itemSplStr5 = itemSpl[v + 5].ToString().Trim().ToUpper();

							if (v + 6 < itemSpl.Length)
								itemSplStr6 = itemSpl[v + 6].ToString().Trim().ToUpper();


							if (v + 5 < itemSpl.Length  
								&& !itemSplStr5.StartsWith("PCE")
								&& !itemSplStr5.Contains("EQUIPMENT") 
								&& !itemSplStr5.Contains("SECTION")
								&& !itemSplStr5.Contains("MANUFACTURER'S PART NO(MF)") 
								&& !itemSplStr5.Contains("BUYER'S PART NO(BP)") 
								&& !itemSplStr5.Contains("SUPPLIER'S PART NO(VP)") 
								&& !itemSplStr5.Contains("IMPA NO.(ZIM)"))
							{
								if (!GetTo.IsInt(itemSplStr5))
								{
									iTemDesc = iTemDesc.Trim() + " " + itemSpl[v + 5].ToString().Trim();
								}
							}

							if (v + 6 < itemSpl.Length  
								&& !itemSplStr6.StartsWith("PCE")
								&& !itemSplStr6.Contains("EQUIPMENT") 
								&& !itemSplStr6.Contains("SECTION") 
								&& !itemSplStr6.Contains("MODEL:") 
								&& !itemSplStr6.Contains("SERIAL:") 
								&& !itemSplStr6.Contains("MANUFACTURER'S PART NO(MF)") 
								&& !itemSplStr6.Contains("BUYER'S PART NO(BP)") 
								&& !itemSplStr6.Contains("SUPPLIER'S PART NO(VP)") 
								&& !itemSplStr6.Contains("IMPA NO.(ZIM)") 
								&& !itemSplStr6.StartsWith("COMMENTS:")
								&& !itemSplStr6.StartsWith("FOR:") 
								&& !itemSplStr6.Contains("DESC:")
								)
							{
								if (!GetTo.IsInt(itemSplStr6))
								{
									iTemDesc = iTemDesc.Trim() + " " + itemSpl[v + 6].ToString().Trim();
								}
							}

							if (v + 6 < itemSpl.Length && !GetTo.IsInt(itemSplStr6) && !itemSplStr6.StartsWith("PCE") && !itemSplStr6.StartsWith("EQUIPMENT SECTION:") &&
								!itemSplStr6.StartsWith("FOR:") && !itemSplStr6.Contains("DESC:") && !itemSplStr6.Contains("BUYER'S PART NO"))
							{

								if(!iTemDesc.ToUpper().Contains(itemSplStr6.ToUpper()))
									iTemDesc = iTemDesc.Trim() + Environment.NewLine + itemSpl[v + 6].ToString().Trim();

								int vResult = v + 6;
								int _v = vResult + 1;

								if (_v < itemSpl.Length)
								{
									while (!GetTo.IsInt(itemSpl[_v].ToString()))
									{
										if (!itemSpl[_v].ToString().ToLower().Contains("equipment section"))
											iTemDesc = iTemDesc.Trim() + " " + itemSpl[_v].ToString().Trim();
										else
											break;
										_v += 1;

										if (_v >= itemSpl.Length)
											break;
									}
								}
							}
						}
					}
					else if (itemSpl.Length == v + 4)
					{
						iTemSubj = string.Empty;

						if (itemSpl[v].ToString().Contains("Equipment"))
						{
							subjStr = itemSpl[v].ToString().Trim();

							subjStr = subjStr.Replace("Equipment Section:", "").Trim();

							int _i = v + 1;
							while (!GetTo.IsInt(itemSpl[_i].ToString()))
							{
								subjStr = subjStr.Trim() + Environment.NewLine + itemSpl[_i].ToString();

								_i += 1;

								if (_i >= itemSpl.Length)
									break;
							}
							//subjStr = subjStr.Replace("Equipment Section:", "").Trim();

							//if (itemSpl[v + 1].ToString().Contains("For") || GetTo.IsInt(itemSpl[v + 1].ToString().Replace("\r", "").Replace("\n", "")) || 
							//    itemSpl[v + 1].ToString().Contains("Model:") || !itemSpl[v + 1].ToString().Contains("Serial:"))
							//    subjStr = subjStr.Trim() + Environment.NewLine + itemSpl[v + 1].ToString().Trim();
						}


						if (!string.IsNullOrEmpty(subjHead))
							iTemSubj = subjHead.Trim();

						if (!string.IsNullOrEmpty(subjStr))
							iTemSubj = iTemSubj.Trim() + Environment.NewLine + subjStr.Trim();

						if (!string.IsNullOrEmpty(buyerStr))
							iTemSubj = iTemSubj.Trim() + Environment.NewLine + buyerStr.Trim();

						iTemSubj = iTemSubj.Replace("FOR", "").Replace("For:", "").Trim();

						if (iTemSubj.EndsWith("#"))
							iTemSubj = iTemSubj.Substring(0, iTemSubj.Length - 1).Trim();


						if ((GetTo.IsInt(itemSpl[v].ToString().Replace("\r", "").Replace("\n", "").Trim()) && itemSpl[v].ToString().Length < 5) && (GetTo.IsInt(itemSpl[v + 1].ToString().Replace("\r", "").Replace("\n", "").Trim()) && itemSpl[v + 1].ToString().Length < 5))
						{
							iTemNo = itemSpl[v].ToString().Trim();
							iTemQt = itemSpl[v + 1].ToString().Trim();
							iTemUnit = itemSpl[v + 2].ToString().Trim();
							iTemCode = itemSpl[v + 3].ToString().Trim();

							if (iTemDesc.Contains("Freight") && iTemDesc.Contains("Packing") && iTemDesc.Contains("Handling"))
								break;

							if (iTemCode.Contains("[PCS]") || iTemCode.Contains("[EA]") || iTemCode.Contains("[PCE]") || iTemCode.Contains("[PC]") || iTemCode.Contains("[SET]"))
							{
								if (string.IsNullOrEmpty(iTemDesc))
								{
									idx_lts = -1; idx_lte = -1;

									if (iTemCode.Contains("[PCS]"))
										idx_lts = iTemCode.IndexOf("[PCS]");
									else if (iTemCode.Contains("[EA]"))
										idx_lts = iTemCode.IndexOf("[EA]");
									else if (iTemCode.Contains("[PCE]"))
										idx_lts = iTemCode.IndexOf("[PCE]");
									else if (iTemCode.Contains("[PC]"))
										idx_lts = iTemCode.IndexOf("[PC]");
									else if (iTemCode.Contains("[SET]"))
										idx_lts = iTemCode.IndexOf("[SET]");

									if (idx_lts != -1)
										iTemDesc = iTemCode.Substring(idx_lts, iTemCode.Length - idx_lts).Trim();

									idx_lts = -1;
								}
							}


							if (string.IsNullOrEmpty(iTemDesc))
							{
								idx_lts = -1;
								idx_lts = iTemCode.IndexOf("\r\n");

								if (idx_lts != -1)
								{
									iTemDesc = iTemCode.Substring(idx_lts, iTemCode.Length - idx_lts).Trim();

									iTemCode = iTemCode.Replace(iTemDesc, "");
								}
							}

							if (iTemDesc.Contains("Buyer Part"))
							{
								idx_lts = -1;

								idx_lts = iTemDesc.IndexOf("Buyer Part");

								if (idx_lts != -1)
								{
									iTemUniq = iTemDesc.Substring(idx_lts, iTemDesc.Length - idx_lts).Replace(":", "").Replace("Buyer Part", "").Trim();
								}
							}


							iTemDesc = iTemDesc.Replace("[PCS]", "").Replace("[EA]", "").Replace("[PCE]", "").Replace("[PC]", "").Replace("[SET]", "").Replace("\r\n", " ").Trim();
							iTemDesc = iTemDesc.Replace("Manufacturer's Part No(MF)", "").Replace("Buyer's Part No(BP)", "").Replace("Supplier's Part No(VP)", "").Replace("IMPA No.(ZIM)", "").Trim();

							if (iTemDesc.Contains("Comments:"))
								iTemDesc = iTemDesc.Replace("Comments:", "\r\nComments:").Trim();


							iTemSubj = iTemSubj.ToLower().Replace("equipment section:", "").Trim();
							iTemSubj = iTemSubj.ToLower().Replace("\t", "").Trim();
							iTemSubj = iTemSubj.ToLower().Replace("\r\n ", "\r\n").Trim();
							iTemSubj = iTemSubj.ToLower().Replace("\r\n  ", "\r\n").Trim();
							iTemSubj = iTemSubj.ToLower().Replace("\r\n\r\n", "\r\n").Trim();
							iTemSubj = iTemSubj.ToLower().Replace("\r\n ", "\r\n").Trim();



							// TEST 중

							// Maersk A/S 맵스용
							if ((iTemSubj.ToUpper().Contains("HIMSEN") || iTemSubj.ToUpper().Contains("HYUNDAI")) && tnid.Equals("11327"))
							{
								//mapsInput(tnid);
							}


							// Scorpio 맵스용
							if ((iTemSubj.ToUpper().Contains("HIMSEN") || iTemSubj.ToUpper().Contains("HYUNDAI")) && tnid.Equals("10553"))
							{
								//mapsInput(tnid);
							}


							//ITEM ADD START
							dtItem.Rows.Add();
							dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
							dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDesc;
							dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
							dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;
							if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
							dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSubj;
							dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode.ToUpper().Replace("MANUFACTURER'S PART NO(MF)", "").Replace("BUYER'S PART NO(BP)", "").Replace("SUPPLIER'S PART NO(VP)", "").Replace("IMPA NO.(ZIM)", "").Replace("PART NO", "").Trim();

							iTemNo = string.Empty;
							iTemDesc = string.Empty;
							iTemUnit = string.Empty;
							iTemQt = string.Empty;
							iTemCode = string.Empty;

							break;
						}
					}
					else
					{
						break;
					}

					if (itemSpl[v].ToString().Contains("Equipment"))
					{
						subjStr = itemSpl[v].ToString().Trim();

						subjStr = subjStr.Replace("Equipment Section:", "").Trim();

						int _i = v + 1;
						while (!GetTo.IsInt(itemSpl[_i].ToString()))
						{
							subjStr = subjStr.Trim() + Environment.NewLine + itemSpl[_i].ToString();

							_i += 1;

							if (_i >= itemSpl.Length)
								break;
						}

						//subjStr = subjStr.Replace("Equipment Section:", "").Trim();

						//if (itemSpl[v + 1].ToString().Contains("For") || GetTo.IsInt(itemSpl[v + 1].ToString().Replace("\r", "").Replace("\n", "")) ||
						//        itemSpl[v + 1].ToString().Contains("Model:") || !itemSpl[v + 1].ToString().Contains("Serial:"))
						//    subjStr = subjStr.Trim() + Environment.NewLine + itemSpl[v + 1].ToString().Trim();
					}

					iTemSubj = string.Empty;


					if (!string.IsNullOrEmpty(subjHead))
						iTemSubj = subjHead.Trim();

					if (!string.IsNullOrEmpty(subjStr))
						iTemSubj = iTemSubj.Trim() + Environment.NewLine + subjStr.Trim();

					if (!string.IsNullOrEmpty(buyerStr))
						iTemSubj = iTemSubj.Trim() + Environment.NewLine + buyerStr.Trim();

					iTemSubj = iTemSubj.Replace("FOR", "").Replace("For:", "").Trim();

					if (iTemSubj.EndsWith("#"))
						iTemSubj = iTemSubj.Substring(0, iTemSubj.Length - 1).Trim();


					if (itemSpl[v + 3].ToString().Contains("Part No") || itemSpl[v + 3].ToString().Contains("IMPA No") && itemSpl[v+3].ToString().Length < 10)
					{
						iTemUnit = itemSpl[v + 2].ToString().Trim();
						iTemCode = itemSpl[v + 3].ToString().Trim();
					}


					if ((GetTo.IsInt(itemSpl[v].ToString().Replace("\r", "").Replace("\n", "").Trim()) && itemSpl[v].ToString().Length < 5 ) && (GetTo.IsInt(itemSpl[v + 1].ToString().Replace("\r", "").Replace("\n", "").Trim()) && itemSpl[v+1].ToString().Length < 5) )
					{
						iTemNo = itemSpl[v].ToString();
						iTemQt = itemSpl[v + 1].ToString().Trim();

						if (string.IsNullOrEmpty(iTemUnit))
							iTemUnit = itemSpl[v + 2].ToString().Trim();

						if (GetTo.IsInt(iTemDesc.Replace("\r", "").Replace("\n", "").Trim()) && !string.IsNullOrEmpty(iTemCode))
							iTemDesc = itemSpl[v + 3].ToString().Trim();

						if (iTemDesc.Contains("Freight") && iTemDesc.Contains("Packing") && iTemDesc.Contains("Handling"))
							break;

						if (iTemCode.Contains("[PCS]") || iTemCode.Contains("[EA]") || iTemCode.Contains("[PCE]") || iTemCode.Contains("[PC]") || iTemCode.Contains("[SET]"))
						{
							if (string.IsNullOrEmpty(iTemDesc))
							{
								idx_lts = -1; idx_lte = -1;

								if (iTemCode.Contains("[PCS]"))
									idx_lts = iTemCode.IndexOf("[PCS]");
								else if (iTemCode.Contains("[EA]"))
									idx_lts = iTemCode.IndexOf("[EA]");
								else if (iTemCode.Contains("[PCE]"))
									idx_lts = iTemCode.IndexOf("[PCE]");
								else if (iTemCode.Contains("[PC]"))
									idx_lts = iTemCode.IndexOf("[PC]");
								else if (iTemCode.Contains("[SET]"))
									idx_lts = iTemCode.IndexOf("[SET]");

								if (idx_lts != -1)
									iTemDesc = iTemCode.Substring(idx_lts, iTemCode.Length - idx_lts).Trim();

								idx_lts = -1;
							}
							else
							{
								iTemCode = string.Empty;
							}
						}


						if (iTemDesc.Contains("Buyer Part"))
						{
							idx_lts = -1;

							idx_lts = iTemDesc.IndexOf("Buyer Part");

							if (idx_lts != -1)
							{
								iTemUniq = iTemDesc.Substring(idx_lts, iTemDesc.Length - idx_lts).Replace(":", "").Replace("Buyer Part", "").Trim();
							}
						}

						iTemDesc = iTemDesc.Replace("[PCS]", "").Replace("[EA]", "").Replace("[PCE]", "").Replace("[PC]", "").Replace("[SET]", "").Replace("\r\n", " ").Trim();

						if (iTemDesc.Contains("[Buyer Item"))
						{
							idx_lts = iTemDesc.IndexOf("[Buyer Item");
							idx_lte = iTemDesc.IndexOf("]");

							if (idx_lts > idx_lte)
							{
								if (idx_lts != -1 && idx_lte != -1)
								{
									iTemUniq = iTemDesc.Substring(idx_lts, iTemDesc.Length - idx_lts).Trim();

									iTemUniq = iTemUniq.Replace("[Buyer Item", "").Replace(":", "").Replace("#", "").Replace("]", "").Trim();
								}
							}
							else
							{
								if (idx_lts != -1 && idx_lte != -1)
								{
									iTemUniq = iTemDesc.Substring(idx_lts, idx_lte - idx_lts).Trim();

									iTemUniq = iTemUniq.Replace("[Buyer Item", "").Replace(":", "").Replace("#", "").Trim();
								}
							}
						}

						if (iTemUnit.Contains("Part No"))
							iTemUnit = string.Empty;


						//if (iTemCode.Length > 50 && iTemCode.Contains("\r\n"))
						if (iTemCode.Contains("\r\n"))
						{
							string[] iTemCodeStr = iTemCode.Split(new string[] { "\r\n" }, StringSplitOptions.None);

							if (iTemCodeStr.Length > 2)
							{
								if (iTemCodeStr[0].ToString().Contains("Part No"))
								{
									iTemCode = iTemCodeStr[0].ToString().Trim();
									iTemDesc = string.Empty;
								}

								for (int r = 1; r < iTemCodeStr.Length; r++)
								{
									iTemDesc = iTemDesc.Trim() + " " + iTemCodeStr[r].ToString().Trim();
								}
							}
							else if (iTemCodeStr.Length == 2)
							{
								if (iTemCodeStr[0].ToString().Contains("Part No"))
								{
									iTemCode = iTemCodeStr[0].ToString().Trim();

									if (!iTemCodeStr[1].ToString().ToUpper().Contains("KG"))
										iTemDesc = iTemCodeStr[1].ToString().Trim() + " " + iTemDesc;
								}
							}
						}

						if (iTemCode.Length > 50)
							iTemCode = itemSpl[v + 3].ToString().Trim();

						if (iTemSubj.StartsWith("RFQ No"))
							iTemSubj = string.Empty;


						if (iTemDesc.ToUpper().Contains("PART NO") && string.IsNullOrEmpty(iTemCode))
						{
							//idx_lts = -1;

							//idx_lts = iTemDesc.ToUpper().IndexOf("PART NO");

							//if (idx_lts != -1)
							//	iTemCode = iTemDesc.Substring(idx_lts, iTemDesc.Length - idx_lts).Trim();

							//iTemDesc = iTemDesc.Replace(iTemCode, "").Trim();

							//iTemCode = iTemCode.Replace("Part No", "").Replace(":", "").Replace("PART NO", "").Trim();
						}

						if (iTemDesc.ToUpper().Contains("ITEM:") && string.IsNullOrEmpty(iTemCode))
						{
							idx_lts = -1;

							idx_lts = iTemDesc.ToUpper().IndexOf("ITEM:");

							if (idx_lts != -1)
								iTemCode = iTemDesc.Substring(idx_lts, iTemDesc.Length - idx_lts).Trim();

							iTemDesc = iTemDesc.Replace(iTemCode, "").Trim();

							iTemCode = iTemCode.Replace("ITEM:", "").Replace("Item:", "").Replace("item:", "").Trim();

						}

						if (iTemDesc.Contains("Comments:"))
							iTemDesc = iTemDesc.Replace("Comments:", "\r\nComments:").Trim();

						if(iTemDesc.Contains("Maersk article number"))
						{
							int desc_s = iTemDesc.IndexOf("Maersk article number");


							if (desc_s != -1)
							{
								iTemUniq = iTemDesc.Substring(desc_s, iTemDesc.Length - desc_s).Replace("Maersk article number", "").Replace(":", "").Trim();
								iTemDesc = iTemDesc.Substring(0, desc_s).Trim();
							}
						}	


						iTemSubj = iTemSubj.ToLower().Replace("equipment section:", "").Trim();
						iTemSubj = iTemSubj.ToLower().Replace("\t", "").Trim();
						iTemSubj = iTemSubj.ToLower().Replace("\r\n ", "\r\n").Trim();
						iTemSubj = iTemSubj.ToLower().Replace("\r\n  ", "\r\n").Trim();
						iTemSubj = iTemSubj.ToLower().Replace("\r\n\r\n", "\r\n").Trim();
						iTemSubj = iTemSubj.ToLower().Replace("\r\n ", "\r\n").Trim();

						
						if(string.IsNullOrEmpty(iTemDesc))
						{
							iTemDesc = itemSpl[v + 4].ToString();
						}

						iTemCode = iTemCode.Replace("Manufacturer's Part No(MF)", "").Replace("Buyer's Part No(BP)", "").Replace("Supplier's Part No(VP)", "").Replace("IMPA No.(ZIM)", "").Trim();




						// Maersk A/S 맵스용
						if ((iTemSubj.ToUpper().Contains("HIMSEN") || iTemSubj.ToUpper().Contains("HYUNDAI") || iTemSubj.ToUpper().Contains("B&W")) && tnid.Equals("11327"))
						{
							//mapsInput(tnid);
						}


						// Scorpio 맵스용
						if ((iTemSubj.ToUpper().Contains("HIMSEN") || iTemSubj.ToUpper().Contains("HYUNDAI")) && tnid.Equals("10553"))
						{
							//mapsInput(tnid);
						}



						// ITEM ADD START
						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
						dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDesc;
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;
						if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
						if (!string.IsNullOrEmpty(iTemSubj)) dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSubj;
						dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode.Replace("Manufacturer's Part No(MF)", "").Replace("Buyer's Part No(BP)", "").Replace("Supplier's Part No(VP)", "").Replace("IMPA No.(ZIM)", "").Trim();

						iTemNo = string.Empty;
						iTemDesc = string.Empty;
						iTemUnit = string.Empty;
						iTemQt = string.Empty;
						iTemCode = string.Empty;
					}
				}
			}
		}

		public void mapsInput(string tid)
		{
			int idx_lts = -1; int idx_lte = -1;

			if (tid.Equals("11327"))
			{

				// 엔진모델
				idx_lts = -1; idx_lte = -1;

				if (iTemDesc.ToUpper().Contains("NAME 2:"))
				{
					string engineType = string.Empty;
					idx_lts = iTemDesc.ToUpper().IndexOf("NAME 2:");
					idx_lte = iTemDesc.ToUpper().IndexOf("COMMENTS");

					if (idx_lte == -1)
						idx_lte = iTemDesc.Length;

					if (idx_lts != -1)
					{
						engineType = iTemDesc.Substring(idx_lts, idx_lte - idx_lts).ToUpper().Trim();
						engineType = engineType.Replace("\r\n", "").Replace("NAME 2", "").Replace("NO", "").Replace(".", "").Replace(":", "").Trim();

						iTemSubj = iTemSubj.Trim() + " " + engineType;
					}
				}


				// 아이템 코드
				idx_lts = -1; idx_lte = -1;

				idx_lts = iTemSubj.ToUpper().IndexOf("DRAW. NO.");
				idx_lte = iTemSubj.ToUpper().IndexOf("TYPE:");

				if (idx_lte == -1)
					idx_lte = iTemSubj.Length;

				if (idx_lts != -1)
				{
					iTemCode = iTemSubj.Substring(idx_lts, idx_lte - idx_lts).ToUpper().Trim();
					iTemCode = iTemCode.Replace("\r\n", "").Replace("DRAW", "").Replace("NO", "").Replace(".", "").Replace(":", "").Replace("|", "-").Trim();

					if (iTemCode.EndsWith("-"))
						iTemCode = iTemCode.Substring(0, iTemCode.Length - 1).Trim();
				}
			}
			// 스콜피오
			else if (tid.Equals("10553"))
			{
				string codeStr = string.Empty;

				if(iTemDesc.StartsWith("/"))
				{
					codeStr = iTemDesc.Left(5);

					iTemDesc = iTemDesc.Replace(codeStr, "").Trim();

					iTemCode = iTemCode + codeStr;
				}

				if(iTemCode.Length == 15)
				{
					iTemCode = iTemCode.Left(6) + "-" + iTemCode.Right(3);
				}
				else if (iTemCode.Length == 18)
				{
					iTemCode = iTemCode.Replace("/", "-").Trim();
				}
				else if (iTemCode.Length == 20 && iTemCode.ToUpper().Contains("PLATE"))
				{
					iTemCode = iTemCode.ToUpper().Replace("PLATE", "").Trim();
				}
				else if (iTemCode.Length == 25)
				{
					iTemCode = iTemCode.ToUpper().Replace("PLATE", "").Replace("/","-").Replace(" ","").Trim();
				}
			}
		}
	}
}

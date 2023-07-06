using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class Shipserv_order_pdf
	{
		string vessel;
		string reference;
		string partner;
		string imoNumber;

		string fileno;
		string total;
		string currency;
		
			

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

		public string Partner
		{
			get
			{
				return partner;
			}
		}

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		public string ImoNumber
		{
			get
			{
				return imoNumber;
			}
		}

		public string FileNo
		{
			get
			{
				return fileno;
			}
		}

		public string Total
		{
			get
			{
				return total;
			}
		}

		public string Currency
		{
			get
			{
				return currency;
			}
		}

		#endregion ==================================================================================================== Constructor



		public Shipserv_order_pdf(string fileName)
		{
			vessel = "";                        // 선명
			reference = "";                     // 문의번호
			partner = "";                       // 매입처 담당자
			imoNumber = "";                     // 호선번호

			fileno = string.Empty;              // 파일번호
			total = string.Empty;               // Total Am
			currency = string.Empty;			// 환종

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



			// xml row 나누기
			string[] xmlSpl = { };

			if (!string.IsNullOrEmpty(xmlTemp))
			{
				xmlSpl = xmlTemp.Split(new string[] { "<row>" }, StringSplitOptions.None);
			}

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
					string firstColStr = dt.Rows[i][0].ToString().ToLower();


					if(string.IsNullOrEmpty(fileno))
					{
						for(int c = 0; c < dt.Columns.Count; c++)
						{
							if(dt.Rows[i][c].ToString().Contains("QOT Ref:"))
							{
								fileno = dt.Rows[i][c].ToString().Replace("QOT Ref:", "").Replace("(","").Replace(")","").Trim();

								if(string.IsNullOrEmpty(fileno))
								{
									fileno = dt.Rows[i+1][0].ToString().Replace("QOT Ref:", "").Replace("(", "").Replace(")", "").Trim();
								}
							}
							else if (dt.Rows[i][c].ToString().Contains("REF: YOUR QUOTATION"))
							{
								fileno = dt.Rows[i][c].ToString().Replace("REF:", "").Replace("YOUR QUOTATION", "").Replace("#","").Trim();
							}
							else if (dt.Rows[i][c].ToString().Contains("Your quotation Ref:"))
							{
								fileno = dt.Rows[i][c].ToString().Replace("Your quotation Ref:", "").Trim();
								fileno = fileno.Substring(0, 10).Trim();
							}
							else if (dt.Rows[i][c].ToString().Contains("Dintec ref"))
							{
								fileno = dt.Rows[i][c].ToString().Replace("Dintec ref", "").Trim();
							}
							else if (dt.Rows[i][c].ToString().Contains("SupplierReference"))
							{
								fileno = dt.Rows[i+1][0].ToString().Trim();
							}
							else if (dt.Rows[i][c].ToString().Contains("Vendor Reference"))
							{
								fileno = dt.Rows[i][c].ToString().Replace("Vendor Reference", "").Replace(":","").Replace("[","").Replace("]","").Trim();
							}
							else if (dt.Rows[i][c].ToString().Contains("Quote FB"))
							{
								fileno = dt.Rows[i][c].ToString().Replace("Quote", "").Trim();
							}
							else if (dt.Rows[i][c].ToString().ToLower().Contains("quotation fb"))
							{
								fileno = dt.Rows[i][c].ToString().ToLower().Replace("quotation", "").Trim();

								if(fileno.Length > 11)
								{
									int idx_s = fileno.IndexOf("fb");

									if(idx_s != -1)
									{
										fileno = fileno.Substring(idx_s, 10).Trim();
									}
								}
							}
						}
					}
					

					if(string.IsNullOrEmpty(total))
					{
						for(int c = 1; c < dt.Columns.Count; c++)
						{
							if(dt.Rows[i][c].ToString().Contains("Total ("))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if(string.IsNullOrEmpty(total))
										total = dt.Rows[i][c2].ToString().Trim();
								}
							}
						}
					}

					if(string.IsNullOrEmpty(currency))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("Currency: "))
							{
								currency  = dt.Rows[i][c].ToString().Replace("Currency:", "").Trim();
							}
						}
					}

					if(string.IsNullOrEmpty(reference))
					{
						for(int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("ORD Ref:"))
							{
								for(int c2 = c+1; c2 < dt.Columns.Count; c2++)
								{
									if(string.IsNullOrEmpty(reference))
										reference = dt.Rows[i][c2].ToString().Replace("ORD Ref:", "").Trim();
								}
							}
						}
					}


					if (string.IsNullOrEmpty(imoNumber))
					{
						for (int c =  0; c < dt.Columns.Count; c++)
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
							}
						}
					}



					if (firstColStr.Equals("#"))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							//if (dt.Rows[i][c].ToString().Equals("Aricle")) _itemDesc = c;
							//else if (dt.Rows[i][c].ToString().Equals("Order Ref.")) _itemCode = c;
							//else if (dt.Rows[i][c].ToString().Equals("Qty.")) _itemQt = c;
							//else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
						}
					}
					else if (GetTo.IsInt(firstColStr))
					{
						////ITEM ADD START
						//dtItem.Rows.Add();
						//dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
						//dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
						//dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
						//if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
						//if (!string.IsNullOrEmpty(iTemSUBJ))
						//	dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
						//dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
					}
				}
			}
		}
	}
}

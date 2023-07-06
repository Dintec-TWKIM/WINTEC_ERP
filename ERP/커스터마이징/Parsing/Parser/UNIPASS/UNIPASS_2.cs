using Dintec;
using Duzon.Common.Forms;
using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parsing.Parser.UNIPASS
{
	public class UNIPASS_2
	{
		DataTable dtItemH;
		DataTable dtItemL;

		string NO_IMPORT;

		string DT_ARRIVAL;
		string NO_BL;
		string NO_BL_MASTER;
		string NO_CARGO;
		string TAXABLE_KRW;
		string TAXABLE_USD;
		//string TAXABLE_KRW_TOTAL;
		//string TAXABLE_USD_TOTAL;
		string TAXABLE_VAT;
		string CUSTOMS;
		string VAT;
		string DT_IMPORT;
		string CD_OFFICE;
		string CURRENCY;
		string EXCHANGE_RATE;
		string HSCODE;
		string ORIGIN;
		string DECLARANT;
		string IMPORTER;
		string TAXPAYER;
		string FORWARDER;
		string TRADER;
		string GROSS_WEIGHT;
		string ARRIVAL_PORT;
		string EXPORT_COUNTRY;
		string NET_WEIGHT;
		string TAX_CUSTOMS;
		string TAX_CONSUMPTION;
		string TAX_ENERGY;
		string TAX_LIQUOR;
		string TAX_EDUCATION;
		string TAX_RURAL;
		string TAX_VAT;
		string TAX_LATE_REPORT;
		string TAX_NO_REPORT;

		string NO_RAN = string.Empty;

		bool firstYN = false;

		string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
		string NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;

		string fileName;

		#region ==================================================================================================== Property

		public DataTable ItemH
		{
			get
			{
				return dtItemH;
			}
		}


		public DataTable ItemL
		{
			get
			{
				return dtItemL;
			}
		}



		#endregion

		#region ==================================================================================================== Constructor

		public UNIPASS_2(string fileName)
		{
			NO_IMPORT = string.Empty;
			DT_ARRIVAL = string.Empty;
			NO_BL = string.Empty;
			NO_CARGO = string.Empty;
			TAXABLE_KRW = string.Empty;
			TAXABLE_USD = string.Empty;
			//TAXABLE_KRW_TOTAL = string.Empty;
			//TAXABLE_USD_TOTAL = string.Empty;
			TAXABLE_VAT = string.Empty;
			CUSTOMS = string.Empty;
			VAT = string.Empty;
			DT_IMPORT = string.Empty;
			CD_OFFICE = string.Empty;
			CURRENCY = string.Empty;
			EXCHANGE_RATE = string.Empty;
			HSCODE = string.Empty;
			ORIGIN = string.Empty;
			DECLARANT = string.Empty;
			IMPORTER = string.Empty;
			TAXPAYER = string.Empty;
			FORWARDER = string.Empty;
			TRADER = string.Empty;
			GROSS_WEIGHT = string.Empty;
			NO_BL_MASTER = string.Empty;
			ARRIVAL_PORT = string.Empty;
			EXPORT_COUNTRY = string.Empty;
			NET_WEIGHT = string.Empty;

			TAX_CUSTOMS = string.Empty;
			TAX_CONSUMPTION = string.Empty;
			TAX_ENERGY = string.Empty;
			TAX_LIQUOR = string.Empty;
			TAX_EDUCATION = string.Empty;
			TAX_RURAL = string.Empty;
			TAX_VAT = string.Empty;
			TAX_LATE_REPORT = string.Empty;
			TAX_NO_REPORT = string.Empty;






			dtItemH = new DataTable();
			dtItemL = new DataTable();


			// ======= HEAD =======
			dtItemH.Columns.Add("CD_COMPANY");      // 회사코드
			dtItemH.Columns.Add("NO_IMPORT");		// 신고번호
			dtItemH.Columns.Add("DT_IMPORT");		// 신고일
			dtItemH.Columns.Add("CD_OFFICE");		// 세관
			dtItemH.Columns.Add("DT_ARRIVAL");      // 입항일
			dtItemH.Columns.Add("NO_BL");           // BL번호
			dtItemH.Columns.Add("NO_BL_MASTER");    // BL번호(마스터)
			dtItemH.Columns.Add("NO_CARGO");        // 화물관리번호
			dtItemH.Columns.Add("GROSS_WEIGHT");    // 총중량
			dtItemH.Columns.Add("ARRIVAL_PORT");    // 국내도착항
			dtItemH.Columns.Add("EXPORT_COUNTRY");  // 적출국

			dtItemH.Columns.Add("DECLARANT");       // 신고인
			dtItemH.Columns.Add("IMPORTER");        // 수입자
			dtItemH.Columns.Add("TAXPAYER");        // 납세의무자
			dtItemH.Columns.Add("FORWARDER");       // 운송주선인
			dtItemH.Columns.Add("TRADER");          // 무역거래처

			dtItemH.Columns.Add("TAXABLE_KRW_TOTAL");  // 과세가격 WON TOTAL
			dtItemH.Columns.Add("TAXABLE_USD_TOTAL");  // 과세가격 USD TOTAL
			dtItemH.Columns.Add("TAXABLE_VAT");        // 부가가치세과표
			dtItemH.Columns.Add("CURRENCY");           // 화폐단위
			dtItemH.Columns.Add("EXCHANGE_RATE");      // 환율

			dtItemH.Columns.Add("TAX_CUSTOMS");       // 관세
			dtItemH.Columns.Add("TAX_CONSUMPTION");   // 개별소비세
			dtItemH.Columns.Add("TAX_ENERGY");        // 교통에너지환경세
			dtItemH.Columns.Add("TAX_LIQUOR");        // 주세
			dtItemH.Columns.Add("TAX_EDUCATION");     // 교육세
			dtItemH.Columns.Add("TAX_RURAL");         // 농어촌특별세
			dtItemH.Columns.Add("TAX_VAT");           // 부가가치세
			dtItemH.Columns.Add("TAX_LATE_REPORT");   // 신고지연가산세
			dtItemH.Columns.Add("TAX_NO_REPORT");     // 미신고가산세



			// ======= LINE =======
			dtItemL.Columns.Add("CD_COMPANY");      // 회사코드
			dtItemL.Columns.Add("NO_IMPORT");		// 신고번호
			dtItemL.Columns.Add("SEQ");             // 라인
			dtItemL.Columns.Add("NO_RAN");          // 번호
			dtItemL.Columns.Add("NO_DSP");          // 번호
			dtItemL.Columns.Add("MODEL");           // 모델
			dtItemL.Columns.Add("QT");              // 수량
			dtItemL.Columns.Add("UNIT");            // 단위
			dtItemL.Columns.Add("UM");              // 단가
			dtItemL.Columns.Add("AM");              // 금액

			dtItemL.Columns.Add("HSCODE");          // 세번부호 HSCODE
			dtItemL.Columns.Add("NET_WEIGHT");      // 순중량
			dtItemL.Columns.Add("TAXABLE_USD");     // 과세가격 USD
			dtItemL.Columns.Add("TAXABLE_KRW");     // 과세가격 WON
			dtItemL.Columns.Add("CUSTOMS");         // 관세액
			dtItemL.Columns.Add("VAT");             // 부가세액
			dtItemL.Columns.Add("ORIGIN");          // 원산지


			this.fileName = fileName;
		}

		#endregion


		public void Parse()
		{
			string MODEL = string.Empty;
			string QT = string.Empty;
			string UM = string.Empty;
			string AM = string.Empty;
			string UNIT = string.Empty;
			string SEQ = string.Empty;

			int seqCount = 0;

			string itemNo = string.Empty;


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


			// 아이템 항목 체크
			Regex regex = new Regex(@"\(NO\. .+\)");

			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string firstColStr = dt.Rows[i][0].ToString().Trim();
					string secondColStr = firstColStr + dt.Rows[i][1].ToString().Trim();


					#region dtitemH
					// 신고번호
					if (string.IsNullOrEmpty(NO_IMPORT))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("신고번호"))
							{
								NO_IMPORT = dt.Rows[i + 1][c].ToString().Trim();

								if (string.IsNullOrEmpty(NO_IMPORT))
								{
									if (c != 0)
										NO_IMPORT = dt.Rows[i + 1][c - 1].ToString().Trim();
									else
										NO_IMPORT = dt.Rows[i + 1][c + 1].ToString().Trim();
								}
							}

							if (NO_IMPORT.Length != 16)
							{
								NO_IMPORT = "";
							}
						}
					}

					// 신고일
					if (string.IsNullOrEmpty(DT_IMPORT))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("신고일"))
							{
								DT_IMPORT = dt.Rows[i + 1][c].ToString().Replace("/", "").Trim();
							}
						}
					}


					// 세관과
					if (string.IsNullOrEmpty(CD_OFFICE))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("세관.과"))
							{
								CD_OFFICE = dt.Rows[i + 1][c].ToString().Replace("/", "").Trim();

								if (string.IsNullOrEmpty(CD_OFFICE))
									CD_OFFICE = dt.Rows[i + 1][c + 1].ToString().Replace("/", "").Trim();
							}
						}
					}

					// 입항일
					if (string.IsNullOrEmpty(DT_ARRIVAL))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("입항일"))
							{
								DT_ARRIVAL = dt.Rows[i + 1][c].ToString().Replace("/", "").Trim();
							}
						}
					}

					// BL번호
					if (string.IsNullOrEmpty(NO_BL))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("B/L(AWB)번호"))
							{
								NO_BL = dt.Rows[i + 1][c].ToString().Trim();
							}
						}
					}


					// 화물관리번호
					if (string.IsNullOrEmpty(NO_CARGO))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("5화물관리번호"))
							{
								NO_CARGO = dt.Rows[i + 1][c].ToString().Trim();

								if (string.IsNullOrEmpty(NO_CARGO))
								{
									NO_CARGO = dt.Rows[i + 1][c + 1].ToString().Trim();
								}
							}
						}
					}


					// 총중량
					if (string.IsNullOrEmpty(GROSS_WEIGHT))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("21총중량"))
							{
								GROSS_WEIGHT = dt.Rows[i + 1][c + 1].ToString().Replace("KG", "").Trim() + dt.Rows[i + 1][c + 2].ToString().Replace("KG", "").Trim();

								if (string.IsNullOrEmpty(GROSS_WEIGHT))
									GROSS_WEIGHT = dt.Rows[i + 1][c + 1].ToString().Replace("KG", "").Trim();

								if (string.IsNullOrEmpty(GROSS_WEIGHT))
									GROSS_WEIGHT = dt.Rows[i + 1][c].ToString().Replace("KG", "").Trim();

								GROSS_WEIGHT = GROSS_WEIGHT.Replace(",", "").Trim();

								if(!GetTo.IsInt(GROSS_WEIGHT.Replace(".","")))
								{
									GROSS_WEIGHT = dt.Rows[i + 1][c + 3].ToString().Replace("KG","").Trim();
								}

								GROSS_WEIGHT = GROSS_WEIGHT.Replace(",", "").Trim();
							}
						}
					}



					// 국내도착항
					if (string.IsNullOrEmpty(ARRIVAL_PORT))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("23국내도착항"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if (string.IsNullOrEmpty(ARRIVAL_PORT))
										ARRIVAL_PORT = dt.Rows[i][c2].ToString().Trim();
									else
										break;
								}
							}
						}
					}

					// 적출국
					if (string.IsNullOrEmpty(EXPORT_COUNTRY))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("25적출국"))
							{
								EXPORT_COUNTRY = dt.Rows[i][c + 1].ToString().Trim() + dt.Rows[i][c + 2].ToString().Trim();

								EXPORT_COUNTRY = EXPORT_COUNTRY.Substring(0, 2).Trim() + " " + EXPORT_COUNTRY.Substring(2, EXPORT_COUNTRY.Length - 2).Trim();
							}
						}
					}


					// BL번호(MASTER)
					if (string.IsNullOrEmpty(NO_BL_MASTER))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("MASTERB/L번호"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if (dt.Rows[i][c2].ToString().Replace(" ", "").StartsWith("28운수기관부호"))
										break;
									else if (string.IsNullOrEmpty(NO_BL_MASTER))
										NO_BL_MASTER = dt.Rows[i][c2].ToString().Trim();
								}

							}
						}
					}


					// 화폐단위
					if (string.IsNullOrEmpty(CURRENCY))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("36단가"))
							{
								CURRENCY = dt.Rows[i][c].ToString().Trim();

								CURRENCY = CURRENCY.Replace(" ", "").Replace("36", "").Replace("단가", "").Replace("(", "").Replace(")", "").Trim();
							}
						}
					}


					// 환율
					if (string.IsNullOrEmpty(EXCHANGE_RATE))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("환율"))
							{
								EXCHANGE_RATE = dt.Rows[i][c + 1].ToString().Trim();

								EXCHANGE_RATE = EXCHANGE_RATE.Replace(",", "").Trim();
							}
						}
					}


					// 부가가치세과표
					if (string.IsNullOrEmpty(TAXABLE_VAT))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("부가가치세과표"))
							{
								TAXABLE_VAT = dt.Rows[i][c + 1].ToString().Trim();

								if (string.IsNullOrEmpty(TAXABLE_VAT))
								{
									TAXABLE_VAT = dt.Rows[i][c + 2].ToString().Trim();
								}

								TAXABLE_VAT = TAXABLE_VAT.Replace(",", "").Trim();
							}
						}
					}

					//// 총과세가격
					//if (string.IsNullOrEmpty(TAXABLE_KRW_TOTAL))
					//{
					//	for (int c = 0; c < dt.Columns.Count; c++)
					//	{
					//		if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("55총과세가격"))
					//		{
					//			TAXABLE_USD_TOTAL = dt.Rows[i - 1][c + 1].ToString().Trim();
					//			TAXABLE_KRW_TOTAL = dt.Rows[i + 1][c + 1].ToString().Trim();

					//			if (string.IsNullOrEmpty(TAXABLE_USD_TOTAL.Replace("$", "").Trim()))
					//			{
					//				TAXABLE_USD_TOTAL = dt.Rows[i - 1][c + 2].ToString().Trim();
					//			}


					//			if (string.IsNullOrEmpty(TAXABLE_KRW_TOTAL.Replace("￦", "").Trim()))
					//			{
					//				TAXABLE_KRW_TOTAL = dt.Rows[i + 1][c + 2].ToString().Trim();
					//			}

					//			TAXABLE_USD_TOTAL = TAXABLE_USD_TOTAL.Replace("$", "").Replace(",", "").Trim();
					//			TAXABLE_KRW_TOTAL = TAXABLE_KRW_TOTAL.Replace("￦", "").Replace(",", "").Trim();
					//		}
					//	}
					//}

					#endregion dtitemH


					#region dtitemL


					// 란번호 무조건 돌림
					for (int c = 0; c < dt.Columns.Count; c++)
					{
						if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("(란번호/총란수"))
						{
							NO_RAN = dt.Rows[i][c + 1].ToString().Trim() + dt.Rows[i][c + 2].ToString().Trim();

							string[] ranSpl = NO_RAN.Split('/');

							if (ranSpl.Length == 2)
							{
								NO_RAN = ranSpl[0].ToString().Trim();
							}
						}
					}


					// 과세가격 won&usd
					if (string.IsNullOrEmpty(TAXABLE_KRW))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("39과세가격"))
							{
								TAXABLE_USD = dt.Rows[i - 1][c + 1].ToString().Trim();
								TAXABLE_KRW = dt.Rows[i + 1][c + 1].ToString().Trim();

								if (string.IsNullOrEmpty(TAXABLE_USD.Replace("$", "").Trim()))
								{
									TAXABLE_USD = dt.Rows[i - 1][c + 2].ToString().Trim();
								}


								if (string.IsNullOrEmpty(TAXABLE_KRW.Replace("￦", "").Trim()))
								{
									TAXABLE_KRW = dt.Rows[i + 1][c + 2].ToString().Trim();
								}

								TAXABLE_USD = TAXABLE_USD.Replace("$", "").Replace(",", "").Trim();
								TAXABLE_KRW = TAXABLE_KRW.Replace("￦", "").Replace(",", "").Trim();
							}
						}
					}


					// 세번부호
					if (string.IsNullOrEmpty(HSCODE))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("38세번부호"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if (string.IsNullOrEmpty(HSCODE))
										HSCODE = HSCODE + dt.Rows[i][c2].ToString().Trim();
								}
							}
						}
					}


					// 순중량
					if (string.IsNullOrEmpty(NET_WEIGHT))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("40순중량"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if (string.IsNullOrEmpty(NET_WEIGHT))
										NET_WEIGHT = dt.Rows[i][c2].ToString().Replace("KG", "").Trim();
								}

								NET_WEIGHT = NET_WEIGHT.Replace(",", "").Trim();
							}
						}
					}



					// 원산지
					if (string.IsNullOrEmpty(ORIGIN))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("46원산지"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if (string.IsNullOrEmpty(ORIGIN))
										ORIGIN = ORIGIN + dt.Rows[i][c2].ToString().Trim();
									else
										break;
								}
							}
						}
					}



					// 세액
					if (string.IsNullOrEmpty(CUSTOMS) && string.IsNullOrEmpty(VAT))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").Contains("52세액"))
							{
								CUSTOMS = dt.Rows[i + 1][c + 1].ToString().Trim();
								VAT = dt.Rows[i + 2][c + 1].ToString().Trim();

								CUSTOMS = CUSTOMS.Replace(",", "").Trim();

								if (!GetTo.IsInt(CUSTOMS))
									CUSTOMS = dt.Rows[i + 1][c + 2].ToString().Trim();

								if (string.IsNullOrEmpty(CUSTOMS))
									CUSTOMS = dt.Rows[i + 1][c].ToString().Trim();

								if (string.IsNullOrEmpty(VAT))
									VAT = dt.Rows[i + 2][c].ToString().Trim();


								CUSTOMS = CUSTOMS.Replace(",", "").Trim();
								VAT = VAT.Replace(",", "").Trim();

								if (firstYN)
								{
									for (int r = 0; r < dtItemL.Rows.Count; r++)
									{
										if (string.IsNullOrEmpty(dtItemL.Rows[r]["HSCODE"].ToString()))
										{
											//dtItemL.Rows[r]["CD_COMPANY"] = CD_COMPANY;
											//dtItemL.Rows[r]["NO_IMPORT"] = NO_IMPORT;
											dtItemL.Rows[r]["HSCODE"] = HSCODE;
											dtItemL.Rows[r]["NET_WEIGHT"] = NET_WEIGHT;
											dtItemL.Rows[r]["TAXABLE_USD"] = TAXABLE_USD;
											dtItemL.Rows[r]["TAXABLE_KRW"] = TAXABLE_KRW;
											dtItemL.Rows[r]["CUSTOMS"] = CUSTOMS;
											dtItemL.Rows[r]["VAT"] = VAT;
											dtItemL.Rows[r]["ORIGIN"] = ORIGIN;
											dtItemL.Rows[r]["NO_RAN"] = NO_RAN;
										}
									}

									firstYN = false;
								}
							}

						}



					}


					// 신고인
					if (firstColStr.Replace(" ", "").StartsWith("10신"))
					{
						DECLARANT = dt.Rows[i][1].ToString().Trim() + dt.Rows[i][2].ToString().Trim() + dt.Rows[i][3].ToString().Trim();

						DECLARANT = DECLARANT.Replace("고인", "").Trim();
					}
					// 수입자
					else if (firstColStr.Replace(" ", "").StartsWith("11수"))
					{
						IMPORTER = dt.Rows[i - 1][1].ToString().Trim();

						if (!IMPORTER.StartsWith("(주)") && !IMPORTER.StartsWith("자"))
						{
							IMPORTER = dt.Rows[i][2].ToString().Trim();
						}

						if (IMPORTER.StartsWith("자"))
						{
							IMPORTER = IMPORTER.Substring(1, IMPORTER.Length - 1).Trim();
						}
					}
					// 납세의무자
					else if (firstColStr.Replace(" ", "").StartsWith("12납세의무자"))
					{
						TAXPAYER = firstColStr.Replace("12 납세의무자", "").Trim();
					}
					// 운송주선인
					else if (firstColStr.Replace(" ", "").StartsWith("13운송주선인"))
					{
						FORWARDER = firstColStr.Replace("13 운송주선인", "").Trim();
					}
					// 무역거래처
					else if (firstColStr.Replace(" ", "").StartsWith("14무역거래처"))
					{
						TRADER = firstColStr.Replace("14 무역거래처", "").Trim() + dt.Rows[i + 1][1].ToString().Trim();
					}

					// 신고인
					else if (secondColStr.Replace(" ", "").StartsWith("10신"))
					{
						DECLARANT = dt.Rows[i][2].ToString().Trim() + dt.Rows[i][3].ToString().Trim() + dt.Rows[i][4].ToString().Trim();

						DECLARANT = DECLARANT.Replace("고인", "").Trim();
					}
					// 수입자
					else if (secondColStr.Replace(" ", "").StartsWith("11수"))
					{
						IMPORTER = dt.Rows[i - 1][2].ToString().Trim();

						if (!IMPORTER.StartsWith("(주)") && !IMPORTER.StartsWith("자"))
						{
							IMPORTER = dt.Rows[i][3].ToString().Trim();
						}

						if (IMPORTER.StartsWith("자"))
						{
							IMPORTER = IMPORTER.Substring(1, IMPORTER.Length - 1).Trim();
						}
					}
					// 납세의무자
					else if (secondColStr.Replace(" ", "").StartsWith("12납세의무자"))
					{
						TAXPAYER = secondColStr.Replace("12 납세의무자", "").Trim();
					}
					// 운송주선인
					else if (secondColStr.Replace(" ", "").StartsWith("13운송주선인"))
					{
						FORWARDER = secondColStr.Replace("13 운송주선인", "").Trim();
					}
					// 무역거래처
					else if (secondColStr.Replace(" ", "").StartsWith("14무역거래처"))
					{
						TRADER = secondColStr.Replace("14 무역거래처", "").Trim() + dt.Rows[i + 1][2].ToString().Trim();
					}

					#region 세금 모음
					// 관세
					if (string.IsNullOrEmpty(TAX_CUSTOMS))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").StartsWith("관세"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if (string.IsNullOrEmpty(TAX_CUSTOMS))
										TAX_CUSTOMS = TAX_CUSTOMS + dt.Rows[i][c2].ToString();
									else
										break;
								}
							}
						}

						TAX_CUSTOMS = TAX_CUSTOMS.Replace(",", "").Trim();
					}

					// 개별소비세
					if (string.IsNullOrEmpty(TAX_CONSUMPTION))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").StartsWith("개별소비세"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if (string.IsNullOrEmpty(TAX_CONSUMPTION))
										TAX_CONSUMPTION = TAX_CONSUMPTION + dt.Rows[i][c2].ToString();
									else
										break;
								}
							}
						}

						TAX_CONSUMPTION = TAX_CONSUMPTION.Replace(",", "").Trim();
					}


					// 교통에너지환경세
					if (string.IsNullOrEmpty(TAX_ENERGY))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").StartsWith("교통에너지환경세"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if (string.IsNullOrEmpty(TAX_ENERGY))
										TAX_ENERGY = TAX_ENERGY + dt.Rows[i][c2].ToString();
									else
										break;
								}
							}
						}

						TAX_ENERGY = TAX_ENERGY.Replace(",", "").Trim();
					}

					// 주세
					if (string.IsNullOrEmpty(TAX_LIQUOR))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").StartsWith("주세"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if (string.IsNullOrEmpty(TAX_LIQUOR))
										TAX_LIQUOR = TAX_LIQUOR + dt.Rows[i][c2].ToString();
									else
										break;
								}
							}
						}

						TAX_LIQUOR = TAX_LIQUOR.Replace(",", "").Trim();
					}


					// 교육세
					if (string.IsNullOrEmpty(TAX_EDUCATION))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").StartsWith("교육세"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if (string.IsNullOrEmpty(TAX_EDUCATION))
										TAX_EDUCATION = TAX_EDUCATION + dt.Rows[i][c2].ToString();
									else
										break;
								}
							}
						}

						TAX_EDUCATION = TAX_EDUCATION.Replace(",", "").Trim();
					}

					// 농어촌특별세
					if (string.IsNullOrEmpty(TAX_RURAL))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").StartsWith("농어촌특별세"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if (string.IsNullOrEmpty(TAX_RURAL))
										TAX_RURAL = TAX_RURAL + dt.Rows[i][c2].ToString();
									else
										break;
								}
							}
						}

						TAX_RURAL = TAX_RURAL.Replace(",", "").Trim();
					}


					// 부가가치세
					if (string.IsNullOrEmpty(TAX_VAT))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").StartsWith("부가가치세"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if (string.IsNullOrEmpty(TAX_VAT))
										TAX_VAT = TAX_VAT + dt.Rows[i][c2].ToString();
									else
										break;
								}
							}
						}

						TAX_VAT = TAX_VAT.Replace(",", "").Trim();
					}

					// 신고지연가산세
					if (string.IsNullOrEmpty(TAX_LATE_REPORT))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").StartsWith("신고지연가산세"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if (string.IsNullOrEmpty(TAX_LATE_REPORT))
										TAX_LATE_REPORT = TAX_LATE_REPORT + dt.Rows[i][c2].ToString();
									else
										break;
								}
							}
						}

						TAX_LATE_REPORT = TAX_LATE_REPORT.Replace(",", "").Trim();
					}

					// 미신고가산세
					if (string.IsNullOrEmpty(TAX_NO_REPORT))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Replace(" ", "").StartsWith("미신고가산세"))
							{
								for (int c2 = c + 1; c2 < dt.Columns.Count; c2++)
								{
									if (string.IsNullOrEmpty(TAX_NO_REPORT))
										TAX_NO_REPORT = TAX_NO_REPORT + dt.Rows[i][c2].ToString();
									else
										break;
								}

								TAX_NO_REPORT = TAX_NO_REPORT.Replace(",", "").Trim();



								for (int r = 0; r < dtItemL.Rows.Count; r++)
								{
									if (string.IsNullOrEmpty(dtItemL.Rows[r]["HSCODE"].ToString()))
									{
										//dtItemL.Rows[r]["CD_COMPANY"] = CD_COMPANY;
										//dtItemL.Rows[r]["NO_IMPORT"] = NO_IMPORT;
										dtItemL.Rows[r]["HSCODE"] = HSCODE;
										dtItemL.Rows[r]["NET_WEIGHT"] = NET_WEIGHT;
										dtItemL.Rows[r]["TAXABLE_USD"] = TAXABLE_USD;
										dtItemL.Rows[r]["TAXABLE_KRW"] = TAXABLE_KRW;
										dtItemL.Rows[r]["CUSTOMS"] = CUSTOMS;
										dtItemL.Rows[r]["VAT"] = VAT;
										dtItemL.Rows[r]["ORIGIN"] = ORIGIN;
										dtItemL.Rows[r]["NO_RAN"] = NO_RAN;
									}
								}


								dtItemH.Rows.Add();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["CD_COMPANY"] = CD_COMPANY;
								dtItemH.Rows[dtItemH.Rows.Count - 1]["NO_IMPORT"] = NO_IMPORT;
								dtItemH.Rows[dtItemH.Rows.Count - 1]["DT_IMPORT"] = DT_IMPORT;
								dtItemH.Rows[dtItemH.Rows.Count - 1]["CD_OFFICE"] = CD_OFFICE;
								dtItemH.Rows[dtItemH.Rows.Count - 1]["DT_ARRIVAL"] = DT_ARRIVAL;
								dtItemH.Rows[dtItemH.Rows.Count - 1]["NO_BL"] = NO_BL.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["NO_BL_MASTER"] = NO_BL_MASTER.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["NO_CARGO"] = NO_CARGO.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["DECLARANT"] = DECLARANT.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["IMPORTER"] = IMPORTER.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["TAXPAYER"] = TAXPAYER.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["FORWARDER"] = FORWARDER.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["TRADER"] = TRADER.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["GROSS_WEIGHT"] = GROSS_WEIGHT.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["ARRIVAL_PORT"] = ARRIVAL_PORT.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["EXPORT_COUNTRY"] = EXPORT_COUNTRY.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["CURRENCY"] = CURRENCY.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["EXCHANGE_RATE"] = EXCHANGE_RATE.Replace(" ", "").Trim();
								//dtItemH.Rows[dtItemH.Rows.Count - 1]["TAXABLE_USD_TOTAL"] = TAXABLE_USD_TOTAL;
								//dtItemH.Rows[dtItemH.Rows.Count - 1]["TAXABLE_KRW_TOTAL"] = TAXABLE_KRW_TOTAL;
								dtItemH.Rows[dtItemH.Rows.Count - 1]["TAXABLE_VAT"] = TAXABLE_VAT.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["TAX_CUSTOMS"] = TAX_CUSTOMS.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["TAX_CONSUMPTION"] = TAX_CONSUMPTION.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["TAX_ENERGY"] = TAX_ENERGY.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["TAX_LIQUOR"] = TAX_LIQUOR.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["TAX_EDUCATION"] = TAX_EDUCATION.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["TAX_RURAL"] = TAX_RURAL.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["TAX_VAT"] = TAX_VAT.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["TAX_LATE_REPORT"] = TAX_LATE_REPORT.Replace(" ", "").Trim();
								dtItemH.Rows[dtItemH.Rows.Count - 1]["TAX_NO_REPORT"] = TAX_NO_REPORT.Replace(" ", "").Trim();

							}
						}


					}
					#endregion 세금 모음

					#endregion dtitemL


					// 아이템 등록
					if (regex.IsMatch(firstColStr))
					{
						seqCount += 1;
						SEQ = Convert.ToString(seqCount);

						itemNo = firstColStr.Replace("(", "").Replace(")", "").Replace("NO.", "").Trim();


						if (Convert.ToInt32(itemNo) == 1 && dtItemL.Rows.Count > 0)
						{
							firstYN = true;
							TAXABLE_KRW = string.Empty;
							TAXABLE_USD = string.Empty;
							CUSTOMS = string.Empty;
							VAT = string.Empty;
							CURRENCY = string.Empty;
							HSCODE = string.Empty;
							ORIGIN = string.Empty;
							NET_WEIGHT = string.Empty;
							//NO_RAN = string.Empty;
						}
						//else
						//{
						//	firstYN = false;
						//}


						MODEL = dt.Rows[i + 1][0].ToString().Trim() + Environment.NewLine + dt.Rows[i + 2][0].ToString().Trim();

						for (int c = 2; c < dt.Columns.Count; c++)
						{
							if (string.IsNullOrEmpty(QT))
							{
								QT = dt.Rows[i + 1][c].ToString().Trim();

								if (!string.IsNullOrEmpty(QT))
								{
									string[] qtSpl = QT.Split(' ');

									if (qtSpl.Length == 2)
									{
										QT = qtSpl[0].ToString().Trim();
										UNIT = qtSpl[1].ToString().Trim();
									}

									if (!GetTo.IsInt(QT.Replace(".","").Replace(",","")))
									{
										QT = string.Empty;
										UNIT = string.Empty;
									}
								}
							}
							else if (string.IsNullOrEmpty(UNIT))
							{
								UNIT = dt.Rows[i + 1][c].ToString().Replace(",", "").Trim();

								if (!string.IsNullOrEmpty(UNIT))
								{
									if (GetTo.IsInt(UNIT))
									{
										UNIT = "X";

										c = c - 1;
									}
								}
								else
								{
									UNIT = "X"; 
								}
							}
							else if (string.IsNullOrEmpty(UM))
							{
								UM = dt.Rows[i + 1][c].ToString().Replace(",", "").Trim();
							}
							else if (string.IsNullOrEmpty(AM))
							{
								AM = dt.Rows[i + 1][c].ToString().Replace(",", "").Trim();
							}
						}


						if(string.IsNullOrEmpty(QT) && string.IsNullOrEmpty(UM) && string.IsNullOrEmpty(AM))
						{
							for (int c = 2; c < dt.Columns.Count; c++)
							{
								if (string.IsNullOrEmpty(QT))
								{
									QT = dt.Rows[i + 2][c].ToString().Trim();

									if (!string.IsNullOrEmpty(QT))
									{
										string[] qtSpl = QT.Split(' ');

										if (qtSpl.Length == 2)
										{
											QT = qtSpl[0].ToString().Trim();
											UNIT = qtSpl[1].ToString().Trim();
										}

										if(!GetTo.IsInt(QT.Replace(".", "").Replace(",", "")))
										{
											QT = string.Empty;
											UNIT = string.Empty;
										}
									}
								}
								else if (string.IsNullOrEmpty(UNIT))
								{
									UNIT = dt.Rows[i + 2][c].ToString().Replace(",", "").Trim();

									if (!string.IsNullOrEmpty(UNIT))
									{
										if (GetTo.IsInt(UNIT))
										{
											UNIT = "X";
											c = c - 1;
										}
									}
									else
									{
										UNIT = "X";
									}
								}
								else if (string.IsNullOrEmpty(UM))
								{
									UM = dt.Rows[i + 2][c].ToString().Replace(",", "").Trim();
								}
								else if (string.IsNullOrEmpty(AM))
								{
									AM = dt.Rows[i + 2][c].ToString().Replace(",", "").Trim();
								}
							}
						}


						if(UNIT == "X")
						{
							UNIT = string.Empty;
						}

						dtItemL.Rows.Add();
						dtItemL.Rows[dtItemL.Rows.Count - 1]["CD_COMPANY"] = CD_COMPANY;
						dtItemL.Rows[dtItemL.Rows.Count - 1]["NO_IMPORT"] = NO_IMPORT;
						dtItemL.Rows[dtItemL.Rows.Count - 1]["SEQ"] = SEQ;
						dtItemL.Rows[dtItemL.Rows.Count - 1]["NO_DSP"] = itemNo.Replace(" ", "").Trim();
						dtItemL.Rows[dtItemL.Rows.Count - 1]["MODEL"] = MODEL;
						dtItemL.Rows[dtItemL.Rows.Count - 1]["QT"] = QT.Replace(" ", "").Replace(",","").Trim();
						dtItemL.Rows[dtItemL.Rows.Count - 1]["UNIT"] = UNIT.Replace(" ", "").Trim();
						dtItemL.Rows[dtItemL.Rows.Count - 1]["UM"] = UM.Replace(" ","").Trim();
						dtItemL.Rows[dtItemL.Rows.Count - 1]["AM"] = AM.Replace(" ","").Trim();
						dtItemL.Rows[dtItemL.Rows.Count - 1]["HSCODE"] = HSCODE;
						dtItemL.Rows[dtItemL.Rows.Count - 1]["NET_WEIGHT"] = NET_WEIGHT;
						dtItemL.Rows[dtItemL.Rows.Count - 1]["TAXABLE_USD"] = TAXABLE_USD;
						dtItemL.Rows[dtItemL.Rows.Count - 1]["TAXABLE_KRW"] = TAXABLE_KRW;
						dtItemL.Rows[dtItemL.Rows.Count - 1]["CUSTOMS"] = CUSTOMS;
						dtItemL.Rows[dtItemL.Rows.Count - 1]["VAT"] = VAT.Replace(" ", "").Trim();
						dtItemL.Rows[dtItemL.Rows.Count - 1]["ORIGIN"] = ORIGIN;
						dtItemL.Rows[dtItemL.Rows.Count - 1]["NO_RAN"] = NO_RAN;

						MODEL = string.Empty;
						UM = string.Empty;
						QT = string.Empty;
						UNIT = string.Empty;
						AM = string.Empty;
					}
				}
			}


			// DB 저장
			string xmlDBH = Util.GetTO_Xml(dtItemH);
			DBMgr.ExecuteNonQuery("SP_CZ_PU_IMPORT_DECLARATIONH_REG", new object[] { xmlDBH });

			string xmlDBL = Util.GetTO_Xml(dtItemL);
			DBMgr.ExecuteNonQuery("SP_CZ_PU_IMPORT_DECLARATIONL_REG", new object[] { xmlDBL });
		}
	}
}


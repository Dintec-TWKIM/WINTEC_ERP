using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;


namespace OutParsing
{
	public class InquiryParser
	{
		string fileName = "";

		string imoNumber;
		string vessel;
		string reference;
		string company;
		string remark;


		DataTable dtItem;

		#region ==================================================================================================== Property

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

		public string Reference
		{
			get
			{
				return reference;
			}
		}

		public string Company
		{
			get
			{
				return company;
			}
		}


		public string Remark
		{
			get
			{
				return remark;
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
		public InquiryParser(string fileName)
		{
			imoNumber = string.Empty;
			vessel = string.Empty;
			reference = string.Empty;
			company = string.Empty;
			remark = string.Empty;
			dtItem = new DataTable();

			// 첨부파일이름 (경로, 확장자 포함)
			this.fileName = fileName;
		}
		#endregion


		public bool Parse(bool isReal)
		{
			string extension = Path.GetExtension(fileName);

			try
			{
				if (extension.ToUpper() == ".PDF")
				{
					string text = PdfReader.ToText(fileName);

					if (text == null)
					{
						return false;
						//break;
					}
					else if (text.Contains("www.hanil-fuji.com") && text.Contains("@hanilss.com"))
					{
						한일후지코리아_pdf p = new 한일후지코리아_pdf(fileName);
						p.Parse();

						vessel = p.Vessel;
						reference = p.Reference.ToUpper();
						dtItem = p.Item;
						imoNumber = p.ImoNumber;
						company = "한일후지";

						return true;
					}
					else if (text.Contains("BOSUNG ENGINEERING CO"))
					{
						보성_pdf p = new 보성_pdf(fileName);
						p.Parse();

						vessel = p.Vessel;
						reference = p.Reference.ToUpper();
						dtItem = p.Item;
						imoNumber = p.ImoNumber;
						company = "보성";

						return true;
					}
					else if (text.Contains("케이프라인 주식회사"))
					{
						케이프_pdf p = new 케이프_pdf(fileName);
						p.Parse();

						vessel = p.Vessel;
						reference = p.Reference.ToUpper();
						dtItem = p.Item;
						imoNumber = p.ImoNumber;
						company = "케이프라인";

						return true;
					}
					else if (text.Contains("Sea-one International"))
					{
						Seaone_pdf p = new Seaone_pdf(fileName);
						p.Parse();

						vessel = p.Vessel;
						reference = p.Reference.ToUpper();
						dtItem = p.Item;
						imoNumber = p.ImoNumber;
						company = "SEA-ONE";

						return true;
					}
					else if (text.Contains("komarine@kmarine.co.kr"))
					{
						코리아마린_pdf p = new 코리아마린_pdf(fileName);
						p.Parse();

						vessel = p.Vessel;
						reference = p.Reference.ToUpper();
						dtItem = p.Item;
						imoNumber = p.ImoNumber;
						company = "코리아";

						return true;
					}

					else if (text.Contains("LDC-KOREA"))
					{
						ldc_pdf p = new ldc_pdf(fileName);
						p.Parse();

						vessel = p.Vessel;
						reference = p.Reference.ToUpper();
						dtItem = p.Item;
						imoNumber = p.ImoNumber;
						company = "LDC";

						return true;
					}
					else if (text.Contains("G&P TECH CO."))
					{
						gnp_pdf p = new gnp_pdf(fileName);
						p.Parse();

						vessel = p.Vessel;
						reference = p.Reference.ToUpper();
						dtItem = p.Item;
						imoNumber = p.ImoNumber;
						company = "G&P";

						return true;
					}
					else if (text.Contains("DH MARINE"))
					{
						DHMARINE_pdf p = new DHMARINE_pdf(fileName);
						p.Parse();

						vessel = p.Vessel;
						reference = p.Reference.ToUpper();
						dtItem = p.Item;
						imoNumber = p.ImoNumber;
						company = "DH";

						return true;
					}
					else if (text.Contains("BK OCEAN"))
					{
						BKOCEAN_pdf p = new BKOCEAN_pdf(fileName);
						p.Parse();

						vessel = p.Vessel;
						reference = p.Reference.ToUpper();
						dtItem = p.Item;
						imoNumber = p.ImoNumber;
						company = "BKOCEAN";

						return true;
					}
					else
					{
						return false;
					}
				}
				else if (extension.ToUpper() == ".XLS" || extension.ToUpper() == ".XLSX")
				{
					DataSet ds = ExcelReader.ToDataSet(fileName);
					DataTable dt = ds.Tables[0];

					// 첫번째 페이지 행, 열 카운트
					int rowCount = ds.Tables[0].Rows.Count;
					int colCount = ds.Tables[0].Columns.Count;

					if (rowCount > 7 && colCount > 3 && ds.Tables[0].Rows[7][2].ToString().StartsWith("㈜제일메카트로닉스") && (ds.Tables[0].Rows[2][3].ToString().StartsWith("DUBHE") || ds.Tables[0].Rows[1][3].ToString().StartsWith("DUBHE")))
					{
						jeil_dintec_excel p = new jeil_dintec_excel(fileName);
						p.Parse();


						vessel = p.Vessel;
						reference = p.Reference.ToUpper();
						dtItem = p.Item;
						imoNumber = p.ImoNumber;
						company = "두베코";

						return true;
					}
					else if (rowCount > 7 && colCount > 3 && ds.Tables[0].Rows[7][2].ToString().StartsWith("㈜제일메카트로닉스"))
					{
						jeil_dintec_excel p = new jeil_dintec_excel(fileName);
						p.Parse();


						vessel = p.Vessel;
						reference = p.Reference.ToUpper();
						dtItem = p.Item;
						imoNumber = p.ImoNumber;
						company = "딘텍";

						return true;
					}
					else
					{
						return false;
					}

				}
				else
				{
					return false;
				}

			}catch (Exception e)
			{
				return false;
			}
		}
	}
}

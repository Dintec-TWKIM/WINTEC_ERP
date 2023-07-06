using Aspose.Email.Outlook;
using Dintec;
using Dintec.Parser;

using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parsing
{
	class mailTechcross
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



		public mailTechcross(string fileName)
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
			Regex regex = new Regex(@".*\)*");

			MapiMessage msg = MapiMessage.FromFile(fileName);

			string mailBodyStr = msg.Body;
			string mailSubStr = msg.Subject.ToLower();


			int idx_s = -1;
			int idx_e = -1;

			if (mailSubStr.Contains("ref"))
			{
				idx_s = mailSubStr.IndexOf("ref");
				reference = mailSubStr.Substring(idx_s, mailSubStr.Length - idx_s).Replace("ref", "").Replace(".", "").Replace("#", "").Replace("(fwd to dintec)", "").Replace(":", "").Trim();
			}
			else
			{
				reference = DateTime.Now.ToString("yy/MM/dd") + " E-MAIL";
				reference = reference.Replace("-", "/");
			}


			string 수정본문 = string.Empty;

			idx_s = mailBodyStr.IndexOf("* Information of the inquired materials :");
			idx_e = mailBodyStr.IndexOf("In order to enhance our level");

			// 본문 편집 후 작업
			if (idx_s != -1 && idx_e != -1)
			{
				수정본문 = mailBodyStr.Substring(idx_s, idx_e - idx_s).Trim();
			}


			string[] 수정본문spl = 수정본문.ToLower().Split(new string[] { "\r\n" }, StringSplitOptions.None);

			if (수정본문spl.Length > 0)
			{
				for (int i = 0; i < 수정본문spl.Length; i++)
				{
					if (수정본문spl[i].ToString().Contains("vessel"))
					{
						vessel = 수정본문spl[i].ToString().Replace("-", "").Replace("vessel", "").Replace(":", "").Trim();

						if (vessel.Contains("/"))
						{
							string[] vesselSpl = vessel.Split('/');

							if (vesselSpl.Length == 2)
							{
								vessel = vesselSpl[1].ToString().Trim();

								if (vessel.Contains("("))
								{
									string[] 호선명만 = vessel.Split('(');

									if (호선명만.Length == 2)
										vessel = 호선명만[0].ToString().Trim();
								}
							}
						}
					}
					else if (수정본문spl[i].ToString().Contains(")") && 수정본문spl[i].ToString().Contains("tc"))
					{
						string 라인수정전 = 수정본문spl[i].ToString().Trim();
						string[] 수정전spl = 라인수정전.Split('/');
						string[] 수정전spl2 = 라인수정전.Split('*');

						if (수정전spl.Length == 2)
						{
							string 전Str = 수정전spl[0].ToString().Trim();
							string 후Str = 수정전spl[1].ToString().Trim();

							if (전Str.Contains("tc"))
							{
								iTemCode = string.Join(",", Regex.Matches(전Str, "tc" + @"0\d{5}").Cast<Match>());
								if(!string.IsNullOrEmpty(iTemCode))
									전Str = 전Str.Replace(iTemCode, "").Trim();
								string[] 전spl = 전Str.Split(')');

								if (전spl.Length == 2)
								{
									iTemDesc = 전spl[1].ToString().Trim();
								}
							}

							if (!string.IsNullOrEmpty(후Str))
							{
								if (후Str.Contains("("))
								{
									string[] 후spl = 후Str.Split('(');

									if(후spl.Length > 1)
									{
										후Str = 후spl[0].ToString().Trim();

										iTemQt = Regex.Replace(후Str, @"\D", "");
										iTemUnit = 후Str.Replace(iTemQt, "").Trim();
									}
								}
								else
								{
									if (후Str.Contains("*"))
									{
										string[] descStr = 후Str.Split('*');

										if(descStr.Length == 2)
										{
											iTemDesc = iTemDesc + " " +
												descStr[0].ToString().Trim();

											iTemQt = Regex.Replace(descStr[1], @"\D", "");
											iTemUnit = descStr[1].Replace(iTemQt, "").Trim();
										}

									}
									else
									{
										iTemQt = Regex.Replace(후Str, @"\D", "");
										iTemUnit = 후Str.Replace(iTemQt, "").Trim();
									}
								}
							}


						}
						else if (수정전spl.Length == 3)
						{
							string 전Str = 수정전spl[0].ToString().Trim();
							string 중Str = 수정전spl[1].ToString().Trim();
							string 후Str = 수정전spl[2].ToString().Trim();

							if (전Str.Contains("tc"))
							{
								iTemCode = string.Join(",", Regex.Matches(전Str, "tc" + @"0\d{5}").Cast<Match>());
								전Str = 전Str.Replace(iTemCode, "").Trim();
								string[] 전spl = 전Str.Split(')');
							}

							iTemDesc = 중Str;

							if (!string.IsNullOrEmpty(후Str))
							{
								if (후Str.Contains("("))
								{
									string[] 후spl = 후Str.Split('(');

									if (후spl.Length > 1)
									{
										후Str = 후spl[0].ToString().Trim();

										iTemQt = Regex.Replace(후Str, @"\D", "");
										iTemUnit = 후Str.Replace(iTemQt, "").Trim();
									}
								}
								else
								{
									iTemQt = Regex.Replace(후Str, @"\D", "");
									iTemUnit = 후Str.Replace(iTemQt, "").Trim();
								}
							}
						}
						else if (수정전spl2.Length == 2)
						{
							string 전Str = 수정전spl2[0].ToString().Trim();
							string 후Str = 수정전spl2[1].ToString().Trim();

							if (전Str.Contains("tc"))
							{
								iTemCode = string.Join(",", Regex.Matches(전Str, "tc" + @"0\d{5}").Cast<Match>());
								if (!string.IsNullOrEmpty(iTemCode))
								{
									전Str = 전Str.Replace(iTemCode, "").Trim();
									string[] 전spl = 전Str.Split(')');

									if (전spl.Length == 2)
									{
										iTemDesc = 전spl[1].ToString().Replace(",", "").Trim();
									}
								}
							}

							if (!string.IsNullOrEmpty(후Str))
							{
								iTemQt = Regex.Replace(후Str, @"\D", "");
								iTemUnit = 후Str.Replace(iTemQt, "").Trim();
							}
						}
						else
						{
							iTemDesc = 라인수정전.Trim();
						}


						//ITEM ADD START
						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
						dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDesc;
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = iTemUniq;
						if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
						dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "MAKER : TECHCROSS";
						dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode.ToUpper().Replace("MANUFACTURER'S PART NO(MF)", "").Replace("BUYER'S PART NO(BP)", "").Replace("SUPPLIER'S PART NO(VP)", "").Replace("IMPA NO.(ZIM)", "").Replace("PART NO", "").Trim();

						iTemNo = string.Empty;
						iTemDesc = string.Empty;
						iTemUnit = string.Empty;
						iTemQt = string.Empty;
						iTemCode = string.Empty;


					}
				}
			}
		}
	}
}

using Aspose.Email.Outlook;
using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class FML
	{
		string vessel;
		string reference;
		string contact;
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

		public string Contact
		{
			get
			{
				return contact;
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


		public FML(string fileName)
		{
			vessel = "";                        // 선명
			reference = "";                     // 문의번호
			contact = "";

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
			string iTemNo = string.Empty;
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDesc = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;


			string nameStr = string.Empty;
			string makerStr = string.Empty;
			string modelStr = string.Empty;
			string serialStr = string.Empty;


			MapiMessage msg = MapiMessage.FromFile(fileName);

			string mailBodyStr = msg.Body;

			// Vessel, Reference
			int idx_lts = mailBodyStr.IndexOf("\r\n\r\nShip\r\n\r\n");
			int idx_lte = mailBodyStr.IndexOf("Booking");

			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
			{
				string vesselRef = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("발주 후", "").Replace(":", "").Trim();

				idx_lte = vesselRef.IndexOf("Department");

				string _vesselRef = vesselRef.Substring(0, idx_lte).Trim();

				string[] vesselSpl = _vesselRef.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

				if (vesselSpl.Length == 4)
				{
					vessel = vesselSpl[1].ToString().Trim();
					reference = vesselSpl[3].ToString().Trim();
				}
			}
			else if (idx_lts.Equals(-1))
			{
				idx_lte = -1;

				idx_lts = mailBodyStr.IndexOf("Requisition Ref.");
				idx_lte = mailBodyStr.IndexOf("Department");

				if (idx_lts != -1 && idx_lte != -1)
					reference = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Department", "").Replace("Requisition Ref.", "").Trim();

				idx_lts = mailBodyStr.IndexOf("Ship");
				idx_lte = mailBodyStr.IndexOf("Requisition Ref.");

				if (idx_lts != -1 && idx_lte != -1)
					vessel = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Department", "").Replace("Ship", "").Trim();
			}

			// Subject
			idx_lts = mailBodyStr.IndexOf("Description");
			idx_lte = mailBodyStr.IndexOf("Spare Type");
			int idx_lte_gs = mailBodyStr.IndexOf("Date of Delivery");

			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
			{
				string subjStr = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Trim();

				string[] subjStrSpl = subjStr.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

				if (subjStrSpl.Length == 8)
				{
					if (subjStrSpl[0].ToString().Contains("Description"))
						nameStr = subjStrSpl[1].ToString().Trim();

					if (subjStrSpl[2].ToString().Contains("Maker"))
						makerStr = subjStrSpl[3].ToString().Trim();

					if (subjStrSpl[4].ToString().Contains("Model"))
						modelStr = subjStrSpl[5].ToString().Trim();

					if (subjStrSpl[6].ToString().Contains("Serial No."))
						serialStr = subjStrSpl[7].ToString().Trim();
				}

				if (!string.IsNullOrEmpty(nameStr))
					iTemSUBJ = nameStr.Trim();

				if (!string.IsNullOrEmpty(makerStr))
					iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

				if (!string.IsNullOrEmpty(modelStr))
					iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MODEL: " + modelStr.Trim();

				if (!string.IsNullOrEmpty(serialStr))
					iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO: " + serialStr.Trim();

				if (string.IsNullOrEmpty(iTemSUBJ))
					iTemSUBJ = subjStr.Trim();

			}
			else if (!idx_lts.Equals(-1) && !idx_lte_gs.Equals(-1))
			{
				string subjStr = mailBodyStr.Substring(idx_lts, idx_lte_gs - idx_lts).Trim();

				string[] subjStrSpl = subjStr.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

				if (subjStrSpl.Length == 2)
				{
					if (subjStrSpl[0].ToString().Equals("Description"))
						nameStr = subjStrSpl[1].ToString().Trim();
				}
				else
				{
					nameStr = subjStrSpl[0].ToString().ToLower().Replace("description", "").Trim();
				}



				if (!string.IsNullOrEmpty(nameStr))
					iTemSUBJ = nameStr.Trim();
			}



			//Item
			idx_lts = mailBodyStr.IndexOf("Unit Price");
			idx_lte = mailBodyStr.IndexOf("Your Ref.");

			if (!idx_lts.Equals(-1) && !idx_lte.Equals(-1))
			{
				if (idx_lts > idx_lte)
				{
					idx_lte = mailBodyStr.IndexOf("Currency");
				}

				string itemStr = mailBodyStr.Substring(idx_lts, idx_lte - idx_lts).Replace("Unit Price", "").Trim();

				itemStr = itemStr.Replace("\t\r\n", "\r\n\r\n");

				string[] descSpl = itemStr.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
				string[] descSpl2 = itemStr.Replace("\t  \t", "\t").Split(new string[] { "\t" }, StringSplitOptions.None);

				int cycleCount = descSpl.Length / 5;
				int resultInt = descSpl.Length % 5;

				// \t 로 구분 : 단위와 갯수가 하나에 들어있음.
				int cycleCount2 = descSpl2.Length / 3;
				int resultInt2 = descSpl2.Length % 3;

				//if (resultInt.Equals(0))
				//{
				//    for (int c = 0; c < cycleCount; c++)
				//    {
				//        iTemNo = descSpl[c * 5].Trim();
				//        iTemDesc = descSpl[(c * 5) + 1].Trim();

				//        string[] qtSpl = descSpl[(c * 5) + 2].Split(' ');

				//        if (qtSpl.Length == 2)
				//        {
				//            iTemQt = qtSpl[0].Trim();
				//            iTemUnit = qtSpl[1].Trim();
				//        }


				//        //ITEM ADD START
				//        dtItem.Rows.Add();
				//        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
				//        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDesc;
				//        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
				//        if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
				//        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
				//        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

				//        iTemNo = string.Empty;
				//        iTemDesc = string.Empty;
				//        iTemUnit = string.Empty;
				//        iTemQt = string.Empty;
				//        iTemCode = string.Empty;
				//    }
				//}
				//else if (resultInt2.Equals(0))
				//{
				for (int c = 0; c < cycleCount2; c++)
				{
					iTemNo = descSpl2[c * 3].Trim();
					iTemDesc = descSpl2[(c * 3) + 1].Trim();

					string[] qtSpl = descSpl2[(c * 3) + 2].Split(' ');

					if (qtSpl.Length == 2)
					{
						iTemQt = qtSpl[0].Trim();
						iTemUnit = qtSpl[1].Trim();
					}


					//ITEM ADD START
					dtItem.Rows.Add();
					dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
					dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDesc;
					dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
					if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
					dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
					dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

					iTemNo = string.Empty;
					iTemDesc = string.Empty;
					iTemUnit = string.Empty;
					iTemQt = string.Empty;
					iTemCode = string.Empty;
				}
				//}
			}
		}
	}
}

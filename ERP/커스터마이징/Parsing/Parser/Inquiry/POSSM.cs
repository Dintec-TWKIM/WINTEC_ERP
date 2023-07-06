using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class POSSM
	{
		string contact;
		string reference;
		string vessel;
		string imoNumber;
		string partner;
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

		#endregion

		#region ==================================================================================================== Constructor

		public POSSM(string fileName)
		{
			contact = "";
			reference = "";
			vessel = "";
			imoNumber = "";
			partner = "";

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
			int itemDescription = -1;
			int itemUnit = -1;
			int itemQt = -1;
			int itemCode = -1;
			int itemRemark = -1;

			string iTemNo = string.Empty;
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;

			string makerString = string.Empty;
			string modelString = string.Empty;
			string subjStr = string.Empty;

			string drwingString = string.Empty;

			// 엑셀 읽기
			DataSet ds = ExcelReader.ToDataSet(fileName);
			DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴

			// ********** 문서 검색 모드

			// 선명
            vessel = dt.Rows[2][8].ToString().Trim() + dt.Rows[2][9].ToString().Trim();

			// 문의번호
			reference = dt.Rows[2][2].ToString().Trim();

			// 매출처 담당자
			partner = dt.Rows[6][8].ToString().Trim() + dt.Rows[6][9].ToString().Trim();

			int idx_e = partner.IndexOf("(");

            if (idx_e != -1)
            {
                partner = partner.Substring(0, idx_e).Trim();
                contact = partner;
            }


			int columnCount = dt.Columns.Count -1;

			//if (string.IsNullOrEmpty(iTemSUBJ))
			//{
			//    for (int k = 1; k <= columnCount; k++)
			//    {
			//        iTemSUBJ = iTemSUBJ.Trim() + dt.Rows[4][k].ToString().Trim(); ;
			//    }
			//}

			// ********** 아이템 추가 모드
			for (int i = 7; i < dt.Rows.Count; i++)
			{
				string firstColString = dt.Rows[i][0].ToString();

				if (firstColString.Equals("No"))
				{
					for (int c = 0; c <= columnCount; c++)
					{
						if (dt.Rows[i][c].ToString() == "Item Name") itemDescription = c;           //품목명
						else if (dt.Rows[i][c].ToString() == "Part No") itemCode = c;               //아이템코드
						else if (dt.Rows[i][c].ToString().Contains("Qty")) itemQt = c;              //수량
						else if (dt.Rows[i][c].ToString().Contains("Unit")) itemUnit = c;           //단위
						else if (dt.Rows[i][c].ToString().Contains("Remark")) itemRemark = c;             //비고 (drwaing no)
					}
				}

			   
				if (firstColString == "EndRow") break;	// 종료
				
				if (GetTo.IsInt(firstColString))
				{
					if (!GetTo.IsInt(dt.Rows[i - 1][0].ToString()) && !string.IsNullOrEmpty(dt.Rows[i-1][0].ToString()))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							subjStr = subjStr + " " + dt.Rows[i - 1][c].ToString();
						}

                        subjStr = subjStr.Replace("   ", "|").Replace("Request for Quotation", "");

						subjStr = subjStr.Replace("||", "|");
						

						string[] subjValue = subjStr.Split('|');

						if (subjValue.Length == 3)
						{
							subjStr = subjValue[0].ToString().Trim();
							makerString = subjValue[1].ToString().Trim();
							modelString = subjValue[2].ToString().Trim();
						}

					}






					if (!string.IsNullOrEmpty(subjStr) && !subjStr.Equals("NONE") && !subjStr.Equals("none"))
						iTemSUBJ = subjStr;

					if (!string.IsNullOrEmpty(makerString) && !makerString.Equals("NONE") && !makerString.Equals("none"))
						iTemSUBJ = iTemSUBJ + Environment.NewLine +  "MAKER: " + makerString;

					if (!string.IsNullOrEmpty(modelString) && !modelString.Equals("NONE") && !makerString.Equals("none"))
						iTemSUBJ = iTemSUBJ + Environment.NewLine + "MODEL: " + modelString;

					// iTemSUBJ가 처음에 "," 로 시작하면 제거
					if (iTemSUBJ.Trim().StartsWith(","))
						iTemSUBJ = iTemSUBJ.Substring(1, iTemSUBJ.Length - 1).Trim();

					iTemSUBJ = iTemSUBJ.Replace("ⓣ", "").Replace("Ⓣ", "").Trim();
						


					if(!itemCode.Equals(-1))
						iTemCode = dt.Rows[i][itemCode].ToString().Replace("ⓣ", "").Replace("Ⓣ", "");
					if(!itemDescription.Equals(-1))
						iTemDESC = dt.Rows[i][itemDescription].ToString().Replace("ⓣ", "").Replace("Ⓣ", "");
					if(!itemQt.Equals(-1))
						iTemQt = dt.Rows[i][itemQt].ToString();
					if(!itemUnit.Equals(-1))
						iTemUnit = dt.Rows[i][itemUnit].ToString();

					if (!itemRemark.Equals(-1))
						drwingString = dt.Rows[i][itemRemark].ToString().Trim();

                    if (!string.IsNullOrEmpty(drwingString))
                        iTemDESC = iTemDESC.Trim() + Environment.NewLine + drwingString.Trim();


					dtItem.Rows.Add();
					dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = dt.Rows[i][0];
					if(!string.IsNullOrEmpty(iTemSUBJ))
						dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
					dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
					dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
					dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
					if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;

					iTemCode = string.Empty;
					iTemDESC = string.Empty;
					iTemQt = string.Empty;
					iTemUnit = string.Empty;

					subjStr = string.Empty;
					makerString = string.Empty;
					modelString = string.Empty;
				}
			}
		}

		#endregion
	}
}

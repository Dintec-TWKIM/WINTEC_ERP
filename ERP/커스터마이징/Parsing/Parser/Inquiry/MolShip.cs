using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class MolShip
    {
        string contact;
		string reference;
		string vessel;
		string imoNumber;
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

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

        public MolShip(string fileName)
		{
			contact = "";
			reference = "";
			vessel = "";
			imoNumber = "";

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
            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

			// 엑셀 읽기
			DataSet ds = ExcelReader.ToDataSet(fileName);
			DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴

			// ********** 문서 검색 모드
			// 선명
			string[] text = dt.Rows[6][6].ToString().Split(' ');

			if (text.Length >= 2)
				vessel = text[0] + " " + text[1];
			else
				vessel = string.Join(" ", text);

			// 문의번호
			reference = dt.Rows[8][6].ToString().Trim();

			// ********** 아이템 추가 모드
			string subject = "";

			for (int i = 18; i < dt.Rows.Count; i++)
			{
				string firstColString = dt.Rows[i][0].ToString();

                if (firstColString == "EndRow") break;	// 종료

                subject = dt.Rows[14][2].ToString();

				if (GetTo.IsInt(firstColString))
				{
                    iTemNo = dt.Rows[i][0].ToString().Trim();
                    iTemDESC = dt.Rows[i][4].ToString().Trim();
                    iTemCode = dt.Rows[i][7].ToString().Replace("Plate", "").Replace(",", "").Replace(" Item No. ", "-").Replace("Item No.","-").Trim();
                    iTemQt = dt.Rows[i][14].ToString().Trim();
                    iTemUnit = dt.Rows[i][15].ToString().Trim();

                    // 수량이 0일때는 제외
                    if (!dt.Rows[i][8].ToString().Equals("0"))
                    {
                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + subject;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
                    }
				}
			}
		}

		#endregion
    }
}

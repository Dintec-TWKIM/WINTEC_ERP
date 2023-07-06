using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
    class GreatEstern
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

        public GreatEstern(string fileName)
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


        public void Parse()
        {
            string iTemNo = string.Empty;
            string iTemSUBJ = string.Empty;
            string iTemCode = string.Empty;
            string iTemDESC = string.Empty;
            string iTemUnit = string.Empty;
            string iTemQt = string.Empty;

            string descStr = string.Empty;
            string dwgStr = string.Empty;
            string descSerial = string.Empty;
            string serialStr = string.Empty;
            string makerStr = string.Empty;
            string typeStr = string.Empty;
            string subjStr = string.Empty;

            int _itemRefernce = 0;

            int _itemDesc = 0;
            int _itemQt = 0;
            int _itemUnit = 0;
            int _itemRemark = 0;

            bool itemStart = false;

            int idx_s = -1;
            int idx_e = -1;



            

            // 엑셀 읽기
            DataSet ds = ExcelReader.ToDataSet(fileName);
            DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴


            // ********** 아이템 추가 모드

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string firstColString = dt.Rows[i][1].ToString();
                string secondColString = dt.Rows[i][2].ToString();

                if (firstColString.ToUpper().Equals("ADDITIONAL CHARGES")) break;
                if (secondColString.ToUpper().Equals("ADDITIONAL CHARGES")) break;

                if (secondColString.Equals("To"))
                {
                    for (int c = 3; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Equals("Vessel"))
                        {
                            vessel = dt.Rows[i][c + 2].ToString().Trim();
                            _itemRefernce = c + 2;
                        }
                    }

                    reference = dt.Rows[i + 3][_itemRefernce].ToString().Trim();


                    string[] _vessel = vessel.Split('-');
                    vessel = _vessel[0].ToString().Trim();

                    idx_s = vessel.IndexOf("(");

                    if (idx_s != -1)
                        vessel = vessel.Substring(0, idx_s).Trim();
                }
                //else if (secondColString.Contains("Type"))
                //{
                //    typeStr = dt.Rows[i][3].ToString().Trim();
                //}
                else if (secondColString.Contains("Maker"))
                {
                    makerStr = dt.Rows[i][3].ToString().Replace("Sub-Equipment -  Main Equipment -", "").Trim();
                    makerStr = makerStr.Replace("Sub-Equipment -", "").Replace("Main Equipment -", "").Trim();
                }
                else if (secondColString.Contains("Serial"))
                {
                    serialStr = dt.Rows[i][3].ToString().Trim();
                }
                //else if (secondColString.Contains("Purchase"))
                //{
                //    subjStr = dt.Rows[i][3].ToString().Trim();
                //}


                if (firstColString.Equals("Line No."))
                {
                    itemStart = true;

                    for (int c = 2; c < dt.Columns.Count; c++)
                    {
                        if (dt.Rows[i][c].ToString().Equals("Equipment Description")) _itemDesc = c;
                        else if (dt.Rows[i][c].ToString().Equals("Quantity")) _itemQt = c;
                        else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
                        else if (dt.Rows[i][c].ToString().Equals("Remarks")) _itemRemark = c;
                    }
                }

                if (itemStart)
                {
                    if (GetTo.IsInt(firstColString) || GetTo.IsInt(secondColString))
                    {
                        descStr = dt.Rows[i][_itemDesc].ToString().Trim();
                        iTemQt = dt.Rows[i][_itemQt].ToString().Trim();
                        iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

                        //string[] _descValue = iTemDESC.Split(':');
                        
                        //descStr = iTemDESC.Replace("Part Description :", "").Trim();


                        // ITEM DESC
                        idx_s = descStr.IndexOf("Part Description :", 0);
                        idx_e = descStr.IndexOf("Maker :", 0);
                        if(!idx_s.Equals(-1) && !idx_e.Equals(-1))
                        {
                            iTemDESC = descStr.Substring(idx_s, idx_e - idx_s).Replace("Part Description","").Replace(":","").Trim();
                        }


                        // ITEM CODE
                        idx_s = descStr.IndexOf("Maker Ref.", 0);
                        idx_e = descStr.IndexOf("Component Name", 0);
                        if (!idx_s.Equals(-1) && !idx_e.Equals(-1))
                        {
                            iTemCode = descStr.Substring(idx_s, idx_e - idx_s).Replace("Maker Ref.", "").Replace(":", "").Trim();
                        }

                        // ITEM SUBJ
                        idx_s = descStr.IndexOf("Component Name", 0);
                        //idx_e = descStr.IndexOf("Component Name", 0);
                        if (!idx_s.Equals(-1) && !idx_e.Equals(-1))
                        {
                            subjStr = descStr.Substring(idx_s, descStr.Length - idx_s).Trim();

                            if (subjStr.EndsWith("Drawing Plate No.:"))
                                subjStr = subjStr.Replace("Drawing Plate No.:", "").Trim();
                        }




                        if (!string.IsNullOrEmpty(subjStr))
                            iTemSUBJ = subjStr.Trim();
                        //if (!string.IsNullOrEmpty(typeStr))
                        //    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr;
                        if (!string.IsNullOrEmpty(makerStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr;
                        if (!string.IsNullOrEmpty(serialStr))
                            iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialStr;


                        dtItem.Rows.Add();
                        dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
                        if(!string.IsNullOrEmpty(iTemSUBJ))
                            dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
                        dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
                        dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
                        if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
                        dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);

                        iTemDESC = string.Empty;
                        iTemCode = string.Empty;
                        iTemQt = string.Empty;
                        iTemUnit = string.Empty;
                        iTemSUBJ = string.Empty;

                        descStr = string.Empty;
                        dwgStr = string.Empty;
                    }
                }
            }
        }

    }
}

using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	public class YangMing
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

		public YangMing(string fileName)
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
			string makerStr = string.Empty;
			string typeStr = string.Empty;
			string serialStr = string.Empty;
			
			string itemSUBJ = string.Empty;
			string itemCODE = string.Empty;
			string itemDESC = string.Empty;
			string itemUNIT = string.Empty;
			string itemQT = string.Empty;
			string itemRmk = string.Empty;

			string drwNoStr = string.Empty;
			string positionStr = string.Empty;
			string partNoStr = string.Empty;

			int _itemDesc = -1;
			int _itemUnit = -1;
			int _itemQt = -1;
			int _itemCode = -1;
			int _itemDrw = -1;
			int _itemRmk = -1;
			



			// 엑셀 읽기
			DataSet ds = ExcelReader.ToDataSet(fileName);
			DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴

			// ********** 문서 검색 모드
			// 선명
			string[] text = dt.Rows[7][3].ToString().Split(' ');

			if (text.Length > 2)
				vessel = text[0] + " " + text[1];
			else
				vessel = string.Join("", text);


            // 담당자
            string[] text2 = dt.Rows[5][3].ToString().Split(' ');

            contact = string.Empty;

            if (text2.Length > 2)
            {
                for (int c = 1; c < text2.Length; c++)
                {
                    contact = contact + " " + text2[c].Trim();
                }

                contact = contact.Trim();
            }

			// 문의번호
			reference = dt.Rows[7][4].ToString().Replace("Query No.:", "").Trim();

			// ********** 아이템 추가 모드
			string subject = "";

			for (int i = 7; i < dt.Rows.Count; i++)
			{
				string firstColString = dt.Rows[i][1].ToString();
				string secondColString = dt.Rows[i][2].ToString();

				//if (firstColString == "" && secondColString == "") break;	// 종료
				if (firstColString == "" && secondColString != "")
				{
					subject = dt.Rows[i][3].ToString();// +" " + dt.Rows[i][4];	// 서브젝트행
					serialStr = dt.Rows[i][12].ToString();


					string[] subjValueSpl = dt.Rows[i][4].ToString().Split('/');

					if (subjValueSpl.Length == 2)
					{
						makerStr = subjValueSpl[0].ToString().Trim();
						typeStr = subjValueSpl[1].ToString().Trim();
					}else if(subjValueSpl.Length == 3)
					{
						makerStr = subjValueSpl[0].ToString().Trim();
						typeStr = subjValueSpl[1].ToString().Trim() + "/" + subjValueSpl[2].ToString().Trim();
					}
				}
                

				itemSUBJ = subject.Trim();

				if (!string.IsNullOrEmpty(makerStr))
					itemSUBJ = itemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr.Trim();

				if (!string.IsNullOrEmpty(typeStr))
					itemSUBJ = itemSUBJ.Trim() + Environment.NewLine + "TYPE: " + typeStr.Trim();


				if (!string.IsNullOrEmpty(serialStr) && serialStr.ToUpper().Contains("S/N"))
					itemSUBJ = itemSUBJ.Trim() + Environment.NewLine + serialStr.Trim();




				if (firstColString.StartsWith("No."))
				{
					for (int c = 1; c < dt.Columns.Count; c++)
					{
						if (dt.Rows[i][c].ToString().StartsWith("Equipment Code")) _itemCode = c;
						else if (dt.Rows[i][c].ToString().StartsWith("Description")) _itemDesc = c;
						else if (dt.Rows[i][c].ToString().StartsWith("Drawing / Position")) _itemDrw = c;
						else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
						else if (dt.Rows[i][c].ToString().StartsWith("Quantity Inquity")) _itemQt = c;
						else if (dt.Rows[i][c].ToString().StartsWith("Reqn Remark")) _itemRmk = c;
					}
				}
                else if (firstColString.StartsWith("Charge Man"))
                {
                    contact = dt.Rows[i][2].ToString().Trim();
                }
                else if (secondColString.StartsWith("Charge Man"))
                {
                    contact = dt.Rows[i][3].ToString().Trim();
                }
                else if (GetTo.IsInt(firstColString))
                {

                    if (!_itemDesc.Equals(-1))
                        itemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

                    if (!_itemUnit.Equals(-1))
                        itemUNIT = dt.Rows[i][_itemUnit].ToString().Trim();

                    if (!_itemQt.Equals(-1))
                        itemQT = dt.Rows[i][_itemQt].ToString().Trim();

					if (_itemRmk != -1)
						itemRmk = dt.Rows[i][_itemRmk].ToString().Trim();


                    if (!_itemDrw.Equals(-1))
                    {
						partNoStr = string.Empty;
                        string[] noValueSpl = dt.Rows[i][4].ToString().Split('/');

                        if (noValueSpl.Length >= 3)
                        {
                            drwNoStr = noValueSpl[0].ToString().Trim();
                            positionStr = noValueSpl[1].ToString().Trim();
							
							for (int c = 2; c < noValueSpl.Length; c++)
							{
								partNoStr = partNoStr + "/" + noValueSpl[c].ToString().Trim();
							}

							if (partNoStr.StartsWith("/"))
								partNoStr = partNoStr.Substring(1, partNoStr.Length - 1).Trim();

                            if (!drwNoStr.Equals("*") && !string.IsNullOrEmpty(drwNoStr))
                                itemDESC = itemDESC.Trim() + Environment.NewLine + "DWG.: " + drwNoStr.Trim();

                            if (!partNoStr.Equals("*") && !string.IsNullOrEmpty(partNoStr))
                            {
                                itemCODE = partNoStr.Trim();

                                if (!positionStr.Equals("*") && !string.IsNullOrEmpty(positionStr))
                                    itemDESC = itemDESC.Trim() + Environment.NewLine + "POS. " + positionStr;
                            }
                            else if (!positionStr.Equals("*") && !string.IsNullOrEmpty(positionStr))
                                itemCODE = "POS. " + positionStr.Trim();
                        }
                    }

                    if (itemUNIT.Equals("PC"))
                        itemUNIT = "PCS";

                    itemCODE = itemCODE.Replace(" ", "").Trim();

					if(!string.IsNullOrEmpty(itemRmk))
					{
						itemDESC = itemDESC.Trim() + Environment.NewLine + itemRmk;
					}

                    dtItem.Rows.Add();
                    dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = dt.Rows[i][1];
                    if (!string.IsNullOrEmpty(itemSUBJ))
                        dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + itemSUBJ;
                    dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = itemCODE;
                    dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = itemDESC;
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(itemUNIT);
                    dtItem.Rows[dtItem.Rows.Count - 1]["UNIQ"] = dt.Rows[i][2];
                    dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = itemQT;

                    itemDESC = string.Empty;
                    itemCODE = string.Empty;
                }
			}
		}

		#endregion
	}
}

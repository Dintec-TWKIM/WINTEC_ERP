using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class Euronav
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

		public Euronav(string fileName)
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
			int _itemDesc = 0;
			int _itemUnit = 0;
			int _itemQt = 0;
			int _itemCode = 0;
			int _itemSubj = 0;
			int _itemDraw = 0;
			int _itemPosition = 0;
			int _itemModel = 0;
			int _itemNote = 0;

			string compnentStr = string.Empty;

			string drawDesc = string.Empty;
			string positionDesc = string.Empty;
			string modelDesc = string.Empty;
			string noteDesc = string.Empty;

			string iTemNo = string.Empty;
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;

			int _vesselInt = 0;

			bool itemStart = false;

			// 엑셀 읽기
			DataSet ds = ExcelReader.ToDataSet(fileName);
			DataTable dt = ds.Tables[0];	// 엑셀에서 1번 시트만 가져옴

			for (int i = 3; i < dt.Columns.Count; i++)
			{
				if (dt.Rows[2][i].ToString().Equals("Vendor's Details"))
					_vesselInt = i;
			}

			// #################### 선명, 문의번호 ####################
			for (int i = 3; i < dt.Rows.Count; i++)
			{
				if (string.IsNullOrEmpty(vessel) || string.IsNullOrEmpty(reference))
				{
					int c = i + 1;
					while (!string.IsNullOrEmpty(dt.Rows[c][_vesselInt].ToString()))
					{
						if (dt.Rows[c][_vesselInt].ToString().Equals("Vessel:"))
						{
							vessel = dt.Rows[c][_vesselInt + 1].ToString().Trim();

							if (string.IsNullOrEmpty(vessel))
								vessel = dt.Rows[c][_vesselInt + 2].ToString().Trim();
						}
						else if (dt.Rows[c][_vesselInt].ToString().Equals("RFQ:"))
						{
							reference = dt.Rows[c][_vesselInt + 1].ToString().Trim();

							if (string.IsNullOrEmpty(reference))
								reference = dt.Rows[c][_vesselInt + 2].ToString().Trim();
						}

						c += 1;
					}
				}

				

				string firstColString = dt.Rows[i][0].ToString();

				if (firstColString.Equals("* All changeable fields are in grey.")) break;	// 종료
				else if (firstColString.Contains("Component Details"))
				{
					compnentStr = firstColString.Replace("Component Details:", "").Trim();

					int _i = i + 1;

					while (!dt.Rows[_i][0].ToString().Contains("Requisition Number"))
					{
						compnentStr = compnentStr.Trim() + " " + dt.Rows[_i][0].ToString().Trim();

						_i += 1;

						if (_i >= dt.Rows.Count)
							break;
					}

				}
				else if (firstColString.Equals("Requisition No") || firstColString.Equals("Requisition Number"))
				{
					for (int c = 0; c < dt.Columns.Count; c++)
					{
						if (dt.Rows[i][c].ToString().Equals("Stores Description") || dt.Rows[i][c].ToString().Equals("Part Description")) _itemDesc = c;           //품목명
						else if (dt.Rows[i][c].ToString().Equals("IMPA Code") || dt.Rows[i][c].ToString().Equals("Part Number")) _itemCode = c;               //아이템코드
						else if (dt.Rows[i][c].ToString().Contains("Quantity Required")) _itemQt = c;              //수량
						else if (dt.Rows[i][c].ToString().Contains("Measurement Unit")) _itemUnit = c;           //단위
						else if (dt.Rows[i][c].ToString().Contains("Category")) _itemSubj = c;
						else if (dt.Rows[i][c].ToString().Contains("Drawing No")) _itemDraw = c;
						else if (dt.Rows[i][c].ToString().Contains("Position No")) _itemPosition = c;
						else if (dt.Rows[i][c].ToString().Contains("Model No")) _itemModel = c;
						else if (dt.Rows[i][c].ToString().Contains("Note")) _itemNote = c;
					}

					itemStart = true;
				}
				else if (itemStart && !string.IsNullOrEmpty(dt.Rows[i][0].ToString()) && !dt.Rows[i][0].ToString().Contains("Component Details"))
				{
					// 항목별 대입
					if (!_itemCode.Equals(0))
						iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

					if (!_itemDesc.Equals(0))
						iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

					if (!_itemQt.Equals(0))
						iTemQt = dt.Rows[i][_itemQt].ToString().Trim();

					if (!_itemUnit.Equals(0))
						iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();
					else
						iTemUnit = "PCS";

					if (!_itemSubj.Equals(0))
						iTemSUBJ = dt.Rows[i][_itemSubj].ToString().Trim();
					if (!string.IsNullOrEmpty(compnentStr))
						iTemSUBJ = iTemSUBJ + compnentStr;

					if (!_itemDraw.Equals(0))
						drawDesc = dt.Rows[i][_itemDraw].ToString().Trim();

					if (!_itemPosition.Equals(0))
						positionDesc = dt.Rows[i][_itemPosition].ToString().Trim();

					if (!_itemModel.Equals(0))
						modelDesc = dt.Rows[i][_itemModel].ToString().Trim();

					if (!_itemNote.Equals(0))
						noteDesc = dt.Rows[i][_itemNote].ToString().Trim();


					if (!string.IsNullOrEmpty(modelDesc))
						iTemDESC = iTemDESC.Trim() + Environment.NewLine + "MODEL: " + modelDesc;

					if (!string.IsNullOrEmpty(drawDesc))
						iTemDESC = iTemDESC.Trim() + Environment.NewLine + "DWG NO.: " + drawDesc;

					if (!string.IsNullOrEmpty(positionDesc))
						iTemDESC = iTemDESC.Trim() + Environment.NewLine + "POS.: " + positionDesc;

					if (!string.IsNullOrEmpty(noteDesc))
						iTemDESC = iTemDESC.Trim() + Environment.NewLine + noteDesc;


                    if (iTemCode.Equals("??") || iTemCode.Equals("-"))
                    {
                        iTemCode = string.Empty;

                        iTemCode = positionDesc.Trim();
                    }
                    

                    




					dtItem.Rows.Add();
					//dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = ;
					if (!string.IsNullOrEmpty(iTemSUBJ))
						dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
					dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;
					dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
					dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
					if(GetTo.IsInt(iTemQt.Replace(".",""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;

					iTemCode = string.Empty;
					iTemDESC = string.Empty;
					iTemQt = string.Empty;
					iTemUnit = string.Empty;
					iTemSUBJ = string.Empty;

					drawDesc = string.Empty;
					positionDesc = string.Empty;
					modelDesc = string.Empty;
					noteDesc = string.Empty;
				}
			}
		}

		#endregion
	}
}

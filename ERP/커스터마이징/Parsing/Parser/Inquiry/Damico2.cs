using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dintec;
using System.Data;
using Dintec.Parser;

namespace Parsing
{
	class Damico2
	{
		string vessel;
		string reference;
		string contact;
		DataTable dtItem;
		string imoNumber;

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

		public string Imonumber
		{
			get
			{
				return imoNumber;
			}
		}

		#endregion ==================================================================================================== Constructor



		public Damico2(string fileName)
		{
			vessel = "";                        // 선명
			reference = "";                     // 문의번호
			contact = string.Empty;
			imoNumber = string.Empty;

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
			string iTemNo = string.Empty;
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;

			string subjNameStr = string.Empty;
			string subjMakerStr = string.Empty;
			string subjTypeStr = string.Empty;
			string subjSerialStr = string.Empty;
			string subjDwgStr = string.Empty;

			int _subjNameInt = -1;
			int _subjMakerInt = -1;
			int _subjTypeInt = -1;
			int _subjSerialInt = -1;
			int _subjDrawingInt = -1;

			int _itemDescInt = -1;
			int _itemCodeInt = -1;
			int _itemQtInt = -1;
			int _itemUnitInt = -1;

			int _beforeLine = -1;
			int _beforeNum = -1;
			




			#region ########### READ ###########

			// 엑셀 읽기
			DataSet ds = ExcelReader.ToDataSet(PdfReader.GetExcel(fileName));
			//DataTable dt = ds.Tables[0];    // 엑셀에서 1번 시트만 가져옴


			//// Pdf를 Xml로 변환해서 분석 (1000$ 짜리로 해야함, 500$ 짜리로 하면 Description 부분에 CRLF가 안됨)
			//// 1. 우선 500$ 짜리로 Xml 변환함 (1000$ 짜리의 경우 도면이 붙어 있으면 시간이 엄청 오래 걸림)
			//string xmlTemp = PdfReader.ToXml(fileName);

			//// 2. 도면을 제외한 Page 카운트 가져오기
			//int pageCount = xmlTemp.Count("<page>");

			//// 3. 앞서 나온 Page를 근거로 파싱 시작			
			//string xml = string.Empty;//PdfReader.GetXml(fileName, 1, pageCount);
			//xml = PdfReader.GetXml(fileName);
			//DataSet ds = PdfReader.ToDataSet(xml);


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

			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string firstColStr = dt.Rows[i][0].ToString().ToLower();


					if(string.IsNullOrEmpty(imoNumber))
					{
						for(int c=0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("IMO"))
								imoNumber = dt.Rows[i][c].ToString().Replace("IMO #:", "").Trim();
						}
					}

					if(string.IsNullOrEmpty(vessel))
					{
						for(int c=0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("Vessels:"))
								vessel = dt.Rows[i][c].ToString().Replace("Vessels:", "").Trim();
						}
					}


					if(string.IsNullOrEmpty(reference))
					{
						for(int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("Quotation #:"))
								reference = dt.Rows[i][c].ToString().Replace("Quotation #:", "").Trim();
						}
					}

					
					if(string.IsNullOrEmpty(contact))
					{
						for(int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("Contact Info:"))
								contact = dt.Rows[i][c].ToString() + " " + dt.Rows[i][c + 1].ToString() + " " + dt.Rows[i][c + 2].ToString();
						}

						if (!string.IsNullOrEmpty(contact))
							contact = contact.Replace("Contact Info:", "").Trim();
					}


					//// CONTACT
					//else if (firstColStr.StartsWith("contact person")) 
					//{
					//	contact = dt.Rows[i][1].ToString();

					//	if (string.IsNullOrEmpty(contact))
					//		contact = dt.Rows[i][2].ToString();
					//}

					// GET SUBJECT COLUMN
					if (firstColStr.Equals("no"))
					{
						string forStr = string.Empty;
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							forStr = dt.Rows[i][c].ToString().ToUpper().Trim();

							if (forStr.StartsWith("PART")) _itemCodeInt = c;
							else if (forStr.Equals("DESCRIPTION")) _itemDescInt= c;
							else if (forStr.Equals("QTY")) _itemQtInt = c;
							else if (forStr.Equals("UNIT") && !dt.Rows[i+1][c].ToString().Contains("Price")) _itemUnitInt = c;
						}
					}

					//// SUBJECT
					//else if (firstColStr.Contains("line") && firstColStr.Contains("#"))
					//{

					//	// GET ITEM COLUMN 
					//	for (int c = 1; c < dt.Columns.Count; c++)
					//	{
					//		if (dt.Rows[i][c].ToString().Contains("Full Description")) _itemDescInt = c;
					//		else if (dt.Rows[i][c].ToString().Contains("Quantity") || dt.Rows[i][c].ToString().Equals("Qty")) _itemQtInt = c;
					//		else if (dt.Rows[i][c].ToString().Equals("Unit Type") || dt.Rows[i][c].ToString().Equals("Unit")) _itemUnitInt = c;
					//		else if (dt.Rows[i][c].ToString().Contains("Part")) _itemCodeInt = c;
					//	}
					//	if (_subjNameInt != -1)
					//	{
					//		subjNameStr = dt.Rows[i - 1][_subjNameInt].ToString().Trim();

					//		if (!_subjMakerInt.Equals(-1)) subjMakerStr = dt.Rows[i-1][_subjMakerInt].ToString().Trim();

					//		if (!_subjTypeInt.Equals(-1)) subjTypeStr = dt.Rows[i-1][_subjTypeInt].ToString().Trim();

					//		if (!_subjSerialInt.Equals(-1)) subjSerialStr = dt.Rows[i-1][_subjSerialInt].ToString().Trim();

					//		if (!_subjDrawingInt.Equals(-1)) subjDwgStr = dt.Rows[i-1][_subjDrawingInt].ToString().Trim();
													   						
					//	}
					//}

					// ITEM
					else if (GetTo.IsInt(firstColStr) && firstColStr.Length < 3)
					{
						iTemNo = firstColStr;

						if (_itemDescInt != -1)
						{
							// 원래 자리 있을 경우!
							iTemDESC = dt.Rows[i][_itemDescInt].ToString().Trim();

							int _i = i+1;

							while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
							{
								iTemDESC = iTemDESC + Environment.NewLine + dt.Rows[_i][_itemDescInt].ToString().Trim();

								_i += 1;

								if (dt.Rows.Count < _i)
									break;
							}
						}

						if (_itemCodeInt != -1)
						{
							iTemCode = dt.Rows[i][_itemCodeInt].ToString().Trim();

							//int _i = i + 1;

							//while(string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
							//{
							//	iTemCode = iTemCode + Environment.NewLine + dt.Rows[_i][_itemCodeInt].ToString().Trim();

							//	_i += 1;

							//	if (dt.Rows.Count < _i)
							//		break;
							//}
						}

						if (_itemQtInt != -1)
							iTemQt = dt.Rows[i][_itemQtInt].ToString().Trim();

						if (_itemUnitInt != -1)
							iTemUnit = dt.Rows[i][_itemUnitInt].ToString().Trim();


						


						//// 이거 수정해서 줄일 수 있음.
						//if (!string.IsNullOrEmpty(subjNameStr.Trim()))
						//	iTemSUBJ = subjNameStr;

						//if (!string.IsNullOrEmpty(subjMakerStr.Trim()))
						//	iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + subjMakerStr.Trim();

						//if (!string.IsNullOrEmpty(subjTypeStr.Trim()))
						//	iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "TYPE: " + subjTypeStr.Trim();

						//if (!string.IsNullOrEmpty(subjSerialStr.Trim()))
						//	iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + subjSerialStr.Trim();

						//if (!string.IsNullOrEmpty(subjDwgStr.Trim()))
						//	iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG NO: " + subjDwgStr.Trim();


						//ITEM ADD START
						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
						dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC.Trim();
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
						if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
						if (!string.IsNullOrEmpty(iTemSUBJ))
							dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
						dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

						iTemSUBJ = string.Empty;
						iTemDESC = string.Empty;
						iTemQt = string.Empty;
						iTemUnit = string.Empty;
					}
				}
			}
		}
	}
}

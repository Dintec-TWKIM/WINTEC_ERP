using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class Maran
	{
		string vessel;
		string reference;
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

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		#endregion ==================================================================================================== Constructor



		public Maran(string fileName)
		{
			vessel = "";                        // 선명
			reference = "";                     // 문의번호

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

			string systemStr = string.Empty;
			string makerStr = string.Empty;
			string partStr = string.Empty;
			string serialStr = string.Empty;
			string subsystemStr = string.Empty;
			string makerStr2 = string.Empty;


			string subjStr = string.Empty;

			string descStr = string.Empty;
			string drwStr = string.Empty;

			int _itemDesc = -1;
			int _itemCode = -1;
			int _itemDwg = -1;
			int _itemMaker = -1;
			int _itemQt = -1;
			int _itemUnit = -1;


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

			int _vesselInt = -1;
			bool subCheck = false;



			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count-1; i++)
				{
					string dataFirstColValue = dt.Rows[i][0].ToString().ToLower();
					//string dataSecondColValue = dt.Rows[i][1].ToString();


					//if (string.IsNullOrEmpty(vessel))
					//{
					//	for (int c = 0; c < dt.Columns.Count-1; c++)
					//	{
					//		if (dt.Rows[i][c].ToString().Contains("Vessel"))
					//		{
					//			vessel = dt.Rows[i][c + 1].ToString().Replace("MT", "").Trim();
					//			//_vesselInt = c;
					//			break;
					//		}
					//	}
					//}

					//if (string.IsNullOrEmpty(reference))
					//{
					//	for (int c = 0; c < dt.Rows.Count-1; c++)
					//	{
					//		if (dt.Rows[i][c].ToString().Contains("Inquiry No"))
					//		{
					//			reference = dt.Rows[i][c + 1].ToString().Trim();
					//		}
					//	}
					//}


					//if (!_vesselInt.Equals(-1))
					//{
					//    for (int c = i; c < 7; c++)
					//    {
					//        if (dt.Rows[c][_vesselInt].ToString().Contains("Inquiry No"))
					//        {
					//            reference = dt.Rows[c][_vesselInt + 1].ToString().Trim();
					//        }
					//    }
					//}


					if (dataFirstColValue.StartsWith("enquiry no"))
					{
						reference = dt.Rows[i][1].ToString().Trim();
					}
					else if (dataFirstColValue.StartsWith("vessel"))
					{
						vessel = dt.Rows[i][1].ToString().Trim();
					}
					else if (dataFirstColValue.StartsWith("component"))
					{
						for(int c =0; c < dt.Columns.Count; c++)
						{
							subjStr = subjStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
						}

						int _i = i + 1;

						while(!GetTo.IsInt(dt.Rows[_i][0].ToString()))
						{
							if (dt.Rows[_i][0].ToString().ToLower().StartsWith("no")) break;

							subjStr = subjStr.Trim() + Environment.NewLine;

							for(int c = 0; c < dt.Columns.Count; c++)
							{
								if(!subjStr.EndsWith(Environment.NewLine))
									subjStr = subjStr + " " + dt.Rows[_i][c].ToString().Trim();
								else
									subjStr = subjStr + dt.Rows[_i][c].ToString().Trim();
							}
							
							_i += 1;

							if (_i >= dt.Rows.Count)
								break;
						}
					}
					//else if (dataFirstColValue.Contains("System"))
					//{
					//	for (int c = 1; c < dt.Columns.Count; c++)
					//	{
					//		systemStr = systemStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
					//	}

					//	subsystemStr = string.Empty;
					//	makerStr = string.Empty;
					//	partStr = string.Empty;
					//	serialStr = string.Empty;
					//	drwStr = string.Empty;


					//	int _i = i + 1;
					//	while (!GetTo.IsInt(dt.Rows[_i][0].ToString().Replace(".", "")))
					//	{
					//		if (dt.Rows[_i][0].ToString().Contains("Maker "))
					//		{
					//			for (int c = 0; c < dt.Columns.Count; c++)
					//			{
					//				makerStr = makerStr.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Replace("Maker", "").Replace(":", "").Trim();
					//			}
					//		}
					//		else if (dt.Rows[_i][0].ToString().Contains("Particulars"))
					//		{
					//			for (int c = 0; c < dt.Columns.Count; c++)
					//			{
					//				partStr = partStr + dt.Rows[_i][c].ToString().Replace("Particulars", "").Replace(":", "").Trim();
					//			}
					//		}
					//		else if (dt.Rows[_i][0].ToString().Contains("Serial"))
					//		{
					//			for (int c = 0; c < dt.Columns.Count; c++)
					//			{
					//				serialStr = serialStr + dt.Rows[_i][c].ToString().Replace("Serial No", "").Replace(":", "").Trim();
					//			}
					//		}
					//		else if (dt.Rows[_i][0].ToString().Contains("Subsystem"))
					//		{
					//			for (int c = 0; c < dt.Columns.Count; c++)
					//			{
					//				subsystemStr = subsystemStr + dt.Rows[_i][c].ToString().Replace("Subsystem", "").Replace(":", "").Trim();
					//			}

					//			subCheck = true;
					//		}
					//		else if (dt.Rows[_i][0].ToString().Contains("Maker : :"))
					//		{
					//			makerStr2 = string.Empty;
					//			for (int c = 0; c < dt.Columns.Count; c++)
					//			{
					//				makerStr2 = makerStr2.Trim() + Environment.NewLine + dt.Rows[i + 1][c].ToString().Replace("Maker: :", "").Trim();
					//			}
					//		}
					//		else if (dt.Rows[_i][0].ToString().Contains("Drwg."))
					//		{
					//			for (int c = 0; c < dt.Columns.Count; c++)
					//			{
					//				drwStr = drwStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
					//			}
					//		}

					//		_i += 1;
					//	}
					//}
					//else if (dataSecondColValue.Contains("System"))
					//{
					//	for (int c = 1; c < dt.Columns.Count; c++)
					//	{
					//		systemStr = systemStr.Trim() + " " + dt.Rows[i][c].ToString().Trim();
					//	}

					//	subsystemStr = string.Empty;
					//	makerStr = string.Empty;
					//	partStr = string.Empty;
					//	serialStr = string.Empty;
					//	drwStr = string.Empty;

					//	int _i = i + 1;
					//	while (!GetTo.IsInt(dt.Rows[_i][0].ToString().Replace(".", "")))
					//	{
					//		if (dt.Rows[_i][0].ToString().Contains("Maker "))
					//		{
					//			for (int c = 0; c < dt.Columns.Count; c++)
					//			{
					//				makerStr = makerStr.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Replace("Maker", "").Replace(":", "").Trim();
					//			}
					//		}
					//		else if (dt.Rows[_i][0].ToString().Contains("Particulars"))
					//		{
					//			for (int c = 0; c < dt.Columns.Count; c++)
					//			{
					//				partStr = partStr + dt.Rows[_i][c].ToString().Replace("Particulars", "").Replace(":", "").Trim();
					//			}
					//		}
					//		else if (dt.Rows[_i][0].ToString().Contains("Serial"))
					//		{
					//			for (int c = 0; c < dt.Columns.Count; c++)
					//			{
					//				serialStr = serialStr + dt.Rows[_i][c].ToString().Replace("Serial No", "").Replace(":", "").Trim();
					//			}
					//		}
					//		else if (dt.Rows[_i][1].ToString().Contains("Subsystem"))
					//		{
					//			for (int c = 0; c < dt.Columns.Count; c++)
					//			{
					//				subsystemStr = subsystemStr + dt.Rows[_i][c].ToString().Replace("Subsystem", "").Replace(":", "").Trim();
					//			}

					//			subCheck = true;
					//		}
					//		else if (dt.Rows[_i][0].ToString().Contains("Maker : :"))
					//		{
					//			makerStr2 = string.Empty;
					//			for (int c = 0; c < dt.Columns.Count; c++)
					//			{
					//				makerStr2 = makerStr2.Trim() + Environment.NewLine + dt.Rows[i + 1][c].ToString().Replace("Maker: :", "").Trim();
					//			}
					//		}
					//		else if (dt.Rows[_i][0].ToString().Contains("Drwg."))
					//		{
					//			for (int c = 0; c < dt.Columns.Count; c++)
					//			{
					//				drwStr = drwStr.Trim() + " " + dt.Rows[_i][c].ToString().Trim();
					//			}
					//		}

					//		_i += 1;
					//	}
					//}
					//else if (dataFirstColValue.Contains("Subsystem") && !subCheck)
					//{
					//	// 다른 주제 항목 나오면 일단 리셋
					//	systemStr = string.Empty;
					//	subsystemStr = string.Empty;
					//	makerStr = string.Empty;
					//	partStr = string.Empty;
					//	serialStr = string.Empty;
					//	drwStr = string.Empty;

					//	for (int c = 0; c < dt.Columns.Count; c++)
					//	{
					//		subsystemStr = subsystemStr + dt.Rows[i][c].ToString().Replace("Subsystem", "").Replace(":", "").Trim();
					//	}

					//	if (dt.Rows[i + 1][0].ToString().Contains("Maker "))
					//	{
					//		makerStr2 = string.Empty;
					//		for (int c = 0; c < dt.Columns.Count; c++)
					//		{
					//			makerStr2 = makerStr2.Trim() + Environment.NewLine + dt.Rows[i + 1][c].ToString().Replace("Maker: :", "").Trim();
					//		}
					//	}
					//	else if (dt.Rows[i + 1][0].ToString().Contains("Drwg."))
					//	{
					//		for (int c = 0; c < dt.Columns.Count; c++)
					//		{
					//			drwStr = drwStr.Trim() + " " + dt.Rows[i + 1][c].ToString().Trim();
					//		}
					//	}
					//}
					//else if (dataSecondColValue.Contains("Subsystem") && !subCheck)
					//{
					//	// 다른 주제 항목 나오면 일단 리셋
					//	systemStr = string.Empty;
					//	subsystemStr = string.Empty;
					//	makerStr = string.Empty;
					//	partStr = string.Empty;
					//	serialStr = string.Empty;
					//	drwStr = string.Empty;

					//	for (int c = 0; c < dt.Columns.Count; c++)
					//	{
					//		subsystemStr = subsystemStr + dt.Rows[i][c].ToString().Replace("Subsystem", "").Replace(":", "").Trim();
					//	}

					//	if (dt.Rows[i + 1][0].ToString().Contains("Maker "))
					//	{
					//		makerStr2 = string.Empty;
					//		for (int c = 0; c < dt.Columns.Count; c++)
					//		{
					//			makerStr2 = makerStr2.Trim() + Environment.NewLine + dt.Rows[i + 1][c].ToString().Replace("Maker: :", "").Trim();
					//		}
					//	}
					//	else if (dt.Rows[i + 1][0].ToString().Contains("Drwg."))
					//	{
					//		for (int c = 0; c < dt.Columns.Count; c++)
					//		{
					//			drwStr = drwStr.Trim() + " " + dt.Rows[i + 1][c].ToString().Trim();
					//		}
					//	}
					//}
					//else if (dataFirstColValue.Contains("Maker "))
					//{
					//	makerStr = string.Empty;
					//	for (int c = 0; c < dt.Columns.Count; c++)
					//	{
					//		makerStr = makerStr.Trim() + Environment.NewLine + dt.Rows[i][c].ToString().Replace("Maker", "").Replace(":", "").Trim();
					//	}
					//}
					else if (dataFirstColValue.Equals("no"))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
							else if (dt.Rows[i][c].ToString().Equals("Part No")) _itemCode = c;
							else if (dt.Rows[i][c].ToString().StartsWith("DWG No")) _itemDwg = c;
							else if (dt.Rows[i][c].ToString().Equals("Maker")) _itemMaker = c;
							else if (dt.Rows[i][c].ToString().Equals("Qty Req")) _itemQt = c;
							else if (dt.Rows[i][c].ToString().Equals("Unit")) _itemUnit = c;
						}
					}
					else if (GetTo.IsInt(dataFirstColValue.Replace(".", "")))
					{
						string _unitqt = string.Empty;

						for (int c = 5; c < dt.Columns.Count; c++)
						{
							_unitqt = _unitqt + dt.Rows[i][c].ToString().Trim();
						}

						string[] unitqtValue = _unitqt.Split(' ');

						if (unitqtValue.Length == 2)
						{
							iTemQt = unitqtValue[0].ToString().Trim();
							iTemUnit = unitqtValue[1].ToString().Trim();
						}
						else
						{
							iTemQt = string.Empty;
							iTemUnit = string.Empty;
						}


						if (_itemCode != -1)
							iTemCode = dt.Rows[i][_itemCode].ToString().Trim();

						if (_itemDesc != -1)
							iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim();

						if (_itemUnit != -1)
							iTemUnit = dt.Rows[i][_itemUnit].ToString().Trim();

						if (_itemQt != -1)
							iTemQt = dt.Rows[i][_itemQt].ToString().Trim();


						int _i = i + 1;
						while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
						{
							for (int c = 1; c < dt.Columns.Count; c++)
							{
								descStr = descStr.Trim() + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();
							}
							_i += 1;

							if (_i >= dt.Rows.Count)
								break;
						}
						iTemDESC = iTemDESC.Trim() + Environment.NewLine + descStr.Trim();


						systemStr = systemStr.Trim();
						if (!string.IsNullOrEmpty(systemStr))
							iTemSUBJ = systemStr;

						makerStr = makerStr.Trim();
						if (!string.IsNullOrEmpty(makerStr))
							iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "MAKER: " + makerStr;

						partStr = partStr.Trim();
						if (!string.IsNullOrEmpty(partStr))
							iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "PARTICULARS: " + partStr;

						serialStr = serialStr.Trim();
						if (!string.IsNullOrEmpty(serialStr))
							iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "S/NO. : " + serialStr;


						subsystemStr = subsystemStr.Trim();
						if (!string.IsNullOrEmpty(subsystemStr))
							iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subsystemStr;

						if (!string.IsNullOrEmpty(makerStr2))
							iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + makerStr2.Trim();

						if (!string.IsNullOrEmpty(drwStr))
							iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + drwStr.Trim();

						iTemSUBJ = iTemSUBJ.Trim();



						if(!string.IsNullOrEmpty(subjStr))
						{
							iTemSUBJ = subjStr.Trim();
						}


						//ITEM ADD START
						dtItem.Rows.Add();
						//dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
						dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
						if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
						if (!string.IsNullOrEmpty(iTemSUBJ))
							dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ;
						dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

						iTemDESC = string.Empty;
						iTemUnit = string.Empty;
						iTemQt = string.Empty;
						iTemCode = string.Empty;
						iTemSUBJ = string.Empty;

						descStr = string.Empty;

						subCheck = false;

					}

				}
			}
		}
	}
}

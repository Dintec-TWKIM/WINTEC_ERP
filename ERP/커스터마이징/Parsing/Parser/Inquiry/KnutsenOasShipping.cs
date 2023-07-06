using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class KnutsenOasShipping
	{
		string vessel;
		string reference;
		DataTable dtItem;
		string contact;
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

		public string ImoNumber
		{
			get
			{
				return imoNumber;
			}
		}

		#endregion ==================================================================================================== Constructor



		public KnutsenOasShipping(string fileName)
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
			dtItem.Columns.Add("UNIQ");         //선사코드
			this.fileName = fileName;
			this.uc = new UnitConverter();
		}

		public void Parse()
		{
			int _itemVessel = -1;
			int _itemDesc = 0;
			int _itemQt = 0;

			int _itemCode = -1;

			int _specInt = -1;

			string iTemNo = string.Empty;
			string iTemSUBJ = string.Empty;
			string iTemCode = string.Empty;
			string iTemDESC = string.Empty;
			string iTemUnit = string.Empty;
			string iTemQt = string.Empty;



			string _componentString = string.Empty;
			string _specString = string.Empty;
			string _makerString = string.Empty;
			string _typeString = string.Empty;
			string _serialString = string.Empty;


			string nameString = string.Empty;
			string makerString = string.Empty;
			string typeString = string.Empty;
			string serialString = string.Empty;
			string codeString = string.Empty;
			string specString = string.Empty;
			string descString = string.Empty;
			string catalogStr = string.Empty;
			string impaStr = string.Empty;
			string nameSubStr = string.Empty;

			string descriptionStr = string.Empty;

			string subjStr = string.Empty;




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

			string[] itemName = { };

			bool itemStart = false;


			// 품목명 수정 해야함 두줄 세줄이 될 수도 있음.
			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string dataValue = dt.Rows[i][0].ToString().ToLower();


					//if (string.IsNullOrEmpty(contact))
					//{
					//    for (int c = 1; c < dt.Columns.Count; c++)
					//    {
					//        if (dt.Rows[i][c].ToString().ToLower().Contains("our reference:"))
					//            contact = dt.Rows[i + 1][c].ToString().Trim();
					//    }
					//}

					if (string.IsNullOrEmpty(imoNumber))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().StartsWith("imo n"))
								imoNumber = imoNumber.Trim() + dt.Rows[i + 1][c].ToString().Trim();
						}
					}

					if (string.IsNullOrEmpty(vessel))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().StartsWith("vessel name"))
								vessel = dt.Rows[i + 1][c].ToString().Replace("M/T", "").Trim();
						}
					}

					if (string.IsNullOrEmpty(reference))
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().StartsWith("rfq no"))
								reference = dt.Rows[i + 1][c].ToString().Trim();
						}
					}

					if (dataValue.StartsWith("items below are"))
					{
						int _i = i + 1;
						while (!dt.Rows[_i][0].ToString().ToLower().StartsWith("l.no"))
						{
							specString = specString.Trim() + Environment.NewLine + dt.Rows[_i][2].ToString().Trim();

							subjStr = subjStr.Trim() + Environment.NewLine + dt.Rows[_i][0].ToString() + dt.Rows[_i][1].ToString().Trim();

							_i += 1;

							if (_i >= dt.Rows.Count)
								break;
						}

						subjStr = subjStr.Trim();
						specString = specString.Trim();
					}
					else if (dataValue.StartsWith("component info"))
					{

						subjStr = string.Empty;

						int _i = i + 1;

						while (!dt.Rows[_i][0].ToString().ToLower().StartsWith("l.no") && !GetTo.IsInt(dt.Rows[_i][0].ToString()))
						{
							for(int c =0; c < dt.Columns.Count; c++)
							{
								subjStr = subjStr + " " + dt.Rows[_i][c].ToString().Trim();
							}

							subjStr = subjStr.Trim() + Environment.NewLine;
							subjStr = subjStr.Replace("\r\n ", "\r\n").Replace("   ", "").Replace("  ", "");

							_i += 1;

							if (_i >= dt.Rows.Count)
								break;
						}
					}
					else if (dataValue.StartsWith("l.no"))
					{
						for (int c = 1; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().ToLower().Contains("qty")) _itemQt = c;
							else if (dt.Rows[i][c].ToString().ToLower().Contains("description")) _itemDesc = c;
							else if (dt.Rows[i][c].ToString().ToLower().Contains("suppliers re")) _specInt = c;
						}
					}
					else if (GetTo.IsInt(dataValue))
					{
						if (_itemDesc != -1)
						{
							iTemDESC = dt.Rows[i][_itemDesc].ToString().Trim() + Environment.NewLine;

							int _i = i + 1;

							while (!GetTo.IsInt(dt.Rows[_i][0].ToString()) && string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
							{
								for (int c = _itemQt; c <= _specInt; c++)
								{
									if (!string.IsNullOrEmpty(dt.Rows[_i][c].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[_i][c + 1].ToString().Trim()))
										iTemDESC = iTemDESC + " " + dt.Rows[_i][c].ToString().Trim();
								}

								iTemDESC = iTemDESC.Trim() + Environment.NewLine;
								iTemDESC = iTemDESC.Replace("\r\n ", "\r\n").Replace("   ", "").Replace("  ","");


								_i += 1;

								if (_i >= dt.Rows.Count)
									break;
							}
						}

						if (_itemQt != -1)
						{
							string[] qtSpl = dt.Rows[i][_itemQt].ToString().Split(' ');

							if (qtSpl.Length == 2)
							{
								iTemQt = qtSpl[0].ToString().Trim();
								iTemUnit = qtSpl[1].ToString().Trim();
							}
							else if (qtSpl.Length == 1)
							{
								iTemQt = qtSpl[0].ToString();

								if (iTemDESC.StartsWith("PCE"))
								{
									iTemUnit = "PCE";
									iTemDESC = iTemDESC.Substring(3, iTemDESC.Length - 3).Trim();
								}

								if (iTemDESC.StartsWith("PCS"))
								{
									iTemUnit = "PCS";
									iTemDESC = iTemDESC.Substring(3, iTemDESC.Length - 3).Trim();
								}
							}
						}

						if (_specInt != -1)
						{
							specString = dt.Rows[i][_specInt].ToString().Trim();

							if (string.IsNullOrEmpty(specString))
							{
								specString = dt.Rows[i - 1][_specInt].ToString().Trim() + Environment.NewLine + dt.Rows[i + 1][_specInt].ToString().Trim();
								specString = specString.Replace("Suppliers Reference", "").Trim();

								if (iTemDESC.Contains(specString))
									specString = string.Empty;
							}

							if (!string.IsNullOrEmpty(specString))
								iTemDESC = iTemDESC.Trim() + Environment.NewLine + specString.Trim();

							if (!string.IsNullOrEmpty(specString))
								iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + specString.Trim();

						}


						if (!string.IsNullOrEmpty(subjStr))
							iTemSUBJ = subjStr;

						iTemSUBJ = iTemSUBJ.Trim();


						// 공백 제거 
						if (iTemDESC.Contains("\r\n "))
						{
							while (iTemDESC.Contains("\r\n "))
							{
								iTemDESC = iTemDESC.Replace("\r\n ", "\r\n").Trim();
							}
						}

						// 공백 제거 
						if (iTemSUBJ.Contains("\r\n "))
						{
							while (iTemSUBJ.Contains("\r\n "))
							{
								iTemSUBJ = iTemSUBJ.Replace("\r\n ", "\r\n").Trim();
							}
						}


						iTemSUBJ = iTemSUBJ.ToLower().Replace("maker :\r\n", "").Replace("type :\r\n", "").Trim();

						if (iTemSUBJ.ToLower().EndsWith("serial no :"))
							iTemSUBJ = iTemSUBJ.Replace("serial no :", "").Trim();

						iTemSUBJ = iTemSUBJ.Replace("\r\n", "").Trim();


						//ITEM ADD START
						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = dataValue;
						dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
						if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
						// 주제가 없는 경우가 있음, 없을때는 FOR 제거
						if (!string.IsNullOrEmpty(iTemSUBJ))
							dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ.Trim();
						dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

						iTemDESC = string.Empty;
						iTemUnit = string.Empty;
						iTemQt = string.Empty;
						iTemCode = string.Empty;

						makerString = string.Empty;
						typeString = string.Empty;
						codeString = string.Empty;
						specString = string.Empty;
						nameSubStr = string.Empty;
						impaStr = string.Empty;

						descriptionStr = string.Empty;
					}

					//	if (!itemStart)
					//	{
					//		if (dataValue.Contains("Supplier:"))
					//		{
					//			if (_itemVessel == -1)
					//			{
					//				// 선명 위치 확인
					//				for (int c = 0; c < dt.Columns.Count; c++)
					//				{
					//					if (dt.Rows[i][c].ToString().Contains("Vessel")) _itemVessel = c;    // 선명
					//				}

					//				vessel = dt.Rows[i + 1][_itemVessel].ToString().Trim();
					//				reference = dt.Rows[i + 3][_itemVessel].ToString().Trim();

					//				// 선명이 없으면 + 1
					//				if (string.IsNullOrEmpty(vessel))
					//				{
					//					vessel = dt.Rows[i + 1][_itemVessel + 1].ToString().Replace("LNG/c", "").Trim();
					//				}

					//				// 문의번호가 없으면 + 1
					//				if (string.IsNullOrEmpty(reference))
					//				{
					//					reference = dt.Rows[i + 3][_itemVessel + 1].ToString().Trim();
					//				}

					//				vessel = vessel.Replace("M/T", "").Trim();
					//			}
					//		}

					//		if (dataValue.Contains("Component Info"))
					//		{
					//			// Component Info와 항상 같은 row
					//			for (int c = 1; c < dt.Columns.Count; c++)
					//			{
					//				if (dt.Rows[i][c].ToString().Contains("Specification")) _specInt = c;
					//			}


					//			int _i = i;
					//			while (!dt.Rows[_i][0].ToString().Equals("L.no"))
					//			{
					//				if (!_specInt.Equals(-1))
					//				{
					//					for (int specc = _specInt; specc < dt.Columns.Count; specc++)
					//					{
					//						_specString = _specString.Trim() + Environment.NewLine + dt.Rows[_i][specc].ToString().Trim();
					//						_specString = _specString.Replace("Specification :", "").Trim();
					//					}
					//				}

					//				for (int c = 0; c < dt.Columns.Count; c++)
					//				{
					//					if (dt.Rows[_i][c].ToString().Contains("Component :"))
					//					{

					//						// 주제가 여러 column에 있음, specification 앞까지 반복 추가
					//						if (!_specInt.Equals(-1))
					//						{
					//							for (int cc = 1; cc < _specInt; cc++)
					//							{
					//								_componentString = _componentString.Trim() + " " + dt.Rows[_i][cc].ToString().Trim();
					//							}
					//						}
					//						else
					//						{
					//							// specification 없는 경우에는 column 끝까지 반복
					//							for (int cc = 1; cc < dt.Columns.Count; cc++)
					//							{
					//								_componentString = _componentString.Trim() + " " + dt.Rows[_i][cc].ToString().Trim();
					//							}
					//						}
					//					}

					//					// SUBJ 항목들
					//					if (dt.Rows[_i][c].ToString().Contains("Maker:"))
					//						if (!string.IsNullOrEmpty(dt.Rows[_i][c + 1].ToString()))
					//							_makerString = dt.Rows[_i][c + 1].ToString().Trim();
					//						else
					//							if (c + 2 < dt.Columns.Count)
					//							_makerString = dt.Rows[_i][c + 2].ToString().Trim();

					//					if (dt.Rows[_i][c].ToString().Contains("Makers type:"))
					//						if (!string.IsNullOrEmpty(dt.Rows[_i][c + 1].ToString()))
					//							_typeString = dt.Rows[_i][c + 1].ToString().Trim();
					//						else
					//							if (c + 2 < dt.Columns.Count)
					//							_typeString = dt.Rows[_i][c + 2].ToString().Trim();

					//					if (dt.Rows[_i][c].ToString().Contains("Serial No"))
					//						if (!string.IsNullOrEmpty(dt.Rows[_i][c + 1].ToString()))
					//							_serialString = dt.Rows[_i][c + 1].ToString().Trim();
					//						else
					//							if (c + 2 < dt.Columns.Count)
					//							_serialString = dt.Rows[_i][c + 2].ToString().Trim();

					//				}

					//				_i += 1;

					//			}

					//		}



					//	}
					//	else
					//	{
					//		//################# 순번, 주제, 품목코드, 품목명, 단위, 수량
					//		string firstColString = dt.Rows[i][0].ToString();

					//		if (firstColString.Contains("Component Info"))
					//		{
					//			int _i = i;

					//			while (!dt.Rows[_i][0].ToString().Equals("L.no"))
					//			{
					//				for (int c = 0; c < dt.Columns.Count; c++)
					//				{
					//					if (dt.Rows[_i][c].ToString().Contains("Component :"))
					//					{
					//						if (!_specInt.Equals(-1))
					//						{
					//							for (int cc = 1; cc < _specInt; cc++)
					//							{
					//								_componentString = _componentString.Trim() + " " + dt.Rows[_i][cc].ToString().Trim();
					//							}
					//						}
					//						else
					//						{
					//							for (int cc = 1; cc < dt.Columns.Count; cc++)
					//							{
					//								_componentString = _componentString.Trim() + " " + dt.Rows[_i][cc].ToString().Trim();
					//							}
					//						}
					//					}

					//					//if (!string.IsNullOrEmpty(dt.Rows[_i][c + 1].ToString()))
					//					//    _componentString = dt.Rows[_i][c + 1].ToString().Trim();
					//					//else
					//					//    _componentString = dt.Rows[_i][c + 2].ToString().Trim();

					//					if (dt.Rows[_i][c].ToString().Contains("Maker:"))
					//						if (!string.IsNullOrEmpty(dt.Rows[_i][c + 1].ToString()))
					//							_makerString = dt.Rows[_i][c + 1].ToString().Trim();
					//						else
					//							if (c + 2 < dt.Columns.Count)
					//							_makerString = dt.Rows[_i][c + 2].ToString().Trim();

					//					if (dt.Rows[_i][c].ToString().Contains("Makers type:"))
					//						if (!string.IsNullOrEmpty(dt.Rows[_i][c + 1].ToString()))
					//							_typeString = dt.Rows[_i][c + 1].ToString().Trim();
					//						else
					//							if (c + 2 < dt.Columns.Count)
					//							_typeString = dt.Rows[_i][c + 2].ToString().Trim();

					//					if (dt.Rows[_i][c].ToString().Contains("Serial No"))
					//						if (!string.IsNullOrEmpty(dt.Rows[_i][c + 1].ToString()))
					//							_serialString = dt.Rows[_i][c + 1].ToString().Trim();
					//						else
					//							if (c + 2 < dt.Columns.Count)
					//							_serialString = dt.Rows[_i][c + 2].ToString().Trim();

					//					//if (dt.Rows[_i][c].ToString().Contains("Specification"))
					//					//{
					//					//    if (c + 1 < dt.Columns.Count)
					//					//        _specString = dt.Rows[_i][c + 1].ToString().Trim();
					//					//}

					//					//if (dt.Rows[_i][c].ToString().Contains("Cap:"))
					//					//    _specString = dt.Rows[_i][c].ToString().Replace("Cap:", "").Trim();
					//				}

					//				_i += 1;

					//			}

					//		}


					//		if (firstColString.Equals("L.no"))
					//		{
					//			for (int c = 1; c < dt.Columns.Count; c++)
					//			{
					//				if (dt.Rows[i][c].ToString().Equals("Description")) _itemDesc = c;
					//				else if (dt.Rows[i][c].ToString().Equals("Qty")) _itemQt = c;
					//				else if (dt.Rows[i][c].ToString().ToLower().Contains("suppliers ref")) _itemCode = c;

					//			}

					//		}



					//		//if (_itemDesc == 0)
					//		//{
					//		//    // 수량,단위,품목명 위치 확인
					//		//    for (int c = 0; c < dt.Columns.Count; c++)
					//		//    {
					//		//        if (dt.Rows[i][c].ToString().Contains("Qty"))
					//		//        {
					//		//            _itemQt = c;         // 수량 
					//		//            _itemUnit = c + 1;       // 단위
					//		//        }
					//		//        else if (dt.Rows[i][c].ToString().Contains("Description")) _itemDesc = c;     // 품목명
					//		//        else if (dt.Rows[i][c].ToString().Contains("Suppliers")) _itemCode = c;
					//		//    }

					//		//    if (_itemDesc == _itemUnit)
					//		//    {
					//		//        _itemDesc = _itemDesc + 1;
					//		//    }
					//		//}

					//		if (GetTo.IsInt(firstColString))
					//		{
					//			if (!string.IsNullOrEmpty(dt.Rows[i - 1][0].ToString()) && !dt.Rows[i - 1][0].ToString().Contains("L.no"))
					//			{
					//				subjStr = string.Empty;
					//				for (int c = 0; c < dt.Columns.Count; c++)
					//					subjStr = subjStr.Trim() + " " + dt.Rows[i - 1][c].ToString().Trim();
					//			}

					//			iTemNo = firstColString;
					//			iTemQt = dt.Rows[i][_itemQt].ToString();
					//			//iTemUnit = dt.Rows[i][_itemUnit].ToString();

					//			if (string.IsNullOrEmpty(iTemQt))
					//			{
					//				iTemQt = dt.Rows[i][_itemQt + 1].ToString().Trim();
					//			}

					//			iTemSUBJ = _componentString + Environment.NewLine;

					//			if (!string.IsNullOrEmpty(subjStr))
					//				iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr.Trim();

					//			if (!string.IsNullOrEmpty(_makerString))
					//				iTemSUBJ = iTemSUBJ + "MAKER: " + _makerString + Environment.NewLine;

					//			if (!string.IsNullOrEmpty(_typeString))
					//				iTemSUBJ = iTemSUBJ + "TYPE: " + _typeString + Environment.NewLine;

					//			if (!string.IsNullOrEmpty(_serialString))
					//				iTemSUBJ = iTemSUBJ + "S/NO. : " + _serialString + Environment.NewLine;

					//			if (!string.IsNullOrEmpty(_specString))
					//				iTemSUBJ = iTemSUBJ + _specString;
					//			iTemSUBJ = iTemSUBJ.Trim();


					//			int _i = i + 1;
					//			nameString = dt.Rows[i][_itemDesc].ToString().Trim();
					//			while (string.IsNullOrEmpty(dt.Rows[_i][0].ToString()))
					//			{


					//				for (int c = 1; c < dt.Columns.Count; c++)
					//				{
					//					if (dt.Rows[_i][c].ToString().Contains("Maker"))
					//					{
					//						makerString = dt.Rows[_i][c + 1].ToString().Trim();

					//						if (c + 2 < dt.Columns.Count)
					//							makerString = makerString + dt.Rows[_i][c + 2].ToString().Trim();
					//					}
					//					else if (dt.Rows[_i][c].ToString().Contains("Part No") && string.IsNullOrEmpty(codeString))
					//					{
					//						codeString = dt.Rows[_i][c + 1].ToString().Trim();

					//						if (c + 2 < dt.Columns.Count)
					//							codeString = codeString + dt.Rows[_i][c + 2].ToString().Trim();
					//					}
					//					else if (dt.Rows[_i][c].ToString().Contains("Type"))
					//					{
					//						typeString = dt.Rows[_i][c + 1].ToString().Trim();

					//						if (c + 2 < dt.Columns.Count)
					//							typeString = typeString + dt.Rows[_i][c + 2].ToString().Trim();
					//					}
					//					else if (dt.Rows[_i][c].ToString().Contains("Specification"))
					//					{
					//						specString = dt.Rows[_i][c + 1].ToString().Trim() + dt.Rows[_i][c + 2].ToString().Trim();
					//						int _ii = _i + 1;
					//						while (string.IsNullOrEmpty(dt.Rows[_ii][0].ToString()))
					//						{
					//							specString = specString.Trim() + Environment.NewLine + dt.Rows[_ii][c].ToString().Trim() + dt.Rows[_ii][c + 1].ToString().Trim();

					//							if (c + 2 < dt.Columns.Count)
					//								specString = specString + dt.Rows[_ii][c + 2].ToString().Trim();

					//							_ii += 1;
					//						}
					//					}
					//					else if (dt.Rows[_i][c].ToString().StartsWith("Catalog No"))
					//					{
					//						catalogStr = dt.Rows[_i][c + 1].ToString().Trim() + dt.Rows[_i][c + 2].ToString().Trim();
					//					}
					//					else if (dt.Rows[_i][c].ToString().StartsWith("Description"))
					//					{
					//						descriptionStr = dt.Rows[_i][c + 1].ToString().Trim() + dt.Rows[_i][c + 2].ToString().Trim();
					//					}
					//					else if (dt.Rows[_i][c].ToString().StartsWith("IMPA No"))
					//					{
					//						impaStr = dt.Rows[_i][c + 1].ToString().Trim() + dt.Rows[_i][c + 2].ToString().Trim();
					//					}
					//					else
					//					{
					//						if (!dt.Rows[_i][c - 1].ToString().ToUpper().StartsWith("DESCRIPTION"))
					//							nameSubStr = dt.Rows[_i][c].ToString().Trim();

					//						if (!string.IsNullOrEmpty(nameSubStr))
					//							break;
					//					}
					//				}
					//				_i += 1;

					//				if (_i >= dt.Rows.Count)
					//					break;
					//			}

					//			iTemDESC = nameString.Trim();

					//			if (!string.IsNullOrEmpty(nameSubStr) && !iTemDESC.Contains(nameSubStr))
					//				iTemDESC = iTemDESC.Trim() + " " + nameSubStr.Trim();

					//			if (!string.IsNullOrEmpty(makerString) && !iTemDESC.Contains(makerString))
					//				iTemDESC = iTemDESC.Trim() + Environment.NewLine + "MAKER: " + makerString;

					//			if (!string.IsNullOrEmpty(typeString) && !iTemDESC.Contains(typeString))
					//				iTemDESC = iTemDESC.Trim() + Environment.NewLine + "TYPE: " + typeString;

					//			if (!string.IsNullOrEmpty(specString) && !iTemDESC.Contains(specString))
					//				iTemDESC = iTemDESC.Trim() + Environment.NewLine + specString;

					//			if (!string.IsNullOrEmpty(descString) && !iTemDESC.Contains(descString))
					//				iTemDESC = iTemDESC.Trim() + Environment.NewLine + descString;

					//			//if(!string.IsNullOrEmpty(codeString))
					//			//    iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + "DWG: " + codeString;

					//			if (!string.IsNullOrEmpty(catalogStr) && !iTemDESC.Contains(catalogStr))
					//				iTemDESC = iTemDESC.Trim() + Environment.NewLine + "CATALOG NO: " + catalogStr.Trim();

					//			if (!string.IsNullOrEmpty(descriptionStr) && !iTemDESC.Contains(descriptionStr))
					//				iTemDESC = iTemDESC.Trim() + Environment.NewLine + "DESCRIPTION: " + descriptionStr.Trim();

					//			if (!string.IsNullOrEmpty(impaStr))
					//				iTemCode = impaStr.Trim();




					//			if (codeString.Contains("Dwg") || codeString.Contains("DWG") || codeString.Contains("Drwg."))
					//			{
					//				codeString = codeString.Replace("item 2 Dwg", "").Replace("Dwg", "").Replace(".", "").Trim();
					//			}
					//			else
					//			{
					//				if (string.IsNullOrEmpty(iTemCode))
					//					iTemCode = codeString.Replace("item 2 Dwg", "").Replace("Dwg", "").Trim();
					//			}

					//			if (codeString.Contains("Item"))
					//			{
					//				string[] codeValue = codeString.Split(' ');
					//				iTemCode = codeValue[codeValue.Length - 1].ToString().Trim();
					//				if (!string.IsNullOrEmpty(iTemCode))
					//					iTemSUBJ = iTemSUBJ.Replace("Item", "").Replace(iTemCode, "").Trim();
					//			}

					//			//if (iTemDESC.Contains("PCE") || iTemDESC.Contains("PCS"))
					//			//{
					//			//    iTemDESC = iTemDESC.Replace("PCS", "").Replace("PCE", "").Replace(":", "").Trim();
					//			//    iTemUnit = "PCS";
					//			//}

					//			string[] descSpl = iTemDESC.Split(' ');

					//			if (descSpl.Length > 1)
					//			{
					//				iTemUnit = descSpl[0].ToString().Trim();

					//				string descStr = string.Empty;

					//				for (int r = 1; r < descSpl.Length; r++)
					//				{
					//					descStr = descStr.Trim() + " " + descSpl[r].ToString().Trim();
					//				}

					//				iTemDESC = descStr.Trim();

					//			}


					//			if (string.IsNullOrEmpty(iTemCode))
					//				iTemCode = codeString;

					//			if (string.IsNullOrEmpty(iTemCode) && _itemCode != -1)
					//				iTemCode = dt.Rows[i][_itemCode].ToString().Trim();


					//			//ITEM ADD START
					//			dtItem.Rows.Add();
					//			dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = iTemNo;
					//			dtItem.Rows[dtItem.Rows.Count - 1]["DESC"] = iTemDESC;
					//			dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = uc.Convert(iTemUnit);
					//			if (GetTo.IsInt(iTemQt.Replace(".", ""))) dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = iTemQt;
					//			// 주제가 없는 경우가 있음, 없을때는 FOR 제거
					//			if (!string.IsNullOrEmpty(iTemSUBJ))
					//				dtItem.Rows[dtItem.Rows.Count - 1]["SUBJ"] = "FOR " + iTemSUBJ.Trim();
					//			dtItem.Rows[dtItem.Rows.Count - 1]["ITEM"] = iTemCode;

					//			iTemDESC = string.Empty;
					//			iTemUnit = string.Empty;
					//			iTemQt = string.Empty;
					//			iTemCode = string.Empty;

					//			makerString = string.Empty;
					//			typeString = string.Empty;
					//			codeString = string.Empty;
					//			specString = string.Empty;
					//			nameSubStr = string.Empty;
					//			impaStr = string.Empty;

					//			descriptionStr = string.Empty;
					//		}
					//	}
				}
			}
		}
	}
}

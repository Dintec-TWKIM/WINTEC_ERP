using Dintec;
using Dintec.Parser;

using System;
using System.Data;

namespace Parsing
{
	class Zodiac
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



		public Zodiac(string fileName)
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
			string iTemDrw = string.Empty;


			int _descDrwInt = -1;
			int _descDescInt = -1;
			int _descQtInt = -1;
			int _descUnitInt = -1;
			int _descCodeInt = -1;
			int _descOriginInt = -1;

			string subjStr = string.Empty;
			string subjStr2 = string.Empty;
			string subjStr3 = string.Empty;

			string originStr = string.Empty;


			string noStr = string.Empty;

			int _firstInt = -1;

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

			foreach (DataTable dt in ds.Tables)
			{

				for (int i = 0; i < dt.Rows.Count; i++)
				{
					if (_firstInt == -1)
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							for (int r = 0; r < dt.Rows.Count; r++)
							{
								if (dt.Rows[r][c].ToString().ToLower().Contains("part no."))
									_firstInt = c;
							}
						}
					}
					else
					{

						string firstColStr = dt.Rows[i][_firstInt].ToString().ToLower();


						// VESSEL
						if (string.IsNullOrEmpty(vessel))
						{
							for (int c = 1; c < dt.Columns.Count; c++)
							{
								if (dt.Rows[i][c].ToString().Contains("Vessel:"))
									vessel = dt.Rows[i][c + 1].ToString().Trim();
							}
						}

						// REFERENCE
						if (firstColStr.StartsWith("requisition number"))
						{
							for(int c = _firstInt+1; c < dt.Columns.Count; c++)
							{
								if (string.IsNullOrEmpty(reference))
									reference = dt.Rows[i][c].ToString().Trim();
							}
						}
						else if (firstColStr.StartsWith("part no"))
						{
							string forStr = string.Empty;

							for (int c = 0; c < dt.Columns.Count; c++)
							{
								forStr = dt.Rows[i][c].ToString().ToLower().Trim();
								if (forStr.Contains("part no")) _descCodeInt = c;
								else if (forStr.Contains("draw no")) _descDrwInt = c;
								else if (forStr.Contains("full description")) _descDescInt = c;
								else if (forStr.Contains("required qty")) _descQtInt = c;
								else if (forStr.Equals("unit")) _descUnitInt = c;
								else if (forStr.Contains("origin")) _descOriginInt = c;
							}
						}
						// SUBJECT
						else if (firstColStr.ToUpper().StartsWith("FUNCTION"))
						{
							subjStr = string.Empty;
							for(int c = _firstInt; c < dt.Columns.Count; c++)
							{
								subjStr = subjStr + " " + dt.Rows[i][c].ToString().Replace("function:", "").Trim();
							}
							//subjStr = firstColStr.Replace("function:", "").Trim();
						}
						else if (firstColStr.ToUpper().StartsWith("ADDITIONAL"))
						{
							subjStr3 = string.Empty;
							for (int c = _firstInt; c < dt.Columns.Count; c++)
							{
								subjStr3 = subjStr3 + " " + dt.Rows[i][c].ToString().Replace("Additional Info: ", "").Trim();
							}
						}
						else if (firstColStr.ToUpper().StartsWith("LOCATION"))
						{
							subjStr2 = string.Empty;
							{
							for (int c = _firstInt; c < dt.Columns.Count; c++)
								subjStr2 = subjStr + " " + dt.Rows[i][c].ToString().Replace("location:", "").Trim();
							}
							//subjStr2 = firstColStr.Replace("location:", "").Trim();
						}

						string qt = string.Empty;
						if (!_descQtInt.Equals(-1))
							qt = dt.Rows[i][_descQtInt].ToString().Trim();

						if (GetTo.IsInt(qt))
						{
							qt = string.Empty;

							if (_descDescInt != -1)
								iTemDESC = dt.Rows[i][_descDescInt].ToString().Trim();

							if (_descCodeInt != -1)
							{
								iTemCode = dt.Rows[i][_descCodeInt].ToString().Trim();

								if (iTemCode.Equals("-"))
									iTemCode = string.Empty;
							}

							if (_descQtInt != -1)
								iTemQt = dt.Rows[i][_descQtInt].ToString().Trim();

							if (_descUnitInt != -1)
								iTemUnit = dt.Rows[i][_descUnitInt].ToString().Trim();

							if (_descDrwInt != -1)
							{
								iTemDrw = dt.Rows[i][_descDrwInt].ToString().Trim();

								if (iTemDrw.Equals("-"))
									iTemDrw = string.Empty;
							}


							if (!string.IsNullOrEmpty(iTemDrw))
								iTemDESC = iTemDESC.Trim() + Environment.NewLine + "DWG.NO.: " + iTemDrw.Trim();

							if (!string.IsNullOrEmpty(subjStr))
								iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr.Trim();

							if (!string.IsNullOrEmpty(subjStr2))
								iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr2.Trim();

							if(!string.IsNullOrEmpty(subjStr3))
								iTemSUBJ = iTemSUBJ.Trim() + Environment.NewLine + subjStr3.Trim();


							//ITEM ADD START
							dtItem.Rows.Add();
							//dtItem.Rows[dtItem.Rows.Count - 1]["NO"] = firstColStr;
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
							iTemDrw = string.Empty;
						}
					}
				}
			}
		}
	}
}

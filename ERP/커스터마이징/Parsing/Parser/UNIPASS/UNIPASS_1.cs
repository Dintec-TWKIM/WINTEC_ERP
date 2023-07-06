using Dintec;
using System;
using System.Data;
using System.Linq;

namespace Parsing.Parser.UNIPASS
{
	class UNIPASS_1
	{
		DataTable dtItem;

		string fileName;
		bool lastCh = false;

		#region ==================================================================================================== Property

		public DataTable Item
		{
			get
			{
				return dtItem;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public UNIPASS_1(string fileName)
		{
			dtItem = new DataTable();

			dtItem.Columns.Add("NO_TAX");           // 신고번호	
			dtItem.Columns.Add("NO_IMPORT");        // 수입신고번호
			dtItem.Columns.Add("DC_TAX");           // 신고인기재란


			this.fileName = fileName;
		}

		#endregion


		public void Parse()
		{
			string NO_TAX = string.Empty;
			string NO_IMPORT = string.Empty;
			string DC_TAX = string.Empty;


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
					string firstColStr = dt.Rows[i][0].ToString().Trim();


					if(string.IsNullOrEmpty(NO_TAX))
					{
						for(int c = 0; c < dt.Columns.Count; c++)
						{
							if(dt.Rows[i][c].ToString().Replace(" ","").Contains("5신고번호"))
							{
								NO_TAX = dt.Rows[i + 2][c].ToString().Trim();
							}

							if(NO_TAX.Length != 16)
							{
								NO_TAX = "";
							}
						}
					}


					if(string.IsNullOrEmpty(NO_IMPORT))
					{
						for(int c = 0; c < dt.Columns.Count; c++)
						{
							if(dt.Rows[i][c].ToString().Replace(" ","").Contains("40수입신고번호"))
							{
								NO_IMPORT = dt.Rows[i][c + 1].ToString().Trim();
							}
						}
					}

					if(string.IsNullOrEmpty(DC_TAX))
					{
						for(int c = 0; c < dt.Columns.Count; c++)
						{
							if(dt.Rows[i][c].ToString().Replace(" ","").Contains("신고인기재란"))
							{
								DC_TAX = string.Empty;

								int _i = i + 1;

								while (!dt.Rows[_i][c].ToString().Replace(" ", "").Contains("53운송(신고)인"))
								{
									DC_TAX = DC_TAX + Environment.NewLine + dt.Rows[_i][c].ToString().Trim();

									_i += 1;

									if (_i > i + 5)
										break;

									lastCh = true;
								}
							}
						}
					}



					if (!string.IsNullOrEmpty(NO_TAX) && lastCh)
					{
						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_TAX"] = NO_TAX;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_IMPORT"] = NO_IMPORT;
						if (string.IsNullOrEmpty(DC_TAX))
							dtItem.Rows[dtItem.Rows.Count - 1]["DC_TAX"] = "-";
						else
							dtItem.Rows[dtItem.Rows.Count - 1]["DC_TAX"] = DC_TAX;

						break;
					}
				}
			}
		}
	}
}

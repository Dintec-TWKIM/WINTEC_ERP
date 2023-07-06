using Dintec;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parsing.Parser.Order
{
	class MANOrder_pdf
	{
		DataTable dtItem;

		string fileName;

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

		public MANOrder_pdf(string fileName)
		{
			dtItem = new DataTable();
			dtItem.Columns.Add("NO_PO_PARTNER");                  // 계약번호
			dtItem.Columns.Add("CD_PARTNER");                        // 구매회사
			dtItem.Columns.Add("NO_SO");                     // 공사번호
			dtItem.Columns.Add("TXT_USERDEF3");
			dtItem.Columns.Add("CD_ITEM");           // 도면번호
			dtItem.Columns.Add("TXT_USERDEF6");
			dtItem.Columns.Add("QT_SO");
			dtItem.Columns.Add("CD_EXCH");                     // 화폐
			dtItem.Columns.Add("DT_DUEDATE");        // 수정계약납기(최종)
			dtItem.Columns.Add("DT_SO");                     // 계약일
			dtItem.Columns.Add("UM_SO");
			dtItem.Columns.Add("CD_USERDEF3");
			dtItem.Columns.Add("CD_USERDEF1");
			dtItem.Columns.Add("CD_USERDEF2");
			dtItem.Columns.Add("TXT_USERDEF4");

			this.fileName = fileName;
		}

		#endregion


		public void Parse()
		{
			string 계약번호 = string.Empty;
			string 계약번호front = string.Empty;

			string NO_SO = string.Empty;
			string 도면번호 = string.Empty;
			string QT_SO = string.Empty;
			string 화폐 = string.Empty;
			string 수정계약납기 = string.Empty;
			string 계약일 = string.Empty;
			string UM_SO = string.Empty;
			string CD_USERDEF1 = string.Empty;
			string CD_USERDEF3 = string.Empty;


			int _도면번호 = -1;
			int _QT_SO = -1;
			int _화폐 = -1;
			int _수정계약납기 = -1;
			int _계약일 = -1;
			int _UM_SO = -1;
			int _CD_USERDEF3 = -1;
			int _CD_USERDEF1 = -1;

			int _firstNo = -1;




			string xmlTemp = PdfReader.ToXml(fileName);

			int pageCount = xmlTemp.Count("<page>");

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


			foreach (DataTable dt in ds.Tables)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					if (_firstNo == -1)
					{
						for (int c = 0; c < dt.Columns.Count; c++)
						{
							if (dt.Rows[i][c].ToString().Contains("WINWIN INTEC CO"))
								_firstNo = c;
						}

					}
					else
					{

						string firstColStr = dt.Rows[i][_firstNo].ToString().Trim();


						if (string.IsNullOrEmpty(계약번호front))
						{
							for (int c = 1; c < dt.Columns.Count; c++)
							{
								if (dt.Rows[i][c].ToString().Contains("Purchase order no. :"))
								{
									for (int cc = c; cc < dt.Columns.Count; cc++)
									{
										if(string.IsNullOrEmpty(계약번호front))
											계약번호front = dt.Rows[i][cc].ToString().Replace("Purchase order no. :", "").Trim();
									}
								}
							}
						}


						if (string.IsNullOrEmpty(계약일))
						{
							for (int c = 1; c < dt.Columns.Count; c++)
							{
								if (dt.Rows[i][c].ToString().Contains("Date"))
									계약일 = dt.Rows[i + 1][c].ToString().Trim();
							}
						}


						if (string.IsNullOrEmpty(화폐))
						{
							for (int c = 1; c < dt.Columns.Count; c++)
							{
								if (dt.Rows[i][c].ToString().Contains("Currency"))
									화폐 = dt.Rows[i + 1][c].ToString().Trim();
							}
						}

						if (firstColStr.Equals("Item"))
						{
							for (int c = 0; c < dt.Columns.Count; c++)
							{
								if (dt.Rows[i][c].ToString().Contains("Specification")) _도면번호 = c;
								else if (dt.Rows[i][c].ToString().Equals("Qty")) _QT_SO = c;
								else if (dt.Rows[i][c].ToString().Contains("화폐")) _화폐 = c;
								else if (dt.Rows[i][c].ToString().Contains("Time of delivery")) _수정계약납기 = c;
								else if (dt.Rows[i][c].ToString().Contains("Price")) _UM_SO = c;
							}
						}
						if (GetTo.IsInt(firstColStr) && firstColStr.Length < 4)
						{
							계약번호 = 계약번호front + "-" + firstColStr;

							if (_QT_SO != -1)
							{
								string[] qtSpl = dt.Rows[i][_QT_SO].ToString().Split(' ');

								if (qtSpl.Length == 2)
								{
									QT_SO = qtSpl[0].ToString().Trim();
								}
							}

							if (_도면번호 != -1)
							{
								도면번호 = dt.Rows[i][_도면번호].ToString().Trim();

								int _i = i + 1;

								while (string.IsNullOrEmpty(dt.Rows[_i][_firstNo].ToString()))
								{
									if (dt.Rows[i][_도면번호].ToString().Contains("Engine type :"))
										CD_USERDEF3 = dt.Rows[i][_도면번호 + 1].ToString().Trim();

									if (dt.Rows[_i][_도면번호].ToString().StartsWith("Certificate"))
										CD_USERDEF1 = dt.Rows[i][_도면번호 + 1].ToString().Trim();

									_i += 1;


									if (_i >= dt.Rows.Count)
										break;
								}
							}

							if (_수정계약납기 != -1)
								수정계약납기 = dt.Rows[i][_수정계약납기].ToString().Trim();

							if (_UM_SO != -1)
							{
								string[] umSpl = dt.Rows[i][_UM_SO].ToString().Split('/');

								if (umSpl.Length == 2)
								{
									UM_SO = umSpl[0].ToString().Replace(",","").Trim();
								}
							}



							dtItem.Rows.Add();
							dtItem.Rows[dtItem.Rows.Count - 1]["NO_PO_PARTNER"] = 계약번호;
							dtItem.Rows[dtItem.Rows.Count - 1]["CD_PARTNER"] = "MAN";
							dtItem.Rows[dtItem.Rows.Count - 1]["NO_SO"] = NO_SO;
							dtItem.Rows[dtItem.Rows.Count - 1]["CD_ITEM"] = 도면번호;
							dtItem.Rows[dtItem.Rows.Count - 1]["QT_SO"] = QT_SO;
							dtItem.Rows[dtItem.Rows.Count - 1]["CD_EXCH"] = 화폐;
							dtItem.Rows[dtItem.Rows.Count - 1]["DT_DUEDATE"] = 수정계약납기;
							dtItem.Rows[dtItem.Rows.Count - 1]["DT_SO"] = 계약일;
							dtItem.Rows[dtItem.Rows.Count - 1]["UM_SO"] = UM_SO;
							dtItem.Rows[dtItem.Rows.Count - 1]["CD_USERDEF1"] = CD_USERDEF1;


							계약번호 = string.Empty;
							NO_SO = string.Empty;
							도면번호 = string.Empty;
							QT_SO = string.Empty;
							화폐 = string.Empty;
							수정계약납기 = string.Empty;
							계약일 = string.Empty;
							UM_SO = string.Empty;
							CD_USERDEF1 = string.Empty;
						}
					}
				}
			}
		}
	}
}
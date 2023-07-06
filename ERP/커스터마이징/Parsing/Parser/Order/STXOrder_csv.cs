using Dintec;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parsing.Parser.Order
{
	class STXOrder_csv
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

		public STXOrder_csv(string fileName)
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
			dtItem.Columns.Add("LINE_NO");
			dtItem.Columns.Add("YN_FLAG");

			this.fileName = fileName;
		}

		#endregion


		public void Parse()
		{
			string 계약번호 = string.Empty;
			string NO_SO = string.Empty;
			string 도면번호 = string.Empty;
			string TXT_USERDEF6 = string.Empty;
			string QT_SO = string.Empty;
			string 화폐 = string.Empty;
			string 수정계약납기 = string.Empty;
			string 계약일 = string.Empty;
			string UM_SO = string.Empty;
			string CD_USERDEF1 = string.Empty;
			string LINE_NO = string.Empty;


			int _계약번호 = -1;
			int _NO_SO = -1;
			int _도면번호 = -1;
			int _TXT_USERDEF6 = -1;
			int _QT_SO = -1;
			int _화폐 = -1;
			int _수정계약납기 = -1;
			int _계약일 = -1;
			int _UM_SO = -1;
			int _CD_USERDEF1 = -1;
			int _LINE_NO = -1;

			// 엑셀 읽기
			DataSet ds = ExcelReader.ToDataSet(fileName);
			DataTable dt = ds.Tables[0];

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				string firstColStr = dt.Rows[i][0].ToString().Trim();

				if (firstColStr.StartsWith("PJT_NO"))
				{
					for (int c = 0; c < dt.Columns.Count; c++)
					{
						if (dt.Rows[i][c].ToString().Contains("발주번호")) _계약번호 = c;
						else if (dt.Rows[i][c].ToString().Contains("PJT_NO")) _NO_SO = c;
						//else if (dt.Rows[i][c].ToString().Equals("품번")) _도면번호 = c;
						else if (dt.Rows[i][c].ToString().Equals("품목코드")) _도면번호 = c;
						//						else if (dt.Rows[i][c].ToString().Contains("PJT_NM")) _TXT_USERDEF6 = c;
						else if (dt.Rows[i][c].ToString().Contains("프로젝트명")) _TXT_USERDEF6 = c;
						//else if (dt.Rows[i][c].ToString().Equals("발주수량")) _QT_SO = c;
						else if (dt.Rows[i][c].ToString().Equals("발주량")) _QT_SO = c;
						//else if (dt.Rows[i][c].ToString().Contains("통화")) _화폐 = c;
						else if (dt.Rows[i][c].ToString().Contains("환종")) _화폐 = c;
						else if (dt.Rows[i][c].ToString().Contains("납기일자")) _수정계약납기 = c;
						else if (dt.Rows[i][c].ToString().Contains("발주일자")) _계약일 = c;
						else if (dt.Rows[i][c].ToString().Contains("단가")) _UM_SO = c;
						else if (dt.Rows[i][c].ToString().Contains("선급")) _CD_USERDEF1 = c;
						else if (dt.Rows[i][c].ToString().Contains("LINENO")) _LINE_NO = c;
					}
				}


				if (i != 0)
				{

					if (_계약번호 != -1)
						계약번호 = dt.Rows[i][_계약번호].ToString().Trim();

					if (_NO_SO != -1)
						NO_SO = dt.Rows[i][_NO_SO].ToString().Trim();

					if (_도면번호 != -1)
						도면번호 = dt.Rows[i][_도면번호].ToString().Trim();

					if (_TXT_USERDEF6 != -1)
						TXT_USERDEF6 = dt.Rows[i][_TXT_USERDEF6].ToString().Trim();

					if (_QT_SO != -1)
						QT_SO = dt.Rows[i][_QT_SO].ToString().Trim();

					if (_화폐 != -1)
						화폐 = dt.Rows[i][_화폐].ToString().Replace("Each","").Trim();

					if (_LINE_NO != -1)
						LINE_NO = dt.Rows[i][_LINE_NO].ToString().Trim();

					if (_수정계약납기 != -1)
					{
						string 수정계약납기Str = dt.Rows[i][_수정계약납기].ToString().Trim();

						DateTime dt1 = DateTime.FromOADate(Convert.ToInt32(수정계약납기Str));

						수정계약납기 = dt1.ToString("yyyyMMdd");
					}

					if (_계약일 != -1)
					{
						string 계약일Str = dt.Rows[i][_계약일].ToString().Trim();

						DateTime dt1 = DateTime.FromOADate(Convert.ToInt32(계약일Str));

						계약일 = dt1.ToString("yyyyMMdd");
					}

					if (_UM_SO != -1)
						UM_SO = dt.Rows[i][_UM_SO].ToString().Trim();

					if (_CD_USERDEF1 != -1)
						CD_USERDEF1 = dt.Rows[i][_CD_USERDEF1].ToString().Trim();


					dtItem.Rows.Add();
					dtItem.Rows[dtItem.Rows.Count - 1]["NO_PO_PARTNER"] = 계약번호;
					dtItem.Rows[dtItem.Rows.Count - 1]["CD_PARTNER"] = "STX";
					dtItem.Rows[dtItem.Rows.Count - 1]["NO_SO"] = NO_SO;
					dtItem.Rows[dtItem.Rows.Count - 1]["CD_ITEM"] = 도면번호;
					dtItem.Rows[dtItem.Rows.Count - 1]["TXT_USERDEF6"] = TXT_USERDEF6;
					dtItem.Rows[dtItem.Rows.Count - 1]["QT_SO"] = QT_SO;
					dtItem.Rows[dtItem.Rows.Count - 1]["CD_EXCH"] = 화폐;
					dtItem.Rows[dtItem.Rows.Count - 1]["DT_DUEDATE"] = 수정계약납기;
					dtItem.Rows[dtItem.Rows.Count - 1]["DT_SO"] = 계약일;
					dtItem.Rows[dtItem.Rows.Count - 1]["UM_SO"] = UM_SO;
					dtItem.Rows[dtItem.Rows.Count - 1]["CD_USERDEF1"] = CD_USERDEF1;
					dtItem.Rows[dtItem.Rows.Count - 1]["LINE_NO"] = LINE_NO;
					dtItem.Rows[dtItem.Rows.Count - 1]["YN_FLAG"] = "N";



					계약번호 = string.Empty;
					NO_SO = string.Empty;
					도면번호 = string.Empty;
					TXT_USERDEF6 = string.Empty;
					QT_SO = string.Empty;
					화폐 = string.Empty;
					수정계약납기 = string.Empty;
					계약일 = string.Empty;
					UM_SO = string.Empty;
					CD_USERDEF1 = string.Empty;
					LINE_NO = string.Empty;
				}
			}
		}
	}
}

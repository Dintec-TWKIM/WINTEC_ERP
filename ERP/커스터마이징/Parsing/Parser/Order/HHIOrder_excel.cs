using Dintec;
using Dintec.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parsing.Parser.Order
{
	class HHIOrder_excel
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

		public HHIOrder_excel(string fileName)
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
			dtItem.Columns.Add("DT_DUEDATE2");        // 수정소요납기(최종)
			dtItem.Columns.Add("DT_DUEDATE3");        // 소요납기(최초)
			dtItem.Columns.Add("DT_SO");                     // 계약일
			dtItem.Columns.Add("UM_SO");
			dtItem.Columns.Add("PAYMENT");                  // 지불
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
			string 구매회사 = string.Empty;
			string NO_SO = string.Empty;
			string TXT_USERDEF3 = string.Empty;
			string 도면번호 = string.Empty;
			string TXT_USERDEF6 = string.Empty;
			string QT_SO = string.Empty;
			string 화폐 = string.Empty;
			string 수정계약납기 = string.Empty;
			string 수정소요납기 = string.Empty;
			string 소요납기 = string.Empty;
			string 계약일 = string.Empty;
			string UM_SO = string.Empty;
			string CD_USERDEF3 = string.Empty;
			string CD_USERDEF1 = string.Empty;
			string CD_USERDEF2 = string.Empty;
			string TXT_USERDEF4 = string.Empty;
			string 지불 = string.Empty;


			int _계약번호 = -1;
			int _구매회사 = -1;
			int _NO_SO = 	-1;
			int _TXT_USERDEF3 = 	-1;
			int _도면번호 = 	-1;
			int _TXT_USERDEF6 = 	-1;
			int _QT_SO = -1;
			int _화폐 = -1;
			int _수정계약납기 = -1;
			int _수정소요납기 = -1;
			int _소요납기 = -1;
			int _계약일 = -1;
			int _UM_SO = -1;
			int _CD_USERDEF3 = -1;
			int _CD_USERDEF1 = -1;
			int _CD_USERDEF2 = -1;
			int _TXT_USERDEF4 = -1;
			int _지불 = -1;

			// 엑셀 읽기
			DataSet ds = ExcelReader.ToDataSet(fileName);
			DataTable dt = ds.Tables[0];

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				string firstColStr = dt.Rows[i][0].ToString().Trim();

				if(firstColStr.StartsWith("계약번호"))
				{
					for(int c = 0; c < dt.Columns.Count; c++)
					{
						if (dt.Rows[i][c].ToString().Contains("계약번호")) _계약번호 = c;
						else if (dt.Rows[i][c].ToString().Contains("구매회사")) _구매회사 = c;
						else if (dt.Rows[i][c].ToString().Contains("공사번호")) _NO_SO = c;
						else if (dt.Rows[i][c].ToString().Contains("자재번호")) _TXT_USERDEF3 = c;
						else if (dt.Rows[i][c].ToString().Contains("도면번호")) _도면번호 = c;
						else if (dt.Rows[i][c].ToString().Contains("호선번호")) _TXT_USERDEF6 = c;
						else if (dt.Rows[i][c].ToString().Equals("수량")) _QT_SO = c;
						else if (dt.Rows[i][c].ToString().Contains("화폐")) _화폐 = c;
						else if (dt.Rows[i][c].ToString().Contains("수정계약납기")) _수정계약납기 = c;
						else if (dt.Rows[i][c].ToString().Contains("수정소요납기")) _수정소요납기 = c;
						else if (dt.Rows[i][c].ToString().Contains("소요납기")) _소요납기 = c;
						else if (dt.Rows[i][c].ToString().Contains("계약일")) _계약일 = c;
						else if (dt.Rows[i][c].ToString().Contains("단가")) _UM_SO = c;
						else if (dt.Rows[i][c].ToString().Contains("엔진타입")) _CD_USERDEF3 = c;
						else if (dt.Rows[i][c].ToString().Contains("선급검사기관1")) _CD_USERDEF1 = c;
						else if (dt.Rows[i][c].ToString().Contains("선급검사기관2")) _CD_USERDEF2 = c;
						else if (dt.Rows[i][c].ToString().Contains("도장COLOR")) _TXT_USERDEF4 = c;
						else if (dt.Rows[i][c].ToString().Contains("지불")) _지불 = c;
					}
				}


				if (i != 0)
				{

					if (_계약번호 != -1)
						계약번호 = dt.Rows[i][_계약번호].ToString().Trim();

					if (_구매회사 != -1)
						구매회사 = dt.Rows[i][_구매회사].ToString().Trim();

					if (_NO_SO != -1)
						NO_SO = dt.Rows[i][_NO_SO].ToString().Trim() + "-" + dt.Rows[i][_NO_SO + 1].ToString().Trim() +"-" + dt.Rows[i][_NO_SO + 2].ToString().Trim();

					if (_TXT_USERDEF3 != -1)
						TXT_USERDEF3 = dt.Rows[i][_TXT_USERDEF3].ToString().Trim();

					if (_도면번호 != -1)
						도면번호 = dt.Rows[i][_도면번호].ToString().Trim();

					if (_TXT_USERDEF6 != -1)
						TXT_USERDEF6 = dt.Rows[i][_TXT_USERDEF6].ToString().Trim();

					if (_QT_SO != -1)
						QT_SO = dt.Rows[i][_QT_SO].ToString().Trim();

					if (_화폐 != -1)
						화폐 = dt.Rows[i][_화폐].ToString().Trim();

					if (_수정계약납기 != -1)
					{
						string 수정계약납기Str = dt.Rows[i][_수정계약납기].ToString().Trim();

						DateTime dt1 = DateTime.FromOADate(Convert.ToInt32(수정계약납기Str));

						수정계약납기 = dt1.ToString("yyyyMMdd");
					}

					if (_수정소요납기 != -1)
					{
						string 수정소요납기Str = dt.Rows[i][_수정소요납기].ToString().Trim();

						DateTime dt1 = DateTime.FromOADate(Convert.ToInt32(수정소요납기Str));

						수정소요납기 = dt1.ToString("yyyyMMdd");
					}

					if (_소요납기 != -1)
					{
						string 소요납기Str = dt.Rows[i][_소요납기].ToString().Trim();

						DateTime dt1 = DateTime.FromOADate(Convert.ToInt32(소요납기Str));

						소요납기 = dt1.ToString("yyyyMMdd");
					}



					if (_계약일 != -1)
					{
						string 계약일Str = dt.Rows[i][_계약일].ToString().Trim();

						DateTime dt1 = DateTime.FromOADate(Convert.ToInt32(계약일Str));

						계약일 = dt1.ToString("yyyyMMdd");
					}

					if (_UM_SO != -1)
						UM_SO = dt.Rows[i][_UM_SO].ToString().Trim();

					if (_CD_USERDEF3 != -1)
						CD_USERDEF3 = dt.Rows[i][_CD_USERDEF3].ToString().Trim();

					if (_CD_USERDEF1 != -1)
						CD_USERDEF1 = dt.Rows[i][_CD_USERDEF1].ToString().Trim();

					if (_CD_USERDEF2 != -1)
						CD_USERDEF2 = dt.Rows[i][_CD_USERDEF2].ToString().Trim();

					if (_TXT_USERDEF4 != -1)
						TXT_USERDEF4 = dt.Rows[i][_TXT_USERDEF4].ToString().Trim();

					if (_지불 != -1)
						지불 = dt.Rows[i][_지불].ToString().Trim();

					dtItem.Rows.Add();
					dtItem.Rows[dtItem.Rows.Count - 1]["NO_PO_PARTNER"] = 계약번호;
					dtItem.Rows[dtItem.Rows.Count - 1]["CD_PARTNER"] = 구매회사;
					dtItem.Rows[dtItem.Rows.Count - 1]["NO_SO"] = NO_SO;
					dtItem.Rows[dtItem.Rows.Count - 1]["TXT_USERDEF3"] = TXT_USERDEF3;
					dtItem.Rows[dtItem.Rows.Count - 1]["CD_ITEM"] = 도면번호;
					dtItem.Rows[dtItem.Rows.Count - 1]["TXT_USERDEF6"] = TXT_USERDEF6;
					dtItem.Rows[dtItem.Rows.Count - 1]["QT_SO"] = QT_SO;
					dtItem.Rows[dtItem.Rows.Count - 1]["CD_EXCH"] = 화폐;
					dtItem.Rows[dtItem.Rows.Count - 1]["DT_DUEDATE"] = 수정계약납기;
					dtItem.Rows[dtItem.Rows.Count - 1]["DT_DUEDATE2"] = 수정소요납기;
					dtItem.Rows[dtItem.Rows.Count - 1]["DT_DUEDATE3"] = 소요납기;
					dtItem.Rows[dtItem.Rows.Count - 1]["DT_SO"] = 계약일;
					dtItem.Rows[dtItem.Rows.Count - 1]["UM_SO"] = UM_SO;
					dtItem.Rows[dtItem.Rows.Count - 1]["CD_USERDEF3"] = CD_USERDEF3;
					dtItem.Rows[dtItem.Rows.Count - 1]["CD_USERDEF1"] = CD_USERDEF1;
					dtItem.Rows[dtItem.Rows.Count - 1]["CD_USERDEF2"] = CD_USERDEF2;
					dtItem.Rows[dtItem.Rows.Count - 1]["TXT_USERDEF4"] = TXT_USERDEF4;
					dtItem.Rows[dtItem.Rows.Count - 1]["PAYMENT"] = 지불;


					계약번호 = string.Empty;
					구매회사 = string.Empty;
					NO_SO = string.Empty;
					TXT_USERDEF3 = string.Empty;
					도면번호 = string.Empty;
					TXT_USERDEF6 = string.Empty;
					QT_SO = string.Empty;
					화폐 = string.Empty;
					수정계약납기 = string.Empty;
					수정소요납기 = string.Empty;
					소요납기 = string.Empty;
					계약일 = string.Empty;
					UM_SO = string.Empty;
					CD_USERDEF3 = string.Empty;
					CD_USERDEF1 = string.Empty;
					CD_USERDEF2 = string.Empty;
					TXT_USERDEF4 = string.Empty;
					지불 = string.Empty;
				}
			}
		}
	}
}


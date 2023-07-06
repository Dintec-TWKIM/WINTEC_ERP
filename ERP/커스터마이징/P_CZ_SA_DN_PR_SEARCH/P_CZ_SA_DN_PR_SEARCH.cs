using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using DzHelpFormLib;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using System.Net;
using System.Reflection;
using Duzon.Common.Controls;

namespace cz
{
	public partial class P_CZ_SA_DN_PR_SEARCH : PageBase
	{
		#region ==================================================================================================== 속성

		public string CD_COMPANY { get; set; }

		#endregion

		#region ==================================================================================================== 생성자

		public P_CZ_SA_DN_PR_SEARCH()
		{
			Util.Certify(this);
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
			InitializeComponent();
		}

		#endregion

		#region ==================================================================================================== 초기화

		protected override void InitLoad()
		{
			InitControl();
			InitGrid();
			InitEvent();
		}

		private void InitControl()
		{

		}

		private void InitGrid()
		{
			flex.BeginSetting(1, 1, false);

			flex.SetCol("NO_IO"	, "출하번호", 120, true);
			flex.SetCol("NO_SO"	, "파일번호", 120, false);
			
			flex.KeyActionEnter = KeyActionEnum.MoveDown;
			flex.SettingVersion = "16.02.04.02";
			flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{			
			flex.KeyDown += new KeyEventHandler(flex_KeyDown);
			flex.ValidateEdit += new ValidateEditEventHandler(flex_ValidateEdit);
		}

		protected override void InitPaint()
		{
			flex.Rows.Add();
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void flex_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == (Keys.Control | Keys.V))
			{
				string[,] clipboard = Util.GetClipboardValues();

				for (int i = 0; i < clipboard.GetLength(0); i++)
				{
					int row = flex.Row + i;
					string val = clipboard[i, 0];

					if (i == clipboard.GetLength(0) - 1 && val == "") break;
					if (row == flex.Rows.Count) flex.Rows.Add();

					flex[row, flex.Col] = val;

					string query = @"
SELECT
	TOP 1 *
FROM
(
	SELECT
		  CD_COMPANY
		, NO_IO
		, NO_PSO_MGMT	AS NO_SO
		, SUM(AM)		AS AM_KR
	FROM MM_QTIO
	WHERE 1 = 1
		AND CD_COMPANY = '" + CD_COMPANY + @"'
		AND NO_IO = '" + val + @"'
	GROUP BY CD_COMPANY, NO_IO, NO_PSO_MGMT
) AS A
ORDER BY AM_KR DESC";

					DataTable dt = DBMgr.GetDataTable(query);

					if (dt.Rows.Count == 1)
					{
						flex[row, "NO_SO"] = dt.Rows[0]["NO_SO"];
					}
				}
			}
		}

		private void flex_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			string COLNAME = flex.Cols[e.Col].Name;

			if (COLNAME == "CD_SPEC")
			{

			}
		}

		#endregion
	}
}

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using System.Drawing;

namespace Dintec
{
	public class FlexUtil
	{
		#region ========== 스타일 관련

		public static void AddEditStyle(FlexGrid flex, string columnName)
		{
			// 헤더
			if (flex.Styles.Contains("EDIT_HEADER") == false)
			{
				CellStyle style = flex.Styles.Add("EDIT_HEADER");
				style.Font = new Font(flex.Font, FontStyle.Bold);
				style.ForeColor = Color.Blue;
			}

			flex.SetCellStyle(0, flex.Cols[columnName].Index, "EDIT_HEADER");

			// 컬럼
			//if (flex.Styles.Contains("EDIT_COLUMN") == false)
			//{
			//    CellStyle style = flex.Styles.Add("EDIT_COLUMN");
			//    style.BackColor = Color.FromArgb(255, 237, 242);
			//}

			//flex.Cols[columnName].Style = flex.Styles["EDIT_COLUMN"];
		}

		#endregion

	}
}

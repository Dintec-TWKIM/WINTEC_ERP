using System.Drawing;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;

namespace Dintec
{
	public class SetGrid
	{
		public static void Selected(FlexGrid flex)
		{
			if (!flex.Styles.Contains("SELECTED"))
			{
				CellStyle style = flex.Styles.Add("SELECTED");
				style.Font = new Font(flex.Font, FontStyle.Bold);
				style.BackColor = Color.FromArgb(211, 237, 185);				
			}
		}

		public static void Edit(FlexGrid flex, bool editable, params string[] colNames)
		{
			if (!flex.Styles.Contains("EDIT_HEADER"))
			{
				CellStyle style = flex.Styles.Add("EDIT_HEADER");
				style.Font = new Font(flex.Font, FontStyle.Bold);
				style.ForeColor = Color.Blue;
			}

			foreach (string s in colNames)
			{
				if (!flex.Cols.Contains(s)) continue;

				flex.Cols[s].AllowEditing = editable;

				int headerRow = flex.Rows.Fixed - 1;
				if (flex.SumPosition == SumPositionEnum.Top) headerRow = headerRow - 1;

				flex.SetCellStyle(headerRow, flex.Cols[s].Index, editable ? "EDIT_HEADER" : "");
			}
		}

		public static void CellRed(FlexGrid flex, int row, int col)
		{
			if (!flex.Styles.Contains("RED"))
			{
				CellStyle style = flex.Styles.Add("RED");
				style.ForeColor = Color.Red;
			}

			flex.SetCellStyle(row, col, "RED");			
		}


		public static void CellBlue(FlexGrid flex, int row, int col)
		{
			if (!flex.Styles.Contains("BLUE"))
			{
				CellStyle style = flex.Styles.Add("BLUE");
				style.ForeColor = Color.Blue;
			}

			flex.SetCellStyle(row, col, "RED");
		}


		public static void CellGreen(FlexGrid flex, int row, int col)
		{
			if (!flex.Styles.Contains("GREEN"))
			{
				CellStyle style = flex.Styles.Add("GREEN");
				style.ForeColor = Color.Green;
			}

			flex.SetCellStyle(row, col, "RED");
		}

		public static void CellRed(FlexGrid flex, int row, int col, string styleName)
		{
			flex.SetCellStyle(row, col, styleName);
		}


		

		public static void ColumnStyle(FlexGrid flex, int col, params GridStyle[] style)
		{
			foreach (GridStyle gs in style)
			{
				if (gs == GridStyle.Bold) flex.Cols[col].Style.Font = new Font(flex.Font, FontStyle.Bold);
				if (gs == GridStyle.FontBlue)	flex.Cols[col].Style.ForeColor = Color.Blue;
				if (gs == GridStyle.FontGreen)	flex.Cols[col].Style.ForeColor = Color.Green;
				if (gs == GridStyle.FontRed)	flex.Cols[col].Style.ForeColor = Color.Red;
				if (gs == GridStyle.FontYellow) flex.Cols[col].Style.ForeColor = Color.Yellow;
				if (gs == GridStyle.BackBlue)	flex.Cols[col].Style.BackColor = Color.Blue;
				if (gs == GridStyle.BackGreen)	flex.Cols[col].Style.BackColor = Color.Green;
				if (gs == GridStyle.BackRed)	flex.Cols[col].Style.BackColor = Color.Red;				
				if (gs == GridStyle.BackYellow)	flex.Cols[col].Style.BackColor = Color.Yellow;
			}
		}

		public static void RowStyle(FlexGrid grid, int row, params GridStyle[] style)
		{
			

			foreach (GridStyle s in style)
			{
				if (s == GridStyle.Bold)	   grid.Rows[row].Style.Font = new Font(grid.Font, FontStyle.Bold);
				if (s == GridStyle.FontBlue)   grid.Rows[row].Style.ForeColor = Color.Blue;
				if (s == GridStyle.FontGray)   grid.Rows[row].Style.ForeColor = Color.Gray;
				if (s == GridStyle.FontGreen)  grid.Rows[row].Style.ForeColor = Color.Green;				
				if (s == GridStyle.FontRed)	   grid.Rows[row].Style.ForeColor = Color.Red;
				if (s == GridStyle.FontYellow) grid.Rows[row].Style.ForeColor = Color.Yellow;
				if (s == GridStyle.BackBlue)   grid.Rows[row].Style.BackColor = Color.Blue;
				if (s == GridStyle.BackGray)   grid.Rows[row].Style.BackColor = Color.Gray;
				if (s == GridStyle.BackGreen)  grid.Rows[row].Style.BackColor = Color.Green;
				if (s == GridStyle.BackRed)	   grid.Rows[row].Style.BackColor = Color.Red;
				if (s == GridStyle.BackYellow) grid.Rows[row].Style.BackColor = Color.Yellow;
			}
			
		}
	}

	public enum GridStyle
	{
		  Bold

		, FontBlue
		, FontGray
		, FontGreen
		, FontLightGray
		, FontRed
		, FontYellow

		, BackBlue
		, BackGray
		, BackGreen
		, BackLightGray
		, BackRed
		, BackYellow
	}

	public static class Exten
	{
		// TEST TEST TEST TEST TEST TEST TEST TEST TEST
		
		// 헤더
			//if (flex.Styles.Contains("EDIT_HEADER") == false)
			//{
			//    CellStyle style = flex.Styles.Add("EDIT_HEADER");
			//    style.Font = new Font(flex.Font, FontStyle.Bold);
			//    style.ForeColor = Color.Blue;
			//}

			//flex.SetCellStyle(0, flex.Cols[columnName].Index, "EDIT_HEADER");
	}
}


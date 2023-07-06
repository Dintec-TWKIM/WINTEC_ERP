using System.Windows.Forms;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Dintec;
using C1.Win.C1FlexGrid;
using System.Collections.Generic;
using Duzon.Common.Forms;
using System;
using System.Data;
using Duzon.Common.Forms.Help;

namespace DX
{
	public static class EXT_CON
	{

		public static void Init(this BpCodeTextBox o)
		{
			o.CodeValue = "";
			o.CodeName = "";
		}

		public static void Init(this BpCodeTextBox o, bool setDefault)
		{
			string tag = o.Tag.ToString();
			o.Init();

			if (setDefault)
			{
				if (tag == "NO_EMP;NM_EMP")
				{
					o.CodeValue = Global.MainFrame.LoginInfo.UserID;
					o.CodeName = Global.MainFrame.LoginInfo.UserName;
				}
				else if (tag == "CD_SALEGRP;NM_SALEGRP")
				{
					o.CodeValue = Global.MainFrame.LoginInfo.SalesGroupCode;
					o.CodeName = Global.MainFrame.LoginInfo.SalesGroupName;
				}
				else if (tag == "CD_PURGRP;NM_PURGRP")
				{
					o.CodeValue = Global.MainFrame.LoginInfo.PurchaseGroupCode;
					o.CodeName = Global.MainFrame.LoginInfo.PurchaseGroupName;
				}				
			}
		}





		public static void Edit(this BpPanelControl o, bool enable)
		{
			foreach (Control c in o.Controls)
			{
				if (c is TextBoxExt tbx)			tbx.ReadOnly = !enable;
				else if (c is BpCodeTextBox ctx)	ctx.ReadOnly = !enable ? ReadOnly.TotalReadOnly : ReadOnly.None;
				else if (c is CurrencyTextBox cur)	cur.ReadOnly = !enable;
				else c.Enabled = enable;
			}
		}




















		public static bool CheckValue(this DropDownComboBox o, string errorMsg)
		{
			if (o.GetValue() == "")
			{
				if (errorMsg != "")
					Global.MainFrame.ShowMessage(errorMsg);

				return false;
			}

			return true;
		}














		public static void SetValue(this BpCodeTextBox o, string value)
		{			
			if (value.IndexOf(",") > 0)
			{
				o.CodeValue = value.Split(new[] { ',' }, 2)[0];
				o.CodeName = value.Split(new[] { ',' }, 2)[1];
			}
			else
			{
				o.CodeValue = value;				
			}

			if (o.CodeValue == "")
				o.CodeName = "";
		}















		public static CheckBoxExt GetCheckedControl(this CheckBoxExt o)
		{
			foreach (Control con in ((BpPanelControl)o.Parent).Controls)
			{
				if (con is CheckBoxExt chk && chk.Checked)
					return chk;
			}

			return null;
		}

		public static RadioButtonExt GetCheckedControl(this RadioButtonExt o)
		{
			foreach (Control con in ((BpPanelControl)o.Parent).Controls)
			{
				if (con is RadioButtonExt rdo && rdo.Checked)
					return rdo;
			}

			return null;
		}

		public static string GetTag(this object obj)
		{
			string tag = "";

			if (obj is BpCodeTextBox con) tag = con.Tag.ToString();
			else if (obj is BpComboBox cbo) tag = cbo.Tag.ToString();
			else if (obj is CurrencyTextBox) tag = ((CurrencyTextBox)obj).Tag.ToString();
			else if (obj is DatePicker) tag = ((DatePicker)obj).Tag.ToString();
			else if (obj is DropDownComboBox) tag = ((DropDownComboBox)obj).Tag.ToString();
			else if (obj is RadioButtonExt) tag = ((RadioButtonExt)obj).Tag.ToString();
			else if (obj is TextBoxExt) tag = ((TextBoxExt)obj).Tag.ToString();
			else if (obj is CheckBoxExt) tag = ((CheckBoxExt)obj).Tag.ToString();

			else if (obj is UDatePicker) tag = ((UDatePicker)obj).Tag.ToString();

			else if (obj is FlexGrid grd) tag = grd.Tag.ToString2();

			if (tag.IndexOf(";") > 0)
				return tag.Split(';')[0];
			else
				return tag;
		}






		public static void SetDataMap(this FlexGrid o, string colName, DataTable dataTable)
		{
			o.SetDataMap(colName, dataTable, "CODE", "NAME");
		}

		public static void SetDataMap(this FlexGrid o, string colName, DataRow[] dataRows)
		{
			o.SetDataMap(colName, dataRows.ToDataTable(), "CODE", "NAME");
		}

		public static void SetCol(this FlexGrid o, string colName, string colCaptionDD, int colWidth, CheckTypeEnum checkType)
		{
			o.SetCol(colName, colCaptionDD, colWidth, false, checkType);
		}

		public static void SetCol(this FlexGrid o, string colName, string colCaptionDD, int colWidth, ImageAlignEnum alignEnum)
		{
			o.SetCol(colName, colCaptionDD, colWidth);
			o.Cols[colName].ImageAlign = alignEnum;
		}

		public static void SetExceptSumCol(this FlexGrid o, bool autoExcept, params string[] colNames)
		{
			List<string> cols = new List<string>(colNames);

			if (autoExcept)
			{
				for (int i = 0; i < o.Cols.Count; i++)
				{
					if (new List<string>(new string[] { "NO_DSP", "LT" }).Contains(o.Cols[i].Name)
						|| o.Cols[i].Name.StartsWith("UM_")
						|| o.Cols[i].Name.StartsWith("RT_")
						|| o.Cols[i].Name.StartsWith("LT_"))
					{
						cols.Add(o.Cols[i].Name);
					}
				}
			}

			o.SetExceptSumCol(cols.ToArray());
		}





		public static void SetCopyAndPaste(this FlexGrid o, Action<FlexGrid, int, int> action, params string[] incrementColNames)
		{
			o.KeyDown += delegate (object sender, KeyEventArgs e) { FlexGrid_KeyDown(sender, e, action, incrementColNames); };
		}

		private static void FlexGrid_KeyDown(object sender, KeyEventArgs e, Action<FlexGrid, int, int> action, params string[] incrementColNames)
		{
			FlexGrid grid = (FlexGrid)sender;

			if (e.KeyData == (Keys.Control | Keys.V))
			{
				grid.Redraw = false;

				// 시작
				string[,] clipboard = Util.GetClipboardValues();
				int startRow = grid.Row;
				int startCol = grid.Col;

				for (int i = 0; i < clipboard.GetLength(0); i++)
				{
					int row = startRow + i;

					// 마지막행을 넘어가면 행 추가
					if (row > grid.Rows.Count - 1)
						grid.Rows.Add();

					// 셀 복사
					for (int j = 0, col = grid.Col; j < clipboard.GetLength(1); j++, col++)
					{
						// 보이기 컬럼일때까지 스캔	
						for (; col < grid.Cols.Count; col++)
						{
							if (grid.Cols[col].Visible)
								break;
						}

						//  에딧 가능 컬럼이 아닐 경우 스킵
						if (!grid.Cols[col].AllowEditing)
							continue;

						// 마지막 컬럼을 넘어가면 스톱
						if (col > grid.Cols.Count)
							break;

						grid[row, col] = clipboard[i, j] == "" ? (object)DBNull.Value : clipboard[i, j];

						// 사용자 지정 함수
						action?.Invoke(grid, row, col);
					}
				}

				grid.Redraw = true;
			}
		}
	}
}

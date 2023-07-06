using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Aspose.Email.Exchange.Schema;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;

namespace Dintec
{
	public static class EXT
	{
		public static int IndexOf<T>(this T[] array, T item)
		{
			return Array.IndexOf(array, item);
		}

		public static void AddStyle(this WebBrowser o, string value)
		{
			string html = o.DocumentText;

			if (html == "")
			{
				html = @"
<html>
	<head>
		<style type='text/css'>
			html, body, div, span, table, thead, tbody, tfoot, tr, th, td, img { margin:0; padding:0; border:0; outline:0; line-height:1; font-family:맑은 고딕; font-size:10pt; }			
			table { border-collapse:collapse; border-spacing:0; }
		</style>
	</head>
	<body>
	</body>
</html>";
			}

			int index = html.IndexOf("</style>");
			html = html.Insert(index, value + "\r\n");

			/////////
			o.Navigate("about:blank");
			o.Document.OpenNew(false);
			o.Document.Write(html);
			o.Refresh();
		}

		public static void AddBody(this WebBrowser o, string value)
		{
			string html = o.DocumentText;

			if (html == "")
			{
				html = @"
<html>
	<head>
		<style type='text/css'>
			html, body, div, span, table, thead, tbody, tfoot, tr, th, td, img { margin:0; padding:0; border:0; outline:0; line-height:1; font-family:맑은 고딕; font-size:10pt; }			
			table { border-collapse:collapse; border-spacing:0; }
		</style>
	</head>
	<body>
	</body>
</html>";
			}

			int index = html.IndexOf("</body>");
			html = html.Insert(index, value + "\r\n");

			/////////
			o.Navigate("about:blank");
			o.Document.OpenNew(false);
			o.Document.Write(html);
			o.Refresh();
		}

		public static void SetHtml(this WebBrowser o, string value)
		{
			// 기존거 무시하고 새로 작성
			string html = @"
<!DOCTYPE html>

<html>
	<head>
		<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>
		<style type='text/css'>
			html, body, div, span, table, thead, tbody, tfoot, tr, th, td, img { margin:0; padding:0; border:0; outline:0; line-height:1; font-family:맑은 고딕; font-size:10pt; }			
			table { border-collapse:collapse; border-spacing:0; table-layout:fixed; }

			.dx-viewbox		{ border-top:2px solid #2a56a4; }
			.dx-viewbox tr > * { border-left:1px solid #dedede; border-right:1px solid #dedede; border-bottom:1px solid #dedede; }
			.dx-viewbox th  { font-weight:normal; background-color:#f0f0f0; }
			.dx-viewbox td  { line-height:15px; padding:0px 9px 0px 5px; text-overflow:ellipsis; white-space:nowrap; overflow:hidden; }
		</style>
	</head>
	<body>
	</body>
</html>";			

			int index = html.IndexOf("</body>");
			html = html.Insert(index, value + "\r\n");

			/////////
			o.Navigate("about:blank");
			o.Document.OpenNew(false);
			o.Document.Write(html);
			o.Refresh();
		}





		// ********** Clear2
		public static void Clear2(this FlexGrid o)
		{
			if (o.DataSource != null)
			{
				o.DataTable.Rows.Clear();
				o.AcceptChanges();
			}
		}

		// ********** SetDefault
		public static void SetDefault(this DropDownComboBox o)
		{
			o.SelectedIndex = 0;
		}

		public static void SetDefault(this DatePicker o)
		{
			o.Text = UT.Today();
		}

		public static void SetDefault(this BpCodeTextBox o)
		{
			string tag = o.Tag.ToString();

			if (tag == "NO_EMP;NM_EMP")         // 담당자
			{
				o.CodeValue = Global.MainFrame.LoginInfo.UserID;
				o.CodeName = Global.MainFrame.LoginInfo.UserName;
			}
			if (tag == "CD_SALEGRP;NM_SALEGRP") // 영업그룹
			{
				o.CodeValue = Global.MainFrame.LoginInfo.SalesGroupCode;
				o.CodeName = Global.MainFrame.LoginInfo.SalesGroupName;
			}
			if (tag == "CD_PURGRP;NM_PURGRP")   // 구매그룹
			{
				o.CodeValue = Global.MainFrame.LoginInfo.PurchaseGroupCode;
				o.CodeName = Global.MainFrame.LoginInfo.PurchaseGroupName;
			}
		}



		// ********** SetEdit
		public static void SetEdit(this TextBoxExt o, bool enable)
		{
			o.ReadOnly = !enable;
		}

		public static void SetEdit(this BpCodeTextBox o, bool enable)
		{
			o.ReadOnly = !enable ? ReadOnly.TotalReadOnly : ReadOnly.None;
		}

		public static void SetEdit(this BpPanelControl o, bool enable)
		{
			foreach (Control c in o.Controls)
			{
				if (c is TextBoxExt tbx) tbx.ReadOnly = !enable;
				else if (c is BpCodeTextBox ctx) ctx.ReadOnly = !enable ? ReadOnly.TotalReadOnly : ReadOnly.None;
				else if (c is CurrencyTextBox cur) cur.ReadOnly = !enable;
				else c.Enabled = enable;
			}
		}

		public static void SetEdit(this DropDownComboBox o, bool bo)
		{
			o.Enabled = bo;
		}


		public static string CheckDup(this DataTable dataTable, string colName)
		{
			var query = dataTable.AsEnumerable().Where(r => r.RowState != DataRowState.Deleted && !string.IsNullOrEmpty(r.Field<string>(colName))).GroupBy(r => r.Field<string>(colName)).Where(r => r.Count() > 1);

			if (query.Count() > 0)
				return query.First().Key;
			else
				return "";
		}

		public static string Concatenate(this DataTable dataTable, string separator, string colName)
		{
			return string.Join(separator, dataTable.AsEnumerable().Select(r => r.Field<string>(colName)));
		}

		public static T[] ToArray<T>(this DataTable dataTable, string colName)
		{
			return dataTable.AsEnumerable().Select(r => r.Field<T>(colName)).ToArray();
		}




		//public static void SetDefault(Control control)
		//{
		//	foreach (var o in GetAll(control, typeof(TextBoxExt)))
		//	{
		//		TextBoxExt textBox = (TextBoxExt)o;

		//		if (textBox.Name == "tbx포커스")
		//		{
		//			textBox.Left = -1000;
		//			continue;
		//		}

		//		if (!textBox.Multiline)
		//		{
		//			textBox.AutoSize = false;
		//			textBox.Height = 20;
		//		}

		//		textBox.Enter += TextBox_GotFocus;
		//		textBox.Leave += TextBox_LostFocus;
		//	}
		//}

		//public static IEnumerable<Control> GetAll(Control control, Type type)
		//{
		//	var controls = control.Controls.Cast<Control>();

		//	return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls).Where(c => c.GetType() == type);
		//}

		//private static void TextBox_GotFocus(object sender, EventArgs e)
		//{
		//	TextBoxExt textBox = (TextBoxExt)sender;

		//	if (textBox.Tag == null || textBox.Tag.ToString() == "")
		//		textBox.Tag = "BorderColor:" + textBox.BorderColor.ToArgb();

		//	textBox.BorderColor = Constant.FocusedBorderColor;
		//}

		//private static void TextBox_LostFocus(object sender, EventArgs e)
		//{
		//	TextBoxExt textBox = (TextBoxExt)sender;

		//	if (textBox.Tag.ToString().IndexOf("BorderColor:") == 0)
		//	{
		//		textBox.BorderColor = Color.FromArgb(GetTo.Int(textBox.Tag.ToString().Replace("BorderColor:", "")));
		//		textBox.Tag = "";
		//	}
		//	else
		//		textBox.BorderColor = Constant.UnfocusedBorderColor;			
		//}


		public static void SetCol(this FlexGrid o, string colName, string colCaptionDD1, string colCaptionDD2, int colWidth)
		{
			o.SetCol(colName, colCaptionDD2, colWidth);
			o[0, o.Cols[colName].Index] = colCaptionDD1;
		}

		public static void SetCol(this FlexGrid o, string colName, string colCaptionDD, int colWidth, int maxLength, Type colType, FormatTpType colFormat)
		{
			o.SetCol(colName, colCaptionDD, colWidth, maxLength, false, colType, colFormat);
		}

		public static void SetCol(this FlexGrid o, string colName, string colCaptionDD1, string colCaptionDD2, int colWidth, int maxLength, Type colType, FormatTpType colFormat)
		{
			o.SetCol(colName, colCaptionDD2, colWidth, maxLength, false, colType, colFormat);
			o[0, o.Cols[colName].Index] = colCaptionDD1;
		}


		public static void SetCol(this FlexGrid o, string colName, string colCaptionDD, int colWidth, TextAlignEnum alignEnum)
		{
			o.SetCol(colName, colCaptionDD, colWidth);
			o.Cols[colName].TextAlign = alignEnum;
		}

		/// <param name="o"></param>
		/// <param name="colName">열이름</param>
		/// <param name="colCaptionDD1">헤더가 2줄일때 윗헤더</param>
		/// <param name="colCaptionDD2">헤더가 2줄일때 아랫헤더</param>
		/// <param name="colWidth">넓이</param>
		/// <param name="alignEnum">정렬</param>
		public static void SetCol(this FlexGrid o, string colName, string colCaptionDD1, string colCaptionDD2, int colWidth, TextAlignEnum alignEnum)
		{
			o.SetCol(colName, colCaptionDD2, colWidth);
			o.Cols[colName].TextAlign = alignEnum;
			o[0, o.Cols[colName].Index] = colCaptionDD1;
		}


		public static void SetCol(this FlexGrid o, string colName, string colCaptionDD, int colWidth, Type colType, string colFormat)
		{
			if (colFormat == "####/##/## ##:##:##") // 날짜시간 형식은 YEAR_MONTH_DAY 요고 한번 해줘야 먹힘
				o.SetCol(colName, colCaptionDD, colWidth, false, colType, FormatTpType.YEAR_MONTH_DAY);
			else
				o.SetCol(colName, colCaptionDD, colWidth, false, colType);

			o.Cols[colName].Format = colFormat;
		}

		public static void SetCol(this FlexGrid o, string colName, string colCaptionDD, int colWidth, Type colType, string colFormat, TextAlignEnum alignEnum)
		{
			o.SetCol(colName, colCaptionDD, colWidth, false, colType);
			o.Cols[colName].Format = colFormat;
			o.Cols[colName].TextAlign = alignEnum;
		}

		public static void SetCol(this FlexGrid o, string colName, string colCaptionDD1, string colCaptionDD2, int colWidth, Type colType, string colFormat, TextAlignEnum alignEnum)
		{
			o.SetCol(colName, colCaptionDD2, colWidth, colType, colFormat, alignEnum);
			o[0, o.Cols[colName].Index] = colCaptionDD1;
		}

		public static void SetCol(this FlexGrid o, string colName, string colCaptionDD, int colWidth, Type colType, FormatTpType colFormat)
		{
			o.SetCol(colName, colCaptionDD, colWidth, false, colType, colFormat);
		}

		public static void SetCol(this FlexGrid o, string colName, string colCaptionDD1, string colCaptionDD2, int colWidth, Type colType, FormatTpType colFormat)
		{
			o.SetCol(colName, colCaptionDD2, colWidth, false, colType, colFormat);
			o[0, o.Cols[colName].Index] = colCaptionDD1;
		}

		public static void SetCol(this FlexGrid o, string colName, string colCaptionDD, int colWidth, Type colType, FormatTpType colFormat, TextAlignEnum alignEnum)
		{
			o.SetCol(colName, colCaptionDD, colWidth, false, colType, colFormat);
			o.Cols[colName].TextAlign = alignEnum;
		}

		public static void SetAutoIncrement(this FlexGrid o, string colName)
		{
			o.AfterAddRow += delegate (object sender, RowColEventArgs e) { FlexGrid_AfterAddRow(sender, e, colName); };
		}

		private static void FlexGrid_AfterAddRow(object sender, RowColEventArgs e, string colName)
		{
			FlexGrid flexGrid = (FlexGrid)sender;
			flexGrid[e.Row, colName] = (int)flexGrid.Aggregate(AggregateEnum.Max, "SEQ") + 1;
		}

		public static void SetAlternateRow(this FlexGrid flexGrid)
		{
			flexGrid.Styles["Alternate"].BackColor = Color.FromArgb(247, 247, 247);
		}

		public static void SetMalgunGothic(this FlexGrid flexGrid)
		{
			for (int i = 0; i < flexGrid.Cols.Count; i++)
				flexGrid.Cols[i].Style.Font = new Font("맑은 고딕", 9, flexGrid.Cols[i].Style.Font.Style);

			flexGrid.Cols[0].Width = 40;    // 숫자 컬럼이 글자가 뚱뚱해져서 조금 넓힘
			flexGrid.Rows.DefaultSize = 31; // 글자가 크므로 기본 높이를 좀 높임
		}







		public static void SetRowBackColor(this FlexGrid flexGrid, int row, Color color)
		{
			string styleName = color.ToString();

			if (!flexGrid.Styles.Contains(styleName))
			{
				CellStyle style = flexGrid.Styles.Add(styleName);
				style.BackColor = color;
			}

			flexGrid.Rows[row].Style = flexGrid.Styles[styleName];
			//flexGrid.SetCellStyle(row, col, styleName);
		}

		public static void SetRowBackColor(this FlexGrid flexGrid, int row, string color)
		{
			string styleName = color.ToUpper();

			if (!flexGrid.Styles.Contains(styleName))
			{
				CellStyle style = flexGrid.Styles.Add(styleName);
				style.BackColor = Color.FromName(color);
			}

			flexGrid.Rows[row].Style = flexGrid.Styles[styleName];
		}

		public static void SetCellColor(this FlexGrid flexGrid, int row, int col, Color color)
		{
			string styleName = color.ToString();

			if (!flexGrid.Styles.Contains(styleName))
			{
				CellStyle style = flexGrid.Styles.Add(styleName);
				style.ForeColor = color;
			}

			flexGrid.SetCellStyle(row, col, styleName);
		}


		public static void SetCellColor(this FlexGrid flexGrid, int row, string colName, string color)
		{
			string styleName = color.ToUpper();

			if (!flexGrid.Styles.Contains(styleName))
			{
				CellStyle style = flexGrid.Styles.Add(styleName);
				style.ForeColor = Color.FromName(color);
			}

			flexGrid.SetCellStyle(row, flexGrid.Cols[colName].Index, styleName);
		}

		public static void SetCellColor(this FlexGrid flexGrid, int row, string colName, Color color)
		{
			string styleName = color.Name.ToUpper();

			if (!flexGrid.Styles.Contains(styleName))
			{
				CellStyle style = flexGrid.Styles.Add(styleName);
				style.ForeColor = color;
			}

			flexGrid.SetCellStyle(row, flexGrid.Cols[colName].Index, styleName);
		}

		public static void SetCellImage(this FlexGrid flexGrid, int row, string colName, Image image)
		{
			flexGrid.SetCellImage(row, flexGrid.Cols[colName].Index, image);
		}

		public static void SetCellStyle(this FlexGrid flexGrid, int row, string colName, string styleName)
		{			
			flexGrid.SetCellStyle(row, flexGrid.Cols[colName].Index, styleName);
		}





		public static void EnterSearch(this TextBoxExt o)
		{
			o.KeyDown += TextBox_KeyDown;
		}

		private static void TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			TextBoxExt o = (TextBoxExt)sender;

			if (e.KeyData == Keys.Enter)
			{
				if (o.Text.Trim() == "")
					Global.MainFrame.ShowMessage("검색어를 입력하세요!");
				else
					((PageBase)o.GetContainerControl()).OnToolBarSearchButtonClicked(null, null);
			}
		}



		public static void NumericFormat(this TextBoxExt o)
		{
			o.TextAlign = HorizontalAlignment.Right;
			o.TextChanged += TextBox_TextChanged;
		}

		private static void TextBox_TextChanged(object sender, EventArgs e)
		{
			TextBoxExt o = (TextBoxExt)sender;

			int caret = o.SelectionStart;
			int commaCntOld = o.Text.Count(",");

			o.Text = string.Format("{0:#,###.##}", Regex.Replace(o.Text, "[^0-9.]", "").ToDecimal());

			int commaCntNew = o.Text.Count(",");
			o.SelectionStart = caret + (commaCntNew - commaCntOld);
		}





		/* 체크박스 */
		public static string GetValue(this CheckBoxExt o)
		{
			if (o.Checked)
				return "Y";
			else
				return "N";
		}

		/* 텍스트박스 */
		public static bool Verify(this BpCodeTextBox o, string errorMsg)
		{
			if (o.CodeValue == "")
			{
				if (errorMsg != "")
					Global.MainFrame.ShowMessage(errorMsg);

				return false;
			}

			return true;
		}


		public static bool Verify(this BpComboBox o, string errorMsg)
		{
			if (o.GetValue() == "")
			{
				if (errorMsg != "")
					Global.MainFrame.ShowMessage(errorMsg);

				return false;
			}

			return true;
		}


		public static bool Verify(this DropDownComboBox o, string errorMsg)
		{
			if (o.GetValue() == "")
			{
				if (errorMsg != "")
					Global.MainFrame.ShowMessage(errorMsg);

				return false;
			}

			return true;
		}

		public static bool Verify(this DropDownComboBox o, 공통메세지 errorMsg, params string[] values)
		{
			if (o.GetValue() == "")
			{
				UT.ShowMsg(errorMsg, values);
				return false;
			}

			return true;
		}

		public static bool Verify(this FlexGrid o, string errorMsg)
		{
			if (!o.HasNormalRow)
			{
				if (errorMsg != "")
					Global.MainFrame.ShowMessage(errorMsg);

				return false;
			}

			return true;
		}



		public static bool Verify(this TextBoxExt o, string errorMsg)
		{
			if (o.Text.Replace(" ", "") == "")
			{
				Global.MainFrame.ShowMessage(errorMsg);
				return false;
			}

			return true;
		}


		public static bool Verify(this TextBoxExt o, 공통메세지 errorMsg, params string[] values)
		{
			if (o.Text.Replace(" ", "") == "")
			{
				UT.ShowMsg(errorMsg, values);
				return false;
			}

			return true;
		}


		// ********** DropDownComboBox



		// ********** PageBase
		public static void SetConDefault(this PageBase o)
		{
			// 드랍다운 휠 차단
			foreach (var c in GetAll(o, typeof(DropDownComboBox)))
			{
				DropDownComboBox dropDownComboBox = (DropDownComboBox)c;
				dropDownComboBox.MouseWheel += DropDownComboBox_MouseWheel;
			}

			// ********** 텍스트박스 이벤트
			foreach (var c in GetAll(o, typeof(TextBoxExt)))
			{
				TextBoxExt textBox = (TextBoxExt)c;

				if (textBox.Name == "tbx포커스")
				{
					textBox.Left = -1000;
					continue;
				}

				if (!textBox.Multiline)
				{
					textBox.AutoSize = false;
					textBox.Height = 20;
				}

				textBox.GotFocus += TextBox_GotFocus;
				textBox.LostFocus += TextBox_LostFocus;
			}

			// ********** 텍스트박스 이벤트
			foreach (var c in GetAll(o, typeof(CurrencyTextBox)))
			{
				CurrencyTextBox currencyBox = (CurrencyTextBox)c;
				
				if (!currencyBox.Multiline)
				{
					currencyBox.AutoSize = false;
					currencyBox.Height = 20;
				}

				currencyBox.GotFocus += CurrencyBox_GotFocus;
				currencyBox.LostFocus += CurrencyBox_LostFocus;
			}

			// 스플릿컨테이너 분할자 너비
			foreach (var c in GetAll(o, typeof(SplitContainer)))
			{
				SplitContainer splitContainer = (SplitContainer)c;
				splitContainer.SplitterWidth = 7;
			}

		}

		

		private static void DropDownComboBox_MouseWheel(object sender, MouseEventArgs e)
		{
			((HandledMouseEventArgs)e).Handled = true;
		}

		private static IEnumerable<Control> GetAll(Control control, Type type = null)
		{
			var controls = control.Controls.Cast<Control>();
			return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls).Where(c => c.GetType() == type);
		}


		/// <summary>
		/// TextBoxExt 포커스 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void TextBox_GotFocus(object sender, EventArgs e)
		{
			TextBoxExt con = (TextBoxExt)sender;

			if (con.Tag.ToString2() == "")
				con.Tag = "BorderColor:" + con.BorderColor.ToArgb();

			con.BorderColor = Constant.FocusedBorderColor;
		}

		private static void TextBox_LostFocus(object sender, EventArgs e)
		{
			TextBoxExt con = (TextBoxExt)sender;

			if (con.Tag.ToString2().Contains("BorderColor:"))
			{
				con.BorderColor = Color.FromArgb(con.Tag.ToString().Replace("BorderColor:", "").ToInt());
				con.Tag = "";
			}
			else
			{
				con.BorderColor = Constant.UnfocusedBorderColor;
			}
		}

		/// <summary>
		/// CurrencyTextBox 포커스 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void CurrencyBox_GotFocus(object sender, EventArgs e)
		{
			CurrencyTextBox con = (CurrencyTextBox)sender;

			if (con.Tag.ToString2() == "")
				con.Tag = "BorderColor:" + con.BorderColor.ToArgb();

			con.BorderColor = Constant.FocusedBorderColor;
		}

		private static void CurrencyBox_LostFocus(object sender, EventArgs e)
		{
			CurrencyTextBox con = (CurrencyTextBox)sender;

			if (con.Tag.ToString2().Contains("BorderColor:"))
			{
				con.BorderColor = Color.FromArgb(con.Tag.ToString().Replace("BorderColor:", "").ToInt());
				con.Tag = "";
			}
			else
			{
				con.BorderColor = Constant.UnfocusedBorderColor;
			}
		}

	








		public static string GetEquipMaker(this Image image)
		{
			int ExifOrientationTagId = 0x010F;

			if (Array.IndexOf(image.PropertyIdList, ExifOrientationTagId) > -1)
				return Encoding.Default.GetString(image.GetPropertyItem(ExifOrientationTagId).Value);
			else
				return "";
		}

		public static void NormalizeOrientation(this Image image)
		{
			int ExifOrientationTagId = 0x112;

			if (Array.IndexOf(image.PropertyIdList, ExifOrientationTagId) > -1)
			{
				int orientation = image.GetPropertyItem(ExifOrientationTagId).Value[0];

				if (orientation >= 1 && orientation <= 8)
				{
					switch (orientation)
					{
						case 2:
							image.RotateFlip(RotateFlipType.RotateNoneFlipX);
							break;
						case 3:
							image.RotateFlip(RotateFlipType.Rotate180FlipNone);
							break;
						case 4:
							image.RotateFlip(RotateFlipType.Rotate180FlipX);
							break;
						case 5:
							image.RotateFlip(RotateFlipType.Rotate90FlipX);
							break;
						case 6:
							image.RotateFlip(RotateFlipType.Rotate90FlipNone);
							break;
						case 7:
							image.RotateFlip(RotateFlipType.Rotate270FlipX);
							break;
						case 8:
							image.RotateFlip(RotateFlipType.Rotate270FlipNone);
							break;
					}

					image.RemovePropertyItem(ExifOrientationTagId);
				}
			}
		}

		

		public static void Rotate90(this Image image)
		{
			image.RotateFlip(RotateFlipType.Rotate90FlipNone);
		}

		public static void Rotate180(this Image image)
		{
			image.RotateFlip(RotateFlipType.Rotate180FlipNone);
		}

		public static void Rotate270(this Image image)
		{
			image.RotateFlip(RotateFlipType.Rotate270FlipNone);
		}

		public static void FlipX(this Image image)
		{
			image.RotateFlip(RotateFlipType.RotateNoneFlipX);
		}

		public static void FlipY(this Image image)
		{
			image.RotateFlip(RotateFlipType.RotateNoneFlipY);
		}
	}
}

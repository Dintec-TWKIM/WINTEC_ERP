using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using System.Windows.Forms;
using Aspose.Email.Exchange.Schema;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Newtonsoft.Json;
using CommandType = System.Data.CommandType;
using DzHelpFormLib;
using System.Reflection;

namespace DX
{
	public static class 확장
	{
		private static string 회사코드 => Global.MainFrame.LoginInfo.CompanyCode;
		private static string 사업장코드 => Global.MainFrame.LoginInfo.BizAreaCode;
		private static string 공장코드 => Global.MainFrame.LoginInfo.CdPlant;
		private static string 사원번호 => Global.MainFrame.LoginInfo.UserID;

		private static Color 포커스컬러 => Color.FromArgb(0, 120, 215);

		private static Color 사용불가컬러 => Color.FromArgb(240, 240, 240);


		public static T 클론<T>(this T controlToClone) where T : Control
		{
			PropertyInfo[] controlProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

			T instance = Activator.CreateInstance<T>();

			foreach (PropertyInfo propInfo in controlProperties)
			{
				if (propInfo.CanWrite)
				{
					if (propInfo.Name != "WindowTarget")
						propInfo.SetValue(instance, propInfo.GetValue(controlToClone, null), null);
				}
			}

			return instance;
		}


		public static void 바인딩(this WebBrowser t, string 스타일, string 바디, bool 추가)
		{
			string html = t.DocumentText;

			if (html == "" || !추가)
			{
				html = @"
<!DOCTYPE html>

<html>
	<head>
		<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>
		<style type='text/css'>
			html, body, div, span, table, thead, tbody, tfoot, tr, th, td, img { margin:0; padding:0; border:0; outline:0; line-height:1; font-family:맑은 고딕; font-size:9pt; }			
			table { border-collapse:collapse; border-spacing:0; table-layout:fixed; }

			/* 뷰박스는 가로 + 세로 가 한 행으로 나오는 형태 */
			.dx-viewbox			{ width:100%; }
			.dx-viewbox tr > *	{ padding:0 8px; border-left:1px solid #d2d2d2; border-right:1px solid #d2d2d2; border-bottom:1px solid #d2d2d2; }
			.dx-viewbox th		{ font-weight:normal; text-align:right; background-color:#f0f0f0; }
			.dx-viewbox td		{ text-overflow:ellipsis; white-space:nowrap; overflow:hidden; }
			.dx-viewbox th:first-child	{ border-left:none; }
			.dx-viewbox td:first-child	{ border-left:none; }
			.dx-viewbox .last-cell		{ border-right:none; }
			.dx-viewbox .last-row > *	{ border-bottom:none; }

			/* 뷰박스2는 세로로만 제목 내용 반복되는 형태 */
			.dx-viewbox2		{ width:100%; }
			.dx-viewbox2 tr > *	{ text-align:left; padding:10px; border-top:1px solid #646464; border-bottom:1px solid #646464; }
			.dx-viewbox2 th		{ background-color:#f0f0f0; }
			.dx-viewbox2 td		{ line-height:17px; padding-top:8px; padding-bottom:8px; text-overflow:ellipsis; overflow:hidden; }
			
			.dx-viewbox2 th:first-child	{ border-top:none; }
			.dx-viewbox2 .last-row > *	{ border-bottom:none; }
		</style>
	</head>
	<body>		
	</body>
</html>";
			}

			int index;

			// ********** 스타일
			index = html.IndexOf("</style>");
			html = html.Insert(index, 스타일 + "\r\n");

			// ********** 바디
			// 마지막 td, th 컬럼에 last-cell 클래스 넣어줌
			foreach (Match m in Regex.Matches(바디, "<t[hd]((?:(?!<t[hd]).)*)</tr>", RegexOptions.IgnoreCase | RegexOptions.Singleline))
			{
				string 헌값 = m.Value;
				string 새값 = 헌값;
				새값 = 새값.Replace("<th", "<th class='last-cell'");
				새값 = 새값.Replace("<td", "<td class='last-cell'");
				바디 = 바디.Replace(헌값, 새값);
			}

			// 마지막 tr 행에 last-row 클래스 넣어줌
			foreach (Match m in Regex.Matches(바디, "<tr((?:(?!<tr).)*)</table>", RegexOptions.IgnoreCase | RegexOptions.Singleline))
			{
				string 헌값 = m.Value;
				string 새값 = 헌값;
				새값 = 새값.Replace("<tr", "<tr class='last-row'");
				바디 = 바디.Replace(헌값, 새값);
			}

			index = html.IndexOf("</body>");
			html = html.Insert(index, 바디 + "\r\n");

			// 바인딩
			t.Navigate("about:blank");
			t.Document.OpenNew(false);
			t.Document.Write(html);
			t.Refresh();
		}





		#region ==================================================================================================== 페이지초기화


		public static T[] 컨트롤<T>(this Control t)
		{
			List<T> list = new List<T>();

			foreach (var c in GetAll(t, typeof(T)))
				list.Add((T)Convert.ChangeType(c, typeof(T)));
			
			return list.ToArray();
		}

		public static IEnumerable<Control> 컨트롤(this Control t)
		{
			
			//var controls = t.Controls.Cast<Control>();
			////check the all value, if true then get all the controls
			////otherwise get the controls of the specified type
			//if (type == null)
			//	return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls);
			//else
			//	return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls).Where(c => c.GetType() == type);




			var queue = new Queue<Control>();
			queue.Enqueue(t);

			do
			{
				var control = queue.Dequeue();

				yield return control;

				foreach (var child in control.Controls.OfType<Control>())
					queue.Enqueue(child);

			} while (queue.Count > 0);


		}


		


		//private static IEnumerable<Control> GetAll(Control control, Type type = null)
		//{
		//	var controls = control.Controls.Cast<Control>();
		//	return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls).Where(c => c.GetType() == type);
		//}

		//public static IEnumerable<Control> 컨트롤(this Control t, string 이름, Type type = null)
		//{
		//	var controls = t.Controls.Cast<Control>();
		//	return controls.SelectMany(ctrl => 컨트롤(ctrl, type)).Concat(controls).Where(c => c.GetType() == type && c.Name == 이름);
		//}


		public static void 페이지초기화(this Control t)
		{
			

			// ********** 체크박스
			foreach (var c in GetAll(t, typeof(CheckBox)))
			{
				CheckBox chk = (CheckBox)c;
				chk.Cursor = Cursors.Hand;
			}


			// ********** 라디오버튼
			foreach (var c in GetAll(t, typeof(RadioButton)))
			{
				RadioButton rdo = (RadioButton)c;
				rdo.Cursor = Cursors.Hand;
			}

			// ********** 레이블
			foreach (var c in GetAll(t, typeof(Label)))
			{
				Label label = (Label)c;

				if (label.BorderStyle == BorderStyle.FixedSingle)
					label.Paint += Label_Paint;
			}

			


			// ********** 드랍다운 휠 차단
			foreach (var c in GetAll(t, typeof(DropDownComboBox)))
			{
				DropDownComboBox dropDownComboBox = (DropDownComboBox)c;
				dropDownComboBox.MouseWheel += DropDownComboBox_MouseWheel;
			}

			TextBoxExt focused = null;

			// ********** 텍스트박스 이벤트
			foreach (var c in GetAll(t, typeof(TextBoxExt)))
			{
				TextBoxExt textBox = (TextBoxExt)c;

				if (textBox.Name == "tbx포커스")
				{
					focused = textBox;
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

			// ********** 버튼
			foreach (Button c in t.컨트롤<Button>())
			{
				c.EnabledChanged += Button_EnabledChanged;
				//c.Click += C_Click;
				c.Click += delegate (object sender, EventArgs e) { Button_Click(sender, e, focused); };
			}


			// ********** 커런시트박스 이벤트
			foreach (var c in GetAll(t, typeof(CurrencyTextBox)))
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

			// ********** 스플릿컨테이너 분할자 너비
			foreach (var c in GetAll(t, typeof(SplitContainer)))
			{
				SplitContainer o = (SplitContainer)c;
				o.SplitterWidth = 6;
			}



			foreach (var c in GetAll(t, typeof(DropDownComboBox)))
			{
				DropDownComboBox o = (DropDownComboBox)c;
				o.ValueMember = "CODE";
				o.DisplayMember = "NAME";
			}

			// ********** VIEWBOX
			foreach (var c in GetAll(t, typeof(TableLayoutPanel)))
			{
				TableLayoutPanel lay = (TableLayoutPanel)c;

				if (lay.Tag != null && lay.Tag.ToString() == "VIEWBOX")
				{
					// 뷰박스 자식 컨트롤 마진 설정
					for (int i = 0; i < lay.RowCount; i++)
					{
						for (int j = 0; j < lay.ColumnCount; j++)
						{
							Control con = lay.GetControlFromPosition(j, i); // 주의! i, j 거꾸로임

							if (con != null)
							{
								int right = j < lay.ColumnCount - 1 ? 0 : 1;
								int bottom = i < lay.RowCount - 1 ? 0 : 1;
								con.Margin = new Padding(1, 1, right, bottom);
							}
						}
					}

					// 테두리 새로 그리기
					lay.CellPaint += TableLayoutPanel_CellPaint;
				}
			}
		}

		private static void Button_Click(object sender, EventArgs e, TextBoxExt focused)
		{
			if (focused != null)
				focused.Focus();
		}

		private static void Label_Paint(object sender, PaintEventArgs e)
		{
			((Label)sender).BorderStyle = BorderStyle.None;			
			ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.FromArgb(189, 199, 217), ButtonBorderStyle.Solid);
		}

		private static void Button_EnabledChanged(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			string desc = button.AccessibleDescription.문자();

			if (button.Enabled)
			{
				// 배경색상 있으면 가져오기
				string match = Regex.Match(desc, ",[[]테두리색상:.*[]]").Value;

				if (match != "")
				{
					button.BackColor = Color.FromArgb(match.Replace(",[테두리색상:", "").Replace("]", "").정수());
					button.AccessibleDescription = desc.Replace(match, "");
				}
				else
				{
					button.BackColor = Color.FromArgb(54, 121, 197);
				}

				button.ForeColor = Color.White;
				button.FlatAppearance.BorderSize = 0;
			}
			else
			{
				if (Regex.Match(desc, ",[[]배경색상:.*[]]").Value == "")
					button.AccessibleDescription = desc + ",[배경색상:" + button.BackColor.ToArgb() + "]";

				button.ForeColor = Color.FromArgb(142, 142, 142);
				button.BackColor = Color.FromArgb(235, 235, 235);
				button.FlatAppearance.BorderSize = 1;
				button.FlatAppearance.BorderColor = Color.FromArgb(204, 204, 204);
			}
		}

		private static void DropDownComboBox_MouseWheel(object sender, MouseEventArgs e)
		{
			((HandledMouseEventArgs)e).Handled = true;
		}

		private static void TextBox_GotFocus(object sender, EventArgs e)
		{
			TextBoxExt con = (TextBoxExt)sender;
			string desc = con.AccessibleDescription.문자();

			if (Regex.Match(desc, ",[[]테두리색상:.*[]]").Value == "")
				con.AccessibleDescription = desc + ",[테두리색상:" + con.BorderColor.ToArgb() + "]";

			con.BorderColor = 포커스컬러;
		}

		private static void TextBox_LostFocus(object sender, EventArgs e)
		{
			TextBoxExt con = (TextBoxExt)sender;
			string desc = con.AccessibleDescription.문자();
			string match = Regex.Match(desc, ",[[]테두리색상:.*[]]").Value;

			if (match != "")
			{
				con.BorderColor = Color.FromArgb(match.Replace(",[테두리색상:", "").Replace("]", "").정수());
				con.AccessibleDescription = desc.Replace(match, "");
			}
		}

		private static void CurrencyBox_GotFocus(object sender, EventArgs e)
		{
			CurrencyTextBox con = (CurrencyTextBox)sender;
			string desc = con.AccessibleDescription.문자();

			if (Regex.Match(desc, ",[[]테두리색상:.*[]]").Value == "")
				con.AccessibleDescription = desc + ",[테두리색상:" + con.BorderColor.ToArgb() + "]";

			con.BorderColor = 포커스컬러;
		}

		private static void CurrencyBox_LostFocus(object sender, EventArgs e)
		{
			CurrencyTextBox con = (CurrencyTextBox)sender;
			string desc = con.AccessibleDescription.문자();
			string match = Regex.Match(desc, ",[[]테두리색상:.*[]]").Value;

			if (match != "")
			{
				con.BorderColor = Color.FromArgb(match.Replace(",[테두리색상:", "").Replace("]", "").정수());
				con.AccessibleDescription = desc.Replace(match, "");
			}
		}

		private static void TableLayoutPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
		{
			var panel = sender as TableLayoutPanel;
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

			var rectangle = e.CellBounds;
			var pen_out = new Pen(Color.FromArgb(100, 100, 100), 1);
			var pen_in = new Pen(Color.FromArgb(210, 210, 210), 1);

			// 마지막 칸 크기 조절
			if (e.Row == (panel.RowCount - 1))
				rectangle.Height -= 1;

			if (e.Column == (panel.ColumnCount - 1))
				rectangle.Width -= 1;

			// 안쪽 먼저 그림
			e.Graphics.DrawRectangle(pen_in, rectangle);

			// 바깥쪽 테두리 그림
			if (e.Row == 0)
				e.Graphics.DrawLine(pen_out, rectangle.X, rectangle.Y, rectangle.X + rectangle.Width, rectangle.Y);
			if (e.Row == (panel.RowCount - 1))
				e.Graphics.DrawLine(pen_out, rectangle.X, rectangle.Y + rectangle.Height, rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);

			if (e.Column == 0)
				e.Graphics.DrawLine(pen_out, rectangle.X, rectangle.Y, rectangle.X, rectangle.Y + rectangle.Height);
			if (e.Column == (panel.ColumnCount - 1))
				e.Graphics.DrawLine(pen_out, rectangle.X + rectangle.Width, rectangle.Y, rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
		}

		#endregion


		#region ==================================================================================================== 텍스트박스

		public static void 한글을영어(this TextBoxExt t) => t.Text = t.Text.한글을영어();

		public static void 순번양식(this TextBoxExt t)
		{
			t.TextChanged += TextBox_TextChanged;
		}

		private static void TextBox_TextChanged(object sender, EventArgs e)
		{
			TextBoxExt textBox= (TextBoxExt)sender;

			// 마지막 글자가 .이라면 아직 입력중이므로 아무것도 안해도 됨
			if (textBox.Text.EndsWith("."))
				return;

			// 양식 변환
			int caret = textBox.SelectionStart;
			textBox.Text = string.Format("{0:####.##}", textBox.Text.실수());
			textBox.SelectionStart = caret;
		}

		#endregion



		#region ==================================================================================================== 바인딩

		//public static DataTable 텟흐트(this FreeBinding t, Control 컨테이너)
		//{
		//	DataTable dt = t.GetChanges();
		//	//dt.Rows[0][]
		//}




		public static void 바인딩초기화(this FreeBinding t, DataTable 데이터테이블, Control 컨테이너)
		{
			t.SetBinding(데이터테이블, 컨테이너);
			t.ClearAndNewRow();
		}

		public static void 바인딩(this FreeBinding t, DataTable 데이터테이블) => t.SetDataTable(데이터테이블);

		public static void 바인딩(this FreeBinding t, DataTable 데이터테이블, Control 컨테이너)
		{
			// 바인딩초기화
			DataTable dt = 데이터테이블.Clone();
			t.SetBinding(dt, 컨테이너);

			// 1행만 추가 후 바인딩 (여러행짜리 데이터테이블이 올수 있으므로 1행만 추출)
			dt.Rows.Add();
			for (int j = 0; j < dt.Columns.Count; j++) dt.Rows[0][j] = 데이터테이블.Rows[0][j];
			t.SetDataTable(dt);
		}

		public static void 바인딩(this BpComboBox t, object 값파이프, object 글파이프)
		{
			t.클리어();
			if (값파이프.문자() != "")
				t.AddItem2(값파이프.문자(), 글파이프.문자());
		}


		public static void 바인딩(this DropDownComboBox t, DataTable 데이터테이블) => t.바인딩(데이터테이블, false);

		public static void 바인딩(this DropDownComboBox t, DropDownComboBox 콤보박스, bool 빈줄여부) => t.바인딩((콤보박스.DataSource as DataTable).Copy(), 빈줄여부);

		public static void 바인딩(this DropDownComboBox t, DataRow[] 데이터로우, bool 빈줄여부) => t.바인딩(데이터로우.데이터테이블(), 빈줄여부);

		public static void 바인딩(this DropDownComboBox t, DataTable 데이터테이블, bool 빈줄여부)
		{			
			DataTable dt = 데이터테이블?.Copy();

			if (dt == null)
			{
				t.클리어();
				return;
			}

			// 필드 이름 Default 설정
			if (t.ValueMember == "")
			{
				t.ValueMember = "CODE";
				t.DisplayMember = "NAME";
			}

			// 공백 Row 추가
			if (빈줄여부)
			{
				dt.Rows.InsertAt(dt.NewRow(), 0);
				dt.Rows[0][t.ValueMember] = DBNull.Value;
				dt.Rows[0][t.DisplayMember] = DBNull.Value;
			}

			t.DataSource = dt;
		}

		public static void 바인딩(this DropDownComboBox t, DataTable 데이터테이블, string 첫행값, string 첫행글)
		{
			DataTable dt = 데이터테이블?.Copy();

			// 필드 이름 Default 설정
			if (t.ValueMember == "")
			{
				t.ValueMember = "CODE";
				t.DisplayMember = "NAME";
			}

			// 공백 Row 추가			
			dt.Rows.InsertAt(dt.NewRow(), 0);
			dt.Rows[0][t.ValueMember] = 첫행값;
			dt.Rows[0][t.DisplayMember] = 첫행글;
			
			t.DataSource = dt;
		}




		public static void 바인딩(this ComboBox t, DataTable 데이터테이블, bool 빈줄여부)
		{
			DataTable dt = 데이터테이블?.Copy();

			if (dt == null)
			{
				//t.클리어();
				return;
			}

			// 필드 이름 Default 설정
			if (t.ValueMember == "")
			{
				t.ValueMember = "CODE";
				t.DisplayMember = "NAME";
			}

			// 공백 Row 추가
			if (빈줄여부)
			{
				dt.Rows.InsertAt(dt.NewRow(), 0);
				dt.Rows[0][t.ValueMember] = DBNull.Value;
				dt.Rows[0][t.DisplayMember] = DBNull.Value;
			}

			t.DataSource = dt;
		}














		public static void 바인딩(this FlexGrid t, DataTable 데이터테이블)
		{			
			t.바인딩(데이터테이블, false);
		}

		public static void 바인딩(this FlexGrid t, DataTable 데이터테이블, bool 자동행높이)
		{		
			t.Redraw = false;
			t.Binding = 데이터테이블;
			t.Redraw = true;

			if (자동행높이)
			{
				t.Redraw = false;
				for (int i = t.Rows.Fixed; i < t.Rows.Count; i++) t.AutoSizeRow(i);
				t.Redraw = true;
			}
		}

		public static void 바인딩(this FlexGrid t, DataTable 데이터테이블, string 필터)
		{
			t.BindingAdd(데이터테이블, 필터.Replace("!=", "<>"));			
		}

		#endregion

		#region ==================================================================================================== 값		

		public static string 값(this BpComboBox t) => t.QueryWhereIn_WithValueMember;

		public static string 값(this BpCodeTextBox t) => t.CodeValue;

		public static string 값(this CheckBoxExt t) => t.Checked ? "Y" : "N";
		

		public static void 값(this BpCodeTextBox t, object value) => t.CodeValue = value.String();

		public static void 값(this RadioButton t, object value)
		{
			foreach (RadioButton rdo in t.Parent.컨트롤<RadioButton>())
			{
				if (rdo.태그().발생("," + value))
				{
					rdo.Checked = true;
					return;
				}
			}
		}

		public static string 값(this DropDownComboBox t) => t.SelectedItem == null ? "" : t.SelectedValue.ToString();
		public static string 값(this ComboBox t) => t.SelectedItem == null ? "" : t.SelectedValue.ToString();

		public static void 값(this DropDownComboBox t, object value)
		{
			if (value.ToString() == "" && t.Items.Count > 0)
				t.SelectedIndex = 0;
			else
				t.SelectedValue = value.ToString();
		}

		public static void 값(this ComboBox t, object value)
		{
			if (value.ToString() == "" && t.Items.Count > 0)
				t.SelectedIndex = 0;
			else
				t.SelectedValue = value.ToString();
		}

		public static string 관련(this DropDownComboBox t, int 인덱스)
		{
			return t.데이터행()["CD_FLAG" + 인덱스].문자();
		}


		public static decimal 값(this CurrencyTextBox t) => t.DecimalValue;

		public static string 값(this TextBoxExt t) => t.Text;

		public static string 값(this DatePicker t) => t.Text;

		

		public static DataRow 데이터행(this DropDownComboBox t)
		{
			return t.SelectedItem == null ? ((DataRowView)t.Items[0]).Row : ((DataRowView)t.SelectedItem).Row;
		}

		

		public static DateTime 더하기(this DatePicker t, int value)
		{
			if (t.Text == "")
				return new DateTime();
			else
				return t.Value.AddDays(value);
		}


		#endregion

		#region ==================================================================================================== 데이터테이블

		public static bool 존재(this DataTable t) => !(t == null) && t.Rows.Count > 0;

		public static int 카운트(this DataTable t) => t.Rows.Count;

		public static DataRow 첫행(this DataTable t) => t.Rows.Count > 0 ? t.Rows[0] : null;

		public static object 첫행(this DataTable t, string 컬럼이름) => t.Rows.Count > 0 ? t.Rows[0][컬럼이름] : null;

		public static object 첫행(this DataTable t, int 컬럼인덱스) => t.Rows.Count > 0 ? t.Rows[0][컬럼인덱스] : null;

		public static DataTable 정렬(this DataTable t, string 정렬)
		{
			t.DefaultView.Sort = 정렬;
			return t.DefaultView.ToTable();
		}

		public static void 컬럼추가(this DataTable t, string 컬럼이름) => t.Columns.Add(컬럼이름);

		public static void 컬럼추가(this DataTable t, string 컬럼이름, Type 타입) => t.Columns.Add(컬럼이름, 타입);

		public static void 컬럼추가(this DataTable t, string 컬럼이름, string 값) => t.컬럼추가(컬럼이름, typeof(string), 값);

		public static void 컬럼추가(this DataTable t, string 컬럼이름, string 값, int 위치) => t.컬럼추가(컬럼이름, typeof(string), 값, 위치);

		public static void 컬럼추가(this DataTable t, string 컬럼이름, Type 타입, object 값)
		{
			if (t == null) return;

			// 있으면 삭제하고 새로만듬
			if (t.Columns.Contains(컬럼이름))
				t.Columns.Remove(컬럼이름);

			// 추가
			t.Columns.Add(컬럼이름, 타입);

			foreach (DataRow row in t.Rows) row[컬럼이름] = 값;
		}

		public static void 컬럼추가(this DataTable t, string 컬럼이름, Type 타입, object 값, int 위치)
		{
			if (t == null) return;

			// 있으면 삭제하고 새로만듬
			if (t.Columns.Contains(컬럼이름))
				t.Columns.Remove(컬럼이름);

			t.Columns.Add(컬럼이름, 타입).SetOrdinal(위치);
			foreach (DataRow row in t.Rows) row[컬럼이름] = 값;
		}

		public static void 행추가(this DataTable t, params object[] 값s) => t.Rows.Add(값s);

		public static void 상태변환(this DataTable t, DataRowState 상태)
		{
			foreach (DataRow row in t.Rows)
			{
				if (상태 == DataRowState.Added)
					row.SetAdded();
			}
		}

		#endregion

		#region ==================================================================================================== 데이터행

		public static bool 존재(this DataRow[] t) => t.Length > 0;

		public static void 업데이트(this DataRow[] t, string 열이름, object 값)
		{
			foreach (DataRow row in t)
				row[열이름] = 값;
		}

		#endregion

		#region ==================================================================================================== 글

		public static string 글(this BpCodeTextBox t)
		{
			return t.CodeName;
		}

		public static void 글(this BpCodeTextBox t, object text)
		{
			t.CodeName = text.String();
		}

		public static string 글(this DropDownComboBox t)
		{
			return t.Text;
		}


		public static string 글(this RadioButtonExt t) => t.Text;

		public static string 글(this BpComboBox t) => t.QueryWhereIn_WithDisplayMember;


		#endregion


		#region ==================================================================================================== 사용

		public static void 사용(this Button t, bool 사용) => t.Enabled = 사용;

		public static void 사용(this CheckBoxExt t, bool 사용) => t.Enabled = 사용;

		public static void 사용(this CheckBox t, bool 사용) => t.Enabled = 사용;

		public static void 사용(this CurrencyTextBox t, bool 사용)
		{			
			if (사용)
			{
				// 이벤트 추가
				t.GotFocus += CurrencyBox_GotFocus;
				t.LostFocus += CurrencyBox_LostFocus;

				// 저장된 색상으로 돌림
				string desc = t.AccessibleDescription.문자();
				string match = Regex.Match(desc, ",[[]배경색상:.*[]]").Value;

				if (match != "")
				{
					t.BackColor = Color.FromArgb(match.Replace(",[배경색상:", "").Replace("]", "").정수());
					t.AccessibleDescription = desc.Replace(match, "");
				}
			}
			else
			{
				// 이벤트 제거
				t.GotFocus -= CurrencyBox_GotFocus;
				t.LostFocus -= CurrencyBox_LostFocus;

				// 원래 색상 저장
				string desc = t.AccessibleDescription.문자();

				if (Regex.Match(desc, ",[[]배경색상:.*[]]").Value == "")
					t.AccessibleDescription = desc + ",[배경색상:" + t.BackColor.ToArgb() + "]";

				t.BackColor = 사용불가컬러;
			}

			t.ForeColor = 사용 ? SystemColors.ControlText : Color.FromArgb(160, 160, 160);
			t.ZeroColor = t.ForeColor;	// 종류별로 다 바꿔 줘야함
			t.PositiveColor = t.ForeColor;
			t.NegativeColor = t.ForeColor;
			t.ReadOnly = !사용;	// 요기서 배경색 바꿔주므로 나중에 해야함
		}

		public static void 사용(this TextBoxExt t, bool 사용)
		{
			if (사용)
			{
				// 이벤트 추가
				t.GotFocus += TextBox_GotFocus;
				t.LostFocus += TextBox_LostFocus;

				// 저장된 색상으로 돌림
				string desc = t.AccessibleDescription.문자();
				string match = Regex.Match(desc, ",[[]배경색상:.*[]]").Value;

				if (match != "")
				{
					t.BackColor = Color.FromArgb(match.Replace(",[배경색상:", "").Replace("]", "").정수());
					t.AccessibleDescription = desc.Replace(match, "");
				}
			}
			else
			{
				// 이벤트 제거
				t.GotFocus -= TextBox_GotFocus;
				t.LostFocus -= TextBox_LostFocus;

				// 원래 색상 저장
				string desc = t.AccessibleDescription.문자();

				if (Regex.Match(desc, ",[[]배경색상:.*[]]").Value == "")
					t.AccessibleDescription = desc + ",[배경색상:" + t.BackColor.ToArgb() + "]";

				t.BackColor = 사용불가컬러;
			}

			t.BorderColor = 포커스컬러;						// 다른 색으로 갔다가 
			t.BorderColor = Color.FromArgb(189, 199, 217);	// 원래 색으로 돌림 (이상하게 사용불가 처리 되면서 테두리가 두꺼워짐)
			t.ForeColor = 사용 ? SystemColors.ControlText : Color.FromArgb(160, 160, 160);
			t.ReadOnly = !사용;   // 요기서 배경색 바꿔주므로 나중에 해야함
		}

		public static void 사용(this DropDownComboBox t, bool 사용) => t.Enabled = 사용;

		public static void 사용(this ComboBox t, bool 사용) => t.Enabled = 사용;

		public static void 사용(this DatePicker t, bool 사용)
		{
			

			if (사용)
			{

				// 저장된 색상으로 돌림
				string desc = t.AccessibleDescription.문자();
				string match = Regex.Match(desc, ",[[]배경색상:.*[]]").Value;

				if (match != "")
				{
					t.MaskBackColor = Color.FromArgb(match.Replace(",[배경색상:", "").Replace("]", "").정수());
					t.AccessibleDescription = desc.Replace(match, "");
				}
			}
			else
			{
				// 원래 색상 저장
				string desc = t.AccessibleDescription.문자();

				if (Regex.Match(desc, ",[[]배경색상:.*[]]").Value == "")
					t.AccessibleDescription = desc + ",[배경색상:" + t.MaskBackColor.ToArgb() + "]";

				t.MaskBackColor = 사용불가컬러;
			}

			t.Enabled = 사용;
		}

		public static void 사용(this RadioButton t, bool 사용) => t.Enabled = 사용;

		//public static void 사용(this TextBoxExt t, bool 사용) => t.Enabled = 사용;

		public static void 사용(this FlexGrid t, bool 사용)
		{
			string[] 컬럼s = t.AccessibleDescription.분할(",");
			t.에디트컬럼(사용, 컬럼s);
		}



		public static void 사용(this BpCodeTextBox t, bool 사용) => t.ReadOnly = !사용 ? ReadOnly.TotalReadOnly : ReadOnly.None;

		public static void 사용(this PanelExt t, bool 사용)
		{
			// 이전 컬럼 체크
			if (t.Parent is TableLayoutPanel lay)
			{
				int row = lay.GetRow(t);
				int col = lay.GetColumn(t);

				// 두번째 컬럼 && 이전 컬럼이 레이블일 경우 레이블도 비활성화
				if (col > 0 && lay.GetControlFromPosition(col - 1, row) is Label lbl)
					lbl.Enabled = 사용;
			}

			// 자식 컨트롤에 따라 동작
			foreach (Control c in t.Controls)
			{
				if		(c is TextBoxExt		tbx) tbx.ReadOnly = !사용;
				else if (c is BpCodeTextBox		ctx) ctx.ReadOnly = !사용 ? ReadOnly.TotalReadOnly : ReadOnly.None;
				else if (c is CheckBoxExt		chk) chk.사용(사용);
				else if (c is CurrencyTextBox	cur) cur.ReadOnly = !사용;
				else c.Enabled = 사용;
			}
		}

		public static void 사용(this Panel t, bool 사용)
		{
			// 이전 컬럼 체크
			if (t.Parent is TableLayoutPanel lay)
			{
				int row = lay.GetRow(t);
				int col = lay.GetColumn(t);

				// 두번째 컬럼 && 이전 컬럼이 레이블일 경우 레이블도 비활성화
				if (col > 0 && lay.GetControlFromPosition(col - 1, row) is Label lbl)
					lbl.Enabled = 사용;
			}

			// 자식 컨트롤에 따라 동작
			foreach (Control c in t.Controls)
			{
				if (c is TextBoxExt tbx) tbx.사용(사용);
				else if (c is BpCodeTextBox ctx) ctx.ReadOnly = !사용 ? ReadOnly.TotalReadOnly : ReadOnly.None;
				else if (c is CurrencyTextBox cur) cur.사용(사용);
				else if (c is DatePicker dtp) dtp.사용(사용);
				else c.Enabled = 사용;
			}
		}



		public static bool 사용(this BpCodeTextBox t) => t.ReadOnly != ReadOnly.TotalReadOnly;

		public static bool 사용(this CheckBox t) => t.Enabled;

		public static bool 사용(this ComboBox t) => t.Enabled;

		public static bool 사용(this DatePicker t) => t.Enabled;

		public static bool 사용(this TextBoxExt t) => !t.ReadOnly;





		#endregion

		public static void 강조(this LabelExt t, 강조모드 강조모드)
		{
			if (강조모드 == 강조모드.빨강)
			{
				t.ForeColor = Color.Red;
				t.Font = new Font(t.Font, FontStyle.Bold);
			}
			else if (강조모드 == 강조모드.검정)
			{
				t.ForeColor = Color.Black;
				t.Font = new Font(t.Font, FontStyle.Bold);
			}
			else if (강조모드 == 강조모드.사용불가)
			{
				t.ForeColor = Color.LightGray;
				t.Font = new Font(t.Font, FontStyle.Regular);
			}
		}


		#region ==================================================================================================== 그리드

		public static string 세팅버전(this FlexGrid t) => t.SettingVersion;

		public static void 세팅시작(this FlexGrid t, int 헤드수)
		{
			t.BeginSetting(헤드수, 1, false);
		}

		public static void 세팅종료(this FlexGrid t, string version, bool 합계행) => t.세팅종료(version, 합계행, false);

		public static void 세팅종료(this FlexGrid t, string version, bool 합계행, bool 바인딩초기화)
		{
			// ********** 그리드 초기화 체크
			string formName = t.GetContainerControl().ToString().Split('.')[1];

			if (File.Exists(Application.StartupPath + @"\UserConfig\Grid\" + formName + ".init"))
			{
				File.Delete(Application.StartupPath + @"\UserConfig\Grid\" + formName + ".init");
				File.Delete(Application.StartupPath + @"\UserConfig\Grid\" + formName + ".dzw");
				File.Delete(Application.StartupPath + @"\UserConfig\Help\Grid\" + formName + ".dzw");
			}

			// ********** 마무리
			t.KeyActionEnter = KeyActionEnum.MoveDown;
			t.SettingVersion = version;
			t.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, 합계행 ? SumPositionEnum.Top : SumPositionEnum.None);
			t.Styles.Highlight.Clear();
			t.Styles.Highlight.BackColor = Color.FromArgb(255, 230, 244, 255);

			// ********** 헤드 높이
			int headerCount = t.Rows.Fixed - (합계행 ? 1 : 0);
			for (int i = 0; i < headerCount; i++)
				t.Rows[i].Height = 31;
			
			// ********** 홀수행 짝수행 구분
			t.Styles["Alternate"].BackColor = Color.FromArgb(247, 247, 247);

			for (int i = 0; i < t.Cols.Count; i++)
				t.Cols[i].Style.Font = new Font("맑은 고딕", 9, t.Cols[i].Style.Font.Style);

			//t.Cols[0].Width = 40;    // 숫자 컬럼이 글자가 뚱뚱해져서 조금 넓힘
			t.Rows.DefaultSize = 31; // 글자가 크므로 기본 높이를 좀 높임

			// 사번에 따라서 그리드 선택행 배경색 조정
			if (사원번호 == "S-472")
				t.Styles.Highlight.BackColor = Color.FromArgb(255, 188, 229, 255);  // 선택행 스타일 변경 (좀 진하게) 기본값 : 255, 230, 244, 255

			// ********** 바인딩 초기화 : 자동으로 해줌
			if (바인딩초기화)
			{
				DataTable dt = new DataTable();

				for (int i = t.Cols.Fixed; i < t.Cols.Count; i++)
				{
					// 이미 추가된 컬럼은 패스
					if (dt.Columns.Contains(t.컬럼이름(i)))
						continue;

					// 특정 컬럼은 정해진 데이터 타입으로 해줌
					if (t.컬럼이름(i).포함("NO_LINE", "SEQ", "LT"))
						dt.Columns.Add(t.컬럼이름(i), typeof(int));
					else
						dt.Columns.Add(t.컬럼이름(i), t.Cols[i].DataType);
				}

				t.Binding = dt;
			}

			// 기본 이벤트 부여
			t.BeforeDoubleClick += new BeforeMouseDownEventHandler(FlexGrid_BeforeDoubleClick);

			
		}

		private static void FlexGrid_BeforeDoubleClick(object sender, BeforeMouseDownEventArgs e)
		{
			bool init = Control.ModifierKeys == (Keys.Control | Keys.Alt) || Control.ModifierKeys == Keys.Shift;
			FlexGrid flexGrid = (FlexGrid)sender;
			int row = flexGrid.MouseRow;
			int col = flexGrid.MouseCol;

			// ********** 헤더 젤 오른쪽 빈공간 클릭했을 때 특별한 기능 수행
			if (row < flexGrid.Rows.Fixed && col <= 0)
			{
				if (init)
				{
					//H_CZ_GRID_CONFIG f = new H_CZ_GRID_CONFIG(o);
					//f.ShowDialog();
				}
				else if (flexGrid.HasNormalRow)
				{
					flexGrid.자동행높이();
				}

				e.Cancel = true;
			}

			// ********** 뒤에어 오는 마우스더블클릭 무조건 버리는 이벤트, before에서 미리 취소해준다
			if (!flexGrid.HasNormalRow || col <= 0)
				e.Cancel = true;
		}

		

		public static void 패널바인딩(this FlexGrid t, TableLayoutPanel tableLayoutPanel)
		{
			// 패널 바인딩
			List<PanelExt> 패널s = new List<PanelExt>();

			foreach (Control c in tableLayoutPanel.Controls)
			{
				if (c is PanelExt ext)
					패널s.Add(ext);
			}

			t.SetBinding(패널s.ToArray(), new object[] { });

			// 체크박스 바인딩 (체크박스는 Y,N 말고 다른 형태로는 안받는다고 생각함
			foreach (CheckBoxExt c in GetAll(tableLayoutPanel, typeof(CheckBoxExt)))
			{
				if (c.태그() != "")
					t.SetBindningCheckBox(c, "Y", "N");
			}

			// 라디오버튼 바인딩
			foreach (PanelExt 패널 in 패널s)
			{
				RadioButtonExt[] 라디오s = 패널.컨트롤<RadioButtonExt>();

				// 라디오버튼이 하나라도 있으면
				if (라디오s.Length > 0)
				{
					string[] 값s = new string[라디오s.Length];

					for (int i = 0; i < 라디오s.Length; i++)
					{
						값s[i] = 라디오s[i].태그().분할(",")[1];			// , 이후는 값 집합으로 쓰기
						라디오s[i].Tag = 라디오s[i].태그().분할(",")[0];	// 태그는 , 이전으로 변경 (값 쓰고 태그 변경)
					}

					t.SetBindningRadioButton(라디오s, 값s);
				}
			}
		}

		public static void 데이터맵(this FlexGrid t, string 컬럼명, DataTable 데이터테이블) => t.SetDataMap(컬럼명, 데이터테이블, "CODE", "NAME");

		public static void 데이터맵(this FlexGrid t, string 컬럼명, DataRow[] 데이터행) => t.SetDataMap(컬럼명, 데이터행.DataTable(), "CODE", "NAME");

		public static void 상세그리드(this FlexGrid t, params FlexGrid[] 그리드) => t.DetailGrids = 그리드;

		public static string 상세그리드필터(this FlexGrid t)
		{
			string 필터 = "";

			foreach (string s in t.VerifyPrimaryKey)
			{
				if (t.Cols[s].DataType.포함(typeof(int), typeof(decimal)))
					필터 += " AND " + s + " = " + t[s];
				else
					필터 += " AND " + s + " = '" + t[s] + "'";
			}

			return 필터.Substring(4);
		}

		public static bool 상세그리드쿼리(this FlexGrid t) => t.DetailQueryNeed;

		public static void 기본키(this FlexGrid t, params string[] 기본키) => t.VerifyPrimaryKey = 기본키;

		public static void 필수값(this FlexGrid t, params string[] 필수값) => t.VerifyNotNull = 필수값;

		public static void 더미값(this FlexGrid t, params string[] 더미값) => t.SetDummyColumn(더미값);

		public static void 더미컬럼(this FlexGrid t, params string[] 컬럼) => t.SetDummyColumn(컬럼);

		public static string[] 기본키(this FlexGrid t) => t.VerifyPrimaryKey;

		public static void 그리기중지(this FlexGrid t) => t.Redraw = false;

		public static void 그리기시작(this FlexGrid t) => t.Redraw = true;

		public static void 필터(this FlexGrid t, string 필터) => t.RowFilter = 필터.Replace("!=", "<>").Replace("==", "=");

		public static void 컬럼세팅(this FlexGrid t, string 컬럼명, string 캡션, bool 표시여부) => t.SetCol(컬럼명, 캡션, 표시여부);

		public static void 컬럼세팅(this FlexGrid t, string 컬럼명, string 캡션, int 넓이) => t.SetCol(컬럼명, 캡션, 넓이);

		public static void 컬럼세팅(this FlexGrid t, string 컬럼명, string 캡션, int 넓이, bool 표시여부)
		{
			t.SetCol(컬럼명, 캡션, 넓이);
			t.Cols[컬럼명].Visible = 표시여부;
		}

		public static void 컬럼세팅(this FlexGrid t, string 컬럼명, string 캡션1, string 캡션2, int 넓이)
		{
			t.SetCol(컬럼명, 캡션2, 넓이);
			t[0, t.Cols[컬럼명].Index] = 캡션1;
		}

		public static void 컬럼세팅(this FlexGrid t, string 컬럼명, string 캡션1, string 캡션2, int 넓이, bool 표시여부)
		{
			t.SetCol(컬럼명, 캡션2, 넓이);
			t[0, t.Cols[컬럼명].Index] = 캡션1;
			t.Cols[컬럼명].Visible = 표시여부;
		}

		public static void 컬럼세팅(this FlexGrid t, string 컬럼명, string 캡션1, string 캡션2, int 넓이, 정렬 정렬)
		{
			t.SetCol(컬럼명, 캡션2, 넓이);
			t.Cols[컬럼명].TextAlign = 그리드텍스트정렬(정렬);
			t.Cols[컬럼명].ImageAlign = 그리드이미지정렬(정렬);
			t[0, t.Cols[컬럼명].Index] = 캡션1;
		}

		public static void 컬럼세팅(this FlexGrid t, string 컬럼명, string 캡션1, string 캡션2, int 넓이, 포맷 포맷)
		{
			if		(포맷 == 포맷.체크)		t.SetCol(컬럼명, 캡션2, 넓이, CheckTypeEnum.Y_N);
			else if (포맷 == 포맷.정수)		t.SetCol(컬럼명, 캡션2, 넓이, typeof(int), "#,###");
			else if (포맷 == 포맷.소수)		t.SetCol(컬럼명, 캡션2, 넓이, typeof(int), "#,###.##");
			else if (포맷 == 포맷.날짜시간)	t.SetCol(컬럼명, 캡션2, 넓이, typeof(string), "####/##/## ##:##:##");
			else							t.SetCol(컬럼명, 캡션2, 넓이, 최대길이(포맷), false, 그리드데이터타입(포맷), 그리드포맷(포맷));			

			t[0, t.Cols[컬럼명].Index] = 캡션1;
		}

		public static void 컬럼세팅(this FlexGrid t, string 컬럼명, string 캡션, int 넓이, 정렬 정렬)
		{
			t.SetCol(컬럼명, 캡션, 넓이);
			t.Cols[컬럼명].TextAlign = 그리드텍스트정렬(정렬);
			t.Cols[컬럼명].ImageAlign = 그리드이미지정렬(정렬);
		}

		public static void 컬럼세팅(this FlexGrid t, string 컬럼명, string 캡션, int 넓이, 포맷 포맷) => t.컬럼세팅(컬럼명, 캡션, 넓이, 포맷, true);

		public static void 컬럼세팅(this FlexGrid t, string 컬럼명, string 캡션, int 넓이, 포맷 포맷, bool 표시여부)
		{
			if (포맷 == 포맷.체크)
				t.SetCol(컬럼명, 캡션, 넓이, CheckTypeEnum.Y_N);
			else if (포맷 == 포맷.순번)
				t.SetCol(컬럼명, 캡션, 넓이, typeof(decimal), "####.##", TextAlignEnum.CenterCenter);
			else if (포맷 == 포맷.날짜시간)
				t.SetCol(컬럼명, 캡션, 넓이, typeof(string), "####/##/## ##:##:##");
			else
				t.SetCol(컬럼명, 캡션, 넓이, 최대길이(포맷), false, 그리드데이터타입(포맷), 그리드포맷(포맷));

			t.Cols[컬럼명].Visible = 표시여부;
		}

		public static void 컬럼세팅(this FlexGrid t, string 컬럼명, string 캡션, int 넓이, string 포맷, 정렬 정렬)
		{			
			t.SetCol(컬럼명, 캡션, 넓이, false, 그리드데이터타입(포맷));
			t.Cols[컬럼명].Format = 포맷;
			t.Cols[컬럼명].TextAlign = 그리드텍스트정렬(정렬);
			t.Cols[컬럼명].ImageAlign = 그리드이미지정렬(정렬);
		}

		/// <summary>
		/// 정렬을 그리드 컬럼 정렬 방식으로 변경
		/// </summary>		
		private static TextAlignEnum 그리드텍스트정렬(정렬 정렬)
		{
			if (정렬 == 정렬.오른쪽)		return TextAlignEnum.RightCenter;
			else if (정렬 == 정렬.가운데)	return TextAlignEnum.CenterCenter;
			else						return TextAlignEnum.LeftCenter;
		}

		/// <summary>
		/// 정렬을 그리드 컬럼 정렬 방식으로 변경
		/// </summary>
		private static ImageAlignEnum 그리드이미지정렬(정렬 정렬)
		{
			if (정렬 == 정렬.오른쪽)		return ImageAlignEnum.RightCenter;
			else if (정렬 == 정렬.가운데)	return ImageAlignEnum.CenterCenter;
			else						return ImageAlignEnum.LeftCenter;
		}

		/// <summary>
		/// 포맷을 그리드 포맷 방식으로 변경
		/// </summary>
		private static FormatTpType 그리드포맷(포맷 포맷)
		{
			if (포맷 == 포맷.수량)			return FormatTpType.QUANTITY;
			else if (포맷 == 포맷.원화단가)	return FormatTpType.MONEY;
			else if (포맷 == 포맷.외화단가)	return FormatTpType.FOREIGN_MONEY;
			else if (포맷 == 포맷.비율)		return FormatTpType.RATE;
			else if (포맷 == 포맷.환율)		return FormatTpType.EXCHANGE_RATE;
			else if (포맷 == 포맷.날짜)		return FormatTpType.YEAR_MONTH_DAY;
			else							return FormatTpType.NONE;
		}

		/// <summary>
		/// 포맷을 그리드 포맷 방식으로 변경
		/// </summary>
		private static int 최대길이(포맷 포맷)
		{
			if (포맷 == 포맷.수량)			return 5;
			else if (포맷 == 포맷.원화단가)	return 10;
			else if (포맷 == 포맷.외화단가)	return 10;
			else if (포맷 == 포맷.비율)		return 4;
			else if (포맷 == 포맷.환율)		return 6;
			else if (포맷 == 포맷.날짜)		return 0;
			else							return 0;
		}

		/// <summary>
		/// 포맷문자에 따른 데이터 타입가져오기
		/// </summary>
		private static Type 그리드데이터타입(string 포맷)
		{
			if (포맷.포함("####.##"))	return typeof(decimal);
			if (포맷.포함("hh"))		return typeof(DateTime);
			else					return typeof(string);
		}

		/// <summary>
		/// 포맷형식에 따른 데이터 타입가져오기
		/// </summary>
		private static Type 그리드데이터타입(포맷 포맷)
		{
			if (포맷.포함(포맷.수량, 포맷.원화단가, 포맷.외화단가, 포맷.비율, 포맷.환율))
				return typeof(decimal);
			else
				return typeof(string);
		}

		public static void 컬럼보이기(this FlexGrid t, params string[] 컬럼이름)
		{
			foreach (string s in 컬럼이름)
				t.Cols[s].Visible = true;
		}

		public static void 컬럼숨기기(this FlexGrid t, params string[] 컬럼이름)
		{
			foreach (string s in 컬럼이름)
				t.Cols[s].Visible = false;
		}

		public static void 컬럼에디트가능(this FlexGrid t, params string[] 컬럼명) => 컬럼에디트(t, true, 컬럼명);

		public static void 컬럼에디트불가(this FlexGrid t, params string[] 컬럼명) => 컬럼에디트(t, false, 컬럼명);


		public static void 컬럼에디트(this FlexGrid t, bool 에디트여부, params string[] 컬럼명)
		{
			// 사용 불가 처리때 쓰기 위해 컬럼 저장
			t.AccessibleDescription = 컬럼명.결합(",");

			// 스타일 추가
			string 에디트가능 = "EDIT_HEADER_Y";
			string 에디트불가 = "EDIT_HEADER_N";

			if (!t.Styles.Contains(에디트가능))
			{
				t.Styles.Add(에디트가능);
				t.Styles[에디트가능].ForeColor = Color.Blue;
				t.Styles[에디트가능].Font = new Font(t.Font, FontStyle.Bold);
			}

			if (!t.Styles.Contains(에디트불가))
			{
				t.Styles.Add(에디트불가);
				t.Styles[에디트불가].ForeColor = Color.White;
			}

			// 스타일 부여
			foreach (string s in 컬럼명)
			{
				t.Cols[s].AllowEditing = 에디트여부;

				// 스타일 세팅 (해당셀에 스타일이 없는 경우에만)
				int row = t.헤드행(s);
				int col = t.Cols[s].Index;

				//if (t.GetCellStyle(row, col) == null)
				t.SetCellStyle(row, col, 에디트여부 ? 에디트가능 : 에디트불가);
			}
		}




		public static string[] 에디트컬럼(this FlexGrid t) => t.AccessibleDescription?.분할(",");

		public static void 에디트컬럼(this FlexGrid t, params string[] 컬럼명) => 에디트컬럼(t, true, 컬럼명);

		public static void 에디트컬럼(this FlexGrid t, bool 에디트여부, params string[] 컬럼명)
		{
			if (컬럼명 == null)
				return;

			// 사용 불가 처리때 쓰기 위해 컬럼 저장
			t.AccessibleDescription = 컬럼명.결합(",");

			// 스타일 추가
			string 에디트가능 = "EDIT_HEADER_Y";
			string 에디트불가 = "EDIT_HEADER_N";

			if (!t.Styles.Contains(에디트가능))
			{
				t.Styles.Add(에디트가능);
				t.Styles[에디트가능].ForeColor = Color.Blue;
				t.Styles[에디트가능].Font = new Font(t.Font, FontStyle.Bold);
			}

			if (!t.Styles.Contains(에디트불가))
			{
				t.Styles.Add(에디트불가);
				t.Styles[에디트불가].ForeColor = Color.White;
			}

			// 스타일 부여
			foreach (string s in 컬럼명)
			{				
				t.Cols[s].AllowEditing = 에디트여부;

				// 스타일 세팅 (해당셀에 스타일이 없는 경우에만)
				int row = t.헤드행(s);
				int col = t.Cols[s].Index;

				//if (t.GetCellStyle(row, col) == null)
				t.SetCellStyle(row, col, 에디트여부 ? 에디트가능 : 에디트불가);
			}
		}


		public static void 컬럼복사(this FlexGrid t, FlexGrid 대상그리드)
		{
			for (int i = 0; i < 대상그리드.Cols.Count; i++)
			{
				string 컬럼이름 = 대상그리드.Cols[i].Name;
				if (컬럼이름 == "") continue;

				// 컬럼추가
				t.SetCol(컬럼이름, "", true);

				// 캡션
				t.Cols[컬럼이름].Caption		= 대상그리드.Cols[컬럼이름].Caption;
				if (대상그리드.Rows.Fixed == 2)
				{
					t[0, 컬럼이름] = 대상그리드[0, 컬럼이름];
					t[1, 컬럼이름] = 대상그리드[1, 컬럼이름];
				}

				// 타입 먼저 복사해야함 (정렬 같은게 바뀜)
				t.Cols[컬럼이름].DataMap		= 대상그리드.Cols[컬럼이름].DataMap;
				t.Cols[컬럼이름].DataType		= 대상그리드.Cols[컬럼이름].DataType;

				// 기타
				t.Cols[컬럼이름].Width		= 대상그리드.Cols[컬럼이름].Width;
				t.Cols[컬럼이름].Format		= 대상그리드.Cols[컬럼이름].Format;
				t.Cols[컬럼이름].TextAlign	= 대상그리드.Cols[컬럼이름].TextAlign;
				t.Cols[컬럼이름].ImageAlign	= 대상그리드.Cols[컬럼이름].ImageAlign;
				t.Cols[컬럼이름].Visible		= 대상그리드.Cols[컬럼이름].Visible;				
			}
		}

		public static void 세팅복사(this FlexGrid t, FlexGrid 대상그리드)
		{
			t.세팅복사(대상그리드, false);
		}

		public static void 세팅복사(this FlexGrid t, FlexGrid 대상그리드, bool 바인딩초기화)
		{
			t.세팅시작(대상그리드.Rows.Fixed);

			t.컬럼복사(대상그리드);

			t.기본키(대상그리드.기본키());
			t.에디트컬럼(대상그리드.에디트컬럼());
			t.세팅종료(대상그리드.SettingVersion, 대상그리드.SumPosition == SumPositionEnum.Top, 바인딩초기화);
		}


		/// <summary>
		/// 합계 제외 컬럼 세팅, 컬럼 세팅 종료 후 해야 동작함
		/// </summary>
		public static void 합계제외컬럼(this FlexGrid t, params string[] 컬럼명) => t.SetExceptSumCol(컬럼명);


		public static void 합계컬럼스타일(this FlexGrid t, params string[] 컬럼이름)
		{
			foreach (string s in 컬럼이름)
			{
				// 배경색 변경, 스타일을 지정하면 Format 형식까지 날아가버림
				t.Cols[s].Style.BackColor = Color.FromArgb(241, 241, 241);
			}
		}


		public static int 헤드행(this FlexGrid t, string colName)
		{
			// 실제 Data행의 직전 행 Row 인덱스
			int headerRow = t.Rows.Fixed - 1;

			// 필터링 줄 있을때 한줄 내림 (아래거랑 세트로 옴)
			if (t.GetCellStyle(headerRow, 1) != null && t.GetCellStyle(headerRow, 1).Name == "!u!s!e!r!F!i!l!t!e!r!1")
				headerRow--;

			// Visible이 false 인 경우 한줄 내림 (위에거랑 세트로 옴)
			if (!t.Rows[headerRow].Visible)
				headerRow--;

			// 합계줄 있을때 한줄 내림
			if (t.SumPosition == SumPositionEnum.Top)
				headerRow--;

			// 셀 병합일때 한줄 내림
			if (headerRow >= 1)
			{
				if (t[headerRow - 1, colName].ToString() == t[headerRow, colName].ToString())
					headerRow--;
			}

			return headerRow;
		}

		public static void 행추가(this FlexGrid t)
		{
			t.Rows.Add();
			t.Row = t.Rows.Count - 1;
		}

		public static void 행추가완료(this FlexGrid t)
		{
			bool redraw = t.Redraw;
			
			if (!redraw) t.Redraw = false;
			int width = t.Cols[0].Width;
			t.AddFinished();
			//t.Cols[0].Width = width;
			if (!redraw) t.Redraw = true;
		}

		public static void 행삽입(this FlexGrid t, int row)
		{
			t.InsertRow(row);
			t.Row = row;
		}

		public static void 행삽입완료(this FlexGrid t) => t.AddFinished();

		public static void 행수정완료(this FlexGrid t) => t.AddFinished();

		public static void 행삭제(this FlexGrid t) => t.Rows.Remove(t.Row);

		public static void 행삭제(this FlexGrid t, int row) => t.Rows.Remove(row);


		public static bool 헤더클릭(this FlexGrid t)
		{
			if (!t.HasNormalRow)
				return false;
			else
				return t.MouseRow < t.Rows.Fixed;
		}

		public static bool 아이템클릭(this FlexGrid t)
		{
			if (!t.HasNormalRow)
				return false;
			else
				return t.MouseRow >= t.Rows.Fixed;
		}


		public static void 자동행높이(this FlexGrid t)
		{
			t.Redraw = false;
			for (int i = t.Rows.Fixed; i < t.Rows.Count; i++) t.AutoSizeRow(i);
			t.Redraw = true;
		}



		public static void 셀글자색_빨강(this FlexGrid t, int 행, string 컬럼이름) => t.셀글자색_빨강(행, 컬럼이름, true);
		public static void 셀글자색_빨강(this FlexGrid t, int 행, string 컬럼이름, bool 조건식) => t.셀글자색(행, 컬럼이름, 조건식 ? Color.Red : SystemColors.WindowText);
		public static void 셀글자색_빨강강조(this FlexGrid t, int 행, string 컬럼이름, bool 조건식) => t.셀글자색(행, 컬럼이름, 조건식 ? Color.Red : SystemColors.WindowText, 조건식);

		public static void 셀글자색_파랑(this FlexGrid t, int 행, string 컬럼이름) => t.셀글자색_파랑(행, 컬럼이름, true);
		public static void 셀글자색_파랑(this FlexGrid t, int 행, string 컬럼이름, bool 조건식) => t.셀글자색(행, 컬럼이름, 조건식 ? Color.Blue : SystemColors.WindowText);
		public static void 셀글자색_파랑강조(this FlexGrid t, int 행, string 컬럼이름, bool 조건식) => t.셀글자색(행, 컬럼이름, 조건식 ? Color.Blue : SystemColors.WindowText, 조건식);

		public static void 셀글자색_초록(this FlexGrid t, int 행, string 컬럼이름) => t.셀글자색_초록(행, 컬럼이름, true);
		public static void 셀글자색_초록(this FlexGrid t, int 행, string 컬럼이름, bool 조건식) => t.셀글자색(행, 컬럼이름, 조건식 ? Color.Green : SystemColors.WindowText);
		public static void 셀글자색_초록강조(this FlexGrid t, int 행, string 컬럼이름) => t.셀글자색(행, 컬럼이름, Color.Green, true);
		public static void 셀글자색_초록강조(this FlexGrid t, int 행, string 컬럼이름, bool 조건식) => t.셀글자색(행, 컬럼이름, 조건식 ? Color.Green : SystemColors.WindowText, 조건식);

		public static void 셀글자색(this FlexGrid t, int 행, string 컬럼이름, string 색) => t.셀글자색(행, 컬럼이름, 색 == "" ? SystemColors.WindowText : ColorTranslator.FromHtml(색));
		public static void 셀글자색(this FlexGrid t, int 행, string 컬럼이름, string 색, bool 굵게) => t.셀글자색(행, 컬럼이름, 색 == "" ? SystemColors.WindowText : ColorTranslator.FromHtml(색), 굵게);

		public static void 셀글자색(this FlexGrid t, int 행, string 컬럼이름, Color 색)
		{
			string 스타일 = "CELL_FORECOLOR_" + 색.Name.ToUpper();

			if (!t.Styles.Contains(스타일))
			{
				t.Styles.Add(스타일);
				t.Styles[스타일].ForeColor = 색;
				
			}

			t.SetCellStyle(행, t.Cols[컬럼이름].Index, 스타일);
		}

		public static void 셀글자색(this FlexGrid t, int 행, string 컬럼이름, Color 색, bool 굵게)
		{
			string 스타일 = "CELL_FORECOLOR_" + 색.Name.ToUpper() + "_" + 굵게.ToString().ToUpper();

			if (!t.Styles.Contains(스타일))
			{
				t.Styles.Add(스타일);
				t.Styles[스타일].ForeColor = 색;
				t.Styles[스타일].Font = new Font(t.Cols[0].Style.Font, 굵게 ? FontStyle.Bold : FontStyle.Regular);
			}

			t.SetCellStyle(행, t.Cols[컬럼이름].Index, 스타일);
		}





		public static void 셀배경색_노랑(this FlexGrid t, int 행, string 컬럼이름) => t.셀배경색_노랑(행, 컬럼이름, true);
		public static void 셀배경색_노랑(this FlexGrid t, int 행, string 컬럼이름, bool 조건식) => t.셀배경색(행, 컬럼이름, 조건식 ? Color.Yellow : SystemColors.Window);

		public static void 셀배경색(this FlexGrid t, int 행, string 컬럼이름, string 색) => t.셀배경색(행, 컬럼이름, 색 != "" ? ColorTranslator.FromHtml(색) : SystemColors.Window);
		public static void 셀배경색(this FlexGrid t, int 행, string 컬럼명, Color 색)
		{
			string 스타일 = "CELL_BACKCOLOR_" + 색.Name.ToUpper();

			if (!t.Styles.Contains(스타일))
			{
				t.Styles.Add(스타일);
				t.Styles[스타일].BackColor = 색;
			}

			// 배경색은 스타일 자체를 없애버려야 alternative가 영향을 안받음
			t.SetCellStyle(행, 컬럼명, 색 == SystemColors.Window ? "" : 스타일);
		}








		public static void 셀밑줄(this FlexGrid t, int 행, string 컬럼이름, bool 밑줄)
		{
			string 스타일 = "CELL_UNDERLINE";

			if (!t.Styles.Contains(스타일))
			{
				t.Styles.Add(스타일);
				t.Styles[스타일].Font = new Font(t.Cols[0].Style.Font, FontStyle.Underline);
			}

			t.SetCellStyle(행, t.Cols[컬럼이름].Index, 밑줄 ? 스타일 : "");	
		}


		public static void 셀경고(this FlexGrid t, int 행, string 컬럼이름, bool 조건식)
		{
			string 스타일 = "CELL_WARNING";

			if (!t.Styles.Contains(스타일))
			{
				t.Styles.Add(스타일);
				t.Styles[스타일].Font = new Font(t.Cols[0].Style.Font, FontStyle.Bold);
				t.Styles[스타일].ForeColor = Color.Red;
				t.Styles[스타일].BackColor = Color.Yellow;
			}

			t.SetCellStyle(행, 컬럼이름, 조건식 ? 스타일 : "");
		}





		public static void 셀이미지(this FlexGrid t, int 행, string 컬럼이름, Image 이미지) => t.SetCellImage(행, 컬럼이름, 이미지);
		public static void 셀스타일(this FlexGrid t, int 행, string 컬럼이름, string 스타일) => t.SetCellStyle(행, 컬럼이름, 스타일);

		public static void 굵게(this Column t) => t.굵게(true);

		public static void 굵게(this Column t, bool 굵게)
		{
			if (굵게)
				t.Style.Font = new Font(t.Style.Font, FontStyle.Bold);
			else
				t.Style.Font = new Font(t.Style.Font, FontStyle.Regular);
		}

		




		public static void 글자색(this Column t, Color 색) => t.Style.ForeColor = 색;

		public static void 배경색(this Column t, Color 색) => t.Style.BackColor = 색;

		public static void 행글자색(this FlexGrid t, int 행, Color 색)
		{
			string 스타일 = "ROW_FORECOLOR_" + 색.Name.ToUpper();

			if (!t.Styles.Contains(스타일))
			{
				t.Styles.Add(스타일);
				t.Styles[스타일].ForeColor = 색;
			}

			t.Rows[행].Style = t.Styles[스타일];
		}

		public static void 행글자색(this FlexGrid t, int 행, string 색)
		{
			string 스타일 = "ROW_FORECOLOR_" + 색.ToUpper();

			if (!t.Styles.Contains(스타일))
			{
				t.Styles.Add(스타일);
				t.Styles[스타일].ForeColor = 색 == "" ? Color.Black : ColorTranslator.FromHtml(색);
			}

			t.Rows[행].Style = t.Styles[스타일];
		}





		public static void 행배경색_노랑(this FlexGrid t, int 행) => t.행배경색_노랑(행, true);
		public static void 행배경색_노랑(this FlexGrid t, int 행, bool 조건식) => t.행배경색(행, 조건식 ? Color.Yellow : SystemColors.Window);

		public static void 행배경색(this FlexGrid t, int 행, string 색) => t.행배경색(행, 색 == "" ? SystemColors.Window : ColorTranslator.FromHtml(색));
		public static void 행배경색(this FlexGrid t, int 행, Color 색)
		{
			string 스타일 = "ROW_BACKCOLOR_" + 색.Name.ToUpper();

			if (!t.Styles.Contains(스타일))
			{
				t.Styles.Add(스타일);
				t.Styles[스타일].BackColor = 색;
			}

			// 배경색은 스타일 자체를 없애버려야 alternative가 영향을 안받음
			if (색 == SystemColors.Window)
				t.Rows[행].Style = null;
			else
				t.Rows[행].Style = t.Styles[스타일];
		}






		static CellRange 범위_옛;

		public static void 시프트체크(this FlexGrid t)
		{
			t.KeyUp += FlexGrid_시프트체크_KeyUp;
			t.MouseClick += FlexGrid_시프트체크_MouseClick;
		}

		private static void FlexGrid_시프트체크_KeyUp(object sender, KeyEventArgs e)
		{
			// 시프트 떼면 초기화
			if (e.KeyCode == Keys.ShiftKey)			
				범위_옛.r1 = 0;			
		}

		private static void FlexGrid_시프트체크_MouseClick(object sender, MouseEventArgs e)
		{
			FlexGrid 그리드 = (FlexGrid)sender;

			if (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Shift)
			{
				CellRange 범위_신 = 그리드.Selection;

				// 저장된게 없으면 새거랑 맞춰줌
				if (범위_옛.r1 == 0)
					범위_옛 = 범위_신;

				// 체크
				그리드.그리기중지();

				for (int i = 수학.작은수(범위_옛.r1, 범위_신.r1); i <= 수학.큰수(범위_옛.r2, 범위_신.r2); i++)
					그리드[i, "CHK"] = i.비트윈(범위_신.r1, 범위_신.r2) ? "Y" : "N";

				그리드.그리기시작();

				// 저장
				범위_옛 = 범위_신;
			}
		}






		public static void 헤더더블클릭(this FlexGrid t, Action 헤더더블클릭메서드)
		{
			t.DoubleClick += delegate (object sender, EventArgs e) { FlexGrid_DoubleClick(sender, e, 헤더더블클릭메서드); };
		}

		private static void FlexGrid_DoubleClick(object sender, EventArgs e, Action 헤더더블클릭메서드)
		{
			FlexGrid 그리드 = (FlexGrid)sender;

			if (그리드.헤더클릭())
				헤더더블클릭메서드();
		}

		public static void 복사붙여넣기(this FlexGrid t, Action<object, EventArgs> 행추가메서드, Action<object, RowColEventArgs> 행편집메서드)
		{
			t.KeyDown += delegate (object sender, KeyEventArgs e) { FlexGrid_KeyDown(sender, e, 행추가메서드, 행편집메서드); };
		}

		private static void FlexGrid_KeyDown(object sender, KeyEventArgs e, Action<object, EventArgs> 행추가메서드, Action<object, RowColEventArgs> 행편집메서드)
		{
			FlexGrid 그리드 = (FlexGrid)sender;

			if (e.KeyData == (Keys.Control | Keys.V))
			{
				그리드.Redraw = false;

				// 시작
				string[,] 클립보드 = 유틸.클립보드();
				int 시작행 = 그리드.Row;
				int 시작열 = 그리드.Col;

				for (int i = 0; i < 클립보드.GetLength(0); i++)
				{
					int 현재행 = 시작행 + i;

					// 마지막행을 넘어가면 행 추가
					if (현재행 > 그리드.Rows.Count - 1)
					{
						if (행추가메서드 == null)
							그리드.행추가();
						else
							행추가메서드?.Invoke(sender, null);	// 그리드를 일단 던지자, null로 하느니 그리드를 던져서 쓸데 있으면 쓰자
					}

					// 셀에 값 넣기
					for (int j = 0, 현재열 = 시작열; j < 클립보드.GetLength(1); j++, 현재열++)
					{
						// 보이기 컬럼일때까지 스캔	(안보이는 컬럼이므로 클립보드 인덱스와는 무관)
						for (; 현재열 < 그리드.Cols.Count; 현재열++)
						{
							if (그리드.Cols[현재열].Visible)
								break;
						}

						//  에딧 가능 컬럼이 아닐 경우 스킵
						if (!그리드.Cols[현재열].AllowEditing)
							continue;

						// 마지막 컬럼을 넘어가면 스톱
						if (현재열 > 그리드.Cols.Count)
							break;

						그리드[현재행, 현재열] = 클립보드[i, j] == "" ? (object)DBNull.Value : 클립보드[i, j];

						// 셀에 값 넣을때마다 이벤트 실행
						RowColEventArgs 이벤트 = new RowColEventArgs(현재행, 현재열);
						행편집메서드?.Invoke(그리드, 이벤트);
					}
				}

				그리드.Redraw = true;
				그리드.행추가완료();
			}
		}













		public static void 복사붙여넣기(this FlexGrid t, Action<object, RowColEventArgs> 실행메서드, params string[] 신규행필수컬럼)
		{
			t.KeyDown += delegate (object sender, KeyEventArgs e) { FlexGrid_KeyDown(sender, e, 실행메서드, 신규행필수컬럼); };
		}

		private static void FlexGrid_KeyDown(object sender, KeyEventArgs e, Action<object, RowColEventArgs> 실행메서드, string[] 신규행필수컬럼)
		{
			FlexGrid 그리드 = (FlexGrid)sender;

			if (e.KeyData == (Keys.Control | Keys.V))
			{				
				그리드.Redraw = false;

				// 시작
				string[,] 클립보드 = 유틸.클립보드();
				int 시작행 = 그리드.Row;
				int 시작열 = 그리드.Col;

				for (int i = 0; i < 클립보드.GetLength(0); i++)
				{
					int 현재행 = 시작행 + i;

					// 마지막행을 넘어가면 행 추가
					if (현재행 > 그리드.Rows.Count - 1)
					{
						그리드.행추가();

						// 신규행 필수컬럼은 시작행에 있는걸 복사해서 넣어줌 (자식 그리드의 경우 필터조건에 있는 값 안넣어주면 추가하자마자 없어짐)
						foreach (string s in 신규행필수컬럼)
							그리드[현재행, s] = 그리드[시작행, s];

						// 신규행 필수컬럼이 없으면 PK 있는걸로 넣어줌
						if (신규행필수컬럼.Length == 0)
						{
							string[] 자동증가컬럼 = { "SEQ", "NO_LINE" };

							foreach (string s in 그리드.기본키())
							{
								// 자동증가컬럼이면 자동증가, 나머지는 그냥 복사
								if (s.포함(자동증가컬럼))
									그리드[s] = (int)그리드.Aggregate(AggregateEnum.Max, s) + 1;
								else
									그리드[현재행, s] = 그리드[시작행, s];
							}
						}
					}

					// 셀에 값 넣기
					for (int j = 0, 현재열 = 시작열; j < 클립보드.GetLength(1); j++, 현재열++)
					{
						// 보이기 컬럼일때까지 스캔	(안보이는 컬럼이므로 클립보드 인덱스와는 무관)
						for (; 현재열 < 그리드.Cols.Count; 현재열++)
						{
							if (그리드.Cols[현재열].Visible)
								break;
						}

						//  에딧 가능 컬럼이 아닐 경우 스킵
						if (!그리드.Cols[현재열].AllowEditing)
							continue;

						// 마지막 컬럼을 넘어가면 스톱
						if (현재열 > 그리드.Cols.Count)
							break;

						그리드[현재행, 현재열] = 클립보드[i, j] == "" ? (object)DBNull.Value : 클립보드[i, j];

						// 사용자 지정 함수
						RowColEventArgs 이벤트 = new RowColEventArgs(현재행, 현재열);
						실행메서드?.Invoke(그리드, 이벤트);
					}
				}
				
				그리드.Redraw = true;
			}
		}

		public static int 최대값(this FlexGrid t, string 컬럼명) => (int)t.Aggregate(AggregateEnum.Max, 컬럼명);

		public static string 컬럼이름(this FlexGrid t) => t.Cols[t.Col].Name;

		public static string 컬럼이름(this FlexGrid t, int 인덱스) => t.Cols[인덱스].Name;

		public static string 컬럼이름(this FlexGrid t, RowColEventArgs 이벤트) => t.Cols[이벤트.Col].Name;

		public static string 컬럼이름(this FlexGrid t, ValidateEditEventArgs 이벤트) => t.Cols[이벤트.Col].Name;

		public static int 컬럼인덱스(this FlexGrid t, string 컬럼이름) => t.Cols[컬럼이름].Index;

		public static void 컬럼값변경(this FlexGrid t, string 컬럼이름, object 값)
		{
			for (int i = t.Rows.Fixed; i < t.Rows.Count; i++)
				t[i, 컬럼이름] = 값;
		}

		public static T 이벤트<T>(this FlexGrid t, int 행, string 컬럼이름)
		{
			if (typeof(T) == typeof(RowColEventArgs))
			{			
				return (T)Activator.CreateInstance(typeof(T), 행, t.Cols[컬럼이름].Index);
			}

			return default;
		}


		public static void 컬럼삭제(this FlexGrid t, string 컬럼이름)
		{
			if (t.Cols.Contains(컬럼이름))
				t.Cols.Remove(컬럼이름);
		}



		public static void 수정완료(this FlexGrid t) => t.AcceptChanges();

		public static void 커밋(this FlexGrid t) => t.AcceptChanges();

		public static DataTable 데이터테이블(this FlexGrid t) => t.DataTable;

		public static DataTable 데이터테이블(this FlexGrid t, string 필터) => t.DataTable.선택(필터).데이터테이블() ?? t.DataTable?.Clone();

		public static DataTable 데이터테이블_선택(this FlexGrid t) => t.GetCheckedRows("CHK") ?? t.DataTable?.Clone();

		public static DataTable 데이터테이블_수정(this FlexGrid t) => t.GetChanges() ?? t.DataTable?.Clone();

		public static DataTable 데이터테이블_화면(this FlexGrid t) => t.GetTableFromGrid();

		public static DataRow 데이터행(this FlexGrid t) => t.GetDataRow(t.Row);




		public static DataTable 수정데이터테이블(this FlexGrid t) => t.GetChanges() ?? t.DataTable?.Clone();

		public static DataTable 삭제데이터테이블(this FlexGrid t) => new DataView(t.DataTable, null, null, DataViewRowState.Deleted).ToTable();

		public static DataTable 표시데이터테이블(this FlexGrid t) => t.GetTableFromGrid();


		public static DataRow 선택데이터행(this FlexGrid t) => t.GetDataRow(t.Row);



		static int 그리드선택행;
		static int 그리드선택열;

		public static void 포커스저장(this FlexGrid t)
		{
			그리드선택행 = t.Row;
			그리드선택열 = t.Col;

		}

		public static void 포커스조회(this FlexGrid t)
		{
			t.Row = 그리드선택행;
			t.Col = 그리드선택열;
			t.Focus();
		}


		#endregion


		#region ==================================================================================================== 비트윈

		public static bool 비트윈(this int i, int a, int b) => i >= a && i <= b;


		#endregion

		#region ==================================================================================================== 문자열
		public static string[] 분할(this string t, string 분할자) => t.분할(분할자, 1);

		public static string[] 분할(this string t, string 분할자, int 최소길이) => t.Split(new[] { 분할자 }, StringSplitOptions.None).Select(x => x.Trim()).Where(x => x != "" && x.Length >= 최소길이).ToArray();
		
		public static string 대문자(this string t) => t.ToUpper();

		public static string 삭제(this string t, string 삭제문자) => t.Replace(삭제문자, "");

		public static string 제거(this string t, params string[] 제거문자)
		{
			foreach (string s in 제거문자) t = t.Replace(s, "");
			return t;
		}

		public static string 소문자(this string t) => t.ToLower();

		public static string 트림(this string t) => t.Trim();

		public static string 왼쪽(this string t, int 길이) => t.Length < 길이 ? t : t.Substring(0, 길이);

		public static string 오른쪽(this string t, int 길이) => t.Length < 길이 ? t : t.Substring(t.Length - 길이);

		public static string 단일공백(this string t) => Regex.Replace(t, @"\s+", " ");

		public static string 괄호내용제거(this string t) => Regex.Replace(t, @"\((.*?)\)", "");

		public static string 특수문자제거(this string t) => Regex.Replace(t, "[^A-z가-힣] ", "");

		public static bool 발생(this string t, params string[] 아이템) => 아이템.Any(x => t.Contains(x));

		public static bool 시작(this string t, params string[] 아이템) => 아이템.Any(x => t.StartsWith(x));

		public static bool 종료(this string t, params string[] 아이템) => 아이템.Any(x => t.EndsWith(x));

		public static string 웹(this string t) => t == "" ? "<br style='visibility:hidden'>" : t.Replace("\r\n", "<br />").Replace("\n", "<br />");

		public static T 마지막<T>(this T[] t) => t.Last();

		public static T 마지막<T>(this IList<T> t) => t.Last();

		public static bool 발생<T>(this IEnumerable<T> t, T 값) => t.Contains(값);



		public static string 바꾸기(this string t, string 이전값, string 새값) => t.Replace(이전값, 새값);

		public static int 인덱스(this string t, string value) => t.IndexOf(value);


		public static int 인덱스(this string t, string value, int n)
		{
			Match m = Regex.Match(t, "((" + Regex.Escape(value) + ").*?){" + n + "}");

			if (m.Success)
				return m.Groups[2].Captures[n - 1].Index;
			else
				return -1;
		}

		#endregion


		#region ==================================================================================================== 이미지

		public static void 방향표준화(this Image image)
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

		public static string 제조사(this Image image)
		{
			int ExifOrientationTagId = 0x010F;

			if (Array.IndexOf(image.PropertyIdList, ExifOrientationTagId) > -1)
				return Encoding.Default.GetString(image.GetPropertyItem(ExifOrientationTagId).Value);
			else
				return "";
		}


		public static void 회전_90도(this Image image)
		{
			image.RotateFlip(RotateFlipType.Rotate90FlipNone);
		}

		public static void 회전_180도(this Image image)
		{
			image.RotateFlip(RotateFlipType.Rotate180FlipNone);
		}

		public static void 회전_270도(this Image image)
		{
			image.RotateFlip(RotateFlipType.Rotate270FlipNone);
		}

		public static void 뒤집기_가로(this Image image)
		{
			image.RotateFlip(RotateFlipType.RotateNoneFlipX);
		}

		public static void 뒤집기_세로(this Image image)
		{
			image.RotateFlip(RotateFlipType.RotateNoneFlipY);
		}

		#endregion

		#region ==================================================================================================== 데이터테이블

		public static DataRow[] 선택(this DataTable t, string 필터) => t.Select(필터.Replace("!=", "<>"));

		public static DataRow[] 선택(this DataTable t, string 필터, string 정렬) => t.Select(필터.Replace("!=", "<>"), 정렬);

		public static void 삭제(this DataTable t, string 필터)
		{
			foreach (DataRow row in t.선택(필터))
				row.Delete();			
		}

		public static void 삭제(this IEnumerable<DataRow> t)
		{
			foreach (var row in t)
				row.Delete();
		}

		public static DataTable 중복제거(this DataTable t) => t.DefaultView.ToTable(true);

		public static DataTable 중복제거(this DataTable t, params string[] 컬럼이름) => t.DefaultView.ToTable(true, 컬럼이름);

		public static object 계산(this DataTable t, string 계산식) => t.Compute(계산식, "");

		public static object 계산(this DataTable t, string 계산식, string 필터) => t.Compute(계산식, 필터);


		

		#endregion


		#region ==================================================================================================== 형변환

		public static string String(this object t)
		{
			return t == null ? "" : t.ToString();
		}

		public static string String(this object t, string 포맷)
		{
			return string.Format("{0:" + 포맷 + "}", t);
		}

		public static string 문자(this object t)
		{
			return t == null ? "" : t.ToString();
		}

		public static string 문자(this object t, string 포맷)
		{
			if (t.문자() == "")
				return "";
			else if (포맷.발생("yyyy", "MM", "dd"))
				return string.Format("{0:" + 포맷 + "}", t.날짜());
			else
				return string.Format("{0:" + 포맷 + "}", t);
		}

		public static DateTime 날짜(this object t)
		{
			// 8글자를 변환
			string s = t.ToString();
			if (s.Length == 8) s = s.Substring(0, 4) + "-" + s.Substring(4, 2) + "-" + s.Substring(6, 2);
			return Convert.ToDateTime(s);
		}

		public static decimal 실수(this object t)
		{
			if (t == null)
				return 0;

			if (decimal.TryParse(t.ToString(), out decimal d))
				return d;
			else
				return 0;
		}

		public static int 정수(this object t)
		{
			if (t == null)
				return 0;

			if (int.TryParse(t.ToString().Split('.')[0], out int i))
				return i;
			else
				return 0;
		}

		public static bool 정수여부(this object t) => int.TryParse(t.문자(), out _);
		
		public static DataTable 데이터테이블(this DataRow[] t)
		{
			if (t.Length > 0)
				return t.CopyToDataTable();
			else
				return null;
		}


		public static DataTable DataTable(this DataRow[] t)
		{
			if (t.Length > 0)
				return t.CopyToDataTable();
			else
				return null;
		}

		public static DataTable DataTable(this DataRow[] t, bool 중복제거, params string[] 컬럼명)
		{
			if (t.Length > 0)
				return t.DataTable().DataTable(중복제거, 컬럼명);
			else
				return null;
		}

		public static DataTable DataTable(this DataTable t, bool 중복제거, params string[] 컬럼명)
		{
			return new DataView(t).ToTable(중복제거, 컬럼명);
		}


		public static DataTable 데이터테이블(this DataTable t, bool 중복제거, params string[] 컬럼이름) => new DataView(t).ToTable(중복제거, 컬럼이름);


		public static T[] 배열<T>(this DataTable t, string 열이름) => t?.AsEnumerable().Select(r => r.Field<T>(열이름)).ToArray();

		public static T[] 배열<T>(this DataTable t, int 컬럼인덱스) => t?.AsEnumerable().Select(r => r.Field<T>(컬럼인덱스)).ToArray();

		
		
		public static string 결합(this string[] t, string 구분자) => t == null ? "" : string.Join(구분자, t);

		public static string 결합_따옴표(this string[] t, string 구분자) => t == null ? "" : string.Join(구분자, t.AsEnumerable().Select(x => "'" + x + "'"));

		public static string 결합(this DataTable t, string 구분자, string 컬럼이름) => string.Join(구분자, t.AsEnumerable().Select(r => r.Field<string>(컬럼이름)));

		public static string 결합_따옴표(this DataTable t, string 구분자, string 컬럼이름) => string.Join(구분자, t.AsEnumerable().Select(x => "'" + x[컬럼이름] + "'"));

		#endregion

		private static IEnumerable<Control> GetAll(Control control, Type type = null)
		{
			var controls = control.Controls.Cast<Control>();
			return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls).Where(c => c.GetType() == type);
		}


		public static bool 포함<T>(this T t, params T[] 아이템)
		{
			return new List<T>(아이템).Contains(t);
		}

		













		public static bool 엔터(this KeyEventArgs t)
		{

			return t.KeyData == Keys.Enter;
		}

		public static bool 엔터(this KeyPressEventArgs t)
		{
			return t.KeyChar == (char)13;
		}



		
		public static void 추가(this SqlParameterCollection t, string 이름, object 값)
		{
			if (값 == DBNull.Value)
			{
				t.Add(new SqlParameter(이름, 값));
			}			
			else if (값 != null)
			{
				// STRING 뿐만 아니라 INT 형 같은 애들이 ""로 들어오면 곤란하므로 ""때에는 아예 파라메타 추가를 안함
				string s = 값.ToString();

				if (s.Replace(" ", "") != "")
					t.Add(new SqlParameter(이름, 값));
			}
		}

		public static void 추가(this SqlParameterCollection t, string parameterName, object value, bool allowEmpty)
		{
			if (allowEmpty)
				t.Add(new SqlParameter(parameterName, value));
			else
				t.추가(parameterName, value);
		}

		public static void 추가(this SqlParameterCollection t, string parameterName, string typeName, DataTable value)
		{
			t.Add(new SqlParameter(parameterName, SqlDbType.Structured) { TypeName = typeName, Value = value });
		}

		public static void 추가(this SqlCommand t, object[] 변수)
		{
			SqlCommandBuilder.DeriveParameters(t);

			if (t.Parameters.Count > 0 && t.Parameters[0].ParameterName == "@RETURN_VALUE")
				t.Parameters.Remove(t.Parameters[0]);

			for (int i = 0; i < 변수.Length; i++)
				t.Parameters[i].Value = 변수[i];
		}

		public static void 열기(this SqlCommand t)
		{
			t.CommandTimeout = 300;
			t.CommandType = t.CommandText.대문자().트림().시작("SELECT", "INSERT", "UPDATE", "DELETE", "DECLARE") ? CommandType.Text : CommandType.StoredProcedure;
			t.Connection.Open();
		}

		public static void 열기(this SqlCommand t, object[] 변수)
		{			
			t.CommandTimeout = 300;
			t.CommandType = t.CommandText.대문자().트림().시작("SELECT", "INSERT", "UPDATE", "DELETE", "DECLARE") ? CommandType.Text : CommandType.StoredProcedure;
			t.Connection.Open();

			// 파라미터 추가
			if (t.CommandType == CommandType.StoredProcedure)
			{
				SqlCommandBuilder.DeriveParameters(t);

				if (t.Parameters.Count > 0 && t.Parameters[0].ParameterName == "@RETURN_VALUE")
					t.Parameters.Remove(t.Parameters[0]);

				for (int i = 0; i < 변수.Length; i++)
					t.Parameters[i].Value = 변수[i];
			}
		}



		public static void 열기(this SqlCommand t, string 쿼리, object[] 변수)
		{			
			t.CommandTimeout = 300;
			t.CommandText = 쿼리;
			t.CommandType = t.CommandText.대문자().트림().시작("SELECT", "INSERT", "UPDATE", "DELETE", "DECLARE") ? CommandType.Text : CommandType.StoredProcedure;
			t.Connection = new SqlConnection(상수.디비커넥션_ERP);
			t.Connection.Open();

			// 파라미터 추가
			if (t.CommandType == CommandType.StoredProcedure)
			{
				SqlCommandBuilder.DeriveParameters(t);

				if (t.Parameters.Count > 0 && t.Parameters[0].ParameterName == "@RETURN_VALUE")
					t.Parameters.Remove(t.Parameters[0]);

				for (int i = 0; i < 변수.Length; i++)
					t.Parameters[i].Value = 변수[i];
			}
		}








		public static void 닫기(this SqlCommand t)
		{
			t.Connection.Close();
		}

		public static void 디버그(this SqlCommand cmd, 디버그모드 디버그모드)
		{
			string s = "";

			foreach (SqlParameter p in cmd.Parameters)
			{
				if (p.Value == DBNull.Value || p.Value == null)
				{
					s += "\r\n," + p.ParameterName + "=NULL";
				}
				else
				{
					if (p.ParameterName.StartsWith("@JSON"))
						s += "\r\n," + p.ParameterName + "='" + p.Value.ToString().Replace("'", "''").Replace("[{", "\r\n[{").Replace(",{", "\r\n,{") + "'";
					else
						s += "\r\n," + p.ParameterName + "='" + p.Value.ToString().Replace("'", "''") + "'";
				}
			}

			// 첫번째 콤마(,) 제거
			if (s != "")
				s = " " + s.Substring(3);

			s = cmd.CommandText + "\r\n" + s;

			if (디버그모드 == 디버그모드.출력)
			{
				Debug.WriteLine(s);
			}
			else if (디버그모드 == 디버그모드.팝업)
			{
				if (사원번호.포함("S-343", "S-391", "S-458"))
				{
					H_CZ_DEBUG_PRINT f = new H_CZ_DEBUG_PRINT(s);
					f.ShowDialog();
				}
			}
		}








		public static int NthIndexOf(this string t, string value, int n)
		{
			Match m = Regex.Match(t, "((" + Regex.Escape(value) + ").*?){" + n + "}");

			if (m.Success)
				return m.Groups[2].Captures[n - 1].Index;
			else
				return -1;
		}

		public static void SetBinding(this FlexGrid o, TableLayoutPanel tableLayoutPanel)
		{
			// 패널 바인딩
			List<PanelExt> panelList = new List<PanelExt>();

			foreach (Control c in tableLayoutPanel.Controls)
			{				
				if (c is PanelExt ext)
					panelList.Add(ext);
			}

			o.SetBinding(panelList.ToArray(), new object[] { });

			// 체크박스 바인딩 (체크박스는 Y,N 말고 다른 형태로는 안받는다고 생각함
			foreach (CheckBoxExt c in GetAll(tableLayoutPanel, typeof(CheckBoxExt)))
			{
				if (c.태그() != "")
					o.SetBindningCheckBox(c, "Y", "N");
			}
		}








		public static string 태그(this Control t) => t?.Tag == null ? "" : t.Tag.ToString();








		//public static void 숫자형식(this TextBoxExt o)
		//{
		//	o.TextAlign = HorizontalAlignment.Right;
		//	o.TextChanged += TextBox_TextChanged;
		//}

		//private static void TextBox_TextChanged(object sender, EventArgs e)
		//{
		//	TextBoxExt o = (TextBoxExt)sender;

		//	int caret = o.SelectionStart;
		//	int commaCntOld = o.Text.카운트(",");
		//	string value = Regex.Replace(o.Text, "[^0-9.]", "");
		//	decimal dec;

		//	if (decimal.TryParse(value.ToString(), out decimal d))
		//		dec = d;
		//	else
		//		dec = 0;

		//	o.Text = string.Format("{0:#,###.##}", dec);

		//	int commaCntNew = o.Text.카운트(",");
		//	o.SelectionStart = caret + (commaCntNew - commaCntOld);
		//}










		public static int 카운트(this string o, string str)
		{
			Regex cntStr = new Regex(str);
			int count = int.Parse(cntStr.Matches(o, 0).Count.ToString());

			return count;
		}


		public static string 합자일반화(this string o)
		{
			o = o.Replace("ﬂ", "fl");

			return o;
		}








		public static void 체크(this RadioButtonExt t) => t.Checked = true;


		

		public static RadioButton 선택라디오(this RadioButton t)
		{
			foreach (Control c in t.Parent.Controls)
			{
				if (c is RadioButton rdo && rdo.Checked)
					return rdo;
			}

			return null;
		}


		public static RadioButtonExt 선택(this RadioButtonExt o)
		{			
			foreach (Control con in o.Parent.Controls)
			{
				if (con is RadioButtonExt rdo && rdo.Checked)
					return rdo;
			}

			return null;
		}



		public static void 자동영문(this TextBoxExt t)
		{
			t.Leave += TextBox_Leave;
		}

		private static void TextBox_Leave(object sender, EventArgs e)
		{
			TextBoxExt tbx = (TextBoxExt)sender;
			tbx.Text = HANENG.한글을영어(tbx.Text);			
		}




		

		public static void 엔터검색(this TextBoxExt t) => t.KeyDown += TextBox_KeyDown;

		private static void TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			TextBoxExt o = (TextBoxExt)sender;

			if (e.KeyData == Keys.Enter)
			{
				if (o.Text.Trim() == "")
				{
					Global.MainFrame.ShowMessage("검색어를 입력하세요!");
					o.Focus();
				}
				else
				{
					if (o.GetContainerControl() is PageBase pb)
					{
						pb.OnToolBarSearchButtonClicked(null, null);
					}
					else if (o.GetContainerControl() is Duzon.Common.Forms.CommonDialog cd)
					{
						foreach (Button btn in cd.컨트롤<Button>())
						{
							if (btn.Name.포함("btn조회", "btn검색"))
								btn.PerformClick();
						}
					}
				}
			}
		}

		public static void 숫자만_정수(this TextBoxExt t)
		{			
			t.TextAlign = HorizontalAlignment.Right;
			t.KeyPress += TextBox_숫자만_정수_KeyPress;
			t.TextChanged += TextBox_숫자만_정수_TextChanged;
		}

		private static void TextBox_숫자만_정수_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!(char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
			{
				e.Handled = true;
			}
			if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
			{
				e.Handled = true;
			}
		}

		private static void TextBox_숫자만_정수_TextChanged(object sender, EventArgs e)
		{
			TextBoxExt tbx = (sender as TextBoxExt);

			// 빈칸이면 아무것도 안함
			if (tbx.Text == "")
				return;

			// 변환
			int 캐럿 = tbx.SelectionStart;
			int 콤마숫자_옛 = tbx.Text.Count(",");

			tbx.Text = string.Format("{0:#,##0}", Regex.Replace(tbx.Text, "[^0-9.]", "").ToDecimal().ToInt());

			int 콤마숫자_신 = tbx.Text.Count(",");
			tbx.SelectionStart = 캐럿 + (콤마숫자_신 - 콤마숫자_옛);
		}


		public static void 숫자만_실수(this TextBoxExt t)
		{
			t.TextAlign = HorizontalAlignment.Right;			
			t.KeyPress += TextBox_숫자만_실수_KeyPress;
			t.TextChanged += TextBox_숫자만_실수_TextChanged;
			t.LostFocus += TextBox_숫자만_실수_LostFocus;
		}

		private static void TextBox_숫자만_실수_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!(char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == '.' || e.KeyChar == '-'))
			{
				e.Handled = true;
			}
			if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
			{
				e.Handled = true;
			}			
		}

		private static void TextBox_숫자만_실수_TextChanged(object sender, EventArgs e)
		{
			TextBoxExt tbx = (sender as TextBoxExt);

			// 빈칸이면 아무것도 안함
			if (tbx.Text == "")
				return;

			// 마지막 글자가 .이라면 아직 입력중이므로 아무것도 안해도 됨
			if (tbx.Text.EndsWith("."))
				return;

			// 글하가 하나 있는데 -면 일단 보류 => 바로 포맷 먹이면 0으로 바뀌어버림
			if (tbx.Text == "-")
				return;

			// 변환
			int 캐럿 = tbx.SelectionStart;
			int 콤마숫자_옛 = tbx.Text.Count(",");

			tbx.Text = string.Format("{0:#,##0.##}", Regex.Replace(tbx.Text, "[^0-9.-]", "").ToDecimal());

			int 콤마숫자_신 = tbx.Text.Count(",");
			tbx.SelectionStart = 캐럿 + (콤마숫자_신 - 콤마숫자_옛);
		}

		private static void TextBox_숫자만_실수_LostFocus(object sender, EventArgs e)
		{
			TextBoxExt tbx = (sender as TextBoxExt);

			if (tbx.Text == "-")
				tbx.Text = "";
		}




		public static void 엔터검색(this TextBoxExt t, Button 실행버튼) => t.KeyDown += delegate (object sender, KeyEventArgs e) { TextBox_KeyDown(sender, e, 실행버튼); };

		private static void TextBox_KeyDown(object sender, KeyEventArgs e, Button 실행버튼)
		{
			TextBoxExt o = (TextBoxExt)sender;

			if (e.KeyData == Keys.Enter)
			{
				if (o.Text.Trim() == "")
					Global.MainFrame.ShowMessage("검색어를 입력하세요!");
				else
					실행버튼.PerformClick();

				o.Focus();
			}
		}





		public static void 검색어필수(this BpCodeTextBox t) => t.QueryBefore += BpCodeTextBox_QueryBefore;

		private static void BpCodeTextBox_QueryBefore(object sender, BpQueryArgs e)
		{
			if (e.HelpParam.P92_DETAIL_SEARCH_CODE == "")
				e.QueryCancel = true;
		}






















		#region ==================================================================================================== 초기화

		public static void 초기화(this BpCodeTextBox t) => t.초기화(false);

		public static void 초기화(this BpCodeTextBox t, bool 기본값, FreeBinding 헤더)
		{
			t.초기화(기본값);
			if (기본값) 헤더.CurrentRow[t.태그().분할(";")[0]] = t.CodeValue;
		}

		public static void 초기화(this BpCodeTextBox t, bool 기본값)
		{
			if (기본값 && t.태그().발생("NO_EMP"))
			{
				t.CodeValue = Global.MainFrame.LoginInfo.UserID;
				t.CodeName = Global.MainFrame.LoginInfo.UserName;
			}
			else if (기본값 && t.태그().발생("CD_SALEGRP"))
			{
				t.CodeValue = Global.MainFrame.LoginInfo.SalesGroupCode;
				t.CodeName = Global.MainFrame.LoginInfo.SalesGroupName;
			}
			else if (기본값 && t.태그().발생("CD_PURGRP"))
			{
				t.CodeValue = Global.MainFrame.LoginInfo.PurchaseGroupCode;
				t.CodeName = Global.MainFrame.LoginInfo.PurchaseGroupName;
			}
			else
			{
				t.CodeValue = "";
				t.CodeName = "";
			}
		}

		

		public static void 초기화(this CurrencyTextBox t) => t.DecimalValue = 0;

		public static void 초기화(this DatePicker t, bool 기본값) => t.Text = 기본값 ? 유틸.오늘() : "";

		public static void 초기화(this WebBrowser t) => t.바인딩("", "", false);

		public static void 초기화(this DropDownComboBox t)
		{
			if (t.DataSource != null)	((DataTable)t.DataSource).Clear();
			else if (t.Items.Count > 0)	t.Items.Clear();
		}

		public static void 초기화(this DropDownComboBox t, int 인덱스) => t.SelectedIndex = 인덱스;

		
		public static void 초기화(this FlexGrid t)
		{
			if (t.DataSource != null)
			{
				t.DataTable.Rows.Clear();
				t.AcceptChanges();
			}
		}


		public static void 초기화(this TextBox t) => t.Text = "";

		public static void 초기화(this TextBoxExt t) => t.Text = "";

		#endregion



		public static void 기본값(this DatePicker t) => t.Text = 유틸.오늘();

		public static void 기본값(this DropDownComboBox t) => t.SelectedIndex = 0;

		public static void 기본값(this BpCodeTextBox t)
		{
			if (t.태그().발생("NO_EMP"))
			{
				t.CodeValue = Global.MainFrame.LoginInfo.UserID;
				t.CodeName = Global.MainFrame.LoginInfo.UserName;
			}
			else if (t.태그().발생("CD_SALEGRP"))
			{
				t.CodeValue = Global.MainFrame.LoginInfo.SalesGroupCode;
				t.CodeName = Global.MainFrame.LoginInfo.SalesGroupName;
			}
			else if (t.태그().발생("CD_PURGRP"))
			{
				t.CodeValue = Global.MainFrame.LoginInfo.PurchaseGroupCode;
				t.CodeName = Global.MainFrame.LoginInfo.PurchaseGroupName;
			}
			else
			{
				t.CodeValue = "";
				t.CodeName = "";
			}
		}




		public static DataTable 데이터테이블(this BpComboBox t) => t.DataTable;


		#region ==================================================================================================== 클리어

		public static void 클리어(this BpComboBox t) => t.Clear();

		public static void 클리어(this BpCodeTextBox t)
		{
			t.클리어(false);
		}

		public static void 클리어(this BpCodeTextBox t, bool 기본값)
		{
			if (기본값 && t.태그().Contains("NO_EMP"))
			{
				// 담당자
				t.CodeValue = Global.MainFrame.LoginInfo.UserID;
				t.CodeName = Global.MainFrame.LoginInfo.UserName;
			}
			else if (기본값 && t.태그().Contains("CD_SALEGRP"))
			{
				// 영업그룹
				t.CodeValue = Global.MainFrame.LoginInfo.SalesGroupCode;
				t.CodeName = Global.MainFrame.LoginInfo.SalesGroupName;
			}
			else if (기본값 && t.태그().Contains("CD_PURGRP"))
			{
				// 구매그룹
				t.CodeValue = Global.MainFrame.LoginInfo.PurchaseGroupCode;
				t.CodeName = Global.MainFrame.LoginInfo.PurchaseGroupName;
			}
			else
			{
				t.CodeValue = "";
				t.CodeName = "";
			}
		}

		public static void 클리어(this ComboBox t)
		{
			if (t.DataSource != null)
				((DataTable)t.DataSource).Clear();
			else if (t.Items.Count > 0)
				t.Items.Clear();
		}

		public static void 클리어(this CurrencyTextBox t)
		{
			t.DecimalValue = 0;
		}

		public static void 클리어(this DatePicker t) => t.Text = "";


		public static void 클리어(this DatePicker t, bool 기본값)
		{
			if (기본값)
				t.Text = 유틸.오늘();
			else
				t.Text = "";
		}

		public static void 클리어(this DropDownComboBox t)
		{
			if (t.DataSource != null)
				((DataTable)t.DataSource).Clear();
			else if (t.Items.Count > 0)
				t.Items.Clear();
		}

		public static void 클리어(this DropDownComboBox t, int 인덱스)
		{
			t.SelectedIndex = 인덱스;
		}

		public static void 클리어(this FlexGrid t)
		{
			if (t.DataSource != null)
			{
				t.DataTable.Rows.Clear();
				t.AcceptChanges();
			}
		}

		public static void 클리어(this RadioButtonExt t)
		{
			t.라디오_디폴트().Checked = true;			
		}

		public static RadioButtonExt 라디오_디폴트(this RadioButtonExt t)// => t.Parent.태그();
		{
			// 라디오의 경우 디폴트값이 부모의 패널 태그에 들어있음
			string 부모태그 = t.Parent.태그();
			if (부모태그 == "") return null;

			Control[] 컨트롤s = t.Parent.Controls.Find(부모태그, false);

			if (컨트롤s.Length == 1)
				return (컨트롤s[0] as RadioButtonExt);
			else
				return null;
		}



		public static void 클리어(this TextBoxExt t) => t.Text = "";

		public static void 클리어(this WebBrowser t)
		{
			t.DocumentText = "";
			
		}


		public static void 클리어(this PanelExt t)
		{
			foreach (Control c in t.Controls)
			{
				if (c.태그() == "")
					continue;

				

				if		(c is BpComboBox		cbm) { cbm.클리어(); }
				else if	(c is BpCodeTextBox		ctx) { ctx.값(""); ctx.글(""); }
				else if (c is CheckBox			chk) { chk.Checked = true; }
				else if (c is ComboBox			cbo) { cbo.SelectedIndex = 0; }
				else if (c is CurrencyTextBox	cur) { cur.DecimalValue = 0; }
				else if (c is DatePicker		dtp) { dtp.Text = ""; }
				else if (c is DropDownComboBox	ddc) { ddc.SelectedIndex = 0; }
				else if (c is Label				lbl) { lbl.Text = ""; }
				else if (c is RadioButtonExt	rdo) { rdo.클리어(); }
				else if (c is TextBoxExt		tbx) { tbx.Text = ""; }
			}
		}


		public static void 클리어(this TableLayoutPanel t)
		{
			foreach (Control c in t.Controls)
			{
				if (c is PanelExt pnl) pnl.클리어();
			}			
		}








		#endregion


		public static DataRow 데이터행(this DataRow[] t, int 인덱스)
		{
			if (t.Length > 0)
				return t[인덱스];
			else
				return null;
		}



		public static DataTable 복사(this DataTable t, string 필터)
		{
			return t.복사(필터, "");
		}

		public static DataTable 복사(this DataTable t, string 필터, string 정렬)
		{
			DataRow[] rows = t.Select(필터, 정렬);

			if (rows.Length == 0)
				return t.Clone();
			else
				return rows.CopyToDataTable();
		}












		#region ==================================================================================================== Json

		public static string Json(this DataRow[] t) => t.데이터테이블().Json();

		public static string Json(this DataRow[] t, params string[] columnNames) => t.데이터테이블().Json(columnNames);

		/// <summary>
		/// Json에서 필터가 필요한 경우에만 설정, Json으로 변환 후 필터 리셋됨
		/// </summary>
		/// <param name="t"></param>
		/// <param name="필터"></param>
		public static void 필터(this DataTable t, string 필터) => t.DefaultView.RowFilter = 필터;


		public static string Json(this DataTable t)
		{
			return t.Json("");
		}

		public static string Json(this DataTable t, DataRowState 데이터행상태)
		{
			if (t == null) return "[]";

			DataTable dt = t.Copy();
			dt.AcceptChanges();

			// 상태변경
			if		(데이터행상태 == DataRowState.Added)		foreach (DataRow row in dt.Rows) row.SetAdded();		// 추가
			else if (데이터행상태 == DataRowState.Modified)	foreach (DataRow row in dt.Rows) row.SetModified();		// 수정
			else if (데이터행상태 == DataRowState.Deleted)	foreach (DataRow row in dt.Rows) row.Delete();			// 삭제

			return dt.Json("");
		}

		public static string Json(this DataTable t, params string[] columnNames)
		{
			if (t == null) return "[]";

			string 필터 = t.DefaultView.RowFilter;

			// JSON_FLAG 값 만들기 => 이 방법을 안쓰면 deleted 된 행에 값을 쓸 수가 없음 (JSON_FLAG 값 쓰기 불가)
			DataTable dt = columnNames[0] == "" ? t.Clone() : new DataView(t.Clone()).ToTable(false, columnNames);
			DataTable dtD;
			DataTable dtI;
			DataTable dtU;
			DataTable dtO;

			if (columnNames[0] == "")
			{
				dtD = new DataView(t, 필터, null, DataViewRowState.Deleted).ToTable();
				dtI = new DataView(t, 필터, null, DataViewRowState.Added).ToTable();
				dtU = new DataView(t, 필터, null, DataViewRowState.ModifiedCurrent).ToTable();
				dtO = new DataView(t, 필터, null, DataViewRowState.Unchanged).ToTable();
			}
			else
			{
				dtD = new DataView(t, 필터, null, DataViewRowState.Deleted).ToTable(false, columnNames);
				dtI = new DataView(t, 필터, null, DataViewRowState.Added).ToTable(false, columnNames);
				dtU = new DataView(t, 필터, null, DataViewRowState.ModifiedCurrent).ToTable(false, columnNames);
				dtO = new DataView(t, 필터, null, DataViewRowState.Unchanged).ToTable(false, columnNames);
			}

			dt.Columns.Add("JSON_FLAG", typeof(string));
			dtD.Columns.Add("JSON_FLAG", typeof(string), "'D'");
			dtI.Columns.Add("JSON_FLAG", typeof(string), "'I'");
			dtU.Columns.Add("JSON_FLAG", typeof(string), "'U'");
			dtO.Columns.Add("JSON_FLAG", typeof(string), "'O'");

			dt.Merge(dtD);
			dt.Merge(dtI);
			dt.Merge(dtU);
			dt.Merge(dtO);

			// 제어문자 삭제			
			//for (int j = 0; j < dt.Columns.Count; j++)
			//{
			//	if (dt.Columns[j].DataType == typeof(string))
			//	{
			//		for (int i = 0; i < dt.Rows.Count; i++)
			//		{
			//			Regex regex = new Regex("[\x00-\x1F\x7F]");
			//			if (regex.IsMatch(dt.Rows[i][j].ToString()))
			//				dt.Rows[i][j] = regex.Replace(dt.Rows[i][j].ToString(), "");
			//		}
			//	}
			//}			

			// 컬럼 자동 추가
			if (dt.Rows.Count > 0)
			{
				// 필요없는 컬럼 삭제
				if (dt.Columns.Contains("_IS_DETAILGET"))
					dt.Columns.Remove("_IS_DETAILGET");

				// 필수 컬럼 추가
				if (!dt.Columns.Contains("CD_COMPANY"))	dt.Columns.Add("CD_COMPANY", typeof(string), "'" + 회사코드 + "'").SetOrdinal(0);
				if (!dt.Columns.Contains("CD_BIZAREA"))	dt.Columns.Add("CD_BIZAREA", typeof(string), "'" + 사업장코드 + "'").SetOrdinal(dt.Columns.Count - 2);
				if (!dt.Columns.Contains("CD_PLANT"))	dt.Columns.Add("CD_PLANT", typeof(string), "'" + 공장코드 + "'").SetOrdinal(dt.Columns.Count - 2);
				if (!dt.Columns.Contains("ID_USER"))	dt.Columns.Add("ID_USER", typeof(string), "'" + 사원번호 + "'").SetOrdinal(dt.Columns.Count - 2);
			}

			t.DefaultView.RowFilter = "";

			return JsonConvert.SerializeObject(dt);
		}

		#endregion













		private static readonly char[] IniC = { 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
		private static readonly string[] IniS = { "ㄱ", "ㄲ", "ㄴ", "ㄷ", "ㄸ", "ㄹ", "ㅁ", "ㅂ", "ㅃ", "ㅅ", "ㅆ", "ㅇ", "ㅈ", "ㅉ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };

		private static readonly char[] VolC = { 'ㅏ', 'ㅐ', 'ㅑ', 'ㅒ', 'ㅓ', 'ㅔ', 'ㅕ', 'ㅖ', 'ㅗ', 'ㅘ', 'ㅙ', 'ㅚ', 'ㅛ', 'ㅜ', 'ㅝ', 'ㅞ', 'ㅟ', 'ㅠ', 'ㅡ', 'ㅢ', 'ㅣ' };
		private static readonly string[] VolS = { "ㅏ", "ㅐ", "ㅑ", "ㅒ", "ㅓ", "ㅔ", "ㅕ", "ㅖ", "ㅗ", "ㅘ", "ㅙ", "ㅚ", "ㅛ", "ㅜ", "ㅝ", "ㅞ", "ㅟ", "ㅠ", "ㅡ", "ㅢ", "ㅣ" };

		private static readonly char[] UndC = { '\0', 'ㄱ', 'ㄲ', 'ㄳ', 'ㄴ', 'ㄵ', 'ㄶ', 'ㄷ', 'ㄹ', 'ㄺ', 'ㄻ', 'ㄼ', 'ㄽ', 'ㄾ', 'ㄿ', 'ㅀ', 'ㅁ', 'ㅂ', 'ㅄ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
		private static readonly string[] UndS = { "", "ㄱ", "ㄲ", "ㄳ", "ㄴ", "ㄵ", "ㄶ", "ㄷ", "ㄹ", "ㄺ", "ㄻ", "ㄼ", "ㄽ", "ㄾ", "ㄿ", "ㅀ", "ㅁ", "ㅂ", "ㅄ", "ㅅ", "ㅆ", "ㅇ", "ㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };

		private static readonly string[] Table =
		{
			"ㄱ", "r", "ㄲ", "R",  "ㄳ", "rt",
			"ㄴ", "s", "ㄵ", "sw", "ㄶ", "sg",
			"ㄷ", "e", "ㄸ", "E",
			"ㄹ", "f", "ㄺ", "fr", "ㄻ", "fa", "ㄼ", "fq", "ㄽ", "ft", "ㄾ", "fx", "ㄿ", "fv", "ㅀ", "fg",
			"ㅁ", "a",
			"ㅂ", "q", "ㅃ", "Q",  "ㅄ", "qt",
			"ㅅ", "t", "ㅆ", "T",
			"ㅇ", "d",
			"ㅈ", "w",
			"ㅉ", "W",
			"ㅊ", "c",
			"ㅋ", "z",
			"ㅌ", "x",
			"ㅍ", "v",
			"ㅎ", "g",

			"ㅏ", "k",
			"ㅐ", "o", "ㅒ", "O",
			"ㅑ", "i",
			"ㅓ", "j",
			"ㅔ", "p", "ㅖ", "P",
			"ㅕ", "u",
			"ㅗ", "h", "ㅘ", "hk", "ㅙ", "ho", "ㅚ", "hl",
			"ㅛ", "y",
			"ㅜ", "n", "ㅝ", "nj", "ㅞ", "np", "ㅟ", "nl",
			"ㅠ", "b",
			"ㅣ", "l",
			"ㅡ", "m", "ㅢ", "ml",
		};


		private static char GetKor(string src, int index, int type, out int len, bool onlyOne = false)
		{
			len = 0;
			if (index >= src.Length) return '\0';

			int i = -1;

			if (type != 0 && !onlyOne && index + 1 < src.Length)
			{
				i = Array.IndexOf<string>(Table, new string(new char[] { src[index], src[index + 1] }));
				len = 2;
			}

			if (i == -1)
			{
				i = Array.IndexOf<string>(Table, src[index].ToString());
				len = 1;
			}

			var c = i >= 0 ? Table[i - 1][0] : '\0';

			if (type == 0) return Array.IndexOf<char>(IniC, c) >= 0 ? c : '\0';
			if (type == 1) return Array.IndexOf<char>(VolC, c) >= 0 ? c : '\0';
			if (type == 2) return Array.IndexOf<char>(UndC, c) >= 0 ? c : '\0';

			len = 0;

			return '\0';
		}

		private static bool Split(char src, out int ini, out int vow, out int und)
		{
			// 원래 초중종 나눔
			int charCode = Convert.ToInt32(src) - 44032;
			int i;

			if ((charCode < 0) || (charCode > 11171))
			{
				ini = vow = und = -1;

				if ((i = Array.IndexOf<char>(IniC, src)) != -1)
					ini = i;
				else if ((i = Array.IndexOf<char>(VolC, src)) != -1)
					vow = i;
				else if (src != '\0' && (i = Array.IndexOf<char>(UndC, src)) != -1)
					und = i;
			}
			else
			{
				ini = charCode / 588;
				vow = (charCode % 588) / 28;
				und = (charCode % 588) % 28;
			}

			return ini != -1 || vow != -1 || und != -1;
		}

		private static char Combine(char ini, char vow, char und = '\0')
		{
			// 조합
			int i = 44032 + Array.IndexOf<char>(IniC, ini) * 588;

			if (vow != '\0') i += Array.IndexOf<char>(VolC, vow) * 28;
			if (und != '\0') i += Array.IndexOf<char>(UndC, und);

			return Convert.ToChar(i);
		}

		/// <summary>
		/// gksrmfdl dkscuwudy -> 한글이 안쳐져요
		/// </summary>
		/// <param name="eng">변환할 문자열입니다.</param>
		/// <param name="detectCase">대문자가 더 많은 경우에 대소문자를 반대로 바꿔서 변환합니다</param>
		/// <returns>변환된 결과값입니다</returns>
		/// <exception cref="ArgumentNullException">eng 가 Null 일때 발생합니다</exception>
		/// <exception cref="ArgumentException">eng 의 길이가 0 일때 발생합니다</exception>
		public static string 영어를한글(this string eng, bool detectCase = false)
		{
			if (eng == null) throw new ArgumentNullException();
			if (eng.Length == 0) throw new ArgumentException();

			int index = 0;
			var b = new StringBuilder(eng.Length);

			// 대문자가 더 많으면 대소문자 반대로 바꿈
			if (detectCase)
			{
				b.Append(eng);

				int low = 0, up = 0;

				for (index = 0; index < b.Length; ++index)
				{
					if (char.IsUpper(b[index])) up++;
					else if (char.IsLower(b[index])) low++;
				}

				if (up > low)
				{
					for (index = 0; index < b.Length; ++index)
					{
						if (char.IsUpper(b[index])) b[index] = char.ToLower(b[index]);
						else if (char.IsLower(b[index])) b[index] = char.ToUpper(b[index]);
					}

					eng = b.ToString();
					b.Remove(0, b.Length);
				}
			}

			char ini, vow, und;
			int len, len2;

			index = 0;

			while (index < eng.Length)
			{
				ini = vow = und = '\0';

				////////////////////////////////////////////////// 초성
				ini = GetKor(eng, index, 0, out len);

				// 초성이 아니면
				if (ini == '\0')
				{
					// 자음이 아니면 모음이냐?
					vow = GetKor(eng, index, 1, out len);

					// 모음도 아니네 :3
					if (vow == '\0')
					{
						b.Append(eng[index]);
						index++;
						continue;
					}

					// 모음 맞네!!!
					b.Append(vow);
					index += len;
					continue;
				}

				// 모음 다음에 모음이면... 조합 모음?
				if (GetKor(eng, index + 1, 0, out len2) != '\0')
				{
					// 근데 자자모 순서대로면 조합 모음이 아니라 단순한 모음이니까
					// ㄱㄱㅏ -> ㄱ가
					if (GetKor(eng, index + 2, 1, out len2) != '\0')
					{
						b.Append(ini);
						index += len;
						continue;
					}

					// 조합 모음이 맞는지 확인
					und = GetKor(eng, index, 2, out len2);

					if (len2 == 2)
					{
						// 조합 모음이 맞네
						b.Append(und);
						index += len2;
						continue;
					}

					// 시무룩. 조합모음이 아니였다.
					// 집어넣고 다음 기회를 노리자
					else
					{
						b.Append(ini);
						index += len;
						continue;
					}
				}

				// 초성 길이만큼 이동. 어처피 한글자임
				index += 1;

				////////////////////////////////////////////////// 중성
				vow = GetKor(eng, index, 1, out len);

				// 중성이 아니면 초성만 넣고 스킵
				if (vow == '\0')
				{
					b.Append(ini);
					continue;
				}

				// 중성 길이만큼 이동
				index += len;

				////////////////////////////////////////////////// 종성
				und = GetKor(eng, index, 2, out len);

				// 종성이 아니면 조합해서 넣고 다음으로.
				if (und == '\0')
				{
					b.Append(Combine(ini, vow));
					continue;
				}

				// 자음 뒤에 모음이 나오는 경우 대비
				// 예) 각시 ㄱㅏㄱㅅㅣ => 갃X
				if (len == 2)
				{
					// 자모자자자 순서면 이게 조합 모음이 맞음.
					if (GetKor(eng, index + 2, 0, out len2) != '\0')
					{
						b.Append(Combine(ini, vow, und));
						index += len;
						continue;
					}

					// 어이쿠 조합 모음이 아니라 그냥 모음이였네요.
					und = GetKor(eng, index, 2, out len, true);
					b.Append(Combine(ini, vow, und));
					index += len;
					continue;
				}

				// 가시 = ㄱㅏㅅㅣ ->갓X
				else
				{
					// 다음에 모음이 나오니까 이건 종성이 아님.
					if (GetKor(eng, index + 1, 1, out len2) != '\0')
					{
						b.Append(Combine(ini, vow));
						continue;
					}

					// 이게 종성이 맞음.
					b.Append(Combine(ini, vow, und));
					index += len;
					continue;
				}
			}

			return b.ToString();
		}

		/// <summary>
		/// 한글이 안쳐져요 -> gksrmfdl dkscuwudy
		/// </summary>
		/// <param name="eng">변환할 문자열입니다.</param>
		/// <returns>변환된 결과값입니다</returns>
		/// <exception cref="ArgumentNullException">kor 가 Null 일때 발생합니다</exception>
		/// <exception cref="ArgumentException">kor 의 길이가 0 일때 발생합니다</exception>
		public static string 한글을영어(this string kor)
		{
			if (kor == null || kor == "")
				return "";

			kor = kor.Trim();

			var sb = new StringBuilder(kor.Length * 2);
			int ini, vow, und;
			int i = 0;

			do
			{
				if (!Split(kor[i], out ini, out vow, out und))
					sb.Append(kor[i]);
				else
				{
					if (ini != -1) sb.Append(Table[Array.IndexOf<string>(Table, IniS[ini]) + 1]);
					if (vow != -1) sb.Append(Table[Array.IndexOf<string>(Table, VolS[vow]) + 1]);
					if (und > 0) sb.Append(Table[Array.IndexOf<string>(Table, UndS[und]) + 1]);
				}

			} while (++i < kor.Length);

			return sb.ToString();
		}





		
	}



	
	
}



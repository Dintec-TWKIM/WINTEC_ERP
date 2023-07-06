using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.Windows.OneControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Dintec
{
	public static class ExtensionMethods
	{



		public static void Add(this WebBrowser o, string value)
		{
			string html = o.DocumentText;

			if (html == "")
			{
				html = @"
<html>
	<head>
		<style type='text/css'>
			html, body, div, span, table, thead, tbody, tfoot, tr, th, td, img { margin:0; padding:0; border:0; outline:0; line-height:1; font-family:맑은 고딕; font-size:10pt; }			
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






		public static void SetColor(this C1.Win.C1FlexGrid.Column o, Color color)
		{
			o.Style.ForeColor = color;
		}

		public static void SetBold(this C1.Win.C1FlexGrid.Column o, bool bold)
		{
			if (bold)			
				o.Style.Font = new Font(o.Style.Font, FontStyle.Bold);
			else
				o.Style.Font = new Font(o.Style.Font, FontStyle.Regular);
		}

		public static DataRow GetFirstRow(this DataRow[] dataRows)
		{
			if (dataRows.Length > 0)
				return dataRows[0];
			else
				return null;
		}

		public static DataTable ToDataTable(this DataRow[] dataRows)
		{
			if (dataRows.Length > 0)
				return dataRows.CopyToDataTable();
			else
				return null;
		}

		public static DataTable ToDataTable(this DataRow[] dataRows, bool distinct, params string[] columnNames)
		{
			if (dataRows.Length > 0)
				return dataRows.ToDataTable().ToDataTable(distinct, columnNames);
			else
				return null;
		}



		public static DataTable ToDataTable(this DataTable dataTable, bool distinct, params string[] columnNames)
		{
			return new DataView(dataTable).ToTable(distinct, columnNames);
		}

		public static DataTable Copy(this DataTable dataTable, string filter)
		{
			return dataTable.Select(filter).CopyToDataTable();
		}



		public static DataTable Delete(this DataTable dataTable, string filter)
		{			
			dataTable.Select(filter).Delete();
			return dataTable;
		}
		public static void Delete(this IEnumerable<DataRow> rows)
		{
			foreach (var row in rows)
				row.Delete();
		}



		public static int ToInt(this object value)
		{
			if (value == null || value.ToString() == "")
				return 0;
			else
				return Convert.ToInt32(value);
		}

		public static bool ToBoolean(this object value)
		{
			return Convert.ToBoolean(value);
		}

		public static string ToStr(this object value)
		{
			if (value == null)
				return "";
			else
				return value.ToString();
		}

		public static string ToString2(this object value)
		{
			if (value == null)
				return "";
			else
				return value.ToString();
		}

		public static decimal ToDecimal(this object value)
		{
			if (value == null)
				return 0;

			if (decimal.TryParse(value.ToString(), out decimal d))
				return d;
			else
				return 0;
		}

		public static string ToXml(this DataRow[] o)
		{
			return o.ToDataTable().ToXml("");
		}

		public static string ToXml(this DataRow[] o, params string[] columnNames)
		{
			return o.ToDataTable().ToXml(columnNames);
		}

		public static string ToXml(this DataTable dataTable)
		{
			return dataTable.ToXml("");
		}

		public static string ToXml(this DataTable dataTable, DataRowState rowState)
		{
			DataTable dt = dataTable.Copy();
			dt.AcceptChanges();

			// 상태변경
			if (rowState == DataRowState.Added)			
				foreach (DataRow row in dt.Rows) row.SetAdded();
			else if (rowState == DataRowState.Modified)
				foreach (DataRow row in dt.Rows) row.SetModified();

			return dt.ToXml("");
		}

		public static string ToXml(this DataTable dataTable, params string[] columnNames)
		{
			if (dataTable == null)
				return "<XML />";

			// XML_FLAG 값 만들기 => 이 방법을 안쓰면 deleted 된 행에 값을 쓸 수가 없음 (XML_FLAG 값 쓰기 불가)
			DataTable dt = columnNames[0] == "" ? dataTable.Clone() : new DataView(dataTable.Clone()).ToTable(false, columnNames);
			DataTable dtD;
			DataTable dtI;
			DataTable dtU;
			DataTable dtO;

			if (columnNames[0] == "")
			{
				dtD = new DataView(dataTable, null, null, DataViewRowState.Deleted).ToTable();
				dtI = new DataView(dataTable, null, null, DataViewRowState.Added).ToTable();
				dtU = new DataView(dataTable, null, null, DataViewRowState.ModifiedCurrent).ToTable();
				dtO = new DataView(dataTable, null, null, DataViewRowState.Unchanged).ToTable();
			}
			else
			{
				dtD = new DataView(dataTable, null, null, DataViewRowState.Deleted).ToTable(false, columnNames);
				dtI = new DataView(dataTable, null, null, DataViewRowState.Added).ToTable(false, columnNames);
				dtU = new DataView(dataTable, null, null, DataViewRowState.ModifiedCurrent).ToTable(false, columnNames);
				dtO = new DataView(dataTable, null, null, DataViewRowState.Unchanged).ToTable(false, columnNames);
			}

			dt.Columns.Add("XML_FLAG", typeof(string));
			dtD.Columns.Add("XML_FLAG", typeof(string), "'D'");
			dtI.Columns.Add("XML_FLAG", typeof(string), "'I'");
			dtU.Columns.Add("XML_FLAG", typeof(string), "'U'");
			dtO.Columns.Add("XML_FLAG", typeof(string), "'O'");

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
				if (!dt.Columns.Contains("CD_COMPANY"))
					dt.Columns.Add("CD_COMPANY", typeof(string), "'" + Global.MainFrame.LoginInfo.CompanyCode + "'").SetOrdinal(0);
				
				if (!dt.Columns.Contains("ID_USER"))
					dt.Columns.Add("ID_USER", typeof(string), "'" + Global.MainFrame.LoginInfo.UserID + "'").SetOrdinal(dt.Columns.Count - 2);  // 마지막 앞에 넣음
			}

			

			// 맵핑 타입 변경
			foreach (DataColumn dc in dt.Columns)
				dc.ColumnMapping = MappingType.Attribute;

			// XML 변환			
			DataSet ds = new DataSet();
			dt.TableName = "ROW";
			ds.Tables.Add(dt);

			StringWriter sw = new StringWriter();
			ds.DataSetName = "XML";
			ds.WriteXml(sw, XmlWriteMode.IgnoreSchema);

			return sw.ToString();
		}





		public static string Left(this object value, int length)
		{
			string s = value.ToString();
			return s.Substring(0, length);
		}

		public static string Left(this string value, int length)
		{
			string s = value;
			return s.Substring(0, length);
		}
		public static string Right(this object value, int length)
		{
			string s = value.ToString();
			return s.Substring(s.Length - length);
		}

		public static string Right(this string value, int length)
		{
			string s = value;
			return s.Substring(s.Length - length);
		}

		public static int Count(this string value, string str)
		{
			string s = value;
			Regex cntStr = new Regex(str);
			int count = int.Parse(cntStr.Matches(s, 0).Count.ToString());

			return count;
		}

		#region ==================================================================================================== Clear

		public static void ClearAndDefault(this OneGrid o, bool boDefault)
		{
			foreach (Control c in o.Controls)
				((OneGridItem)c).ClearAndDefault(boDefault);
		}

		public static void ClearAndDefault(this OneGridItem o, bool boDefault)
		{
			foreach (Control c in o.Controls)
			{
				if (c is BpPanelControl pnl)
					pnl.ClearAndDefault(boDefault);
			}
		}

		public static void ClearAndDefault(this BpPanelControl o, bool boDefault)
		{
			foreach (Control c in o.Controls)
			{
				if (c is TextBoxExt tbx)
					tbx.Text = "";
				else if (c is BpCodeTextBox cbx)
					cbx.ClearAndDefault(boDefault);
			}
		}

		public static void ClearAndDefault(this BpCodeTextBox o, bool boDefault)
		{
			string tag = o.Tag.ToString();

			if (tag == "NO_EMP;NM_EMP")					// 담당자
			{
				o.CodeValue = Global.MainFrame.LoginInfo.UserID;
				o.CodeName = Global.MainFrame.LoginInfo.UserName;
			}
			else if (tag == "CD_SALEGRP;NM_SALEGRP")	// 영업그룹
			{
				o.CodeValue = Global.MainFrame.LoginInfo.SalesGroupCode;
				o.CodeName = Global.MainFrame.LoginInfo.SalesGroupName;
			}
			else if (tag == "CD_PURGRP;NM_PURGRP")		// 구매그룹
			{
				o.CodeValue = Global.MainFrame.LoginInfo.PurchaseGroupCode;
				o.CodeName = Global.MainFrame.LoginInfo.PurchaseGroupName;
			}
			else
			{
				o.CodeValue = "";
				o.CodeName = "";
			}
		}



			// 콤보박스
		public static void ClearData(this DropDownComboBox o)
		{
			if (o.DataSource != null)
				((DataTable)o.DataSource).Clear();
			else if (o.Items.Count > 0)
				o.Items.Clear();
		}

		public static void SetDefaultValue(this DropDownComboBox o)
		{
			o.SelectedIndex = 0;
		}

		public static DataTable GetData(this DropDownComboBox o)
		{
			return (DataTable)o.DataSource;
		}

		// 그리드
		public static void ClearData(this FlexGrid o)
		{
			if (o.DataSource != null)
			{
				o.DataTable.Rows.Clear();
				o.AcceptChanges();
			}
		}

		// 데이트픽커
		public static void ClearData(this DatePicker o)
		{
			o.Text = "";
		}

		public static void SetDefaultValue(this DatePicker o)
		{
			o.Text = Util.GetToday();
		}

		// 코드텍스트박스
		public static void ClearData(this BpCodeTextBox o)
		{
			o.CodeValue = "";
			o.CodeName = "";
		}

		public static void SetDefaultValue(this BpCodeTextBox o)
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
			if (tag == "CD_PURGRP;NM_PURGRP")       // 구매그룹
			{
				o.CodeValue = Global.MainFrame.LoginInfo.PurchaseGroupCode;
				o.CodeName = Global.MainFrame.LoginInfo.PurchaseGroupName;
			}
		}














		public static void Clear(this BpCodeTextBox o, bool boDefault)
		{
			o.CodeValue = "";
			o.CodeName = "";

			// 기본값 설정
			if (boDefault)
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
				if (tag == "CD_PURGRP;NM_PURGRP")       // 구매그룹
				{
					o.CodeValue = Global.MainFrame.LoginInfo.PurchaseGroupCode;
					o.CodeName = Global.MainFrame.LoginInfo.PurchaseGroupName;
				}
			}
		}

		public static void Clear(this DatePicker o, bool boDefault)
		{
			o.Text = "";

			// 기본값 설정
			if (boDefault)
				o.Text = Util.GetToday();
		}

		public static void Clear(this DropDownComboBox o)
		{
			Clear(o, false);
		}

		public static void Clear(this DropDownComboBox o, bool boDefault)
		{
			if (o.DataSource != null)
			{
				if (boDefault)
					o.SelectedIndex = 0;
				else
					((DataTable)o.DataSource).Clear();
			}
			else if (o.Items.Count > 0)
			{
				if (boDefault)
					o.SelectedIndex = 0;
				else
					o.Items.Clear();
			}
		}

		public static void Clear(this BpComboBox o, bool boDefault)
		{
			o.Clear();
		}

	
		public static void Clear(this FlexGrid o, bool addOneRow)
		{
			if (o.DataSource != null)
			{
				o.DataTable.Rows.Clear();
				o.AcceptChanges();
			}

			//o.Rows.RemoveRange(o.Rows.Fixed, o.Rows.Count - o.Rows.Fixed);

			//if (addOneRow)
			//    o.Rows.Add();
		}



		#endregion

		public static void Highlight(this LabelExt o, HighlightMode mode)
		{
			if (mode == HighlightMode.On)
			{
				o.ForeColor = Color.Red;
				o.Font = new Font(o.Font, FontStyle.Bold);
			}
			else if (mode == HighlightMode.Neutral)
			{
				o.ForeColor = Color.Black;
				o.Font = new Font(o.Font, FontStyle.Bold);
			}
			else if (mode == HighlightMode.Off)
			{
				o.ForeColor = Color.LightGray;
				o.Font = new Font(o.Font, FontStyle.Regular);
			}
		}



		public static string GetValue(this BpComboBox o)
		{
			string s = "";

			if (o.SelectedItem != null)
				s = o.SelectedValue.ToString();

			return s;
		}
		public static string GetValue(this DropDownComboBox o)
		{
			string s = "";

			if (o.SelectedItem != null)
				s = o.SelectedValue.ToString();

			return s;
		}

		public static string GetValue(this DropDownComboBox o, string colName)
		{
			string s = "";

			if (o.SelectedItem != null)
				s = ((DataRowView)o.SelectedItem).Row[colName].ToString();

			return s;
		}

		public static string GetText(this DropDownComboBox o)
		{
			string s = "";

			if (o.SelectedItem != null)
				s = o.GetItemText(o.SelectedItem);

			return s;
		}

		public static void DataBind(this DropDownComboBox o, DataTable dataTable, bool emptyLine)
		{
			DataTable dt = dataTable.Copy();

			// 필드 이름 Default 설정
			if (o.ValueMember == "")
			{
				o.ValueMember = "CODE";
				o.DisplayMember = "NAME";
			}

			// 공백 Row 추가
			if (emptyLine)
			{
				dt.Rows.InsertAt(dt.NewRow(), 0);
				dt.Rows[0][o.ValueMember] = DBNull.Value;
				dt.Rows[0][o.DisplayMember] = DBNull.Value;
			}

			o.DataSource = dt;
		}

		public static void RemoveItem(this DropDownComboBox o, string value)
		{
			((DataTable)o.DataSource).Rows.Remove(((DataTable)o.DataSource).Select("CODE = '" + value + "'")[0]);
		}



		public static string GetValue(this TextBoxExt o)
		{
			return o.Text.Trim();
		}

		public static decimal GetValue(this CurrencyTextBox o)
		{
			return o.DecimalValue;
		}

		public static void SetValue(this DropDownComboBox o, object value)
		{
			if (value.ToString() == "")
				o.SelectedIndex = 0;
			else
				o.SelectedValue = value.ToString();
		}


		public static void AllowTyping(this DatePicker o, bool allowed)
		{
			MaskedEditBox c = (MaskedEditBox)o.Controls[0];
			c.ReadOnly = true;
			c.ForeColor = Color.FromArgb(109, 109, 109);
			c.BackColor = Color.FromArgb(240, 240, 240);
		}

		public static void Init(this DatePicker o)
		{
			MaskedEditBox c = (MaskedEditBox)o.Controls[0];

			c.Font = new Font(o.Font.Name, 8.4f);
			c.TextSelectAll = false;
			c.UseKeyEnter = false;
		}

		// Panel Control
		// Enabled 는 죽이고 Editable는 살리자
		public static void Enabled(this BpPanelControl o, bool enabled)
		{
			foreach (Control c in o.Controls)
			{
				if (c is TextBoxExt)
					((TextBoxExt)c).ReadOnly = !enabled;
				else if (c is BpCodeTextBox)
					((BpCodeTextBox)c).ReadOnly = !enabled ? ReadOnly.TotalReadOnly : ReadOnly.None;
				else if (c is CurrencyTextBox)
					((CurrencyTextBox)c).ReadOnly = !enabled;
				else
					c.Enabled = enabled;
			}
		}

		public static void Enable(this BpPanelControl o, bool enabled)
		{
			foreach (Control c in o.Controls)
			{
				if (c is TextBoxExt)
					((TextBoxExt)c).ReadOnly = !enabled;
				else if (c is BpCodeTextBox)
					((BpCodeTextBox)c).ReadOnly = !enabled ? ReadOnly.TotalReadOnly : ReadOnly.None;
				else if (c is CurrencyTextBox)
					((CurrencyTextBox)c).ReadOnly = !enabled;
				else
					c.Enabled = enabled;
			}
		}

		// ---------------------------------------------------------------------------------------------------- 편집여부
		public static void Editable(this BpPanelControl o, bool editable)
		{
			foreach (Control c in o.Controls)
			{
				if (c is UTextBox)
				{
					UTextBox tbx = (UTextBox)c;
					tbx.ReadOnly = !editable;

					if (tbx.BorderColor == Color.FromArgb(189, 199, 217))
						tbx.BackColor = editable ? tbx.ColorTag : Color.FromArgb(240, 240, 240);
				}
				else if (c is BpPanelControl) ((BpPanelControl)c).Editable(editable);
				else if (c is TextBoxExt) ((TextBoxExt)c).ReadOnly = !editable;
				else if (c is BpCodeTextBox) ((BpCodeTextBox)c).ReadOnly = !editable ? ReadOnly.TotalReadOnly : ReadOnly.None;
				else if (c is CurrencyTextBox) ((CurrencyTextBox)c).ReadOnly = !editable;
				else c.Enabled = editable;
			}
		}

		public static void Editable(this OneGrid o, bool editable)
		{
			foreach (Control i in o.Controls)
			{
				if (i is OneGridItem)
				{
					foreach (Control j in i.Controls)
					{
						if (j is BpPanelControl)
							((BpPanelControl)j).Editable(editable);
					}
				}
			}
		}

		public static void Editable(this TextBoxExt o, bool editable)
		{
			o.ReadOnly = !editable;
		}

		public static void Editable(this DropDownComboBox o, bool editable)
		{
			o.Enabled = editable;
		}

		public static void Editable(this BpCodeTextBox o, bool editable)
		{
			o.ReadOnly = !editable ? ReadOnly.TotalReadOnly : ReadOnly.None;
		}

		public static void Editable(this ImagePanel o, bool editable)
		{
			foreach (Control c in o.Controls)
			{
				if (c is RoundedButton) ((RoundedButton)c).Enabled = editable;
				else if (c is TextBoxExt) ((TextBoxExt)c).ReadOnly = !editable;
				else if (c is UCurrencyBox) ((UCurrencyBox)c).ReadOnly = !editable;
			}
		}

		//public static void GetValue(this FlexGrid o, int row, int col)
		//{
		//	grd헤드["EZCODE"]
		//}

		//public static void GetValue(this FlexGrid o, int col)
		//{
		//	grd헤드["EZCODE"]
		//}

		public static void RemoveCol(this DataColumnCollection o, string name)
		{
			if (o.Contains(name))
				o.Remove(name);			
		}

		public static string GetValue(this FlexGrid o, string colName)
		{
			if (o.Cols.Contains(colName) && o[colName] != null)
				return o[colName].ToString();
			else
				return "";
		}

		public static string GetValue(this FlexGrid o, int row, string colName)
		{
			if (o.Cols.Contains(colName) && o[row, colName] != null)
				return o[row, colName].ToString();
			else
				return "";
		}




		public static void Editable(this FlexGrid o, bool editable)
		{
			foreach (Column col in o.Cols)
			{
				if (col.Name == "" || col.Name == "_IS_DETAILGET" || o.GetCellStyle(o.GetHeadRow(col.Name), col.Index) == null)
					continue;

				if (editable)
				{
					if (o.GetCellStyle(o.GetHeadRow(col.Name), col.Index).Name == "EDIT_HEADER_N")
					{
						o.Cols[col.Name].AllowEditing = true;
						o.SetCellStyle(o.GetHeadRow(col.Name), col.Index, "EDIT_HEADER_Y");
					}
				}
				else
				{
					if (o.GetCellStyle(o.GetHeadRow(col.Name), col.Index).Name == "EDIT_HEADER_Y")
					{
						o.Cols[col.Name].AllowEditing = false;
						o.SetCellStyle(o.GetHeadRow(col.Name), col.Index, "EDIT_HEADER_N");
					}
				}
			}
		}



		

		public static void SetEditColumn(this FlexGrid o, params string[] colNames)
		{
			SetEditColumn(o, true, colNames);
		}

		public static void SetEditColumn(this FlexGrid o, bool editable, params string[] colNames)
		{
			// EDIT N일때
			CellStyle editNo = o.Styles.Add("EDIT_HEADER_N");
			editNo.ForeColor = Color.White;


			foreach (string s in colNames)
			{				
				string styleName;
				
				if (o.Cols[s].Style.ForeColor == SystemColors.WindowText  || o.Cols[s].Style.ForeColor == Color.Black)
				{
					// 기본값
					styleName = "EDIT_HEADER_Y";

					if (!o.Styles.Contains(styleName))
					{
						CellStyle cs = o.Styles.Add(styleName);
						cs.Font = new Font(o.Font, FontStyle.Bold);
						cs.ForeColor = Color.Blue;
					}
				}
				else
				{
					// 컬럼에 이미 지정된 색상이 있을 경우
					styleName = "EDIT_HEADER_Y_" + o.Cols[s].Style.ForeColor.Name.ToUpper();

					if (!o.Styles.Contains(styleName))
					{
						CellStyle cs = o.Styles.Add(styleName);
						cs.Font = new Font(o.Font, FontStyle.Bold);
						cs.ForeColor = o.Cols[s].Style.ForeColor;
					}
				}

				o.Cols[s].AllowEditing = editable;

				// 스타일 세팅 (해당셀에 스타일이 없는 경우에만)
				int headerRow = o.GetHeadRow(s);
				int headerCol = o.Cols[s].Index;

				if (o.GetCellStyle(headerRow, headerCol) == null)
					o.SetCellStyle(o.GetHeadRow(s), o.Cols[s].Index, editable ? styleName : editNo.Name);
			}
		}

		public static int GetHeadRow(this FlexGrid o, string colName)
		{
			// 실제 Data행의 직전 행 Row 인덱스
			int headerRow = o.Rows.Fixed - 1;

			// 필터링 줄 있을때 한줄 내림 (아래거랑 세트로 옴)
			if (o.GetCellStyle(headerRow, 1) != null && o.GetCellStyle(headerRow, 1).Name == "!u!s!e!r!F!i!l!t!e!r!1")
				headerRow = headerRow - 1;

			// Visible이 false 인 경우 한줄 내림 (위에거랑 세트로 옴)
			if (!o.Rows[headerRow].Visible)
				headerRow = headerRow - 1;

			// 합계줄 있을때 한줄 내림
			if (o.SumPosition == SumPositionEnum.Top)
				headerRow = headerRow - 1;

		

			// 셀 병합일때 한줄 내림
			if (headerRow >= 1)
			{
				if (o[headerRow - 1, colName].ToString() == o[headerRow, colName].ToString())
					headerRow = headerRow - 1;
			}

			return headerRow;
		}

		public static void SetSumColumnStyle(this FlexGrid o, params string[] colNames)
		{
			foreach (string s in colNames)
			{
				// 배경색 변경, 스타일을 지정하면 Format 형식까지 날아가버림
				o.Cols[s].Style.BackColor = Color.FromArgb(241, 241, 241);
			}
		}

		public static void SetDefault(this FlexGrid o, string version, SumPositionEnum sumPosition)
		{
			// 그리드 초기화 체크
			//string formName = o.GetContainerControl().ToString().Split('.')[1];

			//if (File.Exists(Application.StartupPath + @"\UserConfig\Grid\" + formName + ".init"))
			//{
			//	File.Delete(Application.StartupPath + @"\UserConfig\Grid\" + formName + ".init");
			//	File.Delete(Application.StartupPath + @"\UserConfig\Grid\" + formName + ".dzw");
			//	File.Delete(Application.StartupPath + @"\UserConfig\Help\Grid\" + formName + ".dzw");
			//}

			Control parent = o.Parent;
			bool isStop = false;
			while (!isStop)
			{
				switch (parent)
				{
					case IClient _:
					case Duzon.Common.Forms.CommonDialog _:
						if (parent != null)
						{
							string formName = !(parent is IClient) ? parent.Name : (((IClient)parent).PageID == string.Empty ? parent.Name : ((IClient)parent).PageID);
							string path = Application.StartupPath + "\\UserConfig\\Grid\\" + formName + ".init";

							if (File.Exists(path))
							{
								if (File.Exists(path)) File.Delete(path);
								path = Application.StartupPath + "\\UserConfig\\Grid\\" + formName + ".dzw";
								if (File.Exists(path)) File.Delete(path);
								path = Application.StartupPath + "\\UserConfig\\Help\\Grid\\" + formName + ".dzw";
								if (File.Exists(path)) File.Delete(path);
							}

							path = Application.StartupPath + "\\UserConfig\\Grid\\" + formName + ".dzw";

							if (File.Exists(path))
							{
								FileInfo fileInfo = new FileInfo(Application.StartupPath + "\\UserConfig\\Grid\\" + formName + ".dzw");
								if (fileInfo.Length == 0) File.Delete(path);
							}
						}
						isStop = true;
						break;
					case null:
						isStop = true;
						break;
					default:
						parent = parent.Parent;
						continue;
				}
			}

			// 마무리			
			o.KeyActionEnter = KeyActionEnum.MoveDown;
			o.SettingVersion = version;
			o.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, sumPosition);
			o.Styles.Highlight.Clear();
			o.Styles.Highlight.BackColor = Color.FromArgb(255, 230, 244, 255);
			o.Rows.DefaultSize = 22;

			int headerCount = o.Rows.Fixed;
			if (sumPosition == SumPositionEnum.Top)
				headerCount--;

			for (int i = 0; i < headerCount; i++)
				o.Rows[i].Height = 28;

			// 사번에 따라서 그리드 선택행 배경색 조정
			if (Global.MainFrame.LoginInfo.UserID.In("S-472"))
				o.Styles.Highlight.BackColor = Color.FromArgb(255, 188, 229, 255);  // 선택행 스타일 변경 (좀 진하게) 기본값 : 255, 230, 244, 255

			// 기본 이벤트 부여
			o.BeforeDoubleClick += new BeforeMouseDownEventHandler(o_BeforeDoubleClick);
		}

		private static void o_BeforeDoubleClick(object sender, BeforeMouseDownEventArgs e)
		{
			bool init = Control.ModifierKeys == (Keys.Control | Keys.Alt) || Control.ModifierKeys == Keys.Shift ? true : false;
			FlexGrid o = (FlexGrid)sender;
			int row = o.MouseRow;
			int col = o.MouseCol;

			// 헤더 젤 오른쪽 빈공간 클릭했을 때 특별한 기능 수행
			if (row < o.Rows.Fixed && col <= 0)
			{
				if (init)
				{
					H_CZ_GRID_CONFIG f = new H_CZ_GRID_CONFIG(o);
					f.ShowDialog();
				}
				else if (o.HasNormalRow)
				{
					// 오토 로우 사이즈
					o.AutoRowSize();
				}

				e.Cancel = true;
			}

			// 뒤에어 오는 마우스더블클릭 무조건 버리는 이벤트, before에서 미리 취소해준다
			if (!o.HasNormalRow || col <= 0)
				e.Cancel = true;
		}

		public static string GetFullName(this Control control)
		{
			if (control.Parent == null) return control.Name;
			return control.Parent.GetFullName() + "." + control.Name;
		}

		public static void DataBind(this FlexGrid o, DataTable dataTable)
		{
			o.Redraw = false;
			o.Binding = dataTable;
			o.Redraw = true;
			o.Redraw = false;
			for (int i = o.Rows.Fixed; i < o.Rows.Count; i++)
				o.AutoSizeRow(i);
			o.Redraw = true;
		}

		public static void DataBindAdd(this FlexGrid o, DataTable dataTable, string filter)
		{
			o.Redraw = false;
			o.BindingAdd(dataTable, filter);
			for (int i = o.Rows.Fixed; i < o.Rows.Count; i++)
				o.AutoSizeRow(i);
			o.Redraw = true;
		}

		public static void AutoRowSize(this FlexGrid o)
		{
			o.Redraw = false;
			for (int i = o.Rows.Fixed; i < o.Rows.Count; i++)
				o.AutoSizeRow(i);
			o.Redraw = true;
		}



		public static void Merge(this FlexGrid o, string baseColName, params string[] mergeColNames)
		{
			for (int i = o.Rows.Fixed; i < o.Rows.Count; i++)
			{
				foreach (string s in mergeColNames)
					o.SetUserData(i, s, o[i, baseColName] + s);
			}

			o.DoMerge();
		}

		public static bool In(this string item, params string[] items)
		{
			//if (items == null)
			//    throw new ArgumentNullException("items");
			List<string> list = new List<string>(items);

			return list.Contains(item);
		}



		

		
	}
}

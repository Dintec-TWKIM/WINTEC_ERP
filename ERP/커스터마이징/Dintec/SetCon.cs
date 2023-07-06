using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using DzHelpFormLib;
using System;
using System.Data;
using System.Windows.Forms;

namespace Dintec
{
	public class SetCon
	{
		public static void Clear(Control control)
		{
			Clear(control, false);
		}

		public static void Clear(Control control, bool boDefault)
		{
			Clear(control, false, null);
		}

		public static void Clear(Control control, bool boDefault, FreeBinding header)
		{
			string tag = GetTo.String(control.Tag);

			if (control is TextBoxExt)
			{
				((TextBoxExt)control).Text = "";
			}
			else if (control is BpCodeTextBox)
			{
				BpCodeTextBox con = (BpCodeTextBox)control;
				con.CodeValue = "";
				con.CodeName = "";

				if (boDefault)
				{
					if (tag == "NO_EMP;NM_EMP")         // 담당자
					{
						con.CodeValue = Global.MainFrame.LoginInfo.UserID;
						con.CodeName = Global.MainFrame.LoginInfo.UserName;
					}
					if (tag == "CD_SALEGRP;NM_SALEGRP") // 영업그룹
					{
						con.CodeValue = Global.MainFrame.LoginInfo.SalesGroupCode;
						con.CodeName = Global.MainFrame.LoginInfo.SalesGroupName;
					}
					if (tag == "CD_PURGRP;NM_PURGRP")   // 구매그룹
					{
						con.CodeValue = Global.MainFrame.LoginInfo.PurchaseGroupCode;
						con.CodeName = Global.MainFrame.LoginInfo.PurchaseGroupName;
					}
				}

				if (header != null) header.CurrentRow[tag.Substring(0, tag.IndexOf(";"))] = con.CodeValue;
			}
			else if (control is DatePicker)
			{
				DatePicker con = (DatePicker)control;
				con.Text = "";

				if (boDefault) con.Text = Util.GetToday();
				if (header != null) header.CurrentRow[tag] = con.Text;

			}
			else if (control is DropDownComboBox)
			{
				DropDownComboBox con = (DropDownComboBox)control;

				if (con.DataSource != null)
				{
					if (!boDefault) ((DataTable)con.DataSource).Clear();
					else
					{
						con.SelectedIndex = 0;
						if (header != null) header.CurrentRow[tag] = GetCon.Value(con);
					}
				}
			}
			else if (control is FlexGrid)
			{
				FlexGrid con = (FlexGrid)control;

				if (con.DataTable != null)
				{
					con.DataTable.Rows.Clear();
					con.AcceptChanges();
				}
			}
		}

		public static void Enabled(Control control, bool value)
		{
			if (control is RadioButtonExt)
			{
				control.Enabled = value;
			}
		}

		public static void Enabled(BpPanelControl panel, bool value)
		{
			foreach (Control c in panel.Controls)
			{
				if (c is TextBoxExt)
				{
					((TextBoxExt)c).ReadOnly = !value;
				}
				else if (c is BpCodeTextBox)
				{
					((BpCodeTextBox)c).ReadOnly = !value ? ReadOnly.TotalReadOnly : ReadOnly.None;
				}
				else if (c is CurrencyTextBox)
				{
					((CurrencyTextBox)c).ReadOnly = !value;
				}
				else c.Enabled = value;
			}
		}

		public static void DataBind(DropDownComboBox comboBox, DataTable dataTable, bool addEmptyLine)
		{
			if (addEmptyLine)
			{
				dataTable.Rows.InsertAt(dataTable.NewRow(), 0);
				dataTable.Rows[0]["CODE"] = DBNull.Value;
				dataTable.Rows[0]["NAME"] = DBNull.Value;
			}

			comboBox.ValueMember = "CODE";
			comboBox.DisplayMember = "NAME";
			comboBox.DataSource = dataTable;
		}

		public static void DataBind(DropDownComboBox comboBox, string keyCode, bool addEmptyLine)
		{
			DataTable dt = GetDb.Code(keyCode);

			if (addEmptyLine)
			{
				dt.Rows.InsertAt(dt.NewRow(), 0);
				dt.Rows[0]["CODE"] = DBNull.Value;
				dt.Rows[0]["NAME"] = DBNull.Value;
			}

			comboBox.ValueMember = "CODE";
			comboBox.DisplayMember = "NAME";
			comboBox.DataSource = dt;
		}
	}
}

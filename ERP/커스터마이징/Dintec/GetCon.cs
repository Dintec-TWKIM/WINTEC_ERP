using Duzon.Common.Controls;
using System.Data;
using System.Windows.Forms;

namespace Dintec
{
	public class GetCon
	{
		public static string Text(Control control)
		{
			string s = "";

			if (control is DropDownComboBox)
			{
				DropDownComboBox con = (DropDownComboBox)control;
				if (con.SelectedItem != null) s = ((DataRowView)(con.SelectedItem)).Row["NAME"].ToString();
			}

			return s;
		}

		public static string Value(Control control)
		{
			string s = "";

			if (control is DropDownComboBox)
			{
				DropDownComboBox con = (DropDownComboBox)control;
				if (con.SelectedItem != null)
				{
					string field = "";
					DataTable dt = (DataTable)con.DataSource;

					if (dt.Columns.Contains("CODE")) field = "CODE";
					else if (dt.Columns.Contains("SEQ")) field = "SEQ";

					s = ((DataRowView)(con.SelectedItem)).Row[field].ToString();
				}
			}

			return s;
		}
	}
}

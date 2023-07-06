using System.Drawing;
using Duzon.Common.Controls;

namespace Dintec
{
	public partial class UDatePicker : DatePicker
	{
		public bool AllowTyping
		{
			set
			{
				MaskedEditBox c = (MaskedEditBox)this.Controls[0];
				c.ReadOnly = true;
				c.ForeColor = Color.FromArgb(109, 109, 109);
				c.BackColor = Color.FromArgb(240, 240, 240);
			}
		}

		public UDatePicker()
		{
			MaskedEditBox c = (MaskedEditBox)this.Controls[0];

			c.Font = new Font(this.Font.Name, 8.4f);
			c.TextSelectAll = false;
			c.UseKeyEnter = false;
		}


	}
}

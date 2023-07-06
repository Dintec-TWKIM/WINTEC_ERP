using System.Drawing;
using Duzon.Common.Controls;

namespace Dintec
{
	public partial class UPeriodPicker : PeriodPicker
	{
		public UPeriodPicker()
		{
			this.Controls[0].Font = new Font(this.Font.Name, 8.4f);
			this.Controls[2].Font = new Font(this.Font.Name, 8.4f);
		}
	}
}

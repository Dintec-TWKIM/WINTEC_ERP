using System.ComponentModel;
using Duzon.Common.Controls;

namespace Dintec
{
	public partial class UCurrencyBox : CurrencyTextBox
	{
		[DefaultValue(false)]
		[Browsable(true)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		public UCurrencyBox()
		{
			this.AutoSize = false;
			this.Height = 20;
		}
	}
}

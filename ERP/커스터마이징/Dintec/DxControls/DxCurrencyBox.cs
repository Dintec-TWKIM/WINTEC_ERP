using System.ComponentModel;

namespace Dintec.DxControls
{
	public partial class DxCurrencyBox : Duzon.Common.Controls.CurrencyTextBox
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

		public DxCurrencyBox()
		{
			AutoSize = false;
			Height = 20;
		}
	}
}

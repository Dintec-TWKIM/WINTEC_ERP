namespace Dintec.DxControls
{
	public class DxTextBox : Duzon.Common.Controls.TextBoxExt
	{
		[System.ComponentModel.DefaultValue(false)]
		[System.ComponentModel.Browsable(true)]
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

		[System.ComponentModel.Category("ColorTag")]
		public System.Drawing.Color ColorTag
		{
			get;
			set;
		}
	}
}

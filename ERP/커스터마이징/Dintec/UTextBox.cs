using System.ComponentModel;
using System.Drawing;
using Duzon.Common.Controls;

namespace Dintec
{
	public partial class UTextBox : TextBoxExt
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

		[Category("ColorTag")]
		public Color ColorTag
		{
			get;
			set;
		}

		public UTextBox()
		{
			this.AutoSize = false;
			this.Height = 20;
		}
	}
}

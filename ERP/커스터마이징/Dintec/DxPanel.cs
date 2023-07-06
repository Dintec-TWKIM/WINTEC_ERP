using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Dintec
{
	public partial class DxPanel : Panel
	{
		public Color BorderColor { get; set; } = Color.Transparent;

		public DxPanel() : base()
		{
			SetStyle(ControlStyles.UserPaint, true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.DrawRectangle(new Pen(new SolidBrush(BorderColor), 4), e.ClipRectangle);
		}
	}
}

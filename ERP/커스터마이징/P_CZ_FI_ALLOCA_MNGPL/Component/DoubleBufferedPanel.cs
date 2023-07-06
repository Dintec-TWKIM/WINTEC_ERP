using System.Windows.Forms;

namespace cz
{
    public partial class DoubleBufferedPanel : TableLayoutPanel
    {
        public DoubleBufferedPanel()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();
        }
    }
}

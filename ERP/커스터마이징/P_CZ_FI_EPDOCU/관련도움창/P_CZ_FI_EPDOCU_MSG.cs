using Duzon.Common.Controls;
using Duzon.Common.Forms;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_FI_EPDOCU_MSG : Duzon.Common.Forms.CommonDialog
    {
        private DataRow[] _Msg;

        public P_CZ_FI_EPDOCU_MSG(DataTable MSG, string ROW_ID)
        {
            this.InitializeComponent();
            this._Msg = MSG.Select("ROW = '" + ROW_ID + "'");
            this.KeyDown += new KeyEventHandler(this.P_FI_EPDOCU_MSG_KeyDown);
        }

        private void P_FI_EPDOCU_MSG_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Escape)
                    return;
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override void InitLoad()
        {
            base.InitLoad();
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            this.설명서();
        }

        private void 설명서()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (DataRow dataRow in this._Msg)
            {
                stringBuilder.AppendLine(dataRow[1].ToString());
                stringBuilder.AppendLine("");
            }
            this.textBoxExt1.Text = stringBuilder.ToString();
        }

        private void _btn닫기_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
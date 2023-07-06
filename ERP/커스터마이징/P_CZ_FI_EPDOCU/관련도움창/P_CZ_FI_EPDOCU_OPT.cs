using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_FI_EPDOCU_OPT : Duzon.Common.Forms.CommonDialog
    {
        private string _strDtAcct = (string)null;
        private string _strDtWrite = (string)null;
        private bool _optChk;
        private int _rtn;

        public int GetResult
        {
            get
            {
                return this._rtn;
            }
        }

        public string GetDtAcct
        {
            get
            {
                return this._strDtAcct;
            }
        }

        public string GetDtWrite
        {
            get
            {
                return this._strDtWrite;
            }
        }

        public P_CZ_FI_EPDOCU_OPT(bool optChk)
        {
            this.InitializeComponent();
            this._optChk = optChk;
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            if (this._optChk)
            {
                this.panelEx1.Visible = false;
                this.Height = this.Height - this.panelEx1.Height;
            }
            else
            {
                this.dt_회계일자.Text = Global.MainFrame.GetStringToday;
                this.dt_작성일자.Text = Global.MainFrame.GetStringToday;
            }
        }

        private bool Chk()
        {
            bool flag = true;

            if (!this._optChk)
            {
                if (D.IsEmpty((object)this.dt_회계일자.Text) || this.dt_회계일자.Text.Length < 8)
                {
                    Global.MainFrame.ShowMessage("회계일자를 입력하세요");
                    flag = false;
                }
                if (D.IsEmpty((object)this.dt_작성일자.Text) || this.dt_작성일자.Text.Length < 8)
                {
                    Global.MainFrame.ShowMessage("작성일자를 입력하세요");
                    flag = false;
                }
            }
            return flag;
        }

        private void _btn닫기_Click(object sender, EventArgs e)
        {
            if (!this.Chk())
                return;

            if (!this._optChk)
            {
                this._strDtAcct = this.dt_회계일자.Text;
                this._strDtWrite = this.dt_작성일자.Text;
            }

            this._rtn = 1;
            this.DialogResult = DialogResult.OK;
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            if (!this.Chk())
                return;

            if (!this._optChk)
            {
                this._strDtAcct = this.dt_회계일자.Text;
                this._strDtWrite = this.dt_작성일자.Text;
            }

            this._rtn = 2;
            this.DialogResult = DialogResult.OK;
        }
    }
}
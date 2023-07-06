using Duzon.Common.Controls;
using Duzon.Common.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_FI_EPDOCU_DOCU_OPT : Duzon.Common.Forms.CommonDialog
    {
        private string _chk;
        
        public string GetResult
        {
            get
            {
                return this._chk;
            }
        }

        public P_CZ_FI_EPDOCU_DOCU_OPT(string chk)
        {
            this.InitializeComponent();
            this._chk = chk;
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.rBtn0.CheckedChanged += new EventHandler(this.rBtn_CheckedChanged);
            this.rBtn1.CheckedChanged += new EventHandler(this.rBtn_CheckedChanged);
            this.rBtn2.CheckedChanged += new EventHandler(this.rBtn_CheckedChanged);
            this.rBtn3.CheckedChanged += new EventHandler(this.rBtn_CheckedChanged);
            this._btn확인.Click += new EventHandler(this._btn확인_Click);
            this._btn취소.Click += new EventHandler(this._btn취소_Click);
        }

        private void rBtn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            this._chk = radioButton.Name.Substring(radioButton.Name.Length - 1, 1);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            switch (this._chk)
            {
                case "0":
                    this.rBtn0.Checked = true;
                    break;
                case "1":
                    this.rBtn1.Checked = true;
                    break;
                case "2":
                    this.rBtn2.Checked = true;
                    break;
                case "3":
                    this.rBtn3.Checked = true;
                    break;
                default:
                    this.rBtn0.Checked = true;
                    break;
            }
        }

        private void _btn취소_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void _btn확인_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
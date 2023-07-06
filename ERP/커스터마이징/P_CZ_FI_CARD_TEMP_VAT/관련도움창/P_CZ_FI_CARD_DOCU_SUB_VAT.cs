using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_FI_CARD_DOCU_SUB_VAT : Duzon.Common.Forms.CommonDialog
    {
        
        private Hashtable ht = new Hashtable();
        private P_CZ_FI_CARD_TEMP_VAT_BIZ _biz;
        private DataTable _dt;

        public P_CZ_FI_CARD_DOCU_SUB_VAT()
        {
            this.InitializeComponent();

            this._biz = new P_CZ_FI_CARD_TEMP_VAT_BIZ();
            
            this.btn_process.Click += new EventHandler(this.btn_process_Click);
            this.btn_cancle.Click += new EventHandler(this.btn_cancle_Click);
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btn_process_Click(object sender, EventArgs e)
        {
            if (this.rdo건별.Checked)
                this.ht.Add("건별일괄", true);
            else
                this.ht.Add("건별일괄", false);

            this.ht.Add("부가세계정코드", this.bpctx부가세계정.CodeValue);
            this.ht.Add("부가세계정명", this.bpctx부가세계정.CodeName);
            
            this.DialogResult = DialogResult.OK;
        }

        internal Hashtable GetHashtable => this.ht;

        internal DataTable GetDatatable => this._dt;
    }
}
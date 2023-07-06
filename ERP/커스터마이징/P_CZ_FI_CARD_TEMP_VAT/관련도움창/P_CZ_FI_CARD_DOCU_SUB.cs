using Duzon.BizOn.Erpu.Resource;
using Duzon.Common.Controls;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_FI_CARD_DOCU_SUB : Duzon.Common.Forms.CommonDialog
    {
        private Hashtable ht = new Hashtable();
        private P_CZ_FI_CARD_TEMP_VAT_BIZ _biz;
        private DataTable _dt;

        public P_CZ_FI_CARD_DOCU_SUB()
        {
            this.InitializeComponent();

            this._biz = new P_CZ_FI_CARD_TEMP_VAT_BIZ();
            this.btn_건별.Click += new EventHandler(this.btn_건별_Click);
            this.btn_일괄.Click += new EventHandler(this.btn_일괄_Click);
        }

        private void btn_일괄_Click(object sender, EventArgs e)
        {
            this.ht.Add((object)"건별일괄", (object)false);
            this.DialogResult = DialogResult.OK;
        }

        private void btn_건별_Click(object sender, EventArgs e)
        {
            this.ht.Add((object)"건별일괄", (object)true);
            this.DialogResult = DialogResult.OK;
        }

        internal Hashtable GetHashtable => this.ht;

        internal DataTable GetDatatable => this._dt;
    }
}
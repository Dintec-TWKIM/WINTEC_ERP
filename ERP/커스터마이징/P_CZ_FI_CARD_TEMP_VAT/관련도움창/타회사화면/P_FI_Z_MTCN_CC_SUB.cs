using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.Windows.OneControls;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_FI_Z_MTCN_CC_SUB : Duzon.Common.Forms.CommonDialog
    {
        public Hashtable ht;
        
        public P_FI_Z_MTCN_CC_SUB()
        {
            this.InitializeComponent();
            this.ht = new Hashtable();
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitEvent();
        }

        private void InitEvent()
        {
            this.btn확인.Click += new EventHandler(this.btn확인_Click);
            this.btn취소.Click += new EventHandler(this.btn취소_Click);
        }

        private void btn취소_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn확인_Click(object sender, EventArgs e)
        {
            try
            {
                this.ht.Add((object)"계정", (object)this.bpt계정.CodeValue);
                this.ht.Add((object)"계정명", (object)this.bpt계정.CodeName);
                this.ht.Add((object)"코스트센터", (object)this.bpt코스트센터.CodeValue);
                this.ht.Add((object)"코스트센터명", (object)this.bpt코스트센터.CodeName);
                this.DialogResult = DialogResult.Yes;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
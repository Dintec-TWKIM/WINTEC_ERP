using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_FI_Z_MNS_CARD_VAT_ACCT_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_FI_CARD_TEMP_VAT_BIZ _biz;
        
        public P_FI_Z_MNS_CARD_VAT_ACCT_SUB()
        {
            this.InitializeComponent();
            this._biz = new P_CZ_FI_CARD_TEMP_VAT_BIZ();
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitEvent();
            this.InitGrid();
            this.조회();
        }

        private void InitEvent()
        {
            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn저장.Click += new EventHandler(this.btn저장_Click);
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, true);
            this._flex.SetCol("SEQ", "NO", 60, false, typeof(int));
            this._flex.SetCol("CD_ACCT", "계정코드", 80, false);
            this._flex.SetCol("NM_ACCT", "계정명", 100, false);
            this._flex.ExtendLastCol = true;
            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flex.SetCodeHelpCol("CD_ACCT", HelpID.P_FI_ACCTCODE_SUB, ShowHelpEnum.Always, new string[2]
            {
        "CD_ACCT",
        "NM_ACCT"
            }, new string[2] { "CD_ACCT", "NM_ACCT" }, ResultMode.FastMode);
        }

        protected override void InitPaint()
        {
        }

        private void btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex.Col = this._flex.Cols["CD_ACCT"].Index;
                this._flex["SEQ"] = (object)(this._flex.Rows.Count - 1);
                this._flex.AddFinished();
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Rows.Remove(this._flex.Row);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn저장_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Save())
                    return;
                this._flex.AcceptChanges();
                int num = (int)Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 조회()
        {
            try
            {
                List<object> objectList = new List<object>();
                this._flex.Binding = this._biz.Search_VAT_ACCT_SUB(new string[1]
                {
          MA.Login.회사코드
                });
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool Save()
        {
            try
            {
                DataTable changes = this._flex.GetChanges();
                if (changes == null)
                {
                    int num = (int)Global.MainFrame.ShowMessage(공통메세지.변경된내용이없습니다);
                    return false;
                }
                this._biz.Save_VAT_ACCT_SUB(changes);
                this._flex.AcceptChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            return false;
        }
    }
}
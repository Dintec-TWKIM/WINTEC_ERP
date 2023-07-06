﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.Common.BpControls;
using Duzon.Common.Forms.Help;

namespace cz
{
    public partial class P_CZ_PR_MATCHING_ITEM_SUB : Duzon.Common.Forms.CommonDialog
    {
        P_CZ_PR_MATCHING_ITEM_SUB_BIZ _biz = new P_CZ_PR_MATCHING_ITEM_SUB_BIZ();

        public P_CZ_PR_MATCHING_ITEM_SUB()
        {
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            DataSet comboData = Global.MainFrame.GetComboData(new string[] { "NC;MA_PLANT" });

            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.ValueMember = "CODE";
            this.cbo공장.DisplayMember = "NAME";

            this.btn추가.Enabled = false;
            this.btn삭제.Enabled = false;
            this.btn저장.Enabled = false;
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("CD_ITEM", "모품목", 100, true);
            this._flex.SetCol("NM_ITEM", "모품명", 100, false);
            this._flex.SetCol("CD_PITEM", "자품목", 100, true);
            this._flex.SetCol("NM_PITEM", "자품명", 100, false);
            this._flex.SetCol("TP_POS", "위치", 100, true);

            this._flex.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM" }, new string[] { "CD_ITEM", "NM_ITEM" });
            this._flex.SetCodeHelpCol("CD_PITEM", HelpID.P_MA_PITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_PITEM", "NM_PITEM" }, new string[] { "CD_ITEM", "NM_ITEM" });
            this._flex.SetDataMap("TP_POS", MA.GetCodeUser(new string[] { "001", "002", "003", "004", "005" }, new string[] { "1번째", "2번째", "3번째", "4번째", "5번째" }), "CODE", "NAME");

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
			this.btn자동등록.Click += Btn자동등록_Click;

            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn저장.Click += new EventHandler(this.btn저장_Click);
            this.btn닫기.Click += new EventHandler(this.btn닫기_Click);

            this.ctx모품목.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx자품목.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

            this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
        }

        private void Btn자동등록_Click(object sender, EventArgs e)
        {
            try
            {
                if (DBHelper.ExecuteNonQuery("SP_CZ_PR_MATCHING_ITEM_AUTO", new object[] { Global.MainFrame.LoginInfo.CompanyCode }))
                    Global.MainFrame.ShowMessage("자동등록 작업이 완료되었습니다.");
                else
                    Global.MainFrame.ShowMessage("자동등록 작업이 실패되었습니다.");
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                e.Parameter.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(D.GetString(this.cbo공장.SelectedValue)))
                {
                    Global.MainFrame.ShowMessage(공통메세지._자료가선택되어있지않습니다, new string[] { this.lbl공장.Text });

                    this.cbo공장.Focus();
                    e.QueryCancel = true;
                }
                else
                {
                    e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     D.GetString(this.cbo공장.SelectedValue),
                                                                     this.ctx모품목.CodeValue,
                                                                     this.ctx자품목.CodeValue });

                if (this._flex.DataTable == null || this._flex.DataTable.Rows.Count == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }

                this.btn추가.Enabled = true;
                this.btn삭제.Enabled = true;
                this.btn저장.Enabled = true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex.Rows.Add();

                this._flex["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;

                this._flex.AddFinished();
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
                if (this._flex.Row < 0) return;

                this._flex.RemoveItem(this._flex.Row);
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
                if (!this._flex.IsDataChanged) return;
                
                if (!this._biz.Save(this._flex.GetChanges()))
                    return;

                this._flex.AcceptChanges();

                Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn닫기_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

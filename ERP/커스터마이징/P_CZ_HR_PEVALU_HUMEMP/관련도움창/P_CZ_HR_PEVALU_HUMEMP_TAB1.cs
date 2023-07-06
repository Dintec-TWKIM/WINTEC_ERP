using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using System.Diagnostics;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;

namespace cz
{
    public partial class P_CZ_HR_PEVALU_HUMEMP_TAB1 : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_HR_PEVALU_HUMEMP_BIZ _biz = new P_CZ_HR_PEVALU_HUMEMP_BIZ();

        public DataRow[] ReturnValues
        {
            get
            {
                return this._flex.DataTable.Select("S = 'Y'");
            }
        }

        public P_CZ_HR_PEVALU_HUMEMP_TAB1()
        {
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitGrid();
            this.InitEvent();
            this.InitControl();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NM_COMPANY", "회사명", 110, false);
            this._flex.SetCol("NM_BIZAREA", "사업장", 110, false);
            this._flex.SetCol("NM_DEPT", "부서명", 110, false);
            this._flex.SetCol("NO_EMP", "사번", 110, false);
            this._flex.SetCol("NM_KOR", "성명(한글)", 110, false);
            this._flex.SetCol("NM_DUTY_RANK", "직위명", 110, false);
            this._flex.SetCol("DT_ENTER", "입사일자", 110, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_RETIRE", "퇴직일자", 110, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex.SetDummyColumn("S");
            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn확인.Click += new EventHandler(this.btn확인_Click);
            this.btn취소.Click += new EventHandler(this.btn취소_Click);

            this.ctx회사.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.bpc사업장.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.bpc사업장.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.bpc부서.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.bpc사원구분.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.bpc직급.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.bpc직책.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.bpc직위.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
        }

        private void InitControl()
        {
            this.dtp입사일자.Text = Global.MainFrame.GetStringDetailToday;
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.oneGrid1.UseCustomLayout = false;
            this.oneGrid1.IsSearchControl = false;
            this.oneGrid1.InitCustomLayout();
        }

        private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.ControlName == this.bpc사업장.Name)
                {
                    if (this.ctx회사.IsEmpty())
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl회사.Text);
                        this.ctx회사.Focus();
                        e.QueryCancel = true;
                        return;
                    }
                    e.HelpParam.P01_CD_COMPANY = this.ctx회사.CodeValue;
                }
                else if (e.ControlName == this.bpc부서.Name)
                {
                    if (this.bpc사업장.IsEmpty())
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl사업장.Text);
                        this.bpc사업장.Focus();
                        e.QueryCancel = true;
                        return;
                    }
                    e.HelpParam.P01_CD_COMPANY = this.ctx회사.CodeValue;
                    e.HelpParam.P26_AUTH_BIZAREA = this.bpc사업장.QueryWhereIn_WithValueMember;
                }
                else if (e.ControlName == this.bpc사원구분.Name)
                {
                    if (this.ctx회사.IsEmpty())
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl회사.Text);
                        this.ctx회사.Focus();
                        e.QueryCancel = true;
                        return;
                    }

                    e.HelpParam.P01_CD_COMPANY = this.ctx회사.CodeValue;
                    e.HelpParam.P41_CD_FIELD1 = "HR_P000009";
                }
                else if (e.ControlName == this.bpc직급.Name)
                {
                    if (this.ctx회사.IsEmpty())
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl회사.Text);
                        this.ctx회사.Focus();
                        e.QueryCancel = true;
                        return;
                    }

                    e.HelpParam.P01_CD_COMPANY = this.ctx회사.CodeValue;
                    e.HelpParam.P41_CD_FIELD1 = "HR_H000003";
                }
                else if (e.ControlName == this.bpc직책.Name)
                {
                    if (this.ctx회사.IsEmpty())
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl회사.Text);
                        this.ctx회사.Focus();
                        e.QueryCancel = true;
                        return;
                    }
                    
                    e.HelpParam.P01_CD_COMPANY = this.ctx회사.CodeValue;
                    e.HelpParam.P41_CD_FIELD1 = "HR_H000052";
                }
                else if (e.ControlName == this.bpc직위.Name)
                {
                    if (this.ctx회사.IsEmpty())
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl회사.Text);
                        this.ctx회사.Focus();
                        e.QueryCancel = true;
                        return;
                    }

                    e.HelpParam.P01_CD_COMPANY = this.ctx회사.CodeValue;
                    e.HelpParam.P41_CD_FIELD1 = "HR_H000002";
                }

            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.DialogResult != DialogResult.OK) return;

                if (e.ControlName == this.ctx회사.Name)
                {
                    this.bpc사업장.Clear();
                    this.bpc부서.Clear();
                    this.bpc사원구분.Clear();
                    this.bpc직급.Clear();
                    this.bpc직책.Clear();
                    this.bpc직위.Clear();
                }
                else if (e.ControlName == this.bpc사업장.Name)
                {
                    this.bpc부서.Clear();
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
                DataTable dt = this._biz.SearchTab(new object[] { this.ctx회사.CodeValue,
                                                                  this.dtp입사일자.Text,
                                                                  this.bpc사업장.QueryWhereIn_Pipe,
                                                                  this.bpc부서.QueryWhereIn_Pipe,
                                                                  this.bpc사원구분.QueryWhereIn_Pipe,
                                                                  this.bpc직위.QueryWhereIn_Pipe,
                                                                  this.bpc직급.QueryWhereIn_Pipe,
                                                                  this.bpc직책.QueryWhereIn_Pipe,
                                                                  this.txt검색.Text });
                this._flex.Binding = dt;

                if (!this._flex.HasNormalRow)
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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
                if (this._flex.DataTable.Select("S = 'Y'").Length == 0)
                    this.DialogResult = DialogResult.Cancel;
                else
                    this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
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
    }
}

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PR_OPOUT_PO_SUB : Duzon.Common.Forms.CommonDialog
    {
        P_CZ_PR_OPOUT_PO_SUB_BIZ _biz = new P_CZ_PR_OPOUT_PO_SUB_BIZ();
        private object[] m_re = new object[3];
        public DataRow drReturn;
        public string No_Po;

        public P_CZ_PR_OPOUT_PO_SUB(object[] m_result)
        {
            this.InitializeComponent();

            this.InitEvent();

            this.ctx공장.CodeValue = m_result[0].ToString();
            this.ctx공장.CodeName = m_result[1].ToString();
            this.ctx외주처.CodeValue = m_result[2].ToString();
            this.ctx외주처.CodeName = m_result[3].ToString();

            this.dtp발주기간.StartDateToString = Global.MainFrame.GetStringToday.Substring(0, 6) + "01";
            this.dtp발주기간.EndDateToString = Global.MainFrame.GetStringToday;
        }

        private void InitEvent()
		{
            this.Load += new EventHandler(this.Page_Load);
            this.btn검색.Click += new EventHandler(this.OnSearch);
            this.btn확인.Click += new EventHandler(this.OnApply);
            this.btn취소.Click += new EventHandler(this.OnCancel);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitGridM();
                this.InitGridD();

                this._flexH.DetailGrids = new FlexGrid[] { this._flexL };
                this.ctx공장.Focus();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

		private void InitGridM()
        {
            this._flexH.BeginSetting(1, 1, false);
            
            this._flexH.SetCol("NO_PO", "발주번호", 100, false);
            this._flexH.SetCol("DT_PO", "발주일자", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("LN_PARTNER", "외주처명", 140, false);
            this._flexH.SetCol("DC_RMK", "비고", 100, false);
            
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexM_AfterRowChange);
            this._flexH.DoubleClick += new EventHandler(this._flexM_DoubleClick);
        }

        private void InitGridD()
        {
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("CD_WC", "작업장", 50, false);
            this._flexL.SetCol("NM_WC", "작업장명", 100, false);
            this._flexL.SetCol("CD_WCOP", "공정", 50, false);
            this._flexL.SetCol("NM_OP", "공정명", 100, false);
            this._flexL.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flexL.SetCol("CD_OP", "OP", 25, false);
            this._flexL.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexL.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexL.SetCol("STND_ITEM", "규격", 120, false);
            this._flexL.SetCol("UNIT_IM", "단위", 40, false);
            this._flexL.SetCol("DT_DUE", "납기일자", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("QT_PO", "발주수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_RCV", "입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("UM_EX", "외화단가", 90, 17, true, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_EX", "외화금액", 90, 17, true, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("UM", "단가", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM", "금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("NO_PJT", "프로젝트번호", 100, false);
            this._flexL.SetCol("NM_PJT", "프로젝트명", 100, false);
            this._flexL.SetCol("NO_DESIGN", "도면번호", 100, false);

            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);

            this._flexL.Cols["QT_RCV"].Visible = false;
            this._flexL.Cols["QT_CLS"].Visible = false;
        }

        private bool DoContinue()
        {
            return this._flexH.Editor == null || this._flexH.FinishEditing(false);
        }

        private bool SearchCondition()
        {
            if (this.ctx공장.CodeValue == "")
            {
                Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                this.ctx공장.Focus();
                return false;
            }

            if (this.dtp발주기간.StartDateToString == string.Empty ||
                this.dtp발주기간.EndDateToString == string.Empty)
            {
                Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl발주기간.Text);
                this.dtp발주기간.Focus();
                return false;
            }

            if (!(Convert.ToDecimal(this.dtp발주기간.StartDateToString) > Convert.ToDecimal(this.dtp발주기간.EndDateToString)))
                return true;
            
            Global.MainFrame.ShowMessage(공통메세지.시작일자보다종료일자가클수없습니다);
            this.dtp발주기간.Focus();
            
            return false;
        }

        private void OnSearch(object sender, EventArgs e)
        {
            try
            {
                if (!this.DoContinue() || !this.SearchCondition())
                    return;

                this._flexH.Binding = this._biz.search(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                      this.ctx공장.CodeValue,
                                                                      this.ctx외주처.CodeValue,
                                                                      this.dtp발주기간.StartDateToString,
                                                                      this.dtp발주기간.EndDateToString,
                                                                      Global.MainFrame.LoginInfo.EmployeeNo,
                                                                      Global.SystemLanguage.MultiLanguageLpoint });

                if (!this._flexH.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void OnApply(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow)
                    return;
                this.drReturn = this._flexH.DataView[this._flexH.Row - 1].Row;
                this.No_Po = this._flexH["NO_PO"].ToString();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void OnCancel(object sender, EventArgs e)
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

        private void _flexM_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this.ChangeFilter();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ChangeFilter()
        {
            DataTable dt = new DataTable();
            string filter = "NO_PO = '" + this._flexH["NO_PO"].ToString() + "'";

            object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                               this.ctx공장.CodeValue,
                                               this.ctx외주처.CodeValue,
                                               this.dtp발주기간.StartDateToString,
                                               this.dtp발주기간.EndDateToString,
                                               this._flexH["NO_PO"].ToString(),
                                               Global.MainFrame.LoginInfo.EmployeeNo,
                                               Global.SystemLanguage.MultiLanguageLpoint };

            if (this._flexH.DetailQueryNeed)
                dt = this._biz.SearchDetail(objArray);
            
            this._flexL.BindingAdd(dt, filter);
            this._flexH.DetailQueryNeed = false;
        }

        private void _flexM_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.OnApply(null, null);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void OnControlValidated(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is DatePicker datePicker) || datePicker.Text == "")
                    return;
                if (datePicker.Text.Length != 8)
                {
                    Global.MainFrame.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                    datePicker.Text = "";
                    datePicker.Focus();
                }
                else if (!datePicker.IsValidated)
                {
                    Global.MainFrame.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                    datePicker.Text = "";
                    datePicker.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void P_CZ_PR_OPOUT_PO_SUB_Load(object sender, EventArgs e)
        {

        }
    }
}

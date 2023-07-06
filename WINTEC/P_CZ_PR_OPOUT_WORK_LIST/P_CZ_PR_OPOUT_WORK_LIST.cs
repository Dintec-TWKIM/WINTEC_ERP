using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
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
	public partial class P_CZ_PR_OPOUT_WORK_LIST : PageBase
    {
        private P_CZ_PR_OPOUT_WORK_LIST_BIZ _biz = new P_CZ_PR_OPOUT_WORK_LIST_BIZ();
        public P_CZ_PR_OPOUT_WORK_LIST()
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

            this.oneGrid1.UseCustomLayout = true;
            this.bpP_Work.IsNecessaryCondition = true;
            this.bpP_Bizarea.IsNecessaryCondition = true;
            this.bpP_Plant.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
            DataSet comboData = this.GetComboData("NC;MA_BIZAREA", "NC;MA_PLANT", "S;MA_B000004");
            this.cbo사업장.DataSource = comboData.Tables[0];
            this.cbo사업장.DisplayMember = "NAME";
            this.cbo사업장.ValueMember = "CODE";
            this.cbo공장.DataSource = comboData.Tables[1];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";
            this._flex.SetDataMap("UNIT_IM", comboData.Tables[2].Copy(), "CODE", "NAME");
            this._flex.SetDataMap("UNIT_CH", comboData.Tables[2].Copy(), "CODE", "NAME");
            this.dat_Work.Mask = this.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dat_Work.StartDate = this.MainFrameInterface.GetDateTimeToday();
            this.dat_Work.StartDateToString = this.MainFrameInterface.GetStringFirstDayInMonth;
            this.dat_Work.EndDate = this.MainFrameInterface.GetDateTimeToday();
            this.dat_Work.EndDateToString = this.MainFrameInterface.GetStringToday;
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("NO_WORK", "실적번호", 50, false);
            this._flex.SetCol("LN_PARTNER", "외주처", 50, false);
            this._flex.SetCol("CD_ITEM", "품목코드", 50, false);
            this._flex.SetCol("NM_ITEM", "품목명", 50, false);
            this._flex.SetCol("STND_ITEM", "규격", 50, false);
            this._flex.SetCol("UNIT_IM", "단위", 50, false);
            this._flex.SetCol("QT_ITEM", "지시수량", 50, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_WORK", "실적수량", 50, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_PO", "발주수량", 50, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("UM_WORK", "단가", 58, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_WORK", "금액", 58, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_VAT_WORK", "부가세", 58, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_HAP_WORK", "총금액", 58, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("NO_WO", "지시번호", 50, false);
            this._flex.SetCol("NO_PO", "발주번호", 50, false);
            this._flex.SetCol("DT_WORK", "실적일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_WC", "작업장", 50, false);
            this._flex.SetCol("NM_OP", "공정", 50, false);
            this._flex.SetCol("DC_RMK", "발주비고", 100, false);
            this._flex.SetCol("UNIT_CH", "변환단위", 100, false);
            this._flex.SetCol("QT_CHCOEF", "변환계수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("OLD_QT_PO", "변환전수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_WORK_CHCOEF", "실적수량(변환)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_WORK_BAD_CHCOEF", "불량수량(변환)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("UM", "단가(공정외주)", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("AM", "금액(공정외주)", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_VAT", "부가세(공정외주)", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_TOTAL", "합계(공정외주)", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("NO_LINE", "발주항번", 50, false);
            this._flex.SetCol("QT_MOVE", "이동수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_REJECT", "불량수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_REWORK", "재작업수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_BAD", "불량처리수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("NM_REJECT", "불량종류", 100);
            this._flex.SetCol("NM_RESOURCE", "불량원인", 100);
            this._flex.SetCol("UM_MOVE_EX", "외화단가", 90, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("UM_MOVE", "원화단가", 90, 17, false, typeof(decimal), FormatTpType.UNIT_COST);
            this._flex.SetCol("AM_MOVE_EX", "외화금액", 90, 17, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_MOVE", "원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_VAT_MOVE", "부가세", 90, 17, true, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_HAP_MOVE", "합계금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("EN_ITEM", "품목명(영)", false);
            this._flex.SetCol("STND_DETAIL_ITEM", "세부규격", false);
            this._flex.SetCol("NM_MAKER", "Maker", false);
            this._flex.SetCol("BARCODE", "BARCODE", false);
            this._flex.SetCol("NO_MODEL", "모델번호", false);
            this._flex.SetCol("NO_DESIGN", "도면번호", false);
            this._flex.SetCol("MAT_ITEM", "재질", false);
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.ctx작업장.CodeChanged += new EventHandler(this.OnBpControl_CodeChanged);
            this.ctx작업장.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx공정.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx작업장.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx품목To.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx품목From.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
        }

		private void OnBpControl_CodeChanged(object sender, EventArgs e)
		{
            try
            {
                if (!(sender is BpCodeTextBox bpCodeTextBox))
                    return;
                switch (bpCodeTextBox.Name)
                {
                    case "bp_wc":
                        this.ctx공정.CodeValue = "";
                        this.ctx공정.CodeName = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
		{
            switch (e.HelpID)
            {
                case HelpID.P_MA_PITEM_SUB:
                    if (!this.공장선택여부)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                        this.cbo공장.Focus();
                        e.QueryCancel = true;
                        break;
                    }
                    e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                    break;
                case HelpID.P_MA_WC_SUB:
                    if (!this.공장선택여부)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                        this.cbo공장.Focus();
                        e.QueryCancel = true;
                        break;
                    }
                    e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                    break;
                case HelpID.P_PR_WCOP_SUB:
                    if (!this.공장선택여부)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                        this.cbo공장.Focus();
                        e.QueryCancel = true;
                        break;
                    }
                    if (this.ctx작업장.CodeValue == "")
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.ctx작업장.Text);
                        this.ctx작업장.Focus();
                        e.QueryCancel = true;
                        break;
                    }
                    e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                    e.HelpParam.P20_CD_WC = this.ctx작업장.CodeValue;
                    break;
            }
        }

        private void OnBpControl_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID != HelpID.P_MA_WC_SUB)
                    return;
                this.ctx공정.CodeValue = "";
                this.ctx공정.CodeName = "";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch())
                    return;
                this._flex.UnBinding = this._biz.search(new object[] { this.LoginInfo.CompanyCode,
                                                                       this.dat_Work.StartDateToString,
                                                                       this.dat_Work.EndDateToString,
                                                                       this.cbo사업장.SelectedValue.ToString(),
                                                                       this.cbo공장.SelectedValue.ToString(),
                                                                       this.ctx외주처.CodeValue,
                                                                       this.ctx작업장.CodeValue,
                                                                       this.ctx공정.CodeValue,
                                                                       this.ctx품목From.CodeValue,
                                                                       this.ctx품목To.CodeValue,
                                                                       Global.SystemLanguage.MultiLanguageLpoint,
                                                                       this.chk불량처리제외여부.Checked ? "Y" : "N" });
                if (!this._flex.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                    this._flex.ExecuteSubTotal(new SSInfo() { VisibleColumns = new string[] { "NO_WORK",
                                                                                              "LN_PARTNER",
                                                                                              "CD_ITEM",
                                                                                              "NM_ITEM",
                                                                                              "STND_ITEM",
                                                                                              "UNIT_IM",
                                                                                              "QT_ITEM",
                                                                                              "QT_WORK",
                                                                                              "QT_PO",
                                                                                              "UM_WORK",
                                                                                              "AM_WORK",
                                                                                              "AM_VAT_WORK",
                                                                                              "AM_HAP_WORK",
                                                                                              "NO_WO",
                                                                                              "NO_PO",
                                                                                              "DT_WORK",
                                                                                              "NM_WC",
                                                                                              "NM_OP",
                                                                                              "UNIT_CH" },
                                                              GroupColumns = new string[] { "NO_WORK" },
                                                              TotalColumns = new string[] { "AM_WORK",
                                                                                            "AM_VAT_WORK",
                                                                                            "AM_HAP_WORK",
                                                                                            "QT_CHCOEF",
                                                                                            "OLD_QT_PO",
                                                                                            "QT_WORK_CHCOEF",
                                                                                            "QT_WORK_BAD_CHCOEF" },
                                                              AccColumns = new string[] { "AM_WORK",
                                                                                          "AM_VAT_WORK",
                                                                                          "AM_HAP_WORK",
                                                                                          "QT_CHCOEF",
                                                                                          "OLD_QT_PO",
                                                                                          "QT_WORK_CHCOEF",
                                                                                          "QT_WORK_BAD_CHCOEF" },
                                                              CanGrandTotal = true });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch())
                return false;
            if (!this.사업장선택여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                this.cbo사업장.Focus();
                return false;
            }
            if (!this.공장선택여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                this.cbo공장.Focus();
                return false;
            }
            if (this.dat_Work.StartDateToString == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl실적일자.Text);
                this.dat_Work.Focus();
                return false;
            }
            if (this.dat_Work.EndDateToString == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl실적일자.Text);
                this.dat_Work.Focus();
                return false;
            }
            return Checker.IsValid(this.dat_Work, true, this.lbl실적일자.Text);
        }

        private bool 사업장선택여부 => this.cbo사업장.SelectedValue != null && !(this.cbo사업장.SelectedValue.ToString() == "");

        private bool 공장선택여부 => this.cbo공장.SelectedValue != null && !(this.cbo공장.SelectedValue.ToString() == "");
    }
}

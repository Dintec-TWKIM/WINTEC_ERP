using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_PR_OPOUT_PO_WORK_SUB : Duzon.Common.Forms.CommonDialog
	{
        private P_CZ_PR_OPOUT_PO_WORK_SUB_BIZ _biz = new P_CZ_PR_OPOUT_PO_WORK_SUB_BIZ();
        private string str_CdPlant;
        private DataTable dtReturn = null;
        private DataRow[] drsReturn = null;
        private string strCD_EXCH = "";
        private decimal dRT_EXCH = 0M;
        private string strExceptList = "";
        private string sPoDt = string.Empty;
        private decimal dRT_VatRate = 0M;
        private string sChcoef_YN = string.Empty;

        public P_CZ_PR_OPOUT_PO_WORK_SUB(string cdpartner,
                                         string nmpartner,
                                         string cdplant,
                                         string strCD_EXCH,
                                         decimal dRT_EXCH,
                                         ArrayList ar,
                                         string SDT_PO,
                                         decimal dVat_Rate)
        {
            this.InitializeComponent();

            this.str_CdPlant = cdplant;
            this.ctx외주처.CodeValue = cdpartner;
            this.ctx외주처.CodeName = nmpartner;
            this.strCD_EXCH = strCD_EXCH;
            this.dRT_EXCH = dRT_EXCH;
            this.sPoDt = SDT_PO;
            this.dRT_VatRate = dVat_Rate;
            
            foreach (string str in ar)
            {
                P_CZ_PR_OPOUT_PO_WORK_SUB prOpoutPoWorkSub = this;
                prOpoutPoWorkSub.strExceptList = prOpoutPoWorkSub.strExceptList + str + "|";
            }
            
            this.Load += new EventHandler(this.Page_Load);
            this.Paint += new PaintEventHandler(this.Page_Paint);

			this.btn검색.Click += new EventHandler(this.OnSearch);
            this.btn적용.Click += new EventHandler(this.OnApply);
            this.btn취소.Click += new EventHandler(this.OnCancel);


            this.chK작업지시비고적용.Checked = Settings1.Default.Wo_DcRmk_Apply_YN;
            this.sChcoef_YN = ComFunc.전용코드("공정외주임가공단가 단위 변환 사용");
            
            if (!(this.sChcoef_YN == string.Empty))
                return;
            
            this.sChcoef_YN = "000";
        }

		private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitGrid();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("CD_PARTNER", "외주처", 60, false);
            this._flex.SetCol("LN_PARTNER", "외주처명", 140, false);
            this._flex.SetCol("CD_WC", "작업장", 60, false);
            this._flex.SetCol("NM_WC", "작업장명", 120, false);
            this._flex.SetCol("CD_WCOP", "공정", 80, false);
            this._flex.SetCol("NM_OP", "공정명", 120, false);
            this._flex.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flex.SetCol("NM_TP_WO", "오더형태", 100, false);
            this._flex.SetCol("CD_OP", "OP.", 40, false);
            this._flex.SetCol("CD_ITEM", "품목코드", 100, false);
            this._flex.SetCol("NM_ITEM", "품목명", 140, false);
            this._flex.SetCol("STND_ITEM", "규격", 120, false);
            this._flex.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flex.SetCol("UNIT_IM", "단위", 40, false);
            this._flex.SetCol("QT_WO", "지시수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_OUTPO", "발주대상수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_OPOUT_PO", "발주수량(변환전)", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_APPLY_YET", "미발주수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_APPLY", "발주수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);

            if (this.sChcoef_YN != "000")
            {
                this._flex.SetCol("QT_CHCOEF", "변환계수", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flex.SetCol("UNIT_CH", "변환단위", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flex.SetCol("QT_APPLY_CALC", "발주수량(변환)", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            }

            this._flex.SetCol("UM_MATL", "도급자재비", 90, 17, true, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("UM_SOUL", "임가공비", 90, 17, true, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("UM_EX", "외화단가", 90, 17, true, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_EX", "외화금액", 90, 17, true, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("UM", "단가", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM", "금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("DT_REL", "시작일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_DUE", "종료일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NO_LOT", "LOT번호", 70, 8, false);
            this._flex.SetCol("NM_FG_SERNO", "LOT,S/N관리", false);
            this._flex.SetCol("NM_GRP_ITEM", "품목군", false);
            this._flex.SetCol("DC_RMK1", "비고1", false);
            this._flex.SetCol("NO_WORK", "작업실적번호", false);
            this._flex.SetCol("NO_PJT", "프로젝트번호", 100, false);
            this._flex.SetCol("NM_PJT", "프로젝트명", 100, false);
            this._flex.SetCol("DC_RMK_PR", "요청비고", 100, false);

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex.LoadUserCache("P_PR_OPOUT_PO_WORK_SUB_flex");
            this._flex.SetDummyColumn("CHK");
            
            this._flex.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
        }

        private void Page_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                this.Paint -= new PaintEventHandler(this.Page_Paint);

                this.cbo환종.DataSource = Global.MainFrame.GetComboData("N;MA_B000005").Tables[0];
                this.cbo환종.DisplayMember = "NAME";
                this.cbo환종.ValueMember = "CODE";
                this.cbo환종.SelectedValue = this.strCD_EXCH;
                this.cur환율.DecimalValue = this.dRT_EXCH;
                this.dtp작업기간.StartDateToString = Global.MainFrame.GetStringToday.Substring(0, 6) + "01";
                this.dtp작업기간.EndDateToString = Global.MainFrame.GetStringToday;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void OnSearch(object sender, EventArgs e)
        {
            try
            {
                object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                   this.str_CdPlant,
                                                   this.ctx외주처.CodeValue,
                                                   this.dtp작업기간.StartDateToString,
                                                   this.dtp작업기간.EndDateToString,
                                                   this.cbo환종.SelectedValue.ToString(),
                                                   this.cur환율.DecimalValue,
                                                   this.strExceptList,
                                                   this.sPoDt,
                                                   this.dRT_VatRate,
                                                   this.txt작업지시번호.Text,
                                                   this.txtLOT번호.Text,
                                                   this.bpc오더형태.QueryWhereIn_Pipe,
                                                   this.chK작업지시비고적용.Checked ? "Y" : "N",
                                                   Global.MainFrame.LoginInfo.EmployeeNo };
                
                DataTable dataTable = null;

                if (this.sChcoef_YN == "000")
                    dataTable = this._biz.search(objArray);
                else if (this.sChcoef_YN == "001")
                    dataTable = this._biz.search_Chcoef(objArray);
                
                this._flex.Binding = dataTable;
                
                if (!this._flex.HasNormalRow)
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
                if (this._flex.DataView == null || this._flex.DataView.Count == 0)
                    return;

                this.drsReturn = this._flex.DataTable.Select("CHK = 'Y'", "", DataViewRowState.CurrentRows);
                string empty = string.Empty;
                
                if (this.drsReturn.Length == 0)
                {
                    Global.MainFrame.ShowMessage("체크된 건이 없습니다.");
                }
                else
                {
                    object obj = this._flex.DataTable.Compute("SUM(QT_APPLY)", "CHK = 'Y'");
                    decimal result = 0M;
                    decimal.TryParse(obj.ToString(), out result);
                    if (result == 0M)
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은_보다커야합니다, this._flex.Cols["QT_APPLY"].Caption, "0");
                    }
                    else
                    {
                        this.dtReturn = this._flex.DataTable.Clone();

                        foreach (DataRow dataRow in this.drsReturn)
                        {
                            if (empty == string.Empty)
                                empty = dataRow["CD_PARTNER"].ToString();
                            else if (empty != dataRow["CD_PARTNER"].ToString())
                            {
                                Global.MainFrame.ShowMessage("서로 다른 외주처의 데이터를 선택할 수 없습니다");
                                return;
                            }

                            this.dtReturn.Rows.Add(dataRow.ItemArray);
                        }

                        this.DialogResult = DialogResult.OK;
                    }
                }
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

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                if (!(sender is FlexGrid flexGrid))
                    return;

                switch (flexGrid.Cols[e.Col].Name)
                {
                    case "QT_APPLY":
                    case "UM_EX":
                        if (flexGrid.CDecimal(flexGrid.EditData) > flexGrid.CDecimal(flexGrid["QT_APPLY_YET"]))
                        {
                            Global.MainFrame.ShowMessage(공통메세지._은_보다작거나같아야합니다, flexGrid.Cols[e.Col].Caption, flexGrid.Cols["QT_APPLY_YET"].Caption);
                            e.Cancel = true;
                            break;
                        }
                        if (this.sChcoef_YN == "000")
                        {
                            flexGrid["UM"] = Unit.원화단가(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * this.cur환율.DecimalValue);
                            flexGrid["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * D.GetDecimal(flexGrid["QT_APPLY"]));
                            flexGrid["AM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM"]) * D.GetDecimal(flexGrid["QT_APPLY"]) * this.cur환율.DecimalValue);
                            flexGrid["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM"]) * this.dRT_EXCH * 0.01M);
                            break;
                        }
                        int num1 = D.GetDecimal(flexGrid["QT_APPLY"]) == 0M ? 0 : (!(D.GetDecimal(flexGrid["QT_CHCOEF"]) == 0M) ? 1 : 0);
                        flexGrid["QT_APPLY_CALC"] = num1 != 0 ? (D.GetDecimal(flexGrid["QT_APPLY"]) * D.GetDecimal(flexGrid["QT_CHCOEF"])) : 0;
                        flexGrid["UM"] = Unit.원화단가(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * this.cur환율.DecimalValue);
                        flexGrid["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * D.GetDecimal(flexGrid["QT_APPLY_CALC"]));
                        flexGrid["AM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM"]) * D.GetDecimal(flexGrid["QT_APPLY_CALC"]) * this.cur환율.DecimalValue);
                        flexGrid["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM"]) * this.dRT_EXCH * 0.01M);
                        break;
                    case "AM_EX":
                        if (D.GetDecimal(flexGrid["QT_APPLY"]) == 0M || D.GetDecimal(flexGrid["AM_EX"]) == 0M)
                            flexGrid["UM_EX"] = 0;
                        flexGrid["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM_EX"]) / D.GetDecimal(flexGrid["QT_APPLY"]));
                        flexGrid["UM"] = Unit.원화단가(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * this.cur환율.DecimalValue);
                        flexGrid["AM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM"]) * D.GetDecimal(flexGrid["QT_APPLY"]));
                        flexGrid["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM"]) * 0.01M);
                        break;
                    case "UM_MATL":
                    case "UM_SOUL":
                        flexGrid["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_MATL"]) + D.GetDecimal(flexGrid["UM_SOUL"]));
                        flexGrid["UM"] = Unit.원화단가(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * this.cur환율.DecimalValue);
                        flexGrid["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * D.GetDecimal(flexGrid["QT_APPLY"]));
                        flexGrid["AM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM"]) * D.GetDecimal(flexGrid["QT_APPLY"]));
                        flexGrid["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM"]) * 0.01M);
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case HelpID.P_MA_PARTNER_SUB:
                        e.HelpParam.MainFrame = Global.MainFrame;
                        break;
                    case HelpID.P_PR_TPWO_SUB1:
                        e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            this._flex.SaveUserCache("P_PR_OPOUT_PO_WORK_SUB_flex");
            Settings1.Default.Wo_DcRmk_Apply_YN = this.chK작업지시비고적용.Checked;
            Settings1.Default.Save();
        }

        public DataRow[] GetReturnDataRowArray
        {
            get
            {
                return this.drsReturn;
            }
        }

        public DataTable GetReturnDataTable
        {
            get
            {
                return this.dtReturn;
            }
        }
    }
}

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
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
	public partial class P_CZ_PR_OPOUT_IV_APPLY_SUB : Duzon.Common.Forms.CommonDialog
	{
        DataTable dtReturn = new DataTable();
        string str과세구분 = "";
        string str거래구분 = "";
        string str계산서처리구분 = "";
        string str공장;
        string str거래처코드 = "";
        string str거래처명 = "";
        string str처리일자 = "";
        string sChcoef_YN = string.Empty;
        P_CZ_PR_OPOUT_IV_APPLY_SUB_BIZ _biz = new P_CZ_PR_OPOUT_IV_APPLY_SUB_BIZ();

        public P_CZ_PR_OPOUT_IV_APPLY_SUB()
        {
            try
            {
                this.InitializeComponent();
                this.sChcoef_YN = ComFunc.전용코드("공정외주임가공단가 단위 변환 사용");
                this._flexM.WhenRowChangeThenGetDetail = false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public P_CZ_PR_OPOUT_IV_APPLY_SUB(string 처리일자)
        {
            try
            {
                this.InitializeComponent();
                this.sChcoef_YN = ComFunc.전용코드("공정외주임가공단가 단위 변환 사용");
                this.str처리일자 = 처리일자;
                this._flexM.WhenRowChangeThenGetDetail = false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitEvent()
        {
            this.btnEXCH.Click += new EventHandler(this.btnEXCH_Click);
            this.cbo환종.Click += new EventHandler(this.cbo환종_Click);
            // ISSUE: method pointer
            this.bp품목.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.btn단가변경.Click += new EventHandler(this.btn변경_Click);
            this.btn_apply_mst_um.Click += new EventHandler(this.btn_apply_mst_um_Click);
            this.btn_apply_um.Click += new EventHandler(this.btn_apply_mst_um_Click);
            this.btn취소.Click += new EventHandler(this.OnCancel_Click);
            this.btn조회.Click += new EventHandler(this.OnSearch_Click);
            this.btn적용.Click += new EventHandler(this.OnApply_Click);

            // ISSUE: method pointer
            this._flexM.ValidateEdit += new ValidateEditEventHandler(this._flexM_ValidateEdit);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            SetControl setControl = new SetControl();
            setControl.SetCombobox(this.cbo공장, Global.MainFrame.GetComboData(new string[] { "NC;MA_PLANT" }).Tables[0]);
            setControl.SetCombobox(this.cbo환종, MA.GetCode("MA_B000005"));
            this._flexM.SetDataMap("CD_EXCH", MA.GetCode("MA_B000005"), "CODE", "NAME");
            this.per기간.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            this.per기간.EndDateToString = Global.MainFrame.GetStringToday;
            if (this.str공장 != string.Empty)
                this.cbo공장.SelectedValue = this.str공장;
            else
                this.cbo공장.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;
            if (this.str과세구분 != string.Empty)
            {
                this.bp거래처.Enabled = false;
                this.bp거래처.SetCode(this.str거래처코드, this.str거래처명);
            }
            if (this.str처리일자 == string.Empty)
                this.str처리일자 = Global.MainFrame.GetStringToday;
            if (Global.MainFrame.LoginInfo.CompanyLanguage == 0)
                return;
            this.cur원화합계.CurrencyDecimalDigits = 2;
            this.cur총합계.CurrencyDecimalDigits = 2;
            this.cur부가세합계.CurrencyDecimalDigits = 2;
            this.cur변경단가.CurrencyDecimalDigits = 2;
        }

        private void InitGrid()
        {
            this._flexM.BeginSetting(1, 1, false);
            this._flexM.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            this._flexM.SetCol("CD_PARTNER", "거래처코드", 100, false);
            this._flexM.SetCol("LN_PARTNER", "거래처명", 120, false);
            this._flexM.SetCol("NO_WORK", "실적번호", 100, false);
            this._flexM.SetCol("DT_WORK", "실적일자", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("CD_ITEM", "품목코드", 100, false);
            this._flexM.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexM.SetCol("STND_ITEM", "규격", 120, false);
            this._flexM.SetCol("UNIT_IM", "단위", 40, false);
            if (this.sChcoef_YN == "000")
                this._flexM.SetCol("QT_MOVE", "수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            else
                this._flexM.SetCol("QT_MOVE", "수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("CD_EXCH", "환종", 90, false);
            this._flexM.SetCol("UM_MATL", "도급재료비", 90, 17, true, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexM.SetCol("UM_SOUL", "임가공비", 90, 17, true, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexM.SetCol("UM_EX", "외화단가", 90, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexM.SetCol("UM_WORK", "원화단가", 90, 17, false, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexM.SetCol("AM_EX", "외화금액", 90, 17, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexM.SetCol("AM_WORK", "원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexM.SetCol("AM_VAT_WORK", "부가세", 90, 17, true, typeof(decimal), FormatTpType.MONEY);
            this._flexM.SetCol("AM_HAP_WORK", "합계금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexM.SetCol("FG_TAX", "과세구분코드", false);
            this._flexM.SetCol("NM_FG_TAX", "과세구분", 120, false);
            this._flexM.SetCol("FG_TRANS", "거래구분코드", false);
            this._flexM.SetCol("NM_FG_TRANS", "거래구분", 100, false);
            this._flexM.SetCol("NO_WO", "지시번호", 100, false);
            this._flexM.SetCol("NO_PO", "발주번호", 100, false);
            this._flexM.SetCol("NO_POLINE", "발주항번", 60, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("NO_EMP", "담당자코드", false);
            this._flexM.SetCol("NM_KOR", "담당자", 100, false);
            this._flexM.SetCol("DC_RMK1", "비고", 100, false);
            this._flexM.SetCol("DC_RMK2", "비고2", 100, false);
            this._flexM.SetCol("STD_UM_MATL", "STD도급재료비", 0, 17, true, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexM.SetCol("STD_UM_SOUL", "STD임가공비", 0, 17, true, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexM.SetCol("STD_UM", "STD단가", 0, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexM.SetCol("FG_PAYBILL", "지급조건", 0, true, typeof(string));
            this._flexM.SetCol("UNIT_CH", "변환단위", 100, false);
            this._flexM.SetCol("QT_CHCOEF", "변환계수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("OLD_QT_PO", "변환전수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("CD_OP", "Op.", false);
            this._flexM.SetCol("CD_WCOP", "공정코드", false);
            this._flexM.SetCol("NM_OP", "공정명", false);
            this._flexM.SetCol("CD_WC", "작업장코드", false);
            this._flexM.SetCol("NM_WC", "작업장명", false);
            if (this.sChcoef_YN == "000")
                this._flexM.SetCol("QT_WORK_CHCOEF", "실적수량(변환)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            else
                this._flexM.SetCol("QT_WORK_CHCOEF", "실적수량(변환)", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("CD_PJT", "프로젝트코드", 100);
            this._flexM.SetCol("NM_PROJECT", "프로젝트명", 100);
            if (Config.MA_ENV.프로젝트사용)
                this._flexM.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexM.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexM.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexM.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
            }
            this._flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM.Cols["NO_POLINE"].TextAlign = (TextAlignEnum)4;
            this._flexM.SetDummyColumn(new string[] { "CHK" });
            this._flexM.LoadUserCache("P_PR_OPOUT_IV_APPLY_SUB__flexM");
        }

        private bool DoContinue() => this._flexM.Editor == null || this._flexM.FinishEditing(false);

        private bool Check() => this.Chk_PLANT && this.Chk_DT && this.Chk_EXCH;

        private void OnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.DoContinue() || !this.Check())
                    return;
                object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                   D.GetString(( this.cbo공장).SelectedValue),
                                                   this.per기간.StartDateToString,
                                                   this.per기간.EndDateToString,
                                                   this.bp거래처.CodeValue,
                                                   this.bp품목.CodeValue,
                                                   this.str계산서처리구분,
                                                   this.str거래구분,
                                                   this.str과세구분,
                                                   D.GetString(this.cbo환종.SelectedValue),
                                                   Global.MainFrame.LoginInfo.EmployeeNo,
                                                   this.bp프로젝트.CodeValue,
                                                   Global.SystemLanguage.MultiLanguageLpoint };
                DataTable dataTable = !(this.sChcoef_YN == "000") ? this._biz.Chcoef_Search(objArray) : this._biz.Search(objArray);
                dataTable.ColumnChanged += new DataColumnChangeEventHandler(this.dt_ColumnChanged);
                this._flexM.Binding = dataTable;
                if (!this._flexM.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다, new string[0]);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void dt_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            try
            {
                if (!(sender is DataTable dataTable))
                    return;
                switch (e.Column.ColumnName)
                {
                    case "CHK":
                    case "QT_MOVE":
                    case "UM_EX":
                    case "AM_EX":
                    case "AM_VAT_WORK":
                    case "UM_MATL":
                    case "UM_SOUL":
                        e.Row.AcceptChanges();
                        this.전체합계금액들변경(dataTable.Select("CHK = 'Y'", "", DataViewRowState.CurrentRows));
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void OnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexM.HasNormalRow)
                    return;
                DataRow[] dataRowArray = this._flexM.DataTable.Select("CHK = 'Y'");
                if (dataRowArray.Length == 0)
                    return;
                DataTable dataTable = this._flexM.DataTable.Clone();
                foreach (DataRow row in dataRowArray)
                    dataTable.ImportRow(row);
                this.dtReturn = dataTable.Copy();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void OnCancel_Click(object sender, EventArgs e)
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

        private void OnChange_Click(object sender, EventArgs e)
        {
        }

        private void btn변경_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexM.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else if (this.bp품목.CodeValue == "")
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl품목.Text });
                    this.bp품목.Focus();
                }
                else if (this._flexM.DataTable.Select("CHK = 'Y'").Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    if (this._flexM.DataTable.Select("CHK = 'Y' AND CD_ITEM = '" + this.bp품목.CodeValue + "'").Length == 0)
                    {
                        Global.MainFrame.ShowMessage("선택된 품목중 품목(@)와 같은 품목이 없습니다.", new object[] { this.bp품목.CodeValue });
                    }
                    foreach (DataRow dataRow in this._flexM.DataTable.Select("CHK = 'Y' AND  CD_ITEM = '" + this.bp품목.CodeValue + "'", "", DataViewRowState.CurrentRows))
                    {
                        if (!(D.GetString(dataRow["CD_ITEM"]) != this.bp품목.CodeValue))
                        {
                            dataRow["UM_EX"] = this.cur변경단가.DecimalValue;
                            dataRow["UM_WORK"] = Unit.원화단가(DataDictionaryTypes.PR, D.GetDecimal(dataRow["UM_EX"]) * D.GetDecimal(dataRow["RT_EXCH"]));
                            dataRow["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, D.GetDecimal(dataRow["QT_MOVE"]) * D.GetDecimal(dataRow["UM_EX"]));
                            if (Global.MainFrame.LoginInfo.CompanyLanguage != 0)
                            {
                                dataRow["AM_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(dataRow["QT_MOVE"]) * D.GetDecimal(dataRow["UM_EX"]) * D.GetDecimal(dataRow["RT_EXCH"]));
                                dataRow["AM_VAT_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(dataRow["UM_EX"]) * D.GetDecimal(dataRow["RT_EXCH"]) != 0M ? D.GetDecimal(dataRow["UM_EX"]) * D.GetDecimal(dataRow["RT_EXCH"]) * D.GetDecimal(dataRow["TAX_RATE"]) / 100M : 0M);
                            }
                            else
                            {
                                this._flexM["AM_WORK"] = Math.Floor(D.GetDecimal(dataRow["QT_MOVE"]) * D.GetDecimal(dataRow["UM_EX"]) * D.GetDecimal(dataRow["RT_EXCH"]));
                                this._flexM["AM_VAT_WORK"] = (D.GetDecimal(dataRow["UM_EX"]) * D.GetDecimal(dataRow["RT_EXCH"]) != 0M ? Math.Floor(D.GetDecimal(dataRow["UM_EX"]) * D.GetDecimal(dataRow["RT_EXCH"]) * D.GetDecimal(dataRow["TAX_RATE"]) / 100M) : 0M);
                            }
                            dataRow["AM_HAP_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(dataRow["AM_WORK"]) + D.GetDecimal(dataRow["AM_VAT_WORK"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btnEXCH_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexM.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else if (this._flexM.DataTable.Select("CHK = 'Y'").Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else if (D.GetString((this.cbo환종).SelectedValue) == "000")
                {
                    Global.MainFrame.ShowMessage(공통메세지._자료가선택되었습니다, new string[1] { this.cbo환종.Text });
                }
                else
                {
                    DataRow[] dataRowArray = this._flexM.DataTable.Select("CD_EXCH = '" + D.GetString((this.cbo환종).SelectedValue).Trim() + "' AND CHK = 'Y'", "", DataViewRowState.CurrentRows);
                    decimal num4 = 0M;
                    decimal num5 = 0M;
                    decimal num6 = 0M;
                    decimal num7 = 0M;
                    this._flexM.Redraw = false;
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        decimal decimalValue = this.curEXCH.DecimalValue;
                        decimal num8 = D.GetDecimal(dataRow["AM_EX"]);
                        dataRow["RT_EXCH"] = decimalValue;
                        dataRow["AM_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num8 * decimalValue);
                        dataRow["AM_VAT_WORK"] = Global.MainFrame.LoginInfo.CompanyLanguage == 0 ? (Math.Floor(num8 * decimalValue * D.GetDecimal(dataRow["TAX_RATE"])) * 0.01M) : Unit.원화금액(DataDictionaryTypes.PR, num8 * decimalValue * D.GetDecimal(dataRow["TAX_RATE"]) * 0.01M);
                        dataRow["UM_WORK"] = Unit.원화단가(DataDictionaryTypes.PR, Convert.ToDecimal(dataRow["AM_WORK"]) / Convert.ToDecimal(dataRow["QT_MOVE"]));
                        dataRow["AM_HAP_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(dataRow["AM_WORK"]) + D.GetDecimal(dataRow["AM_VAT_WORK"]));
                        num4 += num8;
                        num5 += D.GetDecimal(dataRow["AM_WORK"]);
                        num6 += D.GetDecimal(dataRow["AM_VAT_WORK"]);
                        num7 += num5 + num6;
                    }
                  this._flexM.Redraw = true;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void cbo환종_Click(object sender, EventArgs e)
        {
            try
            {
                this.curEXCH.DecimalValue = this._biz.환율(this.str처리일자, D.GetString((this.cbo환종).SelectedValue));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn_apply_mst_um_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Check() || !this._flexM.HasNormalRow)
                    return;
                DataRow[] dataRowArray = this._flexM.DataTable.Select("CHK = 'Y'");
                if (dataRowArray == null || dataRowArray.Length < 1)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        dataRow["UM_MATL"] = D.GetDecimal(dataRow["STD_UM_MATL"]);
                        dataRow["UM_SOUL"] = D.GetDecimal(dataRow["STD_UM_SOUL"]);
                        dataRow["UM_EX"] = D.GetDecimal(dataRow["STD_UM"]);
                        dataRow["UM_WORK"] = D.GetDecimal(dataRow["STD_UM"]);
                        decimal num2 = !(this.sChcoef_YN == "000") ? Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(dataRow["QT_WORK_CHCOEF"])) : Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(dataRow["QT_MOVE"]));
                        dataRow["UM_WORK"] = Unit.원화단가(DataDictionaryTypes.PR, D.GetDecimal(dataRow["UM_EX"]) * (this.curEXCH.DecimalValue == 0M ? 1M : this.curEXCH.DecimalValue));
                        dataRow["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, D.GetDecimal(dataRow["UM_EX"]) * num2);
                        dataRow["AM_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(dataRow["AM_EX"]) * (this.curEXCH.DecimalValue == 0M ? 1M : this.curEXCH.DecimalValue));
                        dataRow["AM_VAT_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(dataRow["AM_WORK"]) * (D.GetDecimal(dataRow["TAX_RATE"]) / 100M));
                        dataRow["AM_HAP_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(dataRow["AM_WORK"]) + D.GetDecimal(dataRow["AM_VAT_WORK"]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn_apply_um_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Check() || !this._flexM.HasNormalRow)
                    return;
                DataRow[] dataRowArray = this._flexM.DataTable.Select("CHK = 'Y'");
                if (dataRowArray == null || dataRowArray.Length < 1)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                    {
                        DataSet dataSet = this._biz.Search_Um(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             D.GetString(( this.cbo공장).SelectedValue),
                                                                             D.GetString(dr["CD_PARTNER"]),
                                                                             D.GetString(( this.cbo환종).SelectedValue),
                                                                             D.GetString(dr["CD_ITEM"]) });
                        if ((dataSet.Tables[0] != null || dataSet.Tables[1] != null || dataSet.Tables[2] != null) && (dataSet.Tables[0].Rows.Count >= 1 || dataSet.Tables[1].Rows.Count >= 1 || dataSet.Tables[2].Rows.Count >= 1))
                        {
                            for (int index = 0; index < 3; ++index)
                            {
                                if (dataSet.Tables[index] != null && dataSet.Tables[index].Rows.Count > 0)
                                {
                                    dr["UM_EX"] = D.GetDecimal(dataSet.Tables[index].Rows[0]["UM_EX"]);
                                    break;
                                }
                            }
                            decimal num2 = !(this.sChcoef_YN == "000") ? Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(this._flexM["QT_WORK_CHCOEF"])) : Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(this._flexM["QT_MOVE"]));
                            dr["UM_WORK"] = Unit.원화단가(DataDictionaryTypes.PR, D.GetDecimal(dr["UM_EX"]) * (this.curEXCH.DecimalValue == 0M ? 1M : this.curEXCH.DecimalValue));
                            dr["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, D.GetDecimal(dr["UM_EX"]) * num2);
                            dr["AM_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(dr["AM_EX"]) * (this.curEXCH.DecimalValue == 0M ? 1M : this.curEXCH.DecimalValue));
                            dr["AM_VAT_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(dr["AM_WORK"]) * (D.GetDecimal(dr["TAX_RATE"]) / 100M));
                            dr["AM_HAP_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(dr["AM_WORK"]) + D.GetDecimal(dr["AM_VAT_WORK"]));
                            this.개별합계금액들변경(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexM_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                if (!this._flexM.HasNormalRow)
                    return;
                if (this._flexM.Cols[e.Col].Name == "CHK")
                {
                    this._flexM["CHK"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";
                    if (this._flexM.DataView.ToTable().Select("CHK = 'Y'", "", DataViewRowState.CurrentRows).Length != 0)
                        this._flexM.SetCellCheck(this._flexM.Row, this._flexM.Cols["CHK"].Index, CheckEnum.Checked);
                    else
                        this._flexM.SetCellCheck(this._flexM.Row, this._flexM.Cols["CHK"].Index, CheckEnum.Unchecked);
                }
                else
                {
                    decimal num1 = Unit.환율(DataDictionaryTypes.PR, D.GetDecimal(this._flexM["RT_EXCH"]));
                    decimal num2 = D.GetDecimal(this._flexM["TAX_RATE"]);
                    decimal num3 = D.GetDecimal(this._flexM.EditData);
                    D.GetDecimal(this._flexM.GetData(e.Row, e.Col));
                    switch (this._flexM.Cols[e.Col].Name)
                    {
                        case "QT_MOVE":
                            if (num3 > Convert.ToDecimal(this._flexM["QT_MOVE_ORIGIN"]) && Convert.ToDecimal(this._flexM["QT_MOVE_ORIGIN"]) > 0M || num3 < Convert.ToDecimal(this._flexM["QT_MOVE_ORIGIN"]) && Convert.ToDecimal(this._flexM["QT_MOVE_ORIGIN"]) < 0M)
                            {
                                int num4 = (int)Global.MainFrame.ShowMessage("처리수량이 미처리량보다 클 수 없습니다.");
                                e.Cancel = true;
                                break;
                            }
                            decimal num5 = Unit.외화단가(DataDictionaryTypes.PR, D.GetDecimal(this._flexM["UM_EX"]));
                            decimal num6 = Unit.원화단가(DataDictionaryTypes.PR, num5 * num1);
                            decimal num7 = Unit.원화금액(DataDictionaryTypes.PR, num3 * num5);
                            decimal num8;
                            decimal num9;
                            if (Global.MainFrame.LoginInfo.CompanyLanguage != 0)
                            {
                                num8 = Unit.원화금액(DataDictionaryTypes.PR, num3 * num5 * num1);
                                num9 = Unit.원화금액(DataDictionaryTypes.PR, num8 != 0M ? num8 * num2 / 100M : 0M);
                            }
                            else
                            {
                                num8 = Math.Floor(num3 * num5 * num1);
                                num9 = num8 != 0M ? Math.Floor(num8 * num2 / 100M) : 0M;
                            }
                            this._flexM["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PR, num5);
                            this._flexM["UM_WORK"] = Unit.원화단가(DataDictionaryTypes.PR, num6);
                            this._flexM["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, num7);
                            this._flexM["AM_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num8);
                            this._flexM["AM_VAT_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num9);
                            this._flexM["AM_HAP_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num8 + num9);
                            break;
                        case "QT_WORK_CHCOEF":
                            this._flexM["QT_MOVE"] = (D.GetDecimal(this._flexM["QT_WORK_CHCOEF"]) / D.GetDecimal(this._flexM["QT_CHCOEF"]));
                            if (num3 > Convert.ToDecimal(this._flexM["QT_MOVE_ORIGIN"]) && Convert.ToDecimal(this._flexM["QT_MOVE_ORIGIN"]) > 0M || num3 < Convert.ToDecimal(this._flexM["QT_MOVE_ORIGIN"]) && Convert.ToDecimal(this._flexM["QT_MOVE_ORIGIN"]) < 0M)
                            {
                                int num10 = (int)Global.MainFrame.ShowMessage("처리수량이 미처리량보다 클 수 없습니다.");
                                e.Cancel = true;
                                break;
                            }
                            decimal num11 = Unit.외화단가(DataDictionaryTypes.PR, D.GetDecimal(this._flexM["UM_EX"]));
                            decimal num12 = Unit.원화단가(DataDictionaryTypes.PR, num11 * num1);
                            decimal num13 = Unit.원화금액(DataDictionaryTypes.PR, num3 * num11);
                            decimal num14;
                            decimal num15;
                            if (Global.MainFrame.LoginInfo.CompanyLanguage != 0)
                            {
                                num14 = Unit.원화금액(DataDictionaryTypes.PR, num3 * num11 * num1);
                                num15 = Unit.원화금액(DataDictionaryTypes.PR, num14 != 0M ? num14 * num2 / 100M : 0M);
                            }
                            else
                            {
                                num14 = Math.Floor(num3 * num11 * num1);
                                num15 = num14 != 0M ? Math.Floor(num14 * num2 / 100M) : 0M;
                            }
                            this._flexM["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PR, num11);
                            this._flexM["UM_WORK"] = Unit.원화단가(DataDictionaryTypes.PR, num12);
                            this._flexM["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, num13);
                            this._flexM["AM_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num14);
                            this._flexM["AM_VAT_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num15);
                            this._flexM["AM_HAP_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num14 + num15);
                            break;
                        case "UM_EX":
                            decimal num16 = !(this.sChcoef_YN == "000") ? Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(this._flexM["QT_WORK_CHCOEF"])) : Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(this._flexM["QT_MOVE"]));
                            decimal num17 = Unit.외화단가(DataDictionaryTypes.PR, num3);
                            decimal num18 = Unit.원화단가(DataDictionaryTypes.PR, num17 * num1);
                            decimal num19 = Unit.원화금액(DataDictionaryTypes.PR, num16 * num17);
                            decimal num20;
                            decimal num21;
                            if (Global.MainFrame.LoginInfo.CompanyLanguage != 0)
                            {
                                num20 = Unit.원화금액(DataDictionaryTypes.PR, num16 * num17 * num1);
                                num21 = Unit.원화금액(DataDictionaryTypes.PR, num20 != 0M ? num20 * num2 / 100M : 0M);
                            }
                            else
                            {
                                num20 = Math.Floor(num16 * num17 * num1);
                                num21 = num20 != 0M ? Math.Floor(num20 * num2 / 100M) : 0M;
                            }
                            this._flexM["QT_MOVE"] = Unit.수량(DataDictionaryTypes.PR, num16);
                            this._flexM["UM_WORK"] = Unit.원화단가(DataDictionaryTypes.PR, num18);
                            this._flexM["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, num19);
                            this._flexM["AM_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num20);
                            this._flexM["AM_VAT_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num21);
                            this._flexM["AM_HAP_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num20 + num21);
                            break;
                        case "AM_EX":
                            decimal num22 = !(this.sChcoef_YN == "000") ? Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(this._flexM["QT_WORK_CHCOEF"])) : Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(this._flexM["QT_MOVE"]));
                            decimal num23 = Unit.외화금액(DataDictionaryTypes.PR, num3);
                            decimal num24 = num22 != 0M ? num3 / num22 : 0M;
                            decimal num25 = Unit.원화단가(DataDictionaryTypes.PR, num24 * num1);
                            decimal num26;
                            decimal num27;
                            if (Global.MainFrame.LoginInfo.CompanyLanguage != 0)
                            {
                                num26 = Unit.원화금액(DataDictionaryTypes.PR, num23 * num1);
                                num27 = Unit.원화금액(DataDictionaryTypes.PR, num26 != 0M ? num26 * num2 / 100M : 0M);
                            }
                            else
                            {
                                num26 = Math.Floor(num23 * num1);
                                num27 = num26 != 0M ? Math.Floor(num26 * num2 / 100M) : 0M;
                            }
                            this._flexM["QT_MOVE"] = Unit.수량(DataDictionaryTypes.PR, num22);
                            this._flexM["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PR, num24);
                            this._flexM["UM_WORK"] = Unit.원화단가(DataDictionaryTypes.PR, num25);
                            this._flexM["AM_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num26);
                            this._flexM["AM_VAT_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num27);
                            this._flexM["AM_HAP_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num26 + num27);
                            break;
                        case "AM_VAT_WORK":
                            this._flexM["AM_HAP_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(this._flexM["AM_WORK"])) + num3);
                            break;
                        case "UM_MATL":
                        case "UM_SOUL":
                            decimal num28 = !(this.sChcoef_YN == "000") ? Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(this._flexM["QT_WORK_CHCOEF"])) : Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(this._flexM["QT_MOVE"]));
                            decimal num29 = D.GetDecimal(this._flexM["UM_MATL"]) + D.GetDecimal(this._flexM["UM_SOUL"]);
                            decimal num30 = Unit.원화단가(DataDictionaryTypes.PR, num29 * num1);
                            decimal num31 = Unit.원화금액(DataDictionaryTypes.PR, num28 * num29);
                            decimal num32;
                            decimal num33;
                            if (Global.MainFrame.LoginInfo.CompanyLanguage != 0)
                            {
                                num32 = Unit.원화금액(DataDictionaryTypes.PR, num28 * num29 * num1);
                                num33 = Unit.원화금액(DataDictionaryTypes.PR, num32 != 0M ? num32 * num2 / 100M : 0M);
                            }
                            else
                            {
                                num32 = Math.Floor(num28 * num29 * num1);
                                num33 = num32 != 0M ? Math.Floor(num32 * num2 / 100M) : 0M;
                            }
                            this._flexM["QT_MOVE"] = Unit.수량(DataDictionaryTypes.PR, num28);
                            this._flexM["UM_WORK"] = Unit.원화단가(DataDictionaryTypes.PR, num30);
                            this._flexM["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PR, num29);
                            this._flexM["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, num31);
                            this._flexM["AM_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num32);
                            this._flexM["AM_VAT_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num33);
                            this._flexM["AM_HAP_WORK"] = Unit.원화금액(DataDictionaryTypes.PR, num32 + num33);
                            break;
                    }
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
                if (e.HelpID != HelpID.P_MA_PITEM_SUB)
                    return;
                e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 전체합계금액들변경(DataRow[] drs)
        {
            if (!this._flexM.HasNormalRow)
                return;
            decimal num1 = 0M;
            decimal num2 = 0M;
            foreach (DataRow dr in drs)
            {
                num1 += D.GetDecimal(dr["AM_WORK"]);
                num2 += D.GetDecimal(dr["AM_VAT_WORK"]);
            }
            this.cur원화합계.DecimalValue = num1;
            this.cur부가세합계.DecimalValue = num2;
            this.cur총합계.DecimalValue = this.cur원화합계.DecimalValue + this.cur부가세합계.DecimalValue;
        }

        private void 개별합계금액들변경(DataRow dr)
        {
            // ISSUE: unable to decompile the method.
        }

        private bool Chk_PLANT => !Checker.IsEmpty(this.cbo공장, this.lbl공장.Text);

        private bool Chk_DT => !Checker.IsEmpty(this.per기간, this.lbl실적기간.Text);

        private bool Chk_EXCH => !Checker.IsEmpty(this.cbo환종, this.lbl환종.Text);

        public string 과세구분
        {
            get => this.str과세구분;
            set => this.str과세구분 = value;
        }

        public string 거래구분
        {
            get => this.str거래구분;
            set => this.str거래구분 = value;
        }

        public string 계산서처리구분
        {
            get => this.str계산서처리구분;
            set => this.str계산서처리구분 = value;
        }

        public string 공장
        {
            get => this.str공장;
            set => this.str공장 = value;
        }

        public string 거래처코드
        {
            get => this.str거래처코드;
            set => this.str거래처코드 = value;
        }

        public string 거래처명
        {
            get => this.str거래처명;
            set => this.str거래처명 = value;
        }

        public DataTable ReturnTable => this.dtReturn;

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this._flexM.SaveUserCache("P_PR_OPOUT_IV_APPLY_SUB__flexM");
        }
    }
}

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.Windows.Print;
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
    public partial class P_CZ_PR_OPOUT_PO_MNG : PageBase
    {
        private DataTable _dtDelID = new DataTable();
        private bool bGridrowChanging = false;
        private P_PR_OPOUT_PO_MNG_BIZ _biz = new P_PR_OPOUT_PO_MNG_BIZ();
        public P_CZ_PR_OPOUT_PO_MNG()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexM, this._flexD };
            this._flexM.DetailGrids = new FlexGrid[] { this._flexD };

            this._flexM.BeginSetting(1, 1, false);
            this._flexM.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            this._flexM.SetCol("CD_PLANT", "공장", false);
            this._flexM.SetCol("NO_PO", "발주번호", 100, false);
            this._flexM.SetCol("DT_PO", "발주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("CD_PARTNER", "외주처코드", 100, false);
            this._flexM.SetCol("LN_PARTNER", "외주처코드", 100, false);
            this._flexM.SetCol("CD_EXCH", "화폐단위코드", false);
            this._flexM.SetCol("NM_EXCH", "화폐단위", 100, false);
            this._flexM.SetCol("AM_EX", "외화금액", 60, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexM.SetCol("AM", "금액", 60, false, typeof(decimal), FormatTpType.MONEY);
            this._flexM.SetCol("AM_VAT", "부가세", 60, false, typeof(decimal), FormatTpType.MONEY);
            this._flexM.SetCol("FG_TAX", "과세구분코드", false);
            this._flexM.SetCol("NM_FG_TAX", "과세구분", 140, false);
            this._flexM.SetCol("DC_RMK", "비고", 140, false);
            this._flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM.SetDummyColumn("CHK");
            this._flexM.Cols["CD_PLANT"].Visible = false;
            this._flexM.Cols["CD_EXCH"].Visible = false;
            this._flexM.Cols["FG_TAX"].Visible = false;

            this._flexD.BeginSetting(1, 1, false);
            this._flexD.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexD.SetCol("STND_ITEM", "규격", 120, false);
            this._flexD.SetCol("UNIT_IM", "단위", 40, false);
            this._flexD.SetCol("UM_EX", "외화단가", 90, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexD.SetCol("AM_EX", "외화금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD.SetCol("UM", "발주단가", 90, false, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexD.SetCol("AM", "발주금액", 90, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD.SetCol("AM_VAT", "부가세", 90, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD.SetCol("AM_SUM", "합계금액", 90, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flexD.SetCol("CD_WC", "작업장코드", 80, false);
            this._flexD.SetCol("NM_WC", "작업장명", 140, false);
            this._flexD.SetCol("CD_WCOP", "공정코드", 80, false);
            this._flexD.SetCol("NM_OP", "공정명", 120, false);
            this._flexD.SetCol("QT_RCV", "입고수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_CLS", "마감수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("VAT_RATE", "부가세율", 90, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexD.SetCol("NM_FG_SERNO", "LOT,S/N관리", false);
            this._flexD.SetCol("NM_GRP_ITEM", "품목군", false);
            this._flexD.SetCol("UNIT_CH", "변환단위", 100, false);
            this._flexD.SetCol("QT_CHCOEF", "변환계수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("OLD_QT_PO", "변환전수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_PO", "발주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("EN_ITEM", "품목명(영)", false);
            this._flexD.SetCol("STND_DETAIL_ITEM", "세부규격", false);
            this._flexD.SetCol("NM_MAKER", "Maker", false);
            this._flexD.SetCol("BARCODE", "BARCODE", false);
            this._flexD.SetCol("NO_MODEL", "모델번호", false);
            this._flexD.SetCol("NO_DESIGN", "도면번호", false);
            this._flexD.SetCol("MAT_ITEM", "재질", false);
            this._flexD.SetCol("CLS_L", "대분류", false);
            this._flexD.SetCol("CLS_M", "중분류", false);
            this._flexD.SetCol("CLS_S", "소분류", false);
            if (MA.ServerKey(false, "JIGLS"))
                this._flexD.SetCol("TXT_USERDEF1_WO", "규격변경", 80, false);
            this._flexD.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexD.SetDummyColumn("CHK");
            this._flexD.Cols["CD_WC"].Visible = false;
            this._flexD.Cols["CD_WCOP"].Visible = false;
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            this.InitControl();
            this.oneGrid1.UseCustomLayout = true;
            this.bpP_Plant.IsNecessaryCondition = true;
            this.bpP_Dt_Po.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
        }

        private void InitControl()
        {
            DataTable code = MA.GetCode("MA_PLANT_AUTH");
            SetControl setControl = new SetControl();
            setControl.SetCombobox(this.m_cboCdPlant, code);
            setControl.SetCombobox(this.cboSearch1, MA.GetCode("MA_B000112", true));
            if (code.Select("CODE = '" + this.LoginInfo.CdPlant + "'").Length == 1)
                this.m_cboCdPlant.SelectedValue = this.LoginInfo.CdPlant;
            else if (this.m_cboCdPlant.Items.Count > 0)
                this.m_cboCdPlant.SelectedIndex = 0;
            this.dtp발주기간.StartDateToString = this.MainFrameInterface.GetStringToday.Substring(0, 6) + "01";
            this.dtp발주기간.EndDateToString = this.MainFrameInterface.GetStringToday;
            this._flexD.SetDataMap("UNIT_CH", MA.GetCode("MA_B000004", true), "CODE", "NAME");
            this._flexD.SetDataMap("CLS_L", MA.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.대분류, true), "CODE", "NAME");
            this._flexD.SetDataMap("CLS_M", MA.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.중분류, true), "CODE", "NAME");
            this._flexD.SetDataMap("CLS_S", MA.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.소분류, true), "CODE", "NAME");
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            this._flexM.BeforeRowChange += new RangeEventHandler(this._flexM_BeforeRowChange);
            this._flexM.AfterRowChange += new RangeEventHandler(this.Grid_AfterRowChange);

            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.bp외주처.CodeChanged += new EventHandler(this.OnBpControl_CodeChanged);
            this.bp외주처.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.bp외주처.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.bp작업지시번호.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
        }


        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarAddButtonEnabled = false;
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
            if (!this.공장선택여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                this.m_cboCdPlant.Focus();
                return false;
            }
            if (!this.발주기간시작등록여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl발주기간.Text);
                this.dtp발주기간.Focus();
                return false;
            }
            if (!this.발주기간끝등록여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl발주기간.Text);
                this.dtp발주기간.Focus();
                return false;
            }
            return Checker.IsValid(this.dtp발주기간, true, this.lbl발주기간.Text);
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch())
                    return;
                this._flexM.Binding = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                      this.m_cboCdPlant.SelectedValue.ToString(),
                                                                      this.dtp발주기간.StartDateToString,
                                                                      this.dtp발주기간.EndDateToString,
                                                                      this.bp외주처.CodeValue,
                                                                      this.LoginInfo.EmployeeNo,
                                                                      this.bp작업지시번호.CodeValue,
                                                                      Global.SystemLanguage.MultiLanguageLpoint,
                                                                      D.GetString(this.cboSearch1.SelectedValue),
                                                                      this.txt검색1.Text });
                if (!this._flexM.HasNormalRow)
                {
                    this.ShowMessage(PageResultMode.SearchNoData);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeDelete() || !this._flexM.HasNormalRow)
                    return;
                DataRow[] dataRowArray = this._flexM.DataTable.Select("CHK = 'Y'", "");
                if (dataRowArray.Length == 0)
                    return;
                foreach (DataRow dr in dataRowArray)
                {       
                    string[] strArray = new string[] { "CD_PLANT = '",
                                                       dr["CD_PLANT"].ToString(),
                                                       "' AND NO_PO = '",
                                                       dr["NO_PO"].ToString(),
                                                       "' " };
                    if (_dtDelID.Columns.Count == 0)
                    {
                        _dtDelID.Columns.Add("NO_WO");
                        _dtDelID.Columns.Add("NO_WO_LINE");
                        _dtDelID.Columns.Add("NO_PO");
                    }
                    foreach (DataRow dataRow2 in this._flexD.DataTable.Select(string.Concat(strArray), ""))
                    {

                        _dtDelID.Rows.Add(new object[] { dataRow2["NO_WO"].ToString(),
                                                         dataRow2["NO_WO_LINE"].ToString(),
                                                         dataRow2["NO_PO"].ToString() });

                        dataRow2.Delete();
                    }
                    dr.Delete();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
                    return;
                this.ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool Verify() => base.Verify();

        protected override bool SaveData()
        {
            if (!base.SaveData() || !this.Verify())
                return false;
            DataTable changes1 = this._flexM.GetChanges();
            DataTable changes2 = this._flexD.GetChanges();
            if (changes1 == null && changes2 == null)
                return true;
            if (!this._biz.Save(changes1, changes2))
                return false;

            string query = @"UPDATE CZ_PR_WO_INSP
SET
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE()), NO_OPOUT_PO = NULL
WHERE CD_COMPANY = '{0}'
AND NO_WO = '{1}'
AND NO_LINE = '{2}'
AND NO_INSP = '994'
AND NO_OPOUT_PO = '{3}'";

            foreach (DataRow dr in _dtDelID.Rows)
                DBHelper.ExecuteScalar(string.Format(query, new string[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                           dr["NO_WO"].ToString(),
                                                                           dr["NO_WO_LINE"].ToString(),
                                                                           dr["NO_PO"].ToString() }));

            this._flexM.AcceptChanges();
            this._flexD.AcceptChanges();
            return true;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexM.HasNormalRow || !this.BeforePrint() || !this.MsgAndSave(PageActionMode.Print))
                    return;
                DataRow[] dataRowArray = this._flexM.DataTable.Select("CHK = 'Y'", "", DataViewRowState.CurrentRows);
                if (dataRowArray.Length == 0)
                {
                    int num = (int)this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    if (dataRowArray.Length > 1)
                    {
                        int row = this._flexM.Row;
                        for (int index = this._flexM.Rows.Fixed; index < this._flexM.Rows.Count; ++index)
                        {
                            if (!(D.GetString(this._flexM.Rows[index]["CHK"]) != "Y"))
                                this._flexM.Row = index;
                        }
                        this._flexM.Row = row;
                    }
                    ReportHelper rptHelper = new ReportHelper("R_PR_OPOUT_PO_MNG_0", "외주발주관리");
                    rptHelper.가로출력();
                    rptHelper.SetData("공장코드", this.m_cboCdPlant.SelectedValue.ToString());
                    rptHelper.SetData("공장명", this.m_cboCdPlant.Text);
                    rptHelper.SetData("작업기간시작", this.dtp발주기간.StartDateToString);
                    rptHelper.SetData("작업기간끝", this.dtp발주기간.EndDateToString);
                    rptHelper.SetData("외주처코드", this.bp외주처.CodeValue);
                    rptHelper.SetData("외주처명", this.bp외주처.CodeName);
                    if (!this.PrintGridSetting(ref this._flexM, ref this._flexD, "NO_PO", ref rptHelper))
                        return;
                    rptHelper.Print();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool PrintGridSetting(
          ref FlexGrid _flexM,
          ref FlexGrid _flexD,
          string strFilterCondition,
          ref ReportHelper rptHelper)
        {
            if (!_flexM.HasNormalRow)
                return false;
            DataRow[] dataRowArray1 = _flexM.DataTable.Select("CHK = 'Y'", "", DataViewRowState.CurrentRows);
            if (dataRowArray1.Length == 0)
                return false;
            string str = "";
            for (int index = 0; index < dataRowArray1.Length; ++index)
                str = str + " (" + strFilterCondition + " = '" + dataRowArray1[index][strFilterCondition].ToString() + "') OR ";
            string filterExpression = str.Substring(0, str.Length - 3);
            DataRow[] dataRowArray2 = _flexD.DataTable.Select(filterExpression, strFilterCondition);
            if (dataRowArray2.Length == 0)
                return false;
            DataTable dt = _flexD.DataTable.Clone();
            foreach (DataRow row in dataRowArray2)
                dt.ImportRow(row);
            this.CaptionMapping(new FlexGrid[] { _flexM, _flexD }, ref dt);
            rptHelper.SetDataTable(dt);
            return true;
        }

        private void CaptionMapping(FlexGrid[] _flexArr, ref DataTable dt)
        {
            foreach (DataColumn column in dt.Columns)
            {
                foreach (FlexGrid flexGrid in _flexArr)
                {
                    if (flexGrid.Cols.Contains(column.ColumnName))
                        column.Caption = flexGrid.Cols[column.ColumnName].Caption;
                }
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                if (_dtDelID.Columns.Count == 0)
                {
                    _dtDelID.Columns.Add("NO_WO");
                    _dtDelID.Columns.Add("NO_WO_LINE");
                    _dtDelID.Columns.Add("NO_PO");
                }
                _dtDelID.Rows.Add(new object[] { this._flexD["NO_WO"].ToString(),
                                                 this._flexD["NO_WO_LINE"].ToString(),
                                                 this._flexD["NO_PO"].ToString() });
                this._flexD.Rows.Remove(this._flexD.Row);
                if (this._flexM.HasNormalRow)
                {
                    object obj1 = this._flexD.DataTable.Compute("SUM(AM_EX)", "");
                    object obj2 = this._flexD.DataTable.Compute("SUM(AM)", "");
                    object obj3 = this._flexD.DataTable.Compute("SUM(AM_VAT)", "");
                    decimal result1 = 0M;
                    decimal result2 = 0M;
                    decimal result3 = 0M;
                    decimal.TryParse(obj1.ToString(), out result1);
                    decimal.TryParse(obj2.ToString(), out result2);
                    decimal.TryParse(obj3.ToString(), out result3);
                    this._flexM["AM_EX"] = result1;
                    this._flexM["AM"] = result2;
                    this._flexM["AM_VAT"] = result3;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_BeforeRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!this.bGridrowChanging)
                    e.Cancel = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Grid_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this.ToolBarSearchButtonEnabled = false;
                this.bGridrowChanging = false;
                DataTable dt = null;
                string filter = "CD_PLANT = '" + this._flexM["CD_PLANT"].ToString() + "' AND NO_PO = '" + this._flexM["NO_PO"].ToString() + "' ";
                if (this._flexM.DetailQueryNeed)
                    dt = this._biz.SearchDetail(new object[] { this.LoginInfo.CompanyCode,
                                                               this._flexM["CD_PLANT"].ToString(),
                                                               this._flexM["NO_PO"].ToString(),
                                                               this.LoginInfo.EmployeeNo,
                                                               this.bp작업지시번호.CodeValue,
                                                               Global.SystemLanguage.MultiLanguageLpoint,
                                                               D.GetString(this.cboSearch1.SelectedValue),
                                                               this.txt검색1.Text });
                this._flexM.DetailGrids[0].BindingAdd(dt, filter);
                this._flexM.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.ToolBarSearchButtonEnabled = true;
                this.bGridrowChanging = true;
            }
        }

        private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID == HelpID.P_PR_WO_REG_SUB)
                {
                    if (!this.공장선택여부)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                        e.QueryCancel = true;
                        this.m_cboCdPlant.Focus();
                    }
                    else
                        e.HelpParam.P09_CD_PLANT = this.m_cboCdPlant.SelectedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_CodeChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnControlValidating(object sender, CancelEventArgs e)
        {
            string str = ((Control)sender).Name.ToString();
            DataTable dataTable = new DataTable();
            try
            {
                switch (str)
                {
                    case "m_dtpFrom":
                    case "m_dtpTo":
                        DatePicker datePicker = (DatePicker)sender;
                        if (!datePicker.Modified || datePicker.Text == "")
                            break;
                        if (!datePicker.IsValidated)
                        {
                            this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                            datePicker.Text = "";
                            datePicker.Focus();
                            e.Cancel = true;
                            break;
                        }
                        if ((datePicker.Name == "m_dtpFrom" || datePicker.Name == "m_dtpTo") && this.dtp발주기간.StartDate > this.dtp발주기간.EndDate)
                        {
                            this.ShowMessage(공통메세지.시작일자보다종료일자가클수없습니다);
                            this.dtp발주기간.StartDateToString = "";
                            this.dtp발주기간.Focus();
                            e.Cancel = true;
                            break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool 공장선택여부 => this.m_cboCdPlant.SelectedValue != null && !(this.m_cboCdPlant.SelectedValue.ToString() == "");

        private bool 발주기간시작등록여부 => !(this.dtp발주기간.StartDateToString == "");

        private bool 발주기간끝등록여부 => !(this.dtp발주기간.EndDateToString == "");
    }
}

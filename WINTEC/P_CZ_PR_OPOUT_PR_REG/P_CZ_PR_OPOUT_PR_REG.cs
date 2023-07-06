using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.Windows.Print;
using DzHelpFormLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_PR_OPOUT_PR_REG : PageBase
    {
        private P_CZ_PR_OPOUT_PR_REG_BIZ _biz = new P_CZ_PR_OPOUT_PR_REG_BIZ();
        private FreeBinding _header;
        public P_CZ_PR_OPOUT_PR_REG()
        {
            try
            {
                this.InitializeComponent();

                this.DataChanged += new EventHandler(this.Page_DataChanged);
                this._header = new FreeBinding();
                this._header.JobModeChanged += new FreeBindingEventHandler(this._header_JobModeChanged);
                this._header.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);
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

        protected override void InitPaint()
        {
            base.InitPaint();
            object[] objArray = new object[3];

            DataSet comboData = Global.MainFrame.GetComboData("NC;MA_PLANT");
            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";


            if (string.IsNullOrEmpty(this.txt요청번호.Text))
			{
                objArray = new object[] { "XXXXXX", "XXXXXX", "XXXXXX" };
                DataSet ds1 = this._biz.Search(objArray);
                this._header.SetBinding(ds1.Tables[0], this.oneGrid1);
                this._header.ClearAndNewRow();
                this._flexM.Binding = ds1.Tables[1];

                this.bpPanelControl2.IsNecessaryCondition = true;
                this.bpPanelControl3.IsNecessaryCondition = true;

                this.oneGrid1.InitCustomLayout();
            }
			else
			{
                objArray[0] = Global.MainFrame.LoginInfo.CompanyCode;
                objArray[1] = this.cbo공장.SelectedValue.ToString();
                objArray[2] = this.txt요청번호.Text;

                DataSet ds2 = this._biz.Search(objArray);
                this._header.SetBinding(ds2.Tables[0], this.oneGrid1);
                this._flexM.Binding = ds2.Tables[1];
                this._header.SetControlEnabled(false);
            }
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexM, this._flexID };
            this._flexM.DetailGrids = new FlexGrid[] { this._flexID };

            #region _flexM
            this._flexM.BeginSetting(1, 1, false);

            this._flexM.SetCol("CD_PLANT", "공장코드", 100, false);
            this._flexM.SetCol("NO_LINE", "순번", 50, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flexM.SetCol("CD_OP", "OP", 35, false);
            this._flexM.SetCol("CD_WC", "작업장", 50, false);
            this._flexM.SetCol("NM_WC", "작업장명", 100, false);
            this._flexM.SetCol("CD_WCOP", "공정", 50, false);
            this._flexM.SetCol("NM_OP", "공정명", 100, false);
            this._flexM.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexM.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexM.SetCol("STND_ITEM", "규격", 120, false);
            this._flexM.SetCol("NO_DESIGN", "도면번호", 100);
            this._flexM.SetCol("UNIT_IM", "단위", 40, false);
            this._flexM.SetCol("QT_START", "입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("QT_APPLY_YET", "요청대상수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("QT_PR", "요청수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("ST_PR", "상태", 65, false);
            this._flexM.SetCol("DT_DUE", "희망납기일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("CD_PARTNER", "거래처", 100, true);
            this._flexM.SetCol("LN_PARTNER", "거래처명", 100);
            this._flexM.SetCol("CD_WCOP_NEXT", "후공정", 50, false);
            this._flexM.SetCol("NM_OP_NEXT", "후공정명", 100, false);
            this._flexM.SetCol("DC_RMK", "라인비고", 250, true);

            this._flexM.Cols["CD_PLANT"].Visible = false;
            this._flexM.ExtendLastCol = true;
            this._flexM.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
            this._flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);
            #endregion

            #region _flexID
            this._flexID.BeginSetting(1, 1, false);

            this._flexID.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flexID.SetCol("SEQ_WO", "순번", 100);
            this._flexID.SetCol("DT_NO_ID", "투입년월", 100, false, typeof(string), FormatTpType.YEAR_MONTH);
            this._flexID.SetCol("NO_SEQ", "일련번호", 100);
            this._flexID.SetCol("NO_ID", "ID번호", 100);
            this._flexID.SetCol("NO_ID_OLD", "ID번호(이전)", 100);
            this._flexID.SetCol("NO_HEAT", "소재HEAT번호", 100);
            this._flexID.SetCol("NO_OPOUT_PO", "공정외주발주번호", 120);
            this._flexID.SetCol("NO_OPOUT_WORK", "공정외주실적번호", 120);

            this._flexID.EnabledHeaderCheck = false;
            this._flexID.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this._flexM.AfterRowChange += new RangeEventHandler(this._flexM_AfterRowChange);
            this._flexM.ValidateEdit += new ValidateEditEventHandler(this._flexM_ValidateEdit);
            this._flexID.ValidateEdit += new ValidateEditEventHandler(this._flexID_ValidateEdit);

            this.btn작업실적적용.Click += new EventHandler(this.btn작업실적적용_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn선택.Click += new EventHandler(this.btn선택_Click);
            this.btn해제.Click += new EventHandler(this.btn해제_Click);
            this.btn일괄선택.Click += new EventHandler(this.btm일괄선택_Click);
            this.btn일괄해제.Click += new EventHandler(this.btm일괄해제_Click);
        }

        private void _flexM_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            string oldValue, newValue, columnName;
            try
            {
                columnName = this._flexM.Cols[e.Col].Name;
                oldValue = this._flexM[e.Row, e.Col].ToString();
                newValue = this._flexM.EditData;

                if (oldValue == newValue) return;
                if (columnName == "QT_PR")
                {
                    if (D.GetDecimal(this._flexM["QT_APPLY_YET"]) < D.GetDecimal(this._flexM["QT_PR"]))
                    {
                        this.ShowMessage(공통메세지._은_보다작거나같아야합니다, "요청수량", "요청대상수량");
                        e.Cancel = true;
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexID_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            string oldValue, newValue, columnName;
            try
            {
                columnName = this._flexID.Cols[e.Col].Name;
                oldValue = this._flexID[e.Row, e.Col].ToString();
                newValue = this._flexID.EditData;

                if (oldValue == newValue) return;
                if (columnName == "S")
                {
                    if (!string.IsNullOrEmpty(this._flexID["NO_OPOUT_PO"].ToString()))
                    {
                        this.ShowMessage("발주된 건은 수정 할 수 없습니다.");
                        e.Cancel = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string filter;
            try
            {
                if (!this._flexM.HasNormalRow) return;
                dt = null;
                filter = string.Format("NO_WO = '{0}' AND NO_WO_LINE = '{1}'", this._flexM["NO_WO"].ToString(),
                                                                               this._flexM["NO_WO_LINE"].ToString());

                if (this._flexM.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               this._header.CurrentRow["NO_PR"].ToString(),
                                                               D.GetDecimal(this._flexM["NO_LINE"]),
                                                               this._flexM["NO_WO"].ToString(),
                                                               this._flexM["NO_WO_LINE"].ToString() });
                }
                this._flexID.BindingAdd(dt, filter);

                if (this._flexM["ST_PR"].ToString() != "요청")
                    this._flexM.AllowEditing = false;
                else
                    this._flexM.AllowEditing = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.IsChanged())
                    this.ToolBarSaveButtonEnabled = true;

                this.ToolBarDeleteButtonEnabled = !this.추가모드여부;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                this.Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                if (e.JobMode != JobModeEnum.추가후수정)
                    return;

                if (this.cbo공장.Items.Count > 0)
                {
                    if (this.LoginInfo.CdPlant != "")
                        this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;
                    else
                        this.cbo공장.SelectedIndex = 0;
                }

                this._header.CurrentRow["CD_PLANT"] = this.cbo공장.Items.Count <= 0 ? "" : D.GetString(this.cbo공장.SelectedValue);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool Check(string Gubun)
        {
            Hashtable HList = new Hashtable();

            if (Gubun == "SEARCH")
                HList.Add(this.cbo공장, this.lbl공장);
            else
            {
                HList.Add(this.cbo공장, this.lbl공장);
                HList.Add(this.dtp의뢰일자, this.dtp의뢰일자);
            }

            return ComFunc.NullCheck(HList);
        }

        protected override bool BeforeSearch()
        {
            return base.BeforeSearch() && this.Check("SEARCH");
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch()) return;

                P_CZ_PR_OPOUT_PR_SCH_SUB dialog = new P_CZ_PR_OPOUT_PR_SCH_SUB();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SpInfo spInfo = new SpInfo();
                    DataSet ds = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 this.cbo공장.SelectedValue,
                                                                 dialog.returnParams[0].ToString() });

                    this._header.SetDataTable(ds.Tables[0]);
                    this._flexM.Binding = ds.Tables[1];
                    this._header.SetControlEnabled(false);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeAdd()) return;

                this._flexM.DataTable.Rows.Clear();
                this._flexM.AcceptChanges();
                this._header.ClearAndNewRow();
                this._header.SetControlEnabled(true);

                this.InitPaint();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforePrint() || !this.MsgAndSave(PageActionMode.Print))
                    return;

                DataTable dt = this._flexM.DataTable.Clone();

                if (this._flexM.DataTable.Rows.Count == 0) return;

                ReportHelper reportHelper = new ReportHelper("R_CZ_PR_OPOUT_PR_0", "외주가공의뢰서");
                reportHelper.가로출력();
                foreach (DataColumn col in dt.Columns)
                {
                    if (this._flexM.Cols.Contains(col.ColumnName))
                        col.Caption = this._flexM.Cols[col.ColumnName].Caption;
                }
                foreach (DataRow dr in this._flexM.DataTable.Rows)
                {
                    dt.ImportRow(dr);
                }
                reportHelper.SetDataTable(dt);
                reportHelper.Print();
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
                if (!this.BeforeDelete() || !this.Check("") || this._header.JobMode != JobModeEnum.조회후수정 || this.ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes)
                    return;

                foreach (DataRow dr in this._flexM.DataTable.Rows)
                {
                    if (D.GetString(dr["ST_PR"]) != "요청")
                    {
                        this.ShowMessage("발주 진행 된 요청 건이 포함되어 있습니다.");
                        return;
                    }
                }

                this._biz.DeleteAll(this.cbo공장.SelectedValue.ToString(), this.txt요청번호.Text);
                this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                this.OnToolBarAddButtonClicked(null, null);
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
                this.InitPaint();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool Verify()
        {
            if (!base.Verify())
                return false;

            if (this._header.JobMode == JobModeEnum.조회후수정 && !this._flexM.HasNormalRow)
            {
                if (this.ShowMessage(공통메세지.자료를삭제하시겠습니까) == DialogResult.Yes)
                {
                    this._biz.DeleteAll(this.cbo공장.SelectedValue.ToString(), this.txt요청번호.Text);
                    this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    this.OnToolBarAddButtonClicked(null, null);
                    return false;
                }
            }
            else if (!this._flexM.HasNormalRow)
            {
                this.OnToolBarSearchButtonClicked(null, null);
                return false;
            }
            return true;
        }

        protected override bool SaveData()
        {
            string seq = "";
            if (!base.SaveData() || !this.Verify() || !this.Check(""))
                return false;

            if (this._header.JobMode == JobModeEnum.추가후수정)
            {
                seq = (string)this.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "78", this.dtp의뢰일자.Text.Substring(0, 6));
                this._header.CurrentRow["NO_PR"] = seq;
                this.txt요청번호.Text = seq;
            }

            DataTable changes1 = this._flexM.GetChanges();
            DataTable changes2 = this._flexID.GetChanges();

            if (changes1 == null && changes2 == null)
                return true;
            foreach (DataRow dr in this._flexM.DataTable.Rows)
			{
                if (dr.RowState != DataRowState.Deleted)
				{
                    DataRow[] dataRowArray = this._flexID.DataTable.Select("S = 'Y' AND NO_WO = '" + dr["NO_WO"].ToString() + "' AND NO_WO_LINE = '" + dr["NO_WO_LINE"].ToString() + "'");
                    if (dataRowArray.Length != Convert.ToDecimal(dr["QT_PR"]))
                    {
                        this.ShowMessage(공통메세지._와_은같아야합니다, "선택한 ID번호의 수", "요청수량");
                        return false;
                    }
                }
			}
            if (changes1 != null || changes2 != null)
            {
                if (!this._biz.Save(changes1, changes2, this._header.CurrentRow))
                    return false;
            }
            this._header.AcceptChanges();
            this._flexM.AcceptChanges();
            this._flexID.AcceptChanges();
            return true;
        }

        private void btn작업실적적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txt요청번호.Text))
				{
                    if (this.IsChanged())
					{
                        if (this.ShowMessage(공통메세지.변경된사항이있습니다저장하시겠습니까) == DialogResult.No)
                            return;
                        else
						{
                            if (!this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
                                return;
                        }
					}
                    this.InitPaint();
                }

                ArrayList al = new ArrayList();

                foreach (DataRow dr in this._flexM.DataTable.Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        string str = dr["NO_WO"].ToString() + "&" + dr["CD_OP"].ToString();
                        al.Add(str);
                    }
                }

                P_CZ_PR_OPOUT_PR_REG_SUB dialog = new P_CZ_PR_OPOUT_PR_REG_SUB(al);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    DataRow[] returnValues = dialog.ReturnValues;

                    if (returnValues == null || returnValues.Length == 0) return;

                    //this._flexM.AcceptChanges();
                    this._header.SetControlEnabled(false);

                    decimal maxValue = this._flexM.GetMaxValue("NO_LINE");
                    this._flexM.Redraw = false;

                    foreach (DataRow dr in returnValues)
                    {
                        this._flexM.Row = this._flexM.Rows.Count - 1;
                        this._flexM.Rows.Add();

                        this._flexM["CD_COMPANY"] = dr["CD_COMPANY"];
                        this._flexM["CD_PLANT"] = dr["CD_PLANT"];
                        this._flexM["NO_LINE"] = ++maxValue;
                        this._flexM["NO_WO"] = dr["NO_WO"];
                        this._flexM["CD_OP"] = dr["CD_OP"];
                        this._flexM["CD_WC"] = dr["CD_WC"];
                        this._flexM["NM_WC"] = dr["NM_WC"];
                        this._flexM["CD_WCOP"] = dr["CD_WCOP"];
                        this._flexM["NM_OP"] = dr["NM_OP"];
                        this._flexM["CD_ITEM"] = dr["CD_ITEM"];
                        this._flexM["NM_ITEM"] = dr["NM_ITEM"];
                        this._flexM["STND_ITEM"] = dr["STND_ITEM"];
                        this._flexM["NO_DESIGN"] = dr["NO_DESIGN"];
                        this._flexM["UNIT_IM"] = dr["UNIT_IM"];
                        this._flexM["QT_START"] = dr["QT_START"];
                        this._flexM["QT_PR"] = dr["QT_APPLY"];
                        this._flexM["ST_PR"] = "요청";
                        this._flexM["NO_WO_LINE"] = dr["NO_WO_LINE"];
                        this._flexM["NO_EMP"] = Global.MainFrame.LoginInfo.UserID;
                        this._flexM["QT_APPLY_YET"] = dr["QT_APPLY_YET"];

                        this._flexM.AddFinished();
                    }
                    this._flexM.Redraw = true;
                    this._flexM.Col = this._flexM.Cols["CD_ITEM"].Index;
                    this._flexM.Focus();
                }
            }
            catch (Exception ex)
            {
                this._flexM.Redraw = true;
                this.MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexM.HasNormalRow) return;

                if (!string.IsNullOrEmpty(this.txt요청번호.Text))
                {
                    if (this._flexM.GetDataRow(this._flexM.Row).RowState != DataRowState.Added)
					{
                        if (this.IsChanged())
                        {
                            this.ShowMessage("수정된 사항이 있습니다. 저장 후 진행하세요.");
                            return;
                        }
                    }
                }

                if (this._flexM["ST_PR"].ToString() == "요청")
                    this._flexM.GetDataRow(this._flexM.Row).Delete();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn선택_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexID.HasNormalRow) return;

                if (this.curID번호From.DecimalValue == 0 && this.curID번호To.DecimalValue == 0) return;
                else if (this.curID번호From.DecimalValue <= 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, "순번(From)의 값", "0");
                    return;
                }
                else if (this.curID번호To.DecimalValue <= 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, "순번(to)의 값", "0");
                    return;
                }
                else if (this.curID번호To.DecimalValue <= this.curID번호From.DecimalValue)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, "순번(to)의 값", "순번(From)의 값");
                    return;
                }
                for (int row = this._flexID.Rows.Fixed; row < this._flexID.Rows.Count; ++row)
                {
                    if (D.GetDecimal(this._flexID[row, "SEQ_WO"]) >= curID번호From.DecimalValue && D.GetDecimal(this._flexID[row, "SEQ_WO"]) <= curID번호To.DecimalValue)
                    {
                        if (this._flexID[row, "NO_OPOUT_PO"].ToString() == "")
                            this._flexID.SetCellCheck(row, this._flexID.Cols["S"].Index, CheckEnum.Checked);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn해제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexID.HasNormalRow) return;

                if (this.curID번호From.DecimalValue == 0 && this.curID번호To.DecimalValue == 0) return;
                else if (this.curID번호From.DecimalValue <= 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, "순번(From)의 값", "0");
                    return;
                }
                else if (this.curID번호To.DecimalValue <= 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, "순번(to)의 값", "0");
                    return;
                }
                else if (this.curID번호To.DecimalValue <= this.curID번호From.DecimalValue)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, "순번(to)의 값", "순번(From)의 값");
                    return;
                }
                for (int row = this._flexID.Rows.Fixed; row < this._flexID.Rows.Count; ++row)
                {
                    if (D.GetDecimal(this._flexID[row, "SEQ_WO"]) >= curID번호From.DecimalValue && D.GetDecimal(this._flexID[row, "SEQ_WO"]) <= curID번호To.DecimalValue)
                    {
                        if (this._flexID[row, "NO_OPOUT_PO"].ToString() == "")
                            this._flexID.SetCellCheck(row, this._flexID.Cols["S"].Index, CheckEnum.Unchecked);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btm일괄선택_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexID.HasNormalRow) return;

                for (int row = this._flexID.Rows.Fixed; row < this._flexID.Rows.Count; ++row)
                {
                    if (this._flexID[row, "NO_OPOUT_PO"].ToString() == "")
                        this._flexID.SetCellCheck(row, this._flexID.Cols["S"].Index, CheckEnum.Checked);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btm일괄해제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexID.HasNormalRow) return;

                for (int row = this._flexID.Rows.Fixed; row < this._flexID.Rows.Count; ++row)
                {
                    if (this._flexID[row, "NO_OPOUT_PO"].ToString() == "")
                        this._flexID.SetCellCheck(row, this._flexID.Cols["S"].Index, CheckEnum.Unchecked);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool IsChanged()
         {
            return base.IsChanged() || this.헤더변경여부;
        }

        private bool 헤더변경여부
        {
            get
            {
                bool flag = this._header.GetChanges() != null;

                if (flag && this._header.JobMode == JobModeEnum.추가후수정 && !this._flexM.HasNormalRow)
                    flag = false;

                return flag;
            }
        }

        private bool 추가모드여부
        {
            get
            {
                return this._header.JobMode == JobModeEnum.추가후수정;
            }
        }
    }
}
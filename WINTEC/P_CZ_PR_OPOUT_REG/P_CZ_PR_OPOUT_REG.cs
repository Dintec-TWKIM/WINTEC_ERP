using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
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
    public partial class P_CZ_PR_OPOUT_REG : PageBase
    {
        private P_CZ_PR_OPOUT_REG_BIZ _biz = new P_CZ_PR_OPOUT_REG_BIZ();

        public P_CZ_PR_OPOUT_REG()
        {
            try
            {
                InitializeComponent();
            }
            catch(Exception ex)
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

            DataSet comboData = Global.MainFrame.GetComboData("NC;MA_PLANT");
            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";

            if (comboData.Tables[0].Select("CODE = '" + this.LoginInfo.CdPlant + "'").Length == 1)
                this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;
            else if (this.cbo공장.Items.Count > 0)
                this.cbo공장.SelectedIndex = 0;

            this.dtp요청기간.StartDateToString = this.MainFrameInterface.GetStringToday.Substring(0, 6) + "01";
            this.dtp요청기간.EndDateToString = this.MainFrameInterface.GetStringToday;

            object[] obj1 = new object[] { "XXXXX", "XXXXX", "XXXXX", "XXXXX", "XXXXX" };
            this._flex.Binding = this._biz.Search(obj1);

            if (string.IsNullOrEmpty(this.ctx작업지시번호.CodeValue.ToString())) return;

            object[] obj2 = new object[] { this.LoginInfo.CompanyCode,
                                           this.ctx작업지시번호.CodeValue.ToString(),
                                           this.cbo공장.SelectedValue.ToString(),
                                           this.dtp요청기간.StartDateToString,
                                           this.dtp요청기간.EndDateToString };

            this._flex.Binding = this._biz.Search(obj2);
        }
        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex };
            this._flex.DetailGrids = new FlexGrid[] { this._flexID };

            #region _flex
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("CD_PLANT", "공장코드", 100, false);
            this._flex.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flex.SetCol("DT_PR", "외주요청일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("CD_WC", "작업장", 50, false);
            this._flex.SetCol("NM_WC", "작업장명", 100, false);
            this._flex.SetCol("CD_WCOP", "공정", 50, false);
            this._flex.SetCol("NM_OP", "공정명", 100, false);
            this._flex.SetCol("CD_OP", "OP", 35, false);
            this._flex.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flex.SetCol("NM_ITEM", "품목명", 140, false);
            this._flex.SetCol("STND_ITEM", "규격", 120, false);
            this._flex.SetCol("NO_DESIGN", "도면번호", 100);
            this._flex.SetCol("UNIT_IM", "단위", 40, false);
            this._flex.SetCol("QT_WIP", "입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_PR", "요청수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("ST_PR", "상태", 100, false);
            this._flex.SetCol("DC_RMK", "비고", 250, true);
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);

            this._flex.Cols["CD_PLANT"].Visible = false;
            this._flex.SetDummyColumn("S");
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
            this._flexID.SettingVersion = "0.0.0.1";
            this._flexID.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this.ctx작업지시번호.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx작업지시번호.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);

            this._flex.AfterRowChange += this._flex_AfterRowChange;
            this._flexID.ValidateEdit += this._flexID_ValidateEdit;
            this.btn선택.Click += this.btn선택_Click;
            this.btn해제.Click += this.btn해제_Click;
            this.btn일괄선택.Click += this.btm일괄선택_Click;
            this.btn일괄해제.Click += this.btm일괄해제_Click;
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

        private void _flexID_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            FlexGrid grid;
            try
            {
                grid = sender as FlexGrid;

                string colname = grid.Cols[e.Col].Name;
                string oldValue = D.GetString(grid.GetData(e.Row, e.Col));
                string newValue = grid.EditData;

                if (colname == "S")
                {
                    if (grid["NO_OPOUT_PO"].ToString() != "")
                    {
                        grid["S"] = oldValue;
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        grid["S"] = newValue;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string filter;
            try
            {
                if (!this._flex.HasNormalRow) return;

                dt = null;
                filter = string.Format("NO_WO = '{0}' AND NO_WO_LINE = '{1}'", this._flex["NO_WO"].ToString(),
                                                                               this._flex["NO_WO_LINE"].ToString());

                if (this._flex.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               this._flex["NO_WO"].ToString(),
                                                               this._flex["NO_WO_LINE"].ToString() });
                }
                this._flexID.BindingAdd(dt, filter);
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
                if (!this.BeforeSearch()) return;

                if (this._flexID.DataTable != null)
                {
                    this._flexID.DataTable.Rows.Clear();
                    this._flexID.AcceptChanges();
                }

                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this.ctx작업지시번호.CodeValue,
                                                                     this.cbo공장.SelectedValue,
                                                                     this.dtp요청기간.StartDateToString,
                                                                     this.dtp요청기간.EndDateToString });

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
                P_CZ_PR_OPOUT_REG_SUB dialog = new P_CZ_PR_OPOUT_REG_SUB();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    DataRow[] returnValues = dialog.ReturnValues;

                    if (returnValues == null || returnValues.Length == 0) return;

                    this._flex.Redraw = false;

                    foreach (DataRow dr in returnValues)
                    {
                        this._flex.Row = this._flex.Rows.Count - 1;
                        this._flex.Rows.Add();

                        this._flex["CD_COMPANY"] = dr["CD_COMPANY"];
                        this._flex["CD_PLANT"] = dr["CD_PLANT"];
                        this._flex["CD_WC"] = dr["CD_WC"];
                        this._flex["NM_WC"] = dr["NM_WC"];
                        this._flex["CD_WCOP"] = dr["CD_WCOP"];
                        this._flex["NM_OP"] = dr["NM_OP"];
                        this._flex["NO_WO"] = dr["NO_WO"];
                        this._flex["DT_PR"] = this.MainFrameInterface.GetStringToday;
                        this._flex["QT_WIP"] = dr["QT_OUTPO"];
                        this._flex["QT_PR"] = dr["QT_APPLY"];
                        this._flex["CD_OP"] = dr["CD_OP"];
                        this._flex["CD_ITEM"] = dr["CD_ITEM"];
                        this._flex["NM_ITEM"] = dr["NM_ITEM"];
                        this._flex["STND_ITEM"] = dr["STND_ITEM"];
                        this._flex["UNIT_IM"] = dr["UNIT_IM"];
                        this._flex["NM_TP_WO"] = dr["NM_TP_WO"];
                        this._flex["NO_DESIGN"] = dr["NO_DESIGN"];
                        this._flex["NO_WO_LINE"] = dr["NO_LINE"];
                        this._flex["ST_PR"] = "요청";

                        this._flex.AddFinished();
                    }

                    this._flex.Redraw = true;
                    this._flex.Col = this._flex.Cols["NO_WO"].Index;
                    this._flex.Focus();
                }
            }
            catch (Exception ex)
            {
                this._flex.Redraw = true;
                this.MsgEnd(ex);
            }
        }
    

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeDelete() || !this._flex.HasNormalRow)
                    return;

                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    if (this.ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes) return;

                    foreach (DataRow dataRow in dataRowArray)
                    {
                        if (D.GetString(dataRow["ST_PR"]) != "요청")
                        {
                            this.ShowMessage("발주 진행 된 요청 건이 포함되어 있습니다.");
                            return;
                        }
                        DataRow[] dataRowArray2 = this._flexID.DataTable.Select("NO_WO = '" + dataRow["NO_WO"].ToString() + "' AND NO_WO_LINE = '" + dataRow["NO_WO_LINE"].ToString() + "'");
                        foreach (DataRow dr in dataRowArray2)
                        {
                            dr.Delete();
                        }
                    }
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        dataRow.Delete();
                    }

                    this._flex.Refresh();
                    this._flexID.Refresh();
                }
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

                DataTable dt = this._flex.DataTable.Clone();
                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray.Length == 0) return;

                ReportHelper reportHelper = new ReportHelper("R_CZ_PR_OPOUT_PR_0", "외주가공의뢰서");
                reportHelper.가로출력();
                foreach (DataColumn col in dt.Columns)
                {
                    if (this._flex.Cols.Contains(col.ColumnName))
                        col.Caption = this._flex.Cols[col.ColumnName].Caption;
                }
                foreach (DataRow dr in dataRowArray)
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

        protected override bool SaveData()
        {
            DataRow[] dataRowArray1;
            DataRow[] dataRowArray2;
            if (!base.SaveData() || !base.Verify())
                return false;

            DataTable _dtAdd = this._flex.GetChanges(DataRowState.Added);
            DataTable _dtModify = this._flex.GetChanges(DataRowState.Modified);
            DataTable _dtDelete = this._flex.GetChanges(DataRowState.Deleted);
            DataTable _dtIDMod = this._flexID.GetChanges(DataRowState.Modified);
            DataTable _dtIDDel = this._flexID.GetChanges(DataRowState.Deleted);

            if (_dtAdd == null && _dtModify == null && _dtDelete == null && _dtIDMod == null && _dtIDDel == null) return true;

            if (_dtAdd != null)
            {
                foreach (DataRow dr in _dtAdd.Rows)
                {
                    dataRowArray1 = this._flexID.DataTable.Select("S = 'Y' AND NO_WO = '" + dr["NO_WO"].ToString() + "' AND NO_WO_LINE = '" + dr["NO_WO_LINE"].ToString() + "'");
                    if (Convert.ToDecimal(dr["QT_WIP"]) < Convert.ToDecimal(dr["QT_PR"]))
                    {
                        this.ShowMessage("요청수량이 입고수량보다 큽니다.");
                        return false;
                    }
                    if (Convert.ToDecimal(dr["QT_PR"]) != dataRowArray1.Length)
                    {
                        this.ShowMessage("선택한 ID번호 수량과 요청수량이 같지 않은 건이 있습니다.");
                        return false;
                    }
                }
            }

            if (_dtModify != null)
            {
                foreach (DataRow dr in _dtModify.Rows)
                {
                    dataRowArray2 = this._flexID.DataTable.Select("S = 'Y' AND NO_WO = '" + dr["NO_WO"].ToString() + "' AND NO_WO_LINE = '" + dr["NO_WO_LINE"].ToString() + "'");
                    if (Convert.ToDecimal(dr["QT_WIP"]) < Convert.ToDecimal(dr["QT_PR"]))
                    {
                        this.ShowMessage("요청수량이 입고수량보다 큽니다.");
                        return false;
                    }
                    if (Convert.ToDecimal(dr["QT_PR"]) != dataRowArray2.Length)
                    {
                        this.ShowMessage("선택한 ID번호 수량과 요청수량이 같지 않은 건이 있습니다.");
                        return false;
                    }
                }
            }

            if (_dtAdd != null || _dtModify != null || _dtDelete != null)
            {
                if (!this._biz.Save(_dtAdd, _dtModify, _dtDelete, _dtIDDel))
                    return false;
            }

            string query1 = @"IF NOT EXISTS(SELECT 1 FROM CZ_PR_WO_INSP WHERE CD_COMPANY = '{0}' AND NO_WO = '{1}' AND NO_LINE = '{2}' AND SEQ_WO = '{3}' AND NO_INSP = '994')
BEGIN
INSERT INTO CZ_PR_WO_INSP
(
	CD_COMPANY,
	NO_WO,
	NO_LINE,
	SEQ_WO,
	NO_INSP,
	DTS_INSERT
)
VALUES
(
	'{0}',
	'{1}',
	'{2}',
	'{3}',
	'994',
	NEOE.SF_SYSDATE(GETDATE())
) END";
            string query2 = @"DELETE FROM CZ_PR_WO_INSP
WHERE CD_COMPANY = '{0}'
AND NO_WO = '{1}'
AND NO_LINE = '{2}'
AND SEQ_WO = '{3}'
AND NO_INSP = '994'";

            if (_dtIDMod != null)
            {
                foreach (DataRow dr in _dtIDMod.Rows)
                {
                    if (dr["S"].ToString() == "Y")
                    {
                        DBHelper.ExecuteScalar(string.Format(query1, new string[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                dr["NO_WO"].ToString(),
                                                                                dr["NO_WO_LINE"].ToString(),
                                                                                dr["SEQ_WO"].ToString() }));
                    }

                    else if (dr["S"].ToString() == "N")
                    {
                        DBHelper.ExecuteScalar(string.Format(query2, new string[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                dr["NO_WO"].ToString(),
                                                                                dr["NO_WO_LINE"].ToString(),
                                                                                dr["SEQ_WO"].ToString() }));
                    }
                }
            }

            this._flex.AcceptChanges();
            this._flexID.AcceptChanges();
            return true;
        }

        private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                HelpID helpId = e.HelpID;
                if (helpId == HelpID.P_PR_WO_REG_SUB)
                {
                    if (string.IsNullOrEmpty(D.GetString(this.cbo공장.SelectedValue)))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장.Text });
                        this.cbo공장.Focus();
                        e.QueryCancel = true;
                    }
                    else
                        e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
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
                if (e.DialogResult != DialogResult.OK)
                    return;

                DataRow row = e.HelpReturn.Rows[0];
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}

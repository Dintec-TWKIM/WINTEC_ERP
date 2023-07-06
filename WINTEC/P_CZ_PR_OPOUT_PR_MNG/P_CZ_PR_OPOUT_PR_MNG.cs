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
    public partial class P_CZ_PR_OPOUT_PR_MNG : PageBase
    {
        private P_PR_OPOUT_PR_MNG_BIZ _biz = new P_PR_OPOUT_PR_MNG_BIZ();
        public P_CZ_PR_OPOUT_PR_MNG()
        {
            try
            {
                InitializeComponent();
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
            this.MainGrids = new FlexGrid[] { this._flexM };

            this._flexM.BeginSetting(1, 1, false);
            this._flexM.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            this._flexM.SetCol("CD_PLANT", "공장", false);
            this._flexM.SetCol("NO_PR", "요청번호", 100, false);
            this._flexM.SetCol("NO_LINE", "순번", 50, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("DT_PR", "요청일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("NM_KOR", "담당자", 100);
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
            this._flexM.SetCol("QT_PR", "요청수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("ST_PR", "상태", 65, false);
            this._flexM.SetCol("DT_DUE", "희망납기일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("CD_PARTNER", "거래처", 100, false);
            this._flexM.SetCol("LN_PARTNER", "거래처명", 100);
            this._flexM.SetCol("CD_WCOP_NEXT", "후공정", 50, false);
            this._flexM.SetCol("NM_OP_NEXT", "후공정명", 100, false);
            this._flexM.SetCol("DC_RMK", "비고", 250, false);
            this._flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM.SetDummyColumn("CHK");
            this._flexM.Cols["CD_PLANT"].Visible = false;
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            DataSet comboData = Global.MainFrame.GetComboData(new string[] { "NC;MA_PLANT" });
            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.ValueMember = "CODE";
            this.cbo공장.DisplayMember = "NAME";
            this.dtp요청기간.StartDateToString = this.MainFrameInterface.GetStringToday.Substring(0, 6) + "01";
            this.dtp요청기간.EndDateToString = this.MainFrameInterface.GetStringToday;

            this.oneGrid1.UseCustomLayout = true;
            this.bpP_Plant.IsNecessaryCondition = true;
            this.bpP_Dt_Po.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            this.ctx작업지시번호.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
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
            if (this.cbo공장.SelectedValue == null || this.cbo공장.SelectedValue.ToString() == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                this.cbo공장.Focus();
                return false;
            }
            if (this.dtp요청기간.StartDateToString == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl요청기간.Text);
                this.dtp요청기간.Focus();
                return false;
            }
            if (this.dtp요청기간.EndDateToString == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl요청기간.Text);
                this.dtp요청기간.Focus();
                return false;
            }
            return Checker.IsValid(this.dtp요청기간, true, this.lbl요청기간.Text);
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch())
                    return;
                this._flexM.Binding = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                      this.cbo공장.SelectedValue.ToString(),
                                                                      this.dtp요청기간.StartDateToString,
                                                                      this.dtp요청기간.EndDateToString,
                                                                      this.ctx작업지시번호.CodeValue });
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
                if (!this.BeforeDelete() || !this._flexM.HasNormalRow || this.ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes)
                    return;
                DataRow[] dataRowArray = this._flexM.DataTable.Select("CHK = 'Y'", "");
                if (dataRowArray.Length == 0)
                    return;
                foreach (DataRow dr in dataRowArray)
                {
                    if (dr["ST_PR"].ToString() != "요청")
                    {
                        this.ShowMessage("상태가 요청이 아닌 건은 삭제할 수 없습니다.");
                        return;
                    }
                    dr.Delete();
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

                DataTable dt = this._flexM.DataTable.Clone();
                DataRow[] dataRowArray = this._flexM.DataTable.Select("CHK = 'Y'");

                if (dataRowArray.Length == 0) return;

                ReportHelper reportHelper = new ReportHelper("R_CZ_PR_OPOUT_PR_0", "외주가공의뢰서");
                reportHelper.가로출력();
                foreach (DataColumn col in dt.Columns)
                {
                    if (this._flexM.Cols.Contains(col.ColumnName))
                        col.Caption = this._flexM.Cols[col.ColumnName].Caption;
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
            if (!base.SaveData() || !base.Verify())
                return false;
            DataTable changes = this._flexM.GetChanges();
            if (changes == null)
                return true;
            if (!this._biz.Save(changes))
                return false;

            this._flexM.AcceptChanges();
            return true;
        }

        private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID == HelpID.P_PR_WO_REG_SUB)
                {
                    if (this.cbo공장.SelectedValue == null || this.cbo공장.SelectedValue.ToString() == "")
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                        e.QueryCancel = true;
                        this.cbo공장.Focus();
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
                        if ((datePicker.Name == "m_dtpFrom" || datePicker.Name == "m_dtpTo") && this.dtp요청기간.StartDate > this.dtp요청기간.EndDate)
                        {
                            this.ShowMessage(공통메세지.시작일자보다종료일자가클수없습니다);
                            this.dtp요청기간.StartDateToString = "";
                            this.dtp요청기간.Focus();
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
    }
}

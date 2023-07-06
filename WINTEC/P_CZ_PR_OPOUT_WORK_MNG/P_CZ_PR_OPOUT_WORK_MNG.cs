using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_PR_OPOUT_WORK_MNG : PageBase
    {
        DataTable _dtReject = new DataTable();
        bool bGridrowChanging = false;
        DataTable LOT관리항목DT;
        P_CZ_PR_OPOUT_WORK_MNG_BIZ _biz = new P_CZ_PR_OPOUT_WORK_MNG_BIZ();
        public P_CZ_PR_OPOUT_WORK_MNG()
        {
            InitializeComponent();
        }
        protected override void InitLoad()
        {
            base.InitLoad();

            this.LOT관리항목DT = MA.GetCode("PU_C000079");

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexD };
            this._flexM.DetailGrids = new FlexGrid[] { this._flexD };

            this._flexM.BeginSetting(1, 1, false);
            this._flexM.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            this._flexM.SetCol("NO_WO", "작업지시번호", 100, 20, false);
            this._flexM.SetCol("NO_PO", "발주번호", 100, 20, false);
            this._flexM.SetCol("CD_PARTNER", "거래처코드", 100, false);
            this._flexM.SetCol("LN_PARTNER", "거래처명", 140, false);
            this._flexM.SetCol("CD_ITEM", "품목", 100, false);
            this._flexM.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexM.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flexM.SetCol("STND_ITEM", "규격", 120, false);
            this._flexM.SetCol("UNIT_IM", "단위", 40, false);
            this._flexM.SetCol("CD_PLANT", "공장코드", 80, false);
            this._flexM.SetCol("NM_PLANT", "공장명", 100, false);
            this._flexM.SetCol("NO_LINE", "발주항번", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.SetCol("DT_PO", "발주일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("NO_PR", "요청번호", 100);
            this._flexM.SetCol("DC_RMK", "비고", false);
            this._flexM.SetCol("DC_RMK_WO", "작업지시비고", false);
            this._flexM.SetCol("EN_ITEM", "품목명(영)", false);
            this._flexM.SetCol("STND_DETAIL_ITEM", "세부규격", false);
            this._flexM.SetCol("MAT_ITEM", "재질", false);
            this._flexM.SetCol("NM_MAKER", "Maker", false);
            this._flexM.SetCol("BARCODE", "BARCODE", false);
            this._flexM.SetCol("NO_MODEL", "모델번호", false);
            if (MA.ServerKey(false, new string[] { "JIGLS" }))
                this._flexM.SetCol("TXT_USERDEF1_WO", "규격변경", 80, false);
            this._flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM.SetDummyColumn(new string[] { "CHK" });
            this._flexM.Cols["CD_PLANT"].Visible = false;
            this._flexM.Cols["NO_LINE"].Visible = false;

            this._flexD.BeginSetting(1, 1, false);
            this._flexD.SetCol("CD_PLANT", "공장", 100, false);
            this._flexD.SetCol("DT_WORK", "실적일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD.SetCol("NO_WO", "지시번호", 100, false);
            this._flexD.SetCol("NO_WORK", "실적번호", 100, false);
            this._flexD.SetCol("CD_OP", "OP", 30, false);
            this._flexD.SetCol("CD_WC", "작업장", 60, false);
            this._flexD.SetCol("NM_WC", "작업장명", 100, false);
            this._flexD.SetCol("CD_WCOP", "공정", 60, false);
            this._flexD.SetCol("NM_OP", "공정명", 100, false);
            this._flexD.SetCol("QT_WO", "지시수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_PO", "발주수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_WORK", "작업수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_REJECT", "불량수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_REWORK", "재작업수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("UM_EX", "외화단가", 90, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexD.SetCol("UM_WORK", "실적단가", 90, false, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexD.SetCol("AM_EX", "외화금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD.SetCol("AM_WORK", "실적금액", 90, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD.SetCol("AM_VAT_WORK", "부가세", 90, false, typeof(decimal), (FormatTpType)3);
            this._flexD.SetCol("AM_HAP_WORK", "합계금액", 90, false, typeof(decimal), (FormatTpType)3);
            this._flexD.SetCol("NO_PO", "발주번호", 100, 20, false);
            this._flexD.SetCol("NO_LINE", "발주항번", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("UNIT_IM", "지시단위", 100, false);
            this._flexD.SetCol("UNIT_CH", "변환단위", 100, false);
            this._flexD.SetCol("QT_CHCOEF", "변환계수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("OLD_QT_PO", "변환전수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_WORK_CHCOEF", "실적수량(변환)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("QT_WORK_BAD_CHCOEF", "불량수량(변환)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("UM_EX_PO", "외화단가(공정외주)", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexD.SetCol("UM", "단가(공정외주)", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexD.SetCol("AM_EX_PO", "외화금액(공정외주)", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD.SetCol("AM", "금액(공정외주)", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD.SetCol("AM_VAT", "부가세(공정외주)", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD.SetCol("AM_TOTAL", "합계(공정외주)", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD.SetCol("DT_LIMIT", "유효일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD.SetCol("NO_LOT", "LOT번호", 100, 50, false, typeof(string));
            if (this.LOT관리항목DT.Rows.Count != 0)
            {
                foreach (DataRow row in this.LOT관리항목DT.Rows)
                {
                    int num = D.GetInt(row["CODE"]);
                    string str = "CD_MNG" + num;
                    Type type = null;
                    FormatTpType formatTpType = FormatTpType.NONE;
                    if (MA.ServerKey(false, new string[] { "SDBIO", "SKPLASMA" }))
                    {
                        if (num == 1)
                        {
                            type = typeof(string);
                            formatTpType = FormatTpType.YEAR_MONTH_DAY;
                        }
                    }
                    else if (MA.ServerKey(false, new string[] { "WINPLUS" }))
                    {
                        if (num == 6 || num == 7)
                        {
                            type = typeof(decimal);
                            formatTpType = FormatTpType.QUANTITY;
                        }
                        else if (num == 11)
                        {
                            type = typeof(string);
                            formatTpType = FormatTpType.YEAR_MONTH_DAY;
                        }
                    }
                    if (type != null)
                        this._flexD.SetCol(str, D.GetString(row["NAME"]), 80, false, type, formatTpType);
                    else
                        this._flexD.SetCol(str, D.GetString(row["NAME"]), 100, 200, false, typeof(string));
                }
            }
            this._flexD.SetCol("DC_RMK_WO", "작업지시비고", 120, false);
            this._flexD.SetCol("DC_RMK1", "비고1", 120, true);
            this._flexD.SetCol("DC_RMK2", "비고2", 120, true);
            this._flexD.SetCol("DC_RMK3", "비고3", 120, true);
            this._flexD.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            if (this.LoginInfo.MngLot != "Y")
            {
                this._flexD.Cols["NO_LOT"].Visible = false;
                if (this.LOT관리항목DT.Rows.Count != 0)
                {
                    foreach (DataRow row in this.LOT관리항목DT.Rows)
                        this._flexD.Cols["CD_MNG" + D.GetInt(row["CODE"])].Visible = false;
                }
            }
            this._flexD.SetExceptSumCol(new string[] { "QT_WO",
                                                       "QT_PO",
                                                       "UM_EX",
                                                       "UM_WORK",
                                                       "NO_LINE" });
            this._flexD.Cols["CD_PLANT"].Visible = false;
            this._flexD.Cols["NO_WO"].Visible = false;
            this._flexD.Cols["CD_WC"].Visible = false;
            this._flexD.Cols["CD_WCOP"].Visible = false;
            this._flexD.Cols["NO_PO"].Visible = false;
            this._flexD.Cols["NO_LINE"].Visible = false;
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            this._flexM.BeforeRowChange += new RangeEventHandler(this._flexM_BeforeRowChange);
            this._flexM.AfterRowChange += new RangeEventHandler(this._flexM_AfterRowChange);
            this._flexD.StartEdit += new RowColEventHandler(this._flexD_StartEdit);

            this.ctx작업지시번호.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx외주처.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx작업품목.CodeChanged += new EventHandler(this.OnBpControl_CodeChanged);
            this.ctx작업품목.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx작업품목.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);

            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
        }
        protected override void InitPaint()
        {
            base.InitPaint();

            DataSet comboData = this.GetComboData(new string[] { "NC;MA_PLANT", "S;MA_B000004" });
            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";
            if (comboData.Tables[0].Select("CODE = '" + this.LoginInfo.CdPlant + "'").Length == 1)
                this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;
            else if (this.cbo공장.Items.Count > 0)
                this.cbo공장.SelectedIndex = 0;
            this.dtp발주기간.StartDateToString = this.MainFrameInterface.GetStringToday.Substring(0, 6) + "01";
            this.dtp발주기간.EndDateToString = this.MainFrameInterface.GetStringToday;
            this.dtp작업기간.StartDateToString = this.MainFrameInterface.GetStringToday.Substring(0, 6) + "01";
            this.dtp작업기간.EndDateToString = this.MainFrameInterface.GetStringToday;
            this._flexD.SetDataMap("UNIT_IM", comboData.Tables[1].Copy(), "CODE", "NAME");
            this._flexD.SetDataMap("UNIT_CH", comboData.Tables[1].Copy(), "CODE", "NAME");
            this.oneGrid1.UseCustomLayout = true;
            this.bpP_Plant.IsNecessaryCondition = true;
            this.bpP_Status.IsNecessaryCondition = true;
            this.bpP_Dt_Work.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
        }


        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarAddButtonEnabled = false;
                this.ToolBarPrintButtonEnabled = false;
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
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장.Text });
                this.cbo공장.Focus();
                return false;
            }
            if (!this.chk미마감.Checked && !this.chk마감.Checked)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lblStatus.Text });
                this.chk미마감.Focus();
                return false;
            }
            if (!this.chk발주기간.Checked && !this.chk작업기간.Checked)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.chk발주기간.Text, this.chk작업기간.Text });
                this.chk발주기간.Focus();
                return false;
            }
            return (!this.chk발주기간.Checked || Checker.IsValid(this.dtp발주기간, false, this.chk발주기간.Text)) && (!this.chk작업기간.Checked || Checker.IsValid(this.dtp작업기간, false, this.chk작업기간.Text));
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!base.BeforeSearch())
                    return;
                string str1 = "N";
                string str2 = "N";
                if (this.chk발주기간.Checked)
                    str1 = "Y";
                if (this.chk작업기간.Checked)
                    str2 = "Y";
                DataTable dataTable = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                      this.cbo공장.SelectedValue.ToString(),
                                                                      this.ctx작업품목.CodeValue,
                                                                      this.ctx외주처.CodeValue,
                                                                      this.dtp발주기간.StartDateToString,
                                                                      this.dtp발주기간.EndDateToString,
                                                                      this.마감구분,
                                                                      "",
                                                                      this.LoginInfo.EmployeeNo,
                                                                      str1,
                                                                      str2,
                                                                      this.dtp작업기간.StartDateToString,
                                                                      this.dtp작업기간.EndDateToString,
                                                                      this.ctx작업지시번호.CodeValue,
                                                                      Global.SystemLanguage.MultiLanguageLpoint });
                if (this._flexD.DataTable != null)
                {
                    this._flexD.DataTable.Rows.Clear();
                    this._flexD.AcceptChanges();
                }
                this._flexM.Binding = dataTable;
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

                foreach (DataRow dataRow1 in dataRowArray)
                {
                    string[] strArray = new string[] { "CD_PLANT = '",
                                                       dataRow1["CD_PLANT"].ToString(),
                                                       "' AND NO_PO = '",
                                                       dataRow1["NO_PO"].ToString(),
                                                       "' AND NO_LINE = '",
                                                       dataRow1["NO_LINE"].ToString(),
                                                       "' " };
                    foreach (DataRow dataRow2 in this._flexD.DataTable.Select(string.Concat(strArray), ""))
                        dataRow2.Delete();
                    dataRow1["CD_PLANT"].ToString();
                    dataRow1["NO_PO"].ToString();
                    dataRow1.Delete();
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
                if (!base.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
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
            if (!base.SaveData() || !base.Verify())
                return false;
            DataTable changes = this._flexD.GetChanges();
            if (changes == null)
                return true;
            if (!this._biz.Save(changes))
                return false;

            this._flexM.AcceptChanges();
            this._flexD.AcceptChanges();
            return true;
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;

                this._flexD.Rows.Remove(this._flexD.Row);
                if (!this._flexD.HasNormalRow)
                    this._flexM.Rows.Remove(this._flexM.Row);
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

        private void _flexM_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string filter;
            try
            {
                this.ToolBarSearchButtonEnabled = false;
                this.bGridrowChanging = false;

                dt = null;
                filter = "CD_PLANT = '" + this._flexM["CD_PLANT"].ToString() + "' AND NO_PO = '" + this._flexM["NO_PO"].ToString() + "' AND NO_LINE = '" + this._flexM["NO_LINE"].ToString() + "'";
                if (this._flexM.DetailQueryNeed)
                    dt = this._biz.SearchDetail1(new object[] { this.LoginInfo.CompanyCode,
                                                                this._flexM["CD_PLANT"].ToString(),
                                                                this._flexM["NO_PO"].ToString(),
                                                                this._flexM["NO_LINE"].ToString(),
                                                                this.LoginInfo.EmployeeNo,
                                                                this.ctx작업지시번호.CodeValue,
                                                                Global.SystemLanguage.MultiLanguageLpoint });

                this._flexD.BindingAdd(dt, filter);
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

        private void _flexD_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                switch (this._flexD.Cols[e.Col].Name)
                {
                    case "DC_RMK1":
                    case "DC_RMK2":
                    case "DC_RMK3":
                        break;
                    default:
                        e.Cancel = true;
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
            try
            {
                HelpID helpId = e.HelpID;
                if (helpId == HelpID.P_MA_PITEM_SUB || helpId == HelpID.P_PR_WO_REG_SUB)
                {
                    if (!this.공장선택여부)
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
                DataRow[] rows = e.HelpReturn.Rows;
                this.txt품목규격.Text = rows[0]["STND_ITEM"].ToString();
                this.txt재고단위.Text = rows[0]["UNIT_IMNM"].ToString();
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
                this.txt품목규격.Text = this.txt재고단위.Text = "";
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
                        if (!datePicker.Modified || datePicker.Text == "" || datePicker.IsValidated)
                            break;
                        this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);
                        datePicker.Text = "";
                        datePicker.Focus();
                        e.Cancel = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private string 진행상태
        {
            get
            {
                string 진행상태 = "";
                if (this.chk미마감.Checked && this.chk마감.Checked)
                    진행상태 = "SC";
                else if (this.chk미마감.Checked && !this.chk마감.Checked)
                    진행상태 = "S";
                else if (!this.chk미마감.Checked && this.chk마감.Checked)
                    진행상태 = "C";
                else if (!this.chk미마감.Checked && !this.chk마감.Checked)
                    진행상태 = "";
                return 진행상태;
            }
        }

        private string 마감구분
        {
            get
            {
                string 마감구분 = "";
                if (this.chk미마감.Checked && this.chk마감.Checked)
                    마감구분 = "S|C|";
                else if (this.chk미마감.Checked && !this.chk마감.Checked)
                    마감구분 = "S|";
                else if (!this.chk미마감.Checked && this.chk마감.Checked)
                    마감구분 = "C|";
                else if (!this.chk미마감.Checked && !this.chk마감.Checked)
                    마감구분 = "|";
                return 마감구분;
            }
        }

        private bool 공장선택여부 => this.cbo공장.SelectedValue != null && !(this.cbo공장.SelectedValue.ToString() == "");
    }
}

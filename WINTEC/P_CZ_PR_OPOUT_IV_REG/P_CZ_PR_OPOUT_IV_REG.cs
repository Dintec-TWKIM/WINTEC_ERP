using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.Windows.Print;
using prd;
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
    public partial class P_CZ_PR_OPOUT_IV_REG : PageBase
    {
        private P_CZ_PR_OPOUT_IV_REG_BIZ _biz = new P_CZ_PR_OPOUT_IV_REG_BIZ();
        private string 지급관리통제설정 = "N";
        public P_CZ_PR_OPOUT_IV_REG()
        {
            InitializeComponent();
        }
        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitGrid();
            this.InitEvent();
        }
        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };
            this._flexH.WhenRowChangeThenGetDetail = false;

            this._flexH.BeginSetting(1, 1, false);
            this._flexH.SetCol("CHK", "S", 30, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NO_IV", "계산서번호", 100, false);
            this._flexH.SetCol("NO_TEMP", "임시번호", false);
            this._flexH.SetCol("CD_PLANT", "공장코드", false);
            this._flexH.SetCol("CD_PARTNER", "거래처코드", 100, false);
            this._flexH.SetCol("LN_PARTNER", "거래처명", 140, false);
            this._flexH.SetCol("CD_BIZAREA_TAX", "부가세사업장", 100, false);
            this._flexH.SetCol("NM_BIZAREA_TAX", "부가세사업장명", 120, false);
            this._flexH.SetCol("NM_FG_TAX", "과세구분", 100, false);
            this._flexH.SetCol("AM_CLS", "공급가액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("AM_VAT", "부가세", 90, 17, true, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("AM_HAP", "총금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("FG_PAYBILL", "지급조건", 80, true, typeof(string));
            this._flexH.SetCol("DT_PAY_PREARRANGED", "지급예정일", 90, 17, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_DUE", "만기일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("YN_DOCU", "전표처리여부", 90, false);
            this._flexH.SetCol("CD_CC", "C/C코드", false);
            this._flexH.SetCol("NM_CC", "C/C", false);
            this._flexH.SetCol("FG_TPPURCHASE", "매입형태코드", false);
            this._flexH.SetCol("NM_TP", "매입형태", false);
            this._flexH.SetCol("CD_EXCH", "환종코드", false);
            this._flexH.SetCol("NM_EXCH", "환종명", false);
            this._flexH.SetCol("RT_EXCH", "환율", false);
            this._flexH.SetCol("FG_TAX", "과세구분", false);
            this._flexH.SetCol("DC_RMK", "비고", 140, 100, true);
            this._flexH.SetCol("NO_DOCU", "전표번호", false);
            this._flexH.VerifyNotNull = new string[] { "CD_PARTNER" };
            this._flexH.SettingVersion = "1.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flexH.SetDummyColumn(new string[] { "CHK" });
            this._flexH.SetExceptEditCol(new string[] { "NM_BIZAREA_TAX" });
           this._flexH.Cols["YN_DOCU"].TextAlign = TextAlignEnum.CenterCenter;

            this._flexL.BeginSetting(1, 1, false);
            this._flexL.SetCol("NO_IV", "계산서번호", false);
            this._flexL.SetCol("NO_TEMP", "임시번호", false);
            this._flexL.SetCol("NO_LINE", "항번", false);
            this._flexL.SetCol("CD_PLANT", "공장코드", false);
            this._flexL.SetCol("CD_PARTNER", "거래처코드", false);
            this._flexL.SetCol("NO_WORK", "실적번호", 100, false);
            this._flexL.SetCol("NO_WO", "작업지시번호", false);
            this._flexL.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexL.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexL.SetCol("STND_ITEM", "규격", 120, false);
            this._flexL.SetCol("UNIT_IM", "단위", 40, false);
            this._flexL.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), (FormatTpType)1);
            this._flexL.SetCol("UM_EXCLS", "외화단가", 90, 17, false, typeof(decimal), (FormatTpType)4);
            this._flexL.SetCol("UM_CLS", "단가", 90, 17, false, typeof(decimal), (FormatTpType)2);
            this._flexL.SetCol("AM_EXCLS", "외화금액", 90, 17, false, typeof(decimal), (FormatTpType)5);
            this._flexL.SetCol("AM_CLS", "금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_HAP", "총금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("NO_PO", "발주번호", 100, false);
            this._flexL.SetCol("NO_POLINE", "발주항번", 60, 17, false, typeof(decimal), (FormatTpType)1);
            this._flexL.SetCol("CD_OP", "OP", false);
            this._flexL.SetCol("CD_WC", "작업장코드", false);
            this._flexL.SetCol("NM_WC", "작업장", false);
            this._flexL.SetCol("CD_WCOP", "공정코드", false);
            this._flexL.SetCol("NM_OP", "공정", false);
            this._flexL.SetCol("UNIT_CH", "변환단위", 100, false);
            this._flexL.SetCol("QT_CHCOEF", "변환계수", 100, false, typeof(decimal), (FormatTpType)1);
            this._flexL.SetCol("QT_WORK_CHCOEF", "수량(변환)", 100, false, typeof(decimal), (FormatTpType)1);
            this._flexL.SetCol("QT_WORK_BAD_CHCOEF", "불량수량(변환)", 100, false, typeof(decimal), (FormatTpType)1);
            this._flexL.SetCol("QT_CLS_CHCOEF", "마감수량(변환)", 100, false, typeof(decimal), (FormatTpType)1);
            this._flexL.SetCol("CD_PJT", "프로젝트코드", 100);
            this._flexL.SetCol("NM_PROJECT", "프로젝트명", 100);
            if (Config.MA_ENV.프로젝트사용)
                this._flexL.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexL.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexL.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexL.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
            }
            this._flexL.SetCol("FG_TPPURCHASE", "매입형태코드", false);
            this._flexL.SetCol("NM_TP", "매입형태", 80, false);
            this._flexL.SetCol("EN_ITEM", "품목명(영)", false);
            this._flexL.SetCol("STND_DETAIL_ITEM", "세부규격", false);
            this._flexL.SetCol("MAT_ITEM", "재질", false);
            this._flexL.SetCol("NM_MAKER", "Maker", false);
            this._flexL.SetCol("BARCODE", "BARCODE", false);
            this._flexL.SetCol("NO_MODEL", "모델번호", false);
            if (MA.ServerKey(false, new string[] { "JIGLS" }))
                this._flexL.SetCol("TXT_USERDEF1_WO", "규격변경", 80, false);
            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                this._flexL.SetExceptSumCol(new string[] { "UM_EXCLS",
                                                           "UM_CLS",
                                                           "SEQ_PROJECT" });
                this._flexL.VerifyNotNull = new string[] { "CD_PARTNER",
                                                           "SEQ_PROJECT" };
            }
            else
            {
                this._flexL.SetExceptSumCol(new string[] { "UM_EXCLS",
                                                           "UM_CLS" });
                this._flexL.VerifyNotNull = new string[] { "CD_PARTNER" };
            }
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexL.Cols["NO_POLINE"].TextAlign = TextAlignEnum.CenterCenter;
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.지급관리통제설정 = BASIC.GetMAEXC("거래처정보등록-회계지급관리사용여부");
            if (this.지급관리통제설정 == string.Empty || this.지급관리통제설정 == "000")
                this.지급관리통제설정 = "N";

            SetControl setControl = new SetControl();
            setControl.SetCombobox(this.cbo공장, MA.GetCode("MA_PLANT_AUTH"));
            setControl.SetCombobox(this.cbo거래구분, MA.GetCode("PU_C000016"));
            if (this.지급관리통제설정 == "N")
            {
                this._flexH.SetDataMap("FG_PAYBILL", MA.GetCode("PU_C000044"), "CODE", "NAME");
            }
            else
            {
                DataTable payList = ComFunc.GetPayList();
                if (payList != null)
                    this._flexH.SetDataMap("FG_PAYBILL", payList, "CODE", "NAME");
            }
            if (!string.IsNullOrEmpty(this.LoginInfo.CdPlant))
                this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;
            else
                this.cbo공장.SelectedIndex = 0;
            this.cbo거래구분.SelectedValue = "001";
            if (!string.IsNullOrEmpty(this.LoginInfo.BizAreaCode))
                this.ctx부가세사업장.SetCode(this.LoginInfo.BizAreaCode, this.LoginInfo.BizAreaName);
            if (!string.IsNullOrEmpty(this.LoginInfo.EmployeeNo))
                this.ctx담당자.SetCode(this.LoginInfo.EmployeeNo, this.LoginInfo.EmployeeName);
            this.dp처리일자.Mask = this.GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dp처리일자.ToDayDate = this.MainFrameInterface.GetDateTimeToday();
            this.dp처리일자.Text = this.MainFrameInterface.GetStringToday;
            DataSet dataSet = this._biz.search(new object[] { this.LoginInfo.CompanyCode,
                                                              string.Empty,
                                                              string.Empty,
                                                              Global.SystemLanguage.MultiLanguageLpoint });
            this._flexH.Binding = dataSet.Tables[0];
            this._flexL.Binding = dataSet.Tables[1];

            this.oneGrid1.UseCustomLayout = true;
            this.oneGrid1.IsSearchControl = false;
            this._bpPnl_plant.IsNecessaryCondition = true;
            this._bpPnl_emp.IsNecessaryCondition = true;
            this._bpPnl_deal.IsNecessaryCondition = true;
            this._bpPnl_dt.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
        }

        private void InitEvent()
        {
            this.btn실적적용.Click += new EventHandler(this.btn실적적용_Click);
            this.btn세금계산서.Click += new EventHandler(this.btn세금계산서_Click);
            this.btn변경.Click += new EventHandler(this.btn_CHG_BIZAREA_Click);

            this._flexH.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flexH.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flexH.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._flexL.StartEdit += new RowColEventHandler(this._flex_StartEdit);

            this.btn변경.Click += new EventHandler(this.btn_CHG_BIZAREA_Click);
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarSearchButtonEnabled = false;
                this.ToolBarSaveButtonEnabled = this.IsChanged();
                this.btn실적적용.Enabled = !this._flexH.HasNormalRow;
                this.rdo일반외주.Enabled = !this._flexH.HasNormalRow;
                this.rdo공정외주.Enabled = !this._flexH.HasNormalRow;
                this.rdo일괄.Enabled = !this._flexH.HasNormalRow;
                this.rdo건별.Enabled = !this._flexH.HasNormalRow;
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
                if (!this.BeforeAdd())
                    return;
                if (!string.IsNullOrEmpty(this.LoginInfo.CdPlant))
                    this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;
                else
                    this.cbo공장.SelectedIndex = 0;
                this.ctx담당자.CodeValue = this.LoginInfo.EmployeeNo;
                this.ctx담당자.CodeName = this.LoginInfo.EmployeeName;
                this.cbo거래구분.SelectedIndex = 0;
                this.dp처리일자.Text = this.MainFrameInterface.GetStringToday;
                if (this._flexL.HasNormalRow)
                    this._flexL.DataTable.Rows.Clear();
                if (this._flexH.HasNormalRow)
                    this._flexH.DataTable.Rows.Clear();
                this.btn세금계산서.Enabled = false;
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
                if (!this.BeforeDelete() || !this._flexH.HasNormalRow)
                    return;
                this._flexH.Redraw = false;
                this._flexL.Redraw = false;
                for (int index = this._flexL.Rows.Count - this._flexL.Rows.Fixed; index >= this._flexL.Rows.Fixed; --index)
                    this._flexL.Rows.Remove(index);
                this._flexH.Rows.Remove(this._flexH.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexH.Redraw = true;
                this._flexL.Redraw = true;
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.Chk_PLANT || !this.Chk_EMP || !this.Chk_BUSI || !this.Chk_DT || !this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
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
            if (!this.Verify())
                return false;
            if (this._flexH.GetChanges(DataRowState.Added) != null)
            {
                DataRow[] seq = (DataRow[])this.GetSeq(this.LoginInfo.CompanyCode, "SU", "12", this.dp처리일자.Text.Substring(0, 6), this._flexH.DataTable.Rows.Count);
                for (int index1 = 0; index1 < this._flexH.DataTable.Rows.Count; ++index1)
                {
                    if (this._flexH.DataTable.Rows[index1].RowState == DataRowState.Added)
                    {
                        this._flexH.DataTable.Rows[index1]["NO_IV"] = seq[index1]["DOCU_NO"];
                        DataRow[] dataRowArray = null;
                        if ((this.rdo일괄).Checked)
                            dataRowArray = this._flexL.DataTable.Select("CD_PARTNER='" + D.GetString(this._flexH.DataTable.Rows[index1]["CD_PARTNER"]) + "' ", "CD_PARTNER", DataViewRowState.CurrentRows);
                        else if ((this.rdo건별).Checked)
                            dataRowArray = this._flexL.DataTable.Select("NO_WORK='" + D.GetString(this._flexH.DataTable.Rows[index1]["NO_WORK"]) + "' ", "NO_WORK", DataViewRowState.CurrentRows);
                        for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
                        {
                            dataRowArray[index2]["NO_IV"] = seq[index1]["DOCU_NO"];
                            dataRowArray[index2]["NO_LINE"] = (index2 + 1);
                        }
                    }
                }
            }
            DataTable changes1 = this._flexH.GetChanges();
            DataTable changes2 = this._flexL.GetChanges();
            if (changes1 == null && changes2 == null || !this._biz.save(changes1, changes2, this.dp처리일자.Text, this.ctx담당자.CodeValue))
                return false;
            this._flexH.DataTable.AcceptChanges();
            this._flexL.DataTable.AcceptChanges();
            this.btn세금계산서.Enabled = true;
            return true;
        }

        public virtual void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforePrint() || !this.MsgAndSave((PageActionMode)3))
                    return;
                ReportHelper reportHelper = new ReportHelper("R_PR_OPOUT_IV_REG_0", "공정외주매입등록");
                reportHelper.SetData("공장코드", D.GetString(this.cbo공장.SelectedValue));
                reportHelper.SetData("공장명", this.cbo공장.Text);
                reportHelper.SetData("담당자코드", this.ctx담당자.CodeValue);
                reportHelper.SetData("담당자명", this.ctx담당자.CodeName);
                reportHelper.SetData("공정외주여부", this.rdo일반외주.Checked ? "N" : (this.rdo공정외주.Checked ? "Y" : ""));
                reportHelper.SetData("일괄건별여부", this.rdo일괄.Checked ? "일괄" : (this.rdo건별.Checked ? "건별" : ""));
                reportHelper.SetData("거래구분코드", D.GetString(this.cbo거래구분.SelectedValue));
                reportHelper.SetData("거래구분명", this.cbo거래구분.Text);
                reportHelper.SetData("처리일자", this.dp처리일자.Text);
                DataTable dataTable = this._flexH.DataTable.Copy();
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (this._flexH.Cols.Contains(column.ColumnName))
                        column.Caption = this._flexH.Cols[column.ColumnName].Caption;
                }
                reportHelper.SetDataTable(dataTable);
                reportHelper.Print();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn실적적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Chk_PLANT || !this.Chk_EMP || !this.Chk_BUSI || !this.Chk_DT)
                    return;
                DataTable dataTable1 = new DataTable();
                DataTable returnTable;
                if (BASIC.GetMAEXC_Menu(nameof(P_CZ_PR_OPOUT_IV_REG), "PR_A00000008") == "100")
                {
                    P_PR_WO_ROUT_SU_IV_SUB pPrWoRoutSuIvSub = new P_PR_WO_ROUT_SU_IV_SUB();
                    pPrWoRoutSuIvSub.계산서처리구분 = !this.rdo일괄.Checked ? "002" : "001";
                    pPrWoRoutSuIvSub.거래구분 = D.GetString(this.cbo거래구분.SelectedValue);
                    pPrWoRoutSuIvSub.과세구분 = "";
                    pPrWoRoutSuIvSub.공장 = D.GetString(this.cbo공장.SelectedValue);
                    if (pPrWoRoutSuIvSub.ShowDialog(this) != DialogResult.OK)
                        return;
                    returnTable = pPrWoRoutSuIvSub.ReturnTable;
                }
                else
                {
                    P_CZ_PR_OPOUT_IV_APPLY_SUB prOpoutIvApplySub = new P_CZ_PR_OPOUT_IV_APPLY_SUB();
                    prOpoutIvApplySub.계산서처리구분 = !this.rdo일괄.Checked ? "002" : "001";
                    prOpoutIvApplySub.거래구분 = D.GetString(this.cbo거래구분.SelectedValue);
                    prOpoutIvApplySub.과세구분 = "";
                    prOpoutIvApplySub.공장 = D.GetString(this.cbo공장.SelectedValue);
                    if (prOpoutIvApplySub.ShowDialog(this) != DialogResult.OK)
                        return;
                    returnTable = prOpoutIvApplySub.ReturnTable;
                }
                if (returnTable == null)
                    return;
                decimal maxValue = this._flexL.GetMaxValue("NO_LINE");
                this._flexL.Redraw = false;
                DataTable dataTable2 = this._flexL.DataTable.Clone().Copy();
                foreach (DataRow row1 in returnTable.Rows)
                {
                    ++maxValue;
                    DataRow row2 = dataTable2.NewRow();
                    row2["NO_IV"] = "";
                    row2["NO_TEMP"] = "";
                    row2["NO_LINE"] = maxValue;
                    row2["CD_PLANT"] = row1["CD_PLANT"];
                    row2["CD_PARTNER"] = row1["CD_PARTNER"];
                    row2["CD_ITEM"] = row1["CD_ITEM"];
                    row2["NM_ITEM"] = row1["NM_ITEM"];
                    row2["STND_ITEM"] = row1["STND_ITEM"];
                    row2["UNIT_IM"] = row1["UNIT_IM"];
                    row2["QT_CLS"] = row1["QT_MOVE"];
                    row2["UM_EXCLS"] = row1["UM_EX"];
                    row2["UM_CLS"] = row1["UM_WORK"];
                    row2["AM_EXCLS"] = Unit.외화금액(DataDictionaryTypes.PR, D.GetDecimal(row1["AM_EX"]));
                    row2["AM_CLS"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(row1["AM_WORK"]));
                    row2["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(row1["AM_VAT_WORK"]));
                    row2["AM_HAP"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(row1["AM_HAP_WORK"]));
                    row2["NO_WO"] = row1["NO_WO"];
                    row2["NO_WORK"] = row1["NO_WORK"];
                    row2["NO_PO"] = row1["NO_PO"];
                    row2["NO_POLINE"] = row1["NO_POLINE"];
                    row2["CD_OP"] = row1["CD_OP"];
                    row2["CD_WC"] = row1["CD_WC"];
                    row2["CD_WCOP"] = row1["CD_WCOP"];
                    row2["NM_WC"] = row1["NM_WC"];
                    row2["NM_OP"] = row1["NM_OP"];
                    row2["UM_MATL"] = row1["UM_MATL"];
                    row2["UM_SOUL"] = row1["UM_SOUL"];
                    row2["UNIT_CH"] = row1["UNIT_CH"];
                    row2["QT_CHCOEF"] = row1["QT_CHCOEF"];
                    row2["QT_WORK_CHCOEF"] = row1["QT_WORK_CHCOEF"];
                    row2["QT_WORK_BAD_CHCOEF"] = row1["QT_WORK_BAD_CHCOEF"];
                    row2["QT_CLS_CHCOEF"] = Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(row1["QT_MOVE"]) * D.GetDecimal(row1["QT_CHCOEF"]));
                    if (returnTable.Columns.Contains("CD_PJT"))
                        row2["CD_PJT"] = D.GetString(row1["CD_PJT"]);
                    if (returnTable.Columns.Contains("NM_PROJECT"))
                        row2["NM_PROJECT"] = D.GetString(row1["NM_PROJECT"]);
                    row2["SEQ_PROJECT"] = 0M;
                    if (returnTable.Columns.Contains("SEQ_PROJECT") && Config.MA_ENV.YN_UNIT == "Y")
                        row2["SEQ_PROJECT"] = D.GetDecimal(row1["SEQ_PROJECT"]);
                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        if (returnTable.Columns.Contains("CD_PJT_ITEM"))
                            row2["CD_PJT_ITEM"] = row1["CD_PJT_ITEM"];
                        if (returnTable.Columns.Contains("NM_PJT_ITEM"))
                            row2["NM_PJT_ITEM"] = row1["NM_PJT_ITEM"];
                        if (returnTable.Columns.Contains("PJT_ITEM_STND"))
                            row2["PJT_ITEM_STND"] = row1["PJT_ITEM_STND"];
                    }
                    row2["FG_TPPURCHASE"] = row1["FG_TPPURCHASE"];
                    row2["NM_TP"] = row1["NM_TP"];
                    if (returnTable.Columns.Contains("EN_ITEM"))
                        row2["EN_ITEM"] = D.GetString(row1["EN_ITEM"]);
                    if (returnTable.Columns.Contains("STND_DETAIL_ITEM"))
                        row2["STND_DETAIL_ITEM"] = D.GetString(row1["STND_DETAIL_ITEM"]);
                    if (returnTable.Columns.Contains("MAT_ITEM"))
                        row2["MAT_ITEM"] = D.GetString(row1["MAT_ITEM"]);
                    if (returnTable.Columns.Contains("NM_MAKER"))
                        row2["NM_MAKER"] = D.GetString(row1["NM_MAKER"]);
                    if (returnTable.Columns.Contains("BARCODE"))
                        row2["BARCODE"] = D.GetString(row1["BARCODE"]);
                    if (returnTable.Columns.Contains("NO_MODEL"))
                        row2["NO_MODEL"] = D.GetString(row1["NO_MODEL"]);
                    if (returnTable.Columns.Contains("TXT_USERDEF1_WO"))
                        row2["TXT_USERDEF1_WO"] = D.GetString(row1["TXT_USERDEF1_WO"]);
                    dataTable2.Rows.Add(row2);
                }
                this._flexL.BindingAdd(dataTable2, "", false);
                this._flexL.Redraw = true;
                int num1;
                string str1;
                string empty;
                DataTable dataTable3;
                if ((this.rdo일괄).Checked)
                {
                    DataTable table = this._flexH.DataTable.DefaultView.ToTable(true, "CD_PARTNER");
                    table.PrimaryKey = new DataColumn[] { table.Columns["CD_PARTNER"] };
                    num1 = -1;
                    this._flexH.Redraw = false;
                    str1 = null;
                    empty = string.Empty;
                    dataTable3 = null;
                    foreach (DataRow row3 in returnTable.Rows)
                    {
                        if (!table.Rows.Contains(D.GetString(row3["CD_PARTNER"])))
                        {
                            table.Rows.Add(D.GetString(row3["CD_PARTNER"]));
                            DataTable dataTable4 = this._biz.Day(D.GetString(row3["CD_PARTNER"]));
                            string str2 = null;
                            foreach (DataRow row4 in dataTable4.Rows)
                                str2 = D.GetString(row4["DT_PAY_PREARRANGED"]);
                            string str3 = this.dp처리일자.ToDayDate.AddDays((double)D.GetInt(str2)).ToShortDateString().Replace("-", "");
                            this._flexH.Rows.Add();
                            this._flexH.Row = (this._flexH.Rows).Count - 1;
                            this._flexH["CD_PLANT"] = row3["CD_PLANT"];
                            this._flexH["NO_IV"] = "";
                            this._flexH["NO_TEMP"] = "";
                            this._flexH["CD_PARTNER"] = row3["CD_PARTNER"];
                            this._flexH["NM_FG_TAX"] = row3["NM_FG_TAX"];
                            this._flexH["AM_CLS"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(row3["AM_WORK"]));
                            this._flexH["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(row3["AM_VAT_WORK"]));
                            this._flexH["AM_HAP"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(row3["AM_HAP_WORK"]));
                            this._flexH["YN_DOCU"] = "N";
                            this._flexH["NO_DOCU"] = "";
                            this._flexH["LN_PARTNER"] = row3["LN_PARTNER"];
                            this._flexH["CD_CC"] = D.GetString(row3["CD_CC"]);
                            this._flexH["NM_CC"] = row3["NM_CC"];
                            this._flexH["FG_TPPURCHASE"] = row3["FG_TPPURCHASE"];
                            this._flexH["NM_TP"] = row3["NM_TP"];
                            this._flexH["CD_EXCH"] = row3["CD_EXCH"];
                            this._flexH["NM_EXCH"] = row3["NM_EXCH"];
                            this._flexH["RT_EXCH"] = row3["RT_EXCH"];
                            this._flexH["FG_TAX"] = row3["FG_TAX"];
                            this._flexH["CD_BIZAREA_TAX"] = this.ctx부가세사업장.CodeValue;
                            this._flexH["NM_BIZAREA_TAX"] = this.ctx부가세사업장.CodeName;
                            this._flexH[this._flexH.Row, "FG_PAYBILL"] = row3["FG_PAYBILL"];
                            if (this.지급관리통제설정 == "Y")
                            {
                                decimal num2 = 0M + D.GetDecimal(row3["AM_EX"]);
                                string str4 = ComFunc.만기예정일자(this.dp처리일자.Text, num2, D.GetString(row3["FG_PAYBILL"]), "1");
                                if (str4 != string.Empty)
                                    this._flexH[this._flexH.Row, "DT_PAY_PREARRANGED"] = str4;
                                string str5 = ComFunc.만기예정일자(this.dp처리일자.Text, num2, D.GetString(row3["FG_PAYBILL"]), "2");
                                if (str5 != string.Empty)
                                    this._flexH[this._flexH.Row, "DT_DUE"] = str5;
                            }
                            else
                                this._flexH[this._flexH.Row, "DT_PAY_PREARRANGED"] = str3;
                            this._flexH.AddFinished();
                            this._flexH.Col = (this._flexH.Cols).Fixed;
                        }
                        else
                        {
                            int row5 = this._flexH.FindRow(D.GetString(row3["CD_PARTNER"]), (this._flexH.Rows).Fixed, ((RowCol)this._flexH.Cols["CD_PARTNER"]).Index, true);
                            this._flexH[row5, "AM_CLS"] = Unit.원화금액(DataDictionaryTypes.PR, this._flexH.CDecimal(this._flexH[row5, "AM_CLS"]) + this._flexH.CDecimal(row3["AM_WORK"]));
                            this._flexH[row5, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, this._flexH.CDecimal(this._flexH[row5, "AM_VAT"]) + this._flexH.CDecimal(row3["AM_VAT_WORK"]));
                            this._flexH[row5, "AM_HAP"] = Unit.원화금액(DataDictionaryTypes.PR, this._flexH.CDecimal(this._flexH[row5, "AM_HAP"]) + this._flexH.CDecimal(row3["AM_HAP_WORK"]));
                        }
                    }
                  this._flexH.Redraw = true;
                }
                else
                {
                    DataTable table = this._flexH.DataTable.DefaultView.ToTable(true, "NO_WORK");
                    table.PrimaryKey = new DataColumn[1]
                    {
            table.Columns["NO_WORK"]
                    };
                    num1 = -1;
                    this._flexH.Redraw = false;
                    str1 = null;
                    empty = string.Empty;
                    dataTable3 = null;
                    foreach (DataRow row6 in returnTable.Rows)
                    {
                        if (!table.Rows.Contains(D.GetString(row6["NO_WORK"])))
                        {
                            table.Rows.Add(D.GetString(row6["NO_WORK"]));
                            DataTable dataTable5 = this._biz.Day(D.GetString(row6["NO_WORK"]));
                            string str6 = null;
                            foreach (DataRow row7 in dataTable5.Rows)
                                str6 = D.GetString(row7["DT_PAY_PREARRANGED"]);
                            string str7 = this.dp처리일자.ToDayDate.AddDays((double)D.GetInt(str6)).ToShortDateString().Replace("-", "");
                            this._flexH.Rows.Add();
                            this._flexH.Row = (this._flexH.Rows).Count - 1;
                            this._flexH["CD_PLANT"] = row6["CD_PLANT"];
                            this._flexH["NO_IV"] = "";
                            this._flexH["NO_TEMP"] = "";
                            this._flexH["NO_WORK"] = row6["NO_WORK"];
                            this._flexH["CD_PARTNER"] = row6["CD_PARTNER"];
                            this._flexH["NM_FG_TAX"] = row6["NM_FG_TAX"];
                            this._flexH["AM_CLS"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(row6["AM_WORK"]));
                            this._flexH["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(row6["AM_VAT_WORK"]));
                            this._flexH["AM_HAP"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(row6["AM_HAP_WORK"]));
                            this._flexH["YN_DOCU"] = "N";
                            this._flexH["NO_DOCU"] = "";
                            this._flexH["LN_PARTNER"] = row6["LN_PARTNER"];
                            this._flexH["CD_CC"] = D.GetString(row6["CD_CC"]);
                            this._flexH["NM_CC"] = row6["NM_CC"];
                            this._flexH["FG_TPPURCHASE"] = row6["FG_TPPURCHASE"];
                            this._flexH["NM_TP"] = row6["NM_TP"];
                            this._flexH["CD_EXCH"] = row6["CD_EXCH"];
                            this._flexH["NM_EXCH"] = row6["NM_EXCH"];
                            this._flexH["RT_EXCH"] = row6["RT_EXCH"];
                            this._flexH["FG_TAX"] = row6["FG_TAX"];
                            this._flexH["CD_BIZAREA_TAX"] = this.ctx부가세사업장.CodeValue;
                            this._flexH["NM_BIZAREA_TAX"] = this.ctx부가세사업장.CodeName;
                            this._flexH[this._flexH.Row, "FG_PAYBILL"] = row6["FG_PAYBILL"];
                            if (this.지급관리통제설정 == "Y")
                            {
                                decimal num3 = 0M + D.GetDecimal(row6["AM_EX"]);
                                string str8 = ComFunc.만기예정일자(this.dp처리일자.Text, num3, D.GetString(row6["FG_PAYBILL"]), "1");
                                if (str8 != string.Empty)
                                    this._flexH[this._flexH.Row, "DT_PAY_PREARRANGED"] = str8;
                                string str9 = ComFunc.만기예정일자(this.dp처리일자.Text, num3, D.GetString(row6["FG_PAYBILL"]), "2");
                                if (str9 != string.Empty)
                                    this._flexH[this._flexH.Row, "DT_DUE"] = str9;
                            }
                            else
                                this._flexH[this._flexH.Row, "DT_PAY_PREARRANGED"] = str7;
                            this._flexH.AddFinished();
                            this._flexH.Col = (this._flexH.Cols).Fixed;
                        }
                        else
                        {
                            int row8 = this._flexH.FindRow(row6["CD_PARTNER"].ToString(), (this._flexH.Rows).Fixed, ((RowCol)this._flexH.Cols["CD_PARTNER"]).Index, true);
                            this._flexH[row8, "AM_CLS"] = Unit.원화금액(DataDictionaryTypes.PR, this._flexH.CDecimal(this._flexH[row8, "AM_CLS"]) + this._flexH.CDecimal(row6["AM_WORK"]));
                            this._flexH[row8, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, this._flexH.CDecimal(this._flexH[row8, "AM_VAT"]) + this._flexH.CDecimal(row6["AM_VAT_WORK"]));
                            this._flexH[row8, "AM_HAP"] = Unit.원화금액(DataDictionaryTypes.PR, this._flexH.CDecimal(this._flexH[row8, "AM_HAP"]) + this._flexH.CDecimal(row6["AM_HAP_WORK"]));
                        }
                    }
                  this._flexH.Redraw = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexL.Redraw = true;
                this._flexH.Redraw = true;
            }
        }

        private void btn세금계산서_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow)
                    return;
                this.btn세금계산서.Enabled = false;
                object[] objArray = new object[] { D.GetString(this.cbo공장.SelectedValue),
                                                   this.dp처리일자.Text,
                                                   this.ctx담당자.CodeValue,
                                                   this.ctx담당자.CodeName,
                                                   "N" };
                if (this.MainFrameInterface.IsExistPage("P_PR_OPOUT_IV_MNG", false))
                    this.MainFrameInterface.UnLoadPage("P_PR_OPOUT_IV_MNG", false);
                this.MainFrameInterface.LoadPageFrom("P_PR_OPOUT_IV_MNG", this.DD("공정외주매입관리"), this.Grant, objArray);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_CHG_BIZAREA_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flexH.DataTable.Rows.Count < 1)
                    return;
                DataRow[] dataRowArray = this._flexH.DataTable.Select("CHK ='Y'");
                if (dataRowArray == null || dataRowArray.Length < 1)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    for (int index = 0; index < dataRowArray.Length; ++index)
                    {
                        dataRowArray[index]["CD_BIZAREA_TAX"] = this.ctx부가세사업장.CodeValue;
                        dataRowArray[index]["NM_BIZAREA_TAX"] = this.ctx부가세사업장.CodeName;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (D.GetString(this._flexH["YN_DOCU"]) == "Y")
                    e.Cancel = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!(sender is FlexGrid flexGrid))
                    return;
                flexGrid.DetailGrids[0].RowFilter = !this.rdo일괄.Checked ? "NO_WORK = '" + D.GetString(flexGrid["NO_WORK"]) + "' " : "CD_PARTNER = '" + D.GetString(flexGrid["CD_PARTNER"]) + "' ";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                switch (this._flexH.Cols[e.Col].Name)
                {
                    case "AM_VAT":
                        if (!string.IsNullOrEmpty(this._flexH.EditData) && this._flexH.GetData(this._flexH.Row, "AM_VAT").ToString() == this._flexH.EditData)
                            break;
                        decimal num1 = this._flexH.CDecimal(this._flexH.EditData);
                        if (num1 < 0M)
                        {
                            this.ShowMessage("부가세는 음수일 수 없습니다.");
                            e.Cancel = true;
                            break;
                        }
                        this._flexH[e.Row, "AM_HAP"] = Unit.원화금액(DataDictionaryTypes.PR, this._flexH.CDecimal(this._flexH[e.Row, "AM_CLS"]) + num1);
                        object obj = this._flexL.DataTable.Compute("SUM(AM_VAT)", this._flexL.RowFilter);
                        decimal result = 0M;
                        decimal.TryParse(obj.ToString(), out result);
                        decimal num3 = num1 - result;
                        decimal num4 = 0M;
                        bool flag = false;
                        for (int index = this._flexL.Rows.Count - 1; index >= this._flexL.Rows.Fixed; --index)
                        {
                            decimal num5 = this._flexL.CDecimal(this._flexL[index, "AM_VAT"]);
                            decimal num6 = this._flexL.CDecimal(this._flexL[index, "AM_CLS"]);
                            if (num5 > num4)
                                num4 = num5;
                            if (!(num6 == 0M) && (!(num3 < 0M) || !(num5 < Math.Abs(num3))))
                            {
                                this._flexL[index, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, num5 + num3);
                                this._flexL[index, "AM_HAP"] = Unit.원화금액(DataDictionaryTypes.PR, this._flexL.CDecimal(this._flexL[index, "AM_CLS"]) + this._flexL.CDecimal(this._flexL[index, "AM_VAT"]));
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            this.ShowMessage("변경할 금액이 0 이거나 라인의 최고 부가세보다 변경할 부가세 금액(절대값)이 더 큽니다. 변경된 부가세를 반영해줄 곳이 없습니다.");
                            e.Cancel = true;
                            break;
                        }
                        break;
                    case "FG_PAYBILL":
                        string str1 = string.Empty;
                        string str2 = string.Empty;
                        if (this.지급관리통제설정 == "Y")
                        {
                            decimal num8 = D.GetDecimal(this._flexH["AM_CLS"]);
                            str2 = ComFunc.만기예정일자(this.dp처리일자.Text, num8, D.GetString(this._flexH.EditData), "1");
                            if (str2 != string.Empty)
                            {
                                this._flexH[this._flexH.Row, "DT_PAY_PREARRANGED"] = str2;
                                this._flexH[this._flexH.Row, "DT_DUE"] = str2;
                            }
                            str1 = ComFunc.만기예정일자(this.dp처리일자.Text, num8, D.GetString(this._flexH.EditData), "2");
                            if (str1 != string.Empty)
                                this._flexH[this._flexH.Row, "DT_DUE"] = str1;
                        }
                        if (this.지급관리통제설정 == "N" || str2 == string.Empty || str1 == string.Empty)
                        {
                            this._flexH["DT_DUE"] = "";
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

        private bool Chk_PLANT => !Checker.IsEmpty(this.cbo공장, this.lbl공장.Text);

        private bool Chk_EMP => !Checker.IsEmpty(this.ctx담당자, this.lbl담당자.Text);

        private bool Chk_BUSI => !Checker.IsEmpty(this.cbo거래구분, this.lbl거래구분.Text);

        private bool Chk_DT => !Checker.IsEmpty(this.dp처리일자, this.lbl처리일자.Text);

    }
}

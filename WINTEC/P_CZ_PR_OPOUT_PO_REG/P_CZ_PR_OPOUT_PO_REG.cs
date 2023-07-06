using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.MF.EMail;
using Duzon.ERPU.MF.SMS;
using Duzon.Windows.Print;
using DzHelpFormLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_PR_OPOUT_PO_REG : PageBase
    {
        private P_CZ_PR_OPOUT_PO_REG_BIZ _biz = new P_CZ_PR_OPOUT_PO_REG_BIZ();
        private FreeBinding _header;
        private string sChcoef_YN = string.Empty;
        private string _NO_PO = string.Empty;
        private string _CD_PLANT = string.Empty;

        public P_CZ_PR_OPOUT_PO_REG()
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

        public P_CZ_PR_OPOUT_PO_REG(string CD_PLANT, string NO_PO)
          : this()
        {
            this._CD_PLANT = CD_PLANT;
            this._NO_PO = NO_PO;
        }

        protected override void InitLoad()
        {
            this.InitControl();
            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            DataSet comboData = Global.MainFrame.GetComboData("NC;MA_PLANT", "N;MA_CODEDTL_005;MA_B000046", "N;MA_B000005", "N;SU_0000008", "N;TR_IM00002", "S;MA_B000004");
            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";

            if (comboData.Tables[0].Select("CODE = '" + this.LoginInfo.CdPlant + "'").Length == 1)
                this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;
            else if (this.cbo공장.Items.Count > 0)
                this.cbo공장.SelectedIndex = 0;

            this.cbo과세구분.DataSource = comboData.Tables[1];
            this.cbo과세구분.DisplayMember = "NAME";
            this.cbo과세구분.ValueMember = "CODE";
            this.cbo과세구분.SelectedIndex = 0;

            this.cbo환율.DataSource = comboData.Tables[2];
            this.cbo환율.DisplayMember = "NAME";
            this.cbo환율.ValueMember = "CODE";
            this.cbo환율.SelectedIndex = 0;

            this.cbo지급조건.DataSource = comboData.Tables[3];
            this.cbo지급조건.DisplayMember = "NAME";
            this.cbo지급조건.ValueMember = "CODE";
            this.cbo지급조건.SelectedIndex = 0;

            this.cbo가격조건.DataSource = comboData.Tables[4];
            this.cbo가격조건.DisplayMember = "NAME";
            this.cbo가격조건.ValueMember = "CODE";
            this.cbo가격조건.SelectedIndex = 0;

            if (this.sChcoef_YN == "001")
                this._flex.SetDataMap("UNIT_CH", comboData.Tables[5].Copy(), "CODE", "NAME");

            if (MA.ServerKey(false, "GOPT"))
                this.tabPage2.Text = "특기사항";

            object[] objArray = new object[] { "XXXXXX", "XXXXXX", "XXXXXX", Global.SystemLanguage.MultiLanguageLpoint };
            DataSet dataSet1 = this._biz.Search(objArray);
            DataTable table = dataSet1.Tables[0];

            this._header.SetBinding(dataSet1.Tables[0], this.tabControl1);
            this._header.ClearAndNewRow();
            this._flex.Binding = dataSet1.Tables[1];

            this.Control_SelectionChangeCommitted(this.cbo환율, null);
            this.Control_SelectionChangeCommitted(this.cbo과세구분, null);

            this.oneGrid1.UseCustomLayout = true;
            this._bpPnl_dtPo.IsNecessaryCondition = true;
            this._bpPnl_nmPartner.IsNecessaryCondition = true;
            this._bpPnl_cdPlant.IsNecessaryCondition = true;
            this._bpPnl_emp.IsNecessaryCondition = true;
            this._bpPnl_tax.IsNecessaryCondition = true;
            this._bpPnl_exch.IsNecessaryCondition = true;
            this._bpPnl_dept.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();

            if (string.IsNullOrEmpty(this._NO_PO))
                return;

            objArray[0] = this.LoginInfo.CompanyCode;
            objArray[1] = this.cbo공장.SelectedValue.ToString();
            objArray[2] = this._NO_PO;

            DataSet dataSet2 = this._biz.Search(objArray);
            this._header.SetDataTable(dataSet2.Tables[0]);
            this._flex.Binding = dataSet2.Tables[1];
            this._header.SetControlEnabled(false);
            this.txt비고.Enabled = true;
            this.txt텍스트비고1.Enabled = true;
        }

        private void InitControl()
        {
            try
            {
                this.cbo환율.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
                this.cbo과세구분.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);

                if (ComFunc.전용코드("공정외주발주등록-메일전송기능") == "100")
                    this.btn메일전송.Visible = true;
                if (ComFunc.전용코드("공정외주발주등록-최근단가적용") == "100")
                    this.btn최근단가적용.Visible = true;
                if (ComFunc.전용코드("SMS전송 설정") == "001")
                    this.btnSMS전송.Visible = true;

                this.sChcoef_YN = ComFunc.전용코드("공정외주임가공단가 단위 변환 사용");

                if (!(this.sChcoef_YN == string.Empty))
                    return;

                this.sChcoef_YN = "000";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex, this._flexID };
            this._flex.DetailGrids = new FlexGrid[] { this._flexID };

            #region _flex
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("CD_PLANT", "공장코드", 100, false);
            this._flex.SetCol("NO_LINE", "순번", 50, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("CD_WC", "작업장", 50, false);
            this._flex.SetCol("NM_WC", "작업장명", 100, false);
            this._flex.SetCol("CD_WCOP", "공정", 50, false);
            this._flex.SetCol("NM_OP", "공정명", 100, false);
            this._flex.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flex.SetCol("CD_OP", "OP", 25, false);
            this._flex.SetCol("NM_TP_WO", "오더형태명", 100, false);
            this._flex.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flex.SetCol("NM_ITEM", "품목명", 140, false);
            this._flex.SetCol("STND_ITEM", "규격", 120, false);
            this._flex.SetCol("UNIT_IM", "단위", 40, false);
            this._flex.SetCol("DT_DUE", "납기일자", 70, 8, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("QT_PO", "발주수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("WEIGHT", "단위중량", 80, true, typeof(decimal));
            this._flex.Cols["WEIGHT"].Format = "#,###,###.####";
            this._flex.SetCol("UNIT_CH", "변환단위", 100, true);
            this._flex.SetCol("QT_CHCOEF", "변환계수", 100, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("OLD_QT_PO", "변환전수량", 100, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_RCV", "입고수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_CLS", "마감수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("UM_MATL", "도급자재비", 100, 17, true, typeof(decimal), FormatTpType.UNIT_COST);
            this._flex.SetCol("UM_SOUL", "임가공비", 100, 17, true, typeof(decimal), FormatTpType.UNIT_COST);
            this._flex.SetCol("UM_EX", "외화단가", 90, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("AM_EX", "외화금액", 90, 17, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("UM", "단가", 90, 17, false, typeof(decimal), FormatTpType.UNIT_COST);
            this._flex.SetCol("AM", "금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_SUM", "합계금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("DC_RMK", "적요", 120, 100);
            this._flex.SetCol("QT_PO_ORIGIN", "발주가능수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("NM_FG_SERNO", "LOT,S/N관리", false);
            this._flex.SetCol("NM_GRP_ITEM", "품목군", false);
            this._flex.SetCol("NO_WORK", "작업실적번호", 100, false);
            this._flex.SetCol("NO_PJT", "프로젝트번호", 100, false);
            this._flex.SetCol("NM_PJT", "프로젝트명", 100, false);
            this._flex.SetCol("NO_LOT", "LOT번호", 100, false);
            this._flex.SetCol("EN_ITEM", "품목명(영)", false);
            this._flex.SetCol("STND_DETAIL_ITEM", "세부규격", false);
            this._flex.SetCol("MAT_ITEM", "재질", false);
            this._flex.SetCol("NM_MAKER", "Maker", false);
            this._flex.SetCol("BARCODE", "BARCODE", false);
            this._flex.SetCol("NO_MODEL", "모델번호", false);
            this._flex.SetCol("NO_DESIGN", "도면번호", 100);
            this._flex.SetCol("NO_WO_LINE", "공정순번", 100, false);

            if (MA.ServerKey(false, "JIGLS"))
                this._flex.SetCol("TXT_USERDEF1_WO", "규격변경", 80, false);

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.None);
            this._flex.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex.Cols["CD_PLANT"].Visible = false;
            this._flex.Cols["QT_RCV"].Visible = false;
            this._flex.Cols["QT_CLS"].Visible = false;
            this._flex.Cols["QT_PO_ORIGIN"].Visible = false;

            if (this.sChcoef_YN == "001")
            {
                this._flex.Cols["UNIT_CH"].Visible = true;
                this._flex.Cols["QT_CHCOEF"].Visible = true;
                this._flex.Cols["OLD_QT_PO"].Visible = true;
            }
            else if (this.sChcoef_YN == "000")
            {
                this._flex.Cols["UNIT_CH"].Visible = false;
                this._flex.Cols["QT_CHCOEF"].Visible = false;
                this._flex.Cols["OLD_QT_PO"].Visible = false;
            }

            this._flex.SetExceptEditCol("CD_PLANT", "NO_LINE", "CD_WC", "NM_WC", "CD_WCOP", "NM_OP", "NO_WO", "CD_OP", "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_IM", "QT_RCV", "QT_CLS", "UM", "AM", "AM_VAT", "QT_PO_ORIGIN", "AM_SUM", "NO_WORK", "NO_LOT", "EN_ITEM", "STND_DETAIL_ITEM", "MAT_ITEM", "NM_MAKER", "BARCODE", "NO_MODEL");
            this._flex.SetExceptSumCol("NO_LINE", "UM_EX", "UM");

            this._flex.VerifyPrimaryKey = new string[] { "NO_WO", "CD_OP" };
            this._flex.VerifyNotNull = new string[] { "NO_WO", "CD_OP", "CD_WC", "CD_WCOP", "CD_ITEM", "DT_DUE" };

            this._flex.VerifyCompare(this._flex.Cols["QT_PO"], 0, OperatorEnum.Greater);
            this._flex.VerifyCompare(this._flex.Cols["QT_RCV"], 0, OperatorEnum.GreaterOrEqual);
            this._flex.VerifyCompare(this._flex.Cols["QT_CLS"], 0, OperatorEnum.GreaterOrEqual);
            this._flex.VerifyCompare(this._flex.Cols["UM_EX"], 0, OperatorEnum.GreaterOrEqual);
            this._flex.VerifyCompare(this._flex.Cols["AM_EX"], 0, OperatorEnum.GreaterOrEqual);
            this._flex.VerifyCompare(this._flex.Cols["UM"], 0, OperatorEnum.GreaterOrEqual);
            this._flex.VerifyCompare(this._flex.Cols["AM"], 0, OperatorEnum.GreaterOrEqual);
            this._flex.VerifyCompare(this._flex.Cols["AM_VAT"], 0, OperatorEnum.GreaterOrEqual);
            this._flex.VerifyCompare(this._flex.Cols["AM_SUM"], 0, OperatorEnum.GreaterOrEqual);

            if (this.sChcoef_YN == "000")
                this._flex.VerifyCompare(this._flex.Cols["QT_PO"], this._flex.Cols["QT_PO_ORIGIN"], OperatorEnum.LessOrEqual);
            else
                this._flex.VerifyCompare(this._flex.Cols["OLD_QT_PO"], this._flex.Cols["QT_PO_ORIGIN"], OperatorEnum.LessOrEqual);

            this._flex.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            #endregion

            #region _flexID
            this._flexID.BeginSetting(1, 1, false);

            this._flexID.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flexID.SetCol("SEQ_WO", "순번", 100);
            this._flexID.SetCol("DT_NO_ID", "투입년월", 100, false, typeof(string), FormatTpType.YEAR_MONTH);
            this._flexID.SetCol("NO_SEQ", "일련번호", 100);
            this._flexID.SetCol("NO_ID", "ID번호", 100);
            this._flexID.SetCol("NO_ID_OLD", "ID번호(이전)", 100, true);
            this._flexID.SetCol("NO_HEAT", "소재HEAT번호", 100, true);
            this._flexID.SetCol("NO_OPOUT_PR", "공정외주요청번호", 120);
            this._flexID.SetCol("NO_OPOUT_WORK", "공정외주실적번호", 120);

            this._flexID.SettingVersion = "0.0.0.1";
            this._flexID.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this._flex.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._flexID.ValidateEdit += new ValidateEditEventHandler(this._flexID_ValidateEdit);
            this.ctx외주처.CodeChanged += new EventHandler(this.OnBpControl_CodeChanged);
            this.ctx외주처.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx담당자.CodeChanged += new EventHandler(this.OnBpControl_CodeChanged);
            this.ctx담당자.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.btn메일전송.Click += new EventHandler(this.MailButton_Click);
            this.btnSMS전송.Click += new EventHandler(this.btn_smssend_Click);
            this.btn외주요청적용.Click += new EventHandler(this.btn작업실적적용_Click);
            this.btn최근단가적용.Click += new EventHandler(this.btn_apply_um_Click);
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
                    if (!string.IsNullOrEmpty(this._flexID["NO_OPOUT_WORK"].ToString()))
                    {
                        this.ShowMessage("실적 등록된 건은 수정 할 수 없습니다.");
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

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string filter;
            try
            {
                if (!this._flex.HasNormalRow) return;

                dt = null;
                filter = string.Format("NO_WO = '{0}' AND NO_WO_LINE = '{1}' AND NO_OPOUT_PR = '{2}' AND NO_PR_LINE = '{3}'", this._flex["NO_WO"].ToString(),
                                                                                                                              this._flex["NO_WO_LINE"].ToString(),
                                                                                                                              this._flex["NO_PR"].ToString(),
                                                                                                                              this._flex["PR_LINE"].ToString());

                if (this._flex.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               this._flex["NO_WO"].ToString(),
                                                               this._flex["NO_WO_LINE"].ToString(),
                                                               this.txt발주번호.Text.ToString(),
                                                               this._flex["NO_PR"].ToString(),
                                                               this._flex["PR_LINE"].ToString()});
                }
                this._flexID.BindingAdd(dt, filter);
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

                this.cbo과세구분.SelectedIndex = 0;
                this.cbo환율.SelectedIndex = 0;

                this._header.CurrentRow["CD_PLANT"] = this.cbo공장.Items.Count <= 0 ? "" : D.GetString(this.cbo공장.SelectedValue);
                this._header.CurrentRow["FG_TAX"] = D.GetString(this.cbo과세구분.SelectedValue);
                this._header.CurrentRow["CD_EXCH"] = D.GetString(this.cbo환율.SelectedValue);
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
            else if (Gubun == "CD_PARTNER")
            {
                HList.Add(this.cbo공장, this.lbl공장);
                HList.Add(this.dtp발주일자, this.lbl발주일자);
                HList.Add(this.ctx담당자, this.lbl담당자);
                HList.Add(this.cbo과세구분, this.lbl과세구분);
                HList.Add(this.txt부서, this.lbl부서);
                HList.Add(this.cbo환율, this.lbl환율);
            }
            else
            {
                HList.Add(this.cbo공장, this.lbl공장);
                HList.Add(this.dtp발주일자, this.lbl발주일자);
                HList.Add(this.ctx외주처, this.lbl외주처);
                HList.Add(this.ctx담당자, this.lbl담당자);
                HList.Add(this.cbo과세구분, this.lbl과세구분);
                HList.Add(this.txt부서, this.lbl부서);
                HList.Add(this.cbo환율, this.lbl환율);
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
                if (!this.BeforeSearch())
                    return;

                P_CZ_PR_OPOUT_PO_SUB pPrOpoutPoSub = new P_CZ_PR_OPOUT_PO_SUB(new object[] { this.cbo공장.SelectedValue,
                                                                                             this.cbo공장.Text,
                                                                                             this.ctx외주처.CodeValue,
                                                                                             this.ctx외주처.CodeName });

                if (pPrOpoutPoSub.ShowDialog() == DialogResult.OK)
                {
                    SpInfo spInfo = new SpInfo();
                    DataSet dataSet = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                      this.cbo공장.SelectedValue.ToString(),
                                                                      pPrOpoutPoSub.No_Po,
                                                                      Global.SystemLanguage.MultiLanguageLpoint });

                    this._header.SetDataTable(dataSet.Tables[0]);
                    this._flex.Binding = dataSet.Tables[1];
                    this._header.SetControlEnabled(false);
                    this.txt비고.Enabled = true;
                    this.txt텍스트비고1.Enabled = true;

                    if (MA.ServerKey(false, "BKSEMS"))
                        this.ctx외주처.Enabled = true;
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
                if (!this.BeforeAdd())
                    return;

                this._flex.DataTable.Rows.Clear();
                this._flex.AcceptChanges();
                this._header.ClearAndNewRow();
                this._header.SetControlEnabled(true);

                this.InitPaint();
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

                this._biz.DeleteAll(this.cbo공장.SelectedValue.ToString(), this.txt발주번호.Text);
                this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                this.OnToolBarAddButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            string noPo;
            try
            {
                if (!this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
                    return;

                this.ShowMessage(PageResultMode.SaveGood);
                noPo = this.txt발주번호.Text;
                this.InitPaint();
                DataSet dataSet = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                      this.cbo공장.SelectedValue.ToString(),
                                                                      noPo,
                                                                      Global.SystemLanguage.MultiLanguageLpoint });

                this._header.SetDataTable(dataSet.Tables[0]);
                this._flex.Binding = dataSet.Tables[1];
                this._header.SetControlEnabled(false);
                this.txt비고.Enabled = true;
                this.txt텍스트비고1.Enabled = true;
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
                if (!this.BeforePrint() || !this.MsgAndSave(PageActionMode.Print) || !base.BeforeSearch())
                    return;

                DataTable dataTable1 = this._flex.DataTable;

                if (dataTable1 == null || dataTable1.Rows.Count < 1)
                    return;

                if (MA.ServerKey(false, "SOLIDTECH"))
                {
                    this.SetPrint_SOLIDTECH(true);
                }
                else
                {
                    DataTable table = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                      this.cbo공장.SelectedValue.ToString(),
                                                                      this.txt발주번호.Text,
                                                                      Global.SystemLanguage.MultiLanguageLpoint }).Tables[0];
                    string nmCEO = null;
                    string noTEL = null;
                    string noFAX = null;
                    string noEmail = null;

                    foreach (DataRow row in table.Rows)
                    {
                        nmCEO = D.GetString(row["NM_CEO"]);
                        noTEL = D.GetString(row["NO_TEL"]);
                        noFAX = D.GetString(row["NO_FAX"]);
                        noEmail = D.GetString(row["NO_EMAIL"]);
                    }

                    DataTable dataTable2 = this._biz.Print(this.ctx외주처.CodeValue);
                    ReportHelper reportHelper = new ReportHelper("R_PR_OPOUT_PO_REG_0", this.DD("공정외주발주서"));
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();

                    foreach (DataRow row in dataTable2.Rows)
                    {
                        dictionary["CD_PARTNER"] = D.GetString(row["CD_PARTNER"]);
                        dictionary["LN_PARTNER"] = D.GetString(row["LN_PARTNER"]);
                        dictionary["NO_FAX"] = D.GetString(row["NO_FAX"]);
                        dictionary["CD_EMP_PARTNER"] = D.GetString(row["CD_EMP_PARTNER"]);
                        dictionary["NO_COMPANY"] = D.GetString(row["NO_COMPANY"]);
                        dictionary["TP_JOB"] = D.GetString(row["TP_JOB"]);
                        dictionary["CLS_JOB"] = D.GetString(row["CLS_JOB"]);
                        dictionary["DC_ADS1_H"] = D.GetString(row["DC_ADS1_H"]);
                        dictionary["DC_ADS1_D"] = D.GetString(row["DC_ADS1_D"]);
                        dictionary["NO_TEL1"] = D.GetString(row["NO_TEL1"]);
                        dictionary["NO_FAX1"] = D.GetString(row["NO_FAX1"]);
                        dictionary["NO_FAX2"] = D.GetString(row["NO_FAX2"]);
                    }

                    dictionary["NO_PO"] = D.GetString(this._header.CurrentRow["NO_PO"]);
                    dictionary["NM_KOR"] = D.GetString(this._header.CurrentRow["NM_KOR"]);
                    dictionary["NM_COND_PAYMENT"] = this.cbo지급조건.Text;
                    dictionary["DC_RMK"] = this.txt비고.Text;
                    dictionary["NO_TEL"] = D.GetString(noTEL);
                    dictionary["NO_FAX"] = D.GetString(noFAX);
                    dictionary["NO_EMAIL"] = D.GetString(noEmail);
                    dictionary["NM_CEO"] = D.GetString(nmCEO);
                    dictionary["AM"] = D.GetString(this._flex.DataTable.Compute("SUM(AM)", ""));
                    dictionary["AM_VAT"] = D.GetString(this._flex.DataTable.Compute("SUM(AM_VAT)", ""));
                    dictionary["AM_PUR"] = D.GetString((D.GetDecimal(this._flex.DataTable.Compute("SUM(AM)", "")) + D.GetDecimal(this._flex.DataTable.Compute("SUM(AM_VAT)", ""))));
                    dictionary["DT_PO"] = this.dtp발주일자.Text;
                    dictionary["DC_RMK_TEXT1"] = this.txt텍스트비고1.Text;

                    foreach (string key in dictionary.Keys)
                        reportHelper.SetData(key, dictionary[key]);

                    reportHelper.세로출력();
                    reportHelper.SetDataTable(dataTable1, 1);
                    reportHelper.Print();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool Verify()
        {
            //if (!base.Verify())
            //    return false;

            if (this._header.JobMode == JobModeEnum.조회후수정 && !this._flex.HasNormalRow)
            {
                if (this.ShowMessage(공통메세지.자료를삭제하시겠습니까) == DialogResult.Yes)
                {
                    this._biz.DeleteAll(this.cbo공장.SelectedValue.ToString(), this.txt발주번호.Text);
                    this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    this.OnToolBarAddButtonClicked(null, null);
                    return false;
                }
            }
            else if (!this._flex.HasNormalRow)
            {
                this.OnToolBarSearchButtonClicked(null, null);
                return false;
            }
            foreach (DataRow dr in this._flex.DataTable.Rows)
            {
                DataRow[] dataRowArray = this._flexID.DataTable.Select("S = 'Y' AND NO_WO = '" + dr["NO_WO"].ToString() + "' AND NO_WO_LINE = '" + dr["NO_WO_LINE"].ToString() + "' AND NO_OPOUT_PR = '" + dr["NO_PR"].ToString() + "'");
                if (dataRowArray.Length != Convert.ToInt32(dr["QT_PO"]))
                {
                    this.ShowMessage("선택한 ID번호 수량과 발주수량이 같지 않은 건이 있습니다.");
                    return false;
                }
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
                seq = (string)this.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "PR", "30", this.dtp발주일자.Text.Substring(0, 6));
                this._header.CurrentRow["NO_PO"] = seq;
                this.txt발주번호.Text = seq;
            }

            if (this._flex.HasNormalRow)
            {
                this._header.CurrentRow["AM_EX"] = this._flex.DataTable.Compute("SUM(AM_EX)", "");
                this._header.CurrentRow["AM"] = this._flex.DataTable.Compute("SUM(AM)", "");
                this._header.CurrentRow["AM_VAT"] = this._flex.DataTable.Compute("SUM(AM_VAT)", "");
            }

            DataTable changes1 = this._header.GetChanges();
            DataTable changes2 = this._flex.GetChanges();
            DataTable changes3 = this._flexID.GetChanges();

            if (changes1 == null && changes2 == null && changes3 == null)
                return true;
            if (!this._biz.Save(changes1, changes2, changes3, this._header.CurrentRow))
                return false;

            this._header.AcceptChanges();
            this._flex.AcceptChanges();
            this._flexID.AcceptChanges();

            return true;
        }

        private void btn작업실적적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txt발주번호.Text))
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
                }

                ArrayList ar = new ArrayList();

                for (int index = 0; index < this._flex.DataTable.Rows.Count; ++index)
                {
                    if (this._flex.DataTable.Rows[index].RowState != DataRowState.Deleted)
                    {
                        string str = this._flex.DataTable.Rows[index]["NO_WO"].ToString() + "&" + this._flex.DataTable.Rows[index]["CD_OP"].ToString();
                        ar.Add(str);
                    }
                }

                P_CZ_PR_OPOUT_PO_WORK_SUB prOpoutPoWorkSub = new P_CZ_PR_OPOUT_PO_WORK_SUB(this.ctx외주처.CodeValue,
                                                                                           this.ctx외주처.CodeName,
                                                                                           this.cbo공장.SelectedValue.ToString(),
                                                                                           this.cbo환율.SelectedValue.ToString(),
                                                                                           this.cur환율.DecimalValue,
                                                                                           ar,
                                                                                           this.dtp발주일자.Text,
                                                                                           this.cur과세구분.DecimalValue);

                if (prOpoutPoWorkSub.ShowDialog() == DialogResult.OK)
                {
                    DataRow[] returnDataRowArray = prOpoutPoWorkSub.GetReturnDataRowArray;

                    if (returnDataRowArray == null || returnDataRowArray.Length == 0)
                        return;

                    this.ctx외주처.CodeValue = returnDataRowArray[0]["CD_PARTNER"].ToString();
                    this.ctx외주처.CodeName = returnDataRowArray[0]["LN_PARTNER"].ToString();
                    this._header.CurrentRow["CD_PARTNER"] = returnDataRowArray[0]["CD_PARTNER"].ToString();
                    this._header.CurrentRow["LN_PARTNER"] = returnDataRowArray[0]["LN_PARTNER"].ToString();
                    //this._flex.AcceptChanges();
                    this._header.SetControlEnabled(false);
                    this.FillPoL(returnDataRowArray);
                    this.txt비고.Enabled = true;
                    this.txt텍스트비고1.Enabled = true;

                    int num;

                    if (!(this.ctx외주처.CodeValue == string.Empty))
                        num = !MA.ServerKey(false, "BKSEMS") ? 1 : 0;
                    else
                        num = 0;

                    if (num == 0)
                        this.ctx외주처.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void FillPoL(DataRow[] pdr_Line)
        {
            try
            {
                if (pdr_Line == null || pdr_Line.Length == 0)
                    return;

                decimal maxValue = this._flex.GetMaxValue("NO_LINE");
                this._flex.Redraw = false;

                foreach (DataRow dataRow in pdr_Line)
                {
                    this._flex.Row = this._flex.Rows.Count - 1;
                    this._flex.Rows.Add();


                    this._flex["CD_PLANT"] = dataRow["CD_PLANT"];
                    this._flex["NO_LINE"] = ++maxValue;
                    this._flex["CD_WC"] = dataRow["CD_WC"];
                    this._flex["NM_WC"] = dataRow["NM_WC"];
                    this._flex["CD_WCOP"] = dataRow["CD_WCOP"];
                    this._flex["NM_OP"] = dataRow["NM_OP"];
                    this._flex["NO_WO"] = dataRow["NO_WO"];
                    this._flex["NO_WO_LINE"] = dataRow["NO_LINE"];
                    this._flex["CD_OP"] = dataRow["CD_OP"];
                    this._flex["CD_ITEM"] = dataRow["CD_ITEM"];
                    this._flex["NM_ITEM"] = dataRow["NM_ITEM"];
                    this._flex["STND_ITEM"] = dataRow["STND_ITEM"];
                    this._flex["UNIT_IM"] = dataRow["UNIT_IM"];
                    this._flex["DT_DUE"] = dataRow["DT_DUE"];
                    this._flex["QT_PO"] = dataRow["QT_APPLY"];
                    this._flex["QT_PO_ORIGIN"] = dataRow["QT_APPLY_YET"];
                    this._flex["QT_RCV"] = 0M;
                    this._flex["QT_CLS"] = 0M;
                    this._flex["UM_MATL"] = dataRow["UM_MATL"];
                    this._flex["UM_SOUL"] = dataRow["UM_SOUL"];
                    this._flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PR, D.GetDecimal(dataRow["UM_EX"]));
                    this._flex["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, D.GetDecimal(dataRow["AM_EX"]));
                    this._flex["UM"] = Unit.원화단가(DataDictionaryTypes.PR, D.GetDecimal(dataRow["UM"]));
                    this._flex["AM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(dataRow["AM"]));
                    this._flex["AM_VAT"] = dataRow["AM_VAT"];
                    this._flex["NM_TP_WO"] = dataRow["NM_TP_WO"];
                    this._flex["NM_FG_SERNO"] = dataRow["NM_FG_SERNO"];
                    this._flex["NM_GRP_ITEM"] = dataRow["NM_GRP_ITEM"];
                    this._flex["AM_SUM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(this._flex["AM"]) + D.GetDecimal(this._flex["AM_VAT"]));
                    this._flex["NO_DESIGN"] = dataRow["NO_DESIGN"];
                    this._flex["WEIGHT"] = 1;
                    this._flex["NO_PR"] = dataRow["NO_PR"];
                    this._flex["PR_LINE"] = dataRow["NO_PR_LINE"];

                    if (this.sChcoef_YN == "001")
                    {
                        this._flex["QT_PO"] = dataRow["QT_APPLY_CALC"];
                        this._flex["UNIT_CH"] = dataRow["UNIT_CH"];
                        this._flex["QT_CHCOEF"] = dataRow["QT_CHCOEF"];
                        this._flex["OLD_QT_PO"] = dataRow["QT_APPLY"];
                    }

                    if (dataRow.Table.Columns.Contains("DC_RMK"))
                        this._flex["DC_RMK"] = dataRow["DC_RMK"];
                    if (dataRow.Table.Columns.Contains("NO_PJT"))
                        this._flex["NO_PJT"] = dataRow["NO_PJT"];
                    if (dataRow.Table.Columns.Contains("NM_PJT"))
                        this._flex["NM_PJT"] = dataRow["NM_PJT"];
                    if (dataRow.Table.Columns.Contains("NO_WORK"))
                        this._flex["NO_WORK"] = dataRow["NO_WORK"];
                    if (dataRow.Table.Columns.Contains("NO_LOT"))
                        this._flex["NO_LOT"] = dataRow["NO_LOT"];
                    if (dataRow.Table.Columns.Contains("EN_ITEM"))
                        this._flex["EN_ITEM"] = dataRow["EN_ITEM"];
                    if (dataRow.Table.Columns.Contains("STND_DETAIL_ITEM"))
                        this._flex["STND_DETAIL_ITEM"] = dataRow["STND_DETAIL_ITEM"];
                    if (dataRow.Table.Columns.Contains("MAT_ITEM"))
                        this._flex["MAT_ITEM"] = dataRow["MAT_ITEM"];
                    if (dataRow.Table.Columns.Contains("NM_MAKER"))
                        this._flex["NM_MAKER"] = dataRow["NM_MAKER"];
                    if (dataRow.Table.Columns.Contains("BARCODE"))
                        this._flex["BARCODE"] = dataRow["BARCODE"];
                    if (dataRow.Table.Columns.Contains("NO_MODEL"))
                        this._flex["NO_MODEL"] = dataRow["NO_MODEL"];
                    if (dataRow.Table.Columns.Contains("TXT_USERDEF1_WO"))
                        this._flex["TXT_USERDEF1_WO"] = dataRow["TXT_USERDEF1_WO"];
                    this._flex.AddFinished();
                }
                this._flex.Redraw = true;
                this._flex.Col = this._flex.Cols["CD_ITEM"].Index;
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                this._flex.Redraw = true;
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is BpCodeTextBox bpCodeTextBox) || (bpCodeTextBox.HelpID != HelpID.P_MA_EMP_SUB || !(bpCodeTextBox.CodeValue == "")))
                    return;

                this.txt부서.Text = "";
                this._header.CurrentRow["NM_DEPT"] = "";
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

                switch (e.HelpID)
                {
                    case HelpID.P_MA_EMP_SUB:
                        this.ctx담당자.CodeName = e.CodeName;
                        this.txt부서.Text = row["NM_DEPT"].ToString();
                        this._header.CurrentRow["NM_DEPT"] = this.txt부서.Text;
                        break;
                }
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
                if (!(sender is FlexGrid flexGrid))
                    return;

                D.GetDecimal(flexGrid.EditData);
                D.GetDecimal(flexGrid.GetData(e.Row, e.Col));
                Decimal decimalValue1 = this.cur과세구분.DecimalValue;
                Decimal decimalValue2 = this.cur환율.DecimalValue;

                switch (flexGrid.Cols[e.Col].Name)
                {
                    case "QT_PO":
                        flexGrid["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * D.GetDecimal(flexGrid["QT_PO"]) * D.GetDecimal(flexGrid["WEIGHT"]));
                        flexGrid["UM"] = Unit.원화단가(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * decimalValue2);
                        flexGrid["AM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM"]) * D.GetDecimal(flexGrid["QT_PO"]) * D.GetDecimal(flexGrid["WEIGHT"]));
                        flexGrid["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM"]) * decimalValue1 * 0.01M);
                        flexGrid["AM_SUM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM"]) + D.GetDecimal(flexGrid["AM_VAT"]));
                        break;
                    case "UM_EX":
                        flexGrid["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * D.GetDecimal(flexGrid["QT_PO"]) * D.GetDecimal(flexGrid["WEIGHT"]));
                        flexGrid["UM"] = Unit.원화단가(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * decimalValue2);
                        flexGrid["AM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM"]) * D.GetDecimal(flexGrid["QT_PO"]) * D.GetDecimal(flexGrid["WEIGHT"]));
                        flexGrid["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM"]) * decimalValue1 * 0.01M);
                        flexGrid["AM_SUM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM"]) + D.GetDecimal(flexGrid["AM_VAT"]));
                        break;
                    case "AM_EX":
                        if (D.GetDecimal(flexGrid["AM_EX"]) == 0M || D.GetDecimal(flexGrid["QT_PO"]) == 0M)
                            flexGrid["UM"] = 0;
                        flexGrid["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM_EX"]) / D.GetDecimal(flexGrid["QT_PO"]) / D.GetDecimal(flexGrid["WEIGHT"]));
                        flexGrid["UM"] = Unit.원화단가(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * decimalValue2);
                        flexGrid["AM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM_EX"]) * decimalValue2);
                        flexGrid["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM"]) * decimalValue1 * 0.01M);
                        flexGrid["AM_SUM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM"]) + D.GetDecimal(flexGrid["AM_VAT"]));
                        break;
                    case "UM_MATL":
                    case "UM_SOUL":
                        flexGrid["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_MATL"]) + D.GetDecimal(flexGrid["UM_SOUL"]));
                        flexGrid["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * D.GetDecimal(flexGrid["QT_PO"]) * D.GetDecimal(flexGrid["WEIGHT"]));
                        flexGrid["UM"] = Unit.원화단가(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_MATL"]) + D.GetDecimal(flexGrid["UM_SOUL"]) * decimalValue2);
                        flexGrid["AM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM"]) * D.GetDecimal(flexGrid["QT_PO"]) * D.GetDecimal(flexGrid["WEIGHT"]));
                        flexGrid["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM"]) * decimalValue1 * 0.01M);
                        flexGrid["AM_SUM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM"]) + D.GetDecimal(flexGrid["AM_VAT"]));
                        break;
                    case "QT_CHCOEF":
                        flexGrid["OLD_QT_PO"] = Unit.수량(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["QT_PO"]) / D.GetDecimal(flexGrid["QT_CHCOEF"]));
                        break;
                    case "WEIGHT":
                        flexGrid["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * D.GetDecimal(flexGrid["QT_PO"]) * D.GetDecimal(flexGrid["WEIGHT"]));
                        flexGrid["UM"] = Unit.원화단가(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM_EX"]) * decimalValue2);
                        flexGrid["AM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["UM"]) * D.GetDecimal(flexGrid["QT_PO"]) * D.GetDecimal(flexGrid["WEIGHT"]));
                        flexGrid["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM"]) * decimalValue1 * 0.01M);
                        flexGrid["AM_SUM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(flexGrid["AM"]) + D.GetDecimal(flexGrid["AM_VAT"]));
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                if (name == this.cbo환율.Name)
                {
                    if (this.cbo환율.SelectedValue.ToString() == "000")
                    {
                        this.cur환율.DecimalValue = 1M;
                        this._header.CurrentRow["RT_EXCH"] = 1;
                        this.cur환율.ReadOnly = true;
                        return;
                    }

                    this.cur환율.DecimalValue = this._biz.ExchangeSearch(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                           this.dtp발주일자.Text,
                                                                                           this.cbo환율.SelectedValue.ToString() });
                    this.cur환율.ReadOnly = false;
                }
                else if (name == this.cbo과세구분.Name)
                {
                    if (!(this.cbo과세구분.DataSource is DataTable dataSource))
                        return;

                    foreach (DataRow dataRow in dataSource.Select(" CODE = '" + this.cbo과세구분.SelectedValue.ToString() + "' "))
                    {
                        this.cur과세구분.DecimalValue = D.GetDecimal(dataRow["CD_FLAG1"]);
                        this._header.CurrentRow["VAT_RATE"] = this.cur과세구분.DecimalValue;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void MailButton_Click(object sender, EventArgs e)
        {
            this.SetPrint_SOLIDTECH(false);
        }

        private void btn_apply_um_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Check("SEARCH") || !this._flex.HasNormalRow)
                    return;

                foreach (DataRow row in this._flex.DataTable.Rows)
                {
                    DataSet dataSet = this._biz.Search_Um(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         D.GetString(this.cbo공장.SelectedValue),
                                                                         this.ctx외주처.CodeValue,
                                                                         this.cbo환율.SelectedValue.ToString(),
                                                                         D.GetString(row["CD_ITEM"]) });

                    if ((dataSet.Tables[0] != null || dataSet.Tables[1] != null || dataSet.Tables[2] != null) && (dataSet.Tables[0].Rows.Count >= 1 || dataSet.Tables[1].Rows.Count >= 1 || dataSet.Tables[2].Rows.Count >= 1))
                    {
                        for (int index = 0; index < 3; ++index)
                        {
                            if (dataSet.Tables[index] != null && dataSet.Tables[index].Rows.Count > 0)
                            {
                                row["UM_EX"] = D.GetDecimal(dataSet.Tables[index].Rows[0]["UM_EX"]);
                                break;
                            }
                        }

                        row["UM"] = Unit.원화단가(DataDictionaryTypes.PR, D.GetDecimal(row["UM_EX"]) * (this.cur환율.DecimalValue == 0M ? 1M : this.cur환율.DecimalValue));
                        row["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PR, D.GetDecimal(row["UM_EX"]) * D.GetDecimal(row["QT_PO"]));
                        row["AM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(row["AM_EX"]) * (this.cur환율.DecimalValue == 0M ? 1M : this.cur환율.DecimalValue));
                        row["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(row["AM"]) * (this.cur과세구분.DecimalValue / 100M));
                        row["AM_SUM"] = Unit.원화금액(DataDictionaryTypes.PR, D.GetDecimal(row["AM"]) + D.GetDecimal(row["AM_VAT"]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool CheckHeaderValue()
        {
            try
            {
                if (!(this.txt발주번호.Text != string.Empty))
                    return true;

                this.txt발주번호.Text = string.Empty;
                this._header.CurrentRow["NO_PO"] = string.Empty;
                this._header.AcceptChanges();
                this._header.CurrentRow.SetAdded();
                this._header.JobMode = JobModeEnum.추가후수정;

                return false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return true;
        }

        protected override bool IsChanged()
        {
            return base.IsChanged() || this.헤더변경여부;
        }

        private void SetPrint_SOLIDTECH(bool checkprint)
        {
            if (this.추가모드여부)
                return;

            ReportHelper reportHelper = new ReportHelper("R_PR_OPOUT_PO_REG_0", "공정외주발주서");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["NM_KOR"] = D.GetString(this._header.CurrentRow["NM_KOR"]);
            reportHelper.세로출력();

            if (D.GetString(this._header.CurrentRow["NO_PO"]) == string.Empty)
                return;

            DataTable dt = this._biz.Print_SOLIDTECH(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._header.CurrentRow["NO_PO"]));

            if (dt == null || dt.Rows.Count < 1)
                return;

            reportHelper.SetDataTable(dt, 1);
            if (checkprint)
            {
                reportHelper.Print();
            }
            else
            {
                new P_MF_EMAIL(new string[] { this.ctx외주처.CodeValue }, "R_PR_OPOUT_PO_REG_0", new ReportHelper[] { reportHelper }, dic, "공정외주발주서").ShowDialog();
            }
        }

        private bool 헤더변경여부
        {
            get
            {
                bool flag = this._header.GetChanges() != null;

                if (flag && this._header.JobMode == JobModeEnum.추가후수정 && !this._flex.HasNormalRow)
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

        private void btn_smssend_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                {
                    this.ShowMessage("전송할 데이터가 없습니다.");
                }
                else
                {
                    DataTable dt_data = this._flex.DataTable.Clone();

                    foreach (DataRow dataRow in this._flex.DataTable.Select())
                        dt_data.LoadDataRow(dataRow.ItemArray, true);

                    new P_MF_SMS(dt_data, "NO_WO", this.LoginInfo.EmployeeNo).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}
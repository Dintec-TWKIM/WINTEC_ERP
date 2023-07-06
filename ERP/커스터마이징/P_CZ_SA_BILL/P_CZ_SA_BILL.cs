using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.OLD;
using DzHelpFormLib;
using sale;

namespace cz
{
    public partial class P_CZ_SA_BILL : PageBase
    {
        #region 생성자 & 전역변수
        private P_CZ_SA_BILL_BIZ _biz;
        private FreeBinding _header;
        private string 전달된수금번호 = string.Empty;
        private CommonFunction _fuction = new CommonFunction();

        private bool 전표처리여부
        {
            get
            {
                return this._header.CurrentRow["TP_AIS"].Equals("Y") && this._header.CurrentRow["TP_BILLS"].Equals("N");
            }
        }

        private bool 반제등록여부
        {
            get
            {
                return this._flexL.HasNormalRow;
            }
        }

        private bool 추가모드여부
        {
            get
            {
                return this._header.JobMode == JobModeEnum.추가후수정;
            }
        }

        private bool 선수금체크여부
        {
            get
            {
                return this._biz.선수금체크(this.txt수금번호.Text);
            }
        }

        private bool 수금번호여부
        {
            get
            {
                return !(this.txt수금번호.Text == "");
            }
        }

        private bool 수금내용등록여부
        {
            get
            {
                return this._flexH.HasNormalRow;
            }
        }

        private bool 헤더변경여부
        {
            get
            {
                bool flag = this._header.GetChanges() != null;

                if (flag && this._header.JobMode == JobModeEnum.추가후수정 && !this._flexH.HasNormalRow)
                    flag = false;
                
                return flag;
            }
        }

        private bool 선수금여부
        {
            get
            {
                if (D.GetDecimal(_flexH.DataTable.Compute("SUM(AM_RCP_A_EX)", string.Empty)) > 0)
                    return true;
                else
                    return false;
            }
        }

        public enum 메세지
        {
            이미처리된전표입니다,
            등록된수금내용이존재하지않습니다,
            선수금중에선수금정리된건이있어수정이불가합니다,
            반제가등록되어있어서수정할수없습니다,
            총수금액과반제액이동일합니다,
            전표처리가되지않은수금전표입니다,
        }

        public P_CZ_SA_BILL()
        {
            try
            {
                StartUp.Certify(this);
                this.InitializeComponent();

                this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
                this._header = new FreeBinding();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public P_CZ_SA_BILL(string[] ps_args)
        {
            try
            {
                StartUp.Certify(this);
                this.InitializeComponent();

                this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
                this._header = new FreeBinding();

                if (ps_args[0].Length <= 0) return;
                this.전달된수금번호 = ps_args[0];
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 초기화
        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_SA_BILL_BIZ(this.MainFrameInterface);
            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            #region Header
            this._flexH.BeginSetting(1, 1, true);

            this._flexH.SetCol("FG_RCP", "수금구분", 100, 0);
            this._flexH.SetCol("NO_MGMT", "관리번호", 80);
            this._flexH.SetCol("FG_JATA", "자/타수", false);
            this._flexH.SetCol("AM_RCP_EX", "정상수금", 115, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_RCP", "정상수금(원화)", 115, true, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("AM_RCP_A_EX", "선수금", 115, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_RCP_A", "선수금(원화)", 115, true, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("TP_AIS", "처리상태", 70, 20, false, typeof(string));
            this._flexH.SetCol("CD_BANK", "금융기관", false);
            this._flexH.SetCol("NM_BANK", "금융기관", 120, 40);
            this._flexH.SetCol("DC_BANK", "발행기관", 100, 40);
            this._flexH.SetCol("NM_ISSUE", "발행인", 100, 20);
            this._flexH.SetCol("DT_DUE", "만기/약정일", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DY_TURN", "회전일", 100, true, typeof(decimal));
            this._flexH.SetCol("RT_EXCH", "환율", 50, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);

            this._flexH.SetExceptEditCol("TP_AIS");
            this._flexH.SetExceptEditCol("RT_EXCH");

            this._flexH.SetCodeHelpCol("NM_BANK", HelpID.P_MA_BANK_SUB, ShowHelpEnum.Always, new string[] { "CD_BANK", "NM_BANK" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
            this._flexH.SetCodeHelpCol("NO_MGMT", HelpID.P_FI_DEPOSIT_SUB, ShowHelpEnum.Always, new string[] { "NO_MGMT", "CD_BANK", "NM_BANK" }, new string[] { "NO_DEPOSIT", "CD_BANK", "NM_BANK" });

            this._flexH.VerifyAutoDelete = new string[] { "FG_RCP" };
            this._flexH.VerifyNotNull = new string[] { "FG_RCP" };

            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("DT_IV", "발행일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("TP_SO", "매출형태", 80);
            this._flexL.SetCol("NO_TX", "계산서번호", 100);
            this._flexL.SetCol("AM_IV_EX", "대상액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("NM_EXCH", "통화명", 80);
            this._flexL.SetCol("RT_EXCH_IV", "기표환율", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexL.SetCol("AM_IV", "대상액(원화)", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_RCP_TX_EX", "반제액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_RCP_TX", "반제액(원화)", 100, true, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_PL_LOSS", "환차손(-)", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_PL_PROFIT", "환차익(+)", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_PL", "환차손익", 100, true, typeof(decimal), FormatTpType.MONEY);

            this._flexL.VerifyAutoDelete = new string[] { "DT_IV", "NO_TX" };
            this._flexL.VerifyNotNull = new string[] { "DT_IV", "NO_TX" };

            this._flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            //this._flexL.Cols["AM_PL"].Visible = false;
            #endregion
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            this._header.JobModeChanged += new FreeBindingEventHandler(this._header_JobModeChanged);
            this._header.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);

            this.dtp수금일.DateChanged += new EventHandler(this.dtp수금일_DateChanged);

            this.ctx계산서번호검색.QueryAfter += new BpQueryHandler(this.ctx계산서번호검색_QueryAfter);

            this.ctx수주번호검색.QueryBefore += new BpQueryHandler(this.ctx수주번호검색_QueryBefore);
            this.ctx수주번호검색.QueryAfter += new BpQueryHandler(this.ctx수주번호_QueryAfter);

            this.btn전표이동.Click += new EventHandler(this.btn전표이동_Click);
            this.btn회계전표처리.Click += new EventHandler(this.btn회계전표처리_Click);
            this.btn회계전표취소.Click += new EventHandler(this.btn회계전표취소_Click);

            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);

            this.btn추가반제.Click += new EventHandler(this.btn추가반제_Click);
            this.btn삭제반제.Click += new EventHandler(this.btn삭제반제_Click);
            this.btn자동반제.Click += new EventHandler(this.btn자동반제_Click);

            this.cbo거래구분.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cbo통화명.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);

            this._flexH.StartEdit += new RowColEventHandler(this.Grid_StartEdit);
            this._flexH.ValidateEdit += new ValidateEditEventHandler(this.Grid_ValidateEdit);
            this._flexH.AfterEdit += new RowColEventHandler(this.Grid_AfterEdit);
            this._flexH.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this.Grid_BeforeCodeHelp);
            this._flexH.AddRow += new EventHandler(this.btn추가_Click);

            this._flexL.StartEdit += new RowColEventHandler(this.Grid_StartEdit);
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this.Grid_ValidateEdit);
            this._flexL.AfterEdit += new RowColEventHandler(this.Grid_AfterEdit);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.oneGrid1.UseCustomLayout = true;
            this.oneGrid1.IsSearchControl = false;
            this.bpPanelControl1.IsNecessaryCondition = true;
            this.bpPanelControl2.IsNecessaryCondition = true;
            this.bpPanelControl3.IsNecessaryCondition = true;
            this.bpPanelControl4.IsNecessaryCondition = true;
            this.bpPanelControl5.IsNecessaryCondition = true;
            this.bpPanelControl6.IsNecessaryCondition = true;
            this.bpPanelControl7.IsNecessaryCondition = true;
            this.bpPanelControl8.IsNecessaryCondition = true;
            this.bpPanelControl10.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();

            this.InitControl();

            DataSet dataSet = this._biz.Search("");
            this._header.SetBinding(dataSet.Tables[0], this.oneGrid1);
            this._header.ClearAndNewRow();
            this.화폐단위셋팅();
            this.거래구분셋팅();
            this.cbo사업장.SelectedValue = this.LoginInfo.BizAreaCode;
            this._header.CurrentRow["CD_BIZAREA"] = this.LoginInfo.BizAreaCode;
            this._header.CurrentRow["CD_TP"] = (this.cbo수금형태.SelectedValue == null ? string.Empty : this.cbo수금형태.SelectedValue.ToString());
            this.cbo반환여부.SelectedValue = "N";
            this._header.CurrentRow["TP_BILLS"] = "N";
            this._flexH.Binding = dataSet.Tables[1];
            this._flexL.Binding = dataSet.Tables[2];

            if (this.전달된수금번호.Length > 0) this.OnToolBarSearchButtonClicked(null, null);
        }

        private void InitControl()
        {
            DataSet comboData = this.GetComboData("N;PU_C000016",
                                                  "N;SA_B000002",
                                                  "S;SA_B000012",
                                                  "N;MA_AISPOSTH;300",
                                                  "N;SA_B000028",
                                                  "N;MA_AISPOSTH;100",
                                                  "N;MA_B000005",
                                                  "S;MA_BIZAREA",
                                                  "N;SA_B000050");

            this.cbo수금형태.DataSource = comboData.Tables[3];
            this.cbo수금형태.ValueMember = "CODE";
            this.cbo수금형태.DisplayMember = "NAME";

            //this.cbo거래구분.DataSource = new DataView(comboData.Tables[0], "CODE IN ('001', '002')", "CODE", DataViewRowState.CurrentRows);
            this.cbo거래구분.DataSource = comboData.Tables[0];
            this.cbo거래구분.ValueMember = "CODE";
            this.cbo거래구분.DisplayMember = "NAME";

            this.cbo전표처리여부.DataSource = comboData.Tables[4].Copy();
            this.cbo전표처리여부.ValueMember = "CODE";
            this.cbo전표처리여부.DisplayMember = "NAME";
            this.cbo전표처리여부.SelectedValue = "N";

            this.cbo통화명.DataSource = comboData.Tables[6];
            this.cbo통화명.DisplayMember = "NAME";
            this.cbo통화명.ValueMember = "CODE";

            this.cbo사업장.DataSource = comboData.Tables[7];
            this.cbo사업장.DisplayMember = "NAME";
            this.cbo사업장.ValueMember = "CODE";

            this.cbo반환여부.DataSource = comboData.Tables[8];
            this.cbo반환여부.DisplayMember = "NAME";
            this.cbo반환여부.ValueMember = "CODE";

            this.dtp수금일.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp수금일.ToDayDate = this.MainFrameInterface.GetDateTimeToday();
            this.dtp수금일.Text = this.MainFrameInterface.GetStringToday;

            this.cur총수금원화.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT);
            this.cur반제액원화.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT);

            this._flexH.SetDataMap("FG_RCP", comboData.Tables[1], "CODE", "NAME");
            this._flexH.SetDataMap("FG_JATA", comboData.Tables[2], "CODE", "NAME");
            this._flexH.SetDataMap("TP_AIS", comboData.Tables[4].Copy(), "CODE", "NAME");

            this._flexL.SetDataMap("TP_SO", comboData.Tables[5], "CODE", "NAME");
        }
        #endregion

        #region 메인버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                this.cur선수금잔액.Text = "";
                this.cur채권잔액.Text = "";

                if (!this.BeforeSearch()) return;

                DataSet dataSet;
                if (this.전달된수금번호 == null || this.전달된수금번호.Length == 0)
                {
                    P_SA_BILL_SCH pSaBillSch = new P_SA_BILL_SCH(this.MainFrameInterface);

                    if (pSaBillSch.ShowDialog() != DialogResult.OK) return;

                    dataSet = this._biz.Search(pSaBillSch._SearchKey);
                }
                else
                    dataSet = this._biz.Search(this.전달된수금번호);

                this._header.SetDataTable(dataSet.Tables[0]);
                this._flexH.Binding = dataSet.Tables[1];
                this._flexL.Binding = dataSet.Tables[2];
                this.거래구분셋팅();
                this.cbo통화명.Enabled = false;
                this.cur환율.Enabled = false;
                this.dtp수금일.Enabled = false;
                this.cbo수금형태.Enabled = false;
                this.cbo거래구분.Enabled = false;
                this.ctx매출처.Enabled = false;
                this.ctx수금처.Enabled = false;
                this.ctx영업그룹.Enabled = false;
                this.ctx수금담당자.Enabled = false;
                this.cbo반환여부.Enabled = false;

                this.ctx계산서번호검색.ReadOnly = ReadOnly.TotalReadOnly;
                this.ctx수주번호검색.ReadOnly = ReadOnly.TotalReadOnly;

                if (this.전표처리여부)
                {
                    this.cbo사업장.Enabled = false;
                }
                else
                {
                    this.cbo사업장.Enabled = true;
                    this.txt비고.Enabled = true;
                }

                this.ControlEnabledDisable();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeAdd()
        {
            return base.BeforeAdd() && this.MsgAndSave(PageActionMode.Search);
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!this.BeforeAdd()) return;

                Debug.Assert(this._header.CurrentRow != null);
                Debug.Assert(this._flexH.DataTable != null);
                Debug.Assert(this._flexL.DataTable != null);

                this.cur선수금잔액.Text = string.Empty;
                this.cur채권잔액.Text = string.Empty;

                this.ctx계산서번호검색.ReadOnly = ReadOnly.None;
                this.ctx수주번호검색.ReadOnly = ReadOnly.None;
                this.ctx계산서번호검색.CodeValue = string.Empty;
                this.ctx계산서번호검색.CodeName = string.Empty;
                this.ctx수주번호검색.CodeValue = string.Empty;
                this.ctx수주번호검색.CodeName = string.Empty;
                
                this._flexH.DataTable.Rows.Clear();
                this._flexH.AcceptChanges();
                this._flexL.DataTable.Rows.Clear();
                this._flexL.AcceptChanges();
                this._header.ClearAndNewRow();
                this.dtp수금일.Enabled = true;
                this.cbo수금형태.Enabled = true;
                this.cbo거래구분.Enabled = true;
                this.ctx매출처.Enabled = true;
                this.ctx수금처.Enabled = true;
                this.ctx영업그룹.Enabled = true;
                this.ctx수금담당자.Enabled = true;
                this.cbo사업장.Enabled = true;
                this.cbo사업장.SelectedValue = this.LoginInfo.BizAreaCode;
                this._header.CurrentRow["CD_BIZAREA"] = this.LoginInfo.BizAreaCode;
                this.cbo수금형태.SelectedIndex = 0;
                this._header.CurrentRow["CD_TP"] = this.cbo수금형태.SelectedValue.ToString();
                this.cbo반환여부.SelectedValue = "N";
                this._header.CurrentRow["TP_BILLS"] = "N";
                this.화폐단위셋팅();
                this.거래구분셋팅();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete()) return false;

            if (this.전표처리여부)
            {
                this.ShowMessage(메세지.이미처리된전표입니다);
                return false;
            }

            if (!this.추가모드여부 && !this.선수금체크여부)
            {
                this.ShowMessage(메세지.선수금중에선수금정리된건이있어수정이불가합니다);
                return false;
            }

            return this.ShowMessage(공통메세지.자료를삭제하시겠습니까) == DialogResult.Yes;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete()) return;

                this._biz.Delete(this.txt수금번호.Text);

                this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);

                this._flexH.DataTable.Rows.Clear();
                this._flexH.AcceptChanges();
                this._flexL.DataTable.Rows.Clear();
                this._flexL.AcceptChanges();

                this.ToolBarDeleteButtonEnabled = false;
                this.OnToolBarAddButtonClicked(sender, e);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeSave()
        {
            this._flexH.Focus();

            if (this.선수금여부 && string.IsNullOrEmpty(this.ctx프로젝트.CodeValue))
            {
                this.ShowMessage("CZ_선수금일 경우 프로젝트를 지정 해야 합니다.");
                this.ctx프로젝트.Focus();
                return false;
            }

            if (this.전표처리여부 && this._header.GetChanges() != null && !this._flexH.IsDataChanged && !this._flexL.IsDataChanged)
            {
                this._biz.Save(string.Empty, string.Empty, this._header.GetChanges(), null, null);
                this._header.AcceptChanges();
                this.Page_DataChanged(null, null);
                return false;
            }

            if (this.cur총수금원화.DecimalValue > 0 && this.cur총수금원화.DecimalValue < this.cur반제액원화.DecimalValue)
            {
                this.ShowMessage("총수금액(원화) 보다 반제액(원화)이 커서 저장할 수 없습니다.");
                return false;
            }

            for (int @fixed = this._flexL.Rows.Fixed; @fixed < this._flexL.Rows.Count; ++@fixed)
            {
                if (D.GetDecimal(this._flexL[@fixed, "AM_IV_EX"]) == D.GetDecimal(this._flexL[@fixed, "AM_RCP_TX_EX"]) && D.GetDecimal(this._flexL[@fixed, "AM_IV"]) != D.GetDecimal(this._flexL[@fixed, "AM_RCP_TX"]) + D.GetDecimal(this._flexL[@fixed, "AM_PL"]))
                {
                    this.ShowMessage("대상액(원화)와 반제액(원화) + 환차손익이 일치해야 수정이 가능합니다.");
                    return false;
                }
            }

            if ((this.cbo반환여부.SelectedValue == null ? "N" : this.cbo반환여부.SelectedValue.ToString()) == "Y")
            {
                this.cbo전표처리여부.SelectedValue = "Y";
                this._header.CurrentRow["TP_BILLS"] = "Y";
                this._header.CurrentRow["TP_AIS"] = "Y";

                if (this.cur총수금.DecimalValue != 0 || this.cur반제액.DecimalValue != 0)
                {
                    this.ShowMessage("반품수금일 경우 총수금액과 반제액은 항상 0 이어야 저장이 가능합니다.");
                    return false;
                }
            }
            else
            {
                this.cbo전표처리여부.SelectedValue = "N";
                this._header.CurrentRow["TP_BILLS"] = "N";
                this._header.CurrentRow["TP_AIS"] = "N";
                Decimal num = 0;

                foreach (DataRow dataRow in (InternalDataCollectionBase)this._flexH.DataView.ToTable().Rows)
                    num += D.GetDecimal(dataRow["AM_RCP_A_EX"]);

                if (this.cur총수금.DecimalValue - num != this.cur반제액.DecimalValue && this.ShowMessage("적용할 반제액이 총수금액과 일치하지않습니다. 그래도 저장하시겠습니까?", "QY2") == DialogResult.No)
                    return false;
            }

            if (this.전표처리여부)
            {
                this.ShowMessage(메세지.이미처리된전표입니다);
                return false;
            }

            if (!this._flexH.HasNormalRow)
            {
                this.ShowMessage("라인없이 저장할 수 없습니다");
                return false;
            }

            if (!this.추가모드여부 && !this.선수금체크여부)
            {
                this.ShowMessage(메세지.선수금중에선수금정리된건이있어수정이불가합니다);
                return false;
            }

            if (!this.HeaderCheck())
                return false;

            foreach (DataRow dataRow in this._flexH.DataTable.Rows)
            {
                if (dataRow.RowState != DataRowState.Deleted && !this.Check())
                    return false;
            }

            this.수금합계();

            return true;
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.MsgAndSave(PageActionMode.Save)) return;

                this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;

            this.영업그룹설정();

            if (this.추가모드여부)
            {
                string str = (string)this.GetSeq(this.LoginInfo.CompanyCode, "SA", "06", this.dtp수금일.Text.Substring(0, 6).Trim());
                this._header.CurrentRow["NO_RCP"] = str;

                for (int @fixed = this._flexH.Rows.Fixed; @fixed < this._flexH.Rows.Count; ++@fixed)
                    this._flexH[@fixed, "NO_RCP"] = str;

                for (int @fixed = this._flexL.Rows.Fixed; @fixed < this._flexL.Rows.Count; ++@fixed)
                    this._flexL[@fixed, "NO_RCP"] = str;

                this.txt수금번호.Text = str;
            }

            this.수금합계();

            DataTable changes1 = this._header.GetChanges();
            DataTable changes2 = this._flexH.GetChanges();
            DataTable changes3 = this._flexL.GetChanges();

            if (changes1 == null && changes2 == null && changes3 == null)
                return true;

            if (!this._biz.Save(this.dtp수금일.Text, this.ctx수금처.CodeValue, changes1, changes2, changes3))
                return false;

            for (int @fixed = this._flexH.Rows.Fixed; @fixed < this._flexH.Rows.Count; ++@fixed)
                this._flexH[@fixed, "AM_RCP_ORG"] = this._flexH[@fixed, "AM_RCP"];

            this._header.AcceptChanges();
            this._flexH.AcceptChanges();
            this._flexL.AcceptChanges();
            this.ControlEnabledDisable();

            return true;
        }
        #endregion

        #region 그리드 이벤트
        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarSaveButtonEnabled = this.IsChanged();

                if (this.전표처리여부)
                {
                    this.ToolBarDeleteButtonEnabled = false;
                }
                else
                {
                    this.ToolBarDeleteButtonEnabled = true;
                    if (this.IsChanged())
                        this.ToolBarSaveButtonEnabled = true;
                }

                this.ControlEnabledDisable();
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
                if (this.전표처리여부)
                {
                    this._header.SetControlEnabled(false);
                    this.txt비고.Enabled = true;
                }
                else
                    this._header.SetControlEnabled(true);

                if (e.JobMode == JobModeEnum.추가후수정)
                {
                    this.cbo통화명.Enabled = true;

                    if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                        this.cur환율.Enabled = false;
                    else
                        this.cur환율.Enabled = true;

                    this.dtp수금일.Enabled = true;
                    this.cbo거래구분.Enabled = true;
                    this.ctx매출처.Enabled = true;
                    this.ctx수금처.Enabled = true;
                }
                else
                {
                    this.cbo통화명.Enabled = false;
                    this.cur환율.Enabled = false;
                    this.dtp수금일.Enabled = false;
                    this.cbo거래구분.Enabled = false;
                    this.ctx매출처.Enabled = false;
                    this.ctx수금처.Enabled = false;
                }

                this.ControlEnabledDisable();
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
                if(((Control)sender).Name == "ctx매출처")
                {
                    this.ctx수금처.CodeValue = this.ctx매출처.CodeValue;
                    this.ctx수금처.CodeName = this.ctx매출처.CodeName;
                    this._header.CurrentRow["BILL_PARTNER"] = this.ctx매출처.CodeValue;
                    this._header.CurrentRow["LN_BILL_PARTNER"] = this.ctx매출처.CodeName;
                    this.CalcAmremain();

                    string maexc1 = BASIC.GetMAEXC("거래처선택-담당자적용");
                    string maexc2 = BASIC.GetMAEXC("거래처선택-영업그룹적용");

                    if (!(maexc1 == "N") || !(maexc2 == "N"))
                    {
                        string codevalue1 = string.Empty;
                        string codename1 = string.Empty;
                        string codevalue2 = string.Empty;
                        string codename2 = string.Empty;

                        if (this.ctx매출처.CodeValue != string.Empty)
                        {
                            DataRow partner = BASIC.GetPartner(this.ctx매출처.CodeValue);
                            codevalue1 = D.GetString(partner["CD_EMP_SALE"]);
                            codename1 = D.GetString(partner["NM_EMP"]);
                            codevalue2 = D.GetString(partner["CD_SALEGRP"]);
                            codename2 = D.GetString(partner["NM_SALEGRP"]);
                        }

                        if (maexc1 == "Y")
                        {
                            this.ctx수금담당자.SetCode(codevalue1, codename1);
                            this._header.CurrentRow["NO_EMP"] = codevalue1;
                            this._header.CurrentRow["NM_KOR"] = codename1;
                        }

                        if (maexc2 == "Y")
                        {
                            this.ctx영업그룹.SetCode(codevalue2, codename2);
                            this._header.CurrentRow["CD_SALEGRP"] = codevalue2;
                            this._header.CurrentRow["NM_SALEGRP"] = codename2;
                        }
                    }
                }

                if (this.cbo거래구분.SelectedValue == null)
                    this.cbo거래구분.SelectedIndex = 0;

                if (this.dtp수금일.Text != string.Empty && ((this.cbo수금형태.SelectedValue == null ? string.Empty : this.cbo수금형태.SelectedValue.ToString()) != string.Empty && (this.cbo거래구분.SelectedValue != null || this.cbo거래구분.SelectedValue.ToString() != string.Empty) && (this.ctx매출처.CodeValue != string.Empty && this.ctx수금처.CodeValue != string.Empty && (this.ctx영업그룹.CodeValue != string.Empty && this.ctx수금담당자.CodeValue != string.Empty))) && this.dtp수금일.IsValidated)
                {
                    this.btn추가.Enabled = true;
                    this.btn삭제.Enabled = true;
                    this.btn추가반제.Enabled = true;
                    this.btn삭제반제.Enabled = true;
                    this.btn자동반제.Enabled = true;
                }
                else
                {
                    this.btn추가.Enabled = false;
                    this.btn삭제.Enabled = false;
                    this.btn추가반제.Enabled = false;
                    this.btn삭제반제.Enabled = false;
                    this.btn자동반제.Enabled = false;
                }

                this.Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Grid_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                string name = ((C1FlexGridBase)sender).Cols[e.Col].Name;

                if (this.전표처리여부)
                {
                    this.ShowMessage(메세지.이미처리된전표입니다);
                    e.Cancel = true;
                }
                else if (this._flexH.RowState() != DataRowState.Added && (name == "FG_RCP" || name == "CD_USERDEF1"))
                {
                    this.ShowMessage("저장된 이후로는 수정이 불가합니다.");
                    e.Cancel = true;
                }
                else if (this.반제등록여부 && (name == "FG_RCP" || name == "AM_RCP_EX" || (name == "AM_RCP_A_EX" || name == "AM_RCP") || name == "AM_RCP_A"))
                {
                    this.ShowMessage("반제된 이후로는 수정이 불가합니다.");
                    e.Cancel = true;
                }
                else
                {
                    Decimal decimal1 = D.GetDecimal(this._flexL["AM_IV_EX"]);
                    Decimal decimal2 = D.GetDecimal(this._flexL["AM_RCP_TX_EX"]);
                    switch (name)
                    {
                        case "AM_RCP_TX":
                            if (decimal1 != decimal2)
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                        case "AM_PL":
                            if (decimal1 != decimal2)
                            {
                                e.Cancel = true;
                                break;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Grid_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (e.Parameter.HelpID != HelpID.P_FI_DEPOSIT_SUB || !(this._flexH[e.Row, "FG_RCP"].ToString() != "002"))
                    return;

                e.Cancel = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Grid_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string str = ((C1FlexGridBase)sender).GetData(e.Row, e.Col).ToString();
                string editData = ((FlexGrid)sender).EditData;

                if (str.ToUpper() == editData.ToUpper()) return;

                switch (((C1FlexGridBase)sender).Cols[e.Col].Name)
                {
                    case "AM_RCP_EX":
                        this._flexH["AM_RCP"] = this.원화계산(D.GetDecimal(editData) * D.GetDecimal(this._flexH["RT_EXCH"]));
                        break;
                    case "AM_RCP_A_EX":
                        this._flexH["AM_RCP_A"] = this.원화계산(D.GetDecimal(editData) * D.GetDecimal(this._flexH["RT_EXCH"]));
                        break;
                    case "AM_RCP":
                        if (Convert.ToDecimal(this._flexH["AM_RCP_EX"]) == 0)
                        {
                            this._flexH["AM_RCP_EX"] = this.외화계산(D.GetDecimal(editData) / D.GetDecimal(this._flexH["RT_EXCH"]));
                            break;
                        }

                        if (Math.Abs(Math.Abs(Convert.ToDecimal(this._flexH["AM_RCP"])) - Math.Abs(Convert.ToDecimal(this._flexH["AM_RCP_EX"]) * Convert.ToDecimal(this._flexH["RT_EXCH"]))) > 100)
                        {
                            this.ShowMessage("원화 조정금액은 100원이내입니다.");

                            if (this._flexH.Editor != null)
                                this._flexH.Editor.Text = str;

                            this._flexH["AM_RCP"] = str;
                            break;
                        }
                        break;
                    case "AM_RCP_A":
                        if (Convert.ToDecimal(this._flexH["AM_RCP_A_EX"]) == 0)
                        {
                            this._flexH["AM_RCP_A_EX"] = this.외화계산(D.GetDecimal(editData) / D.GetDecimal(this._flexH["RT_EXCH"]));
                            break;
                        }
                        if (Math.Abs(Math.Abs(Convert.ToDecimal(this._flexH["AM_RCP_A"])) - Math.Abs(Convert.ToDecimal(this._flexH["AM_RCP_A_EX"]) * Convert.ToDecimal(this._flexH["RT_EXCH"]))) > 100)
                        {
                            this.ShowMessage("원화 조정금액은 100원이내입니다.");

                            if (this._flexH.Editor != null)
                                this._flexH.Editor.Text = str;

                            this._flexH["AM_RCP_A"] = str;
                            break;
                        }
                        break;
                    case "DT_DUE":
                        if (Global.MainFrame.ServerKey.Contains("SIMMONS"))
                            break;

                        if (this._fuction.GetStartEndDayText(this.dtp수금일.Text, this._flexH["DT_DUE"].ToString()) != "false")
                        {
                            this._flexH["DY_TURN"] = this._flexH.CDecimal(this._fuction.GetStartEndDayText(this.dtp수금일.Text, this._flexH["DT_DUE"].ToString()));
                            break;
                        }

                        this.ShowMessage("만기/약정일은 수금일보다 커야 회전일이 계산됩니다.");
                        this._flexH["DY_TURN"] = 0;
                        break;
                    case "AM_PL":
                        D.GetDecimal(this._flexL["AM_IV"]);
                        D.GetDecimal(this._flexL["AM_RCP_TX"]);
                        Decimal @decimal = D.GetDecimal(editData);

                        if (@decimal > 0)
                        {
                            this._flexL["AM_PL_LOSS"] = (D.GetDecimal(editData) * -1);
                            break;
                        }

                        if (@decimal < 0)
                        {
                            this._flexL["AM_PL_PROFIT"] = D.GetDecimal(editData);
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

        private void Grid_AfterEdit(object sender, RowColEventArgs e)
        {
            string name;

            try
            {
                name = ((FlexGrid)sender).Cols[e.Col].Name;

                if (name != "AM_RCP_EX" && name != "AM_RCP_A_EX" && name != "AM_RCP" && name != "AM_RCP_A" && name != "AM_RCP_TX") return;
                
                this.수금합계();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 버튼 이벤트
        private void dtp수금일_DateChanged(object sender, EventArgs e)
        {
            try
            {
                if (this._header.CurrentRow == null) return;

                this._header.CurrentRow["DT_RCP"] = this.dtp수금일.Text;

                this.화폐단위셋팅();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ctx계산서번호검색_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                this.ctx프로젝트.CodeValue = string.Empty;
                this.ctx프로젝트.CodeName = string.Empty;
                this._header.CurrentRow["NO_PROJECT"] = this.ctx프로젝트.CodeValue;
                this._header.CurrentRow["NM_PROJECT"] = this.ctx프로젝트.CodeName;

                this.ctx영업그룹.CodeValue = string.Empty;
                this.ctx영업그룹.CodeName = string.Empty;
                this._header.CurrentRow["CD_SALEGRP"] = this.ctx영업그룹.CodeValue;
                this._header.CurrentRow["NM_SALEGRP"] = this.ctx영업그룹.CodeName;

                this.ctx매출처.CodeName = D.GetString(e.HelpReturn.Rows[0]["LN_PARTNER"]);
                this.ctx매출처.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]);
                this._header.CurrentRow["CD_PARTNER"] = this.ctx매출처.CodeValue;
                this._header.CurrentRow["LN_CD_PARTNER"] = this.ctx매출처.CodeName;
                this._header_ControlValueChanged(this.ctx매출처, null);

                this.ctx수금처.CodeName = D.GetString(e.HelpReturn.Rows[0]["LN_BILL_PARTNER"]);
                this.ctx수금처.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_BILL_PARTNER"]);
                this._header.CurrentRow["BILL_PARTNER"] = this.ctx수금처.CodeValue;
                this._header.CurrentRow["LN_BILL_PARTNER"] = this.ctx수금처.CodeName;

                this.cbo거래구분.SelectedValue = D.GetString(e.HelpReturn.Rows[0]["FG_TRANS"]);
                this._header.CurrentRow["TP_BUSI"] = this.cbo거래구분.SelectedValue;
                this.Control_SelectionChangeCommitted(this.cbo거래구분, null);

                this.cbo통화명.SelectedValue = D.GetString(e.HelpReturn.Rows[0]["CD_EXCH"]);
                this._header.CurrentRow["CD_EXCH"] = this.cbo통화명.SelectedValue;
                this.Control_SelectionChangeCommitted(this.cbo통화명, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ctx수주번호검색_QueryBefore(object sender, BpQueryArgs e)
        {
            e.HelpParam.P11_ID_MENU = "H_SA_SO_SUB";
            e.HelpParam.P21_FG_MODULE = "N";
        }

        private void ctx수주번호_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                this.ctx프로젝트.CodeValue = D.GetString(e.HelpReturn.Rows[0]["NO_PROJECT"]);
                this.ctx프로젝트.CodeName = D.GetString(e.HelpReturn.Rows[0]["NM_PROJECT"]);
                this._header.CurrentRow["NO_PROJECT"] = this.ctx프로젝트.CodeValue;
                this._header.CurrentRow["NM_PROJECT"] = this.ctx프로젝트.CodeName;

                this.ctx영업그룹.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_SALEGRP"]);
                this.ctx영업그룹.CodeName = D.GetString(e.HelpReturn.Rows[0]["NM_SALEGRP"]);
                this._header.CurrentRow["CD_SALEGRP"] = this.ctx영업그룹.CodeValue;
                this._header.CurrentRow["NM_SALEGRP"] = this.ctx영업그룹.CodeName;
                
                this.ctx매출처.CodeName = D.GetString(e.HelpReturn.Rows[0]["LN_PARTNER"]);
                this.ctx매출처.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]);
                this._header.CurrentRow["CD_PARTNER"] = this.ctx매출처.CodeValue;
                this._header.CurrentRow["LN_CD_PARTNER"] = this.ctx매출처.CodeName;
                this._header_ControlValueChanged(this.ctx매출처, null);

                this.cbo거래구분.SelectedValue = D.GetString(e.HelpReturn.Rows[0]["TP_BUSI"]);
                this._header.CurrentRow["TP_BUSI"] = this.cbo거래구분.SelectedValue;
                this.Control_SelectionChangeCommitted(this.cbo거래구분, null);

                this.cbo통화명.SelectedValue = D.GetString(e.HelpReturn.Rows[0]["CD_EXCH"]);
                this._header.CurrentRow["CD_EXCH"] = this.cbo통화명.SelectedValue;
                this.Control_SelectionChangeCommitted(this.cbo통화명, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전표이동_Click(object sender, EventArgs e)
        {
            if (!(D.GetString(this._header.CurrentRow["NO_DOCU"]) != "")) return;

            this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(this._header.CurrentRow["NO_DOCU"]),
                                                                                                                             "1",
                                                                                                                             D.GetString(this._header.CurrentRow["CD_PC"]),
                                                                                                                             Global.MainFrame.LoginInfo.CompanyCode });
        }

        private void btn회계전표처리_Click(object sender, EventArgs e)
        {
            DataTable dt;
            string query;

            try
            {
                if (this.전표처리여부)
                {
                    this.ShowMessage(메세지.이미처리된전표입니다);
                }
                else
                {
                    if (!this.수금번호여부) return;

                    this._biz.미결전표처리(this.txt수금번호.Text, this.cbo수금형태.SelectedValue.ToString());

                    query = @"SELECT CD_COMPANY,
                                     NO_DOCU,
                                     CD_PC 
                              FROM FI_DOCU WITH(NOLOCK)
                              WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                             "AND NO_MDOCU = '" + this.txt수금번호.Text + "'" + Environment.NewLine +
                             "GROUP BY CD_COMPANY, NO_DOCU, CD_PC";

                    dt = Global.MainFrame.FillDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(D.GetString(dt.Rows[0]["NO_DOCU"])))
                        {
                            this.cbo전표처리여부.SelectedValue = "Y";
                            this._header.CurrentRow["TP_AIS"] = "Y";
                            this.txt전표번호.Text = D.GetString(dt.Rows[0]["NO_DOCU"]);
                            this._header.CurrentRow["NO_DOCU"] = D.GetString(dt.Rows[0]["NO_DOCU"]);
                            this._header.CurrentRow["CD_PC"] = D.GetString(dt.Rows[0]["CD_PC"]);
                            this._header.AcceptChanges();

                            for (int @fixed = this._flexH.Rows.Fixed; @fixed < this._flexH.Rows.Count; ++@fixed)
                                this._flexH[@fixed, "TP_AIS"] = "Y";

                            this._flexH.AcceptChanges();
                            this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn회계전표처리.Text });
                            this.ControlEnabledDisable();
                        }
                        else
                        {
                            this.ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);
                        }
                    }
                    else
                    {
                        this.ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);
                    }              
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn회계전표취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.cbo전표처리여부.SelectedValue.Equals("Y"))
                {
                    this.ShowMessage(메세지.전표처리가되지않은수금전표입니다);
                }
                else
                {
                    this._biz.미결전표취소(this.txt수금번호.Text);
                    this.cbo전표처리여부.SelectedValue = "N";
                    this._header.CurrentRow["TP_AIS"] = "N";
                    this.txt전표번호.Text = string.Empty;
                    this._header.CurrentRow["NO_DOCU"] = string.Empty;
                    this._header.CurrentRow["CD_PC"] = string.Empty;
                    this._header.AcceptChanges();

                    for (int @fixed = this._flexH.Rows.Fixed; @fixed < this._flexH.Rows.Count; ++@fixed)
                        this._flexH[@fixed, "TP_AIS"] = "N";

                    this._flexH.AcceptChanges();
                    this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn회계전표처리.Text });
                    this.ControlEnabledDisable();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.HeaderCheck()) return;

                if (this.cur환율.ToString().Trim() == "" || this.cur환율.DecimalValue == 0)
                {
                    this.ShowMessage("환율을 '0'이상 입력하십시요");
                }
                else if (this.dtp수금일.Text == "" || this.dtp수금일.Text == string.Empty)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl수금일.Text });
                }
                else if (this.전표처리여부)
                {
                    this.ShowMessage(메세지.이미처리된전표입니다);
                }
                else if (this.반제등록여부)
                {
                    this.ShowMessage(메세지.반제가등록되어있어서수정할수없습니다);
                }
                else
                {
                    if (this._flexH.Rows.Count >= 2)
                    {
                        if ((this._flexH["FG_RCP"].ToString().Trim() == "002" || this._flexH["FG_RCP"].ToString().Trim() == "017") && this._flexH["CD_BANK"].ToString().Trim() == "")
                        {
                            this.ShowMessage("금융기관을 입력하세요");
                            return;
                        }
                        if (this._flexH["FG_RCP"].ToString().Trim() == "003" && this._flexH["NM_ISSUE"].ToString().Trim() == "")
                        {
                            this.ShowMessage("발행인을 입력하세요");
                            return;
                        }
                        if ((this._flexH["FG_RCP"].ToString().Trim() == "003" || this._flexH["FG_RCP"].ToString().Trim() == "017") && this._flexH["DT_DUE"].ToString().Trim() == "")
                        {
                            this.ShowMessage("만기/약정일을 입력하세요");
                            return;
                        }
                        if ((this.cbo반환여부.SelectedValue == null ? "N" : this.cbo반환여부.SelectedValue.ToString()) == "Y")
                        {
                            this.ShowMessage("반품 수금일 경우에는 정상수금 및 선수금을 0 으로 입력하고 \n\n 반제되는 금액이 0 이 되도록 저장 합니다.");
                            return;
                        }
                    }

                    if (this.txt수금번호.Text != "" || this._flexL.HasNormalRow)
                    {
                        this.dtp수금일.Enabled = false;
                        this.ctx매출처.Enabled = false;
                        this.ctx수금처.Enabled = false;
                        this.ctx영업그룹.Enabled = false;
                        this.ctx수금담당자.Enabled = false;
                    }

                    this.ctx계산서번호검색.ReadOnly = ReadOnly.TotalReadOnly;
                    this.ctx수주번호검색.ReadOnly = ReadOnly.TotalReadOnly;
                    this.cbo통화명.Enabled = false;
                    this.cur환율.Enabled = false;
                    this.cbo수금형태.Enabled = false;
                    this.cbo거래구분.Enabled = false;
                    this.cbo반환여부.Enabled = false;
                    int num6 = 0;

                    for (int @fixed = this._flexH.Rows.Fixed; @fixed < this._flexH.Rows.Count; ++@fixed)
                    {
                        if (Convert.ToInt32(this._flexH[@fixed, "NO_LINE"]) > num6)
                            num6 = Convert.ToInt32(this._flexH[@fixed, "NO_LINE"]);
                    }

                    int num7 = num6 + 1;
                    this._flexH.Rows.Add();
                    this._flexH.Row = this._flexH.Rows.Count - 1;
                    this._flexH["NO_RCP"] = this.txt수금번호.Text;
                    this._flexH["NO_LINE"] = num7;
                    this._flexH["FG_RCP"] = "001";
                    this._flexH["TP_AIS"] = "N";
                    this._flexH["RT_EXCH"] = this.cur환율.DecimalValue;
                    this._flexH.AddFinished();
                    this._flexH.Col = this._flexH.Cols.Fixed;
                    this._flexH.Focus();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                if (this.전표처리여부)
                {
                    this.ShowMessage(메세지.이미처리된전표입니다);
                }
                else if (this.반제등록여부)
                {
                    this.ShowMessage(메세지.반제가등록되어있어서수정할수없습니다);
                }
                else
                {
                    this._flexH.Rows.Remove(this._flexH.Row);

                    this.수금합계();

                    if (!this._flexH.HasNormalRow && this.txt수금번호.Text == "")
                    {
                        this.화폐단위셋팅();
                        this.거래구분셋팅();
                        this.dtp수금일.Enabled = true;
                        this.cbo수금형태.Enabled = true;
                        this.cbo거래구분.Enabled = true;
                        this.ctx매출처.Enabled = true;
                        this.ctx수금처.Enabled = true;
                        this.ctx영업그룹.Enabled = true;
                        this.ctx수금담당자.Enabled = true;
                        this.cbo반환여부.Enabled = true;

                        this.ctx계산서번호검색.ReadOnly = ReadOnly.None;
                        this.ctx수주번호검색.ReadOnly = ReadOnly.None;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn추가반제_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.전표처리여부)
                {
                    this.ShowMessage(메세지.이미처리된전표입니다);
                }
                else if (!this.수금내용등록여부)
                {
                    this.ShowMessage(메세지.등록된수금내용이존재하지않습니다);
                }
                else if (this.cur총수금원화.DecimalValue == this.cur반제액원화.DecimalValue && (this.cbo반환여부.SelectedValue == null ? "N" : this.cbo반환여부.SelectedValue.ToString()) != "Y")
                {
                    this.ShowMessage("SA_M000129");
                }
                else
                {
                    if (!this.HeaderCheck()) return;

                    string MultiNoTax = string.Empty;

                    for (int @fixed = this._flexL.Rows.Fixed; @fixed < this._flexL.Rows.Count; ++@fixed)
                        MultiNoTax = MultiNoTax + this._flexL[@fixed, "NO_TX"].ToString() + "|";

                    P_CZ_SA_BILL_SUB pSaBillSub = new P_CZ_SA_BILL_SUB(MultiNoTax, this.dtp수금일.Text);

                    pSaBillSub.거래구분코드 = this.cbo거래구분.SelectedValue == null ? string.Empty : this.cbo거래구분.SelectedValue.ToString();
                    pSaBillSub.거래구분명 = this.cbo거래구분.Text;
                    pSaBillSub.매출처코드 = this.ctx매출처.CodeValue;
                    pSaBillSub.매출처명 = this.ctx매출처.CodeName;
                    pSaBillSub.수금처코드 = this.ctx수금처.CodeValue;
                    pSaBillSub.수금처명 = this.ctx수금처.CodeName;
                    DataRow currentRow = this._header.CurrentRow;
                    pSaBillSub.총수금원화 = this._flexH.CDecimal(currentRow["AM_RCP"]);
                    pSaBillSub.반제액원화 = this.cur반제액원화.DecimalValue;
                    pSaBillSub.총수금 = this._flexH.CDecimal(currentRow["AM_RCP_EX"]);
                    pSaBillSub.반제액 = this.cur반제액.DecimalValue;
                    pSaBillSub.통화코드 = this.cbo통화명.SelectedValue == null ? string.Empty : this.cbo통화명.SelectedValue.ToString();
                    pSaBillSub.통화명 = this.cbo통화명.Text;
                    pSaBillSub.환율 = this.cur환율.DecimalValue;
                    pSaBillSub.Set매출구분 = "0";

                    if (pSaBillSub.ShowDialog() != DialogResult.OK) return;

                    DataTable getDataTable = pSaBillSub.GetDataTable;
                    this._flexL.Redraw = false;

                    foreach (DataRow dataRow in getDataTable.Rows)
                    {
                        DataRow row = this._flexL.DataTable.NewRow();
                        row["NO_RCP"] = this.txt수금번호.Text;
                        row["NO_TX"] = dataRow["NO_IV"];
                        row["DT_IV"] = dataRow["DT_PROCESS"];
                        row["TP_SO"] = dataRow["TP_IV"];
                        row["AM_IV_EX"] = dataRow["AM_RCP_JAN_EX"];
                        row["NM_EXCH"] = dataRow["NM_EXCH"];
                        row["RT_EXCH_IV"] = dataRow["RT_EXCH"];
                        row["AM_IV"] = dataRow["AM_RCP_JAN"];
                        row["AM_RCP_TX_EX"] = dataRow["AM_RCP_EX"];
                        row["AM_RCP_TX"] = dataRow["AM_RCP"];
                        row["AM_PL"] = dataRow["AM_PL"];
                        row["AM_PL_LOSS"] = dataRow["AM_PL_LOSS"];
                        row["AM_PL_PROFIT"] = dataRow["AM_PL_PROFIT"];

                        this._flexL.DataTable.Rows.Add(row);
                    }

                    this._flexL.Redraw = true;
                    this._flexL.IsDataChanged = true;
                    this._flexL.SumRefresh();
                    this._flexL.Row = this._flexL.Rows.Count - 1;
                    this.ToolBarSaveButtonEnabled = true;
                    this.ControlEnabledDisable();

                    this.수금합계();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn삭제반제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow) return;

                if (this.전표처리여부)
                {
                    this.ShowMessage(메세지.이미처리된전표입니다);
                }
                else
                {
                    this._flexL.Rows.Remove(this._flexL.Row);
                    this.수금합계();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn자동반제_Click(object sender, EventArgs e)
        {
            if ((this.cbo반환여부.SelectedValue == null ? "N" : this.cbo반환여부.SelectedValue.ToString()) == "Y")
            {
                this.ShowMessage("반품 수금일 경우에는 자동반제가 불가합니다.");
            }
            else if (this.cbo통화명.SelectedValue.ToString() != "000")
            {
                this.ShowMessage("외화수금은 자동반제가 불가합니다.");
            }
            else
            {
                try
                {
                    if (this.전표처리여부)
                    {
                        this.ShowMessage(메세지.이미처리된전표입니다);
                    }
                    else if (!this.수금내용등록여부)
                    {
                        this.ShowMessage(메세지.등록된수금내용이존재하지않습니다);
                    }
                    else if (this.cur총수금원화.DecimalValue == this.cur반제액원화.DecimalValue)
                    {
                        this.ShowMessage(메세지.총수금액과반제액이동일합니다);
                    }
                    else
                    {
                        if (!this.HeaderCheck()) return;

                        string MultiNoTx = string.Empty;

                        for (int @fixed = this._flexL.Rows.Fixed; @fixed < this._flexL.Rows.Count; ++@fixed)
                            MultiNoTx = MultiNoTx + this._flexL[@fixed, "NO_TX"].ToString() + "|";

                        DataRow currentRow = this._header.CurrentRow;
                        DataTable dataTable = this._biz.자동반제(this.ctx매출처.CodeValue, this.ctx수금처.CodeValue, this.cbo거래구분.SelectedValue.ToString(), MultiNoTx, this._flexH.CDecimal(currentRow["AM_RCP"]), this.cur반제액원화.DecimalValue, "0");
                        this._flexL.Redraw = false;

                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            DataRow row = this._flexL.DataTable.NewRow();
                            row["NO_RCP"] = this.txt수금번호.Text;
                            row["NO_TX"] = dataRow["NO_TX"];
                            row["DT_IV"] = dataRow["DT_IV"];
                            row["TP_SO"] = (this.cbo거래구분.SelectedValue.ToString() == "001" ? "001" : "002");
                            row["AM_IV"] = dataRow["AM_IV"];
                            row["AM_RCP_TX"] = dataRow["AM_RCP_TX"];
                            row["AM_IV_EX"] = dataRow["AM_IV"];
                            row["RT_EXCH_IV"] = 1;
                            row["AM_RCP_TX_EX"] = dataRow["AM_RCP_TX"];
                            row["AM_PL"] = 0;
                            row["AM_PL_LOSS"] = 0;
                            row["AM_PL_PROFIT"] = 0;

                            this._flexL.DataTable.Rows.Add(row);
                        }

                        this._flexL.Redraw = true;
                        this._flexL.IsDataChanged = this.ToolBarSaveButtonEnabled = true;
                        this._flexL.SumRefresh();
                        this._flexL.Row = this._flexL.Rows.Count - 1;

                        this.수금합계();
                        this.ControlEnabledDisable();
                    }
                }
                catch (Exception ex)
                {
                    this.MsgEnd(ex);
                }
            }
        }

        private void Control_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "cbo통화명":
                        this.화폐단위셋팅();
                        break;
                    case "cbo거래구분":
                        this.거래구분셋팅();
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타
        protected override bool IsChanged()
        {
            if (base.IsChanged())
                return true;

            return this.헤더변경여부;
        }

        private DialogResult ShowMessage(메세지 msg, params object[] paras)
        {
            switch (msg)
            {
                case 메세지.이미처리된전표입니다:
                    return this.ShowMessage("SA_M000124");
                case 메세지.등록된수금내용이존재하지않습니다:
                    return this.ShowMessage("SA_M000128");
                case 메세지.선수금중에선수금정리된건이있어수정이불가합니다:
                    return this.ShowMessage("SA_M000122");
                case 메세지.반제가등록되어있어서수정할수없습니다:
                    return this.ShowMessage("SA_M000130");
                case 메세지.총수금액과반제액이동일합니다:
                    return this.ShowMessage("SA_M000129");
                case 메세지.전표처리가되지않은수금전표입니다:
                    return this.ShowMessage("SA_M000125");
                default:
                    return DialogResult.None;
            }
        }

        private void 수금합계()
        {
            decimal 정상수금, 정상수금원화, 선수금, 선수금원화, 반제액, 반제액원화;

            정상수금 = D.GetDecimal(this._flexH.DataTable.Compute("SUM(AM_RCP_EX)", string.Empty));
            정상수금원화 = D.GetDecimal(this._flexH.DataTable.Compute("SUM(AM_RCP)", string.Empty));
            선수금 = D.GetDecimal(this._flexH.DataTable.Compute("SUM(AM_RCP_A_EX)", string.Empty));
            선수금원화 = D.GetDecimal(this._flexH.DataTable.Compute("SUM(AM_RCP_A)", string.Empty));
            반제액 = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_RCP_TX_EX)", string.Empty));
            반제액원화 = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_RCP_TX)", string.Empty));

            DataRow currentRow = this._header.CurrentRow;
            currentRow["AM_RCP_EX"] = 정상수금;
            currentRow["AM_RCP"] = 정상수금원화;
            currentRow["AM_RCP_A_EX"] = 선수금;
            currentRow["AM_RCP_A"] = 선수금원화;
            currentRow["AM_RCP_EX_TX"] = 반제액;
            currentRow["AM_RCP_TX"] = 반제액원화;
            currentRow["AM_RCP_EX_TOT"] = (정상수금 + 선수금);
            currentRow["AM_RCP_TOT"] = (정상수금원화 + 선수금원화);

            this.cur총수금.DecimalValue = D.GetDecimal(currentRow["AM_RCP_EX_TOT"]);
            this.cur반제액.DecimalValue = D.GetDecimal(currentRow["AM_RCP_EX_TX"]);
            this.cur총수금원화.DecimalValue = D.GetDecimal(currentRow["AM_RCP_TOT"]);
            this.cur반제액원화.DecimalValue = D.GetDecimal(currentRow["AM_RCP_TX"]);
        }

        private void 화폐단위셋팅()
        {
            if (this.cbo통화명.SelectedValue == null || this.cbo통화명.SelectedValue.ToString() != "000")
            {
                Decimal num = 0;
                if (MA.기준환율.Option != MA.기준환율옵션.적용안함) 
                    num = MA.기준환율적용(this._header.CurrentRow["DT_RCP"].ToString(), this.cbo통화명.SelectedValue.ToString());

                this.cur환율.DecimalValue = num;
                this._header.CurrentRow["RT_EXCH"] = num;

                if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                    this.cur환율.Enabled = false;
                else
                    this.cur환율.Enabled = true;
            }
            else
            {
                if (!(this.cbo통화명.SelectedValue.ToString() == "000")) return;

                this.cur환율.DecimalValue = 1;
                this._header.CurrentRow["RT_EXCH"] = 1;
                this.cur환율.Enabled = false;
            }
        }

        private void 거래구분셋팅()
        {
            if (this.cbo거래구분.SelectedValue == null || this.cbo거래구분.SelectedValue.ToString() == "001")
            {
                this._flexH.Cols["AM_RCP"].Visible = false;
                this._flexH.Cols["AM_RCP_A"].Visible = false;
                this.cbo통화명.Enabled = true;

                if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                    this.cur환율.Enabled = false;
                else
                    this.cur환율.Enabled = true;
            }
            else
            {
                if (!(this.cbo거래구분.SelectedValue.ToString() == "005")) return;
                
                this._flexH.Cols["AM_RCP"].Visible = true;
                this._flexH.Cols["AM_RCP_A"].Visible = true;
                this.cbo통화명.Enabled = true;

                if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                    this.cur환율.Enabled = false;
                else
                    this.cur환율.Enabled = true;
            }
        }

        private void CalcAmremain()
        {
            if (this.ctx매출처.CodeValue == string.Empty)
            {
                this.cur선수금잔액.DecimalValue = 0;
                this.cur채권잔액.DecimalValue = 0;
            }
            else
            {
                string format = "{0}{1}{2}";
                string str1 = DateTime.Now.Year.ToString();
                DateTime now = DateTime.Now;
                string str2 = now.Month.ToString("00");
                now = DateTime.Now;
                string str3 = now.Day.ToString("00");
                string str4 = string.Format(format, str1, str2, str3);
                now = DateTime.Now;
                DateTime dateTime = now.AddYears(-5);
                DataTable dataTable = this._biz.SelectAM_RCP_A(new object[] { this.LoginInfo.CompanyCode,
                                                                              string.Format("{0:D4}{1:D2}{2:D2}",  dateTime.Year,  dateTime.Month,  dateTime.Day),
                                                                              str4,
                                                                              "001",
                                                                              "001",
                                                                              this.ctx매출처.CodeValue,
                                                                              this.ctx매출처.CodeValue,
                                                                              "",
                                                                              "" });
                Decimal num = 0;

                foreach (DataRow dataRow in dataTable.Rows)
                    num += D.GetDecimal(dataRow["AM_REMAIN"]);

                this.cur선수금잔액.DecimalValue = num;
                this.cur채권잔액.DecimalValue = this._biz.SearchOutstandingBond(this.ctx매출처.CodeValue);
            }
        }

        private void ControlEnabledDisable()
        {
            this.cbo전표처리여부.Enabled = false;
            bool flag = !this.전표처리여부;

            if (this.전표처리여부)
            {
                this.btn추가.Enabled = false;
                this.btn삭제.Enabled = false;
                this.btn추가반제.Enabled = false;
                this.btn삭제반제.Enabled = false;
                this.btn자동반제.Enabled = false;
            }
            else
            {
                if (this.반제등록여부)
                {
                    this.btn추가.Enabled = false;
                    this.btn삭제.Enabled = false;
                }
                else
                {
                    this.btn추가.Enabled = true;
                    this.btn삭제.Enabled = true;
                }

                this.btn추가반제.Enabled = true;
                this.btn삭제반제.Enabled = true;
                this.btn자동반제.Enabled = true;
            }

            if (this._header.JobMode == JobModeEnum.추가후수정)
            {
                this.btn회계전표처리.Enabled = false;
                this.btn회계전표취소.Enabled = false;
            }
            else
            {
                this.btn회계전표처리.Enabled = flag;
                if ((this.cbo반환여부.SelectedValue == null ? "N" : this.cbo반환여부.SelectedValue.ToString()) == "Y")
                {
                    this.ToolBarDeleteButtonEnabled = true;
                    this.cbo반환여부.Enabled = false;
                    this.btn회계전표처리.Enabled = false;
                    this.btn회계전표취소.Enabled = false;
                }
                else
                    this.btn회계전표취소.Enabled = !flag;
            }

            if (this.txt수금번호.Text != "" || this._flexL.HasNormalRow)
            {
                this.dtp수금일.Enabled = false;
                this.ctx매출처.Enabled = false;
                this.ctx수금처.Enabled = false;
                this.ctx영업그룹.Enabled = false;
                this.ctx수금담당자.Enabled = false;
            }
            else
            {
                this.dtp수금일.Enabled = true;
                this.ctx매출처.Enabled = true;
                this.ctx수금처.Enabled = true;
                this.ctx영업그룹.Enabled = true;
                this.ctx수금담당자.Enabled = true;
            }
        }

        private bool Check()
        {
            string str = (this.cbo반환여부.SelectedValue == null ? "N" : this.cbo반환여부.SelectedValue.ToString());

            for (int row = 1; row < this._flexH.Rows.Count; ++row)
            {
                if (this._flexH[row, "AM_RCP"].ToString() == "0" && this._flexH[row, "AM_RCP_A"].ToString() == "0" && str != "Y")
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this._flexH.Cols["AM_RCP"].Caption });
                    this._flexH.Select(row, "AM_RCP");
                    this._flexH.Focus();
                    return false;
                }

                if (this._flexH[row, "FG_RCP"].ToString() == "003" && this._flexH[row, "DC_BANK"].ToString().Length == 0)
                {
                    this.ShowMessage("어음일경우 발행기관을 입력하여주십시요");
                    this._flexH.Select(row, "DC_BANK");
                    this._flexH.Focus();
                    return false;
                }

                if ((this._flexH[row, "FG_RCP"].ToString() == "003" || this._flexH[row, "FG_RCP"].ToString() == "017") && this._flexH[row, "DT_DUE"].ToString().Length == 0)
                {
                    this.ShowMessage("어음일경우 만기/약정일을 입력하여주십시요");
                    this._flexH.Select(row, "DT_DUE");
                    this._flexH.Focus();
                    return false;
                }

                if (this._flexH[row, "FG_RCP"].ToString() == "017" && this._flexH[row, "CD_BANK"].ToString().Length == 0)
                {
                    this.ShowMessage("전자어음일경우 금융기간을 입력하여 주십시요");
                    this._flexH.Select(row, "CD_BANK");
                    this._flexH.Focus();
                    return false;
                }

                if (this._flexH[row, "NO_MGMT"].ToString().Length > 0 && (this._flexH[row, "FG_RCP"].ToString() == "003" || this._flexH[row, "FG_RCP"].ToString() == "004" || this._flexH[row, "FG_RCP"].ToString() == "005" || this._flexH[row, "FG_RCP"].ToString() == "017"))
                {
                    DataTable dataTable = this._biz.CheckDuplication(D.GetString(this._flexH[row, "NO_RCP"]), D.GetString(this._flexH[row, "NO_MGMT"]));

                    if (dataTable != null && dataTable.Rows.Count != 0 && dataTable.Rows[0][0].ToString() != string.Empty)
                    {
                        if (D.GetString(this._flexH[row, "FG_RCP"]) == "003" || D.GetString(this._flexH[row, "FG_RCP"]) == "017")
                        {
                            this.ShowMessage("이전 전표에서 입력된 관리번호(어음번호)가 있습니다. :@", dataTable.Rows[0]["NO_RCP"].ToString().Trim());
                        }
                        else
                        {
                            this.ShowMessage("이전 전표에서 입력된 관리번호가 있습니다. :@", dataTable.Rows[0]["NO_RCP"].ToString().Trim());
                        }
                        this._flexH.Select(row, "NO_MGMT");
                        this._flexH.Focus();
                        return false;
                    }
                }

                if (this._flexH[row, "NO_MGMT"].ToString().Length > 0 && (this._flexH[row, "FG_RCP"].ToString() == "003" || this._flexH[row, "FG_RCP"].ToString() == "004" || this._flexH[row, "FG_RCP"].ToString() == "005" || this._flexH[row, "FG_RCP"].ToString() == "017") && Convert.ToDecimal(this._flexH.DataTable.Compute("Count(NO_MGMT)", "NO_MGMT = '" + this._flexH[row, "NO_MGMT"].ToString() + "'")) > 1)
                {
                    if (D.GetString(this._flexH[row, "FG_RCP"]) == "003" || D.GetString(this._flexH[row, "FG_RCP"]) == "017")
                    {
                        this.ShowMessage("현재 내역에 반복 입력된 관리번호(어음번호)가 있습니다. :@", this._flexH[row, "NO_MGMT"].ToString().Trim());
                    }
                    else
                    {
                        this.ShowMessage("현재 내역에 반복 입력된 관리번호가 있습니다. :@", this._flexH[row, "NO_MGMT"].ToString().Trim());
                    }

                    this._flexH.Select(row, "NO_MGMT");
                    this._flexH.Focus();
                    return false;
                }
            }
            return true;
        }

        private bool HeaderCheck()
        {
            if (this.dtp수금일.Text == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl수금일.Text });
                this.dtp수금일.Focus();
                return false;
            }

            if (this.cbo수금형태.SelectedValue == null || this.cbo수금형태.SelectedValue.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl수금형태.Text });
                this.cbo수금형태.Focus();
                return false;
            }

            if (this.cbo거래구분.SelectedValue == null || this.cbo거래구분.SelectedValue.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl거래구분.Text });
                this.cbo거래구분.Focus();
                return false;
            }

            if (this.ctx매출처.IsEmpty())
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl매출처.Text });
                this.ctx매출처.Focus();
                return false;
            }

            if (this.ctx수금처.IsEmpty())
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl수금처.Text });
                this.ctx수금처.Focus();
                return false;
            }

            //if (this.ctx영업그룹.IsEmpty())
            //{
            //    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl영업그룹.Text });
            //    this.ctx영업그룹.Focus();
            //    return false;
            //}

            if (this.ctx수금담당자.IsEmpty())
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl수금담당자.Text });
                this.ctx수금담당자.Focus();
                return false;
            }

            if (!this.dtp수금일.IsValidated)
            {
                this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                this.dtp수금일.Focus();
                return false;
            }

            if (!(Global.MainFrame.ServerKeyCommon == "CIS") || !App.SystemEnv.PROJECT사용 || !this.ctx계산서번호검색.IsEmpty())
                return true;

            this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl계산서번호검색.Text });

            this.ctx수금담당자.Focus();

            return false;
        }

        private void 영업그룹설정()
        {
            DataTable 영업그룹;

            try
            {
                if (string.IsNullOrEmpty(this.ctx영업그룹.CodeValue) && string.IsNullOrEmpty(this.ctx영업그룹.CodeName))
                {
                    if (this._flexL.DataTable.Rows.Count > 0)
                        영업그룹 = this._biz.영업그룹(this._flexL.DataTable.Rows[0]["NO_TX"].ToString());
                    else
                        영업그룹 = this._biz.영업그룹(string.Empty);

                    if (영업그룹.Rows[0] != null)
                    {
                        ctx영업그룹.CodeValue = 영업그룹.Rows[0]["CD_SALEGRP"].ToString();
                        ctx영업그룹.CodeName = 영업그룹.Rows[0]["NM_SALEGRP"].ToString();
                        this._header.CurrentRow["CD_SALEGRP"] = ctx영업그룹.CodeValue;
                        this._header.CurrentRow["NM_SALEGRP"] = ctx영업그룹.CodeName;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private decimal 원화계산(decimal value)
        {
            decimal result = 0;

            try
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    result = Decimal.Round(value, 2, MidpointRounding.AwayFromZero);
                else
                    result = Decimal.Round(value, 0, MidpointRounding.AwayFromZero);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return result;
        }

        private decimal 외화계산(decimal value)
        {
            decimal result = 0;

            try
            {
                result = Decimal.Round(value, 2, MidpointRounding.AwayFromZero);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return result;
        }
        #endregion
    }
}

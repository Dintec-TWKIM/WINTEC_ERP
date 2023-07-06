using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.Windows.Print;
using Duzon.Erpiu.ComponentModel;

namespace master
{
    // ================================================
    // AUTHOR      : 
    // CREATE DATE : 2004.10.01
    // 
    // MODULE      : 시스템관리
    // SYSTEM      : 프로젝트 정보
    // SUBSYSTEM   : 
    // PAGE        : 프로젝트 기본정보 등록
    // PROJECT     : 
    // DESCRIPTION : 
    // ================================================ 
    // CHANGE HISTORY
    // v1.0 : 2011.03.18 이대성 수정 - 소스 리팩터링
    // v1.1 : 2011.03.21 이대성 수정 - 참고금액 0원 입력 시 저장안되는 문제 수정
    // ================================================
	public partial class P_MA_PROJECT : PageBase
    {
        #region ♪ 생성자, 변수선언  ♬
        
        P_MA_PROJECT_BIZ _biz;
        DataRow row복사 = null;

        public P_MA_PROJECT()
        {
            InitializeComponent();

            MainGrids = new FlexGrid[] { _flex };
        }

        #endregion

        #region ♪ 초기화            ♬

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();
            _biz = new P_MA_PROJECT_BIZ();
            InitGrid();
            InitEvent();
            InitControl();
        }

        #endregion

        #region -> InitGrid

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);
            _flex.SetCol("CHK", "S", 20, true, CheckTypeEnum.Y_N);
            _flex.SetDummyColumn("CHK");
            _flex.SetCol("NO_PROJECT", "프로젝트번호", 120);
            _flex.SetCol("NM_PROJECT", "프로젝트명", 150);

            _flex.SetCol("CD_PARTNER", "거래처코드", 100, false);
            _flex.SetCol("NM_PARTNER", "거래처명", 120, false);
            _flex.SetCol("CD_BIZAREA", "사업장코드", 100, false);
            _flex.SetCol("NM_BIZAREA", "사업장명", 120, false);
            _flex.SetCol("CD_DEPT", "부서코드", 100, false);
            _flex.SetCol("NM_DEPT", "부서명", 120, false);
            _flex.SetCol("CD_SALEGRP", "영업그룹코드", 100, false);
            _flex.SetCol("NM_SALEGRP", "영업그룹명", 120, false);
            _flex.SetCol("NO_EMP", "담당자", 80, false);
            _flex.SetCol("NM_EMP", "담당자명", 80, false);
            _flex.SetCol("CD_EXCH", "통화", 80, false);
            _flex.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flex.SetCol("AM_BASE", "금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex.SetCol("AM_WONAMT", "공급가액", 90, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("SD_PROJECT", "진행기간FROM", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("ED_PROJECT", "진행기간TO", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("STA_PROJECT", "진행상태", 80, false);
            _flex.SetCol("DT_CHANGE", "계약일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("DC_RMK", "비고", 150, false);
            _flex.SetCol("AM_VAT", "부가세", 90, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("AM_HAP", "합계액", 90, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("DT_SHIP", "출하일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("DT_DUE", "납기일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CD_MNG_DT1", "참고일자1", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CD_MNG_DT2", "참고일자2", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CD_MNG_DT3", "참고일자3", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CD_MNG_DT4", "참고일자4", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CD_MNG_AM1", "참고금액1", 90, false, typeof(decimal));
            _flex.SetCol("CD_MNG_AM2", "참고금액2", 90, false, typeof(decimal));
            _flex.SetCol("CD_MNG_AM3", "참고금액3", 90, false, typeof(decimal));
            _flex.SetCol("CD_MNG_AM4", "참고금액4", 90, false, typeof(decimal));
            _flex.SetCol("CD_MNG1", "참고비고1", 80, false);
            _flex.SetCol("CD_MNG2", "참고비고2", 80, false);
            _flex.SetCol("CD_MNG3", "참고비고3", 80, false);
            _flex.SetCol("CD_MNG4", "참고비고4", 80, false);
            _flex.SetCol("CD_BUDGET", "예산과목코드", 100, false);
            _flex.SetCol("NM_BUDGET", "예산과목명", 120, false);
            _flex.SetCol("FG_PJT1", "프로젝트구분1", 80, false);
            _flex.SetCol("FG_PJT2", "프로젝트구분2", 80, false);
            _flex.SetCol("FG_PJT3", "프로젝트구분3", 80, false);
            _flex.SetCol("FG_PJT4", "프로젝트구분4", 80, false);
            _flex.SetCol("FG_PJT5", "프로젝트구분5", 80, false);

            //_flex.SetBinding(new PanelExt[] { m_pnlMainH, panelExt1 }, null);
            _flex.SetBinding(new PanelExt[] { }, new object[] { m_txtNoPrj });
            _flex.SetOneGridBinding(new object[] { }, new IUParentControl[] { oneGrid2, oneGrid3, flexibleRoundedCornerBox1 });

            _flex.VerifyDuplicate = new string[] { "NO_PROJECT" };
            _flex.VerifyPrimaryKey = new string[] { "NO_PROJECT", "NM_PROJECT" };
            _flex.VerifyNotNull = new string[] { "NO_PROJECT", "NM_PROJECT" };

            _flex.AfterRowColChange += new RangeEventHandler(Flex_AfterRowColChange);
            _flex.StartEdit += new RowColEventHandler(Flex_StartEdit);

            _flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flex.SettingVersion = "1.0.1.0";
        }

        #endregion

        #region -> InitEvent

        private void InitEvent()
        {
            m_txtCdDept.Validating += new CancelEventHandler(OnControlValidating);
            m_txtCdBiz.Validating += new CancelEventHandler(OnControlValidating);

            m_dtSdPrj.Validated += new EventHandler(OnControlValidated);
            m_dtChange.Validated += new EventHandler(OnControlValidated);
            m_dtEdPrj.Validated += new EventHandler(OnControlValidated);
            m_dtShip.Validated += new EventHandler(OnControlValidated);
            m_dtDue.Validated += new EventHandler(OnControlValidated);
            dat_참고일자1.Validated += new EventHandler(OnControlValidated);
            dat_참고일자2.Validated += new EventHandler(OnControlValidated);
            dat_참고일자3.Validated += new EventHandler(OnControlValidated);
            dat_참고일자4.Validated += new EventHandler(OnControlValidated);

            m_txtAmHap.Validating += new CancelEventHandler(OnControlValidating);
            m_txtAmBase.Validating += new CancelEventHandler(OnControlValidating);
            m_txtAmVat.Validating += new CancelEventHandler(OnControlValidating);
            m_txtCdPartner.Validating += new CancelEventHandler(OnControlValidating);
            m_txtNoEmp.Validating += new CancelEventHandler(OnControlValidating);
            m_txtSaleGrp.Validating += new CancelEventHandler(OnControlValidating);
            m_txtNmPrj.Validating += new CancelEventHandler(OnControlValidating);
            m_txtAmWonAmt.Validating += new CancelEventHandler(OnControlValidating);
            m_txtRtExch.Validating += new CancelEventHandler(OnControlValidating);
            m_txtDcRemark.Validating += new CancelEventHandler(OnControlValidating);
            
            m_txtNoPrj.Validating += new CancelEventHandler(OnControlValidating);
            txt_참고비고1.Validating += new CancelEventHandler(OnControlValidating);
            txt_참고비고2.Validating += new CancelEventHandler(OnControlValidating);
            txt_참고비고3.Validating += new CancelEventHandler(OnControlValidating);
            txt_참고비고4.Validating += new CancelEventHandler(OnControlValidating);
            txt_참고금액1.Validating += new CancelEventHandler(OnControlValidating);
            txt_참고금액2.Validating += new CancelEventHandler(OnControlValidating);
            txt_참고금액3.Validating += new CancelEventHandler(OnControlValidating);
            txt_참고금액4.Validating += new CancelEventHandler(OnControlValidating);

            m_dtDue.DateChanged += new EventHandler(OnControlDateChanged);
            m_dtShip.DateChanged += new EventHandler(OnControlDateChanged);
            m_dtEdPrj.DateChanged += new EventHandler(OnControlDateChanged);
            m_dtSdPrj.DateChanged += new EventHandler(OnControlDateChanged);
            m_dtChange.DateChanged += new EventHandler(OnControlDateChanged);
            dat_참고일자1.DateChanged += new EventHandler(OnControlDateChanged);
            dat_참고일자2.DateChanged += new EventHandler(OnControlDateChanged);
            dat_참고일자3.DateChanged += new EventHandler(OnControlDateChanged);
            dat_참고일자4.DateChanged += new EventHandler(OnControlDateChanged);

            m_cboCdExch.SelectionChangeCommitted += new EventHandler(OnControlComboChanged);
            m_cboStaPrj.SelectionChangeCommitted += new EventHandler(OnControlComboChanged);
            cbo프로젝트구분1.SelectionChangeCommitted += new EventHandler(OnControlComboChanged);
            cbo프로젝트구분2.SelectionChangeCommitted += new EventHandler(OnControlComboChanged);
            cbo프로젝트구분3.SelectionChangeCommitted += new EventHandler(OnControlComboChanged);
            cbo프로젝트구분4.SelectionChangeCommitted += new EventHandler(OnControlComboChanged);
            cbo프로젝트구분5.SelectionChangeCommitted += new EventHandler(OnControlComboChanged);

            m_cboCdExch.SelectedIndexChanged += new EventHandler(CboCdExch_SelectedIndexChanged);

            btn_FILE.Click += new EventHandler(btn_FILE_Click);
        }

        #endregion

        #region -> InitControl

        private void InitControl()
        {
            SetControl sct = new SetControl();
            sct.SetCombobox(m_cboStaPrj, MA.GetCode("PR_PRJ0001", true));
            sct.SetCombobox(cbo진행상태, MA.GetCode("PR_PRJ0001", true));
            sct.SetCombobox(m_cboCdExch, MA.GetCode("MA_B000005", true));
            sct.SetCombobox(cbo프로젝트구분1, MA.GetCode("MA_B000074", true));
            sct.SetCombobox(cbo프로젝트구분2, MA.GetCode("MA_B000075", true));
            sct.SetCombobox(cbo프로젝트구분3, MA.GetCode("MA_B000076", true));
            sct.SetCombobox(cbo프로젝트구분4, MA.GetCode("MA_B000077", true));
            sct.SetCombobox(cbo프로젝트구분5, MA.GetCode("MA_B000078", true));
            m_cboStaPrj.SelectedValue = "100";

            _flex.SetDataMap("STA_PROJECT", MA.GetCode("PR_PRJ0001", true).Copy(), "CODE", "NAME");
            _flex.SetDataMap("CD_EXCH", MA.GetCode("MA_B000005", true).Copy(), "CODE", "NAME");
            _flex.SetDataMap("FG_PJT1", MA.GetCode("MA_B000074", true).Copy(), "CODE", "NAME");
            _flex.SetDataMap("FG_PJT2", MA.GetCode("MA_B000075", true).Copy(), "CODE", "NAME");
            _flex.SetDataMap("FG_PJT3", MA.GetCode("MA_B000076", true).Copy(), "CODE", "NAME");
            _flex.SetDataMap("FG_PJT4", MA.GetCode("MA_B000077", true).Copy(), "CODE", "NAME");
            _flex.SetDataMap("FG_PJT5", MA.GetCode("MA_B000078", true).Copy(), "CODE", "NAME");

            DataTable dt = MA.GetCode("MA_B000070");
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                if (i == 1)
                    lbl_참고일자1.Text = D.GetString(row["NAME"]);
                else if (i == 2)
                    lbl_참고일자2.Text = D.GetString(row["NAME"]);
                else if (i == 3)
                    lbl_참고일자3.Text = D.GetString(row["NAME"]);
                else if (i == 4)
                    lbl_참고일자4.Text = D.GetString(row["NAME"]);
                ++i;
            }

            dt = MA.GetCode("MA_B000071");
            i = 1;
            foreach (DataRow row in dt.Rows)
            {
                if (i == 1)
                    lbl_참고금액1.Text = D.GetString(row["NAME"]);
                else if (i == 2)
                    lbl_참고금액2.Text = D.GetString(row["NAME"]);
                else if (i == 3)
                    lbl_참고금액3.Text = D.GetString(row["NAME"]);
                else if (i == 4)
                    lbl_참고금액4.Text = D.GetString(row["NAME"]);
                ++i;
            }

            dt = MA.GetCode("MA_B000072");
            i = 1;
            foreach (DataRow row in dt.Rows)
            {
                if (i == 1)
                    lbl_참고비고1.Text = D.GetString(row["NAME"]);
                else if (i == 2)
                    lbl_참고비고2.Text = D.GetString(row["NAME"]);
                else if (i == 3)
                    lbl_참고비고3.Text = D.GetString(row["NAME"]);
                else if (i == 4)
                    lbl_참고비고4.Text = D.GetString(row["NAME"]);
                ++i;
            }

            DataTable dtSource = new DataTable("사용유무");
            dtSource.Columns.Add("CODE");
            dtSource.Columns.Add("NAME");
   
            dtSource.Rows.Add(new object[] { "Y", "Y" });
            dtSource.Rows.Add(new object[] { "N", "N" });

            m_cboYnUse.DataSource = dtSource;
            m_cboYnUse.DisplayMember = "NAME";
            m_cboYnUse.ValueMember = "CODE";
            m_cboYnUse.SelectedIndex = 0;
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            oneGrid1.UseCustomLayout = true;
            oneGrid2.UseCustomLayout = true;
            oneGrid2.IsSearchControl = false;
            oneGrid3.UseCustomLayout = true;
            oneGrid3.IsSearchControl = false;
            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl7.IsNecessaryCondition = true;
            bpPanelControl9.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();
            oneGrid2.InitCustomLayout();
            oneGrid3.InitCustomLayout();

            dtp계약일.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            dtp계약일.EndDateToString = Global.MainFrame.GetStringToday;

            m_dtChange.Text = m_dtShip.Text = m_dtDue.Text = m_dtEdPrj.Text = m_dtDue.Text = Global.MainFrame.GetStringToday;
            m_dtSdPrj.Text = Global.MainFrame.GetStringFirstDayInMonth;
        }

        #endregion

        #endregion

        #region ♪ 메인 버튼         ♬

        #region -> BeforeSearch

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;
            
            row복사 = null;

            if (!Chk조회계약일) return false;

            return true;
        }

        #endregion

        #region -> 조회

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
            try
            {
                if (!BeforeSearch()) return;

                // 자료 조회 중입니다. 잠시만 기다려주세요.
                ShowStatusBarMessage(2);

                //object[] obj = new object[52];
                //obj[0] = "S";
                //obj[2] = DBNull.Value;
                //obj[3] = Global.MainFrame.LoginInfo.CompanyCode;
                //obj[5] = m_txtCdPartner_P.CodeValue;
                //obj[6] = m_txtCdBiz_P.CodeValue;
                //obj[7] = m_txtSaleGrp_P.CodeValue;
                //obj[20] = D.GetString(m_dtFrom.Text).Replace("_", "").Replace("/", "").Replace(" ", "");
                //obj[21] = D.GetString(m_dtTo.Text).Replace("_", "").Replace("/", "").Replace(" ", "");
                //obj[51] = txt검색.Text;


                object[] obj = { MA.Login.회사코드,
                                 m_txtCdBiz_P.CodeValue,
                                 m_txtCdPartner_P.CodeValue,
                                 m_txtSaleGrp_P.CodeValue,
                                 dtp계약일.StartDateToString,
                                 dtp계약일.EndDateToString,
                                 txt검색.Text,
                                 D.GetString(cbo진행상태.SelectedValue)
                               };

                _flex.Binding = _biz.Search(obj);

                if (!_flex.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);

                // 몇건이 조회되었습니다.
                ShowStatusBarMessage(3, D.GetString(_flex.DataTable.Rows.Count));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
                ShowStatusBarMessage(0);
            }
        }

        #endregion

        #region -> 추가

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
            try
            {
                _flex.Rows.Add();
                _flex.Row = _flex.Rows.Count - 1;
                _flex["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                _flex["SD_PROJECT"] = Global.MainFrame.GetStringFirstDayInMonth;
                _flex["DT_CHANGE"] = _flex["ED_PROJECT"] = _flex["DT_SHIP"] = _flex["DT_DUE"] = Global.MainFrame.GetStringToday;
                _flex["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;
                _flex["STA_PROJECT"] = "100";
                _flex["CD_EXCH"] = "000";
                _flex["NO_SEQ"] = 1;
                _flex["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
                _flex["NM_EMP"] = Global.MainFrame.LoginInfo.EmployeeName;

                _flex.AddFinished();
                _flex.Col = _flex.Cols.Fixed;
                _flex.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 삭제

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
            try
            {
                if (!_flex.HasNormalRow) return;

                ShowStatusBarMessage(0);

                _flex.Rows.Remove(_flex.Row);

                ShowStatusBarMessage(5);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 저장

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 인쇄

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
            string sManuID = "R_PU_REQ_AL_RPT01_";
            string sManuName = "프로젝트 기본정보 등록";

            try
            {
                DataRow[] ldt_Report = null;

                ldt_Report = _flex.DataTable.Select("CHK ='Y'");

                DataTable dt = _flex.DataTable.Clone();

                if (ldt_Report.Length == 0)
                {
                    ShowMessage("선택된 프로젝트가 없습니다");
                    return;
                }

                foreach (DataRow dr in ldt_Report)
                    dt.ImportRow(dr);

                ReportHelper rptHelper = new ReportHelper(sManuID, sManuName);
                rptHelper.SetDataTable(dt);
                rptHelper.Print();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♪ 저장 관련         ♬

        protected override bool SaveData()
        {
            if (!base.SaveData() || !Verify_Grid(_flex)) return false;

            // 저장하기전에 수정일자를 업데이트
            for (int i = _flex.Rows.Fixed; i <= _flex.Rows.Count - 1; i++)
            {
                if (_flex.RowState(i) != DataRowState.Unchanged)
                    _flex.DataView[_flex.DataIndex(i)]["ID_UPDATE"] = Global.MainFrame.LoginInfo.UserID;
            }

            _biz.Save(_flex.GetChanges());
            _flex.AcceptChanges();

            return true;
        }

        #endregion

        #region ♪ 그리드 이벤트     ♬

        #region -> Flex_StartEdit

        void Flex_StartEdit(object sender, RowColEventArgs e)
		{
            try
            {
                if (D.GetString(_flex.Cols[_flex.Col].Name) != "CHK")
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> Flex_AfterRowColChange

        void Flex_AfterRowColChange(object sender, RangeEventArgs e)
		{
            try
            {
                if (!_flex.IsBindingEnd || !_flex.HasNormalRow)
                {
                    SetTextBinding(-1);
                    return;
                }

                //if (_flex.DataSource != null && e.OldRange.r1 != e.NewRange.r1)
                //    SetTextBinding(_flex.Row);

                if (_flex.RowState() == DataRowState.Added)
                {
                    m_txtNoPrj.Enabled = true;
                    m_txtNoPrj.Focus();
                }
                else
                    m_txtNoPrj.Enabled = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♪ 기타 이벤트       ♬

        #region -> OnControlValidating

        private void OnControlValidating(object sender, CancelEventArgs e)
		{
            try
            {
                if (!_flex.HasNormalRow) return;

                string controlName = ((Control)sender).Name.ToString();

                switch (controlName)
                {
                    case "m_txtRtExch":
                    case "m_txtAmBase":
                        if (!((CurrencyTextBox)sender).Modified) return;
                        _flex[((Control)sender).Tag.ToString()] = ((CurrencyTextBox)sender).Text.ToString();

                        // 공급가(환율 * 금액)
                        _flex["AM_WONAMT"] = m_txtAmWonAmt.DecimalValue = D.GetDecimal(Convert.ToDouble(m_txtRtExch.DecimalValue) * Convert.ToDouble(m_txtAmBase.DecimalValue));

                        // 부가세(공급가 * 0.1)
                        _flex["AM_VAT"] = m_txtAmVat.DecimalValue = D.GetDecimal(Convert.ToDouble(m_txtAmWonAmt.DecimalValue) * 0.1);

                        //합계액(공급가 * 부가세)
                        _flex["AM_HAP"] = m_txtAmHap.DecimalValue = D.GetDecimal(Convert.ToDouble(m_txtAmWonAmt.DecimalValue) + Convert.ToDouble(m_txtAmVat.DecimalValue));
                        break;

                    case "m_txtAmWonAmt":
                    case "m_txtAmVat":
                        if (!((CurrencyTextBox)sender).Modified) return;
                        _flex[((Control)sender).Tag.ToString()] = ((CurrencyTextBox)sender).Text.ToString();
                        _flex["AM_HAP"] = m_txtAmHap.DecimalValue = D.GetDecimal(Convert.ToDouble(m_txtAmWonAmt.DecimalValue) + Convert.ToDouble(m_txtAmVat.DecimalValue));
                        break;

                    case "m_txtAmHap":
                    case "txt_참고금액1":
                    case "txt_참고금액2":
                    case "txt_참고금액3":
                    case "txt_참고금액4":
                        if (!((Duzon.Common.Controls.CurrencyTextBox)sender).Modified) return;
                        _flex[((Control)sender).Tag.ToString()] = ((CurrencyTextBox)sender).DecimalValue;
                        break;

                    case "m_txtSaleGrp":
                        _flex[((Control)sender).Tag.ToString()] = ((Duzon.Common.BpControls.BpCodeTextBox)sender).CodeValue.ToString();
                        _flex["NM_SALEGRP"] = ((Duzon.Common.BpControls.BpCodeTextBox)sender).CodeName;
                        break;

                    case "m_txtCdPartner":
                        _flex[((Control)sender).Tag.ToString()] = ((Duzon.Common.BpControls.BpCodeTextBox)sender).CodeValue.ToString();
                        _flex["NM_PARTNER"] = ((Duzon.Common.BpControls.BpCodeTextBox)sender).CodeName;
                        break;

                    case "m_txtCdBiz":
                        _flex[((Control)sender).Tag.ToString()] = ((Duzon.Common.BpControls.BpCodeTextBox)sender).CodeValue.ToString();
                        _flex["NM_BIZAREA"] = ((Duzon.Common.BpControls.BpCodeTextBox)sender).CodeName;
                        break;

                    case "m_txtNoEmp":
                        _flex[((Control)sender).Tag.ToString()] = ((Duzon.Common.BpControls.BpCodeTextBox)sender).CodeValue.ToString();
                        _flex["NM_EMP"] = ((Duzon.Common.BpControls.BpCodeTextBox)sender).CodeName;
                        break;

                    case "m_txtCdDept":
                        _flex[((Control)sender).Tag.ToString()] = ((Duzon.Common.BpControls.BpCodeTextBox)sender).CodeValue.ToString();
                        _flex["NM_DEPT"] = ((Duzon.Common.BpControls.BpCodeTextBox)sender).CodeName;
                        break;

                    case "m_txtNoPrj":
                    case "m_txtNmPrj":
                    case "m_txtDcRemark":
                    case "txt_참고비고1":
                    case "txt_참고비고2":
                    case "txt_참고비고3":
                    case "txt_참고비고4":
                        if (!((Duzon.Common.Controls.TextBoxExt)sender).Modified) return;
                        _flex[((Control)sender).Tag.ToString()] = ((Control)sender).Text.ToString();
                        break;

                    case "m_dtSdPrj":
                    case "m_dtChange":
                    case "m_dtEdPrj":
                    case "m_dtShip":
                    case "m_dtDue":
                    case "dat_참고일자1":
                    case "dat_참고일자2":
                    case "dat_참고일자3":
                    case "dat_참고일자4":
                        try
                        {
                            if (!((Duzon.Common.Controls.DatePicker)sender).Modified) return;

                            string day = ((Duzon.Common.Controls.DatePicker)sender).Text.Replace("/", "").Replace(" ", "").Replace("_", "").Trim().ToString();
                            System.Convert.ToDateTime(day.Substring(0, 4) + "/" + day.Substring(4, 2) + "/" + day.Substring(6, 2));

                            if (_flex[((Control)sender).Tag.ToString()].ToString() == day.ToString()) return;
                            _flex[((Control)sender).Tag.ToString()] = day.ToString();
                        }
                        catch
                        {
                            // 날짜가 잘못 입력되었습니다. 예)2002/01/01
                            ShowMessage(공통메세지.입력형식이올바르지않습니다);
                            ((Duzon.Common.Controls.DatePicker)sender).Text = string.Empty;
                            ((Duzon.Common.Controls.DatePicker)sender).Focus();
                        }
                        break;

                    default:
                        break;
                }

               if (!ToolBarSaveButtonEnabled) ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
                return;
            }
        }

        #endregion

        #region -> OnControlValidated

        void OnControlValidated(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                Control ctl = sender as Control;
                if (ctl == null) return;

                switch (ctl.Name)
                {
                    case "m_dtSdPrj":
                    case "m_dtChange":
                    case "m_dtEdPrj":
                    case "m_dtShip":
                    case "m_dtDue":
                    case "dat_참고일자1":
                    case "dat_참고일자2":
                    case "dat_참고일자3":
                    case "dat_참고일자4":
                        try
                        {
                            string day = "";
                            if (!((Duzon.Common.Controls.DatePicker)sender).Modified) return;
                            if (ctl.Text != "")
                            {
                                day = ((Duzon.Common.Controls.DatePicker)sender).Text.Replace("/", "").Replace(" ", "").Replace("_", "").Trim().ToString();
                            }

                            if (_flex[((Control)sender).Tag.ToString()].ToString() == day.ToString()) return;
                            _flex[((Control)sender).Tag.ToString()] = day.ToString();

                            if (!ToolBarSaveButtonEnabled) ToolBarSaveButtonEnabled = true;
                        }
                        catch
                        {
                            // 날짜가 잘못 입력되었습니다. 예)2002/01/01
                            ShowMessage(공통메세지.입력형식이올바르지않습니다);
                            ((Duzon.Common.Controls.DatePicker)sender).Text = string.Empty;
                            ((Duzon.Common.Controls.DatePicker)sender).Focus();
                        }
                        break;

                    default:
                        break;
                }

                if (!ToolBarSaveButtonEnabled) ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> OnControlDateChanged

        private void OnControlDateChanged(object sender, EventArgs e)
		{
            try
            {
                if (!((Duzon.Common.Controls.DatePicker)sender).Modified) return;

                if (!_flex.HasNormalRow) return;

                string day = ((Duzon.Common.Controls.DatePicker)sender).Text.Replace("/", "").Replace(" ", "").Replace("_", "").Trim().ToString();

                _flex[((Control)sender).Tag.ToString()] = day.ToString();

                //if (!ToolBarSaveButtonEnabled) ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
                return;
            }
        }

        #endregion

        #region -> OnControlComboChanged

        private void OnControlComboChanged(object sender, EventArgs e)
        {
            try
            {
                if (!((Duzon.Common.Controls.DropDownComboBox)sender).Modified) return;

                if (!_flex.HasNormalRow) return;

                _flex[((Control)sender).Tag.ToString()] = ((Duzon.Common.Controls.DropDownComboBox)sender).SelectedValue.ToString();

                if (!ToolBarSaveButtonEnabled) ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
                return;
            }
        }

        #endregion

        #region -> CboCdExch_SelectedIndexChanged

        private void CboCdExch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string 환율설정 = D.GetString(((Duzon.Common.Controls.DropDownComboBox)sender).SelectedValue);

                if (환율설정 == "" || 환율설정 == string.Empty) return;

                switch (환율설정)
                {
                    case "000":
                    case "":
                        m_txtRtExch.CurrencyDecimalDigits = 0;
                        m_txtAmBase.CurrencyDecimalDigits = 0;
                        m_txtAmWonAmt.CurrencyDecimalDigits = 0;
                        m_txtAmVat.CurrencyDecimalDigits = 0;
                        m_txtAmHap.CurrencyDecimalDigits = 0;
                        break;
                    default:
                        m_txtRtExch.CurrencyDecimalDigits = 4;
                        m_txtAmBase.CurrencyDecimalDigits = 4;
                        m_txtAmWonAmt.CurrencyDecimalDigits = 4;
                        m_txtAmVat.CurrencyDecimalDigits = 4;
                        m_txtAmHap.CurrencyDecimalDigits = 4;
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♪ 기타 메서드       ♬

        #region -> ClearDataControl

        private void ClearDataControl()
        {
            try
            {
                m_txtNoPrj.Text = m_txtNmPrj.Text = m_txtDcRemark.Text = string.Empty;
                m_txtSaleGrp.CodeValue = m_txtCdPartner.CodeValue = m_txtCdBiz.CodeValue = m_txtCdDept.Text = string.Empty;
                m_txtSaleGrp.CodeName = m_txtCdPartner.CodeName = m_txtCdBiz.CodeName = m_txtCdDept.CodeName = string.Empty;

                m_txtRtExch.Text = m_txtAmBase.Text = m_txtAmWonAmt.Text = m_txtAmVat.Text = m_txtAmHap.Text = "0";

                m_dtSdPrj.Text = Global.MainFrame.GetStringFirstDayInMonth;
                m_dtChange.Text = m_dtEdPrj.Text = m_dtShip.Text = m_dtDue.Text = Global.MainFrame.GetStringToday;

                m_cboStaPrj.SelectedIndex = m_cboCdExch.SelectedIndex = 1;

                m_txtNoPrj.Enabled = ToolBarDeleteButtonEnabled = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> SetTextBinding

        private void SetTextBinding(int row)
        {
            try
            {
                if (row > 0)
                {
                    m_txtDcRemark.Text = D.GetString(_flex[(string)m_txtDcRemark.Tag]);
                    m_txtNoPrj.Text = D.GetString(_flex[(string)m_txtNoPrj.Tag]);
                    m_txtNmPrj.Text = D.GetString(_flex[(string)m_txtNmPrj.Tag]);

                    m_txtAmWonAmt.Text = D.GetString(_flex[(string)m_txtAmWonAmt.Tag]);
                    m_txtRtExch.Text = D.GetString(_flex[(string)m_txtRtExch.Tag]);
                    m_txtAmHap.Text = D.GetString(_flex[(string)m_txtAmHap.Tag]);
                    m_txtAmBase.Text = D.GetString(_flex[(string)m_txtAmBase.Tag]);
                    m_txtAmVat.Text = D.GetString(_flex[(string)m_txtAmVat.Tag]);

                    m_txtSaleGrp.CodeValue = D.GetString(_flex["CD_SALEGRP"]);
                    m_txtSaleGrp.CodeName = D.GetString(_flex["NM_SALEGRP"]);
                    m_txtCdPartner.CodeValue = D.GetString(_flex["CD_PARTNER"]);
                    m_txtCdPartner.CodeName = D.GetString(_flex["NM_PARTNER"]);
                    m_txtNoEmp.CodeValue = D.GetString(_flex["NO_EMP"]);
                    m_txtNoEmp.CodeName = D.GetString(_flex["NM_EMP"]);
                    m_txtCdBiz.CodeValue = D.GetString(_flex["CD_BIZAREA"]);
                    m_txtCdBiz.CodeName = D.GetString(_flex["NM_BIZAREA"]);
                    m_txtCdDept.SetCode(D.GetString(_flex["CD_DEPT"]), D.GetString(_flex["NM_DEPT"]));

                    // 날짜
                    m_dtChange.Text = D.GetString(_flex[(string)m_dtChange.Tag]);
                    m_dtEdPrj.Text = D.GetString(_flex[(string)m_dtEdPrj.Tag]);
                    m_dtSdPrj.Text = D.GetString(_flex[(string)m_dtSdPrj.Tag]);
                    m_dtShip.Text = D.GetString(_flex[(string)m_dtShip.Tag]);
                    m_dtDue.Text = D.GetString(_flex[(string)m_dtDue.Tag]);

                    m_cboStaPrj.SelectedValue = D.GetString(_flex[(string)m_cboStaPrj.Tag]);
                    m_cboCdExch.SelectedValue = D.GetString(_flex[(string)m_cboCdExch.Tag]);

                    dat_참고일자1.Text = D.GetString(_flex[(string)dat_참고일자1.Tag]);
                    dat_참고일자2.Text = D.GetString(_flex[(string)dat_참고일자2.Tag]);
                    dat_참고일자3.Text = D.GetString(_flex[(string)dat_참고일자3.Tag]);
                    dat_참고일자4.Text = D.GetString(_flex[(string)dat_참고일자4.Tag]);

                    txt_참고금액1.Text = D.GetString(_flex[(string)txt_참고금액1.Tag]);
                    txt_참고금액2.Text = D.GetString(_flex[(string)txt_참고금액2.Tag]);
                    txt_참고금액3.Text = D.GetString(_flex[(string)txt_참고금액3.Tag]);
                    txt_참고금액4.Text = D.GetString(_flex[(string)txt_참고금액4.Tag]);

                    txt_참고비고1.Text = D.GetString(_flex[(string)txt_참고비고1.Tag]);
                    txt_참고비고2.Text = D.GetString(_flex[(string)txt_참고비고2.Tag]);
                    txt_참고비고3.Text = D.GetString(_flex[(string)txt_참고비고3.Tag]);
                    txt_참고비고4.Text = D.GetString(_flex[(string)txt_참고비고4.Tag]);

                    cbo프로젝트구분1.SelectedValue = D.GetString(_flex["FG_PJT1"]);
                    cbo프로젝트구분2.SelectedValue = D.GetString(_flex["FG_PJT2"]);
                    cbo프로젝트구분3.SelectedValue = D.GetString(_flex["FG_PJT3"]);
                    cbo프로젝트구분4.SelectedValue = D.GetString(_flex["FG_PJT4"]);
                    cbo프로젝트구분5.SelectedValue = D.GetString(_flex["FG_PJT5"]);

                    Application.DoEvents();

                    oneGrid1.Enabled = true;
                    oneGrid2.Enabled = true;
                }
                else
                {
                    oneGrid1.Enabled = false;
                    oneGrid2.Enabled = false;
                    ClearDataControl();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♪ 화면 내 버튼 클릭 ♬

        #region -> 프로젝트복사 버튼 클릭
        
        private void btn프로젝트복사_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                DataRow row품목 = _flex.GetDataRow(_flex.Row);
                DataTable dt복사 = _flex.DataTable.Clone();
                dt복사.ImportRow(row품목);

                row복사 = dt복사.Rows[0];
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 붙여넣기 버튼 클릭
        
        private void btn붙여넣기_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                if (row복사 == null)
                {
                    ShowMessage("붙여넣기 할 내용이 존재하지 않습니다.");
                    return;
                }

                DataRow row = _flex.GetDataRow(_flex.Row);

                foreach (DataColumn col in _flex.DataTable.Columns)
                {
                    row[col.ColumnName] = row복사[col.ColumnName];
                }

                _flex.RefreshBindng(_flex.Row);
                if (tabControlExt1.SelectedTab != tabPage1)
                    tabControlExt1.SelectedTab = tabPage1;
                //txt품목코드.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 첨부파일 버튼 클릭
        
        private void btn_FILE_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                string cd_file_code = Global.MainFrame.LoginInfo.CompanyCode + "P_MA_PROJECT_" + D.GetString(_flex["NO_PROJECT"]);

                DataTable dt = _biz.IsFileHelpCheck();

                // 대용량이 아닌 첨부파일 도움창을 사용함.
                master.P_MA_FILE_SUB m_dlg = new master.P_MA_FILE_SUB("SA", Global.MainFrame.CurrentPageID, cd_file_code);
                if (m_dlg.ShowDialog(this) == DialogResult.Cancel)
                    return;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        bool Chk조회계약일 { get { return Checker.IsValid(dtp계약일, true, m_lblDtChange_P.Text); } }

        #endregion
    }
}
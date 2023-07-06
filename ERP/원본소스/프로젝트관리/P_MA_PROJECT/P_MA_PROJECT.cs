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
    // MODULE      : �ý��۰���
    // SYSTEM      : ������Ʈ ����
    // SUBSYSTEM   : 
    // PAGE        : ������Ʈ �⺻���� ���
    // PROJECT     : 
    // DESCRIPTION : 
    // ================================================ 
    // CHANGE HISTORY
    // v1.0 : 2011.03.18 �̴뼺 ���� - �ҽ� �����͸�
    // v1.1 : 2011.03.21 �̴뼺 ���� - ����ݾ� 0�� �Է� �� ����ȵǴ� ���� ����
    // ================================================
	public partial class P_MA_PROJECT : PageBase
    {
        #region �� ������, ��������  ��
        
        P_MA_PROJECT_BIZ _biz;
        DataRow row���� = null;

        public P_MA_PROJECT()
        {
            InitializeComponent();

            MainGrids = new FlexGrid[] { _flex };
        }

        #endregion

        #region �� �ʱ�ȭ            ��

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
            _flex.SetCol("NO_PROJECT", "������Ʈ��ȣ", 120);
            _flex.SetCol("NM_PROJECT", "������Ʈ��", 150);

            _flex.SetCol("CD_PARTNER", "�ŷ�ó�ڵ�", 100, false);
            _flex.SetCol("NM_PARTNER", "�ŷ�ó��", 120, false);
            _flex.SetCol("CD_BIZAREA", "������ڵ�", 100, false);
            _flex.SetCol("NM_BIZAREA", "������", 120, false);
            _flex.SetCol("CD_DEPT", "�μ��ڵ�", 100, false);
            _flex.SetCol("NM_DEPT", "�μ���", 120, false);
            _flex.SetCol("CD_SALEGRP", "�����׷��ڵ�", 100, false);
            _flex.SetCol("NM_SALEGRP", "�����׷��", 120, false);
            _flex.SetCol("NO_EMP", "�����", 80, false);
            _flex.SetCol("NM_EMP", "����ڸ�", 80, false);
            _flex.SetCol("CD_EXCH", "��ȭ", 80, false);
            _flex.SetCol("RT_EXCH", "ȯ��", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flex.SetCol("AM_BASE", "�ݾ�", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flex.SetCol("AM_WONAMT", "���ް���", 90, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("SD_PROJECT", "����ⰣFROM", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("ED_PROJECT", "����ⰣTO", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("STA_PROJECT", "�������", 80, false);
            _flex.SetCol("DT_CHANGE", "�����", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("DC_RMK", "���", 150, false);
            _flex.SetCol("AM_VAT", "�ΰ���", 90, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("AM_HAP", "�հ��", 90, false, typeof(decimal), FormatTpType.MONEY);
            _flex.SetCol("DT_SHIP", "������", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("DT_DUE", "������", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CD_MNG_DT1", "��������1", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CD_MNG_DT2", "��������2", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CD_MNG_DT3", "��������3", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CD_MNG_DT4", "��������4", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flex.SetCol("CD_MNG_AM1", "����ݾ�1", 90, false, typeof(decimal));
            _flex.SetCol("CD_MNG_AM2", "����ݾ�2", 90, false, typeof(decimal));
            _flex.SetCol("CD_MNG_AM3", "����ݾ�3", 90, false, typeof(decimal));
            _flex.SetCol("CD_MNG_AM4", "����ݾ�4", 90, false, typeof(decimal));
            _flex.SetCol("CD_MNG1", "������1", 80, false);
            _flex.SetCol("CD_MNG2", "������2", 80, false);
            _flex.SetCol("CD_MNG3", "������3", 80, false);
            _flex.SetCol("CD_MNG4", "������4", 80, false);
            _flex.SetCol("CD_BUDGET", "��������ڵ�", 100, false);
            _flex.SetCol("NM_BUDGET", "��������", 120, false);
            _flex.SetCol("FG_PJT1", "������Ʈ����1", 80, false);
            _flex.SetCol("FG_PJT2", "������Ʈ����2", 80, false);
            _flex.SetCol("FG_PJT3", "������Ʈ����3", 80, false);
            _flex.SetCol("FG_PJT4", "������Ʈ����4", 80, false);
            _flex.SetCol("FG_PJT5", "������Ʈ����5", 80, false);

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
            dat_��������1.Validated += new EventHandler(OnControlValidated);
            dat_��������2.Validated += new EventHandler(OnControlValidated);
            dat_��������3.Validated += new EventHandler(OnControlValidated);
            dat_��������4.Validated += new EventHandler(OnControlValidated);

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
            txt_������1.Validating += new CancelEventHandler(OnControlValidating);
            txt_������2.Validating += new CancelEventHandler(OnControlValidating);
            txt_������3.Validating += new CancelEventHandler(OnControlValidating);
            txt_������4.Validating += new CancelEventHandler(OnControlValidating);
            txt_����ݾ�1.Validating += new CancelEventHandler(OnControlValidating);
            txt_����ݾ�2.Validating += new CancelEventHandler(OnControlValidating);
            txt_����ݾ�3.Validating += new CancelEventHandler(OnControlValidating);
            txt_����ݾ�4.Validating += new CancelEventHandler(OnControlValidating);

            m_dtDue.DateChanged += new EventHandler(OnControlDateChanged);
            m_dtShip.DateChanged += new EventHandler(OnControlDateChanged);
            m_dtEdPrj.DateChanged += new EventHandler(OnControlDateChanged);
            m_dtSdPrj.DateChanged += new EventHandler(OnControlDateChanged);
            m_dtChange.DateChanged += new EventHandler(OnControlDateChanged);
            dat_��������1.DateChanged += new EventHandler(OnControlDateChanged);
            dat_��������2.DateChanged += new EventHandler(OnControlDateChanged);
            dat_��������3.DateChanged += new EventHandler(OnControlDateChanged);
            dat_��������4.DateChanged += new EventHandler(OnControlDateChanged);

            m_cboCdExch.SelectionChangeCommitted += new EventHandler(OnControlComboChanged);
            m_cboStaPrj.SelectionChangeCommitted += new EventHandler(OnControlComboChanged);
            cbo������Ʈ����1.SelectionChangeCommitted += new EventHandler(OnControlComboChanged);
            cbo������Ʈ����2.SelectionChangeCommitted += new EventHandler(OnControlComboChanged);
            cbo������Ʈ����3.SelectionChangeCommitted += new EventHandler(OnControlComboChanged);
            cbo������Ʈ����4.SelectionChangeCommitted += new EventHandler(OnControlComboChanged);
            cbo������Ʈ����5.SelectionChangeCommitted += new EventHandler(OnControlComboChanged);

            m_cboCdExch.SelectedIndexChanged += new EventHandler(CboCdExch_SelectedIndexChanged);

            btn_FILE.Click += new EventHandler(btn_FILE_Click);
        }

        #endregion

        #region -> InitControl

        private void InitControl()
        {
            SetControl sct = new SetControl();
            sct.SetCombobox(m_cboStaPrj, MA.GetCode("PR_PRJ0001", true));
            sct.SetCombobox(cbo�������, MA.GetCode("PR_PRJ0001", true));
            sct.SetCombobox(m_cboCdExch, MA.GetCode("MA_B000005", true));
            sct.SetCombobox(cbo������Ʈ����1, MA.GetCode("MA_B000074", true));
            sct.SetCombobox(cbo������Ʈ����2, MA.GetCode("MA_B000075", true));
            sct.SetCombobox(cbo������Ʈ����3, MA.GetCode("MA_B000076", true));
            sct.SetCombobox(cbo������Ʈ����4, MA.GetCode("MA_B000077", true));
            sct.SetCombobox(cbo������Ʈ����5, MA.GetCode("MA_B000078", true));
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
                    lbl_��������1.Text = D.GetString(row["NAME"]);
                else if (i == 2)
                    lbl_��������2.Text = D.GetString(row["NAME"]);
                else if (i == 3)
                    lbl_��������3.Text = D.GetString(row["NAME"]);
                else if (i == 4)
                    lbl_��������4.Text = D.GetString(row["NAME"]);
                ++i;
            }

            dt = MA.GetCode("MA_B000071");
            i = 1;
            foreach (DataRow row in dt.Rows)
            {
                if (i == 1)
                    lbl_����ݾ�1.Text = D.GetString(row["NAME"]);
                else if (i == 2)
                    lbl_����ݾ�2.Text = D.GetString(row["NAME"]);
                else if (i == 3)
                    lbl_����ݾ�3.Text = D.GetString(row["NAME"]);
                else if (i == 4)
                    lbl_����ݾ�4.Text = D.GetString(row["NAME"]);
                ++i;
            }

            dt = MA.GetCode("MA_B000072");
            i = 1;
            foreach (DataRow row in dt.Rows)
            {
                if (i == 1)
                    lbl_������1.Text = D.GetString(row["NAME"]);
                else if (i == 2)
                    lbl_������2.Text = D.GetString(row["NAME"]);
                else if (i == 3)
                    lbl_������3.Text = D.GetString(row["NAME"]);
                else if (i == 4)
                    lbl_������4.Text = D.GetString(row["NAME"]);
                ++i;
            }

            DataTable dtSource = new DataTable("�������");
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

            dtp�����.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            dtp�����.EndDateToString = Global.MainFrame.GetStringToday;

            m_dtChange.Text = m_dtShip.Text = m_dtDue.Text = m_dtEdPrj.Text = m_dtDue.Text = Global.MainFrame.GetStringToday;
            m_dtSdPrj.Text = Global.MainFrame.GetStringFirstDayInMonth;
        }

        #endregion

        #endregion

        #region �� ���� ��ư         ��

        #region -> BeforeSearch

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;
            
            row���� = null;

            if (!Chk��ȸ�����) return false;

            return true;
        }

        #endregion

        #region -> ��ȸ

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
            try
            {
                if (!BeforeSearch()) return;

                // �ڷ� ��ȸ ���Դϴ�. ��ø� ��ٷ��ּ���.
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
                //obj[51] = txt�˻�.Text;


                object[] obj = { MA.Login.ȸ���ڵ�,
                                 m_txtCdBiz_P.CodeValue,
                                 m_txtCdPartner_P.CodeValue,
                                 m_txtSaleGrp_P.CodeValue,
                                 dtp�����.StartDateToString,
                                 dtp�����.EndDateToString,
                                 txt�˻�.Text,
                                 D.GetString(cbo�������.SelectedValue)
                               };

                _flex.Binding = _biz.Search(obj);

                if (!_flex.HasNormalRow)
                    ShowMessage(����޼���.���ǿ��ش��ϴ³����̾����ϴ�);

                // ����� ��ȸ�Ǿ����ϴ�.
                ShowStatusBarMessage(3, D.GetString(_flex.DataTable.Rows.Count));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
                ShowStatusBarMessage(0);
            }
        }

        #endregion

        #region -> �߰�

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

        #region -> ����

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

        #region -> ����

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(����޼���.�ڷᰡ��������������Ǿ����ϴ�);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> �μ�

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
            string sManuID = "R_PU_REQ_AL_RPT01_";
            string sManuName = "������Ʈ �⺻���� ���";

            try
            {
                DataRow[] ldt_Report = null;

                ldt_Report = _flex.DataTable.Select("CHK ='Y'");

                DataTable dt = _flex.DataTable.Clone();

                if (ldt_Report.Length == 0)
                {
                    ShowMessage("���õ� ������Ʈ�� �����ϴ�");
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

        #region �� ���� ����         ��

        protected override bool SaveData()
        {
            if (!base.SaveData() || !Verify_Grid(_flex)) return false;

            // �����ϱ����� �������ڸ� ������Ʈ
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

        #region �� �׸��� �̺�Ʈ     ��

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

        #region �� ��Ÿ �̺�Ʈ       ��

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

                        // ���ް�(ȯ�� * �ݾ�)
                        _flex["AM_WONAMT"] = m_txtAmWonAmt.DecimalValue = D.GetDecimal(Convert.ToDouble(m_txtRtExch.DecimalValue) * Convert.ToDouble(m_txtAmBase.DecimalValue));

                        // �ΰ���(���ް� * 0.1)
                        _flex["AM_VAT"] = m_txtAmVat.DecimalValue = D.GetDecimal(Convert.ToDouble(m_txtAmWonAmt.DecimalValue) * 0.1);

                        //�հ��(���ް� * �ΰ���)
                        _flex["AM_HAP"] = m_txtAmHap.DecimalValue = D.GetDecimal(Convert.ToDouble(m_txtAmWonAmt.DecimalValue) + Convert.ToDouble(m_txtAmVat.DecimalValue));
                        break;

                    case "m_txtAmWonAmt":
                    case "m_txtAmVat":
                        if (!((CurrencyTextBox)sender).Modified) return;
                        _flex[((Control)sender).Tag.ToString()] = ((CurrencyTextBox)sender).Text.ToString();
                        _flex["AM_HAP"] = m_txtAmHap.DecimalValue = D.GetDecimal(Convert.ToDouble(m_txtAmWonAmt.DecimalValue) + Convert.ToDouble(m_txtAmVat.DecimalValue));
                        break;

                    case "m_txtAmHap":
                    case "txt_����ݾ�1":
                    case "txt_����ݾ�2":
                    case "txt_����ݾ�3":
                    case "txt_����ݾ�4":
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
                    case "txt_������1":
                    case "txt_������2":
                    case "txt_������3":
                    case "txt_������4":
                        if (!((Duzon.Common.Controls.TextBoxExt)sender).Modified) return;
                        _flex[((Control)sender).Tag.ToString()] = ((Control)sender).Text.ToString();
                        break;

                    case "m_dtSdPrj":
                    case "m_dtChange":
                    case "m_dtEdPrj":
                    case "m_dtShip":
                    case "m_dtDue":
                    case "dat_��������1":
                    case "dat_��������2":
                    case "dat_��������3":
                    case "dat_��������4":
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
                            // ��¥�� �߸� �ԷµǾ����ϴ�. ��)2002/01/01
                            ShowMessage(����޼���.�Է������̿ùٸ����ʽ��ϴ�);
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
                    case "dat_��������1":
                    case "dat_��������2":
                    case "dat_��������3":
                    case "dat_��������4":
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
                            // ��¥�� �߸� �ԷµǾ����ϴ�. ��)2002/01/01
                            ShowMessage(����޼���.�Է������̿ùٸ����ʽ��ϴ�);
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
                string ȯ������ = D.GetString(((Duzon.Common.Controls.DropDownComboBox)sender).SelectedValue);

                if (ȯ������ == "" || ȯ������ == string.Empty) return;

                switch (ȯ������)
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

        #region �� ��Ÿ �޼���       ��

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

                    // ��¥
                    m_dtChange.Text = D.GetString(_flex[(string)m_dtChange.Tag]);
                    m_dtEdPrj.Text = D.GetString(_flex[(string)m_dtEdPrj.Tag]);
                    m_dtSdPrj.Text = D.GetString(_flex[(string)m_dtSdPrj.Tag]);
                    m_dtShip.Text = D.GetString(_flex[(string)m_dtShip.Tag]);
                    m_dtDue.Text = D.GetString(_flex[(string)m_dtDue.Tag]);

                    m_cboStaPrj.SelectedValue = D.GetString(_flex[(string)m_cboStaPrj.Tag]);
                    m_cboCdExch.SelectedValue = D.GetString(_flex[(string)m_cboCdExch.Tag]);

                    dat_��������1.Text = D.GetString(_flex[(string)dat_��������1.Tag]);
                    dat_��������2.Text = D.GetString(_flex[(string)dat_��������2.Tag]);
                    dat_��������3.Text = D.GetString(_flex[(string)dat_��������3.Tag]);
                    dat_��������4.Text = D.GetString(_flex[(string)dat_��������4.Tag]);

                    txt_����ݾ�1.Text = D.GetString(_flex[(string)txt_����ݾ�1.Tag]);
                    txt_����ݾ�2.Text = D.GetString(_flex[(string)txt_����ݾ�2.Tag]);
                    txt_����ݾ�3.Text = D.GetString(_flex[(string)txt_����ݾ�3.Tag]);
                    txt_����ݾ�4.Text = D.GetString(_flex[(string)txt_����ݾ�4.Tag]);

                    txt_������1.Text = D.GetString(_flex[(string)txt_������1.Tag]);
                    txt_������2.Text = D.GetString(_flex[(string)txt_������2.Tag]);
                    txt_������3.Text = D.GetString(_flex[(string)txt_������3.Tag]);
                    txt_������4.Text = D.GetString(_flex[(string)txt_������4.Tag]);

                    cbo������Ʈ����1.SelectedValue = D.GetString(_flex["FG_PJT1"]);
                    cbo������Ʈ����2.SelectedValue = D.GetString(_flex["FG_PJT2"]);
                    cbo������Ʈ����3.SelectedValue = D.GetString(_flex["FG_PJT3"]);
                    cbo������Ʈ����4.SelectedValue = D.GetString(_flex["FG_PJT4"]);
                    cbo������Ʈ����5.SelectedValue = D.GetString(_flex["FG_PJT5"]);

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

        #region �� ȭ�� �� ��ư Ŭ�� ��

        #region -> ������Ʈ���� ��ư Ŭ��
        
        private void btn������Ʈ����_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                DataRow rowǰ�� = _flex.GetDataRow(_flex.Row);
                DataTable dt���� = _flex.DataTable.Clone();
                dt����.ImportRow(rowǰ��);

                row���� = dt����.Rows[0];
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> �ٿ��ֱ� ��ư Ŭ��
        
        private void btn�ٿ��ֱ�_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                if (row���� == null)
                {
                    ShowMessage("�ٿ��ֱ� �� ������ �������� �ʽ��ϴ�.");
                    return;
                }

                DataRow row = _flex.GetDataRow(_flex.Row);

                foreach (DataColumn col in _flex.DataTable.Columns)
                {
                    row[col.ColumnName] = row����[col.ColumnName];
                }

                _flex.RefreshBindng(_flex.Row);
                if (tabControlExt1.SelectedTab != tabPage1)
                    tabControlExt1.SelectedTab = tabPage1;
                //txtǰ���ڵ�.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> ÷������ ��ư Ŭ��
        
        private void btn_FILE_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                string cd_file_code = Global.MainFrame.LoginInfo.CompanyCode + "P_MA_PROJECT_" + D.GetString(_flex["NO_PROJECT"]);

                DataTable dt = _biz.IsFileHelpCheck();

                // ��뷮�� �ƴ� ÷������ ����â�� �����.
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

        bool Chk��ȸ����� { get { return Checker.IsValid(dtp�����, true, m_lblDtChange_P.Text); } }

        #endregion
    }
}
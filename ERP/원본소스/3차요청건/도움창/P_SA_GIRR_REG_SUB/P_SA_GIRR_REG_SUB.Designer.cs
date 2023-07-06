namespace sale
{
    partial class P_SA_GIRR_REG_SUB
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_GIRR_REG_SUB));
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.bp품목 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.panelExt3 = new Duzon.Common.Controls.PanelExt();
            this.cbo유무환구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.bp수주담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc수주번호 = new Duzon.Common.BpControls.BpComboBox();
            this.panelExt2 = new Duzon.Common.Controls.PanelExt();
            this.bp_PJT = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_cde거래처 = new Duzon.Common.Controls.CodeTextBox(this.components);
            this.bpSalegrp = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_cdeTpvat = new Duzon.Common.Controls.CodeTextBox(this.components);
            this.bpNoEmp = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpTpGi = new Duzon.Common.BpControls.BpCodeTextBox();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.bpCdsl = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_mskEnd = new Duzon.Common.Controls.DatePicker();
            this.m_mskStart = new Duzon.Common.Controls.DatePicker();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.m_cdePlant = new Duzon.Common.Controls.CodeTextBox(this.components);
            this.m_lblDtIo = new Duzon.Common.Controls.LabelExt();
            this.label6 = new Duzon.Common.Controls.LabelExt();
            this.panel2 = new Duzon.Common.Controls.PanelExt();
            this.lbl유무환구분 = new Duzon.Common.Controls.LabelExt();
            this.label1 = new Duzon.Common.Controls.LabelExt();
            this.label3 = new Duzon.Common.Controls.LabelExt();
            this.lbl거래처 = new Duzon.Common.Controls.LabelExt();
            this.panel1 = new Duzon.Common.Controls.PanelExt();
            this.lbl품목 = new Duzon.Common.Controls.LabelExt();
            this.lbl수주번호 = new Duzon.Common.Controls.LabelExt();
            this.labelExt2 = new Duzon.Common.Controls.LabelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.lbl수주담당자 = new Duzon.Common.Controls.LabelExt();
            this.m_lblPlantGir = new Duzon.Common.Controls.LabelExt();
            this.labelExt3 = new Duzon.Common.Controls.LabelExt();
            this.m_lblCdSalegrp = new Duzon.Common.Controls.LabelExt();
            this.m_btnQuery = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnApply = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnCancel = new Duzon.Common.Controls.RoundedButton(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_pnlGridH = new Duzon.Common.Controls.PanelExt();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this.m_pnlGridL = new Duzon.Common.Controls.PanelExt();
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelExt4 = new Duzon.Common.Controls.PanelExt();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskStart)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.m_pnlGridH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            this.m_pnlGridL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelExt4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.bp품목);
            this.panel4.Controls.Add(this.panelExt3);
            this.panel4.Controls.Add(this.cbo유무환구분);
            this.panel4.Controls.Add(this.bp수주담당자);
            this.panel4.Controls.Add(this.bpc수주번호);
            this.panel4.Controls.Add(this.panelExt2);
            this.panel4.Controls.Add(this.bp_PJT);
            this.panel4.Controls.Add(this.m_cde거래처);
            this.panel4.Controls.Add(this.bpSalegrp);
            this.panel4.Controls.Add(this.m_cdeTpvat);
            this.panel4.Controls.Add(this.bpNoEmp);
            this.panel4.Controls.Add(this.bpTpGi);
            this.panel4.Controls.Add(this.panelExt1);
            this.panel4.Controls.Add(this.bpCdsl);
            this.panel4.Controls.Add(this.m_mskEnd);
            this.panel4.Controls.Add(this.m_mskStart);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.m_cdePlant);
            this.panel4.Controls.Add(this.m_lblDtIo);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(794, 137);
            this.panel4.TabIndex = 3;
            // 
            // bp품목
            // 
            this.bp품목.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp품목.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp품목.ButtonImage")));
            this.bp품목.ChildMode = "";
            this.bp품목.CodeName = "";
            this.bp품목.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp품목.CodeValue = "";
            this.bp품목.ComboCheck = true;
            this.bp품목.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.bp품목.IsCodeValueToUpper = true;
            this.bp품목.ItemBackColor = System.Drawing.Color.White;
            this.bp품목.Location = new System.Drawing.Point(80, 111);
            this.bp품목.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp품목.Name = "bp품목";
            this.bp품목.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp품목.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bp품목.SearchCode = true;
            this.bp품목.SelectCount = 0;
            this.bp품목.SetDefaultValue = true;
            this.bp품목.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp품목.Size = new System.Drawing.Size(186, 21);
            this.bp품목.TabIndex = 138;
            this.bp품목.TabStop = false;
            this.bp품목.Tag = "";
            this.bp품목.Text = "bpCodeTextBox3";
            this.bp품목.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            // 
            // panelExt3
            // 
            this.panelExt3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt3.BackColor = System.Drawing.Color.Transparent;
            this.panelExt3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt3.BackgroundImage")));
            this.panelExt3.Location = new System.Drawing.Point(4, 108);
            this.panelExt3.Name = "panelExt3";
            this.panelExt3.Size = new System.Drawing.Size(784, 1);
            this.panelExt3.TabIndex = 137;
            // 
            // cbo유무환구분
            // 
            this.cbo유무환구분.AutoDropDown = true;
            this.cbo유무환구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo유무환구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo유무환구분.ItemHeight = 12;
            this.cbo유무환구분.Location = new System.Drawing.Point(604, 87);
            this.cbo유무환구분.Name = "cbo유무환구분";
            this.cbo유무환구분.ShowCheckBox = false;
            this.cbo유무환구분.Size = new System.Drawing.Size(86, 20);
            this.cbo유무환구분.TabIndex = 136;
            this.cbo유무환구분.Tag = "";
            this.cbo유무환구분.UseKeyEnter = false;
            this.cbo유무환구분.UseKeyF3 = false;
            // 
            // bp수주담당자
            // 
            this.bp수주담당자.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp수주담당자.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp수주담당자.ButtonImage")));
            this.bp수주담당자.ChildMode = "";
            this.bp수주담당자.CodeName = "";
            this.bp수주담당자.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp수주담당자.CodeValue = "";
            this.bp수주담당자.ComboCheck = true;
            this.bp수주담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.bp수주담당자.IsCodeValueToUpper = true;
            this.bp수주담당자.ItemBackColor = System.Drawing.Color.White;
            this.bp수주담당자.Location = new System.Drawing.Point(351, 86);
            this.bp수주담당자.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp수주담당자.Name = "bp수주담당자";
            this.bp수주담당자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp수주담당자.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bp수주담당자.SearchCode = true;
            this.bp수주담당자.SelectCount = 0;
            this.bp수주담당자.SetDefaultValue = true;
            this.bp수주담당자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp수주담당자.Size = new System.Drawing.Size(171, 21);
            this.bp수주담당자.TabIndex = 47;
            this.bp수주담당자.TabStop = false;
            this.bp수주담당자.Tag = "";
            this.bp수주담당자.Text = "bpCodeTextBox3";
            // 
            // bpc수주번호
            // 
            this.bpc수주번호.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc수주번호.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc수주번호.ButtonImage")));
            this.bpc수주번호.ChildMode = "";
            this.bpc수주번호.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc수주번호.ComboCheck = true;
            this.bpc수주번호.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.bpc수주번호.IsCodeValueToUpper = true;
            this.bpc수주번호.ItemBackColor = System.Drawing.SystemColors.Window;
            this.bpc수주번호.Location = new System.Drawing.Point(80, 86);
            this.bpc수주번호.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc수주번호.Name = "bpc수주번호";
            this.bpc수주번호.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc수주번호.SearchCode = true;
            this.bpc수주번호.SelectCount = 0;
            this.bpc수주번호.SelectedIndex = -1;
            this.bpc수주번호.SelectedItem = null;
            this.bpc수주번호.SelectedText = "";
            this.bpc수주번호.SelectedValue = null;
            this.bpc수주번호.SetDefaultValue = false;
            this.bpc수주번호.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc수주번호.Size = new System.Drawing.Size(186, 21);
            this.bpc수주번호.TabIndex = 46;
            this.bpc수주번호.TabStop = false;
            this.bpc수주번호.Text = "bpComboBox1";
            this.bpc수주번호.UserCodeName = "NO_SO";
            this.bpc수주번호.UserCodeValue = "NO_SO";
            this.bpc수주번호.UserHelpID = "H_SA_SO_SUB";
            // 
            // panelExt2
            // 
            this.panelExt2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt2.BackColor = System.Drawing.Color.Transparent;
            this.panelExt2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt2.BackgroundImage")));
            this.panelExt2.Location = new System.Drawing.Point(4, 82);
            this.panelExt2.Name = "panelExt2";
            this.panelExt2.Size = new System.Drawing.Size(784, 1);
            this.panelExt2.TabIndex = 45;
            // 
            // bp_PJT
            // 
            this.bp_PJT.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bp_PJT.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_PJT.ButtonImage")));
            this.bp_PJT.ChildMode = "";
            this.bp_PJT.CodeName = "";
            this.bp_PJT.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp_PJT.CodeValue = "";
            this.bp_PJT.ComboCheck = true;
            this.bp_PJT.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.bp_PJT.IsCodeValueToUpper = true;
            this.bp_PJT.ItemBackColor = System.Drawing.Color.White;
            this.bp_PJT.Location = new System.Drawing.Point(604, 59);
            this.bp_PJT.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp_PJT.Name = "bp_PJT";
            this.bp_PJT.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp_PJT.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bp_PJT.SearchCode = true;
            this.bp_PJT.SelectCount = 0;
            this.bp_PJT.SetDefaultValue = false;
            this.bp_PJT.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp_PJT.Size = new System.Drawing.Size(176, 21);
            this.bp_PJT.TabIndex = 44;
            this.bp_PJT.TabStop = false;
            this.bp_PJT.Text = "bpCodeTextBox1";
            this.bp_PJT.UserCodeName = "NM_PROJECT";
            this.bp_PJT.UserCodeValue = "NO_PROJECT";
            this.bp_PJT.UserHelpID = "H_SA_PRJ_SUB";
            this.bp_PJT.CodeChanged += new System.EventHandler(this.Control_CodeChanged);
            this.bp_PJT.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            this.bp_PJT.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
            // 
            // m_cde거래처
            // 
            this.m_cde거래처.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_cde거래처.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.m_cde거래처.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_cde거래처.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_cde거래처.CodeName = "";
            this.m_cde거래처.CodeValue = "";
            this.m_cde거래처.IgnoreTextChanged = false;
            this.m_cde거래처.IsConfirmed = false;
            this.m_cde거래처.Location = new System.Drawing.Point(604, 3);
            this.m_cde거래처.Name = "m_cde거래처";
            this.m_cde거래처.ReadOnly = true;
            this.m_cde거래처.SelectedAllEnabled = false;
            this.m_cde거래처.Size = new System.Drawing.Size(176, 21);
            this.m_cde거래처.TabIndex = 43;
            this.m_cde거래처.TabStop = false;
            this.m_cde거래처.UseKeyEnter = false;
            this.m_cde거래처.UseKeyF3 = false;
            // 
            // bpSalegrp
            // 
            this.bpSalegrp.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpSalegrp.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpSalegrp.ButtonImage")));
            this.bpSalegrp.ChildMode = "";
            this.bpSalegrp.CodeName = "";
            this.bpSalegrp.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpSalegrp.CodeValue = "";
            this.bpSalegrp.ComboCheck = true;
            this.bpSalegrp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bpSalegrp.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.bpSalegrp.IsCodeValueToUpper = true;
            this.bpSalegrp.ItemBackColor = System.Drawing.Color.White;
            this.bpSalegrp.Location = new System.Drawing.Point(351, 30);
            this.bpSalegrp.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpSalegrp.Name = "bpSalegrp";
            this.bpSalegrp.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpSalegrp.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpSalegrp.SearchCode = true;
            this.bpSalegrp.SelectCount = 0;
            this.bpSalegrp.SetDefaultValue = false;
            this.bpSalegrp.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpSalegrp.Size = new System.Drawing.Size(171, 21);
            this.bpSalegrp.TabIndex = 42;
            this.bpSalegrp.TabStop = false;
            this.bpSalegrp.Tag = "CD_SALEGRP;NM_SALEGRP";
            // 
            // m_cdeTpvat
            // 
            this.m_cdeTpvat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_cdeTpvat.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.m_cdeTpvat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_cdeTpvat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_cdeTpvat.CodeName = "";
            this.m_cdeTpvat.CodeValue = "";
            this.m_cdeTpvat.IgnoreTextChanged = false;
            this.m_cdeTpvat.IsConfirmed = false;
            this.m_cdeTpvat.Location = new System.Drawing.Point(80, 30);
            this.m_cdeTpvat.Name = "m_cdeTpvat";
            this.m_cdeTpvat.ReadOnly = true;
            this.m_cdeTpvat.SelectedAllEnabled = false;
            this.m_cdeTpvat.Size = new System.Drawing.Size(186, 21);
            this.m_cdeTpvat.TabIndex = 5;
            this.m_cdeTpvat.TabStop = false;
            this.m_cdeTpvat.UseKeyEnter = false;
            this.m_cdeTpvat.UseKeyF3 = false;
            // 
            // bpNoEmp
            // 
            this.bpNoEmp.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpNoEmp.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpNoEmp.ButtonImage")));
            this.bpNoEmp.ChildMode = "";
            this.bpNoEmp.CodeName = "";
            this.bpNoEmp.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpNoEmp.CodeValue = "";
            this.bpNoEmp.ComboCheck = true;
            this.bpNoEmp.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.bpNoEmp.IsCodeValueToUpper = true;
            this.bpNoEmp.ItemBackColor = System.Drawing.Color.White;
            this.bpNoEmp.Location = new System.Drawing.Point(351, 59);
            this.bpNoEmp.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpNoEmp.Name = "bpNoEmp";
            this.bpNoEmp.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpNoEmp.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpNoEmp.SearchCode = true;
            this.bpNoEmp.SelectCount = 0;
            this.bpNoEmp.SetDefaultValue = true;
            this.bpNoEmp.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpNoEmp.Size = new System.Drawing.Size(171, 21);
            this.bpNoEmp.TabIndex = 8;
            this.bpNoEmp.TabStop = false;
            this.bpNoEmp.Tag = "";
            this.bpNoEmp.Text = "bpCodeTextBox3";
            // 
            // bpTpGi
            // 
            this.bpTpGi.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpTpGi.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpTpGi.ButtonImage")));
            this.bpTpGi.ChildMode = "";
            this.bpTpGi.CodeName = "";
            this.bpTpGi.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpTpGi.CodeValue = "";
            this.bpTpGi.ComboCheck = true;
            this.bpTpGi.HelpID = Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB;
            this.bpTpGi.IsCodeValueToUpper = true;
            this.bpTpGi.ItemBackColor = System.Drawing.Color.White;
            this.bpTpGi.Location = new System.Drawing.Point(80, 59);
            this.bpTpGi.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpTpGi.Name = "bpTpGi";
            this.bpTpGi.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpTpGi.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpTpGi.SearchCode = true;
            this.bpTpGi.SelectCount = 0;
            this.bpTpGi.SetDefaultValue = false;
            this.bpTpGi.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpTpGi.Size = new System.Drawing.Size(186, 21);
            this.bpTpGi.TabIndex = 7;
            this.bpTpGi.TabStop = false;
            this.bpTpGi.Tag = "";
            this.bpTpGi.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            // 
            // panelExt1
            // 
            this.panelExt1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt1.BackColor = System.Drawing.Color.Transparent;
            this.panelExt1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt1.BackgroundImage")));
            this.panelExt1.Location = new System.Drawing.Point(4, 54);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(784, 1);
            this.panelExt1.TabIndex = 41;
            // 
            // bpCdsl
            // 
            this.bpCdsl.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bpCdsl.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpCdsl.ButtonImage")));
            this.bpCdsl.ChildMode = "";
            this.bpCdsl.CodeName = "";
            this.bpCdsl.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpCdsl.CodeValue = "";
            this.bpCdsl.ComboCheck = true;
            this.bpCdsl.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB;
            this.bpCdsl.IsCodeValueToUpper = true;
            this.bpCdsl.ItemBackColor = System.Drawing.Color.White;
            this.bpCdsl.Location = new System.Drawing.Point(604, 30);
            this.bpCdsl.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpCdsl.Name = "bpCdsl";
            this.bpCdsl.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpCdsl.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpCdsl.SearchCode = true;
            this.bpCdsl.SelectCount = 0;
            this.bpCdsl.SetDefaultValue = false;
            this.bpCdsl.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpCdsl.Size = new System.Drawing.Size(176, 21);
            this.bpCdsl.TabIndex = 6;
            this.bpCdsl.TabStop = false;
            this.bpCdsl.Text = "bpCodeTextBox1";
            this.bpCdsl.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            // 
            // m_mskEnd
            // 
            this.m_mskEnd.CalendarBackColor = System.Drawing.Color.White;
            this.m_mskEnd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_mskEnd.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.m_mskEnd.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.m_mskEnd.Location = new System.Drawing.Point(181, 3);
            this.m_mskEnd.Mask = "####/##/##";
            this.m_mskEnd.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_mskEnd.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_mskEnd.MaximumSize = new System.Drawing.Size(0, 21);
            this.m_mskEnd.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_mskEnd.Modified = false;
            this.m_mskEnd.Name = "m_mskEnd";
            this.m_mskEnd.PaddingCharacter = '_';
            this.m_mskEnd.PassivePromptCharacter = '_';
            this.m_mskEnd.PromptCharacter = '_';
            this.m_mskEnd.SelectedDayColor = System.Drawing.Color.White;
            this.m_mskEnd.ShowToDay = true;
            this.m_mskEnd.ShowTodayCircle = true;
            this.m_mskEnd.ShowUpDown = false;
            this.m_mskEnd.Size = new System.Drawing.Size(85, 21);
            this.m_mskEnd.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.m_mskEnd.TabIndex = 1;
            this.m_mskEnd.TabStop = false;
            this.m_mskEnd.TitleBackColor = System.Drawing.Color.White;
            this.m_mskEnd.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.m_mskEnd.ToDayColor = System.Drawing.Color.Red;
            this.m_mskEnd.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_mskEnd.UseKeyF3 = false;
            this.m_mskEnd.Value = new System.DateTime(((long)(0)));
            // 
            // m_mskStart
            // 
            this.m_mskStart.CalendarBackColor = System.Drawing.Color.White;
            this.m_mskStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_mskStart.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.m_mskStart.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.m_mskStart.Location = new System.Drawing.Point(80, 3);
            this.m_mskStart.Mask = "####/##/##";
            this.m_mskStart.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_mskStart.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_mskStart.MaximumSize = new System.Drawing.Size(0, 21);
            this.m_mskStart.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_mskStart.Modified = false;
            this.m_mskStart.Name = "m_mskStart";
            this.m_mskStart.PaddingCharacter = '_';
            this.m_mskStart.PassivePromptCharacter = '_';
            this.m_mskStart.PromptCharacter = '_';
            this.m_mskStart.SelectedDayColor = System.Drawing.Color.White;
            this.m_mskStart.ShowToDay = true;
            this.m_mskStart.ShowTodayCircle = true;
            this.m_mskStart.ShowUpDown = false;
            this.m_mskStart.Size = new System.Drawing.Size(85, 21);
            this.m_mskStart.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.m_mskStart.TabIndex = 0;
            this.m_mskStart.TabStop = false;
            this.m_mskStart.TitleBackColor = System.Drawing.Color.White;
            this.m_mskStart.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.m_mskStart.ToDayColor = System.Drawing.Color.Red;
            this.m_mskStart.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_mskStart.UseKeyF3 = false;
            this.m_mskStart.Value = new System.DateTime(((long)(0)));
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel6.BackgroundImage")));
            this.panel6.Location = new System.Drawing.Point(5, 26);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(784, 1);
            this.panel6.TabIndex = 25;
            // 
            // m_cdePlant
            // 
            this.m_cdePlant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_cdePlant.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.m_cdePlant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_cdePlant.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_cdePlant.CodeName = "";
            this.m_cdePlant.CodeValue = "";
            this.m_cdePlant.IgnoreTextChanged = false;
            this.m_cdePlant.IsConfirmed = false;
            this.m_cdePlant.Location = new System.Drawing.Point(351, 3);
            this.m_cdePlant.Name = "m_cdePlant";
            this.m_cdePlant.ReadOnly = true;
            this.m_cdePlant.SelectedAllEnabled = false;
            this.m_cdePlant.Size = new System.Drawing.Size(170, 21);
            this.m_cdePlant.TabIndex = 2;
            this.m_cdePlant.TabStop = false;
            this.m_cdePlant.UseKeyEnter = false;
            this.m_cdePlant.UseKeyF3 = false;
            // 
            // m_lblDtIo
            // 
            this.m_lblDtIo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblDtIo.Location = new System.Drawing.Point(3, 6);
            this.m_lblDtIo.Name = "m_lblDtIo";
            this.m_lblDtIo.Resizeble = true;
            this.m_lblDtIo.Size = new System.Drawing.Size(70, 18);
            this.m_lblDtIo.TabIndex = 7;
            this.m_lblDtIo.Tag = "";
            this.m_lblDtIo.Text = "출하일자";
            this.m_lblDtIo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(165, 7);
            this.label6.Name = "label6";
            this.label6.Resizeble = true;
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "∼";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel2.Controls.Add(this.lbl유무환구분);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lbl거래처);
            this.panel2.Location = new System.Drawing.Point(526, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(75, 108);
            this.panel2.TabIndex = 23;
            // 
            // lbl유무환구분
            // 
            this.lbl유무환구분.Location = new System.Drawing.Point(5, 86);
            this.lbl유무환구분.Name = "lbl유무환구분";
            this.lbl유무환구분.Resizeble = true;
            this.lbl유무환구분.Size = new System.Drawing.Size(68, 18);
            this.lbl유무환구분.TabIndex = 4;
            this.lbl유무환구분.Tag = "";
            this.lbl유무환구분.Text = "유무환구분";
            this.lbl유무환구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 59);
            this.label1.Name = "label1";
            this.label1.Resizeble = true;
            this.label1.Size = new System.Drawing.Size(58, 18);
            this.label1.TabIndex = 0;
            this.label1.Tag = "";
            this.label1.Text = "프로젝트";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 33);
            this.label3.Name = "label3";
            this.label3.Resizeble = true;
            this.label3.Size = new System.Drawing.Size(56, 18);
            this.label3.TabIndex = 0;
            this.label3.Tag = "";
            this.label3.Text = "창고";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl거래처
            // 
            this.lbl거래처.Location = new System.Drawing.Point(14, 6);
            this.lbl거래처.Name = "lbl거래처";
            this.lbl거래처.Resizeble = true;
            this.lbl거래처.Size = new System.Drawing.Size(58, 18);
            this.lbl거래처.TabIndex = 3;
            this.lbl거래처.Tag = "";
            this.lbl거래처.Text = "거래처";
            this.lbl거래처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel1.Controls.Add(this.lbl품목);
            this.panel1.Controls.Add(this.lbl수주번호);
            this.panel1.Controls.Add(this.labelExt2);
            this.panel1.Controls.Add(this.labelExt1);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(75, 133);
            this.panel1.TabIndex = 22;
            // 
            // lbl품목
            // 
            this.lbl품목.Location = new System.Drawing.Point(3, 111);
            this.lbl품목.Name = "lbl품목";
            this.lbl품목.Resizeble = true;
            this.lbl품목.Size = new System.Drawing.Size(70, 18);
            this.lbl품목.TabIndex = 4;
            this.lbl품목.Tag = "";
            this.lbl품목.Text = "품목";
            this.lbl품목.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl수주번호
            // 
            this.lbl수주번호.Location = new System.Drawing.Point(3, 86);
            this.lbl수주번호.Name = "lbl수주번호";
            this.lbl수주번호.Resizeble = true;
            this.lbl수주번호.Size = new System.Drawing.Size(70, 18);
            this.lbl수주번호.TabIndex = 3;
            this.lbl수주번호.Tag = "";
            this.lbl수주번호.Text = "수주번호";
            this.lbl수주번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelExt2
            // 
            this.labelExt2.Location = new System.Drawing.Point(3, 59);
            this.labelExt2.Name = "labelExt2";
            this.labelExt2.Resizeble = true;
            this.labelExt2.Size = new System.Drawing.Size(70, 18);
            this.labelExt2.TabIndex = 1;
            this.labelExt2.Tag = "";
            this.labelExt2.Text = "출하형태";
            this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelExt1
            // 
            this.labelExt1.Location = new System.Drawing.Point(3, 32);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(70, 18);
            this.labelExt1.TabIndex = 2;
            this.labelExt1.Tag = "";
            this.labelExt1.Text = "과세구분";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel7.Controls.Add(this.lbl수주담당자);
            this.panel7.Controls.Add(this.m_lblPlantGir);
            this.panel7.Controls.Add(this.labelExt3);
            this.panel7.Controls.Add(this.m_lblCdSalegrp);
            this.panel7.Location = new System.Drawing.Point(272, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(75, 108);
            this.panel7.TabIndex = 40;
            // 
            // lbl수주담당자
            // 
            this.lbl수주담당자.Location = new System.Drawing.Point(4, 86);
            this.lbl수주담당자.Name = "lbl수주담당자";
            this.lbl수주담당자.Resizeble = true;
            this.lbl수주담당자.Size = new System.Drawing.Size(68, 18);
            this.lbl수주담당자.TabIndex = 4;
            this.lbl수주담당자.Tag = "";
            this.lbl수주담당자.Text = "수주담당자";
            this.lbl수주담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblPlantGir
            // 
            this.m_lblPlantGir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblPlantGir.Location = new System.Drawing.Point(4, 5);
            this.m_lblPlantGir.Name = "m_lblPlantGir";
            this.m_lblPlantGir.Resizeble = true;
            this.m_lblPlantGir.Size = new System.Drawing.Size(68, 18);
            this.m_lblPlantGir.TabIndex = 1;
            this.m_lblPlantGir.Tag = "";
            this.m_lblPlantGir.Text = "공장";
            this.m_lblPlantGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelExt3
            // 
            this.labelExt3.Location = new System.Drawing.Point(4, 59);
            this.labelExt3.Name = "labelExt3";
            this.labelExt3.Resizeble = true;
            this.labelExt3.Size = new System.Drawing.Size(68, 18);
            this.labelExt3.TabIndex = 3;
            this.labelExt3.Tag = "";
            this.labelExt3.Text = "담당자";
            this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblCdSalegrp
            // 
            this.m_lblCdSalegrp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblCdSalegrp.Location = new System.Drawing.Point(4, 31);
            this.m_lblCdSalegrp.Name = "m_lblCdSalegrp";
            this.m_lblCdSalegrp.Resizeble = true;
            this.m_lblCdSalegrp.Size = new System.Drawing.Size(68, 18);
            this.m_lblCdSalegrp.TabIndex = 0;
            this.m_lblCdSalegrp.Tag = "";
            this.m_lblCdSalegrp.Text = "영업그룹";
            this.m_lblCdSalegrp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_btnQuery
            // 
            this.m_btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnQuery.BackColor = System.Drawing.Color.White;
            this.m_btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnQuery.Location = new System.Drawing.Point(607, 3);
            this.m_btnQuery.MaximumSize = new System.Drawing.Size(0, 19);
            this.m_btnQuery.Name = "m_btnQuery";
            this.m_btnQuery.Size = new System.Drawing.Size(60, 19);
            this.m_btnQuery.TabIndex = 17;
            this.m_btnQuery.TabStop = false;
            this.m_btnQuery.Tag = "Q_QUERY";
            this.m_btnQuery.Text = "조회";
            this.m_btnQuery.UseVisualStyleBackColor = false;
            this.m_btnQuery.Click += new System.EventHandler(this.OnSearchButtonClicked);
            // 
            // m_btnApply
            // 
            this.m_btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnApply.BackColor = System.Drawing.Color.White;
            this.m_btnApply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnApply.Enabled = false;
            this.m_btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnApply.Location = new System.Drawing.Point(669, 3);
            this.m_btnApply.MaximumSize = new System.Drawing.Size(0, 19);
            this.m_btnApply.Name = "m_btnApply";
            this.m_btnApply.Size = new System.Drawing.Size(60, 19);
            this.m_btnApply.TabIndex = 18;
            this.m_btnApply.TabStop = false;
            this.m_btnApply.Tag = "";
            this.m_btnApply.Text = "적용";
            this.m_btnApply.UseVisualStyleBackColor = false;
            this.m_btnApply.Click += new System.EventHandler(this.OnApply);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.BackColor = System.Drawing.Color.White;
            this.m_btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCancel.Location = new System.Drawing.Point(731, 3);
            this.m_btnCancel.MaximumSize = new System.Drawing.Size(0, 19);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(60, 19);
            this.m_btnCancel.TabIndex = 19;
            this.m_btnCancel.TabStop = false;
            this.m_btnCancel.Tag = "Q_CANCEL";
            this.m_btnCancel.Text = "취소";
            this.m_btnCancel.UseVisualStyleBackColor = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 177);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_pnlGridH);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_pnlGridL);
            this.splitContainer1.Size = new System.Drawing.Size(794, 458);
            this.splitContainer1.SplitterDistance = 225;
            this.splitContainer1.TabIndex = 20;
            // 
            // m_pnlGridH
            // 
            this.m_pnlGridH.Controls.Add(this._flexH);
            this.m_pnlGridH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlGridH.Location = new System.Drawing.Point(0, 0);
            this.m_pnlGridH.Name = "m_pnlGridH";
            this.m_pnlGridH.Size = new System.Drawing.Size(794, 225);
            this.m_pnlGridH.TabIndex = 9;
            // 
            // _flexH
            // 
            this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexH.AutoResize = false;
            this._flexH.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexH.EnabledHeaderCheck = true;
            this._flexH.Font = new System.Drawing.Font("굴림", 9F);
            this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexH.Location = new System.Drawing.Point(0, 0);
            this._flexH.Name = "_flexH";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 18;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(794, 225);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 0;
            // 
            // m_pnlGridL
            // 
            this.m_pnlGridL.Controls.Add(this._flexL);
            this.m_pnlGridL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlGridL.Location = new System.Drawing.Point(0, 0);
            this.m_pnlGridL.Name = "m_pnlGridL";
            this.m_pnlGridL.Size = new System.Drawing.Size(794, 229);
            this.m_pnlGridL.TabIndex = 10;
            // 
            // _flexL
            // 
            this._flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexL.AutoResize = false;
            this._flexL.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexL.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexL.EnabledHeaderCheck = true;
            this._flexL.Font = new System.Drawing.Font("굴림", 9F);
            this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexL.Location = new System.Drawing.Point(0, 0);
            this._flexL.Name = "_flexL";
            this._flexL.Rows.Count = 1;
            this._flexL.Rows.DefaultSize = 18;
            this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexL.ShowSort = false;
            this._flexL.Size = new System.Drawing.Size(794, 229);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelExt4, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 638);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // panelExt4
            // 
            this.panelExt4.Controls.Add(this.m_btnCancel);
            this.panelExt4.Controls.Add(this.m_btnQuery);
            this.panelExt4.Controls.Add(this.m_btnApply);
            this.panelExt4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt4.Location = new System.Drawing.Point(3, 146);
            this.panelExt4.Name = "panelExt4";
            this.panelExt4.Size = new System.Drawing.Size(794, 25);
            this.panelExt4.TabIndex = 21;
            // 
            // P_SA_GIRR_REG_SUB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(804, 689);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_SA_GIRR_REG_SUB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TitleText = "반품의뢰(출하적용)";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskStart)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.m_pnlGridH.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            this.m_pnlGridL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelExt4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Duzon.Common.Controls.PanelExt panel4;
        private Duzon.Common.BpControls.BpCodeTextBox bpCdsl;
        private Duzon.Common.Controls.DatePicker m_mskEnd;
        private Duzon.Common.Controls.DatePicker m_mskStart;
        private Duzon.Common.Controls.PanelExt panel6;
        private Duzon.Common.Controls.CodeTextBox m_cdePlant;
        private Duzon.Common.Controls.LabelExt m_lblDtIo;
        private Duzon.Common.Controls.LabelExt label6;
        private Duzon.Common.Controls.PanelExt panel2;
        private Duzon.Common.Controls.LabelExt label1;
        private Duzon.Common.Controls.LabelExt label3;
        private Duzon.Common.Controls.PanelExt panel1;
        private Duzon.Common.Controls.PanelExt panel7;
        private Duzon.Common.Controls.LabelExt m_lblPlantGir;
        private Duzon.Common.Controls.LabelExt m_lblCdSalegrp;
        private Duzon.Common.Controls.CodeTextBox m_cdeTpvat;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.BpControls.BpCodeTextBox bpTpGi;
        private Duzon.Common.Controls.LabelExt labelExt2;
        private Duzon.Common.Controls.LabelExt labelExt3;
        private Duzon.Common.BpControls.BpCodeTextBox bpNoEmp;
        private Duzon.Common.Controls.RoundedButton m_btnQuery;
        private Duzon.Common.Controls.RoundedButton m_btnApply;
        private Duzon.Common.Controls.RoundedButton m_btnCancel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Duzon.Common.Controls.PanelExt m_pnlGridH;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Duzon.Common.Controls.PanelExt m_pnlGridL;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Duzon.Common.BpControls.BpCodeTextBox bpSalegrp;
        private Duzon.Common.Controls.CodeTextBox m_cde거래처;
        private Duzon.Common.Controls.LabelExt lbl거래처;
        private Duzon.Common.BpControls.BpCodeTextBox bp_PJT;
        private Duzon.Common.BpControls.BpComboBox bpc수주번호;
        private Duzon.Common.Controls.PanelExt panelExt2;
        private Duzon.Common.Controls.LabelExt lbl수주번호;
        private Duzon.Common.Controls.LabelExt lbl수주담당자;
        private Duzon.Common.BpControls.BpCodeTextBox bp수주담당자;
        private Duzon.Common.Controls.LabelExt lbl유무환구분;
        private Duzon.Common.Controls.DropDownComboBox cbo유무환구분;
        private Duzon.Common.Controls.PanelExt panelExt3;
        private Duzon.Common.Controls.LabelExt lbl품목;
        private Duzon.Common.BpControls.BpCodeTextBox bp품목;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.PanelExt panelExt4;
    }
}

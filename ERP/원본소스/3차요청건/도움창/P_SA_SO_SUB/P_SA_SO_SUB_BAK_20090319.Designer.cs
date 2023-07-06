namespace sale
{
    partial class P_SA_SO_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_SO_SUB));
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.m_lblTitle = new Duzon.Common.Controls.LabelExt();
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.bp_Emp = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bp_Project = new Duzon.Common.Controls.TextButton();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.bpSalegrp = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpNm_Sl = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_mskEnd = new Duzon.Common.Controls.DatePicker();
            this.m_mskStart = new Duzon.Common.Controls.DatePicker();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.m_cdePlant = new Duzon.Common.Controls.CodeTextBox(this.components);
            this.m_cdePartner = new Duzon.Common.Controls.CodeTextBox(this.components);
            this.m_lblDtSo = new Duzon.Common.Controls.LabelExt();
            this.label6 = new Duzon.Common.Controls.LabelExt();
            this.panel2 = new Duzon.Common.Controls.PanelExt();
            this.label1 = new Duzon.Common.Controls.LabelExt();
            this.label3 = new Duzon.Common.Controls.LabelExt();
            this.panel1 = new Duzon.Common.Controls.PanelExt();
            this.txt_NoProject = new Duzon.Common.Controls.TextBoxExt();
            this.lbl_프로젝트 = new Duzon.Common.Controls.LabelExt();
            this.m_lblCdPartner = new Duzon.Common.Controls.LabelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.m_lblPlantGir = new Duzon.Common.Controls.LabelExt();
            this.m_lblCdSalegrp = new Duzon.Common.Controls.LabelExt();
            this.m_cdeTpBusi = new Duzon.Common.Controls.CodeTextBox(this.components);
            this.m_btnQuery = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnApply = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnCancel = new Duzon.Common.Controls.RoundedButton(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskStart)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.m_lblTitle);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(804, 47);
            this.panel5.TabIndex = 1;
            // 
            // m_lblTitle
            // 
            this.m_lblTitle.AutoSize = true;
            this.m_lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.m_lblTitle.Font = new System.Drawing.Font("굴림체", 10F, System.Drawing.FontStyle.Bold);
            this.m_lblTitle.ForeColor = System.Drawing.Color.White;
            this.m_lblTitle.Location = new System.Drawing.Point(15, 16);
            this.m_lblTitle.Name = "m_lblTitle";
            this.m_lblTitle.Resizeble = false;
            this.m_lblTitle.Size = new System.Drawing.Size(143, 14);
            this.m_lblTitle.TabIndex = 0;
            this.m_lblTitle.Tag = "APPLY_SO";
            this.m_lblTitle.Text = "납품의뢰(수주적용)";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.bp_Emp);
            this.panel4.Controls.Add(this.bp_Project);
            this.panel4.Controls.Add(this.panelExt1);
            this.panel4.Controls.Add(this.bpSalegrp);
            this.panel4.Controls.Add(this.bpNm_Sl);
            this.panel4.Controls.Add(this.m_mskEnd);
            this.panel4.Controls.Add(this.m_mskStart);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.m_cdePlant);
            this.panel4.Controls.Add(this.m_cdePartner);
            this.panel4.Controls.Add(this.m_lblDtSo);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.m_cdeTpBusi);
            this.panel4.Location = new System.Drawing.Point(10, 55);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(785, 84);
            this.panel4.TabIndex = 2;
            // 
            // bp_Emp
            // 
            this.bp_Emp.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp_Emp.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_Emp.ButtonImage")));
            this.bp_Emp.ChildMode = "";
            this.bp_Emp.CodeName = "";
            this.bp_Emp.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp_Emp.CodeValue = "";
            this.bp_Emp.ComboCheck = true;
            this.bp_Emp.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.bp_Emp.IsCodeValueToUpper = true;
            this.bp_Emp.ItemBackColor = System.Drawing.Color.White;
            this.bp_Emp.Location = new System.Drawing.Point(350, 58);
            this.bp_Emp.Name = "bp_Emp";
            this.bp_Emp.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp_Emp.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bp_Emp.SearchCode = true;
            this.bp_Emp.SelectCount = 0;
            this.bp_Emp.SetDefaultValue = false;
            this.bp_Emp.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp_Emp.Size = new System.Drawing.Size(181, 21);
            this.bp_Emp.TabIndex = 207;
            this.bp_Emp.TabStop = false;
            this.bp_Emp.Tag = "NO_EMP;NM_KOR";
            // 
            // bp_Project
            // 
            this.bp_Project.BackColor = System.Drawing.Color.White;
            this.bp_Project.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_Project.ButtonImage")));
            this.bp_Project.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bp_Project.Location = new System.Drawing.Point(80, 58);
            this.bp_Project.Modified = false;
            this.bp_Project.Name = "bp_Project";
            this.bp_Project.ReadOnly = false;
            this.bp_Project.Size = new System.Drawing.Size(186, 21);
            this.bp_Project.TabIndex = 206;
            this.bp_Project.Tag = "";
            this.bp_Project.TextChanged += new System.EventHandler(this.bp_Project_TextChanged);
            this.bp_Project.Leave += new System.EventHandler(this.bp_Project_Leave);
            this.bp_Project.Search += new Duzon.Common.Controls.SearchEventHandler(this.bp_Project_Search);
            // 
            // panelExt1
            // 
            this.panelExt1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt1.BackColor = System.Drawing.Color.Transparent;
            this.panelExt1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt1.BackgroundImage")));
            this.panelExt1.Location = new System.Drawing.Point(4, 54);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(775, 1);
            this.panelExt1.TabIndex = 41;
            // 
            // bpSalegrp
            // 
            this.bpSalegrp.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bpSalegrp.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpSalegrp.ButtonImage")));
            this.bpSalegrp.ChildMode = "";
            this.bpSalegrp.CodeName = "";
            this.bpSalegrp.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpSalegrp.CodeValue = "";
            this.bpSalegrp.ComboCheck = true;
            this.bpSalegrp.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.bpSalegrp.IsCodeValueToUpper = true;
            this.bpSalegrp.ItemBackColor = System.Drawing.Color.White;
            this.bpSalegrp.Location = new System.Drawing.Point(350, 30);
            this.bpSalegrp.Name = "bpSalegrp";
            this.bpSalegrp.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpSalegrp.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpSalegrp.SearchCode = true;
            this.bpSalegrp.SelectCount = 0;
            this.bpSalegrp.SetDefaultValue = false;
            this.bpSalegrp.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpSalegrp.Size = new System.Drawing.Size(181, 21);
            this.bpSalegrp.TabIndex = 5;
            this.bpSalegrp.TabStop = false;
            this.bpSalegrp.Text = "bpCodeTextBox1";
            // 
            // bpNm_Sl
            // 
            this.bpNm_Sl.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bpNm_Sl.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpNm_Sl.ButtonImage")));
            this.bpNm_Sl.ChildMode = "";
            this.bpNm_Sl.CodeName = "";
            this.bpNm_Sl.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpNm_Sl.CodeValue = "";
            this.bpNm_Sl.ComboCheck = true;
            this.bpNm_Sl.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB;
            this.bpNm_Sl.IsCodeValueToUpper = true;
            this.bpNm_Sl.ItemBackColor = System.Drawing.Color.White;
            this.bpNm_Sl.Location = new System.Drawing.Point(616, 30);
            this.bpNm_Sl.Name = "bpNm_Sl";
            this.bpNm_Sl.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpNm_Sl.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpNm_Sl.SearchCode = true;
            this.bpNm_Sl.SelectCount = 0;
            this.bpNm_Sl.SetDefaultValue = false;
            this.bpNm_Sl.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpNm_Sl.Size = new System.Drawing.Size(163, 21);
            this.bpNm_Sl.TabIndex = 7;
            this.bpNm_Sl.TabStop = false;
            this.bpNm_Sl.Text = "bpCodeTextBox1";
            this.bpNm_Sl.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            // 
            // m_mskEnd
            // 
            this.m_mskEnd.CalendarBackColor = System.Drawing.Color.White;
            this.m_mskEnd.DayColor = System.Drawing.SystemColors.ControlText;
            this.m_mskEnd.FriDayColor = System.Drawing.Color.Blue;
            this.m_mskEnd.Location = new System.Drawing.Point(181, 4);
            this.m_mskEnd.Mask = "####/##/##";
            this.m_mskEnd.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_mskEnd.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_mskEnd.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_mskEnd.Modified = false;
            this.m_mskEnd.Name = "m_mskEnd";
            this.m_mskEnd.PaddingCharacter = '_';
            this.m_mskEnd.PassivePromptCharacter = '_';
            this.m_mskEnd.PromptCharacter = '_';
            this.m_mskEnd.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_mskEnd.ShowToDay = true;
            this.m_mskEnd.ShowTodayCircle = true;
            this.m_mskEnd.ShowUpDown = false;
            this.m_mskEnd.Size = new System.Drawing.Size(85, 21);
            this.m_mskEnd.SunDayColor = System.Drawing.Color.Red;
            this.m_mskEnd.TabIndex = 1;
            this.m_mskEnd.TabStop = false;
            this.m_mskEnd.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_mskEnd.TitleForeColor = System.Drawing.Color.Black;
            this.m_mskEnd.ToDayColor = System.Drawing.Color.Red;
            this.m_mskEnd.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_mskEnd.UseKeyF3 = false;
            this.m_mskEnd.Value = new System.DateTime(((long)(0)));
            // 
            // m_mskStart
            // 
            this.m_mskStart.CalendarBackColor = System.Drawing.Color.White;
            this.m_mskStart.DayColor = System.Drawing.SystemColors.ControlText;
            this.m_mskStart.FriDayColor = System.Drawing.Color.Blue;
            this.m_mskStart.Location = new System.Drawing.Point(80, 4);
            this.m_mskStart.Mask = "####/##/##";
            this.m_mskStart.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_mskStart.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.m_mskStart.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.m_mskStart.Modified = false;
            this.m_mskStart.Name = "m_mskStart";
            this.m_mskStart.PaddingCharacter = '_';
            this.m_mskStart.PassivePromptCharacter = '_';
            this.m_mskStart.PromptCharacter = '_';
            this.m_mskStart.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.m_mskStart.ShowToDay = true;
            this.m_mskStart.ShowTodayCircle = true;
            this.m_mskStart.ShowUpDown = false;
            this.m_mskStart.Size = new System.Drawing.Size(85, 21);
            this.m_mskStart.SunDayColor = System.Drawing.Color.Red;
            this.m_mskStart.TabIndex = 0;
            this.m_mskStart.TabStop = false;
            this.m_mskStart.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_mskStart.TitleForeColor = System.Drawing.Color.Black;
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
            this.panel6.Size = new System.Drawing.Size(775, 1);
            this.panel6.TabIndex = 25;
            // 
            // m_cdePlant
            // 
            this.m_cdePlant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_cdePlant.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_cdePlant.CodeName = "";
            this.m_cdePlant.CodeValue = "";
            this.m_cdePlant.IgnoreTextChanged = false;
            this.m_cdePlant.IsConfirmed = false;
            this.m_cdePlant.Location = new System.Drawing.Point(350, 3);
            this.m_cdePlant.Name = "m_cdePlant";
            this.m_cdePlant.ReadOnly = true;
            this.m_cdePlant.SelectedAllEnabled = false;
            this.m_cdePlant.Size = new System.Drawing.Size(181, 21);
            this.m_cdePlant.TabIndex = 2;
            this.m_cdePlant.TabStop = false;
            this.m_cdePlant.UseKeyEnter = false;
            this.m_cdePlant.UseKeyF3 = false;
            // 
            // m_cdePartner
            // 
            this.m_cdePartner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_cdePartner.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_cdePartner.CodeName = "";
            this.m_cdePartner.CodeValue = "";
            this.m_cdePartner.IgnoreTextChanged = false;
            this.m_cdePartner.IsConfirmed = false;
            this.m_cdePartner.Location = new System.Drawing.Point(80, 30);
            this.m_cdePartner.Name = "m_cdePartner";
            this.m_cdePartner.ReadOnly = true;
            this.m_cdePartner.SelectedAllEnabled = false;
            this.m_cdePartner.Size = new System.Drawing.Size(186, 21);
            this.m_cdePartner.TabIndex = 4;
            this.m_cdePartner.TabStop = false;
            this.m_cdePartner.UseKeyEnter = false;
            this.m_cdePartner.UseKeyF3 = false;
            // 
            // m_lblDtSo
            // 
            this.m_lblDtSo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblDtSo.Location = new System.Drawing.Point(3, 6);
            this.m_lblDtSo.Name = "m_lblDtSo";
            this.m_lblDtSo.Resizeble = true;
            this.m_lblDtSo.Size = new System.Drawing.Size(70, 18);
            this.m_lblDtSo.TabIndex = 7;
            this.m_lblDtSo.Tag = "";
            this.m_lblDtSo.Text = "수주일자";
            this.m_lblDtSo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(538, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(75, 53);
            this.panel2.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 6);
            this.label1.Name = "label1";
            this.label1.Resizeble = true;
            this.label1.Size = new System.Drawing.Size(58, 18);
            this.label1.TabIndex = 0;
            this.label1.Tag = "";
            this.label1.Text = "거래구분";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 31);
            this.label3.Name = "label3";
            this.label3.Resizeble = true;
            this.label3.Size = new System.Drawing.Size(56, 18);
            this.label3.TabIndex = 0;
            this.label3.Tag = "";
            this.label3.Text = "출하창고";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel1.Controls.Add(this.txt_NoProject);
            this.panel1.Controls.Add(this.lbl_프로젝트);
            this.panel1.Controls.Add(this.m_lblCdPartner);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(75, 82);
            this.panel1.TabIndex = 22;
            // 
            // txt_NoProject
            // 
            this.txt_NoProject.Enabled = false;
            this.txt_NoProject.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txt_NoProject.Location = new System.Drawing.Point(3, 57);
            this.txt_NoProject.MaxLength = 100;
            this.txt_NoProject.Name = "txt_NoProject";
            this.txt_NoProject.SelectedAllEnabled = false;
            this.txt_NoProject.Size = new System.Drawing.Size(17, 21);
            this.txt_NoProject.TabIndex = 207;
            this.txt_NoProject.Tag = "";
            this.txt_NoProject.UseKeyEnter = true;
            this.txt_NoProject.UseKeyF3 = true;
            this.txt_NoProject.Visible = false;
            // 
            // lbl_프로젝트
            // 
            this.lbl_프로젝트.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lbl_프로젝트.Location = new System.Drawing.Point(3, 57);
            this.lbl_프로젝트.Name = "lbl_프로젝트";
            this.lbl_프로젝트.Resizeble = true;
            this.lbl_프로젝트.Size = new System.Drawing.Size(70, 18);
            this.lbl_프로젝트.TabIndex = 1;
            this.lbl_프로젝트.Tag = "프로젝트";
            this.lbl_프로젝트.Text = "프로젝트";
            this.lbl_프로젝트.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblCdPartner
            // 
            this.m_lblCdPartner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblCdPartner.Location = new System.Drawing.Point(3, 31);
            this.m_lblCdPartner.Name = "m_lblCdPartner";
            this.m_lblCdPartner.Resizeble = true;
            this.m_lblCdPartner.Size = new System.Drawing.Size(70, 18);
            this.m_lblCdPartner.TabIndex = 0;
            this.m_lblCdPartner.Tag = "";
            this.m_lblCdPartner.Text = "거래처";
            this.m_lblCdPartner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel7.Controls.Add(this.labelExt1);
            this.panel7.Controls.Add(this.m_lblPlantGir);
            this.panel7.Controls.Add(this.m_lblCdSalegrp);
            this.panel7.Location = new System.Drawing.Point(272, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(75, 82);
            this.panel7.TabIndex = 40;
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt1.Location = new System.Drawing.Point(18, 57);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(54, 18);
            this.labelExt1.TabIndex = 2;
            this.labelExt1.Tag = "";
            this.labelExt1.Text = "담당자";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblPlantGir
            // 
            this.m_lblPlantGir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblPlantGir.Location = new System.Drawing.Point(13, 5);
            this.m_lblPlantGir.Name = "m_lblPlantGir";
            this.m_lblPlantGir.Resizeble = true;
            this.m_lblPlantGir.Size = new System.Drawing.Size(59, 18);
            this.m_lblPlantGir.TabIndex = 1;
            this.m_lblPlantGir.Tag = "";
            this.m_lblPlantGir.Text = "출하공장";
            this.m_lblPlantGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblCdSalegrp
            // 
            this.m_lblCdSalegrp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblCdSalegrp.Location = new System.Drawing.Point(18, 32);
            this.m_lblCdSalegrp.Name = "m_lblCdSalegrp";
            this.m_lblCdSalegrp.Resizeble = true;
            this.m_lblCdSalegrp.Size = new System.Drawing.Size(54, 18);
            this.m_lblCdSalegrp.TabIndex = 0;
            this.m_lblCdSalegrp.Tag = "";
            this.m_lblCdSalegrp.Text = "영업그룹";
            this.m_lblCdSalegrp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_cdeTpBusi
            // 
            this.m_cdeTpBusi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_cdeTpBusi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_cdeTpBusi.CodeName = "";
            this.m_cdeTpBusi.CodeValue = "";
            this.m_cdeTpBusi.IgnoreTextChanged = false;
            this.m_cdeTpBusi.IsConfirmed = false;
            this.m_cdeTpBusi.Location = new System.Drawing.Point(616, 3);
            this.m_cdeTpBusi.Name = "m_cdeTpBusi";
            this.m_cdeTpBusi.ReadOnly = true;
            this.m_cdeTpBusi.SelectedAllEnabled = false;
            this.m_cdeTpBusi.Size = new System.Drawing.Size(163, 21);
            this.m_cdeTpBusi.TabIndex = 3;
            this.m_cdeTpBusi.TabStop = false;
            this.m_cdeTpBusi.UseKeyEnter = false;
            this.m_cdeTpBusi.UseKeyF3 = false;
            // 
            // m_btnQuery
            // 
            this.m_btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnQuery.BackColor = System.Drawing.Color.White;
            this.m_btnQuery.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnQuery.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnQuery.Location = new System.Drawing.Point(614, 142);
            this.m_btnQuery.Name = "m_btnQuery";
            this.m_btnQuery.Size = new System.Drawing.Size(60, 24);
            this.m_btnQuery.TabIndex = 14;
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
            this.m_btnApply.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnApply.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnApply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnApply.Enabled = false;
            this.m_btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnApply.Location = new System.Drawing.Point(675, 142);
            this.m_btnApply.Name = "m_btnApply";
            this.m_btnApply.Size = new System.Drawing.Size(60, 24);
            this.m_btnApply.TabIndex = 15;
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
            this.m_btnCancel.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnCancel.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCancel.Location = new System.Drawing.Point(736, 142);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(60, 24);
            this.m_btnCancel.TabIndex = 16;
            this.m_btnCancel.TabStop = false;
            this.m_btnCancel.Tag = "Q_CANCEL";
            this.m_btnCancel.Text = "취소";
            this.m_btnCancel.UseVisualStyleBackColor = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(10, 172);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._flexH);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._flexL);
            this.splitContainer1.Size = new System.Drawing.Size(785, 442);
            this.splitContainer1.SplitterDistance = 218;
            this.splitContainer1.TabIndex = 17;
            // 
            // _flexH
            // 
            this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexH.AutoResize = false;
            this._flexH.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexH.EnabledHeaderCheck = true;
            this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexH.Location = new System.Drawing.Point(0, 0);
            this._flexH.Name = "_flexH";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 20;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(785, 218);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 0;
            // 
            // _flexL
            // 
            this._flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexL.AutoResize = false;
            this._flexL.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexL.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexL.EnabledHeaderCheck = true;
            this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexL.Location = new System.Drawing.Point(0, 0);
            this._flexL.Name = "_flexL";
            this._flexL.Rows.Count = 1;
            this._flexL.Rows.DefaultSize = 20;
            this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexL.ShowSort = false;
            this._flexL.Size = new System.Drawing.Size(785, 220);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 0;
            // 
            // P_SA_SO_SUB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 626);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.m_btnQuery);
            this.Controls.Add(this.m_btnApply);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Name = "P_SA_SO_SUB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.P_SA_SO_SUB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskStart)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Duzon.Common.Controls.PanelExt panel5;
        private Duzon.Common.Controls.LabelExt m_lblTitle;
        private Duzon.Common.Controls.PanelExt panel4;
        private Duzon.Common.BpControls.BpCodeTextBox bpSalegrp;
        private Duzon.Common.BpControls.BpCodeTextBox bpNm_Sl;
        private Duzon.Common.Controls.DatePicker m_mskEnd;
        private Duzon.Common.Controls.DatePicker m_mskStart;
        private Duzon.Common.Controls.PanelExt panel6;
        private Duzon.Common.Controls.CodeTextBox m_cdePlant;
        private Duzon.Common.Controls.CodeTextBox m_cdePartner;
        private Duzon.Common.Controls.LabelExt m_lblDtSo;
        private Duzon.Common.Controls.LabelExt label6;
        private Duzon.Common.Controls.PanelExt panel2;
        private Duzon.Common.Controls.LabelExt label1;
        private Duzon.Common.Controls.PanelExt panel1;
        private Duzon.Common.Controls.LabelExt m_lblCdPartner;
        private Duzon.Common.Controls.LabelExt label3;
        private Duzon.Common.Controls.PanelExt panel7;
        private Duzon.Common.Controls.LabelExt m_lblCdSalegrp;
        private Duzon.Common.Controls.CodeTextBox m_cdeTpBusi;
        private Duzon.Common.Controls.RoundedButton m_btnQuery;
        private Duzon.Common.Controls.RoundedButton m_btnApply;
        private Duzon.Common.Controls.RoundedButton m_btnCancel;
        private Duzon.Common.Controls.LabelExt m_lblPlantGir;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.LabelExt lbl_프로젝트;
        private Duzon.Common.Controls.TextButton bp_Project;
        private Duzon.Common.Controls.TextBoxExt txt_NoProject;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.BpControls.BpCodeTextBox bp_Emp;
    }
}

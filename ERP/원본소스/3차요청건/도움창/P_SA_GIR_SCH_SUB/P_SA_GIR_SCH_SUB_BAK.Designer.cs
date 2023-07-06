namespace sale
{
    partial class P_SA_GIR_SCH_SUB
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_GIR_SCH_SUB));
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.m_lblTitle = new Duzon.Common.Controls.LabelExt();
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.panelExt5 = new Duzon.Common.Controls.PanelExt();
            this.labelExt2 = new Duzon.Common.Controls.LabelExt();
            this.m_txtYnReturn = new Duzon.Common.Controls.TextBoxExt();
            this.panelExt4 = new Duzon.Common.Controls.PanelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.bpNm_Partner = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_mskEnd = new Duzon.Common.Controls.DatePicker();
            this.m_mskStart = new Duzon.Common.Controls.DatePicker();
            this.m_cboPlantGir = new Duzon.Common.Controls.DropDownComboBox();
            this.label6 = new Duzon.Common.Controls.LabelExt();
            this.panel2 = new Duzon.Common.Controls.PanelExt();
            this.m_lblEmpGir = new Duzon.Common.Controls.LabelExt();
            this.panel1 = new Duzon.Common.Controls.PanelExt();
            this.m_lblDtGir = new Duzon.Common.Controls.LabelExt();
            this.m_btnQuery = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnCancel = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnConfirm = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelExt5.SuspendLayout();
            this.panelExt4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskStart)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelExt1.SuspendLayout();
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
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
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
            this.m_lblTitle.Size = new System.Drawing.Size(97, 14);
            this.m_lblTitle.TabIndex = 0;
            this.m_lblTitle.Tag = "납품의뢰조회";
            this.m_lblTitle.Text = "납품의뢰조회";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panelExt5);
            this.panel4.Controls.Add(this.m_txtYnReturn);
            this.panel4.Controls.Add(this.panelExt4);
            this.panel4.Controls.Add(this.bpNm_Partner);
            this.panel4.Controls.Add(this.m_mskEnd);
            this.panel4.Controls.Add(this.m_mskStart);
            this.panel4.Controls.Add(this.m_cboPlantGir);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(10, 55);
            this.panel4.Margin = new System.Windows.Forms.Padding(10, 8, 10, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(784, 31);
            this.panel4.TabIndex = 2;
            // 
            // panelExt5
            // 
            this.panelExt5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt5.Controls.Add(this.labelExt2);
            this.panelExt5.Location = new System.Drawing.Point(673, 1);
            this.panelExt5.Name = "panelExt5";
            this.panelExt5.Size = new System.Drawing.Size(65, 27);
            this.panelExt5.TabIndex = 43;
            // 
            // labelExt2
            // 
            this.labelExt2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt2.Location = new System.Drawing.Point(7, 5);
            this.labelExt2.Name = "labelExt2";
            this.labelExt2.Resizeble = true;
            this.labelExt2.Size = new System.Drawing.Size(55, 18);
            this.labelExt2.TabIndex = 2;
            this.labelExt2.Tag = "";
            this.labelExt2.Text = "반품여부";
            this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtYnReturn
            // 
            this.m_txtYnReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_txtYnReturn.Location = new System.Drawing.Point(744, 4);
            this.m_txtYnReturn.Name = "m_txtYnReturn";
            this.m_txtYnReturn.ReadOnly = true;
            this.m_txtYnReturn.SelectedAllEnabled = false;
            this.m_txtYnReturn.Size = new System.Drawing.Size(32, 21);
            this.m_txtYnReturn.TabIndex = 42;
            this.m_txtYnReturn.TabStop = false;
            this.m_txtYnReturn.Tag = "IV";
            this.m_txtYnReturn.UseKeyEnter = false;
            this.m_txtYnReturn.UseKeyF3 = false;
            // 
            // panelExt4
            // 
            this.panelExt4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt4.Controls.Add(this.labelExt1);
            this.panelExt4.Location = new System.Drawing.Point(480, 1);
            this.panelExt4.Name = "panelExt4";
            this.panelExt4.Size = new System.Drawing.Size(65, 27);
            this.panelExt4.TabIndex = 5;
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt1.Location = new System.Drawing.Point(7, 5);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(55, 18);
            this.labelExt1.TabIndex = 2;
            this.labelExt1.Tag = "출하공장";
            this.labelExt1.Text = "출하공장";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpNm_Partner
            // 
            this.bpNm_Partner.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bpNm_Partner.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpNm_Partner.ButtonImage")));
            this.bpNm_Partner.ChildMode = "";
            this.bpNm_Partner.CodeName = "";
            this.bpNm_Partner.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpNm_Partner.CodeValue = "";
            this.bpNm_Partner.ComboCheck = true;
            this.bpNm_Partner.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpNm_Partner.ItemBackColor = System.Drawing.Color.White;
            this.bpNm_Partner.Location = new System.Drawing.Point(340, 4);
            this.bpNm_Partner.Name = "bpNm_Partner";
            this.bpNm_Partner.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpNm_Partner.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bpNm_Partner.SearchCode = true;
            this.bpNm_Partner.SelectCount = 0;
            this.bpNm_Partner.SetDefaultValue = false;
            this.bpNm_Partner.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpNm_Partner.Size = new System.Drawing.Size(134, 21);
            this.bpNm_Partner.TabIndex = 3;
            this.bpNm_Partner.TabStop = false;
            this.bpNm_Partner.Text = "bpCodeTextBox1";
            // 
            // m_mskEnd
            // 
            this.m_mskEnd.CalendarBackColor = System.Drawing.Color.White;
            this.m_mskEnd.DayColor = System.Drawing.SystemColors.ControlText;
            this.m_mskEnd.FriDayColor = System.Drawing.Color.Blue;
            this.m_mskEnd.Location = new System.Drawing.Point(176, 4);
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
            this.m_mskEnd.Size = new System.Drawing.Size(90, 21);
            this.m_mskEnd.SunDayColor = System.Drawing.Color.Red;
            this.m_mskEnd.TabIndex = 2;
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
            this.m_mskStart.Location = new System.Drawing.Point(69, 4);
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
            this.m_mskStart.Size = new System.Drawing.Size(90, 21);
            this.m_mskStart.SunDayColor = System.Drawing.Color.Red;
            this.m_mskStart.TabIndex = 1;
            this.m_mskStart.TitleBackColor = System.Drawing.SystemColors.Control;
            this.m_mskStart.TitleForeColor = System.Drawing.Color.Black;
            this.m_mskStart.ToDayColor = System.Drawing.Color.Red;
            this.m_mskStart.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.m_mskStart.UseKeyF3 = false;
            this.m_mskStart.Value = new System.DateTime(((long)(0)));
            // 
            // m_cboPlantGir
            // 
            this.m_cboPlantGir.AutoDropDown = true;
            this.m_cboPlantGir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboPlantGir.Location = new System.Drawing.Point(548, 4);
            this.m_cboPlantGir.Name = "m_cboPlantGir";
            this.m_cboPlantGir.ShowCheckBox = false;
            this.m_cboPlantGir.Size = new System.Drawing.Size(119, 20);
            this.m_cboPlantGir.TabIndex = 4;
            this.m_cboPlantGir.UseKeyEnter = false;
            this.m_cboPlantGir.UseKeyF3 = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(159, 8);
            this.label6.Name = "label6";
            this.label6.Resizeble = true;
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "∼";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel2.Controls.Add(this.m_lblEmpGir);
            this.panel2.Location = new System.Drawing.Point(272, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(65, 27);
            this.panel2.TabIndex = 0;
            // 
            // m_lblEmpGir
            // 
            this.m_lblEmpGir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblEmpGir.Location = new System.Drawing.Point(6, 6);
            this.m_lblEmpGir.Name = "m_lblEmpGir";
            this.m_lblEmpGir.Resizeble = true;
            this.m_lblEmpGir.Size = new System.Drawing.Size(56, 18);
            this.m_lblEmpGir.TabIndex = 0;
            this.m_lblEmpGir.Tag = "거래처";
            this.m_lblEmpGir.Text = "거래처";
            this.m_lblEmpGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel1.Controls.Add(this.m_lblDtGir);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(65, 27);
            this.panel1.TabIndex = 0;
            // 
            // m_lblDtGir
            // 
            this.m_lblDtGir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblDtGir.Location = new System.Drawing.Point(5, 5);
            this.m_lblDtGir.Name = "m_lblDtGir";
            this.m_lblDtGir.Resizeble = true;
            this.m_lblDtGir.Size = new System.Drawing.Size(58, 18);
            this.m_lblDtGir.TabIndex = 2;
            this.m_lblDtGir.Tag = "의뢰일자";
            this.m_lblDtGir.Text = "의뢰일자";
            this.m_lblDtGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_btnQuery
            // 
            this.m_btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnQuery.BackColor = System.Drawing.Color.White;
            this.m_btnQuery.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnQuery.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnQuery.Location = new System.Drawing.Point(603, 2);
            this.m_btnQuery.Name = "m_btnQuery";
            this.m_btnQuery.Size = new System.Drawing.Size(60, 22);
            this.m_btnQuery.TabIndex = 11;
            this.m_btnQuery.TabStop = false;
            this.m_btnQuery.Tag = "조회";
            this.m_btnQuery.Text = "조회";
            this.m_btnQuery.UseVisualStyleBackColor = false;
            this.m_btnQuery.Click += new System.EventHandler(this.OnSearchButtonClicked);
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
            this.m_btnCancel.Location = new System.Drawing.Point(723, 2);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(60, 22);
            this.m_btnCancel.TabIndex = 10;
            this.m_btnCancel.TabStop = false;
            this.m_btnCancel.Tag = "취소";
            this.m_btnCancel.Text = "취소";
            this.m_btnCancel.UseVisualStyleBackColor = false;
            // 
            // m_btnConfirm
            // 
            this.m_btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnConfirm.BackColor = System.Drawing.Color.White;
            this.m_btnConfirm.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.m_btnConfirm.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.m_btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnConfirm.Enabled = false;
            this.m_btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnConfirm.Location = new System.Drawing.Point(663, 2);
            this.m_btnConfirm.Name = "m_btnConfirm";
            this.m_btnConfirm.Size = new System.Drawing.Size(60, 22);
            this.m_btnConfirm.TabIndex = 9;
            this.m_btnConfirm.TabStop = false;
            this.m_btnConfirm.Tag = "";
            this.m_btnConfirm.Text = "적용";
            this.m_btnConfirm.UseVisualStyleBackColor = false;
            this.m_btnConfirm.Click += new System.EventHandler(this.OnApply);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelExt1, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(804, 616);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // panelExt1
            // 
            this.panelExt1.Controls.Add(this.m_btnQuery);
            this.panelExt1.Controls.Add(this.m_btnConfirm);
            this.panelExt1.Controls.Add(this.m_btnCancel);
            this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt1.Location = new System.Drawing.Point(10, 92);
            this.panelExt1.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(784, 26);
            this.panelExt1.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(10, 124);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
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
            this.splitContainer1.Size = new System.Drawing.Size(784, 489);
            this.splitContainer1.SplitterDistance = 271;
            this.splitContainer1.TabIndex = 4;
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
            this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexH.Location = new System.Drawing.Point(0, 0);
            this._flexH.Margin = new System.Windows.Forms.Padding(0);
            this._flexH.Name = "_flexH";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 18;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(784, 271);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 1;
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
            this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexL.Location = new System.Drawing.Point(0, 0);
            this._flexL.Margin = new System.Windows.Forms.Padding(0);
            this._flexL.Name = "_flexL";
            this._flexL.Rows.Count = 1;
            this._flexL.Rows.DefaultSize = 18;
            this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexL.ShowSort = false;
            this._flexL.Size = new System.Drawing.Size(784, 214);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 1;
            // 
            // P_SA_GIR_SCH_SUB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 626);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_SA_GIR_SCH_SUB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panelExt5.ResumeLayout(false);
            this.panelExt4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_mskEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_mskStart)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelExt1.ResumeLayout(false);
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
        private Duzon.Common.BpControls.BpCodeTextBox bpNm_Partner;
        private Duzon.Common.Controls.DatePicker m_mskEnd;
        private Duzon.Common.Controls.DatePicker m_mskStart;
        private Duzon.Common.Controls.DropDownComboBox m_cboPlantGir;
        private Duzon.Common.Controls.LabelExt label6;
        private Duzon.Common.Controls.PanelExt panel2;
        private Duzon.Common.Controls.LabelExt m_lblEmpGir;
        private Duzon.Common.Controls.PanelExt panel1;
        private Duzon.Common.Controls.RoundedButton m_btnQuery;
        private Duzon.Common.Controls.RoundedButton m_btnCancel;
        private Duzon.Common.Controls.RoundedButton m_btnConfirm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.LabelExt m_lblDtGir;
        private Duzon.Common.Controls.PanelExt panelExt4;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.PanelExt panelExt5;
        private Duzon.Common.Controls.LabelExt labelExt2;
        private Duzon.Common.Controls.TextBoxExt m_txtYnReturn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexL;
    }
}
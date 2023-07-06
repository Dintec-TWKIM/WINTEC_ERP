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
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.bp프로젝트 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.panelExt3 = new Duzon.Common.Controls.PanelExt();
            this.panelExt5 = new Duzon.Common.Controls.PanelExt();
            this.lbl_반품여부 = new Duzon.Common.Controls.LabelExt();
            this.txt_Return = new Duzon.Common.Controls.TextBoxExt();
            this.panelExt4 = new Duzon.Common.Controls.PanelExt();
            this.lbl_출하공장 = new Duzon.Common.Controls.LabelExt();
            this.bp_Partner = new Duzon.Common.BpControls.BpCodeTextBox();
            this.dt_Gir_To = new Duzon.Common.Controls.DatePicker();
            this.dt_Gir_From = new Duzon.Common.Controls.DatePicker();
            this.cbo_Plant = new Duzon.Common.Controls.DropDownComboBox();
            this.label6 = new Duzon.Common.Controls.LabelExt();
            this.panel2 = new Duzon.Common.Controls.PanelExt();
            this.lbl_거래처 = new Duzon.Common.Controls.LabelExt();
            this.panel1 = new Duzon.Common.Controls.PanelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.lbl_의뢰일자 = new Duzon.Common.Controls.LabelExt();
            this.btn_Search = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_Cancel = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn_Append = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.panel4.SuspendLayout();
            this.panelExt5.SuspendLayout();
            this.panelExt4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_Gir_To)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_Gir_From)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.panelExt1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.bp프로젝트);
            this.panel4.Controls.Add(this.panelExt3);
            this.panel4.Controls.Add(this.panelExt5);
            this.panel4.Controls.Add(this.txt_Return);
            this.panel4.Controls.Add(this.panelExt4);
            this.panel4.Controls.Add(this.bp_Partner);
            this.panel4.Controls.Add(this.dt_Gir_To);
            this.panel4.Controls.Add(this.dt_Gir_From);
            this.panel4.Controls.Add(this.cbo_Plant);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(798, 58);
            this.panel4.TabIndex = 2;
            // 
            // bp프로젝트
            // 
            this.bp프로젝트.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bp프로젝트.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp프로젝트.ButtonImage")));
            this.bp프로젝트.ChildMode = "";
            this.bp프로젝트.CodeName = "";
            this.bp프로젝트.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp프로젝트.CodeValue = "";
            this.bp프로젝트.ComboCheck = true;
            this.bp프로젝트.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.bp프로젝트.IsCodeValueToUpper = true;
            this.bp프로젝트.ItemBackColor = System.Drawing.Color.White;
            this.bp프로젝트.Location = new System.Drawing.Point(69, 32);
            this.bp프로젝트.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp프로젝트.Name = "bp프로젝트";
            this.bp프로젝트.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp프로젝트.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bp프로젝트.SearchCode = true;
            this.bp프로젝트.SelectCount = 0;
            this.bp프로젝트.SetDefaultValue = false;
            this.bp프로젝트.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp프로젝트.Size = new System.Drawing.Size(197, 21);
            this.bp프로젝트.TabIndex = 85;
            this.bp프로젝트.TabStop = false;
            this.bp프로젝트.Text = "bpCodeTextBox1";
            this.bp프로젝트.UserCodeName = "NM_PROJECT";
            this.bp프로젝트.UserCodeValue = "NO_PROJECT";
            this.bp프로젝트.UserHelpID = "H_SA_PRJ_SUB";
            // 
            // panelExt3
            // 
            this.panelExt3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt3.BackColor = System.Drawing.Color.Transparent;
            this.panelExt3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt3.BackgroundImage")));
            this.panelExt3.Location = new System.Drawing.Point(2, 29);
            this.panelExt3.Name = "panelExt3";
            this.panelExt3.Size = new System.Drawing.Size(793, 1);
            this.panelExt3.TabIndex = 84;
            // 
            // panelExt5
            // 
            this.panelExt5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt5.Controls.Add(this.lbl_반품여부);
            this.panelExt5.Location = new System.Drawing.Point(673, 1);
            this.panelExt5.Name = "panelExt5";
            this.panelExt5.Size = new System.Drawing.Size(65, 27);
            this.panelExt5.TabIndex = 43;
            // 
            // lbl_반품여부
            // 
            this.lbl_반품여부.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lbl_반품여부.Location = new System.Drawing.Point(7, 5);
            this.lbl_반품여부.Name = "lbl_반품여부";
            this.lbl_반품여부.Resizeble = true;
            this.lbl_반품여부.Size = new System.Drawing.Size(55, 18);
            this.lbl_반품여부.TabIndex = 2;
            this.lbl_반품여부.Tag = "";
            this.lbl_반품여부.Text = "반품여부";
            this.lbl_반품여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_Return
            // 
            this.txt_Return.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.txt_Return.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt_Return.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Return.Location = new System.Drawing.Point(744, 4);
            this.txt_Return.Name = "txt_Return";
            this.txt_Return.ReadOnly = true;
            this.txt_Return.SelectedAllEnabled = false;
            this.txt_Return.Size = new System.Drawing.Size(32, 21);
            this.txt_Return.TabIndex = 42;
            this.txt_Return.TabStop = false;
            this.txt_Return.Tag = "IV";
            this.txt_Return.UseKeyEnter = false;
            this.txt_Return.UseKeyF3 = false;
            // 
            // panelExt4
            // 
            this.panelExt4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt4.Controls.Add(this.lbl_출하공장);
            this.panelExt4.Location = new System.Drawing.Point(480, 1);
            this.panelExt4.Name = "panelExt4";
            this.panelExt4.Size = new System.Drawing.Size(65, 27);
            this.panelExt4.TabIndex = 5;
            // 
            // lbl_출하공장
            // 
            this.lbl_출하공장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lbl_출하공장.Location = new System.Drawing.Point(7, 5);
            this.lbl_출하공장.Name = "lbl_출하공장";
            this.lbl_출하공장.Resizeble = true;
            this.lbl_출하공장.Size = new System.Drawing.Size(55, 18);
            this.lbl_출하공장.TabIndex = 2;
            this.lbl_출하공장.Tag = "출하공장";
            this.lbl_출하공장.Text = "출하공장";
            this.lbl_출하공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bp_Partner
            // 
            this.bp_Partner.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bp_Partner.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_Partner.ButtonImage")));
            this.bp_Partner.ChildMode = "";
            this.bp_Partner.CodeName = "";
            this.bp_Partner.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp_Partner.CodeValue = "";
            this.bp_Partner.ComboCheck = true;
            this.bp_Partner.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bp_Partner.IsCodeValueToUpper = true;
            this.bp_Partner.ItemBackColor = System.Drawing.Color.White;
            this.bp_Partner.Location = new System.Drawing.Point(340, 4);
            this.bp_Partner.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp_Partner.Name = "bp_Partner";
            this.bp_Partner.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp_Partner.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bp_Partner.SearchCode = true;
            this.bp_Partner.SelectCount = 0;
            this.bp_Partner.SetDefaultValue = false;
            this.bp_Partner.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp_Partner.Size = new System.Drawing.Size(134, 21);
            this.bp_Partner.TabIndex = 3;
            this.bp_Partner.TabStop = false;
            this.bp_Partner.Text = "bpCodeTextBox1";
            // 
            // dt_Gir_To
            // 
            this.dt_Gir_To.CalendarBackColor = System.Drawing.Color.White;
            this.dt_Gir_To.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dt_Gir_To.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dt_Gir_To.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dt_Gir_To.Location = new System.Drawing.Point(176, 4);
            this.dt_Gir_To.Mask = "####/##/##";
            this.dt_Gir_To.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dt_Gir_To.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dt_Gir_To.MaximumSize = new System.Drawing.Size(0, 21);
            this.dt_Gir_To.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dt_Gir_To.Modified = false;
            this.dt_Gir_To.Name = "dt_Gir_To";
            this.dt_Gir_To.PaddingCharacter = '_';
            this.dt_Gir_To.PassivePromptCharacter = '_';
            this.dt_Gir_To.PromptCharacter = '_';
            this.dt_Gir_To.SelectedDayColor = System.Drawing.Color.White;
            this.dt_Gir_To.ShowToDay = true;
            this.dt_Gir_To.ShowTodayCircle = true;
            this.dt_Gir_To.ShowUpDown = false;
            this.dt_Gir_To.Size = new System.Drawing.Size(90, 21);
            this.dt_Gir_To.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dt_Gir_To.TabIndex = 2;
            this.dt_Gir_To.TitleBackColor = System.Drawing.Color.White;
            this.dt_Gir_To.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dt_Gir_To.ToDayColor = System.Drawing.Color.Red;
            this.dt_Gir_To.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dt_Gir_To.UseKeyF3 = false;
            this.dt_Gir_To.Value = new System.DateTime(((long)(0)));
            // 
            // dt_Gir_From
            // 
            this.dt_Gir_From.CalendarBackColor = System.Drawing.Color.White;
            this.dt_Gir_From.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dt_Gir_From.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dt_Gir_From.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dt_Gir_From.Location = new System.Drawing.Point(69, 4);
            this.dt_Gir_From.Mask = "####/##/##";
            this.dt_Gir_From.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dt_Gir_From.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dt_Gir_From.MaximumSize = new System.Drawing.Size(0, 21);
            this.dt_Gir_From.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dt_Gir_From.Modified = false;
            this.dt_Gir_From.Name = "dt_Gir_From";
            this.dt_Gir_From.PaddingCharacter = '_';
            this.dt_Gir_From.PassivePromptCharacter = '_';
            this.dt_Gir_From.PromptCharacter = '_';
            this.dt_Gir_From.SelectedDayColor = System.Drawing.Color.White;
            this.dt_Gir_From.ShowToDay = true;
            this.dt_Gir_From.ShowTodayCircle = true;
            this.dt_Gir_From.ShowUpDown = false;
            this.dt_Gir_From.Size = new System.Drawing.Size(90, 21);
            this.dt_Gir_From.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dt_Gir_From.TabIndex = 1;
            this.dt_Gir_From.TitleBackColor = System.Drawing.Color.White;
            this.dt_Gir_From.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dt_Gir_From.ToDayColor = System.Drawing.Color.Red;
            this.dt_Gir_From.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dt_Gir_From.UseKeyF3 = false;
            this.dt_Gir_From.Value = new System.DateTime(((long)(0)));
            // 
            // cbo_Plant
            // 
            this.cbo_Plant.AutoDropDown = true;
            this.cbo_Plant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Plant.ItemHeight = 12;
            this.cbo_Plant.Location = new System.Drawing.Point(548, 4);
            this.cbo_Plant.Name = "cbo_Plant";
            this.cbo_Plant.ShowCheckBox = false;
            this.cbo_Plant.Size = new System.Drawing.Size(119, 20);
            this.cbo_Plant.TabIndex = 4;
            this.cbo_Plant.UseKeyEnter = false;
            this.cbo_Plant.UseKeyF3 = true;
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
            this.panel2.Controls.Add(this.lbl_거래처);
            this.panel2.Location = new System.Drawing.Point(272, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(65, 27);
            this.panel2.TabIndex = 0;
            // 
            // lbl_거래처
            // 
            this.lbl_거래처.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lbl_거래처.Location = new System.Drawing.Point(6, 6);
            this.lbl_거래처.Name = "lbl_거래처";
            this.lbl_거래처.Resizeble = true;
            this.lbl_거래처.Size = new System.Drawing.Size(56, 18);
            this.lbl_거래처.TabIndex = 0;
            this.lbl_거래처.Tag = "거래처";
            this.lbl_거래처.Text = "거래처";
            this.lbl_거래처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel1.Controls.Add(this.labelExt1);
            this.panel1.Controls.Add(this.lbl_의뢰일자);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(65, 54);
            this.panel1.TabIndex = 0;
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt1.Location = new System.Drawing.Point(5, 33);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(58, 18);
            this.labelExt1.TabIndex = 3;
            this.labelExt1.Tag = "";
            this.labelExt1.Text = "프로젝트";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_의뢰일자
            // 
            this.lbl_의뢰일자.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lbl_의뢰일자.Location = new System.Drawing.Point(5, 5);
            this.lbl_의뢰일자.Name = "lbl_의뢰일자";
            this.lbl_의뢰일자.Resizeble = true;
            this.lbl_의뢰일자.Size = new System.Drawing.Size(58, 18);
            this.lbl_의뢰일자.TabIndex = 2;
            this.lbl_의뢰일자.Tag = "의뢰일자";
            this.lbl_의뢰일자.Text = "의뢰일자";
            this.lbl_의뢰일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.BackColor = System.Drawing.Color.White;
            this.btn_Search.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Search.Location = new System.Drawing.Point(611, 2);
            this.btn_Search.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(60, 19);
            this.btn_Search.TabIndex = 11;
            this.btn_Search.TabStop = false;
            this.btn_Search.Tag = "조회";
            this.btn_Search.Text = "조회";
            this.btn_Search.UseVisualStyleBackColor = false;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.BackColor = System.Drawing.Color.White;
            this.btn_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Location = new System.Drawing.Point(735, 2);
            this.btn_Cancel.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(60, 19);
            this.btn_Cancel.TabIndex = 10;
            this.btn_Cancel.TabStop = false;
            this.btn_Cancel.Tag = "취소";
            this.btn_Cancel.Text = "취소";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            // 
            // btn_Append
            // 
            this.btn_Append.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Append.BackColor = System.Drawing.Color.White;
            this.btn_Append.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Append.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Append.Location = new System.Drawing.Point(673, 2);
            this.btn_Append.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_Append.Name = "btn_Append";
            this.btn_Append.Size = new System.Drawing.Size(60, 19);
            this.btn_Append.TabIndex = 9;
            this.btn_Append.TabStop = false;
            this.btn_Append.Tag = "";
            this.btn_Append.Text = "적용";
            this.btn_Append.UseVisualStyleBackColor = false;
            this.btn_Append.Click += new System.EventHandler(this.btn_Append_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelExt1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 48);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(804, 572);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 99);
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
            this.splitContainer1.Size = new System.Drawing.Size(798, 470);
            this.splitContainer1.SplitterDistance = 260;
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
            this._flexH.Font = new System.Drawing.Font("굴림", 9F);
            this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexH.Location = new System.Drawing.Point(0, 0);
            this._flexH.Margin = new System.Windows.Forms.Padding(0);
            this._flexH.Name = "_flexH";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 18;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(798, 260);
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
            this._flexL.Font = new System.Drawing.Font("굴림", 9F);
            this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexL.Location = new System.Drawing.Point(0, 0);
            this._flexL.Margin = new System.Windows.Forms.Padding(0);
            this._flexL.Name = "_flexL";
            this._flexL.Rows.Count = 1;
            this._flexL.Rows.DefaultSize = 18;
            this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexL.ShowSort = false;
            this._flexL.Size = new System.Drawing.Size(798, 206);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 1;
            // 
            // panelExt1
            // 
            this.panelExt1.Controls.Add(this.btn_Search);
            this.panelExt1.Controls.Add(this.btn_Append);
            this.panelExt1.Controls.Add(this.btn_Cancel);
            this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt1.Location = new System.Drawing.Point(3, 67);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(798, 26);
            this.panelExt1.TabIndex = 3;
            // 
            // P_SA_GIR_SCH_SUB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(804, 623);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_SA_GIR_SCH_SUB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TitleText = "납품의뢰조회";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panelExt5.ResumeLayout(false);
            this.panelExt4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dt_Gir_To)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_Gir_From)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.panelExt1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Duzon.Common.Controls.PanelExt panel4;
        private Duzon.Common.BpControls.BpCodeTextBox bp_Partner;
        private Duzon.Common.Controls.DatePicker dt_Gir_To;
        private Duzon.Common.Controls.DatePicker dt_Gir_From;
        private Duzon.Common.Controls.DropDownComboBox cbo_Plant;
        private Duzon.Common.Controls.LabelExt label6;
        private Duzon.Common.Controls.PanelExt panel2;
        private Duzon.Common.Controls.LabelExt lbl_거래처;
        private Duzon.Common.Controls.PanelExt panel1;
        private Duzon.Common.Controls.RoundedButton btn_Search;
        private Duzon.Common.Controls.RoundedButton btn_Cancel;
        private Duzon.Common.Controls.RoundedButton btn_Append;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.LabelExt lbl_의뢰일자;
        private Duzon.Common.Controls.PanelExt panelExt4;
        private Duzon.Common.Controls.LabelExt lbl_출하공장;
        private Duzon.Common.Controls.PanelExt panelExt5;
        private Duzon.Common.Controls.LabelExt lbl_반품여부;
        private Duzon.Common.Controls.TextBoxExt txt_Return;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.BpControls.BpCodeTextBox bp프로젝트;
        private Duzon.Common.Controls.PanelExt panelExt3;
    }
}
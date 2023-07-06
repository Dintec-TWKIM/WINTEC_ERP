namespace trade
{
    partial class P_TR_EXBL_MNG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_TR_EXBL_MNG));
            this.tblLayout = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.bp프로젝트 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.cbo전표처리여부 = new Duzon.Common.Controls.DropDownComboBox();
            this.dtp기표기간TO = new Duzon.Common.Controls.DatePicker();
            this.dtp기표기간FR = new Duzon.Common.Controls.DatePicker();
            this.bpc담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc영업그룹 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc거래처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.panel9 = new Duzon.Common.Controls.PanelExt();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.label7 = new Duzon.Common.Controls.LabelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.label4 = new Duzon.Common.Controls.LabelExt();
            this.label5 = new Duzon.Common.Controls.LabelExt();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.lbl프로젝트 = new Duzon.Common.Controls.LabelExt();
            this.label3 = new Duzon.Common.Controls.LabelExt();
            this.label2 = new Duzon.Common.Controls.LabelExt();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btn전표발행취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn전표발행 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.bpBizarea = new Duzon.Common.BpControls.BpComboBox();
            this.mDataArea.SuspendLayout();
            this.tblLayout.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp기표기간TO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp기표기간FR)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tblLayout);
            // 
            // tblLayout
            // 
            this.tblLayout.ColumnCount = 1;
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblLayout.Controls.Add(this.panel5, 0, 0);
            this.tblLayout.Controls.Add(this.splitContainer1, 0, 1);
            this.tblLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayout.Location = new System.Drawing.Point(0, 0);
            this.tblLayout.Name = "tblLayout";
            this.tblLayout.RowCount = 2;
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayout.Size = new System.Drawing.Size(827, 579);
            this.tblLayout.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.bpBizarea);
            this.panel5.Controls.Add(this.bp프로젝트);
            this.panel5.Controls.Add(this.panelExt1);
            this.panel5.Controls.Add(this.cbo전표처리여부);
            this.panel5.Controls.Add(this.dtp기표기간TO);
            this.panel5.Controls.Add(this.dtp기표기간FR);
            this.panel5.Controls.Add(this.bpc담당자);
            this.panel5.Controls.Add(this.bpc영업그룹);
            this.panel5.Controls.Add(this.bpc거래처);
            this.panel5.Controls.Add(this.panel9);
            this.panel5.Controls.Add(this.panel8);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(821, 81);
            this.panel5.TabIndex = 133;
            // 
            // bp프로젝트
            // 
            this.bp프로젝트.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp프로젝트.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp프로젝트.ButtonImage")));
            this.bp프로젝트.ChildMode = "";
            this.bp프로젝트.CodeName = "";
            this.bp프로젝트.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp프로젝트.CodeValue = "";
            this.bp프로젝트.ComboCheck = true;
            this.bp프로젝트.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.bp프로젝트.IsCodeValueToUpper = true;
            this.bp프로젝트.ItemBackColor = System.Drawing.Color.White;
            this.bp프로젝트.Location = new System.Drawing.Point(80, 56);
            this.bp프로젝트.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp프로젝트.Name = "bp프로젝트";
            this.bp프로젝트.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp프로젝트.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bp프로젝트.SearchCode = true;
            this.bp프로젝트.SelectCount = 0;
            this.bp프로젝트.SetDefaultValue = false;
            this.bp프로젝트.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp프로젝트.Size = new System.Drawing.Size(162, 21);
            this.bp프로젝트.TabIndex = 147;
            this.bp프로젝트.TabStop = false;
            this.bp프로젝트.Tag = "CD_PJT;NM_PJT";
            this.bp프로젝트.UserCodeName = "NM_PROJECT";
            this.bp프로젝트.UserCodeValue = "NO_PROJECT";
            this.bp프로젝트.UserHelpID = "H_SA_PRJ_SUB";
            this.bp프로젝트.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            // 
            // panelExt1
            // 
            this.panelExt1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt1.BackColor = System.Drawing.Color.Transparent;
            this.panelExt1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt1.BackgroundImage")));
            this.panelExt1.Location = new System.Drawing.Point(-1, 53);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(811, 1);
            this.panelExt1.TabIndex = 4;
            // 
            // cbo전표처리여부
            // 
            this.cbo전표처리여부.AutoDropDown = true;
            this.cbo전표처리여부.BackColor = System.Drawing.Color.White;
            this.cbo전표처리여부.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo전표처리여부.ItemHeight = 12;
            this.cbo전표처리여부.Location = new System.Drawing.Point(582, 31);
            this.cbo전표처리여부.Name = "cbo전표처리여부";
            this.cbo전표처리여부.ShowCheckBox = false;
            this.cbo전표처리여부.Size = new System.Drawing.Size(173, 20);
            this.cbo전표처리여부.TabIndex = 146;
            this.cbo전표처리여부.UseKeyEnter = false;
            this.cbo전표처리여부.UseKeyF3 = false;
            // 
            // dtp기표기간TO
            // 
            this.dtp기표기간TO.CalendarBackColor = System.Drawing.Color.White;
            this.dtp기표기간TO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp기표기간TO.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp기표기간TO.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp기표기간TO.Location = new System.Drawing.Point(414, 3);
            this.dtp기표기간TO.Mask = "####/##/##";
            this.dtp기표기간TO.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp기표기간TO.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp기표기간TO.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp기표기간TO.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp기표기간TO.Modified = false;
            this.dtp기표기간TO.Name = "dtp기표기간TO";
            this.dtp기표기간TO.PaddingCharacter = '_';
            this.dtp기표기간TO.PassivePromptCharacter = '_';
            this.dtp기표기간TO.PromptCharacter = '_';
            this.dtp기표기간TO.SelectedDayColor = System.Drawing.Color.White;
            this.dtp기표기간TO.ShowToDay = true;
            this.dtp기표기간TO.ShowTodayCircle = true;
            this.dtp기표기간TO.ShowUpDown = false;
            this.dtp기표기간TO.Size = new System.Drawing.Size(85, 21);
            this.dtp기표기간TO.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp기표기간TO.TabIndex = 144;
            this.dtp기표기간TO.TitleBackColor = System.Drawing.Color.White;
            this.dtp기표기간TO.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp기표기간TO.ToDayColor = System.Drawing.Color.Red;
            this.dtp기표기간TO.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp기표기간TO.UseKeyF3 = false;
            this.dtp기표기간TO.Value = new System.DateTime(2006, 1, 1, 0, 0, 0, 0);
            // 
            // dtp기표기간FR
            // 
            this.dtp기표기간FR.CalendarBackColor = System.Drawing.Color.White;
            this.dtp기표기간FR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp기표기간FR.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp기표기간FR.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp기표기간FR.Location = new System.Drawing.Point(324, 3);
            this.dtp기표기간FR.Mask = "####/##/##";
            this.dtp기표기간FR.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp기표기간FR.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp기표기간FR.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp기표기간FR.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp기표기간FR.Modified = false;
            this.dtp기표기간FR.Name = "dtp기표기간FR";
            this.dtp기표기간FR.PaddingCharacter = '_';
            this.dtp기표기간FR.PassivePromptCharacter = '_';
            this.dtp기표기간FR.PromptCharacter = '_';
            this.dtp기표기간FR.SelectedDayColor = System.Drawing.Color.White;
            this.dtp기표기간FR.ShowToDay = true;
            this.dtp기표기간FR.ShowTodayCircle = true;
            this.dtp기표기간FR.ShowUpDown = false;
            this.dtp기표기간FR.Size = new System.Drawing.Size(85, 21);
            this.dtp기표기간FR.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp기표기간FR.TabIndex = 143;
            this.dtp기표기간FR.TitleBackColor = System.Drawing.Color.White;
            this.dtp기표기간FR.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp기표기간FR.ToDayColor = System.Drawing.Color.Red;
            this.dtp기표기간FR.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp기표기간FR.UseKeyF3 = false;
            this.dtp기표기간FR.Value = new System.DateTime(((long)(0)));
            // 
            // bpc담당자
            // 
            this.bpc담당자.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc담당자.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc담당자.ButtonImage")));
            this.bpc담당자.ChildMode = "";
            this.bpc담당자.CodeName = "";
            this.bpc담당자.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc담당자.CodeValue = "";
            this.bpc담당자.ComboCheck = true;
            this.bpc담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.bpc담당자.IsCodeValueToUpper = true;
            this.bpc담당자.ItemBackColor = System.Drawing.Color.White;
            this.bpc담당자.Location = new System.Drawing.Point(324, 30);
            this.bpc담당자.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc담당자.Name = "bpc담당자";
            this.bpc담당자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc담당자.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc담당자.SearchCode = true;
            this.bpc담당자.SelectCount = 0;
            this.bpc담당자.SetDefaultValue = false;
            this.bpc담당자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc담당자.Size = new System.Drawing.Size(173, 21);
            this.bpc담당자.TabIndex = 142;
            this.bpc담당자.TabStop = false;
            // 
            // bpc영업그룹
            // 
            this.bpc영업그룹.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc영업그룹.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc영업그룹.ButtonImage")));
            this.bpc영업그룹.ChildMode = "";
            this.bpc영업그룹.CodeName = "";
            this.bpc영업그룹.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc영업그룹.CodeValue = "";
            this.bpc영업그룹.ComboCheck = true;
            this.bpc영업그룹.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.bpc영업그룹.IsCodeValueToUpper = true;
            this.bpc영업그룹.ItemBackColor = System.Drawing.Color.White;
            this.bpc영업그룹.Location = new System.Drawing.Point(80, 29);
            this.bpc영업그룹.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc영업그룹.Name = "bpc영업그룹";
            this.bpc영업그룹.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc영업그룹.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc영업그룹.SearchCode = true;
            this.bpc영업그룹.SelectCount = 0;
            this.bpc영업그룹.SetDefaultValue = false;
            this.bpc영업그룹.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc영업그룹.Size = new System.Drawing.Size(162, 21);
            this.bpc영업그룹.TabIndex = 141;
            this.bpc영업그룹.TabStop = false;
            // 
            // bpc거래처
            // 
            this.bpc거래처.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc거래처.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc거래처.ButtonImage")));
            this.bpc거래처.ChildMode = "";
            this.bpc거래처.CodeName = "";
            this.bpc거래처.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc거래처.CodeValue = "";
            this.bpc거래처.ComboCheck = true;
            this.bpc거래처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc거래처.IsCodeValueToUpper = true;
            this.bpc거래처.ItemBackColor = System.Drawing.Color.White;
            this.bpc거래처.Location = new System.Drawing.Point(582, 3);
            this.bpc거래처.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc거래처.Name = "bpc거래처";
            this.bpc거래처.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc거래처.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc거래처.SearchCode = true;
            this.bpc거래처.SelectCount = 0;
            this.bpc거래처.SetDefaultValue = false;
            this.bpc거래처.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc거래처.Size = new System.Drawing.Size(173, 21);
            this.bpc거래처.TabIndex = 140;
            this.bpc거래처.TabStop = false;
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel9.BackgroundImage")));
            this.panel9.Location = new System.Drawing.Point(5, 26);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(811, 1);
            this.panel9.TabIndex = 3;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel8.Controls.Add(this.labelExt1);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Location = new System.Drawing.Point(503, 1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(75, 51);
            this.panel8.TabIndex = 2;
            // 
            // labelExt1
            // 
            this.labelExt1.Location = new System.Drawing.Point(2, 31);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(70, 16);
            this.labelExt1.TabIndex = 2;
            this.labelExt1.Text = "전표처리";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 5);
            this.label7.Name = "label7";
            this.label7.Resizeble = true;
            this.label7.Size = new System.Drawing.Size(70, 18);
            this.label7.TabIndex = 1;
            this.label7.Text = "거래처";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Location = new System.Drawing.Point(245, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(75, 51);
            this.panel7.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 30);
            this.label4.Name = "label4";
            this.label4.Resizeble = true;
            this.label4.Size = new System.Drawing.Size(70, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "담당자";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 5);
            this.label5.Name = "label5";
            this.label5.Resizeble = true;
            this.label5.Size = new System.Drawing.Size(70, 18);
            this.label5.TabIndex = 1;
            this.label5.Text = "기표기간";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel6.Controls.Add(this.lbl프로젝트);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Location = new System.Drawing.Point(1, 1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(75, 77);
            this.panel6.TabIndex = 0;
            // 
            // lbl프로젝트
            // 
            this.lbl프로젝트.Location = new System.Drawing.Point(3, 56);
            this.lbl프로젝트.Name = "lbl프로젝트";
            this.lbl프로젝트.Resizeble = true;
            this.lbl프로젝트.Size = new System.Drawing.Size(70, 18);
            this.lbl프로젝트.TabIndex = 3;
            this.lbl프로젝트.Text = "프로젝트";
            this.lbl프로젝트.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 30);
            this.label3.Name = "label3";
            this.label3.Resizeble = true;
            this.label3.Size = new System.Drawing.Size(70, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "영업그룹";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Resizeble = true;
            this.label2.Size = new System.Drawing.Size(70, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "사업장";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 90);
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
            this.splitContainer1.Size = new System.Drawing.Size(821, 486);
            this.splitContainer1.SplitterDistance = 229;
            this.splitContainer1.TabIndex = 134;
            // 
            // _flexH
            // 
            this._flexH.AddMyMenu = true;
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
            this._flexH.Size = new System.Drawing.Size(821, 229);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 1;
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
            this._flexL.Size = new System.Drawing.Size(821, 253);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "FILE_ICON.gif");
            // 
            // btn전표발행취소
            // 
            this.btn전표발행취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn전표발행취소.BackColor = System.Drawing.Color.White;
            this.btn전표발행취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn전표발행취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn전표발행취소.Location = new System.Drawing.Point(699, 9);
            this.btn전표발행취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn전표발행취소.Name = "btn전표발행취소";
            this.btn전표발행취소.Size = new System.Drawing.Size(100, 19);
            this.btn전표발행취소.TabIndex = 137;
            this.btn전표발행취소.TabStop = false;
            this.btn전표발행취소.Text = "전표발행취소";
            this.btn전표발행취소.UseVisualStyleBackColor = false;
            this.btn전표발행취소.Click += new System.EventHandler(this.btn전표발행취소_Click);
            // 
            // btn전표발행
            // 
            this.btn전표발행.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn전표발행.BackColor = System.Drawing.Color.White;
            this.btn전표발행.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn전표발행.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn전표발행.Location = new System.Drawing.Point(592, 9);
            this.btn전표발행.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn전표발행.Name = "btn전표발행";
            this.btn전표발행.Size = new System.Drawing.Size(100, 19);
            this.btn전표발행.TabIndex = 136;
            this.btn전표발행.TabStop = false;
            this.btn전표발행.Text = "전표발행";
            this.btn전표발행.UseVisualStyleBackColor = false;
            this.btn전표발행.Click += new System.EventHandler(this.btn전표발행_Click);
            // 
            // bpBizarea
            // 
            this.bpBizarea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bpBizarea.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpBizarea.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpBizarea.ButtonImage")));
            this.bpBizarea.ChildMode = "";
            this.bpBizarea.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpBizarea.ComboCheck = true;
            this.bpBizarea.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BIZAREA_SUB1;
            this.bpBizarea.IsCodeValueToUpper = true;
            this.bpBizarea.ItemBackColor = System.Drawing.SystemColors.Window;
            this.bpBizarea.Location = new System.Drawing.Point(80, 2);
            this.bpBizarea.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpBizarea.Name = "bpBizarea";
            this.bpBizarea.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpBizarea.SearchCode = true;
            this.bpBizarea.SelectCount = 0;
            this.bpBizarea.SelectedIndex = -1;
            this.bpBizarea.SelectedItem = null;
            this.bpBizarea.SelectedText = "";
            this.bpBizarea.SelectedValue = null;
            this.bpBizarea.SetDefaultValue = false;
            this.bpBizarea.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpBizarea.Size = new System.Drawing.Size(162, 21);
            this.bpBizarea.TabIndex = 148;
            this.bpBizarea.TabStop = false;
            this.bpBizarea.Text = "bpComboBox1";
            // 
            // P_TR_EXBL_MNG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btn전표발행취소);
            this.Controls.Add(this.btn전표발행);
            this.Name = "P_TR_EXBL_MNG";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.btn전표발행, 0);
            this.Controls.SetChildIndex(this.btn전표발행취소, 0);
            this.mDataArea.ResumeLayout(false);
            this.tblLayout.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp기표기간TO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp기표기간FR)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLayout;
        private Duzon.Common.Controls.PanelExt panel5;
        private Duzon.Common.Controls.DatePicker dtp기표기간TO;
        private Duzon.Common.Controls.DatePicker dtp기표기간FR;
        private Duzon.Common.BpControls.BpCodeTextBox bpc담당자;
        private Duzon.Common.BpControls.BpCodeTextBox bpc영업그룹;
        private Duzon.Common.BpControls.BpCodeTextBox bpc거래처;
        private Duzon.Common.Controls.PanelExt panel9;
        private Duzon.Common.Controls.PanelExt panel8;
        private Duzon.Common.Controls.LabelExt label7;
        private Duzon.Common.Controls.PanelExt panel7;
        private Duzon.Common.Controls.LabelExt label4;
        private Duzon.Common.Controls.LabelExt label5;
        private Duzon.Common.Controls.PanelExt panel6;
        private Duzon.Common.Controls.LabelExt label3;
        private Duzon.Common.Controls.LabelExt label2;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Dass.FlexGrid.FlexGrid _flexH;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Duzon.Common.Controls.RoundedButton btn전표발행취소;
        private Duzon.Common.Controls.RoundedButton btn전표발행;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.DropDownComboBox cbo전표처리여부;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.LabelExt lbl프로젝트;
        private Duzon.Common.BpControls.BpCodeTextBox bp프로젝트;
        private Duzon.Common.BpControls.BpComboBox bpBizarea;
    }
}

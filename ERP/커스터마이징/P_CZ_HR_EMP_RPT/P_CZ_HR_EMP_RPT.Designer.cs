namespace cz
{
    partial class P_CZ_HR_EMP_RPT
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_HR_EMP_RPT));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp기준일자 = new Duzon.Common.Controls.DatePicker();
			this.lbl기준일자 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc부서 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl부서 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc회사 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl회사 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo재직구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl재직구분 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo내외국인 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl내외국인 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc직군 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl직군 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk전체보기 = new Duzon.Common.Controls.CheckBoxExt();
			this.lbl전체보기 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt검색 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl검색 = new Duzon.Common.Controls.LabelExt();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn단체메일발송 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp기준일자)).BeginInit();
			this.bpPanelControl5.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl6.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl8.SuspendLayout();
			this.bpPanelControl7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1224, 579);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1224, 579);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1218, 86);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.Controls.Add(this.bpPanelControl5);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1208, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.dtp기준일자);
			this.bpPanelControl3.Controls.Add(this.lbl기준일자);
			this.bpPanelControl3.Location = new System.Drawing.Point(640, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(196, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// dtp기준일자
			// 
			this.dtp기준일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp기준일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp기준일자.Location = new System.Drawing.Point(106, 0);
			this.dtp기준일자.Mask = "####/##/##";
			this.dtp기준일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp기준일자.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp기준일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp기준일자.Modified = true;
			this.dtp기준일자.Name = "dtp기준일자";
			this.dtp기준일자.Size = new System.Drawing.Size(90, 21);
			this.dtp기준일자.TabIndex = 1;
			this.dtp기준일자.Value = new System.DateTime(((long)(0)));
			// 
			// lbl기준일자
			// 
			this.lbl기준일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl기준일자.Location = new System.Drawing.Point(0, 0);
			this.lbl기준일자.Name = "lbl기준일자";
			this.lbl기준일자.Size = new System.Drawing.Size(100, 23);
			this.lbl기준일자.TabIndex = 0;
			this.lbl기준일자.Text = "기준일자";
			this.lbl기준일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.bpc부서);
			this.bpPanelControl5.Controls.Add(this.lbl부서);
			this.bpPanelControl5.Location = new System.Drawing.Point(321, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(317, 23);
			this.bpPanelControl5.TabIndex = 2;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// bpc부서
			// 
			this.bpc부서.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc부서.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_DEPT_SUB1;
			this.bpc부서.Location = new System.Drawing.Point(106, 0);
			this.bpc부서.Name = "bpc부서";
			this.bpc부서.Size = new System.Drawing.Size(211, 21);
			this.bpc부서.TabIndex = 1;
			this.bpc부서.TabStop = false;
			this.bpc부서.Text = "bpComboBox1";
			// 
			// lbl부서
			// 
			this.lbl부서.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl부서.Location = new System.Drawing.Point(0, 0);
			this.lbl부서.Name = "lbl부서";
			this.lbl부서.Size = new System.Drawing.Size(100, 23);
			this.lbl부서.TabIndex = 0;
			this.lbl부서.Text = "부서";
			this.lbl부서.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.bpc회사);
			this.bpPanelControl1.Controls.Add(this.lbl회사);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(317, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// bpc회사
			// 
			this.bpc회사.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc회사.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.bpc회사.Location = new System.Drawing.Point(106, 0);
			this.bpc회사.Name = "bpc회사";
			this.bpc회사.Size = new System.Drawing.Size(211, 21);
			this.bpc회사.TabIndex = 1;
			this.bpc회사.TabStop = false;
			this.bpc회사.Text = "bpComboBox1";
			this.bpc회사.UserCodeName = "NM_COMPANY";
			this.bpc회사.UserCodeValue = "CD_COMPANY";
			this.bpc회사.UserHelpID = "H_CZ_MA_COMPANY_SUB";
			this.bpc회사.UserParams = "회사;H_CZ_MA_COMPANY_SUB";
			// 
			// lbl회사
			// 
			this.lbl회사.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl회사.Location = new System.Drawing.Point(0, 0);
			this.lbl회사.Name = "lbl회사";
			this.lbl회사.Size = new System.Drawing.Size(100, 23);
			this.lbl회사.TabIndex = 0;
			this.lbl회사.Text = "회사";
			this.lbl회사.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl6);
			this.oneGridItem2.Controls.Add(this.bpPanelControl2);
			this.oneGridItem2.Controls.Add(this.bpPanelControl4);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1208, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl6
			// 
			this.bpPanelControl6.Controls.Add(this.cbo재직구분);
			this.bpPanelControl6.Controls.Add(this.lbl재직구분);
			this.bpPanelControl6.Location = new System.Drawing.Point(640, 1);
			this.bpPanelControl6.Name = "bpPanelControl6";
			this.bpPanelControl6.Size = new System.Drawing.Size(317, 23);
			this.bpPanelControl6.TabIndex = 3;
			this.bpPanelControl6.Text = "bpPanelControl6";
			// 
			// cbo재직구분
			// 
			this.cbo재직구분.AutoDropDown = true;
			this.cbo재직구분.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo재직구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo재직구분.FormattingEnabled = true;
			this.cbo재직구분.ItemHeight = 12;
			this.cbo재직구분.Location = new System.Drawing.Point(106, 0);
			this.cbo재직구분.Name = "cbo재직구분";
			this.cbo재직구분.Size = new System.Drawing.Size(211, 20);
			this.cbo재직구분.TabIndex = 1;
			// 
			// lbl재직구분
			// 
			this.lbl재직구분.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl재직구분.Location = new System.Drawing.Point(0, 0);
			this.lbl재직구분.Name = "lbl재직구분";
			this.lbl재직구분.Size = new System.Drawing.Size(100, 23);
			this.lbl재직구분.TabIndex = 0;
			this.lbl재직구분.Text = "재직구분";
			this.lbl재직구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.cbo내외국인);
			this.bpPanelControl2.Controls.Add(this.lbl내외국인);
			this.bpPanelControl2.Location = new System.Drawing.Point(321, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(317, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// cbo내외국인
			// 
			this.cbo내외국인.AutoDropDown = true;
			this.cbo내외국인.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo내외국인.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo내외국인.FormattingEnabled = true;
			this.cbo내외국인.ItemHeight = 12;
			this.cbo내외국인.Location = new System.Drawing.Point(106, 0);
			this.cbo내외국인.Name = "cbo내외국인";
			this.cbo내외국인.Size = new System.Drawing.Size(211, 20);
			this.cbo내외국인.TabIndex = 1;
			// 
			// lbl내외국인
			// 
			this.lbl내외국인.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl내외국인.Location = new System.Drawing.Point(0, 0);
			this.lbl내외국인.Name = "lbl내외국인";
			this.lbl내외국인.Size = new System.Drawing.Size(100, 23);
			this.lbl내외국인.TabIndex = 0;
			this.lbl내외국인.Text = "내외국인";
			this.lbl내외국인.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.bpc직군);
			this.bpPanelControl4.Controls.Add(this.lbl직군);
			this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(317, 23);
			this.bpPanelControl4.TabIndex = 1;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// bpc직군
			// 
			this.bpc직군.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc직군.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1;
			this.bpc직군.Location = new System.Drawing.Point(106, 0);
			this.bpc직군.Name = "bpc직군";
			this.bpc직군.Size = new System.Drawing.Size(211, 21);
			this.bpc직군.TabIndex = 1;
			this.bpc직군.TabStop = false;
			this.bpc직군.Text = "bpComboBox1";
			// 
			// lbl직군
			// 
			this.lbl직군.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl직군.Location = new System.Drawing.Point(0, 0);
			this.lbl직군.Name = "lbl직군";
			this.lbl직군.Size = new System.Drawing.Size(100, 23);
			this.lbl직군.TabIndex = 0;
			this.lbl직군.Text = "직군";
			this.lbl직군.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl8);
			this.oneGridItem3.Controls.Add(this.bpPanelControl7);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(1208, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// bpPanelControl8
			// 
			this.bpPanelControl8.Controls.Add(this.chk전체보기);
			this.bpPanelControl8.Controls.Add(this.lbl전체보기);
			this.bpPanelControl8.Location = new System.Drawing.Point(321, 1);
			this.bpPanelControl8.Name = "bpPanelControl8";
			this.bpPanelControl8.Size = new System.Drawing.Size(317, 23);
			this.bpPanelControl8.TabIndex = 3;
			this.bpPanelControl8.Text = "bpPanelControl8";
			// 
			// chk전체보기
			// 
			this.chk전체보기.Dock = System.Windows.Forms.DockStyle.Right;
			this.chk전체보기.Location = new System.Drawing.Point(106, 0);
			this.chk전체보기.Name = "chk전체보기";
			this.chk전체보기.Size = new System.Drawing.Size(211, 23);
			this.chk전체보기.TabIndex = 1;
			this.chk전체보기.TextDD = null;
			this.chk전체보기.UseVisualStyleBackColor = true;
			// 
			// lbl전체보기
			// 
			this.lbl전체보기.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl전체보기.Location = new System.Drawing.Point(0, 0);
			this.lbl전체보기.Name = "lbl전체보기";
			this.lbl전체보기.Size = new System.Drawing.Size(100, 23);
			this.lbl전체보기.TabIndex = 0;
			this.lbl전체보기.Text = "전체보기";
			this.lbl전체보기.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl7
			// 
			this.bpPanelControl7.Controls.Add(this.txt검색);
			this.bpPanelControl7.Controls.Add(this.lbl검색);
			this.bpPanelControl7.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl7.Name = "bpPanelControl7";
			this.bpPanelControl7.Size = new System.Drawing.Size(317, 23);
			this.bpPanelControl7.TabIndex = 2;
			this.bpPanelControl7.Text = "bpPanelControl7";
			// 
			// txt검색
			// 
			this.txt검색.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt검색.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt검색.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt검색.Location = new System.Drawing.Point(106, 0);
			this.txt검색.Name = "txt검색";
			this.txt검색.Size = new System.Drawing.Size(211, 21);
			this.txt검색.TabIndex = 1;
			// 
			// lbl검색
			// 
			this.lbl검색.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl검색.Location = new System.Drawing.Point(0, 0);
			this.lbl검색.Name = "lbl검색";
			this.lbl검색.Size = new System.Drawing.Size(100, 23);
			this.lbl검색.TabIndex = 0;
			this.lbl검색.Text = "검색";
			this.lbl검색.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _flex
			// 
			this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex.AutoResize = false;
			this._flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex.EnabledHeaderCheck = true;
			this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex.Location = new System.Drawing.Point(3, 95);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 20;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(1218, 481);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 1;
			this._flex.UseGridCalculator = true;
			// 
			// btn단체메일발송
			// 
			this.btn단체메일발송.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn단체메일발송.BackColor = System.Drawing.Color.Transparent;
			this.btn단체메일발송.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn단체메일발송.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn단체메일발송.Location = new System.Drawing.Point(1101, 10);
			this.btn단체메일발송.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn단체메일발송.Name = "btn단체메일발송";
			this.btn단체메일발송.Size = new System.Drawing.Size(120, 19);
			this.btn단체메일발송.TabIndex = 3;
			this.btn단체메일발송.TabStop = false;
			this.btn단체메일발송.Text = "단체메일발송";
			this.btn단체메일발송.UseVisualStyleBackColor = false;
			// 
			// P_CZ_HR_EMP_RPT
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.btn단체메일발송);
			this.Name = "P_CZ_HR_EMP_RPT";
			this.Size = new System.Drawing.Size(1224, 619);
			this.TitleText = "임직원현황표";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btn단체메일발송, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp기준일자)).EndInit();
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl6.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl8.ResumeLayout(false);
			this.bpPanelControl7.ResumeLayout(false);
			this.bpPanelControl7.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.BpControls.BpComboBox bpc회사;
        private Duzon.Common.Controls.LabelExt lbl회사;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.DropDownComboBox cbo내외국인;
        private Duzon.Common.Controls.LabelExt lbl내외국인;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.DatePicker dtp기준일자;
        private Duzon.Common.Controls.LabelExt lbl기준일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpComboBox bpc부서;
        private Duzon.Common.Controls.LabelExt lbl부서;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.Controls.DropDownComboBox cbo재직구분;
        private Duzon.Common.Controls.LabelExt lbl재직구분;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpComboBox bpc직군;
        private Duzon.Common.Controls.LabelExt lbl직군;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
        private Duzon.Common.Controls.TextBoxExt txt검색;
        private Duzon.Common.Controls.LabelExt lbl검색;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl8;
        private Duzon.Common.Controls.LabelExt lbl전체보기;
        private Duzon.Common.Controls.CheckBoxExt chk전체보기;
        private Duzon.Common.Controls.RoundedButton btn단체메일발송;
    }
}
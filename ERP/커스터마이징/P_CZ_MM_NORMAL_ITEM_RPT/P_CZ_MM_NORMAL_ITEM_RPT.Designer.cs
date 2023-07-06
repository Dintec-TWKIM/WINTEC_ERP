namespace cz
{
    partial class P_CZ_MM_NORMAL_ITEM_RPT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MM_NORMAL_ITEM_RPT));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpgPivot = new System.Windows.Forms.TabPage();
			this._pivot = new Duzon.BizOn.Windows.PivotGrid.PivotGrid();
			this.tpgList = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.pnl입고 = new Duzon.Common.Controls.ImagePanel(this.components);
			this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
			this.pnl출고 = new Duzon.Common.Controls.ImagePanel(this.components);
			this._flexR = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk재고품제외 = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt수주번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl수주번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp기준일자 = new Duzon.Common.Controls.DatePicker();
			this.lbl기준일자 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc회사 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl회사 = new Duzon.Common.Controls.LabelExt();
			this.btn도움말 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpgPivot.SuspendLayout();
			this.tpgList.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.pnl입고.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
			this.pnl출고.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexR)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp기준일자)).BeginInit();
			this.bpPanelControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1090, 756);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1090, 756);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tpgPivot);
			this.tabControl1.Controls.Add(this.tpgList);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(3, 49);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1084, 704);
			this.tabControl1.TabIndex = 3;
			// 
			// tpgPivot
			// 
			this.tpgPivot.Controls.Add(this._pivot);
			this.tpgPivot.Location = new System.Drawing.Point(4, 22);
			this.tpgPivot.Name = "tpgPivot";
			this.tpgPivot.Padding = new System.Windows.Forms.Padding(3);
			this.tpgPivot.Size = new System.Drawing.Size(1076, 678);
			this.tpgPivot.TabIndex = 0;
			this.tpgPivot.Text = "Pivot";
			this.tpgPivot.UseVisualStyleBackColor = true;
			// 
			// _pivot
			// 
			this._pivot.Dock = System.Windows.Forms.DockStyle.Fill;
			this._pivot.LocalLanguage = Duzon.BizOn.Windows.PivotGrid.LocalLanguage.KOR;
			this._pivot.Location = new System.Drawing.Point(3, 3);
			this._pivot.Name = "_pivot";
			this._pivot.Size = new System.Drawing.Size(1070, 672);
			this._pivot.TabIndex = 0;
			this._pivot.Text = "pivotGrid1";
			// 
			// tpgList
			// 
			this.tpgList.Controls.Add(this.splitContainer1);
			this.tpgList.Location = new System.Drawing.Point(4, 22);
			this.tpgList.Name = "tpgList";
			this.tpgList.Padding = new System.Windows.Forms.Padding(3);
			this.tpgList.Size = new System.Drawing.Size(1076, 678);
			this.tpgList.TabIndex = 1;
			this.tpgList.Text = "List";
			this.tpgList.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._flexH);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(1070, 672);
			this.splitContainer1.SplitterDistance = 372;
			this.splitContainer1.TabIndex = 2;
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
			this._flexH.Size = new System.Drawing.Size(1070, 372);
			this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
			this._flexH.TabIndex = 1;
			this._flexH.UseGridCalculator = true;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.pnl입고);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.pnl출고);
			this.splitContainer2.Size = new System.Drawing.Size(1070, 296);
			this.splitContainer2.SplitterDistance = 511;
			this.splitContainer2.TabIndex = 0;
			// 
			// pnl입고
			// 
			this.pnl입고.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.pnl입고.Controls.Add(this._flexL);
			this.pnl입고.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl입고.LeftImage = null;
			this.pnl입고.Location = new System.Drawing.Point(0, 0);
			this.pnl입고.Name = "pnl입고";
			this.pnl입고.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.pnl입고.PatternImage = null;
			this.pnl입고.RightImage = null;
			this.pnl입고.Size = new System.Drawing.Size(511, 296);
			this.pnl입고.TabIndex = 0;
			this.pnl입고.TitleText = "입고";
			// 
			// _flexL
			// 
			this._flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flexL.AutoResize = false;
			this._flexL.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexL.EnabledHeaderCheck = true;
			this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexL.Location = new System.Drawing.Point(2, 28);
			this._flexL.Name = "_flexL";
			this._flexL.Rows.Count = 1;
			this._flexL.Rows.DefaultSize = 20;
			this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexL.ShowSort = false;
			this._flexL.Size = new System.Drawing.Size(506, 265);
			this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
			this._flexL.TabIndex = 0;
			this._flexL.UseGridCalculator = true;
			// 
			// pnl출고
			// 
			this.pnl출고.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.pnl출고.Controls.Add(this._flexR);
			this.pnl출고.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl출고.LeftImage = null;
			this.pnl출고.Location = new System.Drawing.Point(0, 0);
			this.pnl출고.Name = "pnl출고";
			this.pnl출고.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.pnl출고.PatternImage = null;
			this.pnl출고.RightImage = null;
			this.pnl출고.Size = new System.Drawing.Size(555, 296);
			this.pnl출고.TabIndex = 0;
			this.pnl출고.TitleText = "출고";
			// 
			// _flexR
			// 
			this._flexR.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexR.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexR.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flexR.AutoResize = false;
			this._flexR.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexR.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexR.EnabledHeaderCheck = true;
			this._flexR.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexR.Location = new System.Drawing.Point(3, 28);
			this._flexR.Name = "_flexR";
			this._flexR.Rows.Count = 1;
			this._flexR.Rows.DefaultSize = 20;
			this._flexR.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexR.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexR.ShowSort = false;
			this._flexR.Size = new System.Drawing.Size(549, 265);
			this._flexR.StyleInfo = resources.GetString("_flexR.StyleInfo");
			this._flexR.TabIndex = 0;
			this._flexR.UseGridCalculator = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1084, 40);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl4);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1074, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.chk재고품제외);
			this.bpPanelControl4.Location = new System.Drawing.Point(838, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(97, 23);
			this.bpPanelControl4.TabIndex = 3;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// chk재고품제외
			// 
			this.chk재고품제외.AutoSize = true;
			this.chk재고품제외.Dock = System.Windows.Forms.DockStyle.Left;
			this.chk재고품제외.Location = new System.Drawing.Point(0, 0);
			this.chk재고품제외.Name = "chk재고품제외";
			this.chk재고품제외.Size = new System.Drawing.Size(84, 23);
			this.chk재고품제외.TabIndex = 0;
			this.chk재고품제외.Text = "재고품제외";
			this.chk재고품제외.TextDD = null;
			this.chk재고품제외.UseVisualStyleBackColor = true;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.txt수주번호);
			this.bpPanelControl2.Controls.Add(this.lbl수주번호);
			this.bpPanelControl2.Location = new System.Drawing.Point(519, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(317, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// txt수주번호
			// 
			this.txt수주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt수주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt수주번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt수주번호.Location = new System.Drawing.Point(106, 0);
			this.txt수주번호.Name = "txt수주번호";
			this.txt수주번호.Size = new System.Drawing.Size(211, 21);
			this.txt수주번호.TabIndex = 1;
			// 
			// lbl수주번호
			// 
			this.lbl수주번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl수주번호.Location = new System.Drawing.Point(0, 0);
			this.lbl수주번호.Name = "lbl수주번호";
			this.lbl수주번호.Size = new System.Drawing.Size(100, 23);
			this.lbl수주번호.TabIndex = 0;
			this.lbl수주번호.Text = "수주번호";
			this.lbl수주번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.dtp기준일자);
			this.bpPanelControl3.Controls.Add(this.lbl기준일자);
			this.bpPanelControl3.Location = new System.Drawing.Point(321, 1);
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
			this.dtp기준일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
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
			this.bpc회사.BackColor = System.Drawing.SystemColors.Control;
			this.bpc회사.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc회사.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.bpc회사.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
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
			// btn도움말
			// 
			this.btn도움말.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn도움말.BackColor = System.Drawing.Color.Transparent;
			this.btn도움말.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn도움말.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn도움말.Location = new System.Drawing.Point(1012, 13);
			this.btn도움말.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn도움말.Name = "btn도움말";
			this.btn도움말.Size = new System.Drawing.Size(78, 19);
			this.btn도움말.TabIndex = 3;
			this.btn도움말.TabStop = false;
			this.btn도움말.Text = "도움말";
			this.btn도움말.UseVisualStyleBackColor = false;
			// 
			// P_CZ_MM_NORMAL_ITEM_RPT
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.btn도움말);
			this.Name = "P_CZ_MM_NORMAL_ITEM_RPT";
			this.Size = new System.Drawing.Size(1090, 796);
			this.TitleText = "일반품재고현황";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btn도움말, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tpgPivot.ResumeLayout(false);
			this.tpgList.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.pnl입고.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
			this.pnl출고.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexR)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl4.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl2.PerformLayout();
			this.bpPanelControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp기준일자)).EndInit();
			this.bpPanelControl1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.BpControls.BpComboBox bpc회사;
        private Duzon.Common.Controls.LabelExt lbl회사;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgPivot;
        private Duzon.BizOn.Windows.PivotGrid.PivotGrid _pivot;
        private System.Windows.Forms.TabPage tpgList;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.LabelExt lbl수주번호;
        private Duzon.Common.Controls.TextBoxExt txt수주번호;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Dass.FlexGrid.FlexGrid _flexR;
        private Duzon.Common.Controls.ImagePanel pnl입고;
        private Duzon.Common.Controls.ImagePanel pnl출고;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.DatePicker dtp기준일자;
        private Duzon.Common.Controls.LabelExt lbl기준일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.CheckBoxExt chk재고품제외;
        private Duzon.Common.Controls.RoundedButton btn도움말;
    }
}
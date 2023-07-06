namespace cz
{
	partial class P_CZ_MA_ITEM_HULL
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MA_ITEM_HULL));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tlayT = new System.Windows.Forms.TableLayoutPanel();
			this.grd헤드 = new Dass.FlexGrid.FlexGrid(this.components);
			this.one검색 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl조회대상 = new Duzon.Common.BpControls.BpPanelControl();
			this.cur건수 = new Dintec.UCurrencyBox();
			this.lbl건수 = new Duzon.Common.Controls.LabelExt();
			this.labelExt12 = new Duzon.Common.Controls.LabelExt();
			this.pnl매출처 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx운항선사 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl매출처 = new Duzon.Common.Controls.LabelExt();
			this.pnl호선번호 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx호선번호 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl호선번호 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl검색조건 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk재고코드 = new Duzon.Common.Controls.CheckBoxExt();
			this.chkU코드 = new Duzon.Common.Controls.CheckBoxExt();
			this.chkEZ코드 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk품목코드 = new Duzon.Common.Controls.CheckBoxExt();
			this.labelExt11 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
			this.tbx키워드 = new Dintec.UTextBox();
			this.btn엑셀 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.cbo키워드 = new Duzon.Common.Controls.DropDownComboBox();
			this.tlayB = new System.Windows.Forms.TableLayoutPanel();
			this.grd라인 = new Dass.FlexGrid.FlexGrid(this.components);
			this.ipnl = new Duzon.Common.Controls.ImagePanel(this.components);
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk업로드 = new Duzon.Common.Controls.CheckBoxExt();
			this.labelExt3 = new Duzon.Common.Controls.LabelExt();
			this.mDataArea.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tlayT.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd헤드)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.pnl조회대상.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur건수)).BeginInit();
			this.pnl매출처.SuspendLayout();
			this.pnl호선번호.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.pnl검색조건.SuspendLayout();
			this.bpPanelControl7.SuspendLayout();
			this.tlayB.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd라인)).BeginInit();
			this.bpPanelControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.splitContainer1);
			this.mDataArea.Size = new System.Drawing.Size(1600, 1060);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tlayT);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tlayB);
			this.splitContainer1.Size = new System.Drawing.Size(1600, 1060);
			this.splitContainer1.SplitterDistance = 476;
			this.splitContainer1.TabIndex = 0;
			// 
			// tlayT
			// 
			this.tlayT.ColumnCount = 1;
			this.tlayT.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayT.Controls.Add(this.grd헤드, 0, 1);
			this.tlayT.Controls.Add(this.one검색, 0, 0);
			this.tlayT.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayT.Location = new System.Drawing.Point(0, 0);
			this.tlayT.Name = "tlayT";
			this.tlayT.RowCount = 2;
			this.tlayT.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayT.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayT.Size = new System.Drawing.Size(1600, 476);
			this.tlayT.TabIndex = 0;
			// 
			// grd헤드
			// 
			this.grd헤드.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd헤드.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd헤드.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd헤드.AutoResize = false;
			this.grd헤드.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd헤드.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd헤드.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd헤드.EnabledHeaderCheck = true;
			this.grd헤드.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd헤드.Location = new System.Drawing.Point(3, 72);
			this.grd헤드.Name = "grd헤드";
			this.grd헤드.Rows.Count = 1;
			this.grd헤드.Rows.DefaultSize = 20;
			this.grd헤드.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd헤드.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd헤드.ShowSort = false;
			this.grd헤드.Size = new System.Drawing.Size(1594, 401);
			this.grd헤드.StyleInfo = resources.GetString("grd헤드.StyleInfo");
			this.grd헤드.TabIndex = 2;
			this.grd헤드.UseGridCalculator = true;
			// 
			// one검색
			// 
			this.one검색.Dock = System.Windows.Forms.DockStyle.Top;
			this.one검색.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.one검색.Location = new System.Drawing.Point(3, 3);
			this.one검색.Name = "one검색";
			this.one검색.Size = new System.Drawing.Size(1594, 63);
			this.one검색.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.pnl조회대상);
			this.oneGridItem1.Controls.Add(this.pnl매출처);
			this.oneGridItem1.Controls.Add(this.pnl호선번호);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1584, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// pnl조회대상
			// 
			this.pnl조회대상.Controls.Add(this.cur건수);
			this.pnl조회대상.Controls.Add(this.lbl건수);
			this.pnl조회대상.Controls.Add(this.labelExt12);
			this.pnl조회대상.Location = new System.Drawing.Point(546, 1);
			this.pnl조회대상.Name = "pnl조회대상";
			this.pnl조회대상.Size = new System.Drawing.Size(206, 23);
			this.pnl조회대상.TabIndex = 33;
			this.pnl조회대상.Text = "bpPanelControl3";
			// 
			// cur건수
			// 
			this.cur건수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur건수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur건수.DecimalValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.cur건수.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur건수.Location = new System.Drawing.Point(84, 1);
			this.cur건수.Name = "cur건수";
			this.cur건수.NullString = "1";
			this.cur건수.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur건수.Size = new System.Drawing.Size(40, 20);
			this.cur건수.TabIndex = 21;
			this.cur건수.Tag = "";
			this.cur건수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lbl건수
			// 
			this.lbl건수.Location = new System.Drawing.Point(128, 4);
			this.lbl건수.Name = "lbl건수";
			this.lbl건수.Size = new System.Drawing.Size(60, 16);
			this.lbl건수.TabIndex = 10;
			this.lbl건수.Text = "회 이상";
			this.lbl건수.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelExt12
			// 
			this.labelExt12.Location = new System.Drawing.Point(17, 4);
			this.labelExt12.Name = "labelExt12";
			this.labelExt12.Size = new System.Drawing.Size(65, 16);
			this.labelExt12.TabIndex = 1;
			this.labelExt12.Text = "조회대상";
			this.labelExt12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl매출처
			// 
			this.pnl매출처.Controls.Add(this.ctx운항선사);
			this.pnl매출처.Controls.Add(this.lbl매출처);
			this.pnl매출처.Location = new System.Drawing.Point(274, 1);
			this.pnl매출처.Name = "pnl매출처";
			this.pnl매출처.Size = new System.Drawing.Size(270, 23);
			this.pnl매출처.TabIndex = 23;
			this.pnl매출처.Text = "bpPanelControl4";
			// 
			// ctx운항선사
			// 
			this.ctx운항선사.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
			this.ctx운항선사.Location = new System.Drawing.Point(84, 1);
			this.ctx운항선사.Name = "ctx운항선사";
			this.ctx운항선사.Size = new System.Drawing.Size(186, 21);
			this.ctx운항선사.TabIndex = 1;
			this.ctx운항선사.TabStop = false;
			this.ctx운항선사.Tag = "CD_PARTNER;LN_PARTNER";
			// 
			// lbl매출처
			// 
			this.lbl매출처.Location = new System.Drawing.Point(17, 4);
			this.lbl매출처.Name = "lbl매출처";
			this.lbl매출처.Size = new System.Drawing.Size(65, 16);
			this.lbl매출처.TabIndex = 0;
			this.lbl매출처.Text = "운항선사";
			this.lbl매출처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl호선번호
			// 
			this.pnl호선번호.Controls.Add(this.ctx호선번호);
			this.pnl호선번호.Controls.Add(this.lbl호선번호);
			this.pnl호선번호.Location = new System.Drawing.Point(2, 1);
			this.pnl호선번호.Name = "pnl호선번호";
			this.pnl호선번호.Size = new System.Drawing.Size(270, 23);
			this.pnl호선번호.TabIndex = 19;
			this.pnl호선번호.Text = "bpPanelControl5";
			// 
			// ctx호선번호
			// 
			this.ctx호선번호.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx호선번호.Location = new System.Drawing.Point(84, 1);
			this.ctx호선번호.Name = "ctx호선번호";
			this.ctx호선번호.Size = new System.Drawing.Size(186, 21);
			this.ctx호선번호.TabIndex = 8;
			this.ctx호선번호.TabStop = false;
			this.ctx호선번호.Tag = "";
			this.ctx호선번호.Text = "bpCodeTextBox1";
			this.ctx호선번호.UserCodeName = "NO_HULL";
			this.ctx호선번호.UserCodeValue = "NO_IMO";
			this.ctx호선번호.UserHelpID = "H_CZ_MA_HULL_SUB";
			this.ctx호선번호.UserParams = "호선;H_CZ_MA_HULL_SUB";
			// 
			// lbl호선번호
			// 
			this.lbl호선번호.Location = new System.Drawing.Point(17, 4);
			this.lbl호선번호.Name = "lbl호선번호";
			this.lbl호선번호.Size = new System.Drawing.Size(65, 16);
			this.lbl호선번호.TabIndex = 3;
			this.lbl호선번호.Text = "호선번호";
			this.lbl호선번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl1);
			this.oneGridItem2.Controls.Add(this.pnl검색조건);
			this.oneGridItem2.Controls.Add(this.bpPanelControl7);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1584, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// pnl검색조건
			// 
			this.pnl검색조건.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.pnl검색조건.Controls.Add(this.chk재고코드);
			this.pnl검색조건.Controls.Add(this.chkU코드);
			this.pnl검색조건.Controls.Add(this.chkEZ코드);
			this.pnl검색조건.Controls.Add(this.chk품목코드);
			this.pnl검색조건.Controls.Add(this.labelExt11);
			this.pnl검색조건.Location = new System.Drawing.Point(274, 1);
			this.pnl검색조건.Name = "pnl검색조건";
			this.pnl검색조건.Size = new System.Drawing.Size(478, 23);
			this.pnl검색조건.TabIndex = 34;
			this.pnl검색조건.Text = "bpPanelControl1";
			// 
			// chk재고코드
			// 
			this.chk재고코드.Location = new System.Drawing.Point(184, 5);
			this.chk재고코드.Name = "chk재고코드";
			this.chk재고코드.Size = new System.Drawing.Size(85, 16);
			this.chk재고코드.TabIndex = 11;
			this.chk재고코드.Tag = "CD_ITEM";
			this.chk재고코드.Text = "재고코드";
			this.chk재고코드.TextDD = null;
			this.chk재고코드.UseVisualStyleBackColor = true;
			// 
			// chkU코드
			// 
			this.chkU코드.Location = new System.Drawing.Point(284, 5);
			this.chkU코드.Name = "chkU코드";
			this.chkU코드.Size = new System.Drawing.Size(85, 16);
			this.chkU코드.TabIndex = 8;
			this.chkU코드.Tag = "UCODE";
			this.chkU코드.Text = "U코드";
			this.chkU코드.TextDD = null;
			this.chkU코드.UseVisualStyleBackColor = true;
			// 
			// chkEZ코드
			// 
			this.chkEZ코드.Location = new System.Drawing.Point(384, 5);
			this.chkEZ코드.Name = "chkEZ코드";
			this.chkEZ코드.Size = new System.Drawing.Size(85, 16);
			this.chkEZ코드.TabIndex = 7;
			this.chkEZ코드.Tag = "EZCODE";
			this.chkEZ코드.Text = "도면번호";
			this.chkEZ코드.TextDD = null;
			this.chkEZ코드.UseVisualStyleBackColor = true;
			// 
			// chk품목코드
			// 
			this.chk품목코드.Location = new System.Drawing.Point(84, 5);
			this.chk품목코드.Name = "chk품목코드";
			this.chk품목코드.Size = new System.Drawing.Size(85, 16);
			this.chk품목코드.TabIndex = 5;
			this.chk품목코드.Tag = "NO_PLATE";
			this.chk품목코드.Text = "부품번호";
			this.chk품목코드.TextDD = null;
			this.chk품목코드.UseVisualStyleBackColor = true;
			// 
			// labelExt11
			// 
			this.labelExt11.Location = new System.Drawing.Point(17, 4);
			this.labelExt11.Name = "labelExt11";
			this.labelExt11.Size = new System.Drawing.Size(65, 16);
			this.labelExt11.TabIndex = 1;
			this.labelExt11.Text = "검색조건";
			this.labelExt11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl7
			// 
			this.bpPanelControl7.Controls.Add(this.tbx키워드);
			this.bpPanelControl7.Controls.Add(this.btn엑셀);
			this.bpPanelControl7.Controls.Add(this.cbo키워드);
			this.bpPanelControl7.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl7.Name = "bpPanelControl7";
			this.bpPanelControl7.Size = new System.Drawing.Size(270, 23);
			this.bpPanelControl7.TabIndex = 33;
			this.bpPanelControl7.Text = "bpPanelControl5";
			// 
			// tbx키워드
			// 
			this.tbx키워드.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx키워드.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx키워드.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.tbx키워드.ColorTag = System.Drawing.Color.Empty;
			this.tbx키워드.Location = new System.Drawing.Point(84, 1);
			this.tbx키워드.Name = "tbx키워드";
			this.tbx키워드.Size = new System.Drawing.Size(142, 20);
			this.tbx키워드.TabIndex = 15;
			// 
			// btn엑셀
			// 
			this.btn엑셀.BackColor = System.Drawing.Color.White;
			this.btn엑셀.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn엑셀.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn엑셀.Location = new System.Drawing.Point(229, 1);
			this.btn엑셀.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn엑셀.Name = "btn엑셀";
			this.btn엑셀.Size = new System.Drawing.Size(40, 19);
			this.btn엑셀.TabIndex = 14;
			this.btn엑셀.TabStop = false;
			this.btn엑셀.Tag = "";
			this.btn엑셀.Text = "엑셀";
			this.btn엑셀.UseVisualStyleBackColor = false;
			// 
			// cbo키워드
			// 
			this.cbo키워드.AutoDropDown = true;
			this.cbo키워드.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo키워드.FormattingEnabled = true;
			this.cbo키워드.ItemHeight = 12;
			this.cbo키워드.Location = new System.Drawing.Point(5, 1);
			this.cbo키워드.Name = "cbo키워드";
			this.cbo키워드.Size = new System.Drawing.Size(77, 20);
			this.cbo키워드.TabIndex = 13;
			this.cbo키워드.Tag = "";
			// 
			// tlayB
			// 
			this.tlayB.ColumnCount = 1;
			this.tlayB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayB.Controls.Add(this.grd라인, 0, 1);
			this.tlayB.Controls.Add(this.ipnl, 0, 0);
			this.tlayB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayB.Location = new System.Drawing.Point(0, 0);
			this.tlayB.Name = "tlayB";
			this.tlayB.RowCount = 2;
			this.tlayB.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayB.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayB.Size = new System.Drawing.Size(1600, 580);
			this.tlayB.TabIndex = 0;
			// 
			// grd라인
			// 
			this.grd라인.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd라인.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd라인.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd라인.AutoResize = false;
			this.grd라인.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd라인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd라인.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd라인.EnabledHeaderCheck = true;
			this.grd라인.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd라인.Location = new System.Drawing.Point(3, 36);
			this.grd라인.Name = "grd라인";
			this.grd라인.Rows.Count = 1;
			this.grd라인.Rows.DefaultSize = 20;
			this.grd라인.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd라인.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd라인.ShowSort = false;
			this.grd라인.Size = new System.Drawing.Size(1594, 541);
			this.grd라인.StyleInfo = resources.GetString("grd라인.StyleInfo");
			this.grd라인.TabIndex = 11;
			this.grd라인.UseGridCalculator = true;
			// 
			// ipnl
			// 
			this.ipnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.ipnl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ipnl.LeftImage = null;
			this.ipnl.Location = new System.Drawing.Point(3, 3);
			this.ipnl.Name = "ipnl";
			this.ipnl.Padding = new System.Windows.Forms.Padding(1);
			this.ipnl.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.ipnl.PatternImage = null;
			this.ipnl.RightImage = null;
			this.ipnl.Size = new System.Drawing.Size(1594, 27);
			this.ipnl.TabIndex = 10;
			this.ipnl.TitleText = "상세정보";
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.chk업로드);
			this.bpPanelControl1.Controls.Add(this.labelExt3);
			this.bpPanelControl1.Location = new System.Drawing.Point(754, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(370, 23);
			this.bpPanelControl1.TabIndex = 50;
			this.bpPanelControl1.Text = "bpPanelControl3";
			// 
			// chk업로드
			// 
			this.chk업로드.Checked = true;
			this.chk업로드.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk업로드.Location = new System.Drawing.Point(107, 5);
			this.chk업로드.Name = "chk업로드";
			this.chk업로드.Size = new System.Drawing.Size(146, 16);
			this.chk업로드.TabIndex = 6;
			this.chk업로드.Tag = "";
			this.chk업로드.Text = "업로드";
			this.chk업로드.TextDD = null;
			this.chk업로드.UseVisualStyleBackColor = true;
			// 
			// labelExt3
			// 
			this.labelExt3.Location = new System.Drawing.Point(3, 4);
			this.labelExt3.Name = "labelExt3";
			this.labelExt3.Size = new System.Drawing.Size(100, 16);
			this.labelExt3.TabIndex = 1;
			this.labelExt3.Text = "기타옵션";
			this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// P_CZ_MA_ITEM_HULL
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "P_CZ_MA_ITEM_HULL";
			this.Size = new System.Drawing.Size(1600, 1100);
			this.TitleText = "P_CZ_MA_ITEM_HULL";
			this.mDataArea.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tlayT.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd헤드)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.pnl조회대상.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.cur건수)).EndInit();
			this.pnl매출처.ResumeLayout(false);
			this.pnl호선번호.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.pnl검색조건.ResumeLayout(false);
			this.bpPanelControl7.ResumeLayout(false);
			this.tlayB.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd라인)).EndInit();
			this.bpPanelControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TableLayoutPanel tlayT;
		private System.Windows.Forms.TableLayoutPanel tlayB;
		private Duzon.Erpiu.Windows.OneControls.OneGrid one검색;
		private Dass.FlexGrid.FlexGrid grd헤드;
		private Dass.FlexGrid.FlexGrid grd라인;
		private Duzon.Common.Controls.ImagePanel ipnl;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl pnl호선번호;
		private Duzon.Common.BpControls.BpCodeTextBox ctx호선번호;
		private Duzon.Common.Controls.LabelExt lbl호선번호;
		private Duzon.Common.BpControls.BpPanelControl pnl매출처;
		private Duzon.Common.BpControls.BpCodeTextBox ctx운항선사;
		private Duzon.Common.Controls.LabelExt lbl매출처;
		private Duzon.Common.BpControls.BpPanelControl pnl조회대상;
		private Duzon.Common.Controls.LabelExt lbl건수;
		private Duzon.Common.Controls.LabelExt labelExt12;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
		private Dintec.UTextBox tbx키워드;
		private Duzon.Common.Controls.RoundedButton btn엑셀;
		private Duzon.Common.Controls.DropDownComboBox cbo키워드;
		private Duzon.Common.BpControls.BpPanelControl pnl검색조건;
		private Duzon.Common.Controls.CheckBoxExt chk재고코드;
		private Duzon.Common.Controls.CheckBoxExt chkU코드;
		private Duzon.Common.Controls.CheckBoxExt chkEZ코드;
		private Duzon.Common.Controls.CheckBoxExt chk품목코드;
		private Duzon.Common.Controls.LabelExt labelExt11;
		private Dintec.UCurrencyBox cur건수;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.CheckBoxExt chk업로드;
		private Duzon.Common.Controls.LabelExt labelExt3;
	}
}
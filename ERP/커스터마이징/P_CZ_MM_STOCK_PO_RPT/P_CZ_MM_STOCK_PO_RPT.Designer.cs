namespace cz
{
    partial class P_CZ_MM_STOCK_PO_RPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MM_STOCK_PO_RPT));
            this.tpgMain = new Duzon.Common.Controls.TabControlExt();
            this.tpg재고발주현황 = new System.Windows.Forms.TabPage();
            this.pnlHide = new Duzon.Common.Controls.PanelEx();
            this.webBrowserExt1 = new Duzon.Common.Controls.WebBrowserExt();
            this.tpg기자재재고 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl중분류 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.cur빈도수 = new Duzon.Common.Controls.CurrencyTextBox();
            this.lbl빈도수 = new Duzon.Common.Controls.LabelExt();
            this.btn기자재조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.cur기자재개월수 = new Duzon.Common.Controls.CurrencyTextBox();
            this.lbl기자재개월수 = new Duzon.Common.Controls.LabelExt();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flex기자재재고H = new Dass.FlexGrid.FlexGrid(this.components);
            this._flex기자재재고L = new Dass.FlexGrid.FlexGrid(this.components);
            this.tpg현대재고 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid2 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt엔진모델 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl엔진모델 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.cur호선수 = new Duzon.Common.Controls.CurrencyTextBox();
            this.lbl호선수 = new Duzon.Common.Controls.LabelExt();
            this.btn현대조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.cur현대개월수 = new Duzon.Common.Controls.CurrencyTextBox();
            this.lbl현대개월수 = new Duzon.Common.Controls.LabelExt();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._flex현대재고H = new Dass.FlexGrid.FlexGrid(this.components);
            this._flex현대재고L = new Dass.FlexGrid.FlexGrid(this.components);
            this.ctx중분류 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.mDataArea.SuspendLayout();
            this.tpgMain.SuspendLayout();
            this.tpg재고발주현황.SuspendLayout();
            this.tpg기자재재고.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur빈도수)).BeginInit();
            this.bpPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur기자재개월수)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex기자재재고H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex기자재재고L)).BeginInit();
            this.tpg현대재고.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.oneGridItem3.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur호선수)).BeginInit();
            this.bpPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur현대개월수)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex현대재고H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex현대재고L)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tpgMain);
            this.mDataArea.Size = new System.Drawing.Size(1090, 756);
            // 
            // tpgMain
            // 
            this.tpgMain.Controls.Add(this.tpg재고발주현황);
            this.tpgMain.Controls.Add(this.tpg기자재재고);
            this.tpgMain.Controls.Add(this.tpg현대재고);
            this.tpgMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpgMain.Location = new System.Drawing.Point(0, 0);
            this.tpgMain.Name = "tpgMain";
            this.tpgMain.SelectedIndex = 0;
            this.tpgMain.Size = new System.Drawing.Size(1090, 756);
            this.tpgMain.TabIndex = 0;
            // 
            // tpg재고발주현황
            // 
            this.tpg재고발주현황.Controls.Add(this.pnlHide);
            this.tpg재고발주현황.Controls.Add(this.webBrowserExt1);
            this.tpg재고발주현황.Location = new System.Drawing.Point(4, 22);
            this.tpg재고발주현황.Name = "tpg재고발주현황";
            this.tpg재고발주현황.Padding = new System.Windows.Forms.Padding(3);
            this.tpg재고발주현황.Size = new System.Drawing.Size(1082, 730);
            this.tpg재고발주현황.TabIndex = 0;
            this.tpg재고발주현황.Text = "재고발주현황";
            this.tpg재고발주현황.UseVisualStyleBackColor = true;
            // 
            // pnlHide
            // 
            this.pnlHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.pnlHide.ColorA = System.Drawing.Color.Empty;
            this.pnlHide.ColorB = System.Drawing.Color.Empty;
            this.pnlHide.Location = new System.Drawing.Point(855, 692);
            this.pnlHide.Name = "pnlHide";
            this.pnlHide.Size = new System.Drawing.Size(225, 36);
            this.pnlHide.TabIndex = 2;
            // 
            // webBrowserExt1
            // 
            this.webBrowserExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserExt1.Location = new System.Drawing.Point(3, 3);
            this.webBrowserExt1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserExt1.Name = "webBrowserExt1";
            this.webBrowserExt1.Size = new System.Drawing.Size(1076, 724);
            this.webBrowserExt1.TabIndex = 0;
            // 
            // tpg기자재재고
            // 
            this.tpg기자재재고.Controls.Add(this.tableLayoutPanel1);
            this.tpg기자재재고.Location = new System.Drawing.Point(4, 22);
            this.tpg기자재재고.Name = "tpg기자재재고";
            this.tpg기자재재고.Padding = new System.Windows.Forms.Padding(3);
            this.tpg기자재재고.Size = new System.Drawing.Size(1082, 730);
            this.tpg기자재재고.TabIndex = 1;
            this.tpg기자재재고.Text = "기자재재고";
            this.tpg기자재재고.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1076, 724);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(1070, 39);
            this.oneGrid1.TabIndex = 1;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl6);
            this.oneGridItem1.Controls.Add(this.bpPanelControl4);
            this.oneGridItem1.Controls.Add(this.btn기자재조회);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(1060, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.ctx중분류);
            this.bpPanelControl6.Controls.Add(this.lbl중분류);
            this.bpPanelControl6.Location = new System.Drawing.Point(662, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl6.TabIndex = 3;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // lbl중분류
            // 
            this.lbl중분류.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl중분류.Location = new System.Drawing.Point(0, 0);
            this.lbl중분류.Name = "lbl중분류";
            this.lbl중분류.Size = new System.Drawing.Size(100, 23);
            this.lbl중분류.TabIndex = 0;
            this.lbl중분류.Text = "중분류";
            this.lbl중분류.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.cur빈도수);
            this.bpPanelControl4.Controls.Add(this.lbl빈도수);
            this.bpPanelControl4.Location = new System.Drawing.Point(368, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl4.TabIndex = 2;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // cur빈도수
            // 
            this.cur빈도수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur빈도수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur빈도수.DecimalValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.cur빈도수.Dock = System.Windows.Forms.DockStyle.Right;
            this.cur빈도수.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur빈도수.Location = new System.Drawing.Point(106, 0);
            this.cur빈도수.Name = "cur빈도수";
            this.cur빈도수.NullString = "0";
            this.cur빈도수.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur빈도수.Size = new System.Drawing.Size(186, 21);
            this.cur빈도수.TabIndex = 1;
            this.cur빈도수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl빈도수
            // 
            this.lbl빈도수.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl빈도수.Location = new System.Drawing.Point(0, 0);
            this.lbl빈도수.Name = "lbl빈도수";
            this.lbl빈도수.Size = new System.Drawing.Size(100, 23);
            this.lbl빈도수.TabIndex = 0;
            this.lbl빈도수.Text = "빈도수";
            this.lbl빈도수.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn기자재조회
            // 
            this.btn기자재조회.BackColor = System.Drawing.Color.Transparent;
            this.btn기자재조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn기자재조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn기자재조회.Location = new System.Drawing.Point(296, 1);
            this.btn기자재조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn기자재조회.Name = "btn기자재조회";
            this.btn기자재조회.Size = new System.Drawing.Size(70, 19);
            this.btn기자재조회.TabIndex = 1;
            this.btn기자재조회.TabStop = false;
            this.btn기자재조회.Text = "조회";
            this.btn기자재조회.UseVisualStyleBackColor = false;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.cur기자재개월수);
            this.bpPanelControl1.Controls.Add(this.lbl기자재개월수);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // cur기자재개월수
            // 
            this.cur기자재개월수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur기자재개월수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur기자재개월수.DecimalValue = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.cur기자재개월수.Dock = System.Windows.Forms.DockStyle.Right;
            this.cur기자재개월수.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur기자재개월수.Location = new System.Drawing.Point(106, 0);
            this.cur기자재개월수.Name = "cur기자재개월수";
            this.cur기자재개월수.NullString = "0";
            this.cur기자재개월수.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur기자재개월수.Size = new System.Drawing.Size(186, 21);
            this.cur기자재개월수.TabIndex = 1;
            this.cur기자재개월수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl기자재개월수
            // 
            this.lbl기자재개월수.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl기자재개월수.Location = new System.Drawing.Point(0, 0);
            this.lbl기자재개월수.Name = "lbl기자재개월수";
            this.lbl기자재개월수.Size = new System.Drawing.Size(100, 23);
            this.lbl기자재개월수.TabIndex = 0;
            this.lbl기자재개월수.Text = "개월수";
            this.lbl기자재개월수.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 48);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._flex기자재재고H);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._flex기자재재고L);
            this.splitContainer1.Size = new System.Drawing.Size(1070, 673);
            this.splitContainer1.SplitterDistance = 356;
            this.splitContainer1.TabIndex = 2;
            // 
            // _flex기자재재고H
            // 
            this._flex기자재재고H.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex기자재재고H.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex기자재재고H.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex기자재재고H.AutoResize = false;
            this._flex기자재재고H.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex기자재재고H.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex기자재재고H.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex기자재재고H.EnabledHeaderCheck = true;
            this._flex기자재재고H.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex기자재재고H.Location = new System.Drawing.Point(0, 0);
            this._flex기자재재고H.Name = "_flex기자재재고H";
            this._flex기자재재고H.Rows.Count = 1;
            this._flex기자재재고H.Rows.DefaultSize = 20;
            this._flex기자재재고H.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex기자재재고H.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex기자재재고H.ShowSort = false;
            this._flex기자재재고H.Size = new System.Drawing.Size(356, 673);
            this._flex기자재재고H.StyleInfo = resources.GetString("_flex기자재재고H.StyleInfo");
            this._flex기자재재고H.TabIndex = 0;
            this._flex기자재재고H.UseGridCalculator = true;
            // 
            // _flex기자재재고L
            // 
            this._flex기자재재고L.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex기자재재고L.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex기자재재고L.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex기자재재고L.AutoResize = false;
            this._flex기자재재고L.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex기자재재고L.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex기자재재고L.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex기자재재고L.EnabledHeaderCheck = true;
            this._flex기자재재고L.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex기자재재고L.Location = new System.Drawing.Point(0, 0);
            this._flex기자재재고L.Name = "_flex기자재재고L";
            this._flex기자재재고L.Rows.Count = 1;
            this._flex기자재재고L.Rows.DefaultSize = 20;
            this._flex기자재재고L.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex기자재재고L.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex기자재재고L.ShowSort = false;
            this._flex기자재재고L.Size = new System.Drawing.Size(710, 673);
            this._flex기자재재고L.StyleInfo = resources.GetString("_flex기자재재고L.StyleInfo");
            this._flex기자재재고L.TabIndex = 0;
            this._flex기자재재고L.UseGridCalculator = true;
            // 
            // tpg현대재고
            // 
            this.tpg현대재고.Controls.Add(this.tableLayoutPanel2);
            this.tpg현대재고.Location = new System.Drawing.Point(4, 22);
            this.tpg현대재고.Name = "tpg현대재고";
            this.tpg현대재고.Size = new System.Drawing.Size(1082, 730);
            this.tpg현대재고.TabIndex = 2;
            this.tpg현대재고.Text = "현대재고";
            this.tpg현대재고.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.oneGrid2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.splitContainer2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1082, 730);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // oneGrid2
            // 
            this.oneGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid2.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem3});
            this.oneGrid2.Location = new System.Drawing.Point(3, 3);
            this.oneGrid2.Name = "oneGrid2";
            this.oneGrid2.Size = new System.Drawing.Size(1076, 39);
            this.oneGrid2.TabIndex = 0;
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl3);
            this.oneGridItem3.Controls.Add(this.bpPanelControl5);
            this.oneGridItem3.Controls.Add(this.btn현대조회);
            this.oneGridItem3.Controls.Add(this.bpPanelControl2);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(1066, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.txt엔진모델);
            this.bpPanelControl3.Controls.Add(this.lbl엔진모델);
            this.bpPanelControl3.Location = new System.Drawing.Point(662, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // txt엔진모델
            // 
            this.txt엔진모델.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt엔진모델.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt엔진모델.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt엔진모델.Location = new System.Drawing.Point(106, 0);
            this.txt엔진모델.Name = "txt엔진모델";
            this.txt엔진모델.Size = new System.Drawing.Size(186, 21);
            this.txt엔진모델.TabIndex = 1;
            // 
            // lbl엔진모델
            // 
            this.lbl엔진모델.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl엔진모델.Location = new System.Drawing.Point(0, 0);
            this.lbl엔진모델.Name = "lbl엔진모델";
            this.lbl엔진모델.Size = new System.Drawing.Size(100, 23);
            this.lbl엔진모델.TabIndex = 0;
            this.lbl엔진모델.Text = "엔진모델";
            this.lbl엔진모델.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.cur호선수);
            this.bpPanelControl5.Controls.Add(this.lbl호선수);
            this.bpPanelControl5.Location = new System.Drawing.Point(368, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl5.TabIndex = 3;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // cur호선수
            // 
            this.cur호선수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur호선수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur호선수.DecimalValue = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.cur호선수.Dock = System.Windows.Forms.DockStyle.Right;
            this.cur호선수.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur호선수.Location = new System.Drawing.Point(106, 0);
            this.cur호선수.Name = "cur호선수";
            this.cur호선수.NullString = "0";
            this.cur호선수.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur호선수.Size = new System.Drawing.Size(186, 21);
            this.cur호선수.TabIndex = 1;
            this.cur호선수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl호선수
            // 
            this.lbl호선수.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl호선수.Location = new System.Drawing.Point(0, 0);
            this.lbl호선수.Name = "lbl호선수";
            this.lbl호선수.Size = new System.Drawing.Size(100, 23);
            this.lbl호선수.TabIndex = 0;
            this.lbl호선수.Text = "호선수";
            this.lbl호선수.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn현대조회
            // 
            this.btn현대조회.BackColor = System.Drawing.Color.Transparent;
            this.btn현대조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn현대조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn현대조회.Location = new System.Drawing.Point(296, 1);
            this.btn현대조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn현대조회.Name = "btn현대조회";
            this.btn현대조회.Size = new System.Drawing.Size(70, 19);
            this.btn현대조회.TabIndex = 1;
            this.btn현대조회.TabStop = false;
            this.btn현대조회.Text = "조회";
            this.btn현대조회.UseVisualStyleBackColor = false;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.cur현대개월수);
            this.bpPanelControl2.Controls.Add(this.lbl현대개월수);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 0;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // cur현대개월수
            // 
            this.cur현대개월수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur현대개월수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur현대개월수.DecimalValue = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.cur현대개월수.Dock = System.Windows.Forms.DockStyle.Right;
            this.cur현대개월수.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur현대개월수.Location = new System.Drawing.Point(106, 0);
            this.cur현대개월수.Name = "cur현대개월수";
            this.cur현대개월수.NullString = "0";
            this.cur현대개월수.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur현대개월수.Size = new System.Drawing.Size(186, 21);
            this.cur현대개월수.TabIndex = 1;
            this.cur현대개월수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl현대개월수
            // 
            this.lbl현대개월수.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl현대개월수.Location = new System.Drawing.Point(0, 0);
            this.lbl현대개월수.Name = "lbl현대개월수";
            this.lbl현대개월수.Size = new System.Drawing.Size(100, 23);
            this.lbl현대개월수.TabIndex = 0;
            this.lbl현대개월수.Text = "개월수";
            this.lbl현대개월수.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 48);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._flex현대재고H);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._flex현대재고L);
            this.splitContainer2.Size = new System.Drawing.Size(1076, 679);
            this.splitContainer2.SplitterDistance = 358;
            this.splitContainer2.TabIndex = 1;
            // 
            // _flex현대재고H
            // 
            this._flex현대재고H.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex현대재고H.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex현대재고H.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex현대재고H.AutoResize = false;
            this._flex현대재고H.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex현대재고H.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex현대재고H.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex현대재고H.EnabledHeaderCheck = true;
            this._flex현대재고H.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex현대재고H.Location = new System.Drawing.Point(0, 0);
            this._flex현대재고H.Name = "_flex현대재고H";
            this._flex현대재고H.Rows.Count = 1;
            this._flex현대재고H.Rows.DefaultSize = 20;
            this._flex현대재고H.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex현대재고H.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex현대재고H.ShowSort = false;
            this._flex현대재고H.Size = new System.Drawing.Size(358, 679);
            this._flex현대재고H.StyleInfo = resources.GetString("_flex현대재고H.StyleInfo");
            this._flex현대재고H.TabIndex = 0;
            this._flex현대재고H.UseGridCalculator = true;
            // 
            // _flex현대재고L
            // 
            this._flex현대재고L.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex현대재고L.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex현대재고L.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex현대재고L.AutoResize = false;
            this._flex현대재고L.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex현대재고L.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex현대재고L.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex현대재고L.EnabledHeaderCheck = true;
            this._flex현대재고L.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex현대재고L.Location = new System.Drawing.Point(0, 0);
            this._flex현대재고L.Name = "_flex현대재고L";
            this._flex현대재고L.Rows.Count = 1;
            this._flex현대재고L.Rows.DefaultSize = 20;
            this._flex현대재고L.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex현대재고L.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex현대재고L.ShowSort = false;
            this._flex현대재고L.Size = new System.Drawing.Size(714, 679);
            this._flex현대재고L.StyleInfo = resources.GetString("_flex현대재고L.StyleInfo");
            this._flex현대재고L.TabIndex = 0;
            this._flex현대재고L.UseGridCalculator = true;
            // 
            // ctx중분류
            // 
            this.ctx중분류.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx중분류.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
            this.ctx중분류.Location = new System.Drawing.Point(106, 0);
            this.ctx중분류.Name = "ctx중분류";
            this.ctx중분류.Size = new System.Drawing.Size(186, 21);
            this.ctx중분류.TabIndex = 1;
            this.ctx중분류.TabStop = false;
            this.ctx중분류.Text = "bpCodeTextBox1";
            // 
            // P_CZ_MM_STOCK_PO_RPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "P_CZ_MM_STOCK_PO_RPT";
            this.Size = new System.Drawing.Size(1090, 796);
            this.TitleText = "P_CZ_MM_STOCK_PO_RPT";
            this.mDataArea.ResumeLayout(false);
            this.tpgMain.ResumeLayout(false);
            this.tpg재고발주현황.ResumeLayout(false);
            this.tpg기자재재고.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur빈도수)).EndInit();
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur기자재개월수)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex기자재재고H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex기자재재고L)).EndInit();
            this.tpg현대재고.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.oneGridItem3.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl3.PerformLayout();
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur호선수)).EndInit();
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur현대개월수)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex현대재고H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex현대재고L)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Duzon.Common.Controls.TabControlExt tpgMain;
        private System.Windows.Forms.TabPage tpg재고발주현황;
        private System.Windows.Forms.TabPage tpg기자재재고;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.Controls.WebBrowserExt webBrowserExt1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flex기자재재고H;
        private Dass.FlexGrid.FlexGrid _flex기자재재고L;
        private System.Windows.Forms.TabPage tpg현대재고;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Dass.FlexGrid.FlexGrid _flex현대재고H;
        private Dass.FlexGrid.FlexGrid _flex현대재고L;
        private Duzon.Common.Controls.RoundedButton btn기자재조회;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.CurrencyTextBox cur기자재개월수;
        private Duzon.Common.Controls.LabelExt lbl기자재개월수;
        private Duzon.Common.Controls.RoundedButton btn현대조회;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.LabelExt lbl현대개월수;
        private Duzon.Common.Controls.CurrencyTextBox cur현대개월수;
        private Duzon.Common.Controls.PanelEx pnlHide;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.LabelExt lbl엔진모델;
        private Duzon.Common.Controls.TextBoxExt txt엔진모델;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.CurrencyTextBox cur빈도수;
        private Duzon.Common.Controls.LabelExt lbl빈도수;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.Controls.CurrencyTextBox cur호선수;
        private Duzon.Common.Controls.LabelExt lbl호선수;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.Controls.LabelExt lbl중분류;
        private Duzon.Common.BpControls.BpCodeTextBox ctx중분류;
    }
}
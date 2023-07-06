namespace cz
{
    partial class P_CZ_BI_QTN_SEND_DAY_CHART
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_BI_QTN_SEND_DAY_CHART));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.cur근무일수 = new Duzon.Common.Controls.CurrencyTextBox();
			this.lbl근무일수 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp대상기간 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl대상기간 = new Duzon.Common.Controls.LabelExt();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpg팀별견적현황 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.chart견적소요일 = new Duzon.DASS.Erpu.Windows.FX.UChart();
			this.imagePanel2 = new Duzon.Common.Controls.ImagePanel(this.components);
			this._flex팀별견적현황 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg담당자별견적현황 = new System.Windows.Forms.TabPage();
			this.imagePanel3 = new Duzon.Common.Controls.ImagePanel(this.components);
			this._flex담당자별견적현황 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg매출처별견적현황 = new System.Windows.Forms.TabPage();
			this.imagePanel5 = new Duzon.Common.Controls.ImagePanel(this.components);
			this._flex매출처별견적현황 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg고객문의추이 = new System.Windows.Forms.TabPage();
			this.imagePanel4 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.chart고객문의추이 = new Duzon.DASS.Erpu.Windows.FX.UChart();
			this.tpgPivot = new System.Windows.Forms.TabPage();
			this._pivot견적현황 = new Duzon.BizOn.Windows.PivotGrid.PivotGrid();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl견적유형 = new Duzon.Common.Controls.LabelExt();
			this.cbo견적유형 = new Duzon.Common.Controls.DropDownComboBox();
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur근무일수)).BeginInit();
			this.bpPanelControl1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpg팀별견적현황.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.imagePanel1.SuspendLayout();
			this.imagePanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex팀별견적현황)).BeginInit();
			this.tpg담당자별견적현황.SuspendLayout();
			this.imagePanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex담당자별견적현황)).BeginInit();
			this.tpg매출처별견적현황.SuspendLayout();
			this.imagePanel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex매출처별견적현황)).BeginInit();
			this.tpg고객문의추이.SuspendLayout();
			this.imagePanel4.SuspendLayout();
			this.tpgPivot.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
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
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.084656F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.91534F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1090, 756);
			this.tableLayoutPanel1.TabIndex = 0;
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
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.cur근무일수);
			this.bpPanelControl3.Controls.Add(this.lbl근무일수);
			this.bpPanelControl3.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 4;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// cur근무일수
			// 
			this.cur근무일수.BackColor = System.Drawing.SystemColors.Control;
			this.cur근무일수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur근무일수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur근무일수.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur근무일수.Dock = System.Windows.Forms.DockStyle.Right;
			this.cur근무일수.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur근무일수.Location = new System.Drawing.Point(106, 0);
			this.cur근무일수.Name = "cur근무일수";
			this.cur근무일수.NullString = "0";
			this.cur근무일수.ReadOnly = true;
			this.cur근무일수.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur근무일수.Size = new System.Drawing.Size(186, 21);
			this.cur근무일수.TabIndex = 1;
			this.cur근무일수.TabStop = false;
			this.cur근무일수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lbl근무일수
			// 
			this.lbl근무일수.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl근무일수.Location = new System.Drawing.Point(0, 0);
			this.lbl근무일수.Name = "lbl근무일수";
			this.lbl근무일수.Size = new System.Drawing.Size(100, 23);
			this.lbl근무일수.TabIndex = 0;
			this.lbl근무일수.Text = "근무일수";
			this.lbl근무일수.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.dtp대상기간);
			this.bpPanelControl1.Controls.Add(this.lbl대상기간);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 3;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// dtp대상기간
			// 
			this.dtp대상기간.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp대상기간.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp대상기간.IsNecessaryCondition = true;
			this.dtp대상기간.Location = new System.Drawing.Point(107, 0);
			this.dtp대상기간.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp대상기간.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp대상기간.Name = "dtp대상기간";
			this.dtp대상기간.Size = new System.Drawing.Size(185, 21);
			this.dtp대상기간.TabIndex = 1;
			// 
			// lbl대상기간
			// 
			this.lbl대상기간.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl대상기간.Location = new System.Drawing.Point(0, 0);
			this.lbl대상기간.Name = "lbl대상기간";
			this.lbl대상기간.Size = new System.Drawing.Size(100, 23);
			this.lbl대상기간.TabIndex = 0;
			this.lbl대상기간.Text = "대상기간";
			this.lbl대상기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tpg팀별견적현황);
			this.tabControl1.Controls.Add(this.tpg담당자별견적현황);
			this.tabControl1.Controls.Add(this.tpg매출처별견적현황);
			this.tabControl1.Controls.Add(this.tpg고객문의추이);
			this.tabControl1.Controls.Add(this.tpgPivot);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(3, 49);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1084, 704);
			this.tabControl1.TabIndex = 1;
			// 
			// tpg팀별견적현황
			// 
			this.tpg팀별견적현황.Controls.Add(this.splitContainer1);
			this.tpg팀별견적현황.Location = new System.Drawing.Point(4, 22);
			this.tpg팀별견적현황.Name = "tpg팀별견적현황";
			this.tpg팀별견적현황.Padding = new System.Windows.Forms.Padding(3);
			this.tpg팀별견적현황.Size = new System.Drawing.Size(1076, 678);
			this.tpg팀별견적현황.TabIndex = 0;
			this.tpg팀별견적현황.Text = "팀별견적현황";
			this.tpg팀별견적현황.UseVisualStyleBackColor = true;
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
			this.splitContainer1.Panel1.Controls.Add(this.imagePanel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.imagePanel2);
			this.splitContainer1.Size = new System.Drawing.Size(1070, 672);
			this.splitContainer1.SplitterDistance = 320;
			this.splitContainer1.TabIndex = 0;
			// 
			// imagePanel1
			// 
			this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel1.Controls.Add(this.chart견적소요일);
			this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel1.LeftImage = null;
			this.imagePanel1.Location = new System.Drawing.Point(0, 0);
			this.imagePanel1.Name = "imagePanel1";
			this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel1.PatternImage = null;
			this.imagePanel1.RightImage = null;
			this.imagePanel1.Size = new System.Drawing.Size(1070, 320);
			this.imagePanel1.TabIndex = 0;
			this.imagePanel1.TitleText = "월별견적소요일";
			// 
			// chart견적소요일
			// 
			this.chart견적소요일.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.chart견적소요일.Location = new System.Drawing.Point(3, 28);
			this.chart견적소요일.Name = "chart견적소요일";
			this.chart견적소요일.Size = new System.Drawing.Size(1064, 289);
			this.chart견적소요일.TabIndex = 0;
			this.chart견적소요일.Text = "uChart1";
			// 
			// imagePanel2
			// 
			this.imagePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel2.Controls.Add(this._flex팀별견적현황);
			this.imagePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel2.LeftImage = null;
			this.imagePanel2.Location = new System.Drawing.Point(0, 0);
			this.imagePanel2.Name = "imagePanel2";
			this.imagePanel2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel2.PatternImage = null;
			this.imagePanel2.RightImage = null;
			this.imagePanel2.Size = new System.Drawing.Size(1070, 348);
			this.imagePanel2.TabIndex = 0;
			this.imagePanel2.TitleText = "팀별견적현황";
			// 
			// _flex팀별견적현황
			// 
			this._flex팀별견적현황.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex팀별견적현황.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex팀별견적현황.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex팀별견적현황.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex팀별견적현황.AutoResize = false;
			this._flex팀별견적현황.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex팀별견적현황.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex팀별견적현황.EnabledHeaderCheck = true;
			this._flex팀별견적현황.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex팀별견적현황.Location = new System.Drawing.Point(3, 28);
			this._flex팀별견적현황.Name = "_flex팀별견적현황";
			this._flex팀별견적현황.Rows.Count = 1;
			this._flex팀별견적현황.Rows.DefaultSize = 20;
			this._flex팀별견적현황.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex팀별견적현황.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex팀별견적현황.ShowSort = false;
			this._flex팀별견적현황.Size = new System.Drawing.Size(1064, 317);
			this._flex팀별견적현황.StyleInfo = resources.GetString("_flex팀별견적현황.StyleInfo");
			this._flex팀별견적현황.TabIndex = 0;
			this._flex팀별견적현황.UseGridCalculator = true;
			// 
			// tpg담당자별견적현황
			// 
			this.tpg담당자별견적현황.Controls.Add(this.imagePanel3);
			this.tpg담당자별견적현황.Location = new System.Drawing.Point(4, 22);
			this.tpg담당자별견적현황.Name = "tpg담당자별견적현황";
			this.tpg담당자별견적현황.Padding = new System.Windows.Forms.Padding(3);
			this.tpg담당자별견적현황.Size = new System.Drawing.Size(1076, 678);
			this.tpg담당자별견적현황.TabIndex = 1;
			this.tpg담당자별견적현황.Text = "담당자별견적현황";
			this.tpg담당자별견적현황.UseVisualStyleBackColor = true;
			// 
			// imagePanel3
			// 
			this.imagePanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel3.Controls.Add(this._flex담당자별견적현황);
			this.imagePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel3.LeftImage = null;
			this.imagePanel3.Location = new System.Drawing.Point(3, 3);
			this.imagePanel3.Name = "imagePanel3";
			this.imagePanel3.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel3.PatternImage = null;
			this.imagePanel3.RightImage = null;
			this.imagePanel3.Size = new System.Drawing.Size(1070, 672);
			this.imagePanel3.TabIndex = 0;
			this.imagePanel3.TitleText = "담당자별견적현황";
			// 
			// _flex담당자별견적현황
			// 
			this._flex담당자별견적현황.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex담당자별견적현황.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex담당자별견적현황.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex담당자별견적현황.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex담당자별견적현황.AutoResize = false;
			this._flex담당자별견적현황.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex담당자별견적현황.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex담당자별견적현황.EnabledHeaderCheck = true;
			this._flex담당자별견적현황.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex담당자별견적현황.Location = new System.Drawing.Point(3, 28);
			this._flex담당자별견적현황.Name = "_flex담당자별견적현황";
			this._flex담당자별견적현황.Rows.Count = 1;
			this._flex담당자별견적현황.Rows.DefaultSize = 20;
			this._flex담당자별견적현황.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex담당자별견적현황.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex담당자별견적현황.ShowSort = false;
			this._flex담당자별견적현황.Size = new System.Drawing.Size(1064, 641);
			this._flex담당자별견적현황.StyleInfo = resources.GetString("_flex담당자별견적현황.StyleInfo");
			this._flex담당자별견적현황.TabIndex = 0;
			this._flex담당자별견적현황.UseGridCalculator = true;
			// 
			// tpg매출처별견적현황
			// 
			this.tpg매출처별견적현황.Controls.Add(this.imagePanel5);
			this.tpg매출처별견적현황.Location = new System.Drawing.Point(4, 22);
			this.tpg매출처별견적현황.Name = "tpg매출처별견적현황";
			this.tpg매출처별견적현황.Size = new System.Drawing.Size(1076, 678);
			this.tpg매출처별견적현황.TabIndex = 3;
			this.tpg매출처별견적현황.Text = "매출처별견적현황";
			this.tpg매출처별견적현황.UseVisualStyleBackColor = true;
			// 
			// imagePanel5
			// 
			this.imagePanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel5.Controls.Add(this._flex매출처별견적현황);
			this.imagePanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel5.LeftImage = null;
			this.imagePanel5.Location = new System.Drawing.Point(0, 0);
			this.imagePanel5.Name = "imagePanel5";
			this.imagePanel5.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel5.PatternImage = null;
			this.imagePanel5.RightImage = null;
			this.imagePanel5.Size = new System.Drawing.Size(1076, 678);
			this.imagePanel5.TabIndex = 0;
			this.imagePanel5.TitleText = "매출처별견적현황";
			// 
			// _flex매출처별견적현황
			// 
			this._flex매출처별견적현황.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex매출처별견적현황.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex매출처별견적현황.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex매출처별견적현황.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex매출처별견적현황.AutoResize = false;
			this._flex매출처별견적현황.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex매출처별견적현황.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex매출처별견적현황.EnabledHeaderCheck = true;
			this._flex매출처별견적현황.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex매출처별견적현황.Location = new System.Drawing.Point(3, 28);
			this._flex매출처별견적현황.Name = "_flex매출처별견적현황";
			this._flex매출처별견적현황.Rows.Count = 1;
			this._flex매출처별견적현황.Rows.DefaultSize = 20;
			this._flex매출처별견적현황.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex매출처별견적현황.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex매출처별견적현황.ShowSort = false;
			this._flex매출처별견적현황.Size = new System.Drawing.Size(1070, 647);
			this._flex매출처별견적현황.StyleInfo = resources.GetString("_flex매출처별견적현황.StyleInfo");
			this._flex매출처별견적현황.TabIndex = 0;
			this._flex매출처별견적현황.UseGridCalculator = true;
			// 
			// tpg고객문의추이
			// 
			this.tpg고객문의추이.Controls.Add(this.imagePanel4);
			this.tpg고객문의추이.Location = new System.Drawing.Point(4, 22);
			this.tpg고객문의추이.Name = "tpg고객문의추이";
			this.tpg고객문의추이.Size = new System.Drawing.Size(1076, 678);
			this.tpg고객문의추이.TabIndex = 2;
			this.tpg고객문의추이.Text = "고객문의추이";
			this.tpg고객문의추이.UseVisualStyleBackColor = true;
			// 
			// imagePanel4
			// 
			this.imagePanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel4.Controls.Add(this.chart고객문의추이);
			this.imagePanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel4.LeftImage = null;
			this.imagePanel4.Location = new System.Drawing.Point(0, 0);
			this.imagePanel4.Name = "imagePanel4";
			this.imagePanel4.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel4.PatternImage = null;
			this.imagePanel4.RightImage = null;
			this.imagePanel4.Size = new System.Drawing.Size(1076, 678);
			this.imagePanel4.TabIndex = 1;
			this.imagePanel4.TitleText = "고객문의추이";
			// 
			// chart고객문의추이
			// 
			this.chart고객문의추이.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.chart고객문의추이.Location = new System.Drawing.Point(3, 27);
			this.chart고객문의추이.Name = "chart고객문의추이";
			this.chart고객문의추이.Size = new System.Drawing.Size(1070, 648);
			this.chart고객문의추이.TabIndex = 0;
			this.chart고객문의추이.Text = "uChart1";
			// 
			// tpgPivot
			// 
			this.tpgPivot.Controls.Add(this._pivot견적현황);
			this.tpgPivot.Location = new System.Drawing.Point(4, 22);
			this.tpgPivot.Name = "tpgPivot";
			this.tpgPivot.Size = new System.Drawing.Size(1076, 678);
			this.tpgPivot.TabIndex = 4;
			this.tpgPivot.Text = "Pivot";
			this.tpgPivot.UseVisualStyleBackColor = true;
			// 
			// _pivot견적현황
			// 
			this._pivot견적현황.Dock = System.Windows.Forms.DockStyle.Fill;
			this._pivot견적현황.LocalLanguage = Duzon.BizOn.Windows.PivotGrid.LocalLanguage.KOR;
			this._pivot견적현황.Location = new System.Drawing.Point(0, 0);
			this._pivot견적현황.Name = "_pivot견적현황";
			this._pivot견적현황.Size = new System.Drawing.Size(1076, 678);
			this._pivot견적현황.TabIndex = 0;
			this._pivot견적현황.Text = "pivotGrid1";
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.cbo견적유형);
			this.bpPanelControl2.Controls.Add(this.lbl견적유형);
			this.bpPanelControl2.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 5;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// lbl견적유형
			// 
			this.lbl견적유형.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl견적유형.Location = new System.Drawing.Point(0, 0);
			this.lbl견적유형.Name = "lbl견적유형";
			this.lbl견적유형.Size = new System.Drawing.Size(100, 23);
			this.lbl견적유형.TabIndex = 0;
			this.lbl견적유형.Text = "견적유형";
			this.lbl견적유형.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo견적유형
			// 
			this.cbo견적유형.AutoDropDown = true;
			this.cbo견적유형.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo견적유형.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo견적유형.FormattingEnabled = true;
			this.cbo견적유형.ItemHeight = 12;
			this.cbo견적유형.Location = new System.Drawing.Point(106, 0);
			this.cbo견적유형.Name = "cbo견적유형";
			this.cbo견적유형.Size = new System.Drawing.Size(186, 20);
			this.cbo견적유형.TabIndex = 1;
			// 
			// P_CZ_BI_QTN_SEND_DAY_CHART
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_BI_QTN_SEND_DAY_CHART";
			this.Size = new System.Drawing.Size(1090, 796);
			this.TitleText = "견적소요일분석";
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur근무일수)).EndInit();
			this.bpPanelControl1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tpg팀별견적현황.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.imagePanel1.ResumeLayout(false);
			this.imagePanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex팀별견적현황)).EndInit();
			this.tpg담당자별견적현황.ResumeLayout(false);
			this.imagePanel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex담당자별견적현황)).EndInit();
			this.tpg매출처별견적현황.ResumeLayout(false);
			this.imagePanel5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex매출처별견적현황)).EndInit();
			this.tpg고객문의추이.ResumeLayout(false);
			this.imagePanel4.ResumeLayout(false);
			this.tpgPivot.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpg팀별견적현황;
        private System.Windows.Forms.TabPage tpg담당자별견적현황;
        private System.Windows.Forms.TabPage tpg고객문의추이;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Duzon.DASS.Erpu.Windows.FX.UChart chart견적소요일;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.PeriodPicker dtp대상기간;
        private Duzon.Common.Controls.LabelExt lbl대상기간;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.CurrencyTextBox cur근무일수;
        private Duzon.Common.Controls.LabelExt lbl근무일수;
        private Duzon.Common.Controls.ImagePanel imagePanel1;
        private Duzon.Common.Controls.ImagePanel imagePanel2;
        private Dass.FlexGrid.FlexGrid _flex팀별견적현황;
        private Duzon.Common.Controls.ImagePanel imagePanel3;
        private Dass.FlexGrid.FlexGrid _flex담당자별견적현황;
        private Duzon.DASS.Erpu.Windows.FX.UChart chart고객문의추이;
        private Duzon.Common.Controls.ImagePanel imagePanel4;
        private System.Windows.Forms.TabPage tpg매출처별견적현황;
        private Duzon.Common.Controls.ImagePanel imagePanel5;
        private Dass.FlexGrid.FlexGrid _flex매출처별견적현황;
        private System.Windows.Forms.TabPage tpgPivot;
        private Duzon.BizOn.Windows.PivotGrid.PivotGrid _pivot견적현황;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.DropDownComboBox cbo견적유형;
		private Duzon.Common.Controls.LabelExt lbl견적유형;
	}
}
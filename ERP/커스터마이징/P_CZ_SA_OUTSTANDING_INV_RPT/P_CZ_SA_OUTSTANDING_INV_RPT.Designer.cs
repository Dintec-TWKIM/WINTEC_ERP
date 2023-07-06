namespace cz
{
    partial class P_CZ_SA_OUTSTANDING_INV_RPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_OUTSTANDING_INV_RPT));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx매출처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl매출처 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp기준년월 = new Duzon.Common.Controls.DatePicker();
            this.lbl기준년월 = new Duzon.Common.Controls.LabelExt();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgPivot = new System.Windows.Forms.TabPage();
            this._pivot미수금현황 = new Duzon.BizOn.Windows.PivotGrid.PivotGrid();
            this.tpgList = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flex미수금현황 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tpg상세내역 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._flex매출리스트 = new Dass.FlexGrid.FlexGrid(this.components);
            this._flex수금리스트 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tpg차트 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chart미수금 = new Duzon.DASS.Erpu.Windows.FX.UChart();
            this.chart회수율 = new Duzon.DASS.Erpu.Windows.FX.UChart();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp기준년월)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tpgPivot.SuspendLayout();
            this.tpgList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex미수금현황)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tpg상세내역.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex매출리스트)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex수금리스트)).BeginInit();
            this.tpg차트.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
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
            this.oneGrid1.Size = new System.Drawing.Size(1084, 41);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(1074, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.ctx매출처);
            this.bpPanelControl2.Controls.Add(this.lbl매출처);
            this.bpPanelControl2.Location = new System.Drawing.Point(199, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(293, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // ctx매출처
            // 
            this.ctx매출처.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx매출처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx매출처.Location = new System.Drawing.Point(106, 0);
            this.ctx매출처.Name = "ctx매출처";
            this.ctx매출처.Size = new System.Drawing.Size(187, 21);
            this.ctx매출처.TabIndex = 1;
            this.ctx매출처.TabStop = false;
            this.ctx매출처.Text = "bpCodeTextBox1";
            // 
            // lbl매출처
            // 
            this.lbl매출처.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl매출처.Location = new System.Drawing.Point(0, 0);
            this.lbl매출처.Name = "lbl매출처";
            this.lbl매출처.Size = new System.Drawing.Size(100, 23);
            this.lbl매출처.TabIndex = 0;
            this.lbl매출처.Text = "매출처";
            this.lbl매출처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp기준년월);
            this.bpPanelControl1.Controls.Add(this.lbl기준년월);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(195, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // dtp기준년월
            // 
            this.dtp기준년월.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp기준년월.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp기준년월.Location = new System.Drawing.Point(105, 0);
            this.dtp기준년월.Mask = "####/##";
            this.dtp기준년월.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp기준년월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp기준년월.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp기준년월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp기준년월.Modified = true;
            this.dtp기준년월.Name = "dtp기준년월";
            this.dtp기준년월.ShowUpDown = true;
            this.dtp기준년월.Size = new System.Drawing.Size(90, 21);
            this.dtp기준년월.TabIndex = 1;
            this.dtp기준년월.Value = new System.DateTime(((long)(0)));
            // 
            // lbl기준년월
            // 
            this.lbl기준년월.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl기준년월.Location = new System.Drawing.Point(0, 0);
            this.lbl기준년월.Name = "lbl기준년월";
            this.lbl기준년월.Size = new System.Drawing.Size(100, 23);
            this.lbl기준년월.TabIndex = 0;
            this.lbl기준년월.Text = "기준년월";
            this.lbl기준년월.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgPivot);
            this.tabControl1.Controls.Add(this.tpgList);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1084, 703);
            this.tabControl1.TabIndex = 1;
            // 
            // tpgPivot
            // 
            this.tpgPivot.Controls.Add(this._pivot미수금현황);
            this.tpgPivot.Location = new System.Drawing.Point(4, 22);
            this.tpgPivot.Name = "tpgPivot";
            this.tpgPivot.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPivot.Size = new System.Drawing.Size(1076, 677);
            this.tpgPivot.TabIndex = 0;
            this.tpgPivot.Text = "Pivot";
            this.tpgPivot.UseVisualStyleBackColor = true;
            // 
            // _pivot미수금현황
            // 
            this._pivot미수금현황.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pivot미수금현황.LocalLanguage = Duzon.BizOn.Windows.PivotGrid.LocalLanguage.KOR;
            this._pivot미수금현황.Location = new System.Drawing.Point(3, 3);
            this._pivot미수금현황.Name = "_pivot미수금현황";
            this._pivot미수금현황.Size = new System.Drawing.Size(1070, 671);
            this._pivot미수금현황.TabIndex = 0;
            this._pivot미수금현황.Text = "pivotGrid1";
            // 
            // tpgList
            // 
            this.tpgList.Controls.Add(this.splitContainer1);
            this.tpgList.Location = new System.Drawing.Point(4, 22);
            this.tpgList.Name = "tpgList";
            this.tpgList.Padding = new System.Windows.Forms.Padding(3);
            this.tpgList.Size = new System.Drawing.Size(1076, 677);
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
            this.splitContainer1.Panel1.Controls.Add(this._flex미수금현황);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer1.Size = new System.Drawing.Size(1070, 671);
            this.splitContainer1.SplitterDistance = 356;
            this.splitContainer1.TabIndex = 1;
            // 
            // _flex미수금현황
            // 
            this._flex미수금현황.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex미수금현황.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex미수금현황.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex미수금현황.AutoResize = false;
            this._flex미수금현황.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex미수금현황.Cursor = System.Windows.Forms.Cursors.Default;
            this._flex미수금현황.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex미수금현황.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex미수금현황.EnabledHeaderCheck = true;
            this._flex미수금현황.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex미수금현황.Location = new System.Drawing.Point(0, 0);
            this._flex미수금현황.Name = "_flex미수금현황";
            this._flex미수금현황.Rows.Count = 1;
            this._flex미수금현황.Rows.DefaultSize = 20;
            this._flex미수금현황.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex미수금현황.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex미수금현황.ShowSort = false;
            this._flex미수금현황.Size = new System.Drawing.Size(1070, 356);
            this._flex미수금현황.StyleInfo = resources.GetString("_flex미수금현황.StyleInfo");
            this._flex미수금현황.TabIndex = 0;
            this._flex미수금현황.UseGridCalculator = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tpg차트);
            this.tabControl2.Controls.Add(this.tpg상세내역);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1070, 311);
            this.tabControl2.TabIndex = 1;
            // 
            // tpg상세내역
            // 
            this.tpg상세내역.Controls.Add(this.splitContainer2);
            this.tpg상세내역.Location = new System.Drawing.Point(4, 22);
            this.tpg상세내역.Name = "tpg상세내역";
            this.tpg상세내역.Padding = new System.Windows.Forms.Padding(3);
            this.tpg상세내역.Size = new System.Drawing.Size(1062, 285);
            this.tpg상세내역.TabIndex = 0;
            this.tpg상세내역.Text = "상세내역";
            this.tpg상세내역.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._flex매출리스트);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._flex수금리스트);
            this.splitContainer2.Size = new System.Drawing.Size(1056, 279);
            this.splitContainer2.SplitterDistance = 524;
            this.splitContainer2.TabIndex = 0;
            // 
            // _flex매출리스트
            // 
            this._flex매출리스트.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex매출리스트.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex매출리스트.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex매출리스트.AutoResize = false;
            this._flex매출리스트.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex매출리스트.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex매출리스트.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex매출리스트.EnabledHeaderCheck = true;
            this._flex매출리스트.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex매출리스트.Location = new System.Drawing.Point(0, 0);
            this._flex매출리스트.Name = "_flex매출리스트";
            this._flex매출리스트.Rows.Count = 1;
            this._flex매출리스트.Rows.DefaultSize = 20;
            this._flex매출리스트.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex매출리스트.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex매출리스트.ShowSort = false;
            this._flex매출리스트.Size = new System.Drawing.Size(524, 279);
            this._flex매출리스트.StyleInfo = resources.GetString("_flex매출리스트.StyleInfo");
            this._flex매출리스트.TabIndex = 0;
            this._flex매출리스트.UseGridCalculator = true;
            // 
            // _flex수금리스트
            // 
            this._flex수금리스트.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex수금리스트.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex수금리스트.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex수금리스트.AutoResize = false;
            this._flex수금리스트.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex수금리스트.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex수금리스트.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex수금리스트.EnabledHeaderCheck = true;
            this._flex수금리스트.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex수금리스트.Location = new System.Drawing.Point(0, 0);
            this._flex수금리스트.Name = "_flex수금리스트";
            this._flex수금리스트.Rows.Count = 1;
            this._flex수금리스트.Rows.DefaultSize = 20;
            this._flex수금리스트.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex수금리스트.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex수금리스트.ShowSort = false;
            this._flex수금리스트.Size = new System.Drawing.Size(528, 279);
            this._flex수금리스트.StyleInfo = resources.GetString("_flex수금리스트.StyleInfo");
            this._flex수금리스트.TabIndex = 0;
            this._flex수금리스트.UseGridCalculator = true;
            // 
            // tpg차트
            // 
            this.tpg차트.Controls.Add(this.tableLayoutPanel2);
            this.tpg차트.Location = new System.Drawing.Point(4, 22);
            this.tpg차트.Name = "tpg차트";
            this.tpg차트.Padding = new System.Windows.Forms.Padding(3);
            this.tpg차트.Size = new System.Drawing.Size(1062, 285);
            this.tpg차트.TabIndex = 1;
            this.tpg차트.Text = "차트";
            this.tpg차트.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.chart미수금, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.chart회수율, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1056, 279);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // chart미수금
            // 
            this.chart미수금.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart미수금.Location = new System.Drawing.Point(3, 3);
            this.chart미수금.Name = "chart미수금";
            this.chart미수금.Size = new System.Drawing.Size(522, 273);
            this.chart미수금.TabIndex = 0;
            this.chart미수금.Text = "uChart1";
            // 
            // chart회수율
            // 
            this.chart회수율.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart회수율.Location = new System.Drawing.Point(531, 3);
            this.chart회수율.Name = "chart회수율";
            this.chart회수율.Size = new System.Drawing.Size(522, 273);
            this.chart회수율.TabIndex = 1;
            this.chart회수율.Text = "uChart2";
            // 
            // P_CZ_SA_OUTSTANDING_INV_RPT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_CZ_SA_OUTSTANDING_INV_RPT";
            this.Size = new System.Drawing.Size(1090, 796);
            this.TitleText = "월별미수금현황";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp기준년월)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tpgPivot.ResumeLayout(false);
            this.tpgList.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex미수금현황)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tpg상세내역.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex매출리스트)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex수금리스트)).EndInit();
            this.tpg차트.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.LabelExt lbl기준년월;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgPivot;
        private System.Windows.Forms.TabPage tpgList;
        private Duzon.Common.Controls.DatePicker dtp기준년월;
        private Duzon.BizOn.Windows.PivotGrid.PivotGrid _pivot미수금현황;
        private Dass.FlexGrid.FlexGrid _flex미수금현황;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpCodeTextBox ctx매출처;
        private Duzon.Common.Controls.LabelExt lbl매출처;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Dass.FlexGrid.FlexGrid _flex매출리스트;
        private Dass.FlexGrid.FlexGrid _flex수금리스트;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tpg상세내역;
        private System.Windows.Forms.TabPage tpg차트;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Duzon.DASS.Erpu.Windows.FX.UChart chart미수금;
        private Duzon.DASS.Erpu.Windows.FX.UChart chart회수율;
    }
}
namespace cz
{
    partial class P_CZ_BI_INQ_INPUT_TIME_CHART
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_BI_INQ_INPUT_TIME_CHART));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.cur근무일수 = new Duzon.Common.Controls.CurrencyTextBox();
            this.lbl근무일수 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp대상기간 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl대상기간 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp기준년월 = new Duzon.Common.Controls.DatePicker();
            this.lbl기준년월 = new Duzon.Common.Controls.LabelExt();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.imagePanel3 = new Duzon.Common.Controls.ImagePanel(this.components);
            this.chart월별입력시간 = new Duzon.DASS.Erpu.Windows.FX.UChart();
            this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
            this._flex부서별 = new Dass.FlexGrid.FlexGrid(this.components);
            this.imagePanel2 = new Duzon.Common.Controls.ImagePanel(this.components);
            this._flex담당자별 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur근무일수)).BeginInit();
            this.bpPanelControl1.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp기준년월)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.imagePanel3.SuspendLayout();
            this.imagePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex부서별)).BeginInit();
            this.imagePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex담당자별)).BeginInit();
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
            this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
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
            this.oneGrid1.Size = new System.Drawing.Size(1084, 40);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl3);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
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
            this.bpPanelControl3.Location = new System.Drawing.Point(495, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 3;
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
            this.bpPanelControl1.Location = new System.Drawing.Point(201, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 2;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // dtp대상기간
            // 
            this.dtp대상기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp대상기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp대상기간.Enabled = false;
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
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.dtp기준년월);
            this.bpPanelControl2.Controls.Add(this.lbl기준년월);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(197, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // dtp기준년월
            // 
            this.dtp기준년월.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp기준년월.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp기준년월.Location = new System.Drawing.Point(106, 0);
            this.dtp기준년월.Mask = "####/##";
            this.dtp기준년월.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp기준년월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp기준년월.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp기준년월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp기준년월.Modified = true;
            this.dtp기준년월.Name = "dtp기준년월";
            this.dtp기준년월.ShowUpDown = true;
            this.dtp기준년월.Size = new System.Drawing.Size(91, 21);
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
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 49);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.imagePanel3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer2.Size = new System.Drawing.Size(1084, 704);
            this.splitContainer2.SplitterDistance = 357;
            this.splitContainer2.TabIndex = 1;
            // 
            // imagePanel3
            // 
            this.imagePanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.imagePanel3.Controls.Add(this.chart월별입력시간);
            this.imagePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel3.LeftImage = null;
            this.imagePanel3.Location = new System.Drawing.Point(0, 0);
            this.imagePanel3.Name = "imagePanel3";
            this.imagePanel3.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
            this.imagePanel3.PatternImage = null;
            this.imagePanel3.RightImage = null;
            this.imagePanel3.Size = new System.Drawing.Size(1084, 357);
            this.imagePanel3.TabIndex = 3;
            this.imagePanel3.TitleText = "평균 입력 소요 시간";
            // 
            // chart월별입력시간
            // 
            this.chart월별입력시간.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart월별입력시간.Location = new System.Drawing.Point(3, 28);
            this.chart월별입력시간.Name = "chart월별입력시간";
            this.chart월별입력시간.Size = new System.Drawing.Size(1078, 326);
            this.chart월별입력시간.TabIndex = 1;
            this.chart월별입력시간.Text = "uChart1";
            // 
            // imagePanel1
            // 
            this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.imagePanel1.Controls.Add(this._flex부서별);
            this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel1.LeftImage = null;
            this.imagePanel1.Location = new System.Drawing.Point(3, 3);
            this.imagePanel1.Name = "imagePanel1";
            this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
            this.imagePanel1.PatternImage = null;
            this.imagePanel1.RightImage = null;
            this.imagePanel1.Size = new System.Drawing.Size(536, 337);
            this.imagePanel1.TabIndex = 1;
            this.imagePanel1.TitleText = "부서별 업무 현황";
            // 
            // _flex부서별
            // 
            this._flex부서별.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex부서별.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex부서별.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex부서별.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._flex부서별.AutoResize = false;
            this._flex부서별.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex부서별.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex부서별.EnabledHeaderCheck = true;
            this._flex부서별.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex부서별.Location = new System.Drawing.Point(4, 28);
            this._flex부서별.Name = "_flex부서별";
            this._flex부서별.Rows.Count = 1;
            this._flex부서별.Rows.DefaultSize = 20;
            this._flex부서별.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex부서별.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex부서별.ShowSort = false;
            this._flex부서별.Size = new System.Drawing.Size(529, 306);
            this._flex부서별.StyleInfo = resources.GetString("_flex부서별.StyleInfo");
            this._flex부서별.TabIndex = 0;
            this._flex부서별.UseGridCalculator = true;
            // 
            // imagePanel2
            // 
            this.imagePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.imagePanel2.Controls.Add(this._flex담당자별);
            this.imagePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel2.LeftImage = null;
            this.imagePanel2.Location = new System.Drawing.Point(545, 3);
            this.imagePanel2.Name = "imagePanel2";
            this.imagePanel2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
            this.imagePanel2.PatternImage = null;
            this.imagePanel2.RightImage = null;
            this.imagePanel2.Size = new System.Drawing.Size(536, 337);
            this.imagePanel2.TabIndex = 1;
            this.imagePanel2.TitleText = "담당자별 업무 현황";
            // 
            // _flex담당자별
            // 
            this._flex담당자별.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex담당자별.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex담당자별.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex담당자별.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._flex담당자별.AutoResize = false;
            this._flex담당자별.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex담당자별.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex담당자별.EnabledHeaderCheck = true;
            this._flex담당자별.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex담당자별.Location = new System.Drawing.Point(3, 28);
            this._flex담당자별.Name = "_flex담당자별";
            this._flex담당자별.Rows.Count = 1;
            this._flex담당자별.Rows.DefaultSize = 20;
            this._flex담당자별.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex담당자별.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex담당자별.ShowSort = false;
            this._flex담당자별.Size = new System.Drawing.Size(530, 306);
            this._flex담당자별.StyleInfo = resources.GetString("_flex담당자별.StyleInfo");
            this._flex담당자별.TabIndex = 0;
            this._flex담당자별.UseGridCalculator = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.imagePanel2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.imagePanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1084, 343);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // P_CZ_BI_INQ_INPUT_TIME_CHART
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_CZ_BI_INQ_INPUT_TIME_CHART";
            this.Size = new System.Drawing.Size(1090, 796);
            this.TitleText = "입력담당자업무분석";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur근무일수)).EndInit();
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp기준년월)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.imagePanel3.ResumeLayout(false);
            this.imagePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex부서별)).EndInit();
            this.imagePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex담당자별)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Dass.FlexGrid.FlexGrid _flex부서별;
        private Dass.FlexGrid.FlexGrid _flex담당자별;
        private Duzon.DASS.Erpu.Windows.FX.UChart chart월별입력시간;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.DatePicker dtp기준년월;
        private Duzon.Common.Controls.LabelExt lbl기준년월;
        private Duzon.Common.Controls.ImagePanel imagePanel1;
        private Duzon.Common.Controls.ImagePanel imagePanel2;
        private Duzon.Common.Controls.ImagePanel imagePanel3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.PeriodPicker dtp대상기간;
        private Duzon.Common.Controls.LabelExt lbl대상기간;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.CurrencyTextBox cur근무일수;
        private Duzon.Common.Controls.LabelExt lbl근무일수;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
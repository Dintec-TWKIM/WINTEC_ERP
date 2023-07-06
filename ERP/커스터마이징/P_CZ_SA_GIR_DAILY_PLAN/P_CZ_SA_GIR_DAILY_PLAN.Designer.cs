namespace cz
{
    partial class P_CZ_SA_GIR_DAILY_PLAN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_GIR_DAILY_PLAN));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControlExt1 = new Duzon.Common.Controls.TabControlExt();
            this.tpg업무계획 = new System.Windows.Forms.TabPage();
            this._flex업무계획 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tpg본선선적 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flex본선선적H = new Dass.FlexGrid.FlexGrid(this.components);
            this._flex본선선적L = new Dass.FlexGrid.FlexGrid(this.components);
            this.tpg전달수령 = new System.Windows.Forms.TabPage();
            this._flex전달수령 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tpg인원현황 = new System.Windows.Forms.TabPage();
            this._flex인원현황 = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt협조전번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl협조전번호 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp작업일자 = new Duzon.Common.Controls.DatePicker();
            this.lbl작업일자 = new Duzon.Common.Controls.LabelExt();
            this.btn자동등록 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControlExt1.SuspendLayout();
            this.tpg업무계획.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex업무계획)).BeginInit();
            this.tpg본선선적.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex본선선적H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex본선선적L)).BeginInit();
            this.tpg전달수령.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex전달수령)).BeginInit();
            this.tpg인원현황.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex인원현황)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp작업일자)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(1259, 816);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabControlExt1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1259, 816);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tabControlExt1
            // 
            this.tabControlExt1.Controls.Add(this.tpg업무계획);
            this.tabControlExt1.Controls.Add(this.tpg본선선적);
            this.tabControlExt1.Controls.Add(this.tpg전달수령);
            this.tabControlExt1.Controls.Add(this.tpg인원현황);
            this.tabControlExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlExt1.Location = new System.Drawing.Point(3, 48);
            this.tabControlExt1.Name = "tabControlExt1";
            this.tabControlExt1.SelectedIndex = 0;
            this.tabControlExt1.Size = new System.Drawing.Size(1253, 765);
            this.tabControlExt1.TabIndex = 0;
            // 
            // tpg업무계획
            // 
            this.tpg업무계획.Controls.Add(this._flex업무계획);
            this.tpg업무계획.Location = new System.Drawing.Point(4, 22);
            this.tpg업무계획.Name = "tpg업무계획";
            this.tpg업무계획.Padding = new System.Windows.Forms.Padding(3);
            this.tpg업무계획.Size = new System.Drawing.Size(1245, 739);
            this.tpg업무계획.TabIndex = 0;
            this.tpg업무계획.Text = "업무계획";
            this.tpg업무계획.UseVisualStyleBackColor = true;
            // 
            // _flex업무계획
            // 
            this._flex업무계획.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex업무계획.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex업무계획.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex업무계획.AutoResize = false;
            this._flex업무계획.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex업무계획.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex업무계획.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex업무계획.EnabledHeaderCheck = true;
            this._flex업무계획.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex업무계획.Location = new System.Drawing.Point(3, 3);
            this._flex업무계획.Name = "_flex업무계획";
            this._flex업무계획.Rows.Count = 1;
            this._flex업무계획.Rows.DefaultSize = 20;
            this._flex업무계획.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex업무계획.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex업무계획.ShowSort = false;
            this._flex업무계획.Size = new System.Drawing.Size(1239, 733);
            this._flex업무계획.StyleInfo = resources.GetString("_flex업무계획.StyleInfo");
            this._flex업무계획.TabIndex = 0;
            this._flex업무계획.UseGridCalculator = true;
            // 
            // tpg본선선적
            // 
            this.tpg본선선적.Controls.Add(this.splitContainer1);
            this.tpg본선선적.Location = new System.Drawing.Point(4, 22);
            this.tpg본선선적.Name = "tpg본선선적";
            this.tpg본선선적.Padding = new System.Windows.Forms.Padding(3);
            this.tpg본선선적.Size = new System.Drawing.Size(1245, 739);
            this.tpg본선선적.TabIndex = 1;
            this.tpg본선선적.Text = "본선선적";
            this.tpg본선선적.UseVisualStyleBackColor = true;
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
            this.splitContainer1.Panel1.Controls.Add(this._flex본선선적H);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._flex본선선적L);
            this.splitContainer1.Size = new System.Drawing.Size(1239, 733);
            this.splitContainer1.SplitterDistance = 411;
            this.splitContainer1.TabIndex = 1;
            // 
            // _flex본선선적H
            // 
            this._flex본선선적H.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex본선선적H.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex본선선적H.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex본선선적H.AutoResize = false;
            this._flex본선선적H.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex본선선적H.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex본선선적H.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex본선선적H.EnabledHeaderCheck = true;
            this._flex본선선적H.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex본선선적H.Location = new System.Drawing.Point(0, 0);
            this._flex본선선적H.Name = "_flex본선선적H";
            this._flex본선선적H.Rows.Count = 1;
            this._flex본선선적H.Rows.DefaultSize = 20;
            this._flex본선선적H.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex본선선적H.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex본선선적H.ShowSort = false;
            this._flex본선선적H.Size = new System.Drawing.Size(1239, 411);
            this._flex본선선적H.StyleInfo = resources.GetString("_flex본선선적H.StyleInfo");
            this._flex본선선적H.TabIndex = 0;
            this._flex본선선적H.UseGridCalculator = true;
            // 
            // _flex본선선적L
            // 
            this._flex본선선적L.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex본선선적L.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex본선선적L.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex본선선적L.AutoResize = false;
            this._flex본선선적L.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex본선선적L.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex본선선적L.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex본선선적L.EnabledHeaderCheck = true;
            this._flex본선선적L.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex본선선적L.Location = new System.Drawing.Point(0, 0);
            this._flex본선선적L.Name = "_flex본선선적L";
            this._flex본선선적L.Rows.Count = 1;
            this._flex본선선적L.Rows.DefaultSize = 20;
            this._flex본선선적L.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex본선선적L.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex본선선적L.ShowSort = false;
            this._flex본선선적L.Size = new System.Drawing.Size(1239, 318);
            this._flex본선선적L.StyleInfo = resources.GetString("_flex본선선적L.StyleInfo");
            this._flex본선선적L.TabIndex = 1;
            this._flex본선선적L.UseGridCalculator = true;
            // 
            // tpg전달수령
            // 
            this.tpg전달수령.Controls.Add(this._flex전달수령);
            this.tpg전달수령.Location = new System.Drawing.Point(4, 22);
            this.tpg전달수령.Name = "tpg전달수령";
            this.tpg전달수령.Size = new System.Drawing.Size(1245, 739);
            this.tpg전달수령.TabIndex = 2;
            this.tpg전달수령.Text = "전달수령";
            this.tpg전달수령.UseVisualStyleBackColor = true;
            // 
            // _flex전달수령
            // 
            this._flex전달수령.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex전달수령.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex전달수령.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex전달수령.AutoResize = false;
            this._flex전달수령.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex전달수령.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex전달수령.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex전달수령.EnabledHeaderCheck = true;
            this._flex전달수령.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex전달수령.Location = new System.Drawing.Point(0, 0);
            this._flex전달수령.Name = "_flex전달수령";
            this._flex전달수령.Rows.Count = 1;
            this._flex전달수령.Rows.DefaultSize = 20;
            this._flex전달수령.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex전달수령.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex전달수령.ShowSort = false;
            this._flex전달수령.Size = new System.Drawing.Size(1245, 739);
            this._flex전달수령.StyleInfo = resources.GetString("_flex전달수령.StyleInfo");
            this._flex전달수령.TabIndex = 0;
            this._flex전달수령.UseGridCalculator = true;
            // 
            // tpg인원현황
            // 
            this.tpg인원현황.Controls.Add(this._flex인원현황);
            this.tpg인원현황.Location = new System.Drawing.Point(4, 22);
            this.tpg인원현황.Name = "tpg인원현황";
            this.tpg인원현황.Size = new System.Drawing.Size(1245, 739);
            this.tpg인원현황.TabIndex = 3;
            this.tpg인원현황.Text = "인원현황";
            this.tpg인원현황.UseVisualStyleBackColor = true;
            // 
            // _flex인원현황
            // 
            this._flex인원현황.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex인원현황.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex인원현황.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex인원현황.AutoResize = false;
            this._flex인원현황.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex인원현황.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex인원현황.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex인원현황.EnabledHeaderCheck = true;
            this._flex인원현황.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex인원현황.Location = new System.Drawing.Point(0, 0);
            this._flex인원현황.Name = "_flex인원현황";
            this._flex인원현황.Rows.Count = 1;
            this._flex인원현황.Rows.DefaultSize = 20;
            this._flex인원현황.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex인원현황.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex인원현황.ShowSort = false;
            this._flex인원현황.Size = new System.Drawing.Size(1245, 739);
            this._flex인원현황.StyleInfo = resources.GetString("_flex인원현황.StyleInfo");
            this._flex인원현황.TabIndex = 0;
            this._flex인원현황.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(1253, 39);
            this.oneGrid1.TabIndex = 1;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl3);
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(1243, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.txt협조전번호);
            this.bpPanelControl3.Controls.Add(this.lbl협조전번호);
            this.bpPanelControl3.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // txt협조전번호
            // 
            this.txt협조전번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt협조전번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt협조전번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt협조전번호.Location = new System.Drawing.Point(106, 0);
            this.txt협조전번호.Name = "txt협조전번호";
            this.txt협조전번호.Size = new System.Drawing.Size(186, 21);
            this.txt협조전번호.TabIndex = 1;
            // 
            // lbl협조전번호
            // 
            this.lbl협조전번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl협조전번호.Location = new System.Drawing.Point(0, 0);
            this.lbl협조전번호.Name = "lbl협조전번호";
            this.lbl협조전번호.Size = new System.Drawing.Size(100, 23);
            this.lbl협조전번호.TabIndex = 0;
            this.lbl협조전번호.Text = "협조전번호";
            this.lbl협조전번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.dtp작업일자);
            this.bpPanelControl2.Controls.Add(this.lbl작업일자);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // dtp작업일자
            // 
            this.dtp작업일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp작업일자.Location = new System.Drawing.Point(105, 1);
            this.dtp작업일자.Mask = "####/##/##";
            this.dtp작업일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp작업일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp작업일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp작업일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp작업일자.Modified = true;
            this.dtp작업일자.Name = "dtp작업일자";
            this.dtp작업일자.Size = new System.Drawing.Size(90, 21);
            this.dtp작업일자.TabIndex = 1;
            this.dtp작업일자.Value = new System.DateTime(((long)(0)));
            // 
            // lbl작업일자
            // 
            this.lbl작업일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl작업일자.Location = new System.Drawing.Point(0, 0);
            this.lbl작업일자.Name = "lbl작업일자";
            this.lbl작업일자.Size = new System.Drawing.Size(100, 23);
            this.lbl작업일자.TabIndex = 0;
            this.lbl작업일자.Text = "작업일자";
            this.lbl작업일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn자동등록
            // 
            this.btn자동등록.BackColor = System.Drawing.Color.Transparent;
            this.btn자동등록.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn자동등록.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn자동등록.Location = new System.Drawing.Point(10, 3);
            this.btn자동등록.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn자동등록.Name = "btn자동등록";
            this.btn자동등록.Size = new System.Drawing.Size(98, 19);
            this.btn자동등록.TabIndex = 3;
            this.btn자동등록.TabStop = false;
            this.btn자동등록.Text = "자동등록";
            this.btn자동등록.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btn자동등록);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1145, 9);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(111, 24);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // P_CZ_SA_GIR_DAILY_PLAN
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "P_CZ_SA_GIR_DAILY_PLAN";
            this.Size = new System.Drawing.Size(1259, 856);
            this.TitleText = "일일업무계획";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControlExt1.ResumeLayout(false);
            this.tpg업무계획.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex업무계획)).EndInit();
            this.tpg본선선적.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex본선선적H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex본선선적L)).EndInit();
            this.tpg전달수령.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex전달수령)).EndInit();
            this.tpg인원현황.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex인원현황)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl3.PerformLayout();
            this.bpPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp작업일자)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.TabControlExt tabControlExt1;
        private System.Windows.Forms.TabPage tpg업무계획;
        private System.Windows.Forms.TabPage tpg본선선적;
        private System.Windows.Forms.TabPage tpg전달수령;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Dass.FlexGrid.FlexGrid _flex업무계획;
        private Dass.FlexGrid.FlexGrid _flex본선선적H;
        private Dass.FlexGrid.FlexGrid _flex전달수령;
        private System.Windows.Forms.TabPage tpg인원현황;
        private Dass.FlexGrid.FlexGrid _flex인원현황;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flex본선선적L;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.DatePicker dtp작업일자;
        private Duzon.Common.Controls.LabelExt lbl작업일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.TextBoxExt txt협조전번호;
        private Duzon.Common.Controls.LabelExt lbl협조전번호;
        private Duzon.Common.Controls.RoundedButton btn자동등록;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
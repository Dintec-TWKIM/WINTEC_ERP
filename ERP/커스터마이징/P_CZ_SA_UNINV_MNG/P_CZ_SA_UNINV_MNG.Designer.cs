namespace cz
{
    partial class P_CZ_SA_UNINV_MNG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_UNINV_MNG));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx매출처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl매출처 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp수주일자 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl수주일자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt수주번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl수주번호 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chk미매출 = new Duzon.Common.Controls.CheckBoxExt();
            this.chk미청구 = new Duzon.Common.Controls.CheckBoxExt();
            this.chk무상공급 = new Duzon.Common.Controls.CheckBoxExt();
            this.chk잔액0제외 = new Duzon.Common.Controls.CheckBoxExt();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx호선 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl호선 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt매출처발주번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl매출처발주번호 = new Duzon.Common.Controls.LabelExt();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
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
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1090, 756);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(1084, 63);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.Controls.Add(this.bpPanelControl5);
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
            this.bpPanelControl2.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // ctx매출처
            // 
            this.ctx매출처.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx매출처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx매출처.Location = new System.Drawing.Point(106, 0);
            this.ctx매출처.Name = "ctx매출처";
            this.ctx매출처.Size = new System.Drawing.Size(186, 21);
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
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.dtp수주일자);
            this.bpPanelControl5.Controls.Add(this.lbl수주일자);
            this.bpPanelControl5.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl5.TabIndex = 1;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // dtp수주일자
            // 
            this.dtp수주일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp수주일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp수주일자.IsNecessaryCondition = true;
            this.dtp수주일자.Location = new System.Drawing.Point(107, 0);
            this.dtp수주일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp수주일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp수주일자.Name = "dtp수주일자";
            this.dtp수주일자.Size = new System.Drawing.Size(185, 21);
            this.dtp수주일자.TabIndex = 1;
            // 
            // lbl수주일자
            // 
            this.lbl수주일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl수주일자.Location = new System.Drawing.Point(0, 0);
            this.lbl수주일자.Name = "lbl수주일자";
            this.lbl수주일자.Size = new System.Drawing.Size(100, 23);
            this.lbl수주일자.TabIndex = 0;
            this.lbl수주일자.Text = "수주일자";
            this.lbl수주일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.txt수주번호);
            this.bpPanelControl1.Controls.Add(this.lbl수주번호);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // txt수주번호
            // 
            this.txt수주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt수주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt수주번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt수주번호.Location = new System.Drawing.Point(106, 0);
            this.txt수주번호.Name = "txt수주번호";
            this.txt수주번호.Size = new System.Drawing.Size(186, 21);
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
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.flowLayoutPanel1);
            this.oneGridItem2.Controls.Add(this.bpPanelControl3);
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(1074, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.chk미매출);
            this.flowLayoutPanel1.Controls.Add(this.chk미청구);
            this.flowLayoutPanel1.Controls.Add(this.chk무상공급);
            this.flowLayoutPanel1.Controls.Add(this.chk잔액0제외);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(590, 1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(481, 19);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // chk미매출
            // 
            this.chk미매출.AutoSize = true;
            this.chk미매출.Location = new System.Drawing.Point(3, 3);
            this.chk미매출.Name = "chk미매출";
            this.chk미매출.Size = new System.Drawing.Size(60, 16);
            this.chk미매출.TabIndex = 0;
            this.chk미매출.Text = "미매출";
            this.chk미매출.TextDD = null;
            this.chk미매출.UseVisualStyleBackColor = true;
            // 
            // chk미청구
            // 
            this.chk미청구.AutoSize = true;
            this.chk미청구.Location = new System.Drawing.Point(69, 3);
            this.chk미청구.Name = "chk미청구";
            this.chk미청구.Size = new System.Drawing.Size(60, 16);
            this.chk미청구.TabIndex = 1;
            this.chk미청구.Text = "미청구";
            this.chk미청구.TextDD = null;
            this.chk미청구.UseVisualStyleBackColor = true;
            // 
            // chk무상공급
            // 
            this.chk무상공급.AutoSize = true;
            this.chk무상공급.Location = new System.Drawing.Point(135, 3);
            this.chk무상공급.Name = "chk무상공급";
            this.chk무상공급.Size = new System.Drawing.Size(72, 16);
            this.chk무상공급.TabIndex = 2;
            this.chk무상공급.Text = "무상공급";
            this.chk무상공급.TextDD = null;
            this.chk무상공급.UseVisualStyleBackColor = true;
            // 
            // chk잔액0제외
            // 
            this.chk잔액0제외.AutoSize = true;
            this.chk잔액0제외.Location = new System.Drawing.Point(213, 3);
            this.chk잔액0제외.Name = "chk잔액0제외";
            this.chk잔액0제외.Size = new System.Drawing.Size(86, 16);
            this.chk잔액0제외.TabIndex = 3;
            this.chk잔액0제외.Text = "잔액 0 제외";
            this.chk잔액0제외.TextDD = null;
            this.chk잔액0제외.UseVisualStyleBackColor = true;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.ctx호선);
            this.bpPanelControl3.Controls.Add(this.lbl호선);
            this.bpPanelControl3.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // ctx호선
            // 
            this.ctx호선.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx호선.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.ctx호선.Location = new System.Drawing.Point(107, 0);
            this.ctx호선.Name = "ctx호선";
            this.ctx호선.Size = new System.Drawing.Size(185, 21);
            this.ctx호선.TabIndex = 2;
            this.ctx호선.TabStop = false;
            this.ctx호선.Text = "bpCodeTextBox1";
            this.ctx호선.UserCodeName = "NO_HULL";
            this.ctx호선.UserCodeValue = "NO_IMO";
            this.ctx호선.UserHelpID = "H_CZ_MA_HULL_SUB";
            this.ctx호선.UserParams = "호선;H_CZ_MA_HULL_SUB";
            // 
            // lbl호선
            // 
            this.lbl호선.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl호선.Location = new System.Drawing.Point(0, 0);
            this.lbl호선.Name = "lbl호선";
            this.lbl호선.Size = new System.Drawing.Size(100, 23);
            this.lbl호선.TabIndex = 0;
            this.lbl호선.Text = "호선";
            this.lbl호선.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.txt매출처발주번호);
            this.bpPanelControl4.Controls.Add(this.lbl매출처발주번호);
            this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl4.TabIndex = 0;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // txt매출처발주번호
            // 
            this.txt매출처발주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt매출처발주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt매출처발주번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt매출처발주번호.Location = new System.Drawing.Point(107, 0);
            this.txt매출처발주번호.Name = "txt매출처발주번호";
            this.txt매출처발주번호.Size = new System.Drawing.Size(185, 21);
            this.txt매출처발주번호.TabIndex = 1;
            // 
            // lbl매출처발주번호
            // 
            this.lbl매출처발주번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl매출처발주번호.Location = new System.Drawing.Point(0, 0);
            this.lbl매출처발주번호.Name = "lbl매출처발주번호";
            this.lbl매출처발주번호.Size = new System.Drawing.Size(100, 23);
            this.lbl매출처발주번호.TabIndex = 0;
            this.lbl매출처발주번호.Text = "매출처발주번호";
            this.lbl매출처발주번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 72);
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
            this.splitContainer1.Size = new System.Drawing.Size(1084, 681);
            this.splitContainer1.SplitterDistance = 346;
            this.splitContainer1.TabIndex = 1;
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
            this._flexH.Size = new System.Drawing.Size(1084, 346);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 0;
            this._flexH.UseGridCalculator = true;
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
            this._flexL.Size = new System.Drawing.Size(1084, 331);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 0;
            this._flexL.UseGridCalculator = true;
            // 
            // P_CZ_SA_UNINV_MNG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_CZ_SA_UNINV_MNG";
            this.Size = new System.Drawing.Size(1090, 796);
            this.TitleText = "미청구관리";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl1.PerformLayout();
            this.oneGridItem2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.TextBoxExt txt수주번호;
        private Duzon.Common.Controls.LabelExt lbl수주번호;
        private Duzon.Common.Controls.CheckBoxExt chk미매출;
        private Duzon.Common.Controls.CheckBoxExt chk미청구;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.CheckBoxExt chk무상공급;
        private Duzon.Common.Controls.CheckBoxExt chk잔액0제외;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.LabelExt lbl호선;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpCodeTextBox ctx매출처;
        private Duzon.Common.Controls.LabelExt lbl매출처;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpCodeTextBox ctx호선;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.TextBoxExt txt매출처발주번호;
        private Duzon.Common.Controls.LabelExt lbl매출처발주번호;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.Controls.PeriodPicker dtp수주일자;
        private Duzon.Common.Controls.LabelExt lbl수주일자;
    }
}
namespace cz
{
    partial class P_CZ_SA_LEAD_TIME_RPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_LEAD_TIME_RPT));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx영업담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl영업담당자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt파일번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl파일번호 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp조회기간 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl조회기간 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx영업그룹 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl영업그룹 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx매출처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl매출처 = new Duzon.Common.Controls.LabelExt();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx매입처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl매입처 = new Duzon.Common.Controls.LabelExt();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.bpPanelControl6.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(971, 579);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 182F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(971, 579);
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
            this.oneGrid1.Size = new System.Drawing.Size(965, 63);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl3);
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(955, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.ctx영업담당자);
            this.bpPanelControl3.Controls.Add(this.lbl영업담당자);
            this.bpPanelControl3.Location = new System.Drawing.Point(586, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(298, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // ctx영업담당자
            // 
            this.ctx영업담당자.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx영업담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.ctx영업담당자.Location = new System.Drawing.Point(106, 0);
            this.ctx영업담당자.Name = "ctx영업담당자";
            this.ctx영업담당자.Size = new System.Drawing.Size(192, 21);
            this.ctx영업담당자.TabIndex = 1;
            this.ctx영업담당자.TabStop = false;
            this.ctx영업담당자.Text = "bpCodeTextBox1";
            // 
            // lbl영업담당자
            // 
            this.lbl영업담당자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl영업담당자.Location = new System.Drawing.Point(0, 0);
            this.lbl영업담당자.Name = "lbl영업담당자";
            this.lbl영업담당자.Size = new System.Drawing.Size(100, 23);
            this.lbl영업담당자.TabIndex = 0;
            this.lbl영업담당자.Text = "영업담당자";
            this.lbl영업담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.txt파일번호);
            this.bpPanelControl2.Controls.Add(this.lbl파일번호);
            this.bpPanelControl2.Location = new System.Drawing.Point(294, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(290, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // txt파일번호
            // 
            this.txt파일번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt파일번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt파일번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt파일번호.Location = new System.Drawing.Point(106, 0);
            this.txt파일번호.Name = "txt파일번호";
            this.txt파일번호.Size = new System.Drawing.Size(184, 21);
            this.txt파일번호.TabIndex = 1;
            // 
            // lbl파일번호
            // 
            this.lbl파일번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl파일번호.Location = new System.Drawing.Point(0, 0);
            this.lbl파일번호.Name = "lbl파일번호";
            this.lbl파일번호.Size = new System.Drawing.Size(100, 23);
            this.lbl파일번호.TabIndex = 0;
            this.lbl파일번호.Text = "파일번호";
            this.lbl파일번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp조회기간);
            this.bpPanelControl1.Controls.Add(this.lbl조회기간);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(290, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // dtp조회기간
            // 
            this.dtp조회기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp조회기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp조회기간.IsNecessaryCondition = true;
            this.dtp조회기간.Location = new System.Drawing.Point(105, 0);
            this.dtp조회기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp조회기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp조회기간.Name = "dtp조회기간";
            this.dtp조회기간.Size = new System.Drawing.Size(185, 21);
            this.dtp조회기간.TabIndex = 1;
            // 
            // lbl조회기간
            // 
            this.lbl조회기간.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl조회기간.Location = new System.Drawing.Point(0, 0);
            this.lbl조회기간.Name = "lbl조회기간";
            this.lbl조회기간.Size = new System.Drawing.Size(100, 23);
            this.lbl조회기간.TabIndex = 0;
            this.lbl조회기간.Text = "조회기간";
            this.lbl조회기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(955, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.ctx영업그룹);
            this.bpPanelControl5.Controls.Add(this.lbl영업그룹);
            this.bpPanelControl5.Location = new System.Drawing.Point(586, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(290, 23);
            this.bpPanelControl5.TabIndex = 4;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // ctx영업그룹
            // 
            this.ctx영업그룹.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx영업그룹.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.ctx영업그룹.Location = new System.Drawing.Point(105, 0);
            this.ctx영업그룹.Name = "ctx영업그룹";
            this.ctx영업그룹.Size = new System.Drawing.Size(185, 21);
            this.ctx영업그룹.TabIndex = 1;
            this.ctx영업그룹.TabStop = false;
            this.ctx영업그룹.Text = "bpCodeTextBox1";
            // 
            // lbl영업그룹
            // 
            this.lbl영업그룹.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl영업그룹.Location = new System.Drawing.Point(0, 0);
            this.lbl영업그룹.Name = "lbl영업그룹";
            this.lbl영업그룹.Size = new System.Drawing.Size(100, 23);
            this.lbl영업그룹.TabIndex = 0;
            this.lbl영업그룹.Text = "영업그룹";
            this.lbl영업그룹.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.ctx매출처);
            this.bpPanelControl4.Controls.Add(this.lbl매출처);
            this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(290, 23);
            this.bpPanelControl4.TabIndex = 3;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // ctx매출처
            // 
            this.ctx매출처.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx매출처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx매출처.Location = new System.Drawing.Point(105, 0);
            this.ctx매출처.Name = "ctx매출처";
            this.ctx매출처.Size = new System.Drawing.Size(185, 21);
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
            this._flex.Location = new System.Drawing.Point(3, 72);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(965, 504);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 1;
            this._flex.UseGridCalculator = true;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.ctx매입처);
            this.bpPanelControl6.Controls.Add(this.lbl매입처);
            this.bpPanelControl6.Location = new System.Drawing.Point(294, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(290, 23);
            this.bpPanelControl6.TabIndex = 5;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // ctx매입처
            // 
            this.ctx매입처.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx매입처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx매입처.Location = new System.Drawing.Point(105, 0);
            this.ctx매입처.Name = "ctx매입처";
            this.ctx매입처.Size = new System.Drawing.Size(185, 21);
            this.ctx매입처.TabIndex = 1;
            this.ctx매입처.TabStop = false;
            this.ctx매입처.Text = "bpCodeTextBox1";
            // 
            // lbl매입처
            // 
            this.lbl매입처.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl매입처.Location = new System.Drawing.Point(0, 0);
            this.lbl매입처.Name = "lbl매입처";
            this.lbl매입처.Size = new System.Drawing.Size(100, 23);
            this.lbl매입처.TabIndex = 0;
            this.lbl매입처.Text = "매입처";
            this.lbl매입처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // P_CZ_SA_LEAD_TIME_RPT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_CZ_SA_LEAD_TIME_RPT";
            this.Size = new System.Drawing.Size(971, 619);
            this.TitleText = "P_CZ_SA_LEAD_TIME_RPT";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl2.PerformLayout();
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.bpPanelControl6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.LabelExt lbl조회기간;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.Controls.PeriodPicker dtp조회기간;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.TextBoxExt txt파일번호;
        private Duzon.Common.Controls.LabelExt lbl파일번호;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpCodeTextBox ctx영업담당자;
        private Duzon.Common.Controls.LabelExt lbl영업담당자;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpCodeTextBox ctx매출처;
        private Duzon.Common.Controls.LabelExt lbl매출처;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
		private Duzon.Common.BpControls.BpCodeTextBox ctx영업그룹;
		private Duzon.Common.Controls.LabelExt lbl영업그룹;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
		private Duzon.Common.BpControls.BpCodeTextBox ctx매입처;
		private Duzon.Common.Controls.LabelExt lbl매입처;
	}
}
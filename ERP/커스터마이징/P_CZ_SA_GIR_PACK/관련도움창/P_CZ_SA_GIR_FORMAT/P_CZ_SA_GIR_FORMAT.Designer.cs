namespace cz
{
    partial class P_CZ_SA_GIR_FORMAT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_GIR_FORMAT));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpPanelControl12 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl매출처 = new Duzon.Common.Controls.LabelExt();
			this.ctx매출처 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt호선명 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl호선명 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl13 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx호선번호 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl호선번호 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.chk송장정보포함 = new Duzon.Common.Controls.CheckBoxExt();
			this.lbl복사옵션 = new Duzon.Common.Controls.LabelExt();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn적용 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.bpPanelControl13.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 48);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(847, 508);
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
			this.oneGrid1.Size = new System.Drawing.Size(841, 62);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl5);
			this.oneGridItem1.Controls.Add(this.bpPanelControl4);
			this.oneGridItem1.Controls.Add(this.bpPanelControl13);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(831, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.bpPanelControl12);
			this.bpPanelControl5.Controls.Add(this.lbl매출처);
			this.bpPanelControl5.Controls.Add(this.ctx매출처);
			this.bpPanelControl5.Location = new System.Drawing.Point(554, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(275, 23);
			this.bpPanelControl5.TabIndex = 39;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// bpPanelControl12
			// 
			this.bpPanelControl12.Location = new System.Drawing.Point(246, 22);
			this.bpPanelControl12.Name = "bpPanelControl12";
			this.bpPanelControl12.Size = new System.Drawing.Size(240, 23);
			this.bpPanelControl12.TabIndex = 9;
			this.bpPanelControl12.Text = "bpPanelControl12";
			// 
			// lbl매출처
			// 
			this.lbl매출처.BackColor = System.Drawing.Color.Transparent;
			this.lbl매출처.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl매출처.Location = new System.Drawing.Point(0, 0);
			this.lbl매출처.Name = "lbl매출처";
			this.lbl매출처.Size = new System.Drawing.Size(80, 23);
			this.lbl매출처.TabIndex = 20;
			this.lbl매출처.Text = "매출처";
			this.lbl매출처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx매출처
			// 
			this.ctx매출처.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx매출처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
			this.ctx매출처.Location = new System.Drawing.Point(89, 0);
			this.ctx매출처.Name = "ctx매출처";
			this.ctx매출처.Size = new System.Drawing.Size(186, 21);
			this.ctx매출처.TabIndex = 21;
			this.ctx매출처.TabStop = false;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.txt호선명);
			this.bpPanelControl4.Controls.Add(this.lbl호선명);
			this.bpPanelControl4.Location = new System.Drawing.Point(278, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(274, 23);
			this.bpPanelControl4.TabIndex = 38;
			// 
			// txt호선명
			// 
			this.txt호선명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt호선명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt호선명.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt호선명.Location = new System.Drawing.Point(89, 0);
			this.txt호선명.Name = "txt호선명";
			this.txt호선명.ReadOnly = true;
			this.txt호선명.Size = new System.Drawing.Size(185, 21);
			this.txt호선명.TabIndex = 33;
			this.txt호선명.TabStop = false;
			// 
			// lbl호선명
			// 
			this.lbl호선명.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl호선명.Location = new System.Drawing.Point(0, 0);
			this.lbl호선명.Name = "lbl호선명";
			this.lbl호선명.Size = new System.Drawing.Size(80, 23);
			this.lbl호선명.TabIndex = 32;
			this.lbl호선명.Text = "호선명";
			this.lbl호선명.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl13
			// 
			this.bpPanelControl13.Controls.Add(this.ctx호선번호);
			this.bpPanelControl13.Controls.Add(this.lbl호선번호);
			this.bpPanelControl13.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl13.Name = "bpPanelControl13";
			this.bpPanelControl13.Size = new System.Drawing.Size(274, 23);
			this.bpPanelControl13.TabIndex = 37;
			// 
			// ctx호선번호
			// 
			this.ctx호선번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx호선번호.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx호선번호.Location = new System.Drawing.Point(89, 0);
			this.ctx호선번호.Name = "ctx호선번호";
			this.ctx호선번호.Size = new System.Drawing.Size(185, 21);
			this.ctx호선번호.TabIndex = 33;
			this.ctx호선번호.TabStop = false;
			this.ctx호선번호.UserCodeName = "NO_HULL";
			this.ctx호선번호.UserCodeValue = "NO_IMO";
			this.ctx호선번호.UserHelpID = "H_CZ_MA_HULL_SUB";
			this.ctx호선번호.UserParams = "호선;H_CZ_MA_HULL_SUB";
			// 
			// lbl호선번호
			// 
			this.lbl호선번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl호선번호.Location = new System.Drawing.Point(0, 0);
			this.lbl호선번호.Name = "lbl호선번호";
			this.lbl호선번호.Size = new System.Drawing.Size(80, 23);
			this.lbl호선번호.TabIndex = 32;
			this.lbl호선번호.Text = "호선번호";
			this.lbl호선번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl1);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(831, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.flowLayoutPanel2);
			this.bpPanelControl1.Controls.Add(this.lbl복사옵션);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(274, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.chk송장정보포함);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(89, 0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(185, 23);
			this.flowLayoutPanel2.TabIndex = 1;
			// 
			// chk송장정보포함
			// 
			this.chk송장정보포함.AutoSize = true;
			this.chk송장정보포함.Location = new System.Drawing.Point(3, 3);
			this.chk송장정보포함.Name = "chk송장정보포함";
			this.chk송장정보포함.Size = new System.Drawing.Size(96, 16);
			this.chk송장정보포함.TabIndex = 0;
			this.chk송장정보포함.Text = "송장정보포함";
			this.chk송장정보포함.TextDD = null;
			this.chk송장정보포함.UseVisualStyleBackColor = true;
			// 
			// lbl복사옵션
			// 
			this.lbl복사옵션.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl복사옵션.Location = new System.Drawing.Point(0, 0);
			this.lbl복사옵션.Name = "lbl복사옵션";
			this.lbl복사옵션.Size = new System.Drawing.Size(80, 23);
			this.lbl복사옵션.TabIndex = 0;
			this.lbl복사옵션.Text = "복사옵션";
			this.lbl복사옵션.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _flex
			// 
			this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex.AutoResize = false;
			this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex.EnabledHeaderCheck = true;
			this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex.Location = new System.Drawing.Point(3, 104);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 18;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(841, 401);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 1;
			this._flex.UseGridCalculator = true;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn취소);
			this.flowLayoutPanel1.Controls.Add(this.btn적용);
			this.flowLayoutPanel1.Controls.Add(this.btn조회);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 71);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(841, 27);
			this.flowLayoutPanel1.TabIndex = 2;
			// 
			// btn취소
			// 
			this.btn취소.BackColor = System.Drawing.Color.Transparent;
			this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.Location = new System.Drawing.Point(768, 3);
			this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(70, 19);
			this.btn취소.TabIndex = 0;
			this.btn취소.TabStop = false;
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// btn적용
			// 
			this.btn적용.BackColor = System.Drawing.Color.Transparent;
			this.btn적용.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn적용.Location = new System.Drawing.Point(692, 3);
			this.btn적용.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn적용.Name = "btn적용";
			this.btn적용.Size = new System.Drawing.Size(70, 19);
			this.btn적용.TabIndex = 1;
			this.btn적용.TabStop = false;
			this.btn적용.Text = "적용";
			this.btn적용.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.BackColor = System.Drawing.Color.Transparent;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(616, 3);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 2;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// P_CZ_SA_GIR_FORMAT
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CancelButton = this.btn취소;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(849, 557);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_CZ_SA_GIR_FORMAT";
			this.Text = "P_CZ_SA_GIR_FORMAT";
			this.TitleText = "협조전적용";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl4.PerformLayout();
			this.bpPanelControl13.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn취소;
        private Duzon.Common.Controls.RoundedButton btn적용;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl12;
        private Duzon.Common.Controls.LabelExt lbl매출처;
        private Duzon.Common.BpControls.BpCodeTextBox ctx매출처;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.TextBoxExt txt호선명;
        private Duzon.Common.Controls.LabelExt lbl호선명;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl13;
        private Duzon.Common.BpControls.BpCodeTextBox ctx호선번호;
        private Duzon.Common.Controls.LabelExt lbl호선번호;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Duzon.Common.Controls.LabelExt lbl복사옵션;
        private Duzon.Common.Controls.CheckBoxExt chk송장정보포함;
    }
}
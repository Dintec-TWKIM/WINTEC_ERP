namespace cz
{
    partial class P_CZ_MA_PARTNER_REQ_CHECK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MA_PARTNER_REQ_CHECK));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn닫기 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.lbl거래처명 = new Duzon.Common.Controls.LabelExt();
            this.txt거래처명 = new Duzon.Common.Controls.TextBoxExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl비교자리수 = new Duzon.Common.Controls.LabelExt();
            this.cur비교자리수 = new Duzon.Common.Controls.CurrencyTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.bpPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur비교자리수)).BeginInit();
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
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(599, 446);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(593, 39);
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
            this.oneGridItem1.Size = new System.Drawing.Size(583, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn닫기);
            this.flowLayoutPanel1.Controls.Add(this.btn조회);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 48);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(593, 65);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btn닫기
            // 
            this.btn닫기.BackColor = System.Drawing.Color.Transparent;
            this.btn닫기.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn닫기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn닫기.Location = new System.Drawing.Point(520, 3);
            this.btn닫기.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn닫기.Name = "btn닫기";
            this.btn닫기.Size = new System.Drawing.Size(70, 19);
            this.btn닫기.TabIndex = 0;
            this.btn닫기.TabStop = false;
            this.btn닫기.Text = "닫기";
            this.btn닫기.UseVisualStyleBackColor = false;
            // 
            // btn조회
            // 
            this.btn조회.BackColor = System.Drawing.Color.Transparent;
            this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn조회.Location = new System.Drawing.Point(444, 3);
            this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn조회.Name = "btn조회";
            this.btn조회.Size = new System.Drawing.Size(70, 19);
            this.btn조회.TabIndex = 2;
            this.btn조회.TabStop = false;
            this.btn조회.Text = "조회";
            this.btn조회.UseVisualStyleBackColor = false;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.txt거래처명);
            this.bpPanelControl1.Controls.Add(this.lbl거래처명);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(286, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
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
            this._flex.Location = new System.Drawing.Point(3, 119);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(593, 324);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 2;
            this._flex.UseGridCalculator = true;
            // 
            // lbl거래처명
            // 
            this.lbl거래처명.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl거래처명.Location = new System.Drawing.Point(0, 0);
            this.lbl거래처명.Name = "lbl거래처명";
            this.lbl거래처명.Size = new System.Drawing.Size(100, 23);
            this.lbl거래처명.TabIndex = 0;
            this.lbl거래처명.Text = "거래처명";
            this.lbl거래처명.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt거래처명
            // 
            this.txt거래처명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt거래처명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt거래처명.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt거래처명.Location = new System.Drawing.Point(106, 0);
            this.txt거래처명.Name = "txt거래처명";
            this.txt거래처명.Size = new System.Drawing.Size(180, 21);
            this.txt거래처명.TabIndex = 1;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.cur비교자리수);
            this.bpPanelControl2.Controls.Add(this.lbl비교자리수);
            this.bpPanelControl2.Location = new System.Drawing.Point(290, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(286, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // lbl비교자리수
            // 
            this.lbl비교자리수.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl비교자리수.Location = new System.Drawing.Point(0, 0);
            this.lbl비교자리수.Name = "lbl비교자리수";
            this.lbl비교자리수.Size = new System.Drawing.Size(100, 23);
            this.lbl비교자리수.TabIndex = 0;
            this.lbl비교자리수.Text = "비교자리수";
            this.lbl비교자리수.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cur비교자리수
            // 
            this.cur비교자리수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur비교자리수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur비교자리수.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur비교자리수.Dock = System.Windows.Forms.DockStyle.Right;
            this.cur비교자리수.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur비교자리수.Location = new System.Drawing.Point(106, 0);
            this.cur비교자리수.Name = "cur비교자리수";
            this.cur비교자리수.NullString = "0";
            this.cur비교자리수.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur비교자리수.Size = new System.Drawing.Size(180, 21);
            this.cur비교자리수.TabIndex = 1;
            this.cur비교자리수.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // P_CZ_MA_PARTNER_REQ_CHECK
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(601, 495);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_CZ_MA_PARTNER_REQ_CHECK";
            this.TitleText = "거래처중복확인";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur비교자리수)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn닫기;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.TextBoxExt txt거래처명;
        private Duzon.Common.Controls.LabelExt lbl거래처명;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.CurrencyTextBox cur비교자리수;
        private Duzon.Common.Controls.LabelExt lbl비교자리수;
    }
}
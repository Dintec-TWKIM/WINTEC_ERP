namespace cz
{
    partial class P_CZ_MA_PARTNER_COPY
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn종료 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn동기화 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx거래처 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl거래처 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx원본회사 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl원본회사 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc대상회사 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl대상회사 = new Duzon.Common.Controls.LabelExt();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 48);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 101F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(403, 134);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn종료);
			this.flowLayoutPanel1.Controls.Add(this.btn동기화);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(397, 26);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// btn종료
			// 
			this.btn종료.BackColor = System.Drawing.Color.Transparent;
			this.btn종료.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn종료.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn종료.Location = new System.Drawing.Point(312, 3);
			this.btn종료.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn종료.Name = "btn종료";
			this.btn종료.Size = new System.Drawing.Size(82, 19);
			this.btn종료.TabIndex = 0;
			this.btn종료.TabStop = false;
			this.btn종료.Text = "종료";
			this.btn종료.UseVisualStyleBackColor = false;
			// 
			// btn동기화
			// 
			this.btn동기화.BackColor = System.Drawing.Color.Transparent;
			this.btn동기화.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn동기화.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn동기화.Location = new System.Drawing.Point(222, 3);
			this.btn동기화.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn동기화.Name = "btn동기화";
			this.btn동기화.Size = new System.Drawing.Size(84, 19);
			this.btn동기화.TabIndex = 1;
			this.btn동기화.TabStop = false;
			this.btn동기화.Text = "동기화";
			this.btn동기화.UseVisualStyleBackColor = false;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
			this.oneGrid1.Location = new System.Drawing.Point(3, 35);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(397, 96);
			this.oneGrid1.TabIndex = 1;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(387, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.ctx거래처);
			this.bpPanelControl1.Controls.Add(this.lbl거래처);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(295, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// ctx거래처
			// 
			this.ctx거래처.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx거래처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
			this.ctx거래처.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx거래처.Location = new System.Drawing.Point(106, 0);
			this.ctx거래처.Name = "ctx거래처";
			this.ctx거래처.Size = new System.Drawing.Size(189, 21);
			this.ctx거래처.TabIndex = 1;
			this.ctx거래처.TabStop = false;
			this.ctx거래처.Text = "bpCodeTextBox1";
			// 
			// lbl거래처
			// 
			this.lbl거래처.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl거래처.Location = new System.Drawing.Point(0, 0);
			this.lbl거래처.Name = "lbl거래처";
			this.lbl거래처.Size = new System.Drawing.Size(100, 23);
			this.lbl거래처.TabIndex = 0;
			this.lbl거래처.Text = "거래처";
			this.lbl거래처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl3);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(387, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.ctx원본회사);
			this.bpPanelControl3.Controls.Add(this.lbl원본회사);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(295, 23);
			this.bpPanelControl3.TabIndex = 1;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// ctx원본회사
			// 
			this.ctx원본회사.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx원본회사.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx원본회사.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx원본회사.Location = new System.Drawing.Point(106, 0);
			this.ctx원본회사.Name = "ctx원본회사";
			this.ctx원본회사.Size = new System.Drawing.Size(189, 21);
			this.ctx원본회사.TabIndex = 1;
			this.ctx원본회사.TabStop = false;
			this.ctx원본회사.Text = "bpCodeTextBox1";
			this.ctx원본회사.UserCodeName = "NM_COMPANY";
			this.ctx원본회사.UserCodeValue = "CD_COMPANY";
			this.ctx원본회사.UserHelpID = "H_CZ_MA_COMPANY_SUB";
			this.ctx원본회사.UserParams = "회사;H_CZ_MA_COMPANY_SUB";
			// 
			// lbl원본회사
			// 
			this.lbl원본회사.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl원본회사.Location = new System.Drawing.Point(0, 0);
			this.lbl원본회사.Name = "lbl원본회사";
			this.lbl원본회사.Size = new System.Drawing.Size(100, 23);
			this.lbl원본회사.TabIndex = 0;
			this.lbl원본회사.Text = "원본회사";
			this.lbl원본회사.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl2);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(387, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.bpc대상회사);
			this.bpPanelControl2.Controls.Add(this.lbl대상회사);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(295, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// bpc대상회사
			// 
			this.bpc대상회사.BackColor = System.Drawing.Color.White;
			this.bpc대상회사.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc대상회사.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.bpc대상회사.Location = new System.Drawing.Point(106, 0);
			this.bpc대상회사.Name = "bpc대상회사";
			this.bpc대상회사.Size = new System.Drawing.Size(189, 21);
			this.bpc대상회사.TabIndex = 1;
			this.bpc대상회사.TabStop = false;
			this.bpc대상회사.Text = "bpComboBox1";
			this.bpc대상회사.UserCodeName = "NM_COMPANY";
			this.bpc대상회사.UserCodeValue = "CD_COMPANY";
			this.bpc대상회사.UserHelpID = "H_CZ_MA_COMPANY_SUB";
			this.bpc대상회사.UserParams = "회사;H_CZ_MA_COMPANY_SUB";
			// 
			// lbl대상회사
			// 
			this.lbl대상회사.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl대상회사.Location = new System.Drawing.Point(0, 0);
			this.lbl대상회사.Name = "lbl대상회사";
			this.lbl대상회사.Size = new System.Drawing.Size(100, 23);
			this.lbl대상회사.TabIndex = 0;
			this.lbl대상회사.Text = "대상회사";
			this.lbl대상회사.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// P_CZ_MA_PARTNER_COPY
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(405, 182);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "P_CZ_MA_PARTNER_COPY";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ERP iU";
			this.TitleText = "거래처동기화";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn종료;
        private Duzon.Common.Controls.RoundedButton btn동기화;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.BpControls.BpCodeTextBox ctx거래처;
        private Duzon.Common.Controls.LabelExt lbl거래처;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpComboBox bpc대상회사;
        private Duzon.Common.Controls.LabelExt lbl대상회사;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpCodeTextBox ctx원본회사;
        private Duzon.Common.Controls.LabelExt lbl원본회사;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;

    }
}
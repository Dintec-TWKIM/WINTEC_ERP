
namespace cz
{
	partial class P_CZ_SA_GIR_AUTO_CPR
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
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.chk자동발송제외 = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.txtCPR발송메일 = new Duzon.Common.Controls.TextBoxExt();
			this.lblCPR발송메일 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt납품처메일 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl납품처메일 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt상업송장비고 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl상업송장비고 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt전달사항 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl전달사항 = new Duzon.Common.Controls.LabelExt();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.chkCPR발송완료 = new Duzon.Common.Controls.CheckBoxExt();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
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
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 49);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(715, 284);
			this.tableLayoutPanel1.TabIndex = 0;
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
			this.oneGrid1.Size = new System.Drawing.Size(709, 246);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.chk자동발송제외);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(699, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// chk자동발송제외
			// 
			this.chk자동발송제외.AutoSize = true;
			this.chk자동발송제외.Location = new System.Drawing.Point(590, 1);
			this.chk자동발송제외.Name = "chk자동발송제외";
			this.chk자동발송제외.Size = new System.Drawing.Size(104, 24);
			this.chk자동발송제외.TabIndex = 0;
			this.chk자동발송제외.Text = "자동발송제외";
			this.chk자동발송제외.TextDD = null;
			this.chk자동발송제외.UseVisualStyleBackColor = true;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.txtCPR발송메일);
			this.bpPanelControl2.Controls.Add(this.lblCPR발송메일);
			this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// txtCPR발송메일
			// 
			this.txtCPR발송메일.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtCPR발송메일.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtCPR발송메일.Dock = System.Windows.Forms.DockStyle.Right;
			this.txtCPR발송메일.Location = new System.Drawing.Point(106, 0);
			this.txtCPR발송메일.Name = "txtCPR발송메일";
			this.txtCPR발송메일.Size = new System.Drawing.Size(186, 21);
			this.txtCPR발송메일.TabIndex = 1;
			this.txtCPR발송메일.Tag = "NO_DELIVERY_EMAIL";
			// 
			// lblCPR발송메일
			// 
			this.lblCPR발송메일.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblCPR발송메일.Location = new System.Drawing.Point(0, 0);
			this.lblCPR발송메일.Name = "lblCPR발송메일";
			this.lblCPR발송메일.Size = new System.Drawing.Size(100, 23);
			this.lblCPR발송메일.TabIndex = 0;
			this.lblCPR발송메일.Text = "CPR발송메일";
			this.lblCPR발송메일.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.txt납품처메일);
			this.bpPanelControl3.Controls.Add(this.lbl납품처메일);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// txt납품처메일
			// 
			this.txt납품처메일.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt납품처메일.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt납품처메일.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt납품처메일.Location = new System.Drawing.Point(106, 0);
			this.txt납품처메일.Name = "txt납품처메일";
			this.txt납품처메일.ReadOnly = true;
			this.txt납품처메일.Size = new System.Drawing.Size(186, 21);
			this.txt납품처메일.TabIndex = 1;
			this.txt납품처메일.TabStop = false;
			this.txt납품처메일.Tag = "NO_EMAIL";
			// 
			// lbl납품처메일
			// 
			this.lbl납품처메일.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl납품처메일.Location = new System.Drawing.Point(0, 0);
			this.lbl납품처메일.Name = "lbl납품처메일";
			this.lbl납품처메일.Size = new System.Drawing.Size(100, 23);
			this.lbl납품처메일.TabIndex = 0;
			this.lbl납품처메일.Text = "납품처메일";
			this.lbl납품처메일.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl1);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(699, 69);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoWidth;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.txt상업송장비고);
			this.bpPanelControl1.Controls.Add(this.lbl상업송장비고);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(692, 69);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// txt상업송장비고
			// 
			this.txt상업송장비고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt상업송장비고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt상업송장비고.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt상업송장비고.Location = new System.Drawing.Point(106, 0);
			this.txt상업송장비고.Multiline = true;
			this.txt상업송장비고.Name = "txt상업송장비고";
			this.txt상업송장비고.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txt상업송장비고.Size = new System.Drawing.Size(586, 69);
			this.txt상업송장비고.TabIndex = 1;
			this.txt상업송장비고.Tag = "DC_RMK_CI";
			this.txt상업송장비고.UseKeyEnter = false;
			// 
			// lbl상업송장비고
			// 
			this.lbl상업송장비고.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl상업송장비고.Location = new System.Drawing.Point(0, 0);
			this.lbl상업송장비고.Name = "lbl상업송장비고";
			this.lbl상업송장비고.Size = new System.Drawing.Size(100, 69);
			this.lbl상업송장비고.TabIndex = 0;
			this.lbl상업송장비고.Text = "상업송장비고";
			this.lbl상업송장비고.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl4);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 92);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(699, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.txt전달사항);
			this.bpPanelControl4.Controls.Add(this.lbl전달사항);
			this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(692, 23);
			this.bpPanelControl4.TabIndex = 1;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// txt전달사항
			// 
			this.txt전달사항.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt전달사항.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt전달사항.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt전달사항.Location = new System.Drawing.Point(106, 0);
			this.txt전달사항.Name = "txt전달사항";
			this.txt전달사항.Size = new System.Drawing.Size(586, 21);
			this.txt전달사항.TabIndex = 1;
			this.txt전달사항.Tag = "DC_RMK_CPR";
			// 
			// lbl전달사항
			// 
			this.lbl전달사항.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl전달사항.Location = new System.Drawing.Point(0, 0);
			this.lbl전달사항.Name = "lbl전달사항";
			this.lbl전달사항.Size = new System.Drawing.Size(100, 23);
			this.lbl전달사항.TabIndex = 0;
			this.lbl전달사항.Text = "전달사항";
			this.lbl전달사항.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn저장);
			this.flowLayoutPanel1.Controls.Add(this.btn조회);
			this.flowLayoutPanel1.Controls.Add(this.chkCPR발송완료);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(709, 26);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// btn저장
			// 
			this.btn저장.BackColor = System.Drawing.Color.Transparent;
			this.btn저장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn저장.Location = new System.Drawing.Point(636, 3);
			this.btn저장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn저장.Name = "btn저장";
			this.btn저장.Size = new System.Drawing.Size(70, 19);
			this.btn저장.TabIndex = 1;
			this.btn저장.TabStop = false;
			this.btn저장.Text = "저장";
			this.btn저장.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.BackColor = System.Drawing.Color.Transparent;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(560, 3);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 0;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// chkCPR발송완료
			// 
			this.chkCPR발송완료.AutoSize = true;
			this.chkCPR발송완료.Location = new System.Drawing.Point(464, 3);
			this.chkCPR발송완료.Name = "chkCPR발송완료";
			this.chkCPR발송완료.Size = new System.Drawing.Size(90, 16);
			this.chkCPR발송완료.TabIndex = 0;
			this.chkCPR발송완료.Text = "CPR발송완료";
			this.chkCPR발송완료.TextDD = null;
			this.chkCPR발송완료.UseVisualStyleBackColor = true;
			// 
			// P_CZ_SA_GIR_AUTO_CPR
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(719, 334);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_CZ_SA_GIR_AUTO_CPR";
			this.Text = "P_CZ_SA_GIR_AUTO_CPR";
			this.TitleText = "CPR 자동발송 설정";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.oneGridItem1.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl2.PerformLayout();
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl4.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private Duzon.Common.Controls.RoundedButton btn조회;
		private Duzon.Common.Controls.RoundedButton btn저장;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.TextBoxExt txt상업송장비고;
		private Duzon.Common.Controls.LabelExt lbl상업송장비고;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.TextBoxExt txtCPR발송메일;
		private Duzon.Common.Controls.LabelExt lblCPR발송메일;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.Controls.TextBoxExt txt납품처메일;
		private Duzon.Common.Controls.LabelExt lbl납품처메일;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
		private Duzon.Common.Controls.TextBoxExt txt전달사항;
		private Duzon.Common.Controls.LabelExt lbl전달사항;
		private Duzon.Common.Controls.CheckBoxExt chk자동발송제외;
		private Duzon.Common.Controls.CheckBoxExt chkCPR발송완료;
	}
}
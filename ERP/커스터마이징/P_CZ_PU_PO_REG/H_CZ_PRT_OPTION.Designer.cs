namespace cz
{
	partial class H_CZ_PRT_OPTION
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
			this.panelExt1 = new Duzon.Common.Controls.PanelExt();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btn인쇄 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl파일번호 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk발주번호인쇄 = new Duzon.Common.Controls.CheckBoxExt();
			this.chkInquiry = new Duzon.Common.Controls.CheckBoxExt();
			this.chkAgency = new Duzon.Common.Controls.CheckBoxExt();
			this.chkLogAddress = new Duzon.Common.Controls.CheckBoxExt();
			this.lbl표시여부 = new Duzon.Common.Controls.LabelExt();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.panelExt1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.pnl파일번호.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelExt1
			// 
			this.panelExt1.BackColor = System.Drawing.Color.Transparent;
			this.panelExt1.Controls.Add(this.panel1);
			this.panelExt1.Controls.Add(this.oneGrid1);
			this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt1.Location = new System.Drawing.Point(0, 0);
			this.panelExt1.Name = "panelExt1";
			this.panelExt1.Padding = new System.Windows.Forms.Padding(6, 53, 6, 3);
			this.panelExt1.Size = new System.Drawing.Size(737, 153);
			this.panelExt1.TabIndex = 5;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btn인쇄);
			this.panel1.Controls.Add(this.btn취소);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.ForeColor = System.Drawing.Color.Transparent;
			this.panel1.Location = new System.Drawing.Point(6, 97);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(725, 44);
			this.panel1.TabIndex = 9;
			// 
			// btn인쇄
			// 
			this.btn인쇄.BackColor = System.Drawing.Color.White;
			this.btn인쇄.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn인쇄.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn인쇄.Location = new System.Drawing.Point(275, 12);
			this.btn인쇄.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn인쇄.Name = "btn인쇄";
			this.btn인쇄.Size = new System.Drawing.Size(70, 19);
			this.btn인쇄.TabIndex = 4;
			this.btn인쇄.TabStop = false;
			this.btn인쇄.Text = "인쇄";
			this.btn인쇄.UseVisualStyleBackColor = false;
			// 
			// btn취소
			// 
			this.btn취소.BackColor = System.Drawing.Color.Transparent;
			this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.Location = new System.Drawing.Point(360, 12);
			this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(70, 19);
			this.btn취소.TabIndex = 3;
			this.btn취소.TabStop = false;
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(6, 53);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(725, 44);
			this.oneGrid1.TabIndex = 6;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.pnl파일번호);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(715, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// pnl파일번호
			// 
			this.pnl파일번호.Controls.Add(this.chk발주번호인쇄);
			this.pnl파일번호.Controls.Add(this.chkInquiry);
			this.pnl파일번호.Controls.Add(this.chkAgency);
			this.pnl파일번호.Controls.Add(this.chkLogAddress);
			this.pnl파일번호.Controls.Add(this.lbl표시여부);
			this.pnl파일번호.Location = new System.Drawing.Point(2, 1);
			this.pnl파일번호.Name = "pnl파일번호";
			this.pnl파일번호.Size = new System.Drawing.Size(660, 23);
			this.pnl파일번호.TabIndex = 3;
			this.pnl파일번호.Text = "bpPanelControl1";
			// 
			// chk발주번호인쇄
			// 
			this.chk발주번호인쇄.Enabled = false;
			this.chk발주번호인쇄.Location = new System.Drawing.Point(490, 4);
			this.chk발주번호인쇄.Name = "chk발주번호인쇄";
			this.chk발주번호인쇄.Size = new System.Drawing.Size(100, 16);
			this.chk발주번호인쇄.TabIndex = 6;
			this.chk발주번호인쇄.Text = "발주번호인쇄";
			this.chk발주번호인쇄.TextDD = null;
			this.chk발주번호인쇄.UseVisualStyleBackColor = true;
			// 
			// chkInquiry
			// 
			this.chkInquiry.Location = new System.Drawing.Point(301, 5);
			this.chkInquiry.Name = "chkInquiry";
			this.chkInquiry.Size = new System.Drawing.Size(183, 16);
			this.chkInquiry.TabIndex = 5;
			this.chkInquiry.Text = "매입문의서 발송(대행업체)";
			this.chkInquiry.TextDD = null;
			this.chkInquiry.UseVisualStyleBackColor = true;
			// 
			// chkAgency
			// 
			this.chkAgency.Location = new System.Drawing.Point(210, 5);
			this.chkAgency.Name = "chkAgency";
			this.chkAgency.Size = new System.Drawing.Size(100, 16);
			this.chkAgency.TabIndex = 4;
			this.chkAgency.Text = "대행업체";
			this.chkAgency.TextDD = null;
			this.chkAgency.UseVisualStyleBackColor = true;
			// 
			// chkLogAddress
			// 
			this.chkLogAddress.Checked = true;
			this.chkLogAddress.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLogAddress.Location = new System.Drawing.Point(104, 5);
			this.chkLogAddress.Name = "chkLogAddress";
			this.chkLogAddress.Size = new System.Drawing.Size(100, 16);
			this.chkLogAddress.TabIndex = 3;
			this.chkLogAddress.Text = "물류센터주소";
			this.chkLogAddress.TextDD = null;
			this.chkLogAddress.UseVisualStyleBackColor = true;
			// 
			// lbl표시여부
			// 
			this.lbl표시여부.Location = new System.Drawing.Point(17, 4);
			this.lbl표시여부.Name = "lbl표시여부";
			this.lbl표시여부.Size = new System.Drawing.Size(65, 16);
			this.lbl표시여부.TabIndex = 1;
			this.lbl표시여부.Text = "표시여부";
			this.lbl표시여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// H_CZ_PRT_OPTION
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(737, 153);
			this.Controls.Add(this.panelExt1);
			this.Name = "H_CZ_PRT_OPTION";
			this.Text = "H_CZ_PRT_OPTION";
			this.TitleText = "인쇄 옵션";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.panelExt1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.pnl파일번호.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Duzon.Common.Controls.PanelExt panelExt1;
		private System.Windows.Forms.Panel panel1;
		private Duzon.Common.Controls.RoundedButton btn인쇄;
		private Duzon.Common.Controls.RoundedButton btn취소;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl pnl파일번호;
		private Duzon.Common.Controls.CheckBoxExt chkLogAddress;
		private Duzon.Common.Controls.LabelExt lbl표시여부;
		private Duzon.Common.Controls.CheckBoxExt chkAgency;
		private Duzon.Common.Controls.CheckBoxExt chkInquiry;
		private Duzon.Common.Controls.CheckBoxExt chk발주번호인쇄;
	}
}
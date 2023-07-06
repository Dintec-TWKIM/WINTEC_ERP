namespace cz
{
	partial class H_CZ_STATUS_TABLE
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
			this.tblMain = new System.Windows.Forms.TableLayoutPanel();
			this.oneEdit = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt5 = new Duzon.Common.Controls.LabelExt();
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.lbl결재 = new Duzon.Common.Controls.LabelExt();
			this.btn전자결재 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.txt비고 = new Duzon.Common.Controls.TextBoxExt();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tblMain.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tblMain
			// 
			this.tblMain.ColumnCount = 1;
			this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tblMain.Controls.Add(this.oneEdit, 0, 1);
			this.tblMain.Controls.Add(this.webBrowser1, 0, 0);
			this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tblMain.Location = new System.Drawing.Point(3, 50);
			this.tblMain.Name = "tblMain";
			this.tblMain.RowCount = 2;
			this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.70242F));
			this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.29758F));
			this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tblMain.Size = new System.Drawing.Size(990, 578);
			this.tblMain.TabIndex = 0;
			// 
			// oneEdit
			// 
			this.oneEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneEdit.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneEdit.Location = new System.Drawing.Point(3, 429);
			this.oneEdit.Name = "oneEdit";
			this.oneEdit.Size = new System.Drawing.Size(984, 146);
			this.oneEdit.TabIndex = 3;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(974, 130);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoWidth;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.txt비고);
			this.bpPanelControl1.Controls.Add(this.labelExt5);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(952, 125);
			this.bpPanelControl1.TabIndex = 8;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// labelExt5
			// 
			this.labelExt5.Location = new System.Drawing.Point(17, 4);
			this.labelExt5.Name = "labelExt5";
			this.labelExt5.Size = new System.Drawing.Size(65, 16);
			this.labelExt5.TabIndex = 5;
			this.labelExt5.Text = "비고";
			this.labelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// webBrowser1
			// 
			this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser1.Location = new System.Drawing.Point(3, 3);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(984, 420);
			this.webBrowser1.TabIndex = 4;
			// 
			// lbl결재
			// 
			this.lbl결재.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbl결재.AutoSize = true;
			this.lbl결재.BackColor = System.Drawing.Color.Transparent;
			this.lbl결재.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl결재.ForeColor = System.Drawing.Color.LightGray;
			this.lbl결재.Location = new System.Drawing.Point(783, 24);
			this.lbl결재.Name = "lbl결재";
			this.lbl결재.Size = new System.Drawing.Size(31, 12);
			this.lbl결재.TabIndex = 16;
			this.lbl결재.Text = "결재";
			this.lbl결재.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btn전자결재
			// 
			this.btn전자결재.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn전자결재.BackColor = System.Drawing.Color.White;
			this.btn전자결재.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전자결재.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전자결재.Location = new System.Drawing.Point(841, 20);
			this.btn전자결재.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전자결재.Name = "btn전자결재";
			this.btn전자결재.Size = new System.Drawing.Size(70, 19);
			this.btn전자결재.TabIndex = 14;
			this.btn전자결재.TabStop = false;
			this.btn전자결재.Text = "전자결재";
			this.btn전자결재.UseVisualStyleBackColor = false;
			// 
			// btn취소
			// 
			this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn취소.BackColor = System.Drawing.Color.Transparent;
			this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.Location = new System.Drawing.Point(917, 20);
			this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(70, 19);
			this.btn취소.TabIndex = 13;
			this.btn취소.TabStop = false;
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// txt비고
			// 
			this.txt비고.BackColor = System.Drawing.Color.White;
			this.txt비고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt비고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt비고.Location = new System.Drawing.Point(84, 1);
			this.txt비고.Multiline = true;
			this.txt비고.Name = "txt비고";
			this.txt비고.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txt비고.Size = new System.Drawing.Size(856, 121);
			this.txt비고.TabIndex = 7;
			this.txt비고.Tag = "DC_RMK";
			this.txt비고.UseKeyEnter = false;
			// 
			// H_CZ_STATUS_TABLE
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(996, 631);
			this.Controls.Add(this.lbl결재);
			this.Controls.Add(this.btn전자결재);
			this.Controls.Add(this.btn취소);
			this.Controls.Add(this.tblMain);
			this.Name = "H_CZ_STATUS_TABLE";
			this.Padding = new System.Windows.Forms.Padding(3, 50, 3, 3);
			this.ShowInTaskbar = false;
			this.Text = "H_CZ_STATUS_TABLE";
			this.TitleText = "견적현황표";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tblMain.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tblMain;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneEdit;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.LabelExt labelExt5;
		private System.Windows.Forms.WebBrowser webBrowser1;
		private Duzon.Common.Controls.LabelExt lbl결재;
		private Duzon.Common.Controls.RoundedButton btn전자결재;
		private Duzon.Common.Controls.RoundedButton btn취소;
		private Duzon.Common.Controls.TextBoxExt txt비고;
	}
}
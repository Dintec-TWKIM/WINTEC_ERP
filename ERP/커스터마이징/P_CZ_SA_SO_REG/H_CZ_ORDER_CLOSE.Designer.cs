namespace cz
{
	partial class H_CZ_ORDER_CLOSE
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
			this.btn복구 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn전자결재 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl사아유 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt5 = new Duzon.Common.Controls.LabelExt();
			this.pnl사유 = new Duzon.Common.Controls.PanelEx();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl비고 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt비고 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.lbl결재 = new Duzon.Common.Controls.LabelExt();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.pnl사아유.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.pnl비고.SuspendLayout();
			this.SuspendLayout();
			// 
			// btn복구
			// 
			this.btn복구.BackColor = System.Drawing.Color.Transparent;
			this.btn복구.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn복구.Enabled = false;
			this.btn복구.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn복구.Location = new System.Drawing.Point(366, 23);
			this.btn복구.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn복구.Name = "btn복구";
			this.btn복구.Size = new System.Drawing.Size(70, 19);
			this.btn복구.TabIndex = 6;
			this.btn복구.TabStop = false;
			this.btn복구.Text = "복구";
			this.btn복구.UseVisualStyleBackColor = false;
			// 
			// btn전자결재
			// 
			this.btn전자결재.BackColor = System.Drawing.Color.White;
			this.btn전자결재.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전자결재.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전자결재.Location = new System.Drawing.Point(290, 23);
			this.btn전자결재.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전자결재.Name = "btn전자결재";
			this.btn전자결재.Size = new System.Drawing.Size(70, 19);
			this.btn전자결재.TabIndex = 5;
			this.btn전자결재.TabStop = false;
			this.btn전자결재.Text = "전자결재";
			this.btn전자결재.UseVisualStyleBackColor = false;
			// 
			// btn취소
			// 
			this.btn취소.BackColor = System.Drawing.Color.Transparent;
			this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.Location = new System.Drawing.Point(442, 23);
			this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(70, 19);
			this.btn취소.TabIndex = 4;
			this.btn취소.TabStop = false;
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 50);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 323F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 323F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(513, 323);
			this.tableLayoutPanel1.TabIndex = 7;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(507, 317);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.pnl사아유);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(497, 216);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoWidth;
			this.oneGridItem1.TabIndex = 0;
			// 
			// pnl사아유
			// 
			this.pnl사아유.Controls.Add(this.labelExt5);
			this.pnl사아유.Controls.Add(this.pnl사유);
			this.pnl사아유.Location = new System.Drawing.Point(2, 1);
			this.pnl사아유.Name = "pnl사아유";
			this.pnl사아유.Size = new System.Drawing.Size(485, 213);
			this.pnl사아유.TabIndex = 8;
			this.pnl사아유.Text = "bpPanelControl1";
			// 
			// labelExt5
			// 
			this.labelExt5.Location = new System.Drawing.Point(17, 4);
			this.labelExt5.Name = "labelExt5";
			this.labelExt5.Size = new System.Drawing.Size(65, 16);
			this.labelExt5.TabIndex = 5;
			this.labelExt5.Text = "마감사유";
			this.labelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl사유
			// 
			this.pnl사유.ColorA = System.Drawing.Color.Empty;
			this.pnl사유.ColorB = System.Drawing.Color.Empty;
			this.pnl사유.Location = new System.Drawing.Point(84, 1);
			this.pnl사유.Name = "pnl사유";
			this.pnl사유.Size = new System.Drawing.Size(394, 212);
			this.pnl사유.TabIndex = 6;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.pnl비고);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 216);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(497, 85);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoWidth;
			this.oneGridItem2.TabIndex = 1;
			// 
			// pnl비고
			// 
			this.pnl비고.Controls.Add(this.txt비고);
			this.pnl비고.Controls.Add(this.labelExt1);
			this.pnl비고.Location = new System.Drawing.Point(2, 1);
			this.pnl비고.Name = "pnl비고";
			this.pnl비고.Size = new System.Drawing.Size(485, 85);
			this.pnl비고.TabIndex = 8;
			this.pnl비고.Text = "bpPanelControl2";
			// 
			// txt비고
			// 
			this.txt비고.BackColor = System.Drawing.SystemColors.Window;
			this.txt비고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt비고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt비고.Location = new System.Drawing.Point(84, 1);
			this.txt비고.Multiline = true;
			this.txt비고.Name = "txt비고";
			this.txt비고.Size = new System.Drawing.Size(394, 82);
			this.txt비고.TabIndex = 6;
			this.txt비고.Tag = "DC_RMK";
			this.txt비고.UseKeyEnter = false;
			// 
			// labelExt1
			// 
			this.labelExt1.Location = new System.Drawing.Point(17, 4);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(65, 16);
			this.labelExt1.TabIndex = 5;
			this.labelExt1.Text = "비고";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl결재
			// 
			this.lbl결재.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbl결재.AutoSize = true;
			this.lbl결재.BackColor = System.Drawing.Color.Transparent;
			this.lbl결재.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl결재.ForeColor = System.Drawing.Color.LightGray;
			this.lbl결재.Location = new System.Drawing.Point(243, 26);
			this.lbl결재.Name = "lbl결재";
			this.lbl결재.Size = new System.Drawing.Size(31, 12);
			this.lbl결재.TabIndex = 12;
			this.lbl결재.Text = "결재";
			this.lbl결재.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// H_CZ_ORDER_CLOSE
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(519, 376);
			this.Controls.Add(this.lbl결재);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.btn복구);
			this.Controls.Add(this.btn전자결재);
			this.Controls.Add(this.btn취소);
			this.Name = "H_CZ_ORDER_CLOSE";
			this.Padding = new System.Windows.Forms.Padding(3, 50, 3, 3);
			this.Text = "H_CZ_ORDER_CLOSE";
			this.TitleText = "수주 마감";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.pnl사아유.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.pnl비고.ResumeLayout(false);
			this.pnl비고.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Duzon.Common.Controls.RoundedButton btn복구;
		private Duzon.Common.Controls.RoundedButton btn전자결재;
		private Duzon.Common.Controls.RoundedButton btn취소;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl pnl사아유;
		private Duzon.Common.Controls.LabelExt labelExt5;
		private Duzon.Common.Controls.PanelEx pnl사유;
		private Duzon.Common.BpControls.BpPanelControl pnl비고;
		private Duzon.Common.Controls.TextBoxExt txt비고;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Duzon.Common.Controls.LabelExt lbl결재;
	}
}
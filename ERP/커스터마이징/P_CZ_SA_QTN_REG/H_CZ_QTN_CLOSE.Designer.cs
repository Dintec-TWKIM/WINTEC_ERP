namespace cz
{
	partial class H_CZ_QTN_CLOSE
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
			this.tlayM = new System.Windows.Forms.TableLayoutPanel();
			this.oneH = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt5 = new Duzon.Common.Controls.LabelExt();
			this.pnl사유 = new Duzon.Common.Controls.PanelEx();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt비고 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.btn마감 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tlayM.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btn복구
			// 
			this.btn복구.BackColor = System.Drawing.Color.Transparent;
			this.btn복구.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn복구.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn복구.Location = new System.Drawing.Point(366, 23);
			this.btn복구.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn복구.Name = "btn복구";
			this.btn복구.Size = new System.Drawing.Size(70, 19);
			this.btn복구.TabIndex = 3;
			this.btn복구.TabStop = false;
			this.btn복구.Text = "복구";
			this.btn복구.UseVisualStyleBackColor = false;
			// 
			// tlayM
			// 
			this.tlayM.ColumnCount = 1;
			this.tlayM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayM.Controls.Add(this.oneH, 0, 0);
			this.tlayM.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayM.Location = new System.Drawing.Point(3, 50);
			this.tlayM.Name = "tlayM";
			this.tlayM.RowCount = 1;
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlayM.Size = new System.Drawing.Size(513, 323);
			this.tlayM.TabIndex = 1;
			// 
			// oneH
			// 
			this.oneH.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneH.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneH.Location = new System.Drawing.Point(3, 3);
			this.oneH.Name = "oneH";
			this.oneH.Size = new System.Drawing.Size(507, 318);
			this.oneH.TabIndex = 7;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(497, 216);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoWidth;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.labelExt5);
			this.bpPanelControl1.Controls.Add(this.pnl사유);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(492, 213);
			this.bpPanelControl1.TabIndex = 7;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// labelExt5
			// 
			this.labelExt5.Location = new System.Drawing.Point(17, 4);
			this.labelExt5.Name = "labelExt5";
			this.labelExt5.Resizeble = true;
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
			this.oneGridItem2.Controls.Add(this.bpPanelControl2);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 216);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(497, 86);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoWidth;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.txt비고);
			this.bpPanelControl2.Controls.Add(this.labelExt1);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(492, 83);
			this.bpPanelControl2.TabIndex = 7;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// txt비고
			// 
			this.txt비고.BackColor = System.Drawing.Color.White;
			this.txt비고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt비고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt비고.Location = new System.Drawing.Point(84, 1);
			this.txt비고.Multiline = true;
			this.txt비고.Name = "txt비고";
			this.txt비고.SelectedAllEnabled = false;
			this.txt비고.Size = new System.Drawing.Size(394, 82);
			this.txt비고.TabIndex = 6;
			this.txt비고.Tag = "DC_RMK";
			this.txt비고.UseKeyEnter = false;
			this.txt비고.UseKeyF3 = true;
			// 
			// labelExt1
			// 
			this.labelExt1.Location = new System.Drawing.Point(17, 4);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Resizeble = true;
			this.labelExt1.Size = new System.Drawing.Size(65, 16);
			this.labelExt1.TabIndex = 5;
			this.labelExt1.Text = "비고";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btn마감
			// 
			this.btn마감.BackColor = System.Drawing.Color.White;
			this.btn마감.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn마감.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn마감.Location = new System.Drawing.Point(290, 23);
			this.btn마감.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn마감.Name = "btn마감";
			this.btn마감.Size = new System.Drawing.Size(70, 19);
			this.btn마감.TabIndex = 2;
			this.btn마감.TabStop = false;
			this.btn마감.Text = "마감";
			this.btn마감.UseVisualStyleBackColor = false;
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
			this.btn취소.TabIndex = 1;
			this.btn취소.TabStop = false;
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// H_CZ_QTN_CLOSE
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(519, 376);
			this.Controls.Add(this.tlayM);
			this.Controls.Add(this.btn복구);
			this.Controls.Add(this.btn마감);
			this.Controls.Add(this.btn취소);
			this.Name = "H_CZ_QTN_CLOSE";
			this.Padding = new System.Windows.Forms.Padding(3, 50, 3, 3);
			this.TitleText = "견적 마감";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tlayM.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlayM;
		private Duzon.Common.Controls.RoundedButton btn마감;
		private Duzon.Common.Controls.RoundedButton btn취소;
		private Duzon.Common.Controls.PanelEx pnl사유;
		private Duzon.Common.Controls.RoundedButton btn복구;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneH;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.LabelExt labelExt5;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Duzon.Common.Controls.TextBoxExt txt비고;
	}
}
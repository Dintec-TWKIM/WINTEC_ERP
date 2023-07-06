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
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.chkSelItem = new Duzon.Common.Controls.CheckBoxExt();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl파일번호 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo인쇄형태 = new Duzon.Common.Controls.DropDownComboBox();
			this.chk바탕화면저장 = new Duzon.Common.Controls.CheckBoxExt();
			this.chkAgentLogo = new Duzon.Common.Controls.CheckBoxExt();
			this.chkRevised = new Duzon.Common.Controls.CheckBoxExt();
			this.lbl표시여부 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.tbx거래처명 = new Dintec.UTextBox();
			this.lbl거래처 = new Duzon.Common.Controls.LabelExt();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.panelExt1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.pnl파일번호.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
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
			this.panelExt1.Size = new System.Drawing.Size(592, 189);
			this.panelExt1.TabIndex = 4;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btn인쇄);
			this.panel1.Controls.Add(this.btn취소);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.ForeColor = System.Drawing.Color.Transparent;
			this.panel1.Location = new System.Drawing.Point(6, 138);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(580, 44);
			this.panel1.TabIndex = 9;
			// 
			// btn인쇄
			// 
			this.btn인쇄.BackColor = System.Drawing.Color.White;
			this.btn인쇄.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn인쇄.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn인쇄.Location = new System.Drawing.Point(264, 12);
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
			this.btn취소.Location = new System.Drawing.Point(349, 12);
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
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
			this.oneGrid1.Location = new System.Drawing.Point(6, 53);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(580, 85);
			this.oneGrid1.TabIndex = 6;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(570, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.chkSelItem);
			this.bpPanelControl1.Controls.Add(this.labelExt1);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(465, 23);
			this.bpPanelControl1.TabIndex = 3;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// chkSelItem
			// 
			this.chkSelItem.Location = new System.Drawing.Point(104, 5);
			this.chkSelItem.Name = "chkSelItem";
			this.chkSelItem.Size = new System.Drawing.Size(120, 16);
			this.chkSelItem.TabIndex = 3;
			this.chkSelItem.Text = "선택된 아이템만";
			this.chkSelItem.TextDD = null;
			this.chkSelItem.UseVisualStyleBackColor = true;
			// 
			// labelExt1
			// 
			this.labelExt1.Location = new System.Drawing.Point(17, 4);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(65, 16);
			this.labelExt1.TabIndex = 1;
			this.labelExt1.Text = "Inquiry";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.pnl파일번호);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(570, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// pnl파일번호
			// 
			this.pnl파일번호.Controls.Add(this.cbo인쇄형태);
			this.pnl파일번호.Controls.Add(this.chk바탕화면저장);
			this.pnl파일번호.Controls.Add(this.chkAgentLogo);
			this.pnl파일번호.Controls.Add(this.chkRevised);
			this.pnl파일번호.Controls.Add(this.lbl표시여부);
			this.pnl파일번호.Location = new System.Drawing.Point(2, 1);
			this.pnl파일번호.Name = "pnl파일번호";
			this.pnl파일번호.Size = new System.Drawing.Size(735, 23);
			this.pnl파일번호.TabIndex = 2;
			this.pnl파일번호.Text = "bpPanelControl1";
			// 
			// cbo인쇄형태
			// 
			this.cbo인쇄형태.AutoDropDown = true;
			this.cbo인쇄형태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo인쇄형태.FormattingEnabled = true;
			this.cbo인쇄형태.ItemHeight = 12;
			this.cbo인쇄형태.Location = new System.Drawing.Point(104, 1);
			this.cbo인쇄형태.Name = "cbo인쇄형태";
			this.cbo인쇄형태.Size = new System.Drawing.Size(84, 20);
			this.cbo인쇄형태.TabIndex = 8;
			this.cbo인쇄형태.Tag = "";
			// 
			// chk바탕화면저장
			// 
			this.chk바탕화면저장.Location = new System.Drawing.Point(393, 5);
			this.chk바탕화면저장.Name = "chk바탕화면저장";
			this.chk바탕화면저장.Size = new System.Drawing.Size(104, 16);
			this.chk바탕화면저장.TabIndex = 5;
			this.chk바탕화면저장.Text = "바탕화면저장";
			this.chk바탕화면저장.TextDD = null;
			this.chk바탕화면저장.UseVisualStyleBackColor = true;
			// 
			// chkAgentLogo
			// 
			this.chkAgentLogo.Checked = true;
			this.chkAgentLogo.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAgentLogo.Location = new System.Drawing.Point(203, 5);
			this.chkAgentLogo.Name = "chkAgentLogo";
			this.chkAgentLogo.Size = new System.Drawing.Size(93, 16);
			this.chkAgentLogo.TabIndex = 3;
			this.chkAgentLogo.Text = "대리점 로고";
			this.chkAgentLogo.TextDD = null;
			this.chkAgentLogo.UseVisualStyleBackColor = true;
			// 
			// chkRevised
			// 
			this.chkRevised.Location = new System.Drawing.Point(302, 5);
			this.chkRevised.Name = "chkRevised";
			this.chkRevised.Size = new System.Drawing.Size(78, 16);
			this.chkRevised.TabIndex = 2;
			this.chkRevised.Text = "Revised";
			this.chkRevised.TextDD = null;
			this.chkRevised.UseVisualStyleBackColor = true;
			// 
			// lbl표시여부
			// 
			this.lbl표시여부.Location = new System.Drawing.Point(17, 4);
			this.lbl표시여부.Name = "lbl표시여부";
			this.lbl표시여부.Size = new System.Drawing.Size(65, 16);
			this.lbl표시여부.TabIndex = 1;
			this.lbl표시여부.Text = "Quotation";
			this.lbl표시여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl2);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(570, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.tbx거래처명);
			this.bpPanelControl2.Controls.Add(this.lbl거래처);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(564, 23);
			this.bpPanelControl2.TabIndex = 3;
			this.bpPanelControl2.Text = "bpPanelControl1";
			// 
			// tbx거래처명
			// 
			this.tbx거래처명.BackColor = System.Drawing.Color.White;
			this.tbx거래처명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx거래처명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx거래처명.ColorTag = System.Drawing.Color.Empty;
			this.tbx거래처명.Location = new System.Drawing.Point(104, 1);
			this.tbx거래처명.Name = "tbx거래처명";
			this.tbx거래처명.Size = new System.Drawing.Size(315, 20);
			this.tbx거래처명.TabIndex = 55;
			this.tbx거래처명.Tag = "";
			this.tbx거래처명.TextSelectAll = false;
			this.tbx거래처명.UseKeyEnter = false;
			// 
			// lbl거래처
			// 
			this.lbl거래처.Location = new System.Drawing.Point(17, 4);
			this.lbl거래처.Name = "lbl거래처";
			this.lbl거래처.Size = new System.Drawing.Size(65, 16);
			this.lbl거래처.TabIndex = 2;
			this.lbl거래처.Text = "거래처명";
			this.lbl거래처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// H_CZ_PRT_OPTION
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(592, 189);
			this.Controls.Add(this.panelExt1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "H_CZ_PRT_OPTION";
			this.Text = "H_CZ_PRT_OPTION";
			this.TitleText = "인쇄 옵션";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.panelExt1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.pnl파일번호.ResumeLayout(false);
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Duzon.Common.Controls.PanelExt panelExt1;
		private Duzon.Common.Controls.RoundedButton btn인쇄;
		private Duzon.Common.Controls.RoundedButton btn취소;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl pnl파일번호;
		private Duzon.Common.Controls.LabelExt lbl표시여부;
		private Duzon.Common.Controls.CheckBoxExt chkAgentLogo;
		private Duzon.Common.Controls.CheckBoxExt chkRevised;
		private System.Windows.Forms.Panel panel1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.CheckBoxExt chkSelItem;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.Controls.CheckBoxExt chk바탕화면저장;
		private Duzon.Common.Controls.DropDownComboBox cbo인쇄형태;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.LabelExt lbl거래처;
		private Dintec.UTextBox tbx거래처명;
	}
}
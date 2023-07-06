namespace cz
{
	partial class P_CZ_MM_BAD_STOCK_RPT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MM_BAD_STOCK_RPT));
			this.tlayMain = new System.Windows.Forms.TableLayoutPanel();
			this.grdList = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneMain = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnlClassifyS = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt9 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo키워드 = new Duzon.Common.Controls.DropDownComboBox();
			this.tbxKeyword = new Duzon.Common.Controls.TextBoxExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt3 = new Duzon.Common.Controls.LabelExt();
			this.tbxItemName = new Duzon.Common.Controls.TextBoxExt();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt7 = new Duzon.Common.Controls.LabelExt();
			this.tbxItemCode = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl검색옵션 = new Duzon.Common.BpControls.BpPanelControl();
			this.chkCond2 = new Duzon.Common.Controls.CheckBoxExt();
			this.chkCond1 = new Duzon.Common.Controls.CheckBoxExt();
			this.labelExt4 = new Duzon.Common.Controls.LabelExt();
			this.chkCond3 = new Duzon.Common.Controls.CheckBoxExt();
			this.ctxClassL = new Duzon.Common.BpControls.BpCodeTextBox();
			this.ctxClassM = new Duzon.Common.BpControls.BpCodeTextBox();
			this.ctxClassS = new Duzon.Common.BpControls.BpCodeTextBox();
			this.mDataArea.SuspendLayout();
			this.tlayMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.pnlClassifyS.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.pnl검색옵션.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tlayMain);
			this.mDataArea.Size = new System.Drawing.Size(1200, 560);
			// 
			// tlayMain
			// 
			this.tlayMain.ColumnCount = 1;
			this.tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayMain.Controls.Add(this.grdList, 0, 1);
			this.tlayMain.Controls.Add(this.oneMain, 0, 0);
			this.tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayMain.Location = new System.Drawing.Point(0, 0);
			this.tlayMain.Name = "tlayMain";
			this.tlayMain.RowCount = 2;
			this.tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayMain.Size = new System.Drawing.Size(1200, 560);
			this.tlayMain.TabIndex = 1;
			// 
			// grdList
			// 
			this.grdList.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grdList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grdList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grdList.AutoResize = false;
			this.grdList.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdList.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grdList.EnabledHeaderCheck = true;
			this.grdList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grdList.Location = new System.Drawing.Point(3, 94);
			this.grdList.Name = "grdList";
			this.grdList.Rows.Count = 1;
			this.grdList.Rows.DefaultSize = 20;
			this.grdList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grdList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grdList.ShowSort = false;
			this.grdList.Size = new System.Drawing.Size(1194, 463);
			this.grdList.StyleInfo = resources.GetString("grdList.StyleInfo");
			this.grdList.TabIndex = 2;
			this.grdList.UseGridCalculator = true;
			// 
			// oneMain
			// 
			this.oneMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneMain.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
			this.oneMain.Location = new System.Drawing.Point(3, 3);
			this.oneMain.Name = "oneMain";
			this.oneMain.Size = new System.Drawing.Size(1194, 85);
			this.oneMain.TabIndex = 1;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.Controls.Add(this.pnlClassifyS);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1184, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl4);
			this.oneGridItem2.Controls.Add(this.bpPanelControl3);
			this.oneGridItem2.Controls.Add(this.bpPanelControl5);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1184, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// pnlClassifyS
			// 
			this.pnlClassifyS.Controls.Add(this.ctxClassL);
			this.pnlClassifyS.Controls.Add(this.labelExt9);
			this.pnlClassifyS.Location = new System.Drawing.Point(2, 1);
			this.pnlClassifyS.Name = "pnlClassifyS";
			this.pnlClassifyS.Size = new System.Drawing.Size(280, 23);
			this.pnlClassifyS.TabIndex = 35;
			this.pnlClassifyS.Text = "bpPanelControl11";
			// 
			// labelExt9
			// 
			this.labelExt9.Location = new System.Drawing.Point(17, 4);
			this.labelExt9.Name = "labelExt9";
			this.labelExt9.Size = new System.Drawing.Size(75, 16);
			this.labelExt9.TabIndex = 2;
			this.labelExt9.Text = "대분류";
			this.labelExt9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.ctxClassM);
			this.bpPanelControl1.Controls.Add(this.labelExt1);
			this.bpPanelControl1.Location = new System.Drawing.Point(284, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(280, 23);
			this.bpPanelControl1.TabIndex = 36;
			this.bpPanelControl1.Text = "bpPanelControl11";
			// 
			// labelExt1
			// 
			this.labelExt1.Location = new System.Drawing.Point(17, 4);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(75, 16);
			this.labelExt1.TabIndex = 2;
			this.labelExt1.Text = "중분류";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.ctxClassS);
			this.bpPanelControl2.Controls.Add(this.labelExt2);
			this.bpPanelControl2.Location = new System.Drawing.Point(566, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(280, 23);
			this.bpPanelControl2.TabIndex = 37;
			this.bpPanelControl2.Text = "bpPanelControl11";
			// 
			// labelExt2
			// 
			this.labelExt2.Location = new System.Drawing.Point(17, 4);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Size = new System.Drawing.Size(75, 16);
			this.labelExt2.TabIndex = 2;
			this.labelExt2.Text = "소분류";
			this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.cbo키워드);
			this.bpPanelControl4.Controls.Add(this.tbxKeyword);
			this.bpPanelControl4.Location = new System.Drawing.Point(566, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(280, 23);
			this.bpPanelControl4.TabIndex = 26;
			this.bpPanelControl4.Text = "bpPanelControl5";
			// 
			// cbo키워드
			// 
			this.cbo키워드.AutoDropDown = true;
			this.cbo키워드.DisplayMember = "NAME";
			this.cbo키워드.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo키워드.FormattingEnabled = true;
			this.cbo키워드.ItemHeight = 12;
			this.cbo키워드.Location = new System.Drawing.Point(12, 1);
			this.cbo키워드.Name = "cbo키워드";
			this.cbo키워드.Size = new System.Drawing.Size(80, 20);
			this.cbo키워드.TabIndex = 13;
			this.cbo키워드.Tag = "";
			this.cbo키워드.ValueMember = "CODE";
			// 
			// tbxKeyword
			// 
			this.tbxKeyword.BackColor = System.Drawing.Color.White;
			this.tbxKeyword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbxKeyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbxKeyword.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.tbxKeyword.Location = new System.Drawing.Point(94, 1);
			this.tbxKeyword.Name = "tbxKeyword";
			this.tbxKeyword.Size = new System.Drawing.Size(185, 21);
			this.tbxKeyword.TabIndex = 4;
			this.tbxKeyword.Tag = "";
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.labelExt3);
			this.bpPanelControl3.Controls.Add(this.tbxItemName);
			this.bpPanelControl3.Location = new System.Drawing.Point(284, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(280, 23);
			this.bpPanelControl3.TabIndex = 25;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// labelExt3
			// 
			this.labelExt3.Location = new System.Drawing.Point(17, 4);
			this.labelExt3.Name = "labelExt3";
			this.labelExt3.Size = new System.Drawing.Size(75, 16);
			this.labelExt3.TabIndex = 5;
			this.labelExt3.Text = "재고명";
			this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbxItemName
			// 
			this.tbxItemName.BackColor = System.Drawing.Color.White;
			this.tbxItemName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbxItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbxItemName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.tbxItemName.Location = new System.Drawing.Point(94, 1);
			this.tbxItemName.Name = "tbxItemName";
			this.tbxItemName.Size = new System.Drawing.Size(185, 21);
			this.tbxItemName.TabIndex = 4;
			this.tbxItemName.Tag = "";
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.labelExt7);
			this.bpPanelControl5.Controls.Add(this.tbxItemCode);
			this.bpPanelControl5.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(280, 23);
			this.bpPanelControl5.TabIndex = 24;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// labelExt7
			// 
			this.labelExt7.Location = new System.Drawing.Point(17, 4);
			this.labelExt7.Name = "labelExt7";
			this.labelExt7.Size = new System.Drawing.Size(75, 16);
			this.labelExt7.TabIndex = 5;
			this.labelExt7.Text = "재고코드";
			this.labelExt7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbxItemCode
			// 
			this.tbxItemCode.BackColor = System.Drawing.Color.White;
			this.tbxItemCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbxItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbxItemCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.tbxItemCode.Location = new System.Drawing.Point(94, 1);
			this.tbxItemCode.Name = "tbxItemCode";
			this.tbxItemCode.Size = new System.Drawing.Size(185, 21);
			this.tbxItemCode.TabIndex = 4;
			this.tbxItemCode.Tag = "";
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.pnl검색옵션);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(1184, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// pnl검색옵션
			// 
			this.pnl검색옵션.Controls.Add(this.chkCond3);
			this.pnl검색옵션.Controls.Add(this.chkCond2);
			this.pnl검색옵션.Controls.Add(this.chkCond1);
			this.pnl검색옵션.Controls.Add(this.labelExt4);
			this.pnl검색옵션.Location = new System.Drawing.Point(2, 1);
			this.pnl검색옵션.Name = "pnl검색옵션";
			this.pnl검색옵션.Size = new System.Drawing.Size(1178, 23);
			this.pnl검색옵션.TabIndex = 25;
			this.pnl검색옵션.TabStop = false;
			this.pnl검색옵션.Text = "bpPanelControl1";
			// 
			// chkCond2
			// 
			this.chkCond2.Location = new System.Drawing.Point(164, 4);
			this.chkCond2.Name = "chkCond2";
			this.chkCond2.Size = new System.Drawing.Size(71, 16);
			this.chkCond2.TabIndex = 6;
			this.chkCond2.Text = "조건2";
			this.chkCond2.TextDD = null;
			this.chkCond2.UseVisualStyleBackColor = true;
			// 
			// chkCond1
			// 
			this.chkCond1.Location = new System.Drawing.Point(94, 4);
			this.chkCond1.Name = "chkCond1";
			this.chkCond1.Size = new System.Drawing.Size(63, 16);
			this.chkCond1.TabIndex = 5;
			this.chkCond1.Text = "조건1";
			this.chkCond1.TextDD = null;
			this.chkCond1.UseVisualStyleBackColor = true;
			// 
			// labelExt4
			// 
			this.labelExt4.Location = new System.Drawing.Point(17, 4);
			this.labelExt4.Name = "labelExt4";
			this.labelExt4.Size = new System.Drawing.Size(75, 16);
			this.labelExt4.TabIndex = 1;
			this.labelExt4.Text = "검색옵션";
			this.labelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// chkCond3
			// 
			this.chkCond3.Location = new System.Drawing.Point(234, 4);
			this.chkCond3.Name = "chkCond3";
			this.chkCond3.Size = new System.Drawing.Size(71, 16);
			this.chkCond3.TabIndex = 7;
			this.chkCond3.Text = "조건3";
			this.chkCond3.TextDD = null;
			this.chkCond3.UseVisualStyleBackColor = true;
			// 
			// ctxClassL
			// 
			this.ctxClassL.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.ctxClassL.Location = new System.Drawing.Point(94, 1);
			this.ctxClassL.Name = "ctxClassL";
			this.ctxClassL.Size = new System.Drawing.Size(186, 21);
			this.ctxClassL.TabIndex = 4;
			this.ctxClassL.TabStop = false;
			this.ctxClassL.Tag = "";
			// 
			// ctxClassM
			// 
			this.ctxClassM.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.ctxClassM.Location = new System.Drawing.Point(94, 1);
			this.ctxClassM.Name = "ctxClassM";
			this.ctxClassM.Size = new System.Drawing.Size(186, 21);
			this.ctxClassM.TabIndex = 5;
			this.ctxClassM.TabStop = false;
			this.ctxClassM.Tag = "";
			// 
			// ctxClassS
			// 
			this.ctxClassS.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.ctxClassS.Location = new System.Drawing.Point(94, 1);
			this.ctxClassS.Name = "ctxClassS";
			this.ctxClassS.Size = new System.Drawing.Size(186, 21);
			this.ctxClassS.TabIndex = 6;
			this.ctxClassS.TabStop = false;
			this.ctxClassS.Tag = "";
			// 
			// P_CZ_MM_BAD_STOCK_RPT
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "P_CZ_MM_BAD_STOCK_RPT";
			this.Size = new System.Drawing.Size(1200, 600);
			this.TitleText = "P_CZ_MM_BAD_STOCK_RPT";
			this.mDataArea.ResumeLayout(false);
			this.tlayMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.pnlClassifyS.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl4.PerformLayout();
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl5.PerformLayout();
			this.oneGridItem3.ResumeLayout(false);
			this.pnl검색옵션.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlayMain;
		private Dass.FlexGrid.FlexGrid grdList;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneMain;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl pnlClassifyS;
		private Duzon.Common.Controls.LabelExt labelExt9;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.LabelExt labelExt2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
		private Duzon.Common.Controls.DropDownComboBox cbo키워드;
		private Duzon.Common.Controls.TextBoxExt tbxKeyword;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.Controls.LabelExt labelExt3;
		private Duzon.Common.Controls.TextBoxExt tbxItemName;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
		private Duzon.Common.Controls.LabelExt labelExt7;
		private Duzon.Common.Controls.TextBoxExt tbxItemCode;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
		private Duzon.Common.BpControls.BpPanelControl pnl검색옵션;
		private Duzon.Common.Controls.CheckBoxExt chkCond3;
		private Duzon.Common.Controls.CheckBoxExt chkCond2;
		private Duzon.Common.Controls.CheckBoxExt chkCond1;
		private Duzon.Common.Controls.LabelExt labelExt4;
		private Duzon.Common.BpControls.BpCodeTextBox ctxClassL;
		private Duzon.Common.BpControls.BpCodeTextBox ctxClassS;
		private Duzon.Common.BpControls.BpCodeTextBox ctxClassM;
	}
}
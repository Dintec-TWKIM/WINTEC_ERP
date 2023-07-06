namespace cz
{
	partial class P_CZ_PU_GR_RPT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PU_GR_RPT));
			this.oneS = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.tabMain = new Duzon.Common.Controls.TabControlExt();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.oneGridItem4 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx회사코드 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.labelExt9 = new Duzon.Common.Controls.LabelExt();
			this.pnl작성일자 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo일자 = new Duzon.Common.Controls.DropDownComboBox();
			this.dtp일자 = new Duzon.Common.Controls.PeriodPicker();
			this.pnl파일번호 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo파일 = new Duzon.Common.Controls.DropDownComboBox();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.cbo차수 = new Duzon.Common.Controls.DropDownComboBox();
			this.txt파일번호 = new Duzon.Common.Controls.TextBoxExt();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.grdFileHead = new Dass.FlexGrid.FlexGrid(this.components);
			this.grdFileLine = new Dass.FlexGrid.FlexGrid(this.components);
			this.grdItemLine = new Dass.FlexGrid.FlexGrid(this.components);
			this.mDataArea.SuspendLayout();
			this.tabMain.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.pnl작성일자.SuspendLayout();
			this.pnl파일번호.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdFileHead)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdFileLine)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdItemLine)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tabMain);
			this.mDataArea.Controls.Add(this.oneS);
			this.mDataArea.Size = new System.Drawing.Size(1300, 610);
			// 
			// oneS
			// 
			this.oneS.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneS.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3,
            this.oneGridItem4});
			this.oneS.Location = new System.Drawing.Point(0, 0);
			this.oneS.Name = "oneS";
			this.oneS.Size = new System.Drawing.Size(1300, 157);
			this.oneS.TabIndex = 1;
			// 
			// tabMain
			// 
			this.tabMain.Controls.Add(this.tabPage1);
			this.tabMain.Controls.Add(this.tabPage2);
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.ItemSize = new System.Drawing.Size(120, 20);
			this.tabMain.Location = new System.Drawing.Point(0, 157);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(1300, 453);
			this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabMain.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.splitContainer1);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1292, 425);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "입고현황(파일)";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.grdItemLine);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1292, 425);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "입고현황(품목)";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.pnl작성일자);
			this.oneGridItem1.Controls.Add(this.pnl파일번호);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1290, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1290, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(1290, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// oneGridItem4
			// 
			this.oneGridItem4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem4.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem4.Location = new System.Drawing.Point(0, 69);
			this.oneGridItem4.Name = "oneGridItem4";
			this.oneGridItem4.Size = new System.Drawing.Size(1290, 23);
			this.oneGridItem4.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem4.TabIndex = 3;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.ctx회사코드);
			this.bpPanelControl2.Controls.Add(this.labelExt9);
			this.bpPanelControl2.Location = new System.Drawing.Point(646, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(320, 23);
			this.bpPanelControl2.TabIndex = 17;
			this.bpPanelControl2.Text = "bpPanelControl3";
			// 
			// ctx회사코드
			// 
			this.ctx회사코드.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_COMPANY_SUB;
			this.ctx회사코드.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx회사코드.Location = new System.Drawing.Point(94, 1);
			this.ctx회사코드.Name = "ctx회사코드";
			this.ctx회사코드.Size = new System.Drawing.Size(226, 21);
			this.ctx회사코드.TabIndex = 5;
			this.ctx회사코드.TabStop = false;
			this.ctx회사코드.Tag = "";
			// 
			// labelExt9
			// 
			this.labelExt9.Location = new System.Drawing.Point(17, 4);
			this.labelExt9.Name = "labelExt9";
			this.labelExt9.Size = new System.Drawing.Size(75, 16);
			this.labelExt9.TabIndex = 3;
			this.labelExt9.Text = "회사코드";
			this.labelExt9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl작성일자
			// 
			this.pnl작성일자.Controls.Add(this.cbo일자);
			this.pnl작성일자.Controls.Add(this.dtp일자);
			this.pnl작성일자.Location = new System.Drawing.Point(324, 1);
			this.pnl작성일자.Name = "pnl작성일자";
			this.pnl작성일자.Size = new System.Drawing.Size(320, 23);
			this.pnl작성일자.TabIndex = 16;
			this.pnl작성일자.Text = "bpPanelControl2";
			// 
			// cbo일자
			// 
			this.cbo일자.AutoDropDown = true;
			this.cbo일자.DisplayMember = "NAME";
			this.cbo일자.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo일자.FormattingEnabled = true;
			this.cbo일자.ItemHeight = 12;
			this.cbo일자.Location = new System.Drawing.Point(12, 1);
			this.cbo일자.Name = "cbo일자";
			this.cbo일자.Size = new System.Drawing.Size(80, 20);
			this.cbo일자.TabIndex = 11;
			this.cbo일자.Tag = "CD_PARTNER_GRP";
			this.cbo일자.ValueMember = "CODE";
			// 
			// dtp일자
			// 
			this.dtp일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp일자.IsNecessaryCondition = true;
			this.dtp일자.Location = new System.Drawing.Point(94, 1);
			this.dtp일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp일자.Name = "dtp일자";
			this.dtp일자.Size = new System.Drawing.Size(185, 21);
			this.dtp일자.TabIndex = 2;
			// 
			// pnl파일번호
			// 
			this.pnl파일번호.Controls.Add(this.cbo파일);
			this.pnl파일번호.Controls.Add(this.labelExt1);
			this.pnl파일번호.Controls.Add(this.cbo차수);
			this.pnl파일번호.Controls.Add(this.txt파일번호);
			this.pnl파일번호.Location = new System.Drawing.Point(2, 1);
			this.pnl파일번호.Name = "pnl파일번호";
			this.pnl파일번호.Size = new System.Drawing.Size(320, 23);
			this.pnl파일번호.TabIndex = 15;
			this.pnl파일번호.Text = "bpPanelControl1";
			// 
			// cbo파일
			// 
			this.cbo파일.AutoDropDown = true;
			this.cbo파일.DisplayMember = "NAME";
			this.cbo파일.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo파일.FormattingEnabled = true;
			this.cbo파일.ItemHeight = 12;
			this.cbo파일.Location = new System.Drawing.Point(197, 1);
			this.cbo파일.Name = "cbo파일";
			this.cbo파일.Size = new System.Drawing.Size(60, 20);
			this.cbo파일.TabIndex = 11;
			this.cbo파일.Tag = "";
			this.cbo파일.ValueMember = "CODE";
			// 
			// labelExt1
			// 
			this.labelExt1.Location = new System.Drawing.Point(17, 4);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(75, 16);
			this.labelExt1.TabIndex = 3;
			this.labelExt1.Text = "파일번호";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo차수
			// 
			this.cbo차수.AutoDropDown = true;
			this.cbo차수.DisplayMember = "NAME";
			this.cbo차수.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo차수.FormattingEnabled = true;
			this.cbo차수.ItemHeight = 12;
			this.cbo차수.Location = new System.Drawing.Point(259, 1);
			this.cbo차수.Name = "cbo차수";
			this.cbo차수.Size = new System.Drawing.Size(60, 20);
			this.cbo차수.TabIndex = 10;
			this.cbo차수.Tag = "";
			this.cbo차수.ValueMember = "CODE";
			// 
			// txt파일번호
			// 
			this.txt파일번호.BackColor = System.Drawing.Color.White;
			this.txt파일번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt파일번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt파일번호.Location = new System.Drawing.Point(94, 1);
			this.txt파일번호.Name = "txt파일번호";
			this.txt파일번호.Size = new System.Drawing.Size(101, 21);
			this.txt파일번호.TabIndex = 2;
			this.txt파일번호.Tag = "";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.grdFileHead);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.grdFileLine);
			this.splitContainer1.Size = new System.Drawing.Size(1286, 419);
			this.splitContainer1.SplitterDistance = 192;
			this.splitContainer1.TabIndex = 0;
			// 
			// grdFileHead
			// 
			this.grdFileHead.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grdFileHead.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grdFileHead.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grdFileHead.AutoResize = false;
			this.grdFileHead.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grdFileHead.Cursor = System.Windows.Forms.Cursors.Default;
			this.grdFileHead.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdFileHead.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grdFileHead.EnabledHeaderCheck = true;
			this.grdFileHead.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grdFileHead.Location = new System.Drawing.Point(0, 0);
			this.grdFileHead.Name = "grdFileHead";
			this.grdFileHead.Rows.Count = 1;
			this.grdFileHead.Rows.DefaultSize = 20;
			this.grdFileHead.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grdFileHead.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grdFileHead.ShowSort = false;
			this.grdFileHead.Size = new System.Drawing.Size(1286, 192);
			this.grdFileHead.StyleInfo = resources.GetString("grdFileHead.StyleInfo");
			this.grdFileHead.TabIndex = 1;
			this.grdFileHead.UseGridCalculator = true;
			// 
			// grdFileLine
			// 
			this.grdFileLine.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grdFileLine.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grdFileLine.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grdFileLine.AutoResize = false;
			this.grdFileLine.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grdFileLine.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdFileLine.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grdFileLine.EnabledHeaderCheck = true;
			this.grdFileLine.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grdFileLine.Location = new System.Drawing.Point(0, 0);
			this.grdFileLine.Name = "grdFileLine";
			this.grdFileLine.Rows.Count = 1;
			this.grdFileLine.Rows.DefaultSize = 20;
			this.grdFileLine.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grdFileLine.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grdFileLine.ShowSort = false;
			this.grdFileLine.Size = new System.Drawing.Size(1286, 223);
			this.grdFileLine.StyleInfo = resources.GetString("grdFileLine.StyleInfo");
			this.grdFileLine.TabIndex = 2;
			this.grdFileLine.UseGridCalculator = true;
			// 
			// grdItemLine
			// 
			this.grdItemLine.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grdItemLine.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grdItemLine.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grdItemLine.AutoResize = false;
			this.grdItemLine.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grdItemLine.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdItemLine.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grdItemLine.EnabledHeaderCheck = true;
			this.grdItemLine.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grdItemLine.Location = new System.Drawing.Point(3, 3);
			this.grdItemLine.Name = "grdItemLine";
			this.grdItemLine.Rows.Count = 1;
			this.grdItemLine.Rows.DefaultSize = 20;
			this.grdItemLine.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grdItemLine.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grdItemLine.ShowSort = false;
			this.grdItemLine.Size = new System.Drawing.Size(1286, 419);
			this.grdItemLine.StyleInfo = resources.GetString("grdItemLine.StyleInfo");
			this.grdItemLine.TabIndex = 3;
			this.grdItemLine.UseGridCalculator = true;
			// 
			// P_CZ_PU_GR_RPT
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_PU_GR_RPT";
			this.Size = new System.Drawing.Size(1300, 650);
			this.TitleText = "P_CZ_PU_GR_RPT";
			this.mDataArea.ResumeLayout(false);
			this.tabMain.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.pnl작성일자.ResumeLayout(false);
			this.pnl파일번호.ResumeLayout(false);
			this.pnl파일번호.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdFileHead)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdFileLine)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdItemLine)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Duzon.Erpiu.Windows.OneControls.OneGrid oneS;
		private Duzon.Common.Controls.TabControlExt tabMain;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem4;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.BpControls.BpCodeTextBox ctx회사코드;
		private Duzon.Common.Controls.LabelExt labelExt9;
		private Duzon.Common.BpControls.BpPanelControl pnl작성일자;
		private Duzon.Common.Controls.DropDownComboBox cbo일자;
		private Duzon.Common.Controls.PeriodPicker dtp일자;
		private Duzon.Common.BpControls.BpPanelControl pnl파일번호;
		private Duzon.Common.Controls.DropDownComboBox cbo파일;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Duzon.Common.Controls.DropDownComboBox cbo차수;
		private Duzon.Common.Controls.TextBoxExt txt파일번호;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Dass.FlexGrid.FlexGrid grdFileHead;
		private Dass.FlexGrid.FlexGrid grdFileLine;
		private Dass.FlexGrid.FlexGrid grdItemLine;

	}
}
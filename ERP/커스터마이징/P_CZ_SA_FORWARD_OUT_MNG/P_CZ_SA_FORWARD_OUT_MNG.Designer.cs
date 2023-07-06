
namespace cz
{
	partial class P_CZ_SA_FORWARD_OUT_MNG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_FORWARD_OUT_MNG));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx납품처 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl납품처 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp출고일자 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl출고일자 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo회사 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl회사 = new Duzon.Common.Controls.LabelExt();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpg등록 = new System.Windows.Forms.TabPage();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg조회 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpg등록.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.tpg조회.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1087, 543);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1087, 543);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1081, 40);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.Controls.Add(this.bpPanelControl4);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1071, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.ctx납품처);
			this.bpPanelControl3.Controls.Add(this.lbl납품처);
			this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// ctx납품처
			// 
			this.ctx납품처.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx납품처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_TABLE_SUB;
			this.ctx납품처.Location = new System.Drawing.Point(106, 0);
			this.ctx납품처.Name = "ctx납품처";
			this.ctx납품처.Size = new System.Drawing.Size(186, 21);
			this.ctx납품처.TabIndex = 1;
			this.ctx납품처.TabStop = false;
			// 
			// lbl납품처
			// 
			this.lbl납품처.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl납품처.Location = new System.Drawing.Point(0, 0);
			this.lbl납품처.Name = "lbl납품처";
			this.lbl납품처.Size = new System.Drawing.Size(100, 23);
			this.lbl납품처.TabIndex = 0;
			this.lbl납품처.Text = "납품처";
			this.lbl납품처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.dtp출고일자);
			this.bpPanelControl2.Controls.Add(this.lbl출고일자);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// dtp출고일자
			// 
			this.dtp출고일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp출고일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp출고일자.IsNecessaryCondition = true;
			this.dtp출고일자.Location = new System.Drawing.Point(107, 0);
			this.dtp출고일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp출고일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp출고일자.Name = "dtp출고일자";
			this.dtp출고일자.Size = new System.Drawing.Size(185, 21);
			this.dtp출고일자.TabIndex = 1;
			// 
			// lbl출고일자
			// 
			this.lbl출고일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl출고일자.Location = new System.Drawing.Point(0, 0);
			this.lbl출고일자.Name = "lbl출고일자";
			this.lbl출고일자.Size = new System.Drawing.Size(100, 23);
			this.lbl출고일자.TabIndex = 0;
			this.lbl출고일자.Text = "출고일자";
			this.lbl출고일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.cbo회사);
			this.bpPanelControl4.Controls.Add(this.lbl회사);
			this.bpPanelControl4.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl4.TabIndex = 0;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// cbo회사
			// 
			this.cbo회사.AutoDropDown = true;
			this.cbo회사.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo회사.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo회사.FormattingEnabled = true;
			this.cbo회사.ItemHeight = 12;
			this.cbo회사.Location = new System.Drawing.Point(107, 0);
			this.cbo회사.Name = "cbo회사";
			this.cbo회사.Size = new System.Drawing.Size(185, 20);
			this.cbo회사.TabIndex = 1;
			// 
			// lbl회사
			// 
			this.lbl회사.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl회사.Location = new System.Drawing.Point(0, 0);
			this.lbl회사.Name = "lbl회사";
			this.lbl회사.Size = new System.Drawing.Size(100, 23);
			this.lbl회사.TabIndex = 0;
			this.lbl회사.Text = "회사";
			this.lbl회사.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tpg등록);
			this.tabControl1.Controls.Add(this.tpg조회);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(3, 49);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1081, 491);
			this.tabControl1.TabIndex = 1;
			// 
			// tpg등록
			// 
			this.tpg등록.Controls.Add(this._flex);
			this.tpg등록.Location = new System.Drawing.Point(4, 22);
			this.tpg등록.Name = "tpg등록";
			this.tpg등록.Padding = new System.Windows.Forms.Padding(3);
			this.tpg등록.Size = new System.Drawing.Size(1073, 465);
			this.tpg등록.TabIndex = 0;
			this.tpg등록.Text = "등록";
			this.tpg등록.UseVisualStyleBackColor = true;
			// 
			// _flex
			// 
			this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex.AutoResize = false;
			this._flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex.EnabledHeaderCheck = true;
			this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex.Location = new System.Drawing.Point(3, 3);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 20;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(1067, 459);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 0;
			this._flex.UseGridCalculator = true;
			// 
			// tpg조회
			// 
			this.tpg조회.Controls.Add(this.splitContainer1);
			this.tpg조회.Location = new System.Drawing.Point(4, 22);
			this.tpg조회.Name = "tpg조회";
			this.tpg조회.Padding = new System.Windows.Forms.Padding(3);
			this.tpg조회.Size = new System.Drawing.Size(1073, 443);
			this.tpg조회.TabIndex = 1;
			this.tpg조회.Text = "조회";
			this.tpg조회.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._flexH);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this._flexL);
			this.splitContainer1.Size = new System.Drawing.Size(1067, 437);
			this.splitContainer1.SplitterDistance = 254;
			this.splitContainer1.TabIndex = 0;
			// 
			// _flexH
			// 
			this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexH.AutoResize = false;
			this._flexH.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexH.EnabledHeaderCheck = true;
			this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexH.Location = new System.Drawing.Point(0, 0);
			this._flexH.Name = "_flexH";
			this._flexH.Rows.Count = 1;
			this._flexH.Rows.DefaultSize = 20;
			this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexH.ShowSort = false;
			this._flexH.Size = new System.Drawing.Size(254, 437);
			this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
			this._flexH.TabIndex = 0;
			this._flexH.UseGridCalculator = true;
			// 
			// _flexL
			// 
			this._flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexL.AutoResize = false;
			this._flexL.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexL.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexL.EnabledHeaderCheck = true;
			this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexL.Location = new System.Drawing.Point(0, 0);
			this._flexL.Name = "_flexL";
			this._flexL.Rows.Count = 1;
			this._flexL.Rows.DefaultSize = 20;
			this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexL.ShowSort = false;
			this._flexL.Size = new System.Drawing.Size(809, 437);
			this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
			this._flexL.TabIndex = 0;
			this._flexL.UseGridCalculator = true;
			// 
			// P_CZ_SA_FORWARD_OUT_MNG
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_SA_FORWARD_OUT_MNG";
			this.Size = new System.Drawing.Size(1087, 583);
			this.TitleText = "P_CZ_SA_FORWARD_OUT_MNG";
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tpg등록.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.tpg조회.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.LabelExt lbl출고일자;
		private Duzon.Common.Controls.PeriodPicker dtp출고일자;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.BpControls.BpCodeTextBox ctx납품처;
		private Duzon.Common.Controls.LabelExt lbl납품처;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpg등록;
		private System.Windows.Forms.TabPage tpg조회;
		private Dass.FlexGrid.FlexGrid _flex;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Dass.FlexGrid.FlexGrid _flexH;
		private Dass.FlexGrid.FlexGrid _flexL;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
		private Duzon.Common.Controls.DropDownComboBox cbo회사;
		private Duzon.Common.Controls.LabelExt lbl회사;
	}
}
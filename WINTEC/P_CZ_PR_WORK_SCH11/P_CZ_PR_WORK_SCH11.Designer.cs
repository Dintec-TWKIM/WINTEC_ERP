
namespace cz
{
	partial class P_CZ_PR_WORK_SCH11
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
			this.panel9 = new Duzon.Common.Controls.PanelExt();
			this.chk중분류 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk대분류 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk소분류 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk실적일 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk품목 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk공정 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk작업장 = new Duzon.Common.Controls.CheckBoxExt();
			this.m_tabWork = new System.Windows.Forms.TabControl();
			this.m_tabOp = new System.Windows.Forms.TabPage();
			this.m_pnlOp = new Duzon.Common.Controls.PanelExt();
			this._flex공정별 = new Dass.FlexGrid.FlexGrid(this.components);
			this.m_tabItem = new System.Windows.Forms.TabPage();
			this.m_pnlItem = new Duzon.Common.Controls.PanelExt();
			this._flex품목별 = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpP_Date = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp조회기간 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl조회기간 = new Duzon.Common.Controls.LabelExt();
			this.bpP_Dt_Wo = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp작업기간 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl작업기간 = new Duzon.Common.Controls.LabelExt();
			this.bpP_Plant = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl공장 = new Duzon.Common.Controls.LabelExt();
			this.cbo공장 = new Duzon.Common.Controls.DzComboBox();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpP_Wc = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc작업장 = new Duzon.Common.BpControls.BpComboBox();
			this.m_tabEmp = new System.Windows.Forms.TabPage();
			this._flex작업자별 = new Dass.FlexGrid.FlexGrid(this.components);
			this.lbl작업장 = new Duzon.Common.Controls.LabelExt();
			this.chk사원 = new Duzon.Common.Controls.CheckBoxExt();
			this.m_tabEquip = new System.Windows.Forms.TabPage();
			this._flex장비별 = new Dass.FlexGrid.FlexGrid(this.components);
			this.chk장비 = new Duzon.Common.Controls.CheckBoxExt();
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel9.SuspendLayout();
			this.m_tabWork.SuspendLayout();
			this.m_tabOp.SuspendLayout();
			this.m_pnlOp.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex공정별)).BeginInit();
			this.m_tabItem.SuspendLayout();
			this.m_pnlItem.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex품목별)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpP_Date.SuspendLayout();
			this.bpP_Dt_Wo.SuspendLayout();
			this.bpP_Plant.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpP_Wc.SuspendLayout();
			this.m_tabEmp.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex작업자별)).BeginInit();
			this.m_tabEquip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex장비별)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1090, 756);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panel9, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.m_tabWork, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1090, 756);
			this.tableLayoutPanel1.TabIndex = 92;
			// 
			// panel9
			// 
			this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel9.Controls.Add(this.chk장비);
			this.panel9.Controls.Add(this.chk사원);
			this.panel9.Controls.Add(this.chk중분류);
			this.panel9.Controls.Add(this.chk대분류);
			this.panel9.Controls.Add(this.chk소분류);
			this.panel9.Controls.Add(this.chk실적일);
			this.panel9.Controls.Add(this.chk품목);
			this.panel9.Controls.Add(this.chk공정);
			this.panel9.Controls.Add(this.chk작업장);
			this.panel9.Location = new System.Drawing.Point(3, 71);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(1084, 22);
			this.panel9.TabIndex = 90;
			// 
			// chk중분류
			// 
			this.chk중분류.AutoSize = true;
			this.chk중분류.Enabled = false;
			this.chk중분류.ForeColor = System.Drawing.Color.Brown;
			this.chk중분류.Location = new System.Drawing.Point(213, 5);
			this.chk중분류.Name = "chk중분류";
			this.chk중분류.Size = new System.Drawing.Size(60, 16);
			this.chk중분류.TabIndex = 6;
			this.chk중분류.Tag = "";
			this.chk중분류.Text = "중분류";
			this.chk중분류.TextDD = "중분류";
			// 
			// chk대분류
			// 
			this.chk대분류.AutoSize = true;
			this.chk대분류.Enabled = false;
			this.chk대분류.ForeColor = System.Drawing.Color.DarkRed;
			this.chk대분류.Location = new System.Drawing.Point(143, 5);
			this.chk대분류.Name = "chk대분류";
			this.chk대분류.Size = new System.Drawing.Size(60, 16);
			this.chk대분류.TabIndex = 5;
			this.chk대분류.Tag = "";
			this.chk대분류.Text = "대분류";
			this.chk대분류.TextDD = "대분류";
			// 
			// chk소분류
			// 
			this.chk소분류.AutoSize = true;
			this.chk소분류.Enabled = false;
			this.chk소분류.ForeColor = System.Drawing.Color.IndianRed;
			this.chk소분류.Location = new System.Drawing.Point(283, 5);
			this.chk소분류.Name = "chk소분류";
			this.chk소분류.Size = new System.Drawing.Size(60, 16);
			this.chk소분류.TabIndex = 4;
			this.chk소분류.Tag = "";
			this.chk소분류.Text = "소분류";
			this.chk소분류.TextDD = "소분류";
			// 
			// chk실적일
			// 
			this.chk실적일.AutoSize = true;
			this.chk실적일.Enabled = false;
			this.chk실적일.ForeColor = System.Drawing.Color.DarkGreen;
			this.chk실적일.Location = new System.Drawing.Point(411, 5);
			this.chk실적일.Name = "chk실적일";
			this.chk실적일.Size = new System.Drawing.Size(60, 16);
			this.chk실적일.TabIndex = 3;
			this.chk실적일.Tag = "";
			this.chk실적일.Text = "실적일";
			this.chk실적일.TextDD = "실적일";
			// 
			// chk품목
			// 
			this.chk품목.AutoSize = true;
			this.chk품목.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.chk품목.Location = new System.Drawing.Point(353, 5);
			this.chk품목.Name = "chk품목";
			this.chk품목.Size = new System.Drawing.Size(48, 16);
			this.chk품목.TabIndex = 2;
			this.chk품목.Tag = "";
			this.chk품목.Text = "품목";
			this.chk품목.TextDD = "품목";
			// 
			// chk공정
			// 
			this.chk공정.AutoSize = true;
			this.chk공정.Checked = true;
			this.chk공정.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk공정.ForeColor = System.Drawing.Color.DarkMagenta;
			this.chk공정.Location = new System.Drawing.Point(85, 5);
			this.chk공정.Name = "chk공정";
			this.chk공정.Size = new System.Drawing.Size(48, 16);
			this.chk공정.TabIndex = 1;
			this.chk공정.Tag = "";
			this.chk공정.Text = "공정";
			this.chk공정.TextDD = "공정";
			// 
			// chk작업장
			// 
			this.chk작업장.AutoSize = true;
			this.chk작업장.Checked = true;
			this.chk작업장.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk작업장.ForeColor = System.Drawing.Color.DarkBlue;
			this.chk작업장.Location = new System.Drawing.Point(15, 5);
			this.chk작업장.Name = "chk작업장";
			this.chk작업장.Size = new System.Drawing.Size(60, 16);
			this.chk작업장.TabIndex = 0;
			this.chk작업장.Tag = "";
			this.chk작업장.Text = "작업장";
			this.chk작업장.TextDD = "작업장";
			// 
			// m_tabWork
			// 
			this.m_tabWork.Controls.Add(this.m_tabOp);
			this.m_tabWork.Controls.Add(this.m_tabItem);
			this.m_tabWork.Controls.Add(this.m_tabEmp);
			this.m_tabWork.Controls.Add(this.m_tabEquip);
			this.m_tabWork.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_tabWork.ItemSize = new System.Drawing.Size(111, 20);
			this.m_tabWork.Location = new System.Drawing.Point(3, 99);
			this.m_tabWork.Name = "m_tabWork";
			this.m_tabWork.SelectedIndex = 0;
			this.m_tabWork.Size = new System.Drawing.Size(1084, 654);
			this.m_tabWork.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.m_tabWork.TabIndex = 0;
			this.m_tabWork.TabStop = false;
			this.m_tabWork.Tag = "1";
			// 
			// m_tabOp
			// 
			this.m_tabOp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
			this.m_tabOp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_tabOp.Controls.Add(this.m_pnlOp);
			this.m_tabOp.ImageIndex = 0;
			this.m_tabOp.Location = new System.Drawing.Point(4, 24);
			this.m_tabOp.Name = "m_tabOp";
			this.m_tabOp.Size = new System.Drawing.Size(1076, 626);
			this.m_tabOp.TabIndex = 0;
			this.m_tabOp.Tag = "공정별";
			this.m_tabOp.Text = "공정별";
			this.m_tabOp.UseVisualStyleBackColor = true;
			// 
			// m_pnlOp
			// 
			this.m_pnlOp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_pnlOp.BackColor = System.Drawing.Color.White;
			this.m_pnlOp.Controls.Add(this._flex공정별);
			this.m_pnlOp.Location = new System.Drawing.Point(0, 0);
			this.m_pnlOp.Name = "m_pnlOp";
			this.m_pnlOp.Size = new System.Drawing.Size(1072, 622);
			this.m_pnlOp.TabIndex = 0;
			// 
			// _flex공정별
			// 
			this._flex공정별.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex공정별.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex공정별.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex공정별.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex공정별.AutoResize = false;
			this._flex공정별.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex공정별.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex공정별.EnabledHeaderCheck = true;
			this._flex공정별.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex공정별.Location = new System.Drawing.Point(0, 0);
			this._flex공정별.Name = "_flex공정별";
			this._flex공정별.Rows.Count = 1;
			this._flex공정별.Rows.DefaultSize = 20;
			this._flex공정별.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex공정별.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex공정별.ShowSort = false;
			this._flex공정별.Size = new System.Drawing.Size(1072, 622);
			this._flex공정별.TabIndex = 15;
			this._flex공정별.UseGridCalculator = true;
			// 
			// m_tabItem
			// 
			this.m_tabItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
			this.m_tabItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_tabItem.Controls.Add(this.m_pnlItem);
			this.m_tabItem.ImageIndex = 1;
			this.m_tabItem.Location = new System.Drawing.Point(4, 24);
			this.m_tabItem.Name = "m_tabItem";
			this.m_tabItem.Size = new System.Drawing.Size(1076, 626);
			this.m_tabItem.TabIndex = 2;
			this.m_tabItem.Tag = "품목별";
			this.m_tabItem.Text = "품목별";
			this.m_tabItem.UseVisualStyleBackColor = true;
			this.m_tabItem.Visible = false;
			// 
			// m_pnlItem
			// 
			this.m_pnlItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_pnlItem.BackColor = System.Drawing.Color.White;
			this.m_pnlItem.Controls.Add(this._flex품목별);
			this.m_pnlItem.Location = new System.Drawing.Point(0, 0);
			this.m_pnlItem.Name = "m_pnlItem";
			this.m_pnlItem.Size = new System.Drawing.Size(1072, 622);
			this.m_pnlItem.TabIndex = 0;
			// 
			// _flex품목별
			// 
			this._flex품목별.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex품목별.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex품목별.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex품목별.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex품목별.AutoResize = false;
			this._flex품목별.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex품목별.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex품목별.EnabledHeaderCheck = true;
			this._flex품목별.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex품목별.Location = new System.Drawing.Point(0, 0);
			this._flex품목별.Name = "_flex품목별";
			this._flex품목별.Rows.Count = 1;
			this._flex품목별.Rows.DefaultSize = 20;
			this._flex품목별.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex품목별.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex품목별.ShowSort = false;
			this._flex품목별.Size = new System.Drawing.Size(1072, 622);
			this._flex품목별.TabIndex = 15;
			this._flex품목별.UseGridCalculator = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1084, 62);
			this.oneGrid1.TabIndex = 91;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpP_Date);
			this.oneGridItem1.Controls.Add(this.bpP_Dt_Wo);
			this.oneGridItem1.Controls.Add(this.bpP_Plant);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1074, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpP_Date
			// 
			this.bpP_Date.Controls.Add(this.dtp조회기간);
			this.bpP_Date.Controls.Add(this.lbl조회기간);
			this.bpP_Date.Location = new System.Drawing.Point(540, 1);
			this.bpP_Date.Name = "bpP_Date";
			this.bpP_Date.Size = new System.Drawing.Size(267, 23);
			this.bpP_Date.TabIndex = 5;
			this.bpP_Date.Text = "bpPanelControl4";
			// 
			// dtp조회기간
			// 
			this.dtp조회기간.CalendarPeriodType = Duzon.Common.Controls.PeriodType.YearMonth;
			this.dtp조회기간.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp조회기간.IsNecessaryCondition = true;
			this.dtp조회기간.Location = new System.Drawing.Point(81, 1);
			this.dtp조회기간.Mask = "####/##";
			this.dtp조회기간.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp조회기간.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp조회기간.Name = "dtp조회기간";
			this.dtp조회기간.Size = new System.Drawing.Size(185, 21);
			this.dtp조회기간.TabIndex = 10;
			// 
			// lbl조회기간
			// 
			this.lbl조회기간.Location = new System.Drawing.Point(0, 2);
			this.lbl조회기간.Name = "lbl조회기간";
			this.lbl조회기간.Size = new System.Drawing.Size(80, 16);
			this.lbl조회기간.TabIndex = 0;
			this.lbl조회기간.Tag = "조회기간";
			this.lbl조회기간.Text = "조회기간";
			this.lbl조회기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpP_Dt_Wo
			// 
			this.bpP_Dt_Wo.Controls.Add(this.dtp작업기간);
			this.bpP_Dt_Wo.Controls.Add(this.lbl작업기간);
			this.bpP_Dt_Wo.Location = new System.Drawing.Point(271, 1);
			this.bpP_Dt_Wo.Name = "bpP_Dt_Wo";
			this.bpP_Dt_Wo.Size = new System.Drawing.Size(267, 23);
			this.bpP_Dt_Wo.TabIndex = 4;
			this.bpP_Dt_Wo.Text = "bpPanelControl5";
			// 
			// dtp작업기간
			// 
			this.dtp작업기간.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp작업기간.IsNecessaryCondition = true;
			this.dtp작업기간.Location = new System.Drawing.Point(81, 1);
			this.dtp작업기간.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp작업기간.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp작업기간.Name = "dtp작업기간";
			this.dtp작업기간.Size = new System.Drawing.Size(185, 21);
			this.dtp작업기간.TabIndex = 10;
			// 
			// lbl작업기간
			// 
			this.lbl작업기간.Location = new System.Drawing.Point(0, 2);
			this.lbl작업기간.Name = "lbl작업기간";
			this.lbl작업기간.Size = new System.Drawing.Size(80, 16);
			this.lbl작업기간.TabIndex = 0;
			this.lbl작업기간.Tag = "작업기간";
			this.lbl작업기간.Text = "작업기간";
			this.lbl작업기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpP_Plant
			// 
			this.bpP_Plant.Controls.Add(this.lbl공장);
			this.bpP_Plant.Controls.Add(this.cbo공장);
			this.bpP_Plant.Location = new System.Drawing.Point(2, 1);
			this.bpP_Plant.Name = "bpP_Plant";
			this.bpP_Plant.Size = new System.Drawing.Size(267, 23);
			this.bpP_Plant.TabIndex = 3;
			this.bpP_Plant.Text = "bpPanelControl6";
			// 
			// lbl공장
			// 
			this.lbl공장.Location = new System.Drawing.Point(0, 2);
			this.lbl공장.Name = "lbl공장";
			this.lbl공장.Size = new System.Drawing.Size(80, 16);
			this.lbl공장.TabIndex = 0;
			this.lbl공장.Tag = "공장";
			this.lbl공장.Text = "공장";
			this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo공장
			// 
			this.cbo공장.AutoDropDown = true;
			this.cbo공장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo공장.Location = new System.Drawing.Point(81, 1);
			this.cbo공장.Name = "cbo공장";
			this.cbo공장.Size = new System.Drawing.Size(186, 20);
			this.cbo공장.TabIndex = 1;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpP_Wc);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1074, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpP_Wc
			// 
			this.bpP_Wc.Controls.Add(this.lbl작업장);
			this.bpP_Wc.Controls.Add(this.bpc작업장);
			this.bpP_Wc.Location = new System.Drawing.Point(2, 1);
			this.bpP_Wc.Name = "bpP_Wc";
			this.bpP_Wc.Size = new System.Drawing.Size(267, 23);
			this.bpP_Wc.TabIndex = 6;
			this.bpP_Wc.Text = "bpPanelControl3";
			// 
			// bpc작업장
			// 
			this.bpc작업장.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc작업장.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_WC_SUB1;
			this.bpc작업장.Location = new System.Drawing.Point(81, 0);
			this.bpc작업장.Name = "bpc작업장";
			this.bpc작업장.Size = new System.Drawing.Size(186, 21);
			this.bpc작업장.TabIndex = 87;
			this.bpc작업장.TabStop = false;
			// 
			// m_tabEmp
			// 
			this.m_tabEmp.Controls.Add(this._flex작업자별);
			this.m_tabEmp.Location = new System.Drawing.Point(4, 24);
			this.m_tabEmp.Name = "m_tabEmp";
			this.m_tabEmp.Size = new System.Drawing.Size(1076, 626);
			this.m_tabEmp.TabIndex = 3;
			this.m_tabEmp.Text = "작업자별";
			this.m_tabEmp.UseVisualStyleBackColor = true;
			// 
			// _flex작업자별
			// 
			this._flex작업자별.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex작업자별.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex작업자별.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex작업자별.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex작업자별.AutoResize = false;
			this._flex작업자별.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex작업자별.Cursor = System.Windows.Forms.Cursors.Default;
			this._flex작업자별.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex작업자별.EnabledHeaderCheck = true;
			this._flex작업자별.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex작업자별.Location = new System.Drawing.Point(2, 2);
			this._flex작업자별.Name = "_flex작업자별";
			this._flex작업자별.Rows.Count = 1;
			this._flex작업자별.Rows.DefaultSize = 20;
			this._flex작업자별.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex작업자별.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex작업자별.ShowSort = false;
			this._flex작업자별.Size = new System.Drawing.Size(1072, 622);
			this._flex작업자별.TabIndex = 16;
			this._flex작업자별.UseGridCalculator = true;
			// 
			// lbl작업장
			// 
			this.lbl작업장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl작업장.Location = new System.Drawing.Point(0, 0);
			this.lbl작업장.Name = "lbl작업장";
			this.lbl작업장.Size = new System.Drawing.Size(80, 23);
			this.lbl작업장.TabIndex = 88;
			this.lbl작업장.Tag = "공장";
			this.lbl작업장.Text = "작업장";
			this.lbl작업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// chk사원
			// 
			this.chk사원.AutoSize = true;
			this.chk사원.Enabled = false;
			this.chk사원.ForeColor = System.Drawing.Color.Olive;
			this.chk사원.Location = new System.Drawing.Point(481, 5);
			this.chk사원.Name = "chk사원";
			this.chk사원.Size = new System.Drawing.Size(48, 16);
			this.chk사원.TabIndex = 7;
			this.chk사원.Tag = "";
			this.chk사원.Text = "사원";
			this.chk사원.TextDD = "실적일";
			// 
			// m_tabEquip
			// 
			this.m_tabEquip.Controls.Add(this._flex장비별);
			this.m_tabEquip.Location = new System.Drawing.Point(4, 24);
			this.m_tabEquip.Name = "m_tabEquip";
			this.m_tabEquip.Size = new System.Drawing.Size(1076, 626);
			this.m_tabEquip.TabIndex = 4;
			this.m_tabEquip.Text = "장비별";
			this.m_tabEquip.UseVisualStyleBackColor = true;
			// 
			// _flex장비별
			// 
			this._flex장비별.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex장비별.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex장비별.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex장비별.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex장비별.AutoResize = false;
			this._flex장비별.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex장비별.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex장비별.EnabledHeaderCheck = true;
			this._flex장비별.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex장비별.Location = new System.Drawing.Point(2, 2);
			this._flex장비별.Name = "_flex장비별";
			this._flex장비별.Rows.Count = 1;
			this._flex장비별.Rows.DefaultSize = 20;
			this._flex장비별.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex장비별.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex장비별.ShowSort = false;
			this._flex장비별.Size = new System.Drawing.Size(1072, 622);
			this._flex장비별.TabIndex = 17;
			this._flex장비별.UseGridCalculator = true;
			// 
			// chk장비
			// 
			this.chk장비.AutoSize = true;
			this.chk장비.Enabled = false;
			this.chk장비.ForeColor = System.Drawing.Color.DarkSlateBlue;
			this.chk장비.Location = new System.Drawing.Point(539, 5);
			this.chk장비.Name = "chk장비";
			this.chk장비.Size = new System.Drawing.Size(48, 16);
			this.chk장비.TabIndex = 8;
			this.chk장비.Tag = "";
			this.chk장비.Text = "장비";
			this.chk장비.TextDD = "실적일";
			// 
			// P_CZ_PR_WORK_SCH11
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Name = "P_CZ_PR_WORK_SCH11";
			this.Size = new System.Drawing.Size(1090, 796);
			this.mDataArea.ResumeLayout(false);
			this.mDataArea.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel9.ResumeLayout(false);
			this.panel9.PerformLayout();
			this.m_tabWork.ResumeLayout(false);
			this.m_tabOp.ResumeLayout(false);
			this.m_pnlOp.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex공정별)).EndInit();
			this.m_tabItem.ResumeLayout(false);
			this.m_pnlItem.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex품목별)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpP_Date.ResumeLayout(false);
			this.bpP_Dt_Wo.ResumeLayout(false);
			this.bpP_Plant.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpP_Wc.ResumeLayout(false);
			this.m_tabEmp.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex작업자별)).EndInit();
			this.m_tabEquip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex장비별)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Common.Controls.DzComboBox cbo공장;
		private Duzon.Common.Controls.LabelExt lbl조회기간;
		private Duzon.Common.Controls.LabelExt lbl공장;
		private Duzon.Common.Controls.LabelExt lbl작업기간;
		private Duzon.Common.Controls.PanelExt panel9;
		private Duzon.Common.Controls.CheckBoxExt chk실적일;
		private Duzon.Common.Controls.CheckBoxExt chk품목;
		private Duzon.Common.Controls.CheckBoxExt chk공정;
		private Duzon.Common.Controls.CheckBoxExt chk작업장;
		private System.Windows.Forms.TabControl m_tabWork;
		private System.Windows.Forms.TabPage m_tabOp;
		private Duzon.Common.Controls.PanelExt m_pnlOp;
		private Dass.FlexGrid.FlexGrid _flex공정별;
		private System.Windows.Forms.TabPage m_tabItem;
		private Duzon.Common.Controls.PanelExt m_pnlItem;
		private Dass.FlexGrid.FlexGrid _flex품목별;
		//private System.Windows.Forms.ImageList imageList1;
		private Duzon.Common.Controls.CheckBoxExt chk소분류;
		private Duzon.Common.BpControls.BpComboBox bpc작업장;
		private Duzon.Common.Controls.CheckBoxExt chk중분류;
		private Duzon.Common.Controls.CheckBoxExt chk대분류;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpP_Date;
		private Duzon.Common.BpControls.BpPanelControl bpP_Dt_Wo;
		private Duzon.Common.BpControls.BpPanelControl bpP_Plant;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl bpP_Wc;
		private Duzon.Common.Controls.PeriodPicker dtp조회기간;
		private Duzon.Common.Controls.PeriodPicker dtp작업기간;
		private System.Windows.Forms.TabPage m_tabEmp;
		private Dass.FlexGrid.FlexGrid _flex작업자별;
		private Duzon.Common.Controls.LabelExt lbl작업장;
		private Duzon.Common.Controls.CheckBoxExt chk사원;
		private Duzon.Common.Controls.CheckBoxExt chk장비;
		private System.Windows.Forms.TabPage m_tabEquip;
		private Dass.FlexGrid.FlexGrid _flex장비별;
	}
}
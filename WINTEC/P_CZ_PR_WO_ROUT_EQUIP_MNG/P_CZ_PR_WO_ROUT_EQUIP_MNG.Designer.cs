
namespace cz
{
	partial class P_CZ_PR_WO_ROUT_EQUIP_MNG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_WO_ROUT_EQUIP_MNG));
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt작업지시번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl작업지시번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp작업기간 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl작업기간 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl공장 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx설비 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl설비 = new Duzon.Common.Controls.LabelExt();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tabControlExt1 = new Duzon.Common.Controls.TabControlExt();
			this.tpg설비할당 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.imagePanel4 = new Duzon.Common.Controls.ImagePanel(this.components);
			this._flex공정 = new Dass.FlexGrid.FlexGrid(this.components);
			this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.btn할당취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn할당 = new Duzon.Common.Controls.RoundedButton(this.components);
			this._flex설비 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg설비별작업현황 = new System.Windows.Forms.TabPage();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.imagePanel2 = new Duzon.Common.Controls.ImagePanel(this.components);
			this._flex설비1 = new Dass.FlexGrid.FlexGrid(this.components);
			this.imagePanel3 = new Duzon.Common.Controls.ImagePanel(this.components);
			this._flex공정1 = new Dass.FlexGrid.FlexGrid(this.components);
			this.chk마감제외 = new Duzon.Common.Controls.CheckBoxExt();
			this.mDataArea.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControlExt1.SuspendLayout();
			this.tpg설비할당.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.imagePanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex공정)).BeginInit();
			this.imagePanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex설비)).BeginInit();
			this.tpg설비별작업현황.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.imagePanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex설비1)).BeginInit();
			this.imagePanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex공정1)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1129, 657);
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1123, 62);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1113, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.txt작업지시번호);
			this.bpPanelControl3.Controls.Add(this.lbl작업지시번호);
			this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// txt작업지시번호
			// 
			this.txt작업지시번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt작업지시번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt작업지시번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt작업지시번호.Location = new System.Drawing.Point(106, 0);
			this.txt작업지시번호.Name = "txt작업지시번호";
			this.txt작업지시번호.Size = new System.Drawing.Size(186, 21);
			this.txt작업지시번호.TabIndex = 1;
			// 
			// lbl작업지시번호
			// 
			this.lbl작업지시번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl작업지시번호.Location = new System.Drawing.Point(0, 0);
			this.lbl작업지시번호.Name = "lbl작업지시번호";
			this.lbl작업지시번호.Size = new System.Drawing.Size(100, 23);
			this.lbl작업지시번호.TabIndex = 0;
			this.lbl작업지시번호.Text = "작업지시번호";
			this.lbl작업지시번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.dtp작업기간);
			this.bpPanelControl2.Controls.Add(this.lbl작업기간);
			this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// dtp작업기간
			// 
			this.dtp작업기간.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp작업기간.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp작업기간.IsNecessaryCondition = true;
			this.dtp작업기간.Location = new System.Drawing.Point(107, 0);
			this.dtp작업기간.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp작업기간.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp작업기간.Name = "dtp작업기간";
			this.dtp작업기간.Size = new System.Drawing.Size(185, 21);
			this.dtp작업기간.TabIndex = 1;
			// 
			// lbl작업기간
			// 
			this.lbl작업기간.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl작업기간.Location = new System.Drawing.Point(0, 0);
			this.lbl작업기간.Name = "lbl작업기간";
			this.lbl작업기간.Size = new System.Drawing.Size(100, 23);
			this.lbl작업기간.TabIndex = 0;
			this.lbl작업기간.Text = "작업기간";
			this.lbl작업기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.cbo공장);
			this.bpPanelControl1.Controls.Add(this.lbl공장);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// cbo공장
			// 
			this.cbo공장.AutoDropDown = true;
			this.cbo공장.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo공장.FormattingEnabled = true;
			this.cbo공장.ItemHeight = 12;
			this.cbo공장.Location = new System.Drawing.Point(106, 0);
			this.cbo공장.Name = "cbo공장";
			this.cbo공장.Size = new System.Drawing.Size(186, 20);
			this.cbo공장.TabIndex = 1;
			// 
			// lbl공장
			// 
			this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl공장.Location = new System.Drawing.Point(0, 0);
			this.lbl공장.Name = "lbl공장";
			this.lbl공장.Size = new System.Drawing.Size(100, 23);
			this.lbl공장.TabIndex = 0;
			this.lbl공장.Text = "공장";
			this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.chk마감제외);
			this.oneGridItem2.Controls.Add(this.bpPanelControl4);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1113, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.ctx설비);
			this.bpPanelControl4.Controls.Add(this.lbl설비);
			this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl4.TabIndex = 3;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// ctx설비
			// 
			this.ctx설비.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx설비.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_TABLE_SUB;
			this.ctx설비.Location = new System.Drawing.Point(106, 0);
			this.ctx설비.Name = "ctx설비";
			this.ctx설비.Size = new System.Drawing.Size(186, 21);
			this.ctx설비.TabIndex = 1;
			this.ctx설비.TabStop = false;
			this.ctx설비.Text = "bpCodeTextBox1";
			// 
			// lbl설비
			// 
			this.lbl설비.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl설비.Location = new System.Drawing.Point(0, 0);
			this.lbl설비.Name = "lbl설비";
			this.lbl설비.Size = new System.Drawing.Size(100, 23);
			this.lbl설비.TabIndex = 0;
			this.lbl설비.Text = "설비";
			this.lbl설비.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tabControlExt1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1129, 657);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tabControlExt1
			// 
			this.tabControlExt1.Controls.Add(this.tpg설비할당);
			this.tabControlExt1.Controls.Add(this.tpg설비별작업현황);
			this.tabControlExt1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlExt1.Location = new System.Drawing.Point(3, 71);
			this.tabControlExt1.Name = "tabControlExt1";
			this.tabControlExt1.SelectedIndex = 0;
			this.tabControlExt1.Size = new System.Drawing.Size(1123, 583);
			this.tabControlExt1.TabIndex = 1;
			// 
			// tpg설비할당
			// 
			this.tpg설비할당.Controls.Add(this.splitContainer1);
			this.tpg설비할당.Location = new System.Drawing.Point(4, 22);
			this.tpg설비할당.Name = "tpg설비할당";
			this.tpg설비할당.Padding = new System.Windows.Forms.Padding(3);
			this.tpg설비할당.Size = new System.Drawing.Size(1115, 557);
			this.tpg설비할당.TabIndex = 0;
			this.tpg설비할당.Text = "설비할당";
			this.tpg설비할당.UseVisualStyleBackColor = true;
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
			this.splitContainer1.Panel1.Controls.Add(this.imagePanel4);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.imagePanel1);
			this.splitContainer1.Size = new System.Drawing.Size(1109, 551);
			this.splitContainer1.SplitterDistance = 331;
			this.splitContainer1.TabIndex = 1;
			// 
			// imagePanel4
			// 
			this.imagePanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel4.Controls.Add(this._flex공정);
			this.imagePanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel4.LeftImage = null;
			this.imagePanel4.Location = new System.Drawing.Point(0, 0);
			this.imagePanel4.Name = "imagePanel4";
			this.imagePanel4.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel4.PatternImage = null;
			this.imagePanel4.RightImage = null;
			this.imagePanel4.Size = new System.Drawing.Size(1109, 331);
			this.imagePanel4.TabIndex = 2;
			this.imagePanel4.TitleText = "작업지시";
			// 
			// _flex공정
			// 
			this._flex공정.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex공정.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex공정.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex공정.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex공정.AutoResize = false;
			this._flex공정.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex공정.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex공정.EnabledHeaderCheck = true;
			this._flex공정.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex공정.Location = new System.Drawing.Point(3, 27);
			this._flex공정.Name = "_flex공정";
			this._flex공정.Rows.Count = 1;
			this._flex공정.Rows.DefaultSize = 20;
			this._flex공정.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex공정.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex공정.ShowSort = false;
			this._flex공정.Size = new System.Drawing.Size(1103, 301);
			this._flex공정.StyleInfo = resources.GetString("_flex공정.StyleInfo");
			this._flex공정.TabIndex = 2;
			this._flex공정.UseGridCalculator = true;
			// 
			// imagePanel1
			// 
			this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel1.Controls.Add(this.btn할당취소);
			this.imagePanel1.Controls.Add(this.btn할당);
			this.imagePanel1.Controls.Add(this._flex설비);
			this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel1.LeftImage = null;
			this.imagePanel1.Location = new System.Drawing.Point(0, 0);
			this.imagePanel1.Name = "imagePanel1";
			this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel1.PatternImage = null;
			this.imagePanel1.RightImage = null;
			this.imagePanel1.Size = new System.Drawing.Size(1109, 216);
			this.imagePanel1.TabIndex = 0;
			this.imagePanel1.TitleText = "설비";
			// 
			// btn할당취소
			// 
			this.btn할당취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn할당취소.BackColor = System.Drawing.Color.Transparent;
			this.btn할당취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn할당취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn할당취소.Location = new System.Drawing.Point(1032, 3);
			this.btn할당취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn할당취소.Name = "btn할당취소";
			this.btn할당취소.Size = new System.Drawing.Size(70, 19);
			this.btn할당취소.TabIndex = 3;
			this.btn할당취소.TabStop = false;
			this.btn할당취소.Text = "할당취소";
			this.btn할당취소.UseVisualStyleBackColor = false;
			// 
			// btn할당
			// 
			this.btn할당.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn할당.BackColor = System.Drawing.Color.Transparent;
			this.btn할당.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn할당.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn할당.Location = new System.Drawing.Point(956, 3);
			this.btn할당.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn할당.Name = "btn할당";
			this.btn할당.Size = new System.Drawing.Size(70, 19);
			this.btn할당.TabIndex = 2;
			this.btn할당.TabStop = false;
			this.btn할당.Text = "할당";
			this.btn할당.UseVisualStyleBackColor = false;
			// 
			// _flex설비
			// 
			this._flex설비.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex설비.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex설비.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex설비.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex설비.AutoResize = false;
			this._flex설비.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex설비.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex설비.EnabledHeaderCheck = true;
			this._flex설비.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex설비.Location = new System.Drawing.Point(3, 27);
			this._flex설비.Name = "_flex설비";
			this._flex설비.Rows.Count = 1;
			this._flex설비.Rows.DefaultSize = 20;
			this._flex설비.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex설비.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex설비.ShowSort = false;
			this._flex설비.Size = new System.Drawing.Size(1103, 186);
			this._flex설비.StyleInfo = resources.GetString("_flex설비.StyleInfo");
			this._flex설비.TabIndex = 1;
			this._flex설비.UseGridCalculator = true;
			// 
			// tpg설비별작업현황
			// 
			this.tpg설비별작업현황.Controls.Add(this.splitContainer2);
			this.tpg설비별작업현황.Location = new System.Drawing.Point(4, 22);
			this.tpg설비별작업현황.Name = "tpg설비별작업현황";
			this.tpg설비별작업현황.Padding = new System.Windows.Forms.Padding(3);
			this.tpg설비별작업현황.Size = new System.Drawing.Size(1115, 557);
			this.tpg설비별작업현황.TabIndex = 1;
			this.tpg설비별작업현황.Text = "설비별작업현황";
			this.tpg설비별작업현황.UseVisualStyleBackColor = true;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(3, 3);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.imagePanel2);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.imagePanel3);
			this.splitContainer2.Size = new System.Drawing.Size(1109, 551);
			this.splitContainer2.SplitterDistance = 369;
			this.splitContainer2.TabIndex = 0;
			// 
			// imagePanel2
			// 
			this.imagePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel2.Controls.Add(this._flex설비1);
			this.imagePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel2.LeftImage = null;
			this.imagePanel2.Location = new System.Drawing.Point(0, 0);
			this.imagePanel2.Name = "imagePanel2";
			this.imagePanel2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel2.PatternImage = null;
			this.imagePanel2.RightImage = null;
			this.imagePanel2.Size = new System.Drawing.Size(369, 551);
			this.imagePanel2.TabIndex = 1;
			this.imagePanel2.TitleText = "설비";
			// 
			// _flex설비1
			// 
			this._flex설비1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex설비1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex설비1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex설비1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex설비1.AutoResize = false;
			this._flex설비1.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex설비1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex설비1.EnabledHeaderCheck = true;
			this._flex설비1.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex설비1.Location = new System.Drawing.Point(3, 28);
			this._flex설비1.Name = "_flex설비1";
			this._flex설비1.Rows.Count = 1;
			this._flex설비1.Rows.DefaultSize = 20;
			this._flex설비1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex설비1.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex설비1.ShowSort = false;
			this._flex설비1.Size = new System.Drawing.Size(364, 520);
			this._flex설비1.StyleInfo = resources.GetString("_flex설비1.StyleInfo");
			this._flex설비1.TabIndex = 1;
			this._flex설비1.UseGridCalculator = true;
			// 
			// imagePanel3
			// 
			this.imagePanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel3.Controls.Add(this._flex공정1);
			this.imagePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel3.LeftImage = null;
			this.imagePanel3.Location = new System.Drawing.Point(0, 0);
			this.imagePanel3.Name = "imagePanel3";
			this.imagePanel3.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel3.PatternImage = null;
			this.imagePanel3.RightImage = null;
			this.imagePanel3.Size = new System.Drawing.Size(736, 551);
			this.imagePanel3.TabIndex = 1;
			this.imagePanel3.TitleText = "작업지시";
			// 
			// _flex공정1
			// 
			this._flex공정1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex공정1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex공정1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex공정1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex공정1.AutoResize = false;
			this._flex공정1.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex공정1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex공정1.EnabledHeaderCheck = true;
			this._flex공정1.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex공정1.Location = new System.Drawing.Point(3, 28);
			this._flex공정1.Name = "_flex공정1";
			this._flex공정1.Rows.Count = 1;
			this._flex공정1.Rows.DefaultSize = 20;
			this._flex공정1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex공정1.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex공정1.ShowSort = false;
			this._flex공정1.Size = new System.Drawing.Size(730, 520);
			this._flex공정1.StyleInfo = resources.GetString("_flex공정1.StyleInfo");
			this._flex공정1.TabIndex = 1;
			this._flex공정1.UseGridCalculator = true;
			// 
			// chk마감제외
			// 
			this.chk마감제외.AutoSize = true;
			this.chk마감제외.Checked = true;
			this.chk마감제외.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk마감제외.Location = new System.Drawing.Point(296, 1);
			this.chk마감제외.Name = "chk마감제외";
			this.chk마감제외.Size = new System.Drawing.Size(104, 24);
			this.chk마감제외.TabIndex = 4;
			this.chk마감제외.Text = "마감제외";
			this.chk마감제외.TextDD = null;
			this.chk마감제외.UseVisualStyleBackColor = true;
			// 
			// P_CZ_PR_WO_ROUT_EQUIP_MNG
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_PR_WO_ROUT_EQUIP_MNG";
			this.Size = new System.Drawing.Size(1129, 697);
			this.TitleText = "작업공정별설비할당";
			this.mDataArea.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.oneGridItem2.PerformLayout();
			this.bpPanelControl4.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tabControlExt1.ResumeLayout(false);
			this.tpg설비할당.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.imagePanel4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex공정)).EndInit();
			this.imagePanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex설비)).EndInit();
			this.tpg설비별작업현황.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.imagePanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex설비1)).EndInit();
			this.imagePanel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex공정1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.LabelExt lbl공장;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.Controls.TextBoxExt txt작업지시번호;
		private Duzon.Common.Controls.LabelExt lbl작업지시번호;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.PeriodPicker dtp작업기간;
		private Duzon.Common.Controls.LabelExt lbl작업기간;
		private Duzon.Common.Controls.DropDownComboBox cbo공장;
		private Duzon.Common.Controls.ImagePanel imagePanel1;
		private Dass.FlexGrid.FlexGrid _flex설비;
		private Duzon.Common.Controls.RoundedButton btn할당취소;
		private Duzon.Common.Controls.RoundedButton btn할당;
		private Duzon.Common.Controls.TabControlExt tabControlExt1;
		private System.Windows.Forms.TabPage tpg설비할당;
		private System.Windows.Forms.TabPage tpg설비별작업현황;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Duzon.Common.Controls.ImagePanel imagePanel2;
		private Dass.FlexGrid.FlexGrid _flex설비1;
		private Duzon.Common.Controls.ImagePanel imagePanel3;
		private Dass.FlexGrid.FlexGrid _flex공정1;
		private Duzon.Common.Controls.ImagePanel imagePanel4;
		private Dass.FlexGrid.FlexGrid _flex공정;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
		private Duzon.Common.BpControls.BpCodeTextBox ctx설비;
		private Duzon.Common.Controls.LabelExt lbl설비;
		private Duzon.Common.Controls.CheckBoxExt chk마감제외;
	}
}
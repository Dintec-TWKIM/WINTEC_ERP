namespace cz
{
    partial class P_CZ_SA_LOG_CHARGE_RPT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_LOG_CHARGE_RPT));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt협조전번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl협조전번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt수주번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl수주번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo기간유형 = new Duzon.Common.Controls.DropDownComboBox();
			this.dtp일자 = new Duzon.Common.Controls.PeriodPicker();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.chk포장금액존재 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk이윤보다큰건 = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo포장형태 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl포장형태 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo청구여부 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl청구가능여부 = new Duzon.Common.Controls.LabelExt();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpg수주번호별 = new System.Windows.Forms.TabPage();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg협조전별 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.tpg수주번호 = new System.Windows.Forms.TabPage();
			this._flex수주번호 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg목공포장 = new System.Windows.Forms.TabPage();
			this._flex목공포장 = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn포장비계산 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpg수주번호별.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.tpg협조전별.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
			this.tabControl2.SuspendLayout();
			this.tpg수주번호.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex수주번호)).BeginInit();
			this.tpg목공포장.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex목공포장)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1338, 814);
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
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1338, 814);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1332, 62);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl5);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1322, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.txt협조전번호);
			this.bpPanelControl5.Controls.Add(this.lbl협조전번호);
			this.bpPanelControl5.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl5.TabIndex = 4;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// txt협조전번호
			// 
			this.txt협조전번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt협조전번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt협조전번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt협조전번호.Location = new System.Drawing.Point(107, 0);
			this.txt협조전번호.Name = "txt협조전번호";
			this.txt협조전번호.Size = new System.Drawing.Size(185, 21);
			this.txt협조전번호.TabIndex = 28;
			// 
			// lbl협조전번호
			// 
			this.lbl협조전번호.BackColor = System.Drawing.Color.Transparent;
			this.lbl협조전번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl협조전번호.Location = new System.Drawing.Point(0, 0);
			this.lbl협조전번호.Name = "lbl협조전번호";
			this.lbl협조전번호.Size = new System.Drawing.Size(100, 23);
			this.lbl협조전번호.TabIndex = 4;
			this.lbl협조전번호.Text = "협조전번호";
			this.lbl협조전번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.txt수주번호);
			this.bpPanelControl1.Controls.Add(this.lbl수주번호);
			this.bpPanelControl1.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 3;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// txt수주번호
			// 
			this.txt수주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt수주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt수주번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt수주번호.Location = new System.Drawing.Point(107, 0);
			this.txt수주번호.Name = "txt수주번호";
			this.txt수주번호.Size = new System.Drawing.Size(185, 21);
			this.txt수주번호.TabIndex = 28;
			// 
			// lbl수주번호
			// 
			this.lbl수주번호.BackColor = System.Drawing.Color.Transparent;
			this.lbl수주번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl수주번호.Location = new System.Drawing.Point(0, 0);
			this.lbl수주번호.Name = "lbl수주번호";
			this.lbl수주번호.Size = new System.Drawing.Size(100, 23);
			this.lbl수주번호.TabIndex = 4;
			this.lbl수주번호.Text = "수주번호";
			this.lbl수주번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.cbo기간유형);
			this.bpPanelControl2.Controls.Add(this.dtp일자);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 2;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// cbo기간유형
			// 
			this.cbo기간유형.AutoDropDown = true;
			this.cbo기간유형.Dock = System.Windows.Forms.DockStyle.Left;
			this.cbo기간유형.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo기간유형.FormattingEnabled = true;
			this.cbo기간유형.ItemHeight = 12;
			this.cbo기간유형.Location = new System.Drawing.Point(0, 0);
			this.cbo기간유형.Name = "cbo기간유형";
			this.cbo기간유형.Size = new System.Drawing.Size(100, 20);
			this.cbo기간유형.TabIndex = 2;
			// 
			// dtp일자
			// 
			this.dtp일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp일자.IsNecessaryCondition = true;
			this.dtp일자.Location = new System.Drawing.Point(107, 0);
			this.dtp일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp일자.Name = "dtp일자";
			this.dtp일자.Size = new System.Drawing.Size(185, 21);
			this.dtp일자.TabIndex = 1;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.flowLayoutPanel1);
			this.oneGridItem2.Controls.Add(this.bpPanelControl4);
			this.oneGridItem2.Controls.Add(this.bpPanelControl3);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1322, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.chk포장금액존재);
			this.flowLayoutPanel1.Controls.Add(this.chk이윤보다큰건);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(590, 1);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(215, 23);
			this.flowLayoutPanel1.TabIndex = 7;
			// 
			// chk포장금액존재
			// 
			this.chk포장금액존재.AutoSize = true;
			this.chk포장금액존재.Checked = true;
			this.chk포장금액존재.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk포장금액존재.Location = new System.Drawing.Point(3, 3);
			this.chk포장금액존재.Name = "chk포장금액존재";
			this.chk포장금액존재.Size = new System.Drawing.Size(96, 16);
			this.chk포장금액존재.TabIndex = 6;
			this.chk포장금액존재.Text = "포장금액존재";
			this.chk포장금액존재.TextDD = null;
			this.chk포장금액존재.UseVisualStyleBackColor = true;
			// 
			// chk이윤보다큰건
			// 
			this.chk이윤보다큰건.AutoSize = true;
			this.chk이윤보다큰건.Location = new System.Drawing.Point(105, 3);
			this.chk이윤보다큰건.Name = "chk이윤보다큰건";
			this.chk이윤보다큰건.Size = new System.Drawing.Size(96, 16);
			this.chk이윤보다큰건.TabIndex = 7;
			this.chk이윤보다큰건.Text = "이윤보다큰건";
			this.chk이윤보다큰건.TextDD = null;
			this.chk이윤보다큰건.UseVisualStyleBackColor = true;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.cbo포장형태);
			this.bpPanelControl4.Controls.Add(this.lbl포장형태);
			this.bpPanelControl4.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl4.TabIndex = 5;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// cbo포장형태
			// 
			this.cbo포장형태.AutoDropDown = true;
			this.cbo포장형태.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo포장형태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo포장형태.FormattingEnabled = true;
			this.cbo포장형태.ItemHeight = 12;
			this.cbo포장형태.Location = new System.Drawing.Point(107, 0);
			this.cbo포장형태.Name = "cbo포장형태";
			this.cbo포장형태.Size = new System.Drawing.Size(185, 20);
			this.cbo포장형태.TabIndex = 5;
			// 
			// lbl포장형태
			// 
			this.lbl포장형태.BackColor = System.Drawing.Color.Transparent;
			this.lbl포장형태.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl포장형태.Location = new System.Drawing.Point(0, 0);
			this.lbl포장형태.Name = "lbl포장형태";
			this.lbl포장형태.Size = new System.Drawing.Size(100, 23);
			this.lbl포장형태.TabIndex = 4;
			this.lbl포장형태.Text = "포장형태";
			this.lbl포장형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.cbo청구여부);
			this.bpPanelControl3.Controls.Add(this.lbl청구가능여부);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 4;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// cbo청구여부
			// 
			this.cbo청구여부.AutoDropDown = true;
			this.cbo청구여부.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo청구여부.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo청구여부.FormattingEnabled = true;
			this.cbo청구여부.ItemHeight = 12;
			this.cbo청구여부.Location = new System.Drawing.Point(107, 0);
			this.cbo청구여부.Name = "cbo청구여부";
			this.cbo청구여부.Size = new System.Drawing.Size(185, 20);
			this.cbo청구여부.TabIndex = 5;
			// 
			// lbl청구가능여부
			// 
			this.lbl청구가능여부.BackColor = System.Drawing.Color.Transparent;
			this.lbl청구가능여부.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl청구가능여부.Location = new System.Drawing.Point(0, 0);
			this.lbl청구가능여부.Name = "lbl청구가능여부";
			this.lbl청구가능여부.Size = new System.Drawing.Size(100, 23);
			this.lbl청구가능여부.TabIndex = 4;
			this.lbl청구가능여부.Text = "청구가능여부";
			this.lbl청구가능여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tpg수주번호별);
			this.tabControl1.Controls.Add(this.tpg협조전별);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(3, 71);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1332, 740);
			this.tabControl1.TabIndex = 1;
			// 
			// tpg수주번호별
			// 
			this.tpg수주번호별.Controls.Add(this._flex);
			this.tpg수주번호별.Location = new System.Drawing.Point(4, 22);
			this.tpg수주번호별.Name = "tpg수주번호별";
			this.tpg수주번호별.Padding = new System.Windows.Forms.Padding(3);
			this.tpg수주번호별.Size = new System.Drawing.Size(1324, 714);
			this.tpg수주번호별.TabIndex = 0;
			this.tpg수주번호별.Text = "수주번호별";
			this.tpg수주번호별.UseVisualStyleBackColor = true;
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
			this._flex.Size = new System.Drawing.Size(1318, 708);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 0;
			this._flex.UseGridCalculator = true;
			// 
			// tpg협조전별
			// 
			this.tpg협조전별.Controls.Add(this.splitContainer1);
			this.tpg협조전별.Location = new System.Drawing.Point(4, 22);
			this.tpg협조전별.Name = "tpg협조전별";
			this.tpg협조전별.Padding = new System.Windows.Forms.Padding(3);
			this.tpg협조전별.Size = new System.Drawing.Size(1324, 714);
			this.tpg협조전별.TabIndex = 1;
			this.tpg협조전별.Text = "협조전별";
			this.tpg협조전별.UseVisualStyleBackColor = true;
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
			this.splitContainer1.Panel1.Controls.Add(this._flexH);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
			this.splitContainer1.Size = new System.Drawing.Size(1318, 708);
			this.splitContainer1.SplitterDistance = 352;
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
			this._flexH.Size = new System.Drawing.Size(1318, 352);
			this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
			this._flexH.TabIndex = 0;
			this._flexH.UseGridCalculator = true;
			// 
			// tabControl2
			// 
			this.tabControl2.Controls.Add(this.tpg수주번호);
			this.tabControl2.Controls.Add(this.tpg목공포장);
			this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl2.Location = new System.Drawing.Point(0, 0);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(1318, 352);
			this.tabControl2.TabIndex = 1;
			// 
			// tpg수주번호
			// 
			this.tpg수주번호.Controls.Add(this._flex수주번호);
			this.tpg수주번호.Location = new System.Drawing.Point(4, 22);
			this.tpg수주번호.Name = "tpg수주번호";
			this.tpg수주번호.Padding = new System.Windows.Forms.Padding(3);
			this.tpg수주번호.Size = new System.Drawing.Size(1310, 326);
			this.tpg수주번호.TabIndex = 0;
			this.tpg수주번호.Text = "수주번호";
			this.tpg수주번호.UseVisualStyleBackColor = true;
			// 
			// _flex수주번호
			// 
			this._flex수주번호.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex수주번호.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex수주번호.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex수주번호.AutoResize = false;
			this._flex수주번호.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex수주번호.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex수주번호.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex수주번호.EnabledHeaderCheck = true;
			this._flex수주번호.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex수주번호.Location = new System.Drawing.Point(3, 3);
			this._flex수주번호.Name = "_flex수주번호";
			this._flex수주번호.Rows.Count = 1;
			this._flex수주번호.Rows.DefaultSize = 20;
			this._flex수주번호.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex수주번호.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex수주번호.ShowSort = false;
			this._flex수주번호.Size = new System.Drawing.Size(1304, 320);
			this._flex수주번호.StyleInfo = resources.GetString("_flex수주번호.StyleInfo");
			this._flex수주번호.TabIndex = 0;
			this._flex수주번호.UseGridCalculator = true;
			// 
			// tpg목공포장
			// 
			this.tpg목공포장.Controls.Add(this._flex목공포장);
			this.tpg목공포장.Location = new System.Drawing.Point(4, 22);
			this.tpg목공포장.Name = "tpg목공포장";
			this.tpg목공포장.Padding = new System.Windows.Forms.Padding(3);
			this.tpg목공포장.Size = new System.Drawing.Size(1310, 326);
			this.tpg목공포장.TabIndex = 1;
			this.tpg목공포장.Text = "목공포장";
			this.tpg목공포장.UseVisualStyleBackColor = true;
			// 
			// _flex목공포장
			// 
			this._flex목공포장.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex목공포장.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex목공포장.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex목공포장.AutoResize = false;
			this._flex목공포장.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex목공포장.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex목공포장.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex목공포장.EnabledHeaderCheck = true;
			this._flex목공포장.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex목공포장.Location = new System.Drawing.Point(3, 3);
			this._flex목공포장.Name = "_flex목공포장";
			this._flex목공포장.Rows.Count = 1;
			this._flex목공포장.Rows.DefaultSize = 20;
			this._flex목공포장.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex목공포장.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex목공포장.ShowSort = false;
			this._flex목공포장.Size = new System.Drawing.Size(1304, 320);
			this._flex목공포장.StyleInfo = resources.GetString("_flex목공포장.StyleInfo");
			this._flex목공포장.TabIndex = 0;
			this._flex목공포장.UseGridCalculator = true;
			// 
			// btn포장비계산
			// 
			this.btn포장비계산.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn포장비계산.BackColor = System.Drawing.Color.Transparent;
			this.btn포장비계산.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn포장비계산.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn포장비계산.Location = new System.Drawing.Point(1236, 10);
			this.btn포장비계산.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn포장비계산.Name = "btn포장비계산";
			this.btn포장비계산.Size = new System.Drawing.Size(99, 19);
			this.btn포장비계산.TabIndex = 3;
			this.btn포장비계산.TabStop = false;
			this.btn포장비계산.Text = "포장비계산";
			this.btn포장비계산.UseVisualStyleBackColor = false;
			// 
			// P_CZ_SA_LOG_CHARGE_RPT
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.btn포장비계산);
			this.Name = "P_CZ_SA_LOG_CHARGE_RPT";
			this.Size = new System.Drawing.Size(1338, 854);
			this.TitleText = "P_CZ_SA_LOG_CHARGE_RPT";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btn포장비계산, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl5.PerformLayout();
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tpg수주번호별.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.tpg협조전별.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
			this.tabControl2.ResumeLayout(false);
			this.tpg수주번호.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex수주번호)).EndInit();
			this.tpg목공포장.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex목공포장)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.PeriodPicker dtp일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.TextBoxExt txt수주번호;
        private Duzon.Common.Controls.LabelExt lbl수주번호;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.DropDownComboBox cbo청구여부;
        private Duzon.Common.Controls.LabelExt lbl청구가능여부;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.DropDownComboBox cbo포장형태;
        private Duzon.Common.Controls.LabelExt lbl포장형태;
        private Duzon.Common.Controls.CheckBoxExt chk포장금액존재;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.CheckBoxExt chk이윤보다큰건;
        private Duzon.Common.Controls.RoundedButton btn포장비계산;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpg수주번호별;
        private Dass.FlexGrid.FlexGrid _flex;
        private System.Windows.Forms.TabPage tpg협조전별;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flex수주번호;
        private Duzon.Common.Controls.DropDownComboBox cbo기간유형;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tpg수주번호;
        private System.Windows.Forms.TabPage tpg목공포장;
        private Dass.FlexGrid.FlexGrid _flex목공포장;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.Controls.TextBoxExt txt협조전번호;
        private Duzon.Common.Controls.LabelExt lbl협조전번호;
    }
}
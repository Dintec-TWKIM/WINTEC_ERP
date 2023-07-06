namespace cz
{
    partial class P_CZ_SA_TRCREDITSCH
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_TRCREDITSCH));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_DataArea = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpg계산서번호 = new System.Windows.Forms.TabPage();
            this._flex계산서번호 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tpg수주번호 = new System.Windows.Forms.TabPage();
            this._flex수주번호 = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx수금처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl수금처 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl매출처 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo조회기간 = new Duzon.Common.Controls.DropDownComboBox();
            this.dtp조회기간 = new Duzon.Common.Controls.PeriodPicker();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt계산서번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl계산서번호 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl마감담당자 = new Duzon.Common.Controls.LabelExt();
            this.ctx마감담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.dtp조회기준일 = new Duzon.Common.Controls.DatePicker();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.chk미수금0표시 = new Duzon.Common.Controls.CheckBoxExt();
            this.bpc매출처 = new Duzon.Common.BpControls.BpComboBox();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.m_DataArea.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpg계산서번호.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex계산서번호)).BeginInit();
            this.tpg수주번호.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex수주번호)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.bpPanelControl7.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp조회기준일)).BeginInit();
            this.oneGridItem3.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(1090, 756);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.m_DataArea, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1090, 756);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // m_DataArea
            // 
            this.m_DataArea.Controls.Add(this.tabControl1);
            this.m_DataArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DataArea.Location = new System.Drawing.Point(3, 94);
            this.m_DataArea.Name = "m_DataArea";
            this.m_DataArea.Size = new System.Drawing.Size(1084, 659);
            this.m_DataArea.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpg계산서번호);
            this.tabControl1.Controls.Add(this.tpg수주번호);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1084, 659);
            this.tabControl1.TabIndex = 0;
            // 
            // tpg계산서번호
            // 
            this.tpg계산서번호.Controls.Add(this._flex계산서번호);
            this.tpg계산서번호.Location = new System.Drawing.Point(4, 22);
            this.tpg계산서번호.Name = "tpg계산서번호";
            this.tpg계산서번호.Padding = new System.Windows.Forms.Padding(3);
            this.tpg계산서번호.Size = new System.Drawing.Size(1076, 633);
            this.tpg계산서번호.TabIndex = 0;
            this.tpg계산서번호.Text = "계산서번호";
            this.tpg계산서번호.UseVisualStyleBackColor = true;
            // 
            // _flex계산서번호
            // 
            this._flex계산서번호.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex계산서번호.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex계산서번호.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex계산서번호.AutoResize = false;
            this._flex계산서번호.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex계산서번호.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex계산서번호.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex계산서번호.EnabledHeaderCheck = true;
            this._flex계산서번호.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex계산서번호.Location = new System.Drawing.Point(3, 3);
            this._flex계산서번호.Name = "_flex계산서번호";
            this._flex계산서번호.Rows.Count = 1;
            this._flex계산서번호.Rows.DefaultSize = 20;
            this._flex계산서번호.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex계산서번호.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex계산서번호.ShowSort = false;
            this._flex계산서번호.Size = new System.Drawing.Size(1070, 627);
            this._flex계산서번호.StyleInfo = resources.GetString("_flex계산서번호.StyleInfo");
            this._flex계산서번호.TabIndex = 0;
            this._flex계산서번호.UseGridCalculator = true;
            // 
            // tpg수주번호
            // 
            this.tpg수주번호.Controls.Add(this._flex수주번호);
            this.tpg수주번호.Location = new System.Drawing.Point(4, 22);
            this.tpg수주번호.Name = "tpg수주번호";
            this.tpg수주번호.Padding = new System.Windows.Forms.Padding(3);
            this.tpg수주번호.Size = new System.Drawing.Size(1076, 633);
            this.tpg수주번호.TabIndex = 1;
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
            this._flex수주번호.Size = new System.Drawing.Size(1070, 627);
            this._flex수주번호.StyleInfo = resources.GetString("_flex수주번호.StyleInfo");
            this._flex수주번호.TabIndex = 0;
            this._flex수주번호.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(1084, 85);
            this.oneGrid1.TabIndex = 5;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl6);
            this.oneGridItem1.Controls.Add(this.bpPanelControl5);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(1074, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.ctx수금처);
            this.bpPanelControl6.Controls.Add(this.lbl수금처);
            this.bpPanelControl6.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl6.TabIndex = 2;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // ctx수금처
            // 
            this.ctx수금처.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx수금처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx수금처.ItemBackColor = System.Drawing.Color.Empty;
            this.ctx수금처.Location = new System.Drawing.Point(106, 0);
            this.ctx수금처.Name = "ctx수금처";
            this.ctx수금처.Size = new System.Drawing.Size(186, 21);
            this.ctx수금처.TabIndex = 130;
            this.ctx수금처.TabStop = false;
            // 
            // lbl수금처
            // 
            this.lbl수금처.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl수금처.Location = new System.Drawing.Point(0, 0);
            this.lbl수금처.Name = "lbl수금처";
            this.lbl수금처.Size = new System.Drawing.Size(100, 23);
            this.lbl수금처.TabIndex = 87;
            this.lbl수금처.Tag = "";
            this.lbl수금처.Text = "수금처";
            this.lbl수금처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.bpc매출처);
            this.bpPanelControl5.Controls.Add(this.lbl매출처);
            this.bpPanelControl5.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl5.TabIndex = 1;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // lbl매출처
            // 
            this.lbl매출처.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl매출처.Location = new System.Drawing.Point(0, 0);
            this.lbl매출처.Name = "lbl매출처";
            this.lbl매출처.Size = new System.Drawing.Size(100, 23);
            this.lbl매출처.TabIndex = 86;
            this.lbl매출처.Tag = "";
            this.lbl매출처.Text = "매출처";
            this.lbl매출처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.cbo조회기간);
            this.bpPanelControl1.Controls.Add(this.dtp조회기간);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // cbo조회기간
            // 
            this.cbo조회기간.AutoDropDown = true;
            this.cbo조회기간.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbo조회기간.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo조회기간.FormattingEnabled = true;
            this.cbo조회기간.ItemHeight = 12;
            this.cbo조회기간.Location = new System.Drawing.Point(0, 0);
            this.cbo조회기간.Name = "cbo조회기간";
            this.cbo조회기간.Size = new System.Drawing.Size(100, 20);
            this.cbo조회기간.TabIndex = 86;
            // 
            // dtp조회기간
            // 
            this.dtp조회기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp조회기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp조회기간.IsNecessaryCondition = true;
            this.dtp조회기간.Location = new System.Drawing.Point(107, 0);
            this.dtp조회기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp조회기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp조회기간.Name = "dtp조회기간";
            this.dtp조회기간.Size = new System.Drawing.Size(185, 21);
            this.dtp조회기간.TabIndex = 85;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.Controls.Add(this.bpPanelControl7);
            this.oneGridItem2.Controls.Add(this.bpPanelControl2);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(1074, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.txt계산서번호);
            this.bpPanelControl4.Controls.Add(this.lbl계산서번호);
            this.bpPanelControl4.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl4.TabIndex = 1;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // txt계산서번호
            // 
            this.txt계산서번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt계산서번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt계산서번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt계산서번호.Location = new System.Drawing.Point(106, 0);
            this.txt계산서번호.Name = "txt계산서번호";
            this.txt계산서번호.Size = new System.Drawing.Size(186, 21);
            this.txt계산서번호.TabIndex = 1;
            // 
            // lbl계산서번호
            // 
            this.lbl계산서번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl계산서번호.Location = new System.Drawing.Point(0, 0);
            this.lbl계산서번호.Name = "lbl계산서번호";
            this.lbl계산서번호.Size = new System.Drawing.Size(100, 23);
            this.lbl계산서번호.TabIndex = 0;
            this.lbl계산서번호.Text = "계산서번호";
            this.lbl계산서번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl7
            // 
            this.bpPanelControl7.Controls.Add(this.lbl마감담당자);
            this.bpPanelControl7.Controls.Add(this.ctx마감담당자);
            this.bpPanelControl7.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl7.Name = "bpPanelControl7";
            this.bpPanelControl7.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl7.TabIndex = 2;
            this.bpPanelControl7.Text = "bpPanelControl7";
            // 
            // lbl마감담당자
            // 
            this.lbl마감담당자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl마감담당자.Location = new System.Drawing.Point(0, 0);
            this.lbl마감담당자.Name = "lbl마감담당자";
            this.lbl마감담당자.Size = new System.Drawing.Size(100, 23);
            this.lbl마감담당자.TabIndex = 0;
            this.lbl마감담당자.Text = "마감담당자";
            this.lbl마감담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx마감담당자
            // 
            this.ctx마감담당자.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx마감담당자.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctx마감담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.ctx마감담당자.ItemBackColor = System.Drawing.Color.Empty;
            this.ctx마감담당자.Location = new System.Drawing.Point(106, 0);
            this.ctx마감담당자.Name = "ctx마감담당자";
            this.ctx마감담당자.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.ctx마감담당자.Size = new System.Drawing.Size(186, 21);
            this.ctx마감담당자.TabIndex = 128;
            this.ctx마감담당자.TabStop = false;
            this.ctx마감담당자.Tag = "NO_EMP;NM_KOR";
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.labelExt1);
            this.bpPanelControl2.Controls.Add(this.dtp조회기준일);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 0;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // labelExt1
            // 
            this.labelExt1.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelExt1.Location = new System.Drawing.Point(0, 0);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Size = new System.Drawing.Size(100, 23);
            this.labelExt1.TabIndex = 85;
            this.labelExt1.Tag = "";
            this.labelExt1.Text = "조회기준일";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtp조회기준일
            // 
            this.dtp조회기준일.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp조회기준일.Location = new System.Drawing.Point(107, 0);
            this.dtp조회기준일.Mask = "####/##/##";
            this.dtp조회기준일.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp조회기준일.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp조회기준일.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp조회기준일.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp조회기준일.Name = "dtp조회기준일";
            this.dtp조회기준일.Size = new System.Drawing.Size(85, 21);
            this.dtp조회기준일.TabIndex = 109;
            this.dtp조회기준일.Tag = "DT_STN";
            this.dtp조회기준일.Value = new System.DateTime(((long)(0)));
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl3);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(1074, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 2;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.chk미수금0표시);
            this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 0;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // chk미수금0표시
            // 
            this.chk미수금0표시.Dock = System.Windows.Forms.DockStyle.Right;
            this.chk미수금0표시.Location = new System.Drawing.Point(107, 0);
            this.chk미수금0표시.Name = "chk미수금0표시";
            this.chk미수금0표시.Size = new System.Drawing.Size(185, 23);
            this.chk미수금0표시.TabIndex = 0;
            this.chk미수금0표시.Text = "미수금 0 표시";
            this.chk미수금0표시.TextDD = null;
            this.chk미수금0표시.UseVisualStyleBackColor = true;
            // 
            // bpc매출처
            // 
            this.bpc매출처.Dock = System.Windows.Forms.DockStyle.Right;
            this.bpc매출처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB1;
            this.bpc매출처.Location = new System.Drawing.Point(106, 0);
            this.bpc매출처.Name = "bpc매출처";
            this.bpc매출처.Size = new System.Drawing.Size(186, 21);
            this.bpc매출처.TabIndex = 87;
            this.bpc매출처.TabStop = false;
            this.bpc매출처.Text = "bpComboBox1";
            // 
            // P_CZ_SA_TRCREDITSCH
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_CZ_SA_TRCREDITSCH";
            this.Size = new System.Drawing.Size(1090, 796);
            this.TitleText = "미수채권상세현황";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.m_DataArea.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpg계산서번호.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex계산서번호)).EndInit();
            this.tpg수주번호.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex수주번호)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl4.PerformLayout();
            this.bpPanelControl7.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp조회기준일)).EndInit();
            this.oneGridItem3.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel m_DataArea;
        private Duzon.Common.Controls.DatePicker dtp조회기준일;
        private Duzon.Common.BpControls.BpCodeTextBox ctx마감담당자;
        private Duzon.Common.BpControls.BpCodeTextBox ctx수금처;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.PeriodPicker dtp조회기간;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.CheckBoxExt chk미수금0표시;

        #endregion
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.TextBoxExt txt계산서번호;
        private Duzon.Common.Controls.LabelExt lbl계산서번호;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpg계산서번호;
        private Dass.FlexGrid.FlexGrid _flex계산서번호;
        private System.Windows.Forms.TabPage tpg수주번호;
        private Dass.FlexGrid.FlexGrid _flex수주번호;
        private Duzon.Common.Controls.DropDownComboBox cbo조회기간;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.Controls.LabelExt lbl수금처;
        private Duzon.Common.Controls.LabelExt lbl매출처;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
        private Duzon.Common.Controls.LabelExt lbl마감담당자;
        private Duzon.Common.BpControls.BpComboBox bpc매출처;
    }
}
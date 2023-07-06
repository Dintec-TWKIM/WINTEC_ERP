namespace cz
{
    partial class P_CZ_SA_BILL_SCH_PRE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_BILL_SCH_PRE));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo거래구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl거래구분 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo수금형태 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl수금형태 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp기간 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl기간 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx영업그룹 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl영업그룹 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx수금처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl수금처 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx매출처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl매출처 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl담당자 = new Duzon.Common.Controls.LabelExt();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn닫기 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.oneGridItem3.SuspendLayout();
            this.bpPanelControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(899, 539);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(893, 86);
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
            this.oneGridItem1.Size = new System.Drawing.Size(883, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.cbo거래구분);
            this.bpPanelControl3.Controls.Add(this.lbl거래구분);
            this.bpPanelControl3.Location = new System.Drawing.Point(588, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // cbo거래구분
            // 
            this.cbo거래구분.AutoDropDown = true;
            this.cbo거래구분.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo거래구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo거래구분.FormattingEnabled = true;
            this.cbo거래구분.ItemHeight = 12;
            this.cbo거래구분.Location = new System.Drawing.Point(106, 0);
            this.cbo거래구분.Name = "cbo거래구분";
            this.cbo거래구분.Size = new System.Drawing.Size(185, 20);
            this.cbo거래구분.TabIndex = 1;
            // 
            // lbl거래구분
            // 
            this.lbl거래구분.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl거래구분.Location = new System.Drawing.Point(0, 0);
            this.lbl거래구분.Name = "lbl거래구분";
            this.lbl거래구분.Size = new System.Drawing.Size(100, 23);
            this.lbl거래구분.TabIndex = 0;
            this.lbl거래구분.Text = "거래구분";
            this.lbl거래구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.cbo수금형태);
            this.bpPanelControl2.Controls.Add(this.lbl수금형태);
            this.bpPanelControl2.Location = new System.Drawing.Point(295, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // cbo수금형태
            // 
            this.cbo수금형태.AutoDropDown = true;
            this.cbo수금형태.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo수금형태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo수금형태.FormattingEnabled = true;
            this.cbo수금형태.ItemHeight = 12;
            this.cbo수금형태.Location = new System.Drawing.Point(106, 0);
            this.cbo수금형태.Name = "cbo수금형태";
            this.cbo수금형태.Size = new System.Drawing.Size(185, 20);
            this.cbo수금형태.TabIndex = 1;
            // 
            // lbl수금형태
            // 
            this.lbl수금형태.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl수금형태.Location = new System.Drawing.Point(0, 0);
            this.lbl수금형태.Name = "lbl수금형태";
            this.lbl수금형태.Size = new System.Drawing.Size(100, 23);
            this.lbl수금형태.TabIndex = 0;
            this.lbl수금형태.Text = "수금형태";
            this.lbl수금형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp기간);
            this.bpPanelControl1.Controls.Add(this.lbl기간);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // dtp기간
            // 
            this.dtp기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp기간.IsNecessaryCondition = true;
            this.dtp기간.Location = new System.Drawing.Point(106, 0);
            this.dtp기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp기간.Name = "dtp기간";
            this.dtp기간.Size = new System.Drawing.Size(185, 21);
            this.dtp기간.TabIndex = 1;
            // 
            // lbl기간
            // 
            this.lbl기간.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl기간.Location = new System.Drawing.Point(0, 0);
            this.lbl기간.Name = "lbl기간";
            this.lbl기간.Size = new System.Drawing.Size(100, 23);
            this.lbl기간.TabIndex = 0;
            this.lbl기간.Text = "기간";
            this.lbl기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(883, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.ctx영업그룹);
            this.bpPanelControl6.Controls.Add(this.lbl영업그룹);
            this.bpPanelControl6.Location = new System.Drawing.Point(588, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl6.TabIndex = 4;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // ctx영업그룹
            // 
            this.ctx영업그룹.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx영업그룹.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.ctx영업그룹.Location = new System.Drawing.Point(106, 0);
            this.ctx영업그룹.Name = "ctx영업그룹";
            this.ctx영업그룹.Size = new System.Drawing.Size(185, 21);
            this.ctx영업그룹.TabIndex = 1;
            this.ctx영업그룹.TabStop = false;
            this.ctx영업그룹.Text = "bpCodeTextBox1";
            // 
            // lbl영업그룹
            // 
            this.lbl영업그룹.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl영업그룹.Location = new System.Drawing.Point(0, 0);
            this.lbl영업그룹.Name = "lbl영업그룹";
            this.lbl영업그룹.Size = new System.Drawing.Size(100, 23);
            this.lbl영업그룹.TabIndex = 0;
            this.lbl영업그룹.Text = "영업그룹";
            this.lbl영업그룹.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.ctx수금처);
            this.bpPanelControl5.Controls.Add(this.lbl수금처);
            this.bpPanelControl5.Location = new System.Drawing.Point(295, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl5.TabIndex = 3;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // ctx수금처
            // 
            this.ctx수금처.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx수금처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx수금처.Location = new System.Drawing.Point(106, 0);
            this.ctx수금처.Name = "ctx수금처";
            this.ctx수금처.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.TotalReadOnly;
            this.ctx수금처.Size = new System.Drawing.Size(185, 21);
            this.ctx수금처.TabIndex = 1;
            this.ctx수금처.TabStop = false;
            this.ctx수금처.Text = "bpCodeTextBox1";
            // 
            // lbl수금처
            // 
            this.lbl수금처.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl수금처.Location = new System.Drawing.Point(0, 0);
            this.lbl수금처.Name = "lbl수금처";
            this.lbl수금처.Size = new System.Drawing.Size(100, 23);
            this.lbl수금처.TabIndex = 0;
            this.lbl수금처.Text = "수금처";
            this.lbl수금처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.ctx매출처);
            this.bpPanelControl4.Controls.Add(this.lbl매출처);
            this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl4.TabIndex = 2;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // ctx매출처
            // 
            this.ctx매출처.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx매출처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx매출처.Location = new System.Drawing.Point(106, 0);
            this.ctx매출처.Name = "ctx매출처";
            this.ctx매출처.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.TotalReadOnly;
            this.ctx매출처.Size = new System.Drawing.Size(185, 21);
            this.ctx매출처.TabIndex = 1;
            this.ctx매출처.TabStop = false;
            this.ctx매출처.Text = "bpCodeTextBox1";
            // 
            // lbl매출처
            // 
            this.lbl매출처.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl매출처.Location = new System.Drawing.Point(0, 0);
            this.lbl매출처.Name = "lbl매출처";
            this.lbl매출처.Size = new System.Drawing.Size(100, 23);
            this.lbl매출처.TabIndex = 0;
            this.lbl매출처.Text = "매출처";
            this.lbl매출처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl7);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(883, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 2;
            // 
            // bpPanelControl7
            // 
            this.bpPanelControl7.Controls.Add(this.ctx담당자);
            this.bpPanelControl7.Controls.Add(this.lbl담당자);
            this.bpPanelControl7.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl7.Name = "bpPanelControl7";
            this.bpPanelControl7.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl7.TabIndex = 3;
            this.bpPanelControl7.Text = "bpPanelControl7";
            // 
            // ctx담당자
            // 
            this.ctx담당자.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.ctx담당자.Location = new System.Drawing.Point(106, 0);
            this.ctx담당자.Name = "ctx담당자";
            this.ctx담당자.Size = new System.Drawing.Size(185, 21);
            this.ctx담당자.TabIndex = 1;
            this.ctx담당자.TabStop = false;
            this.ctx담당자.Text = "bpCodeTextBox1";
            // 
            // lbl담당자
            // 
            this.lbl담당자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl담당자.Location = new System.Drawing.Point(0, 0);
            this.lbl담당자.Name = "lbl담당자";
            this.lbl담당자.Size = new System.Drawing.Size(100, 23);
            this.lbl담당자.TabIndex = 0;
            this.lbl담당자.Text = "담당자";
            this.lbl담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(3, 126);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(893, 410);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 1;
            this._flex.UseGridCalculator = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn닫기);
            this.flowLayoutPanel1.Controls.Add(this.btn확인);
            this.flowLayoutPanel1.Controls.Add(this.btn조회);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 95);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(893, 25);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btn닫기
            // 
            this.btn닫기.BackColor = System.Drawing.Color.Transparent;
            this.btn닫기.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn닫기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn닫기.Location = new System.Drawing.Point(820, 3);
            this.btn닫기.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn닫기.Name = "btn닫기";
            this.btn닫기.Size = new System.Drawing.Size(70, 19);
            this.btn닫기.TabIndex = 0;
            this.btn닫기.TabStop = false;
            this.btn닫기.Text = "닫기";
            this.btn닫기.UseVisualStyleBackColor = false;
            // 
            // btn확인
            // 
            this.btn확인.BackColor = System.Drawing.Color.Transparent;
            this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn확인.Location = new System.Drawing.Point(744, 3);
            this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn확인.Name = "btn확인";
            this.btn확인.Size = new System.Drawing.Size(70, 19);
            this.btn확인.TabIndex = 1;
            this.btn확인.TabStop = false;
            this.btn확인.Text = "확인";
            this.btn확인.UseVisualStyleBackColor = false;
            // 
            // btn조회
            // 
            this.btn조회.BackColor = System.Drawing.Color.Transparent;
            this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn조회.Location = new System.Drawing.Point(668, 3);
            this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn조회.Name = "btn조회";
            this.btn조회.Size = new System.Drawing.Size(70, 19);
            this.btn조회.TabIndex = 2;
            this.btn조회.TabStop = false;
            this.btn조회.Text = "조회";
            this.btn조회.UseVisualStyleBackColor = false;
            // 
            // P_CZ_SA_BILL_SCH_PRE
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(901, 588);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_CZ_SA_BILL_SCH_PRE";
            this.TitleText = "수금현황";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.oneGridItem3.ResumeLayout(false);
            this.bpPanelControl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.LabelExt lbl기간;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Dass.FlexGrid.FlexGrid _flex;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn닫기;
        private Duzon.Common.Controls.RoundedButton btn확인;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.LabelExt lbl수금형태;
        private Duzon.Common.Controls.PeriodPicker dtp기간;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.DropDownComboBox cbo거래구분;
        private Duzon.Common.Controls.LabelExt lbl거래구분;
        private Duzon.Common.Controls.DropDownComboBox cbo수금형태;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.BpControls.BpCodeTextBox ctx영업그룹;
        private Duzon.Common.Controls.LabelExt lbl영업그룹;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpCodeTextBox ctx수금처;
        private Duzon.Common.Controls.LabelExt lbl수금처;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpCodeTextBox ctx매출처;
        private Duzon.Common.Controls.LabelExt lbl매출처;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
        private Duzon.Common.BpControls.BpCodeTextBox ctx담당자;
        private Duzon.Common.Controls.LabelExt lbl담당자;
    }
}
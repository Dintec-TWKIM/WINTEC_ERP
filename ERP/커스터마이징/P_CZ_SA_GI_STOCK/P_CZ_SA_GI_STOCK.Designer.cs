namespace cz
{
    partial class P_CZ_SA_GI_STOCK
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_GI_STOCK));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk완료건제외 = new Duzon.Common.Controls.CheckBoxExt();
			this.lbl완료건제외 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc회사 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl회사 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp의뢰일자 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl의뢰일자 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk기포장건제외 = new Duzon.Common.Controls.CheckBoxExt();
			this.lbl기포장건제외 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt수주번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl수주번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt의뢰번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl의뢰번호 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo업무구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl업무구분 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo진행상태 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl진행상태 = new Duzon.Common.Controls.LabelExt();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.mDataArea.SuspendLayout();
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
			this.bpPanelControl8.SuspendLayout();
			this.bpPanelControl7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(996, 579);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 91F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 192F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(996, 579);
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
			this.oneGrid1.Size = new System.Drawing.Size(990, 85);
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
			this.oneGridItem1.Size = new System.Drawing.Size(980, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.chk완료건제외);
			this.bpPanelControl3.Controls.Add(this.lbl완료건제외);
			this.bpPanelControl3.Location = new System.Drawing.Point(598, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(211, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// chk완료건제외
			// 
			this.chk완료건제외.Checked = true;
			this.chk완료건제외.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk완료건제외.Dock = System.Windows.Forms.DockStyle.Right;
			this.chk완료건제외.Location = new System.Drawing.Point(107, 0);
			this.chk완료건제외.Name = "chk완료건제외";
			this.chk완료건제외.Size = new System.Drawing.Size(104, 23);
			this.chk완료건제외.TabIndex = 1;
			this.chk완료건제외.Text = "제외";
			this.chk완료건제외.TextDD = null;
			this.chk완료건제외.UseVisualStyleBackColor = true;
			// 
			// lbl완료건제외
			// 
			this.lbl완료건제외.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl완료건제외.Location = new System.Drawing.Point(0, 0);
			this.lbl완료건제외.Name = "lbl완료건제외";
			this.lbl완료건제외.Size = new System.Drawing.Size(100, 23);
			this.lbl완료건제외.TabIndex = 0;
			this.lbl완료건제외.Text = "완료";
			this.lbl완료건제외.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.bpc회사);
			this.bpPanelControl2.Controls.Add(this.lbl회사);
			this.bpPanelControl2.Location = new System.Drawing.Point(293, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(303, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// bpc회사
			// 
			this.bpc회사.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc회사.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.bpc회사.Location = new System.Drawing.Point(106, 0);
			this.bpc회사.Name = "bpc회사";
			this.bpc회사.Size = new System.Drawing.Size(197, 21);
			this.bpc회사.TabIndex = 1;
			this.bpc회사.TabStop = false;
			this.bpc회사.Text = "bpComboBox1";
			this.bpc회사.UserCodeName = "NM_COMPANY";
			this.bpc회사.UserCodeValue = "CD_COMPANY";
			this.bpc회사.UserHelpID = "H_CZ_MA_COMPANY_SUB";
			this.bpc회사.UserParams = "회사;H_CZ_MA_COMPANY_SUB";
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
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.dtp의뢰일자);
			this.bpPanelControl1.Controls.Add(this.lbl의뢰일자);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(289, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// dtp의뢰일자
			// 
			this.dtp의뢰일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp의뢰일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp의뢰일자.IsNecessaryCondition = true;
			this.dtp의뢰일자.Location = new System.Drawing.Point(104, 0);
			this.dtp의뢰일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp의뢰일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp의뢰일자.Name = "dtp의뢰일자";
			this.dtp의뢰일자.Size = new System.Drawing.Size(185, 21);
			this.dtp의뢰일자.TabIndex = 1;
			// 
			// lbl의뢰일자
			// 
			this.lbl의뢰일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl의뢰일자.Location = new System.Drawing.Point(0, 0);
			this.lbl의뢰일자.Name = "lbl의뢰일자";
			this.lbl의뢰일자.Size = new System.Drawing.Size(100, 23);
			this.lbl의뢰일자.TabIndex = 0;
			this.lbl의뢰일자.Text = "의뢰일자";
			this.lbl의뢰일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.oneGridItem2.Size = new System.Drawing.Size(980, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl6
			// 
			this.bpPanelControl6.Controls.Add(this.chk기포장건제외);
			this.bpPanelControl6.Controls.Add(this.lbl기포장건제외);
			this.bpPanelControl6.Location = new System.Drawing.Point(598, 1);
			this.bpPanelControl6.Name = "bpPanelControl6";
			this.bpPanelControl6.Size = new System.Drawing.Size(211, 23);
			this.bpPanelControl6.TabIndex = 3;
			this.bpPanelControl6.Text = "bpPanelControl6";
			// 
			// chk기포장건제외
			// 
			this.chk기포장건제외.Checked = true;
			this.chk기포장건제외.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk기포장건제외.Dock = System.Windows.Forms.DockStyle.Right;
			this.chk기포장건제외.Location = new System.Drawing.Point(107, 0);
			this.chk기포장건제외.Name = "chk기포장건제외";
			this.chk기포장건제외.Size = new System.Drawing.Size(104, 23);
			this.chk기포장건제외.TabIndex = 1;
			this.chk기포장건제외.Text = "제외";
			this.chk기포장건제외.TextDD = null;
			this.chk기포장건제외.UseVisualStyleBackColor = true;
			// 
			// lbl기포장건제외
			// 
			this.lbl기포장건제외.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl기포장건제외.Location = new System.Drawing.Point(0, 0);
			this.lbl기포장건제외.Name = "lbl기포장건제외";
			this.lbl기포장건제외.Size = new System.Drawing.Size(100, 23);
			this.lbl기포장건제외.TabIndex = 0;
			this.lbl기포장건제외.Text = "기포장건";
			this.lbl기포장건제외.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.txt수주번호);
			this.bpPanelControl5.Controls.Add(this.lbl수주번호);
			this.bpPanelControl5.Location = new System.Drawing.Point(293, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(303, 23);
			this.bpPanelControl5.TabIndex = 1;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// txt수주번호
			// 
			this.txt수주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt수주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt수주번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt수주번호.Location = new System.Drawing.Point(106, 0);
			this.txt수주번호.Name = "txt수주번호";
			this.txt수주번호.Size = new System.Drawing.Size(197, 21);
			this.txt수주번호.TabIndex = 1;
			// 
			// lbl수주번호
			// 
			this.lbl수주번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl수주번호.Location = new System.Drawing.Point(0, 0);
			this.lbl수주번호.Name = "lbl수주번호";
			this.lbl수주번호.Size = new System.Drawing.Size(100, 23);
			this.lbl수주번호.TabIndex = 0;
			this.lbl수주번호.Text = "수주번호";
			this.lbl수주번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.txt의뢰번호);
			this.bpPanelControl4.Controls.Add(this.lbl의뢰번호);
			this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(289, 23);
			this.bpPanelControl4.TabIndex = 0;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// txt의뢰번호
			// 
			this.txt의뢰번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt의뢰번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt의뢰번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt의뢰번호.Location = new System.Drawing.Point(104, 0);
			this.txt의뢰번호.Name = "txt의뢰번호";
			this.txt의뢰번호.Size = new System.Drawing.Size(185, 21);
			this.txt의뢰번호.TabIndex = 1;
			// 
			// lbl의뢰번호
			// 
			this.lbl의뢰번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl의뢰번호.Location = new System.Drawing.Point(0, 0);
			this.lbl의뢰번호.Name = "lbl의뢰번호";
			this.lbl의뢰번호.Size = new System.Drawing.Size(100, 23);
			this.lbl의뢰번호.TabIndex = 0;
			this.lbl의뢰번호.Text = "의뢰번호";
			this.lbl의뢰번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl8);
			this.oneGridItem3.Controls.Add(this.bpPanelControl7);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(980, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// bpPanelControl8
			// 
			this.bpPanelControl8.Controls.Add(this.cbo업무구분);
			this.bpPanelControl8.Controls.Add(this.lbl업무구분);
			this.bpPanelControl8.Location = new System.Drawing.Point(293, 1);
			this.bpPanelControl8.Name = "bpPanelControl8";
			this.bpPanelControl8.Size = new System.Drawing.Size(303, 23);
			this.bpPanelControl8.TabIndex = 1;
			this.bpPanelControl8.Text = "bpPanelControl8";
			// 
			// cbo업무구분
			// 
			this.cbo업무구분.AutoDropDown = true;
			this.cbo업무구분.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo업무구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo업무구분.FormattingEnabled = true;
			this.cbo업무구분.ItemHeight = 12;
			this.cbo업무구분.Location = new System.Drawing.Point(106, 0);
			this.cbo업무구분.Name = "cbo업무구분";
			this.cbo업무구분.Size = new System.Drawing.Size(197, 20);
			this.cbo업무구분.TabIndex = 1;
			// 
			// lbl업무구분
			// 
			this.lbl업무구분.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl업무구분.Location = new System.Drawing.Point(0, 0);
			this.lbl업무구분.Name = "lbl업무구분";
			this.lbl업무구분.Size = new System.Drawing.Size(100, 23);
			this.lbl업무구분.TabIndex = 0;
			this.lbl업무구분.Text = "업무구분";
			this.lbl업무구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl7
			// 
			this.bpPanelControl7.Controls.Add(this.cbo진행상태);
			this.bpPanelControl7.Controls.Add(this.lbl진행상태);
			this.bpPanelControl7.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl7.Name = "bpPanelControl7";
			this.bpPanelControl7.Size = new System.Drawing.Size(289, 23);
			this.bpPanelControl7.TabIndex = 0;
			this.bpPanelControl7.Text = "bpPanelControl7";
			// 
			// cbo진행상태
			// 
			this.cbo진행상태.AutoDropDown = true;
			this.cbo진행상태.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo진행상태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo진행상태.FormattingEnabled = true;
			this.cbo진행상태.ItemHeight = 12;
			this.cbo진행상태.Location = new System.Drawing.Point(104, 0);
			this.cbo진행상태.Name = "cbo진행상태";
			this.cbo진행상태.Size = new System.Drawing.Size(185, 20);
			this.cbo진행상태.TabIndex = 1;
			// 
			// lbl진행상태
			// 
			this.lbl진행상태.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl진행상태.Location = new System.Drawing.Point(0, 0);
			this.lbl진행상태.Name = "lbl진행상태";
			this.lbl진행상태.Size = new System.Drawing.Size(100, 23);
			this.lbl진행상태.TabIndex = 0;
			this.lbl진행상태.Text = "진행상태";
			this.lbl진행상태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this._flex.Location = new System.Drawing.Point(3, 94);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 20;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(990, 482);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 1;
			this._flex.UseGridCalculator = true;
			// 
			// P_CZ_SA_GI_STOCK
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_SA_GI_STOCK";
			this.Size = new System.Drawing.Size(996, 619);
			this.TitleText = "P_CZ_SA_GI_STOCK";
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl6.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl5.PerformLayout();
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl4.PerformLayout();
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl8.ResumeLayout(false);
			this.bpPanelControl7.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpComboBox bpc회사;
        private Duzon.Common.Controls.LabelExt lbl회사;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.PeriodPicker dtp의뢰일자;
        private Duzon.Common.Controls.LabelExt lbl의뢰일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.LabelExt lbl완료건제외;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.Controls.CheckBoxExt chk완료건제외;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.Controls.TextBoxExt txt수주번호;
        private Duzon.Common.Controls.LabelExt lbl수주번호;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.TextBoxExt txt의뢰번호;
        private Duzon.Common.Controls.LabelExt lbl의뢰번호;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.Controls.CheckBoxExt chk기포장건제외;
        private Duzon.Common.Controls.LabelExt lbl기포장건제외;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
        private Duzon.Common.Controls.DropDownComboBox cbo진행상태;
        private Duzon.Common.Controls.LabelExt lbl진행상태;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl8;
        private Duzon.Common.Controls.DropDownComboBox cbo업무구분;
        private Duzon.Common.Controls.LabelExt lbl업무구분;
    }
}
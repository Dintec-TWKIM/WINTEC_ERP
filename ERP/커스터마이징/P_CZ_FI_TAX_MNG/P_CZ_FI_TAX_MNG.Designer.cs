namespace cz
{
    partial class P_CZ_FI_TAX_MNG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_FI_TAX_MNG));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp적재일자 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl적재일자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp부가세신고일자 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl부가세신고일자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp청구일자 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl청구일자 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp수출선적일자 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl수출선적일자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp수출신고일자 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl수출신고일자 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo수출구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl수출구분 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rbo전표전체 = new Duzon.Common.Controls.RadioButtonExt();
            this.rbo전표승인 = new Duzon.Common.Controls.RadioButtonExt();
            this.rbo전표미승인 = new Duzon.Common.Controls.RadioButtonExt();
            this.lbl전표승인여부 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.rdo부가세전체 = new Duzon.Common.Controls.RadioButtonExt();
            this.rdo부가세신고 = new Duzon.Common.Controls.RadioButtonExt();
            this.rdo부가세미신고 = new Duzon.Common.Controls.RadioButtonExt();
            this.lbl부가세신고여부 = new Duzon.Common.Controls.LabelExt();
            this.btn엑셀업로드 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn엑셀양식다운로드 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn전산매체작성 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.oneGridItem3.SuspendLayout();
            this.bpPanelControl8.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbo전표전체)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbo전표승인)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbo전표미승인)).BeginInit();
            this.bpPanelControl7.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdo부가세전체)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo부가세신고)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo부가세미신고)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(1032, 461);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1032, 461);
            this.tableLayoutPanel1.TabIndex = 0;
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
            this._flex.Location = new System.Drawing.Point(3, 95);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(1026, 363);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 0;
            this._flex.UseGridCalculator = true;
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
            this.oneGrid1.Size = new System.Drawing.Size(1026, 86);
            this.oneGrid1.TabIndex = 1;
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
            this.oneGridItem1.Size = new System.Drawing.Size(1016, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.dtp적재일자);
            this.bpPanelControl3.Controls.Add(this.lbl적재일자);
            this.bpPanelControl3.Location = new System.Drawing.Point(596, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(295, 23);
            this.bpPanelControl3.TabIndex = 3;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // dtp적재일자
            // 
            this.dtp적재일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp적재일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp적재일자.Location = new System.Drawing.Point(110, 0);
            this.dtp적재일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp적재일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp적재일자.Name = "dtp적재일자";
            this.dtp적재일자.Size = new System.Drawing.Size(185, 21);
            this.dtp적재일자.TabIndex = 1;
            // 
            // lbl적재일자
            // 
            this.lbl적재일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl적재일자.Location = new System.Drawing.Point(0, 0);
            this.lbl적재일자.Name = "lbl적재일자";
            this.lbl적재일자.Size = new System.Drawing.Size(100, 23);
            this.lbl적재일자.TabIndex = 0;
            this.lbl적재일자.Text = "적재일자";
            this.lbl적재일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.dtp부가세신고일자);
            this.bpPanelControl2.Controls.Add(this.lbl부가세신고일자);
            this.bpPanelControl2.Location = new System.Drawing.Point(299, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(295, 23);
            this.bpPanelControl2.TabIndex = 2;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // dtp부가세신고일자
            // 
            this.dtp부가세신고일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp부가세신고일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp부가세신고일자.Location = new System.Drawing.Point(110, 0);
            this.dtp부가세신고일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp부가세신고일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp부가세신고일자.Name = "dtp부가세신고일자";
            this.dtp부가세신고일자.Size = new System.Drawing.Size(185, 21);
            this.dtp부가세신고일자.TabIndex = 1;
            // 
            // lbl부가세신고일자
            // 
            this.lbl부가세신고일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl부가세신고일자.Location = new System.Drawing.Point(0, 0);
            this.lbl부가세신고일자.Name = "lbl부가세신고일자";
            this.lbl부가세신고일자.Size = new System.Drawing.Size(100, 23);
            this.lbl부가세신고일자.TabIndex = 0;
            this.lbl부가세신고일자.Text = "부가세신고일자";
            this.lbl부가세신고일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp청구일자);
            this.bpPanelControl1.Controls.Add(this.lbl청구일자);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(295, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // dtp청구일자
            // 
            this.dtp청구일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp청구일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp청구일자.IsNecessaryCondition = true;
            this.dtp청구일자.Location = new System.Drawing.Point(110, 0);
            this.dtp청구일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp청구일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp청구일자.Name = "dtp청구일자";
            this.dtp청구일자.Size = new System.Drawing.Size(185, 21);
            this.dtp청구일자.TabIndex = 1;
            // 
            // lbl청구일자
            // 
            this.lbl청구일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl청구일자.Location = new System.Drawing.Point(0, 0);
            this.lbl청구일자.Name = "lbl청구일자";
            this.lbl청구일자.Size = new System.Drawing.Size(100, 23);
            this.lbl청구일자.TabIndex = 0;
            this.lbl청구일자.Text = "청구일자";
            this.lbl청구일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(1016, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.dtp수출선적일자);
            this.bpPanelControl6.Controls.Add(this.lbl수출선적일자);
            this.bpPanelControl6.Location = new System.Drawing.Point(299, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(295, 23);
            this.bpPanelControl6.TabIndex = 6;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // dtp수출선적일자
            // 
            this.dtp수출선적일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp수출선적일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp수출선적일자.Location = new System.Drawing.Point(110, 0);
            this.dtp수출선적일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp수출선적일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp수출선적일자.Name = "dtp수출선적일자";
            this.dtp수출선적일자.Size = new System.Drawing.Size(185, 21);
            this.dtp수출선적일자.TabIndex = 1;
            // 
            // lbl수출선적일자
            // 
            this.lbl수출선적일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl수출선적일자.Location = new System.Drawing.Point(0, 0);
            this.lbl수출선적일자.Name = "lbl수출선적일자";
            this.lbl수출선적일자.Size = new System.Drawing.Size(100, 23);
            this.lbl수출선적일자.TabIndex = 0;
            this.lbl수출선적일자.Text = "수출선적일자";
            this.lbl수출선적일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.dtp수출신고일자);
            this.bpPanelControl4.Controls.Add(this.lbl수출신고일자);
            this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(295, 23);
            this.bpPanelControl4.TabIndex = 4;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // dtp수출신고일자
            // 
            this.dtp수출신고일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp수출신고일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp수출신고일자.Location = new System.Drawing.Point(110, 0);
            this.dtp수출신고일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp수출신고일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp수출신고일자.Name = "dtp수출신고일자";
            this.dtp수출신고일자.Size = new System.Drawing.Size(185, 21);
            this.dtp수출신고일자.TabIndex = 1;
            // 
            // lbl수출신고일자
            // 
            this.lbl수출신고일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl수출신고일자.Location = new System.Drawing.Point(0, 0);
            this.lbl수출신고일자.Name = "lbl수출신고일자";
            this.lbl수출신고일자.Size = new System.Drawing.Size(100, 23);
            this.lbl수출신고일자.TabIndex = 0;
            this.lbl수출신고일자.Text = "수출신고일자";
            this.lbl수출신고일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl8);
            this.oneGridItem3.Controls.Add(this.bpPanelControl5);
            this.oneGridItem3.Controls.Add(this.bpPanelControl7);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(1016, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 2;
            // 
            // bpPanelControl8
            // 
            this.bpPanelControl8.Controls.Add(this.cbo수출구분);
            this.bpPanelControl8.Controls.Add(this.lbl수출구분);
            this.bpPanelControl8.Location = new System.Drawing.Point(596, 1);
            this.bpPanelControl8.Name = "bpPanelControl8";
            this.bpPanelControl8.Size = new System.Drawing.Size(295, 23);
            this.bpPanelControl8.TabIndex = 4;
            this.bpPanelControl8.Text = "bpPanelControl8";
            // 
            // cbo수출구분
            // 
            this.cbo수출구분.AutoDropDown = true;
            this.cbo수출구분.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo수출구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo수출구분.FormattingEnabled = true;
            this.cbo수출구분.ItemHeight = 12;
            this.cbo수출구분.Location = new System.Drawing.Point(110, 0);
            this.cbo수출구분.Name = "cbo수출구분";
            this.cbo수출구분.Size = new System.Drawing.Size(185, 20);
            this.cbo수출구분.TabIndex = 1;
            // 
            // lbl수출구분
            // 
            this.lbl수출구분.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl수출구분.Location = new System.Drawing.Point(0, 0);
            this.lbl수출구분.Name = "lbl수출구분";
            this.lbl수출구분.Size = new System.Drawing.Size(100, 23);
            this.lbl수출구분.TabIndex = 0;
            this.lbl수출구분.Text = "수출구분";
            this.lbl수출구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.flowLayoutPanel1);
            this.bpPanelControl5.Controls.Add(this.lbl전표승인여부);
            this.bpPanelControl5.Location = new System.Drawing.Point(299, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(295, 23);
            this.bpPanelControl5.TabIndex = 7;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.rbo전표전체);
            this.flowLayoutPanel1.Controls.Add(this.rbo전표승인);
            this.flowLayoutPanel1.Controls.Add(this.rbo전표미승인);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(110, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(185, 23);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // rbo전표전체
            // 
            this.rbo전표전체.AutoSize = true;
            this.rbo전표전체.Checked = true;
            this.rbo전표전체.Location = new System.Drawing.Point(3, 3);
            this.rbo전표전체.Name = "rbo전표전체";
            this.rbo전표전체.Size = new System.Drawing.Size(47, 16);
            this.rbo전표전체.TabIndex = 2;
            this.rbo전표전체.TabStop = true;
            this.rbo전표전체.Text = "전체";
            this.rbo전표전체.TextDD = null;
            this.rbo전표전체.UseKeyEnter = true;
            this.rbo전표전체.UseVisualStyleBackColor = true;
            // 
            // rbo전표승인
            // 
            this.rbo전표승인.AutoSize = true;
            this.rbo전표승인.Location = new System.Drawing.Point(56, 3);
            this.rbo전표승인.Name = "rbo전표승인";
            this.rbo전표승인.Size = new System.Drawing.Size(47, 16);
            this.rbo전표승인.TabIndex = 0;
            this.rbo전표승인.Text = "승인";
            this.rbo전표승인.TextDD = null;
            this.rbo전표승인.UseKeyEnter = true;
            this.rbo전표승인.UseVisualStyleBackColor = true;
            // 
            // rbo전표미승인
            // 
            this.rbo전표미승인.AutoSize = true;
            this.rbo전표미승인.Location = new System.Drawing.Point(109, 3);
            this.rbo전표미승인.Name = "rbo전표미승인";
            this.rbo전표미승인.Size = new System.Drawing.Size(59, 16);
            this.rbo전표미승인.TabIndex = 1;
            this.rbo전표미승인.Text = "미승인";
            this.rbo전표미승인.TextDD = null;
            this.rbo전표미승인.UseKeyEnter = true;
            this.rbo전표미승인.UseVisualStyleBackColor = true;
            // 
            // lbl전표승인여부
            // 
            this.lbl전표승인여부.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl전표승인여부.Location = new System.Drawing.Point(0, 0);
            this.lbl전표승인여부.Name = "lbl전표승인여부";
            this.lbl전표승인여부.Size = new System.Drawing.Size(100, 23);
            this.lbl전표승인여부.TabIndex = 0;
            this.lbl전표승인여부.Text = "전표승인여부";
            this.lbl전표승인여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl7
            // 
            this.bpPanelControl7.Controls.Add(this.flowLayoutPanel2);
            this.bpPanelControl7.Controls.Add(this.lbl부가세신고여부);
            this.bpPanelControl7.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl7.Name = "bpPanelControl7";
            this.bpPanelControl7.Size = new System.Drawing.Size(295, 23);
            this.bpPanelControl7.TabIndex = 6;
            this.bpPanelControl7.Text = "bpPanelControl7";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.rdo부가세전체);
            this.flowLayoutPanel2.Controls.Add(this.rdo부가세신고);
            this.flowLayoutPanel2.Controls.Add(this.rdo부가세미신고);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(110, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(185, 23);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // rdo부가세전체
            // 
            this.rdo부가세전체.AutoSize = true;
            this.rdo부가세전체.Checked = true;
            this.rdo부가세전체.Location = new System.Drawing.Point(3, 3);
            this.rdo부가세전체.Name = "rdo부가세전체";
            this.rdo부가세전체.Size = new System.Drawing.Size(47, 16);
            this.rdo부가세전체.TabIndex = 2;
            this.rdo부가세전체.TabStop = true;
            this.rdo부가세전체.Text = "전체";
            this.rdo부가세전체.TextDD = null;
            this.rdo부가세전체.UseKeyEnter = true;
            this.rdo부가세전체.UseVisualStyleBackColor = true;
            // 
            // rdo부가세신고
            // 
            this.rdo부가세신고.AutoSize = true;
            this.rdo부가세신고.Location = new System.Drawing.Point(56, 3);
            this.rdo부가세신고.Name = "rdo부가세신고";
            this.rdo부가세신고.Size = new System.Drawing.Size(47, 16);
            this.rdo부가세신고.TabIndex = 0;
            this.rdo부가세신고.TabStop = true;
            this.rdo부가세신고.Text = "신고";
            this.rdo부가세신고.TextDD = null;
            this.rdo부가세신고.UseKeyEnter = true;
            this.rdo부가세신고.UseVisualStyleBackColor = true;
            // 
            // rdo부가세미신고
            // 
            this.rdo부가세미신고.AutoSize = true;
            this.rdo부가세미신고.Location = new System.Drawing.Point(109, 3);
            this.rdo부가세미신고.Name = "rdo부가세미신고";
            this.rdo부가세미신고.Size = new System.Drawing.Size(59, 16);
            this.rdo부가세미신고.TabIndex = 1;
            this.rdo부가세미신고.TabStop = true;
            this.rdo부가세미신고.Text = "미신고";
            this.rdo부가세미신고.TextDD = null;
            this.rdo부가세미신고.UseKeyEnter = true;
            this.rdo부가세미신고.UseVisualStyleBackColor = true;
            // 
            // lbl부가세신고여부
            // 
            this.lbl부가세신고여부.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl부가세신고여부.Location = new System.Drawing.Point(0, 0);
            this.lbl부가세신고여부.Name = "lbl부가세신고여부";
            this.lbl부가세신고여부.Size = new System.Drawing.Size(100, 23);
            this.lbl부가세신고여부.TabIndex = 0;
            this.lbl부가세신고여부.Text = "부가세신고여부";
            this.lbl부가세신고여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn엑셀업로드
            // 
            this.btn엑셀업로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn엑셀업로드.BackColor = System.Drawing.Color.Transparent;
            this.btn엑셀업로드.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn엑셀업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn엑셀업로드.Location = new System.Drawing.Point(953, 10);
            this.btn엑셀업로드.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn엑셀업로드.Name = "btn엑셀업로드";
            this.btn엑셀업로드.Size = new System.Drawing.Size(79, 19);
            this.btn엑셀업로드.TabIndex = 6;
            this.btn엑셀업로드.TabStop = false;
            this.btn엑셀업로드.Text = "엑셀업로드";
            this.btn엑셀업로드.UseVisualStyleBackColor = false;
            // 
            // btn엑셀양식다운로드
            // 
            this.btn엑셀양식다운로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn엑셀양식다운로드.BackColor = System.Drawing.Color.Transparent;
            this.btn엑셀양식다운로드.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn엑셀양식다운로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn엑셀양식다운로드.Location = new System.Drawing.Point(840, 10);
            this.btn엑셀양식다운로드.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn엑셀양식다운로드.Name = "btn엑셀양식다운로드";
            this.btn엑셀양식다운로드.Size = new System.Drawing.Size(108, 19);
            this.btn엑셀양식다운로드.TabIndex = 5;
            this.btn엑셀양식다운로드.TabStop = false;
            this.btn엑셀양식다운로드.Text = "엑셀양식다운로드";
            this.btn엑셀양식다운로드.UseVisualStyleBackColor = false;
            // 
            // btn전산매체작성
            // 
            this.btn전산매체작성.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn전산매체작성.BackColor = System.Drawing.Color.Transparent;
            this.btn전산매체작성.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn전산매체작성.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn전산매체작성.Location = new System.Drawing.Point(743, 10);
            this.btn전산매체작성.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn전산매체작성.Name = "btn전산매체작성";
            this.btn전산매체작성.Size = new System.Drawing.Size(91, 19);
            this.btn전산매체작성.TabIndex = 7;
            this.btn전산매체작성.TabStop = false;
            this.btn전산매체작성.Text = "전산매체작성";
            this.btn전산매체작성.UseVisualStyleBackColor = false;
            // 
            // P_CZ_FI_TAX_MNG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btn전산매체작성);
            this.Controls.Add(this.btn엑셀업로드);
            this.Controls.Add(this.btn엑셀양식다운로드);
            this.Name = "P_CZ_FI_TAX_MNG";
            this.Size = new System.Drawing.Size(1032, 501);
            this.TitleText = "부가세관리";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.btn엑셀양식다운로드, 0);
            this.Controls.SetChildIndex(this.btn엑셀업로드, 0);
            this.Controls.SetChildIndex(this.btn전산매체작성, 0);
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.oneGridItem3.ResumeLayout(false);
            this.bpPanelControl8.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbo전표전체)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbo전표승인)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbo전표미승인)).EndInit();
            this.bpPanelControl7.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdo부가세전체)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo부가세신고)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdo부가세미신고)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.PeriodPicker dtp청구일자;
        private Duzon.Common.Controls.LabelExt lbl청구일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.PeriodPicker dtp적재일자;
        private Duzon.Common.Controls.LabelExt lbl적재일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.PeriodPicker dtp부가세신고일자;
        private Duzon.Common.Controls.LabelExt lbl부가세신고일자;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.Controls.PeriodPicker dtp수출선적일자;
        private Duzon.Common.Controls.LabelExt lbl수출선적일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.PeriodPicker dtp수출신고일자;
        private Duzon.Common.Controls.LabelExt lbl수출신고일자;
        private Duzon.Common.Controls.RoundedButton btn엑셀업로드;
        private Duzon.Common.Controls.RoundedButton btn엑셀양식다운로드;
        private Duzon.Common.Controls.RoundedButton btn전산매체작성;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl8;
        private Duzon.Common.Controls.DropDownComboBox cbo수출구분;
        private Duzon.Common.Controls.LabelExt lbl수출구분;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Duzon.Common.Controls.RadioButtonExt rdo부가세전체;
        private Duzon.Common.Controls.RadioButtonExt rdo부가세신고;
        private Duzon.Common.Controls.RadioButtonExt rdo부가세미신고;
        private Duzon.Common.Controls.LabelExt lbl부가세신고여부;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RadioButtonExt rbo전표전체;
        private Duzon.Common.Controls.RadioButtonExt rbo전표승인;
        private Duzon.Common.Controls.RadioButtonExt rbo전표미승인;
        private Duzon.Common.Controls.LabelExt lbl전표승인여부;
    }
}
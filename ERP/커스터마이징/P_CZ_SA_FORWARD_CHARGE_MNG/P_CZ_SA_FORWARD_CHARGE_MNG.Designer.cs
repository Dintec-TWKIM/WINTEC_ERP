namespace cz
{
    partial class P_CZ_SA_FORWARD_CHARGE_MNG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_FORWARD_CHARGE_MNG));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp정산년월 = new Duzon.Common.Controls.DatePicker();
			this.lbl정산년월 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo운송구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl운송구분 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx포워더 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl포워더 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.btn파일번호조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt송장번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl송장번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt출고번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl출고번호 = new Duzon.Common.Controls.LabelExt();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn회계전표취소L = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn회계전표처리L = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn제거 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn회계전표취소통관비 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn회계전표취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn회계전표처리통관비 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn회계전표처리 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn엑셀업로드월말 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn엑셀업로드주간 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn엑셀양식다운로드통관 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn엑셀양식다운로드 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.txt수주번호 = new Duzon.Common.Controls.TextBoxExt();
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp정산년월)).BeginInit();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
			this.imagePanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1747, 699);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1747, 699);
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
			this.oneGrid1.Size = new System.Drawing.Size(1741, 62);
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
			this.oneGridItem1.Size = new System.Drawing.Size(1731, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.dtp정산년월);
			this.bpPanelControl3.Controls.Add(this.lbl정산년월);
			this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(195, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// dtp정산년월
			// 
			this.dtp정산년월.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp정산년월.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp정산년월.Location = new System.Drawing.Point(105, 0);
			this.dtp정산년월.Mask = "####/##";
			this.dtp정산년월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp정산년월.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp정산년월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp정산년월.Modified = true;
			this.dtp정산년월.Name = "dtp정산년월";
			this.dtp정산년월.ShowUpDown = true;
			this.dtp정산년월.Size = new System.Drawing.Size(90, 21);
			this.dtp정산년월.TabIndex = 1;
			this.dtp정산년월.Value = new System.DateTime(((long)(0)));
			// 
			// lbl정산년월
			// 
			this.lbl정산년월.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl정산년월.Location = new System.Drawing.Point(0, 0);
			this.lbl정산년월.Name = "lbl정산년월";
			this.lbl정산년월.Size = new System.Drawing.Size(100, 23);
			this.lbl정산년월.TabIndex = 0;
			this.lbl정산년월.Text = "정산년월";
			this.lbl정산년월.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.cbo운송구분);
			this.bpPanelControl2.Controls.Add(this.lbl운송구분);
			this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// cbo운송구분
			// 
			this.cbo운송구분.AutoDropDown = true;
			this.cbo운송구분.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo운송구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo운송구분.FormattingEnabled = true;
			this.cbo운송구분.ItemHeight = 12;
			this.cbo운송구분.Location = new System.Drawing.Point(106, 0);
			this.cbo운송구분.Name = "cbo운송구분";
			this.cbo운송구분.Size = new System.Drawing.Size(186, 20);
			this.cbo운송구분.TabIndex = 1;
			// 
			// lbl운송구분
			// 
			this.lbl운송구분.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl운송구분.Location = new System.Drawing.Point(0, 0);
			this.lbl운송구분.Name = "lbl운송구분";
			this.lbl운송구분.Size = new System.Drawing.Size(100, 23);
			this.lbl운송구분.TabIndex = 0;
			this.lbl운송구분.Text = "운송구분";
			this.lbl운송구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.ctx포워더);
			this.bpPanelControl1.Controls.Add(this.lbl포워더);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// ctx포워더
			// 
			this.ctx포워더.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx포워더.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
			this.ctx포워더.Location = new System.Drawing.Point(106, 0);
			this.ctx포워더.Name = "ctx포워더";
			this.ctx포워더.Size = new System.Drawing.Size(186, 21);
			this.ctx포워더.TabIndex = 1;
			this.ctx포워더.TabStop = false;
			this.ctx포워더.Text = "bpCodeTextBox1";
			// 
			// lbl포워더
			// 
			this.lbl포워더.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl포워더.Location = new System.Drawing.Point(0, 0);
			this.lbl포워더.Name = "lbl포워더";
			this.lbl포워더.Size = new System.Drawing.Size(100, 23);
			this.lbl포워더.TabIndex = 0;
			this.lbl포워더.Text = "포워더";
			this.lbl포워더.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.txt수주번호);
			this.oneGridItem2.Controls.Add(this.btn파일번호조회);
			this.oneGridItem2.Controls.Add(this.bpPanelControl5);
			this.oneGridItem2.Controls.Add(this.bpPanelControl4);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1731, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// btn파일번호조회
			// 
			this.btn파일번호조회.BackColor = System.Drawing.Color.Transparent;
			this.btn파일번호조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn파일번호조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn파일번호조회.Location = new System.Drawing.Point(590, 1);
			this.btn파일번호조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn파일번호조회.Name = "btn파일번호조회";
			this.btn파일번호조회.Size = new System.Drawing.Size(70, 19);
			this.btn파일번호조회.TabIndex = 2;
			this.btn파일번호조회.TabStop = false;
			this.btn파일번호조회.Text = "조회";
			this.btn파일번호조회.UseVisualStyleBackColor = false;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.txt송장번호);
			this.bpPanelControl5.Controls.Add(this.lbl송장번호);
			this.bpPanelControl5.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl5.TabIndex = 1;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// txt송장번호
			// 
			this.txt송장번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt송장번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt송장번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt송장번호.Location = new System.Drawing.Point(106, 0);
			this.txt송장번호.Name = "txt송장번호";
			this.txt송장번호.Size = new System.Drawing.Size(186, 21);
			this.txt송장번호.TabIndex = 1;
			// 
			// lbl송장번호
			// 
			this.lbl송장번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl송장번호.Location = new System.Drawing.Point(0, 0);
			this.lbl송장번호.Name = "lbl송장번호";
			this.lbl송장번호.Size = new System.Drawing.Size(100, 23);
			this.lbl송장번호.TabIndex = 0;
			this.lbl송장번호.Text = "송장번호";
			this.lbl송장번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.txt출고번호);
			this.bpPanelControl4.Controls.Add(this.lbl출고번호);
			this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl4.TabIndex = 0;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// txt출고번호
			// 
			this.txt출고번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt출고번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt출고번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt출고번호.Location = new System.Drawing.Point(106, 0);
			this.txt출고번호.Name = "txt출고번호";
			this.txt출고번호.Size = new System.Drawing.Size(186, 21);
			this.txt출고번호.TabIndex = 1;
			// 
			// lbl출고번호
			// 
			this.lbl출고번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl출고번호.Location = new System.Drawing.Point(0, 0);
			this.lbl출고번호.Name = "lbl출고번호";
			this.lbl출고번호.Size = new System.Drawing.Size(100, 23);
			this.lbl출고번호.TabIndex = 0;
			this.lbl출고번호.Text = "출고번호";
			this.lbl출고번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 71);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._flexH);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.imagePanel1);
			this.splitContainer1.Size = new System.Drawing.Size(1741, 625);
			this.splitContainer1.SplitterDistance = 211;
			this.splitContainer1.TabIndex = 1;
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
			this._flexH.Size = new System.Drawing.Size(1741, 211);
			this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
			this._flexH.TabIndex = 0;
			this._flexH.UseGridCalculator = true;
			// 
			// imagePanel1
			// 
			this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel1.Controls.Add(this.flowLayoutPanel2);
			this.imagePanel1.Controls.Add(this._flexL);
			this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel1.LeftImage = null;
			this.imagePanel1.Location = new System.Drawing.Point(0, 0);
			this.imagePanel1.Name = "imagePanel1";
			this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel1.PatternImage = null;
			this.imagePanel1.RightImage = null;
			this.imagePanel1.Size = new System.Drawing.Size(1741, 410);
			this.imagePanel1.TabIndex = 2;
			this.imagePanel1.TitleText = "List";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
			this.flowLayoutPanel2.Controls.Add(this.btn회계전표취소L);
			this.flowLayoutPanel2.Controls.Add(this.btn회계전표처리L);
			this.flowLayoutPanel2.Controls.Add(this.btn제거);
			this.flowLayoutPanel2.Controls.Add(this.btn추가);
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(1362, 3);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(376, 26);
			this.flowLayoutPanel2.TabIndex = 6;
			// 
			// btn회계전표취소L
			// 
			this.btn회계전표취소L.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn회계전표취소L.BackColor = System.Drawing.Color.Transparent;
			this.btn회계전표취소L.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn회계전표취소L.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn회계전표취소L.ForeColor = System.Drawing.Color.Transparent;
			this.btn회계전표취소L.Location = new System.Drawing.Point(270, 3);
			this.btn회계전표취소L.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn회계전표취소L.Name = "btn회계전표취소L";
			this.btn회계전표취소L.Size = new System.Drawing.Size(103, 19);
			this.btn회계전표취소L.TabIndex = 5;
			this.btn회계전표취소L.TabStop = false;
			this.btn회계전표취소L.Text = "회계전표취소";
			this.btn회계전표취소L.UseVisualStyleBackColor = false;
			// 
			// btn회계전표처리L
			// 
			this.btn회계전표처리L.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn회계전표처리L.BackColor = System.Drawing.Color.Transparent;
			this.btn회계전표처리L.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn회계전표처리L.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn회계전표처리L.ForeColor = System.Drawing.Color.Transparent;
			this.btn회계전표처리L.Location = new System.Drawing.Point(161, 3);
			this.btn회계전표처리L.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn회계전표처리L.Name = "btn회계전표처리L";
			this.btn회계전표처리L.Size = new System.Drawing.Size(103, 19);
			this.btn회계전표처리L.TabIndex = 4;
			this.btn회계전표처리L.TabStop = false;
			this.btn회계전표처리L.Text = "회계전표처리";
			this.btn회계전표처리L.UseVisualStyleBackColor = false;
			// 
			// btn제거
			// 
			this.btn제거.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn제거.BackColor = System.Drawing.Color.Transparent;
			this.btn제거.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn제거.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn제거.ForeColor = System.Drawing.Color.Transparent;
			this.btn제거.Location = new System.Drawing.Point(85, 3);
			this.btn제거.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn제거.Name = "btn제거";
			this.btn제거.Size = new System.Drawing.Size(70, 19);
			this.btn제거.TabIndex = 2;
			this.btn제거.TabStop = false;
			this.btn제거.Text = "제거";
			this.btn제거.UseVisualStyleBackColor = false;
			// 
			// btn추가
			// 
			this.btn추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn추가.BackColor = System.Drawing.Color.Transparent;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.ForeColor = System.Drawing.Color.Transparent;
			this.btn추가.Location = new System.Drawing.Point(9, 3);
			this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn추가.Name = "btn추가";
			this.btn추가.Size = new System.Drawing.Size(70, 19);
			this.btn추가.TabIndex = 3;
			this.btn추가.TabStop = false;
			this.btn추가.Text = "추가";
			this.btn추가.UseVisualStyleBackColor = false;
			// 
			// _flexL
			// 
			this._flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flexL.AutoResize = false;
			this._flexL.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexL.EnabledHeaderCheck = true;
			this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexL.Location = new System.Drawing.Point(3, 28);
			this._flexL.Name = "_flexL";
			this._flexL.Rows.Count = 1;
			this._flexL.Rows.DefaultSize = 20;
			this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexL.ShowSort = false;
			this._flexL.Size = new System.Drawing.Size(1735, 379);
			this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
			this._flexL.TabIndex = 1;
			this._flexL.UseGridCalculator = true;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Controls.Add(this.btn회계전표취소통관비);
			this.flowLayoutPanel1.Controls.Add(this.btn회계전표취소);
			this.flowLayoutPanel1.Controls.Add(this.btn회계전표처리통관비);
			this.flowLayoutPanel1.Controls.Add(this.btn회계전표처리);
			this.flowLayoutPanel1.Controls.Add(this.btn엑셀업로드월말);
			this.flowLayoutPanel1.Controls.Add(this.btn엑셀업로드주간);
			this.flowLayoutPanel1.Controls.Add(this.btn엑셀양식다운로드통관);
			this.flowLayoutPanel1.Controls.Add(this.btn엑셀양식다운로드);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(716, 10);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(1028, 24);
			this.flowLayoutPanel1.TabIndex = 3;
			// 
			// btn회계전표취소통관비
			// 
			this.btn회계전표취소통관비.BackColor = System.Drawing.Color.Transparent;
			this.btn회계전표취소통관비.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn회계전표취소통관비.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn회계전표취소통관비.Location = new System.Drawing.Point(893, 3);
			this.btn회계전표취소통관비.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn회계전표취소통관비.Name = "btn회계전표취소통관비";
			this.btn회계전표취소통관비.Size = new System.Drawing.Size(132, 19);
			this.btn회계전표취소통관비.TabIndex = 7;
			this.btn회계전표취소통관비.TabStop = false;
			this.btn회계전표취소통관비.Text = "회계전표취소(통관비)";
			this.btn회계전표취소통관비.UseVisualStyleBackColor = false;
			// 
			// btn회계전표취소
			// 
			this.btn회계전표취소.BackColor = System.Drawing.Color.Transparent;
			this.btn회계전표취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn회계전표취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn회계전표취소.Location = new System.Drawing.Point(791, 3);
			this.btn회계전표취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn회계전표취소.Name = "btn회계전표취소";
			this.btn회계전표취소.Size = new System.Drawing.Size(96, 19);
			this.btn회계전표취소.TabIndex = 5;
			this.btn회계전표취소.TabStop = false;
			this.btn회계전표취소.Text = "회계전표취소";
			this.btn회계전표취소.UseVisualStyleBackColor = false;
			// 
			// btn회계전표처리통관비
			// 
			this.btn회계전표처리통관비.BackColor = System.Drawing.Color.Transparent;
			this.btn회계전표처리통관비.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn회계전표처리통관비.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn회계전표처리통관비.Location = new System.Drawing.Point(649, 3);
			this.btn회계전표처리통관비.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn회계전표처리통관비.Name = "btn회계전표처리통관비";
			this.btn회계전표처리통관비.Size = new System.Drawing.Size(136, 19);
			this.btn회계전표처리통관비.TabIndex = 6;
			this.btn회계전표처리통관비.TabStop = false;
			this.btn회계전표처리통관비.Text = "회계전표처리(통관비)";
			this.btn회계전표처리통관비.UseVisualStyleBackColor = false;
			// 
			// btn회계전표처리
			// 
			this.btn회계전표처리.BackColor = System.Drawing.Color.Transparent;
			this.btn회계전표처리.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn회계전표처리.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn회계전표처리.Location = new System.Drawing.Point(547, 3);
			this.btn회계전표처리.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn회계전표처리.Name = "btn회계전표처리";
			this.btn회계전표처리.Size = new System.Drawing.Size(96, 19);
			this.btn회계전표처리.TabIndex = 4;
			this.btn회계전표처리.TabStop = false;
			this.btn회계전표처리.Text = "회계전표처리";
			this.btn회계전표처리.UseVisualStyleBackColor = false;
			// 
			// btn엑셀업로드월말
			// 
			this.btn엑셀업로드월말.BackColor = System.Drawing.Color.Transparent;
			this.btn엑셀업로드월말.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn엑셀업로드월말.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn엑셀업로드월말.Location = new System.Drawing.Point(421, 3);
			this.btn엑셀업로드월말.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn엑셀업로드월말.Name = "btn엑셀업로드월말";
			this.btn엑셀업로드월말.Size = new System.Drawing.Size(120, 19);
			this.btn엑셀업로드월말.TabIndex = 2;
			this.btn엑셀업로드월말.TabStop = false;
			this.btn엑셀업로드월말.Text = "엑셀업로드(월말)";
			this.btn엑셀업로드월말.UseVisualStyleBackColor = false;
			// 
			// btn엑셀업로드주간
			// 
			this.btn엑셀업로드주간.BackColor = System.Drawing.Color.Transparent;
			this.btn엑셀업로드주간.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn엑셀업로드주간.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn엑셀업로드주간.Location = new System.Drawing.Point(295, 3);
			this.btn엑셀업로드주간.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn엑셀업로드주간.Name = "btn엑셀업로드주간";
			this.btn엑셀업로드주간.Size = new System.Drawing.Size(120, 19);
			this.btn엑셀업로드주간.TabIndex = 0;
			this.btn엑셀업로드주간.TabStop = false;
			this.btn엑셀업로드주간.Text = "엑셀업로드(주간)";
			this.btn엑셀업로드주간.UseVisualStyleBackColor = false;
			// 
			// btn엑셀양식다운로드통관
			// 
			this.btn엑셀양식다운로드통관.BackColor = System.Drawing.Color.Transparent;
			this.btn엑셀양식다운로드통관.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn엑셀양식다운로드통관.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn엑셀양식다운로드통관.Location = new System.Drawing.Point(129, 3);
			this.btn엑셀양식다운로드통관.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn엑셀양식다운로드통관.Name = "btn엑셀양식다운로드통관";
			this.btn엑셀양식다운로드통관.Size = new System.Drawing.Size(160, 19);
			this.btn엑셀양식다운로드통관.TabIndex = 3;
			this.btn엑셀양식다운로드통관.TabStop = false;
			this.btn엑셀양식다운로드통관.Text = "엑셀양식다운로드(통관비)";
			this.btn엑셀양식다운로드통관.UseVisualStyleBackColor = false;
			// 
			// btn엑셀양식다운로드
			// 
			this.btn엑셀양식다운로드.BackColor = System.Drawing.Color.Transparent;
			this.btn엑셀양식다운로드.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn엑셀양식다운로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn엑셀양식다운로드.Location = new System.Drawing.Point(3, 3);
			this.btn엑셀양식다운로드.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn엑셀양식다운로드.Name = "btn엑셀양식다운로드";
			this.btn엑셀양식다운로드.Size = new System.Drawing.Size(120, 19);
			this.btn엑셀양식다운로드.TabIndex = 1;
			this.btn엑셀양식다운로드.TabStop = false;
			this.btn엑셀양식다운로드.Text = "엑셀양식다운로드";
			this.btn엑셀양식다운로드.UseVisualStyleBackColor = false;
			// 
			// txt수주번호
			// 
			this.txt수주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt수주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt수주번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt수주번호.Location = new System.Drawing.Point(662, 1);
			this.txt수주번호.Name = "txt수주번호";
			this.txt수주번호.ReadOnly = true;
			this.txt수주번호.Size = new System.Drawing.Size(186, 21);
			this.txt수주번호.TabIndex = 3;
			this.txt수주번호.TabStop = false;
			// 
			// P_CZ_SA_FORWARD_CHARGE_MNG
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "P_CZ_SA_FORWARD_CHARGE_MNG";
			this.Size = new System.Drawing.Size(1747, 739);
			this.TitleText = "P_CZ_SA_FORWARD_CHARGE_MNG";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp정산년월)).EndInit();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.oneGridItem2.PerformLayout();
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl5.PerformLayout();
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl4.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
			this.imagePanel1.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.BpControls.BpCodeTextBox ctx포워더;
        private Duzon.Common.Controls.LabelExt lbl포워더;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn엑셀업로드주간;
        private Duzon.Common.Controls.RoundedButton btn엑셀양식다운로드;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Duzon.Common.Controls.ImagePanel imagePanel1;
        private Duzon.Common.Controls.RoundedButton btn추가;
        private Duzon.Common.Controls.RoundedButton btn제거;
        private Duzon.Common.Controls.RoundedButton btn엑셀업로드월말;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.DropDownComboBox cbo운송구분;
        private Duzon.Common.Controls.LabelExt lbl운송구분;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.DatePicker dtp정산년월;
        private Duzon.Common.Controls.LabelExt lbl정산년월;
        private Duzon.Common.Controls.RoundedButton btn엑셀양식다운로드통관;
        private Duzon.Common.Controls.RoundedButton btn회계전표취소;
        private Duzon.Common.Controls.RoundedButton btn회계전표처리;
        private Duzon.Common.Controls.RoundedButton btn회계전표취소통관비;
        private Duzon.Common.Controls.RoundedButton btn회계전표처리통관비;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
		private Duzon.Common.Controls.LabelExt lbl출고번호;
		private Duzon.Common.Controls.TextBoxExt txt출고번호;
		private Duzon.Common.Controls.RoundedButton btn회계전표취소L;
		private Duzon.Common.Controls.RoundedButton btn회계전표처리L;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
		private Duzon.Common.Controls.TextBoxExt txt송장번호;
		private Duzon.Common.Controls.LabelExt lbl송장번호;
		private Duzon.Common.Controls.RoundedButton btn파일번호조회;
		private Duzon.Common.Controls.TextBoxExt txt수주번호;
	}
}
namespace cz
{
	partial class P_CZ_SA_SO_BATCH_WINTEC
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_SO_BATCH_WINTEC));
			this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn엑셀업로드 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn엑셀양식다운로드 = new Duzon.Common.Controls.RoundedButton(this.components);
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl담당자 = new Duzon.Common.Controls.LabelExt();
			this.ctx담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl수주형태 = new Duzon.Common.Controls.LabelExt();
			this.ctx수주형태 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl영업그룹 = new Duzon.Common.Controls.LabelExt();
			this.ctx영업그룹 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpP단가적용 = new Duzon.Common.BpControls.BpPanelControl();
			this.btn단가적용 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.lbl단가적용 = new Duzon.Common.Controls.LabelExt();
			this.cbo단가적용 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl단가유형 = new Duzon.Common.Controls.LabelExt();
			this.cbo단가유형 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
			this.cur부가세율 = new Duzon.Common.Controls.CurrencyTextBox();
			this.cboVAT구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.lblVAT구분 = new Duzon.Common.Controls.LabelExt();
			this.btn자동등록 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this._tlayMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpP단가적용.SuspendLayout();
			this.bpPanelControl6.SuspendLayout();
			this.bpPanelControl8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur부가세율)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this._tlayMain);
			this.mDataArea.Size = new System.Drawing.Size(1060, 756);
			// 
			// _tlayMain
			// 
			this._tlayMain.ColumnCount = 1;
			this._tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this._tlayMain.Controls.Add(this.splitContainer1, 0, 1);
			this._tlayMain.Controls.Add(this.oneGrid1, 0, 0);
			this._tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this._tlayMain.Location = new System.Drawing.Point(0, 0);
			this._tlayMain.Name = "_tlayMain";
			this._tlayMain.RowCount = 2;
			this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
			this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this._tlayMain.Size = new System.Drawing.Size(1060, 756);
			this._tlayMain.TabIndex = 108;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 75);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this._flex);
			this.splitContainer1.Size = new System.Drawing.Size(1054, 678);
			this.splitContainer1.SplitterDistance = 25;
			this.splitContainer1.TabIndex = 108;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.btn삭제);
			this.flowLayoutPanel2.Controls.Add(this.btn추가);
			this.flowLayoutPanel2.Controls.Add(this.btn엑셀업로드);
			this.flowLayoutPanel2.Controls.Add(this.btn엑셀양식다운로드);
			this.flowLayoutPanel2.Controls.Add(this.btn자동등록);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(1054, 25);
			this.flowLayoutPanel2.TabIndex = 207;
			// 
			// btn삭제
			// 
			this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn삭제.BackColor = System.Drawing.Color.White;
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(989, 1);
			this.btn삭제.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(62, 19);
			this.btn삭제.TabIndex = 105;
			this.btn삭제.TabStop = false;
			this.btn삭제.Tag = "DEL";
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// btn추가
			// 
			this.btn추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn추가.BackColor = System.Drawing.Color.White;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.Location = new System.Drawing.Point(924, 1);
			this.btn추가.Margin = new System.Windows.Forms.Padding(3, 1, 0, 3);
			this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn추가.Name = "btn추가";
			this.btn추가.Size = new System.Drawing.Size(62, 19);
			this.btn추가.TabIndex = 106;
			this.btn추가.TabStop = false;
			this.btn추가.Tag = "APPEND";
			this.btn추가.Text = "추가";
			this.btn추가.UseVisualStyleBackColor = false;
			// 
			// btn엑셀업로드
			// 
			this.btn엑셀업로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn엑셀업로드.BackColor = System.Drawing.Color.White;
			this.btn엑셀업로드.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn엑셀업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn엑셀업로드.Location = new System.Drawing.Point(843, 1);
			this.btn엑셀업로드.Margin = new System.Windows.Forms.Padding(3, 1, 0, 3);
			this.btn엑셀업로드.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn엑셀업로드.Name = "btn엑셀업로드";
			this.btn엑셀업로드.Size = new System.Drawing.Size(78, 19);
			this.btn엑셀업로드.TabIndex = 115;
			this.btn엑셀업로드.TabStop = false;
			this.btn엑셀업로드.Text = "엑셀업로드";
			this.btn엑셀업로드.UseVisualStyleBackColor = true;
			// 
			// btn엑셀양식다운로드
			// 
			this.btn엑셀양식다운로드.BackColor = System.Drawing.Color.White;
			this.btn엑셀양식다운로드.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn엑셀양식다운로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn엑셀양식다운로드.Location = new System.Drawing.Point(733, 1);
			this.btn엑셀양식다운로드.Margin = new System.Windows.Forms.Padding(3, 1, 0, 3);
			this.btn엑셀양식다운로드.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn엑셀양식다운로드.Name = "btn엑셀양식다운로드";
			this.btn엑셀양식다운로드.Size = new System.Drawing.Size(107, 19);
			this.btn엑셀양식다운로드.TabIndex = 116;
			this.btn엑셀양식다운로드.TabStop = false;
			this.btn엑셀양식다운로드.Text = "엑셀양식다운로드";
			this.btn엑셀양식다운로드.UseVisualStyleBackColor = true;
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
			this._flex.Location = new System.Drawing.Point(0, 0);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 20;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(1054, 649);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 0;
			this._flex.UseGridCalculator = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1054, 66);
			this.oneGrid1.TabIndex = 1;
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
			this.oneGridItem1.Size = new System.Drawing.Size(1044, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.lbl담당자);
			this.bpPanelControl3.Controls.Add(this.ctx담당자);
			this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// lbl담당자
			// 
			this.lbl담당자.BackColor = System.Drawing.Color.Transparent;
			this.lbl담당자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl담당자.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl담당자.Location = new System.Drawing.Point(0, 0);
			this.lbl담당자.Name = "lbl담당자";
			this.lbl담당자.Size = new System.Drawing.Size(100, 23);
			this.lbl담당자.TabIndex = 126;
			this.lbl담당자.Text = "담당자";
			this.lbl담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx담당자
			// 
			this.ctx담당자.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.ctx담당자.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx담당자.Location = new System.Drawing.Point(106, 0);
			this.ctx담당자.Name = "ctx담당자";
			this.ctx담당자.Size = new System.Drawing.Size(186, 21);
			this.ctx담당자.TabIndex = 2;
			this.ctx담당자.TabStop = false;
			this.ctx담당자.Tag = "NO_EMP;NM_KOR";
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.lbl수주형태);
			this.bpPanelControl4.Controls.Add(this.ctx수주형태);
			this.bpPanelControl4.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl4.TabIndex = 0;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// lbl수주형태
			// 
			this.lbl수주형태.BackColor = System.Drawing.Color.Transparent;
			this.lbl수주형태.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl수주형태.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl수주형태.Location = new System.Drawing.Point(0, 0);
			this.lbl수주형태.Name = "lbl수주형태";
			this.lbl수주형태.Size = new System.Drawing.Size(100, 23);
			this.lbl수주형태.TabIndex = 126;
			this.lbl수주형태.Text = "수주형태";
			this.lbl수주형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx수주형태
			// 
			this.ctx수주형태.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx수주형태.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ctx수주형태.HelpID = Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB;
			this.ctx수주형태.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx수주형태.Location = new System.Drawing.Point(106, 0);
			this.ctx수주형태.Name = "ctx수주형태";
			this.ctx수주형태.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
			this.ctx수주형태.Size = new System.Drawing.Size(186, 21);
			this.ctx수주형태.TabIndex = 3;
			this.ctx수주형태.TabStop = false;
			this.ctx수주형태.Tag = "TP_SO;NM_SO";
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.lbl영업그룹);
			this.bpPanelControl2.Controls.Add(this.ctx영업그룹);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// lbl영업그룹
			// 
			this.lbl영업그룹.BackColor = System.Drawing.Color.Transparent;
			this.lbl영업그룹.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl영업그룹.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl영업그룹.Location = new System.Drawing.Point(0, 0);
			this.lbl영업그룹.Name = "lbl영업그룹";
			this.lbl영업그룹.Size = new System.Drawing.Size(100, 23);
			this.lbl영업그룹.TabIndex = 126;
			this.lbl영업그룹.Text = "영업그룹";
			this.lbl영업그룹.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx영업그룹
			// 
			this.ctx영업그룹.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx영업그룹.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
			this.ctx영업그룹.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx영업그룹.Location = new System.Drawing.Point(106, 0);
			this.ctx영업그룹.Name = "ctx영업그룹";
			this.ctx영업그룹.Size = new System.Drawing.Size(186, 21);
			this.ctx영업그룹.TabIndex = 1;
			this.ctx영업그룹.TabStop = false;
			this.ctx영업그룹.Tag = "CD_SALEGRP;NM_SALEGRP";
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpP단가적용);
			this.oneGridItem2.Controls.Add(this.bpPanelControl6);
			this.oneGridItem2.Controls.Add(this.bpPanelControl8);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1044, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpP단가적용
			// 
			this.bpP단가적용.Controls.Add(this.btn단가적용);
			this.bpP단가적용.Controls.Add(this.lbl단가적용);
			this.bpP단가적용.Controls.Add(this.cbo단가적용);
			this.bpP단가적용.Location = new System.Drawing.Point(590, 1);
			this.bpP단가적용.Name = "bpP단가적용";
			this.bpP단가적용.Size = new System.Drawing.Size(292, 23);
			this.bpP단가적용.TabIndex = 3;
			this.bpP단가적용.Text = "bpPanelControl9";
			this.bpP단가적용.Visible = false;
			// 
			// btn단가적용
			// 
			this.btn단가적용.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn단가적용.BackColor = System.Drawing.Color.White;
			this.btn단가적용.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn단가적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn단가적용.Location = new System.Drawing.Point(230, 2);
			this.btn단가적용.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn단가적용.Name = "btn단가적용";
			this.btn단가적용.Size = new System.Drawing.Size(62, 19);
			this.btn단가적용.TabIndex = 126;
			this.btn단가적용.TabStop = false;
			this.btn단가적용.Text = "적용";
			this.btn단가적용.UseVisualStyleBackColor = false;
			// 
			// lbl단가적용
			// 
			this.lbl단가적용.BackColor = System.Drawing.Color.Transparent;
			this.lbl단가적용.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl단가적용.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl단가적용.Location = new System.Drawing.Point(0, 0);
			this.lbl단가적용.Name = "lbl단가적용";
			this.lbl단가적용.Size = new System.Drawing.Size(100, 23);
			this.lbl단가적용.TabIndex = 125;
			this.lbl단가적용.Text = "단가적용";
			this.lbl단가적용.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo단가적용
			// 
			this.cbo단가적용.AutoDropDown = true;
			this.cbo단가적용.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.cbo단가적용.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo단가적용.ItemHeight = 12;
			this.cbo단가적용.Location = new System.Drawing.Point(107, 1);
			this.cbo단가적용.Name = "cbo단가적용";
			this.cbo단가적용.Size = new System.Drawing.Size(121, 20);
			this.cbo단가적용.TabIndex = 6;
			this.cbo단가적용.Tag = "TP_PRICE";
			this.cbo단가적용.UseKeyEnter = false;
			this.cbo단가적용.UseKeyF3 = false;
			// 
			// bpPanelControl6
			// 
			this.bpPanelControl6.Controls.Add(this.lbl단가유형);
			this.bpPanelControl6.Controls.Add(this.cbo단가유형);
			this.bpPanelControl6.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl6.Name = "bpPanelControl6";
			this.bpPanelControl6.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl6.TabIndex = 2;
			this.bpPanelControl6.Text = "bpPanelControl6";
			// 
			// lbl단가유형
			// 
			this.lbl단가유형.BackColor = System.Drawing.Color.Transparent;
			this.lbl단가유형.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl단가유형.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl단가유형.Location = new System.Drawing.Point(0, 0);
			this.lbl단가유형.Name = "lbl단가유형";
			this.lbl단가유형.Size = new System.Drawing.Size(100, 23);
			this.lbl단가유형.TabIndex = 125;
			this.lbl단가유형.Text = "단가유형";
			this.lbl단가유형.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo단가유형
			// 
			this.cbo단가유형.AutoDropDown = true;
			this.cbo단가유형.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.cbo단가유형.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo단가유형.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo단가유형.ItemHeight = 12;
			this.cbo단가유형.Location = new System.Drawing.Point(106, 0);
			this.cbo단가유형.Name = "cbo단가유형";
			this.cbo단가유형.Size = new System.Drawing.Size(186, 20);
			this.cbo단가유형.TabIndex = 6;
			this.cbo단가유형.Tag = "TP_PRICE";
			this.cbo단가유형.UseKeyEnter = false;
			this.cbo단가유형.UseKeyF3 = false;
			// 
			// bpPanelControl8
			// 
			this.bpPanelControl8.Controls.Add(this.cur부가세율);
			this.bpPanelControl8.Controls.Add(this.cboVAT구분);
			this.bpPanelControl8.Controls.Add(this.lblVAT구분);
			this.bpPanelControl8.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl8.Name = "bpPanelControl8";
			this.bpPanelControl8.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl8.TabIndex = 1;
			this.bpPanelControl8.Text = "bpPanelControl8";
			// 
			// cur부가세율
			// 
			this.cur부가세율.BackColor = System.Drawing.Color.White;
			this.cur부가세율.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur부가세율.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur부가세율.CurrencyDecimalDigits = 2;
			this.cur부가세율.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur부가세율.Dock = System.Windows.Forms.DockStyle.Right;
			this.cur부가세율.Font = new System.Drawing.Font("굴림체", 9F);
			this.cur부가세율.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur부가세율.Location = new System.Drawing.Point(230, 0);
			this.cur부가세율.Margin = new System.Windows.Forms.Padding(3, 4, 0, 3);
			this.cur부가세율.MaxLength = 17;
			this.cur부가세율.Name = "cur부가세율";
			this.cur부가세율.NullString = "0";
			this.cur부가세율.PositiveColor = System.Drawing.Color.Black;
			this.cur부가세율.ReadOnly = true;
			this.cur부가세율.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur부가세율.Size = new System.Drawing.Size(62, 21);
			this.cur부가세율.TabIndex = 206;
			this.cur부가세율.TabStop = false;
			this.cur부가세율.Tag = "RT_EXCH";
			this.cur부가세율.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.cur부가세율.UseKeyEnter = false;
			this.cur부가세율.UseKeyF3 = false;
			// 
			// cboVAT구분
			// 
			this.cboVAT구분.AutoDropDown = true;
			this.cboVAT구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.cboVAT구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboVAT구분.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.cboVAT구분.ItemHeight = 12;
			this.cboVAT구분.Location = new System.Drawing.Point(106, 1);
			this.cboVAT구분.Name = "cboVAT구분";
			this.cboVAT구분.Size = new System.Drawing.Size(122, 20);
			this.cboVAT구분.TabIndex = 8;
			this.cboVAT구분.Tag = "TP_VAT";
			this.cboVAT구분.UseKeyEnter = false;
			this.cboVAT구분.UseKeyF3 = false;
			// 
			// lblVAT구분
			// 
			this.lblVAT구분.BackColor = System.Drawing.Color.Transparent;
			this.lblVAT구분.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblVAT구분.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblVAT구분.Location = new System.Drawing.Point(0, 0);
			this.lblVAT구분.Name = "lblVAT구분";
			this.lblVAT구분.Size = new System.Drawing.Size(100, 23);
			this.lblVAT구분.TabIndex = 125;
			this.lblVAT구분.Text = "VAT구분";
			this.lblVAT구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btn자동등록
			// 
			this.btn자동등록.BackColor = System.Drawing.Color.White;
			this.btn자동등록.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn자동등록.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn자동등록.Location = new System.Drawing.Point(641, 1);
			this.btn자동등록.Margin = new System.Windows.Forms.Padding(3, 1, 0, 3);
			this.btn자동등록.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn자동등록.Name = "btn자동등록";
			this.btn자동등록.Size = new System.Drawing.Size(89, 19);
			this.btn자동등록.TabIndex = 117;
			this.btn자동등록.TabStop = false;
			this.btn자동등록.Text = "자동등록";
			this.btn자동등록.UseVisualStyleBackColor = true;
			// 
			// P_CZ_SA_SO_BATCH_WINTEC
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Name = "P_CZ_SA_SO_BATCH_WINTEC";
			this.Size = new System.Drawing.Size(1060, 796);
			this.TitleText = "일괄수주등록(윈텍)";
			this.mDataArea.ResumeLayout(false);
			this._tlayMain.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpP단가적용.ResumeLayout(false);
			this.bpPanelControl6.ResumeLayout(false);
			this.bpPanelControl8.ResumeLayout(false);
			this.bpPanelControl8.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur부가세율)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel _tlayMain;
		private Duzon.Common.Controls.RoundedButton btn추가;
		private Duzon.Common.Controls.RoundedButton btn삭제;
		private Dass.FlexGrid.FlexGrid _flex;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Duzon.Common.Controls.RoundedButton btn엑셀업로드;
		private Duzon.Common.Controls.LabelExt lbl단가유형;
		private Duzon.Common.Controls.LabelExt lbl수주형태;
		private Duzon.Common.Controls.LabelExt lblVAT구분;
		private Duzon.Common.Controls.LabelExt lbl담당자;
		private Duzon.Common.Controls.LabelExt lbl영업그룹;
		private Duzon.Common.Controls.DropDownComboBox cbo단가유형;
		private Duzon.Common.BpControls.BpCodeTextBox ctx수주형태;
		private Duzon.Common.BpControls.BpCodeTextBox ctx담당자;
		private Duzon.Common.BpControls.BpCodeTextBox ctx영업그룹;
		private Duzon.Common.Controls.DropDownComboBox cboVAT구분;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl8;
		private Duzon.Common.BpControls.BpPanelControl bpP단가적용;
		private Duzon.Common.Controls.RoundedButton btn단가적용;
		private Duzon.Common.Controls.LabelExt lbl단가적용;
		private Duzon.Common.Controls.DropDownComboBox cbo단가적용;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private Duzon.Common.Controls.CurrencyTextBox cur부가세율;
		private Duzon.Common.Controls.RoundedButton btn엑셀양식다운로드;
		private Duzon.Common.Controls.RoundedButton btn자동등록;
	}
}
namespace cz
{
	partial class P_CZ_SA_NCR_MGT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_NCR_MGT));
			this.lay메인 = new System.Windows.Forms.TableLayoutPanel();
			this.one검색 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx회사코드 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.labelExt9 = new Duzon.Common.Controls.LabelExt();
			this.pnl담당자 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl담당자 = new Duzon.Common.Controls.LabelExt();
			this.pnl작성일자 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo파일번호 = new Duzon.Common.Controls.DropDownComboBox();
			this.labelExt7 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt5 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt3 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbm호선 = new Duzon.Common.BpControls.BpComboBox();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbm매출처 = new Duzon.Common.BpControls.BpComboBox();
			this.labelExt6 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl검색옵션 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk가용재고 = new Duzon.Common.Controls.CheckBoxExt();
			this.labelExt8 = new Duzon.Common.Controls.LabelExt();
			this.pnl문의번호 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo키워드 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbm매입처 = new Duzon.Common.BpControls.BpComboBox();
			this.labelExt4 = new Duzon.Common.Controls.LabelExt();
			this.tab메인 = new Duzon.Common.Controls.TabControlExt();
			this.tab재고납기 = new System.Windows.Forms.TabPage();
			this.grd재고납기 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tab견적현황표 = new System.Windows.Forms.TabPage();
			this.grd견적현황표 = new Dass.FlexGrid.FlexGrid(this.components);
			this.ctx대분류 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.ctx중분류 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.tbx포커스 = new Dintec.UTextBox();
			this.dtp등록일자 = new Dintec.UPeriodPicker();
			this.tbx파일번호 = new Dintec.UTextBox();
			this.tbx키워드 = new Dintec.UTextBox();
			this.mDataArea.SuspendLayout();
			this.lay메인.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl7.SuspendLayout();
			this.pnl담당자.SuspendLayout();
			this.pnl작성일자.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl6.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.pnl검색옵션.SuspendLayout();
			this.pnl문의번호.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.tab메인.SuspendLayout();
			this.tab재고납기.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd재고납기)).BeginInit();
			this.tab견적현황표.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd견적현황표)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.lay메인);
			this.mDataArea.Size = new System.Drawing.Size(1150, 560);
			// 
			// lay메인
			// 
			this.lay메인.ColumnCount = 1;
			this.lay메인.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay메인.Controls.Add(this.one검색, 0, 0);
			this.lay메인.Controls.Add(this.tab메인, 0, 1);
			this.lay메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lay메인.Location = new System.Drawing.Point(0, 0);
			this.lay메인.Name = "lay메인";
			this.lay메인.RowCount = 2;
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay메인.Size = new System.Drawing.Size(1150, 560);
			this.lay메인.TabIndex = 0;
			// 
			// one검색
			// 
			this.one검색.Dock = System.Windows.Forms.DockStyle.Top;
			this.one검색.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
			this.one검색.Location = new System.Drawing.Point(3, 3);
			this.one검색.Name = "one검색";
			this.one검색.Size = new System.Drawing.Size(1144, 85);
			this.one검색.TabIndex = 2;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl7);
			this.oneGridItem1.Controls.Add(this.pnl담당자);
			this.oneGridItem1.Controls.Add(this.pnl작성일자);
			this.oneGridItem1.Controls.Add(this.bpPanelControl5);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1134, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl7
			// 
			this.bpPanelControl7.Controls.Add(this.ctx회사코드);
			this.bpPanelControl7.Controls.Add(this.labelExt9);
			this.bpPanelControl7.Location = new System.Drawing.Point(818, 1);
			this.bpPanelControl7.Name = "bpPanelControl7";
			this.bpPanelControl7.Size = new System.Drawing.Size(270, 23);
			this.bpPanelControl7.TabIndex = 37;
			this.bpPanelControl7.Text = "bpPanelControl3";
			// 
			// ctx회사코드
			// 
			this.ctx회사코드.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_COMPANY_SUB;
			this.ctx회사코드.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx회사코드.Location = new System.Drawing.Point(84, 1);
			this.ctx회사코드.Name = "ctx회사코드";
			this.ctx회사코드.Size = new System.Drawing.Size(186, 21);
			this.ctx회사코드.TabIndex = 5;
			this.ctx회사코드.TabStop = false;
			this.ctx회사코드.Tag = "";
			// 
			// labelExt9
			// 
			this.labelExt9.Location = new System.Drawing.Point(17, 4);
			this.labelExt9.Name = "labelExt9";
			this.labelExt9.Size = new System.Drawing.Size(65, 16);
			this.labelExt9.TabIndex = 3;
			this.labelExt9.Text = "회사코드";
			this.labelExt9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl담당자
			// 
			this.pnl담당자.Controls.Add(this.ctx담당자);
			this.pnl담당자.Controls.Add(this.lbl담당자);
			this.pnl담당자.Location = new System.Drawing.Point(546, 1);
			this.pnl담당자.Name = "pnl담당자";
			this.pnl담당자.Size = new System.Drawing.Size(270, 23);
			this.pnl담당자.TabIndex = 36;
			this.pnl담당자.Text = "bpPanelControl3";
			// 
			// ctx담당자
			// 
			this.ctx담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx담당자.Location = new System.Drawing.Point(84, 1);
			this.ctx담당자.Name = "ctx담당자";
			this.ctx담당자.Size = new System.Drawing.Size(186, 21);
			this.ctx담당자.TabIndex = 5;
			this.ctx담당자.TabStop = false;
			this.ctx담당자.Tag = "";
			this.ctx담당자.UserCodeName = "NM_USER";
			this.ctx담당자.UserCodeValue = "ID_USER";
			this.ctx담당자.UserHelpID = "H_CZ_SA_USER_SUB";
			this.ctx담당자.UserParams = "담당자;H_CZ_SA_USER_SUB";
			// 
			// lbl담당자
			// 
			this.lbl담당자.Location = new System.Drawing.Point(17, 4);
			this.lbl담당자.Name = "lbl담당자";
			this.lbl담당자.Size = new System.Drawing.Size(65, 16);
			this.lbl담당자.TabIndex = 3;
			this.lbl담당자.Text = "담당자";
			this.lbl담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl작성일자
			// 
			this.pnl작성일자.Controls.Add(this.dtp등록일자);
			this.pnl작성일자.Controls.Add(this.labelExt1);
			this.pnl작성일자.Location = new System.Drawing.Point(274, 1);
			this.pnl작성일자.Name = "pnl작성일자";
			this.pnl작성일자.Size = new System.Drawing.Size(270, 23);
			this.pnl작성일자.TabIndex = 34;
			this.pnl작성일자.Text = "bpPanelControl2";
			// 
			// labelExt1
			// 
			this.labelExt1.Location = new System.Drawing.Point(17, 4);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(65, 16);
			this.labelExt1.TabIndex = 4;
			this.labelExt1.Text = "등록일자";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.tbx파일번호);
			this.bpPanelControl5.Controls.Add(this.cbo파일번호);
			this.bpPanelControl5.Controls.Add(this.labelExt7);
			this.bpPanelControl5.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(270, 23);
			this.bpPanelControl5.TabIndex = 33;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// cbo파일번호
			// 
			this.cbo파일번호.AutoDropDown = true;
			this.cbo파일번호.DisplayMember = "NAME";
			this.cbo파일번호.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo파일번호.FormattingEnabled = true;
			this.cbo파일번호.ItemHeight = 12;
			this.cbo파일번호.Location = new System.Drawing.Point(204, 1);
			this.cbo파일번호.Name = "cbo파일번호";
			this.cbo파일번호.Size = new System.Drawing.Size(65, 20);
			this.cbo파일번호.TabIndex = 13;
			this.cbo파일번호.Tag = "";
			this.cbo파일번호.ValueMember = "CODE";
			// 
			// labelExt7
			// 
			this.labelExt7.Location = new System.Drawing.Point(17, 4);
			this.labelExt7.Name = "labelExt7";
			this.labelExt7.Size = new System.Drawing.Size(65, 16);
			this.labelExt7.TabIndex = 5;
			this.labelExt7.Text = "파일번호";
			this.labelExt7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl6);
			this.oneGridItem2.Controls.Add(this.bpPanelControl2);
			this.oneGridItem2.Controls.Add(this.bpPanelControl1);
			this.oneGridItem2.Controls.Add(this.bpPanelControl3);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1134, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl6
			// 
			this.bpPanelControl6.Controls.Add(this.ctx중분류);
			this.bpPanelControl6.Controls.Add(this.labelExt5);
			this.bpPanelControl6.Location = new System.Drawing.Point(818, 1);
			this.bpPanelControl6.Name = "bpPanelControl6";
			this.bpPanelControl6.Size = new System.Drawing.Size(270, 23);
			this.bpPanelControl6.TabIndex = 43;
			this.bpPanelControl6.Text = "bpPanelControl3";
			// 
			// labelExt5
			// 
			this.labelExt5.Location = new System.Drawing.Point(17, 4);
			this.labelExt5.Name = "labelExt5";
			this.labelExt5.Size = new System.Drawing.Size(65, 16);
			this.labelExt5.TabIndex = 3;
			this.labelExt5.Text = "중분류";
			this.labelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.ctx대분류);
			this.bpPanelControl2.Controls.Add(this.labelExt3);
			this.bpPanelControl2.Location = new System.Drawing.Point(546, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(270, 23);
			this.bpPanelControl2.TabIndex = 42;
			this.bpPanelControl2.Text = "bpPanelControl3";
			// 
			// labelExt3
			// 
			this.labelExt3.Location = new System.Drawing.Point(17, 4);
			this.labelExt3.Name = "labelExt3";
			this.labelExt3.Size = new System.Drawing.Size(65, 16);
			this.labelExt3.TabIndex = 3;
			this.labelExt3.Text = "대분류";
			this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.cbm호선);
			this.bpPanelControl1.Controls.Add(this.labelExt2);
			this.bpPanelControl1.Location = new System.Drawing.Point(274, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(270, 23);
			this.bpPanelControl1.TabIndex = 41;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// cbm호선
			// 
			this.cbm호선.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.cbm호선.Location = new System.Drawing.Point(84, 1);
			this.cbm호선.Name = "cbm호선";
			this.cbm호선.Size = new System.Drawing.Size(185, 21);
			this.cbm호선.TabIndex = 4;
			this.cbm호선.TabStop = false;
			this.cbm호선.Text = "bpComboBox1";
			this.cbm호선.UserCodeName = "NO_HULL";
			this.cbm호선.UserCodeValue = "NO_IMO";
			this.cbm호선.UserHelpID = "H_CZ_MA_HULL_SUB";
			this.cbm호선.UserParams = "호선;H_CZ_MA_HULL_SUB";
			// 
			// labelExt2
			// 
			this.labelExt2.Location = new System.Drawing.Point(17, 4);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Size = new System.Drawing.Size(65, 16);
			this.labelExt2.TabIndex = 3;
			this.labelExt2.Text = "호선";
			this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.cbm매출처);
			this.bpPanelControl3.Controls.Add(this.labelExt6);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(270, 23);
			this.bpPanelControl3.TabIndex = 40;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// cbm매출처
			// 
			this.cbm매출처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB1;
			this.cbm매출처.Location = new System.Drawing.Point(84, 1);
			this.cbm매출처.Name = "cbm매출처";
			this.cbm매출처.Size = new System.Drawing.Size(185, 21);
			this.cbm매출처.TabIndex = 4;
			this.cbm매출처.TabStop = false;
			this.cbm매출처.Text = "bpComboBox1";
			// 
			// labelExt6
			// 
			this.labelExt6.Location = new System.Drawing.Point(17, 4);
			this.labelExt6.Name = "labelExt6";
			this.labelExt6.Size = new System.Drawing.Size(65, 16);
			this.labelExt6.TabIndex = 3;
			this.labelExt6.Text = "매출처";
			this.labelExt6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.pnl검색옵션);
			this.oneGridItem3.Controls.Add(this.pnl문의번호);
			this.oneGridItem3.Controls.Add(this.bpPanelControl4);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(1134, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// pnl검색옵션
			// 
			this.pnl검색옵션.Controls.Add(this.chk가용재고);
			this.pnl검색옵션.Controls.Add(this.labelExt8);
			this.pnl검색옵션.Location = new System.Drawing.Point(546, 1);
			this.pnl검색옵션.Name = "pnl검색옵션";
			this.pnl검색옵션.Size = new System.Drawing.Size(542, 23);
			this.pnl검색옵션.TabIndex = 42;
			this.pnl검색옵션.Text = "bpPanelControl1";
			// 
			// chk가용재고
			// 
			this.chk가용재고.Checked = true;
			this.chk가용재고.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk가용재고.Location = new System.Drawing.Point(84, 5);
			this.chk가용재고.Name = "chk가용재고";
			this.chk가용재고.Size = new System.Drawing.Size(226, 16);
			this.chk가용재고.TabIndex = 6;
			this.chk가용재고.Text = "가용재고가 견적수량보다 큰 경우만";
			this.chk가용재고.TextDD = null;
			this.chk가용재고.UseVisualStyleBackColor = true;
			// 
			// labelExt8
			// 
			this.labelExt8.Location = new System.Drawing.Point(17, 4);
			this.labelExt8.Name = "labelExt8";
			this.labelExt8.Size = new System.Drawing.Size(65, 16);
			this.labelExt8.TabIndex = 1;
			this.labelExt8.Text = "검색옵션";
			this.labelExt8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl문의번호
			// 
			this.pnl문의번호.Controls.Add(this.cbo키워드);
			this.pnl문의번호.Controls.Add(this.tbx키워드);
			this.pnl문의번호.Location = new System.Drawing.Point(274, 1);
			this.pnl문의번호.Name = "pnl문의번호";
			this.pnl문의번호.Size = new System.Drawing.Size(270, 23);
			this.pnl문의번호.TabIndex = 41;
			this.pnl문의번호.Text = "bpPanelControl6";
			// 
			// cbo키워드
			// 
			this.cbo키워드.AutoDropDown = true;
			this.cbo키워드.DisplayMember = "NAME";
			this.cbo키워드.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo키워드.FormattingEnabled = true;
			this.cbo키워드.ItemHeight = 12;
			this.cbo키워드.Location = new System.Drawing.Point(7, 1);
			this.cbo키워드.Name = "cbo키워드";
			this.cbo키워드.Size = new System.Drawing.Size(75, 20);
			this.cbo키워드.TabIndex = 18;
			this.cbo키워드.ValueMember = "CODE";
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.cbm매입처);
			this.bpPanelControl4.Controls.Add(this.labelExt4);
			this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(270, 23);
			this.bpPanelControl4.TabIndex = 40;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// cbm매입처
			// 
			this.cbm매입처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB1;
			this.cbm매입처.Location = new System.Drawing.Point(84, 1);
			this.cbm매입처.Name = "cbm매입처";
			this.cbm매입처.Size = new System.Drawing.Size(185, 21);
			this.cbm매입처.TabIndex = 4;
			this.cbm매입처.TabStop = false;
			this.cbm매입처.Text = "bpComboBox1";
			// 
			// labelExt4
			// 
			this.labelExt4.Location = new System.Drawing.Point(17, 4);
			this.labelExt4.Name = "labelExt4";
			this.labelExt4.Size = new System.Drawing.Size(65, 16);
			this.labelExt4.TabIndex = 3;
			this.labelExt4.Text = "매입처";
			this.labelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tab메인
			// 
			this.tab메인.Controls.Add(this.tab재고납기);
			this.tab메인.Controls.Add(this.tab견적현황표);
			this.tab메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tab메인.ItemSize = new System.Drawing.Size(120, 20);
			this.tab메인.Location = new System.Drawing.Point(3, 94);
			this.tab메인.Name = "tab메인";
			this.tab메인.SelectedIndex = 0;
			this.tab메인.Size = new System.Drawing.Size(1144, 463);
			this.tab메인.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tab메인.TabIndex = 3;
			// 
			// tab재고납기
			// 
			this.tab재고납기.Controls.Add(this.grd재고납기);
			this.tab재고납기.Location = new System.Drawing.Point(4, 24);
			this.tab재고납기.Name = "tab재고납기";
			this.tab재고납기.Padding = new System.Windows.Forms.Padding(3);
			this.tab재고납기.Size = new System.Drawing.Size(1136, 435);
			this.tab재고납기.TabIndex = 0;
			this.tab재고납기.Text = "재고납기";
			this.tab재고납기.UseVisualStyleBackColor = true;
			// 
			// grd재고납기
			// 
			this.grd재고납기.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd재고납기.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd재고납기.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd재고납기.AutoResize = false;
			this.grd재고납기.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd재고납기.Cursor = System.Windows.Forms.Cursors.Default;
			this.grd재고납기.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd재고납기.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd재고납기.EnabledHeaderCheck = true;
			this.grd재고납기.ExtendLastCol = true;
			this.grd재고납기.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd재고납기.Location = new System.Drawing.Point(3, 3);
			this.grd재고납기.Name = "grd재고납기";
			this.grd재고납기.Rows.Count = 1;
			this.grd재고납기.Rows.DefaultSize = 20;
			this.grd재고납기.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd재고납기.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd재고납기.ShowSort = false;
			this.grd재고납기.Size = new System.Drawing.Size(1130, 429);
			this.grd재고납기.StyleInfo = resources.GetString("grd재고납기.StyleInfo");
			this.grd재고납기.TabIndex = 3;
			this.grd재고납기.UseGridCalculator = true;
			// 
			// tab견적현황표
			// 
			this.tab견적현황표.Controls.Add(this.grd견적현황표);
			this.tab견적현황표.Location = new System.Drawing.Point(4, 24);
			this.tab견적현황표.Name = "tab견적현황표";
			this.tab견적현황표.Padding = new System.Windows.Forms.Padding(3);
			this.tab견적현황표.Size = new System.Drawing.Size(1136, 435);
			this.tab견적현황표.TabIndex = 1;
			this.tab견적현황표.Text = "견적현황표";
			this.tab견적현황표.UseVisualStyleBackColor = true;
			// 
			// grd견적현황표
			// 
			this.grd견적현황표.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd견적현황표.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd견적현황표.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd견적현황표.AutoResize = false;
			this.grd견적현황표.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd견적현황표.Cursor = System.Windows.Forms.Cursors.Default;
			this.grd견적현황표.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd견적현황표.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd견적현황표.EnabledHeaderCheck = true;
			this.grd견적현황표.ExtendLastCol = true;
			this.grd견적현황표.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd견적현황표.Location = new System.Drawing.Point(3, 3);
			this.grd견적현황표.Name = "grd견적현황표";
			this.grd견적현황표.Rows.Count = 1;
			this.grd견적현황표.Rows.DefaultSize = 20;
			this.grd견적현황표.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd견적현황표.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd견적현황표.ShowSort = false;
			this.grd견적현황표.Size = new System.Drawing.Size(1130, 429);
			this.grd견적현황표.StyleInfo = resources.GetString("grd견적현황표.StyleInfo");
			this.grd견적현황표.TabIndex = 4;
			this.grd견적현황표.UseGridCalculator = true;
			// 
			// ctx대분류
			// 
			this.ctx대분류.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.ctx대분류.Location = new System.Drawing.Point(84, 1);
			this.ctx대분류.Name = "ctx대분류";
			this.ctx대분류.SetDefaultValue = true;
			this.ctx대분류.Size = new System.Drawing.Size(186, 21);
			this.ctx대분류.TabIndex = 7;
			this.ctx대분류.TabStop = false;
			this.ctx대분류.Tag = "CLS_L;NM_CLS_L";
			// 
			// ctx중분류
			// 
			this.ctx중분류.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
			this.ctx중분류.Location = new System.Drawing.Point(84, 1);
			this.ctx중분류.Name = "ctx중분류";
			this.ctx중분류.SetDefaultValue = true;
			this.ctx중분류.Size = new System.Drawing.Size(186, 21);
			this.ctx중분류.TabIndex = 7;
			this.ctx중분류.TabStop = false;
			this.ctx중분류.Tag = "CLS_M;NM_CLS_M";
			// 
			// tbx포커스
			// 
			this.tbx포커스.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx포커스.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx포커스.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.tbx포커스.Location = new System.Drawing.Point(214, 10);
			this.tbx포커스.Name = "tbx포커스";
			this.tbx포커스.Size = new System.Drawing.Size(55, 20);
			this.tbx포커스.TabIndex = 12;
			// 
			// dtp등록일자
			// 
			this.dtp등록일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp등록일자.Location = new System.Drawing.Point(84, 1);
			this.dtp등록일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp등록일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp등록일자.Name = "dtp등록일자";
			this.dtp등록일자.Size = new System.Drawing.Size(185, 21);
			this.dtp등록일자.TabIndex = 6;
			// 
			// tbx파일번호
			// 
			this.tbx파일번호.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.tbx파일번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(141)))), ((int)(((byte)(171)))));
			this.tbx파일번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx파일번호.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.tbx파일번호.Location = new System.Drawing.Point(84, 1);
			this.tbx파일번호.Name = "tbx파일번호";
			this.tbx파일번호.Size = new System.Drawing.Size(118, 20);
			this.tbx파일번호.TabIndex = 14;
			this.tbx파일번호.TextSelectAll = false;
			this.tbx파일번호.UseKeyEnter = false;
			// 
			// tbx키워드
			// 
			this.tbx키워드.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx키워드.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx키워드.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.tbx키워드.Location = new System.Drawing.Point(84, 1);
			this.tbx키워드.Name = "tbx키워드";
			this.tbx키워드.Size = new System.Drawing.Size(185, 20);
			this.tbx키워드.TabIndex = 0;
			// 
			// P_CZ_SA_NCR_MGT
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tbx포커스);
			this.Name = "P_CZ_SA_NCR_MGT";
			this.Size = new System.Drawing.Size(1150, 600);
			this.TitleText = "P_CZ_SA_NCR_MGT";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.tbx포커스, 0);
			this.mDataArea.ResumeLayout(false);
			this.lay메인.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl7.ResumeLayout(false);
			this.pnl담당자.ResumeLayout(false);
			this.pnl작성일자.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl6.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.oneGridItem3.ResumeLayout(false);
			this.pnl검색옵션.ResumeLayout(false);
			this.pnl문의번호.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.tab메인.ResumeLayout(false);
			this.tab재고납기.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd재고납기)).EndInit();
			this.tab견적현황표.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd견적현황표)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel lay메인;
		private Duzon.Erpiu.Windows.OneControls.OneGrid one검색;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
		private Duzon.Common.Controls.TabControlExt tab메인;
		private System.Windows.Forms.TabPage tab재고납기;
		private System.Windows.Forms.TabPage tab견적현황표;
		private Dass.FlexGrid.FlexGrid grd재고납기;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
		private Dintec.UTextBox tbx파일번호;
		private Duzon.Common.Controls.DropDownComboBox cbo파일번호;
		private Duzon.Common.Controls.LabelExt labelExt7;
		private Duzon.Common.BpControls.BpPanelControl pnl작성일자;
		private Dintec.UPeriodPicker dtp등록일자;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
		private Duzon.Common.BpControls.BpCodeTextBox ctx회사코드;
		private Duzon.Common.Controls.LabelExt labelExt9;
		private Duzon.Common.BpControls.BpPanelControl pnl담당자;
		private Duzon.Common.BpControls.BpCodeTextBox ctx담당자;
		private Duzon.Common.Controls.LabelExt lbl담당자;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.BpControls.BpComboBox cbm호선;
		private Duzon.Common.Controls.LabelExt labelExt2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.BpControls.BpComboBox cbm매출처;
		private Duzon.Common.Controls.LabelExt labelExt6;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
		private Duzon.Common.BpControls.BpComboBox cbm매입처;
		private Duzon.Common.Controls.LabelExt labelExt4;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
		private Duzon.Common.Controls.LabelExt labelExt5;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.LabelExt labelExt3;
		private Duzon.Common.BpControls.BpPanelControl pnl문의번호;
		private Duzon.Common.Controls.DropDownComboBox cbo키워드;
		private Dintec.UTextBox tbx키워드;
		private Duzon.Common.BpControls.BpPanelControl pnl검색옵션;
		private Duzon.Common.Controls.CheckBoxExt chk가용재고;
		private Duzon.Common.Controls.LabelExt labelExt8;
		private Dass.FlexGrid.FlexGrid grd견적현황표;
		private Dintec.UTextBox tbx포커스;
		private Duzon.Common.BpControls.BpCodeTextBox ctx대분류;
		private Duzon.Common.BpControls.BpCodeTextBox ctx중분류;
	}
}
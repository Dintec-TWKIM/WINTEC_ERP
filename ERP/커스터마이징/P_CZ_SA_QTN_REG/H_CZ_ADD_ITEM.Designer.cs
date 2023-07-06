namespace cz
{
	partial class H_CZ_ADD_ITEM
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_CZ_ADD_ITEM));
			this.panelExt1 = new Duzon.Common.Controls.PanelExt();
			this.tlay메인 = new System.Windows.Forms.TableLayoutPanel();
			this.pnl엔진정보 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.pnl검색 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.btn저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.oneH = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl선명 = new Duzon.Common.BpControls.BpPanelControl();
			this.tbx선명 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl선명 = new Duzon.Common.Controls.LabelExt();
			this.pnl호선번호 = new Duzon.Common.BpControls.BpPanelControl();
			this.tbx호선번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl호선번호 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl품목코드 = new Duzon.Common.BpControls.BpPanelControl();
			this.tbx품목코드 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl품목코드 = new Duzon.Common.Controls.LabelExt();
			this.pnl아이템정보 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.btn엑셀양식 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn엑셀업로드 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this.tab = new Duzon.Common.Controls.TabControlExt();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.flexLS = new Dass.FlexGrid.FlexGrid(this.components);
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.flexLE = new Dass.FlexGrid.FlexGrid(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.panelExt1.SuspendLayout();
			this.tlay메인.SuspendLayout();
			this.pnl검색.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.pnl선명.SuspendLayout();
			this.pnl호선번호.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.pnl품목코드.SuspendLayout();
			this.pnl아이템정보.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.flexH)).BeginInit();
			this.tab.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.flexLS)).BeginInit();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.flexLE)).BeginInit();
			this.SuspendLayout();
			// 
			// panelExt1
			// 
			this.panelExt1.BackColor = System.Drawing.Color.Transparent;
			this.panelExt1.Controls.Add(this.tlay메인);
			this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt1.Location = new System.Drawing.Point(0, 0);
			this.panelExt1.Name = "panelExt1";
			this.panelExt1.Padding = new System.Windows.Forms.Padding(6, 53, 6, 3);
			this.panelExt1.Size = new System.Drawing.Size(954, 729);
			this.panelExt1.TabIndex = 3;
			// 
			// tlay메인
			// 
			this.tlay메인.ColumnCount = 1;
			this.tlay메인.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlay메인.Controls.Add(this.pnl엔진정보, 0, 2);
			this.tlay메인.Controls.Add(this.pnl검색, 0, 0);
			this.tlay메인.Controls.Add(this.oneH, 0, 1);
			this.tlay메인.Controls.Add(this.pnl아이템정보, 0, 4);
			this.tlay메인.Controls.Add(this.flexH, 0, 3);
			this.tlay메인.Controls.Add(this.tab, 0, 5);
			this.tlay메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlay메인.Location = new System.Drawing.Point(6, 53);
			this.tlay메인.Name = "tlay메인";
			this.tlay메인.RowCount = 6;
			this.tlay메인.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlay메인.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlay메인.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32F));
			this.tlay메인.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68F));
			this.tlay메인.Size = new System.Drawing.Size(942, 673);
			this.tlay메인.TabIndex = 1;
			// 
			// pnl엔진정보
			// 
			this.pnl엔진정보.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.pnl엔진정보.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl엔진정보.LeftImage = null;
			this.pnl엔진정보.Location = new System.Drawing.Point(3, 106);
			this.pnl엔진정보.Name = "pnl엔진정보";
			this.pnl엔진정보.Padding = new System.Windows.Forms.Padding(1);
			this.pnl엔진정보.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.pnl엔진정보.PatternImage = null;
			this.pnl엔진정보.RightImage = null;
			this.pnl엔진정보.Size = new System.Drawing.Size(936, 27);
			this.pnl엔진정보.TabIndex = 10;
			this.pnl엔진정보.TitleText = "엔진 정보";
			// 
			// pnl검색
			// 
			this.pnl검색.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.pnl검색.Controls.Add(this.btn저장);
			this.pnl검색.Controls.Add(this.btn조회);
			this.pnl검색.Controls.Add(this.btn취소);
			this.pnl검색.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl검색.LeftImage = null;
			this.pnl검색.Location = new System.Drawing.Point(3, 3);
			this.pnl검색.Name = "pnl검색";
			this.pnl검색.Padding = new System.Windows.Forms.Padding(1);
			this.pnl검색.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.pnl검색.PatternImage = null;
			this.pnl검색.RightImage = null;
			this.pnl검색.Size = new System.Drawing.Size(936, 27);
			this.pnl검색.TabIndex = 8;
			this.pnl검색.TitleText = "검색";
			// 
			// btn저장
			// 
			this.btn저장.BackColor = System.Drawing.Color.White;
			this.btn저장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn저장.Location = new System.Drawing.Point(782, 4);
			this.btn저장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn저장.Name = "btn저장";
			this.btn저장.Size = new System.Drawing.Size(70, 19);
			this.btn저장.TabIndex = 1;
			this.btn저장.TabStop = false;
			this.btn저장.Text = "저장";
			this.btn저장.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.BackColor = System.Drawing.Color.White;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(706, 4);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 0;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// btn취소
			// 
			this.btn취소.BackColor = System.Drawing.Color.White;
			this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.Location = new System.Drawing.Point(858, 4);
			this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(70, 19);
			this.btn취소.TabIndex = 2;
			this.btn취소.TabStop = false;
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// oneH
			// 
			this.oneH.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneH.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneH.Location = new System.Drawing.Point(3, 36);
			this.oneH.Name = "oneH";
			this.oneH.Size = new System.Drawing.Size(936, 64);
			this.oneH.TabIndex = 9;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.pnl선명);
			this.oneGridItem1.Controls.Add(this.pnl호선번호);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(926, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// pnl선명
			// 
			this.pnl선명.Controls.Add(this.tbx선명);
			this.pnl선명.Controls.Add(this.lbl선명);
			this.pnl선명.Enabled = false;
			this.pnl선명.Location = new System.Drawing.Point(314, 1);
			this.pnl선명.Name = "pnl선명";
			this.pnl선명.Size = new System.Drawing.Size(310, 23);
			this.pnl선명.TabIndex = 6;
			this.pnl선명.Text = "bpPanelControl5";
			// 
			// tbx선명
			// 
			this.tbx선명.BackColor = System.Drawing.Color.White;
			this.tbx선명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx선명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx선명.Location = new System.Drawing.Point(84, 1);
			this.tbx선명.Name = "tbx선명";
			this.tbx선명.Size = new System.Drawing.Size(225, 21);
			this.tbx선명.TabIndex = 8;
			this.tbx선명.Tag = "NM_VESSEL";
			// 
			// lbl선명
			// 
			this.lbl선명.Location = new System.Drawing.Point(17, 4);
			this.lbl선명.Name = "lbl선명";
			this.lbl선명.Size = new System.Drawing.Size(65, 16);
			this.lbl선명.TabIndex = 3;
			this.lbl선명.Text = "선명";
			this.lbl선명.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl호선번호
			// 
			this.pnl호선번호.Controls.Add(this.tbx호선번호);
			this.pnl호선번호.Controls.Add(this.lbl호선번호);
			this.pnl호선번호.Enabled = false;
			this.pnl호선번호.Location = new System.Drawing.Point(2, 1);
			this.pnl호선번호.Name = "pnl호선번호";
			this.pnl호선번호.Size = new System.Drawing.Size(310, 23);
			this.pnl호선번호.TabIndex = 5;
			this.pnl호선번호.Text = "bpPanelControl5";
			// 
			// tbx호선번호
			// 
			this.tbx호선번호.BackColor = System.Drawing.Color.White;
			this.tbx호선번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx호선번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx호선번호.Location = new System.Drawing.Point(84, 1);
			this.tbx호선번호.Name = "tbx호선번호";
			this.tbx호선번호.Size = new System.Drawing.Size(225, 21);
			this.tbx호선번호.TabIndex = 9;
			this.tbx호선번호.Tag = "NM_VESSEL";
			// 
			// lbl호선번호
			// 
			this.lbl호선번호.Location = new System.Drawing.Point(17, 4);
			this.lbl호선번호.Name = "lbl호선번호";
			this.lbl호선번호.Size = new System.Drawing.Size(65, 16);
			this.lbl호선번호.TabIndex = 3;
			this.lbl호선번호.Text = "호선번호";
			this.lbl호선번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.pnl품목코드);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(926, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// pnl품목코드
			// 
			this.pnl품목코드.Controls.Add(this.tbx품목코드);
			this.pnl품목코드.Controls.Add(this.lbl품목코드);
			this.pnl품목코드.Location = new System.Drawing.Point(2, 1);
			this.pnl품목코드.Name = "pnl품목코드";
			this.pnl품목코드.Size = new System.Drawing.Size(310, 23);
			this.pnl품목코드.TabIndex = 15;
			this.pnl품목코드.Text = "bpPanelControl5";
			// 
			// tbx품목코드
			// 
			this.tbx품목코드.BackColor = System.Drawing.Color.White;
			this.tbx품목코드.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx품목코드.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx품목코드.Location = new System.Drawing.Point(84, 1);
			this.tbx품목코드.Name = "tbx품목코드";
			this.tbx품목코드.Size = new System.Drawing.Size(225, 21);
			this.tbx품목코드.TabIndex = 8;
			this.tbx품목코드.Tag = "NM_VESSEL";
			// 
			// lbl품목코드
			// 
			this.lbl품목코드.Location = new System.Drawing.Point(17, 4);
			this.lbl품목코드.Name = "lbl품목코드";
			this.lbl품목코드.Size = new System.Drawing.Size(65, 16);
			this.lbl품목코드.TabIndex = 3;
			this.lbl품목코드.Text = "품목코드";
			this.lbl품목코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl아이템정보
			// 
			this.pnl아이템정보.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.pnl아이템정보.Controls.Add(this.btn엑셀양식);
			this.pnl아이템정보.Controls.Add(this.btn엑셀업로드);
			this.pnl아이템정보.Controls.Add(this.btn추가);
			this.pnl아이템정보.Controls.Add(this.btn삭제);
			this.pnl아이템정보.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl아이템정보.LeftImage = null;
			this.pnl아이템정보.Location = new System.Drawing.Point(3, 300);
			this.pnl아이템정보.Name = "pnl아이템정보";
			this.pnl아이템정보.Padding = new System.Windows.Forms.Padding(1);
			this.pnl아이템정보.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.pnl아이템정보.PatternImage = null;
			this.pnl아이템정보.RightImage = null;
			this.pnl아이템정보.Size = new System.Drawing.Size(936, 27);
			this.pnl아이템정보.TabIndex = 11;
			this.pnl아이템정보.TitleText = "아이템 정보";
			// 
			// btn엑셀양식
			// 
			this.btn엑셀양식.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn엑셀양식.BackColor = System.Drawing.Color.White;
			this.btn엑셀양식.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn엑셀양식.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn엑셀양식.Location = new System.Drawing.Point(616, 4);
			this.btn엑셀양식.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn엑셀양식.Name = "btn엑셀양식";
			this.btn엑셀양식.Size = new System.Drawing.Size(70, 19);
			this.btn엑셀양식.TabIndex = 8;
			this.btn엑셀양식.TabStop = false;
			this.btn엑셀양식.Text = "엑셀양식";
			this.btn엑셀양식.UseVisualStyleBackColor = false;
			// 
			// btn엑셀업로드
			// 
			this.btn엑셀업로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn엑셀업로드.BackColor = System.Drawing.Color.White;
			this.btn엑셀업로드.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn엑셀업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn엑셀업로드.Location = new System.Drawing.Point(692, 4);
			this.btn엑셀업로드.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn엑셀업로드.Name = "btn엑셀업로드";
			this.btn엑셀업로드.Size = new System.Drawing.Size(70, 19);
			this.btn엑셀업로드.TabIndex = 7;
			this.btn엑셀업로드.TabStop = false;
			this.btn엑셀업로드.Text = "엑셀업로드";
			this.btn엑셀업로드.UseVisualStyleBackColor = false;
			// 
			// btn추가
			// 
			this.btn추가.BackColor = System.Drawing.Color.White;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.Location = new System.Drawing.Point(782, 4);
			this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn추가.Name = "btn추가";
			this.btn추가.Size = new System.Drawing.Size(70, 19);
			this.btn추가.TabIndex = 1;
			this.btn추가.TabStop = false;
			this.btn추가.Text = "추가";
			this.btn추가.UseVisualStyleBackColor = false;
			// 
			// btn삭제
			// 
			this.btn삭제.BackColor = System.Drawing.Color.White;
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(858, 4);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(70, 19);
			this.btn삭제.TabIndex = 2;
			this.btn삭제.TabStop = false;
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// flexH
			// 
			this.flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.flexH.AutoResize = false;
			this.flexH.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.flexH.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.flexH.EnabledHeaderCheck = true;
			this.flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.flexH.Location = new System.Drawing.Point(3, 139);
			this.flexH.Name = "flexH";
			this.flexH.Rows.Count = 1;
			this.flexH.Rows.DefaultSize = 18;
			this.flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.flexH.ShowSort = false;
			this.flexH.Size = new System.Drawing.Size(936, 155);
			this.flexH.StyleInfo = resources.GetString("flexH.StyleInfo");
			this.flexH.TabIndex = 12;
			this.flexH.UseGridCalculator = true;
			// 
			// tab
			// 
			this.tab.Controls.Add(this.tabPage1);
			this.tab.Controls.Add(this.tabPage2);
			this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tab.ItemSize = new System.Drawing.Size(100, 20);
			this.tab.Location = new System.Drawing.Point(3, 333);
			this.tab.Name = "tab";
			this.tab.SelectedIndex = 0;
			this.tab.Size = new System.Drawing.Size(936, 337);
			this.tab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tab.TabIndex = 13;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.flexLS);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(928, 309);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "선택";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// flexLS
			// 
			this.flexLS.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.flexLS.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.flexLS.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.flexLS.AutoResize = false;
			this.flexLS.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.flexLS.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexLS.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.flexLS.EnabledHeaderCheck = true;
			this.flexLS.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.flexLS.Location = new System.Drawing.Point(3, 3);
			this.flexLS.Name = "flexLS";
			this.flexLS.Rows.Count = 1;
			this.flexLS.Rows.DefaultSize = 18;
			this.flexLS.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.flexLS.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.flexLS.ShowSort = false;
			this.flexLS.Size = new System.Drawing.Size(922, 303);
			this.flexLS.StyleInfo = resources.GetString("flexLS.StyleInfo");
			this.flexLS.TabIndex = 13;
			this.flexLS.UseGridCalculator = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.flexLE);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(928, 309);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "편집";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// flexLE
			// 
			this.flexLE.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.flexLE.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.flexLE.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.flexLE.AutoResize = false;
			this.flexLE.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.flexLE.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexLE.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.flexLE.EnabledHeaderCheck = true;
			this.flexLE.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.flexLE.Location = new System.Drawing.Point(3, 3);
			this.flexLE.Name = "flexLE";
			this.flexLE.Rows.Count = 1;
			this.flexLE.Rows.DefaultSize = 18;
			this.flexLE.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.flexLE.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.flexLE.ShowSort = false;
			this.flexLE.Size = new System.Drawing.Size(922, 303);
			this.flexLE.StyleInfo = resources.GetString("flexLE.StyleInfo");
			this.flexLE.TabIndex = 14;
			this.flexLE.UseGridCalculator = true;
			// 
			// H_CZ_ADD_ITEM
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(954, 729);
			this.Controls.Add(this.panelExt1);
			this.Name = "H_CZ_ADD_ITEM";
			this.ShowInTaskbar = false;
			this.Text = "H_CZ_ADD_ITEM";
			this.TitleText = "아이템 추가";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.panelExt1.ResumeLayout(false);
			this.tlay메인.ResumeLayout(false);
			this.pnl검색.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.pnl선명.ResumeLayout(false);
			this.pnl선명.PerformLayout();
			this.pnl호선번호.ResumeLayout(false);
			this.pnl호선번호.PerformLayout();
			this.oneGridItem2.ResumeLayout(false);
			this.pnl품목코드.ResumeLayout(false);
			this.pnl품목코드.PerformLayout();
			this.pnl아이템정보.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.flexH)).EndInit();
			this.tab.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.flexLS)).EndInit();
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.flexLE)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Duzon.Common.Controls.PanelExt panelExt1;
		private System.Windows.Forms.TableLayoutPanel tlay메인;
		private Dass.FlexGrid.FlexGrid flexLS;
		private Duzon.Common.Controls.ImagePanel pnl엔진정보;
		private Duzon.Common.Controls.ImagePanel pnl검색;
		private Duzon.Common.Controls.RoundedButton btn저장;
		private Duzon.Common.Controls.RoundedButton btn조회;
		private Duzon.Common.Controls.RoundedButton btn취소;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneH;
		private Duzon.Common.Controls.ImagePanel pnl아이템정보;
		private Dass.FlexGrid.FlexGrid flexH;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl pnl품목코드;
		private Duzon.Common.Controls.TextBoxExt tbx품목코드;
		private Duzon.Common.Controls.LabelExt lbl품목코드;
		private Duzon.Common.BpControls.BpPanelControl pnl호선번호;
		private Duzon.Common.Controls.LabelExt lbl호선번호;
		private Duzon.Common.BpControls.BpPanelControl pnl선명;
		private Duzon.Common.Controls.TextBoxExt tbx선명;
		private Duzon.Common.Controls.LabelExt lbl선명;
		private Duzon.Common.Controls.TextBoxExt tbx호선번호;
		private Duzon.Common.Controls.RoundedButton btn추가;
		private Duzon.Common.Controls.RoundedButton btn삭제;
		private Duzon.Common.Controls.TabControlExt tab;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private Dass.FlexGrid.FlexGrid flexLE;
		private Duzon.Common.Controls.RoundedButton btn엑셀양식;
		private Duzon.Common.Controls.RoundedButton btn엑셀업로드;
	}
}
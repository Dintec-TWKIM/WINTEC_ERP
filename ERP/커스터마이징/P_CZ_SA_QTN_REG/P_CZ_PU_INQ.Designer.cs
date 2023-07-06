namespace cz
{
	partial class P_CZ_PU_INQ
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PU_INQ));
			this.lay메인 = new System.Windows.Forms.TableLayoutPanel();
			this.spl메인 = new System.Windows.Forms.SplitContainer();
			this.grd헤드 = new Dass.FlexGrid.FlexGrid(this.components);
			this.one헤드 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo매입처담당자 = new Duzon.Common.Controls.DropDownComboBox();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp작성일자 = new Duzon.Common.Controls.DatePicker();
			this.lbl견적일자 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl15 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk계약단가제외 = new Duzon.Common.Controls.CheckBoxExt();
			this.labelExt14 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk자동 = new Duzon.Common.Controls.CheckBoxExt();
			this.tbx견적번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl문의번호 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.btn비고적용 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.tbx비고 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.pnl버튼 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.btn워크전달 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn일괄변환 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn메일발송 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn팩스발송 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.lay라인 = new System.Windows.Forms.TableLayoutPanel();
			this.grd라인 = new Dass.FlexGrid.FlexGrid(this.components);
			this.pnl화살표 = new System.Windows.Forms.Panel();
			this.btnTo아이템2 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btnTo아이템1 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.grd카트 = new Dass.FlexGrid.FlexGrid(this.components);
			this.mDataArea.SuspendLayout();
			this.lay메인.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spl메인)).BeginInit();
			this.spl메인.Panel1.SuspendLayout();
			this.spl메인.Panel2.SuspendLayout();
			this.spl메인.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd헤드)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp작성일자)).BeginInit();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl15.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.pnl버튼.SuspendLayout();
			this.lay라인.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd라인)).BeginInit();
			this.pnl화살표.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd카트)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.lay메인);
			this.mDataArea.Location = new System.Drawing.Point(0, 0);
			this.mDataArea.Size = new System.Drawing.Size(1687, 650);
			// 
			// lay메인
			// 
			this.lay메인.ColumnCount = 1;
			this.lay메인.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay메인.Controls.Add(this.spl메인, 0, 1);
			this.lay메인.Controls.Add(this.pnl버튼, 0, 0);
			this.lay메인.Controls.Add(this.lay라인, 0, 2);
			this.lay메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lay메인.Location = new System.Drawing.Point(0, 0);
			this.lay메인.Name = "lay메인";
			this.lay메인.RowCount = 3;
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.lay메인.Size = new System.Drawing.Size(1687, 650);
			this.lay메인.TabIndex = 1;
			// 
			// spl메인
			// 
			this.spl메인.Dock = System.Windows.Forms.DockStyle.Top;
			this.spl메인.Location = new System.Drawing.Point(3, 36);
			this.spl메인.Name = "spl메인";
			// 
			// spl메인.Panel1
			// 
			this.spl메인.Panel1.Controls.Add(this.grd헤드);
			// 
			// spl메인.Panel2
			// 
			this.spl메인.Panel2.Controls.Add(this.one헤드);
			this.spl메인.Size = new System.Drawing.Size(1681, 146);
			this.spl메인.SplitterDistance = 689;
			this.spl메인.TabIndex = 5;
			// 
			// grd헤드
			// 
			this.grd헤드.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd헤드.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd헤드.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd헤드.AutoResize = false;
			this.grd헤드.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd헤드.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd헤드.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd헤드.EnabledHeaderCheck = true;
			this.grd헤드.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd헤드.Location = new System.Drawing.Point(0, 0);
			this.grd헤드.Name = "grd헤드";
			this.grd헤드.Rows.Count = 1;
			this.grd헤드.Rows.DefaultSize = 20;
			this.grd헤드.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd헤드.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd헤드.ShowSort = false;
			this.grd헤드.Size = new System.Drawing.Size(689, 146);
			this.grd헤드.StyleInfo = resources.GetString("grd헤드.StyleInfo");
			this.grd헤드.TabIndex = 0;
			this.grd헤드.UseGridCalculator = true;
			// 
			// one헤드
			// 
			this.one헤드.Dock = System.Windows.Forms.DockStyle.Fill;
			this.one헤드.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem3,
            this.oneGridItem2});
			this.one헤드.Location = new System.Drawing.Point(0, 0);
			this.one헤드.Name = "one헤드";
			this.one헤드.Size = new System.Drawing.Size(988, 146);
			this.one헤드.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(978, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.cbo매입처담당자);
			this.bpPanelControl2.Controls.Add(this.labelExt1);
			this.bpPanelControl2.Location = new System.Drawing.Point(274, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(310, 23);
			this.bpPanelControl2.TabIndex = 9;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// cbo매입처담당자
			// 
			this.cbo매입처담당자.AutoDropDown = true;
			this.cbo매입처담당자.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo매입처담당자.FormattingEnabled = true;
			this.cbo매입처담당자.ItemHeight = 12;
			this.cbo매입처담당자.Location = new System.Drawing.Point(84, 1);
			this.cbo매입처담당자.Name = "cbo매입처담당자";
			this.cbo매입처담당자.Size = new System.Drawing.Size(225, 20);
			this.cbo매입처담당자.TabIndex = 5;
			this.cbo매입처담당자.Tag = "";
			// 
			// labelExt1
			// 
			this.labelExt1.Location = new System.Drawing.Point(2, 4);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(80, 16);
			this.labelExt1.TabIndex = 3;
			this.labelExt1.Text = "매입처담당자";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.dtp작성일자);
			this.bpPanelControl1.Controls.Add(this.lbl견적일자);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(270, 23);
			this.bpPanelControl1.TabIndex = 8;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// dtp작성일자
			// 
			this.dtp작성일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp작성일자.Location = new System.Drawing.Point(84, 1);
			this.dtp작성일자.Mask = "####/##/##";
			this.dtp작성일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp작성일자.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp작성일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp작성일자.Modified = true;
			this.dtp작성일자.Name = "dtp작성일자";
			this.dtp작성일자.Size = new System.Drawing.Size(90, 21);
			this.dtp작성일자.TabIndex = 2;
			this.dtp작성일자.Tag = "DT_INQ";
			this.dtp작성일자.Value = new System.DateTime(((long)(0)));
			// 
			// lbl견적일자
			// 
			this.lbl견적일자.Location = new System.Drawing.Point(17, 4);
			this.lbl견적일자.Name = "lbl견적일자";
			this.lbl견적일자.Size = new System.Drawing.Size(65, 16);
			this.lbl견적일자.TabIndex = 1;
			this.lbl견적일자.Text = "작성일자";
			this.lbl견적일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl15);
			this.oneGridItem3.Controls.Add(this.bpPanelControl3);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(978, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 1;
			// 
			// bpPanelControl15
			// 
			this.bpPanelControl15.Controls.Add(this.chk계약단가제외);
			this.bpPanelControl15.Controls.Add(this.labelExt14);
			this.bpPanelControl15.Location = new System.Drawing.Point(274, 1);
			this.bpPanelControl15.Name = "bpPanelControl15";
			this.bpPanelControl15.Size = new System.Drawing.Size(310, 23);
			this.bpPanelControl15.TabIndex = 24;
			this.bpPanelControl15.Text = "bpPanelControl15";
			// 
			// chk계약단가제외
			// 
			this.chk계약단가제외.Checked = true;
			this.chk계약단가제외.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk계약단가제외.Location = new System.Drawing.Point(88, 4);
			this.chk계약단가제외.Name = "chk계약단가제외";
			this.chk계약단가제외.Size = new System.Drawing.Size(120, 16);
			this.chk계약단가제외.TabIndex = 6;
			this.chk계약단가제외.Text = "계약단가 제외";
			this.chk계약단가제외.TextDD = null;
			this.chk계약단가제외.UseVisualStyleBackColor = true;
			// 
			// labelExt14
			// 
			this.labelExt14.Location = new System.Drawing.Point(17, 4);
			this.labelExt14.Name = "labelExt14";
			this.labelExt14.Size = new System.Drawing.Size(65, 16);
			this.labelExt14.TabIndex = 1;
			this.labelExt14.Text = "검색옵션";
			this.labelExt14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.chk자동);
			this.bpPanelControl3.Controls.Add(this.tbx견적번호);
			this.bpPanelControl3.Controls.Add(this.lbl문의번호);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(270, 23);
			this.bpPanelControl3.TabIndex = 6;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// chk자동
			// 
			this.chk자동.Location = new System.Drawing.Point(220, 5);
			this.chk자동.Name = "chk자동";
			this.chk자동.Size = new System.Drawing.Size(50, 16);
			this.chk자동.TabIndex = 8;
			this.chk자동.Tag = "YN_QUICK";
			this.chk자동.Text = "자동";
			this.chk자동.TextDD = null;
			this.chk자동.UseVisualStyleBackColor = true;
			// 
			// tbx견적번호
			// 
			this.tbx견적번호.BackColor = System.Drawing.SystemColors.Window;
			this.tbx견적번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx견적번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx견적번호.Location = new System.Drawing.Point(84, 1);
			this.tbx견적번호.Name = "tbx견적번호";
			this.tbx견적번호.Size = new System.Drawing.Size(130, 21);
			this.tbx견적번호.TabIndex = 7;
			this.tbx견적번호.Tag = "NO_REF";
			// 
			// lbl문의번호
			// 
			this.lbl문의번호.Location = new System.Drawing.Point(17, 4);
			this.lbl문의번호.Name = "lbl문의번호";
			this.lbl문의번호.Size = new System.Drawing.Size(65, 16);
			this.lbl문의번호.TabIndex = 1;
			this.lbl문의번호.Text = "문의번호";
			this.lbl문의번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.AutoSize = true;
			this.oneGridItem2.Controls.Add(this.bpPanelControl5);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(978, 72);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoWidth;
			this.oneGridItem2.TabIndex = 2;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.btn비고적용);
			this.bpPanelControl5.Controls.Add(this.tbx비고);
			this.bpPanelControl5.Controls.Add(this.labelExt2);
			this.bpPanelControl5.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(934, 69);
			this.bpPanelControl5.TabIndex = 5;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// btn비고적용
			// 
			this.btn비고적용.BackColor = System.Drawing.Color.White;
			this.btn비고적용.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn비고적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn비고적용.Location = new System.Drawing.Point(835, 1);
			this.btn비고적용.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn비고적용.Name = "btn비고적용";
			this.btn비고적용.Size = new System.Drawing.Size(62, 19);
			this.btn비고적용.TabIndex = 11;
			this.btn비고적용.TabStop = false;
			this.btn비고적용.Text = "전체적용";
			this.btn비고적용.UseVisualStyleBackColor = false;
			// 
			// tbx비고
			// 
			this.tbx비고.BackColor = System.Drawing.Color.White;
			this.tbx비고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx비고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx비고.Location = new System.Drawing.Point(84, 1);
			this.tbx비고.Multiline = true;
			this.tbx비고.Name = "tbx비고";
			this.tbx비고.Size = new System.Drawing.Size(748, 42);
			this.tbx비고.TabIndex = 2;
			this.tbx비고.Tag = "DC_RMK_INQ";
			this.tbx비고.UseKeyEnter = false;
			// 
			// labelExt2
			// 
			this.labelExt2.Location = new System.Drawing.Point(17, 4);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Size = new System.Drawing.Size(65, 16);
			this.labelExt2.TabIndex = 1;
			this.labelExt2.Text = "비고";
			this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl버튼
			// 
			this.pnl버튼.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.pnl버튼.Controls.Add(this.btn워크전달);
			this.pnl버튼.Controls.Add(this.btn일괄변환);
			this.pnl버튼.Controls.Add(this.btn메일발송);
			this.pnl버튼.Controls.Add(this.btn팩스발송);
			this.pnl버튼.Controls.Add(this.btn추가);
			this.pnl버튼.Controls.Add(this.btn삭제);
			this.pnl버튼.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl버튼.LeftImage = null;
			this.pnl버튼.Location = new System.Drawing.Point(3, 3);
			this.pnl버튼.Name = "pnl버튼";
			this.pnl버튼.Padding = new System.Windows.Forms.Padding(1);
			this.pnl버튼.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.pnl버튼.PatternImage = null;
			this.pnl버튼.RightImage = null;
			this.pnl버튼.Size = new System.Drawing.Size(1681, 27);
			this.pnl버튼.TabIndex = 6;
			this.pnl버튼.TitleText = "매입처 정보";
			// 
			// btn워크전달
			// 
			this.btn워크전달.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn워크전달.BackColor = System.Drawing.Color.White;
			this.btn워크전달.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn워크전달.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn워크전달.Location = new System.Drawing.Point(1347, 4);
			this.btn워크전달.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn워크전달.Name = "btn워크전달";
			this.btn워크전달.Size = new System.Drawing.Size(70, 19);
			this.btn워크전달.TabIndex = 13;
			this.btn워크전달.TabStop = false;
			this.btn워크전달.Text = "워크전달";
			this.btn워크전달.UseVisualStyleBackColor = false;
			// 
			// btn일괄변환
			// 
			this.btn일괄변환.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn일괄변환.BackColor = System.Drawing.Color.White;
			this.btn일괄변환.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn일괄변환.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn일괄변환.Location = new System.Drawing.Point(1423, 4);
			this.btn일괄변환.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn일괄변환.Name = "btn일괄변환";
			this.btn일괄변환.Size = new System.Drawing.Size(70, 19);
			this.btn일괄변환.TabIndex = 12;
			this.btn일괄변환.TabStop = false;
			this.btn일괄변환.Text = "일괄변환";
			this.btn일괄변환.UseVisualStyleBackColor = false;
			// 
			// btn메일발송
			// 
			this.btn메일발송.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn메일발송.BackColor = System.Drawing.Color.White;
			this.btn메일발송.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn메일발송.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn메일발송.Location = new System.Drawing.Point(1271, 4);
			this.btn메일발송.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn메일발송.Name = "btn메일발송";
			this.btn메일발송.Size = new System.Drawing.Size(70, 19);
			this.btn메일발송.TabIndex = 8;
			this.btn메일발송.TabStop = false;
			this.btn메일발송.Text = "메일발송";
			this.btn메일발송.UseVisualStyleBackColor = false;
			// 
			// btn팩스발송
			// 
			this.btn팩스발송.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn팩스발송.BackColor = System.Drawing.Color.White;
			this.btn팩스발송.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn팩스발송.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn팩스발송.Location = new System.Drawing.Point(965, 4);
			this.btn팩스발송.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn팩스발송.Name = "btn팩스발송";
			this.btn팩스발송.Size = new System.Drawing.Size(70, 19);
			this.btn팩스발송.TabIndex = 7;
			this.btn팩스발송.TabStop = false;
			this.btn팩스발송.Text = "팩스발송";
			this.btn팩스발송.UseVisualStyleBackColor = false;
			this.btn팩스발송.Visible = false;
			// 
			// btn추가
			// 
			this.btn추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn추가.BackColor = System.Drawing.Color.White;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.Location = new System.Drawing.Point(1529, 4);
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
			this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn삭제.BackColor = System.Drawing.Color.Transparent;
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(1605, 4);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(70, 19);
			this.btn삭제.TabIndex = 1;
			this.btn삭제.TabStop = false;
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// lay라인
			// 
			this.lay라인.ColumnCount = 3;
			this.lay라인.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.lay라인.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.lay라인.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.lay라인.Controls.Add(this.grd라인, 0, 0);
			this.lay라인.Controls.Add(this.pnl화살표, 1, 0);
			this.lay라인.Controls.Add(this.grd카트, 2, 0);
			this.lay라인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lay라인.Location = new System.Drawing.Point(0, 185);
			this.lay라인.Margin = new System.Windows.Forms.Padding(0);
			this.lay라인.Name = "lay라인";
			this.lay라인.RowCount = 1;
			this.lay라인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay라인.Size = new System.Drawing.Size(1687, 465);
			this.lay라인.TabIndex = 8;
			// 
			// grd라인
			// 
			this.grd라인.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd라인.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd라인.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd라인.AutoResize = false;
			this.grd라인.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd라인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd라인.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd라인.EnabledHeaderCheck = true;
			this.grd라인.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd라인.Location = new System.Drawing.Point(3, 3);
			this.grd라인.Name = "grd라인";
			this.grd라인.Rows.Count = 1;
			this.grd라인.Rows.DefaultSize = 20;
			this.grd라인.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd라인.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd라인.ShowSort = false;
			this.grd라인.Size = new System.Drawing.Size(810, 459);
			this.grd라인.StyleInfo = resources.GetString("grd라인.StyleInfo");
			this.grd라인.TabIndex = 5;
			this.grd라인.UseGridCalculator = true;
			// 
			// pnl화살표
			// 
			this.pnl화살표.BackColor = System.Drawing.Color.Transparent;
			this.pnl화살표.Controls.Add(this.btnTo아이템2);
			this.pnl화살표.Controls.Add(this.btnTo아이템1);
			this.pnl화살표.ForeColor = System.Drawing.Color.Transparent;
			this.pnl화살표.Location = new System.Drawing.Point(819, 3);
			this.pnl화살표.Name = "pnl화살표";
			this.pnl화살표.Size = new System.Drawing.Size(48, 312);
			this.pnl화살표.TabIndex = 4;
			// 
			// btnTo아이템2
			// 
			this.btnTo아이템2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnTo아이템2.BackColor = System.Drawing.Color.White;
			this.btnTo아이템2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnTo아이템2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTo아이템2.Location = new System.Drawing.Point(3, 187);
			this.btnTo아이템2.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnTo아이템2.Name = "btnTo아이템2";
			this.btnTo아이템2.Size = new System.Drawing.Size(40, 19);
			this.btnTo아이템2.TabIndex = 1;
			this.btnTo아이템2.TabStop = false;
			this.btnTo아이템2.Text = "→";
			this.btnTo아이템2.UseVisualStyleBackColor = false;
			// 
			// btnTo아이템1
			// 
			this.btnTo아이템1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnTo아이템1.BackColor = System.Drawing.Color.Transparent;
			this.btnTo아이템1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnTo아이템1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTo아이템1.Location = new System.Drawing.Point(3, 162);
			this.btnTo아이템1.MaximumSize = new System.Drawing.Size(0, 19);
			this.btnTo아이템1.Name = "btnTo아이템1";
			this.btnTo아이템1.Size = new System.Drawing.Size(40, 19);
			this.btnTo아이템1.TabIndex = 1;
			this.btnTo아이템1.TabStop = false;
			this.btnTo아이템1.Text = "←";
			this.btnTo아이템1.UseVisualStyleBackColor = false;
			// 
			// grd카트
			// 
			this.grd카트.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd카트.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd카트.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd카트.AutoResize = false;
			this.grd카트.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd카트.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd카트.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd카트.EnabledHeaderCheck = true;
			this.grd카트.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd카트.Location = new System.Drawing.Point(873, 3);
			this.grd카트.Name = "grd카트";
			this.grd카트.Rows.Count = 1;
			this.grd카트.Rows.DefaultSize = 20;
			this.grd카트.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd카트.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd카트.ShowSort = false;
			this.grd카트.Size = new System.Drawing.Size(811, 459);
			this.grd카트.StyleInfo = resources.GetString("grd카트.StyleInfo");
			this.grd카트.TabIndex = 1;
			this.grd카트.UseGridCalculator = true;
			// 
			// P_CZ_PU_INQ
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_PU_INQ";
			this.Padding = new System.Windows.Forms.Padding(0);
			this.Size = new System.Drawing.Size(1687, 650);
			this.TitleText = "P_CZ_PU_INQ";
			this.VisibleCaption = false;
			this.mDataArea.ResumeLayout(false);
			this.lay메인.ResumeLayout(false);
			this.spl메인.Panel1.ResumeLayout(false);
			this.spl메인.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spl메인)).EndInit();
			this.spl메인.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd헤드)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp작성일자)).EndInit();
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl15.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl5.PerformLayout();
			this.pnl버튼.ResumeLayout(false);
			this.lay라인.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd라인)).EndInit();
			this.pnl화살표.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd카트)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel lay메인;
		private System.Windows.Forms.SplitContainer spl메인;
		private Dass.FlexGrid.FlexGrid grd헤드;
		private Duzon.Erpiu.Windows.OneControls.OneGrid one헤드;
		private Duzon.Common.Controls.ImagePanel pnl버튼;
		private Duzon.Common.Controls.RoundedButton btn추가;
		private Duzon.Common.Controls.RoundedButton btn삭제;
		private System.Windows.Forms.TableLayoutPanel lay라인;
		private Dass.FlexGrid.FlexGrid grd라인;
		private System.Windows.Forms.Panel pnl화살표;
		private Duzon.Common.Controls.RoundedButton btnTo아이템2;
		private Duzon.Common.Controls.RoundedButton btnTo아이템1;
		private Dass.FlexGrid.FlexGrid grd카트;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
		private Duzon.Common.Controls.TextBoxExt tbx비고;
		private Duzon.Common.Controls.LabelExt labelExt2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.DatePicker dtp작성일자;
		private Duzon.Common.Controls.LabelExt lbl견적일자;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.DropDownComboBox cbo매입처담당자;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Duzon.Common.Controls.RoundedButton btn메일발송;
		private Duzon.Common.Controls.RoundedButton btn팩스발송;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl15;
		private Duzon.Common.Controls.CheckBoxExt chk계약단가제외;
		private Duzon.Common.Controls.LabelExt labelExt14;
		private Duzon.Common.Controls.RoundedButton btn일괄변환;
		private Duzon.Common.Controls.RoundedButton btn비고적용;
		private Duzon.Common.Controls.RoundedButton btn워크전달;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.Controls.TextBoxExt tbx견적번호;
		private Duzon.Common.Controls.LabelExt lbl문의번호;
		private Duzon.Common.Controls.CheckBoxExt chk자동;
	}
}
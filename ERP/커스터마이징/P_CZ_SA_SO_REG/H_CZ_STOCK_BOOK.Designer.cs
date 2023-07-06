namespace cz
{
	partial class H_CZ_STOCK_BOOK
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_CZ_STOCK_BOOK));
			this.rdo재고 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo재고_발주 = new Duzon.Common.Controls.RadioButtonExt();
			this.grd라인 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tabH = new Duzon.Common.Controls.TabControlExt();
			this.tabBOOK = new System.Windows.Forms.TabPage();
			this.grd출고예약 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tabHOLD = new System.Windows.Forms.TabPage();
			this.grd발주예약 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tabNONGR = new System.Windows.Forms.TabPage();
			this.grd재고발주 = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn새로고침 = new System.Windows.Forms.Button();
			this.lay메인 = new System.Windows.Forms.TableLayoutPanel();
			this.lay검색 = new System.Windows.Forms.TableLayoutPanel();
			this.pnl옵션 = new Duzon.Common.Controls.PanelExt();
			this.chk가용재고 = new Duzon.Common.Controls.CheckBoxExt();
			this.label4 = new System.Windows.Forms.Label();
			this.panelExt2 = new Duzon.Common.Controls.PanelExt();
			this.label3 = new System.Windows.Forms.Label();
			this.ctx담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pnl예약 = new Duzon.Common.Controls.PanelExt();
			this.spc라인 = new System.Windows.Forms.SplitContainer();
			this.btn일괄예약 = new System.Windows.Forms.Button();
			this.btn저장 = new System.Windows.Forms.Button();
			this.btn취소 = new System.Windows.Forms.Button();
			this.btn초기화 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo재고)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo재고_발주)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grd라인)).BeginInit();
			this.tabH.SuspendLayout();
			this.tabBOOK.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd출고예약)).BeginInit();
			this.tabHOLD.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd발주예약)).BeginInit();
			this.tabNONGR.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd재고발주)).BeginInit();
			this.lay메인.SuspendLayout();
			this.lay검색.SuspendLayout();
			this.pnl옵션.SuspendLayout();
			this.panelExt2.SuspendLayout();
			this.pnl예약.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spc라인)).BeginInit();
			this.spc라인.Panel1.SuspendLayout();
			this.spc라인.Panel2.SuspendLayout();
			this.spc라인.SuspendLayout();
			this.SuspendLayout();
			// 
			// rdo재고
			// 
			this.rdo재고.AutoSize = true;
			this.rdo재고.Location = new System.Drawing.Point(128, 10);
			this.rdo재고.Name = "rdo재고";
			this.rdo재고.Size = new System.Drawing.Size(47, 16);
			this.rdo재고.TabIndex = 9;
			this.rdo재고.TabStop = true;
			this.rdo재고.Tag = "";
			this.rdo재고.Text = "재고";
			this.rdo재고.TextDD = null;
			this.rdo재고.UseKeyEnter = true;
			this.rdo재고.UseVisualStyleBackColor = true;
			// 
			// rdo재고_발주
			// 
			this.rdo재고_발주.AutoSize = true;
			this.rdo재고_발주.Checked = true;
			this.rdo재고_발주.Location = new System.Drawing.Point(11, 10);
			this.rdo재고_발주.Name = "rdo재고_발주";
			this.rdo재고_발주.Size = new System.Drawing.Size(89, 16);
			this.rdo재고_발주.TabIndex = 8;
			this.rdo재고_발주.TabStop = true;
			this.rdo재고_발주.Tag = "";
			this.rdo재고_발주.Text = "재고 + 발주";
			this.rdo재고_발주.TextDD = null;
			this.rdo재고_발주.UseKeyEnter = true;
			this.rdo재고_발주.UseVisualStyleBackColor = true;
			// 
			// grd라인
			// 
			this.grd라인.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd라인.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd라인.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd라인.AutoResize = false;
			this.grd라인.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd라인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd라인.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd라인.EnabledHeaderCheck = true;
			this.grd라인.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd라인.Location = new System.Drawing.Point(0, 0);
			this.grd라인.Name = "grd라인";
			this.grd라인.Rows.Count = 1;
			this.grd라인.Rows.DefaultSize = 18;
			this.grd라인.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd라인.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd라인.ShowSort = false;
			this.grd라인.Size = new System.Drawing.Size(1835, 558);
			this.grd라인.StyleInfo = resources.GetString("grd라인.StyleInfo");
			this.grd라인.TabIndex = 2;
			this.grd라인.UseGridCalculator = true;
			// 
			// tabH
			// 
			this.tabH.Controls.Add(this.tabBOOK);
			this.tabH.Controls.Add(this.tabHOLD);
			this.tabH.Controls.Add(this.tabNONGR);
			this.tabH.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabH.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.tabH.ItemSize = new System.Drawing.Size(120, 24);
			this.tabH.Location = new System.Drawing.Point(0, 0);
			this.tabH.Name = "tabH";
			this.tabH.SelectedIndex = 0;
			this.tabH.Size = new System.Drawing.Size(1835, 242);
			this.tabH.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabH.TabIndex = 3;
			// 
			// tabBOOK
			// 
			this.tabBOOK.Controls.Add(this.grd출고예약);
			this.tabBOOK.Location = new System.Drawing.Point(4, 28);
			this.tabBOOK.Name = "tabBOOK";
			this.tabBOOK.Padding = new System.Windows.Forms.Padding(3);
			this.tabBOOK.Size = new System.Drawing.Size(1827, 210);
			this.tabBOOK.TabIndex = 0;
			this.tabBOOK.Text = "출고예약현황";
			this.tabBOOK.UseVisualStyleBackColor = true;
			// 
			// grd출고예약
			// 
			this.grd출고예약.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd출고예약.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd출고예약.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd출고예약.AutoResize = false;
			this.grd출고예약.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd출고예약.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd출고예약.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd출고예약.EnabledHeaderCheck = true;
			this.grd출고예약.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd출고예약.Location = new System.Drawing.Point(3, 3);
			this.grd출고예약.Name = "grd출고예약";
			this.grd출고예약.Rows.Count = 1;
			this.grd출고예약.Rows.DefaultSize = 18;
			this.grd출고예약.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd출고예약.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd출고예약.ShowSort = false;
			this.grd출고예약.Size = new System.Drawing.Size(1821, 204);
			this.grd출고예약.StyleInfo = resources.GetString("grd출고예약.StyleInfo");
			this.grd출고예약.TabIndex = 0;
			this.grd출고예약.UseGridCalculator = true;
			// 
			// tabHOLD
			// 
			this.tabHOLD.Controls.Add(this.grd발주예약);
			this.tabHOLD.Location = new System.Drawing.Point(4, 28);
			this.tabHOLD.Name = "tabHOLD";
			this.tabHOLD.Padding = new System.Windows.Forms.Padding(3);
			this.tabHOLD.Size = new System.Drawing.Size(1827, 210);
			this.tabHOLD.TabIndex = 2;
			this.tabHOLD.Text = "입고예약현황";
			this.tabHOLD.UseVisualStyleBackColor = true;
			// 
			// grd발주예약
			// 
			this.grd발주예약.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd발주예약.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd발주예약.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd발주예약.AutoResize = false;
			this.grd발주예약.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd발주예약.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd발주예약.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd발주예약.EnabledHeaderCheck = true;
			this.grd발주예약.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd발주예약.Location = new System.Drawing.Point(3, 3);
			this.grd발주예약.Name = "grd발주예약";
			this.grd발주예약.Rows.Count = 1;
			this.grd발주예약.Rows.DefaultSize = 18;
			this.grd발주예약.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd발주예약.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd발주예약.ShowSort = false;
			this.grd발주예약.Size = new System.Drawing.Size(1821, 204);
			this.grd발주예약.StyleInfo = resources.GetString("grd발주예약.StyleInfo");
			this.grd발주예약.TabIndex = 1;
			this.grd발주예약.UseGridCalculator = true;
			// 
			// tabNONGR
			// 
			this.tabNONGR.Controls.Add(this.grd재고발주);
			this.tabNONGR.Location = new System.Drawing.Point(4, 28);
			this.tabNONGR.Name = "tabNONGR";
			this.tabNONGR.Padding = new System.Windows.Forms.Padding(3);
			this.tabNONGR.Size = new System.Drawing.Size(1827, 210);
			this.tabNONGR.TabIndex = 1;
			this.tabNONGR.Text = "미입고현황";
			this.tabNONGR.UseVisualStyleBackColor = true;
			// 
			// grd재고발주
			// 
			this.grd재고발주.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd재고발주.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd재고발주.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd재고발주.AutoResize = false;
			this.grd재고발주.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd재고발주.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd재고발주.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd재고발주.EnabledHeaderCheck = true;
			this.grd재고발주.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd재고발주.Location = new System.Drawing.Point(3, 3);
			this.grd재고발주.Name = "grd재고발주";
			this.grd재고발주.Rows.Count = 1;
			this.grd재고발주.Rows.DefaultSize = 18;
			this.grd재고발주.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd재고발주.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd재고발주.ShowSort = false;
			this.grd재고발주.Size = new System.Drawing.Size(1821, 204);
			this.grd재고발주.StyleInfo = resources.GetString("grd재고발주.StyleInfo");
			this.grd재고발주.TabIndex = 0;
			this.grd재고발주.UseGridCalculator = true;
			// 
			// btn새로고침
			// 
			this.btn새로고침.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn새로고침.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(121)))), ((int)(((byte)(197)))));
			this.btn새로고침.FlatAppearance.BorderSize = 0;
			this.btn새로고침.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn새로고침.ForeColor = System.Drawing.Color.White;
			this.btn새로고침.Location = new System.Drawing.Point(1416, 10);
			this.btn새로고침.Name = "btn새로고침";
			this.btn새로고침.Size = new System.Drawing.Size(70, 26);
			this.btn새로고침.TabIndex = 19;
			this.btn새로고침.Text = "새로고침";
			this.btn새로고침.UseVisualStyleBackColor = false;
			// 
			// lay메인
			// 
			this.lay메인.ColumnCount = 1;
			this.lay메인.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay메인.Controls.Add(this.lay검색, 0, 0);
			this.lay메인.Controls.Add(this.spc라인, 0, 1);
			this.lay메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lay메인.Location = new System.Drawing.Point(6, 53);
			this.lay메인.Name = "lay메인";
			this.lay메인.RowCount = 2;
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay메인.Size = new System.Drawing.Size(1835, 884);
			this.lay메인.TabIndex = 20;
			// 
			// lay검색
			// 
			this.lay검색.ColumnCount = 4;
			this.lay검색.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.lay검색.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
			this.lay검색.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.lay검색.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay검색.Controls.Add(this.pnl옵션, 3, 0);
			this.lay검색.Controls.Add(this.label4, 2, 0);
			this.lay검색.Controls.Add(this.panelExt2, 1, 1);
			this.lay검색.Controls.Add(this.label2, 0, 1);
			this.lay검색.Controls.Add(this.label1, 0, 0);
			this.lay검색.Controls.Add(this.pnl예약, 1, 0);
			this.lay검색.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lay검색.Location = new System.Drawing.Point(0, 0);
			this.lay검색.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.lay검색.Name = "lay검색";
			this.lay검색.RowCount = 2;
			this.lay검색.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.lay검색.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay검색.Size = new System.Drawing.Size(1835, 74);
			this.lay검색.TabIndex = 0;
			this.lay검색.Tag = "VIEWBOX";
			// 
			// pnl옵션
			// 
			this.pnl옵션.Controls.Add(this.chk가용재고);
			this.pnl옵션.Location = new System.Drawing.Point(601, 1);
			this.pnl옵션.Margin = new System.Windows.Forms.Padding(1);
			this.pnl옵션.Name = "pnl옵션";
			this.pnl옵션.Size = new System.Drawing.Size(356, 33);
			this.pnl옵션.TabIndex = 23;
			// 
			// chk가용재고
			// 
			this.chk가용재고.AutoSize = true;
			this.chk가용재고.Location = new System.Drawing.Point(10, 11);
			this.chk가용재고.Name = "chk가용재고";
			this.chk가용재고.Size = new System.Drawing.Size(222, 16);
			this.chk가용재고.TabIndex = 17;
			this.chk가용재고.Text = "가용재고가 수주수량보다 큰 경우만";
			this.chk가용재고.TextDD = null;
			this.chk가용재고.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label4.Location = new System.Drawing.Point(501, 1);
			this.label4.Margin = new System.Windows.Forms.Padding(1);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(98, 33);
			this.label4.TabIndex = 22;
			this.label4.Text = "옵션";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelExt2
			// 
			this.lay검색.SetColumnSpan(this.panelExt2, 3);
			this.panelExt2.Controls.Add(this.label3);
			this.panelExt2.Controls.Add(this.ctx담당자);
			this.panelExt2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt2.Location = new System.Drawing.Point(101, 36);
			this.panelExt2.Margin = new System.Windows.Forms.Padding(1);
			this.panelExt2.Name = "panelExt2";
			this.panelExt2.Size = new System.Drawing.Size(1733, 37);
			this.panelExt2.TabIndex = 21;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label3.Location = new System.Drawing.Point(222, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(298, 17);
			this.label3.TabIndex = 26;
			this.label3.Text = "※ 담당자 변경시 예약 정보는 저장되지 않습니다.";
			// 
			// ctx담당자
			// 
			this.ctx담당자.Enabled = false;
			this.ctx담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.ctx담당자.Location = new System.Drawing.Point(8, 8);
			this.ctx담당자.Name = "ctx담당자";
			this.ctx담당자.Size = new System.Drawing.Size(186, 21);
			this.ctx담당자.TabIndex = 1;
			this.ctx담당자.TabStop = false;
			this.ctx담당자.Tag = "";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label2.Location = new System.Drawing.Point(1, 36);
			this.label2.Margin = new System.Windows.Forms.Padding(1);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98, 37);
			this.label2.TabIndex = 20;
			this.label2.Text = "담당자변경";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.Location = new System.Drawing.Point(1, 1);
			this.label1.Margin = new System.Windows.Forms.Padding(1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 33);
			this.label1.TabIndex = 18;
			this.label1.Text = "예약";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl예약
			// 
			this.pnl예약.Controls.Add(this.rdo재고);
			this.pnl예약.Controls.Add(this.rdo재고_발주);
			this.pnl예약.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnl예약.Location = new System.Drawing.Point(101, 1);
			this.pnl예약.Margin = new System.Windows.Forms.Padding(1);
			this.pnl예약.Name = "pnl예약";
			this.pnl예약.Size = new System.Drawing.Size(284, 33);
			this.pnl예약.TabIndex = 19;
			// 
			// spc라인
			// 
			this.spc라인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spc라인.Location = new System.Drawing.Point(0, 80);
			this.spc라인.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.spc라인.Name = "spc라인";
			this.spc라인.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spc라인.Panel1
			// 
			this.spc라인.Panel1.Controls.Add(this.grd라인);
			// 
			// spc라인.Panel2
			// 
			this.spc라인.Panel2.Controls.Add(this.tabH);
			this.spc라인.Size = new System.Drawing.Size(1835, 804);
			this.spc라인.SplitterDistance = 558;
			this.spc라인.TabIndex = 1;
			// 
			// btn일괄예약
			// 
			this.btn일괄예약.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn일괄예약.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(121)))), ((int)(((byte)(197)))));
			this.btn일괄예약.FlatAppearance.BorderSize = 0;
			this.btn일괄예약.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn일괄예약.ForeColor = System.Drawing.Color.White;
			this.btn일괄예약.Location = new System.Drawing.Point(1510, 10);
			this.btn일괄예약.Name = "btn일괄예약";
			this.btn일괄예약.Size = new System.Drawing.Size(70, 26);
			this.btn일괄예약.TabIndex = 21;
			this.btn일괄예약.Text = "일괄예약";
			this.btn일괄예약.UseVisualStyleBackColor = false;
			// 
			// btn저장
			// 
			this.btn저장.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn저장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(121)))), ((int)(((byte)(197)))));
			this.btn저장.FlatAppearance.BorderSize = 0;
			this.btn저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn저장.ForeColor = System.Drawing.Color.White;
			this.btn저장.Location = new System.Drawing.Point(1682, 10);
			this.btn저장.Name = "btn저장";
			this.btn저장.Size = new System.Drawing.Size(70, 26);
			this.btn저장.TabIndex = 22;
			this.btn저장.Text = "저장";
			this.btn저장.UseVisualStyleBackColor = false;
			// 
			// btn취소
			// 
			this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn취소.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(121)))), ((int)(((byte)(197)))));
			this.btn취소.FlatAppearance.BorderSize = 0;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.ForeColor = System.Drawing.Color.White;
			this.btn취소.Location = new System.Drawing.Point(1760, 10);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(70, 26);
			this.btn취소.TabIndex = 23;
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// btn초기화
			// 
			this.btn초기화.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn초기화.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(121)))), ((int)(((byte)(197)))));
			this.btn초기화.FlatAppearance.BorderSize = 0;
			this.btn초기화.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn초기화.ForeColor = System.Drawing.Color.White;
			this.btn초기화.Location = new System.Drawing.Point(1588, 10);
			this.btn초기화.Name = "btn초기화";
			this.btn초기화.Size = new System.Drawing.Size(70, 26);
			this.btn초기화.TabIndex = 24;
			this.btn초기화.Text = "초기화";
			this.btn초기화.UseVisualStyleBackColor = false;
			// 
			// H_CZ_STOCK_BOOK
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(1847, 943);
			this.Controls.Add(this.btn초기화);
			this.Controls.Add(this.btn취소);
			this.Controls.Add(this.btn일괄예약);
			this.Controls.Add(this.btn저장);
			this.Controls.Add(this.lay메인);
			this.Controls.Add(this.btn새로고침);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "H_CZ_STOCK_BOOK";
			this.Padding = new System.Windows.Forms.Padding(6, 53, 6, 6);
			this.ShowInTaskbar = false;
			this.Text = "H_CZ_STOCK_BOOK";
			this.TitleText = "재고 예약";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo재고)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo재고_발주)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grd라인)).EndInit();
			this.tabH.ResumeLayout(false);
			this.tabBOOK.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd출고예약)).EndInit();
			this.tabHOLD.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd발주예약)).EndInit();
			this.tabNONGR.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd재고발주)).EndInit();
			this.lay메인.ResumeLayout(false);
			this.lay검색.ResumeLayout(false);
			this.pnl옵션.ResumeLayout(false);
			this.pnl옵션.PerformLayout();
			this.panelExt2.ResumeLayout(false);
			this.panelExt2.PerformLayout();
			this.pnl예약.ResumeLayout(false);
			this.pnl예약.PerformLayout();
			this.spc라인.Panel1.ResumeLayout(false);
			this.spc라인.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spc라인)).EndInit();
			this.spc라인.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private Dass.FlexGrid.FlexGrid grd라인;
		private Duzon.Common.Controls.TabControlExt tabH;
		private System.Windows.Forms.TabPage tabBOOK;
		private System.Windows.Forms.TabPage tabNONGR;
		private Dass.FlexGrid.FlexGrid grd출고예약;
		private Dass.FlexGrid.FlexGrid grd재고발주;
		private System.Windows.Forms.TabPage tabHOLD;
		private Dass.FlexGrid.FlexGrid grd발주예약;
		private Duzon.Common.Controls.RadioButtonExt rdo재고;
		private Duzon.Common.Controls.RadioButtonExt rdo재고_발주;
		private System.Windows.Forms.Button btn새로고침;
		private System.Windows.Forms.TableLayoutPanel lay메인;
		private System.Windows.Forms.TableLayoutPanel lay검색;
		private System.Windows.Forms.SplitContainer spc라인;
		private System.Windows.Forms.Label label1;
		private Duzon.Common.Controls.PanelExt pnl예약;
		private Duzon.Common.Controls.PanelExt panelExt2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btn일괄예약;
		private System.Windows.Forms.Button btn저장;
		private System.Windows.Forms.Button btn취소;
		private System.Windows.Forms.Button btn초기화;
		private Duzon.Common.BpControls.BpCodeTextBox ctx담당자;
		private System.Windows.Forms.Label label3;
		private Duzon.Common.Controls.PanelExt pnl옵션;
		private System.Windows.Forms.Label label4;
		private Duzon.Common.Controls.CheckBoxExt chk가용재고;
	}
}
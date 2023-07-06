namespace cz
{
	partial class P_CZ_PR_ITEM_REPLACE_REG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_ITEM_REPLACE_REG));
			this.oneS = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
			this.rdo입고 = new Duzon.Common.Controls.RadioButtonExt();
			this.rdo반품 = new Duzon.Common.Controls.RadioButtonExt();
			this.labelExt3 = new Duzon.Common.Controls.LabelExt();
			this.pnl작성일자 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo일자 = new Duzon.Common.Controls.DropDownComboBox();
			this.dtp일자 = new Duzon.Common.Controls.PeriodPicker();
			this.pnl파일번호 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo번호 = new Duzon.Common.Controls.DropDownComboBox();
			this.txt번호 = new Duzon.Common.Controls.TextBoxExt();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt품목명 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo품목코드 = new Duzon.Common.Controls.DropDownComboBox();
			this.txt품목코드 = new Duzon.Common.Controls.TextBoxExt();
			this.pnl매출처 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx거래처 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl거래처 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt비고 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt7 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx창고 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.flexS = new Dass.FlexGrid.FlexGrid(this.components);
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tabControlExt1 = new Duzon.Common.Controls.TabControlExt();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.flexE = new Dass.FlexGrid.FlexGrid(this.components);
			this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.grdSearch = new Dass.FlexGrid.FlexGrid(this.components);
			this.imagePanel2 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.mDataArea.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rdo입고)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo반품)).BeginInit();
			this.pnl작성일자.SuspendLayout();
			this.pnl파일번호.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.pnl매출처.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl8.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.flexS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tabControlExt1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.flexE)).BeginInit();
			this.imagePanel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdSearch)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.splitContainer2);
			this.mDataArea.Size = new System.Drawing.Size(1300, 610);
			// 
			// oneS
			// 
			this.oneS.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneS.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem3,
            this.oneGridItem2});
			this.oneS.Location = new System.Drawing.Point(3, 3);
			this.oneS.Name = "oneS";
			this.oneS.Size = new System.Drawing.Size(1294, 85);
			this.oneS.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl6);
			this.oneGridItem1.Controls.Add(this.pnl작성일자);
			this.oneGridItem1.Controls.Add(this.pnl파일번호);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1284, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl6
			// 
			this.bpPanelControl6.Controls.Add(this.rdo입고);
			this.bpPanelControl6.Controls.Add(this.rdo반품);
			this.bpPanelControl6.Controls.Add(this.labelExt3);
			this.bpPanelControl6.Location = new System.Drawing.Point(626, 1);
			this.bpPanelControl6.Name = "bpPanelControl6";
			this.bpPanelControl6.Size = new System.Drawing.Size(310, 23);
			this.bpPanelControl6.TabIndex = 19;
			this.bpPanelControl6.Text = "bpPanelControl5";
			// 
			// rdo입고
			// 
			this.rdo입고.Checked = true;
			this.rdo입고.Location = new System.Drawing.Point(88, 0);
			this.rdo입고.Name = "rdo입고";
			this.rdo입고.Size = new System.Drawing.Size(60, 24);
			this.rdo입고.TabIndex = 7;
			this.rdo입고.TabStop = true;
			this.rdo입고.Tag = "";
			this.rdo입고.Text = "입고";
			this.rdo입고.TextDD = null;
			this.rdo입고.UseKeyEnter = true;
			this.rdo입고.UseVisualStyleBackColor = true;
			// 
			// rdo반품
			// 
			this.rdo반품.Location = new System.Drawing.Point(148, 0);
			this.rdo반품.Name = "rdo반품";
			this.rdo반품.Size = new System.Drawing.Size(60, 24);
			this.rdo반품.TabIndex = 5;
			this.rdo반품.TabStop = true;
			this.rdo반품.Tag = "";
			this.rdo반품.Text = "반품";
			this.rdo반품.TextDD = null;
			this.rdo반품.UseKeyEnter = true;
			this.rdo반품.UseVisualStyleBackColor = true;
			// 
			// labelExt3
			// 
			this.labelExt3.Location = new System.Drawing.Point(17, 4);
			this.labelExt3.Name = "labelExt3";
			this.labelExt3.Size = new System.Drawing.Size(65, 16);
			this.labelExt3.TabIndex = 3;
			this.labelExt3.Text = "구분";
			this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl작성일자
			// 
			this.pnl작성일자.Controls.Add(this.cbo일자);
			this.pnl작성일자.Controls.Add(this.dtp일자);
			this.pnl작성일자.Location = new System.Drawing.Point(314, 1);
			this.pnl작성일자.Name = "pnl작성일자";
			this.pnl작성일자.Size = new System.Drawing.Size(310, 23);
			this.pnl작성일자.TabIndex = 13;
			this.pnl작성일자.Text = "bpPanelControl2";
			// 
			// cbo일자
			// 
			this.cbo일자.AutoDropDown = true;
			this.cbo일자.DisplayMember = "NAME";
			this.cbo일자.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo일자.FormattingEnabled = true;
			this.cbo일자.ItemHeight = 12;
			this.cbo일자.Location = new System.Drawing.Point(5, 1);
			this.cbo일자.Name = "cbo일자";
			this.cbo일자.Size = new System.Drawing.Size(77, 20);
			this.cbo일자.TabIndex = 13;
			this.cbo일자.Tag = "";
			this.cbo일자.ValueMember = "CODE";
			// 
			// dtp일자
			// 
			this.dtp일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp일자.Location = new System.Drawing.Point(84, 1);
			this.dtp일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp일자.Name = "dtp일자";
			this.dtp일자.Size = new System.Drawing.Size(185, 21);
			this.dtp일자.TabIndex = 2;
			// 
			// pnl파일번호
			// 
			this.pnl파일번호.Controls.Add(this.cbo번호);
			this.pnl파일번호.Controls.Add(this.txt번호);
			this.pnl파일번호.Location = new System.Drawing.Point(2, 1);
			this.pnl파일번호.Name = "pnl파일번호";
			this.pnl파일번호.Size = new System.Drawing.Size(310, 23);
			this.pnl파일번호.TabIndex = 12;
			this.pnl파일번호.Text = "bpPanelControl1";
			// 
			// cbo번호
			// 
			this.cbo번호.AutoDropDown = true;
			this.cbo번호.DisplayMember = "NAME";
			this.cbo번호.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo번호.FormattingEnabled = true;
			this.cbo번호.ItemHeight = 12;
			this.cbo번호.Location = new System.Drawing.Point(5, 1);
			this.cbo번호.Name = "cbo번호";
			this.cbo번호.Size = new System.Drawing.Size(77, 20);
			this.cbo번호.TabIndex = 12;
			this.cbo번호.Tag = "";
			this.cbo번호.ValueMember = "CODE";
			// 
			// txt번호
			// 
			this.txt번호.BackColor = System.Drawing.Color.White;
			this.txt번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt번호.Location = new System.Drawing.Point(84, 1);
			this.txt번호.Name = "txt번호";
			this.txt번호.Size = new System.Drawing.Size(225, 21);
			this.txt번호.TabIndex = 2;
			this.txt번호.Tag = "";
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl2);
			this.oneGridItem3.Controls.Add(this.bpPanelControl1);
			this.oneGridItem3.Controls.Add(this.pnl매출처);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(1284, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 1;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.txt품목명);
			this.bpPanelControl2.Controls.Add(this.labelExt2);
			this.bpPanelControl2.Location = new System.Drawing.Point(626, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(310, 23);
			this.bpPanelControl2.TabIndex = 24;
			this.bpPanelControl2.Text = "bpPanelControl7";
			// 
			// txt품목명
			// 
			this.txt품목명.BackColor = System.Drawing.Color.White;
			this.txt품목명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt품목명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt품목명.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.txt품목명.Location = new System.Drawing.Point(84, 1);
			this.txt품목명.Name = "txt품목명";
			this.txt품목명.Size = new System.Drawing.Size(225, 21);
			this.txt품목명.TabIndex = 2;
			this.txt품목명.Tag = "NO_PO";
			// 
			// labelExt2
			// 
			this.labelExt2.Location = new System.Drawing.Point(17, 4);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Size = new System.Drawing.Size(65, 16);
			this.labelExt2.TabIndex = 1;
			this.labelExt2.Text = "품목명";
			this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.cbo품목코드);
			this.bpPanelControl1.Controls.Add(this.txt품목코드);
			this.bpPanelControl1.Location = new System.Drawing.Point(314, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(310, 23);
			this.bpPanelControl1.TabIndex = 23;
			this.bpPanelControl1.Text = "bpPanelControl7";
			// 
			// cbo품목코드
			// 
			this.cbo품목코드.AutoDropDown = true;
			this.cbo품목코드.DisplayMember = "NAME";
			this.cbo품목코드.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo품목코드.FormattingEnabled = true;
			this.cbo품목코드.ItemHeight = 12;
			this.cbo품목코드.Location = new System.Drawing.Point(5, 1);
			this.cbo품목코드.Name = "cbo품목코드";
			this.cbo품목코드.Size = new System.Drawing.Size(77, 20);
			this.cbo품목코드.TabIndex = 14;
			this.cbo품목코드.Tag = "";
			this.cbo품목코드.ValueMember = "CODE";
			// 
			// txt품목코드
			// 
			this.txt품목코드.BackColor = System.Drawing.Color.White;
			this.txt품목코드.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt품목코드.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt품목코드.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.txt품목코드.Location = new System.Drawing.Point(84, 1);
			this.txt품목코드.Name = "txt품목코드";
			this.txt품목코드.Size = new System.Drawing.Size(225, 21);
			this.txt품목코드.TabIndex = 2;
			this.txt품목코드.Tag = "NO_PO";
			// 
			// pnl매출처
			// 
			this.pnl매출처.Controls.Add(this.ctx거래처);
			this.pnl매출처.Controls.Add(this.lbl거래처);
			this.pnl매출처.Location = new System.Drawing.Point(2, 1);
			this.pnl매출처.Name = "pnl매출처";
			this.pnl매출처.Size = new System.Drawing.Size(310, 23);
			this.pnl매출처.TabIndex = 22;
			this.pnl매출처.Text = "bpPanelControl4";
			// 
			// ctx거래처
			// 
			this.ctx거래처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
			this.ctx거래처.Location = new System.Drawing.Point(84, 1);
			this.ctx거래처.Name = "ctx거래처";
			this.ctx거래처.Size = new System.Drawing.Size(226, 21);
			this.ctx거래처.TabIndex = 1;
			this.ctx거래처.TabStop = false;
			this.ctx거래처.Tag = "CD_PARTNER;LN_PARTNER";
			// 
			// lbl거래처
			// 
			this.lbl거래처.Location = new System.Drawing.Point(17, 4);
			this.lbl거래처.Name = "lbl거래처";
			this.lbl거래처.Size = new System.Drawing.Size(65, 16);
			this.lbl거래처.TabIndex = 0;
			this.lbl거래처.Text = "매입처";
			this.lbl거래처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl8);
			this.oneGridItem2.Controls.Add(this.bpPanelControl3);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1284, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 2;
			// 
			// bpPanelControl8
			// 
			this.bpPanelControl8.Controls.Add(this.txt비고);
			this.bpPanelControl8.Controls.Add(this.labelExt7);
			this.bpPanelControl8.Location = new System.Drawing.Point(314, 1);
			this.bpPanelControl8.Name = "bpPanelControl8";
			this.bpPanelControl8.Size = new System.Drawing.Size(798, 23);
			this.bpPanelControl8.TabIndex = 24;
			this.bpPanelControl8.Text = "bpPanelControl8";
			// 
			// txt비고
			// 
			this.txt비고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt비고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt비고.Location = new System.Drawing.Point(84, 1);
			this.txt비고.Name = "txt비고";
			this.txt비고.Size = new System.Drawing.Size(537, 21);
			this.txt비고.TabIndex = 9;
			this.txt비고.Tag = "DELIVERY_TIME";
			// 
			// labelExt7
			// 
			this.labelExt7.Location = new System.Drawing.Point(17, 4);
			this.labelExt7.Name = "labelExt7";
			this.labelExt7.Size = new System.Drawing.Size(65, 16);
			this.labelExt7.TabIndex = 1;
			this.labelExt7.Text = "비고";
			this.labelExt7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.ctx창고);
			this.bpPanelControl3.Controls.Add(this.labelExt1);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(310, 23);
			this.bpPanelControl3.TabIndex = 23;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// ctx창고
			// 
			this.ctx창고.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB;
			this.ctx창고.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx창고.Location = new System.Drawing.Point(84, 1);
			this.ctx창고.Name = "ctx창고";
			this.ctx창고.Size = new System.Drawing.Size(226, 21);
			this.ctx창고.TabIndex = 10;
			this.ctx창고.TabStop = false;
			this.ctx창고.Tag = "";
			// 
			// labelExt1
			// 
			this.labelExt1.Location = new System.Drawing.Point(17, 4);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(65, 16);
			this.labelExt1.TabIndex = 1;
			this.labelExt1.Text = "창고";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// flexS
			// 
			this.flexS.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.flexS.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.flexS.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.flexS.AutoResize = false;
			this.flexS.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.flexS.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexS.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.flexS.EnabledHeaderCheck = true;
			this.flexS.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.flexS.Location = new System.Drawing.Point(3, 94);
			this.flexS.Name = "flexS";
			this.flexS.Rows.Count = 1;
			this.flexS.Rows.DefaultSize = 20;
			this.flexS.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.flexS.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.flexS.ShowSort = false;
			this.flexS.Size = new System.Drawing.Size(1294, 235);
			this.flexS.StyleInfo = resources.GetString("flexS.StyleInfo");
			this.flexS.TabIndex = 2;
			this.flexS.UseGridCalculator = true;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel2);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.tabControlExt1);
			this.splitContainer2.Size = new System.Drawing.Size(1300, 610);
			this.splitContainer2.SplitterDistance = 332;
			this.splitContainer2.TabIndex = 1;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.flexS, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.oneS, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1300, 332);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// tabControlExt1
			// 
			this.tabControlExt1.Controls.Add(this.tabPage1);
			this.tabControlExt1.Controls.Add(this.tabPage2);
			this.tabControlExt1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlExt1.ItemSize = new System.Drawing.Size(120, 20);
			this.tabControlExt1.Location = new System.Drawing.Point(0, 0);
			this.tabControlExt1.Name = "tabControlExt1";
			this.tabControlExt1.SelectedIndex = 0;
			this.tabControlExt1.Size = new System.Drawing.Size(1300, 274);
			this.tabControlExt1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControlExt1.TabIndex = 10;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.tableLayoutPanel3);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1292, 246);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "추가/삭제";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.tableLayoutPanel1);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1292, 246);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "조회";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.flexE, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.imagePanel1, 0, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(1286, 240);
			this.tableLayoutPanel3.TabIndex = 1;
			// 
			// flexE
			// 
			this.flexE.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.flexE.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.flexE.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.flexE.AutoResize = false;
			this.flexE.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.flexE.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexE.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.flexE.EnabledHeaderCheck = true;
			this.flexE.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.flexE.Location = new System.Drawing.Point(3, 36);
			this.flexE.Name = "flexE";
			this.flexE.Rows.Count = 1;
			this.flexE.Rows.DefaultSize = 20;
			this.flexE.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.flexE.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.flexE.ShowSort = false;
			this.flexE.Size = new System.Drawing.Size(1280, 201);
			this.flexE.StyleInfo = resources.GetString("flexE.StyleInfo");
			this.flexE.TabIndex = 9;
			this.flexE.UseGridCalculator = true;
			// 
			// imagePanel1
			// 
			this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel1.Controls.Add(this.btn삭제);
			this.imagePanel1.Controls.Add(this.btn추가);
			this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.imagePanel1.LeftImage = null;
			this.imagePanel1.Location = new System.Drawing.Point(3, 3);
			this.imagePanel1.Name = "imagePanel1";
			this.imagePanel1.Padding = new System.Windows.Forms.Padding(1);
			this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel1.PatternImage = null;
			this.imagePanel1.RightImage = null;
			this.imagePanel1.Size = new System.Drawing.Size(1280, 27);
			this.imagePanel1.TabIndex = 8;
			this.imagePanel1.TitleText = "상세정보";
			// 
			// btn삭제
			// 
			this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn삭제.BackColor = System.Drawing.Color.Transparent;
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(1201, 4);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(70, 19);
			this.btn삭제.TabIndex = 7;
			this.btn삭제.TabStop = false;
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// btn추가
			// 
			this.btn추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn추가.BackColor = System.Drawing.Color.Transparent;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.Location = new System.Drawing.Point(1125, 4);
			this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn추가.Name = "btn추가";
			this.btn추가.Size = new System.Drawing.Size(70, 19);
			this.btn추가.TabIndex = 6;
			this.btn추가.TabStop = false;
			this.btn추가.Text = "추가";
			this.btn추가.UseVisualStyleBackColor = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.grdSearch, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.imagePanel2, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1286, 240);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// grdSearch
			// 
			this.grdSearch.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grdSearch.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grdSearch.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grdSearch.AutoResize = false;
			this.grdSearch.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grdSearch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdSearch.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grdSearch.EnabledHeaderCheck = true;
			this.grdSearch.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grdSearch.Location = new System.Drawing.Point(3, 36);
			this.grdSearch.Name = "grdSearch";
			this.grdSearch.Rows.Count = 1;
			this.grdSearch.Rows.DefaultSize = 20;
			this.grdSearch.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grdSearch.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grdSearch.ShowSort = false;
			this.grdSearch.Size = new System.Drawing.Size(1280, 201);
			this.grdSearch.StyleInfo = resources.GetString("grdSearch.StyleInfo");
			this.grdSearch.TabIndex = 9;
			this.grdSearch.UseGridCalculator = true;
			// 
			// imagePanel2
			// 
			this.imagePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.imagePanel2.LeftImage = null;
			this.imagePanel2.Location = new System.Drawing.Point(3, 3);
			this.imagePanel2.Name = "imagePanel2";
			this.imagePanel2.Padding = new System.Windows.Forms.Padding(1);
			this.imagePanel2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel2.PatternImage = null;
			this.imagePanel2.RightImage = null;
			this.imagePanel2.Size = new System.Drawing.Size(1280, 27);
			this.imagePanel2.TabIndex = 8;
			this.imagePanel2.TitleText = "상세정보";
			// 
			// P_CZ_PR_ITEM_REPLACE_REG
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_PR_ITEM_REPLACE_REG";
			this.Size = new System.Drawing.Size(1300, 650);
			this.TitleText = "P_CZ_PR_ITEM_REPLACE_REG";
			this.mDataArea.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl6.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.rdo입고)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rdo반품)).EndInit();
			this.pnl작성일자.ResumeLayout(false);
			this.pnl파일번호.ResumeLayout(false);
			this.pnl파일번호.PerformLayout();
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl2.PerformLayout();
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			this.pnl매출처.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl8.ResumeLayout(false);
			this.bpPanelControl8.PerformLayout();
			this.bpPanelControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.flexS)).EndInit();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tabControlExt1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.flexE)).EndInit();
			this.imagePanel1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdSearch)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Duzon.Erpiu.Windows.OneControls.OneGrid oneS;
		private Dass.FlexGrid.FlexGrid flexS;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl pnl파일번호;
		private Duzon.Common.Controls.DropDownComboBox cbo번호;
		private Duzon.Common.Controls.TextBoxExt txt번호;
		private Duzon.Common.BpControls.BpPanelControl pnl작성일자;
		private Duzon.Common.Controls.PeriodPicker dtp일자;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
		private Duzon.Common.Controls.RadioButtonExt rdo입고;
		private Duzon.Common.Controls.RadioButtonExt rdo반품;
		private Duzon.Common.Controls.LabelExt labelExt3;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.TextBoxExt txt품목명;
		private Duzon.Common.Controls.LabelExt labelExt2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.TextBoxExt txt품목코드;
		private Duzon.Common.BpControls.BpPanelControl pnl매출처;
		private Duzon.Common.BpControls.BpCodeTextBox ctx거래처;
		private Duzon.Common.Controls.LabelExt lbl거래처;
		private Duzon.Common.Controls.DropDownComboBox cbo일자;
		private Duzon.Common.Controls.DropDownComboBox cbo품목코드;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.BpControls.BpCodeTextBox ctx창고;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl8;
		private Duzon.Common.Controls.TextBoxExt txt비고;
		private Duzon.Common.Controls.LabelExt labelExt7;
		private Duzon.Common.Controls.TabControlExt tabControlExt1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private Dass.FlexGrid.FlexGrid flexE;
		private Duzon.Common.Controls.ImagePanel imagePanel1;
		private Duzon.Common.Controls.RoundedButton btn삭제;
		private Duzon.Common.Controls.RoundedButton btn추가;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Dass.FlexGrid.FlexGrid grdSearch;
		private Duzon.Common.Controls.ImagePanel imagePanel2;
	}
}
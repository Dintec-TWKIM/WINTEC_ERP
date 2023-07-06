namespace cz
{
	partial class P_CZ_PR_LINK_MES_REG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_LINK_MES_REG));
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPnl처리여부 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo처리여부 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl처리여부 = new Duzon.Common.Controls.LabelExt();
            this.bpPnl기간 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp기간 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl기간 = new Duzon.Common.Controls.LabelExt();
            this.bpPnl공장 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl공장 = new Duzon.Common.Controls.LabelExt();
            this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.btn입고창고적용 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.lbl입고창고 = new Duzon.Common.Controls.LabelExt();
            this.ctx입고창고 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx품목To = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl품목To = new Duzon.Common.Controls.LabelExt();
            this.bpPnl품목 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx품목From = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl품목From = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt작업지시번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl작업지시번호 = new Duzon.Common.Controls.LabelExt();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn연동삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn연동적용 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn표준경로일괄적용 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn이력조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btnMES10 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl작업장 = new Duzon.Common.Controls.LabelExt();
            this.ctx작업장 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.mDataArea.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPnl처리여부.SuspendLayout();
            this.bpPnl기간.SuspendLayout();
            this.bpPnl공장.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.bpPnl품목.SuspendLayout();
            this.oneGridItem3.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.bpPanelControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(1090, 756);
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
            this.oneGrid1.Size = new System.Drawing.Size(1084, 85);
            this.oneGrid1.TabIndex = 73;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPnl처리여부);
            this.oneGridItem1.Controls.Add(this.bpPnl기간);
            this.oneGridItem1.Controls.Add(this.bpPnl공장);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(1074, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPnl처리여부
            // 
            this.bpPnl처리여부.Controls.Add(this.cbo처리여부);
            this.bpPnl처리여부.Controls.Add(this.lbl처리여부);
            this.bpPnl처리여부.Location = new System.Drawing.Point(590, 1);
            this.bpPnl처리여부.Name = "bpPnl처리여부";
            this.bpPnl처리여부.Size = new System.Drawing.Size(292, 23);
            this.bpPnl처리여부.TabIndex = 2;
            // 
            // cbo처리여부
            // 
            this.cbo처리여부.AutoDropDown = true;
            this.cbo처리여부.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo처리여부.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo처리여부.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo처리여부.ItemHeight = 12;
            this.cbo처리여부.Location = new System.Drawing.Point(106, 0);
            this.cbo처리여부.Name = "cbo처리여부";
            this.cbo처리여부.Size = new System.Drawing.Size(186, 20);
            this.cbo처리여부.TabIndex = 3;
            this.cbo처리여부.UseKeyF3 = false;
            // 
            // lbl처리여부
            // 
            this.lbl처리여부.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl처리여부.Location = new System.Drawing.Point(0, 0);
            this.lbl처리여부.Name = "lbl처리여부";
            this.lbl처리여부.Size = new System.Drawing.Size(100, 23);
            this.lbl처리여부.TabIndex = 2;
            this.lbl처리여부.Text = "처리여부";
            this.lbl처리여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPnl기간
            // 
            this.bpPnl기간.Controls.Add(this.dtp기간);
            this.bpPnl기간.Controls.Add(this.lbl기간);
            this.bpPnl기간.Location = new System.Drawing.Point(296, 1);
            this.bpPnl기간.Name = "bpPnl기간";
            this.bpPnl기간.Size = new System.Drawing.Size(292, 23);
            this.bpPnl기간.TabIndex = 1;
            // 
            // dtp기간
            // 
            this.dtp기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp기간.IsNecessaryCondition = true;
            this.dtp기간.Location = new System.Drawing.Point(107, 0);
            this.dtp기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp기간.Name = "dtp기간";
            this.dtp기간.Size = new System.Drawing.Size(185, 21);
            this.dtp기간.TabIndex = 2;
            // 
            // lbl기간
            // 
            this.lbl기간.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl기간.Location = new System.Drawing.Point(0, 0);
            this.lbl기간.Name = "lbl기간";
            this.lbl기간.Size = new System.Drawing.Size(100, 23);
            this.lbl기간.TabIndex = 1;
            this.lbl기간.Text = "기간";
            this.lbl기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPnl공장
            // 
            this.bpPnl공장.Controls.Add(this.lbl공장);
            this.bpPnl공장.Controls.Add(this.cbo공장);
            this.bpPnl공장.Location = new System.Drawing.Point(2, 1);
            this.bpPnl공장.Name = "bpPnl공장";
            this.bpPnl공장.Size = new System.Drawing.Size(292, 23);
            this.bpPnl공장.TabIndex = 0;
            // 
            // lbl공장
            // 
            this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl공장.Location = new System.Drawing.Point(0, 0);
            this.lbl공장.Name = "lbl공장";
            this.lbl공장.Size = new System.Drawing.Size(100, 23);
            this.lbl공장.TabIndex = 0;
            this.lbl공장.Text = "공장";
            this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo공장
            // 
            this.cbo공장.AutoDropDown = true;
            this.cbo공장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo공장.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo공장.ItemHeight = 12;
            this.cbo공장.Location = new System.Drawing.Point(106, 0);
            this.cbo공장.Name = "cbo공장";
            this.cbo공장.Size = new System.Drawing.Size(186, 20);
            this.cbo공장.TabIndex = 1;
            this.cbo공장.UseKeyF3 = false;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl2);
            this.oneGridItem2.Controls.Add(this.bpPanelControl1);
            this.oneGridItem2.Controls.Add(this.bpPnl품목);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(1074, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.btn입고창고적용);
            this.bpPanelControl2.Controls.Add(this.lbl입고창고);
            this.bpPanelControl2.Controls.Add(this.ctx입고창고);
            this.bpPanelControl2.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 4;
            // 
            // btn입고창고적용
            // 
            this.btn입고창고적용.BackColor = System.Drawing.Color.Transparent;
            this.btn입고창고적용.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn입고창고적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn입고창고적용.Location = new System.Drawing.Point(239, 1);
            this.btn입고창고적용.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn입고창고적용.Name = "btn입고창고적용";
            this.btn입고창고적용.Size = new System.Drawing.Size(50, 19);
            this.btn입고창고적용.TabIndex = 6;
            this.btn입고창고적용.TabStop = false;
            this.btn입고창고적용.Text = "적용";
            this.btn입고창고적용.UseVisualStyleBackColor = false;
            // 
            // lbl입고창고
            // 
            this.lbl입고창고.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl입고창고.Location = new System.Drawing.Point(0, 0);
            this.lbl입고창고.Name = "lbl입고창고";
            this.lbl입고창고.Size = new System.Drawing.Size(100, 23);
            this.lbl입고창고.TabIndex = 7;
            this.lbl입고창고.Text = "입고창고";
            this.lbl입고창고.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx입고창고
            // 
            this.ctx입고창고.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB;
            this.ctx입고창고.Location = new System.Drawing.Point(106, 0);
            this.ctx입고창고.Name = "ctx입고창고";
            this.ctx입고창고.Size = new System.Drawing.Size(131, 21);
            this.ctx입고창고.TabIndex = 5;
            this.ctx입고창고.TabStop = false;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.ctx품목To);
            this.bpPanelControl1.Controls.Add(this.lbl품목To);
            this.bpPanelControl1.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 3;
            // 
            // ctx품목To
            // 
            this.ctx품목To.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx품목To.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.ctx품목To.Location = new System.Drawing.Point(106, 0);
            this.ctx품목To.Name = "ctx품목To";
            this.ctx품목To.Size = new System.Drawing.Size(186, 21);
            this.ctx품목To.TabIndex = 4;
            this.ctx품목To.TabStop = false;
            // 
            // lbl품목To
            // 
            this.lbl품목To.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl품목To.Location = new System.Drawing.Point(0, 0);
            this.lbl품목To.Name = "lbl품목To";
            this.lbl품목To.Size = new System.Drawing.Size(100, 23);
            this.lbl품목To.TabIndex = 2;
            this.lbl품목To.Text = "품목To";
            this.lbl품목To.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPnl품목
            // 
            this.bpPnl품목.Controls.Add(this.ctx품목From);
            this.bpPnl품목.Controls.Add(this.lbl품목From);
            this.bpPnl품목.Location = new System.Drawing.Point(2, 1);
            this.bpPnl품목.Name = "bpPnl품목";
            this.bpPnl품목.Size = new System.Drawing.Size(292, 23);
            this.bpPnl품목.TabIndex = 0;
            // 
            // ctx품목From
            // 
            this.ctx품목From.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx품목From.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.ctx품목From.Location = new System.Drawing.Point(106, 0);
            this.ctx품목From.Name = "ctx품목From";
            this.ctx품목From.Size = new System.Drawing.Size(186, 21);
            this.ctx품목From.TabIndex = 3;
            this.ctx품목From.TabStop = false;
            // 
            // lbl품목From
            // 
            this.lbl품목From.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl품목From.Location = new System.Drawing.Point(0, 0);
            this.lbl품목From.Name = "lbl품목From";
            this.lbl품목From.Size = new System.Drawing.Size(100, 23);
            this.lbl품목From.TabIndex = 2;
            this.lbl품목From.Text = "품목From";
            this.lbl품목From.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl4);
            this.oneGridItem3.Controls.Add(this.bpPanelControl3);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(1074, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 2;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.txt작업지시번호);
            this.bpPanelControl3.Controls.Add(this.lbl작업지시번호);
            this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 1;
            // 
            // txt작업지시번호
            // 
            this.txt작업지시번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt작업지시번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt작업지시번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt작업지시번호.Location = new System.Drawing.Point(106, 0);
            this.txt작업지시번호.Name = "txt작업지시번호";
            this.txt작업지시번호.Size = new System.Drawing.Size(186, 21);
            this.txt작업지시번호.TabIndex = 1;
            // 
            // lbl작업지시번호
            // 
            this.lbl작업지시번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl작업지시번호.Location = new System.Drawing.Point(0, 0);
            this.lbl작업지시번호.Name = "lbl작업지시번호";
            this.lbl작업지시번호.Size = new System.Drawing.Size(100, 23);
            this.lbl작업지시번호.TabIndex = 0;
            this.lbl작업지시번호.Text = "작업지시번호";
            this.lbl작업지시번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btn연동삭제);
            this.flowLayoutPanel1.Controls.Add(this.btn연동적용);
            this.flowLayoutPanel1.Controls.Add(this.btn표준경로일괄적용);
            this.flowLayoutPanel1.Controls.Add(this.btn이력조회);
            this.flowLayoutPanel1.Controls.Add(this.btnMES10);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(628, 7);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(463, 23);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // btn연동삭제
            // 
            this.btn연동삭제.BackColor = System.Drawing.Color.Transparent;
            this.btn연동삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn연동삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn연동삭제.Location = new System.Drawing.Point(381, 3);
            this.btn연동삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn연동삭제.Name = "btn연동삭제";
            this.btn연동삭제.Size = new System.Drawing.Size(79, 19);
            this.btn연동삭제.TabIndex = 1;
            this.btn연동삭제.TabStop = false;
            this.btn연동삭제.Text = "연동삭제";
            this.btn연동삭제.UseVisualStyleBackColor = false;
            // 
            // btn연동적용
            // 
            this.btn연동적용.BackColor = System.Drawing.Color.Transparent;
            this.btn연동적용.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn연동적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn연동적용.Location = new System.Drawing.Point(299, 3);
            this.btn연동적용.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btn연동적용.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn연동적용.Name = "btn연동적용";
            this.btn연동적용.Size = new System.Drawing.Size(79, 19);
            this.btn연동적용.TabIndex = 0;
            this.btn연동적용.TabStop = false;
            this.btn연동적용.Text = "연동적용";
            this.btn연동적용.UseVisualStyleBackColor = false;
            // 
            // btn표준경로일괄적용
            // 
            this.btn표준경로일괄적용.BackColor = System.Drawing.Color.Transparent;
            this.btn표준경로일괄적용.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn표준경로일괄적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn표준경로일괄적용.Location = new System.Drawing.Point(176, 3);
            this.btn표준경로일괄적용.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btn표준경로일괄적용.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn표준경로일괄적용.Name = "btn표준경로일괄적용";
            this.btn표준경로일괄적용.Size = new System.Drawing.Size(120, 19);
            this.btn표준경로일괄적용.TabIndex = 2;
            this.btn표준경로일괄적용.TabStop = false;
            this.btn표준경로일괄적용.Text = "표준경로 일괄적용";
            this.btn표준경로일괄적용.UseVisualStyleBackColor = false;
            // 
            // btn이력조회
            // 
            this.btn이력조회.BackColor = System.Drawing.Color.Transparent;
            this.btn이력조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn이력조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn이력조회.Location = new System.Drawing.Point(94, 3);
            this.btn이력조회.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btn이력조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn이력조회.Name = "btn이력조회";
            this.btn이력조회.Size = new System.Drawing.Size(79, 19);
            this.btn이력조회.TabIndex = 5;
            this.btn이력조회.TabStop = false;
            this.btn이력조회.Text = "이력조회";
            this.btn이력조회.UseVisualStyleBackColor = false;
            // 
            // btnMES10
            // 
            this.btnMES10.BackColor = System.Drawing.Color.Transparent;
            this.btnMES10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMES10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMES10.Location = new System.Drawing.Point(12, 3);
            this.btnMES10.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btnMES10.MaximumSize = new System.Drawing.Size(0, 19);
            this.btnMES10.Name = "btnMES10";
            this.btnMES10.Size = new System.Drawing.Size(79, 19);
            this.btnMES10.TabIndex = 4;
            this.btnMES10.TabStop = false;
            this.btnMES10.Text = "MES10";
            this.btnMES10.UseVisualStyleBackColor = false;
            this.btnMES10.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1090, 756);
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
            this._flex.Location = new System.Drawing.Point(3, 94);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(1084, 659);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 74;
            this._flex.UseGridCalculator = true;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.ctx작업장);
            this.bpPanelControl4.Controls.Add(this.lbl작업장);
            this.bpPanelControl4.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl4.TabIndex = 2;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // lbl작업장
            // 
            this.lbl작업장.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl작업장.Location = new System.Drawing.Point(0, 0);
            this.lbl작업장.Name = "lbl작업장";
            this.lbl작업장.Size = new System.Drawing.Size(100, 23);
            this.lbl작업장.TabIndex = 0;
            this.lbl작업장.Text = "작업장";
            this.lbl작업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx작업장
            // 
            this.ctx작업장.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_WC_SUB;
            this.ctx작업장.Location = new System.Drawing.Point(106, 0);
            this.ctx작업장.Name = "ctx작업장";
            this.ctx작업장.Size = new System.Drawing.Size(186, 21);
            this.ctx작업장.TabIndex = 1;
            this.ctx작업장.TabStop = false;
            this.ctx작업장.Text = "bpCodeTextBox1";
            // 
            // P_CZ_PR_LINK_MES_REG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.flowLayoutPanel1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_CZ_PR_LINK_MES_REG";
            this.Size = new System.Drawing.Size(1090, 796);
            this.TitleText = "생산MES연동처리";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.mDataArea.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPnl처리여부.ResumeLayout(false);
            this.bpPnl기간.ResumeLayout(false);
            this.bpPnl공장.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPnl품목.ResumeLayout(false);
            this.oneGridItem3.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.bpPanelControl4.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		private Duzon.Common.Controls.DropDownComboBox cbo공장;
		private Duzon.Common.Controls.LabelExt lbl공장;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPnl공장;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private Duzon.Common.Controls.RoundedButton btn연동적용;
		private Duzon.Common.BpControls.BpPanelControl bpPnl기간;
		private Duzon.Common.Controls.PeriodPicker dtp기간;
		private Duzon.Common.Controls.LabelExt lbl기간;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.Controls.RoundedButton btn연동삭제;
		private Duzon.Common.BpControls.BpPanelControl bpPnl처리여부;
		private Duzon.Common.BpControls.BpPanelControl bpPnl품목;
		private Duzon.Common.Controls.DropDownComboBox cbo처리여부;
		private Duzon.Common.Controls.LabelExt lbl처리여부;
		private Duzon.Common.BpControls.BpCodeTextBox ctx품목To;
		private Duzon.Common.BpControls.BpCodeTextBox ctx품목From;
		private Duzon.Common.Controls.LabelExt lbl품목From;
		private Duzon.Common.Controls.RoundedButton btn표준경로일괄적용;
		private Duzon.Common.Controls.RoundedButton btn입고창고적용;
		private Duzon.Common.BpControls.BpCodeTextBox ctx입고창고;
		private Duzon.Common.Controls.RoundedButton btnMES10;
		private Duzon.Common.Controls.RoundedButton btn이력조회;
		#endregion

		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.LabelExt lbl품목To;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.LabelExt lbl입고창고;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Dass.FlexGrid.FlexGrid _flex;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.Controls.TextBoxExt txt작업지시번호;
		private Duzon.Common.Controls.LabelExt lbl작업지시번호;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpCodeTextBox ctx작업장;
        private Duzon.Common.Controls.LabelExt lbl작업장;
    }
}
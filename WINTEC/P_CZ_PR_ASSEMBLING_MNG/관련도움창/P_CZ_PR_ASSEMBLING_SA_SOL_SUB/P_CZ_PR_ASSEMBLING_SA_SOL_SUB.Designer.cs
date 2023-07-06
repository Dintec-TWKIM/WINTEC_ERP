
namespace cz
{
	partial class P_CZ_PR_ASSEMBLING_SA_SOL_SUB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_ASSEMBLING_SA_SOL_SUB));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.chk종결제외 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk출고제외 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk의뢰제외 = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp납기일자 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl납기일자 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.chk판매품목제외 = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.txtID번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lblID번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt수주번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl수주번호 = new Duzon.Common.Controls.LabelExt();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn판매해제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn판매처리 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.txtQR코드스캔 = new Duzon.Common.Controls.TextBoxExt();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flex수주품목 = new Dass.FlexGrid.FlexGrid(this.components);
			this._flex생산품목 = new Dass.FlexGrid.FlexGrid(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex수주품목)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flex생산품목)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 50);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(992, 622);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem2,
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(986, 66);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl4);
			this.oneGridItem2.Controls.Add(this.bpPanelControl3);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(976, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 0;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.flowLayoutPanel2);
			this.bpPanelControl4.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl4.TabIndex = 1;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.chk종결제외);
			this.flowLayoutPanel2.Controls.Add(this.chk출고제외);
			this.flowLayoutPanel2.Controls.Add(this.chk의뢰제외);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(292, 23);
			this.flowLayoutPanel2.TabIndex = 3;
			// 
			// chk종결제외
			// 
			this.chk종결제외.AutoSize = true;
			this.chk종결제외.Checked = true;
			this.chk종결제외.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk종결제외.Location = new System.Drawing.Point(217, 3);
			this.chk종결제외.Name = "chk종결제외";
			this.chk종결제외.Size = new System.Drawing.Size(72, 16);
			this.chk종결제외.TabIndex = 2;
			this.chk종결제외.Text = "종결제외";
			this.chk종결제외.TextDD = null;
			this.chk종결제외.UseVisualStyleBackColor = true;
			// 
			// chk출고제외
			// 
			this.chk출고제외.AutoSize = true;
			this.chk출고제외.Checked = true;
			this.chk출고제외.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk출고제외.Location = new System.Drawing.Point(139, 3);
			this.chk출고제외.Name = "chk출고제외";
			this.chk출고제외.Size = new System.Drawing.Size(72, 16);
			this.chk출고제외.TabIndex = 1;
			this.chk출고제외.Text = "출고제외";
			this.chk출고제외.TextDD = null;
			this.chk출고제외.UseVisualStyleBackColor = true;
			// 
			// chk의뢰제외
			// 
			this.chk의뢰제외.AutoSize = true;
			this.chk의뢰제외.Checked = true;
			this.chk의뢰제외.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk의뢰제외.Location = new System.Drawing.Point(61, 3);
			this.chk의뢰제외.Name = "chk의뢰제외";
			this.chk의뢰제외.Size = new System.Drawing.Size(72, 16);
			this.chk의뢰제외.TabIndex = 0;
			this.chk의뢰제외.Text = "의뢰제외";
			this.chk의뢰제외.TextDD = null;
			this.chk의뢰제외.UseVisualStyleBackColor = true;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.dtp납기일자);
			this.bpPanelControl3.Controls.Add(this.lbl납기일자);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 0;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// dtp납기일자
			// 
			this.dtp납기일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp납기일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp납기일자.IsNecessaryCondition = true;
			this.dtp납기일자.Location = new System.Drawing.Point(107, 0);
			this.dtp납기일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp납기일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp납기일자.Name = "dtp납기일자";
			this.dtp납기일자.Size = new System.Drawing.Size(185, 21);
			this.dtp납기일자.TabIndex = 1;
			// 
			// lbl납기일자
			// 
			this.lbl납기일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl납기일자.Location = new System.Drawing.Point(0, 0);
			this.lbl납기일자.Name = "lbl납기일자";
			this.lbl납기일자.Size = new System.Drawing.Size(100, 23);
			this.lbl납기일자.TabIndex = 0;
			this.lbl납기일자.Text = "납기일자";
			this.lbl납기일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.chk판매품목제외);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(976, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 1;
			// 
			// chk판매품목제외
			// 
			this.chk판매품목제외.AutoSize = true;
			this.chk판매품목제외.Checked = true;
			this.chk판매품목제외.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk판매품목제외.Location = new System.Drawing.Point(590, 1);
			this.chk판매품목제외.Name = "chk판매품목제외";
			this.chk판매품목제외.Size = new System.Drawing.Size(96, 16);
			this.chk판매품목제외.TabIndex = 1;
			this.chk판매품목제외.Text = "판매품목제외";
			this.chk판매품목제외.TextDD = null;
			this.chk판매품목제외.UseVisualStyleBackColor = true;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.txtID번호);
			this.bpPanelControl2.Controls.Add(this.lblID번호);
			this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// txtID번호
			// 
			this.txtID번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtID번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtID번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txtID번호.Location = new System.Drawing.Point(106, 0);
			this.txtID번호.Name = "txtID번호";
			this.txtID번호.Size = new System.Drawing.Size(186, 21);
			this.txtID번호.TabIndex = 1;
			// 
			// lblID번호
			// 
			this.lblID번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblID번호.Location = new System.Drawing.Point(0, 0);
			this.lblID번호.Name = "lblID번호";
			this.lblID번호.Size = new System.Drawing.Size(100, 23);
			this.lblID번호.TabIndex = 0;
			this.lblID번호.Text = "ID번호";
			this.lblID번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.txt수주번호);
			this.bpPanelControl1.Controls.Add(this.lbl수주번호);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// txt수주번호
			// 
			this.txt수주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt수주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt수주번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt수주번호.Location = new System.Drawing.Point(106, 0);
			this.txt수주번호.Name = "txt수주번호";
			this.txt수주번호.Size = new System.Drawing.Size(186, 21);
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
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn판매해제);
			this.flowLayoutPanel1.Controls.Add(this.btn판매처리);
			this.flowLayoutPanel1.Controls.Add(this.btn조회);
			this.flowLayoutPanel1.Controls.Add(this.txtQR코드스캔);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 75);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(986, 25);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// btn판매해제
			// 
			this.btn판매해제.BackColor = System.Drawing.Color.Transparent;
			this.btn판매해제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn판매해제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn판매해제.Location = new System.Drawing.Point(913, 3);
			this.btn판매해제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn판매해제.Name = "btn판매해제";
			this.btn판매해제.Size = new System.Drawing.Size(70, 19);
			this.btn판매해제.TabIndex = 2;
			this.btn판매해제.TabStop = false;
			this.btn판매해제.Text = "판매해제";
			this.btn판매해제.UseVisualStyleBackColor = false;
			// 
			// btn판매처리
			// 
			this.btn판매처리.BackColor = System.Drawing.Color.Transparent;
			this.btn판매처리.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn판매처리.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn판매처리.Location = new System.Drawing.Point(837, 3);
			this.btn판매처리.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn판매처리.Name = "btn판매처리";
			this.btn판매처리.Size = new System.Drawing.Size(70, 19);
			this.btn판매처리.TabIndex = 0;
			this.btn판매처리.TabStop = false;
			this.btn판매처리.Text = "판매처리";
			this.btn판매처리.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.BackColor = System.Drawing.Color.Transparent;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(761, 3);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 1;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// txtQR코드스캔
			// 
			this.txtQR코드스캔.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtQR코드스캔.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtQR코드스캔.Location = new System.Drawing.Point(639, 3);
			this.txtQR코드스캔.Name = "txtQR코드스캔";
			this.txtQR코드스캔.Size = new System.Drawing.Size(116, 21);
			this.txtQR코드스캔.TabIndex = 3;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 106);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._flex수주품목);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this._flex생산품목);
			this.splitContainer1.Size = new System.Drawing.Size(986, 513);
			this.splitContainer1.SplitterDistance = 510;
			this.splitContainer1.TabIndex = 2;
			// 
			// _flex수주품목
			// 
			this._flex수주품목.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex수주품목.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex수주품목.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex수주품목.AutoResize = false;
			this._flex수주품목.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex수주품목.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex수주품목.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex수주품목.EnabledHeaderCheck = true;
			this._flex수주품목.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex수주품목.Location = new System.Drawing.Point(0, 0);
			this._flex수주품목.Name = "_flex수주품목";
			this._flex수주품목.Rows.Count = 1;
			this._flex수주품목.Rows.DefaultSize = 18;
			this._flex수주품목.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex수주품목.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex수주품목.ShowSort = false;
			this._flex수주품목.Size = new System.Drawing.Size(510, 513);
			this._flex수주품목.StyleInfo = resources.GetString("_flex수주품목.StyleInfo");
			this._flex수주품목.TabIndex = 0;
			this._flex수주품목.UseGridCalculator = true;
			// 
			// _flex생산품목
			// 
			this._flex생산품목.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex생산품목.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex생산품목.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex생산품목.AutoResize = false;
			this._flex생산품목.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex생산품목.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex생산품목.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex생산품목.EnabledHeaderCheck = true;
			this._flex생산품목.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex생산품목.Location = new System.Drawing.Point(0, 0);
			this._flex생산품목.Name = "_flex생산품목";
			this._flex생산품목.Rows.Count = 1;
			this._flex생산품목.Rows.DefaultSize = 18;
			this._flex생산품목.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex생산품목.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex생산품목.ShowSort = false;
			this._flex생산품목.Size = new System.Drawing.Size(472, 513);
			this._flex생산품목.StyleInfo = resources.GetString("_flex생산품목.StyleInfo");
			this._flex생산품목.TabIndex = 0;
			this._flex생산품목.UseGridCalculator = true;
			// 
			// P_CZ_PR_ASSEMBLING_SA_SOL_SUB
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(997, 675);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_CZ_PR_ASSEMBLING_SA_SOL_SUB";
			this.Text = "ERP iU";
			this.TitleText = "단품판매등록";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.bpPanelControl3.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.oneGridItem1.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl2.PerformLayout();
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex수주품목)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flex생산품목)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private Duzon.Common.Controls.RoundedButton btn판매처리;
		private Duzon.Common.Controls.RoundedButton btn조회;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Dass.FlexGrid.FlexGrid _flex수주품목;
		private Dass.FlexGrid.FlexGrid _flex생산품목;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.TextBoxExt txt수주번호;
		private Duzon.Common.Controls.LabelExt lbl수주번호;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.TextBoxExt txtID번호;
		private Duzon.Common.Controls.LabelExt lblID번호;
		private Duzon.Common.Controls.RoundedButton btn판매해제;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.Controls.PeriodPicker dtp납기일자;
		private Duzon.Common.Controls.LabelExt lbl납기일자;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private Duzon.Common.Controls.CheckBoxExt chk종결제외;
		private Duzon.Common.Controls.CheckBoxExt chk출고제외;
		private Duzon.Common.Controls.CheckBoxExt chk의뢰제외;
		private Duzon.Common.Controls.CheckBoxExt chk판매품목제외;
		private Duzon.Common.Controls.TextBoxExt txtQR코드스캔;
	}
}
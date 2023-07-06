namespace cz
{
	partial class P_CZ_HR_WAPPLY_RPT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_HR_WAPPLY_RPT));
			this.tlayM = new System.Windows.Forms.TableLayoutPanel();
			this.oneS = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbx사원 = new Duzon.Common.BpControls.BpComboBox();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.dtp신청To = new Duzon.Common.Controls.DatePicker();
			this.dtp신청From = new Duzon.Common.Controls.DatePicker();
			this.labelExt4 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl문의번호 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbx부서 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl문의번호 = new Duzon.Common.Controls.LabelExt();
			this.pnl거래처그룹 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo상태 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl거래처그룹 = new Duzon.Common.Controls.LabelExt();
			this.flexL = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn승인 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn승인취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.txt포커스 = new Duzon.Common.Controls.TextBoxExt();
			this.mDataArea.SuspendLayout();
			this.tlayM.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp신청To)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtp신청From)).BeginInit();
			this.oneGridItem2.SuspendLayout();
			this.pnl문의번호.SuspendLayout();
			this.pnl거래처그룹.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.flexL)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tlayM);
			this.mDataArea.Size = new System.Drawing.Size(1300, 610);
			// 
			// tlayM
			// 
			this.tlayM.ColumnCount = 1;
			this.tlayM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayM.Controls.Add(this.oneS, 0, 0);
			this.tlayM.Controls.Add(this.flexL, 0, 1);
			this.tlayM.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayM.Location = new System.Drawing.Point(0, 0);
			this.tlayM.Name = "tlayM";
			this.tlayM.RowCount = 2;
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayM.Size = new System.Drawing.Size(1300, 610);
			this.tlayM.TabIndex = 0;
			// 
			// oneS
			// 
			this.oneS.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneS.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneS.Location = new System.Drawing.Point(3, 3);
			this.oneS.Name = "oneS";
			this.oneS.Size = new System.Drawing.Size(1294, 63);
			this.oneS.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1284, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.cbx사원);
			this.bpPanelControl1.Controls.Add(this.labelExt2);
			this.bpPanelControl1.Location = new System.Drawing.Point(314, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(310, 23);
			this.bpPanelControl1.TabIndex = 18;
			this.bpPanelControl1.Text = "bpPanelControl6";
			// 
			// cbx사원
			// 
			this.cbx사원.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB1;
			this.cbx사원.Location = new System.Drawing.Point(84, 1);
			this.cbx사원.Name = "cbx사원";
			this.cbx사원.Size = new System.Drawing.Size(190, 21);
			this.cbx사원.TabIndex = 4;
			this.cbx사원.TabStop = false;
			this.cbx사원.Text = "bpComboBox1";
			// 
			// labelExt2
			// 
			this.labelExt2.Location = new System.Drawing.Point(17, 4);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Size = new System.Drawing.Size(65, 16);
			this.labelExt2.TabIndex = 3;
			this.labelExt2.Text = "사원";
			this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.labelExt1);
			this.bpPanelControl2.Controls.Add(this.dtp신청To);
			this.bpPanelControl2.Controls.Add(this.dtp신청From);
			this.bpPanelControl2.Controls.Add(this.labelExt4);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(310, 23);
			this.bpPanelControl2.TabIndex = 17;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// labelExt1
			// 
			this.labelExt1.Location = new System.Drawing.Point(179, 3);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(14, 16);
			this.labelExt1.TabIndex = 5;
			this.labelExt1.Text = "~";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// dtp신청To
			// 
			this.dtp신청To.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp신청To.Location = new System.Drawing.Point(196, 0);
			this.dtp신청To.Mask = "####/##/##";
			this.dtp신청To.MaskBackColor = System.Drawing.Color.White;
			this.dtp신청To.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp신청To.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp신청To.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp신청To.Modified = true;
			this.dtp신청To.Name = "dtp신청To";
			this.dtp신청To.Size = new System.Drawing.Size(90, 21);
			this.dtp신청To.TabIndex = 4;
			this.dtp신청To.Tag = "DT_CLOSE";
			this.dtp신청To.Value = new System.DateTime(((long)(0)));
			// 
			// dtp신청From
			// 
			this.dtp신청From.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp신청From.Location = new System.Drawing.Point(84, 1);
			this.dtp신청From.Mask = "####/##/##";
			this.dtp신청From.MaskBackColor = System.Drawing.Color.White;
			this.dtp신청From.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp신청From.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp신청From.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp신청From.Modified = true;
			this.dtp신청From.Name = "dtp신청From";
			this.dtp신청From.Size = new System.Drawing.Size(90, 21);
			this.dtp신청From.TabIndex = 3;
			this.dtp신청From.Tag = "DT_START";
			this.dtp신청From.Value = new System.DateTime(((long)(0)));
			// 
			// labelExt4
			// 
			this.labelExt4.Location = new System.Drawing.Point(17, 4);
			this.labelExt4.Name = "labelExt4";
			this.labelExt4.Size = new System.Drawing.Size(65, 16);
			this.labelExt4.TabIndex = 1;
			this.labelExt4.Text = "신청일자";
			this.labelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.pnl문의번호);
			this.oneGridItem2.Controls.Add(this.pnl거래처그룹);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1284, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// pnl문의번호
			// 
			this.pnl문의번호.Controls.Add(this.cbx부서);
			this.pnl문의번호.Controls.Add(this.lbl문의번호);
			this.pnl문의번호.Location = new System.Drawing.Point(314, 1);
			this.pnl문의번호.Name = "pnl문의번호";
			this.pnl문의번호.Size = new System.Drawing.Size(310, 23);
			this.pnl문의번호.TabIndex = 11;
			this.pnl문의번호.Text = "bpPanelControl5";
			// 
			// cbx부서
			// 
			this.cbx부서.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_DEPT_SUB1;
			this.cbx부서.Location = new System.Drawing.Point(84, 1);
			this.cbx부서.Name = "cbx부서";
			this.cbx부서.Size = new System.Drawing.Size(190, 21);
			this.cbx부서.TabIndex = 5;
			this.cbx부서.TabStop = false;
			this.cbx부서.Text = "bpComboBox1";
			// 
			// lbl문의번호
			// 
			this.lbl문의번호.Location = new System.Drawing.Point(17, 4);
			this.lbl문의번호.Name = "lbl문의번호";
			this.lbl문의번호.Size = new System.Drawing.Size(65, 16);
			this.lbl문의번호.TabIndex = 1;
			this.lbl문의번호.Text = "부서";
			this.lbl문의번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl거래처그룹
			// 
			this.pnl거래처그룹.Controls.Add(this.cbo상태);
			this.pnl거래처그룹.Controls.Add(this.lbl거래처그룹);
			this.pnl거래처그룹.Location = new System.Drawing.Point(2, 1);
			this.pnl거래처그룹.Name = "pnl거래처그룹";
			this.pnl거래처그룹.Size = new System.Drawing.Size(310, 23);
			this.pnl거래처그룹.TabIndex = 7;
			this.pnl거래처그룹.Text = "bpPanelControl6";
			// 
			// cbo상태
			// 
			this.cbo상태.AutoDropDown = true;
			this.cbo상태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo상태.FormattingEnabled = true;
			this.cbo상태.ItemHeight = 12;
			this.cbo상태.Location = new System.Drawing.Point(84, 1);
			this.cbo상태.Name = "cbo상태";
			this.cbo상태.Size = new System.Drawing.Size(136, 20);
			this.cbo상태.TabIndex = 6;
			this.cbo상태.Tag = "CD_PARTNER_GRP";
			// 
			// lbl거래처그룹
			// 
			this.lbl거래처그룹.Location = new System.Drawing.Point(17, 4);
			this.lbl거래처그룹.Name = "lbl거래처그룹";
			this.lbl거래처그룹.Size = new System.Drawing.Size(65, 16);
			this.lbl거래처그룹.TabIndex = 3;
			this.lbl거래처그룹.Text = "상태";
			this.lbl거래처그룹.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// flexL
			// 
			this.flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.flexL.AutoResize = false;
			this.flexL.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.flexL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.flexL.EnabledHeaderCheck = true;
			this.flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.flexL.Location = new System.Drawing.Point(3, 72);
			this.flexL.Name = "flexL";
			this.flexL.Rows.Count = 1;
			this.flexL.Rows.DefaultSize = 20;
			this.flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.flexL.ShowSort = false;
			this.flexL.Size = new System.Drawing.Size(1294, 535);
			this.flexL.StyleInfo = resources.GetString("flexL.StyleInfo");
			this.flexL.TabIndex = 1;
			this.flexL.UseGridCalculator = true;
			// 
			// btn승인
			// 
			this.btn승인.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn승인.BackColor = System.Drawing.Color.White;
			this.btn승인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn승인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn승인.Location = new System.Drawing.Point(1151, 10);
			this.btn승인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn승인.Name = "btn승인";
			this.btn승인.Size = new System.Drawing.Size(70, 19);
			this.btn승인.TabIndex = 4;
			this.btn승인.TabStop = false;
			this.btn승인.Text = "승인";
			this.btn승인.UseVisualStyleBackColor = false;
			// 
			// btn승인취소
			// 
			this.btn승인취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn승인취소.BackColor = System.Drawing.Color.White;
			this.btn승인취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn승인취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn승인취소.Location = new System.Drawing.Point(1227, 10);
			this.btn승인취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn승인취소.Name = "btn승인취소";
			this.btn승인취소.Size = new System.Drawing.Size(70, 19);
			this.btn승인취소.TabIndex = 3;
			this.btn승인취소.TabStop = false;
			this.btn승인취소.Text = "승인취소";
			this.btn승인취소.UseVisualStyleBackColor = false;
			// 
			// txt포커스
			// 
			this.txt포커스.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt포커스.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt포커스.Location = new System.Drawing.Point(150, 10);
			this.txt포커스.Name = "txt포커스";
			this.txt포커스.Size = new System.Drawing.Size(100, 21);
			this.txt포커스.TabIndex = 7;
			// 
			// P_CZ_HR_WAPPLY_RPT
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.txt포커스);
			this.Controls.Add(this.btn승인);
			this.Controls.Add(this.btn승인취소);
			this.Name = "P_CZ_HR_WAPPLY_RPT";
			this.Size = new System.Drawing.Size(1300, 650);
			this.TitleText = "휴가신청현황";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btn승인취소, 0);
			this.Controls.SetChildIndex(this.btn승인, 0);
			this.Controls.SetChildIndex(this.txt포커스, 0);
			this.mDataArea.ResumeLayout(false);
			this.tlayM.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp신청To)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtp신청From)).EndInit();
			this.oneGridItem2.ResumeLayout(false);
			this.pnl문의번호.ResumeLayout(false);
			this.pnl거래처그룹.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.flexL)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlayM;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneS;
		private Dass.FlexGrid.FlexGrid flexL;
		private Duzon.Common.Controls.RoundedButton btn승인;
		private Duzon.Common.Controls.RoundedButton btn승인취소;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Duzon.Common.Controls.DatePicker dtp신청To;
		private Duzon.Common.Controls.DatePicker dtp신청From;
		private Duzon.Common.Controls.LabelExt labelExt4;
		private Duzon.Common.BpControls.BpPanelControl pnl거래처그룹;
		private Duzon.Common.Controls.DropDownComboBox cbo상태;
		private Duzon.Common.Controls.LabelExt lbl거래처그룹;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.BpControls.BpComboBox cbx사원;
		private Duzon.Common.Controls.LabelExt labelExt2;
		private Duzon.Common.BpControls.BpPanelControl pnl문의번호;
		private Duzon.Common.Controls.LabelExt lbl문의번호;
		private Duzon.Common.BpControls.BpComboBox cbx부서;
		private Duzon.Common.Controls.TextBoxExt txt포커스;
	}
}
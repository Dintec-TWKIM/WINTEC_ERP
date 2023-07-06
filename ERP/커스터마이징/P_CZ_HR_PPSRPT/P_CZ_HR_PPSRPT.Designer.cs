namespace cz
{
	partial class P_CZ_HR_PPSRPT
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
			this._web = new System.Windows.Forms.WebBrowser();
			this._pnlMain = new Duzon.Common.Controls.PanelExt();
			this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bppnl지급일자 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl지급일자 = new Duzon.Common.Controls.LabelExt();
			this.cbo지급일자 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx사원 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl사원 = new Duzon.Common.Controls.LabelExt();
			this.bppnl귀속년월 = new Duzon.Common.BpControls.BpPanelControl();
			this.btn지급일자 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.lbl귀속년월 = new Duzon.Common.Controls.LabelExt();
			this.dtp귀속년월 = new Duzon.Common.Controls.DatePicker();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bppnl양식구분 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx양식구분 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl양식구분 = new Duzon.Common.Controls.LabelExt();
			this.bppnl급여구분 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl급여구분 = new Duzon.Common.Controls.LabelExt();
			this.cbo급여구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.bppnl순번 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp순번 = new Duzon.Common.Controls.NumericUpDownExt();
			this.lbl순번 = new Duzon.Common.Controls.LabelExt();
			this.mDataArea.SuspendLayout();
			this._pnlMain.SuspendLayout();
			this._tlayMain.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bppnl지급일자.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.bppnl귀속년월.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp귀속년월)).BeginInit();
			this.oneGridItem2.SuspendLayout();
			this.bppnl양식구분.SuspendLayout();
			this.bppnl급여구분.SuspendLayout();
			this.bppnl순번.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp순번)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this._tlayMain);
			this.mDataArea.Size = new System.Drawing.Size(1154, 756);
			// 
			// _web
			// 
			this._web.Dock = System.Windows.Forms.DockStyle.Fill;
			this._web.Location = new System.Drawing.Point(0, 0);
			this._web.MinimumSize = new System.Drawing.Size(20, 20);
			this._web.Name = "_web";
			this._web.Size = new System.Drawing.Size(1146, 680);
			this._web.TabIndex = 5;
			// 
			// _pnlMain
			// 
			this._pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this._pnlMain.Controls.Add(this._web);
			this._pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this._pnlMain.Location = new System.Drawing.Point(3, 71);
			this._pnlMain.Name = "_pnlMain";
			this._pnlMain.Size = new System.Drawing.Size(1148, 682);
			this._pnlMain.TabIndex = 6;
			// 
			// _tlayMain
			// 
			this._tlayMain.ColumnCount = 1;
			this._tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this._tlayMain.Controls.Add(this._pnlMain, 0, 1);
			this._tlayMain.Controls.Add(this.oneGrid1, 0, 0);
			this._tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this._tlayMain.Location = new System.Drawing.Point(0, 0);
			this._tlayMain.Name = "_tlayMain";
			this._tlayMain.RowCount = 2;
			this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this._tlayMain.Size = new System.Drawing.Size(1154, 756);
			this._tlayMain.TabIndex = 7;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1148, 62);
			this.oneGrid1.TabIndex = 8;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bppnl지급일자);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.Controls.Add(this.bppnl귀속년월);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1138, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bppnl지급일자
			// 
			this.bppnl지급일자.Controls.Add(this.lbl지급일자);
			this.bppnl지급일자.Controls.Add(this.cbo지급일자);
			this.bppnl지급일자.Location = new System.Drawing.Point(590, 1);
			this.bppnl지급일자.Name = "bppnl지급일자";
			this.bppnl지급일자.Size = new System.Drawing.Size(292, 23);
			this.bppnl지급일자.TabIndex = 2;
			this.bppnl지급일자.Text = "bpPanelControl3";
			this.bppnl지급일자.Visible = false;
			// 
			// lbl지급일자
			// 
			this.lbl지급일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl지급일자.Location = new System.Drawing.Point(0, 0);
			this.lbl지급일자.Name = "lbl지급일자";
			this.lbl지급일자.Size = new System.Drawing.Size(100, 23);
			this.lbl지급일자.TabIndex = 134;
			this.lbl지급일자.Tag = "";
			this.lbl지급일자.Text = "지급일자";
			this.lbl지급일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo지급일자
			// 
			this.cbo지급일자.AutoDropDown = true;
			this.cbo지급일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo지급일자.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo지급일자.ItemHeight = 12;
			this.cbo지급일자.Location = new System.Drawing.Point(106, 0);
			this.cbo지급일자.Name = "cbo지급일자";
			this.cbo지급일자.Size = new System.Drawing.Size(186, 20);
			this.cbo지급일자.TabIndex = 131;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.ctx사원);
			this.bpPanelControl1.Controls.Add(this.lbl사원);
			this.bpPanelControl1.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 2;
			this.bpPanelControl1.Text = "bpPanelControl5";
			// 
			// ctx사원
			// 
			this.ctx사원.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx사원.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.ctx사원.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx사원.Location = new System.Drawing.Point(106, 0);
			this.ctx사원.Name = "ctx사원";
			this.ctx사원.SetDefaultValue = true;
			this.ctx사원.Size = new System.Drawing.Size(186, 21);
			this.ctx사원.TabIndex = 136;
			this.ctx사원.TabStop = false;
			this.ctx사원.Text = "bpCodeTextBox1";
			this.ctx사원.UserCodeName = "NAME";
			this.ctx사원.UserCodeValue = "CODE";
			this.ctx사원.UserHelpID = "H_HR_HELP01";
			// 
			// lbl사원
			// 
			this.lbl사원.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl사원.Location = new System.Drawing.Point(0, 0);
			this.lbl사원.Name = "lbl사원";
			this.lbl사원.Size = new System.Drawing.Size(100, 23);
			this.lbl사원.TabIndex = 135;
			this.lbl사원.Tag = "";
			this.lbl사원.Text = "사원";
			this.lbl사원.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bppnl귀속년월
			// 
			this.bppnl귀속년월.Controls.Add(this.btn지급일자);
			this.bppnl귀속년월.Controls.Add(this.lbl귀속년월);
			this.bppnl귀속년월.Controls.Add(this.dtp귀속년월);
			this.bppnl귀속년월.Location = new System.Drawing.Point(2, 1);
			this.bppnl귀속년월.Name = "bppnl귀속년월";
			this.bppnl귀속년월.Size = new System.Drawing.Size(292, 23);
			this.bppnl귀속년월.TabIndex = 0;
			this.bppnl귀속년월.Text = "bpPanelControl1";
			// 
			// btn지급일자
			// 
			this.btn지급일자.BackColor = System.Drawing.Color.Transparent;
			this.btn지급일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn지급일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.btn지급일자.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn지급일자.Location = new System.Drawing.Point(222, 0);
			this.btn지급일자.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn지급일자.Name = "btn지급일자";
			this.btn지급일자.Size = new System.Drawing.Size(70, 19);
			this.btn지급일자.TabIndex = 135;
			this.btn지급일자.TabStop = false;
			this.btn지급일자.Text = "지급일자";
			this.btn지급일자.UseVisualStyleBackColor = false;
			// 
			// lbl귀속년월
			// 
			this.lbl귀속년월.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl귀속년월.Location = new System.Drawing.Point(0, 0);
			this.lbl귀속년월.Name = "lbl귀속년월";
			this.lbl귀속년월.Size = new System.Drawing.Size(100, 23);
			this.lbl귀속년월.TabIndex = 133;
			this.lbl귀속년월.Tag = "";
			this.lbl귀속년월.Text = "귀속년월";
			this.lbl귀속년월.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtp귀속년월
			// 
			this.dtp귀속년월.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp귀속년월.Location = new System.Drawing.Point(106, 1);
			this.dtp귀속년월.Mask = "####/##";
			this.dtp귀속년월.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.dtp귀속년월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp귀속년월.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp귀속년월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp귀속년월.Name = "dtp귀속년월";
			this.dtp귀속년월.ShowUpDown = true;
			this.dtp귀속년월.Size = new System.Drawing.Size(89, 21);
			this.dtp귀속년월.TabIndex = 130;
			this.dtp귀속년월.Value = new System.DateTime(((long)(0)));
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bppnl양식구분);
			this.oneGridItem2.Controls.Add(this.bppnl급여구분);
			this.oneGridItem2.Controls.Add(this.bppnl순번);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1138, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bppnl양식구분
			// 
			this.bppnl양식구분.Controls.Add(this.ctx양식구분);
			this.bppnl양식구분.Controls.Add(this.lbl양식구분);
			this.bppnl양식구분.Location = new System.Drawing.Point(590, 1);
			this.bppnl양식구분.Name = "bppnl양식구분";
			this.bppnl양식구분.Size = new System.Drawing.Size(292, 23);
			this.bppnl양식구분.TabIndex = 1;
			this.bppnl양식구분.Text = "bpPanelControl5";
			this.bppnl양식구분.Visible = false;
			// 
			// ctx양식구분
			// 
			this.ctx양식구분.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx양식구분.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx양식구분.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx양식구분.Location = new System.Drawing.Point(106, 0);
			this.ctx양식구분.Name = "ctx양식구분";
			this.ctx양식구분.Size = new System.Drawing.Size(186, 21);
			this.ctx양식구분.TabIndex = 136;
			this.ctx양식구분.TabStop = false;
			this.ctx양식구분.Text = "bpCodeTextBox1";
			this.ctx양식구분.UserCodeName = "NAME";
			this.ctx양식구분.UserCodeValue = "CODE";
			this.ctx양식구분.UserHelpID = "H_HR_HELP01";
			// 
			// lbl양식구분
			// 
			this.lbl양식구분.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl양식구분.Location = new System.Drawing.Point(0, 0);
			this.lbl양식구분.Name = "lbl양식구분";
			this.lbl양식구분.Size = new System.Drawing.Size(100, 23);
			this.lbl양식구분.TabIndex = 135;
			this.lbl양식구분.Tag = "";
			this.lbl양식구분.Text = "양식구분";
			this.lbl양식구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bppnl급여구분
			// 
			this.bppnl급여구분.Controls.Add(this.lbl급여구분);
			this.bppnl급여구분.Controls.Add(this.cbo급여구분);
			this.bppnl급여구분.Location = new System.Drawing.Point(296, 1);
			this.bppnl급여구분.Name = "bppnl급여구분";
			this.bppnl급여구분.Size = new System.Drawing.Size(292, 23);
			this.bppnl급여구분.TabIndex = 1;
			this.bppnl급여구분.Text = "bpPanelControl2";
			this.bppnl급여구분.Visible = false;
			// 
			// lbl급여구분
			// 
			this.lbl급여구분.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl급여구분.Location = new System.Drawing.Point(0, 0);
			this.lbl급여구분.Name = "lbl급여구분";
			this.lbl급여구분.Size = new System.Drawing.Size(100, 23);
			this.lbl급여구분.TabIndex = 129;
			this.lbl급여구분.Tag = "TP_PAY";
			this.lbl급여구분.Text = "급여구분";
			this.lbl급여구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo급여구분
			// 
			this.cbo급여구분.AutoDropDown = true;
			this.cbo급여구분.BackColor = System.Drawing.Color.White;
			this.cbo급여구분.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo급여구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo급여구분.ItemHeight = 12;
			this.cbo급여구분.Location = new System.Drawing.Point(106, 0);
			this.cbo급여구분.Name = "cbo급여구분";
			this.cbo급여구분.Size = new System.Drawing.Size(186, 20);
			this.cbo급여구분.TabIndex = 132;
			// 
			// bppnl순번
			// 
			this.bppnl순번.Controls.Add(this.dtp순번);
			this.bppnl순번.Controls.Add(this.lbl순번);
			this.bppnl순번.Location = new System.Drawing.Point(2, 1);
			this.bppnl순번.Name = "bppnl순번";
			this.bppnl순번.Size = new System.Drawing.Size(292, 23);
			this.bppnl순번.TabIndex = 0;
			this.bppnl순번.Text = "bpPanelControl4";
			// 
			// dtp순번
			// 
			this.dtp순번.Location = new System.Drawing.Point(106, 1);
			this.dtp순번.Modified = true;
			this.dtp순번.Name = "dtp순번";
			this.dtp순번.Size = new System.Drawing.Size(50, 21);
			this.dtp순번.TabIndex = 139;
			this.dtp순번.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dtp순번.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lbl순번
			// 
			this.lbl순번.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl순번.Location = new System.Drawing.Point(0, 0);
			this.lbl순번.Name = "lbl순번";
			this.lbl순번.Size = new System.Drawing.Size(100, 23);
			this.lbl순번.TabIndex = 138;
			this.lbl순번.Tag = "NO_SEQ";
			this.lbl순번.Text = "순번";
			this.lbl순번.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// P_CZ_HR_PPSRPT
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_HR_PPSRPT";
			this.Size = new System.Drawing.Size(1154, 796);
			this.TitleText = "급여명세서조회";
			this.mDataArea.ResumeLayout(false);
			this._pnlMain.ResumeLayout(false);
			this._tlayMain.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bppnl지급일자.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bppnl귀속년월.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp귀속년월)).EndInit();
			this.oneGridItem2.ResumeLayout(false);
			this.bppnl양식구분.ResumeLayout(false);
			this.bppnl급여구분.ResumeLayout(false);
			this.bppnl순번.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp순번)).EndInit();
			this.ResumeLayout(false);

        }

		private System.Windows.Forms.WebBrowser _web;
		private Duzon.Common.Controls.PanelExt _pnlMain;
		private System.Windows.Forms.TableLayoutPanel _tlayMain;
		private Duzon.Common.Controls.NumericUpDownExt dtp순번;
		private Duzon.Common.Controls.LabelExt lbl순번;
		private Duzon.Common.Controls.LabelExt lbl양식구분;
		private Duzon.Common.Controls.LabelExt lbl귀속년월;
		private Duzon.Common.Controls.LabelExt lbl지급일자;
		private Duzon.Common.Controls.DropDownComboBox cbo급여구분;
		private Duzon.Common.Controls.LabelExt lbl급여구분;
		private Duzon.Common.Controls.DropDownComboBox cbo지급일자;
		private Duzon.Common.Controls.DatePicker dtp귀속년월;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bppnl지급일자;
		private Duzon.Common.BpControls.BpPanelControl bppnl급여구분;
		private Duzon.Common.BpControls.BpPanelControl bppnl귀속년월;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl bppnl양식구분;
		private Duzon.Common.BpControls.BpPanelControl bppnl순번;
		private Duzon.Common.BpControls.BpCodeTextBox ctx양식구분;

		#endregion

		private Duzon.Common.Controls.RoundedButton btn지급일자;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.BpControls.BpCodeTextBox ctx사원;
		private Duzon.Common.Controls.LabelExt lbl사원;
	}
}
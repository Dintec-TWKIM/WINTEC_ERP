
namespace cz
{
	partial class P_CZ_PR_OPOUT_IV_MNG
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
			this._flexM = new Dass.FlexGrid.FlexGrid(this.components);
			this._flexD = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.cbo전표처리여부 = new Duzon.Common.Controls.DropDownComboBox();
			this.ctx담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl전표처리여부 = new Duzon.Common.Controls.LabelExt();
			this.lbl공장 = new Duzon.Common.Controls.LabelExt();
			this.lbl처리일자 = new Duzon.Common.Controls.LabelExt();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpP_Emp = new Duzon.Common.BpControls.BpPanelControl();
			this.bpP_Date = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp처리일자 = new Duzon.Common.Controls.PeriodPicker();
			this.bpP_Plant = new Duzon.Common.BpControls.BpPanelControl();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpPanelControl9 = new Duzon.Common.BpControls.BpPanelControl();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn미결전표처리 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn미결전표처리취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.lbl담당자 = new Duzon.Common.Controls.LabelExt();
			this.lbl외주처 = new Duzon.Common.Controls.LabelExt();
			this.ctx외주처 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.mDataArea.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexM)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flexD)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpP_Emp.SuspendLayout();
			this.bpP_Date.SuspendLayout();
			this.bpP_Plant.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl8.SuspendLayout();
			this.bpPanelControl9.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1060, 756);
			// 
			// _flexM
			// 
			this._flexM.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexM.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexM.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexM.AutoResize = false;
			this._flexM.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexM.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexM.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexM.EnabledHeaderCheck = true;
			this._flexM.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexM.Location = new System.Drawing.Point(3, 71);
			this._flexM.Name = "_flexM";
			this._flexM.Rows.Count = 1;
			this._flexM.Rows.DefaultSize = 20;
			this._flexM.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexM.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexM.ShowSort = false;
			this._flexM.Size = new System.Drawing.Size(1054, 324);
			this._flexM.TabIndex = 7;
			this._flexM.UseGridCalculator = true;
			// 
			// _flexD
			// 
			this._flexD.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexD.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexD.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexD.AutoResize = false;
			this._flexD.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexD.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexD.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexD.EnabledHeaderCheck = true;
			this._flexD.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexD.Location = new System.Drawing.Point(3, 428);
			this._flexD.Name = "_flexD";
			this._flexD.Rows.Count = 1;
			this._flexD.Rows.DefaultSize = 18;
			this._flexD.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexD.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexD.ShowSort = false;
			this._flexD.Size = new System.Drawing.Size(1054, 325);
			this._flexD.TabIndex = 7;
			this._flexD.UseGridCalculator = true;
			// 
			// btn삭제
			// 
			this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn삭제.BackColor = System.Drawing.Color.White;
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(74, 1);
			this.btn삭제.Margin = new System.Windows.Forms.Padding(3, 1, 0, 3);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(62, 19);
			this.btn삭제.TabIndex = 6;
			this.btn삭제.TabStop = false;
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// cbo전표처리여부
			// 
			this.cbo전표처리여부.AutoDropDown = false;
			this.cbo전표처리여부.BackColor = System.Drawing.Color.White;
			this.cbo전표처리여부.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo전표처리여부.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo전표처리여부.ItemHeight = 12;
			this.cbo전표처리여부.Location = new System.Drawing.Point(107, 0);
			this.cbo전표처리여부.Name = "cbo전표처리여부";
			this.cbo전표처리여부.Size = new System.Drawing.Size(185, 20);
			this.cbo전표처리여부.TabIndex = 147;
			this.cbo전표처리여부.UseKeyF3 = false;
			// 
			// ctx담당자
			// 
			this.ctx담당자.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.ctx담당자.LabelWidth = 156;
			this.ctx담당자.Location = new System.Drawing.Point(107, 0);
			this.ctx담당자.Name = "ctx담당자";
			this.ctx담당자.Size = new System.Drawing.Size(185, 21);
			this.ctx담당자.TabIndex = 146;
			this.ctx담당자.TabStop = false;
			this.ctx담당자.Tag = "CD_PARTNER;LN_PARTNER";
			this.ctx담당자.Text = "담당자";
			// 
			// cbo공장
			// 
			this.cbo공장.AutoDropDown = false;
			this.cbo공장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.cbo공장.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo공장.ItemHeight = 12;
			this.cbo공장.Location = new System.Drawing.Point(107, 0);
			this.cbo공장.Name = "cbo공장";
			this.cbo공장.Size = new System.Drawing.Size(185, 20);
			this.cbo공장.TabIndex = 1;
			this.cbo공장.UseKeyF3 = false;
			// 
			// lbl전표처리여부
			// 
			this.lbl전표처리여부.BackColor = System.Drawing.Color.Transparent;
			this.lbl전표처리여부.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl전표처리여부.Location = new System.Drawing.Point(0, 0);
			this.lbl전표처리여부.Name = "lbl전표처리여부";
			this.lbl전표처리여부.Size = new System.Drawing.Size(100, 23);
			this.lbl전표처리여부.TabIndex = 6;
			this.lbl전표처리여부.Text = "전표처리여부";
			this.lbl전표처리여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl공장
			// 
			this.lbl공장.BackColor = System.Drawing.Color.Transparent;
			this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl공장.Location = new System.Drawing.Point(0, 0);
			this.lbl공장.Name = "lbl공장";
			this.lbl공장.Size = new System.Drawing.Size(100, 23);
			this.lbl공장.TabIndex = 1;
			this.lbl공장.Tag = "공장";
			this.lbl공장.Text = "공장";
			this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl처리일자
			// 
			this.lbl처리일자.BackColor = System.Drawing.Color.Transparent;
			this.lbl처리일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl처리일자.Location = new System.Drawing.Point(0, 0);
			this.lbl처리일자.Name = "lbl처리일자";
			this.lbl처리일자.Size = new System.Drawing.Size(100, 23);
			this.lbl처리일자.TabIndex = 4;
			this.lbl처리일자.Text = "처리일자";
			this.lbl처리일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this._flexD, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this._flexM, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1060, 756);
			this.tableLayoutPanel1.TabIndex = 148;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1054, 62);
			this.oneGrid1.TabIndex = 131;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpP_Emp);
			this.oneGridItem1.Controls.Add(this.bpP_Date);
			this.oneGridItem1.Controls.Add(this.bpP_Plant);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1044, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpP_Emp
			// 
			this.bpP_Emp.Controls.Add(this.lbl담당자);
			this.bpP_Emp.Controls.Add(this.ctx담당자);
			this.bpP_Emp.Location = new System.Drawing.Point(590, 1);
			this.bpP_Emp.Name = "bpP_Emp";
			this.bpP_Emp.Size = new System.Drawing.Size(292, 23);
			this.bpP_Emp.TabIndex = 3;
			this.bpP_Emp.Text = "bpPanelControl2";
			// 
			// bpP_Date
			// 
			this.bpP_Date.Controls.Add(this.dtp처리일자);
			this.bpP_Date.Controls.Add(this.lbl처리일자);
			this.bpP_Date.Location = new System.Drawing.Point(296, 1);
			this.bpP_Date.Name = "bpP_Date";
			this.bpP_Date.Size = new System.Drawing.Size(292, 23);
			this.bpP_Date.TabIndex = 4;
			this.bpP_Date.Text = "bpPanelControl5";
			// 
			// dtp처리일자
			// 
			this.dtp처리일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp처리일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp처리일자.IsNecessaryCondition = true;
			this.dtp처리일자.Location = new System.Drawing.Point(107, 0);
			this.dtp처리일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp처리일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp처리일자.Name = "dtp처리일자";
			this.dtp처리일자.Size = new System.Drawing.Size(185, 21);
			this.dtp처리일자.TabIndex = 248;
			// 
			// bpP_Plant
			// 
			this.bpP_Plant.Controls.Add(this.lbl공장);
			this.bpP_Plant.Controls.Add(this.cbo공장);
			this.bpP_Plant.Location = new System.Drawing.Point(2, 1);
			this.bpP_Plant.Name = "bpP_Plant";
			this.bpP_Plant.Size = new System.Drawing.Size(292, 23);
			this.bpP_Plant.TabIndex = 2;
			this.bpP_Plant.Text = "bpPanelControl6";
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl8);
			this.oneGridItem2.Controls.Add(this.bpPanelControl9);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1044, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl8
			// 
			this.bpPanelControl8.Controls.Add(this.cbo전표처리여부);
			this.bpPanelControl8.Controls.Add(this.lbl전표처리여부);
			this.bpPanelControl8.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl8.Name = "bpPanelControl8";
			this.bpPanelControl8.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl8.TabIndex = 4;
			this.bpPanelControl8.Text = "bpPanelControl8";
			// 
			// bpPanelControl9
			// 
			this.bpPanelControl9.Controls.Add(this.ctx외주처);
			this.bpPanelControl9.Controls.Add(this.lbl외주처);
			this.bpPanelControl9.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl9.Name = "bpPanelControl9";
			this.bpPanelControl9.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl9.TabIndex = 2;
			this.bpPanelControl9.Text = "bpPanelControl9";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel2.Controls.Add(this.btn삭제);
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(921, 401);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(136, 21);
			this.flowLayoutPanel2.TabIndex = 132;
			// 
			// btn미결전표처리
			// 
			this.btn미결전표처리.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn미결전표처리.BackColor = System.Drawing.Color.White;
			this.btn미결전표처리.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn미결전표처리.Enabled = false;
			this.btn미결전표처리.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn미결전표처리.Location = new System.Drawing.Point(50, 3);
			this.btn미결전표처리.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn미결전표처리.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn미결전표처리.Name = "btn미결전표처리";
			this.btn미결전표처리.Size = new System.Drawing.Size(89, 19);
			this.btn미결전표처리.TabIndex = 7;
			this.btn미결전표처리.TabStop = false;
			this.btn미결전표처리.Text = "미결전표처리";
			this.btn미결전표처리.UseVisualStyleBackColor = false;
			// 
			// btn미결전표처리취소
			// 
			this.btn미결전표처리취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn미결전표처리취소.BackColor = System.Drawing.Color.White;
			this.btn미결전표처리취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn미결전표처리취소.Enabled = false;
			this.btn미결전표처리취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn미결전표처리취소.Location = new System.Drawing.Point(142, 3);
			this.btn미결전표처리취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn미결전표처리취소.Name = "btn미결전표처리취소";
			this.btn미결전표처리취소.Size = new System.Drawing.Size(112, 19);
			this.btn미결전표처리취소.TabIndex = 6;
			this.btn미결전표처리취소.TabStop = false;
			this.btn미결전표처리취소.Text = "미결전표처리취소";
			this.btn미결전표처리취소.UseVisualStyleBackColor = false;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Controls.Add(this.btn미결전표처리취소);
			this.flowLayoutPanel1.Controls.Add(this.btn미결전표처리);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(803, 10);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(257, 23);
			this.flowLayoutPanel1.TabIndex = 8;
			// 
			// lbl담당자
			// 
			this.lbl담당자.BackColor = System.Drawing.Color.Transparent;
			this.lbl담당자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl담당자.Location = new System.Drawing.Point(0, 0);
			this.lbl담당자.Name = "lbl담당자";
			this.lbl담당자.Size = new System.Drawing.Size(100, 23);
			this.lbl담당자.TabIndex = 147;
			this.lbl담당자.Text = "담당자";
			this.lbl담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl외주처
			// 
			this.lbl외주처.BackColor = System.Drawing.Color.Transparent;
			this.lbl외주처.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl외주처.Location = new System.Drawing.Point(0, 0);
			this.lbl외주처.Name = "lbl외주처";
			this.lbl외주처.Size = new System.Drawing.Size(100, 23);
			this.lbl외주처.TabIndex = 143;
			this.lbl외주처.Tag = "공장";
			this.lbl외주처.Text = "외주처";
			this.lbl외주처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx외주처
			// 
			this.ctx외주처.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx외주처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
			this.ctx외주처.LabelWidth = 156;
			this.ctx외주처.Location = new System.Drawing.Point(107, 0);
			this.ctx외주처.Name = "ctx외주처";
			this.ctx외주처.Size = new System.Drawing.Size(185, 21);
			this.ctx외주처.TabIndex = 144;
			this.ctx외주처.TabStop = false;
			this.ctx외주처.Tag = "CD_PARTNER;LN_PARTNER";
			this.ctx외주처.Text = "외주처";
			// 
			// P_CZ_PR_OPOUT_IV_MNG
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.flowLayoutPanel1);
			this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Name = "P_CZ_PR_OPOUT_IV_MNG";
			this.Size = new System.Drawing.Size(1060, 796);
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
			this.mDataArea.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexM)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flexD)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpP_Emp.ResumeLayout(false);
			this.bpP_Date.ResumeLayout(false);
			this.bpP_Plant.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl8.ResumeLayout(false);
			this.bpPanelControl9.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

		#endregion
		private Duzon.Common.Controls.DropDownComboBox cbo공장;
		private Duzon.Common.Controls.LabelExt lbl공장;
		private Dass.FlexGrid.FlexGrid _flexM;
		private Dass.FlexGrid.FlexGrid _flexD;
		private Duzon.Common.Controls.RoundedButton btn삭제;
		private Duzon.Common.Controls.LabelExt lbl처리일자;
		private Duzon.Common.BpControls.BpCodeTextBox ctx담당자;
		private Duzon.Common.Controls.LabelExt lbl전표처리여부;
		private Duzon.Common.Controls.DropDownComboBox cbo전표처리여부;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Common.Controls.RoundedButton btn미결전표처리취소;
		private Duzon.Common.Controls.RoundedButton btn미결전표처리;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpP_Emp;
		private Duzon.Common.BpControls.BpPanelControl bpP_Date;
		private Duzon.Common.BpControls.BpPanelControl bpP_Plant;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl8;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl9;
		private Duzon.Common.Controls.PeriodPicker dtp처리일자;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private Duzon.Common.Controls.LabelExt lbl담당자;
		private Duzon.Common.BpControls.BpCodeTextBox ctx외주처;
		private Duzon.Common.Controls.LabelExt lbl외주처;
	}
}
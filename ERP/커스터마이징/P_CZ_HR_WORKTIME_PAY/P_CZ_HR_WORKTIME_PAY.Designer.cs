
namespace cz
{
	partial class P_CZ_HR_WORKTIME_PAY
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_HR_WORKTIME_PAY));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx사원 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.labelExt3 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx팀 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.pnl부서 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx부서 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.labelExt5 = new Duzon.Common.Controls.LabelExt();
			this.pnl참조 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp근무년월 = new Duzon.Common.Controls.DatePicker();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.spc메인 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.grd라인 = new Dass.FlexGrid.FlexGrid(this.components);
			this.imagePanel2 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.imagePanel3 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.grd일일 = new Dass.FlexGrid.FlexGrid(this.components);
			this.grd주간 = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn급여반영 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.pnl부서.SuspendLayout();
			this.pnl참조.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp근무년월)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spc메인)).BeginInit();
			this.spc메인.Panel1.SuspendLayout();
			this.spc메인.Panel2.SuspendLayout();
			this.spc메인.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd라인)).BeginInit();
			this.tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd일일)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grd주간)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1438, 799);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.spc메인, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1438, 799);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1432, 51);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.pnl부서);
			this.oneGridItem1.Controls.Add(this.pnl참조);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1422, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.ctx사원);
			this.bpPanelControl3.Controls.Add(this.labelExt3);
			this.bpPanelControl3.Dock = System.Windows.Forms.DockStyle.Top;
			this.bpPanelControl3.Location = new System.Drawing.Point(703, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(270, 23);
			this.bpPanelControl3.TabIndex = 13;
			this.bpPanelControl3.Text = "bpPanelControl7";
			// 
			// ctx사원
			// 
			this.ctx사원.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.ctx사원.Location = new System.Drawing.Point(84, 1);
			this.ctx사원.Name = "ctx사원";
			this.ctx사원.Size = new System.Drawing.Size(186, 21);
			this.ctx사원.TabIndex = 6;
			this.ctx사원.TabStop = false;
			this.ctx사원.Tag = "";
			// 
			// labelExt3
			// 
			this.labelExt3.Location = new System.Drawing.Point(17, 4);
			this.labelExt3.Name = "labelExt3";
			this.labelExt3.Size = new System.Drawing.Size(65, 16);
			this.labelExt3.TabIndex = 3;
			this.labelExt3.Text = "사원";
			this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.ctx팀);
			this.bpPanelControl2.Controls.Add(this.labelExt2);
			this.bpPanelControl2.Dock = System.Windows.Forms.DockStyle.Top;
			this.bpPanelControl2.Location = new System.Drawing.Point(431, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(270, 23);
			this.bpPanelControl2.TabIndex = 12;
			this.bpPanelControl2.Text = "bpPanelControl7";
			// 
			// ctx팀
			// 
			this.ctx팀.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CC_SUB;
			this.ctx팀.Location = new System.Drawing.Point(84, 1);
			this.ctx팀.Name = "ctx팀";
			this.ctx팀.Size = new System.Drawing.Size(186, 21);
			this.ctx팀.TabIndex = 6;
			this.ctx팀.TabStop = false;
			this.ctx팀.Tag = "";
			// 
			// labelExt2
			// 
			this.labelExt2.Location = new System.Drawing.Point(17, 4);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Size = new System.Drawing.Size(65, 16);
			this.labelExt2.TabIndex = 3;
			this.labelExt2.Text = "팀";
			this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl부서
			// 
			this.pnl부서.Controls.Add(this.ctx부서);
			this.pnl부서.Controls.Add(this.labelExt5);
			this.pnl부서.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl부서.Location = new System.Drawing.Point(159, 1);
			this.pnl부서.Name = "pnl부서";
			this.pnl부서.Size = new System.Drawing.Size(270, 23);
			this.pnl부서.TabIndex = 11;
			this.pnl부서.Text = "bpPanelControl7";
			// 
			// ctx부서
			// 
			this.ctx부서.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_DEPT_SUB;
			this.ctx부서.Location = new System.Drawing.Point(84, 1);
			this.ctx부서.Name = "ctx부서";
			this.ctx부서.Size = new System.Drawing.Size(186, 21);
			this.ctx부서.TabIndex = 6;
			this.ctx부서.TabStop = false;
			this.ctx부서.Tag = "";
			// 
			// labelExt5
			// 
			this.labelExt5.Location = new System.Drawing.Point(17, 4);
			this.labelExt5.Name = "labelExt5";
			this.labelExt5.Size = new System.Drawing.Size(65, 16);
			this.labelExt5.TabIndex = 3;
			this.labelExt5.Text = "부서";
			this.labelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl참조
			// 
			this.pnl참조.Controls.Add(this.dtp근무년월);
			this.pnl참조.Controls.Add(this.labelExt1);
			this.pnl참조.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl참조.Location = new System.Drawing.Point(2, 1);
			this.pnl참조.Name = "pnl참조";
			this.pnl참조.Size = new System.Drawing.Size(155, 23);
			this.pnl참조.TabIndex = 9;
			this.pnl참조.Text = "bpPanelControl7";
			// 
			// dtp근무년월
			// 
			this.dtp근무년월.BackColor = System.Drawing.Color.White;
			this.dtp근무년월.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp근무년월.Location = new System.Drawing.Point(84, 1);
			this.dtp근무년월.Mask = "####/##";
			this.dtp근무년월.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.dtp근무년월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp근무년월.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp근무년월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp근무년월.Modified = true;
			this.dtp근무년월.Name = "dtp근무년월";
			this.dtp근무년월.ShowUpDown = true;
			this.dtp근무년월.Size = new System.Drawing.Size(70, 21);
			this.dtp근무년월.TabIndex = 4;
			this.dtp근무년월.Tag = "DT_START";
			this.dtp근무년월.Value = new System.DateTime(((long)(0)));
			// 
			// labelExt1
			// 
			this.labelExt1.Location = new System.Drawing.Point(17, 4);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(65, 16);
			this.labelExt1.TabIndex = 3;
			this.labelExt1.Text = "근무년월";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// spc메인
			// 
			this.spc메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spc메인.Location = new System.Drawing.Point(3, 60);
			this.spc메인.Name = "spc메인";
			// 
			// spc메인.Panel1
			// 
			this.spc메인.Panel1.Controls.Add(this.tableLayoutPanel2);
			// 
			// spc메인.Panel2
			// 
			this.spc메인.Panel2.Controls.Add(this.tableLayoutPanel3);
			this.spc메인.Size = new System.Drawing.Size(1432, 736);
			this.spc메인.SplitterDistance = 479;
			this.spc메인.TabIndex = 1;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.grd라인, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.imagePanel2, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(479, 736);
			this.tableLayoutPanel2.TabIndex = 0;
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
			this.grd라인.Location = new System.Drawing.Point(3, 36);
			this.grd라인.Name = "grd라인";
			this.grd라인.Rows.Count = 1;
			this.grd라인.Rows.DefaultSize = 18;
			this.grd라인.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd라인.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd라인.ShowSort = false;
			this.grd라인.Size = new System.Drawing.Size(473, 697);
			this.grd라인.StyleInfo = resources.GetString("grd라인.StyleInfo");
			this.grd라인.TabIndex = 17;
			this.grd라인.UseGridCalculator = true;
			// 
			// imagePanel2
			// 
			this.imagePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.imagePanel2.LeftImage = null;
			this.imagePanel2.Location = new System.Drawing.Point(3, 3);
			this.imagePanel2.Name = "imagePanel2";
			this.imagePanel2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel2.PatternImage = null;
			this.imagePanel2.RightImage = null;
			this.imagePanel2.Size = new System.Drawing.Size(473, 27);
			this.imagePanel2.TabIndex = 16;
			this.imagePanel2.TitleText = "사원별";
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Controls.Add(this.imagePanel1, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.imagePanel3, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.grd일일, 1, 1);
			this.tableLayoutPanel3.Controls.Add(this.grd주간, 0, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(949, 736);
			this.tableLayoutPanel3.TabIndex = 0;
			// 
			// imagePanel1
			// 
			this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.imagePanel1.LeftImage = null;
			this.imagePanel1.Location = new System.Drawing.Point(3, 3);
			this.imagePanel1.Name = "imagePanel1";
			this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel1.PatternImage = null;
			this.imagePanel1.RightImage = null;
			this.imagePanel1.Size = new System.Drawing.Size(468, 27);
			this.imagePanel1.TabIndex = 16;
			this.imagePanel1.TitleText = "주간 근무현황";
			// 
			// imagePanel3
			// 
			this.imagePanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.imagePanel3.LeftImage = null;
			this.imagePanel3.Location = new System.Drawing.Point(477, 3);
			this.imagePanel3.Name = "imagePanel3";
			this.imagePanel3.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel3.PatternImage = null;
			this.imagePanel3.RightImage = null;
			this.imagePanel3.Size = new System.Drawing.Size(469, 27);
			this.imagePanel3.TabIndex = 17;
			this.imagePanel3.TitleText = "일일 근무현황";
			// 
			// grd일일
			// 
			this.grd일일.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd일일.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd일일.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd일일.AutoResize = false;
			this.grd일일.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd일일.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd일일.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd일일.EnabledHeaderCheck = true;
			this.grd일일.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd일일.Location = new System.Drawing.Point(477, 36);
			this.grd일일.Name = "grd일일";
			this.grd일일.Rows.Count = 1;
			this.grd일일.Rows.DefaultSize = 18;
			this.grd일일.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd일일.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd일일.ShowSort = false;
			this.grd일일.Size = new System.Drawing.Size(469, 697);
			this.grd일일.StyleInfo = resources.GetString("grd일일.StyleInfo");
			this.grd일일.TabIndex = 19;
			this.grd일일.UseGridCalculator = true;
			// 
			// grd주간
			// 
			this.grd주간.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd주간.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd주간.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd주간.AutoResize = false;
			this.grd주간.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd주간.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd주간.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd주간.EnabledHeaderCheck = true;
			this.grd주간.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd주간.Location = new System.Drawing.Point(3, 36);
			this.grd주간.Name = "grd주간";
			this.grd주간.Rows.Count = 1;
			this.grd주간.Rows.DefaultSize = 18;
			this.grd주간.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd주간.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd주간.ShowSort = false;
			this.grd주간.Size = new System.Drawing.Size(468, 697);
			this.grd주간.StyleInfo = resources.GetString("grd주간.StyleInfo");
			this.grd주간.TabIndex = 18;
			this.grd주간.UseGridCalculator = true;
			// 
			// btn급여반영
			// 
			this.btn급여반영.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn급여반영.BackColor = System.Drawing.Color.White;
			this.btn급여반영.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn급여반영.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn급여반영.Location = new System.Drawing.Point(1368, 10);
			this.btn급여반영.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn급여반영.Name = "btn급여반영";
			this.btn급여반영.Size = new System.Drawing.Size(70, 19);
			this.btn급여반영.TabIndex = 3;
			this.btn급여반영.TabStop = false;
			this.btn급여반영.Text = "급여반영";
			this.btn급여반영.UseVisualStyleBackColor = false;
			// 
			// P_CZ_HR_WORKTIME_PAY
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btn급여반영);
			this.Name = "P_CZ_HR_WORKTIME_PAY";
			this.Size = new System.Drawing.Size(1438, 839);
			this.TitleText = "P_CZ_HR_WORKTIME_PAY";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btn급여반영, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.pnl부서.ResumeLayout(false);
			this.pnl참조.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp근무년월)).EndInit();
			this.spc메인.Panel1.ResumeLayout(false);
			this.spc메인.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spc메인)).EndInit();
			this.spc메인.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd라인)).EndInit();
			this.tableLayoutPanel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd일일)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grd주간)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private System.Windows.Forms.SplitContainer spc메인;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private Duzon.Common.Controls.ImagePanel imagePanel2;
		private Dass.FlexGrid.FlexGrid grd라인;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl pnl참조;
		private Duzon.Common.Controls.DatePicker dtp근무년월;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Duzon.Common.BpControls.BpPanelControl pnl부서;
		private Duzon.Common.BpControls.BpCodeTextBox ctx부서;
		private Duzon.Common.Controls.LabelExt labelExt5;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.BpControls.BpCodeTextBox ctx팀;
		private Duzon.Common.Controls.LabelExt labelExt2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.BpControls.BpCodeTextBox ctx사원;
		private Duzon.Common.Controls.LabelExt labelExt3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private Duzon.Common.Controls.ImagePanel imagePanel1;
		private Duzon.Common.Controls.ImagePanel imagePanel3;
		private Dass.FlexGrid.FlexGrid grd주간;
		private Dass.FlexGrid.FlexGrid grd일일;
		private Duzon.Common.Controls.RoundedButton btn급여반영;
	}
}
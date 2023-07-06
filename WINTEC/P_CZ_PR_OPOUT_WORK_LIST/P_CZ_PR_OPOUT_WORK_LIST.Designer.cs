
namespace cz
{
	partial class P_CZ_PR_OPOUT_WORK_LIST
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_OPOUT_WORK_LIST));
			this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
			this.ctx공정 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.ctx작업장 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.ctx품목To = new Duzon.Common.BpControls.BpCodeTextBox();
			this.ctx품목From = new Duzon.Common.BpControls.BpCodeTextBox();
			this.ctx외주처 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl공장 = new Duzon.Common.Controls.LabelExt();
			this.lbl사업장 = new Duzon.Common.Controls.LabelExt();
			this.cbo사업장 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl실적일자 = new Duzon.Common.Controls.LabelExt();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpP_Plant = new Duzon.Common.BpControls.BpPanelControl();
			this.bpP_Bizarea = new Duzon.Common.BpControls.BpPanelControl();
			this.bpP_Work = new Duzon.Common.BpControls.BpPanelControl();
			this.dat_Work = new Duzon.Common.Controls.PeriodPicker();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl9 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk불량처리제외여부 = new Duzon.Common.Controls.CheckBoxExt();
			this.lbl불량처리제외여부 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.lbl외주처 = new Duzon.Common.Controls.LabelExt();
			this.lbl작업장 = new Duzon.Common.Controls.LabelExt();
			this.lbl공정 = new Duzon.Common.Controls.LabelExt();
			this.lbl품목 = new Duzon.Common.Controls.LabelExt();
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpP_Plant.SuspendLayout();
			this.bpP_Bizarea.SuspendLayout();
			this.bpP_Work.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl9.SuspendLayout();
			this.bpPanelControl7.SuspendLayout();
			this.bpPanelControl8.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel2);
			this.mDataArea.Size = new System.Drawing.Size(1060, 756);
			// 
			// cbo공장
			// 
			this.cbo공장.AutoDropDown = true;
			this.cbo공장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo공장.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo공장.FormattingEnabled = true;
			this.cbo공장.ItemHeight = 12;
			this.cbo공장.Location = new System.Drawing.Point(107, 0);
			this.cbo공장.Name = "cbo공장";
			this.cbo공장.Size = new System.Drawing.Size(185, 20);
			this.cbo공장.TabIndex = 53;
			// 
			// ctx공정
			// 
			this.ctx공정.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx공정.HelpID = Duzon.Common.Forms.Help.HelpID.P_PR_WCOP_SUB;
			this.ctx공정.LabelWidth = 156;
			this.ctx공정.Location = new System.Drawing.Point(107, 0);
			this.ctx공정.Name = "ctx공정";
			this.ctx공정.Size = new System.Drawing.Size(185, 21);
			this.ctx공정.TabIndex = 52;
			this.ctx공정.TabStop = false;
			this.ctx공정.Text = "공정";
			// 
			// ctx작업장
			// 
			this.ctx작업장.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx작업장.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_WC_SUB;
			this.ctx작업장.LabelWidth = 156;
			this.ctx작업장.Location = new System.Drawing.Point(107, 0);
			this.ctx작업장.Name = "ctx작업장";
			this.ctx작업장.Size = new System.Drawing.Size(185, 21);
			this.ctx작업장.TabIndex = 51;
			this.ctx작업장.TabStop = false;
			this.ctx작업장.Text = "작업장";
			// 
			// ctx품목To
			// 
			this.ctx품목To.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
			this.ctx품목To.Location = new System.Drawing.Point(310, 0);
			this.ctx품목To.Name = "ctx품목To";
			this.ctx품목To.Size = new System.Drawing.Size(185, 21);
			this.ctx품목To.TabIndex = 49;
			this.ctx품목To.TabStop = false;
			this.ctx품목To.Text = "bpCodeTextBox2";
			// 
			// ctx품목From
			// 
			this.ctx품목From.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
			this.ctx품목From.LabelWidth = 156;
			this.ctx품목From.Location = new System.Drawing.Point(107, 0);
			this.ctx품목From.Name = "ctx품목From";
			this.ctx품목From.Size = new System.Drawing.Size(185, 21);
			this.ctx품목From.TabIndex = 48;
			this.ctx품목From.TabStop = false;
			this.ctx품목From.Text = "품목";
			// 
			// ctx외주처
			// 
			this.ctx외주처.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx외주처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
			this.ctx외주처.LabelWidth = 156;
			this.ctx외주처.Location = new System.Drawing.Point(107, 0);
			this.ctx외주처.Name = "ctx외주처";
			this.ctx외주처.Size = new System.Drawing.Size(185, 21);
			this.ctx외주처.TabIndex = 47;
			this.ctx외주처.TabStop = false;
			this.ctx외주처.Text = "외주처";
			// 
			// lbl공장
			// 
			this.lbl공장.BackColor = System.Drawing.Color.Transparent;
			this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl공장.Location = new System.Drawing.Point(0, 0);
			this.lbl공장.Name = "lbl공장";
			this.lbl공장.Size = new System.Drawing.Size(100, 23);
			this.lbl공장.TabIndex = 0;
			this.lbl공장.Text = "공장";
			this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl사업장
			// 
			this.lbl사업장.BackColor = System.Drawing.Color.Transparent;
			this.lbl사업장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl사업장.Location = new System.Drawing.Point(0, 0);
			this.lbl사업장.Name = "lbl사업장";
			this.lbl사업장.Size = new System.Drawing.Size(100, 23);
			this.lbl사업장.TabIndex = 0;
			this.lbl사업장.Text = "사업장";
			this.lbl사업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo사업장
			// 
			this.cbo사업장.AutoDropDown = true;
			this.cbo사업장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
			this.cbo사업장.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo사업장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사업장.FormattingEnabled = true;
			this.cbo사업장.ItemHeight = 12;
			this.cbo사업장.Location = new System.Drawing.Point(107, 0);
			this.cbo사업장.Name = "cbo사업장";
			this.cbo사업장.Size = new System.Drawing.Size(185, 20);
			this.cbo사업장.TabIndex = 41;
			// 
			// lbl실적일자
			// 
			this.lbl실적일자.BackColor = System.Drawing.Color.Transparent;
			this.lbl실적일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl실적일자.Location = new System.Drawing.Point(0, 0);
			this.lbl실적일자.Name = "lbl실적일자";
			this.lbl실적일자.Size = new System.Drawing.Size(100, 23);
			this.lbl실적일자.TabIndex = 0;
			this.lbl실적일자.Text = "실적일자";
			this.lbl실적일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this._flex, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1060, 756);
			this.tableLayoutPanel2.TabIndex = 2;
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
			this._flex.Size = new System.Drawing.Size(1054, 659);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 233;
			this._flex.UseGridCalculator = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1054, 85);
			this.oneGrid1.TabIndex = 234;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpP_Plant);
			this.oneGridItem1.Controls.Add(this.bpP_Bizarea);
			this.oneGridItem1.Controls.Add(this.bpP_Work);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1044, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpP_Plant
			// 
			this.bpP_Plant.Controls.Add(this.lbl공장);
			this.bpP_Plant.Controls.Add(this.cbo공장);
			this.bpP_Plant.Location = new System.Drawing.Point(590, 1);
			this.bpP_Plant.Name = "bpP_Plant";
			this.bpP_Plant.Size = new System.Drawing.Size(292, 23);
			this.bpP_Plant.TabIndex = 0;
			this.bpP_Plant.Text = "bpPanelControl1";
			// 
			// bpP_Bizarea
			// 
			this.bpP_Bizarea.Controls.Add(this.lbl사업장);
			this.bpP_Bizarea.Controls.Add(this.cbo사업장);
			this.bpP_Bizarea.Location = new System.Drawing.Point(296, 1);
			this.bpP_Bizarea.Name = "bpP_Bizarea";
			this.bpP_Bizarea.Size = new System.Drawing.Size(292, 23);
			this.bpP_Bizarea.TabIndex = 0;
			this.bpP_Bizarea.Text = "bpPanelControl1";
			// 
			// bpP_Work
			// 
			this.bpP_Work.Controls.Add(this.dat_Work);
			this.bpP_Work.Controls.Add(this.lbl실적일자);
			this.bpP_Work.Location = new System.Drawing.Point(2, 1);
			this.bpP_Work.Name = "bpP_Work";
			this.bpP_Work.Size = new System.Drawing.Size(292, 23);
			this.bpP_Work.TabIndex = 0;
			this.bpP_Work.Text = "bpPanelControl1";
			// 
			// dat_Work
			// 
			this.dat_Work.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dat_Work.Dock = System.Windows.Forms.DockStyle.Right;
			this.dat_Work.IsNecessaryCondition = true;
			this.dat_Work.Location = new System.Drawing.Point(107, 0);
			this.dat_Work.MaximumSize = new System.Drawing.Size(185, 21);
			this.dat_Work.MinimumSize = new System.Drawing.Size(185, 21);
			this.dat_Work.Name = "dat_Work";
			this.dat_Work.Size = new System.Drawing.Size(185, 21);
			this.dat_Work.TabIndex = 8;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl9);
			this.oneGridItem2.Controls.Add(this.bpPanelControl7);
			this.oneGridItem2.Controls.Add(this.bpPanelControl8);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1044, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl9
			// 
			this.bpPanelControl9.Controls.Add(this.lbl공정);
			this.bpPanelControl9.Controls.Add(this.ctx공정);
			this.bpPanelControl9.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl9.Name = "bpPanelControl9";
			this.bpPanelControl9.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl9.TabIndex = 0;
			this.bpPanelControl9.Text = "bpPanelControl1";
			// 
			// bpPanelControl7
			// 
			this.bpPanelControl7.Controls.Add(this.lbl작업장);
			this.bpPanelControl7.Controls.Add(this.ctx작업장);
			this.bpPanelControl7.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl7.Name = "bpPanelControl7";
			this.bpPanelControl7.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl7.TabIndex = 0;
			this.bpPanelControl7.Text = "bpPanelControl1";
			// 
			// bpPanelControl8
			// 
			this.bpPanelControl8.Controls.Add(this.lbl외주처);
			this.bpPanelControl8.Controls.Add(this.ctx외주처);
			this.bpPanelControl8.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl8.Name = "bpPanelControl8";
			this.bpPanelControl8.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl8.TabIndex = 0;
			this.bpPanelControl8.Text = "bpPanelControl1";
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl2);
			this.oneGridItem3.Controls.Add(this.bpPanelControl1);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(1044, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.chk불량처리제외여부);
			this.bpPanelControl2.Controls.Add(this.lbl불량처리제외여부);
			this.bpPanelControl2.Location = new System.Drawing.Point(505, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 2;
			// 
			// chk불량처리제외여부
			// 
			this.chk불량처리제외여부.Location = new System.Drawing.Point(157, 1);
			this.chk불량처리제외여부.Name = "chk불량처리제외여부";
			this.chk불량처리제외여부.Size = new System.Drawing.Size(104, 22);
			this.chk불량처리제외여부.TabIndex = 1;
			this.chk불량처리제외여부.TextDD = null;
			this.chk불량처리제외여부.UseVisualStyleBackColor = true;
			// 
			// lbl불량처리제외여부
			// 
			this.lbl불량처리제외여부.BackColor = System.Drawing.Color.Transparent;
			this.lbl불량처리제외여부.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl불량처리제외여부.Location = new System.Drawing.Point(0, 0);
			this.lbl불량처리제외여부.Name = "lbl불량처리제외여부";
			this.lbl불량처리제외여부.Size = new System.Drawing.Size(151, 23);
			this.lbl불량처리제외여부.TabIndex = 0;
			this.lbl불량처리제외여부.Text = "불량처리제외여부";
			this.lbl불량처리제외여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.lbl품목);
			this.bpPanelControl1.Controls.Add(this.labelExt2);
			this.bpPanelControl1.Controls.Add(this.ctx품목From);
			this.bpPanelControl1.Controls.Add(this.ctx품목To);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(501, 23);
			this.bpPanelControl1.TabIndex = 1;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// labelExt2
			// 
			this.labelExt2.BackColor = System.Drawing.Color.Transparent;
			this.labelExt2.Location = new System.Drawing.Point(294, 1);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Size = new System.Drawing.Size(15, 21);
			this.labelExt2.TabIndex = 9;
			this.labelExt2.Text = "~";
			this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl외주처
			// 
			this.lbl외주처.BackColor = System.Drawing.Color.Transparent;
			this.lbl외주처.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl외주처.Location = new System.Drawing.Point(0, 0);
			this.lbl외주처.Name = "lbl외주처";
			this.lbl외주처.Size = new System.Drawing.Size(100, 23);
			this.lbl외주처.TabIndex = 48;
			this.lbl외주처.Text = "외주처";
			this.lbl외주처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl작업장
			// 
			this.lbl작업장.BackColor = System.Drawing.Color.Transparent;
			this.lbl작업장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl작업장.Location = new System.Drawing.Point(0, 0);
			this.lbl작업장.Name = "lbl작업장";
			this.lbl작업장.Size = new System.Drawing.Size(100, 23);
			this.lbl작업장.TabIndex = 52;
			this.lbl작업장.Text = "작업장";
			this.lbl작업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl공정
			// 
			this.lbl공정.BackColor = System.Drawing.Color.Transparent;
			this.lbl공정.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl공정.Location = new System.Drawing.Point(0, 0);
			this.lbl공정.Name = "lbl공정";
			this.lbl공정.Size = new System.Drawing.Size(100, 23);
			this.lbl공정.TabIndex = 53;
			this.lbl공정.Text = "공정";
			this.lbl공정.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl품목
			// 
			this.lbl품목.BackColor = System.Drawing.Color.Transparent;
			this.lbl품목.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl품목.Location = new System.Drawing.Point(0, 0);
			this.lbl품목.Name = "lbl품목";
			this.lbl품목.Size = new System.Drawing.Size(100, 23);
			this.lbl품목.TabIndex = 50;
			this.lbl품목.Text = "품목";
			this.lbl품목.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// P_CZ_PR_OPOUT_WORK_LIST
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_PR_OPOUT_WORK_LIST";
			this.Size = new System.Drawing.Size(1060, 796);
			this.TitleText = "공정외주실적리스트";
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpP_Plant.ResumeLayout(false);
			this.bpP_Bizarea.ResumeLayout(false);
			this.bpP_Work.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl9.ResumeLayout(false);
			this.bpPanelControl7.ResumeLayout(false);
			this.bpPanelControl8.ResumeLayout(false);
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.ResumeLayout(false);

        }
        #endregion
        private Duzon.Common.Controls.DropDownComboBox cbo공장;
        private Duzon.Common.BpControls.BpCodeTextBox ctx공정;
        private Duzon.Common.BpControls.BpCodeTextBox ctx작업장;
        private Duzon.Common.BpControls.BpCodeTextBox ctx품목To;
        private Duzon.Common.BpControls.BpCodeTextBox ctx품목From;
        private Duzon.Common.BpControls.BpCodeTextBox ctx외주처;
        private Duzon.Common.Controls.LabelExt lbl공장;
        private Duzon.Common.Controls.LabelExt lbl사업장;
        private Duzon.Common.Controls.DropDownComboBox cbo사업장;
        private Duzon.Common.Controls.LabelExt lbl실적일자;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Common.BpControls.BpPanelControl bpP_Plant;
        private Duzon.Common.BpControls.BpPanelControl bpP_Bizarea;
        private Duzon.Common.BpControls.BpPanelControl bpP_Work;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl9;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl8;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.PeriodPicker dat_Work;
        private Duzon.Common.Controls.LabelExt labelExt2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.CheckBoxExt chk불량처리제외여부;
        private Duzon.Common.Controls.LabelExt lbl불량처리제외여부;
		private Duzon.Common.Controls.LabelExt lbl외주처;
		private Duzon.Common.Controls.LabelExt lbl작업장;
		private Duzon.Common.Controls.LabelExt lbl공정;
		private Duzon.Common.Controls.LabelExt lbl품목;
	}
}
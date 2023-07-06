using Dass.FlexGrid;
using System.Windows.Forms;

namespace cz
{
	partial class P_CZ_PR_WO_MNG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_WO_MNG));
			this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpg공정 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid2 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem7 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.btn임의입력 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.bpPanelControl15 = new Duzon.Common.BpControls.BpPanelControl();
			this.cur순번To = new Duzon.Common.Controls.CurrencyTextBox();
			this.lbl순번To = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl14 = new Duzon.Common.BpControls.BpPanelControl();
			this.cur순번From = new Duzon.Common.Controls.CurrencyTextBox();
			this.lbl순번From = new Duzon.Common.Controls.LabelExt();
			this.lbl측정치 = new Duzon.Common.Controls.LabelExt();
			this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg측정치 = new System.Windows.Forms.TabPage();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this._flexDH = new Dass.FlexGrid.FlexGrid(this.components);
			this._flexDL = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl지시구분 = new System.Windows.Forms.Label();
			this.cbo지시구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.periodPicker1 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl작업기간 = new System.Windows.Forms.Label();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl공장 = new System.Windows.Forms.Label();
			this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl13 = new Duzon.Common.BpControls.BpPanelControl();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.chk계획 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk확정 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk발행 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk시작 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk마감 = new Duzon.Common.Controls.CheckBoxExt();
			this.lbl오더상태 = new System.Windows.Forms.Label();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl프로젝트 = new System.Windows.Forms.Label();
			this.ctx프로젝트 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx품목군 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl품목군 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo경로유형 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl경로유형 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc오더형태 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl오더형태 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem4 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl11 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc제품군 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl제품군 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl10 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc공정 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl공정 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl9 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc작업장 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl작업장 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem5 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl12 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctxSFT = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lblSFT = new Duzon.Common.Controls.LabelExt();
			this.bpPnl_NO_WO = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx작업지시번호To = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lblFromTo1 = new Duzon.Common.Controls.LabelExt();
			this.ctx작업지시번호From = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl작업지시번호 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem6 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl담당자 = new Duzon.Common.Controls.LabelExt();
			this.ctx = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPnl_CD_ITEM = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx품목To = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lblFromTo2 = new Duzon.Common.Controls.LabelExt();
			this.ctx품목From = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl품목 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem8 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.chk측정항목모두보기 = new Duzon.Common.Controls.CheckBoxExt();
			this.btn측정치삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn소재Heat번호적용 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn열처리번호적용 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.bpPanelControl16 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.cur열처리번호To = new Duzon.Common.Controls.CurrencyTextBox();
			this.cur열처리번호From = new Duzon.Common.Controls.CurrencyTextBox();
			this.txt열처리번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl열처리번호 = new Duzon.Common.Controls.LabelExt();
			this.btn작업지시마감취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn작업지시마감 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn작업지시서출력 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn소요자재정보 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn계획작업지시확정 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn작업지시확정취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn생산투입취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn생산투입 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn작업지시삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn측정치삭제1 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this._tlayMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tpg공정.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem7.SuspendLayout();
			this.bpPanelControl15.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur순번To)).BeginInit();
			this.bpPanelControl14.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur순번From)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
			this.tpg측정치.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexDH)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flexDL)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl13.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl8.SuspendLayout();
			this.bpPanelControl7.SuspendLayout();
			this.bpPanelControl6.SuspendLayout();
			this.oneGridItem4.SuspendLayout();
			this.bpPanelControl11.SuspendLayout();
			this.bpPanelControl10.SuspendLayout();
			this.bpPanelControl9.SuspendLayout();
			this.oneGridItem5.SuspendLayout();
			this.bpPanelControl12.SuspendLayout();
			this.bpPnl_NO_WO.SuspendLayout();
			this.oneGridItem6.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.bpPnl_CD_ITEM.SuspendLayout();
			this.oneGridItem8.SuspendLayout();
			this.bpPanelControl16.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur열처리번호To)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cur열처리번호From)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this._tlayMain);
			this.mDataArea.Size = new System.Drawing.Size(1589, 816);
			// 
			// _tlayMain
			// 
			this._tlayMain.AutoSize = true;
			this._tlayMain.ColumnCount = 1;
			this._tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this._tlayMain.Controls.Add(this.splitContainer1, 0, 1);
			this._tlayMain.Controls.Add(this.oneGrid1, 0, 0);
			this._tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this._tlayMain.Location = new System.Drawing.Point(0, 0);
			this._tlayMain.Name = "_tlayMain";
			this._tlayMain.RowCount = 2;
			this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 183F));
			this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this._tlayMain.Size = new System.Drawing.Size(1589, 816);
			this._tlayMain.TabIndex = 97;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 186);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._flexH);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(1583, 627);
			this.splitContainer1.SplitterDistance = 297;
			this.splitContainer1.TabIndex = 13;
			// 
			// _flexH
			// 
			this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexH.AutoResize = false;
			this._flexH.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexH.EnabledHeaderCheck = true;
			this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexH.Location = new System.Drawing.Point(0, 0);
			this._flexH.Name = "_flexH";
			this._flexH.Rows.Count = 1;
			this._flexH.Rows.DefaultSize = 20;
			this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexH.ShowSort = false;
			this._flexH.Size = new System.Drawing.Size(1583, 297);
			this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
			this._flexH.TabIndex = 12;
			this._flexH.UseGridCalculator = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tpg공정);
			this.tabControl1.Controls.Add(this.tpg측정치);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1583, 326);
			this.tabControl1.TabIndex = 13;
			// 
			// tpg공정
			// 
			this.tpg공정.Controls.Add(this.tableLayoutPanel1);
			this.tpg공정.Location = new System.Drawing.Point(4, 22);
			this.tpg공정.Name = "tpg공정";
			this.tpg공정.Padding = new System.Windows.Forms.Padding(3);
			this.tpg공정.Size = new System.Drawing.Size(1575, 300);
			this.tpg공정.TabIndex = 0;
			this.tpg공정.Text = "공정";
			this.tpg공정.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this._flexL, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1569, 294);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid2
			// 
			this.oneGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid2.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem7});
			this.oneGrid2.Location = new System.Drawing.Point(3, 252);
			this.oneGrid2.Name = "oneGrid2";
			this.oneGrid2.Size = new System.Drawing.Size(1563, 39);
			this.oneGrid2.TabIndex = 0;
			// 
			// oneGridItem7
			// 
			this.oneGridItem7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem7.Controls.Add(this.btn측정치삭제1);
			this.oneGridItem7.Controls.Add(this.btn임의입력);
			this.oneGridItem7.Controls.Add(this.bpPanelControl15);
			this.oneGridItem7.Controls.Add(this.bpPanelControl14);
			this.oneGridItem7.Controls.Add(this.lbl측정치);
			this.oneGridItem7.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem7.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem7.Name = "oneGridItem7";
			this.oneGridItem7.Size = new System.Drawing.Size(1553, 23);
			this.oneGridItem7.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem7.TabIndex = 0;
			// 
			// btn임의입력
			// 
			this.btn임의입력.BackColor = System.Drawing.Color.Transparent;
			this.btn임의입력.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn임의입력.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn임의입력.Location = new System.Drawing.Point(692, 1);
			this.btn임의입력.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn임의입력.Name = "btn임의입력";
			this.btn임의입력.Size = new System.Drawing.Size(70, 19);
			this.btn임의입력.TabIndex = 0;
			this.btn임의입력.TabStop = false;
			this.btn임의입력.Text = "임의입력";
			this.btn임의입력.UseVisualStyleBackColor = false;
			// 
			// bpPanelControl15
			// 
			this.bpPanelControl15.Controls.Add(this.cur순번To);
			this.bpPanelControl15.Controls.Add(this.lbl순번To);
			this.bpPanelControl15.Location = new System.Drawing.Point(398, 1);
			this.bpPanelControl15.Name = "bpPanelControl15";
			this.bpPanelControl15.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl15.TabIndex = 1;
			this.bpPanelControl15.Text = "bpPanelControl15";
			// 
			// cur순번To
			// 
			this.cur순번To.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur순번To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur순번To.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur순번To.Dock = System.Windows.Forms.DockStyle.Right;
			this.cur순번To.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur순번To.Location = new System.Drawing.Point(106, 0);
			this.cur순번To.Name = "cur순번To";
			this.cur순번To.NullString = "0";
			this.cur순번To.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur순번To.Size = new System.Drawing.Size(186, 21);
			this.cur순번To.TabIndex = 1;
			this.cur순번To.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lbl순번To
			// 
			this.lbl순번To.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl순번To.Location = new System.Drawing.Point(0, 0);
			this.lbl순번To.Name = "lbl순번To";
			this.lbl순번To.Size = new System.Drawing.Size(100, 23);
			this.lbl순번To.TabIndex = 0;
			this.lbl순번To.Text = "순번(To)";
			this.lbl순번To.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl14
			// 
			this.bpPanelControl14.Controls.Add(this.cur순번From);
			this.bpPanelControl14.Controls.Add(this.lbl순번From);
			this.bpPanelControl14.Location = new System.Drawing.Point(104, 1);
			this.bpPanelControl14.Name = "bpPanelControl14";
			this.bpPanelControl14.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl14.TabIndex = 0;
			this.bpPanelControl14.Text = "bpPanelControl14";
			// 
			// cur순번From
			// 
			this.cur순번From.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur순번From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur순번From.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur순번From.Dock = System.Windows.Forms.DockStyle.Right;
			this.cur순번From.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur순번From.Location = new System.Drawing.Point(106, 0);
			this.cur순번From.Name = "cur순번From";
			this.cur순번From.NullString = "0";
			this.cur순번From.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur순번From.Size = new System.Drawing.Size(186, 21);
			this.cur순번From.TabIndex = 1;
			this.cur순번From.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lbl순번From
			// 
			this.lbl순번From.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl순번From.Location = new System.Drawing.Point(0, 0);
			this.lbl순번From.Name = "lbl순번From";
			this.lbl순번From.Size = new System.Drawing.Size(100, 23);
			this.lbl순번From.TabIndex = 0;
			this.lbl순번From.Text = "순번(From)";
			this.lbl순번From.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl측정치
			// 
			this.lbl측정치.Location = new System.Drawing.Point(2, 1);
			this.lbl측정치.Name = "lbl측정치";
			this.lbl측정치.Size = new System.Drawing.Size(100, 23);
			this.lbl측정치.TabIndex = 2;
			this.lbl측정치.Text = "측정치";
			this.lbl측정치.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _flexL
			// 
			this._flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexL.AutoResize = false;
			this._flexL.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexL.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexL.EnabledHeaderCheck = true;
			this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexL.Location = new System.Drawing.Point(3, 3);
			this._flexL.Name = "_flexL";
			this._flexL.Rows.Count = 1;
			this._flexL.Rows.DefaultSize = 20;
			this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexL.ShowSort = false;
			this._flexL.Size = new System.Drawing.Size(1563, 243);
			this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
			this._flexL.TabIndex = 14;
			this._flexL.UseGridCalculator = true;
			// 
			// tpg측정치
			// 
			this.tpg측정치.Controls.Add(this.splitContainer2);
			this.tpg측정치.Location = new System.Drawing.Point(4, 22);
			this.tpg측정치.Name = "tpg측정치";
			this.tpg측정치.Padding = new System.Windows.Forms.Padding(3);
			this.tpg측정치.Size = new System.Drawing.Size(1575, 300);
			this.tpg측정치.TabIndex = 1;
			this.tpg측정치.Text = "측정치";
			this.tpg측정치.UseVisualStyleBackColor = true;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(3, 3);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this._flexDH);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this._flexDL);
			this.splitContainer2.Size = new System.Drawing.Size(1569, 294);
			this.splitContainer2.SplitterDistance = 523;
			this.splitContainer2.TabIndex = 2;
			// 
			// _flexDH
			// 
			this._flexDH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexDH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexDH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexDH.AutoResize = false;
			this._flexDH.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexDH.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexDH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexDH.EnabledHeaderCheck = true;
			this._flexDH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexDH.Location = new System.Drawing.Point(0, 0);
			this._flexDH.Name = "_flexDH";
			this._flexDH.Rows.Count = 1;
			this._flexDH.Rows.DefaultSize = 20;
			this._flexDH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexDH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexDH.ShowSort = false;
			this._flexDH.Size = new System.Drawing.Size(523, 294);
			this._flexDH.StyleInfo = resources.GetString("_flexDH.StyleInfo");
			this._flexDH.TabIndex = 3;
			this._flexDH.UseGridCalculator = true;
			// 
			// _flexDL
			// 
			this._flexDL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexDL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexDL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexDL.AutoResize = false;
			this._flexDL.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexDL.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexDL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexDL.EnabledHeaderCheck = true;
			this._flexDL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexDL.Location = new System.Drawing.Point(0, 0);
			this._flexDL.Name = "_flexDL";
			this._flexDL.Rows.Count = 1;
			this._flexDL.Rows.DefaultSize = 20;
			this._flexDL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexDL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexDL.ShowSort = false;
			this._flexDL.Size = new System.Drawing.Size(1042, 294);
			this._flexDL.StyleInfo = resources.GetString("_flexDL.StyleInfo");
			this._flexDL.TabIndex = 0;
			this._flexDL.UseGridCalculator = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3,
            this.oneGridItem4,
            this.oneGridItem5,
            this.oneGridItem6,
            this.oneGridItem8});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1583, 177);
			this.oneGrid1.TabIndex = 93;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1573, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.lbl지시구분);
			this.bpPanelControl3.Controls.Add(this.cbo지시구분);
			this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 0;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// lbl지시구분
			// 
			this.lbl지시구분.BackColor = System.Drawing.Color.Transparent;
			this.lbl지시구분.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl지시구분.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl지시구분.Location = new System.Drawing.Point(0, 0);
			this.lbl지시구분.Name = "lbl지시구분";
			this.lbl지시구분.Size = new System.Drawing.Size(100, 23);
			this.lbl지시구분.TabIndex = 71;
			this.lbl지시구분.Tag = "지시구분";
			this.lbl지시구분.Text = "지시구분";
			this.lbl지시구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo지시구분
			// 
			this.cbo지시구분.AutoDropDown = true;
			this.cbo지시구분.BackColor = System.Drawing.Color.White;
			this.cbo지시구분.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo지시구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo지시구분.ItemHeight = 12;
			this.cbo지시구분.Location = new System.Drawing.Point(106, 0);
			this.cbo지시구분.Name = "cbo지시구분";
			this.cbo지시구분.Size = new System.Drawing.Size(186, 20);
			this.cbo지시구분.TabIndex = 4;
			this.cbo지시구분.UseKeyEnter = false;
			this.cbo지시구분.UseKeyF3 = false;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.periodPicker1);
			this.bpPanelControl2.Controls.Add(this.lbl작업기간);
			this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 0;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// periodPicker1
			// 
			this.periodPicker1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.periodPicker1.Dock = System.Windows.Forms.DockStyle.Right;
			this.periodPicker1.IsNecessaryCondition = true;
			this.periodPicker1.Location = new System.Drawing.Point(107, 0);
			this.periodPicker1.MaximumSize = new System.Drawing.Size(185, 21);
			this.periodPicker1.MinimumSize = new System.Drawing.Size(185, 21);
			this.periodPicker1.Name = "periodPicker1";
			this.periodPicker1.Size = new System.Drawing.Size(185, 21);
			this.periodPicker1.TabIndex = 247;
			// 
			// lbl작업기간
			// 
			this.lbl작업기간.BackColor = System.Drawing.Color.Transparent;
			this.lbl작업기간.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl작업기간.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl작업기간.Location = new System.Drawing.Point(0, 0);
			this.lbl작업기간.Name = "lbl작업기간";
			this.lbl작업기간.Size = new System.Drawing.Size(100, 23);
			this.lbl작업기간.TabIndex = 72;
			this.lbl작업기간.Tag = "작업기간";
			this.lbl작업기간.Text = "작업기간";
			this.lbl작업기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.lbl공장);
			this.bpPanelControl1.Controls.Add(this.cbo공장);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// lbl공장
			// 
			this.lbl공장.BackColor = System.Drawing.Color.Transparent;
			this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl공장.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl공장.Location = new System.Drawing.Point(0, 0);
			this.lbl공장.Name = "lbl공장";
			this.lbl공장.Size = new System.Drawing.Size(100, 23);
			this.lbl공장.TabIndex = 71;
			this.lbl공장.Tag = "공장";
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
			this.cbo공장.UseKeyEnter = false;
			this.cbo공장.UseKeyF3 = false;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl13);
			this.oneGridItem2.Controls.Add(this.bpPanelControl4);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1573, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl13
			// 
			this.bpPanelControl13.Controls.Add(this.flowLayoutPanel2);
			this.bpPanelControl13.Controls.Add(this.lbl오더상태);
			this.bpPanelControl13.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl13.Name = "bpPanelControl13";
			this.bpPanelControl13.Size = new System.Drawing.Size(586, 23);
			this.bpPanelControl13.TabIndex = 1;
			this.bpPanelControl13.Text = "bpPanelControl13";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.chk계획);
			this.flowLayoutPanel2.Controls.Add(this.chk확정);
			this.flowLayoutPanel2.Controls.Add(this.chk발행);
			this.flowLayoutPanel2.Controls.Add(this.chk시작);
			this.flowLayoutPanel2.Controls.Add(this.chk마감);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(107, 0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(479, 23);
			this.flowLayoutPanel2.TabIndex = 73;
			// 
			// chk계획
			// 
			this.chk계획.AutoSize = true;
			this.chk계획.BackColor = System.Drawing.Color.Transparent;
			this.chk계획.Checked = true;
			this.chk계획.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk계획.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.chk계획.Location = new System.Drawing.Point(3, 3);
			this.chk계획.Name = "chk계획";
			this.chk계획.Size = new System.Drawing.Size(66, 16);
			this.chk계획.TabIndex = 5;
			this.chk계획.Text = "P(계획)";
			this.chk계획.TextDD = null;
			this.chk계획.UseVisualStyleBackColor = false;
			// 
			// chk확정
			// 
			this.chk확정.AutoSize = true;
			this.chk확정.BackColor = System.Drawing.Color.Transparent;
			this.chk확정.Checked = true;
			this.chk확정.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk확정.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.chk확정.Location = new System.Drawing.Point(75, 3);
			this.chk확정.Name = "chk확정";
			this.chk확정.Size = new System.Drawing.Size(67, 16);
			this.chk확정.TabIndex = 6;
			this.chk확정.Text = "O(확정)";
			this.chk확정.TextDD = null;
			this.chk확정.UseVisualStyleBackColor = false;
			// 
			// chk발행
			// 
			this.chk발행.AutoSize = true;
			this.chk발행.BackColor = System.Drawing.Color.Transparent;
			this.chk발행.Checked = true;
			this.chk발행.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk발행.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.chk발행.Location = new System.Drawing.Point(148, 3);
			this.chk발행.Name = "chk발행";
			this.chk발행.Size = new System.Drawing.Size(66, 16);
			this.chk발행.TabIndex = 7;
			this.chk발행.Text = "R(발행)";
			this.chk발행.TextDD = null;
			this.chk발행.UseVisualStyleBackColor = false;
			// 
			// chk시작
			// 
			this.chk시작.AutoSize = true;
			this.chk시작.BackColor = System.Drawing.Color.Transparent;
			this.chk시작.Checked = true;
			this.chk시작.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk시작.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.chk시작.Location = new System.Drawing.Point(220, 3);
			this.chk시작.Name = "chk시작";
			this.chk시작.Size = new System.Drawing.Size(66, 16);
			this.chk시작.TabIndex = 8;
			this.chk시작.Text = "S(시작)";
			this.chk시작.TextDD = null;
			this.chk시작.UseVisualStyleBackColor = false;
			// 
			// chk마감
			// 
			this.chk마감.AutoSize = true;
			this.chk마감.BackColor = System.Drawing.Color.Transparent;
			this.chk마감.Checked = true;
			this.chk마감.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk마감.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.chk마감.Location = new System.Drawing.Point(292, 3);
			this.chk마감.Name = "chk마감";
			this.chk마감.Size = new System.Drawing.Size(67, 16);
			this.chk마감.TabIndex = 9;
			this.chk마감.Text = "C(마감)";
			this.chk마감.TextDD = null;
			this.chk마감.UseVisualStyleBackColor = false;
			// 
			// lbl오더상태
			// 
			this.lbl오더상태.BackColor = System.Drawing.Color.Transparent;
			this.lbl오더상태.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl오더상태.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl오더상태.Location = new System.Drawing.Point(0, 0);
			this.lbl오더상태.Name = "lbl오더상태";
			this.lbl오더상태.Size = new System.Drawing.Size(100, 23);
			this.lbl오더상태.TabIndex = 72;
			this.lbl오더상태.Tag = "오더상태";
			this.lbl오더상태.Text = "오더상태";
			this.lbl오더상태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.lbl프로젝트);
			this.bpPanelControl4.Controls.Add(this.ctx프로젝트);
			this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl4.TabIndex = 0;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// lbl프로젝트
			// 
			this.lbl프로젝트.BackColor = System.Drawing.Color.Transparent;
			this.lbl프로젝트.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl프로젝트.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl프로젝트.Location = new System.Drawing.Point(0, 0);
			this.lbl프로젝트.Name = "lbl프로젝트";
			this.lbl프로젝트.Size = new System.Drawing.Size(100, 23);
			this.lbl프로젝트.TabIndex = 229;
			this.lbl프로젝트.Text = "프로젝트";
			this.lbl프로젝트.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx프로젝트
			// 
			this.ctx프로젝트.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx프로젝트.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx프로젝트.Location = new System.Drawing.Point(106, 0);
			this.ctx프로젝트.Name = "ctx프로젝트";
			this.ctx프로젝트.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
			this.ctx프로젝트.Size = new System.Drawing.Size(186, 21);
			this.ctx프로젝트.TabIndex = 243;
			this.ctx프로젝트.TabStop = false;
			this.ctx프로젝트.Tag = "NO_PJT;NM_PJT";
			this.ctx프로젝트.Text = "bpCodeTextBox2";
			this.ctx프로젝트.UserCodeName = "NM_PROJECT";
			this.ctx프로젝트.UserCodeValue = "NO_PROJECT";
			this.ctx프로젝트.UserHelpID = "H_SA_PRJ_SUB";
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl8);
			this.oneGridItem3.Controls.Add(this.bpPanelControl7);
			this.oneGridItem3.Controls.Add(this.bpPanelControl6);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(1573, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// bpPanelControl8
			// 
			this.bpPanelControl8.Controls.Add(this.ctx품목군);
			this.bpPanelControl8.Controls.Add(this.lbl품목군);
			this.bpPanelControl8.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl8.Name = "bpPanelControl8";
			this.bpPanelControl8.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl8.TabIndex = 2;
			this.bpPanelControl8.Text = "bpPanelControl8";
			// 
			// ctx품목군
			// 
			this.ctx품목군.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx품목군.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_ITEMGP_SUB;
			this.ctx품목군.Location = new System.Drawing.Point(106, 0);
			this.ctx품목군.Name = "ctx품목군";
			this.ctx품목군.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
			this.ctx품목군.Size = new System.Drawing.Size(186, 21);
			this.ctx품목군.TabIndex = 244;
			this.ctx품목군.TabStop = false;
			this.ctx품목군.Text = "bpCodeTextBox2";
			// 
			// lbl품목군
			// 
			this.lbl품목군.BackColor = System.Drawing.Color.Transparent;
			this.lbl품목군.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl품목군.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl품목군.Location = new System.Drawing.Point(0, 0);
			this.lbl품목군.Name = "lbl품목군";
			this.lbl품목군.Size = new System.Drawing.Size(100, 23);
			this.lbl품목군.TabIndex = 238;
			this.lbl품목군.Text = "품목군";
			this.lbl품목군.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl7
			// 
			this.bpPanelControl7.Controls.Add(this.cbo경로유형);
			this.bpPanelControl7.Controls.Add(this.lbl경로유형);
			this.bpPanelControl7.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl7.Name = "bpPanelControl7";
			this.bpPanelControl7.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl7.TabIndex = 1;
			this.bpPanelControl7.Text = "bpPanelControl7";
			// 
			// cbo경로유형
			// 
			this.cbo경로유형.AutoDropDown = true;
			this.cbo경로유형.BackColor = System.Drawing.Color.White;
			this.cbo경로유형.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo경로유형.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo경로유형.ItemHeight = 12;
			this.cbo경로유형.Location = new System.Drawing.Point(106, 0);
			this.cbo경로유형.Name = "cbo경로유형";
			this.cbo경로유형.Size = new System.Drawing.Size(186, 20);
			this.cbo경로유형.TabIndex = 239;
			this.cbo경로유형.UseKeyEnter = false;
			this.cbo경로유형.UseKeyF3 = false;
			// 
			// lbl경로유형
			// 
			this.lbl경로유형.BackColor = System.Drawing.Color.Transparent;
			this.lbl경로유형.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl경로유형.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl경로유형.Location = new System.Drawing.Point(0, 0);
			this.lbl경로유형.Name = "lbl경로유형";
			this.lbl경로유형.Size = new System.Drawing.Size(100, 23);
			this.lbl경로유형.TabIndex = 238;
			this.lbl경로유형.Text = "경로유형";
			this.lbl경로유형.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl6
			// 
			this.bpPanelControl6.Controls.Add(this.bpc오더형태);
			this.bpPanelControl6.Controls.Add(this.lbl오더형태);
			this.bpPanelControl6.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl6.Name = "bpPanelControl6";
			this.bpPanelControl6.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl6.TabIndex = 0;
			this.bpPanelControl6.Text = "bpPanelControl6";
			// 
			// bpc오더형태
			// 
			this.bpc오더형태.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc오더형태.HelpID = Duzon.Common.Forms.Help.HelpID.P_PR_TPWO_SUB1;
			this.bpc오더형태.Location = new System.Drawing.Point(106, 0);
			this.bpc오더형태.Name = "bpc오더형태";
			this.bpc오더형태.Size = new System.Drawing.Size(186, 21);
			this.bpc오더형태.TabIndex = 244;
			this.bpc오더형태.TabStop = false;
			// 
			// lbl오더형태
			// 
			this.lbl오더형태.BackColor = System.Drawing.Color.Transparent;
			this.lbl오더형태.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl오더형태.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl오더형태.Location = new System.Drawing.Point(0, 0);
			this.lbl오더형태.Name = "lbl오더형태";
			this.lbl오더형태.Size = new System.Drawing.Size(100, 23);
			this.lbl오더형태.TabIndex = 237;
			this.lbl오더형태.Text = "오더형태";
			this.lbl오더형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem4
			// 
			this.oneGridItem4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem4.Controls.Add(this.bpPanelControl11);
			this.oneGridItem4.Controls.Add(this.bpPanelControl10);
			this.oneGridItem4.Controls.Add(this.bpPanelControl9);
			this.oneGridItem4.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem4.Location = new System.Drawing.Point(0, 69);
			this.oneGridItem4.Name = "oneGridItem4";
			this.oneGridItem4.Size = new System.Drawing.Size(1573, 23);
			this.oneGridItem4.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem4.TabIndex = 3;
			// 
			// bpPanelControl11
			// 
			this.bpPanelControl11.Controls.Add(this.bpc제품군);
			this.bpPanelControl11.Controls.Add(this.lbl제품군);
			this.bpPanelControl11.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl11.Name = "bpPanelControl11";
			this.bpPanelControl11.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl11.TabIndex = 249;
			this.bpPanelControl11.Text = "bpPanelControl11";
			// 
			// bpc제품군
			// 
			this.bpc제품군.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc제품군.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1;
			this.bpc제품군.Location = new System.Drawing.Point(106, 0);
			this.bpc제품군.Name = "bpc제품군";
			this.bpc제품군.Size = new System.Drawing.Size(186, 21);
			this.bpc제품군.TabIndex = 239;
			this.bpc제품군.TabStop = false;
			this.bpc제품군.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// lbl제품군
			// 
			this.lbl제품군.BackColor = System.Drawing.Color.Transparent;
			this.lbl제품군.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl제품군.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl제품군.Location = new System.Drawing.Point(0, 0);
			this.lbl제품군.Name = "lbl제품군";
			this.lbl제품군.Size = new System.Drawing.Size(100, 23);
			this.lbl제품군.TabIndex = 249;
			this.lbl제품군.Text = "제품군";
			this.lbl제품군.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl10
			// 
			this.bpPanelControl10.Controls.Add(this.bpc공정);
			this.bpPanelControl10.Controls.Add(this.lbl공정);
			this.bpPanelControl10.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl10.Name = "bpPanelControl10";
			this.bpPanelControl10.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl10.TabIndex = 1;
			this.bpPanelControl10.Text = "bpPanelControl10";
			// 
			// bpc공정
			// 
			this.bpc공정.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc공정.HelpID = Duzon.Common.Forms.Help.HelpID.P_PR_WCOP_SUB1;
			this.bpc공정.Location = new System.Drawing.Point(106, 0);
			this.bpc공정.Name = "bpc공정";
			this.bpc공정.Size = new System.Drawing.Size(186, 21);
			this.bpc공정.TabIndex = 248;
			this.bpc공정.TabStop = false;
			this.bpc공정.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// lbl공정
			// 
			this.lbl공정.BackColor = System.Drawing.Color.Transparent;
			this.lbl공정.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl공정.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl공정.Location = new System.Drawing.Point(0, 0);
			this.lbl공정.Name = "lbl공정";
			this.lbl공정.Size = new System.Drawing.Size(100, 23);
			this.lbl공정.TabIndex = 247;
			this.lbl공정.Text = "공정";
			this.lbl공정.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl9
			// 
			this.bpPanelControl9.Controls.Add(this.bpc작업장);
			this.bpPanelControl9.Controls.Add(this.lbl작업장);
			this.bpPanelControl9.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl9.Name = "bpPanelControl9";
			this.bpPanelControl9.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl9.TabIndex = 0;
			this.bpPanelControl9.Text = "bpPanelControl9";
			// 
			// bpc작업장
			// 
			this.bpc작업장.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc작업장.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_WC_SUB1;
			this.bpc작업장.Location = new System.Drawing.Point(106, 0);
			this.bpc작업장.Name = "bpc작업장";
			this.bpc작업장.Size = new System.Drawing.Size(186, 21);
			this.bpc작업장.TabIndex = 238;
			this.bpc작업장.TabStop = false;
			this.bpc작업장.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// lbl작업장
			// 
			this.lbl작업장.BackColor = System.Drawing.Color.Transparent;
			this.lbl작업장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl작업장.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl작업장.ForeColor = System.Drawing.Color.Black;
			this.lbl작업장.Location = new System.Drawing.Point(0, 0);
			this.lbl작업장.Name = "lbl작업장";
			this.lbl작업장.Size = new System.Drawing.Size(100, 23);
			this.lbl작업장.TabIndex = 237;
			this.lbl작업장.Text = "작업장";
			this.lbl작업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem5
			// 
			this.oneGridItem5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem5.Controls.Add(this.bpPanelControl12);
			this.oneGridItem5.Controls.Add(this.bpPnl_NO_WO);
			this.oneGridItem5.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem5.Location = new System.Drawing.Point(0, 92);
			this.oneGridItem5.Name = "oneGridItem5";
			this.oneGridItem5.Size = new System.Drawing.Size(1573, 23);
			this.oneGridItem5.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem5.TabIndex = 4;
			// 
			// bpPanelControl12
			// 
			this.bpPanelControl12.Controls.Add(this.ctxSFT);
			this.bpPanelControl12.Controls.Add(this.lblSFT);
			this.bpPanelControl12.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl12.Name = "bpPanelControl12";
			this.bpPanelControl12.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl12.TabIndex = 1;
			this.bpPanelControl12.Text = "bpPanelControl12";
			// 
			// ctxSFT
			// 
			this.ctxSFT.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctxSFT.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctxSFT.Location = new System.Drawing.Point(106, 0);
			this.ctxSFT.Name = "ctxSFT";
			this.ctxSFT.Size = new System.Drawing.Size(186, 21);
			this.ctxSFT.TabIndex = 250;
			this.ctxSFT.TabStop = false;
			this.ctxSFT.UserCodeName = "NM_SFT";
			this.ctxSFT.UserCodeValue = "NO_SFT";
			this.ctxSFT.UserHelpID = "H_PR_SFT_SUB";
			this.ctxSFT.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// lblSFT
			// 
			this.lblSFT.BackColor = System.Drawing.Color.Transparent;
			this.lblSFT.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblSFT.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lblSFT.Location = new System.Drawing.Point(0, 0);
			this.lblSFT.Name = "lblSFT";
			this.lblSFT.Size = new System.Drawing.Size(100, 23);
			this.lblSFT.TabIndex = 249;
			this.lblSFT.Text = "SFT";
			this.lblSFT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPnl_NO_WO
			// 
			this.bpPnl_NO_WO.Controls.Add(this.ctx작업지시번호To);
			this.bpPnl_NO_WO.Controls.Add(this.lblFromTo1);
			this.bpPnl_NO_WO.Controls.Add(this.ctx작업지시번호From);
			this.bpPnl_NO_WO.Controls.Add(this.lbl작업지시번호);
			this.bpPnl_NO_WO.Location = new System.Drawing.Point(2, 1);
			this.bpPnl_NO_WO.Name = "bpPnl_NO_WO";
			this.bpPnl_NO_WO.Size = new System.Drawing.Size(586, 23);
			this.bpPnl_NO_WO.TabIndex = 0;
			this.bpPnl_NO_WO.Text = "bpPanelControl11";
			// 
			// ctx작업지시번호To
			// 
			this.ctx작업지시번호To.HelpID = Duzon.Common.Forms.Help.HelpID.P_PR_WO_REG_SUB;
			this.ctx작업지시번호To.Location = new System.Drawing.Point(314, 1);
			this.ctx작업지시번호To.Name = "ctx작업지시번호To";
			this.ctx작업지시번호To.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
			this.ctx작업지시번호To.Size = new System.Drawing.Size(186, 21);
			this.ctx작업지시번호To.TabIndex = 252;
			this.ctx작업지시번호To.TabStop = false;
			this.ctx작업지시번호To.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// lblFromTo1
			// 
			this.lblFromTo1.BackColor = System.Drawing.Color.Transparent;
			this.lblFromTo1.Location = new System.Drawing.Point(295, 1);
			this.lblFromTo1.Name = "lblFromTo1";
			this.lblFromTo1.Size = new System.Drawing.Size(15, 21);
			this.lblFromTo1.TabIndex = 251;
			this.lblFromTo1.Text = "~";
			this.lblFromTo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ctx작업지시번호From
			// 
			this.ctx작업지시번호From.HelpID = Duzon.Common.Forms.Help.HelpID.P_PR_WO_REG_SUB;
			this.ctx작업지시번호From.Location = new System.Drawing.Point(106, 1);
			this.ctx작업지시번호From.Name = "ctx작업지시번호From";
			this.ctx작업지시번호From.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
			this.ctx작업지시번호From.Size = new System.Drawing.Size(186, 21);
			this.ctx작업지시번호From.TabIndex = 249;
			this.ctx작업지시번호From.TabStop = false;
			this.ctx작업지시번호From.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryAfter);
			this.ctx작업지시번호From.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// lbl작업지시번호
			// 
			this.lbl작업지시번호.BackColor = System.Drawing.Color.Transparent;
			this.lbl작업지시번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl작업지시번호.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl작업지시번호.Location = new System.Drawing.Point(0, 0);
			this.lbl작업지시번호.Name = "lbl작업지시번호";
			this.lbl작업지시번호.Size = new System.Drawing.Size(100, 23);
			this.lbl작업지시번호.TabIndex = 248;
			this.lbl작업지시번호.Text = "작업지시번호";
			this.lbl작업지시번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem6
			// 
			this.oneGridItem6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem6.Controls.Add(this.bpPanelControl5);
			this.oneGridItem6.Controls.Add(this.bpPnl_CD_ITEM);
			this.oneGridItem6.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem6.Location = new System.Drawing.Point(0, 115);
			this.oneGridItem6.Name = "oneGridItem6";
			this.oneGridItem6.Size = new System.Drawing.Size(1573, 23);
			this.oneGridItem6.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem6.TabIndex = 5;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.lbl담당자);
			this.bpPanelControl5.Controls.Add(this.ctx);
			this.bpPanelControl5.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl5.TabIndex = 2;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// lbl담당자
			// 
			this.lbl담당자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl담당자.Location = new System.Drawing.Point(0, 0);
			this.lbl담당자.Name = "lbl담당자";
			this.lbl담당자.Size = new System.Drawing.Size(100, 23);
			this.lbl담당자.TabIndex = 3;
			this.lbl담당자.Text = "담당자";
			this.lbl담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx
			// 
			this.ctx.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.ctx.LabelWidth = 156;
			this.ctx.Location = new System.Drawing.Point(106, 0);
			this.ctx.Name = "ctx";
			this.ctx.Size = new System.Drawing.Size(186, 21);
			this.ctx.TabIndex = 2;
			this.ctx.TabStop = false;
			// 
			// bpPnl_CD_ITEM
			// 
			this.bpPnl_CD_ITEM.Controls.Add(this.ctx품목To);
			this.bpPnl_CD_ITEM.Controls.Add(this.lblFromTo2);
			this.bpPnl_CD_ITEM.Controls.Add(this.ctx품목From);
			this.bpPnl_CD_ITEM.Controls.Add(this.lbl품목);
			this.bpPnl_CD_ITEM.Location = new System.Drawing.Point(2, 1);
			this.bpPnl_CD_ITEM.Name = "bpPnl_CD_ITEM";
			this.bpPnl_CD_ITEM.Size = new System.Drawing.Size(586, 23);
			this.bpPnl_CD_ITEM.TabIndex = 1;
			this.bpPnl_CD_ITEM.Text = "bpPanelControl13";
			// 
			// ctx품목To
			// 
			this.ctx품목To.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
			this.ctx품목To.Location = new System.Drawing.Point(314, 1);
			this.ctx품목To.Name = "ctx품목To";
			this.ctx품목To.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
			this.ctx품목To.Size = new System.Drawing.Size(186, 21);
			this.ctx품목To.TabIndex = 256;
			this.ctx품목To.TabStop = false;
			this.ctx품목To.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// lblFromTo2
			// 
			this.lblFromTo2.BackColor = System.Drawing.Color.Transparent;
			this.lblFromTo2.Location = new System.Drawing.Point(295, 1);
			this.lblFromTo2.Name = "lblFromTo2";
			this.lblFromTo2.Size = new System.Drawing.Size(15, 21);
			this.lblFromTo2.TabIndex = 255;
			this.lblFromTo2.Text = "~";
			this.lblFromTo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ctx품목From
			// 
			this.ctx품목From.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
			this.ctx품목From.Location = new System.Drawing.Point(106, 1);
			this.ctx품목From.Name = "ctx품목From";
			this.ctx품목From.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
			this.ctx품목From.Size = new System.Drawing.Size(186, 21);
			this.ctx품목From.TabIndex = 254;
			this.ctx품목From.TabStop = false;
			this.ctx품목From.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryAfter);
			this.ctx품목From.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
			// 
			// lbl품목
			// 
			this.lbl품목.BackColor = System.Drawing.Color.Transparent;
			this.lbl품목.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl품목.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl품목.Location = new System.Drawing.Point(0, 0);
			this.lbl품목.Name = "lbl품목";
			this.lbl품목.Size = new System.Drawing.Size(100, 23);
			this.lbl품목.TabIndex = 253;
			this.lbl품목.Text = "품목";
			this.lbl품목.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem8
			// 
			this.oneGridItem8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem8.Controls.Add(this.chk측정항목모두보기);
			this.oneGridItem8.Controls.Add(this.btn측정치삭제);
			this.oneGridItem8.Controls.Add(this.btn소재Heat번호적용);
			this.oneGridItem8.Controls.Add(this.btn열처리번호적용);
			this.oneGridItem8.Controls.Add(this.bpPanelControl16);
			this.oneGridItem8.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem8.Location = new System.Drawing.Point(0, 138);
			this.oneGridItem8.Name = "oneGridItem8";
			this.oneGridItem8.Size = new System.Drawing.Size(1573, 23);
			this.oneGridItem8.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem8.TabIndex = 6;
			// 
			// chk측정항목모두보기
			// 
			this.chk측정항목모두보기.AutoSize = true;
			this.chk측정항목모두보기.Location = new System.Drawing.Point(1014, 1);
			this.chk측정항목모두보기.Name = "chk측정항목모두보기";
			this.chk측정항목모두보기.Size = new System.Drawing.Size(123, 24);
			this.chk측정항목모두보기.TabIndex = 3;
			this.chk측정항목모두보기.Text = "측정항목모두보기";
			this.chk측정항목모두보기.TextDD = null;
			this.chk측정항목모두보기.UseVisualStyleBackColor = true;
			// 
			// btn측정치삭제
			// 
			this.btn측정치삭제.BackColor = System.Drawing.Color.Transparent;
			this.btn측정치삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn측정치삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn측정치삭제.Location = new System.Drawing.Point(918, 1);
			this.btn측정치삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn측정치삭제.Name = "btn측정치삭제";
			this.btn측정치삭제.Size = new System.Drawing.Size(94, 19);
			this.btn측정치삭제.TabIndex = 2;
			this.btn측정치삭제.TabStop = false;
			this.btn측정치삭제.Text = "측정치삭제";
			this.btn측정치삭제.UseVisualStyleBackColor = false;
			// 
			// btn소재Heat번호적용
			// 
			this.btn소재Heat번호적용.BackColor = System.Drawing.Color.Transparent;
			this.btn소재Heat번호적용.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn소재Heat번호적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn소재Heat번호적용.Location = new System.Drawing.Point(798, 1);
			this.btn소재Heat번호적용.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn소재Heat번호적용.Name = "btn소재Heat번호적용";
			this.btn소재Heat번호적용.Size = new System.Drawing.Size(118, 19);
			this.btn소재Heat번호적용.TabIndex = 5;
			this.btn소재Heat번호적용.TabStop = false;
			this.btn소재Heat번호적용.Text = "소재HEAT번호적용";
			this.btn소재Heat번호적용.UseVisualStyleBackColor = false;
			// 
			// btn열처리번호적용
			// 
			this.btn열처리번호적용.BackColor = System.Drawing.Color.Transparent;
			this.btn열처리번호적용.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn열처리번호적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn열처리번호적용.Location = new System.Drawing.Point(702, 1);
			this.btn열처리번호적용.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn열처리번호적용.Name = "btn열처리번호적용";
			this.btn열처리번호적용.Size = new System.Drawing.Size(94, 19);
			this.btn열처리번호적용.TabIndex = 4;
			this.btn열처리번호적용.TabStop = false;
			this.btn열처리번호적용.Text = "열처리번호적용";
			this.btn열처리번호적용.UseVisualStyleBackColor = false;
			// 
			// bpPanelControl16
			// 
			this.bpPanelControl16.Controls.Add(this.labelExt1);
			this.bpPanelControl16.Controls.Add(this.cur열처리번호To);
			this.bpPanelControl16.Controls.Add(this.cur열처리번호From);
			this.bpPanelControl16.Controls.Add(this.txt열처리번호);
			this.bpPanelControl16.Controls.Add(this.lbl열처리번호);
			this.bpPanelControl16.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl16.Name = "bpPanelControl16";
			this.bpPanelControl16.Size = new System.Drawing.Size(698, 23);
			this.bpPanelControl16.TabIndex = 0;
			this.bpPanelControl16.Text = "bpPanelControl16";
			// 
			// labelExt1
			// 
			this.labelExt1.BackColor = System.Drawing.Color.Transparent;
			this.labelExt1.Location = new System.Drawing.Point(295, 1);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(15, 21);
			this.labelExt1.TabIndex = 256;
			this.labelExt1.Text = "~";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cur열처리번호To
			// 
			this.cur열처리번호To.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur열처리번호To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur열처리번호To.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur열처리번호To.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur열처리번호To.Location = new System.Drawing.Point(314, 1);
			this.cur열처리번호To.Name = "cur열처리번호To";
			this.cur열처리번호To.NullString = "0";
			this.cur열처리번호To.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur열처리번호To.Size = new System.Drawing.Size(186, 21);
			this.cur열처리번호To.TabIndex = 3;
			this.cur열처리번호To.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cur열처리번호From
			// 
			this.cur열처리번호From.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur열처리번호From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur열처리번호From.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur열처리번호From.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur열처리번호From.Location = new System.Drawing.Point(106, 1);
			this.cur열처리번호From.Name = "cur열처리번호From";
			this.cur열처리번호From.NullString = "0";
			this.cur열처리번호From.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur열처리번호From.Size = new System.Drawing.Size(186, 21);
			this.cur열처리번호From.TabIndex = 2;
			this.cur열처리번호From.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txt열처리번호
			// 
			this.txt열처리번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt열처리번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt열처리번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt열처리번호.Location = new System.Drawing.Point(512, 0);
			this.txt열처리번호.Name = "txt열처리번호";
			this.txt열처리번호.Size = new System.Drawing.Size(186, 21);
			this.txt열처리번호.TabIndex = 1;
			// 
			// lbl열처리번호
			// 
			this.lbl열처리번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl열처리번호.Location = new System.Drawing.Point(0, 0);
			this.lbl열처리번호.Name = "lbl열처리번호";
			this.lbl열처리번호.Size = new System.Drawing.Size(100, 23);
			this.lbl열처리번호.TabIndex = 0;
			this.lbl열처리번호.Text = "열처리번호";
			this.lbl열처리번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btn작업지시마감취소
			// 
			this.btn작업지시마감취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn작업지시마감취소.BackColor = System.Drawing.Color.White;
			this.btn작업지시마감취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn작업지시마감취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn작업지시마감취소.Location = new System.Drawing.Point(420, 3);
			this.btn작업지시마감취소.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn작업지시마감취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn작업지시마감취소.Name = "btn작업지시마감취소";
			this.btn작업지시마감취소.Size = new System.Drawing.Size(112, 19);
			this.btn작업지시마감취소.TabIndex = 11;
			this.btn작업지시마감취소.TabStop = false;
			this.btn작업지시마감취소.Tag = "작업지시마감취소";
			this.btn작업지시마감취소.Text = "작업지시마감취소";
			this.btn작업지시마감취소.UseVisualStyleBackColor = false;
			// 
			// btn작업지시마감
			// 
			this.btn작업지시마감.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn작업지시마감.BackColor = System.Drawing.Color.White;
			this.btn작업지시마감.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn작업지시마감.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn작업지시마감.Location = new System.Drawing.Point(328, 3);
			this.btn작업지시마감.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn작업지시마감.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn작업지시마감.Name = "btn작업지시마감";
			this.btn작업지시마감.Size = new System.Drawing.Size(89, 19);
			this.btn작업지시마감.TabIndex = 10;
			this.btn작업지시마감.TabStop = false;
			this.btn작업지시마감.Tag = "작업지시마감";
			this.btn작업지시마감.Text = "작업지시마감";
			this.btn작업지시마감.UseVisualStyleBackColor = false;
			// 
			// btn작업지시서출력
			// 
			this.btn작업지시서출력.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn작업지시서출력.BackColor = System.Drawing.Color.White;
			this.btn작업지시서출력.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn작업지시서출력.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn작업지시서출력.Location = new System.Drawing.Point(535, 3);
			this.btn작업지시서출력.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn작업지시서출력.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn작업지시서출력.Name = "btn작업지시서출력";
			this.btn작업지시서출력.Size = new System.Drawing.Size(112, 19);
			this.btn작업지시서출력.TabIndex = 12;
			this.btn작업지시서출력.TabStop = false;
			this.btn작업지시서출력.Tag = "작업지시서출력";
			this.btn작업지시서출력.Text = "작업지시서출력";
			this.btn작업지시서출력.UseVisualStyleBackColor = false;
			// 
			// btn소요자재정보
			// 
			this.btn소요자재정보.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn소요자재정보.BackColor = System.Drawing.Color.White;
			this.btn소요자재정보.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn소요자재정보.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn소요자재정보.Location = new System.Drawing.Point(650, 3);
			this.btn소요자재정보.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn소요자재정보.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn소요자재정보.Name = "btn소요자재정보";
			this.btn소요자재정보.Size = new System.Drawing.Size(89, 19);
			this.btn소요자재정보.TabIndex = 13;
			this.btn소요자재정보.TabStop = false;
			this.btn소요자재정보.Tag = "소요자재정보";
			this.btn소요자재정보.Text = "소요자재정보";
			this.btn소요자재정보.UseVisualStyleBackColor = false;
			// 
			// btn계획작업지시확정
			// 
			this.btn계획작업지시확정.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn계획작업지시확정.BackColor = System.Drawing.Color.White;
			this.btn계획작업지시확정.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn계획작업지시확정.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn계획작업지시확정.Location = new System.Drawing.Point(98, 3);
			this.btn계획작업지시확정.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn계획작업지시확정.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn계획작업지시확정.Name = "btn계획작업지시확정";
			this.btn계획작업지시확정.Size = new System.Drawing.Size(112, 19);
			this.btn계획작업지시확정.TabIndex = 14;
			this.btn계획작업지시확정.TabStop = false;
			this.btn계획작업지시확정.Tag = "계획작업지시확정";
			this.btn계획작업지시확정.Text = "계획작업지시확정";
			this.btn계획작업지시확정.UseVisualStyleBackColor = false;
			// 
			// btn작업지시확정취소
			// 
			this.btn작업지시확정취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn작업지시확정취소.BackColor = System.Drawing.Color.White;
			this.btn작업지시확정취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn작업지시확정취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn작업지시확정취소.Location = new System.Drawing.Point(213, 3);
			this.btn작업지시확정취소.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn작업지시확정취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn작업지시확정취소.Name = "btn작업지시확정취소";
			this.btn작업지시확정취소.Size = new System.Drawing.Size(112, 19);
			this.btn작업지시확정취소.TabIndex = 15;
			this.btn작업지시확정취소.TabStop = false;
			this.btn작업지시확정취소.Tag = "작업지시확정취소";
			this.btn작업지시확정취소.Text = "작업지시확정취소";
			this.btn작업지시확정취소.UseVisualStyleBackColor = false;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Controls.Add(this.btn생산투입취소);
			this.flowLayoutPanel1.Controls.Add(this.btn생산투입);
			this.flowLayoutPanel1.Controls.Add(this.btn소요자재정보);
			this.flowLayoutPanel1.Controls.Add(this.btn작업지시서출력);
			this.flowLayoutPanel1.Controls.Add(this.btn작업지시마감취소);
			this.flowLayoutPanel1.Controls.Add(this.btn작업지시마감);
			this.flowLayoutPanel1.Controls.Add(this.btn작업지시확정취소);
			this.flowLayoutPanel1.Controls.Add(this.btn계획작업지시확정);
			this.flowLayoutPanel1.Controls.Add(this.btn작업지시삭제);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(661, 8);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(923, 23);
			this.flowLayoutPanel1.TabIndex = 16;
			// 
			// btn생산투입취소
			// 
			this.btn생산투입취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn생산투입취소.BackColor = System.Drawing.Color.White;
			this.btn생산투입취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn생산투입취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn생산투입취소.Location = new System.Drawing.Point(834, 3);
			this.btn생산투입취소.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn생산투입취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn생산투입취소.Name = "btn생산투입취소";
			this.btn생산투입취소.Size = new System.Drawing.Size(89, 19);
			this.btn생산투입취소.TabIndex = 18;
			this.btn생산투입취소.TabStop = false;
			this.btn생산투입취소.Text = "생산투입취소";
			this.btn생산투입취소.UseVisualStyleBackColor = false;
			// 
			// btn생산투입
			// 
			this.btn생산투입.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn생산투입.BackColor = System.Drawing.Color.White;
			this.btn생산투입.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn생산투입.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn생산투입.Location = new System.Drawing.Point(742, 3);
			this.btn생산투입.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn생산투입.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn생산투입.Name = "btn생산투입";
			this.btn생산투입.Size = new System.Drawing.Size(89, 19);
			this.btn생산투입.TabIndex = 17;
			this.btn생산투입.TabStop = false;
			this.btn생산투입.Text = "생산투입";
			this.btn생산투입.UseVisualStyleBackColor = false;
			// 
			// btn작업지시삭제
			// 
			this.btn작업지시삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn작업지시삭제.BackColor = System.Drawing.Color.White;
			this.btn작업지시삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn작업지시삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn작업지시삭제.Location = new System.Drawing.Point(6, 3);
			this.btn작업지시삭제.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn작업지시삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn작업지시삭제.Name = "btn작업지시삭제";
			this.btn작업지시삭제.Size = new System.Drawing.Size(89, 19);
			this.btn작업지시삭제.TabIndex = 16;
			this.btn작업지시삭제.TabStop = false;
			this.btn작업지시삭제.Text = "작업지시삭제";
			this.btn작업지시삭제.UseVisualStyleBackColor = false;
			// 
			// btn측정치삭제1
			// 
			this.btn측정치삭제1.BackColor = System.Drawing.Color.Transparent;
			this.btn측정치삭제1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn측정치삭제1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn측정치삭제1.Location = new System.Drawing.Point(764, 1);
			this.btn측정치삭제1.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn측정치삭제1.Name = "btn측정치삭제1";
			this.btn측정치삭제1.Size = new System.Drawing.Size(70, 19);
			this.btn측정치삭제1.TabIndex = 3;
			this.btn측정치삭제1.TabStop = false;
			this.btn측정치삭제1.Text = "측정치삭제";
			this.btn측정치삭제1.UseVisualStyleBackColor = false;
			// 
			// P_CZ_PR_WO_MNG
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.flowLayoutPanel1);
			this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Name = "P_CZ_PR_WO_MNG";
			this.Size = new System.Drawing.Size(1589, 856);
			this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.mDataArea.ResumeLayout(false);
			this.mDataArea.PerformLayout();
			this._tlayMain.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tpg공정.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem7.ResumeLayout(false);
			this.bpPanelControl15.ResumeLayout(false);
			this.bpPanelControl15.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur순번To)).EndInit();
			this.bpPanelControl14.ResumeLayout(false);
			this.bpPanelControl14.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur순번From)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
			this.tpg측정치.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexDH)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flexDL)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl13.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.bpPanelControl4.ResumeLayout(false);
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl8.ResumeLayout(false);
			this.bpPanelControl7.ResumeLayout(false);
			this.bpPanelControl6.ResumeLayout(false);
			this.oneGridItem4.ResumeLayout(false);
			this.bpPanelControl11.ResumeLayout(false);
			this.bpPanelControl10.ResumeLayout(false);
			this.bpPanelControl9.ResumeLayout(false);
			this.oneGridItem5.ResumeLayout(false);
			this.bpPanelControl12.ResumeLayout(false);
			this.bpPnl_NO_WO.ResumeLayout(false);
			this.oneGridItem6.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPnl_CD_ITEM.ResumeLayout(false);
			this.oneGridItem8.ResumeLayout(false);
			this.oneGridItem8.PerformLayout();
			this.bpPanelControl16.ResumeLayout(false);
			this.bpPanelControl16.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur열처리번호To)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cur열처리번호From)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel _tlayMain;
        private Duzon.Common.Controls.CheckBoxExt chk계획;
        private Duzon.Common.Controls.CheckBoxExt chk마감;
        private Duzon.Common.Controls.CheckBoxExt chk시작;
        private Duzon.Common.Controls.CheckBoxExt chk발행;
        private Duzon.Common.Controls.CheckBoxExt chk확정;
        private Duzon.Common.Controls.DropDownComboBox cbo공장;
        private Duzon.Common.Controls.DropDownComboBox cbo지시구분;
        private Label lbl작업기간;
        private Label lbl오더상태;
        private Label lbl공장;
        private Label lbl지시구분;
        private Duzon.Common.Controls.RoundedButton btn작업지시마감취소;
        private Duzon.Common.Controls.RoundedButton btn작업지시마감;
        private Duzon.Common.Controls.RoundedButton btn작업지시서출력;
        private Duzon.Common.Controls.RoundedButton btn소요자재정보;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FlexGrid _flexH;
        private Duzon.Common.Controls.RoundedButton btn계획작업지시확정;
        private Duzon.Common.Controls.RoundedButton btn작업지시확정취소;
        private Label lbl프로젝트;
        private Duzon.Common.BpControls.BpCodeTextBox ctx프로젝트;
        private Duzon.Common.Controls.LabelExt lbl오더형태;
        private Duzon.Common.BpControls.BpComboBox bpc오더형태;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.Controls.PeriodPicker periodPicker1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl8;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
        private Duzon.Common.Controls.LabelExt lbl품목군;
        private Duzon.Common.Controls.LabelExt lbl경로유형;
        private Duzon.Common.BpControls.BpCodeTextBox ctx품목군;
        private Duzon.Common.Controls.DropDownComboBox cbo경로유형;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem4;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl9;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl10;
        private Duzon.Common.BpControls.BpComboBox bpc공정;
        private Duzon.Common.Controls.LabelExt lbl공정;
        private Duzon.Common.BpControls.BpComboBox bpc작업장;
        private Duzon.Common.Controls.LabelExt lbl작업장;
        private Duzon.Common.BpControls.BpPanelControl bpPnl_NO_WO;
        private Duzon.Common.Controls.LabelExt lblFromTo1;
        private Duzon.Common.BpControls.BpCodeTextBox ctx작업지시번호From;
        private Duzon.Common.Controls.LabelExt lbl작업지시번호;
        private Duzon.Common.BpControls.BpCodeTextBox ctx작업지시번호To;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem5;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl11;
        private Duzon.Common.BpControls.BpComboBox bpc제품군;
        private Duzon.Common.Controls.LabelExt lbl제품군;
        private Duzon.Common.Controls.RoundedButton btn작업지시삭제;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl12;
        private Duzon.Common.Controls.LabelExt lblSFT;
        private Duzon.Common.BpControls.BpCodeTextBox ctxSFT;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem6;
        private Duzon.Common.BpControls.BpPanelControl bpPnl_CD_ITEM;
        private Duzon.Common.BpControls.BpCodeTextBox ctx품목To;
        private Duzon.Common.Controls.LabelExt lblFromTo2;
        private Duzon.Common.BpControls.BpCodeTextBox ctx품목From;
        private Duzon.Common.Controls.LabelExt lbl품목;
        private Duzon.Common.BpControls.BpCodeTextBox ctx;

		#endregion

		private Duzon.Common.BpControls.BpPanelControl bpPanelControl13;
		private FlowLayoutPanel flowLayoutPanel2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
		private Duzon.Common.Controls.LabelExt lbl담당자;
		private Duzon.Common.Controls.RoundedButton btn생산투입;
		private Duzon.Common.Controls.RoundedButton btn생산투입취소;
		private TabControl tabControl1;
		private TabPage tpg공정;
		private TabPage tpg측정치;
		private SplitContainer splitContainer2;
		private FlexGrid _flexDH;
		private FlexGrid _flexDL;
		private Duzon.Common.Controls.CheckBoxExt chk측정항목모두보기;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid2;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem7;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl14;
		private Duzon.Common.Controls.CurrencyTextBox cur순번From;
		private Duzon.Common.Controls.LabelExt lbl순번From;
		private Duzon.Common.Controls.RoundedButton btn임의입력;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl15;
		private Duzon.Common.Controls.CurrencyTextBox cur순번To;
		private Duzon.Common.Controls.LabelExt lbl순번To;
		private Duzon.Common.Controls.LabelExt lbl측정치;
		private TableLayoutPanel tableLayoutPanel1;
		private FlexGrid _flexL;
		private Duzon.Common.Controls.RoundedButton btn측정치삭제;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem8;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl16;
		private Duzon.Common.Controls.TextBoxExt txt열처리번호;
		private Duzon.Common.Controls.LabelExt lbl열처리번호;
		private Duzon.Common.Controls.RoundedButton btn열처리번호적용;
		private Duzon.Common.Controls.CurrencyTextBox cur열처리번호From;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Duzon.Common.Controls.CurrencyTextBox cur열처리번호To;
		private Duzon.Common.Controls.RoundedButton btn소재Heat번호적용;
		private Duzon.Common.Controls.RoundedButton btn측정치삭제1;
	}
}
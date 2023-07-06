
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_CZ_PR_OPOUT_PO_WORK_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_OPOUT_PO_WORK_SUB));
            this.chK작업지시비고적용 = new Duzon.Common.Controls.CheckBoxExt();
            this.bpc오더형태 = new Duzon.Common.BpControls.BpComboBox();
            this.txtLOT번호 = new Duzon.Common.Controls.TextBoxExt();
            this.txt작업지시번호 = new Duzon.Common.Controls.TextBoxExt();
            this.cur환율 = new Duzon.Common.Controls.CurrencyTextBox();
            this.cbo환종 = new Duzon.Common.Controls.DropDownComboBox();
            this.ctx외주처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl오더형태 = new Duzon.Common.Controls.LabelExt();
            this.lblLOT번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl작업기간 = new Duzon.Common.Controls.LabelExt();
            this.lbl작업지시번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl환종 = new Duzon.Common.Controls.LabelExt();
            this.lbl외주처 = new Duzon.Common.Controls.LabelExt();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.btn검색 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn적용 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp작업기간 = new Duzon.Common.Controls.PeriodPicker();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur환율)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.oneGridItem3.SuspendLayout();
            this.bpPanelControl7.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chK작업지시비고적용
            // 
            this.chK작업지시비고적용.AutoSize = true;
            this.chK작업지시비고적용.Dock = System.Windows.Forms.DockStyle.Left;
            this.chK작업지시비고적용.Location = new System.Drawing.Point(0, 0);
            this.chK작업지시비고적용.Name = "chK작업지시비고적용";
            this.chK작업지시비고적용.Size = new System.Drawing.Size(120, 23);
            this.chK작업지시비고적용.TabIndex = 223;
            this.chK작업지시비고적용.Tag = "";
            this.chK작업지시비고적용.Text = "작업지시비고적용";
            this.chK작업지시비고적용.TextDD = null;
            // 
            // bpc오더형태
            // 
            this.bpc오더형태.Dock = System.Windows.Forms.DockStyle.Right;
            this.bpc오더형태.HelpID = Duzon.Common.Forms.Help.HelpID.P_PR_TPWO_SUB1;
            this.bpc오더형태.Location = new System.Drawing.Point(106, 0);
            this.bpc오더형태.Name = "bpc오더형태";
            this.bpc오더형태.Size = new System.Drawing.Size(186, 21);
            this.bpc오더형태.TabIndex = 168;
            this.bpc오더형태.TabStop = false;
            this.bpc오더형태.Text = "bpComboBox1";
            this.bpc오더형태.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
            // 
            // txtLOT번호
            // 
            this.txtLOT번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txtLOT번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLOT번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtLOT번호.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtLOT번호.Location = new System.Drawing.Point(106, 0);
            this.txtLOT번호.MaxLength = 100;
            this.txtLOT번호.Name = "txtLOT번호";
            this.txtLOT번호.Size = new System.Drawing.Size(186, 21);
            this.txtLOT번호.TabIndex = 167;
            this.txtLOT번호.Tag = "NO_LOT";
            this.txtLOT번호.UseKeyEnter = false;
            this.txtLOT번호.UseKeyF3 = false;
            // 
            // txt작업지시번호
            // 
            this.txt작업지시번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt작업지시번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt작업지시번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt작업지시번호.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt작업지시번호.Location = new System.Drawing.Point(106, 0);
            this.txt작업지시번호.MaxLength = 100;
            this.txt작업지시번호.Name = "txt작업지시번호";
            this.txt작업지시번호.Size = new System.Drawing.Size(186, 21);
            this.txt작업지시번호.TabIndex = 166;
            this.txt작업지시번호.Tag = "NO_WO";
            this.txt작업지시번호.UseKeyEnter = false;
            this.txt작업지시번호.UseKeyF3 = false;
            // 
            // cur환율
            // 
            this.cur환율.BackColor = System.Drawing.SystemColors.Control;
            this.cur환율.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur환율.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur환율.CurrencyDecimalDigits = 4;
            this.cur환율.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur환율.Dock = System.Windows.Forms.DockStyle.Right;
            this.cur환율.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cur환율.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur환율.Location = new System.Drawing.Point(191, 0);
            this.cur환율.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.cur환율.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur환율.Name = "cur환율";
            this.cur환율.NullString = "0";
            this.cur환율.PositiveColor = System.Drawing.Color.Black;
            this.cur환율.ReadOnly = true;
            this.cur환율.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur환율.Size = new System.Drawing.Size(101, 21);
            this.cur환율.TabIndex = 164;
            this.cur환율.TabStop = false;
            this.cur환율.Tag = "RT_EXCH";
            this.cur환율.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur환율.UseKeyEnter = false;
            this.cur환율.UseKeyF3 = false;
            // 
            // cbo환종
            // 
            this.cbo환종.AutoDropDown = true;
            this.cbo환종.BackColor = System.Drawing.Color.White;
            this.cbo환종.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo환종.Enabled = false;
            this.cbo환종.ItemHeight = 12;
            this.cbo환종.Location = new System.Drawing.Point(106, 1);
            this.cbo환종.Name = "cbo환종";
            this.cbo환종.Size = new System.Drawing.Size(79, 20);
            this.cbo환종.TabIndex = 163;
            this.cbo환종.Tag = "CD_EXCH";
            this.cbo환종.UseKeyEnter = false;
            this.cbo환종.UseKeyF3 = false;
            // 
            // ctx외주처
            // 
            this.ctx외주처.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.ctx외주처.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx외주처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx외주처.ItemBackColor = System.Drawing.SystemColors.Window;
            this.ctx외주처.Location = new System.Drawing.Point(106, 0);
            this.ctx외주처.Name = "ctx외주처";
            this.ctx외주처.Size = new System.Drawing.Size(186, 21);
            this.ctx외주처.TabIndex = 1;
            this.ctx외주처.TabStop = false;
            this.ctx외주처.Text = "bpCodeTextBox1";
            this.ctx외주처.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
            // 
            // lbl오더형태
            // 
            this.lbl오더형태.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl오더형태.Location = new System.Drawing.Point(0, 0);
            this.lbl오더형태.Name = "lbl오더형태";
            this.lbl오더형태.Size = new System.Drawing.Size(100, 23);
            this.lbl오더형태.TabIndex = 2;
            this.lbl오더형태.Text = "오더형태";
            this.lbl오더형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLOT번호
            // 
            this.lblLOT번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLOT번호.Location = new System.Drawing.Point(0, 0);
            this.lblLOT번호.Name = "lblLOT번호";
            this.lblLOT번호.Size = new System.Drawing.Size(100, 23);
            this.lblLOT번호.TabIndex = 1;
            this.lblLOT번호.Text = "LOT번호";
            this.lblLOT번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl작업기간
            // 
            this.lbl작업기간.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl작업기간.Location = new System.Drawing.Point(0, 0);
            this.lbl작업기간.Name = "lbl작업기간";
            this.lbl작업기간.Size = new System.Drawing.Size(100, 23);
            this.lbl작업기간.TabIndex = 0;
            this.lbl작업기간.Text = "작업기간";
            this.lbl작업기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl작업지시번호
            // 
            this.lbl작업지시번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl작업지시번호.Location = new System.Drawing.Point(0, 0);
            this.lbl작업지시번호.Name = "lbl작업지시번호";
            this.lbl작업지시번호.Size = new System.Drawing.Size(100, 23);
            this.lbl작업지시번호.TabIndex = 2;
            this.lbl작업지시번호.Text = "작업지시번호";
            this.lbl작업지시번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl환종
            // 
            this.lbl환종.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl환종.Location = new System.Drawing.Point(0, 0);
            this.lbl환종.Name = "lbl환종";
            this.lbl환종.Size = new System.Drawing.Size(100, 23);
            this.lbl환종.TabIndex = 1;
            this.lbl환종.Text = "환종";
            this.lbl환종.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl외주처
            // 
            this.lbl외주처.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl외주처.Location = new System.Drawing.Point(0, 0);
            this.lbl외주처.Name = "lbl외주처";
            this.lbl외주처.Size = new System.Drawing.Size(100, 23);
            this.lbl외주처.TabIndex = 0;
            this.lbl외주처.Text = "외주처";
            this.lbl외주처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.Font = new System.Drawing.Font("굴림", 9F);
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(3, 129);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(899, 443);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 6;
            this._flex.UseGridCalculator = true;
            // 
            // btn검색
            // 
            this.btn검색.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn검색.BackColor = System.Drawing.Color.White;
            this.btn검색.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn검색.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn검색.Location = new System.Drawing.Point(704, 3);
            this.btn검색.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn검색.Name = "btn검색";
            this.btn검색.Size = new System.Drawing.Size(60, 19);
            this.btn검색.TabIndex = 6;
            this.btn검색.TabStop = false;
            this.btn검색.Text = "검색";
            this.btn검색.UseVisualStyleBackColor = false;
            // 
            // btn적용
            // 
            this.btn적용.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn적용.BackColor = System.Drawing.Color.White;
            this.btn적용.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn적용.Location = new System.Drawing.Point(770, 3);
            this.btn적용.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn적용.Name = "btn적용";
            this.btn적용.Size = new System.Drawing.Size(60, 19);
            this.btn적용.TabIndex = 7;
            this.btn적용.TabStop = false;
            this.btn적용.Text = "적용";
            this.btn적용.UseVisualStyleBackColor = false;
            // 
            // btn취소
            // 
            this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn취소.BackColor = System.Drawing.Color.White;
            this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn취소.Location = new System.Drawing.Point(836, 3);
            this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn취소.Name = "btn취소";
            this.btn취소.Size = new System.Drawing.Size(60, 19);
            this.btn취소.TabIndex = 8;
            this.btn취소.TabStop = false;
            this.btn취소.Text = "취소";
            this.btn취소.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(905, 575);
            this.tableLayoutPanel1.TabIndex = 9;
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
            this.oneGrid1.Size = new System.Drawing.Size(899, 86);
            this.oneGrid1.TabIndex = 0;
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
            this.oneGridItem1.Size = new System.Drawing.Size(889, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.lbl환종);
            this.bpPanelControl3.Controls.Add(this.cbo환종);
            this.bpPanelControl3.Controls.Add(this.cur환율);
            this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.dtp작업기간);
            this.bpPanelControl2.Controls.Add(this.lbl작업기간);
            this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // dtp작업기간
            // 
            this.dtp작업기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp작업기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp작업기간.IsNecessaryCondition = true;
            this.dtp작업기간.Location = new System.Drawing.Point(107, 0);
            this.dtp작업기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp작업기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp작업기간.Name = "dtp작업기간";
            this.dtp작업기간.Size = new System.Drawing.Size(185, 21);
            this.dtp작업기간.TabIndex = 1;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.lbl외주처);
            this.bpPanelControl1.Controls.Add(this.ctx외주처);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(889, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.lblLOT번호);
            this.bpPanelControl6.Controls.Add(this.txtLOT번호);
            this.bpPanelControl6.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl6.TabIndex = 4;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.lbl작업지시번호);
            this.bpPanelControl5.Controls.Add(this.txt작업지시번호);
            this.bpPanelControl5.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl5.TabIndex = 3;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.lbl오더형태);
            this.bpPanelControl4.Controls.Add(this.bpc오더형태);
            this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl4.TabIndex = 2;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl7);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(889, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 2;
            // 
            // bpPanelControl7
            // 
            this.bpPanelControl7.Controls.Add(this.chK작업지시비고적용);
            this.bpPanelControl7.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl7.Name = "bpPanelControl7";
            this.bpPanelControl7.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl7.TabIndex = 2;
            this.bpPanelControl7.Text = "bpPanelControl7";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn취소);
            this.flowLayoutPanel1.Controls.Add(this.btn적용);
            this.flowLayoutPanel1.Controls.Add(this.btn검색);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 95);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(899, 28);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // P_CZ_PR_OPOUT_PO_WORK_SUB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(908, 626);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "P_CZ_PR_OPOUT_PO_WORK_SUB";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ERP iU";
            this.TitleText = "외주요청적용(외주공정)";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur환율)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl3.PerformLayout();
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.bpPanelControl6.PerformLayout();
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl5.PerformLayout();
            this.bpPanelControl4.ResumeLayout(false);
            this.oneGridItem3.ResumeLayout(false);
            this.bpPanelControl7.ResumeLayout(false);
            this.bpPanelControl7.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		private BpCodeTextBox ctx외주처;
		private LabelExt lbl작업기간;
		private LabelExt lbl외주처;
		private FlexGrid _flex;
		private RoundedButton btn검색;
		private RoundedButton btn적용;
		private RoundedButton btn취소;
		private LabelExt lbl환종;
		private DropDownComboBox cbo환종;
		private CurrencyTextBox cur환율;
		private LabelExt lbl작업지시번호;
		private LabelExt lblLOT번호;
		private TextBoxExt txt작업지시번호;
		private TextBoxExt txtLOT번호;
		private LabelExt lbl오더형태;
		private BpComboBox bpc오더형태;
		private CheckBoxExt chK작업지시비고적용;

		#endregion

		private TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private BpPanelControl bpPanelControl3;
		private BpPanelControl bpPanelControl2;
		private BpPanelControl bpPanelControl1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private BpPanelControl bpPanelControl6;
		private BpPanelControl bpPanelControl5;
		private BpPanelControl bpPanelControl4;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
		private BpPanelControl bpPanelControl7;
		private FlowLayoutPanel flowLayoutPanel1;
		private PeriodPicker dtp작업기간;
	}
}
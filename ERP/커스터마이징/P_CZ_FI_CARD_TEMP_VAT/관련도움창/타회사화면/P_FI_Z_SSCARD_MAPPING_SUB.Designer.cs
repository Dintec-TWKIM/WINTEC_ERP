
using C1.Win.C1FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.Windows.OneControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_FI_Z_SSCARD_MAPPING_SUB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_FI_Z_SSCARD_MAPPING_SUB));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn적용 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc거래처 = new Duzon.Common.BpControls.BpComboBox();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.dtp회계일자 = new Duzon.Common.Controls.PeriodPicker();
			this.bppnl마감연도 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpt회계단위 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl마감연도 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.cur금액 = new Duzon.Common.Controls.CurrencyTextBox();
			this.labelExt4 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt전표번호 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt3 = new Duzon.Common.Controls.LabelExt();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.bppnl마감연도.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur금액)).BeginInit();
			this.bpPanelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 55);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 322F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(820, 456);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn취소);
			this.flowLayoutPanel1.Controls.Add(this.btn적용);
			this.flowLayoutPanel1.Controls.Add(this.btn조회);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 71);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(814, 25);
			this.flowLayoutPanel1.TabIndex = 163;
			// 
			// btn취소
			// 
			this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn취소.BackColor = System.Drawing.Color.Transparent;
			this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.Location = new System.Drawing.Point(752, 3);
			this.btn취소.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(62, 19);
			this.btn취소.TabIndex = 162;
			this.btn취소.TabStop = false;
			this.btn취소.Text = "취소";
			this.btn취소.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn취소.UseVisualStyleBackColor = true;
			// 
			// btn적용
			// 
			this.btn적용.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn적용.BackColor = System.Drawing.Color.Transparent;
			this.btn적용.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn적용.Location = new System.Drawing.Point(687, 3);
			this.btn적용.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn적용.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn적용.Name = "btn적용";
			this.btn적용.Size = new System.Drawing.Size(62, 19);
			this.btn적용.TabIndex = 163;
			this.btn적용.TabStop = false;
			this.btn적용.Text = "적용";
			this.btn적용.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn적용.UseVisualStyleBackColor = true;
			// 
			// btn조회
			// 
			this.btn조회.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn조회.BackColor = System.Drawing.Color.Transparent;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(622, 3);
			this.btn조회.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(62, 19);
			this.btn조회.TabIndex = 164;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn조회.UseVisualStyleBackColor = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(814, 62);
			this.oneGrid1.TabIndex = 164;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.Controls.Add(this.bppnl마감연도);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(804, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.bpc거래처);
			this.bpPanelControl2.Controls.Add(this.labelExt2);
			this.bpPanelControl2.Location = new System.Drawing.Point(536, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(265, 23);
			this.bpPanelControl2.TabIndex = 2;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// bpc거래처
			// 
			this.bpc거래처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB1;
			this.bpc거래처.Location = new System.Drawing.Point(80, 1);
			this.bpc거래처.Name = "bpc거래처";
			this.bpc거래처.Size = new System.Drawing.Size(185, 21);
			this.bpc거래처.TabIndex = 3;
			this.bpc거래처.TabStop = false;
			this.bpc거래처.Text = "bpComboBox1";
			// 
			// labelExt2
			// 
			this.labelExt2.Location = new System.Drawing.Point(0, 2);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Size = new System.Drawing.Size(79, 16);
			this.labelExt2.TabIndex = 2;
			this.labelExt2.Text = "거래처";
			this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.labelExt1);
			this.bpPanelControl1.Controls.Add(this.dtp회계일자);
			this.bpPanelControl1.Location = new System.Drawing.Point(269, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(265, 23);
			this.bpPanelControl1.TabIndex = 1;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// labelExt1
			// 
			this.labelExt1.Location = new System.Drawing.Point(0, 2);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(79, 16);
			this.labelExt1.TabIndex = 1;
			this.labelExt1.Text = "회계일자";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtp회계일자
			// 
			this.dtp회계일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp회계일자.IsNecessaryCondition = true;
			this.dtp회계일자.Location = new System.Drawing.Point(80, 1);
			this.dtp회계일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp회계일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp회계일자.Name = "dtp회계일자";
			this.dtp회계일자.Size = new System.Drawing.Size(185, 21);
			this.dtp회계일자.TabIndex = 0;
			// 
			// bppnl마감연도
			// 
			this.bppnl마감연도.Controls.Add(this.bpt회계단위);
			this.bppnl마감연도.Controls.Add(this.lbl마감연도);
			this.bppnl마감연도.Location = new System.Drawing.Point(2, 1);
			this.bppnl마감연도.Name = "bppnl마감연도";
			this.bppnl마감연도.Size = new System.Drawing.Size(265, 23);
			this.bppnl마감연도.TabIndex = 0;
			this.bppnl마감연도.Text = "bpPanelControl1";
			// 
			// bpt회계단위
			// 
			this.bpt회계단위.BackColor = System.Drawing.Color.White;
			this.bpt회계단위.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PC_SUB;
			this.bpt회계단위.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.bpt회계단위.Location = new System.Drawing.Point(80, 1);
			this.bpt회계단위.Name = "bpt회계단위";
			this.bpt회계단위.SetDefaultValue = true;
			this.bpt회계단위.Size = new System.Drawing.Size(185, 21);
			this.bpt회계단위.TabIndex = 1;
			this.bpt회계단위.TabStop = false;
			this.bpt회계단위.Text = "bpCodeTextBox1";
			// 
			// lbl마감연도
			// 
			this.lbl마감연도.Location = new System.Drawing.Point(0, 2);
			this.lbl마감연도.Name = "lbl마감연도";
			this.lbl마감연도.Size = new System.Drawing.Size(79, 16);
			this.lbl마감연도.TabIndex = 0;
			this.lbl마감연도.Text = "회계단위";
			this.lbl마감연도.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl5);
			this.oneGridItem2.Controls.Add(this.bpPanelControl4);
			this.oneGridItem2.Controls.Add(this.bpPanelControl3);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(804, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Location = new System.Drawing.Point(536, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(265, 23);
			this.bpPanelControl5.TabIndex = 2;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.cur금액);
			this.bpPanelControl4.Controls.Add(this.labelExt4);
			this.bpPanelControl4.Location = new System.Drawing.Point(269, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(265, 23);
			this.bpPanelControl4.TabIndex = 1;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// cur금액
			// 
			this.cur금액.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.cur금액.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cur금액.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.cur금액.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cur금액.Location = new System.Drawing.Point(80, 1);
			this.cur금액.Name = "cur금액";
			this.cur금액.NullString = "0";
			this.cur금액.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cur금액.Size = new System.Drawing.Size(185, 21);
			this.cur금액.TabIndex = 2;
			this.cur금액.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// labelExt4
			// 
			this.labelExt4.Location = new System.Drawing.Point(0, 2);
			this.labelExt4.Name = "labelExt4";
			this.labelExt4.Size = new System.Drawing.Size(79, 16);
			this.labelExt4.TabIndex = 1;
			this.labelExt4.Text = "금액";
			this.labelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.txt전표번호);
			this.bpPanelControl3.Controls.Add(this.labelExt3);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(265, 23);
			this.bpPanelControl3.TabIndex = 0;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// txt전표번호
			// 
			this.txt전표번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt전표번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt전표번호.Location = new System.Drawing.Point(80, 1);
			this.txt전표번호.Name = "txt전표번호";
			this.txt전표번호.Size = new System.Drawing.Size(185, 21);
			this.txt전표번호.TabIndex = 2;
			// 
			// labelExt3
			// 
			this.labelExt3.Location = new System.Drawing.Point(0, 2);
			this.labelExt3.Name = "labelExt3";
			this.labelExt3.Size = new System.Drawing.Size(79, 16);
			this.labelExt3.TabIndex = 1;
			this.labelExt3.Text = "전표번호";
			this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex.Location = new System.Drawing.Point(3, 102);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 18;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(814, 351);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 4;
			this._flex.UseGridCalculator = true;
			// 
			// P_FI_Z_SSCARD_MAPPING_SUB
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(844, 519);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_FI_Z_SSCARD_MAPPING_SUB";
			this.Text = "더존 ERP-iU";
			this.TitleText = "전표번호매핑";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bppnl마감연도.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cur금액)).EndInit();
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.ResumeLayout(false);

        }

		private TableLayoutPanel tableLayoutPanel1;
		private FlowLayoutPanel flowLayoutPanel1;
		private RoundedButton btn취소;
		private RoundedButton btn적용;
		private RoundedButton btn조회;
		private OneGrid oneGrid1;
		private OneGridItem oneGridItem1;
		private BpPanelControl bppnl마감연도;
		private LabelExt lbl마감연도;
		private Dass.FlexGrid.FlexGrid _flex;
		private OneGridItem oneGridItem2;
		private BpPanelControl bpPanelControl2;
		private BpPanelControl bpPanelControl1;
		private BpPanelControl bpPanelControl5;
		private BpPanelControl bpPanelControl4;
		private BpPanelControl bpPanelControl3;
		private BpComboBox bpc거래처;
		private LabelExt labelExt2;
		private LabelExt labelExt1;
		private PeriodPicker dtp회계일자;
		private CurrencyTextBox cur금액;
		private LabelExt labelExt4;
		private TextBoxExt txt전표번호;
		private LabelExt labelExt3;
		private BpCodeTextBox bpt회계단위;

		#endregion
	}
}
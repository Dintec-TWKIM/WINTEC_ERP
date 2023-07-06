
using C1.Win.C1FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Erpiu.Windows.OneControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	partial class P_FI_Z_HAATZ_CARDCLOSE_SUB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_FI_Z_HAATZ_CARDCLOSE_SUB));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn종료 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bppnl마감연도 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp마감연도 = new Duzon.Common.Controls.DatePicker();
			this.lbl마감연도 = new Duzon.Common.Controls.LabelExt();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bppnl마감연도.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp마감연도)).BeginInit();
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
			this.tableLayoutPanel1.Size = new System.Drawing.Size(480, 398);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn종료);
			this.flowLayoutPanel1.Controls.Add(this.btn저장);
			this.flowLayoutPanel1.Controls.Add(this.btn조회);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 49);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(474, 25);
			this.flowLayoutPanel1.TabIndex = 163;
			// 
			// btn종료
			// 
			this.btn종료.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn종료.BackColor = System.Drawing.Color.Transparent;
			this.btn종료.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn종료.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn종료.Location = new System.Drawing.Point(412, 3);
			this.btn종료.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn종료.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn종료.Name = "btn종료";
			this.btn종료.Size = new System.Drawing.Size(62, 19);
			this.btn종료.TabIndex = 162;
			this.btn종료.TabStop = false;
			this.btn종료.Text = "종료";
			this.btn종료.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn종료.UseVisualStyleBackColor = true;
			// 
			// btn저장
			// 
			this.btn저장.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn저장.BackColor = System.Drawing.Color.Transparent;
			this.btn저장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn저장.Location = new System.Drawing.Point(347, 3);
			this.btn저장.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.btn저장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn저장.Name = "btn저장";
			this.btn저장.Size = new System.Drawing.Size(62, 19);
			this.btn저장.TabIndex = 163;
			this.btn저장.TabStop = false;
			this.btn저장.Text = "저장";
			this.btn저장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn저장.UseVisualStyleBackColor = true;
			// 
			// btn조회
			// 
			this.btn조회.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn조회.BackColor = System.Drawing.Color.Transparent;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(282, 3);
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
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(474, 40);
			this.oneGrid1.TabIndex = 164;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bppnl마감연도);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(464, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bppnl마감연도
			// 
			this.bppnl마감연도.Controls.Add(this.dtp마감연도);
			this.bppnl마감연도.Controls.Add(this.lbl마감연도);
			this.bppnl마감연도.Location = new System.Drawing.Point(2, 1);
			this.bppnl마감연도.Name = "bppnl마감연도";
			this.bppnl마감연도.Size = new System.Drawing.Size(548, 23);
			this.bppnl마감연도.TabIndex = 0;
			this.bppnl마감연도.Text = "bpPanelControl1";
			// 
			// dtp마감연도
			// 
			this.dtp마감연도.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp마감연도.Location = new System.Drawing.Point(157, 1);
			this.dtp마감연도.Mask = "####";
			this.dtp마감연도.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.dtp마감연도.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp마감연도.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp마감연도.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp마감연도.Modified = true;
			this.dtp마감연도.Name = "dtp마감연도";
			this.dtp마감연도.NullCheck = true;
			this.dtp마감연도.ShowUpDown = true;
			this.dtp마감연도.Size = new System.Drawing.Size(90, 21);
			this.dtp마감연도.TabIndex = 1;
			this.dtp마감연도.Value = new System.DateTime(((long)(0)));
			// 
			// lbl마감연도
			// 
			this.lbl마감연도.Location = new System.Drawing.Point(0, 3);
			this.lbl마감연도.Name = "lbl마감연도";
			this.lbl마감연도.Size = new System.Drawing.Size(156, 16);
			this.lbl마감연도.TabIndex = 0;
			this.lbl마감연도.Text = "마감연도";
			this.lbl마감연도.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this._flex.Location = new System.Drawing.Point(3, 80);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 18;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(474, 316);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 4;
			this._flex.UseGridCalculator = true;
			// 
			// P_FI_Z_HAATZ_CARDCLOSE_SUB
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(504, 461);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_FI_Z_HAATZ_CARDCLOSE_SUB";
			this.Text = "더존 ERP-iU";
			this.TitleText = "카드전표마감";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bppnl마감연도.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp마감연도)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.ResumeLayout(false);

        }

		private TableLayoutPanel tableLayoutPanel1;
		private FlowLayoutPanel flowLayoutPanel1;
		private RoundedButton btn종료;
		private RoundedButton btn저장;
		private RoundedButton btn조회;
		private OneGrid oneGrid1;
		private OneGridItem oneGridItem1;
		private BpPanelControl bppnl마감연도;
		private DatePicker dtp마감연도;
		private LabelExt lbl마감연도;
		private Dass.FlexGrid.FlexGrid _flex;
		#endregion
	}
}
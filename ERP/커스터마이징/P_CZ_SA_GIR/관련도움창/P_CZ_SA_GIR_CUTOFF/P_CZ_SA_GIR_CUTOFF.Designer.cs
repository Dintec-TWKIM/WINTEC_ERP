
namespace cz
{
	partial class P_CZ_SA_GIR_CUTOFF
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_GIR_CUTOFF));
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this._flex컷오프시간 = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl컷오프시간 = new Duzon.Common.Controls.LabelExt();
			this.dtp컷오프시간 = new Duzon.Common.Controls.DateTimePickerExt();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex컷오프시간)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn저장);
			this.flowLayoutPanel1.Controls.Add(this.btn조회);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 49);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(791, 27);
			this.flowLayoutPanel1.TabIndex = 2;
			// 
			// btn저장
			// 
			this.btn저장.BackColor = System.Drawing.Color.Transparent;
			this.btn저장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn저장.Location = new System.Drawing.Point(718, 3);
			this.btn저장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn저장.Name = "btn저장";
			this.btn저장.Size = new System.Drawing.Size(70, 19);
			this.btn저장.TabIndex = 0;
			this.btn저장.TabStop = false;
			this.btn저장.Text = "저장";
			this.btn저장.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.BackColor = System.Drawing.Color.Transparent;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(642, 3);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 1;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// _flex컷오프시간
			// 
			this._flex컷오프시간.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex컷오프시간.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex컷오프시간.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex컷오프시간.AutoResize = false;
			this._flex컷오프시간.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex컷오프시간.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex컷오프시간.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex컷오프시간.EnabledHeaderCheck = true;
			this._flex컷오프시간.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex컷오프시간.Location = new System.Drawing.Point(3, 82);
			this._flex컷오프시간.Name = "_flex컷오프시간";
			this._flex컷오프시간.Rows.Count = 1;
			this._flex컷오프시간.Rows.DefaultSize = 18;
			this._flex컷오프시간.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex컷오프시간.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex컷오프시간.ShowSort = false;
			this._flex컷오프시간.Size = new System.Drawing.Size(791, 313);
			this._flex컷오프시간.StyleInfo = resources.GetString("_flex컷오프시간.StyleInfo");
			this._flex컷오프시간.TabIndex = 1;
			this._flex컷오프시간.UseGridCalculator = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(791, 40);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(781, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.lbl컷오프시간);
			this.bpPanelControl1.Controls.Add(this.dtp컷오프시간);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(224, 23);
			this.bpPanelControl1.TabIndex = 0;
			// 
			// lbl컷오프시간
			// 
			this.lbl컷오프시간.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl컷오프시간.Location = new System.Drawing.Point(0, 0);
			this.lbl컷오프시간.Name = "lbl컷오프시간";
			this.lbl컷오프시간.Size = new System.Drawing.Size(100, 23);
			this.lbl컷오프시간.TabIndex = 0;
			this.lbl컷오프시간.Text = "컷오프시간";
			this.lbl컷오프시간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtp컷오프시간
			// 
			this.dtp컷오프시간.CustomFormat = "yyyy-MM-dd HH시";
			this.dtp컷오프시간.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp컷오프시간.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtp컷오프시간.Location = new System.Drawing.Point(108, 0);
			this.dtp컷오프시간.Name = "dtp컷오프시간";
			this.dtp컷오프시간.ShowUpDown = true;
			this.dtp컷오프시간.Size = new System.Drawing.Size(116, 21);
			this.dtp컷오프시간.TabIndex = 1;
			this.dtp컷오프시간.UseKeyEnter = true;
			this.dtp컷오프시간.UseKeyF3 = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._flex컷오프시간, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 50);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(797, 398);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// P_CZ_SA_GIR_CUTOFF
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_CZ_SA_GIR_CUTOFF";
			this.Text = "P_CZ_SA_GIR_CUTOFF";
			this.TitleText = "컷오프시간설정";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex컷오프시간)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private Duzon.Common.Controls.RoundedButton btn저장;
		private Duzon.Common.Controls.RoundedButton btn조회;
		private Dass.FlexGrid.FlexGrid _flex컷오프시간;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.LabelExt lbl컷오프시간;
		private Duzon.Common.Controls.DateTimePickerExt dtp컷오프시간;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}
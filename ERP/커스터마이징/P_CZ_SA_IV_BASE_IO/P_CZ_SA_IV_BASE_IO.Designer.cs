
namespace cz
{
	partial class P_CZ_SA_IV_BASE_IO
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_IV_BASE_IO));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl출고일자 = new Duzon.Common.Controls.LabelExt();
			this.dtp조회기간 = new Duzon.Common.Controls.PeriodPicker();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.btn상세보기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
			this.imagePanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1250, 804);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1250, 804);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1244, 41);
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
			this.oneGridItem1.Size = new System.Drawing.Size(1234, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.lbl출고일자);
			this.bpPanelControl1.Controls.Add(this.dtp조회기간);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(294, 23);
			this.bpPanelControl1.TabIndex = 1;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// lbl출고일자
			// 
			this.lbl출고일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl출고일자.Location = new System.Drawing.Point(0, 0);
			this.lbl출고일자.Name = "lbl출고일자";
			this.lbl출고일자.Size = new System.Drawing.Size(100, 23);
			this.lbl출고일자.TabIndex = 2;
			this.lbl출고일자.Text = "출고일자";
			this.lbl출고일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtp조회기간
			// 
			this.dtp조회기간.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp조회기간.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp조회기간.IsNecessaryCondition = true;
			this.dtp조회기간.Location = new System.Drawing.Point(109, 0);
			this.dtp조회기간.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp조회기간.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp조회기간.Name = "dtp조회기간";
			this.dtp조회기간.Size = new System.Drawing.Size(185, 21);
			this.dtp조회기간.TabIndex = 1;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 50);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._flexH);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.imagePanel1);
			this.splitContainer1.Size = new System.Drawing.Size(1244, 751);
			this.splitContainer1.SplitterDistance = 493;
			this.splitContainer1.TabIndex = 1;
			// 
			// _flexH
			// 
			this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexH.AutoResize = false;
			this._flexH.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
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
			this._flexH.Size = new System.Drawing.Size(1244, 493);
			this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
			this._flexH.TabIndex = 2;
			this._flexH.UseGridCalculator = true;
			// 
			// imagePanel1
			// 
			this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel1.Controls.Add(this.btn상세보기);
			this.imagePanel1.Controls.Add(this._flexL);
			this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel1.LeftImage = null;
			this.imagePanel1.Location = new System.Drawing.Point(0, 0);
			this.imagePanel1.Name = "imagePanel1";
			this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel1.PatternImage = null;
			this.imagePanel1.RightImage = null;
			this.imagePanel1.Size = new System.Drawing.Size(1244, 254);
			this.imagePanel1.TabIndex = 1;
			this.imagePanel1.TitleText = "상세보기";
			// 
			// btn상세보기
			// 
			this.btn상세보기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn상세보기.BackColor = System.Drawing.Color.Transparent;
			this.btn상세보기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn상세보기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn상세보기.Location = new System.Drawing.Point(1169, 4);
			this.btn상세보기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn상세보기.Name = "btn상세보기";
			this.btn상세보기.Size = new System.Drawing.Size(70, 19);
			this.btn상세보기.TabIndex = 1;
			this.btn상세보기.TabStop = false;
			this.btn상세보기.Text = "상세보기";
			this.btn상세보기.UseVisualStyleBackColor = false;
			// 
			// _flexL
			// 
			this._flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flexL.AutoResize = false;
			this._flexL.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexL.EnabledHeaderCheck = true;
			this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexL.Location = new System.Drawing.Point(3, 29);
			this._flexL.Name = "_flexL";
			this._flexL.Rows.Count = 1;
			this._flexL.Rows.DefaultSize = 20;
			this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexL.ShowSort = false;
			this._flexL.Size = new System.Drawing.Size(1238, 222);
			this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
			this._flexL.TabIndex = 0;
			this._flexL.UseGridCalculator = true;
			// 
			// P_CZ_SA_IV_BASE_IO
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_SA_IV_BASE_IO";
			this.Size = new System.Drawing.Size(1250, 844);
			this.TitleText = "P_CZ_SA_IV_BASE_IO";
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
			this.imagePanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.LabelExt lbl출고일자;
		private Duzon.Common.Controls.PeriodPicker dtp조회기간;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Dass.FlexGrid.FlexGrid _flexH;
		private Dass.FlexGrid.FlexGrid _flexL;
		private Duzon.Common.Controls.ImagePanel imagePanel1;
		private Duzon.Common.Controls.RoundedButton btn상세보기;
	}
}
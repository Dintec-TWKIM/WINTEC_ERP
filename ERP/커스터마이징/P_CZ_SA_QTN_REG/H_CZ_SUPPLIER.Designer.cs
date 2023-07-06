namespace cz
{
	partial class H_CZ_SUPPLIER
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_CZ_SUPPLIER));
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tlayS = new System.Windows.Forms.TableLayoutPanel();
			this.grd조회 = new Dass.FlexGrid.FlexGrid(this.components);
			this.pnl제목1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl12 = new Duzon.Common.BpControls.BpPanelControl();
			this.tbx범위 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt11 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.tbx검색 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.tlayL = new System.Windows.Forms.TableLayoutPanel();
			this.grd선택 = new Dass.FlexGrid.FlexGrid(this.components);
			this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tlayS.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd조회)).BeginInit();
			this.pnl제목1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl12.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.tlayL.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd선택)).BeginInit();
			this.imagePanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btn취소
			// 
			this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn취소.BackColor = System.Drawing.Color.Transparent;
			this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.Location = new System.Drawing.Point(442, 18);
			this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(70, 19);
			this.btn취소.TabIndex = 8;
			this.btn취소.TabStop = false;
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn조회.BackColor = System.Drawing.Color.White;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(360, 4);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 7;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
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
			this.splitContainer1.Panel1.Controls.Add(this.tlayS);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tlayL);
			this.splitContainer1.Size = new System.Drawing.Size(520, 546);
			this.splitContainer1.SplitterDistance = 288;
			this.splitContainer1.TabIndex = 9;
			// 
			// tlayS
			// 
			this.tlayS.ColumnCount = 1;
			this.tlayS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayS.Controls.Add(this.grd조회, 0, 2);
			this.tlayS.Controls.Add(this.pnl제목1, 0, 1);
			this.tlayS.Controls.Add(this.oneGrid1, 0, 0);
			this.tlayS.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayS.Location = new System.Drawing.Point(0, 0);
			this.tlayS.Name = "tlayS";
			this.tlayS.RowCount = 3;
			this.tlayS.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayS.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayS.Size = new System.Drawing.Size(520, 288);
			this.tlayS.TabIndex = 0;
			// 
			// grd조회
			// 
			this.grd조회.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd조회.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd조회.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd조회.AutoResize = false;
			this.grd조회.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd조회.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd조회.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd조회.EnabledHeaderCheck = true;
			this.grd조회.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
			this.grd조회.Location = new System.Drawing.Point(3, 104);
			this.grd조회.Name = "grd조회";
			this.grd조회.Rows.Count = 1;
			this.grd조회.Rows.DefaultSize = 18;
			this.grd조회.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd조회.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd조회.ShowSort = false;
			this.grd조회.Size = new System.Drawing.Size(514, 181);
			this.grd조회.StyleInfo = resources.GetString("grd조회.StyleInfo");
			this.grd조회.TabIndex = 15;
			this.grd조회.UseGridCalculator = true;
			// 
			// pnl제목1
			// 
			this.pnl제목1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.pnl제목1.Controls.Add(this.btn추가);
			this.pnl제목1.Controls.Add(this.btn조회);
			this.pnl제목1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl제목1.LeftImage = null;
			this.pnl제목1.Location = new System.Drawing.Point(3, 71);
			this.pnl제목1.Name = "pnl제목1";
			this.pnl제목1.Padding = new System.Windows.Forms.Padding(1);
			this.pnl제목1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.pnl제목1.PatternImage = null;
			this.pnl제목1.RightImage = null;
			this.pnl제목1.Size = new System.Drawing.Size(514, 27);
			this.pnl제목1.TabIndex = 9;
			this.pnl제목1.TitleText = "매입처 정보";
			// 
			// btn추가
			// 
			this.btn추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn추가.BackColor = System.Drawing.Color.Transparent;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.Location = new System.Drawing.Point(436, 4);
			this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn추가.Name = "btn추가";
			this.btn추가.Size = new System.Drawing.Size(70, 19);
			this.btn추가.TabIndex = 1;
			this.btn추가.TabStop = false;
			this.btn추가.Text = "추가";
			this.btn추가.UseVisualStyleBackColor = false;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(514, 62);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl12);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(504, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl12
			// 
			this.bpPanelControl12.Controls.Add(this.tbx범위);
			this.bpPanelControl12.Controls.Add(this.labelExt11);
			this.bpPanelControl12.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl12.Name = "bpPanelControl12";
			this.bpPanelControl12.Size = new System.Drawing.Size(310, 23);
			this.bpPanelControl12.TabIndex = 19;
			this.bpPanelControl12.Text = "bpPanelControl5";
			// 
			// tbx범위
			// 
			this.tbx범위.BackColor = System.Drawing.Color.White;
			this.tbx범위.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx범위.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx범위.Location = new System.Drawing.Point(84, 1);
			this.tbx범위.Name = "tbx범위";
			this.tbx범위.Size = new System.Drawing.Size(96, 21);
			this.tbx범위.TabIndex = 4;
			this.tbx범위.Tag = "";
			// 
			// labelExt11
			// 
			this.labelExt11.Location = new System.Drawing.Point(17, 4);
			this.labelExt11.Name = "labelExt11";
			this.labelExt11.Size = new System.Drawing.Size(65, 16);
			this.labelExt11.TabIndex = 3;
			this.labelExt11.Text = "범위";
			this.labelExt11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl1);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(504, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.tbx검색);
			this.bpPanelControl1.Controls.Add(this.labelExt1);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(439, 23);
			this.bpPanelControl1.TabIndex = 19;
			this.bpPanelControl1.Text = "bpPanelControl5";
			// 
			// tbx검색
			// 
			this.tbx검색.BackColor = System.Drawing.Color.White;
			this.tbx검색.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx검색.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx검색.Location = new System.Drawing.Point(84, 1);
			this.tbx검색.Name = "tbx검색";
			this.tbx검색.Size = new System.Drawing.Size(339, 21);
			this.tbx검색.TabIndex = 4;
			this.tbx검색.Tag = "";
			// 
			// labelExt1
			// 
			this.labelExt1.Location = new System.Drawing.Point(17, 4);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(65, 16);
			this.labelExt1.TabIndex = 3;
			this.labelExt1.Text = "검색";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tlayL
			// 
			this.tlayL.ColumnCount = 1;
			this.tlayL.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayL.Controls.Add(this.grd선택, 0, 1);
			this.tlayL.Controls.Add(this.imagePanel1, 0, 0);
			this.tlayL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayL.Location = new System.Drawing.Point(0, 0);
			this.tlayL.Name = "tlayL";
			this.tlayL.RowCount = 2;
			this.tlayL.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayL.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayL.Size = new System.Drawing.Size(520, 254);
			this.tlayL.TabIndex = 0;
			// 
			// grd선택
			// 
			this.grd선택.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd선택.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd선택.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd선택.AutoResize = false;
			this.grd선택.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd선택.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd선택.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd선택.EnabledHeaderCheck = true;
			this.grd선택.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
			this.grd선택.Location = new System.Drawing.Point(3, 36);
			this.grd선택.Name = "grd선택";
			this.grd선택.Rows.Count = 1;
			this.grd선택.Rows.DefaultSize = 18;
			this.grd선택.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd선택.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd선택.ShowSort = false;
			this.grd선택.Size = new System.Drawing.Size(514, 215);
			this.grd선택.StyleInfo = resources.GetString("grd선택.StyleInfo");
			this.grd선택.TabIndex = 16;
			this.grd선택.UseGridCalculator = true;
			// 
			// imagePanel1
			// 
			this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel1.Controls.Add(this.btn삭제);
			this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.imagePanel1.LeftImage = null;
			this.imagePanel1.Location = new System.Drawing.Point(3, 3);
			this.imagePanel1.Name = "imagePanel1";
			this.imagePanel1.Padding = new System.Windows.Forms.Padding(1);
			this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel1.PatternImage = null;
			this.imagePanel1.RightImage = null;
			this.imagePanel1.Size = new System.Drawing.Size(514, 27);
			this.imagePanel1.TabIndex = 10;
			this.imagePanel1.TitleText = "매입처 정보";
			// 
			// btn삭제
			// 
			this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn삭제.BackColor = System.Drawing.Color.Transparent;
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(436, 4);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(70, 19);
			this.btn삭제.TabIndex = 1;
			this.btn삭제.TabStop = false;
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// btn확인
			// 
			this.btn확인.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn확인.BackColor = System.Drawing.Color.Transparent;
			this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn확인.Location = new System.Drawing.Point(366, 18);
			this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn확인.Name = "btn확인";
			this.btn확인.Size = new System.Drawing.Size(70, 19);
			this.btn확인.TabIndex = 2;
			this.btn확인.TabStop = false;
			this.btn확인.Text = "확인";
			this.btn확인.UseVisualStyleBackColor = false;
			// 
			// H_CZ_SUPPLIER
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(526, 599);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.btn확인);
			this.Controls.Add(this.btn취소);
			this.Name = "H_CZ_SUPPLIER";
			this.Padding = new System.Windows.Forms.Padding(3, 50, 3, 3);
			this.ShowInTaskbar = false;
			this.Text = "H_CZ_SUPPLIER";
			this.TitleText = "매입처 검색";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tlayS.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd조회)).EndInit();
			this.pnl제목1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl12.ResumeLayout(false);
			this.bpPanelControl12.PerformLayout();
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			this.tlayL.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd선택)).EndInit();
			this.imagePanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Duzon.Common.Controls.RoundedButton btn취소;
		private Duzon.Common.Controls.RoundedButton btn조회;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TableLayoutPanel tlayS;
		private System.Windows.Forms.TableLayoutPanel tlayL;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Common.Controls.ImagePanel pnl제목1;
		private Duzon.Common.Controls.RoundedButton btn추가;
		private Duzon.Common.Controls.RoundedButton btn확인;
		private Dass.FlexGrid.FlexGrid grd조회;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Dass.FlexGrid.FlexGrid grd선택;
		private Duzon.Common.Controls.ImagePanel imagePanel1;
		private Duzon.Common.Controls.RoundedButton btn삭제;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl12;
		private Duzon.Common.Controls.TextBoxExt tbx범위;
		private Duzon.Common.Controls.LabelExt labelExt11;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.TextBoxExt tbx검색;
		private Duzon.Common.Controls.LabelExt labelExt1;
	}
}
namespace cz
{
	partial class P_CZ_SA_QTN_PREREG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_QTN_PREREG));
			this.spc메인 = new System.Windows.Forms.SplitContainer();
			this.lay헤드 = new System.Windows.Forms.TableLayoutPanel();
			this.lay라인 = new System.Windows.Forms.TableLayoutPanel();
			this.one검색 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.fgd헤드 = new Dass.FlexGrid.FlexGrid(this.components);
			this.ipnl = new Duzon.Common.Controls.ImagePanel(this.components);
			this.fgd라인 = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.tbx사전등록번호 = new Dintec.DxControls.DxTextBox();
			this.btn견적등록 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx회사코드 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.labelExt9 = new Duzon.Common.Controls.LabelExt();
			this.mDataArea.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spc메인)).BeginInit();
			this.spc메인.Panel1.SuspendLayout();
			this.spc메인.Panel2.SuspendLayout();
			this.spc메인.SuspendLayout();
			this.lay헤드.SuspendLayout();
			this.lay라인.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.fgd헤드)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fgd라인)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl7.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.spc메인);
			this.mDataArea.Size = new System.Drawing.Size(1502, 926);
			// 
			// spc메인
			// 
			this.spc메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spc메인.Location = new System.Drawing.Point(0, 0);
			this.spc메인.Name = "spc메인";
			this.spc메인.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spc메인.Panel1
			// 
			this.spc메인.Panel1.Controls.Add(this.lay헤드);
			// 
			// spc메인.Panel2
			// 
			this.spc메인.Panel2.Controls.Add(this.lay라인);
			this.spc메인.Size = new System.Drawing.Size(1502, 926);
			this.spc메인.SplitterDistance = 500;
			this.spc메인.TabIndex = 0;
			// 
			// lay헤드
			// 
			this.lay헤드.ColumnCount = 1;
			this.lay헤드.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay헤드.Controls.Add(this.fgd헤드, 0, 1);
			this.lay헤드.Controls.Add(this.one검색, 0, 0);
			this.lay헤드.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lay헤드.Location = new System.Drawing.Point(0, 0);
			this.lay헤드.Name = "lay헤드";
			this.lay헤드.RowCount = 2;
			this.lay헤드.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.lay헤드.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay헤드.Size = new System.Drawing.Size(1502, 500);
			this.lay헤드.TabIndex = 0;
			// 
			// lay라인
			// 
			this.lay라인.ColumnCount = 1;
			this.lay라인.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay라인.Controls.Add(this.fgd라인, 0, 1);
			this.lay라인.Controls.Add(this.ipnl, 0, 0);
			this.lay라인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lay라인.Location = new System.Drawing.Point(0, 0);
			this.lay라인.Name = "lay라인";
			this.lay라인.RowCount = 2;
			this.lay라인.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.lay라인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay라인.Size = new System.Drawing.Size(1502, 422);
			this.lay라인.TabIndex = 0;
			// 
			// one검색
			// 
			this.one검색.Dock = System.Windows.Forms.DockStyle.Top;
			this.one검색.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.one검색.Location = new System.Drawing.Point(3, 3);
			this.one검색.Name = "one검색";
			this.one검색.Size = new System.Drawing.Size(1496, 43);
			this.one검색.TabIndex = 0;
			// 
			// fgd헤드
			// 
			this.fgd헤드.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.fgd헤드.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.fgd헤드.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.fgd헤드.AutoResize = false;
			this.fgd헤드.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.fgd헤드.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fgd헤드.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.fgd헤드.EnabledHeaderCheck = true;
			this.fgd헤드.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.fgd헤드.Location = new System.Drawing.Point(3, 52);
			this.fgd헤드.Name = "fgd헤드";
			this.fgd헤드.Rows.Count = 1;
			this.fgd헤드.Rows.DefaultSize = 20;
			this.fgd헤드.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.fgd헤드.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.fgd헤드.ShowSort = false;
			this.fgd헤드.Size = new System.Drawing.Size(1496, 445);
			this.fgd헤드.StyleInfo = resources.GetString("fgd헤드.StyleInfo");
			this.fgd헤드.TabIndex = 5;
			this.fgd헤드.UseGridCalculator = true;
			// 
			// ipnl
			// 
			this.ipnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.ipnl.Dock = System.Windows.Forms.DockStyle.Top;
			this.ipnl.LeftImage = null;
			this.ipnl.Location = new System.Drawing.Point(3, 3);
			this.ipnl.Name = "ipnl";
			this.ipnl.Padding = new System.Windows.Forms.Padding(1);
			this.ipnl.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.ipnl.PatternImage = null;
			this.ipnl.RightImage = null;
			this.ipnl.Size = new System.Drawing.Size(1496, 27);
			this.ipnl.TabIndex = 12;
			this.ipnl.TitleText = "상세정보";
			// 
			// fgd라인
			// 
			this.fgd라인.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.fgd라인.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.fgd라인.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.fgd라인.AutoResize = false;
			this.fgd라인.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.fgd라인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fgd라인.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.fgd라인.EnabledHeaderCheck = true;
			this.fgd라인.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.fgd라인.Location = new System.Drawing.Point(3, 36);
			this.fgd라인.Name = "fgd라인";
			this.fgd라인.Rows.Count = 1;
			this.fgd라인.Rows.DefaultSize = 20;
			this.fgd라인.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.fgd라인.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.fgd라인.ShowSort = false;
			this.fgd라인.Size = new System.Drawing.Size(1496, 383);
			this.fgd라인.StyleInfo = resources.GetString("fgd라인.StyleInfo");
			this.fgd라인.TabIndex = 13;
			this.fgd라인.UseGridCalculator = true;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.Controls.Add(this.bpPanelControl7);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1486, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl7
			// 
			this.bpPanelControl7.Controls.Add(this.labelExt1);
			this.bpPanelControl7.Controls.Add(this.tbx사전등록번호);
			this.bpPanelControl7.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl7.Name = "bpPanelControl7";
			this.bpPanelControl7.Size = new System.Drawing.Size(270, 23);
			this.bpPanelControl7.TabIndex = 35;
			this.bpPanelControl7.Text = "bpPanelControl5";
			// 
			// labelExt1
			// 
			this.labelExt1.Location = new System.Drawing.Point(4, 4);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(78, 16);
			this.labelExt1.TabIndex = 16;
			this.labelExt1.Text = "사전등록번호";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbx사전등록번호
			// 
			this.tbx사전등록번호.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.tbx사전등록번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(141)))), ((int)(((byte)(171)))));
			this.tbx사전등록번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx사전등록번호.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.tbx사전등록번호.ColorTag = System.Drawing.Color.Empty;
			this.tbx사전등록번호.Location = new System.Drawing.Point(84, 1);
			this.tbx사전등록번호.Name = "tbx사전등록번호";
			this.tbx사전등록번호.Size = new System.Drawing.Size(185, 20);
			this.tbx사전등록번호.TabIndex = 15;
			// 
			// btn견적등록
			// 
			this.btn견적등록.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn견적등록.BackColor = System.Drawing.Color.White;
			this.btn견적등록.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn견적등록.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn견적등록.Location = new System.Drawing.Point(1419, 10);
			this.btn견적등록.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn견적등록.Name = "btn견적등록";
			this.btn견적등록.Size = new System.Drawing.Size(80, 19);
			this.btn견적등록.TabIndex = 14;
			this.btn견적등록.TabStop = false;
			this.btn견적등록.Text = "견적등록";
			this.btn견적등록.UseVisualStyleBackColor = false;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.ctx회사코드);
			this.bpPanelControl1.Controls.Add(this.labelExt9);
			this.bpPanelControl1.Location = new System.Drawing.Point(274, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(270, 23);
			this.bpPanelControl1.TabIndex = 36;
			this.bpPanelControl1.Text = "bpPanelControl3";
			// 
			// ctx회사코드
			// 
			this.ctx회사코드.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_COMPANY_SUB;
			this.ctx회사코드.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx회사코드.Location = new System.Drawing.Point(84, 1);
			this.ctx회사코드.Name = "ctx회사코드";
			this.ctx회사코드.Size = new System.Drawing.Size(186, 21);
			this.ctx회사코드.TabIndex = 5;
			this.ctx회사코드.TabStop = false;
			this.ctx회사코드.Tag = "";
			// 
			// labelExt9
			// 
			this.labelExt9.Location = new System.Drawing.Point(17, 4);
			this.labelExt9.Name = "labelExt9";
			this.labelExt9.Size = new System.Drawing.Size(65, 16);
			this.labelExt9.TabIndex = 3;
			this.labelExt9.Text = "회사코드";
			this.labelExt9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// P_CZ_SA_QTN_PREREG
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btn견적등록);
			this.Name = "P_CZ_SA_QTN_PREREG";
			this.Size = new System.Drawing.Size(1502, 966);
			this.TitleText = "P_CZ_SA_QTN_PREREG";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btn견적등록, 0);
			this.mDataArea.ResumeLayout(false);
			this.spc메인.Panel1.ResumeLayout(false);
			this.spc메인.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spc메인)).EndInit();
			this.spc메인.ResumeLayout(false);
			this.lay헤드.ResumeLayout(false);
			this.lay라인.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.fgd헤드)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fgd라인)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl7.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer spc메인;
		private System.Windows.Forms.TableLayoutPanel lay헤드;
		private System.Windows.Forms.TableLayoutPanel lay라인;
		private Duzon.Erpiu.Windows.OneControls.OneGrid one검색;
		private Dass.FlexGrid.FlexGrid fgd헤드;
		private Dass.FlexGrid.FlexGrid fgd라인;
		private Duzon.Common.Controls.ImagePanel ipnl;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
		private Duzon.Common.Controls.LabelExt labelExt1;
		private Dintec.DxControls.DxTextBox tbx사전등록번호;
		private Duzon.Common.Controls.RoundedButton btn견적등록;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.BpControls.BpCodeTextBox ctx회사코드;
		private Duzon.Common.Controls.LabelExt labelExt9;
	}
}
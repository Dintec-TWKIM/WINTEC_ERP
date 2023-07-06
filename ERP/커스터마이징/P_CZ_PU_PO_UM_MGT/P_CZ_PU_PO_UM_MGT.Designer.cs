
namespace cz
{
	partial class P_CZ_PU_PO_UM_MGT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PU_PO_UM_MGT));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.one헤드 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.btn확장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.pnl매입처 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx매입처 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl매출처 = new Duzon.Common.Controls.LabelExt();
			this.spc메인 = new System.Windows.Forms.SplitContainer();
			this.lay헤드 = new System.Windows.Forms.TableLayoutPanel();
			this.pnl제목 = new System.Windows.Forms.Panel();
			this.lbl제목 = new System.Windows.Forms.Label();
			this.grd목록 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tab메인 = new Duzon.Common.Controls.TabControlExt();
			this.tab아이템 = new System.Windows.Forms.TabPage();
			this.grd아이템 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tab매입처 = new System.Windows.Forms.TabPage();
			this.grd매입처 = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn테스트 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.pnl매입처.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spc메인)).BeginInit();
			this.spc메인.Panel1.SuspendLayout();
			this.spc메인.Panel2.SuspendLayout();
			this.spc메인.SuspendLayout();
			this.lay헤드.SuspendLayout();
			this.pnl제목.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd목록)).BeginInit();
			this.tab메인.SuspendLayout();
			this.tab아이템.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd아이템)).BeginInit();
			this.tab매입처.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd매입처)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1511, 878);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.one헤드, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.spc메인, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1511, 878);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// one헤드
			// 
			this.one헤드.Dock = System.Windows.Forms.DockStyle.Top;
			this.one헤드.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem2});
			this.one헤드.Location = new System.Drawing.Point(3, 3);
			this.one헤드.Name = "one헤드";
			this.one헤드.Size = new System.Drawing.Size(1505, 61);
			this.one헤드.TabIndex = 0;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl4);
			this.oneGridItem2.Controls.Add(this.pnl매입처);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1495, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 0;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.btn확장);
			this.bpPanelControl4.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpPanelControl4.Location = new System.Drawing.Point(274, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(32, 23);
			this.bpPanelControl4.TabIndex = 39;
			this.bpPanelControl4.Text = "v";
			// 
			// btn확장
			// 
			this.btn확장.BackColor = System.Drawing.Color.White;
			this.btn확장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn확장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn확장.Location = new System.Drawing.Point(10, 2);
			this.btn확장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn확장.Name = "btn확장";
			this.btn확장.Size = new System.Drawing.Size(20, 19);
			this.btn확장.TabIndex = 15;
			this.btn확장.TabStop = false;
			this.btn확장.Text = "<";
			this.btn확장.UseVisualStyleBackColor = false;
			// 
			// pnl매입처
			// 
			this.pnl매입처.Controls.Add(this.ctx매입처);
			this.pnl매입처.Controls.Add(this.lbl매출처);
			this.pnl매입처.Location = new System.Drawing.Point(2, 1);
			this.pnl매입처.Name = "pnl매입처";
			this.pnl매입처.Size = new System.Drawing.Size(270, 23);
			this.pnl매입처.TabIndex = 23;
			this.pnl매입처.Text = "bpPanelControl4";
			// 
			// ctx매입처
			// 
			this.ctx매입처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
			this.ctx매입처.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx매입처.Location = new System.Drawing.Point(84, 1);
			this.ctx매입처.Name = "ctx매입처";
			this.ctx매입처.Size = new System.Drawing.Size(186, 21);
			this.ctx매입처.TabIndex = 1;
			this.ctx매입처.TabStop = false;
			this.ctx매입처.Tag = "CD_HEAD,NM_HEAD";
			// 
			// lbl매출처
			// 
			this.lbl매출처.Location = new System.Drawing.Point(17, 4);
			this.lbl매출처.Name = "lbl매출처";
			this.lbl매출처.Size = new System.Drawing.Size(65, 16);
			this.lbl매출처.TabIndex = 0;
			this.lbl매출처.Text = "매입처";
			this.lbl매출처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// spc메인
			// 
			this.spc메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spc메인.Location = new System.Drawing.Point(0, 70);
			this.spc메인.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.spc메인.Name = "spc메인";
			// 
			// spc메인.Panel1
			// 
			this.spc메인.Panel1.Controls.Add(this.lay헤드);
			// 
			// spc메인.Panel2
			// 
			this.spc메인.Panel2.Controls.Add(this.tab메인);
			this.spc메인.Size = new System.Drawing.Size(1511, 808);
			this.spc메인.SplitterDistance = 503;
			this.spc메인.TabIndex = 2;
			// 
			// lay헤드
			// 
			this.lay헤드.ColumnCount = 1;
			this.lay헤드.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay헤드.Controls.Add(this.pnl제목, 0, 0);
			this.lay헤드.Controls.Add(this.grd목록, 0, 1);
			this.lay헤드.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lay헤드.Location = new System.Drawing.Point(0, 0);
			this.lay헤드.Name = "lay헤드";
			this.lay헤드.RowCount = 2;
			this.lay헤드.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.lay헤드.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay헤드.Size = new System.Drawing.Size(503, 808);
			this.lay헤드.TabIndex = 13;
			// 
			// pnl제목
			// 
			this.pnl제목.BackColor = System.Drawing.Color.DimGray;
			this.pnl제목.Controls.Add(this.lbl제목);
			this.pnl제목.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl제목.Location = new System.Drawing.Point(0, 0);
			this.pnl제목.Margin = new System.Windows.Forms.Padding(0, 0, 3, 1);
			this.pnl제목.Name = "pnl제목";
			this.pnl제목.Padding = new System.Windows.Forms.Padding(1);
			this.pnl제목.Size = new System.Drawing.Size(500, 23);
			this.pnl제목.TabIndex = 40;
			// 
			// lbl제목
			// 
			this.lbl제목.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(226)))), ((int)(((byte)(236)))));
			this.lbl제목.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbl제목.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.lbl제목.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbl제목.Location = new System.Drawing.Point(1, 1);
			this.lbl제목.Margin = new System.Windows.Forms.Padding(0);
			this.lbl제목.Name = "lbl제목";
			this.lbl제목.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.lbl제목.Size = new System.Drawing.Size(498, 21);
			this.lbl제목.TabIndex = 130;
			this.lbl제목.Text = "매입처 리스트";
			this.lbl제목.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// grd목록
			// 
			this.grd목록.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd목록.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd목록.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd목록.AutoResize = false;
			this.grd목록.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd목록.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd목록.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd목록.EnabledHeaderCheck = true;
			this.grd목록.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd목록.Location = new System.Drawing.Point(0, 27);
			this.grd목록.Margin = new System.Windows.Forms.Padding(0, 3, 3, 0);
			this.grd목록.Name = "grd목록";
			this.grd목록.Rows.Count = 1;
			this.grd목록.Rows.DefaultSize = 20;
			this.grd목록.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
			this.grd목록.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd목록.ShowSort = false;
			this.grd목록.Size = new System.Drawing.Size(500, 781);
			this.grd목록.StyleInfo = resources.GetString("grd목록.StyleInfo");
			this.grd목록.TabIndex = 12;
			this.grd목록.UseGridCalculator = true;
			// 
			// tab메인
			// 
			this.tab메인.Controls.Add(this.tab아이템);
			this.tab메인.Controls.Add(this.tab매입처);
			this.tab메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tab메인.ItemSize = new System.Drawing.Size(120, 20);
			this.tab메인.Location = new System.Drawing.Point(0, 0);
			this.tab메인.Name = "tab메인";
			this.tab메인.SelectedIndex = 0;
			this.tab메인.Size = new System.Drawing.Size(1004, 808);
			this.tab메인.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tab메인.TabIndex = 1;
			// 
			// tab아이템
			// 
			this.tab아이템.Controls.Add(this.grd아이템);
			this.tab아이템.Location = new System.Drawing.Point(4, 24);
			this.tab아이템.Name = "tab아이템";
			this.tab아이템.Padding = new System.Windows.Forms.Padding(3);
			this.tab아이템.Size = new System.Drawing.Size(996, 780);
			this.tab아이템.TabIndex = 0;
			this.tab아이템.Text = "아이템";
			this.tab아이템.UseVisualStyleBackColor = true;
			// 
			// grd아이템
			// 
			this.grd아이템.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd아이템.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd아이템.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd아이템.AutoResize = false;
			this.grd아이템.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd아이템.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd아이템.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd아이템.EnabledHeaderCheck = true;
			this.grd아이템.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd아이템.Location = new System.Drawing.Point(3, 3);
			this.grd아이템.Name = "grd아이템";
			this.grd아이템.Rows.Count = 1;
			this.grd아이템.Rows.DefaultSize = 20;
			this.grd아이템.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
			this.grd아이템.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd아이템.ShowSort = false;
			this.grd아이템.Size = new System.Drawing.Size(990, 774);
			this.grd아이템.StyleInfo = resources.GetString("grd아이템.StyleInfo");
			this.grd아이템.TabIndex = 11;
			this.grd아이템.UseGridCalculator = true;
			// 
			// tab매입처
			// 
			this.tab매입처.Controls.Add(this.grd매입처);
			this.tab매입처.Location = new System.Drawing.Point(4, 24);
			this.tab매입처.Name = "tab매입처";
			this.tab매입처.Padding = new System.Windows.Forms.Padding(3);
			this.tab매입처.Size = new System.Drawing.Size(716, 658);
			this.tab매입처.TabIndex = 1;
			this.tab매입처.Text = "매입처";
			this.tab매입처.UseVisualStyleBackColor = true;
			// 
			// grd매입처
			// 
			this.grd매입처.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd매입처.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd매입처.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd매입처.AutoResize = false;
			this.grd매입처.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd매입처.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd매입처.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd매입처.EnabledHeaderCheck = true;
			this.grd매입처.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd매입처.Location = new System.Drawing.Point(3, 3);
			this.grd매입처.Name = "grd매입처";
			this.grd매입처.Rows.Count = 1;
			this.grd매입처.Rows.DefaultSize = 20;
			this.grd매입처.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
			this.grd매입처.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd매입처.ShowSort = false;
			this.grd매입처.Size = new System.Drawing.Size(710, 652);
			this.grd매입처.StyleInfo = resources.GetString("grd매입처.StyleInfo");
			this.grd매입처.TabIndex = 12;
			this.grd매입처.UseGridCalculator = true;
			// 
			// btn추가
			// 
			this.btn추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn추가.BackColor = System.Drawing.Color.Transparent;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.Location = new System.Drawing.Point(1344, 10);
			this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn추가.Name = "btn추가";
			this.btn추가.Size = new System.Drawing.Size(70, 19);
			this.btn추가.TabIndex = 7;
			this.btn추가.TabStop = false;
			this.btn추가.Text = "추가";
			this.btn추가.UseVisualStyleBackColor = false;
			// 
			// btn삭제
			// 
			this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn삭제.BackColor = System.Drawing.Color.Transparent;
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(1420, 10);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(70, 19);
			this.btn삭제.TabIndex = 8;
			this.btn삭제.TabStop = false;
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// btn테스트
			// 
			this.btn테스트.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn테스트.BackColor = System.Drawing.Color.Transparent;
			this.btn테스트.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn테스트.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn테스트.Location = new System.Drawing.Point(1025, 10);
			this.btn테스트.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn테스트.Name = "btn테스트";
			this.btn테스트.Size = new System.Drawing.Size(70, 19);
			this.btn테스트.TabIndex = 9;
			this.btn테스트.TabStop = false;
			this.btn테스트.Text = "테스트";
			this.btn테스트.UseVisualStyleBackColor = false;
			// 
			// P_CZ_PU_PO_UM_MGT
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btn테스트);
			this.Controls.Add(this.btn삭제);
			this.Controls.Add(this.btn추가);
			this.Name = "P_CZ_PU_PO_UM_MGT";
			this.Size = new System.Drawing.Size(1511, 918);
			this.TitleText = "P_CZ_PU_PO_UM_MGT";
			this.Controls.SetChildIndex(this.btn추가, 0);
			this.Controls.SetChildIndex(this.btn삭제, 0);
			this.Controls.SetChildIndex(this.btn테스트, 0);
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.pnl매입처.ResumeLayout(false);
			this.spc메인.Panel1.ResumeLayout(false);
			this.spc메인.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spc메인)).EndInit();
			this.spc메인.ResumeLayout(false);
			this.lay헤드.ResumeLayout(false);
			this.pnl제목.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd목록)).EndInit();
			this.tab메인.ResumeLayout(false);
			this.tab아이템.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd아이템)).EndInit();
			this.tab매입처.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd매입처)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid one헤드;
		private Duzon.Common.Controls.RoundedButton btn추가;
		private Duzon.Common.Controls.TabControlExt tab메인;
		private System.Windows.Forms.TabPage tab아이템;
		private Dass.FlexGrid.FlexGrid grd아이템;
		private System.Windows.Forms.TabPage tab매입처;
		private Dass.FlexGrid.FlexGrid grd매입처;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.Controls.RoundedButton btn삭제;
		private Duzon.Common.BpControls.BpPanelControl pnl매입처;
		private Duzon.Common.BpControls.BpCodeTextBox ctx매입처;
		private Duzon.Common.Controls.LabelExt lbl매출처;
		private Duzon.Common.Controls.RoundedButton btn테스트;
		private System.Windows.Forms.SplitContainer spc메인;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
		private Duzon.Common.Controls.RoundedButton btn확장;
		private System.Windows.Forms.TableLayoutPanel lay헤드;
		private System.Windows.Forms.Panel pnl제목;
		private System.Windows.Forms.Label lbl제목;
		private Dass.FlexGrid.FlexGrid grd목록;
	}
}
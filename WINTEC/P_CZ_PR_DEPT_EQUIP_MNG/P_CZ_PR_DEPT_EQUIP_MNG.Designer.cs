
namespace cz
{
	partial class P_CZ_PR_DEPT_EQUIP_MNG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_DEPT_EQUIP_MNG));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.chk설비미할당 = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt작업지시번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl작업지시번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx설비 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl설비 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo부서 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl부서 = new Duzon.Common.Controls.LabelExt();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.btn할당취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this._flex작업대기 = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn설비할당 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.imagePanel2 = new Duzon.Common.Controls.ImagePanel(this.components);
			this._flex수주정보 = new Dass.FlexGrid.FlexGrid(this.components);
			this.imagePanel3 = new Duzon.Common.Controls.ImagePanel(this.components);
			this._flex작업진행 = new Dass.FlexGrid.FlexGrid(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.imagePanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex작업대기)).BeginInit();
			this.imagePanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex수주정보)).BeginInit();
			this.imagePanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex작업진행)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1706, 836);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1706, 836);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1700, 41);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.chk설비미할당);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1690, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// chk설비미할당
			// 
			this.chk설비미할당.AutoSize = true;
			this.chk설비미할당.Location = new System.Drawing.Point(884, 1);
			this.chk설비미할당.Name = "chk설비미할당";
			this.chk설비미할당.Size = new System.Drawing.Size(104, 24);
			this.chk설비미할당.TabIndex = 2;
			this.chk설비미할당.Text = "설비미할당";
			this.chk설비미할당.TextDD = null;
			this.chk설비미할당.UseVisualStyleBackColor = true;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.txt작업지시번호);
			this.bpPanelControl2.Controls.Add(this.lbl작업지시번호);
			this.bpPanelControl2.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// txt작업지시번호
			// 
			this.txt작업지시번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt작업지시번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt작업지시번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt작업지시번호.Location = new System.Drawing.Point(106, 0);
			this.txt작업지시번호.Name = "txt작업지시번호";
			this.txt작업지시번호.Size = new System.Drawing.Size(186, 21);
			this.txt작업지시번호.TabIndex = 1;
			// 
			// lbl작업지시번호
			// 
			this.lbl작업지시번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl작업지시번호.Location = new System.Drawing.Point(0, 0);
			this.lbl작업지시번호.Name = "lbl작업지시번호";
			this.lbl작업지시번호.Size = new System.Drawing.Size(100, 23);
			this.lbl작업지시번호.TabIndex = 0;
			this.lbl작업지시번호.Text = "작업지시번호";
			this.lbl작업지시번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.ctx설비);
			this.bpPanelControl3.Controls.Add(this.lbl설비);
			this.bpPanelControl3.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 3;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// ctx설비
			// 
			this.ctx설비.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx설비.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_TABLE_SUB;
			this.ctx설비.Location = new System.Drawing.Point(106, 0);
			this.ctx설비.Name = "ctx설비";
			this.ctx설비.Size = new System.Drawing.Size(186, 21);
			this.ctx설비.TabIndex = 1;
			this.ctx설비.TabStop = false;
			this.ctx설비.Text = "bpCodeTextBox1";
			// 
			// lbl설비
			// 
			this.lbl설비.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl설비.Location = new System.Drawing.Point(0, 0);
			this.lbl설비.Name = "lbl설비";
			this.lbl설비.Size = new System.Drawing.Size(100, 23);
			this.lbl설비.TabIndex = 0;
			this.lbl설비.Text = "설비";
			this.lbl설비.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.cbo부서);
			this.bpPanelControl1.Controls.Add(this.lbl부서);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// cbo부서
			// 
			this.cbo부서.AutoDropDown = true;
			this.cbo부서.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo부서.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo부서.FormattingEnabled = true;
			this.cbo부서.ItemHeight = 12;
			this.cbo부서.Location = new System.Drawing.Point(106, 0);
			this.cbo부서.Name = "cbo부서";
			this.cbo부서.Size = new System.Drawing.Size(186, 20);
			this.cbo부서.TabIndex = 1;
			// 
			// lbl부서
			// 
			this.lbl부서.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl부서.Location = new System.Drawing.Point(0, 0);
			this.lbl부서.Name = "lbl부서";
			this.lbl부서.Size = new System.Drawing.Size(100, 23);
			this.lbl부서.TabIndex = 0;
			this.lbl부서.Text = "부서";
			this.lbl부서.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.imagePanel3);
			this.splitContainer1.Size = new System.Drawing.Size(1700, 783);
			this.splitContainer1.SplitterDistance = 381;
			this.splitContainer1.TabIndex = 1;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.imagePanel1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.imagePanel2);
			this.splitContainer2.Size = new System.Drawing.Size(1700, 381);
			this.splitContainer2.SplitterDistance = 837;
			this.splitContainer2.TabIndex = 0;
			// 
			// imagePanel1
			// 
			this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel1.Controls.Add(this.btn할당취소);
			this.imagePanel1.Controls.Add(this._flex작업대기);
			this.imagePanel1.Controls.Add(this.btn설비할당);
			this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel1.LeftImage = null;
			this.imagePanel1.Location = new System.Drawing.Point(0, 0);
			this.imagePanel1.Name = "imagePanel1";
			this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel1.PatternImage = null;
			this.imagePanel1.RightImage = null;
			this.imagePanel1.Size = new System.Drawing.Size(837, 381);
			this.imagePanel1.TabIndex = 1;
			this.imagePanel1.TitleText = "작업대기";
			// 
			// btn할당취소
			// 
			this.btn할당취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn할당취소.BackColor = System.Drawing.Color.Transparent;
			this.btn할당취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn할당취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn할당취소.Location = new System.Drawing.Point(750, 4);
			this.btn할당취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn할당취소.Name = "btn할당취소";
			this.btn할당취소.Size = new System.Drawing.Size(84, 19);
			this.btn할당취소.TabIndex = 4;
			this.btn할당취소.TabStop = false;
			this.btn할당취소.Text = "할당취소";
			this.btn할당취소.UseVisualStyleBackColor = false;
			// 
			// _flex작업대기
			// 
			this._flex작업대기.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex작업대기.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex작업대기.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex작업대기.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex작업대기.AutoResize = false;
			this._flex작업대기.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex작업대기.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex작업대기.EnabledHeaderCheck = true;
			this._flex작업대기.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex작업대기.Location = new System.Drawing.Point(3, 28);
			this._flex작업대기.Name = "_flex작업대기";
			this._flex작업대기.Rows.Count = 1;
			this._flex작업대기.Rows.DefaultSize = 20;
			this._flex작업대기.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex작업대기.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex작업대기.ShowSort = false;
			this._flex작업대기.Size = new System.Drawing.Size(831, 350);
			this._flex작업대기.StyleInfo = resources.GetString("_flex작업대기.StyleInfo");
			this._flex작업대기.TabIndex = 1;
			this._flex작업대기.UseGridCalculator = true;
			// 
			// btn설비할당
			// 
			this.btn설비할당.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn설비할당.BackColor = System.Drawing.Color.Transparent;
			this.btn설비할당.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn설비할당.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn설비할당.Location = new System.Drawing.Point(660, 4);
			this.btn설비할당.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn설비할당.Name = "btn설비할당";
			this.btn설비할당.Size = new System.Drawing.Size(84, 19);
			this.btn설비할당.TabIndex = 3;
			this.btn설비할당.TabStop = false;
			this.btn설비할당.Text = "설비할당";
			this.btn설비할당.UseVisualStyleBackColor = false;
			// 
			// imagePanel2
			// 
			this.imagePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel2.Controls.Add(this._flex수주정보);
			this.imagePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel2.LeftImage = null;
			this.imagePanel2.Location = new System.Drawing.Point(0, 0);
			this.imagePanel2.Name = "imagePanel2";
			this.imagePanel2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel2.PatternImage = null;
			this.imagePanel2.RightImage = null;
			this.imagePanel2.Size = new System.Drawing.Size(859, 381);
			this.imagePanel2.TabIndex = 0;
			this.imagePanel2.TitleText = "수주정보";
			// 
			// _flex수주정보
			// 
			this._flex수주정보.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex수주정보.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex수주정보.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex수주정보.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex수주정보.AutoResize = false;
			this._flex수주정보.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex수주정보.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex수주정보.EnabledHeaderCheck = true;
			this._flex수주정보.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex수주정보.Location = new System.Drawing.Point(3, 28);
			this._flex수주정보.Name = "_flex수주정보";
			this._flex수주정보.Rows.Count = 1;
			this._flex수주정보.Rows.DefaultSize = 20;
			this._flex수주정보.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex수주정보.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex수주정보.ShowSort = false;
			this._flex수주정보.Size = new System.Drawing.Size(853, 350);
			this._flex수주정보.StyleInfo = resources.GetString("_flex수주정보.StyleInfo");
			this._flex수주정보.TabIndex = 1;
			this._flex수주정보.UseGridCalculator = true;
			// 
			// imagePanel3
			// 
			this.imagePanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel3.Controls.Add(this._flex작업진행);
			this.imagePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel3.LeftImage = null;
			this.imagePanel3.Location = new System.Drawing.Point(0, 0);
			this.imagePanel3.Name = "imagePanel3";
			this.imagePanel3.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel3.PatternImage = null;
			this.imagePanel3.RightImage = null;
			this.imagePanel3.Size = new System.Drawing.Size(1700, 398);
			this.imagePanel3.TabIndex = 1;
			this.imagePanel3.TitleText = "작업진행";
			// 
			// _flex작업진행
			// 
			this._flex작업진행.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex작업진행.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex작업진행.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex작업진행.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._flex작업진행.AutoResize = false;
			this._flex작업진행.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex작업진행.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex작업진행.EnabledHeaderCheck = true;
			this._flex작업진행.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex작업진행.Location = new System.Drawing.Point(3, 28);
			this._flex작업진행.Name = "_flex작업진행";
			this._flex작업진행.Rows.Count = 1;
			this._flex작업진행.Rows.DefaultSize = 20;
			this._flex작업진행.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex작업진행.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex작업진행.ShowSort = false;
			this._flex작업진행.Size = new System.Drawing.Size(1694, 367);
			this._flex작업진행.StyleInfo = resources.GetString("_flex작업진행.StyleInfo");
			this._flex작업진행.TabIndex = 1;
			this._flex작업진행.UseGridCalculator = true;
			// 
			// P_CZ_PR_DEPT_EQUIP_MNG
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "P_CZ_PR_DEPT_EQUIP_MNG";
			this.Size = new System.Drawing.Size(1706, 876);
			this.TitleText = "부서별설비관리";
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.oneGridItem1.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl2.PerformLayout();
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.imagePanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex작업대기)).EndInit();
			this.imagePanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex수주정보)).EndInit();
			this.imagePanel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex작업진행)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.DropDownComboBox cbo부서;
		private Duzon.Common.Controls.LabelExt lbl부서;
		private Duzon.Common.Controls.ImagePanel imagePanel1;
		private Dass.FlexGrid.FlexGrid _flex작업대기;
		private Duzon.Common.Controls.ImagePanel imagePanel2;
		private Dass.FlexGrid.FlexGrid _flex수주정보;
		private Duzon.Common.Controls.ImagePanel imagePanel3;
		private Dass.FlexGrid.FlexGrid _flex작업진행;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.TextBoxExt txt작업지시번호;
		private Duzon.Common.Controls.LabelExt lbl작업지시번호;
		private Duzon.Common.Controls.RoundedButton btn설비할당;
		private Duzon.Common.Controls.RoundedButton btn할당취소;
		private Duzon.Common.Controls.CheckBoxExt chk설비미할당;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.Controls.LabelExt lbl설비;
		private Duzon.Common.BpControls.BpCodeTextBox ctx설비;
	}
}
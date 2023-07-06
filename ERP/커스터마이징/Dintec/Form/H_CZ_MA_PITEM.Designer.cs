namespace Dintec
{
	partial class H_CZ_MA_PITEM
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_CZ_MA_PITEM));
			this.panelExt1 = new Duzon.Common.Controls.PanelExt();
			this.tlay메인 = new System.Windows.Forms.TableLayoutPanel();
			this.pnl제목1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.oneS = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.cbo계정구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt검색 = new Duzon.Common.Controls.TextBoxExt();
			this.labelExt3 = new Duzon.Common.Controls.LabelExt();
			this.flex = new Dass.FlexGrid.FlexGrid(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.panelExt1.SuspendLayout();
			this.tlay메인.SuspendLayout();
			this.pnl제목1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.flex)).BeginInit();
			this.SuspendLayout();
			// 
			// panelExt1
			// 
			this.panelExt1.BackColor = System.Drawing.Color.Transparent;
			this.panelExt1.Controls.Add(this.tlay메인);
			this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt1.Location = new System.Drawing.Point(0, 0);
			this.panelExt1.Name = "panelExt1";
			this.panelExt1.Padding = new System.Windows.Forms.Padding(6, 53, 6, 3);
			this.panelExt1.Size = new System.Drawing.Size(842, 541);
			this.panelExt1.TabIndex = 1;
			// 
			// tlay메인
			// 
			this.tlay메인.ColumnCount = 1;
			this.tlay메인.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlay메인.Controls.Add(this.pnl제목1, 0, 1);
			this.tlay메인.Controls.Add(this.oneS, 0, 0);
			this.tlay메인.Controls.Add(this.flex, 0, 2);
			this.tlay메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlay메인.Location = new System.Drawing.Point(6, 53);
			this.tlay메인.Name = "tlay메인";
			this.tlay메인.RowCount = 3;
			this.tlay메인.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlay메인.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlay메인.Size = new System.Drawing.Size(830, 485);
			this.tlay메인.TabIndex = 1;
			// 
			// pnl제목1
			// 
			this.pnl제목1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.pnl제목1.Controls.Add(this.btn확인);
			this.pnl제목1.Controls.Add(this.btn취소);
			this.pnl제목1.Controls.Add(this.btn조회);
			this.pnl제목1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl제목1.LeftImage = null;
			this.pnl제목1.Location = new System.Drawing.Point(3, 72);
			this.pnl제목1.Name = "pnl제목1";
			this.pnl제목1.Padding = new System.Windows.Forms.Padding(1);
			this.pnl제목1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.pnl제목1.PatternImage = null;
			this.pnl제목1.RightImage = null;
			this.pnl제목1.Size = new System.Drawing.Size(824, 27);
			this.pnl제목1.TabIndex = 8;
			this.pnl제목1.TitleText = "품목 정보";
			// 
			// btn확인
			// 
			this.btn확인.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn확인.BackColor = System.Drawing.Color.Transparent;
			this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn확인.Location = new System.Drawing.Point(671, 4);
			this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn확인.Name = "btn확인";
			this.btn확인.Size = new System.Drawing.Size(70, 19);
			this.btn확인.TabIndex = 1;
			this.btn확인.TabStop = false;
			this.btn확인.Text = "확인";
			this.btn확인.UseVisualStyleBackColor = false;
			// 
			// btn취소
			// 
			this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn취소.BackColor = System.Drawing.Color.Transparent;
			this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.Location = new System.Drawing.Point(747, 4);
			this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(70, 19);
			this.btn취소.TabIndex = 2;
			this.btn취소.TabStop = false;
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn조회.BackColor = System.Drawing.Color.Transparent;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(595, 4);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 2;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// oneS
			// 
			this.oneS.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneS.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneS.Location = new System.Drawing.Point(3, 3);
			this.oneS.Name = "oneS";
			this.oneS.Size = new System.Drawing.Size(824, 63);
			this.oneS.TabIndex = 1;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(814, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.labelExt2);
			this.bpPanelControl1.Controls.Add(this.cbo계정구분);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(661, 23);
			this.bpPanelControl1.TabIndex = 5;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// labelExt2
			// 
			this.labelExt2.Location = new System.Drawing.Point(5, 3);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Resizeble = true;
			this.labelExt2.Size = new System.Drawing.Size(75, 16);
			this.labelExt2.TabIndex = 3;
			this.labelExt2.Text = "계정구분";
			this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo계정구분
			// 
			this.cbo계정구분.AutoDropDown = true;
			this.cbo계정구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo계정구분.FormattingEnabled = true;
			this.cbo계정구분.ItemHeight = 12;
			this.cbo계정구분.Location = new System.Drawing.Point(82, 1);
			this.cbo계정구분.Name = "cbo계정구분";
			this.cbo계정구분.ShowCheckBox = false;
			this.cbo계정구분.Size = new System.Drawing.Size(140, 20);
			this.cbo계정구분.TabIndex = 2;
			this.cbo계정구분.UseKeyEnter = true;
			this.cbo계정구분.UseKeyF3 = true;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl3);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(814, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.txt검색);
			this.bpPanelControl3.Controls.Add(this.labelExt3);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(661, 23);
			this.bpPanelControl3.TabIndex = 7;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// txt검색
			// 
			this.txt검색.BackColor = System.Drawing.Color.White;
			this.txt검색.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt검색.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt검색.Location = new System.Drawing.Point(82, 1);
			this.txt검색.Name = "txt검색";
			this.txt검색.SelectedAllEnabled = false;
			this.txt검색.Size = new System.Drawing.Size(551, 21);
			this.txt검색.TabIndex = 6;
			this.txt검색.Tag = "";
			this.txt검색.UseKeyEnter = false;
			this.txt검색.UseKeyF3 = true;
			// 
			// labelExt3
			// 
			this.labelExt3.Location = new System.Drawing.Point(5, 3);
			this.labelExt3.Name = "labelExt3";
			this.labelExt3.Resizeble = true;
			this.labelExt3.Size = new System.Drawing.Size(75, 16);
			this.labelExt3.TabIndex = 5;
			this.labelExt3.Text = "검색";
			this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// flex
			// 
			this.flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.flex.AutoResize = false;
			this.flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.flex.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.flex.EnabledHeaderCheck = true;
			this.flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.flex.Location = new System.Drawing.Point(3, 105);
			this.flex.Name = "flex";
			this.flex.Rows.Count = 1;
			this.flex.Rows.DefaultSize = 18;
			this.flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.flex.ShowSort = false;
			this.flex.Size = new System.Drawing.Size(824, 377);
			this.flex.StyleInfo = resources.GetString("flex.StyleInfo");
			this.flex.TabIndex = 10;
			this.flex.UseGridCalculator = true;
			// 
			// H_CZ_MA_PITEM
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(842, 541);
			this.Controls.Add(this.panelExt1);
			this.Name = "H_CZ_MA_PITEM";
			this.Text = "H_CZ_MA_PITEM";
			this.TitleText = "품목 연결";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.panelExt1.ResumeLayout(false);
			this.tlay메인.ResumeLayout(false);
			this.pnl제목1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.flex)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Duzon.Common.Controls.PanelExt panelExt1;
		private System.Windows.Forms.TableLayoutPanel tlay메인;
		private Duzon.Common.Controls.RoundedButton btn취소;
		private Duzon.Common.Controls.RoundedButton btn확인;
		private Duzon.Common.Controls.ImagePanel pnl제목1;
		private Duzon.Common.Controls.RoundedButton btn조회;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneS;
		private Dass.FlexGrid.FlexGrid flex;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.LabelExt labelExt2;
		private Duzon.Common.Controls.DropDownComboBox cbo계정구분;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.Controls.LabelExt labelExt3;
		private Duzon.Common.Controls.TextBoxExt txt검색;
	}
}
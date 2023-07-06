
namespace cz
{
	partial class P_CZ_DX_QR_UPLOAD
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_DX_QR_UPLOAD));
			this.spc메인 = new System.Windows.Forms.SplitContainer();
			this.lay메인 = new System.Windows.Forms.TableLayoutPanel();
			this.lay조건 = new System.Windows.Forms.TableLayoutPanel();
			this.panelExt2 = new Duzon.Common.Controls.PanelExt();
			this.dtp선적일자 = new Duzon.Common.Controls.DatePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.panelExt1 = new Duzon.Common.Controls.PanelExt();
			this.chk바코드 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk데이터매트릭스 = new Duzon.Common.Controls.CheckBoxExt();
			this.chkQR코드 = new Duzon.Common.Controls.CheckBoxExt();
			this.label1 = new System.Windows.Forms.Label();
			this.panelExt11 = new Duzon.Common.Controls.PanelExt();
			this.cbo문서종류 = new Duzon.Common.Controls.DropDownComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.spc라인 = new System.Windows.Forms.SplitContainer();
			this.grd라인 = new Dass.FlexGrid.FlexGrid(this.components);
			this.grd최종 = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn업로드_단일문서 = new System.Windows.Forms.Button();
			this.btn업로드_혼합문서 = new System.Windows.Forms.Button();
			this.btn자동업로드 = new System.Windows.Forms.Button();
			this.tbx로그 = new System.Windows.Forms.TextBox();
			this.mDataArea.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spc메인)).BeginInit();
			this.spc메인.Panel1.SuspendLayout();
			this.spc메인.Panel2.SuspendLayout();
			this.spc메인.SuspendLayout();
			this.lay메인.SuspendLayout();
			this.lay조건.SuspendLayout();
			this.panelExt2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp선적일자)).BeginInit();
			this.panelExt1.SuspendLayout();
			this.panelExt11.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spc라인)).BeginInit();
			this.spc라인.Panel1.SuspendLayout();
			this.spc라인.Panel2.SuspendLayout();
			this.spc라인.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd라인)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grd최종)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.spc메인);
			this.mDataArea.Size = new System.Drawing.Size(1532, 965);
			// 
			// spc메인
			// 
			this.spc메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spc메인.Location = new System.Drawing.Point(0, 0);
			this.spc메인.Name = "spc메인";
			// 
			// spc메인.Panel1
			// 
			this.spc메인.Panel1.Controls.Add(this.tbx로그);
			this.spc메인.Panel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
			// 
			// spc메인.Panel2
			// 
			this.spc메인.Panel2.Controls.Add(this.lay메인);
			this.spc메인.Size = new System.Drawing.Size(1532, 965);
			this.spc메인.SplitterDistance = 440;
			this.spc메인.TabIndex = 0;
			// 
			// lay메인
			// 
			this.lay메인.ColumnCount = 1;
			this.lay메인.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay메인.Controls.Add(this.lay조건, 0, 0);
			this.lay메인.Controls.Add(this.spc라인, 0, 1);
			this.lay메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lay메인.Location = new System.Drawing.Point(0, 0);
			this.lay메인.Name = "lay메인";
			this.lay메인.RowCount = 2;
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay메인.Size = new System.Drawing.Size(1088, 965);
			this.lay메인.TabIndex = 0;
			// 
			// lay조건
			// 
			this.lay조건.ColumnCount = 6;
			this.lay조건.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.lay조건.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.lay조건.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.lay조건.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
			this.lay조건.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.lay조건.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay조건.Controls.Add(this.panelExt2, 5, 0);
			this.lay조건.Controls.Add(this.label2, 4, 0);
			this.lay조건.Controls.Add(this.panelExt1, 3, 0);
			this.lay조건.Controls.Add(this.label1, 2, 0);
			this.lay조건.Controls.Add(this.panelExt11, 1, 0);
			this.lay조건.Controls.Add(this.label11, 0, 0);
			this.lay조건.Dock = System.Windows.Forms.DockStyle.Top;
			this.lay조건.Location = new System.Drawing.Point(0, 3);
			this.lay조건.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.lay조건.Name = "lay조건";
			this.lay조건.RowCount = 1;
			this.lay조건.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay조건.Size = new System.Drawing.Size(1088, 42);
			this.lay조건.TabIndex = 0;
			this.lay조건.Tag = "VIEWBOX";
			// 
			// panelExt2
			// 
			this.panelExt2.Controls.Add(this.dtp선적일자);
			this.panelExt2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelExt2.Location = new System.Drawing.Point(801, 1);
			this.panelExt2.Margin = new System.Windows.Forms.Padding(1);
			this.panelExt2.Name = "panelExt2";
			this.panelExt2.Size = new System.Drawing.Size(198, 40);
			this.panelExt2.TabIndex = 24;
			// 
			// dtp선적일자
			// 
			this.dtp선적일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp선적일자.Location = new System.Drawing.Point(10, 9);
			this.dtp선적일자.Mask = "####/##/##";
			this.dtp선적일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp선적일자.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp선적일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp선적일자.Modified = true;
			this.dtp선적일자.Name = "dtp선적일자";
			this.dtp선적일자.Size = new System.Drawing.Size(90, 21);
			this.dtp선적일자.TabIndex = 131;
			this.dtp선적일자.Tag = "";
			this.dtp선적일자.Value = new System.DateTime(((long)(0)));
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label2.Location = new System.Drawing.Point(701, 1);
			this.label2.Margin = new System.Windows.Forms.Padding(1);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
			this.label2.Size = new System.Drawing.Size(98, 40);
			this.label2.TabIndex = 23;
			this.label2.Text = "선적일자";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelExt1
			// 
			this.panelExt1.Controls.Add(this.chk바코드);
			this.panelExt1.Controls.Add(this.chk데이터매트릭스);
			this.panelExt1.Controls.Add(this.chkQR코드);
			this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt1.Location = new System.Drawing.Point(401, 1);
			this.panelExt1.Margin = new System.Windows.Forms.Padding(1);
			this.panelExt1.Name = "panelExt1";
			this.panelExt1.Size = new System.Drawing.Size(298, 40);
			this.panelExt1.TabIndex = 22;
			// 
			// chk바코드
			// 
			this.chk바코드.Checked = true;
			this.chk바코드.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk바코드.Location = new System.Drawing.Point(207, 12);
			this.chk바코드.Name = "chk바코드";
			this.chk바코드.Size = new System.Drawing.Size(73, 18);
			this.chk바코드.TabIndex = 22;
			this.chk바코드.Tag = "";
			this.chk바코드.Text = "바코드";
			this.chk바코드.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.chk바코드.TextDD = null;
			this.chk바코드.UseVisualStyleBackColor = false;
			// 
			// chk데이터매트릭스
			// 
			this.chk데이터매트릭스.Checked = true;
			this.chk데이터매트릭스.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk데이터매트릭스.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.chk데이터매트릭스.Location = new System.Drawing.Point(87, 12);
			this.chk데이터매트릭스.Name = "chk데이터매트릭스";
			this.chk데이터매트릭스.Size = new System.Drawing.Size(118, 18);
			this.chk데이터매트릭스.TabIndex = 21;
			this.chk데이터매트릭스.Tag = "";
			this.chk데이터매트릭스.Text = "데이터매트릭스";
			this.chk데이터매트릭스.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.chk데이터매트릭스.TextDD = null;
			this.chk데이터매트릭스.UseVisualStyleBackColor = false;
			// 
			// chkQR코드
			// 
			this.chkQR코드.Checked = true;
			this.chkQR코드.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkQR코드.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.chkQR코드.Location = new System.Drawing.Point(12, 12);
			this.chkQR코드.Name = "chkQR코드";
			this.chkQR코드.Size = new System.Drawing.Size(76, 18);
			this.chkQR코드.TabIndex = 20;
			this.chkQR코드.Tag = "";
			this.chkQR코드.Text = "QR코드";
			this.chkQR코드.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.chkQR코드.TextDD = null;
			this.chkQR코드.UseVisualStyleBackColor = false;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.Location = new System.Drawing.Point(301, 1);
			this.label1.Margin = new System.Windows.Forms.Padding(1);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
			this.label1.Size = new System.Drawing.Size(98, 40);
			this.label1.TabIndex = 21;
			this.label1.Text = "스캔구분";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelExt11
			// 
			this.panelExt11.Controls.Add(this.cbo문서종류);
			this.panelExt11.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelExt11.Location = new System.Drawing.Point(101, 1);
			this.panelExt11.Margin = new System.Windows.Forms.Padding(1);
			this.panelExt11.Name = "panelExt11";
			this.panelExt11.Size = new System.Drawing.Size(198, 40);
			this.panelExt11.TabIndex = 20;
			// 
			// cbo문서종류
			// 
			this.cbo문서종류.AutoDropDown = true;
			this.cbo문서종류.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo문서종류.FormattingEnabled = true;
			this.cbo문서종류.ItemHeight = 12;
			this.cbo문서종류.Location = new System.Drawing.Point(10, 10);
			this.cbo문서종류.Name = "cbo문서종류";
			this.cbo문서종류.Size = new System.Drawing.Size(172, 20);
			this.cbo문서종류.TabIndex = 3;
			this.cbo문서종류.Tag = "";
			// 
			// label11
			// 
			this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label11.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label11.Location = new System.Drawing.Point(1, 1);
			this.label11.Margin = new System.Windows.Forms.Padding(1);
			this.label11.Name = "label11";
			this.label11.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
			this.label11.Size = new System.Drawing.Size(98, 40);
			this.label11.TabIndex = 3;
			this.label11.Text = "문서종류";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// spc라인
			// 
			this.spc라인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spc라인.Location = new System.Drawing.Point(0, 51);
			this.spc라인.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.spc라인.Name = "spc라인";
			this.spc라인.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spc라인.Panel1
			// 
			this.spc라인.Panel1.Controls.Add(this.grd라인);
			// 
			// spc라인.Panel2
			// 
			this.spc라인.Panel2.Controls.Add(this.grd최종);
			this.spc라인.Size = new System.Drawing.Size(1088, 911);
			this.spc라인.SplitterDistance = 360;
			this.spc라인.TabIndex = 1;
			// 
			// grd라인
			// 
			this.grd라인.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd라인.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd라인.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd라인.AutoResize = false;
			this.grd라인.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd라인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd라인.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd라인.EnabledHeaderCheck = true;
			this.grd라인.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd라인.Location = new System.Drawing.Point(0, 0);
			this.grd라인.Name = "grd라인";
			this.grd라인.Rows.Count = 1;
			this.grd라인.Rows.DefaultSize = 20;
			this.grd라인.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
			this.grd라인.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd라인.ShowSort = false;
			this.grd라인.Size = new System.Drawing.Size(1088, 360);
			this.grd라인.StyleInfo = resources.GetString("grd라인.StyleInfo");
			this.grd라인.TabIndex = 10;
			this.grd라인.UseGridCalculator = true;
			// 
			// grd최종
			// 
			this.grd최종.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd최종.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd최종.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd최종.AutoResize = false;
			this.grd최종.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd최종.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd최종.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd최종.EnabledHeaderCheck = true;
			this.grd최종.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd최종.Location = new System.Drawing.Point(0, 0);
			this.grd최종.Name = "grd최종";
			this.grd최종.Rows.Count = 1;
			this.grd최종.Rows.DefaultSize = 20;
			this.grd최종.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
			this.grd최종.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd최종.ShowSort = false;
			this.grd최종.Size = new System.Drawing.Size(1088, 547);
			this.grd최종.StyleInfo = resources.GetString("grd최종.StyleInfo");
			this.grd최종.TabIndex = 11;
			this.grd최종.UseGridCalculator = true;
			// 
			// btn업로드_단일문서
			// 
			this.btn업로드_단일문서.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn업로드_단일문서.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(121)))), ((int)(((byte)(197)))));
			this.btn업로드_단일문서.FlatAppearance.BorderSize = 0;
			this.btn업로드_단일문서.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn업로드_단일문서.ForeColor = System.Drawing.Color.White;
			this.btn업로드_단일문서.Location = new System.Drawing.Point(1306, 5);
			this.btn업로드_단일문서.Name = "btn업로드_단일문서";
			this.btn업로드_단일문서.Size = new System.Drawing.Size(110, 25);
			this.btn업로드_단일문서.TabIndex = 133;
			this.btn업로드_단일문서.Text = "업로드(단일문서)";
			this.btn업로드_단일문서.UseVisualStyleBackColor = false;
			// 
			// btn업로드_혼합문서
			// 
			this.btn업로드_혼합문서.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn업로드_혼합문서.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(121)))), ((int)(((byte)(197)))));
			this.btn업로드_혼합문서.FlatAppearance.BorderSize = 0;
			this.btn업로드_혼합문서.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn업로드_혼합문서.ForeColor = System.Drawing.Color.White;
			this.btn업로드_혼합문서.Location = new System.Drawing.Point(1422, 5);
			this.btn업로드_혼합문서.Name = "btn업로드_혼합문서";
			this.btn업로드_혼합문서.Size = new System.Drawing.Size(110, 25);
			this.btn업로드_혼합문서.TabIndex = 134;
			this.btn업로드_혼합문서.Text = "업로드(혼합문서)";
			this.btn업로드_혼합문서.UseVisualStyleBackColor = false;
			// 
			// btn자동업로드
			// 
			this.btn자동업로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn자동업로드.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(121)))), ((int)(((byte)(197)))));
			this.btn자동업로드.FlatAppearance.BorderSize = 0;
			this.btn자동업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn자동업로드.ForeColor = System.Drawing.Color.White;
			this.btn자동업로드.Location = new System.Drawing.Point(799, 5);
			this.btn자동업로드.Name = "btn자동업로드";
			this.btn자동업로드.Size = new System.Drawing.Size(110, 25);
			this.btn자동업로드.TabIndex = 135;
			this.btn자동업로드.Text = "자동업로드";
			this.btn자동업로드.UseVisualStyleBackColor = false;
			// 
			// tbx로그
			// 
			this.tbx로그.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbx로그.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbx로그.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.tbx로그.Location = new System.Drawing.Point(0, 3);
			this.tbx로그.Multiline = true;
			this.tbx로그.Name = "tbx로그";
			this.tbx로그.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbx로그.Size = new System.Drawing.Size(440, 959);
			this.tbx로그.TabIndex = 9;
			// 
			// P_CZ_DX_QR_UPLOAD
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btn자동업로드);
			this.Controls.Add(this.btn업로드_혼합문서);
			this.Controls.Add(this.btn업로드_단일문서);
			this.Name = "P_CZ_DX_QR_UPLOAD";
			this.Size = new System.Drawing.Size(1532, 1005);
			this.TitleText = "P_CZ_DX_QR_UPLOAD";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btn업로드_단일문서, 0);
			this.Controls.SetChildIndex(this.btn업로드_혼합문서, 0);
			this.Controls.SetChildIndex(this.btn자동업로드, 0);
			this.mDataArea.ResumeLayout(false);
			this.spc메인.Panel1.ResumeLayout(false);
			this.spc메인.Panel1.PerformLayout();
			this.spc메인.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spc메인)).EndInit();
			this.spc메인.ResumeLayout(false);
			this.lay메인.ResumeLayout(false);
			this.lay조건.ResumeLayout(false);
			this.panelExt2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp선적일자)).EndInit();
			this.panelExt1.ResumeLayout(false);
			this.panelExt11.ResumeLayout(false);
			this.spc라인.Panel1.ResumeLayout(false);
			this.spc라인.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spc라인)).EndInit();
			this.spc라인.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd라인)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grd최종)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer spc메인;
		private System.Windows.Forms.TableLayoutPanel lay메인;
		private System.Windows.Forms.TableLayoutPanel lay조건;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label1;
		private Duzon.Common.Controls.PanelExt panelExt11;
		private Duzon.Common.Controls.PanelExt panelExt2;
		private System.Windows.Forms.Label label2;
		private Duzon.Common.Controls.PanelExt panelExt1;
		private Duzon.Common.Controls.CheckBoxExt chk바코드;
		private Duzon.Common.Controls.CheckBoxExt chk데이터매트릭스;
		private Duzon.Common.Controls.CheckBoxExt chkQR코드;
		private Duzon.Common.Controls.DatePicker dtp선적일자;
		private Dass.FlexGrid.FlexGrid grd라인;
		private System.Windows.Forms.Button btn업로드_단일문서;
		private System.Windows.Forms.SplitContainer spc라인;
		private Dass.FlexGrid.FlexGrid grd최종;
		private Duzon.Common.Controls.DropDownComboBox cbo문서종류;
		private System.Windows.Forms.Button btn업로드_혼합문서;
		private System.Windows.Forms.Button btn자동업로드;
		private System.Windows.Forms.TextBox tbx로그;
	}
}
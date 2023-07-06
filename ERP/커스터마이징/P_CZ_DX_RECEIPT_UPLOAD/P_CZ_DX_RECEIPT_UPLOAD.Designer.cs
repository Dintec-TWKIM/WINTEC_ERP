
namespace cz
{
	partial class P_CZ_DX_RECEIPT_UPLOAD
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_DX_RECEIPT_UPLOAD));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tbx로그 = new Duzon.Common.Controls.TextBoxExt();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.dtp선적일자 = new Duzon.Common.Controls.DatePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.grd라인 = new Dass.FlexGrid.FlexGrid(this.components);
			this.grd상세 = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn업로드_혼합문서 = new System.Windows.Forms.Button();
			this.btn삭제 = new System.Windows.Forms.Button();
			this.btn업로드_인수증만 = new System.Windows.Forms.Button();
			this.panelExt1 = new Duzon.Common.Controls.PanelExt();
			this.label1 = new System.Windows.Forms.Label();
			this.panelExt2 = new Duzon.Common.Controls.PanelExt();
			this.chkQR = new Duzon.Common.Controls.CheckBoxExt();
			this.chk데이터매트릭스 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk바코드 = new Duzon.Common.Controls.CheckBoxExt();
			this.mDataArea.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.imagePanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp선적일자)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd라인)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grd상세)).BeginInit();
			this.panelExt1.SuspendLayout();
			this.panelExt2.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.splitContainer1);
			this.mDataArea.Size = new System.Drawing.Size(1544, 908);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tbx로그);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Size = new System.Drawing.Size(1544, 908);
			this.splitContainer1.SplitterDistance = 623;
			this.splitContainer1.TabIndex = 0;
			// 
			// tbx로그
			// 
			this.tbx로그.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx로그.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx로그.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbx로그.Location = new System.Drawing.Point(0, 0);
			this.tbx로그.Multiline = true;
			this.tbx로그.Name = "tbx로그";
			this.tbx로그.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbx로그.Size = new System.Drawing.Size(623, 908);
			this.tbx로그.TabIndex = 7;
			this.tbx로그.Tag = "";
			this.tbx로그.UseKeyEnter = false;
			this.tbx로그.UseKeyF3 = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.imagePanel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(917, 908);
			this.tableLayoutPanel1.TabIndex = 9;
			// 
			// imagePanel1
			// 
			this.imagePanel1.BackColor = System.Drawing.Color.White;
			this.imagePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.imagePanel1.Controls.Add(this.panelExt2);
			this.imagePanel1.Controls.Add(this.label1);
			this.imagePanel1.Controls.Add(this.panelExt1);
			this.imagePanel1.Controls.Add(this.label4);
			this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.imagePanel1.LeftImage = null;
			this.imagePanel1.Location = new System.Drawing.Point(3, 3);
			this.imagePanel1.Name = "imagePanel1";
			this.imagePanel1.PatternImage = null;
			this.imagePanel1.RightImage = null;
			this.imagePanel1.Size = new System.Drawing.Size(911, 44);
			this.imagePanel1.TabIndex = 9;
			this.imagePanel1.TitleText = "";
			// 
			// dtp선적일자
			// 
			this.dtp선적일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp선적일자.Location = new System.Drawing.Point(10, 10);
			this.dtp선적일자.Mask = "####/##/##";
			this.dtp선적일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp선적일자.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp선적일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp선적일자.Modified = true;
			this.dtp선적일자.Name = "dtp선적일자";
			this.dtp선적일자.Size = new System.Drawing.Size(90, 21);
			this.dtp선적일자.TabIndex = 130;
			this.dtp선적일자.Tag = "";
			this.dtp선적일자.Value = new System.DateTime(((long)(0)));
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.AliceBlue;
			this.label4.Dock = System.Windows.Forms.DockStyle.Left;
			this.label4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label4.Location = new System.Drawing.Point(0, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(90, 42);
			this.label4.TabIndex = 129;
			this.label4.Text = "선적일자";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(3, 53);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.grd라인);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.grd상세);
			this.splitContainer2.Size = new System.Drawing.Size(911, 852);
			this.splitContainer2.SplitterDistance = 303;
			this.splitContainer2.TabIndex = 10;
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
			this.grd라인.Size = new System.Drawing.Size(911, 303);
			this.grd라인.StyleInfo = resources.GetString("grd라인.StyleInfo");
			this.grd라인.TabIndex = 8;
			this.grd라인.UseGridCalculator = true;
			// 
			// grd상세
			// 
			this.grd상세.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd상세.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd상세.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd상세.AutoResize = false;
			this.grd상세.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd상세.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd상세.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd상세.EnabledHeaderCheck = true;
			this.grd상세.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd상세.Location = new System.Drawing.Point(0, 0);
			this.grd상세.Name = "grd상세";
			this.grd상세.Rows.Count = 1;
			this.grd상세.Rows.DefaultSize = 20;
			this.grd상세.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
			this.grd상세.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd상세.ShowSort = false;
			this.grd상세.Size = new System.Drawing.Size(911, 545);
			this.grd상세.StyleInfo = resources.GetString("grd상세.StyleInfo");
			this.grd상세.TabIndex = 9;
			this.grd상세.UseGridCalculator = true;
			// 
			// btn업로드_혼합문서
			// 
			this.btn업로드_혼합문서.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn업로드_혼합문서.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(121)))), ((int)(((byte)(197)))));
			this.btn업로드_혼합문서.FlatAppearance.BorderSize = 0;
			this.btn업로드_혼합문서.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn업로드_혼합문서.ForeColor = System.Drawing.Color.White;
			this.btn업로드_혼합문서.Location = new System.Drawing.Point(1202, 5);
			this.btn업로드_혼합문서.Name = "btn업로드_혼합문서";
			this.btn업로드_혼합문서.Size = new System.Drawing.Size(110, 25);
			this.btn업로드_혼합문서.TabIndex = 132;
			this.btn업로드_혼합문서.Text = "업로드(혼합문서)";
			this.btn업로드_혼합문서.UseVisualStyleBackColor = false;
			// 
			// btn삭제
			// 
			this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn삭제.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(121)))), ((int)(((byte)(197)))));
			this.btn삭제.FlatAppearance.BorderSize = 0;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.ForeColor = System.Drawing.Color.White;
			this.btn삭제.Location = new System.Drawing.Point(1434, 5);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(110, 25);
			this.btn삭제.TabIndex = 29;
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// btn업로드_인수증만
			// 
			this.btn업로드_인수증만.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn업로드_인수증만.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(121)))), ((int)(((byte)(197)))));
			this.btn업로드_인수증만.FlatAppearance.BorderSize = 0;
			this.btn업로드_인수증만.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn업로드_인수증만.ForeColor = System.Drawing.Color.White;
			this.btn업로드_인수증만.Location = new System.Drawing.Point(1318, 5);
			this.btn업로드_인수증만.Name = "btn업로드_인수증만";
			this.btn업로드_인수증만.Size = new System.Drawing.Size(110, 25);
			this.btn업로드_인수증만.TabIndex = 28;
			this.btn업로드_인수증만.Text = "업로드(인수증만)";
			this.btn업로드_인수증만.UseVisualStyleBackColor = false;
			// 
			// panelExt1
			// 
			this.panelExt1.Controls.Add(this.dtp선적일자);
			this.panelExt1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelExt1.Location = new System.Drawing.Point(90, 0);
			this.panelExt1.Name = "panelExt1";
			this.panelExt1.Size = new System.Drawing.Size(146, 42);
			this.panelExt1.TabIndex = 131;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.AliceBlue;
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.Location = new System.Drawing.Point(236, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 42);
			this.label1.TabIndex = 132;
			this.label1.Text = "구분";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelExt2
			// 
			this.panelExt2.Controls.Add(this.chk바코드);
			this.panelExt2.Controls.Add(this.chk데이터매트릭스);
			this.panelExt2.Controls.Add(this.chkQR);
			this.panelExt2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelExt2.Location = new System.Drawing.Point(326, 0);
			this.panelExt2.Name = "panelExt2";
			this.panelExt2.Size = new System.Drawing.Size(355, 42);
			this.panelExt2.TabIndex = 133;
			// 
			// chkQR
			// 
			this.chkQR.Checked = true;
			this.chkQR.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkQR.Location = new System.Drawing.Point(6, 4);
			this.chkQR.Name = "chkQR";
			this.chkQR.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
			this.chkQR.Size = new System.Drawing.Size(100, 34);
			this.chkQR.TabIndex = 17;
			this.chkQR.Tag = "";
			this.chkQR.Text = "QR코드";
			this.chkQR.TextDD = null;
			this.chkQR.UseVisualStyleBackColor = false;
			// 
			// chk데이터매트릭스
			// 
			this.chk데이터매트릭스.Location = new System.Drawing.Point(88, 4);
			this.chk데이터매트릭스.Name = "chk데이터매트릭스";
			this.chk데이터매트릭스.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
			this.chk데이터매트릭스.Size = new System.Drawing.Size(126, 34);
			this.chk데이터매트릭스.TabIndex = 18;
			this.chk데이터매트릭스.Tag = "";
			this.chk데이터매트릭스.Text = "데이터매트릭스";
			this.chk데이터매트릭스.TextDD = null;
			this.chk데이터매트릭스.UseVisualStyleBackColor = false;
			// 
			// chk바코드
			// 
			this.chk바코드.Location = new System.Drawing.Point(212, 4);
			this.chk바코드.Name = "chk바코드";
			this.chk바코드.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
			this.chk바코드.Size = new System.Drawing.Size(100, 34);
			this.chk바코드.TabIndex = 19;
			this.chk바코드.Tag = "";
			this.chk바코드.Text = "바코드";
			this.chk바코드.TextDD = null;
			this.chk바코드.UseVisualStyleBackColor = false;
			// 
			// P_CZ_DX_RECEIPT_UPLOAD
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btn업로드_혼합문서);
			this.Controls.Add(this.btn삭제);
			this.Controls.Add(this.btn업로드_인수증만);
			this.Name = "P_CZ_DX_RECEIPT_UPLOAD";
			this.Size = new System.Drawing.Size(1544, 948);
			this.TitleText = "P_CZ_DX_RECEIPT_UPLOAD";
			this.Controls.SetChildIndex(this.btn업로드_인수증만, 0);
			this.Controls.SetChildIndex(this.btn삭제, 0);
			this.Controls.SetChildIndex(this.btn업로드_혼합문서, 0);
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.mDataArea.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.imagePanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp선적일자)).EndInit();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd라인)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grd상세)).EndInit();
			this.panelExt1.ResumeLayout(false);
			this.panelExt2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Dass.FlexGrid.FlexGrid grd라인;
		private Duzon.Common.Controls.TextBoxExt tbx로그;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Common.Controls.ImagePanel imagePanel1;
		private System.Windows.Forms.Button btn삭제;
		private System.Windows.Forms.Button btn업로드_인수증만;
		private System.Windows.Forms.Label label4;
		private Duzon.Common.Controls.DatePicker dtp선적일자;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Dass.FlexGrid.FlexGrid grd상세;
		private System.Windows.Forms.Button btn업로드_혼합문서;
		private System.Windows.Forms.Label label1;
		private Duzon.Common.Controls.PanelExt panelExt1;
		private Duzon.Common.Controls.PanelExt panelExt2;
		private Duzon.Common.Controls.CheckBoxExt chk바코드;
		private Duzon.Common.Controls.CheckBoxExt chk데이터매트릭스;
		private Duzon.Common.Controls.CheckBoxExt chkQR;
	}
}
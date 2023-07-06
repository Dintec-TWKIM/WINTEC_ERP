namespace cz
{
	partial class P_CZ_PU_QTN_HGS
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PU_QTN_HGS));
			this.tlayMain = new System.Windows.Forms.TableLayoutPanel();
			this.grd목록 = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn확정 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tlayMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd목록)).BeginInit();
			this.SuspendLayout();
			// 
			// tlayMain
			// 
			this.tlayMain.ColumnCount = 1;
			this.tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlayMain.Controls.Add(this.grd목록, 0, 0);
			this.tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayMain.Location = new System.Drawing.Point(3, 50);
			this.tlayMain.Name = "tlayMain";
			this.tlayMain.RowCount = 1;
			this.tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 497F));
			this.tlayMain.Size = new System.Drawing.Size(1154, 505);
			this.tlayMain.TabIndex = 0;
			// 
			// grd목록
			// 
			this.grd목록.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd목록.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd목록.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd목록.AutoResize = false;
			this.grd목록.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd목록.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd목록.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd목록.EnabledHeaderCheck = true;
			this.grd목록.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd목록.Location = new System.Drawing.Point(3, 3);
			this.grd목록.Name = "grd목록";
			this.grd목록.Rows.Count = 1;
			this.grd목록.Rows.DefaultSize = 18;
			this.grd목록.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd목록.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd목록.ShowSort = false;
			this.grd목록.Size = new System.Drawing.Size(1148, 499);
			this.grd목록.StyleInfo = resources.GetString("grd목록.StyleInfo");
			this.grd목록.TabIndex = 9;
			this.grd목록.UseGridCalculator = true;
			// 
			// btn확정
			// 
			this.btn확정.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn확정.BackColor = System.Drawing.Color.Transparent;
			this.btn확정.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn확정.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn확정.Location = new System.Drawing.Point(1008, 22);
			this.btn확정.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn확정.Name = "btn확정";
			this.btn확정.Size = new System.Drawing.Size(70, 19);
			this.btn확정.TabIndex = 13;
			this.btn확정.TabStop = false;
			this.btn확정.Text = "확정";
			this.btn확정.UseVisualStyleBackColor = false;
			// 
			// btn취소
			// 
			this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn취소.BackColor = System.Drawing.Color.Transparent;
			this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.Location = new System.Drawing.Point(1084, 22);
			this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(70, 19);
			this.btn취소.TabIndex = 12;
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
			this.btn조회.Location = new System.Drawing.Point(932, 22);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 14;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// P_CZ_PU_QTN_HGS
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(1160, 558);
			this.Controls.Add(this.btn조회);
			this.Controls.Add(this.btn확정);
			this.Controls.Add(this.btn취소);
			this.Controls.Add(this.tlayMain);
			this.Name = "P_CZ_PU_QTN_HGS";
			this.Padding = new System.Windows.Forms.Padding(3, 50, 3, 3);
			this.ShowInTaskbar = false;
			this.Text = "P_CZ_PU_QTN_HGS";
			this.TitleText = "파싱 도우미";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tlayMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd목록)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlayMain;
		private Dass.FlexGrid.FlexGrid grd목록;
		private Duzon.Common.Controls.RoundedButton btn확정;
		private Duzon.Common.Controls.RoundedButton btn취소;
		private Duzon.Common.Controls.RoundedButton btn조회;

	}
}
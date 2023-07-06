
namespace cz
{
	partial class H_CZ_DLV_PARTNER_SEL
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_CZ_DLV_PARTNER_SEL));
			this.lay메인 = new System.Windows.Forms.TableLayoutPanel();
			this.grd검색 = new Dass.FlexGrid.FlexGrid();
			this.pnl아이템정보 = new System.Windows.Forms.Panel();
			this.label60 = new System.Windows.Forms.Label();
			this.lay검색 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.panelExt1 = new Duzon.Common.Controls.PanelExt();
			this.tbx검색 = new Duzon.Common.Controls.TextBoxExt();
			this.btn취소 = new System.Windows.Forms.Button();
			this.btn조회 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.lay메인.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd검색)).BeginInit();
			this.pnl아이템정보.SuspendLayout();
			this.lay검색.SuspendLayout();
			this.panelExt1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lay메인
			// 
			this.lay메인.ColumnCount = 1;
			this.lay메인.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay메인.Controls.Add(this.grd검색, 0, 2);
			this.lay메인.Controls.Add(this.pnl아이템정보, 0, 1);
			this.lay메인.Controls.Add(this.lay검색, 0, 0);
			this.lay메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lay메인.Location = new System.Drawing.Point(6, 53);
			this.lay메인.Name = "lay메인";
			this.lay메인.RowCount = 3;
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay메인.Size = new System.Drawing.Size(1034, 572);
			this.lay메인.TabIndex = 0;
			// 
			// grd검색
			// 
			this.grd검색.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd검색.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd검색.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd검색.AutoResize = false;
			this.grd검색.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd검색.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd검색.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd검색.EnabledHeaderCheck = true;
			this.grd검색.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd검색.Location = new System.Drawing.Point(0, 86);
			this.grd검색.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.grd검색.Name = "grd검색";
			this.grd검색.Rows.Count = 1;
			this.grd검색.Rows.DefaultSize = 18;
			this.grd검색.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
			this.grd검색.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd검색.ShowSort = false;
			this.grd검색.Size = new System.Drawing.Size(1034, 483);
			this.grd검색.StyleInfo = resources.GetString("grd검색.StyleInfo");
			this.grd검색.TabIndex = 28;
			this.grd검색.UseGridCalculator = true;
			// 
			// pnl아이템정보
			// 
			this.pnl아이템정보.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.pnl아이템정보.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnl아이템정보.Controls.Add(this.label60);
			this.pnl아이템정보.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl아이템정보.Location = new System.Drawing.Point(0, 44);
			this.pnl아이템정보.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.pnl아이템정보.Name = "pnl아이템정보";
			this.pnl아이템정보.Size = new System.Drawing.Size(1034, 36);
			this.pnl아이템정보.TabIndex = 27;
			// 
			// label60
			// 
			this.label60.Dock = System.Windows.Forms.DockStyle.Left;
			this.label60.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label60.ForeColor = System.Drawing.Color.Blue;
			this.label60.Location = new System.Drawing.Point(0, 0);
			this.label60.Name = "label60";
			this.label60.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
			this.label60.Size = new System.Drawing.Size(80, 34);
			this.label60.TabIndex = 0;
			this.label60.Text = "검색 결과";
			this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lay검색
			// 
			this.lay검색.ColumnCount = 2;
			this.lay검색.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.lay검색.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay검색.Controls.Add(this.label1, 0, 0);
			this.lay검색.Controls.Add(this.panelExt1, 1, 0);
			this.lay검색.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lay검색.Location = new System.Drawing.Point(0, 0);
			this.lay검색.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.lay검색.Name = "lay검색";
			this.lay검색.RowCount = 1;
			this.lay검색.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay검색.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
			this.lay검색.Size = new System.Drawing.Size(1034, 38);
			this.lay검색.TabIndex = 1;
			this.lay검색.Tag = "VIEWBOX";
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.label1.Location = new System.Drawing.Point(1, 1);
			this.label1.Margin = new System.Windows.Forms.Padding(1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 36);
			this.label1.TabIndex = 17;
			this.label1.Text = "검색";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panelExt1
			// 
			this.panelExt1.Controls.Add(this.tbx검색);
			this.panelExt1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelExt1.Location = new System.Drawing.Point(101, 1);
			this.panelExt1.Margin = new System.Windows.Forms.Padding(1);
			this.panelExt1.Name = "panelExt1";
			this.panelExt1.Size = new System.Drawing.Size(370, 36);
			this.panelExt1.TabIndex = 18;
			// 
			// tbx검색
			// 
			this.tbx검색.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx검색.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx검색.Location = new System.Drawing.Point(8, 8);
			this.tbx검색.Name = "tbx검색";
			this.tbx검색.Size = new System.Drawing.Size(250, 21);
			this.tbx검색.TabIndex = 16;
			this.tbx검색.Tag = "";
			// 
			// btn취소
			// 
			this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn취소.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(121)))), ((int)(((byte)(197)))));
			this.btn취소.FlatAppearance.BorderSize = 0;
			this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn취소.ForeColor = System.Drawing.Color.White;
			this.btn취소.Location = new System.Drawing.Point(960, 10);
			this.btn취소.Name = "btn취소";
			this.btn취소.Size = new System.Drawing.Size(70, 26);
			this.btn취소.TabIndex = 22;
			this.btn취소.Text = "취소";
			this.btn취소.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn조회.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(121)))), ((int)(((byte)(197)))));
			this.btn조회.FlatAppearance.BorderSize = 0;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.ForeColor = System.Drawing.Color.White;
			this.btn조회.Location = new System.Drawing.Point(882, 10);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 26);
			this.btn조회.TabIndex = 21;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// H_CZ_DLV_PARTNER_SEL
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(1046, 631);
			this.Controls.Add(this.btn취소);
			this.Controls.Add(this.btn조회);
			this.Controls.Add(this.lay메인);
			this.Name = "H_CZ_DLV_PARTNER_SEL";
			this.Padding = new System.Windows.Forms.Padding(6, 53, 6, 6);
			this.Text = "H_CZ_DLV_PARTNER_SEL";
			this.TitleText = "납품처 선택";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.lay메인.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd검색)).EndInit();
			this.pnl아이템정보.ResumeLayout(false);
			this.lay검색.ResumeLayout(false);
			this.panelExt1.ResumeLayout(false);
			this.panelExt1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel lay메인;
		private System.Windows.Forms.TableLayoutPanel lay검색;
		private System.Windows.Forms.Label label1;
		private Duzon.Common.Controls.PanelExt panelExt1;
		private Duzon.Common.Controls.TextBoxExt tbx검색;
		private System.Windows.Forms.Panel pnl아이템정보;
		private System.Windows.Forms.Label label60;
		private Dass.FlexGrid.FlexGrid grd검색;
		private System.Windows.Forms.Button btn취소;
		private System.Windows.Forms.Button btn조회;
	}
}
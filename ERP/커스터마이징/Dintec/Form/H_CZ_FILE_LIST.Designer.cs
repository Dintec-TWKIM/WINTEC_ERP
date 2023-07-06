namespace Dintec
{
	partial class H_CZ_FILE_LIST
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_CZ_FILE_LIST));
			this.lay메인 = new System.Windows.Forms.TableLayoutPanel();
			this.grd목록 = new Dass.FlexGrid.FlexGrid(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.btn발송용저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.lay메인.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd목록)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lay메인
			// 
			this.lay메인.ColumnCount = 1;
			this.lay메인.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay메인.Controls.Add(this.grd목록, 0, 0);
			this.lay메인.Controls.Add(this.panel1, 0, 1);
			this.lay메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lay메인.Location = new System.Drawing.Point(10, 10);
			this.lay메인.Name = "lay메인";
			this.lay메인.RowCount = 2;
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.lay메인.Size = new System.Drawing.Size(874, 418);
			this.lay메인.TabIndex = 6;
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
			this.grd목록.ExtendLastCol = true;
			this.grd목록.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd목록.Location = new System.Drawing.Point(3, 3);
			this.grd목록.Name = "grd목록";
			this.grd목록.Rows.Count = 1;
			this.grd목록.Rows.DefaultSize = 18;
			this.grd목록.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd목록.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd목록.ShowSort = false;
			this.grd목록.Size = new System.Drawing.Size(868, 382);
			this.grd목록.StyleInfo = resources.GetString("grd목록.StyleInfo");
			this.grd목록.TabIndex = 5;
			this.grd목록.UseGridCalculator = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btn발송용저장);
			this.panel1.Controls.Add(this.labelExt1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 391);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.panel1.Size = new System.Drawing.Size(868, 24);
			this.panel1.TabIndex = 6;
			// 
			// btn발송용저장
			// 
			this.btn발송용저장.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn발송용저장.BackColor = System.Drawing.Color.White;
			this.btn발송용저장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn발송용저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn발송용저장.Location = new System.Drawing.Point(786, 1);
			this.btn발송용저장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn발송용저장.Name = "btn발송용저장";
			this.btn발송용저장.Size = new System.Drawing.Size(80, 19);
			this.btn발송용저장.TabIndex = 14;
			this.btn발송용저장.TabStop = false;
			this.btn발송용저장.Text = "발송용저장";
			this.btn발송용저장.UseVisualStyleBackColor = false;
			// 
			// labelExt1
			// 
			this.labelExt1.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelExt1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.labelExt1.ForeColor = System.Drawing.Color.Black;
			this.labelExt1.Location = new System.Drawing.Point(0, 3);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(261, 21);
			this.labelExt1.TabIndex = 7;
			this.labelExt1.Text = "※ 개별 파일 더블클릭 시 실행됩니다.";
			// 
			// H_CZ_FILE_LIST
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(894, 438);
			this.Controls.Add(this.lay메인);
			this.Name = "H_CZ_FILE_LIST";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.Text = "H_CZ_FILE_LIST";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.lay메인.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd목록)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel lay메인;
		private Dass.FlexGrid.FlexGrid grd목록;
		private System.Windows.Forms.Panel panel1;
		private Duzon.Common.Controls.RoundedButton btn발송용저장;
		private Duzon.Common.Controls.LabelExt labelExt1;
	}
}
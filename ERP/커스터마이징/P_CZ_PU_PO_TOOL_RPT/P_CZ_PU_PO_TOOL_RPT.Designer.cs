namespace cz
{
	partial class P_CZ_PU_PO_TOOL_RPT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PU_PO_TOOL_RPT));
			this.lay메인 = new System.Windows.Forms.TableLayoutPanel();
			this.one메인 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.grd헤드 = new Dass.FlexGrid.FlexGrid(this.components);
			this.mDataArea.SuspendLayout();
			this.lay메인.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grd헤드)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.lay메인);
			this.mDataArea.Size = new System.Drawing.Size(1356, 849);
			// 
			// lay메인
			// 
			this.lay메인.ColumnCount = 1;
			this.lay메인.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay메인.Controls.Add(this.grd헤드, 0, 1);
			this.lay메인.Controls.Add(this.one메인, 0, 0);
			this.lay메인.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lay메인.Location = new System.Drawing.Point(0, 0);
			this.lay메인.Name = "lay메인";
			this.lay메인.RowCount = 2;
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.lay메인.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.lay메인.Size = new System.Drawing.Size(1356, 849);
			this.lay메인.TabIndex = 0;
			// 
			// one메인
			// 
			this.one메인.Dock = System.Windows.Forms.DockStyle.Top;
			this.one메인.Location = new System.Drawing.Point(3, 3);
			this.one메인.Name = "one메인";
			this.one메인.Size = new System.Drawing.Size(1350, 100);
			this.one메인.TabIndex = 0;
			// 
			// grd헤드
			// 
			this.grd헤드.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.grd헤드.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.grd헤드.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.grd헤드.AutoResize = false;
			this.grd헤드.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.grd헤드.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grd헤드.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.grd헤드.EnabledHeaderCheck = true;
			this.grd헤드.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.grd헤드.Location = new System.Drawing.Point(3, 109);
			this.grd헤드.Name = "grd헤드";
			this.grd헤드.Rows.Count = 1;
			this.grd헤드.Rows.DefaultSize = 20;
			this.grd헤드.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.grd헤드.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.grd헤드.ShowSort = false;
			this.grd헤드.Size = new System.Drawing.Size(1350, 737);
			this.grd헤드.StyleInfo = resources.GetString("grd헤드.StyleInfo");
			this.grd헤드.TabIndex = 5;
			this.grd헤드.UseGridCalculator = true;
			// 
			// P_CZ_PU_PO_TOOL_RPT
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "P_CZ_PU_PO_TOOL_RPT";
			this.Size = new System.Drawing.Size(1356, 889);
			this.TitleText = "P_CZ_PU_PO_TOOL_RPT";
			this.mDataArea.ResumeLayout(false);
			this.lay메인.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grd헤드)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel lay메인;
		private Duzon.Erpiu.Windows.OneControls.OneGrid one메인;
		private Dass.FlexGrid.FlexGrid grd헤드;
	}
}
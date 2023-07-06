namespace cz
{
	partial class P_CZ_SA_DN_PR_SEARCH
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_DN_PR_SEARCH));
			this.tlayM = new System.Windows.Forms.TableLayoutPanel();
			this.pnl제목 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneS = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.mDataArea.SuspendLayout();
			this.tlayM.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.flex)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tlayM);
			// 
			// tlayM
			// 
			this.tlayM.ColumnCount = 1;
			this.tlayM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayM.Controls.Add(this.pnl제목, 0, 0);
			this.tlayM.Controls.Add(this.imagePanel1, 0, 2);
			this.tlayM.Controls.Add(this.flex, 0, 3);
			this.tlayM.Controls.Add(this.oneS, 0, 1);
			this.tlayM.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayM.Location = new System.Drawing.Point(0, 0);
			this.tlayM.Name = "tlayM";
			this.tlayM.RowCount = 4;
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayM.Size = new System.Drawing.Size(827, 579);
			this.tlayM.TabIndex = 0;
			// 
			// pnl제목
			// 
			this.pnl제목.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.pnl제목.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl제목.LeftImage = null;
			this.pnl제목.Location = new System.Drawing.Point(3, 3);
			this.pnl제목.Name = "pnl제목";
			this.pnl제목.Padding = new System.Windows.Forms.Padding(1);
			this.pnl제목.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.pnl제목.PatternImage = null;
			this.pnl제목.RightImage = null;
			this.pnl제목.Size = new System.Drawing.Size(821, 27);
			this.pnl제목.TabIndex = 8;
			this.pnl제목.TitleText = "검색조건";
			// 
			// imagePanel1
			// 
			this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.imagePanel1.LeftImage = null;
			this.imagePanel1.Location = new System.Drawing.Point(3, 110);
			this.imagePanel1.Name = "imagePanel1";
			this.imagePanel1.Padding = new System.Windows.Forms.Padding(1);
			this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.imagePanel1.PatternImage = null;
			this.imagePanel1.RightImage = null;
			this.imagePanel1.Size = new System.Drawing.Size(821, 27);
			this.imagePanel1.TabIndex = 9;
			this.imagePanel1.TitleText = "수주번호";
			// 
			// flex
			// 
			this.flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.flex.AutoResize = false;
			this.flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.flex.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.flex.EnabledHeaderCheck = true;
			this.flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.flex.Location = new System.Drawing.Point(3, 143);
			this.flex.Name = "flex";
			this.flex.Rows.Count = 1;
			this.flex.Rows.DefaultSize = 20;
			this.flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.CellRange;
			this.flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.flex.ShowSort = false;
			this.flex.Size = new System.Drawing.Size(821, 433);
			this.flex.StyleInfo = resources.GetString("flex.StyleInfo");
			this.flex.TabIndex = 10;
			this.flex.UseGridCalculator = true;
			// 
			// oneS
			// 
			this.oneS.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneS.Location = new System.Drawing.Point(3, 36);
			this.oneS.Name = "oneS";
			this.oneS.Size = new System.Drawing.Size(821, 68);
			this.oneS.TabIndex = 11;
			// 
			// P_CZ_SA_DN_PR_SEARCH
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "P_CZ_SA_DN_PR_SEARCH";
			this.TitleText = "P_CZ_SA_DN_PR_SEARCH";
			this.mDataArea.ResumeLayout(false);
			this.tlayM.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.flex)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlayM;
		private Duzon.Common.Controls.ImagePanel pnl제목;
		private Duzon.Common.Controls.ImagePanel imagePanel1;
		private Dass.FlexGrid.FlexGrid flex;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneS;

	}
}
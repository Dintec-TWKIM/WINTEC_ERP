namespace cz
{
	partial class P_CZ_PR_BOM_IO_REG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_BOM_IO_REG));
			this.tlayM = new System.Windows.Forms.TableLayoutPanel();
			this.pnl1 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.txt포커스 = new Duzon.Common.Controls.TextBoxExt();
			this.oneS = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.pnl2 = new Duzon.Common.Controls.ImagePanel(this.components);
			this.tlayS = new System.Windows.Forms.TableLayoutPanel();
			this.flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this.flexL = new Dass.FlexGrid.FlexGrid(this.components);
			this.mDataArea.SuspendLayout();
			this.tlayM.SuspendLayout();
			this.pnl1.SuspendLayout();
			this.tlayS.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.flexH)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.flexL)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tlayM);
			this.mDataArea.Size = new System.Drawing.Size(1178, 579);
			// 
			// tlayM
			// 
			this.tlayM.ColumnCount = 1;
			this.tlayM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayM.Controls.Add(this.pnl1, 0, 0);
			this.tlayM.Controls.Add(this.oneS, 0, 1);
			this.tlayM.Controls.Add(this.pnl2, 0, 2);
			this.tlayM.Controls.Add(this.tlayS, 0, 3);
			this.tlayM.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayM.Location = new System.Drawing.Point(0, 0);
			this.tlayM.Name = "tlayM";
			this.tlayM.RowCount = 4;
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlayM.Size = new System.Drawing.Size(1178, 579);
			this.tlayM.TabIndex = 3;
			// 
			// pnl1
			// 
			this.pnl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.pnl1.Controls.Add(this.txt포커스);
			this.pnl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl1.LeftImage = null;
			this.pnl1.Location = new System.Drawing.Point(3, 3);
			this.pnl1.Name = "pnl1";
			this.pnl1.Padding = new System.Windows.Forms.Padding(1);
			this.pnl1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.pnl1.PatternImage = null;
			this.pnl1.RightImage = null;
			this.pnl1.Size = new System.Drawing.Size(1172, 27);
			this.pnl1.TabIndex = 7;
			this.pnl1.TitleText = "매출처 정보";
			// 
			// txt포커스
			// 
			this.txt포커스.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt포커스.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt포커스.Location = new System.Drawing.Point(171, 4);
			this.txt포커스.Name = "txt포커스";
			this.txt포커스.SelectedAllEnabled = false;
			this.txt포커스.Size = new System.Drawing.Size(100, 21);
			this.txt포커스.TabIndex = 7;
			this.txt포커스.UseKeyEnter = true;
			this.txt포커스.UseKeyF3 = true;
			// 
			// oneS
			// 
			this.oneS.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneS.Location = new System.Drawing.Point(3, 36);
			this.oneS.Name = "oneS";
			this.oneS.Size = new System.Drawing.Size(1172, 49);
			this.oneS.TabIndex = 0;
			// 
			// pnl2
			// 
			this.pnl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
			this.pnl2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnl2.LeftImage = null;
			this.pnl2.Location = new System.Drawing.Point(3, 91);
			this.pnl2.Name = "pnl2";
			this.pnl2.Padding = new System.Windows.Forms.Padding(1);
			this.pnl2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
			this.pnl2.PatternImage = null;
			this.pnl2.RightImage = null;
			this.pnl2.Size = new System.Drawing.Size(1172, 27);
			this.pnl2.TabIndex = 9;
			this.pnl2.TitleText = "아이템 정보";
			// 
			// tlayS
			// 
			this.tlayS.ColumnCount = 2;
			this.tlayS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 920F));
			this.tlayS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayS.Controls.Add(this.flexH, 0, 0);
			this.tlayS.Controls.Add(this.flexL, 1, 0);
			this.tlayS.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayS.Location = new System.Drawing.Point(3, 124);
			this.tlayS.Name = "tlayS";
			this.tlayS.RowCount = 1;
			this.tlayS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayS.Size = new System.Drawing.Size(1172, 452);
			this.tlayS.TabIndex = 11;
			// 
			// flexH
			// 
			this.flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.flexH.AutoResize = false;
			this.flexH.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.flexH.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.flexH.EnabledHeaderCheck = true;
			this.flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.flexH.Location = new System.Drawing.Point(3, 3);
			this.flexH.Name = "flexH";
			this.flexH.Rows.Count = 1;
			this.flexH.Rows.DefaultSize = 20;
			this.flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.flexH.ShowSort = false;
			this.flexH.Size = new System.Drawing.Size(914, 446);
			this.flexH.StyleInfo = resources.GetString("flexH.StyleInfo");
			this.flexH.TabIndex = 11;
			this.flexH.UseGridCalculator = true;
			// 
			// flexL
			// 
			this.flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this.flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this.flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this.flexL.AutoResize = false;
			this.flexL.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this.flexL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this.flexL.EnabledHeaderCheck = true;
			this.flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this.flexL.Location = new System.Drawing.Point(923, 3);
			this.flexL.Name = "flexL";
			this.flexL.Rows.Count = 1;
			this.flexL.Rows.DefaultSize = 20;
			this.flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this.flexL.ShowSort = false;
			this.flexL.Size = new System.Drawing.Size(246, 446);
			this.flexL.StyleInfo = resources.GetString("flexL.StyleInfo");
			this.flexL.TabIndex = 10;
			this.flexL.UseGridCalculator = true;
			// 
			// P_CZ_PR_BOM_IO_REG
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_PR_BOM_IO_REG";
			this.Size = new System.Drawing.Size(1178, 619);
			this.TitleText = "BOM 입출고 처리";
			this.mDataArea.ResumeLayout(false);
			this.tlayM.ResumeLayout(false);
			this.pnl1.ResumeLayout(false);
			this.pnl1.PerformLayout();
			this.tlayS.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.flexH)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.flexL)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlayM;
		private Duzon.Common.Controls.ImagePanel pnl1;
		private Duzon.Common.Controls.TextBoxExt txt포커스;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneS;
		private Duzon.Common.Controls.ImagePanel pnl2;
		private System.Windows.Forms.TableLayoutPanel tlayS;
		private Dass.FlexGrid.FlexGrid flexL;
		private Dass.FlexGrid.FlexGrid flexH;
	}
}

namespace cz
{
	partial class P_CZ_PR_ID_NO_HISTORY
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_ID_NO_HISTORY));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this._flex공정 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpg측정치 = new System.Windows.Forms.TabPage();
			this._flex측정치 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg실적 = new System.Windows.Forms.TabPage();
			this._flex실적 = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.txtID번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lblID번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt작업지시번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl작업지시번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.txtID번호이전 = new Duzon.Common.Controls.TextBoxExt();
			this.lblID번호이전 = new Duzon.Common.Controls.LabelExt();
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex공정)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tpg측정치.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex측정치)).BeginInit();
			this.tpg실적.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex실적)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1324, 694);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1324, 694);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 50);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._flexH);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(1318, 641);
			this.splitContainer1.SplitterDistance = 375;
			this.splitContainer1.TabIndex = 0;
			// 
			// _flexH
			// 
			this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexH.AutoResize = false;
			this._flexH.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexH.EnabledHeaderCheck = true;
			this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexH.Location = new System.Drawing.Point(0, 0);
			this._flexH.Name = "_flexH";
			this._flexH.Rows.Count = 1;
			this._flexH.Rows.DefaultSize = 20;
			this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexH.ShowSort = false;
			this._flexH.Size = new System.Drawing.Size(375, 641);
			this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
			this._flexH.TabIndex = 0;
			this._flexH.UseGridCalculator = true;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this._flex공정);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer2.Size = new System.Drawing.Size(939, 641);
			this.splitContainer2.SplitterDistance = 313;
			this.splitContainer2.TabIndex = 0;
			// 
			// _flex공정
			// 
			this._flex공정.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex공정.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex공정.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex공정.AutoResize = false;
			this._flex공정.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex공정.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex공정.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex공정.EnabledHeaderCheck = true;
			this._flex공정.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex공정.Location = new System.Drawing.Point(0, 0);
			this._flex공정.Name = "_flex공정";
			this._flex공정.Rows.Count = 1;
			this._flex공정.Rows.DefaultSize = 20;
			this._flex공정.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex공정.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex공정.ShowSort = false;
			this._flex공정.Size = new System.Drawing.Size(939, 313);
			this._flex공정.StyleInfo = resources.GetString("_flex공정.StyleInfo");
			this._flex공정.TabIndex = 0;
			this._flex공정.UseGridCalculator = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tpg측정치);
			this.tabControl1.Controls.Add(this.tpg실적);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(939, 324);
			this.tabControl1.TabIndex = 0;
			// 
			// tpg측정치
			// 
			this.tpg측정치.Controls.Add(this._flex측정치);
			this.tpg측정치.Location = new System.Drawing.Point(4, 22);
			this.tpg측정치.Name = "tpg측정치";
			this.tpg측정치.Padding = new System.Windows.Forms.Padding(3);
			this.tpg측정치.Size = new System.Drawing.Size(931, 298);
			this.tpg측정치.TabIndex = 0;
			this.tpg측정치.Text = "측정치";
			this.tpg측정치.UseVisualStyleBackColor = true;
			// 
			// _flex측정치
			// 
			this._flex측정치.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex측정치.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex측정치.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex측정치.AutoResize = false;
			this._flex측정치.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex측정치.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex측정치.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex측정치.EnabledHeaderCheck = true;
			this._flex측정치.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex측정치.Location = new System.Drawing.Point(3, 3);
			this._flex측정치.Name = "_flex측정치";
			this._flex측정치.Rows.Count = 1;
			this._flex측정치.Rows.DefaultSize = 20;
			this._flex측정치.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex측정치.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex측정치.ShowSort = false;
			this._flex측정치.Size = new System.Drawing.Size(925, 292);
			this._flex측정치.StyleInfo = resources.GetString("_flex측정치.StyleInfo");
			this._flex측정치.TabIndex = 0;
			this._flex측정치.UseGridCalculator = true;
			// 
			// tpg실적
			// 
			this.tpg실적.Controls.Add(this._flex실적);
			this.tpg실적.Location = new System.Drawing.Point(4, 22);
			this.tpg실적.Name = "tpg실적";
			this.tpg실적.Padding = new System.Windows.Forms.Padding(3);
			this.tpg실적.Size = new System.Drawing.Size(931, 298);
			this.tpg실적.TabIndex = 1;
			this.tpg실적.Text = "실적";
			this.tpg실적.UseVisualStyleBackColor = true;
			// 
			// _flex실적
			// 
			this._flex실적.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex실적.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex실적.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex실적.AutoResize = false;
			this._flex실적.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex실적.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex실적.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex실적.EnabledHeaderCheck = true;
			this._flex실적.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex실적.Location = new System.Drawing.Point(3, 3);
			this._flex실적.Name = "_flex실적";
			this._flex실적.Rows.Count = 1;
			this._flex실적.Rows.DefaultSize = 20;
			this._flex실적.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex실적.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex실적.ShowSort = false;
			this._flex실적.Size = new System.Drawing.Size(925, 292);
			this._flex실적.StyleInfo = resources.GetString("_flex실적.StyleInfo");
			this._flex실적.TabIndex = 0;
			this._flex실적.UseGridCalculator = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1318, 41);
			this.oneGrid1.TabIndex = 1;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1308, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.txtID번호);
			this.bpPanelControl1.Controls.Add(this.lblID번호);
			this.bpPanelControl1.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// txtID번호
			// 
			this.txtID번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtID번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtID번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txtID번호.Location = new System.Drawing.Point(106, 0);
			this.txtID번호.Name = "txtID번호";
			this.txtID번호.Size = new System.Drawing.Size(186, 21);
			this.txtID번호.TabIndex = 1;
			// 
			// lblID번호
			// 
			this.lblID번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblID번호.Location = new System.Drawing.Point(0, 0);
			this.lblID번호.Name = "lblID번호";
			this.lblID번호.Size = new System.Drawing.Size(100, 23);
			this.lblID번호.TabIndex = 0;
			this.lblID번호.Text = "ID번호";
			this.lblID번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.txt작업지시번호);
			this.bpPanelControl2.Controls.Add(this.lbl작업지시번호);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
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
			this.bpPanelControl3.Controls.Add(this.txtID번호이전);
			this.bpPanelControl3.Controls.Add(this.lblID번호이전);
			this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// txtID번호이전
			// 
			this.txtID번호이전.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtID번호이전.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtID번호이전.Dock = System.Windows.Forms.DockStyle.Right;
			this.txtID번호이전.Location = new System.Drawing.Point(106, 0);
			this.txtID번호이전.Name = "txtID번호이전";
			this.txtID번호이전.Size = new System.Drawing.Size(186, 21);
			this.txtID번호이전.TabIndex = 1;
			// 
			// lblID번호이전
			// 
			this.lblID번호이전.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblID번호이전.Location = new System.Drawing.Point(0, 0);
			this.lblID번호이전.Name = "lblID번호이전";
			this.lblID번호이전.Size = new System.Drawing.Size(100, 23);
			this.lblID번호이전.TabIndex = 0;
			this.lblID번호이전.Text = "ID번호(이전)";
			this.lblID번호이전.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// P_CZ_PR_ID_NO_HISTORY
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_PR_ID_NO_HISTORY";
			this.Size = new System.Drawing.Size(1324, 734);
			this.TitleText = "P_CZ_PR_ID_NO_HISTORY";
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex공정)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tpg측정치.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex측정치)).EndInit();
			this.tpg실적.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex실적)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl2.PerformLayout();
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Dass.FlexGrid.FlexGrid _flexH;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.TextBoxExt txtID번호;
		private Duzon.Common.Controls.LabelExt lblID번호;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Dass.FlexGrid.FlexGrid _flex공정;
		private Dass.FlexGrid.FlexGrid _flex측정치;
		private Dass.FlexGrid.FlexGrid _flex실적;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpg측정치;
		private System.Windows.Forms.TabPage tpg실적;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.TextBoxExt txt작업지시번호;
		private Duzon.Common.Controls.LabelExt lbl작업지시번호;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.Controls.TextBoxExt txtID번호이전;
		private Duzon.Common.Controls.LabelExt lblID번호이전;
	}
}
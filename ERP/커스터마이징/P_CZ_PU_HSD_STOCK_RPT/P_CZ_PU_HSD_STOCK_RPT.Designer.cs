
namespace cz
{
	partial class P_CZ_PU_HSD_STOCK_RPT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PU_HSD_STOCK_RPT));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.chk입력된건제외 = new Duzon.Common.Controls.CheckBoxExt();
			this.chk판매처확인대상 = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo취소여부 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl취소여부 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp수주일자 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl수주일자 = new Duzon.Common.Controls.LabelExt();
			this.btn판매데이터업로드 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn자동등록 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.tabControlExt1 = new Duzon.Common.Controls.TabControlExt();
			this.tpgERP기준 = new System.Windows.Forms.TabPage();
			this.tpgHSD등록 = new System.Windows.Forms.TabPage();
			this._flexERP기준 = new Dass.FlexGrid.FlexGrid(this.components);
			this._flexHSD등록 = new Dass.FlexGrid.FlexGrid(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.tabControlExt1.SuspendLayout();
			this.tpgERP기준.SuspendLayout();
			this.tpgHSD등록.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexERP기준)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flexHSD등록)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel2);
			this.mDataArea.Size = new System.Drawing.Size(1220, 661);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 40);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.65558F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.34442F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1220, 661);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.tabControlExt1, 0, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1220, 661);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1214, 40);
			this.oneGrid1.TabIndex = 1;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.chk입력된건제외);
			this.oneGridItem1.Controls.Add(this.chk판매처확인대상);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1204, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// chk입력된건제외
			// 
			this.chk입력된건제외.AutoSize = true;
			this.chk입력된건제외.Location = new System.Drawing.Point(700, 1);
			this.chk입력된건제외.Name = "chk입력된건제외";
			this.chk입력된건제외.Size = new System.Drawing.Size(108, 16);
			this.chk입력된건제외.TabIndex = 3;
			this.chk입력된건제외.Text = "입력된건제외";
			this.chk입력된건제외.TextDD = null;
			this.chk입력된건제외.UseVisualStyleBackColor = true;
			// 
			// chk판매처확인대상
			// 
			this.chk판매처확인대상.AutoSize = true;
			this.chk판매처확인대상.Location = new System.Drawing.Point(590, 1);
			this.chk판매처확인대상.Name = "chk판매처확인대상";
			this.chk판매처확인대상.Size = new System.Drawing.Size(108, 16);
			this.chk판매처확인대상.TabIndex = 2;
			this.chk판매처확인대상.Text = "판매처확인대상";
			this.chk판매처확인대상.TextDD = null;
			this.chk판매처확인대상.UseVisualStyleBackColor = true;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.cbo취소여부);
			this.bpPanelControl2.Controls.Add(this.lbl취소여부);
			this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// cbo취소여부
			// 
			this.cbo취소여부.AutoDropDown = true;
			this.cbo취소여부.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo취소여부.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo취소여부.FormattingEnabled = true;
			this.cbo취소여부.ItemHeight = 12;
			this.cbo취소여부.Location = new System.Drawing.Point(106, 0);
			this.cbo취소여부.Name = "cbo취소여부";
			this.cbo취소여부.Size = new System.Drawing.Size(186, 20);
			this.cbo취소여부.TabIndex = 1;
			// 
			// lbl취소여부
			// 
			this.lbl취소여부.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl취소여부.Location = new System.Drawing.Point(0, 0);
			this.lbl취소여부.Name = "lbl취소여부";
			this.lbl취소여부.Size = new System.Drawing.Size(100, 23);
			this.lbl취소여부.TabIndex = 0;
			this.lbl취소여부.Text = "취소여부";
			this.lbl취소여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.dtp수주일자);
			this.bpPanelControl1.Controls.Add(this.lbl수주일자);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// dtp수주일자
			// 
			this.dtp수주일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp수주일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp수주일자.IsNecessaryCondition = true;
			this.dtp수주일자.Location = new System.Drawing.Point(107, 0);
			this.dtp수주일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp수주일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp수주일자.Name = "dtp수주일자";
			this.dtp수주일자.Size = new System.Drawing.Size(185, 21);
			this.dtp수주일자.TabIndex = 1;
			// 
			// lbl수주일자
			// 
			this.lbl수주일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl수주일자.Location = new System.Drawing.Point(0, 0);
			this.lbl수주일자.Name = "lbl수주일자";
			this.lbl수주일자.Size = new System.Drawing.Size(100, 23);
			this.lbl수주일자.TabIndex = 0;
			this.lbl수주일자.Text = "수주일자";
			this.lbl수주일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btn판매데이터업로드
			// 
			this.btn판매데이터업로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn판매데이터업로드.BackColor = System.Drawing.Color.Transparent;
			this.btn판매데이터업로드.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn판매데이터업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn판매데이터업로드.Location = new System.Drawing.Point(1002, 8);
			this.btn판매데이터업로드.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn판매데이터업로드.Name = "btn판매데이터업로드";
			this.btn판매데이터업로드.Size = new System.Drawing.Size(122, 19);
			this.btn판매데이터업로드.TabIndex = 3;
			this.btn판매데이터업로드.TabStop = false;
			this.btn판매데이터업로드.Text = "판매데이터업로드";
			this.btn판매데이터업로드.UseVisualStyleBackColor = false;
			// 
			// btn자동등록
			// 
			this.btn자동등록.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn자동등록.BackColor = System.Drawing.Color.Transparent;
			this.btn자동등록.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn자동등록.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn자동등록.Location = new System.Drawing.Point(1129, 8);
			this.btn자동등록.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn자동등록.Name = "btn자동등록";
			this.btn자동등록.Size = new System.Drawing.Size(86, 19);
			this.btn자동등록.TabIndex = 4;
			this.btn자동등록.TabStop = false;
			this.btn자동등록.Text = "자동등록";
			this.btn자동등록.UseVisualStyleBackColor = false;
			// 
			// tabControlExt1
			// 
			this.tabControlExt1.Controls.Add(this.tpgERP기준);
			this.tabControlExt1.Controls.Add(this.tpgHSD등록);
			this.tabControlExt1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlExt1.Location = new System.Drawing.Point(3, 49);
			this.tabControlExt1.Name = "tabControlExt1";
			this.tabControlExt1.SelectedIndex = 0;
			this.tabControlExt1.Size = new System.Drawing.Size(1214, 609);
			this.tabControlExt1.TabIndex = 2;
			// 
			// tpgERP기준
			// 
			this.tpgERP기준.Controls.Add(this._flexERP기준);
			this.tpgERP기준.Location = new System.Drawing.Point(4, 22);
			this.tpgERP기준.Name = "tpgERP기준";
			this.tpgERP기준.Padding = new System.Windows.Forms.Padding(3);
			this.tpgERP기준.Size = new System.Drawing.Size(1206, 583);
			this.tpgERP기준.TabIndex = 0;
			this.tpgERP기준.Text = "ERP기준";
			this.tpgERP기준.UseVisualStyleBackColor = true;
			// 
			// tpgHSD등록
			// 
			this.tpgHSD등록.Controls.Add(this._flexHSD등록);
			this.tpgHSD등록.Location = new System.Drawing.Point(4, 22);
			this.tpgHSD등록.Name = "tpgHSD등록";
			this.tpgHSD등록.Padding = new System.Windows.Forms.Padding(3);
			this.tpgHSD등록.Size = new System.Drawing.Size(1206, 583);
			this.tpgHSD등록.TabIndex = 1;
			this.tpgHSD등록.Text = "HSD등록";
			this.tpgHSD등록.UseVisualStyleBackColor = true;
			// 
			// _flexERP기준
			// 
			this._flexERP기준.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexERP기준.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexERP기준.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexERP기준.AutoResize = false;
			this._flexERP기준.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexERP기준.Cursor = System.Windows.Forms.Cursors.Default;
			this._flexERP기준.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexERP기준.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexERP기준.EnabledHeaderCheck = true;
			this._flexERP기준.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexERP기준.Location = new System.Drawing.Point(3, 3);
			this._flexERP기준.Name = "_flexERP기준";
			this._flexERP기준.Rows.Count = 1;
			this._flexERP기준.Rows.DefaultSize = 20;
			this._flexERP기준.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexERP기준.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexERP기준.ShowSort = false;
			this._flexERP기준.Size = new System.Drawing.Size(1200, 577);
			this._flexERP기준.StyleInfo = resources.GetString("_flexERP기준.StyleInfo");
			this._flexERP기준.TabIndex = 1;
			this._flexERP기준.UseGridCalculator = true;
			// 
			// _flexHSD등록
			// 
			this._flexHSD등록.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexHSD등록.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexHSD등록.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexHSD등록.AutoResize = false;
			this._flexHSD등록.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexHSD등록.Cursor = System.Windows.Forms.Cursors.Default;
			this._flexHSD등록.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexHSD등록.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexHSD등록.EnabledHeaderCheck = true;
			this._flexHSD등록.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexHSD등록.Location = new System.Drawing.Point(3, 3);
			this._flexHSD등록.Name = "_flexHSD등록";
			this._flexHSD등록.Rows.Count = 1;
			this._flexHSD등록.Rows.DefaultSize = 20;
			this._flexHSD등록.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexHSD등록.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexHSD등록.ShowSort = false;
			this._flexHSD등록.Size = new System.Drawing.Size(1200, 577);
			this._flexHSD등록.StyleInfo = resources.GetString("_flexHSD등록.StyleInfo");
			this._flexHSD등록.TabIndex = 0;
			this._flexHSD등록.UseGridCalculator = true;
			// 
			// P_CZ_PU_HSD_STOCK_RPT
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btn자동등록);
			this.Controls.Add(this.btn판매데이터업로드);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_CZ_PU_HSD_STOCK_RPT";
			this.Size = new System.Drawing.Size(1220, 701);
			this.TitleText = "P_CZ_PU_HSD_STOCK_RPT";
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btn판매데이터업로드, 0);
			this.Controls.SetChildIndex(this.btn자동등록, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.oneGridItem1.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.tabControlExt1.ResumeLayout(false);
			this.tpgERP기준.ResumeLayout(false);
			this.tpgHSD등록.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexERP기준)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flexHSD등록)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private Duzon.Common.Controls.RoundedButton btn판매데이터업로드;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.PeriodPicker dtp수주일자;
		private Duzon.Common.Controls.LabelExt lbl수주일자;
		private Duzon.Common.Controls.RoundedButton btn자동등록;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.DropDownComboBox cbo취소여부;
		private Duzon.Common.Controls.LabelExt lbl취소여부;
		private Duzon.Common.Controls.CheckBoxExt chk판매처확인대상;
		private Duzon.Common.Controls.CheckBoxExt chk입력된건제외;
		private Duzon.Common.Controls.TabControlExt tabControlExt1;
		private System.Windows.Forms.TabPage tpgERP기준;
		private Dass.FlexGrid.FlexGrid _flexERP기준;
		private System.Windows.Forms.TabPage tpgHSD등록;
		private Dass.FlexGrid.FlexGrid _flexHSD등록;
	}
}
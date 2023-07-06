
namespace cz
{
	partial class P_CZ_FI_ETAX_DIFF_RPT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_FI_ETAX_DIFF_RPT));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.chk차이건만 = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt승인번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl승인번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp작성일자 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl작성일자 = new Duzon.Common.Controls.LabelExt();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flex차이 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this._flex국세청 = new Dass.FlexGrid.FlexGrid(this.components);
			this._flexERP = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn첨부파일 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn세금계산서가져오기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex차이)).BeginInit();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex국세청)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flexERP)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1231, 703);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1231, 703);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1225, 40);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.chk차이건만);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1215, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// chk차이건만
			// 
			this.chk차이건만.AutoSize = true;
			this.chk차이건만.Checked = true;
			this.chk차이건만.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk차이건만.Location = new System.Drawing.Point(590, 1);
			this.chk차이건만.Name = "chk차이건만";
			this.chk차이건만.Size = new System.Drawing.Size(72, 16);
			this.chk차이건만.TabIndex = 1;
			this.chk차이건만.Text = "차이건만";
			this.chk차이건만.TextDD = null;
			this.chk차이건만.UseVisualStyleBackColor = true;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.txt승인번호);
			this.bpPanelControl2.Controls.Add(this.lbl승인번호);
			this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 2;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// txt승인번호
			// 
			this.txt승인번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt승인번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt승인번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt승인번호.Location = new System.Drawing.Point(106, 0);
			this.txt승인번호.Name = "txt승인번호";
			this.txt승인번호.Size = new System.Drawing.Size(186, 21);
			this.txt승인번호.TabIndex = 1;
			// 
			// lbl승인번호
			// 
			this.lbl승인번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl승인번호.Location = new System.Drawing.Point(0, 0);
			this.lbl승인번호.Name = "lbl승인번호";
			this.lbl승인번호.Size = new System.Drawing.Size(100, 23);
			this.lbl승인번호.TabIndex = 0;
			this.lbl승인번호.Text = "승인번호";
			this.lbl승인번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.dtp작성일자);
			this.bpPanelControl1.Controls.Add(this.lbl작성일자);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// dtp작성일자
			// 
			this.dtp작성일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp작성일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp작성일자.IsNecessaryCondition = true;
			this.dtp작성일자.Location = new System.Drawing.Point(107, 0);
			this.dtp작성일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp작성일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp작성일자.Name = "dtp작성일자";
			this.dtp작성일자.Size = new System.Drawing.Size(185, 21);
			this.dtp작성일자.TabIndex = 1;
			// 
			// lbl작성일자
			// 
			this.lbl작성일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl작성일자.Location = new System.Drawing.Point(0, 0);
			this.lbl작성일자.Name = "lbl작성일자";
			this.lbl작성일자.Size = new System.Drawing.Size(100, 23);
			this.lbl작성일자.TabIndex = 0;
			this.lbl작성일자.Text = "작성일자";
			this.lbl작성일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 49);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._flex차이);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
			this.splitContainer1.Size = new System.Drawing.Size(1225, 651);
			this.splitContainer1.SplitterDistance = 317;
			this.splitContainer1.TabIndex = 1;
			// 
			// _flex차이
			// 
			this._flex차이.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex차이.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex차이.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex차이.AutoResize = false;
			this._flex차이.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex차이.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex차이.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex차이.EnabledHeaderCheck = true;
			this._flex차이.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex차이.Location = new System.Drawing.Point(0, 0);
			this._flex차이.Name = "_flex차이";
			this._flex차이.Rows.Count = 1;
			this._flex차이.Rows.DefaultSize = 20;
			this._flex차이.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex차이.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex차이.ShowSort = false;
			this._flex차이.Size = new System.Drawing.Size(1225, 317);
			this._flex차이.StyleInfo = resources.GetString("_flex차이.StyleInfo");
			this._flex차이.TabIndex = 0;
			this._flex차이.UseGridCalculator = true;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this._flex국세청, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this._flexERP, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 358F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1225, 330);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// _flex국세청
			// 
			this._flex국세청.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex국세청.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex국세청.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex국세청.AutoResize = false;
			this._flex국세청.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex국세청.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex국세청.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex국세청.EnabledHeaderCheck = true;
			this._flex국세청.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex국세청.Location = new System.Drawing.Point(3, 3);
			this._flex국세청.Name = "_flex국세청";
			this._flex국세청.Rows.Count = 1;
			this._flex국세청.Rows.DefaultSize = 20;
			this._flex국세청.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex국세청.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex국세청.ShowSort = false;
			this._flex국세청.Size = new System.Drawing.Size(606, 324);
			this._flex국세청.StyleInfo = resources.GetString("_flex국세청.StyleInfo");
			this._flex국세청.TabIndex = 3;
			this._flex국세청.UseGridCalculator = true;
			// 
			// _flexERP
			// 
			this._flexERP.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexERP.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexERP.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexERP.AutoResize = false;
			this._flexERP.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexERP.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexERP.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexERP.EnabledHeaderCheck = true;
			this._flexERP.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexERP.Location = new System.Drawing.Point(615, 3);
			this._flexERP.Name = "_flexERP";
			this._flexERP.Rows.Count = 1;
			this._flexERP.Rows.DefaultSize = 20;
			this._flexERP.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexERP.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexERP.ShowSort = false;
			this._flexERP.Size = new System.Drawing.Size(607, 324);
			this._flexERP.StyleInfo = resources.GetString("_flexERP.StyleInfo");
			this._flexERP.TabIndex = 1;
			this._flexERP.UseGridCalculator = true;
			// 
			// btn첨부파일
			// 
			this.btn첨부파일.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn첨부파일.BackColor = System.Drawing.Color.Transparent;
			this.btn첨부파일.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn첨부파일.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn첨부파일.Location = new System.Drawing.Point(1012, 10);
			this.btn첨부파일.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn첨부파일.Name = "btn첨부파일";
			this.btn첨부파일.Size = new System.Drawing.Size(70, 19);
			this.btn첨부파일.TabIndex = 3;
			this.btn첨부파일.TabStop = false;
			this.btn첨부파일.Text = "첨부파일";
			this.btn첨부파일.UseVisualStyleBackColor = false;
			// 
			// btn세금계산서가져오기
			// 
			this.btn세금계산서가져오기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn세금계산서가져오기.BackColor = System.Drawing.Color.Transparent;
			this.btn세금계산서가져오기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn세금계산서가져오기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn세금계산서가져오기.Location = new System.Drawing.Point(1088, 10);
			this.btn세금계산서가져오기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn세금계산서가져오기.Name = "btn세금계산서가져오기";
			this.btn세금계산서가져오기.Size = new System.Drawing.Size(135, 19);
			this.btn세금계산서가져오기.TabIndex = 4;
			this.btn세금계산서가져오기.TabStop = false;
			this.btn세금계산서가져오기.Text = "세금계산서가져오기";
			this.btn세금계산서가져오기.UseVisualStyleBackColor = false;
			// 
			// P_CZ_FI_ETAX_DIFF_RPT
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btn세금계산서가져오기);
			this.Controls.Add(this.btn첨부파일);
			this.Name = "P_CZ_FI_ETAX_DIFF_RPT";
			this.Size = new System.Drawing.Size(1231, 743);
			this.TitleText = "세금계산서처리현황(딘텍)";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btn첨부파일, 0);
			this.Controls.SetChildIndex(this.btn세금계산서가져오기, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.oneGridItem1.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl2.PerformLayout();
			this.bpPanelControl1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex차이)).EndInit();
			this.tableLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex국세청)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flexERP)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Dass.FlexGrid.FlexGrid _flex차이;
		private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.PeriodPicker dtp작성일자;
		private Duzon.Common.Controls.LabelExt lbl작성일자;
		private Duzon.Common.Controls.CheckBoxExt chk차이건만;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private Dass.FlexGrid.FlexGrid _flex국세청;
		private Dass.FlexGrid.FlexGrid _flexERP;
		private Duzon.Common.Controls.RoundedButton btn첨부파일;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.TextBoxExt txt승인번호;
		private Duzon.Common.Controls.LabelExt lbl승인번호;
		private Duzon.Common.Controls.RoundedButton btn세금계산서가져오기;
	}
}
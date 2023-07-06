namespace cz
{
    partial class P_CZ_MM_NOT_GI_ITEM_LIST
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MM_NOT_GI_ITEM_LIST));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.chk이미지미등록 = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp입고일자 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl입고일자 = new Duzon.Common.Controls.LabelExt();
			this._flex미출고리스트 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpg미출고리스트 = new System.Windows.Forms.TabPage();
			this.tpg재고코드 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this._flex재고코드 = new Dass.FlexGrid.FlexGrid(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex미출고리스트)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tpg미출고리스트.SuspendLayout();
			this.tpg재고코드.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex재고코드)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tabControl1);
			this.mDataArea.Size = new System.Drawing.Size(1263, 698);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._flex미출고리스트, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1249, 666);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1243, 39);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.chk이미지미등록);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1233, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// chk이미지미등록
			// 
			this.chk이미지미등록.AutoSize = true;
			this.chk이미지미등록.Checked = true;
			this.chk이미지미등록.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk이미지미등록.Location = new System.Drawing.Point(296, 1);
			this.chk이미지미등록.Name = "chk이미지미등록";
			this.chk이미지미등록.Size = new System.Drawing.Size(104, 24);
			this.chk이미지미등록.TabIndex = 0;
			this.chk이미지미등록.Text = "이미지미등록";
			this.chk이미지미등록.TextDD = null;
			this.chk이미지미등록.UseVisualStyleBackColor = true;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.dtp입고일자);
			this.bpPanelControl1.Controls.Add(this.lbl입고일자);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 1;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// dtp입고일자
			// 
			this.dtp입고일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp입고일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp입고일자.IsNecessaryCondition = true;
			this.dtp입고일자.Location = new System.Drawing.Point(107, 0);
			this.dtp입고일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp입고일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp입고일자.Name = "dtp입고일자";
			this.dtp입고일자.Size = new System.Drawing.Size(185, 21);
			this.dtp입고일자.TabIndex = 1;
			// 
			// lbl입고일자
			// 
			this.lbl입고일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl입고일자.Location = new System.Drawing.Point(0, 0);
			this.lbl입고일자.Name = "lbl입고일자";
			this.lbl입고일자.Size = new System.Drawing.Size(100, 23);
			this.lbl입고일자.TabIndex = 0;
			this.lbl입고일자.Text = "입고일자";
			this.lbl입고일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _flex미출고리스트
			// 
			this._flex미출고리스트.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex미출고리스트.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex미출고리스트.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex미출고리스트.AutoResize = false;
			this._flex미출고리스트.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex미출고리스트.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex미출고리스트.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex미출고리스트.EnabledHeaderCheck = true;
			this._flex미출고리스트.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex미출고리스트.Location = new System.Drawing.Point(3, 48);
			this._flex미출고리스트.Name = "_flex미출고리스트";
			this._flex미출고리스트.Rows.Count = 1;
			this._flex미출고리스트.Rows.DefaultSize = 20;
			this._flex미출고리스트.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex미출고리스트.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex미출고리스트.ShowSort = false;
			this._flex미출고리스트.Size = new System.Drawing.Size(1243, 615);
			this._flex미출고리스트.StyleInfo = resources.GetString("_flex미출고리스트.StyleInfo");
			this._flex미출고리스트.TabIndex = 1;
			this._flex미출고리스트.UseGridCalculator = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tpg미출고리스트);
			this.tabControl1.Controls.Add(this.tpg재고코드);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1263, 698);
			this.tabControl1.TabIndex = 3;
			// 
			// tpg미출고리스트
			// 
			this.tpg미출고리스트.Controls.Add(this.tableLayoutPanel1);
			this.tpg미출고리스트.Location = new System.Drawing.Point(4, 22);
			this.tpg미출고리스트.Name = "tpg미출고리스트";
			this.tpg미출고리스트.Padding = new System.Windows.Forms.Padding(3);
			this.tpg미출고리스트.Size = new System.Drawing.Size(1255, 672);
			this.tpg미출고리스트.TabIndex = 0;
			this.tpg미출고리스트.Text = "미출고리스트";
			this.tpg미출고리스트.UseVisualStyleBackColor = true;
			// 
			// tpg재고코드
			// 
			this.tpg재고코드.Controls.Add(this.tableLayoutPanel2);
			this.tpg재고코드.Location = new System.Drawing.Point(4, 22);
			this.tpg재고코드.Name = "tpg재고코드";
			this.tpg재고코드.Padding = new System.Windows.Forms.Padding(3);
			this.tpg재고코드.Size = new System.Drawing.Size(1255, 672);
			this.tpg재고코드.TabIndex = 1;
			this.tpg재고코드.Text = "재고코드";
			this.tpg재고코드.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this._flex재고코드, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1249, 666);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// _flex재고코드
			// 
			this._flex재고코드.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex재고코드.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex재고코드.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex재고코드.AutoResize = false;
			this._flex재고코드.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex재고코드.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex재고코드.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex재고코드.EnabledHeaderCheck = true;
			this._flex재고코드.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex재고코드.Location = new System.Drawing.Point(3, 3);
			this._flex재고코드.Name = "_flex재고코드";
			this._flex재고코드.Rows.Count = 1;
			this._flex재고코드.Rows.DefaultSize = 20;
			this._flex재고코드.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex재고코드.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex재고코드.ShowSort = false;
			this._flex재고코드.Size = new System.Drawing.Size(1243, 660);
			this._flex재고코드.StyleInfo = resources.GetString("_flex재고코드.StyleInfo");
			this._flex재고코드.TabIndex = 0;
			this._flex재고코드.UseGridCalculator = true;
			// 
			// P_CZ_MM_NOT_GI_ITEM_LIST
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "P_CZ_MM_NOT_GI_ITEM_LIST";
			this.Size = new System.Drawing.Size(1263, 738);
			this.TitleText = "P_CZ_MM_NOT_GI_ITEM_LIST";
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.oneGridItem1.PerformLayout();
			this.bpPanelControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex미출고리스트)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tpg미출고리스트.ResumeLayout(false);
			this.tpg재고코드.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex재고코드)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Dass.FlexGrid.FlexGrid _flex미출고리스트;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.Controls.CheckBoxExt chk이미지미등록;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.LabelExt lbl입고일자;
        private Duzon.Common.Controls.PeriodPicker dtp입고일자;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpg미출고리스트;
		private System.Windows.Forms.TabPage tpg재고코드;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private Dass.FlexGrid.FlexGrid _flex재고코드;
	}
}
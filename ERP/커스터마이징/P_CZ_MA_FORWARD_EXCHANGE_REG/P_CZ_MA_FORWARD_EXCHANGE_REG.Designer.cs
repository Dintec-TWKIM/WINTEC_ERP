namespace cz
{
    partial class P_CZ_MA_FORWARD_EXCHANGE_REG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MA_FORWARD_EXCHANGE_REG));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp선물환월 = new Duzon.Common.Controls.DatePicker();
            this.lbl선물환월 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl체결일자 = new Duzon.Common.Controls.LabelExt();
            this.dtp체결일자 = new Duzon.Common.Controls.PeriodPicker();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl선물환일자 = new Duzon.Common.Controls.LabelExt();
            this.dtp선물환일자 = new Duzon.Common.Controls.PeriodPicker();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo은행 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl은행 = new Duzon.Common.Controls.LabelExt();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl선물일자미도래 = new Duzon.Common.Controls.LabelExt();
            this.chk선물일자미도래 = new Duzon.Common.Controls.CheckBoxExt();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp선물환월)).BeginInit();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.bpPanelControl5.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(827, 579);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(821, 62);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl3);
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.dtp선물환월);
            this.bpPanelControl3.Controls.Add(this.lbl선물환월);
            this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(195, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // dtp선물환월
            // 
            this.dtp선물환월.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp선물환월.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp선물환월.Location = new System.Drawing.Point(105, 0);
            this.dtp선물환월.Mask = "####/##";
            this.dtp선물환월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp선물환월.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp선물환월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp선물환월.Modified = true;
            this.dtp선물환월.Name = "dtp선물환월";
            this.dtp선물환월.ShowUpDown = true;
            this.dtp선물환월.Size = new System.Drawing.Size(90, 21);
            this.dtp선물환월.TabIndex = 2;
            this.dtp선물환월.Value = new System.DateTime(((long)(0)));
            // 
            // lbl선물환월
            // 
            this.lbl선물환월.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl선물환월.Location = new System.Drawing.Point(0, 0);
            this.lbl선물환월.Name = "lbl선물환월";
            this.lbl선물환월.Size = new System.Drawing.Size(100, 23);
            this.lbl선물환월.TabIndex = 1;
            this.lbl선물환월.Text = "월(선물환)";
            this.lbl선물환월.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.lbl체결일자);
            this.bpPanelControl2.Controls.Add(this.dtp체결일자);
            this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // lbl체결일자
            // 
            this.lbl체결일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl체결일자.Location = new System.Drawing.Point(0, 0);
            this.lbl체결일자.Name = "lbl체결일자";
            this.lbl체결일자.Size = new System.Drawing.Size(100, 23);
            this.lbl체결일자.TabIndex = 1;
            this.lbl체결일자.Text = "체결일자";
            this.lbl체결일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtp체결일자
            // 
            this.dtp체결일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp체결일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp체결일자.Location = new System.Drawing.Point(107, 0);
            this.dtp체결일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp체결일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp체결일자.Name = "dtp체결일자";
            this.dtp체결일자.Size = new System.Drawing.Size(185, 21);
            this.dtp체결일자.TabIndex = 0;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.lbl선물환일자);
            this.bpPanelControl1.Controls.Add(this.dtp선물환일자);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // lbl선물환일자
            // 
            this.lbl선물환일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl선물환일자.Location = new System.Drawing.Point(0, 0);
            this.lbl선물환일자.Name = "lbl선물환일자";
            this.lbl선물환일자.Size = new System.Drawing.Size(100, 23);
            this.lbl선물환일자.TabIndex = 1;
            this.lbl선물환일자.Text = "선물환일자";
            this.lbl선물환일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtp선물환일자
            // 
            this.dtp선물환일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp선물환일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp선물환일자.Location = new System.Drawing.Point(107, 0);
            this.dtp선물환일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp선물환일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp선물환일자.Name = "dtp선물환일자";
            this.dtp선물환일자.Size = new System.Drawing.Size(185, 21);
            this.dtp선물환일자.TabIndex = 0;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.cbo은행);
            this.bpPanelControl4.Controls.Add(this.lbl은행);
            this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl4.TabIndex = 1;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // cbo은행
            // 
            this.cbo은행.AutoDropDown = true;
            this.cbo은행.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo은행.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo은행.FormattingEnabled = true;
            this.cbo은행.ItemHeight = 12;
            this.cbo은행.Location = new System.Drawing.Point(107, 0);
            this.cbo은행.Name = "cbo은행";
            this.cbo은행.Size = new System.Drawing.Size(185, 20);
            this.cbo은행.TabIndex = 2;
            // 
            // lbl은행
            // 
            this.lbl은행.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl은행.Location = new System.Drawing.Point(0, 0);
            this.lbl은행.Name = "lbl은행";
            this.lbl은행.Size = new System.Drawing.Size(100, 23);
            this.lbl은행.TabIndex = 1;
            this.lbl은행.Text = "은행";
            this.lbl은행.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(3, 71);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(821, 505);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 1;
            this._flex.UseGridCalculator = true;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.chk선물일자미도래);
            this.bpPanelControl5.Controls.Add(this.lbl선물일자미도래);
            this.bpPanelControl5.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(122, 23);
            this.bpPanelControl5.TabIndex = 2;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // lbl선물일자미도래
            // 
            this.lbl선물일자미도래.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl선물일자미도래.Location = new System.Drawing.Point(0, 0);
            this.lbl선물일자미도래.Name = "lbl선물일자미도래";
            this.lbl선물일자미도래.Size = new System.Drawing.Size(100, 23);
            this.lbl선물일자미도래.TabIndex = 0;
            this.lbl선물일자미도래.Text = "선물일자미도래";
            this.lbl선물일자미도래.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chk선물일자미도래
            // 
            this.chk선물일자미도래.AutoSize = true;
            this.chk선물일자미도래.Dock = System.Windows.Forms.DockStyle.Right;
            this.chk선물일자미도래.Location = new System.Drawing.Point(107, 0);
            this.chk선물일자미도래.Name = "chk선물일자미도래";
            this.chk선물일자미도래.Size = new System.Drawing.Size(15, 23);
            this.chk선물일자미도래.TabIndex = 1;
            this.chk선물일자미도래.TextDD = null;
            this.chk선물일자미도래.UseVisualStyleBackColor = true;
            // 
            // P_CZ_MA_FORWARD_EXCHANGE_REG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_CZ_MA_FORWARD_EXCHANGE_REG";
            this.TitleText = "선물환정보등록";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp선물환월)).EndInit();
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.LabelExt lbl선물환일자;
        private Duzon.Common.Controls.PeriodPicker dtp선물환일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.LabelExt lbl체결일자;
        private Duzon.Common.Controls.PeriodPicker dtp체결일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.DatePicker dtp선물환월;
        private Duzon.Common.Controls.LabelExt lbl선물환월;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.DropDownComboBox cbo은행;
        private Duzon.Common.Controls.LabelExt lbl은행;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.Controls.CheckBoxExt chk선물일자미도래;
        private Duzon.Common.Controls.LabelExt lbl선물일자미도래;
    }
}
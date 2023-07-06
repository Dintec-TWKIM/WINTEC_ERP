namespace cz
{
    partial class P_CZ_BI_WTMCALC_MONTH_RMK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_BI_WTMCALC_MONTH_RMK));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.btn저장 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.dtp반영월 = new Duzon.Common.Controls.DatePicker();
            this.labelExt4 = new Duzon.Common.Controls.LabelExt();
            this.lblWeek = new Duzon.Common.Controls.LabelExt();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp반영월)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 508F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(633, 742);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(3, 48);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(627, 691);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 1;
            this._flex.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(627, 39);
            this.oneGrid1.TabIndex = 2;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(617, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.btn저장);
            this.bpPanelControl2.Controls.Add(this.btn조회);
            this.bpPanelControl2.Controls.Add(this.dtp반영월);
            this.bpPanelControl2.Controls.Add(this.labelExt4);
            this.bpPanelControl2.Controls.Add(this.lblWeek);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(611, 22);
            this.bpPanelControl2.TabIndex = 19;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // btn저장
            // 
            this.btn저장.BackColor = System.Drawing.Color.White;
            this.btn저장.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn저장.Location = new System.Drawing.Point(536, 3);
            this.btn저장.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn저장.Name = "btn저장";
            this.btn저장.Size = new System.Drawing.Size(70, 19);
            this.btn저장.TabIndex = 15;
            this.btn저장.TabStop = false;
            this.btn저장.Text = "저장";
            this.btn저장.UseVisualStyleBackColor = false;
            // 
            // btn조회
            // 
            this.btn조회.BackColor = System.Drawing.Color.White;
            this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn조회.Location = new System.Drawing.Point(151, 3);
            this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn조회.Name = "btn조회";
            this.btn조회.Size = new System.Drawing.Size(70, 19);
            this.btn조회.TabIndex = 15;
            this.btn조회.TabStop = false;
            this.btn조회.Text = "조회";
            this.btn조회.UseVisualStyleBackColor = false;
            // 
            // dtp반영월
            // 
            this.dtp반영월.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp반영월.Location = new System.Drawing.Point(69, 2);
            this.dtp반영월.Mask = "####/##";
            this.dtp반영월.MaskBackColor = System.Drawing.Color.White;
            this.dtp반영월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp반영월.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp반영월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp반영월.Modified = true;
            this.dtp반영월.Name = "dtp반영월";
            this.dtp반영월.ShowUpDown = true;
            this.dtp반영월.Size = new System.Drawing.Size(69, 21);
            this.dtp반영월.TabIndex = 4;
            this.dtp반영월.Tag = "DT_START";
            this.dtp반영월.Value = new System.DateTime(((long)(0)));
            // 
            // labelExt4
            // 
            this.labelExt4.Location = new System.Drawing.Point(15, 2);
            this.labelExt4.Name = "labelExt4";
            this.labelExt4.Size = new System.Drawing.Size(47, 22);
            this.labelExt4.TabIndex = 3;
            this.labelExt4.Text = "조회월";
            this.labelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWeek
            // 
            this.lblWeek.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblWeek.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblWeek.Location = new System.Drawing.Point(-168, 0);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(779, 22);
            this.lblWeek.TabIndex = 3;
            this.lblWeek.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // P_CZ_BI_WTMCALC_MONTH_RMK
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(633, 742);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_CZ_BI_WTMCALC_MONTH_RMK";
            this.Text = "ERP iU";
            this.TitleText = "월별근태등록 비고";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp반영월)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.RoundedButton btn저장;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Duzon.Common.Controls.DatePicker dtp반영월;
        private Duzon.Common.Controls.LabelExt labelExt4;
        private Duzon.Common.Controls.LabelExt lblWeek;


    }
}
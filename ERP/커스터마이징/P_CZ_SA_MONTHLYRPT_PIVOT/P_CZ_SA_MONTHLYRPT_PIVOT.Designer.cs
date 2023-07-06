namespace cz
{
    partial class P_CZ_SA_MONTHLYRPT_PIVOT
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.chk비용포함 = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk무상공급금액제외 = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo견적유형 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl견적유형 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo목표기준 = new Duzon.Common.Controls.DropDownComboBox();
			this.labelExt1 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp귀속년월 = new Duzon.Common.Controls.DatePicker();
			this.lbl귀속년월 = new Duzon.Common.Controls.LabelExt();
			this._pivot = new Duzon.BizOn.Windows.PivotGrid.PivotGrid();
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp귀속년월)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1090, 756);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._pivot, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1090, 756);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1084, 39);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.chk비용포함);
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.Controls.Add(this.bpPanelControl4);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1074, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// chk비용포함
			// 
			this.chk비용포함.Location = new System.Drawing.Point(828, 1);
			this.chk비용포함.Name = "chk비용포함";
			this.chk비용포함.Size = new System.Drawing.Size(73, 24);
			this.chk비용포함.TabIndex = 4;
			this.chk비용포함.Text = "비용포함";
			this.chk비용포함.TextDD = null;
			this.chk비용포함.UseVisualStyleBackColor = true;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.chk무상공급금액제외);
			this.bpPanelControl3.Location = new System.Drawing.Point(707, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(119, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// chk무상공급금액제외
			// 
			this.chk무상공급금액제외.AutoSize = true;
			this.chk무상공급금액제외.Dock = System.Windows.Forms.DockStyle.Left;
			this.chk무상공급금액제외.Location = new System.Drawing.Point(0, 0);
			this.chk무상공급금액제외.Name = "chk무상공급금액제외";
			this.chk무상공급금액제외.Size = new System.Drawing.Size(120, 23);
			this.chk무상공급금액제외.TabIndex = 0;
			this.chk무상공급금액제외.Text = "무상공급금액제외";
			this.chk무상공급금액제외.TextDD = null;
			this.chk무상공급금액제외.UseVisualStyleBackColor = true;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.cbo견적유형);
			this.bpPanelControl4.Controls.Add(this.lbl견적유형);
			this.bpPanelControl4.Location = new System.Drawing.Point(447, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(258, 23);
			this.bpPanelControl4.TabIndex = 3;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// cbo견적유형
			// 
			this.cbo견적유형.AutoDropDown = true;
			this.cbo견적유형.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo견적유형.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo견적유형.FormattingEnabled = true;
			this.cbo견적유형.ItemHeight = 12;
			this.cbo견적유형.Location = new System.Drawing.Point(106, 0);
			this.cbo견적유형.Name = "cbo견적유형";
			this.cbo견적유형.Size = new System.Drawing.Size(152, 20);
			this.cbo견적유형.TabIndex = 1;
			// 
			// lbl견적유형
			// 
			this.lbl견적유형.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl견적유형.Location = new System.Drawing.Point(0, 0);
			this.lbl견적유형.Name = "lbl견적유형";
			this.lbl견적유형.Size = new System.Drawing.Size(100, 23);
			this.lbl견적유형.TabIndex = 0;
			this.lbl견적유형.Text = "견적유형";
			this.lbl견적유형.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.cbo목표기준);
			this.bpPanelControl2.Controls.Add(this.labelExt1);
			this.bpPanelControl2.Location = new System.Drawing.Point(187, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(258, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// cbo목표기준
			// 
			this.cbo목표기준.AutoDropDown = true;
			this.cbo목표기준.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo목표기준.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo목표기준.FormattingEnabled = true;
			this.cbo목표기준.ItemHeight = 12;
			this.cbo목표기준.Location = new System.Drawing.Point(106, 0);
			this.cbo목표기준.Name = "cbo목표기준";
			this.cbo목표기준.Size = new System.Drawing.Size(152, 20);
			this.cbo목표기준.TabIndex = 1;
			// 
			// labelExt1
			// 
			this.labelExt1.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelExt1.Location = new System.Drawing.Point(0, 0);
			this.labelExt1.Name = "labelExt1";
			this.labelExt1.Size = new System.Drawing.Size(100, 23);
			this.labelExt1.TabIndex = 0;
			this.labelExt1.Text = "목표기준";
			this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.dtp귀속년월);
			this.bpPanelControl1.Controls.Add(this.lbl귀속년월);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(183, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// dtp귀속년월
			// 
			this.dtp귀속년월.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp귀속년월.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp귀속년월.Location = new System.Drawing.Point(110, 0);
			this.dtp귀속년월.Mask = "####/##";
			this.dtp귀속년월.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.dtp귀속년월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp귀속년월.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp귀속년월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp귀속년월.Modified = true;
			this.dtp귀속년월.Name = "dtp귀속년월";
			this.dtp귀속년월.ShowUpDown = true;
			this.dtp귀속년월.Size = new System.Drawing.Size(73, 21);
			this.dtp귀속년월.TabIndex = 1;
			this.dtp귀속년월.Value = new System.DateTime(((long)(0)));
			// 
			// lbl귀속년월
			// 
			this.lbl귀속년월.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl귀속년월.Location = new System.Drawing.Point(0, 0);
			this.lbl귀속년월.Name = "lbl귀속년월";
			this.lbl귀속년월.Size = new System.Drawing.Size(100, 23);
			this.lbl귀속년월.TabIndex = 0;
			this.lbl귀속년월.Text = "귀속년월";
			this.lbl귀속년월.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _pivot
			// 
			this._pivot.Dock = System.Windows.Forms.DockStyle.Fill;
			this._pivot.LocalLanguage = Duzon.BizOn.Windows.PivotGrid.LocalLanguage.KOR;
			this._pivot.Location = new System.Drawing.Point(3, 48);
			this._pivot.Name = "_pivot";
			this._pivot.Size = new System.Drawing.Size(1084, 705);
			this._pivot.TabIndex = 1;
			this._pivot.Text = "pivotGrid1";
			// 
			// P_CZ_SA_MONTHLYRPT_PIVOT
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_SA_MONTHLYRPT_PIVOT";
			this.Size = new System.Drawing.Size(1090, 796);
			this.TitleText = "월별수주실적현황(PIVOT)";
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp귀속년월)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.LabelExt lbl귀속년월;
        private Duzon.Common.Controls.DatePicker dtp귀속년월;
        private Duzon.BizOn.Windows.PivotGrid.PivotGrid _pivot;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.DropDownComboBox cbo목표기준;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.CheckBoxExt chk무상공급금액제외;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
		private Duzon.Common.Controls.DropDownComboBox cbo견적유형;
		private Duzon.Common.Controls.LabelExt lbl견적유형;
		private Duzon.Common.Controls.CheckBoxExt chk비용포함;
	}
}
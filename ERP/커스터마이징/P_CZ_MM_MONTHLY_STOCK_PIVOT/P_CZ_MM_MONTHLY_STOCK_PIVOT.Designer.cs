namespace cz
{
    partial class P_CZ_MM_MONTHLY_STOCK_PIVOT
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.chk재고수량0제외 = new Duzon.Common.Controls.CheckBoxExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp기준년월 = new Duzon.Common.Controls.DatePicker();
            this.lbl기준년월 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx소분류 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl소분류 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx중분류 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl중분류 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx대분류 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl대분류 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt재고코드 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl재고코드 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo계정구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl계정구분 = new Duzon.Common.Controls.LabelExt();
            this._pivot = new Duzon.BizOn.Windows.PivotGrid.PivotGrid();
            this.btn도움말 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp기준년월)).BeginInit();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.oneGridItem3.SuspendLayout();
            this.bpPanelControl7.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(976, 579);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._pivot, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(976, 579);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(970, 85);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(960, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.chk재고수량0제외);
            this.bpPanelControl2.Location = new System.Drawing.Point(201, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(131, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // chk재고수량0제외
            // 
            this.chk재고수량0제외.Dock = System.Windows.Forms.DockStyle.Right;
            this.chk재고수량0제외.Location = new System.Drawing.Point(5, 0);
            this.chk재고수량0제외.Name = "chk재고수량0제외";
            this.chk재고수량0제외.Size = new System.Drawing.Size(126, 23);
            this.chk재고수량0제외.TabIndex = 0;
            this.chk재고수량0제외.Text = "재고수량0제외";
            this.chk재고수량0제외.TextDD = null;
            this.chk재고수량0제외.UseVisualStyleBackColor = true;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp기준년월);
            this.bpPanelControl1.Controls.Add(this.lbl기준년월);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(197, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // dtp기준년월
            // 
            this.dtp기준년월.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp기준년월.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp기준년월.Location = new System.Drawing.Point(106, 0);
            this.dtp기준년월.Mask = "####/##";
            this.dtp기준년월.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp기준년월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp기준년월.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp기준년월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp기준년월.Modified = true;
            this.dtp기준년월.Name = "dtp기준년월";
            this.dtp기준년월.ShowUpDown = true;
            this.dtp기준년월.Size = new System.Drawing.Size(91, 21);
            this.dtp기준년월.TabIndex = 1;
            this.dtp기준년월.Value = new System.DateTime(((long)(0)));
            // 
            // lbl기준년월
            // 
            this.lbl기준년월.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl기준년월.Location = new System.Drawing.Point(0, 0);
            this.lbl기준년월.Name = "lbl기준년월";
            this.lbl기준년월.Size = new System.Drawing.Size(100, 23);
            this.lbl기준년월.TabIndex = 0;
            this.lbl기준년월.Text = "기준년월";
            this.lbl기준년월.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(960, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.ctx소분류);
            this.bpPanelControl6.Controls.Add(this.lbl소분류);
            this.bpPanelControl6.Location = new System.Drawing.Point(640, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(317, 23);
            this.bpPanelControl6.TabIndex = 5;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // ctx소분류
            // 
            this.ctx소분류.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx소분류.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
            this.ctx소분류.Location = new System.Drawing.Point(106, 0);
            this.ctx소분류.Name = "ctx소분류";
            this.ctx소분류.Size = new System.Drawing.Size(211, 21);
            this.ctx소분류.TabIndex = 1;
            this.ctx소분류.TabStop = false;
            this.ctx소분류.Text = "bpCodeTextBox3";
            // 
            // lbl소분류
            // 
            this.lbl소분류.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl소분류.Location = new System.Drawing.Point(0, 0);
            this.lbl소분류.Name = "lbl소분류";
            this.lbl소분류.Size = new System.Drawing.Size(100, 23);
            this.lbl소분류.TabIndex = 0;
            this.lbl소분류.Text = "소분류";
            this.lbl소분류.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.ctx중분류);
            this.bpPanelControl5.Controls.Add(this.lbl중분류);
            this.bpPanelControl5.Location = new System.Drawing.Point(321, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(317, 23);
            this.bpPanelControl5.TabIndex = 4;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // ctx중분류
            // 
            this.ctx중분류.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx중분류.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
            this.ctx중분류.Location = new System.Drawing.Point(106, 0);
            this.ctx중분류.Name = "ctx중분류";
            this.ctx중분류.Size = new System.Drawing.Size(211, 21);
            this.ctx중분류.TabIndex = 1;
            this.ctx중분류.TabStop = false;
            this.ctx중분류.Text = "bpCodeTextBox2";
            // 
            // lbl중분류
            // 
            this.lbl중분류.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl중분류.Location = new System.Drawing.Point(0, 0);
            this.lbl중분류.Name = "lbl중분류";
            this.lbl중분류.Size = new System.Drawing.Size(100, 23);
            this.lbl중분류.TabIndex = 0;
            this.lbl중분류.Text = "중분류";
            this.lbl중분류.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.ctx대분류);
            this.bpPanelControl4.Controls.Add(this.lbl대분류);
            this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(317, 23);
            this.bpPanelControl4.TabIndex = 3;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // ctx대분류
            // 
            this.ctx대분류.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx대분류.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
            this.ctx대분류.Location = new System.Drawing.Point(106, 0);
            this.ctx대분류.Name = "ctx대분류";
            this.ctx대분류.Size = new System.Drawing.Size(211, 21);
            this.ctx대분류.TabIndex = 1;
            this.ctx대분류.TabStop = false;
            this.ctx대분류.Text = "bpCodeTextBox1";
            // 
            // lbl대분류
            // 
            this.lbl대분류.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl대분류.Location = new System.Drawing.Point(0, 0);
            this.lbl대분류.Name = "lbl대분류";
            this.lbl대분류.Size = new System.Drawing.Size(100, 23);
            this.lbl대분류.TabIndex = 0;
            this.lbl대분류.Text = "대분류";
            this.lbl대분류.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl7);
            this.oneGridItem3.Controls.Add(this.bpPanelControl3);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(960, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 2;
            // 
            // bpPanelControl7
            // 
            this.bpPanelControl7.Controls.Add(this.txt재고코드);
            this.bpPanelControl7.Controls.Add(this.lbl재고코드);
            this.bpPanelControl7.Location = new System.Drawing.Point(321, 1);
            this.bpPanelControl7.Name = "bpPanelControl7";
            this.bpPanelControl7.Size = new System.Drawing.Size(317, 23);
            this.bpPanelControl7.TabIndex = 4;
            this.bpPanelControl7.Text = "bpPanelControl7";
            // 
            // txt재고코드
            // 
            this.txt재고코드.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt재고코드.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt재고코드.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt재고코드.Location = new System.Drawing.Point(106, 0);
            this.txt재고코드.Name = "txt재고코드";
            this.txt재고코드.Size = new System.Drawing.Size(211, 21);
            this.txt재고코드.TabIndex = 1;
            // 
            // lbl재고코드
            // 
            this.lbl재고코드.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl재고코드.Location = new System.Drawing.Point(0, 0);
            this.lbl재고코드.Name = "lbl재고코드";
            this.lbl재고코드.Size = new System.Drawing.Size(100, 23);
            this.lbl재고코드.TabIndex = 0;
            this.lbl재고코드.Text = "재고코드";
            this.lbl재고코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.cbo계정구분);
            this.bpPanelControl3.Controls.Add(this.lbl계정구분);
            this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(317, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // cbo계정구분
            // 
            this.cbo계정구분.AutoDropDown = true;
            this.cbo계정구분.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo계정구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo계정구분.FormattingEnabled = true;
            this.cbo계정구분.ItemHeight = 12;
            this.cbo계정구분.Location = new System.Drawing.Point(106, 0);
            this.cbo계정구분.Name = "cbo계정구분";
            this.cbo계정구분.Size = new System.Drawing.Size(211, 20);
            this.cbo계정구분.TabIndex = 1;
            // 
            // lbl계정구분
            // 
            this.lbl계정구분.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl계정구분.Location = new System.Drawing.Point(0, 0);
            this.lbl계정구분.Name = "lbl계정구분";
            this.lbl계정구분.Size = new System.Drawing.Size(100, 23);
            this.lbl계정구분.TabIndex = 0;
            this.lbl계정구분.Text = "계정구분";
            this.lbl계정구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _pivot
            // 
            this._pivot.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pivot.LocalLanguage = Duzon.BizOn.Windows.PivotGrid.LocalLanguage.KOR;
            this._pivot.Location = new System.Drawing.Point(3, 94);
            this._pivot.Name = "_pivot";
            this._pivot.Size = new System.Drawing.Size(970, 482);
            this._pivot.TabIndex = 1;
            this._pivot.Text = "pivotGrid1";
            // 
            // btn도움말
            // 
            this.btn도움말.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn도움말.BackColor = System.Drawing.Color.Transparent;
            this.btn도움말.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn도움말.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn도움말.Location = new System.Drawing.Point(893, 10);
            this.btn도움말.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn도움말.Name = "btn도움말";
            this.btn도움말.Size = new System.Drawing.Size(83, 19);
            this.btn도움말.TabIndex = 3;
            this.btn도움말.TabStop = false;
            this.btn도움말.Text = "도움말";
            this.btn도움말.UseVisualStyleBackColor = false;
            // 
            // P_CZ_MM_MONTHLY_STOCK_PIVOT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btn도움말);
            this.Name = "P_CZ_MM_MONTHLY_STOCK_PIVOT";
            this.Size = new System.Drawing.Size(976, 619);
            this.TitleText = "P_CZ_MM_MONTHLY_STOCK_RPT_PIVOT";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.btn도움말, 0);
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp기준년월)).EndInit();
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.oneGridItem3.ResumeLayout(false);
            this.bpPanelControl7.ResumeLayout(false);
            this.bpPanelControl7.PerformLayout();
            this.bpPanelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.BizOn.Windows.PivotGrid.PivotGrid _pivot;
        private Duzon.Common.Controls.DatePicker dtp기준년월;
        private Duzon.Common.Controls.LabelExt lbl기준년월;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.CheckBoxExt chk재고수량0제외;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.LabelExt lbl계정구분;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.Controls.DropDownComboBox cbo계정구분;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.LabelExt lbl대분류;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
        private Duzon.Common.Controls.TextBoxExt txt재고코드;
        private Duzon.Common.Controls.LabelExt lbl재고코드;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.BpControls.BpCodeTextBox ctx소분류;
        private Duzon.Common.Controls.LabelExt lbl소분류;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpCodeTextBox ctx중분류;
        private Duzon.Common.Controls.LabelExt lbl중분류;
        private Duzon.Common.BpControls.BpCodeTextBox ctx대분류;
        private Duzon.Common.Controls.RoundedButton btn도움말;
    }
}
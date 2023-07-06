namespace cz
{
    partial class P_CZ_SA_IV_BASE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_IV_BASE));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx매출처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl매출처 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctxCC = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lblCC = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp조회기간 = new Duzon.Common.Controls.PeriodPicker();
            this.cbo조회기간 = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chk전표승인 = new Duzon.Common.Controls.CheckBoxExt();
            this.chk매출계정만표시 = new Duzon.Common.Controls.CheckBoxExt();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt계산서번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl계산서번호 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt프로젝트 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl프로젝트 = new Duzon.Common.Controls.LabelExt();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._flexH, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1090, 756);
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
            this.oneGrid1.Size = new System.Drawing.Size(1084, 63);
            this.oneGrid1.TabIndex = 1;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl6);
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(1074, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.ctx매출처);
            this.bpPanelControl6.Controls.Add(this.lbl매출처);
            this.bpPanelControl6.Location = new System.Drawing.Point(594, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(294, 23);
            this.bpPanelControl6.TabIndex = 3;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // ctx매출처
            // 
            this.ctx매출처.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx매출처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx매출처.Location = new System.Drawing.Point(106, 0);
            this.ctx매출처.Name = "ctx매출처";
            this.ctx매출처.Size = new System.Drawing.Size(188, 21);
            this.ctx매출처.TabIndex = 1;
            this.ctx매출처.TabStop = false;
            this.ctx매출처.Text = "bpCodeTextBox1";
            // 
            // lbl매출처
            // 
            this.lbl매출처.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl매출처.Location = new System.Drawing.Point(0, 0);
            this.lbl매출처.Name = "lbl매출처";
            this.lbl매출처.Size = new System.Drawing.Size(100, 23);
            this.lbl매출처.TabIndex = 0;
            this.lbl매출처.Text = "매출처";
            this.lbl매출처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.ctxCC);
            this.bpPanelControl2.Controls.Add(this.lblCC);
            this.bpPanelControl2.Location = new System.Drawing.Point(298, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(294, 23);
            this.bpPanelControl2.TabIndex = 2;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // ctxCC
            // 
            this.ctxCC.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctxCC.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CC_SUB;
            this.ctxCC.Location = new System.Drawing.Point(106, 0);
            this.ctxCC.Name = "ctxCC";
            this.ctxCC.Size = new System.Drawing.Size(188, 21);
            this.ctxCC.TabIndex = 1;
            this.ctxCC.TabStop = false;
            this.ctxCC.Text = "bpCodeTextBox1";
            // 
            // lblCC
            // 
            this.lblCC.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCC.Location = new System.Drawing.Point(0, 0);
            this.lblCC.Name = "lblCC";
            this.lblCC.Size = new System.Drawing.Size(100, 23);
            this.lblCC.TabIndex = 0;
            this.lblCC.Text = "C/C";
            this.lblCC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp조회기간);
            this.bpPanelControl1.Controls.Add(this.cbo조회기간);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(294, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // dtp조회기간
            // 
            this.dtp조회기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp조회기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp조회기간.IsNecessaryCondition = true;
            this.dtp조회기간.Location = new System.Drawing.Point(109, 0);
            this.dtp조회기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp조회기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp조회기간.Name = "dtp조회기간";
            this.dtp조회기간.Size = new System.Drawing.Size(185, 21);
            this.dtp조회기간.TabIndex = 1;
            // 
            // cbo조회기간
            // 
            this.cbo조회기간.AutoDropDown = true;
            this.cbo조회기간.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbo조회기간.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo조회기간.FormattingEnabled = true;
            this.cbo조회기간.ItemHeight = 12;
            this.cbo조회기간.Location = new System.Drawing.Point(0, 0);
            this.cbo조회기간.Name = "cbo조회기간";
            this.cbo조회기간.Size = new System.Drawing.Size(103, 20);
            this.cbo조회기간.TabIndex = 0;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.flowLayoutPanel1);
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.Controls.Add(this.bpPanelControl3);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(1074, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.chk전표승인);
            this.flowLayoutPanel1.Controls.Add(this.chk매출계정만표시);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(594, 1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(294, 23);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // chk전표승인
            // 
            this.chk전표승인.AutoSize = true;
            this.chk전표승인.Location = new System.Drawing.Point(3, 3);
            this.chk전표승인.Name = "chk전표승인";
            this.chk전표승인.Size = new System.Drawing.Size(72, 16);
            this.chk전표승인.TabIndex = 0;
            this.chk전표승인.Text = "전표승인";
            this.chk전표승인.TextDD = null;
            this.chk전표승인.UseVisualStyleBackColor = true;
            // 
            // chk매출계정만표시
            // 
            this.chk매출계정만표시.AutoSize = true;
            this.chk매출계정만표시.Location = new System.Drawing.Point(81, 3);
            this.chk매출계정만표시.Name = "chk매출계정만표시";
            this.chk매출계정만표시.Size = new System.Drawing.Size(108, 16);
            this.chk매출계정만표시.TabIndex = 1;
            this.chk매출계정만표시.Text = "매출계정만표시";
            this.chk매출계정만표시.TextDD = null;
            this.chk매출계정만표시.UseVisualStyleBackColor = true;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.txt계산서번호);
            this.bpPanelControl4.Controls.Add(this.lbl계산서번호);
            this.bpPanelControl4.Location = new System.Drawing.Point(298, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(294, 23);
            this.bpPanelControl4.TabIndex = 4;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // txt계산서번호
            // 
            this.txt계산서번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt계산서번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt계산서번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt계산서번호.Location = new System.Drawing.Point(106, 0);
            this.txt계산서번호.Name = "txt계산서번호";
            this.txt계산서번호.Size = new System.Drawing.Size(188, 21);
            this.txt계산서번호.TabIndex = 1;
            // 
            // lbl계산서번호
            // 
            this.lbl계산서번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl계산서번호.Location = new System.Drawing.Point(0, 0);
            this.lbl계산서번호.Name = "lbl계산서번호";
            this.lbl계산서번호.Size = new System.Drawing.Size(100, 23);
            this.lbl계산서번호.TabIndex = 0;
            this.lbl계산서번호.Text = "계산서번호";
            this.lbl계산서번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.txt프로젝트);
            this.bpPanelControl3.Controls.Add(this.lbl프로젝트);
            this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(294, 23);
            this.bpPanelControl3.TabIndex = 3;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // txt프로젝트
            // 
            this.txt프로젝트.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt프로젝트.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt프로젝트.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt프로젝트.Location = new System.Drawing.Point(109, 0);
            this.txt프로젝트.Name = "txt프로젝트";
            this.txt프로젝트.Size = new System.Drawing.Size(185, 21);
            this.txt프로젝트.TabIndex = 1;
            // 
            // lbl프로젝트
            // 
            this.lbl프로젝트.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl프로젝트.Location = new System.Drawing.Point(0, 0);
            this.lbl프로젝트.Name = "lbl프로젝트";
            this.lbl프로젝트.Size = new System.Drawing.Size(100, 23);
            this.lbl프로젝트.TabIndex = 0;
            this.lbl프로젝트.Text = "프로젝트";
            this.lbl프로젝트.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this._flexH.Location = new System.Drawing.Point(3, 72);
            this._flexH.Name = "_flexH";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 20;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(1084, 681);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 2;
            this._flexH.UseGridCalculator = true;
            // 
            // P_CZ_SA_IV_BASE
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_CZ_SA_IV_BASE";
            this.Size = new System.Drawing.Size(1090, 796);
            this.TitleText = "매출원가분석";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl4.PerformLayout();
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.PeriodPicker dtp조회기간;
        private Duzon.Common.Controls.DropDownComboBox cbo조회기간;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpCodeTextBox ctxCC;
        private Duzon.Common.Controls.LabelExt lblCC;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.BpControls.BpCodeTextBox ctx매출처;
        private Duzon.Common.Controls.LabelExt lbl매출처;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.TextBoxExt txt계산서번호;
        private Duzon.Common.Controls.LabelExt lbl계산서번호;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.TextBoxExt txt프로젝트;
        private Duzon.Common.Controls.LabelExt lbl프로젝트;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.CheckBoxExt chk전표승인;
        private Duzon.Common.Controls.CheckBoxExt chk매출계정만표시;
        private Dass.FlexGrid.FlexGrid _flexH;
    }
}
namespace cz
{
    partial class P_CZ_HR_PHONE_PAY_REG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_HR_PHONE_PAY_REG));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbx사원 = new Duzon.Common.BpControls.BpComboBox();
            this.labelExt2 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbx부서 = new Duzon.Common.BpControls.BpComboBox();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp급여반영월 = new Duzon.Common.Controls.DatePicker();
            this.labelExt4 = new Duzon.Common.Controls.LabelExt();
            this.btn이월복사 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn초기화 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp급여반영월)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Dock = System.Windows.Forms.DockStyle.None;
            this.mDataArea.Size = new System.Drawing.Size(1276, 706);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 504F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1276, 706);
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
            this._flex.Location = new System.Drawing.Point(3, 52);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(1270, 651);
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
            this.oneGrid1.Size = new System.Drawing.Size(1270, 43);
            this.oneGrid1.TabIndex = 2;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.Controls.Add(this.bpPanelControl3);
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(1260, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.cbx사원);
            this.bpPanelControl1.Controls.Add(this.labelExt2);
            this.bpPanelControl1.Location = new System.Drawing.Point(408, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(201, 23);
            this.bpPanelControl1.TabIndex = 19;
            this.bpPanelControl1.Text = "bpPanelControl6";
            // 
            // cbx사원
            // 
            this.cbx사원.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB1;
            this.cbx사원.Location = new System.Drawing.Point(84, 1);
            this.cbx사원.Name = "cbx사원";
            this.cbx사원.Size = new System.Drawing.Size(117, 21);
            this.cbx사원.TabIndex = 4;
            this.cbx사원.TabStop = false;
            this.cbx사원.Text = "bpComboBox1";
            // 
            // labelExt2
            // 
            this.labelExt2.Location = new System.Drawing.Point(17, 4);
            this.labelExt2.Name = "labelExt2";
            this.labelExt2.Size = new System.Drawing.Size(65, 16);
            this.labelExt2.TabIndex = 3;
            this.labelExt2.Text = "사원";
            this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.cbx부서);
            this.bpPanelControl3.Controls.Add(this.labelExt1);
            this.bpPanelControl3.Location = new System.Drawing.Point(205, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(201, 23);
            this.bpPanelControl3.TabIndex = 20;
            this.bpPanelControl3.Text = "bpPanelControl5";
            // 
            // cbx부서
            // 
            this.cbx부서.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CC_SUB1;
            this.cbx부서.Location = new System.Drawing.Point(84, 1);
            this.cbx부서.Name = "cbx부서";
            this.cbx부서.Size = new System.Drawing.Size(117, 21);
            this.cbx부서.TabIndex = 5;
            this.cbx부서.TabStop = false;
            this.cbx부서.Text = "bpComboBox1";
            // 
            // labelExt1
            // 
            this.labelExt1.Location = new System.Drawing.Point(17, 4);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Size = new System.Drawing.Size(65, 16);
            this.labelExt1.TabIndex = 1;
            this.labelExt1.Text = "코스트센터";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.dtp급여반영월);
            this.bpPanelControl2.Controls.Add(this.labelExt4);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(201, 23);
            this.bpPanelControl2.TabIndex = 18;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // dtp급여반영월
            // 
            this.dtp급여반영월.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp급여반영월.Location = new System.Drawing.Point(84, 1);
            this.dtp급여반영월.Mask = "####/##";
            this.dtp급여반영월.MaskBackColor = System.Drawing.Color.White;
            this.dtp급여반영월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp급여반영월.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp급여반영월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp급여반영월.Modified = true;
            this.dtp급여반영월.Name = "dtp급여반영월";
            this.dtp급여반영월.ShowUpDown = true;
            this.dtp급여반영월.Size = new System.Drawing.Size(70, 21);
            this.dtp급여반영월.TabIndex = 3;
            this.dtp급여반영월.Tag = "DT_START";
            this.dtp급여반영월.Value = new System.DateTime(((long)(0)));
            // 
            // labelExt4
            // 
            this.labelExt4.Location = new System.Drawing.Point(17, 4);
            this.labelExt4.Name = "labelExt4";
            this.labelExt4.Size = new System.Drawing.Size(65, 16);
            this.labelExt4.TabIndex = 1;
            this.labelExt4.Text = "지급반영월";
            this.labelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn이월복사
            // 
            this.btn이월복사.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn이월복사.BackColor = System.Drawing.Color.White;
            this.btn이월복사.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn이월복사.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn이월복사.Location = new System.Drawing.Point(1177, 10);
            this.btn이월복사.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn이월복사.Name = "btn이월복사";
            this.btn이월복사.Size = new System.Drawing.Size(94, 19);
            this.btn이월복사.TabIndex = 21;
            this.btn이월복사.TabStop = false;
            this.btn이월복사.Text = "전월복사";
            this.btn이월복사.UseVisualStyleBackColor = false;
            // 
            // btn초기화
            // 
            this.btn초기화.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn초기화.BackColor = System.Drawing.Color.White;
            this.btn초기화.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn초기화.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn초기화.Location = new System.Drawing.Point(1103, 10);
            this.btn초기화.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn초기화.Name = "btn초기화";
            this.btn초기화.Size = new System.Drawing.Size(70, 19);
            this.btn초기화.TabIndex = 21;
            this.btn초기화.TabStop = false;
            this.btn초기화.Text = "초기화";
            this.btn초기화.UseVisualStyleBackColor = false;
            // 
            // P_CZ_HR_PHONE_PAY_REG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btn초기화);
            this.Controls.Add(this.btn이월복사);
            this.Name = "P_CZ_HR_PHONE_PAY_REG";
            this.Size = new System.Drawing.Size(1276, 746);
            this.TitleText = "휴대폰 사용자 등록";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.btn이월복사, 0);
            this.Controls.SetChildIndex(this.btn초기화, 0);
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp급여반영월)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.BpControls.BpComboBox cbx사원;
        private Duzon.Common.Controls.LabelExt labelExt2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.DatePicker dtp급여반영월;
        private Duzon.Common.Controls.LabelExt labelExt4;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpComboBox cbx부서;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.RoundedButton btn이월복사;
        private Duzon.Common.Controls.RoundedButton btn초기화;
    }
}
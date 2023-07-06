namespace cz
{
    partial class P_CZ_PU_ADPAYMENT_SCH
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PU_ADPAYMENT_SCH));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo거래구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl거래구분 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo지급형태 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl지급형태 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp지급일자 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl지급일자 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl담당자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx구매그룹 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl구매그룹 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx매입처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl매입처 = new Duzon.Common.Controls.LabelExt();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn닫기 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 182F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(898, 414);
            this.tableLayoutPanel1.TabIndex = 0;
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
            this._flex.Location = new System.Drawing.Point(3, 103);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(892, 308);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 0;
            this._flex.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(892, 62);
            this.oneGrid1.TabIndex = 1;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl5);
            this.oneGridItem1.Controls.Add(this.bpPanelControl4);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(882, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.cbo거래구분);
            this.bpPanelControl5.Controls.Add(this.lbl거래구분);
            this.bpPanelControl5.Location = new System.Drawing.Point(588, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(291, 22);
            this.bpPanelControl5.TabIndex = 5;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // cbo거래구분
            // 
            this.cbo거래구분.AutoDropDown = true;
            this.cbo거래구분.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo거래구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo거래구분.Enabled = false;
            this.cbo거래구분.FormattingEnabled = true;
            this.cbo거래구분.ItemHeight = 12;
            this.cbo거래구분.Location = new System.Drawing.Point(106, 0);
            this.cbo거래구분.Name = "cbo거래구분";
            this.cbo거래구분.Size = new System.Drawing.Size(185, 20);
            this.cbo거래구분.TabIndex = 1;
            // 
            // lbl거래구분
            // 
            this.lbl거래구분.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl거래구분.Location = new System.Drawing.Point(0, 0);
            this.lbl거래구분.Name = "lbl거래구분";
            this.lbl거래구분.Size = new System.Drawing.Size(100, 22);
            this.lbl거래구분.TabIndex = 0;
            this.lbl거래구분.Text = "거래구분";
            this.lbl거래구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.cbo지급형태);
            this.bpPanelControl4.Controls.Add(this.lbl지급형태);
            this.bpPanelControl4.Location = new System.Drawing.Point(295, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(291, 22);
            this.bpPanelControl4.TabIndex = 4;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // cbo지급형태
            // 
            this.cbo지급형태.AutoDropDown = true;
            this.cbo지급형태.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo지급형태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo지급형태.Enabled = false;
            this.cbo지급형태.FormattingEnabled = true;
            this.cbo지급형태.ItemHeight = 12;
            this.cbo지급형태.Location = new System.Drawing.Point(106, 0);
            this.cbo지급형태.Name = "cbo지급형태";
            this.cbo지급형태.Size = new System.Drawing.Size(185, 20);
            this.cbo지급형태.TabIndex = 1;
            // 
            // lbl지급형태
            // 
            this.lbl지급형태.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl지급형태.Location = new System.Drawing.Point(0, 0);
            this.lbl지급형태.Name = "lbl지급형태";
            this.lbl지급형태.Size = new System.Drawing.Size(100, 22);
            this.lbl지급형태.TabIndex = 0;
            this.lbl지급형태.Text = "지급형태";
            this.lbl지급형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp지급일자);
            this.bpPanelControl1.Controls.Add(this.lbl지급일자);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(291, 22);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // dtp지급일자
            // 
            this.dtp지급일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp지급일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp지급일자.IsNecessaryCondition = true;
            this.dtp지급일자.Location = new System.Drawing.Point(106, 0);
            this.dtp지급일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp지급일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp지급일자.Name = "dtp지급일자";
            this.dtp지급일자.Size = new System.Drawing.Size(185, 21);
            this.dtp지급일자.TabIndex = 1;
            // 
            // lbl지급일자
            // 
            this.lbl지급일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl지급일자.Location = new System.Drawing.Point(0, 0);
            this.lbl지급일자.Name = "lbl지급일자";
            this.lbl지급일자.Size = new System.Drawing.Size(100, 22);
            this.lbl지급일자.TabIndex = 0;
            this.lbl지급일자.Text = "지급일자";
            this.lbl지급일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.Controls.Add(this.bpPanelControl3);
            this.oneGridItem2.Controls.Add(this.bpPanelControl2);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(882, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.ctx담당자);
            this.bpPanelControl6.Controls.Add(this.lbl담당자);
            this.bpPanelControl6.Location = new System.Drawing.Point(588, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(291, 22);
            this.bpPanelControl6.TabIndex = 4;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // ctx담당자
            // 
            this.ctx담당자.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.ctx담당자.Location = new System.Drawing.Point(106, 0);
            this.ctx담당자.Name = "ctx담당자";
            this.ctx담당자.Size = new System.Drawing.Size(185, 21);
            this.ctx담당자.TabIndex = 1;
            this.ctx담당자.TabStop = false;
            this.ctx담당자.Text = "bpCodeTextBox1";
            // 
            // lbl담당자
            // 
            this.lbl담당자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl담당자.Location = new System.Drawing.Point(0, 0);
            this.lbl담당자.Name = "lbl담당자";
            this.lbl담당자.Size = new System.Drawing.Size(100, 22);
            this.lbl담당자.TabIndex = 0;
            this.lbl담당자.Text = "담당자";
            this.lbl담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.ctx구매그룹);
            this.bpPanelControl3.Controls.Add(this.lbl구매그룹);
            this.bpPanelControl3.Location = new System.Drawing.Point(295, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(291, 22);
            this.bpPanelControl3.TabIndex = 3;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // ctx구매그룹
            // 
            this.ctx구매그룹.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx구매그룹.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PURGRP_SUB;
            this.ctx구매그룹.Location = new System.Drawing.Point(106, 0);
            this.ctx구매그룹.Name = "ctx구매그룹";
            this.ctx구매그룹.Size = new System.Drawing.Size(185, 21);
            this.ctx구매그룹.TabIndex = 1;
            this.ctx구매그룹.TabStop = false;
            this.ctx구매그룹.Text = "bpCodeTextBox1";
            // 
            // lbl구매그룹
            // 
            this.lbl구매그룹.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl구매그룹.Location = new System.Drawing.Point(0, 0);
            this.lbl구매그룹.Name = "lbl구매그룹";
            this.lbl구매그룹.Size = new System.Drawing.Size(100, 22);
            this.lbl구매그룹.TabIndex = 0;
            this.lbl구매그룹.Text = "구매그룹";
            this.lbl구매그룹.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.ctx매입처);
            this.bpPanelControl2.Controls.Add(this.lbl매입처);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(291, 22);
            this.bpPanelControl2.TabIndex = 2;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // ctx매입처
            // 
            this.ctx매입처.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx매입처.Enabled = false;
            this.ctx매입처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx매입처.Location = new System.Drawing.Point(106, 0);
            this.ctx매입처.Name = "ctx매입처";
            this.ctx매입처.Size = new System.Drawing.Size(185, 21);
            this.ctx매입처.TabIndex = 1;
            this.ctx매입처.TabStop = false;
            this.ctx매입처.Text = "bpCodeTextBox1";
            // 
            // lbl매입처
            // 
            this.lbl매입처.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl매입처.Location = new System.Drawing.Point(0, 0);
            this.lbl매입처.Name = "lbl매입처";
            this.lbl매입처.Size = new System.Drawing.Size(100, 22);
            this.lbl매입처.TabIndex = 0;
            this.lbl매입처.Text = "매입처";
            this.lbl매입처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn닫기);
            this.flowLayoutPanel1.Controls.Add(this.btn확인);
            this.flowLayoutPanel1.Controls.Add(this.btn조회);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 71);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(892, 26);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btn닫기
            // 
            this.btn닫기.BackColor = System.Drawing.Color.Transparent;
            this.btn닫기.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn닫기.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn닫기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn닫기.Location = new System.Drawing.Point(819, 3);
            this.btn닫기.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn닫기.Name = "btn닫기";
            this.btn닫기.Size = new System.Drawing.Size(70, 19);
            this.btn닫기.TabIndex = 0;
            this.btn닫기.TabStop = false;
            this.btn닫기.Text = "닫기";
            this.btn닫기.UseVisualStyleBackColor = false;
            // 
            // btn확인
            // 
            this.btn확인.BackColor = System.Drawing.Color.Transparent;
            this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn확인.Location = new System.Drawing.Point(743, 3);
            this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn확인.Name = "btn확인";
            this.btn확인.Size = new System.Drawing.Size(70, 19);
            this.btn확인.TabIndex = 1;
            this.btn확인.TabStop = false;
            this.btn확인.Text = "확인";
            this.btn확인.UseVisualStyleBackColor = false;
            // 
            // btn조회
            // 
            this.btn조회.BackColor = System.Drawing.Color.Transparent;
            this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn조회.Location = new System.Drawing.Point(667, 3);
            this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn조회.Name = "btn조회";
            this.btn조회.Size = new System.Drawing.Size(70, 19);
            this.btn조회.TabIndex = 2;
            this.btn조회.TabStop = false;
            this.btn조회.Text = "조회";
            this.btn조회.UseVisualStyleBackColor = false;
            // 
            // P_CZ_PU_ADPAYMENT_SCH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn닫기;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(900, 463);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_CZ_PU_ADPAYMENT_SCH";
            this.Text = "더존 ERP iU";
            this.TitleText = "선지급현황";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn닫기;
        private Duzon.Common.Controls.RoundedButton btn확인;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.Controls.DropDownComboBox cbo거래구분;
        private Duzon.Common.Controls.LabelExt lbl거래구분;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.DropDownComboBox cbo지급형태;
        private Duzon.Common.Controls.LabelExt lbl지급형태;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.PeriodPicker dtp지급일자;
        private Duzon.Common.Controls.LabelExt lbl지급일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.BpControls.BpCodeTextBox ctx담당자;
        private Duzon.Common.Controls.LabelExt lbl담당자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpCodeTextBox ctx구매그룹;
        private Duzon.Common.Controls.LabelExt lbl구매그룹;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpCodeTextBox ctx매입처;
        private Duzon.Common.Controls.LabelExt lbl매입처;
    }
}
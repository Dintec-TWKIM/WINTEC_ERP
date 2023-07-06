namespace cz
{
    partial class P_CZ_PR_MATCHING_DEACTIVATE_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_MATCHING_DEACTIVATE_SUB));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn폐기 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn분실 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn수정 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn저장 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn닫기 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl공장 = new Duzon.Common.Controls.LabelExt();
            this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo상태 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl상태 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl품목 = new Duzon.Common.Controls.LabelExt();
            this.ctx품목 = new Duzon.Common.BpControls.BpCodeTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(936, 642);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn닫기);
            this.flowLayoutPanel1.Controls.Add(this.btn저장);
            this.flowLayoutPanel1.Controls.Add(this.btn삭제);
            this.flowLayoutPanel1.Controls.Add(this.btn조회);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 50);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(930, 26);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btn폐기
            // 
            this.btn폐기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn폐기.BackColor = System.Drawing.Color.Transparent;
            this.btn폐기.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn폐기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn폐기.Location = new System.Drawing.Point(863, 23);
            this.btn폐기.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn폐기.Name = "btn폐기";
            this.btn폐기.Size = new System.Drawing.Size(70, 19);
            this.btn폐기.TabIndex = 2;
            this.btn폐기.TabStop = false;
            this.btn폐기.Text = "폐기";
            this.btn폐기.UseVisualStyleBackColor = false;
            // 
            // btn분실
            // 
            this.btn분실.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn분실.BackColor = System.Drawing.Color.Transparent;
            this.btn분실.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn분실.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn분실.Location = new System.Drawing.Point(787, 23);
            this.btn분실.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn분실.Name = "btn분실";
            this.btn분실.Size = new System.Drawing.Size(70, 19);
            this.btn분실.TabIndex = 3;
            this.btn분실.TabStop = false;
            this.btn분실.Text = "분실";
            this.btn분실.UseVisualStyleBackColor = false;
            // 
            // btn수정
            // 
            this.btn수정.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn수정.BackColor = System.Drawing.Color.Transparent;
            this.btn수정.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn수정.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn수정.Location = new System.Drawing.Point(711, 23);
            this.btn수정.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn수정.Name = "btn수정";
            this.btn수정.Size = new System.Drawing.Size(70, 19);
            this.btn수정.TabIndex = 1;
            this.btn수정.TabStop = false;
            this.btn수정.Text = "수정";
            this.btn수정.UseVisualStyleBackColor = false;
            // 
            // btn저장
            // 
            this.btn저장.BackColor = System.Drawing.Color.Transparent;
            this.btn저장.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn저장.Location = new System.Drawing.Point(781, 3);
            this.btn저장.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn저장.Name = "btn저장";
            this.btn저장.Size = new System.Drawing.Size(70, 19);
            this.btn저장.TabIndex = 4;
            this.btn저장.TabStop = false;
            this.btn저장.Text = "저장";
            this.btn저장.UseVisualStyleBackColor = false;
            // 
            // btn조회
            // 
            this.btn조회.BackColor = System.Drawing.Color.Transparent;
            this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn조회.Location = new System.Drawing.Point(629, 3);
            this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn조회.Name = "btn조회";
            this.btn조회.Size = new System.Drawing.Size(70, 19);
            this.btn조회.TabIndex = 0;
            this.btn조회.TabStop = false;
            this.btn조회.Text = "조회";
            this.btn조회.UseVisualStyleBackColor = false;
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
            this._flex.Location = new System.Drawing.Point(3, 82);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(930, 557);
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
            this.oneGrid1.Size = new System.Drawing.Size(930, 41);
            this.oneGrid1.TabIndex = 2;
            // 
            // btn삭제
            // 
            this.btn삭제.BackColor = System.Drawing.Color.Transparent;
            this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn삭제.Location = new System.Drawing.Point(705, 3);
            this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn삭제.Name = "btn삭제";
            this.btn삭제.Size = new System.Drawing.Size(70, 19);
            this.btn삭제.TabIndex = 5;
            this.btn삭제.TabStop = false;
            this.btn삭제.Text = "삭제";
            this.btn삭제.UseVisualStyleBackColor = false;
            // 
            // btn닫기
            // 
            this.btn닫기.BackColor = System.Drawing.Color.Transparent;
            this.btn닫기.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn닫기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn닫기.Location = new System.Drawing.Point(857, 3);
            this.btn닫기.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn닫기.Name = "btn닫기";
            this.btn닫기.Size = new System.Drawing.Size(70, 19);
            this.btn닫기.TabIndex = 6;
            this.btn닫기.TabStop = false;
            this.btn닫기.Text = "닫기";
            this.btn닫기.UseVisualStyleBackColor = false;
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
            this.oneGridItem1.Size = new System.Drawing.Size(920, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.cbo공장);
            this.bpPanelControl1.Controls.Add(this.lbl공장);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // lbl공장
            // 
            this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl공장.Location = new System.Drawing.Point(0, 0);
            this.lbl공장.Name = "lbl공장";
            this.lbl공장.Size = new System.Drawing.Size(100, 23);
            this.lbl공장.TabIndex = 4;
            this.lbl공장.Text = "공장";
            this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo공장
            // 
            this.cbo공장.AutoDropDown = true;
            this.cbo공장.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo공장.FormattingEnabled = true;
            this.cbo공장.ItemHeight = 12;
            this.cbo공장.Location = new System.Drawing.Point(106, 0);
            this.cbo공장.Name = "cbo공장";
            this.cbo공장.Size = new System.Drawing.Size(186, 20);
            this.cbo공장.TabIndex = 4;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.cbo상태);
            this.bpPanelControl2.Controls.Add(this.lbl상태);
            this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // cbo상태
            // 
            this.cbo상태.AutoDropDown = true;
            this.cbo상태.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo상태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo상태.FormattingEnabled = true;
            this.cbo상태.ItemHeight = 12;
            this.cbo상태.Location = new System.Drawing.Point(106, 0);
            this.cbo상태.Name = "cbo상태";
            this.cbo상태.Size = new System.Drawing.Size(186, 20);
            this.cbo상태.TabIndex = 4;
            // 
            // lbl상태
            // 
            this.lbl상태.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl상태.Location = new System.Drawing.Point(0, 0);
            this.lbl상태.Name = "lbl상태";
            this.lbl상태.Size = new System.Drawing.Size(100, 23);
            this.lbl상태.TabIndex = 4;
            this.lbl상태.Text = "상태";
            this.lbl상태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.ctx품목);
            this.bpPanelControl3.Controls.Add(this.lbl품목);
            this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // lbl품목
            // 
            this.lbl품목.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl품목.Location = new System.Drawing.Point(0, 0);
            this.lbl품목.Name = "lbl품목";
            this.lbl품목.Size = new System.Drawing.Size(100, 23);
            this.lbl품목.TabIndex = 4;
            this.lbl품목.Text = "품목";
            this.lbl품목.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx품목
            // 
            this.ctx품목.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx품목.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.ctx품목.Location = new System.Drawing.Point(106, 0);
            this.ctx품목.Name = "ctx품목";
            this.ctx품목.Size = new System.Drawing.Size(186, 21);
            this.ctx품목.TabIndex = 5;
            this.ctx품목.TabStop = false;
            this.ctx품목.Text = "bpCodeTextBox1";
            // 
            // P_CZ_PR_MATCHING_DEACTIVATE_SUB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(938, 691);
            this.Controls.Add(this.btn폐기);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btn분실);
            this.Controls.Add(this.btn수정);
            this.Name = "P_CZ_PR_MATCHING_DEACTIVATE_SUB";
            this.Text = "ERP iU";
            this.TitleText = "해체리스트";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Common.Controls.RoundedButton btn폐기;
        private Duzon.Common.Controls.RoundedButton btn분실;
        private Duzon.Common.Controls.RoundedButton btn수정;
        private Duzon.Common.Controls.RoundedButton btn저장;
        private Duzon.Common.Controls.RoundedButton btn닫기;
        private Duzon.Common.Controls.RoundedButton btn삭제;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.DropDownComboBox cbo공장;
        private Duzon.Common.Controls.LabelExt lbl공장;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.DropDownComboBox cbo상태;
        private Duzon.Common.Controls.LabelExt lbl상태;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpCodeTextBox ctx품목;
        private Duzon.Common.Controls.LabelExt lbl품목;
    }
}
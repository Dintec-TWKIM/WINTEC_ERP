namespace cz
{
    partial class P_CZ_PR_MATCHING_ITEM_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_MATCHING_ITEM_SUB));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx완성품목 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl완성품목 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl공장 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx우측품목 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl우측품목 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx중간품목 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl중간품목 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx좌측품목 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl좌측품목 = new Duzon.Common.Controls.LabelExt();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn닫기 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn저장 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 46);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(933, 493);
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
            this.oneGrid1.Size = new System.Drawing.Size(927, 62);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.Controls.Add(this.bpPanelControl5);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(917, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.ctx완성품목);
            this.bpPanelControl1.Controls.Add(this.lbl완성품목);
            this.bpPanelControl1.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 3;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // ctx완성품목
            // 
            this.ctx완성품목.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx완성품목.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.ctx완성품목.ItemBackColor = System.Drawing.Color.Empty;
            this.ctx완성품목.Location = new System.Drawing.Point(106, 0);
            this.ctx완성품목.Name = "ctx완성품목";
            this.ctx완성품목.Size = new System.Drawing.Size(186, 21);
            this.ctx완성품목.TabIndex = 1;
            this.ctx완성품목.TabStop = false;
            this.ctx완성품목.Text = "bpCodeTextBox1";
            // 
            // lbl완성품목
            // 
            this.lbl완성품목.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl완성품목.Location = new System.Drawing.Point(0, 0);
            this.lbl완성품목.Name = "lbl완성품목";
            this.lbl완성품목.Size = new System.Drawing.Size(100, 23);
            this.lbl완성품목.TabIndex = 0;
            this.lbl완성품목.Text = "완성품목";
            this.lbl완성품목.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.cbo공장);
            this.bpPanelControl5.Controls.Add(this.lbl공장);
            this.bpPanelControl5.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl5.TabIndex = 2;
            this.bpPanelControl5.Text = "bpPanelControl5";
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
            this.cbo공장.TabIndex = 1;
            // 
            // lbl공장
            // 
            this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl공장.Location = new System.Drawing.Point(0, 0);
            this.lbl공장.Name = "lbl공장";
            this.lbl공장.Size = new System.Drawing.Size(100, 23);
            this.lbl공장.TabIndex = 0;
            this.lbl공장.Text = "공장";
            this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.Controls.Add(this.bpPanelControl3);
            this.oneGridItem2.Controls.Add(this.bpPanelControl2);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(917, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.ctx우측품목);
            this.bpPanelControl4.Controls.Add(this.lbl우측품목);
            this.bpPanelControl4.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl4.TabIndex = 6;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // ctx우측품목
            // 
            this.ctx우측품목.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx우측품목.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.ctx우측품목.ItemBackColor = System.Drawing.Color.Empty;
            this.ctx우측품목.Location = new System.Drawing.Point(106, 0);
            this.ctx우측품목.Name = "ctx우측품목";
            this.ctx우측품목.Size = new System.Drawing.Size(186, 21);
            this.ctx우측품목.TabIndex = 1;
            this.ctx우측품목.TabStop = false;
            this.ctx우측품목.Text = "bpCodeTextBox4";
            // 
            // lbl우측품목
            // 
            this.lbl우측품목.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl우측품목.Location = new System.Drawing.Point(0, 0);
            this.lbl우측품목.Name = "lbl우측품목";
            this.lbl우측품목.Size = new System.Drawing.Size(100, 23);
            this.lbl우측품목.TabIndex = 0;
            this.lbl우측품목.Text = "우측품목";
            this.lbl우측품목.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.ctx중간품목);
            this.bpPanelControl3.Controls.Add(this.lbl중간품목);
            this.bpPanelControl3.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 5;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // ctx중간품목
            // 
            this.ctx중간품목.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx중간품목.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.ctx중간품목.ItemBackColor = System.Drawing.Color.Empty;
            this.ctx중간품목.Location = new System.Drawing.Point(106, 0);
            this.ctx중간품목.Name = "ctx중간품목";
            this.ctx중간품목.Size = new System.Drawing.Size(186, 21);
            this.ctx중간품목.TabIndex = 1;
            this.ctx중간품목.TabStop = false;
            this.ctx중간품목.Text = "bpCodeTextBox3";
            // 
            // lbl중간품목
            // 
            this.lbl중간품목.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl중간품목.Location = new System.Drawing.Point(0, 0);
            this.lbl중간품목.Name = "lbl중간품목";
            this.lbl중간품목.Size = new System.Drawing.Size(100, 23);
            this.lbl중간품목.TabIndex = 0;
            this.lbl중간품목.Text = "중간품목";
            this.lbl중간품목.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.ctx좌측품목);
            this.bpPanelControl2.Controls.Add(this.lbl좌측품목);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 4;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // ctx좌측품목
            // 
            this.ctx좌측품목.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx좌측품목.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.ctx좌측품목.ItemBackColor = System.Drawing.Color.Empty;
            this.ctx좌측품목.Location = new System.Drawing.Point(106, 0);
            this.ctx좌측품목.Name = "ctx좌측품목";
            this.ctx좌측품목.Size = new System.Drawing.Size(186, 21);
            this.ctx좌측품목.TabIndex = 1;
            this.ctx좌측품목.TabStop = false;
            this.ctx좌측품목.Text = "bpCodeTextBox2";
            // 
            // lbl좌측품목
            // 
            this.lbl좌측품목.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl좌측품목.Location = new System.Drawing.Point(0, 0);
            this.lbl좌측품목.Name = "lbl좌측품목";
            this.lbl좌측품목.Size = new System.Drawing.Size(100, 23);
            this.lbl좌측품목.TabIndex = 0;
            this.lbl좌측품목.Text = "좌측품목";
            this.lbl좌측품목.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn닫기);
            this.flowLayoutPanel1.Controls.Add(this.btn저장);
            this.flowLayoutPanel1.Controls.Add(this.btn삭제);
            this.flowLayoutPanel1.Controls.Add(this.btn추가);
            this.flowLayoutPanel1.Controls.Add(this.btn조회);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 71);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(927, 26);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btn닫기
            // 
            this.btn닫기.BackColor = System.Drawing.Color.Transparent;
            this.btn닫기.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn닫기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn닫기.Location = new System.Drawing.Point(854, 3);
            this.btn닫기.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn닫기.Name = "btn닫기";
            this.btn닫기.Size = new System.Drawing.Size(70, 19);
            this.btn닫기.TabIndex = 0;
            this.btn닫기.TabStop = false;
            this.btn닫기.Text = "닫기";
            this.btn닫기.UseVisualStyleBackColor = false;
            // 
            // btn저장
            // 
            this.btn저장.BackColor = System.Drawing.Color.Transparent;
            this.btn저장.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn저장.Location = new System.Drawing.Point(778, 3);
            this.btn저장.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn저장.Name = "btn저장";
            this.btn저장.Size = new System.Drawing.Size(70, 19);
            this.btn저장.TabIndex = 1;
            this.btn저장.TabStop = false;
            this.btn저장.Text = "저장";
            this.btn저장.UseVisualStyleBackColor = false;
            // 
            // btn삭제
            // 
            this.btn삭제.BackColor = System.Drawing.Color.Transparent;
            this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn삭제.Location = new System.Drawing.Point(702, 3);
            this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn삭제.Name = "btn삭제";
            this.btn삭제.Size = new System.Drawing.Size(70, 19);
            this.btn삭제.TabIndex = 3;
            this.btn삭제.TabStop = false;
            this.btn삭제.Text = "삭제";
            this.btn삭제.UseVisualStyleBackColor = false;
            // 
            // btn추가
            // 
            this.btn추가.BackColor = System.Drawing.Color.Transparent;
            this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn추가.Location = new System.Drawing.Point(626, 3);
            this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn추가.Name = "btn추가";
            this.btn추가.Size = new System.Drawing.Size(70, 19);
            this.btn추가.TabIndex = 4;
            this.btn추가.TabStop = false;
            this.btn추가.Text = "추가";
            this.btn추가.UseVisualStyleBackColor = false;
            // 
            // btn조회
            // 
            this.btn조회.BackColor = System.Drawing.Color.Transparent;
            this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn조회.Location = new System.Drawing.Point(550, 3);
            this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn조회.Name = "btn조회";
            this.btn조회.Size = new System.Drawing.Size(70, 19);
            this.btn조회.TabIndex = 2;
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
            this._flex.Location = new System.Drawing.Point(3, 103);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(927, 387);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 2;
            this._flex.UseGridCalculator = true;
            // 
            // P_CZ_PR_MATCHING_ITEM_SUB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(933, 539);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_CZ_PR_MATCHING_ITEM_SUB";
            this.Text = "ERP iU";
            this.TitleText = "현합품목등록";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn닫기;
        private Duzon.Common.Controls.RoundedButton btn저장;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Duzon.Common.Controls.RoundedButton btn삭제;
        private Duzon.Common.Controls.RoundedButton btn추가;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.Controls.DropDownComboBox cbo공장;
        private Duzon.Common.Controls.LabelExt lbl공장;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.BpControls.BpCodeTextBox ctx완성품목;
        private Duzon.Common.Controls.LabelExt lbl완성품목;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpCodeTextBox ctx우측품목;
        private Duzon.Common.Controls.LabelExt lbl우측품목;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpCodeTextBox ctx중간품목;
        private Duzon.Common.Controls.LabelExt lbl중간품목;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpCodeTextBox ctx좌측품목;
        private Duzon.Common.Controls.LabelExt lbl좌측품목;
    }
}
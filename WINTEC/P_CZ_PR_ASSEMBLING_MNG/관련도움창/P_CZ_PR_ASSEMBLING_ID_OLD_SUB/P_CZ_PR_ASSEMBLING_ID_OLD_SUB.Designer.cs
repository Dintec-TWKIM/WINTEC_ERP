namespace cz
{
    partial class P_CZ_PR_ASSEMBLING_ID_OLD_SUB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_ASSEMBLING_ID_OLD_SUB));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn닫기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt가공ID = new Duzon.Common.Controls.TextBoxExt();
			this.lbl가공ID = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx품목코드 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl품목코드 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사용여부 = new Duzon.Common.Controls.LabelExt();
			this.cbo사용여부 = new Duzon.Common.Controls.DropDownComboBox();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
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
			this.tableLayoutPanel1.Size = new System.Drawing.Size(902, 642);
			this.tableLayoutPanel1.TabIndex = 0;
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
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 50);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(896, 26);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// btn닫기
			// 
			this.btn닫기.BackColor = System.Drawing.Color.Transparent;
			this.btn닫기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn닫기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn닫기.Location = new System.Drawing.Point(823, 3);
			this.btn닫기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn닫기.Name = "btn닫기";
			this.btn닫기.Size = new System.Drawing.Size(70, 19);
			this.btn닫기.TabIndex = 6;
			this.btn닫기.TabStop = false;
			this.btn닫기.Text = "닫기";
			this.btn닫기.UseVisualStyleBackColor = false;
			// 
			// btn저장
			// 
			this.btn저장.BackColor = System.Drawing.Color.Transparent;
			this.btn저장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn저장.Location = new System.Drawing.Point(747, 3);
			this.btn저장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn저장.Name = "btn저장";
			this.btn저장.Size = new System.Drawing.Size(70, 19);
			this.btn저장.TabIndex = 4;
			this.btn저장.TabStop = false;
			this.btn저장.Text = "저장";
			this.btn저장.UseVisualStyleBackColor = false;
			// 
			// btn삭제
			// 
			this.btn삭제.BackColor = System.Drawing.Color.Transparent;
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(671, 3);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(70, 19);
			this.btn삭제.TabIndex = 5;
			this.btn삭제.TabStop = false;
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// btn추가
			// 
			this.btn추가.BackColor = System.Drawing.Color.Transparent;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.Location = new System.Drawing.Point(595, 3);
			this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn추가.Name = "btn추가";
			this.btn추가.Size = new System.Drawing.Size(70, 19);
			this.btn추가.TabIndex = 7;
			this.btn추가.TabStop = false;
			this.btn추가.Text = "추가";
			this.btn추가.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.BackColor = System.Drawing.Color.Transparent;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(519, 3);
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
			this._flex.Size = new System.Drawing.Size(896, 557);
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
			this.oneGrid1.Size = new System.Drawing.Size(896, 41);
			this.oneGrid1.TabIndex = 2;
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
			this.oneGridItem1.Size = new System.Drawing.Size(886, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.txt가공ID);
			this.bpPanelControl2.Controls.Add(this.lbl가공ID);
			this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// txt가공ID
			// 
			this.txt가공ID.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt가공ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt가공ID.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt가공ID.Location = new System.Drawing.Point(106, 0);
			this.txt가공ID.Name = "txt가공ID";
			this.txt가공ID.Size = new System.Drawing.Size(186, 21);
			this.txt가공ID.TabIndex = 1;
			// 
			// lbl가공ID
			// 
			this.lbl가공ID.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl가공ID.Location = new System.Drawing.Point(0, 0);
			this.lbl가공ID.Name = "lbl가공ID";
			this.lbl가공ID.Size = new System.Drawing.Size(100, 23);
			this.lbl가공ID.TabIndex = 0;
			this.lbl가공ID.Text = "가공ID";
			this.lbl가공ID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.ctx품목코드);
			this.bpPanelControl1.Controls.Add(this.lbl품목코드);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// ctx품목코드
			// 
			this.ctx품목코드.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx품목코드.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
			this.ctx품목코드.Location = new System.Drawing.Point(106, 0);
			this.ctx품목코드.Name = "ctx품목코드";
			this.ctx품목코드.Size = new System.Drawing.Size(186, 21);
			this.ctx품목코드.TabIndex = 1;
			this.ctx품목코드.TabStop = false;
			this.ctx품목코드.Text = "bpCodeTextBox1";
			// 
			// lbl품목코드
			// 
			this.lbl품목코드.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl품목코드.Location = new System.Drawing.Point(0, 0);
			this.lbl품목코드.Name = "lbl품목코드";
			this.lbl품목코드.Size = new System.Drawing.Size(100, 23);
			this.lbl품목코드.TabIndex = 0;
			this.lbl품목코드.Text = "품목코드";
			this.lbl품목코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.cbo사용여부);
			this.bpPanelControl3.Controls.Add(this.lbl사용여부);
			this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// lbl사용여부
			// 
			this.lbl사용여부.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl사용여부.Location = new System.Drawing.Point(0, 0);
			this.lbl사용여부.Name = "lbl사용여부";
			this.lbl사용여부.Size = new System.Drawing.Size(100, 23);
			this.lbl사용여부.TabIndex = 0;
			this.lbl사용여부.Text = "사용여부";
			this.lbl사용여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo사용여부
			// 
			this.cbo사용여부.AutoDropDown = true;
			this.cbo사용여부.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo사용여부.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo사용여부.FormattingEnabled = true;
			this.cbo사용여부.ItemHeight = 12;
			this.cbo사용여부.Location = new System.Drawing.Point(106, 0);
			this.cbo사용여부.Name = "cbo사용여부";
			this.cbo사용여부.Size = new System.Drawing.Size(186, 20);
			this.cbo사용여부.TabIndex = 1;
			// 
			// P_CZ_PR_MATCHING_ID_OLD_SUB
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(904, 691);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_CZ_PR_MATCHING_ID_OLD_SUB";
			this.Text = "ERP iU";
			this.TitleText = "대상품목등록";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl2.PerformLayout();
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Common.Controls.RoundedButton btn저장;
        private Duzon.Common.Controls.RoundedButton btn닫기;
        private Duzon.Common.Controls.RoundedButton btn삭제;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
		private Duzon.Common.Controls.RoundedButton btn추가;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.BpControls.BpCodeTextBox ctx품목코드;
		private Duzon.Common.Controls.LabelExt lbl품목코드;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.Controls.LabelExt lbl가공ID;
		private Duzon.Common.Controls.TextBoxExt txt가공ID;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
		private Duzon.Common.Controls.DropDownComboBox cbo사용여부;
		private Duzon.Common.Controls.LabelExt lbl사용여부;
	}
}
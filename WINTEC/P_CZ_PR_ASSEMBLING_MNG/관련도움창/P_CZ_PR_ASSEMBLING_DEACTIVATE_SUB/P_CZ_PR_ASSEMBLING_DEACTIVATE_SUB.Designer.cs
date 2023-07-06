namespace cz
{
    partial class P_CZ_PR_ASSEMBLING_DEACTIVATE_SUB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_ASSEMBLING_DEACTIVATE_SUB));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn닫기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn폐기취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn폐기처리 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo상태 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl상태 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.lblID번호 = new Duzon.Common.Controls.LabelExt();
			this.txtID번호 = new Duzon.Common.Controls.TextBoxExt();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
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
			this.tableLayoutPanel1.Size = new System.Drawing.Size(690, 642);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn닫기);
			this.flowLayoutPanel1.Controls.Add(this.btn폐기취소);
			this.flowLayoutPanel1.Controls.Add(this.btn폐기처리);
			this.flowLayoutPanel1.Controls.Add(this.btn저장);
			this.flowLayoutPanel1.Controls.Add(this.btn삭제);
			this.flowLayoutPanel1.Controls.Add(this.btn조회);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 50);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(684, 26);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// btn닫기
			// 
			this.btn닫기.BackColor = System.Drawing.Color.Transparent;
			this.btn닫기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn닫기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn닫기.Location = new System.Drawing.Point(611, 3);
			this.btn닫기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn닫기.Name = "btn닫기";
			this.btn닫기.Size = new System.Drawing.Size(70, 19);
			this.btn닫기.TabIndex = 6;
			this.btn닫기.TabStop = false;
			this.btn닫기.Text = "닫기";
			this.btn닫기.UseVisualStyleBackColor = false;
			// 
			// btn폐기취소
			// 
			this.btn폐기취소.BackColor = System.Drawing.Color.Transparent;
			this.btn폐기취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn폐기취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn폐기취소.Location = new System.Drawing.Point(535, 3);
			this.btn폐기취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn폐기취소.Name = "btn폐기취소";
			this.btn폐기취소.Size = new System.Drawing.Size(70, 19);
			this.btn폐기취소.TabIndex = 9;
			this.btn폐기취소.TabStop = false;
			this.btn폐기취소.Text = "폐기취소";
			this.btn폐기취소.UseVisualStyleBackColor = false;
			// 
			// btn폐기처리
			// 
			this.btn폐기처리.BackColor = System.Drawing.Color.Transparent;
			this.btn폐기처리.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn폐기처리.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn폐기처리.Location = new System.Drawing.Point(459, 3);
			this.btn폐기처리.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn폐기처리.Name = "btn폐기처리";
			this.btn폐기처리.Size = new System.Drawing.Size(70, 19);
			this.btn폐기처리.TabIndex = 8;
			this.btn폐기처리.TabStop = false;
			this.btn폐기처리.Text = "폐기처리";
			this.btn폐기처리.UseVisualStyleBackColor = false;
			// 
			// btn저장
			// 
			this.btn저장.BackColor = System.Drawing.Color.Transparent;
			this.btn저장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn저장.Location = new System.Drawing.Point(383, 3);
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
			this.btn삭제.Location = new System.Drawing.Point(307, 3);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(70, 19);
			this.btn삭제.TabIndex = 5;
			this.btn삭제.TabStop = false;
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.BackColor = System.Drawing.Color.Transparent;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(231, 3);
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
			this._flex.Size = new System.Drawing.Size(684, 557);
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
			this.oneGrid1.Size = new System.Drawing.Size(684, 41);
			this.oneGrid1.TabIndex = 2;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(674, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.cbo상태);
			this.bpPanelControl2.Controls.Add(this.lbl상태);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
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
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.txtID번호);
			this.bpPanelControl1.Controls.Add(this.lblID번호);
			this.bpPanelControl1.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 2;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// lblID번호
			// 
			this.lblID번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblID번호.Location = new System.Drawing.Point(0, 0);
			this.lblID번호.Name = "lblID번호";
			this.lblID번호.Size = new System.Drawing.Size(100, 23);
			this.lblID번호.TabIndex = 4;
			this.lblID번호.Text = "ID번호";
			this.lblID번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtID번호
			// 
			this.txtID번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txtID번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtID번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txtID번호.Location = new System.Drawing.Point(106, 0);
			this.txtID번호.Name = "txtID번호";
			this.txtID번호.Size = new System.Drawing.Size(186, 21);
			this.txtID번호.TabIndex = 5;
			// 
			// P_CZ_PR_ASSEMBLING_DEACTIVATE_SUB
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(692, 691);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_CZ_PR_ASSEMBLING_DEACTIVATE_SUB";
			this.Text = "ERP iU";
			this.TitleText = "대기리스트";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
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
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.DropDownComboBox cbo상태;
        private Duzon.Common.Controls.LabelExt lbl상태;
		private Duzon.Common.Controls.RoundedButton btn폐기처리;
		private Duzon.Common.Controls.RoundedButton btn폐기취소;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
		private Duzon.Common.Controls.TextBoxExt txtID번호;
		private Duzon.Common.Controls.LabelExt lblID번호;
	}
}
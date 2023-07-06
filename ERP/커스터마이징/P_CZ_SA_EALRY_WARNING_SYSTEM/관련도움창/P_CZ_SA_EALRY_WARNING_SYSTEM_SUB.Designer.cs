namespace cz
{
    partial class P_CZ_SA_EALRY_WARNING_SYSTEM_SUB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_EALRY_WARNING_SYSTEM_SUB));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn닫기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn제거 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpg메시지 = new System.Windows.Forms.TabPage();
			this._flex메시지 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg조건 = new System.Windows.Forms.TabPage();
			this._flex조건 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg경과일수 = new System.Windows.Forms.TabPage();
			this._flex경과일수 = new Dass.FlexGrid.FlexGrid(this.components);
			this.tpg예외 = new System.Windows.Forms.TabPage();
			this._flex예외 = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.btn결과적용 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.chk시뮬레이션 = new Duzon.Common.Controls.CheckBoxExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx회사 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl회사 = new Duzon.Common.Controls.LabelExt();
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpg메시지.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex메시지)).BeginInit();
			this.tpg조건.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex조건)).BeginInit();
			this.tpg경과일수.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex경과일수)).BeginInit();
			this.tpg예외.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex예외)).BeginInit();
			this.oneGridItem1.SuspendLayout();
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
			this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 48);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(695, 571);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn닫기);
			this.flowLayoutPanel1.Controls.Add(this.btn저장);
			this.flowLayoutPanel1.Controls.Add(this.btn제거);
			this.flowLayoutPanel1.Controls.Add(this.btn추가);
			this.flowLayoutPanel1.Controls.Add(this.btn조회);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 50);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(689, 27);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// btn닫기
			// 
			this.btn닫기.BackColor = System.Drawing.Color.Transparent;
			this.btn닫기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn닫기.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn닫기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn닫기.Location = new System.Drawing.Point(616, 3);
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
			this.btn저장.Location = new System.Drawing.Point(540, 3);
			this.btn저장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn저장.Name = "btn저장";
			this.btn저장.Size = new System.Drawing.Size(70, 19);
			this.btn저장.TabIndex = 1;
			this.btn저장.TabStop = false;
			this.btn저장.Text = "저장";
			this.btn저장.UseVisualStyleBackColor = false;
			// 
			// btn제거
			// 
			this.btn제거.BackColor = System.Drawing.Color.Transparent;
			this.btn제거.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn제거.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn제거.Location = new System.Drawing.Point(464, 3);
			this.btn제거.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn제거.Name = "btn제거";
			this.btn제거.Size = new System.Drawing.Size(70, 19);
			this.btn제거.TabIndex = 3;
			this.btn제거.TabStop = false;
			this.btn제거.Text = "제거";
			this.btn제거.UseVisualStyleBackColor = false;
			// 
			// btn추가
			// 
			this.btn추가.BackColor = System.Drawing.Color.Transparent;
			this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn추가.Location = new System.Drawing.Point(388, 3);
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
			this.btn조회.Location = new System.Drawing.Point(312, 3);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 2;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tpg메시지);
			this.tabControl1.Controls.Add(this.tpg조건);
			this.tabControl1.Controls.Add(this.tpg경과일수);
			this.tabControl1.Controls.Add(this.tpg예외);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(3, 83);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(689, 485);
			this.tabControl1.TabIndex = 2;
			// 
			// tpg메시지
			// 
			this.tpg메시지.Controls.Add(this._flex메시지);
			this.tpg메시지.Location = new System.Drawing.Point(4, 22);
			this.tpg메시지.Name = "tpg메시지";
			this.tpg메시지.Padding = new System.Windows.Forms.Padding(3);
			this.tpg메시지.Size = new System.Drawing.Size(681, 459);
			this.tpg메시지.TabIndex = 0;
			this.tpg메시지.Text = "메시지";
			this.tpg메시지.UseVisualStyleBackColor = true;
			// 
			// _flex메시지
			// 
			this._flex메시지.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex메시지.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex메시지.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex메시지.AutoResize = false;
			this._flex메시지.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex메시지.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex메시지.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex메시지.EnabledHeaderCheck = true;
			this._flex메시지.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex메시지.Location = new System.Drawing.Point(3, 3);
			this._flex메시지.Name = "_flex메시지";
			this._flex메시지.Rows.Count = 1;
			this._flex메시지.Rows.DefaultSize = 18;
			this._flex메시지.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex메시지.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex메시지.ShowSort = false;
			this._flex메시지.Size = new System.Drawing.Size(675, 453);
			this._flex메시지.StyleInfo = resources.GetString("_flex메시지.StyleInfo");
			this._flex메시지.TabIndex = 0;
			this._flex메시지.UseGridCalculator = true;
			// 
			// tpg조건
			// 
			this.tpg조건.Controls.Add(this._flex조건);
			this.tpg조건.Location = new System.Drawing.Point(4, 22);
			this.tpg조건.Name = "tpg조건";
			this.tpg조건.Padding = new System.Windows.Forms.Padding(3);
			this.tpg조건.Size = new System.Drawing.Size(681, 459);
			this.tpg조건.TabIndex = 1;
			this.tpg조건.Text = "조건";
			this.tpg조건.UseVisualStyleBackColor = true;
			// 
			// _flex조건
			// 
			this._flex조건.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex조건.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex조건.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex조건.AutoResize = false;
			this._flex조건.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex조건.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex조건.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex조건.EnabledHeaderCheck = true;
			this._flex조건.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex조건.Location = new System.Drawing.Point(3, 3);
			this._flex조건.Name = "_flex조건";
			this._flex조건.Rows.Count = 1;
			this._flex조건.Rows.DefaultSize = 18;
			this._flex조건.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex조건.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex조건.ShowSort = false;
			this._flex조건.Size = new System.Drawing.Size(675, 453);
			this._flex조건.StyleInfo = resources.GetString("_flex조건.StyleInfo");
			this._flex조건.TabIndex = 0;
			this._flex조건.UseGridCalculator = true;
			// 
			// tpg경과일수
			// 
			this.tpg경과일수.Controls.Add(this._flex경과일수);
			this.tpg경과일수.Location = new System.Drawing.Point(4, 22);
			this.tpg경과일수.Name = "tpg경과일수";
			this.tpg경과일수.Size = new System.Drawing.Size(681, 459);
			this.tpg경과일수.TabIndex = 3;
			this.tpg경과일수.Text = "경과일수";
			this.tpg경과일수.UseVisualStyleBackColor = true;
			// 
			// _flex경과일수
			// 
			this._flex경과일수.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex경과일수.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex경과일수.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex경과일수.AutoResize = false;
			this._flex경과일수.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex경과일수.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex경과일수.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex경과일수.EnabledHeaderCheck = true;
			this._flex경과일수.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex경과일수.Location = new System.Drawing.Point(0, 0);
			this._flex경과일수.Name = "_flex경과일수";
			this._flex경과일수.Rows.Count = 1;
			this._flex경과일수.Rows.DefaultSize = 18;
			this._flex경과일수.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex경과일수.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex경과일수.ShowSort = false;
			this._flex경과일수.Size = new System.Drawing.Size(681, 459);
			this._flex경과일수.StyleInfo = resources.GetString("_flex경과일수.StyleInfo");
			this._flex경과일수.TabIndex = 0;
			this._flex경과일수.UseGridCalculator = true;
			// 
			// tpg예외
			// 
			this.tpg예외.Controls.Add(this._flex예외);
			this.tpg예외.Location = new System.Drawing.Point(4, 22);
			this.tpg예외.Name = "tpg예외";
			this.tpg예외.Size = new System.Drawing.Size(681, 459);
			this.tpg예외.TabIndex = 2;
			this.tpg예외.Text = "예외";
			this.tpg예외.UseVisualStyleBackColor = true;
			// 
			// _flex예외
			// 
			this._flex예외.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex예외.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex예외.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex예외.AutoResize = false;
			this._flex예외.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex예외.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex예외.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex예외.EnabledHeaderCheck = true;
			this._flex예외.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex예외.Location = new System.Drawing.Point(0, 0);
			this._flex예외.Name = "_flex예외";
			this._flex예외.Rows.Count = 1;
			this._flex예외.Rows.DefaultSize = 18;
			this._flex예외.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex예외.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex예외.ShowSort = false;
			this._flex예외.Size = new System.Drawing.Size(681, 459);
			this._flex예외.StyleInfo = resources.GetString("_flex예외.StyleInfo");
			this._flex예외.TabIndex = 0;
			this._flex예외.UseGridCalculator = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(689, 41);
			this.oneGrid1.TabIndex = 3;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.btn결과적용);
			this.oneGridItem1.Controls.Add(this.chk시뮬레이션);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(679, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// btn결과적용
			// 
			this.btn결과적용.BackColor = System.Drawing.Color.Transparent;
			this.btn결과적용.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn결과적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn결과적용.Location = new System.Drawing.Point(382, 1);
			this.btn결과적용.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn결과적용.Name = "btn결과적용";
			this.btn결과적용.Size = new System.Drawing.Size(77, 19);
			this.btn결과적용.TabIndex = 5;
			this.btn결과적용.TabStop = false;
			this.btn결과적용.Text = "결과적용";
			this.btn결과적용.UseVisualStyleBackColor = false;
			// 
			// chk시뮬레이션
			// 
			this.chk시뮬레이션.AutoSize = true;
			this.chk시뮬레이션.Location = new System.Drawing.Point(296, 1);
			this.chk시뮬레이션.Name = "chk시뮬레이션";
			this.chk시뮬레이션.Size = new System.Drawing.Size(84, 16);
			this.chk시뮬레이션.TabIndex = 5;
			this.chk시뮬레이션.Text = "시뮬레이션";
			this.chk시뮬레이션.TextDD = null;
			this.chk시뮬레이션.UseVisualStyleBackColor = true;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.ctx회사);
			this.bpPanelControl1.Controls.Add(this.lbl회사);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// ctx회사
			// 
			this.ctx회사.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx회사.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx회사.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx회사.Location = new System.Drawing.Point(107, 0);
			this.ctx회사.Name = "ctx회사";
			this.ctx회사.Size = new System.Drawing.Size(185, 21);
			this.ctx회사.TabIndex = 1;
			this.ctx회사.TabStop = false;
			this.ctx회사.Text = "bpCodeTextBox1";
			this.ctx회사.UserCodeName = "NM_COMPANY";
			this.ctx회사.UserCodeValue = "CD_COMPANY";
			this.ctx회사.UserHelpID = "H_CZ_MA_COMPANY_SUB";
			this.ctx회사.UserParams = "회사;H_CZ_MA_COMPANY_SUB";
			// 
			// lbl회사
			// 
			this.lbl회사.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl회사.Location = new System.Drawing.Point(0, 0);
			this.lbl회사.Name = "lbl회사";
			this.lbl회사.Size = new System.Drawing.Size(100, 23);
			this.lbl회사.TabIndex = 0;
			this.lbl회사.Text = "회사";
			this.lbl회사.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// P_CZ_SA_EALRY_WARNING_SYSTEM_SUB
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(697, 620);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_CZ_SA_EALRY_WARNING_SYSTEM_SUB";
			this.Text = "ERP iU";
			this.TitleText = "조기경보시스템설정";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tpg메시지.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex메시지)).EndInit();
			this.tpg조건.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex조건)).EndInit();
			this.tpg경과일수.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex경과일수)).EndInit();
			this.tpg예외.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex예외)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.oneGridItem1.PerformLayout();
			this.bpPanelControl1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn닫기;
        private Duzon.Common.Controls.RoundedButton btn저장;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Duzon.Common.Controls.RoundedButton btn제거;
        private Duzon.Common.Controls.RoundedButton btn추가;
        private Duzon.Common.Controls.CheckBoxExt chk시뮬레이션;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpg메시지;
        private Dass.FlexGrid.FlexGrid _flex메시지;
        private System.Windows.Forms.TabPage tpg조건;
        private Dass.FlexGrid.FlexGrid _flex조건;
        private System.Windows.Forms.TabPage tpg예외;
        private Dass.FlexGrid.FlexGrid _flex예외;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.BpControls.BpCodeTextBox ctx회사;
        private Duzon.Common.Controls.LabelExt lbl회사;
        private Duzon.Common.Controls.RoundedButton btn결과적용;
        private System.Windows.Forms.TabPage tpg경과일수;
        private Dass.FlexGrid.FlexGrid _flex경과일수;
	}
}
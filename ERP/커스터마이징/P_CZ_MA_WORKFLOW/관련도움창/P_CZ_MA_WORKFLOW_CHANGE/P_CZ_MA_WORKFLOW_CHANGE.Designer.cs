namespace cz
{
    partial class P_CZ_MA_WORKFLOW_CHANGE
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MA_WORKFLOW_CHANGE));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn닫기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn저장 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo워크플로우단계 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl워크플로우단계 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp완료일자 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl완료일자 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx회사 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl회사 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt파일번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl파일번호 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.chk완료제외 = new Duzon.Common.Controls.CheckBoxExt();
			this.lbl완료여부 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.btn담당자변경 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.ctx담당자변경 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.cbo담당자변경 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl담당자변경 = new Duzon.Common.Controls.LabelExt();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.bpPanelControl6.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.flowLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this._flex, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 47);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 91F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 117F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(958, 402);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn닫기);
			this.flowLayoutPanel1.Controls.Add(this.btn저장);
			this.flowLayoutPanel1.Controls.Add(this.btn조회);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(952, 26);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// btn닫기
			// 
			this.btn닫기.BackColor = System.Drawing.Color.Transparent;
			this.btn닫기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn닫기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn닫기.Location = new System.Drawing.Point(879, 3);
			this.btn닫기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn닫기.Name = "btn닫기";
			this.btn닫기.Size = new System.Drawing.Size(70, 19);
			this.btn닫기.TabIndex = 1;
			this.btn닫기.TabStop = false;
			this.btn닫기.Text = "닫기";
			this.btn닫기.UseVisualStyleBackColor = false;
			// 
			// btn저장
			// 
			this.btn저장.BackColor = System.Drawing.Color.Transparent;
			this.btn저장.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn저장.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn저장.Location = new System.Drawing.Point(803, 3);
			this.btn저장.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn저장.Name = "btn저장";
			this.btn저장.Size = new System.Drawing.Size(70, 19);
			this.btn저장.TabIndex = 2;
			this.btn저장.TabStop = false;
			this.btn저장.Text = "저장";
			this.btn저장.UseVisualStyleBackColor = false;
			// 
			// btn조회
			// 
			this.btn조회.BackColor = System.Drawing.Color.Transparent;
			this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn조회.Location = new System.Drawing.Point(727, 3);
			this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn조회.Name = "btn조회";
			this.btn조회.Size = new System.Drawing.Size(70, 19);
			this.btn조회.TabIndex = 0;
			this.btn조회.TabStop = false;
			this.btn조회.Text = "조회";
			this.btn조회.UseVisualStyleBackColor = false;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem3,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 35);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(952, 85);
			this.oneGrid1.TabIndex = 2;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl4);
			this.oneGridItem1.Controls.Add(this.bpPanelControl6);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(942, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.cbo워크플로우단계);
			this.bpPanelControl4.Controls.Add(this.lbl워크플로우단계);
			this.bpPanelControl4.Location = new System.Drawing.Point(592, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(317, 23);
			this.bpPanelControl4.TabIndex = 2;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// cbo워크플로우단계
			// 
			this.cbo워크플로우단계.AutoDropDown = true;
			this.cbo워크플로우단계.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo워크플로우단계.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo워크플로우단계.FormattingEnabled = true;
			this.cbo워크플로우단계.ItemHeight = 12;
			this.cbo워크플로우단계.Location = new System.Drawing.Point(106, 0);
			this.cbo워크플로우단계.Name = "cbo워크플로우단계";
			this.cbo워크플로우단계.Size = new System.Drawing.Size(211, 20);
			this.cbo워크플로우단계.TabIndex = 1;
			// 
			// lbl워크플로우단계
			// 
			this.lbl워크플로우단계.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl워크플로우단계.Location = new System.Drawing.Point(0, 0);
			this.lbl워크플로우단계.Name = "lbl워크플로우단계";
			this.lbl워크플로우단계.Size = new System.Drawing.Size(100, 23);
			this.lbl워크플로우단계.TabIndex = 0;
			this.lbl워크플로우단계.Text = "워크플로우단계";
			this.lbl워크플로우단계.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl6
			// 
			this.bpPanelControl6.Controls.Add(this.dtp완료일자);
			this.bpPanelControl6.Controls.Add(this.lbl완료일자);
			this.bpPanelControl6.Location = new System.Drawing.Point(297, 1);
			this.bpPanelControl6.Name = "bpPanelControl6";
			this.bpPanelControl6.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl6.TabIndex = 0;
			this.bpPanelControl6.Text = "bpPanelControl6";
			// 
			// dtp완료일자
			// 
			this.dtp완료일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp완료일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp완료일자.Location = new System.Drawing.Point(108, 0);
			this.dtp완료일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp완료일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp완료일자.Name = "dtp완료일자";
			this.dtp완료일자.Size = new System.Drawing.Size(185, 21);
			this.dtp완료일자.TabIndex = 1;
			// 
			// lbl완료일자
			// 
			this.lbl완료일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl완료일자.Location = new System.Drawing.Point(0, 0);
			this.lbl완료일자.Name = "lbl완료일자";
			this.lbl완료일자.Size = new System.Drawing.Size(100, 23);
			this.lbl완료일자.TabIndex = 0;
			this.lbl완료일자.Text = "완료일자";
			this.lbl완료일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.ctx회사);
			this.bpPanelControl2.Controls.Add(this.lbl회사);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// ctx회사
			// 
			this.ctx회사.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx회사.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx회사.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx회사.Location = new System.Drawing.Point(106, 0);
			this.ctx회사.Name = "ctx회사";
			this.ctx회사.Size = new System.Drawing.Size(187, 21);
			this.ctx회사.TabIndex = 2;
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
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpPanelControl1);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(942, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 1;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.txt파일번호);
			this.bpPanelControl1.Controls.Add(this.lbl파일번호);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// txt파일번호
			// 
			this.txt파일번호.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.txt파일번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt파일번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt파일번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt파일번호.Location = new System.Drawing.Point(106, 0);
			this.txt파일번호.Name = "txt파일번호";
			this.txt파일번호.Size = new System.Drawing.Size(187, 21);
			this.txt파일번호.TabIndex = 1;
			// 
			// lbl파일번호
			// 
			this.lbl파일번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl파일번호.Location = new System.Drawing.Point(0, 0);
			this.lbl파일번호.Name = "lbl파일번호";
			this.lbl파일번호.Size = new System.Drawing.Size(100, 23);
			this.lbl파일번호.TabIndex = 0;
			this.lbl파일번호.Text = "파일번호";
			this.lbl파일번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl5);
			this.oneGridItem2.Controls.Add(this.bpPanelControl3);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(942, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 2;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.chk완료제외);
			this.bpPanelControl5.Controls.Add(this.lbl완료여부);
			this.bpPanelControl5.Location = new System.Drawing.Point(616, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(317, 23);
			this.bpPanelControl5.TabIndex = 1;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// chk완료제외
			// 
			this.chk완료제외.AutoSize = true;
			this.chk완료제외.Location = new System.Drawing.Point(106, 3);
			this.chk완료제외.Name = "chk완료제외";
			this.chk완료제외.Size = new System.Drawing.Size(72, 16);
			this.chk완료제외.TabIndex = 1;
			this.chk완료제외.Text = "완료제외";
			this.chk완료제외.TextDD = null;
			this.chk완료제외.UseVisualStyleBackColor = true;
			// 
			// lbl완료여부
			// 
			this.lbl완료여부.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl완료여부.Location = new System.Drawing.Point(0, 0);
			this.lbl완료여부.Name = "lbl완료여부";
			this.lbl완료여부.Size = new System.Drawing.Size(100, 23);
			this.lbl완료여부.TabIndex = 0;
			this.lbl완료여부.Text = "완료여부";
			this.lbl완료여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.btn담당자변경);
			this.bpPanelControl3.Controls.Add(this.ctx담당자변경);
			this.bpPanelControl3.Controls.Add(this.cbo담당자변경);
			this.bpPanelControl3.Controls.Add(this.lbl담당자변경);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(612, 23);
			this.bpPanelControl3.TabIndex = 0;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// btn담당자변경
			// 
			this.btn담당자변경.BackColor = System.Drawing.Color.Transparent;
			this.btn담당자변경.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn담당자변경.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn담당자변경.Location = new System.Drawing.Point(508, 1);
			this.btn담당자변경.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn담당자변경.Name = "btn담당자변경";
			this.btn담당자변경.Size = new System.Drawing.Size(101, 19);
			this.btn담당자변경.TabIndex = 3;
			this.btn담당자변경.TabStop = false;
			this.btn담당자변경.Text = "담당자변경";
			this.btn담당자변경.UseVisualStyleBackColor = false;
			// 
			// ctx담당자변경
			// 
			this.ctx담당자변경.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx담당자변경.Location = new System.Drawing.Point(299, 1);
			this.ctx담당자변경.Name = "ctx담당자변경";
			this.ctx담당자변경.Size = new System.Drawing.Size(203, 21);
			this.ctx담당자변경.TabIndex = 2;
			this.ctx담당자변경.TabStop = false;
			this.ctx담당자변경.Text = "bpCodeTextBox1";
			this.ctx담당자변경.UserCodeName = "NM_USER";
			this.ctx담당자변경.UserCodeValue = "ID_USER";
			this.ctx담당자변경.UserHelpID = "H_CZ_SA_USER_SUB";
			this.ctx담당자변경.UserParams = "담당자;H_CZ_SA_USER_SUB";
			// 
			// cbo담당자변경
			// 
			this.cbo담당자변경.AutoDropDown = true;
			this.cbo담당자변경.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo담당자변경.FormattingEnabled = true;
			this.cbo담당자변경.ItemHeight = 12;
			this.cbo담당자변경.Location = new System.Drawing.Point(106, 1);
			this.cbo담당자변경.Name = "cbo담당자변경";
			this.cbo담당자변경.Size = new System.Drawing.Size(187, 20);
			this.cbo담당자변경.TabIndex = 1;
			// 
			// lbl담당자변경
			// 
			this.lbl담당자변경.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl담당자변경.Location = new System.Drawing.Point(0, 0);
			this.lbl담당자변경.Name = "lbl담당자변경";
			this.lbl담당자변경.Size = new System.Drawing.Size(100, 23);
			this.lbl담당자변경.TabIndex = 0;
			this.lbl담당자변경.Text = "담당자변경";
			this.lbl담당자변경.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this._flex.Location = new System.Drawing.Point(3, 157);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 18;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(952, 242);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 1;
			this._flex.UseGridCalculator = true;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.btn삭제);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 126);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(952, 25);
			this.flowLayoutPanel2.TabIndex = 3;
			// 
			// btn삭제
			// 
			this.btn삭제.BackColor = System.Drawing.Color.Transparent;
			this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn삭제.Location = new System.Drawing.Point(879, 3);
			this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn삭제.Name = "btn삭제";
			this.btn삭제.Size = new System.Drawing.Size(70, 19);
			this.btn삭제.TabIndex = 0;
			this.btn삭제.TabStop = false;
			this.btn삭제.Text = "삭제";
			this.btn삭제.UseVisualStyleBackColor = false;
			// 
			// P_CZ_MA_WORKFLOW_CHANGE
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.CaptionPaint = true;
			this.ClientSize = new System.Drawing.Size(958, 449);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "P_CZ_MA_WORKFLOW_CHANGE";
			this.Text = "ERP iU";
			this.TitleText = "관리페이지";
			((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl6.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.oneGridItem3.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl5.PerformLayout();
			this.bpPanelControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn닫기;
        private Duzon.Common.Controls.RoundedButton btn저장;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.TextBoxExt txt파일번호;
        private Duzon.Common.Controls.LabelExt lbl파일번호;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.LabelExt lbl회사;
        private Duzon.Common.BpControls.BpCodeTextBox ctx회사;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.RoundedButton btn담당자변경;
        private Duzon.Common.BpControls.BpCodeTextBox ctx담당자변경;
        private Duzon.Common.Controls.DropDownComboBox cbo담당자변경;
        private Duzon.Common.Controls.LabelExt lbl담당자변경;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Duzon.Common.Controls.RoundedButton btn삭제;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.DropDownComboBox cbo워크플로우단계;
        private Duzon.Common.Controls.LabelExt lbl워크플로우단계;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.Controls.CheckBoxExt chk완료제외;
        private Duzon.Common.Controls.LabelExt lbl완료여부;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.Controls.PeriodPicker dtp완료일자;
        private Duzon.Common.Controls.LabelExt lbl완료일자;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
    }
}
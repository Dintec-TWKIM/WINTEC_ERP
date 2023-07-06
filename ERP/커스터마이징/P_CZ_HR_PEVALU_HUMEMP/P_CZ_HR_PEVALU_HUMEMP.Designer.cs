namespace cz
{
    partial class P_CZ_HR_PEVALU_HUMEMP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_HR_PEVALU_HUMEMP));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bppnl평가차수 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo평가그룹 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl평가그룹 = new Duzon.Common.Controls.LabelExt();
            this.bppnl평가유형 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo평가유형 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl평가유형 = new Duzon.Common.Controls.LabelExt();
            this.bppnl평가코드 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx평가코드 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl평가코드 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo평가차수 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl평가차수 = new Duzon.Common.Controls.LabelExt();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.imagePanel2 = new Duzon.Common.Controls.ImagePanel(this.components);
            this.btn추가평가자 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn대상자선정평가자 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn삭제평가자 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
            this.btn추가피평가자 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn대상자선정피평가자 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn삭제피평가자 = new Duzon.Common.Controls.RoundedButton(this.components);
            this._flex평가자 = new Dass.FlexGrid.FlexGrid(this.components);
            this._flex피평가자 = new Dass.FlexGrid.FlexGrid(this.components);
            this.btn복사 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn엑셀양식다운로드 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn엑셀업로드 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bppnl평가차수.SuspendLayout();
            this.bppnl평가유형.SuspendLayout();
            this.bppnl평가코드.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.imagePanel2.SuspendLayout();
            this.imagePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex평가자)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex피평가자)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(983, 579);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(983, 579);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(977, 62);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bppnl평가차수);
            this.oneGridItem1.Controls.Add(this.bppnl평가유형);
            this.oneGridItem1.Controls.Add(this.bppnl평가코드);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(967, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bppnl평가차수
            // 
            this.bppnl평가차수.Controls.Add(this.cbo평가그룹);
            this.bppnl평가차수.Controls.Add(this.lbl평가그룹);
            this.bppnl평가차수.Location = new System.Drawing.Point(640, 1);
            this.bppnl평가차수.Name = "bppnl평가차수";
            this.bppnl평가차수.Size = new System.Drawing.Size(317, 23);
            this.bppnl평가차수.TabIndex = 2;
            this.bppnl평가차수.Text = "bpPanelControl3";
            // 
            // cbo평가그룹
            // 
            this.cbo평가그룹.AutoDropDown = true;
            this.cbo평가그룹.BackColor = System.Drawing.Color.White;
            this.cbo평가그룹.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo평가그룹.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo평가그룹.ItemHeight = 12;
            this.cbo평가그룹.Location = new System.Drawing.Point(106, 0);
            this.cbo평가그룹.Name = "cbo평가그룹";
            this.cbo평가그룹.Size = new System.Drawing.Size(211, 20);
            this.cbo평가그룹.TabIndex = 178;
            this.cbo평가그룹.Tag = "";
            // 
            // lbl평가그룹
            // 
            this.lbl평가그룹.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl평가그룹.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl평가그룹.Location = new System.Drawing.Point(0, 0);
            this.lbl평가그룹.Name = "lbl평가그룹";
            this.lbl평가그룹.Size = new System.Drawing.Size(100, 23);
            this.lbl평가그룹.TabIndex = 177;
            this.lbl평가그룹.Tag = "";
            this.lbl평가그룹.Text = "평가그룹";
            this.lbl평가그룹.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bppnl평가유형
            // 
            this.bppnl평가유형.Controls.Add(this.cbo평가유형);
            this.bppnl평가유형.Controls.Add(this.lbl평가유형);
            this.bppnl평가유형.Location = new System.Drawing.Point(321, 1);
            this.bppnl평가유형.Name = "bppnl평가유형";
            this.bppnl평가유형.Size = new System.Drawing.Size(317, 23);
            this.bppnl평가유형.TabIndex = 1;
            this.bppnl평가유형.Text = "bpPanelControl2";
            // 
            // cbo평가유형
            // 
            this.cbo평가유형.AutoDropDown = true;
            this.cbo평가유형.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo평가유형.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo평가유형.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo평가유형.ItemHeight = 12;
            this.cbo평가유형.Location = new System.Drawing.Point(106, 0);
            this.cbo평가유형.Name = "cbo평가유형";
            this.cbo평가유형.Size = new System.Drawing.Size(211, 20);
            this.cbo평가유형.TabIndex = 181;
            this.cbo평가유형.Tag = "";
            // 
            // lbl평가유형
            // 
            this.lbl평가유형.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl평가유형.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl평가유형.Location = new System.Drawing.Point(0, 0);
            this.lbl평가유형.Name = "lbl평가유형";
            this.lbl평가유형.Size = new System.Drawing.Size(100, 23);
            this.lbl평가유형.TabIndex = 180;
            this.lbl평가유형.Tag = "";
            this.lbl평가유형.Text = "평가유형";
            this.lbl평가유형.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bppnl평가코드
            // 
            this.bppnl평가코드.Controls.Add(this.ctx평가코드);
            this.bppnl평가코드.Controls.Add(this.lbl평가코드);
            this.bppnl평가코드.Location = new System.Drawing.Point(2, 1);
            this.bppnl평가코드.Name = "bppnl평가코드";
            this.bppnl평가코드.Size = new System.Drawing.Size(317, 23);
            this.bppnl평가코드.TabIndex = 0;
            this.bppnl평가코드.Text = "bpPanelControl1";
            // 
            // ctx평가코드
            // 
            this.ctx평가코드.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx평가코드.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_TABLE_SUB;
            this.ctx평가코드.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.ctx평가코드.Location = new System.Drawing.Point(106, 0);
            this.ctx평가코드.Name = "ctx평가코드";
            this.ctx평가코드.Size = new System.Drawing.Size(211, 21);
            this.ctx평가코드.TabIndex = 13;
            this.ctx평가코드.TabStop = false;
            this.ctx평가코드.Tag = "";
            this.ctx평가코드.Text = "bpCodeTextBox1";
            // 
            // lbl평가코드
            // 
            this.lbl평가코드.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl평가코드.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl평가코드.Location = new System.Drawing.Point(0, 0);
            this.lbl평가코드.Name = "lbl평가코드";
            this.lbl평가코드.Size = new System.Drawing.Size(100, 23);
            this.lbl평가코드.TabIndex = 12;
            this.lbl평가코드.Tag = "";
            this.lbl평가코드.Text = "평가코드";
            this.lbl평가코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl1);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(967, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.cbo평가차수);
            this.bpPanelControl1.Controls.Add(this.lbl평가차수);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(317, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // cbo평가차수
            // 
            this.cbo평가차수.AutoDropDown = true;
            this.cbo평가차수.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo평가차수.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo평가차수.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo평가차수.ItemHeight = 12;
            this.cbo평가차수.Location = new System.Drawing.Point(106, 0);
            this.cbo평가차수.Name = "cbo평가차수";
            this.cbo평가차수.Size = new System.Drawing.Size(211, 20);
            this.cbo평가차수.TabIndex = 182;
            this.cbo평가차수.Tag = "";
            // 
            // lbl평가차수
            // 
            this.lbl평가차수.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl평가차수.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl평가차수.Location = new System.Drawing.Point(0, 0);
            this.lbl평가차수.Name = "lbl평가차수";
            this.lbl평가차수.Size = new System.Drawing.Size(100, 23);
            this.lbl평가차수.TabIndex = 181;
            this.lbl평가차수.Tag = "";
            this.lbl평가차수.Text = "평가차수";
            this.lbl평가차수.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.imagePanel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.imagePanel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this._flex평가자, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this._flex피평가자, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 71);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(977, 505);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // imagePanel2
            // 
            this.imagePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.imagePanel2.Controls.Add(this.btn추가평가자);
            this.imagePanel2.Controls.Add(this.btn대상자선정평가자);
            this.imagePanel2.Controls.Add(this.btn삭제평가자);
            this.imagePanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.imagePanel2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.imagePanel2.LeftImage = null;
            this.imagePanel2.Location = new System.Drawing.Point(3, 3);
            this.imagePanel2.Name = "imagePanel2";
            this.imagePanel2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
            this.imagePanel2.PatternImage = null;
            this.imagePanel2.RightImage = null;
            this.imagePanel2.Size = new System.Drawing.Size(482, 24);
            this.imagePanel2.TabIndex = 5;
            this.imagePanel2.TitleText = "평가자";
            // 
            // btn추가평가자
            // 
            this.btn추가평가자.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn추가평가자.BackColor = System.Drawing.Color.Transparent;
            this.btn추가평가자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn추가평가자.Enabled = false;
            this.btn추가평가자.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn추가평가자.Location = new System.Drawing.Point(356, 3);
            this.btn추가평가자.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn추가평가자.Name = "btn추가평가자";
            this.btn추가평가자.Size = new System.Drawing.Size(57, 19);
            this.btn추가평가자.TabIndex = 5;
            this.btn추가평가자.TabStop = false;
            this.btn추가평가자.Text = "추가";
            this.btn추가평가자.UseVisualStyleBackColor = false;
            // 
            // btn대상자선정평가자
            // 
            this.btn대상자선정평가자.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn대상자선정평가자.BackColor = System.Drawing.Color.White;
            this.btn대상자선정평가자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn대상자선정평가자.Enabled = false;
            this.btn대상자선정평가자.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn대상자선정평가자.Location = new System.Drawing.Point(268, 3);
            this.btn대상자선정평가자.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn대상자선정평가자.Name = "btn대상자선정평가자";
            this.btn대상자선정평가자.Size = new System.Drawing.Size(82, 19);
            this.btn대상자선정평가자.TabIndex = 3;
            this.btn대상자선정평가자.TabStop = false;
            this.btn대상자선정평가자.Text = "대상자선정";
            this.btn대상자선정평가자.UseVisualStyleBackColor = false;
            // 
            // btn삭제평가자
            // 
            this.btn삭제평가자.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn삭제평가자.BackColor = System.Drawing.Color.White;
            this.btn삭제평가자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn삭제평가자.Enabled = false;
            this.btn삭제평가자.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn삭제평가자.Location = new System.Drawing.Point(419, 3);
            this.btn삭제평가자.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn삭제평가자.Name = "btn삭제평가자";
            this.btn삭제평가자.Size = new System.Drawing.Size(60, 19);
            this.btn삭제평가자.TabIndex = 4;
            this.btn삭제평가자.TabStop = false;
            this.btn삭제평가자.Text = "삭제";
            this.btn삭제평가자.UseVisualStyleBackColor = false;
            // 
            // imagePanel1
            // 
            this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.imagePanel1.Controls.Add(this.btn추가피평가자);
            this.imagePanel1.Controls.Add(this.btn대상자선정피평가자);
            this.imagePanel1.Controls.Add(this.btn삭제피평가자);
            this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.imagePanel1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.imagePanel1.LeftImage = null;
            this.imagePanel1.Location = new System.Drawing.Point(491, 3);
            this.imagePanel1.Name = "imagePanel1";
            this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
            this.imagePanel1.PatternImage = null;
            this.imagePanel1.RightImage = null;
            this.imagePanel1.Size = new System.Drawing.Size(483, 24);
            this.imagePanel1.TabIndex = 6;
            this.imagePanel1.TitleText = "피평가자";
            // 
            // btn추가피평가자
            // 
            this.btn추가피평가자.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn추가피평가자.BackColor = System.Drawing.Color.Transparent;
            this.btn추가피평가자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn추가피평가자.Enabled = false;
            this.btn추가피평가자.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn추가피평가자.Location = new System.Drawing.Point(357, 3);
            this.btn추가피평가자.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn추가피평가자.Name = "btn추가피평가자";
            this.btn추가피평가자.Size = new System.Drawing.Size(57, 19);
            this.btn추가피평가자.TabIndex = 6;
            this.btn추가피평가자.TabStop = false;
            this.btn추가피평가자.Text = "추가";
            this.btn추가피평가자.UseVisualStyleBackColor = false;
            // 
            // btn대상자선정피평가자
            // 
            this.btn대상자선정피평가자.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn대상자선정피평가자.BackColor = System.Drawing.Color.White;
            this.btn대상자선정피평가자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn대상자선정피평가자.Enabled = false;
            this.btn대상자선정피평가자.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn대상자선정피평가자.Location = new System.Drawing.Point(269, 3);
            this.btn대상자선정피평가자.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn대상자선정피평가자.Name = "btn대상자선정피평가자";
            this.btn대상자선정피평가자.Size = new System.Drawing.Size(82, 19);
            this.btn대상자선정피평가자.TabIndex = 3;
            this.btn대상자선정피평가자.TabStop = false;
            this.btn대상자선정피평가자.Text = "대상자선정";
            this.btn대상자선정피평가자.UseVisualStyleBackColor = false;
            // 
            // btn삭제피평가자
            // 
            this.btn삭제피평가자.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn삭제피평가자.BackColor = System.Drawing.Color.White;
            this.btn삭제피평가자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn삭제피평가자.Enabled = false;
            this.btn삭제피평가자.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn삭제피평가자.Location = new System.Drawing.Point(420, 3);
            this.btn삭제피평가자.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn삭제피평가자.Name = "btn삭제피평가자";
            this.btn삭제피평가자.Size = new System.Drawing.Size(60, 19);
            this.btn삭제피평가자.TabIndex = 4;
            this.btn삭제피평가자.TabStop = false;
            this.btn삭제피평가자.Text = "삭제";
            this.btn삭제피평가자.UseVisualStyleBackColor = false;
            // 
            // _flex평가자
            // 
            this._flex평가자.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex평가자.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex평가자.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex평가자.AutoResize = false;
            this._flex평가자.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex평가자.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex평가자.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex평가자.EnabledHeaderCheck = true;
            this._flex평가자.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex평가자.Location = new System.Drawing.Point(3, 33);
            this._flex평가자.Name = "_flex평가자";
            this._flex평가자.Rows.Count = 1;
            this._flex평가자.Rows.DefaultSize = 20;
            this._flex평가자.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex평가자.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex평가자.ShowSort = false;
            this._flex평가자.Size = new System.Drawing.Size(482, 469);
            this._flex평가자.StyleInfo = resources.GetString("_flex평가자.StyleInfo");
            this._flex평가자.TabIndex = 7;
            this._flex평가자.UseGridCalculator = true;
            // 
            // _flex피평가자
            // 
            this._flex피평가자.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex피평가자.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex피평가자.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex피평가자.AutoResize = false;
            this._flex피평가자.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex피평가자.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex피평가자.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex피평가자.EnabledHeaderCheck = true;
            this._flex피평가자.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex피평가자.Location = new System.Drawing.Point(491, 33);
            this._flex피평가자.Name = "_flex피평가자";
            this._flex피평가자.Rows.Count = 1;
            this._flex피평가자.Rows.DefaultSize = 20;
            this._flex피평가자.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex피평가자.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex피평가자.ShowSort = false;
            this._flex피평가자.Size = new System.Drawing.Size(483, 469);
            this._flex피평가자.StyleInfo = resources.GetString("_flex피평가자.StyleInfo");
            this._flex피평가자.TabIndex = 8;
            this._flex피평가자.UseGridCalculator = true;
            // 
            // btn복사
            // 
            this.btn복사.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn복사.BackColor = System.Drawing.Color.White;
            this.btn복사.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn복사.Enabled = false;
            this.btn복사.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn복사.Font = new System.Drawing.Font("굴림", 9F);
            this.btn복사.Location = new System.Drawing.Point(829, 14);
            this.btn복사.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn복사.Name = "btn복사";
            this.btn복사.Size = new System.Drawing.Size(148, 19);
            this.btn복사.TabIndex = 150;
            this.btn복사.TabStop = false;
            this.btn복사.Text = "평가자 / 피평가자복사";
            this.btn복사.UseVisualStyleBackColor = true;
            // 
            // btn엑셀양식다운로드
            // 
            this.btn엑셀양식다운로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn엑셀양식다운로드.BackColor = System.Drawing.Color.Transparent;
            this.btn엑셀양식다운로드.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn엑셀양식다운로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn엑셀양식다운로드.Location = new System.Drawing.Point(610, 14);
            this.btn엑셀양식다운로드.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn엑셀양식다운로드.Name = "btn엑셀양식다운로드";
            this.btn엑셀양식다운로드.Size = new System.Drawing.Size(111, 19);
            this.btn엑셀양식다운로드.TabIndex = 151;
            this.btn엑셀양식다운로드.TabStop = false;
            this.btn엑셀양식다운로드.Text = "엑셀양식다운로드";
            this.btn엑셀양식다운로드.UseVisualStyleBackColor = false;
            // 
            // btn엑셀업로드
            // 
            this.btn엑셀업로드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn엑셀업로드.BackColor = System.Drawing.Color.Transparent;
            this.btn엑셀업로드.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn엑셀업로드.Enabled = false;
            this.btn엑셀업로드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn엑셀업로드.Location = new System.Drawing.Point(727, 14);
            this.btn엑셀업로드.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn엑셀업로드.Name = "btn엑셀업로드";
            this.btn엑셀업로드.Size = new System.Drawing.Size(96, 19);
            this.btn엑셀업로드.TabIndex = 152;
            this.btn엑셀업로드.TabStop = false;
            this.btn엑셀업로드.Text = "엑셀업로드";
            this.btn엑셀업로드.UseVisualStyleBackColor = false;
            // 
            // P_CZ_HR_PEVALU_HUMEMP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btn엑셀업로드);
            this.Controls.Add(this.btn엑셀양식다운로드);
            this.Controls.Add(this.btn복사);
            this.Name = "P_CZ_HR_PEVALU_HUMEMP";
            this.Size = new System.Drawing.Size(983, 619);
            this.TitleText = "평가자/피평가자 등록";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.btn복사, 0);
            this.Controls.SetChildIndex(this.btn엑셀양식다운로드, 0);
            this.Controls.SetChildIndex(this.btn엑셀업로드, 0);
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bppnl평가차수.ResumeLayout(false);
            this.bppnl평가유형.ResumeLayout(false);
            this.bppnl평가코드.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.imagePanel2.ResumeLayout(false);
            this.imagePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex평가자)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex피평가자)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bppnl평가차수;
        private Duzon.Common.BpControls.BpPanelControl bppnl평가유형;
        private Duzon.Common.Controls.DropDownComboBox cbo평가유형;
        private Duzon.Common.Controls.LabelExt lbl평가유형;
        private Duzon.Common.BpControls.BpPanelControl bppnl평가코드;
        private Duzon.Common.BpControls.BpCodeTextBox ctx평가코드;
        private Duzon.Common.Controls.LabelExt lbl평가코드;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.DropDownComboBox cbo평가그룹;
        private Duzon.Common.Controls.LabelExt lbl평가그룹;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Duzon.Common.Controls.ImagePanel imagePanel2;
        private Duzon.Common.Controls.RoundedButton btn대상자선정평가자;
        private Duzon.Common.Controls.RoundedButton btn삭제평가자;
        private Duzon.Common.Controls.ImagePanel imagePanel1;
        private Duzon.Common.Controls.RoundedButton btn대상자선정피평가자;
        private Duzon.Common.Controls.RoundedButton btn삭제피평가자;
        private Dass.FlexGrid.FlexGrid _flex평가자;
        private Dass.FlexGrid.FlexGrid _flex피평가자;
        private Duzon.Common.Controls.DropDownComboBox cbo평가차수;
        private Duzon.Common.Controls.LabelExt lbl평가차수;
        private Duzon.Common.Controls.RoundedButton btn복사;
        #endregion
        private Duzon.Common.Controls.RoundedButton btn추가평가자;
        private Duzon.Common.Controls.RoundedButton btn추가피평가자;
        private Duzon.Common.Controls.RoundedButton btn엑셀양식다운로드;
        private Duzon.Common.Controls.RoundedButton btn엑셀업로드;
    }
}
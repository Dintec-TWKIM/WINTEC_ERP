namespace cz
{
    partial class P_CZ_SA_SO
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_SO));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tabControl = new Duzon.Common.Controls.TabControlExt();
            this.tpg기본정보 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.imagePanel = new Duzon.Common.Controls.ImagePanel(this.components);
            this.pnl기본정보 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl매출처 = new Duzon.Common.Controls.LabelExt();
            this.ctx매출처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl청구일자 = new Duzon.Common.Controls.LabelExt();
            this.dtp청구일자 = new Duzon.Common.Controls.DatePicker();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo청구구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl청구번호 = new Duzon.Common.Controls.LabelExt();
            this.txt청구번호 = new Duzon.Common.Controls.TextBoxExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl수주형태 = new Duzon.Common.Controls.LabelExt();
            this.ctx수주형태 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl담당자 = new Duzon.Common.Controls.LabelExt();
            this.ctx담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl영업그룹 = new Duzon.Common.Controls.LabelExt();
            this.ctx영업그룹 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.oneGridItem7 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt주문번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl주문번호 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl31 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl지불조건 = new Duzon.Common.Controls.LabelExt();
            this.cbo지불조건 = new Duzon.Common.Controls.DropDownComboBox();
            this.bpPanelControl32 = new Duzon.Common.BpControls.BpPanelControl();
            this.cboINCOTERMS지역 = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo선적조건 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl선적조건 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl20 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt호선명 = new Duzon.Common.Controls.TextBoxExt();
            this.ctx호선번호 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl호선 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl9 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl화폐단위 = new Duzon.Common.Controls.LabelExt();
            this.cur환율 = new Duzon.Common.Controls.CurrencyTextBox();
            this.cbo화폐단위 = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem4 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl프로젝트 = new Duzon.Common.Controls.LabelExt();
            this.ctx프로젝트 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.btn프로젝트적용 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.bpPanelControl11 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl부가세포함 = new Duzon.Common.Controls.LabelExt();
            this.cbo부가세포함 = new Duzon.Common.Controls.DropDownComboBox();
            this.bpPanelControl12 = new Duzon.Common.BpControls.BpPanelControl();
            this.lblVAT구분 = new Duzon.Common.Controls.LabelExt();
            this.cur부가세율 = new Duzon.Common.Controls.CurrencyTextBox();
            this.cbo부가세구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.tpg매출정보 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.imagePanel2 = new Duzon.Common.Controls.ImagePanel(this.components);
            this.pnl매출정보 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem5 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl60 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp수금예정일자 = new Duzon.Common.Controls.DatePicker();
            this.lbl수금예정일자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl57 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp매출일자 = new Duzon.Common.Controls.DatePicker();
            this.lbl매출일자 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem6 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl10 = new Duzon.Common.BpControls.BpPanelControl();
            this.cur외화금액 = new Duzon.Common.Controls.CurrencyTextBox();
            this.lbl외화금액 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl65 = new Duzon.Common.BpControls.BpPanelControl();
            this.cur부가세액 = new Duzon.Common.Controls.CurrencyTextBox();
            this.lbl부가세액 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl66 = new Duzon.Common.BpControls.BpPanelControl();
            this.cur공급가액 = new Duzon.Common.Controls.CurrencyTextBox();
            this.lbl공급가액 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem8 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl13 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt매출비고 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl매출비고 = new Duzon.Common.Controls.LabelExt();
            this.img = new System.Windows.Forms.ImageList(this.components);
            this.roundedButton1 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn자동삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.oneGridItem9 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl14 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl비고 = new Duzon.Common.Controls.LabelExt();
            this.txt비고 = new Duzon.Common.Controls.TextBoxExt();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.imagePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tpg기본정보.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp청구일자)).BeginInit();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.oneGridItem7.SuspendLayout();
            this.bpPanelControl8.SuspendLayout();
            this.bpPanelControl31.SuspendLayout();
            this.bpPanelControl32.SuspendLayout();
            this.oneGridItem3.SuspendLayout();
            this.bpPanelControl20.SuspendLayout();
            this.bpPanelControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur환율)).BeginInit();
            this.oneGridItem4.SuspendLayout();
            this.bpPanelControl7.SuspendLayout();
            this.bpPanelControl11.SuspendLayout();
            this.bpPanelControl12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur부가세율)).BeginInit();
            this.tpg매출정보.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.oneGridItem5.SuspendLayout();
            this.bpPanelControl60.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp수금예정일자)).BeginInit();
            this.bpPanelControl57.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp매출일자)).BeginInit();
            this.oneGridItem6.SuspendLayout();
            this.bpPanelControl10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur외화금액)).BeginInit();
            this.bpPanelControl65.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur부가세액)).BeginInit();
            this.bpPanelControl66.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur공급가액)).BeginInit();
            this.oneGridItem8.SuspendLayout();
            this.bpPanelControl13.SuspendLayout();
            this.oneGridItem9.SuspendLayout();
            this.bpPanelControl14.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(935, 579);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.imagePanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 226F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(935, 579);
            this.tableLayoutPanel1.TabIndex = 118;
            // 
            // imagePanel1
            // 
            this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.imagePanel1.Controls.Add(this._flex);
            this.imagePanel1.Controls.Add(this.btn삭제);
            this.imagePanel1.Controls.Add(this.btn추가);
            this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel1.LeftImage = null;
            this.imagePanel1.Location = new System.Drawing.Point(0, 226);
            this.imagePanel1.Margin = new System.Windows.Forms.Padding(0);
            this.imagePanel1.Name = "imagePanel1";
            this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
            this.imagePanel1.PatternImage = null;
            this.imagePanel1.RightImage = null;
            this.imagePanel1.Size = new System.Drawing.Size(935, 353);
            this.imagePanel1.TabIndex = 36;
            this.imagePanel1.TitleText = "품목";
            // 
            // _flex
            // 
            this._flex.AddMyMenu = true;
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.Font = new System.Drawing.Font("굴림", 9F);
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(3, 27);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(929, 323);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 1;
            this._flex.UseGridCalculator = true;
            // 
            // btn삭제
            // 
            this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn삭제.BackColor = System.Drawing.Color.White;
            this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn삭제.Location = new System.Drawing.Point(870, 3);
            this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn삭제.Name = "btn삭제";
            this.btn삭제.Size = new System.Drawing.Size(62, 19);
            this.btn삭제.TabIndex = 112;
            this.btn삭제.TabStop = false;
            this.btn삭제.Tag = "삭제";
            this.btn삭제.Text = "삭제";
            this.btn삭제.UseVisualStyleBackColor = false;
            // 
            // btn추가
            // 
            this.btn추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn추가.BackColor = System.Drawing.Color.White;
            this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn추가.Location = new System.Drawing.Point(802, 3);
            this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn추가.Name = "btn추가";
            this.btn추가.Size = new System.Drawing.Size(62, 19);
            this.btn추가.TabIndex = 113;
            this.btn추가.TabStop = false;
            this.btn추가.Text = "추가";
            this.btn추가.UseVisualStyleBackColor = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpg기본정보);
            this.tabControl.Controls.Add(this.tpg매출정보);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ImageList = this.img;
            this.tabControl.ItemSize = new System.Drawing.Size(120, 20);
            this.tabControl.Location = new System.Drawing.Point(3, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(929, 220);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 0;
            // 
            // tpg기본정보
            // 
            this.tpg기본정보.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.tpg기본정보.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpg기본정보.Controls.Add(this.tableLayoutPanel2);
            this.tpg기본정보.ImageIndex = 0;
            this.tpg기본정보.Location = new System.Drawing.Point(4, 24);
            this.tpg기본정보.Name = "tpg기본정보";
            this.tpg기본정보.Size = new System.Drawing.Size(921, 192);
            this.tpg기본정보.TabIndex = 0;
            this.tpg기본정보.Text = "기본정보";
            this.tpg기본정보.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.imagePanel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pnl기본정보, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(917, 188);
            this.tableLayoutPanel2.TabIndex = 36;
            // 
            // imagePanel
            // 
            this.imagePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.imagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel.LeftImage = null;
            this.imagePanel.Location = new System.Drawing.Point(0, 0);
            this.imagePanel.Margin = new System.Windows.Forms.Padding(0);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
            this.imagePanel.PatternImage = null;
            this.imagePanel.RightImage = null;
            this.imagePanel.Size = new System.Drawing.Size(917, 28);
            this.imagePanel.TabIndex = 35;
            this.imagePanel.TitleText = "기본정보";
            // 
            // pnl기본정보
            // 
            this.pnl기본정보.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl기본정보.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem7,
            this.oneGridItem3,
            this.oneGridItem4,
            this.oneGridItem9});
            this.pnl기본정보.Location = new System.Drawing.Point(3, 31);
            this.pnl기본정보.Name = "pnl기본정보";
            this.pnl기본정보.Size = new System.Drawing.Size(911, 154);
            this.pnl기본정보.TabIndex = 3;
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
            this.oneGridItem1.Size = new System.Drawing.Size(901, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.lbl매출처);
            this.bpPanelControl3.Controls.Add(this.ctx매출처);
            this.bpPanelControl3.Location = new System.Drawing.Point(598, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(296, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // lbl매출처
            // 
            this.lbl매출처.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl매출처.Location = new System.Drawing.Point(0, 3);
            this.lbl매출처.Name = "lbl매출처";
            this.lbl매출처.Size = new System.Drawing.Size(80, 16);
            this.lbl매출처.TabIndex = 139;
            this.lbl매출처.Text = "매출처";
            this.lbl매출처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx매출처
            // 
            this.ctx매출처.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctx매출처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx매출처.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.ctx매출처.Location = new System.Drawing.Point(81, 1);
            this.ctx매출처.Name = "ctx매출처";
            this.ctx매출처.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.ctx매출처.Size = new System.Drawing.Size(215, 21);
            this.ctx매출처.TabIndex = 2;
            this.ctx매출처.TabStop = false;
            this.ctx매출처.Tag = "CD_PARTNER;LN_PARTNER";
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.lbl청구일자);
            this.bpPanelControl2.Controls.Add(this.dtp청구일자);
            this.bpPanelControl2.Location = new System.Drawing.Point(300, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(296, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // lbl청구일자
            // 
            this.lbl청구일자.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl청구일자.Location = new System.Drawing.Point(0, 3);
            this.lbl청구일자.Name = "lbl청구일자";
            this.lbl청구일자.Size = new System.Drawing.Size(80, 16);
            this.lbl청구일자.TabIndex = 79;
            this.lbl청구일자.Text = "청구일자";
            this.lbl청구일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtp청구일자
            // 
            this.dtp청구일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp청구일자.Location = new System.Drawing.Point(81, 1);
            this.dtp청구일자.Mask = "####/##/##";
            this.dtp청구일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp청구일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp청구일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp청구일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp청구일자.Name = "dtp청구일자";
            this.dtp청구일자.Size = new System.Drawing.Size(86, 21);
            this.dtp청구일자.TabIndex = 1;
            this.dtp청구일자.Tag = "DT_SO";
            this.dtp청구일자.Value = new System.DateTime(((long)(0)));
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.cbo청구구분);
            this.bpPanelControl1.Controls.Add(this.lbl청구번호);
            this.bpPanelControl1.Controls.Add(this.txt청구번호);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(296, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // cbo청구구분
            // 
            this.cbo청구구분.AutoDropDown = true;
            this.cbo청구구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo청구구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo청구구분.FormattingEnabled = true;
            this.cbo청구구분.ItemHeight = 12;
            this.cbo청구구분.Location = new System.Drawing.Point(81, 1);
            this.cbo청구구분.Name = "cbo청구구분";
            this.cbo청구구분.Size = new System.Drawing.Size(85, 20);
            this.cbo청구구분.TabIndex = 77;
            // 
            // lbl청구번호
            // 
            this.lbl청구번호.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl청구번호.Location = new System.Drawing.Point(0, 3);
            this.lbl청구번호.Name = "lbl청구번호";
            this.lbl청구번호.Size = new System.Drawing.Size(80, 16);
            this.lbl청구번호.TabIndex = 76;
            this.lbl청구번호.Text = "청구번호";
            this.lbl청구번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt청구번호
            // 
            this.txt청구번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt청구번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt청구번호.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt청구번호.Font = new System.Drawing.Font("굴림체", 9F);
            this.txt청구번호.Location = new System.Drawing.Point(172, 1);
            this.txt청구번호.MaxLength = 20;
            this.txt청구번호.Name = "txt청구번호";
            this.txt청구번호.ReadOnly = true;
            this.txt청구번호.Size = new System.Drawing.Size(124, 21);
            this.txt청구번호.TabIndex = 0;
            this.txt청구번호.TabStop = false;
            this.txt청구번호.Tag = "NO_SO";
            this.txt청구번호.UseKeyF3 = false;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(901, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.lbl수주형태);
            this.bpPanelControl4.Controls.Add(this.ctx수주형태);
            this.bpPanelControl4.Location = new System.Drawing.Point(598, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(296, 23);
            this.bpPanelControl4.TabIndex = 5;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // lbl수주형태
            // 
            this.lbl수주형태.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl수주형태.Location = new System.Drawing.Point(0, 3);
            this.lbl수주형태.Name = "lbl수주형태";
            this.lbl수주형태.Size = new System.Drawing.Size(80, 16);
            this.lbl수주형태.TabIndex = 125;
            this.lbl수주형태.Text = "수주형태";
            this.lbl수주형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx수주형태
            // 
            this.ctx수주형태.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctx수주형태.HelpID = Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB;
            this.ctx수주형태.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.ctx수주형태.Location = new System.Drawing.Point(81, 1);
            this.ctx수주형태.Name = "ctx수주형태";
            this.ctx수주형태.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.ctx수주형태.Size = new System.Drawing.Size(215, 21);
            this.ctx수주형태.TabIndex = 5;
            this.ctx수주형태.TabStop = false;
            this.ctx수주형태.Tag = "TP_SO;NM_SO";
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.lbl담당자);
            this.bpPanelControl5.Controls.Add(this.ctx담당자);
            this.bpPanelControl5.Location = new System.Drawing.Point(300, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(296, 23);
            this.bpPanelControl5.TabIndex = 4;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // lbl담당자
            // 
            this.lbl담당자.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl담당자.Location = new System.Drawing.Point(0, 3);
            this.lbl담당자.Name = "lbl담당자";
            this.lbl담당자.Size = new System.Drawing.Size(80, 16);
            this.lbl담당자.TabIndex = 127;
            this.lbl담당자.Text = "담당자";
            this.lbl담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx담당자
            // 
            this.ctx담당자.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctx담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.ctx담당자.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.ctx담당자.Location = new System.Drawing.Point(81, 1);
            this.ctx담당자.Name = "ctx담당자";
            this.ctx담당자.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.ctx담당자.Size = new System.Drawing.Size(215, 21);
            this.ctx담당자.TabIndex = 4;
            this.ctx담당자.TabStop = false;
            this.ctx담당자.Tag = "NO_EMP;NM_KOR";
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.lbl영업그룹);
            this.bpPanelControl6.Controls.Add(this.ctx영업그룹);
            this.bpPanelControl6.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(296, 23);
            this.bpPanelControl6.TabIndex = 3;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // lbl영업그룹
            // 
            this.lbl영업그룹.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl영업그룹.Location = new System.Drawing.Point(0, 3);
            this.lbl영업그룹.Name = "lbl영업그룹";
            this.lbl영업그룹.Size = new System.Drawing.Size(80, 16);
            this.lbl영업그룹.TabIndex = 82;
            this.lbl영업그룹.Text = "영업그룹";
            this.lbl영업그룹.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx영업그룹
            // 
            this.ctx영업그룹.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctx영업그룹.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.ctx영업그룹.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.ctx영업그룹.Location = new System.Drawing.Point(81, 1);
            this.ctx영업그룹.Name = "ctx영업그룹";
            this.ctx영업그룹.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.ctx영업그룹.Size = new System.Drawing.Size(215, 21);
            this.ctx영업그룹.TabIndex = 3;
            this.ctx영업그룹.TabStop = false;
            this.ctx영업그룹.Tag = "CD_SALEGRP;NM_SALEGRP";
            // 
            // oneGridItem7
            // 
            this.oneGridItem7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem7.Controls.Add(this.bpPanelControl8);
            this.oneGridItem7.Controls.Add(this.bpPanelControl31);
            this.oneGridItem7.Controls.Add(this.bpPanelControl32);
            this.oneGridItem7.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem7.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem7.Name = "oneGridItem7";
            this.oneGridItem7.Size = new System.Drawing.Size(901, 23);
            this.oneGridItem7.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem7.TabIndex = 2;
            // 
            // bpPanelControl8
            // 
            this.bpPanelControl8.Controls.Add(this.txt주문번호);
            this.bpPanelControl8.Controls.Add(this.lbl주문번호);
            this.bpPanelControl8.Location = new System.Drawing.Point(598, 1);
            this.bpPanelControl8.Name = "bpPanelControl8";
            this.bpPanelControl8.Size = new System.Drawing.Size(296, 23);
            this.bpPanelControl8.TabIndex = 49;
            this.bpPanelControl8.Text = "bpPanelControl8";
            // 
            // txt주문번호
            // 
            this.txt주문번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt주문번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt주문번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt주문번호.Location = new System.Drawing.Point(81, 0);
            this.txt주문번호.Name = "txt주문번호";
            this.txt주문번호.Size = new System.Drawing.Size(215, 21);
            this.txt주문번호.TabIndex = 1;
            this.txt주문번호.Tag = "NO_PO_PARTNER";
            // 
            // lbl주문번호
            // 
            this.lbl주문번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl주문번호.Location = new System.Drawing.Point(0, 0);
            this.lbl주문번호.Name = "lbl주문번호";
            this.lbl주문번호.Size = new System.Drawing.Size(80, 23);
            this.lbl주문번호.TabIndex = 0;
            this.lbl주문번호.Text = "주문번호";
            this.lbl주문번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl31
            // 
            this.bpPanelControl31.Controls.Add(this.lbl지불조건);
            this.bpPanelControl31.Controls.Add(this.cbo지불조건);
            this.bpPanelControl31.Location = new System.Drawing.Point(300, 1);
            this.bpPanelControl31.Name = "bpPanelControl31";
            this.bpPanelControl31.Size = new System.Drawing.Size(296, 23);
            this.bpPanelControl31.TabIndex = 48;
            this.bpPanelControl31.Text = "bpPanelControl31";
            // 
            // lbl지불조건
            // 
            this.lbl지불조건.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl지불조건.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl지불조건.Location = new System.Drawing.Point(0, 0);
            this.lbl지불조건.Name = "lbl지불조건";
            this.lbl지불조건.Size = new System.Drawing.Size(80, 23);
            this.lbl지불조건.TabIndex = 17;
            this.lbl지불조건.Text = "지불조건";
            this.lbl지불조건.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo지불조건
            // 
            this.cbo지불조건.AutoDropDown = true;
            this.cbo지불조건.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo지불조건.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo지불조건.ItemHeight = 12;
            this.cbo지불조건.Location = new System.Drawing.Point(81, 0);
            this.cbo지불조건.Name = "cbo지불조건";
            this.cbo지불조건.Size = new System.Drawing.Size(215, 20);
            this.cbo지불조건.TabIndex = 47;
            this.cbo지불조건.Tag = "COND_PAY";
            // 
            // bpPanelControl32
            // 
            this.bpPanelControl32.Controls.Add(this.cboINCOTERMS지역);
            this.bpPanelControl32.Controls.Add(this.cbo선적조건);
            this.bpPanelControl32.Controls.Add(this.lbl선적조건);
            this.bpPanelControl32.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl32.Name = "bpPanelControl32";
            this.bpPanelControl32.Size = new System.Drawing.Size(296, 23);
            this.bpPanelControl32.TabIndex = 7;
            this.bpPanelControl32.Text = "bpPanelControl32";
            // 
            // cboINCOTERMS지역
            // 
            this.cboINCOTERMS지역.AutoDropDown = true;
            this.cboINCOTERMS지역.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboINCOTERMS지역.ItemHeight = 12;
            this.cboINCOTERMS지역.Location = new System.Drawing.Point(172, 1);
            this.cboINCOTERMS지역.Name = "cboINCOTERMS지역";
            this.cboINCOTERMS지역.Size = new System.Drawing.Size(124, 20);
            this.cboINCOTERMS지역.TabIndex = 49;
            this.cboINCOTERMS지역.Tag = "TP_TRANSPORT";
            // 
            // cbo선적조건
            // 
            this.cbo선적조건.AutoDropDown = true;
            this.cbo선적조건.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo선적조건.ItemHeight = 12;
            this.cbo선적조건.Location = new System.Drawing.Point(81, 1);
            this.cbo선적조건.Name = "cbo선적조건";
            this.cbo선적조건.Size = new System.Drawing.Size(85, 20);
            this.cbo선적조건.TabIndex = 48;
            this.cbo선적조건.Tag = "TP_TRANS";
            // 
            // lbl선적조건
            // 
            this.lbl선적조건.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl선적조건.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl선적조건.Location = new System.Drawing.Point(0, 0);
            this.lbl선적조건.Name = "lbl선적조건";
            this.lbl선적조건.Size = new System.Drawing.Size(80, 23);
            this.lbl선적조건.TabIndex = 21;
            this.lbl선적조건.Text = "선적조건";
            this.lbl선적조건.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl20);
            this.oneGridItem3.Controls.Add(this.bpPanelControl9);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 69);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(901, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 3;
            // 
            // bpPanelControl20
            // 
            this.bpPanelControl20.Controls.Add(this.txt호선명);
            this.bpPanelControl20.Controls.Add(this.ctx호선번호);
            this.bpPanelControl20.Controls.Add(this.lbl호선);
            this.bpPanelControl20.Location = new System.Drawing.Point(300, 1);
            this.bpPanelControl20.Name = "bpPanelControl20";
            this.bpPanelControl20.Size = new System.Drawing.Size(417, 23);
            this.bpPanelControl20.TabIndex = 4;
            this.bpPanelControl20.Text = "bpPanelControl20";
            // 
            // txt호선명
            // 
            this.txt호선명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt호선명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt호선명.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt호선명.Location = new System.Drawing.Point(218, 0);
            this.txt호선명.Name = "txt호선명";
            this.txt호선명.ReadOnly = true;
            this.txt호선명.Size = new System.Drawing.Size(199, 21);
            this.txt호선명.TabIndex = 29;
            this.txt호선명.TabStop = false;
            this.txt호선명.Tag = "NM_VESSEL";
            // 
            // ctx호선번호
            // 
            this.ctx호선번호.BackColor = System.Drawing.Color.White;
            this.ctx호선번호.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.ctx호선번호.ItemBackColor = System.Drawing.Color.Empty;
            this.ctx호선번호.Location = new System.Drawing.Point(81, 1);
            this.ctx호선번호.Name = "ctx호선번호";
            this.ctx호선번호.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.ctx호선번호.Size = new System.Drawing.Size(131, 21);
            this.ctx호선번호.TabIndex = 3;
            this.ctx호선번호.TabStop = false;
            this.ctx호선번호.Tag = "NO_IMO;NO_HULL";
            this.ctx호선번호.UserCodeName = "NO_HULL";
            this.ctx호선번호.UserCodeValue = "NO_IMO";
            this.ctx호선번호.UserHelpID = "H_CZ_MA_HULL_SUB";
            this.ctx호선번호.UserParams = "호선;H_CZ_MA_HULL_SUB";
            // 
            // lbl호선
            // 
            this.lbl호선.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl호선.Location = new System.Drawing.Point(0, 0);
            this.lbl호선.Name = "lbl호선";
            this.lbl호선.Size = new System.Drawing.Size(80, 23);
            this.lbl호선.TabIndex = 28;
            this.lbl호선.Text = "호선";
            this.lbl호선.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl9
            // 
            this.bpPanelControl9.Controls.Add(this.lbl화폐단위);
            this.bpPanelControl9.Controls.Add(this.cur환율);
            this.bpPanelControl9.Controls.Add(this.cbo화폐단위);
            this.bpPanelControl9.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl9.Name = "bpPanelControl9";
            this.bpPanelControl9.Size = new System.Drawing.Size(296, 23);
            this.bpPanelControl9.TabIndex = 3;
            this.bpPanelControl9.Text = "bpPanelControl9";
            // 
            // lbl화폐단위
            // 
            this.lbl화폐단위.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl화폐단위.Location = new System.Drawing.Point(0, 3);
            this.lbl화폐단위.Name = "lbl화폐단위";
            this.lbl화폐단위.Size = new System.Drawing.Size(80, 16);
            this.lbl화폐단위.TabIndex = 147;
            this.lbl화폐단위.Text = "화폐단위";
            this.lbl화폐단위.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cur환율
            // 
            this.cur환율.BackColor = System.Drawing.Color.White;
            this.cur환율.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur환율.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur환율.CurrencyDecimalDigits = 4;
            this.cur환율.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur환율.Font = new System.Drawing.Font("굴림체", 9F);
            this.cur환율.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur환율.Location = new System.Drawing.Point(219, 1);
            this.cur환율.MaxLength = 17;
            this.cur환율.Name = "cur환율";
            this.cur환율.NullString = "0";
            this.cur환율.PositiveColor = System.Drawing.Color.Black;
            this.cur환율.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur환율.Size = new System.Drawing.Size(77, 21);
            this.cur환율.TabIndex = 7;
            this.cur환율.Tag = "RT_EXCH";
            this.cur환율.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur환율.UseKeyF3 = false;
            // 
            // cbo화폐단위
            // 
            this.cbo화폐단위.AutoDropDown = true;
            this.cbo화폐단위.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo화폐단위.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo화폐단위.ItemHeight = 12;
            this.cbo화폐단위.Location = new System.Drawing.Point(81, 1);
            this.cbo화폐단위.Name = "cbo화폐단위";
            this.cbo화폐단위.Size = new System.Drawing.Size(136, 20);
            this.cbo화폐단위.TabIndex = 6;
            this.cbo화폐단위.Tag = "CD_EXCH";
            this.cbo화폐단위.UseKeyF3 = false;
            // 
            // oneGridItem4
            // 
            this.oneGridItem4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem4.Controls.Add(this.bpPanelControl7);
            this.oneGridItem4.Controls.Add(this.bpPanelControl11);
            this.oneGridItem4.Controls.Add(this.bpPanelControl12);
            this.oneGridItem4.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem4.Location = new System.Drawing.Point(0, 92);
            this.oneGridItem4.Name = "oneGridItem4";
            this.oneGridItem4.Size = new System.Drawing.Size(901, 23);
            this.oneGridItem4.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem4.TabIndex = 4;
            // 
            // bpPanelControl7
            // 
            this.bpPanelControl7.Controls.Add(this.lbl프로젝트);
            this.bpPanelControl7.Controls.Add(this.ctx프로젝트);
            this.bpPanelControl7.Controls.Add(this.btn프로젝트적용);
            this.bpPanelControl7.Location = new System.Drawing.Point(598, 1);
            this.bpPanelControl7.Name = "bpPanelControl7";
            this.bpPanelControl7.Size = new System.Drawing.Size(296, 23);
            this.bpPanelControl7.TabIndex = 5;
            this.bpPanelControl7.Text = "bpPanelControl7";
            // 
            // lbl프로젝트
            // 
            this.lbl프로젝트.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl프로젝트.Location = new System.Drawing.Point(0, 3);
            this.lbl프로젝트.Name = "lbl프로젝트";
            this.lbl프로젝트.Size = new System.Drawing.Size(80, 16);
            this.lbl프로젝트.TabIndex = 166;
            this.lbl프로젝트.Text = "프로젝트";
            this.lbl프로젝트.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx프로젝트
            // 
            this.ctx프로젝트.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctx프로젝트.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.ctx프로젝트.ItemBackColor = System.Drawing.Color.Empty;
            this.ctx프로젝트.Location = new System.Drawing.Point(81, 1);
            this.ctx프로젝트.Name = "ctx프로젝트";
            this.ctx프로젝트.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.ctx프로젝트.Size = new System.Drawing.Size(151, 21);
            this.ctx프로젝트.TabIndex = 9;
            this.ctx프로젝트.TabStop = false;
            this.ctx프로젝트.Tag = "NO_PROJECT;NM_PROJECT";
            this.ctx프로젝트.UserCodeName = "NM_PROJECT";
            this.ctx프로젝트.UserCodeValue = "NO_PROJECT";
            this.ctx프로젝트.UserHelpID = "H_SA_PRJ_SUB";
            // 
            // btn프로젝트적용
            // 
            this.btn프로젝트적용.BackColor = System.Drawing.Color.White;
            this.btn프로젝트적용.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn프로젝트적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn프로젝트적용.Location = new System.Drawing.Point(234, 2);
            this.btn프로젝트적용.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn프로젝트적용.Name = "btn프로젝트적용";
            this.btn프로젝트적용.Size = new System.Drawing.Size(62, 19);
            this.btn프로젝트적용.TabIndex = 217;
            this.btn프로젝트적용.TabStop = false;
            this.btn프로젝트적용.Text = "적용";
            this.btn프로젝트적용.UseVisualStyleBackColor = false;
            // 
            // bpPanelControl11
            // 
            this.bpPanelControl11.Controls.Add(this.lbl부가세포함);
            this.bpPanelControl11.Controls.Add(this.cbo부가세포함);
            this.bpPanelControl11.Location = new System.Drawing.Point(300, 1);
            this.bpPanelControl11.Name = "bpPanelControl11";
            this.bpPanelControl11.Size = new System.Drawing.Size(296, 23);
            this.bpPanelControl11.TabIndex = 4;
            this.bpPanelControl11.Text = "bpPanelControl11";
            // 
            // lbl부가세포함
            // 
            this.lbl부가세포함.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl부가세포함.Location = new System.Drawing.Point(0, 3);
            this.lbl부가세포함.Name = "lbl부가세포함";
            this.lbl부가세포함.Size = new System.Drawing.Size(80, 16);
            this.lbl부가세포함.TabIndex = 0;
            this.lbl부가세포함.Text = "부가세포함";
            this.lbl부가세포함.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo부가세포함
            // 
            this.cbo부가세포함.AutoDropDown = true;
            this.cbo부가세포함.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo부가세포함.Enabled = false;
            this.cbo부가세포함.ItemHeight = 12;
            this.cbo부가세포함.Location = new System.Drawing.Point(81, 1);
            this.cbo부가세포함.Name = "cbo부가세포함";
            this.cbo부가세포함.Size = new System.Drawing.Size(215, 20);
            this.cbo부가세포함.TabIndex = 12;
            this.cbo부가세포함.Tag = "FG_VAT";
            this.cbo부가세포함.UseKeyF3 = false;
            // 
            // bpPanelControl12
            // 
            this.bpPanelControl12.Controls.Add(this.lblVAT구분);
            this.bpPanelControl12.Controls.Add(this.cur부가세율);
            this.bpPanelControl12.Controls.Add(this.cbo부가세구분);
            this.bpPanelControl12.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl12.Name = "bpPanelControl12";
            this.bpPanelControl12.Size = new System.Drawing.Size(296, 23);
            this.bpPanelControl12.TabIndex = 3;
            this.bpPanelControl12.Text = "bpPanelControl12";
            // 
            // lblVAT구분
            // 
            this.lblVAT구분.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblVAT구분.Location = new System.Drawing.Point(0, 3);
            this.lblVAT구분.Name = "lblVAT구분";
            this.lblVAT구분.Size = new System.Drawing.Size(80, 16);
            this.lblVAT구분.TabIndex = 158;
            this.lblVAT구분.Text = "VAT구분";
            this.lblVAT구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cur부가세율
            // 
            this.cur부가세율.BackColor = System.Drawing.SystemColors.Control;
            this.cur부가세율.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur부가세율.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur부가세율.CurrencyDecimalDigits = 2;
            this.cur부가세율.CurrencyNegativePattern = 3;
            this.cur부가세율.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur부가세율.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cur부가세율.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur부가세율.Location = new System.Drawing.Point(234, 0);
            this.cur부가세율.MaxLength = 3;
            this.cur부가세율.Name = "cur부가세율";
            this.cur부가세율.NullString = "0";
            this.cur부가세율.PositiveColor = System.Drawing.Color.Black;
            this.cur부가세율.ReadOnly = true;
            this.cur부가세율.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur부가세율.Size = new System.Drawing.Size(62, 21);
            this.cur부가세율.TabIndex = 11;
            this.cur부가세율.TabStop = false;
            this.cur부가세율.Tag = "RT_VAT";
            this.cur부가세율.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur부가세율.UseKeyF3 = false;
            // 
            // cbo부가세구분
            // 
            this.cbo부가세구분.AutoDropDown = true;
            this.cbo부가세구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo부가세구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo부가세구분.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbo부가세구분.ItemHeight = 12;
            this.cbo부가세구분.Location = new System.Drawing.Point(81, 1);
            this.cbo부가세구분.Name = "cbo부가세구분";
            this.cbo부가세구분.Size = new System.Drawing.Size(151, 20);
            this.cbo부가세구분.TabIndex = 10;
            this.cbo부가세구분.Tag = "TP_VAT";
            this.cbo부가세구분.UseKeyF3 = false;
            // 
            // tpg매출정보
            // 
            this.tpg매출정보.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpg매출정보.Controls.Add(this.tableLayoutPanel3);
            this.tpg매출정보.ImageIndex = 5;
            this.tpg매출정보.Location = new System.Drawing.Point(4, 24);
            this.tpg매출정보.Name = "tpg매출정보";
            this.tpg매출정보.Size = new System.Drawing.Size(921, 172);
            this.tpg매출정보.TabIndex = 1;
            this.tpg매출정보.Text = "매출정보";
            this.tpg매출정보.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.imagePanel2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.pnl매출정보, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(917, 168);
            this.tableLayoutPanel3.TabIndex = 40;
            // 
            // imagePanel2
            // 
            this.imagePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.imagePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel2.LeftImage = null;
            this.imagePanel2.Location = new System.Drawing.Point(0, 0);
            this.imagePanel2.Margin = new System.Windows.Forms.Padding(0);
            this.imagePanel2.Name = "imagePanel2";
            this.imagePanel2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
            this.imagePanel2.PatternImage = null;
            this.imagePanel2.RightImage = null;
            this.imagePanel2.Size = new System.Drawing.Size(917, 28);
            this.imagePanel2.TabIndex = 39;
            this.imagePanel2.TitleText = "매출정보";
            // 
            // pnl매출정보
            // 
            this.pnl매출정보.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl매출정보.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem5,
            this.oneGridItem6,
            this.oneGridItem8});
            this.pnl매출정보.Location = new System.Drawing.Point(3, 31);
            this.pnl매출정보.Name = "pnl매출정보";
            this.pnl매출정보.Size = new System.Drawing.Size(911, 134);
            this.pnl매출정보.TabIndex = 0;
            // 
            // oneGridItem5
            // 
            this.oneGridItem5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem5.Controls.Add(this.bpPanelControl60);
            this.oneGridItem5.Controls.Add(this.bpPanelControl57);
            this.oneGridItem5.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem5.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem5.Name = "oneGridItem5";
            this.oneGridItem5.Size = new System.Drawing.Size(901, 23);
            this.oneGridItem5.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem5.TabIndex = 0;
            // 
            // bpPanelControl60
            // 
            this.bpPanelControl60.Controls.Add(this.dtp수금예정일자);
            this.bpPanelControl60.Controls.Add(this.lbl수금예정일자);
            this.bpPanelControl60.Location = new System.Drawing.Point(301, 1);
            this.bpPanelControl60.Name = "bpPanelControl60";
            this.bpPanelControl60.Size = new System.Drawing.Size(297, 23);
            this.bpPanelControl60.TabIndex = 2;
            this.bpPanelControl60.Text = "bpPanelControl60";
            // 
            // dtp수금예정일자
            // 
            this.dtp수금예정일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp수금예정일자.Location = new System.Drawing.Point(81, 1);
            this.dtp수금예정일자.Mask = "####/##/##";
            this.dtp수금예정일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp수금예정일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp수금예정일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp수금예정일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp수금예정일자.Modified = true;
            this.dtp수금예정일자.Name = "dtp수금예정일자";
            this.dtp수금예정일자.Size = new System.Drawing.Size(90, 21);
            this.dtp수금예정일자.TabIndex = 3;
            this.dtp수금예정일자.Tag = "DT_RCP_RSV";
            this.dtp수금예정일자.Value = new System.DateTime(((long)(0)));
            // 
            // lbl수금예정일자
            // 
            this.lbl수금예정일자.Location = new System.Drawing.Point(0, 3);
            this.lbl수금예정일자.Name = "lbl수금예정일자";
            this.lbl수금예정일자.Size = new System.Drawing.Size(80, 16);
            this.lbl수금예정일자.TabIndex = 2;
            this.lbl수금예정일자.Text = "수금예정일자";
            this.lbl수금예정일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl57
            // 
            this.bpPanelControl57.Controls.Add(this.dtp매출일자);
            this.bpPanelControl57.Controls.Add(this.lbl매출일자);
            this.bpPanelControl57.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl57.Name = "bpPanelControl57";
            this.bpPanelControl57.Size = new System.Drawing.Size(297, 23);
            this.bpPanelControl57.TabIndex = 1;
            this.bpPanelControl57.Text = "bpPanelControl57";
            // 
            // dtp매출일자
            // 
            this.dtp매출일자.BackColor = System.Drawing.Color.White;
            this.dtp매출일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp매출일자.Location = new System.Drawing.Point(81, 1);
            this.dtp매출일자.Mask = "####/##/##";
            this.dtp매출일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp매출일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp매출일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp매출일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp매출일자.Modified = true;
            this.dtp매출일자.Name = "dtp매출일자";
            this.dtp매출일자.Size = new System.Drawing.Size(90, 21);
            this.dtp매출일자.TabIndex = 1;
            this.dtp매출일자.Tag = "DT_PROCESS";
            this.dtp매출일자.Value = new System.DateTime(((long)(0)));
            // 
            // lbl매출일자
            // 
            this.lbl매출일자.Location = new System.Drawing.Point(0, 3);
            this.lbl매출일자.Name = "lbl매출일자";
            this.lbl매출일자.Size = new System.Drawing.Size(80, 16);
            this.lbl매출일자.TabIndex = 0;
            this.lbl매출일자.Text = "매출일자";
            this.lbl매출일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem6
            // 
            this.oneGridItem6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem6.Controls.Add(this.bpPanelControl10);
            this.oneGridItem6.Controls.Add(this.bpPanelControl65);
            this.oneGridItem6.Controls.Add(this.bpPanelControl66);
            this.oneGridItem6.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem6.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem6.Name = "oneGridItem6";
            this.oneGridItem6.Size = new System.Drawing.Size(901, 23);
            this.oneGridItem6.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem6.TabIndex = 1;
            // 
            // bpPanelControl10
            // 
            this.bpPanelControl10.Controls.Add(this.cur외화금액);
            this.bpPanelControl10.Controls.Add(this.lbl외화금액);
            this.bpPanelControl10.Location = new System.Drawing.Point(600, 1);
            this.bpPanelControl10.Name = "bpPanelControl10";
            this.bpPanelControl10.Size = new System.Drawing.Size(297, 23);
            this.bpPanelControl10.TabIndex = 6;
            this.bpPanelControl10.Text = "bpPanelControl10";
            // 
            // cur외화금액
            // 
            this.cur외화금액.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cur외화금액.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur외화금액.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur외화금액.CurrencyDecimalDigits = 2;
            this.cur외화금액.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur외화금액.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur외화금액.Location = new System.Drawing.Point(81, 1);
            this.cur외화금액.Name = "cur외화금액";
            this.cur외화금액.NullString = "0";
            this.cur외화금액.ReadOnly = true;
            this.cur외화금액.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur외화금액.Size = new System.Drawing.Size(215, 21);
            this.cur외화금액.TabIndex = 4;
            this.cur외화금액.TabStop = false;
            this.cur외화금액.Tag = "AM_IV_EX";
            this.cur외화금액.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl외화금액
            // 
            this.lbl외화금액.Location = new System.Drawing.Point(0, 3);
            this.lbl외화금액.Name = "lbl외화금액";
            this.lbl외화금액.Size = new System.Drawing.Size(80, 16);
            this.lbl외화금액.TabIndex = 3;
            this.lbl외화금액.Text = "외화금액";
            this.lbl외화금액.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl65
            // 
            this.bpPanelControl65.Controls.Add(this.cur부가세액);
            this.bpPanelControl65.Controls.Add(this.lbl부가세액);
            this.bpPanelControl65.Location = new System.Drawing.Point(301, 1);
            this.bpPanelControl65.Name = "bpPanelControl65";
            this.bpPanelControl65.Size = new System.Drawing.Size(297, 23);
            this.bpPanelControl65.TabIndex = 5;
            this.bpPanelControl65.Text = "bpPanelControl65";
            // 
            // cur부가세액
            // 
            this.cur부가세액.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cur부가세액.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur부가세액.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur부가세액.CurrencyDecimalDigits = 2;
            this.cur부가세액.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur부가세액.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur부가세액.Location = new System.Drawing.Point(81, 1);
            this.cur부가세액.Name = "cur부가세액";
            this.cur부가세액.NullString = "0";
            this.cur부가세액.ReadOnly = true;
            this.cur부가세액.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur부가세액.Size = new System.Drawing.Size(215, 21);
            this.cur부가세액.TabIndex = 4;
            this.cur부가세액.TabStop = false;
            this.cur부가세액.Tag = "AM_IV_VAT";
            this.cur부가세액.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl부가세액
            // 
            this.lbl부가세액.Location = new System.Drawing.Point(0, 3);
            this.lbl부가세액.Name = "lbl부가세액";
            this.lbl부가세액.Size = new System.Drawing.Size(80, 16);
            this.lbl부가세액.TabIndex = 3;
            this.lbl부가세액.Text = "부가세액";
            this.lbl부가세액.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl66
            // 
            this.bpPanelControl66.Controls.Add(this.cur공급가액);
            this.bpPanelControl66.Controls.Add(this.lbl공급가액);
            this.bpPanelControl66.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl66.Name = "bpPanelControl66";
            this.bpPanelControl66.Size = new System.Drawing.Size(297, 23);
            this.bpPanelControl66.TabIndex = 4;
            this.bpPanelControl66.Text = "bpPanelControl66";
            // 
            // cur공급가액
            // 
            this.cur공급가액.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cur공급가액.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.cur공급가액.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cur공급가액.CurrencyDecimalDigits = 2;
            this.cur공급가액.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur공급가액.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur공급가액.Location = new System.Drawing.Point(81, 1);
            this.cur공급가액.Name = "cur공급가액";
            this.cur공급가액.NullString = "0";
            this.cur공급가액.ReadOnly = true;
            this.cur공급가액.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur공급가액.Size = new System.Drawing.Size(215, 21);
            this.cur공급가액.TabIndex = 3;
            this.cur공급가액.TabStop = false;
            this.cur공급가액.Tag = "AM_IV";
            this.cur공급가액.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl공급가액
            // 
            this.lbl공급가액.Location = new System.Drawing.Point(0, 3);
            this.lbl공급가액.Name = "lbl공급가액";
            this.lbl공급가액.Size = new System.Drawing.Size(80, 16);
            this.lbl공급가액.TabIndex = 2;
            this.lbl공급가액.Text = "공급가액";
            this.lbl공급가액.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem8
            // 
            this.oneGridItem8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem8.Controls.Add(this.bpPanelControl13);
            this.oneGridItem8.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem8.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem8.Name = "oneGridItem8";
            this.oneGridItem8.Size = new System.Drawing.Size(901, 23);
            this.oneGridItem8.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem8.TabIndex = 2;
            // 
            // bpPanelControl13
            // 
            this.bpPanelControl13.Controls.Add(this.txt매출비고);
            this.bpPanelControl13.Controls.Add(this.lbl매출비고);
            this.bpPanelControl13.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl13.Name = "bpPanelControl13";
            this.bpPanelControl13.Size = new System.Drawing.Size(895, 23);
            this.bpPanelControl13.TabIndex = 0;
            this.bpPanelControl13.Text = "bpPanelControl13";
            // 
            // txt매출비고
            // 
            this.txt매출비고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt매출비고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt매출비고.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt매출비고.Location = new System.Drawing.Point(81, 0);
            this.txt매출비고.Name = "txt매출비고";
            this.txt매출비고.Size = new System.Drawing.Size(814, 21);
            this.txt매출비고.TabIndex = 1;
            this.txt매출비고.Tag = "DC_RMK_IVH";
            // 
            // lbl매출비고
            // 
            this.lbl매출비고.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl매출비고.Location = new System.Drawing.Point(0, 0);
            this.lbl매출비고.Name = "lbl매출비고";
            this.lbl매출비고.Size = new System.Drawing.Size(80, 23);
            this.lbl매출비고.TabIndex = 0;
            this.lbl매출비고.Text = "매출비고";
            this.lbl매출비고.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // img
            // 
            this.img.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img.ImageStream")));
            this.img.TransparentColor = System.Drawing.Color.Transparent;
            this.img.Images.SetKeyName(0, "tab_icon_0004.gif");
            this.img.Images.SetKeyName(1, "tab_icon_02.gif");
            this.img.Images.SetKeyName(2, "tab_icon_hr001.gif");
            this.img.Images.SetKeyName(3, "tab_icon_fi_ze02.gif");
            this.img.Images.SetKeyName(4, "tab_icon_fi_cnt07.gif");
            this.img.Images.SetKeyName(5, "tab_icon_hr_ple01.gif");
            // 
            // roundedButton1
            // 
            this.roundedButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.roundedButton1.BackColor = System.Drawing.Color.White;
            this.roundedButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButton1.Location = new System.Drawing.Point(432, 300);
            this.roundedButton1.MaximumSize = new System.Drawing.Size(0, 19);
            this.roundedButton1.Name = "roundedButton1";
            this.roundedButton1.Size = new System.Drawing.Size(78, 19);
            this.roundedButton1.TabIndex = 123;
            this.roundedButton1.TabStop = false;
            this.roundedButton1.Text = "업체전용3";
            this.roundedButton1.UseVisualStyleBackColor = true;
            this.roundedButton1.Visible = false;
            // 
            // btn자동삭제
            // 
            this.btn자동삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn자동삭제.BackColor = System.Drawing.Color.Transparent;
            this.btn자동삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn자동삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn자동삭제.Location = new System.Drawing.Point(849, 14);
            this.btn자동삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn자동삭제.Name = "btn자동삭제";
            this.btn자동삭제.Size = new System.Drawing.Size(86, 19);
            this.btn자동삭제.TabIndex = 124;
            this.btn자동삭제.TabStop = false;
            this.btn자동삭제.Text = "자동삭제";
            this.btn자동삭제.UseVisualStyleBackColor = false;
            this.btn자동삭제.Visible = false;
            // 
            // oneGridItem9
            // 
            this.oneGridItem9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem9.Controls.Add(this.bpPanelControl14);
            this.oneGridItem9.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem9.Location = new System.Drawing.Point(0, 115);
            this.oneGridItem9.Name = "oneGridItem9";
            this.oneGridItem9.Size = new System.Drawing.Size(901, 23);
            this.oneGridItem9.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem9.TabIndex = 5;
            // 
            // bpPanelControl14
            // 
            this.bpPanelControl14.Controls.Add(this.txt비고);
            this.bpPanelControl14.Controls.Add(this.lbl비고);
            this.bpPanelControl14.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl14.Name = "bpPanelControl14";
            this.bpPanelControl14.Size = new System.Drawing.Size(895, 23);
            this.bpPanelControl14.TabIndex = 0;
            this.bpPanelControl14.Text = "bpPanelControl14";
            // 
            // lbl비고
            // 
            this.lbl비고.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl비고.Location = new System.Drawing.Point(0, 0);
            this.lbl비고.Name = "lbl비고";
            this.lbl비고.Size = new System.Drawing.Size(80, 23);
            this.lbl비고.TabIndex = 0;
            this.lbl비고.Text = "비고";
            this.lbl비고.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt비고
            // 
            this.txt비고.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt비고.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt비고.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt비고.Location = new System.Drawing.Point(81, 1);
            this.txt비고.Name = "txt비고";
            this.txt비고.Size = new System.Drawing.Size(814, 21);
            this.txt비고.TabIndex = 1;
            this.txt비고.Tag = "DC_RMK";
            // 
            // P_CZ_SA_SO
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btn자동삭제);
            this.Controls.Add(this.roundedButton1);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_CZ_SA_SO";
            this.Size = new System.Drawing.Size(935, 619);
            this.TitleText = "청구등록";
            this.Controls.SetChildIndex(this.roundedButton1, 0);
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.btn자동삭제, 0);
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.imagePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tpg기본정보.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp청구일자)).EndInit();
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl1.PerformLayout();
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.oneGridItem7.ResumeLayout(false);
            this.bpPanelControl8.ResumeLayout(false);
            this.bpPanelControl8.PerformLayout();
            this.bpPanelControl31.ResumeLayout(false);
            this.bpPanelControl32.ResumeLayout(false);
            this.oneGridItem3.ResumeLayout(false);
            this.bpPanelControl20.ResumeLayout(false);
            this.bpPanelControl20.PerformLayout();
            this.bpPanelControl9.ResumeLayout(false);
            this.bpPanelControl9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur환율)).EndInit();
            this.oneGridItem4.ResumeLayout(false);
            this.bpPanelControl7.ResumeLayout(false);
            this.bpPanelControl11.ResumeLayout(false);
            this.bpPanelControl12.ResumeLayout(false);
            this.bpPanelControl12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur부가세율)).EndInit();
            this.tpg매출정보.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.oneGridItem5.ResumeLayout(false);
            this.bpPanelControl60.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp수금예정일자)).EndInit();
            this.bpPanelControl57.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp매출일자)).EndInit();
            this.oneGridItem6.ResumeLayout(false);
            this.bpPanelControl10.ResumeLayout(false);
            this.bpPanelControl10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur외화금액)).EndInit();
            this.bpPanelControl65.ResumeLayout(false);
            this.bpPanelControl65.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur부가세액)).EndInit();
            this.bpPanelControl66.ResumeLayout(false);
            this.bpPanelControl66.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur공급가액)).EndInit();
            this.oneGridItem8.ResumeLayout(false);
            this.bpPanelControl13.ResumeLayout(false);
            this.bpPanelControl13.PerformLayout();
            this.oneGridItem9.ResumeLayout(false);
            this.bpPanelControl14.ResumeLayout(false);
            this.bpPanelControl14.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Duzon.Common.Controls.TabControlExt tabControl;
        private System.Windows.Forms.TabPage tpg기본정보;
        private Duzon.Common.BpControls.BpCodeTextBox ctx수주형태;
        private Duzon.Common.BpControls.BpCodeTextBox ctx담당자;
        private Duzon.Common.BpControls.BpCodeTextBox ctx영업그룹;
        private Duzon.Common.BpControls.BpCodeTextBox ctx매출처;
        private Duzon.Common.Controls.DatePicker dtp청구일자;
        private Duzon.Common.Controls.LabelExt lbl청구일자;
        private Duzon.Common.Controls.LabelExt lbl담당자;
        private Duzon.Common.Controls.LabelExt lbl부가세포함;
        private Duzon.Common.Controls.LabelExt lbl매출처;
        private Duzon.Common.Controls.LabelExt lbl수주형태;
        private Duzon.Common.Controls.LabelExt lbl프로젝트;
        private Duzon.Common.Controls.LabelExt lbl청구번호;
        private Duzon.Common.Controls.LabelExt lbl영업그룹;
        private Duzon.Common.Controls.LabelExt lbl화폐단위;
        private Duzon.Common.Controls.LabelExt lblVAT구분;
        private Duzon.Common.Controls.TextBoxExt txt청구번호;
        private Duzon.Common.Controls.CurrencyTextBox cur환율;
        private Duzon.Common.Controls.DropDownComboBox cbo화폐단위;
        private Duzon.Common.Controls.CurrencyTextBox cur부가세율;
        private Duzon.Common.Controls.DropDownComboBox cbo부가세구분;
        private Duzon.Common.Controls.DropDownComboBox cbo부가세포함;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.BpControls.BpCodeTextBox ctx프로젝트;
        private System.Windows.Forms.ImageList img;
        private Duzon.Common.Controls.RoundedButton btn프로젝트적용;
        private Duzon.Erpiu.Windows.OneControls.OneGrid pnl기본정보;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem4;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl9;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl11;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl12;
        private Duzon.Common.Controls.ImagePanel imagePanel;
        private Duzon.Common.Controls.RoundedButton roundedButton1;
        private Duzon.Common.Controls.DropDownComboBox cbo청구구분;
        private System.Windows.Forms.TabPage tpg매출정보;
        private Duzon.Common.Controls.ImagePanel imagePanel1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.Controls.RoundedButton btn삭제;
        private Duzon.Common.Controls.RoundedButton btn추가;
        private Duzon.Common.Controls.ImagePanel imagePanel2;
        private Duzon.Erpiu.Windows.OneControls.OneGrid pnl매출정보;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem5;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl60;
        private Duzon.Common.Controls.DatePicker dtp수금예정일자;
        private Duzon.Common.Controls.LabelExt lbl수금예정일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl57;
        private Duzon.Common.Controls.DatePicker dtp매출일자;
        private Duzon.Common.Controls.LabelExt lbl매출일자;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem6;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl65;
        private Duzon.Common.Controls.CurrencyTextBox cur부가세액;
        private Duzon.Common.Controls.LabelExt lbl부가세액;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl66;
        private Duzon.Common.Controls.CurrencyTextBox cur공급가액;
        private Duzon.Common.Controls.LabelExt lbl공급가액;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl20;
        private Duzon.Common.Controls.TextBoxExt txt호선명;
        private Duzon.Common.BpControls.BpCodeTextBox ctx호선번호;
        private Duzon.Common.Controls.LabelExt lbl호선;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem7;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl32;
        private Duzon.Common.Controls.DropDownComboBox cboINCOTERMS지역;
        private Duzon.Common.Controls.DropDownComboBox cbo선적조건;
        private Duzon.Common.Controls.LabelExt lbl선적조건;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl31;
        private Duzon.Common.Controls.LabelExt lbl지불조건;
        private Duzon.Common.Controls.DropDownComboBox cbo지불조건;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl8;
        private Duzon.Common.Controls.TextBoxExt txt주문번호;
        private Duzon.Common.Controls.LabelExt lbl주문번호;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl10;
        private Duzon.Common.Controls.CurrencyTextBox cur외화금액;
        private Duzon.Common.Controls.LabelExt lbl외화금액;
        private Duzon.Common.Controls.RoundedButton btn자동삭제;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem8;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl13;
        private Duzon.Common.Controls.TextBoxExt txt매출비고;
        private Duzon.Common.Controls.LabelExt lbl매출비고;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem9;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl14;
        private Duzon.Common.Controls.TextBoxExt txt비고;
        private Duzon.Common.Controls.LabelExt lbl비고;
    }
}

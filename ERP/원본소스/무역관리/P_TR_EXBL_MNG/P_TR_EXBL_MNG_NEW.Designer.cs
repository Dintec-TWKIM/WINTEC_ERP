namespace trade
{
    partial class P_TR_EXBL_MNG_NEW
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_TR_EXBL_MNG_NEW));
            this.tblLayout = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.label7 = new Duzon.Common.Controls.LabelExt();
            this.bpc거래처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.pp기표기간 = new Duzon.Common.Controls.PeriodPicker();
            this.label5 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpBizarea = new Duzon.Common.BpControls.BpComboBox();
            this.label2 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.cbo전표처리여부 = new Duzon.Common.Controls.DropDownComboBox();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.label4 = new Duzon.Common.Controls.LabelExt();
            this.bpc담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.label3 = new Duzon.Common.Controls.LabelExt();
            this.bpc영업그룹 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl9 = new Duzon.Common.BpControls.BpPanelControl();
            this.bp프로젝트 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl프로젝트 = new Duzon.Common.Controls.LabelExt();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btn전표발행취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn전표발행 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this.tblLayout.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.oneGridItem3.SuspendLayout();
            this.bpPanelControl9.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tblLayout);
            // 
            // tblLayout
            // 
            this.tblLayout.ColumnCount = 1;
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblLayout.Controls.Add(this.splitContainer1, 0, 1);
            this.tblLayout.Controls.Add(this.oneGrid1, 0, 0);
            this.tblLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayout.Location = new System.Drawing.Point(0, 0);
            this.tblLayout.Name = "tblLayout";
            this.tblLayout.RowCount = 2;
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayout.Size = new System.Drawing.Size(827, 579);
            this.tblLayout.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 94);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._flexH);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._flexL);
            this.splitContainer1.Size = new System.Drawing.Size(821, 482);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.TabIndex = 134;
            // 
            // _flexH
            // 
            this._flexH.AddMyMenu = true;
            this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexH.AutoResize = false;
            this._flexH.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexH.EnabledHeaderCheck = true;
            this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexH.Location = new System.Drawing.Point(0, 0);
            this._flexH.Name = "_flexH";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 20;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(821, 227);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 1;
            this._flexH.UseGridCalculator = true;
            // 
            // _flexL
            // 
            this._flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexL.AutoResize = false;
            this._flexL.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexL.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexL.EnabledHeaderCheck = true;
            this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexL.Location = new System.Drawing.Point(0, 0);
            this._flexL.Name = "_flexL";
            this._flexL.Rows.Count = 1;
            this._flexL.Rows.DefaultSize = 20;
            this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexL.ShowSort = false;
            this._flexL.Size = new System.Drawing.Size(821, 251);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 0;
            this._flexL.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(821, 85);
            this.oneGrid1.TabIndex = 135;
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
            this.oneGridItem1.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.label7);
            this.bpPanelControl3.Controls.Add(this.bpc거래처);
            this.bpPanelControl3.Location = new System.Drawing.Point(540, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 3);
            this.label7.Name = "label7";
            this.label7.Resizeble = true;
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "거래처";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpc거래처
            // 
            this.bpc거래처.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc거래처.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc거래처.ButtonImage")));
            this.bpc거래처.ChildMode = "";
            this.bpc거래처.CodeName = "";
            this.bpc거래처.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc거래처.CodeValue = "";
            this.bpc거래처.ComboCheck = true;
            this.bpc거래처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc거래처.IsCodeValueToUpper = true;
            this.bpc거래처.ItemBackColor = System.Drawing.Color.White;
            this.bpc거래처.Location = new System.Drawing.Point(81, 1);
            this.bpc거래처.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc거래처.Name = "bpc거래처";
            this.bpc거래처.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc거래처.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc거래처.SearchCode = true;
            this.bpc거래처.SelectCount = 0;
            this.bpc거래처.SetDefaultValue = false;
            this.bpc거래처.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc거래처.Size = new System.Drawing.Size(185, 21);
            this.bpc거래처.TabIndex = 140;
            this.bpc거래처.TabStop = false;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.pp기표기간);
            this.bpPanelControl2.Controls.Add(this.label5);
            this.bpPanelControl2.Location = new System.Drawing.Point(271, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // pp기표기간
            // 
            this.pp기표기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pp기표기간.IsNecessaryCondition = true;
            this.pp기표기간.Location = new System.Drawing.Point(81, 1);
            this.pp기표기간.Mask = "####/##/##";
            this.pp기표기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.pp기표기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.pp기표기간.Name = "pp기표기간";
            this.pp기표기간.Size = new System.Drawing.Size(185, 21);
            this.pp기표기간.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 3);
            this.label5.Name = "label5";
            this.label5.Resizeble = true;
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "기표기간";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.bpBizarea);
            this.bpPanelControl1.Controls.Add(this.label2);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // bpBizarea
            // 
            this.bpBizarea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.bpBizarea.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpBizarea.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpBizarea.ButtonImage")));
            this.bpBizarea.ChildMode = "";
            this.bpBizarea.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpBizarea.ComboCheck = true;
            this.bpBizarea.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BIZAREA_SUB1;
            this.bpBizarea.IsCodeValueToUpper = true;
            this.bpBizarea.ItemBackColor = System.Drawing.SystemColors.Window;
            this.bpBizarea.Location = new System.Drawing.Point(81, 1);
            this.bpBizarea.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpBizarea.Name = "bpBizarea";
            this.bpBizarea.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpBizarea.SearchCode = true;
            this.bpBizarea.SelectCount = 0;
            this.bpBizarea.SelectedIndex = -1;
            this.bpBizarea.SelectedItem = null;
            this.bpBizarea.SelectedText = "";
            this.bpBizarea.SelectedValue = null;
            this.bpBizarea.SetDefaultValue = false;
            this.bpBizarea.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpBizarea.Size = new System.Drawing.Size(185, 21);
            this.bpBizarea.TabIndex = 148;
            this.bpBizarea.TabStop = false;
            this.bpBizarea.Text = "bpComboBox1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 3);
            this.label2.Name = "label2";
            this.label2.Resizeble = true;
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "사업장";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.oneGridItem2.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.labelExt1);
            this.bpPanelControl4.Controls.Add(this.cbo전표처리여부);
            this.bpPanelControl4.Location = new System.Drawing.Point(540, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl4.TabIndex = 5;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // labelExt1
            // 
            this.labelExt1.Location = new System.Drawing.Point(0, 3);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(80, 16);
            this.labelExt1.TabIndex = 2;
            this.labelExt1.Text = "전표처리";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbo전표처리여부
            // 
            this.cbo전표처리여부.AutoDropDown = true;
            this.cbo전표처리여부.BackColor = System.Drawing.Color.White;
            this.cbo전표처리여부.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo전표처리여부.ItemHeight = 12;
            this.cbo전표처리여부.Location = new System.Drawing.Point(81, 1);
            this.cbo전표처리여부.Name = "cbo전표처리여부";
            this.cbo전표처리여부.ShowCheckBox = false;
            this.cbo전표처리여부.Size = new System.Drawing.Size(185, 20);
            this.cbo전표처리여부.TabIndex = 146;
            this.cbo전표처리여부.UseKeyEnter = false;
            this.cbo전표처리여부.UseKeyF3 = false;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.label4);
            this.bpPanelControl5.Controls.Add(this.bpc담당자);
            this.bpPanelControl5.Location = new System.Drawing.Point(271, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl5.TabIndex = 4;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 3);
            this.label4.Name = "label4";
            this.label4.Resizeble = true;
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "담당자";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpc담당자
            // 
            this.bpc담당자.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc담당자.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc담당자.ButtonImage")));
            this.bpc담당자.ChildMode = "";
            this.bpc담당자.CodeName = "";
            this.bpc담당자.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc담당자.CodeValue = "";
            this.bpc담당자.ComboCheck = true;
            this.bpc담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.bpc담당자.IsCodeValueToUpper = true;
            this.bpc담당자.ItemBackColor = System.Drawing.Color.White;
            this.bpc담당자.Location = new System.Drawing.Point(81, 1);
            this.bpc담당자.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc담당자.Name = "bpc담당자";
            this.bpc담당자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc담당자.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc담당자.SearchCode = true;
            this.bpc담당자.SelectCount = 0;
            this.bpc담당자.SetDefaultValue = false;
            this.bpc담당자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc담당자.Size = new System.Drawing.Size(185, 21);
            this.bpc담당자.TabIndex = 142;
            this.bpc담당자.TabStop = false;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.label3);
            this.bpPanelControl6.Controls.Add(this.bpc영업그룹);
            this.bpPanelControl6.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl6.TabIndex = 3;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 3);
            this.label3.Name = "label3";
            this.label3.Resizeble = true;
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "영업그룹";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpc영업그룹
            // 
            this.bpc영업그룹.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc영업그룹.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc영업그룹.ButtonImage")));
            this.bpc영업그룹.ChildMode = "";
            this.bpc영업그룹.CodeName = "";
            this.bpc영업그룹.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc영업그룹.CodeValue = "";
            this.bpc영업그룹.ComboCheck = true;
            this.bpc영업그룹.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SALEGRP_SUB;
            this.bpc영업그룹.IsCodeValueToUpper = true;
            this.bpc영업그룹.ItemBackColor = System.Drawing.Color.White;
            this.bpc영업그룹.Location = new System.Drawing.Point(81, 1);
            this.bpc영업그룹.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc영업그룹.Name = "bpc영업그룹";
            this.bpc영업그룹.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc영업그룹.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc영업그룹.SearchCode = true;
            this.bpc영업그룹.SelectCount = 0;
            this.bpc영업그룹.SetDefaultValue = false;
            this.bpc영업그룹.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc영업그룹.Size = new System.Drawing.Size(185, 21);
            this.bpc영업그룹.TabIndex = 141;
            this.bpc영업그룹.TabStop = false;
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl9);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 2;
            // 
            // bpPanelControl9
            // 
            this.bpPanelControl9.Controls.Add(this.bp프로젝트);
            this.bpPanelControl9.Controls.Add(this.lbl프로젝트);
            this.bpPanelControl9.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl9.Name = "bpPanelControl9";
            this.bpPanelControl9.Size = new System.Drawing.Size(267, 22);
            this.bpPanelControl9.TabIndex = 3;
            this.bpPanelControl9.Text = "bpPanelControl9";
            // 
            // bp프로젝트
            // 
            this.bp프로젝트.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp프로젝트.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp프로젝트.ButtonImage")));
            this.bp프로젝트.ChildMode = "";
            this.bp프로젝트.CodeName = "";
            this.bp프로젝트.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp프로젝트.CodeValue = "";
            this.bp프로젝트.ComboCheck = true;
            this.bp프로젝트.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.bp프로젝트.IsCodeValueToUpper = true;
            this.bp프로젝트.ItemBackColor = System.Drawing.Color.White;
            this.bp프로젝트.Location = new System.Drawing.Point(81, 1);
            this.bp프로젝트.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp프로젝트.Name = "bp프로젝트";
            this.bp프로젝트.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp프로젝트.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bp프로젝트.SearchCode = true;
            this.bp프로젝트.SelectCount = 0;
            this.bp프로젝트.SetDefaultValue = false;
            this.bp프로젝트.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp프로젝트.Size = new System.Drawing.Size(185, 21);
            this.bp프로젝트.TabIndex = 147;
            this.bp프로젝트.TabStop = false;
            this.bp프로젝트.Tag = "CD_PJT;NM_PJT";
            this.bp프로젝트.UserCodeName = "NM_PROJECT";
            this.bp프로젝트.UserCodeValue = "NO_PROJECT";
            this.bp프로젝트.UserHelpID = "H_SA_PRJ_SUB";
            this.bp프로젝트.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryBefore);
            // 
            // lbl프로젝트
            // 
            this.lbl프로젝트.Location = new System.Drawing.Point(0, 3);
            this.lbl프로젝트.Name = "lbl프로젝트";
            this.lbl프로젝트.Resizeble = true;
            this.lbl프로젝트.Size = new System.Drawing.Size(80, 16);
            this.lbl프로젝트.TabIndex = 3;
            this.lbl프로젝트.Text = "프로젝트";
            this.lbl프로젝트.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "FILE_ICON.gif");
            // 
            // btn전표발행취소
            // 
            this.btn전표발행취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn전표발행취소.BackColor = System.Drawing.Color.White;
            this.btn전표발행취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn전표발행취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn전표발행취소.Location = new System.Drawing.Point(730, 10);
            this.btn전표발행취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn전표발행취소.Name = "btn전표발행취소";
            this.btn전표발행취소.Size = new System.Drawing.Size(89, 19);
            this.btn전표발행취소.TabIndex = 137;
            this.btn전표발행취소.TabStop = false;
            this.btn전표발행취소.Text = "전표발행취소";
            this.btn전표발행취소.UseVisualStyleBackColor = false;
            this.btn전표발행취소.Click += new System.EventHandler(this.btn전표발행취소_Click);
            // 
            // btn전표발행
            // 
            this.btn전표발행.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn전표발행.BackColor = System.Drawing.Color.White;
            this.btn전표발행.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn전표발행.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn전표발행.Location = new System.Drawing.Point(663, 10);
            this.btn전표발행.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn전표발행.Name = "btn전표발행";
            this.btn전표발행.Size = new System.Drawing.Size(64, 19);
            this.btn전표발행.TabIndex = 136;
            this.btn전표발행.TabStop = false;
            this.btn전표발행.Text = "전표발행";
            this.btn전표발행.UseVisualStyleBackColor = false;
            this.btn전표발행.Click += new System.EventHandler(this.btn전표발행_Click);
            // 
            // P_TR_EXBL_MNG_NEW
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btn전표발행취소);
            this.Controls.Add(this.btn전표발행);
            this.Name = "P_TR_EXBL_MNG_NEW";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.btn전표발행, 0);
            this.Controls.SetChildIndex(this.btn전표발행취소, 0);
            this.mDataArea.ResumeLayout(false);
            this.tblLayout.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.oneGridItem3.ResumeLayout(false);
            this.bpPanelControl9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLayout;
        private Duzon.Common.BpControls.BpCodeTextBox bpc담당자;
        private Duzon.Common.BpControls.BpCodeTextBox bpc영업그룹;
        private Duzon.Common.BpControls.BpCodeTextBox bpc거래처;
        private Duzon.Common.Controls.LabelExt label7;
        private Duzon.Common.Controls.LabelExt label4;
        private Duzon.Common.Controls.LabelExt label5;
        private Duzon.Common.Controls.LabelExt label3;
        private Duzon.Common.Controls.LabelExt label2;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Dass.FlexGrid.FlexGrid _flexH;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Duzon.Common.Controls.RoundedButton btn전표발행취소;
        private Duzon.Common.Controls.RoundedButton btn전표발행;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.DropDownComboBox cbo전표처리여부;
        private Duzon.Common.Controls.LabelExt lbl프로젝트;
        private Duzon.Common.BpControls.BpCodeTextBox bp프로젝트;
        private Duzon.Common.BpControls.BpComboBox bpBizarea;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.PeriodPicker pp기표기간;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl9;
    }
}

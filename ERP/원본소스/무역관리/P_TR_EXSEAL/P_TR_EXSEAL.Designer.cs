namespace trade
{
    partial class P_TR_EXSEAL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_TR_EXSEAL));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.m_lblClText = new Duzon.Common.Controls.LabelExt();
            this.m_pnlMain = new System.Windows.Forms.Panel();
            this.panelExt2 = new Duzon.Common.Controls.PanelExt();
            this.panelExt3 = new Duzon.Common.Controls.PanelExt();
            this.panel18 = new Duzon.Common.Controls.PanelExt();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl사업장 = new Duzon.Common.Controls.LabelExt();
            this.m_lblAmtIssueDt = new Duzon.Common.Controls.LabelExt();
            this.m_lblTransferDt = new Duzon.Common.Controls.LabelExt();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl거래처 = new Duzon.Common.Controls.LabelExt();
            this.m_lblDtSeal = new Duzon.Common.Controls.LabelExt();
            this.m_lblDtIssue = new Duzon.Common.Controls.LabelExt();
            this.m_lblRemark = new Duzon.Common.Controls.LabelExt();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lblNoSeal = new Duzon.Common.Controls.LabelExt();
            this.m_lblNmEmp = new Duzon.Common.Controls.LabelExt();
            this.m_lblAmtSeal = new Duzon.Common.Controls.LabelExt();
            this.m_lblExpiryDate = new Duzon.Common.Controls.LabelExt();
            this.bpc담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc사업장 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc거래처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.cur발행원화금액 = new Duzon.Common.Controls.CurrencyTextBox();
            this.dtp발행일자 = new Duzon.Common.Controls.DatePicker();
            this.dtp유효일자 = new Duzon.Common.Controls.DatePicker();
            this.dtp인도일자 = new Duzon.Common.Controls.DatePicker();
            this.dtp물품인수일자 = new Duzon.Common.Controls.DatePicker();
            this.txt비고 = new Duzon.Common.Controls.TextBoxExt();
            this.txt인수증발급번호 = new Duzon.Common.Controls.TextBoxExt();
            this.cur물품인수금액 = new Duzon.Common.Controls.CurrencyTextBox();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.btn계산서적용 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panelExt4 = new Duzon.Common.Controls.PanelExt();
            this.m_curCdExch = new Duzon.Common.Controls.CurrencyTextBox();
            this.m_cboCdExch = new Duzon.Common.Controls.DropDownComboBox();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.txt마감번호 = new Duzon.Common.Controls.TextBoxExt();
            this.labelExt2 = new Duzon.Common.Controls.LabelExt();
            this.curAmBan_ex = new Duzon.Common.Controls.CurrencyTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelExt3 = new Duzon.Common.Controls.LabelExt();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelExt1.SuspendLayout();
            this.m_pnlMain.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cur발행원화금액)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp발행일자)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp유효일자)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp인도일자)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp물품인수일자)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur물품인수금액)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_curCdExch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.curAmBan_ex)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.m_pnlMain, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelExt1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 143F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 577);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelExt1
            // 
            this.panelExt1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt1.BackgroundImage")));
            this.panelExt1.Controls.Add(this.m_lblClText);
            this.panelExt1.Location = new System.Drawing.Point(3, 146);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(787, 25);
            this.panelExt1.TabIndex = 140;
            // 
            // m_lblClText
            // 
            this.m_lblClText.BackColor = System.Drawing.Color.Transparent;
            this.m_lblClText.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.m_lblClText.ForeColor = System.Drawing.Color.Black;
            this.m_lblClText.Location = new System.Drawing.Point(20, 5);
            this.m_lblClText.Name = "m_lblClText";
            this.m_lblClText.Resizeble = true;
            this.m_lblClText.Size = new System.Drawing.Size(200, 15);
            this.m_lblClText.TabIndex = 0;
            this.m_lblClText.Tag = "CL_TEXT";
            this.m_lblClText.Text = "등록내역";
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.Controls.Add(this.panel4);
            this.m_pnlMain.Controls.Add(this.curAmBan_ex);
            this.m_pnlMain.Controls.Add(this.txt마감번호);
            this.m_pnlMain.Controls.Add(this.m_curCdExch);
            this.m_pnlMain.Controls.Add(this.m_cboCdExch);
            this.m_pnlMain.Controls.Add(this.panelExt4);
            this.m_pnlMain.Controls.Add(this.panelExt2);
            this.m_pnlMain.Controls.Add(this.panelExt3);
            this.m_pnlMain.Controls.Add(this.panel18);
            this.m_pnlMain.Controls.Add(this.panel3);
            this.m_pnlMain.Controls.Add(this.panel2);
            this.m_pnlMain.Controls.Add(this.panel1);
            this.m_pnlMain.Controls.Add(this.bpc담당자);
            this.m_pnlMain.Controls.Add(this.bpc사업장);
            this.m_pnlMain.Controls.Add(this.bpc거래처);
            this.m_pnlMain.Controls.Add(this.cur발행원화금액);
            this.m_pnlMain.Controls.Add(this.dtp발행일자);
            this.m_pnlMain.Controls.Add(this.dtp유효일자);
            this.m_pnlMain.Controls.Add(this.dtp인도일자);
            this.m_pnlMain.Controls.Add(this.dtp물품인수일자);
            this.m_pnlMain.Controls.Add(this.txt비고);
            this.m_pnlMain.Controls.Add(this.txt인수증발급번호);
            this.m_pnlMain.Controls.Add(this.cur물품인수금액);
            this.m_pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlMain.Location = new System.Drawing.Point(3, 3);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(787, 137);
            this.m_pnlMain.TabIndex = 142;
            // 
            // panelExt2
            // 
            this.panelExt2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt2.BackColor = System.Drawing.Color.Transparent;
            this.panelExt2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt2.BackgroundImage")));
            this.panelExt2.Location = new System.Drawing.Point(6, 78);
            this.panelExt2.Name = "panelExt2";
            this.panelExt2.Size = new System.Drawing.Size(775, 1);
            this.panelExt2.TabIndex = 175;
            // 
            // panelExt3
            // 
            this.panelExt3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt3.BackColor = System.Drawing.Color.Transparent;
            this.panelExt3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt3.BackgroundImage")));
            this.panelExt3.Location = new System.Drawing.Point(6, 52);
            this.panelExt3.Name = "panelExt3";
            this.panelExt3.Size = new System.Drawing.Size(775, 1);
            this.panelExt3.TabIndex = 174;
            // 
            // panel18
            // 
            this.panel18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel18.BackColor = System.Drawing.Color.Transparent;
            this.panel18.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel18.BackgroundImage")));
            this.panel18.Location = new System.Drawing.Point(6, 28);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(775, 1);
            this.panel18.TabIndex = 173;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel3.Controls.Add(this.lbl사업장);
            this.panel3.Controls.Add(this.m_lblAmtIssueDt);
            this.panel3.Controls.Add(this.m_lblTransferDt);
            this.panel3.Location = new System.Drawing.Point(526, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(103, 73);
            this.panel3.TabIndex = 153;
            // 
            // lbl사업장
            // 
            this.lbl사업장.Location = new System.Drawing.Point(5, 4);
            this.lbl사업장.Name = "lbl사업장";
            this.lbl사업장.Resizeble = true;
            this.lbl사업장.Size = new System.Drawing.Size(95, 18);
            this.lbl사업장.TabIndex = 10;
            this.lbl사업장.Tag = "NO_LLC";
            this.lbl사업장.Text = "사업장";
            this.lbl사업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblAmtIssueDt
            // 
            this.m_lblAmtIssueDt.Location = new System.Drawing.Point(4, 54);
            this.m_lblAmtIssueDt.Name = "m_lblAmtIssueDt";
            this.m_lblAmtIssueDt.Resizeble = true;
            this.m_lblAmtIssueDt.Size = new System.Drawing.Size(95, 18);
            this.m_lblAmtIssueDt.TabIndex = 8;
            this.m_lblAmtIssueDt.Tag = "AMT_ISSUE_KOR";
            this.m_lblAmtIssueDt.Text = "발행원화금액";
            this.m_lblAmtIssueDt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblTransferDt
            // 
            this.m_lblTransferDt.Location = new System.Drawing.Point(5, 28);
            this.m_lblTransferDt.Name = "m_lblTransferDt";
            this.m_lblTransferDt.Resizeble = true;
            this.m_lblTransferDt.Size = new System.Drawing.Size(95, 18);
            this.m_lblTransferDt.TabIndex = 7;
            this.m_lblTransferDt.Tag = "TRANSFER_DT";
            this.m_lblTransferDt.Text = "인도일자";
            this.m_lblTransferDt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel2.Controls.Add(this.labelExt2);
            this.panel2.Controls.Add(this.lbl거래처);
            this.panel2.Controls.Add(this.m_lblAmtSeal);
            this.panel2.Controls.Add(this.m_lblDtIssue);
            this.panel2.Controls.Add(this.m_lblRemark);
            this.panel2.Location = new System.Drawing.Point(274, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(91, 128);
            this.panel2.TabIndex = 152;
            // 
            // lbl거래처
            // 
            this.lbl거래처.Location = new System.Drawing.Point(6, 4);
            this.lbl거래처.Name = "lbl거래처";
            this.lbl거래처.Resizeble = true;
            this.lbl거래처.Size = new System.Drawing.Size(80, 18);
            this.lbl거래처.TabIndex = 5;
            this.lbl거래처.Tag = "CD_TRANS";
            this.lbl거래처.Text = "거래처";
            this.lbl거래처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblDtSeal
            // 
            this.m_lblDtSeal.Location = new System.Drawing.Point(21, 30);
            this.m_lblDtSeal.Name = "m_lblDtSeal";
            this.m_lblDtSeal.Resizeble = true;
            this.m_lblDtSeal.Size = new System.Drawing.Size(79, 18);
            this.m_lblDtSeal.TabIndex = 11;
            this.m_lblDtSeal.Tag = "DT_SEAL";
            this.m_lblDtSeal.Text = "물품인수일자";
            this.m_lblDtSeal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblDtIssue
            // 
            this.m_lblDtIssue.Location = new System.Drawing.Point(6, 28);
            this.m_lblDtIssue.Name = "m_lblDtIssue";
            this.m_lblDtIssue.Resizeble = true;
            this.m_lblDtIssue.Size = new System.Drawing.Size(80, 18);
            this.m_lblDtIssue.TabIndex = 3;
            this.m_lblDtIssue.Tag = "DT_ISSUE";
            this.m_lblDtIssue.Text = "발행일자";
            this.m_lblDtIssue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblRemark
            // 
            this.m_lblRemark.Location = new System.Drawing.Point(6, 79);
            this.m_lblRemark.Name = "m_lblRemark";
            this.m_lblRemark.Resizeble = true;
            this.m_lblRemark.Size = new System.Drawing.Size(80, 18);
            this.m_lblRemark.TabIndex = 4;
            this.m_lblRemark.Tag = "REMARK";
            this.m_lblRemark.Text = "비고";
            this.m_lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel1.Controls.Add(this.labelExt1);
            this.panel1.Controls.Add(this.m_lblNoSeal);
            this.panel1.Controls.Add(this.m_lblDtSeal);
            this.panel1.Controls.Add(this.m_lblNmEmp);
            this.panel1.Controls.Add(this.m_lblExpiryDate);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(103, 130);
            this.panel1.TabIndex = 151;
            // 
            // m_lblNoSeal
            // 
            this.m_lblNoSeal.Location = new System.Drawing.Point(4, 5);
            this.m_lblNoSeal.Name = "m_lblNoSeal";
            this.m_lblNoSeal.Resizeble = true;
            this.m_lblNoSeal.Size = new System.Drawing.Size(96, 18);
            this.m_lblNoSeal.TabIndex = 0;
            this.m_lblNoSeal.Tag = "NO_SEAL";
            this.m_lblNoSeal.Text = "인수증발급번호";
            this.m_lblNoSeal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblNmEmp
            // 
            this.m_lblNmEmp.Location = new System.Drawing.Point(3, 80);
            this.m_lblNmEmp.Name = "m_lblNmEmp";
            this.m_lblNmEmp.Resizeble = true;
            this.m_lblNmEmp.Size = new System.Drawing.Size(95, 18);
            this.m_lblNmEmp.TabIndex = 13;
            this.m_lblNmEmp.Tag = "NM_EMP";
            this.m_lblNmEmp.Text = "담당자";
            this.m_lblNmEmp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblAmtSeal
            // 
            this.m_lblAmtSeal.Location = new System.Drawing.Point(6, 51);
            this.m_lblAmtSeal.Name = "m_lblAmtSeal";
            this.m_lblAmtSeal.Resizeble = true;
            this.m_lblAmtSeal.Size = new System.Drawing.Size(80, 18);
            this.m_lblAmtSeal.TabIndex = 6;
            this.m_lblAmtSeal.Tag = "AMT_SEAL";
            this.m_lblAmtSeal.Text = "물품인수금액";
            this.m_lblAmtSeal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblExpiryDate
            // 
            this.m_lblExpiryDate.Location = new System.Drawing.Point(3, 55);
            this.m_lblExpiryDate.Name = "m_lblExpiryDate";
            this.m_lblExpiryDate.Resizeble = true;
            this.m_lblExpiryDate.Size = new System.Drawing.Size(96, 18);
            this.m_lblExpiryDate.TabIndex = 12;
            this.m_lblExpiryDate.Tag = "EXPIRY_DATE";
            this.m_lblExpiryDate.Text = "유효일자";
            this.m_lblExpiryDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpc담당자
            // 
            this.bpc담당자.BackColor = System.Drawing.Color.White;
            this.bpc담당자.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc담당자.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc담당자.ButtonImage")));
            this.bpc담당자.ChildMode = "";
            this.bpc담당자.CodeName = "";
            this.bpc담당자.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc담당자.CodeValue = "";
            this.bpc담당자.ComboCheck = true;
            this.bpc담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.bpc담당자.IsCodeValueToUpper = true;
            this.bpc담당자.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bpc담당자.Location = new System.Drawing.Point(110, 83);
            this.bpc담당자.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc담당자.Name = "bpc담당자";
            this.bpc담당자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc담당자.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc담당자.SearchCode = true;
            this.bpc담당자.SelectCount = 0;
            this.bpc담당자.SetDefaultValue = false;
            this.bpc담당자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc담당자.Size = new System.Drawing.Size(151, 21);
            this.bpc담당자.TabIndex = 150;
            this.bpc담당자.TabStop = false;
            this.bpc담당자.Tag = "NO_EMP;NM_KOR";
            this.bpc담당자.Text = "bpCodeTextBox1";
            // 
            // bpc사업장
            // 
            this.bpc사업장.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc사업장.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc사업장.ButtonImage")));
            this.bpc사업장.ChildMode = "";
            this.bpc사업장.CodeName = "";
            this.bpc사업장.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc사업장.CodeValue = "";
            this.bpc사업장.ComboCheck = true;
            this.bpc사업장.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BIZAREA_SUB;
            this.bpc사업장.IsCodeValueToUpper = true;
            this.bpc사업장.ItemBackColor = System.Drawing.Color.White;
            this.bpc사업장.Location = new System.Drawing.Point(631, 5);
            this.bpc사업장.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc사업장.Name = "bpc사업장";
            this.bpc사업장.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc사업장.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc사업장.SearchCode = true;
            this.bpc사업장.SelectCount = 0;
            this.bpc사업장.SetDefaultValue = true;
            this.bpc사업장.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc사업장.Size = new System.Drawing.Size(153, 21);
            this.bpc사업장.TabIndex = 149;
            this.bpc사업장.TabStop = false;
            this.bpc사업장.Tag = "CD_BIZAREA;NM_BIZAREA";
            this.bpc사업장.Text = "bpCodeTextBox1";
            // 
            // bpc거래처
            // 
            this.bpc거래처.BackColor = System.Drawing.Color.White;
            this.bpc거래처.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc거래처.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc거래처.ButtonImage")));
            this.bpc거래처.ChildMode = "";
            this.bpc거래처.CodeName = "";
            this.bpc거래처.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc거래처.CodeValue = "";
            this.bpc거래처.ComboCheck = true;
            this.bpc거래처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc거래처.IsCodeValueToUpper = true;
            this.bpc거래처.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.bpc거래처.Location = new System.Drawing.Point(370, 5);
            this.bpc거래처.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc거래처.Name = "bpc거래처";
            this.bpc거래처.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc거래처.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc거래처.SearchCode = true;
            this.bpc거래처.SelectCount = 0;
            this.bpc거래처.SetDefaultValue = false;
            this.bpc거래처.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc거래처.Size = new System.Drawing.Size(150, 21);
            this.bpc거래처.TabIndex = 148;
            this.bpc거래처.TabStop = false;
            this.bpc거래처.Tag = "CD_PARTNER;LN_PARTNER";
            this.bpc거래처.Text = "bpCodeTextBox1";
            // 
            // cur발행원화금액
            // 
            this.cur발행원화금액.BackColor = System.Drawing.SystemColors.Control;
            this.cur발행원화금액.CurrencyDecimalDigits = 4;
            this.cur발행원화금액.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur발행원화금액.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur발행원화금액.Location = new System.Drawing.Point(631, 56);
            this.cur발행원화금액.Mask = null;
            this.cur발행원화금액.MaxLength = 22;
            this.cur발행원화금액.Name = "cur발행원화금액";
            this.cur발행원화금액.NullString = "0";
            this.cur발행원화금액.PositiveColor = System.Drawing.Color.Black;
            this.cur발행원화금액.ReadOnly = true;
            this.cur발행원화금액.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur발행원화금액.Size = new System.Drawing.Size(151, 21);
            this.cur발행원화금액.TabIndex = 147;
            this.cur발행원화금액.TabStop = false;
            this.cur발행원화금액.Tag = "AM";
            this.cur발행원화금액.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur발행원화금액.UseKeyEnter = true;
            this.cur발행원화금액.UseKeyF3 = true;
            // 
            // dtp발행일자
            // 
            this.dtp발행일자.BackColor = System.Drawing.Color.White;
            this.dtp발행일자.CalendarBackColor = System.Drawing.Color.White;
            this.dtp발행일자.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp발행일자.FriDayColor = System.Drawing.Color.Blue;
            this.dtp발행일자.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp발행일자.Location = new System.Drawing.Point(370, 30);
            this.dtp발행일자.Mask = "####/##/##";
            this.dtp발행일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp발행일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp발행일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp발행일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp발행일자.Modified = false;
            this.dtp발행일자.Name = "dtp발행일자";
            this.dtp발행일자.PaddingCharacter = '_';
            this.dtp발행일자.PassivePromptCharacter = '_';
            this.dtp발행일자.PromptCharacter = '_';
            this.dtp발행일자.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp발행일자.ShowToDay = true;
            this.dtp발행일자.ShowTodayCircle = true;
            this.dtp발행일자.ShowUpDown = false;
            this.dtp발행일자.Size = new System.Drawing.Size(110, 21);
            this.dtp발행일자.SunDayColor = System.Drawing.Color.Red;
            this.dtp발행일자.TabIndex = 146;
            this.dtp발행일자.Tag = "DT_BALLOT";
            this.dtp발행일자.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp발행일자.TitleForeColor = System.Drawing.Color.Black;
            this.dtp발행일자.ToDayColor = System.Drawing.Color.Red;
            this.dtp발행일자.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp발행일자.UseKeyF3 = false;
            this.dtp발행일자.Value = new System.DateTime(((long)(0)));
            // 
            // dtp유효일자
            // 
            this.dtp유효일자.BackColor = System.Drawing.Color.White;
            this.dtp유효일자.CalendarBackColor = System.Drawing.Color.White;
            this.dtp유효일자.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp유효일자.FriDayColor = System.Drawing.Color.Blue;
            this.dtp유효일자.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp유효일자.Location = new System.Drawing.Point(110, 55);
            this.dtp유효일자.Mask = "####/##/##";
            this.dtp유효일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp유효일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp유효일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp유효일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp유효일자.Modified = false;
            this.dtp유효일자.Name = "dtp유효일자";
            this.dtp유효일자.PaddingCharacter = '_';
            this.dtp유효일자.PassivePromptCharacter = '_';
            this.dtp유효일자.PromptCharacter = '_';
            this.dtp유효일자.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp유효일자.ShowToDay = true;
            this.dtp유효일자.ShowTodayCircle = true;
            this.dtp유효일자.ShowUpDown = false;
            this.dtp유효일자.Size = new System.Drawing.Size(110, 21);
            this.dtp유효일자.SunDayColor = System.Drawing.Color.Red;
            this.dtp유효일자.TabIndex = 145;
            this.dtp유효일자.Tag = "DT_VALIDITY";
            this.dtp유효일자.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp유효일자.TitleForeColor = System.Drawing.Color.Black;
            this.dtp유효일자.ToDayColor = System.Drawing.Color.Red;
            this.dtp유효일자.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp유효일자.UseKeyF3 = false;
            this.dtp유효일자.Value = new System.DateTime(((long)(0)));
            // 
            // dtp인도일자
            // 
            this.dtp인도일자.BackColor = System.Drawing.Color.White;
            this.dtp인도일자.CalendarBackColor = System.Drawing.Color.White;
            this.dtp인도일자.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp인도일자.FriDayColor = System.Drawing.Color.Blue;
            this.dtp인도일자.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp인도일자.Location = new System.Drawing.Point(631, 30);
            this.dtp인도일자.Mask = "####/##/##";
            this.dtp인도일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp인도일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp인도일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp인도일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp인도일자.Modified = false;
            this.dtp인도일자.Name = "dtp인도일자";
            this.dtp인도일자.PaddingCharacter = '_';
            this.dtp인도일자.PassivePromptCharacter = '_';
            this.dtp인도일자.PromptCharacter = '_';
            this.dtp인도일자.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp인도일자.ShowToDay = true;
            this.dtp인도일자.ShowTodayCircle = true;
            this.dtp인도일자.ShowUpDown = false;
            this.dtp인도일자.Size = new System.Drawing.Size(107, 21);
            this.dtp인도일자.SunDayColor = System.Drawing.Color.Red;
            this.dtp인도일자.TabIndex = 144;
            this.dtp인도일자.Tag = "DT_TRANS";
            this.dtp인도일자.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp인도일자.TitleForeColor = System.Drawing.Color.Black;
            this.dtp인도일자.ToDayColor = System.Drawing.Color.Red;
            this.dtp인도일자.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp인도일자.UseKeyF3 = false;
            this.dtp인도일자.Value = new System.DateTime(((long)(0)));
            // 
            // dtp물품인수일자
            // 
            this.dtp물품인수일자.BackColor = System.Drawing.Color.White;
            this.dtp물품인수일자.CalendarBackColor = System.Drawing.Color.White;
            this.dtp물품인수일자.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp물품인수일자.FriDayColor = System.Drawing.Color.Blue;
            this.dtp물품인수일자.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp물품인수일자.Location = new System.Drawing.Point(110, 30);
            this.dtp물품인수일자.Mask = "####/##/##";
            this.dtp물품인수일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp물품인수일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp물품인수일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp물품인수일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp물품인수일자.Modified = false;
            this.dtp물품인수일자.Name = "dtp물품인수일자";
            this.dtp물품인수일자.PaddingCharacter = '_';
            this.dtp물품인수일자.PassivePromptCharacter = '_';
            this.dtp물품인수일자.PromptCharacter = '_';
            this.dtp물품인수일자.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp물품인수일자.ShowToDay = true;
            this.dtp물품인수일자.ShowTodayCircle = true;
            this.dtp물품인수일자.ShowUpDown = false;
            this.dtp물품인수일자.Size = new System.Drawing.Size(110, 21);
            this.dtp물품인수일자.SunDayColor = System.Drawing.Color.Red;
            this.dtp물품인수일자.TabIndex = 143;
            this.dtp물품인수일자.Tag = "DT_SEAL";
            this.dtp물품인수일자.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp물품인수일자.TitleForeColor = System.Drawing.Color.Black;
            this.dtp물품인수일자.ToDayColor = System.Drawing.Color.Red;
            this.dtp물품인수일자.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp물품인수일자.UseKeyF3 = false;
            this.dtp물품인수일자.Value = new System.DateTime(((long)(0)));
            // 
            // txt비고
            // 
            this.txt비고.Location = new System.Drawing.Point(370, 83);
            this.txt비고.MaxLength = 100;
            this.txt비고.Name = "txt비고";
            this.txt비고.SelectedAllEnabled = false;
            this.txt비고.Size = new System.Drawing.Size(411, 21);
            this.txt비고.TabIndex = 142;
            this.txt비고.Tag = "REMARK";
            this.txt비고.UseKeyEnter = true;
            this.txt비고.UseKeyF3 = true;
            // 
            // txt인수증발급번호
            // 
            this.txt인수증발급번호.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.txt인수증발급번호.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt인수증발급번호.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.txt인수증발급번호.Location = new System.Drawing.Point(110, 5);
            this.txt인수증발급번호.MaxLength = 20;
            this.txt인수증발급번호.Name = "txt인수증발급번호";
            this.txt인수증발급번호.SelectedAllEnabled = false;
            this.txt인수증발급번호.Size = new System.Drawing.Size(151, 21);
            this.txt인수증발급번호.TabIndex = 135;
            this.txt인수증발급번호.Tag = "NO_SEAL";
            this.txt인수증발급번호.UseKeyEnter = true;
            this.txt인수증발급번호.UseKeyF3 = true;
            // 
            // cur물품인수금액
            // 
            this.cur물품인수금액.BackColor = System.Drawing.SystemColors.Control;
            this.cur물품인수금액.CurrencyDecimalDigits = 4;
            this.cur물품인수금액.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cur물품인수금액.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cur물품인수금액.Location = new System.Drawing.Point(370, 55);
            this.cur물품인수금액.Mask = null;
            this.cur물품인수금액.MaxLength = 22;
            this.cur물품인수금액.Name = "cur물품인수금액";
            this.cur물품인수금액.NullString = "0";
            this.cur물품인수금액.PositiveColor = System.Drawing.Color.Black;
            this.cur물품인수금액.ReadOnly = true;
            this.cur물품인수금액.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cur물품인수금액.Size = new System.Drawing.Size(151, 21);
            this.cur물품인수금액.TabIndex = 139;
            this.cur물품인수금액.TabStop = false;
            this.cur물품인수금액.Tag = "AM_SEAL";
            this.cur물품인수금액.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cur물품인수금액.UseKeyEnter = true;
            this.cur물품인수금액.UseKeyF3 = true;
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(3, 177);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(787, 397);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 143;
            // 
            // btn계산서적용
            // 
            this.btn계산서적용.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn계산서적용.BackColor = System.Drawing.Color.White;
            this.btn계산서적용.BackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.GrayRounded;
            this.btn계산서적용.ClickedBackgroundStyle = Duzon.Common.Controls.RoundedButton.BackgroundStyles.YellowRounded;
            this.btn계산서적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn계산서적용.Location = new System.Drawing.Point(705, 9);
            this.btn계산서적용.MaximumSize = new System.Drawing.Size(0, 22);
            this.btn계산서적용.Name = "btn계산서적용";
            this.btn계산서적용.Size = new System.Drawing.Size(91, 22);
            this.btn계산서적용.TabIndex = 3;
            this.btn계산서적용.TabStop = false;
            this.btn계산서적용.Text = "계산서 적용";
            this.btn계산서적용.UseVisualStyleBackColor = true;
            this.btn계산서적용.Click += new System.EventHandler(this.btn계산서적용_Click);
            // 
            // panelExt4
            // 
            this.panelExt4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt4.BackColor = System.Drawing.Color.Transparent;
            this.panelExt4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt4.BackgroundImage")));
            this.panelExt4.Location = new System.Drawing.Point(5, 108);
            this.panelExt4.Name = "panelExt4";
            this.panelExt4.Size = new System.Drawing.Size(775, 1);
            this.panelExt4.TabIndex = 176;
            // 
            // m_curCdExch
            // 
            this.m_curCdExch.BackColor = System.Drawing.Color.White;
            this.m_curCdExch.CurrencyDecimalDigits = 4;
            this.m_curCdExch.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_curCdExch.Enabled = false;
            this.m_curCdExch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_curCdExch.Location = new System.Drawing.Point(196, 112);
            this.m_curCdExch.Mask = "#,###.0000";
            this.m_curCdExch.MaxLength = 17;
            this.m_curCdExch.Name = "m_curCdExch";
            this.m_curCdExch.NullString = "0";
            this.m_curCdExch.PositiveColor = System.Drawing.Color.Black;
            this.m_curCdExch.ReadOnly = true;
            this.m_curCdExch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_curCdExch.Size = new System.Drawing.Size(74, 21);
            this.m_curCdExch.TabIndex = 178;
            this.m_curCdExch.TabStop = false;
            this.m_curCdExch.Tag = "RT_EXCH";
            this.m_curCdExch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_curCdExch.UseKeyEnter = false;
            this.m_curCdExch.UseKeyF3 = false;
            // 
            // m_cboCdExch
            // 
            this.m_cboCdExch.AutoDropDown = true;
            this.m_cboCdExch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_cboCdExch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCdExch.Enabled = false;
            this.m_cboCdExch.Location = new System.Drawing.Point(110, 112);
            this.m_cboCdExch.Name = "m_cboCdExch";
            this.m_cboCdExch.ShowCheckBox = false;
            this.m_cboCdExch.Size = new System.Drawing.Size(86, 20);
            this.m_cboCdExch.TabIndex = 177;
            this.m_cboCdExch.Tag = "CD_EXCH";
            this.m_cboCdExch.UseKeyEnter = false;
            this.m_cboCdExch.UseKeyF3 = false;
            // 
            // labelExt1
            // 
            this.labelExt1.Location = new System.Drawing.Point(2, 109);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(95, 18);
            this.labelExt1.TabIndex = 14;
            this.labelExt1.Tag = "NM_EMP";
            this.labelExt1.Text = "마감환종";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt마감번호
            // 
            this.txt마감번호.BackColor = System.Drawing.SystemColors.Control;
            this.txt마감번호.Location = new System.Drawing.Point(370, 112);
            this.txt마감번호.Name = "txt마감번호";
            this.txt마감번호.ReadOnly = true;
            this.txt마감번호.SelectedAllEnabled = false;
            this.txt마감번호.Size = new System.Drawing.Size(152, 21);
            this.txt마감번호.TabIndex = 179;
            this.txt마감번호.TabStop = false;
            this.txt마감번호.Tag = "NO_IV";
            this.txt마감번호.UseKeyEnter = true;
            this.txt마감번호.UseKeyF3 = true;
            // 
            // labelExt2
            // 
            this.labelExt2.Location = new System.Drawing.Point(6, 107);
            this.labelExt2.Name = "labelExt2";
            this.labelExt2.Resizeble = true;
            this.labelExt2.Size = new System.Drawing.Size(80, 18);
            this.labelExt2.TabIndex = 12;
            this.labelExt2.Tag = "REMARK";
            this.labelExt2.Text = "마감번호";
            this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // curAmBan_ex
            // 
            this.curAmBan_ex.BackColor = System.Drawing.SystemColors.Control;
            this.curAmBan_ex.CurrencyDecimalDigits = 2;
            this.curAmBan_ex.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.curAmBan_ex.ForeColor = System.Drawing.SystemColors.ControlText;
            this.curAmBan_ex.Location = new System.Drawing.Point(631, 111);
            this.curAmBan_ex.Mask = "#,###.00";
            this.curAmBan_ex.Name = "curAmBan_ex";
            this.curAmBan_ex.NullString = "0";
            this.curAmBan_ex.PositiveColor = System.Drawing.Color.Black;
            this.curAmBan_ex.ReadOnly = true;
            this.curAmBan_ex.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.curAmBan_ex.Size = new System.Drawing.Size(151, 21);
            this.curAmBan_ex.TabIndex = 180;
            this.curAmBan_ex.TabStop = false;
            this.curAmBan_ex.Tag = "AM_BAN_EX";
            this.curAmBan_ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.curAmBan_ex.UseKeyEnter = true;
            this.curAmBan_ex.UseKeyF3 = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel4.Controls.Add(this.labelExt3);
            this.panel4.Location = new System.Drawing.Point(527, 110);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(103, 24);
            this.panel4.TabIndex = 154;
            // 
            // labelExt3
            // 
            this.labelExt3.Location = new System.Drawing.Point(5, 4);
            this.labelExt3.Name = "labelExt3";
            this.labelExt3.Resizeble = true;
            this.labelExt3.Size = new System.Drawing.Size(95, 18);
            this.labelExt3.TabIndex = 10;
            this.labelExt3.Tag = "NO_LLC";
            this.labelExt3.Text = "NEGO금액";
            this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // P_TR_EXSEAL
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btn계산서적용);
            this.Name = "P_TR_EXSEAL";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.btn계산서적용, 0);
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelExt1.ResumeLayout(false);
            this.m_pnlMain.ResumeLayout(false);
            this.m_pnlMain.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cur발행원화금액)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp발행일자)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp유효일자)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp인도일자)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp물품인수일자)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cur물품인수금액)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_curCdExch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.curAmBan_ex)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.LabelExt m_lblClText;
        private Duzon.Common.Controls.RoundedButton btn계산서적용;
        private System.Windows.Forms.Panel m_pnlMain;
        private Duzon.Common.BpControls.BpCodeTextBox bpc담당자;
        private Duzon.Common.BpControls.BpCodeTextBox bpc사업장;
        private Duzon.Common.BpControls.BpCodeTextBox bpc거래처;
        private Duzon.Common.Controls.CurrencyTextBox cur발행원화금액;
        private Duzon.Common.Controls.DatePicker dtp발행일자;
        private Duzon.Common.Controls.DatePicker dtp유효일자;
        private Duzon.Common.Controls.DatePicker dtp인도일자;
        private Duzon.Common.Controls.DatePicker dtp물품인수일자;
        private Duzon.Common.Controls.TextBoxExt txt비고;
        private Duzon.Common.Controls.TextBoxExt txt인수증발급번호;
        private Duzon.Common.Controls.LabelExt m_lblAmtIssueDt;
        private Duzon.Common.Controls.LabelExt lbl사업장;
        private Duzon.Common.Controls.LabelExt m_lblTransferDt;
        private Duzon.Common.Controls.LabelExt lbl거래처;
        private Duzon.Common.Controls.LabelExt m_lblRemark;
        private Duzon.Common.Controls.LabelExt m_lblDtIssue;
        private Duzon.Common.Controls.LabelExt m_lblDtSeal;
        private Duzon.Common.Controls.LabelExt m_lblNmEmp;
        private Duzon.Common.Controls.LabelExt m_lblExpiryDate;
        private Duzon.Common.Controls.LabelExt m_lblNoSeal;
        private Duzon.Common.Controls.LabelExt m_lblAmtSeal;
        private Duzon.Common.Controls.CurrencyTextBox cur물품인수금액;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.Controls.PanelExt panel18;
        private Duzon.Common.Controls.PanelExt panelExt2;
        private Duzon.Common.Controls.PanelExt panelExt3;
        private Duzon.Common.Controls.PanelExt panelExt4;
        private Duzon.Common.Controls.CurrencyTextBox curAmBan_ex;
        private Duzon.Common.Controls.TextBoxExt txt마감번호;
        private Duzon.Common.Controls.CurrencyTextBox m_curCdExch;
        private Duzon.Common.Controls.DropDownComboBox m_cboCdExch;
        private System.Windows.Forms.Panel panel4;
        private Duzon.Common.Controls.LabelExt labelExt3;
        private Duzon.Common.Controls.LabelExt labelExt2;
        private Duzon.Common.Controls.LabelExt labelExt1;
    }
}

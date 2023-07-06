namespace pur
{
    partial class P_PU_INV_SCH2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_PU_INV_SCH2));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.bp_CLS_S = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bp_CLS_M = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bp_CLS_L = new Duzon.Common.BpControls.BpCodeTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboFG_ABC = new Duzon.Common.Controls.DropDownComboBox();
            this.chk_품목사용체크된건만 = new Duzon.Common.Controls.CheckBoxExt();
            this.bp_ITEM_GROUP = new Duzon.Common.BpControls.BpCodeTextBox();
            this.cboFG_ACCT = new Duzon.Common.Controls.DropDownComboBox();
            this.cbo_CD_PLANT = new Duzon.Common.Controls.DropDownComboBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.m_lblDy_Io = new System.Windows.Forms.Label();
            this.m_lbl_ITEM_GROUP = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.m_lblFg_Acct = new System.Windows.Forms.Label();
            this.m_lblCd_Plant = new System.Windows.Forms.Label();
            this.dp기준일자 = new Duzon.Common.Controls.DatePicker();
            this.bp_CD_ITEM_END = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bp_CD_ITEM_START = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_pnlGrid1 = new System.Windows.Forms.Panel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.bpc창고 = new Duzon.Common.BpControls.BpComboBox();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dp기준일자)).BeginInit();
            this.m_pnlGrid1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(893, 577);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_pnlGrid1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(893, 577);
            this.tableLayoutPanel1.TabIndex = 102;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.bpc창고);
            this.panel4.Controls.Add(this.bp_CLS_S);
            this.panel4.Controls.Add(this.bp_CLS_M);
            this.panel4.Controls.Add(this.bp_CLS_L);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.cboFG_ABC);
            this.panel4.Controls.Add(this.chk_품목사용체크된건만);
            this.panel4.Controls.Add(this.bp_ITEM_GROUP);
            this.panel4.Controls.Add(this.cboFG_ACCT);
            this.panel4.Controls.Add(this.cbo_CD_PLANT);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel14);
            this.panel4.Controls.Add(this.panel15);
            this.panel4.Controls.Add(this.dp기준일자);
            this.panel4.Controls.Add(this.bp_CD_ITEM_END);
            this.panel4.Controls.Add(this.bp_CD_ITEM_START);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(887, 78);
            this.panel4.TabIndex = 101;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(632, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 18);
            this.label2.TabIndex = 215;
            this.label2.Text = "~";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bp_CLS_S
            // 
            this.bp_CLS_S.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp_CLS_S.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_CLS_S.ButtonImage")));
            this.bp_CLS_S.ChildMode = "";
            this.bp_CLS_S.CodeName = "";
            this.bp_CLS_S.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp_CLS_S.CodeValue = "";
            this.bp_CLS_S.ComboCheck = true;
            this.bp_CLS_S.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
            this.bp_CLS_S.IsCodeValueToUpper = true;
            this.bp_CLS_S.ItemBackColor = System.Drawing.Color.White;
            this.bp_CLS_S.Location = new System.Drawing.Point(493, 53);
            this.bp_CLS_S.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp_CLS_S.Name = "bp_CLS_S";
            this.bp_CLS_S.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp_CLS_S.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bp_CLS_S.SearchCode = true;
            this.bp_CLS_S.SelectCount = 0;
            this.bp_CLS_S.SetDefaultValue = false;
            this.bp_CLS_S.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp_CLS_S.Size = new System.Drawing.Size(137, 21);
            this.bp_CLS_S.TabIndex = 214;
            this.bp_CLS_S.TabStop = false;
            this.bp_CLS_S.Text = "bpCodeTextBox2";
            this.bp_CLS_S.UseAccessGrant = true;
            this.bp_CLS_S.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
            // 
            // bp_CLS_M
            // 
            this.bp_CLS_M.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp_CLS_M.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_CLS_M.ButtonImage")));
            this.bp_CLS_M.ChildMode = "";
            this.bp_CLS_M.CodeName = "";
            this.bp_CLS_M.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp_CLS_M.CodeValue = "";
            this.bp_CLS_M.ComboCheck = true;
            this.bp_CLS_M.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
            this.bp_CLS_M.IsCodeValueToUpper = true;
            this.bp_CLS_M.ItemBackColor = System.Drawing.Color.White;
            this.bp_CLS_M.Location = new System.Drawing.Point(268, 53);
            this.bp_CLS_M.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp_CLS_M.Name = "bp_CLS_M";
            this.bp_CLS_M.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp_CLS_M.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bp_CLS_M.SearchCode = true;
            this.bp_CLS_M.SelectCount = 0;
            this.bp_CLS_M.SetDefaultValue = false;
            this.bp_CLS_M.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp_CLS_M.Size = new System.Drawing.Size(137, 21);
            this.bp_CLS_M.TabIndex = 212;
            this.bp_CLS_M.TabStop = false;
            this.bp_CLS_M.Text = "bpCodeTextBox2";
            this.bp_CLS_M.UseAccessGrant = true;
            this.bp_CLS_M.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
            // 
            // bp_CLS_L
            // 
            this.bp_CLS_L.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp_CLS_L.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_CLS_L.ButtonImage")));
            this.bp_CLS_L.ChildMode = "";
            this.bp_CLS_L.CodeName = "";
            this.bp_CLS_L.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp_CLS_L.CodeValue = "";
            this.bp_CLS_L.ComboCheck = true;
            this.bp_CLS_L.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB;
            this.bp_CLS_L.IsCodeValueToUpper = true;
            this.bp_CLS_L.ItemBackColor = System.Drawing.Color.White;
            this.bp_CLS_L.Location = new System.Drawing.Point(65, 53);
            this.bp_CLS_L.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp_CLS_L.Name = "bp_CLS_L";
            this.bp_CLS_L.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp_CLS_L.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bp_CLS_L.SearchCode = true;
            this.bp_CLS_L.SelectCount = 0;
            this.bp_CLS_L.SetDefaultValue = false;
            this.bp_CLS_L.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp_CLS_L.Size = new System.Drawing.Size(137, 21);
            this.bp_CLS_L.TabIndex = 209;
            this.bp_CLS_L.TabStop = false;
            this.bp_CLS_L.Text = "bpCodeTextBox2";
            this.bp_CLS_L.UseAccessGrant = true;
            this.bp_CLS_L.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Location = new System.Drawing.Point(4, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(877, 1);
            this.panel2.TabIndex = 106;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.Location = new System.Drawing.Point(5, 24);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(877, 1);
            this.panel8.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(407, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(84, 76);
            this.panel1.TabIndex = 77;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(27, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 16);
            this.label8.TabIndex = 76;
            this.label8.Text = "소분류";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(28, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 74;
            this.label1.Text = "품목";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 16);
            this.label5.TabIndex = 75;
            this.label5.Text = "품목사용유무";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboFG_ABC
            // 
            this.cboFG_ABC.AutoDropDown = true;
            this.cboFG_ABC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFG_ABC.Location = new System.Drawing.Point(702, 2);
            this.cboFG_ABC.Name = "cboFG_ABC";
            this.cboFG_ABC.ShowCheckBox = false;
            this.cboFG_ABC.Size = new System.Drawing.Size(80, 20);
            this.cboFG_ABC.TabIndex = 110;
            this.cboFG_ABC.UseKeyEnter = true;
            this.cboFG_ABC.UseKeyF3 = true;
            // 
            // chk_품목사용체크된건만
            // 
            this.chk_품목사용체크된건만.BackColor = System.Drawing.Color.Transparent;
            this.chk_품목사용체크된건만.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_품목사용체크된건만.Checked = true;
            this.chk_품목사용체크된건만.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_품목사용체크된건만.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chk_품목사용체크된건만.Location = new System.Drawing.Point(498, 4);
            this.chk_품목사용체크된건만.Name = "chk_품목사용체크된건만";
            this.chk_품목사용체크된건만.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_품목사용체크된건만.Size = new System.Drawing.Size(132, 18);
            this.chk_품목사용체크된건만.TabIndex = 109;
            this.chk_품목사용체크된건만.Tag = "";
            this.chk_품목사용체크된건만.Text = "품목사용체크된건만";
            this.chk_품목사용체크된건만.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_품목사용체크된건만.TextDD = null;
            this.chk_품목사용체크된건만.UseKeyEnter = true;
            this.chk_품목사용체크된건만.UseVisualStyleBackColor = false;
            // 
            // bp_ITEM_GROUP
            // 
            this.bp_ITEM_GROUP.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp_ITEM_GROUP.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_ITEM_GROUP.ButtonImage")));
            this.bp_ITEM_GROUP.ChildMode = "";
            this.bp_ITEM_GROUP.CodeName = "";
            this.bp_ITEM_GROUP.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp_ITEM_GROUP.CodeValue = "";
            this.bp_ITEM_GROUP.ComboCheck = true;
            this.bp_ITEM_GROUP.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_ITEMGP_SUB;
            this.bp_ITEM_GROUP.IsCodeValueToUpper = true;
            this.bp_ITEM_GROUP.ItemBackColor = System.Drawing.Color.Empty;
            this.bp_ITEM_GROUP.Location = new System.Drawing.Point(268, 27);
            this.bp_ITEM_GROUP.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp_ITEM_GROUP.Name = "bp_ITEM_GROUP";
            this.bp_ITEM_GROUP.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp_ITEM_GROUP.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bp_ITEM_GROUP.SearchCode = true;
            this.bp_ITEM_GROUP.SelectCount = 0;
            this.bp_ITEM_GROUP.SetDefaultValue = false;
            this.bp_ITEM_GROUP.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp_ITEM_GROUP.Size = new System.Drawing.Size(137, 21);
            this.bp_ITEM_GROUP.TabIndex = 107;
            this.bp_ITEM_GROUP.TabStop = false;
            this.bp_ITEM_GROUP.UseAccessGrant = true;
            this.bp_ITEM_GROUP.CodeChanged += new System.EventHandler(this.OnBpControl_CodeChanged);
            this.bp_ITEM_GROUP.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
            this.bp_ITEM_GROUP.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryAfter);
            // 
            // cboFG_ACCT
            // 
            this.cboFG_ACCT.AutoDropDown = true;
            this.cboFG_ACCT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFG_ACCT.Location = new System.Drawing.Point(65, 27);
            this.cboFG_ACCT.Name = "cboFG_ACCT";
            this.cboFG_ACCT.ShowCheckBox = false;
            this.cboFG_ACCT.Size = new System.Drawing.Size(137, 20);
            this.cboFG_ACCT.TabIndex = 3;
            this.cboFG_ACCT.UseKeyEnter = true;
            this.cboFG_ACCT.UseKeyF3 = true;
            // 
            // cbo_CD_PLANT
            // 
            this.cbo_CD_PLANT.AutoDropDown = true;
            this.cbo_CD_PLANT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cbo_CD_PLANT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_CD_PLANT.Location = new System.Drawing.Point(65, 2);
            this.cbo_CD_PLANT.Name = "cbo_CD_PLANT";
            this.cbo_CD_PLANT.ShowCheckBox = false;
            this.cbo_CD_PLANT.Size = new System.Drawing.Size(137, 20);
            this.cbo_CD_PLANT.TabIndex = 0;
            this.cbo_CD_PLANT.UseKeyEnter = true;
            this.cbo_CD_PLANT.UseKeyF3 = true;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel7.Controls.Add(this.label4);
            this.panel7.Location = new System.Drawing.Point(635, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(63, 24);
            this.panel7.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 18);
            this.label4.TabIndex = 76;
            this.label4.Text = "ABC구분";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel14.Controls.Add(this.label7);
            this.panel14.Controls.Add(this.m_lblDy_Io);
            this.panel14.Controls.Add(this.m_lbl_ITEM_GROUP);
            this.panel14.Location = new System.Drawing.Point(206, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(60, 76);
            this.panel14.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 16);
            this.label7.TabIndex = 76;
            this.label7.Text = "중분류";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblDy_Io
            // 
            this.m_lblDy_Io.Location = new System.Drawing.Point(3, 6);
            this.m_lblDy_Io.Name = "m_lblDy_Io";
            this.m_lblDy_Io.Size = new System.Drawing.Size(54, 16);
            this.m_lblDy_Io.TabIndex = 72;
            this.m_lblDy_Io.Text = "기준일자";
            this.m_lblDy_Io.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lbl_ITEM_GROUP
            // 
            this.m_lbl_ITEM_GROUP.Location = new System.Drawing.Point(2, 29);
            this.m_lbl_ITEM_GROUP.Name = "m_lbl_ITEM_GROUP";
            this.m_lbl_ITEM_GROUP.Size = new System.Drawing.Size(54, 16);
            this.m_lbl_ITEM_GROUP.TabIndex = 72;
            this.m_lbl_ITEM_GROUP.Text = "품목군";
            this.m_lbl_ITEM_GROUP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel15.Controls.Add(this.label6);
            this.panel15.Controls.Add(this.m_lblFg_Acct);
            this.panel15.Controls.Add(this.m_lblCd_Plant);
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(62, 76);
            this.panel15.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(5, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 16);
            this.label6.TabIndex = 75;
            this.label6.Text = "대분류";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblFg_Acct
            // 
            this.m_lblFg_Acct.Location = new System.Drawing.Point(2, 27);
            this.m_lblFg_Acct.Name = "m_lblFg_Acct";
            this.m_lblFg_Acct.Size = new System.Drawing.Size(57, 18);
            this.m_lblFg_Acct.TabIndex = 73;
            this.m_lblFg_Acct.Text = "계정구분";
            this.m_lblFg_Acct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblCd_Plant
            // 
            this.m_lblCd_Plant.Location = new System.Drawing.Point(3, 4);
            this.m_lblCd_Plant.Name = "m_lblCd_Plant";
            this.m_lblCd_Plant.Size = new System.Drawing.Size(56, 18);
            this.m_lblCd_Plant.TabIndex = 71;
            this.m_lblCd_Plant.Text = "공장";
            this.m_lblCd_Plant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dp기준일자
            // 
            this.dp기준일자.CalendarBackColor = System.Drawing.Color.White;
            this.dp기준일자.DayColor = System.Drawing.Color.Black;
            this.dp기준일자.FriDayColor = System.Drawing.Color.Blue;
            this.dp기준일자.Location = new System.Drawing.Point(270, 2);
            this.dp기준일자.Mask = "####/##/##";
            this.dp기준일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dp기준일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dp기준일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dp기준일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dp기준일자.Modified = false;
            this.dp기준일자.Name = "dp기준일자";
            this.dp기준일자.PaddingCharacter = '_';
            this.dp기준일자.PassivePromptCharacter = '_';
            this.dp기준일자.PromptCharacter = '_';
            this.dp기준일자.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dp기준일자.ShowToDay = true;
            this.dp기준일자.ShowTodayCircle = true;
            this.dp기준일자.ShowUpDown = false;
            this.dp기준일자.Size = new System.Drawing.Size(92, 21);
            this.dp기준일자.SunDayColor = System.Drawing.Color.Red;
            this.dp기준일자.TabIndex = 1;
            this.dp기준일자.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dp기준일자.TitleForeColor = System.Drawing.Color.Black;
            this.dp기준일자.ToDayColor = System.Drawing.Color.Red;
            this.dp기준일자.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dp기준일자.UseKeyF3 = false;
            this.dp기준일자.Value = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            // 
            // bp_CD_ITEM_END
            // 
            this.bp_CD_ITEM_END.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp_CD_ITEM_END.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_CD_ITEM_END.ButtonImage")));
            this.bp_CD_ITEM_END.ChildMode = "";
            this.bp_CD_ITEM_END.CodeName = "";
            this.bp_CD_ITEM_END.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp_CD_ITEM_END.CodeValue = "";
            this.bp_CD_ITEM_END.ComboCheck = true;
            this.bp_CD_ITEM_END.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.bp_CD_ITEM_END.IsCodeValueToUpper = true;
            this.bp_CD_ITEM_END.ItemBackColor = System.Drawing.Color.Empty;
            this.bp_CD_ITEM_END.Location = new System.Drawing.Point(644, 27);
            this.bp_CD_ITEM_END.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp_CD_ITEM_END.Name = "bp_CD_ITEM_END";
            this.bp_CD_ITEM_END.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp_CD_ITEM_END.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bp_CD_ITEM_END.SearchCode = true;
            this.bp_CD_ITEM_END.SelectCount = 0;
            this.bp_CD_ITEM_END.SetDefaultValue = false;
            this.bp_CD_ITEM_END.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp_CD_ITEM_END.Size = new System.Drawing.Size(140, 21);
            this.bp_CD_ITEM_END.TabIndex = 6;
            this.bp_CD_ITEM_END.TabStop = false;
            this.bp_CD_ITEM_END.UseAccessGrant = true;
            this.bp_CD_ITEM_END.CodeChanged += new System.EventHandler(this.OnBpControl_CodeChanged);
            this.bp_CD_ITEM_END.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
            this.bp_CD_ITEM_END.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryAfter);
            // 
            // bp_CD_ITEM_START
            // 
            this.bp_CD_ITEM_START.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp_CD_ITEM_START.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp_CD_ITEM_START.ButtonImage")));
            this.bp_CD_ITEM_START.ChildMode = "";
            this.bp_CD_ITEM_START.CodeName = "";
            this.bp_CD_ITEM_START.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp_CD_ITEM_START.CodeValue = "";
            this.bp_CD_ITEM_START.ComboCheck = true;
            this.bp_CD_ITEM_START.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.bp_CD_ITEM_START.IsCodeValueToUpper = true;
            this.bp_CD_ITEM_START.ItemBackColor = System.Drawing.Color.Empty;
            this.bp_CD_ITEM_START.Location = new System.Drawing.Point(493, 27);
            this.bp_CD_ITEM_START.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp_CD_ITEM_START.Name = "bp_CD_ITEM_START";
            this.bp_CD_ITEM_START.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp_CD_ITEM_START.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bp_CD_ITEM_START.SearchCode = true;
            this.bp_CD_ITEM_START.SelectCount = 0;
            this.bp_CD_ITEM_START.SetDefaultValue = false;
            this.bp_CD_ITEM_START.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp_CD_ITEM_START.Size = new System.Drawing.Size(137, 21);
            this.bp_CD_ITEM_START.TabIndex = 5;
            this.bp_CD_ITEM_START.TabStop = false;
            this.bp_CD_ITEM_START.UseAccessGrant = true;
            this.bp_CD_ITEM_START.CodeChanged += new System.EventHandler(this.OnBpControl_CodeChanged);
            this.bp_CD_ITEM_START.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryBefore);
            this.bp_CD_ITEM_START.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpControl_QueryAfter);
            // 
            // m_pnlGrid1
            // 
            this.m_pnlGrid1.Controls.Add(this._flex);
            this.m_pnlGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlGrid1.Location = new System.Drawing.Point(3, 87);
            this.m_pnlGrid1.Name = "m_pnlGrid1";
            this.m_pnlGrid1.Size = new System.Drawing.Size(887, 487);
            this.m_pnlGrid1.TabIndex = 100;
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
            this._flex.Location = new System.Drawing.Point(0, 0);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(887, 487);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 9;
            this._flex.BeforeSort += new C1.Win.C1FlexGrid.SortColEventHandler(this._flex_BeforeSort);
            this._flex.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this._flex_AfterSort);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(633, 52);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(63, 24);
            this.panel3.TabIndex = 218;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 18);
            this.label3.TabIndex = 76;
            this.label3.Text = "창고";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpc창고
            // 
            this.bpc창고.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc창고.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc창고.ButtonImage")));
            this.bpc창고.ChildMode = "";
            this.bpc창고.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc창고.ComboCheck = true;
            this.bpc창고.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB1;
            this.bpc창고.IsCodeValueToUpper = true;
            this.bpc창고.ItemBackColor = System.Drawing.SystemColors.Window;
            this.bpc창고.Location = new System.Drawing.Point(702, 53);
            this.bpc창고.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc창고.Name = "bpc창고";
            this.bpc창고.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc창고.SearchCode = true;
            this.bpc창고.SelectCount = 0;
            this.bpc창고.SelectedIndex = -1;
            this.bpc창고.SelectedItem = null;
            this.bpc창고.SelectedText = "";
            this.bpc창고.SelectedValue = null;
            this.bpc창고.SetDefaultValue = false;
            this.bpc창고.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc창고.Size = new System.Drawing.Size(148, 21);
            this.bpc창고.TabIndex = 217;
            this.bpc창고.TabStop = false;
            this.bpc창고.Text = "bpComboBox1";
            this.bpc창고.UseAccessGrant = true;
            // 
            // P_PU_INV_SCH2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_PU_INV_SCH2";
            this.Size = new System.Drawing.Size(905, 620);
            this.TitleText = "창고별재고현황";
            this.mDataArea.ResumeLayout(false);
            this.mDataArea.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dp기준일자)).EndInit();
            this.m_pnlGrid1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel m_pnlGrid1;
        private Dass.FlexGrid.FlexGrid _flex;
        private System.Windows.Forms.Panel panel4;
        private Duzon.Common.Controls.DropDownComboBox cboFG_ABC;
        private Duzon.Common.Controls.CheckBoxExt chk_품목사용체크된건만;
        private Duzon.Common.BpControls.BpCodeTextBox bp_ITEM_GROUP;
        private System.Windows.Forms.Panel panel2;
        private Duzon.Common.Controls.DropDownComboBox cboFG_ACCT;
        private Duzon.Common.Controls.DropDownComboBox cbo_CD_PLANT;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label m_lblDy_Io;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label m_lblFg_Acct;
        private System.Windows.Forms.Label m_lbl_ITEM_GROUP;
        private System.Windows.Forms.Label m_lblCd_Plant;
        private Duzon.Common.Controls.DatePicker dp기준일자;
        private Duzon.Common.BpControls.BpCodeTextBox bp_CD_ITEM_END;
        private Duzon.Common.BpControls.BpCodeTextBox bp_CD_ITEM_START;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private Duzon.Common.BpControls.BpCodeTextBox bp_CLS_L;
        private Duzon.Common.BpControls.BpCodeTextBox bp_CLS_M;
        private Duzon.Common.BpControls.BpCodeTextBox bp_CLS_S;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private Duzon.Common.BpControls.BpComboBox bpc창고;
    }
}

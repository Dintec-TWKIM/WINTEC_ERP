namespace trade
{
    partial class P_TR_TO_LIST
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_TR_TO_LIST));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.txt발주번호 = new Duzon.Common.Controls.TextBoxExt();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtITEM_TO = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_txtITEM_FR = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_txtCdTrans = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_txtGroupRcv = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_comFgLc = new Duzon.Common.Controls.DropDownComboBox();
            this.m_lblFgLc = new Duzon.Common.Controls.LabelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.m_lblCdTrans = new Duzon.Common.Controls.LabelExt();
            this.m_lblDisTermTo = new Duzon.Common.Controls.LabelExt();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl7 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl8 = new Duzon.Common.BpControls.BpPanelControl();
            this.periodPicker = new Duzon.Common.Controls.PeriodPicker();
            this.bpPanelControl9 = new Duzon.Common.BpControls.BpPanelControl();
            this.labelExt3 = new Duzon.Common.Controls.LabelExt();
            this.labelExt4 = new Duzon.Common.Controls.LabelExt();
            this.labelExt5 = new Duzon.Common.Controls.LabelExt();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.oneGridItem3.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl7.SuspendLayout();
            this.bpPanelControl8.SuspendLayout();
            this.bpPanelControl9.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(827, 579);
            this.tableLayoutPanel1.TabIndex = 99;
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
            this._flex.Location = new System.Drawing.Point(3, 96);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(821, 480);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 1;
            // 
            // txt발주번호
            // 
            this.txt발주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt발주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt발주번호.Location = new System.Drawing.Point(81, 1);
            this.txt발주번호.Name = "txt발주번호";
            this.txt발주번호.SelectedAllEnabled = false;
            this.txt발주번호.Size = new System.Drawing.Size(186, 21);
            this.txt발주번호.TabIndex = 238;
            this.txt발주번호.UseKeyEnter = true;
            this.txt발주번호.UseKeyF3 = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(271, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 18);
            this.label1.TabIndex = 237;
            this.label1.Text = "~";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_txtITEM_TO
            // 
            this.m_txtITEM_TO.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.m_txtITEM_TO.ButtonImage = ((System.Drawing.Image)(resources.GetObject("m_txtITEM_TO.ButtonImage")));
            this.m_txtITEM_TO.ChildMode = "";
            this.m_txtITEM_TO.CodeName = "";
            this.m_txtITEM_TO.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.m_txtITEM_TO.CodeValue = "";
            this.m_txtITEM_TO.ComboCheck = true;
            this.m_txtITEM_TO.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.m_txtITEM_TO.IsCodeValueToUpper = true;
            this.m_txtITEM_TO.ItemBackColor = System.Drawing.Color.Empty;
            this.m_txtITEM_TO.Location = new System.Drawing.Point(287, 1);
            this.m_txtITEM_TO.MaximumSize = new System.Drawing.Size(0, 21);
            this.m_txtITEM_TO.Name = "m_txtITEM_TO";
            this.m_txtITEM_TO.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.m_txtITEM_TO.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.m_txtITEM_TO.SearchCode = true;
            this.m_txtITEM_TO.SelectCount = 0;
            this.m_txtITEM_TO.SetDefaultValue = false;
            this.m_txtITEM_TO.SetNoneTypeMsg = "Please! Set Help Type!";
            this.m_txtITEM_TO.Size = new System.Drawing.Size(250, 21);
            this.m_txtITEM_TO.TabIndex = 236;
            this.m_txtITEM_TO.TabStop = false;
            this.m_txtITEM_TO.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryBefore);
            // 
            // m_txtITEM_FR
            // 
            this.m_txtITEM_FR.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.m_txtITEM_FR.ButtonImage = ((System.Drawing.Image)(resources.GetObject("m_txtITEM_FR.ButtonImage")));
            this.m_txtITEM_FR.ChildMode = "";
            this.m_txtITEM_FR.CodeName = "";
            this.m_txtITEM_FR.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.m_txtITEM_FR.CodeValue = "";
            this.m_txtITEM_FR.ComboCheck = true;
            this.m_txtITEM_FR.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB;
            this.m_txtITEM_FR.IsCodeValueToUpper = true;
            this.m_txtITEM_FR.ItemBackColor = System.Drawing.Color.Empty;
            this.m_txtITEM_FR.Location = new System.Drawing.Point(81, 1);
            this.m_txtITEM_FR.MaximumSize = new System.Drawing.Size(0, 21);
            this.m_txtITEM_FR.Name = "m_txtITEM_FR";
            this.m_txtITEM_FR.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.m_txtITEM_FR.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.m_txtITEM_FR.SearchCode = true;
            this.m_txtITEM_FR.SelectCount = 0;
            this.m_txtITEM_FR.SetDefaultValue = false;
            this.m_txtITEM_FR.SetNoneTypeMsg = "Please! Set Help Type!";
            this.m_txtITEM_FR.Size = new System.Drawing.Size(186, 21);
            this.m_txtITEM_FR.TabIndex = 235;
            this.m_txtITEM_FR.TabStop = false;
            this.m_txtITEM_FR.QueryBefore += new Duzon.Common.BpControls.BpQueryHandler(this.OnBpCodeTextBox_QueryBefore);
            // 
            // m_txtCdTrans
            // 
            this.m_txtCdTrans.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_txtCdTrans.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.m_txtCdTrans.ButtonImage = ((System.Drawing.Image)(resources.GetObject("m_txtCdTrans.ButtonImage")));
            this.m_txtCdTrans.ChildMode = "";
            this.m_txtCdTrans.CodeName = "";
            this.m_txtCdTrans.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.m_txtCdTrans.CodeValue = "";
            this.m_txtCdTrans.ComboCheck = true;
            this.m_txtCdTrans.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.m_txtCdTrans.IsCodeValueToUpper = true;
            this.m_txtCdTrans.ItemBackColor = System.Drawing.Color.Empty;
            this.m_txtCdTrans.Location = new System.Drawing.Point(81, 1);
            this.m_txtCdTrans.MaximumSize = new System.Drawing.Size(0, 21);
            this.m_txtCdTrans.Name = "m_txtCdTrans";
            this.m_txtCdTrans.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.m_txtCdTrans.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.m_txtCdTrans.SearchCode = true;
            this.m_txtCdTrans.SelectCount = 0;
            this.m_txtCdTrans.SetDefaultValue = false;
            this.m_txtCdTrans.SetNoneTypeMsg = "Please! Set Help Type!";
            this.m_txtCdTrans.Size = new System.Drawing.Size(186, 21);
            this.m_txtCdTrans.TabIndex = 3;
            this.m_txtCdTrans.TabStop = false;
            // 
            // m_txtGroupRcv
            // 
            this.m_txtGroupRcv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.m_txtGroupRcv.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.m_txtGroupRcv.ButtonImage = ((System.Drawing.Image)(resources.GetObject("m_txtGroupRcv.ButtonImage")));
            this.m_txtGroupRcv.ChildMode = "";
            this.m_txtGroupRcv.CodeName = "";
            this.m_txtGroupRcv.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.m_txtGroupRcv.CodeValue = "";
            this.m_txtGroupRcv.ComboCheck = true;
            this.m_txtGroupRcv.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PURGRP_SUB;
            this.m_txtGroupRcv.IsCodeValueToUpper = true;
            this.m_txtGroupRcv.ItemBackColor = System.Drawing.Color.Empty;
            this.m_txtGroupRcv.Location = new System.Drawing.Point(81, 1);
            this.m_txtGroupRcv.MaximumSize = new System.Drawing.Size(0, 21);
            this.m_txtGroupRcv.Name = "m_txtGroupRcv";
            this.m_txtGroupRcv.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.m_txtGroupRcv.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.m_txtGroupRcv.SearchCode = true;
            this.m_txtGroupRcv.SelectCount = 0;
            this.m_txtGroupRcv.SetDefaultValue = false;
            this.m_txtGroupRcv.SetNoneTypeMsg = "Please! Set Help Type!";
            this.m_txtGroupRcv.Size = new System.Drawing.Size(186, 21);
            this.m_txtGroupRcv.TabIndex = 4;
            this.m_txtGroupRcv.TabStop = false;
            // 
            // m_comFgLc
            // 
            this.m_comFgLc.AutoDropDown = true;
            this.m_comFgLc.BackColor = System.Drawing.SystemColors.Window;
            this.m_comFgLc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_comFgLc.ItemHeight = 12;
            this.m_comFgLc.Location = new System.Drawing.Point(81, 1);
            this.m_comFgLc.Name = "m_comFgLc";
            this.m_comFgLc.ShowCheckBox = false;
            this.m_comFgLc.Size = new System.Drawing.Size(186, 20);
            this.m_comFgLc.TabIndex = 2;
            this.m_comFgLc.UseKeyEnter = false;
            this.m_comFgLc.UseKeyF3 = false;
            // 
            // m_lblFgLc
            // 
            this.m_lblFgLc.Location = new System.Drawing.Point(0, 3);
            this.m_lblFgLc.Name = "m_lblFgLc";
            this.m_lblFgLc.Resizeble = true;
            this.m_lblFgLc.Size = new System.Drawing.Size(80, 16);
            this.m_lblFgLc.TabIndex = 1;
            this.m_lblFgLc.Tag = "FG_LC";
            this.m_lblFgLc.Text = "L/C구분";
            this.m_lblFgLc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelExt1
            // 
            this.labelExt1.Location = new System.Drawing.Point(169, 493);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(98, 18);
            this.labelExt1.TabIndex = 3;
            this.labelExt1.Tag = "CD_TRANS";
            this.labelExt1.Text = "품목";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblCdTrans
            // 
            this.m_lblCdTrans.Location = new System.Drawing.Point(0, 3);
            this.m_lblCdTrans.Name = "m_lblCdTrans";
            this.m_lblCdTrans.Resizeble = true;
            this.m_lblCdTrans.Size = new System.Drawing.Size(80, 16);
            this.m_lblCdTrans.TabIndex = 2;
            this.m_lblCdTrans.Tag = "CD_TRANS";
            this.m_lblCdTrans.Text = "거래처";
            this.m_lblCdTrans.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblDisTermTo
            // 
            this.m_lblDisTermTo.Location = new System.Drawing.Point(0, 3);
            this.m_lblDisTermTo.Name = "m_lblDisTermTo";
            this.m_lblDisTermTo.Resizeble = true;
            this.m_lblDisTermTo.Size = new System.Drawing.Size(80, 16);
            this.m_lblDisTermTo.TabIndex = 1;
            this.m_lblDisTermTo.Tag = "DIS_TERMTO";
            this.m_lblDisTermTo.Text = "신고기간";
            this.m_lblDisTermTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGrid1
            // 
            this.oneGrid1.AutoScroll = true;
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(821, 87);
            this.oneGrid1.TabIndex = 2;
            // 
            // oneGridItem3
            // 
            this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem3.Controls.Add(this.bpPanelControl3);
            this.oneGridItem3.Controls.Add(this.bpPanelControl2);
            this.oneGridItem3.Controls.Add(this.bpPanelControl1);
            this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
            this.oneGridItem3.Name = "oneGridItem3";
            this.oneGridItem3.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem3.TabIndex = 2;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Location = new System.Drawing.Point(540, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl3.TabIndex = 2;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Location = new System.Drawing.Point(271, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl2.TabIndex = 1;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.txt발주번호);
            this.bpPanelControl1.Controls.Add(this.labelExt3);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl1.TabIndex = 0;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.labelExt5);
            this.bpPanelControl5.Controls.Add(this.labelExt1);
            this.bpPanelControl5.Controls.Add(this.m_txtITEM_FR);
            this.bpPanelControl5.Controls.Add(this.label1);
            this.bpPanelControl5.Controls.Add(this.m_txtITEM_TO);
            this.bpPanelControl5.Location = new System.Drawing.Point(271, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(542, 23);
            this.bpPanelControl5.TabIndex = 1;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.labelExt4);
            this.bpPanelControl6.Controls.Add(this.m_txtGroupRcv);
            this.bpPanelControl6.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl6.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl7);
            this.oneGridItem1.Controls.Add(this.bpPanelControl8);
            this.oneGridItem1.Controls.Add(this.bpPanelControl9);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl7
            // 
            this.bpPanelControl7.Controls.Add(this.m_txtCdTrans);
            this.bpPanelControl7.Controls.Add(this.m_lblCdTrans);
            this.bpPanelControl7.Location = new System.Drawing.Point(540, 1);
            this.bpPanelControl7.Name = "bpPanelControl7";
            this.bpPanelControl7.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl7.TabIndex = 2;
            // 
            // bpPanelControl8
            // 
            this.bpPanelControl8.Controls.Add(this.m_comFgLc);
            this.bpPanelControl8.Controls.Add(this.m_lblFgLc);
            this.bpPanelControl8.Location = new System.Drawing.Point(271, 1);
            this.bpPanelControl8.Name = "bpPanelControl8";
            this.bpPanelControl8.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl8.TabIndex = 1;
            // 
            // periodPicker
            // 
            this.periodPicker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.periodPicker.IsNecessaryCondition = true;
            this.periodPicker.Location = new System.Drawing.Point(81, 1);
            this.periodPicker.Mask = "####/##/##";
            this.periodPicker.MaximumSize = new System.Drawing.Size(185, 21);
            this.periodPicker.MinimumSize = new System.Drawing.Size(185, 21);
            this.periodPicker.Name = "periodPicker";
            this.periodPicker.Size = new System.Drawing.Size(185, 21);
            this.periodPicker.TabIndex = 73;
            // 
            // bpPanelControl9
            // 
            this.bpPanelControl9.Controls.Add(this.periodPicker);
            this.bpPanelControl9.Controls.Add(this.m_lblDisTermTo);
            this.bpPanelControl9.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl9.Name = "bpPanelControl9";
            this.bpPanelControl9.Size = new System.Drawing.Size(267, 23);
            this.bpPanelControl9.TabIndex = 0;
            // 
            // labelExt3
            // 
            this.labelExt3.Location = new System.Drawing.Point(0, 3);
            this.labelExt3.Name = "labelExt3";
            this.labelExt3.Resizeble = true;
            this.labelExt3.Size = new System.Drawing.Size(80, 16);
            this.labelExt3.TabIndex = 74;
            this.labelExt3.Tag = "DIS_TERMTO";
            this.labelExt3.Text = "발주번호";
            this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelExt4
            // 
            this.labelExt4.Location = new System.Drawing.Point(0, 3);
            this.labelExt4.Name = "labelExt4";
            this.labelExt4.Resizeble = true;
            this.labelExt4.Size = new System.Drawing.Size(80, 16);
            this.labelExt4.TabIndex = 74;
            this.labelExt4.Tag = "DIS_TERMTO";
            this.labelExt4.Text = "구매그룹";
            this.labelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelExt5
            // 
            this.labelExt5.Location = new System.Drawing.Point(0, 3);
            this.labelExt5.Name = "labelExt5";
            this.labelExt5.Resizeble = true;
            this.labelExt5.Size = new System.Drawing.Size(80, 16);
            this.labelExt5.TabIndex = 3;
            this.labelExt5.Tag = "FG_LC";
            this.labelExt5.Text = "품목코드";
            this.labelExt5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // P_TR_TO_LIST
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_TR_TO_LIST";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.oneGridItem3.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl1.PerformLayout();
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl7.ResumeLayout(false);
            this.bpPanelControl8.ResumeLayout(false);
            this.bpPanelControl9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.BpControls.BpCodeTextBox m_txtCdTrans;
        private Duzon.Common.BpControls.BpCodeTextBox m_txtGroupRcv;
        private Duzon.Common.Controls.DropDownComboBox m_comFgLc;
        private Duzon.Common.Controls.LabelExt m_lblFgLc;
        private Duzon.Common.Controls.LabelExt m_lblCdTrans;
        private Duzon.Common.Controls.LabelExt m_lblDisTermTo;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private System.Windows.Forms.Label label1;
        private Duzon.Common.BpControls.BpCodeTextBox m_txtITEM_TO;
        private Duzon.Common.BpControls.BpCodeTextBox m_txtITEM_FR;
        private Duzon.Common.Controls.TextBoxExt txt발주번호;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.LabelExt labelExt3;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl7;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl8;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl9;
        private Duzon.Common.Controls.PeriodPicker periodPicker;
        private Duzon.Common.Controls.LabelExt labelExt4;
        private Duzon.Common.Controls.LabelExt labelExt5;
    }
}

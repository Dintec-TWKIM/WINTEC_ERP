namespace cz
{
    partial class P_CZ_CAR_MNG_CONFIRM_Y
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_CAR_MNG_CONFIRM_Y));
            this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelEx3 = new Duzon.Common.Controls.PanelEx();
            this.btn_처리 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this._pnlMain = new System.Windows.Forms.Panel();
            this.bpC작성자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc부가세사업장 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpc사업장 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.dtpOut매출일자 = new Duzon.Common.Controls.DatePicker();
            this.dtpTO = new Duzon.Common.Controls.DatePicker();
            this.dtpFROM = new Duzon.Common.Controls.DatePicker();
            this.txt화주 = new Duzon.Common.Controls.TextBoxExt();
            this.panelExt4 = new Duzon.Common.Controls.PanelExt();
            this.panelExt5 = new Duzon.Common.Controls.PanelExt();
            this.panelExt6 = new Duzon.Common.Controls.PanelExt();
            this.labelExt2 = new Duzon.Common.Controls.LabelExt();
            this.lblTitle02 = new Duzon.Common.Controls.LabelExt();
            this.lblTitle05 = new Duzon.Common.Controls.LabelExt();
            this.panelExt7 = new Duzon.Common.Controls.PanelExt();
            this.labelExt3 = new Duzon.Common.Controls.LabelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.lblTitle01 = new Duzon.Common.Controls.LabelExt();
            this.mDataArea.SuspendLayout();
            this._tlayMain.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this._pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpOut매출일자)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFROM)).BeginInit();
            this.panelExt6.SuspendLayout();
            this.panelExt7.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this._tlayMain);
            // 
            // _tlayMain
            // 
            this._tlayMain.AutoSize = true;
            this._tlayMain.ColumnCount = 1;
            this._tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlayMain.Controls.Add(this.panelEx3, 0, 1);
            this._tlayMain.Controls.Add(this.panel2, 0, 1);
            this._tlayMain.Controls.Add(this._pnlMain, 0, 0);
            this._tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tlayMain.Location = new System.Drawing.Point(0, 0);
            this._tlayMain.Name = "_tlayMain";
            this._tlayMain.RowCount = 2;
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlayMain.Size = new System.Drawing.Size(827, 579);
            this._tlayMain.TabIndex = 7;
            // 
            // panelEx3
            // 
            this.panelEx3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEx3.ColorA = System.Drawing.Color.Empty;
            this.panelEx3.ColorB = System.Drawing.Color.Empty;
            this.panelEx3.Controls.Add(this.btn_처리);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(3, 90);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(821, 26);
            this.panelEx3.TabIndex = 231;
            // 
            // btn_처리
            // 
            this.btn_처리.BackColor = System.Drawing.Color.Transparent;
            this.btn_처리.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_처리.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_처리.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_처리.Location = new System.Drawing.Point(749, 0);
            this.btn_처리.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn_처리.Name = "btn_처리";
            this.btn_처리.Size = new System.Drawing.Size(70, 19);
            this.btn_처리.TabIndex = 0;
            this.btn_처리.TabStop = false;
            this.btn_처리.Text = "매출마감";
            this.btn_처리.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._flex);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 122);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(821, 474);
            this.panel2.TabIndex = 72;
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
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
            this._flex.Size = new System.Drawing.Size(821, 474);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 6;
            this._flex.UseGridCalculator = true;
            // 
            // _pnlMain
            // 
            this._pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pnlMain.Controls.Add(this.bpC작성자);
            this._pnlMain.Controls.Add(this.bpc부가세사업장);
            this._pnlMain.Controls.Add(this.bpc사업장);
            this._pnlMain.Controls.Add(this.dtpOut매출일자);
            this._pnlMain.Controls.Add(this.dtpTO);
            this._pnlMain.Controls.Add(this.dtpFROM);
            this._pnlMain.Controls.Add(this.txt화주);
            this._pnlMain.Controls.Add(this.panelExt4);
            this._pnlMain.Controls.Add(this.panelExt5);
            this._pnlMain.Controls.Add(this.panelExt6);
            this._pnlMain.Controls.Add(this.panelExt7);
            this._pnlMain.Location = new System.Drawing.Point(3, 3);
            this._pnlMain.Name = "_pnlMain";
            this._pnlMain.Size = new System.Drawing.Size(821, 81);
            this._pnlMain.TabIndex = 230;
            // 
            // bpC작성자
            // 
            this.bpC작성자.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpC작성자.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpC작성자.ButtonImage")));
            this.bpC작성자.ChildMode = "";
            this.bpC작성자.CodeName = "";
            this.bpC작성자.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpC작성자.CodeValue = "";
            this.bpC작성자.ComboCheck = true;
            this.bpC작성자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.bpC작성자.IsCodeValueToUpper = true;
            this.bpC작성자.ItemBackColor = System.Drawing.Color.White;
            this.bpC작성자.Location = new System.Drawing.Point(95, 56);
            this.bpC작성자.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpC작성자.Name = "bpC작성자";
            this.bpC작성자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpC작성자.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpC작성자.SearchCode = true;
            this.bpC작성자.SelectCount = 0;
            this.bpC작성자.SetDefaultValue = false;
            this.bpC작성자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpC작성자.Size = new System.Drawing.Size(169, 21);
            this.bpC작성자.TabIndex = 142;
            this.bpC작성자.TabStop = false;
            this.bpC작성자.UserCodeName = "NM_KOR";
            this.bpC작성자.UserCodeValue = "NAME";
            this.bpC작성자.UserParams = "NAME";
            // 
            // bpc부가세사업장
            // 
            this.bpc부가세사업장.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc부가세사업장.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc부가세사업장.ButtonImage")));
            this.bpc부가세사업장.ChildMode = "";
            this.bpc부가세사업장.CodeName = "";
            this.bpc부가세사업장.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc부가세사업장.CodeValue = "";
            this.bpc부가세사업장.ComboCheck = true;
            this.bpc부가세사업장.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BIZAREA_SUB;
            this.bpc부가세사업장.IsCodeValueToUpper = true;
            this.bpc부가세사업장.ItemBackColor = System.Drawing.Color.White;
            this.bpc부가세사업장.Location = new System.Drawing.Point(419, 29);
            this.bpc부가세사업장.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc부가세사업장.Name = "bpc부가세사업장";
            this.bpc부가세사업장.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc부가세사업장.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc부가세사업장.SearchCode = true;
            this.bpc부가세사업장.SelectCount = 0;
            this.bpc부가세사업장.SetDefaultValue = false;
            this.bpc부가세사업장.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc부가세사업장.Size = new System.Drawing.Size(169, 21);
            this.bpc부가세사업장.TabIndex = 140;
            this.bpc부가세사업장.TabStop = false;
            this.bpc부가세사업장.UserCodeName = "NM_KOR";
            this.bpc부가세사업장.UserCodeValue = "NAME";
            this.bpc부가세사업장.UserParams = "NAME";
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
            this.bpc사업장.Location = new System.Drawing.Point(419, 2);
            this.bpc사업장.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc사업장.Name = "bpc사업장";
            this.bpc사업장.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc사업장.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc사업장.SearchCode = true;
            this.bpc사업장.SelectCount = 0;
            this.bpc사업장.SetDefaultValue = false;
            this.bpc사업장.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc사업장.Size = new System.Drawing.Size(169, 21);
            this.bpc사업장.TabIndex = 139;
            this.bpc사업장.TabStop = false;
            this.bpc사업장.UserCodeName = "NM_KOR";
            this.bpc사업장.UserCodeValue = "NAME";
            this.bpc사업장.UserParams = "NAME";
            // 
            // dtpOut매출일자
            // 
            this.dtpOut매출일자.CalendarBackColor = System.Drawing.Color.White;
            this.dtpOut매출일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpOut매출일자.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtpOut매출일자.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtpOut매출일자.Location = new System.Drawing.Point(421, 56);
            this.dtpOut매출일자.Mask = "####/##/##";
            this.dtpOut매출일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtpOut매출일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtpOut매출일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtpOut매출일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtpOut매출일자.Modified = true;
            this.dtpOut매출일자.Name = "dtpOut매출일자";
            this.dtpOut매출일자.NullCheck = false;
            this.dtpOut매출일자.PaddingCharacter = '_';
            this.dtpOut매출일자.PassivePromptCharacter = '_';
            this.dtpOut매출일자.PromptCharacter = '_';
            this.dtpOut매출일자.SelectedDayColor = System.Drawing.Color.White;
            this.dtpOut매출일자.ShowToDay = true;
            this.dtpOut매출일자.ShowTodayCircle = true;
            this.dtpOut매출일자.ShowUpDown = false;
            this.dtpOut매출일자.Size = new System.Drawing.Size(85, 21);
            this.dtpOut매출일자.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtpOut매출일자.TabIndex = 137;
            this.dtpOut매출일자.TitleBackColor = System.Drawing.Color.White;
            this.dtpOut매출일자.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtpOut매출일자.ToDayColor = System.Drawing.Color.Red;
            this.dtpOut매출일자.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtpOut매출일자.UseKeyF3 = false;
            this.dtpOut매출일자.Value = new System.DateTime(((long)(0)));
            // 
            // dtpTO
            // 
            this.dtpTO.CalendarBackColor = System.Drawing.Color.White;
            this.dtpTO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpTO.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtpTO.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtpTO.Location = new System.Drawing.Point(210, 6);
            this.dtpTO.Mask = "####/##/##";
            this.dtpTO.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtpTO.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtpTO.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtpTO.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtpTO.Modified = true;
            this.dtpTO.Name = "dtpTO";
            this.dtpTO.NullCheck = false;
            this.dtpTO.PaddingCharacter = '_';
            this.dtpTO.PassivePromptCharacter = '_';
            this.dtpTO.PromptCharacter = '_';
            this.dtpTO.SelectedDayColor = System.Drawing.Color.White;
            this.dtpTO.ShowToDay = true;
            this.dtpTO.ShowTodayCircle = true;
            this.dtpTO.ShowUpDown = false;
            this.dtpTO.Size = new System.Drawing.Size(85, 21);
            this.dtpTO.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtpTO.TabIndex = 128;
            this.dtpTO.TitleBackColor = System.Drawing.Color.White;
            this.dtpTO.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtpTO.ToDayColor = System.Drawing.Color.Red;
            this.dtpTO.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtpTO.UseKeyF3 = false;
            this.dtpTO.Value = new System.DateTime(((long)(0)));
            // 
            // dtpFROM
            // 
            this.dtpFROM.CalendarBackColor = System.Drawing.Color.White;
            this.dtpFROM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFROM.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtpFROM.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtpFROM.Location = new System.Drawing.Point(95, 6);
            this.dtpFROM.Mask = "####/##/##";
            this.dtpFROM.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtpFROM.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtpFROM.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtpFROM.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtpFROM.Modified = true;
            this.dtpFROM.Name = "dtpFROM";
            this.dtpFROM.NullCheck = false;
            this.dtpFROM.PaddingCharacter = '_';
            this.dtpFROM.PassivePromptCharacter = '_';
            this.dtpFROM.PromptCharacter = '_';
            this.dtpFROM.SelectedDayColor = System.Drawing.Color.White;
            this.dtpFROM.ShowToDay = true;
            this.dtpFROM.ShowTodayCircle = true;
            this.dtpFROM.ShowUpDown = false;
            this.dtpFROM.Size = new System.Drawing.Size(85, 21);
            this.dtpFROM.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtpFROM.TabIndex = 127;
            this.dtpFROM.TitleBackColor = System.Drawing.Color.White;
            this.dtpFROM.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtpFROM.ToDayColor = System.Drawing.Color.Red;
            this.dtpFROM.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtpFROM.UseKeyF3 = false;
            this.dtpFROM.Value = new System.DateTime(((long)(0)));
            // 
            // txt화주
            // 
            this.txt화주.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt화주.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt화주.Location = new System.Drawing.Point(94, 30);
            this.txt화주.Name = "txt화주";
            this.txt화주.SelectedAllEnabled = false;
            this.txt화주.Size = new System.Drawing.Size(201, 21);
            this.txt화주.TabIndex = 124;
            this.txt화주.UseKeyEnter = true;
            this.txt화주.UseKeyF3 = true;
            // 
            // panelExt4
            // 
            this.panelExt4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt4.BackColor = System.Drawing.Color.Transparent;
            this.panelExt4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt4.BackgroundImage")));
            this.panelExt4.Location = new System.Drawing.Point(6, 52);
            this.panelExt4.Name = "panelExt4";
            this.panelExt4.Size = new System.Drawing.Size(811, 1);
            this.panelExt4.TabIndex = 120;
            // 
            // panelExt5
            // 
            this.panelExt5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt5.BackColor = System.Drawing.Color.Transparent;
            this.panelExt5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt5.BackgroundImage")));
            this.panelExt5.Location = new System.Drawing.Point(6, 26);
            this.panelExt5.Name = "panelExt5";
            this.panelExt5.Size = new System.Drawing.Size(811, 1);
            this.panelExt5.TabIndex = 119;
            // 
            // panelExt6
            // 
            this.panelExt6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt6.Controls.Add(this.labelExt2);
            this.panelExt6.Controls.Add(this.lblTitle02);
            this.panelExt6.Controls.Add(this.lblTitle05);
            this.panelExt6.Location = new System.Drawing.Point(325, 2);
            this.panelExt6.Name = "panelExt6";
            this.panelExt6.Size = new System.Drawing.Size(90, 78);
            this.panelExt6.TabIndex = 38;
            // 
            // labelExt2
            // 
            this.labelExt2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt2.Location = new System.Drawing.Point(3, 54);
            this.labelExt2.Name = "labelExt2";
            this.labelExt2.Resizeble = true;
            this.labelExt2.Size = new System.Drawing.Size(85, 18);
            this.labelExt2.TabIndex = 11;
            this.labelExt2.Tag = "";
            this.labelExt2.Text = "매출일자";
            this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle02
            // 
            this.lblTitle02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lblTitle02.Location = new System.Drawing.Point(3, 7);
            this.lblTitle02.Name = "lblTitle02";
            this.lblTitle02.Resizeble = true;
            this.lblTitle02.Size = new System.Drawing.Size(85, 18);
            this.lblTitle02.TabIndex = 5;
            this.lblTitle02.Tag = "";
            this.lblTitle02.Text = "사업장";
            this.lblTitle02.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle05
            // 
            this.lblTitle05.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lblTitle05.Location = new System.Drawing.Point(3, 29);
            this.lblTitle05.Name = "lblTitle05";
            this.lblTitle05.Resizeble = true;
            this.lblTitle05.Size = new System.Drawing.Size(85, 18);
            this.lblTitle05.TabIndex = 10;
            this.lblTitle05.Tag = "";
            this.lblTitle05.Text = "부가세사업장";
            this.lblTitle05.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelExt7
            // 
            this.panelExt7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt7.Controls.Add(this.labelExt3);
            this.panelExt7.Controls.Add(this.labelExt1);
            this.panelExt7.Controls.Add(this.lblTitle01);
            this.panelExt7.Location = new System.Drawing.Point(1, 1);
            this.panelExt7.Name = "panelExt7";
            this.panelExt7.Size = new System.Drawing.Size(90, 79);
            this.panelExt7.TabIndex = 37;
            // 
            // labelExt3
            // 
            this.labelExt3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt3.Location = new System.Drawing.Point(2, 55);
            this.labelExt3.Name = "labelExt3";
            this.labelExt3.Resizeble = true;
            this.labelExt3.Size = new System.Drawing.Size(85, 18);
            this.labelExt3.TabIndex = 12;
            this.labelExt3.Tag = "";
            this.labelExt3.Text = "작성자";
            this.labelExt3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt1.Location = new System.Drawing.Point(3, 4);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(85, 18);
            this.labelExt1.TabIndex = 11;
            this.labelExt1.Tag = "";
            this.labelExt1.Text = "출고일자";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle01
            // 
            this.lblTitle01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lblTitle01.Location = new System.Drawing.Point(3, 31);
            this.lblTitle01.Name = "lblTitle01";
            this.lblTitle01.Resizeble = true;
            this.lblTitle01.Size = new System.Drawing.Size(85, 18);
            this.lblTitle01.TabIndex = 0;
            this.lblTitle01.Tag = "";
            this.lblTitle01.Text = "화주명";
            this.lblTitle01.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // P_CZ_CAR_MNG_CONFIRM_Y
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_CZ_CAR_MNG_CONFIRM_Y";
            this.mDataArea.ResumeLayout(false);
            this.mDataArea.PerformLayout();
            this._tlayMain.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this._pnlMain.ResumeLayout(false);
            this._pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpOut매출일자)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFROM)).EndInit();
            this.panelExt6.ResumeLayout(false);
            this.panelExt7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _tlayMain;
        private System.Windows.Forms.Panel panel2;
        private Dass.FlexGrid.FlexGrid _flex;
        private System.Windows.Forms.Panel _pnlMain;
        private Duzon.Common.Controls.PanelExt panelExt6;
        private Duzon.Common.Controls.LabelExt lblTitle02;
        private Duzon.Common.Controls.PanelExt panelExt7;
        private Duzon.Common.Controls.LabelExt lblTitle01;
        private Duzon.Common.Controls.LabelExt lblTitle05;
        private Duzon.Common.Controls.PanelExt panelExt4;
        private Duzon.Common.Controls.PanelExt panelExt5;
        private Duzon.Common.Controls.TextBoxExt txt화주;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.DatePicker dtpTO;
        private Duzon.Common.Controls.DatePicker dtpFROM;
        private Duzon.Common.Controls.PanelEx panelEx3;
        private Duzon.Common.Controls.RoundedButton btn_처리;
        private Duzon.Common.Controls.DatePicker dtpOut매출일자;
        private Duzon.Common.Controls.LabelExt labelExt2;
        private Duzon.Common.Controls.LabelExt labelExt3;
        private Duzon.Common.BpControls.BpCodeTextBox bpc부가세사업장;
        private Duzon.Common.BpControls.BpCodeTextBox bpc사업장;
        private Duzon.Common.BpControls.BpCodeTextBox bpC작성자;


    }
}
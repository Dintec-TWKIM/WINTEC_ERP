namespace sale
{
    partial class P_SA_Z_NICEGR_SA_PLAN_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_Z_NICEGR_SA_PLAN_SUB));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt차수 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl차수 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpWeekNo = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lblweekno = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp년도 = new Duzon.Common.Controls.DatePicker();
            this.lbl년도 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo환종 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl환종 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.bp거래처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl거래처 = new Duzon.Common.Controls.LabelExt();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.btn닫기 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp년도)).BeginInit();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.panelExt1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelExt1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 50);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(775, 500);
            this.tableLayoutPanel1.TabIndex = 0;
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
            this._flex.Location = new System.Drawing.Point(3, 102);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(769, 395);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 2;
            this._flex.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(769, 63);
            this.oneGrid1.TabIndex = 1;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl3);
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(759, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.txt차수);
            this.bpPanelControl3.Controls.Add(this.lbl차수);
            this.bpPanelControl3.Location = new System.Drawing.Point(506, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(250, 23);
            this.bpPanelControl3.TabIndex = 2;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // txt차수
            // 
            this.txt차수.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt차수.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt차수.Location = new System.Drawing.Point(81, 1);
            this.txt차수.Name = "txt차수";
            this.txt차수.SelectedAllEnabled = false;
            this.txt차수.Size = new System.Drawing.Size(167, 21);
            this.txt차수.TabIndex = 6;
            this.txt차수.UseKeyEnter = true;
            this.txt차수.UseKeyF3 = true;
            // 
            // lbl차수
            // 
            this.lbl차수.Location = new System.Drawing.Point(0, 3);
            this.lbl차수.Name = "lbl차수";
            this.lbl차수.Resizeble = true;
            this.lbl차수.Size = new System.Drawing.Size(80, 16);
            this.lbl차수.TabIndex = 5;
            this.lbl차수.Text = "차수";
            this.lbl차수.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.bpWeekNo);
            this.bpPanelControl2.Controls.Add(this.lblweekno);
            this.bpPanelControl2.Location = new System.Drawing.Point(254, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(250, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // bpWeekNo
            // 
            this.bpWeekNo.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpWeekNo.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpWeekNo.ButtonImage")));
            this.bpWeekNo.ChildMode = "";
            this.bpWeekNo.CodeName = "";
            this.bpWeekNo.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpWeekNo.CodeValue = "";
            this.bpWeekNo.ComboCheck = true;
            this.bpWeekNo.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.bpWeekNo.IsCodeValueToUpper = true;
            this.bpWeekNo.ItemBackColor = System.Drawing.Color.White;
            this.bpWeekNo.Location = new System.Drawing.Point(81, 1);
            this.bpWeekNo.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpWeekNo.Name = "bpWeekNo";
            this.bpWeekNo.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpWeekNo.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpWeekNo.SearchCode = true;
            this.bpWeekNo.SelectCount = 0;
            this.bpWeekNo.SetDefaultValue = false;
            this.bpWeekNo.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpWeekNo.Size = new System.Drawing.Size(167, 21);
            this.bpWeekNo.TabIndex = 5;
            this.bpWeekNo.TabStop = false;
            this.bpWeekNo.Text = "bpCodeTextBox1";
            this.bpWeekNo.UserCodeName = "NM_NO_WEEK";
            this.bpWeekNo.UserCodeValue = "NO_WEEK";
            this.bpWeekNo.UserHelpID = "H_SA_Z_NICEGR_WEEKNO_SUB";
            // 
            // lblweekno
            // 
            this.lblweekno.Location = new System.Drawing.Point(0, 3);
            this.lblweekno.Name = "lblweekno";
            this.lblweekno.Resizeble = true;
            this.lblweekno.Size = new System.Drawing.Size(80, 16);
            this.lblweekno.TabIndex = 4;
            this.lblweekno.Text = "Week No";
            this.lblweekno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp년도);
            this.bpPanelControl1.Controls.Add(this.lbl년도);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(250, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // dtp년도
            // 
            this.dtp년도.CalendarBackColor = System.Drawing.Color.White;
            this.dtp년도.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp년도.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp년도.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp년도.Location = new System.Drawing.Point(81, 1);
            this.dtp년도.Mask = "####";
            this.dtp년도.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp년도.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp년도.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp년도.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp년도.Modified = true;
            this.dtp년도.Name = "dtp년도";
            this.dtp년도.NullCheck = false;
            this.dtp년도.PaddingCharacter = '_';
            this.dtp년도.PassivePromptCharacter = '_';
            this.dtp년도.PromptCharacter = '_';
            this.dtp년도.SelectedDayColor = System.Drawing.Color.White;
            this.dtp년도.ShowToDay = true;
            this.dtp년도.ShowTodayCircle = true;
            this.dtp년도.ShowUpDown = true;
            this.dtp년도.Size = new System.Drawing.Size(75, 21);
            this.dtp년도.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp년도.TabIndex = 6;
            this.dtp년도.TitleBackColor = System.Drawing.Color.White;
            this.dtp년도.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp년도.ToDayColor = System.Drawing.Color.Red;
            this.dtp년도.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp년도.UseKeyF3 = false;
            this.dtp년도.Value = new System.DateTime(((long)(0)));
            // 
            // lbl년도
            // 
            this.lbl년도.Location = new System.Drawing.Point(0, 3);
            this.lbl년도.Name = "lbl년도";
            this.lbl년도.Resizeble = true;
            this.lbl년도.Size = new System.Drawing.Size(80, 16);
            this.lbl년도.TabIndex = 5;
            this.lbl년도.Text = "년도";
            this.lbl년도.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(759, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Location = new System.Drawing.Point(506, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(250, 23);
            this.bpPanelControl6.TabIndex = 2;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.cbo환종);
            this.bpPanelControl5.Controls.Add(this.lbl환종);
            this.bpPanelControl5.Location = new System.Drawing.Point(254, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(250, 23);
            this.bpPanelControl5.TabIndex = 1;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // cbo환종
            // 
            this.cbo환종.AutoDropDown = true;
            this.cbo환종.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo환종.Enabled = false;
            this.cbo환종.FormattingEnabled = true;
            this.cbo환종.ItemHeight = 12;
            this.cbo환종.Location = new System.Drawing.Point(81, 1);
            this.cbo환종.Name = "cbo환종";
            this.cbo환종.ShowCheckBox = false;
            this.cbo환종.Size = new System.Drawing.Size(167, 20);
            this.cbo환종.TabIndex = 8;
            this.cbo환종.UseKeyEnter = true;
            this.cbo환종.UseKeyF3 = true;
            // 
            // lbl환종
            // 
            this.lbl환종.Location = new System.Drawing.Point(0, 4);
            this.lbl환종.Name = "lbl환종";
            this.lbl환종.Resizeble = true;
            this.lbl환종.Size = new System.Drawing.Size(80, 16);
            this.lbl환종.TabIndex = 7;
            this.lbl환종.Text = "환종";
            this.lbl환종.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.bp거래처);
            this.bpPanelControl4.Controls.Add(this.lbl거래처);
            this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(250, 23);
            this.bpPanelControl4.TabIndex = 0;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // bp거래처
            // 
            this.bp거래처.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bp거래처.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp거래처.ButtonImage")));
            this.bp거래처.ChildMode = "";
            this.bp거래처.CodeName = "";
            this.bp거래처.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp거래처.CodeValue = "";
            this.bp거래처.ComboCheck = true;
            this.bp거래처.Enabled = false;
            this.bp거래처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bp거래처.IsCodeValueToUpper = true;
            this.bp거래처.ItemBackColor = System.Drawing.Color.White;
            this.bp거래처.Location = new System.Drawing.Point(81, 1);
            this.bp거래처.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp거래처.Name = "bp거래처";
            this.bp거래처.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp거래처.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bp거래처.SearchCode = true;
            this.bp거래처.SelectCount = 0;
            this.bp거래처.SetDefaultValue = false;
            this.bp거래처.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp거래처.Size = new System.Drawing.Size(167, 21);
            this.bp거래처.TabIndex = 7;
            this.bp거래처.TabStop = false;
            this.bp거래처.Text = "bpCodeTextBox1";
            // 
            // lbl거래처
            // 
            this.lbl거래처.Location = new System.Drawing.Point(1, 4);
            this.lbl거래처.Name = "lbl거래처";
            this.lbl거래처.Resizeble = true;
            this.lbl거래처.Size = new System.Drawing.Size(80, 16);
            this.lbl거래처.TabIndex = 6;
            this.lbl거래처.Text = "거래처";
            this.lbl거래처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelExt1
            // 
            this.panelExt1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt1.Controls.Add(this.btn닫기);
            this.panelExt1.Controls.Add(this.btn확인);
            this.panelExt1.Controls.Add(this.btn조회);
            this.panelExt1.Location = new System.Drawing.Point(3, 72);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(769, 24);
            this.panelExt1.TabIndex = 3;
            // 
            // btn닫기
            // 
            this.btn닫기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn닫기.BackColor = System.Drawing.Color.Transparent;
            this.btn닫기.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn닫기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn닫기.Location = new System.Drawing.Point(704, 2);
            this.btn닫기.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn닫기.Name = "btn닫기";
            this.btn닫기.Size = new System.Drawing.Size(62, 19);
            this.btn닫기.TabIndex = 2;
            this.btn닫기.TabStop = false;
            this.btn닫기.Text = "닫기";
            this.btn닫기.UseVisualStyleBackColor = true;
            // 
            // btn확인
            // 
            this.btn확인.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn확인.BackColor = System.Drawing.Color.Transparent;
            this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn확인.Location = new System.Drawing.Point(640, 2);
            this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn확인.Name = "btn확인";
            this.btn확인.Size = new System.Drawing.Size(62, 19);
            this.btn확인.TabIndex = 1;
            this.btn확인.TabStop = false;
            this.btn확인.Text = "확인";
            this.btn확인.UseVisualStyleBackColor = true;
            // 
            // btn조회
            // 
            this.btn조회.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn조회.BackColor = System.Drawing.Color.Transparent;
            this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn조회.Location = new System.Drawing.Point(576, 2);
            this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn조회.Name = "btn조회";
            this.btn조회.Size = new System.Drawing.Size(62, 19);
            this.btn조회.TabIndex = 0;
            this.btn조회.TabStop = false;
            this.btn조회.Text = "조회";
            this.btn조회.UseVisualStyleBackColor = true;
            // 
            // P_SA_Z_NICEGR_SA_PLAN_SUB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(785, 552);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_SA_Z_NICEGR_SA_PLAN_SUB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TitleText = "계획적용 도움창";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl3.PerformLayout();
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp년도)).EndInit();
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.panelExt1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.TextBoxExt txt차수;
        private Duzon.Common.Controls.LabelExt lbl차수;
        private Duzon.Common.BpControls.BpCodeTextBox bpWeekNo;
        private Duzon.Common.Controls.LabelExt lblweekno;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.RoundedButton btn확인;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Duzon.Common.Controls.RoundedButton btn닫기;
        private Duzon.Common.Controls.DatePicker dtp년도;
        private Duzon.Common.Controls.LabelExt lbl년도;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.Controls.DropDownComboBox cbo환종;
        private Duzon.Common.Controls.LabelExt lbl환종;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpCodeTextBox bp거래처;
        private Duzon.Common.Controls.LabelExt lbl거래처;
    }
}

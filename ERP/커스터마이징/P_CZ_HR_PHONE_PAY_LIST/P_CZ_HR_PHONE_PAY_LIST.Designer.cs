namespace cz
{
    partial class P_CZ_HR_PHONE_PAY_LIST
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_HR_PHONE_PAY_LIST));
			this.tlayM = new System.Windows.Forms.TableLayoutPanel();
			this.oneS = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbx사원 = new Duzon.Common.BpControls.BpComboBox();
			this.labelExt2 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp급여반영월 = new Duzon.Common.Controls.DatePicker();
			this.labelExt4 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.pnl문의번호 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbx부서 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl코스트센터 = new Duzon.Common.Controls.LabelExt();
			this.pnl거래처그룹 = new Duzon.Common.BpControls.BpPanelControl();
			this.cbo처리구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.lbl승인구분 = new Duzon.Common.Controls.LabelExt();
			this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this.tbx첨부파일 = new Duzon.Common.Controls.TextBoxExt();
			this.btn파일보기 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn자동승인 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.btn승인 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn승인취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn전표처리 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn전표이동 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn전표취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this.tlayM.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp급여반영월)).BeginInit();
			this.oneGridItem2.SuspendLayout();
			this.pnl문의번호.SuspendLayout();
			this.pnl거래처그룹.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tlayM);
			this.mDataArea.Size = new System.Drawing.Size(1243, 752);
			// 
			// tlayM
			// 
			this.tlayM.ColumnCount = 1;
			this.tlayM.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlayM.Controls.Add(this.oneS, 0, 0);
			this.tlayM.Controls.Add(this._flexH, 0, 1);
			this.tlayM.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlayM.Location = new System.Drawing.Point(0, 0);
			this.tlayM.Name = "tlayM";
			this.tlayM.RowCount = 2;
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlayM.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlayM.Size = new System.Drawing.Size(1243, 752);
			this.tlayM.TabIndex = 1;
			// 
			// oneS
			// 
			this.oneS.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneS.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneS.Location = new System.Drawing.Point(3, 3);
			this.oneS.Name = "oneS";
			this.oneS.Size = new System.Drawing.Size(1237, 63);
			this.oneS.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1227, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.cbx사원);
			this.bpPanelControl1.Controls.Add(this.labelExt2);
			this.bpPanelControl1.Location = new System.Drawing.Point(314, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(310, 23);
			this.bpPanelControl1.TabIndex = 19;
			this.bpPanelControl1.Text = "bpPanelControl6";
			// 
			// cbx사원
			// 
			this.cbx사원.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB1;
			this.cbx사원.Location = new System.Drawing.Point(84, 1);
			this.cbx사원.Name = "cbx사원";
			this.cbx사원.Size = new System.Drawing.Size(117, 21);
			this.cbx사원.TabIndex = 4;
			this.cbx사원.TabStop = false;
			this.cbx사원.Text = "bpComboBox1";
			// 
			// labelExt2
			// 
			this.labelExt2.Location = new System.Drawing.Point(17, 4);
			this.labelExt2.Name = "labelExt2";
			this.labelExt2.Size = new System.Drawing.Size(65, 16);
			this.labelExt2.TabIndex = 3;
			this.labelExt2.Text = "사원";
			this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.dtp급여반영월);
			this.bpPanelControl2.Controls.Add(this.labelExt4);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(310, 23);
			this.bpPanelControl2.TabIndex = 18;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// dtp급여반영월
			// 
			this.dtp급여반영월.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp급여반영월.Location = new System.Drawing.Point(84, 1);
			this.dtp급여반영월.Mask = "####/##";
			this.dtp급여반영월.MaskBackColor = System.Drawing.Color.White;
			this.dtp급여반영월.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp급여반영월.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp급여반영월.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp급여반영월.Modified = true;
			this.dtp급여반영월.Name = "dtp급여반영월";
			this.dtp급여반영월.ShowUpDown = true;
			this.dtp급여반영월.Size = new System.Drawing.Size(69, 21);
			this.dtp급여반영월.TabIndex = 3;
			this.dtp급여반영월.Tag = "DT_START";
			this.dtp급여반영월.Value = new System.DateTime(((long)(0)));
			// 
			// labelExt4
			// 
			this.labelExt4.Location = new System.Drawing.Point(17, 4);
			this.labelExt4.Name = "labelExt4";
			this.labelExt4.Size = new System.Drawing.Size(65, 16);
			this.labelExt4.TabIndex = 1;
			this.labelExt4.Text = "지급반영월";
			this.labelExt4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.pnl문의번호);
			this.oneGridItem2.Controls.Add(this.pnl거래처그룹);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1227, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// pnl문의번호
			// 
			this.pnl문의번호.Controls.Add(this.cbx부서);
			this.pnl문의번호.Controls.Add(this.lbl코스트센터);
			this.pnl문의번호.Location = new System.Drawing.Point(314, 1);
			this.pnl문의번호.Name = "pnl문의번호";
			this.pnl문의번호.Size = new System.Drawing.Size(310, 23);
			this.pnl문의번호.TabIndex = 12;
			this.pnl문의번호.Text = "bpPanelControl5";
			// 
			// cbx부서
			// 
			this.cbx부서.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_CC_SUB1;
			this.cbx부서.Location = new System.Drawing.Point(84, 1);
			this.cbx부서.Name = "cbx부서";
			this.cbx부서.Size = new System.Drawing.Size(117, 21);
			this.cbx부서.TabIndex = 5;
			this.cbx부서.TabStop = false;
			this.cbx부서.Text = "bpComboBox1";
			// 
			// lbl코스트센터
			// 
			this.lbl코스트센터.Location = new System.Drawing.Point(17, 4);
			this.lbl코스트센터.Name = "lbl코스트센터";
			this.lbl코스트센터.Size = new System.Drawing.Size(65, 16);
			this.lbl코스트센터.TabIndex = 1;
			this.lbl코스트센터.Text = "코스트센터";
			this.lbl코스트센터.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnl거래처그룹
			// 
			this.pnl거래처그룹.Controls.Add(this.cbo처리구분);
			this.pnl거래처그룹.Controls.Add(this.lbl승인구분);
			this.pnl거래처그룹.Location = new System.Drawing.Point(2, 1);
			this.pnl거래처그룹.Name = "pnl거래처그룹";
			this.pnl거래처그룹.Size = new System.Drawing.Size(310, 23);
			this.pnl거래처그룹.TabIndex = 8;
			this.pnl거래처그룹.Text = "bpPanelControl6";
			// 
			// cbo처리구분
			// 
			this.cbo처리구분.AutoDropDown = true;
			this.cbo처리구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo처리구분.FormattingEnabled = true;
			this.cbo처리구분.ItemHeight = 12;
			this.cbo처리구분.Location = new System.Drawing.Point(84, 1);
			this.cbo처리구분.Name = "cbo처리구분";
			this.cbo처리구분.Size = new System.Drawing.Size(117, 20);
			this.cbo처리구분.TabIndex = 6;
			this.cbo처리구분.Tag = "CD_PARTNER_GRP";
			// 
			// lbl승인구분
			// 
			this.lbl승인구분.Location = new System.Drawing.Point(17, 4);
			this.lbl승인구분.Name = "lbl승인구분";
			this.lbl승인구분.Size = new System.Drawing.Size(65, 16);
			this.lbl승인구분.TabIndex = 3;
			this.lbl승인구분.Text = "승인구분";
			this.lbl승인구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _flexH
			// 
			this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flexH.AutoResize = false;
			this._flexH.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexH.EnabledHeaderCheck = true;
			this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexH.Location = new System.Drawing.Point(3, 72);
			this._flexH.Name = "_flexH";
			this._flexH.Rows.Count = 1;
			this._flexH.Rows.DefaultSize = 20;
			this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexH.ShowSort = false;
			this._flexH.Size = new System.Drawing.Size(1237, 677);
			this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
			this._flexH.TabIndex = 1;
			this._flexH.UseGridCalculator = true;
			// 
			// tbx첨부파일
			// 
			this.tbx첨부파일.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbx첨부파일.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.tbx첨부파일.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbx첨부파일.Location = new System.Drawing.Point(3, 3);
			this.tbx첨부파일.Name = "tbx첨부파일";
			this.tbx첨부파일.ReadOnly = true;
			this.tbx첨부파일.Size = new System.Drawing.Size(242, 21);
			this.tbx첨부파일.TabIndex = 9;
			this.tbx첨부파일.TabStop = false;
			// 
			// btn파일보기
			// 
			this.btn파일보기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn파일보기.BackColor = System.Drawing.Color.White;
			this.btn파일보기.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn파일보기.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn파일보기.Location = new System.Drawing.Point(251, 3);
			this.btn파일보기.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn파일보기.Name = "btn파일보기";
			this.btn파일보기.Size = new System.Drawing.Size(70, 19);
			this.btn파일보기.TabIndex = 10;
			this.btn파일보기.TabStop = false;
			this.btn파일보기.Text = "파일열기";
			this.btn파일보기.UseVisualStyleBackColor = false;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Controls.Add(this.tbx첨부파일);
			this.flowLayoutPanel1.Controls.Add(this.btn파일보기);
			this.flowLayoutPanel1.Controls.Add(this.btn자동승인);
			this.flowLayoutPanel1.Controls.Add(this.splitter1);
			this.flowLayoutPanel1.Controls.Add(this.btn승인);
			this.flowLayoutPanel1.Controls.Add(this.btn승인취소);
			this.flowLayoutPanel1.Controls.Add(this.btn전표처리);
			this.flowLayoutPanel1.Controls.Add(this.btn전표이동);
			this.flowLayoutPanel1.Controls.Add(this.btn전표취소);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(444, 6);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(796, 27);
			this.flowLayoutPanel1.TabIndex = 13;
			// 
			// btn자동승인
			// 
			this.btn자동승인.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn자동승인.BackColor = System.Drawing.Color.White;
			this.btn자동승인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn자동승인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn자동승인.Location = new System.Drawing.Point(327, 3);
			this.btn자동승인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn자동승인.Name = "btn자동승인";
			this.btn자동승인.Size = new System.Drawing.Size(70, 19);
			this.btn자동승인.TabIndex = 17;
			this.btn자동승인.TabStop = false;
			this.btn자동승인.Text = "자동등록";
			this.btn자동승인.UseVisualStyleBackColor = false;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(403, 3);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(10, 24);
			this.splitter1.TabIndex = 15;
			this.splitter1.TabStop = false;
			// 
			// btn승인
			// 
			this.btn승인.BackColor = System.Drawing.Color.Transparent;
			this.btn승인.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn승인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn승인.Location = new System.Drawing.Point(419, 3);
			this.btn승인.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn승인.Name = "btn승인";
			this.btn승인.Size = new System.Drawing.Size(70, 19);
			this.btn승인.TabIndex = 11;
			this.btn승인.TabStop = false;
			this.btn승인.Text = "승인";
			this.btn승인.UseVisualStyleBackColor = false;
			// 
			// btn승인취소
			// 
			this.btn승인취소.BackColor = System.Drawing.Color.Transparent;
			this.btn승인취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn승인취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn승인취소.Location = new System.Drawing.Point(495, 3);
			this.btn승인취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn승인취소.Name = "btn승인취소";
			this.btn승인취소.Size = new System.Drawing.Size(70, 19);
			this.btn승인취소.TabIndex = 12;
			this.btn승인취소.TabStop = false;
			this.btn승인취소.Text = "승인취소";
			this.btn승인취소.UseVisualStyleBackColor = false;
			// 
			// btn전표처리
			// 
			this.btn전표처리.BackColor = System.Drawing.Color.Transparent;
			this.btn전표처리.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전표처리.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전표처리.Location = new System.Drawing.Point(571, 3);
			this.btn전표처리.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전표처리.Name = "btn전표처리";
			this.btn전표처리.Size = new System.Drawing.Size(70, 19);
			this.btn전표처리.TabIndex = 13;
			this.btn전표처리.TabStop = false;
			this.btn전표처리.Text = "전표처리";
			this.btn전표처리.UseVisualStyleBackColor = false;
			// 
			// btn전표이동
			// 
			this.btn전표이동.BackColor = System.Drawing.Color.Transparent;
			this.btn전표이동.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전표이동.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전표이동.Location = new System.Drawing.Point(647, 3);
			this.btn전표이동.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전표이동.Name = "btn전표이동";
			this.btn전표이동.Size = new System.Drawing.Size(70, 19);
			this.btn전표이동.TabIndex = 16;
			this.btn전표이동.TabStop = false;
			this.btn전표이동.Text = "전표이동";
			this.btn전표이동.UseVisualStyleBackColor = false;
			// 
			// btn전표취소
			// 
			this.btn전표취소.BackColor = System.Drawing.Color.Transparent;
			this.btn전표취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn전표취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn전표취소.Location = new System.Drawing.Point(723, 3);
			this.btn전표취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn전표취소.Name = "btn전표취소";
			this.btn전표취소.Size = new System.Drawing.Size(70, 19);
			this.btn전표취소.TabIndex = 14;
			this.btn전표취소.TabStop = false;
			this.btn전표취소.Text = "전표취소";
			this.btn전표취소.UseVisualStyleBackColor = false;
			// 
			// P_CZ_HR_PHONE_PAY_LIST
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "P_CZ_HR_PHONE_PAY_LIST";
			this.Size = new System.Drawing.Size(1243, 792);
			this.TitleText = "휴대폰사용등록현황";
			this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.mDataArea.ResumeLayout(false);
			this.tlayM.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp급여반영월)).EndInit();
			this.oneGridItem2.ResumeLayout(false);
			this.pnl문의번호.ResumeLayout(false);
			this.pnl거래처그룹.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlayM;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneS;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.BpControls.BpComboBox cbx사원;
        private Duzon.Common.Controls.LabelExt labelExt2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.DatePicker dtp급여반영월;
        private Duzon.Common.Controls.LabelExt labelExt4;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl pnl문의번호;
        private Duzon.Common.BpControls.BpComboBox cbx부서;
        private Duzon.Common.Controls.LabelExt lbl코스트센터;
        private Duzon.Common.BpControls.BpPanelControl pnl거래처그룹;
        private Duzon.Common.Controls.DropDownComboBox cbo처리구분;
        private Duzon.Common.Controls.LabelExt lbl승인구분;
        private Duzon.Common.Controls.TextBoxExt tbx첨부파일;
        private Duzon.Common.Controls.RoundedButton btn파일보기;
        private Dass.FlexGrid.FlexGrid _flexH;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Splitter splitter1;
        private Duzon.Common.Controls.RoundedButton btn승인;
        private Duzon.Common.Controls.RoundedButton btn승인취소;
        private Duzon.Common.Controls.RoundedButton btn전표처리;
        private Duzon.Common.Controls.RoundedButton btn전표취소;
        private Duzon.Common.Controls.RoundedButton btn전표이동;
        private Duzon.Common.Controls.RoundedButton btn자동승인;
    }
}
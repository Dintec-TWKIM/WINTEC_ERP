namespace cz
{
    partial class P_CZ_UNLOADINGRPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_UNLOADINGRPT));
            this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_line = new Duzon.Common.Controls.PanelExt();
            this.panelEx2 = new Duzon.Common.Controls.PanelEx();
            this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn추가 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.조회조건panel = new Duzon.Common.Controls.PanelExt();
            this.ctx담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.ctx거래처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.dtp종료일 = new Duzon.Common.Controls.DatePicker();
            this.dtp시작일 = new Duzon.Common.Controls.DatePicker();
            this.구분선1 = new Duzon.Common.Controls.PanelExt();
            this.panelExt2 = new Duzon.Common.Controls.PanelExt();
            this.우panel = new Duzon.Common.Controls.PanelExt();
            this.labelExt2 = new Duzon.Common.Controls.LabelExt();
            this.중panel = new Duzon.Common.Controls.PanelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.좌panel = new Duzon.Common.Controls.PanelExt();
            this.lblTitle01 = new Duzon.Common.Controls.LabelExt();
            this.panelEx1 = new Duzon.Common.Controls.PanelEx();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this._flexL2 = new Dass.FlexGrid.FlexGrid(this.components);
            this.panelExt3 = new Duzon.Common.Controls.PanelExt();
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.btn02 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn01 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this._tlayMain.SuspendLayout();
            this.pnl_line.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.조회조건panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp종료일)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp시작일)).BeginInit();
            this.구분선1.SuspendLayout();
            this.우panel.SuspendLayout();
            this.중panel.SuspendLayout();
            this.좌panel.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            this.panelExt1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexL2)).BeginInit();
            this.panelExt3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this._tlayMain);
            // 
            // _tlayMain
            // 
            this._tlayMain.ColumnCount = 1;
            this._tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlayMain.Controls.Add(this.pnl_line, 0, 2);
            this._tlayMain.Controls.Add(this.조회조건panel, 0, 0);
            this._tlayMain.Controls.Add(this.panelEx1, 0, 1);
            this._tlayMain.Controls.Add(this.panelExt1, 0, 3);
            this._tlayMain.Controls.Add(this.panelExt3, 0, 4);
            this._tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tlayMain.Location = new System.Drawing.Point(0, 0);
            this._tlayMain.Name = "_tlayMain";
            this._tlayMain.RowCount = 5;
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.46154F));
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.07692F));
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.46154F));
            this._tlayMain.Size = new System.Drawing.Size(827, 579);
            this._tlayMain.TabIndex = 10;
            // 
            // pnl_line
            // 
            this.pnl_line.Controls.Add(this.panelEx2);
            this.pnl_line.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_line.Location = new System.Drawing.Point(3, 238);
            this.pnl_line.Name = "pnl_line";
            this.pnl_line.Size = new System.Drawing.Size(821, 24);
            this.pnl_line.TabIndex = 12;
            // 
            // panelEx2
            // 
            this.panelEx2.ColorA = System.Drawing.Color.Empty;
            this.panelEx2.ColorB = System.Drawing.Color.Empty;
            this.panelEx2.Controls.Add(this.btn삭제);
            this.panelEx2.Controls.Add(this.btn추가);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(821, 24);
            this.panelEx2.TabIndex = 0;
            // 
            // btn삭제
            // 
            this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn삭제.BackColor = System.Drawing.Color.White;
            this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn삭제.Location = new System.Drawing.Point(736, 3);
            this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn삭제.Name = "btn삭제";
            this.btn삭제.Size = new System.Drawing.Size(80, 19);
            this.btn삭제.TabIndex = 7;
            this.btn삭제.TabStop = false;
            this.btn삭제.Tag = "";
            this.btn삭제.Text = "삭제";
            this.btn삭제.UseVisualStyleBackColor = false;
            // 
            // btn추가
            // 
            this.btn추가.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn추가.BackColor = System.Drawing.Color.White;
            this.btn추가.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn추가.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn추가.Location = new System.Drawing.Point(650, 3);
            this.btn추가.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn추가.Name = "btn추가";
            this.btn추가.Size = new System.Drawing.Size(80, 19);
            this.btn추가.TabIndex = 6;
            this.btn추가.TabStop = false;
            this.btn추가.Tag = "";
            this.btn추가.Text = "추가";
            this.btn추가.UseVisualStyleBackColor = false;
            // 
            // 조회조건panel
            // 
            this.조회조건panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.조회조건panel.Controls.Add(this.ctx담당자);
            this.조회조건panel.Controls.Add(this.ctx거래처);
            this.조회조건panel.Controls.Add(this.dtp종료일);
            this.조회조건panel.Controls.Add(this.dtp시작일);
            this.조회조건panel.Controls.Add(this.구분선1);
            this.조회조건panel.Controls.Add(this.우panel);
            this.조회조건panel.Controls.Add(this.중panel);
            this.조회조건panel.Controls.Add(this.좌panel);
            this.조회조건panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.조회조건panel.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.조회조건panel.Location = new System.Drawing.Point(3, 3);
            this.조회조건panel.Name = "조회조건panel";
            this.조회조건panel.Size = new System.Drawing.Size(821, 34);
            this.조회조건panel.TabIndex = 0;
            // 
            // ctx담당자
            // 
            this.ctx담당자.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx담당자.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx담당자.ButtonImage")));
            this.ctx담당자.ChildMode = "";
            this.ctx담당자.CodeName = "";
            this.ctx담당자.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx담당자.CodeValue = "";
            this.ctx담당자.ComboCheck = true;
            this.ctx담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.ctx담당자.IsCodeValueToUpper = true;
            this.ctx담당자.ItemBackColor = System.Drawing.Color.White;
            this.ctx담당자.Location = new System.Drawing.Point(710, 3);
            this.ctx담당자.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx담당자.Name = "ctx담당자";
            this.ctx담당자.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx담당자.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.ctx담당자.SearchCode = true;
            this.ctx담당자.SelectCount = 0;
            this.ctx담당자.SetDefaultValue = false;
            this.ctx담당자.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx담당자.Size = new System.Drawing.Size(189, 21);
            this.ctx담당자.TabIndex = 166;
            this.ctx담당자.TabStop = false;
            this.ctx담당자.Text = "bpCodeTextBox1";
            // 
            // ctx거래처
            // 
            this.ctx거래처.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx거래처.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx거래처.ButtonImage")));
            this.ctx거래처.ChildMode = "";
            this.ctx거래처.CodeName = "";
            this.ctx거래처.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx거래처.CodeValue = "";
            this.ctx거래처.ComboCheck = true;
            this.ctx거래처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx거래처.IsCodeValueToUpper = true;
            this.ctx거래처.ItemBackColor = System.Drawing.Color.White;
            this.ctx거래처.Location = new System.Drawing.Point(409, 2);
            this.ctx거래처.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx거래처.Name = "ctx거래처";
            this.ctx거래처.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx거래처.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.ctx거래처.SearchCode = true;
            this.ctx거래처.SelectCount = 0;
            this.ctx거래처.SetDefaultValue = false;
            this.ctx거래처.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx거래처.Size = new System.Drawing.Size(189, 21);
            this.ctx거래처.TabIndex = 163;
            this.ctx거래처.TabStop = false;
            this.ctx거래처.Text = "bpCodeTextBox1";
            // 
            // dtp종료일
            // 
            this.dtp종료일.CalendarBackColor = System.Drawing.Color.White;
            this.dtp종료일.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp종료일.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp종료일.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp종료일.Location = new System.Drawing.Point(210, 3);
            this.dtp종료일.Mask = "####/##/##";
            this.dtp종료일.MaskBackColor = System.Drawing.SystemColors.Window;
            this.dtp종료일.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp종료일.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp종료일.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp종료일.Modified = true;
            this.dtp종료일.Name = "dtp종료일";
            this.dtp종료일.NullCheck = false;
            this.dtp종료일.PaddingCharacter = '_';
            this.dtp종료일.PassivePromptCharacter = '_';
            this.dtp종료일.PromptCharacter = '_';
            this.dtp종료일.SelectedDayColor = System.Drawing.Color.White;
            this.dtp종료일.ShowToDay = true;
            this.dtp종료일.ShowTodayCircle = true;
            this.dtp종료일.ShowUpDown = false;
            this.dtp종료일.Size = new System.Drawing.Size(90, 21);
            this.dtp종료일.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp종료일.TabIndex = 161;
            this.dtp종료일.TitleBackColor = System.Drawing.Color.White;
            this.dtp종료일.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp종료일.ToDayColor = System.Drawing.Color.Red;
            this.dtp종료일.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp종료일.UseKeyF3 = false;
            this.dtp종료일.Value = new System.DateTime(((long)(0)));
            // 
            // dtp시작일
            // 
            this.dtp시작일.CalendarBackColor = System.Drawing.Color.White;
            this.dtp시작일.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp시작일.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp시작일.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp시작일.Location = new System.Drawing.Point(105, 3);
            this.dtp시작일.Mask = "####/##/##";
            this.dtp시작일.MaskBackColor = System.Drawing.SystemColors.Window;
            this.dtp시작일.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp시작일.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp시작일.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp시작일.Modified = true;
            this.dtp시작일.Name = "dtp시작일";
            this.dtp시작일.NullCheck = false;
            this.dtp시작일.PaddingCharacter = '_';
            this.dtp시작일.PassivePromptCharacter = '_';
            this.dtp시작일.PromptCharacter = '_';
            this.dtp시작일.SelectedDayColor = System.Drawing.Color.White;
            this.dtp시작일.ShowToDay = true;
            this.dtp시작일.ShowTodayCircle = true;
            this.dtp시작일.ShowUpDown = false;
            this.dtp시작일.Size = new System.Drawing.Size(90, 21);
            this.dtp시작일.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp시작일.TabIndex = 160;
            this.dtp시작일.TitleBackColor = System.Drawing.Color.White;
            this.dtp시작일.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp시작일.ToDayColor = System.Drawing.Color.Red;
            this.dtp시작일.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp시작일.UseKeyF3 = false;
            this.dtp시작일.Value = new System.DateTime(((long)(0)));
            // 
            // 구분선1
            // 
            this.구분선1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.구분선1.BackColor = System.Drawing.Color.Transparent;
            this.구분선1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("구분선1.BackgroundImage")));
            this.구분선1.Controls.Add(this.panelExt2);
            this.구분선1.Location = new System.Drawing.Point(6, 26);
            this.구분선1.Name = "구분선1";
            this.구분선1.Size = new System.Drawing.Size(811, 1);
            this.구분선1.TabIndex = 159;
            // 
            // panelExt2
            // 
            this.panelExt2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt2.BackColor = System.Drawing.Color.Transparent;
            this.panelExt2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt2.BackgroundImage")));
            this.panelExt2.Location = new System.Drawing.Point(0, 29);
            this.panelExt2.Name = "panelExt2";
            this.panelExt2.Size = new System.Drawing.Size(811, 1);
            this.panelExt2.TabIndex = 160;
            // 
            // 우panel
            // 
            this.우panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.우panel.Controls.Add(this.labelExt2);
            this.우panel.Location = new System.Drawing.Point(606, -1);
            this.우panel.Name = "우panel";
            this.우panel.Size = new System.Drawing.Size(100, 85);
            this.우panel.TabIndex = 157;
            // 
            // labelExt2
            // 
            this.labelExt2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt2.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelExt2.Location = new System.Drawing.Point(3, 4);
            this.labelExt2.Name = "labelExt2";
            this.labelExt2.Resizeble = true;
            this.labelExt2.Size = new System.Drawing.Size(95, 18);
            this.labelExt2.TabIndex = 157;
            this.labelExt2.Tag = "";
            this.labelExt2.Text = "담당자";
            this.labelExt2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // 중panel
            // 
            this.중panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.중panel.Controls.Add(this.labelExt1);
            this.중panel.Location = new System.Drawing.Point(303, 1);
            this.중panel.Name = "중panel";
            this.중panel.Size = new System.Drawing.Size(100, 83);
            this.중panel.TabIndex = 11;
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.Transparent;
            this.labelExt1.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelExt1.ForeColor = System.Drawing.Color.Black;
            this.labelExt1.Location = new System.Drawing.Point(3, 4);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(95, 18);
            this.labelExt1.TabIndex = 1;
            this.labelExt1.Tag = "";
            this.labelExt1.Text = "거래처";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // 좌panel
            // 
            this.좌panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.좌panel.Controls.Add(this.lblTitle01);
            this.좌panel.Location = new System.Drawing.Point(1, 1);
            this.좌panel.Name = "좌panel";
            this.좌panel.Size = new System.Drawing.Size(100, 83);
            this.좌panel.TabIndex = 10;
            // 
            // lblTitle01
            // 
            this.lblTitle01.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle01.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle01.ForeColor = System.Drawing.Color.Black;
            this.lblTitle01.Location = new System.Drawing.Point(3, 4);
            this.lblTitle01.Name = "lblTitle01";
            this.lblTitle01.Resizeble = true;
            this.lblTitle01.Size = new System.Drawing.Size(95, 18);
            this.lblTitle01.TabIndex = 0;
            this.lblTitle01.Tag = "";
            this.lblTitle01.Text = "선적일자";
            this.lblTitle01.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelEx1
            // 
            this.panelEx1.ColorA = System.Drawing.Color.Empty;
            this.panelEx1.ColorB = System.Drawing.Color.Empty;
            this.panelEx1.Controls.Add(this._flexH);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(3, 43);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(821, 189);
            this.panelEx1.TabIndex = 13;
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
            this._flexH.Location = new System.Drawing.Point(0, 0);
            this._flexH.Name = "_flexH";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 20;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(821, 189);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 2;
            this._flexH.UseGridCalculator = true;
            // 
            // panelExt1
            // 
            this.panelExt1.Controls.Add(this._flexL2);
            this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt1.Location = new System.Drawing.Point(3, 268);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(821, 111);
            this.panelExt1.TabIndex = 14;
            // 
            // _flexL2
            // 
            this._flexL2.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexL2.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexL2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexL2.AutoResize = false;
            this._flexL2.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexL2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexL2.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexL2.EnabledHeaderCheck = true;
            this._flexL2.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexL2.Location = new System.Drawing.Point(0, 0);
            this._flexL2.Name = "_flexL2";
            this._flexL2.Rows.Count = 1;
            this._flexL2.Rows.DefaultSize = 20;
            this._flexL2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexL2.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexL2.ShowSort = false;
            this._flexL2.Size = new System.Drawing.Size(821, 111);
            this._flexL2.StyleInfo = resources.GetString("_flexL2.StyleInfo");
            this._flexL2.TabIndex = 2;
            this._flexL2.UseGridCalculator = true;
            // 
            // panelExt3
            // 
            this.panelExt3.Controls.Add(this._flexL);
            this.panelExt3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt3.Location = new System.Drawing.Point(3, 385);
            this.panelExt3.Name = "panelExt3";
            this.panelExt3.Size = new System.Drawing.Size(821, 191);
            this.panelExt3.TabIndex = 15;
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
            this._flexL.Size = new System.Drawing.Size(821, 191);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 3;
            this._flexL.UseGridCalculator = true;
            // 
            // btn02
            // 
            this.btn02.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn02.BackColor = System.Drawing.Color.White;
            this.btn02.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn02.Location = new System.Drawing.Point(719, 10);
            this.btn02.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn02.Name = "btn02";
            this.btn02.Size = new System.Drawing.Size(80, 19);
            this.btn02.TabIndex = 3;
            this.btn02.TabStop = false;
            this.btn02.Tag = "적용";
            this.btn02.Text = "btn02";
            this.btn02.UseVisualStyleBackColor = false;
            // 
            // btn01
            // 
            this.btn01.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn01.BackColor = System.Drawing.Color.White;
            this.btn01.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn01.Location = new System.Drawing.Point(638, 10);
            this.btn01.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn01.Name = "btn01";
            this.btn01.Size = new System.Drawing.Size(80, 19);
            this.btn01.TabIndex = 4;
            this.btn01.TabStop = false;
            this.btn01.Tag = "적용";
            this.btn01.Text = "btn01";
            this.btn01.UseVisualStyleBackColor = false;
            // 
            // P_CZ_UNLOADINGRPT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btn01);
            this.Controls.Add(this.btn02);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_CZ_UNLOADINGRPT";
            this.Controls.SetChildIndex(this.btn02, 0);
            this.Controls.SetChildIndex(this.btn01, 0);
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.mDataArea.ResumeLayout(false);
            this._tlayMain.ResumeLayout(false);
            this.pnl_line.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.조회조건panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp종료일)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp시작일)).EndInit();
            this.구분선1.ResumeLayout(false);
            this.우panel.ResumeLayout(false);
            this.중panel.ResumeLayout(false);
            this.좌panel.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            this.panelExt1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexL2)).EndInit();
            this.panelExt3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _tlayMain;
        private Duzon.Common.Controls.RoundedButton btn02;
        private Duzon.Common.Controls.PanelExt 조회조건panel;
        private Duzon.Common.Controls.PanelExt 중panel;
        private Duzon.Common.Controls.PanelExt 좌panel;
        private Duzon.Common.Controls.LabelExt lblTitle01;
        private Duzon.Common.Controls.RoundedButton btn01;
        private Duzon.Common.Controls.PanelExt 우panel;
        private Duzon.Common.Controls.PanelExt 구분선1;
        private Duzon.Common.Controls.DatePicker dtp종료일;
        private Duzon.Common.Controls.DatePicker dtp시작일;
        private Duzon.Common.Controls.PanelExt panelExt2;
        private Duzon.Common.BpControls.BpCodeTextBox ctx거래처;
        private Duzon.Common.Controls.LabelExt labelExt2;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.BpControls.BpCodeTextBox ctx담당자;
        private Duzon.Common.Controls.PanelExt pnl_line;
        private Duzon.Common.Controls.PanelEx panelEx1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Duzon.Common.Controls.PanelEx panelEx2;
        private Duzon.Common.Controls.RoundedButton btn삭제;
        private Duzon.Common.Controls.RoundedButton btn추가;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Dass.FlexGrid.FlexGrid _flexL2;
        private Duzon.Common.Controls.PanelExt panelExt3;
        private Dass.FlexGrid.FlexGrid _flexL;
    }
}
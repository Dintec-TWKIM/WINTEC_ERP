namespace cz
{
    partial class P_CZ_PU_IV_REG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PU_IV_REG));
			this._tlay_Main = new System.Windows.Forms.TableLayoutPanel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl처리일자 = new Duzon.Common.Controls.LabelExt();
			this.dtp처리일자 = new Duzon.Common.Controls.DatePicker();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl담당자 = new Duzon.Common.Controls.LabelExt();
			this.ctx담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl사업장 = new Duzon.Common.Controls.LabelExt();
			this.ctx사업장 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl계산서처리 = new Duzon.Common.Controls.LabelExt();
			this.rbtn계산서처리건별 = new Duzon.Common.Controls.RadioButtonExt();
			this.rbtn계산서처리일괄 = new Duzon.Common.Controls.RadioButtonExt();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl거래구분 = new Duzon.Common.Controls.LabelExt();
			this.cbo거래구분 = new Duzon.Common.Controls.DropDownComboBox();
			this.bpc부가세사업장변경 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl부가세사업장 = new Duzon.Common.Controls.LabelExt();
			this.ctx부가세사업장 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.btn부가세사업장변경 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.oneGridItem3 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpc매입형태변경 = new Duzon.Common.BpControls.BpPanelControl();
			this.lbl매입형태 = new Duzon.Common.Controls.LabelExt();
			this.btn매입형태변경 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.cbo매입형태 = new Duzon.Common.Controls.DropDownComboBox();
			this.btn매입관리 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn입고적용 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this._tlay_Main.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtp처리일자)).BeginInit();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rbtn계산서처리건별)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rbtn계산서처리일괄)).BeginInit();
			this.bpPanelControl5.SuspendLayout();
			this.bpc부가세사업장변경.SuspendLayout();
			this.oneGridItem3.SuspendLayout();
			this.bpc매입형태변경.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this._tlay_Main);
			this.mDataArea.Size = new System.Drawing.Size(1090, 756);
			// 
			// _tlay_Main
			// 
			this._tlay_Main.AutoSize = true;
			this._tlay_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this._tlay_Main.ColumnCount = 1;
			this._tlay_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this._tlay_Main.Controls.Add(this.splitContainer1, 0, 1);
			this._tlay_Main.Controls.Add(this.oneGrid1, 0, 0);
			this._tlay_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this._tlay_Main.Location = new System.Drawing.Point(0, 0);
			this._tlay_Main.Name = "_tlay_Main";
			this._tlay_Main.RowCount = 2;
			this._tlay_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tlay_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this._tlay_Main.Size = new System.Drawing.Size(1090, 756);
			this._tlay_Main.TabIndex = 135;
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
			this.splitContainer1.Size = new System.Drawing.Size(1084, 659);
			this.splitContainer1.SplitterDistance = 138;
			this.splitContainer1.TabIndex = 153;
			// 
			// _flexH
			// 
			this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexH.AllowNumbering = false;
			this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
			this._flexH.AutoResize = false;
			this._flexH.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexH.EnabledHeaderCheck = true;
			this._flexH.Font = new System.Drawing.Font("굴림", 9F);
			this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexH.Location = new System.Drawing.Point(0, 0);
			this._flexH.Name = "_flexH";
			this._flexH.Rows.Count = 1;
			this._flexH.Rows.DefaultSize = 18;
			this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexH.ShowSort = false;
			this._flexH.Size = new System.Drawing.Size(1084, 138);
			this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
			this._flexH.TabIndex = 136;
			this._flexH.UseGridCalculator = true;
			// 
			// _flexL
			// 
			this._flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
			this._flexL.AutoResize = false;
			this._flexL.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flexL.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flexL.EnabledHeaderCheck = true;
			this._flexL.Font = new System.Drawing.Font("굴림", 9F);
			this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexL.Location = new System.Drawing.Point(0, 0);
			this._flexL.Name = "_flexL";
			this._flexL.Rows.Count = 1;
			this._flexL.Rows.DefaultSize = 18;
			this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexL.ShowSort = false;
			this._flexL.Size = new System.Drawing.Size(1084, 517);
			this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
			this._flexL.TabIndex = 155;
			this._flexL.UseGridCalculator = true;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2,
            this.oneGridItem3});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1084, 85);
			this.oneGrid1.TabIndex = 154;
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
			this.oneGridItem1.Size = new System.Drawing.Size(1074, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.lbl처리일자);
			this.bpPanelControl3.Controls.Add(this.dtp처리일자);
			this.bpPanelControl3.Location = new System.Drawing.Point(592, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(219, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// lbl처리일자
			// 
			this.lbl처리일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl처리일자.Font = new System.Drawing.Font("굴림", 9F);
			this.lbl처리일자.Location = new System.Drawing.Point(0, 0);
			this.lbl처리일자.Name = "lbl처리일자";
			this.lbl처리일자.Size = new System.Drawing.Size(100, 23);
			this.lbl처리일자.TabIndex = 135;
			this.lbl처리일자.Tag = "처리일자";
			this.lbl처리일자.Text = "처리일자";
			this.lbl처리일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtp처리일자
			// 
			this.dtp처리일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp처리일자.Location = new System.Drawing.Point(106, 1);
			this.dtp처리일자.Mask = "####/##/##";
			this.dtp처리일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.dtp처리일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
			this.dtp처리일자.MaximumSize = new System.Drawing.Size(0, 21);
			this.dtp처리일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
			this.dtp처리일자.Name = "dtp처리일자";
			this.dtp처리일자.Size = new System.Drawing.Size(90, 21);
			this.dtp처리일자.TabIndex = 3;
			this.dtp처리일자.Value = new System.DateTime(((long)(0)));
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.lbl담당자);
			this.bpPanelControl2.Controls.Add(this.ctx담당자);
			this.bpPanelControl2.Location = new System.Drawing.Point(297, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// lbl담당자
			// 
			this.lbl담당자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl담당자.Font = new System.Drawing.Font("굴림", 9F);
			this.lbl담당자.Location = new System.Drawing.Point(0, 0);
			this.lbl담당자.Name = "lbl담당자";
			this.lbl담당자.Size = new System.Drawing.Size(100, 23);
			this.lbl담당자.TabIndex = 135;
			this.lbl담당자.Tag = "담당자";
			this.lbl담당자.Text = "담당자";
			this.lbl담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx담당자
			// 
			this.ctx담당자.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.ctx담당자.Location = new System.Drawing.Point(106, 0);
			this.ctx담당자.Name = "ctx담당자";
			this.ctx담당자.Size = new System.Drawing.Size(187, 21);
			this.ctx담당자.TabIndex = 140;
			this.ctx담당자.TabStop = false;
			this.ctx담당자.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.lbl사업장);
			this.bpPanelControl1.Controls.Add(this.ctx사업장);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// lbl사업장
			// 
			this.lbl사업장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl사업장.Font = new System.Drawing.Font("굴림", 9F);
			this.lbl사업장.Location = new System.Drawing.Point(0, 0);
			this.lbl사업장.Name = "lbl사업장";
			this.lbl사업장.Size = new System.Drawing.Size(100, 23);
			this.lbl사업장.TabIndex = 132;
			this.lbl사업장.Tag = "사업장";
			this.lbl사업장.Text = "사업장";
			this.lbl사업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx사업장
			// 
			this.ctx사업장.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx사업장.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BIZAREA_SUB;
			this.ctx사업장.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.ctx사업장.Location = new System.Drawing.Point(106, 0);
			this.ctx사업장.Name = "ctx사업장";
			this.ctx사업장.Size = new System.Drawing.Size(187, 21);
			this.ctx사업장.TabIndex = 148;
			this.ctx사업장.TabStop = false;
			this.ctx사업장.QueryAfter += new Duzon.Common.BpControls.BpQueryHandler(this.Control_QueryAfter);
			// 
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl4);
			this.oneGridItem2.Controls.Add(this.bpPanelControl5);
			this.oneGridItem2.Controls.Add(this.bpc부가세사업장변경);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1074, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.lbl계산서처리);
			this.bpPanelControl4.Controls.Add(this.rbtn계산서처리건별);
			this.bpPanelControl4.Controls.Add(this.rbtn계산서처리일괄);
			this.bpPanelControl4.Location = new System.Drawing.Point(592, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(219, 23);
			this.bpPanelControl4.TabIndex = 5;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// lbl계산서처리
			// 
			this.lbl계산서처리.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl계산서처리.Location = new System.Drawing.Point(0, 0);
			this.lbl계산서처리.Name = "lbl계산서처리";
			this.lbl계산서처리.Size = new System.Drawing.Size(100, 23);
			this.lbl계산서처리.TabIndex = 2;
			this.lbl계산서처리.Text = "계산서처리";
			this.lbl계산서처리.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// rbtn계산서처리건별
			// 
			this.rbtn계산서처리건별.Font = new System.Drawing.Font("굴림", 9F);
			this.rbtn계산서처리건별.Location = new System.Drawing.Point(153, 0);
			this.rbtn계산서처리건별.Name = "rbtn계산서처리건별";
			this.rbtn계산서처리건별.Size = new System.Drawing.Size(49, 18);
			this.rbtn계산서처리건별.TabIndex = 1;
			this.rbtn계산서처리건별.TabStop = true;
			this.rbtn계산서처리건별.Tag = "건별";
			this.rbtn계산서처리건별.Text = "건별";
			this.rbtn계산서처리건별.TextDD = null;
			this.rbtn계산서처리건별.UseKeyEnter = true;
			// 
			// rbtn계산서처리일괄
			// 
			this.rbtn계산서처리일괄.Checked = true;
			this.rbtn계산서처리일괄.Font = new System.Drawing.Font("굴림", 9F);
			this.rbtn계산서처리일괄.Location = new System.Drawing.Point(106, 0);
			this.rbtn계산서처리일괄.Name = "rbtn계산서처리일괄";
			this.rbtn계산서처리일괄.Size = new System.Drawing.Size(49, 18);
			this.rbtn계산서처리일괄.TabIndex = 0;
			this.rbtn계산서처리일괄.TabStop = true;
			this.rbtn계산서처리일괄.Tag = "일괄";
			this.rbtn계산서처리일괄.Text = "일괄";
			this.rbtn계산서처리일괄.TextDD = null;
			this.rbtn계산서처리일괄.UseKeyEnter = true;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.lbl거래구분);
			this.bpPanelControl5.Controls.Add(this.cbo거래구분);
			this.bpPanelControl5.Location = new System.Drawing.Point(297, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(293, 23);
			this.bpPanelControl5.TabIndex = 4;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// lbl거래구분
			// 
			this.lbl거래구분.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl거래구분.Font = new System.Drawing.Font("굴림", 9F);
			this.lbl거래구분.Location = new System.Drawing.Point(0, 0);
			this.lbl거래구분.Name = "lbl거래구분";
			this.lbl거래구분.Size = new System.Drawing.Size(100, 23);
			this.lbl거래구분.TabIndex = 134;
			this.lbl거래구분.Tag = "거래구분";
			this.lbl거래구분.Text = "거래구분";
			this.lbl거래구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbo거래구분
			// 
			this.cbo거래구분.AutoDropDown = true;
			this.cbo거래구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.cbo거래구분.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbo거래구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo거래구분.Font = new System.Drawing.Font("굴림", 9F);
			this.cbo거래구분.ItemHeight = 12;
			this.cbo거래구분.Location = new System.Drawing.Point(106, 0);
			this.cbo거래구분.Name = "cbo거래구분";
			this.cbo거래구분.Size = new System.Drawing.Size(187, 20);
			this.cbo거래구분.TabIndex = 2;
			this.cbo거래구분.Tag = "FG_TRANS";
			this.cbo거래구분.UseKeyEnter = false;
			this.cbo거래구분.UseKeyF3 = false;
			// 
			// bpc부가세사업장변경
			// 
			this.bpc부가세사업장변경.Controls.Add(this.lbl부가세사업장);
			this.bpc부가세사업장변경.Controls.Add(this.ctx부가세사업장);
			this.bpc부가세사업장변경.Controls.Add(this.btn부가세사업장변경);
			this.bpc부가세사업장변경.Location = new System.Drawing.Point(2, 1);
			this.bpc부가세사업장변경.Name = "bpc부가세사업장변경";
			this.bpc부가세사업장변경.Size = new System.Drawing.Size(293, 23);
			this.bpc부가세사업장변경.TabIndex = 3;
			this.bpc부가세사업장변경.Text = "bpPanelControl6";
			// 
			// lbl부가세사업장
			// 
			this.lbl부가세사업장.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl부가세사업장.Font = new System.Drawing.Font("굴림", 9F);
			this.lbl부가세사업장.Location = new System.Drawing.Point(0, 0);
			this.lbl부가세사업장.Name = "lbl부가세사업장";
			this.lbl부가세사업장.Size = new System.Drawing.Size(100, 23);
			this.lbl부가세사업장.TabIndex = 133;
			this.lbl부가세사업장.Tag = "부가세사업장";
			this.lbl부가세사업장.Text = "부가세사업장";
			this.lbl부가세사업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ctx부가세사업장
			// 
			this.ctx부가세사업장.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BIZAREA_SUB;
			this.ctx부가세사업장.Location = new System.Drawing.Point(106, 1);
			this.ctx부가세사업장.Name = "ctx부가세사업장";
			this.ctx부가세사업장.Size = new System.Drawing.Size(138, 21);
			this.ctx부가세사업장.TabIndex = 149;
			this.ctx부가세사업장.TabStop = false;
			// 
			// btn부가세사업장변경
			// 
			this.btn부가세사업장변경.BackColor = System.Drawing.Color.White;
			this.btn부가세사업장변경.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn부가세사업장변경.Dock = System.Windows.Forms.DockStyle.Right;
			this.btn부가세사업장변경.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn부가세사업장변경.Font = new System.Drawing.Font("굴림", 9F);
			this.btn부가세사업장변경.Location = new System.Drawing.Point(250, 0);
			this.btn부가세사업장변경.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn부가세사업장변경.Name = "btn부가세사업장변경";
			this.btn부가세사업장변경.Size = new System.Drawing.Size(43, 19);
			this.btn부가세사업장변경.TabIndex = 145;
			this.btn부가세사업장변경.TabStop = false;
			this.btn부가세사업장변경.Tag = " 변경";
			this.btn부가세사업장변경.Text = " 변경";
			this.btn부가세사업장변경.UseVisualStyleBackColor = false;
			// 
			// oneGridItem3
			// 
			this.oneGridItem3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem3.Controls.Add(this.bpc매입형태변경);
			this.oneGridItem3.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem3.Location = new System.Drawing.Point(0, 46);
			this.oneGridItem3.Name = "oneGridItem3";
			this.oneGridItem3.Size = new System.Drawing.Size(1074, 23);
			this.oneGridItem3.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem3.TabIndex = 2;
			// 
			// bpc매입형태변경
			// 
			this.bpc매입형태변경.Controls.Add(this.lbl매입형태);
			this.bpc매입형태변경.Controls.Add(this.btn매입형태변경);
			this.bpc매입형태변경.Controls.Add(this.cbo매입형태);
			this.bpc매입형태변경.Location = new System.Drawing.Point(2, 1);
			this.bpc매입형태변경.Name = "bpc매입형태변경";
			this.bpc매입형태변경.Size = new System.Drawing.Size(293, 23);
			this.bpc매입형태변경.TabIndex = 3;
			this.bpc매입형태변경.Text = "bpPanelControl9";
			// 
			// lbl매입형태
			// 
			this.lbl매입형태.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl매입형태.Font = new System.Drawing.Font("굴림", 9F);
			this.lbl매입형태.Location = new System.Drawing.Point(0, 0);
			this.lbl매입형태.Name = "lbl매입형태";
			this.lbl매입형태.Size = new System.Drawing.Size(100, 23);
			this.lbl매입형태.TabIndex = 137;
			this.lbl매입형태.Tag = "";
			this.lbl매입형태.Text = "매입형태";
			this.lbl매입형태.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btn매입형태변경
			// 
			this.btn매입형태변경.BackColor = System.Drawing.Color.White;
			this.btn매입형태변경.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn매입형태변경.Dock = System.Windows.Forms.DockStyle.Right;
			this.btn매입형태변경.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn매입형태변경.Font = new System.Drawing.Font("굴림", 9F);
			this.btn매입형태변경.Location = new System.Drawing.Point(250, 0);
			this.btn매입형태변경.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn매입형태변경.Name = "btn매입형태변경";
			this.btn매입형태변경.Size = new System.Drawing.Size(43, 19);
			this.btn매입형태변경.TabIndex = 151;
			this.btn매입형태변경.TabStop = false;
			this.btn매입형태변경.Tag = " 변경";
			this.btn매입형태변경.Text = " 변경";
			this.btn매입형태변경.UseVisualStyleBackColor = false;
			// 
			// cbo매입형태
			// 
			this.cbo매입형태.AutoDropDown = true;
			this.cbo매입형태.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.cbo매입형태.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbo매입형태.Font = new System.Drawing.Font("굴림", 9F);
			this.cbo매입형태.ItemHeight = 12;
			this.cbo매입형태.Location = new System.Drawing.Point(106, 1);
			this.cbo매입형태.Name = "cbo매입형태";
			this.cbo매입형태.Size = new System.Drawing.Size(138, 20);
			this.cbo매입형태.TabIndex = 150;
			this.cbo매입형태.Tag = "FG_TPPURCHAS";
			this.cbo매입형태.UseKeyEnter = false;
			this.cbo매입형태.UseKeyF3 = false;
			// 
			// btn매입관리
			// 
			this.btn매입관리.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn매입관리.BackColor = System.Drawing.Color.White;
			this.btn매입관리.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn매입관리.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn매입관리.Font = new System.Drawing.Font("굴림", 9F);
			this.btn매입관리.Location = new System.Drawing.Point(879, 11);
			this.btn매입관리.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn매입관리.Name = "btn매입관리";
			this.btn매입관리.Size = new System.Drawing.Size(100, 19);
			this.btn매입관리.TabIndex = 133;
			this.btn매입관리.TabStop = false;
			this.btn매입관리.Tag = "";
			this.btn매입관리.Text = "매입관리";
			this.btn매입관리.UseVisualStyleBackColor = false;
			// 
			// btn입고적용
			// 
			this.btn입고적용.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn입고적용.BackColor = System.Drawing.Color.White;
			this.btn입고적용.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn입고적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn입고적용.Font = new System.Drawing.Font("굴림", 9F);
			this.btn입고적용.Location = new System.Drawing.Point(985, 11);
			this.btn입고적용.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn입고적용.Name = "btn입고적용";
			this.btn입고적용.Size = new System.Drawing.Size(100, 19);
			this.btn입고적용.TabIndex = 132;
			this.btn입고적용.TabStop = false;
			this.btn입고적용.Tag = "입고적용";
			this.btn입고적용.Text = "입고적용";
			this.btn입고적용.UseVisualStyleBackColor = false;
			// 
			// P_CZ_PU_IV_REG
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.btn매입관리);
			this.Controls.Add(this.btn입고적용);
			this.Font = new System.Drawing.Font("굴림", 9F);
			this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Name = "P_CZ_PU_IV_REG";
			this.Size = new System.Drawing.Size(1090, 796);
			this.TitleText = "매입등록";
			this.Controls.SetChildIndex(this.btn입고적용, 0);
			this.Controls.SetChildIndex(this.btn매입관리, 0);
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.mDataArea.ResumeLayout(false);
			this.mDataArea.PerformLayout();
			this._tlay_Main.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtp처리일자)).EndInit();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.rbtn계산서처리건별)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rbtn계산서처리일괄)).EndInit();
			this.bpPanelControl5.ResumeLayout(false);
			this.bpc부가세사업장변경.ResumeLayout(false);
			this.oneGridItem3.ResumeLayout(false);
			this.bpc매입형태변경.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _tlay_Main;
        private Duzon.Common.Controls.RoundedButton btn매입관리;
        private Duzon.Common.Controls.RoundedButton btn입고적용;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Duzon.Common.BpControls.BpCodeTextBox ctx부가세사업장;
        private Duzon.Common.BpControls.BpCodeTextBox ctx사업장;
        private Duzon.Common.Controls.RoundedButton btn부가세사업장변경;
        private Duzon.Common.Controls.LabelExt lbl담당자;
        private Duzon.Common.Controls.LabelExt lbl거래구분;
        private Duzon.Common.Controls.LabelExt lbl처리일자;
        private Duzon.Common.BpControls.BpCodeTextBox ctx담당자;
        private Duzon.Common.Controls.LabelExt lbl부가세사업장;
        private Duzon.Common.Controls.LabelExt lbl사업장;
        private Duzon.Common.Controls.RadioButtonExt rbtn계산서처리건별;
        private Duzon.Common.Controls.RadioButtonExt rbtn계산서처리일괄;
        private Duzon.Common.Controls.DropDownComboBox cbo거래구분;
        private Duzon.Common.Controls.DatePicker dtp처리일자;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpPanelControl bpc부가세사업장변경;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem3;
        private Duzon.Common.Controls.LabelExt lbl계산서처리;
        private Duzon.Common.BpControls.BpPanelControl bpc매입형태변경;
        private Duzon.Common.Controls.LabelExt lbl매입형태;
        private Duzon.Common.Controls.RoundedButton btn매입형태변경;
        private Duzon.Common.Controls.DropDownComboBox cbo매입형태;
	}
}


namespace cz
{
    partial class P_CZ_PR_OPOUT_PR_REG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_OPOUT_PR_REG));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flexID = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexM = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl공장 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp의뢰일자 = new Duzon.Common.Controls.DatePicker();
            this.lbl의뢰일자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt요청번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl요청번호 = new Duzon.Common.Controls.LabelExt();
            this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn작업실적적용 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.curID번호From = new Duzon.Common.Controls.CurrencyTextBox();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.curID번호To = new Duzon.Common.Controls.CurrencyTextBox();
            this.btn선택 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn해제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn일괄선택 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn일괄해제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexM)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp의뢰일자)).BeginInit();
            this.bpPanelControl1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.curID번호From)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.curID번호To)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(1060, 756);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._flexID, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._flexM, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1060, 756);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _flexID
            // 
            this._flexID.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexID.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexID.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
            this._flexID.AutoResize = false;
            this._flexID.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexID.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexID.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexID.EnabledHeaderCheck = true;
            this._flexID.Font = new System.Drawing.Font("굴림", 9F);
            this._flexID.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexID.Location = new System.Drawing.Point(3, 418);
            this._flexID.Name = "_flexID";
            this._flexID.Rows.Count = 1;
            this._flexID.Rows.DefaultSize = 20;
            this._flexID.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexID.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexID.ShowSort = false;
            this._flexID.Size = new System.Drawing.Size(1054, 335);
            this._flexID.StyleInfo = resources.GetString("_flexID.StyleInfo");
            this._flexID.TabIndex = 3;
            this._flexID.UseGridCalculator = true;
            // 
            // _flexM
            // 
            this._flexM.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
            this._flexM.AutoResize = false;
            this._flexM.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM.EnabledHeaderCheck = true;
            this._flexM.Font = new System.Drawing.Font("굴림", 9F);
            this._flexM.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM.Location = new System.Drawing.Point(3, 48);
            this._flexM.Name = "_flexM";
            this._flexM.Rows.Count = 1;
            this._flexM.Rows.DefaultSize = 20;
            this._flexM.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM.ShowSort = false;
            this._flexM.Size = new System.Drawing.Size(1054, 335);
            this._flexM.StyleInfo = resources.GetString("_flexM.StyleInfo");
            this._flexM.TabIndex = 1;
            this._flexM.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(1054, 39);
            this.oneGrid1.TabIndex = 0;
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
            this.oneGridItem1.Size = new System.Drawing.Size(1044, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.cbo공장);
            this.bpPanelControl3.Controls.Add(this.lbl공장);
            this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 4;
            // 
            // cbo공장
            // 
            this.cbo공장.AutoDropDown = true;
            this.cbo공장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo공장.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo공장.ItemHeight = 12;
            this.cbo공장.Location = new System.Drawing.Point(106, 0);
            this.cbo공장.Name = "cbo공장";
            this.cbo공장.Size = new System.Drawing.Size(186, 20);
            this.cbo공장.TabIndex = 77;
            this.cbo공장.Tag = "CD_PLANT";
            this.cbo공장.UseKeyEnter = false;
            this.cbo공장.UseKeyF3 = false;
            // 
            // lbl공장
            // 
            this.lbl공장.BackColor = System.Drawing.Color.Transparent;
            this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl공장.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl공장.Location = new System.Drawing.Point(0, 0);
            this.lbl공장.Name = "lbl공장";
            this.lbl공장.Size = new System.Drawing.Size(100, 23);
            this.lbl공장.TabIndex = 45;
            this.lbl공장.Tag = "CD_PLANT";
            this.lbl공장.Text = "공장";
            this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.dtp의뢰일자);
            this.bpPanelControl2.Controls.Add(this.lbl의뢰일자);
            this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // dtp의뢰일자
            // 
            this.dtp의뢰일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp의뢰일자.Location = new System.Drawing.Point(106, 1);
            this.dtp의뢰일자.Mask = "####/##/##";
            this.dtp의뢰일자.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp의뢰일자.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp의뢰일자.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp의뢰일자.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp의뢰일자.Name = "dtp의뢰일자";
            this.dtp의뢰일자.Size = new System.Drawing.Size(90, 21);
            this.dtp의뢰일자.TabIndex = 74;
            this.dtp의뢰일자.Tag = "DT_PR";
            this.dtp의뢰일자.Value = new System.DateTime(((long)(0)));
            // 
            // lbl의뢰일자
            // 
            this.lbl의뢰일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl의뢰일자.Location = new System.Drawing.Point(0, 0);
            this.lbl의뢰일자.Name = "lbl의뢰일자";
            this.lbl의뢰일자.Size = new System.Drawing.Size(100, 23);
            this.lbl의뢰일자.TabIndex = 0;
            this.lbl의뢰일자.Text = "의뢰일자";
            this.lbl의뢰일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.txt요청번호);
            this.bpPanelControl1.Controls.Add(this.lbl요청번호);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // txt요청번호
            // 
            this.txt요청번호.BackColor = System.Drawing.SystemColors.Control;
            this.txt요청번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt요청번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt요청번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt요청번호.Location = new System.Drawing.Point(107, 0);
            this.txt요청번호.Name = "txt요청번호";
            this.txt요청번호.ReadOnly = true;
            this.txt요청번호.Size = new System.Drawing.Size(185, 21);
            this.txt요청번호.TabIndex = 1;
            this.txt요청번호.TabStop = false;
            this.txt요청번호.Tag = "NO_PR";
            this.txt요청번호.UseKeyEnter = false;
            this.txt요청번호.UseKeyF3 = false;
            // 
            // lbl요청번호
            // 
            this.lbl요청번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl요청번호.Location = new System.Drawing.Point(0, 0);
            this.lbl요청번호.Name = "lbl요청번호";
            this.lbl요청번호.Size = new System.Drawing.Size(100, 23);
            this.lbl요청번호.TabIndex = 0;
            this.lbl요청번호.Text = "요청번호";
            this.lbl요청번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn삭제
            // 
            this.btn삭제.BackColor = System.Drawing.Color.Transparent;
            this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn삭제.Location = new System.Drawing.Point(111, 3);
            this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn삭제.Name = "btn삭제";
            this.btn삭제.Size = new System.Drawing.Size(70, 19);
            this.btn삭제.TabIndex = 0;
            this.btn삭제.TabStop = false;
            this.btn삭제.Text = "삭제";
            this.btn삭제.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btn삭제);
            this.flowLayoutPanel1.Controls.Add(this.btn작업실적적용);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(873, 10);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(184, 23);
            this.flowLayoutPanel1.TabIndex = 123;
            // 
            // btn작업실적적용
            // 
            this.btn작업실적적용.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn작업실적적용.BackColor = System.Drawing.Color.White;
            this.btn작업실적적용.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn작업실적적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn작업실적적용.Location = new System.Drawing.Point(12, 3);
            this.btn작업실적적용.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn작업실적적용.Name = "btn작업실적적용";
            this.btn작업실적적용.Size = new System.Drawing.Size(93, 19);
            this.btn작업실적적용.TabIndex = 12;
            this.btn작업실적적용.TabStop = false;
            this.btn작업실적적용.Tag = "";
            this.btn작업실적적용.Text = "작업실적적용";
            this.btn작업실적적용.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btn일괄해제);
            this.flowLayoutPanel2.Controls.Add(this.btn일괄선택);
            this.flowLayoutPanel2.Controls.Add(this.btn해제);
            this.flowLayoutPanel2.Controls.Add(this.btn선택);
            this.flowLayoutPanel2.Controls.Add(this.curID번호To);
            this.flowLayoutPanel2.Controls.Add(this.labelExt1);
            this.flowLayoutPanel2.Controls.Add(this.curID번호From);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 389);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1054, 23);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // curID번호From
            // 
            this.curID번호From.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.curID번호From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.curID번호From.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.curID번호From.ForeColor = System.Drawing.SystemColors.ControlText;
            this.curID번호From.Location = new System.Drawing.Point(555, 3);
            this.curID번호From.Name = "curID번호From";
            this.curID번호From.NullString = "0";
            this.curID번호From.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.curID번호From.Size = new System.Drawing.Size(100, 21);
            this.curID번호From.TabIndex = 6;
            this.curID번호From.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelExt1
            // 
            this.labelExt1.Location = new System.Drawing.Point(661, 0);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Size = new System.Drawing.Size(10, 23);
            this.labelExt1.TabIndex = 7;
            this.labelExt1.Text = "~";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // curID번호To
            // 
            this.curID번호To.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.curID번호To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.curID번호To.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.curID번호To.ForeColor = System.Drawing.SystemColors.ControlText;
            this.curID번호To.Location = new System.Drawing.Point(677, 3);
            this.curID번호To.Name = "curID번호To";
            this.curID번호To.NullString = "0";
            this.curID번호To.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.curID번호To.Size = new System.Drawing.Size(100, 21);
            this.curID번호To.TabIndex = 8;
            this.curID번호To.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn선택
            // 
            this.btn선택.BackColor = System.Drawing.Color.Transparent;
            this.btn선택.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn선택.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn선택.Location = new System.Drawing.Point(783, 3);
            this.btn선택.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn선택.Name = "btn선택";
            this.btn선택.Size = new System.Drawing.Size(55, 19);
            this.btn선택.TabIndex = 9;
            this.btn선택.TabStop = false;
            this.btn선택.Text = "선택";
            this.btn선택.UseVisualStyleBackColor = false;
            // 
            // btn해제
            // 
            this.btn해제.BackColor = System.Drawing.Color.Transparent;
            this.btn해제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn해제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn해제.Location = new System.Drawing.Point(844, 3);
            this.btn해제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn해제.Name = "btn해제";
            this.btn해제.Size = new System.Drawing.Size(55, 19);
            this.btn해제.TabIndex = 10;
            this.btn해제.TabStop = false;
            this.btn해제.Text = "해제";
            this.btn해제.UseVisualStyleBackColor = false;
            // 
            // btn일괄선택
            // 
            this.btn일괄선택.BackColor = System.Drawing.Color.Transparent;
            this.btn일괄선택.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn일괄선택.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn일괄선택.Location = new System.Drawing.Point(905, 3);
            this.btn일괄선택.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn일괄선택.Name = "btn일괄선택";
            this.btn일괄선택.Size = new System.Drawing.Size(70, 19);
            this.btn일괄선택.TabIndex = 11;
            this.btn일괄선택.TabStop = false;
            this.btn일괄선택.Text = "일괄선택";
            this.btn일괄선택.UseVisualStyleBackColor = false;
            // 
            // btn일괄해제
            // 
            this.btn일괄해제.BackColor = System.Drawing.Color.Transparent;
            this.btn일괄해제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn일괄해제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn일괄해제.Location = new System.Drawing.Point(981, 3);
            this.btn일괄해제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn일괄해제.Name = "btn일괄해제";
            this.btn일괄해제.Size = new System.Drawing.Size(70, 19);
            this.btn일괄해제.TabIndex = 12;
            this.btn일괄해제.TabStop = false;
            this.btn일괄해제.Text = "일괄해제";
            this.btn일괄해제.UseVisualStyleBackColor = false;
            // 
            // P_CZ_PR_OPOUT_PR_REG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "P_CZ_PR_OPOUT_PR_REG";
            this.Size = new System.Drawing.Size(1060, 796);
            this.TitleText = "P_CZ_PR_OPOUT_PR_REG";
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexM)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp의뢰일자)).EndInit();
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.curID번호From)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.curID번호To)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.LabelExt lbl의뢰일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.LabelExt lbl요청번호;
        private Dass.FlexGrid.FlexGrid _flexM;
        private Duzon.Common.Controls.TextBoxExt txt요청번호;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn작업실적적용;
        private Duzon.Common.Controls.DatePicker dtp의뢰일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.DropDownComboBox cbo공장;
        private Duzon.Common.Controls.LabelExt lbl공장;
        private Dass.FlexGrid.FlexGrid _flexID;
        private Duzon.Common.Controls.RoundedButton btn삭제;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Duzon.Common.Controls.CurrencyTextBox curID번호From;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.CurrencyTextBox curID번호To;
        private Duzon.Common.Controls.RoundedButton btn선택;
        private Duzon.Common.Controls.RoundedButton btn해제;
        private Duzon.Common.Controls.RoundedButton btn일괄선택;
        private Duzon.Common.Controls.RoundedButton btn일괄해제;
    }
}
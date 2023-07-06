namespace cz
{
    partial class P_CZ_SA_GIR_SCH_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_GIR_SCH_SUB));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx매출처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl매출처 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp의뢰일자 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl의뢰일자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt협조전번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl협조전번호 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt반품여부 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl반품여부 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo출고공장 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl출고공장 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx프로젝트 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl프로젝트 = new Duzon.Common.Controls.LabelExt();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn적용 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(911, 564);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 104);
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
            this.splitContainer1.Size = new System.Drawing.Size(905, 457);
            this.splitContainer1.SplitterDistance = 259;
            this.splitContainer1.TabIndex = 0;
            // 
            // _flexH
            // 
            this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexH.AutoResize = false;
            this._flexH.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexH.EnabledHeaderCheck = true;
            this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexH.Location = new System.Drawing.Point(0, 0);
            this._flexH.Name = "_flexH";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 18;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(905, 259);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 0;
            this._flexH.UseGridCalculator = true;
            // 
            // _flexL
            // 
            this._flexL.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexL.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexL.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexL.AutoResize = false;
            this._flexL.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexL.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexL.EnabledHeaderCheck = true;
            this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexL.Location = new System.Drawing.Point(0, 0);
            this._flexL.Name = "_flexL";
            this._flexL.Rows.Count = 1;
            this._flexL.Rows.DefaultSize = 18;
            this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexL.ShowSort = false;
            this._flexL.Size = new System.Drawing.Size(905, 194);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 0;
            this._flexL.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(905, 64);
            this.oneGrid1.TabIndex = 1;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.Controls.Add(this.bpPanelControl6);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(895, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.ctx매출처);
            this.bpPanelControl2.Controls.Add(this.lbl매출처);
            this.bpPanelControl2.Location = new System.Drawing.Point(596, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(295, 22);
            this.bpPanelControl2.TabIndex = 2;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // ctx매출처
            // 
            this.ctx매출처.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx매출처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx매출처.Location = new System.Drawing.Point(106, 0);
            this.ctx매출처.Name = "ctx매출처";
            this.ctx매출처.Size = new System.Drawing.Size(189, 21);
            this.ctx매출처.TabIndex = 1;
            this.ctx매출처.TabStop = false;
            this.ctx매출처.Text = "bpCodeTextBox1";
            // 
            // lbl매출처
            // 
            this.lbl매출처.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl매출처.Location = new System.Drawing.Point(0, 0);
            this.lbl매출처.Name = "lbl매출처";
            this.lbl매출처.Size = new System.Drawing.Size(100, 22);
            this.lbl매출처.TabIndex = 0;
            this.lbl매출처.Text = "매출처";
            this.lbl매출처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp의뢰일자);
            this.bpPanelControl1.Controls.Add(this.lbl의뢰일자);
            this.bpPanelControl1.Location = new System.Drawing.Point(299, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(295, 22);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // dtp의뢰일자
            // 
            this.dtp의뢰일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp의뢰일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp의뢰일자.IsNecessaryCondition = true;
            this.dtp의뢰일자.Location = new System.Drawing.Point(110, 0);
            this.dtp의뢰일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp의뢰일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp의뢰일자.Name = "dtp의뢰일자";
            this.dtp의뢰일자.Size = new System.Drawing.Size(185, 21);
            this.dtp의뢰일자.TabIndex = 1;
            // 
            // lbl의뢰일자
            // 
            this.lbl의뢰일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl의뢰일자.Location = new System.Drawing.Point(0, 0);
            this.lbl의뢰일자.Name = "lbl의뢰일자";
            this.lbl의뢰일자.Size = new System.Drawing.Size(100, 22);
            this.lbl의뢰일자.TabIndex = 0;
            this.lbl의뢰일자.Text = "의뢰일자";
            this.lbl의뢰일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.txt협조전번호);
            this.bpPanelControl6.Controls.Add(this.lbl협조전번호);
            this.bpPanelControl6.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(295, 23);
            this.bpPanelControl6.TabIndex = 5;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // txt협조전번호
            // 
            this.txt협조전번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt협조전번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt협조전번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt협조전번호.Location = new System.Drawing.Point(110, 0);
            this.txt협조전번호.Name = "txt협조전번호";
            this.txt협조전번호.Size = new System.Drawing.Size(185, 21);
            this.txt협조전번호.TabIndex = 1;
            // 
            // lbl협조전번호
            // 
            this.lbl협조전번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl협조전번호.Location = new System.Drawing.Point(0, 0);
            this.lbl협조전번호.Name = "lbl협조전번호";
            this.lbl협조전번호.Size = new System.Drawing.Size(100, 23);
            this.lbl협조전번호.TabIndex = 0;
            this.lbl협조전번호.Text = "협조전번호";
            this.lbl협조전번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl3);
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl4);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(895, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.txt반품여부);
            this.bpPanelControl3.Controls.Add(this.lbl반품여부);
            this.bpPanelControl3.Location = new System.Drawing.Point(596, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(295, 22);
            this.bpPanelControl3.TabIndex = 3;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // txt반품여부
            // 
            this.txt반품여부.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.txt반품여부.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt반품여부.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt반품여부.Location = new System.Drawing.Point(106, 0);
            this.txt반품여부.Name = "txt반품여부";
            this.txt반품여부.ReadOnly = true;
            this.txt반품여부.Size = new System.Drawing.Size(52, 21);
            this.txt반품여부.TabIndex = 1;
            this.txt반품여부.TabStop = false;
            // 
            // lbl반품여부
            // 
            this.lbl반품여부.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl반품여부.Location = new System.Drawing.Point(0, 0);
            this.lbl반품여부.Name = "lbl반품여부";
            this.lbl반품여부.Size = new System.Drawing.Size(100, 22);
            this.lbl반품여부.TabIndex = 0;
            this.lbl반품여부.Text = "반품여부";
            this.lbl반품여부.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.cbo출고공장);
            this.bpPanelControl5.Controls.Add(this.lbl출고공장);
            this.bpPanelControl5.Location = new System.Drawing.Point(299, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(295, 22);
            this.bpPanelControl5.TabIndex = 5;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // cbo출고공장
            // 
            this.cbo출고공장.AutoDropDown = true;
            this.cbo출고공장.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo출고공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo출고공장.FormattingEnabled = true;
            this.cbo출고공장.ItemHeight = 12;
            this.cbo출고공장.Location = new System.Drawing.Point(110, 0);
            this.cbo출고공장.Name = "cbo출고공장";
            this.cbo출고공장.Size = new System.Drawing.Size(185, 20);
            this.cbo출고공장.TabIndex = 1;
            // 
            // lbl출고공장
            // 
            this.lbl출고공장.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl출고공장.Location = new System.Drawing.Point(0, 0);
            this.lbl출고공장.Name = "lbl출고공장";
            this.lbl출고공장.Size = new System.Drawing.Size(100, 22);
            this.lbl출고공장.TabIndex = 0;
            this.lbl출고공장.Text = "출고공장";
            this.lbl출고공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.ctx프로젝트);
            this.bpPanelControl4.Controls.Add(this.lbl프로젝트);
            this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(295, 22);
            this.bpPanelControl4.TabIndex = 4;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // ctx프로젝트
            // 
            this.ctx프로젝트.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx프로젝트.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.ctx프로젝트.Location = new System.Drawing.Point(110, 0);
            this.ctx프로젝트.Name = "ctx프로젝트";
            this.ctx프로젝트.Size = new System.Drawing.Size(185, 21);
            this.ctx프로젝트.TabIndex = 1;
            this.ctx프로젝트.TabStop = false;
            this.ctx프로젝트.UserCodeName = "NM_PROJECT";
            this.ctx프로젝트.UserCodeValue = "NO_PROJECT";
            this.ctx프로젝트.UserHelpID = "H_SA_PRJ_SUB";
            // 
            // lbl프로젝트
            // 
            this.lbl프로젝트.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl프로젝트.Location = new System.Drawing.Point(0, 0);
            this.lbl프로젝트.Name = "lbl프로젝트";
            this.lbl프로젝트.Size = new System.Drawing.Size(100, 22);
            this.lbl프로젝트.TabIndex = 0;
            this.lbl프로젝트.Text = "프로젝트";
            this.lbl프로젝트.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn취소);
            this.flowLayoutPanel1.Controls.Add(this.btn적용);
            this.flowLayoutPanel1.Controls.Add(this.btn조회);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 73);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(905, 25);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btn취소
            // 
            this.btn취소.BackColor = System.Drawing.Color.Transparent;
            this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn취소.Location = new System.Drawing.Point(832, 3);
            this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn취소.Name = "btn취소";
            this.btn취소.Size = new System.Drawing.Size(70, 19);
            this.btn취소.TabIndex = 0;
            this.btn취소.TabStop = false;
            this.btn취소.Text = "취소";
            this.btn취소.UseVisualStyleBackColor = false;
            // 
            // btn적용
            // 
            this.btn적용.BackColor = System.Drawing.Color.Transparent;
            this.btn적용.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn적용.Location = new System.Drawing.Point(756, 3);
            this.btn적용.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn적용.Name = "btn적용";
            this.btn적용.Size = new System.Drawing.Size(70, 19);
            this.btn적용.TabIndex = 1;
            this.btn적용.TabStop = false;
            this.btn적용.Text = "적용";
            this.btn적용.UseVisualStyleBackColor = false;
            // 
            // btn조회
            // 
            this.btn조회.BackColor = System.Drawing.Color.Transparent;
            this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn조회.Location = new System.Drawing.Point(680, 3);
            this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn조회.Name = "btn조회";
            this.btn조회.Size = new System.Drawing.Size(70, 19);
            this.btn조회.TabIndex = 2;
            this.btn조회.TabStop = false;
            this.btn조회.Text = "조회";
            this.btn조회.UseVisualStyleBackColor = false;
            // 
            // P_CZ_SA_GIR_SCH_SUB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(911, 612);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_CZ_SA_GIR_SCH_SUB";
            this.Text = "더존 ERP iU";
            this.TitleText = "협조전조회";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl6.ResumeLayout(false);
            this.bpPanelControl6.PerformLayout();
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl3.PerformLayout();
            this.bpPanelControl5.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.Controls.DropDownComboBox cbo출고공장;
        private Duzon.Common.Controls.LabelExt lbl출고공장;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpCodeTextBox ctx매출처;
        private Duzon.Common.Controls.LabelExt lbl매출처;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.PeriodPicker dtp의뢰일자;
        private Duzon.Common.Controls.LabelExt lbl의뢰일자;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.TextBoxExt txt반품여부;
        private Duzon.Common.Controls.LabelExt lbl반품여부;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpCodeTextBox ctx프로젝트;
        private Duzon.Common.Controls.LabelExt lbl프로젝트;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn취소;
        private Duzon.Common.Controls.RoundedButton btn적용;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.Controls.TextBoxExt txt협조전번호;
        private Duzon.Common.Controls.LabelExt lbl협조전번호;
    }
}
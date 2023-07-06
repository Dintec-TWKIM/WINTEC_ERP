namespace cz
{
    partial class P_CZ_HR_PEVALU
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_HR_PEVALU));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bppnl평가그룹 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo평가그룹 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl평가그룹 = new Duzon.Common.Controls.LabelExt();
            this.bppnl평가유형 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo평가유형 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl평가유형 = new Duzon.Common.Controls.LabelExt();
            this.bppnl평가코드 = new Duzon.Common.BpControls.BpPanelControl();
            this.bpc평가코드 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl평가코드 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bppnl평가차수 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo평가차수 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl평가차수 = new Duzon.Common.Controls.LabelExt();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.imagePanel4 = new Duzon.Common.Controls.ImagePanel(this.components);
            this.txt차수별이전단계 = new Duzon.Common.Controls.TextBoxExt();
            this.imagePanel3 = new Duzon.Common.Controls.ImagePanel(this.components);
            this.txt차수별 = new Duzon.Common.Controls.TextBoxExt();
            this.imagePanel2 = new Duzon.Common.Controls.ImagePanel(this.components);
            this.txt항목별이전단계 = new Duzon.Common.Controls.TextBoxExt();
            this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
            this.txt항목별 = new Duzon.Common.Controls.TextBoxExt();
            this.btn개인별정보 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bppnl평가그룹.SuspendLayout();
            this.bppnl평가유형.SuspendLayout();
            this.bppnl평가코드.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bppnl평가차수.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            this.imagePanel4.SuspendLayout();
            this.imagePanel3.SuspendLayout();
            this.imagePanel2.SuspendLayout();
            this.imagePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(979, 579);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(979, 579);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(973, 62);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bppnl평가그룹);
            this.oneGridItem1.Controls.Add(this.bppnl평가유형);
            this.oneGridItem1.Controls.Add(this.bppnl평가코드);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(963, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bppnl평가그룹
            // 
            this.bppnl평가그룹.Controls.Add(this.cbo평가그룹);
            this.bppnl평가그룹.Controls.Add(this.lbl평가그룹);
            this.bppnl평가그룹.Location = new System.Drawing.Point(640, 1);
            this.bppnl평가그룹.Name = "bppnl평가그룹";
            this.bppnl평가그룹.Size = new System.Drawing.Size(317, 23);
            this.bppnl평가그룹.TabIndex = 2;
            this.bppnl평가그룹.Text = "bpPanelControl3";
            // 
            // cbo평가그룹
            // 
            this.cbo평가그룹.AutoDropDown = true;
            this.cbo평가그룹.BackColor = System.Drawing.Color.White;
            this.cbo평가그룹.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo평가그룹.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo평가그룹.ItemHeight = 12;
            this.cbo평가그룹.Location = new System.Drawing.Point(106, 0);
            this.cbo평가그룹.Name = "cbo평가그룹";
            this.cbo평가그룹.Size = new System.Drawing.Size(211, 20);
            this.cbo평가그룹.TabIndex = 180;
            this.cbo평가그룹.Tag = "";
            // 
            // lbl평가그룹
            // 
            this.lbl평가그룹.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl평가그룹.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl평가그룹.Location = new System.Drawing.Point(0, 0);
            this.lbl평가그룹.Name = "lbl평가그룹";
            this.lbl평가그룹.Size = new System.Drawing.Size(100, 23);
            this.lbl평가그룹.TabIndex = 179;
            this.lbl평가그룹.Tag = "";
            this.lbl평가그룹.Text = "평가그룹";
            this.lbl평가그룹.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bppnl평가유형
            // 
            this.bppnl평가유형.Controls.Add(this.cbo평가유형);
            this.bppnl평가유형.Controls.Add(this.lbl평가유형);
            this.bppnl평가유형.Location = new System.Drawing.Point(321, 1);
            this.bppnl평가유형.Name = "bppnl평가유형";
            this.bppnl평가유형.Size = new System.Drawing.Size(317, 23);
            this.bppnl평가유형.TabIndex = 1;
            this.bppnl평가유형.Text = "bpPanelControl2";
            // 
            // cbo평가유형
            // 
            this.cbo평가유형.AutoDropDown = true;
            this.cbo평가유형.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo평가유형.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo평가유형.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo평가유형.ItemHeight = 12;
            this.cbo평가유형.Location = new System.Drawing.Point(106, 0);
            this.cbo평가유형.Name = "cbo평가유형";
            this.cbo평가유형.Size = new System.Drawing.Size(211, 20);
            this.cbo평가유형.TabIndex = 181;
            this.cbo평가유형.Tag = "";
            // 
            // lbl평가유형
            // 
            this.lbl평가유형.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl평가유형.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl평가유형.Location = new System.Drawing.Point(0, 0);
            this.lbl평가유형.Name = "lbl평가유형";
            this.lbl평가유형.Size = new System.Drawing.Size(100, 23);
            this.lbl평가유형.TabIndex = 180;
            this.lbl평가유형.Tag = "";
            this.lbl평가유형.Text = "평가유형";
            this.lbl평가유형.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bppnl평가코드
            // 
            this.bppnl평가코드.Controls.Add(this.bpc평가코드);
            this.bppnl평가코드.Controls.Add(this.lbl평가코드);
            this.bppnl평가코드.Location = new System.Drawing.Point(2, 1);
            this.bppnl평가코드.Name = "bppnl평가코드";
            this.bppnl평가코드.Size = new System.Drawing.Size(317, 23);
            this.bppnl평가코드.TabIndex = 0;
            this.bppnl평가코드.Text = "bpPanelControl1";
            // 
            // bpc평가코드
            // 
            this.bpc평가코드.Dock = System.Windows.Forms.DockStyle.Right;
            this.bpc평가코드.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_TABLE_SUB;
            this.bpc평가코드.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.bpc평가코드.Location = new System.Drawing.Point(106, 0);
            this.bpc평가코드.Name = "bpc평가코드";
            this.bpc평가코드.Size = new System.Drawing.Size(211, 21);
            this.bpc평가코드.TabIndex = 11;
            this.bpc평가코드.TabStop = false;
            this.bpc평가코드.Tag = "";
            this.bpc평가코드.Text = "bpCodeTextBox1";
            // 
            // lbl평가코드
            // 
            this.lbl평가코드.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl평가코드.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl평가코드.Location = new System.Drawing.Point(0, 0);
            this.lbl평가코드.Name = "lbl평가코드";
            this.lbl평가코드.Size = new System.Drawing.Size(100, 23);
            this.lbl평가코드.TabIndex = 10;
            this.lbl평가코드.Tag = "";
            this.lbl평가코드.Text = "평가코드";
            this.lbl평가코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bppnl평가차수);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(963, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bppnl평가차수
            // 
            this.bppnl평가차수.Controls.Add(this.cbo평가차수);
            this.bppnl평가차수.Controls.Add(this.lbl평가차수);
            this.bppnl평가차수.Location = new System.Drawing.Point(2, 1);
            this.bppnl평가차수.Name = "bppnl평가차수";
            this.bppnl평가차수.Size = new System.Drawing.Size(317, 23);
            this.bppnl평가차수.TabIndex = 0;
            this.bppnl평가차수.Text = "bpPanelControl4";
            // 
            // cbo평가차수
            // 
            this.cbo평가차수.AutoDropDown = true;
            this.cbo평가차수.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo평가차수.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo평가차수.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo평가차수.ItemHeight = 12;
            this.cbo평가차수.Location = new System.Drawing.Point(106, 0);
            this.cbo평가차수.Name = "cbo평가차수";
            this.cbo평가차수.Size = new System.Drawing.Size(211, 20);
            this.cbo평가차수.TabIndex = 182;
            this.cbo평가차수.Tag = "";
            // 
            // lbl평가차수
            // 
            this.lbl평가차수.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl평가차수.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl평가차수.Location = new System.Drawing.Point(0, 0);
            this.lbl평가차수.Name = "lbl평가차수";
            this.lbl평가차수.Size = new System.Drawing.Size(100, 23);
            this.lbl평가차수.TabIndex = 181;
            this.lbl평가차수.Tag = "";
            this.lbl평가차수.Text = "평가차수";
            this.lbl평가차수.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.Controls.Add(this._flexH, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 71);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(973, 505);
            this.tableLayoutPanel2.TabIndex = 1;
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
            this._flexH.Location = new System.Drawing.Point(4, 4);
            this._flexH.Name = "_flexH";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 20;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(188, 497);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 0;
            this._flexH.UseGridCalculator = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this._flexL, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(199, 4);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 251F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(770, 497);
            this.tableLayoutPanel6.TabIndex = 1;
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
            this._flexL.Location = new System.Drawing.Point(3, 3);
            this._flexL.Name = "_flexL";
            this._flexL.Rows.Count = 1;
            this._flexL.Rows.DefaultSize = 20;
            this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexL.ShowSort = false;
            this._flexL.Size = new System.Drawing.Size(764, 245);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 0;
            this._flexL.UseGridCalculator = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.imagePanel4, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.imagePanel3, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.imagePanel2, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.imagePanel1, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 254);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(764, 240);
            this.tableLayoutPanel7.TabIndex = 1;
            // 
            // imagePanel4
            // 
            this.imagePanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.imagePanel4.Controls.Add(this.txt차수별이전단계);
            this.imagePanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel4.LeftImage = null;
            this.imagePanel4.Location = new System.Drawing.Point(385, 123);
            this.imagePanel4.Name = "imagePanel4";
            this.imagePanel4.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
            this.imagePanel4.PatternImage = null;
            this.imagePanel4.RightImage = null;
            this.imagePanel4.Size = new System.Drawing.Size(376, 114);
            this.imagePanel4.TabIndex = 3;
            this.imagePanel4.TitleText = "차수별 평가의견 (이전단계)";
            // 
            // txt차수별이전단계
            // 
            this.txt차수별이전단계.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt차수별이전단계.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt차수별이전단계.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt차수별이전단계.Location = new System.Drawing.Point(3, 27);
            this.txt차수별이전단계.MaxLength = 1000;
            this.txt차수별이전단계.Multiline = true;
            this.txt차수별이전단계.Name = "txt차수별이전단계";
            this.txt차수별이전단계.ReadOnly = true;
            this.txt차수별이전단계.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt차수별이전단계.Size = new System.Drawing.Size(370, 84);
            this.txt차수별이전단계.TabIndex = 182;
            this.txt차수별이전단계.TabStop = false;
            this.txt차수별이전단계.Tag = "COMMENT2";
            // 
            // imagePanel3
            // 
            this.imagePanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.imagePanel3.Controls.Add(this.txt차수별);
            this.imagePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel3.LeftImage = null;
            this.imagePanel3.Location = new System.Drawing.Point(3, 123);
            this.imagePanel3.Name = "imagePanel3";
            this.imagePanel3.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
            this.imagePanel3.PatternImage = null;
            this.imagePanel3.RightImage = null;
            this.imagePanel3.Size = new System.Drawing.Size(376, 114);
            this.imagePanel3.TabIndex = 2;
            this.imagePanel3.TitleText = "차수별 평가의견";
            // 
            // txt차수별
            // 
            this.txt차수별.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt차수별.BackColor = System.Drawing.Color.White;
            this.txt차수별.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt차수별.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt차수별.Location = new System.Drawing.Point(3, 26);
            this.txt차수별.MaxLength = 1500;
            this.txt차수별.Multiline = true;
            this.txt차수별.Name = "txt차수별";
            this.txt차수별.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt차수별.Size = new System.Drawing.Size(370, 85);
            this.txt차수별.TabIndex = 182;
            this.txt차수별.Tag = "COMMENT1";
            this.txt차수별.UseKeyEnter = false;
            this.txt차수별.UseKeyF3 = false;
            // 
            // imagePanel2
            // 
            this.imagePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.imagePanel2.Controls.Add(this.txt항목별이전단계);
            this.imagePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel2.LeftImage = null;
            this.imagePanel2.Location = new System.Drawing.Point(385, 3);
            this.imagePanel2.Name = "imagePanel2";
            this.imagePanel2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
            this.imagePanel2.PatternImage = null;
            this.imagePanel2.RightImage = null;
            this.imagePanel2.Size = new System.Drawing.Size(376, 114);
            this.imagePanel2.TabIndex = 1;
            this.imagePanel2.TitleText = "항목별 평가의견 (이전단계)";
            // 
            // txt항목별이전단계
            // 
            this.txt항목별이전단계.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt항목별이전단계.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt항목별이전단계.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt항목별이전단계.Location = new System.Drawing.Point(3, 28);
            this.txt항목별이전단계.MaxLength = 500;
            this.txt항목별이전단계.Multiline = true;
            this.txt항목별이전단계.Name = "txt항목별이전단계";
            this.txt항목별이전단계.ReadOnly = true;
            this.txt항목별이전단계.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt항목별이전단계.Size = new System.Drawing.Size(370, 83);
            this.txt항목별이전단계.TabIndex = 182;
            this.txt항목별이전단계.TabStop = false;
            this.txt항목별이전단계.Tag = "DC_COMMENT2";
            // 
            // imagePanel1
            // 
            this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.imagePanel1.Controls.Add(this.txt항목별);
            this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel1.LeftImage = null;
            this.imagePanel1.Location = new System.Drawing.Point(3, 3);
            this.imagePanel1.Name = "imagePanel1";
            this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
            this.imagePanel1.PatternImage = null;
            this.imagePanel1.RightImage = null;
            this.imagePanel1.Size = new System.Drawing.Size(376, 114);
            this.imagePanel1.TabIndex = 0;
            this.imagePanel1.TitleText = "항목별 평가의견";
            // 
            // txt항목별
            // 
            this.txt항목별.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt항목별.BackColor = System.Drawing.Color.White;
            this.txt항목별.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt항목별.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt항목별.Location = new System.Drawing.Point(3, 28);
            this.txt항목별.MaxLength = 500;
            this.txt항목별.Multiline = true;
            this.txt항목별.Name = "txt항목별";
            this.txt항목별.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt항목별.Size = new System.Drawing.Size(370, 83);
            this.txt항목별.TabIndex = 182;
            this.txt항목별.Tag = "DC_COMMENT1";
            this.txt항목별.UseKeyEnter = false;
            this.txt항목별.UseKeyF3 = false;
            // 
            // btn개인별정보
            // 
            this.btn개인별정보.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn개인별정보.BackColor = System.Drawing.Color.White;
            this.btn개인별정보.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn개인별정보.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn개인별정보.Location = new System.Drawing.Point(875, 14);
            this.btn개인별정보.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn개인별정보.Name = "btn개인별정보";
            this.btn개인별정보.Size = new System.Drawing.Size(101, 19);
            this.btn개인별정보.TabIndex = 162;
            this.btn개인별정보.TabStop = false;
            this.btn개인별정보.Text = "개인별관련정보";
            this.btn개인별정보.UseVisualStyleBackColor = false;
            // 
            // P_CZ_HR_PEVALU
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btn개인별정보);
            this.Name = "P_CZ_HR_PEVALU";
            this.Size = new System.Drawing.Size(979, 619);
            this.TitleText = "평가등록";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.btn개인별정보, 0);
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bppnl평가그룹.ResumeLayout(false);
            this.bppnl평가유형.ResumeLayout(false);
            this.bppnl평가코드.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bppnl평가차수.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.imagePanel4.ResumeLayout(false);
            this.imagePanel4.PerformLayout();
            this.imagePanel3.ResumeLayout(false);
            this.imagePanel3.PerformLayout();
            this.imagePanel2.ResumeLayout(false);
            this.imagePanel2.PerformLayout();
            this.imagePanel1.ResumeLayout(false);
            this.imagePanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bppnl평가그룹;
        private Duzon.Common.Controls.DropDownComboBox cbo평가그룹;
        private Duzon.Common.Controls.LabelExt lbl평가그룹;
        private Duzon.Common.BpControls.BpPanelControl bppnl평가유형;
        private Duzon.Common.Controls.DropDownComboBox cbo평가유형;
        private Duzon.Common.Controls.LabelExt lbl평가유형;
        private Duzon.Common.BpControls.BpPanelControl bppnl평가코드;
        private Duzon.Common.BpControls.BpCodeTextBox bpc평가코드;
        private Duzon.Common.Controls.LabelExt lbl평가코드;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bppnl평가차수;
        private Duzon.Common.Controls.DropDownComboBox cbo평가차수;
        private Duzon.Common.Controls.LabelExt lbl평가차수;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Duzon.Common.Controls.TextBoxExt txt항목별이전단계;
        private Duzon.Common.Controls.TextBoxExt txt항목별;
        private Duzon.Common.Controls.RoundedButton btn개인별정보;
        private Duzon.Common.Controls.TextBoxExt txt차수별이전단계;
        private Duzon.Common.Controls.TextBoxExt txt차수별;
        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private Duzon.Common.Controls.ImagePanel imagePanel4;
        private Duzon.Common.Controls.ImagePanel imagePanel3;
        private Duzon.Common.Controls.ImagePanel imagePanel2;
        private Duzon.Common.Controls.ImagePanel imagePanel1;
    }
}
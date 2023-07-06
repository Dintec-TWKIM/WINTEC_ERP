namespace cz
{
    partial class P_CZ_HR_PEVALU_DLG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_HR_PEVALU_DLG));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flex2 = new Dass.FlexGrid.FlexGrid(this.components);
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.txt성명 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl이름 = new Duzon.Common.Controls.LabelExt();
            this.txt사번 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl사번 = new Duzon.Common.Controls.LabelExt();
            this.label13 = new Duzon.Common.Controls.LabelExt();
            this.dtp시작 = new Duzon.Common.Controls.DatePicker();
            this.dtp종료 = new Duzon.Common.Controls.DatePicker();
            this.lbl조회년월 = new Duzon.Common.Controls.LabelExt();
            this.imagePanel2 = new Duzon.Common.Controls.ImagePanel(this.components);
            this.imagePanel1 = new Duzon.Common.Controls.ImagePanel(this.components);
            this._flex1 = new Dass.FlexGrid.FlexGrid(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex2)).BeginInit();
            this.panelExt1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp시작)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp종료)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._flex2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panelExt1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.imagePanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.imagePanel1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._flex1, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 50);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(717, 449);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _flex2
            // 
            this._flex2.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex2.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex2.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex2.AutoResize = false;
            this._flex2.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex2.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex2.EnabledHeaderCheck = true;
            this._flex2.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex2.Location = new System.Drawing.Point(4, 278);
            this._flex2.Name = "_flex2";
            this._flex2.Rows.Count = 1;
            this._flex2.Rows.DefaultSize = 18;
            this._flex2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex2.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex2.ShowSort = false;
            this._flex2.Size = new System.Drawing.Size(709, 167);
            this._flex2.StyleInfo = resources.GetString("_flex2.StyleInfo");
            this._flex2.TabIndex = 12;
            this._flex2.UseGridCalculator = true;
            // 
            // panelExt1
            // 
            this.panelExt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelExt1.Controls.Add(this.btn조회);
            this.panelExt1.Controls.Add(this.txt성명);
            this.panelExt1.Controls.Add(this.lbl이름);
            this.panelExt1.Controls.Add(this.txt사번);
            this.panelExt1.Controls.Add(this.lbl사번);
            this.panelExt1.Controls.Add(this.label13);
            this.panelExt1.Controls.Add(this.dtp시작);
            this.panelExt1.Controls.Add(this.dtp종료);
            this.panelExt1.Controls.Add(this.lbl조회년월);
            this.panelExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt1.Location = new System.Drawing.Point(4, 4);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(709, 31);
            this.panelExt1.TabIndex = 1;
            // 
            // btn조회
            // 
            this.btn조회.BackColor = System.Drawing.Color.White;
            this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn조회.Location = new System.Drawing.Point(619, 5);
            this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn조회.Name = "btn조회";
            this.btn조회.Size = new System.Drawing.Size(79, 19);
            this.btn조회.TabIndex = 183;
            this.btn조회.TabStop = false;
            this.btn조회.Text = "조  회";
            this.btn조회.UseVisualStyleBackColor = false;
            // 
            // txt성명
            // 
            this.txt성명.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.txt성명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt성명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt성명.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt성명.Enabled = false;
            this.txt성명.Font = new System.Drawing.Font("굴림", 9F);
            this.txt성명.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txt성명.Location = new System.Drawing.Point(507, 4);
            this.txt성명.MaxLength = 10;
            this.txt성명.Name = "txt성명";
            this.txt성명.Size = new System.Drawing.Size(92, 21);
            this.txt성명.TabIndex = 182;
            this.txt성명.Tag = "NM_KOR";
            this.txt성명.UseKeyF3 = false;
            // 
            // lbl이름
            // 
            this.lbl이름.Location = new System.Drawing.Point(426, 7);
            this.lbl이름.Name = "lbl이름";
            this.lbl이름.Size = new System.Drawing.Size(80, 16);
            this.lbl이름.TabIndex = 181;
            this.lbl이름.Tag = "";
            this.lbl이름.Text = "성명";
            this.lbl이름.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt사번
            // 
            this.txt사번.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.txt사번.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt사번.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt사번.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt사번.Enabled = false;
            this.txt사번.Font = new System.Drawing.Font("굴림", 9F);
            this.txt사번.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txt사번.Location = new System.Drawing.Point(328, 4);
            this.txt사번.MaxLength = 10;
            this.txt사번.Name = "txt사번";
            this.txt사번.Size = new System.Drawing.Size(92, 21);
            this.txt사번.TabIndex = 180;
            this.txt사번.Tag = "NO_EMP";
            this.txt사번.UseKeyF3 = false;
            // 
            // lbl사번
            // 
            this.lbl사번.Location = new System.Drawing.Point(247, 7);
            this.lbl사번.Name = "lbl사번";
            this.lbl사번.Size = new System.Drawing.Size(80, 16);
            this.lbl사번.TabIndex = 179;
            this.lbl사번.Tag = "";
            this.lbl사번.Text = "사번";
            this.lbl사번.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(155, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 18);
            this.label13.TabIndex = 178;
            this.label13.Text = "~";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtp시작
            // 
            this.dtp시작.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp시작.Location = new System.Drawing.Point(83, 4);
            this.dtp시작.Mask = "####/##";
            this.dtp시작.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp시작.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp시작.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp시작.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp시작.Name = "dtp시작";
            this.dtp시작.ShowUpDown = true;
            this.dtp시작.Size = new System.Drawing.Size(71, 21);
            this.dtp시작.TabIndex = 176;
            this.dtp시작.Value = new System.DateTime(((long)(0)));
            // 
            // dtp종료
            // 
            this.dtp종료.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp종료.Location = new System.Drawing.Point(170, 4);
            this.dtp종료.Mask = "####/##";
            this.dtp종료.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp종료.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp종료.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp종료.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp종료.Name = "dtp종료";
            this.dtp종료.ShowUpDown = true;
            this.dtp종료.Size = new System.Drawing.Size(71, 21);
            this.dtp종료.TabIndex = 177;
            this.dtp종료.Value = new System.DateTime(((long)(0)));
            // 
            // lbl조회년월
            // 
            this.lbl조회년월.Location = new System.Drawing.Point(2, 7);
            this.lbl조회년월.Name = "lbl조회년월";
            this.lbl조회년월.Size = new System.Drawing.Size(80, 16);
            this.lbl조회년월.TabIndex = 175;
            this.lbl조회년월.Tag = "";
            this.lbl조회년월.Text = "조회년월";
            this.lbl조회년월.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // imagePanel2
            // 
            this.imagePanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.imagePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.imagePanel2.LeftImage = null;
            this.imagePanel2.Location = new System.Drawing.Point(4, 42);
            this.imagePanel2.Name = "imagePanel2";
            this.imagePanel2.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
            this.imagePanel2.PatternImage = null;
            this.imagePanel2.RightImage = null;
            this.imagePanel2.Size = new System.Drawing.Size(709, 24);
            this.imagePanel2.TabIndex = 7;
            this.imagePanel2.TitleText = "교육현황";
            // 
            // imagePanel1
            // 
            this.imagePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.imagePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.imagePanel1.LeftImage = null;
            this.imagePanel1.Location = new System.Drawing.Point(4, 247);
            this.imagePanel1.Name = "imagePanel1";
            this.imagePanel1.PanelStyle = Duzon.Common.Controls.ImagePanel.Style.SubTitle;
            this.imagePanel1.PatternImage = null;
            this.imagePanel1.RightImage = null;
            this.imagePanel1.Size = new System.Drawing.Size(709, 24);
            this.imagePanel1.TabIndex = 10;
            this.imagePanel1.TitleText = "포상 / 징계 현황";
            // 
            // _flex1
            // 
            this._flex1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex1.AutoResize = false;
            this._flex1.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex1.EnabledHeaderCheck = true;
            this._flex1.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex1.Location = new System.Drawing.Point(4, 73);
            this._flex1.Name = "_flex1";
            this._flex1.Rows.Count = 1;
            this._flex1.Rows.DefaultSize = 18;
            this._flex1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex1.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex1.ShowSort = false;
            this._flex1.Size = new System.Drawing.Size(709, 167);
            this._flex1.StyleInfo = resources.GetString("_flex1.StyleInfo");
            this._flex1.TabIndex = 11;
            this._flex1.UseGridCalculator = true;
            // 
            // P_CZ_HR_PEVALU_DLG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(726, 503);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_CZ_HR_PEVALU_DLG";
            this.TitleText = "개인별 관련정보";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex2)).EndInit();
            this.panelExt1.ResumeLayout(false);
            this.panelExt1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp시작)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp종료)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex1)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Duzon.Common.Controls.TextBoxExt txt성명;
        private Duzon.Common.Controls.LabelExt lbl이름;
        private Duzon.Common.Controls.TextBoxExt txt사번;
        private Duzon.Common.Controls.LabelExt lbl사번;
        private Duzon.Common.Controls.LabelExt label13;
        private Duzon.Common.Controls.DatePicker dtp시작;
        private Duzon.Common.Controls.DatePicker dtp종료;
        private Duzon.Common.Controls.LabelExt lbl조회년월;
        private Dass.FlexGrid.FlexGrid _flex2;
        private Duzon.Common.Controls.ImagePanel imagePanel2;
        private Duzon.Common.Controls.ImagePanel imagePanel1;
        private Dass.FlexGrid.FlexGrid _flex1;
        #endregion
    }
}
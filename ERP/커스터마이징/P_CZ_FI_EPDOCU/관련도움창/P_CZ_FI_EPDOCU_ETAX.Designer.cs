namespace cz
{
    partial class P_CZ_FI_EPDOCU_ETAX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_FI_EPDOCU_ETAX));
            this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn증빙유형 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn결의코드 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn데이타 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.flexibleRoundedCornerBox1 = new Duzon.Common.Controls.FlexibleRoundedCornerBox();
            this.m_DT = new Duzon.Common.Controls.PeriodPicker();
            this.m_cbo처리구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.m_bp사업장 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.m_cbo과세구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl처리구분 = new Duzon.Common.Controls.LabelExt();
            this.lbl과세구분 = new Duzon.Common.Controls.LabelExt();
            this.lbl사업장 = new Duzon.Common.Controls.LabelExt();
            this.lbl발생일자 = new Duzon.Common.Controls.LabelExt();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.flexibleRoundedCornerBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn확인
            // 
            this.btn확인.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn확인.BackColor = System.Drawing.Color.White;
            this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn확인.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn확인.Location = new System.Drawing.Point(791, 53);
            this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn확인.Name = "btn확인";
            this.btn확인.Size = new System.Drawing.Size(62, 19);
            this.btn확인.TabIndex = 114;
            this.btn확인.TabStop = false;
            this.btn확인.Text = "확인";
            this.btn확인.UseVisualStyleBackColor = true;
            this.btn확인.Click += new System.EventHandler(this.btn확인_Click);
            // 
            // btn취소
            // 
            this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn취소.BackColor = System.Drawing.Color.White;
            this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn취소.Location = new System.Drawing.Point(856, 53);
            this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn취소.Name = "btn취소";
            this.btn취소.Size = new System.Drawing.Size(62, 19);
            this.btn취소.TabIndex = 115;
            this.btn취소.TabStop = false;
            this.btn취소.Text = "취소";
            this.btn취소.UseVisualStyleBackColor = true;
            // 
            // btn증빙유형
            // 
            this.btn증빙유형.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn증빙유형.BackColor = System.Drawing.Color.White;
            this.btn증빙유형.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn증빙유형.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn증빙유형.Location = new System.Drawing.Point(611, 53);
            this.btn증빙유형.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn증빙유형.Name = "btn증빙유형";
            this.btn증빙유형.Size = new System.Drawing.Size(112, 19);
            this.btn증빙유형.TabIndex = 120;
            this.btn증빙유형.TabStop = false;
            this.btn증빙유형.Text = "증빙유형일괄적용";
            this.btn증빙유형.UseVisualStyleBackColor = true;
            this.btn증빙유형.Click += new System.EventHandler(this.btn증빙유형_Click);
            // 
            // btn결의코드
            // 
            this.btn결의코드.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn결의코드.BackColor = System.Drawing.Color.White;
            this.btn결의코드.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn결의코드.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn결의코드.Location = new System.Drawing.Point(496, 53);
            this.btn결의코드.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn결의코드.Name = "btn결의코드";
            this.btn결의코드.Size = new System.Drawing.Size(112, 19);
            this.btn결의코드.TabIndex = 121;
            this.btn결의코드.TabStop = false;
            this.btn결의코드.Text = "결의코드일괄적용";
            this.btn결의코드.UseVisualStyleBackColor = true;
            this.btn결의코드.Click += new System.EventHandler(this.btn결의코드_Click);
            // 
            // btn데이타
            // 
            this.btn데이타.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn데이타.BackColor = System.Drawing.Color.White;
            this.btn데이타.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn데이타.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn데이타.Location = new System.Drawing.Point(381, 53);
            this.btn데이타.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn데이타.Name = "btn데이타";
            this.btn데이타.Size = new System.Drawing.Size(112, 19);
            this.btn데이타.TabIndex = 122;
            this.btn데이타.TabStop = false;
            this.btn데이타.Text = "데이타내려받기";
            this.btn데이타.UseVisualStyleBackColor = true;
            this.btn데이타.Click += new System.EventHandler(this.btn데이타_Click);
            // 
            // btn조회
            // 
            this.btn조회.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn조회.BackColor = System.Drawing.Color.White;
            this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn조회.Location = new System.Drawing.Point(726, 53);
            this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn조회.Name = "btn조회";
            this.btn조회.Size = new System.Drawing.Size(62, 19);
            this.btn조회.TabIndex = 123;
            this.btn조회.TabStop = false;
            this.btn조회.Text = "조회";
            this.btn조회.UseVisualStyleBackColor = true;
            this.btn조회.Click += new System.EventHandler(this.btn조회_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flexibleRoundedCornerBox1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 81);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(916, 509);
            this.tableLayoutPanel1.TabIndex = 124;
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.Font = new System.Drawing.Font("굴림", 9F);
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(3, 69);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(910, 437);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 0;
            this._flex.UseGridCalculator = true;
            // 
            // flexibleRoundedCornerBox1
            // 
            this.flexibleRoundedCornerBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flexibleRoundedCornerBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
            this.flexibleRoundedCornerBox1.Controls.Add(this.m_DT);
            this.flexibleRoundedCornerBox1.Controls.Add(this.m_cbo처리구분);
            this.flexibleRoundedCornerBox1.Controls.Add(this.m_bp사업장);
            this.flexibleRoundedCornerBox1.Controls.Add(this.m_cbo과세구분);
            this.flexibleRoundedCornerBox1.Controls.Add(this.lbl처리구분);
            this.flexibleRoundedCornerBox1.Controls.Add(this.lbl과세구분);
            this.flexibleRoundedCornerBox1.Controls.Add(this.lbl사업장);
            this.flexibleRoundedCornerBox1.Controls.Add(this.lbl발생일자);
            this.flexibleRoundedCornerBox1.Location = new System.Drawing.Point(3, 3);
            this.flexibleRoundedCornerBox1.Name = "flexibleRoundedCornerBox1";
            this.flexibleRoundedCornerBox1.Size = new System.Drawing.Size(910, 60);
            this.flexibleRoundedCornerBox1.TabIndex = 1;
            // 
            // m_DT
            // 
            this.m_DT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_DT.IsNecessaryCondition = true;
            this.m_DT.Location = new System.Drawing.Point(377, 7);
            this.m_DT.MaximumSize = new System.Drawing.Size(185, 21);
            this.m_DT.MinimumSize = new System.Drawing.Size(185, 21);
            this.m_DT.Name = "m_DT";
            this.m_DT.Size = new System.Drawing.Size(185, 21);
            this.m_DT.TabIndex = 122;
            // 
            // m_cbo처리구분
            // 
            this.m_cbo처리구분.AutoDropDown = true;
            this.m_cbo처리구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbo처리구분.ItemHeight = 12;
            this.m_cbo처리구분.Location = new System.Drawing.Point(120, 32);
            this.m_cbo처리구분.Name = "m_cbo처리구분";
            this.m_cbo처리구분.Size = new System.Drawing.Size(170, 20);
            this.m_cbo처리구분.TabIndex = 119;
            this.m_cbo처리구분.UseKeyF3 = false;
            // 
            // m_bp사업장
            // 
            this.m_bp사업장.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_BIZAREA_SUB;
            this.m_bp사업장.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.m_bp사업장.Location = new System.Drawing.Point(120, 8);
            this.m_bp사업장.Name = "m_bp사업장";
            this.m_bp사업장.SetDefaultValue = true;
            this.m_bp사업장.Size = new System.Drawing.Size(170, 21);
            this.m_bp사업장.TabIndex = 118;
            this.m_bp사업장.TabStop = false;
            // 
            // m_cbo과세구분
            // 
            this.m_cbo과세구분.AutoDropDown = true;
            this.m_cbo과세구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbo과세구분.ItemHeight = 12;
            this.m_cbo과세구분.Location = new System.Drawing.Point(676, 8);
            this.m_cbo과세구분.Name = "m_cbo과세구분";
            this.m_cbo과세구분.Size = new System.Drawing.Size(84, 20);
            this.m_cbo과세구분.TabIndex = 116;
            // 
            // lbl처리구분
            // 
            this.lbl처리구분.BackColor = System.Drawing.Color.Transparent;
            this.lbl처리구분.Location = new System.Drawing.Point(18, 33);
            this.lbl처리구분.Name = "lbl처리구분";
            this.lbl처리구분.Size = new System.Drawing.Size(97, 18);
            this.lbl처리구분.TabIndex = 9;
            this.lbl처리구분.Tag = "ST_BILL";
            this.lbl처리구분.Text = "처리구분";
            this.lbl처리구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl과세구분
            // 
            this.lbl과세구분.BackColor = System.Drawing.Color.Transparent;
            this.lbl과세구분.Location = new System.Drawing.Point(574, 9);
            this.lbl과세구분.Name = "lbl과세구분";
            this.lbl과세구분.Size = new System.Drawing.Size(97, 18);
            this.lbl과세구분.TabIndex = 8;
            this.lbl과세구분.Tag = "ST_BILL";
            this.lbl과세구분.Text = "과세구분";
            this.lbl과세구분.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl사업장
            // 
            this.lbl사업장.AutoEllipsis = true;
            this.lbl사업장.BackColor = System.Drawing.Color.Transparent;
            this.lbl사업장.Location = new System.Drawing.Point(18, 8);
            this.lbl사업장.Name = "lbl사업장";
            this.lbl사업장.Size = new System.Drawing.Size(97, 18);
            this.lbl사업장.TabIndex = 2;
            this.lbl사업장.Tag = "";
            this.lbl사업장.Text = "사업장(부가세)";
            this.lbl사업장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl발생일자
            // 
            this.lbl발생일자.BackColor = System.Drawing.Color.Transparent;
            this.lbl발생일자.Location = new System.Drawing.Point(298, 9);
            this.lbl발생일자.Name = "lbl발생일자";
            this.lbl발생일자.Size = new System.Drawing.Size(73, 18);
            this.lbl발생일자.TabIndex = 1;
            this.lbl발생일자.Tag = "";
            this.lbl발생일자.Text = "발생일자";
            this.lbl발생일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // P_CZ_FI_EPDOCU_ETAX
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(923, 595);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btn조회);
            this.Controls.Add(this.btn데이타);
            this.Controls.Add(this.btn결의코드);
            this.Controls.Add(this.btn증빙유형);
            this.Controls.Add(this.btn취소);
            this.Controls.Add(this.btn확인);
            this.Name = "P_CZ_FI_EPDOCU_ETAX";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ERP - iU";
            this.TitleText = "전자세금계산서처리";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.flexibleRoundedCornerBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Duzon.Common.Controls.RoundedButton btn확인;
        private Duzon.Common.Controls.RoundedButton btn취소;
        private Duzon.Common.Controls.RoundedButton btn증빙유형;
        private Duzon.Common.Controls.RoundedButton btn결의코드;
        private Duzon.Common.Controls.RoundedButton btn데이타;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.Controls.FlexibleRoundedCornerBox flexibleRoundedCornerBox1;
        private Duzon.Common.Controls.DropDownComboBox m_cbo처리구분;
        private Duzon.Common.BpControls.BpCodeTextBox m_bp사업장;
        private Duzon.Common.Controls.DropDownComboBox m_cbo과세구분;
        private Duzon.Common.Controls.LabelExt lbl처리구분;
        private Duzon.Common.Controls.LabelExt lbl과세구분;
        private Duzon.Common.Controls.LabelExt lbl사업장;
        private Duzon.Common.Controls.LabelExt lbl발생일자;
        private Duzon.Common.Controls.PeriodPicker m_DT;
        #endregion
    }
}
namespace cz
{
    partial class P_CZ_MA_EMAIL_SUB1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MA_EMAIL_SUB1));
            this.m_pnlMain = new Duzon.Common.Controls.PanelExt();
            this._lbl내용 = new Duzon.Common.Controls.LabelExt();
            this._lbl보내는사람 = new Duzon.Common.Controls.LabelExt();
            this._lbl제목 = new Duzon.Common.Controls.LabelExt();
            this._txt내용 = new System.Windows.Forms.RichTextBox();
            this._txt제목 = new Duzon.Common.Controls.TextBoxExt();
            this._txt보내는사람 = new Duzon.Common.Controls.TextBoxExt();
            this.panel10 = new Duzon.Common.Controls.PanelExt();
            this.panel9 = new Duzon.Common.Controls.PanelExt();
            this.panel8 = new Duzon.Common.Controls.PanelExt();
            this.flexPart = new Dass.FlexGrid.FlexGrid(this.components);
            this.btnSendMail = new Duzon.Common.Controls.RoundedButton(this.components);
            this.cbo_Print = new Duzon.Common.Controls.DropDownComboBox();
            this.btnAdd = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btnFileUpload = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tb_mail_add = new Duzon.Common.Controls.TabControlExt();
            this.tb_partner = new System.Windows.Forms.TabPage();
            this.tb_emp = new System.Windows.Forms.TabPage();
            this.flexEmp = new Dass.FlexGrid.FlexGrid(this.components);
            this.tb_ref = new System.Windows.Forms.TabPage();
            this.flexRef = new Dass.FlexGrid.FlexGrid(this.components);
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnDel = new Duzon.Common.Controls.RoundedButton(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rdo_outlook = new System.Windows.Forms.RadioButton();
            this.rdo_smtp = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.m_pnlMain.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flexPart)).BeginInit();
            this.tb_mail_add.SuspendLayout();
            this.tb_partner.SuspendLayout();
            this.tb_emp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flexEmp)).BeginInit();
            this.tb_ref.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flexRef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.Controls.Add(this._lbl내용);
            this.m_pnlMain.Controls.Add(this._lbl보내는사람);
            this.m_pnlMain.Controls.Add(this._lbl제목);
            this.m_pnlMain.Controls.Add(this._txt내용);
            this.m_pnlMain.Controls.Add(this._txt제목);
            this.m_pnlMain.Controls.Add(this._txt보내는사람);
            this.m_pnlMain.Controls.Add(this.panel10);
            this.m_pnlMain.Controls.Add(this.panel9);
            this.m_pnlMain.Location = new System.Drawing.Point(7, 93);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(652, 225);
            this.m_pnlMain.TabIndex = 163;
            // 
            // _lbl내용
            // 
            this._lbl내용.BackColor = System.Drawing.Color.Transparent;
            this._lbl내용.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold);
            this._lbl내용.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(129)))), ((int)(((byte)(172)))));
            this._lbl내용.Location = new System.Drawing.Point(5, 71);
            this._lbl내용.Name = "_lbl내용";
            this._lbl내용.Size = new System.Drawing.Size(85, 18);
            this._lbl내용.TabIndex = 0;
            this._lbl내용.Tag = "";
            this._lbl내용.Text = "메일내용";
            this._lbl내용.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lbl보내는사람
            // 
            this._lbl보내는사람.BackColor = System.Drawing.Color.Transparent;
            this._lbl보내는사람.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold);
            this._lbl보내는사람.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(129)))), ((int)(((byte)(172)))));
            this._lbl보내는사람.Location = new System.Drawing.Point(5, 11);
            this._lbl보내는사람.Name = "_lbl보내는사람";
            this._lbl보내는사람.Size = new System.Drawing.Size(85, 19);
            this._lbl보내는사람.TabIndex = 3;
            this._lbl보내는사람.Tag = "";
            this._lbl보내는사람.Text = "보내는 사람";
            this._lbl보내는사람.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lbl제목
            // 
            this._lbl제목.BackColor = System.Drawing.Color.Transparent;
            this._lbl제목.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Bold);
            this._lbl제목.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(129)))), ((int)(((byte)(172)))));
            this._lbl제목.Location = new System.Drawing.Point(5, 41);
            this._lbl제목.Name = "_lbl제목";
            this._lbl제목.Size = new System.Drawing.Size(85, 19);
            this._lbl제목.TabIndex = 0;
            this._lbl제목.Tag = "";
            this._lbl제목.Text = "제목";
            this._lbl제목.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _txt내용
            // 
            this._txt내용.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txt내용.Location = new System.Drawing.Point(100, 73);
            this._txt내용.Name = "_txt내용";
            this._txt내용.Size = new System.Drawing.Size(526, 137);
            this._txt내용.TabIndex = 2;
            this._txt내용.Text = "";
            // 
            // _txt제목
            // 
            this._txt제목.BackColor = System.Drawing.Color.White;
            this._txt제목.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this._txt제목.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txt제목.Location = new System.Drawing.Point(100, 40);
            this._txt제목.Name = "_txt제목";
            this._txt제목.Size = new System.Drawing.Size(526, 21);
            this._txt제목.TabIndex = 1;
            this._txt제목.Tag = "";
            // 
            // _txt보내는사람
            // 
            this._txt보내는사람.BackColor = System.Drawing.Color.White;
            this._txt보내는사람.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this._txt보내는사람.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txt보내는사람.Location = new System.Drawing.Point(100, 10);
            this._txt보내는사람.Name = "_txt보내는사람";
            this._txt보내는사람.Size = new System.Drawing.Size(526, 21);
            this._txt보내는사람.TabIndex = 0;
            this._txt보내는사람.Tag = "";
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.BackColor = System.Drawing.Color.Transparent;
            this.panel10.Location = new System.Drawing.Point(5, 55);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(644, 1);
            this.panel10.TabIndex = 18;
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.Controls.Add(this.panel8);
            this.panel9.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel9.Location = new System.Drawing.Point(5, 27);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(644, 1);
            this.panel9.TabIndex = 17;
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.Font = new System.Drawing.Font("굴림체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel8.Location = new System.Drawing.Point(0, 16);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(644, 1);
            this.panel8.TabIndex = 18;
            // 
            // flexPart
            // 
            this.flexPart.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this.flexPart.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.flexPart.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.flexPart.AutoResize = false;
            this.flexPart.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this.flexPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flexPart.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flexPart.EnabledHeaderCheck = true;
            this.flexPart.Font = new System.Drawing.Font("굴림", 9F);
            this.flexPart.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.flexPart.Location = new System.Drawing.Point(3, 3);
            this.flexPart.Name = "flexPart";
            this.flexPart.Rows.Count = 1;
            this.flexPart.Rows.DefaultSize = 20;
            this.flexPart.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flexPart.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.flexPart.ShowSort = false;
            this.flexPart.Size = new System.Drawing.Size(638, 225);
            this.flexPart.StyleInfo = resources.GetString("flexPart.StyleInfo");
            this.flexPart.TabIndex = 171;
            this.flexPart.UseGridCalculator = true;
            // 
            // btnSendMail
            // 
            this.btnSendMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendMail.BackColor = System.Drawing.Color.White;
            this.btnSendMail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendMail.Location = new System.Drawing.Point(569, 324);
            this.btnSendMail.MaximumSize = new System.Drawing.Size(0, 19);
            this.btnSendMail.Name = "btnSendMail";
            this.btnSendMail.Size = new System.Drawing.Size(90, 19);
            this.btnSendMail.TabIndex = 172;
            this.btnSendMail.TabStop = false;
            this.btnSendMail.Text = "메일보내기";
            this.btnSendMail.UseVisualStyleBackColor = false;
            // 
            // cbo_Print
            // 
            this.cbo_Print.AutoDropDown = true;
            this.cbo_Print.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Print.FormattingEnabled = true;
            this.cbo_Print.ItemHeight = 12;
            this.cbo_Print.Location = new System.Drawing.Point(7, 67);
            this.cbo_Print.Name = "cbo_Print";
            this.cbo_Print.Size = new System.Drawing.Size(197, 20);
            this.cbo_Print.TabIndex = 173;
            this.cbo_Print.SelectedValueChanged += new System.EventHandler(this.cbo_Print_SelectedValueChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(7, 324);
            this.btnAdd.MaximumSize = new System.Drawing.Size(0, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 19);
            this.btnAdd.TabIndex = 174;
            this.btnAdd.TabStop = false;
            this.btnAdd.Text = "추가";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnFileUpload
            // 
            this.btnFileUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFileUpload.BackColor = System.Drawing.Color.White;
            this.btnFileUpload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFileUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFileUpload.Location = new System.Drawing.Point(477, 324);
            this.btnFileUpload.MaximumSize = new System.Drawing.Size(0, 19);
            this.btnFileUpload.Name = "btnFileUpload";
            this.btnFileUpload.Size = new System.Drawing.Size(90, 19);
            this.btnFileUpload.TabIndex = 175;
            this.btnFileUpload.TabStop = false;
            this.btnFileUpload.Text = "파일첨부";
            this.btnFileUpload.UseVisualStyleBackColor = false;
            // 
            // tb_mail_add
            // 
            this.tb_mail_add.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_mail_add.Controls.Add(this.tb_partner);
            this.tb_mail_add.Controls.Add(this.tb_emp);
            this.tb_mail_add.Controls.Add(this.tb_ref);
            this.tb_mail_add.ImageList = this.imageList;
            this.tb_mail_add.Location = new System.Drawing.Point(7, 349);
            this.tb_mail_add.Name = "tb_mail_add";
            this.tb_mail_add.SelectedIndex = 0;
            this.tb_mail_add.Size = new System.Drawing.Size(652, 258);
            this.tb_mail_add.TabIndex = 176;
            // 
            // tb_partner
            // 
            this.tb_partner.Controls.Add(this.flexPart);
            this.tb_partner.ImageIndex = 0;
            this.tb_partner.Location = new System.Drawing.Point(4, 23);
            this.tb_partner.Name = "tb_partner";
            this.tb_partner.Padding = new System.Windows.Forms.Padding(3);
            this.tb_partner.Size = new System.Drawing.Size(644, 231);
            this.tb_partner.TabIndex = 0;
            this.tb_partner.Text = "거래처담당자";
            this.tb_partner.UseVisualStyleBackColor = true;
            // 
            // tb_emp
            // 
            this.tb_emp.Controls.Add(this.flexEmp);
            this.tb_emp.ImageIndex = 1;
            this.tb_emp.Location = new System.Drawing.Point(4, 23);
            this.tb_emp.Name = "tb_emp";
            this.tb_emp.Padding = new System.Windows.Forms.Padding(3);
            this.tb_emp.Size = new System.Drawing.Size(644, 231);
            this.tb_emp.TabIndex = 1;
            this.tb_emp.Text = "사내담당자";
            this.tb_emp.UseVisualStyleBackColor = true;
            // 
            // flexEmp
            // 
            this.flexEmp.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this.flexEmp.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.flexEmp.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.flexEmp.AutoResize = false;
            this.flexEmp.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this.flexEmp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flexEmp.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flexEmp.EnabledHeaderCheck = true;
            this.flexEmp.Font = new System.Drawing.Font("굴림", 9F);
            this.flexEmp.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.flexEmp.Location = new System.Drawing.Point(3, 3);
            this.flexEmp.Name = "flexEmp";
            this.flexEmp.Rows.Count = 1;
            this.flexEmp.Rows.DefaultSize = 20;
            this.flexEmp.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flexEmp.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.flexEmp.ShowSort = false;
            this.flexEmp.Size = new System.Drawing.Size(638, 225);
            this.flexEmp.StyleInfo = resources.GetString("flexEmp.StyleInfo");
            this.flexEmp.TabIndex = 173;
            this.flexEmp.UseGridCalculator = true;
            // 
            // tb_ref
            // 
            this.tb_ref.Controls.Add(this.flexRef);
            this.tb_ref.ImageIndex = 1;
            this.tb_ref.Location = new System.Drawing.Point(4, 23);
            this.tb_ref.Name = "tb_ref";
            this.tb_ref.Size = new System.Drawing.Size(644, 231);
            this.tb_ref.TabIndex = 2;
            this.tb_ref.Text = "참조자";
            this.tb_ref.UseVisualStyleBackColor = true;
            // 
            // flexRef
            // 
            this.flexRef.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this.flexRef.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.flexRef.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.flexRef.AutoResize = false;
            this.flexRef.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this.flexRef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flexRef.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flexRef.EnabledHeaderCheck = true;
            this.flexRef.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this.flexRef.Location = new System.Drawing.Point(0, 0);
            this.flexRef.Name = "flexRef";
            this.flexRef.Rows.Count = 1;
            this.flexRef.Rows.DefaultSize = 18;
            this.flexRef.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flexRef.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.flexRef.ShowSort = false;
            this.flexRef.Size = new System.Drawing.Size(644, 231);
            this.flexRef.StyleInfo = resources.GetString("flexRef.StyleInfo");
            this.flexRef.TabIndex = 0;
            this.flexRef.UseGridCalculator = true;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.BackColor = System.Drawing.Color.White;
            this.btnDel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Location = new System.Drawing.Point(99, 324);
            this.btnDel.MaximumSize = new System.Drawing.Size(0, 19);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(90, 19);
            this.btnDel.TabIndex = 177;
            this.btnDel.TabStop = false;
            this.btnDel.Text = "삭제";
            this.btnDel.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(666, 61);
            this.pictureBox1.TabIndex = 178;
            this.pictureBox1.TabStop = false;
            // 
            // rdo_outlook
            // 
            this.rdo_outlook.AutoSize = true;
            this.rdo_outlook.Location = new System.Drawing.Point(594, 68);
            this.rdo_outlook.Name = "rdo_outlook";
            this.rdo_outlook.Size = new System.Drawing.Size(65, 16);
            this.rdo_outlook.TabIndex = 179;
            this.rdo_outlook.Text = "OUTLOOK";
            this.rdo_outlook.UseVisualStyleBackColor = true;
            // 
            // rdo_smtp
            // 
            this.rdo_smtp.AutoSize = true;
            this.rdo_smtp.Checked = true;
            this.rdo_smtp.Location = new System.Drawing.Point(541, 68);
            this.rdo_smtp.Name = "rdo_smtp";
            this.rdo_smtp.Size = new System.Drawing.Size(47, 16);
            this.rdo_smtp.TabIndex = 180;
            this.rdo_smtp.TabStop = true;
            this.rdo_smtp.Text = "SMTP";
            this.rdo_smtp.UseVisualStyleBackColor = true;
            // 
            // P_CZ_MF_EMAIL
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(666, 619);
            this.Controls.Add(this.rdo_smtp);
            this.Controls.Add(this.rdo_outlook);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tb_mail_add);
            this.Controls.Add(this.btnFileUpload);
            this.Controls.Add(this.cbo_Print);
            this.Controls.Add(this.m_pnlMain);
            this.Controls.Add(this.btnSendMail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "P_CZ_MF_EMAIL";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.m_pnlMain.ResumeLayout(false);
            this.m_pnlMain.PerformLayout();
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flexPart)).EndInit();
            this.tb_mail_add.ResumeLayout(false);
            this.tb_partner.ResumeLayout(false);
            this.tb_emp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flexEmp)).EndInit();
            this.tb_ref.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flexRef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Duzon.Common.Controls.PanelExt m_pnlMain;
        private Duzon.Common.Controls.PanelExt panel10;
        private Duzon.Common.Controls.PanelExt panel9;
        private Duzon.Common.Controls.PanelExt panel8;
        private Duzon.Common.Controls.LabelExt _lbl보내는사람;
        private Duzon.Common.Controls.LabelExt _lbl내용;
        private Duzon.Common.Controls.TextBoxExt _txt보내는사람;
        private Duzon.Common.Controls.LabelExt _lbl제목;
        private Duzon.Common.Controls.TextBoxExt _txt제목;
        private Dass.FlexGrid.FlexGrid flexPart;
        private Duzon.Common.Controls.RoundedButton btnSendMail;
        private Duzon.Common.Controls.DropDownComboBox cbo_Print;
        private Duzon.Common.Controls.RoundedButton btnAdd;
        private Duzon.Common.Controls.RoundedButton btnFileUpload;
        private Duzon.Common.Controls.TabControlExt tb_mail_add;
        private System.Windows.Forms.TabPage tb_partner;
        private System.Windows.Forms.TabPage tb_emp;
        private Dass.FlexGrid.FlexGrid flexEmp;
        private Duzon.Common.Controls.RoundedButton btnDel;
        private System.Windows.Forms.RichTextBox _txt내용;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TabPage tb_ref;
        private Dass.FlexGrid.FlexGrid flexRef;
        private System.Windows.Forms.RadioButton rdo_outlook;
        private System.Windows.Forms.RadioButton rdo_smtp;

        #endregion
    }
}
namespace sale
{
    partial class P_SA_Z_VPHI_PMS_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_Z_VPHI_PMS_SUB));
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.txt인보이스번호 = new Duzon.Common.Controls.TextBoxExt();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.cboPACKING구분 = new Duzon.Common.Controls.DropDownComboBox();
            this.txtPMS번호 = new Duzon.Common.Controls.TextBoxExt();
            this.bp거래처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.dtp일자to = new Duzon.Common.Controls.DatePicker();
            this.dtp일자from = new Duzon.Common.Controls.DatePicker();
            this.panel6 = new Duzon.Common.Controls.PanelExt();
            this.ctx공장 = new Duzon.Common.Controls.CodeTextBox(this.components);
            this.label6 = new Duzon.Common.Controls.LabelExt();
            this.panel2 = new Duzon.Common.Controls.PanelExt();
            this.label1 = new Duzon.Common.Controls.LabelExt();
            this.label3 = new Duzon.Common.Controls.LabelExt();
            this.panel1 = new Duzon.Common.Controls.PanelExt();
            this.lbl인보이스번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl일자 = new Duzon.Common.Controls.LabelExt();
            this.m_lblCdPartner = new Duzon.Common.Controls.LabelExt();
            this.panel7 = new Duzon.Common.Controls.PanelExt();
            this.m_lblPlantGir = new Duzon.Common.Controls.LabelExt();
            this.lblPMS번호 = new Duzon.Common.Controls.LabelExt();
            this.ctx거래구분 = new Duzon.Common.Controls.CodeTextBox(this.components);
            this.m_btnQuery = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnApply = new Duzon.Common.Controls.RoundedButton(this.components);
            this.m_btnCancel = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.panelExt3 = new Duzon.Common.Controls.PanelExt();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp일자to)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp일자from)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.panelExt3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.txt인보이스번호);
            this.panel4.Controls.Add(this.panelExt1);
            this.panel4.Controls.Add(this.cboPACKING구분);
            this.panel4.Controls.Add(this.txtPMS번호);
            this.panel4.Controls.Add(this.bp거래처);
            this.panel4.Controls.Add(this.dtp일자to);
            this.panel4.Controls.Add(this.dtp일자from);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.ctx공장);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.ctx거래구분);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(796, 88);
            this.panel4.TabIndex = 0;
            // 
            // txt인보이스번호
            // 
            this.txt인보이스번호.BackColor = System.Drawing.SystemColors.Window;
            this.txt인보이스번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt인보이스번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt인보이스번호.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt인보이스번호.Location = new System.Drawing.Point(93, 61);
            this.txt인보이스번호.MaxLength = 50;
            this.txt인보이스번호.Name = "txt인보이스번호";
            this.txt인보이스번호.SelectedAllEnabled = false;
            this.txt인보이스번호.Size = new System.Drawing.Size(187, 21);
            this.txt인보이스번호.TabIndex = 43;
            this.txt인보이스번호.Tag = "NO_PO_PARTNER";
            this.txt인보이스번호.UseKeyEnter = false;
            this.txt인보이스번호.UseKeyF3 = false;
            // 
            // panelExt1
            // 
            this.panelExt1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt1.BackColor = System.Drawing.Color.Transparent;
            this.panelExt1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt1.BackgroundImage")));
            this.panelExt1.Location = new System.Drawing.Point(4, 56);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(786, 1);
            this.panelExt1.TabIndex = 42;
            // 
            // cboPACKING구분
            // 
            this.cboPACKING구분.AutoDropDown = true;
            this.cboPACKING구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.cboPACKING구분.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPACKING구분.ItemHeight = 12;
            this.cboPACKING구분.Location = new System.Drawing.Point(633, 31);
            this.cboPACKING구분.Name = "cboPACKING구분";
            this.cboPACKING구분.ShowCheckBox = false;
            this.cboPACKING구분.Size = new System.Drawing.Size(158, 20);
            this.cboPACKING구분.TabIndex = 41;
            this.cboPACKING구분.Tag = "TP_BUSI";
            this.cboPACKING구분.UseKeyEnter = false;
            this.cboPACKING구분.UseKeyF3 = false;
            // 
            // txtPMS번호
            // 
            this.txtPMS번호.BackColor = System.Drawing.SystemColors.Window;
            this.txtPMS번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txtPMS번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPMS번호.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPMS번호.Location = new System.Drawing.Point(363, 31);
            this.txtPMS번호.MaxLength = 50;
            this.txtPMS번호.Name = "txtPMS번호";
            this.txtPMS번호.SelectedAllEnabled = false;
            this.txtPMS번호.Size = new System.Drawing.Size(174, 21);
            this.txtPMS번호.TabIndex = 10;
            this.txtPMS번호.Tag = "NO_PO_PARTNER";
            this.txtPMS번호.UseKeyEnter = false;
            this.txtPMS번호.UseKeyF3 = false;
            // 
            // bp거래처
            // 
            this.bp거래처.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Sub;
            this.bp거래처.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bp거래처.ButtonImage")));
            this.bp거래처.ChildMode = "";
            this.bp거래처.CodeName = "";
            this.bp거래처.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bp거래처.CodeValue = "";
            this.bp거래처.ComboCheck = true;
            this.bp거래처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bp거래처.IsCodeValueToUpper = true;
            this.bp거래처.ItemBackColor = System.Drawing.Color.White;
            this.bp거래처.Location = new System.Drawing.Point(93, 32);
            this.bp거래처.MaximumSize = new System.Drawing.Size(0, 21);
            this.bp거래처.Name = "bp거래처";
            this.bp거래처.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bp거래처.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.bp거래처.SearchCode = true;
            this.bp거래처.SelectCount = 0;
            this.bp거래처.SetDefaultValue = false;
            this.bp거래처.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bp거래처.Size = new System.Drawing.Size(187, 21);
            this.bp거래처.TabIndex = 4;
            this.bp거래처.TabStop = false;
            this.bp거래처.Text = "bpCodeTextBox1";
            // 
            // dtp일자to
            // 
            this.dtp일자to.CalendarBackColor = System.Drawing.Color.White;
            this.dtp일자to.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp일자to.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp일자to.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp일자to.Location = new System.Drawing.Point(195, 3);
            this.dtp일자to.Mask = "####/##/##";
            this.dtp일자to.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp일자to.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp일자to.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp일자to.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp일자to.Modified = false;
            this.dtp일자to.Name = "dtp일자to";
            this.dtp일자to.PaddingCharacter = '_';
            this.dtp일자to.PassivePromptCharacter = '_';
            this.dtp일자to.PromptCharacter = '_';
            this.dtp일자to.SelectedDayColor = System.Drawing.Color.White;
            this.dtp일자to.ShowToDay = true;
            this.dtp일자to.ShowTodayCircle = true;
            this.dtp일자to.ShowUpDown = false;
            this.dtp일자to.Size = new System.Drawing.Size(85, 21);
            this.dtp일자to.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp일자to.TabIndex = 1;
            this.dtp일자to.TabStop = false;
            this.dtp일자to.TitleBackColor = System.Drawing.Color.White;
            this.dtp일자to.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp일자to.ToDayColor = System.Drawing.Color.Red;
            this.dtp일자to.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp일자to.UseKeyF3 = false;
            this.dtp일자to.Value = new System.DateTime(((long)(0)));
            // 
            // dtp일자from
            // 
            this.dtp일자from.CalendarBackColor = System.Drawing.Color.White;
            this.dtp일자from.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp일자from.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp일자from.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtp일자from.Location = new System.Drawing.Point(93, 3);
            this.dtp일자from.Mask = "####/##/##";
            this.dtp일자from.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp일자from.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp일자from.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp일자from.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp일자from.Modified = false;
            this.dtp일자from.Name = "dtp일자from";
            this.dtp일자from.PaddingCharacter = '_';
            this.dtp일자from.PassivePromptCharacter = '_';
            this.dtp일자from.PromptCharacter = '_';
            this.dtp일자from.SelectedDayColor = System.Drawing.Color.White;
            this.dtp일자from.ShowToDay = true;
            this.dtp일자from.ShowTodayCircle = true;
            this.dtp일자from.ShowUpDown = false;
            this.dtp일자from.Size = new System.Drawing.Size(85, 21);
            this.dtp일자from.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtp일자from.TabIndex = 0;
            this.dtp일자from.TabStop = false;
            this.dtp일자from.TitleBackColor = System.Drawing.Color.White;
            this.dtp일자from.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtp일자from.ToDayColor = System.Drawing.Color.Red;
            this.dtp일자from.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp일자from.UseKeyF3 = false;
            this.dtp일자from.Value = new System.DateTime(((long)(0)));
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel6.BackgroundImage")));
            this.panel6.Location = new System.Drawing.Point(5, 27);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(786, 1);
            this.panel6.TabIndex = 25;
            // 
            // ctx공장
            // 
            this.ctx공장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.ctx공장.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.ctx공장.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctx공장.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ctx공장.CodeName = "";
            this.ctx공장.CodeValue = "";
            this.ctx공장.IgnoreTextChanged = false;
            this.ctx공장.IsConfirmed = false;
            this.ctx공장.Location = new System.Drawing.Point(363, 3);
            this.ctx공장.Name = "ctx공장";
            this.ctx공장.ReadOnly = true;
            this.ctx공장.SelectedAllEnabled = false;
            this.ctx공장.Size = new System.Drawing.Size(174, 21);
            this.ctx공장.TabIndex = 2;
            this.ctx공장.TabStop = false;
            this.ctx공장.UseKeyEnter = false;
            this.ctx공장.UseKeyF3 = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(179, 7);
            this.label6.Name = "label6";
            this.label6.Resizeble = true;
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "∼";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(540, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(90, 54);
            this.panel2.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 6);
            this.label1.Name = "label1";
            this.label1.Resizeble = true;
            this.label1.Size = new System.Drawing.Size(73, 18);
            this.label1.TabIndex = 0;
            this.label1.Tag = "";
            this.label1.Text = "거래구분";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 31);
            this.label3.Name = "label3";
            this.label3.Resizeble = true;
            this.label3.Size = new System.Drawing.Size(84, 18);
            this.label3.TabIndex = 0;
            this.label3.Tag = "";
            this.label3.Text = "Packing 구분";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel1.Controls.Add(this.lbl인보이스번호);
            this.panel1.Controls.Add(this.lbl일자);
            this.panel1.Controls.Add(this.m_lblCdPartner);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(89, 104);
            this.panel1.TabIndex = 22;
            // 
            // lbl인보이스번호
            // 
            this.lbl인보이스번호.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lbl인보이스번호.Location = new System.Drawing.Point(5, 61);
            this.lbl인보이스번호.Name = "lbl인보이스번호";
            this.lbl인보이스번호.Resizeble = true;
            this.lbl인보이스번호.Size = new System.Drawing.Size(80, 18);
            this.lbl인보이스번호.TabIndex = 216;
            this.lbl인보이스번호.Tag = "";
            this.lbl인보이스번호.Text = "INVOICE 번호";
            this.lbl인보이스번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl일자
            // 
            this.lbl일자.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lbl일자.Location = new System.Drawing.Point(4, 5);
            this.lbl일자.Name = "lbl일자";
            this.lbl일자.Resizeble = true;
            this.lbl일자.Size = new System.Drawing.Size(81, 18);
            this.lbl일자.TabIndex = 215;
            this.lbl일자.Tag = "";
            this.lbl일자.Text = "Packing 일자";
            this.lbl일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblCdPartner
            // 
            this.m_lblCdPartner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblCdPartner.Location = new System.Drawing.Point(15, 31);
            this.m_lblCdPartner.Name = "m_lblCdPartner";
            this.m_lblCdPartner.Resizeble = true;
            this.m_lblCdPartner.Size = new System.Drawing.Size(70, 18);
            this.m_lblCdPartner.TabIndex = 0;
            this.m_lblCdPartner.Tag = "";
            this.m_lblCdPartner.Text = "거래처";
            this.m_lblCdPartner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel7.Controls.Add(this.m_lblPlantGir);
            this.panel7.Controls.Add(this.lblPMS번호);
            this.panel7.Location = new System.Drawing.Point(285, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(75, 54);
            this.panel7.TabIndex = 40;
            // 
            // m_lblPlantGir
            // 
            this.m_lblPlantGir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.m_lblPlantGir.Location = new System.Drawing.Point(13, 5);
            this.m_lblPlantGir.Name = "m_lblPlantGir";
            this.m_lblPlantGir.Resizeble = true;
            this.m_lblPlantGir.Size = new System.Drawing.Size(59, 18);
            this.m_lblPlantGir.TabIndex = 1;
            this.m_lblPlantGir.Tag = "";
            this.m_lblPlantGir.Text = "공장";
            this.m_lblPlantGir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPMS번호
            // 
            this.lblPMS번호.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lblPMS번호.Location = new System.Drawing.Point(18, 31);
            this.lblPMS번호.Name = "lblPMS번호";
            this.lblPMS번호.Resizeble = true;
            this.lblPMS번호.Size = new System.Drawing.Size(54, 18);
            this.lblPMS번호.TabIndex = 0;
            this.lblPMS번호.Tag = "";
            this.lblPMS번호.Text = "PMS 번호";
            this.lblPMS번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx거래구분
            // 
            this.ctx거래구분.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.ctx거래구분.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.ctx거래구분.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctx거래구분.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ctx거래구분.CodeName = "";
            this.ctx거래구분.CodeValue = "";
            this.ctx거래구분.IgnoreTextChanged = false;
            this.ctx거래구분.IsConfirmed = false;
            this.ctx거래구분.Location = new System.Drawing.Point(633, 3);
            this.ctx거래구분.Name = "ctx거래구분";
            this.ctx거래구분.ReadOnly = true;
            this.ctx거래구분.SelectedAllEnabled = false;
            this.ctx거래구분.Size = new System.Drawing.Size(158, 21);
            this.ctx거래구분.TabIndex = 3;
            this.ctx거래구분.TabStop = false;
            this.ctx거래구분.UseKeyEnter = false;
            this.ctx거래구분.UseKeyF3 = false;
            // 
            // m_btnQuery
            // 
            this.m_btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnQuery.BackColor = System.Drawing.Color.White;
            this.m_btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnQuery.Location = new System.Drawing.Point(606, 3);
            this.m_btnQuery.MaximumSize = new System.Drawing.Size(0, 19);
            this.m_btnQuery.Name = "m_btnQuery";
            this.m_btnQuery.Size = new System.Drawing.Size(60, 19);
            this.m_btnQuery.TabIndex = 14;
            this.m_btnQuery.TabStop = false;
            this.m_btnQuery.Tag = "Q_QUERY";
            this.m_btnQuery.Text = "조회";
            this.m_btnQuery.UseVisualStyleBackColor = false;
            this.m_btnQuery.Click += new System.EventHandler(this.OnSearchButtonClicked);
            // 
            // m_btnApply
            // 
            this.m_btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnApply.BackColor = System.Drawing.Color.White;
            this.m_btnApply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnApply.Location = new System.Drawing.Point(669, 3);
            this.m_btnApply.MaximumSize = new System.Drawing.Size(0, 19);
            this.m_btnApply.Name = "m_btnApply";
            this.m_btnApply.Size = new System.Drawing.Size(60, 19);
            this.m_btnApply.TabIndex = 15;
            this.m_btnApply.TabStop = false;
            this.m_btnApply.Tag = "";
            this.m_btnApply.Text = "적용";
            this.m_btnApply.UseVisualStyleBackColor = false;
            this.m_btnApply.Click += new System.EventHandler(this.OnApply);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.BackColor = System.Drawing.Color.White;
            this.m_btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCancel.Location = new System.Drawing.Point(732, 3);
            this.m_btnCancel.MaximumSize = new System.Drawing.Size(0, 19);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(60, 19);
            this.m_btnCancel.TabIndex = 16;
            this.m_btnCancel.TabStop = false;
            this.m_btnCancel.Tag = "Q_CANCEL";
            this.m_btnCancel.Text = "취소";
            this.m_btnCancel.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelExt3, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(802, 574);
            this.tableLayoutPanel1.TabIndex = 211;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 128);
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
            this.splitContainer1.Size = new System.Drawing.Size(796, 443);
            this.splitContainer1.SplitterDistance = 212;
            this.splitContainer1.TabIndex = 18;
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
            this._flexH.Font = new System.Drawing.Font("굴림", 9F);
            this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexH.Location = new System.Drawing.Point(0, 0);
            this._flexH.Name = "_flexH";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 20;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(796, 212);
            this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
            this._flexH.TabIndex = 0;
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
            this._flexL.Font = new System.Drawing.Font("굴림", 9F);
            this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexL.Location = new System.Drawing.Point(0, 0);
            this._flexL.Name = "_flexL";
            this._flexL.Rows.Count = 1;
            this._flexL.Rows.DefaultSize = 20;
            this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexL.ShowSort = false;
            this._flexL.Size = new System.Drawing.Size(796, 227);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 0;
            // 
            // panelExt3
            // 
            this.panelExt3.Controls.Add(this.m_btnCancel);
            this.panelExt3.Controls.Add(this.m_btnQuery);
            this.panelExt3.Controls.Add(this.m_btnApply);
            this.panelExt3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt3.Location = new System.Drawing.Point(3, 97);
            this.panelExt3.Name = "panelExt3";
            this.panelExt3.Size = new System.Drawing.Size(796, 25);
            this.panelExt3.TabIndex = 1;
            // 
            // P_SA_Z_VPHI_PMS_SUB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(809, 626);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_SA_Z_VPHI_PMS_SUB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TitleText = "PMS 적용";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp일자to)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp일자from)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.panelExt3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Duzon.Common.Controls.PanelExt panel4;
        private Duzon.Common.Controls.DatePicker dtp일자to;
        private Duzon.Common.Controls.DatePicker dtp일자from;
        private Duzon.Common.Controls.PanelExt panel6;
        private Duzon.Common.Controls.CodeTextBox ctx공장;
        private Duzon.Common.Controls.LabelExt label6;
        private Duzon.Common.Controls.PanelExt panel2;
        private Duzon.Common.Controls.LabelExt label1;
        private Duzon.Common.Controls.PanelExt panel1;
        private Duzon.Common.Controls.LabelExt m_lblCdPartner;
        private Duzon.Common.Controls.PanelExt panel7;
        private Duzon.Common.Controls.CodeTextBox ctx거래구분;
        private Duzon.Common.Controls.RoundedButton m_btnQuery;
        private Duzon.Common.Controls.RoundedButton m_btnApply;
        private Duzon.Common.Controls.RoundedButton m_btnCancel;
        private Duzon.Common.Controls.LabelExt m_lblPlantGir;
        private Duzon.Common.BpControls.BpCodeTextBox bp거래처;
        private Duzon.Common.Controls.TextBoxExt txtPMS번호;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.PanelExt panelExt3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Duzon.Common.Controls.LabelExt label3;
        private Duzon.Common.Controls.LabelExt lbl일자;
        private Duzon.Common.Controls.LabelExt lblPMS번호;
        private Duzon.Common.Controls.DropDownComboBox cboPACKING구분;
        private Duzon.Common.Controls.TextBoxExt txt인보이스번호;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.LabelExt lbl인보이스번호;
    }
}

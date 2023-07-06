namespace cz
{
    partial class P_CZ_UNLOADINGRPT_RPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_UNLOADINGRPT_RPT));
            this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this._pnlMain = new System.Windows.Forms.Panel();
            this.dtpTO = new Duzon.Common.Controls.DatePicker();
            this.txtSearch = new Duzon.Common.Controls.TextBoxExt();
            this.panelExt5 = new Duzon.Common.Controls.PanelExt();
            this.bpc거래처 = new Duzon.Common.BpControls.BpCodeNTextBox();
            this.dtpFROM = new Duzon.Common.Controls.DatePicker();
            this.panelExt6 = new Duzon.Common.Controls.PanelExt();
            this.lblTitle05 = new Duzon.Common.Controls.LabelExt();
            this.panelExt7 = new Duzon.Common.Controls.PanelExt();
            this.labelExt1 = new Duzon.Common.Controls.LabelExt();
            this.lblTitle01 = new Duzon.Common.Controls.LabelExt();
            this.mDataArea.SuspendLayout();
            this._tlayMain.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this._pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFROM)).BeginInit();
            this.panelExt6.SuspendLayout();
            this.panelExt7.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this._tlayMain);
            // 
            // _tlayMain
            // 
            this._tlayMain.AutoSize = true;
            this._tlayMain.ColumnCount = 1;
            this._tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlayMain.Controls.Add(this.panel2, 0, 1);
            this._tlayMain.Controls.Add(this._pnlMain, 0, 0);
            this._tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tlayMain.Location = new System.Drawing.Point(0, 0);
            this._tlayMain.Name = "_tlayMain";
            this._tlayMain.RowCount = 2;
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlayMain.Size = new System.Drawing.Size(827, 579);
            this._tlayMain.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this._flex);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(821, 506);
            this.panel2.TabIndex = 72;
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(0, 0);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(821, 506);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 6;
            this._flex.UseGridCalculator = true;
            // 
            // _pnlMain
            // 
            this._pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pnlMain.Controls.Add(this.dtpTO);
            this._pnlMain.Controls.Add(this.txtSearch);
            this._pnlMain.Controls.Add(this.panelExt5);
            this._pnlMain.Controls.Add(this.bpc거래처);
            this._pnlMain.Controls.Add(this.dtpFROM);
            this._pnlMain.Controls.Add(this.panelExt6);
            this._pnlMain.Controls.Add(this.panelExt7);
            this._pnlMain.Location = new System.Drawing.Point(3, 3);
            this._pnlMain.Name = "_pnlMain";
            this._pnlMain.Size = new System.Drawing.Size(821, 61);
            this._pnlMain.TabIndex = 230;
            // 
            // dtpTO
            // 
            this.dtpTO.CalendarBackColor = System.Drawing.Color.White;
            this.dtpTO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpTO.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtpTO.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtpTO.Location = new System.Drawing.Point(186, 2);
            this.dtpTO.Mask = "####/##/##";
            this.dtpTO.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtpTO.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtpTO.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtpTO.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtpTO.Modified = true;
            this.dtpTO.Name = "dtpTO";
            this.dtpTO.NullCheck = false;
            this.dtpTO.PaddingCharacter = '_';
            this.dtpTO.PassivePromptCharacter = '_';
            this.dtpTO.PromptCharacter = '_';
            this.dtpTO.SelectedDayColor = System.Drawing.Color.White;
            this.dtpTO.ShowToDay = true;
            this.dtpTO.ShowTodayCircle = true;
            this.dtpTO.ShowUpDown = false;
            this.dtpTO.Size = new System.Drawing.Size(85, 21);
            this.dtpTO.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtpTO.TabIndex = 122;
            this.dtpTO.TitleBackColor = System.Drawing.Color.White;
            this.dtpTO.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtpTO.ToDayColor = System.Drawing.Color.Red;
            this.dtpTO.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtpTO.UseKeyF3 = false;
            this.dtpTO.Value = new System.DateTime(((long)(0)));
            // 
            // txtSearch
            // 
            this.txtSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(95, 33);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.SelectedAllEnabled = false;
            this.txtSearch.Size = new System.Drawing.Size(224, 21);
            this.txtSearch.TabIndex = 121;
            this.txtSearch.UseKeyEnter = true;
            this.txtSearch.UseKeyF3 = true;
            // 
            // panelExt5
            // 
            this.panelExt5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt5.BackColor = System.Drawing.Color.Transparent;
            this.panelExt5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt5.BackgroundImage")));
            this.panelExt5.Location = new System.Drawing.Point(6, 26);
            this.panelExt5.Name = "panelExt5";
            this.panelExt5.Size = new System.Drawing.Size(811, 1);
            this.panelExt5.TabIndex = 119;
            // 
            // bpc거래처
            // 
            this.bpc거래처.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc거래처.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc거래처.ButtonImage")));
            this.bpc거래처.ChildMode = "";
            this.bpc거래처.CodeName = "";
            this.bpc거래처.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc거래처.CodeValue = "";
            this.bpc거래처.ComboCheck = true;
            this.bpc거래처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bpc거래처.IsCodeValueToUpper = true;
            this.bpc거래처.ItemBackColor = System.Drawing.Color.White;
            this.bpc거래처.Location = new System.Drawing.Point(419, 3);
            this.bpc거래처.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc거래처.Name = "bpc거래처";
            this.bpc거래처.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc거래처.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc거래처.SearchCode = true;
            this.bpc거래처.SelectCount = 0;
            this.bpc거래처.SetDefaultValue = false;
            this.bpc거래처.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc거래처.Size = new System.Drawing.Size(200, 21);
            this.bpc거래처.TabIndex = 117;
            this.bpc거래처.TabStop = false;
            // 
            // dtpFROM
            // 
            this.dtpFROM.CalendarBackColor = System.Drawing.Color.White;
            this.dtpFROM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpFROM.DayColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtpFROM.FriDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(72)))), ((int)(((byte)(125)))));
            this.dtpFROM.Location = new System.Drawing.Point(95, 3);
            this.dtpFROM.Mask = "####/##/##";
            this.dtpFROM.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtpFROM.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtpFROM.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtpFROM.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtpFROM.Modified = true;
            this.dtpFROM.Name = "dtpFROM";
            this.dtpFROM.NullCheck = false;
            this.dtpFROM.PaddingCharacter = '_';
            this.dtpFROM.PassivePromptCharacter = '_';
            this.dtpFROM.PromptCharacter = '_';
            this.dtpFROM.SelectedDayColor = System.Drawing.Color.White;
            this.dtpFROM.ShowToDay = true;
            this.dtpFROM.ShowTodayCircle = true;
            this.dtpFROM.ShowUpDown = false;
            this.dtpFROM.Size = new System.Drawing.Size(85, 21);
            this.dtpFROM.SunDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(104)))), ((int)(((byte)(90)))));
            this.dtpFROM.TabIndex = 31;
            this.dtpFROM.TitleBackColor = System.Drawing.Color.White;
            this.dtpFROM.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dtpFROM.ToDayColor = System.Drawing.Color.Red;
            this.dtpFROM.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtpFROM.UseKeyF3 = false;
            this.dtpFROM.Value = new System.DateTime(2004, 1, 1, 0, 0, 0, 0);
            // 
            // panelExt6
            // 
            this.panelExt6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt6.Controls.Add(this.lblTitle05);
            this.panelExt6.Location = new System.Drawing.Point(325, 2);
            this.panelExt6.Name = "panelExt6";
            this.panelExt6.Size = new System.Drawing.Size(90, 58);
            this.panelExt6.TabIndex = 38;
            // 
            // lblTitle05
            // 
            this.lblTitle05.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lblTitle05.Location = new System.Drawing.Point(3, 3);
            this.lblTitle05.Name = "lblTitle05";
            this.lblTitle05.Resizeble = true;
            this.lblTitle05.Size = new System.Drawing.Size(85, 18);
            this.lblTitle05.TabIndex = 10;
            this.lblTitle05.Tag = "";
            this.lblTitle05.Text = "거래처";
            this.lblTitle05.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelExt7
            // 
            this.panelExt7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt7.Controls.Add(this.labelExt1);
            this.panelExt7.Controls.Add(this.lblTitle01);
            this.panelExt7.Location = new System.Drawing.Point(1, 1);
            this.panelExt7.Name = "panelExt7";
            this.panelExt7.Size = new System.Drawing.Size(90, 59);
            this.panelExt7.TabIndex = 37;
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.labelExt1.Location = new System.Drawing.Point(3, 35);
            this.labelExt1.Name = "labelExt1";
            this.labelExt1.Resizeble = true;
            this.labelExt1.Size = new System.Drawing.Size(85, 18);
            this.labelExt1.TabIndex = 1;
            this.labelExt1.Tag = "";
            this.labelExt1.Text = "선명";
            this.labelExt1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle01
            // 
            this.lblTitle01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lblTitle01.Location = new System.Drawing.Point(3, 4);
            this.lblTitle01.Name = "lblTitle01";
            this.lblTitle01.Resizeble = true;
            this.lblTitle01.Size = new System.Drawing.Size(85, 18);
            this.lblTitle01.TabIndex = 0;
            this.lblTitle01.Tag = "";
            this.lblTitle01.Text = "선적일";
            this.lblTitle01.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // P_CZ_UNLOADINGRPT_RPT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_CZ_UNLOADINGRPT_RPT";
            this.mDataArea.ResumeLayout(false);
            this.mDataArea.PerformLayout();
            this._tlayMain.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this._pnlMain.ResumeLayout(false);
            this._pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFROM)).EndInit();
            this.panelExt6.ResumeLayout(false);
            this.panelExt7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _tlayMain;
        private System.Windows.Forms.Panel panel2;
        private Dass.FlexGrid.FlexGrid _flex;
        private System.Windows.Forms.Panel _pnlMain;
        private Duzon.Common.Controls.DatePicker dtpFROM;
        private Duzon.Common.Controls.PanelExt panelExt6;
        private Duzon.Common.Controls.PanelExt panelExt7;
        private Duzon.Common.Controls.LabelExt lblTitle01;
        private Duzon.Common.Controls.LabelExt lblTitle05;
        private Duzon.Common.BpControls.BpCodeNTextBox bpc거래처;
        private Duzon.Common.Controls.PanelExt panelExt5;
        private Duzon.Common.Controls.TextBoxExt txtSearch;
        private Duzon.Common.Controls.LabelExt labelExt1;
        private Duzon.Common.Controls.DatePicker dtpTO;


    }
}
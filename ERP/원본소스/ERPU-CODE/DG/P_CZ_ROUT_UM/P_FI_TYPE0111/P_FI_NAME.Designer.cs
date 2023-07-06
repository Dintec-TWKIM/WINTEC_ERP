namespace account
{
    partial class P_FI_TYPE0111
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_FI_TYPE0111));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new Duzon.Common.Controls.PanelExt();
            this.bpctb01 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.dtp01 = new Duzon.Common.Controls.DatePicker();
            this.panel5 = new Duzon.Common.Controls.PanelExt();
            this.lblTitle01 = new Duzon.Common.Controls.LabelExt();
            this.panelExt1 = new Duzon.Common.Controls.PanelExt();
            this.lblTitle02 = new Duzon.Common.Controls.LabelExt();
            this._tab = new Duzon.Common.Controls.TabControlExt();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flexTab1M = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexTab1D = new Dass.FlexGrid.FlexGrid(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._flexTab2M = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexTab2D = new Dass.FlexGrid.FlexGrid(this.components);
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btn01 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp01)).BeginInit();
            this.panel5.SuspendLayout();
            this.panelExt1.SuspendLayout();
            this._tab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexTab1M)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexTab1D)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexTab2M)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexTab2D)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._tab, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(793, 577);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.bpctb01);
            this.panel4.Controls.Add(this.dtp01);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panelExt1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(787, 29);
            this.panel4.TabIndex = 119;
            // 
            // bpctb01
            // 
            this.bpctb01.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpctb01.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpctb01.ButtonImage")));
            this.bpctb01.ChildMode = "";
            this.bpctb01.CodeName = "";
            this.bpctb01.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpctb01.CodeValue = "";
            this.bpctb01.ComboCheck = true;
            this.bpctb01.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PC_SUB;
            this.bpctb01.IsCodeValueToUpper = true;
            this.bpctb01.ItemBackColor = System.Drawing.Color.White;
            this.bpctb01.Location = new System.Drawing.Point(334, 3);
            this.bpctb01.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpctb01.Name = "bpctb01";
            this.bpctb01.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpctb01.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpctb01.SearchCode = true;
            this.bpctb01.SelectCount = 0;
            this.bpctb01.SetDefaultValue = false;
            this.bpctb01.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpctb01.Size = new System.Drawing.Size(180, 21);
            this.bpctb01.TabIndex = 1;
            this.bpctb01.TabStop = false;
            // 
            // dtp01
            // 
            this.dtp01.CalendarBackColor = System.Drawing.Color.White;
            this.dtp01.DayColor = System.Drawing.SystemColors.ControlText;
            this.dtp01.FriDayColor = System.Drawing.Color.Blue;
            this.dtp01.Location = new System.Drawing.Point(95, 3);
            this.dtp01.Mask = "####/##/##";
            this.dtp01.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dtp01.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp01.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp01.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp01.Modified = true;
            this.dtp01.Name = "dtp01";
            this.dtp01.PaddingCharacter = '_';
            this.dtp01.PassivePromptCharacter = '_';
            this.dtp01.PromptCharacter = '_';
            this.dtp01.SelectedDayColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(228)))), ((int)(((byte)(153)))));
            this.dtp01.ShowToDay = true;
            this.dtp01.ShowTodayCircle = true;
            this.dtp01.ShowUpDown = false;
            this.dtp01.Size = new System.Drawing.Size(84, 21);
            this.dtp01.SunDayColor = System.Drawing.Color.Red;
            this.dtp01.TabIndex = 0;
            this.dtp01.TitleBackColor = System.Drawing.SystemColors.Control;
            this.dtp01.TitleForeColor = System.Drawing.Color.Black;
            this.dtp01.ToDayColor = System.Drawing.Color.Red;
            this.dtp01.TrailingForeColor = System.Drawing.SystemColors.ControlDark;
            this.dtp01.UseKeyF3 = false;
            this.dtp01.Value = new System.DateTime(2005, 2, 1, 0, 0, 0, 0);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panel5.Controls.Add(this.lblTitle01);
            this.panel5.Location = new System.Drawing.Point(1, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(90, 25);
            this.panel5.TabIndex = 13;
            // 
            // lblTitle01
            // 
            this.lblTitle01.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle01.Location = new System.Drawing.Point(3, 4);
            this.lblTitle01.Name = "lblTitle01";
            this.lblTitle01.Resizeble = true;
            this.lblTitle01.Size = new System.Drawing.Size(85, 18);
            this.lblTitle01.TabIndex = 0;
            this.lblTitle01.Tag = "";
            this.lblTitle01.Text = "lblTitle01";
            this.lblTitle01.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelExt1
            // 
            this.panelExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt1.Controls.Add(this.lblTitle02);
            this.panelExt1.Location = new System.Drawing.Point(240, 1);
            this.panelExt1.Name = "panelExt1";
            this.panelExt1.Size = new System.Drawing.Size(90, 25);
            this.panelExt1.TabIndex = 14;
            // 
            // lblTitle02
            // 
            this.lblTitle02.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle02.Location = new System.Drawing.Point(3, 4);
            this.lblTitle02.Name = "lblTitle02";
            this.lblTitle02.Resizeble = true;
            this.lblTitle02.Size = new System.Drawing.Size(85, 18);
            this.lblTitle02.TabIndex = 0;
            this.lblTitle02.Tag = "CD_PC";
            this.lblTitle02.Text = "lblTitle02";
            this.lblTitle02.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _tab
            // 
            this._tab.Controls.Add(this.tabPage1);
            this._tab.Controls.Add(this.tabPage2);
            this._tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tab.ImageList = this.imageList;
            this._tab.Location = new System.Drawing.Point(3, 38);
            this._tab.Name = "_tab";
            this._tab.SelectedIndex = 0;
            this._tab.Size = new System.Drawing.Size(787, 536);
            this._tab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this._tab.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(779, 509);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._flexTab1M);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._flexTab1D);
            this.splitContainer1.Size = new System.Drawing.Size(773, 503);
            this.splitContainer1.SplitterDistance = 327;
            this.splitContainer1.TabIndex = 0;
            // 
            // _flexTab1M
            // 
            this._flexTab1M.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexTab1M.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexTab1M.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexTab1M.AutoResize = false;
            this._flexTab1M.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexTab1M.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexTab1M.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexTab1M.EnabledHeaderCheck = true;
            this._flexTab1M.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexTab1M.Location = new System.Drawing.Point(0, 0);
            this._flexTab1M.Name = "_flexTab1M";
            this._flexTab1M.Rows.Count = 1;
            this._flexTab1M.Rows.DefaultSize = 20;
            this._flexTab1M.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexTab1M.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexTab1M.ShowSort = false;
            this._flexTab1M.Size = new System.Drawing.Size(327, 503);
            this._flexTab1M.StyleInfo = resources.GetString("_flexTab1M.StyleInfo");
            this._flexTab1M.TabIndex = 0;
            // 
            // _flexTab1D
            // 
            this._flexTab1D.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexTab1D.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexTab1D.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexTab1D.AutoResize = false;
            this._flexTab1D.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexTab1D.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexTab1D.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexTab1D.EnabledHeaderCheck = true;
            this._flexTab1D.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexTab1D.Location = new System.Drawing.Point(0, 0);
            this._flexTab1D.Name = "_flexTab1D";
            this._flexTab1D.Rows.Count = 1;
            this._flexTab1D.Rows.DefaultSize = 20;
            this._flexTab1D.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexTab1D.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexTab1D.ShowSort = false;
            this._flexTab1D.Size = new System.Drawing.Size(442, 503);
            this._flexTab1D.StyleInfo = resources.GetString("_flexTab1D.StyleInfo");
            this._flexTab1D.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(779, 509);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._flexTab2M);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._flexTab2D);
            this.splitContainer2.Size = new System.Drawing.Size(773, 503);
            this.splitContainer2.SplitterDistance = 327;
            this.splitContainer2.TabIndex = 1;
            // 
            // _flexTab2M
            // 
            this._flexTab2M.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexTab2M.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexTab2M.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexTab2M.AutoResize = false;
            this._flexTab2M.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexTab2M.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexTab2M.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexTab2M.EnabledHeaderCheck = true;
            this._flexTab2M.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexTab2M.Location = new System.Drawing.Point(0, 0);
            this._flexTab2M.Name = "_flexTab2M";
            this._flexTab2M.Rows.Count = 1;
            this._flexTab2M.Rows.DefaultSize = 20;
            this._flexTab2M.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexTab2M.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexTab2M.ShowSort = false;
            this._flexTab2M.Size = new System.Drawing.Size(327, 503);
            this._flexTab2M.StyleInfo = resources.GetString("_flexTab2M.StyleInfo");
            this._flexTab2M.TabIndex = 0;
            // 
            // _flexTab2D
            // 
            this._flexTab2D.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexTab2D.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexTab2D.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexTab2D.AutoResize = false;
            this._flexTab2D.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexTab2D.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexTab2D.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexTab2D.EnabledHeaderCheck = true;
            this._flexTab2D.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexTab2D.Location = new System.Drawing.Point(0, 0);
            this._flexTab2D.Name = "_flexTab2D";
            this._flexTab2D.Rows.Count = 1;
            this._flexTab2D.Rows.DefaultSize = 20;
            this._flexTab2D.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexTab2D.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexTab2D.ShowSort = false;
            this._flexTab2D.Size = new System.Drawing.Size(442, 503);
            this._flexTab2D.StyleInfo = resources.GetString("_flexTab2D.StyleInfo");
            this._flexTab2D.TabIndex = 1;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "tab_icon_hr_pca02.gif");
            this.imageList.Images.SetKeyName(1, "tab_icon_trpr02.gif");
            // 
            // btn01
            // 
            this.btn01.BackColor = System.Drawing.Color.White;
            this.btn01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn01.Location = new System.Drawing.Point(729, 10);
            this.btn01.MaximumSize = new System.Drawing.Size(0, 22);
            this.btn01.Name = "btn01";
            this.btn01.Size = new System.Drawing.Size(70, 22);
            this.btn01.TabIndex = 2;
            this.btn01.TabStop = false;
            this.btn01.Text = "btn01";
            this.btn01.UseVisualStyleBackColor = true;
            // 
            // P_FI_TYPE0111
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btn01);
            this.Name = "P_FI_NAME";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.btn01, 0);
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp01)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panelExt1.ResumeLayout(false);
            this._tab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexTab1M)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexTab1D)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexTab2M)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexTab2D)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Common.Controls.PanelExt panel4;
        private Duzon.Common.BpControls.BpCodeTextBox bpctb01;
        private Duzon.Common.Controls.DatePicker dtp01;
        private Duzon.Common.Controls.PanelExt panel5;
        private Duzon.Common.Controls.LabelExt lblTitle01;
        private Duzon.Common.Controls.PanelExt panelExt1;
        private Duzon.Common.Controls.LabelExt lblTitle02;
        private Duzon.Common.Controls.TabControlExt _tab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flexTab1M;
        private Dass.FlexGrid.FlexGrid _flexTab1D;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Dass.FlexGrid.FlexGrid _flexTab2M;
        private Dass.FlexGrid.FlexGrid _flexTab2D;
        private System.Windows.Forms.ImageList imageList;
        private Duzon.Common.Controls.RoundedButton btn01;
    }
}
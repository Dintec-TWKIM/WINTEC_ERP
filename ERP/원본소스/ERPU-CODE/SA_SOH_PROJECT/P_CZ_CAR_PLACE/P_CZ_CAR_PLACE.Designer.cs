namespace cz
{
    partial class P_CZ_CAR_PLACE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_CAR_PLACE));
            this._pnlMain = new System.Windows.Forms.Panel();
            this.ctx상하차지명 = new Duzon.Common.Controls.TextBoxExt();
            this.panelExt4 = new Duzon.Common.Controls.PanelExt();
            this.panelExt5 = new Duzon.Common.Controls.PanelExt();
            this.bpc공장명 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.panelExt6 = new Duzon.Common.Controls.PanelExt();
            this.lblTitle02 = new Duzon.Common.Controls.LabelExt();
            this.panelExt7 = new Duzon.Common.Controls.PanelExt();
            this.lblTitle01 = new Duzon.Common.Controls.LabelExt();
            this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.mDataArea.SuspendLayout();
            this._pnlMain.SuspendLayout();
            this.panelExt6.SuspendLayout();
            this.panelExt7.SuspendLayout();
            this._tlayMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this._tlayMain);
            // 
            // _pnlMain
            // 
            this._pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pnlMain.Controls.Add(this.ctx상하차지명);
            this._pnlMain.Controls.Add(this.panelExt4);
            this._pnlMain.Controls.Add(this.panelExt5);
            this._pnlMain.Controls.Add(this.bpc공장명);
            this._pnlMain.Controls.Add(this.panelExt6);
            this._pnlMain.Controls.Add(this.panelExt7);
            this._pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pnlMain.Location = new System.Drawing.Point(3, 3);
            this._pnlMain.Name = "_pnlMain";
            this._pnlMain.Size = new System.Drawing.Size(821, 36);
            this._pnlMain.TabIndex = 230;
            // 
            // ctx상하차지명
            // 
            this.ctx상하차지명.BackColor = System.Drawing.Color.White;
            this.ctx상하차지명.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.ctx상하차지명.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctx상하차지명.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ctx상하차지명.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.ctx상하차지명.Location = new System.Drawing.Point(99, 5);
            this.ctx상하차지명.MaxLength = 20;
            this.ctx상하차지명.Name = "ctx상하차지명";
            this.ctx상하차지명.SelectedAllEnabled = false;
            this.ctx상하차지명.Size = new System.Drawing.Size(165, 21);
            this.ctx상하차지명.TabIndex = 130;
            this.ctx상하차지명.Tag = "";
            this.ctx상하차지명.UseKeyEnter = false;
            this.ctx상하차지명.UseKeyF3 = true;
            // 
            // panelExt4
            // 
            this.panelExt4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt4.BackColor = System.Drawing.Color.Transparent;
            this.panelExt4.Location = new System.Drawing.Point(6, 52);
            this.panelExt4.Name = "panelExt4";
            this.panelExt4.Size = new System.Drawing.Size(845, 1);
            this.panelExt4.TabIndex = 129;
            // 
            // panelExt5
            // 
            this.panelExt5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt5.BackColor = System.Drawing.Color.Transparent;
            this.panelExt5.Location = new System.Drawing.Point(6, 26);
            this.panelExt5.Name = "panelExt5";
            this.panelExt5.Size = new System.Drawing.Size(845, 1);
            this.panelExt5.TabIndex = 128;
            // 
            // bpc공장명
            // 
            this.bpc공장명.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.bpc공장명.ButtonImage = ((System.Drawing.Image)(resources.GetObject("bpc공장명.ButtonImage")));
            this.bpc공장명.ChildMode = "";
            this.bpc공장명.CodeName = "";
            this.bpc공장명.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.bpc공장명.CodeValue = "";
            this.bpc공장명.ComboCheck = true;
            this.bpc공장명.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PLANT_SUB;
            this.bpc공장명.IsCodeValueToUpper = true;
            this.bpc공장명.ItemBackColor = System.Drawing.Color.White;
            this.bpc공장명.Location = new System.Drawing.Point(365, 5);
            this.bpc공장명.MaximumSize = new System.Drawing.Size(0, 21);
            this.bpc공장명.Name = "bpc공장명";
            this.bpc공장명.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.bpc공장명.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.bpc공장명.SearchCode = true;
            this.bpc공장명.SelectCount = 0;
            this.bpc공장명.SetDefaultValue = false;
            this.bpc공장명.SetNoneTypeMsg = "Please! Set Help Type!";
            this.bpc공장명.Size = new System.Drawing.Size(200, 21);
            this.bpc공장명.TabIndex = 3;
            this.bpc공장명.TabStop = false;
            // 
            // panelExt6
            // 
            this.panelExt6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt6.Controls.Add(this.lblTitle02);
            this.panelExt6.Location = new System.Drawing.Point(286, 1);
            this.panelExt6.Name = "panelExt6";
            this.panelExt6.Size = new System.Drawing.Size(75, 26);
            this.panelExt6.TabIndex = 38;
            // 
            // lblTitle02
            // 
            this.lblTitle02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lblTitle02.Location = new System.Drawing.Point(3, 4);
            this.lblTitle02.Name = "lblTitle02";
            this.lblTitle02.Resizeble = true;
            this.lblTitle02.Size = new System.Drawing.Size(70, 18);
            this.lblTitle02.TabIndex = 5;
            this.lblTitle02.Tag = "";
            this.lblTitle02.Text = "공장명";
            this.lblTitle02.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelExt7
            // 
            this.panelExt7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt7.Controls.Add(this.lblTitle01);
            this.panelExt7.Location = new System.Drawing.Point(1, 1);
            this.panelExt7.Name = "panelExt7";
            this.panelExt7.Size = new System.Drawing.Size(92, 26);
            this.panelExt7.TabIndex = 37;
            // 
            // lblTitle01
            // 
            this.lblTitle01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lblTitle01.Location = new System.Drawing.Point(-3, 4);
            this.lblTitle01.Name = "lblTitle01";
            this.lblTitle01.Resizeble = true;
            this.lblTitle01.Size = new System.Drawing.Size(92, 18);
            this.lblTitle01.TabIndex = 0;
            this.lblTitle01.Tag = "";
            this.lblTitle01.Text = "상/하차지명";
            this.lblTitle01.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _tlayMain
            // 
            this._tlayMain.AutoSize = true;
            this._tlayMain.ColumnCount = 1;
            this._tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlayMain.Controls.Add(this._flex, 0, 1);
            this._tlayMain.Controls.Add(this._pnlMain, 0, 0);
            this._tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tlayMain.Location = new System.Drawing.Point(0, 0);
            this._tlayMain.Name = "_tlayMain";
            this._tlayMain.RowCount = 2;
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.13854F));
            this._tlayMain.Size = new System.Drawing.Size(827, 579);
            this._tlayMain.TabIndex = 7;
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
            this._flex.Location = new System.Drawing.Point(3, 45);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(821, 531);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 232;
            this._flex.UseGridCalculator = true;
            // 
            // P_CZ_CAR_PLACE
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_CZ_CAR_PLACE";
            this.mDataArea.ResumeLayout(false);
            this.mDataArea.PerformLayout();
            this._pnlMain.ResumeLayout(false);
            this._pnlMain.PerformLayout();
            this.panelExt6.ResumeLayout(false);
            this.panelExt7.ResumeLayout(false);
            this._tlayMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _tlayMain;
        private Dass.FlexGrid.FlexGrid _flex;
        private System.Windows.Forms.Panel _pnlMain;
        private Duzon.Common.Controls.TextBoxExt ctx상하차지명;
        private Duzon.Common.Controls.PanelExt panelExt4;
        private Duzon.Common.Controls.PanelExt panelExt5;
        private Duzon.Common.BpControls.BpCodeTextBox bpc공장명;
        private Duzon.Common.Controls.PanelExt panelExt6;
        private Duzon.Common.Controls.LabelExt lblTitle02;
        private Duzon.Common.Controls.PanelExt panelExt7;
        private Duzon.Common.Controls.LabelExt lblTitle01;

    }
}


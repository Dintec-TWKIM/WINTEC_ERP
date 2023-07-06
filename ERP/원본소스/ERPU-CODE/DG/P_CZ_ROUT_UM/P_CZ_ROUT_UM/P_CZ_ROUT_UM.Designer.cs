namespace cz
{
    partial class P_CZ_ROUT_UM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_ROUT_UM));
            this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this._pnlMain = new System.Windows.Forms.Panel();
            this.panelExt4 = new Duzon.Common.Controls.PanelExt();
            this.panelExt5 = new Duzon.Common.Controls.PanelExt();
            this.ctx화주 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.panelExt6 = new Duzon.Common.Controls.PanelExt();
            this.lblTitle02 = new Duzon.Common.Controls.LabelExt();
            this.실상차지 = new Duzon.Common.Controls.LabelExt();
            this.panelExt7 = new Duzon.Common.Controls.PanelExt();
            this.lblTitle04 = new Duzon.Common.Controls.LabelExt();
            this.lblTitle03 = new Duzon.Common.Controls.LabelExt();
            this.ctx실상차지 = new Duzon.Common.Controls.TextBoxExt();
            this.ctx하차지 = new Duzon.Common.Controls.TextBoxExt();
            this.ctx상차지 = new Duzon.Common.Controls.TextBoxExt();
            this.mDataArea.SuspendLayout();
            this._tlayMain.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this._pnlMain.SuspendLayout();
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
            this.panel2.Location = new System.Drawing.Point(3, 91);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(821, 485);
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
            this._flex.Size = new System.Drawing.Size(821, 485);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 6;
            this._flex.UseGridCalculator = true;
            // 
            // _pnlMain
            // 
            this._pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pnlMain.Controls.Add(this.ctx상차지);
            this._pnlMain.Controls.Add(this.ctx하차지);
            this._pnlMain.Controls.Add(this.ctx실상차지);
            this._pnlMain.Controls.Add(this.panelExt4);
            this._pnlMain.Controls.Add(this.panelExt5);
            this._pnlMain.Controls.Add(this.ctx화주);
            this._pnlMain.Controls.Add(this.panelExt6);
            this._pnlMain.Controls.Add(this.panelExt7);
            this._pnlMain.Location = new System.Drawing.Point(3, 3);
            this._pnlMain.Name = "_pnlMain";
            this._pnlMain.Size = new System.Drawing.Size(821, 82);
            this._pnlMain.TabIndex = 230;
            // 
            // panelExt4
            // 
            this.panelExt4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExt4.BackColor = System.Drawing.Color.Transparent;
            this.panelExt4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelExt4.BackgroundImage")));
            this.panelExt4.Location = new System.Drawing.Point(6, 52);
            this.panelExt4.Name = "panelExt4";
            this.panelExt4.Size = new System.Drawing.Size(811, 1);
            this.panelExt4.TabIndex = 120;
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
            // ctx화주
            // 
            this.ctx화주.BpControlMode = Duzon.Common.Forms.Help.BpControlMode.Normal;
            this.ctx화주.ButtonImage = ((System.Drawing.Image)(resources.GetObject("ctx화주.ButtonImage")));
            this.ctx화주.ChildMode = "";
            this.ctx화주.CodeName = "";
            this.ctx화주.CodeSearchMode = Duzon.Common.Forms.Help.CodeSearchMode.NotExistOpenHelp;
            this.ctx화주.CodeValue = "";
            this.ctx화주.ComboCheck = true;
            this.ctx화주.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx화주.IsCodeValueToUpper = true;
            this.ctx화주.ItemBackColor = System.Drawing.Color.White;
            this.ctx화주.Location = new System.Drawing.Point(94, 2);
            this.ctx화주.MaximumSize = new System.Drawing.Size(0, 21);
            this.ctx화주.Name = "ctx화주";
            this.ctx화주.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.None;
            this.ctx화주.ResultMode = Duzon.Common.Forms.Help.ResultMode.FastMode;
            this.ctx화주.SearchCode = true;
            this.ctx화주.SelectCount = 0;
            this.ctx화주.SetDefaultValue = false;
            this.ctx화주.SetNoneTypeMsg = "Please! Set Help Type!";
            this.ctx화주.Size = new System.Drawing.Size(200, 21);
            this.ctx화주.TabIndex = 116;
            this.ctx화주.TabStop = false;
            // 
            // panelExt6
            // 
            this.panelExt6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt6.Controls.Add(this.lblTitle02);
            this.panelExt6.Controls.Add(this.실상차지);
            this.panelExt6.Location = new System.Drawing.Point(325, 2);
            this.panelExt6.Name = "panelExt6";
            this.panelExt6.Size = new System.Drawing.Size(90, 51);
            this.panelExt6.TabIndex = 38;
            // 
            // lblTitle02
            // 
            this.lblTitle02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lblTitle02.Location = new System.Drawing.Point(3, 32);
            this.lblTitle02.Name = "lblTitle02";
            this.lblTitle02.Resizeble = true;
            this.lblTitle02.Size = new System.Drawing.Size(85, 18);
            this.lblTitle02.TabIndex = 5;
            this.lblTitle02.Tag = "";
            this.lblTitle02.Text = "하차지";
            this.lblTitle02.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // 실상차지
            // 
            this.실상차지.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.실상차지.Location = new System.Drawing.Point(5, 4);
            this.실상차지.Name = "실상차지";
            this.실상차지.Resizeble = true;
            this.실상차지.Size = new System.Drawing.Size(85, 18);
            this.실상차지.TabIndex = 13;
            this.실상차지.Tag = "";
            this.실상차지.Text = "실상차지";
            this.실상차지.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelExt7
            // 
            this.panelExt7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.panelExt7.Controls.Add(this.lblTitle04);
            this.panelExt7.Controls.Add(this.lblTitle03);
            this.panelExt7.Location = new System.Drawing.Point(1, 1);
            this.panelExt7.Name = "panelExt7";
            this.panelExt7.Size = new System.Drawing.Size(90, 52);
            this.panelExt7.TabIndex = 37;
            // 
            // lblTitle04
            // 
            this.lblTitle04.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lblTitle04.Location = new System.Drawing.Point(2, 28);
            this.lblTitle04.Name = "lblTitle04";
            this.lblTitle04.Resizeble = true;
            this.lblTitle04.Size = new System.Drawing.Size(85, 18);
            this.lblTitle04.TabIndex = 14;
            this.lblTitle04.Tag = "";
            this.lblTitle04.Text = "상차지";
            this.lblTitle04.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle03
            // 
            this.lblTitle03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.lblTitle03.Location = new System.Drawing.Point(2, 3);
            this.lblTitle03.Name = "lblTitle03";
            this.lblTitle03.Resizeble = true;
            this.lblTitle03.Size = new System.Drawing.Size(85, 18);
            this.lblTitle03.TabIndex = 12;
            this.lblTitle03.Tag = "";
            this.lblTitle03.Text = "화주";
            this.lblTitle03.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ctx실상차지
            // 
            this.ctx실상차지.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.ctx실상차지.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctx실상차지.Location = new System.Drawing.Point(419, 2);
            this.ctx실상차지.Name = "ctx실상차지";
            this.ctx실상차지.SelectedAllEnabled = false;
            this.ctx실상차지.Size = new System.Drawing.Size(195, 21);
            this.ctx실상차지.TabIndex = 124;
            this.ctx실상차지.UseKeyEnter = true;
            this.ctx실상차지.UseKeyF3 = true;
            // 
            // ctx하차지
            // 
            this.ctx하차지.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.ctx하차지.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctx하차지.Location = new System.Drawing.Point(421, 29);
            this.ctx하차지.Name = "ctx하차지";
            this.ctx하차지.SelectedAllEnabled = false;
            this.ctx하차지.Size = new System.Drawing.Size(195, 21);
            this.ctx하차지.TabIndex = 125;
            this.ctx하차지.UseKeyEnter = true;
            this.ctx하차지.UseKeyF3 = true;
            // 
            // ctx상차지
            // 
            this.ctx상차지.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.ctx상차지.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctx상차지.Location = new System.Drawing.Point(97, 29);
            this.ctx상차지.Name = "ctx상차지";
            this.ctx상차지.SelectedAllEnabled = false;
            this.ctx상차지.Size = new System.Drawing.Size(195, 21);
            this.ctx상차지.TabIndex = 126;
            this.ctx상차지.UseKeyEnter = true;
            this.ctx상차지.UseKeyF3 = true;
            this.ctx상차지.TextChanged += new System.EventHandler(this.ctx상차지_TextChanged);
            // 
            // P_CZ_ROUT_UM
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_CZ_ROUT_UM";
            this.mDataArea.ResumeLayout(false);
            this.mDataArea.PerformLayout();
            this._tlayMain.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this._pnlMain.ResumeLayout(false);
            this._pnlMain.PerformLayout();
            this.panelExt6.ResumeLayout(false);
            this.panelExt7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _tlayMain;
        private System.Windows.Forms.Panel panel2;
        private Dass.FlexGrid.FlexGrid _flex;
        private System.Windows.Forms.Panel _pnlMain;
        private Duzon.Common.BpControls.BpCodeTextBox ctx화주;
        private Duzon.Common.Controls.PanelExt panelExt6;
        private Duzon.Common.Controls.LabelExt lblTitle02;
        private Duzon.Common.Controls.LabelExt 실상차지;
        private Duzon.Common.Controls.PanelExt panelExt7;
        private Duzon.Common.Controls.LabelExt lblTitle03;
        private Duzon.Common.Controls.LabelExt lblTitle04;
        private Duzon.Common.Controls.PanelExt panelExt4;
        private Duzon.Common.Controls.PanelExt panelExt5;
        private Duzon.Common.Controls.TextBoxExt ctx실상차지;
        private Duzon.Common.Controls.TextBoxExt ctx상차지;
        private Duzon.Common.Controls.TextBoxExt ctx하차지;


    }
}
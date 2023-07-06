
namespace cz
{
    partial class P_CZ_PR_OPOUT_PO_MNG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_OPOUT_PO_MNG));
            this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn삭제 = new Duzon.Common.Controls.RoundedButton(this.components);
            this._flexM = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexD = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.bp외주처 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.bpP_Dt_Po = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp발주기간 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl발주기간 = new Duzon.Common.Controls.LabelExt();
            this.bpP_Plant = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl공장 = new Duzon.Common.Controls.LabelExt();
            this.m_cboCdPlant = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPnlSearch1 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt검색1 = new System.Windows.Forms.TextBox();
            this.cboSearch1 = new Duzon.Common.Controls.DropDownComboBox();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.bp작업지시번호 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl작업지시번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl외주처 = new Duzon.Common.Controls.LabelExt();
            this.mDataArea.SuspendLayout();
            this._tlayMain.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexD)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.bpP_Dt_Po.SuspendLayout();
            this.bpP_Plant.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPnlSearch1.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this._tlayMain);
            this.mDataArea.Size = new System.Drawing.Size(1060, 756);
            // 
            // _tlayMain
            // 
            this._tlayMain.ColumnCount = 1;
            this._tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlayMain.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this._tlayMain.Controls.Add(this._flexM, 0, 1);
            this._tlayMain.Controls.Add(this._flexD, 0, 3);
            this._tlayMain.Controls.Add(this.oneGrid1, 0, 0);
            this._tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tlayMain.Location = new System.Drawing.Point(0, 0);
            this._tlayMain.Name = "_tlayMain";
            this._tlayMain.RowCount = 4;
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this._tlayMain.Size = new System.Drawing.Size(1060, 756);
            this._tlayMain.TabIndex = 139;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btn삭제);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(817, 400);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(240, 21);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // btn삭제
            // 
            this.btn삭제.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn삭제.BackColor = System.Drawing.Color.White;
            this.btn삭제.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn삭제.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn삭제.Location = new System.Drawing.Point(178, 1);
            this.btn삭제.Margin = new System.Windows.Forms.Padding(3, 1, 0, 3);
            this.btn삭제.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn삭제.Name = "btn삭제";
            this.btn삭제.Size = new System.Drawing.Size(62, 19);
            this.btn삭제.TabIndex = 6;
            this.btn삭제.TabStop = false;
            this.btn삭제.Text = "삭제";
            this.btn삭제.UseVisualStyleBackColor = false;
            // 
            // _flexM
            // 
            this._flexM.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexM.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexM.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexM.AutoResize = false;
            this._flexM.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexM.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexM.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexM.EnabledHeaderCheck = true;
            this._flexM.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexM.Location = new System.Drawing.Point(3, 71);
            this._flexM.Name = "_flexM";
            this._flexM.Rows.Count = 1;
            this._flexM.Rows.DefaultSize = 20;
            this._flexM.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM.ShowSort = false;
            this._flexM.Size = new System.Drawing.Size(1054, 323);
            this._flexM.StyleInfo = resources.GetString("_flexM.StyleInfo");
            this._flexM.TabIndex = 7;
            this._flexM.UseGridCalculator = true;
            // 
            // _flexD
            // 
            this._flexD.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexD.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexD.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexD.AutoResize = false;
            this._flexD.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexD.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexD.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexD.EnabledHeaderCheck = true;
            this._flexD.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexD.Location = new System.Drawing.Point(3, 428);
            this._flexD.Name = "_flexD";
            this._flexD.Rows.Count = 1;
            this._flexD.Rows.DefaultSize = 18;
            this._flexD.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexD.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexD.ShowSort = false;
            this._flexD.Size = new System.Drawing.Size(1054, 325);
            this._flexD.StyleInfo = resources.GetString("_flexD.StyleInfo");
            this._flexD.TabIndex = 7;
            this._flexD.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(1054, 62);
            this.oneGrid1.TabIndex = 131;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl4);
            this.oneGridItem1.Controls.Add(this.bpP_Dt_Po);
            this.oneGridItem1.Controls.Add(this.bpP_Plant);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(1044, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.lbl외주처);
            this.bpPanelControl4.Controls.Add(this.bp외주처);
            this.bpPanelControl4.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl4.TabIndex = 3;
            this.bpPanelControl4.Text = "bpPanelControl2";
            // 
            // bp외주처
            // 
            this.bp외주처.Dock = System.Windows.Forms.DockStyle.Right;
            this.bp외주처.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.bp외주처.LabelWidth = 156;
            this.bp외주처.Location = new System.Drawing.Point(107, 0);
            this.bp외주처.Name = "bp외주처";
            this.bp외주처.Size = new System.Drawing.Size(185, 21);
            this.bp외주처.TabIndex = 142;
            this.bp외주처.TabStop = false;
            this.bp외주처.Tag = "CD_PARTNER;LN_PARTNER";
            this.bp외주처.Text = "외주처";
            // 
            // bpP_Dt_Po
            // 
            this.bpP_Dt_Po.Controls.Add(this.dtp발주기간);
            this.bpP_Dt_Po.Controls.Add(this.lbl발주기간);
            this.bpP_Dt_Po.Location = new System.Drawing.Point(296, 1);
            this.bpP_Dt_Po.Name = "bpP_Dt_Po";
            this.bpP_Dt_Po.Size = new System.Drawing.Size(292, 23);
            this.bpP_Dt_Po.TabIndex = 4;
            this.bpP_Dt_Po.Text = "bpPanelControl5";
            // 
            // dtp발주기간
            // 
            this.dtp발주기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp발주기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp발주기간.IsNecessaryCondition = true;
            this.dtp발주기간.Location = new System.Drawing.Point(107, 0);
            this.dtp발주기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp발주기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp발주기간.Name = "dtp발주기간";
            this.dtp발주기간.Size = new System.Drawing.Size(185, 21);
            this.dtp발주기간.TabIndex = 248;
            // 
            // lbl발주기간
            // 
            this.lbl발주기간.BackColor = System.Drawing.Color.Transparent;
            this.lbl발주기간.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl발주기간.Location = new System.Drawing.Point(0, 0);
            this.lbl발주기간.Name = "lbl발주기간";
            this.lbl발주기간.Size = new System.Drawing.Size(100, 23);
            this.lbl발주기간.TabIndex = 3;
            this.lbl발주기간.Text = "발주기간";
            this.lbl발주기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpP_Plant
            // 
            this.bpP_Plant.Controls.Add(this.lbl공장);
            this.bpP_Plant.Controls.Add(this.m_cboCdPlant);
            this.bpP_Plant.Location = new System.Drawing.Point(2, 1);
            this.bpP_Plant.Name = "bpP_Plant";
            this.bpP_Plant.Size = new System.Drawing.Size(292, 23);
            this.bpP_Plant.TabIndex = 2;
            this.bpP_Plant.Text = "bpPanelControl6";
            // 
            // lbl공장
            // 
            this.lbl공장.BackColor = System.Drawing.Color.Transparent;
            this.lbl공장.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl공장.Location = new System.Drawing.Point(0, 0);
            this.lbl공장.Name = "lbl공장";
            this.lbl공장.Size = new System.Drawing.Size(100, 23);
            this.lbl공장.TabIndex = 1;
            this.lbl공장.Tag = "공장";
            this.lbl공장.Text = "공장";
            this.lbl공장.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_cboCdPlant
            // 
            this.m_cboCdPlant.AutoDropDown = false;
            this.m_cboCdPlant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.m_cboCdPlant.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_cboCdPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCdPlant.ItemHeight = 12;
            this.m_cboCdPlant.Location = new System.Drawing.Point(107, 0);
            this.m_cboCdPlant.Name = "m_cboCdPlant";
            this.m_cboCdPlant.Size = new System.Drawing.Size(185, 20);
            this.m_cboCdPlant.TabIndex = 1;
            this.m_cboCdPlant.UseKeyF3 = false;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPnlSearch1);
            this.oneGridItem2.Controls.Add(this.bpPanelControl1);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(1044, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPnlSearch1
            // 
            this.bpPnlSearch1.Controls.Add(this.txt검색1);
            this.bpPnlSearch1.Controls.Add(this.cboSearch1);
            this.bpPnlSearch1.Location = new System.Drawing.Point(296, 1);
            this.bpPnlSearch1.Name = "bpPnlSearch1";
            this.bpPnlSearch1.Size = new System.Drawing.Size(292, 23);
            this.bpPnlSearch1.TabIndex = 4;
            // 
            // txt검색1
            // 
            this.txt검색1.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt검색1.Location = new System.Drawing.Point(107, 0);
            this.txt검색1.MaxLength = 100;
            this.txt검색1.Name = "txt검색1";
            this.txt검색1.Size = new System.Drawing.Size(185, 21);
            this.txt검색1.TabIndex = 9;
            // 
            // cboSearch1
            // 
            this.cboSearch1.AutoDropDown = true;
            this.cboSearch1.Dock = System.Windows.Forms.DockStyle.Left;
            this.cboSearch1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearch1.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.cboSearch1.ItemHeight = 12;
            this.cboSearch1.Location = new System.Drawing.Point(0, 0);
            this.cboSearch1.MaxLength = 15;
            this.cboSearch1.Name = "cboSearch1";
            this.cboSearch1.Size = new System.Drawing.Size(100, 20);
            this.cboSearch1.TabIndex = 32;
            this.cboSearch1.Tag = "GRP_MFG";
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.bp작업지시번호);
            this.bpPanelControl1.Controls.Add(this.lbl작업지시번호);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // bp작업지시번호
            // 
            this.bp작업지시번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.bp작업지시번호.HelpID = Duzon.Common.Forms.Help.HelpID.P_PR_WO_REG_SUB;
            this.bp작업지시번호.Location = new System.Drawing.Point(107, 0);
            this.bp작업지시번호.Name = "bp작업지시번호";
            this.bp작업지시번호.Size = new System.Drawing.Size(185, 21);
            this.bp작업지시번호.TabIndex = 3;
            this.bp작업지시번호.TabStop = false;
            this.bp작업지시번호.Text = "bpCodeTextBox1";
            // 
            // lbl작업지시번호
            // 
            this.lbl작업지시번호.BackColor = System.Drawing.Color.Transparent;
            this.lbl작업지시번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl작업지시번호.Location = new System.Drawing.Point(0, 0);
            this.lbl작업지시번호.Name = "lbl작업지시번호";
            this.lbl작업지시번호.Size = new System.Drawing.Size(100, 23);
            this.lbl작업지시번호.TabIndex = 2;
            this.lbl작업지시번호.Text = "작업지시번호";
            this.lbl작업지시번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl외주처
            // 
            this.lbl외주처.BackColor = System.Drawing.Color.Transparent;
            this.lbl외주처.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl외주처.Location = new System.Drawing.Point(0, 0);
            this.lbl외주처.Name = "lbl외주처";
            this.lbl외주처.Size = new System.Drawing.Size(100, 23);
            this.lbl외주처.TabIndex = 143;
            this.lbl외주처.Text = "외주처";
            this.lbl외주처.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // P_CZ_PR_OPOUT_PO_MNG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_CZ_PR_OPOUT_PO_MNG";
            this.Size = new System.Drawing.Size(1060, 796);
            this.mDataArea.ResumeLayout(false);
            this._tlayMain.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexD)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpP_Dt_Po.ResumeLayout(false);
            this.bpP_Plant.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPnlSearch1.ResumeLayout(false);
            this.bpPnlSearch1.PerformLayout();
            this.bpPanelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel _tlayMain;
        private Duzon.Common.Controls.DropDownComboBox m_cboCdPlant;
        private Duzon.Common.Controls.LabelExt lbl발주기간;
        private Duzon.Common.Controls.LabelExt lbl공장;
        private Dass.FlexGrid.FlexGrid _flexM;
        private Dass.FlexGrid.FlexGrid _flexD;
        private Duzon.Common.BpControls.BpCodeTextBox bp외주처;
        private Duzon.Common.Controls.RoundedButton btn삭제;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpPanelControl bpP_Dt_Po;
        private Duzon.Common.BpControls.BpPanelControl bpP_Plant;
        private Duzon.Common.Controls.PeriodPicker dtp발주기간;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.BpControls.BpCodeTextBox bp작업지시번호;
        private Duzon.Common.Controls.LabelExt lbl작업지시번호;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.BpControls.BpPanelControl bpPnlSearch1;
        private System.Windows.Forms.TextBox txt검색1;
        private Duzon.Common.Controls.DropDownComboBox cboSearch1;
        private Duzon.Common.Controls.LabelExt lbl외주처;
    }
}
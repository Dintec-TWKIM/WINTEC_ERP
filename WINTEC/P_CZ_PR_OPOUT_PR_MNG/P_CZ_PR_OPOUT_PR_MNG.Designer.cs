
namespace cz
{
    partial class P_CZ_PR_OPOUT_PR_MNG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PR_OPOUT_PR_MNG));
            this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
            this._flexM = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx작업지시번호 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl작업지시번호 = new Duzon.Common.Controls.LabelExt();
            this.bpP_Dt_Po = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp요청기간 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl요청기간 = new Duzon.Common.Controls.LabelExt();
            this.bpP_Plant = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl공장 = new Duzon.Common.Controls.LabelExt();
            this.cbo공장 = new Duzon.Common.Controls.DropDownComboBox();
            this.mDataArea.SuspendLayout();
            this._tlayMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexM)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.bpP_Dt_Po.SuspendLayout();
            this.bpP_Plant.SuspendLayout();
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
            this._tlayMain.Controls.Add(this._flexM, 0, 1);
            this._tlayMain.Controls.Add(this.oneGrid1, 0, 0);
            this._tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tlayMain.Location = new System.Drawing.Point(0, 0);
            this._tlayMain.Name = "_tlayMain";
            this._tlayMain.RowCount = 2;
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._tlayMain.Size = new System.Drawing.Size(1060, 756);
            this._tlayMain.TabIndex = 139;
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
            this._flexM.Location = new System.Drawing.Point(3, 49);
            this._flexM.Name = "_flexM";
            this._flexM.Rows.Count = 1;
            this._flexM.Rows.DefaultSize = 20;
            this._flexM.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexM.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexM.ShowSort = false;
            this._flexM.Size = new System.Drawing.Size(1054, 704);
            this._flexM.StyleInfo = resources.GetString("_flexM.StyleInfo");
            this._flexM.TabIndex = 7;
            this._flexM.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(1054, 40);
            this.oneGrid1.TabIndex = 131;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.Controls.Add(this.bpP_Dt_Po);
            this.oneGridItem1.Controls.Add(this.bpP_Plant);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(1044, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.ctx작업지시번호);
            this.bpPanelControl1.Controls.Add(this.lbl작업지시번호);
            this.bpPanelControl1.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // ctx작업지시번호
            // 
            this.ctx작업지시번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx작업지시번호.HelpID = Duzon.Common.Forms.Help.HelpID.P_PR_WO_REG_SUB;
            this.ctx작업지시번호.Location = new System.Drawing.Point(107, 0);
            this.ctx작업지시번호.Name = "ctx작업지시번호";
            this.ctx작업지시번호.Size = new System.Drawing.Size(185, 21);
            this.ctx작업지시번호.TabIndex = 3;
            this.ctx작업지시번호.TabStop = false;
            this.ctx작업지시번호.Text = "bpCodeTextBox1";
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
            // bpP_Dt_Po
            // 
            this.bpP_Dt_Po.Controls.Add(this.dtp요청기간);
            this.bpP_Dt_Po.Controls.Add(this.lbl요청기간);
            this.bpP_Dt_Po.Location = new System.Drawing.Point(296, 1);
            this.bpP_Dt_Po.Name = "bpP_Dt_Po";
            this.bpP_Dt_Po.Size = new System.Drawing.Size(292, 23);
            this.bpP_Dt_Po.TabIndex = 4;
            this.bpP_Dt_Po.Text = "bpPanelControl5";
            // 
            // dtp요청기간
            // 
            this.dtp요청기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp요청기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp요청기간.IsNecessaryCondition = true;
            this.dtp요청기간.Location = new System.Drawing.Point(107, 0);
            this.dtp요청기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp요청기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp요청기간.Name = "dtp요청기간";
            this.dtp요청기간.Size = new System.Drawing.Size(185, 21);
            this.dtp요청기간.TabIndex = 248;
            // 
            // lbl요청기간
            // 
            this.lbl요청기간.BackColor = System.Drawing.Color.Transparent;
            this.lbl요청기간.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl요청기간.Location = new System.Drawing.Point(0, 0);
            this.lbl요청기간.Name = "lbl요청기간";
            this.lbl요청기간.Size = new System.Drawing.Size(100, 23);
            this.lbl요청기간.TabIndex = 3;
            this.lbl요청기간.Text = "요청기간";
            this.lbl요청기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpP_Plant
            // 
            this.bpP_Plant.Controls.Add(this.lbl공장);
            this.bpP_Plant.Controls.Add(this.cbo공장);
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
            // cbo공장
            // 
            this.cbo공장.AutoDropDown = false;
            this.cbo공장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.cbo공장.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo공장.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo공장.ItemHeight = 12;
            this.cbo공장.Location = new System.Drawing.Point(107, 0);
            this.cbo공장.Name = "cbo공장";
            this.cbo공장.Size = new System.Drawing.Size(185, 20);
            this.cbo공장.TabIndex = 1;
            this.cbo공장.UseKeyF3 = false;
            // 
            // P_CZ_PR_OPOUT_PR_MNG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "P_CZ_PR_OPOUT_PR_MNG";
            this.Size = new System.Drawing.Size(1060, 796);
            this.mDataArea.ResumeLayout(false);
            this._tlayMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexM)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpP_Dt_Po.ResumeLayout(false);
            this.bpP_Plant.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel _tlayMain;
        private Duzon.Common.Controls.DropDownComboBox cbo공장;
        private Duzon.Common.Controls.LabelExt lbl요청기간;
        private Duzon.Common.Controls.LabelExt lbl공장;
        private Dass.FlexGrid.FlexGrid _flexM;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpP_Dt_Po;
        private Duzon.Common.BpControls.BpPanelControl bpP_Plant;
        private Duzon.Common.Controls.PeriodPicker dtp요청기간;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.BpControls.BpCodeTextBox ctx작업지시번호;
        private Duzon.Common.Controls.LabelExt lbl작업지시번호;
    }
}
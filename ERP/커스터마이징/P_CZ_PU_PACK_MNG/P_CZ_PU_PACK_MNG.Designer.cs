namespace cz
{
    partial class P_CZ_PU_PACK_MNG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_PU_PACK_MNG));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt발주번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl발주번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx회사 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl회사 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp포장일자 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl포장일자 = new Duzon.Common.Controls.LabelExt();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(904, 579);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 168F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(904, 579);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(898, 40);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.Controls.Add(this.bpPanelControl2);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(888, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.txt발주번호);
			this.bpPanelControl3.Controls.Add(this.lbl발주번호);
			this.bpPanelControl3.Location = new System.Drawing.Point(588, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(291, 23);
			this.bpPanelControl3.TabIndex = 0;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// txt발주번호
			// 
			this.txt발주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt발주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt발주번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt발주번호.Location = new System.Drawing.Point(106, 0);
			this.txt발주번호.Name = "txt발주번호";
			this.txt발주번호.Size = new System.Drawing.Size(185, 21);
			this.txt발주번호.TabIndex = 1;
			// 
			// lbl발주번호
			// 
			this.lbl발주번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl발주번호.Location = new System.Drawing.Point(0, 0);
			this.lbl발주번호.Name = "lbl발주번호";
			this.lbl발주번호.Size = new System.Drawing.Size(100, 23);
			this.lbl발주번호.TabIndex = 0;
			this.lbl발주번호.Text = "발주번호";
			this.lbl발주번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.ctx회사);
			this.bpPanelControl2.Controls.Add(this.lbl회사);
			this.bpPanelControl2.Location = new System.Drawing.Point(295, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(291, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// ctx회사
			// 
			this.ctx회사.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx회사.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx회사.Location = new System.Drawing.Point(106, 0);
			this.ctx회사.Name = "ctx회사";
			this.ctx회사.SetDefaultValue = true;
			this.ctx회사.Size = new System.Drawing.Size(185, 21);
			this.ctx회사.TabIndex = 1;
			this.ctx회사.TabStop = false;
			this.ctx회사.Text = "bpCodeTextBox1";
			this.ctx회사.UserCodeName = "NM_COMPANY";
			this.ctx회사.UserCodeValue = "CD_COMPANY";
			this.ctx회사.UserHelpID = "H_CZ_MA_COMPANY_SUB";
			this.ctx회사.UserParams = "회사;H_CZ_MA_COMPANY_SUB";
			// 
			// lbl회사
			// 
			this.lbl회사.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl회사.Location = new System.Drawing.Point(0, 0);
			this.lbl회사.Name = "lbl회사";
			this.lbl회사.Size = new System.Drawing.Size(100, 23);
			this.lbl회사.TabIndex = 0;
			this.lbl회사.Text = "회사";
			this.lbl회사.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.dtp포장일자);
			this.bpPanelControl1.Controls.Add(this.lbl포장일자);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(291, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// dtp포장일자
			// 
			this.dtp포장일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp포장일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp포장일자.IsNecessaryCondition = true;
			this.dtp포장일자.Location = new System.Drawing.Point(106, 0);
			this.dtp포장일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp포장일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp포장일자.Name = "dtp포장일자";
			this.dtp포장일자.Size = new System.Drawing.Size(185, 21);
			this.dtp포장일자.TabIndex = 1;
			// 
			// lbl포장일자
			// 
			this.lbl포장일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl포장일자.Location = new System.Drawing.Point(0, 0);
			this.lbl포장일자.Name = "lbl포장일자";
			this.lbl포장일자.Size = new System.Drawing.Size(100, 23);
			this.lbl포장일자.TabIndex = 0;
			this.lbl포장일자.Text = "포장일자";
			this.lbl포장일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 49);
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
			this.splitContainer1.Size = new System.Drawing.Size(898, 527);
			this.splitContainer1.SplitterDistance = 241;
			this.splitContainer1.TabIndex = 1;
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
			this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexH.Location = new System.Drawing.Point(0, 0);
			this._flexH.Name = "_flexH";
			this._flexH.Rows.Count = 1;
			this._flexH.Rows.DefaultSize = 20;
			this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexH.ShowSort = false;
			this._flexH.Size = new System.Drawing.Size(898, 241);
			this._flexH.StyleInfo = resources.GetString("_flexH.StyleInfo");
			this._flexH.TabIndex = 0;
			this._flexH.UseGridCalculator = true;
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
			this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flexL.Location = new System.Drawing.Point(0, 0);
			this._flexL.Name = "_flexL";
			this._flexL.Rows.Count = 1;
			this._flexL.Rows.DefaultSize = 20;
			this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flexL.ShowSort = false;
			this._flexL.Size = new System.Drawing.Size(898, 282);
			this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
			this._flexL.TabIndex = 0;
			this._flexL.UseGridCalculator = true;
			// 
			// P_CZ_PU_PACK_MNG
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_PU_PACK_MNG";
			this.Size = new System.Drawing.Size(904, 619);
			this.TitleText = "포장관리(입고)";
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.PeriodPicker dtp포장일자;
        private Duzon.Common.Controls.LabelExt lbl포장일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpCodeTextBox ctx회사;
        private Duzon.Common.Controls.LabelExt lbl회사;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.TextBoxExt txt발주번호;
        private Duzon.Common.Controls.LabelExt lbl발주번호;
    }
}
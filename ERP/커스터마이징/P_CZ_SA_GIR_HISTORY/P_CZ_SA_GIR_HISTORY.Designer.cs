namespace cz
{
    partial class P_CZ_SA_GIR_HISTORY
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_GIR_HISTORY));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp삭제일자 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl삭제일자 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx회사 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl회사 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt품목코드 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl품목코드 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt수주번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl수주번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt협조전번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl협조전번호 = new Duzon.Common.Controls.LabelExt();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
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
			this.mDataArea.Size = new System.Drawing.Size(1124, 579);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1124, 579);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// oneGrid1
			// 
			this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
			this.oneGrid1.Location = new System.Drawing.Point(3, 3);
			this.oneGrid1.Name = "oneGrid1";
			this.oneGrid1.Size = new System.Drawing.Size(1118, 63);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl5);
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1108, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.dtp삭제일자);
			this.bpPanelControl5.Controls.Add(this.lbl삭제일자);
			this.bpPanelControl5.Location = new System.Drawing.Point(295, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(291, 23);
			this.bpPanelControl5.TabIndex = 2;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// dtp삭제일자
			// 
			this.dtp삭제일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp삭제일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp삭제일자.IsNecessaryCondition = true;
			this.dtp삭제일자.Location = new System.Drawing.Point(106, 0);
			this.dtp삭제일자.MaximumSize = new System.Drawing.Size(185, 21);
			this.dtp삭제일자.MinimumSize = new System.Drawing.Size(185, 21);
			this.dtp삭제일자.Name = "dtp삭제일자";
			this.dtp삭제일자.Size = new System.Drawing.Size(185, 21);
			this.dtp삭제일자.TabIndex = 1;
			// 
			// lbl삭제일자
			// 
			this.lbl삭제일자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl삭제일자.Location = new System.Drawing.Point(0, 0);
			this.lbl삭제일자.Name = "lbl삭제일자";
			this.lbl삭제일자.Size = new System.Drawing.Size(100, 23);
			this.lbl삭제일자.TabIndex = 0;
			this.lbl삭제일자.Text = "삭제일자";
			this.lbl삭제일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.ctx회사);
			this.bpPanelControl1.Controls.Add(this.lbl회사);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(291, 23);
			this.bpPanelControl1.TabIndex = 0;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// ctx회사
			// 
			this.ctx회사.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx회사.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx회사.Location = new System.Drawing.Point(106, 0);
			this.ctx회사.Name = "ctx회사";
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
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl4);
			this.oneGridItem2.Controls.Add(this.bpPanelControl3);
			this.oneGridItem2.Controls.Add(this.bpPanelControl2);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1108, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.txt품목코드);
			this.bpPanelControl4.Controls.Add(this.lbl품목코드);
			this.bpPanelControl4.Location = new System.Drawing.Point(588, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(291, 23);
			this.bpPanelControl4.TabIndex = 3;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// txt품목코드
			// 
			this.txt품목코드.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt품목코드.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt품목코드.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt품목코드.Location = new System.Drawing.Point(106, 0);
			this.txt품목코드.Name = "txt품목코드";
			this.txt품목코드.Size = new System.Drawing.Size(185, 21);
			this.txt품목코드.TabIndex = 1;
			// 
			// lbl품목코드
			// 
			this.lbl품목코드.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl품목코드.Location = new System.Drawing.Point(0, 0);
			this.lbl품목코드.Name = "lbl품목코드";
			this.lbl품목코드.Size = new System.Drawing.Size(100, 23);
			this.lbl품목코드.TabIndex = 0;
			this.lbl품목코드.Text = "품목코드";
			this.lbl품목코드.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.txt수주번호);
			this.bpPanelControl3.Controls.Add(this.lbl수주번호);
			this.bpPanelControl3.Location = new System.Drawing.Point(295, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(291, 23);
			this.bpPanelControl3.TabIndex = 2;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// txt수주번호
			// 
			this.txt수주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt수주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt수주번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt수주번호.Location = new System.Drawing.Point(106, 0);
			this.txt수주번호.Name = "txt수주번호";
			this.txt수주번호.Size = new System.Drawing.Size(185, 21);
			this.txt수주번호.TabIndex = 1;
			// 
			// lbl수주번호
			// 
			this.lbl수주번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl수주번호.Location = new System.Drawing.Point(0, 0);
			this.lbl수주번호.Name = "lbl수주번호";
			this.lbl수주번호.Size = new System.Drawing.Size(100, 23);
			this.lbl수주번호.TabIndex = 0;
			this.lbl수주번호.Text = "수주번호";
			this.lbl수주번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.txt협조전번호);
			this.bpPanelControl2.Controls.Add(this.lbl협조전번호);
			this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(291, 23);
			this.bpPanelControl2.TabIndex = 1;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// txt협조전번호
			// 
			this.txt협조전번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt협조전번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt협조전번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt협조전번호.Location = new System.Drawing.Point(106, 0);
			this.txt협조전번호.Name = "txt협조전번호";
			this.txt협조전번호.Size = new System.Drawing.Size(185, 21);
			this.txt협조전번호.TabIndex = 1;
			// 
			// lbl협조전번호
			// 
			this.lbl협조전번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl협조전번호.Location = new System.Drawing.Point(0, 0);
			this.lbl협조전번호.Name = "lbl협조전번호";
			this.lbl협조전번호.Size = new System.Drawing.Size(100, 23);
			this.lbl협조전번호.TabIndex = 0;
			this.lbl협조전번호.Text = "협조전번호";
			this.lbl협조전번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 72);
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
			this.splitContainer1.Size = new System.Drawing.Size(1118, 504);
			this.splitContainer1.SplitterDistance = 282;
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
			this._flexH.Size = new System.Drawing.Size(1118, 282);
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
			this._flexL.Size = new System.Drawing.Size(1118, 218);
			this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
			this._flexL.TabIndex = 0;
			this._flexL.UseGridCalculator = true;
			// 
			// P_CZ_SA_GIR_HISTORY
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Name = "P_CZ_SA_GIR_HISTORY";
			this.Size = new System.Drawing.Size(1124, 619);
			this.TitleText = "협조전이력조회";
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl4.PerformLayout();
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl2.PerformLayout();
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
        private Duzon.Common.BpControls.BpCodeTextBox ctx회사;
        private Duzon.Common.Controls.LabelExt lbl회사;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.TextBoxExt txt품목코드;
        private Duzon.Common.Controls.LabelExt lbl품목코드;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.TextBoxExt txt수주번호;
        private Duzon.Common.Controls.LabelExt lbl수주번호;
        private Duzon.Common.Controls.TextBoxExt txt협조전번호;
        private Duzon.Common.Controls.LabelExt lbl협조전번호;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.Controls.PeriodPicker dtp삭제일자;
        private Duzon.Common.Controls.LabelExt lbl삭제일자;
    }
}
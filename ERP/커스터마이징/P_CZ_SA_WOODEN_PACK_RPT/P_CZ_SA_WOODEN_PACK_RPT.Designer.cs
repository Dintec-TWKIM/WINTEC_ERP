namespace cz
{
    partial class P_CZ_SA_WOODEN_PACK_RPT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_WOODEN_PACK_RPT));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt수주번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl수주번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt협조전번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl협조전번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp포장일자 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl포장일자 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx회사 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl회사 = new Duzon.Common.Controls.LabelExt();
			this._flex = new Dass.FlexGrid.FlexGrid(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn회계전표취소 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn회계전표처리 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mDataArea
			// 
			this.mDataArea.Controls.Add(this.tableLayoutPanel1);
			this.mDataArea.Size = new System.Drawing.Size(1137, 683);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1137, 683);
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
			this.oneGrid1.Size = new System.Drawing.Size(1131, 63);
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
			this.oneGridItem1.Size = new System.Drawing.Size(1121, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.txt수주번호);
			this.bpPanelControl3.Controls.Add(this.lbl수주번호);
			this.bpPanelControl3.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 3;
			this.bpPanelControl3.Text = "bpPanelControl3";
			// 
			// txt수주번호
			// 
			this.txt수주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt수주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt수주번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt수주번호.Location = new System.Drawing.Point(106, 0);
			this.txt수주번호.Name = "txt수주번호";
			this.txt수주번호.Size = new System.Drawing.Size(186, 21);
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
			this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 2;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// txt협조전번호
			// 
			this.txt협조전번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt협조전번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt협조전번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt협조전번호.Location = new System.Drawing.Point(106, 0);
			this.txt협조전번호.Name = "txt협조전번호";
			this.txt협조전번호.Size = new System.Drawing.Size(186, 21);
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
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.dtp포장일자);
			this.bpPanelControl1.Controls.Add(this.lbl포장일자);
			this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl1.TabIndex = 1;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// dtp포장일자
			// 
			this.dtp포장일자.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dtp포장일자.Dock = System.Windows.Forms.DockStyle.Right;
			this.dtp포장일자.IsNecessaryCondition = true;
			this.dtp포장일자.Location = new System.Drawing.Point(107, 0);
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
			// oneGridItem2
			// 
			this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem2.Controls.Add(this.bpPanelControl4);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1121, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.ctx회사);
			this.bpPanelControl4.Controls.Add(this.lbl회사);
			this.bpPanelControl4.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl4.TabIndex = 3;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// ctx회사
			// 
			this.ctx회사.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx회사.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.ctx회사.Location = new System.Drawing.Point(107, 0);
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
			// _flex
			// 
			this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
			this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
			this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
			this._flex.AutoResize = false;
			this._flex.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
			this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
			this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
			this._flex.EnabledHeaderCheck = true;
			this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
			this._flex.Location = new System.Drawing.Point(3, 72);
			this._flex.Name = "_flex";
			this._flex.Rows.Count = 1;
			this._flex.Rows.DefaultSize = 20;
			this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
			this._flex.ShowSort = false;
			this._flex.Size = new System.Drawing.Size(1131, 608);
			this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
			this._flex.TabIndex = 1;
			this._flex.UseGridCalculator = true;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Controls.Add(this.btn회계전표취소);
			this.flowLayoutPanel1.Controls.Add(this.btn회계전표처리);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(883, 6);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(251, 27);
			this.flowLayoutPanel1.TabIndex = 3;
			// 
			// btn회계전표취소
			// 
			this.btn회계전표취소.BackColor = System.Drawing.Color.Transparent;
			this.btn회계전표취소.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn회계전표취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn회계전표취소.Location = new System.Drawing.Point(141, 3);
			this.btn회계전표취소.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn회계전표취소.Name = "btn회계전표취소";
			this.btn회계전표취소.Size = new System.Drawing.Size(107, 19);
			this.btn회계전표취소.TabIndex = 0;
			this.btn회계전표취소.TabStop = false;
			this.btn회계전표취소.Text = "회계전표취소";
			this.btn회계전표취소.UseVisualStyleBackColor = false;
			// 
			// btn회계전표처리
			// 
			this.btn회계전표처리.BackColor = System.Drawing.Color.Transparent;
			this.btn회계전표처리.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn회계전표처리.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn회계전표처리.Location = new System.Drawing.Point(29, 3);
			this.btn회계전표처리.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn회계전표처리.Name = "btn회계전표처리";
			this.btn회계전표처리.Size = new System.Drawing.Size(106, 19);
			this.btn회계전표처리.TabIndex = 1;
			this.btn회계전표처리.TabStop = false;
			this.btn회계전표처리.Text = "회계전표처리";
			this.btn회계전표처리.UseVisualStyleBackColor = false;
			// 
			// P_CZ_SA_WOODEN_PACK_RPT
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.flowLayoutPanel1);
			this.Name = "P_CZ_SA_WOODEN_PACK_RPT";
			this.Size = new System.Drawing.Size(1137, 723);
			this.TitleText = "P_CZ_SA_WOODEN_PACK_RPT";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.bpPanelControl3.PerformLayout();
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl2.PerformLayout();
			this.bpPanelControl1.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.PeriodPicker dtp포장일자;
        private Duzon.Common.Controls.LabelExt lbl포장일자;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.TextBoxExt txt협조전번호;
        private Duzon.Common.Controls.LabelExt lbl협조전번호;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.TextBoxExt txt수주번호;
        private Duzon.Common.Controls.LabelExt lbl수주번호;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpCodeTextBox ctx회사;
        private Duzon.Common.Controls.LabelExt lbl회사;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn회계전표취소;
        private Duzon.Common.Controls.RoundedButton btn회계전표처리;
    }
}
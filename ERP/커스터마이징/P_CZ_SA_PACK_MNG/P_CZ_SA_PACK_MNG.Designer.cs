namespace cz
{
    partial class P_CZ_SA_PACK_MNG
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_PACK_MNG));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
			this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt의뢰번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl의뢰번호 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
			this.bpc회사 = new Duzon.Common.BpControls.BpComboBox();
			this.lbl회사 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
			this.dtp포장일자 = new Duzon.Common.Controls.PeriodPicker();
			this.lbl포장일자 = new Duzon.Common.Controls.LabelExt();
			this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
			this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
			this.ctx담당자 = new Duzon.Common.BpControls.BpCodeTextBox();
			this.lbl담당자 = new Duzon.Common.Controls.LabelExt();
			this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
			this.txt수주번호 = new Duzon.Common.Controls.TextBoxExt();
			this.lbl수주번호 = new Duzon.Common.Controls.LabelExt();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
			this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
			this.btn포장명세서출력 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn상업송장출력 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.btn인수증출력 = new Duzon.Common.Controls.RoundedButton(this.components);
			this.mDataArea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.oneGridItem1.SuspendLayout();
			this.bpPanelControl1.SuspendLayout();
			this.bpPanelControl4.SuspendLayout();
			this.bpPanelControl3.SuspendLayout();
			this.oneGridItem2.SuspendLayout();
			this.bpPanelControl2.SuspendLayout();
			this.bpPanelControl5.SuspendLayout();
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
			this.mDataArea.Size = new System.Drawing.Size(1632, 724);
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
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 176F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1632, 724);
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
			this.oneGrid1.Size = new System.Drawing.Size(1626, 64);
			this.oneGrid1.TabIndex = 0;
			// 
			// oneGridItem1
			// 
			this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.oneGridItem1.Controls.Add(this.bpPanelControl1);
			this.oneGridItem1.Controls.Add(this.bpPanelControl4);
			this.oneGridItem1.Controls.Add(this.bpPanelControl3);
			this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
			this.oneGridItem1.Name = "oneGridItem1";
			this.oneGridItem1.Size = new System.Drawing.Size(1616, 23);
			this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem1.TabIndex = 0;
			// 
			// bpPanelControl1
			// 
			this.bpPanelControl1.Controls.Add(this.txt의뢰번호);
			this.bpPanelControl1.Controls.Add(this.lbl의뢰번호);
			this.bpPanelControl1.Location = new System.Drawing.Point(590, 1);
			this.bpPanelControl1.Name = "bpPanelControl1";
			this.bpPanelControl1.Size = new System.Drawing.Size(281, 23);
			this.bpPanelControl1.TabIndex = 1;
			this.bpPanelControl1.Text = "bpPanelControl1";
			// 
			// txt의뢰번호
			// 
			this.txt의뢰번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt의뢰번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt의뢰번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt의뢰번호.Location = new System.Drawing.Point(106, 0);
			this.txt의뢰번호.Name = "txt의뢰번호";
			this.txt의뢰번호.Size = new System.Drawing.Size(175, 21);
			this.txt의뢰번호.TabIndex = 1;
			// 
			// lbl의뢰번호
			// 
			this.lbl의뢰번호.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl의뢰번호.Location = new System.Drawing.Point(0, 0);
			this.lbl의뢰번호.Name = "lbl의뢰번호";
			this.lbl의뢰번호.Size = new System.Drawing.Size(100, 23);
			this.lbl의뢰번호.TabIndex = 0;
			this.lbl의뢰번호.Text = "의뢰번호";
			this.lbl의뢰번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl4
			// 
			this.bpPanelControl4.Controls.Add(this.bpc회사);
			this.bpPanelControl4.Controls.Add(this.lbl회사);
			this.bpPanelControl4.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl4.Name = "bpPanelControl4";
			this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl4.TabIndex = 0;
			this.bpPanelControl4.Text = "bpPanelControl4";
			// 
			// bpc회사
			// 
			this.bpc회사.Dock = System.Windows.Forms.DockStyle.Right;
			this.bpc회사.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
			this.bpc회사.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.bpc회사.Location = new System.Drawing.Point(106, 0);
			this.bpc회사.Name = "bpc회사";
			this.bpc회사.SetDefaultValue = true;
			this.bpc회사.Size = new System.Drawing.Size(186, 21);
			this.bpc회사.TabIndex = 1;
			this.bpc회사.TabStop = false;
			this.bpc회사.Text = "bpComboBox1";
			this.bpc회사.UserCodeName = "NM_COMPANY";
			this.bpc회사.UserCodeValue = "CD_COMPANY";
			this.bpc회사.UserHelpID = "H_CZ_MA_COMPANY_SUB";
			this.bpc회사.UserParams = "회사;H_CZ_MA_COMPANY_SUB";
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
			// bpPanelControl3
			// 
			this.bpPanelControl3.Controls.Add(this.dtp포장일자);
			this.bpPanelControl3.Controls.Add(this.lbl포장일자);
			this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl3.Name = "bpPanelControl3";
			this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl3.TabIndex = 3;
			this.bpPanelControl3.Text = "bpPanelControl3";
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
			this.oneGridItem2.Controls.Add(this.bpPanelControl2);
			this.oneGridItem2.Controls.Add(this.bpPanelControl5);
			this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
			this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
			this.oneGridItem2.Name = "oneGridItem2";
			this.oneGridItem2.Size = new System.Drawing.Size(1616, 23);
			this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
			this.oneGridItem2.TabIndex = 1;
			// 
			// bpPanelControl2
			// 
			this.bpPanelControl2.Controls.Add(this.ctx담당자);
			this.bpPanelControl2.Controls.Add(this.lbl담당자);
			this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
			this.bpPanelControl2.Name = "bpPanelControl2";
			this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl2.TabIndex = 2;
			this.bpPanelControl2.Text = "bpPanelControl2";
			// 
			// ctx담당자
			// 
			this.ctx담당자.Dock = System.Windows.Forms.DockStyle.Right;
			this.ctx담당자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
			this.ctx담당자.Location = new System.Drawing.Point(106, 0);
			this.ctx담당자.Name = "ctx담당자";
			this.ctx담당자.Size = new System.Drawing.Size(186, 21);
			this.ctx담당자.TabIndex = 1;
			this.ctx담당자.TabStop = false;
			this.ctx담당자.Text = "bpCodeTextBox1";
			// 
			// lbl담당자
			// 
			this.lbl담당자.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl담당자.Location = new System.Drawing.Point(0, 0);
			this.lbl담당자.Name = "lbl담당자";
			this.lbl담당자.Size = new System.Drawing.Size(100, 23);
			this.lbl담당자.TabIndex = 0;
			this.lbl담당자.Text = "담당자";
			this.lbl담당자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bpPanelControl5
			// 
			this.bpPanelControl5.Controls.Add(this.txt수주번호);
			this.bpPanelControl5.Controls.Add(this.lbl수주번호);
			this.bpPanelControl5.Location = new System.Drawing.Point(2, 1);
			this.bpPanelControl5.Name = "bpPanelControl5";
			this.bpPanelControl5.Size = new System.Drawing.Size(292, 23);
			this.bpPanelControl5.TabIndex = 1;
			this.bpPanelControl5.Text = "bpPanelControl5";
			// 
			// txt수주번호
			// 
			this.txt수주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
			this.txt수주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txt수주번호.Dock = System.Windows.Forms.DockStyle.Right;
			this.txt수주번호.Location = new System.Drawing.Point(107, 0);
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
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 73);
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
			this.splitContainer1.Size = new System.Drawing.Size(1626, 648);
			this.splitContainer1.SplitterDistance = 352;
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
			this._flexH.Size = new System.Drawing.Size(1626, 352);
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
			this._flexL.Size = new System.Drawing.Size(1626, 292);
			this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
			this._flexL.TabIndex = 0;
			this._flexL.UseGridCalculator = true;
			// 
			// btn포장명세서출력
			// 
			this.btn포장명세서출력.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn포장명세서출력.BackColor = System.Drawing.Color.Transparent;
			this.btn포장명세서출력.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn포장명세서출력.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn포장명세서출력.Location = new System.Drawing.Point(1338, 10);
			this.btn포장명세서출력.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn포장명세서출력.Name = "btn포장명세서출력";
			this.btn포장명세서출력.Size = new System.Drawing.Size(97, 19);
			this.btn포장명세서출력.TabIndex = 9;
			this.btn포장명세서출력.TabStop = false;
			this.btn포장명세서출력.Tag = "Packing List";
			this.btn포장명세서출력.Text = "포장명세서출력";
			this.btn포장명세서출력.UseVisualStyleBackColor = false;
			// 
			// btn상업송장출력
			// 
			this.btn상업송장출력.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn상업송장출력.BackColor = System.Drawing.Color.Transparent;
			this.btn상업송장출력.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn상업송장출력.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn상업송장출력.Location = new System.Drawing.Point(1441, 10);
			this.btn상업송장출력.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn상업송장출력.Name = "btn상업송장출력";
			this.btn상업송장출력.Size = new System.Drawing.Size(94, 19);
			this.btn상업송장출력.TabIndex = 10;
			this.btn상업송장출력.TabStop = false;
			this.btn상업송장출력.Tag = "Commercial Invoice";
			this.btn상업송장출력.Text = "상업송장출력";
			this.btn상업송장출력.UseVisualStyleBackColor = false;
			// 
			// btn인수증출력
			// 
			this.btn인수증출력.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn인수증출력.BackColor = System.Drawing.Color.Transparent;
			this.btn인수증출력.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btn인수증출력.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btn인수증출력.Location = new System.Drawing.Point(1541, 10);
			this.btn인수증출력.MaximumSize = new System.Drawing.Size(0, 19);
			this.btn인수증출력.Name = "btn인수증출력";
			this.btn인수증출력.Size = new System.Drawing.Size(88, 19);
			this.btn인수증출력.TabIndex = 11;
			this.btn인수증출력.TabStop = false;
			this.btn인수증출력.Text = "인수증출력";
			this.btn인수증출력.UseVisualStyleBackColor = false;
			// 
			// P_CZ_SA_PACK_MNG
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.btn인수증출력);
			this.Controls.Add(this.btn상업송장출력);
			this.Controls.Add(this.btn포장명세서출력);
			this.Name = "P_CZ_SA_PACK_MNG";
			this.Size = new System.Drawing.Size(1632, 764);
			this.TitleText = "포장관리(출고)";
			this.Controls.SetChildIndex(this.mDataArea, 0);
			this.Controls.SetChildIndex(this.btn포장명세서출력, 0);
			this.Controls.SetChildIndex(this.btn상업송장출력, 0);
			this.Controls.SetChildIndex(this.btn인수증출력, 0);
			this.mDataArea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.oneGridItem1.ResumeLayout(false);
			this.bpPanelControl1.ResumeLayout(false);
			this.bpPanelControl1.PerformLayout();
			this.bpPanelControl4.ResumeLayout(false);
			this.bpPanelControl3.ResumeLayout(false);
			this.oneGridItem2.ResumeLayout(false);
			this.bpPanelControl2.ResumeLayout(false);
			this.bpPanelControl5.ResumeLayout(false);
			this.bpPanelControl5.PerformLayout();
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
        private Duzon.Common.Controls.TextBoxExt txt의뢰번호;
        private Duzon.Common.Controls.LabelExt lbl의뢰번호;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.LabelExt lbl포장일자;
        private Duzon.Common.Controls.PeriodPicker dtp포장일자;
        private Duzon.Common.Controls.RoundedButton btn포장명세서출력;
        private Duzon.Common.Controls.RoundedButton btn상업송장출력;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.LabelExt lbl회사;
        private Duzon.Common.Controls.TextBoxExt txt수주번호;
        private Duzon.Common.Controls.LabelExt lbl수주번호;
        private Duzon.Common.Controls.RoundedButton btn인수증출력;
        private Duzon.Common.BpControls.BpComboBox bpc회사;
		private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
		private Duzon.Common.BpControls.BpCodeTextBox ctx담당자;
		private Duzon.Common.Controls.LabelExt lbl담당자;
	}
}
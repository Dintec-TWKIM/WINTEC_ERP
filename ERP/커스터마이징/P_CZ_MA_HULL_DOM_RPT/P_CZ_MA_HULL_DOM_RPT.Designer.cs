namespace cz
{
    partial class P_CZ_MA_HULL_DOM_RPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MA_HULL_DOM_RPT));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx운항선사 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl운항선사 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx호선 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl호선 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp조회기간 = new Duzon.Common.Controls.PeriodPicker();
            this.cbo조회기간 = new Duzon.Common.Controls.DropDownComboBox();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl5 = new Duzon.Common.BpControls.BpPanelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chk국내기항선박 = new Duzon.Common.Controls.CheckBoxExt();
            this.bpPanelControl6 = new Duzon.Common.BpControls.BpPanelControl();
            this.cbo국가 = new Duzon.Common.Controls.DropDownComboBox();
            this.lbl국가 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.lbl조선소 = new Duzon.Common.Controls.LabelExt();
            this.txt조선소 = new Duzon.Common.Controls.TextBoxExt();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl5.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.bpPanelControl6.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(910, 756);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(910, 756);
            this.tableLayoutPanel1.TabIndex = 0;
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
            this.splitContainer1.Size = new System.Drawing.Size(904, 681);
            this.splitContainer1.SplitterDistance = 415;
            this.splitContainer1.TabIndex = 0;
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
            this._flexH.Size = new System.Drawing.Size(904, 415);
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
            this._flexL.Size = new System.Drawing.Size(904, 262);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 0;
            this._flexL.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1,
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(904, 63);
            this.oneGrid1.TabIndex = 1;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl4);
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(894, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.ctx운항선사);
            this.bpPanelControl4.Controls.Add(this.lbl운항선사);
            this.bpPanelControl4.Location = new System.Drawing.Point(588, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl4.TabIndex = 1;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // ctx운항선사
            // 
            this.ctx운항선사.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx운항선사.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB;
            this.ctx운항선사.Location = new System.Drawing.Point(106, 0);
            this.ctx운항선사.Name = "ctx운항선사";
            this.ctx운항선사.Size = new System.Drawing.Size(185, 21);
            this.ctx운항선사.TabIndex = 1;
            this.ctx운항선사.TabStop = false;
            this.ctx운항선사.Text = "bpCodeTextBox1";
            // 
            // lbl운항선사
            // 
            this.lbl운항선사.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl운항선사.Location = new System.Drawing.Point(0, 0);
            this.lbl운항선사.Name = "lbl운항선사";
            this.lbl운항선사.Size = new System.Drawing.Size(100, 23);
            this.lbl운항선사.TabIndex = 0;
            this.lbl운항선사.Text = "운항선사";
            this.lbl운항선사.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.ctx호선);
            this.bpPanelControl2.Controls.Add(this.lbl호선);
            this.bpPanelControl2.Location = new System.Drawing.Point(295, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl2.TabIndex = 1;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // ctx호선
            // 
            this.ctx호선.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx호선.HelpID = Duzon.Common.Forms.Help.HelpID.P_USER;
            this.ctx호선.Location = new System.Drawing.Point(106, 0);
            this.ctx호선.Name = "ctx호선";
            this.ctx호선.Size = new System.Drawing.Size(185, 21);
            this.ctx호선.TabIndex = 1;
            this.ctx호선.TabStop = false;
            this.ctx호선.Text = "bpCodeTextBox1";
            this.ctx호선.UserCodeName = "NO_HULL";
            this.ctx호선.UserCodeValue = "NO_IMO";
            this.ctx호선.UserHelpID = "H_CZ_MA_HULL_SUB";
            this.ctx호선.UserParams = "호선;H_CZ_MA_HULL_SUB";
            // 
            // lbl호선
            // 
            this.lbl호선.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl호선.Location = new System.Drawing.Point(0, 0);
            this.lbl호선.Name = "lbl호선";
            this.lbl호선.Size = new System.Drawing.Size(100, 23);
            this.lbl호선.TabIndex = 0;
            this.lbl호선.Text = "호선";
            this.lbl호선.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp조회기간);
            this.bpPanelControl1.Controls.Add(this.cbo조회기간);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // dtp조회기간
            // 
            this.dtp조회기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp조회기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp조회기간.IsNecessaryCondition = true;
            this.dtp조회기간.Location = new System.Drawing.Point(106, 0);
            this.dtp조회기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp조회기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp조회기간.Name = "dtp조회기간";
            this.dtp조회기간.Size = new System.Drawing.Size(185, 21);
            this.dtp조회기간.TabIndex = 3;
            // 
            // cbo조회기간
            // 
            this.cbo조회기간.AutoDropDown = true;
            this.cbo조회기간.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbo조회기간.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo조회기간.FormattingEnabled = true;
            this.cbo조회기간.ItemHeight = 12;
            this.cbo조회기간.Location = new System.Drawing.Point(0, 0);
            this.cbo조회기간.Name = "cbo조회기간";
            this.cbo조회기간.Size = new System.Drawing.Size(100, 20);
            this.cbo조회기간.TabIndex = 2;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl5);
            this.oneGridItem2.Controls.Add(this.bpPanelControl6);
            this.oneGridItem2.Controls.Add(this.bpPanelControl3);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(894, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 1;
            // 
            // bpPanelControl5
            // 
            this.bpPanelControl5.Controls.Add(this.flowLayoutPanel1);
            this.bpPanelControl5.Location = new System.Drawing.Point(588, 1);
            this.bpPanelControl5.Name = "bpPanelControl5";
            this.bpPanelControl5.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl5.TabIndex = 2;
            this.bpPanelControl5.Text = "bpPanelControl5";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.chk국내기항선박);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(291, 23);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // chk국내기항선박
            // 
            this.chk국내기항선박.AutoSize = true;
            this.chk국내기항선박.Checked = true;
            this.chk국내기항선박.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk국내기항선박.Location = new System.Drawing.Point(3, 3);
            this.chk국내기항선박.Name = "chk국내기항선박";
            this.chk국내기항선박.Size = new System.Drawing.Size(96, 16);
            this.chk국내기항선박.TabIndex = 0;
            this.chk국내기항선박.Text = "국내기항선박";
            this.chk국내기항선박.TextDD = null;
            this.chk국내기항선박.UseVisualStyleBackColor = true;
            // 
            // bpPanelControl6
            // 
            this.bpPanelControl6.Controls.Add(this.cbo국가);
            this.bpPanelControl6.Controls.Add(this.lbl국가);
            this.bpPanelControl6.Location = new System.Drawing.Point(295, 1);
            this.bpPanelControl6.Name = "bpPanelControl6";
            this.bpPanelControl6.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl6.TabIndex = 2;
            this.bpPanelControl6.Text = "bpPanelControl6";
            // 
            // cbo국가
            // 
            this.cbo국가.AutoDropDown = true;
            this.cbo국가.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbo국가.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo국가.FormattingEnabled = true;
            this.cbo국가.ItemHeight = 12;
            this.cbo국가.Location = new System.Drawing.Point(106, 0);
            this.cbo국가.Name = "cbo국가";
            this.cbo국가.Size = new System.Drawing.Size(185, 20);
            this.cbo국가.TabIndex = 1;
            // 
            // lbl국가
            // 
            this.lbl국가.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl국가.Location = new System.Drawing.Point(0, 0);
            this.lbl국가.Name = "lbl국가";
            this.lbl국가.Size = new System.Drawing.Size(100, 23);
            this.lbl국가.TabIndex = 0;
            this.lbl국가.Text = "국가";
            this.lbl국가.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.txt조선소);
            this.bpPanelControl3.Controls.Add(this.lbl조선소);
            this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl3.TabIndex = 1;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // lbl조선소
            // 
            this.lbl조선소.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl조선소.Location = new System.Drawing.Point(0, 0);
            this.lbl조선소.Name = "lbl조선소";
            this.lbl조선소.Size = new System.Drawing.Size(100, 23);
            this.lbl조선소.TabIndex = 0;
            this.lbl조선소.Text = "조선소";
            this.lbl조선소.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt조선소
            // 
            this.txt조선소.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt조선소.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt조선소.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt조선소.Location = new System.Drawing.Point(106, 0);
            this.txt조선소.Name = "txt조선소";
            this.txt조선소.Size = new System.Drawing.Size(185, 21);
            this.txt조선소.TabIndex = 1;
            // 
            // P_CZ_MA_HULL_DOM_RPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "P_CZ_MA_HULL_DOM_RPT";
            this.Size = new System.Drawing.Size(910, 796);
            this.TitleText = "P_CZ_MA_HULL_DOM_RPT";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl5.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.bpPanelControl6.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.bpPanelControl3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl6;
        private Duzon.Common.Controls.LabelExt lbl국가;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.BpControls.BpCodeTextBox ctx운항선사;
        private Duzon.Common.Controls.LabelExt lbl운항선사;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl5;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.Controls.LabelExt lbl조선소;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.BpControls.BpCodeTextBox ctx호선;
        private Duzon.Common.Controls.LabelExt lbl호선;
        private Duzon.Common.Controls.PeriodPicker dtp조회기간;
        private Duzon.Common.Controls.DropDownComboBox cbo조회기간;
        private Duzon.Common.Controls.DropDownComboBox cbo국가;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.CheckBoxExt chk국내기항선박;
        private Duzon.Common.Controls.TextBoxExt txt조선소;
    }
}
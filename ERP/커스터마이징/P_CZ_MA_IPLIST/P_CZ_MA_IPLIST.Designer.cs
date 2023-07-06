namespace cz
{
    partial class P_CZ_MA_IPLIST
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MA_IPLIST));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.mebMAC = new Duzon.Common.Controls.MaskedEditBox();
            this.lblMAC = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.meb아이피 = new Duzon.Common.Controls.MaskedEditBox();
            this.lbl아이피 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx사용자 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl사용자 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl3 = new Duzon.Common.BpControls.BpPanelControl();
            this.ctx회사 = new Duzon.Common.BpControls.BpCodeTextBox();
            this.lbl회사 = new Duzon.Common.Controls.LabelExt();
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mebMAC)).BeginInit();
            this.bpPanelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meb아이피)).BeginInit();
            this.bpPanelControl1.SuspendLayout();
            this.bpPanelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Dock = System.Windows.Forms.DockStyle.None;
            this.mDataArea.Size = new System.Drawing.Size(1403, 579);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 508F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1403, 579);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.EnabledHeaderCheck = true;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(3, 48);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(1397, 528);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 1;
            this._flex.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(1397, 39);
            this.oneGrid1.TabIndex = 2;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl2);
            this.oneGridItem1.Controls.Add(this.bpPanelControl4);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.Controls.Add(this.bpPanelControl3);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(1387, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.mebMAC);
            this.bpPanelControl2.Controls.Add(this.lblMAC);
            this.bpPanelControl2.Location = new System.Drawing.Point(884, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 14;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // mebMAC
            // 
            this.mebMAC.AccessibleDescription = "MaskedEdit TextBox";
            this.mebMAC.AccessibleName = "MaskedEditBox";
            this.mebMAC.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.mebMAC.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.mebMAC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mebMAC.Culture = new System.Globalization.CultureInfo("ko-KR");
            this.mebMAC.Dock = System.Windows.Forms.DockStyle.Right;
            this.mebMAC.Location = new System.Drawing.Point(107, 0);
            this.mebMAC.Mask = "CC:CC:CC:CC:CC:CC";
            this.mebMAC.MaxLength = 17;
            this.mebMAC.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.mebMAC.Name = "mebMAC";
            this.mebMAC.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mebMAC.Size = new System.Drawing.Size(185, 21);
            this.mebMAC.TabIndex = 12;
            this.mebMAC.Tag = "NO_RES";
            this.mebMAC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblMAC
            // 
            this.lblMAC.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMAC.Location = new System.Drawing.Point(0, 0);
            this.lblMAC.Name = "lblMAC";
            this.lblMAC.Size = new System.Drawing.Size(100, 23);
            this.lblMAC.TabIndex = 0;
            this.lblMAC.Text = "MAC";
            this.lblMAC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.meb아이피);
            this.bpPanelControl4.Controls.Add(this.lbl아이피);
            this.bpPanelControl4.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl4.TabIndex = 13;
            this.bpPanelControl4.Text = "bpPanelControl4";
            // 
            // meb아이피
            // 
            this.meb아이피.AccessibleDescription = "MaskedEdit TextBox";
            this.meb아이피.AccessibleName = "MaskedEditBox";
            this.meb아이피.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.meb아이피.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.meb아이피.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.meb아이피.Culture = new System.Globalization.CultureInfo("ko-KR");
            this.meb아이피.Dock = System.Windows.Forms.DockStyle.Right;
            this.meb아이피.Location = new System.Drawing.Point(107, 0);
            this.meb아이피.Mask = "999.999.99#.99#";
            this.meb아이피.MaxLength = 15;
            this.meb아이피.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.meb아이피.Name = "meb아이피";
            this.meb아이피.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.meb아이피.Size = new System.Drawing.Size(185, 21);
            this.meb아이피.TabIndex = 11;
            this.meb아이피.Tag = "NO_RES";
            this.meb아이피.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl아이피
            // 
            this.lbl아이피.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl아이피.Location = new System.Drawing.Point(0, 0);
            this.lbl아이피.Name = "lbl아이피";
            this.lbl아이피.Size = new System.Drawing.Size(100, 23);
            this.lbl아이피.TabIndex = 0;
            this.lbl아이피.Text = "아이피";
            this.lbl아이피.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.ctx사용자);
            this.bpPanelControl1.Controls.Add(this.lbl사용자);
            this.bpPanelControl1.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 12;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // ctx사용자
            // 
            this.ctx사용자.BackColor = System.Drawing.Color.White;
            this.ctx사용자.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx사용자.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctx사용자.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB;
            this.ctx사용자.Location = new System.Drawing.Point(107, 0);
            this.ctx사용자.Name = "ctx사용자";
            this.ctx사용자.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.ctx사용자.Size = new System.Drawing.Size(185, 21);
            this.ctx사용자.TabIndex = 100;
            this.ctx사용자.TabStop = false;
            this.ctx사용자.Tag = "NO_EMP;NM_KOR";
            // 
            // lbl사용자
            // 
            this.lbl사용자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl사용자.Location = new System.Drawing.Point(0, 0);
            this.lbl사용자.Name = "lbl사용자";
            this.lbl사용자.Size = new System.Drawing.Size(100, 23);
            this.lbl사용자.TabIndex = 0;
            this.lbl사용자.Text = "사용자";
            this.lbl사용자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bpPanelControl3
            // 
            this.bpPanelControl3.Controls.Add(this.ctx회사);
            this.bpPanelControl3.Controls.Add(this.lbl회사);
            this.bpPanelControl3.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl3.Name = "bpPanelControl3";
            this.bpPanelControl3.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl3.TabIndex = 9;
            this.bpPanelControl3.Text = "bpPanelControl3";
            // 
            // ctx회사
            // 
            this.ctx회사.Dock = System.Windows.Forms.DockStyle.Right;
            this.ctx회사.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctx회사.HelpID = Duzon.Common.Forms.Help.HelpID.P_MA_COMPANY_SUB;
            this.ctx회사.Location = new System.Drawing.Point(107, 0);
            this.ctx회사.Name = "ctx회사";
            this.ctx회사.ResultMode = Duzon.Common.Forms.Help.ResultMode.SlowMode;
            this.ctx회사.Size = new System.Drawing.Size(185, 21);
            this.ctx회사.TabIndex = 100;
            this.ctx회사.TabStop = false;
            this.ctx회사.Tag = "NO_EMP;NM_KOR";
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
            // P_CZ_MA_IPLIST
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_CZ_MA_IPLIST";
            this.Size = new System.Drawing.Size(1403, 619);
            this.TitleText = "IP 관리";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mebMAC)).EndInit();
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meb아이피)).EndInit();
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl3;
        private Duzon.Common.BpControls.BpCodeTextBox ctx회사;
        private Duzon.Common.Controls.LabelExt lbl회사;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.MaskedEditBox mebMAC;
        private Duzon.Common.Controls.LabelExt lblMAC;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.MaskedEditBox meb아이피;
        private Duzon.Common.Controls.LabelExt lbl아이피;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.BpControls.BpCodeTextBox ctx사용자;
        private Duzon.Common.Controls.LabelExt lbl사용자;





    }
}
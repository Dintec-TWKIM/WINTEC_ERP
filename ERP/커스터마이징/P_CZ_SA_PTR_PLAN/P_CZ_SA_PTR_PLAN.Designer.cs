namespace cz
{
    partial class P_CZ_SA_PTR_PLAN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_PTR_PLAN));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp계획년도 = new Duzon.Common.Controls.DatePicker();
            this.lbl계획년도 = new Duzon.Common.Controls.LabelExt();
            this.tabControlExt1 = new Duzon.Common.Controls.TabControlExt();
            this.tpgCC = new System.Windows.Forms.TabPage();
            this._flexCC = new Dass.FlexGrid.FlexGrid(this.components);
            this.tpg매출처그룹 = new System.Windows.Forms.TabPage();
            this._flex매출처그룹 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tpg영업그룹 = new System.Windows.Forms.TabPage();
            this._flex영업그룹 = new Dass.FlexGrid.FlexGrid(this.components);
            this.tpg매출처 = new System.Windows.Forms.TabPage();
            this._flex매출처 = new Dass.FlexGrid.FlexGrid(this.components);
            this.mDataArea.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp계획년도)).BeginInit();
            this.tabControlExt1.SuspendLayout();
            this.tpgCC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexCC)).BeginInit();
            this.tpg매출처그룹.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex매출처그룹)).BeginInit();
            this.tpg영업그룹.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex영업그룹)).BeginInit();
            this.tpg매출처.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex매출처)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControlExt1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(827, 579);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(821, 40);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(811, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp계획년도);
            this.bpPanelControl1.Controls.Add(this.lbl계획년도);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(164, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // dtp계획년도
            // 
            this.dtp계획년도.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp계획년도.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp계획년도.Location = new System.Drawing.Point(106, 0);
            this.dtp계획년도.Mask = "####";
            this.dtp계획년도.MaskBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.dtp계획년도.MaxDate = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            this.dtp계획년도.MaximumSize = new System.Drawing.Size(0, 21);
            this.dtp계획년도.MinDate = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            this.dtp계획년도.Modified = true;
            this.dtp계획년도.Name = "dtp계획년도";
            this.dtp계획년도.ShowUpDown = true;
            this.dtp계획년도.Size = new System.Drawing.Size(58, 21);
            this.dtp계획년도.TabIndex = 1;
            this.dtp계획년도.Value = new System.DateTime(((long)(0)));
            // 
            // lbl계획년도
            // 
            this.lbl계획년도.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl계획년도.Location = new System.Drawing.Point(0, 0);
            this.lbl계획년도.Name = "lbl계획년도";
            this.lbl계획년도.Size = new System.Drawing.Size(100, 23);
            this.lbl계획년도.TabIndex = 0;
            this.lbl계획년도.Text = "계획년도";
            this.lbl계획년도.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControlExt1
            // 
            this.tabControlExt1.Controls.Add(this.tpgCC);
            this.tabControlExt1.Controls.Add(this.tpg매출처그룹);
            this.tabControlExt1.Controls.Add(this.tpg영업그룹);
            this.tabControlExt1.Controls.Add(this.tpg매출처);
            this.tabControlExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlExt1.Location = new System.Drawing.Point(3, 49);
            this.tabControlExt1.Name = "tabControlExt1";
            this.tabControlExt1.SelectedIndex = 0;
            this.tabControlExt1.Size = new System.Drawing.Size(821, 527);
            this.tabControlExt1.TabIndex = 1;
            // 
            // tpgCC
            // 
            this.tpgCC.Controls.Add(this._flexCC);
            this.tpgCC.Location = new System.Drawing.Point(4, 22);
            this.tpgCC.Name = "tpgCC";
            this.tpgCC.Padding = new System.Windows.Forms.Padding(3);
            this.tpgCC.Size = new System.Drawing.Size(813, 501);
            this.tpgCC.TabIndex = 0;
            this.tpgCC.Text = "C/C";
            this.tpgCC.UseVisualStyleBackColor = true;
            // 
            // _flexCC
            // 
            this._flexCC.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexCC.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexCC.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexCC.AutoResize = false;
            this._flexCC.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexCC.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexCC.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexCC.EnabledHeaderCheck = true;
            this._flexCC.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexCC.Location = new System.Drawing.Point(3, 3);
            this._flexCC.Name = "_flexCC";
            this._flexCC.Rows.Count = 1;
            this._flexCC.Rows.DefaultSize = 20;
            this._flexCC.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexCC.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexCC.ShowSort = false;
            this._flexCC.Size = new System.Drawing.Size(807, 495);
            this._flexCC.StyleInfo = resources.GetString("_flexCC.StyleInfo");
            this._flexCC.TabIndex = 0;
            this._flexCC.UseGridCalculator = true;
            // 
            // tpg매출처그룹
            // 
            this.tpg매출처그룹.Controls.Add(this._flex매출처그룹);
            this.tpg매출처그룹.Location = new System.Drawing.Point(4, 22);
            this.tpg매출처그룹.Name = "tpg매출처그룹";
            this.tpg매출처그룹.Padding = new System.Windows.Forms.Padding(3);
            this.tpg매출처그룹.Size = new System.Drawing.Size(813, 476);
            this.tpg매출처그룹.TabIndex = 1;
            this.tpg매출처그룹.Text = "매출처그룹";
            this.tpg매출처그룹.UseVisualStyleBackColor = true;
            // 
            // _flex매출처그룹
            // 
            this._flex매출처그룹.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex매출처그룹.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex매출처그룹.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex매출처그룹.AutoResize = false;
            this._flex매출처그룹.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex매출처그룹.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex매출처그룹.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex매출처그룹.EnabledHeaderCheck = true;
            this._flex매출처그룹.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex매출처그룹.Location = new System.Drawing.Point(3, 3);
            this._flex매출처그룹.Name = "_flex매출처그룹";
            this._flex매출처그룹.Rows.Count = 1;
            this._flex매출처그룹.Rows.DefaultSize = 20;
            this._flex매출처그룹.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex매출처그룹.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex매출처그룹.ShowSort = false;
            this._flex매출처그룹.Size = new System.Drawing.Size(807, 470);
            this._flex매출처그룹.StyleInfo = resources.GetString("_flex매출처그룹.StyleInfo");
            this._flex매출처그룹.TabIndex = 1;
            this._flex매출처그룹.UseGridCalculator = true;
            // 
            // tpg영업그룹
            // 
            this.tpg영업그룹.Controls.Add(this._flex영업그룹);
            this.tpg영업그룹.Location = new System.Drawing.Point(4, 22);
            this.tpg영업그룹.Name = "tpg영업그룹";
            this.tpg영업그룹.Size = new System.Drawing.Size(813, 476);
            this.tpg영업그룹.TabIndex = 2;
            this.tpg영업그룹.Text = "영업그룹";
            this.tpg영업그룹.UseVisualStyleBackColor = true;
            // 
            // _flex영업그룹
            // 
            this._flex영업그룹.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex영업그룹.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex영업그룹.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex영업그룹.AutoResize = false;
            this._flex영업그룹.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex영업그룹.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex영업그룹.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex영업그룹.EnabledHeaderCheck = true;
            this._flex영업그룹.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex영업그룹.Location = new System.Drawing.Point(0, 0);
            this._flex영업그룹.Name = "_flex영업그룹";
            this._flex영업그룹.Rows.Count = 1;
            this._flex영업그룹.Rows.DefaultSize = 20;
            this._flex영업그룹.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex영업그룹.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex영업그룹.ShowSort = false;
            this._flex영업그룹.Size = new System.Drawing.Size(813, 476);
            this._flex영업그룹.StyleInfo = resources.GetString("_flex영업그룹.StyleInfo");
            this._flex영업그룹.TabIndex = 1;
            this._flex영업그룹.UseGridCalculator = true;
            // 
            // tpg매출처
            // 
            this.tpg매출처.Controls.Add(this._flex매출처);
            this.tpg매출처.Location = new System.Drawing.Point(4, 22);
            this.tpg매출처.Name = "tpg매출처";
            this.tpg매출처.Size = new System.Drawing.Size(813, 476);
            this.tpg매출처.TabIndex = 3;
            this.tpg매출처.Text = "매출처";
            this.tpg매출처.UseVisualStyleBackColor = true;
            // 
            // _flex매출처
            // 
            this._flex매출처.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex매출처.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex매출처.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex매출처.AutoResize = false;
            this._flex매출처.ColumnInfo = "1,1,0,0,0,0,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex매출처.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex매출처.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex매출처.EnabledHeaderCheck = true;
            this._flex매출처.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex매출처.Location = new System.Drawing.Point(0, 0);
            this._flex매출처.Name = "_flex매출처";
            this._flex매출처.Rows.Count = 1;
            this._flex매출처.Rows.DefaultSize = 20;
            this._flex매출처.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex매출처.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex매출처.ShowSort = false;
            this._flex매출처.Size = new System.Drawing.Size(813, 476);
            this._flex매출처.StyleInfo = resources.GetString("_flex매출처.StyleInfo");
            this._flex매출처.TabIndex = 1;
            this._flex매출처.UseGridCalculator = true;
            // 
            // P_CZ_SA_PTR_PLAN
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "P_CZ_SA_PTR_PLAN";
            this.TitleText = "매출목표등록";
            this.mDataArea.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtp계획년도)).EndInit();
            this.tabControlExt1.ResumeLayout(false);
            this.tpgCC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexCC)).EndInit();
            this.tpg매출처그룹.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex매출처그룹)).EndInit();
            this.tpg영업그룹.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex영업그룹)).EndInit();
            this.tpg매출처.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex매출처)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.DatePicker dtp계획년도;
        private Duzon.Common.Controls.LabelExt lbl계획년도;
        private Duzon.Common.Controls.TabControlExt tabControlExt1;
        private System.Windows.Forms.TabPage tpgCC;
        private Dass.FlexGrid.FlexGrid _flexCC;
        private System.Windows.Forms.TabPage tpg매출처그룹;
        private Dass.FlexGrid.FlexGrid _flex매출처그룹;
        private System.Windows.Forms.TabPage tpg영업그룹;
        private Dass.FlexGrid.FlexGrid _flex영업그룹;
        private System.Windows.Forms.TabPage tpg매출처;
        private Dass.FlexGrid.FlexGrid _flex매출처;
    }
}
namespace cz
{
    partial class H_CZ_MA_CUSTOMIZE_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(H_CZ_MA_CUSTOMIZE_SUB));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp기간 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl기간 = new Duzon.Common.Controls.LabelExt();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt검색 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl검색 = new Duzon.Common.Controls.LabelExt();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn검색 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn멀티검색 = new Duzon.Common.Controls.RoundedButton(this.components);
            this._flex = new Dass.FlexGridLight.FlexGrid(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._flex, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 217F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(667, 544);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem2,
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(661, 62);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl2);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(651, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 0;
            // 
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.dtp기간);
            this.bpPanelControl2.Controls.Add(this.lbl기간);
            this.bpPanelControl2.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(291, 23);
            this.bpPanelControl2.TabIndex = 0;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // dtp기간
            // 
            this.dtp기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp기간.Location = new System.Drawing.Point(106, 0);
            this.dtp기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp기간.Name = "dtp기간";
            this.dtp기간.Size = new System.Drawing.Size(185, 21);
            this.dtp기간.TabIndex = 1;
            // 
            // lbl기간
            // 
            this.lbl기간.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl기간.Location = new System.Drawing.Point(0, 0);
            this.lbl기간.Name = "lbl기간";
            this.lbl기간.Size = new System.Drawing.Size(100, 23);
            this.lbl기간.TabIndex = 0;
            this.lbl기간.Text = "기간";
            this.lbl기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 23);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(651, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 1;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.txt검색);
            this.bpPanelControl1.Controls.Add(this.lbl검색);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(645, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // txt검색
            // 
            this.txt검색.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt검색.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt검색.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt검색.Location = new System.Drawing.Point(106, 0);
            this.txt검색.Name = "txt검색";
            this.txt검색.Size = new System.Drawing.Size(539, 21);
            this.txt검색.TabIndex = 1;
            // 
            // lbl검색
            // 
            this.lbl검색.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl검색.Location = new System.Drawing.Point(0, 0);
            this.lbl검색.Name = "lbl검색";
            this.lbl검색.Size = new System.Drawing.Size(100, 23);
            this.lbl검색.TabIndex = 0;
            this.lbl검색.Text = "검색";
            this.lbl검색.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn취소);
            this.flowLayoutPanel1.Controls.Add(this.btn확인);
            this.flowLayoutPanel1.Controls.Add(this.btn검색);
            this.flowLayoutPanel1.Controls.Add(this.btn멀티검색);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 71);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(661, 25);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btn취소
            // 
            this.btn취소.BackColor = System.Drawing.Color.Transparent;
            this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn취소.Location = new System.Drawing.Point(588, 3);
            this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn취소.Name = "btn취소";
            this.btn취소.Size = new System.Drawing.Size(70, 19);
            this.btn취소.TabIndex = 0;
            this.btn취소.TabStop = false;
            this.btn취소.Text = "취소";
            this.btn취소.UseVisualStyleBackColor = false;
            // 
            // btn확인
            // 
            this.btn확인.BackColor = System.Drawing.Color.Transparent;
            this.btn확인.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn확인.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn확인.Location = new System.Drawing.Point(512, 3);
            this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn확인.Name = "btn확인";
            this.btn확인.Size = new System.Drawing.Size(70, 19);
            this.btn확인.TabIndex = 1;
            this.btn확인.TabStop = false;
            this.btn확인.Text = "확인";
            this.btn확인.UseVisualStyleBackColor = false;
            // 
            // btn검색
            // 
            this.btn검색.BackColor = System.Drawing.Color.Transparent;
            this.btn검색.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn검색.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn검색.Location = new System.Drawing.Point(436, 3);
            this.btn검색.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn검색.Name = "btn검색";
            this.btn검색.Size = new System.Drawing.Size(70, 19);
            this.btn검색.TabIndex = 2;
            this.btn검색.TabStop = false;
            this.btn검색.Text = "검색";
            this.btn검색.UseVisualStyleBackColor = false;
            // 
            // btn멀티검색
            // 
            this.btn멀티검색.BackColor = System.Drawing.Color.Transparent;
            this.btn멀티검색.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn멀티검색.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn멀티검색.Location = new System.Drawing.Point(360, 3);
            this.btn멀티검색.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn멀티검색.Name = "btn멀티검색";
            this.btn멀티검색.Size = new System.Drawing.Size(70, 19);
            this.btn멀티검색.TabIndex = 3;
            this.btn멀티검색.TabStop = false;
            this.btn멀티검색.Text = "멀티검색";
            this.btn멀티검색.UseVisualStyleBackColor = false;
            // 
            // _flex
            // 
            this._flex.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex.AutoResize = false;
            this._flex.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex.Location = new System.Drawing.Point(3, 102);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 18;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(661, 439);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 2;
            // 
            // H_CZ_MA_CUSTOMIZE_SUB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btn취소;
            this.ClientSize = new System.Drawing.Size(669, 593);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimizeBox = false;
            this.Name = "H_CZ_MA_CUSTOMIZE_SUB";
            this.ShowIcon = true;
            this.ShowInTaskbar = true;
            this.Text = "ERP iU";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl2.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn취소;
        private Duzon.Common.Controls.RoundedButton btn확인;
        private Duzon.Common.Controls.RoundedButton btn검색;
        private Dass.FlexGridLight.FlexGrid _flex;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.TextBoxExt txt검색;
        private Duzon.Common.Controls.LabelExt lbl검색;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.PeriodPicker dtp기간;
        private Duzon.Common.Controls.LabelExt lbl기간;
        private Duzon.Common.Controls.RoundedButton btn멀티검색;
    }
}
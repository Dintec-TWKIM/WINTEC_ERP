namespace cz
{
    partial class P_CZ_SA_IVMNG_INVOICE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_SA_IVMNG_INVOICE));
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.chkDHL발송자동선택 = new Duzon.Common.Controls.CheckBoxExt();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl번호 = new Duzon.Common.Controls.LabelExt();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDHL발송 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._flex = new Dass.FlexGrid.FlexGrid(this.components);
            this.btnDHL픽업 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex)).BeginInit();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this.tableLayoutPanel1);
            this.mDataArea.Size = new System.Drawing.Size(881, 499);
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(875, 39);
            this.oneGrid1.TabIndex = 2;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.chkDHL발송자동선택);
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(865, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // chkDHL발송자동선택
            // 
            this.chkDHL발송자동선택.AutoSize = true;
            this.chkDHL발송자동선택.Location = new System.Drawing.Point(329, 1);
            this.chkDHL발송자동선택.Name = "chkDHL발송자동선택";
            this.chkDHL발송자동선택.Size = new System.Drawing.Size(121, 24);
            this.chkDHL발송자동선택.TabIndex = 1;
            this.chkDHL발송자동선택.Text = "DHL발송자동선택";
            this.chkDHL발송자동선택.TextDD = null;
            this.chkDHL발송자동선택.UseVisualStyleBackColor = true;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.txt번호);
            this.bpPanelControl1.Controls.Add(this.lbl번호);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(325, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // txt번호
            // 
            this.txt번호.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.txt번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt번호.Location = new System.Drawing.Point(106, 0);
            this.txt번호.Name = "txt번호";
            this.txt번호.Size = new System.Drawing.Size(219, 21);
            this.txt번호.TabIndex = 1;
            // 
            // lbl번호
            // 
            this.lbl번호.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl번호.Location = new System.Drawing.Point(0, 0);
            this.lbl번호.Name = "lbl번호";
            this.lbl번호.Size = new System.Drawing.Size(100, 23);
            this.lbl번호.TabIndex = 0;
            this.lbl번호.Text = "번호";
            this.lbl번호.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this.btnDHL발송);
            this.flowLayoutPanel2.Controls.Add(this.btnDHL픽업);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(720, 10);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(158, 22);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // btnDHL발송
            // 
            this.btnDHL발송.BackColor = System.Drawing.Color.Transparent;
            this.btnDHL발송.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDHL발송.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDHL발송.Location = new System.Drawing.Point(85, 3);
            this.btnDHL발송.MaximumSize = new System.Drawing.Size(0, 19);
            this.btnDHL발송.Name = "btnDHL발송";
            this.btnDHL발송.Size = new System.Drawing.Size(70, 19);
            this.btnDHL발송.TabIndex = 0;
            this.btnDHL발송.TabStop = false;
            this.btnDHL발송.Text = "DHL발송";
            this.btnDHL발송.UseVisualStyleBackColor = false;
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(881, 499);
            this.tableLayoutPanel1.TabIndex = 0;
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
            this._flex.Location = new System.Drawing.Point(3, 48);
            this._flex.Name = "_flex";
            this._flex.Rows.Count = 1;
            this._flex.Rows.DefaultSize = 20;
            this._flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex.ShowSort = false;
            this._flex.Size = new System.Drawing.Size(875, 448);
            this._flex.StyleInfo = resources.GetString("_flex.StyleInfo");
            this._flex.TabIndex = 3;
            this._flex.UseGridCalculator = true;
            // 
            // btnDHL픽업
            // 
            this.btnDHL픽업.BackColor = System.Drawing.Color.Transparent;
            this.btnDHL픽업.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDHL픽업.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDHL픽업.Location = new System.Drawing.Point(9, 3);
            this.btnDHL픽업.MaximumSize = new System.Drawing.Size(0, 19);
            this.btnDHL픽업.Name = "btnDHL픽업";
            this.btnDHL픽업.Size = new System.Drawing.Size(70, 19);
            this.btnDHL픽업.TabIndex = 1;
            this.btnDHL픽업.TabStop = false;
            this.btnDHL픽업.Text = "DHL픽업";
            this.btnDHL픽업.UseVisualStyleBackColor = false;
            // 
            // P_CZ_SA_IVMNG_INVOICE
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.flowLayoutPanel2);
            this.Name = "P_CZ_SA_IVMNG_INVOICE";
            this.Size = new System.Drawing.Size(881, 539);
            this.TitleText = "발송주소";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.flowLayoutPanel2, 0);
            this.mDataArea.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.oneGridItem1.PerformLayout();
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.LabelExt lbl번호;
        private Duzon.Common.Controls.TextBoxExt txt번호;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Duzon.Common.Controls.RoundedButton btnDHL발송;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Dass.FlexGrid.FlexGrid _flex;
        private Duzon.Common.Controls.CheckBoxExt chkDHL발송자동선택;
        private Duzon.Common.Controls.RoundedButton btnDHL픽업;
    }
}
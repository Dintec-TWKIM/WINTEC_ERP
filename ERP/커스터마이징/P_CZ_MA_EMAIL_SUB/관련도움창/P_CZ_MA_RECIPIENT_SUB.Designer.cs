namespace cz
{
    partial class P_CZ_MA_RECIPIENT_SUB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_MA_RECIPIENT_SUB));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem2 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt검색 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl검색 = new Duzon.Common.Controls.LabelExt();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn확인 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.tabControlExt1 = new Duzon.Common.Controls.TabControlExt();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._flex사내담당자 = new Dass.FlexGrid.FlexGrid(this.components);
            this._flex거래처 = new Dass.FlexGrid.FlexGrid(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.oneGridItem2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControlExt1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flex사내담당자)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex거래처)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControlExt1, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 181F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(720, 457);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem2});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(714, 40);
            this.oneGrid1.TabIndex = 0;
            // 
            // oneGridItem2
            // 
            this.oneGridItem2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem2.Controls.Add(this.bpPanelControl1);
            this.oneGridItem2.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoSize;
            this.oneGridItem2.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem2.Name = "oneGridItem2";
            this.oneGridItem2.Size = new System.Drawing.Size(704, 23);
            this.oneGridItem2.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem2.TabIndex = 0;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.txt검색);
            this.bpPanelControl1.Controls.Add(this.lbl검색);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(698, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // txt검색
            // 
            this.txt검색.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt검색.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt검색.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt검색.Location = new System.Drawing.Point(106, 1);
            this.txt검색.Name = "txt검색";
            this.txt검색.Size = new System.Drawing.Size(592, 21);
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
            this.flowLayoutPanel1.Controls.Add(this.btn조회);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 49);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(714, 27);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btn취소
            // 
            this.btn취소.BackColor = System.Drawing.Color.Transparent;
            this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn취소.Location = new System.Drawing.Point(641, 3);
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
            this.btn확인.Location = new System.Drawing.Point(565, 3);
            this.btn확인.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn확인.Name = "btn확인";
            this.btn확인.Size = new System.Drawing.Size(70, 19);
            this.btn확인.TabIndex = 1;
            this.btn확인.TabStop = false;
            this.btn확인.Text = "확인";
            this.btn확인.UseVisualStyleBackColor = false;
            // 
            // btn조회
            // 
            this.btn조회.BackColor = System.Drawing.Color.Transparent;
            this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn조회.Location = new System.Drawing.Point(489, 3);
            this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn조회.Name = "btn조회";
            this.btn조회.Size = new System.Drawing.Size(70, 19);
            this.btn조회.TabIndex = 2;
            this.btn조회.TabStop = false;
            this.btn조회.Text = "조회";
            this.btn조회.UseVisualStyleBackColor = false;
            // 
            // tabControlExt1
            // 
            this.tabControlExt1.Controls.Add(this.tabPage1);
            this.tabControlExt1.Controls.Add(this.tabPage2);
            this.tabControlExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlExt1.Location = new System.Drawing.Point(3, 82);
            this.tabControlExt1.Name = "tabControlExt1";
            this.tabControlExt1.SelectedIndex = 0;
            this.tabControlExt1.Size = new System.Drawing.Size(714, 372);
            this.tabControlExt1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._flex사내담당자);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(706, 321);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "사내담당자";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._flex거래처);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(706, 346);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "거래처";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _flex사내담당자
            // 
            this._flex사내담당자.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex사내담당자.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex사내담당자.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex사내담당자.AutoResize = false;
            this._flex사내담당자.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex사내담당자.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex사내담당자.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex사내담당자.EnabledHeaderCheck = true;
            this._flex사내담당자.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex사내담당자.Location = new System.Drawing.Point(3, 3);
            this._flex사내담당자.Name = "_flex사내담당자";
            this._flex사내담당자.Rows.Count = 1;
            this._flex사내담당자.Rows.DefaultSize = 18;
            this._flex사내담당자.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex사내담당자.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex사내담당자.ShowSort = false;
            this._flex사내담당자.Size = new System.Drawing.Size(700, 315);
            this._flex사내담당자.StyleInfo = resources.GetString("_flex사내담당자.StyleInfo");
            this._flex사내담당자.TabIndex = 0;
            this._flex사내담당자.UseGridCalculator = true;
            // 
            // _flex거래처
            // 
            this._flex거래처.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flex거래처.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flex거래처.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flex거래처.AutoResize = false;
            this._flex거래처.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flex거래처.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flex거래처.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flex거래처.EnabledHeaderCheck = true;
            this._flex거래처.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flex거래처.Location = new System.Drawing.Point(3, 3);
            this._flex거래처.Name = "_flex거래처";
            this._flex거래처.Rows.Count = 1;
            this._flex거래처.Rows.DefaultSize = 18;
            this._flex거래처.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flex거래처.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flex거래처.ShowSort = false;
            this._flex거래처.Size = new System.Drawing.Size(700, 340);
            this._flex거래처.StyleInfo = resources.GetString("_flex거래처.StyleInfo");
            this._flex거래처.TabIndex = 0;
            this._flex거래처.UseGridCalculator = true;
            // 
            // P_CZ_MA_RECIPIENT_SUB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn취소;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(721, 506);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_CZ_MA_RECIPIENT_SUB";
            this.Text = "DINTEC ERP iU";
            this.TitleText = "메일주소";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.oneGridItem2.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bpPanelControl1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControlExt1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flex사내담당자)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flex거래처)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn취소;
        private Duzon.Common.Controls.RoundedButton btn확인;
        private Duzon.Common.Controls.RoundedButton btn조회;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.TextBoxExt txt검색;
        private Duzon.Common.Controls.LabelExt lbl검색;
        private Duzon.Common.Controls.TabControlExt tabControlExt1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Dass.FlexGrid.FlexGrid _flex사내담당자;
        private Dass.FlexGrid.FlexGrid _flex거래처;
    }
}
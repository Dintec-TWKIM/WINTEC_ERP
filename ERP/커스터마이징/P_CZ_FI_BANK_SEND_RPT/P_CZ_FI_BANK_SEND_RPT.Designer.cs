namespace cz
{
    partial class P_CZ_FI_BANK_SEND_RPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CZ_FI_BANK_SEND_RPT));
            this._tlayMain = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp이체일자 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl이체일자 = new Duzon.Common.Controls.LabelExt();
            this.bppnl파일작성기간 = new Duzon.Common.BpControls.BpPanelControl();
            this.dtp파일작성기간 = new Duzon.Common.Controls.PeriodPicker();
            this.lbl파일작성기간 = new Duzon.Common.Controls.LabelExt();
            this.btn파일생성 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn메일발송 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn계좌번호갱신 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.mDataArea.SuspendLayout();
            this._tlayMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.bppnl파일작성기간.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mDataArea
            // 
            this.mDataArea.Controls.Add(this._tlayMain);
            this.mDataArea.Size = new System.Drawing.Size(929, 756);
            // 
            // _tlayMain
            // 
            this._tlayMain.ColumnCount = 1;
            this._tlayMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlayMain.Controls.Add(this.splitContainer1, 0, 1);
            this._tlayMain.Controls.Add(this.oneGrid1, 0, 0);
            this._tlayMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tlayMain.Location = new System.Drawing.Point(0, 0);
            this._tlayMain.Name = "_tlayMain";
            this._tlayMain.RowCount = 2;
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tlayMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._tlayMain.Size = new System.Drawing.Size(929, 756);
            this._tlayMain.TabIndex = 81;
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
            this.splitContainer1.Size = new System.Drawing.Size(923, 704);
            this.splitContainer1.SplitterDistance = 331;
            this.splitContainer1.TabIndex = 4;
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
            this._flexH.Size = new System.Drawing.Size(923, 331);
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
            this._flexL.Size = new System.Drawing.Size(923, 369);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 0;
            this._flexL.UseGridCalculator = true;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(923, 40);
            this.oneGrid1.TabIndex = 5;
            // 
            // oneGridItem1
            // 
            this.oneGridItem1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneGridItem1.Controls.Add(this.bpPanelControl1);
            this.oneGridItem1.Controls.Add(this.bppnl파일작성기간);
            this.oneGridItem1.ItemSizeMode = Duzon.Erpiu.Windows.OneControls.ItemSizeMode.AutoLocation;
            this.oneGridItem1.Location = new System.Drawing.Point(0, 0);
            this.oneGridItem1.Name = "oneGridItem1";
            this.oneGridItem1.Size = new System.Drawing.Size(913, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp이체일자);
            this.bpPanelControl1.Controls.Add(this.lbl이체일자);
            this.bpPanelControl1.Location = new System.Drawing.Point(297, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(293, 23);
            this.bpPanelControl1.TabIndex = 2;
            this.bpPanelControl1.Text = "bpPanelControl2";
            // 
            // dtp이체일자
            // 
            this.dtp이체일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp이체일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp이체일자.Location = new System.Drawing.Point(108, 0);
            this.dtp이체일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp이체일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp이체일자.Name = "dtp이체일자";
            this.dtp이체일자.Size = new System.Drawing.Size(185, 21);
            this.dtp이체일자.TabIndex = 1;
            // 
            // lbl이체일자
            // 
            this.lbl이체일자.BackColor = System.Drawing.Color.Transparent;
            this.lbl이체일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl이체일자.Location = new System.Drawing.Point(0, 0);
            this.lbl이체일자.Name = "lbl이체일자";
            this.lbl이체일자.Size = new System.Drawing.Size(100, 23);
            this.lbl이체일자.TabIndex = 0;
            this.lbl이체일자.Tag = "";
            this.lbl이체일자.Text = "이체일자";
            this.lbl이체일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bppnl파일작성기간
            // 
            this.bppnl파일작성기간.Controls.Add(this.dtp파일작성기간);
            this.bppnl파일작성기간.Controls.Add(this.lbl파일작성기간);
            this.bppnl파일작성기간.Location = new System.Drawing.Point(2, 1);
            this.bppnl파일작성기간.Name = "bppnl파일작성기간";
            this.bppnl파일작성기간.Size = new System.Drawing.Size(293, 23);
            this.bppnl파일작성기간.TabIndex = 1;
            this.bppnl파일작성기간.Text = "bpPanelControl2";
            // 
            // dtp파일작성기간
            // 
            this.dtp파일작성기간.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp파일작성기간.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp파일작성기간.IsNecessaryCondition = true;
            this.dtp파일작성기간.Location = new System.Drawing.Point(108, 0);
            this.dtp파일작성기간.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp파일작성기간.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp파일작성기간.Name = "dtp파일작성기간";
            this.dtp파일작성기간.Size = new System.Drawing.Size(185, 21);
            this.dtp파일작성기간.TabIndex = 1;
            // 
            // lbl파일작성기간
            // 
            this.lbl파일작성기간.BackColor = System.Drawing.Color.Transparent;
            this.lbl파일작성기간.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl파일작성기간.Location = new System.Drawing.Point(0, 0);
            this.lbl파일작성기간.Name = "lbl파일작성기간";
            this.lbl파일작성기간.Size = new System.Drawing.Size(100, 23);
            this.lbl파일작성기간.TabIndex = 0;
            this.lbl파일작성기간.Tag = "";
            this.lbl파일작성기간.Text = "파일작성기간";
            this.lbl파일작성기간.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn파일생성
            // 
            this.btn파일생성.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn파일생성.BackColor = System.Drawing.Color.White;
            this.btn파일생성.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn파일생성.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn파일생성.Location = new System.Drawing.Point(201, 3);
            this.btn파일생성.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btn파일생성.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn파일생성.Name = "btn파일생성";
            this.btn파일생성.Size = new System.Drawing.Size(90, 19);
            this.btn파일생성.TabIndex = 133;
            this.btn파일생성.TabStop = false;
            this.btn파일생성.Text = "파일생성";
            this.btn파일생성.UseVisualStyleBackColor = false;
            this.btn파일생성.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btn파일생성);
            this.flowLayoutPanel1.Controls.Add(this.btn메일발송);
            this.flowLayoutPanel1.Controls.Add(this.btn계좌번호갱신);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(639, 9);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(294, 24);
            this.flowLayoutPanel1.TabIndex = 134;
            // 
            // btn메일발송
            // 
            this.btn메일발송.BackColor = System.Drawing.Color.Transparent;
            this.btn메일발송.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn메일발송.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn메일발송.Location = new System.Drawing.Point(108, 3);
            this.btn메일발송.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btn메일발송.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn메일발송.Name = "btn메일발송";
            this.btn메일발송.Size = new System.Drawing.Size(90, 19);
            this.btn메일발송.TabIndex = 134;
            this.btn메일발송.TabStop = false;
            this.btn메일발송.Text = "메일발송";
            this.btn메일발송.UseVisualStyleBackColor = false;
            this.btn메일발송.Visible = false;
            // 
            // btn계좌번호갱신
            // 
            this.btn계좌번호갱신.BackColor = System.Drawing.Color.Transparent;
            this.btn계좌번호갱신.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn계좌번호갱신.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn계좌번호갱신.Location = new System.Drawing.Point(16, 3);
            this.btn계좌번호갱신.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn계좌번호갱신.Name = "btn계좌번호갱신";
            this.btn계좌번호갱신.Size = new System.Drawing.Size(89, 19);
            this.btn계좌번호갱신.TabIndex = 135;
            this.btn계좌번호갱신.TabStop = false;
            this.btn계좌번호갱신.Text = "계좌번호갱신";
            this.btn계좌번호갱신.UseVisualStyleBackColor = false;
            // 
            // P_CZ_FI_BANK_SEND_RPT
            // 
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "P_CZ_FI_BANK_SEND_RPT";
            this.Size = new System.Drawing.Size(929, 796);
            this.TitleText = "이체내역현황";
            this.Controls.SetChildIndex(this.mDataArea, 0);
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.mDataArea.ResumeLayout(false);
            this._tlayMain.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl1.ResumeLayout(false);
            this.bppnl파일작성기간.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel _tlayMain;
        private Duzon.Common.Controls.LabelExt lbl파일작성기간;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Duzon.Common.Controls.RoundedButton btn파일생성;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bppnl파일작성기간;
        private Duzon.Common.Controls.PeriodPicker dtp파일작성기간;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Common.Controls.RoundedButton btn메일발송;
        #endregion
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexL;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.LabelExt lbl이체일자;
        private Duzon.Common.Controls.PeriodPicker dtp이체일자;
        private Duzon.Common.Controls.RoundedButton btn계좌번호갱신;
    }
}
namespace cz
{
    partial class P_SA_GIRR_REG_SUB
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SA_GIRR_REG_SUB));
            this.btn조회 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn적용 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.btn취소 = new Duzon.Common.Controls.RoundedButton(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._flexH = new Dass.FlexGrid.FlexGrid(this.components);
            this._flexL = new Dass.FlexGrid.FlexGrid(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.oneGrid1 = new Duzon.Erpiu.Windows.OneControls.OneGrid();
            this.oneGridItem1 = new Duzon.Erpiu.Windows.OneControls.OneGridItem();
            this.bpPanelControl4 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt협조전번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl협조전번호 = new Duzon.Common.Controls.LabelExt();
            this.bpPanelControl2 = new Duzon.Common.BpControls.BpPanelControl();
            this.txt수주번호 = new Duzon.Common.Controls.TextBoxExt();
            this.lbl수주번호 = new Duzon.Common.Controls.LabelExt();
            this.lbl출고일자 = new Duzon.Common.Controls.LabelExt();
            this.dtp출고일자 = new Duzon.Common.Controls.PeriodPicker();
            this.bpPanelControl1 = new Duzon.Common.BpControls.BpPanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.oneGridItem1.SuspendLayout();
            this.bpPanelControl4.SuspendLayout();
            this.bpPanelControl2.SuspendLayout();
            this.bpPanelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn조회
            // 
            this.btn조회.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn조회.BackColor = System.Drawing.Color.White;
            this.btn조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn조회.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn조회.Location = new System.Drawing.Point(710, 3);
            this.btn조회.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn조회.Name = "btn조회";
            this.btn조회.Size = new System.Drawing.Size(60, 19);
            this.btn조회.TabIndex = 17;
            this.btn조회.TabStop = false;
            this.btn조회.Tag = "Q_QUERY";
            this.btn조회.Text = "조회";
            this.btn조회.UseVisualStyleBackColor = false;
            // 
            // btn적용
            // 
            this.btn적용.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn적용.BackColor = System.Drawing.Color.White;
            this.btn적용.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn적용.Enabled = false;
            this.btn적용.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn적용.Location = new System.Drawing.Point(776, 3);
            this.btn적용.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn적용.Name = "btn적용";
            this.btn적용.Size = new System.Drawing.Size(60, 19);
            this.btn적용.TabIndex = 18;
            this.btn적용.TabStop = false;
            this.btn적용.Text = "적용";
            this.btn적용.UseVisualStyleBackColor = false;
            // 
            // btn취소
            // 
            this.btn취소.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn취소.BackColor = System.Drawing.Color.White;
            this.btn취소.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn취소.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn취소.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn취소.Location = new System.Drawing.Point(842, 3);
            this.btn취소.MaximumSize = new System.Drawing.Size(0, 19);
            this.btn취소.Name = "btn취소";
            this.btn취소.Size = new System.Drawing.Size(60, 19);
            this.btn취소.TabIndex = 19;
            this.btn취소.TabStop = false;
            this.btn취소.Tag = "Q_CANCEL";
            this.btn취소.Text = "취소";
            this.btn취소.UseVisualStyleBackColor = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 84);
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
            this.splitContainer1.Size = new System.Drawing.Size(905, 551);
            this.splitContainer1.SplitterDistance = 269;
            this.splitContainer1.TabIndex = 20;
            // 
            // _flexH
            // 
            this._flexH.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Both;
            this._flexH.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this._flexH.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this._flexH.AutoResize = false;
            this._flexH.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexH.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexH.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexH.EnabledHeaderCheck = true;
            this._flexH.Font = new System.Drawing.Font("굴림", 9F);
            this._flexH.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexH.Location = new System.Drawing.Point(0, 0);
            this._flexH.Name = "_flexH";
            this._flexH.Rows.Count = 1;
            this._flexH.Rows.DefaultSize = 18;
            this._flexH.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexH.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexH.ShowSort = false;
            this._flexH.Size = new System.Drawing.Size(905, 269);
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
            this._flexL.ColumnInfo = "1,1,0,0,0,90,Columns:0{TextAlign:CenterCenter;TextAlignFixed:CenterCenter;}\t";
            this._flexL.Dock = System.Windows.Forms.DockStyle.Fill;
            this._flexL.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this._flexL.EnabledHeaderCheck = true;
            this._flexL.Font = new System.Drawing.Font("굴림", 9F);
            this._flexL.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
            this._flexL.Location = new System.Drawing.Point(0, 0);
            this._flexL.Name = "_flexL";
            this._flexL.Rows.Count = 1;
            this._flexL.Rows.DefaultSize = 18;
            this._flexL.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this._flexL.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this._flexL.ShowSort = false;
            this._flexL.Size = new System.Drawing.Size(905, 278);
            this._flexL.StyleInfo = resources.GetString("_flexL.StyleInfo");
            this._flexL.TabIndex = 0;
            this._flexL.UseGridCalculator = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.oneGrid1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(911, 638);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn취소);
            this.flowLayoutPanel1.Controls.Add(this.btn적용);
            this.flowLayoutPanel1.Controls.Add(this.btn조회);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 51);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(905, 27);
            this.flowLayoutPanel1.TabIndex = 22;
            // 
            // oneGrid1
            // 
            this.oneGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oneGrid1.ItemCollection.AddRange(new Duzon.Erpiu.Windows.OneControls.OneGridItem[] {
            this.oneGridItem1});
            this.oneGrid1.Location = new System.Drawing.Point(3, 3);
            this.oneGrid1.Name = "oneGrid1";
            this.oneGrid1.Size = new System.Drawing.Size(905, 42);
            this.oneGrid1.TabIndex = 23;
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
            this.oneGridItem1.Size = new System.Drawing.Size(895, 23);
            this.oneGridItem1.SizeMode = Duzon.Erpiu.Windows.OneControls.SizeMode.AutoSize;
            this.oneGridItem1.TabIndex = 0;
            // 
            // bpPanelControl4
            // 
            this.bpPanelControl4.Controls.Add(this.txt협조전번호);
            this.bpPanelControl4.Controls.Add(this.lbl협조전번호);
            this.bpPanelControl4.Location = new System.Drawing.Point(590, 1);
            this.bpPanelControl4.Name = "bpPanelControl4";
            this.bpPanelControl4.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl4.TabIndex = 3;
            this.bpPanelControl4.Text = "bpPanelControl4";
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
            // bpPanelControl2
            // 
            this.bpPanelControl2.Controls.Add(this.txt수주번호);
            this.bpPanelControl2.Controls.Add(this.lbl수주번호);
            this.bpPanelControl2.Location = new System.Drawing.Point(296, 1);
            this.bpPanelControl2.Name = "bpPanelControl2";
            this.bpPanelControl2.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl2.TabIndex = 2;
            this.bpPanelControl2.Text = "bpPanelControl2";
            // 
            // txt수주번호
            // 
            this.txt수주번호.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(199)))), ((int)(((byte)(217)))));
            this.txt수주번호.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt수주번호.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt수주번호.Location = new System.Drawing.Point(106, 0);
            this.txt수주번호.Name = "txt수주번호";
            this.txt수주번호.ReadOnly = true;
            this.txt수주번호.Size = new System.Drawing.Size(186, 21);
            this.txt수주번호.TabIndex = 1;
            this.txt수주번호.TabStop = false;
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
            // lbl출고일자
            // 
            this.lbl출고일자.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl출고일자.Location = new System.Drawing.Point(0, 0);
            this.lbl출고일자.Name = "lbl출고일자";
            this.lbl출고일자.Size = new System.Drawing.Size(100, 23);
            this.lbl출고일자.TabIndex = 0;
            this.lbl출고일자.Text = "출고일자";
            this.lbl출고일자.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtp출고일자
            // 
            this.dtp출고일자.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtp출고일자.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtp출고일자.IsNecessaryCondition = true;
            this.dtp출고일자.Location = new System.Drawing.Point(107, 0);
            this.dtp출고일자.MaximumSize = new System.Drawing.Size(185, 21);
            this.dtp출고일자.MinimumSize = new System.Drawing.Size(185, 21);
            this.dtp출고일자.Name = "dtp출고일자";
            this.dtp출고일자.Size = new System.Drawing.Size(185, 21);
            this.dtp출고일자.TabIndex = 1;
            // 
            // bpPanelControl1
            // 
            this.bpPanelControl1.Controls.Add(this.dtp출고일자);
            this.bpPanelControl1.Controls.Add(this.lbl출고일자);
            this.bpPanelControl1.Location = new System.Drawing.Point(2, 1);
            this.bpPanelControl1.Name = "bpPanelControl1";
            this.bpPanelControl1.Size = new System.Drawing.Size(292, 23);
            this.bpPanelControl1.TabIndex = 0;
            this.bpPanelControl1.Text = "bpPanelControl1";
            // 
            // P_SA_GIRR_REG_SUB
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CaptionPaint = true;
            this.ClientSize = new System.Drawing.Size(915, 689);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P_SA_GIRR_REG_SUB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ERP iU";
            this.TitleText = "출고적용";
            ((System.ComponentModel.ISupportInitialize)(this.closeButton)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._flexH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._flexL)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.oneGridItem1.ResumeLayout(false);
            this.bpPanelControl4.ResumeLayout(false);
            this.bpPanelControl4.PerformLayout();
            this.bpPanelControl2.ResumeLayout(false);
            this.bpPanelControl2.PerformLayout();
            this.bpPanelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Duzon.Common.Controls.RoundedButton btn조회;
        private Duzon.Common.Controls.RoundedButton btn적용;
        private Duzon.Common.Controls.RoundedButton btn취소;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Dass.FlexGrid.FlexGrid _flexH;
        private Dass.FlexGrid.FlexGrid _flexL;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Duzon.Erpiu.Windows.OneControls.OneGrid oneGrid1;
        private Duzon.Erpiu.Windows.OneControls.OneGridItem oneGridItem1;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl2;
        private Duzon.Common.Controls.TextBoxExt txt수주번호;
        private Duzon.Common.Controls.LabelExt lbl수주번호;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl4;
        private Duzon.Common.Controls.TextBoxExt txt협조전번호;
        private Duzon.Common.Controls.LabelExt lbl협조전번호;
        private Duzon.Common.BpControls.BpPanelControl bpPanelControl1;
        private Duzon.Common.Controls.PeriodPicker dtp출고일자;
        private Duzon.Common.Controls.LabelExt lbl출고일자;
    }
}
